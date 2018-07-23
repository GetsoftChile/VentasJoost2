<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="IngresoPago.aspx.cs" Inherits="crm_valvulas_industriales.IngresoPago" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<br />
    <ol class="breadcrumb">
        <li><a href="Default.aspx">Caja</a></li>
        <li class="active">Ingreso de pagos</li>
    </ol>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <div class="panel panel-primary">
        <div class="panel-heading">
            <strong>Modulo Caja - Ingreso de Pago</strong>
        </div>

        <table class="table small">
        <tr class="active">
            <td>
                <strong>Nota de Venta: </strong>
                <asp:Label ID="lblIdNotaVenta" runat="server"></asp:Label>
            </td>
            <td>
                <strong>Rut: </strong>
                <asp:Label ID="lblRutCliente" runat="server"></asp:Label>
            </td>
            <td>
                <strong>Nombre Cliente: </strong>
                <asp:Label ID="lblNombreCliente" runat="server"></asp:Label>
            </td>
            <td>
                <strong>Monto Neto: </strong>
                <asp:Label ID="lblMontoNeto" runat="server"></asp:Label>
            </td>
            <td>
                <strong>Iva: </strong>
                <asp:Label ID="lblIva" runat="server"></asp:Label>
            </td>
             <td>
                <strong>Monto Total: </strong>
                <asp:Label ID="lblMontoTotal" runat="server"></asp:Label>
            </td>
        </tr>
        </table>

        <div class="panel-heading">
            <strong>Detalle Pago</strong>
        </div>

        <table class="table small">
        <tr class="active">
            <td width="200px">
                <strong>Forma de Pago</strong>
                <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td>
                <strong>Rut del cheque</strong>
                <asp:TextBox ID="txtRutCheque" Enabled="false" runat="server" MaxLength="10" CssClass="form-control input-sm"></asp:TextBox>
            </td>
            <td>
                <strong>Monto</strong>
                <asp:TextBox ID="txtMonto" Enabled="false" runat="server" MaxLength="10" CssClass="form-control input-sm"></asp:TextBox>
            </td>
            <td>
                <strong>Fecha Vencimiento</strong>
                <asp:TextBox ID="txtFechaVencimiento"  Enabled="false" runat="server" MaxLength="10" CssClass="form-control input-sm"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender3" runat="server" 
                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
                TargetControlID="txtFechaVencimiento"></asp:CalendarExtender>
            </td>
            <td>
                <strong>Banco</strong>
                <asp:DropDownList ID="ddlBanco" Enabled="false" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </td>
            <td>
                <strong>Nro. Cheque o Dep</strong>
                <asp:TextBox ID="txtNumeroChequeODeposito" Enabled="false" runat="server" MaxLength="20" CssClass="form-control input-sm"></asp:TextBox>
            </td>
            <td>
                <strong>Cuenta Cte</strong>
                <asp:TextBox ID="txtCuentaCorriente" Enabled="false" runat="server" MaxLength="20" CssClass="form-control input-sm"></asp:TextBox>
            </td>
            <td>
                <br />
                <asp:LinkButton ID="lbtnAgregar" OnClick="lbtnAgregar_Click" CssClass="btn btn-success btn-sm" runat="server" 
                    ><i aria-hidden="true" class="glyphicon glyphicon-plus"></i></asp:LinkButton>
            </td>
        </tr>
        </table>


        
            <asp:GridView ID="grvPago" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" AutoGenerateColumns="false" ShowFooter="true" onrowdatabound="GridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Forma de pago" ControlStyle-Width="130px">
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblIdFormaPago" runat="server" Text='<%# Bind("ID_FORMA_PAGO") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblFormaPago" runat="server" Text='<%# Bind("FORMA_PAGO") %>'></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text="Total:" Font-Bold="true" />
                            </FooterTemplate> 
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Rut del cheque" ControlStyle-Width="80px">
                            <ItemTemplate>
                                <asp:Label ID="lblRutCheque" runat="server" Text='<%# Bind("RUT_CHEQUE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Monto" FooterStyle-HorizontalAlign="Right" ControlStyle-Width="85px">
                            <ItemTemplate>
                                <asp:Label ID="lblMonto" runat="server" Visible="false" Text='<%# Bind("MONTO") %>'></asp:Label>
                                <asp:Label ID="lblMonto2" runat="server" Text='<%# Eval("MONTO", "{0:n0}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"/>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalMonto" runat="server"  />
                            </FooterTemplate> 
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Fecha Vencimiento" ControlStyle-Width="60px">
                            <ItemTemplate>
                                <asp:Label ID="lblFechaVencimiento" runat="server" Text='<%# Bind("FECHA_VENCIMIENTO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Banco">
                            <ItemTemplate>
                                <asp:Label ID="lblIdBanco" Visible="false" runat="server" Text='<%# Bind("ID_BANCO") %>'></asp:Label>
                                <asp:Label ID="lblBanco" runat="server" Text='<%# Bind("BANCO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nro. Cheque o Dep">
                            <ItemTemplate>
                                <asp:Label ID="lblNumeroChequeDeposito" runat="server" Text='<%# Bind("NUMERO_CHEQUE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cuenta Cte">
                            <ItemTemplate>
                                <asp:Label ID="lblCuentaCorriente" runat="server" Text='<%# Bind("CUENTA_CORRIENTE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEliminar" OnClick="lbtnEliminar_Click" runat="server" runat="server" ImageUrl="~/assets/img/delete.png"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>



                <div class="panel-footer">
                    <asp:LinkButton ID="lbtnGrabar" CssClass="btn btn-danger btn-sm" runat="server" 
                    onclick="lbtnGrabar_Click"><i aria-hidden="true" class="glyphicon glyphicon-floppy-disk"></i> Grabar</asp:LinkButton>
                </div>

        </div>
        




        
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
