using System.ComponentModel.DataAnnotations;
using System.Numerics;
using PandoraLib.Data.Stream.Impl;
using PandoraLib.Extensions;
using PandoraLib.Models;

namespace PandoraLib.Data.Json.Impl;

public class JsonPrimitive: JsonElement
{
    private readonly object _value;
    private JsonPrimitive(string value) => _value = value;
    private JsonPrimitive(Number value) => _value = value;
    private JsonPrimitive(char value) => _value = value;
    private JsonPrimitive(bool value) => _value = value;

    public string GetAsString() => _value.Cast<string>();
    public Number GetAsNumber() => _value.Cast<Number>();
    public char GetAsChar() => _value.Cast<char>();
    public bool GetAsBool() => _value.Cast<bool>();

    public static JsonPrimitive Of(string value) => new JsonPrimitive(value);
    public static JsonPrimitive Of(Number value) => new JsonPrimitive(value);
    public static JsonPrimitive Of(char value) => new JsonPrimitive(value);
    public static JsonPrimitive Of(bool value) => new JsonPrimitive(value);
    public static JsonPrimitive Of<T>(T value) where T : INumberBase<T>
    {
        return Of(Number.Of(value));
    }

    public override ArrayStream<object> Stream()
    {
        return ArrayStream<object>.Of([_value]);
    }
    
    public static JsonPrimitive Parse(string jsonString)
    {
        return JsonParser.ParsePrimitive(jsonString);
    }

    public override string ToJsonString()
    {
        return _value switch
        {
            string s => $"\"{s}\"",
            Number n => n.ToString(),
            char c => $"'{c}'",
            bool b => b.ToString().ToLower(),
            _ => throw new NotSupportedException($"Unsupported type: {_value.GetType()}")
        };
    }
    
    public override string ToString()
    {
        return _value.ToString() ?? throw new InvalidOperationException();
    }
}