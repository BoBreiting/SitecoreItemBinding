using System;
using Sitecore.Data.Items;
using Sitecore.Mvc.Pipelines.Response.RenderRendering;

namespace ItemBinding.Mvc.Infrastructure.Pipelines.RenderRendering
{
  public class ResolveDataSourceQuery : RenderRenderingProcessor
  {
    public override void Process(RenderRenderingArgs args)
    {
      String dataSource = args.Rendering.DataSource;
      if (!dataSource.StartsWith(Query, StringComparison.OrdinalIgnoreCase))
        return;

      String query = dataSource.Substring(Query.Length);
      Item dataSourceItem = args.PageContext.Item.Axes.SelectSingleItem(query);
      if (dataSourceItem != null)
      {
        args.Rendering.DataSource = dataSourceItem.Paths.FullPath;
      }
    }

    private const String Query = "query:";
  }
}