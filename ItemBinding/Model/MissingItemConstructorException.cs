using System;

namespace ItemBinding.Model
{
  /// <summary>
  /// Exception used by the <see cref="ModelFactory"/> if the type being created does not contain a constructor that accepts an item.
  /// </summary>
  public class MissingItemConstructorException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MissingItemConstructorException"/> class.
    /// </summary>
    /// <param name="type">The type that didn't implement a constructor that accepts an item.</param>
    public MissingItemConstructorException(Type type)
    {
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
        return String.Format("The model class '{0}' does not contain a constructor that accepts a Sitecore Item argument", _type.FullName);
      }
    }

    private readonly Type _type;
  }
}