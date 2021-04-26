using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL = IB.SUPER.IAP30.BLL;
using Models = IB.SUPER.IAP30.Models;
using IB.SUPER.Shared;


public partial class Capa_Presentacion_Reporte_GASVI_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        int nPSN = 0;
        byte idMotivo = 1;
        string sIdUser = Session["NUM_EMPLEADO_IAP"].ToString();
        this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";

        string script1 = "IB.vars.idficepi = '" + Session["IDFICEPI_PC_ACTUAL"] + "';";
        script1 += "IB.vars.codUsu = '" + sIdUser + "';";
        script1 += "IB.vars.bLectura = false;";
        script1 += "IB.vars.nMinimoKmsECO = " + Constantes.nNumeroMinimoKmsECO.ToString() + ";";

        Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());

        if (ht["nPSN"].ToString() != "")
        {
            nPSN = int.Parse(ht["nPSN"].ToString());
            script1 += "IB.vars.nPSN = '" + ht["nPSN"].ToString() + "';";
        }
        else
            script1 += "IB.vars.nPSN = '';";

        if (ht["sF"].ToString() != "")
            script1 += "IB.vars.sFechaIAP = '" + ht["sF"].ToString() + "';";
        else
            script1 += "IB.vars.sFechaIAP = '';";

        if (ht["nN"].ToString() != "")
        {
            script1 += "IB.vars.nIDNaturaleza = '" + ht["nN"].ToString() + "';";
            if (ht["nN"].ToString() == "20")
            {
                script1 += "IB.vars.idMotivo = 6;";
                idMotivo = 6;
            }
            else
            {
                script1 += "IB.vars.idMotivo = 1;";
            }
        }
        else
        {
            script1 += "IB.vars.nIDNaturaleza = '';";
            script1 += "IB.vars.idMotivo = 1;";
        }
        #region PonerDatosIniciales

        DBConn DBConn = new DBConn();
        IB.sqldblib.SqlServerSP cDblib = DBConn.dblibclass;

        //this.txtMotivo.Value = "Proyecto";
        Models.Motivo oMotivo = new Models.Motivo();
        BLL.Motivo bMotivo = new BLL.Motivo(cDblib);        

        Models.UsuarioGV oUsuario = new Models.UsuarioGV();
        BLL.UsuarioGV bUser = new BLL.UsuarioGV(cDblib);
        try
        {
            oMotivo = bMotivo.Select(idMotivo);
            this.txtMotivo.Value = oMotivo.t423_denominacion;

            int idUser = int.Parse(Session["NUM_EMPLEADO_IAP"].ToString());
            lblBeneficiario.InnerText = (Session["SEXOUSUARIO"].ToString() == "V") ? "Beneficiario" : "Beneficiaria";
            this.txtnomBene.Value = Session["DES_EMPLEADO"].ToString();

            if (nPSN > 0)
            {
                //hdnIdProyectoSubNodo.Text = nPSN.ToString();
                this.txtPro.Value = SUPER.BLL.PROYECTOGV.GetCodigoyNombre(nPSN);
            }

            oUsuario = bUser.Select(idUser);
            if (oUsuario.t010_idoficina == null)
            {
                throw new Exception("No se ha podido determinar la oficina liquidadora.");
            }
            txtnomBene.Value = oUsuario.Nombre;
            //hdnInteresado.Text = oUsuario.t314_idusuario.ToString();
            script1 += "IB.vars.idInteresado = '" + oUsuario.t314_idusuario.ToString() + "';";
            script1 += "IB.vars.sNodoUsuario = '" + oUsuario.t303_denominacion + "';";
            txtEmpresa.Value = oUsuario.t313_denominacion;
            txtOfi.Value = oUsuario.t010_desoficina;

            //if (oUsuario.t422_idmoneda != "" && oUsuario.t422_idmoneda != null) //Moneda por defecto a nivel de usuario
            //    cboMoneda.SelectedValue = oUsuario.t422_idmoneda.ToString();
            script1 += "IB.vars.idMoneda = '" + oUsuario.t422_idmoneda + "';";
            if (oUsuario.t069_iddietakm != null)
            {
                cldKMCO.InnerText = oUsuario.t069_ick.ToString("N");
                cldDCCO.InnerText = oUsuario.t069_icdc.ToString("N");
                cldMDCO.InnerText = oUsuario.t069_icmd.ToString("N");
                cldDECO.InnerText = oUsuario.t069_icde.ToString("N");
                cldDACO.InnerText = oUsuario.t069_icda.ToString("N");

                script1 += "IB.vars.cldKMCO = '" + cldKMCO.InnerText + "';";
                script1 += "IB.vars.cldDCCO = '" + cldDCCO.InnerText + "';";
                script1 += "IB.vars.cldMDCO = '" + cldMDCO.InnerText + "';";
                script1 += "IB.vars.cldDECO = '" + cldDECO.InnerText + "';";
                script1 += "IB.vars.cldDACO = '" + cldDACO.InnerText + "';";
            }
            //if (oUsuario.oTerritorio != null)
            //{
            cldKMEX.InnerText = oUsuario.T007_ITERK.ToString("N");
            cldDCEX.InnerText = oUsuario.T007_ITERDC.ToString("N");
            cldMDEX.InnerText = oUsuario.T007_ITERMD.ToString("N");
            cldDEEX.InnerText = oUsuario.T007_ITERDE.ToString("N");
            cldDAEX.InnerText = oUsuario.T007_ITERDA.ToString("N");

            script1 += "IB.vars.cldKMEX = '" + cldKMEX.InnerText + "';";
            script1 += "IB.vars.cldDCEX = '" + cldDCEX.InnerText + "';";
            script1 += "IB.vars.cldMDEX = '" + cldMDEX.InnerText + "';";
            script1 += "IB.vars.cldDEEX = '" + cldDEEX.InnerText + "';";
            script1 += "IB.vars.cldDAEX = '" + cldDAEX.InnerText + "';";
            //}

            //hdnOficinaBase.Text = (oUsuario.t010_idoficina_base.HasValue) ? oUsuario.t010_idoficina_base.ToString() : "";
            if ((oUsuario.t010_idoficina_base.HasValue))
                script1 += "IB.vars.sIdOficinaBase = '" + oUsuario.t010_idoficina_base.ToString() + "';";
            else
                script1 += "IB.vars.sIdOficinaBase = '';";
            //hdnOficinaLiquidadora.Text = (oUsuario.oOficinaLiquidadora != null) ? oUsuario.oOficinaLiquidadora.t010_idoficina.ToString() : "";
            if ((oUsuario.t010_idoficina.HasValue))
                script1 += "IB.vars.sIdOficinaLiq = '" + oUsuario.t010_idoficina.ToString() + "';";
            else
                script1 += "IB.vars.sIdOficinaLiq = '';";
            //hdnAutorresponsable.Text = (oUsuario.bAutorresponsable) ? "1" : "0";
            if (oUsuario.bAutorresponsable)
                script1 += "IB.vars.bAutorresponsable = 1;";
            else
                script1 += "IB.vars.bAutorresponsable = 0;";
                
            //parametros para poder volver a la pantalla de imputación
            script1 += "IB.vars.qs = '" + Request.QueryString.ToString() + "';";

            //1ºComprobar si el profesional tiene más de una empresa.
            script1 += setEmpresaTerritorio(int.Parse(sIdUser));

            script1 += "IB.vars.strServer = '" + Session["strServer"].ToString() + "';";
        }
        catch(Exception e1)
        {
            string sError = e1.Message;
        }
        finally{
            bUser.Dispose();
            bMotivo.Dispose();
            DBConn.Dispose();
        }
        #endregion

        //registramos en un form runat='server'
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);
    }

    protected string setEmpresaTerritorio(int nUsuario)
    {
        string script1 = "";
        //1ºComprobar si el profesional tiene más de una empresa.
        ArrayList aEmpresas =SUPER.BLL.USUARIOGV.ObtenerEmpresasTerritorios(nUsuario);
        txtEmpresa.Value = "";
        //hdnIDEmpresa.Text = "";
        string sIdEmpresa = "", sIdTerritorio="";
        if (aEmpresas.Count > 1)
        {
            txtEmpresa.Style.Add("display", "none");
            cboEmpresa.Style.Add("display", "block");

            ListItem oLI = null;
            for (int i = 0; i < aEmpresas.Count; i++)
            {
                oLI = new ListItem(((string[])aEmpresas[i])[1], ((string[])aEmpresas[i])[0]);
                oLI.Attributes.Add("idterritorio", ((string[])aEmpresas[i])[2]);
                oLI.Attributes.Add("nomterritorio", ((string[])aEmpresas[i])[3]);
                oLI.Attributes.Add("ITERDC", ((string[])aEmpresas[i])[4]);
                oLI.Attributes.Add("ITERMD", ((string[])aEmpresas[i])[5]);
                oLI.Attributes.Add("ITERDA", ((string[])aEmpresas[i])[6]);
                oLI.Attributes.Add("ITERDE", ((string[])aEmpresas[i])[7]);
                oLI.Attributes.Add("ITERK", ((string[])aEmpresas[i])[8]);

                cboEmpresa.Items.Add(oLI);

                if (cboEmpresa.Items.Count == 1 || (((string[])aEmpresas[i])[0] == "1") )
                {
                    //cboEmpresa.SelectedValue = ((string[])aEmpresas[i])[0];
                    //cboEmpresa.SelectedIndex = int.Parse(((string[])aEmpresas[i])[0]);
                    cboEmpresa.SelectedIndex = i;
                    //hdnIDEmpresa.Text = ((string[])aEmpresas[i])[0];
                    sIdEmpresa = ((string[])aEmpresas[i])[0];
                    //hdnIDTerritorio.Text = ((string[])aEmpresas[i])[2];
                    sIdTerritorio = ((string[])aEmpresas[i])[2];
                    lblTerritorio.InnerHtml = ((string[])aEmpresas[i])[3];
                }
            }
        }
        else if (aEmpresas.Count == 1)
        {
            txtEmpresa.Style.Add("display", "block");
            cboEmpresa.Style.Add("display", "none");
            //hdnIDEmpresa.Text = ((string[])aEmpresas[0])[0];
            sIdEmpresa = ((string[])aEmpresas[0])[0];
            txtEmpresa.Value = ((string[])aEmpresas[0])[1];
            //hdnIDTerritorio.Text = ((string[])aEmpresas[0])[2];
            sIdTerritorio = ((string[])aEmpresas[0])[2];
            lblTerritorio.InnerHtml = ((string[])aEmpresas[0])[3];
            cldKMEX.InnerText = decimal.Parse(((string[])aEmpresas[0])[8]).ToString("N");
            cldDCEX.InnerText = decimal.Parse(((string[])aEmpresas[0])[4]).ToString("N");
            cldMDEX.InnerText = decimal.Parse(((string[])aEmpresas[0])[5]).ToString("N");
            cldDEEX.InnerText = decimal.Parse(((string[])aEmpresas[0])[7]).ToString("N");
            cldDAEX.InnerText = decimal.Parse(((string[])aEmpresas[0])[6]).ToString("N");
        }
        script1 += "IB.vars.sIdEmpresa = '" + sIdEmpresa + "';";
        script1 += "IB.vars.sIdTerritorio = '" + sIdTerritorio + "';";

        return script1;
    }

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static List<Models.Motivo> getMotivos()
    //{
    //    IB.SUPER.IAP30.BLL.Motivo cCatalogoMotivos = new IB.SUPER.IAP30.BLL.Motivo();
    //    try
    //    {
    //        List<Models.Motivo> lMotivos = cCatalogoMotivos.Catalogo();
    //        return lMotivos;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (cCatalogoMotivos != null) cCatalogoMotivos.Dispose();
    //        throw ex;
    //    }
    //    finally
    //    {
    //        cCatalogoMotivos.Dispose();
    //    }
    //}
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.Moneda> getMonedas()
    {
        IB.SUPER.IAP30.BLL.Moneda cCatalogoMonedas = new IB.SUPER.IAP30.BLL.Moneda();

        try
        {
            List<Models.Moneda> lMonedas = cCatalogoMonedas.Catalogo();
            return lMonedas;
        }
        catch (Exception ex)
        {
            if (cCatalogoMonedas != null) cCatalogoMonedas.Dispose();
            throw ex;
        }
        finally
        {
            cCatalogoMonedas.Dispose();
        }
    }
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static List<Models.Historial> getHistorial()
    //{
    //    IB.SUPER.IAP30.BLL.Historial cCatalogo = new IB.SUPER.IAP30.BLL.Historial();

    //    try
    //    {
    //        List<Models.Historial> lCatalogo = cCatalogo.Catalogo();
    //        return lCatalogo;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (cCatalogo != null) cCatalogo.Dispose();
    //        throw ex;
    //    }
    //    finally
    //    {
    //        cCatalogo.Dispose();
    //    }
    //}
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.DESPLAZAMIENTO> ObtenerDesplazamientos(string sInteresado, string sDesde, string sHasta, string sReferencia)
    {
        BLL.DESPLAZAMIENTO DesplazamientosGasvi = new BLL.DESPLAZAMIENTO();
        try
        {
            List<Models.DESPLAZAMIENTO> lst = DesplazamientosGasvi.Catalogo
                (
                    int.Parse(sInteresado), //Interesado-Beneficiario
                    DateTime.Parse(sDesde), //desde
                    DateTime.Parse(sHasta), //hasta
                    int.Parse(sReferencia) //referencia GASVI
                );

            DesplazamientosGasvi.Dispose();

            return lst;
        }
        catch (Exception ex)
        {
            if (DesplazamientosGasvi != null) DesplazamientosGasvi.Dispose();
            throw ex;
        }
        finally
        {
            DesplazamientosGasvi.Dispose();
        }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static int grabar(Models.CabeceraGV cabecera, List<Models.LineaGV> lineas)
    {

        BLL.NotaGASVI nota = new BLL.NotaGASVI();
        int idReferencia;
        try
        {
            idReferencia = nota.grabar(cabecera, lineas);

            return idReferencia;
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al grabar la nota de gasto.", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al grabar la nota de gasto. " + ex.Message));

        }
        finally
        {
            nota.Dispose();
        }
        
    }

}