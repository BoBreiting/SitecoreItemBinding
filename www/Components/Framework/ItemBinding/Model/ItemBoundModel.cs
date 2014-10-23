using Sitecore.Data.Items;

namespace ItemBinding.Model
{
  /// <summary>
  /// Represents a base model class that can be used by model classes to standardize access to the bound item.
  /// </summary>
  public class ItemBoundModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ItemBoundModel"/> class.
    /// </summary>
    /// <param name="item">The item.</param>
    public ItemBoundModel(Item item)
    {
      _innerItem = item;
    }

    /// <summary>
    /// Gets the inner item bound to the model.
    /// </summary>
    /// <value>The inner item bound to the model.</value>
    public Item InnerItem
    {
      get { return _innerItem; }
    }

    private readonly Item _innerItem;
  }
}