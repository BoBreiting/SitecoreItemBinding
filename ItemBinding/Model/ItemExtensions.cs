using System;
using System.Collections.Generic;
using System.Linq;
using ItemBinding.Application;
using ItemBinding.Infrastructure;
using ItemBinding.Model.BindingContracts;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace ItemBinding.Model
{
  /// <summary>
  /// Item extension methods for accessing the item binding framework.
  /// </summary>
  public static class ItemExtensions
  {
    /// <summary>
    /// Determines whether the specified item is bindable to the model class T.
    /// </summary>
    /// <typeparam name="T">The model class that the item should be bindable to.</typeparam>
    /// <param name="item">The item to test.</param>
    /// <returns><c>true</c> if the item is bindable to the model class T; otherwise <c>false</c></returns>
    public static Boolean IsBindable<T>(this Item item) where T : class
    {
      IBindingContract bindingContract = ModelFactoryService.GetPrototypeClone().BindingContract;
      return item.IsBindable<T>(bindingContract);
    }

    /// <summary>
    /// Determines whether the specified item is bindable to the model class T.
    /// </summary>
    /// <typeparam name="T">The model class that the item should be bindable to.</typeparam>
    /// <param name="item">The item to test.</param>
    /// <param name="bindingContract">The binding contract to use for testing the item.</param>
    /// <returns><c>true</c> if the item is bindable to the model class T; otherwise <c>false</c></returns>
    public static Boolean IsBindable<T>(this Item item, IBindingContract bindingContract) where T : class
    {
      return bindingContract.IsCompliant<T>(item);
    }

    /// <summary>
    /// Determines whether the selected item is bindable to the model class T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item">The item containing the field with the selected item to bind.</param>
    /// <param name="fieldId">The id of the field containing the selected item to test.</param>
    /// <returns><c>true</c> if the selected item is bindable to the model class T; otherwise <c>false</c></returns>
    public static Boolean IsSelectedItemBindable<T>(this Item item, ID fieldId) where T : class
    {
      IBindingContract bindingContract = ModelFactoryService.GetPrototypeClone().BindingContract;
      return item.IsSelectedItemBindable<T>(fieldId, bindingContract);
    }

    /// <summary>
    /// Determines whether the selected item is bindable to the model class T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item">The item containing the field with the selected item to bind.</param>
    /// <param name="fieldId">The id of the field containing the selected item to test.</param>
    /// <param name="bindingContract">The binding contract to use for testing the selected item.</param>
    /// <returns><c>true</c> if the selected item is bindable to the model class T; otherwise <c>false</c></returns>
    public static Boolean IsSelectedItemBindable<T>(this Item item, ID fieldId, IBindingContract bindingContract) where T : class
    {
      Item targetItem = item.GetDropLinkSelectedItem(fieldId);
      return targetItem != null && targetItem.IsBindable<T>(bindingContract);
    }

    /// <summary>
    /// Create a model class T instance and bind the specified item to the instance.
    /// </summary>
    /// <typeparam name="T">The model class that the item should be bound to.</typeparam>
    /// <param name="item">The item to bind.</param>
    /// <returns>A new instance of the model class T bound to the specified item.</returns>
    /// <exception cref="System.Exception">This method calls the model factory and the associated binding contract that may throw an error if the item does not comply with the binding contract or if the model class does not contain a constructor that accepts an item.</exception>
    public static T BindAs<T>(this Item item) where T : class
    {
      IModelFactory modelFactory = ModelFactoryService.GetPrototypeClone();
      return item.BindAs<T>(modelFactory);
    }

    /// <summary>
    /// Create a model class T instance and bind the specified item to the instance.
    /// </summary>
    /// <typeparam name="T">The model class that the item should be bound to.</typeparam>
    /// <param name="item">The item to bind.</param>
    /// <param name="modelFactory">The model factory to use.</param>
    /// <returns>A new instance of the model class T bound to the specified item.</returns>
    /// <exception cref="System.Exception">This method calls the model factory and the associated binding contract that may throw an error if the item does not comply with the binding contract or if the model class does not contain a constructor that accepts an item.</exception>
    public static T BindAs<T>(this Item item, IModelFactory modelFactory) where T : class
    {
      return modelFactory.Create<T>(item);
    }

    /// <summary>
    /// Creates a collection of model class T instances and bind the specified item's children to the instances.
    /// </summary>
    /// <typeparam name="T">The model class that the item's children should be bound to.</typeparam>
    /// <param name="item">The parent item of the children to bind.</param>
    /// <returns>A collection of model class T instances bound to the specified item's children.</returns>
    public static IEnumerable<T> BindChildrenAs<T>(this Item item) where T : class
    {
      IModelFactory modelFactory = ModelFactoryService.GetPrototypeClone();
      return BindChildrenAs<T>(item, modelFactory);
    }

    /// <summary>
    /// Creates a collection of model class T instances and bind the specified item's children to the instances.
    /// </summary>
    /// <typeparam name="T">The model class that the item's children should be bound to.</typeparam>
    /// <param name="item">The parent item of the children to bind.</param>
    /// <param name="modelFactory">The model factory to use.</param>
    /// <returns>A collection of model class T instances bound to the specified item's children.</returns>
    public static IEnumerable<T> BindChildrenAs<T>(this Item item, IModelFactory modelFactory) where T : class
    {
      return item.GetChildren().Where(itm => itm.IsBindable<T>()).Select(itm => itm.BindAs<T>(modelFactory));
    }

    /// <summary>
    /// Creates a collection of model class T instances and bind the selected items in the specified field on the specified item to the instances.
    /// </summary>
    /// <typeparam name="T">The model class that the item's children should be bound to.</typeparam>
    /// <param name="item">The item containing the field with the selected items to bind.</param>
    /// <param name="fieldId">The id of the field containing the selected items to bind.</param>
    /// <returns>A collection of model class T instances bound to the selected items in the specified field on the specified item.</returns>
    public static IEnumerable<T> BindSelectedItemsAs<T>(this Item item, ID fieldId) where T : class
    {
      IModelFactory modelFactory = ModelFactoryService.GetPrototypeClone();
      return BindSelectedItemsAs<T>(item, fieldId, modelFactory);
    }

    /// <summary>
    /// Creates a collection of model class T instances and bind the selected items in the specified field on the specified item to the instances.
    /// </summary>
    /// <typeparam name="T">The model class that the item's children should be bound to.</typeparam>
    /// <param name="item">The item containing the field with the selected items to bind.</param>
    /// <param name="fieldId">The id of the field containing the selected items to bind.</param>
    /// <param name="modelFactory">The model factory to use.</param>
    /// <returns>A collection of model class T instances bound to the selected items in the specified field on the specified item.</returns>
    public static IEnumerable<T> BindSelectedItemsAs<T>(this Item item, ID fieldId, IModelFactory modelFactory) where T : class
    {
      return item.GetMultiListValues(fieldId).Where(itm => itm.IsBindable<T>()).Select(itm => itm.BindAs<T>(modelFactory));
    }

    /// <summary>
    /// Create a model class T instance and bind the selected item to the instance.
    /// </summary>
    /// <typeparam name="T">The model class that the selected item should be bound to.</typeparam>
    /// <param name="item">The item containing the field with the selected item to bind.</param>
    /// <param name="fieldId">The id of the field containing the selected item to bind.</param>
    /// <returns>A new instance of the model class T bound to the selected item.</returns>
    /// <exception cref="System.Exception">This method calls the model factory and the associated binding contract that may throw an error if the selected item does not comply with the binding contract or if the model class does not contain a constructor that accepts an item.</exception>
    public static T BindSelectedItemAs<T>(this Item item, ID fieldId) where T : class
    {
      IModelFactory modelFactory = ModelFactoryService.GetPrototypeClone();
      return BindSelectedItemAs<T>(item, fieldId, modelFactory);
    }

    /// <summary>
    /// Create a model class T instance and bind the selected item to the instance.
    /// </summary>
    /// <typeparam name="T">The model class that the selected item should be bound to.</typeparam>
    /// <param name="item">The item containing the field with the selected item to bind.</param>
    /// <param name="fieldId">The id of the field containing the selected item to bind.</param>
    /// <param name="modelFactory">The model factory to use.</param>
    /// <returns>A new instance of the model class T bound to the selected item.</returns>
    /// <exception cref="System.Exception">This method calls the model factory and the associated binding contract that may throw an error if the selected item does not comply with the binding contract or if the model class does not contain a constructor that accepts an item.</exception>
    public static T BindSelectedItemAs<T>(this Item item, ID fieldId, IModelFactory modelFactory) where T : class
    {
      Item targetItem = item.GetDropLinkSelectedItem(fieldId);
      return targetItem != null ? targetItem.BindAs<T>(modelFactory) : null;
    }
  }
}