using PandoraLib.Attributes;

namespace PandoraLib.Stream.Impl;

public class ArrayStream<T>(IEnumerable<T> array) : IStream
{
    
    [NewInstanceCopy]
    public ArrayStream<TN> Map<TN>(Func<T, TN> mapper)
    {
        return new ArrayStream<TN>(array.Select(mapper));
    }
    
    [NewInstanceCopy]
    public ArrayStream<T> Filter(Func<T, bool> predicate)
    {
        return new ArrayStream<T>(array.Where(predicate));
    }
    
    public void ForEach(Action<T> action)
    {
        foreach (var item in array)
        {
            action(item);
        }
    }
    
    public void ToArray(out T[] outArray)
    {
        outArray = array.ToArray();
    }

    public TN To<TN>(Func<IEnumerable<T>, TN> mapper)
    {
        return mapper(array);
    }
    public T[] ToArray()
    {
        return array.ToArray();
    }
    
    public T First()
    {
        return array.First();
    }
    
    public static ArrayStream<T> Of(IEnumerable<T> array)
    {
        return new ArrayStream<T>(array);
    }
}