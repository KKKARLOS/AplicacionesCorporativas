using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using SUPER.BLL;
using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "", sLectura = "false", sIDDocuAux = "";
    public int nIdSolicitud = 0;
    public SqlConnection oConn;
    public SqlTransaction tr;


    protected void Page_Load(object sender, EventArgs e)
    {
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
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

            if (!Page.IsPostBack)
            {
                sIDDocuAux = "SUPER-" + Session["IDFICEPI_CVT_ACTUAL"].ToString() + "-" + DateTime.Now.Ticks.ToString();

                Utilidades.SetEventosFecha(this.txtFechaO);

                this.hdnIdFicepi.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["iF"].ToString());
                this.hdnUserAct.Value = Session["IDFICEPI_CVT_ACTUAL"].ToString();

                //Compruebo si estoy en mi propio curriculum
                if (this.hdnIdFicepi.Value == this.hdnUserAct.Value)
                    this.hdnEsMiCV.Value = "S";

                if (Request.QueryString["eE"] != null && (Request.QueryString["eE"] != ""))
                    hdnEstado.Value = Utilidades.decodpar(Request.QueryString["eE"]);
                else
                    hdnEstado.Value = "B";

                if (Request.QueryString["iF"] != null && (Request.QueryString["iF"] != ""))
                    hdnIdFicepiExamen.Value = Utilidades.decodpar(Request.QueryString["iF"]);                

                switch (hdnEstado.Value)
                {
                    case "S": //Pte. cumplimentar (origen ECV)
                    case "T": //Pte. cumplimentar (origen Validador)
                        imgEstado.ImageUrl = "~/Images/imgEstadoCVTPenCumplimentar.png";
                        break;
                    case "O": //Pte. validar (origen ECV)
                    case "P": //Pte. validar (origen Validador)
                        //imgEstado.ImageUrl = "~/Images/imgEstadoCVTPenValidar.png";
                        imgEstado.ImageUrl = "~/Images/imgSeparador.gif";
                        break;
                    case "Y": //Pseudovalidado (origen ECV)
                    case "X": //Pseudovalidado (origen Validador)
                        imgEstado.ImageUrl = "~/Images/imgEstadoCVTPseudovalidado.png";
                        break;
                    case "B": //Borrador
                        imgEstado.ImageUrl = "~/Images/imgEstadoCVTBorrador.png";
                        break;
                    case "R": //No Interesante
                        //imgEstado.ImageUrl = "~/Images/imgEstadoCVTNoInteresante.png";
                        imgEstado.ImageUrl = "~/Images/imgSeparador.gif";
                        break;
                    case "V": //Validado
                        //imgEstado.ImageUrl = "~/Images/imgEstadoCVTValidado.png";
                        imgEstado.ImageUrl = "~/Images/imgSeparador.gif";
                        break;
                }
               

                if (Request.QueryString["nm"] != null && (Request.QueryString["nm"] != ""))
                    txtDenom.Value = Utilidades.decodpar(Request.QueryString["nm"]);

                
                //12/06/2018 Cualquier modificación sobre exámenes se hará a través de una nueva solicitud  
                txtNombreDocumento.ReadOnly = true;
                txtDenom.Disabled = true;

                //Tratamiento especial en la botonera de los exámenes       
                //12/06/2018 Cualquier modificación sobre exámenes se hará a través de una nueva solicitud                  
                btnCancelar.Style.Add("display", "inline-block");
                btnEnviar.Style.Add("display", "inline-block");

            }

        }
        catch (Exception ex)
        {
            hdnErrores.Value = Errores.mostrarError("Error al obtener los datos", ex);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "", sCad="";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        // 2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
            case ("documentos"):
                sResultado += getDocumento(aArgs[1]); ;
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
                nIdSolicitud = SUPER.BLL.SOLICITUD.Insertar(tr, aDatos[0], Utilidades.unescape(aDatos[1]), "Fecha de obtención: " + aDatos[3],
                                                           int.Parse(aDatos[4]), null, aDatos[2], "R");
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
