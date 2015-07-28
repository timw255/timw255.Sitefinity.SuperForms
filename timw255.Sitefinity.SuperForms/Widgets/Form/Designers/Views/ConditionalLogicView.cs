using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Pages.Model;
using System.Globalization;
using System.Reflection;
using Telerik.Sitefinity.Modules.Pages;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security;

namespace timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views
{
    internal class ConditionalLogicView : ContentViewDesignerView
    {
        private FormsManager _formsManager { get; set; }
        public FormsManager FManager
        {
            get
            {
                if (_formsManager == null)
                {
                    _formsManager = FormsManager.GetManager();
                }
                return _formsManager;
            }
        }

        #region Control References

        protected internal virtual Literal Script
        {
            get
            {
                return this.Container.GetControl<Literal>("Script", true);
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }
        #endregion

        #region Properties

        public override string ViewName
        {
            get { return "Logic"; }
        }

        public override string ViewTitle
        {
            get { return Res.Get<SuperFormsResources>().ViewTitle; }
        }

        protected override string LayoutTemplateName
        {
            get
            {
                return "timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.ConditionalLogicView.ascx";
            }
        }

        public override string LayoutTemplatePath
        {
            get
            {
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }
        #endregion

        protected override void InitializeControls(GenericContainer container)
        {
            IFormFieldControl thisControl = base.ParentDesigner.PropertyEditor.Control as IFormFieldControl;
            FormDraftControl thisControlData = base.ParentDesigner.PropertyEditor.ControlData as FormDraftControl;

            FormDescription form = FManager.GetFormByName(thisControlData.Form.Name);

            IControlsContainer cc = GetControlsContainer(form.Id);

            List<ControlData> formControls = (List<ControlData>)typeof(PageHelper)
                .GetMethod("SortControls", BindingFlags.Static | BindingFlags.NonPublic)
                .Invoke(null, new object[] { new[] { cc }, 1 });

            formControls.RemoveAll(fc => fc.ObjectType == "Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormSubmitButton, Telerik.Sitefinity" || fc.ObjectType == "timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormInstructionalText, timw255.Sitefinity.SuperForms" || fc.IsLayoutControl == true);

            if (formControls.Count > 0)
            {
                List<CriteriaOption> criteriaOptions = new List<CriteriaOption>();

                CultureInfo uiCulture = CultureInfo.GetCultureInfo(this.GetUICulture());

                foreach (var formControl in formControls)
                {
                    FieldControl fieldControl = FManager.LoadControl(formControl, uiCulture) as FieldControl;

                    CriteriaOption co = new CriteriaOption();

                    if (fieldControl is FormChoiceField)
                    {
                        co.FieldType = "ChoiceField";
                        co.FieldName = Helpers.GetFieldName(fieldControl);
                        co.FieldId = fieldControl.ID;
                        co.Options = ((FormChoiceField)fieldControl).Choices
                            .Select(c => new SimpleChoiceItem() { Text = c.Text, Value = c.Value }).ToList();

                        co.Conditions = new List<SimpleChoiceItem>()
                        {
                            new SimpleChoiceItem() { Text = "=", Value = "==" },
                            new SimpleChoiceItem() { Text = "!=", Value = "!=" }
                        };
                    }

                    if (fieldControl is TextField)
                    {
                        co.FieldType = "TextField";
                        co.FieldName = Helpers.GetFieldName(fieldControl);
                        co.FieldId = fieldControl.ID;

                        co.Conditions = new List<SimpleChoiceItem>()
                        {
                            new SimpleChoiceItem() { Text = "=", Value = "==" },
                            new SimpleChoiceItem() { Text = "!=", Value = "!=" },
                            new SimpleChoiceItem() { Text = "<", Value = "lt" },
                            new SimpleChoiceItem() { Text = ">", Value = "gt" }
                        };
                    }

                    if (!String.IsNullOrWhiteSpace(co.FieldType) && !String.IsNullOrWhiteSpace(co.FieldName) && co.Conditions.Count > 0)
                    {
                        criteriaOptions.Add(co);
                    }
                }

                StringBuilder script = new StringBuilder();

                script.Append(@"<script>");
                script.AppendFormat(@"var currentCultureC = ""{0}"";", this.GetUICulture());

                string optionFilter = "";

                if (thisControl != null)
                {
                    optionFilter = ((FieldControl)thisControl).ID;//Helpers.GetFieldName((FieldControl)thisControl);
                }

                script.AppendFormat(@"var optionFilter = ""{0}"";", optionFilter);

                script.AppendFormat(@"var criteriaOptions = {0};", Helpers.SerializeJSON<List<CriteriaOption>>(criteriaOptions));

                IConditionalFormControl cfc = ((IConditionalFormControl)base.ParentDesigner.PropertyEditor.Control);

                string criteriaSet = "[]";

                if (cfc != null)
                {
                    string criteriaSetPropertyValue = cfc.CriteriaSet;

                    if (!String.IsNullOrWhiteSpace(criteriaSetPropertyValue))
                    {
                        criteriaSet = criteriaSetPropertyValue;
                    }
                }

                script.AppendFormat("var criteria = {0};", criteriaSet);

                script.Append(@"</script>");

                Script.Text = script.ToString();
            }
        }

        public IControlsContainer GetControlsContainer(Guid id)
        {
            Guid currentUserId = SecurityManager.GetCurrentUserId();

            FormDraft formDraft = FManager.GetDrafts().Where(d => d.ParentForm.Id == id && d.Owner == currentUserId && d.IsTempDraft).SingleOrDefault<FormDraft>();

            if (formDraft != null)
            {
                return formDraft;
            }

            return FManager.GetForm(id);
        }

        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            var scriptDescriptors = new ScriptControlDescriptor(GetType().FullName, ClientID);

            return new[] { scriptDescriptors };
        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            List<ScriptReference> scriptReferences = new List<ScriptReference>()
            {
                new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", "Telerik.Sitefinity"),
                new ScriptReference("timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.ConditionalLogicView.js", "timw255.Sitefinity.SuperForms")
            };
            return scriptReferences;
        }
    }
}