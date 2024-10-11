namespace PandoraLib.Extensions;

public static class CasterExtension
{
    public static T? CastWithNull<T>(this object obj)
    {
        return obj is T cObj ? cObj : default;
    }
    
    public static T Cast<T>(this object obj)
    {
        return obj is T cObj ? cObj : throw new InvalidCastException($"Cannot cast {obj.GetType().Name} to {typeof(T).Name}");
    }
}