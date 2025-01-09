using System;
using System.Threading;

namespace AdapterPatternExample
{
    // target
    public interface IDisplayTimer
    {
        int Next();
        string GetFormattedTime();
        
    }

    // adaptee: 
    public class Timer
    {
        private int _countSeconds;
        public Timer(int seconds)
        {
            _countSeconds = seconds;
        }

        public int Count(bool isCountUp)
        {
            if (isCountUp)
            {
                _countSeconds++;
            }
            else
            {
                _countSeconds--;
            }
            return _countSeconds;
        }
        public int GetRemainingSeconds()
        {
            return _countSeconds;
        }
    }

    // adapter
    public class FormatTimer : Timer, IDisplayTimer
    {
        public FormatTimer(int seconds) : base(seconds)
        {
        }

        public int Next()
        {
            return base.Count(false);
        }
        public string GetFormattedTime()
        {
            int totalSeconds = GetRemainingSeconds();
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }
    }

    public class CountUpAdapter : Timer, IDisplayTimer
    {
            private int endTime;
    
            public CountUpAdapter(int seconds) : base(seconds)
            {
                endTime = seconds;
            }
    
            public int Next()
        {
            return base.Count(true);
        }
        public string GetFormattedTime()
        {
            int totalSeconds = GetRemainingSeconds() - endTime;
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }
    }
    // main
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("秒数を入力してください");
            if (int.TryParse(Console.ReadLine(), out int totalSeconds))
            {
                IDisplayTimer timer = new CountUpAdapter(totalSeconds);

                while (timer.Next() > 0)
                {
                    Console.WriteLine(timer.GetFormattedTime());
                    Thread.Sleep(1000); // 1秒待つ
                }
                Console.WriteLine("時間になりました");
            }
        }
    }
}
