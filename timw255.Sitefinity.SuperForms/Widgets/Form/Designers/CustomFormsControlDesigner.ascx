<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="designers" Namespace="Telerik.Sitefinity.Web.UI.ControlDesign" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sitefinity" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<sitefinity:ResourceLinks ID="resourcesLinks" runat="server">
    <sitefinity:ResourceFile Name="Styles/Tabstrip.css" />
</sitefinity:ResourceLinks>

<div id="selectorTag" style="display: none;">
    <telerik:RadTabStrip runat="server" ID="tsFormsWidget" MultiPageID="mpFormsWidget"
        Skin="Default" SelectedIndex="0" CssClass="sfSwitchControlViews" OnClientTabSelected="OnClientTabSelected">
        <tabs>
        <telerik:RadTab runat="server" Text="Form" Value="formSelector"></telerik:RadTab>
        <telerik:RadTab runat="server" Text="Settings" Value="widgetSettings"></telerik:RadTab>
    </tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage runat="server" ID="mpFormsWidget" SelectedIndex="0" CssClass="sfContentViews">
        <telerik:RadPageView ID="RadPageView1" runat="server" Visible="true">
            <sitefinity:SitefinityLabel runat="server" id="SitefinityLabel1" WrapperTagName="h2"  Text="<%$Resources:FormsResources, SelectForm %>" />
            <div id="formSelector" class="sfNoSelectFilter sfNoProdiver">
                <designers:ContentSelector
                    ID="selector"
                    runat="server"
                    ItemType="Telerik.Sitefinity.Forms.Model.FormDescription"
                    ItemsFilter="Visible == true AND Status == Live"
                    TitleText=""
                    BindOnLoad="false"
                    AllowMultipleSelection="false"
                    WorkMode="List"
                    SearchBoxInnerText=""
                    SearchBoxTitleText="<%$Resources:Labels, NarrowByTyping %>"
                    ServiceUrl="~/Sitefinity/Services/Forms/FormsService.svc"
                    ListModeClientTemplate="<strong class='sfItemTitle'>{{Title}}</strong>">
                </designers:ContentSelector>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView2" Visible="true" runat="server">
            <div id="widgetSettings">
                <sitefinity:SitefinityLabel runat="server" id="lblSectionTitle" WrapperTagName="h2"  Text="<%$Resources:FormsResources, ConfirmationOptions %>" />
                <div class="sfCheckBox sfMTop5 sfMBottom10">
                    <asp:CheckBox ID="chkPerWidgetConfirmation" runat="server" Text="<%$Resources:FormsResources, UseCustomConfirmation %>" />
                </div>
                <ul id="panelConfirmation" class="sfDependentFieldCtrl sfRadioList">
                    <li>
                        <asp:RadioButton ID="confirmationSuccess" runat="server" GroupName="confirmation" Text="<%$Resources:FormsResources, ShowMessageForSuccess %>" Checked="true" />
                        <sfFields:TextField ID="successMessageTextField" runat="server" DisplayMode="Write" Title="" CssClass="sfExpandedPropertyDetails" Rows="5" />
                    </li>
                    <li>
                        <asp:RadioButton ID="confirmationRedirect" runat="server" GroupName="confirmation" Text="<%$Resources:FormsResources, RedirectToAPage %>" />
                        <sfFields:TextField ID="redirectUrlTextField" runat="server" DisplayMode="Write" Title="" CssClass="sfExpandedPropertyDetails" Example="<%$Resources:FormsResources, RedirectToAPageExample %>" />
                    </li>
                </ul>
                <h2>Extended Form Type</h2>
                <ul>
                    <li>
                        <select ID="extendedFormType">
                            <option Value="none">None</option>
                            <option Value="conditional">Conditional Logic</option>
                            <option Value="progressive">Progressive Profiling</option>
                        </select>
                    </li>
                </ul>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</div>

<script type="text/javascript">
    function OnClientTabSelected(sender, eventArgs) {
        dialogBase.resizeToContent();
    }
</script>