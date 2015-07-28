<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>

<sf:ResourceLinks ID="resourcesLinks" runat="server">
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_common_min.css" Static="True" />
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_default_min.css" Static="True" />
    <sf:ResourceFile JavaScriptLibrary="KendoAll" />
</sf:ResourceLinks>

<div id="progressiveView">
    <div id="keyField"><input type="checkbox" id="isProgressiveKey" /><span id="keyFieldMessage"> Use this field to identify previously submitted responses.</span></div>
    <div><input type="checkbox" id="usesProgressiveLogic" /> Show this field only if the following fields have been completed.</div>
    <div id="progressiveLogic" style="display:none;">
        <div id="progressiveCriteriaList">
            <table id="progressiveCriteria">
                <thead><tr><th class="fieldname">Field name</th><th class="delete"></th></tr></thead>
                <tbody></tbody>
            </table>
            <script type="text/x-kendo-tmpl" id="progressiveCriteriaTemplate">
                <tr>
                    <td class="fieldname">#: getProgressiveFieldName(fieldId) #</td>
                    <td class="delete"><a class="deleteCriteria sfDeleteBtn" data-id="#: id #" title="Remove"></a></td>
                </tr>
            </script>
        </div>

        <div id="progressiveCriteriaBuilder">
            <select id="progressiveField"></select>
            <a class="sfLinkBtn" id="addProgressiveCriteria"><strong class="sfLinkBtnIn">Add</strong></a>
        </div>
    </div>
</div>

<style>
    #keyField { margin-bottom: 15px; }
    #progressiveView, #progressiveLogic { margin-top: 20px; }
    #progressiveLogic { width: 360px; }
    #progressiveCriteriaBuilder { margin-top: 25px; }
    #progressiveCriteriaBuilder .k-dropdown + .k-dropdown { margin-left: 5px; }
    #addProgressiveCriteria { display: inline-block; margin-left: 3px; }
    #progressiveCriteriaList { padding: 10px; margin-top: 20px; height: 200px; background-color: #fff; border: 1px solid #c5c5c5; }
    #progressiveCriteria thead th { padding: 5px 4px 10px; white-space: nowrap; font-size: 9px; letter-spacing: 1px; text-transform: uppercase; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #EEE; font-weight: normal; color: #777; }
    #progressiveCriteria td { padding: 5px 3px; }
    #progressiveCriteria .fieldname { width: 304px; }
    #progressiveCriteria .delete { width: 18px; text-align: right; }
    #progressiveField-list { min-width: 160px !important; width: auto !important; }
    #progressiveField_listbox { width: auto !important; }
</style>

<asp:Literal ID="Script" runat="server" />

<script>
    var dynamicOptionFilter = { logic: "and", filters: [] };

    for (var i = 0; i < progressiveCriteria.length; i++) {
        dynamicOptionFilter.filters.push({ field: "fieldName", operator: "neq", value: progressiveCriteria[i].fieldName });
    }

    dynamicOptionFilter.filters.push({ field: "fieldName", operator: "neq", value: progressiveOptionFilter });

    var progressiveCriteriaOptionsDataSource = new kendo.data.DataSource({
        data: progressiveCriteriaOptions,
        schema: {
            model: {
                id: "fieldId"
            }
        },
        filter: dynamicOptionFilter
    });

    progressiveCriteriaOptionsDataSource.read();

    var progressiveCriteriaDataSource = new kendo.data.DataSource({
        data: progressiveCriteria,
        schema: {
            model: { id: "id" }
        },
        filter: { field: "culture", operator: "eq", value: currentProgressiveCulture }
    });

    progressiveCriteriaDataSource.bind("change", function (e) {
        var html = kendo.render(kendo.template($("#progressiveCriteriaTemplate").html()), this.view());

        $("#progressiveCriteria tbody").html(html);
    });

    progressiveCriteriaDataSource.read();

    $("#progressiveField").kendoDropDownList({
        dataTextField: "fieldName",
        dataValueField: "fieldId",
        dataSource: progressiveCriteriaOptionsDataSource
    });

    if (progressiveCriteriaOptionsDataSource.view().length < 1) {
        $("#progressiveField").data("kendoDropDownList").setDataSource([{ fieldName: "" }]);
        $("#progressiveField").data("kendoDropDownList").enable(false);
        $("#addProgressiveCriteria").addClass("sfDisabledLinkBtn");
    }

    $("#progressiveField").data("kendoDropDownList").list.css("min-width", "168px");
    $("#progressiveField").closest(".k-widget").width(170);

    $("#addProgressiveCriteria").click(function (e) {
        e.preventDefault();

        if ($("#progressiveField").val() == "") {
            return false;
        }

        var item = progressiveCriteriaOptionsDataSource.get($("#progressiveField").val()),
            newCriteria = {};

        newCriteria.culture = currentProgressiveCulture;
        newCriteria.id = kendo.guid();
        newCriteria.fieldId = $("#progressiveField").val();

        progressiveCriteriaDataSource.add(newCriteria);

        dynamicOptionFilter.filters.push({ field: "fieldId", operator: "neq", value: $("#progressiveField").val() });

        progressiveCriteriaOptionsDataSource.filter(dynamicOptionFilter);

        if (progressiveCriteriaOptionsDataSource.view().length < 1) {
            $("#progressiveField").data("kendoDropDownList").setDataSource([{ fieldName: "" }]);
            $("#progressiveField").data("kendoDropDownList").enable(false);
            $("#addProgressiveCriteria").addClass("sfDisabledLinkBtn");
        }
    });

    $("#progressiveCriteria").on("click", ".deleteCriteria", function (e) {
        e.preventDefault();

        var $target = $(e.currentTarget),
            id = $target.data("id"),
            item = progressiveCriteriaDataSource.get(id);

        for (var i = 0; i < dynamicOptionFilter.filters.length; i++) {
            if (dynamicOptionFilter.filters[i].value === item.fieldName) {
                dynamicOptionFilter.filters.splice(i, 1);
            }
        }

        progressiveCriteriaOptionsDataSource.filter(dynamicOptionFilter);

        if (progressiveCriteriaOptionsDataSource.view().length > 0) {
            $("#progressiveField").data("kendoDropDownList").setDataSource(progressiveCriteriaOptionsDataSource);
            $("#progressiveField").data("kendoDropDownList").enable(true);
            $("#addProgressiveCriteria").removeClass("sfDisabledLinkBtn");
        }

        progressiveCriteriaDataSource.remove(item);
    });

    $('#isProgressiveKey').click(function () {
        if (this.checked) {
            $("#usesProgressiveLogic").attr("checked", false).attr("disabled", "disabled");
            $("#progressiveLogic").toggle(false);
        } else {
            $("#usesProgressiveLogic").removeAttr("disabled");
        }
    });

    $('#usesProgressiveLogic').click(function () {
        $("#progressiveLogic").toggle(this.checked);
    });

    function getProgressiveFieldName(fieldId) {
        var dataItem = progressiveCriteriaOptionsDataSource.get(fieldId);
        return dataItem.fieldName;
    }
</script>