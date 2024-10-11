using PandoraLib.Attributes;
using PandoraLib.Data.Stream.Impl;

namespace PandoraLib.Data.Json.Impl;

[Warning("Using this class will break support for normal JSON parsing.")]
public class JsonBytes: JsonElement
{
    public byte[] Value { get; }
    private JsonBytes(byte[] value)
    {
        Value = value;
    }

    public static JsonBytes Of(byte b) => Of([b]);

    public static JsonBytes Of(byte[] b)
    {
        return new JsonBytes(b);
    }
    
    public override ArrayStream<byte> Stream()
    {
        return ArrayStream<byte>.Of(Value);
    }
    
    public static JsonBytes Parse(string jsonString)
    {
        return JsonParser.ParseBytes(jsonString);
    }
    
    public override string ToJsonString()
    {
        return $"´{Convert.ToBase64String(Value)}´";
    }

}