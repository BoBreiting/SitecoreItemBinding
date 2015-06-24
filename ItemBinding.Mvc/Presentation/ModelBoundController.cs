using System;
using System.Web.Mvc;
using ItemBinding.Application;
using ItemBinding.Model;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Presentation;

namespace ItemBinding.Mvc.Presentation
{
  public class ModelBoundController<T> : Controller where T : class
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
          _model = ModelFactory.Create<T>(SourceItem);

          if (_model is IRenderingModel)
            InitializeRenderingModel(_model);

          return _model;
        }
        catch (Exception exception)
        {
          Log.Error(String.Format("Unable to bind the source item '{0}' to the model class '{1}'", SourceItem.Paths.FullPath, typeof(T).FullName), exception, this);
          return null;
        }
      }
    }

    private void InitializeRenderingModel(T model)
    {
      IRenderingModel renderingModel = model as IRenderingModel;
      if (renderingModel == null)
        return;

      renderingModel.Initialize(Rendering);
    }

    /// <summary>
    /// Gets the model factory that is used to bind the SourceItem to the model class T.
    /// </summary>
    /// <value>The model factory that is used to bind the SourceItem to the model class T.</value>
    protected virtual IModelFactory ModelFactory
    {
      get { return _modelFactory ?? (_modelFactory = ModelFactoryService.ResolveModelFactory<T>()); }
    }

    /// <summary>
    /// Gets the source item that is bound to the model class T exposed by the Model member.
    /// </summary>
    /// <value>The source item that is bound to the model class T exposed by the Model member.</value>
    protected virtual Item SourceItem
    {
      get { return _sourceItem ?? (_sourceItem = Rendering.Item ?? PageContext.Current.Item); }
    }

    protected virtual Rendering Rendering
    {
      get { return RenderingContext.Current.Rendering; }
    }

    private T _model;
    private Item _sourceItem;
    private IModelFactory _modelFactory;
  }
}