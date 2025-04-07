using AbstractFactory;
using System;

namespace MarioControllerFactory {
    // concrete product
    public class DashButton : PrimaryButton {
        protected override string Action => "ダッシュ";
        public override void Execute() {
            Console.WriteLine("ダッシュ！");
        }
    }

    public class JumpButton : SecondaryButton {
        protected override string Action => "ジャンプ";
        public override void Execute() {
            Console.WriteLine("ジャンプ！");
        }
    }

    public class MoveUpButton : UpButton {
        protected override string Action => "";
        public override void Execute() {
            Console.WriteLine("");
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
    public class MarioController : Factory {
        public override PrimaryButton CreatePrimaryButton() {
            return new DashButton();
        }

        public override SecondaryButton CreateSecondaryButton() {
            return new JumpButton();
        }

        public override UpButton CreateUpButton() {
            return new MoveUpButton();
        }
        
        public override DownButton CreateDownButton() {
            return new MoveDownButton();
        }

        public override LeftButton CreateLeftButton() {
            return new MoveLeftButton();
        }

        public override RightButton CreateRightButton() {
            return new MoveRightButton();
        }
    }
}   