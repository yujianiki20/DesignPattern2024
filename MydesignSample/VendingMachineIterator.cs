namespace MydesignSample
    {
    public class VendingMachineIterator : IIterator
    {
        private readonly VendingMachine vendingMachine;
        private int index = 0;

        public VendingMachineIterator(VendingMachine vendingMachine)
        {
            this.vendingMachine = vendingMachine;
            this.index = 0;
        }
        
        public bool HasNext()
        {
            if (index < vendingMachine.GetLength()) {
                return true;
            } else {
                return false;
            }
        }

        public object Next()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("No more elements.");
            }
            Drink drink = vendingMachine.GetDrink(index);
            index++;
            return drink;
        }
    }
}