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
//
using System.Text;
using System.Text.RegularExpressions;

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;

//Para el StreamReader
using System.IO;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    DataSet ds;
    DataSetHelper dsHelper;

    public string sErrores = "";

    public SqlConnection oConn;
    public SqlTransaction tr;
    public StringBuilder sb = new StringBuilder();
    public StringBuilder sbE = new StringBuilder();
    public StringBuilder sbFlsCab1 = new StringBuilder();
    public StringBuilder sbFlsCab2 = new StringBuilder();
    public StringBuilder sbFlsPie1 = new StringBuilder();
    public StringBuilder sbFlsPie2 = new StringBuilder();

    public Hashtable htTarea, htProfesional;

    public TAREA oTarea = null;
    public PROFESIONAL oProfesional = null;

    public int iCont = 0, iNumOk = 0;
    public ArrayList aListCorreo;

    protected void Page_Load(object sender, EventArgs e)
    {

        CabecerasError();
        PiesError();

        if (!Page.IsCallback)
        {
            sErrores = "";
            //Master.nBotonera = 43;
            Master.sbotonesOpcionOn = "50";
            Master.sbotonesOpcionOff = "50";
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Carga datos en IAP desde fichero";

            cldLinProc.InnerText = iCont.ToString("#,##0");
            cldLinOK.InnerText = iNumOk.ToString("#,##0");
            cldLinErr.InnerText = (iCont - iNumOk).ToString("#,##0");

            HttpPostedFile selectedFile = this.uplTheFile.PostedFile;
            if (selectedFile != null)
            {
                valproc.InnerText = "analizadas";
                //lblFileName.Text = selectedFile.FileName;
                lblFileName.Text = Path.GetFileName(selectedFile.FileName);

                Validar(selectedFile, Request.Form[Constantes.sPrefijo + "rdbImputacion"].ToString());
            }
        }

        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");

        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        
        switch (aArgs[0])
        {
            case ("procesar"):
                sResultado += Procesar() + "@#@" + iCont.ToString("#,##0") + "@#@" + iNumOk.ToString("#,##0") + "@#@" + (iCont - iNumOk).ToString("#,##0") + "@#@" + sbE.ToString();
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    public void CabecerasError()
    {

        sbFlsCab1.Length = 0;
        sbFlsCab1.Append("<FIELDSET style='width:950px; height:170px; padding-left:5px;'>");
        sbFlsCab1.Append("<LEGEND>Relación de filas erróneas en el proceso de validación</LEGEND>");
        sbFlsCab1.Append("<table height='17px' style='width:930px'>");
        sbFlsCab1.Append("<colgroup>");
        sbFlsCab1.Append("<col style='width:230px;'/>");
        sbFlsCab1.Append("<col style='width:90px;'/>");
        sbFlsCab1.Append("<col style='width:245px;'/>");
        sbFlsCab1.Append("<col style='width:75px;'/>");
        sbFlsCab1.Append("<col style='width:280px'/>");
        sbFlsCab1.Append("</colgroup>");
        sbFlsCab1.Append("<tr class='TBLINI'>");
        sbFlsCab1.Append("<td>Usuario</td>");
        sbFlsCab1.Append("<td>Fecha</td>");
        sbFlsCab1.Append("<td>Tarea</td>");
        sbFlsCab1.Append("<td>Esfuerzo</td>");
        sbFlsCab1.Append("<td>Error</td>");
        sbFlsCab1.Append("</tr>");
        sbFlsCab1.Append("</table>");
        sbFlsCab1.Append("<div id='divErrores' style='overflow-y: auto; overflow-x:hidden; width: 946px; height: 120px;' align='left'>");
        sbFlsCab1.Append("<div id='divDia' style=\"background-image:url('../../../Images/imgFT16.gif');width: 930px;\" runat='server' >");
        sbFlsCab1.Append("<table id='tblErrores' style='width: 930px;'>");
        sbFlsCab1.Append("<colgroup>");
        sbFlsCab1.Append("<col style='width:230px;'/>");
        sbFlsCab1.Append("<col style='width:90px'/>");
        sbFlsCab1.Append("<col style='width:245px;'/>");
        sbFlsCab1.Append("<col style='width:75px;'/>");
        sbFlsCab1.Append("<col style='width:280px;'/>");
        sbFlsCab1.Append("</colgroup>");
        sCab1.Value = sbFlsCab1.ToString();

        sbFlsCab2.Length = 0;
        sbFlsCab2.Append("<FIELDSET style='width:950px; height:170px; padding-left:5px;'>");
        sbFlsCab2.Append("<LEGEND>Relación de filas erróneas en el proceso de validación</LEGEND>");
        sbFlsCab2.Append("<table height='17px' style='width: 930px;'>");
        sbFlsCab2.Append("<colgroup>");

        sbFlsCab2.Append("<col style='width:200px;' />");
        sbFlsCab2.Append("<col style='width:100px'/>");
        sbFlsCab2.Append("<col style='width:100px'/>");
        sbFlsCab2.Append("<col style='width:200px;'/>");
        sbFlsCab2.Append("<col style='width:80px;'/>");
        sbFlsCab2.Append("<col style='width:30px;'/>");
        sbFlsCab2.Append("<col style='width:220px;'/>");
        sbFlsCab2.Append("</colgroup>");

        sbFlsCab2.Append("<tr class='TBLINI'>");
        sbFlsCab2.Append("<td>Usuario</td>");
        sbFlsCab2.Append("<td>F.Desde</td>");
        sbFlsCab2.Append("<td>F.Hasta</td>");
        sbFlsCab2.Append("<td>Tarea</td>");
        sbFlsCab2.Append("<td>Esfuerzo</td>");
        sbFlsCab2.Append("<td>Fes</td>");
        sbFlsCab2.Append("<td>Error</td>");
        sbFlsCab2.Append("</tr>");
        sbFlsCab2.Append("</table>");

        sbFlsCab2.Append("<div id='divErrores' style='overflow: auto; width: 946px; height: 120px;' align='left' >");
        sbFlsCab2.Append("<div id='divDia' style=\"background-image:url('../../../Images/imgFT16.gif');width: 930px;\" runat='server' >");
        sbFlsCab2.Append("<table id='tblErrores' style='width: 930px;'>");
        sbFlsCab2.Append("<colgroup>");
        sbFlsCab2.Append("<col style='width:200px'/>");
        sbFlsCab2.Append("<col style='width:100px'/>");
        sbFlsCab2.Append("<col style='width:100px'/>");
        sbFlsCab2.Append("<col style='width:200px;'/>");
        sbFlsCab2.Append("<col style='width:80px;'/>");
        sbFlsCab2.Append("<col style='width:30px;'/>");
        sbFlsCab2.Append("<col style='width:220px;'/>");
        sbFlsCab2.Append("</colgroup>");
        sCab2.Value = sbFlsCab2.ToString();
    }
    public void PiesError()
    {
        sbFlsPie1.Append("</table>");
        sbFlsPie1.Append("</div>");
        sbFlsPie1.Append("</div>");
        sbFlsPie1.Append("<table style='margin-left:2px;' height='17px' width='930px' align='left'>");
        sbFlsPie1.Append("<tr class='TBLFIN'><td>&nbsp;</td></TR>");
        sbFlsPie1.Append("</table>");
        sbFlsPie1.Append("</FIELDSET>");
        sPie1.Value = sbFlsPie1.ToString();

        sbFlsPie2.Append("</table>");
        sbFlsPie2.Append("</div>");
        sbFlsPie2.Append("</div>");
        sbFlsPie2.Append("<table style='margin-left:2px;' height='17px' width='930px' align='left'>");
        sbFlsPie2.Append("<tr class='TBLFIN'><td>&nbsp;</td></TR>");
        sbFlsPie2.Append("</table>");
        sbFlsPie2.Append("</FIELDSET>");
        sPie2.Value = sbFlsPie2.ToString();
    }    
    private void CargarArrayHT()
    {
        #region Obtención de dataset con Tareas, Usuarios
        oTarea = null;
        oProfesional = null;
        DataSet ds;
        try
        {
            ds = AddConsumo.ValidarFichero();
        }
        catch 
        {
            throw (new Exception("Error al obtener las consultas para la carga de datos."));
        }
        htTarea = new Hashtable();
        try
        {
            foreach (DataRow dsTarea in ds.Tables[0].Rows) //Recorro tabla de TAREA
            {
                htTarea.Add(dsTarea["t332_idtarea"].ToString(), new TAREA(
                                                                    (int)dsTarea["t332_idtarea"],
                                                                    dsTarea["t332_destarea"].ToString(),
                                                                    (int)dsTarea["t331_idpt"],
                                                                    byte.Parse(dsTarea["t332_estado"].ToString()),
                                                                    double.Parse(dsTarea["t332_cle"].ToString()),
                                                                    dsTarea["t332_tipocle"].ToString(),
                                                                    (dsTarea["t332_impiap"].ToString() == "1") ? true : false,
                                                                    (int)dsTarea["t305_idproyectosubnodo"],
                                                                    (dsTarea["t332_fiv"].ToString() == "") ? null : (DateTime?)DateTime.Parse(dsTarea["t332_fiv"].ToString()),
                                                                    (dsTarea["t332_ffv"].ToString() == "") ? null : (DateTime?)DateTime.Parse(dsTarea["t332_ffv"].ToString()),
                                                                    //(dsTarea["t323_regjornocompleta"].ToString() == "1") ? true : false
                                                                    (bool)dsTarea["t323_regjornocompleta"],
                                                                    (bool)dsTarea["t331_obligaest"],
                                                                    byte.Parse(dsTarea["t331_estado"].ToString()),
                                                                    (bool)dsTarea["t323_regfes"],
                                                                    dsTarea["t301_estado"].ToString()
                                                                    ));
            }
        }
        catch 
        {
            throw (new Exception("Error al cargar los datos de la tarea."));
        }
        htProfesional = new Hashtable();
        try
        {
            foreach (DataRow dsProfesional in ds.Tables[1].Rows) //Recorro tabla de Profesionales
            {
                htProfesional.Add(dsProfesional["t314_idusuario"].ToString(), new PROFESIONAL(
                                                                (int)dsProfesional["t001_idficepi"],
                                                                (int)dsProfesional["t314_idusuario"],
                                                                dsProfesional["Profesional"].ToString(),
                                                                (dsProfesional["t303_ultcierreIAP"].ToString() == "") ? null : (int?)dsProfesional["t303_ultcierreIAP"],
                                                                bool.Parse(dsProfesional["t314_jornadareducida"].ToString()),
                                                                (dsProfesional["t303_idnodo"].ToString() == "") ? null : (int?)dsProfesional["t303_idnodo"],
                                                                double.Parse(dsProfesional["t314_horasjor_red"].ToString()),
                                                                (dsProfesional["t314_fdesde_red"].ToString() == "") ? null : (DateTime?)DateTime.Parse(dsProfesional["t314_fdesde_red"].ToString()),
                                                                (dsProfesional["t314_fhasta_red"].ToString() == "") ? null : (DateTime?)DateTime.Parse(dsProfesional["t314_fhasta_red"].ToString()),
                                                                bool.Parse(dsProfesional["t314_controlhuecos"].ToString()),
                                                                (dsProfesional["fUltImputacion"].ToString() == "") ? null : (DateTime?)DateTime.Parse(dsProfesional["fUltImputacion"].ToString()),
                                                                (int)dsProfesional["t066_idcal"],
                                                                dsProfesional["t066_descal"].ToString(),
                                                                dsProfesional["t066_semlabL"].ToString() + "," + dsProfesional["t066_semlabM"].ToString() + "," + dsProfesional["t066_semlabX"].ToString() + "," + dsProfesional["t066_semlabJ"].ToString() + "," + dsProfesional["t066_semlabV"].ToString() + "," + dsProfesional["t066_semlabS"].ToString() + "," + dsProfesional["t066_semlabD"].ToString(),
                                                                dsProfesional["t001_codred"].ToString(),
                                                                (dsProfesional["T001_FECALTA"].ToString() == "") ? null : (DateTime?)DateTime.Parse(dsProfesional["T001_FECALTA"].ToString()),
                                                                (dsProfesional["T001_FECBAJA"].ToString() == "") ? null : (DateTime?)DateTime.Parse(dsProfesional["T001_FECBAJA"].ToString())
                                                                ));
            }
        }
        catch 
        {
            throw (new Exception("Error al cargar los datos del profesional."));
        }
        #endregion
    }

    private void Validar(HttpPostedFile selectedFile, string sEstructu)
    {
        //StringBuilder sbF = new StringBuilder();
        bool bErrorControlado = false;
        try
        {
            try
            {
                CargarArrayHT();
            }
            catch (Exception ex)
            {
				bErrorControlado = true;
				throw (new Exception(ex.Message));
            }

            iCont = 0;
            iNumOk = 0;

            if (selectedFile.ContentLength != 0)
            {
                string sFichero = selectedFile.FileName;
                //Grabo el archivo en base de datos
                byte[] ArchivoEnBinario = new Byte[0];
                ArchivoEnBinario = new Byte[selectedFile.ContentLength]; //Crear el array de bytes con la longitud del archivo
                selectedFile.InputStream.Read(ArchivoEnBinario, 0, selectedFile.ContentLength); //Forzar al control del archivo a cargar los datos en el array

                int iRows = FICHEROSMANIOBRAUSU.Update(null, Constantes.FicheroIAP, int.Parse(Session["IDFICEPI_ENTRADA"].ToString()), ArchivoEnBinario);
                if (iRows == 0)
                {
                    bErrorControlado = true;
                    throw (new Exception("No existe entrada asociada a este proceso en el fichero de Maniobra"));
                }

                selectedFile.InputStream.Position = 0;
                StreamReader r = new StreamReader(selectedFile.InputStream, System.Text.Encoding.UTF7);
                DesdeFicheroIAP oDesdeFicheroIAP = null;

                String strLinea = null;
                while ((strLinea = r.ReadLine()) != "")
                {
                    if (strLinea == null) break;
                    iCont++;

                    try
                    {
                      oDesdeFicheroIAP = validarLinea(DesdeFicheroIAP.getFila(strLinea, sEstructu));
                    }
                    catch (Exception ex)
                    {
                        bErrorControlado = true;
                        oDesdeFicheroIAP = new DesdeFicheroIAP();
                        sbE.Append(ponerFilaError(oDesdeFicheroIAP, "Error al procesar el fichero de entrada en la línea (" + iCont + ") " + ex.Message));
                        continue;
                    }

                    if (validarCampos(oDesdeFicheroIAP, true)) continue;
                    iNumOk++;
                }
            }

            if (sEstructu == "D") sFLS.Value = sCab1.Value + sbE.ToString() + sPie1.Value;
            else sFLS.Value = sCab2.Value + sbE.ToString() + sPie2.Value;

            cldLinProc.InnerText = iCont.ToString("#,##0");
            cldLinOK.InnerText = iNumOk.ToString("#,##0");
            cldLinErr.InnerText = (iCont - iNumOk).ToString("#,##0");
            this.hdnIniciado.Value = "T";

        }
        catch (Exception ex)
        {
            if (bErrorControlado)
            {
                if (iCont != 0) sErrores = "Error al procesar el fichero de entrada en la línea (" + iCont + ") " + ex.Message;
                else sErrores = ex.Message;
            }
            else sErrores = "El fichero no tiene el formato requerido para el proceso"; ;
        }
    }
    private DesdeFicheroIAP validarLinea(DesdeFicheroIAP oDesdeFicheroIAP)
    {
        char[] chrs = { '+', '-', ',', '.' };
        char[] chrs2 = { '+', '-', '.' };

        int idx = 0;

        oTarea = (TAREA)htTarea[oDesdeFicheroIAP.idtarea];
        if (oTarea != null)
        {
            oDesdeFicheroIAP.t332_destarea = oTarea.t332_destarea;
        }

        oProfesional = (PROFESIONAL)htProfesional[oDesdeFicheroIAP.idusuario];
        if (oProfesional != null)
        {
            oDesdeFicheroIAP.Profesional = oProfesional.Profesional;
        }

        if (Utilidades.isNumeric(oDesdeFicheroIAP.idusuario))
        {
            idx = oDesdeFicheroIAP.idusuario.IndexOfAny(chrs);
            if (idx == -1) oDesdeFicheroIAP.t314_idusuario = System.Convert.ToInt32(oDesdeFicheroIAP.idusuario);
            else oDesdeFicheroIAP.t314_idusuario = -99999;
        }
        else oDesdeFicheroIAP.t314_idusuario = -1;

        if (Utilidades.isNumeric(oDesdeFicheroIAP.idtarea))
        {
            idx = oDesdeFicheroIAP.idtarea.IndexOfAny(chrs);
            if (idx == -1) oDesdeFicheroIAP.t332_idtarea = System.Convert.ToInt32(oDesdeFicheroIAP.idtarea);
            else oDesdeFicheroIAP.t332_idtarea = -99999;
        }
        else oDesdeFicheroIAP.t332_idtarea = -1;

        if (oDesdeFicheroIAP.fechaDesde.Length == 10)
        {
            if (Utilidades.isDate(oDesdeFicheroIAP.fechaDesde))
            {
                oDesdeFicheroIAP.t337_fechaDesde = DateTime.Parse(oDesdeFicheroIAP.fechaDesde);
                if (
                        (!Utilidades.isNumeric(oDesdeFicheroIAP.fechaDesde.ToString().Substring(0, 2)))
                    ||
                        (!Utilidades.isNumeric(oDesdeFicheroIAP.fechaDesde.ToString().Substring(3, 2)))
                    ||
                        (!Utilidades.isNumeric(oDesdeFicheroIAP.fechaDesde.ToString().Substring(6, 4)))
                    )
                    oDesdeFicheroIAP.t337_fechaDesde = null;
            }
            else
            {
                oDesdeFicheroIAP.t337_fechaDesde = null;
            }
        }
        else
        {
            oDesdeFicheroIAP.t337_fechaDesde = null;
        }
        if (Utilidades.isNumeric(oDesdeFicheroIAP.esfuerzo))
        {
            idx = oDesdeFicheroIAP.esfuerzo.IndexOfAny(chrs2);
            if (idx == -1) oDesdeFicheroIAP.t337_esfuerzo = Double.Parse(oDesdeFicheroIAP.esfuerzo);
            else oDesdeFicheroIAP.t337_esfuerzo = -99999;
        }
        else
        {
            oDesdeFicheroIAP.t337_esfuerzo = -1;
        }

        if (Request.Form[Constantes.sPrefijo + "rdbImputacion"].ToString() != "D")
        {
            if (oDesdeFicheroIAP.fechaHasta.Length == 10)
            {
                if (Utilidades.isDate(oDesdeFicheroIAP.fechaHasta))
                {
                    oDesdeFicheroIAP.t337_fechaHasta = DateTime.Parse(oDesdeFicheroIAP.fechaHasta);
                    if (
                            (!Utilidades.isNumeric(oDesdeFicheroIAP.fechaHasta.ToString().Substring(0, 2)))
                        ||
                            (!Utilidades.isNumeric(oDesdeFicheroIAP.fechaHasta.ToString().Substring(3, 2)))
                        ||
                            (!Utilidades.isNumeric(oDesdeFicheroIAP.fechaHasta.ToString().Substring(6, 4)))
                        )
                    {
                        oDesdeFicheroIAP.t337_fechaHasta = null;
                    }
                    else if (oDesdeFicheroIAP.t337_fechaDesde > oDesdeFicheroIAP.t337_fechaHasta)
                    { oDesdeFicheroIAP.t337_fechaHasta = null; }
                }
                else
                {
                    oDesdeFicheroIAP.t337_fechaHasta = null;
                }
            }
            else
            {
                oDesdeFicheroIAP.t337_fechaHasta = null;
            }

            if (Utilidades.isNumeric(oDesdeFicheroIAP.festivos))
            {
                if (oDesdeFicheroIAP.festivos == "1") oDesdeFicheroIAP.bfestivos = true;
                else if (oDesdeFicheroIAP.festivos == "0") oDesdeFicheroIAP.bfestivos = false;
                else oDesdeFicheroIAP.bfestivos = null;
            }
            else oDesdeFicheroIAP.bfestivos = null;
        }

        return oDesdeFicheroIAP;
    }
    private bool validarCampos(DesdeFicheroIAP oDesdeFicheroIAP, bool bEscribir)
    {
        bool bErrores = false;
        if (oDesdeFicheroIAP.t332_idtarea == -99999)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFicheroIAP, "Formato incorrecto. El número de tarea no acepta puntos, signos(+,-) ni comas decimales, (" + oDesdeFicheroIAP.idtarea + ")"));
            bErrores = true;
            //return false;
        }
        oTarea = (TAREA)htTarea[oDesdeFicheroIAP.idtarea];
        if (oTarea == null)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFicheroIAP, "La tarea (" + oDesdeFicheroIAP.idtarea + ") no existe."));
            bErrores = true;
            //return false;
        }
        else
        {
            if ((oTarea.t332_estado == 3 || oTarea.t332_estado == 4) && oTarea.t332_impiap == false)
            {
                if (bEscribir) sbE.Append(ponerFilaError(oDesdeFicheroIAP, "La tarea (" + oDesdeFicheroIAP.idtarea + ") tiene el estado (cerrado o finalizado) y no permite imputar IAP."));
                bErrores = true;
                //return false;
            }
            else if (!(oTarea.t332_estado==1 || ((oTarea.t332_estado == 3 || oTarea.t332_estado == 4) && oTarea.t332_impiap == true)))
            {
                if (bEscribir) sbE.Append(ponerFilaError(oDesdeFicheroIAP, "La tarea (" + oDesdeFicheroIAP.idtarea + ") tiene un estado no permitido para imputaciones (" + oTarea.t332_estado.ToString()+ "."));
                bErrores = true;
                //return false;
            }
            else if (oTarea.t331_estado != 1)
            {
                if (bEscribir) sbE.Append(ponerFilaError(oDesdeFicheroIAP, "La tarea (" + oDesdeFicheroIAP.idtarea + ") tiene el estado de su proyecto técnico no activo."));
                bErrores = true;
                //return false;
            }            
        }

        if (oDesdeFicheroIAP.t314_idusuario == -99999)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFicheroIAP, "Formato incorrecto. El número de usuario no acepta puntos, signos(+,-) ni comas decimales, (" + oDesdeFicheroIAP.idusuario + ")"));
            bErrores = true;
            //return false;
        }

        if ((PROFESIONAL)htProfesional[oDesdeFicheroIAP.idusuario] == null)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFicheroIAP, "El usuario (" + oDesdeFicheroIAP.idusuario + ") no existe."));
            bErrores = true;
            //return false;
        }

        if (oDesdeFicheroIAP.t332_idtarea == -1)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFicheroIAP, "Número de tarea no numérico (" + oDesdeFicheroIAP.idtarea + ")"));
            bErrores = true;
            //return false;
        }

        if (oDesdeFicheroIAP.t314_idusuario == -1)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFicheroIAP, "Número de usuario no numérico (" + oDesdeFicheroIAP.idusuario + ")"));
            bErrores = true;
            //return false;
        }

        if (oDesdeFicheroIAP.t337_esfuerzo == -1)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFicheroIAP, "El valor del campo esfuerzo es no numérico (" + oDesdeFicheroIAP.esfuerzo + ")"));
            bErrores = true;
            //return false;
        }

        if (oDesdeFicheroIAP.t337_esfuerzo == -99999)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFicheroIAP, "El valor del campo esfuerzo no acepta puntos ni signos (+,-), (" + oDesdeFicheroIAP.esfuerzo + ")"));
            bErrores = true;
            //return false;
        }

        if (oDesdeFicheroIAP.t337_fechaDesde == null)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFicheroIAP, "El valor del campo fecha desde tiene el formato incorrecto o su valor no es válido (" + oDesdeFicheroIAP.fechaDesde + ")"));
            bErrores = true;
            //return false;
        }

        if (Request.Form[Constantes.sPrefijo + "rdbImputacion"].ToString() != "D")
        {
            if (oDesdeFicheroIAP.t337_fechaHasta == null)
            {
                if (bEscribir) sbE.Append(ponerFilaError(oDesdeFicheroIAP, "El valor del campo fecha hasta tiene el formato incorrecto o su valor no es válido (" + oDesdeFicheroIAP.fechaHasta + ")"));
                bErrores = true;
                //return false;
            }

            if (oDesdeFicheroIAP.bfestivos == null)
            {
                if (bEscribir) sbE.Append(ponerFilaError(oDesdeFicheroIAP, "El valor del campo permitir imputar a festivos o no laborables, o no es numérico o su valor no es válido (" + oDesdeFicheroIAP.festivos + ")"));
                bErrores = true;
                //return false;
            }
        }
        return bErrores;
    }
    private string Procesar()
    {
        bool bErrorControlado = false;
        string sResul = "", lin = "", strLinea = "";
        aListCorreo = new ArrayList();
        try
        {
            iCont = 0;
            iNumOk = 0;
            bool bErrores = false;
            string sAsistente = " Asistente: " + Session["DES_EMPLEADO"].ToString();
            sAsistente += "(IdUser=" + Session["UsuarioActual"].ToString() + ") ";
            try
            {
                CargarArrayHT();
            }
            catch (Exception ex)
            {
				bErrorControlado = true;
				throw (new Exception(ex.Message));
            }

            #region Apertura de conexión y transacción
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
            //Leo el fichero de base de datos
            FICHEROSMANIOBRAUSU oFic = FICHEROSMANIOBRAUSU.Select(tr, Constantes.FicheroIAP, int.Parse(Session["IDFICEPI_ENTRADA"].ToString()));
            if (oFic.t722_fichero.Length > 0)
            {
                #region Leer fichero de BBDD
                MemoryStream mstr = new MemoryStream(oFic.t722_fichero);
                mstr.Seek(0, SeekOrigin.Begin);
                int count = 0;
                byte[] byteArray = new byte[mstr.Length];
                while (count < mstr.Length)
                {
                    byteArray[count++] = System.Convert.ToByte(mstr.ReadByte());
                }
                lin = FromASCIIByteArray(byteArray);
                #endregion

                string[] aArgs = Regex.Split(lin, "\r\n");

                #region Crear Datatable principal tomando como base el fichero de entrada así como datatables auxiliares cara a las validaciones

                // Crear un datatable y alimentarlo en base a los valores del fichero

                DataTable table_input = new DataTable("IAP_IN");

                table_input.Columns.Add("t332_idtarea", typeof(int));
                table_input.Columns.Add("t314_idusuario", typeof(int));
                table_input.Columns.Add("t337_fecha", typeof(DateTime));
                table_input.Columns.Add("t337_esfuerzo", typeof(double));
                table_input.Columns.Add("t337_comentario", typeof(string));
                table_input.Columns.Add("festivos", typeof(bool));
                table_input.Columns.Add("fila", typeof(int));

                for (int iLinea = 0; iLinea < aArgs.Length; iLinea++)
                {
                    if (aArgs[iLinea] != "")
                    {
                        strLinea = aArgs[iLinea];
                        iCont = iLinea + 1;

                        string[] aAtributo = Regex.Split(strLinea, @"\t");
                        if (rdbImputacion.SelectedValue == "D")
                        {
                            table_input.Rows.Add(
                                            int.Parse(aAtributo[0]),            // IdTarea 
                                            int.Parse(aAtributo[1]),            // IdUsuario
                                            DateTime.Parse(aAtributo[2]),       // Fecha
                                            double.Parse(aAtributo[3]),         // Esfuerzo
                                            aAtributo[4],                       // Comentario
                                            true,                               // festivos
                                            iCont                               // Nro Línea erronea
                                            );
                        }
                        else
                        {
                            DateTime dDesde = DateTime.Parse(aAtributo[2]);     //  Fecha desde
                            DateTime dHasta = DateTime.Parse(aAtributo[3]);     //  Fecha hasta
                            int nDifDias = Fechas.DateDiff("day", dDesde, dHasta);
                            DateTime dDiaAux;
                            dDiaAux = DateTime.Parse("01/01/1900");

                            for (int i = 0; i <= nDifDias; i++)
                            {
                                dDiaAux = dDesde.AddDays(i);
                                table_input.Rows.Add(
                                            int.Parse(aAtributo[0]),                // IdTarea 
                                            int.Parse(aAtributo[1]),                // IdUsuario
                                            dDiaAux,                                // Fecha
                                            double.Parse(aAtributo[4]),             // Esfuerzo
                                            aAtributo[6],                           // Comentario
                                            (aAtributo[5] == "1") ? true : false,   // festivos
                                            iCont                                   // Nro Línea erronea
                                            );
                            }
                        }
                    }
                }

                // Crear DataTables auxiliares con información agrupada cara a las validaciones

                ds = new DataSet();
                dsHelper = new DataSetHelper(ref ds);

                // Agrupar en el fichero de entrada los esfuerzos por Usuario-Fecha-Tarea, para ver si existen varias imputaciones de un usuario a la misma tarea y a la misma fecha.

                DataTable table_usufechatarea = new DataTable("USUARIO_FECHA_TAREA");

                table_usufechatarea.Columns.Add("t332_idtarea", typeof(int));
                table_usufechatarea.Columns.Add("t314_idusuario", typeof(int));
                table_usufechatarea.Columns.Add("t337_fecha", typeof(DateTime));
                table_usufechatarea.Columns.Add("contador", typeof(double));

                //string sIdusuario = "";
                //string sFecha = "";
                //string sIdTarea = "";
                //string sEsfuerzo = "";
                //string sFDesde = "";
                //string sFHasta = "";
                //string sContador = "";

                //foreach (DataRow oImpu in table_usufechatarea.Rows)          //Recorro tabla de usuarios
                //{
                //    sIdusuario = oImpu["t314_idusuario"].ToString();
                //    sFecha = oImpu["t337_fecha"].ToString();
                //    sIdTarea = oImpu["t332_idtarea"].ToString();
                //    sContador = oImpu["contador"].ToString();
                //}

                //DataTable UsuarioEsfuerzo = new DataTable("UsuarioEsfuerzo");
                //UsuarioEsfuerzo.Columns.Add("t314_idusuario", typeof(int));
                //UsuarioEsfuerzo.Columns.Add("t337_fecha", typeof(DateTime));
                //UsuarioEsfuerzo.Columns.Add("t337_esfuerzo", typeof(double));

                // Para controlar que no haya más de una imputación a una tarea de un usuario en un día determinado

                DataTable TareaUsuarioFecha = new DataTable("TareaUsuarioFecha");
                TareaUsuarioFecha.Columns.Add("t332_idtarea", typeof(int));
                TareaUsuarioFecha.Columns.Add("t314_idusuario", typeof(int));
                TareaUsuarioFecha.Columns.Add("t337_fecha", typeof(DateTime));
                TareaUsuarioFecha.Columns.Add("Cantidad", typeof(int));

                //DataColumn[] myKey = new DataColumn[2];
                //myKey[0] = UsuarioEsfuerzo.Columns["t314_idusuario"];
                //myKey[1] = UsuarioEsfuerzo.Columns["t337_fecha"];
                //UsuarioEsfuerzo.PrimaryKey = myKey;

                DataColumn[] myKey1 = new DataColumn[3];
                myKey1[0] = table_usufechatarea.Columns["t332_idtarea"];
                myKey1[1] = table_usufechatarea.Columns["t314_idusuario"];
                myKey1[2] = table_usufechatarea.Columns["t337_fecha"];
                table_usufechatarea.PrimaryKey = myKey1;

                dsHelper.InsertGroupByInto(table_usufechatarea, table_input, "t314_idusuario, t337_fecha, t332_idtarea, count(*) contador", "", " t314_idusuario ASC, t337_fecha ASC, t332_idtarea ASC");
                //dsHelper.InsertGroupByInto(UsuarioEsfuerzo, table_input, "t314_idusuario,  t337_fecha, sum(t337_esfuerzo) t337_esfuerzo", "", "t314_idusuario asc, t337_fecha asc");

                // Recorrer la tabla y hacer las validaciones y grabaciones

                //foreach (DataRow oUsuario in UsuarioEsfuerzo.Rows)          //Recorro tabla de UsuarioEsfuerzo
                //{
                //    sIdusuario = oUsuario["t314_idusuario"].ToString();
                //    sFecha = oUsuario["t337_fecha"].ToString();
                //    sEsfuerzo = oUsuario["t337_esfuerzo"].ToString();
                //}


                // Una vez construido el Datatable lo recorro con la ordenación deseada

                DataRow[] oImputaciones = table_input.Select("", "t314_idusuario ASC, t337_fecha ASC, t332_idtarea ASC");

                // borrado previo

                foreach (DataRow oImputacion in oImputaciones)  //Recorro tabla de imputaciones para borrar los consumos
                {
                    try
                    {
                        #region Eliminamos todos los consumos de un profesional en la fecha que tratamos. 

                        oProfesional = (PROFESIONAL)htProfesional[oImputacion["t314_idusuario"].ToString()];
                        if (oProfesional == null) continue;

                        oTarea = (TAREA)htTarea[oImputacion["t332_idtarea"].ToString()];
                        if (oTarea == null) continue;

                        DateTime dDiaAux = DateTime.Parse(oImputacion["t337_fecha"].ToString());
                        Consumo.EliminarRango(tr, oProfesional.t314_idusuario, dDiaAux, dDiaAux);
                        
                        #endregion
                    }
                    catch 
                    {
                        continue;
                    }
                 }


                SqlDataReader drAsis = FIGURANODO.Catalogo(null, (int)Session["UsuarioActual"], "S", 1, 0);
                ArrayList alNodosUsu = new ArrayList();
                sAsistente += "-> Nodos(";
                while (drAsis.Read())
                {
                    alNodosUsu.Add(int.Parse(drAsis["t303_idnodo"].ToString()));
                    sAsistente += drAsis["t303_idnodo"].ToString() + ",";
                }
                sAsistente += ")";
                drAsis.Close();
                drAsis.Dispose();

                iCont = 0;
                int nFila = 0;
                sbE.Append("<table id='tblErroresGrab' class='texto' style='WIDTH: 930px; BORDER-COLLAPSE: collapse;' cellSpacing='0' cellPadding='0' border='0'>");
				sbE.Append("<colgroup><col style='width:70px'/><col style='width:860px'/></colgroup>");
                foreach (DataRow oImputacion in oImputaciones)  //Recorro tabla de imputaciones
                {
                    try
                    {
						strLinea =  oImputacion["t332_idtarea"].ToString() + ":";
						strLinea += oImputacion["t314_idusuario"].ToString() + ":";
						strLinea += oImputacion["t337_fecha"].ToString() + ":";
						strLinea += oImputacion["t337_esfuerzo"].ToString() + ":";
						strLinea += oImputacion["t337_comentario"].ToString() + ":";
                        strLinea += (bool.Parse(oImputacion["festivos"].ToString()))? "1":"0";

                        strLinea += "|";
                        nFila = int.Parse(oImputacion["fila"].ToString());
                        iCont++;

                        #region Obtención de datos relacionados con el PROFESIONAL y el último día reportado

                        oProfesional = (PROFESIONAL)htProfesional[oImputacion["t314_idusuario"].ToString()];
                        if (oProfesional == null)
                        {
                            bErrorControlado = true;
                            throw (new Exception("No existe el código del profesional."));
                        }

                        int? iUMC_IAP = null;
                        Recurso oRecurso = new Recurso();

                        //bool bIdentificado = oRecurso.ObtenerRecurso(oProfesional.t001_codred, (int.Parse(oImputacion["t314_idusuario"].ToString()) == 0) ? null : (int?)int.Parse(oImputacion["t314_idusuario"].ToString()));

                        iUMC_IAP = (oProfesional.t303_ultcierreIAP.HasValue) ? oProfesional.t303_ultcierreIAP : DateTime.Today.AddMonths(-1).Year * 100 + DateTime.Today.AddMonths(-1).Month;
                        if (iUMC_IAP != null)
                        {
                            if (Fechas.FechaAAnnomes(DateTime.Parse(oImputacion["t337_fecha"].ToString())) <= iUMC_IAP)
                            {
                                bErrorControlado = true;
                                throw (new Exception("La fecha de imputación (" + oImputacion["t337_fecha"].ToString() + ") pertenece a un mes IAP cerrado. Último mes cerrado IAP (" + Fechas.AnnomesAFechaDescLarga(int.Parse(iUMC_IAP.ToString())) + ")."));
                            }
                            if (DateTime.Parse(oImputacion["t337_fecha"].ToString()) > Fechas.AnnomesAFecha(int.Parse(iUMC_IAP.ToString())).AddMonths(3).AddDays(-1))
                            {
                                bErrorControlado = true;
                                throw (new Exception("La fecha de imputación (" + oImputacion["t337_fecha"].ToString() + ") debe ser como máximo 2 meses posterior al último cierre IAP y se ha sobrepasado. Último mes cerrado IAP (" + Fechas.AnnomesAFechaDescLarga(int.Parse(iUMC_IAP.ToString())) + ")."));
                            }
                            if (DateTime.Parse(oImputacion["t337_fecha"].ToString()) < oProfesional.fAlta || DateTime.Parse(oImputacion["t337_fecha"].ToString()) > oProfesional.fBaja)
                            {
                                bErrorControlado = true;
                                throw (new Exception("La fecha de imputación (" + oImputacion["t337_fecha"].ToString() + ") debe estar entre la fecha de alta y de baja del profesional.(Fecha Alta:" + ((DateTime)oProfesional.fAlta.Value).ToShortDateString() + " / Fecha Baja:" + ((DateTime)oProfesional.fBaja.Value).ToShortDateString()+")."));
                            }

                        }

                        // Controlar si es un recurso interno deberá pertenecer a un CR del ámbito 
                        // de visión del asistente, en caso de ser un recurso externo no será necesario.

                        // Si el usuario actual es administrador no realizar comprobaciones

                        if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "")
                        {
                            if (oProfesional.t303_idnodo != null)
                            {
                                bool bEncontrado = false;
                                //SqlDataReader dr = FIGURANODO.Catalogo(null, (int)Session["UsuarioActual"], "S", 1, 0);
                                //while (dr.Read())
                                //{
                                //    if (oProfesional.t303_idnodo == int.Parse(dr["t303_idnodo"].ToString()))
                                //    {
                                //        bEncontrado = true;
                                //        break;
                                //    }
                                //}

                                foreach (int iNodo in alNodosUsu)
                                {
                                    if (oProfesional.t303_idnodo == iNodo)
                                    {
                                        bEncontrado = true;
                                        break;
                                    }
                                }

                                if (bEncontrado == false)
                                {
                                    bErrorControlado = true;
                                    throw (new Exception("Se trata del profesional interno " + oProfesional.Profesional + " (Nodo =" + oProfesional.t303_idnodo.ToString() + ") que no pertenece a ninguno de los CR'S a los que tiene ambito de visión el Asistente."));
                                }
                            }
                        }

                        DateTime? dUDR;
                        string sFecUltImputac = USUARIO.ObtenerFecUltImputac(null, int.Parse(oImputacion["t314_idusuario"].ToString()));
                        if (sFecUltImputac != "") dUDR = DateTime.Parse(sFecUltImputac);
                        else dUDR = null;

                        //DateTime? dUDR = oRecurso.FecUltImputacion;
                        //string sFecUltImputac = (oRecurso.FecUltImputacion.HasValue) ? ((DateTime)oRecurso.FecUltImputacion.Value).ToShortDateString() : null;

                        #endregion

                        #region Obtención de datos relacionados con la TAREA
                        //Obtener los datos de la tarea a la que se va a imputar.

                        oTarea = (TAREA)htTarea[oImputacion["t332_idtarea"].ToString()];
                        if (oTarea == null)
                        {
                            bErrorControlado = true;
                            throw (new Exception("No existe el código de la tarea."));
                        }
                        //

                        if (oTarea.t301_estado == "C")
                        {
                            bErrorControlado = true;
                            throw (new Exception("El proyecto económico de la tarea está cerrado."));
                        }
                        //
                        DataRow[] rowUsuFechaTarea = table_usufechatarea.Select("t314_idusuario=" + oImputacion["t314_idusuario"].ToString() + " and t337_fecha='" + oImputacion["t337_fecha"].ToString() + "'" + " and t332_idtarea=" + oImputacion["t332_idtarea"].ToString() + "");

                        if (int.Parse(rowUsuFechaTarea[0]["contador"].ToString()) > 1)
                        {
                            bErrorControlado = true;
                            throw (new Exception("El usuario '" + oProfesional.Profesional + "' para el día " + oImputacion["t337_fecha"].ToString() + " no puede tener para la tarea '" + oTarea.t332_destarea + "' más de una imputación"));
                        }

                        //DataRow[] rowUsuarioEsfuerzo = UsuarioEsfuerzo.Select("t314_idusuario=" + oImputacion["t314_idusuario"].ToString() + " and t337_fecha='" + oImputacion["t337_fecha"].ToString() + "'");

                        //if (double.Parse(rowUsuarioEsfuerzo[0]["t337_esfuerzo"].ToString()) < 0 || double.Parse(rowUsuarioEsfuerzo[0]["t337_esfuerzo"].ToString()) > 24)
                        //{
                        //    bErrorControlado = true;
                        //    throw (new Exception("El numero de horas imputadas por el profesional (" + oProfesional.Profesional + ") para el día " + oImputacion["t337_fecha"].ToString() + " es > 24 horas o <= 0 horas."));
                        //}
                        #endregion

                        #region Obtención de datos de la IMPUTACION y del PROFESIONAL

                        double nHoras = double.Parse(oImputacion["t337_esfuerzo"].ToString());
                        bool bFestivos = bool.Parse(oImputacion["festivos"].ToString());
                        bool bRegjornocompleta = bool.Parse(oTarea.t323_regjornocompleta.ToString());
                        bool bRegFes = bool.Parse(oTarea.t323_regfes.ToString());
                        
                        bool bObligaEst = bool.Parse(oTarea.t331_obligaest.ToString());

                        string sComentario = oImputacion["t337_comentario"].ToString();

                        bool bFestAux = false;
                        DateTime dDiaAux = DateTime.Parse(oImputacion["t337_fecha"].ToString());
                        float nHorasDia = 0; // ojo
                        double nJornadas = 0;

                        #endregion

                        #region El identificador de usuario debe estar activo para esa tarea

                        TAREAPSP oTareaUsu = TAREAPSP.ObtenerDatosRecurso(null, oTarea.t332_idtarea, oProfesional.t314_idusuario);

                        bool bEstadoUsu = bool.Parse(oTareaUsu.t336_estado.ToString());

                        if (!bEstadoUsu)
                        {
                            bErrorControlado = true;
                            throw (new Exception("El identificador de usuario debe estar activo para esta tarea."));
                        }
                        #endregion

                        #region Controlar si el proyecto técnico obliga a realizar estimaciones a nivel de tarea

                        if (bObligaEst)
                        {
                            if (oTareaUsu.t336_ete == 0 || oTareaUsu.t336_ffe == null)
                            {
                                bErrorControlado = true;
                                throw (new Exception("Es obligatorio realizar estimaciones para esta tarea y no se han hecho. No es posible cargar los esfuerzos de esta tarea desde fichero."));
                            }
                        }
                        #endregion

                        #region Vemos si con esa imputación si se superan las 24 h/diarias
                        //Obtener las imputaciones de otras tareas.

                        Consumo oConsumo = new Consumo();
                        oConsumo.ObtenerImputacionesDia(tr, oProfesional.t314_idusuario, dDiaAux, oTarea.t332_idtarea);
                        double nImpDia = oConsumo.nHorasDiaGlobal;         //Consumos totales del día de otras tareas.

                        double nTotalHoras = nHoras + nImpDia; // +nImpDiaTarea;
                        double nTotalTarea = nHoras; // +nImpDiaTarea;

                        if (nTotalHoras > 24)
                        {
                            bErrorControlado = true;
                            throw (new Exception("Las imputaciones del día " + dDiaAux.ToShortDateString() + " superan las 24h."));
                        }
                        #endregion

                        #region Control de huecos

                        ///Antes de hacer nada, comprobar que no se dejan huecos. 
                      
                        if (oProfesional.t314_controlhuecos)
                        {
                            ///Controlar si entre el último día imputado (f_ult_imputac)
                            ///y el primer día de imputación (dDiaAux) hay días laborables.
                        
                            if (existenHuecos(dDiaAux, oProfesional.t066_idcal, DateTime.Parse(sFecUltImputac)))
                            {
                                bErrorControlado = true;
                                throw (new Exception("Se ha detectado que entre el último día reportado y la fecha inicio imputación existen huecos."));
                            }
                        }
                        #endregion

                        #region Controlar el límite de esfuerzos

                        string sCLE = ControlLimiteEsfuerzos(tr, int.Parse(oImputacion["t332_idtarea"].ToString()), nHoras);
                        if (sCLE != "OK")
                        {
                            bErrorControlado = true;
                            throw (new Exception(sCLE));
                        }
                        #endregion

                        #region Obtención de datos relacionados con la tarea para posteriores validaciones

                        //Obtención de las horas estándar y festivos del rango de fechas.

                        Calendario oCalendario = new Calendario(oProfesional.t066_idcal);
                        try
                        {
                            oCalendario = obtenerDatosHorarios(tr, oProfesional.t066_idcal, DateTime.Parse(oImputacion["t337_fecha"].ToString()), DateTime.Parse(oImputacion["t337_fecha"].ToString()));
                        }
                        catch (Exception ex)
                        {
                            bErrorControlado = true;
                            throw (new Exception(ex.Message));
                        }

                        USUARIOPROYECTOSUBNODO oUPSN = new USUARIOPROYECTOSUBNODO();

                        try
                        {
                            oUPSN = USUARIOPROYECTOSUBNODO.Select(tr, oTarea.t305_idproyectosubnodo, int.Parse(oImputacion["t314_idusuario"].ToString()));
                        }
                        catch (Exception ex)
                        {
                            bErrorControlado = true;
                            throw (new Exception(ex.Message));
                        }

                        //Obtener las fechas de inicio y final de la asociación del recurso al proyecto.

                        DateTime dAltaProy = oUPSN.t330_falta;
                        DateTime? dBajaProy = (oUPSN.t330_fbaja.HasValue) ? oUPSN.t330_fbaja : null;

                        if (dAltaProy == DateTime.Parse("01/01/1900"))
                        {
                            bErrorControlado = true;
                            throw (new Exception("No existe fecha de alta en el proyecto."));
                        }
                        #endregion

                        #region Control día laborable y no festivo
                        foreach (DiaCal oDia in oCalendario.aHorasDia)
                        {
                            if (((DiaCal)oDia).dFecha == dDiaAux)
                            {
                                nHorasDia = float.Parse(((DiaCal)oDia).nHoras.ToString());
                                if (nHorasDia == 0) nJornadas = 1;
                                else nJornadas = nHoras / nHorasDia;
                                //Festivo
                                if (((DiaCal)oDia).nFestivo == 1)
                                {
                                    bFestAux = true;
                                    break;
                                }
                                //No laborable
                                switch (((DiaCal)oDia).dFecha.DayOfWeek)
                                {
                                    case DayOfWeek.Monday:
                                        if (oCalendario.nSemLabL == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Tuesday:
                                        if (oCalendario.nSemLabM == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Wednesday:
                                        if (oCalendario.nSemLabX == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Thursday:
                                        if (oCalendario.nSemLabJ == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Friday:
                                        if (oCalendario.nSemLabV == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Saturday:
                                        if (oCalendario.nSemLabS == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Sunday:
                                        if (oCalendario.nSemLabD == 0) bFestAux = true;
                                        break;
                                }
                                if (bFestAux) break;
                            }
                        }
                        #endregion

                        #region Control de jornada reducida

                        double nHorasRed = 0;
                        DateTime? dDesdeRed = null;
                        DateTime? dHastaRed = null;

                        if (oProfesional.t314_jornadareducida)
                        {
                            nHorasRed = oProfesional.t314_horasjor_red;
                            dDesdeRed = oProfesional.t314_fdesde_red;
                            dHastaRed = oProfesional.t314_fhasta_red;
                        }
                        #endregion

                        #region Controlar vigencia de la tarea

                        ///Control para verificar las fechas de vigencia de la tarea dentro del periodo seleccionado
                        if ((oTarea.t332_fiv == null || dDiaAux >= oTarea.t332_fiv) && (oTarea.t332_ffv == null || dDiaAux <= oTarea.t332_ffv))
                        {
                            #region Imputación
                            ///Control para verificar las fechas de asociación del recurso al proyecto.
                            if (dDiaAux >= dAltaProy && (dBajaProy == null || dDiaAux <= dBajaProy))
                            {
                                //Ahora, si el día es laborable y no festivo, insert de las horas estándar.
                                if (bFestivos || (!bFestivos && !bFestAux))
                                {
                                    ///Control de jornada reducida.
                                    if (oProfesional.t314_jornadareducida)
                                    {
                                        if (dDiaAux >= dDesdeRed && dDiaAux <= dHastaRed)
                                        {
                                            nHorasDia = float.Parse(nHorasRed.ToString());
                                            nJornadas = nHoras / nHorasDia;
                                        }
                                    }

                                    #region Controlar segun la naturaleza del proyecto si obliga imputar o no a jornada completa

                                    if (bRegjornocompleta == false)
                                    {
                                        if (float.Parse(nHoras.ToString()) != nHorasDia)
                                        {
                                            bErrorControlado = true;
                                            throw (new Exception("Es obligatorio realizar la imputación de esta tarea a jornada completa."));
                                        }
                                    }
                                    if (!bRegFes && bFestivos && bFestAux)
                                    {
                                        bErrorControlado = true;
                                        throw (new Exception("El proyecto económico de la tarea no permite imputar en festivos."));
                                    }
                                    #endregion

                                    //  Imputación.
                                    try
                                    {
                                        CONSUMOIAP.Insert(tr, int.Parse(oImputacion["t332_idtarea"].ToString()), int.Parse(oImputacion["t314_idusuario"].ToString()), dDiaAux, (float)nHoras, nJornadas, sComentario, DateTime.Now, (int)Session["NUM_EMPLEADO_ENTRADA"]);
                                    }
                                    catch (Exception ex)
                                    {
                                        bErrores = true;
                                        bErrorControlado = true;
                                        throw (new Exception(ex.Message));
                                    }
                                    //  Control de traspaso de IAP realizado

                                    SqlDataReader drT = TAREAPSP.flContolTraspasoIAP(tr, int.Parse(oImputacion["t332_idtarea"].ToString()), dDiaAux);
                                    while (drT.Read())
                                    {
                                        string sRe = GenerarCorreoTraspasoIAP(oProfesional.Profesional,
                                                     drT["mail"].ToString(),
                                                     int.Parse(drT["t301_idproyecto"].ToString()).ToString("#,###") + " " + drT["t301_denominacion"].ToString(),
                                                     drT["t331_despt"].ToString(), drT["t334_desfase"].ToString(),
                                                     drT["t335_desactividad"].ToString(),
                                                     int.Parse(oImputacion["t332_idtarea"].ToString()).ToString("#,###") + " " + drT["t332_destarea"].ToString(),
                                                     dDiaAux.ToString(), nHoras.ToString("N"));//
                                        string[] aRe = Regex.Split(sRe, "@#@");
                                        if (aRe[0] != "OK")
                                        {
                                            bErrorControlado = true;
                                            throw (new Exception(aRe[1]));
                                        }
                                    }
                                    drT.Close();
                                    drT.Dispose();                                   
                                }
                                //else
                                //{
                                //    bErrorControlado = true;
                                //    throw (new Exception("No se permite imputar a festivos."));
                                //}
                                iNumOk++;
                            }
                            else
                            {
                                bErrorControlado = true;
                                throw (new Exception("En la fecha de imputación seleccionada, el recurso se encuentra en parte o totalmente fuera de su asignación al proyecto. "));
                            }
                            #endregion
                        }
                        else
                        {
                            bErrorControlado = true;
                            throw (new Exception("La fecha de imputación seleccionada se encuentra en parte o totalmente fuera del periodo de vigencia la tarea. "));
                        }
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        if (!bErrorControlado) sResul = Errores.mostrarError("Datos de entrada: " + strLinea, ex);
                        //else sResul = "Datos de entrada: " + strLinea + ex.Message;
                        sResul = "Datos de entrada: " + strLinea + ex.Message + sAsistente;

                        sbE.Append(ponerFilaErrorGrab(nFila, sResul));
                        bErrores = true;
                    }
                }
                sbE.Append("</table>");

                if (bErrores == false)
                {
                    Conexion.CommitTransaccion(tr);
                }
                else
                {
                    Conexion.CerrarTransaccion(tr);
                }
                sResul = "OK";
                #endregion
            }

            try
            {
                if ((bErrores == false) && aListCorreo.Count > 0) 
                    Correo.EnviarCorreos(aListCorreo);
            }
            catch (Exception ex)
            {
                sResul = "Error@#@" + Errores.mostrarError("Error al enviar el mail a los responsables del proyecto", ex);
            }
        }
        catch (Exception ex)
        {
            //Errores.mostrarError("Error al tramitar el fichero", ex);
            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al procesar el fichero de entrada en la línea ", ex);
            else sResul = "Error@#@" + ex.Message;
            sbE.Append(ponerFilaErrorGrab(iCont, sResul));

            Conexion.CerrarTransaccion(tr);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string ponerFilaError(DesdeFicheroIAP oDesdeFicheroIAP, string sMens)
    {

        sb.Length = 0;
        if (Request.Form[Constantes.sPrefijo + "rdbImputacion"].ToString() == "D")
        {
            sb.Append("<tr style='cursor:default;height:16px;' id=" + iCont.ToString() + ">");
            sb.Append("<td><div style='cursor:default;width:235px;' title='" + oDesdeFicheroIAP.Profesional + "' class='NBR'>");
            sb.Append(oDesdeFicheroIAP.idusuario + "-" + oDesdeFicheroIAP.Profesional);
            sb.Append("</div></td><td>");
            sb.Append(oDesdeFicheroIAP.fechaDesde + "</td>");
            sb.Append("<td><div style='cursor:default;width:235px;' title='" + oDesdeFicheroIAP.t332_destarea + "' class='NBR'>");
            sb.Append(oDesdeFicheroIAP.idtarea + "-" + oDesdeFicheroIAP.t332_destarea);
            sb.Append("</div></td><td>");
            sb.Append(oDesdeFicheroIAP.esfuerzo);
            sb.Append("</td><td><div style='cursor:default;width:280px;' title='" + sMens + "' class='NBR'>");
            sb.Append(sMens);
            sb.Append("</div></td></tr>");
        }
        else
        {
            sb.Append("<tr style='cursor:default;height:16px;' id=" + iCont.ToString() + ">");
            sb.Append("<td><div style='cursor:default;width:200px;' title='" + oDesdeFicheroIAP.Profesional + "' class='NBR'>");
            sb.Append(oDesdeFicheroIAP.idusuario + "-" + oDesdeFicheroIAP.Profesional);
            sb.Append("</div></td><td>");
            sb.Append(oDesdeFicheroIAP.fechaDesde);
            sb.Append("</td><td>");
            sb.Append(oDesdeFicheroIAP.fechaHasta + "</td>");
            sb.Append("<td><div style='cursor:default;width:200px;' title='" + oDesdeFicheroIAP.t332_destarea + "' class='NBR'>");
            sb.Append(oDesdeFicheroIAP.idtarea + "-" + oDesdeFicheroIAP.t332_destarea);
            sb.Append("</div></td><td>");
            sb.Append(oDesdeFicheroIAP.esfuerzo);
            sb.Append("</td><td>");
            sb.Append(oDesdeFicheroIAP.festivos);
            sb.Append("</td><td><div style='cursor:default;width:220px;' title='" + sMens + "' class='NBR'>");
            sb.Append(sMens);
            sb.Append("</div></td></tr>");
        }
        return sb.ToString();
    }
    private string ponerFilaErrorGrab(int iCont, string sMens)
    {
        sb.Length = 0;
        sb.Append("<tr style='cursor:default;height:16px;' id=" + iCont.ToString() + ">");
        sb.Append("<td>");
        sb.Append(iCont.ToString());
        sb.Append("</td><td><div title='" + sMens + "' class='NBR' style='width:860px;cursor:default'>");
        sb.Append(sMens);
        sb.Append("</div></td></tr>");

        return sb.ToString();
    }

    private string FromASCIIByteArray(byte[] characters)
    {
        UTF7Encoding encoding = new UTF7Encoding();
        //ASCIIEncoding encoding = new ASCIIEncoding();
        //UTF8Encoding encoding = new UTF8Encoding();
        //UTF32Encoding encoding = new UTF32Encoding();
        //UnicodeEncoding encoding = new UnicodeEncoding();
        string constructedString = encoding.GetString(characters);
        return (constructedString);
    }
    private Calendario obtenerDatosHorarios(SqlTransaction tr, int iDCalendario, DateTime dDesde, DateTime dHasta)
    {
        Calendario objCal = new Calendario(iDCalendario);
        objCal.Obtener();
        objCal.ObtenerHorasRango(dDesde, dHasta);

        return objCal;
    }
    private bool existenHuecos(DateTime dDesde, int iDCalendario, DateTime dUDR)
    {
        bool bResul = false;
        Calendario oCal = new Calendario(iDCalendario);
        oCal.Obtener();
        oCal.ObtenerHorasRango(dUDR, dDesde);

        int nDif = Fechas.DateDiff("day", dUDR, dDesde);
        if (nDif <= 0)
        {
            ///Si nDif fuera menor o igual a 0, es que se va a imputar a una fecha anterior a
            ///la fecha de última imputación, por lo que no hay huecos.
            bResul = false;
        }
        else
        {
            bool bFestAux = false;
            for (int i = 1; i < nDif; i++)
            {
                bFestAux = false;
                DateTime dDiaAux = dUDR.AddDays(i);
                #region laborable y no festivo
                foreach (DiaCal oDia in oCal.aHorasDia)
                {
                    if (((DiaCal)oDia).dFecha == dDiaAux)
                    {
                        //Festivo
                        if (((DiaCal)oDia).nFestivo == 1)
                        {
                            bFestAux = true;
                            break;
                        }
                        //No laborable
                        switch (((DiaCal)oDia).dFecha.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                if (oCal.nSemLabL == 0) bFestAux = true;
                                break;
                            case DayOfWeek.Tuesday:
                                if (oCal.nSemLabM == 0) bFestAux = true;
                                break;
                            case DayOfWeek.Wednesday:
                                if (oCal.nSemLabX == 0) bFestAux = true;
                                break;
                            case DayOfWeek.Thursday:
                                if (oCal.nSemLabJ == 0) bFestAux = true;
                                break;
                            case DayOfWeek.Friday:
                                if (oCal.nSemLabV == 0) bFestAux = true;
                                break;
                            case DayOfWeek.Saturday:
                                if (oCal.nSemLabS == 0) bFestAux = true;
                                break;
                            case DayOfWeek.Sunday:
                                if (oCal.nSemLabD == 0) bFestAux = true;
                                break;
                        }
                        if (bFestAux) break;
                    }
                }
                #endregion
                if (!bFestAux)
                {
                    bResul = true;
                    break;
                }
            }
        }


        return bResul;
    }
    private string ControlLimiteEsfuerzos(SqlTransaction tr, int nTarea, double nHoras)
    {
        string sResul = "OK", sTipoCle = "", sDesTarea = "";
        double dCle = 0;
        int idProy = -1, idPT = -1;//, idRTPT=-1;
        SqlDataReader dr = TAREAPSP.flContolLimiteEsfuerzos(tr, nTarea);
        if (dr.Read())
        {
            dCle = double.Parse(dr["t332_cle"].ToString());
            sTipoCle = dr["t332_tipocle"].ToString();
            sDesTarea = dr["t332_destarea"].ToString();
            idProy = int.Parse(dr["t301_idproyecto"].ToString());
            idPT = int.Parse(dr["t331_idpt"].ToString());
        }
        dr.Close();
        dr.Dispose();
        if (idProy != -1)
        {
            TAREAPSP o2 = TAREAPSP.ObtenerDatosIAP(tr, nTarea);

            if (dCle > 0 && o2.nConsumidoHoras + nHoras > dCle)
            {
                if (sTipoCle == "I")//Control de límite de esfuerzos Informativo
                {
                    sResul = "OK";
                    //Inserto registro para que el proceso nocturno avise de la situación a cada RTPT de la tarea
                    //De momento lo hago por trigger
                    //SqlDataReader dr2 = RTPT.Catalogo(idPT, null, 2, 0);
                    //while (dr2.Read())
                    //{
                    //    idRTPT = int.Parse(dr2["t314_idusuario"].ToString());
                    //    Consumo.InsertarCorreo(tr, 12, true, false, idRTPT, nTarea, null, "", idProy);
                    //}
                    //dr2.Close();
                    //dr2.Dispose();
                }
                else if (sTipoCle == "B")//Control de límite de esfuerzos Bloqueante
                {
                    ///Indicación de que con la imputación realizada se va a sobrepasar el límite de esfuerzos y cortar la transacción.
                    sResul = "Se ha sobrepasado el límite de horas máximo permitido ";
                    sResul += "para la tarea '" + nTarea.ToString() + " " + sDesTarea + "'.";
                }
            }
        }
        return sResul;
    }
    protected string GenerarCorreoTraspasoIAP(string sProfesional, string sTO, string sProy, string sProyTec, string sFase, string sActiv,
                                              string sTarea, string sFecha, string sConsumo)
    {
        string sResul = "", sAsunto = "", sTexto = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            sAsunto = "Imputación en IAP a tarea con el traspaso de dedicaciones al módulo económico ya realizado.";

            sb.Append("<BR>SUPER le informa de que se ha producido una imputación de consumo a tarea en IAP estando el traspaso de dedicaciones al módulo económico realizado.");
            sb.Append("<BR>La imputación ha sido realizada por " + Session["DES_EMPLEADO_ENTRADA"].ToString() + "<BR><BR>");
            //sb.Append("<label style='width:120px'>Proyecto económico: </label>" + aDatosTarea[2] + @" - " + Utilidades.unescape(aDatosTarea[3]) + "<br>");
            sb.Append("<label style='width:120px'>Profesional: </label><b>" + sProfesional + "</b><br>");
            sb.Append("<label style='width:120px'>Proyecto económico: </label><b>" + sProy + "</b><br>");
            sb.Append("<label style='width:120px'>Proyecto Técnico: </label>" + sProyTec + "<br>");
            if (sFase != "") sb.Append("<label style='width:120px'>Fase: </label>" + sFase + "<br>");
            if (sActiv != "") sb.Append("<label style='width:120px'>Actividad: </label>" + sActiv + "<br>");
            //sb.Append("<label style='width:120px'>Tarea: </label><b>" + sIdTarea + @" - " + Utilidades.unescape(aDatosTarea[1]) + "</b><br><br>");
            sb.Append("<label style='width:120px'>Tarea: </label>" + sTarea + "<br>");
            sb.Append("<label style='width:120px'>Fecha: </label>" + sFecha.Substring(0, 10) + "<br>");
            sb.Append("<label style='width:120px'>Dedicación: </label>" + sConsumo + "<br><br>");
            sTexto = sb.ToString();

            string[] aMail = { sAsunto, sTexto, sTO };
            aListCorreo.Add(aMail);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo de imputación IAP a tarea con traspaso IAP ya realizado.", ex);
        }
        return sResul;
    }
}

