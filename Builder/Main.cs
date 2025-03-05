using System;

public abstract class Builder
{
    protected int _temperature;
    protected string _drink = "";
    
    public abstract Dictionary<int, string> GetOptions();
    public abstract void SetDrink();
    protected abstract void SetDefaultTemperature();
    public abstract void AdjustTemperature(string level);
    public void Heat()
    {
        _drink += $"({_temperature}℃)";
    }
    public abstract string Extract();
}

public class CoffeeBuilder : Builder
{
    public override void SetDrink()
    {
        _drink += "コーヒー";
        SetDefaultTemperature();
    }

    public override Dictionary<int, string> GetOptions()
    {
        return new Dictionary<int, string>
        {
            {1, "ホット"},
            {2, "アイス"}
        };
    }
    
    protected override void SetDefaultTemperature()
    {
        _temperature = 90;
    }

    public override void AdjustTemperature(string level)
    {
        _drink += level;
        if (level == "ホット")
            _temperature += 5;
        else if (level == "アイス")
            _temperature -= 85;
    }

    public override string Extract()
    {
        _drink += " 抽出完了！";
        return _drink;
    }
}

public class TeaBuilder : Builder
{
    public override void SetDrink()
    {
        _drink = "お茶";
        SetDefaultTemperature();
    }
    public override Dictionary<int, string> GetOptions()
    {
        return new Dictionary<int, string>
        {
            {1, "熱め"},
            {2, "ぬるめ"},
            {3, "普通"}
        };
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

    public override string Extract()
    {
        _drink += " 抽出完了！";
        return _drink;
    }
}

public class MilkBuilder : Builder
{
    public override void SetDrink()
    {
        _drink = "ミルク";
        SetDefaultTemperature();
    }
    public override Dictionary<int, string> GetOptions()
    {
        return new Dictionary<int, string>
        {
            {1, "熱め"},
            {2, "ぬるめ"},
            {3, "普通"}
        };
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

    public override string Extract()
    {
        _drink += " 抽出完了！";
        return _drink;
    }
}

public class CocaColaBuilder : Builder
{
    public override void SetDrink()
    {
        _drink = "コカコーラ";
        SetDefaultTemperature();
    }
    public override Dictionary<int, string> GetOptions()
    {
        return new Dictionary<int, string>
        {
            {1, "氷あり"},
            {2, "氷なし"},
            {3, "ホット"}
        };
    }

    protected override void SetDefaultTemperature()
    {
        _temperature = 10;
    }

    public override void AdjustTemperature(string level)
    {
        _drink += level;
        if (level == "氷あり")
            _temperature -= 5;
        else if (level == "氷なし")
            _drink += " 氷なし";
        else if (level == "ホット")
            _temperature += 70;
    }

    public override string Extract()
    {
        _drink += " カップにはいりました！";
        return _drink;
    }
}

public class DrinkDirector
{
    private Builder _builder;

    public DrinkDirector(Builder builder)
    {
        _builder = builder;
    }

    public Dictionary<int, string> GetOptions()
    {
        return _builder.GetOptions();
    }
    public string Construct(string heatLevel)
    {
        _builder.SetDrink();
        _builder.AdjustTemperature(heatLevel);
        _builder.Heat();
        return _builder.Extract();
    }
}


public class Program 
{
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("選択してください: 1. コーヒー 2. お茶 3. ミルク 4. コカコーラ");
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
                case "4":
                    builder = new CocaColaBuilder();
                    break;
                default:
                    Console.WriteLine("終了");
                    return;
            }

            DrinkDirector director = new DrinkDirector(builder);
            // Console.WriteLine("温度レベルを選択してください: 1 熱め / 2 ぬるめ / 3 普通");
            var options = director.GetOptions();
            Console.WriteLine("オプションを選択してください");
            Console.WriteLine(string.Join(", ", options.Select(o => $"{o.Key}: {o.Value}")));
            string? selectedOption = Console.ReadLine();
            // 選択した値が存在しない場合は終了
            if (string.IsNullOrEmpty(selectedOption))
            {
                Console.WriteLine("終了");
                return;
            }
            
            string drinkCup = director.Construct(options[int.Parse(selectedOption)]);

            Console.WriteLine("...");
            Console.WriteLine(drinkCup);
            Console.WriteLine();
        }
    }
}
