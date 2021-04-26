using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections;
//using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

using System.Runtime.Serialization.Json;
//Para el MemoryStream
using System.IO;
//Para el Encoding
using System.Text;
//Para el XML
using System.Xml;
//Para las credenciales de llamada al servicio SAP
using System.Web.Services.Protocols;
using System.Net;
using System.ServiceModel;

public partial class Capa_Presentacion_Pruebas_JsonNet_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int x=0, y = 10, z = 1;
        //svcSUPERIBOffice.IsvcSUPERClient osvcExcel = null;
        try
        {
            Master.TituloPagina = "Test Newtonsoft.Json";
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FuncionesJavaScript.Add("Javascript/dhtmltooltip.js");
            Master.bFuncionesLocales = true;
            //Master.FuncionesJavaScript.Add("Javascript/jquery-1.7.1.min.js");

            //Acceso a BBDD
            //SUPER.Capa_Negocio.TAREAPSP.Prueba();

            //Decodificar paquete PaqExpress
            //http://paqexpress2.ibermatica.com/default.aspx?x=TUlLRUwgQUdVSUxBUiMjNzQzODU=##X64X 
            string sAux = System.Text.ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String("TUlLRUwgQUdVSUxBUiMjNzQzODU="));

            //osvcExcel = new svcSUPERIBOffice.IsvcSUPERClient();
            //string sAux = osvcExcel.SaludarDesdeIbOffice("Mikel");
            /*
            DateTime dt= new DateTime();
            dt = System.DateTime.Now;
            string sTemp = dt.ToShortDateString();
            string sTemp2 = dt.ToString();
            string sTemp3 = sTemp2.Substring(0, 10);
            */
            #region prueba IBERDOK
            //Obtengo un dataset
            //DataSet ds = SUPER.DAL.Curriculum.pruebaInsertIberdok(1797);

            //Del dataset extraigo un datatable para pasarlo como parámetro
            //DataTable dtDP = ds.Tables[0];

            //SqlParameter[] aParam = new SqlParameter[]{ParametroSql.add("@PROFESIONAL", SqlDbType.Structured, dtDP)};
            //object oRes = SqlHelper.ExecuteScalar("ZZZ_MIKEL_CARGA_IBERDOK_PRUEBA_PASO2", aParam);
            #endregion
            #region prueba cache
            //HttpContext.Current.Cache.Remove("EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString());
            //HttpContext.Current.Cache.Insert("MIKEL", "HOLA", null, DateTime.Now.AddMinutes(5), TimeSpan.Zero);
            //string sCadena = (string)HttpContext.Current.Cache.Get("MIKEL");
            //HttpContext.Current.Cache.Insert("MIKEL", "ADIOS", null, DateTime.Now.AddMinutes(5), TimeSpan.Zero);
            //sCadena = (string)HttpContext.Current.Cache.Get("MIKEL");
            //string sIdCache = Request.QueryString["cachekk"].ToString();
            #endregion
            //z = z - 1;
            //x = y / z;
            //string sDestinatariosCentro = "", sIdCentro = "";
            //string[] aCentros = Regex.Split("1##@#@28##jm.gonzalez-ripoll@ibermatica.com##f.garcia.conejo@externo.ibermatica.com@#@-1##j.gorospe@ibermatica.com##a.saldana@ibermatica.com", "@#@");
            //for (int iAux = 0; iAux < aCentros.Length; iAux++)
            //{
            //    sDestinatariosCentro = "";
            //    if (aCentros[iAux] != "")
            //    {
            //        string[] aDatosCentro = Regex.Split(aCentros[iAux], "##");
            //        if (aDatosCentro[0] != "")
            //        {
            //            sIdCentro = aDatosCentro[0];
            //            for (int iAux2 = 1; iAux2 < aDatosCentro.Length; iAux2++)
            //            {
            //                if (aDatosCentro[iAux2] != "")
            //                {
            //                    if (sDestinatariosCentro == "")
            //                        sDestinatariosCentro = aDatosCentro[iAux2];
            //                    else
            //                        sDestinatariosCentro = sDestinatariosCentro + ";" + aDatosCentro[iAux2];
            //                }
            //            }
            //        }
            //    }
            //}

        }
        catch (Exception ex)
        {
            //Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
            string sError = ex.Message;
            throw ex;
        }
        finally
        {/*
            if (osvcExcel != null && osvcExcel.State != System.ServiceModel.CommunicationState.Closed)
            {
                try
                {
                    if (osvcExcel.State != System.ServiceModel.CommunicationState.Faulted) osvcExcel.Close();
                    else if (osvcExcel.State != System.ServiceModel.CommunicationState.Closed) osvcExcel.Abort();
                }
                catch {}
            }*/
            string sFinally = "Hola";
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string Test(int annomes, ArrayList aCentros, ArrayList aEvaluadores, ArrayList aPSN, ArrayList aTareas)
    {
        //System.Web.Helpers. //dynamic json
        //StringBuilder sb = new StringBuilder();
        string sb = "AAAA";

        //foreach (Dictionary<string, object> kvp in aPSN)
        //{
        //    string a = "";
        //    //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
        //}
        //foreach (Dictionary<string, object> kvp in aTareas)
        //{
        //    string b = "";
        //    //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
        //}
        //JObject o = JObject.Parse(json);
        //JObject o = JObject.FromObject(json);   //;JsonConvert.SerializeObject(json);
        return sb.ToString();

        
        //dynamic newJson = json;

        //newJson.NewProperty = "Something";
        //newJson.Date = DateTime.Now;

        //return newJson;

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string TestIberdok(string solicitante, string password, string pedido, int code)
    {
        //Llamada a servicio REST
        var client = new SUPER.BLL.RestClient();
        client.EndPoint = @"http://services.intranet.ibermatica/IberDokControl/IberDok.svc/Control";
        client.Method = HttpVerb.POST;
        //client.PostData = "{idUsuario:admin,uidPedido:7149C6BD-B49D-418B-BD3D-73E88CFCA459,modelo:3473,tipo:PDF, clase:I}";}

        //El servicio responde
        //string sParam = @"{""idUsuario"":""admin"",""uidPedido"":""7149C6BD-B49D-418B-BD3D-73E88CFCA459"",""modelo"":""3473"",""tipo"":""PDF"",""clase"":""I""}";
        string sParam = @"{""solicitante"":""" + solicitante + @"""";
        sParam += @",""password"":""" + password + @"""";
        sParam += @",""pedido"":""" + pedido + @"""";
        sParam += @",""code"":" + code.ToString() + @"}";

        //Prueba serializando con JSON
        //IberDok oPet = new IberDok(usuario, uiPedido, modelo, tipo, clase);
        //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IberDok));
        //MemoryStream mem = new MemoryStream();
        //ser.WriteObject(mem, oPet);
        //string sParam = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);

        client.PostData = sParam;

        var json = client.MakeRequest();

        return json;

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string TestIberdok2(string solicitante, string password, string pedido, int code)
    {
        //Llamada a servicio REST
        var client = new SUPER.BLL.RestClient();
        client.EndPoint = @"http://mk17706.intranet.ibermatica/IBSERVICES/IberDokControl/IberDok.svc/Control";
        client.Method = HttpVerb.POST;
        //client.PostData = "{idUsuario:admin,uidPedido:7149C6BD-B49D-418B-BD3D-73E88CFCA459,modelo:3473,tipo:PDF, clase:I}";}

        //El servicio responde
        //string sParam = @"{""idUsuario"":""admin"",""uidPedido"":""7149C6BD-B49D-418B-BD3D-73E88CFCA459"",""modelo"":""3473"",""tipo"":""PDF"",""clase"":""I""}";
        string sParam = @"{""solicitante"":""" + solicitante + @"""";
        sParam += @",""password"":""" + password + @"""";
        sParam += @",""pedido"":""" + pedido + @"""";
        sParam += @",""code"":" + code.ToString() + @"}";

        //Prueba serializando con JSON
        //IberDok oPet = new IberDok(usuario, uiPedido, modelo, tipo, clase);
        //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IberDok));
        //MemoryStream mem = new MemoryStream();
        //ser.WriteObject(mem, oPet);
        //string sParam = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);

        client.PostData = sParam;

        var json = client.MakeRequest();

        return json;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string CarpetaIberdok(string dominio, string usuario, string password, string pedido)
    {
        string sRes = "";
        string sPath = @"\\iberdok\clienteDISPedidos\" + pedido.ToString() + "\\";
        try
        {
            using (new SUPER.BLL.Impersonation(dominio, usuario, password))
            {
                string[] files = System.IO.Directory.GetFiles(sPath);
                if ((System.IO.File.OpenRead(files[0]).Length / 1048576) < 10)
                {
                    sRes = "1";
                }
                else
                {//Si el resultado es > 10Mb enviarlo por PaqExpress
                    sRes = "2";
                }
            }
        }
        catch(Exception e)
        {
            sRes = e.Message;
        }
        return sRes;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string TestQTCNDrupal(string solicitante, string password, string t513_idlinea)
    {
        //Llamada a servicio REST
        var client = new SUPER.BLL.RestClient();
        #region Metodos GET
        //client.Method = HttpVerb.GET;
        //Obtener paises
        //client.EndPoint = @"http://mk17706.intranet.ibermatica/IBSERVICES/QTCNDrupal/QTCN.svc/QTCNObtenerPaises?solicitante=" + solicitante + "&password=" + password;
        //Obtener Provincias (de España)
        //client.EndPoint = @"http://mk17706.intranet.ibermatica/IBSERVICES/QTCNDrupal/QTCN.svc/QTCNObtenerProvincias?solicitante=" + solicitante + "&password=" + password + "&t172_idpais=1";
        //Obtener Líneas
        //client.EndPoint = @"http://mk17706.intranet.ibermatica/IBSERVICES/QTCNDrupal/QTCN.svc/QTCNObtenerLineas?solicitante=" + solicitante + "&password=" + password + "&columna=0&orden=0";
        //Obtener datos línea
        //client.EndPoint = @"http://mk17706.intranet.ibermatica/IBSERVICES/QTCNDrupal/QTCN.svc/QTCNObtenerDatosLinea?solicitante=" + solicitante + "&password=" + password + "&t513_idlinea=8707";
        #endregion

        #region Métodos POST
        client.Method = HttpVerb.POST;
        //Prueba
        //client.EndPoint = @"http://mk17706.intranet.ibermatica/IBSERVICES/QTCNDrupal/QTCN.svc/Prueba";
        //QTCNRegistrarInscripcionOferta
        client.EndPoint = @"http://mk17706.intranet.ibermatica/IBSERVICES/QTCNDrupal/QTCN.svc/QTCNRegistrarInscripcionOferta";

        #region Pruebas
        //string sexo = "V";//M
        //string sNom = "MIKEL", sAp1 = "ARIZTEGUI", sAp2 = "HUARTE", sPais="66", sProv="20", sTfno="12346789";
        //string sEmail = "m.ariztegui@ibermatica.com", sFNac="20/10/1966", sNif="72667667H", sObs="camión ñandú";
        //string sOrigen = "1";//Amigo
        //string sExt = "doc";//, t2_iddocumento="";
        //byte[] data=null;

        //string candidato = @"{""t500_sexo"":""" + sexo + @"""";
        //candidato += @",""t500_nombre"":""" + sNom + @"""";
        //candidato += @",""t500_apellido1"":""" + sAp1 + @"""";
        //candidato += @",""t500_apellido2"":""" + sAp2 + @"""";
        //candidato += @",""t172_idpais"":""" + sPais + @"""";
        //candidato += @",""t173_idprovincia"":""" + sProv + @"""";
        //candidato += @",""t500_telefono"":""" + sTfno + @"""";
        //candidato += @",""t500_email"":""" + sEmail + @"""";
        //candidato += @",""t500_fecnacim"":""" + sFNac + @"""";
        //candidato += @",""t500_nif"":""" + sNif + @"""";
        //candidato += @",""t500_observa"":""" + sObs + @"""";
        //candidato += @",""t530_idorigen"":""" + sOrigen + @"""";
        //candidato += @",""t500_newpais"":""" + sPais + @"""";
        //candidato += @",""t500_newprovincia"":""" + sProv + @"""";
        //candidato += @",""ext"":""" + sExt + @"""";
        //if (data == null)
        //    candidato += @",""cv"":""""";
        //else
        //    candidato += @",""cv"":""" + Convert.ToBase64String(data) + @"""";
        //candidato += @"}";

        //string sParam = @"{""solicitante"":""" + solicitante + @"""";
        //sParam += @",""password"":""" + password + @"""";
        //sParam += @",""t513_idlinea"":""" + t513_idlinea + @"""";
        //candidato = "MIKEL";
        //sParam += @",""candidato"":""" + precandidato + @"""";
        //sParam += @"}";
        //string pedido = "1", code="2";

        //string sParam = @"{""solicitante"":""" + solicitante + @"""";
        //sParam += @",""password"":""" + password + @"""";
        //sParam += @",""t513_idlinea"":""" + t513_idlinea + @"""";
        //sParam += @",""candidato"":" + candidato + @"";
        //sParam += @"}";
        #endregion

        //string sPathDoc = "d:\\Docs\\_FicheroPruebaQTCN.txt";
        string sPathDoc = "d:\\Docs\\Gordos\\Doc_5Mb.doc";

        byte[] oCV = System.IO.File.ReadAllBytes(sPathDoc);
        string temp_inBase64 = Convert.ToBase64String(oCV);

        Candidato oCandidato = new Candidato();
        oCandidato.t500_sexo = "V";
        oCandidato.t500_nombre = "MIKEL";
        oCandidato.t500_apellido1 = "ARIZTEGUI";
        oCandidato.t500_apellido2 = "HUARTE";
        oCandidato.t500_nif = "73667667H";
        oCandidato.t500_telefono = "123465789";
        oCandidato.t500_email = "m.ariztegui@ibermatica.com";
        DateTime dt = new DateTime(1966, 10, 20);
        oCandidato.t500_fecnacim = dt;
        oCandidato.t500_nif = "72789456X";
        oCandidato.t500_observa = "observaciones";
        oCandidato.t530_idorigen = 1;
        oCandidato.t172_idpais = 1;
        oCandidato.t173_idprovincia = 20;
        //oCandidato.t500_newpais = "";
        //oCandidato.t500_newprovincia = "";
        oCandidato.extension = "doc";
        oCandidato.cv = temp_inBase64;

        Solicitud oSolicitud = new Solicitud();
        oSolicitud.solicitante = solicitante;
        oSolicitud.password = password;
        oSolicitud.t513_idlinea = t513_idlinea;
        oSolicitud.candidato = JsonConvert.SerializeObject(oCandidato);

        string sParam = JsonConvert.SerializeObject(oSolicitud);
        client.PostData = sParam;
        #endregion

        var json = client.MakeRequest();

        return json;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string TestQTCNDrupalGET(string solicitante, string password, string t513_idlinea)
    {
        //Llamada a servicio REST
        var client = new SUPER.BLL.RestClient();
        #region Metodos GET
        //client.Method = HttpVerb.GET;
        //Obtener paises
        client.EndPoint = @"http://mk17706.intranet.ibermatica/IBSERVICES/QTCNDrupal/QTCN.svc/QTCNObtenerPaises?solicitante=" + solicitante + "&password=" + password;
        //Obtener Provincias (de España)
        //client.EndPoint = @"http://mk17706.intranet.ibermatica/IBSERVICES/QTCNDrupal/QTCN.svc/QTCNObtenerProvincias?solicitante=" + solicitante + "&password=" + password + "&t172_idpais=1";
        //Obtener Líneas
        //client.EndPoint = @"http://mk17706.intranet.ibermatica/IBSERVICES/QTCNDrupal/QTCN.svc/QTCNObtenerLineas?solicitante=" + solicitante + "&password=" + password + "&columna=0&orden=0";
        //Obtener datos línea
        //client.EndPoint = @"http://mk17706.intranet.ibermatica/IBSERVICES/QTCNDrupal/QTCN.svc/QTCNObtenerDatosLinea?solicitante=" + solicitante + "&password=" + password + "&t513_idlinea=8707";
        #endregion


        var json = client.MakeRequest();
        string sAux = json;
        sAux = sAux.Substring(1);
        sAux = sAux.Substring(0, sAux.Length - 1);
        //return json;
        return sAux;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string TestQTCNDrupalPOST(string solicitante, string password, string t513_idlinea)
    {
        //Llamada a servicio REST 
        var client = new SUPER.BLL.RestClient();

        client.Method = HttpVerb.POST;
        client.EndPoint = @"http://mk17706.intranet.ibermatica/IBSERVICES/QTCNDrupal/QTCN.svc/Prueba3";
        //client.EndPoint = @"http://mk17706.intranet.ibermatica/IBSERVICES/QTCNDrupal/QTCN.svc/QTCNRegistrarInscripcionOferta";

        string sPathDoc = "d:\\Docs\\_FicheroPruebaQTCN.txt";//Fichero pequeño
        //string sPathDoc = "d:\\Docs\\CaducidadSesion.doc";
        //string sPathDoc = "d:\\Docs\\Aplicaciones_CrossBrowser.doc";
        //string sPathDoc = "d:\\Docs\\Gordos\\Doc_5Mb.doc";

        byte[] oCV = System.IO.File.ReadAllBytes(sPathDoc);
        string temp_inBase64 = Convert.ToBase64String(oCV);

        //Prueba oPrueba = new Prueba();
        //Precandidato oPrueba = new Precandidato();
        //oPrueba.t500_sexo = "V";//V
        //oPrueba.t500_apellido1 = "ARIZTEGUI";
        //oPrueba.t500_apellido2 = "HUARTE";
        //oPrueba.t500_nombre = "MIKEL";
        //oPrueba.t172_idpais = 1;
        //oPrueba.t173_idprovincia = 20;
        //oPrueba.t500_telefono = "943613817";
        //oPrueba.t500_email = "m.ariztegui@gmail.com";
        //DateTime dt = new DateTime(1966, 10, 20);
        //oPrueba.t500_fecnacim = dt;
        //oPrueba.t500_nif = "72667667H";
        //oPrueba.t500_observa = "hola";
        //oPrueba.t530_idorigen = 1;
        //oPrueba.extension = "doc";
        //oPrueba.cv = temp_inBase64;// 

        SolicitudPrueba oSolicitud = new SolicitudPrueba();
        oSolicitud.solicitante = solicitante;
        oSolicitud.password = password;
        oSolicitud.t513_idlinea = t513_idlinea;
        //oSolicitud.Prueba = JsonConvert.SerializeObject(oPrueba);
        //oSolicitud.precandidato = JsonConvert.SerializeObject(oPrueba);
        oSolicitud.t500_sexo = "V";//V
        oSolicitud.t500_apellido1 = "ARIZTEGUI";
        oSolicitud.t500_apellido2 = "HUARTE";
        oSolicitud.t500_nombre = "MIKEL";
        oSolicitud.t172_idpais = 1;
        oSolicitud.t173_idprovincia = 20;
        oSolicitud.t500_telefono = "943613817";
        oSolicitud.t500_email = "m.ariztegui@gmail.com";
        DateTime dt = new DateTime(1966, 10, 20);
        oSolicitud.t500_fecnacim = dt;
        oSolicitud.t500_nif = "72667667H";
        oSolicitud.t500_observa = "hola";
        oSolicitud.t530_idorigen = 51;
        oSolicitud.extension = "doc";
        oSolicitud.cv = temp_inBase64;// 

        string sParam = JsonConvert.SerializeObject(oSolicitud);
        client.PostData = sParam;

        var json = client.MakeRequest();

        return json;
    }
    
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string TestSAP(string I_FECHA)
    {
        string json = "";
        try
        {

            //wsSapSaldo.ZVISTA_PA_WS KK = new wsSapSaldo.ZVISTA_PA_WS();
            wsSapSaldo.ZVISTA_PA_WSClient mySAP = new wsSapSaldo.ZVISTA_PA_WSClient();


            //ICredentials credenciales = new NetworkCredential("prg1", "fase10");
            //mySAP.Credentials = credenciales;
            mySAP.ClientCredentials.UserName.UserName = "prg1";
            mySAP.ClientCredentials.UserName.Password = "fase10";

            wsSapSaldo.ZIB_WSRFC_PA_CREAR_TAB oParamIn = new wsSapSaldo.ZIB_WSRFC_PA_CREAR_TAB();
            oParamIn.I_FECHA= I_FECHA;
            wsSapSaldo.ZIB_WSRFC_PA_CREAR_TABResponse oResponse = mySAP.ZIB_WSRFC_PA_CREAR_TAB(oParamIn);


        }
        catch (Exception e)
        {
            json = e.InnerException + e.Message;
        }
        return json;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string TestSAP2(string I_FECHA)
    {
        string json = "ok";
        wsSapSaldo.ZVISTA_PA_WSClient mySAP=null;
        try
        {
            //Manually reconfigure the binding for SAP-BasicAuth
            BasicHttpBinding basicAuthBinding = new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly);
            basicAuthBinding.MaxReceivedMessageSize = 2147483647;
            basicAuthBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            //EndpointAddress basicAuthEndpoint = new EndpointAddress("http://vmsapibd.intranet.ibermatica:8000/sap/bc/srt/wsdl/flv_10002A111AD1/bndg_url/sap/bc/srt/rfc/sap/zvista_pa_ws/111/zvista_pa_ws/zb_vista_pa_ws?sap-client=111");
            //EndpointAddress basicAuthEndpoint = new EndpointAddress("http://vmsapibd.intranet.ibermatica:8000/sap/bc/srt/wsdl/flv_10002A111AD1/bndg_url/sap/bc/srt/rfc/sap/zvista_pa_ws/111/zvista_pa_ws/zb_vista_pa_ws");
            EndpointAddress basicAuthEndpoint = new EndpointAddress("http://VMSAPIBD.intranet.ibermatica:8000/sap/bc/srt/rfc/sap/zvista_pa_ws/111/zvista_pa_ws/zb_vista_pa_ws");
        
            mySAP = new wsSapSaldo.ZVISTA_PA_WSClient(basicAuthBinding, basicAuthEndpoint);
            mySAP.ClientCredentials.UserName.UserName = "prg1";
            mySAP.ClientCredentials.UserName.Password = "fase10";
            wsSapSaldo.ZIB_WSRFC_PA_CREAR_TAB oParamIn = new wsSapSaldo.ZIB_WSRFC_PA_CREAR_TAB();

            wsSapSaldo.ZIB_WSRFC_PA_CREAR_TABResponse oResponse = new wsSapSaldo.ZIB_WSRFC_PA_CREAR_TABResponse();
            oParamIn.I_FECHA = I_FECHA;

            wsSapSaldo.ZVISTA_PA_SUPER oCobro = new wsSapSaldo.ZVISTA_PA_SUPER();

            //wsSapSaldo.ZVISTA_PA_SUPER[] TI_ZVISTA_PA = { };
            wsSapSaldo.ZVISTA_PA_SUPER[] TI_ZVISTA_PA = { oCobro };
            oParamIn.TI_ZVISTA_PA = TI_ZVISTA_PA;

            oResponse = mySAP.ZIB_WSRFC_PA_CREAR_TAB(oParamIn);

            TI_ZVISTA_PA = oResponse.TI_ZVISTA_PA;


            //object oResponse = mySAP.ZIB_WSRFC_PA_CREAR_TAB(oParamIn);


        }
        catch (Exception e)
        {
            json = e.InnerException + e.Message;
        }
        finally
        {
            mySAP.Close();
        }
        return json;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string EscribirFichero(string solicitante)
    {
        string sRes = "OK";

        string path = @"\\IBDATDO\Area\Temp\Mikel\prueba.txt";
        //string path = @"\\IBDATDO\Area\Grupos\SSII\subext\ETT\altages_batch_input.txt";
        StringBuilder result = new StringBuilder();
        DataSet ds = SUPER.DAL.PROCALMA.AltaGestion(null);

        foreach (DataRow oProyecto in ds.Tables[0].Rows)	
        {
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                result.Append(oProyecto[i].ToString());
                result.Append(i == ds.Tables[0].Columns.Count - 1 ? "\r\n" : "\t");
            }
            //result.AppendLine();
        }

        StreamWriter swProyecto = new StreamWriter(path, false);
        swProyecto.WriteLine(result.ToString());
        swProyecto.Close();

        return sRes;
    }

    #region Explotación
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetPaisExpl(string solicitante, string password)
    {
        //Llamada a servicio REST
        var client = new SUPER.BLL.RestClient();

        client.Method = HttpVerb.GET;
        //Obtener paises
        client.EndPoint = @"http://services.intranet.ibermatica/IBSERVICES/QTCN/QTCN.svc/QTCNObtenerPaises?solicitante=" + solicitante + "&password=" + password;

        var json = client.MakeRequest();
        return json;
    }

    #endregion
}
public class Prueba
{
    public string t500_sexo { get; set; }
    public string t500_nombre { get; set; }
    public string t500_apellido1 { get; set; }
    public string t500_apellido2 { get; set; }
    public int? t172_idpais { get; set; }
    public int? t173_idprovincia { get; set; }
    public string t500_telefono { get; set; }
    public string t500_email { get; set; }
    public DateTime? t500_fecnacim { get; set; }
    public string t500_nif { get; set; }
    public string t500_observa { get; set; }
    public int? t530_idorigen { get; set; }
    public string extension { get; set; }
    //public byte[] cv { get; set; }
    public string cv { get; set; }
}
public class Precandidato
{
    public string t500_sexo { get; set; }
    public string t500_nombre { get; set; }
    public string t500_apellido1 { get; set; }
    public string t500_apellido2 { get; set; }
    public int? t172_idpais { get; set; }
    public int? t173_idprovincia { get; set; }
    public string t500_telefono { get; set; }
    public string t500_email { get; set; }
    public DateTime? t500_fecnacim { get; set; }
    public string t500_nif { get; set; }
    public string t500_observa { get; set; }
    public int? t530_idorigen { get; set; }
    public string extension { get; set; }
    public string cv { get; set; }
}

public class SolicitudPrueba
{
    public string solicitante { get; set; }
    public string password { get; set; }
    public string t513_idlinea { get; set; }
    //public Precandidato precandidato { get; set; }
    //public string precandidato { get; set; }
    public string t500_sexo { get; set; }
    public string t500_nombre { get; set; }
    public string t500_apellido1 { get; set; }
    public string t500_apellido2 { get; set; }
    public int? t172_idpais { get; set; }
    public int? t173_idprovincia { get; set; }
    public string t500_telefono { get; set; }
    public string t500_email { get; set; }
    public DateTime? t500_fecnacim { get; set; }
    public string t500_nif { get; set; }
    public string t500_observa { get; set; }
    public int? t530_idorigen { get; set; }
    public string extension { get; set; }
    public string cv { get; set; }
}

public class Candidato
{
    public string t500_sexo { get; set; }
    public string t500_nombre { get; set; }
    public string t500_apellido1 { get; set; }
    public string t500_apellido2 { get; set; }
    public int? t172_idpais { get; set; }
    public int? t173_idprovincia { get; set; }
    public string t500_telefono { get; set; }
    public string t500_email { get; set; }
    public DateTime? t500_fecnacim { get; set; }
    public string t500_nif { get; set; }
    public string t500_observa { get; set; }
    public int? t530_idorigen { get; set; }
    public string extension { get; set; }
    //public byte[] cv { get; set; }
    public string cv { get; set; }
}
public class Solicitud
{
    public string solicitante { get; set; }
    public string password { get; set; }
    public string t513_idlinea { get; set; }
    //public Candidato candidato { get; set; }
    public string candidato { get; set; }
}
