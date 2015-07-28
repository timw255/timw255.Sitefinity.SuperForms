Type.registerNamespace("timw255.Sitefinity.SuperForms.Widgets.Form");

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormParagraphTextBox = function (element) {
    timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormParagraphTextBox.initializeBase(this, [element]);
}

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormParagraphTextBox.prototype = {
    /* --------------------------------- set up and tear down ---------------------------- */
    initialize: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormParagraphTextBox.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormParagraphTextBox.callBaseMethod(this, 'dispose');
    }

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */
}

timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormParagraphTextBox.registerClass('timw255.Sitefinity.SuperForms.Widgets.Form.LogicalFormParagraphTextBox', Telerik.Sitefinity.Web.UI.Fields.TextField);
