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

public class Task : TaskComponent
{
    private TimeSpan _duration;

    public override TimeSpan Duration => _duration;

    public Task(string name, TimeSpan duration)
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
        _tasks.Add(task);
    }

    public override void RemoveTask(TaskComponent task)
    {
        _tasks.Remove(task);
    }

    public override void ShowTasks(int indent = 0)
    {
        string prefix = indent == 0 ? "" : new string('-', indent);
        Console.WriteLine($"{prefix} {Name}  合計:  {(int)Duration.TotalMinutes}分");
        foreach (TaskComponent task in _tasks)
        {
            task.ShowTasks(indent + 1);
        }
    }
}

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
        TaskComponent task1 = new Task("掃除", TimeSpan.FromMinutes(5));
        TaskComponent task2 = new Task("整頓", TimeSpan.FromMinutes(5));
        TaskComponent task3 = new Task("着替え", TimeSpan.FromMinutes(10));
        TaskComponent task4 = new Task("荷物用意", TimeSpan.FromMinutes(10));

        TaskComponent group1 = new TaskGroup("日課");
        group1.AddTask(task1);
        group1.AddTask(task2);

        TaskComponent group2 = new TaskGroup("外出");
        group2.AddTask(task3);
        group2.AddTask(task4);

        TaskComponent group3 = new TaskGroup("今日の予定");
        group3.AddTask(group1);
        group3.AddTask(group2);

        Console.WriteLine($"{group1.Name} 所要時間: {group1.Duration}");
        Console.WriteLine($"Group 2 所要時間: {group2.Duration}");
        Console.WriteLine($"Group 3 所要時間: {group3.Duration}");

        group3.ShowTasks();

    }
}