using System;
using System.Web.UI;
using ItemBinding.Application;
using ItemBinding.Infrastructure;
using ItemBinding.Model;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace ItemBinding.Presentation
{
  /// <summary>
  /// Abstract UserControl used for binding an item to a model class instance
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class ModelBoundUserControl<T> : UserControl where T : class
  {
    /// <summary>
    /// Gets the model class instance that is bound to the SourceItem.
    /// </summary>
    /// <value>The model class instance that is bound to the SourceItem.</value>
    public virtual T Model
    {
      get
      {
        if (_model != null)
          return _model;

        if (SourceItem == null)
          return _model = null;

        try
        {
          return _model = ModelFactory.Create<T>(SourceItem);
        }
        catch (Exception exception)
        {
          Log.Error(String.Format("Unable to bind the source item '{0}' to the model class '{1}'", SourceItem.Paths.FullPath, typeof(T).FullName), exception, this);
          return null;
        }
      }
    }

    /// <summary>
    /// Gets the model factory that is used to bind the SourceItem to the model class T.
    /// </summary>
    /// <value>The model factory that is used to bind the SourceItem to the model class T.</value>
    protected virtual IModelFactory ModelFactory
    {
      get { return _modelFactory ?? (_modelFactory = ModelFactoryService.GetPrototypeClone()); }
    }

    /// <summary>
    /// Gets the source item that is bound to the model class T exposed by the Model member.
    /// </summary>
    /// <value>The source item that is bound to the model class T exposed by the Model member.</value>
    protected virtual Item SourceItem
    {
      get { return _sourceItem ?? (_sourceItem = this.GetDataSourceOrContextItem()); }
    }

    /// <summary>
    /// Gets a value indicating whether the data source unpublishable information text should be shown.
    /// </summary>
    /// <value>
    /// <c>true</c> if the data source unpublishable information text should be shown; otherwise, <c>false</c>.
    /// </value>
    protected virtual Boolean ShowDataSourceUnpublishableInfoText
    {
      get { return true; }
    }

    /// <summary>
    /// Gets the information text that is displayed if the data source is unpublishable.
    /// </summary>
    /// <value>The information text that is displayed if the data source is unpublishable.</value>
    protected virtual String DataSourceUnpublishableInfoText
    {
      get { return "The datasource is unpublishable"; }
    }

    /// <summary>
    /// Gets a value indicating whether the data source unavailable information text should be shown.
    /// </summary>
    /// <value>
    /// <c>true</c> if the data source unavailable information text should be shown; otherwise, <c>false</c>.
    /// </value>
    protected virtual Boolean ShowDataSourceUnavailableInfoText
    {
      get { return true; }
    }

    /// <summary>
    /// Gets the information text that is displayed if the data source is unavailable.
    /// </summary>
    /// <value>The information text that is displayed if the data source is unavailable.</value>
    protected virtual String DataSourceUnavailableInfoText
    {
      get { return "The datasource is unavailable"; }
    }

    /// <summary>
    /// Gets a value indicating whether the control should be automatically databound on init.
    /// </summary>
    /// <value>
    /// <c>true</c> if the control should be automatically databound on init; otherwise, <c>false</c>.
    /// </value>
    protected virtual Boolean DatabindOnInit { get { return true; } }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnDataBinding(EventArgs e)
    {
      _databound = true;
      base.OnDataBinding(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);

      if (!DatabindOnInit || Model == null)
        return;

      try
      {
        DataBind();
      }
      catch (Exception exception)
      {
        Log.Error(String.Format("Error while initializing model bound user control of type '{0}'", typeof(T).FullName), exception, this);
        throw;
      }
    }

    /// <summary>
    /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.
    /// </summary>
    /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the server control content.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (SourceItem == null && Model == null)
      {
        Controls.Clear();

        if (!Sitecore.Context.PageMode.IsNormal && ShowDataSourceUnavailableInfoText)
          RenderDatasourceUnavailableInfo(writer);

        return;
      }

      if (SourceItem != null && !SourceItem.IsPublishable())
      {
        Controls.Clear();

        if (!Sitecore.Context.PageMode.IsNormal && ShowDataSourceUnpublishableInfoText)
          RenderDatasourceUnpublishableInfo(writer);

        return;
      }

      if (Model == null)
      {
        Controls.Clear();
        return;
      }

      if (_databound == false)
      {
        Controls.Clear();
        return;
      }
      base.Render(writer);
    }

    /// <summary>
    /// Renders the datasource unpublishable information if the datasource used by the Model instance is unpublishable.
    /// </summary>
    /// <param name="writer">The writer.</param>
    private void RenderDatasourceUnpublishableInfo(HtmlTextWriter writer)
    {
      Control control = new DataSourceInfo(DataSourceUnpublishableInfoText);
      control.RenderControl(writer);

      if (SourceItem != null && Sitecore.Context.Item != null)
      {
        Log.Error(String.Format("Datasource is unpublishable for source item '{0}' on item '{1}'", SourceItem.Paths.FullPath, Sitecore.Context.Item.Paths.FullPath), this);
      }
    }

    /// <summary>
    /// Renders the datasource unavailable information if the datasource used by the Model instance is unavailable.
    /// </summary>
    /// <param name="writer">The writer.</param>
    private void RenderDatasourceUnavailableInfo(HtmlTextWriter writer)
    {
      Control control = new DataSourceInfo(DataSourceUnavailableInfoText);
      control.RenderControl(writer);

      if (SourceItem != null && Sitecore.Context.Item != null)
      {
        Log.Error(String.Format("Datasource is unavailable for source item '{0}' on item '{1}'", SourceItem.Paths.FullPath, Sitecore.Context.Item.Paths.FullPath), this);
      }
    }

    private T _model;
    private Item _sourceItem;
    private IModelFactory _modelFactory;
    private Boolean _databound;
  }
}