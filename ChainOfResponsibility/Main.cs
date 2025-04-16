using System;
using System.Collections.Generic;

// 請求の内容、これを元に割り勘の条件を決める
class Bill
{
    public int TotalPrice { get; }
    public int PeopleCount { get; }

    public Bill(int totalPrice, int peopleCount)
    {
        TotalPrice = totalPrice;
        PeopleCount = peopleCount;
    }

    // 条件の計算用プロパティ
    public double PerPerson => (double)TotalPrice / PeopleCount;
    public int PerPersonInt => TotalPrice / PeopleCount;
    // 余り
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
        return next;//??? //未解決
    }

    public void Support(Bill bill)
    {
        SplitHandler obj = this;
        while (obj != null)
        {
            if (obj.Resolve(bill))
            {
                obj.Done(bill);
                return;
            }
            obj = obj.next;
        }
        Fail(bill);
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

//コンクリートハンドラー役
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
        // 100円単位で割り切れる場合
        if (bill.Remainder == 0 && bill.PerPerson % 100 == 0)
        {
            Console.WriteLine($"全員ぴったり {bill.PerPersonInt}円ずつ払う。（100円単位OK）");
            return true;
        }
        return false;
    }
}

/// 100円単位で1人が多く払う
class HundredYenSplit : SplitHandler
{
    public HundredYenSplit(string name) : base(name) { }

    protected override bool Resolve(Bill bill)
    {        
        int basePrice = bill.PerPersonInt;
        int down = (int)Math.Floor(basePrice / 100.0) * 100; // 100円単位で切り下げ
        int up = down + 100; // 100円単位で切り上げ
        int total = down * (bill.PeopleCount - 1) + up;

        if (total >= bill.TotalPrice)
        {
            // 100円単位で1人が多く払うことで解決するケース
            Console.WriteLine($"1人は{up}円払い、残りの{bill.PeopleCount - 1}人は{down}円ずつ払うことで解決します。");
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
        return false;
    }
}

/// 1人が余りを負担する
class YenAdjustmentSplit : SplitHandler
{
    public YenAdjustmentSplit(string name) : base(name) { }

    protected override bool Resolve(Bill bill)
    {
        return false;
    }
}


class Program
{
    static void Main()
    {


        var testCases = new List<Bill>
        {
            new Bill(4400, 4),
            new Bill(4100, 4),
            new Bill(4080, 4),
            new Bill(60, 4),
            new Bill(1001, 4),
            new Bill(1001, 3),
            new Bill(1011, 3)
        };
        // 大雑把スタイル 100円単位なら誰かが多めに出しても良いグループ
        /// 4000円を4人で割る
        /// 4100円なら一人は1100円負担してあとの3人は1000円
        /// 4080円なら一人は1080円負担してあとの3人は1000円
        RunTest(testCases, () =>
            new ExactSplit("Exact")
                .SetNext(new HundredYenSplit("Hundred"))
                .SetNext(new TenYenSplit("Ten"))
                .SetNext(new YenAdjustmentSplit("Adjust"))
        );

        // 平等スタイル できるだけ細かく割り勘する
        /// 4000円を4人で割る
        /// 4100円なら1025円ずつだけどややこしいので1020x3 と 一人1040の負担とする（TenYenSplit）が解決
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
