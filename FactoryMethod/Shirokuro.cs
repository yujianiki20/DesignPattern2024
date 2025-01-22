
using System;
using Framework;

namespace Shirokuro
{
    public class Shirokuro : Framework.Image
    {
        private string _input;
        public Shirokuro(string input)
        {
            Console.WriteLine($"{input}をプラグインに読み込み");
            _input = input;
        }
        public override void process()
        {
            Console.WriteLine($"{_input}を白黒にする処理");
        }
        public override void show()
        {
            Console.WriteLine($"白黒になった{_input}");
        }
        public override void save()
        {
            Console.WriteLine($"白黒になった{_input}を保存");
        }
        public override void send()
        {
            Console.WriteLine($"白黒になった{_input}をメール送信");
        }
    }
    public class ShirokuroPlugin : Framework.Plugin
    {
        protected override Image LoadImage(string input)
        {
            return new Shirokuro(input);
        }
        protected override Image ProcessImage(Image image)
        {
            image.process();
            return image;
        }
    }
}