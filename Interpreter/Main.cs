using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

//// タスク管理のクラス////
// タスクを表すクラス
public class TaskObject
{
    public required string Name { get; set; }
    public DateTime Start { get; set; }
    public DateTime? Finish { get; set; }
}

// タスクを管理しておくクラス（デザインパターン外）
public static class TaskRuntime
{
    private static readonly List<TaskObject> _tasks = new();
    private static TaskObject? _current;

    public static IReadOnlyList<TaskObject> Tasks => _tasks;

    public static Func<DateTime> Clock { get; set; } = () => DateTime.Now;

    public static void StartTask(string name)
    {
        DateTime now = Clock();

        // 進行中があれば同時刻でクローズ＆出力
        if (_current != null && _current.Finish == null)
        {
            _current.Finish = now;
            Console.WriteLine($"{_current.Name} 〜 {now:HH:mm:ss}");
        }

        Console.WriteLine($"{name} {now:HH:mm:ss} ~");

        _current = new TaskObject { Name = name, Start = now };
        _tasks.Add(_current);
    }

    public static void FinishTask(string? nameOverride = null)
    {
        DateTime now = Clock();

        string? name = nameOverride ?? _current?.Name;
        if (name != null)
            Console.WriteLine($"{name} 〜 {now:HH:mm:ss}");

        if (_current != null)
        {
            _current.Finish = now;
            _current = null;
        }
    }
}

//// interpreter パターン////////////////////////////////////////////////////////////
// AbstractExpression役 Node
// parse メソッドを規定
public abstract class Node
{
    public abstract void Parse(Context context);
}

// パースに失敗した場合に投げる例外
public class ParseException : Exception
{
    public ParseException(string msg) : base(msg) { }
}

// Context役
// 正規表現でのパーサーと、トークンの管理をする
public class Context
{
    private static readonly Regex _tokenRegex =
        new Regex("\\S+", RegexOptions.Compiled);

    private readonly string[] _tokens;
    private string? _lastToken;
    private int _index;

    public Context(string text)
    {
        var m = _tokenRegex.Matches(text);
        _tokens = new string[m.Count];
        for (int i = 0; i < m.Count; i++) _tokens[i] = m[i].Value;
        _index = 0;
        NextToken();
    }

    public string? NextToken()
    {
        _lastToken = _index < _tokens.Length ? _tokens[_index++] : null;
        return _lastToken;
    }

    public string? CurrentToken() => _lastToken;

    public void SkipToken(string token)
    {
        if (CurrentToken() == null)
            throw new ParseException($"'{token}' expected, but no more token.");
        if (!token.Equals(CurrentToken(), StringComparison.OrdinalIgnoreCase))
            throw new ParseException($"'{token}' expected, but '{CurrentToken()}' found.");
        NextToken();
    }

    public string CurrentString()
    {
        if (CurrentToken() == null)
            throw new ParseException("String expected.");
        return CurrentToken()!;
    }
}


// NonTerminalExpression役
public class ProgramNode : Node
{
    private CommandListNode _commands;

    public override void Parse(Context ctx)
    {
        // スクリプト行の判断をする処理は保留
        // if ("script".Equals(ctx.CurrentToken(), StringComparison.OrdinalIgnoreCase))
        //     ctx.SkipToken("script");

        _commands = new CommandListNode();
        _commands.Parse(ctx);
    }
    public override string ToString() => "[program " + _commands + "]";
}

// NonTerminalExpression役
public class CommandListNode : Node
{
    private readonly List<Node> _list = new();

    public override void Parse(Context ctx)
    {
        while (ctx.CurrentToken() != null)
        {
            Node n = new CommandNode();
            n.Parse(ctx);
            _list.Add(n);
        }
    }
    public override string ToString() => string.Join(" ", _list);
}

// TerminalExpression役
public class CommandNode : Node
{
    private Node? _node;

    public override void Parse(Context ctx)
    {
        string tok = ctx.CurrentToken() ?? throw new ParseException("Unexpected end of input");
        if ("WHAT".Equals(tok, StringComparison.OrdinalIgnoreCase))
            _node = new WhatCommandNode();
        else if ("DONE".Equals(tok, StringComparison.OrdinalIgnoreCase))
            _node = new DoneCommandNode();
        else
            throw new ParseException($"Unknown command '{tok}'");

        _node.Parse(ctx);   // ← Parse 中に副作用が走る
    }

    public override string ToString() => _node?.ToString() ?? "[null]";
}

// TerminalExpression役 終端
// タスクを開始する
public class WhatCommandNode : Node
{
    private string? _taskName;

    public override void Parse(Context ctx)
    {
        ctx.SkipToken("WHAT");
        _taskName = ctx.CurrentString();
        ctx.NextToken();

        if (_taskName == null)
            throw new ParseException("Task name is required");

        TaskRuntime.StartTask(_taskName);
    }

    public override string ToString()
    {
        return string.Format("[WHAT {0}]", _taskName);
    }
}

// TerminalExpression役 終端
// タスクを終了する
public class DoneCommandNode : Node
{
    private string? _taskName;

    public override void Parse(Context ctx)
    {
        ctx.SkipToken("DONE");
        if (ctx.CurrentToken() != null)
        {
            _taskName = ctx.CurrentString();
            ctx.NextToken();
        }

        TaskRuntime.FinishTask(_taskName);
    }

    public override string ToString()
    {
        if (_taskName != null)
            return string.Format("[DONE {0}]", _taskName);
        else
            return "[DONE]";
    }
}

// クライアント
class Program
{
    static void Main()
    {
        if (!File.Exists("program.txt"))
        {
            Console.WriteLine("program.txt が見つかりません。");
            return;
        }

        // 動作テストとして1行ごとに1時間進めて投稿を解釈
        DateTime demo = new DateTime(2025, 6, 11, 0, 0, 0);
        TaskRuntime.Clock = () => demo;
        // 一行を一投稿として解釈
        foreach (var line in File.ReadAllLines("program.txt"))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            Console.WriteLine($"text = \"{line}\"");

            try
            {
                Node root = new ProgramNode();
                root.Parse(new Context(line));
                Console.WriteLine("node = " + root);
            }
            catch (ParseException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine();
            demo = demo.AddHours(1);
        }

        Console.WriteLine("--- 最終的な記録集計 ---");
        foreach (var t in TaskRuntime.Tasks)
            Console.WriteLine($"{t.Name}: {t.Start:HH:mm:ss} 〜 {t.Finish:HH:mm:ss}");
    }
}
