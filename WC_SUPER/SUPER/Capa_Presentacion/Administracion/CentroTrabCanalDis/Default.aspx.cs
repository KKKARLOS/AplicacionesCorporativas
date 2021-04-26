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

using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;


public partial class Administradores : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaCentrosTrabajo;

    public SqlConnection oConn;
    public SqlTransaction tr;

 	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.sbotonesOpcionOn = "4";
            Master.sbotonesOpcionOff = "4";

            Master.TituloPagina = "Mantenimiento de los canales de distribución de los centros de trabajo";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");

            try
            {
                string strTabla0 = obtenerCentrosTrabajo();
                string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                if (aTabla0[0] != "Error") strTablaCentrosTrabajo = aTabla0[1];
                else Master.sErrores = aTabla0[1];

            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
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
    private string obtenerCentrosTrabajo()
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            //Cargamos en una lista los canales de distribución

            List<CANALDIS> listaCanales = CANALDIS.ListaGlobal();
            SqlDataReader dr = CENTROTRAB.Catalogo();
            sb.Append("<TABLE id='tblDatos' style='WIDTH: 500px;' class='texto MANO'>");
            sb.Append("<colgroup><col style='width: 20px;' /><col style='width: 280px' /><col style='width: 200px;' /></colgroup>");
            sb.Append("<tbody>");
            string sCD = "";
            while (dr.Read())
            {
                sCD = "";
                sb.Append("<tr id='" + dr["codigo"].ToString() + "' bd='' ");
                sb.Append("style='height:20px;' onclick='ms(this);getCD(this);'>");
                sb.Append("<td style='padding-left:2px;'><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td><div class='NBR W300'>" + dr["Denominacion"].ToString() + "</div></td>");

                // Damos contenido al combo y seleccionamos la opción                
                sb.Append("<td>");
                sb.Append("<select class='combo' style='width:200px;display:none;' onchange='aG(this);'>");

                foreach (CANALDIS oCanal in listaCanales)
                {
                    sb.Append("<option value='" + oCanal.codigo + "' ");
                    if (dr["T009_CDSAP"].ToString() == oCanal.codigo)
                    {
                        sb.Append(" selected ");
                        sCD = oCanal.denominacion;
                    }
                    sb.Append(">" + oCanal.denominacion + "</option>");
                }

                sb.Append("</select><div class='NBR W190' style='padding-left:5px;'>" + sCD + "</div></td>");
                //
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de centros de trabajo/canales distribución.", ex);
        }
    }
    private string Grabar(string strCentroTrabCanalDis)
    {
        string sResul = "" ;

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion
        try
        {
            #region Datos Canales de distribución asignados
            if (strCentroTrabCanalDis != "")
            {
                string[] aCentroTrabCanalDis = Regex.Split(strCentroTrabCanalDis, "///");
                foreach (string oCentroTrabCanalDis in aCentroTrabCanalDis)
                {
                    if (oCentroTrabCanalDis == "") continue;
                    string[] aValores = Regex.Split(oCentroTrabCanalDis, "##");
                    ///aValores[0] = bd
                    ///aValores[1] = T009_IDCENTRAB
                    ///aValores[2] = T009_CDSAP
                    switch (aValores[0])
                    {
                        case "U":
                            CENTROTRAB.Update(tr, int.Parse(aValores[1]), aValores[2]);
                            break;
                    }
                }
            }

            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los canales de distribución asociados al centro de trabajo", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
