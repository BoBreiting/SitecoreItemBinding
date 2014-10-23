using System;
using ItemBinding.Infrastructure;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace ItemBinding.Model.BindingContracts
{
  /// <summary>
  /// Represents a clause attribute for specifying required Sitecore base template inheritance.
  /// </summary>
  public class RequiredBaseTemplateAttribute : ClauseAttribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RequiredBaseTemplateAttribute"/> class.
    /// </summary>
    /// <param name="templateId">The template identifier.</param>
    /// <exception cref="System.ArgumentException"></exception>
    public RequiredBaseTemplateAttribute(String templateId)
    {
      if (!ID.IsID(templateId))
        throw new ArgumentException(String.Format("The provided value '{0}' for the argument templateId cannot be converted to a valid Sitecore.Data.ID", templateId));
      _templateId = ID.Parse(templateId);
    }

    /// <summary>
    /// Gets the base template identifier.
    /// </summary>
    /// <value>
    /// The base template identifier.
    /// </value>
    public ID BaseTemplateId
    {
      get { return _templateId; }
    }

    /// <summary>
    /// Determines whether the specified item complies with the requirements set by the clause attribute.
    /// </summary>
    /// <param name="item">The item to test for clause compliance.</param>
    /// <returns>
    ///   <c>true</c> if the item complies with the clause requirements; otherwise <c>false</c>
    /// </returns>
    public override Boolean IsComplied(Item item)
    {
      return item.IsDerived(BaseTemplateId);
    }

    /// <summary>
    /// Asserts that the specified item complies with the requirements set by the clause attribute.
    /// </summary>
    /// <param name="item">The item to test for clause compliance.</param>
    /// <param name="type">The model class type that the item is being bound to.</param>
    /// <exception cref="ItemBinding.Model.BindingContracts.RequiredBaseTemplateException"></exception>
    public override void Assert(Item item, Type type)
    {
      if (!item.IsDerived(BaseTemplateId))
        throw new RequiredBaseTemplateException(item, BaseTemplateId, type);
    }

    private readonly ID _templateId;
  }
}