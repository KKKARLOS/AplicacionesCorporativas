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


public partial class Capa_Presentacion_Mantenimientos_Funciones_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtml, sErrores;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.

            Master.nBotonera = 9;// 52;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            Master.TituloPagina = "Relación de funciones";
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                    strTablaHtml = "<table id='tblDatos'><tbody id='tbodyDatos'></tbody></table>";
                    //Cargo la denominacion del label Nodo
                    this.lblNodo.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    {
                        cboCR.Visible = false;
                        //hdnIdNodo.Visible = true;
                        txtDesNodo.Visible = true;
                    }
                    else
                    {
                        cboCR.Visible = true;
                        //hdnIdNodo.Visible = false;
                        txtDesNodo.Visible = false;
                        cargarNodos("");
                    }
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
            case ("buscar"):
                sResultado += ObtenerFunciones(aArgs[1]);
                break;
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

    private string ObtenerFunciones(string sCR)
    {
        StringBuilder strBuilder = new StringBuilder();
        int i = 0;

        strBuilder.Append("<table id='tblDatos' class='texto MANO' style='width: 400px;' mantenimiento='1'>");
        strBuilder.Append("<colgroup><col style='width:15px;' /><col style='width:385px;' /></colgroup>");
        strBuilder.Append("<tbody>");
        SqlDataReader dr = FUNCIONES.Catalogo(null, "", short.Parse(sCR), 2, 0);

        while (dr.Read())
        {
            strBuilder.Append("<tr id='" + dr["t356_idfuncion"].ToString() + "' style='height:20px' bd='' onclick='mm(event)' onKeyUp=\"mfa(this,'U')\">");
            strBuilder.Append("<td><img src='../../../../images/imgFN.gif'></td><td><input type='text' id='txtFun");
            strBuilder.Append(i.ToString() + "' class='txtL' style='width:380px' maxlength='40' value='");
            strBuilder.Append(dr["t356_desfuncion"].ToString() + "'></td></tr>");
            i++;
        }
        dr.Close();
        dr.Dispose();
        strBuilder.Append("</tbody>");
        strBuilder.Append("</table>");
        strTablaHtml = strBuilder.ToString();
        return "OK@#@" + strBuilder.ToString();
    }

    protected string Grabar(string sCR, string strFunciones)
    {
        string sResul = "";

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
            string[] aFun = Regex.Split(strFunciones, "///");
            foreach (string oFun in aFun)
            {
                string[] aValores = Regex.Split(oFun, "##");
                switch (aValores[0])
                {
                    case "I":
                        FUNCIONES.Insert(tr, Utilidades.unescape(aValores[2]), short.Parse(sCR));
                        break;
                    case "U":
                        FUNCIONES.Update(tr, int.Parse(aValores[1]), Utilidades.unescape(aValores[2]), short.Parse(sCR));
                        break;
                    case "D":
                        FUNCIONES.Delete(tr, int.Parse(aValores[1]));
                        break;
                }
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al actualizar las funciones", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }
    private void cargarNodos(string sNodo)
    {
        try
        {
            bool bSeleccionado = false;
            //Cargar el combo de nodos accesibles
            ListItem oLI = null;
            SqlDataReader dr = NODO.CatalogoAdministrables((int)Session["UsuarioActual"], true);
            while (dr.Read())
            {
                oLI = new ListItem(dr["t303_denominacion"].ToString(), dr["t303_idnodo"].ToString());
                if (!bSeleccionado)
                {
                    if (sNodo == "")
                    {
                        oLI.Selected = true;
                        bSeleccionado = true;
                        ObtenerFunciones(dr["t303_idnodo"].ToString());
                    }
                    else
                    {
                        if (sNodo == dr["t303_idnodo"].ToString())
                        {
                            oLI.Selected = true;
                            bSeleccionado = true;
                            ObtenerFunciones(sNodo);
                        }
                    }
                }
                cboCR.Items.Add(oLI);
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al cargar los nodos", ex);
        }
    }
}