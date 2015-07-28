using System;
using System.Collections.Generic;
using System.Text;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;

namespace timw255.Sitefinity.SuperForms.Widgets.Form
{
    internal interface IProgressiveFormControl
    {
        bool UsesProgressiveLogic { get; set; }

        bool IsProgressiveKeyField { get; set; }

        string ProgressiveCriteriaSet { get; set; }
    }
}