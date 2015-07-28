<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>

<sf:ResourceLinks ID="resourcesLinks" runat="server">
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_common_min.css" Static="True" />
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_default_min.css" Static="True" />
    <sf:ResourceFile JavaScriptLibrary="KendoAll" />
</sf:ResourceLinks>

<div id="logicView">
    <input type="checkbox" id="usesConditionalLogic" /> <%$Resources:SuperFormsResources, UseConditionalLogicLabel %>
    <div id="conditionalLogic" style="display:none;">
        <div>
            <select id="action">
                <option value="0"><%$Resources:SuperFormsResources, ShowActionOptionText %></option>
                <option value="1"><%$Resources:SuperFormsResources, HideActionOptionText %></option>
            </select>
            <%$Resources:SuperFormsResources, IfLabelText %>
            <select id="quantity">
                <option value="0"><%$Resources:SuperFormsResources, AnyBoolOptionText %></option>
                <option value="1"><%$Resources:SuperFormsResources, AllBoolOptionText %></option>
            </select>
            <%$Resources:SuperFormsResources, OfTheFollowingCriteriaMatch %>
        </div>

        <div id="criteriaList">
            <table id="criteria">
                <thead><tr><th class="fieldname">Field name</th><th class="operator"></th><th class="value">Value</th><th class="delete"></th></tr></thead>
                <tbody></tbody>
            </table>
            <script type="text/x-kendo-tmpl" id="criteriaTemplate">
                <tr>
                    <td class="fieldname">#: getFieldName(fieldId) #</td>
                    <td class="operator">#: condition #</td>
                    <td class="value">#: option #</td>
                    <td class="delete"><a class="deleteCriteria sfDeleteBtn" data-id="#: id #" title="<%$Resources:SuperFormsResources, RemoveButtonText %>"></a></td>
                </tr>
            </script>
        </div>

        <div id="criteriaBuilder">
            <select id="field"></select>
            <select id="operator"></select>
            <select id="choiceValue"></select>
            <input type="text" id="textValue" style="display:none;" />

            <a class="sfLinkBtn" id="addCriteria"><strong class="sfLinkBtnIn"><%$Resources:SuperFormsResources, AddButtonText %></strong></a>
        </div>
    </div>
</div>

<style>
    #logicView, #conditionalLogic { margin-top: 20px; }
    #conditionalLogic { width: 441px; }
    #criteriaBuilder { margin-top: 25px; }
    #criteriaBuilder .k-dropdown + .k-dropdown { margin-left: 5px; }
    #textValue { margin-left: 5px; height: 20px; width: 144px; vertical-align: middle; }
    #operator { width: 50px; }
    #addCriteria { display: inline-block; margin-left: 3px; }
    #criteriaList { padding: 10px; margin-top: 20px; height: 200px; background-color: #fff; border: 1px solid #c5c5c5; }
    #criteria thead th { padding: 5px 4px 10px; white-space: nowrap; font-size: 9px; letter-spacing: 1px; text-transform: uppercase; border-bottom-width: 1px; border-bottom-style: solid; border-bottom-color: #EEE; font-weight: normal; color: #777; }
    #criteria td { padding: 5px 3px; }
    #criteria .fieldname { width: 172px; }
    #criteria .operator { width: 30px; text-align: center; }
    #criteria .value { width: 177px; }
    #criteria .delete { width: 18px; text-align: right; }
    #field-list { min-width: 160px !important; width: auto !important; }
    #field_listbox { width: auto !important; }
</style>

<asp:Literal ID="Script" runat="server" />

<script>
        var criteriaOptionsDataSource = new kendo.data.DataSource({
            data: criteriaOptions,
            schema: {
                model: {
                    id: "fieldId"
                }
            },
            filter: { field: "fieldId", operator: "neq", value: optionFilter }
        });

        criteriaOptionsDataSource.read();

        var criteriaDataSource = new kendo.data.DataSource({
            data: criteria,
            schema: {
                model: { id: "id" }
            },
            filter: { field: "culture", operator: "eq", value: currentCultureC }
        });

        criteriaDataSource.bind("change", function (e) {
            var html = kendo.render(kendo.template($("#criteriaTemplate").html()), this.view());

            $("#criteria tbody").html(html);
        });

        criteriaDataSource.read();

        $("#field").kendoDropDownList({
            dataTextField: "fieldName",
            dataValueField: "fieldId",
            dataSource: criteriaOptionsDataSource,
            change: initCriteriaBuilder
        });

        if (criteriaOptionsDataSource.view().length < 1) {
            $("#field").data("kendoDropDownList").setDataSource([{ fieldName: "" }]);
            $("#field").data("kendoDropDownList").enable(false);
            $("#addCriteria").addClass("sfDisabledLinkBtn");
        }

        $("#field").closest(".k-widget").width(160);

        $("#operator").kendoDropDownList({
            dataTextField: "Text",
            dataValueField: "Value"
        });

        $("#operator").data("kendoDropDownList").list.css("min-width", "58px");
        $("#operator").closest(".k-widget").width(60);

        $("#choiceValue").kendoDropDownList({
            dataTextField: "Text",
            dataValueField: "Value"
        });

        $("#operator").data("kendoDropDownList").list.width("auto");

        $("#addCriteria").click(function (e) {
            e.preventDefault();

            if ($("#field").val() == "") {
                return false;
            }

            var item = criteriaOptionsDataSource.get($("#field").val());
            var newCriteria = {};

            newCriteria.culture = currentCultureC;
            newCriteria.id = kendo.guid();
            newCriteria.fieldId = $("#field").val();
            newCriteria.condition = $("#operator").val();

            if (item.fieldType === "ChoiceField") {
                newCriteria.option = $("#choiceValue").val();
            }
            else {
                newCriteria.option = $("#textValue").val();
            }

            criteriaDataSource.add(newCriteria);
        });

        $("#criteria").on("click", ".deleteCriteria", function (e) {
            e.preventDefault();

            var $target = $(e.currentTarget);
            var id = $target.data("id");
            var item = criteriaDataSource.get(id);

            criteriaDataSource.remove(item);
        });

        $('#usesConditionalLogic').click(function () {
            $("#conditionalLogic").toggle(this.checked);
        });

        function initCriteriaBuilder() {
            var item = criteriaOptionsDataSource.get($("#field").data("kendoDropDownList").value());
            var ddlOperator = $("#operator").data("kendoDropDownList");

            ddlOperator.setDataSource(item.conditions);

            if (item.fieldType === "ChoiceField") {
                var ddlValue = $("#choiceValue").data("kendoDropDownList");

                ddlValue.setDataSource(item.options);

                $("#textValue").hide();
                $("#choiceValue").closest(".k-widget").show();
            }
            else {
                $("#choiceValue").closest(".k-widget").hide();
                $("#textValue").show();
            }
        }

        if (criteriaOptionsDataSource.view().length > 0) {
            initCriteriaBuilder();
        }

        function getFieldName(fieldId) {
            var dataItem = criteriaOptionsDataSource.get(fieldId);
            return dataItem.fieldName;
        }
</script>