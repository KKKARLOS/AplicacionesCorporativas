using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using EO.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTMLPerfPri = "", strTablaHTMLPerfPub = "", strTablaHTMLPerfAje = "", sNombreProfesional = "", sErrores="";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }
        try
        {
            if (!Page.IsCallback)
            {
                try
                {
                    hdnIdTipo.Value = Request.QueryString["t"].ToString();
                    sNombreProfesional = Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString();
                    obtenerFamiliasPerfiles(int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()));
                }
                catch (Exception ex)
                {
                    sErrores = Errores.mostrarError("Error al cargar los datos ", ex);
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
            sErrores = Errores.mostrarError("Error al cargar la pantalla", ex);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()));
                break;
            case ("getDatosPestana"):
                sResultado += "OK@#@" + aArgs[1] + "@#@";
                switch (int.Parse(aArgs[1]))
                {
                    case 0://Perfiles
                        //sResultado += obtenerDatosPE(aArgs[1], aArgs[2]);
                        break;
                    case 1://Entornos
                        sResultado += obtenerFamiliasEntornos(int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()));
                        break;
                }
                break;
            case ("perfiles"):
                sResultado += "OK@#@" + obtenerPerfilesFamilia(int.Parse(aArgs[1]));
                break;
            case ("entornos"):
                sResultado += "OK@#@" + obtenerEntornosFamilia(int.Parse(aArgs[1]));
                break;
            case ("publicarFamilia"):
                sResultado += PublicarFamilia(int.Parse(aArgs[1]), aArgs[2], int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()));
                break;
            case ("importarFamilia"):
                sResultado += ImportarFamilia(int.Parse(aArgs[1]), aArgs[2], int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()));
                break;
            case ("publicarFamiliaEntorno"):
                sResultado += PublicarFamiliaEntorno(int.Parse(aArgs[1]), aArgs[2], int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()));
                break;
            case ("importarFamiliaEntorno"):
                sResultado += ImportarFamiliaEntorno(int.Parse(aArgs[1]), aArgs[2], int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()));
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sFamPerfil">Familias de perfiles</param>
    /// <param name="sPerfilesFam">Perfiles de una famulia</param>
    /// <param name="sFamEntorno">Familias de entornos</param>
    /// <param name="sEntornosFam">Entornos de una familia</param>
    /// <returns></returns>
    private string Grabar(string sFamPerfil, string sFamEntorno, int idFicepi)
    {
        string sRes = "OK";
        int idFamPerf = -1, idFamEnt=-1;
        bool bPublica = false;
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);

            #region Perfiles

            if (sFamPerfil != "")
            {
                string[] aFamPerf = Regex.Split(sFamPerfil, @"///");
                for (int i = 0; i < aFamPerf.Length - 1; i++)
                {
                    if (aFamPerf[i] != "")
                    {
                        string[] aPerf = Regex.Split(aFamPerf[i], @"##");
                        if (aPerf[3] == "1") bPublica = true;
                        else bPublica = false;
                        switch (aPerf[0])
                        {
                            case "I":
                                idFamPerf = SUPER.BLL.FamiliaPerfil.Insertar(tr, Utilidades.unescape(aPerf[2]), idFicepi, bPublica);
                                break;
                            case "U":
                                SUPER.BLL.FamiliaPerfil.Modificar(tr, int.Parse(aPerf[1]), Utilidades.unescape(aPerf[2]), idFicepi, bPublica);
                                break;
                            case "D":
                                SUPER.BLL.FamiliaPerfil.Borrar(tr, int.Parse(aPerf[1]));
                                break;
                        }
                    }
                }
            }

            #endregion

            #region Entornos
            if (sFamEntorno != "")
            {
                string[] aFamEnt = Regex.Split(sFamEntorno, @"///");
                for (int i = 0; i < aFamEnt.Length - 1; i++)
                {
                    if (aFamEnt[i] != "")
                    {
                        string[] aEnt = Regex.Split(aFamEnt[i], @"##");
                        if (aEnt[3] == "1") bPublica = true;
                        else bPublica = false;
                        switch (aEnt[0])
                        {
                            //case "I":
                            //    idFamEnt = SUPER.BLL.FamiliaEntorno.Insertar(tr, Utilidades.unescape(aEnt[2]), idFicepi, bPublica);
                            //    break;
                            //case "U":
                            //    SUPER.BLL.FamiliaEntorno.Modificar(tr, int.Parse(aEnt[1]), Utilidades.unescape(aEnt[2]), idFicepi, bPublica);
                            //    break;
                            case "D":
                                SUPER.BLL.FamiliaEntorno.Borrar(tr, int.Parse(aEnt[1]));
                                break;
                        }
                    }
                }
            }
            #endregion

            Conexion.CommitTransaccion(tr);
            sRes = "OK@#@";// +sCad;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sRes = "Error@#@" + Errores.mostrarError("Error al grabar familias", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sRes;
    }

    #region Perfiles

    private void obtenerFamiliasPerfiles(int t001_idficepi)
    {
        StringBuilder sbPri = new StringBuilder();//Familias de perfiles privadas
        StringBuilder sbPub = new StringBuilder();//Familias de perfiles públicas
        StringBuilder sbAje = new StringBuilder();//Familias de perfiles ajenas
        sbPri.Append("<table id='tblFamPerPri' style='width:450px;' mantenimiento='0'>");
        sbPub.Append("<table id='tblFamPerPub' style='width:450px;' mantenimiento='0'>");
        sbAje.Append("<table id='tblFamPerAje' style='width:450px;' mantenimiento='0'>");

        //sbPri.Append("<colgoup><col style='width:350px' /><col style='width:100px' /></colgoup>");
        sbPub.Append("<colgoup><col style='width:350px' /><col style='width:100px' /></colgoup>");
        sbAje.Append("<colgoup><col style='width:350px' /><col style='width:100px' /></colgoup>");

        sbPri.Append("<tbody>");
        sbPub.Append("<tbody>");
        sbAje.Append("<tbody>");

        List<SUPER.BLL.FamiliaPerfil> Lista = SUPER.BLL.FamiliaPerfil.Catalogo();
        foreach (SUPER.BLL.FamiliaPerfil oElem in Lista)
        {
            if (oElem.t859_publica)
            {
                /*
                sbPub.Append("<tr id='" + oElem.t859_idfamperfil + "' style='height:20px;' class='MA' bd='' onclick='mostrarPerf(this,2)'");
                //sbPub.Append(" ondblclick='mostrarDetPerf(this,2)' onkeyup='modFilaPerf(this)'>");
                sbPub.Append(" ondblclick='mostrarDetPerf(this,2)' >");
                //sbPub.Append("<td><img src='../../../../images/imgFN.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border:0px;' /></td>");
                sbPub.Append("<td><input type='text' class='txtL' maxlength='50' style='width:345px; ");
                sbPub.Append("cursor: url(../../../../images/imgManoAzul2.cur),pointer;' ");
                sbPub.Append(" value='" + oElem.t859_denominacion + "' title='" + oElem.t859_denominacion + "'></td>");
                sbPub.Append("<td><input type='text' class='txtL' style='width:95px; ");
                sbPub.Append("cursor: url(../../../../images/imgManoAzul2.cur),pointer;' ");
                sbPub.Append(" value='" + oElem.Modificador + "' title='" + oElem.Modificador + "'></td>");
                sbPub.Append("</tr>");
                 * */
                sbPub.Append("<tr id='" + oElem.t859_idfamperfil + "' f=" + t001_idficepi.ToString()+ " style='height:20px;' class='MA' ");
                sbPub.Append(" onclick='mostrarPerf(this,2)' ondblclick='mostrarDetPerf(this,2)' >");
                sbPub.Append("<td style='padding-left:3px;'><nobr class='NBR W350' ");
                sbPub.Append(" title='" + oElem.t859_denominacion + "'>" + oElem.t859_denominacion + "</nobr></td>");
                sbPub.Append("<td style='padding-left:3px;'><nobr class='NBR W100' ");
                sbPub.Append(" title='" + oElem.Modificador + "'>" + oElem.Modificador + "</nobr></td>");
                sbPub.Append("</tr>");
            }
            else
            {
                if (oElem.t001_idficepi_autor == t001_idficepi)
                {
                    sbPri.Append("<tr id='" + oElem.t859_idfamperfil + "' style='height:20px;' class='MA' bd='' onclick='mostrarPerf(this,1)'");
                    //sbPri.Append(" ondblclick='mostrarDetPerf(this)' onkeyup=\"mfa(this,'U')\">");
                    sbPri.Append(" ondblclick='mostrarDetPerf(this,1)' onkeyup='modFilaPerf(this)'>");
                    //sbPri.Append("<td><img src='../../../../images/imgFN.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border:0px;' /></td>");
                    sbPri.Append("<td>");
                    //sbPri.Append("<input type='text' class='txtL' maxlength='50' style='width:380px; cursor: url(../../../../images/imgManoAzul2.cur),pointer;' value='" + oElem.t859_denominacion + "'></td></tr>");
                    sbPri.Append("<input type='text' class='txtL' maxlength='50' style='width:445px; ");
                    sbPri.Append("cursor: url(../../../../images/imgManoAzul2.cur),pointer;' ");
                    sbPri.Append(" value='" + oElem.t859_denominacion + "' title='" + oElem.t859_denominacion + "'></td></tr>");
                }
                else
                {
                    sbAje.Append("<tr id='" + oElem.t859_idfamperfil + "' style='height:20px;' class='MANO' onclick='mostrarPerf(this,3)'>");
                    sbAje.Append("<td style='padding-left:3px;'><nobr class='NBR W350' ");
                    sbAje.Append(" title='" + oElem.t859_denominacion + "'>" + oElem.t859_denominacion + "</nobr></td>");
                    sbAje.Append("<td style='padding-left:3px;'><nobr class='NBR W100' ");
                    sbAje.Append(" title='" + oElem.Modificador + "'>" + oElem.Modificador + "</nobr></td>");
                    sbAje.Append("</tr>");
                }
            }
        }
        sbPri.Append("</tbody></table>");
        sbPub.Append("</tbody></table>");
        sbAje.Append("</tbody></table>");

        strTablaHTMLPerfPri = sbPri.ToString();
        strTablaHTMLPerfPub = sbPub.ToString();
        strTablaHTMLPerfAje = sbAje.ToString();

    }
    private string obtenerPerfilesFamilia(int idFamilia)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table id='tblPerFam' style='width:370px;' mantenimiento='0'>");
        sb.Append("<tbody>");

        //List<SUPER.BLL.FamiliaPerfil> Lista = SUPER.BLL.FamiliaPerfil.Catalogo();
        List<SUPER.BLL.ElementoLista> Lista = SUPER.BLL.FamiliaPerfil.CatalogoPerfil(idFamilia);
        foreach (SUPER.BLL.ElementoLista oElem in Lista)
        {
            sb.Append("<tr id='" + oElem.sValor + "' style='height:20px;'>");
            sb.Append("<td style='padding-left:3px;' title='" + oElem.sDenominacion + "'><nobr class='NBR W360'>");
            sb.Append(oElem.sDenominacion + "</nobr></td></tr>");
        }
        sb.Append("</tbody></table>");

        return sb.ToString();
    }
    private string PublicarFamilia(int idFam, string sDenFam, int idFicepi)
    {
        string sRes = "";
        try
        {
            SUPER.BLL.FamiliaPerfil.Publicar(null, idFam, idFicepi);
            sRes = "OK@#@" + idFam.ToString() + "@#@" + sDenFam;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sRes = "Error@#@" + Errores.mostrarError("Error al publicar familia", ex);
        }
        return sRes;
    }
    /// <summary>
    /// Dado una familia inserta una copia como familia privada
    /// </summary>
    /// <param name="idFam"></param>
    /// <param name="sDenfam"></param>
    /// <param name="idFicepi"></param>
    /// <returns></returns>
    private string ImportarFamilia(int idFam, string sDenFamilia, int idFicepi)
    {
        string sRes = "";
        string sDenFam = Utilidades.unescape(sDenFamilia);
        int idNewFam = -1;
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);

            //Puede ocurrir que yo haya publicado una familia y luego la quiera importar como privada.
            //En este caso, tendríamos una familia pública y otra privada con el mismo autor y el mismo nombre
            //Y por tanto daría error al grabar pues hay un índice único por autor y denominación
            if (SUPER.BLL.FamiliaPerfil.ExisteDenominacion(tr, sDenFam, idFicepi))
            {
                sDenFam = "COPIA DE " + sDenFam;
                if (sDenFam.Length > 50)
                    sDenFam = sDenFam.Substring(0, 50);
            }
            idNewFam = SUPER.BLL.FamiliaPerfil.Insertar(tr, sDenFam, idFicepi, false);
            SUPER.BLL.FamiliaPerfil.CopiarPerfiles(tr, idFam, idNewFam);

            Conexion.CommitTransaccion(tr);
            sRes = "OK@#@" + idNewFam.ToString() + "@#@" + sDenFam;// +"@#@" + sDenProfesional;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sRes = "Error@#@" + Errores.mostrarError("Error al importar familia", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sRes;
    }

    #endregion

    #region Entornos
    private string obtenerEntornosFamilia(int idFamilia)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table id='tblEntFam' style='width:370px;' mantenimiento='0'>");
        sb.Append("<tbody>");

        List<SUPER.BLL.ElementoLista> Lista = SUPER.BLL.FamiliaEntorno.CatalogoEntorno(idFamilia);
        foreach (SUPER.BLL.ElementoLista oElem in Lista)
        {
            sb.Append("<tr id='" + oElem.sValor + "' style='height:20px;'>");
            sb.Append("<td style='padding-left:3px;' title='" + oElem.sDenominacion + "'><nobr class='NBR W360'>");
            sb.Append(oElem.sDenominacion + "</nobr></td></tr>");
        }
        sb.Append("</tbody></table>");

        return sb.ToString();
    }
    private string obtenerFamiliasEntornos(int t001_idficepi)
    {
        string sRes = "";
        StringBuilder sbPri = new StringBuilder();//Familias de entornos privadas
        StringBuilder sbPub = new StringBuilder();//Familias de entornos públicas
        StringBuilder sbAje = new StringBuilder();//Familias de entornos ajenas
        sbPri.Append("<table id='tblFamEntPri' style='width:450px;' mantenimiento='0'>");
        sbPub.Append("<table id='tblFamEntPub' style='width:450px;' mantenimiento='0'>");
        sbAje.Append("<table id='tblFamEntAje' style='width:450px;' mantenimiento='0'>");

        //sbPri.Append("<colgoup><col style='width:350px' /><col style='width:100px' /></colgoup>");
        sbPub.Append("<colgoup><col style='width:350px' /><col style='width:100px' /></colgoup>");
        sbAje.Append("<colgoup><col style='width:350px' /><col style='width:100px' /></colgoup>");

        sbPri.Append("<tbody>");
        sbPub.Append("<tbody>");
        sbAje.Append("<tbody>");

        List<SUPER.BLL.FamiliaEntorno> Lista = SUPER.BLL.FamiliaEntorno.Catalogo();
        foreach (SUPER.BLL.FamiliaEntorno oElem in Lista)
        {
            if (oElem.t861_publica)
            {
                /*
                sbPub.Append("<tr id='" + oElem.t861_idfament + "' style='height:20px;' class='MA' bd='' onclick='mostrarEnt(this,2)'");
                sbPub.Append(" ondblclick='mostrarDetEnt(this,2)' onkeyup='modFilaEnt(this)'>");
                sbPub.Append("<td><img src='../../../../images/imgFN.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border:0px;' /></td>");
                sbPub.Append("<td>");
                sbPub.Append("<input type='text' class='txtL' maxlength='50' style='width:380px; ");
                sbPub.Append("cursor: url(../../../../images/imgManoAzul2.cur),pointer;' ");
                sbPub.Append(" value='" + oElem.t861_denominacion + "' title='" + oElem.t861_denominacion + "'></td></tr>");
                 * */
                sbPub.Append("<tr id='" + oElem.t861_idfament + "' f='" + t001_idficepi.ToString() + "' style='height:20px;' class='MA' ");
                sbPub.Append(" onclick='mostrarEnt(this,2)' ondblclick='mostrarDetEnt(this,2)' >");
                sbPub.Append("<td style='padding-left:3px;'><nobr class='NBR W350' ");
                sbPub.Append(" title='" + oElem.t861_denominacion + "'>" + oElem.t861_denominacion + "</nobr></td>");
                sbPub.Append("<td style='padding-left:3px;'><nobr class='NBR W100' ");
                sbPub.Append(" title='" + oElem.Modificador + "'>" + oElem.Modificador + "</nobr></td>");
                sbPub.Append("</tr>");
            }
            else
            {
                if (oElem.t001_idficepi_autor == t001_idficepi)
                {
                    sbPri.Append("<tr id='" + oElem.t861_idfament + "' style='height:20px;' class='MA' bd='' onclick='mostrarEnt(this,1)'");
                    //sbPri.Append(" ondblclick='mostrarDetEnt(this,1)' onkeyup='modFilaEnt(this)'>");
                    sbPri.Append(" ondblclick='mostrarDetEnt(this,1)' >");
                    //sbPri.Append("<td><img src='../../../../images/imgFN.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border:0px;' /></td>");
                    sbPri.Append("<td>");
                    //sbPri.Append("<input type='text' class='txtL' maxlength='50' style='width:380px; cursor: url(../../../../images/imgManoAzul2.cur),pointer;' value='" + oElem.t859_denominacion + "'></td></tr>");
                    sbPri.Append("<input type='text' class='txtL' maxlength='50' style='width:340px; ");
                    sbPri.Append("cursor: url(../../../../images/imgManoAzul2.cur),pointer;' ");
                    sbPri.Append(" value='" + oElem.t861_denominacion + "' title='" + oElem.t861_denominacion + "'></td></tr>");
                }
                else
                {
                    sbAje.Append("<tr id='" + oElem.t861_idfament + "' style='height:20px;' class='MANO' onclick='mostrarEnt(this,3)'>");
                    sbAje.Append("<td style='padding-left:3px;'><nobr class='NBR W350' ");
                    sbAje.Append(" title='" + oElem.t861_denominacion + "'>" + oElem.t861_denominacion + "</nobr></td>");
                    sbAje.Append("<td style='padding-left:3px;'><nobr class='NBR W100' ");
                    sbAje.Append(" title='" + oElem.Modificador + "'>" + oElem.Modificador + "</nobr></td>");
                    sbAje.Append("</tr>");
                }
            }
        }
        sbPri.Append("</tbody></table>");
        sbPub.Append("</tbody></table>");
        sbAje.Append("</tbody></table>");

        sRes = sbPri.ToString() + "///" + sbPub.ToString() + "///" + sbAje.ToString();

        return sRes;
    }
    private string PublicarFamiliaEntorno(int idFam, string sDenFam, int idFicepi)
    {
        string sRes = "";
        try
        {
            SUPER.BLL.FamiliaEntorno.Publicar(null, idFam, idFicepi);
            sRes = "OK@#@" + idFam.ToString() + "@#@" + sDenFam;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sRes = "Error@#@" + Errores.mostrarError("Error al publicar familia", ex);
        }
        return sRes;
    }
    private string ImportarFamiliaEntorno(int idFam, string sDenFamilia, int idFicepi)
    {
        string sRes = "";
        string sDenFam = Utilidades.unescape(sDenFamilia);
        int idNewFam = -1;
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);

            //Puede ocurrir que yo haya publicado una familia y luego la quiera importar como privada.
            //En este caso, tendríamos una familia pública y otra privada con el mismo autor y el mismo nombre
            //Y por tanto daría error al grabar pues hay un índice único por autor y denominación
            if (SUPER.BLL.FamiliaPerfil.ExisteDenominacion(tr, sDenFam, idFicepi))
            {
                sDenFam = "COPIA DE " + sDenFam;
                if (sDenFam.Length > 50)
                    sDenFam = sDenFam.Substring(0, 50);
            }
            idNewFam = SUPER.BLL.FamiliaEntorno.Insertar(tr, sDenFam, idFicepi, false);
            SUPER.BLL.FamiliaEntorno.CopiarEntornos(tr, idFam, idNewFam);

            Conexion.CommitTransaccion(tr);
            sRes = "OK@#@" + idNewFam.ToString() + "@#@" + sDenFam;// +"@#@" + sDenProfesional;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sRes = "Error@#@" + Errores.mostrarError("Error al importar familia", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sRes;
    }
    #endregion
}
