using System;
using ItemBinding.Model.BindingContracts;
using Sitecore.Data.Items;

namespace ItemBinding.Model
{
  /// <summary>
  /// Exposes the methods used by the model factory to facilitate item model binding.  
  /// </summary>
  public interface IModelFactory : ICloneable
  {
    /// <summary>
    /// Binds the specified item to the model class specified by T.
    /// </summary>
    /// <typeparam name="T">The model class to bind the specified item to.</typeparam>
    /// <param name="item">The item to bind tom the model class T.</param>
    /// <returns>New instance of the model class T bound to the specified item.</returns>
    T Create<T>(Item item) where T : class;

    /// <summary>
    /// Binds the specified item to the model class specified by the type parameter.
    /// </summary>
    /// <param name="item">The item to bind tom the model class.</param>
    /// <param name="type">The type of the model class.</param>
    /// <returns>New instance of the model class specified by the type parameter bound to the specified item.</returns>
    Object Create(Item item, Type type);

    /// <summary>
    /// Gets or sets the binding contract used by the Create method to assert that the item being bound to the model class is compliant.
    /// </summary>
    /// <value>The binding contract.</value>
    IBindingContract BindingContract { get; set; }
  }
}