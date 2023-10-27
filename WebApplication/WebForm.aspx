<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm.aspx.cs" Inherits="WebApplication.WebForm" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btNewRegistro" runat="server" Text="New Registro" OnClick="btNovoRegistro_Click" />
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>

            <%-- Grid Categories --%>
            <telerik:RadGrid 
                ID="RadGrid1"
                runat="server"
                OnNeedDataSource="RadGrid1_NeedDataSource"          
                OnItemCommand="RadGrid1_ItemCommand"             
                AutoGenerateColumns="False" Height="316px">
                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                <ExportSettings>
                    <Pdf PageWidth=""></Pdf>
                </ExportSettings>
                <MasterTableView DataKeyNames="CategoryID">         
                    <Columns>
                        <telerik:GridTemplateColumn                   
                            UniqueName="OP"                          
                            AllowFiltering="false">     
                            <ItemTemplate>
                                <asp:LinkButton 
                                    ID="lnkEditar" 
                                    runat="server" 
                                    CommandName="opEditar">
                                    Editar
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn 
                            FilterControlAltText="Filter column column" 
                            UniqueName="column" 
                            DataField="CategoryName" 
                            HeaderText="CategoryName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn 
                            FilterControlAltText="Filter column1 column" 
                            UniqueName="column1" DataField="Description" 
                            HeaderText="Description">
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>

            <%-- Painel Editar --%>
            <asp:Panel ID="pnlForm" runat="server" Visible="false">
                <asp:Table ID="tb_CampoEditar" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <telerik:RadLabel ID="lbCategoryName" runat="server" Text="Categoria"></telerik:RadLabel>
                            <br />
                            <telerik:RadTextBox ID="txtCategoryName" runat="server"></telerik:RadTextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <telerik:RadLabel ID="lbDescription" runat="server" Text="Descrição"></telerik:RadLabel>
                            <br />
                            <telerik:RadTextBox ID="txtDescription" runat="server" Width="250px"></telerik:RadTextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <telerik:RadButton ID="btSalvar" runat="server" Text="Salvar" OnClick="btSalvar_Click1"></telerik:RadButton>&nbsp
                <telerik:RadButton ID="btCancelar" runat="server" Text="Cancelar" OnClick="btCancelar_Click"></telerik:RadButton>
            </asp:Panel>

            <asp:HiddenField ID="hdfCategoryID" runat="server" />
        </div>
    </form>
</body>
</html>
