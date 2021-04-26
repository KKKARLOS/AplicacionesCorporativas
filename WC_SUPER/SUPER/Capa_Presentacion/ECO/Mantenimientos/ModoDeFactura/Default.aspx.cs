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
using System.Text;
using System.Text.RegularExpressions;
using EO.Web; 
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtml, strTablaHtmlModoFac; 
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
        if (!Page.IsCallback)
        {
            Master.sbotonesOpcionOn = "4";
            Master.sbotonesOpcionOff = "4";

            Master.TituloPagina = "Mantenimiento del modo de facturación a nivel de " + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2);
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FuncionesJavaScript.Add("Capa_Presentacion/ECO/Mantenimientos/ModoDeFactura/Functions/ModoFac.js");
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                    ObtenerSN2();
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
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

    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                try
                {
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
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
    private void ObtenerSN2()
    {        
        SqlDataReader dr;
        if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            dr = SUPERNODO2.CatalogoAdm();
        else
            dr = SUPERNODO2.CatalogoSuperNodo2Usuario((int)Session["UsuarioActual"]);

        StringBuilder sb = new StringBuilder();
        sb.Append("<table id='tblDatos' class='texto MANO' style='WIDTH: 400px;' mantenimiento='1'>");
        //sb.Append("<colgroup><col style='width:410px;' /></colgroup>");
        sb.Append("<tbody id='tbodyDatos'>");
        while (dr.Read())
        {
            sb.Append("<tr id='" + dr["t392_idsupernodo2"].ToString() + "' style='height:20px' onclick='ms(this);refrescarModoFac(this.id);' onmouseover='TTip(event);'>");
            sb.Append("<td style='padding-left:5px;'>" + dr["t392_denominacion"].ToString() + "</td>");
            sb.Append("</tr>");
        }
        dr.Close();
        dr.Dispose();
        sModoFac.Value = ObtenerModosDeFacturacionSN2();
        sb.Append("</tbody>");
        sb.Append("</table>");
        strTablaHtml = sb.ToString();
    }
    protected string Grabar(string strDatosModoFac)
    {
        string sResul = "", sNuevosModoFacs = "";
        //bool bEstado;
        int idSN2;//, idModoFac;
        #region conexion
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
        #endregion
        try
        {
            #region ModoFac
            int idNuevoModoFac;
            string[] aModoFac = Regex.Split(strDatosModoFac, "///");

            foreach (string oModoFac in aModoFac)
            {
                if (oModoFac == "") break;
                string[] aValoresModoFac = Regex.Split(oModoFac, "##");
                ///aValoresModoFac[0] = opcionBD;
                ///aValoresModoFac[1] = idSN2;
                ///aValoresModoFac[2] = idModoFac;
                ///aValoresModoFac[3] = Valor;
                ///aValoresModoFac[4] = Activo;
                
                idSN2 = int.Parse(aValoresModoFac[1]);

                bool bEstadoModoFac = false;
                if (aValoresModoFac[4] == "1") bEstadoModoFac = true;

                switch (aValoresModoFac[0])
                {
                    case "I":
                        
                        idNuevoModoFac = MODOFACTSN2.Insert(tr, Utilidades.unescape(aValoresModoFac[3]), bEstadoModoFac, idSN2);
                        sNuevosModoFacs += aValoresModoFac[2] + "##" + idNuevoModoFac.ToString() + "@@";
                        break;
                    case "U":
                        MODOFACTSN2.Update(tr, int.Parse(aValoresModoFac[2]), Utilidades.unescape(aValoresModoFac[3]), bEstadoModoFac, idSN2);
                        break;
                    case "D":
                        MODOFACTSN2.Delete(tr, int.Parse(aValoresModoFac[2]));
                        break;
                }
            }
            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + sNuevosModoFacs;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los modos de facturación.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

    private string ObtenerModosDeFacturacionSN2()
    {
        StringBuilder sbuilder = new StringBuilder();
        byte bEstado;
        sbuilder.Append(" aModoFac = new Array();\n");
        SqlDataReader dr = MODOFACTSN2.Catalogo(null, "", null, null, 2, 0);
        int i = 0;
        while (dr.Read())
        {
            if ((bool)dr["t324_activo"]) bEstado = 1;
            else bEstado = 0;
            sbuilder.Append("\taModoFac[" + i.ToString() + "] = {bd:\"\", " +
                            "idSN2:\"" + dr["t392_idsupernodo2"].ToString() + "\"," +
                            "idModoFac:\"" + dr["t324_idmodofact"].ToString() + "\"," +
                            "nombre:\"" + Utilidades.escape(dr["t324_denominacion"].ToString()) + "\"," +
                            "estado:\"" + bEstado.ToString() + "\"};\n");
            i++;
        }
        dr.Close();
        dr.Dispose();
        return sbuilder.ToString();
    }
}