using System;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace ItemBinding.Model.BindingContracts
{
  /// <summary>
  /// Represents a clause attribute for specifying required Sitecore fields.
  /// </summary>
  public class RequiredFieldAttribute : ClauseAttribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RequiredFieldAttribute"/> class.
    /// </summary>
    /// <param name="fieldId">The field identifier.</param>
    /// <param name="ignoreInEditMode">if set to <c>true</c> the required field should be ignored in edit mode; otherwise an error will be thrown if <c>false</c>.</param>
    /// <exception cref="System.ArgumentException"></exception>
    public RequiredFieldAttribute(String fieldId, Boolean ignoreInEditMode = true)
    {
      if (!ID.IsID(fieldId))
        throw new ArgumentException(String.Format("The provided value '{0}' for the argument fieldId cannot be converted to a valid Sitecore.Data.ID", fieldId));
      _fieldId = ID.Parse(fieldId);

      _ignoreInPageEditMode = ignoreInEditMode;
    }

    /// <summary>
    /// Gets the field identifier.
    /// </summary>
    /// <value>
    /// The field identifier.
    /// </value>
    public ID FieldId
    {
      get { return _fieldId; }
    }

    /// <summary>
    /// Gets a value indicating whether the required field should be ignored in page edit mode.
    /// </summary>
    /// <value>
    /// <c>true</c> if the required field should be ignored in page edit mode; otherwise, <c>false</c>.
    /// </value>
    public Boolean IgnoreInPageEditMode
    {
      get { return _ignoreInPageEditMode; }
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
      return !String.IsNullOrEmpty(item[FieldId]) || IgnoreInPageEditMode && Sitecore.Context.PageMode.IsPageEditor;
    }

    /// <summary>
    /// Asserts that the specified item complies with the requirements set by the clause attribute.
    /// </summary>
    /// <param name="item">The item to test for clause compliance.</param>
    /// <param name="type">The model class type that the item is being bound to.</param>
    /// <exception cref="ItemBinding.Model.BindingContracts.RequiredFieldException"></exception>
    public override void Assert(Item item, Type type)
    {
      if (IgnoreInPageEditMode && Sitecore.Context.PageMode.IsPageEditor)
        return;

      if (String.IsNullOrEmpty(item[FieldId]))
        throw new RequiredFieldException(item, FieldId, type);
    }

    private readonly ID _fieldId;
    private readonly Boolean _ignoreInPageEditMode;
  }
}