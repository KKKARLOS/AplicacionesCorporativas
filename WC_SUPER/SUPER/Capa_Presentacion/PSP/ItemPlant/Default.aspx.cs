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

public partial class Capa_Presentacion_ItemPlant_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores;
    public int nIdTarea;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaAECR, strTablaAET, strArrayVAE, gsTipoPlant;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            sErrores = "";
            strTablaAECR = "";
            strArrayVAE = "";

            nIdTarea = int.Parse(Request.QueryString["nIdTarea"].ToString());

            try
            {
                ObtenerDatosTarea();
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos del elemento", ex);
            }

            try
            {
                if (gsTipoPlant != "E")
                {//No puede haber atributos estadisticos para plantillas empresariales
                    PlantProy oPlant = PlantProy.Select(int.Parse(this.hdnIdPlant.Text));
                    ObtenerAtributosEstadisticosCR(oPlant.codune.ToString());
                    ObtenerAtributosEstadisticosTarea();
                    ObtenerValoresAtributosEstadisticosCR(oPlant.codune.ToString());
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener datos complementarios", ex);
            }

            this.hdnAcceso.Text = Request.QueryString["Permiso"].ToString();
            if (this.hdnAcceso.Text == "R")
            {
                ModoLectura.Poner(this.Controls);
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
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3]);
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

    private void ObtenerDatosTarea()
    {
        ITEMSPLANTILLA o = ITEMSPLANTILLA.Select(null, nIdTarea);

        txtIdTarea.Text = o.t339_iditems.ToString("#,###");
        txtDesTarea.Text = o.t339_desitem;
        this.hdnTipo.Text = o.t339_tipoitem;
        this.hdnMargen.Text = o.t339_margen.ToString();
        this.hdnIdPlant.Text = o.t338_idplantilla.ToString();
        if (o.t339_avanceauto) this.chkAvance.Checked = true;
        else this.chkAvance.Checked = false;
        if (o.t339_obligaest) this.chkObliga.Checked = true;
        else this.chkObliga.Checked = false;
        if (o.t339_facturable) chkFacturable.Checked = true;
        hdnOrden.Text = o.t339_orden.ToString();
        gsTipoPlant = o.tipoPlant.ToString();
    }

    private void ObtenerAtributosEstadisticosCR(string sNodo)
    {
        StringBuilder sbuilder = new StringBuilder();
        if (sNodo != "" && sNodo != "-1")
        {
            SqlDataReader dr = AE.Catalogo(null, "", true, null, null, short.Parse(sNodo), null, "T", 4, 0);
            while (dr.Read())
            {
                string sObl = "0";
                if ((bool)dr["t341_obligatorio"]) sObl = "1";

                sbuilder.Append("<tr style='height:16px'");
                if (this.hdnAcceso.Text == "R")
                {
                    sbuilder.Append("id='" + dr["t341_idae"].ToString() + "' cliente='" + dr["cod_cliente"].ToString() + "' obl='" + sObl + "'>");
                }
                else
                {
                    sbuilder.Append("id='" + dr["t341_idae"].ToString() + "' cliente='" + dr["cod_cliente"].ToString() + "' obl='" + sObl + "' onclick='mm(event);' ondblclick='asociarAE(this, true)' onmousedown='DD(event);'>");
                }
                if ((bool)dr["t341_obligatorio"])
                    sbuilder.Append("<td><img src='../../../images/imgIconoObl.gif' title='Atributo estadístico obligatorio'></td>");
                else
                    sbuilder.Append("<td><img src='../../../images/imgSeparador.gif'></td>");

                sbuilder.Append("<td><div class='NBR W160'>" + dr["t341_nombre"].ToString() + "<div/></td></tr>");
            }
            dr.Close();
            dr.Dispose();
        }
        strTablaAECR = sbuilder.ToString();
    }

    private void ObtenerAtributosEstadisticosTarea()
    {
        StringBuilder sbuilder = new StringBuilder();
        SqlDataReader dr = AEITEMSPLANTILLA.SelectByt339_iditems(null, nIdTarea);
        
        while (dr.Read())
        {
            string sObl = "0";
            if ((bool)dr["t341_obligatorio"]) sObl = "1";

            sbuilder.Append("<tr style='height:16px'");
            if (this.hdnAcceso.Text == "R")
            {
                sbuilder.Append("id='" + dr["t341_idae"].ToString() + "' vae='" + dr["t340_idvae"].ToString() + "' obl='" + sObl + "' bd=''>");
            }
            else
            {
                sbuilder.Append("id='" + dr["t341_idae"].ToString() + "' vae='" + dr["t340_idvae"].ToString() + "' obl='" + sObl + "' bd='' onclick='mm(event);mostrarValoresAE(this);' onmousedown='DD(event);'>");
            }
            sbuilder.Append("<td><img src='../../../images/imgFN.gif'></td>");
            if ((bool)dr["t341_obligatorio"])
                sbuilder.Append("<td ondblclick='desasociarAE(this.parentNode)'><img src='../../../images/imgOk.gif' title='Atributo estadístico obligatorio'></td>");
            else
                sbuilder.Append("<td ondblclick='desasociarAE(this.parentNode)'><img src='../../../images/imgSeparador.gif'></td>");

            sbuilder.Append("<td ondblclick='desasociarAE(this.parentNode)'><div class='NBR W200'>" + dr["t341_nombre"].ToString() + "</div></td>");
            sbuilder.Append("<td ondblclick='desasignarValorAE(this.parentNode)'>" + dr["t340_valor"].ToString() + "</td></tr>");
        }
        dr.Close();
        dr.Dispose();

        strTablaAET = sbuilder.ToString();
    }

    private void ObtenerValoresAtributosEstadisticosCR(string sNodo)
    {
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append("var aVAE_js = new Array();\n");
        if (sNodo != "")
        {
            SqlDataReader dr = VAE.CatalogoByUne(int.Parse(sNodo), "T");
            int i = 0;
            while (dr.Read())
            {
                sbuilder.Append("\taVAE_js[" + i.ToString() + "] = new Array(\"" + dr["t341_idae"].ToString() + "\",\"" + dr["t340_idvae"].ToString() + "\",\"" + dr["t340_valor"].ToString() + "\");\n");
                i++;
            }
            dr.Close();
            dr.Dispose();
        }
        strArrayVAE = sbuilder.ToString();
    }

    protected string Grabar(string strDatosItem, string strDatosAE, string strDatosApertura)
    {
        string sResul = "";

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
            #region datos Item plantilla
            string[] aDatosTarea = Regex.Split(strDatosItem, "##");
            ///aDatosTarea[0] = ID item plantilla;
            ///aDatosTarea[1] = Tipo item;
            ///aDatosTarea[2] = Des. item;
            ///aDatosTarea[3] = margen;
            ///aDatosTarea[4] = orden;
            ///aDatosTarea[5] = ID plantilla;
            ///aDatosTarea[6] = Facturable o no (1 / 0);
            ///aDatosTarea[7] = Avence automatico o no (1 / 0);
            ///aDatosTarea[8] = Obliga estimación ;

            bool bFacturable = false,bAvanceAuto=false, bObligaEst=false;
            if (aDatosTarea[6] == "1") bFacturable = true;
            if (aDatosTarea[7] == "1") bAvanceAuto = true;
            if (aDatosTarea[8] == "1") bObligaEst = true;
            ITEMSPLANTILLA.Update(tr,
                                int.Parse(aDatosTarea[0]), //ID item plantilla;
                                aDatosTarea[1],//TIPO de item
                                Utilidades.unescape(aDatosTarea[2]), //Des. item;
                                byte.Parse(aDatosTarea[3]), //margen
                                short.Parse(aDatosTarea[4]), //orden
                                int.Parse(aDatosTarea[5]), //ID plantilla;
                                bFacturable, bAvanceAuto, bObligaEst);
            #endregion

            #region datos Atributos Estadísticos

            string[] aAE = Regex.Split(strDatosAE, "///");

            foreach (string oAE in aAE)
            {
                string[] aValores = Regex.Split(oAE, "##");
                switch (aValores[0])
                {
                    case "I":
                        AEITEMSPLANTILLA.Insert(tr, int.Parse(aValores[1]),  int.Parse(aValores[3]));
                        break;
                    case "U":
                        AEITEMSPLANTILLA.Update(tr, int.Parse(aValores[1]), int.Parse(aValores[3]));
                        break;
                    case "D":
                        AEITEMSPLANTILLA.Delete(tr, int.Parse(aValores[1]), int.Parse(aValores[3]));
                        break;
                }
            }

            #endregion

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + DateTime.Now.ToString() + "@#@" + Session["NUM_EMPLEADO_ENTRADA"].ToString() + "@#@" + Session["DES_EMPLEADO_ENTRADA"].ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del elemento", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
