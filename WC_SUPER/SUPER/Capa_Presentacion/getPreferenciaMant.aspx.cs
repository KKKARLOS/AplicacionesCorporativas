using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;

public partial class getPreferenciaMant : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string sErrores = "", strTablaHTML = "";
    public string sPantalla = "0";
    public int nIDPreferencia = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            if (!Page.IsCallback)
            {
                bool bEsCV = false;
                if (Request.QueryString["nP"] != null)
                    sPantalla = Utilidades.decodpar(Request.QueryString["nP"].ToString());
                if (Request.QueryString["CV"] != null)
                {
                    if (Request.QueryString["CV"] == "S")
                    {
                        bEsCV = true;
                        this.hdnEsCV.Value = "S";
                    }
                }
                if (Request.QueryString["nPref"] != null)
                    nIDPreferencia = int.Parse(Utilidades.decodpar(Request.QueryString["nPref"].ToString()));

                ObtenerPreferencias(short.Parse(sPantalla), bEsCV);
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }

        //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
        //   y la funci�n que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2� Se "registra" la funci�n que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1� Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            //case ("setDefecto"):
            //    sResultado += setDefecto(aArgs[1]);
            //    break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2]);
                break;
        }

        //3� Damos contenido a la variable que se env�a de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }

    protected void ObtenerPreferencias(short nPantalla, bool bEsCV)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr;
            if (bEsCV)
                dr = PREFERENCIAUSUARIO.CatalogoPantallaUsuarioCVT(null, (int)Session["IDFICEPI_CVT_ACTUAL"], nPantalla);
            else
                dr = PREFERENCIAUSUARIO.CatalogoPantallaUsuario(null, (int)Session["IDFICEPI_PC_ACTUAL"], nPantalla);

            sb.Append("<table id='tblDatos' class='texto MANO' style='WIDTH: 400px; table-layout: fixed;' cellSpacing='0' cellPadding='0' border='0' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:15px;' /><col style='width:20px;' /><col style='width:340px;' /><col style='width:25px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDatos'>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t462_idPrefUsuario"].ToString() + "' ");
                sb.Append("orden='" + dr["t462_orden"].ToString() + "' bd='' onclick='mm(event)' style='height: 20px;'>");
                sb.Append("<td style='text-align:center;'><img src='../images/imgFN.gif'></td>");
                sb.Append("<td><img src='../images/imgMoveRow.gif' style='cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>");
                sb.Append("<td style='padding-left:3px;'><input type='text' class='txtL' style='width:330px;' value=\"" + dr["t462_denominacion"].ToString() + "\" maxlength='50' onFocus='this.select()' onKeyUp='fm(event)'></td>");
                if ((bool)dr["t462_defecto"]) sb.Append("<td style='text-align:center;'><input type='checkbox' class='check' onclick='setDefecto(this);fm(event)' checked></td>");
                else sb.Append("<td style='text-align:center;'><input type='checkbox' class='check' onclick='setDefecto(this);fm(event)'></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaHTML = sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener la relaci�n de preferencias.", ex);
        }
    }
    protected string Grabar(string sEsCV, string strDatos)
    {
        string sResul = "";

        #region abrir conexi�n y transacci�n
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexi�n", ex);
            return sResul;
        }
        #endregion

        try
        {
            string[] aPreferencia = Regex.Split(strDatos, "///");
            foreach (string oPreferencia in aPreferencia)
            {
                if (oPreferencia == "") continue;
                string[] aValores = Regex.Split(oPreferencia, "##");
                //0. Opcion BD. "I", "U", "D"
                //1. ID Preferencia
                //2. Denominacion
                //3. Defecto
                //4. Orden

                switch (aValores[0])
                {
                    case "U":
                        if (sEsCV == "S")
                            PREFERENCIAUSUARIO.UpdateCatalogoCVT(tr, int.Parse(aValores[1]), Utilidades.unescape(aValores[2]), (aValores[3] == "1") ? true : false, byte.Parse(aValores[4]));
                        else
                            PREFERENCIAUSUARIO.UpdateCatalogo(tr, int.Parse(aValores[1]), Utilidades.unescape(aValores[2]), (aValores[3] == "1") ? true : false, byte.Parse(aValores[4]));
                        break;
                    case "D":
                        if (sEsCV == "S")
                            PREFERENCIAUSUARIO.DeleteCVT(tr, int.Parse(aValores[1]));
                        else
                            PREFERENCIAUSUARIO.Delete(tr, int.Parse(aValores[1]));
                        break;
                }
            }
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar las preferencias.", ex, false);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

    //private string setDefecto(string sIdPrefUsuario)
    //{
    //    try
    //    {
    //        PREFERENCIAUSUARIO.setDefecto(null, int.Parse(sIdPrefUsuario));
    //        return "OK@#@";
    //    }
    //    catch (Exception ex)
    //    {
    //        return "Error@#@" + Errores.mostrarError("Error al establecer la preferencia por defecto.", ex);
    //    }
    //}

}
