using timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views;
using System.Collections.Generic;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace timw255.Sitefinity.SuperForms.Widgets.Form.Designers
{
    public class CustomFormsControlDesigner : FormsControlDesigner
    {
        public override string LayoutTemplatePath
        {
            get
            {
                return "~/SuperForms/timw255.Sitefinity.SuperForms.Widgets.Form.Designers.CustomFormsControlDesigner.ascx";
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());

            scripts.Add(new ScriptReference("timw255.Sitefinity.SuperForms.Widgets.Form.Designers.CustomFormsControlDesigner.js", "timw255.Sitefinity.SuperForms"));

            return scripts.ToArray();
        }
    }
}