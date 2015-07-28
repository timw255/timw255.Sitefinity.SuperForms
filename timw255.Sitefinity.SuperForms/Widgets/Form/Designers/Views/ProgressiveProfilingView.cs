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
    internal class ProgressiveProfilingView : ContentViewDesignerView
    {
        private bool _disableKeyFieldSelector;
        private string _progressiveKeyFieldName;
        private bool _wrongTypeForKeyField;

        private FormsManager _formsManager { get; set; }
        private FormsManager FManager
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
            get { return "Progressive"; }
        }

        public override string ViewTitle
        {
            get { return "Progressive"; }
        }

        protected override string LayoutTemplateName
        {
            get
            {
                return "timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.ProgressiveProfilingView.ascx";
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

            ControlData progressiveKeyFieldControlData = formControls.Where(c => ((FormDraftControl)c).Properties.Any(p => p.Name == "IsProgressiveKeyField" && p.Value == "True")).FirstOrDefault();

            if (progressiveKeyFieldControlData != null)
            {
                FieldControl progressiveKeyFieldControl = FManager.LoadControl(progressiveKeyFieldControlData, CultureInfo.CurrentUICulture) as FieldControl;

                _progressiveKeyFieldName = Helpers.GetFieldName(progressiveKeyFieldControl);

                if (!String.IsNullOrEmpty(_progressiveKeyFieldName) && _progressiveKeyFieldName != Helpers.GetFieldName(thisControl as FieldControl))
                {
                    _disableKeyFieldSelector = true;
                }
            }

            if (!(thisControl is FormTextBox))
            {
                _disableKeyFieldSelector = true;
                _wrongTypeForKeyField = true;

            }

            if (formControls.Count > 0)
            {
                List<CriteriaOption> progressiveCriteriaOptions = new List<CriteriaOption>();

                CultureInfo uiCulture = CultureInfo.GetCultureInfo(this.GetUICulture());

                foreach (var formControl in formControls)
                {
                    FieldControl fieldControl = FManager.LoadControl(formControl, uiCulture) as FieldControl;

                    CriteriaOption co = new CriteriaOption();

                    if (fieldControl != null)
                    {
                        co.FieldName = Helpers.GetFieldName(fieldControl);
                        co.FieldId = fieldControl.ID;

                        progressiveCriteriaOptions.Add(co);
                    }
                }

                StringBuilder script = new StringBuilder();

                script.Append(@"<script>");
                script.AppendFormat(@"var currentProgressiveCulture = ""{0}"";", this.GetUICulture());
                script.AppendFormat(@"var progressiveOptionFilter = ""{0}"";", Helpers.GetFieldName((FieldControl)thisControl));
                script.AppendFormat(@"var progressiveCriteriaOptions = {0};", Helpers.SerializeJSON<List<CriteriaOption>>(progressiveCriteriaOptions));

                string progressiveCriteriaSetPropertyValue = ((IProgressiveFormControl)thisControl).ProgressiveCriteriaSet;
                string criteriaSet = "[]";

                if (!String.IsNullOrWhiteSpace(progressiveCriteriaSetPropertyValue))
                {
                    criteriaSet = progressiveCriteriaSetPropertyValue;
                }

                script.AppendFormat("var progressiveCriteria = {0};", criteriaSet);
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

            scriptDescriptors.AddProperty("_disableKeyFieldSelector", _disableKeyFieldSelector);
            scriptDescriptors.AddProperty("_wrongTypeForKeyField", _wrongTypeForKeyField);
            scriptDescriptors.AddProperty("_keyFieldName", _progressiveKeyFieldName);

            return new[] { scriptDescriptors };
        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            List<ScriptReference> scriptReferences = new List<ScriptReference>()
            {
                new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", "Telerik.Sitefinity"),
                new ScriptReference("timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.ProgressiveProfilingView.js", "timw255.Sitefinity.SuperForms")
            };
            return scriptReferences;
        }
    }
}