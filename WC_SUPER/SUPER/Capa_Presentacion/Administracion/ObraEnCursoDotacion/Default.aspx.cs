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
    public string strTablaHTML="";
    public int nAnoMes = DateTime.Now.Year * 100 + DateTime.Now.Month;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 43;
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Ajuste-Dotación obra en curso";
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
/*
                string[] aTabla = Regex.Split(cargarNodos(nAnoMes, int.Parse(txtMesesAntig.Text)), "@#@");
                if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                else Master.sErrores += Errores.mostrarError(aTabla[1]);
*/                
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
            case ("procesar"):
                sResultado += Procesar(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("cambiarMes"):
            case ("getNodos"):
                sResultado += cargarNodos(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
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

    private string cargarNodos(int nAnoMes, int nMeses)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = NODO.CatalogoObraEnCursoDotacion(nAnoMes, nMeses);
            string sTootTip = "";

            sb.Append("<table id='tblNodos' class='texto MANO' style='width: 600px;'>");
            sb.Append("<colgroup><col style='width:50px;' /><col style='width:70px;' /><col style='width:400px;' /><col style='width:80px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sTootTip = "";
                if (Utilidades.EstructuraActiva("SN4")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["t394_denominacion"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["t393_denominacion"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["t392_denominacion"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN1")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["t391_denominacion"].ToString();

                sb.Append("<tr id='" + dr["t303_idnodo"].ToString() + "' pocar=" + dr["num_proy_con_obra_en_curso"].ToString() + " style='height:20px;'>");
                sb.Append("<td align='center'><input type='checkbox' class='check' onclick='setEstadistica()'></td>");
                sb.Append("<td style='text-align:right; padding-right:3px'>" + dr["t303_idnodo"].ToString() + "</td>");
                sb.Append("<td style='padding-left:8px;'><nobr class='NBR W340' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Estructura] body=[" + sTootTip + "] hideselects=[off]\">" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                if ((int)dr["num_proy_con_obra_en_curso"] > 0) sb.Append("<td style='padding-right:5px;text-align:center' class='MA' ondblclick='getPOC(this);'>" + dr["num_proy_con_obra_en_curso"].ToString() + "</td>");
                else sb.Append("<td style='padding-right:5px;text-align:center'></td>");

                //sb.Append("<td style='text-align:right; padding-right:2px;'>" + int.Parse(dr["num_proy_con_obra_en_curso"].ToString()).ToString("#,###") + "</td>");
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
            return "Error@#@" + Errores.mostrarError("Error al obtener los nodos.", ex);
        }
    }

    private string Procesar(string sAnoMes, string sMeses, string strNodos)
    {
        string sResul = "";
        ArrayList aListCorreo = new ArrayList();

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

            PROYECTOSUBNODO.EliminarObraEnCursoDotacion(tr, int.Parse(sAnoMes), strNodos);
            DataSet ds = PROYECTOSUBNODO.ObtenerProyectosObraEnCursoDotacion(tr, int.Parse(sAnoMes), int.Parse(sMeses), strNodos);
            int idT325 = 0;
            foreach (DataRow oPSN in ds.Tables[0].Rows)
            {

                idT325 = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, (int)oPSN["t305_idproyectosubnodo"], int.Parse(sAnoMes));
                if (idT325 == 0) idT325 = SEGMESPROYECTOSUBNODO.InsertSiNoExiste(tr, (int)oPSN["t305_idproyectosubnodo"], int.Parse(sAnoMes));
                
                //Insertamos en el año-mes indicado el importe del saldo obra en curso en negativo.
                DATOECO.Insert(tr, idT325, Constantes.nIdClaseDotacionObraEnCurso, "Dotación-Ajuste Obra en Curso", decimal.Parse(oPSN["Obra_Curso_Acum"].ToString()) * -1, null, null, null);
                //Registramos en la cola de correo
                EncolarCorreo(  aListCorreo,
                                oPSN["codred_gestorprod"].ToString(), 
                                oPSN["codred_comercialhermes"].ToString(), 
                                decimal.Parse(oPSN["Obra_Curso_Acum"].ToString()).ToString("N"), 
                                int.Parse(oPSN["t306_idcontrato"].ToString()).ToString("#,###"),                                 
                                oPSN["t377_denominacion"].ToString(), 
                                int.Parse(oPSN["t301_idproyecto"].ToString()).ToString("#,###"), 
                                oPSN["t301_denominacion"].ToString(),
                                sMeses.ToString()
                             );
            }
            ds.Dispose();

        //    Conexion.CommitTransaccion(tr); 
        Conexion.CerrarTransaccion(tr);
            EnviarCorreos(aListCorreo);
            sResul = "OK";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al realizar el ajuste de la dotación de la obra en curso.", ex, false);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private static void EnviarCorreos(ArrayList aListCorreo)
    {
        SUPER.Capa_Negocio.Correo.EnviarCorreos(aListCorreo);
    }
    /// <summary>
    /// Añade un elemento a la lista de correos a enviar
    /// </summary>
    /// <param name="aListCorreo"></param>
    /// <param name="codred_gestorprod"></param>
    /// <param name="codred_comercialhermes"></param>
    /// <param name="Obra_Curso_Acum"></param>
    /// <param name="t306_idcontrato"></param>
    /// <param name="t377_denominacion"></param>
    /// <param name="t301_idproyecto"></param>
    /// <param name="t301_denominacion"></param>
    /// <param name="sMeses"></param>
    
    /// 
     private static void EncolarCorreo(ArrayList aListCorreo, string codred_gestorprod, string codred_comercialhermes, string Obra_Curso_Acum, string t306_idcontrato, string t377_denominacion, string t301_idproyecto, string t301_denominacion, string sMeses)
    {
        string sTO = codred_gestorprod;
        string sAsunto = "Ajuste de proyectos con obras antiguas";
        string sTexto = "";
        StringBuilder sbuilder = new StringBuilder();

        sbuilder.Append(@"<br />SUPER le informa de que tiene un proyecto con saldo de obra en curso con una antigüedad igual o superior a " + sMeses + " meses.");
        sbuilder.Append("<br /><br /><p>En este sentido, te recordamos que, de acuerdo a la normativa vigente de producción, los saldos de obra en curso con una antigüedad superior a " + sMeses + " meses deben ser dotados. A estos efectos, el cálculo de la antigüedad se realiza en función del número de meses sin que el proyecto haya tenido registros de producción.</p>");
        sbuilder.Append("<p>Se trata del proyecto '" + t301_idproyecto + "-" + t301_denominacion + "' y oportunidad '" + t306_idcontrato + "-" + t377_denominacion + "' con un saldo actual de " + Obra_Curso_Acum + " € que se ha procedido a dotar.</p>");
        sbuilder.Append("<p>Este saldo podrá volver a registrarse si posteriormente se factura o si disponemos de un plan de facturación y cobros firmado por el cliente con el objeto de que sirva de justificación ante la probable circularización de clientes por parte de los auditores.</p>");
        sbuilder.Append("<p>Revisa por favor la situación de tu proyecto para tomar las medidas que consideres oportunas de cara a su facturación.</p>");
        sbuilder.Append("<p>Para cualquier consulta, puedes ponerte en contacto con CAU-DEF.</p>");
        sbuilder.Append("<p>Saludos.</p>");

        sTexto = sbuilder.ToString();
        string[] aMail = { sAsunto, sTexto, sTO };
        aListCorreo.Add(aMail);
        
        sTO = codred_comercialhermes;

        string[] aMail2 = { sAsunto, sTexto, sTO };
        aListCorreo.Add(aMail);
    }

}
