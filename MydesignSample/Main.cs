namespace MydesignSample
{
    class Program{
    static void Main(string[] args)
    {
        VendingMachine vendingMachine = new VendingMachine(10);
        vendingMachine.AddItem(new Drink("Coke", 150));
        vendingMachine.AddItem(new Drink("Pepsi", 350));
        vendingMachine.AddItem(new Drink("Sprite", 450));
        vendingMachine.AddItem(new Drink("Fanta", 250));
        vendingMachine.AddItem(new Drink("7up", 200));
        IIterator it = vendingMachine.CreateIterator();
        while (it.HasNext())
        {
            Drink drink = (Drink)it.Next();
            Console.WriteLine(drink.Name + " : " + drink.Price);
        }
        Console.WriteLine("---安い順--");
       IIterator cit = vendingMachine.CreateCheapIterator();
        while (cit.HasNext())
        {
            Drink drink = (Drink)cit.Next();
            Console.WriteLine(drink.Name + " : " + drink.Price);
        }
    }
}

}
