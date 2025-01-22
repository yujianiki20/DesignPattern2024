
using System;
using Framework;

namespace Suisai
{
    public class Suisai : Framework.Image
    {
        private string _input;
        public Suisai(string input)
        {
            Console.WriteLine($"{input}をプラグインに読み込み");
            _input = input;
        }
        public override void process()
        {
            Console.WriteLine($"{_input}を水彩画風にする処理");
        }
        public override void show()
        {
            Console.WriteLine($"水彩画風になった{_input}");
        }
        public override void save()
        {
            Console.WriteLine($"水彩画風になった{_input}を保存");
        }
        public override void send()
        {
            Console.WriteLine($"水彩画風になった{_input}をメール送信");
        }
    }
    public class SuisaiPlugin : Framework.Plugin
    {
        protected override Image LoadImage(string input)
        {
            return new Suisai(input);
        }
        protected override Image ProcessImage(Image image)
        {
            image.process();
            return image;
        }
    }
}