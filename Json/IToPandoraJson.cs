using PandoraLib.Json.Impl;
using PandoraLib.Utils.Extensions;

namespace PandoraLib.Json;

public interface IToPandoraJson
{
    JsonElement ToJson();

    JsonObject ToJsonObject() => ToJson().Cast<JsonObject>();
    JsonArray ToJsonArray() => ToJson().Cast<JsonArray>();
    JsonPrimitive ToJsonPrimitive() => ToJson().Cast<JsonPrimitive>();
    JsonNull ToJsonNull() => ToJson().Cast<JsonNull>();
    JsonBytes ToJsonBytes() => ToJson().Cast<JsonBytes>();
    String ToJsonString() => ToJson().ToJsonString();
    
}