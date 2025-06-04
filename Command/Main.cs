using System;
using System.Collections.Generic;
using System.Threading;

// コマンド インターフェイス
public interface ICommand
{
    void Execute();
}

// レシーバー インターフェイス
public interface IColorable
{
    void SetColor(byte r, byte g, byte b);
}

public class ConsoleColorReceiver : IColorable
{
    public void SetColor(byte r, byte g, byte b)
    {
        // 24bit TrueColor 背景
        Console.Write($"\u001b[48;2;{r};{g};{b}m");

        // 画面クリア＋カーソル原点
        Console.Write("\u001b[2J\u001b[H");

        // 全面塗りつぶし（スクロールバックを埋める）
        int h = Console.WindowHeight;
        int w = Console.WindowWidth;
        string blank = new string(' ', w);

        for (int i = 0; i < h; i++)
        {
            Console.WriteLine(blank);
        }
        Console.SetCursorPosition(0, 0);
    }
}

// コンクリート コマンド 履歴保持用
public class MacroCommand : ICommand
{
    private readonly List<ICommand> _commands;

    public MacroCommand()
    {
        _commands = new List<ICommand>();
    }

    public void Execute()
    {
        foreach (var command in _commands)
        {
            command.Execute();
            Thread.Sleep(1000);
        }
    }

    public void AddCommand(ICommand command)
    {
        _commands.Add(command);
    }

    public void Undo()
    {
        if (_commands.Count == 0) return;

        _commands.RemoveAt(_commands.Count - 1);
        if (_commands.Count == 0) return;
        _commands.Last().Execute();
    }
}

// コンクリート コマンド 背景色変更
public class ChangeBgColorCommand : ICommand
{
    private readonly IColorable _colorable;
    private readonly byte _r, _g, _b;

    public ChangeBgColorCommand(IColorable colorable, byte r, byte g, byte b)
    {
        _colorable = colorable;
        _r = r;
        _g = g;
        _b = b;
    }

    public void Execute()
    {
        _colorable.SetColor(_r, _g, _b);
    }
}

// インボーカー
public class ColorManager
{
    private readonly MacroCommand _history;

    public ColorManager()
    {
        _history = new MacroCommand();
    }

    public void Invoke(ICommand cmd)
    {
        _history.AddCommand(cmd);
        cmd.Execute();
    }

    public void Undo()
    {
        _history.Undo();
    }

    public void Replay()
    {
        _history.Execute();
    }
}

// クライアント / エントリーポイント
internal class Program
{
    private static void Main()
    {
        Console.WriteLine("=== BG Color (6-digit hex only) ===");
        Console.WriteLine("  例)  ff8800   0044ff   00ff00");
        Console.WriteLine("  quit で終了\n");

        IColorable receiver = new ConsoleColorReceiver();
        ColorManager manager = new ColorManager();

        while (true)
        {
            Console.Write("> ");
            string? str = Console.ReadLine();
            str = str?.Trim();
            if (string.IsNullOrEmpty(str))
            {
                continue;
            }
            else if (str == "replay")
            {
                manager.Replay();
            }
            else if (str == "quit")
            {
                break;
            }
            // 桁数だけチェックする
            else if (str.Length == 6)
            {
                // 文字列をバイト変換
                byte r = Convert.ToByte(str.Substring(0, 2), 16);
                byte g = Convert.ToByte(str.Substring(2, 2), 16);
                byte b = Convert.ToByte(str.Substring(4, 2), 16);

                ICommand cmd = new ChangeBgColorCommand(receiver, r, g, b);
                manager.Invoke(cmd);
            }

        }

        // ANSI リセットして終了
        Console.Write("\u001b[0m");
    }
}
