using AbstractFactory;
using System;
namespace KakutouControllerFactory {
    // concrete product
    public class Abutton : PrimaryButton {
        protected override string Action => "パンチ";
        public override void Execute() {
            Console.WriteLine("パンチ！");
        }
    }

    public class Bbutton : SecondaryButton {
        protected override string Action => "キック";
        public override void Execute() {
            Console.WriteLine("キック！");
    }
    }
    public class MoveUpButton : UpButton {
        protected override string Action => "ジャンプ";
        public override void Execute() {
            Console.WriteLine("ジャンプ！");
        }
    }

    public class MoveDownButton : DownButton {
        protected override string Action => "しゃがみ";
        public override void Execute() {
            Console.WriteLine("しゃがみ");
        }
    }

    public class MoveLeftButton : LeftButton {
        protected override string Action => "左移動";
        public override void Execute() {
            Console.WriteLine("←");
        }
    }

    public class MoveRightButton : RightButton {
        protected override string Action => "右移動";
        public override void Execute() {
            Console.WriteLine("→");
        }
    }

    // Concrete Factory
    public class KakutouController : Factory {
        public override Button CreatePrimaryButton() {
            return new Abutton();
        }

        public override Button CreateSecondaryButton() {
            return new Bbutton();
        }

        public override Button CreateUpButton() {
            return new MoveUpButton();
        }

        public override Button CreateDownButton() {
            return new MoveDownButton();
        }

        public override Button CreateLeftButton() {
            return new MoveLeftButton();
        }

        public override Button CreateRightButton() {
            return new MoveRightButton();
        }   
    }
}