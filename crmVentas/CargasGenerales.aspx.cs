using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DAL;

namespace crm_valvulas_industriales
{
    public partial class CargasGenerales : System.Web.UI.Page
    {
        Datos dal = new Datos();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.lbtnGrabar);
                
                if (!this.Page.IsPostBack)
                {
                    buscarEstados();
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        protected void lbtnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                litLogError.Text = string.Empty;

                if (fuArchivo.HasFile)
                {
                    string idUsuario = Session["variableIdUsuario"].ToString();
                    //string idCliente = Session["idCliente"].ToString();
                    // se verifica si la extension del archivo
                    string tipoArchivo = fuArchivo.FileName;
                    tipoArchivo = tipoArchivo.Substring(tipoArchivo.LastIndexOf(".") + 1).ToLower();
                    // se valida el archivo si es PDF
                    if (tipoArchivo == "csv")
                    {
                        fuArchivo.SaveAs(Server.MapPath("formatosCarga/temp/" + fuArchivo.FileName));

                        DataTable dt = new DataTable();
                        List<string[]> testParse = parseCSV(Server.MapPath("formatosCarga/temp/" + fuArchivo.FileName));

                        foreach (string column in testParse[0])
                        {
                            dt.Columns.Add(column);
                        }

                        for (int n = 1; n < testParse.Count; n++)
                        {
                            string[] row = testParse[n];
                            dt.Rows.Add(row);
                        }

                        if (ddlTipoProceso.SelectedValue == "CARGA_CLIENTES")
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                string rutCliente = item["RUT"].ToString();
                                string idEstadoCliente = item["ID_ESTADO"].ToString();
                                string montoAprobacion = item["MONTO_APROBACION"].ToString();
                                string condicionDeVenta = item["CONDICION_DE_VENTA"].ToString();

                                dal.setEditarClienteCargaMasiva(rutCliente, idEstadoCliente, montoAprobacion.Replace(",", "."), condicionDeVenta);
                            }

                            lblInformacion.Text = "Proceso realizado correctamente";
                            mdlInformacion.Show();
                        }

                        if (ddlTipoProceso.SelectedValue == "CARGA_PRODUCTOS")
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                string codigo = item["CODIGO"].ToString();
                                string precio = item["PRECIO"].ToString();

                                dal.setEditarProductoCargaMasiva(codigo,precio.Replace(",","."));
                            }

                            lblInformacion.Text = "Proceso realizado correctamente";
                            mdlInformacion.Show();
                        }

                        if (ddlTipoProceso.SelectedValue == "ACTUALIZACION_PRODUCTOS")
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                string producto = item["NOM_PRODUCTO"].ToString();
                                string codigo = item["CODIGO"].ToString();
                                string bodega = item["BODEGA"].ToString();
                                string idGrupo = item["ID_GRUPO"].ToString();
                                string idSubGrupo = item["ID_SUB_GRUPO"].ToString();
                                string medida = item["U_MEDIDA"].ToString();
                                string stock = item["STOCK"].ToString();
                                string costoUnitario = item["COSTO_UNITARIO"].ToString();
                                string valorVenta = item["VALOR_VENTA"].ToString();

                                dal.setEditarProductoMasivo(producto, codigo, bodega, idGrupo, idSubGrupo, medida, stock, costoUnitario, valorVenta);
                                //dal.setEditarProductoCargaMasiva(codigo, precio.Replace(",", "."));
                            }

                            lblInformacion.Text = "Proceso realizado correctamente";
                            mdlInformacion.Show();
                        }

                        if (ddlTipoProceso.SelectedValue == "CARGA_CLIENTES_CARGA")
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                string rutCliente = item["RUT_CLIENTE"].ToString();
                                string razonSocial = item["RAZON_SOCIAL"].ToString();
                                string telefono = item["TELEFONO"].ToString();
                                string direccion = item["DIRECCION"].ToString();
                                string comuna = item["COMUNA"].ToString();
                                string ciudad = item["CIUDAD"].ToString();
                                string email = item["EMAIL"].ToString();
                                string info = item["INFO"].ToString();
                                string montoCredito = item["MONTO_CREDITO"].ToString();
                                string clasificacion = item["CLASIFICACION"].ToString();
                                string zona = item["ZONA"].ToString();
                                string idTipoCliente = item["ID_TIPO_CLIENTE"].ToString();
                                string condicionVenta = item["CONDICION_DE_VENTA"].ToString();
                                string giro = item["GIRO"].ToString();
                                string idActividadComercial = item["ID_ACTIVIDAD_COMERCIAL"].ToString();
                                string rutClientePadre = item["RUT_CLIENTE_PADRE"].ToString();

                                DataTable dtCli = new DataTable();
                                dtCli = dal.getBuscarClientePorRut(rutCliente).Tables[0];
                                int si = dtCli.Rows.Count;

                                if (si > 0)
                                {
                                    litLogError.Text += "Rut cliente " + rutCliente + " ya existe en la base de datos <br>";
                                }

                                dal.setIngresarClienteMasivo(rutCliente, razonSocial, telefono, direccion, comuna, ciudad, email, info, montoCredito, clasificacion, zona, giro, condicionVenta, idActividadComercial, idTipoCliente, rutClientePadre);
                            }
                            lblInformacion.Text = "Proceso realizado correctamente";
                            mdlInformacion.Show();
                        }

                        if (ddlTipoProceso.SelectedValue == "CARGA_CONTACTOS")
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                string rutCliente = item["RUT_CLIENTE"].ToString();
                                string nomContacto = item["NOM_CONTACTO"].ToString();
                                string email1 = item["EMAIL_1"].ToString();
                                string email2 = item["EMAIL_2"].ToString();
                                string celular = item["CELULAR"].ToString();
                                string telefono1 = item["TELEFONO1"].ToString();
                                string telefono2 = item["TELEFONO2"].ToString();
                                string idCargo = item["ID_CARGO"].ToString();

                                dal.setIngresarContacto(nomContacto, rutCliente, email1, email2, celular, telefono1, telefono2, idCargo);
                            }

                            lblInformacion.Text = "Proceso realizado correctamente";
                            mdlInformacion.Show();
                        }

                        if (ddlTipoProceso.SelectedValue == "CARGA_LEAD")
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                //NOMBRE	EMPRESA	CARGO	DIRECCION	COMUNA	EMAIL	TELEFONO_1	TELEFONO_2	TIENE_SW_ERP	TIENE_SW_COBROS	TIENE_SW_TICKET	
                                //TIENE_SW_VENTAS	COMENTARIO

                                string nombre = item["NOMBRE"].ToString();
                                string empresa = item["EMPRESA"].ToString();
                                string cargo = item["CARGO"].ToString();
                                string direccion = item["DIRECCION"].ToString();
                                string comuna = item["COMUNA"].ToString();
                                string email = item["EMAIL"].ToString();
                                string telefono1 = item["TELEFONO_1"].ToString();
                                string telefono2 = item["TELEFONO_2"].ToString();

                                int tieneSwERP = 0;
                                if (!string.IsNullOrEmpty(item["TIENE_SW_ERP"].ToString()))
                                {
                                    tieneSwERP = Convert.ToInt32(item["TIENE_SW_ERP"].ToString());
                                }

                                int tieneSwCobros = 0;
                                if (!String.IsNullOrEmpty(item["TIENE_SW_COBROS"].ToString()))
                                {
                                    tieneSwCobros = Convert.ToInt32(item["TIENE_SW_COBROS"].ToString());
                                }

                                int tieneSwTicket = 0;
                                if (!String.IsNullOrEmpty(item["TIENE_SW_TICKET"].ToString()))
                                {
                                    tieneSwTicket = Convert.ToInt32(item["TIENE_SW_TICKET"].ToString());
                                }

                                int tieneSwVentas = 0;
                                if (!String.IsNullOrEmpty(item["TIENE_SW_VENTAS"].ToString()))
                                {
                                    tieneSwVentas = Convert.ToInt32(item["TIENE_SW_VENTAS"].ToString());
                                }

                                string comentario = item["COMENTARIO"].ToString();

                                dal.setIngresarLead(nombre, empresa, cargo, direccion, comuna, email, telefono1, telefono2, tieneSwERP, tieneSwCobros, tieneSwTicket, tieneSwVentas, comentario, 1, Convert.ToInt32(idUsuario),Convert.ToInt32(idUsuario));

                            }

                            lblInformacion.Text = "Proceso realizado correctamente";
                            mdlInformacion.Show();
                        }
                    }
                    else
                    {
                        lblInformacion.Text = "Su archivo no es de tipo .csv";
                        mdlInformacion.Show();
                    }
                }
                else
                {
                    lblInformacion.Text = "Favor de adjuntar un archivo .csv";
                    mdlInformacion.Show();
                }
            }
            catch (Exception ex)
            {
                lblInformacion.Text = ex.Message;
                mdlInformacion.Show();
            }
        }

        public List<string[]> parseCSV(string path)
        {
            List<string[]> parsedData = new List<string[]>();

            using (StreamReader readFile = new StreamReader(path))
            {
                string line;
                string[] row;

                while ((line = readFile.ReadLine()) != null)
                {
                    row = line.Split(';');
                    parsedData.Add(row);
                }
            }
            return parsedData;
        }


        void buscarEstados()
        {
            DataTable dt = new DataTable();
            dt = dal.getBuscarEstadoCliente(null, null).Tables[0];

            grvClientes.DataSource = dt;
            grvClientes.DataBind();

            grvTipoCliente.DataSource = dal.getBuscarTipoCliente(null);
            grvTipoCliente.DataBind();

            grvActividadComercial.DataSource = dal.getBuscarActividadComercial();
            grvActividadComercial.DataBind();

            grvCondicionVenta.DataSource = dal.getBuscarCondicionVenta(null);
            grvCondicionVenta.DataBind();
        }
    }
}