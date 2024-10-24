using System.Diagnostics;

namespace PandoraLib.Attributes;

[AttributeUsage(AttributeTargets.Method)]
[Conditional("FLUENT_INFO")]
public sealed class FluentAttribute(string description = "This method of the class returns itself.") : Attribute
{
    public string Description { get; init; } = description;
}