using System;

namespace ItemBinding.Model.BindingContracts
{
  /// <summary>
  /// Represents a clause attribute for specifying composite type members.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
  public class CompositeTypeAttribute : Attribute
  {
    public CompositeTypeAttribute(Type type)
    {
      Type = type;
    }

    /// <summary>
    /// Gets the type of the composite member.
    /// </summary>
    /// <value>
    /// The type of the composite member.
    /// </value>
    public Type Type { get; private set; }
  }
}