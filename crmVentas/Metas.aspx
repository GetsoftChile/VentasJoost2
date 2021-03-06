﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="Metas.aspx.cs" Inherits="crm_valvulas_industriales.Metas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <ol class="breadcrumb">
        <li><a href="Default.aspx">Inicio</a></li>
        <li><a href="Default.aspx">Administración</a></li>
        <li class="active">Metas</li>
    </ol>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <strong>Metas</strong>
                </div>
                <table class="table table-condensed small">
                    <tr class="success"> 
                        <td>
                            <strong>Vendedor</strong>
                            <asp:DropDownList ID="ddlVendedor" runat="server" OnDataBound="ddlVendedor_DataBound" CssClass="form-control input-sm">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <strong>Mes</strong>
                            <asp:DropDownList ID="ddlMes" runat="server" CssClass="form-control input-sm">
                                <asp:ListItem Value="1" Text="Enero"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Febrero"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Marzo"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Abril"></asp:ListItem>
                                <asp:ListItem Value="5" Text="Mayo"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Junio"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Julio"></asp:ListItem>
                                <asp:ListItem Value="8" Text="Agosto"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Septiembre"></asp:ListItem>
                                <asp:ListItem Value="10" Text="Octubre"></asp:ListItem>
                                <asp:ListItem Value="11" Text="Noviembre"></asp:ListItem>
                                <asp:ListItem Value="12" Text="Diciembre"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <strong>Ano</strong>
                            <asp:DropDownList ID="ddlAno" runat="server" CssClass="form-control input-sm">
                                <asp:ListItem Value="2017"></asp:ListItem>
                                <asp:ListItem Value="2018"></asp:ListItem>
                                <asp:ListItem Value="2019"></asp:ListItem>
                                <asp:ListItem Value="2020"></asp:ListItem>
                                <asp:ListItem Value="2021"></asp:ListItem>
                                <asp:ListItem Value="2022"></asp:ListItem>
                                <asp:ListItem Value="2023"></asp:ListItem>
                                <asp:ListItem Value="2024"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                       
                        <td>
                            <br />
                            <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-default btn-sm" Text="Buscar" OnClick="btnBuscar_Click" />
                            <asp:Button ID="btnGrabar" runat="server" CssClass="btn btn-default btn-sm" Text="Grabar Meta" OnClick="btnGrabar_Click" />
                        </td>
                    </tr>
                </table>
            </div>

            <asp:GridView ID="grvMetas" runat="server"
                CssClass="table table-bordered table-hover table-condensed table small"
                HeaderStyle-CssClass="active" PagerStyle-CssClass="active" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Vendedor">
                        <ItemTemplate>
                            <asp:Label ID="lblIdVendedor" runat="server" Text='<%# Bind("ID_USUARIO") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblVendedor" runat="server" Text='<%# Bind("USUARIO") %>' Visible="true"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mes" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblMes" runat="server" Text='<%# Bind("MES") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ano" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAno" runat="server" Text='<%# Bind("ANO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FechaCierre">
                        <ItemTemplate>
                            <asp:TextBox ID="txtFechaCierre" runat="server" CssClass="form-control input-sm" Text='<%# Bind("FECHA_CIERRE","{0:dd-MM-yyyy}") %>' ></asp:TextBox>
                            <asp:CalendarExtender ID="ce_txtFechaDesdeAgendamiento" runat="server" 
                                Enabled="True" FirstDayOfWeek="Monday" Format="dd-MM-yyyy" 
                                TargetControlID="txtFechaCierre"></asp:CalendarExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Meta Mensual">
                        <ItemTemplate>
                            <asp:TextBox ID="txtMetaMensual" runat="server" CssClass="form-control input-sm" Text='<%# Bind("META_VENDIDO") %>' ></asp:TextBox>

                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>


            </asp:GridView>

            
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

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
