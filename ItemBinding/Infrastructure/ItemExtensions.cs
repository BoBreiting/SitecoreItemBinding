using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace ItemBinding.Infrastructure
{
  internal static class ItemExtensions
  {
    internal static Boolean IsPublishable(this Item item)
    {
      Item validVersion = item.Publishing.GetValidVersion(DateTime.Now, true, false);
      return validVersion != null && validVersion.Publishing.IsPublishable(DateTime.Now, true);
    }

    internal static Item GetDropLinkSelectedItem(this Item item, ID fieldId)
    {
      return new InternalLinkField(item.Fields[fieldId]).TargetItem;
    }

    internal static IEnumerable<Item> GetMultiListValues(this Item item, ID fieldId)
    {
      return new MultilistField(item.Fields[fieldId]).GetItems() ?? Enumerable.Empty<Item>();
    }

    internal static Boolean IsDerived(this Item item, ID templateId)
    {
      return IsDerived(item.Template, templateId);
    }

    private static Boolean IsDerived(TemplateItem template, ID templateId)
    {
      if (template == null || ID.IsNullOrEmpty(templateId))
        return false;

      String key = String.Format("template_isderived_cache_{0},{1}", templateId, template.ID);
      if (MemoryCache.Default.Contains(key))
        return (Boolean) MemoryCache.Default[key];

      Boolean isDerived = template.IsDerived(templateId);
      if (!MemoryCache.Default.Contains(key))
      {
        MemoryCache.Default.Add(key, isDerived, new CacheItemPolicy {SlidingExpiration = TimeSpan.FromMinutes(30)});
      }
      return isDerived;
    }
  }
}