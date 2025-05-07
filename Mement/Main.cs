using PlantSimulator;
// ケアテイカー役（メイン）
class Program
{
    static void Main(string[] args)
    {
        Plant plant = new Plant("バナナ", 19, 22); // 19~22度で成長するバナナ
        plant.ShowStatus();

        Random random = new Random();
        int baseTemp = 20; // 基準温度
        Memento memento = null;
        int updateCount = 0;
        int tempHealth = 100;

        // 0.1秒ごとに成長していく（update）
        while (true)
        {
            // 基準温度から±5度の範囲でランダムな温度を生成
            int currentTemp = baseTemp + random.Next(-5, 6);
            plant.Update(currentTemp);
            plant.ShowStatus();
            
            // 体力が50以下になったら状態を保存 保存はこの一回のみ
            if (plant.GetHealth() <= 50 && tempHealth > 50)
            {
                memento = plant.CreateMemento();
                Console.WriteLine("状態を保存しました。");
                tempHealth = plant.GetHealth();
            }
            
            if (plant.IsGameOver())
            {
                Console.WriteLine("1. 体力50から再開");
                Console.WriteLine("2. 終了");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("無効な入力です。終了します。");
                    break;
                }
                
                int choice = int.Parse(input);
                if (choice == 1)
                {
                    if (memento != null)
                    {
                        plant.RestoreMemento(memento);
                        Console.WriteLine($"{memento.GetName()}の状態を復元しました。");
                    }
                    else
                    {
                        Console.WriteLine("保存された状態がありません。終了します。");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("終了します。");
                    break;
                }
            }
            
            Thread.Sleep(50);
        }
    }
}       