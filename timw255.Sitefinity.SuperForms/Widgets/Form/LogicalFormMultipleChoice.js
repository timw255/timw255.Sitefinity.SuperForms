Type.registerNamespace("timw255.Sitefinity.SuperForms.Widgets.Form");

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormMultipleChoice = function (element) {
    timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormMultipleChoice.initializeBase(this, [element]);
}

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormMultipleChoice.prototype = {
    /* --------------------------------- set up and tear down ---------------------------- */
    initialize: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormMultipleChoice.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormMultipleChoice.callBaseMethod(this, 'dispose');
    }

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */
}

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormMultipleChoice.registerClass('timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormMultipleChoice', Telerik.Sitefinity.Web.UI.Fields.ChoiceField);
