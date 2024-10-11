using System.Globalization;
using System.Numerics;
using PandoraLib.Extensions;

namespace PandoraLib.Models;

public class Number
{
    private readonly object _value;
    private Number(object value)
    {
        _value = value;
    }
    
    public T GetAsValue<T>() => _value.Cast<T>();
    public short GetAsShort() => _value.Cast<short>();
    public int GetAsInt() => _value.Cast<int>();
    public long GetAsLong() => _value.Cast<long>();
    public ushort GetAsUShort() => _value.Cast<ushort>();
    public uint GetAsUInt() => _value.Cast<uint>();
    public ulong GetAsULong() => _value.Cast<ulong>();
    public float GetAsFloat() => _value.Cast<float>();
    public double GetAsDouble() => _value.Cast<double>();
    public decimal GetAsDecimal() => _value.Cast<decimal>();

    public static Number Of<T>(T value) where T : INumberBase<T>
    {
        return new Number(value);
    }
    
    public static Number Parse(string value)
    {
        if (value.Contains('.'))
        {
            var v = double.Parse(value);
            return Of(v < float.MaxValue ? (float)v : v);
        } else
        {
            var v =long.Parse(value);
            return Of(v < int.MaxValue ? (int)v : v);
        }
    }

    public override string ToString()
    {
        return _value switch
        {
            float f => f.ToString(CultureInfo.InvariantCulture),
            double d => d.ToString(CultureInfo.InvariantCulture),
            decimal dec => dec.ToString(CultureInfo.InvariantCulture),
            _ => _value.ToString()
        } ?? throw new InvalidOperationException();
    }
}