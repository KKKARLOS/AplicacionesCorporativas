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

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public ArrayList aListCorreo = null;
    public string sHayAparcadas = "false";
	
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 45;
                Master.nResolucion = 1280;
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Cambio de estructura de usuarios";
                Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");

                rdbAmbito.Items[1].Text = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                //02/02/2016 Victor, sacar el mes del último mes fcerrado de la empresa
                //txtMesValor.Text = Fechas.AnnomesAFechaDescLarga(Fechas.FechaAAnnomes(DateTime.Now));
                PARAMETRIZACIONSUPER oPar = PARAMETRIZACIONSUPER.Select(null);
                txtMesValor.Text = Fechas.AnnomesAFechaDescLarga(Fechas.AddAnnomes(oPar.t725_ultcierreempresa_ECO, 1));

                hdnMesValor.Text = Fechas.FechaAAnnomes(DateTime.Now).ToString();

                if (CAMBIOESTRUCTURAUSUARIO.bHayAparcadas(null))
                    sHayAparcadas = "true";
            }
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
            case ("tecnicos"):
                sResultado += ObtenerUsuarios(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8]);
                break;
            case ("aparcar"):
                sResultado += Aparcar(aArgs[1]);
                break;
            case ("recuperar"):
                sResultado += Recuperar();
                break;
            case ("procesar"):
                sResultado += Procesar(aArgs[1], aArgs[2]);
                break;
            case ("replicasmeses"):
                sResultado += GenerarReplicasMeses();
                break;
            case ("aparcardel"):
                sResultado += AparcarDel();
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

    protected string ObtenerUsuarios(string strOpcion, string sApellido1, string sApellido2, string sNombre, string sNodoOrigen, string sMostrarBajas, string sLista, string sParesDatos)
    {
        string sResul = "";
        int? nNodo = null;
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        try
        {
            if (sNodoOrigen != "") nNodo = int.Parse(sNodoOrigen);

            SqlDataReader dr = USUARIO.ObtenerUsuarioCambioEstructura(strOpcion, Utilidades.unescape(sApellido1), Utilidades.unescape(sApellido2), Utilidades.unescape(sNombre), nNodo, (sMostrarBajas == "1") ? true : false, sLista);

            sb.Append("<table id='tblDatos' class='texto MAM' style='width: 560px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:320px;' /><col style='width:220px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("nodo_origen='" + dr["t303_idnodo"].ToString() + "' ");

                sb.Append("onclick='mm(event)' ondblclick='insertarRecurso(this)' onmousedown='DD(event)' ");
                sb.Append("style='height:20px' >");
                sb.Append("<td></td>");
                //sb.Append("<td><nobr class='NBR W310'  style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>Empresa:</label>" + dr["t313_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W310'  style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W210'  style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["t303_denominacion"].ToString() + "</nobr></td></tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            if (sParesDatos != "")
            {
                dr = USUARIO.ObtenerUsuariosCambioEstructuraParesDatos(sParesDatos);
                while (dr.Read())
                {
                    sb2.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' ");
                    sb2.Append("baja='" + dr["baja"].ToString() + "' ");
                    sb2.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb2.Append("nodo_origen='" + dr["t303_idnodo_origen"].ToString() + "' ");
                    sb2.Append("nodo_destino='" + dr["t303_idnodo_destino"].ToString() + "' ");
                    sb2.Append("procesado='' ");
                    sb2.Append("excepcion='' ");
                    sb2.Append("codigo_excepcion='' ");

                    sb2.Append("onclick='mm(event)' onmousedown='DD(event)' ");
                    sb2.Append("style='height:20px' >");
                    sb2.Append("<td></td>");
                    //sb2.Append("<td><nobr class='NBR W220' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Empresa:</label>" + dr["t313_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                    sb2.Append("<td><nobr class='NBR W220' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["Profesional"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                    sb2.Append("<td><nobr class='NBR W180' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                    sb2.Append("<td></td>");
                    sb2.Append("<td></td></tr>");
                }
                dr.Close();
                dr.Dispose();
            }
            sResul = "OK@#@" + sb.ToString() + "@#@" + sb2.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de usuarios.", ex, false);
        }

        return sResul;
    }
    private string Aparcar(string strDatos)
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
            CAMBIOESTRUCTURAUSUARIO.DeleteAll(tr);

            string[] aDatos = Regex.Split(strDatos, "///");
            foreach (string oUsuario in aDatos)
            {
                if (oUsuario == "") continue;
                string[] aUsuario = Regex.Split(oUsuario, "##");
                ///aUsuario[0] = idUsuario
                ///aUsuario[1] = idNodo_origen
                ///aUsuario[2] = idNodo_destino
                ///aUsuario[3] = Mes valor
                ///aUsuario[4] = procesado

                int? nNodoDestino = null;
                bool? bProcesado = null;
                if (aUsuario[2] != "") nNodoDestino = int.Parse(aUsuario[2]);
                if (aUsuario[4] != "") bProcesado = (aUsuario[4] == "1") ? true : false;

                CAMBIOESTRUCTURAUSUARIO.Insertar(tr, int.Parse(aUsuario[0]), nNodoDestino, int.Parse(aUsuario[3]), bProcesado);
            }

            Conexion.CommitTransaccion(tr);

            sResul = "OK";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al aparcar la situación destino.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string AparcarDel()
    {
        string sResul = "";

        try
        {
            CAMBIOESTRUCTURAUSUARIO.DeleteAll(null);
            sResul = "OK";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al borrar la situación destino.", ex);
        }

        return sResul;
    }
    protected string Recuperar()
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = CAMBIOESTRUCTURAUSUARIO.CatalogoDestino();

            sb.Append("<table id='tblDatos2' class='texto MM' style='width: 560px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:230px;' /><col style='width:180px;' /><col style='width:100px;' /><col style='width:20px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("nodo_origen='" + dr["t303_idnodo_origen"].ToString() + "' ");
                sb.Append("nodo_destino='" + dr["t303_idnodo_destino"].ToString() + "' ");
                sb.Append("procesado='' ");
                sb.Append("excepcion='' ");
                sb.Append("codigo_excepcion='' ");
                //if (dr["t466_procesado"].ToString() == "") sb.Append("procesado='' ");
                //else if ((bool)dr["t466_procesado"]) sb.Append("procesado='1' ");
                //else sb.Append("procesado='0' ");
                //sb.Append("excepcion='" + Utilidades.escape(dr["t466_excepcion"].ToString()) + "' ");
                //sb.Append("codigo_excepcion='" + dr["t776_codigoexcepcion"].ToString() + "' ");

                sb.Append("onclick='mm(event)' onmousedown='DD(event)' ");
                sb.Append("style='height:20px' >");
                sb.Append("<td></td>");
                //sb.Append("<td><nobr class='NBR W220'  style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Empresa:</label>" + dr["t313_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<td style='padding-left:2px;'><nobr class='NBR W220'  style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["Profesional"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W180'  style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td align='center'>" + dr["t466_anomes"].ToString() + "</td>");
                sb.Append("<td></td></tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al recuperar la relación de usuarios.", ex);
        }

        return sResul;
    }

    private string Procesar(string sPorDeadLockTimeout, string strDatos)
    {
        string sResul = "";
        int nUsuario = 0, nNodoOrigen = 0, nNodoDestino = 0, nAnomesValor = 0;
        DataSet dsCB211 = null, dsCB212 = null, dsCB213 = null, dsCB223 = null, dsCB232 = null, dsCB24 = null; //dsConsumosBorrados
        aListCorreo = new ArrayList();
        bool bErrorDeadLockTimeout = false;
        try
        {
            string[] aDatos = Regex.Split(strDatos, "///");
            bool bEstadoCorreo = ACCESOAPLI.CorreoActivado(null, 14);

            CAMBIOESTRUCTURAUSUARIO_AUX.DeleteMyAll(null, (int)Session["IDFICEPI_ENTRADA"]);

            #region Aparca en tabla auxiliar los datos a procesar
            foreach (string oUsuario in aDatos)
            {
                if (oUsuario == "") continue;
                string[] aUsuario = Regex.Split(oUsuario, "##");
                ///aUsuario[0] = idUsuario
                ///aUsuario[1] = idNodo_origen
                ///aUsuario[2] = idNodo_destino
                ///aUsuario[3] = Mes valor
                ///aUsuario[4] = procesado
                ///aUsuario[5] = codigo_excepcion

                bool? bProcesado = null;
                if (aUsuario[4] != "") bProcesado = (aUsuario[4] == "1") ? true : false;

                CAMBIOESTRUCTURAUSUARIO_AUX.Insertar(null, int.Parse(aUsuario[0]), (aUsuario[2] == "")? null: (int?)int.Parse(aUsuario[2]), int.Parse(aUsuario[3]), bProcesado, (int)Session["IDFICEPI_ENTRADA"]);
            }
            #endregion

            #region Procesa los datos
            foreach (string oUsuario in aDatos)
            {
                #region Grabación de cada usuario
                try
                {
                    if (oUsuario == "") continue;
                    string[] aUsuario = Regex.Split(oUsuario, "##");
                    ///aUsuario[0] = idUsuario
                    ///aUsuario[1] = idNodo_origen
                    ///aUsuario[2] = idNodo_destino
                    ///aUsuario[3] = Mes valor
                    ///aUsuario[4] = procesado  //1->verde, 0->rojo, ""->no procesado anteriormente
                    ///aUsuario[5] = codigo_excepcion
                    
                    nUsuario = int.Parse(aUsuario[0]);
                    nNodoOrigen = int.Parse(aUsuario[1]);
                    nNodoDestino = int.Parse(aUsuario[2]);
                    nAnomesValor = int.Parse(aUsuario[3]);
                    //bProcesado = false;

                    if (aUsuario[4] == "1" || aUsuario[1] == aUsuario[2])
                    {
                        CAMBIOESTRUCTURAUSUARIO_AUX.Modificar(null, nUsuario, nNodoDestino, nAnomesValor, true, "", (int)Session["IDFICEPI_ENTRADA"], null);
                        continue;
                    }
                    if (sPorDeadLockTimeout == "1" && aUsuario[4] == "0" && aUsuario[5] != "1505" && aUsuario[5] != "-2")
                    {
                        continue;
                    }

                    #region Abrir conexión y transacción
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

                    if (CAMBIOESTRUCTURAUSUARIO.HayConsumosMesesAbiertos(tr, nUsuario, nNodoOrigen, nAnomesValor))
                    {
                        CAMBIOESTRUCTURAUSUARIO_AUX.Modificar(tr, nUsuario, nNodoDestino, nAnomesValor, false, "Existen consumos en meses abiertos anteriores al mes valor.", (int)Session["IDFICEPI_ENTRADA"], null);
                        Conexion.CommitTransaccion(tr);
                        continue;
                    }

                    USUARIO.UpdateNodo(tr, nUsuario, nNodoDestino, nAnomesValor);

                    dsCB211 = CAMBIOESTRUCTURAUSUARIO.Caso_2_1_1(tr, nUsuario, nNodoOrigen, nNodoDestino, nAnomesValor);
                    dsCB212 = CAMBIOESTRUCTURAUSUARIO.Caso_2_1_2(tr, nUsuario, nNodoOrigen, nNodoDestino, nAnomesValor);
                    dsCB213 = CAMBIOESTRUCTURAUSUARIO.Caso_2_1_3(tr, nUsuario, nNodoOrigen, nNodoDestino, nAnomesValor);
                    CAMBIOESTRUCTURAUSUARIO.Caso_2_2_1(tr, nUsuario, nNodoOrigen, nNodoDestino, nAnomesValor);
                    CAMBIOESTRUCTURAUSUARIO.Caso_2_2_2(tr, nUsuario, nNodoOrigen, nNodoDestino, nAnomesValor);
                    dsCB223 = CAMBIOESTRUCTURAUSUARIO.Caso_2_2_3(tr, nUsuario, nNodoOrigen, nNodoDestino, nAnomesValor);
                    CAMBIOESTRUCTURAUSUARIO.Caso_2_3_1(tr, nUsuario, nNodoOrigen, nNodoDestino, nAnomesValor);
                    dsCB232 = CAMBIOESTRUCTURAUSUARIO.Caso_2_3_2(tr, nUsuario, nNodoOrigen, nNodoDestino, nAnomesValor);
                    dsCB24 = CAMBIOESTRUCTURAUSUARIO.Caso_2_4(tr, nUsuario, nNodoOrigen, nNodoDestino, nAnomesValor);
                    CAMBIOESTRUCTURAUSUARIO.Caso_2_5(tr, nUsuario, nNodoOrigen);
                    CAMBIOESTRUCTURAUSUARIO.Caso_2_6(tr, nUsuario, nNodoOrigen);
                    CAMBIOESTRUCTURAUSUARIO.Caso_2_7(tr, nUsuario, nAnomesValor);

                    //update proceso OK
                    CAMBIOESTRUCTURAUSUARIO_AUX.Modificar(tr, nUsuario, nNodoDestino, nAnomesValor, true, "", (int)Session["IDFICEPI_ENTRADA"], null);
                    
                    Conexion.CommitTransaccion(tr);

                    if (bEstadoCorreo) GenerarCorreoConsumosBorrados(dsCB211);
                    dsCB211.Dispose();
                    if (bEstadoCorreo) GenerarCorreoConsumosBorrados(dsCB212);
                    dsCB212.Dispose();
                    if (bEstadoCorreo) GenerarCorreoConsumosBorrados(dsCB213);
                    dsCB213.Dispose();
                    if (bEstadoCorreo) GenerarCorreoConsumosBorrados(dsCB223);
                    dsCB223.Dispose();
                    if (bEstadoCorreo) GenerarCorreoConsumosBorrados(dsCB232);
                    if (bEstadoCorreo) GenerarCorreoProduccionProfesionalBorrada(dsCB232);
                    dsCB223.Dispose();

                    if (bEstadoCorreo) GenerarCorreoConsumosBorrados(dsCB24);
                    dsCB24.Dispose();

                    if (aListCorreo.Count > 0)
                    {
                        Correo.EnviarCorreos(aListCorreo);
                        aListCorreo.Clear(); //hay que borrar los elementos enviados, porque la lista se utiliza para el siguiente usuario.
                    }
                }
                catch (Exception ex)
                {
                    Conexion.CerrarTransaccion(tr);
                    dsCB211 = null;
                    dsCB212 = null;
                    dsCB213 = null;
                    dsCB223 = null;
                    dsCB24 = null;
                    //update proceso KO
                    int? nError = null;
                    if (ex.GetType().ToString() == "System.Data.SqlClient.SqlException"){
                        nError = ((System.Data.SqlClient.SqlException)ex).Number;
                        if (nError == 1505 || nError == -2) //DeadLock o Timeout
                            bErrorDeadLockTimeout = true;
                    }

                    CAMBIOESTRUCTURAUSUARIO_AUX.Modificar(null, nUsuario, nNodoDestino, nAnomesValor, false, ex.Message, (int)Session["IDFICEPI_ENTRADA"], nError);
                    if (bErrorDeadLockTimeout) sResul = "OK@#@";
                    else sResul = "Error@#@" + Errores.mostrarError("Error al realizar el cambio de estructura de usuario.", ex);

                    if (ex.Message.IndexOf("servicio de mensajería") != -1)//Si se ha producido un error a la hora de enviar algún correo de consumos borrados.
                    {
                        aListCorreo.Clear(); //hay que borrar los elementos enviados, porque la lista se utiliza para el siguiente usuario.
                    }
                }
                finally
                {
                    Conexion.Cerrar(oConn);
                }
                #endregion
            }
            #endregion

            #region Recupera de la tabla auxiliar los datos procesados
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = CAMBIOESTRUCTURAUSUARIO_AUX.CatalogoDestino(null, (int)Session["IDFICEPI_ENTRADA"]);

            sb.Append("<table id='tblDatos2' class='texto MM' style='width: 560px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:240px;' /><col style='width:180px;' /><col style='width:100px; text-align:center;' /><col style='width:20px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("nodo_origen='" + dr["t303_idnodo_origen"].ToString() + "' ");
                sb.Append("nodo_destino='" + dr["t303_idnodo_destino"].ToString() + "' ");
                if (dr["t776_procesado"].ToString() == "") sb.Append("procesado='' ");
                else if ((bool)dr["t776_procesado"]) sb.Append("procesado='1' ");
                else sb.Append("procesado='0' ");
                sb.Append("excepcion=\"" + Utilidades.escape(dr["t776_excepcion"].ToString()) + "\" ");
                sb.Append("codigo_excepcion='" + dr["t776_codigoexcepcion"].ToString() + "' ");
                
                sb.Append("onclick='mm(event)' onmousedown='DD(event)' ");
                sb.Append("style='height:20px' >");
                sb.Append("<td></td>");
                //sb.Append("<td><nobr class='NBR W220' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Empresa:</label>" + dr["t313_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W220' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["Profesional"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W180' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["t776_anomes"].ToString() + "</td>");
                sb.Append("<td></td></tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            #endregion

            sResul = "OK@#@" + sb.ToString() + "@#@" + ((bErrorDeadLockTimeout)?"1":"0");
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al realizar el cambio de estructura de usuario.", ex);
        }
        return sResul;
    }

    private void GenerarCorreoConsumosBorrados(DataSet ds)
    {
        StringBuilder sbCB = new StringBuilder();
        string sAsunto = "Imputaciones IAP eliminadas por cambio de estructura.";
        string sTO = "";
        int nIdTarea = 0;

        if (ds.Tables[0].Rows.Count > 0)
        {
            //sTO = ds.Tables[0].Rows[0]["t001_codred"].ToString();
            // 19/01/2015: a Petición de Yolanda, no se comunican al usuario los consumos borrados. 
            // Se hará un proceso para insertar los consumos donde diga el CAU-DEF.
            sTO = ConfigurationManager.AppSettings["CorreoDEF"].ToString();

            bool bPrimero = true;
            int nFila = 0;
            //sbCB.Append(@"<BR>SUPER le informa de que se han eliminado imputaciones realizadas en IAP, debido a un cambio de estructura. Si tiene alguna duda, póngase en contacto con el CAU:<BR><BR>");
            sbCB.Append(@"<BR>SUPER le informa de que se han eliminado imputaciones de '" + ds.Tables[0].Rows[0]["Profesional"].ToString() + @"' realizadas en IAP, debido a un cambio de estructura.<BR><BR>");
            foreach (DataRow oCB in ds.Tables[0].Rows)
            {
                if (nIdTarea != (int)oCB["t332_idtarea"])
                {
                    nIdTarea = (int)oCB["t332_idtarea"];
                    nFila = 0;
                    if (!bPrimero)
                    {
                        sbCB.Append("</table>");
                        sbCB.Append("<table class='textoResultadoTabla' width='90%'>");
                        sbCB.Append("<tr><td>&nbsp;</td></tr>");
                        sbCB.Append("</table><br><br>");
                    }
                    else bPrimero = false;
                    sbCB.Append("<label style='width:120px'>Proyecto económico: </label>" + int.Parse(oCB["t301_idproyecto"].ToString()).ToString("#,###") + @" - " + Utilidades.unescape(oCB["t305_seudonimo"].ToString()) + "<br>");
                    sbCB.Append("<label style='width:120px'>Proyecto técnico: </label>" + Utilidades.unescape(oCB["t331_despt"].ToString()) + "<br>");
                    if (oCB["t334_desfase"].ToString() != "") sbCB.Append("<label style='width:120px'>Fase: </label>" + Utilidades.unescape(oCB["t334_desfase"].ToString()) + "<br>");
                    if (oCB["t335_desactividad"].ToString() != "") sbCB.Append("<label style='width:120px'>Fase: </label>" + Utilidades.unescape(oCB["t335_desactividad"].ToString()) + "<br>");
                    sbCB.Append("<label style='width:120px'>Tarea: </label><b>" + int.Parse(oCB["t332_idtarea"].ToString()).ToString("#,###") + @" - " + Utilidades.unescape(oCB["t332_destarea"].ToString()) + "</b><br><br>");

                    sbCB.Append("<table class='TBLINI' width='90%'>");
                    sbCB.Append("<colgroup><col style='width:70px;' /><col style='width:40px;' /><col /></colgroup>");
                    sbCB.Append("<tr align='center'><td>Fecha</td><td align='center'>Horas</td><td style='padding-left:5px;'>Comentario</td></tr>");
                    sbCB.Append("</table>");

                    sbCB.Append("<table class='texto' width='90%' cellpadding='2'>");
                    sbCB.Append("<colgroup><col style='width:70px;' /><col style='width:40px;' /><col /></colgroup>");
                }

                if (nFila % 2 == 0) sbCB.Append("<tr class='FA' ");
                else sbCB.Append("<tr class='FB' ");
                sbCB.Append(" style='valign:top;'><td>" + ((DateTime)oCB["t337_fecha"]).ToShortDateString() + "</td>");
                sbCB.Append("<td align='center'>" + double.Parse(oCB["t337_esfuerzo"].ToString()).ToString("N") + "</td>");
                sbCB.Append("<td style='padding-left:5px;'>" + oCB["t337_comentario"].ToString() + "</td></tr>");
                nFila++;
            }

            sbCB.Append("</table>");
            sbCB.Append("<table class='textoResultadoTabla' width='90%'>");
            sbCB.Append("<tr><td>&nbsp;</td></tr>");
            sbCB.Append("</table><br><br>");

            string[] aMail = { sAsunto, sbCB.ToString(), sTO };
            aListCorreo.Add(aMail);
            //Correo.EnviarCorreos(aListCorreo);
            sbCB.Length = 0;
        }
    }
    private void GenerarCorreoProduccionProfesionalBorrada(DataSet ds)
    {
        StringBuilder sbCB = new StringBuilder();
        string sAsunto = "Producción por profesional eliminada por cambio de estructura.";
        string sTO = "";
        int nIdTarea = 0;

        if (ds.Tables[1].Rows.Count > 0)
        {
            //sTO = ds.Tables[1].Rows[0]["t001_codred"].ToString();
            // 19/01/2015: a Petición de Yolanda, no se comunican al usuario los consumos borrados. 
            // Se hará un proceso para insertar los consumos donde diga el CAU-DEF.
            sTO = ConfigurationManager.AppSettings["CorreoDEF"].ToString();

            bool bPrimero = true;
            int nFila = 0;
            //sbCB.Append(@"<BR>SUPER le informa de que se ha eliminado producción por profesional, debido a un cambio de estructura. Si tiene alguna duda, póngase en contacto con el CAU:<BR><BR>");
            sbCB.Append(@"<BR>SUPER le informa de que se ha eliminado producción por profesional de '" + ds.Tables[1].Rows[0]["Profesional"].ToString() + @"', debido a un cambio de estructura.<BR><BR>");
            foreach (DataRow oCB in ds.Tables[1].Rows)
            {
                if (nIdTarea != (int)oCB["t332_idtarea"])
                {
                    nIdTarea = (int)oCB["t332_idtarea"];
                    nFila = 0;
                    if (!bPrimero)
                    {
                        sbCB.Append("</table>");
                        sbCB.Append("<table class='textoResultadoTabla' width='90%' cellpadding='0' cellspacing='0' border='0'>");
                        sbCB.Append("<tr><td>&nbsp;</td></tr>");
                        sbCB.Append("</table><br><br>");
                    }
                    else bPrimero = false;
                    sbCB.Append("<label style='width:120px'>Proyecto económico: </label>" + int.Parse(oCB["t301_idproyecto"].ToString()).ToString("#,###") + @" - " + Utilidades.unescape(oCB["t305_seudonimo"].ToString()) + "<br>");
                    sbCB.Append("<label style='width:120px'>Proyecto técnico: </label>" + Utilidades.unescape(oCB["t331_despt"].ToString()) + "<br>");
                    if (oCB["t334_desfase"].ToString() != "") sbCB.Append("<label style='width:120px'>Fase: </label>" + Utilidades.unescape(oCB["t334_desfase"].ToString()) + "<br>");
                    if (oCB["t335_desactividad"].ToString() != "") sbCB.Append("<label style='width:120px'>Fase: </label>" + Utilidades.unescape(oCB["t335_desactividad"].ToString()) + "<br>");
                    sbCB.Append("<label style='width:120px'>Tarea: </label><b>" + int.Parse(oCB["t332_idtarea"].ToString()).ToString("#,###") + @" - " + Utilidades.unescape(oCB["t332_destarea"].ToString()) + "</b><br><br>");

                    sbCB.Append("<table class='TBLINI' width='300px' cellpadding='0' cellspacing='0' border='0'>");
                    sbCB.Append("<colgroup><col style='width:100px;' /><col style='width:100px;' /><col style='width:100px;' /></colgroup>");
                    sbCB.Append("<tr align='center'><td style='text-align:right;'>Unidades</td><td style='text-align:right;'>Tarifa</td><td style='text-align:right;'>Importe</td></tr>");
                    sbCB.Append("</table>");

                    sbCB.Append("<table class='texto' width='300px' cellpadding='2' cellspacing='0' border='0'>");
                    sbCB.Append("<colgroup><col style='width:70px;' /><col style='width:40px;' /><col style='width:190px' /></colgroup>");
                }

                if (nFila % 2 == 0) sbCB.Append("<tr class='FA' style='vertical-align:top;'>");
                else sbCB.Append("<tr class='FB' style='vertical-align:top;'>");
                sbCB.Append("<td>" + double.Parse(oCB["t433_unidades"].ToString()).ToString("N") + "</td>");
                sbCB.Append("<td style='text-align:right;'>" + double.Parse(oCB["t333_imptarifa"].ToString()).ToString("N") + "</td>");
                sbCB.Append("<td style='padding-left:5px;'>" + double.Parse(oCB["importe"].ToString()).ToString("N") + "</td></tr>");
                nFila++;
            }

            sbCB.Append("</table>");
            sbCB.Append("<table class='textoResultadoTabla' width='90%' cellpadding='0' cellspacing='0' border='0'>");
            sbCB.Append("<tr><td>&nbsp;</td></tr>");
            sbCB.Append("</table><br><br>");

            string[] aMail = { sAsunto, sbCB.ToString(), sTO };
            aListCorreo.Add(aMail);
            //Correo.EnviarCorreos(aListCorreo);
            sbCB.Length = 0;
        }
    }
    protected string GenerarReplicasMeses()
    {
        string sResul = "";
        //bool bErrorDeadLockTimeout = false;
        try
        {
            //Este método es susceptible de bloqueo por lo que añado código para reintentos automáticos
            try{
                NODO.GenerarReplicasMesesCerrados();
                SEGMESPROYECTOSUBNODO.GenerarMesesEnReplicas();
                sResul = "OK@#@0";
            }
            catch (Exception ex)
            {
                int? nError = null;
                if (ex.GetType().ToString() == "System.Data.SqlClient.SqlException")
                {
                    nError = ((System.Data.SqlClient.SqlException)ex).Number;
                    if (nError == 1205 || nError == -2) //DeadLock o Timeout
                    {
                        //bErrorDeadLockTimeout = true;
                        //sResul = "OK@#@" + ((bErrorDeadLockTimeout) ? "1" : "0");
                        sResul = "OK@#@1";
                    }
                    else
                        sResul = "Error@#@Error al generar meses en réplicas. " + ex.Message;
                }
                else
                    sResul = "Error@#@Error al generar meses en réplicas. " + ex.Message;
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al generar meses en réplicas.", ex);
        }

        return sResul;
    }

}
