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
//para el log
using System.IO;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "", sLectura = "false", esPostBack = "false";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //put("Inicio-Pregunta Request.QueryString[f]=" + Request.QueryString["f"].ToString());
            //if (Page.IsPostBack)
            //{
            //    esPostBack = "true";
            //    if (this.chkAceptar.Checked && this.txtPregunta.Text != "" && this.txtRespuesta.Text != "")
            //        Grabar(this.txtPregunta.Text + "##" + this.txtRespuesta.Text);
            //    return;
            //}
            if (Request.QueryString["f"] != null)
            {
                if (Request.QueryString["f"].ToString() != "")
                {
                    int idFicepi = int.Parse(Request.QueryString["f"].ToString());

                    if (Request.QueryString["vez"] != null)
                    {
                        int vez = int.Parse(Request.QueryString["vez"].ToString());
                        this.hdnVez.Value = vez.ToString();
                        btnGrabar.Style.Add("display", "none");
                        btnSalir.Style.Add("display", "none");
                        btnAbandonar.Style.Add("display", "block");
                        btnSiguiente.Style.Add("display", "block");
                    }
                    this.hdnIdFicepi.Value = idFicepi.ToString();
                    getDatos(idFicepi);
                }
            }
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
        //put("Entro en RaiseCallbackEvent");
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@";

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
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

    protected string obtenerCadenaConexion()
    {
        string sConn = "";
        if (HttpContext.Current.Cache.Get("CadenaConexion") == null)
        {
            if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper() == "E")
                sConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionExplotacion"].ToString();
            else
                sConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionDesarrollo"].ToString();

            HttpContext.Current.Cache.Insert("CadenaConexion", sConn, null, DateTime.Now.AddHours(24), TimeSpan.Zero);
        }
        else
        {
            sConn = (string)HttpContext.Current.Cache.Get("CadenaConexion");
        }

        return sConn;
    }
    protected string unescape(string sCadena)
    {
        if (sCadena == null) return "";  //El método UnescapeDataString no acepta un null como input

        return System.Uri.UnescapeDataString(sCadena);
    }
    protected void put(string sLinea)
    {
        string sFile = HttpContext.Current.Request.PhysicalApplicationPath + "Upload\\" + "logApp.txt";
        FileStream fs = new FileStream(sFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);

        StreamWriter w = new StreamWriter(fs); // create a stream writer 
        w.BaseStream.Seek(0, SeekOrigin.End); // set the file pointer to the end of file 
        w.WriteLine(sLinea);
        w.Flush(); // update underlying file
        w.Close();
    }

    protected void getDatos(int idFicepi)
    {
        //string strDatos = SUPER.BLL.FORANEO.GetDatosRecordatorio(idFicepi);
        string strDatos = GetDatosRecordatorio(idFicepi);
        if (strDatos != "")
        {
            string[] aDatos = Regex.Split(strDatos, "@#@");
            if (aDatos[0] == "T")
            {
                this.chkAceptar.Checked = true;
                this.chkAceptar.Disabled = true;
                this.divCondicionesLegales.Style.Add("display", "block");
                this.fechaAceptacion.InnerText =aDatos[3];
            }

            if (aDatos[0] == "F")
            {
                
            }

            this.txtPregunta.Text = aDatos[1];
            this.txtRespuesta.Text = aDatos[2];
        }
    }
    protected string GetDatosRecordatorio(int idFicepi)
    {
        string sRes = "";
        SqlDataReader dr = GetDatosForaneo(idFicepi);
        if (dr.Read())
        {
            if (dr["t080_facep"].ToString() == "")
                sRes = "F@#@";
            else
            {
                sRes = "T@#@";
                this.divCondicionesLegales.Attributes.Add("display", "block");
                this.fechaAceptacion.InnerText = dr["t080_facep"].ToString();
            }
            sRes += SUPER.BLL.Seguridad.DesEncriptar(dr["t080_pregunta"].ToString()) + "@#@" + 
                    SUPER.BLL.Seguridad.DesEncriptar(dr["t080_respuesta"].ToString()) + "@#@" + 
                    dr["t080_facep"].ToString();
        }
        dr.Close();
        dr.Dispose();

        return sRes;
    }
    protected SqlDataReader GetDatosForaneo(int idFicepi)
    {
        SqlConnection con = null;
        SqlCommand cmd = null;

        string ConnectionString = obtenerCadenaConexion();
        con = new SqlConnection(ConnectionString);
        con.Open();

        cmd = new SqlCommand("SUP_FICEPIFORANEO_SEL", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@t001_idficepi", SqlDbType.Int, 4);
        cmd.Parameters[0].Value = idFicepi;

        return cmd.ExecuteReader();
    }

    protected string Grabar(string strDatos)
    {
        string sResul = "OK@#@";
        //put("Entro en Grabar");
        try
        {
            //put("strDatos=" + strDatos);
            string[] aDatos = Regex.Split(strDatos, "##");
            //SUPER.BLL.FORANEO.GrabarRecordatorio(aDatos[0], aDatos[1]);
            GrabarRecordatorio(aDatos[0], aDatos[1], aDatos[2]);
        }
        catch (Exception ex)
        {
            //sResul = "Error@#@" + SUPER.Capa_Negocio.Errores.mostrarError("Error al grabar la pregunta para recordar la contraseña", ex);
            sResul = "Error@#@Error al grabar la pregunta para recordar la contraseña\n\n" + ex.Message;
        }
        return sResul;
    }
    protected void GrabarRecordatorio(string sIdFicepi, string sPregunta, string sRespuesta)
    {
        //put("Entro en GrabarRecordatorio");
        int idFicepi = int.Parse(sIdFicepi);
        //DAL.FICEPIFORANEO.Updatear(null, idFicepi, null, DateTime.Now, DateTime.Now, "",
        //                           SUPER.Capa_Negocio.Utilidades.unescape(sPregunta),
        //                           SUPER.Capa_Negocio.Utilidades.unescape(sRespuesta));
        Updatear(idFicepi, null, DateTime.Now, DateTime.Now, "", unescape(sPregunta.Trim()), unescape(sRespuesta.Trim()));
    }
    protected void Updatear(int t001_idficepi, Nullable<int> t001_idficepi_promotor,
                                    Nullable<DateTime> t080_facep, Nullable<DateTime> t080_fultacc, string t080_passw,
                                    string t080_pregunta, string t080_respuesta)
    {
        //put("Entro en Updatear");
        SqlConnection con = null;
        SqlCommand cmd = null;

        string ConnectionString = obtenerCadenaConexion();
        con = new SqlConnection(ConnectionString);
        con.Open();
        cmd = new SqlCommand("SUP_FICEPIFORANEO_UPD", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@t001_idficepi", SqlDbType.Int, 4);
        cmd.Parameters[0].Value = t001_idficepi;
        cmd.Parameters.Add("@t001_idficepi_promotor", SqlDbType.Int, 4);
        cmd.Parameters[1].Value = t001_idficepi_promotor;
        cmd.Parameters.Add("@t080_facep", SqlDbType.SmallDateTime, 10);
        cmd.Parameters[2].Value = t080_facep;
        cmd.Parameters.Add("@t080_fultacc", SqlDbType.SmallDateTime, 10);
        cmd.Parameters[3].Value = t080_fultacc;
        cmd.Parameters.Add("@t080_passw", SqlDbType.Text, 12);
        cmd.Parameters[4].Value = t080_passw;
        cmd.Parameters.Add("@t080_pregunta", SqlDbType.Text, 100);
        cmd.Parameters[5].Value = SUPER.BLL.Seguridad.Encriptar(t080_pregunta);
        cmd.Parameters.Add("@t080_respuesta", SqlDbType.Text, 100);
        cmd.Parameters[6].Value = SUPER.BLL.Seguridad.Encriptar(t080_respuesta);

        cmd.ExecuteNonQuery();
    }
}
