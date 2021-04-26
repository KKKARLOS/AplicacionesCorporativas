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

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    //public string sErrores = "";
    public string strTablaHTML = "<table id='tblDatos' class='texto MANO' style='WIDTH: 940px; table-layout:fixed; BORDER-COLLAPSE: collapse;' cellSpacing='0' cellPadding='0' border='0' mantenimiento='1'></table>";
    SqlDataReader dr = null;
    private ArrayList aListCorreo = new ArrayList();
    private string sMensaje = "";
    private string sTexto = "";
    private string sAsunto = "";
    private string sTO;
    private string[] aCorreoUSA;
    private string sCorreoUSA = "";
    private int iCabecera = 0;
    private StringBuilder sb = new StringBuilder();
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.bFuncionesLocales = true;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            switch (Request.QueryString["origen"])
            {
                case "menu_espacio":
                    Master.sbotonesOpcionOn = "4";
                    Master.sbotonesOpcionOff = "4";
                    break;
                default:
                    Master.sbotonesOpcionOn = "4,6";
                    Master.sbotonesOpcionOff = "4";
                break;
            }

            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.TituloPagina = "Espacio de comunicación";

            try
            {
                //hdnProy.Text = Request.QueryString["nProy"].ToString();
                //hdnProyDen.Text = Request.QueryString["dProy"].ToString();
                //hdnUSA.Text = Request.QueryString["USA"].ToString();

                if (Session["ID_PROYECTOSUBNODO"] != null && Session["ID_PROYECTOSUBNODO"].ToString() != "")
                {
                    hdnIdProyectoSubNodo.Text = Session["ID_PROYECTOSUBNODO"].ToString();
                    string[] aTabla = Regex.Split(obtenerComunicaciones(int.Parse(Session["ID_PROYECTOSUBNODO"].ToString())), "@#@");
                    if (aTabla[0] == "OK")
                    {
                        strTablaHTML = aTabla[1];
                        txtNumPE.Text =  aTabla[2];
                        txtDesPE.Text = aTabla[3];
                        hdnUSA.Text = aTabla[4];
                        hdnProyUSA.Text = aTabla[5];
                        hdnProyExternalizable.Text = aTabla[6];
                    }
                    else Master.sErrores += Errores.mostrarError(aTabla[1]);
                }
            }
            catch (Exception ex)
            {
                Master.sErrores += Errores.mostrarError("Error al obtener los datos", ex);
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
                sResultado += Grabar(int.Parse(aArgs[1]), aArgs[2]);
                break;
            case ("getComunicaciones"):
                sResultado += obtenerComunicaciones(int.Parse(aArgs[1]));
                break;
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
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

    private string obtenerComunicaciones(int iPSN)
    {
        try
        {
            string sNumPE = "", sDesc = "", sUSA = "N", sProyUSA = "N", sProyExternalizable = "N";
            string sContable = "", sVigencia = "", sEstado = "";
            int iFecha = DateTime.Now.Year * 100 + DateTime.Now.Month, idProy=-1;

            SqlDataReader dr2 = PROYECTO.fgGetDatosProy(iPSN);
            if (dr2.Read())
            {
                idProy = int.Parse(dr2["t301_idproyecto"].ToString());
                sNumPE = int.Parse(dr2["t301_idproyecto"].ToString()).ToString("###,###");
                sDesc = dr2["t301_denominacion"].ToString();
                if (dr2["t305_cualidad"].ToString() == "C" && 
                    dr2["t301_estado"].ToString() == "A" && 
                    (Session["UsuarioActual"].ToString() == dr2["t314_idusuario_SAT"].ToString() || 
                        Session["UsuarioActual"].ToString() == dr2["t314_idusuario_SAA"].ToString()))
                    sUSA = "S";
                if (dr2["t314_idusuario_SAT"].ToString() != "0" || dr2["t314_idusuario_SAA"].ToString() != "0") sProyUSA = "S";
                sProyExternalizable = ((bool)dr2["t301_externalizable"]) ? "S" : "N";  
            }
            dr2.Close();
            dr2.Dispose();

            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 960px;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:10px;' />");
            sb.Append("     <col style='width:60px;' />");
            sb.Append("     <col style='width:205px;' />");
            sb.Append("     <col style='width:70px;' />");
            sb.Append("     <col style='width:200px;' />");
            sb.Append("     <col style='width:125x;' />");
            sb.Append("     <col style='width:175px;' />");
            sb.Append("     <col style='width:25px;' />");
            sb.Append("     <col style='width:70px;' />");
            sb.Append("     <col style='width:20px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody id='tbodyDatos'>");

            SqlDataReader dr = ESPACIOCOMUNICACION.Catalogo(idProy);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t639_idcomunicacion"].ToString() + "' bd='' onclick='mm(event)' onDblClick='detalle(this.id,event);' ");

                if (sUSA == "S")
                    sb.Append(" obs=\"" + Utilidades.escape(dr["t639_observaciones"].ToString()) + "\" ");

                sb.Append(" style=' height:20px'>");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");

                sb.Append("<td style='text-align:right; padding-right:3px;'>" + DateTime.Parse(dr["t639_fechacom"].ToString()).ToShortDateString() + "</td>");

                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W190' onmouseover='TTip(event)'>" + dr["Autor"].ToString() + "</nobr></td>");

                sContable = "";
                if ((bool)dr["t639_consumo"]) sContable += "C,";
                if ((bool)dr["t639_produccion"]) sContable += "P,";
                if ((bool)dr["t639_facturacion"]) sContable += "F,";
                if ((bool)dr["t639_otros"]) sContable += "O,";

                if (sContable.Length > 0) sContable = sContable.Substring(0, sContable.Length - 1);

                sb.Append("<td>" + sContable + "</td>");
                sb.Append("<td><nobr class='NBR W190' onDblClick='detalle(this.parentNode.parentNode.id,event);' ");

                sb.Append("style='noWrap:true;' ");
                if (dr["t639_descripcion"].ToString()!="")
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + Utilidades.CadenaParaTooltipExtendido(dr["t639_descripcion"].ToString()) + "] hideselects=[off]\" ");
                sb.Append(">" + dr["t639_descripcion"].ToString() + "</nobr></td>");
                sVigencia="";
                sEstado="";

                if ((bool)dr["t639_vigenciaproyecto"]) sVigencia = "Todo el proyecto";
                else sVigencia = Fechas.AnnomesAFechaDescCorta(int.Parse(dr["t639_vigenciadesde"].ToString())) + " - " + Fechas.AnnomesAFechaDescCorta(int.Parse(dr["t639_vigenciahasta"].ToString()));

                sb.Append("<td>" + sVigencia + "</td>");
                //sb.Append("<td><nobr class='NBR W160' onDblClick='detalle(this.parentNode.parentNode.id);' style='noWrap:true;height:16px' ");
                sb.Append("<td><nobr class='NBR W160' onDblClick='detalle(this.parentNode.parentNode.id,event);' style='noWrap:true;' ");
                if (dr["t639_observaciones"].ToString() != "")
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + Utilidades.CadenaParaTooltipExtendido(dr["t639_observaciones"].ToString()) + "] hideselects=[off]\" ");
                sb.Append(">" + dr["t639_observaciones"].ToString() + "</nobr></td>");

                sb.Append("<td style='text-align:center;'>");
                if (sUSA == "S")
                {
                    sb.Append("<image style='width:16px;' class='MA' ondblclick='cargarObserva(this.parentNode.parentNode);' src='../../../images/imgComentario.gif'></image>");
                }
                else
                {
                    sb.Append("<image style='width:16px;' src='../../../images/imgSeparador.gif'></image>");
                }
                sb.Append("</td>");


                if ((bool)dr["t639_vigenciaproyecto"]) sEstado = "Abierto";
                else if (iFecha >= int.Parse(dr["t639_vigenciadesde"].ToString()) && iFecha <= int.Parse(dr["t639_vigenciahasta"].ToString()))
                    sEstado = "Abierto";
                else if (iFecha < int.Parse(dr["t639_vigenciadesde"].ToString()))
                    sEstado = "Pendiente";
                else sEstado = "Cerrado";

                sb.Append("<td style='text-align:center;'>");
                if (sEstado == "Abierto") sb.Append("<img style='width:16px;' src='../../../images/imgSI.gif' />");
                else if (sEstado == "Pendiente") sb.Append("<img style='width:16px;' src='../../../images/imgPendiente.gif' />");
                else sb.Append("<img style='width:16px;' src='../../../images/imgNO.gif' />");   
                sb.Append("</td>");
                sb.Append("<td style='text-align:center;'>");
                if ((int)dr["docs"] > 0)
                    sb.Append("<img style='width:16px;' src='../../../images/imgDocumento.gif' />");
                sb.Append("</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString() + "@#@" + sNumPE + "@#@" + sDesc + "@#@" + sUSA + "@#@" + sProyUSA + "@#@" + sProyExternalizable;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los espacios de comunicación", ex);
        }
    }
    protected string Grabar(int iProy, string strDatos)
    {
        string sResul = "";
//        int nAux = 0;

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
             
        try
        {
            string[] aComunicacion = Regex.Split(strDatos, "///");
            foreach (string oComunicacion in aComunicacion)
            {
                if (oComunicacion == "") continue;
                string[] aValores = Regex.Split(oComunicacion, "##");
                //0. Opcion BD. "D"
                //1. ID Comunicacion
                //2. Descripción


                switch (aValores[0])
                {
                   case "D":
                        if (iCabecera == 0) CabeceraCorreoUSA();
                        DetalleCorreoUSA(int.Parse(aValores[1]), aValores[0]);
                        ESPACIOCOMUNICACION.Delete(tr, int.Parse(aValores[1]));
                        break;
                    case "U":
                        ESPACIOCOMUNICACION.Update(tr, int.Parse(aValores[1]), Utilidades.unescape(aValores[2]));
                        //DetalleCorreoUSA(int.Parse(aValores[1]), aValores[0]);
                        break;
                }
            }
            Conexion.CommitTransaccion(tr);

            if (iCabecera == 1) EnviarCorreoUSA(iProy);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar las Comunicaciones.", ex) + "@#@";
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

    public void CabeceraCorreoUSA()
    {
        iCabecera = 1;
        sAsunto = "Espacio de comunicación.";
        sb.Append("<BR>SUPER le informa que " + Session["NOMBRE"] + ' ' + Session["APELLIDO1"] + ' ' + Session["APELLIDO2"] + ", ha eliminado la/s siguiente/s comunicación/es:</br></br>");
    }
    public void DetalleCorreoUSA(int idcomunicacion, string sCaso)
    {
        string sVigencia = "";
        string sEstado = "";
        int iFecha = 0;

        ESPACIOCOMUNICACION oESPACIOCOMUNICACION = ESPACIOCOMUNICACION.Select(null, idcomunicacion);

        sb.Append("</br></br><table id='tblContenido' style='WIDTH:1200px' class='texto' border='0' cellspacing='0' cellpadding='0'>");
        sb.Append("<tr><td style='width:120px; vertical-align:top;'>");
        sb.Append("<LABEL class='TITULO'>Proyecto:</LABEL>");
        sb.Append("</td><td>");
        sb.Append(int.Parse(oESPACIOCOMUNICACION.t301_idproyecto.ToString()).ToString("###,###") + " - " + oESPACIOCOMUNICACION.t301_denominacion + "</br></br>");
        sb.Append("</td></tr>");

        sb.Append("<tr><td style='vertical-align:top;'>");
        sb.Append("<LABEL class='TITULO'>Autor:</LABEL>");
        sb.Append("</td><td>");
        sb.Append(oESPACIOCOMUNICACION.autor + "</br></br>");
        sb.Append("</td></tr>");

        sb.Append("<tr><td style='vertical-align:top;'>");
        sb.Append("<LABEL class='TITULO'>Fecha de aviso:</LABEL>");
        sb.Append("</td><td>");
        sb.Append(oESPACIOCOMUNICACION.t639_fechacom.ToShortDateString() + "</br></br>");
        sb.Append("</td></tr>");

        sb.Append("<tr><td style='vertical-align:top;'>");
        sb.Append("<LABEL class='TITULO'>Partidas contables:</LABEL>");
        sb.Append("</td><td>");

        string sPartidasConta = "";

        if ((bool)oESPACIOCOMUNICACION.t639_consumo) sPartidasConta += "Consumo, ";
        if ((bool)oESPACIOCOMUNICACION.t639_produccion) sPartidasConta += "Producción, ";
        if ((bool)oESPACIOCOMUNICACION.t639_facturacion) sPartidasConta += "Facturación, ";
        if ((bool)oESPACIOCOMUNICACION.t639_facturacion) sPartidasConta += "Otros, ";

        if (sPartidasConta.Length > 0) sPartidasConta = sPartidasConta.Substring(0, sPartidasConta.Length - 2);

        sb.Append(sPartidasConta + "</br></br>");
        sb.Append("</td></tr>");

        sb.Append("<tr><td style='vertical-align:top;'>");
        sb.Append("<LABEL class='TITULO'>Vigencia:</LABEL>");
        sb.Append("</td><td style='vertical-align:top;'>");

        if ((bool)oESPACIOCOMUNICACION.t639_vigenciaproyecto) sVigencia = "Todo el proyecto";
        else sVigencia = Fechas.AnnomesAFechaDescLarga((int)oESPACIOCOMUNICACION.t639_vigenciadesde) + " - " + Fechas.AnnomesAFechaDescLarga((int)oESPACIOCOMUNICACION.t639_vigenciahasta);

        sb.Append(sVigencia + "</br></br>");
        sb.Append("</td></tr>");

        sb.Append("<tr><td style='vertical-align:top;'>");
        sb.Append("<LABEL class='TITULO'>Estado:</LABEL>");
        sb.Append("</td><td>");

        iFecha = DateTime.Now.Year * 100 + DateTime.Now.Month;

        if ((bool)oESPACIOCOMUNICACION.t639_vigenciaproyecto) sEstado = "Abierto";
        else if (iFecha >= int.Parse(oESPACIOCOMUNICACION.t639_vigenciadesde.ToString()) && iFecha <= int.Parse(oESPACIOCOMUNICACION.t639_vigenciahasta.ToString()))
            sEstado = "Abierto";
        else sEstado = "Cerrado";

        sb.Append(sEstado + "</br></br>");
        sb.Append("</td></tr>");

        sb.Append("<tr><td style='vertical-align:top;'>");
        sb.Append("<LABEL class='TITULO'>Descripción:</LABEL>");
        sb.Append("</td><td>");
        sTexto = oESPACIOCOMUNICACION.t639_descripcion;

        sTexto = sTexto.Replace(((char)13).ToString() + ((char)10).ToString(), "<br>").Replace((char)34, (char)39);

        sb.Append(sTexto + "</br></br>");
        sb.Append("</td></tr>");

        sb.Append("<tr><td style='vertical-align:top;'>");
        sb.Append("<LABEL class='TITULO'>Observaciones:</LABEL>");
        sb.Append("</td><td>");
        sTexto = oESPACIOCOMUNICACION.t639_observaciones;

        sTexto = sTexto.Replace(((char)13).ToString() + ((char)10).ToString(), "<br>").Replace((char)34, (char)39);

        sb.Append(sTexto + "</br></br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td colspan='2' class='punteado'>&nbsp;");
        sb.Append("</td></tr>");

        sb.Append("</table>");	
    }
    public void EnviarCorreoUSA(int iProy)
    {
        dr = null;
        dr = PROYECTO.getSoporteAdministrativo(null, iProy);
        sCorreoUSA = "";

        while (dr.Read())
        {
            if (dr["t301_codred_SAT"].ToString() != "") sCorreoUSA += dr["t301_codred_SAT"].ToString() + ',';
            if (dr["t301_codred_SAA"].ToString() != "") sCorreoUSA += dr["t301_codred_SAA"].ToString() + ',';
        }

        dr.Close();
        dr.Dispose();

        if (sCorreoUSA != "")
        {
            aCorreoUSA = Regex.Split(sCorreoUSA, ",");
            for (int j = 0; j < aCorreoUSA.Length; j++)
            {
                if (aCorreoUSA[j] == "") continue;

                string[] aID = Regex.Split(aCorreoUSA[j], "/");

                sTO = aID[0];
                sMensaje = sb.ToString();
                string[] aMail = { sAsunto, sMensaje, sTO };
                if (sTO != "") aListCorreo.Add(aMail);
            }
            Correo.EnviarCorreos(aListCorreo);
        }
    }
    private string buscarPE(string sNumProyecto)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerProyectosByNumPE("pge", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], false, false, false);
            bool sw = false;
            while (dr.Read())
            {
                if (!sw)
                {
                    Session["ID_PROYECTOSUBNODO"] = dr["t305_idproyectosubnodo"].ToString();
                    Session["MODOLECTURA_PROYECTOSUBNODO"] = (dr["modo_lectura"].ToString() == "1") ? true : false;
                    Session["RTPT_PROYECTOSUBNODO"] = false;
                    Session["MONEDA_PROYECTOSUBNODO"] = dr["t422_idmoneda_proyecto"].ToString();
                    sw = true;
                }
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "##");
                sb.Append(dr["modo_lectura"].ToString() + "##");
                sb.Append(dr["rtpt"].ToString() + "##");
                sb.Append(dr["t301_denominacion"].ToString() + "///");
                
            }

            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al recuperar los datos del proyecto", ex);
        }
        return sResul;
    }
}
