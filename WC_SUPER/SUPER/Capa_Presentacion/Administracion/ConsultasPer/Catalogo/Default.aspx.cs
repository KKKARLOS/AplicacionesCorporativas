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

using System.Text;
using System.Text.RegularExpressions;
using EO.Web; 
using System.Collections.Generic;
using SUPER.Capa_Negocio;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaHTML = "", strParametros = "";
    //public int nConsultas=0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 54;
                Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
                Master.TituloPagina = "Catálogo de consultas personalizadas";
                Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
                Master.FuncionesJavaScript.Add("Capa_Presentacion/Administracion/ConsultasPer/Catalogo/Functions/parametro.js");
                Master.bFuncionesLocales = true;

                //strArrayVAE = "";
                //cboTipoParam.Items.Insert(0, new ListItem("Entero", "I"));
                //cboTipoParam.Items.Insert(1, new ListItem("Varchar", "V"));
                //cboTipoParam.Items.Insert(2, new ListItem("Money", "M"));
                //cboTipoParam.Items.Insert(3, new ListItem("Date", "D"));
                //cboTipoParam.Items.Insert(4, new ListItem("Boolean", "B"));
                //cboTipoParam.Items.Insert(5, new ListItem("Añomes", "A"));
                cboTipoParam.SelectedValue = "I";
                cboVisible.SelectedValue = "M";
                //nConsultas = CONSULTAPERSONAL.GetNumConsultas(false);
                string sConsAux = Request.QueryString["nIdCons"];
                if (sConsAux != null) this.hdnIdCons.Value = sConsAux;

                string strTabla = obtenerConsultas("1");

                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] == "OK")
                {
                    this.strTablaHTML = aTabla[1];
                    strParametros = aTabla[2];
                }
                else Master.sErrores += Errores.mostrarError(aTabla[1]);
            }
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar las consultas", ex);
        }
        //1º Se indican (por este orden) la función a la que se va a devolver el resultado
        //   y la función que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);

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
            case ("buscar"):
                sResultado += obtenerConsultas(aArgs[1]);
                break;
            case ("preEliminarAE"):
                sResultado += preEliminarConsulta(aArgs[1]);
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

    private string obtenerConsultas(string sActivos)
    {
        try
        {
            string sParams = "", sProcAlm;
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = CONSULTAPERSONAL.ObtenerCatalogo(null, (sActivos == "1") ? true : false);

            sb.Append("<table id='tblDatos' class='texto MA' style='width:940px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:300px;' /><col style='width:65px;' /><col style='width:220px;' /><col style='width:30px;' /><col style='width:200px;' /><col style='width:115px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDatos'>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t472_idconsulta"].ToString() + "' bd='' ");
                sb.Append("style='height:20px;' onclick='ms(this);refrescarParams(this.id);habCom();' ondblclick='setProfesionales(this.id)'>");
                sb.Append("<td><img src='../../../../images/imgFN.gif'></td>");
                sb.Append("<td><input type='text' class='txtL' style='width:295px' value='" + dr["t472_denominacion"].ToString() + "' maxlength='50' onKeyUp='fm(event)'></td>");

                //sb.Append("<td>SUP_USER_</td>");
                sb.Append("<td><input type='text' class='txtL' style='width:65px' value='SUP_USER_' readonly='true'></td>");
                sb.Append("<td><input type='text' class='txtL' style='width:220px' value='");
                sProcAlm = dr["t472_procalm"].ToString();
                if (sProcAlm.Length < 8)
                    sb.Append(sProcAlm);
                else
                {
                    if (sProcAlm.StartsWith("SUP_USER_"))
                        sb.Append(sProcAlm.Substring(9));
                    else
                    {
                        int iLen = sProcAlm.Length;
                        if (iLen > 21) iLen = 21;
                        sb.Append(sProcAlm.Substring(0, iLen));
                    }
                }
                sb.Append("' maxlength='21' onKeyUp='fm(event)'></td>");
                sb.Append("<td><input type='checkbox' style='width:15px;' class='check' onclick='fm(event)' ");
                if ((bool)dr["t472_estado"]) sb.Append("checked=true");
                sb.Append("></td>");
                sb.Append("<td><nobr class='NBR W190' onmouseover='TTip(event)' style='height:16px;'>" + dr["t472_descripcion"].ToString().Replace(((char)13).ToString(), "<br>") + "</nobr></td>");
                if (User.IsInRole("DIS"))
                    sb.Append("<td><input type='text' class='txtL' style='width:105px' value='" + dr["t472_clavews"].ToString() + "' maxlength='20' onKeyUp='fm(event)'></td>");
                else
                    sb.Append("<td><input type='text' class='txtL' style='width:105px' value='" + dr["t472_clavews"].ToString() + "' maxlength='20' readonly='true'></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</tbody>");
            sb.Append("</table>");
            //Cargo un array con todos los valores de todas las consultas
            //Ese array es necesario para poder realizar la grabación de los parametros de las diferentes consultas del catálogo
            sParams = ObtenerParametrosConsulta(sActivos);

            return "OK@#@" + sb.ToString() + "@#@" + sParams;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al cargar las consultas", ex);
        }
    }
    protected string preEliminarConsulta(string strIDAE)
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
                iNumElems = CONSULTAPERSONAL.numUsuarios(int.Parse(oAE));
            }
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + iNumElems.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al intentar eliminar consultas", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }
    private string ObtenerParametrosConsulta(string sActivos)
    {
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append(" aParams = new Array();\n");
        SqlDataReader dr = PARAMETROCONSULTAPERSONAL.Catalogo(null, (sActivos == "1") ? true : false);
        int i = 0;
        while (dr.Read())
        {
            sbuilder.Append("\taParams[" + i.ToString() + "] = {bd:\"\", " +
                            "idCons:\"" + dr["t472_idconsulta"].ToString() + "\"," +
                            "idParam:\"" + Utilidades.escape(dr["t474_textoparametro"].ToString()) + "\"," +
                            "nombre:\"" + Utilidades.escape(dr["t474_nombreparametro"].ToString()) + "\"," +
                            "texto:\"" + Utilidades.escape(dr["t474_textoparametro"].ToString()) + "\"," +
                            "comentario:\"" + Utilidades.escape(dr["t474_comentarioparametro"].ToString()) + "\"," +
                            "tipo:\"" + dr["t474_tipoparametro"].ToString() + "\"," +
                            "defecto:\"" + Utilidades.escape(dr["t474_valordefecto"].ToString()) + "\"," +
                            "visible:\"" + dr["t474_visible"].ToString() + "\"," +
                            "orden:" + dr["t474_orden"].ToString() + ",");
            if ((bool)dr["t474_opcional"]) sbuilder.Append("opcional:\"1\"};\n");
            else sbuilder.Append("opcional:\"0\"};\n");
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
        catch (Exception)
        {
            //sErrores = "Error@#@" + Errores.mostrarError("Error al grabar las consultas.", ex);
        }

        return iResul;
    }
    
    private string Grabar(string strConsultas, string strParametros)
    {
        string sResul = "", sNuevosAEs = "", sClaveWS="";
        int idConsulta;
        try
        {
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

            #region CONSULTAS
            int idNuevaConsulta;

            string[] aAE = Regex.Split(strConsultas, "///");
            foreach (string oAE in aAE)
            {
                if (oAE == "") continue;
                string[] aValores = Regex.Split(oAE, "##");
                //0. Opcion BD. "I", "U", "D"
                //1. ID consulta
                //2. Denominación
                //3. Procedimiento almacenado
                //4. Estado
                //5. Descripción
                //6. Clave Web Service
                switch (aValores[0])
                {
                    case "D":
                        CONSULTAPERSONAL.Delete(tr, int.Parse(aValores[1]));
                        break;
                    case "I":
                        if (aValores[6] != "")
                        {
                            sClaveWS = Utilidades.unescape(aValores[6]).Trim();
                            //Compruebo que no exista ya esa clave
                            if (CONSULTAPERSONAL.ExisteClave(tr, sClaveWS, -1))
                                throw (new NullReferenceException("La clave del servicio web está asignada a otra consulta."));
                        }
                        idNuevaConsulta = CONSULTAPERSONAL.Insert(tr, Utilidades.unescape(aValores[2]), Utilidades.unescape(aValores[3]), 
                                                                  (aValores[4] == "1") ? true : false, Utilidades.unescape(aValores[5]),
                                                                  Utilidades.unescape(aValores[6]));
                        sNuevosAEs += aValores[1] + "##" + idNuevaConsulta.ToString() + "@@";
                        break;
                    case "U":
                        if (aValores[6] != "")
                        {
                            sClaveWS = Utilidades.unescape(aValores[6]).Trim();
                            //Compruebo que no exista ya esa clave
                            if (CONSULTAPERSONAL.ExisteClave(tr, sClaveWS, int.Parse(aValores[1])))
                                throw (new NullReferenceException("La clave del servicio web está asignada a otra consulta."));
                        }
                        CONSULTAPERSONAL.Update(tr, int.Parse(aValores[1]), Utilidades.unescape(aValores[2]), Utilidades.unescape(aValores[3]), 
                                                (aValores[4] == "1") ? true : false, Utilidades.unescape(aValores[5]), 
                                                Utilidades.unescape(aValores[6]));
                        break;
                }
            }
            #endregion

            #region PARAMETROS CONSULTA

            string[] aVAE = Regex.Split(strParametros, "///");

            foreach (string oVAE in aVAE)
            {
                if (oVAE == "") break;
                string[] aKeysAE = Regex.Split(sNuevosAEs, "@@");
                string[] aValoresVAE = Regex.Split(oVAE, "##");
                ///aValoresVAE[0] = opcionBD;
                ///aValoresVAE[1] = idConsulta;
                ///aValoresVAE[2] = idParámetro;
                ///aValoresVAE[3] = tipo;
                ///aValoresVAE[4] = comentario;
                ///aValoresVAE[5] = defecto;
                ///aValoresVAE[6] = visible;
                ///aValoresVAE[7] = orden;
                ///aValoresVAE[8] = texto;
                ///aValoresVAE[9] = nombre;
                ///aValoresVAE[10] = opcional;
                idConsulta = int.Parse(aValoresVAE[1]);
                if (idConsulta < 0)
                    idConsulta = flBuscarKeyAE(aValoresVAE[1], aKeysAE);

                switch (aValoresVAE[0])
                {
                    case "I":
                        PARAMETROCONSULTAPERSONAL.Insert(tr, idConsulta, 
                                                        Utilidades.unescape(aValoresVAE[8]),
                                                        Utilidades.unescape(aValoresVAE[9]),
                                                        aValoresVAE[3], 
                                                        Utilidades.unescape(aValoresVAE[4]),
                                                        Utilidades.unescape(aValoresVAE[5]),
                                                        aValoresVAE[6], 
                                                        byte.Parse(aValoresVAE[7]),
                                                        (aValoresVAE[10]=="0")?false:true);
                        break;
                    case "U":
                        PARAMETROCONSULTAPERSONAL.Update(tr, idConsulta,
                                                        Utilidades.unescape(aValoresVAE[8]),
                                                        Utilidades.unescape(aValoresVAE[9]),
                                                        aValoresVAE[3],
                                                        Utilidades.unescape(aValoresVAE[4]),
                                                        Utilidades.unescape(aValoresVAE[5]),
                                                        aValoresVAE[6],
                                                        byte.Parse(aValoresVAE[7]),
                                                        (aValoresVAE[10] == "0") ? false : true);
                        break;
                    case "D":
                        PARAMETROCONSULTAPERSONAL.Delete(tr, int.Parse(aValoresVAE[1]), Utilidades.unescape(aValoresVAE[2]));
                        break;
                }
            }
            #endregion

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + sNuevosAEs;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar las consultas.", ex, false);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
