using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web;
using Telerik.Sitefinity.Modules.Forms.Web.UI;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;
using timw255.Sitefinity.SuperForms.Widgets.Form;
using timw255.Sitefinity.SuperForms.Widgets.Form.Designers;

namespace timw255.Sitefinity.SuperForms.FormControls
{
    [ControlDesigner(typeof(CustomFormsControlDesigner))]
    public sealed class LogicalFormsControl : FormsControl
    {
        private bool _progressiveProfiling;
        private bool _conditionalLogic;
        private bool _formAnalytics;

        private string _progressiveKeyFieldName;

        private FormEntry _priorFormEntry;
        private Guid _currentSiteId;
        private HttpCookie _sfTrackingCookie;

        private bool formDescriptionFound = true;

        public string ExtendedFormType { get; set; }
        private bool _isConditionalForm = false;
        private bool _isProgressiveForm = false;

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

        protected override void InitializeControls(GenericContainer container)
        {
            _currentSiteId = SystemManager.CurrentContext.CurrentSite.Id;
            _sfTrackingCookie = HttpContext.Current.Request.Cookies["sf-trckngckie"];

            switch (ExtendedFormType)
            {
                case "conditional":
                    _isConditionalForm = true;
                    break;
                case "progressive":
                    _isProgressiveForm = true;
                    break;
            }

            if (_isProgressiveForm)
            {
                _isProgressiveForm = true;
                _priorFormEntry = GetPriorFormEntry();
            }

            if (!this.IsLicensed)
            {
                this.ErrorsPanel.Visible = true;
                ControlCollection controls = this.ErrorsPanel.Controls;
                Label label = new Label()
                {
                    Text = Res.Get<FormsResources>().ModuleNotLicensed
                };

                controls.Add(label);
                return;
            }

            if (this.FormData != null)
            {
                bool flag = true;
                CultureInfo currentUICulture = CultureInfo.CurrentUICulture;

                if (this.CurrentLanguage != null)
                {
                    currentUICulture = new CultureInfo(this.CurrentLanguage);
                }

                if (!this.FormData.IsPublished(currentUICulture))
                {
                    flag = false;
                }

                if (flag)
                {
                    this.RenderFormFields();
                }
            }

            if (!this.formDescriptionFound && this.IsDesignMode())
            {
                this.ErrorsPanel.Visible = true;
                ControlCollection controlCollections = this.ErrorsPanel.Controls;
                Label label1 = new Label()
                {
                    Text = Res.Get<FormsResources>().TheSpecifiedFormNoLongerExists
                };

                controlCollections.Add(label1);
            }

            HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;

            if (currentHttpContext != null && this.formDescriptionFound)
            {
                if (!currentHttpContext.Items.Contains("PageFormControls"))
                {
                    currentHttpContext.Items["PageFormControls"] = string.Empty;
                }

                currentHttpContext.Items["PageFormControls"] = string.Format("{0}Id={1},ValidationGroup={2};", (string)currentHttpContext.Items["PageFormControls"], this.FormId, this.ValidationGroup);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (this.FormData != null)
            {
                this.AddCssClass("logicalForm");

                this.Attributes.Add("data-siteId", _currentSiteId.ToString());
                this.Attributes.Add("data-formId", this.FormId.ToString());
                this.Attributes.Add("data-fieldName", this.FormData.Title);
                this.AddCssClass("analyticForm");

                if (!Page.IsPostBack)
                {
                    foreach (FieldControl fieldControl in this.FieldControls)
                    {
                        string fieldName = Helpers.GetFieldName(fieldControl);

                        if (_isProgressiveForm)
                        {
                            if (_priorFormEntry != null)
                            {
                                if (fieldName == _progressiveKeyFieldName && (_priorFormEntry.DoesFieldExist(fieldName) && _priorFormEntry.GetValue(fieldName) != null))
                                {
                                    fieldControl.Value = _priorFormEntry.GetValue(fieldName);
                                }
                            }
                        }

                        fieldControl.AddCssClass("faContainer");
                        fieldControl.Attributes.Add("data-fieldName", fieldName);
                    }

                    if (_isConditionalForm)
                    {
                        List<ConditionalRule> ruleSet = BuildConditionalRuleSet();

                        if (ruleSet.Count > 0)
                        {
                            string logicalFields = Helpers.SerializeJSON(ruleSet.SelectMany(r => r.Fields).Distinct().ToArray());
                            string js = Helpers.SerializeJSON<List<ConditionalRule>>(ruleSet);
                            string scriptBlock = String.Format(CultureInfo.InvariantCulture, "<script>$(document).ready(function() {{\nvar logicalForm = new LogicalForms();\nlogicalForm.validationGroup = \"{0}\";\nlogicalForm.switches = {1};\nlogicalForm.switchers = {2};\nlogicalForm.run(); \n}});</script><style>.lf-hidden {{display: none;}}</style>", this.ValidationGroup, js, logicalFields);

                            this.Container.Controls.Add(new LiteralControl(scriptBlock));
                        }
                    }
                }
            }
        }

        private List<ConditionalRule> BuildConditionalRuleSet()
        {
            List<ConditionalRule> conditionalRules = new List<ConditionalRule>();

            IControlsContainer cc = FManager.GetForm(this.FormId);

            List<ControlData> formControls = (List<ControlData>)typeof(PageHelper)
                .GetMethod("SortControls", BindingFlags.Static | BindingFlags.NonPublic)
                .Invoke(null, new object[] { new[] { cc }, 1 });

            formControls.RemoveAll(fc => fc.ObjectType == "Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormSubmitButton, Telerik.Sitefinity" || fc.IsLayoutControl == true);

            CultureInfo uiCulture = null;
            if (Config.Get<ResourcesConfig>().Multilingual)
            {
                uiCulture = System.Globalization.CultureInfo.CurrentUICulture;
            }

            foreach (var fieldControl in formControls)
            {
                var fc1 = FManager.LoadControl(fieldControl, uiCulture);

                IConditionalFormControl lfc = fc1 as IConditionalFormControl;

                if (lfc != null && lfc.UsesConditionalLogic == true)
                {
                    ConditionalRule conditionalRule = new ConditionalRule();

                    conditionalRule.Target = lfc.TargetId;
                    conditionalRule.Action = lfc.Action == 0 ? "Show" : "Hide";
                    conditionalRule.Bool = lfc.Quantity == 0 ? "OR" : "AND";

                    List<CriteriaItem> checks = Helpers.DeserializeJSON<List<CriteriaItem>>(lfc.CriteriaSet);

                    conditionalRule.Fields = checks.Select(ci => ((IConditionalFormControl)this.FieldControls.Where(fc => ((SimpleScriptView)fc).ID == ci.FieldId).Single()).TargetId).Distinct().ToArray();

                    conditionalRule.Checks = checks.Select(ci => new CriteriaItem()
                    {
                        Field = ((IConditionalFormControl)this.FieldControls.Where(fc => ((SimpleScriptView)fc).ID == ci.FieldId).Single()).TargetId,
                        Condition = ci.Condition,
                        Option = ci.Option
                    }).ToList();

                    conditionalRules.Add(conditionalRule);
                }
            }
            return conditionalRules;
        }

        private void RenderFormFields()
        {
            Control control;
            if (this.FormData != null)
            {
                List<IControlsContainer> controlsContainers = new List<IControlsContainer>()
                {
                    this.FormData
                };
                List<ControlData> controlDatas = new List<ControlData>();

                typeof(PageHelper)
                    .GetMethod("ProcessControls", BindingFlags.Static | BindingFlags.NonPublic, null, CallingConventions.Any, new Type[] { typeof(IList<ControlData>), typeof(IList<ControlData>), typeof(IList<IControlsContainer>) }, null)
                    .Invoke(null, new object[] { new List<ControlData>(), controlDatas, controlsContainers });

                PlaceHoldersCollection placeHoldersCollection = new PlaceHoldersCollection();
                PlaceHolder placeHolder = new PlaceHolder()
                {
                    ID = "Body"
                };

                placeHoldersCollection.Add(placeHolder);

                List<ControlData> controlDatas1 = (List<ControlData>)typeof(PageHelper)
                    .GetMethod("SortControls", BindingFlags.Static | BindingFlags.NonPublic)
                    .Invoke(null, new object[] { controlsContainers.AsEnumerable<IControlsContainer>().Reverse<IControlsContainer>(), controlsContainers.Count });

                
                if (_isProgressiveForm)
                {
                    ControlData progressiveKeyFieldControlData = controlDatas1.Where(c => ((FormControl)c).Properties.Any(p => p.Name == "IsProgressiveKeyField" && p.Value == "True")).FirstOrDefault();

                    if (progressiveKeyFieldControlData != null)
                    {
                        FieldControl progressiveKeyFieldControl = FManager.LoadControl(progressiveKeyFieldControlData, CultureInfo.CurrentUICulture) as FieldControl;

                        _progressiveKeyFieldName = Helpers.GetFieldName(progressiveKeyFieldControl);
                    }
                }

                foreach (ControlData controlDatum in controlDatas1)
                {
                    Control control1 = FManager.LoadControl(controlDatum, CultureInfo.CurrentUICulture);

                    if (placeHoldersCollection.TryGetValue(controlDatum.PlaceHolder, out control))
                    {
                        if (control1 is FormSubmitButton)
                        {
                            this.ConfigureSubmitButton(control1, this.ValidationGroup);
                        }

                        bool showProgressiveField = ShowProgressiveField(control1);

                        if (control1 is FormSubmitButton || (_isProgressiveForm ? showProgressiveField : true))
                        {
                            control.Controls.Add(control1);
                        }

                        IFormFieldControl formFieldControl = control1 as IFormFieldControl;

                        if (formFieldControl != null & (control1 is FormSubmitButton || (_isProgressiveForm ? showProgressiveField : true)))
                        {
                            FieldControl fieldName = formFieldControl as FieldControl;

                            if (fieldName != null)
                            {
                                if (formFieldControl.MetaField != null && !string.IsNullOrEmpty(formFieldControl.MetaField.FieldName))
                                {
                                    fieldName.DataFieldName = formFieldControl.MetaField.FieldName;
                                }

                                fieldName.ValidationGroup = this.ValidationGroup;
                                fieldName.ValidatorDefinition.MessageCssClass = "sfError";
                                fieldName.ControlCssClassOnError = "sfErrorWrp";
                            }

                            this.FieldControls.Add(formFieldControl);
                        }
                    }

                    if (!(control1 is LayoutControl))
                    {
                        continue;
                    }

                    LayoutControl layoutControl = (LayoutControl)control1;

                    layoutControl.PlaceHolder = controlDatum.PlaceHolder;
                    placeHoldersCollection.AddRange(layoutControl.Placeholders);
                }
                this.FormControls.Controls.Add(placeHolder);
            }
        }

        protected override void Submit_Click(object sender, EventArgs e)
        {
            this.ProcessForm(this.FormData);
        }

        private void ProcessForm(FormDescription form)
        {
            string str;
            if (form == null)
            {
                return;
            }
            if (!this.ValidateFormSubmissionRestrictions(form, out str))
            {
                this.FormControls.Visible = true;
                this.ErrorsPanel.Visible = true;
                this.ErrorsPanel.Controls.Add(new Label()
                {
                    Text = str
                });
            }
            else if (this.ValidateFormInput())
            {
                CancelEventArgs cancelEventArg = new CancelEventArgs();

                this.OnBeforeFormSave(cancelEventArg);

                if (!cancelEventArg.Cancel)
                {
                    this.SaveFormEntry(form);
                    this.OnFormSaved(null);

                    CancelEventArgs cancelEventArg1 = new CancelEventArgs();

                    this.OnBeforeFormAction(cancelEventArg1);

                    if (!cancelEventArg1.Cancel)
                    {
                        this.ProcessFromSubmitAction(form);
                        return;
                    }
                }
            }
        }

        new private void SaveFormEntry(FormDescription description)
        {
            FormsManager manager = FormsManager.GetManager();
            FormEntry userHostAddress = null;

            if (_isProgressiveForm && _priorFormEntry == null)
            {
                FieldControl keyField = this.FieldControls.Where(fc => ((IFormFieldControl)fc).MetaField.FieldName == _progressiveKeyFieldName).FirstOrDefault() as FieldControl;

                if (keyField != null && !String.IsNullOrWhiteSpace((string)keyField.Value))
                {
                    _priorFormEntry = GetPriorFormEntryByKeyField((string)keyField.Value);
                }
            }

            if (_isProgressiveForm && _priorFormEntry != null)
            {
                string entryType = String.Format("{0}.{1}", manager.Provider.FormsNamespace, this.FormData.Name);

                userHostAddress = manager.GetFormEntry(entryType, _priorFormEntry.Id);
            }
            else
            {
                userHostAddress = manager.CreateFormEntry(string.Format("{0}.{1}", manager.Provider.FormsNamespace, description.Name));
            }
            
            foreach (IFormFieldControl formFieldControl in this.FieldControls)
            {
                FieldControl fieldControl = formFieldControl as FieldControl;
                object value = fieldControl.Value;

                if (fieldControl.GetType().Name == "FormFileUpload")
                {
                    typeof(FormsControl)
                        .GetMethod("SaveFiles", BindingFlags.Static | BindingFlags.NonPublic)
                        .Invoke(null, new object[] { value as UploadedFileCollection, manager, description, userHostAddress, formFieldControl.MetaField.FieldName });
                }
                else if (!(value is List<string>))
                {
                    userHostAddress.SetValue(formFieldControl.MetaField.FieldName, value);
                }
                else
                {
                    StringArrayConverter stringArrayConverter = new StringArrayConverter();
                    object obj = stringArrayConverter.ConvertTo((value as List<string>).ToArray(), typeof(string));

                    userHostAddress.SetValue(formFieldControl.MetaField.FieldName, obj);
                }
            }
            userHostAddress.IpAddress = this.Page.Request.UserHostAddress;
            userHostAddress.SubmittedOn = DateTime.UtcNow;

            Guid userId = ClaimsManager.GetCurrentUserId();

            userHostAddress.UserId = userId == Guid.Empty ? Guid.Parse(_sfTrackingCookie.Value) : userId;

            if (userHostAddress.UserId == userId)
            {
                userHostAddress.Owner = userId;
            }

            if (SystemManager.CurrentContext.AppSettings.Multilingual)
            {
                userHostAddress.Language = CultureInfo.CurrentUICulture.Name;
            }

            FormDescription formData = this.FormData;
            formData.FormEntriesSeed = formData.FormEntriesSeed + (long)1;
            userHostAddress.ReferralCode = this.FormData.FormEntriesSeed.ToString();

            manager.SaveChanges();
        }

        private bool ShowProgressiveField(Control control1)
        {
            IProgressiveFormControl pfc = control1 as IProgressiveFormControl;

            if (pfc != null)
            {
                string fieldName = Helpers.GetFieldName((FieldControl)pfc);

                if (fieldName == _progressiveKeyFieldName)
                {
                    return true;
                }

                if (!pfc.UsesProgressiveLogic & _priorFormEntry == null)
                {
                    return true;
                }

                if (pfc.UsesProgressiveLogic & _priorFormEntry == null)
                {
                    return false;
                }

                if (_priorFormEntry.DoesFieldExist(fieldName) && _priorFormEntry.GetValue(fieldName) != null)
                {
                    return false;
                }

                if (!pfc.UsesProgressiveLogic & (_priorFormEntry.DoesFieldExist(fieldName) && _priorFormEntry.GetValue(fieldName) == null))
                {
                    return true;
                }

                if (pfc.UsesProgressiveLogic)
                {
                    List<CriteriaItem> checks = Helpers.DeserializeJSON<List<CriteriaItem>>(pfc.ProgressiveCriteriaSet);

                    foreach (CriteriaItem c in checks)
                    {
                        var checkField = this.FieldControls.Where(fc => ((SimpleScriptView)fc).ID == c.FieldId).SingleOrDefault();

                        if (checkField != null && _priorFormEntry.DoesFieldExist(checkField.MetaField.FieldName) && _priorFormEntry.GetValue(checkField.MetaField.FieldName) == null)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private FormEntry GetPriorFormEntry()
        {
            FormEntry entry = null;

            if (_sfTrackingCookie != null)
            {
                entry = GetFormEntryByUserId(Guid.Parse(_sfTrackingCookie.Value));
            }

            Guid userId = ClaimsManager.GetCurrentUserId();

            if (entry == null && userId != Guid.Empty)
            {
                entry = GetFormEntryByUserId(userId);
            }

            return entry;
        }

        private FormEntry GetPriorFormEntryByKeyField(string keyFieldValue)
        {
            FormDescription form = FManager.GetForms().Where(f => f.Name == this.FormData.Name).SingleOrDefault();

            return FManager.GetFormEntries(form).Where(e => e.GetValue<string>(_progressiveKeyFieldName) == keyFieldValue).FirstOrDefault();
        }

        private FormEntry GetFormEntryByUserId(Guid userId)
        {
            FormDescription form = FManager.GetForms().Where(f => f.Name == this.FormData.Name).SingleOrDefault();

            return FManager.GetFormEntries(form).Where(e => e.UserId == userId).FirstOrDefault();
        }
    }
}