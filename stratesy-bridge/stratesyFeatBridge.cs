using System;

// インプリメンタ役
abstract class MaskImpl
{
    public abstract int[,] RowCreateMask(int rows, int cols);
}

/// コンクリートインプリメンタ役（既存）
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

class RandomMaskImpl : MaskImpl
{
    public override int[,] RowCreateMask(int rows, int cols)
    {
        int[,] mask = new int[rows, cols];
        Random random = new Random(); // ループ外で1度だけ初期化
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                // 0 または 1 をランダムに生成
                mask[i, j] = random.Next(0, 2);
            }
        }
        return mask;
    }
}

/// 追加のマスクパターン実装

// 1. 螺旋状のフレーム（外枠から内側へ、交互のパターン）
class SpiralFrameMaskImpl : MaskImpl
{
    public override int[,] RowCreateMask(int rows, int cols)
    {
        int[,] mask = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int d = Math.Min(Math.Min(i, j), Math.Min(rows - 1 - i, cols - 1 - j));
                mask[i, j] = d % 2;
            }
        }
        return mask;
    }
}

// 2. 三角形パターン（右下側が1）
class TriangleMaskImpl : MaskImpl
{
    public override int[,] RowCreateMask(int rows, int cols)
    {
        int[,] mask = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                mask[i, j] = (i < j) ? 1 : 0;
            }
        }
        return mask;
    }
}

// 3. サイン波パターン（周期的な波状パターン）
class SineWaveMaskImpl : MaskImpl
{
    public override int[,] RowCreateMask(int rows, int cols)
    {
        int[,] mask = new int[rows, cols];
        double frequency = 2 * Math.PI / cols;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                // i, j の組み合わせで波を発生させ、正の部分を1とする
                mask[i, j] = (Math.Sin(j * frequency + i * frequency) > 0) ? 1 : 0;
            }
        }
        return mask;
    }
}

// 4. X字パターン（対角線上のみを1に）
class XPatternMaskImpl : MaskImpl
{
    public override int[,] RowCreateMask(int rows, int cols)
    {
        int[,] mask = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                mask[i, j] = (i == j || i + j == rows - 1) ? 1 : 0;
            }
        }
        return mask;
    }
}

// 5. 十字パターン（中央の行と列を1に）
class PlusMaskImpl : MaskImpl
{
    public override int[,] RowCreateMask(int rows, int cols)
    {
        int[,] mask = new int[rows, cols];
        int midRow = rows / 2;
        int midCol = cols / 2;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                mask[i, j] = (i == midRow || j == midCol) ? 1 : 0;
            }
        }
        return mask;
    }
}

// 6. 菱形パターン（中央を起点にしたマンハッタン距離による判定）
class DiamondMaskImpl : MaskImpl
{
    public override int[,] RowCreateMask(int rows, int cols)
    {
        int[,] mask = new int[rows, cols];
        int centerRow = rows / 2;
        int centerCol = cols / 2;
        int threshold = Math.Min(rows, cols) / 4;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int distance = Math.Abs(i - centerRow) + Math.Abs(j - centerCol);
                mask[i, j] = (distance <= threshold) ? 1 : 0;
            }
        }
        return mask;
    }
}

// 7. ドットマトリックスパターン（一定間隔ごとに1を配置）
class DotMatrixMaskImpl : MaskImpl
{
    public override int[,] RowCreateMask(int rows, int cols)
    {
        int[,] mask = new int[rows, cols];
        int dotSpacing = 4; // 4セルごとにドットを配置
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                mask[i, j] = (i % dotSpacing == 0 && j % dotSpacing == 0) ? 1 : 0;
            }
        }
        return mask;
    }
}

// 8. 逆交互パターン（OneOne の反転版）
class ReverseOneOneMaskImpl : MaskImpl
{
    public override int[,] RowCreateMask(int rows, int cols)
    {
        int[,] mask = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                mask[i, j] = 1 - ((i + j) % 2);
            }
        }
        return mask;
    }
}

// 9. 水平グラデーションパターン（上半分と下半分で値を分ける）
class HorizontalGradientMaskImpl : MaskImpl
{
    public override int[,] RowCreateMask(int rows, int cols)
    {
        int[,] mask = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            int value = (i < rows / 2) ? 0 : 1;
            for (int j = 0; j < cols; j++)
            {
                mask[i, j] = value;
            }
        }
        return mask;
    }
}

// 10. 垂直グラデーションパターン（左半分と右半分で値を分ける）
class VerticalGradientMaskImpl : MaskImpl
{
    public override int[,] RowCreateMask(int rows, int cols)
    {
        int[,] mask = new int[rows, cols];
        for (int j = 0; j < cols; j++)
        {
            int value = (j < cols / 2) ? 0 : 1;
            for (int i = 0; i < rows; i++)
            {
                mask[i, j] = value;
            }
        }
        return mask;
    }
}

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

    public void ChangeMaskImpl(MaskImpl maskImpl)
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

/// AltPattern ではシンメトリー表示
class AltPattern : Pattern
{
    public AltPattern(int size, MaskImpl maskImpl) : base(size, maskImpl) { }

    public void PrintGrid2()
    {
        ApplyStrategy();
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        int halfRows = (rows + 1) / 2;
        int halfCols = (cols + 1) / 2;

        for (int i = 0; i < rows; i++)
        {
            int sourceRow = (i < halfRows) ? i : (rows - 1 - i);
            for (int j = 0; j < cols; j++)
            {
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
            Console.WriteLine("マスクパターンを選択してください:");
            Console.WriteLine("1: 交互 (OneOne)");
            Console.WriteLine("2: 3x3 (TripleTriple)");
            Console.WriteLine("3: 5x5 (FiveFive)");
            Console.WriteLine("4: 縦縞 (Tatejima)");
            Console.WriteLine("5: ランダム (Random)");
            Console.WriteLine("6: 螺旋フレーム (SpiralFrame)");
            Console.WriteLine("7: 三角形 (Triangle)");
            Console.WriteLine("8: サイン波 (SineWave)");
            Console.WriteLine("9: X字 (XPattern)");
            Console.WriteLine("10: 十字 (Plus)");
            Console.WriteLine("11: 菱形 (Diamond)");
            Console.WriteLine("12: ドットマトリックス (DotMatrix)");
            Console.WriteLine("13: 逆交互 (ReverseOneOne)");
            Console.WriteLine("14: 水平グラデーション (HorizontalGradient)");
            Console.WriteLine("15: 垂直グラデーション (VerticalGradient)");
            Console.WriteLine("exit: 終了");

            string choice = Console.ReadLine();
            if (choice == "exit") break;

            switch (choice)
            {
                case "1":
                    pattern.ChangeMaskImpl(new OneOneMaskImpl());
                    break;
                case "2":
                    pattern.ChangeMaskImpl(new TripleTripleMaskImpl());
                    break;
                case "3":
                    pattern.ChangeMaskImpl(new FiveFiveMaskImpl());
                    break;
                case "4":
                    pattern.ChangeMaskImpl(new TatejimaMaskImpl());
                    break;
                case "5":
                    pattern.ChangeMaskImpl(new RandomMaskImpl());
                    break;
                case "6":
                    pattern.ChangeMaskImpl(new SpiralFrameMaskImpl());
                    break;
                case "7":
                    pattern.ChangeMaskImpl(new TriangleMaskImpl());
                    break;
                case "8":
                    pattern.ChangeMaskImpl(new SineWaveMaskImpl());
                    break;
                case "9":
                    pattern.ChangeMaskImpl(new XPatternMaskImpl());
                    break;
                case "10":
                    pattern.ChangeMaskImpl(new PlusMaskImpl());
                    break;
                case "11":
                    pattern.ChangeMaskImpl(new DiamondMaskImpl());
                    break;
                case "12":
                    pattern.ChangeMaskImpl(new DotMatrixMaskImpl());
                    break;
                case "13":
                    pattern.ChangeMaskImpl(new ReverseOneOneMaskImpl());
                    break;
                case "14":
                    pattern.ChangeMaskImpl(new HorizontalGradientMaskImpl());
                    break;
                case "15":
                    pattern.ChangeMaskImpl(new VerticalGradientMaskImpl());
                    break;
                default:
                    Console.WriteLine("無効な選択です。");
                    continue;
            }
            pattern.PrintGrid2();
        }
    }
}
