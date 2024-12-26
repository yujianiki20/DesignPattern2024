namespace MydesignSample
{
    public class VendingMachine : IAggregate
    {
        private List<Drink> drinks = new List<Drink>();
        private int index = 0;

        public IIterator CreateIterator()
        {
            return new VendingMachineIterator(this);
        }
        public IIterator CreateCheapIterator()
        {
            return new VendingMachineCheapIterator(this);
        }
        public VendingMachine(int initialSize){
            drinks = new List<Drink>(initialSize);
        }
        public Drink GetDrink(int index){
            return drinks[index];
        }
        public int GetLength(){
            return drinks.Count;
        }

        public void AddItem(Drink drink)
        {
            drinks.Add(drink);
        }
    }
}