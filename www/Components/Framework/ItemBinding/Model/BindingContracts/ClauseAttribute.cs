using System;
using Sitecore.Data.Items;

namespace ItemBinding.Model.BindingContracts
{
  /// <summary>
  /// Represents the base abstract class for creating concrete binding contract clause attributes.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
  public abstract class ClauseAttribute : Attribute
  {
    /// <summary>
    /// Determines whether the specified item complies with the requirements set by the clause attribute.
    /// </summary>
    /// <param name="item">The item to test for clause compliance.</param>
    /// <returns><c>true</c> if the item complies with the clause requirements; otherwise <c>false</c></returns>
    public abstract Boolean IsComplied(Item item);

    /// <summary>
    /// Asserts that the specified item complies with the requirements set by the clause attribute.
    /// </summary>
    /// <param name="item">The item to test for clause compliance.</param>
    /// <param name="type">The model class type that the item is being bound to.</param>
    /// <exception cref="System.Exception">This method will throw a specialized error if the item does not comply with the clause requirements.</exception>
    public abstract void Assert(Item item, Type type);
  }
}