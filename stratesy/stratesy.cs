using System;

// ストラテジー役
interface Strategy
{
    int[,] CreateMask(int rows, int cols);
}

//以下コンクリートストラテジー役
class OneOnePatternStrategy : Strategy
{
    public int[,] CreateMask(int rows, int cols)
    {
        int[,] mask = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                mask[i, j] = (i + j) % 2;
            }
        }
        return mask;
    }
}

class TripleTriplePatternStrategy : Strategy
{
    public int[,] CreateMask(int rows, int cols)
    {
        int[,] mask = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                mask[i, j] = ((i / 3) % 2) ^ ((j / 3) % 2);
            }
        }
        return mask;
    }
}

class FiveFivePatternStrategy : Strategy
{
    public int[,] CreateMask(int rows, int cols)
    {
        int[,] mask = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                mask[i, j] = ((i / 5) % 2) ^ ((j / 5) % 2);
            }
        }
        return mask;
    }
}

class TatejimaPatternStrategy : Strategy
{
    public int[,] CreateMask(int rows, int cols)
    {
        int[,] mask = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                mask[i, j] = (j / 3) % 2;
            }
        }
        return mask;
    }
}
class YokojimaPatternStrategy : Strategy
{
    public int[,] CreateMask(int rows, int cols)
    {
        int[,] mask = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                mask[i, j] = (i / 3) % 2;
            }
        }
        return mask;
    }
}

/////↑↑↑コンクリートストラテジー役//////////////


//コンテキスト役
class PatternMaker
{
    private readonly int[,] grid;
    private Strategy strategy;

    // サイズを指定して初期化
    public PatternMaker(int size, Strategy strategy)
    {
        grid = new int[size, size];
        this.strategy = strategy;
    }

    public void ChangeStrategy(Strategy strategy)
    {
        this.strategy = strategy;
    }

    // ストラテジーの適用 ストラテジーのグリッドとXOR演算
    public void ApplyStrategy()
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        int[,] mask = strategy.CreateMask(rows, cols);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                grid[i, j] ^= mask[i, j];
            }
        }
    }

    // ストラテジーを適用して変化後のグリッドを表示
    public void PrintGrid()
    {
        ApplyStrategy();
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(grid[i, j] == 1 ? "■" : " ");
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main()
    {
        // Dictionary<ConsoleKey, Action> keyActions = new Dictionary<ConsoleKey, Action>()//ディスパッチテーブル
        // {
        //     { ConsoleKey.A, () => primaryButton.Execute() },
        //     { ConsoleKey.B, () => secondaryButton.Execute() },
        //     { ConsoleKey.UpArrow, () => upButton.Execute() },
        //     { ConsoleKey.DownArrow, () => downButton.Execute() },
        //     { ConsoleKey.LeftArrow, () => leftButton.Execute() },
        //     { ConsoleKey.RightArrow, () => rightButton.Execute() }
        // };
        
        // while (true)
        // {
        //     if (Console.KeyAvailable)
        //     {
        //         ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        //         if (keyActions.TryGetValue(keyInfo.Key, out Action action))
        //             {
        //                 action.Invoke();
        //             }
        //     }
        // }
        int size = 30;
        PatternMaker context = new PatternMaker(size, new OneOnePatternStrategy());

        while (true)
        {
            Console.WriteLine("1-5 を選択してください:");
            Console.WriteLine("1: 交互 2: 3x3 3: 5x5 4: 縦縞 5: 横縞 6: 終了");

            string choice = Console.ReadLine();
            if (choice == "6") break;

            switch (choice)
            {
                case "1":
                    context.ChangeStrategy(new OneOnePatternStrategy());
                    break;
                case "2":
                    context.ChangeStrategy(new TripleTriplePatternStrategy());
                    break;
                case "3":
                    context.ChangeStrategy(new FiveFivePatternStrategy());
                    break;
                case "4":
                    context.ChangeStrategy(new TatejimaPatternStrategy());
                    break;
                case "5":
                    context.ChangeStrategy(new YokojimaPatternStrategy());
                    break;
                default:
                    Console.WriteLine("無効な選択です。");
                    continue;
            }
            context.PrintGrid();
        }
    }
}
