using System;
using ItemBinding.Application;
using ItemBinding.Model;
using Sitecore.Data.Items;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Helpers;
using Sitecore.StringExtensions;

namespace ItemBinding.Mvc.Infrastructure
{
  public class ModelLocator : Sitecore.Mvc.Presentation.ModelLocator
  {
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

      return ModelFactory.BindingContract.IsCompliant(sourceItem, type) ? ModelFactory.Create(sourceItem, type) : null;
    }

    protected IModelFactory ModelFactory
    {
      get { return _modelFactory ?? (_modelFactory = ModelFactoryService.GetPrototypeClone()); }
    }

    private IModelFactory _modelFactory;
  }
}