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
    /// </summary>
    /// <returns>A clone of the prototype model factory</returns>
    public static IModelFactory GetPrototypeClone()
    {
      return _prototypeModelFactory.Clone() as IModelFactory;
    }

    /// <summary>
    /// The prototype model factory instance
    /// </summary>
    private static IModelFactory _prototypeModelFactory;
  }
}