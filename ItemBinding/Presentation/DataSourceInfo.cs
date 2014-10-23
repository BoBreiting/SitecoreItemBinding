using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ItemBinding.Presentation
{
  /// <summary>
  /// WebControl for rendering data source information
  /// </summary>
  [DefaultProperty("Text")]
  [ToolboxData("<{0}:ServerControl1 runat=server></{0}:ServerControl1>")]
  public class DataSourceInfo : WebControl
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="DataSourceInfo"/> class.
    /// </summary>
    /// <param name="text">The text.</param>
    public DataSourceInfo(String text)
    {
      Text = text;
    }

    /// <summary>
    /// Gets or sets the text that is displayed.
    /// </summary>
    /// <value>The text that is displayed.</value>
    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("")]
    [Localizable(true)]
    public String Text { get; set; }

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      writer.AddAttribute(HtmlTextWriterAttribute.Class, "dataSourceInfo");
      writer.RenderBeginTag(HtmlTextWriterTag.Div);
    }

    /// <summary>
    /// Renders the contents of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void RenderContents(HtmlTextWriter writer)
    {
      writer.Write(Text);
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
      writer.RenderEndTag();
    }
  }
}