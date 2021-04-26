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


public partial class Capa_Presentacion_Mantenimientos_FuncionesTecnico_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtml, sErrores;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strArrayTecFun;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, �nicamente hay que indicar el n�mero de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 15;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Cat�logo de funciones y profesionales";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Capa_Presentacion/PSP/Mantenimientos/FuncionesTecnico/Functions/FuncionTecnico.js");
            if (!Page.IsPostBack)
            {
                try
                {
                    strTablaHtml = "<table id='tblFun'></table>";
                    //Cargo la denominacion del label Nodo
                    this.lblNodo.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    {
                        cboCR.Visible = false;
                        txtDesNodo.Visible = true;
                    }
                    else
                    {
                        cboCR.Visible = true;
                        txtDesNodo.Visible = false;
                        cargarNodos("");
                    }
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }
            }
            //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
            //   y la funci�n que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2� Se "registra" la funci�n que va a acceder al servidor.
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
        //1� Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += ObtenerFunciones(aArgs[1]) + "@#@" + ObtenerFuncionesTecnicos2(aArgs[1]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
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

    private string ObtenerFunciones(string sCR)
    {
        StringBuilder strBuilder = new StringBuilder();

        strBuilder.Append("<table id='tblFun' class='texto MANO' style='width: 275px;'>");
        strBuilder.Append("<colgroup><col style='width:275px;' /></colgroup>");
        strBuilder.Append("<tbody>");
        SqlDataReader dr = FUNCIONES.Catalogo(null, "", short.Parse(sCR), 2, 0);
        while (dr.Read())
        {
            strBuilder.Append("<tr id='" + dr["t356_idfuncion"].ToString() + "' style='height:16px' onclick=\"ms(this);mostrarTecnicos(this.id);\">");
            strBuilder.Append("<td style='padding-left:5px;'>" + dr["t356_desfuncion"].ToString() + "</td></tr>");
        }
        dr.Close();
        dr.Dispose();
        strBuilder.Append("</tbody>");
        strBuilder.Append("</table>");
        strTablaHtml = strBuilder.ToString();
        return "OK@#@" + strBuilder.ToString();
    }
    
    protected string ObtenerFuncionesTecnicos(string sCR)
    {
        string sResul = "";
        StringBuilder strBuilder = new StringBuilder();
        try
        {
            SqlDataReader dr = FUNCIONESRECURSO.Catalogo(null, null, "", short.Parse(sCR), 3, 0);
            while (dr.Read())
            {
                strBuilder.Append("insertarTecnicoEnArray(\"\",\"" + dr["t356_idfuncion"].ToString() + "\",\"" + dr["t314_idusuario"].ToString() + "\",\"" + dr["nombre"].ToString() + "\");\n");
            }
            dr.Close();
            dr.Dispose();
            strArrayTecFun = strBuilder.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener las funciones de los t�cnicos.", ex);
        }

        return sResul;
    }
    protected string ObtenerFuncionesTecnicos2(string sCR)
    {
        string sResul = "";
        StringBuilder strBuilder = new StringBuilder();
        try
        {
            SqlDataReader dr = FUNCIONESRECURSO.Catalogo(null, null, "", short.Parse(sCR), 3, 0);
            while (dr.Read())
            {
                strBuilder.Append(dr["t356_idfuncion"].ToString() + "##" + dr["t314_idusuario"].ToString() + "##" + dr["nombre"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();
            return strBuilder.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener las funciones de los t�cnicos(2).", ex);
        }

        return sResul;
    }

    protected string Grabar(string strFunciones)
    {
        string sResul = "";

        #region abrir conexion
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexi�n", ex);
            return sResul;
        }
        #endregion

        try
        {
            string[] aFunTec = Regex.Split(strFunciones, "///");
            foreach (string oFunTec in aFunTec)
            {
                string[] aValores = Regex.Split(oFunTec, "##");
                switch (aValores[0])
                {
                    case "I":
                        FUNCIONESRECURSO.Insert(tr, int.Parse(aValores[1]), int.Parse(aValores[2]));
                        break;
                    case "D":
                        FUNCIONESRECURSO.Delete(tr, int.Parse(aValores[1]), int.Parse(aValores[2]));
                        break;
                }
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar la funci�n del t�cnico", ex);
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
                        ObtenerFuncionesTecnicos(dr["t303_idnodo"].ToString());
                    }
                    else
                    {
                        if (sNodo == dr["t303_idnodo"].ToString())
                        {
                            oLI.Selected = true;
                            bSeleccionado = true;
                            ObtenerFunciones(sNodo);
                            ObtenerFuncionesTecnicos(sNodo);
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