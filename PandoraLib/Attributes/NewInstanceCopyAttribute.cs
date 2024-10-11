using System.Diagnostics;

namespace PandoraLib.Attributes;

[AttributeUsage(AttributeTargets.Method)]
[Conditional("NEW_INSTANCE_COPY_INFO")]
public sealed class NewInstanceCopyAttribute(string description = "This method returns a new copy of it self") : Attribute
{
    public string Description { get; init; } = description;
}