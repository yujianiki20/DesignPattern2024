///コンポーネント役
public abstract class TaskComponent
{

    public string Name { get; protected set; }
    public abstract TimeSpan Duration { get; }

    public virtual void AddTask(TaskComponent task)
    {
        throw new TaskTreatmentException();
    }

    public virtual void RemoveTask(TaskComponent task)
    {
        throw new TaskTreatmentException();
    }

    public abstract void ShowTasks(int indent = 0);
} 

//リーフ役
public class TaskLeaf : TaskComponent
{
    private TimeSpan _duration;

    public override TimeSpan Duration => _duration;

    public TaskLeaf(string name, TimeSpan duration)
    {
        Name = name;
        _duration = duration;
    }

    public override void ShowTasks(int indent = 0)
    {
        string prefix = indent == 0 ? "" : new string('-', indent);
        Console.WriteLine($"{prefix} {Name} : {(int)_duration.TotalMinutes}分");
    }
} 

//コンポジット役
public class TaskGroup : TaskComponent
{
    private List<TaskComponent> _tasks = new List<TaskComponent>();

    public override TimeSpan Duration 
    { 
        get
        {
            TimeSpan duration = TimeSpan.Zero;
            foreach (TaskComponent task in _tasks)
            {
                duration += task.Duration;
            }
            return duration;
        }
    }

    public TaskGroup(string name)
    {
        Name = name;
    }

    public override void AddTask(TaskComponent task)
    {
        if (task == this)
        {
            throw new TaskTreatmentException();
        }
        _tasks.Add(task);
    }

    public override void RemoveTask(TaskComponent task)
    {
        _tasks.Remove(task);
    }

    public override void ShowTasks(int indent = 0)
    {
        string prefix = indent == 0 ? "" : new string('-', indent);
        Console.WriteLine($"{prefix} {Name}  合計:  {Duration.ToString("%m")}分");
        Console.WriteLine($"{prefix} {Name}  合計: {(int)Duration.TotalHours:D2}:{Duration.Minutes:D2}分");
        Console.WriteLine($"{prefix} {Name}  合計: {(int)Duration.TotalMinutes:D2}分");
        Console.WriteLine($"{prefix} {Name}  合計:  {Duration.ToString(@"hh\:mm")}");
        Console.WriteLine($"{prefix} {Name} 合計: {Duration.ToString(@"%h\:%m")}");

        foreach (TaskComponent task in _tasks)
        {
            task.ShowTasks(indent + 1);
        }
    }
}

//例外クラス
class TaskTreatmentException : Exception
{
    public TaskTreatmentException() : base("Task can't be added or removed from a task.")
    {
    }
}

class Program
{
    static void Main()
    {
        TaskComponent task1 = new TaskLeaf("掃除", TimeSpan.FromMinutes(5));
        TaskComponent task2 = new TaskLeaf("整頓", TimeSpan.FromMinutes(5));
        TaskComponent task3 = new TaskLeaf("着替え", TimeSpan.FromMinutes(10));
        TaskComponent task4 = new TaskLeaf("荷物用意", TimeSpan.FromMinutes(100));

        TaskComponent group1 = new TaskGroup("日課");
        group1.AddTask(task1);
        group1.AddTask(task2);

        TaskComponent group2 = new TaskGroup("外出");
        group2.AddTask(task3);
        group2.AddTask(task4);

        TaskComponent group3 = new TaskGroup("今日の予定");
        group3.AddTask(group1);
        group3.AddTask(group2);


        Console.WriteLine("----テスト表示----");
        Console.WriteLine($"{group1.Name} 所要時間: {group1.Duration}");
        Console.WriteLine($"{group2.Name} 所要時間: {group2.Duration}");
        Console.WriteLine($"{group3.Name} 所要時間: {group3.Duration}");

        Console.WriteLine("");   
        Console.WriteLine("-----ShowTasks()------");
        group3.ShowTasks();

        Console.WriteLine("");
        Console.WriteLine("-----RemoveTask()------");
        group3.RemoveTask(group2);
        group3.ShowTasks();

        // group3.AddTask(group3);
        // group3.ShowTasks();
        // Console.WriteLine("");
        // Console.WriteLine("-----例外テスト------");
        // try
        // {
        //     task1.AddTask(task2);  // Taskクラスへの追加は例外が発生するはず
        // }
        // catch (TaskTreatmentException ex)
        // {
        //     Console.WriteLine($"予想通りの例外が発生: {ex.Message}");
        // }

        // try 
        // {
        //     task1.RemoveTask(task2);  // Taskクラスからの削除も例外が発生するはず
        // }
        // catch (TaskTreatmentException ex)
        // {
        //     Console.WriteLine($"予想通りの例外が発生: {ex.Message}");
        // }

        // Console.WriteLine("");
        // Console.WriteLine("-----追加テスト------");
        // TaskComponent task5 = new Task("メール確認", TimeSpan.FromMinutes(15));
        // group1.AddTask(task5);
        // Console.WriteLine("group1に新タスク追加後:");
        // group1.ShowTasks();
        // Console.WriteLine($"group1の更新後の所要時間: {group1.Duration}");

    }
}