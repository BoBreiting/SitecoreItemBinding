using System;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace ItemBinding.Model.BindingContracts
{
  /// <summary>
  /// Exception used by the <see cref="RequiredFieldAttribute"/> if the item being bound does not contain a valid value in the required Sitecore field.
  /// </summary>
  public class RequiredFieldException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RequiredFieldException"/> class.
    /// </summary>
    /// <param name="item">The item that cause the error.</param>
    /// <param name="fieldId">The field identifier that did not contain a valid value.</param>
    /// <param name="type">The type that specified the required field.</param>
    public RequiredFieldException(Item item, ID fieldId, Type type)
    {
      _item = item;
      _fieldId = fieldId;
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
        return String.Format("The field '{0}' on the item '{1}' doesn't contain a value as required by the type '{2}'", _fieldId, _item.Paths.FullPath, _type.FullName);
      }
    }

    private readonly Item _item;
    private readonly ID _fieldId;
    private readonly Type _type;
  }
}