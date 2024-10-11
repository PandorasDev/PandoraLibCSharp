using System.Runtime.InteropServices.JavaScript;
using System.Text;
using PandoraLib.Data.Json;
using PandoraLib.Data.Json.Impl;
using PandoraLib.Models;

namespace PandoraLibTests.Data.Json;

public class JsonParserTest
{
    private static readonly string JsonObjectText = "{\"name\":\"John\",\"age\":30,\"car\":null}";
    private static readonly string JsonArrayText = "[\"Ford\", \"BMW\", \"Fiat\"]";
    private static readonly string JsonPrimitiveText = "42";
    private static readonly string JsonPrimitiveText2 = "\"42\"";
    private static readonly string JsonPrimitiveText3 = "true";
    private static readonly string JsonPrimitiveText4 = "\"true\"";
    private static readonly string JsonPrimitiveText5 = "\"Hello, World!\"";
    private static readonly string JsonPrimitiveText6 = "'H'";
    private static readonly string JsonPrimitiveText7 = "123.45";
    private static readonly string JsonByteOriginalText =  "Will this work 100%?";
    private static readonly byte[] JsonByteBytes = Encoding.UTF8.GetBytes(JsonByteOriginalText);
    private static readonly string JsonByteText = $"´{Convert.ToBase64String(JsonByteBytes)}´";
    

    [Test]
    public void TestParsingOfJsonObject()
    {
        var json = JsonParser.Parse(JsonObjectText);
        Assert.Multiple(() =>
        {
            Assert.That(json.IsJsonObject(), Is.True, "Expected JSON object");
            Assert.That(json.IsJsonArray(), Is.False, "Expected not a JSON array");
            Assert.That(json.IsJsonPrimitive(), Is.False, "Expected not a JSON primitive");
            Assert.That(json.IsJsonBytes(), Is.False, "Expected not JSON bytes");
            Assert.That(json.IsJsonNull(), Is.False, "Expected not JSON null");
        });

        JsonObject? jsonObject = null;
        Assert.DoesNotThrow(() => jsonObject = json.AsJsonObject(), "Expected no exception when casting to JsonObject");
        Assert.Throws<InvalidCastException>(() => json.AsJsonArray(),
            "Expected InvalidCastException when casting to JsonArray");
        Assert.That(jsonObject, Is.Not.Null, "Expected non-null JsonObject");
        Assert.Multiple(() =>
        {
            Assert.That(jsonObject!["name"], Is.InstanceOf<JsonPrimitive>(), "Expected 'name' to be a JsonPrimitive");
            Assert.That(jsonObject!["car"], Is.InstanceOf<JsonNull>(), "Expected 'car' to be a JsonNull");
            Assert.That(jsonObject!.ContainsKey("age"), Is.True, "Expected 'age' key to be present");
            Assert.That(jsonObject!["age"], Is.InstanceOf<JsonPrimitive>(), "Expected 'age' to be a JsonPrimitive");
        });
    }

    [Test]
    public void TestParsingOfJsonArray()
    {
        var json = JsonParser.Parse(JsonArrayText);
        Assert.Multiple(() =>
        {
            Assert.That(json.IsJsonArray(), Is.True, "Expected JSON array");
            Assert.That(json.IsJsonObject(), Is.False, "Expected not a JSON object");
            Assert.That(json.IsJsonPrimitive(), Is.False, "Expected not a JSON primitive");
            Assert.That(json.IsJsonBytes(), Is.False, "Expected not JSON bytes");
            Assert.That(json.IsJsonNull(), Is.False, "Expected not JSON null");
        });

        JsonArray? jsonArray = null;
        Assert.DoesNotThrow(() => jsonArray = json.AsJsonArray(), "Expected no exception when casting to JsonArray");
        Assert.Throws<InvalidCastException>(() => json.AsJsonObject(),
            "Expected InvalidCastException when casting to JsonObject");
        Assert.That(jsonArray, Is.Not.Null, "Expected non-null JsonArray");
        Assert.Multiple(() =>
        {
            Assert.That(jsonArray![0], Is.InstanceOf<JsonPrimitive>(), "Expected first element to be a JsonPrimitive");
            Assert.That(jsonArray![1], Is.InstanceOf<JsonPrimitive>(), "Expected second element to be a JsonPrimitive");
            Assert.That(jsonArray![2], Is.InstanceOf<JsonPrimitive>(), "Expected third element to be a JsonPrimitive");
        });
    }

    [Test]
    public void TestParsingJsonPrimitive()
    {
        var json = JsonParser.Parse(JsonPrimitiveText);
        Assert.Multiple(() =>
        {
            Assert.That(json.IsJsonPrimitive(), Is.True, "Expected JSON primitive");
            Assert.That(json.IsJsonObject(), Is.False, "Expected not a JSON object");
            Assert.That(json.IsJsonArray(), Is.False, "Expected not a JSON array");
            Assert.That(json.IsJsonBytes(), Is.False, "Expected not JSON bytes");
            Assert.That(json.IsJsonNull(), Is.False, "Expected not JSON null");
        });

        JsonPrimitive? jsonPrimitive = null;
        Assert.DoesNotThrow(() => jsonPrimitive = json.AsJsonPrimitive(), "Expected no exception when casting to JsonPrimitive");
        Assert.Throws<InvalidCastException>(() => json.AsJsonObject(),
            "Expected InvalidCastException when casting to JsonObject");
        Assert.That(jsonPrimitive, Is.Not.Null, "Expected non-null JsonPrimitive");
        Assert.Multiple(() =>
        {
            Assert.That(jsonPrimitive!.GetAsNumber().GetAsInt(), Is.EqualTo(42), "Expected value to be 42");
            Assert.Throws<InvalidCastException>(() => jsonPrimitive.GetAsBool(), "Expected InvalidCastException when getting as bool");
        });
    }

    [Test]
    public void TestParsingJsonPrimitive2()
    {
        var json = JsonParser.Parse(JsonPrimitiveText2);

        JsonPrimitive? jsonPrimitive = null;
        Assert.DoesNotThrow(() => jsonPrimitive = json.AsJsonPrimitive(), "Expected no exception when casting to JsonPrimitive");
        Assert.That(jsonPrimitive, Is.Not.Null, "Expected non-null JsonPrimitive");
        Assert.That(jsonPrimitive!.GetAsString(), Is.EqualTo("42"), "Expected value to be '42'");
    }
    
    [Test]
    public void TestParsingJsonPrimitive3()
    {
        var json = JsonParser.Parse(JsonPrimitiveText3);

        JsonPrimitive? jsonPrimitive = null;
        Assert.DoesNotThrow(() => jsonPrimitive = json.AsJsonPrimitive(), "Expected no exception when casting to JsonPrimitive");
        Assert.That(jsonPrimitive, Is.Not.Null, "Expected non-null JsonPrimitive");
        Assert.That(jsonPrimitive!.GetAsBool(), Is.True, "Expected value to be true");
    }
    
    [Test]
    public void TestParsingJsonPrimitive4()
    {
        var json = JsonParser.Parse(JsonPrimitiveText4);

        JsonPrimitive? jsonPrimitive = null;
        Assert.DoesNotThrow(() => jsonPrimitive = json.AsJsonPrimitive(), "Expected no exception when casting to JsonPrimitive");
        Assert.That(jsonPrimitive, Is.Not.Null, "Expected non-null JsonPrimitive");
        Assert.That(jsonPrimitive!.GetAsString(), Is.EqualTo("true"), "Expected value to be 'true'");
    }
    
    [Test]
    public void TestParsingJsonPrimitive5()
    {
        var json = JsonParser.Parse(JsonPrimitiveText5);

        JsonPrimitive? jsonPrimitive = null;
        Assert.DoesNotThrow(() => jsonPrimitive = json.AsJsonPrimitive(), "Expected no exception when casting to JsonPrimitive");
        Assert.That(jsonPrimitive, Is.Not.Null, "Expected non-null JsonPrimitive");
        Assert.That(jsonPrimitive!.GetAsString(), Is.EqualTo("Hello, World!"), "Expected value to be 'Hello, World!'");
    }
    
    [Test]
    public void TestParsingJsonPrimitive6()
    {
        var json = JsonParser.Parse(JsonPrimitiveText6);

        JsonPrimitive? jsonPrimitive = null;
        Assert.DoesNotThrow(() => jsonPrimitive = json.AsJsonPrimitive(), "Expected no exception when casting to JsonPrimitive");
        Assert.That(jsonPrimitive, Is.Not.Null, "Expected non-null JsonPrimitive");
        Assert.That(jsonPrimitive!.GetAsChar(), Is.EqualTo('H'), "Expected value to be 'H'");
    }
    
    [Test]
    public void TestParsingJsonPrimitive7()
    {
        var json = JsonParser.Parse(JsonPrimitiveText7);

        JsonPrimitive? jsonPrimitive = null;
        Assert.DoesNotThrow(() => jsonPrimitive = json.AsJsonPrimitive(), "Expected no exception when casting to JsonPrimitive");
        Assert.That(jsonPrimitive, Is.Not.Null, "Expected non-null JsonPrimitive");
        Assert.That(jsonPrimitive!.GetAsNumber(), Is.InstanceOf<Number>(), "Expected value to be a Number");
    }

    [Test]
    public void TestParsingJsonByte()
    {
        var json = JsonParser.Parse(JsonByteText);
        
        Assert.Multiple(() =>
        {
            Assert.That(json.IsJsonBytes(), Is.True, "Expected JSON bytes");
            Assert.That(json.IsJsonObject(), Is.False, "Expected not a JSON object");
            Assert.That(json.IsJsonArray(), Is.False, "Expected not a JSON array");
            Assert.That(json.IsJsonPrimitive(), Is.False, "Expected not a JSON primitive");
            Assert.That(json.IsJsonNull(), Is.False, "Expected not JSON null");
        });
        
        JsonBytes? jsonBytes = null;
        Assert.DoesNotThrow(() => jsonBytes = json.AsJsonBytes(), "Expected no exception when casting to JsonBytes");
        Assert.That(jsonBytes, Is.Not.Null, "Expected non-null JsonBytes");
        Assert.That(jsonBytes!.Value, Is.EqualTo(JsonByteBytes), "Expected value to be equal to original bytes");
        Assert.That(Encoding.UTF8.GetString(jsonBytes!.Value), Is.EqualTo(JsonByteOriginalText), "Expected value to be equal to original text");
    }
}