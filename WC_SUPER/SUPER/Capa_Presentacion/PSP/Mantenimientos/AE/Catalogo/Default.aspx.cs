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
    public string strTablaHtml, strTablaHtmlVAE, strArrayVAE;
    public string sErrores = "";
    //public string sNodo = "";
    protected string sAmbito;
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
            Master.TituloPagina = "Catálogo de criterios estadísticos técnicos";
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Capa_Presentacion/PSP/Mantenimientos/AE/Catalogo/Functions/vae.js");
            Master.bFuncionesLocales = true;
            
            if (!Page.IsPostBack)
            {
                try
                {
                    strArrayVAE = "";
                    sAmbito ="'" + Request.QueryString["A"].ToString() + "'";
                    if (Request.QueryString["A"].ToString()=="E")
                        Master.TituloPagina = "Catálogo de criterios estadísticos económicos departamentales";

                    strTablaHtml = "<table id='tblDatos'><tbody id='tbodyDatos'></tbody></table>";
                    //Cargo la denominacion del label Nodo
                    //sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    //if (sNodo.Trim() != "")
                    //{
                        this.lblNodo.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                        //this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                        this.gomaNodo.Attributes.Add("title", "Borra el " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    //}
                        if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    {
                        cboCR.Visible = false;
                        hdnIdNodo.Visible = true;
                        txtDesNodo.Visible = true;
                        //gomaNodo.Visible = true;
                    }
                    else
                    {
                        cboCR.Visible = true;
                        hdnIdNodo.Visible = false;
                        txtDesNodo.Visible = false;
                        //gomaNodo.Visible = false;
                        cargarNodos(Request.QueryString["A"].ToString());
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
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("buscar"):
                sResultado += ObtenerAEs(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("eliminar"):
                sResultado += EliminarAE(aArgs[1]);
                break;
            //case ("getVAEs"):
            //    sResultado += "OK@#@"+ObtenerValoresAtributosEstadisticos(aArgs[1], aArgs[2]);
            //    break;
            case ("preEliminarAE"):
                sResultado += preEliminarAE(aArgs[1]);
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

    private string ObtenerAEs(string sNodo, string sAmbito, string sCliente)
    {
        //string sResul = "";
        int? idCliente = null;
        StringBuilder sb = new StringBuilder();
        string sVaes = "";
        //sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 900px; BORDER-COLLAPSE: collapse; ' cellSpacing='0' border='0' mantenimiento='1'>");
        sb.Append("<table id='tblDatos' class='texto' style='width: 900px;' mantenimiento='1'>");
        if (sNodo != "")
        {
            if (sCliente != "") idCliente = int.Parse(sCliente);
            SqlDataReader dr = AE.CatalogoByUne(int.Parse(sNodo), sAmbito, idCliente);
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:20px;' /><col style='width:300px;' /><col style='width:75px' /><col style='width:75px' /><col style='width:420px' /></colgroup>");
            sb.Append("<tbody id='tbodyDatos'>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t341_idae"].ToString() + "' bd='' orden='" + dr["t341_orden"].ToString() + "' cli='"+ dr["cod_cliente"].ToString() + "'");
                sb.Append(" style='height:20px' onclick='ms(this);refrescarVAEs(this.id)'>");//ondblclick='mostrarDetalle(this.id)'
                sb.Append("<td><img src='../../../../../images/imgFN.gif'></td>");
                sb.Append("<td style='text-align:center;'><img src='../../../../../images/imgMoveRow.gif' style='cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>");
                //sb.Append("<td>" + dr["t341_nombre"].ToString() + "</td>");
                sb.Append("<td style='padding-left:5px;'><input type='text' id='txtDesc' class='txtL' style='width:290px' value='" + dr["t341_nombre"].ToString() + "' maxlength='30' onKeyUp='fm(event)'></td>");
                //sb.Append("<td>" + dr["t341_orden"].ToString() + "</td>");
                //if ((bool)dr["t341_estado"])
                //    sb.Append("<td><img src='../../../../../images/imgOk.gif'></td>");
                //else
                //    sb.Append("<td><img src='../../../../../images/imgSeparador.gif'></td>");
                sb.Append("<td style='text-align:center;'><input type='checkbox' style='width:15px;' name='chkEst' id='chkEst' class='check' onclick='fm(event)' ");
                if ((bool)dr["t341_estado"]) sb.Append("checked=true");
                //if (dr["t341_ambito"].ToString() == "T")
                //    sb.Append("<td>Tarea</td>");
                //else
                //    sb.Append("<td>P. Técnico</td>");

                //if ((bool)dr["t341_obligatorio"])
                //    sb.Append("<td><img src='../../../../../images/imgOk.gif'></td>");
                //else
                //    sb.Append("<td><img src='../../../../../images/imgSeparador.gif'></td>");
                sb.Append("><td style='text-align:center;'><input type='checkbox' style='width:15px' name='chkObli' id='chkObli' class='check' onclick='fm(event)' ");
                if ((bool)dr["t341_obligatorio"]) sb.Append("checked=true");
                sb.Append("></td>");

                //sb.Append("<td><img src='../../../../../Images/imgCliente16.gif' style='cursor:pointer;' onclick='getClienteAE(this.parentNode.parentNode);fm(this);' title='Selección de cliente' /></td>");
                sb.Append("<td><nobr class='NBR W345'>" + dr["nom_cliente"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            //Cargo un array con todos los valores de todos los Atributos Estadisticos del nodo-ambito
            //Ese array es necesario para poder realizar la grabación de los valores de los diferentes criterios estadísticos del catálogo
            sVaes=ObtenerValoresAtributosEstadisticos(sNodo, sAmbito);
        }
        sb.Append("</tbody>");
        sb.Append("</table>");
        strTablaHtml = sb.ToString();
        return "OK@#@" + sb.ToString() + "@#@" + sVaes;
    }

    protected string EliminarAE(string strIDAE)
    {
        string sResul = "";
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
            string[] aAE = Regex.Split(strIDAE, "##");
            foreach (string oAE in aAE)
            {
                AE.Delete(tr, int.Parse(oAE));
            }
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al eliminar los criterios estadísticos", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }
    protected string preEliminarAE(string strIDAE)
    {
        string sResul = "";
        int iNumElems = 0;
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
            string[] aAE = Regex.Split(strIDAE, "##");
            foreach (string oAE in aAE)
            {
                //AE.Delete(tr, int.Parse(oAE));
                iNumElems = AE.numAEusados(int.Parse(oAE));
            }
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + iNumElems.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al revisar los criterios estadísticos", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }
    private void cargarNodos(string sAmbito)
    {
        try
        {
            //this.lblNodo.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
            ListItem oLI = null;
            bool bPrimer = true;

            SqlDataReader dr = NODO.CatalogoAdministrables((int)Session["UsuarioActual"], true);
            while (dr.Read())
            {
                oLI = new ListItem(dr["t303_denominacion"].ToString(), dr["t303_idnodo"].ToString());
                if (bPrimer)
                {
                    oLI.Selected = true;
                    ObtenerAEs(dr["t303_idnodo"].ToString(), sAmbito, "");
                    this.hdnVAE.Value = ObtenerValoresAtributosEstadisticos(dr["t303_idnodo"].ToString(), sAmbito);
                    bPrimer = false;
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
    protected string Grabar(string strDatos, string strDatosVAE, string sNodo, string sAmbito)//string sIdAE, 
    {
        string sResul = "", sNuevosAEs="";
        //int ID = int.Parse(sIdAE);
        int idAE;
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
            #region AE
            bool bEstado = false;
            bool bObligatorio = false;
            int? nCliente = null, idNuevoAE;

            string[] aAE = Regex.Split(strDatos, "///");
            foreach (string oAE in aAE)
            {
                if (oAE == "") continue;
                string[] aValores = Regex.Split(oAE, "##");
                //0. Opcion BD. "I", "U", "D"
                //1. ID AE
                //2. Denominación
                //3. Estado
                //4. Obligatorio
                //5. Nodo
                //6. Cliente
                //7. Orden
                //8. Ambito (E-> económico, T-> técnico)
                bEstado = false;
                if (aValores[3] == "1") bEstado = true;
                bObligatorio = false;
                if (aValores[4] == "1") bObligatorio = true;
                nCliente = null;
                if (aValores[6] != "") nCliente = int.Parse(aValores[6]);

                switch (aValores[0])
                {
                    case "D":
                        AE.Delete(tr, int.Parse(aValores[1]));
                        break;
                    case "I":
                        idNuevoAE = AE.Insert(tr, Utilidades.unescape(aValores[2]), bEstado, int.Parse(aValores[7]), bObligatorio, 
                                              int.Parse(aValores[5]), nCliente, aValores[8]);
                        sNuevosAEs += aValores[1] + "##" + idNuevoAE.ToString() + "@@";
                        break;
                    case "U":
                        //AE.UpdateOrden(tr, int.Parse(aValores[1]), int.Parse(aValores[2]));
                        AE.Update(tr, int.Parse(aValores[1]), Utilidades.unescape(aValores[2]), bEstado, int.Parse(aValores[7]), bObligatorio,
                                  int.Parse(aValores[5]), nCliente);
                        break;
                }
            }
            #endregion

            #region VAE

            string[] aVAE = Regex.Split(strDatosVAE, "///");

            foreach (string oVAE in aVAE)
            {
                if (oVAE == "") break;
                string[] aKeysAE = Regex.Split(sNuevosAEs, "@@");
                string[] aValoresVAE = Regex.Split(oVAE, "##");
                ///aValoresVAE[0] = opcionBD;
                ///aValoresVAE[1] = idAE;
                ///aValoresVAE[2] = idVAE;
                ///aValoresVAE[3] = Valor;
                ///aValoresVAE[4] = Orden;
                ///aValoresVAE[5] = Activo;
                idAE = int.Parse(aValoresVAE[1]);
                if (idAE < 0)
                    idAE = flBuscarKeyAE(aValoresVAE[1], aKeysAE);

                bool bEstadoVAE = false;
                if (aValoresVAE[5] == "1") bEstadoVAE = true;

                switch (aValoresVAE[0])
                {
                    case "I":
                        //VAE.Insert(tr, Utilidades.unescape(aValoresVAE[3]), bEstadoVAE, int.Parse(aValoresVAE[1]), byte.Parse(aValoresVAE[4]));
                        VAE.Insert(tr, Utilidades.unescape(aValoresVAE[3]), bEstadoVAE, idAE, int.Parse(aValoresVAE[4]));
                        break;
                    case "U":
                        //VAE.Update(tr, int.Parse(aValoresVAE[2]), Utilidades.unescape(aValoresVAE[3]), bEstadoVAE, int.Parse(aValoresVAE[1]), byte.Parse(aValoresVAE[4]));
                        VAE.Update(tr, int.Parse(aValoresVAE[2]), Utilidades.unescape(aValoresVAE[3]), bEstadoVAE, idAE, int.Parse(aValoresVAE[4]));
                        break;
                    case "D":
                        VAE.Delete(tr, int.Parse(aValoresVAE[2]));
                        break;
                }
            }
            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + sNuevosAEs + "@#@" + ObtenerValoresAtributosEstadisticos(sNodo, sAmbito);
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los criterios estadísticos.", ex);// +"@#@" + sDesc;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

    private string ObtenerValoresAtributosEstadisticos(string sNodo, string sAmbito)
    {
        StringBuilder sbuilder = new StringBuilder();
        byte bEstado;
        sbuilder.Append(" aVAES = new Array();\n");
        if (sNodo != "")
        {
            SqlDataReader dr = VAE.CatalogoByUne(int.Parse(sNodo), sAmbito, null);
            int i = 0;
            while (dr.Read())
            {
                if ((bool)dr["t340_estado"]) bEstado = 1;
                else bEstado = 0;
                sbuilder.Append("\taVAES[" + i.ToString() + "] = {bd:\"\", "+
                                "idAE:\"" + dr["t341_idae"].ToString() + "\"," +
                                "idVAE:\"" + dr["t340_idvae"].ToString() + "\"," +
                                "nombre:\"" + Utilidades.escape(dr["t340_valor"].ToString()) + "\"," +
                                "estado:\"" + bEstado.ToString() + "\"," +
                                "orden:\"" + dr["t340_orden"].ToString() + "\"};\n");
                i++;
            }
            dr.Close();
            dr.Dispose();
        }
        strArrayVAE = sbuilder.ToString();
        return strArrayVAE;
    }
    private int flBuscarKeyAE(string sIdAE, string[] aKeysAE)
    {
        int iResul= int.Parse(sIdAE);
        try
        {
            foreach (string oKey in aKeysAE)
            {
                if (oKey=="") continue;
                string[] aElem = Regex.Split(oKey, "##");
                if (aElem[0] == sIdAE)
                {
                    iResul = int.Parse(aElem[1]);
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            sErrores = "Error@#@" + Errores.mostrarError("Error al grabar los criterios estadísticos.", ex);// +"@#@" + sDesc;
        }

        return iResul;
    }
}