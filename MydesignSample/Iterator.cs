

namespace MydesignSample
{
    public interface IIterator
    {
        bool HasNext();
        object Next();
    }

    public interface IAggregate
    {
        IIterator CreateIterator();
    }
}