<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="GrafSeguimientoCotizaciones.aspx.cs" Inherits="crm_valvulas_industriales.GrafSeguimientoCotizaciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script  type="text/javascript" language="javascript" src="assets/JSClass/FusionCharts.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<br />
    <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li><a href="Default.aspx">Paneles</a></li>
          <li class="active">Seguimiento Cotizaciones</li>
    </ol>
    
    <div class="panel panel-primary">
    <div class="panel-heading">
        <strong>Modulo Paneles - Seguimiento Cotizaciones</strong>
    </div>

    <table class="table table-condensed small">
    <tr class="active">
        <td width="200px">
            <strong>Ejecutivo</strong>
            <asp:DropDownList ID="ddlVendedorIndicador" CssClass="form-control input-sm" runat="server" OnDataBound="ddlVendedorIndicador_DataBound">
            </asp:DropDownList>
        </td>
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
            <asp:LinkButton ID="lbtnBuscar" CssClass="btn btn-danger btn-sm" runat="server" onclick="lbtnBuscar_Click" ><i aria-hidden="true" class="glyphicon glyphicon-search"></i> Buscar</asp:LinkButton>
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
                <asp:Literal ID="litSeguimientoCotizaciones" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
    </div>


    
            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>Seguimiento</strong>
                    <asp:ImageButton ID="ibtnExportar" ImageUrl="~/assets/img/export_excel.png" OnClick="ibtnExportar_Click" runat="server" />
                </div>
                
                <asp:GridView ID="grvSeguimiento" runat="server" HeaderStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed small" AutoGenerateColumns="false" AllowPaging="True" PageSize="30" OnRowDataBound="paginacion_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="ibtnIrAGestion" ImageUrl="~/assets/img/note_go.png" runat="server" OnClick="ibtnIrAGestion_Click" />
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
                    <asp:TemplateField HeaderText="Cotización" SortExpression="ID_COTIZACION">
                        <ItemTemplate>
                            <asp:Label ID="lblCotizacion" runat="server" Visible="true"  Text='<%# Bind("ID_COTIZACION") %>'></asp:Label>
                            <%--<asp:LinkButton ID="lbtnCotizacion" runat="server" Text='<%# Bind("ID_COTIZACION") %>' OnClick="lbtnDetalleCotizacionSeguimiento_Click"></asp:LinkButton>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PDF">
                        <ItemTemplate>
                            <asp:Label ID="lblRutaPdf" runat="server" Visible="false"  Text='<%# Bind("RUTA_COTIZACION") %>'></asp:Label>
                            <asp:ImageButton ID="imgPdf" ImageUrl="~/assets/img/file_extension_pdf.png" OnClick="imgPdf_Click" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fec Cotización" SortExpression="FECHA">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaCotizacion" runat="server"  Text='<%# Bind("FECHA", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Monto Cot" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_NETO">
                        <ItemTemplate>
                            <asp:Label ID="lblMontoCotizacion" runat="server" Visible="false" Text='<%# Bind("MONTO_NETO") %>'></asp:Label>
                            <asp:Label ID="lblMontoCotizacion2" runat="server" Text='<%# Eval("MONTO_NETO", "{0:n0}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha Validez" SortExpression="FECHA_VALIDEZ">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaValidez" runat="server"  Text='<%# Bind("FECHA_VALIDEZ", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estado" SortExpression="NOM_ESTADO_COTIZACION">
                        <ItemTemplate>
                            <asp:Label ID="lblEstadoCotizacion" runat="server" ToolTip='<%# Bind("MOTIVO_RECHAZO") %>'  Text='<%# Bind("NOM_ESTADO_COTIZACION") %>'></asp:Label>
                            <asp:Label ID="lblIdEstadoCotizacion" runat="server" Visible="false" Text='<%# Bind("ID_ESTADO_COTIZACION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Usuario asig" SortExpression="USUARIO">
                        <ItemTemplate>
                            <asp:Label ID="lblIdUsuarioAsig" runat="server" Visible="false"  Text='<%# Bind("ID_USUARIO_ASIG") %>'></asp:Label>
                            <asp:Label ID="lblUsuario" runat="server" Text='<%# Bind("USUARIO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fec Gestión">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaGestion" runat="server"  Text='<%# Bind("FECHA_ULT_GESTION", "{0:dd/MM/yyyy}") %>'></asp:Label>
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



    
<asp:Button ID="btnActivarPopUp" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="mdlInformacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlInformacion" TargetControlID="btnActivarPopUp" BehaviorID="_mdlInformacion">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlInformacion" runat="server" DefaultButton="imgAceptar" style="display:none; background:white; width:auto; height:auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                  <div class="modal-header">
                        <button class="close" data-dismiss="modal">×</button>
                        <h3>Información</h3>
                  </div>
                  <div class="modal-body">
                      <asp:Label ID="lblInformacion" runat="server" Text="" CssClass="alert"></asp:Label>
                  </div>
                  <div class="modal-footer">
                    <asp:ImageButton ID="imgAceptar" runat="server"  ImageUrl="~/assets/img/accept.png" />
                  </div>
                <%--MODALPOPUP CON BOOTSTRAP--%>
            </asp:Panel>

</asp:Content>
