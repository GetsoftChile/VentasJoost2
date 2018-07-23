<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="GrafClientesIngresados.aspx.cs" Inherits="crm_valvulas_industriales.GrafClientesIngresados" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script  type="text/javascript" language="javascript" src="assets/JSClass/FusionCharts.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<br />
    <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li><a href="Default.aspx">Paneles</a></li>
          <li class="active">Clientes Nuevos</li>
    </ol>

    <div class="panel panel-primary">
    <div class="panel-heading">
        <strong>Modulo Paneles - Clientes Nuevos</strong>
    </div>

    <table class="table table-condensed small">
    <tr class="active">
        <td width="200px">
            <strong>Fecha Desde</strong>
            <asp:TextBox ID="txtFechaDesde" Width="105px" runat="server" MaxLength="10" CssClass="form-control input-sm"></asp:TextBox>
                    <asp:CalendarExtender ID="ce_txtFecha" runat="server"
                            Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
                            TargetControlID="txtFechaDesde" ></asp:CalendarExtender>
        </td>
        <td width="200px">
            <strong>Fecha Hasta</strong>
            <asp:TextBox ID="txtFechaHasta" Width="105px" runat="server" MaxLength="10" CssClass="form-control input-sm"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy"
                    TargetControlID="txtFechaHasta"></asp:CalendarExtender>
        </td>
        <td>
            <br />
            <asp:LinkButton ID="lbtnBuscar" CssClass="btn btn-danger btn-sm" runat="server" onclick="lbtnBuscar_Click">Buscar</asp:LinkButton>
        </td>
    </tr>
    </table>
    </div>

    <br />
    <div class="row">
    <div class="col-sm-12">
        <div class="panel panel-success">
            <div class="panel-heading">
                <strong></strong>
            </div>
            <div class="panel-body">
                <asp:Literal ID="litClientesNuevos" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
    </div>

    <br />
    <div class="row">
    <div class="col-sm-12">
        <div class="panel panel-success">
            <div class="panel-heading">
                <strong>Clientes Nuevos</strong>
                <asp:ImageButton ID="ibtnExportar" OnClick="ibtnExportar_Click" ImageUrl="~/assets/img/export_excel.png" runat="server" />
            </div>
            
                <asp:GridView ID="grvClientesNuevos" runat="server" HeaderStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed small" AutoGenerateColumns="false" AllowPaging="True" PageSize="30" OnRowDataBound="paginacion_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Id Cliente">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnIdCliente" runat="server" Text='<%# Bind("ID_CLIENTE") %>' OnClick="lbtnRut_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Rut Cliente">
                        <ItemTemplate>
                            <asp:Label ID="lblRut" runat="server" Visible="false" Text='<%# Bind("RUT_CLIENTE") %>'></asp:Label>
                            <asp:LinkButton ID="lbtnRut" runat="server" Text='<%# Bind("RUT_CLIENTE") %>' OnClick="lbtnRut_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cliente">
                        <ItemTemplate>
                            <asp:Label ID="lblNombreCliente" runat="server" Text='<%# Bind("RAZON_SOCIAL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Monto Credito" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblMontoCredito" runat="server" Visible="true" Text='<%# Bind("MONTO_CREDITO", "{0:n0}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Clasificación">
                        <ItemTemplate>
                            <asp:Label ID="lblClasificacion" runat="server" Visible="true" Text='<%# Bind("CLASIFICACION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fec Ingreso">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaIngreso" runat="server"  Text='<%# Bind("FECHA_INGRESO", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <asp:Label ID="lblEstadoCliente" runat="server"  Text='<%# Bind("NOM_ESTADO_CLIENTE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Usuario asig">
                        <ItemTemplate>
                            <asp:Label ID="lblUsuario" runat="server" Text='<%# Bind("USUARIO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>

                <PagerTemplate>
                    <div>
                        <div style="float:left">
                            <asp:ImageButton ID="imgFirst" runat="server"  
                                ImageUrl="~/assets/img/grid/first.gif" onclick="imgFirst_Click" 
                                style="height: 15px" title="Navegación: Ir a la Primera Pagina" Width="26px" />
                            <asp:ImageButton ID="imgPrev" runat="server" 
                                ImageUrl="~/assets/img/grid/prev.gif" onclick="imgPrev_Click" 
                                title="Navegación: Ir a la Pagina Anterior" Width="26px" />
                            <asp:ImageButton ID="imgNext" runat="server"
                                ImageUrl="~/assets/img/grid/next.gif" onclick="imgNext_Click" 
                                title="Navegación: Ir a la Siguiente Pagina" Width="26px" />
                            <asp:ImageButton ID="imgLast" runat="server" 
                                ImageUrl="~/assets/img/grid/last.gif" onclick="imgLast_Click" 
                                title="Navegación: Ir a la Ultima Pagina" Width="26px" />
                        </div>
                        <div style="float:right">
                            Página
                            <asp:Label ID="lblPagina" runat="server"></asp:Label>
                            de
                            <asp:Label ID="lblTotal" runat="server"></asp:Label>
                        </div>
                    </div>
                </PagerTemplate>
            </asp:GridView>
        </div>
    </div>
    </div>


        <asp:Button ID="btnActivarPopUp" runat="server" style="display:none" />
        <asp:ModalPopupExtender ID="mdlInformacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlInformacion" TargetControlID="btnActivarPopUp" BehaviorID="_mdlInformacion">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlInformacion" runat="server" CssClass="panel" style="display:none; background:white; width:40%; height:auto">
            <%--MODALPOPUP CON BOOTSTRAP--%>
            <div class="panel panel-red">
            <div class="panel-heading">
                <button class="close" data-dismiss="modal">×</button>
                    <strong>Información</strong>
            </div>
                <div class="alert">
                    <asp:Label ID="lblInformacion" runat="server" Text=""></asp:Label>
                </div>
            <div class="panel-footer">
                <asp:ImageButton ID="imgAceptar" runat="server" ImageUrl="~/assets/img/accept.png"/>
                    
            </div>
            </div>
            <%--MODALPOPUP CON BOOTSTRAP--%>
        </asp:Panel>


 
</asp:Content>
