<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="Campanas.aspx.cs" Inherits="crm_valvulas_industriales.Campanas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<br />
    <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li><a href="Default.aspx">Administración</a></li>
          <li class="active">Campañas</li>
    </ol>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>


    <asp:Panel ID="pnlClientes" runat="server" DefaultButton="btnBuscar">
        <div class="well well-sm">

        <h4>Campañas</h4>
        <div class="form-inline">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control input-sm" placeholder="Buscar"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary btn-sm" 
            Text="Buscar" onclick="btnBuscar_Click"
            />
            <asp:Button ID="btnNuevoCampana" runat="server" Text="Nueva Campaña" 
            CssClass="btn btn-success btn-sm" onclick="btnNuevoCampana_Click"/>
        </div>
        </div>
    </asp:Panel>
        
    <asp:GridView ID="grvCampanas" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false" AllowPaging="True" PageSize="15" OnRowDataBound="paginacion_RowDataBound">
        <Columns>

        <asp:TemplateField HeaderText="Id">
                <ItemTemplate>
                    <asp:Label ID="lblIdCampana" runat="server" Text='<%# Bind("ID_CAMPANA") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Nombre">
                <ItemTemplate>
                    <asp:Label ID="lblNombreCampana" runat="server" Text='<%# Bind("NOM_CAMPANA") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                        
            <asp:TemplateField HeaderText="Fecha Inicio">
                <ItemTemplate>
                    <asp:Label ID="lblFechaInicio" runat="server" Text='<%# Bind("FECHA_INICIO", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Fecha Termino">
                <ItemTemplate>
                    <asp:Label ID="lblFechaTermino" runat="server" Text='<%# Bind("FECHA_TERMINO", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                       
            <asp:TemplateField HeaderText="Activo">
                <ItemTemplate>
                    <asp:Label ID="lblIdActivo" runat="server" Visible="false" Text='<%# Bind("ACTIVA") %>'></asp:Label>
                    <asp:Label ID="lblActivo" runat="server" Text='<%# Bind("ACTIVA2") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderStyle-Width="7%">
                <ItemTemplate>
                    <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/assets/img/edit_button.png" OnClick="imgEditar_Click" ToolTip="Editar"  />
                    <asp:ImageButton ID="imgEliminar" runat="server" ImageUrl="~/assets/img/delete.png" OnClick="imgEliminar_Click" onclientclick="return confirm('¿Desea eliminar el registro?');" ToolTip="Eliminar"/>
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


                
            <%--MODALPOPUP CON BOOTSTRAP--%>
            <asp:Button ID="btnAgregarClienteMDL" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="mdlAgregarCliente" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlAgregar" TargetControlID="btnAgregarClienteMDL" BehaviorID="_mdlAgregarCliente">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlAgregar" runat="server" CssClass="panel" style="display:none; background:white; width:75%; height:50%;overflow:auto">
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
                            <td><strong>Nombre Campaña</strong></td>
                            <td colspan="3">
                                <asp:TextBox ID="txtNombreCampana" Width="27%" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="active">
                            <td>
                                <strong>Fecha Inicio</strong></td>
                            <td>
                                <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
                                TargetControlID="txtFechaInicio"></asp:CalendarExtender>
                            </td>
                            <td><strong>Fecha Termino</strong></td>
                            <td><asp:TextBox ID="txtFechaTermino" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
                                TargetControlID="txtFechaTermino"></asp:CalendarExtender>
                            
                            </td>
                        </tr>
                        
                        <tr class="active">
                            <td><strong>Activo</strong></td>
                            <td>
                                <asp:DropDownList ID="ddlActiva" runat="server" Width="150px" CssClass="form-control input-sm">
                                <asp:ListItem Text="SI" Value="1"></asp:ListItem>
                                <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td><strong></strong></td>
                            <td></td>

                        </tr>
                        </table>

                <div class="panel-footer">
                    <asp:Button ID="btnModificar" runat="server" Text="Guardar" CssClass="btn btn-primary" onclick="btnModificar_Click" />
                    <asp:Button ID="btnAgregar" runat="server" Text="Guardar" CssClass="btn btn-primary" onclick="btnAgregar_Click" />
                </div>
                <%--MODALPOPUP CON BOOTSTRAP--%>
                </div>
            </asp:Panel>






            

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


    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
