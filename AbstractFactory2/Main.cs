using AbstractFactory;
using KakutouControllerFactory;
using MarioControllerFactory;
using System;
public class Program{
     public static void Main(string[] args)
    {

        if (args.Length != 1)
        {
            Console.WriteLine("Usage: dotnet run [Example]");
            Console.WriteLine("Example 1: KakutouControllerFactory.KakutouController");
            Console.WriteLine("Example 2: MarioControllerFactory.MarioController");
            return;
        }
        Factory factory = Factory.GetFactory(args[0]);

        Button primaryButton = factory.CreatePrimaryButton();
        Button secondaryButton = factory.CreateSecondaryButton();
        Button upButton = factory.CreateUpButton();
        Button downButton = factory.CreateDownButton();
        Button leftButton = factory.CreateLeftButton();
        Button rightButton = factory.CreateRightButton();

        // 説明の出力
        Console.WriteLine("ボタンの説明");
        primaryButton.GetDescription();
        primaryButton.GetRole();
        secondaryButton.GetDescription();
        secondaryButton.GetRole();
        upButton.GetDescription();
        upButton.GetRole();
        downButton.GetDescription();
        downButton.GetRole();
        leftButton.GetDescription();
        leftButton.GetRole();
        rightButton.GetDescription();
        rightButton.GetRole();

        Dictionary<ConsoleKey, Action> keyActions = new Dictionary<ConsoleKey, Action>()
        {
            { ConsoleKey.A, () => primaryButton.Execute() },
            { ConsoleKey.B, () => secondaryButton.Execute() },
            { ConsoleKey.UpArrow, () => upButton.Execute() },
            { ConsoleKey.DownArrow, () => downButton.Execute() },
            { ConsoleKey.LeftArrow, () => leftButton.Execute() },
            { ConsoleKey.RightArrow, () => rightButton.Execute() }
        };
        
        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyActions.TryGetValue(keyInfo.Key, out Action action))
                    {
                        action.Invoke();
                    }
            }
        }
    }
}