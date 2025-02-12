using System;
using System.IO;

namespace Freamwork{
    public interface IProduct{
        public string Text { get; set; }
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
        public Manager ShallowCopy()
        {
            return (Manager)this.MemberwiseClone();
        }

        public Manager DeepCopy()
        {
            Manager newManager = (Manager)this.MemberwiseClone();
            newManager.showcase = new Dictionary<string, IProduct>(showcase);
            return newManager; //これではIProductのインスタンスは同じものを参照している
        }
        public void Show(){
            foreach(KeyValuePair<string, IProduct> kvp in showcase){
                Console.WriteLine(kvp.Key);
            }
        }
}

namespace Program{
    using Freamwork;
    public class Document : IProduct{
        private string _text;

        public string Text{
            get { return _text; }
            set { _text = value; }
        }
        public Document(string text){
            this._text = text;
        }

        public string Open(){
            return _text;
        }

        public IProduct CreateClone(){
            return (IProduct)this.MemberwiseClone();
            //return new Document(_text);
    }

    public class Program{
        public static void Main(){
            Manager manager1 = new Manager();

            Document doc1 = new Document("コーヒー、お茶");
            manager1.Register("買い物テンプレート", doc1); //manager1にだけ登録

            Manager manager2 = manager1.ShallowCopy(); //manager1インスタンスをシャローコピー
            Manager manager3 = manager1.DeepCopy(); //manager1インスタンスをディープコピー
            
            Document doc2 = new Document("掃除、洗濯");
            manager1.Register("家事テンプレート", doc2); //manager1にだけ登録

            Document doc3 = new Document("運動、散歩");
            manager1.Register("日課", doc3); //manager1にだけ登録

            Console.WriteLine("---浅いコピー---");
            manager2.Show(); //manager2のみshowcase辞書を表示
            Console.WriteLine("---深いコピー---");
            manager3.Show(); //manager3のみshowcase辞書を表示





            //IProduct p1 = manager.Create("買い物テンプレート");
        
            // IProduct p2 = manager.Create("買い物テンプレート");

            // p1.Text = "aaaaa";
            // Console.WriteLine(p1.Open());
            // Console.WriteLine(p2.Open());

            //  string body = p1.Open();
            //  Console.WriteLine(body);

            // Console.WriteLine("--追記---");
            // body += "、うどん";
            // doc1.Text = "aaaaaaaaaa";
            // Document doc2 = new Document(body);
            // manager.Register("スーパーのチェックリスト", doc2);
            // p1 = manager.Create("スーパーのチェックリスト");
            // Console.WriteLine(p1.Open());

            // Console.WriteLine("--新規--");
            // body = "デスク整理、ゴミ箱片付け、掃除機かけ";
            // doc1 = new Document(body);
            // manager.Register("掃除手順書", doc1);
            // p1 = manager.Create("掃除手順書");

            // Console.WriteLine(p1.Open());

            }
        }
    }

}