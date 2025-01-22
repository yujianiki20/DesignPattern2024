using System;
using Shirokuro;
using Framework;
using Suisai;

public class Program 
{
    public static void Main()
    {
        Console.WriteLine("--白黒化プラグイン--");
        Plugin plugin = new ShirokuroPlugin();
        Image image1 = plugin.Load("(猫の画像)");
        image1.show();

        Console.WriteLine("--水彩画風プラグイン--");

        plugin = new SuisaiPlugin();
        Image image2 = plugin.Load("(旅行の画像)");
        image2.show();

        Console.WriteLine("--水彩画プラグイン--");

        plugin = new SuisaiPlugin();
        Image image3 = plugin.Load("(食べ物の画像)");
        image3.show();
        image3.save();
        image3.send();
    }
}