﻿using PandoraLib.Attributes;
using PandoraLib.Stream.Extensions;
using PandoraLib.Stream.Impl;

namespace PandoraLib.Json.Impl;

public class JsonArray(IEnumerable<JsonElement> list) : JsonElement
{
    private readonly List<JsonElement> _list = list.ToList();

    public JsonArray() : this([]) { }

    public override ArrayStream<JsonElement> Stream()
    {
        return ArrayStream<JsonElement>.Of(_list);
    }
    
    [Fluent]
    public JsonArray Add(JsonElement json)
    {
        _list.Add(json);
        return this;
    }
    
    public JsonElement Get(int i) => _list[i];

    public bool Remove(JsonElement json) => _list.Remove(json);

    public int Size() => _list.Count;

    public override string ToJsonString()
    {
        return $"[{_list.Stream(e => e.ToJsonString()).Join(", ")}]";
    }

    public static JsonArray Of<T>(IEnumerable<T> list)
    {
        return new JsonArray(list.Stream(t => JsonElement.Of(t)).ToArray());
    }

    public static JsonArray Parse(string jsonString)
    {
        return JsonParser.ParseArray(jsonString);
    }

    public JsonElement this[int i] => Get(i);
}