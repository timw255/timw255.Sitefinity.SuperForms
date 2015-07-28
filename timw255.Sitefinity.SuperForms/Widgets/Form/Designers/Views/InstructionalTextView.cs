using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;

namespace timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views
{
    class InstructionalTextView : ContentViewDesignerView
    {
        internal const string designerScriptName = "Telerik.Sitefinity.Modules.GenericContent.Web.UI.Scripts.ContentBlockDesignerBase.js";

        public static string _layoutTemplatePath;

        protected virtual HtmlField HtmlEditor
        {
            get
            {
                return this.Container.GetControl<HtmlField>("htmlEditor", true);
            }
        }

        #region Properties

        public override string ViewName
        {
            get { return "Instructional Text"; }
        }

        public override string ViewTitle
        {
            get { return "Default"; }
        }

        public override string LayoutTemplatePath
        {
            get
            {
                return "~/SuperForms/timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.InstructionalTextView.ascx";
            }
        }
        #endregion

        protected override string ScriptDescriptorTypeName
        {
            get
            {
                return typeof(InstructionalTextView).FullName;
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        static InstructionalTextView()
        {
        }

        public InstructionalTextView()
        {
        }

        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();

            ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor(GetType().FullName, ClientID);
            scriptControlDescriptor.AddComponentProperty("htmlEditor", this.HtmlEditor.ClientID);

            scriptDescriptors.Add(scriptControlDescriptor);
            return scriptDescriptors;
        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            string fullName = typeof(InstructionalTextView).Assembly.FullName;
            List<ScriptReference> scriptReferences = new List<ScriptReference>()
            {
                new ScriptReference("timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.InstructionalTextView.js", "timw255.Sitefinity.SuperForms"),
                new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase.js", "Telerik.Sitefinity")
            };
            return scriptReferences.ToArray();
        }

        protected override void InitializeControls(GenericContainer container)
        {
            //base.DesignerMode = ControlDesignerModes.Simple;
            //base.AdvancedModeIsDefault = false;
            //if (this.PropertyEditor != null)
            //{
            //    this.HtmlEditor.UICulture = this.PropertyEditor.PropertyValuesCulture;
            //}
        }
    }
}
