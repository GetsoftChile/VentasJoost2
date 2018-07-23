<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="CarteraAsignada.aspx.cs" Inherits="crm_valvulas_industriales.CarteraAsignada" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<br />
    <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li><a href="Default.aspx">Gestión</a></li>
          <li class="active">Cartera Asignada</li>
    </ol>

<h3>Cartera Asignada</h3>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        
    <asp:Panel ID="pnlCarteraAsignada" runat="server" DefaultButton="lbtnBuscar">
        <div class="well well-sm">
            <table class="table table-condensed small">
            <tr>
            <td>
                <strong>Rut</strong>
                <asp:TextBox ID="txtRut" runat="server" CssClass="form-control input-sm" placeholder="Rut Cliente"></asp:TextBox>
            </td>
            <td>
                <strong>Nombre</strong>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control input-sm" placeholder="Nombre Cliente"></asp:TextBox>
            </td>
            <td width="200px"><strong>Ejecutivo</strong>
                <asp:DropDownList ID="ddlEjecutivo" runat="server" CssClass="form-control input-sm" Width="200px" OnDataBound="ddlEjecutivo_DataBound">
                </asp:DropDownList>
            </td>
            <td width="200px"><strong>Campaña</strong>
                <asp:DropDownList ID="ddlCampana" runat="server" CssClass="form-control input-sm" Width="200px" OnDataBound="ddlCampana_DataBound">
                </asp:DropDownList>
            </td>

            <td width="100px">
            <br />
            <asp:LinkButton ID="lbtnBuscar" CssClass="btn btn-primary btn-sm" runat="server" onclick="lbtnBuscar_Click">Buscar</asp:LinkButton>
            </td>
            <td>
            <br />
                <asp:ImageButton ID="ibtnExportarExcel" 
                    ImageUrl="~/assets/img/export_excel.png" runat="server" 
                    onclick="ibtnExportarExcel_Click"/>
            </td>
            </tr>
            </table>
        </div>
        </asp:Panel>


    <asp:GridView ID="grvClientes" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false" AllowPaging="True" PageSize="25" OnRowDataBound="paginacion_RowDataBound"  OnSorting="gvEmployeegrvCotizacionesCRM_Sorting" AllowSorting="true">
                    <Columns>
                        

                        <asp:TemplateField HeaderText="Id Cliente" SortExpression="ID_CLIENTE">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnIdCliente" runat="server" Text='<%# Bind("ID_CLIENTE") %>' OnClick="lbtnIdCliente_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rut" SortExpression="RUT_CLIENTE">
                            <ItemTemplate>
                                <asp:Label ID="lblRut" runat="server" Text='<%# Bind("RUT_CLIENTE") %>' Visible="false"></asp:Label>
                                <asp:LinkButton ID="lbtnRut" runat="server" Text='<%# Bind("RUT_CLIENTE") %>' OnClick="lbtnRut_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Razon Social" SortExpression="RAZON_SOCIAL">
                            <ItemTemplate>
                                <asp:Label ID="lblRazonSocial" runat="server" Text='<%# Bind("RAZON_SOCIAL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Condición de Venta" SortExpression="CONDICION_VENTA">
                            <ItemTemplate>
                                <asp:Label ID="lblCondicionDeVenta" runat="server" Text='<%# Bind("CONDICION_VENTA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Monto Credito" SortExpression="MONTO_CREDITO">
                            <ItemTemplate>
                                <asp:Label ID="lblMontoCredito" runat="server" Text='<%# Bind("MONTO_CREDITO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                       <asp:TemplateField HeaderText="Fec Ult Gest" SortExpression="FECHA_ULT_GESTION">
                            <ItemTemplate>
                                <asp:Label ID="lblFechaUltimaGestion" runat="server" Visible="true" Text='<%# Bind("FECHA_ULT_GESTION") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Estado" SortExpression="NOM_ESTADO_CLIENTE">
                            <ItemTemplate>
                                <asp:Label ID="lblEstado" runat="server" Visible="true" Text='<%# Bind("NOM_ESTADO_CLIENTE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Usuario Asig" SortExpression="USUARIO">
                            <ItemTemplate>
                                <asp:Label ID="lblUsuarioAsig" runat="server" Visible="true" Text='<%# Bind("USUARIO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Fec Ult Cot" SortExpression="ULTIMA_FECHA_COTIZACION">
                            <ItemTemplate>
                                <asp:Label ID="lblFechaUltimaCotizacion" runat="server" Visible="true" Text='<%# Bind("ULTIMA_FECHA_COTIZACION") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Campaña" SortExpression="NOM_CAMPANA">
                            <ItemTemplate>
                                <asp:Label ID="lblCampana" runat="server" Visible="true" Text='<%# Bind("NOM_CAMPANA") %>'></asp:Label>
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




        
        <asp:Button ID="btnActivarPopUp" runat="server" style="display:none" />
        <asp:ModalPopupExtender ID="mdlInformacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlInformacion" TargetControlID="btnActivarPopUp" BehaviorID="_mdlInformacion">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlInformacion" runat="server" CssClass="panel" style="display:none; background:white; width:40%; height:auto">
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
                <asp:ImageButton ID="imgAceptar" runat="server" ImageUrl="~/assets/img/accept.png"/>
            </div>
            </div>
            <%--MODALPOPUP CON BOOTSTRAP--%>
        </asp:Panel>




    </ContentTemplate>
</asp:UpdatePanel>



</asp:Content>
