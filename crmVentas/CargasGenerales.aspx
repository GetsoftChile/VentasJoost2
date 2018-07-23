<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="CargasGenerales.aspx.cs" Inherits="crm_valvulas_industriales.CargasGenerales" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <ol class="breadcrumb">
          <li><a href="Default.aspx">Administración</a></li>
          <li class="active">Cargas Generales</li>
        </ol>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <div class="panel panel-primary">
        <div class="panel-heading">
            <strong>Modulo administración - cargas - cargas generales</strong>
        </div>
        <table class="table table-condensed small">

            <tr class="active">
                <td><strong>Tipo carga</strong></td>
                <td>
                    <asp:DropDownList ID="ddlTipoProceso" runat="server" CssClass="form-control input-sm" Width="200px">
                        <%--<asp:ListItem Value="CARGA" Text="Carga de Siniestro"></asp:ListItem>--%>
                        <%--<asp:ListItem Value="CARGA_D" Text="Carga de Siniestros Directos Judicial"></asp:ListItem>--%>
                        <asp:ListItem Value="CARGA_CLIENTES" Text="Actualización Clientes"></asp:ListItem>
                        <%--<asp:ListItem Value="CARGA_PRODUCTOS" Text="Carga Productos"></asp:ListItem>--%>
                        <asp:ListItem Value="ACTUALIZACION_PRODUCTOS" Text="Actualización Productos"></asp:ListItem>
                        <asp:ListItem Value="CARGA_CLIENTES_CARGA" Text="Carga Clientes"></asp:ListItem>
                        <asp:ListItem Value="CARGA_CONTACTOS" Text="Carga Contactos"></asp:ListItem>
                        <asp:ListItem Value="CARGA_LEAD" Text="Carga Lead"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="active">
                <td><strong>Archivo de carga</strong></td>
                <td>
                    <asp:FileUpload ID="fuArchivo" runat="server" CssClass="btn btn-default" />
                </td>
            </tr>
            <tr class="active">
                <td>
                <asp:LinkButton ID="lbtnGrabar" CssClass="btn btn-danger btn-sm" runat="server" OnClick="lbtnGrabar_Click">Procesar</asp:LinkButton>
                </td>
                <td></td>
            </tr>
        </table>
        <br />
        <table class="table table-condensed small">
        <%--<tr>
            <td width="200px"><strong>Formato de carga asignación</strong></td>
            <td>
            <a href="formatosCarga/FORMATO_CARGA.XLS">Descargar</a>
            </td>
        </tr>
        <tr>
            <td width="200px"><strong>Formato de carga Actualización</strong></td>
            <td>
            <a href="formatosCarga/FORMATO_ACTUALIZACION.XLS">Descargar</a>
            </td>
        </tr>--%>
        <tr class="active">
            <td width="200px"><strong>Formato de actualización Clientes</strong></td>
            <td>
            <a href="formatosCarga/FORMATO_ACT_CLIENTES.csv">Descargar</a>
            </td>
        </tr>
        <tr class="active">
            <td width="200px"><strong>Formato de carga Productos</strong></td>
            <td>
            <a href="formatosCarga/FORMATO_ACTUALIZACION_PRODUCTO.csv">Descargar</a>
            </td>
        </tr>

        <tr class="active">
            <td width="200px"><strong>Formato de carga Cliente</strong></td>
            <td>
            <a href="formatosCarga/CLIENTES.csv">Descargar</a>
            </td>
        </tr>

        <tr class="active">
            <td width="200px"><strong>Formato de carga Contactos</strong></td>
            <td>
            <a href="formatosCarga/CONTACTOS.csv">Descargar</a>
            </td>
        </tr>
        <tr class="active">
            <td width="200px"><strong>Formato de carga LEAD</strong></td>
            <td>
            <a href="formatosCarga/LEAD.csv">Descargar</a>
            </td>
        </tr>
        
        </table>

        <br />
        
    </div>

    

    <div class="row">
    <div class="col-sm-6">
    
        <div class="panel panel-primary">
        <div class="panel-heading">
            <strong>Estados del Cliente</strong>
        </div>
                <asp:GridView ID="grvClientes" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Id Estado">
                            <ItemTemplate>
                                <asp:Label ID="lblIdEstado" runat="server" Text='<%# Bind("ID_ESTADO_CLIENTE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("NOM_ESTADO_CLIENTE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
        </div>
    </div>

    <div class="col-sm-6">
    
        <div class="panel panel-primary">
        <div class="panel-heading">
            <strong>Tipo Cliente</strong>
        </div>
                <asp:GridView ID="grvTipoCliente" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Id Tipo">
                            <ItemTemplate>
                                <asp:Label ID="lblIdEstado" runat="server" Text='<%# Bind("ID_TIPO_CLIENTE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo Cliente">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoCliente" runat="server" Text='<%# Bind("NOM_TIPO_CLIENTE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
        </div>
    </div>
    </div>

    <div class="row">

        <div class="col-sm-6">
    
        <div class="panel panel-primary">
        <div class="panel-heading">
            <strong>Actividad Comercial</strong>
        </div>
                <asp:GridView ID="grvActividadComercial" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Id Actividad Comercial">
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID_ACTIVIDAD_COMERCIAL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre Actividad Comercial">
                            <ItemTemplate>
                                <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("NOM_ACTIVIDAD_COMERCIAL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
        </div>
        </div>


        <div class="col-sm-6">
    
        <div class="panel panel-primary">
        <div class="panel-heading">
            <strong>Condición Venta</strong>
        </div>
                <asp:GridView ID="grvCondicionVenta" runat="server" CssClass="table table-bordered table-hover table-condensed table small" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Id Condición Venta">
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID_COND_VENTA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre Condición Venta">
                            <ItemTemplate>
                                <asp:Label ID="lblNombreCondicionVenta" runat="server" Text='<%# Bind("CONDICION_VENTA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
        </div>
        </div>

    </div>

    <div class="row">
        <div class="col-sm-6">
            <asp:Literal ID="litLogError" runat="server"></asp:Literal>
        </div>
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


    </ContentTemplate>
</asp:UpdatePanel>


</asp:Content>
