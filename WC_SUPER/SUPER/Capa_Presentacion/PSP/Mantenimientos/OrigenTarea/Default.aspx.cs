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


public partial class Capa_Presentacion_Mantenimientos_OrigenTarea_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML, sErrores;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 49;// 15;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Relación de orígenes de tarea";
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                    //Cargo la denominacion del label Nodo
                    this.lblNodo.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    {
                        cboCR.Visible = false;
                        txtDesNodo.Visible = true;
                        strTablaHTML = "<table id='tblDatos'></table>";
                    }
                    else
                    {
                        cboCR.Visible = true;
                        txtDesNodo.Visible = false;
                        cargarNodos("");
                    }
                    //string[] aTabla = Regex.Split(ObtenerOrigenes(cboCR.SelectedValue), "@#@");
                    //if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                    //else sErrores += Errores.mostrarError(aTabla[1]);
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
                sResultado += Grabar(aArgs[1], aArgs[2]);
                break;
            case ("getNodo"):
                sResultado += ObtenerOrigenes(aArgs[1]);
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

    private string ObtenerOrigenes(string sCR)
    {
        try
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblDatos' class='texto' style='width: 900px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:15px;' /><col style='width:210px;' /><col style='width:75px;' /><col style='width:550px;' /><col style='width:50px;' /></colgroup>");

            SqlDataReader dr = TAREAORIGEN.SelectByt303_idnodo(null, short.Parse(sCR));
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T353_idorigen"].ToString() + "' bd='' onclick='mm(event)'>");
                sb.Append("<td><img src='../../../../images/imgFN.gif'></td>");
                sb.Append("<td style='padding-left:5px;'><input type='text' id='txtDesc' class='txtL' style='width:200px' value='" + dr["T353_desorigen"].ToString() + "' maxlength='25' onKeyUp='fm(event)'></td>");
                sb.Append("<td><input type='checkbox' style='width:15px;' name='chkNot' id='chkNot' class='check' onclick='fm(event)' ");
                if ((bool)dr["T353_notificable"]) sb.Append("checked=true");
                sb.Append("></td><td><input type='text' id='txtMail' class='txtL' style='width:540px' value='" + dr["T353_email"].ToString() + "' maxlength='250' onKeyUp='fm(event)'></td>");
                sb.Append("<td><input type='checkbox' style='width:15px' name='chkEst' id='chkEst' class='check' onclick='fm(event)' ");
                if ((bool)dr["T353_estado"]) sb.Append("checked=true");
                sb.Append("></td></tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            strTablaHTML = sb.ToString();
            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al ordenar el catálogo", ex);
        }
    }

    protected string Grabar(string sCR, string strFunciones)
    {
        string sResul = "",sDesc="";

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
                //0. Opcion BD. "I", "U", "D"
                //1. ID Origen
                //2. Descripcion
                //3. Notificable
                //4. E-Mail
                sDesc = Utilidades.unescape(aValores[2]);
                bool bNotificable = false;
                if (aValores[3] == "1") bNotificable = true;
                bool bEstado = false;
                if (aValores[5] == "1") bEstado = true;
                switch (aValores[0])
                {
                    case "I":
                        TAREAORIGEN.Insert(tr, Utilidades.unescape(aValores[2]), Utilidades.unescape(aValores[4]), short.Parse(sCR), 
                                            bNotificable, bEstado);
                        break;
                    case "U":
                        TAREAORIGEN.Update(tr, short.Parse(aValores[1]), Utilidades.unescape(aValores[2]), Utilidades.unescape(aValores[4]), 
                                            short.Parse(sCR), bNotificable, bEstado);
                        break;
                    case "D":
                        TAREAORIGEN.Delete(tr, short.Parse(aValores[1]));
                        break;
                }
            }
            Conexion.CommitTransaccion(tr);

            string[] aTabla = Regex.Split(ObtenerOrigenes(sCR), "@#@");
            sResul = "OK@#@" + aTabla[1];
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al actualizar los orígenes.", ex) + "@#@" + sDesc;
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
                        ObtenerOrigenes(dr["t303_idnodo"].ToString());
                    }
                    else
                    {
                        if (sNodo == dr["t303_idnodo"].ToString())
                        {
                            oLI.Selected = true;
                            bSeleccionado = true;
                            ObtenerOrigenes(sNodo);
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