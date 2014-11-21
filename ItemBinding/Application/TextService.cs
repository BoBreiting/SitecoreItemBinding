using System;
using System.Web.Configuration;

namespace ItemBinding.Application
{
  public static class TextService
  {
    static TextService()
    {
      DataSourceUnavailableInfoText = WebConfigurationManager.AppSettings["ItemBinding.DataSourceUnavailableInfoText"];
      if (String.IsNullOrEmpty(DataSourceUnavailableInfoText))
        DataSourceUnavailableInfoText = "The datasource is unavailable";

      DataSourceUnpublishableInfoText = WebConfigurationManager.AppSettings["ItemBinding.DataSourceUnpublishableInfoText"];
      if (String.IsNullOrEmpty(DataSourceUnpublishableInfoText))
        DataSourceUnpublishableInfoText = "The datasource is unpublishable";
    }

    public static String DataSourceUnavailableInfoText { get; set; }

    public static String DataSourceUnpublishableInfoText { get; set; }
  }
}