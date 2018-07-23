<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="ReporteGestiones.aspx.cs" Inherits="crm_valvulas_industriales.ReporteGestiones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<br />
    <ol class="breadcrumb">
          <li><a href="Default.aspx">Inicio</a></li>
          <li><a href="Default.aspx">Control</a></li>
          <li><a href="Default.aspx">Gestiones</a></li>
          <li class="active">Gestiones Extrajudiciales</li>
    </ol>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlPrincipal" runat="server" DefaultButton="lbtnBuscar">
        <div class="well well-sm">
        
            <table class="table small">
                <tr>
                    <td>
                        <strong>Ejecutivo</strong>
                        <asp:DropDownList ID="ddlEjecutivo" runat="server" Visible="true" OnDataBound="ddlEjecutivo_DataBound" CssClass="form-control input-sm">
                        </asp:DropDownList>
                    </td>
                    <td><strong>Fecha Desde</strong>
                        <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control input-sm" Width="120px"></asp:TextBox>
                        <asp:CalendarExtender ID="ce_txtFecha" runat="server"
                            Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy"
                            TargetControlID="txtFechaDesde">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <strong>Fecha Hasta</strong>
                        <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control input-sm" Width="120px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                            Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy"
                            TargetControlID="txtFechaHasta">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <br />
                        <asp:LinkButton ID="lbtnBuscar" CssClass="btn btn-primary btn-sm" runat="server"
                            OnClick="lbtnBuscar_Click"><i aria-hidden="true" class="glyphicon glyphicon-search"></i> Buscar</asp:LinkButton>
                        <asp:ImageButton ID="ibtnExporte" runat="server" OnClick="ibtnExporte_Click" ImageUrl="~/assets/img/export_excel.png" />
                    </td>
                </tr>
            </table>
        </div>
        </asp:Panel>
        

        <asp:GridView ID="grvCantidadDeGestiones" EmptyDataText="Selección realizada no obtuvo registros" runat="server" HeaderStyle-CssClass="active" PagerStyle-CssClass="active" CssClass="table table-bordered table-hover table-condensed table small panel" AutoGenerateColumns="false" OnDataBound="grvCantidadDeGestiones_DataBound" OnRowDataBound="grvCantidadDeGestiones_RowDataBound" ShowFooter="True">
            <Columns>
                <asp:TemplateField HeaderText="Categoría">
                    <ItemTemplate>
                        <asp:Label ID="lblCategoria" runat="server" Text='<%# Bind("CATEGORIA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="SubCategoria">
                    <ItemTemplate>
                        <asp:Label ID="lblSubCategoria" runat="server" Text='<%# Bind("SUBCATEGORIA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Gestión">
                    <ItemTemplate>
                        <asp:Label ID="lblGestion" runat="server" Text='<%# Bind("GESTION") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Cantidad">
                    <ItemTemplate>
                        <asp:Label ID="lblCodCategoria" runat="server" Text='<%# Bind("CODCATEGORIA") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblCodSubCategoria" runat="server" Text='<%# Bind("CODSUBCATEGORIA") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblCodGestion" runat="server" Text='<%# Bind("CODGESTION") %>' Visible="false"></asp:Label>
                        <asp:LinkButton ID="lbtnCantidad" runat="server" Text='<%# Bind("CANTIDAD") %>' OnClick="lbtnCantidad_Click"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />

                    <FooterTemplate>
                        <div style="float: right">
                            <strong>Total: </strong>
                            <asp:LinkButton ID="lbtnTotalCantidad" OnClick="lbtnTotalCantidad_Click" runat="server"></asp:LinkButton>
                        </div>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>


        </asp:GridView>


        
        <asp:Button ID="btnActivarPopUp" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="mdlInformacion" BackgroundCssClass="modalBackground" runat="server" PopupControlID="pnlInformacion" TargetControlID="btnActivarPopUp" BehaviorID="_mdlInformacion">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlInformacion" runat="server" style="display:none; background:white; width:40%; height:auto">
                <%--MODALPOPUP CON BOOTSTRAP--%>
                  <div class="panel-header">
                        <button class="close" data-dismiss="modal">×</button>
                        <h3>Información</h3>
                  </div>
                  <div class="panel-body">
                    <div class="alert alert-error">
                        <asp:Label ID="lblInformacion" runat="server" Text=""></asp:Label>
                    </div>
                  </div>
                  <div class="panel-footer">
                    <asp:ImageButton ID="imgAceptar" runat="server" ImageUrl="~/assets/img/accept.png" />
                  </div>
                <%--MODALPOPUP CON BOOTSTRAP--%>
            </asp:Panel>


    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
