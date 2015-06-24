using System;
using ItemBinding.Application;
using ItemBinding.Model;
using Sitecore.Data.Items;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Helpers;
using Sitecore.StringExtensions;

namespace ItemBinding.Mvc.Infrastructure
{
  /// <summary>
  /// ModelLocator pipeline step for resolving model class instances in the mvc.getModel pipeline
  /// </summary>
  public class ModelLocator : Sitecore.Mvc.Presentation.ModelLocator
  {
    /// <summary>
    /// Gets a model class instance from the specified model bound to the specified sourceItem.
    /// </summary>
    /// <param name="model">The model reference as provided by the rendering information.</param>
    /// <param name="dataSource">The data source item to bind to the model class instance.</param>
    /// <param name="throwIfNotFound">if set to <c>true</c> an error will be thrown if the model class cannot be instantiated.</param>
    /// <returns>New instance of the model class specified by the model parameter bound to the specified dataSource item.</returns>
    public Object GetModel(String model, Item dataSource, Boolean throwIfNotFound)
    {
      if (model.IsWhiteSpaceOrNull())
        return null;

      if (TypeHelper.LooksLikeTypeName(model))
        return GetModelFromTypeName(model, model, dataSource, throwIfNotFound);

      Item itemContainingModel = GetItemContainingModel(model, throwIfNotFound);
      if (itemContainingModel == null)
        return null;

      String modelTypeName = GetModelTypeName(itemContainingModel, throwIfNotFound);
      if (modelTypeName.IsWhiteSpaceOrNull())
        return null;

      return GetModelFromTypeName(modelTypeName, model, dataSource, throwIfNotFound);
    }

    /// <summary>
    /// Gets a model class instance from the specified type bound to the specified sourceItem.
    /// </summary>
    /// <param name="typeName">The name of the type.</param>
    /// <param name="model">The model reference as provided by the rendering information.</param>
    /// <param name="sourceItem">The source item to bind to the model class instance.</param>
    /// <param name="throwOnTypeCreationError">if set to <c>true</c> an error will be thrown if the model class cannot be instantiated.</param>
    /// <returns>New instance of the model class specified by the typeName parameter bound to the specified sourceItem.</returns>
    /// <exception cref="System.InvalidOperationException">Could not locate type '{typeName}'. Model reference: '{model}'</exception>
    protected Object GetModelFromTypeName(String typeName, String model, Item sourceItem, Boolean throwOnTypeCreationError)
    {
      Type type = TypeHelper.GetType(typeName);

      if (type == null)
      {
        if (throwOnTypeCreationError)
        {
          throw new InvalidOperationException("Could not locate type '{0}'. Model reference: '{1}'".FormatWith(new object[] { typeName, model }));
        }
        return null;
      }

      IModelFactory modelFactory = GetModelFactory(type);
      return modelFactory.BindingContract.IsCompliant(sourceItem, type) ? modelFactory.Create(sourceItem, type) : null;
    }

    /// <summary>
    /// Gets the model factory that is capable of instantiating the specified model class type.
    /// </summary>
    /// <param name="type">The model class type that the model factory should instantiate.</param>
    /// <returns></returns>
    protected IModelFactory GetModelFactory(Type type)
    {
      return ModelFactoryService.ResolveModelFactory(type);
    }

    /// <summary>
    /// Gets an instance of the prototype model factory set by the ModelFactoryService.
    /// This method is obsolete. Use the GetModelFactory method instead to ensure that globally registered model factories are taken into account.
    /// </summary>
    /// <value>
    /// The model factory.
    /// </value>
    [Obsolete]
    protected IModelFactory ModelFactory
    {
      get { return _modelFactory ?? (_modelFactory = ModelFactoryService.GetPrototypeClone()); }
    }

    private IModelFactory _modelFactory;
  }
}