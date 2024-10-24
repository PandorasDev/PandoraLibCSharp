using System.Diagnostics.CodeAnalysis;
using PandoraLib.Attributes;
using PandoraLib.Stream.Extensions;
using PandoraLib.Stream.Impl;

namespace PandoraLib.Json.Impl;

public class JsonObject(IDictionary<string, JsonElement> map) : JsonElement
{
    public JsonObject() : this(new Dictionary<string, JsonElement>())
    {
    }
    
    public JsonElement this[string key]
    {
        get => Get(key);
        set => Add(key, value);
    }
    
    [Fluent] 
    public JsonObject Add(string key, object? value)
    {
        return Add(key, Of(value));
    }
    
    [Fluent] 
    public JsonObject Add(string key, JsonElement value)
    {
        map.Add(key, value);
        return this;
    }
    public bool AddIfAbsent(string key, JsonElement value) => map.TryAdd(key, value);
    
    public bool Remove(string key) => map.Remove(key);
    
    public bool ContainsKey(string key) => map.ContainsKey(key);
    
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out JsonElement value) => map.TryGetValue(key, out value);
    
    public JsonElement Get(string key) => map[key];
    
    
    public static JsonObject Of<T>(IDictionary<string, T> map)
    {
        var json = new JsonObject();
        foreach (var keyValuePair in map)
        {
            json.Add(keyValuePair.Key, keyValuePair.Value);
        }

        return json;
    }

    public override DictionaryStream<string,JsonElement> Stream()
    {
        return DictionaryStream<string,JsonElement>.Of(map);
    }
    
    public static JsonObject Parse(string jsonString)
    {
        return JsonParser.ParseObject(jsonString);
    }

    public override string ToJsonString()
    {
        return $"{{{map.Stream(kp => $"{kp.Key}: {kp.Value.ToJsonString()}").Join(", ")}}}";
    }
}