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
    public string sErrores = "", sLectura = "false", sIDDocuAux = "";
    public int nIdSolicitud = 0;
    public SqlConnection oConn;
    public SqlTransaction tr;

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
                this.hdnTipo.Value = Utilidades.decodpar(Request.QueryString["t"].ToString());
                if (Request.QueryString["c"] != null && (Request.QueryString["c"] != ""))
                    this.hdnIdCert.Value = Utilidades.decodpar(Request.QueryString["c"].ToString());
                if (Request.QueryString["f"] != null && Request.QueryString["f"].ToString() != "-1")
                    this.hdnIdFicepiExamen.Value = Utilidades.decodpar(Request.QueryString["f"].ToString());
                else
                    this.hdnIdFicepiExamen.Value = Session["IDFICEPI_CVT_ACTUAL"].ToString();
                //Recojo si el certificado tiene examenes sin borrado pdte
                if (Request.QueryString["te"] != null && (Request.QueryString["te"] != ""))
                    this.hdnHayExamenes.Value = Utilidades.decodpar(Request.QueryString["te"]);

                sIDDocuAux = "SUPER-" + Session["IDFICEPI_CVT_ACTUAL"].ToString() + "-" + DateTime.Now.Ticks.ToString();
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la cabecera de la orden", ex);
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
                sResultado += Grabar(aArgs[1]);
                break;
            case ("documentos"):
                sResultado += getDocumento(aArgs[1]);
                break;
            //case ("borrarDoc"):
            //    sResultado += BorrarDocumento(aArgs[1]);
            //    break;
            //case ("elimdocs"):
            //    sResultado += EliminarDocumentos(aArgs[1]);
            //    break;
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    private string Grabar(string strDatos)
    {
        string sResul = "";
        bool bErrorControlado = false;

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
            string[] aDatos = Regex.Split(strDatos, "##");

            if (nIdSolicitud == 0)
            {
                //sIDDocuAux = "SUPER-" + Session["IDFICEPI_ENTRADA"].ToString() + "-" + DateTime.Now.Ticks.ToString();
                int? IdCertificado = null;
                if (aDatos[4] != "" && aDatos[4] != "-1")
                    IdCertificado = int.Parse(aDatos[4]);
                nIdSolicitud= SUPER.BLL.SOLICITUD.Insertar(tr,aDatos[0],Utilidades.unescape(aDatos[1]),Utilidades.unescape(aDatos[2]),
                                                           int.Parse(aDatos[5]), IdCertificado, aDatos[3], "C");
            }

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + nIdSolicitud.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);

            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de la solicitud", ex);
            else sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    
    private string getDocumento(string t696_id)
    {
        try
        {
            string sNomArchivo = "OK@#@";
            bool bPrimero = true;
            SqlDataReader dr;
            if (Utilidades.isNumeric(t696_id))
                dr = SUPER.BLL.DOCSOLICITUD.Catalogo(int.Parse(t696_id));
            else
                dr = SUPER.BLL.DOCSOLICITUD.CatalogoByUsuTicks(t696_id);
            //Si hay mas de un registro quiere decir que hemos subido un archivo y luego otro
            //Como solo debe quedar el último, eliminamos los anteriores
            while (dr.Read())
            {
                if (bPrimero)
                    sNomArchivo += dr["t697_nombrearchivo"].ToString();
                else
                {
                    SUPER.BLL.DOCSOLICITUD.Delete(null, int.Parse(dr["t697_iddoc"].ToString()));
                }
                bPrimero = false;
            }
            dr.Close();
            dr.Dispose();

            return sNomArchivo;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener documento de la solicitud de examen", ex);
        }
    }
    private string BorrarDocumento(string strDatos)
    {
        string sResul = "";
        bool bErrorControlado = false;

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
            string[] aDatos = Regex.Split(strDatos, "##");
            string sIdSolicitud = aDatos[0];
            if (sIdSolicitud != "" && sIdSolicitud != "-1")
            {
                SUPER.BLL.DOCSOLICITUD.Delete(tr, int.Parse(sIdSolicitud));
            }
            else
            {
                string sUsuTick = aDatos[1];
                if (sUsuTick != "")
                {
                    SUPER.BLL.DOCSOLICITUD.DeleteByUsuTicks(tr, sUsuTick);
                }
            }

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);

            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al borrar el documento asociado a la solicitud", ex);
            else sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
