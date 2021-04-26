using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strArrayPSN = "";
    public string sNodo = "";
    private bool bHayPreferencia = false;
    public short nPantallaPreferencia = 16;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 39;
                Master.bFuncionesLocales = true;
                Master.FuncionesJavaScript.Add("Capa_Presentacion/ECO/TraspasoIAP/Functions/PSN.js");
                Master.TituloPagina = "Traspaso de IAP";

                try
                {
                    //if (!(bool)Session["FORANEOS"])
                    //{
                    //    this.imgForaneo.Visible = false;
                    //    this.lblForaneo.Visible = false;
                    //}
                    sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);

                    string[] aDatosPref = Regex.Split(getPreferencia(""), "@#@");
                    if (bHayPreferencia && aDatosPref[0] == "OK")
                    {
                        chkSobreescribir.Checked = (aDatosPref[1] == "1") ? true : false;
                        chkRPCCR.Checked = (aDatosPref[2] == "1") ? true : false;
                        chkProfCon.Checked = (aDatosPref[3] == "1") ? true : false;
                    }
                    else if (aDatosPref[0] == "Error") Master.sErrores += Errores.mostrarError(aDatosPref[1]);

                    int? nPSN = null;
                    if (Request.QueryString["nPSN"] != null)
                    {
                        nPSN = (int?)int.Parse(Utilidades.decodpar(Request.QueryString["nPSN"].ToString()));
                        this.chkRPCCR.Checked = false;
                        this.chkRPCCR.Disabled = true;
                    }

                    ObtenerPSNs(nPSN);
                }
                catch (Exception ex)
                {
                    Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
                }

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("getPSN"):
                sResultado += getDatosPSN(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("traspglobal"):
                sResultado += Traspglobal(aArgs[1]);
                break;
            case ("setPreferencia"):
                sResultado += setPreferencia(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("delPreferencia"):
                sResultado += delPreferencia();
                break;
            case ("getPreferencia"):
                sResultado += getPreferencia(aArgs[1]);
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

    private void ObtenerPSNs(int? nPSN)
    {
        StringBuilder sb = new StringBuilder();

        SqlDataReader dr = null;
        if (nPSN==null) dr = CONSPERMES.ObtenerPSNaTraspasar((int)Session["UsuarioActual"]);
        else dr = CONSPERMES.ObtenerPSNaTraspasarByPSN((int)nPSN);

        while (dr.Read())
        {
            sb.Append("insertarPSNEnArray(" + dr["t305_idproyectosubnodo"].ToString() + ",");
            sb.Append(dr["t301_idproyecto"].ToString() + ",");
            sb.Append("\""+ Utilidades.escape(dr["t301_denominacion"].ToString()) + "\",");
            sb.Append("\""+ Utilidades.escape(dr["t302_denominacion"].ToString()) + "\",");
            sb.Append("\""+ Utilidades.escape(dr["t303_denominacion"].ToString()) + "\",");
            sb.Append(dr["t325_traspasoIAP"].ToString() + ",");
            sb.Append("\""+ dr["t301_estado"].ToString() + "\",");
            sb.Append("\""+ dr["t301_categoria"].ToString() + "\",");
            sb.Append("\""+ dr["t305_cualidad"].ToString() + "\",");
            sb.Append(dr["tiene_consumos"].ToString() + ",");
            sb.Append(dr["annomes_traspaso"].ToString() + ",");
            sb.Append("\"" + dr["t325_estado"].ToString() + "\",");
            sb.Append("\"" + dr["t301_modelocoste"].ToString() + "\");\n");
            //sb.Append("\taVAE_js[" + i.ToString() + "] = new Array(\"" + dr["t341_idae"].ToString() + "\",\"" + dr["t340_idvae"].ToString() + "\",\"" + dr["t340_valor"].ToString() + "\");\n");
        }
        dr.Close();
        dr.Dispose();

        strArrayPSN = sb.ToString();
    }
    private string getDatosPSN(string sPSN, string sAnnomes, string sModelocoste, string sProfCons, string sEstadoMes, string esMesModificable)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = CONSPERMES.ObtenerDatosPSNaTraspasar(null, int.Parse(sPSN), int.Parse(sAnnomes), sModelocoste, (sProfCons=="1")?true:false, false);

            sb.Append("<table id='tblDatos' style='width:700px;' mantenimiento=1>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:440px;' /><col style='width:60px;' /><col style='width:60px;' /><col style='width:60px;' /><col style='width:60px;' /></colgroup>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' ");
                sb.Append("costecon='" + dr["t330_costecon"].ToString() + "' ");
                sb.Append("costerep='" + dr["t330_costerep"].ToString() + "' ");
                sb.Append("nodo_usuario='" + dr["t303_idnodo"].ToString() + "' ");
                sb.Append("empresa_nodo='" + dr["t313_idempresa"].ToString() + "' ");

                sb.Append("style='height:20px' onclick='ms(this)'>");
                sb.Append("<td style=' border-right: 0px;'><img border='0' src='../../../Images/imgUsu" + dr["tipo"].ToString() + dr["t001_sexo"].ToString() + ".gif' width='16px' height='16px' /></td>");
                sb.Append("<td>" + dr["profesional"].ToString() + "</td>");
                sb.Append("<td style='text-align:right;'>" + double.Parse(dr["horas_reportadas_proy"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='text-align:right;'>" + double.Parse(dr["jornadas_reportadas_proy"].ToString()).ToString("N") + "</td>");

                if (sModelocoste == "J") 
                    sb.Append("<td style='text-align:right;'>" + double.Parse(dr["jornadas_adaptadas"].ToString()).ToString("N") + "</td>");
                else
                    sb.Append("<td style='text-align:right;'>" + double.Parse(dr["horas_adaptadas"].ToString()).ToString("N") + "</td>");

                sb.Append("<td style='text-align:right; padding-right:2px;'><input id='txtUE-" + dr["t314_idusuario"].ToString() + "' type='text' class='txtNumL' style='width:55px' value='" + double.Parse(dr["unidades_economicas"].ToString()).ToString("N") + "' ");//
                if (esMesModificable=="N" || (sEstadoMes == "C" && Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "SA"))
                    sb.Append(" readonly");
                else
                    sb.Append(" onchange='activarGrabar();setTotales();' onfocus='fn(this, 5, 2);'");
                sb.Append(" ></td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return "OK@#@"+ sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos del proyecto.", ex);
        }
    }
    private string Grabar(string sPSN, string sAnnomes, string strProfesionales, string sHayDatosProf)
    {
        string sResul = "";
        string sEstadoMes = "";
        int nPSN = int.Parse(sPSN), nAnnomes = int.Parse(sAnnomes), nSMPSN = 0;
        bool bErrorControlado = false;

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
            if (PROYECTOSUBNODO.ObtenerUltCierreEcoNodoPSN(tr, nPSN) >= nAnnomes)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = "Error@#@" + Errores.mostrarError("No se ha realizado el traspaso, debido a que el "+ Estructura.getDefLarga(Estructura.sTipoElem.NODO) +" se encuentra cerrado en el mes a traspasar.");
            }
            else
            {
                nSMPSN = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, nPSN, nAnnomes);
                if (nSMPSN == 0)
                {
                    if (sHayDatosProf == "1")
                    {
                        sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, nPSN, nAnnomes);
                        if (sEstadoMes != "C")
                            nSMPSN = SEGMESPROYECTOSUBNODO.Insert(tr, nPSN, nAnnomes, sEstadoMes, 0, 0, false, 0, 0);
                    }
                }
                else{
                    if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "SA")
                    {
                        SEGMESPROYECTOSUBNODO oSMPSN = SEGMESPROYECTOSUBNODO.Obtener(tr, nSMPSN, null);
                        if (oSMPSN.t325_estado == "C")
                        {
                            bErrorControlado = true;
                            throw (new Exception("No se permite grabar, debido a que el mes en curso está cerrado para el proyecto."));
                        }
                    }

                    CONSPERMES.DeleteByT325_idsegmesproy(tr, nSMPSN);
                }

                #region Datos Profesionales
                if (strProfesionales != "" && sEstadoMes != "C")
                {
                    string[] aProfesionales = Regex.Split(strProfesionales, "##");
                    foreach (string oProf in aProfesionales)
                    {
                        if (oProf == "") continue;
                        string[] aProf = Regex.Split(oProf, "//");
                        ///aProf[0] = idUsuario
                        ///aProf[1] = costecon
                        ///aProf[2] = costerep
                        ///aProf[3] = nodo_usuario
                        ///aProf[4] = empresa_nodo
                        ///aProf[5] = unidades económicas

                        CONSPERMES.Insert(tr, nSMPSN, int.Parse(aProf[0]), double.Parse(aProf[5]), decimal.Parse(aProf[1]), decimal.Parse(aProf[2]), (aProf[3] != "") ? (int?)int.Parse(aProf[3]) : null, (aProf[4] != "") ? (int?)int.Parse(aProf[4]) : null);
                    }
                }
                #endregion

                SEGMESPROYECTOSUBNODO.UpdateTraspasoIAP(tr, nSMPSN, true);

                Conexion.CommitTransaccion(tr);
                sResul = "OK@#@";
            }
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al grabar los consumos del proyecto.", ex);
            else sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string Traspglobal(string sSobreescribir)
    {
        string sResul = "";
        string sEstadoMes = "";
        int nSMPSN = 0;
        DataSet dsProf = null;

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
            DataSet ds = CONSPERMES.ObtenerPSNaTraspasarDS((int)Session["UsuarioActual"]);
            foreach (DataRow oPSN in ds.Tables[0].Rows)
            {
                if (PROYECTOSUBNODO.ObtenerUltCierreEcoNodoPSN(tr, (int)oPSN["t305_idproyectosubnodo"]) >= (int)oPSN["annomes_traspaso"])
                {
                    Conexion.CerrarTransaccion(tr);
                    sResul = "Error@#@" + Errores.mostrarError("No se ha realizado el traspaso, debido a que el " + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + " '" + oPSN["t303_denominacion"].ToString() + "' se encuentra cerrado en el mes a traspasar.");
                    break;
                }
                else
                {
                    nSMPSN = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, (int)oPSN["t305_idproyectosubnodo"], (int)oPSN["annomes_traspaso"]);
                    if (nSMPSN == 0)
                    {
                        if (oPSN["tiene_consumos"].ToString() == "1")
                        {
                            sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, (int)oPSN["t305_idproyectosubnodo"], (int)oPSN["annomes_traspaso"]);
                            if (sEstadoMes == "C")
                                continue;

                            nSMPSN = SEGMESPROYECTOSUBNODO.Insert(tr, (int)oPSN["t305_idproyectosubnodo"], (int)oPSN["annomes_traspaso"], sEstadoMes, 0, 0, false, 0, 0);
                        }
                    }
                    else
                    {
                        SEGMESPROYECTOSUBNODO oSegMes = SEGMESPROYECTOSUBNODO.Obtener(tr, nSMPSN, null);
                        if (oSegMes.t325_estado == "C")
                            continue;

                        if (sSobreescribir == "1") CONSPERMES.DeleteByT325_idsegmesproy(tr, nSMPSN);
                    }

                    #region Datos Profesionales
                    if (oPSN["tiene_consumos"].ToString() == "1") //si tiene consumos técnicos (IAP)
                    {
                        dsProf = CONSPERMES.ObtenerDatosPSNaTraspasarDS(tr, (int)oPSN["t305_idproyectosubnodo"], (int)oPSN["annomes_traspaso"], oPSN["t301_modelocoste"].ToString(), true, (sSobreescribir == "1")? false:true);
                        foreach (DataRow oProf in dsProf.Tables[0].Rows)
                        {
                            double nUnidades = (oPSN["t301_modelocoste"].ToString() == "J") ? double.Parse(oProf["jornadas_adaptadas"].ToString()) : double.Parse(oProf["horas_reportadas_proy"].ToString());
                            if (nUnidades != 0){
                                CONSPERMES.Insert(tr, nSMPSN,
                                                (int)oProf["t314_idusuario"],
                                                (oPSN["t301_modelocoste"].ToString() == "J") ? double.Parse(oProf["jornadas_adaptadas"].ToString()) : double.Parse(oProf["horas_reportadas_proy"].ToString()),
                                                decimal.Parse(oProf["t330_costecon"].ToString()),
                                                decimal.Parse(oProf["t330_costerep"].ToString()),
                                                (oProf["t303_idnodo"] != DBNull.Value)? (int?)oProf["t303_idnodo"]:null,
                                                (oProf["t313_idempresa"] != DBNull.Value)? (int?)oProf["t313_idempresa"]:null);
                            }
                        }
                        dsProf.Dispose();
                    }

                    #endregion

                    SEGMESPROYECTOSUBNODO.UpdateTraspasoIAP(tr, nSMPSN, true);
               }
            }

            ds.Dispose();

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al realizar el traspaso global.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    private string setPreferencia(string sSobreescribir, string sRPCCR, string sProfCon)
    {
        string sResul = "";

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

            int nPref = PREFERENCIAUSUARIO.Insertar(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 16,
                                        sSobreescribir,
                                        sRPCCR,
                                        sProfCon,
                                        null, null, null, null, null, null, null,
                                        null, null, null, null, null, null, null, null, null, null, null);

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + nPref.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al guardar la preferencia.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string delPreferencia()
    {
        try
        {
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 16);
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al eliminar la preferencia", ex);
        }
    }
    private string getPreferencia(string sIdPrefUsuario)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
                                                        (int)Session["IDFICEPI_PC_ACTUAL"], 16);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["Sobreescribir"].ToString() + "@#@"); //0
                sb.Append(dr["RPCCR"].ToString() + "@#@"); //1
                sb.Append(dr["ProfCon"].ToString()); //2
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
        }
    }

}
