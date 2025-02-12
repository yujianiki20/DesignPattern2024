using System.Linq;

namespace MydesignSample

    {
    public class VendingMachineCheapIterator : IIterator
    {
        private readonly VendingMachine vendingMachine;
        private int index = 0;
        private List<Drink> _drinks = new();

        public VendingMachineCheapIterator(VendingMachine vendingMachine)
        {
            this.vendingMachine = vendingMachine;
            // 価格でソート
            _drinks = vendingMachine.GetDrinks().OrderBy(x => x.Price).ToList();
            this.index = 0;
        }
        

        public bool HasNext()
        {
            if (index < _drinks.Count) {
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
            Drink drink = _drinks[index];
            index++;
            return drink;
        }
    }
}