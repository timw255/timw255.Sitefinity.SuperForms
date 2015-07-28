Type.registerNamespace("timw255.Sitefinity.SuperForms.Widgets.Form");

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormInstructionalText = function (element) {
    timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormInstructionalText.initializeBase(this, [element]);
}

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormInstructionalText.prototype = {
    /* --------------------------------- set up and tear down ---------------------------- */
    initialize: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormInstructionalText.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormInstructionalText.callBaseMethod(this, 'dispose');
    }

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */
}

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormInstructionalText.registerClass('timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormInstructionalText', Telerik.Sitefinity.Web.UI.Fields.ChoiceField);
