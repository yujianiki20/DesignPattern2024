// 状態インターフェース
public interface IState
{
    void DoPressSwitch(Context context);
    // 客席に着いた時の処理用のメソッド
    void DoComplete(Context context){ }
}

// コンテキスト
public class Context
{
    public IState State { get; set; }

    public bool OnMeal { get; set; } = false;

    public Context()
    {
        State = WaitingState.GetInstance();
    }

    public void PressSwitch()
    {
        State.DoPressSwitch(this);
    }
    public void Complete()
    {
        // ここはロボットの目的を達成する処理と状態変更を行う
        State.DoComplete(this);
    }
    public void ChangeState(IState state)
    {
        this.State = state;
    }   
}

public class WaitingState : IState
{
    private static IState singleton = new WaitingState();
    private WaitingState() { }
    public static IState GetInstance()
    {
        return singleton;
    }
    public void DoPressSwitch(Context context)
    {
        Console.WriteLine("スイッチが押されました");

        if (context.OnMeal)
        {
            Console.WriteLine("お客様へ配膳します。");
            context.ChangeState(DeliveringState.GetInstance());
        }
        else
        {
            Console.WriteLine("料理を乗せてください。");
        }
    }
    // 客席に着いた時の処理用のメソッド このstateは何もしない
    public void DoComplete(Context context) { }
}
public class DeliveringState : IState
{
    private static IState singleton = new DeliveringState();
    private DeliveringState() { }
    public static IState GetInstance()
    {
        return singleton;
    }
    public void DoPressSwitch(Context context)
    {
        Console.WriteLine("スイッチが押されました。移動中のため無視します。");
    }
    public void DoComplete(Context context)
    {
        Console.WriteLine("客席に到着しました。提供モードへ移行します。");
        context.ChangeState(ServingState.GetInstance());
    }
}
public class ServingState : IState
{
    private static IState singleton = new ServingState();
    private ServingState() { }
    public static IState GetInstance()
    {
        return singleton;
    }
    public void DoPressSwitch(Context context)
    {
        Console.WriteLine("スイッチが押されました。");

        if (!context.OnMeal)
        {
            Console.WriteLine("料理を受け取ったのを確認。厨房へ戻ります。");
            context.ChangeState(ReturnState.GetInstance());
        }
        else
        {
            Console.WriteLine("料理をとってからボタンを押してください。");
        }
    }
    public void DoComplete(Context context) { }
}
public class ReturnState : IState
{
    private static IState singleton = new ReturnState();
    private ReturnState() { }
    public static IState GetInstance()
    {
        return singleton;
    }
    public void DoPressSwitch(Context context)
    {
        Console.WriteLine("スイッチが押されました。厨房へ移動中のため無視します。");
    }
    public void DoComplete(Context context)
    {
        Console.WriteLine("厨房に戻りました。待機します。");
        context.ChangeState(WaitingState.GetInstance());
    }
}
public class Program
{
    public static void Main()
    {
        var context = new Context();

        // 1. 料理がまだ乗っていない
        context.PressSwitch();  // 「料理なし」

        // 2. 料理を乗せる
        context.OnMeal = true;
        context.PressSwitch();  // 「運搬開始」

        // 3. お客様の席に移動中
        context.PressSwitch();  // この時はボタン押されても無視される
        context.Complete();  // 客席に到着したら提供モードへ移行

        // 4. まだ取られてない
        context.OnMeal = true;
        context.PressSwitch();  // 「まだ取られていない」

        // 5. 取られた！
        context.OnMeal = false;
        context.PressSwitch();  // 「キッチンへ戻る」
        context.Complete();  // 厨房に戻ったら待機状態へ移行

        // 6. 待機状態へ戻る
        context.PressSwitch();  // 「待機状態に戻る」
    }
}
