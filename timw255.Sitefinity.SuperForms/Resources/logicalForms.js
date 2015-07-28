; (function (window, $) {
    LogicalForms = window.LogicalForms || {};

    LogicalForms = function () {
        this.switchers = [];
        this.switches = [];
        this.validationGroup = "";
        this.oldSubmitOnClick = null;
    },
    LogicalForms.prototype.run = function () {
        this.initFields();
        this.bindChangeEvents();

        var submitButton = $('.logicalForm input[type=submit]');

        this.oldSubmitOnClick = submitButton.attr('onclick');
        submitButton.attr('onclick', null);

        submitButton.bind("click", $.proxy(this.submit, this));
    },
    LogicalForms.prototype.initFields = function () {
        $(".lf-c,.lf-r").each(function () {
            $(this).find("input").each(function () {
                var i = $(this).parent().data("tid");

                $(this).addClass("lf-field" + i + "_").data("tid", "" + i);
            });

            if ($(this).is('.lf-hidden')) {
                this.hideFields(i);
            }
        }).removeClass("lf-c lf-r");

        $('.lf-hidden.lf-required').each(function () {
            if($find($(this).attr('id')) != null) {
                $find($(this).attr('id'))._validator._required = false;
            }
        });
    },
    LogicalForms.prototype.bindChangeEvents = function () {
        for (var i = 0; i < this.switchers.length; i++) {
            var fields = this.getField(this.switchers[i]);

            for (n = 0; n < fields.length; n++) {
                var type = fields[n].type.toLowerCase();

                var event;

                if (type == "radio" || type == "checkbox") {
                    event = "click";
                }
                else {
                    event = "change";
                }

                $(fields[n]).bind(event, $.proxy(this.checkSwitches, this));
            }
        }
    },
    LogicalForms.prototype.checkSwitches = function (field) {
        var fId = "string" != typeof field ? (this.getFieldId(field.target)) : field;

        for (s = 0; s < this.switches.length; s++) {
            if (!(this.switches[s].fields.indexOf(fId) < 0)) {
                var d = this.switches[s];
                var takeAction = false;

                for (var c = 0; c < d.checks.length; c++) {
                    var check = d.checks[c];

                    if (check.condition == "gt" ? takeAction = this.isFieldGreaterThanValue(check.field, check.option) : check.condition == "lt" ? takeAction = this.isFieldLessThanValue(check.field, check.option) : (takeAction = this.isFieldEqualToValue(check.field, check.option), check.condition == "!=" && (takeAction = !takeAction)), d.bool == "AND" && !takeAction) {
                        break;
                    }

                    if (d.bool == "OR" && takeAction) {
                        break;
                    }
                }

                takeAction && "Show" == d.action || !takeAction && "Hide" == d.action ? this.showField(d.target) : this.hideField(d.target);
            }
        }
    },
    LogicalForms.prototype.getFieldContainer = function (fId) {
        return $(".lf-container-" + fId);
    },
    LogicalForms.prototype.getFieldId = function (field) {
        return $(field).data("tid") + "";
    },
    LogicalForms.prototype.getField = function (fId) {
        var b = $(".lf-field" + fId);
        var c = $(".lf-field" + fId + "_");

        return b.length > 0 ? b : c;
    },
    LogicalForms.prototype.getFieldValues = function (fId) {
        var fields = this.getField(fId);
        
        var values = new Array;

        for (f = 0; f < fields.length; f++) {
            var field = fields[f];

            if (field.type == "checkbox" || field.type == "radio") {
                if (field.checked) {
                    values.push(field.value);
                }
            }
            else {
                values.push(field.value);
            }
        }

        return values;
    },
    LogicalForms.prototype.isFieldEqualToOnlyValue = function (fId, value) {
        var fieldValues = this.getFieldValues(fId);
        return fieldValues.length == 1 && fieldValues.indexOf(value) != -1;
    },
    LogicalForms.prototype.isFieldEqualToValue = function (fId, value) {
        return this.getFieldValues(fId).indexOf(value) != -1;
    },
    LogicalForms.prototype.isFieldGreaterThanValue = function (fId, value) {
        return parseInt(this.getFieldValues(fId)[0]) > value;
    },
    LogicalForms.prototype.isFieldLessThanValue = function (fId, value) {
        return parseInt(this.getFieldValues(fId)[0]) < value;
    },
    LogicalForms.prototype.showField = function (fId) {
        var container = $(this.getFieldContainer(fId));

        if (container.is(".lf-hidden")) {
            var fields = container.find("input,select,textarea")

            for (f = 0; f < fields.length; f++) {
                fields[f].disabled = false;
            }

            if (container.is(".lf-required")) {
                if ($find(container.attr('id')) != null) {
                    $find(container.attr('id'))._validator._required = true;
                }
            }

            container.removeClass("lf-hidden");

            for (var s = 0, count = this.switches.length; s < count; s++) {
                if (!(this.switches[s].fields.indexOf(fId) < 0)) {
                    this.checkSwitches(fId);
                }
            }
        }
    },
    LogicalForms.prototype.hideField = function (fId) {
        var container = $(this.getFieldContainer(fId));

        if (!container.is(".lf-hidden")) {
            container.addClass("lf-hidden");

            var fields = container.find("input,select,textarea");

            for (var f = 0; f < fields.length; f++) {
                fields[f].disabled = true;
            }

            if (container.is(".lf-required")) {
                if ($find(container.attr('id')) != null) {
                    $find(container.attr('id'))._validator._required = false;
                }
            }
            
            for (var s = 0, count = this.switches.length; s < count; s++) {
                if (!(this.switches[s].fields.indexOf(fId) < 0)) {
                    var t = this.switches[s];
                    if (t.target != fId) {
                        var x = $(this.getFieldContainer(t.target));

                        if (!x.is(".lf-hidden")) {
                            this.hideField(t.target);
                        }
                    }
                }
            }
        }
    },
    LogicalForms.prototype.submit = function () {
        var hiddenFields = $('.logicalForm .lf-hidden input, .logicalForm .lf-hidden textarea, .logicalForm .lf-hidden select');
        var that = this;

        hiddenFields.each(function () {
            if (this.type == "text" || this.type == "textarea") {
                this.value = this.defaultValue;
            }

            if (this.type == "checkbox") {
                this.checked = false;
            }

            if (this.type == "radio") {
                if ($(this).closest('span').data('defaultvalue') == "True") {
                    $("input:radio[name='" + $(this).attr('name') + "']:first").attr('checked', true);
                }
                else {
                    this.checked = false;
                }
            }

            if (this.type == "select-one") {
                $(this).val($(this).data('defaultvalue'));
            }
        });

        var f = Function(this.oldSubmitOnClick);
        return f();
    }

    window.LogicalForms = LogicalForms;
}(window, jQuery));