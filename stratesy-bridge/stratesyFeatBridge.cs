using System;


// インプリメンタ役
abstract class MaskImpl
{
    public abstract int[,] RowCreateMask(int rows, int cols);
}

///コンクリートインプリメンタ役
class OneOneMaskImpl : MaskImpl
{
    public override int[,] RowCreateMask(int rows, int cols)
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

class TripleTripleMaskImpl : MaskImpl
{
    public override int[,] RowCreateMask(int rows, int cols)
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

class FiveFiveMaskImpl : MaskImpl
{
    public override int[,] RowCreateMask(int rows, int cols)
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

class TatejimaMaskImpl : MaskImpl
{
    public override int[,] RowCreateMask(int rows, int cols)
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

class YokojimaMaskImpl : MaskImpl
{
    public override int[,] RowCreateMask(int rows, int cols)
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
/////////////////////////////////////////////////////////////////
///
/// アブストラクション役
class Pattern
{
    protected int[,] grid;
    private MaskImpl maskImpl;

    public Pattern(int size, MaskImpl maskImpl)
    {
        grid = new int[size, size];
        this.maskImpl = maskImpl;
    }

    public void SetMaskImpl(MaskImpl maskImpl)
    {
        this.maskImpl = maskImpl;
    }

    public int[,] CreateMask(int rows, int cols)
    {
        return maskImpl.RowCreateMask(rows, cols);
    }

    public void ApplyStrategy()
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        int[,] mask = CreateMask(rows, cols);
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

/// 
class AltPattern : Pattern
{
    public AltPattern(int size, MaskImpl maskImpl) : base(size, maskImpl) { }

    public void PrintGrid2()
    {
        // ストラテジーを適用してグリッドを更新
        ApplyStrategy();
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        int halfRows = (rows + 1) / 2;
        int halfCols = (cols + 1) / 2;

        // 各行について表示
        for (int i = 0; i < rows; i++)
        {
            // 上半分（もしくは中央行まで）はそのまま、下半分は上側と対称に表示
            int sourceRow = (i < halfRows) ? i : (rows - 1 - i);
            
            for (int j = 0; j < cols; j++)
            {
                // 左側（もしくは中央列まで）はそのまま、右側は左側と対称に表示
                int sourceCol = (j < halfCols) ? j : (cols - 1 - j);
                
                Console.Write(grid[sourceRow, sourceCol] == 1 ? "■" : " ");
            }
            Console.WriteLine();
        }
    }

}

class Program
{
    static void Main()
    {
        int size = 30;
        AltPattern pattern = new AltPattern(size, new OneOneMaskImpl());

        while (true)
        {
            Console.WriteLine("1-5 を選択してください:");
            Console.WriteLine("1: 交互 2: 3x3 3: 5x5 4: 縦縞 5: 横縞 6: 終了");

            string choice = Console.ReadLine();
            if (choice == "6") break;

            switch (choice)
            {
                case "1":
                    pattern.SetMaskImpl(new OneOneMaskImpl());
                    break;
                case "2":
                    pattern.SetMaskImpl(new TripleTripleMaskImpl());
                    break;
                case "3":
                    pattern.SetMaskImpl(new FiveFiveMaskImpl());
                    break;
                case "4":
                    pattern.SetMaskImpl(new TatejimaMaskImpl());
                    break;
                case "5":
                    pattern.SetMaskImpl(new YokojimaMaskImpl());
                    break;
                default:
                    Console.WriteLine("無効な選択です。");
                    continue;
            }
            pattern.PrintGrid2();
        }
    }
}