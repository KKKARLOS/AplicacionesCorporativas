using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using GESTAR.Capa_Negocio;

namespace GESTAR.Capa_Presentacion.Catalogo
{
    /// <summary>
    /// Descripción breve de Catalogo de profesionales.
    /// </summary>
    public partial class CatGenerico : System.Web.UI.Page, ICallbackEventHandler
    {
        private string _callbackResultado = null;
        public string strTitulo;
        public string strCabecera;
        SqlDataReader dr;

        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                if (!Page.IsCallback)
                {
                    if (Session["IDRED"] == null)
                    {
                        try
                        {
                            Response.Redirect("~/SesionCaducadaModal.aspx", true);
                        }
                        catch (System.Threading.ThreadAbortException) { return; }
                    }
                    //Tipo de acceso b->Busqueda, m->Mantenimiento
                    if (Request.QueryString["op"] != null)
                        this.hdnTipoPant.Value = Request.QueryString["op"].ToString().ToUpper();

                    hdnOpcion.Text = Request.QueryString["OPCION"].ToString();
                    if (Request.QueryString["IDAREA"] != null)
                        hdnIDArea.Text = Request.QueryString["IDAREA"].ToString();
                    
                    if (Request.QueryString["SOLICITANTE"]!=null)
                        hdnEsSolicitante.Text = Request.QueryString["SOLICITANTE"].ToString();

                    if (hdnOpcion.Text == "Origen")
                    {
                        strTitulo = "origenes";
                        //strCabecera = "Origenes";
                    }
                    else if (hdnOpcion.Text == "Entrada")
                    {
                        strTitulo = "entradas";
                        //strCabecera = "Entradas";
                    }
                    else if (hdnOpcion.Text == "Alcance")
                    {
                        strTitulo = "alcances";
                        //strCabecera = "Alcances";
                    }
                    else if (hdnOpcion.Text == "Tipo")
                    {
                        strTitulo = "tipos";
                        //strCabecera = "Tipos";
                    }
                    else if (hdnOpcion.Text == "Producto")
                    {
                        strTitulo = "productos";
                        //strCabecera = "Productos";
                    }
                    else if (hdnOpcion.Text == "Proceso")
                    {
                        strTitulo = "procesos";
                        //strCabecera = "Procesos";
                    }
                    else if (hdnOpcion.Text == "Requisito")
                    {
                        strTitulo = "requisitos";
                        //strCabecera = "Requisitos";
                    }
                    else if (hdnOpcion.Text == "Causa")
                    {
                        strTitulo = "causas";
                        //strCabecera = "Causas";
                    }
                    else if (hdnOpcion.Text == "CR")
                    {
                        strTitulo = "CR";
                        //strCabecera = "CR";
                    }
                    else if (hdnOpcion.Text == "Area")
                    {
                        strTitulo = "áreas";
                        //strCabecera = "CR";
                    }
                    else if (hdnOpcion.Text == "Proveedor")
                    {
                        strTitulo = "proveedores";
                        //strCabecera = "Proveedores";
                    }
                    else if (hdnOpcion.Text == "Cliente")
                    {
                        strTitulo = "clientes";
                        //strCabecera = "Clientes";
                    }
                    else if (hdnOpcion.Text == "Coordinadores")
                    {
                        strTitulo = "coordinadores";
                    }
                    else if (hdnOpcion.Text == "Solicitantes")
                    {
                        strTitulo = "solicitantes";
                    }
                    else if (hdnOpcion.Text == "Especialistas")
                    {
                        strTitulo = "especialistas";
                    }
                    else if (hdnOpcion.Text == "Deficiencias")
                    {
                        strTitulo = "órdenes";
                    }


                    //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                    //   y la función que va a acceder al servidor
                    string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                    string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

                    //2º Se "registra" la función que va a acceder al servidor.
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
                }
            }
            catch (Exception ex)
            {
                hdnErrores.Text = Errores.mostrarError("Error al cargar la página", ex);
            }
        }

        public void RaiseCallbackEvent(string eventArg)
        {
            string sResultado = "";
            //1º Si hubiera argumentos, se recogen y tratan.
            //string MisArg = eventArg;
            string[] aArgs = Regex.Split(eventArg, "@@");
            sResultado = aArgs[0] + @"@@";
            sResultado += ObtenerDatos();
            //3º Damos contenido a la variable que se envía de vuelta al cliente.
            _callbackResultado = sResultado;
        }
        public string GetCallbackResult()
        {
            //Se envía el resultado al cliente.
            return _callbackResultado;
        }
        private string ObtenerDatos()
        {
            string sResul = "";
            try
            {
                StringBuilder strBuilder = new StringBuilder();
                int i = 0;

                strBuilder.Append("<table id='tblDatos' class='texto' style='width: 396px;'>");
                strBuilder.Append("<colgroup><col style='width:396px;' /></colgroup>");
                if (hdnOpcion.Text == "Area")
                    dr = Areas.Listado(int.Parse(Session["IDFICEPI"].ToString()), Session["ADMIN"].ToString());
                else if (hdnOpcion.Text == "Tipo")
                    dr = TIPO.Catalogo(null, short.Parse(hdnIDArea.Text), "", null, 4, 0);
                else if (hdnOpcion.Text == "Entrada")
                    dr = ENTRADA.Catalogo(null, short.Parse(hdnIDArea.Text), "", null, 4, 0);
                else if (hdnOpcion.Text == "Alcance")
                    dr = ALCANCE.Catalogo(null, short.Parse(hdnIDArea.Text), "", null, 4, 0);
                else if (hdnOpcion.Text == "Proceso")
                    dr = PROCESO.Catalogo(null, short.Parse(hdnIDArea.Text), "", null, 4, 0);
                else if (hdnOpcion.Text == "Producto")
                    dr = PRODUCTO.Catalogo(null, short.Parse(hdnIDArea.Text), "", null, 4, 0);
                else if (hdnOpcion.Text == "Requisito")
                    dr = REQUISITO.Catalogo(null, short.Parse(hdnIDArea.Text), "", null, 4, 0);
                else if (hdnOpcion.Text == "CR")
                    dr = CR.Catalogo();
                else if (hdnOpcion.Text == "CR_TEXTO")
                    dr = CR.Catalogo(short.Parse(hdnIDArea.Text));
                else if (hdnOpcion.Text == "Cliente")
                    dr = Cliente.Catalogo(short.Parse(hdnIDArea.Text));
                else if (hdnOpcion.Text == "Proveedor")
                    dr = Proveedor.Catalogo(short.Parse(hdnIDArea.Text));
                else if (hdnOpcion.Text == "Causa")
                    dr = CAUSA.Catalogo(null, short.Parse(hdnIDArea.Text), "", null, 4, 0);
                else if (hdnOpcion.Text == "Origen")
                    dr = ORIGEN.Catalogo(null, short.Parse(hdnIDArea.Text), "", null, 4, 0);
                else if (hdnOpcion.Text == "Coordinadores")
                    dr = Areas.CoordinadoresArea( int.Parse(hdnIDArea.Text));
                else if (hdnOpcion.Text == "Solicitantes")
                    dr = Areas.SolicitantesArea(int.Parse(hdnIDArea.Text));
                else if (hdnOpcion.Text == "Especialistas")
                    dr = Areas.LeerTecnicosArea(int.Parse(hdnIDArea.Text));
                else if (hdnOpcion.Text == "Deficiencias")
                    dr = Areas.DeficienciasArea(int.Parse(hdnIDArea.Text));

                while (dr.Read())
                {
                    //if (i % 2 == 0) strBuilder.Append("<tr class=FA ");
                    //else strBuilder.Append("<tr class=FB ");
                    i++;
                    strBuilder.Append("<tr ");
                    if (hdnOpcion.Text == "Cliente" || hdnOpcion.Text == "Proveedor" || hdnOpcion.Text == "CR_TEXTO")
                        strBuilder.Append("id='" + i.ToString() + "'");
                    else
                        strBuilder.Append("id='" + dr["ID"].ToString() + "'");
                    strBuilder.Append(" onclick='ms(this)' ondblclick='aceptarClick(this);' onmousemove='TTip(event);' style='cursor:pointer;height:16px'>");
                    //strBuilder.Append("<td width='15%'>" + (int.Parse(dr["ID"].ToString())).ToString("#,###,##0") + "</td>");

                    if (hdnOpcion.Text == "Cliente" || hdnOpcion.Text == "Proveedor" || hdnOpcion.Text == "CR_TEXTO")
                        strBuilder.Append("<td style='padding-left:5px'><label class=texto id='lbl" + i.ToString() + "' style='width:315px;text-overflow:ellipsis;overflow:hidden'");
                    else
                        strBuilder.Append("<td style='padding-left:5px'><label class=texto id='lbl" + dr["ID"].ToString() + "' style='width:315px;text-overflow:ellipsis;overflow:hidden'");
                    if (dr["DESCRIPCION"].ToString().Length > 80)
                    {
                        strBuilder.Append(" title='" + dr["DESCRIPCION"].ToString() + "'");
                    }
                    strBuilder.Append("><nobr class='NBR W395'>" + dr["DESCRIPCION"] + "</nobr></label></td></tr>");
                }

                dr.Close();
                dr.Dispose();

                strBuilder.Append("</table>");

                sResul = "OK@@" + strBuilder.ToString();
            }
            catch (Exception ex)
            {
                sResul = "Error@@" + Errores.mostrarError("Error al obtener los datos", ex);
            }
            return sResul;
        }
    }
}
