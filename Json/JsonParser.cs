using System.Text;
using PandoraLib.Attributes;
using PandoraLib.Json.Impl;
using PandoraLib.Utils;

namespace PandoraLib.Json;

[Warning("This parser does not support JSON5 parsing yet. Only standard and Pandora JSON is supported for now.")]
public static class JsonParser
{
    public static JsonElement Parse(string jsonString)
    {
        var trimmed = jsonString.Trim();
        return trimmed switch
        {
            _ when trimmed.StartsWith('{') => ParseObject(trimmed),
            _ when trimmed.StartsWith('[') => ParseArray(trimmed),
            _ when trimmed.StartsWith('"') => ParseString(trimmed),
            _ when trimmed.StartsWith('\'') => ParseChar(trimmed),
            _ when trimmed.StartsWith('´') => ParseBytes(trimmed),
            "null" => JsonNull.Instance,
            "true" or "false" => JsonPrimitive.Of(bool.Parse(trimmed)),
            _ => JsonPrimitive.Of(Number.Parse(trimmed))
        };
    }

    public static JsonElement ParseObject(object? obj)
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
            byte b => JsonBytes.Of(b),
            byte[] bytes => JsonBytes.Of(bytes),
            JsonElement jsonElement => jsonElement,
            IToPandoraJson toPandoraJson => toPandoraJson.ToJson(),
            IEnumerable<object?> enumerable => JsonArray.Of(enumerable),
            IDictionary<string, object?> dictionary => JsonObject.Of(dictionary),
            _ => throw new NotSupportedException("This type does not support auto conversion to JsonElement. Implement IToPandoraJson or convert it manually.")
        };
    }
    
    public static JsonNull ParseNull(string jsonString)
    {
        if (jsonString == "null") return JsonNull.Instance;
        throw new ArgumentException("Invalid JsonNull format");
    }

    public static JsonPrimitive ParsePrimitive(string jsonString)
    {
        var trimmed = jsonString.Trim();
        return trimmed switch
        {
            _ when trimmed.StartsWith('"') => ParseString(trimmed),
            _ when trimmed.StartsWith('\'') => ParseChar(trimmed),
            "true" or "false" => JsonPrimitive.Of(bool.Parse(trimmed)),
            _ => JsonPrimitive.Of(Number.Parse(trimmed))
        };
    }

    public static JsonPrimitive ParseString(string jsonString)
    {
        return JsonPrimitive.Of(jsonString.Substring(1, jsonString.Length - 2));
    }

    public static JsonPrimitive ParseChar(string jsonString)
    {
        if (jsonString.StartsWith('\'') && jsonString.EndsWith('\'') && jsonString.Length == 3)
        {
            return JsonPrimitive.Of(jsonString[1]);
        }
        throw new ArgumentException("Invalid JsonChar format");
    }

    public static JsonBytes ParseBytes(string jsonString)
    {
        if (jsonString.StartsWith('´') && jsonString.EndsWith('´') && jsonString.Length > 2)
        {
            return JsonBytes.Of(Convert.FromBase64String(jsonString.Substring(1, jsonString.Length - 2)));
        }
        throw new ArgumentException("Invalid JsonBytes format");
    }

    public static JsonObject ParseObject(string jsonString)
    {
        var jsonObject = new JsonObject();
        string content = jsonString.Substring(1, jsonString.Length - 2).Trim();
        var elements = SplitElements(content, ',');
        
        foreach (var element in elements)
        {
            var (key, value) = SplitKeyValue(element);
            jsonObject.Add(key.Trim('"'), Parse(value));
        }
        return jsonObject;
    }

    public static JsonArray ParseArray(string jsonString)
    {
        var jsonArray = new JsonArray();
        string content = jsonString.Substring(1, jsonString.Length - 2).Trim();
        var elements = SplitElements(content, ',');

        foreach (var element in elements)
        {
            jsonArray.Add(Parse(element));
        }
        return jsonArray;
    }

    public static List<string> SplitElements(string content, char delimiter)
    {
        var elements = new List<string>();
        int bracketCount = 0;
        var currentElement = new StringBuilder();

        foreach (var c in content)
        {
            if (c is '{' or '[') bracketCount++;
            if (c is '}' or ']') bracketCount--;

            if (c == delimiter && bracketCount == 0)
            {
                elements.Add(currentElement.ToString().Trim());
                currentElement.Clear();
            }
            else
            {
                currentElement.Append(c);
            }
        }

        if (currentElement.Length > 0)
        {
            elements.Add(currentElement.ToString().Trim());
        }

        return elements;
    }

    private static (string key, string value) SplitKeyValue(string pair)
    {
        int colonIndex = pair.IndexOf(':');
        string key = pair.Substring(0, colonIndex).Trim();
        string value = pair.Substring(colonIndex + 1).Trim();
        return (key, value);
    }
    
}
