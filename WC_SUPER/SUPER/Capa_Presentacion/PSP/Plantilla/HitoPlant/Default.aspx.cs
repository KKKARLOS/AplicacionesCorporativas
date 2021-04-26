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


using System.Text.RegularExpressions;
using System.Text;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores, strTablaHitos, strTablaTareas;
    public int nIdPlant, nIdHito;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
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

            sErrores = "";
            strTablaHitos = "";
            strTablaTareas = "";

            nIdHito = int.Parse(Request.QueryString["nIdHito"].ToString());
            nIdPlant = int.Parse(quitaPuntos(Request.QueryString["nIdPlant"].ToString()));
            try
            {
                ObtenerDatosHito();
                //if (sTipoHito != "HF")
                ObtenerTareas(nIdHito);
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos del hito", ex);
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

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2]);
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
    private void ObtenerDatosHito()
    {
        try
        {
            HITOE_PLANT o = HITOE_PLANT.Select(null, nIdHito);

            txtIdHito.Text = o.t369_idhito.ToString("#,###");
            txtDesHito.Text = o.t369_deshito;
            txtDescripcion.Text = o.t369_deshitolong;
            hdnOrden.Text = o.t369_orden.ToString();
            if (o.t369_alerta) chkAlerta.Checked = true;
            else chkAlerta.Checked = false;
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos del hito", ex);
        }
    }
    protected string ObtenerTareas(int iCodHito)
    {
    //Relacion de tareas asignadas al hito
    string sResul = "", sCodTarea;
    StringBuilder sb = new StringBuilder();
    try
    {
        sb.Append("<table id='tblTareas' class='texto MANO' style='WIDTH: 800px;'>");
        //............................ idTarea........... Desc Tarea
        sb.Append("<colgroup><col style='width:40px' /><col style='width:760px' /></colgroup>");
        sb.Append("<tbody>");
        SqlDataReader dr = HITOE_PLANT_TAREA.SelectByt369_idhito(null, iCodHito);

        int i = 0;
        while (dr.Read())
        {
            sCodTarea = dr["codTarea"].ToString();

            sb.Append("<tr style='height:16px' id='" + sCodTarea + "' est='N' onclick='mm(event)'>");
            sb.Append("<td>" + sCodTarea + "</td>");
            sb.Append("<td><div class='NBR W750'>" + dr["desTarea"].ToString() + "</div></td></tr>");
            i++;
        }
        dr.Close();
        dr.Dispose();
        sb.Append("</tbody>");
        sb.Append("</table>");
        strTablaTareas = sb.ToString();
        sResul = "OK@#@" + strTablaTareas;
    }
    catch (Exception ex)
    {
        sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de tareas.", ex);
    }

        return sResul;
    }
    protected string Grabar(string strDatosHito, string sTareas)
    {
        string sResul = "",sDesHito,sDesHitoLong,sAlerta,sTipoLinea,sCad,sCodTarea;
        string[] aDatosHito;
        int  iCodHito;
        short iOrden;
        bool bAlerta = false;
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        try
        {
            aDatosHito = Regex.Split(strDatosHito, "##");
            iCodHito = int.Parse(aDatosHito[0]);
            sDesHito = Utilidades.unescape(aDatosHito[1]);
            sDesHitoLong = Utilidades.unescape(aDatosHito[2]);
            sAlerta = aDatosHito[3];
            if (sAlerta == "1") bAlerta = true;
            iOrden = short.Parse(aDatosHito[4]);
            HITOE_PLANT.Update(tr, iCodHito, sDesHito, sDesHitoLong, bAlerta, iOrden);

            //Grabamos las tareas asociadas al hito
            if (sTareas != "")
            {
                string[] aTareas = Regex.Split(sTareas, @"##");

                for (int i = 0; i < aTareas.Length - 1; i++)
                {
                    sCad = aTareas[i];
                    sTipoLinea = sCad.Substring(0, 1);
                    sCodTarea = sCad.Substring(1);
                    if (sTipoLinea == "D")
                    {//Borrar hito-tarea
                        HITOE_PLANT_TAREA.Delete(tr, iCodHito, int.Parse(sCodTarea));
                    }
                    else
                    {
                        if (sTipoLinea == "I")
                        {//Insertar hito-tarea
                            HITOE_PLANT_TAREA.Insert(tr, iCodHito, int.Parse(sCodTarea));
                        }
                    }
                }
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + iCodHito.ToString() + "@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del hito", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string quitaPuntos(string sCadena)
    {
        //Finalidad:Elimina los puntos de una cadena
        string sRes;

        sRes = sCadena;
        try
        {
            if (sCadena == "") return "";
            sRes = sRes.Replace(".", "");
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al quitar puntos de la cadena" + sCadena, ex);
        }
        return sRes;
    }
}
