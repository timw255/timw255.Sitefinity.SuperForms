using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views;

namespace timw255.Sitefinity.SuperForms.Widgets.Form.Designers
{
    class CustomInstructionalTextDesigner : ContentViewDesignerBase
    {
        protected override string ScriptDescriptorTypeName
        {
            get
            {
                return typeof(ContentViewDesignerBase).FullName;
            }
        }

        protected override Type ResourcesAssemblyInfo
        {
            get
            {
                return typeof(Telerik.Sitefinity.Resources.Reference);
            }
        }

        protected override void AddViews(Dictionary<string, ControlDesignerView> views)
        {
            InstructionalTextView instructionalTextView = new InstructionalTextView();
            views.Add(instructionalTextView.ViewName, instructionalTextView);
            ConditionalLogicView customLogicView = new ConditionalLogicView();
            views.Add(customLogicView.ViewName, customLogicView);
        }
    }
}
