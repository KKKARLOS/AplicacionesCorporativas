using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_Administracion_EstructuraOrg_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 9;
                Master.bFuncionesLocales = true;
                Master.bEstilosLocales = true;
                Master.TituloPagina = "Mantenimiento de alertas por proyecto";
                Master.FuncionesJavaScript.Add("Javascript/dhtmltooltip.js");
                Master.FuncionesJavaScript.Add("Javascript/funcionesPestVertical.js");

                cargarNodos("pge", true);
            }
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
        }
        //1º Se indican (por este orden) la función a la que se va a devolver el resultado
        //   y la función que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);

    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += ObtenerProyectos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9]);
                break;
            //case "setAlertaProyecto":
            //    sResultado += setAlertaProyecto(byte.Parse(aArgs[1]), (aArgs[2] == "1") ? true : false, aArgs[3]);
            //    break;
            case "grabar":
                sResultado += Grabar(aArgs[1]);
                break;
        } 

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    private string ObtenerProyectos(string sNumPE, string sDesPE, string sTipoBusqueda, string sNodo, 
        string sIdCliente, string sIDContrato, string sIdResponsable, string sIdHorizontal, string sEstado)
    {
        try
        {
            StringBuilder sbTM = new StringBuilder();
            StringBuilder sbTMF1 = new StringBuilder();
            StringBuilder sbBF = new StringBuilder();
            StringBuilder sbBM = new StringBuilder();

            bool bColgroupCreado = false;
            int nWidthBM = 0;
            int nID = 0;

            DataSet ds = PROYECTOSUBNODO.ObtenerProyectosAlertas(null,
                            (sNumPE == "") ? null : (int?)int.Parse(sNumPE),
                            Utilidades.unescape(sDesPE),
                            sTipoBusqueda,
                            (sNodo == "") ? null : (int?)int.Parse(sNodo),
                            (sIdCliente == "") ? null : (int?)int.Parse(sIdCliente),
                            (sIDContrato == "") ? null : (int?)int.Parse(sIDContrato),
                            (sIdResponsable == "") ? null : (int?)int.Parse(sIdResponsable),
                            (sIdHorizontal == "") ? null : (int?)int.Parse(sIdHorizontal),
                            (sEstado == "") ? null : sEstado);

            string sTooltip = "";
            int nAnchuraCol = 60;
            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                if (!bColgroupCreado)
                {
                    bColgroupCreado = true;

                    #region tblTituloMovil
                    nWidthBM = ds.Tables[1].Rows.Count * nAnchuraCol;
                    sbTM.Append("<table id='tblTituloMovil' class='TBLINI' style='width:" + nWidthBM.ToString() + "px; display:block;' cellpadding='0' cellspacing='0' border='1'>");
                    sbTM.Append("    <colgroup>");
                    #endregion

                    #region tblBodyFijo
                    sbBF.Append("<table id='tblBodyFijo' style='width:500px;' cellpadding='0' cellspacing='0' border='1'>");
                    sbBF.Append("<colgroup>");
                    sbBF.Append("   <col style='width:30px;' />");
                    sbBF.Append("   <col style='width:55px;' />");
                    sbBF.Append("   <col style='width:415px;' />");
                    sbBF.Append("</colgroup>");
                    sbBF.Append("<tbody>");

                    #endregion

                    #region tblBodyMovil
                    sbBM.Append("<table id='tblBodyMovil' style='width:" + nWidthBM.ToString() + "px;' cellpadding='0' cellspacing='0' border='0'>");
                    sbBM.Append("    <colgroup>");
                    #endregion

                    #region Creacion Colgroups Móviles
                    sbTMF1.Append("<tr style='height:17px;'>");

                    foreach (DataRow oFilaAlerta in ds.Tables[1].Rows)//Tabla de ALERTAS
                    {
                        sbTM.Append("   <col style='width:" + nAnchuraCol.ToString() + "px;' />");
                        //sbTMF1.Append("   <td style='width:40px;text-align:center; background-color:red;' onmouseover='showTTE(\"" + Utilidades.escape("<label style=width:50px>Grupo:</label>" + oFilaAlerta["t821_denominacion"].ToString() + "<br/><label style=width:50px>Asunto:</label>" + oFilaAlerta["t820_denominacion"].ToString()) + "\")' onMouseout=\"hideTTE()\">A" + oFilaAlerta["t820_idalerta"].ToString() + "</td>");
                        sTooltip = Utilidades.escape("<label style='width:50px'>Grupo:</label>" + oFilaAlerta["t821_denominacion"].ToString() + "<br/><label style='width:50px'>Asunto:</label>" + oFilaAlerta["t820_denominacion"].ToString());
                        sbTMF1.Append("   <td id='" + oFilaAlerta["t820_idalerta"].ToString() + "' style='width:" + nAnchuraCol.ToString() + "px;text-align:center;' onmouseover=showTTE(\"" + sTooltip + "\") onMouseout=\"hideTTE()\">");
                        sbTMF1.Append("<img src='../../../images/botones/imgmarcar.gif' onclick='setAlertaProyecto(" + oFilaAlerta["t820_idalerta"].ToString() + ",1)' title='Activa la alerta a todos los proyectos marcados' style='cursor:pointer; margin-right:2px;' />");
                        sbTMF1.Append("A" + oFilaAlerta["t820_idalerta"].ToString());
                        sbTMF1.Append("<img src='../../../images/botones/imgdesmarcar.gif' onclick='setAlertaProyecto(" + oFilaAlerta["t820_idalerta"].ToString() + ",0)' title='Desactiva la alerta a todos los proyectos marcados' style='cursor:pointer; margin-left:2px;' />");
                        sbTMF1.Append("</td>");

                        sbBM.Append("   <col style='width:" + nAnchuraCol.ToString() + "px;' />");
                    }

                    sbTM.Append("</colgroup>");

                    sbTMF1.Append("</tr>");
                    sbTM.Append(sbTMF1.ToString());
                    sbTM.Append("</table>");

                    sbBM.Append("</colgroup>");

                    #endregion
                }

                #region tblBodyFijo
                sbBF.Append("<tr style='height:20px' id='"+ oFila["t305_idproyectosubnodo"].ToString()+ "' ");
                sbBF.Append("categoria='" + oFila["t301_categoria"].ToString() + "' ");
                //sbBF.Append("cualidad='" + oFila["t305_cualidad"].ToString() + "' ");
                //sbBF.Append("estado='" + oFila["t301_estado"].ToString() + "' ");
                sbBF.Append(">");

                sbBF.Append("<td style='text-align:center;'><input type='checkbox' class='check' style='cursor:pointer;' /></td>");
                //sbBF.Append("<td></td>");
                //sbBF.Append("<td></td>");
                sbBF.Append("<td style='text-align:right; padding-right:10px;'>" + int.Parse(oFila["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");

                sTooltip = Utilidades.escape("<label style='width:70px;'>Proyecto:</label>" + oFila["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + oFila["Responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + oFila["t302_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>"+ Estructura.getDefCorta(Estructura.sTipoElem.NODO) +":</label>" + oFila["t303_denominacion"].ToString().Replace((char)34, (char)39));

                sbBF.Append("<td style='border-right: solid 3px #A6C3D2;'><nobr class='NBR W350' onmouseover=showTTE(\"" + sTooltip + "\") onMouseout=\"hideTTE()\">" + oFila["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sbBF.Append("</tr>");

                #endregion

                #region tblBodyMovil
                //sbBM.Append("<tr style='height:20px;'>");
                nID++;
                sbBM.Append("<tr id='" + oFila["t305_idproyectosubnodo"].ToString() + "' style='height:20px;' ");// cursor:pointer;
                //sbBM.Append("onclick='setFilaMovil(this)' ");
                sbBM.Append(">");

                foreach (DataRow oFilaAlerta in ds.Tables[1].Rows)
                {
                    sbBM.Append("<td style='width:" + nAnchuraCol.ToString() + "px; text-align:center;'>");
                    sbBM.Append("<input type='checkbox' style='cursor:pointer;' alerta='" + oFilaAlerta["t820_idalerta"].ToString() + "' onclick=\"setAlerta(this)\" ");
                    if ((bool)oFila["alerta_" + oFilaAlerta["t820_idalerta"].ToString()])
                        sbBM.Append(" checked");
                    sbBM.Append(" />");
                    sbBM.Append("</td>");
                }

                sbBM.Append("</tr>");
                #endregion
            }
            //dr.Close();
            ds.Dispose();
            sbBF.Append("</tbody>");
            sbBF.Append("</table>");
            sbBM.Append("</table>");

            return "OK@#@" + "<div style=\"background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:500px; height:auto;\">" + sbBF.ToString() +"</div>{{septabla}}"
                    + sbTM.ToString() + "{{septabla}}"
                    + "<div style=\"background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:" + nWidthBM.ToString() + "px; height:auto;\">" + sbBM.ToString() + "</div>" + "{{septabla}}";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la estructura organizativa", ex);
        }
    }
    private void cargarNodos(string sModulo, bool bSoloActivos)
    {
        try
        {
            //Cargar el combo de nodos accesibles t303_estado
            lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
            ListItem oLI = null;
            SqlDataReader dr = NODO.CatalogoConEstructura();
            while (dr.Read())
            {
                oLI = new ListItem(dr["denominacion"].ToString(), dr["nodo"].ToString());
                cboCR.Items.Add(oLI);
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //private string setAlertaProyecto(byte nAlerta, bool bActivar, string sPSNs)
    //{
    //    try
    //    {
    //        PSNALERTAS.EstablecerAlertaProyecto(nAlerta, bActivar, sPSNs);

    //        return "OK@#@";
    //    }
    //    catch (Exception ex)
    //    {
    //        return "Error@#@" + Errores.mostrarError("Error al establecer las alertas a nivel de proyecto.", ex);
    //    }
    //}
    private string Grabar(string sDatosAlertas)
    {
        try
        {
            PSNALERTAS.EstablecerAlertaProyectosubnodo(sDatosAlertas);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al establecer las alertas a nivel de proyecto.", ex);
        }
    }

}
