using System;
using System.IO;
using System.Collections.Generic;

// Flyweight役
public class StampFlyweight
{
    private readonly string _imageName;
    private readonly byte[] _imageData;

    public StampFlyweight(string imageName)
    {
        _imageName = imageName;
        var path = $"{imageName}.png";
        if (!File.Exists(path))
            throw new FileNotFoundException($"画像が見つかりません: {path}");
            
        _imageData = File.ReadAllBytes(path);
        Console.WriteLine($"{_imageName} を読み込みました");
    }

    // 任意の座標状にFlyweightを描画
    // extrinsic エクストリンジックな情報 
    public void Render(int x, int y)
    {
        Console.WriteLine($"Draw {_imageName} at ({x}, {y})");
        // 本来は具体的に描画するような処理を行う
    }
}

// FlyweightFactory役 
public class StampFactory
{
    private Dictionary<string, StampFlyweight> pool = new();
    private static StampFactory singleton = new(); 
    private StampFactory() { }
    public static StampFactory GetInstance => singleton;

    public StampFlyweight GetStamp(string name)
    {
        lock(this)
        {
            if (pool.TryGetValue(name, out var fw))
                return fw;

            fw = new StampFlyweight(name);
            pool[name] = fw;
            return fw;
        }
    }
}

// クライアント役 家具名と座標だけで配置する
public class FurnitureClient
{
    private readonly StampFactory _factory;

    public FurnitureClient()
    {
        _factory = StampFactory.GetInstance;
    }

    public void Put(string name, int x, int y)
    {
        var furniture = _factory.GetStamp(name);
        furniture.Render(x, y);
    }
}

internal static class Program
{
    private static void Main()
    {
        FurnitureClient client = new FurnitureClient();

        // 家具を配置
        client.Put("椅子", 50, 100);
        client.Put("机", 200, 140);
        client.Put("椅子", 300, 80);  // 2つ目の椅子は共有インスタンス
        client.Put("ソファ", 150, 200);
        client.Put("観葉植物", 400, 150);
        client.Put("観葉植物", 100, 250);  // 2つ目の観葉植物も共有インスタンス
    }
}
