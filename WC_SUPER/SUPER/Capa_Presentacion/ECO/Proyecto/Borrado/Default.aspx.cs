using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text.RegularExpressions;
using EO.Web; 
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;
//para unescape


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "<table id='tblDatos'></table>", gsTipo = "E", sErrores;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 43;
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.TituloPagina = "Borrado de proyectos";
            if (!Page.IsPostBack)
            {
                try
                {
                    //Cargo la denominacion del label Nodo
                    string sAux = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    if (sAux.Trim() != "")
                    {
                        this.lblNodo2.InnerText = sAux;
                        this.lblNodo2.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    }
                    ObtenerProyectos(false);
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener el catálogo de plantillas", ex);
                }
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("eliminar"):
                sResultado += EliminarProyectos(aArgs[1]);
                break;
            case ("buscar"):
                sResultado += ObtenerProyectos(true);
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        return _callbackResultado;
    }
    private string EliminarProyectos(string slIDProy)
    {
        string sResul = "", sEstado, sMens="", sBorrados="";
        int IdProy, IdPSN;
        bool bBorrable = true;
        #region apertura de conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion
        try
        {
            string[] lstProy = Regex.Split(slIDProy, "##");
            for (int i = 0; i < lstProy.Length; i++)
            {
                string[] oProy = Regex.Split(lstProy[i].ToString(), ",");
                if (oProy[0].ToString() != "")
                {
                    IdPSN = int.Parse(oProy[0].ToString());
                    IdProy = int.Parse(oProy[1].ToString());
                    sEstado = oProy[2].ToString();
                    bBorrable = true;
                    #region Comprobaciones
                    if (sEstado == "A")
                    {
                        //Compruebo que no haya meses cerrados
                        if (SEGMESPROYECTOSUBNODO.HayMesesCerradosPosteriores(tr, IdPSN, 0))
                        {
                            bBorrable = false;
                            sMens += "El proyecto " + IdProy.ToString("#,###") + " no se puede borrar pues tiene meses cerrados.\n";
                        }
                        if (bBorrable)
                        {
                            //Compruebo que no tenga réplicas
                            SqlDataReader dr = PROYECTOSUBNODO.ObtenerReplicasDeProyecto(tr, IdPSN);
                            if (dr.Read())
                            {
                                bBorrable = false;
                                sMens += "El proyecto " + IdProy.ToString("#,###") + " no se puede borrar pues tiene proyectos replicados.\n";
                            }
                            dr.Close();
                            dr.Dispose();
                        }
                        if (bBorrable)
                        {
                            //Compruebo que no tenga imputaciones
                            if (PROYECTOSUBNODO.TieneConsumos(tr, IdPSN))
                            {
                                bBorrable = false;
                                sMens += "El proyecto " + IdProy.ToString("#,###") + " no se puede borrar pues tiene imputaciones de IAP.\n";
                            }
                        }
                        if (bBorrable)
                        {
                            //Compruebo que no tenga ANOTACIONES EN gasvi
                            if (PROYECTOSUBNODO.TieneApuntesGasvi(tr, IdPSN))
                            {
                                bBorrable = false;
                                sMens += "El proyecto " + IdProy.ToString("#,###") + " no se puede borrar pues tiene anotaciones en GASVI.\n";
                            }
                        }
                    }
                    #endregion
                    if (bBorrable)
                    {
                        PROYECTO.Delete(tr, IdProy);
                        sBorrados += IdProy.ToString() + "#";
                        //Si estamos borrando el último proyecto al que hemos accedido lo elimino de las vbles de sesión
                        if (IdPSN.ToString() == Session["ID_PROYECTOSUBNODO"].ToString())
                        {
                            Session["ID_PROYECTOSUBNODO"] = "";
                            Session["MODOLECTURA_PROYECTOSUBNODO"] = false;
                            Session["RTPT_PROYECTOSUBNODO"] = false;
                            Session["MONEDA_PROYECTOSUBNODO"] = "";
                            break;
                        }

                    }
                }
            }
            Conexion.CommitTransaccion(tr);
            //sResul = "OK@#@" + sBorrados + "@#@" + sMens;
            sResul = "OK@#@" + sMens;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al borrar los proyectos indicados.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string ObtenerProyectos(bool bRecarga)
    {
        StringBuilder sb = new StringBuilder();
        string sResul = "";
        try
        {
            sb.Append("<table id='tblDatos' class='texto' style='width: 960px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:40px;' />");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:65px; ' />");
            sb.Append("<col style='width:355px' />");
            sb.Append("<col style='width:220px' />");
            sb.Append("<col style='width:220px' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = PROYECTO.ObtenerProyectosBorrables((int)Session["UsuarioActual"]);

            while (dr.Read())
            {
                sb.Append("<tr style='height:20px' id='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("pry='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append(">");

                //sb.Append("<td><input type='checkbox' class='checkTabla' onclick='bCambios=true;'></td>");
                sb.Append("<td style='text-align:center;'><input type='checkbox' class='checkTabla' ></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right; padding-right:10px;'");
                if (ConfigurationManager.AppSettings["MOSTRAR_MOTIVO_PROY"] == "1")
                    sb.Append(" title=\"" + dr["desmotivo"].ToString() + "\"");
                sb.Append(">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");

                sb.Append("<td><nobr class='NBR W350' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["Responsable"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");

                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W220'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W220'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            if (bRecarga)
                sResul= "OK@#@" + sb.ToString();
            else
                strTablaHTML = sb.ToString();

            return sResul;
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los proyectos económicos", ex);
            return "Error@#@Error al obtener los proyectos económicos";
        }
    }
}
