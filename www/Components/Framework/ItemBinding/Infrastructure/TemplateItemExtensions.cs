using System;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace ItemBinding.Infrastructure
{
  public static class TemplateItemExtensions
  {
    public static Boolean IsDerived(this TemplateItem template, ID templateId)
    {
      return template.ID == templateId || template.BaseTemplates.Any(baseTemplate => baseTemplate.IsDerived(templateId));
    }
  }
}