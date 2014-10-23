using System;
using Sitecore.Data.Items;

namespace ItemBinding.Model.BindingContracts
{
  /// <summary>
  /// Exposes the methods used by the binding contract to facilitate contract compliance checking.  
  /// </summary>
  public interface IBindingContract
  {
    /// <summary>
    /// Determines whether the specified item is compliant with the binding contract.
    /// </summary>
    /// <typeparam name="T">The model class used to determine the binding contract to test against.</typeparam>
    /// <param name="item">The item that should be tested for compliance.</param>
    /// <returns><c>true</c> if the item complies with the binding contract; otherwise <c>false</c></returns>
    Boolean IsCompliant<T>(Item item) where T : class;

    /// <summary>
    /// Determines whether the specified item is compliant with the binding contract.
    /// </summary>
    /// <param name="item">The item that should be tested for compliance.</param>
    /// <param name="type">The type of the model class used to determine the binding contract to test against.</param>
    /// <returns><c>true</c> if the item complies with the binding contract; otherwise <c>false</c></returns>
    Boolean IsCompliant(Item item, Type type);

    /// <summary>
    /// Asserts that the specified item is compliant with the binding contract.
    /// </summary>
    /// <typeparam name="T">The model class that the item is being bound to.</typeparam>
    /// <param name="item">The item that should be tested for compliance.</param>
    void Assert<T>(Item item) where T : class;

    /// <summary>
    /// Asserts that the specified item is compliant with the binding contract.
    /// </summary>
    /// <param name="item">The item that should be tested for compliance.</param>
    /// <param name="type">The type of the model class that the item is being bound to.</param>
    void Assert(Item item, Type type);
  }
}