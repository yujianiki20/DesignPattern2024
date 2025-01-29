using System;
using System.Collections.Generic;

namespace SingletonPattern
{
    // singleton
    public class JankenLogger
    {
        private static readonly JankenLogger _instance = new JankenLogger();
        private List<string> _playerChoice;
        private List<string> _cpuChoice;
        private List<string> _result;

        private JankenLogger()
        {
            _playerChoice = new List<string>();
            _cpuChoice = new List<string>();
            _result = new List<string>();
        }

        public static JankenLogger GetInstance
        {
            get
            {
                return _instance;
            }
        }

        public void Record(string playerChoice, string cpuChoice, string result)
        {
            _playerChoice.Add(playerChoice);
            _cpuChoice.Add(cpuChoice);
            _result.Add(result);
        }

        public void ShowLog()
        {
            Console.WriteLine("---");
            for (int i = 0; i < _playerChoice.Count; i++)
            {
                Console.WriteLine($"ゲーム {i + 1}: あなた[{_playerChoice[i]}] vs 相手[{_cpuChoice[i]}] => { _result[i]}");
            }
        }

    }

    public class JankenGame
    {
        private static readonly string[] HANDS = { "グー", "チョキ", "パー" };
        private Random _random;

        public JankenGame()
        {
            _random = new Random();
        }

        public int Play()
        {
            Console.WriteLine("数字で手を選択 1: グー, 2: チョキ, 3: パー");
            string? input = Console.ReadLine();
            // 1-3以外の場合は0を返す
            if (!int.TryParse(input, out int playerChoice) || playerChoice < 1 || playerChoice > 3)
            {
                return 0;
            }

            int cpuChoice = _random.Next(1, 4);

            string playerHand = HANDS[playerChoice - 1];
            string cpuHand = HANDS[cpuChoice - 1];

            Console.WriteLine($"あなた: {playerHand}");
            Console.WriteLine($"相手: {cpuHand}");

            string result;
            if (playerChoice == cpuChoice)
            {
                result = "draw";
            }
            else if ((playerChoice == 1 && cpuChoice == 2) ||
                     (playerChoice == 2 && cpuChoice == 3) ||
                     (playerChoice == 3 && cpuChoice == 1))
            {
                result = "win!";
            }
            else
            {
                result = "lose...";
            }
            Console.WriteLine(result);
            // ロガーに記録
            JankenLogger.GetInstance.Record(playerHand, cpuHand, result);
            return playerChoice;
        }
    }

    // main
    class Program
    {
        static void Main()
        {
            int res = -1;
            JankenGame game = new JankenGame();
            while (res != 0)
            {
                res = game.Play();
                Console.WriteLine("------");
            }
            JankenLogger.GetInstance.ShowLog();
        }
    }
}
