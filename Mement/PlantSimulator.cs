namespace PlantSimulator
{

// オリジネイター役

public class Plant
{
    private string name;
    private int growth; // 成長度
    private int health; // 体力
    private int growthMinTemp; // 成長下限温度
    private int growthMaxTemp; // 成長上限温度
    private int damageCount; // 連続で至適温度外になった回数
    private int currentTemp; // 現在の温度

    public Plant(string name, int growthMinTemp, int growthMaxTemp)
    {
        this.name = name;
        this.growthMinTemp = growthMinTemp;
        this.growthMaxTemp = growthMaxTemp;
        this.growth = 0;
        this.health = 100;
        this.damageCount = 0;
        this.currentTemp = 0;
    }

    public void Update(int currentTemp)
    {        
        this.currentTemp = currentTemp;
        // 成長判定
        if (currentTemp >= growthMinTemp && currentTemp <= growthMaxTemp)
        {
            growth += 1;
            damageCount = 0; // 至適温度の範囲内に入ったらカウントをリセット
        }
        else
        {
            damageCount++;
            // 体力減少判定（気温が良くない時が3回連続以上）
            if (damageCount >= 3)
            {
                int penalty = Math.Min(Math.Abs(currentTemp - growthMinTemp), Math.Abs(currentTemp - growthMaxTemp));
                health -= penalty;
            }
        }

        if (health <= 0)
        {
            Console.WriteLine($"{name}は枯れてしまった");
        }
    }

    public Memento CreateMemento()
    {
        return new Memento(name, growth, health, damageCount);
    }

    public void RestoreMemento(Memento memento)
    {
        this.name = memento.Name;
        this.growth = memento.Growth;
        this.health = memento.Health;
        this.damageCount = memento.DamageCount;
    }

    public bool IsGameOver()
    {
        if (health <= 0)
        {
            Console.WriteLine($"{name}の体力が0になりました。");
            return true;
        }
        if (growth >= 100)
        {
            Console.WriteLine($"{name}の成長度が100に達しました！");
            return true;
        }
        return false;
    }

    public void ShowStatus()
    {
        Console.WriteLine($"[{name}] 成長度: {growth} / 体力: {health} / 温度: {currentTemp}");
    }

    public int GetHealth()
    {
        return health;
    }
}

// メメント役

public class Memento
{
    // wide interface
    // 
    internal string Name { get; }
    internal int Growth { get; }
    internal int Health { get; }
    internal int DamageCount { get; }
    internal Memento(string name, int growth, int health, int damageCount)
    {
        this.Name = name;
        this.Growth = growth;
        this.Health = health;
        this.DamageCount = damageCount;
    }

    // narrow interface
    public string GetName()
    {
        return Name;
    }
}
}       