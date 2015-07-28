Type.registerNamespace("timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views");

timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.InstructionalTextView = function (element) {
    this._propertyEditor = null;
    this._parentDesigner = null;
    this._htmlEditor = null;
    timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.InstructionalTextView.initializeBase(this, [element]);
};

timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.InstructionalTextView.prototype = {
    initialize: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.InstructionalTextView.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.InstructionalTextView.callBaseMethod(this, 'dispose');
    },
    refreshUI: function () {
        var html = this.get_parentDesigner().get_propertyEditor().get_control().Html;
        if (html) {
            this._htmlEditor.set_value(html);
        }
    },
    applyChanges: function () {
        var editor = this._htmlEditor._editControl;
        //trigger switch from "html" to "design" in order filters to be applied.
        if (editor) {
            editor.set_mode(1);
        }

        this.get_parentDesigner().get_propertyEditor().get_control().Html = this._htmlEditor.get_value();
    },
    get_parentDesigner: function () {
        return this._parentDesigner;
    },
    set_parentDesigner: function (value) {
        this._parentDesigner = value;
    },
    // gets the reference to the rad editor control used to edit the
    // html content of the ContentBlock control
    get_htmlEditor: function () {
        return this._htmlEditor;
    },
    // gets the reference to the rad editor control used to edit the
    // html content of the ContentBlock control
    set_htmlEditor: function (value) {
        this._htmlEditor = value;
    }
};

timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.InstructionalTextView.registerClass('timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.InstructionalTextView', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();