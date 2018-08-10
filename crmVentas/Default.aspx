<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="crm_fadonel.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style type="text/css">
        .ajax__tab_xp .ajax__tab_header .ajax__tab_tab {
            height: 21px !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--<script src="assets/js/jsUpdateProgress.js" type="text/javascript"></script>
    <script src="assets/js/jquery-1.11.1.min.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>--%>


    <script type="text/javascript">

        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        //row.style.backgroundColor = "aqua";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            //row.style.backgroundColor = "#C2D69B";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }

    </script>

    <asp:HiddenField ID="hfIdCliente" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfMensaje" runat="server" />

            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                <asp:TabPanel ID="tpCliente" runat="server" HeaderText="Cliente">
                    <ContentTemplate>
                        <br />
                        <div class="col-sm-4">
                            <div class="well well-sm">
                                <div class="form-inline">
                                    <div class="form-group">
                                        Rut, Razón Social
                                <br />
                                        <asp:TextBox ID="txtRutoRazonSocial" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-danger btn-sm"
                                            OnClick="btnBuscar_Click" />
                                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-success btn-sm"
                                            OnClick="btnLimpiar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="well well-sm">
                                <div class="form-inline">
                                    <div class="form-group">
                                        Cotización
                                <br />
                                        <asp:TextBox ID="txtBuscarCotizacion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:Button ID="btnBuscarCotizacion" runat="server" Text="Buscar" CssClass="btn btn-danger btn-sm"
                                            OnClick="btnBuscarCotizacion_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="well well-sm">
                                <div class="form-inline">
                                    <div class="form-group">
                                        Email
                                <br />
                                        <asp:TextBox ID="txtBuscarTelefono" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:Button ID="btnBuscarTelefono" runat="server" Text="Buscar" CssClass="btn btn-danger btn-sm"
                                            OnClick="btnBuscarTelefono_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        

                        <div class="col-sm-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    Datos del cliente
                   
                            <asp:ImageButton ID="ibtnAgregarCliente" OnClick="ibtnAgregarCliente_Click" ImageUrl="~/assets/img/add.png" runat="server" />
                                    <asp:ImageButton ID="ibtnEditarCliente" OnClick="ibtnEditarCliente_Click" ImageUrl="~/assets/img/edit_button.png" runat="server" />
                                </div>

                                <table class="table table-condensed small" id="tablaCliente" runat="server" visible="false">
                                    <tr class="success">
                                        <td>Rut</td>
                                        <td>
                                            <asp:Label ID="lblRut" runat="server" CssClass="text-danger"></asp:Label></td>
                                        <td>Razón Social</td>
                                        <td>
                                            <asp:Label ID="lblRazonSocial" runat="server" CssClass="text-danger"></asp:Label></td>
                                        <td>Condición de Venta</td>
                                        <td>
                                            <asp:Label ID="lblCondiciondeVenta" runat="server" CssClass="text-danger"></asp:Label>
                                            <asp:Label ID="lblDireccion" Visible="false" runat="server" CssClass="text-danger"></asp:Label>

                                        </td>
                                        <td>Monto Credito</td>
                                        <td>
                                            <asp:Label ID="lblMontoCredito" runat="server" CssClass="text-danger"></asp:Label></td>
                                        <td></td>
                                    </tr>
                                    <tr class="success">
                                        <td>Clasificación</td>
                                        <td>
                                            <asp:Label ID="lblClasificacion" runat="server" CssClass="text-danger"></asp:Label></td>
                                        <td>Zona</td>
                                        <td>
                                            <asp:Label ID="lblZona" runat="server" CssClass="text-danger"></asp:Label></td>
                                        <td>Giro</td>
                                        <td>
                                            <asp:Label ID="lblGiro" runat="server" CssClass="text-danger"></asp:Label></td>
                                        <td>Estado</td>
                                        <td>
                                            <asp:Label ID="lblEstado" runat="server" CssClass="text-danger"></asp:Label></td>
                                        <td></td>
                                    </tr>

                                    <tr class="success">
                                        <td>Usuario Asig</td>
                                        <td>
                                            <asp:Label ID="lblUsuarioAsig" runat="server" CssClass="text-danger"></asp:Label></td>
                                        <td>Campaña</td>
                                        <td>
                                            <asp:Label ID="lblCampana" runat="server" CssClass="text-danger"></asp:Label>
                                            <asp:Label ID="lblIdCampana" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                                        </td>
                                        <td>Monto Venta Ult 12 Meses</td>
                                        <td>
                                            <asp:Label ID="lblMontoVenta12Meses" runat="server" CssClass="text-danger"></asp:Label></td>
                                        <td>Total Cotizado</td>
                                        <td>
                                            <asp:Label ID="lblTotalCotizado" runat="server" CssClass="text-danger"></asp:Label></td>
                                        <td></td>
                                    </tr>
                                    <tr class="success">
                                        <td>Total Cerrado</td>
                                        <td>
                                            <asp:Label ID="lblTotalCerrado" runat="server" CssClass="text-danger"></asp:Label></td>
                                        <td>% Cierre</td>
                                        <td>
                                            <asp:Label ID="lblPorcentajeCierre" runat="server" CssClass="text-danger"></asp:Label>

                                        </td>
                                        <td>Email
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEmailCliente" runat="server" CssClass="text-danger"></asp:Label>

                                        </td>
                                        <td>

                                            <asp:Button ID="btnVerClientesAsociados" runat="server" Text="Clientes Asociados" OnClick="btnVerClientesAsociados_Click" Visible="false" CssClass="btn btn-success btn-xs" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnVerProductosVendidos" runat="server" Text="Productos Vendidos" OnClick="btnVerProductosVendidos_Click" CssClass="btn btn-info btn-xs" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnNuevaCotizacion" runat="server" Text="Nueva Cotización" OnClick="btnNuevaCotizacion_Click" CssClass="btn btn-danger btn-xs" />
                                        </td>

                                    </tr>
                                    <tr id="trRutClientePadreCliente" runat="server" visible="false" class="success">
                                        <td>Rut Cliente Padre</td>
                                        <td><b>
                                            <asp:Label ID="lblRutClientePadreCliente" runat="server"></asp:Label></b>
                                        </td>
                                        <td>Razon Social Padre</td>
                                        <td>
                                            <b>
                                                <asp:Label ID="lblRazonSocialPadreCliente" runat="server"></asp:Label></b>

                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>

                                    </tr>

                                </table>

                            </div>
                        </div>


                        <div class="col-sm-6">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    Dirección Cliente
                            <asp:ImageButton ID="ibtnAgregarDireccionCliente" OnClick="ibtnAgregarDireccionCliente_Click" ImageUrl="~/assets/img/add.png" runat="server" />
                                </div>

                                <asp:GridView ID="grvDireccionCliente" runat="server" EmptyDataText="No hay direcciones de este cliente" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID_CLIENTE_DIRECCION") %>' Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Calle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCalle" runat="server" Text='<%# Bind("CALLE") %>' Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Número">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNumero" runat="server" Text='<%# Bind("NUMERO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resto">
                                            <ItemTemplate>
                                                <asp:Label ID="lblResto" runat="server" Text='<%# Bind("RESTO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comuna">
                                            <ItemTemplate>
                                                <asp:Label ID="lblComuna" runat="server" Text='<%# Bind("COMUNA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnEditarDireccionCliente" runat="server" ImageUrl="~/assets/img/edit_button.png" OnClick="ibtnEditarDireccionCliente_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>


                        <div class="col-sm-6">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    Datos Contacto
                            <asp:ImageButton ID="ibtnAgregarContacto" OnClick="ibtnAgregarContacto_Click" ImageUrl="~/assets/img/add.png" runat="server" />
                                </div>

                                <asp:GridView ID="grvContactos" runat="server" EmptyDataText="No hay contactos en este cliente" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdContacto" runat="server" Text='<%# Bind("ID_CONTACTO") %>' Visible="true"></asp:Label>
                                                <asp:Label ID="lblEmail1" runat="server" Text='<%# Bind("EMAIL_1") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblEmail2" runat="server" Text='<%# Bind("EMAIL_2") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblCelular" runat="server" Text='<%# Bind("CELULAR") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblTelefono1" runat="server" Text='<%# Bind("TELEFONO1") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblTelefono2" runat="server" Text='<%# Bind("TELEFONO2") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblIdCargo" runat="server" Text='<%# Bind("ID_CARGO") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre Contacto">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNombreContacto" runat="server" Text='<%# Bind("NOM_CONTACTO") %>' Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cargo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCargo" runat="server" Text='<%# Bind("CONTACTO_CARGO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnEditarContacto" OnClick="ibtnEditarContacto_Click" runat="server" ImageUrl="~/assets/img/edit_button.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="col-sm-12">


                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <strong>Historial Gestiones Seguimiento</strong>
                                </div>

                                <asp:GridView ID="grvGestionesCRM" runat="server" HeaderStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed table small" AutoGenerateColumns="false" EmptyDataText="No existe información" OnRowDataBound="grvGestionesCRM_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdGestion" runat="server" Text='<%# Bind("ID_GESTION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estatus">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstatus" runat="server" Text='<%# Bind("ESTATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SubEstatus">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubEstatus" runat="server" Text='<%# Bind("SUB_ESTATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estatus Seguimiento">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstatusSeguimiento" runat="server" Text='<%# Bind("ESTATUS_SEGUIMIENTO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Usuario">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUsuario" runat="server" Text='<%# Bind("USUARIO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fec. Ingreso">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaIngreso" runat="server" Text='<%# Bind("FECHA_INGRESO", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fec Agend">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaAgendamiento" runat="server" Text='<%# Bind("FECHA_AGENDAMIENTO2", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hora Agend">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHora" runat="server" Text='<%# Bind("HORA_AGENDAMIENTO", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cotizaciones">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCotizacion" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Obs">
                                            <ItemTemplate>
                                                <asp:Label ID="lblObservacion" runat="server" Text='<%# Eval("OBSERVACION2") %>' ToolTip='<%# Eval("OBSERVACION") %>'></asp:Label>
                                                <asp:Image ID="imgObservacion" runat="server" ImageUrl="~/assets/img/text.png" Visible="false" ToolTip='<%# Bind("OBSERVACION") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--                            <asp:TemplateField HeaderText="Telefono Asociado">
                                <ItemTemplate>
                                    <asp:Label ID="lblTelefonoAsociado" runat="server" Text='<%# Bind("TELEFONO_ASOCIADO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>



                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <strong>Cotizaciones</strong>
                                </div>

                                <asp:GridView ID="grvCotizacionesCRM" runat="server" HeaderStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed small" 
                                    AutoGenerateColumns="false" AllowPaging="True" PageSize="20" OnRowDataBound="paginaciongrvCotizacionesCRM_RowDataBound" OnSorting="gvEmployeegrvCotizacionesCRM_Sorting" AllowSorting="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Cotización" SortExpression="ID_COTIZACION" HeaderStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdCotizacion" runat="server" Visible="false" Text='<%# Bind("ID_COTIZACION") %>'></asp:Label>
                                                <asp:LinkButton ID="lbtnCotizacion" runat="server" Text='<%# Bind("ID_COTIZACION") %>' OnClick="lbtnDetalleCotizacionCotizacionesCRM_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PDF">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRutaPdf" runat="server" Visible="false" Text='<%# Bind("RUTA_COTIZACION") %>'></asp:Label>
                                                <asp:ImageButton ID="imgPdf" ImageUrl="~/assets/img/file_extension_pdf.png" OnClick="imgPdf_Click" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Image ID="imgContacto" ImageUrl="~/assets/img/search_accounts.png" ToolTip='<%# Bind("NOM_CONTACTO") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Empresa" SortExpression="EMPRESA">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpresa" runat="server" Text='<%# Bind("NOMBRE_EMPRESA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha" SortExpression="FECHA">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaCotizacion" runat="server" Text='<%# Bind("FECHA", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Monto Neto" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_NETO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMontoNeto" runat="server" Visible="false" Text='<%# Bind("MONTO_NETO") %>'></asp:Label>
                                                <asp:Label ID="lblMontoNeto2" runat="server" Text='<%# Eval("MONTO_NETO", "{0:n0}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dcto Pago Contado" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_DESCUENTO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMontoDescuento" runat="server" Visible="false" Text='<%# Bind("MONTO_DESCUENTO") %>'></asp:Label>
                                                <asp:Label ID="lblMontoDescuento2" runat="server" Text='<%# Eval("MONTO_DESCUENTO", "{0:n0}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Porc Desc" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPorcentajeDescuentaCotizacion" runat="server" Visible="false" Text='<%# Bind("PORC_DESCUENTO") %>'></asp:Label>
                                                <asp:Label ID="lblPorcentajeDescuentaCotizacion2" runat="server" Text='<%# Eval("PORC_DESCUENTO", "{0:n0}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Monto Iva" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_IVA">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMontoIva" runat="server" Visible="false" Text='<%# Bind("MONTO_IVA") %>'></asp:Label>
                                                <asp:Label ID="lblMontoOIva2" runat="server" Text='<%# Eval("MONTO_IVA", "{0:n0}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Monto Total" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_TOTAL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMontoTotal" runat="server" Visible="false" Text='<%# Bind("MONTO_TOTAL") %>'></asp:Label>
                                                <asp:Label ID="lblMontoTotal2" runat="server" Text='<%# Eval("MONTO_TOTAL", "{0:n0}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fec Validez" SortExpression="FECHA_VALIDEZ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaValidez" runat="server" Text='<%# Bind("FECHA_VALIDEZ", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estado Cot" SortExpression="ESTADO_COTIZACION">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdEstadoCotizacion" runat="server" Visible="false" Text='<%# Bind("ID_ESTADO_COTIZACION") %>'></asp:Label>
                                                <asp:Label ID="lblEstadoCotizacion" runat="server" ToolTip='<%# Bind("MOTIVO_RECHAZO") %>' Text='<%# Bind("NOM_ESTADO_COTIZACION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Usuario Asig" SortExpression="USUARIO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUsuarioAsig" runat="server" Text='<%# Bind("USUARIO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NV" SortExpression="ID_NOTA_VENTA">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdNotaVenta" runat="server" Text='<%# Bind("ID_NOTA_VENTA") %>'></asp:Label>
                                                <asp:ImageButton ID="ibtnGenerarNotaVenta" runat="server" ImageUrl="~/assets/img/page_go.png" ToolTip="Generar Nota Venta" OnClick="ibtnGenerarNotaVenta_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                  

                                        <asp:TemplateField HeaderText="Eliminar Cot">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnEliminarCotizacion" runat="server" OnClientClick="return confirm('¿Desea eliminar la cotización?');" ImageUrl="~/assets/img/delete.png" ToolTip="Eliminar Cotización" OnClick="ibtnEliminarCotizacion_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                    <PagerTemplate>
                                        <div>
                                            <div style="float: left">
                                                <asp:ImageButton ID="imgFirst" runat="server"
                                                    ImageUrl="~/assets/img/grid/first.gif" OnClick="imgFirstgrvCotizacionesCRM_Click"
                                                    Style="height: 15px" title="Navegación: Ir a la Primera Pagina" Width="26px" />
                                                <asp:ImageButton ID="imgPrev" runat="server"
                                                    ImageUrl="~/assets/img/grid/prev.gif" OnClick="imgPrevgrvCotizacionesCRM_Click"
                                                    title="Navegación: Ir a la Pagina Anterior" Width="26px" />
                                                <asp:ImageButton ID="imgNext" runat="server"
                                                    ImageUrl="~/assets/img/grid/next.gif" OnClick="imgNextgrvCotizacionesCRM_Click"
                                                    title="Navegación: Ir a la Siguiente Pagina" Width="26px" />
                                                <asp:ImageButton ID="imgLast" runat="server"
                                                    ImageUrl="~/assets/img/grid/last.gif" OnClick="imgLastgrvCotizacionesCRM_Click"
                                                    title="Navegación: Ir a la Ultima Pagina" Width="26px" />
                                            </div>
                                            <div style="float: right">
                                                Página
                                <asp:Label ID="lblPagina" runat="server"></asp:Label>
                                                de
                                <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </PagerTemplate>
                                </asp:GridView>


                            </div>




                            <div class="panel panel-danger">
                                <div class="panel-heading">
                                    <strong>Nota de Venta</strong>
                                </div>

                                <asp:GridView ID="grvNotaVenta" runat="server" HeaderStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed small"
                                    OnRowDataBound="paginaciongrvNotaVenta_RowDataBound" AutoGenerateColumns="false">
                                    <Columns>


                                        <asp:TemplateField HeaderText="Nota Venta" SortExpression="ID_NOTA_VENTA">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdNotaVenta" runat="server" Visible="true" Text='<%# Bind("ID_NOTA_VENTA") %>'></asp:Label>
                                                <%--<asp:Label ID="lblIdPago" runat="server" Visible="false" Text='<%# Bind("ID_PAGO") %>'></asp:Label>--%>
                                                <%--<asp:LinkButton ID="lbtnIdNotaVenta" runat="server" Text='<%# Bind("ID_NOTA_VENTA") %>' OnClick="lbtnDetalleCotizacionCotizacionesCRM_Click"></asp:LinkButton>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Fecha" SortExpression="FECHA">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaNotaVenta" runat="server" Text='<%# Bind("FECHA", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Monto Neto" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_NETO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMontoNeto" runat="server" Visible="false" Text='<%# Bind("MONTO_NETO") %>'></asp:Label>
                                                <asp:Label ID="lblMontoNeto2" runat="server" Text='<%# Eval("MONTO_NETO", "{0:n0}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dcto Pago Contado" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_DESCUENTO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMontoDescuento" runat="server" Visible="false" Text='<%# Bind("MONTO_DESCUENTO") %>'></asp:Label>
                                                <asp:Label ID="lblMontoDescuento2" runat="server" Text='<%# Eval("MONTO_DESCUENTO", "{0:n0}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Porc Desc" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPorcentajeDescuentaCotizacion" runat="server" Visible="false" Text='<%# Bind("PORC_DESCUENTO") %>'></asp:Label>
                                                <asp:Label ID="lblPorcentajeDescuentaCotizacion2" runat="server" Text='<%# Eval("PORC_DESCUENTO", "{0:n0}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Monto Iva" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_IVA">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMontoIva" runat="server" Visible="false" Text='<%# Bind("MONTO_IVA") %>'></asp:Label>
                                                <asp:Label ID="lblMontoOIva2" runat="server" Text='<%# Eval("MONTO_IVA", "{0:n0}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Monto Total" ItemStyle-HorizontalAlign="Right" SortExpression="MONTO_TOTAL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMontoTotal" runat="server" Visible="false" Text='<%# Bind("MONTO_TOTAL") %>'></asp:Label>
                                                <asp:Label ID="lblMontoTotal2" runat="server" Text='<%# Eval("MONTO_TOTAL", "{0:n0}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Saldo" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSaldo" runat="server" Visible="false" Text='<%# Bind("SALDO") %>'></asp:Label>
                                                <asp:Label ID="lblSaldo2" runat="server" Text='<%# Eval("SALDO", "{0:n0}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estado Not Venta">
                                            <ItemTemplate>

                                                <asp:Label ID="lblEstadoNotaVenta" runat="server" Text='<%# Bind("NOM_ESTADO_NOTA_VENTA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Usuario Asig" SortExpression="USUARIO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUsuarioAsig" runat="server" Text='<%# Bind("USUARIO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="O.C." SortExpression="ORDEN_DE_COMPRA">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrdenCompra" runat="server" Text='<%# Bind("ORDEN_DE_COMPRA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cot" SortExpression="ID_COTIZACION">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdCotizacion" runat="server" Visible="false" Text='<%# Bind("ID_COTIZACION") %>'></asp:Label>
                                                <asp:LinkButton ID="lbtnCotizacion" runat="server" Text='<%# Bind("ID_COTIZACION") %>' OnClick="lbtnDetalleCotizacionCotizacionesCRMNv_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Adjunto">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRutaOrdenDeCompra" runat="server" Visible="false" Text='<%# Bind("RUTA_ORDEN_DE_COMPRA") %>'></asp:Label>
                                                <asp:ImageButton ID="ibtnRutaOrdenCompra" ImageUrl="~/assets/img/file_manager.png" runat="server" OnClick="ibtnRutaOrdenCompra_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OT">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgIngresarOT" runat="server" ToolTip="Ingresar OT" ImageUrl="~/assets/img/text.png" OnClick="imgIngresarOT_Click" />
                                                <asp:Label ID="lblIdOrdenTrabajo" runat="server" Visible="true" Text='<%# Bind("ID_ORDEN_TRABAJO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnPagarNotaVenta" ToolTip="Ingresar Pago" ImageUrl="~/assets/img/money_dollar.png" OnClick="ibtnPagarNotaVenta_Click" runat="server" />
                                                <asp:ImageButton ID="ibtnVerPago" ToolTip="Visualizar Pago" ImageUrl="~/assets/img/search_accounts.png" OnClick="ibtnVerPago_Click" runat="server" />
                                                <asp:ImageButton ID="ibtnEliminarPago" ToolTip="Eliminar Pago" ImageUrl="~/assets/img/vcard_delete.png" Visible="false" OnClick="ibtnEliminarPago_Click" runat="server" OnClientClick="return confirm('¿Desea eliminar el registro?');" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnAgregarFactura" ToolTip="Ingresar Factura" ImageUrl="~/assets/img/note_go.png" OnClick="ibtnAgregarFactura_Click" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PDF">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRutaPdf" runat="server" Visible="false" Text='<%# Bind("RUTA_NOTA_VENTA") %>'></asp:Label>
                                                <asp:ImageButton ID="imgPdfNotaVenta" ImageUrl="~/assets/img/file_extension_pdf.png" OnClick="imgPdfNotaVenta_Click" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgEliminar" runat="server" ToolTip="Eliminar Nota de venta" ImageUrl="~/assets/img/delete.png" OnClick="imgEliminarNotaVenta_Click" OnClientClick="return confirm('¿Desea eliminar el registro?');" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>


                            </div>



                            <div class="panel panel-warning">
                                <div class="panel-heading">
                                    <strong>Orden de Trabajo</strong>
                                </div>
                                
                                <asp:GridView ID="grvOrdenDeTrabajo" runat="server" HeaderStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed small"
                                    OnRowDataBound="grvOrdenDeTrabajo_RowDataBound" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="OT" SortExpression="ID_ORDEN_TRABAJO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdOrdenDeTrabajo" runat="server" Visible="true" Text='<%# Bind("ID_ORDEN_TRABAJO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nota Venta" SortExpression="ID_NOTA_VENTA">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdNotaVenta" runat="server" Visible="true" Text='<%# Bind("ID_NOTA_VENTA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Creacion" SortExpression="FECHA">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaOT" runat="server" Text='<%# Bind("FECHA", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Entrega" SortExpression="FECHA_ENTREGA">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaOTEntrega" runat="server" Text='<%# Bind("FECHA_ENTREGA", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estado OT">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstadoOT" runat="server" Text='<%# Bind("NOMBRE_ESTADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Usuario Asig" SortExpression="USUARIO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUsuarioAsig" runat="server" Text='<%# Bind("USUARIO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cot" SortExpression="ID_COTIZACION">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdCotizacion" runat="server" Visible="false" Text='<%# Bind("ID_COTIZACION") %>'></asp:Label>
                                                <asp:LinkButton ID="lbtnCotizacion" runat="server" Text='<%# Bind("ID_COTIZACION") %>' OnClick="lbtnDetalleCotizacionCotizacionesCRMOT_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PDF"  HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRutaPdf" runat="server" Visible="false" Text='<%# Bind("RUTA_ORDEN_TRABAJO") %>'></asp:Label>
                                                <asp:ImageButton ID="imgPdfOT" ImageUrl="~/assets/img/file_extension_pdf.png" OnClick="imgPdfOT_Click" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgEliminarOT" runat="server" ToolTip="Eliminar OT" ImageUrl="~/assets/img/delete.png" OnClick="imgEliminarOT_Click" OnClientClick="return confirm('¿Desea eliminar el registro?');" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>


                            </div>









                            <div class="panel panel-danger">
                                <div class="panel-heading">
                                    <strong>Facturas</strong>
                                </div>

                                <asp:GridView ID="grvFacturas" runat="server" HeaderStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed small" AutoGenerateColumns="false" OnRowDataBound="grvFacturasPaginacion_RowDataBound">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Id Factura">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdFactura" runat="server" Visible="true" Text='<%# Bind("ID_FACTURA") %>'></asp:Label>
                                                <%--<asp:LinkButton ID="lbtnIdNotaVenta" runat="server" Text='<%# Bind("ID_NOTA_VENTA") %>' OnClick="lbtnDetalleCotizacionCotizacionesCRM_Click"></asp:LinkButton>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Id NV">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdNotaVenta" runat="server" Visible="true" Text='<%# Bind("ID_NOTA_VENTA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Facturación">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaFacturacion" runat="server" Text='<%# Bind("FECHA_FACTURACION", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Monto Neto" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMontoNeto" runat="server" Visible="true" Text='<%# Bind("MONTO_NETO", "{0:n0}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Forma Pago Neto" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFormaPago" runat="server" Visible="true" Text='<%# Bind("NOM_FORMA_PAGO") %>'></asp:Label>
                                                <asp:Label ID="lblIdFormaPago" runat="server" Visible="false" Text='<%# Bind("ID_FORMA_PAGO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgEditar" ToolTip="Modificar Factura" runat="server" ImageUrl="~/assets/img/edit_button.png" OnClick="imgEditarFactura_Click" />
                                                <asp:ImageButton ID="imgEliminar" ToolTip="Eliminar Factura" runat="server" ImageUrl="~/assets/img/delete.png" OnClick="imgEliminarFactura_Click" OnClientClick="return confirm('¿Desea eliminar el registro?');" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>


                            </div>









                        </div>

                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tpGestion" runat="server" HeaderText="Gestión">
                    <ContentTemplate>

                        <br />

                        <div class="alert alert-success" id="divMensaje" runat="server" visible="false">
                            <a href="#" class="close" data-dismiss="alert">&times;</a>
                            <strong>Correcto!</strong> La gestión se ingresó correctamente.
                        </div>

                        <div class="well well-sm">
                            <table class="table table-condensed table-responsive small">
                                <tr class="active">
                                    <td>
                                        <strong>ID:</strong>
                                        <asp:Label ID="lblId" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <strong>Rut:</strong>
                                        <asp:Label ID="lblRutGestiones" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <strong>Razon Social:</strong>
                                        <asp:Label ID="lblRazonSocialGestiones" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trRutClientePadreGestion" runat="server" visible="false">
                                    <td>
                                        <strong>Rut Padre</strong>
                                        <asp:Label ID="lblRutClientePadreGestion" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <strong>Razon Social Padre</strong>
                                        <asp:Label ID="lblRazonSocialPadreGestion" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>

                        <asp:GridView ID="grvGestion" runat="server" EmptyDataText="No hay cotizaciones sin nota de venta para gestionar." EmptyDataRowStyle-Font-Bold="true" HeaderStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed small" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="3%">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cotización">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCotizacion" runat="server" Visible="false" Text='<%# Bind("ID_COTIZACION") %>'></asp:Label>
                                        <asp:LinkButton ID="lbtnCotizacion" runat="server" Text='<%# Bind("ID_COTIZACION") %>' OnClick="lbtnDetalleCotizacion_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Contacto">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContacto" runat="server" Visible="true" Text='<%# Bind("NOM_CONTACTO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Fecha Cot">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaCotizacion" runat="server" Text='<%# Bind("FECHA", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Monto Neto" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMontoNeto" runat="server" Visible="false" Text='<%# Bind("MONTO_NETO") %>'></asp:Label>
                                        <asp:Label ID="lblMontoNeto2" runat="server" Text='<%# Eval("MONTO_NETO", "{0:n0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dcto Pago Contado" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescuentaCotizacion" runat="server" Visible="false" Text='<%# Bind("MONTO_DESCUENTO") %>'></asp:Label>
                                        <asp:Label ID="lblDescuentaCotizacion2" runat="server" Text='<%# Eval("MONTO_DESCUENTO", "{0:n0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Porc Desc" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPorcentajeDescuentaCotizacion" runat="server" Visible="false" Text='<%# Bind("PORC_DESCUENTO") %>'></asp:Label>
                                        <asp:Label ID="lblPorcentajeDescuentaCotizacion2" runat="server" Text='<%# Eval("PORC_DESCUENTO", "{0:n0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Monto Iva" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMontoIva" runat="server" Visible="false" Text='<%# Bind("MONTO_IVA") %>'></asp:Label>
                                        <asp:Label ID="lblMontoIva2" runat="server" Text='<%# Eval("MONTO_IVA", "{0:n0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Monto Cot" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMontoCotizacion" runat="server" Visible="false" Text='<%# Bind("MONTO_TOTAL") %>'></asp:Label>
                                        <asp:Label ID="lblMontoCotizacion2" runat="server" Text='<%# Eval("MONTO_TOTAL", "{0:n0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Fec Validez">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaValidez" runat="server" Text='<%# Bind("FECHA_VALIDEZ", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdEstadoCotizacion" runat="server" Visible="false" Text='<%# Bind("ID_ESTADO_COTIZACION") %>'></asp:Label>
                                        <asp:Label ID="lblEstadoCotizacion" runat="server" ToolTip='<%# Bind("MOTIVO_RECHAZO") %>' Text='<%# Bind("NOM_ESTADO_COTIZACION") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>


                        <div id="divPanelGestionar" runat="server">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Gestion</h3>
                                </div>
                                <div class="panel-body">

                                    <table class="table">

                                        <tr>
                                            <td><b>Estatus</b>
                                                <asp:DropDownList ID="ddlEstatus" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlEstatus_SelectedIndexChanged" AutoPostBack="true" OnDataBound="ddlEstatus_DataBound">
                                                </asp:DropDownList>
                                            </td>
                                            <td><b>Sub estatus</b><asp:DropDownList ID="ddlSubEstatus" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlSubEstatus_SelectedIndexChanged" AutoPostBack="true" OnDataBound="ddlSubEstatus_DataBound">
                                            </asp:DropDownList></td>

                                            <td><b>Estatus Seguimiento</b>
                                                <asp:DropDownList ID="ddlEstatusSeguimiento" runat="server" CssClass="form-control input-sm" OnDataBound="ddlEstatusSeguimiento_DataBound" OnSelectedIndexChanged="ddlEstatusSeguimiento_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>

                                            </td>
                                            <td>
                                                <%--<b>Motivo no compra</b>
                                                --%>
                                                <asp:DropDownList ID="ddlMotivoNoCompra" Visible="false" runat="server" CssClass="form-control input-sm">
                                                </asp:DropDownList>
                                            </td>

                                            <td><b>
                                                <asp:Label ID="lblFechaAgendamiento" Visible="false" runat="server" Text="Fec Agendamiento"></asp:Label></b>
                                                <asp:TextBox ID="txtFecAgendamiento" runat="server" CssClass="form-control input-sm" Width="130px" Visible="false"></asp:TextBox>
                                                <asp:CalendarExtender ID="txtFecAgendamiento_CalendarExtender" runat="server"
                                                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy"
                                                    TargetControlID="txtFecAgendamiento">
                                                </asp:CalendarExtender>
                                            </td>
                                            <td>
                                                <b>
                                                    <asp:Label ID="lblHora" Visible="false" runat="server" Text="Hora"></asp:Label></b>
                                                <asp:TextBox ID="txtHora" Visible="false" MaxLength="5" runat="server" CssClass="form-control input-sm" Width="130px"></asp:TextBox>

                                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                    AcceptAMPM="true"
                                                    MaskType="Time"
                                                    Mask="99:99:99"
                                                    ErrorTooltipEnabled="true"
                                                    InputDirection="RightToLeft"
                                                    CultureName="es-ES"
                                                    TargetControlID="txtHora"
                                                    MessageValidatorTip="true">
                                                </asp:MaskedEditExtender>

                                                <asp:MaskedEditValidator
                                                    ID="MaskedEditValidator1"
                                                    runat="server"
                                                    ToolTip="ERROR FORMATO HORA"
                                                    ErrorMessage="*"
                                                    ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txtHora"
                                                    InvalidValueMessage="ERROR FORMATO HORA"
                                                    TooltipMessage="Hora 0:00:00 hasta 23:59:59"></asp:MaskedEditValidator>


                                                <asp:TextBox ID="txtCotizacion" MaxLength="20" Visible="false" runat="server" CssClass="form-control input-sm" Width="130px"></asp:TextBox>
                                            </td>

                                            <td>
                                                <b>
                                                    <asp:Label ID="lblFechaVisita" Visible="false" runat="server" Text="Fecha Visita"></asp:Label></b>
                                                <asp:TextBox ID="txtFechaVisita" runat="server" CssClass="form-control input-sm" Width="130px" Visible="false"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy"
                                                    TargetControlID="txtFechaVisita">
                                                </asp:CalendarExtender>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td colspan="5">
                                                <b>Comentario</b><asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control input-sm" TextMode="MultiLine" Height="100px" Width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <br />
                                                <asp:HiddenField ID="hfVieneSeguimiento" runat="server" />
                                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-danger btn-sm" OnClick="btnGuardar_Click" />
                                            </td>
                                        </tr>
                                    </table>

                                </div>
                            </div>

                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <strong>Historial Gestiones Seguimiento</strong>
                                </div>
                                <asp:GridView ID="grvHistorialGestiones" runat="server" HeaderStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed table small" AutoGenerateColumns="false" EmptyDataText="No existe información">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdGestion" runat="server" Text='<%# Bind("ID_GESTION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estatus">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstatus" runat="server" Text='<%# Bind("ESTATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SubEstatus">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubEstatus" runat="server" Text='<%# Bind("SUB_ESTATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estatus Seguimiento">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstatusSeguimiento" runat="server" Text='<%# Bind("ESTATUS_SEGUIMIENTO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Usuario">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUsuario" runat="server" Text='<%# Bind("USUARIO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fec. Ingreso">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaIngreso" runat="server" Text='<%# Bind("FECHA_INGRESO", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fec Agend">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaAgendamiento" runat="server" Text='<%# Bind("FECHA_AGENDAMIENTO2", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Obs">
                                            <ItemTemplate>
                                                <asp:Label ID="lblObservacion" runat="server" Text='<%# Eval("OBSERVACION2") %>' ToolTip='<%# Eval("OBSERVACION") %>'></asp:Label>
                                                <asp:Image ID="imgObservacion" runat="server" ImageUrl="~/assets/img/text.png" Visible="false" ToolTip='<%# Bind("OBSERVACION") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--                            <asp:TemplateField HeaderText="Telefono Asociado">
                                <ItemTemplate>
                                    <asp:Label ID="lblTelefonoAsociado" runat="server" Text='<%# Bind("TELEFONO_ASOCIADO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>


                            <asp:HiddenField ID="hfRutClientePost" runat="server" />
                            <asp:HiddenField ID="hfrutaArchivoPdf" runat="server" />
                        </div>


                    </ContentTemplate>
                </asp:TabPanel>


            </asp:TabContainer>







            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="btnEmpresasMDL" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mdlEmpresas" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlEmpresas" TargetControlID="btnEmpresasMDL">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlEmpresas" runat="server" CssClass="panel" Style="background: white; width: 90%; max-height: 90%; overflow: auto; display: none">

                <div id="panelInformacion" runat="server" class="panel-primary">
                    <div class="panel-heading">
                        <button class="close" data-dismiss="modal">×</button>
                        <strong>Empresas</strong>
                    </div>
                    <asp:GridView ID="grvEmpresas" HeaderStyle-CssClass="active" runat="server" CssClass="table table-bordered table-hover table-condensed small" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Rut">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdCliente" runat="server" Visible="false" Text='<%# Bind("ID_CLIENTE") %>'></asp:Label>
                                    <asp:Label ID="lblEmailCliente" runat="server" Visible="false" Text='<%# Bind("EMAIL") %>'></asp:Label>
                                    <asp:Label ID="lblRutCliente" runat="server" Text='<%# Bind("RUT_CLIENTE") %>'></asp:Label>
                                    <asp:Label ID="lblCondicionVenta" runat="server" Visible="false" Text='<%# Bind("CONDICION_VENTA") %>'></asp:Label>
                                    <asp:Label ID="lblGiro" runat="server" Visible="false" Text='<%# Bind("GIRO") %>'></asp:Label>

                                    <asp:Label ID="lblIdCampana" runat="server" Visible="false" Text='<%# Bind("ID_CAMPANA") %>'></asp:Label>
                                    <asp:Label ID="lblNomCampana" runat="server" Visible="false" Text='<%# Bind("NOM_CAMPANA") %>'></asp:Label>
                                    <asp:Label ID="lblMontoVenta12Meses" runat="server" Visible="false" Text='<%# Bind("MONTO_VENTA_ULTIMO_12") %>'></asp:Label>

                                    <asp:Label ID="lblTotalCotizado" runat="server" Visible="false" Text='<%# Bind("MONTO_COTIZADO") %>'></asp:Label>
                                    <asp:Label ID="lblTotalCerrado" runat="server" Visible="false" Text='<%# Bind("MONTO_CERRADO") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Razon Social">
                                <ItemTemplate>
                                    <asp:Label ID="lblRazonSocial" runat="server" Text='<%# Bind("RAZON_SOCIAL") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Dirección">
                                <ItemTemplate>
                                    <asp:Label ID="lblDireccion" runat="server" Visible="true" Text='<%# Bind("DIRECCION") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Comuna">
                                <ItemTemplate>
                                    <asp:Label ID="lblComuna" runat="server" Visible="true" Text='<%# Bind("COMUNA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Monto Credito">
                                <ItemTemplate>
                                    <asp:Label ID="lblMontoCredito" runat="server" Visible="true" Text='<%# Bind("MONTO_CREDITO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Clasificación">
                                <ItemTemplate>
                                    <asp:Label ID="lblClasificacion" runat="server" Visible="false" Text='<%# Bind("CLASIFICACION") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Zona">
                                <ItemTemplate>
                                    <asp:Label ID="lblZona" runat="server" Visible="true" Text='<%# Bind("ZONA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Visible="true" Text='<%# Bind("NOM_ESTADO_CLIENTE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Seleccionar">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgSeleccionar" CausesValidation="False" runat="server" Visible="true" ImageUrl="~/assets/img/add.png" ToolTip="Seleccionar" OnClick="imgSeleccionarCliente_Click" />
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>




            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="btnDetalleCotizacionMDL" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mdlDetalleCotizacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlDetalleCotizacion" TargetControlID="btnDetalleCotizacionMDL">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlDetalleCotizacion" runat="server" CssClass="panel panel-primary" Style="background: white; width: 95%; max-height: 90%; overflow: auto; display: none">
                <div class="panel panel-heading">
                    <button class="close" data-dismiss="modal">
                        ×
                    </button>
                    <strong>Detalle Cotización 
                        <asp:Label ID="lblNumeroCotizacionDetalle" runat="server" CssClass="label label-danger"></asp:Label></strong>
                </div>

                <asp:GridView ID="grvDetalleCotizacion" runat="server" CssClass="table table-bordered table-hover table-condensed small" HeaderStyle-CssClass="active" AutoGenerateColumns="false" EmptyDataText="No se encontraron registros asociados a esa cotización">
                    <Columns>
                        <asp:TemplateField HeaderText="Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdCotizacion" runat="server" Text='<%# Bind("ID_COTIZACION") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Correlativo">
                            <ItemTemplate>
                                <asp:Label ID="lblCorrelativo" runat="server" Text='<%# Bind("CORRELATIVO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Código">
                            <ItemTemplate>
                                <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("CODIGO") %>'></asp:Label>
                                <asp:Label ID="lblIdProducto" runat="server" Visible="false" Text='<%# Bind("ID_PRODUCTO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Producto">
                            <ItemTemplate>
                                <asp:Label ID="lblNombreProducto" runat="server" Text='<%# Bind("NOM_PRODUCTO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Monto Neto" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblMontoNeto" runat="server" Text='<%# Eval("MONTO_NETO", "{0:n0}")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("CANTIDAD", "{0:n0}")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Monto Total" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblMontoTotal" runat="server" Text='<%# Eval("MONTO_TOTAL", "{0:n0}")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </asp:Panel>


            <%--MODALPOPUP CON BOOTSTRAP--%>





            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="btnMDLAgregarContacto" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mdlAgregarContacto" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlAgregarContacto" TargetControlID="btnMDLAgregarContacto" BehaviorID="_mdlAgregarObra">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlAgregarContacto" runat="server" CssClass="panel" Style="display: none; background: white; width: 75%; height: auto; overflow: auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <button class="close" data-dismiss="modal">
                            ×
                        </button>
                        <strong>
                            <asp:Label ID="lblAgregarContacto" runat="server"></asp:Label>
                        </strong>
                    </div>
                    <asp:HiddenField ID="hfIdContacto" runat="server" />
                    <asp:HiddenField ID="hfRutCliente" runat="server" />

                    <table class="table table-condensed small">
                        <tr class="active">
                            <td><strong>Nombre Contacto</strong></td>
                            <td colspan="3">
                                <asp:TextBox ID="txtNombreContacto" Width="27%" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td>
                                <strong>Email 1</strong></td>
                            <td>
                                <asp:TextBox ID="txtEmail1" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Email 2</strong></td>
                            <td>
                                <asp:TextBox ID="txtEmail2" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                        </tr>
                        <tr class="active">
                            <td><strong>Celular</strong></td>
                            <td>
                                <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Teléfono 1</strong></td>
                            <td>
                                <asp:TextBox ID="txtTelefono1" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Teléfono 2</strong></td>
                            <td>
                                <asp:TextBox ID="txtTelefono2" runat="server" Width="80%" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <strong>Cargo</strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCargo" CssClass="form-control input-sm" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <div class="panel-footer">
                        <asp:Button ID="btnGrabarContacto" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGrabarContacto_Click" />
                        <asp:Button ID="btnModificarContacto" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnModificarContacto_Click" />
                    </div>
                    <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
            </asp:Panel>








            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="btnAgregarClienteMDL" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mdlAgregarCliente" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlAgregar" TargetControlID="btnAgregarClienteMDL" BehaviorID="_mdlAgregarCliente">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlAgregar" runat="server" CssClass="panel" Style="display: none; background: white; width: 75%; height: auto; overflow: auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <button class="close" data-dismiss="modal">
                            ×
                        </button>
                        <strong>
                            <asp:Label ID="lblAgregarUsuario" runat="server" Text=""></asp:Label>
                        </strong>
                    </div>
                    <asp:HiddenField ID="hfIdUsuario" runat="server" />

                    <table class="table table-condensed small">
                        <tr class="active">
                            <td><strong>Razon Social</strong></td>
                            <td colspan="3">
                                <asp:TextBox ID="txtRazonSocial" Width="27%" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td>
                                <strong>Rut</strong></td>
                            <td>
                                <asp:TextBox ID="txtRut" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Nombre Corto</strong></td>
                            <td>
                                <asp:TextBox ID="txtNombreCorto" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                        </tr>
                        <tr class="active">
                            <td><strong>Dirección</strong></td>
                            <td>
                                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Comuna</strong></td>
                            <td>
                                <asp:TextBox ID="txtComuna" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Ciudad</strong></td>
                            <td>
                                <asp:TextBox ID="txtCiudad" runat="server" Width="80%" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <strong>Teléfono</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTelefono" runat="server" Width="80%" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Email</strong></td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" Width="80%" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Info</strong></td>
                            <td>
                                <asp:TextBox ID="txtInfo" runat="server" Width="80%" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Monto Credito</strong></td>
                            <td>
                                <asp:TextBox ID="txtMontoCredito" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                            <td><strong>Clasificacion</strong></td>
                            <td>
                                <asp:TextBox ID="txtClasificacion" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                        </tr>
                        <tr class="active">
                            <td><strong>Zona</strong></td>
                            <td>
                                <asp:TextBox ID="txtZona" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                            <td><strong>Condición de Venta</strong></td>
                            <td>
                                <asp:DropDownList ID="ddlCondicionDeVenta" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Giro</strong></td>
                            <td>
                                <asp:TextBox ID="txtGiro" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                            <td><strong>Estado</strong></td>
                            <td>
                                <asp:DropDownList ID="ddlEstado" runat="server" Width="150px" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </td>
                        </tr>


                        <tr class="active">
                            <td><strong>Url</strong></td>
                            <td>
                                <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                            <td><strong>Act Comercial</strong></td>
                            <td>
                                <asp:DropDownList ID="ddlActividadComercial" runat="server" Width="150px" CssClass="form-control input-sm" OnDataBound="ddlActividadComercial_DataBound">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr class="active">
                            <td><strong>Observación</strong></td>
                            <td>
                                <asp:TextBox ID="txtObservacionCliente" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                            <td><strong>Campaña</strong></td>
                            <td>
                                <asp:DropDownList ID="ddlCampana" runat="server" Width="150px" CssClass="form-control input-sm" OnDataBound="ddlCampana_DataBound">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="active">
                            <td>
                                <strong>Rut Cliente Padre</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRutClientePadre" runat="server" MaxLength="12" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <strong>Activo</strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlActivo" runat="server" Width="150px" CssClass="form-control input-sm">
                                    <asp:ListItem Text="SI" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                    <div class="panel-footer">
                        <asp:Button ID="btnModificar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnModificar_Click" />
                        <asp:Button ID="btnAgregar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
                    </div>
                    <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
            </asp:Panel>




            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="btnContactosMDL" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mdlContactos" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlContactos" TargetControlID="btnContactosMDL" BehaviorID="_mdlContactos">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlContactos" runat="server" CssClass="panel" Style="display: none; background: white; width: 75%; height: auto; overflow: auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <button class="close" data-dismiss="modal">
                            ×
                        </button>
                        <strong>
                            <asp:Label ID="Label1" runat="server" Text="Contactos"></asp:Label>
                            <asp:Button ID="btnNuevoContacto" runat="server" Text="Nuevo Contacto" CssClass="btn btn-danger btn-xs" OnClick="btnNuevoContacto_Click" />
                        </strong>
                    </div>

                    <asp:GridView ID="grvContactosCotizacion" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="lblNombreContacto" runat="server" Text='<%# Bind("NOM_CONTACTO") %>'></asp:Label>
                                    <asp:Label ID="lblIdContacto" runat="server" Visible="false" Text='<%# Bind("ID_CONTACTO") %>'></asp:Label>
                                    <asp:Label ID="lblRutCliente" runat="server" Visible="false" Text='<%# Bind("RUT_CLIENTE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Email 1">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail1" runat="server" Text='<%# Bind("EMAIL_1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Email 2">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail2" runat="server" Text='<%# Bind("EMAIL_2") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Celular">
                                <ItemTemplate>
                                    <asp:Label ID="lblCelular" runat="server" Text='<%# Bind("CELULAR") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Teléfono 1">
                                <ItemTemplate>
                                    <asp:Label ID="lblTelefono1" runat="server" Text='<%# Bind("TELEFONO1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Teléfono 2">
                                <ItemTemplate>
                                    <asp:Label ID="lblTelefono2" runat="server" Text='<%# Bind("TELEFONO2") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cargo">
                                <ItemTemplate>
                                    <asp:Label ID="lblCargo" runat="server" Text='<%# Bind("CONTACTO_CARGO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-Width="7%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgSeleccionarContacto" runat="server" ImageUrl="~/assets/img/add.png" OnClick="imgSeleccionarContacto_Click" ToolTip="Seleccionar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
            </asp:Panel>






            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="btnMDLAgregarContactoCotizacion" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mdlAgregarContactoCotizacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlAgregarContacto" TargetControlID="btnMDLAgregarContactoCotizacion" BehaviorID="_mdlAgrea">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlAgregarContactoCotizacion" runat="server" CssClass="panel" Style="display: none; background: white; width: 75%; height: auto; overflow: auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <button class="close" data-dismiss="modal">
                            ×
                        </button>
                        <strong>
                            <asp:Label ID="lblAgregarContactoCotizacion" runat="server" Text="Nuevo Contacto"></asp:Label>
                        </strong>
                    </div>
                    <asp:HiddenField ID="hfIdContactoCotizacion" runat="server" />
                    <asp:HiddenField ID="hfRutClienteCotizacion" runat="server" />

                    <table class="table table-condensed small">
                        <tr class="active">
                            <td><strong>Nombre Contacto</strong></td>
                            <td colspan="3">
                                <asp:TextBox ID="txtNombreContactoCotizacion" Width="27%" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td>
                                <strong>Email 1</strong></td>
                            <td>
                                <asp:TextBox ID="txtEmail1Cotizacion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Email 2</strong></td>
                            <td>
                                <asp:TextBox ID="txtEmail2Cotizacion" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>
                        </tr>
                        <tr class="active">
                            <td><strong>Celular</strong></td>
                            <td>
                                <asp:TextBox ID="txtCelularCotizacion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Teléfono 1</strong></td>
                            <td>
                                <asp:TextBox ID="txtTelefono1Cotizacion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Teléfono 2</strong></td>
                            <td>
                                <asp:TextBox ID="txtTelefono2Cotizacion" runat="server" Width="80%" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <strong>Cargo</strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCargoCotizacion" CssClass="form-control input-sm" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <div class="panel-footer">
                        <asp:Button ID="btnGrabarContactoCotizacion" runat="server" Visible="true" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGrabarContactoCotizacion_Click" />
                        <%--<asp:Button ID="btnModificarContactoCotizacion" runat="server" Visible="false"  Text="Guardar" CssClass="btn btn-primary" onclick="btnModificarContactoCotizacion_Click" />--%>
                    </div>
                    <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
            </asp:Panel>


            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="Button3" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mdlSeleccionarTipoDireccion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlSeleccionarTipoDireccion" TargetControlID="Button3">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlSeleccionarTipoDireccion" runat="server" CssClass="panel" Style="display: none; background: white; width: 40%; height: auto; overflow: auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <button class="close" data-dismiss="modal">
                            ×
                        </button>
                        <strong>
                            <asp:Label ID="lbl" runat="server" Text="Seleccionar Dirección"></asp:Label>
                        </strong>
                    </div>
                    <asp:HiddenField ID="hfIdCotizacionSeleccionDireccion" runat="server" />

                    <table class="table table-condensed small">
                        <tr class="active">
                            <td><strong>Dirección Facturación</strong></td>
                            <td>
                                <asp:DropDownList ID="ddlDireccionFacturacionNotaVenta" CssClass="form-control input-sm" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr class="active">
                            <td><strong>Dirección Despacho</strong></td>
                            <td>
                                <asp:DropDownList ID="ddlDireccionNotaVenta" CssClass="form-control input-sm" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Referencia</strong></td>
                            <td>
                                <asp:TextBox ID="txtReferencia" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>O.C.</strong></td>
                            <td>
                                <asp:TextBox ID="txtOrdenCompra" runat="server" Width="150px" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Archivo 1</strong></td>
                            <td>
                                <asp:FileUpload ID="fuArchivoOC" runat="server" CssClass="form-control input-sm" />
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Archivo 2</strong></td>
                            <td>
                                <asp:FileUpload ID="fuArchivoOC2" runat="server" CssClass="form-control input-sm" />
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Glosa</strong></td>
                            <td>
                                <asp:TextBox ID="txtGlosa" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active" runat="server" visible="false">
                            <td><strong>Fecha Entrega</strong></td>
                            <td>
                                <asp:TextBox ID="txtFechaEntrega" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server"
                                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy"
                                    TargetControlID="txtFechaEntrega">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                    <div class="panel-footer">
                        <asp:Button ID="btnSeleccionarDireccionGenerarNotaVenta" runat="server" Text="Generar Nota Venta" CssClass="btn btn-danger btn-sm" OnClick="btnSeleccionarDireccionGenerarNotaVenta_Click" />
                    </div>
                    <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
            </asp:Panel>




            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="Button1" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mdlIngresarOT" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlIngresarOt" TargetControlID="Button1">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlIngresarOt" runat="server" CssClass="panel" Style="display: none; background: white; width: 40%; height: auto; overflow: auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <button class="close" data-dismiss="modal">
                            ×
                        </button>
                        <strong>
                            <p>Ingresar Fecha Entrega</p>
                        </strong>
                    </div>

                    <table class="table table-condensed small">
                        <tr>
                            <td>Nota de Venta
                            </td>
                            <td>
                                <asp:Label ID="lblIdNotaDeVenta" runat="server" CssClass="label label-primary"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Nro Cotizacion
                            </td>
                            <td>
                                <asp:Label ID="lblIdCotizacion" runat="server" CssClass="label label-primary"></asp:Label>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Fecha Entrega</strong></td>
                            <td>
                                <asp:TextBox ID="txtFechaEntregaOriginal" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server"
                                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy hh:mm:ss"
                                    TargetControlID="txtFechaEntregaOriginal">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                    <div class="panel-footer">
                        <asp:Button ID="btnGenerarOT" runat="server" Text="Generar OT" CssClass="btn btn-danger btn-sm" OnClick="btnGenerarOT_Click" />
                    </div>
                    <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
            </asp:Panel>




            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="Button4" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mdlAgregarDireccion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlAgregarDireccion" TargetControlID="Button4">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlAgregarDireccion" runat="server" CssClass="panel" Style="display: none; background: white; width: 50%; height: auto; overflow: auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <button class="close" data-dismiss="modal">
                            ×
                        </button>
                        <strong>
                            <asp:Label ID="lblAgregarDireccion" runat="server" Text="Agregar Dirección"></asp:Label>
                        </strong>
                    </div>
                    <asp:HiddenField ID="hfIdDireccion" runat="server" />

                    <table class="table table-condensed small">
                        <tr class="active">
                            <td><strong>Calle</strong>
                                <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <strong>Número</strong>
                                <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <strong>Resto</strong>
                                <asp:TextBox ID="txtResto" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <strong>Comuna</strong>
                                <asp:TextBox ID="txtComunaDireccion" runat="server" Visible="false" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:DropDownList ID="ddlComunaDireccion" runat="server" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <div class="panel-footer">
                        <asp:Button ID="btnGrabarDireccion" runat="server" Text="Guardar" CssClass="btn btn-danger btn-sm" OnClick="btnGrabarDireccion_Click" />
                        <asp:Button ID="btnEditarDireccion" runat="server" Text="Guardar" CssClass="btn btn-danger btn-sm" OnClick="btnEditarDireccion_Click" />
                    </div>
                    <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
            </asp:Panel>










            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="Button6" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mdlAgregarFactura" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlAgregarFactura" TargetControlID="Button6">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlAgregarFactura" runat="server" CssClass="panel" Style="display: none; background: white; width: 40%; height: auto; overflow: auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <button class="close" data-dismiss="modal">
                            ×
                        </button>
                        <strong>
                            <asp:Label ID="Label4" runat="server" Text="Datos Facturación"></asp:Label>
                        </strong>
                    </div>
                    <asp:HiddenField ID="HiddenField1" runat="server" />

                    <table class="table table-condensed small">
                        <tr class="active">
                            <td><strong>Nro Factura</strong></td>
                            <td>
                                <asp:TextBox ID="txtIdFactura" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:HiddenField ID="hfIdNotaVenta" runat="server" />
                            </td>
                        </tr>

                        <tr class="active">
                            <td><strong>Fecha Facturación</strong></td>
                            <td>
                                <asp:TextBox ID="txtFechaFacturacion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                                    Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy"
                                    TargetControlID="txtFechaFacturacion">
                                </asp:CalendarExtender>
                            </td>
                        </tr>

                        <tr class="active">
                            <td><strong>Monto Neto</strong></td>
                            <td>
                                <asp:TextBox ID="txtMontoNeto" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>

                        <tr class="active" runat="server" visible="false">
                            <td><strong>Forma Pago</strong></td>
                            <td>
                                <asp:DropDownList ID="ddlFormaPago" CssClass="form-control input-sm" OnDataBound="ddlFormaPago_DataBound" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>


                    </table>
                    <div class="panel-footer">
                        <asp:Button ID="btnGuardarFactura" runat="server" Text="Guardar Factura" CssClass="btn btn-danger btn-sm" OnClick="btnGuardarFactura_Click" />
                    </div>
                    <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
            </asp:Panel>




















            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="Button7" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mdlVerPago" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlVerPago" TargetControlID="Button7">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlVerPago" runat="server" CssClass="panel" Style="display: none; background: white; width: 80%; height: auto; overflow: auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <button class="close" data-dismiss="modal">
                            ×
                        </button>
                        <strong>
                            <asp:Label ID="Label5" runat="server" Text="Pago"></asp:Label>
                        </strong>
                    </div>



                    <asp:GridView ID="grvPago" runat="server" CssClass="table table-bordered table-hover table-condensed table small" EmptyDataText="No hay un pago asociado a esta nota de venta" HeaderStyle-CssClass="active" AutoGenerateColumns="false" OnRowDataBound="grvPago_RowDataBound">
                        <Columns>

                            <asp:TemplateField HeaderText="Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID_PAGO") %>' Visible="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Correlativo">
                                <ItemTemplate>
                                    <asp:Label ID="lblCorrelativo" runat="server" Text='<%# Bind("CORRELATIVO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fec Ingreso">
                                <ItemTemplate>
                                    <asp:Label ID="lblFechaIngreso" runat="server" Visible="true" Text='<%# Bind("FECHA_INGRESO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FP">
                                <ItemTemplate>
                                    <asp:Label ID="lblFormaPago" runat="server" Text='<%# Bind("NOM_FORMA_PAGO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Banco">
                                <ItemTemplate>
                                    <asp:Label ID="lblNombreBanco" runat="server" Text='<%# Bind("NOM_BANCO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Monto" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblMonto" runat="server" Text='<%# Bind("MONTO", "{0:n0}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Fec Venc">
                                <ItemTemplate>
                                    <asp:Label ID="lblFechaVencimiento" runat="server" Visible="true" Text='<%# Bind("FECHA_VENC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Rut Cheque">
                                <ItemTemplate>
                                    <asp:Label ID="lblRutCheque" runat="server" Text='<%# Bind("RUT_CHEQUE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nro CC">
                                <ItemTemplate>
                                    <asp:Label ID="lblNroCC" runat="server" Text='<%# Bind("NRO_CTACTE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                        </Columns>
                    </asp:GridView>


                    <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
            </asp:Panel>











            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="Button8" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mdlProductosVendidos" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlVerProductosVendidos" TargetControlID="Button8">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlVerProductosVendidos" runat="server" CssClass="panel" Style="display: none; background: white; width: 95%; height: auto; overflow: auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <button class="close" data-dismiss="modal">
                            ×
                        </button>
                        <strong>
                            <asp:Label ID="lblProductosVendidos" runat="server" Text="Productos Vendidos"></asp:Label>
                        </strong>
                    </div>

                    <asp:GridView ID="grvProductosVendidosPorCliente" runat="server" CssClass="table table-bordered table-hover table-condensed table small" EmptyDataText="No hay productos vendidos por este cliente" HeaderStyle-CssClass="info" AutoGenerateColumns="false">
                        <Columns>
                            <%--nv.rut_cliente,nv.fecha,p.codigo,p.nom_producto,nvd.cantidad,
                        (nvd.monto_neto/nvd.cantidad) as valorUnitario,
                        nvd.monto_Total,nvd.Descuento, cast(round(((nvd.descuento*100) / nvd.monto_neto),2)as numeric(5,2)) as porcentajeDescuento--%>
                            <asp:TemplateField HeaderText="Código">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigoProducto" runat="server" Text='<%# Bind("codigo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="lblNombreProducto" runat="server" Text='<%# Bind("nom_producto") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Fecha">
                                <ItemTemplate>
                                    <asp:Label ID="lblFecha" runat="server" Text='<%# Bind("Fecha","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cantidad">
                                <ItemTemplate>
                                    <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("cantidad", "{0:n0}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Valor Unitario">
                                <ItemTemplate>
                                    <asp:Label ID="lblValorUnitario" runat="server" Text='<%# Bind("valorUnitario", "{0:n0}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Monto Total">
                                <ItemTemplate>
                                    <asp:Label ID="lblMontoTotal" runat="server" Text='<%# Bind("monto_Total", "{0:n0}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descuento">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescuento" runat="server" Text='<%# Bind("descuento", "{0:n0}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="% Descuento">
                                <ItemTemplate>
                                    <asp:Label ID="lblPorcentajeDescuento" runat="server" Text='<%# Bind("porcentajeDescuento") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Vendedor">
                                <ItemTemplate>
                                    <asp:Label ID="lblVendedor" runat="server" Text='<%# Bind("vendedor") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nom Contacto">
                                <ItemTemplate>
                                    <asp:Label ID="lblNombreContacto" runat="server" Text='<%# Bind("Nom_Contacto") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Dirección">
                                <ItemTemplate>
                                    <asp:Label ID="lblDireccion" runat="server" Text='<%# Bind("direccion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
            </asp:Panel>









            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="Button9" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mdlVerClientesAsociados" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlVerClientesAsociados" TargetControlID="Button9">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlVerClientesAsociados" runat="server" CssClass="panel" Style="display: none; background: white; width: 35%; height: auto; overflow: auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <button class="close" data-dismiss="modal">
                            ×
                        </button>
                        <strong>
                            <asp:Label ID="lblClientesAsociados" runat="server" Text="Clientes Asociados"></asp:Label>
                        </strong>
                    </div>

                    <asp:GridView ID="grvClientesAsociados" runat="server" CssClass="table table-bordered table-hover table-condensed table small" EmptyDataText="No hay productos vendidos por este cliente" HeaderStyle-CssClass="info" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Rut Cliente Asociado">
                                <ItemTemplate>
                                    <asp:Label ID="lblRutClienteAsociado" runat="server" Text='<%# Bind("RUT_CLIENTE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Razon Social">
                                <ItemTemplate>
                                    <asp:Label ID="lblRazonSocial" runat="server" Text='<%# Bind("RAZON_SOCIAL") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
            </asp:Panel>




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




            <asp:Button ID="Button2" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mdlInformacionValidacionProducto" BackgroundCssClass="modalBackground" runat="server" PopupControlID="Panel1" TargetControlID="Button2" BehaviorID="_mdlInfo">
            </asp:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" CssClass="panel" Style="display: none; background: white; width: 40%; height: auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <strong>Información</strong>
                    </div>
                    <div class="alert">
                        <asp:Label ID="lblInformacionValidacionProducto" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="panel-footer">
                        <asp:ImageButton ID="imgValidacion" runat="server" OnClick="imgValidacion_Click" ImageUrl="~/assets/img/accept.png" />

                    </div>
                </div>
                <%--MODALPOPUP CON BOOTSTRAP--%>
            </asp:Panel>










            <%--
            <asp:Panel ID="pnlUpdateProgress" runat="server" CssClass="loadingPage">
                <asp:UpdateProgress ID="upProgress" runat="server">
                    <ProgressTemplate>
                        <div class="alert alert-success">
                            <img src="assets/img/loading-v2.gif" alt="Cargando" />
                            Cargando...
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>ModalProgress
            <asp:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="pnlUpdateProgress"
                BackgroundCssClass="modalBackground" PopupControlID="pnlUpdateProgress">
            </asp:ModalPopupExtender>
            --%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
