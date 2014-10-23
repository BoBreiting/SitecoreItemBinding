using System.Web.UI;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace ItemBinding.Infrastructure
{
  internal static class UserControlExtensions
  {
    internal static Item GetDataSourceOrContextItem(this UserControl control)
    {
      return GetDataSourceItem(control) ?? Context.Item;
    }

    internal static Item GetDataSourceItem(this UserControl control)
    {
      Sublayout sublayout = GetSublayout(control);
      return sublayout != null ? sublayout.GetDataSourceItem() : null;
    }

    internal static Sublayout GetSublayout(this UserControl userControl)
    {
      return userControl.Parent as Sublayout;
    }
  }
}