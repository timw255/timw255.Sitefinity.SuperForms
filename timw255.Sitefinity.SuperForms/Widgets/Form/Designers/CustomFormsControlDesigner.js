/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.8.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type._registerScript("CustomFormsControlDesigner.js", ["FormsControlDesigner.js"]);

Type.registerNamespace("timw255.Sitefinity.SuperForms.Widgets.Form.Designers");

timw255.Sitefinity.SuperForms.Widgets.Form.Designers.CustomFormsControlDesigner = function (element) {
    timw255.Sitefinity.SuperForms.Widgets.Form.Designers.CustomFormsControlDesigner.initializeBase(this, [element]);
}

timw255.Sitefinity.SuperForms.Widgets.Form.Designers.CustomFormsControlDesigner.prototype = {
    /* --------------------------------- set up and tear down ---------------------------- */
    initialize: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.Designers.CustomFormsControlDesigner.callBaseMethod(this, 'initialize');
        var controlData = this.get_controlData();

        jQuery("#extendedFormType").val(controlData.ExtendedFormType);
    },
    dispose: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.Designers.CustomFormsControlDesigner.callBaseMethod(this, 'dispose');
    },
    refreshUI: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.Designers.CustomFormsControlDesigner.callBaseMethod(this, 'refreshUI');

        var controlData = this.get_controlData();

        jQuery("#extendedFormType").val(controlData.ExtendedFormType);
    },
    applyChanges: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.Designers.CustomFormsControlDesigner.callBaseMethod(this, 'applyChanges');

        var controlData = this.get_controlData();

        controlData.ExtendedFormType = jQuery("#extendedFormType").val();
    }

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */
}

timw255.Sitefinity.SuperForms.Widgets.Form.Designers.CustomFormsControlDesigner.registerClass('timw255.Sitefinity.SuperForms.Widgets.Form.Designers.CustomFormsControlDesigner', Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormsControlDesigner);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();