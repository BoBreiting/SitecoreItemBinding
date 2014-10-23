using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sitecore.Data.Items;

namespace ItemBinding.Model.BindingContracts
{
  /// <summary>
  /// Represents an attribute based binding contract that uses ClauseAttributes to check binding contract compliance.
  /// </summary>
  public class AttributeBasedBindingContract : IBindingContract
  {
    /// <summary>
    /// Determines whether the specified item is compliant with the binding contract.
    /// </summary>
    /// <typeparam name="T">The model class used to determine the binding contract to test against.</typeparam>
    /// <param name="item">The item that should be tested for compliance.</param>
    /// <returns><c>true</c> if the item complies with the binding contract; otherwise <c>false</c></returns>
    public virtual Boolean IsCompliant<T>(Item item) where T : class
    {
      Type type = typeof (T);
      return IsCompliant(item, type);
    }

    /// <summary>
    /// Determines whether the specified item is compliant with the binding contract.
    /// </summary>
    /// <param name="item">The item that should be tested for compliance.</param>
    /// <param name="type">The type of the model class used to determine the binding contract to test against.</param>
    /// <returns><c>true</c> if the item complies with the binding contract; otherwise <c>false</c></returns>
    public Boolean IsCompliant(Item item, Type type)
    {
      List<ClauseAttribute> clauseAttributes = new List<ClauseAttribute>(GetClauseAttributes(type));

      if (!clauseAttributes.All(clauseAttribute => clauseAttribute.IsComplied(item)))
        return false;

      foreach (CompositeTypeAttribute compositeTypeAttribute in GetCompositeTypeAttributes(type))
      {
        clauseAttributes = new List<ClauseAttribute>(GetClauseAttributes(compositeTypeAttribute.Type));

        if (!clauseAttributes.All(clauseAttribute => clauseAttribute.IsComplied(item)))
          return false;
      }

      return true;
    }

    /// <summary>
    /// Asserts that the specified item is compliant with the binding contract.
    /// </summary>
    /// <typeparam name="T">The model class that the item is being bound to.</typeparam>
    /// <param name="item">The item that should be tested for compliance.</param>
    public virtual void Assert<T>(Item item) where T : class
    {
      Type type = typeof (T);
      Assert(item, type);
    }

    /// <summary>
    /// Asserts that the specified item is compliant with the binding contract.
    /// </summary>
    /// <param name="item">The item that should be tested for compliance.</param>
    /// <param name="type">The type of the model class that the item is being bound to.</param>
    public virtual void Assert(Item item, Type type)
    {
      List<ClauseAttribute> clauseAttributes = new List<ClauseAttribute>(GetClauseAttributes(type));

      foreach (ClauseAttribute clauseAttribute in clauseAttributes)
      {
        clauseAttribute.Assert(item, type);
      }

      foreach (CompositeTypeAttribute compositeTypeAttribute in GetCompositeTypeAttributes(type))
      {
        clauseAttributes = new List<ClauseAttribute>(GetClauseAttributes(compositeTypeAttribute.Type));

        foreach (ClauseAttribute clauseAttribute in clauseAttributes)
        {
          clauseAttribute.Assert(item, compositeTypeAttribute.Type);
        }
      }
    }

    /// <summary>
    /// Gets the clause attributes by iterating the specified type and base types.
    /// </summary>
    /// <param name="type">The type to iterate for clause attributes.</param>
    /// <returns>A collection of the clause attributes that are specified on the specified type and base types.</returns>
    private IEnumerable<ClauseAttribute> GetClauseAttributes(Type type)
    {
      List<ClauseAttribute> clauseAttributes = new List<ClauseAttribute>();

      for (Type currentType = type; currentType != null; currentType = currentType.BaseType)
      {
        MemberInfo memberInfo = currentType;
        clauseAttributes.AddRange(memberInfo.GetCustomAttributes<ClauseAttribute>());
      }
      return clauseAttributes;
    }

    /// <summary>
    /// Gets the composite type attributes.
    /// </summary>
    /// <param name="type">The type to examine.</param>
    /// <returns>Collection of composite type attributes specified on the specified type and base types.</returns>
    private IEnumerable<CompositeTypeAttribute> GetCompositeTypeAttributes(Type type)
    {
      List<CompositeTypeAttribute> compositeTypeAttributes = new List<CompositeTypeAttribute>();
      List<Type> traversedTypes = new List<Type>();
      GetCompositeTypeAttributesRecursively(type, compositeTypeAttributes, traversedTypes);
      return compositeTypeAttributes;
    }

    /// <summary>
    /// Gets the composite type attributes by iterating the specified type and base types recursively.
    /// </summary>
    /// <param name="type">The type to iterate for composite type attributes.</param>
    /// <param name="compositeTypeAttributes">The list of composite type attributes specified on the specified type and base types.</param>
    /// <param name="traversedTypes">The types that have been traversed. This is used to ensure that a type is only traversed once to prevent endless loops.</param>
    private void GetCompositeTypeAttributesRecursively(Type type, List<CompositeTypeAttribute> compositeTypeAttributes, List<Type> traversedTypes)
    {
      for (Type currentType = type; currentType != null; currentType = currentType.BaseType)
      {
        if (traversedTypes.Contains(currentType))
          break;

        traversedTypes.Add(currentType);
        MemberInfo memberInfo = currentType;
        List<CompositeTypeAttribute> compositeAttributes = memberInfo.GetCustomAttributes<CompositeTypeAttribute>().ToList();
        compositeTypeAttributes.AddRange(compositeAttributes);
        foreach (CompositeTypeAttribute compositeTypeAttribute in compositeAttributes)
        {
          GetCompositeTypeAttributesRecursively(compositeTypeAttribute.Type, compositeTypeAttributes, traversedTypes);
        }
      }
    }
  }
}