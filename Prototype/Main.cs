using System;
using System.IO;

namespace Freamwork{
    public interface IProduct{
        public string Open();
        public IProduct CreateClone();
    }

    public class Manager{
        private Dictionary<string, IProduct> showcase = new Dictionary<string, IProduct>();
        public void Register(string name, IProduct proto){
            showcase.Add(name, proto);
        }

        public IProduct Create(string protoname){
            IProduct p = showcase[protoname];
            return p.CreateClone();
        }

}

namespace Program{
    using Freamwork;
    public class Document : IProduct{
        private string _text;
        public Document(string text){
            this._text = text;
        }

        public string Open(){
            return _text;
        }

        public IProduct CreateClone(){
            return new Document(_text);
        }
    }

    public class Program{
        public static void Main(){
            Manager manager = new Manager();
            Document doc = new Document("コーヒー、お茶");
            manager.Register("買い物テンプレート", doc);

            IProduct p1 = manager.Create("買い物テンプレート");
            string body = p1.Open();
            Console.WriteLine(body);

            Console.WriteLine("--追記---");
            body += "、うどん";
            doc = new Document(body);
            manager.Register("スーパーのチェックリスト", doc);
            p1 = manager.Create("スーパーのチェックリスト");
            Console.WriteLine(p1.Open());

            Console.WriteLine("--新規--");
            body = "デスク整理、ゴミ箱片付け、掃除機かけ";
            doc = new Document(body);
            manager.Register("掃除手順書", doc);
            p1 = manager.Create("掃除手順書");

            Console.WriteLine(p1.Open());

            }
        }
    }

}