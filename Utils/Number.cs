using System.Globalization;
using System.Numerics;
using PandoraLib.Utils.Extensions;

namespace PandoraLib.Utils;

public class Number
{
    private readonly object _value;

    private Number(object value)
    {
        _value = value;
    }

    public T GetAsValue<T>()
    {
        return _value switch
        {
            short s when typeof(T) == typeof(short) => GetAsShort().Cast<T>(),
            int i when typeof(T) == typeof(int) => GetAsInt().Cast<T>(),
            long l when typeof(T) == typeof(long) => GetAsLong().Cast<T>(),
            ushort us when typeof(T) == typeof(ushort) => GetAsUShort().Cast<T>(),
            uint ui when typeof(T) == typeof(uint) => GetAsUInt().Cast<T>(),
            ulong ul when typeof(T) == typeof(ulong) => GetAsULong().Cast<T>(),
            float f when typeof(T) == typeof(float) => GetAsFloat().Cast<T>(),
            double d when typeof(T) == typeof(double) => GetAsDouble().Cast<T>(),
            decimal dec when typeof(T) == typeof(decimal) => GetAsDecimal().Cast<T>(),
            BigInteger bi when typeof(T) == typeof(BigInteger) => GetAsBigInteger().Cast<T>(),
            _ => _value.Cast<T>()
        };
    }

    public short GetAsShort()
    {
        if (_value is short s) return s;
        throw new InvalidCastException("Cannot cast to short");
    }

    public int GetAsInt()
    {
        return _value switch
        {
            int i => i,
            short s => int.Parse(s.ToString()),
            _ => throw new InvalidCastException("Cannot cast to int")
        };
    }

    public long GetAsLong()
    {
        return _value switch
        {
            long l => l,
            int i => long.Parse(i.ToString()),
            short s => long.Parse(s.ToString()),
            _ => throw new InvalidCastException("Cannot cast to long")
        };
    }

    public ushort GetAsUShort()
    {
        return _value switch
        {
            ushort us => us,
            _ => throw new InvalidCastException("Cannot cast to ushort")
        };
    }

    public uint GetAsUInt()
    {
        return _value switch
        {
            uint ui => ui,
            ushort us => uint.Parse(us.ToString()),
            _ => throw new InvalidCastException("Cannot cast to uint")
        };
    }

    public ulong GetAsULong()
    {
        return _value switch
        {
            ulong ul => ul,
            uint ui => ulong.Parse(ui.ToString()),
            ushort us => ulong.Parse(us.ToString()),
            _ => throw new InvalidCastException("Cannot cast to ulong")
        };
    }

    public float GetAsFloat()
    {
        return _value switch
        {
            float f => f,
            _ => throw new InvalidCastException("Cannot cast to float")
        };
    }

    public double GetAsDouble()
    {
        return _value switch
        {
            double d => d,
            float f => double.Parse(f.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
            _ => throw new InvalidCastException("Cannot cast to double")
        };
    }

    public decimal GetAsDecimal()
    {
        return _value switch
        {
            decimal dec => dec,
            double d => decimal.Parse(d.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
            _ => throw new InvalidCastException("Cannot cast to decimal")
        };
    }

    public BigInteger GetAsBigInteger()
    {
        return _value switch
        {
            BigInteger bi => bi,
            short s => new BigInteger(s),
            int i => new BigInteger(i),
            long l => new BigInteger(l),
            ushort us => new BigInteger(us),
            uint ui => new BigInteger(ui),
            ulong ul => new BigInteger(ul),
            _ => throw new InvalidCastException("Cannot cast to BigInteger")
        };
    }

    public static Number Of<T>(T value) where T : INumberBase<T>
    {
        return new Number(value);
    }

    public static Number Parse(string value)
    {
        if (value.Contains('E'))
        {
            var v = BigInteger.Parse(value, NumberStyles.AllowExponent | NumberStyles.Float, CultureInfo.InvariantCulture);
            return Of(v);
        }
        if (value.Contains('.'))
        {
            var v = double.Parse(value, CultureInfo.InvariantCulture);
            return Of(v < float.MaxValue ? float.Parse(value, CultureInfo.InvariantCulture) : (float)v);
        }
        else
        {
            if (!BigInteger.TryParse(value, CultureInfo.InvariantCulture, out var v)) throw new FormatException();
            return v switch
            {
                _ when v <= short.MaxValue && v >= short.MinValue => Of((short) v),
                _ when v <= int.MaxValue && v >= int.MinValue => Of((int) v),
                _ when v <= long.MaxValue && v >= long.MinValue => Of((long) v),
                _ when v <= ushort.MaxValue && v >= ushort.MinValue => Of((ushort) v),
                _ when v <= uint.MaxValue && v >= uint.MinValue => Of((uint) v),
                _ when v <= ulong.MaxValue && v >= ulong.MinValue => Of((ulong) v),
                _ => Of(v)
            };
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