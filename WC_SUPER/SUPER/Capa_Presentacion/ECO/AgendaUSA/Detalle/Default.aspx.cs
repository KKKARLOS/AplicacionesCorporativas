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

using System.Text.RegularExpressions;
using System.Text;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "<table id='tblDatos' class='texto MANO' style='WIDTH:670px;'></table>";
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    SqlDataReader dr = null;
    private string[] aCorreoUSA;
    private string sCorreoUSA = "";
    private int iFecha;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }
                // Leer Agenda USA
                hdnProy.Text = Request.QueryString["nProy"].ToString();

                if (Request.QueryString["bNueva"] != "true")
                {
                    hdnID.Text = Request.QueryString["ID"].ToString();
                    AGENDAUSA oAGENDAUSA = AGENDAUSA.Select(null, int.Parse(hdnID.Text));

                    this.lblMes.InnerHtml = Fechas.AnnomesAFechaDescLarga(int.Parse(oAGENDAUSA.t641_anomes.ToString()));
                    txtConsumos.Text = oAGENDAUSA.t641_consumos;
                    txtProduccion.Text = oAGENDAUSA.t641_produccion;
                    txtFacturacion.Text = oAGENDAUSA.t641_facturacion;
                    txtOtros.Text = oAGENDAUSA.t641_otros;
                    iFecha = int.Parse(oAGENDAUSA.t641_anomes.ToString());
                }


                string[] aTabla = Regex.Split(obtenerComunicaciones(int.Parse(hdnProy.Text)), "@#@");
                if (aTabla[0] == "OK")
                {
                    strTablaHTML = aTabla[1];
                }
                else sErrores += Errores.mostrarError(aTabla[1]);
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la comunicación", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    private string obtenerComunicaciones(int iPE)
    {
        try
        {
            string sTexto = "";

            StringBuilder sb = new StringBuilder();
            dr = ESPACIOCOMUNICACION.Catalogo(iPE);

            sb.Append("<table id='tblDatos' class='texto MA'");
            sb.Append(" style='WIDTH: 670px;'>");
            sb.Append("<colgroup><col style='width:205px;' /><col style='width:65px;' /><col style='width:200px;' /><col style='width:180px;' /><col style='width:20px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDatos'>");

            string sContable = "";

            while (dr.Read())
            {
                if ((((bool)dr["t639_vigenciaproyecto"]) || (iFecha >= int.Parse(dr["t639_vigenciadesde"].ToString()) && iFecha <= int.Parse(dr["t639_vigenciahasta"].ToString()))
                   ) == false) continue;

                sb.Append("<tr id='" + dr["t639_idcomunicacion"].ToString() + "' onclick='ms(this)' ");

                sb.Append(" onDblClick='detalle(this.id);' style=' height:20px'>");
                sb.Append("<td  style='padding-left:5px;'><nobr class='NBR W190' onmouseover='TTip(event)' ondblclick='detalle(this.parentNode.parentNode.id);'>" + dr["Autor"].ToString() + "</nobr></td>");

                sContable = "";
                if ((bool)dr["t639_consumo"]) sContable += "C,";
                if ((bool)dr["t639_produccion"]) sContable += "P,";
                if ((bool)dr["t639_facturacion"]) sContable += "F,";
                if ((bool)dr["t639_otros"]) sContable += "O,";

                if (sContable.Length > 0) sContable = sContable.Substring(0, sContable.Length - 1);

                sb.Append("<td>" + sContable + "</td>");

                sTexto = dr["t639_descripcion"].ToString();
                sTexto = sTexto.Replace(((char)13).ToString() + ((char)10).ToString(), "<br>").Replace((char)34, (char)39);

                sb.Append("<td><nobr class='NBR W200' style='noWrap:true;' ");

                if (dr["t639_descripcion"].ToString() != "")
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTexto + "] hideselects=[off]\" ");
                sb.Append(" ondblclick='detalle(this.parentNode.parentNode.id);'>" + dr["t639_descripcion"].ToString() + "</nobr></td>");

                sTexto = dr["t639_observaciones"].ToString();
                sTexto = sTexto.Replace(((char)13).ToString() + ((char)10).ToString(), "</br>").Replace((char)34, (char)39);

                sb.Append("<td><nobr class='NBR W170' style='noWrap:true;' ");

                if (dr["t639_observaciones"].ToString() != "")
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[</br>" + sTexto + "] hideselects=[off]\" ");
                sb.Append(" ondblclick='detalle(this.parentNode.parentNode.id);'>" + dr["t639_observaciones"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:center;'>");
                if ((int)dr["docs"] > 0)
                    sb.Append("<img style='width:16px;' src='../../../../images/imgDocumento.gif' />");
                sb.Append("</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los espacios de comunicación", ex);
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
                sResultado += Grabar(
                                    byte.Parse(aArgs[1]),			                        // 0=UPDATE 1=INSERT
                                    int.Parse(aArgs[2]),                                    // Id AgendaUSA
                                    Utilidades.unescape(aArgs[3]),                        // Consumo
                                    Utilidades.unescape(aArgs[4]),                        // Producción
                                    Utilidades.unescape(aArgs[5]),                        // Facturación
                                    Utilidades.unescape(aArgs[6])                         // Otros
                                    );

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

    private string Grabar  (
                            byte byteNueva,
                            int iID, 
                            string sConsumo, 
                            string sProduccion, 
                            string sFacturacion, 
                            string sOtros
                            )
    {
        string sResul = "";
        int nID = -1;

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
            #region Datos Generales

/*
            if (byteNueva == 1)
            {
                nID = ESPACIOCOMUNICACION.Insert
                                (
                                tr,
                                int.Parse(hdnProy.Text),
                                null,
                                (int)Session["UsuarioActual"],
                                bConsumo,
                                bProduccion,
                                bFacturacion,
                                bOtros,
                                sDescripcion,
                                bVigencia,
                                iFechaDesde,
                                iFechaHasta,
                                ""
                                );
            }
            else //update
            {
 */
                AGENDAUSA.Update(
                                tr,
                                iID,
                                sConsumo,
                                sProduccion,
                                sFacturacion,
                                sOtros
                                );
//            }
            


            #endregion

            Conexion.CommitTransaccion(tr);

            // Notificación de correo

/*
            dr = null;
            dr = PROYECTO.getSoporteAdministrativo(null, int.Parse(hdnProy.Text));
            
            while (dr.Read())
            {
                if (dr["t301_codred_SAT"].ToString() != "") sCorreoUSA += dr["t301_codred_SAT"].ToString() + ',';
                if (dr["t301_codred_SAA"].ToString() != "") sCorreoUSA += dr["t301_codred_SAA"].ToString() + ',';
            }

            dr.Close();
            dr.Dispose();

            if (sCorreoUSA != "")
            {   
                int iAviso;
                if (byteNueva == 1) iAviso=nID;
                else iAviso = iID;

                CorreoDestinatariosUSA(iAviso, byteNueva);
            }
*/
            sResul = "OK@#@" + nID.ToString("#,###");
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del espacio de comunicación", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    public void CorreoDestinatariosUSA(int idcomunicacion,byte byteNueva)
    {
        string sVigencia = "";
//        string sEstado = "";
        string sTexto = "";
        iFecha = 0;

        ArrayList aListCorreo = new ArrayList();
        StringBuilder sbuilder = new StringBuilder();
//        string sMensaje = "";
        string sAsunto = "";
        string sTO;

        ESPACIOCOMUNICACION oESPACIOCOMUNICACION = ESPACIOCOMUNICACION.Select(null, idcomunicacion);
        
        if (byteNueva == 1) sAsunto = "Creación de un nuevo aviso para el proyecto '" + oESPACIOCOMUNICACION.t301_denominacion + "'.";
        else sAsunto = "Modificación de un aviso del proyecto '" + oESPACIOCOMUNICACION.t301_denominacion + "'.";

        sbuilder.Append(@"<BR>SUPER le informa de la ");

        if (byteNueva == 1) sbuilder.Append(@"creación de la siguiente comunicación por parte del usuario: "+Session["DES_EMPLEADO_ENTRADA"]+" ");
        else sbuilder.Append(@"modificación de la siguiente comunicación por parte del usuario: "+Session["DES_EMPLEADO_ENTRADA"]+" ");
        sbuilder.Append(@"<BR><BR>Los datos de la comunicación son:<BR><BR><BR>");

        sbuilder.Append("<label style='width:120px'>Fecha de aviso: </label>" + oESPACIOCOMUNICACION.t639_fechacom + "<br>");
        sbuilder.Append("<label style='width:120px'>Autor: </label>" + oESPACIOCOMUNICACION.autor + "<br>");

        string sPartidasConta = "";

        if ((bool)oESPACIOCOMUNICACION.t639_consumo) sPartidasConta += "Consumo,";
        if ((bool)oESPACIOCOMUNICACION.t639_produccion) sPartidasConta += "Producción,";
        if ((bool)oESPACIOCOMUNICACION.t639_facturacion) sPartidasConta += "Facturación,";
        if ((bool)oESPACIOCOMUNICACION.t639_facturacion) sPartidasConta += "Otros,";

        if (sPartidasConta.Length > 0) sPartidasConta = sPartidasConta.Substring(0, sPartidasConta.Length - 1);

        sbuilder.Append("<label style='width:120px'>Partidas contables: </label>" + sPartidasConta + "<br>");

        sTexto = oESPACIOCOMUNICACION.t639_descripcion;

        sTexto = sTexto.Replace(((char)13).ToString() + ((char)10).ToString(), "<br>").Replace((char)34, (char)39);

        sbuilder.Append("<label style='width:120px'>Descripción: </label>" + sTexto + "<br>");

        if ((bool)oESPACIOCOMUNICACION.t639_vigenciaproyecto) sVigencia = "Todo el proyecto";
        else sVigencia = Fechas.AnnomesAFechaDescCorta((int)oESPACIOCOMUNICACION.t639_vigenciadesde) + " - " + Fechas.AnnomesAFechaDescCorta((int)oESPACIOCOMUNICACION.t639_vigenciahasta);

        sbuilder.Append("<label style='width:120px'>Vigencia: </label>" + sVigencia + "<br>");

        sTexto = oESPACIOCOMUNICACION.t639_observaciones;
        sTexto = sTexto.Replace(((char)13).ToString() + ((char)10).ToString(), "<br>").Replace((char)34, (char)39);

        sbuilder.Append("<label style='width:120px'>Observaciones: </label>" + sTexto + "<br>");

        iFecha = DateTime.Now.Year * 100 + DateTime.Now.Month;

        //if ((bool)oESPACIOCOMUNICACION.t639_vigenciaproyecto) sEstado = "Abierto";
        //else if (iFecha >= int.Parse(oESPACIOCOMUNICACION.t639_vigenciadesde.ToString()) && iFecha <= int.Parse(oESPACIOCOMUNICACION.t639_vigenciahasta.ToString()))
        //    sEstado = "Abierto";
        //else sEstado = "Cerrado";

        sbuilder.Append("<label style='width:120px'>Estado: </label>" + sTexto + "<br>");


        aCorreoUSA = Regex.Split(sCorreoUSA, ",");

        for (int j = 0; j < aCorreoUSA.Length; j++)
        {
            if (aCorreoUSA[j] == "") continue;

            string[] aID = Regex.Split(aCorreoUSA[j], "/");

            sTO = aID[0];
            sTexto = sbuilder.ToString();

            string[] aMail = { sAsunto, sTexto, sTO };
            if (sTO != "") aListCorreo.Add(aMail);
        }

        Correo.EnviarCorreos(aListCorreo);
    }
}
