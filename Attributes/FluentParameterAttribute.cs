using System.Diagnostics;

namespace PandoraLib.Attributes;

[AttributeUsage(AttributeTargets.Method)]
[Conditional("FLUENT_PARAMETER_INFO")]
public sealed class FluentParameterAttribute(string description = "This method returns you parameter back.") : Attribute
{
    public string Description { get; init; } = description;
}