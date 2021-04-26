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


public partial class Administradores : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaOrgVtasSAP;
    public string strTablaOrgVtasSuper;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.sbotonesOpcionOn = "4";
            Master.sbotonesOpcionOff = "4";

            Master.TituloPagina = "Mantenimiento de organizaciones de venta";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");

            try
            {
                string strTabla0 = obtenerOrgVtasSAP();
                string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                if (aTabla0[0] != "Error") strTablaOrgVtasSAP = aTabla0[1];
                else Master.sErrores = aTabla0[1];

                strTabla0 = obtenerOrgVtasSUPER();
                aTabla0 = Regex.Split(strTabla0, "@#@");
                if (aTabla0[0] != "Error") strTablaOrgVtasSuper = aTabla0[1];
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
    private string obtenerOrgVtasSAP()
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = ORGVENTASSAP.CatalogoSAP();
            sb.Append("<TABLE id='tblDatos' class='texto MAM' style='WIDTH: 450px;'>");
            sb.Append("<colgroup><col style='width:70px;' /><col style='width:380px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["codigo"].ToString() + "' style='height:20px;' onmousedown='DD(event);' onclick='mm(event)' ondblclick='addItem(this)'>");
                sb.Append("<td style=\"padding-left:5px;\">" + dr["codigo"].ToString() + "</td>");
                sb.Append("<td><div class='NBR' style='width:370px;'>" + dr["Denominacion"].ToString() + "</div></td>");
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
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de organizaciones de venta de SAP.", ex);
        }
    }
    private string obtenerOrgVtasSUPER()
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = ORGVENTASSAP.Catalogo();
            sb.Append("<TABLE id='tblDatos2' style='WIDTH: 450px;' class='texto MM'>");
            sb.Append("<colgroup><col style='width: 15px' /><col style='width: 70px' /><col style='width: 365px' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["codigo"].ToString() + "' bd='' style='height:20px;' onmousedown='DD(event);' onclick='mm(event)'>");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td>" + dr["codigo"].ToString() + "</td>");
                sb.Append("<td><div class='NBR' style='width:365px'>" + dr["Denominacion"].ToString() + "</div></td>");
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
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de organizaciones de venta de SAP.", ex);
        }
    }   
    private string Grabar(string strOrgVtas)
    {
        string sResul = "" , sElementosInsertados = "";

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
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
            #region Datos Organizaciones de Venta
            if (strOrgVtas != "") 
            {
                string[] aOrgVtas = Regex.Split(strOrgVtas, "///");
                foreach (string oOrgVtas in aOrgVtas)
                {
                    if (oOrgVtas == "") continue;
                    string[] aValores = Regex.Split(oOrgVtas, "##");
                    ///aValores[0] = bd
                    ///aValores[1] = t621_idovsap

                    switch (aValores[0])
                    {
                        case "I":
                            ORGVENTASSAP.Insert(tr, aValores[1]);
                            if (sElementosInsertados == "") sElementosInsertados = aValores[1];
                            else sElementosInsertados += "//" + aValores[1];
                            break;
                        case "D":
                            ORGVENTASSAP.Delete(tr, aValores[1]);
                            break;
                    }
                }
            }

            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + sElementosInsertados;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar las organizaciones de venta", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
