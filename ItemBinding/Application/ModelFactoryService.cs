using System;
using System.Collections.Generic;
using ItemBinding.Model;

namespace ItemBinding.Application
{
  /// <summary>
  /// ModelFactoryService for managing the model factory prototype used to create clones when a model factory instance is required.
  /// </summary>
  public static class ModelFactoryService
  {
    /// <summary>
    /// Initializes the <see cref="ModelFactoryService"/> class.
    /// </summary>
    static ModelFactoryService()
    {
      _prototypeModelFactory = new ModelFactory();
      ModelFactories = new Dictionary<Type, IModelFactory>();
    }

    /// <summary>
    /// Registers a model factory in the global model factory collection.
    /// </summary>
    /// <typeparam name="T">The type that the model class can instantiate.</typeparam>
    /// <param name="modelFactory">The model factory.</param>
    public static void RegisterModelFactory<T>(IModelFactory modelFactory) where T : class
    {
      Type type = typeof(T);

      if (ModelFactories.ContainsKey(type))
        ModelFactories[type] = modelFactory;

      ModelFactories.Add(type, modelFactory.Clone() as IModelFactory);
    }

    /// <summary>
    /// Resolves the model factory that can instantiate the specified model class.
    /// </summary>
    /// <typeparam name="T">The model class type that the model factory should instantiate.</typeparam>
    /// <returns>IModelFactory capable of instantiating the specified model class type.</returns>
    public static IModelFactory ResolveModelFactory<T>() where T : class
    {
      return ResolveModelFactory(typeof(T));
    }

    /// <summary>
    /// Resolves the model factory that can instantiate the specified model class.
    /// </summary>
    /// <param name="type">The model class type that the model factory should instantiate.</param>
    /// <returns>IModelFactory capable of instantiating the specified model class type.</returns>
    public static IModelFactory ResolveModelFactory(Type type)
    {
      if (ModelFactories.ContainsKey(type))
        return ModelFactories[type].Clone() as IModelFactory;

      return GetPrototypeClone();
    }

    /// <summary>
    /// Sets the IModelFactory prototype that is cloned when a model factory instance is required by the framework.
    /// </summary>
    /// <param name="modelFactory">The model factory to set as the prototype.</param>
    public static void SetPrototype(IModelFactory modelFactory)
    {
      _prototypeModelFactory = modelFactory.Clone() as IModelFactory;
    }

    /// <summary>
    /// Returns a clone of the currently registered model factory prototype.
    /// This method is obsolete. Use the ResolveModelFactory methods instead to ensure that registered model factories are taken into account.
    /// This method will be called internally by the ResolveModelFactory methods if no other suitable model factory is found in the global collection.
    /// </summary>
    /// <returns>A clone of the prototype model factory</returns>
    [Obsolete]
    public static IModelFactory GetPrototypeClone()
    {
      return _prototypeModelFactory.Clone() as IModelFactory;
    }

    /// <summary>
    /// The prototype model factory instance
    /// </summary>
    private static IModelFactory _prototypeModelFactory;

    private readonly static Dictionary<Type, IModelFactory> ModelFactories;
  }
}