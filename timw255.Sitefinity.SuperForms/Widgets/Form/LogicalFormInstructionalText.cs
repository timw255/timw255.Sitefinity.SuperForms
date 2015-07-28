using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using timw255.Sitefinity.SuperForms.Widgets.Form.Designers;

namespace timw255.Sitefinity.SuperForms.Widgets.Form
{
    [ControlDesigner(typeof(CustomInstructionalTextDesigner))]
    class LogicalFormInstructionalText : FormInstructionalText, IConditionalFormControl, IConditionalSlave
    {
        public bool UsesConditionalLogic { get; set; }
        public int Action { get; set; }
        public int Quantity { get; set; }
        public string CriteriaSet { get; set; }
        public string TargetId { get; set; }

        public LogicalFormInstructionalText()
        {
            this.TargetId = Helpers.GenerateTargetId();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            this.AddCssClass("lf-container-" + this.TargetId);

            if (this.UsesConditionalLogic && this.Action == 0)
            {
                this.AddCssClass("lf-hidden");
            }

            this.AddCssClass("lf-field" + this.TargetId);
            this.Attributes["data-tid"] = this.TargetId;
        }

        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            List<ScriptReference> scriptReferences = new List<ScriptReference>()
            {
                new ScriptReference("timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormInstructionalText.min.js", "timw255.Sitefinity.SuperForms"),
                new ScriptReference("timw255.Sitefinity.SuperForms.Resources.logicalForms.min.js", "timw255.Sitefinity.SuperForms")
            };
            return scriptReferences;
        }
    }
}
