namespace FacadePattern
{
    // 以下その他大勢のクラス

    // 起床時刻を取得するためのクラス
    public class Clock
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }

    // タスクリストを取得するクラス
    public class TaskManager
    {
        private string filename;

        public TaskManager(string filename)
        {
            this.filename = filename;
        }
        public List<string> GetTasks()
        {
            if (File.Exists(filename))
            {
                return new List<string>(File.ReadAllLines(filename));
            }
            else
            {
                return new List<string> { "---" };
            }
        }
    }

    // カレンダーから予定を取得するクラス

    public class CalendarManager
    {
        private string filename;

        public CalendarManager(string filename)
        {
            this.filename = filename;
        }

        public List<string> GetEvents()
        {
            if (File.Exists(filename))
            {
                return new List<string>(File.ReadAllLines(filename));
            }
            else
            {
                return new List<string> { "---" };
            }
        }
    }

    // メールなどでテキストやデータを送信するクラス
    public class Notifier
    {
        public void SendNotification(string message)
        {
            // 通知の代わりにコンソール出力
            Console.WriteLine(message);
        }
    }
    
    // Facade役
    public class Facade
    {
        private Facade() {}
        
        public static void SendDailySummary()
        {
            // 各クラスのインスタンスを作成
            Clock clock = new Clock();
            TaskManager taskManager = new TaskManager("tasks.txt");
            CalendarManager calendarManager = new CalendarManager("calendar.txt");
            Notifier notifier = new Notifier();

            // 現在時刻を取得
            DateTime currentTime = clock.GetCurrentTime();

            // タスクリストと予定を取得
            List<string> tasks = taskManager.GetTasks();
            List<string> events = calendarManager.GetEvents();

            // メッセージを作成
            string message = $"起床時刻: {currentTime}\n" +
                             "\n今日のタスク:\n" + string.Join("\n", tasks) + "\n" +
                             "\n今日の予定:\n" + string.Join("\n", events);

            // 通知を送信
            notifier.SendNotification(message);
        }
    }
}
