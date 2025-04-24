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
    void setColleagueEnabled(bool enabled);
}

// 可能な操作を規定するクラス。操作をうけつけるクラスはこのクラスを継承する
public abstract class DeviceController
{
    public string name;
    public DeviceController(string name)
    {
        this.name = name;
    }
    public virtual void TurnOn()
    {
        Console.WriteLine($"[{name}] デバイスを起動");
    }
    public virtual void TurnOff()
    {
        Console.WriteLine($"[{name}] デバイスを停止");
    }
    
}

// コンクリートコリーグ役　人感センサー
public class SensorDevice : DeviceController, Colleague
{
    private Mediator mediator;
    private bool isPerson;
    public SensorDevice(Mediator mediator, string name) : base(name)
    {
        this.mediator = mediator;
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
    public void ReceiveCommand(string command)
    {
        Console.WriteLine($"[{name}] {command}");
    }
    
}

// コンクリートコリーグ役　温度センサー
public class TemperatureSensorDevice : DeviceController, Colleague
{
    private Mediator mediator;
    private float temperature;

    public TemperatureSensorDevice(Mediator mediator, string name) : base(name)
    {
        this.mediator = mediator;
    }

    public float GetTemperature()
    {
        return temperature;
    }
    /// 動作検証用
    public void SetTemperature(float temperature)
    {
        this.temperature = temperature;
    }

    public void Detect()
    {
        mediator.ColleagueChanged();
    }
    public void ReceiveCommand(string command)
    {
        Console.WriteLine($"[{name}] {command}");
    }
}

// コンクリートコリーグ役　照明

public class LightDevice : DeviceController, Colleague
{
    private Mediator mediator;

    public LightDevice(Mediator mediator, string name) : base(name)
    {
        this.mediator = mediator;
    }
    public void ReceiveCommand(string command)
    {
        Console.WriteLine($"[{name}] {command}");
    }
}

// コンクリートコリーグ役　エアコン

public class AirconDevice : DeviceController, Colleague
{
    private Mediator mediator;
    private float temperature;

    public AirconDevice(Mediator mediator, string name) : base(name)
    {
        this.mediator = mediator;
    }

    public void SetTemperature(float temperature)
    {
        this.temperature = temperature;
        Console.WriteLine($"[{name}] 温度を{temperature}に設定");
    }
    public void ReceiveCommand(string command)
    {
        Console.WriteLine($"[{name}] {command}");
    }
    
}

public class SmartHub : Mediator
{
    private LightDevice light;
    public AirconDevice ac;
    public SensorDevice sensor;
    public TemperatureSensorDevice temperatureSensor;
    public float temperature = 25;

    public SmartHub()
    {
        CreateColleague();
    }

    public void CreateColleague()
    {
        light = new LightDevice(this, "照明");
        ac = new AirconDevice(this, "エアコン");
        sensor = new SensorDevice(this, "人感センサー");
        temperatureSensor = new TemperatureSensorDevice(this, "温度センサー");
        temperatureSensor.SetTemperature(temperature);
    }

    public void ColleagueChanged()
    {   
        if (sensor.GetSensorData())
        {
            light.TurnOn();
            ac.TurnOn();
        }
        else
        {
            light.TurnOff();
            ac.TurnOff();
        }

        // 寒すぎる → エアコンの温度を上げる
        if (temperature < 20)
        {
            ac.TurnOff();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var mediator = new SmartHub();


        // 人がいる → ライトON、エアコンON
        Console.WriteLine("人がいる");
        mediator.sensor.SetSensorData(true);
        mediator.sensor.Detect();
        Console.WriteLine();

        // 人がいない → ライトOFF、エアコンOFF
        Console.WriteLine("人がいない");
        mediator.sensor.SetSensorData(false);
        mediator.sensor.Detect();
        Console.WriteLine();

        // 温度センサーの温度を設定
        Console.WriteLine("エアコン効きすぎて寒い時");
        mediator.temperatureSensor.SetTemperature(18);
        mediator.temperatureSensor.Detect();
    }
}
