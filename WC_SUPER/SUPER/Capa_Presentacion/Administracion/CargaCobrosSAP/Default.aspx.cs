using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL = IB.SUPER.APP.BLL;
using Models = IB.SUPER.APP.Models;
using Shared = IB.SUPER.Shared;
using Newtonsoft.Json;
//Para las credenciales de llamada al servicio SAP
using System.Web.Services.Protocols;
using System.Net;
using System.ServiceModel;

public partial class Capa_Presentacion_Administracion_CargaCobrosSAP_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";
        /*
        string script1 = "";
        Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());
        try
        {
            script1 += "IB.vars.idAsunto = '" + ht["idAsunto"].ToString() + "';";
        }
        catch { script1 += "IB.vars.idAsunto = '';"; }

        script1 += "IB.vars.qs = '" + Utils.decodpar(Request.QueryString.ToString()) + "';";
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);
        */
    }
    /*
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void borrarSaldos()
    {
        BLL.SALDOSSAP bSaldo = new BLL.SALDOSSAP();
        try
        {
            bSaldo.Delete();
        }
        catch (Exception ex)
        {
            if (bSaldo != null) bSaldo.Dispose();
            throw ex;
        }
        finally
        {
            bSaldo.Dispose();
        }
    }

    /// <summary>
    /// 1.- Accede a SAP mediante un servicio y obtiene un catálogo de saldos
    /// 2.- Inserta los elementos del catálogo en la tabla T297_SALDOSAP
    /// </summary>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void cargaSaldos(string fecha)
    {
        string sError = "";
        BLL.SALDOSSAP bSaldo = new BLL.SALDOSSAP();
        wsSapSaldo.ZVISTA_PA_WSClient mySAP = null;
        wsSapSaldo.ZVISTA_PA_SUPER oCobro = new wsSapSaldo.ZVISTA_PA_SUPER();
        wsSapSaldo.ZVISTA_PA_SUPER[] TI_ZVISTA_PA = { oCobro };
        try
        {
            #region SERVICIO WEB
            //Convierto la fecha a formato AAAA-MM-DD
            string sFecha = fecha.Substring(6, 4) + "-" + fecha.Substring(3, 2) + "-" + fecha.Substring(0, 2);
            //sFecha = "2005-12-31";

            BasicHttpBinding basicAuthBinding = new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly);
            basicAuthBinding.MaxReceivedMessageSize = 2147483647;
            basicAuthBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            EndpointAddress basicAuthEndpoint = new EndpointAddress("http://VMSAPIBD.intranet.ibermatica:8000/sap/bc/srt/rfc/sap/zvista_pa_ws/111/zvista_pa_ws/zb_vista_pa_ws");

            mySAP = new wsSapSaldo.ZVISTA_PA_WSClient(basicAuthBinding, basicAuthEndpoint);
            mySAP.ClientCredentials.UserName.UserName = "prg1";
            mySAP.ClientCredentials.UserName.Password = "fase10";
            wsSapSaldo.ZIB_WSRFC_PA_CREAR_TAB oParamIn = new wsSapSaldo.ZIB_WSRFC_PA_CREAR_TAB();
            wsSapSaldo.ZIB_WSRFC_PA_CREAR_TABResponse oResponse = new wsSapSaldo.ZIB_WSRFC_PA_CREAR_TABResponse();
            
            oParamIn.I_FECHA = sFecha;
            oParamIn.TI_ZVISTA_PA = TI_ZVISTA_PA;
            try
            {
                oResponse = mySAP.ZIB_WSRFC_PA_CREAR_TAB(oParamIn);
                TI_ZVISTA_PA = oResponse.TI_ZVISTA_PA;
            }
            catch(Exception e)
            {
                sError = e.Message + " " + e.InnerException.ToString();
            }
            finally
            {
                mySAP.Close();
            }

            #endregion
            if (sError == "")
            {
                #region Inserto los elementos del catálogo en la tabla T297_SALDOSAP
                foreach (wsSapSaldo.ZVISTA_PA_SUPER elem in TI_ZVISTA_PA)
                {
                    Models.SALDOSSAP mSaldo = new Models.SALDOSSAP();
                    mSaldo.AUGBL = elem.AUGBL;
                    mSaldo.AUGDT = elem.AUGDT.Replace("-", "");
                    mSaldo.AUGGJ = elem.AUGGJ;
                    mSaldo.BELNR = elem.BELNR;
                    mSaldo.BUDAT = elem.BUDAT.Replace("-", ""); ;
                    mSaldo.BUKRS = elem.BUKRS;
                    mSaldo.BUSAB = elem.BUSAB;
                    mSaldo.BUZEI = elem.BUZEI;
                    mSaldo.DMBT1 = elem.DMBT1;
                    mSaldo.DMBT2 = elem.DMBT2;
                    mSaldo.DMBT3 = elem.DMBT3;
                    mSaldo.DMBTR = elem.DMBTR;
                    mSaldo.FKDAT = elem.FKDAT.Replace("-", "");
                    mSaldo.GJAHR = elem.GJAHR;
                    mSaldo.HKONT = elem.HKONT;
                    mSaldo.KUNNR = elem.KUNNR;
                    mSaldo.LIFNR = elem.LIFNR;
                    mSaldo.MANDT = elem.MANDT;
                    mSaldo.MANSP = elem.MANSP;
                    mSaldo.MWSK1 = elem.MWSK1;
                    mSaldo.MWSK2 = elem.MWSK2;
                    mSaldo.MWSK3 = elem.MWSK3;
                    mSaldo.MWSKZ = elem.MWSKZ;
                    mSaldo.PARVW = elem.PARVW;
                    mSaldo.POSNR = elem.POSNR;
                    mSaldo.REBZG = elem.REBZG;
                    mSaldo.SGTXT = elem.SGTXT;
                    mSaldo.SHKZG = elem.SHKZG;
                    mSaldo.UMSKS = elem.UMSKS;
                    mSaldo.UMSKZ = elem.UMSKZ;
                    mSaldo.VBELN = elem.VBELN;
                    mSaldo.XBLNR = elem.XBLNR;
                    mSaldo.ZBD1T = elem.ZBD1T;
                    mSaldo.ZBD2T = elem.ZBD2T;
                    mSaldo.ZBD3T = elem.ZBD3T;
                    mSaldo.ZFBDT = elem.ZFBDT.Replace("-", "");
                    mSaldo.ZLSCH = elem.ZLSCH;
                    mSaldo.ZTERM = elem.ZTERM;
                    mSaldo.ZUONR = elem.ZUONR;
                    mSaldo.ZVENC = elem.ZVENC.Replace("-", "");

                    bSaldo.Insert(mSaldo);
                }
                #endregion
            }
            else
            {
                throw new Exception("Error en la llamada al servicio SAP: " + sError);
            }
        }
        catch (Exception ex)
        {
            if (bSaldo != null) bSaldo.Dispose();
            throw ex;
        }
        finally
        {
            bSaldo.Dispose();
            
        }
    }

    /// <summary>
    /// Llama al procedimiento almacenado SUP_GENERARCOBROSSAP que lee de la T297_SALDOSAP e inserta los cobros en T376_DATOECO
    /// </summary>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void generarCobros()
    {
        BLL.SALDOSSAP bSaldo = new BLL.SALDOSSAP();
        try
        {
            bSaldo.Pasar_a_SUPER();
        }
        catch (Exception ex)
        {
            if (bSaldo != null) bSaldo.Dispose();
            throw ex;
        }
        finally
        {
            bSaldo.Dispose();
        }
    }
    */
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void borrarSaldos()
    {
        BLL.SALDOSSAP bSaldo = new BLL.SALDOSSAP();
        try
        {
            bSaldo.Delete();
            //throw new Exception("Error al borrar saldos");
        }
        catch (Exception ex)
        {
            if (bSaldo != null) bSaldo.Dispose();
            throw ex;
        }
        finally
        {
            bSaldo.Dispose();
        }
    }

    /// <summary>
    /// 1.- Accede a SAP mediante un servicio y obtiene un catálogo de saldos
    /// 2.- Inserta los elementos del catálogo en la tabla T297_SALDOSAP
    /// </summary>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void cargaSaldos(string fecha)
    {
        string sError = "";
        BLL.SALDOSSAP bSaldo = new BLL.SALDOSSAP();
        wsSapSaldo.ZVISTA_PA_WSClient mySAP = null;
        wsSapSaldo.ZVISTA_PA_SUPER oCobro = new wsSapSaldo.ZVISTA_PA_SUPER();
        wsSapSaldo.ZVISTA_PA_SUPER[] TI_ZVISTA_PA = { oCobro };
        try
        {
            #region SERVICIO WEB
            //Convierto la fecha a formato AAAA-MM-DD
            string sFecha = fecha.Substring(6, 4) + "-" + fecha.Substring(3, 2) + "-" + fecha.Substring(0, 2);
            //sFecha = "2005-12-31";

            BasicHttpBinding basicAuthBinding = new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly);

            basicAuthBinding.OpenTimeout = new TimeSpan(0, 3, 0);
            basicAuthBinding.CloseTimeout = new TimeSpan(0, 3, 0);
            basicAuthBinding.ReceiveTimeout = new TimeSpan(0, 3, 0);
            basicAuthBinding.SendTimeout = new TimeSpan(0, 3, 0);

            basicAuthBinding.MaxReceivedMessageSize = 2147483647;
            basicAuthBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            EndpointAddress basicAuthEndpoint = new EndpointAddress(System.Configuration.ConfigurationManager.AppSettings["wsSapSaldo"]);


            mySAP = new wsSapSaldo.ZVISTA_PA_WSClient(basicAuthBinding, basicAuthEndpoint);
           /* mySAP.ClientCredentials.UserName.UserName = "prg1";
            mySAP.ClientCredentials.UserName.Password = "fase10";*/
            mySAP.ClientCredentials.UserName.UserName = "adminsap";
            mySAP.ClientCredentials.UserName.Password = "Productivo_14";
            wsSapSaldo.ZIB_WSRFC_PA_CREAR_TAB oParamIn = new wsSapSaldo.ZIB_WSRFC_PA_CREAR_TAB();
            wsSapSaldo.ZIB_WSRFC_PA_CREAR_TABResponse oResponse = new wsSapSaldo.ZIB_WSRFC_PA_CREAR_TABResponse();

            oParamIn.I_FECHA = sFecha;
            oParamIn.TI_ZVISTA_PA = TI_ZVISTA_PA;
            try
            {
                oResponse = mySAP.ZIB_WSRFC_PA_CREAR_TAB(oParamIn);
                TI_ZVISTA_PA = oResponse.TI_ZVISTA_PA;
            }
            catch (Exception e)
            {
                sError = e.Message + " " + e.InnerException.ToString();
            }
            finally
            {
                mySAP.Close();
            }

            #endregion
            if (sError == "")
            {
                #region Inserto los elementos del catálogo en la tabla T297_SALDOSAP
                foreach (wsSapSaldo.ZVISTA_PA_SUPER elem in TI_ZVISTA_PA)
                {
                    Models.SALDOSSAP mSaldo = new Models.SALDOSSAP();
                    mSaldo.AUGBL = elem.AUGBL;
                    mSaldo.AUGDT = elem.AUGDT.Replace("-", "");
                    mSaldo.AUGGJ = elem.AUGGJ;
                    mSaldo.BELNR = elem.BELNR;
                    mSaldo.BUDAT = elem.BUDAT.Replace("-", ""); ;
                    mSaldo.BUKRS = elem.BUKRS;
                    mSaldo.BUSAB = elem.BUSAB;
                    mSaldo.BUZEI = elem.BUZEI;
                    mSaldo.DMBT1 = elem.DMBT1;
                    mSaldo.DMBT2 = elem.DMBT2;
                    mSaldo.DMBT3 = elem.DMBT3;
                    mSaldo.DMBTR = elem.DMBTR;
                    mSaldo.FKDAT = elem.FKDAT.Replace("-", "");
                    mSaldo.GJAHR = elem.GJAHR;
                    mSaldo.HKONT = elem.HKONT;
                    mSaldo.KUNNR = elem.KUNNR;
                    mSaldo.LIFNR = elem.LIFNR;
                    mSaldo.MANDT = elem.MANDT;
                    mSaldo.MANSP = elem.MANSP;
                    mSaldo.MWSK1 = elem.MWSK1;
                    mSaldo.MWSK2 = elem.MWSK2;
                    mSaldo.MWSK3 = elem.MWSK3;
                    mSaldo.MWSKZ = elem.MWSKZ;
                    mSaldo.PARVW = elem.PARVW;
                    mSaldo.POSNR = elem.POSNR;
                    mSaldo.REBZG = elem.REBZG;
                    mSaldo.SGTXT = elem.SGTXT;
                    mSaldo.SHKZG = elem.SHKZG;
                    mSaldo.UMSKS = elem.UMSKS;
                    mSaldo.UMSKZ = elem.UMSKZ;
                    mSaldo.VBELN = elem.VBELN;
                    mSaldo.XBLNR = elem.XBLNR;
                    mSaldo.ZBD1T = elem.ZBD1T;
                    mSaldo.ZBD2T = elem.ZBD2T;
                    mSaldo.ZBD3T = elem.ZBD3T;
                    mSaldo.ZFBDT = elem.ZFBDT.Replace("-", "");
                    mSaldo.ZLSCH = elem.ZLSCH;
                    mSaldo.ZTERM = elem.ZTERM;
                    mSaldo.ZUONR = elem.ZUONR;
                    mSaldo.ZVENC = elem.ZVENC.Replace("-", "");
                    
                    bSaldo.Insert(mSaldo);
                }
                #endregion
            }
            else
            {
                throw new Exception("Error en la llamada al servicio SAP: " + sError);
            }
        }
        catch (Exception ex)
        {
            if (bSaldo != null) bSaldo.Dispose();
            throw ex;
        }
        finally
        {
            bSaldo.Dispose();

        }
    }


    /// <summary>
    /// Llama al procedimiento almacenado SUP_GENERARCOBROSSAP que lee de la T297_SALDOSAP e inserta los cobros en T376_DATOECO
    /// </summary>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string generarCobros(string fecha)
    {
        string sRes = "0";
        BLL.SALDOSSAP bSaldo = new BLL.SALDOSSAP();
        try
        {
            string sFecha = fecha.Substring(6, 4) + fecha.Substring(3, 2);
            int iNumCobros = bSaldo.Pasar_a_SUPER(int.Parse(sFecha));
            sRes = iNumCobros.ToString("#,##0");
            //throw new Exception("Error al generar cobros");
        }
        catch (Exception ex)
        {
            if (bSaldo != null) bSaldo.Dispose();
            throw ex;
        }
        finally
        {
            bSaldo.Dispose();
        }
        return sRes;
    }

}