using System;
using System.Threading;

namespace AdapterPatternExample
{
    // target
    public interface IDisplayTimer
    {
        int Countdown();
        string GetFormattedTime();
        
    }

    // adaptee: 
    public class Timer
    {
        private int _remainingSeconds;
        public Timer(int seconds)
        {
            _remainingSeconds = seconds;
        }

        public int Reduce()
        {
            if (_remainingSeconds > 0)
            {
                _remainingSeconds--;
            }
            return _remainingSeconds;
        }

        public int GetRemainingSeconds()
        {
            return _remainingSeconds;
        }
    }

    // adapter
    public class FormatTimer : Timer, IDisplayTimer
    {
        public FormatTimer(int seconds) : base(seconds)
        {
        }

        public int Countdown()
        {
            return base.Reduce();
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

    // main
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("秒数を入力してください");
            if (int.TryParse(Console.ReadLine(), out int totalSeconds))
            {
                IDisplayTimer timer = new FormatTimer(totalSeconds);

                while (timer.Countdown() > 0)
                {
                    Console.WriteLine(timer.GetFormattedTime());
                    Thread.Sleep(1000); // 1秒待つ
                }
                Console.WriteLine("時間になりました");
            }
        }
    }
}
