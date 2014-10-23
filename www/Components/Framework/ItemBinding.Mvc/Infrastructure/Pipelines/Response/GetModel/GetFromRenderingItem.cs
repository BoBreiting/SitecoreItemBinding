using System;
using Sitecore.Data.Items;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Pipelines.Response.GetModel;
using Sitecore.Mvc.Presentation;

namespace ItemBinding.Mvc.Infrastructure.Pipelines.Response.GetModel
{
  public class GetFromRenderingItem : GetModelProcessor
  {
    public override void Process(GetModelArgs args)
    {
      if (args.Result != null)
        return;
      
      Rendering = args.Rendering;
      args.Result = GetModelFromField();
    }

    protected virtual Object GetModelFromField()
    {
      Item item = Rendering.RenderingItem.ValueOrDefault(i => i.InnerItem);
      return item != null ? ModelLocator.GetModel(item["Model"], SourceItem, true) : null;
    }

    protected Rendering Rendering { get; private set; }

    protected Item SourceItem
    {
      get { return Rendering.Item ?? PageContext.Current.Item; }
    }

    protected ModelLocator ModelLocator
    {
      get { return _modelLocator ?? (_modelLocator = new ModelLocator()); }
    }

    private ModelLocator _modelLocator;

  }
}