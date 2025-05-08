using PlantSimulator;
// ケアテイカー役（メイン）
class Program
{
    static void Main(string[] args)
    {
        Plant plant = new Plant("バナナ", 19, 22); // 19~22度で成長するバナナ
        plant.ShowStatus();

        Dictionary<int, Memento> mementos = new Dictionary<int, Memento>(); // mementoを保存する辞書
        Random random = new Random();
        int baseTemp = 20; // 基準温度
        Memento memento = null;
        int currentDay = 0;

        // 0.1秒ごとに成長していく（update）
        while (true)
        {
            // 基準温度から±5度の範囲でランダムな温度を生成
            int currentTemp = baseTemp + random.Next(-5, 6);
            plant.Update(currentTemp);
            plant.ShowStatus();

            // 状態を保存（日付をキーとして使用）
            memento = plant.CreateMemento();
            mementos[memento.GetDay()] = memento;

            if (plant.IsGameOver())
            {
                Console.Write("\n復元したい日付を入力してください: ");
                string? input = Console.ReadLine();
                
                if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int choice) || 
                    !mementos.ContainsKey(choice))
                {
                    Console.WriteLine("無効な入力です。終了します。");
                    break;
                }

                plant.RestoreMemento(mementos[choice]);
                Console.WriteLine($"{choice}日目の状態を復元しました。");
            }
            
            Thread.Sleep(100);
        }
    }
}       