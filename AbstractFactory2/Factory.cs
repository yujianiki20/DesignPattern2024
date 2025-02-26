using System;
using System.IO;

namespace AbstractFactory {
    public abstract class Button {
        protected abstract string Action { get; }

        public void GetDescription() {
            Console.WriteLine($"{GetType().Name}: {Action}");
        }

        public abstract void Execute(); 
        public abstract void GetRole();
    }

    public abstract class PrimaryButton : Button {
        protected string Role => "PrimaryButton";

        public override void GetRole() {
            Console.WriteLine(Role);
        }
    }

    public abstract class SecondaryButton : Button {
        protected string Role = "SecondaryButton";

        public override void GetRole() {
            Console.WriteLine(Role);
        }
    }

    public abstract class UpButton : Button 
    {
        protected string Role = "UpButton";

        public override void GetRole() {
            Console.WriteLine(Role);
        }
    }

    public abstract class DownButton : Button 
    {
        protected string Role = "DownButton";

        public override void GetRole() {
            Console.WriteLine(Role);
        }
    }

    public abstract class LeftButton : Button 
    {
        protected string Role = "LeftButton";

        public override void GetRole() {
            Console.WriteLine(Role);
        }
    }

    public abstract class RightButton : Button
    {
        protected string Role = "RightButton";

        public override void GetRole() {
            Console.WriteLine(Role);
        }
    }

    // Abstract Factory
    public abstract class Factory {
        public static Factory GetFactory(string className)
        {
            Factory factory = null;
            try
            {
                Type type = Type.GetType(className);
                if (type == null)
                {
                    Console.WriteLine("クラス " + className + " が見つかりません。");
                }
                else
                {
                    factory = (Factory)Activator.CreateInstance(type);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return factory;
        }

        public abstract Button CreatePrimaryButton();
        public abstract Button CreateSecondaryButton();
        public abstract Button CreateUpButton();
        public abstract Button CreateDownButton();
        public abstract Button CreateLeftButton();
        public abstract Button CreateRightButton();
    }
}