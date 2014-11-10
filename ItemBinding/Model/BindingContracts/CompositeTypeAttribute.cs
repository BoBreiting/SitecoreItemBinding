using System;

namespace ItemBinding.Model.BindingContracts
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
  public class CompositeTypeAttribute : Attribute
  {
    public CompositeTypeAttribute(Type type)
    {
      Type = type;
    }

    public Type Type { get; private set; }
  }
}