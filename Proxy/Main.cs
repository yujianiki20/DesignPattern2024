using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Runtime.CompilerServices;
// サブジェクト役（インターフェース）
public interface IBookReader
{
    string GetBookInfo(string fileName);
    string GetText(string fileName);
}

// リアルサブジェクト
// 疑似インターネット
public class RemoteBookReader : IBookReader
{
    // サーバー側のミニデータベース
    private readonly Dictionary<string, string> _db = new()
    {
        ["桃太郎.txt"] = "昔々、あるところにお祖父さんとお婆さんがいました。おじいさんは山へ芝刈りに行きました。おばあさんは川へ洗濯に行きました。",
        ["新聞.txt"] = "5/27 晴れ。万博で各国のパビリオンが話題です。",
        ["算数.txt"] = "1+2=3 2+2=4 3+3=6 4+4=8 5+5=10 6+6=12 7+7=14 8+8=16 9+9=18 10+10=20"
    };
    
    private readonly Dictionary<string, string> _dbInfo = new()
    {
        ["桃太郎.txt"] = "作者不詳 ジャンル：昔話 1200円",
        ["新聞.txt"] = "読売新聞社 ジャンル：新聞 300円",
        ["算数.txt"] = "講談社 人気のある算数教科書 ジャンル：教科書 1500円"
    };

    public string GetBookInfo(string fileName)
    {
        if (_dbInfo.TryGetValue(fileName, out var text))
        {
            return text;
        }
        return $"{fileName} の情報はありません。";
    }

    public RemoteBookReader()
    {
        Console.WriteLine("[Remote] インターネットに接続します");
        Thread.Sleep(2000);
    }

    public string GetText(string fileName)
    {
        Console.WriteLine($"[Remote] ダウンロード中… ({fileName})");
        Thread.Sleep(1200);
        if (_db.TryGetValue(fileName, out var text))
        {
            return text;
        }
        return $"404: {fileName}";
    }
}

// プロキシ（オフライン → リモート） ─────
public class BookReaderProxy : IBookReader
{
    private RemoteBookReader? real;

    private readonly Dictionary<string, string> _dbInfo = new()
    {
        ["桃太郎.txt"]  = "作者不詳 ジャンル：昔話 1200円",
        ["新聞.txt"]   = "読売新聞社 ジャンル：新聞 300円",
        ["算数.txt"] = "講談社 人気のある算数教科書 ジャンル：教科書 1500円"
    };

    public string GetBookInfo(string fileName)
    {
        if (_dbInfo.TryGetValue(fileName, out var text))
        {
            return text;
        }
        return $"{fileName} の情報はありません。";
    }
    public string GetText(string fileName)
    {
        if (File.Exists(fileName))
        {
            Console.WriteLine($"[Proxy] オフラインから読み込み: {fileName}");
            return File.ReadAllText(fileName);
        }

        Console.WriteLine($"[Proxy] オフラインにないためインターネットから取得します: {fileName}");
        Realize(fileName);
        string text = real!.GetText(fileName);

        // キャッシュ保存（404 は保存しない）
        if (!text.StartsWith("404"))
        {
            File.WriteAllText(fileName, text);
            Console.WriteLine($"[Proxy] 保存完了: {fileName}");
        }

        return text;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void Realize(string fileName)
    {
        if (real == null)
        {
            real = new RemoteBookReader();
        }
    }
}

// 動作確認 ──────────────────────────
class Program
{
    static void Main()
    {
        IBookReader bookReader = new BookReaderProxy();

        Console.WriteLine("=== book info (桃太郎.txt) ===");
        Console.WriteLine(bookReader.GetBookInfo("桃太郎.txt"));
        Console.WriteLine("\n=== book info (算数.txt) ===");
        Console.WriteLine(bookReader.GetBookInfo("算数.txt"));
        Console.WriteLine("\n=== book info (新聞.txt) ===");
        Console.WriteLine(bookReader.GetBookInfo("新聞.txt"));

        // どれを読むかユーザーに入力で選択してもらう処理
        Console.WriteLine("どの本を読みますか？");
        Console.Write("タイトルを入力してください:");
        string title = Console.ReadLine()!;
        
        Console.WriteLine($"=== request ({title}) ===");
        Console.WriteLine(bookReader.GetText(title));
        
        // Console.WriteLine("=== request (桃太郎.txt) ===");
        // Console.WriteLine(bookReader.GetText("桃太郎.txt"));

        // Console.WriteLine("\n=== request (算数.txt) ===");
        // Console.WriteLine(bookReader.GetText("算数.txt"));

        // Console.WriteLine("\n=== request (新聞.txt) ===");
        // Console.WriteLine(bookReader.GetText("新聞.txt"));
    }
}
