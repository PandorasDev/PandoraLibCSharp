using PandoraLib.Attributes;

namespace PandoraLib.Stream.Impl;

public class DictionaryStream<TKey, TValue>(IDictionary<TKey, TValue> map): IStream where TKey : notnull
{
    [NewInstanceCopy]
    public DictionaryStream<TNk, TNv> Map<TNk, TNv>(Func<KeyValuePair<TKey, TValue>, KeyValuePair<TNk, TNv>> mapper) where TNk : notnull
    {
        var newMap = new Dictionary<TNk, TNv>();
        foreach (var keyValuePair in map)
        {
            var newKeyValuePair = mapper(keyValuePair);
            
            newMap.Add(newKeyValuePair.Key, newKeyValuePair.Value);
        }

        return DictionaryStream<TNk, TNv>.Of(newMap);
        
    }
    
    [NewInstanceCopy]
    public DictionaryStream<TNk, TValue> MapKeys<TNk>(Func<TKey, TNk> mapper) where TNk : notnull
    {
        return Map(keyValuePair => new KeyValuePair<TNk, TValue>(mapper(keyValuePair.Key), keyValuePair.Value));
    }
    
    [NewInstanceCopy]
    public DictionaryStream<TKey, TNv> MapValues<TNv>(Func<TValue, TNv> mapper)
    {
        return Map(keyValuePair => new KeyValuePair<TKey, TNv>(keyValuePair.Key, mapper(keyValuePair.Value)));
    }
    
    [NewInstanceCopy]
    public DictionaryStream<TKey, TValue> Filter(Func<KeyValuePair<TKey, TValue>, bool> predicate)
    {
        var newMap = new Dictionary<TKey, TValue>();
        foreach (var keyValuePair in map)
        {
            if (predicate(keyValuePair)) newMap.Add(keyValuePair.Key, keyValuePair.Value);
        }

        return Of(newMap);
    }
    
    public DictionaryStream<TKey, TValue> FilterKeys(Func<TKey, bool> predicate)
    {
        return Filter(keyValuePair => predicate(keyValuePair.Key));
    }
    
    public DictionaryStream<TKey, TValue> FilterValues(Func<TValue, bool> predicate)
    {
        return Filter(keyValuePair => predicate(keyValuePair.Value));
    }
    
    public void ForEach(Action<KeyValuePair<TKey, TValue>> action)
    {
        foreach (var keyValuePair in map)
        {
            action(keyValuePair);
        }
    }
    
    public T To<T>(Func<IDictionary<TKey, TValue>, T> mapper)
    {
        return mapper(map);
    }
    
    public void ToDictionary(out IDictionary<TKey, TValue> outMap)
    {
        outMap = new Dictionary<TKey, TValue>(map);
    }
    
    public Dictionary<TKey, TValue> ToDictionary()
    {
        return new Dictionary<TKey, TValue>(map);
    }
    
    
    public static DictionaryStream<TKey, TValue> Of(IDictionary<TKey, TValue> map)
    {
        return new DictionaryStream<TKey, TValue>(map);
    }
}