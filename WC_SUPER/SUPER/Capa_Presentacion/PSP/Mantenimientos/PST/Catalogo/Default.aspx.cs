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

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
//using System.Xml;
using System.IO;


public partial class Capa_Presentacion_Mantenimientos_PST_Catalogo_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtml, sErrores;
    public SqlConnection oConn;
    public SqlTransaction tr;
    //private XmlDocument docxml = new XmlDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.

            Master.nBotonera = 49;
            //Master.Botonera.ButtonClick += new System.EventHandler(this.Botonera_Click);

            Master.TituloPagina = "Catálogo de órdenes de trabajo codificadas";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
            if (!Page.IsPostBack)
            {
                try
                {
                    strTablaHtml = "<table id='tblDatos'><tbody id='tbodyDatos'></tbody></table>";
                    this.lblNodo.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    {
                        cboCR.Visible = false;
                        txtDesNodo.Visible = true;
                        if (Request.QueryString["nCR"] != null)
                        {
                            string sIdNodo = Request.QueryString["nCR"].ToString();
                            NODO miNodo = NODO.Select(null, int.Parse(sIdNodo));
                            this.hdnIdNodo.Text = sIdNodo;
                            this.txtDesNodo.Text = miNodo.t303_denominacion;
                            ObtenerPSTs("A", sIdNodo);
                        }
                    }
                    else
                    {
                        cboCR.Visible = true;
                        txtDesNodo.Visible = false;
                        string sNodo = "";
                        if (Request.QueryString["nCR"] != null) sNodo = Request.QueryString["nCR"].ToString();
                        cargarNodos(sNodo);
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

    // private void cargarFicheroXml2()
    // {
    //     string strFileNameOnServer = @"\PST.xml";
    //     string strBaseLocation = Server.MapPath(".");
    //     try
    //     {
    //         fileXML.PostedFile.SaveAs(strBaseLocation + strFileNameOnServer);
    //     }
    //     catch (Exception ex)
    //     {
    //         this.hdnMensajeError.Text = Errores.mostrarError("Error al guardar el fichero XML", ex);
    //     }
    //     try
    //     {
    //         docxml.Load(Server.MapPath(".") + @"\PST.xml");            

    //         XmlNode nodoraiz = docxml.DocumentElement;
    //         if (nodoraiz.HasChildNodes)
    //         {
    //             //Recorrer los nodos y grabar
    //             for (int i = 0; i < nodoraiz.ChildNodes.Count; i++)
    //             {
    //                 //selección del nodo deseado, no es necesario
    //                 XmlNode nodoREGISTRO = nodoraiz.ChildNodes[i];
    //                 XmlNode nodoCODIGO = nodoREGISTRO.ChildNodes[0];
    //                 XmlNode nodoDESCRIPCION = nodoREGISTRO.ChildNodes[1];
    //                 PST.Insert(null, short.Parse(Session["NodoActivo"].ToString()), nodoCODIGO.InnerText, nodoDESCRIPCION.InnerText, true);
    //             }
    //         }
    //         File.Delete(Server.MapPath(".") + strFileNameOnServer);

    //         this.Controls.Add(LoadControl("~/Capa_Presentacion/UserControls/Mensaje.ascx"));
    //     }
    //     catch (Exception ex)
    //     {
    //         this.hdnMensajeError.Text = Errores.mostrarError("Error al grabar en tabla t346_PST", ex);
    //     }        
    //     ObtenerPSTs();
    //}
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
            //case ("eliminar"):
            //    sResultado += EliminarPST(aArgs[1]);
            //    break;
            case ("pst"):
                sResultado += ObtenerPSTs(aArgs[1], aArgs[2]);
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
    private string ObtenerPSTs(string sTipo, string sCR)
    {
        StringBuilder sb = new StringBuilder();
        bool? bEstado;
        string sEstado, sFecha;

        sb.Append("<table id='tblDatos' class='texto MANO' style='width:970px;' mantenimiento='1'>");
        sb.Append("<colgroup><col style='width:20px;' /><col style='width:110px;'><col style='width:245px'><col style='width:50px'><col style='width:80px'><col style='width:60px'><col style='width:80px;'><col style='width:40px;'><col style='width:285px'></colgroup>");
        sb.Append("<tbody>");
        //Si sTipo=T no restrinjo por estado de la PST, sino saco solo las activas
        if (sTipo == "T") bEstado = null;
        else bEstado = true;
        SqlDataReader dr = PST.Catalogo(null, int.Parse(sCR), "", "", bEstado, null, null, "", null, "", null);
        while (dr.Read())
        {
            sb.Append("<tr id='" + dr["t346_idpst"].ToString() + "' bd='' idOText='" + dr["idOTExterno"].ToString() + "' idOriExt='" + dr["idOrigenExterno"].ToString() + "'");

            sEstado = ((bool)dr["t346_estado"]) ? "1" : "0";
            sb.Append(" estado='" + sEstado + "'");
            sb.Append(" cli='" + dr["cod_cliente"].ToString() + "' style='height:20px'>");//onkeydown='activarGrabar()'

            sb.Append("<td></td>");

            sb.Append("<td style='padding-left:5px;'>" + dr["t346_codpst"].ToString() + "</td>");

            sb.Append("<td>" + dr["t346_despst"].ToString() + "</td>");

            sb.Append("<td>" + double.Parse(dr["t346_horas"].ToString()).ToString("#,##0.00") + "</td>");

            sb.Append("<td>" + double.Parse(dr["t346_presupuesto"].ToString()).ToString("#,##0.00") + "</td>");

            sb.Append("<td>" + dr["moneda"].ToString() + "</td>");

            if (dr["t346_fecharef"].ToString() == "") sFecha = "";
            else sFecha = DateTime.Parse(dr["t346_fecharef"].ToString()).ToShortDateString();

            sb.Append("<td>");
            sb.Append(sFecha);
            sb.Append("</td>");

            sb.Append("<td style='text-align:center'></td>");
            sb.Append("<td onmouseover='TTip(event)'><span class='NBR' style='width:260px'>" + dr["nom_cliente"].ToString() + "</span></td>");
            sb.Append("</tr>");
        }
        dr.Close();
        dr.Dispose();
        sb.Append("</tbody>");
        sb.Append("</table>");
        strTablaHtml = sb.ToString();

        return "OK@#@" + strTablaHtml;
    }
    //private string ObtenerPSTs(string sOrden, string sAscDesc, string sTipo, string sCR)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    bool? bEstado;
    //    string sFecha, sChecked = "";

    //    sb.Append("<table id='tblDatos' class='texto MANO' style='WIDTH: 970px; table-layout:fixed; ' cellSpacing='0' border='0' mantenimiento='1'>");
    //    sb.Append("<colgroup><col style='width:10px;' /><col style='width:200px;padding-left:5px;'><col style='width:335px'><col style='width:90px;align:center'><col style='width:50px;text-align:center'><col style='width:285px'></colgroup>");
    //    sb.Append("<tbody>");
    //    //Si sTipo=T no restrinjo por estado de la PST, sino saco solo las activas
    //    if (sTipo == "T") bEstado = null;
    //    else bEstado = true;
    //    SqlDataReader dr = PST.Catalogo(null, int.Parse(sCR), "", "", bEstado, null, null, "", null, byte.Parse(sOrden), byte.Parse(sAscDesc));
    //    while (dr.Read())
    //    {
    //        sb.Append("<tr id='" + dr["t346_idpst"].ToString() + "' bd='' idOText='" + dr["idOTExterno"].ToString() + "' idOriExt='" + dr["idOrigenExterno"].ToString() + "'");
    //        sb.Append(" cli='" + dr["cod_cliente"].ToString() + "' onclick='mmse(this)' >");//onkeydown='activarGrabar()'
    //        sb.Append("<td><img src='../../../../../images/imgFN.gif'></td>");
    //        sb.Append("<td><input type='text' class='txtL' style='width:200px' value='" + dr["t346_codpst"].ToString() + "' maxlength='25' onKeyUp='fm(this)'></td>");
    //        sb.Append("<td><input type='text' class='txtL' style='width:330px' value='" + dr["t346_despst"].ToString() + "' maxlength='30' onKeyUp='fm(this)'></td>");
    //        //sb.Append("<td onmouseover='TTip()'><nobr class='NBR' style='width:330px'>" + dr["t346_despst"].ToString() + "</nobr></td>");

    //        if (dr["t346_fecharef"].ToString() == "") sFecha = "";
    //        else sFecha = DateTime.Parse(dr["t346_fecharef"].ToString()).ToShortDateString();
    //        sb.Append("<td>");
    //        sb.Append("<input type='text' id='f" + dr["t346_idpst"].ToString() + "' value='" + sFecha + "' class='txtL' style='width:60px;' readonly Calendar='oCal' onclick='mc(this);fm(this);'>");
    //        sb.Append("</td>");

    //        if ((bool)dr["t346_estado"]) sChecked = "checked";
    //        else sChecked = "";
    //        sb.Append("<td><input type='checkbox' class='check' onclick='fm(this)' " + sChecked + "></td>");
    //        sb.Append("<td onmouseover='TTip()'><nobr class='NBR' style='width:265px'>" + dr["nom_cliente"].ToString() + "</nobr></td>");
    //        sb.Append("</tr>");
    //    }
    //    dr.Close();
    //    dr.Dispose();
    //    sb.Append("</tbody>");
    //    sb.Append("</table>");
    //    strTablaHtml = sb.ToString();

    //    return "OK@#@" + strTablaHtml;
    //}

    //protected string EliminarPST(string strIDPST)
    //{
    //    string sResul = "";
    //    string sCodOtc, sDescOtc = "";
    //    try
    //    {
    //        oConn = Conexion.Abrir();
    //        tr = Conexion.AbrirTransaccion(oConn);
    //    }
    //    catch (Exception ex)
    //    {
    //        sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
    //        return sResul;
    //    }

    //    try
    //    {
    //        string[] aOTC = Regex.Split(strIDPST, "##");
    //        foreach (string oOTC in aOTC)
    //        {
    //            string[] aOTC2 = Regex.Split(oOTC, @"\\");
    //            sCodOtc = aOTC2[0];
    //            sDescOtc = Utilidades.unescape(aOTC2[1]);
    //            PST.Delete(null, int.Parse(sCodOtc));
    //        }
    //        Conexion.CommitTransaccion(tr);
    //        sResul = "OK@#@";
    //    }
    //    catch (Exception ex)
    //    {
    //        Conexion.CerrarTransaccion(tr);
    //        sResul = "Error@#@" + Errores.mostrarError("Error al eliminar la OTC ", ex) + "@#@" + sDescOtc;
    //    }
    //    finally
    //    {
    //        Conexion.Cerrar(oConn);
    //    }

    //    return sResul;
    //}
    private void cargarNodos(string sNodo)
    {
        try
        {
            bool bSeleccionado = false;
            //Cargo la denominacion del label Nodo
            this.lblNodo.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);

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
                        ObtenerPSTs("A", dr["t303_idnodo"].ToString());
                    }
                    else
                    {
                        if (sNodo == dr["t303_idnodo"].ToString())
                        {
                            oLI.Selected = true;
                            bSeleccionado = true;
                            ObtenerPSTs("A", sNodo);
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

    protected string Grabar(string strDatos)
    {
        string sResul = "", sElementosInsertados = "";
        int nAux = 0;

        #region apertura de conexión y transacción
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
            string[] aClase = Regex.Split(strDatos, "///");
            foreach (string oClase in aClase)
            {
                if (oClase == "") continue;
                string[] aValores = Regex.Split(oClase, "##");
                //0. Opcion BD. "I", "U", "D"
                //1. ID OTC
                //2. codigo OTC
                //3. Descripcion OTC
                //4. ID cliente
                //5. Fecha de referncia
                //6. Activo
                //7. Nodo
                //8 id OT externa;
                //9 id Origen Externo;
                //10 horas;
                //11 presupuesto;
                //12 id Moneda;

                bool bEstado = false;
                if (aValores[6] == "1") bEstado = true;
                int? iCodCli;
                if (aValores[4] == "") iCodCli = null;
                else iCodCli = int.Parse(aValores[4]);
                int? idOTExt;
                if (aValores[8] == "") idOTExt = null;
                else idOTExt = int.Parse(aValores[8]);
                DateTime? dtFecha;
                if (aValores[5] == "") dtFecha = null;
                else dtFecha = DateTime.Parse(aValores[5]);

                decimal fHoras;
                if (aValores[10] == "") fHoras = 0;
                else fHoras = Decimal.Parse(aValores[10]);

                decimal fPresupuesto;
                if (aValores[11] == "") fPresupuesto = 0;
                else fPresupuesto = Decimal.Parse(aValores[11]);

                switch (aValores[0])
                {
                    case "I":
                        nAux = PST.Insert(null, int.Parse(aValores[7]), Utilidades.unescape(aValores[2]),
                                Utilidades.unescape(aValores[3]), bEstado, iCodCli, idOTExt, aValores[9], dtFecha, fHoras, fPresupuesto, aValores[12]);

                        if (sElementosInsertados == "") sElementosInsertados = nAux.ToString();
                        else sElementosInsertados += "//" + nAux.ToString();
                        break;
                    case "U":
                        nAux = PST.Update(null, int.Parse(aValores[1]), int.Parse(aValores[7]), Utilidades.unescape(aValores[2]),
                            Utilidades.unescape(aValores[3]), bEstado, iCodCli, idOTExt, aValores[9], dtFecha, fHoras, fPresupuesto, aValores[12]);
                        break;
                    case "D":
                        PST.Delete(null, int.Parse(aValores[1]));
                        break;
                }
            }
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + sElementosInsertados;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar las clases.", ex, false);// +"@#@" + sDesc;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

}