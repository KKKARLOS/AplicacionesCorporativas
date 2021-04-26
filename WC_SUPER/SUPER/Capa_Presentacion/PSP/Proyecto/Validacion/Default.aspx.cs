using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Web;
using System.IO;
using System.Text;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;

using System.Xml;
using System.Xml.XPath;

namespace SUPER
{
public partial class Validacion_OpenProj : System.Web.UI.Page//, ICallbackEventHandler
{
    protected byte[] binaryImage;
    protected MemoryStream msFichero;
    public string EsPostBack = "false";
    public string hayConsumos = "false";
    private XmlDocument docxml = new XmlDocument();
//    private string en = @"http://schemas.microsoft.com/project";
//   private string _callbackResultado = null;
    protected Hashtable htRecursos, htTareas, htItems, htPTs, htFs, htAs, htHFs; 
    public string strTablaHTMLTarea = "<table id='tblDatos'></table>", strTablaHTMLHito = "<table id='tblDatos2'></table>";

    private void Page_Load(object sender, System.EventArgs e)
    {
        Session["bSubido"] = false;
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

            hdnConsumos.Value = Request.QueryString["Cons"].ToString();
            if (hdnConsumos.Value == "S")
                hayConsumos = "true";
            if (!Page.IsCallback)
            {
                if (!Page.IsPostBack)
                {
                    this.hdnResul.Value = "";
                    hdnPSN.Value = Request.QueryString["sPSN"].ToString();
                }
                else
                {
                    Session["bSubido"] = true;
                    //if (EsPostBack == "true")
                    if (this.hdnAccion.Value == "I")
                    {
                        this.hdnResul.Value = Importar(this.chkEstr.Checked);
                        EsPostBack = "true";
                    }
                    else
                        this.hdnResul.Value = Validar(this.chkEstr.Checked);
                }
            }
        }
        catch (System.OutOfMemoryException)
        {
            //Si el archivo a subir es demasiado grande, se produce un error por
            //falta de memoria. La ventana de la barra de progreso ya avisa al usuario de
            //esta situación y cierre esta ventana.
        }
    }
    /// <summary>
    /// Carga en pantalla la estructura técnica contenida en el archivo XML para ver si es válida o no
    /// </summary>
    private string Validar(bool bBorrarEstructura)
    {
        StringBuilder sb = new StringBuilder();//Aqui construiré el HTML de la tabla con la estructura técnica
        StringBuilder sbHito = new StringBuilder();//Aqui construiré el HTML de la tabla de hitos de fecha
        StringBuilder sbTareas = new StringBuilder();//Aqui construiré el churro con la estructura técnica (incluido hitos) para su grabación
        StringBuilder sbRecursos = new StringBuilder();
        int idPSN = -1, iMargen = 0, iCodUne = -1,  iOrden = 1;
        string sTipo, sNota = "", sTaskUID = "", sFIPL = "", sFFPL = "", sFFPR = "", sWorkTarea = "";
        string sCodUser = "", sResourceID = "", sAux="", sMsg="";
        bool bFacturable;
        decimal dETPL = 0, dETPR = 0, dAux=0;
        //SqlConnection oConn = null;
        //SqlTransaction tr = null;

        #region cargo el fichero
        //string strFileNameOnServer = Server.MapPath(".") + @"\" + Session["IDFICEPI_ENTRADA"] + @".xml";
        string strFileNameOnServer = getPath(Session["IDFICEPI_ENTRADA"].ToString(), "xml");
        HttpPostedFile selectedFile = txtArchivo.PostedFile;
        try
        {
            selectedFile.SaveAs(strFileNameOnServer);
            docxml.Load(strFileNameOnServer);
            File.Delete(strFileNameOnServer);
            if (!flEstructuraCorrecta(docxml))
                return "Error@#@La estructura a importar no es correcta.";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al guardar el fichero XML", ex);
        }
        #endregion
        #region Abro transaccion
        //try
        //{
        //    oConn = Conexion.Abrir();
        //    tr = Conexion.AbrirTransaccion(oConn);
        //}
        //catch (Exception ex)
        //{
        //    return "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
        //}
        #endregion
        idPSN = int.Parse(this.hdnPSN.Value);
        htTareas = new Hashtable();
        try
        {
            iCodUne = PROYECTOSUBNODO.GetNodo(null, idPSN);
            bFacturable = PROYECTOSUBNODO.GetFacturable(null, idPSN);

            XmlNodeList TAREAS = docxml.GetElementsByTagName("Tasks");
            XmlNodeList ListaTask = ((XmlElement)TAREAS[0]).GetElementsByTagName("Task");

            XmlNodeList Assignments = docxml.GetElementsByTagName("Assignments");
            XmlNodeList ListaAssignment = ((XmlElement)Assignments[0]).GetElementsByTagName("Assignment");

            #region Leo el XML generado por OpenProj
            sb.Append("<table id='tblDatos' class='texto MANO' style='width: 800px;' mantenimiento='0'>");
            sb.Append("<colgroup><col style='width:430px;' />");//Denominacion
            sb.Append("<col style='width:80px;' />");//ETPL
            sb.Append("<col style='width:70px;' />");//FIPL
            sb.Append("<col style='width:70px;' />");//FFPL
            sb.Append("<col style='width:80px;' />");//ETPR
            sb.Append("<col style='width:70px;' />");//FFPR
            sb.Append("</colgroup>");//
            sb.Append("<tbody>");

            sbHito.Append("<table id='tblDatos2' class='texto MANO' style='width: 550px;' mantenimiento='0'>");
            sbHito.Append("<colgroup><col style='width:25px;' /><col style='width:450px;' /><col style='width:75px;' /></colgroup>");
            sbHito.Append("<tbody>");

            foreach (XmlElement NODO in ListaTask)
            {
                #region recojo datos del XML
                XmlNodeList sUID = NODO.GetElementsByTagName("UID");
                XmlNodeList sNAME = NODO.GetElementsByTagName("Name");
                XmlNodeList sOUTLINELEVEL = NODO.GetElementsByTagName("OutlineLevel");
                XmlNodeList sNot = NODO.GetElementsByTagName("Notes");
                XmlNodeList sStart = NODO.GetElementsByTagName("Start");
                XmlNodeList sFinish = NODO.GetElementsByTagName("Finish");
                XmlNodeList sDuration = NODO.GetElementsByTagName("Duration");
                XmlNodeList sWork = NODO.GetElementsByTagName("Work");
                XmlNodeList sBaseline = NODO.GetElementsByTagName("Baseline");
                XmlNodeList sMilestone = NODO.GetElementsByTagName("Milestone");
                XmlNodeList sWBS = NODO.GetElementsByTagName("WBS");
                XmlNodeList sAttribExten = NODO.GetElementsByTagName("ExtendedAttribute");

                sTaskUID = sUID[0].InnerText;
                dETPL = 0;
                dETPR = 0;
                sFIPL = "";
                sFFPL = "";
                sFFPR = "";
                if (sStart.Count != 0)
                    sFIPL = sStart[0].InnerText.Substring(8, 2) + "/" + sStart[0].InnerText.Substring(5, 2) + "/" + sStart[0].InnerText.Substring(0, 4);
                if (sFinish.Count != 0)
                {
                    sFFPL = sFinish[0].InnerText.Substring(8, 2) + "/" + sFinish[0].InnerText.Substring(5, 2) + "/" + sFinish[0].InnerText.Substring(0, 4);
                    sFFPR = sFFPL;
                }

                //Si hay línea base cojo sus datos
                if (sBaseline.Count != 0)
                {
                    XmlNodeList lAtribs = sBaseline[0].ChildNodes;
                    foreach (XmlNode oNodo in lAtribs)
                    {
                        switch (oNodo.Name)
                        {
                            case "Work":
                                dETPL = flDuracionSUPER(oNodo.InnerText);
                                dETPR = getSumatorioWork(ListaAssignment, sTaskUID); ;
                                break;
                            case "Start":
                                sFIPL = oNodo.InnerText.Substring(8, 2) + "/" + oNodo.InnerText.Substring(5, 2) + "/" + oNodo.InnerText.Substring(0, 4);
                                break;
                            case "Finish":
                                sFFPL = oNodo.InnerText.Substring(8, 2) + "/" + oNodo.InnerText.Substring(5, 2) + "/" + oNodo.InnerText.Substring(0, 4);
                                break;
                        }
                    }
                }
                else
                {//Si no hay línea base, el ETPL hay que obtenerlo como el sumatorio de los elementos <Work> de cada <Assignment> para esa tarea
                    dETPL = getSumatorioWork(ListaAssignment, sTaskUID);
                    dETPR = dETPL;
                }

                if (sNot.Count == 0)
                    sNota = "";
                else
                    sNota = sNot[0].InnerText;
                #endregion

                #region cálculo del tipo y de la identación del elemento
                if (sMilestone.Count != 0 && sMilestone[0].InnerText == "1")
                {
                    //bEsHito = true;
                    sTipo = "HF";
                    iMargen = 0;
                }
                else
                {
                    if (sWBS.Count == 0) sTipo = "T";
                    else sTipo = sWBS[0].InnerText;
                    if (sTipo == "") sTipo = "T";
                    //if (iOrden == 1)
                    //{
                    //    sTipo = "P";
                    //    iMargen = 0;
                    //}
                    if (sTipo == "PT")
                    {
                        sTipo = "P";
                        iMargen = 0;
                    }
                }
                //switch (sTipo)
                //{
                //    case "P":
                //        iMargen = 0;
                //        break;
                //    case "F":
                //        iMargen = 20;
                //        break;
                //    case "A":
                //        if (sOUTLINELEVEL[0].InnerText == "2")
                //            iMargen = 20;
                //        else
                //            if (sOUTLINELEVEL[0].InnerText == "3")
                //                iMargen = 40;
                //            else
                //            {//es un error -> lo traduzco a tarea
                //                sTipo = "T";
                //                iMargen = 20;
                //            }
                //        break;
                //    case "T":
                //        if (sOUTLINELEVEL[0].InnerText == "2")
                //            iMargen = 20;
                //        else
                //            if (sOUTLINELEVEL[0].InnerText == "3")
                //                iMargen = 40;
                //            else
                //            {
                //                if (sOUTLINELEVEL[0].InnerText == "4")
                //                    iMargen = 60;
                //                else
                //                {
                //                    //es un error -> lo traduzco a tarea
                //                    iMargen = 20;
                //                }
                //            }
                //        break;
                //    case "HF":
                //        iMargen = 0;
                //        break;
                //    default://No trae tipo -> lo traduzco a tarea
                //        iMargen = 20;
                //        sTipo = "T";
                //        break;
                //}
                sAux = sOUTLINELEVEL[0].InnerText;
                if (sAux == "" || sAux == "0") sAux = "1";
                iMargen = 20 * (int.Parse(sAux) - 1);
                #endregion

                #region Añado el item a la estructura 
                if (sTipo != "HF")
                {
                    sb.Append("<tr style='height:20px' tipo='" + sTipo + "' id=" + sTaskUID + " mar=" + iMargen.ToString());
                    sb.Append(" nota='" + sNota + "' >");
                    sb.Append("<td style='text-align:left; padding-left:3px;'>");
                    switch (sTipo)
                    {
                        case "P":
                            sb.Append("<img src='../../../../Images/imgProyTecN.gif' title='Proyecto técnico' class='ICO' style='margin-left:" + iMargen + "px'>");
                            break;
                        case "F":
                            sb.Append("<img src='../../../../Images/imgFaseN.gif' title='Fase' class='ICO' style='margin-left:" + iMargen + "px'>");
                            break;
                        case "A":
                            sb.Append("<img src='../../../../Images/imgActividadN.gif' title='Actividad' class='ICO' style='margin-left:" + iMargen + "px'>");
                            break;
                        case "T":
                            sb.Append("<img src='../../../../Images/imgTareaN.gif' title='Tarea' class='ICO' style='margin-left:" + iMargen + "px'>");
                            break;
                    }
                    sb.Append(sNAME[0].InnerText + "</td>");
                    if (sTipo == "T")
                    {
                        sb.Append("<td style='text-align:right;'>" + dETPL.ToString("#,###.##") + "</td>");
                        sb.Append("<td style='text-align:center;'>" + sFIPL + "</td>");
                        sb.Append("<td style='text-align:center;'>" + sFFPL + "</td>");
                        sb.Append("<td style='text-align:right;'>" + dETPR.ToString("#,###.##") + "</td>");
                        sb.Append("<td  style='text-align:right;padding-right:3px;'>" + sFFPR + "</td></tr>");
                        htTareas.Add(sTaskUID, sFIPL + "@" + sFFPL);
                    }
                    else
                        sb.Append("<td colspan=5></td></tr>");
                }
                else
                {
                    sbHito.Append("<tr style='height:20px' tipo='" + sTipo + "' id=" + sTaskUID + " nota='" + sNota + "' >");
                    sbHito.Append("<td><img src='../../../../Images/imgHitoN.gif' border='0' title='Hito' class='ICO'></td>");
                    sbHito.Append("<td>" + sNAME[0].InnerText + "</td>");
                    sbHito.Append("<td>" + sFIPL + "</td></tr>");
                }
                sbTareas.Append(sTipo + "#@#" + sTaskUID + "#@#" + iMargen.ToString() + "#@#" + sNAME[0].InnerText + "#@#" + 
                                dETPL.ToString() + "#@#" + sFIPL + "#@#" + sFFPL + "#@#" + dETPR.ToString() + "#@#" + sFFPR + 
                                "#@#" + sNota + "/#/");
                iOrden++;
                #endregion
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            sbHito.Append("</tbody>");
            sbHito.Append("</table>");

            #endregion

            #region Si hay recursos con nº empleado SUPER y con asignación a tarea, los meto
            bool bHayRecursos = false, bPermiteRecursosPST = PROYECTOSUBNODO.GetAdmiteRecursoPST(null, idPSN);
            //if (bPermiteRecursosPST)
            //{
                sCodUser = ""; 
                sResourceID = "";
                htRecursos = new Hashtable();
                #region recojo los recursos que tengan indicado código de usuario en el campo Initials y los meto en una hashtable
                XmlNodeList Resources = docxml.GetElementsByTagName("Resources");
                XmlNodeList ListaRecursos = ((XmlElement)Resources[0]).GetElementsByTagName("Resource");
                foreach (XmlElement RECURSO in ListaRecursos)
                {
                    XmlNodeList ResourceID = RECURSO.GetElementsByTagName("UID");
                    sResourceID = ResourceID[0].InnerText;

                    XmlNodeList Initials = RECURSO.GetElementsByTagName("Initials");
                    if (Initials.Count != 0)
                    {
                        sCodUser = Initials[0].InnerText.Replace(".","");
                        if (Utilidades.isNumeric(sCodUser))
                        {
                            //sino está en la hashtable (resourceid - t314_idusuario), compruebo que exista y lo introduzco
                            if (!htRecursos.ContainsKey(sResourceID))
                            {
                                //if (Recurso.CodigoRed(int.Parse(sCodUser)) != "")
                                sAux = Recurso.Asignable(int.Parse(sCodUser), idPSN, bPermiteRecursosPST);
                                if (sAux == "OK")
                                {
                                    htRecursos.Add(sResourceID, sCodUser);
                                    bHayRecursos = true;
                                }
                                else
                                    sMsg += sAux;// "El empleado SUPER " + sCodUser + "  no puede ser asignado al proyecto.\n\n";
                            }
                        }
                    }
                }
                #endregion
                #region Creo una lista con asignaciones de recursos a tareas
                if (bHayRecursos)
                {
                    foreach (XmlElement Asignacion in ListaAssignment)
                    {
                        //Recojo el código SUPER del recurso en la tarea
                        sCodUser = "";
                        XmlNodeList ResourceUID = Asignacion.GetElementsByTagName("ResourceUID");
                        sResourceID = ResourceUID[0].InnerText; ;
                        if (sResourceID == "-65535") sResourceID = "0";
                        if (htRecursos[sResourceID] != null)
                            sCodUser = htRecursos[sResourceID].ToString();
                        if (sCodUser != "")
                        {
                            //Recojo el código de la tarea
                            XmlNodeList TaskUID = Asignacion.GetElementsByTagName("TaskUID");
                            sTaskUID = TaskUID[0].InnerText;
                            //Recojo el esfuerzo del recurso en la tarea
                            XmlNodeList WorkTarea = Asignacion.GetElementsByTagName("Work");
                            dAux = flDuracionSUPER(WorkTarea[0].InnerText);
                            sWorkTarea = dAux.ToString();
                            if (sTaskUID != "" && sWorkTarea != "")
                            {
                                //if (htTareas[sTaskUID] != null)
                                //{
                                    sFIPL = "";
                                    sFFPL = "";
                                    sAux = htTareas[sTaskUID].ToString();
                                    if (sAux != "")
                                    {
                                        string[] aFechas = Regex.Split(sAux, @"@");
                                        sFIPL = aFechas[0];
                                        sFFPL = aFechas[1];
                                    }
                                    sbRecursos.Append(sTaskUID + "#@#" + sCodUser + "#@#" + sWorkTarea + "#@#" + sFIPL + "#@#" + sFFPL + "/#/");
                                //}
                            }
                        }
                    }
                }
                #endregion
                //}
            #endregion

            //Cierro transaccion
            //Conexion.CommitTransaccion(tr);

            strTablaHTMLTarea = sb.ToString();
            strTablaHTMLHito = sbHito.ToString();
            this.hdnRecursos.Value = sbRecursos.ToString();
            this.hdnTareas.Value = sbTareas.ToString();
            if (sMsg == "")
                sMsg= "OK";
           return sMsg;
        }
        catch (Exception ex)
        {
            //Conexion.CerrarTransaccion(tr);
            return Errores.mostrarError("Error al cargar la estructura a partir del fichero XML", ex);
        }
        //finally
        //{
        //    Conexion.Cerrar(oConn);
        //}
    }
    /// <summary>
    /// Genera la estructura técnica de un proyecto en SUPER, sobre un proyecto ya existente
    /// Se borran los item que están en SUPER y no están en OpenProj (salvo que tengan consumos)
    /// Si el ítem es nuevo en OpenProj - > se inserta en SUPER
    /// Si el ítem tiene codigo (<Numero1></Numero1>) se updatea en SUPER
    /// </summary>
    private string Importar(bool bBorrarEstructura)
    {
        StringBuilder sb = new StringBuilder();
        int idPSN = -1, iMargen = 0, iCodUne = -1, iNumProy = -1, iPos, iAux;
        int iPT = -1, iFase = -1, iActiv = -1, iTarea = -1, iHito = -1, iOrden = 1;
        string sSituacion = "1", sAux, sTipo, sCodSuperItem = "", sNota = "", sAccion = "", sTaskUID = "", sCodTarea = "";//sMargen = "0", 
        string sFIPL = "", sFFPL = "", sFFPR = "", sWorkTarea = "", sListaTareas="", sListaRecursos="", sDenTarea="", sCodUser="";
        string sMensError = "";
        bool bFacturable, bHayQueUpdatear = false;//, bEsHito = false
        decimal dETPL = 0, dETPR = 0;
        DateTime? dtFIPL = null;
        DateTime? dtFFPL = null;
        SqlConnection oConn = null;
        SqlTransaction tr = null;

        sListaTareas = this.hdnTareas.Value;
        sListaRecursos = this.hdnRecursos.Value;
        htTareas = new Hashtable();

        #region Abro transaccion
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
        }
        #endregion
        idPSN = int.Parse(this.hdnPSN.Value);

        try
        {
            iCodUne = PROYECTOSUBNODO.GetNodo(tr, idPSN);
            if (bBorrarEstructura)
            {
                if (PROYECTOSUBNODO.TieneConsumos(tr, idPSN))
                {
                    Conexion.CerrarTransaccion(tr);
                    return "Error@#@El proyecto tiene consumos. No se puede eliminar la estructura actual.";
                }
                else
                    EstrProy.BorrarEstructura(tr, idPSN);
            }
            else
            {
                //Vamos a meter lo que importamos debajo de lo que ya existe. Para ello obtenemos el mayor orden de los PT existentes
                iOrden = ProyTec.GetMaxOrden(tr, idPSN) + 1;
            }
            bFacturable = PROYECTOSUBNODO.GetFacturable(tr, idPSN);

            #region Inserto y/o updateo los items del proyecto 
            string[] aTareas = Regex.Split(sListaTareas, @"/#/");
            for (int i = 0; i < aTareas.Length - 1; i++)
            {
                string[] aElem = Regex.Split(aTareas[i], @"#@#");
                #region recojo datos del XML
                /*
                aElem[0]: sTipo + "##";         
                aElem[1]: UID + "##";        
                aElem[2]: sMargen + "##";       
                aElem[3]: DENOMINACION + "##";  
                aElem[4]: ETPL + "##";     
                aElem[5]: FIPL + "##";          
                aElem[6]: FFPL + "##";          
                aElem[7]: ETPR + "##";         
                aElem[8]: FFPR + "##";         
                aElem[9]: NOTAS + "##";  
                */
                sTipo = aElem[0];
                sTaskUID = aElem[1];
                iMargen = int.Parse(aElem[2]);
                sDenTarea = aElem[3];
                dETPL = decimal.Parse(aElem[4]);
                dETPR = decimal.Parse(aElem[7]);
                sFIPL = aElem[5];
                sFFPL = aElem[6];
                sFFPR = aElem[8];
                sNota = aElem[9];
                #endregion

                #region Cálculo de códigos padre
                switch (sTipo)
                {
                    case "P":
                        iFase = -1;
                        iActiv = -1;
                        break;
                    case "F":
                        iActiv = -1;
                        break;
                    case "A":
                        if (iMargen != 40) iFase = -1;
                        break;
                    case "T":
                        if (iMargen == 40)
                            iFase = -1;
                        else
                            if (iMargen != 60) { iFase = -1; iActiv = -1; }
                        break;
                    //case "HT":
                    //case "HF":
                    //case "HM":
                    //    iHito = int.Parse(aElem[7]);
                    //    if (sEstado == "D") sCadenaBorrado += sTipo + "@#@" + iHito.ToString() + @"##";//hito
                    //    break;
                }
                #endregion

                #region Inserto o updateo el item de la estructura en BBDD
                if (sCodSuperItem == "" || bBorrarEstructura)
                    sAccion = "I";
                else
                {
                    if (sCodSuperItem == "0.0" || sCodSuperItem == "0,0")
                        sAccion = "I";
                    else
                    {
                        sAccion = "U";
                        //El item esta en OpenProj -> marco en la hashtable para que no lo borre
                        #region obtención del código del elemento updateado
                        sAux = sCodSuperItem.Replace(",", ".");
                        iAux = int.Parse(sAux);
                        ItemsProyecto oItemAux = new ItemsProyecto();
                        switch (sTipo)
                        {
                            case "P":
                                iPT = iAux;
                                oItemAux = (ItemsProyecto)htPTs[iPT];
                                oItemAux.borrar = false;
                                htPTs[iPT] = oItemAux;
                                break;
                            case "F":
                                iFase = iAux;
                                oItemAux = (ItemsProyecto)htFs[iFase];
                                oItemAux.borrar = false;
                                htFs[iFase] = oItemAux;
                                break;
                            case "A":
                                iActiv = iAux;
                                oItemAux = (ItemsProyecto)htAs[iActiv];
                                oItemAux.borrar = false;
                                htAs[iActiv] = oItemAux;
                                break;
                            case "T":
                                iTarea = iAux;
                                oItemAux = (ItemsProyecto)htItems[iTarea];
                                oItemAux.borrar = false;
                                htItems[iTarea] = oItemAux;
                                break;
                            case "HF":
                                iHito = iAux;
                                oItemAux = (ItemsProyecto)htHFs[iHito];
                                oItemAux.borrar = false;
                                htHFs[iHito] = oItemAux;
                                break;
                        }
                        //if (sTipo.Substring(0, 1) == "H")
                        //{
                        //    AsociarTareasHitos(tr, iT305IdProy, iPT, iFase, iActiv, iTarea, iHito, iMargen);
                        //}
                        #endregion
                    }
                }
                if (sAccion == "I")
                {
                    sAux = OpenProj.Insertar(tr, iCodUne, iNumProy, idPSN, sTipo, sDenTarea, iPT, iFase, iActiv, iMargen, iOrden,
                                             sFIPL, sFFPL, dETPL, sFFPR, dETPR, Fechas.primerDiaMes(DateTime.Now).ToShortDateString(), "",
                                             0, bFacturable, false, true, sSituacion, sNota, 0
                                             );
                    #region obtención del código del elemento grabado
                    iPos = sAux.IndexOf("##");
                    iAux = int.Parse(sAux.Substring(0, iPos));
                    switch (sTipo)
                    {
                        case "P":
                            iPT = iAux;
                            break;
                        case "F":
                            iFase = iAux;
                            break;
                        case "A":
                            iActiv = iAux;
                            break;
                        case "T":
                            iTarea = iAux;
                            htTareas.Add(sTaskUID, iTarea.ToString());
                            break;
                        //case "HT":
                        //    iHito = iAux;
                        //    break;
                    }
                    //if (sTipo.Substring(0, 1) == "H")
                    //{
                    //    AsociarTareasHitos(tr, iT305IdProy, iPT, iFase, iActiv, iTarea, iHito, iMargen);
                    //}
                    #endregion
                }
                else
                {//Hay que updatear el item (si hay algún cambio)
                    bHayQueUpdatear = false;

                    #region Mira si hay algún datos distinto para ver si hay que updatear el registro en la BBDD
                    ItemsProyecto oItem = new ItemsProyecto();
                    switch (sTipo)
                    {
                        case "T":
                            oItem = (ItemsProyecto)htItems[iTarea];
                            break;
                        case "P":
                            oItem = (ItemsProyecto)htPTs[iPT];
                            break;
                        case "F":
                            oItem = (ItemsProyecto)htFs[iFase];
                            break;
                        case "A":
                            oItem = (ItemsProyecto)htAs[iActiv];
                            break;
                        case "HF":
                            oItem = (ItemsProyecto)htHFs[iHito];
                            break;
                    }
                    if (oItem.nombre != sDenTarea)
                        bHayQueUpdatear = true;
                    else
                    {
                        if (oItem.descripcion != sNota)
                            bHayQueUpdatear = true;
                        else
                        {
                            if (sTipo == "T")
                            {
                                if (oItem.PRIMER_CONSUMO.Substring(0, 10) != sFIPL)
                                    bHayQueUpdatear = true;
                                else
                                {
                                    if (oItem.FFPR.Substring(0, 10) != sFFPR)
                                        bHayQueUpdatear = true;
                                    else
                                    {
                                        if (oItem.ETPR != dETPR)
                                            bHayQueUpdatear = true;
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    if (bHayQueUpdatear)
                    {
                        OpenProj.Modificar(tr, iCodUne, idPSN, sTipo, sDenTarea, iPT, iFase, iActiv, iTarea, iHito,
                                            iMargen, iOrden, sFIPL, sFFPL, dETPL, sNota);
                    }
                }
                iOrden++;
                #endregion
            }
            #endregion

            #region Si hay recursos con nº empleado SUPER y con asignación a tarea, los meto
            bool bAux = false;
            string[] aRecursos = Regex.Split(sListaRecursos, @"/#/");
            for (int i = 0; i < aRecursos.Length - 1; i++)
            {
                string[] aElem = Regex.Split(aRecursos[i], @"#@#");
                //Recojo el código SUPER del recurso en la tarea
                sTaskUID = aElem[0];
                sCodUser = aElem[1];
                sWorkTarea = aElem[2];
                sFIPL = aElem[3];
                sFFPL = aElem[4];
                if (sFIPL != "") dtFIPL = DateTime.Parse(sFIPL);
                if (sFFPL != "") dtFFPL = DateTime.Parse(sFFPL);
                if (sCodUser != "")
                {
                    sCodTarea = htTareas[sTaskUID].ToString();
                    if (sCodTarea != "" && sWorkTarea != "")
                    {
                        bAux = TareaRecurso.InsertarTEC(tr, int.Parse(sCodTarea), int.Parse(sCodUser), null, null,
                                                 double.Parse(sWorkTarea), dtFIPL, dtFFPL, 
                                                 null, 1, "", "", true, true, idPSN, -1, -1);
                        if (!bAux)
                            sMensError += "Por restricciones del proyecto no es posible asignar el empleado " + sCodUser + " a la tarea " + sCodTarea + "\n";
                        TareaRecurso.UpdateEsfuerzo(tr, int.Parse(sCodTarea), int.Parse(sCodUser), null, double.Parse(sWorkTarea),
                                                    dtFIPL, dtFFPL, null, 1);
                    }
                }
            }
            #endregion

            #region borro los items que estando en SUPER no están en OpenProj (si es tarea que no tenga consumo)
            //if (!bBorrarEstructura)
            //{
            //    ItemsProyecto oItemD = new ItemsProyecto();
            //    //Borrado de Proyectos Técnicos
            //    foreach (DictionaryEntry item in htPTs)
            //    {
            //        oItemD = (ItemsProyecto)item.Value;
            //        if (oItemD.borrar)
            //            ProyTec.Eliminar(tr, oItemD.codPT);
            //    }
            //    //Borrado de Fases
            //    foreach (DictionaryEntry item in htFs)
            //    {
            //        oItemD = (ItemsProyecto)item.Value;
            //        if (oItemD.borrar)
            //            FASEPSP.Delete(tr, oItemD.codFase);
            //    }
            //    //Borrado de Actividades
            //    foreach (DictionaryEntry item in htAs)
            //    {
            //        oItemD = (ItemsProyecto)item.Value;
            //        if (oItemD.borrar)
            //            ACTIVIDADPSP.Delete(tr, oItemD.codActiv);
            //    }
            //    //Borrado de Hitos de fecha
            //    foreach (DictionaryEntry item in htHFs)
            //    {
            //        oItemD = (ItemsProyecto)item.Value;
            //        if (oItemD.borrar)
            //            HITOPSP.Delete(tr, "HF", oItemD.codTarea);
            //    }
            //    //Borrado de Tareas que no tengan consumos
            //    foreach (DictionaryEntry item in htItems)
            //    {
            //        oItemD = (ItemsProyecto)item.Value;
            //        if (oItemD.borrar)
            //        {
            //            if (oItemD.Consumido == 0)
            //                TAREAPSP.Delete(tr, oItemD.codTarea);
            //        }
            //    }
            //}
            #endregion

            //Cierro transaccion
            Conexion.CommitTransaccion(tr);
            if (sMensError == "") sMensError = "OK";
            else sMensError = "OKMSG@#@" + sMensError;
            return sMensError;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            return Errores.mostrarError("Error al cargar la estructura a partir del fichero XML", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
    }
    private bool flEstructuraCorrecta(XmlDocument xmlDoc)
    {
        //Dejamos el código de validación para más adelante
        return true;
    }

    //Dado un nombre de archivo devuelve el path completo donde almacenarlo o leerlo
    private string getPath(string sNomFile, string sExtension)
    {
        string sRes = "";
        //if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() == "D")
        //    sRes = @"D:\Aplicaciones\Openproj\";
        //else
        //if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() == "D")
        //{
        //    sRes = Server.MapPath(@"/SUPER/Upload/");
        //    //sRes = Server.MapPath(@"/KK/Upload/");
        //}
        //else
        //{
        //    //Para distinguir si el acceso es desde fuera de Ibermática
        //    if (Session["strServer"].ToString() == "/")
        //        sRes = Server.MapPath(@"/Upload/");
        //    else
        //        sRes = Server.MapPath(@"/SUPER/Upload/");

        //}
        sRes = Request.PhysicalApplicationPath + @"/Upload/";
        sRes += sNomFile + "_" + DateTime.Now.Ticks.ToString() + "." + sExtension;

        return sRes;
    }
    //Dada una duración en formato OpenProj lo traslada a horas en decimal
    private decimal flDuracionSUPER(string sDuracion)
    {
        decimal dRes=0;
        if (sDuracion != "")
        {
            sDuracion=sDuracion.Substring(2);
            string[] aH = Regex.Split(sDuracion, "H");
            dRes = decimal.Parse(aH[0]);
            //Paso los minutos
            string[] aM = Regex.Split(aH[1], "M");
            dRes+= decimal.Parse(aM[0]) / 60;
        }
        return dRes;
    }
    //Dada una tarea devuelve el sumatorio del trabajo asignado a cada recurso sobre esa tarea
    private decimal getSumatorioWork(XmlNodeList ListaAssignment, string sTaskUID)
    {
        decimal dRes = 0;
        foreach (XmlElement Asig in ListaAssignment)
        {
            //Recojo el código SUPER de la tarea
            XmlNodeList TaskUID = Asig.GetElementsByTagName("TaskUID");
            if (sTaskUID == TaskUID[0].InnerText)
            {
                XmlNodeList Work = Asig.GetElementsByTagName("Work");
                dRes += flDuracionSUPER(Work[0].InnerText);
            }
        }
        return dRes;
    }
}
}