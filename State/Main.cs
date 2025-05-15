using System.Threading;

// 状態インターフェース
public interface IState
{
    void DoPressSwitch(Context context);
    void DoExecute(Context context){ }
    void DoComplete(Context context){ }
}

// コンテキスト
public class Context
{
    private IState State { get; set; }

    public bool OnMeal { get; set; } = false;

    public Context()
    {
        State = WaitingState.GetInstance();
    }

    public void PressSwitch()
    {
        State.DoPressSwitch(this);
    }
    public void ChangeState(IState state)
    {
        this.State = state;
        this.State.DoExecute(this);
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
        Console.WriteLine(" スイッチが押されました");

        if (context.OnMeal)
        {
            Console.WriteLine(" お客様へ配膳します。");
            DoComplete(context);
        }
        else
        {
            Console.WriteLine(" 料理を乗せてください。");
        }
    }
    public void DoExecute(Context context) {
        Console.WriteLine("Start: WaitingState");
        Console.WriteLine(" 待機開始...");
    }
    public void DoComplete(Context context) { 
        context.ChangeState(DeliveringState.GetInstance());
    }

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
        Console.WriteLine(" スイッチが押されました。移動中のため無視します。");
    }
    public void DoExecute(Context context) {
        Console.WriteLine("Start: DeliveringState");
        Console.WriteLine(" 運搬開始...");
        Thread.Sleep(3000); // 3秒待機
        DoComplete(context);
    }
    public void DoComplete(Context context)
    {
        Console.WriteLine(" 客席に到着しました。提供モードへ移行します。");
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
            Console.WriteLine(" 料理を受け取ったのを確認。厨房へ戻ります。");
            DoComplete(context);
        }
        else
        {
            Console.WriteLine(" 料理をとってからボタンを押してください。");
        }
    }
    public void DoExecute(Context context) {
        Console.WriteLine("Start: ServingState");
        Console.WriteLine(" 料理を提供します、料理がとられるまで待機します。");
    }
    public void DoComplete(Context context) { 
        context.ChangeState(ReturnState.GetInstance());
    }
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
        Console.WriteLine(" スイッチが押されました。厨房へ移動中のため無視します。");
    }
    public void DoExecute(Context context) {
        Console.WriteLine("Start: ReturnState");
        Console.WriteLine(" 厨房に戻ります。");
        Thread.Sleep(3000); // 3秒待機
        DoComplete(context);
    }
    public void DoComplete(Context context)
    {
        Console.WriteLine(" 厨房に戻りました。待機します。");
        context.ChangeState(WaitingState.GetInstance());
    }
}
public class Program
{
    public static void Main()
    {
        
        var context = new Context();

        // 1. 料理がまだ乗っていない
        //context.PressSwitch();  // 「料理なし」

        // 2. 料理を乗せる
        context.OnMeal = true;
        context.PressSwitch();  // 「運搬開始」

        // 3. お客様の席に移動中
        //context.PressSwitch();  // この時はボタン押されても無視される

        // 4. まだ取られてない
        // context.OnMeal = true;
        // context.PressSwitch();  // 「まだ取られていない」

        // 5. 取られた！
        context.OnMeal = false;
        context.PressSwitch();  // 「キッチンへ戻る」

        // 6. 待機状態へ戻る
        //context.PressSwitch();  // 「待機状態に戻る」確認。
    }
}
