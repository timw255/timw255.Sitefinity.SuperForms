using timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views;
using System.Collections.Generic;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace timw255.Sitefinity.SuperForms.Widgets.Form.Designers
{
    public class CustomCheckboxesDesigner : FormCheckboxesDesigner
    {
        protected override void AddViews(Dictionary<string, ControlDesignerView> views)
        {
            base.AddViews(views);

            ConditionalLogicView customLogicView = new ConditionalLogicView();
            views.Add(customLogicView.ViewName, customLogicView);
            ProgressiveProfilingView progressiveProfilingView = new ProgressiveProfilingView();
            views.Add(progressiveProfilingView.ViewName, progressiveProfilingView);
        }
    }
}