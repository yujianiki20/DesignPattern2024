using System;

// メディエーター役
public interface Mediator
{
    void CreateColleague();
    void ColleagueChanged();
}

// コリーグ役
public interface Colleague
{
        void ReceiveCommand(string command);
}

public class SensorDevice : Colleague
{
    private Mediator mediator;
    private bool isPerson;
    public SensorDevice(Mediator mediator)
    {
        this.mediator = mediator;
    }

    public void ReceiveCommand(string command)
    {
        // Sensor はコマンドを受け取らない
    }

    public bool GetSensorData()
    {
        return isPerson;
    }

    // 動作検証用
    public void SetSensorData(bool isPerson)
    {
        this.isPerson = isPerson;
    }

    public void Detect()
    {
        mediator.ColleagueChanged();
    }
    
}

public class LightDevice : Colleague
{
    private Mediator mediator;

    public LightDevice(Mediator mediator)
    {
        this.mediator = mediator;
    }

    public void ReceiveCommand(string command)
    {
        if (command == "TurnOn")
            Console.WriteLine("[照明] ライトを点灯");
        else if (command == "TurnOff")
            Console.WriteLine("[照明] ライトを消灯");
    }
}

public class AirconDevice : Colleague
{
    private Mediator mediator;

    public AirconDevice(Mediator mediator)
    {
        this.mediator = mediator;
    }

    public void ReceiveCommand(string command)
    {
        if (command == "TurnOn")
            Console.WriteLine("[エアコン] エアコンを起動");
        else if (command == "TurnOff")
            Console.WriteLine("[エアコン] エアコンを停止");
    }
}

public class SmartHub : Mediator
{
    private LightDevice light;
    private AirconDevice ac;
    public SensorDevice sensor;

    public SmartHub()
    {
        CreateColleague();
    }

    public void CreateColleague()
    {
        light = new LightDevice(this);
        ac = new AirconDevice(this);
        sensor = new SensorDevice(this);
    }

    public void ColleagueChanged()
    {
        if (sensor.GetSensorData())
        {
            light.ReceiveCommand("TurnOn");
            ac.ReceiveCommand("TurnOn");
        }
        else
        {
            light.ReceiveCommand("TurnOff");
            ac.ReceiveCommand("TurnOff");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var mediator = new SmartHub();


        // 人がいる → ライトON、エアコンON
        mediator.sensor.SetSensorData(true);
        mediator.sensor.Detect();

        // 人がいない → ライトOFF、エアコンOFF
        mediator.sensor.SetSensorData(false);
        mediator.sensor.Detect();
    }
}
