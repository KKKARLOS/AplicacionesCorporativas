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
//
using System.Text;
using System.Text.RegularExpressions;
using EO.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtmlGP, strTablaHTMLIntegrantes, sErrores;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    strTablaHtmlGP = "<table id='tblDatos'><tbody id='tbodyDatos'></tbody></table>";
                    string strTabla0 = SUPER.Capa_Negocio.GrupoProf.Catalogo();
                    string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                    if (aTabla0[0] == "OK") strTablaHtmlGP = aTabla0[1];
                    else sErrores = aTabla0[1];
                }
                catch (Exception ex)
                {
                    sErrores = Errores.mostrarError("Error al obtener los datos", ex);
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
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        try
        {
            switch (aArgs[0])
            {
                case ("eliminar"):
                    sResultado += SUPER.Capa_Negocio.GrupoProf.Eliminar(aArgs[1]);
                    break;
                case ("getDatos"):
                    sResultado += SUPER.Capa_Negocio.GrupoProf.Catalogo();
                    string uidEquipo = aArgs[1];
                    if (uidEquipo != "-1")
                    {
                        string sAux=SUPER.Capa_Negocio.GrupoProf.Integrantes(uidEquipo);
                        string[] aAux = Regex.Split(sAux, "@#@");
                        sResultado += "@#@" + aAux[1] + "@#@" + uidEquipo;
                    }
                    else
                        sResultado += "@#@" ;
                    break;
                case ("integrantes"):
                    sResultado += SUPER.Capa_Negocio.GrupoProf.Integrantes(aArgs[1]);
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("eliminar"):
                    sResultado += "Error@#@" + ex.Message;
                    break;
                case ("getDatos"):
                    sResultado += "Error@#@" + ex.Message;
                    break;
                case ("integrantes"):
                    sResultado += "Error@#@" + ex.Message;
                    break;
            }
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    //private string ObtenerGP(string sOrden, string sAscDesc, string sCodUne)
    //{// Devuelve el código HTML del catalogo de grupos funcionales de la UNE que se pasa por parámetro
    //    StringBuilder strBuilder = new StringBuilder();
    //    string sDesc, sCod, sResul;
    //    try
    //    {
    //        //strBuilder.Append("<div style='background-image:url(../../../../../Images/imgFT16.gif); width:0%; height:0%'>");
    //        strBuilder.Append("<table id='tblDatos' class='texto MA' style='width: 430px;'>");
    //        strBuilder.Append("<colgroup><col style='width:430px;'/></colgroup>");
    //        //strBuilder.Append("<tbody>");
    //        if (sCodUne != "")
    //        {
    //            SqlDataReader dr = GrupoFun.CatalogoGrupos(int.Parse(sOrden), int.Parse(sAscDesc), int.Parse(sCodUne));
    //            while (dr.Read())
    //            {
    //                sCod = dr["idGrupro"].ToString();
    //                sDesc = dr["Nombre"].ToString();

    //                strBuilder.Append("<tr id='" + sCod + "' style='height:20px'");
    //                //strBuilder.Append(" onclick='mm(event);mostrarIntegrantes(this.id);' ondblclick='mostrarDetalle(this.id)'>");
    //                strBuilder.Append(" onclick='mm(event);mostrarIntegrantes(this.id);' ondblclick='mostrarDetalleAux(this)'>");
    //                strBuilder.Append("<td>" + sDesc + "</td></tr>");
    //            }
    //            dr.Close();
    //            dr.Dispose();
    //        }
    //        //strBuilder.Append("</tbody>");
    //        strBuilder.Append("</table>");//</div>

    //        sResul = strBuilder.ToString();
    //        this.strTablaHtmlGP = sResul;
    //        return "OK@#@" + sResul;
    //    }
    //    catch (Exception ex)
    //    {
    //        sErrores = Errores.mostrarError("Error al obtener los Grupos Funcionales", ex);
    //        return "error@#@Error al obtener los Grupos Funcionales " + ex.Message;
    //    }
    //}
    //private string EliminarGP(string strGrupo)
    //{
    //    string sResul = "OK@#@";
    //    #region abrir conexión y transacción
    //    try
    //    {
    //        oConn = Conexion.Abrir();
    //        tr = Conexion.AbrirTransaccion(oConn);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
    //        sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
    //        return sResul;
    //    }
    //    #endregion

    //    try
    //    {
    //        string[] aGF = Regex.Split(strGrupo, "##");
    //        foreach (string oGF in aGF)
    //        {
    //            if (oGF !="")
    //                GrupoFun.BorrarGrupo(tr, int.Parse(oGF));
    //        }
    //        Conexion.CommitTransaccion(tr);
    //        sResul = "OK@#@";
    //    }
    //    catch (Exception ex)
    //    {
    //        Conexion.CerrarTransaccion(tr);
    //        sErrores = Errores.mostrarError("Error al eliminar el grupo funcional " + strGrupo, ex);
    //    }
    //    finally
    //    {
    //        Conexion.Cerrar(oConn);
    //    }
    //    return sResul;
    //}  
}