using System;
using System.Collections.Generic;

class Bill
{
    public int TotalPrice { get; }
    public int PeopleCount { get; }

    public Bill(int totalPrice, int peopleCount)
    {
        TotalPrice = totalPrice;
        PeopleCount = peopleCount;
    }

    public double PerPerson => (double)TotalPrice / PeopleCount;
    public int PerPersonInt => TotalPrice / PeopleCount;
    public int Remainder => TotalPrice % PeopleCount;
}

/// ハンドラー役(抽象)
abstract class SplitHandler
{
    protected SplitHandler next;
    private readonly string name;

    public SplitHandler(string name)
    {
        this.name = name;
    }

    public SplitHandler SetNext(SplitHandler next)
    {
        this.next = next;
        return this;
    }

    public void Support(Bill bill)
    {
        if (Resolve(bill))
        {
            Done(bill);
        }
        else if (next != null)
        {
            next.Support(bill);
        }
        else
        {
            Fail(bill);
        }
    }

    protected abstract bool Resolve(Bill bill);

    protected virtual void Done(Bill bill)
    {
        Console.WriteLine($"{bill.TotalPrice}円を{bill.PeopleCount}人 → 解決：{this}");
    }

    protected virtual void Fail(Bill bill)
    {
        Console.WriteLine($"{bill.TotalPrice}円を{bill.PeopleCount}人 → 解決できませんでした。");
    }

    public override string ToString()
    {
        return $"[{name}]";
    }
}

//コンクリートハンドラー
/// 全員ピッタリ割り切れる
class ExactSplit : SplitHandler
{
    public ExactSplit(string name) : base(name) { }

    protected override bool Resolve(Bill bill)
    {
        if (bill.Remainder == 0)
        {
            Console.WriteLine($"全員ぴったり {bill.PerPersonInt}円ずつ払えます。");
            return true;
        }
        return false;
    }
}

/// 1人あたり100円単位でピッタリ割り切れる場合だけ「割り切れた」とみなす
/// 大雑把な割り勘ルール
class RoughSplit : SplitHandler
{
    public RoughSplit(string name) : base(name) { }

    protected override bool Resolve(Bill bill)
    {
        if ()
        {
            Console.WriteLine($"全員ぴったり {bill.PerPersonInt}円ずつ払えます。（100円単位OK）");
            return true;
        }

        return false; // 1円単位なら割り切れてても拒否
    }
}

/// 100円単位で1人が多く払う
class HundredYenSplit : SplitHandler
{
    public HundredYenSplit(string name) : base(name) { }

    protected override bool Resolve(Bill bill)
    {
        if ()
        {
            // 100円誰かが多く払うことで解決するケース
            return true;
        }

        return false;
    }
}

/// 10円単位で1人が多く払う
class TenYenSplit : SplitHandler
{
    public TenYenSplit(string name) : base(name) { }

    protected override bool Resolve(Bill bill)
    {
        if (total >= bill.TotalPrice)
        {
            // 10円誰かが多く払うことで解決するケース
            return true;
        }

        return false;
    }
}

/// 1人が余りを負担する
class YenAdjustmentSplit : SplitHandler
{
    public YenAdjustmentSplit(string name) : base(name) { }

    protected override bool Resolve(Bill bill)
    {
        if (bill.Remainder > 0)
        {
            // 1人が余りを負担することで解決するケース
            return true;
        }
        return false;
    }
}


class Program
{
    static void Main()
    {
        var testCases = new List<Bill>
        {
            new Bill(1000, 4),
            new Bill(1030, 4),
            new Bill(1010, 4),
            new Bill(1003, 4),
            new Bill(1001, 4),
            new Bill(1001, 3),
            new Bill(1011, 3)
        };
        // 大雑把スタイル 100円単位なら誰かが多めに出しても良いグループ
        RunTest(testCases, () =>
            new RoughSplit("Rough")
                .SetNext(new HundredYenSplit("Hundred"))
                .SetNext(new TenYenSplit("Ten"))
                .SetNext(new YenAdjustmentSplit("Adjust"))
        );

        // 平等スタイル できるだけ細かく割り勘する
        RunTest(testCases, () =>
            new ExactSplit("Exact")
                .SetNext(new YenAdjustmentSplit("Adjust"))
                .SetNext(new TenYenSplit("Ten"))
                .SetNext(new HundredYenSplit("Hundred"))
        );
    }

    static void RunTest(List<Bill> bills, Func<SplitHandler> buildChain)
    {
        var handler = buildChain();

        foreach (var bill in bills)
        {
            Console.WriteLine($"--- {bill.TotalPrice}円を{bill.PeopleCount}人で割り勘 ---");
            handler.Support(bill);
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
