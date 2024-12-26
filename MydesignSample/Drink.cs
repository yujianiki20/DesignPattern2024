namespace MydesignSample{
    public class Drink
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public Drink(string name, int price)
        {
            Name = name;
            Price = price;
        }
    }
}