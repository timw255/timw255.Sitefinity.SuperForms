using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using timw255.Sitefinity.SuperForms.Widgets.Form.Designers;

namespace timw255.Sitefinity.SuperForms.Widgets.Form
{
    [ControlDesigner(typeof(CustomSectionHeaderDesigner))]
    public sealed class LogicalFormSectionHeader : FormSectionHeader, IConditionalFormControl, IConditionalSlave
    {
        public bool UsesConditionalLogic { get; set; }
        public int Action { get; set; }
        public int Quantity { get; set; }
        public string CriteriaSet { get; set; }
        public string TargetId { get; set; }

        public LogicalFormSectionHeader()
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
                new ScriptReference("timw255.Sitefinity.SuperForms.Resources.logicalForms.min.js", "timw255.Sitefinity.SuperForms")
            };
            return scriptReferences;
        }
    }
}
