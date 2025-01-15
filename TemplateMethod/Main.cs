using System;

namespace TemplateMethodPattern
{
    // target
    public abstract class AbstractLighting
    {
        public abstract void Morning();
        public abstract void Daytime();
        public abstract void Night();
        public void Off()
        {
            Console.WriteLine("消灯");
        }
        public void set()
        {
            for (int i = 0; i <= 24; i++){
                Console.Write($"時刻: {i} ");
                if (i == 7){
                    Morning();
                }
                else if (i == 11){
                    Daytime();
                }
                else if (i == 18){
                    Night();
                }
                else if (i == 24){
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
        }
    }
}
