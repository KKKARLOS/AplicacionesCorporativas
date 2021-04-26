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
using EO.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sTipologias = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 9;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Cambio de naturaleza y contrato";
            Master.bFuncionesLocales = true;
            obtenerTipologias();

            if (!Page.IsPostBack)
            {
                try
                {
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
    protected void Regresar()
    {
        try
        {
            Response.Redirect(HistorialNavegacion.Leer(), true);
        }
        catch (System.Threading.ThreadAbortException) { }
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
            case ("NumContrato"):
                //sResultado += ObtenerContrato(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                sResultado += ObtenerContrato(int.Parse(aArgs[1]));
                break;
            case ("buscar"):
                sResultado += ObtenerPE(aArgs[1]);
                break;
            case ("setTipologia"):
                sResultado += setTipologia(aArgs[1]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
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
    //private string ObtenerContrato(int idContrato, int idNodo)
    private string ObtenerContrato(int idContrato)
    {
        try
        {
            string sIdContrato = "", sDenomContrato = "", sIdCliente = "", sCliente="";
            //SqlDataReader dr = CONTRATO.ObtenerExtensionPadre(idContrato, idNodo);
            SqlDataReader dr = CONTRATO.ObtenerExtensionPadre(idContrato);

            if (dr.Read())
            {
                sIdContrato = dr["t306_idcontrato"].ToString();
                sDenomContrato =  dr["t377_denominacion"].ToString();
                sIdCliente = dr["t302_idcliente"].ToString();
                sCliente = dr["t302_denominacion"].ToString();
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sIdContrato + "##" + Utilidades.escape(sDenomContrato) + "##" + sIdCliente + "##" + Utilidades.escape(sCliente);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener el Contrato.", ex);
        }
    }

    private string ObtenerPE(string sNumPE)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            //Obtengo los datos del proyecto
            PROYECTO oProy = PROYECTO.Obtener(tr, int.Parse(sNumPE));
            PROYECTOSUBNODO oPSNCON = PROYECTOSUBNODO.ObtenerContratante(null, int.Parse(sNumPE));

            return "OK@#@" + 
                    Utilidades.escape(oProy.t301_denominacion) + "##" +
                    oProy.t301_estado + "##" + oProy.t301_categoria + "##" + oProy.t320_idtipologiaproy.ToString() + "##" + oProy.t323_idnaturaleza.ToString() + 
                    "##" + Utilidades.escape(oProy.t323_denominacion) + "##" + oProy.t306_idcontrato.ToString() + "##" + Utilidades.escape(oProy.t377_denominacion) + 
                    "##" + oPSNCON.t303_idnodo.ToString() + "##" + Utilidades.escape(oProy.t302_denominacion) + "##" + oProy.t302_idcliente_proyecto.ToString() +
                    "##" + (oProy.t323_coste?"1":"0");
        }
        catch (Exception ex)
        {
            if (ex.Message == "No se ha obtenido ningun dato de PROYECTO")
                return "OK@#@";
                //return "error@#@Proyecto no encontrado.";
            else
                return "error@#@Error al obtener el Proyecto Económico./n " + ex.Message;
        }
    }
    private string Grabar(string sNumPE, string sIdNaturaleza, string sIdContrato, string sIdCliente)
    {
        string sResul = "OK@#@";
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
            if (sNumPE != "")
                PROYECTO.UpdateNatuContraCli(tr, int.Parse(sNumPE), int.Parse(sIdNaturaleza), (sIdContrato == "0") ? null : (int?)int.Parse(sIdContrato), int.Parse(sIdCliente));
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "error@#@Error al actualizar la naturaleza, el contrato o el cliente del proyecto económico " + sNumPE + "\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    protected string setTipologia(string sIDTipologia)
    {
        string sResul = "";

        try
        {
            int nFilas = 0;
            SqlDataReader dr = NATURALEZA.NaturalezasPorTipologia(int.Parse(sIDTipologia));
            while (dr.Read())
            {
                sResul = dr["t323_idnaturaleza"].ToString() + "@#@" + dr["denominacion"].ToString() + "@#@" + dr["t338_idplantilla"].ToString() + "@#@" + dr["t338_denominacion"].ToString();
                nFilas++;
                if (nFilas > 1)
                {
                    sResul = "";
                    break;
                }
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sResul;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la naturaleza", ex);
        }
    }
    private void obtenerTipologias()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("var js_tip = new Array();\n");

        SqlDataReader dr = TIPOLOGIAPROY.Catalogo(null, "", null, null, null, null, null, 7, 0);
        ListItem oItem;
        int i = 0;
        while (dr.Read())
        {
            oItem = new ListItem(dr["t320_denominacion"].ToString(), dr["t320_idtipologiaproy"].ToString());
            oItem.Attributes.Add("interno", ((bool)dr["t320_interno"]) ? "1" : "0");
            oItem.Attributes.Add("requierecontrato", ((bool)dr["t320_requierecontrato"]) ? "1" : "0");
            cboTipologiaNew.Items.Add(oItem);
            sb.Append("\tjs_tip[" + i + "] = {\"id\":" + dr["t320_idtipologiaproy"].ToString() + ",\"denominacion\":\"" + dr["t320_denominacion"].ToString() + "\",\"interno\":\"");
            if ((bool)dr["t320_interno"]) sb.Append("1");
            else sb.Append("0");
            sb.Append("\",\"requierecontrato\":\"");
            if ((bool)dr["t320_requierecontrato"]) sb.Append("1");
            else sb.Append("0");
            sb.Append("\"};\n");
            i++;
        }
        dr.Close();
        dr.Dispose();
        sTipologias = sb.ToString();
    }
}