/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.8.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type._registerScript("ProgressiveProfilingView.js", ["IDesignerViewControl.js"]);

Type.registerNamespace("timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views");

timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.ProgressiveProfilingView = function (element) {
    timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.ProgressiveProfilingView.initializeBase(this, [element]);

    this._controldataFieldNameMap = {};
    this._parentDesigner = null;
    this._refreshing = false;
    this._onLoadDelegate = null;
    this._onUnloadDelegate = null;
    this._resizeControlDesignerDelegate = null;

    this._disableKeyFieldSelector = false;
    this._keyFieldName = null;
};

timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.ProgressiveProfilingView.prototype = {
    initialize: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.ProgressiveProfilingView.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.ProgressiveProfilingView.callBaseMethod(this, 'dispose');
    },
    refreshUI: function () {
        var controlData = this.get_controlData();

        if (controlData.IsProgressiveKeyField) {
            jQuery("#isProgressiveKey").attr("checked", true);
            jQuery("#usesProgressiveLogic").attr("disabled", "disabled");
        }

        if (this._disableKeyFieldSelector) {
            jQuery("#isProgressiveKey").attr("disabled", "disabled");

            if (this._wrongTypeForKeyField) {
                jQuery("#keyField").hide();
            } else {
                jQuery("#keyFieldMessage").html("<strong>Progressive Key Field:</strong> " + this._keyFieldName);
            }
        }

        if (controlData.UsesProgressiveLogic) {
            jQuery("#usesProgressiveLogic").attr("checked", true);
            jQuery("#progressiveLogic").show();
        }

        this.resizeEvents();
    },
    applyChanges: function () {
        var controlData = this.get_controlData();

        controlData.IsProgressiveKeyField = jQuery("#isProgressiveKey").is(':checked');

        controlData.UsesProgressiveLogic = jQuery("#usesProgressiveLogic").is(':checked');

        var progressiveCriteria = [];

        if (controlData.UsesProgressiveLogic & !controlData.IsProgressiveKeyField) {
            progressiveCriteria = progressiveCriteriaDataSource.view();
        }

        controlData.ProgressiveCriteriaSet = JSON.stringify(progressiveCriteria);
    },
    resizeEvents: function () {
        this._resizeControlDesignerDelegate = Function.createDelegate(this, this._resizeControlDesigner);

        var usesProgressiveLogicCheckbox = document.getElementById("usesProgressiveLogic");
        $addHandler(usesProgressiveLogicCheckbox, "click", this._resizeControlDesignerDelegate);

        var isProgressiveKeyCheckbox = document.getElementById("isProgressiveKey");
        $addHandler(isProgressiveKeyCheckbox, "click", this._resizeControlDesignerDelegate);
    },

    // gets the reference to the parent designer control
    get_parentDesigner: function () {
        return this._parentDesigner;
    },

    // sets the reference fo the parent designer control
    set_parentDesigner: function (value) {
        this._parentDesigner = value;
    },

    // gets the name of the currently selected master view name of the content view control
    get_currentViewName: function () {
        return (this._currentViewName) ? this._currentViewName : this.get_controlData().MasterViewName;
    },

    // gets the client side representation of the currently selected master view definition
    get_currentView: function () {
        var currentViewName = this.get_currentViewName();
        var data = this.get_controlData();
        var views = data.ControlDefinition.Views;
        if (views.hasOwnProperty(currentViewName)) {
            return views[currentViewName];
        }
        else {
            var views = data.ControlDefinition.Views;
            for (var v in views) {
                var current = views[v];
                if (current.IsMasterView) {
                    return current;
                }
            }
            return null;
        }
    },

    // this fixes the data if there are some incompatible values set in advanced mode
    _adjustControlData: function (data) {
        var view = data.ControlDefinition.Views[this.get_currentViewName()];
        if (!view) {
            var views = data.ControlDefinition.Views;
            var viewName;
            for (var key in views) {
                if (views[key].IsMasterView) {
                    viewName = key;
                    break;
                }
            }
            data.MasterViewName = viewName;
        }
    },

    _resolvePropertyPath: function (fieldControl) {
        var dataFieldName = this._hidePriceControlDataFieldName;
        var viewPath = "ControlDefinition.Views['" + this.get_currentViewName() + "']";
        return viewPath;
    },

    // gets the object that represents the client side representation of the control 
    // being edited
    get_controlData: function () {
        var parent = this.get_parentDesigner();
        if (parent) {
            var pe = parent.get_propertyEditor();
            if (pe) {
                return pe.get_control();
            }
        }
        alert('Control designer cannot find the control properties object!');
    },

    // function to initialize resizer methods and handlers
    _resizeControlDesigner: function () {
        dialogBase.resizeToContent();
    }
};

timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.ProgressiveProfilingView.registerClass('timw255.Sitefinity.SuperForms.Widgets.Form.Designers.Views.ProgressiveProfilingView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();