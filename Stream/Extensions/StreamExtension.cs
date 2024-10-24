using PandoraLib.Stream.Impl;

namespace PandoraLib.Stream.Extensions;

public static class StreamExtension
{
    public static ArrayStream<T> Stream<T>(this IEnumerable<T> enumerable)
    {
        return new ArrayStream<T>(enumerable);
    }
    
    public static ArrayStream<TN> Stream<T, TN>(this IEnumerable<T> enumerable, Func<T, TN> mapper)
    {
        return new ArrayStream<T>(enumerable).Map(mapper);
    }
    
    public static ArrayStream<T> Stream<T>(this IEnumerable<T> enumerable, Func<T, bool> filter)
    {
        return new ArrayStream<T>(enumerable).Filter(filter);
    }

    
    public static DictionaryStream<TKey, TValue> Stream<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) where TKey : notnull
    {
        return new DictionaryStream<TKey, TValue>(dictionary);
    }
    
    public static DictionaryStream<TNk, TNv> Stream<TKey, TValue, TNk, TNv>(this IDictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, KeyValuePair<TNk, TNv>> mapper) where TKey : notnull where TNk : notnull
    {
        return new DictionaryStream<TKey, TValue>(dictionary).Map(mapper);
    }
    
    public static DictionaryStream<TKey, TValue> Stream<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, bool> filter) where TKey : notnull
    {
        return new DictionaryStream<TKey, TValue>(dictionary).Filter(filter);
    }
    
    public static string Join(this ArrayStream<string> enumerable, string separator)
    {
        return string.Join(separator, enumerable);
    }
    
    
}