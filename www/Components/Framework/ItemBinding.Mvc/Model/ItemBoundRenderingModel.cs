using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace ItemBinding.Mvc.Model
{
  /// <summary>
  /// Represents a base rendering model class that can be used by model classes to standardize access to the bound item.
  /// </summary>
  public class ItemBoundRenderingModel : RenderingModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ItemBoundRenderingModel"/> class.
    /// </summary>
    /// <param name="item">The item.</param>
    public ItemBoundRenderingModel(Item item)
    {
      _innerItem = item;
    }

    /// <summary>
    /// Gets the inner item bound to the ItemBoundRenderingModel.
    /// </summary>
    /// <value>The inner item bound to the ItemBoundRenderingModel.</value>
    public Item InnerItem
    {
      get { return _innerItem; }
    }

    private readonly Item _innerItem;
  }
}