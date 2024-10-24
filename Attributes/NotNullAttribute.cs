using System.Diagnostics;

namespace PandoraLib.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Parameter)]
[Conditional("NOT_NULL_INFO")]
public sealed class NotNullAttribute(string description = "This method returns not null or this parameter, field can't be null.") : Attribute
{
    public string Description { get; init; } = description;
}