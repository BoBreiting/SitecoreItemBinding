using System;
using ItemBinding.Model.BindingContracts;
using Sitecore.Data.Items;

namespace ItemBinding.Model
{
  /// <summary>
  /// Represents a model factory for binding items to model classes.
  /// </summary>
  public class ModelFactory : IModelFactory
  {
    /// <summary>
    /// Binds the specified item to the model class specified by T.
    /// </summary>
    /// <typeparam name="T">The model class to bind the specified item to.</typeparam>
    /// <param name="item">The item to bind tom the model class T.</param>
    /// <returns>New instance of the model class T bound to the specified item.</returns>
    /// <exception cref="MissingItemConstructorException"></exception>
    public virtual T Create<T>(Item item) where T : class
    {
      Type type = typeof (T);
      return (T) Create(item, type);
    }

    /// <summary>
    /// Binds the specified item to the model class specified by the type parameter.
    /// </summary>
    /// <param name="item">The item to bind tom the model class.</param>
    /// <param name="type">The type of the model class.</param>
    /// <returns>New instance of the model class specified by the type parameter bound to the specified item.</returns>
    public virtual Object Create(Item item, Type type)
    {
      BindingContract.Assert(item, type);
      try
      {
        return Activator.CreateInstance(type, new Object[] { item });
      }
      catch (MissingMethodException)
      {
        throw new MissingItemConstructorException(type);
      }
    }

    /// <summary>
    /// Gets or sets the binding contract used by the Create method to assert that the item being bound to the model class is compliant.
    /// </summary>
    /// <value>The binding contract.</value>
    public virtual IBindingContract BindingContract
    {
      get { return _bindingContract ?? (_bindingContract = new AttributeBasedBindingContract()); }
      set { _bindingContract = value; }
    }

    /// <summary>
    /// Creates a new model factory that is a copy of the current instance.
    /// </summary>
    /// <returns>A new model factory that is a copy of this instance.</returns>
    public Object Clone()
    {
      return MemberwiseClone();
    }

    private IBindingContract _bindingContract;
  }
}