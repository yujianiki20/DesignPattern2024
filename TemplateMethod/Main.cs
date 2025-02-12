using System;

namespace TemplateMethodPattern
{
    // target
    public abstract class AbstractLighting
    {
        protected virtual int OnTime { get; } = 7;
        protected virtual int SwitchDaytime { get; } = 11;
        protected virtual int SwitchNight { get;} = 18;
        protected virtual int OffTime { get; } = 24;
        public abstract void Morning();
        public abstract void Daytime();
        public abstract void Night();
        public void Off()
        {
            Console.Write("消灯");
        }
        public void set()
        {
            for (int i = 0; i <= 24; i++){
                Console.Write($"時刻: {i} ");
                if (i == OnTime){
                    Morning();
                }
                else if (i == SwitchDaytime){
                    Daytime();
                }
                else if (i == SwitchNight){
                    Night();
                }
                else if (i == OffTime){
                    Off();
                }
                Console.WriteLine();
            }
        }
    }

    // concrete class
    public class LightingA : AbstractLighting
    {
        public override void Morning()
        {
            Console.Write("朝の照明をつける");
        }
        public override void Daytime()
        {
            Console.Write("昼の照明をつける");
        }
        public override void Night()
        {
            Console.Write("夜の照明をつける");
        }
    }
        public class LightingB : AbstractLighting
    {
        public override void Morning()
        {
            Console.Write("薄暗い照明が徐々に明るくなる");
        }
        public override void Daytime()
        {
            Console.Write("明るい照明をつける");
        }
        public override void Night()
        {
            Console.Write("オレンジの照明をつける");
        }
    }

    public class LightingC : LightingB
    {
        protected override int OffTime { get; } = 22;
    }
    // main
    class Program
    {
        static void Main(string[] args)
        {
            AbstractLighting lighting1 = new LightingA();
            lighting1.set();
            Console.WriteLine("----");
            AbstractLighting lighting2 = new LightingB();
            lighting2.set();
            Console.WriteLine("----");
            LightingC lighting3 = new LightingC();
            lighting3.set();
        }
    }
}
