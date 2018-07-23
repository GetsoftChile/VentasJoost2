<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="AgendamientoLead.aspx.cs" Inherits="crm_valvulas_industriales.AgendamientoLead" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    

    <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li><a href="Default.aspx">Tareas</a></li>
          <li class="active">Agendamiento Lead</li>
    </ol>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            
            <div class="well well-sm">
                <div class="form-inline">
                    <strong>Fec Desde</strong>
                    <asp:TextBox ID="txtFechaDesdeAgendamiento" runat="server" Width="12%" CssClass="form-control input-sm"></asp:TextBox>
                    <asp:CalendarExtender ID="ce_txtFechaDesdeAgendamiento" runat="server"
                        Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy"
                        TargetControlID="txtFechaDesdeAgendamiento">
                    </asp:CalendarExtender>
                    <strong>Fec Hasta</strong>
                    <asp:TextBox ID="txtFechaHastaAgendamiento" runat="server" Width="12%" CssClass="form-control input-sm"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                        Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy"
                        TargetControlID="txtFechaHastaAgendamiento">
                    </asp:CalendarExtender>

                    <asp:Button ID="btnBuscarAgendamiento" CssClass="btn btn-primary btn-sm" runat="server" Text="Buscar" OnClick="btnBuscarAgendamiento_Click" />
                </div>

            </div>


            <div class="panel panel-info">
                <div class="panel-heading">
                    <strong>Agendamientos Lead</strong>
                </div>
                <asp:GridView ID="grvAgendamientos" HeaderStyle-CssClass="active" runat="server" CssClass="table table-bordered table-hover table-condensed table small" AutoGenerateColumns="false" EmptyDataText="No existe información">
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="lblIdGestion" runat="server" Text='<%# Bind("ID_GESTION_LEAD") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID Lead">
                            <ItemTemplate>
                                <asp:Label ID="lblRut" runat="server" Visible="true" Text='<%# Bind("ID_LEAD") %>'></asp:Label>
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
                                <asp:Label ID="lblHoraAgendamiento" runat="server" Text='<%# Bind("HORA_AGENDAMIENTO", "{0:dd/MM/yyyy}") %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="" Visible="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgSeleccionar" CausesValidation="False" runat="server" Visible="true" ImageUrl="~/assets/img/plus.png" ToolTip="Gestionar" OnClick="imgGestionarAgendamiento_Click" />
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
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
