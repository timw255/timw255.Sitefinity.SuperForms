using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views;

namespace timw255.Sitefinity.SuperForms.Widgets.Form.Designers
{
    public class CustomSectionHeaderDesigner : FormSectionHeaderDesigner
    {
        protected override void AddViews(Dictionary<string, ControlDesignerView> views)
        {
            base.AddViews(views);

            ConditionalLogicView customLogicView = new ConditionalLogicView();
            views.Add(customLogicView.ViewName, customLogicView);
        }
    }
}
