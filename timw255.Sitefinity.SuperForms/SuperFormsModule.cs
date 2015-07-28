using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent.Modules;
using Telerik.Sitefinity.Fluent.Modules.Toolboxes;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using timw255.Sitefinity.SuperForms.Widgets.Form;
using timw255.Sitefinity.SuperForms.FormControls;

namespace timw255.Sitefinity.SuperForms
{
    /// <summary>
    /// Custom Sitefinity module 
    /// </summary>
    public class SuperFormsModule : ModuleBase
    {
        #region Properties
        /// <summary>
        /// Gets the landing page id for the module.
        /// </summary>
        /// <value>The landing page id.</value>
        public override Guid LandingPageId
        {
            get
            {
                return SiteInitializer.DashboardPageNodeId;
            }
        }

        /// <summary>
        /// Gets the CLR types of all data managers provided by this module.
        /// </summary>
        /// <value>An array of <see cref="T:System.Type" /> objects.</value>
        public override Type[] Managers
        {
            get
            {
                return new Type[0];
            }
        }
        #endregion

        #region Module Initialization
        /// <summary>
        /// Initializes the service with specified settings.
        /// This method is called every time the module is initializing (on application startup by default)
        /// </summary>
        /// <param name="settings">The settings.</param>
        public override void Initialize(ModuleSettings settings)
        {
            base.Initialize(settings);

            App.WorkWith()
                .Module(settings.Name)
                    .Initialize()
                    .Localization<SuperFormsResources>();
        }

        /// <summary>
        /// Installs this module in Sitefinity system for the first time.
        /// </summary>
        /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
        public override void Install(SiteInitializer initializer)
        {
            this.InstallVirtualPaths(initializer);
            this.InstallPageWidgets(initializer);
            this.InstallFormWidgets(initializer);
        }

        /// <summary>
        /// Uninstalls the specified initializer.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public override void Uninstall(SiteInitializer initializer)
        {
            base.Uninstall(initializer);
        }
        #endregion

        #region Public and overriden methods
        /// <summary>
        /// Gets the module configuration.
        /// </summary>
        protected override ConfigSection GetModuleConfig()
        {
            return null;
        }
        #endregion

        #region Virtual paths
        /// <summary>
        /// Installs module virtual paths.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        private void InstallVirtualPaths(SiteInitializer initializer)
        {
            var virtualPathConfig = initializer.Context.GetConfig<VirtualPathSettingsConfig>();
            var virtualPathElement = new VirtualPathElement(virtualPathConfig.VirtualPaths)
            {
                VirtualPath = "~/SuperForms/" + "*",
                ResolverName = "EmbeddedResourceResolver",
                ResourceLocation = "timw255.Sitefinity.SuperForms"
            };
            if (!virtualPathConfig.VirtualPaths.ContainsKey("~/SuperForms/" + "*"))
            {
                virtualPathConfig.VirtualPaths.Add(virtualPathElement);
                Config.GetManager().SaveSection(virtualPathConfig);
            }
        }
        #endregion

        #region Widgets
        /// <summary>
        /// Installs the form widgets.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        private void InstallFormWidgets(SiteInitializer initializer)
        {
            initializer.Installer
                .Toolbox(CommonToolbox.FormWidgets)
                    .LoadOrAddSection("SuperForms")
                        .SetTitle("SuperForms Controls")
                        .SetDescription("SuperForms field controls")
                        .LoadOrAddWidget<LogicalFormTextBox>("LogicalFormTextBox")
                            .SetTitle("Super TextBox")
                            .SetDescription("SuperForms TextBox field")
                            .SetCssClass("sfTextboxIcn")
                        .Done()
                        .LoadOrAddWidget<LogicalFormMultipleChoice>("LogicalFormMultipleChoice")
                            .SetTitle("Super MultipleChoice")
                            .SetDescription("SuperForms MultipleChoice field")
                            .SetCssClass("sfMultipleChoiceIcn")
                        .Done()
                        .LoadOrAddWidget<LogicalFormCheckboxes>("LogicalFormCheckboxes")
                            .SetTitle("Super Checkboxes")
                            .SetDescription("SuperForms Checkboxes field")
                            .SetCssClass("sfCheckboxesIcn")
                        .Done()
                        .LoadOrAddWidget<LogicalFormParagraphTextBox>("LogicalFormParagraphTextBox")
                            .SetTitle("Super ParagraphTextBox")
                            .SetDescription("SuperForms ParagraphTextBox field")
                            .SetCssClass("sfParagraphboxIcn")
                        .Done()
                        .LoadOrAddWidget<LogicalFormDropDownList>("LogicalFormDropDownList")
                            .SetTitle("Super DropDownList")
                            .SetDescription("SuperForms DropDownList field")
                            .SetCssClass("sfDropdownIcn")
                        .Done()
                        .LoadOrAddWidget<LogicalFormSectionHeader>("LogicalFormSectionHeader")
                            .SetTitle("Super SectionHeader")
                            .SetDescription("SuperForms SectionHeader")
                            .SetCssClass("sfSectionHeaderIcn")
                        .Done()
                        .LoadOrAddWidget<LogicalFormInstructionalText>("LogicalFormInstructionalText")
                            .SetTitle("Super InstructionalText")
                            .SetDescription("SuperForms InstructionalText")
                            .SetCssClass("sfInstructionIcn")
                        .Done()
                    .Done()
                .Done();
        }

        /// <summary>
        /// Installs the page widgets.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        private void InstallPageWidgets(SiteInitializer initializer)
        {
            initializer.Installer
                .Toolbox(CommonToolbox.PageWidgets)
                    .LoadOrAddSection("SuperForms")
                    .SetTitle("SuperForms")
                    .SetDescription("SuperForms")
                    .LoadOrAddWidget<LogicalFormsControl>("LogicalFormsControl")
                            .SetTitle("SuperForms Form")
                            .SetDescription("SuperForms Form control")
                            .SetCssClass("sfFormsIcn")
                    .Done();
        }
        #endregion

        #region Upgrade methods
        #endregion

        #region Private members & constants
        public const string ModuleName = "SuperForms";
        internal const string ModuleTitle = "Super Forms";
        internal const string ModuleDescription = "Conditional logic and progressive profiling for Sitefinity Form Builder.";
        internal const string ModuleVirtualPath = "~/SuperForms/";
        #endregion
    }
}