# Sitecore ItemBinding TDS Code Generation Templates #

## Important notice ##
The included ItemBinding code generation templates requires the following include files from the sample project provided by Hedgehog Development

Helpers.tt<br/>
StringExtensions.tt<br/>
GeneralExtensions.tt<br/>
Inflector.tt<br/>

The Hedgehog Development sample project is available at [https://github.com/HedgehogDevelopment/tds-codegen](https://github.com/HedgehogDevelopment/tds-codegen)

## How to use the ItemBinding templates ##
1. Copy the Sitecore ItemBinding templates to the Code Generation Templates folder of your TDS project.
2. Copy the required include files from the Hedgehog sample project to the Code Generation Templates folder of your TDS project.
3. Configure your TDS project to use the desired templates.

## Template description ##
### ItemBindingSeparateTemplates.tt ###
This template will generate a separate class for each of the Sitecore templates with a RequiredBaseTemplate attribute set to the Sitecore template and include public fields for each of the fields on the template. However the template will not do anything to handle inherited base templates.
### ItemBindingCombinedTemplates.tt ###
This template will generate a separate class for each of the Sitecore templates with a RequiredBaseTemplate attribute set to the Sitecore template and all inherited base template and include public fields for each of the fields on the template and the inherited base templates.
### ItemBindingCompositeTemplates.tt ###
This template will generate a separate class for each of the Sitecore templates with a RequiredBaseTemplate attribute set to the Sitecore template. In addition a CompositeType attribute will be set for all inherited base templates. Public fields will be generated for each of the fields on the template. In addition a composite type property will be created for each of the classes that represent the inherited base templates. This will result in a template class with nested template classes for each of the inherited base templates.