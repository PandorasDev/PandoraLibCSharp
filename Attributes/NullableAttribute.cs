using System.Diagnostics;

namespace PandoraLib.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Parameter)]
[Conditional("NULLABLE_INFO")]
public sealed class NullableAttribute(string description = "This method can returns null or this parameter, field can be null.") : Attribute
{
    public string Description { get; init; } = description;
}