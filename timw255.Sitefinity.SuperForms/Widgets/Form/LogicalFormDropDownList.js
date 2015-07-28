Type.registerNamespace("timw255.Sitefinity.SuperForms.Widgets.Form");

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormDropDownList = function (element) {
    timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormDropDownList.initializeBase(this, [element]);
}

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormDropDownList.prototype = {
    /* --------------------------------- set up and tear down ---------------------------- */
    initialize: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormDropDownList.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormDropDownList.callBaseMethod(this, 'dispose');
    }

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */
}

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormDropDownList.registerClass('timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormDropDownList', Telerik.Sitefinity.Web.UI.Fields.ChoiceField);
