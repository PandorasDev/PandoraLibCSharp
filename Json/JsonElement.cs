using PandoraLib.Json.Impl;
using PandoraLib.Stream;
using PandoraLib.Utils;

namespace PandoraLib.Json;
public abstract class JsonElement
{
    public abstract IStream Stream();
    public abstract string ToJsonString();
    
    public bool IsJsonNull()
    {
        return this is JsonNull;
    }
    
    public bool IsJsonPrimitive()
    {
        return this is JsonPrimitive;
    }
    
    public bool IsJsonArray()
    {
        return this is JsonArray;
    }
    
    public bool IsJsonObject()
    {
        return this is JsonObject;
    }
    
    public bool IsJsonBytes()
    {
        return this is JsonBytes;
    }
    
    public JsonNull AsJsonNull()
    {
        return this as JsonNull ?? throw new InvalidCastException("This is not a JsonNull");
    }
    
    public JsonPrimitive AsJsonPrimitive()
    {
        return this as JsonPrimitive ?? throw new InvalidCastException("This is not a JsonPrimitive");
    }
    
    public JsonArray AsJsonArray()
    {
        return this as JsonArray ?? throw new InvalidCastException("This is not a JsonArray");
    }
    
    public JsonObject AsJsonObject()
    {
        return this as JsonObject ?? throw new InvalidCastException("This is not a JsonObject");
    }
    
    public JsonBytes AsJsonBytes()
    {
        return this as JsonBytes ?? throw new InvalidCastException("This is not a JsonBytes");
    }
    
    public JsonElement FromJsonString(string json)
    {
        return JsonParser.Parse(json);
    }

    public static JsonElement Of(object? obj)
    {
        return obj switch
        {
            null => JsonNull.Instance,
            string s => JsonPrimitive.Of(s),
            int i => JsonPrimitive.Of(i),
            long l => JsonPrimitive.Of(l),
            float f => JsonPrimitive.Of(f),
            double d => JsonPrimitive.Of(d),
            bool b => JsonPrimitive.Of(b),
            Number n => JsonPrimitive.Of(n),
            byte b => JsonPrimitive.Of(b),
            byte[] bytes => JsonBytes.Of(bytes),
            JsonElement jsonElement => jsonElement,
            IToPandoraJson toPandoraJson => toPandoraJson.ToJson(),
            IEnumerable<object?> enumerable => JsonArray.Of(enumerable),
            IDictionary<string, object?> dictionary => JsonObject.Of(dictionary),
            _ => throw new NotSupportedException("This type does not support auto conversion to JsonElement. Implement IToPandoraJson or convert it manually.")
        };
        
    }





}