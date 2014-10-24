Sitecore ItemBinding
====================

The main purpose of the Sitecore ItemBinding framework is to create a simple and lightweight framework that eliminates the repetitive code that is usually required to access Sitecore items and creating model classes for representing item data and to automate model class instantiation and item binding.

The framework is intended to standardize and minimize the code required to instantiate and bind model classes to Sitecore items. This means that the individual developers need to write less tedious plumbing code which means that they will have more time to work with business logic that will add true value to the project. It will also help developers instantly recognize how the various parts of a solution works by using common code patterns and functionality and will greatly simplify code reviews and refactoring.

By centralizing the item-to-model binding process the framework also removes the errors that would normally be scattered throughout the solution as individual developers write localized functionality to handle these aspects and ensures that if any errors are found they can be corrected centrally.

The framework also offers a standardized methodology for ensuring that the items that are bound to the model classes complies to the requirements set by the model classes through the use of an extensible set of binding contracts not unlike the data contracts featured in the .NET framework. This helps to cut down on run time errors caused by content editors creating or selecting items that are not of the correct type which would normally cause the code to break.

Unlike other similar frameworks such as Glass.Mapper, the model class generator in Sitecore Rocks or CustomItemGenerator by Velir the ItemBinding framework is not about abstracting away the underlying Sitecore data model by wrapping the Sitecore item in a proxy model class or an attempt at automatic mapping of field values to model class properties. Basically the ItemBinding framework is all about ease of model class instantiation and binding that model with the appropriate Sitecore items while ensuring that the item complies to the model class requirements. How you choose to construct your model classes is entirely up to you and you can even combine ItemBinding with some of the other frameworks to draw on their capabilities when it comes to creating proxy wrapper classes or automatic field to property mappings.

For a more detailed description and examples feel free to read the documentation located in the Documentation folder in the project or visit the [wiki pages](https://github.com/BoBreiting/SitecoreItemBinding/wiki).
