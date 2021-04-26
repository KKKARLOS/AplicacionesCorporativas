using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;
using SUPER.Capa_Datos;
using IB.SUPER.Shared;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web;

public partial class Default : System.Web.UI.Page
{
    public string strEnlace = "";
    public string strMsg = "";
    private bool bError = false;
    public bool bEntrar = false;
    //public bool bPreguntar = false;
    public DataSet dsAcceso = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        bool bForaneo = false;
        string sUsuarioWindows = "", sIdFicepi = "", sPregunta = "", sRespuesta = "";
        //cLog miLog = new cLog();
        byte iApp = byte.Parse(ConfigurationManager.AppSettings["CODIGO_APLICACION"].ToString());
        int? t314_idusuario = null;

        //Para pruebas de usuario windows///////
        //sUsuarioWindows = "DOARHUMI";
        //string sKK = SUPER.BLL.Seguridad.DesEncriptar("VwBFAEgAUQBaAEUASQBNAA==");
        //bool bPruebas = false;
        ////////////////////////////////////////
        //25/05/2015 Petición de Javi Asenjo. Para verificar si la petición de entrada a SUPER viene del CURVIT viejo
        //en cuyo caso se le redirije a la pantala de CVT->MiCV
        Session["OLDCURVIT"] = "N";
        if (Request.QueryString["nav"] != null)
        {
            if (Request.QueryString["nav"] == "micv")
                Session["OLDCURVIT"] = "S";
        }

        if (Request.QueryString["uw"] != null)//Es un acceso de un usuario autenticado en el dominio
        {
            sUsuarioWindows = Request.QueryString["uw"];
            //miLog.put("sUsuarioWindows=" + sUsuarioWindows);
        }
        else
        {
            //Session["ua"] = "15979";//Usuario forastero de Victor en explotacion
            //Session["ua"] = "15421";//usuario forastero de Mikel en desarrollo
            if (Session["ua"] != null)//
            {
                //Es un acceso de un usuario anónimo. En la vble de sesión tenemos el IdFicepi del foraneo
                sIdFicepi = Session["ua"].ToString();
                this.hdnIdFicepi.Value = sIdFicepi;
                //miLog.put("sIdFicepi=" + sIdFicepi);
                bForaneo = true;
            }
            else
            {
                if (Request.QueryString["tipo"] != null)
                {
                    if (Utilidades.decodpar(Request.QueryString["tipo"]) == "F") //es una reconexión a un forastero
                    {
                        sIdFicepi = Utilidades.decodpar(Request.QueryString["idf"].ToString());
                        this.hdnIdFicepi.Value = sIdFicepi;
                        bForaneo = true;
                    }
                }
            }
        }

        if (Request.QueryString["in"] != null)
        {
            int nCountVariables = Session.Count;
            for (int i = nCountVariables - 1; i >= 0; i--)
                Session[i] = null;
        }

        #region Nuevo Control de acceso de aplicación y usuario
        try
        {
            string[] sUrlAux = Regex.Split(Request.ServerVariables["URL"], "/");
            bool bEsReconexion = false;

            if (sUrlAux[1].ToUpper() != "SUPER") Session["strServer"] = "/";
            else Session["strServer"] = "/SUPER/";

            if (Session["ADMINISTRADOR_PC_ENTRADA"] == null) Session["ADMINISTRADOR_PC_ENTRADA"] = "";
            if (Session["ADMINISTRADOR_CVT_ENTRADA"] == null) Session["ADMINISTRADOR_CVT_ENTRADA"] = "";

            if (Session["FOTOUSUARIO"] != null) Session["FOTOUSUARIO"] = null;
            Session["MULTIUSUARIO"] = false;
            Session["TiempoMensajeBienvenida"] = 0;
            Session["NOVEDADESMOSTRADAS"] = false;
            if (Session["BIENVENIDAMOSTRADA"] == null)
            {
                Session["BIENVENIDAMOSTRADA"] = false;
                Session["MSGBIENVENIDA"] = "";
            }
            Session["UsuarioActual"] = 0;
            Session["UsuarioActual_CVT"] = 0;
            if (bForaneo)
            {
                //miLog.put("USUARIO.ObtenerDatosAccesoForaneo sIdFicepi= " + sIdFicepi);
                dsAcceso = SUPER.Capa_Negocio.USUARIO.ObtenerDatosAccesoForaneo(sIdFicepi, iApp);
            }
            else
            {
                if (sUsuarioWindows != "")
                {
                    Session["IDRED"] = sUsuarioWindows;
                    Session["PREGUNTA_GRABADA"] = "True";
                }
                else
                {
                    //if (Request.QueryString["codred"] != null)
                    //{
                    //    Session["IDRED"] = Request.QueryString["codred"].ToString();
                    //}
                    //else 
                    if (Request.QueryString["scr"] != null)
                    {
                        Session["IDRED"] = Utilidades.decodpar(Request.QueryString["scr"].ToString());
                        if (Request.QueryString["iu"] != null)
                        {
                            if (Session["ADMINISTRADOR_PC_ENTRADA"].ToString() != "" || Session["ESDIS_ENTRADA"].ToString() == "S")
                                Session["UsuarioActual"] = int.Parse(Utilidades.decodpar(Request.QueryString["iu"].ToString()));
                            if (Session["ADMINISTRADOR_CVT_ENTRADA"].ToString() != "" || Session["ESDIS_ENTRADA"].ToString() == "S")
                                Session["UsuarioActual_CVT"] = int.Parse(Utilidades.decodpar(Request.QueryString["iu"].ToString()));
                            bEsReconexion = true;
                        }
                    }
                    else if (Request.QueryString["iu"] == null)
                    {
                        //Captura del usuario de red.
                        string[] aIdRed = Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\");
                        Array.Reverse(aIdRed);
                        //aIdRed[0] = "DOALASLE";//Leire Alberro
                        //aIdRed[0] = "BIANMOLE";//Leticia Antolin
                        //aIdRed[0] = "BIGOCAJD";//Jose Daniel Gonzalez Carbonero
                        //aIdRed[0] = "BIMAIRHU";//Humberto Marcos Irazabal
                        //aIdRed[0] = "DORESAPA";//PABLO REZOLA
                        //aIdRed[0] = "BIALCASU";//Susana Alonso Cadenas
                        //aIdRed[0] = "DOBEJUAI";//AITZIBER BERAZA
                        //aIdRed[0] = "BAANHENA";//ANTON HERNANDEZ, NATIVIDAD
                        //aIdRed[0] = "DOCACAPI";//Pilar Campano
                        //aIdRed[0] = "MAARSAJI";//Jose Ignacio Arias Sanchez
                        //aIdRed[0] = "BIARGAMC";//MARIA CARMEN ARANSAY
                        //aIdRed[0] = "DORAVIMA";//Michel Ranero
                        //aIdRed[0] = "DOZATEMO";//Monique Zaballa
                        //aIdRed[0] = "DOPAGAMI";//Mikel Paniego
                        //aIdRed[0] = "DOAIOSMI";//Miren Aierbe
                        //aIdRed[0] = "DOIZALVI";//Victor Izaguirre
                        //aIdRed[0] = "MAROMUJM";//José Manuel Rojas
                        //aIdRed[0] = "DOGAGOCO"; //Coro Garin
                        //aIdRed[0] = "DOVEMODA";// David Velázquez Montaña
                        //aIdRed[0] = "MRACVAJM";// Acero Vazquez
                        //aIdRed[0] = "DONAIGYO";//Yolanda Nanclares
                        //aIdRed[0] = "DOGABEJO";//Jon Garcia de Cortazar
                        //aIdRed[0] = "DOREZUJO";//Jokin Rezola
                        //aIdRed[0] = "BIVEALJA";//Jacinta Vega
                        //aIdRed[0] = "DOTAGAJR";//Joserra Tamayo
                        //aIdRed[0] = "BIPEHEMA";//Maitena Pedrera Helguera
                        //aIdRed[0] = "BIHELAGA";//Gaizka Herrero Larrea
                        //aIdRed[0] = "BICHALMA";//Miguel Angel Chiscano Aliste
                        //aIdRed[0] = "MAGOSEMA";//Mar Gomez Sereno
                        //aIdRed[0] = "DOIMNAGA";//Gari Imaz
                        //aIdRed[0] = "DOBEPAMA";//María Berasategui
                        //aIdRed[0] = "MAANLAXA";//Xabi Antoñana
                        //aIdRed[0] = "MACOSAAL";//Almudena Concha
                        //aIdRed[0] = "DOVESASI";//Silvia Vega
                        //aIdRed[0] = "DOGACLOI";//Oiane Garcia Clavijo
                        //aIdRed[0] = "DOLOZUMI";//Miren Loyola
                        //aIdRed[0] = "DOOTGEJO";//Jon Otegui
                        //aIdRed[0] = "DOTOIRIS";//Isidro Toledano
                        //aIdRed[0] = "MACASAPA";//Pablo Carretero
                        //aIdRed[0] = "DOBEBERO";//Rocío Bescós
                        //aIdRed[0] = "DOMOMAJU";//Juan Antonio Morales
                        //aIdRed[0] = "DOLAGAJM";//Josemi Lacalle
                        //aIdRed[0] = "MAINVEPA";//Paloma Indarte
                        //aIdRed[0] = "MAFEGOFR";//Francisco Fernandez Gomez
                        //aIdRed[0] = "DOECGAAI";//Izaskun Echeburua
                        //aIdRed[0] = "MABAPESE";//Sergio Daniel Barranco
                        //aIdRed[0] = "MAANGOMC";//Clementina Andres
                        //aIdRed[0] = "MAROTRAL";//Angel Luis Ruiz Olivares Trejo
                        //aIdRed[0] = "DOCRMAHU";//Hugo de la Cruz
                        //aIdRed[0] = "MAMAGOJE";//JESUS MARTIN GONZALEZ
                        //aIdRed[0] = "DOGAELIN";//Iñigo Garro
                        //aIdRed[0] = "DOPAPAJO";//Jon Parro
                        //aIdRed[0] = "MAPESISC";//Mª Soledad Perez Sixto
                        //aIdRed[0] = "MAGABAEN";//Encarna Galea Ballesteros
                        //aIdRed[0] = "MAPAASSU";//Susana Parra
                        //aIdRed[0] = "BIPIURIS";//ISABEL PIEDRA
                        //aIdRed[0] = "MALAGOMI";//MIGUEL ANGEL DE LARA GONZALEZ
                        //aIdRed[0] = "MACUGOCR";//Cristina Cuesta Gómez
                        //aIdRed[0] = "DOROMAAN";//ANGEL RODRIGUEZ MAIDAGAN
                        //aIdRed[0] = "ARGONGAS";//GASTON GONZALEZ
                        //aIdRed[0] = "MASABEJI";//JIS
                        //aIdRed[0] = "BISOAROS";//Oskar Soto Araneta
                        //aIdRed[0] = "DOLORIIZ";//Izaskun Lopez
                        //aIdRed[0] = "DOASTALU";//Luis Astiazaran
                        //aIdRed[0] = "DOASMOMI";//Mila Asenjo
                        //aIdRed[0] = "DOORESSA";//Saioa Ormaetxea
                        Session["IDRED"] = aIdRed[0];
                    }

                    if (Request.QueryString["iu"] != null)
                    {
                        if (!bEsReconexion)
                        {//Por aqui de momento solo pasa Pablo Carretero (es el único que tiene dos usuarios SUPER)
                            //if (Session["ADMINISTRADOR_PC_ENTRADA"].ToString() != "" || Session["ESDIS_ENTRADA"].ToString() == "S")
                            Session["UsuarioActual"] = int.Parse(Utilidades.decodpar(Request.QueryString["iu"].ToString()));
                            //if (Session["ADMINISTRADOR_CVT_ENTRADA"].ToString() != "" || Session["ESDIS_ENTRADA"].ToString() == "S")
                            Session["UsuarioActual_CVT"] = int.Parse(Utilidades.decodpar(Request.QueryString["iu"].ToString()));
                            bEntrar = true;
                        }
                    }
                }
                dsAcceso = SUPER.Capa_Negocio.USUARIO.ObtenerDatosAcceso(Session["IDRED"].ToString(), byte.Parse(ConfigurationManager.AppSettings["CODIGO_APLICACION"].ToString()));
            }


            #region Casuística de acceso
            if (dsAcceso.Tables[0].Rows.Count == 0) //1ª.- Profesional no existe en Ficepi, o no está dado de alta en Ficepi.
            {
                bError = true;
                strMsg = "Acceso no autorizado. Profesional no registrado(a).";
                throw new Exception(strMsg);
            }
            //else if (dsAcceso.Tables[1].Rows.Count == 0) //2ª.- Profesional no existe en SUPER o tiene bloqueado su acceso
            //{
            //    bError = true;
            //    strMsg = "Acceso no autorizado.|n|nUsuario no registrado(a) o acceso bloqueado";
            //    throw new Exception(strMsg);
            //}
            else if (dsAcceso.Tables[0].Rows.Count > 1) //3ª.- Profesional existe en Ficepi con más de un registro de alta.
            {
                bError = true;
                strMsg = "Acceso no autorizado. Te encuentras registrado(a) incorrectamente.|n|nPor favor, contacta con el CAU.|n|n|nPerdona las molestias.";
                throw new Exception(strMsg);
            }
            else if (!(bool)dsAcceso.Tables[0].Rows[0]["T000_ESTADO"] && (int)dsAcceso.Tables[0].Rows[0]["ADM"] == 0
                && !SUPER.Capa_Negocio.Utilidades.EsAdminProduccionEntrada())
            {//3ª Profesional existe en Ficepi con un solo registro de alta. Aplicación cerrada. Ningún usuario SUPER administrador activo y el usuario de entrada no es administrador.
                bError = true;
                if (dsAcceso.Tables[0].Rows[0]["t001_sexo"].ToString() == "V") strMsg = "Sr. ";
                else strMsg = "Sra. ";

                strMsg += dsAcceso.Tables[0].Rows[0]["t001_apellido1"] + ", el acceso a SUPER se encuentra bloqueado temporalmente.|n|n";
                strMsg += "Motivo:|n|n";
                strMsg += dsAcceso.Tables[0].Rows[0]["T000_MOTIVO"].ToString().Replace(((char)13).ToString() + ((char)10).ToString(), "|n") + "|n|n|n";
                strMsg += "Perdone las molestias.";
                throw new Exception(strMsg);
            }
            //Si es foraneo y no tiene pregunta/respuesta grabada, hay que sacarle pantalla para que lo grabe
            //Ya no hace falta comprobar si tiene pregunta definida porque eso se realiza en la pantalla de login
            //else if (bForaneo &&
            //    (dsAcceso.Tables[1].Rows[0]["t080_pregunta"].ToString() == "" && dsAcceso.Tables[1].Rows[0]["t080_respuesta"].ToString() == ""))
            else if (bForaneo)
            {
                bEntrar = true;
                //bPreguntar = true;
                if (dsAcceso.Tables[0].Rows[0]["t001_sexo"].ToString() == "V") Session["MSGBIENVENIDA"] = "Bienvenido a SUPER, Sr. ";
                else Session["MSGBIENVENIDA"] = "Bienvenida a SUPER, Sra. ";
                Session["MSGBIENVENIDA"] += dsAcceso.Tables[0].Rows[0]["t001_apellido1"] + ".";
            }
            else if (!(bool)dsAcceso.Tables[0].Rows[0]["T000_ESTADO"] && (int)dsAcceso.Tables[0].Rows[0]["ADM"] == 1) //4ª.- Profesional existe en Ficepi con un solo registro de alta. Aplicación cerrada y es administrador activo
            {
                bEntrar = true;
                if (Session["IDFICEPI_ENTRADA"] == null)
                {
                    if (dsAcceso.Tables[0].Rows[0]["t001_sexo"].ToString() == "V") strMsg = "Sr. ";
                    else strMsg = "Sra. ";

                    strMsg += dsAcceso.Tables[0].Rows[0]["t001_apellido1"] + ", el acceso a SUPER se encuentra bloqueado temporalmente.|n|n";
                    strMsg += "Motivo:|n|n";
                    strMsg += dsAcceso.Tables[0].Rows[0]["T000_MOTIVO"].ToString().Replace(((char)13).ToString() + ((char)10).ToString(), "|n") + "|n|n|n";
                    if (dsAcceso.Tables[0].Rows[0]["t001_sexo"].ToString() == "V") strMsg += "Se le autoriza el acceso por ser administrador.";
                    else strMsg += "Se le autoriza el acceso por ser administradora.";
                    if (dsAcceso.Tables[0].Rows[0]["t001_sexo"].ToString() == "V") Session["MSGBIENVENIDA"] = "Bienvenido a SUPER, Sr. ";
                    else Session["MSGBIENVENIDA"] = "Bienvenida a SUPER, Sra. ";
                    Session["MSGBIENVENIDA"] += dsAcceso.Tables[0].Rows[0]["t001_apellido1"] + ".";
                    this.hdnError.Value = strMsg;
                }
            }
            else if ((bool)dsAcceso.Tables[0].Rows[0]["T000_ESTADO"] && dsAcceso.Tables[1].Rows.Count > 1) //5ª.- Profesional existe en Ficepi con un solo registro de alta. Aplicación abierta. Más de un usuario SUPER activo
            {
                bEntrar = true;
                Session["MULTIUSUARIO"] = true;
                if (dsAcceso.Tables[0].Rows[0]["t001_sexo"].ToString() == "V") Session["MSGBIENVENIDA"] = "Bienvenido a SUPER, Sr. ";
                else Session["MSGBIENVENIDA"] = "Bienvenida a SUPER, Sra. ";
                Session["MSGBIENVENIDA"] += dsAcceso.Tables[0].Rows[0]["t001_apellido1"] + ".";
            }
            else if ((bool)dsAcceso.Tables[0].Rows[0]["T000_ESTADO"] && dsAcceso.Tables[1].Rows.Count == 1) //6ª.- Profesional existe en Ficepi con un solo registro de alta. Aplicación abierta. Un usuario SUPER activo.
            {
                bEntrar = true;
                if (dsAcceso.Tables[0].Rows[0]["t001_sexo"].ToString() == "V") Session["MSGBIENVENIDA"] = "Bienvenido a SUPER, Sr. ";
                else Session["MSGBIENVENIDA"] = "Bienvenida a SUPER, Sra. ";
                Session["MSGBIENVENIDA"] += dsAcceso.Tables[0].Rows[0]["t001_apellido1"] + ".";
            }
            else if (!(bool)dsAcceso.Tables[0].Rows[0]["T000_ESTADO"] && SUPER.Capa_Negocio.Utilidades.EsAdminProduccionEntrada())
            {//7ª.- Profesional existe en Ficepi y el usuario entrada inicial es administrador
                bEntrar = true;
                if (dsAcceso.Tables[1].Rows.Count > 1) Session["MULTIUSUARIO"] = true; // El usuario de reconexión es multiusuario
            }
            //else //8ªProfesional existe en Ficepi con un solo registro de alta. Aplicación abierta. Cero usuarios SUPER activos.
            //{
            //    bError = true;
            //    strMsg = "Acceso no autorizado. ";
            //    if (dsAcceso.Tables[0].Rows[0]["t001_sexo"].ToString() == "V") strMsg += "Sr. ";
            //    else strMsg += "Sra. ";
            //    strMsg += dsAcceso.Tables[0].Rows[0]["t001_apellido1"];
            //    if (dsAcceso.Tables[0].Rows[0]["t001_sexo"].ToString() == "V") strMsg += ", no está registrado en SUPER, o se encuentra deshabilitado.";
            //    else strMsg += ", no está registrada en SUPER, o se encuentra deshabilitada.";
            //    strMsg += "|n|nPor favor, contacte con el CAU.|n|n|nPerdone las molestias.";
            //}
            else //8ªProfesional existe en Ficepi con un solo registro de alta. Aplicación abierta. Cero usuarios SUPER activos.
            {
                bEntrar = true;
                if (dsAcceso.Tables[0].Rows[0]["t001_sexo"].ToString() == "V") Session["MSGBIENVENIDA"] = "Bienvenido a SUPER, Sr. ";
                else Session["MSGBIENVENIDA"] = "Bienvenida a SUPER, Sra. ";
                Session["MSGBIENVENIDA"] += dsAcceso.Tables[0].Rows[0]["t001_apellido1"] + ".";
            }
            #endregion
            if (!bError)
            {
                Session["IDRED"] = dsAcceso.Tables[0].Rows[0]["t001_codred"].ToString();

                if (dsAcceso.Tables[1].Rows.Count != 0)
                {
                    t314_idusuario = int.Parse(dsAcceso.Tables[1].Rows[0]["t314_idusuario"].ToString());
                    sPregunta = dsAcceso.Tables[1].Rows[0]["t080_pregunta"].ToString();
                    sRespuesta = dsAcceso.Tables[1].Rows[0]["t080_respuesta"].ToString();
                }
                else
                {
                    t314_idusuario = null;
                    sPregunta = "";
                    sRespuesta = "";
                }

                if (sPregunta != "" && sRespuesta != "")
                    Session["PREGUNTA_GRABADA"] = true;
                else
                    Session["PREGUNTA_GRABADA"] = false;
                //Se establece si se activan los botones de auditoría
                Session["OCULTAR_AUDITORIA"] = "0"; //La variable es "Ocultar Acceso"
                if ((bool)dsAcceso.Tables[0].Rows[0]["t725_accesoauditoria"]) Session["OCULTAR_AUDITORIA"] = "1";
                //Se establece si hay novedades o no que mostrar a los usuarios.
                Session["HAYNOVEDADES"] = "0";
                if ((bool)dsAcceso.Tables[0].Rows[0]["T000_NOVEDADES"]) Session["HAYNOVEDADES"] = "1";
                Session["ALERTASPROY_ACTIVAS"] = (bool)dsAcceso.Tables[0].Rows[0]["t725_alertasproy_activas"];
                Session["FORANEOS"] = (bool)dsAcceso.Tables[0].Rows[0]["t725_foraneos"];
                //if (Session["IDRED"].ToString() == "DOIZALVI" 
                //    || Session["IDRED"].ToString() == "DOARHUMI"
                //    || Session["IDRED"].ToString() == "DOGAGOCO")
                //    Session["FORANEOS"] = true;
                //else
                //    Session["FORANEOS"] = (bool)dsAcceso.Tables[0].Rows[0]["t725_foraneos"];

                Session["FIGURASFORANEOS"] = dsAcceso.Tables[0].Rows[0]["t725_figurasforaneos"];
            }
            //if (bEntrar)
            //    Utilidades.InsertUsuario(Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\")[Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\").GetLength(0) - 1]);
            //else
            //    Utilidades.DeleteUsuario(Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\")[Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\").GetLength(0) - 1]);
        }
        catch (Exception ex)
        {
            if (bError) strMsg = ex.Message;
            else strMsg = Errores.mostrarError("Error al comprobar los datos de acceso del usuario y la aplicación:", ex);
            this.hdnError.Value = strMsg;
            bError = true;
        }
        #endregion

        #region Control de identificación del usuario
        if (!bError)
        {
            try
            {
                //Para la gestión de la reconexión
                Session["IDFICEPI_PC_RECONECTADO"] = "";
                Session["IDFICEPI_CVT_RECONECTADO"] = "";
                if (!bForaneo)//if (Session["IDRED"].ToString() != "")
                {
                    #region Usuario Windows
                    Recurso objRec = new Recurso();
                    bool bIdentificado = objRec.ObtenerRecurso(Session["IDRED"].ToString(), ((int)Session["UsuarioActual"] == 0) ? null : (int?)int.Parse(Session["UsuarioActual"].ToString()));

                    if (bIdentificado)
                    {

                        #region Variables de sesión para el entorno de Producción y Comercialización
                        if (Session["IDFICEPI_ENTRADA"] == null
                            || Session["ADMINISTRADOR_PC_ENTRADA"].ToString() != ""
                            || Session["ESDIS_ENTRADA"].ToString() == "S")
                        {

                            if (Session["IDFICEPI_ENTRADA"] == null)
                            {
                                #region Valores PC que se cargan una única vez al entrar
                                Session["ADMINISTRADOR_PC_ENTRADA"] = objRec.AdminPC;//A->Administrador; SA->SuperAdministrador de producción y Comercialización
                                if (objRec.esDIS)
                                    Session["ESDIS_ENTRADA"] = "S";
                                else
                                    Session["ESDIS_ENTRADA"] = "N";
                                Session["IDRED_ENTRADA"] = Session["IDRED"];
                                Session["SEXO_ENTRADA"] = objRec.sSexo;
                                Session["NOMBRE_ENTRADA"] = objRec.Nombre;
                                Session["APELLIDO1_ENTRADA"] = objRec.Apellido1;
                                Session["APELLIDO2_ENTRADA"] = objRec.Apellido2;
                                Session["DES_EMPLEADO_ENTRADA"] = objRec.Nombre + " " + objRec.Apellido1 + " " + objRec.Apellido2;
                                Session["NUM_EMPLEADO_ENTRADA"] = objRec.IdUsuario;

                                Session["CARRUSEL1024"] = objRec.t314_carrusel1024;
                                Session["AVANCE1024"] = objRec.t314_avance1024;
                                Session["RESUMEN1024"] = objRec.t314_resumen1024;
                                Session["DATOSRES1024"] = objRec.t314_datosres1024;
                                Session["FICHAECO1024"] = objRec.t314_fichaeco1024;
                                Session["SEGRENTA1024"] = objRec.t314_segrenta1024;
                                Session["AVANTEC1024"] = objRec.t314_avantec1024;
                                Session["ESTRUCT1024"] = objRec.t314_estruct1024;
                                Session["FOTOPST1024"] = objRec.t314_fotopst1024;
                                Session["PLANT1024"] = objRec.t314_plant1024;
                                Session["CONST1024"] = objRec.t314_const1024;
                                Session["IAPFACT1024"] = objRec.t314_iapfact1024;
                                Session["IAPDIARIO1024"] = objRec.t314_iapdiario1024;
                                Session["CUADROMANDO1024"] = objRec.t314_cuadromando1024;
                                Session["RECIBIRMAILS"] = objRec.t314_recibirmails;
                                Session["MULTIVENTANA"] = objRec.t314_multiventana;
                                Session["BTN_FECHA"] = objRec.t001_botonfecha;
                                Session["MONEDA_VDC"] = objRec.t422_idmoneda_vdc;
                                Session["DENOMINACION_VDC"] = objRec.t422_denominacionimportes_vdc;
                                Session["MONEDA_VDP"] = objRec.t422_idmoneda_vdp;
                                Session["DENOMINACION_VDP"] = objRec.t422_denominacionimportes_vdp;
                                Session["CALCULOONLINE"] = objRec.t314_calculoonline;
                                Session["CARGAESTRUCTURA"] = objRec.t314_cargaestructura;
                                #endregion
                                #region Avisos
                                ////Se establece si hay avisos o no que mostrar al usuario.
                                Session["HAYAVISOS"] = "0";
                                SqlDataReader dr2 = USUARIOAVISOS.SelectByT314_idusuario(null, (int)Session["NUM_EMPLEADO_ENTRADA"]);
                                if (dr2.Read())
                                    Session["HAYAVISOS"] = "1";
                                dr2.Close();
                                dr2.Dispose();
                                #endregion
                            }
                            else//Para la gestión del reconectado en la cabecera de la master y del usuario actual en CVT
                            {
                                Session["IDFICEPI_PC_RECONECTADO"] = objRec.IdFicepi;
                                Session["UsuarioActual_CVT"] = objRec.IdUsuario;
                            }
                            Session["IDFICEPI_PC_ACTUAL"] = objRec.IdFicepi;
                            Session["NOMBRE"] = objRec.Nombre;
                            Session["APELLIDO1"] = objRec.Apellido1;
                            Session["APELLIDO2"] = objRec.Apellido2;
                            Session["DES_EMPLEADO"] = objRec.Nombre + " " + objRec.Apellido1 + " " + objRec.Apellido2;
                            Session["ADMINISTRADOR_PC_ACTUAL"] = objRec.AdminPC;//A->Administrador; SA->SuperAdministrador de producción y Comercialización
                            Session["NUEVOGASVI"] = objRec.t314_nuevogasvi;
                            Session["NodoActivo"] = "";
                            Session["DesNodoActivo"] = "";
                            Session["UsuarioActual"] = objRec.IdUsuario;
                            Session["NUM_PROYECTO"] = "";
                            Session["ID_PROYECTOSUBNODO"] = "";
                            Session["MODOLECTURA_PROYECTOSUBNODO"] = false;
                            Session["RTPT_PROYECTOSUBNODO"] = false;
                            Session["MONEDA_PROYECTOSUBNODO"] = "";
                            #region Variables para IAP
                            ////VARIABLES NECESARIAS PARA IAP
                            Session["IDFICEPI_IAP"] = objRec.IdFicepi;
                            Session["NUM_EMPLEADO_IAP"] = objRec.IdUsuario;
                            //Session["DES_EMPLEADO_IAP"] = objRec.Apellido1 + " " + objRec.Apellido2 + ", " + objRec.Nombre;
                            Session["DES_EMPLEADO_IAP"] = objRec.Nombre + " " + objRec.Apellido1 + " " + objRec.Apellido2;
                            Session["IDRED_IAP"] = objRec.sCodRed;
                            Session["JORNADA_REDUCIDA"] = objRec.JornadaReducida;
                            Session["CONTROLHUECOS"] = objRec.ControlHuecos;
                            Session["IDCALENDARIO_IAP"] = objRec.IdCalendario;
                            Session["DESCALENDARIO_IAP"] = objRec.DesCalendario;
                            Session["COD_CENTRO"] = objRec.CodCentro;
                            Session["DES_CENTRO"] = objRec.DesCentro;
                            Session["FEC_ULT_IMPUTACION"] = (objRec.FecUltImputacion.HasValue) ? ((DateTime)objRec.FecUltImputacion.Value).ToShortDateString() : null;
                            Session["FEC_ALTA"] = objRec.FecAlta.ToShortDateString();
                            Session["FEC_BAJA"] = (objRec.FecBaja.HasValue) ? ((DateTime)objRec.FecBaja.Value).ToShortDateString() : null;
                            if (Session["reconectar_iap"] == null) Session["reconectar_iap"] = "";
                            if (Session["reconectar_msg_iap"] == null) Session["reconectar_msg_iap"] = 0;
                            if (Session["perfil_iap"] == null) Session["perfil_iap"] = "";
                            Session["UMC_IAP"] = (objRec.UMCIAP.HasValue) ? (int?)objRec.UMCIAP.Value : DateTime.Today.AddMonths(-1).Year * 100 + DateTime.Today.AddMonths(-1).Month;
                            Session["NHORASRED"] = objRec.nHorasJorRed;
                            Session["FECDESRED"] = (objRec.FecDesdeJorRed.HasValue) ? ((DateTime)objRec.FecDesdeJorRed.Value).ToShortDateString() : null;
                            Session["FECHASRED"] = (objRec.FecHastaJorRed.HasValue) ? ((DateTime)objRec.FecHastaJorRed.Value).ToShortDateString() : null;
                            Session["aSemLab"] = objRec.sSemanaLaboral;
                            //FIN VARIABLES NECESARIAS PARA IAP
                            #endregion
                            Session["IMPORTACIONGASVI"] = objRec.t314_importaciongasvi;
                        }
                        #endregion
                        #region Variables de sesión para el entorno de CVT
                        if (Session["IDFICEPI_ENTRADA"] == null
                            || Session["ADMINISTRADOR_CVT_ENTRADA"].ToString() != ""
                            || Session["ESDIS_ENTRADA"].ToString() == "S")
                        {
                            if (Session["IDFICEPI_ENTRADA"] == null)
                            {//Valores que se cargan una vez al entrar
                                Session["ADMINISTRADOR_CVT_ENTRADA"] = objRec.AdminCVT;
                                if (objRec.esDIS)
                                    Session["ESDIS_ENTRADA"] = "S";
                                else
                                    Session["ESDIS_ENTRADA"] = "N";
                                Session["IDRED_ENTRADA"] = Session["IDRED"];
                                Session["NOMBRE_ENTRADA"] = objRec.Nombre;
                                Session["APELLIDO1_ENTRADA"] = objRec.Apellido1;
                                Session["APELLIDO2_ENTRADA"] = objRec.Apellido2;
                                Session["DES_EMPLEADO_ENTRADA"] = objRec.Nombre + " " + objRec.Apellido1 + " " + objRec.Apellido2;
                                Session["BTN_FECHA"] = objRec.t001_botonfecha;
                            }
                            else//Para la gestión del reconectado en la cabecera de la master
                                Session["IDFICEPI_CVT_RECONECTADO"] = objRec.IdFicepi;
                            Session["IDFICEPI_CVT_ACTUAL"] = objRec.IdFicepi;
                            Session["NOMBRE"] = objRec.Nombre;
                            Session["APELLIDO1"] = objRec.Apellido1;
                            Session["APELLIDO2"] = objRec.Apellido2;
                            Session["DES_EMPLEADO"] = objRec.Nombre + " " + objRec.Apellido1 + " " + objRec.Apellido2;
                            Session["ADMINISTRADOR_CVT_ACTUAL"] = objRec.AdminCVT;
                            Session["CVFINALIZADO"] = objRec.CVFinalizado;
                            Session["RESPONSABLE_CVEXCLUSION"] = objRec.Responsable_CvExclusion;
                            Session["PROFESIONAL_CVEXCLUSION"] = objRec.Profesional_CvExclusion;
                            //El usuario SUPER se usa en la parte CVT para mostrar las Experiencias Profesionales
                            Session["UsuarioActual_CVT"] = objRec.IdUsuario;
                        }
                        #endregion
                        if (Session["IDFICEPI_ENTRADA"] == null)
                            Session["IDFICEPI_ENTRADA"] = objRec.IdFicepi;


                        #region Registro de actividad

                        string sNavegador = "";
                        string sVersion = "";
                        #region Detección de navegador
                        try
                        {
                            System.Web.HttpBrowserCapabilities browser = Request.Browser;
                            switch (browser.Browser.ToUpper())
                            {
                                case "IE":
                                case "INTERNETEXPLORER":
                                case "FIREFOX":
                                case "CHROME":
                                    sNavegador = browser.Browser.ToUpper();
                                    sVersion = browser.Version;
                                    break;
                                case "APPLEMAC-SAFARI":
                                    string sUserAgent = Request.ServerVariables["http_user_agent"].ToUpper();
                                    if (sUserAgent.IndexOf("CHROME") != -1)
                                    {
                                        sNavegador = "CHROME";
                                        sVersion = sUserAgent.Substring(sUserAgent.IndexOf("CHROME") + 7, sUserAgent.IndexOf("SAFARI") - sUserAgent.IndexOf("CHROME") - 8);
                                    }
                                    else
                                    {
                                        sNavegador = "SAFARI";
                                        sVersion = sUserAgent.Substring(sUserAgent.IndexOf("VERSION") + 8, sUserAgent.IndexOf("SAFARI") - sUserAgent.IndexOf("VERSION") - 8);
                                    }
                                    break;
                            }
                        }
                        catch { }
                        #endregion

                        if (objRec.IdUsuario.ToString() != Session["NUM_EMPLEADO_ENTRADA"].ToString() && !User.IsInRole("DIS"))
                        {
                            CONEXIONES.Insert(null, DateTime.Now, objRec.IdUsuario, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()), sNavegador, sVersion);
                        }
                        else
                        {
                            if (Session["REGISTRADO"] == null)
                            {//Para que si me reconecto como yo mismo no me vuelva a contar
                                CONEXIONES.Insert(null, DateTime.Now, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()), null, sNavegador, sVersion);
                                Session["REGISTRADO"] = "1";
                            }
                        }
                        #endregion
                        #region Novedades
                        Session["NOVEDADESLEIDAS"] = "0";
                        if (Session["HAYNOVEDADES"].ToString() == "1")
                        {//Las novedades se mostrarán en base al usuario inicial, no al reconectado
                            SqlDataReader dr = LECTURANOVEDAD.Catalogo(byte.Parse(ConfigurationManager.AppSettings["CODIGO_APLICACION"]), int.Parse(Session["IDFICEPI_ENTRADA"].ToString()), 1, 0);
                            if (dr.Read())
                                Session["NOVEDADESLEIDAS"] = "1";
                            dr.Close();
                            dr.Dispose();
                        }
                        #endregion

                        Session["DIAMANTE"] = objRec.bDiamanteMovil;
                        Session["SEXOUSUARIO"] = objRec.sSexo;
                        Session["TIPORECURSO"] = objRec.sTipoRecurso;
                        Session["FOTOUSUARIO"] = objRec.t001_foto;

                        Session["CODWEATHER"] = objRec.T010_CODWEATHER;
                        Session["NOMWEATHER"] = objRec.T010_NOMWEATHER;
                        Session["MostrarMensajeBienvenida"] = (objRec.t314_nsegmb > 0) ? true : false;
                        Session["TiempoMensajeBienvenida"] = objRec.t314_nsegmb;

                        string sUS = "0";
                        if (Request.QueryString["iu"] != null) sUS = "1";

                        try
                        {
                            Hashtable ht = IB.SUPER.Shared.Utils.ParseQuerystring(Request.QueryString.ToString());
                            string modulo = String.Empty;

                            if (ht["modulo"] != null)
                                modulo = ht["modulo"].ToString();

                            switch (modulo)
                            {
                                //Módulo SIC. Se mandan los parámetros codificados desde los correos en base 64 ASCII
                                case "SIC":
                                    strEnlace = Session["strServer"] + "Capa_Presentacion/SIC/redir.ashx?" + Request.QueryString.ToString();
                                    break;
                                //Módulo CVT. Se mandan los parámetros codificados desde los correos en base 64 ASCII
                                case "CVT":
                                    strEnlace = Session["strServer"] + "Capa_Presentacion/CVT/redir.ashx?" + Request.QueryString.ToString();
                                    break;
                                default:
                                    strEnlace = Session["strServer"] + "Capa_Presentacion/bsInicio/Default.aspx?sUS=" + sUS;
                                    break;
                            }
                        }
                        catch (Exception) { }

                        //Home de Super
                        if (strEnlace == "")
                            strEnlace = Session["strServer"] + "Capa_Presentacion/bsInicio/Default.aspx?sUS=" + sUS;

                        Recurso.CargarRoles(objRec.IdUsuario.ToString(), objRec.IdFicepi.ToString(), objRec.AdminPC, objRec.AdminPer, objRec.AdminCVT);

                        //TODO --> asignar figuras SIC y eliminar esto
                        if (Session["IDRED"].ToString().ToUpper() == "DOASANJA" || Session["IDRED"].ToString().ToUpper() == "DOVEMODA")
                            Roles.AddUserToRole(User.Identity.Name, "SPM");
                    }
                    else
                    {//Para cuando el profesional no tiene usuario SUPER
                        bIdentificado = objRec.ObtenerRecursoFICEPI(Session["IDRED"].ToString());

                        if (bIdentificado)
                        {
                            #region Variables de sesión para el entorno de CVT
                            if (Session["IDFICEPI_ENTRADA"] == null
                                || Session["ADMINISTRADOR_CVT_ENTRADA"].ToString() != ""
                                || Session["ESDIS_ENTRADA"].ToString() == "S")
                            {
                                if (Session["IDFICEPI_ENTRADA"] == null)
                                {//Valores que se cargan una vez al entrar
                                    Session["ADMINISTRADOR_CVT_ENTRADA"] = objRec.AdminCVT;
                                    if (objRec.esDIS)
                                        Session["ESDIS_ENTRADA"] = "S";
                                    else
                                        Session["ESDIS_ENTRADA"] = "N";
                                    Session["IDRED_ENTRADA"] = Session["IDRED"];
                                    Session["NOMBRE_ENTRADA"] = objRec.Nombre;
                                    Session["APELLIDO1_ENTRADA"] = objRec.Apellido1;
                                    Session["APELLIDO2_ENTRADA"] = objRec.Apellido2;
                                    Session["DES_EMPLEADO_ENTRADA"] = objRec.Nombre + " " + objRec.Apellido1 + " " + objRec.Apellido2;
                                    Session["BTN_FECHA"] = objRec.t001_botonfecha;
                                }
                                Session["NOVEDADESLEIDAS"] = "0";
                                Session["ADMINISTRADOR_CVT_ACTUAL"] = objRec.AdminCVT;
                                Session["IDFICEPI_CVT_ACTUAL"] = objRec.IdFicepi;
                                Session["IDFICEPI_PC_ACTUAL"] = objRec.IdFicepi;
                                Session["NOMBRE"] = objRec.Nombre;
                                Session["APELLIDO1"] = objRec.Apellido1;
                                Session["APELLIDO2"] = objRec.Apellido2;
                                Session["DES_EMPLEADO"] = objRec.Nombre + " " + objRec.Apellido1 + " " + objRec.Apellido2;
                                Session["CVFINALIZADO"] = objRec.CVFinalizado;
                                Session["RESPONSABLE_CVEXCLUSION"] = objRec.Responsable_CvExclusion;
                                Session["PROFESIONAL_CVEXCLUSION"] = objRec.Profesional_CvExclusion;
                                Session["MostrarMensajeBienvenida"] = true;
                                Session["TiempoMensajeBienvenida"] = 4;
                                Session["HAYAVISOS"] = "0";
                            }
                            #endregion
                            if (Session["IDFICEPI_ENTRADA"] == null)
                                Session["IDFICEPI_ENTRADA"] = objRec.IdFicepi;

                            #region Registro de actividad
                            //if (objRec.IdUsuario.ToString() != Session["NUM_EMPLEADO_ENTRADA"].ToString() && !User.IsInRole("DIS"))
                            //{
                            //    CONEXIONES.Insert(null, DateTime.Now, objRec.IdUsuario, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
                            //}
                            //else
                            //{
                            //    if (Session["REGISTRADO"] == null)
                            //    {//Para que si me reconecto como yo mismo no me vuelva a contar
                            //        CONEXIONES.Insert(null, DateTime.Now, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()), null);
                            //        Session["REGISTRADO"] = "1";
                            //    }
                            //}
                            #endregion
                            #region Novedades
                            if (Session["HAYNOVEDADES"].ToString() == "1")
                            {
                                //SqlDataReader dr = LECTURANOVEDAD.Catalogo(byte.Parse(ConfigurationManager.AppSettings["CODIGO_APLICACION"]), objRec.IdFicepi, 1, 0);
                                //Las novedades se mostrarán en base al usuario inicial, no al reconectado
                                SqlDataReader dr = LECTURANOVEDAD.Catalogo(byte.Parse(ConfigurationManager.AppSettings["CODIGO_APLICACION"]), int.Parse(Session["IDFICEPI_ENTRADA"].ToString()), 1, 0);
                                if (dr.Read())
                                    Session["NOVEDADESLEIDAS"] = "1";
                                dr.Close();
                                dr.Dispose();
                            }
                            #endregion

                            bEntrar = true;
                            string sUS = "0";
                            if (Request.QueryString["iu"] != null) sUS = "1";
                            strEnlace = Session["strServer"] + "Capa_Presentacion/bsInicio/Default.aspx?sUS=" + sUS;
                            Recurso.CargarRoles("", objRec.IdFicepi.ToString(), "", "", objRec.AdminCVT);
                        }
                        else
                        {
                            strMsg = "No se han podido obtener los datos del usuario '" + Session["IDRED"].ToString() + "'";
                        }
                    }
                    #endregion
                }
                else
                {
                    if (sIdFicepi != "")
                    {
                        #region Usuario Foraneo
                        //miLog.put("Usuario foraneo");
                        SUPER.Capa_Negocio.Recurso objRec = new SUPER.Capa_Negocio.Recurso();
                        //miLog.put("Antes de obtener datos del usuario " + t314_idusuario.ToString());
                        bool bIdentificado = objRec.ObtenerRecurso("", t314_idusuario);
                        //miLog.put("Despues de obtener datos del usuario " + t314_idusuario.ToString());
                        if (bIdentificado)
                        {
                            #region Variables de sesión para el entorno de Producción y Comercialización
                            if (Session["IDFICEPI_ENTRADA"] == null
                                || Session["ADMINISTRADOR_PC_ENTRADA"].ToString() != ""
                                || Session["ESDIS_ENTRADA"].ToString() == "S")
                            {

                                if (Session["IDFICEPI_ENTRADA"] == null)
                                {
                                    #region Valores PC que se cargan una única vez al entrar
                                    Session["ADMINISTRADOR_PC_ENTRADA"] = objRec.AdminPC;//A->Administrador; SA->SuperAdministrador de producción y Comercialización
                                    if (objRec.esDIS)
                                        Session["ESDIS_ENTRADA"] = "S";
                                    else
                                        Session["ESDIS_ENTRADA"] = "N";
                                    Session["IDRED_ENTRADA"] = Session["IDRED"];
                                    Session["NOMBRE_ENTRADA"] = objRec.Nombre;
                                    Session["APELLIDO1_ENTRADA"] = objRec.Apellido1;
                                    Session["APELLIDO2_ENTRADA"] = objRec.Apellido2;
                                    Session["DES_EMPLEADO_ENTRADA"] = objRec.Nombre + " " + objRec.Apellido1 + " " + objRec.Apellido2;
                                    Session["NUM_EMPLEADO_ENTRADA"] = objRec.IdUsuario;

                                    Session["CARRUSEL1024"] = objRec.t314_carrusel1024;
                                    Session["AVANCE1024"] = objRec.t314_avance1024;
                                    Session["RESUMEN1024"] = objRec.t314_resumen1024;
                                    Session["DATOSRES1024"] = objRec.t314_datosres1024;
                                    Session["FICHAECO1024"] = objRec.t314_fichaeco1024;
                                    Session["SEGRENTA1024"] = objRec.t314_segrenta1024;
                                    Session["AVANTEC1024"] = objRec.t314_avantec1024;
                                    Session["ESTRUCT1024"] = objRec.t314_estruct1024;
                                    Session["FOTOPST1024"] = objRec.t314_fotopst1024;
                                    Session["PLANT1024"] = objRec.t314_plant1024;
                                    Session["CONST1024"] = objRec.t314_const1024;
                                    Session["IAPFACT1024"] = objRec.t314_iapfact1024;
                                    Session["IAPDIARIO1024"] = objRec.t314_iapdiario1024;
                                    Session["CUADROMANDO1024"] = objRec.t314_cuadromando1024;
                                    Session["RECIBIRMAILS"] = objRec.t314_recibirmails;
                                    Session["MULTIVENTANA"] = objRec.t314_multiventana;
                                    Session["BTN_FECHA"] = objRec.t001_botonfecha;
                                    Session["MONEDA_VDC"] = objRec.t422_idmoneda_vdc;
                                    Session["DENOMINACION_VDC"] = objRec.t422_denominacionimportes_vdc;
                                    Session["MONEDA_VDP"] = objRec.t422_idmoneda_vdp;
                                    Session["DENOMINACION_VDP"] = objRec.t422_denominacionimportes_vdp;
                                    Session["CALCULOONLINE"] = objRec.t314_calculoonline;
                                    Session["CARGAESTRUCTURA"] = objRec.t314_cargaestructura;
                                    #endregion
                                    #region Avisos
                                    ////Se establece si hay avisos o no que mostrar al usuario.
                                    Session["HAYAVISOS"] = "0";
                                    SqlDataReader dr2 = USUARIOAVISOS.SelectByT314_idusuario(null, (int)Session["NUM_EMPLEADO_ENTRADA"]);
                                    if (dr2.Read())
                                        Session["HAYAVISOS"] = "1";
                                    dr2.Close();
                                    dr2.Dispose();
                                    #endregion
                                }

                                Session["IDFICEPI_PC_ACTUAL"] = objRec.IdFicepi;
                                Session["NOMBRE"] = objRec.Nombre;
                                Session["APELLIDO1"] = objRec.Apellido1;
                                Session["APELLIDO2"] = objRec.Apellido2;
                                Session["DES_EMPLEADO"] = objRec.Nombre + " " + objRec.Apellido1 + " " + objRec.Apellido2;
                                Session["ADMINISTRADOR_PC_ACTUAL"] = objRec.AdminPC;//A->Administrador; SA->SuperAdministrador de producción y Comercialización
                                Session["NUEVOGASVI"] = objRec.t314_nuevogasvi;
                                Session["NodoActivo"] = "";
                                Session["DesNodoActivo"] = "";
                                Session["UsuarioActual"] = objRec.IdUsuario;
                                Session["NUM_PROYECTO"] = "";
                                Session["ID_PROYECTOSUBNODO"] = "";
                                Session["MODOLECTURA_PROYECTOSUBNODO"] = false;
                                Session["RTPT_PROYECTOSUBNODO"] = false;
                                Session["MONEDA_PROYECTOSUBNODO"] = "";
                                #region Variables para IAP
                                ////VARIABLES NECESARIAS PARA IAP
                                Session["IDFICEPI_IAP"] = objRec.IdFicepi;
                                Session["NUM_EMPLEADO_IAP"] = objRec.IdUsuario;
                                //Session["DES_EMPLEADO_IAP"] = objRec.Apellido1 + " " + objRec.Apellido2 + ", " + objRec.Nombre;
                                Session["DES_EMPLEADO_IAP"] = objRec.Nombre + " " + objRec.Apellido1 + " " + objRec.Apellido2;
                                Session["IDRED_IAP"] = objRec.sCodRed;
                                Session["JORNADA_REDUCIDA"] = objRec.JornadaReducida;
                                Session["CONTROLHUECOS"] = objRec.ControlHuecos;
                                Session["IDCALENDARIO_IAP"] = objRec.IdCalendario;
                                Session["DESCALENDARIO_IAP"] = objRec.DesCalendario;
                                Session["COD_CENTRO"] = objRec.CodCentro;
                                Session["DES_CENTRO"] = objRec.DesCentro;
                                Session["FEC_ULT_IMPUTACION"] = (objRec.FecUltImputacion.HasValue) ? ((DateTime)objRec.FecUltImputacion.Value).ToShortDateString() : null;
                                Session["FEC_ALTA"] = objRec.FecAlta.ToShortDateString();
                                Session["FEC_BAJA"] = (objRec.FecBaja.HasValue) ? ((DateTime)objRec.FecBaja.Value).ToShortDateString() : null;
                                if (Session["reconectar_iap"] == null) Session["reconectar_iap"] = "";
                                if (Session["reconectar_msg_iap"] == null) Session["reconectar_msg_iap"] = 0;
                                if (Session["perfil_iap"] == null) Session["perfil_iap"] = "";
                                Session["UMC_IAP"] = (objRec.UMCIAP.HasValue) ? (int?)objRec.UMCIAP.Value : DateTime.Today.AddMonths(-1).Year * 100 + DateTime.Today.AddMonths(-1).Month;
                                Session["NHORASRED"] = objRec.nHorasJorRed;
                                Session["FECDESRED"] = (objRec.FecDesdeJorRed.HasValue) ? ((DateTime)objRec.FecDesdeJorRed.Value).ToShortDateString() : null;
                                Session["FECHASRED"] = (objRec.FecHastaJorRed.HasValue) ? ((DateTime)objRec.FecHastaJorRed.Value).ToShortDateString() : null;
                                Session["aSemLab"] = objRec.sSemanaLaboral;
                                //FIN VARIABLES NECESARIAS PARA IAP
                                #endregion
                                Session["IMPORTACIONGASVI"] = objRec.t314_importaciongasvi;
                            }
                            #endregion
                            if (Session["IDFICEPI_ENTRADA"] == null)
                                Session["IDFICEPI_ENTRADA"] = objRec.IdFicepi;

                            #region Registro de actividad

                            string sNavegador = "";
                            string sVersion = "";
                            #region Detección de navegador
                            try
                            {
                                System.Web.HttpBrowserCapabilities browser = Request.Browser;
                                switch (browser.Browser.ToUpper())
                                {
                                    case "IE":
                                    case "INTERNETEXPLORER":
                                    case "FIREFOX":
                                    case "CHROME":
                                        sNavegador = browser.Browser.ToUpper();
                                        sVersion = browser.Version;
                                        break;
                                    case "APPLEMAC-SAFARI":
                                        string sUserAgent = Request.ServerVariables["http_user_agent"].ToUpper();
                                        if (sUserAgent.IndexOf("CHROME") != -1)
                                        {
                                            sNavegador = "CHROME";
                                            sVersion = sUserAgent.Substring(sUserAgent.IndexOf("CHROME") + 7, sUserAgent.IndexOf("SAFARI") - sUserAgent.IndexOf("CHROME") - 8);
                                        }
                                        else
                                        {
                                            sNavegador = "SAFARI";
                                            sVersion = sUserAgent.Substring(sUserAgent.IndexOf("VERSION") + 8, sUserAgent.IndexOf("SAFARI") - sUserAgent.IndexOf("VERSION") - 8);
                                        }
                                        break;
                                }
                            }
                            catch { }
                            #endregion

                            if (objRec.IdUsuario.ToString() != Session["NUM_EMPLEADO_ENTRADA"].ToString() && !User.IsInRole("DIS"))
                            {
                                SUPER.Capa_Negocio.CONEXIONES.Insert(null, DateTime.Now, objRec.IdUsuario, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()), sNavegador, sVersion);
                            }
                            else
                            {
                                if (Session["REGISTRADO"] == null)
                                {//Para que si me reconecto como yo mismo no me vuelva a contar
                                    SUPER.Capa_Negocio.CONEXIONES.Insert(null, DateTime.Now, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()), null, sNavegador, sVersion);
                                    Session["REGISTRADO"] = "1";
                                }
                            }
                            #endregion
                            #region Novedades
                            if (Session["HAYNOVEDADES"].ToString() == "1")
                            {
                                //SqlDataReader dr = LECTURANOVEDAD.Catalogo(byte.Parse(ConfigurationManager.AppSettings["CODIGO_APLICACION"]), objRec.IdFicepi, 1, 0);
                                //Las novedades se mostrarán en base al usuario inicial, no al reconectado
                                SqlDataReader dr = SUPER.Capa_Negocio.LECTURANOVEDAD.Catalogo(byte.Parse(ConfigurationManager.AppSettings["CODIGO_APLICACION"]), int.Parse(Session["IDFICEPI_ENTRADA"].ToString()), 1, 0);
                                if (dr.Read())
                                    Session["NOVEDADESLEIDAS"] = "1";
                                dr.Close();
                                dr.Dispose();
                            }
                            #endregion

                            Session["NodoActivo"] = "";
                            Session["DesNodoActivo"] = "";
                            Session["UsuarioActual"] = objRec.IdUsuario;
                            Session["NUM_PROYECTO"] = "";
                            Session["ID_PROYECTOSUBNODO"] = "";
                            Session["MODOLECTURA_PROYECTOSUBNODO"] = false;
                            Session["RTPT_PROYECTOSUBNODO"] = false;
                            Session["MONEDA_PROYECTOSUBNODO"] = "";
                            string sUS = "0";
                            if (Request.QueryString["iu"] != null) sUS = "1";
                            strEnlace = Session["strServer"] + "Capa_Presentacion/bsInicio/Default.aspx?sUS=" + sUS;
                            //miLog.put("Antes de cargar Roles del foraneo");
                            SUPER.Capa_Negocio.Recurso.CargarRolesForaneo(objRec.Nif, objRec.IdUsuario.ToString(), objRec.IdFicepi.ToString(), objRec.AdminPC, objRec.AdminPer, objRec.AdminCVT);
                            //miLog.put("strEnlace= " + strEnlace);
                            SUPER.BLL.FORANEO.RegistrarAcceso(objRec.IdFicepi);
                        }
                        #endregion
                    }
                    else
                        strMsg = "Usuario de Windows no identificado";
                }
            }
            catch (Exception ex)
            {
                bError = true;
                strMsg += Errores.mostrarError("Error al obtener los datos del usuario", ex);
                this.hdnError.Value = strMsg;
            }
        }
        #endregion

        #region carga de la variable CertificadoPath
        //Antes la ruta del certificado se cogia por MapPath en la clase correo
        //Como ahora se envian correos en un segundo hilo cargamos la ruta del certificado en una variable del config
        ConfigurationManager.AppSettings["CertificadoPath"] = System.Web.HttpContext.Current.Request.MapPath("~/Certificado");
        #endregion

        #region Carga de la vble para Reporting Services

        string[] link = Regex.Split(Request.ServerVariables["URL"], "/");
        if (link[1].ToUpper() != "SUPER")
        {//Accedemos desde la red interna
            if (ConfigurationManager.AppSettings["ENTORNO"].ToUpper() == "E")
                Session["ServidorSSRS"] = ConfigurationManager.AppSettings["SERVIDOR_RS_E"];// http://ibsqlcorp01/reportserver?/SUPER/
            else
                Session["ServidorSSRS"] = ConfigurationManager.AppSettings["SERVIDOR_RS_D"];// http://ibdisdesa01/reportserver?/SUPER/
        }
        else
        {//Accedemos desde la extranet
            Session["ServidorSSRS"] = ConfigurationManager.AppSettings["SERVIDOR_RS_EX"];//https://informes.ibermatica.com/SUPER
        }

        #endregion

        try
        {
            //if (User.IsInRole("PPIAP30")) this.hdnMostrarIAP30.Value = "S";

            if (dsAcceso != null) dsAcceso.Dispose();
        }
        catch (Exception ex)
        {
            strMsg += Errores.mostrarError("Error al eliminar los datos de acceso del usuario y la aplicación:", ex);
            this.hdnError.Value = strMsg;
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void logoff()
    {
        HttpContext.Current.Session.Abandon();
    }


}
