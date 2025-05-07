using System;

// オブザーバー（インターフェース）
public interface IObserver
{
    void Update(Subject subject);
}

// サブジェクト（抽象クラス）
public abstract class Subject
{
    private List<IObserver> observers = new List<IObserver>();
    private bool isExecuted = false;

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }
    public void DeleteObserver(IObserver observer)
    {
        observers.Remove(observer);
    }
    public void NotifyObservers()
    {
        if (isExecuted)
        {
            // Console.WriteLine("すでに実行しています");
            isExecuted = false;
            return;
        }
        isExecuted = true;
        foreach (IObserver observer in observers)
        {
            observer.Update(this);
        }
        isExecuted = false;
    }
    public abstract string GetFileName();
    public abstract string GetFileText();
    public abstract string GetAction();
    public abstract void Execute();
}

// コンクリートサブジェクト
public class FileSaveSubject : Subject
{
    private string fileName;
    private string action = "保存";
    public FileSaveSubject(string fileName)
    {
        this.fileName = fileName;
    }
    public override string GetFileName()
    {
        return fileName;
    }
    public override string GetFileText()
    {
        try
        {
            return File.ReadAllText(fileName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ファイルの読み込みに失敗しました: {ex.Message}");
            return ""; // 失敗時はデフォルトのテキストを返す
        }
    }
    public override string GetAction()
    {
        return action;
    }
    public override void Execute()
    {
        NotifyObservers();
    }
}

public class FileDeleteSubject : Subject
{
    private string fileName;
    private string action = "削除";
    public FileDeleteSubject(string fileName)
    {
        this.fileName = fileName;
    }
    public override string GetFileName()
    {
        return fileName;
    }
    public override void Execute()
    {
        NotifyObservers();
    }
    public override string GetAction()
    {
        return action;
    }
    public override string GetFileText()
    {
        return "deleted";
    }
}
// コンクリートオブザーバー
// ログ出力オブザーバー
public class LoggerObserver : IObserver
{
    // private bool isExecuted = false;
    public void Update(Subject subject)
    {
        // if (isExecuted)
        // {
        //     Console.WriteLine("すでにログ出力しています");
        //     return;
        // }
        // isExecuted = true;
        Console.WriteLine($"{subject.GetFileName()} を {subject.GetAction()} しました");
        Console.WriteLine("--テキストの内容↓↓---");
        Console.WriteLine($"{subject.GetFileText()}");
        Console.WriteLine("--テキストの内容↑↑---");
        subject.Execute(); // 無限ループの再現
    }
}

// メール送信オブザーバー
public class EmailObserver : IObserver
{
    public void Update(Subject subject)
    {
        Console.WriteLine($"{subject.GetFileName()} を {subject.GetAction()} しました メール送信");
    }
}

// Dropbox連携オブザーバー
public class DropboxObserver : IObserver
{
    public void Update(Subject subject)
    {
        Console.WriteLine($"{subject.GetFileName()} を {subject.GetAction()} しました Dropbox");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // ファイル保存時サブジェクト
        Subject subject1 = new FileSaveSubject("日記.txt");
        subject1.AddObserver(new LoggerObserver());
        subject1.AddObserver(new EmailObserver());
        subject1.AddObserver(new DropboxObserver());
        subject1.Execute();

        Console.WriteLine("");
        Console.WriteLine("--ファイル削除サブジェクト--");
        Console.WriteLine("");
        // ファイル削除サブジェクト     
        Subject subject2 = new FileDeleteSubject("日記.txt");
        subject2.AddObserver(new LoggerObserver());
        subject2.AddObserver(new DropboxObserver());
        subject2.Execute();

        Console.WriteLine("");
        Console.WriteLine("--ファイル保存サブジェクト--");
        Console.WriteLine("");
        subject1.Execute(); // 再度実行
    }
}