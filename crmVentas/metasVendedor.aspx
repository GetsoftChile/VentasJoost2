<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="metasVendedor.aspx.cs" Inherits="crm_valvulas_industriales.metasVendedor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <script type="text/javascript" language="javascript" src="assets/JSClass/FusionCharts.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <ol class="breadcrumb">
        <li><a href="Default.aspx">Inicio</a></li>
        <li class="active">Metas Vendedor</li>
    </ol>

<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <strong>Metas</strong>
                </div>
                <table class="table table-condensed small">
                    <tr class="success">
                        <td>
                            <strong>Mes</strong>
                            <asp:DropDownList ID="ddlMes" runat="server" CssClass="form-control input-sm">
                                <asp:ListItem Value="1" Text="Enero"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Febrero"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Marzo"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Abril"></asp:ListItem>
                                <asp:ListItem Value="5" Text="Mayo"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Junio"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Julio"></asp:ListItem>
                                <asp:ListItem Value="8" Text="Agosto"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Septiembre"></asp:ListItem>
                                <asp:ListItem Value="10" Text="Octubre"></asp:ListItem>
                                <asp:ListItem Value="11" Text="Noviembre"></asp:ListItem>
                                <asp:ListItem Value="12" Text="Diciembre"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <strong>Ano</strong>
                            <asp:DropDownList ID="ddlAno" runat="server" CssClass="form-control input-sm">
                                <asp:ListItem Value="2017"></asp:ListItem>
                                <asp:ListItem Value="2018"></asp:ListItem>
                                <asp:ListItem Value="2019"></asp:ListItem>
                                <asp:ListItem Value="2020"></asp:ListItem>
                                <asp:ListItem Value="2021"></asp:ListItem>
                                <asp:ListItem Value="2022"></asp:ListItem>
                                <asp:ListItem Value="2023"></asp:ListItem>
                                <asp:ListItem Value="2024"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <strong>Vendedor</strong>
                            <asp:DropDownList ID="ddlVendedor" runat="server" OnDataBound="ddlVendedor_DataBound" CssClass="form-control input-sm">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <br />
                            <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-default btn-sm" Text="Buscar" OnClick="btnBuscar_Click" />
                        </td>
                    </tr>
                </table>
            </div>

            <div class="row">
                <div class="col-sm-6">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                        </div>
                        <div class="panel-body">
                            <asp:Literal ID="litGrafico" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>




            <asp:Button ID="btnActivarPopUp" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mdlInformacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlInformacion" TargetControlID="btnActivarPopUp" BehaviorID="_mdlInformacion">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlInformacion" runat="server" CssClass="panel" Style="display: none; background: white; width: 40%; height: auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <button class="close" data-dismiss="modal">×</button>
                        <strong>Información</strong>
                    </div>
                    <div class="alert">
                        <asp:Label ID="lblInformacion" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="panel-footer">
                        <asp:ImageButton ID="imgAceptar" runat="server" ImageUrl="~/assets/img/accept.png" />

                    </div>
                </div>
                <%--MODALPOPUP CON BOOTSTRAP--%>
            </asp:Panel>


       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>



</asp:Content>
