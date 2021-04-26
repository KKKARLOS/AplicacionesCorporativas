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
using SUPER.BLL;
using System.IO;
using SUPER.Capa_Negocio;
using System.Net.Mime;
using System.Text.RegularExpressions;

public partial class Capa_Presentacion_Documentos_Descargar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Control de sesión
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }
        #endregion
        string sTipo = Request.QueryString["sTipo"].ToString();
        string[] sIDDOC = null;
        int nIDDOC=0;
        switch (sTipo)
        {
            case "TIF":
            case "TAD":
            case "TAE":
                sIDDOC = Regex.Split(Request.QueryString["nIDDOC"].ToString(), "datos");
                break;
            case "CVTCUR":
            case "CVTCUR_IMP":
            case "CVTEXAMEN":
            case "CVTCERT":
            case "CVTEXAMEN2":
                sIDDOC = Regex.Split(Request.QueryString["nIDDOC"].ToString(), "datos");
                break;
            default:
                try { nIDDOC = int.Parse(Request.QueryString["nIDDOC"].ToString()); }
                catch (Exception e1)
                {
                    string sError = "Descargar.aspx->Page_Load. Tipo=" + sTipo + " Id documento=" + Request.QueryString["nIDDOC"].ToString();
                    sError += " Ficepi=" + Session["IDFICEPI_PC_ACTUAL"].ToString() + " Error=" + e1.Message;
                    throw (new Exception(sError));
                }
                break;
        }
        #region Pruebas
        //string sPath = "", sNomFich="";

        ////sPath = @"../../Upload/634527150288235168.txt";
        ////sPath = Server.MapPath(@"/Upload/634527150288235168.txt");
        ////sPath = @"d:\inetpub\wwwroot\SUPER\Upload\634527150288235168.txt";
        //if (Request.QueryString["sPath"] != null)
        //{
        //    sNomFich = Utilidades.decodpar(Request.QueryString["sPath"].ToString());
        //    //sNomFich = "pepe.xml";
        //    //sPath = Server.MapPath(@"/SUPER/Upload/") + @"\" + sNomFich; 
        //    if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() == "D")
        //        sPath = Server.MapPath(@"/SUPER/Upload/") + sNomFich;
        //    else
        //        sPath = Server.MapPath(@"/Upload/") + sNomFich;
        //        //sPath = @"d:\inetpub\wwwroot\SUPER\Upload\634527150288235168.txt";
        //        //sPath = Server.MapPath(@"/Upload/634527150288235168.txt");
            
        //}
        ////sPath = Server.MapPath(@"/Upload/634527150288235168.xml");
        ////sNomFich = "634527150288235168.txt";
        #endregion
        string sNombreArchivo = "";
        byte[] ArchivoBinario=null;
        long? t2_iddocumento = null;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Buffer = true;
        if (Request.QueryString["descargaToken"] != null)
            Response.AppendCookie(new HttpCookie("fileDownloadToken", Request.QueryString["descargaToken"].ToString())); //downloadTokenValue will have been provided in the form submit via the hidden input field

        Response.ContentType = "application/octet-stream";
        try
        {
            #region Leer archivo en función de la tabla solicitada
            switch (sTipo)
            {
                case "AS_T"://Asunto de Bitácora de TAREA
                    DOCASU_T oDocAS_T = DOCASU_T.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocAS_T.t602_nombrearchivo;
                    t2_iddocumento = oDocAS_T.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocAS_T.t602_archivo;
                    break;

                case "AC_T"://Acción de Bitácora de TAREA
                    DOCACC_T oDocAC_T = DOCACC_T.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocAC_T.t603_nombrearchivo;
                    t2_iddocumento = oDocAC_T.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocAC_T.t603_archivo;
                    break;
                case "AS_PT"://Asunto de Bitácora de PT
                    DOCASU_PT oDocAS_PT = DOCASU_PT.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocAS_PT.t411_nombrearchivo;
                    t2_iddocumento = oDocAS_PT.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocAS_PT.t411_archivo;
                    break;
                case "AC_PT"://Acción de Bitácora de PT
                    DOCACC_PT oDocAC_PT = DOCACC_PT.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocAC_PT.t412_nombrearchivo;
                    t2_iddocumento = oDocAC_PT.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocAC_PT.t412_archivo;
                    break;
                case "AS"://Asunto de Bitácora
                case "AS_PE"://Asunto de Bitácora
                    DOCASU oDocAS = DOCASU.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocAS.t386_nombrearchivo;
                    t2_iddocumento = oDocAS.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocAS.t386_archivo;
                    break;
                case "AC"://Acción de Bitácora
                case "AC_PE"://Acción de Bitácora
                    DOCACC oDocAC = DOCACC.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocAC.t387_nombrearchivo;
                    t2_iddocumento = oDocAC.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocAC.t387_archivo;
                    break;
                case "IAP_T":
                case "T":
                    DOCUT oDocT = DOCUT.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocT.t363_nombrearchivo;
                    t2_iddocumento = oDocT.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocT.t363_archivo;
                    break;
                case "A": //Actividad
                    DOCUA oDocA = DOCUA.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocA.t365_nombrearchivo;
                    t2_iddocumento = oDocA.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocA.t365_archivo;
                    break;
                case "F": //Fase
                    DOCUF oDocF = DOCUF.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocF.t364_nombrearchivo;
                    t2_iddocumento = oDocF.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocF.t364_archivo;
                    break;
                case "PT": //Proyecto Técnico
                    DOCUPT oDocPT = DOCUPT.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocPT.t362_nombrearchivo;
                    t2_iddocumento = oDocPT.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocPT.t362_archivo;
                    break;
                case "PE": //Proyecto Económico
                case "PSN": //Proyecto Económico
                    DOCUPE oDocPE = DOCUPE.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocPE.t368_nombrearchivo;
                    t2_iddocumento = oDocPE.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocPE.t368_archivo;
                    break;
                case "PEF": //Espacio de acuerdo de Proyecto Económico
                    DOC_ACUERDO_PROY oDocPEF = DOC_ACUERDO_PROY.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocPEF.t640_nombrearchivo;
                    t2_iddocumento = oDocPEF.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocPEF.t640_archivo;
                    break;
                case "HT": //Hito lineal
                case "HM": //Hito discontinuo
                    DOCUH oDocH = DOCUH.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocH.t366_nombrearchivo;
                    t2_iddocumento = oDocH.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocH.t366_archivo;
                    break;
                case "HF": //Hito de fecha
                    DOCUHE oDocHE = DOCUHE.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocHE.t367_nombrearchivo;
                    t2_iddocumento = oDocHE.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocHE.t367_archivo;
                    break;
                case "OF": //ORDEN DE FACTURACIÓN
                    DOCUOF oDocOF = DOCUOF.Select(null, nIDDOC, true);
                    sNombreArchivo = oDocOF.t624_nombrearchivo;
                    t2_iddocumento = oDocOF.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocOF.t624_archivo;
                    break;
                case "PL_OF": //PLANTILLA ORDEN DE FACTURACIÓN
                    PLANTILLADOCUOF oDocPOF = PLANTILLADOCUOF.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocPOF.t631_nombrearchivo;
                    t2_iddocumento = oDocPOF.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocPOF.t631_archivo;
                    break;
                case "EC": //ESPACIO DE COMUNICACION
                    DOCUEC oDocEC = DOCUEC.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocEC.t658_nombrearchivo;
                    t2_iddocumento = oDocEC.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocEC.t658_archivo;
                    break;
                case "DI": //DIALOGO DE ALERTA
                    SUPER.Capa_Datos.DOCDIALOGO oDocDI = SUPER.Capa_Datos.DOCDIALOGO.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocDI.t837_nombrearchivo;
                    t2_iddocumento = oDocDI.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocDI.t837_archivo;
                    break;
                case "SC": //SOLICITUD DE CERTIFICADO
                    SUPER.BLL.DOCSOLICITUD oDocSC = SUPER.BLL.DOCSOLICITUD.Select(null, nIDDOC);//, true);
                    sNombreArchivo = oDocSC.t697_nombrearchivo;
                    t2_iddocumento = oDocSC.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oDocSC.t697_archivo;
                    break;

                case "CVTEXAMEN": //CURVIT EXAMEN
                    if (Utilidades.isNumeric(sIDDOC[0].ToString()))
                    {
                        Examen oDocCVTE = Examen.SelectDoc(null, int.Parse(sIDDOC[0].ToString()), int.Parse(sIDDOC[1].ToString()));
                        sNombreArchivo = oDocCVTE.T591_NDOC;
                        //ArchivoBinario = oDocCVTE.T591_DOC;
                        t2_iddocumento = oDocCVTE.t2_iddocumento;
                        //if (t2_iddocumento == null) ArchivoBinario = oDocCVTE.T591_DOC;
                    }
                    else
                    {
                        SUPER.DAL.DocuAux oDoc = SUPER.DAL.DocuAux.GetDocumento(null, sIDDOC[0].ToString());
                        sNombreArchivo = oDoc.t686_nombre;
                        t2_iddocumento = oDoc.t2_iddocumento;
                    }
                    break;
                case "CVTEXAMEN2": //DESDE VARIABLE DE SESIÓN. NO ESTÁ AÚN GRABADO...
                    sNombreArchivo = sIDDOC[1];
                    ArchivoBinario = (byte[])Session[Utilidades.decodpar(sIDDOC[0])];
                    break;
                case "CVTCERT": //CURVIT CERTIFICADO
                    if (Utilidades.isNumeric(sIDDOC[0].ToString()))
                    {
                        Certificado oDocCVTC = Certificado.SelectDoc(null, int.Parse(sIDDOC[0].ToString()), int.Parse(sIDDOC[1].ToString()));
                        sNombreArchivo = oDocCVTC.T593_NDOC;
                        //ArchivoBinario = oDocCVTC.T593_DOC;
                        t2_iddocumento = oDocCVTC.t2_iddocumento;
                        //if (t2_iddocumento == null) ArchivoBinario = oDocCVTC.T593_DOC;
                    }
                    else
                    {
                        SUPER.DAL.DocuAux oDoc = SUPER.DAL.DocuAux.GetDocumento(null, sIDDOC[0].ToString());
                        sNombreArchivo = oDoc.t686_nombre;
                        t2_iddocumento = oDoc.t2_iddocumento;
                    }
                    break;
                case "TIF": //CURVIT TITULO IDIOMA FICEPI
                    if (Utilidades.isNumeric(sIDDOC[0].ToString()))
                    {
                        TituloIdiomaFic o = TituloIdiomaFic.SelectDoc(null, int.Parse(sIDDOC[0].ToString()));
                        sNombreArchivo = o.T021_NDOC;
                        t2_iddocumento = o.t2_iddocumento;
                    }
                    else
                    {
                        SUPER.DAL.DocuAux oDoc = SUPER.DAL.DocuAux.GetDocumento(null, sIDDOC[0].ToString());
                        sNombreArchivo = oDoc.t686_nombre;
                        t2_iddocumento = oDoc.t2_iddocumento;
                    }
                    break;
                case "CVTCUR": //CURVIT CURSO RECIBIDOS
                    if (Utilidades.isNumeric(sIDDOC[0].ToString()))
                    {
                        Curso oCurso = Curso.SelectDoc(null, int.Parse(sIDDOC[0].ToString()), int.Parse(sIDDOC[1].ToString()));
                        sNombreArchivo = oCurso.T575_NDOC;
                        //ArchivoBinario = oCurso.T575_DOC;
                        t2_iddocumento = oCurso.t2_iddocumento;
                        //if (t2_iddocumento == null) ArchivoBinario = oCurso.T575_DOC;
                    }
                    else
                    {
                        SUPER.DAL.DocuAux oDoc = SUPER.DAL.DocuAux.GetDocumento(null, sIDDOC[0].ToString());
                        sNombreArchivo = oDoc.t686_nombre;
                        t2_iddocumento = oDoc.t2_iddocumento;
                    }
                    break;
                case "CVTCUR_IMP": //CURVIT CURSO IMPARTIDOS
                    if (Utilidades.isNumeric(sIDDOC[0].ToString()))
                    {
                        Curso oCursoImp = Curso.SelectDoc2(null, int.Parse(sIDDOC[0].ToString()), int.Parse(sIDDOC[1].ToString()));
                        sNombreArchivo = oCursoImp.T580_NDOC;
                        //ArchivoBinario = oCursoImp.T580_DOC;
                        t2_iddocumento = oCursoImp.t2_iddocumento;
                        //if (t2_iddocumento == null) ArchivoBinario = oCursoImp.T580_DOC;
                    }
                    else
                    {
                        SUPER.DAL.DocuAux oDoc = SUPER.DAL.DocuAux.GetDocumento(null, sIDDOC[0].ToString());
                        sNombreArchivo = oDoc.t686_nombre;
                        t2_iddocumento = oDoc.t2_iddocumento;
                    }
                    break;
                case "CVTDOCTIT": //CURVIT TITULACION DOCUMENTO TITULO
                    Titulacion oTitulo = Titulacion.SelectDoc(null, nIDDOC, "Tit");
                    sNombreArchivo = oTitulo.NDOC;
                    //ArchivoBinario = oTitulo.DOC;
                    t2_iddocumento = oTitulo.t2_iddocumento;
                    //if (t2_iddocumento == null) ArchivoBinario = oTitulo.DOC;
                    break;
                case "CVTDOCEX": //CURVIT TITULACION DOCUMENTO EXPEDIENTE
                    Titulacion oTitulo1 = Titulacion.SelectDoc(null, nIDDOC, "Ex");
                    sNombreArchivo = oTitulo1.NDOC;
                    //ArchivoBinario = oTitulo1.DOC;
                    t2_iddocumento = oTitulo1.t2_iddocumentoExpte;
                    //if (t2_iddocumento == null) ArchivoBinario = oTitulo1.DOC;
                    break;
                case "TAD": //CURVIT TITULO FICEPI
                    if (Utilidades.isNumeric(sIDDOC[0].ToString()))
                    {
                        TituloFicepi o = TituloFicepi.Select(int.Parse(sIDDOC[0].ToString()));
                        sNombreArchivo = o.T012_NDOCTITULO;
                        t2_iddocumento = o.t2_iddocumento;
                    }
                    else
                    {
                        SUPER.DAL.DocuAux oDoc = SUPER.DAL.DocuAux.GetDocumento(null, sIDDOC[0].ToString());
                        sNombreArchivo = oDoc.t686_nombre;
                        t2_iddocumento = oDoc.t2_iddocumento;
                    }
                    break;
                case "TAE": //CURVIT Expediente TITULO FICEPI
                    if (Utilidades.isNumeric(sIDDOC[0].ToString()))
                    {
                        TituloFicepi o = TituloFicepi.Select(int.Parse(sIDDOC[0].ToString()));
                        sNombreArchivo = o.T012_NDOCEXPDTE;
                        t2_iddocumento = o.t2_iddocumentoExpte;
                    }
                    else
                    {
                        SUPER.DAL.DocuAux oDoc = SUPER.DAL.DocuAux.GetDocumento(null, sIDDOC[0].ToString());
                        sNombreArchivo = oDoc.t686_nombre;
                        t2_iddocumento = oDoc.t2_iddocumento;
                    }
                    break;
            }
            #endregion

            if (t2_iddocumento != null)
                ArchivoBinario = IB.Conserva.ConservaHelper.ObtenerDocumento((long)t2_iddocumento).content;

            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + sNombreArchivo + "\"");
            if (HttpContext.Current.Request.Browser.Browser.ToString() == "Chrome") Response.AddHeader("Content-Length", "999999999999");
            Response.BinaryWrite(ArchivoBinario);

            if (Response.IsClientConnected)
                Response.Flush();
        }
        catch (ConservaException cex)
        {
            this.hdnError.Value = Utilidades.MsgErrorConserva("R", cex);
        }
        //catch (System.Web.HttpException hexc)
        //{
        //}
        catch (Exception ex)
        {
            this.hdnError.Value = "No se ha podido obtener el archivo.<br /><br />Error: " + ex.Message;
            if (ex.InnerException != null)
                this.hdnError.Value += "<br />Detalle error: " + ex.InnerException.Message;
        }
        //Response.Flush();
        finally
        {
            if (this.hdnError.Value == "")
            {
                Response.Close();
                //Response.End();
            }
        }
    }
}
