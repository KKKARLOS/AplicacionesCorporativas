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

using System.IO;
using SUPER.Capa_Negocio;
using System.Net.Mime;
using System.Xml;
using System.Xml.XPath;
using System.Text;

public partial class Capa_Presentacion_Documentos_DescargaDirecta : System.Web.UI.Page
{
    private string en = "http://schemas.microsoft.com/project";
    private XmlDocument docxml = new XmlDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }

        string sPSN = Request.QueryString["sPSN"].ToString();
        int idPSN = int.Parse(sPSN);
        string sRTPT = Request.QueryString["RTPT"].ToString();
        //string sNomFich = Utilidades.decodpar(Request.QueryString["sPath"].ToString());

        int iOrden = 1, iAssignmentUID = 1, iNumDias = 0, iNumDiasBase = 0;//, iUID=0
        StringBuilder sb = new StringBuilder();
        string sCodItem, sDenProy = "", sCodProy = "", sIniProy = "", sFinProy = "", sResponsable = "", sPdte = "";
        //string sMaxUnits = "0.01000000000000000020816681711721685132943093776702880859375";
        string sMaxUnits = "1";
        decimal dParticipacion = 0, dParticipacionBase = 0, dEsfuerzo = 0, dEsfuerzoBase = 0, dEsfAcum = 0;
        string sCodUser = "";
        bool bIncluirRecursos = false, bConLineBase = true;

        if (Request.QueryString["base"].ToString() == "N")
            bConLineBase = false;
        //try
        //{
            if (sPSN != "")
            {
                #region Obtengo datos del proyectos a exportar
                idPSN = int.Parse(sPSN);
                SqlDataReader drP = PROYECTO.fgGetDatosProy4(idPSN);
                if (drP.Read())
                {
                    sCodProy = drP["t301_idproyecto"].ToString();
                    sDenProy = "Proyecto " + int.Parse(sCodProy).ToString("#,###") + ". " + drP["t301_denominacion"].ToString();
                    sIniProy = drP["t301_fiprev"].ToString();
                    sFinProy = drP["t301_ffprev"].ToString();
                    sResponsable = drP["Profesional"].ToString();
                }
                drP.Close();
                drP.Dispose();
                #endregion
            }
            if (sCodProy != "")
            {
                #region Meto la cabecera del XML leyéndola de los datos de la plantilla guardados en la tabla T681_PLANTILLA_OPENPROJ
                //string sArchivoPlant = getPath("Plantilla_" + Session["IDFICEPI_ENTRADA"].ToString(), "xml");
                SqlDataReader dr = OpenProj.GetPlantilla(null, 1);
                if (dr.Read())
                {
                    //byte[] fileData = (byte[])dr.GetValue(1);
                    byte[] fileData;
                    if (dr["t2_iddocumento"].ToString() != "")
                    {
                        fileData = IB.Conserva.ConservaHelper.ObtenerDocumento((long)dr.GetValue(0)).content;
                    }
                    else
                    {
                        //fileData = (byte[])dr.GetValue(1);
                        throw new Exception("No se ha encontrado la plantilla de OpenProj en el repositorio de documentos");
                    }

                    MemoryStream ms = new MemoryStream();
                    ms.Write(fileData, 0, fileData.Length);
                    ms.Seek(0, SeekOrigin.Begin);//Hay que ponerse al principio porque sino el XML no se carga correctamente
                    docxml.Load(ms);
                    ms.Close();
                }
                dr.Close();
                dr.Dispose();
                XmlNode nodoRaiz = docxml.DocumentElement;
                XmlNodeList LN = docxml.GetElementsByTagName("Name");
                XmlNode nodoAux = LN.Item(0);
                nodoAux.InnerText = "Proyecto " + sCodProy;

                LN = docxml.GetElementsByTagName("Title");
                nodoAux = LN.Item(0);
                nodoAux.InnerText = sDenProy;

                LN = docxml.GetElementsByTagName("Manager");
                nodoAux = LN.Item(0);
                nodoAux.InnerText = sResponsable;

                LN = docxml.GetElementsByTagName("StartDate");
                nodoAux = LN.Item(0);
                nodoAux.InnerText = OpenProj.flGetFechaOpenProj(sIniProy, "ID");

                LN = docxml.GetElementsByTagName("FinishDate");
                nodoAux = LN.Item(0);
                nodoAux.InnerText = OpenProj.flGetFechaOpenProj(sFinProy, "ID");

                LN = docxml.GetElementsByTagName("CurrentDate");
                nodoAux = LN.Item(0);
                nodoAux.InnerText = OpenProj.flGetFechaOpenProj(DateTime.Now.ToShortDateString(), "IJ");

                //Pongo calendario 24x7 porque sino tiene en cuenta los fines de semana y recalcula la fecha de fin de las tareas
                LN = docxml.GetElementsByTagName("CalendarUID");
                nodoAux = LN.Item(0);
                nodoAux.InnerText = OpenProj.fgGetCalendario();//"2";

                System.Xml.XmlElement Tasks = docxml.CreateElement("Tasks", en);
                System.Xml.XmlElement Assignments = docxml.CreateElement("Assignments", en);
                System.Xml.XmlElement Resources = docxml.CreateElement("Resources", en);
                #endregion
                #region cargo los recursos asociados a todas las tareas del proyecto
                //Cargo un recurso ficticio para tareas no asignadas a recursos
                //Resources.AppendChild(CrearResourceXml(docxml, "0", "0", "N", "No asignado", sMaxUnits, "3", "0", "3", "0", "0", "0"));
                Resources.AppendChild(OpenProj.CrearResourceXml(docxml, "1", "1", ".", "Ocupación diaria ", sMaxUnits, "3", "0", "3", "0", "0", "0"));

                //Resources.AppendChild(CrearResourceXml(docxml, "1", "1", "Perdiguero", "0.01000000000000000020816681711721685132943093776702880859375", "3", "0", "3", "0", "0", "10000"));
                if (bIncluirRecursos)
                {
                    //Para cada recurso hay que meter un calendario
                    XmlNodeList Calendarios = docxml.GetElementsByTagName("Calendars");
                    XmlNode nodoCalendario = Calendarios.Item(0);//Calendarios.Count - 1

                    SqlDataReader dr2 = OpenProj.GetProfesionales(null, idPSN, true);
                    while (dr2.Read())
                    {
                        sCodUser = dr2["t314_idusuario"].ToString();
                        System.Xml.XmlNode Calendario =
                                    nodoCalendario.AppendChild(OpenProj.CrearCalendarioUsuarioXml(docxml, sCodUser, dr2["Profesional"].ToString(), "2"));
                        Resources.AppendChild(OpenProj.CrearResourceXml(docxml, sCodUser, sCodUser, sCodUser, dr2["Profesional"].ToString(), sMaxUnits,
                                                                "3", "0", "3", "0", "0", "0"));
                    }
                    dr2.Close();
                    dr2.Dispose();
                }
                #endregion
                #region Meto los items del proyecto
                bool bHayRecursos = false;
                SqlDataReader dr1;
                if (sRTPT == "0")
                    dr1 = OpenProj.GetEstructura(null, idPSN);
                else
                    dr1 = OpenProj.GetEstructura(null, idPSN, int.Parse(Session["UsuarioActual"].ToString()));
                while (dr1.Read())
                //foreach (int iKey in htItems.Keys)
                {
                    //ItemsProyecto oItem = new ItemsProyecto();
                    //oItem = (ItemsProyecto)htItems[iKey];
                    ItemsProyecto oItem = new ItemsProyecto((int)dr1["codPT"], (int)dr1["codFase"], (int)dr1["codActiv"], (int)dr1["codTarea"],
                                                            dr1["nombre"].ToString(), dr1["descripcion"].ToString(), dr1["tipo"].ToString(),
                                                            iOrden++,/*(int)dr1["orden"],*/ dr1["FIPL"].ToString(), dr1["FFPL"].ToString(),
                                                            decimal.Parse(dr1["ETPL"].ToString()), dr1["PRIMER_CONSUMO"].ToString(),
                                                            dr1["ULTIMO_CONSUMO"].ToString(),
                                                            dr1["FFPR"].ToString(), decimal.Parse(dr1["ETPR"].ToString()),
                                                            decimal.Parse(dr1["Consumido"].ToString()), dr1["SITUACION"].ToString(),
                                                            ((int)dr1["FACTURABLE"] == 0) ? false : true, (int)dr1["MARGEN"]);
                    sCodItem = "";
                    string sIdent = OpenProj.flGetIdentacion(oItem.margen);
                    switch (oItem.tipo)
                    {
                        case "P":
                            sCodItem = oItem.codPT.ToString();
                            break;
                        case "F":
                            sCodItem = oItem.codFase.ToString();
                            break;
                        case "A":
                            sCodItem = oItem.codActiv.ToString();
                            break;
                        case "T":
                        case "HF":
                            sCodItem = oItem.codTarea.ToString();
                            break;
                    }
                    //if (sCodItem == "143637")
                    //    sCodItem = sCodItem;
                    if (sCodItem != "" && oItem.FIPL != "" && oItem.FFPL != "")
                    {
                        //if (oItem.codTarea == 148600) oItem.Consumido = 6;
                        //if (oItem.Consumido != 0)
                        sPdte = OpenProj.flPdteOpenProj(oItem.ETPR, oItem.Consumido);
                        //else
                        //sPdte = "";
                        bHayRecursos = false;
                        sCodUser = "0";
                        dEsfAcum = 0;
                        iNumDiasBase = OpenProj.flDuracionDias(oItem.FIPL, oItem.FFPL);
                        iNumDias = OpenProj.flDuracionDias(oItem.PRIMER_CONSUMO, oItem.FFPR);
                        dEsfuerzo = oItem.EsfuerzoHoras;
                        dEsfuerzoBase = oItem.ETPL;
                        //Hay que dividir por el nº de dias de duración de la tarea
                        if (iNumDiasBase != 0)
                            dParticipacionBase = dEsfuerzoBase / (iNumDiasBase * 8);
                        else
                            dParticipacionBase = 0;
                        //if (dParticipacionBase > 1) dParticipacionBase = 1;

                        if (iNumDias != 0)
                            dParticipacion = dEsfuerzo / (iNumDias * 8);
                        else
                            dParticipacion = 0;
                        //if (dParticipacion > 1) dParticipacion = 1;

                        Tasks.AppendChild(
                            OpenProj.CrearTareaXml(docxml, sCodItem, oItem.orden.ToString(), oItem.nombre, oItem.descripcion, oItem.tipo,
                                          sIdent, "0", OpenProj.flGetFechaOpenProj(oItem.PRIMER_CONSUMO, "IJ"),
                                          OpenProj.flGetFechaOpenProj(oItem.FFPR, "FJ"),
                                          OpenProj.flDuracionOpenProj(0, oItem.PRIMER_CONSUMO, oItem.FFPR),
                                          "7", "0", "2", sPdte, oItem.Consumido,
                                          oItem.FIPL, oItem.FFPL, oItem.ETPL, oItem.ETPR, dParticipacion, bConLineBase)
                                  );

                        #region Meto los recursos asociados a cada tarea y las asignaciones de la situación actual
                        //Resources.AppendChild(CrearResourceXml(docxml, "1", "1", "Perdiguero", "0.01000000000000000020816681711721685132943093776702880859375", "3", "0", "3", "0", "0", "10000"));
                        if (bIncluirRecursos)
                        {
                            #region Incluyendo recursos
                            if (oItem.tipo == "T")
                            {//De momento como no tengo claro lo que hay que hacer con los recursos, solo los calculo para las tareas
                                //Luego, si hace falta, ya pondré los de PT, F y A. Según la reunión del 8/9/2011 con Iñigo Garro
                                //basta con asociar los recursos a las tareas

                                //Leer de BBDD los recursos asignados a la tarea
                                SqlDataReader dr3 = OpenProj.GetProfesionalesTarea(null, int.Parse(sCodItem), true);
                                while (dr3.Read())
                                {
                                    bHayRecursos = true;
                                    sCodUser = dr3["t314_idusuario"].ToString();
                                    //Segundo hay que calcular el porcentaje de participación del usuario en la tarea
                                    //  Para ello yo haria t336_etp / oItem.ETPR (si fuera división por cero, devolver cero)
                                    if (dr3["t336_etp"].ToString() == "") dEsfuerzo = 0;
                                    else dEsfuerzo = decimal.Parse(dr3["t336_etp"].ToString());
                                    dEsfAcum += dEsfuerzo;
                                    //if (oItem.ETPR == 0 || dr3["t336_etp"].ToString()=="") 
                                    //    dParticipacion = 0;
                                    //else 
                                    //    dParticipacion = decimal.Parse(dr3["t336_etp"].ToString()) / oItem.ETPR;

                                    //Hay que dividir el esfuerzo en horas por el nº de dias de duración de la tarea
                                    if (iNumDiasBase != 0)
                                        dParticipacionBase = dEsfuerzo / (iNumDiasBase * 8);
                                    else
                                        dParticipacionBase = 0;
                                    if (iNumDias != 0)
                                        dParticipacion = dEsfuerzo / (iNumDias * 8);
                                    else
                                        dParticipacion = 0;

                                    Assignments.AppendChild(
                                        OpenProj.CrearAssignmentXml(docxml, oItem, iAssignmentUID.ToString(), sCodUser, dEsfuerzo,
                                                           dParticipacion, dParticipacionBase, sPdte, bConLineBase, iNumDias));
                                    iAssignmentUID++;
                                }
                                dr3.Close();
                                dr3.Dispose();
                                if (bHayRecursos)
                                {
                                    //Si la suma de los esfuerzos asignados a la tarea no llegan al esfuerzo total de la tarea
                                    //metemos las horas que faltan al recurso imaginario (Sino, no respeta el esfuerzo de la tarea)
                                    if (dEsfAcum < oItem.EsfuerzoHoras)
                                    {
                                        sCodUser = "0";
                                        dEsfuerzo = oItem.EsfuerzoHoras - dEsfAcum;
                                        if (iNumDias != 0)
                                            dParticipacion = dEsfuerzo / (iNumDias * 8);
                                        else
                                            dParticipacion = 0;
                                        Assignments.AppendChild(
                                            OpenProj.CrearAssignmentXml(docxml, oItem, iAssignmentUID.ToString(), sCodUser,
                                                               dEsfuerzo, dParticipacion, dParticipacionBase, sPdte, bConLineBase, iNumDias));
                                        iAssignmentUID++;
                                    }
                                }
                                else
                                {
                                    //meto los elementos Assignement que contiene para cada item del proyecto un elemento por cada día del intervalo
                                    Assignments.AppendChild(
                                            OpenProj.CrearAssignmentXml(docxml, oItem, iAssignmentUID.ToString(), sCodUser,
                                                               dEsfuerzo, dParticipacion, dParticipacionBase, sPdte, bConLineBase, iNumDias));
                                    iAssignmentUID++;
                                }
                            }
                            else
                            {//El item no es una tarea -> asignamos recurso ficticio ¿seguro que es necesario?
                                Assignments.AppendChild(
                                            OpenProj.CrearAssignmentXml(docxml, oItem, iAssignmentUID.ToString(), "1",
                                                               dEsfuerzo, dParticipacion, dParticipacionBase, sPdte, bConLineBase, iNumDias));
                                iAssignmentUID++;
                            }
                        }
                            #endregion
                        else
                        {//En el criterio de exportación hemos marcado "No coger recursos" -> asignamos recurso ficticio
                            switch (oItem.tipo)
                            {
                                //case "PT":
                                //case "P":
                                //case "F":
                                //case "A":
                                //    Assignments.AppendChild(
                                //                OpenProj.CrearAssignmentPadreXml(docxml, oItem, iAssignmentUID.ToString(), "-65535"));
                                //    iAssignmentUID++;
                                //    break;
                                case "T":
                                    Assignments.AppendChild(
                                                OpenProj.CrearAssignmentXml(docxml, oItem, iAssignmentUID.ToString(), "1",
                                                                   dEsfuerzo, dParticipacion, dParticipacionBase, sPdte, bConLineBase, iNumDias));
                                    iAssignmentUID++;
                                    break;
                                case "HF":
                                    Assignments.AppendChild(
                                          OpenProj.CrearAssignmentXml(docxml, oItem, iAssignmentUID.ToString(), "1", 0, 1, 1, "", false, iNumDias));
                                    iAssignmentUID++;
                                    break;
                            }
                        }
                        #endregion
                    }
                }
                dr1.Close();
                dr1.Dispose();
                #endregion
                nodoRaiz.AppendChild(Tasks);
                nodoRaiz.AppendChild(Resources);
                nodoRaiz.AppendChild(Assignments);

                string sNumProy = PROYECTO.flGetNumProy(null, idPSN);
                sNumProy = sNumProy.Replace(@"@", @"_");
                string sArchivoSalida = Session["IDFICEPI_ENTRADA"].ToString() + "_Proyecto_" + sNumProy + "_" + DateTime.Now.Ticks.ToString() + ".xml";
                
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;

                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + sArchivoSalida + "\"");
                Response.ContentType = "text/xml"; //RFC 3023 
                //Lo codifico en UTF8 para que respete los caracteres especialed (Ñ, tíldes, etc...)
                Response.BinaryWrite(Encoding.UTF8.GetBytes(docxml.OuterXml));

                //Response.Flush();
                //Response.Close();
                Response.End();
            }
        //}
        //catch (Exception ex)
        //{
        //    //return "Error@#@" + Errores.mostrarError("Error al generar el fichero XML con la estructura del proyecto.", ex);
        //    return "Error@#@Error al generar el fichero XML con la estructura del proyecto. " + ex.Message;
        //}
    }
}
