<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.Fields" TagPrefix="sf" %>

<sf:ResourceLinks id="resourcesLinks" runat="server">
    <sf:ResourceFile Name="Styles/Window.css" />
</sf:ResourceLinks>

<sf:FormManager ID="formManager" runat="server" />
<div style="width: 660px; overflow: hidden;" class="sfPTop5">
    <sf:HtmlField 
        ID="htmlEditor" 
        runat="server" 
        Width="99%"
        Height="370px"
        EditorContentFilters="DefaultFilters"
        EditorStripFormattingOptions="MSWord,Css,Font,Span,ConvertWordLists"
        DisplayMode="Write"
        FixCursorIssue="True">
    </sf:HtmlField>
</div>
<script type="text/javascript">
    $("body").addClass("sfContentBlockDesigner");
</script>