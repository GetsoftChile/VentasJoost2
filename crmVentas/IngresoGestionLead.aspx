<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="IngresoGestionLead.aspx.cs" Inherits="crm_valvulas_industriales.IngresoGestionLead" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



    
      <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li><a href="Default.aspx">Tareas</a></li>
          <li class="active">Ingreso Gestión</li>
    </ol>



    <div class="panel panel-primary">
                  <div class="panel-heading">
                    <button class="close" data-dismiss="modal">
                        ×
                    </button>
                    <strong>
                    <asp:Label ID="lblAgregarLead" runat="server" Text="Lead"></asp:Label>
                    </strong>
                  </div>
                    <asp:HiddenField ID="hfIdLead" runat="server" />

                    <table class="table table-condensed small">
                        <tr class="active">
                            <td ><strong>Nombre</strong>
                                <asp:TextBox ID="txtNombre"  runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Empresa</strong>
                                <asp:TextBox ID="txtEmpresa" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Cargo</strong>
                                <asp:TextBox ID="txtCargo" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Dirección</strong>
                                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Comuna</strong>
                                <asp:DropDownList ID="ddlComuna" runat="server" CssClass="form-control input-sm" OnDataBound="ddlComuna_DataBound"></asp:DropDownList>
                            </td>
                            <td><strong>Email</strong>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Teléfono 1</strong>
                                <asp:TextBox ID="txtTelefono1" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Teléfono 2</strong>
                                <asp:TextBox ID="txtTelefono2" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td><strong>Tiene SW ERP</strong>
                                <asp:DropDownList ID="ddlTieneSwERP" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td><strong>Tiene SW Cobros</strong>
                                <asp:DropDownList ID="ddlTieneSwCobros" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td><strong>Tiene SW Ticket</strong>
                                <asp:DropDownList ID="ddlTieneSwTicket" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                    
                                </asp:DropDownList>
                            </td>
                            <td><strong>Tiene SW Ventas</strong>
                                <asp:DropDownList ID="ddlTieneSwVentas" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="active">
                            <td>
                                <strong>SW ERP</strong>
                                <asp:TextBox ID="txtSwErp" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <strong>SW Cobros</strong>
                                <asp:TextBox ID="txtSwCobros" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <strong>SW Ticket</strong>
                                <asp:TextBox ID="txtSwTicket" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <strong>SW Ventas</strong>
                                <asp:TextBox ID="txtSwVentas" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td colspan="2"><strong>Comentario</strong>
                                <asp:TextBox ID="txtComentario" runat="server" Height="100" TextMode="MultiLine" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td><strong>Activos</strong>
                                <asp:DropDownList ID="ddlActivo" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td><br />
                                <asp:HiddenField ID="hfIdUsuarioAsignado" runat="server" />
                                <asp:Button ID="btnModificarLead" runat="server" Text="Guardar" CssClass="btn btn-primary btn-sm" onclick="btnModificarLead_Click" />
                            </td>
                        </tr>

                        </table>
                <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>

    
            <div id="divPanelGestionar" runat="server">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Gestión</h3>
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

                </div>

            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>Historial Gestiones Seguimiento</strong>
                </div>
                <asp:GridView ID="grvHistorialGestiones" runat="server" HeaderStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed table small" AutoGenerateColumns="false" EmptyDataText="No existe información">
                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdGestion" runat="server" Text='<%# Bind("ID_GESTION_LEAD") %>'></asp:Label>
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
                                    <asp:Label ID="lblFechaAgendamiento" runat="server"  Text='<%# Bind("FECHA_AGENDAMIENTO2", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Obs" >
                                <ItemTemplate>
                                    <asp:label id="lblObservacion" runat ="server" Text='<%# Eval("OBSERVACION2") %>' ToolTip='<%# Eval("OBSERVACION") %>'></asp:label>
                                    <asp:Image ID="imgObservacion" runat="server" ImageUrl="~/assets/img/text.png" Visible="false" ToolTip='<%# Bind("OBSERVACION") %>'/>
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
                


            
            <asp:Button ID="btnActivarPopUp" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="mdlInformacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlInformacion" TargetControlID="btnActivarPopUp">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlInformacion" runat="server" CssClass="panel" style="display:none; background:white; width:40%; height:auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                <div id="panelInformacion" runat="server" class="panel panel-red">
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
            </asp:Panel>



</asp:Content>
