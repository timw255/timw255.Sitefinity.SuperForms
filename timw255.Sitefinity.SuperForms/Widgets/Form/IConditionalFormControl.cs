using System;
using System.Collections.Generic;
using System.Text;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;

namespace timw255.Sitefinity.SuperForms.Widgets.Form
{
    internal interface IConditionalFormControl
    {
        bool UsesConditionalLogic { get; set; }

        int Action { get; set; }

        int Quantity { get; set; }

        string CriteriaSet { get; set; }

        string TargetId { get; set; }
    }
}