using System;

public abstract class Builder
{
    protected int _temperature;
    protected string _drink = "";
    
    public abstract void SetDrink();
    protected abstract void SetDefaultTemperature();
    public abstract void AdjustTemperature(string level);
    public void Heat()
    {
        _drink += $"({_temperature}℃)";
    }
    public abstract object Extract();
    public void PrintDrink()
    {
        Console.WriteLine(_drink);
    }
}

public class CoffeeBuilder : Builder
{
    public override void SetDrink()
    {
        _drink += "コーヒー";
        SetDefaultTemperature();
    }
    
    protected override void SetDefaultTemperature()
    {
        _temperature = 90;
    }

    public override void AdjustTemperature(string level)
    {
        if (level == "熱め")
            _temperature += 5;
        else if (level == "ぬるめ")
            _temperature -= 10;
    }

    public override Builder Extract()
    {
        _drink += " 抽出完了！";
        return this;
    }
}

public class TeaBuilder : Builder
{
    public override void SetDrink()
    {
        _drink = "お茶";
        SetDefaultTemperature();
    }
    
    protected override void SetDefaultTemperature()
    {
        _temperature = 65;
    }

    public override void AdjustTemperature(string level)
    {
        if (level == "熱め")
            _temperature += 10;
        else if (level == "ぬるめ")
            _temperature -= 10;
    }

    public override Builder Extract()
    {
        _drink += " 抽出完了！";
        return this;
    }
}

public class MilkBuilder : Builder
{
    public override void SetDrink()
    {
        _drink = "ミルク";
        SetDefaultTemperature();
    }
    
    protected override void SetDefaultTemperature()
    {
        _temperature = 40;
    }

    public override void AdjustTemperature(string level)
    {
        if (level == "熱め")
            _temperature += 5;
        else if (level == "ぬるめ")
            _temperature -= 5;
    }

    public override Builder Extract()
    {
        _drink += " 抽出完了！";
        return this;
    }
}

public class DrinkDirector
{
    private Builder _builder;

    public DrinkDirector(Builder builder)
    {
        _builder = builder;
    }

    public void Construct(string heatLevel)
    {
        _builder.SetDrink();
        _builder.AdjustTemperature(heatLevel);
        _builder.Heat();
        _builder.Extract();
    }
}

public class Program 
{
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("選択してください: 1. コーヒー 2. お茶 3. ミルク");
            string? input = Console.ReadLine();
            Builder? builder = null;

            switch (input)
            {
                case "1":
                    builder = new CoffeeBuilder();
                    break;
                case "2":
                    builder = new TeaBuilder();
                    break;
                case "3":
                    builder = new MilkBuilder();
                    break;
                default:
                    Console.WriteLine("終了");
                    return;
            }

            DrinkDirector director = new DrinkDirector(builder);
            Console.WriteLine("温度レベルを選択してください: 1 熱め / 2 ぬるめ / 3 普通");
            string? heatLevel = Console.ReadLine();
            switch (heatLevel)
            {
                case "1":
                    director.Construct("熱め");
                    break;
                case "2":
                    director.Construct("ぬるめ");
                    break;
                case "3":
                    director.Construct("普通");
                    break;
                default:
                    Console.WriteLine("終了");
                    return;
            }
            Console.WriteLine("...");
            builder.PrintDrink();
            Console.WriteLine();
        }
    }
}
