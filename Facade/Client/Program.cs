using FacadePattern;

public class Program
{
    public static void Main(string[] args)
    {
        // 朝、その日の予定を自分にメールする
        FacadePattern.Calendar calendar = new FacadePattern.Calendar();
        FacadePattern.Facade.SendDailySummary();
    }
}