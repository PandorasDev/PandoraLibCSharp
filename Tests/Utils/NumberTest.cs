using PandoraLib.Utils;

namespace PandoraLib.Test.Utils;

[TestFixture]
public class NumberTest
{
    [Test]
    public void Parse_ValidString_ReturnsCorrectNumber()
    {
        var i = Number.Parse("123");
        var f = Number.Parse("123.45");
        var l = Number.Parse("9223372036854775807");
        var ul = Number.Parse("18446744073709551615");
        Assert.Multiple(() =>
        {
            Assert.That(i.GetAsInt(), Is.EqualTo(123), "Expected int value: 123");
            Assert.That(f.GetAsFloat(), Is.EqualTo(123.45f), "Expected float value: 123.45");
            Assert.That(l.GetAsLong(), Is.EqualTo(long.MaxValue), $"Expected long value: {long.MaxValue}");
            Assert.That(ul.GetAsULong(), Is.EqualTo(ulong.MaxValue), $"Expected ulong value: {ulong.MaxValue}");
        });
    }

    [Test]
    public void GetAsValue_ValidType_ReturnsCorrectValue()
    {
        var number = Number.Of(123.45M);
        Assert.That(number.GetAsValue<decimal>(), Is.EqualTo(123.45M), "Expected decimal value");
    }

    [Test]
    public void ToString_ReturnsCorrectString()
    {
        var number = Number.Of(123.45M);
        Assert.That(number.ToString(), Is.EqualTo("123.45"), "Expected correct string representation");
    }

    [Test]
    public void Parse_InvalidString_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() => Number.Parse("invalid"), "Expected FormatException");
    }

    [Test]
    public void Parse_OverflowValue_ThrowsOverflowException()
    {
        Assert.Throws<OverflowException>(() => Number.Parse("1E+1000"), "Expected OverflowException");
    }
}