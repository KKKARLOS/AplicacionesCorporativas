using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;

using EO.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using SUPER.Capa_Negocio;
using System.Runtime.InteropServices;
using System.ServiceModel;
//para generar archivos zip
using Ionic.Zip;

// Para impersonarse
//using System.Web.Security;
//using System.Security.Permissions;
//using System.Security.Principal;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaHTML = "", strTablaHTML2 = "";

    #region inicio Impersonalizar
    /*
    public const int LOGON32_LOGON_INTERACTIVE = 9;
    public const int LOGON32_PROVIDER_DEFAULT = 3;

    WindowsImpersonationContext impersonationContext;

    [DllImport("advapi32.dll")]
    public static extern int LogonUserA(String lpszUserName,
        String lpszDomain,
        String lpszPassword,
        int dwLogonType,
        int dwLogonProvider,
        ref IntPtr phToken);
    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int DuplicateToken(IntPtr hToken,
        int impersonationLevel,
        ref IntPtr hNewToken);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool RevertToSelf();

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern bool CloseHandle(IntPtr handle);
    private bool impersonateValidUser(String userName, String domain, String password)
    {
        WindowsIdentity tempWindowsIdentity;
        IntPtr token = IntPtr.Zero;
        IntPtr tokenDuplicate = IntPtr.Zero;

        if (RevertToSelf())
        {
            if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                LOGON32_PROVIDER_DEFAULT, ref token) != 0)
            {
                if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                {
                    tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                    impersonationContext = tempWindowsIdentity.Impersonate();
                    if (impersonationContext != null)
                    {
                        CloseHandle(token);
                        CloseHandle(tokenDuplicate);
                        return true;
                    }
                }
            }
        }
        if (token != IntPtr.Zero)
            CloseHandle(token);
        if (tokenDuplicate != IntPtr.Zero)
            CloseHandle(tokenDuplicate);
        return false;
    }

    private void undoImpersonation()
    {
        impersonationContext.Undo();
    }
     * */
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 43;
                Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
                Master.nResolucion = 1280;
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Generación de ficheros para IAP";
                Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");

                string strTabla = obtenerNodos(true);
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] != "Error") this.strTablaHTML = aTabla[1];
                else
                {
                    Master.sErrores = aTabla[1];
                    return;
                }

                strTabla = obtenerNodosSel(ConfigurationManager.AppSettings["CRsFavoritosFichIAP"].ToString());
                aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] != "Error") this.strTablaHTML2 = aTabla[1];
                else
                {
                    Master.sErrores = aTabla[1];
                    return;
                }

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
            case ("procesar"):
                sResultado += Procesar(int.Parse(aArgs[1]), aArgs[2]);
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
    private string obtenerNodos(bool bSoloActivos)
    {
        string sResul = "";
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader dr = null;

            dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosTEC(null, (int)Session["UsuarioActual"], false, bSoloActivos);

            sb.Append("<table id='tblDatos' class='texto MA' style='width: 430px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:430px;' /></colgroup>" + (char)10);
            sb.Append("<tbody>");
            string sTootTip = "";
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["IDENTIFICADOR"].ToString() + "'");
                sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)' ");
                sTootTip = "";

                if (Utilidades.EstructuraActiva("SN4")) sTootTip = "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["DES_SN3"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["DES_SN2"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN1")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["DES_SN1"].ToString() + "<br>";
                sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label> " + dr["DES_NODO"].ToString();

                sb.Append("style='height:16px;noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Estructura] body=[" + sTootTip + "] hideselects=[off]\">");
                sb.Append("<td style='padding-left:5px; cursor:pointer;' >" + dr["DENOMINACION"].ToString() + "</td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString();
        }
        catch (System.Exception objError)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al leer el catálogo de nodos ", objError);
        }
        return sResul;
    }
    private string obtenerNodosSel(string sIdsCRsFav)
    {
        string sResul = "";
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader dr = null;

            dr = NODO.FavoritosIAP(sIdsCRsFav);

            sb.Append("<table id='tblDatos2' class='texto MM' style='width: 430px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:430px;' /></colgroup>" + (char)10);
            sb.Append("<tbody>");
            string sTootTip = "";
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["IDENTIFICADOR"].ToString() + "'");
                sb.Append("onclick='mm(event)' onmousedown='DD(event)' ");
                sTootTip = "";

                if (Utilidades.EstructuraActiva("SN4")) sTootTip = "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["DES_SN3"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["DES_SN2"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN1")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["DES_SN1"].ToString() + "<br>";
                sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label> " + dr["DENOMINACION"].ToString();

                sb.Append("style='height:16px;noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Estructura] body=[" + sTootTip + "] hideselects=[off]\">");
                sb.Append("<td style='padding-left:5px; cursor:pointer;' >" + dr["DENOMINACION"].ToString() + "</td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString();
        }
        catch (System.Exception objError)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al leer el catálogo de nodos seleccionados ", objError);
        }
        return sResul;
    }
    private string Procesar(int nAnomes, string strDatos)
    {
        string sRes = "OK", sIdFicepi = Session["IDFICEPI_ENTRADA"].ToString(), sNomArchivoZip="";
        StringBuilder strb = new StringBuilder();

        try
        {
            DataSet ds = NODO.FicherosIAP(null, nAnomes, strDatos);

            #region Crear directorio
            //string path = @"D:\cursos\ficheros";

            //if (!impersonateValidUser("aplicacion_super", @"ibdatdo\aplicacion_super", "@pL1suP3r!"))
            //    return "Error@#@Error en la impersonación";
            //string path = ConfigurationManager.AppSettings["pathFicherosIAP"].ToString();
            string path = Request.PhysicalApplicationPath + "TempImagesGraficos\\FicheroIAP\\" + Session["IDRED"];

            //SUPER.DAL.Log.Insertar("Verificar si el path existe");

            if (Directory.Exists(path)) Directory.Delete(path, true);
            Directory.CreateDirectory(path);
            #endregion

            #region creacion de ficheros
            StringBuilder result = new StringBuilder();
            foreach (DataRow oProyecto in ds.Tables[0].Rows)//Recorro tabla de proyectos		
            {
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    result.Append(oProyecto[i].ToString());
                    result.Append(i == ds.Tables[0].Columns.Count - 1 ? "\r\n" : "\t");
                }
                //result.AppendLine();
            }

            StreamWriter swProyecto = new StreamWriter(path + @"\proyecto.txt", false);
            swProyecto.WriteLine(result.ToString());
            swProyecto.Close();
            //SUPER.DAL.Log.Insertar("Fin tabla Proyectos");

            result.Length=0;

            foreach (DataRow oTarea in ds.Tables[1].Rows)//Recorro tabla de tarea		
            {
                for (int i = 0; i < ds.Tables[1].Columns.Count; i++)
                {
                    result.Append(oTarea[i].ToString());
                    result.Append(i == ds.Tables[1].Columns.Count - 1 ? "\r\n" : "\t");
                }
            }
            StreamWriter swTarea = new StreamWriter(path + @"\tarea.txt", false);
            swTarea.WriteLine(result.ToString());
            swTarea.Close();

            result.Length = 0;

            foreach (DataRow oAtributo in ds.Tables[2].Rows)//Recorro tabla de Atributo		
            {
                for (int i = 0; i < ds.Tables[2].Columns.Count; i++)
                {
                    result.Append(oAtributo[i].ToString());
                    result.Append(i == ds.Tables[2].Columns.Count - 1 ? "\r\n" : "\t");
                }
            }

            StreamWriter swAtributo = new StreamWriter(path + @"\atributo.txt", false);
            swAtributo.WriteLine(result.ToString());
            swAtributo.Close();

            result.Length = 0;

            foreach (DataRow oConsumo in ds.Tables[3].Rows)//Recorro tabla de Consumo		
            {
                for (int i = 0; i < ds.Tables[3].Columns.Count; i++)
                {
                    result.Append(oConsumo[i].ToString());
                    result.Append(i == ds.Tables[3].Columns.Count - 1 ? "\r\n" : "\t");
                }
            }

            StreamWriter swConsumo = new StreamWriter(path + @"\consumo.txt", false);
            swConsumo.WriteLine(result.ToString());
            swConsumo.Close();

            result.Length = 0;

            foreach (DataRow oCliente in ds.Tables[4].Rows)//Recorro tabla de Cliente		
            {
                for (int i = 0; i < ds.Tables[4].Columns.Count; i++)
                {
                    result.Append(oCliente[i].ToString());
                    result.Append(i == ds.Tables[4].Columns.Count - 1 ? "\r\n" : "\t");
                }
            }

            StreamWriter swCliente = new StreamWriter(path + @"\cliente.txt", false);
            swCliente.WriteLine(result.ToString());
            swCliente.Close();

            result.Length = 0;

            foreach (DataRow oEmpleado in ds.Tables[5].Rows)//Recorro tabla de Empleado		
            {
                for (int i = 0; i < ds.Tables[5].Columns.Count; i++)
                {
                    result.Append(oEmpleado[i].ToString());
                    result.Append(i == ds.Tables[5].Columns.Count - 1 ? "\r\n" : "\t");
                }
            }

            StreamWriter swEmpleado = new StreamWriter(path + @"\empleado.txt", false);
            swEmpleado.WriteLine(result.ToString());
            swEmpleado.Close();

            result.Length = 0;

            foreach (DataRow oOrdenFact in ds.Tables[6].Rows)//Recorro tabla de Ordenes de facturación		
            {
                for (int i = 0; i < ds.Tables[6].Columns.Count; i++)
                {
                    result.Append(oOrdenFact[i].ToString());
                    result.Append(i == ds.Tables[6].Columns.Count - 1 ? "\r\n" : "\t");
                }
            }

            StreamWriter swOrdenFact = new StreamWriter(path + @"\ordenesFacturacion.txt", false);
            swOrdenFact.WriteLine(result.ToString());
            swOrdenFact.Close();
            #endregion

            #region Genero un zip con todos los archivos de la carpeta
            sNomArchivoZip = CompressFile(path);
            FileInfo fInfo = new FileInfo(sNomArchivoZip);
            long lTamFich = fInfo.Length;
            long iTamEnBytes = fInfo.Length, TamMaxPermitido = 104857600;//100Mb        
            string sTamMax = ConfigurationManager.AppSettings["TamMaxPack"];
            if (sTamMax != "")
                TamMaxPermitido = long.Parse(sTamMax) * 1024 * 1024;//Paso de Mb a bytes
            if (iTamEnBytes > TamMaxPermitido)
            {
                return "TAMANO_EXCEDIDO@#@" + iTamEnBytes.ToString();
            }
            #endregion

            #region Envío los ficheros al usuario
            //Si el fichero < 10Mb -> envío por correo sino por PaqExpress
            #region Establezco el tamaño del archivo
            double dTamMax = 10;
            bool bPaqExpress = false;
            FileStream fsFichero = System.IO.File.OpenRead(sNomArchivoZip);
            if ((fsFichero.Length / 1048576) > dTamMax)
            {//Si el resultado es > 10Mb enviarlo por PaqExpress
                bPaqExpress = true;
            }
            #endregion

            if (bPaqExpress)
            {
                #region Envío por PaqExpress
                svcSendPack.SendPackClient oPaq = new svcSendPack.SendPackClient();
                try
                {
                    strb.Append("<Pack>");
                    strb.Append("<User>PAQEXPRESS</User>");
                    strb.Append("<Clave>XRJ001-WCF-SUPER-CV.</Clave>");
                    strb.Append("<IdFicepi>" + sIdFicepi + "</IdFicepi>");
                    strb.Append("<FPedido>" + DateTime.Now.ToString() + "</FPedido>");
                    strb.Append("<Profesionales></Profesionales>");
                    strb.Append("<Obs>Paquete de ficheros para IAP</Obs>");
                    strb.Append("<Ref></Ref>");//Nº de referencia para tracking
                    strb.Append("</Pack>");
                    //oPaq.CrearPaqueteCV(new FileInfo(nombreDoc + extension).Name, strb.ToString(), File.OpenRead(pathDirectory + trackingId + @"\" + nombreDoc + extension));
                    FileStream fAux = File.OpenRead(sNomArchivoZip);
                    string sAuxName = new FileInfo(sNomArchivoZip).Name;
                    oPaq.CrearPaqueteCV(sAuxName, strb.ToString(), fAux);
                }
                catch (FaultException<svcSendPack.PackException> cex)
                {
                    string sError = "Error: Código:" + cex.Detail.ErrorCode + ". Descripción: " + cex.Detail.Message;// +" " + cex.Detail.InnerMessage;
                    if (cex.Detail.InnerMessage != "")
                        sError += "\r\nInnerMessage: " + cex.Detail.InnerMessage;
                    sRes = sError + "\r\n";
                }
                catch (Exception ex)
                {
                    sRes = ex.Message;
                }
                finally
                {
                    //Cierre del canal
                    if (oPaq != null && oPaq.State != System.ServiceModel.CommunicationState.Closed)
                    {
                        if (oPaq.State != System.ServiceModel.CommunicationState.Faulted) oPaq.Close();
                        else if (oPaq.State != System.ServiceModel.CommunicationState.Closed) oPaq.Abort();
                    }
                }
                #endregion
            }
            else
            {
                #region Envío por correo
                string strAsunto = "Ficheros generados para IAP";
                string strMensaje = "Se adjunta archivo comprimido que contiene los archivos generados para IAP";
                string sDestinatario = Session["IDRED"].ToString();
                string[] aMail = { strAsunto, strMensaje, sDestinatario, sNomArchivoZip };
                ArrayList aListCorreo = new ArrayList();
                aListCorreo.Add(aMail);
                SUPER.Capa_Negocio.Correo.EnviarCorreosCita(aListCorreo);
                #endregion
            }
            #endregion

            return sRes + "@#@" + path; 
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos.", ex);
        }
    }
    private static string CompressFile(string sDirectorio)
    {
        string finalPath = sDirectorio + "\\Ficheros.zip";
        //try
        //{
        //DirectoryInfo directorySelected = new DirectoryInfo(sDirectorio);
        ZipFile zip = new ZipFile(finalPath);
        zip.AddDirectory(sDirectorio);
        zip.Save();

        return finalPath;
        //}
        //catch (Exception ex)
        //{
        //    throw new FaultException<IBOfficeException>(new IBOfficeException(107, "Error al comprimir la carpeta.", ex.Message));
        //}
    }

}
