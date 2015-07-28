Type.registerNamespace("timw255.Sitefinity.SuperForms.Widgets.Form");

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormCheckboxes = function (element) {
    timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormCheckboxes.initializeBase(this, [element]);
}

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormCheckboxes.prototype = {
    /* --------------------------------- set up and tear down ---------------------------- */
    initialize: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormCheckboxes.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormCheckboxes.callBaseMethod(this, 'dispose');
    }

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */
}

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormCheckboxes.registerClass('timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormCheckboxes', Telerik.Sitefinity.Web.UI.Fields.ChoiceField);
