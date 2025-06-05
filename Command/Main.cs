using System;
using System.Collections.Generic;
using System.Threading;

// コマンド インターフェイス
public interface ICommand
{
    void Execute();
    void Undo();
    ICommand? PrevCommand { get; set; } 
}

// レシーバー インターフェイス
public interface IColorable
{
    void SetColor(byte r, byte g, byte b);
    void GetColor(out byte r, out byte g, out byte b);
}

public class ConsoleColorReceiver : IColorable
{
    private byte _r, _g, _b; // 現在の色を保持

    public void SetColor(byte r, byte g, byte b)
    {
        _r = r;
        _g = g;
        _b = b;
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

    public void GetColor(out byte r, out byte g, out byte b) // 現在の色を取得 （Undo用）
    {
        r = _r;
        g = _g;
        b = _b;
    }
}

// コンクリート コマンド 背景色変更
public class ChangeBgColorCommand : ICommand
{
    private readonly IColorable _colorable;
    private readonly byte _r, _g, _b;
    private  byte _prevR, _prevG, _prevB; // 直前の色
    private bool _getPrev = false; // 直前の色を一回だけ取得するフラグ
    public ICommand? PrevCommand { get; set; }

    public ChangeBgColorCommand(IColorable colorable, byte r, byte g, byte b)
    {
        _colorable = colorable;
        _r = r;
        _g = g;
        _b = b;
        PrevCommand = null;
    }


    public void Execute()
    {
        if (!_getPrev)
        {
            _colorable.GetColor(out _prevR, out _prevG, out _prevB);
            _getPrev = true;
        }
        _colorable.SetColor(_r, _g, _b);
    }

    public void Undo()
    {
        _colorable.SetColor(_prevR, _prevG, _prevB);
    }
}

// インボーカー
public class ColorManager
{
    private ICommand? _currentCommand; // 直近のコマンドを保持しておく
    public void Invoke(ICommand cmd)
    {
        cmd.PrevCommand = _currentCommand;
        cmd.Execute();
        _currentCommand = cmd;
    }

    public void Undo()
    {
        _currentCommand?.Undo();
        _currentCommand = _currentCommand?.PrevCommand;
    }
}

// クライアント / エントリーポイント
internal class Program
{
    private static void Main()
    {
        Console.WriteLine("=== カラーコードを入力 ===");
        Console.WriteLine("  例)  ff8800   0044ff   00ff00");
        Console.WriteLine("  uでundo q で終了\n");

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
            else if (str == "u")
            {
                manager.Undo();
            }
            else if (str == "q")
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
