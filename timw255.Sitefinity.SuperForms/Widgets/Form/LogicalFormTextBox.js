Type.registerNamespace("timw255.Sitefinity.SuperForms.Widgets.Form");

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormTextBox = function (element) {
    timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormTextBox.initializeBase(this, [element]);
};

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormTextBox.prototype = {
    /* --------------------------------- set up and tear down ---------------------------- */
    initialize: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormTextBox.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormTextBox.callBaseMethod(this, 'dispose');
    }

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */
};

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormTextBox.registerClass('timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormTextBox', Telerik.Sitefinity.Web.UI.Fields.TextField);
