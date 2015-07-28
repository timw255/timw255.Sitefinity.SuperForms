using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using timw255.Sitefinity.SuperForms.Widgets.Form.Designers;

namespace timw255.Sitefinity.SuperForms.Widgets.Form
{
    [ControlDesigner(typeof(CustomDropDownListDesigner))]
    public sealed class LogicalFormDropDownList : FormDropDownList, IConditionalFormControl, IConditionalMaster, IConditionalSlave, IProgressiveFormControl
    {
        public bool UsesConditionalLogic { get; set; }
        public int Action { get; set; }
        public int Quantity { get; set; }
        public string CriteriaSet { get; set; }
        public string TargetId { get; set; }

        public bool UsesProgressiveLogic { get; set; }
        public bool IsProgressiveKeyField { get; set; }
        public string ProgressiveCriteriaSet { get; set; }

        public LogicalFormDropDownList()
        {
            this.TargetId = Helpers.GenerateTargetId();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (this.DisplayMode == FieldDisplayMode.Write)
            {
                this.AddCssClass("lf-container-" + this.TargetId);

                if (this.UsesConditionalLogic && this.Action == 0)
                {
                    this.AddCssClass("lf-hidden");
                    this.Container.GetControl<DropDownList>("dropDown", true).Attributes.Add("disabled", "disabled");
                }

                this.Container.GetControl<DropDownList>("dropDown", true).AddCssClass("lf-field" + this.TargetId);
                this.Container.GetControl<DropDownList>("dropDown", true).Attributes["data-tid"] = this.TargetId;
                this.Container.GetControl<DropDownList>("dropDown", true).Attributes["data-defaultvalue"] = this.DefaultSelectedTitle;
            }
        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences())
            {
                new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js", "Telerik.Sitefinity"),
                new ScriptReference("timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormDropDownList.min.js", "timw255.Sitefinity.SuperForms"),
                new ScriptReference("timw255.Sitefinity.SuperForms.Resources.logicalForms.min.js", "timw255.Sitefinity.SuperForms")
            };
            return scriptReferences;
        }
    }
}