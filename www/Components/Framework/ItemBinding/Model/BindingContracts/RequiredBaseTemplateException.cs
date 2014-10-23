using System;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace ItemBinding.Model.BindingContracts
{
  /// <summary>
  /// Exception used by the <see cref="RequiredBaseTemplateAttribute"/> if the item being bound does not inherit from the required Sitecore base template.
  /// </summary>
  public class RequiredBaseTemplateException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RequiredBaseTemplateException"/> class.
    /// </summary>
    /// <param name="item">The item that cause the error.</param>
    /// <param name="templateId">The base template that was not inherited by the item.</param>
    /// <param name="type">The type that specified the required base template.</param>
    public RequiredBaseTemplateException(Item item, ID templateId, Type type)
    {
      _item = item;
      _templateId = templateId;
      _type = type;
    }

    /// <summary>
    /// Gets a message that describes the current exception.
    /// </summary>
    /// <returns>The error message that explains the reason for the exception, or an empty string("").</returns>
    public override String Message
    {
      get
      {
        return String.Format("The item '{0}' does not inherit from the base template '{1}' as required by the type '{2}'", _item.Paths.FullPath, _templateId, _type.FullName);
      }
    }

    private readonly Item _item;
    private readonly ID _templateId;
    private readonly Type _type;
  }
}