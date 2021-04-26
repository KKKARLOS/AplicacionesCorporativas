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


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtml, strTablaHtmlVAE, strArrayVAE;
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            // De momento no muestro el botón 37
            //Master.sbotonesOpcionOn = "2,7,4,71";
            Master.sbotonesOpcionOn = "2,7,4,37";
            Master.sbotonesOpcionOff = "4";

            Master.TituloPagina = "Catálogo de criterios estadísticos económicos corporativos";
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FuncionesJavaScript.Add("Capa_Presentacion/PSP/Mantenimientos/AE/Catalogo/Functions/vae.js");
            Master.FuncionesJavaScript.Add("Capa_Presentacion/ECO/Mantenimientos/AE_Corporativos/Catalogo/Functions/vaeNodo.js");
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                    strArrayVAE = "";
                    //Cargo la denominacion del label Nodo
                    string sNodo = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                    if (sNodo.Trim() != "")
                    {
                        this.lblNodo.InnerText = sNodo;
                        this.lblNodo1.InnerText = "Asociados al " + sNodo;
                        //this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                        //this.gomaNodo.Attributes.Add("title", "Borra el " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    }
                    ObtenerAEs();
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
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5]);
                break;
            case ("getCECnodo"):
                sResultado += GetCriteriosDeNodos(aArgs[1]);
                break;
            case ("eliminar"):
                sResultado += EliminarAE(aArgs[1]);
                break;
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

    private void ObtenerAEs()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table id='tblDatos' class='texto MM' style='WIDTH: 400px;' mantenimiento='1'>");
        SqlDataReader dr = CEC.Catalogo(null, "", null, null, 4, 0);
        sb.Append("<colgroup><col style='width:10px;' /><col style='width:20px;' /><col style='width:310px;' /><col style='width:60px' /></colgroup>");
        sb.Append("<tbody id='tbodyDatos'>");
        while (dr.Read())
        {
            sb.Append("<tr id='" + dr["t345_idcec"].ToString() + "' bd='' orden='" + dr["t345_orden"].ToString() + "' style='height:20px' onclick='ms(this);refrescarVAEs(this.id);localizarAENodo(this.id);' onmouseover='TTip(event);' onmousedown='limpiarSeleccion();DD(event);'>");
            sb.Append("<td><img src='../../../../../images/imgFN.gif'></td>");
            sb.Append("<td><img src='../../../../../images/imgMoveRow.gif' style='cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>");
            sb.Append("<td style='padding-left:5px;'><input type='text' id='txtDesc' class='txtL' style='width:300px' value='" + dr["t345_denominacion"].ToString() + "' maxlength='30' onKeyUp='fm(event)'></td>");
            sb.Append("<td><input type='checkbox' style='width:15px;margin-left:10px;' class='check' onclick='fm(event)' ");
            if ((bool)dr["t345_estado"]) sb.Append("checked=true");
            sb.Append("></td></tr>");
        }
        dr.Close();
        dr.Dispose();
        //Cargo un array con todos los valores de todos los Atributos Estadisticos 
        //Ese array es necesario para poder realizar la grabación de los valores de los diferentes criterios estadísticos del catálogo
        this.sVAE.Value = ObtenerValoresAtributosEstadisticos();
        sb.Append("</tbody>");
        sb.Append("</table>");
        strTablaHtml = sb.ToString();
        //return "OK@#@" + sb.ToString() + "@#@" + sVaes;
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
                //AE.Delete(tr, int.Parse(oAE));
                CEC.Delete(tr, int.Parse(oAE));
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
                //iNumElems = AE.numAEusados(int.Parse(oAE));
                iNumElems = CEC.numCECusados(int.Parse(oAE));
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
    protected string Grabar(string strDatos, string strDatosVAE, string sCEC, string sVCEC, string strNodos)
    {
        string sResul = "", sNuevosAEs = "", sNuevosVAEs = "", sTablaAct="T341";
        bool bEstado;
        int idAE, idVAE, idNodo;
        string[] aKeysAE;
        string[] aKeysVAE;
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
            int idNuevoAE;
            string[] aAE = Regex.Split(strDatos, "///");
            foreach (string oAE in aAE)
            {
                if (oAE == "") continue;
                string[] aValores = Regex.Split(oAE, "##");
                //0. Opcion BD. "I", "U", "D"
                //1. ID AE
                //2. Denominación
                //3. Estado
                //4. Orden
                bEstado = false;
                if (aValores[3] == "1") bEstado = true;

                switch (aValores[0])
                {
                    case "D":
                        CEC.Delete(tr, int.Parse(aValores[1]));
                        break;
                    case "I":
                        idNuevoAE = CEC.Insert(tr, Utilidades.unescape(aValores[2]), bEstado, int.Parse(aValores[4]));
                        sNuevosAEs += aValores[1] + "##" + idNuevoAE.ToString() + "@@";
                        break;
                    case "U":
                        CEC.Update(tr, int.Parse(aValores[1]), Utilidades.unescape(aValores[2]), bEstado, int.Parse(aValores[4]));
                        break;
                }
            }
            #endregion
            aKeysAE = Regex.Split(sNuevosAEs, "@@");
            sTablaAct = "T340";
            #region VAE
            int idNuevoVAE;
            string[] aVAE = Regex.Split(strDatosVAE, "///");

            foreach (string oVAE in aVAE)
            {
                if (oVAE == "") break;
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
                        idNuevoVAE=VCEC.Insert(tr, Utilidades.unescape(aValoresVAE[3]), bEstadoVAE, idAE, byte.Parse(aValoresVAE[4]));
                        sNuevosVAEs += aValoresVAE[2] + "##" + idNuevoVAE.ToString() + "@@";
                        break;
                    case "U":
                        VCEC.Update(tr, int.Parse(aValoresVAE[2]), Utilidades.unescape(aValoresVAE[3]), bEstadoVAE, idAE, byte.Parse(aValoresVAE[4]));
                        break;
                    case "D":
                        VCEC.Delete(tr, int.Parse(aValoresVAE[2]));
                        break;
                }
            }
            #endregion
            aKeysVAE = Regex.Split(sNuevosVAEs, "@@");
            sTablaAct = "T345";
            #region CEC
            string[] aCEC = Regex.Split(sCEC, "///");
            foreach (string oCEC in aCEC)
            {
                if (oCEC != "")
                {
                    string[] aValores = Regex.Split(oCEC, "##");
                    //0. Opcion BD. "I", "U", "D"
                    //1. ID AE
                    //2. Obligatorio
                    //3. Nodo
                    idAE = int.Parse(aValores[1]);
                    if (idAE < 0)
                        idAE = flBuscarKeyAE(aValores[1], aKeysAE);

                    bEstado = false;
                    if (aValores[2] == "1") bEstado = true;
                    switch (aValores[0])
                    {
                        case "D":
                            CECNODO.Delete(tr, idAE, int.Parse(aValores[3]));
                            break;
                        case "I":
                            if (CECNODO.Existe(tr,idAE,int.Parse(aValores[3])))
                            {
                                CECNODO.Update(tr, idAE, int.Parse(aValores[3]), bEstado);
                            }
                            else
                                CECNODO.Insert(tr, idAE, int.Parse(aValores[3]), bEstado);
                            break;
                        case "U":
                            CECNODO.Update(tr, idAE, int.Parse(aValores[3]), bEstado);
                            break;
                    }
                }
            }
            #endregion
            sTablaAct = "T435";
            #region VCEC
            string[] aNodo = Regex.Split(strNodos, "///");
            string[] aVAENodo = Regex.Split(sVCEC, "///");

            foreach (string oNodo in aNodo)
            {
                if (oNodo == "") break;
                idNodo = int.Parse(oNodo);
                foreach (string oVAENodo in aVAENodo)
                {
                    if (oVAENodo == "") break;
                    string[] aValoresVAENodo = Regex.Split(oVAENodo, "##");
                    ///aValoresVAENodo[0] = opcionBD;
                    ///aValoresVAENodo[1] = idAE;
                    ///aValoresVAENodo[2] = idVAE;
                    idAE = int.Parse(aValoresVAENodo[1]);
                    if (idAE < 0)
                        idAE = flBuscarKeyAE(aValoresVAENodo[1], aKeysAE);

                    idVAE = int.Parse(aValoresVAENodo[2]);
                    if (idVAE < 0)
                        idVAE = flBuscarKeyAE(aValoresVAENodo[2], aKeysVAE);

                    switch (aValoresVAENodo[0])
                    {
                        case "I":
                        case "U":
                            if (!CECRESTRICCION.Existe(tr, idAE, idNodo, idVAE))
                                CECRESTRICCION.Insert(tr, idAE, idNodo, idVAE);
                            break;
                        case "D":
                            CECRESTRICCION.Delete(tr, idAE, idNodo, idVAE);
                            break;
                    }
                }
            }
            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + sNuevosAEs + "@#@" + sNuevosVAEs;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los criterios estadísticos.", ex) + "@#@" + sTablaAct;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

    private string ObtenerValoresAtributosEstadisticos()
    {
        StringBuilder sbuilder = new StringBuilder();
        byte bEstado;
        sbuilder.Append(" aVAES = new Array();\n");
        SqlDataReader dr = VCEC.Catalogo(null, "", null, null, null, 5, 0);
        int i = 0;
        while (dr.Read())
        {
            if ((bool)dr["t435_estado"]) bEstado = 1;
            else bEstado = 0;
            sbuilder.Append("\taVAES[" + i.ToString() + "] = {bd:\"\", " +
                            "idAE:\"" + dr["t345_idcec"].ToString() + "\"," +
                            "idVAE:\"" + dr["t435_idvcec"].ToString() + "\"," +
                            "nombre:\"" + Utilidades.escape(dr["t435_valor"].ToString()) + "\"," +
                            "estado:\"" + bEstado.ToString() + "\"," +
                            "orden:\"" + dr["t435_orden"].ToString() + "\"};\n");
            i++;
        }
        dr.Close();
        dr.Dispose();
        return sbuilder.ToString();
    }

    private string GetCriteriosDeNodos(string slNodos)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table id='tblCECNodo' class='texto MM' style='WIDTH: 400px;' mantenimiento='1'>");
        if (slNodos != "")
        {
            SqlDataReader dr = CEC.CatalogoCorporativosByListaNodos(slNodos);
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:330px;' /><col style='width:60px' /></colgroup>");
            sb.Append("<tbody id='tbodyCECNodo'>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t345_idcec"].ToString() + "' bd='' onclick='ms(this);refrescarVAEsNodo(this.id);' onmousedown='DD(event)'>");//onmouseover='TTip(event);' style='height:20px' 
                sb.Append("<td><img src='../../../../../images/imgFN.gif'></td>");
                sb.Append("<td style='padding-left:6px;'>" + dr["t345_denominacion"].ToString() + "</td>");
                sb.Append("<td><input type='checkbox' class='check' onclick='fm(event)' ");
                //if ((bool)dr["t381_obligatorio"]) sb.Append("checked=true");
                switch (int.Parse(dr["tipo_obli"].ToString()))
                {
                    case 0:
                        sb.Append(" style='width:15px;'");
                        break;
                    case 1:
                        sb.Append(" style='width:15px;' checked=true");
                        break;
                    case 2:
                        sb.Append(" style='width:15px;' checked=true disabled");
                        break;
                }
                sb.Append("></td></tr>");
            }
            dr.Close();
            dr.Dispose();
        }
        sb.Append("</tbody>");
        sb.Append("</table>");
        //Cargo un array con todos los valores de todos los Atributos Estadisticos 
        //Ese array es necesario para poder realizar la grabación de los valores de los diferentes criterios estadísticos del catálogo
        string sVaes = ObtenerVAEsDeNodos(slNodos);
        return "OK@#@" + sb.ToString() + "@#@" + sVaes;
    }
    private string ObtenerVAEsDeNodos(string slNodos)
    {
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append(" aVAEsNodo = new Array();\n");
        SqlDataReader dr = VCEC.CatalogoCorporativosByListaNodo(slNodos);
        int i = 0;
        while (dr.Read())
        {
            //sbuilder.Append("\taVAEsNodo[" + i.ToString() + "] = {bd:\"\"," +
            //                "idAE:\"" + dr["t345_idcec"].ToString() + "\"," +
            //                "idVAE:\"" + dr["t435_idvcec"].ToString() + "\"," +
            //                "idNodo:\"" + dr["t303_idnodo"].ToString() + "\"," +
            //                "nombre:\"" + Utilidades.escape(dr["t435_valor"].ToString()) + "\"};\n");
            sbuilder.Append("\taVAEsNodo[" + i.ToString() + "] = {bd:\"\"," +
                            "idAE:\"" + dr["t345_idcec"].ToString() + "\"," +
                            "idVAE:\"" + dr["t435_idvcec"].ToString() + "\"," +
                            "nombre:\"" + Utilidades.escape(dr["t435_valor"].ToString()) + "\"};\n");
            i++;
        }
        dr.Close();
        dr.Dispose();
        return sbuilder.ToString();
    }
    private int flBuscarKeyAE(string sIdAE, string[] aKeysAE)
    {
        int iResul = int.Parse(sIdAE);
        try
        {
            foreach (string oKey in aKeysAE)
            {
                if (oKey == "") continue;
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