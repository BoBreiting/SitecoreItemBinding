using System;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace ItemBinding.Infrastructure
{
  internal static class SublayoutExtensions
  {
    internal static Item GetDataSourceItem(this Sublayout sublayout)
    {
      String dataSource = sublayout.DataSource;

      if (String.IsNullOrEmpty(dataSource))
        return null;

      if (IsDataSourceQuery(dataSource))
        return GetDataSourceItemByQuery(dataSource);

      return Context.Database != null ? Context.Database.GetItem(dataSource) : null;
    }

    private static Boolean IsDataSourceQuery(String dataSourcePath)
    {
      return dataSourcePath.StartsWith("query:", StringComparison.OrdinalIgnoreCase);
    }

    private static Boolean IsRelativeQuery(String query)
    {
      return !query.StartsWith("/");
    }

    private static Item GetDataSourceItemByQuery(String query)
    {
      query = EscapeWordsWithDashes(query.Replace("query:", String.Empty));

      if (IsRelativeQuery(query))
      {
        return Context.Item != null ? Context.Item.Axes.SelectSingleItem(query) : null;
      }
      return Context.Database != null ? Context.Database.SelectSingleItem(query) : null;
    }

    private static String EscapeWordsWithDashes(String query)
    {
      String[] querySegments = query.Split(new[] {'/'});

      for (Int32 index = 0; index < querySegments.Length; ++index)
      {
        String segment = querySegments[index];
        if (segment.Contains("-") && !segment.Equals("ancestor-or-self") && !segment.Equals("descendant-or-self"))
          querySegments[index] = "#" + querySegments[index] + "#";
      }
      return String.Join("/", querySegments);
    }
  }
}