using System;

namespace Framework
{
    public abstract class Plugin{
        public Image Load(string input){
            Image image = LoadImage(input);
            image = ProcessImage(image);
            return image;
        }
        protected abstract Image LoadImage(string input);
        protected abstract Image ProcessImage(Image image);
    }
    public abstract class Image{
        public abstract void process();
        public abstract void show();
        public abstract void save();
        public abstract void send();
    }
}