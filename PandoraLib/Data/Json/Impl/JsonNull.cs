using PandoraLib.Data.Stream;
using PandoraLib.Data.Stream.Impl;

namespace PandoraLib.Data.Json.Impl;

public class JsonNull: JsonElement
{
    public static readonly JsonNull Instance = new ();

    private JsonNull()
    {
    }

    public override ArrayStream<JsonNull> Stream()
    {
        throw new NotSupportedException("JsonNull cannot be streamed");
    }

    public static JsonNull Parse(string jsonString)
    {
        return JsonParser.ParseNull(jsonString);
    }
    
    public override string ToJsonString()
    {
        return "null";
    }
}