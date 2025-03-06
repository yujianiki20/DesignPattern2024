public class Counter {
    private CounterImpl _impl;
    public Counter(CounterImpl impl) {
        _impl = impl;
    }
    public void Add() {
        _impl.RowAdd();
    }
    public void Lose() {
        _impl.RowLose();
    }
    public Dictionary<string, int> GetScores(){
        return _impl.RowGetScores();
    }
}

public class CounterDisplay : Counter {
    public CounterDisplay(CounterImpl impl) : base(impl) {
    }
    public void Show() {
        Dictionary<string, int> scores = GetScores();
        Console.WriteLine(string.Join(" | ", scores.Select(score => $"{score.Key}: {score.Value}")));
    }
}

public class CounterLog : CounterDisplay {
    protected List<Dictionary<string, int>> _logs = new List<Dictionary<string, int>>();
    public CounterLog(CounterImpl impl) : base(impl) {
    }
    private void Logging() {
        _logs.Add(GetScores());
    }
    public void LoggingAdd() {
        Add();
        Logging();
    }

    public void LoggingLose() {
        Lose();
        Logging();
    }
    
    public void ShowLogs() {
        foreach (var log in _logs) {
            Console.WriteLine(string.Join(" | ", log.Select(score => $"{score.Key}: {score.Value}")));
        }
    }
}

public abstract class CounterImpl {
    public abstract void RowAdd();
    public abstract void RowLose();
    public abstract Dictionary<string, int> RowGetScores();
}

public class SoccerScoring : CounterImpl {
    private int _count1, _count2;
    public override void RowAdd() {
        _count1++;
    }
    public override void RowLose() {
        _count2++;
    }
    
    public override Dictionary<string, int> RowGetScores(){
        return new Dictionary<string, int>{
            {"得点", _count1},
            {"失点", _count2}
        };
    }
}

public class TennisScoring : CounterImpl {
    private int[] _scores = { 0, 15, 30, 40 };
    private int _count1 = 0, _count2 = 0;
    public override void RowAdd() {
        if (_count1 < _scores.Length - 1) {
            _count1++;
        }
    }
    public override void RowLose() {
        if (_count2 < _scores.Length - 1) {
            _count2++;
        }
    }

    public override Dictionary<string, int> RowGetScores(){
        return new Dictionary<string, int>(){
            {"自分", _scores[_count1]},
            {"相手", _scores[_count2]}
        };
    }
}

public class CoinGameScoring : CounterImpl  {
    private int _count1 = 10;
    public override void RowAdd() {
        _count1++;
    }
    public override void RowLose() {
        _count1--;
    }
    public override Dictionary<string, int> RowGetScores(){
        return new Dictionary<string, int>(){
            {"ポイント", _count1}
        };
    }
}

class Program {
    static void Main(string[] args) {
        Counter soccerCounter = new Counter(new SoccerScoring());
        //CounterDisplay soccerDisplay = new CounterDisplay(new SoccerScoring());
        soccerCounter.Add();
        soccerCounter.Add();
        soccerCounter.Lose();
        Dictionary<string, int> scores = soccerCounter.GetScores();
        Console.WriteLine(string.Join(" | ", scores.Select(score => $"{score.Key}: {score.Value}")));
        
        Console.WriteLine("-----");
        Counter tennisCounter = new Counter(new TennisScoring());
        CounterDisplay tennisDisplay = new CounterDisplay(new TennisScoring());
        CounterLog tennisLog = new CounterLog(new TennisScoring());
        tennisLog.LoggingAdd();
        tennisLog.LoggingAdd();
        tennisLog.LoggingAdd();
        
        tennisLog.LoggingLose();
        tennisLog.ShowLogs();

        Console.WriteLine("-----");
        tennisDisplay.Show();
        //tennisDisplay.GetScores();

        Console.WriteLine("-----");

        CounterLog coinGameLog = new CounterLog(new CoinGameScoring());
        coinGameLog.LoggingAdd();
        coinGameLog.LoggingAdd();
        coinGameLog.LoggingAdd();
        coinGameLog.LoggingLose();
        coinGameLog.LoggingLose();
        coinGameLog.LoggingLose();
        coinGameLog.LoggingLose();
        coinGameLog.ShowLogs();
    }
}