using System.Diagnostics;

namespace PandoraLib.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
[Conditional("WARNING_INFO")]
public sealed class WarningAttribute(string description) : Attribute
{
    public string Description { get; init; } = description;
}