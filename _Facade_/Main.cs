using FacadePattern;

public class Program
{
    public static void Main(string[] args)
    {
        // 朝、その日の予定を自分にメールする
        Facade.SendDailySummary();
    }
}