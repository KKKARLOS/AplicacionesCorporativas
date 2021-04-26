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

using System.IO;
using GASVI.BLL;
using System.Net.Mime;

public partial class Capa_Presentacion_Documentos_Descargar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sTipo = Request.QueryString["sTipo"].ToString();
        int nIDDOC = int.Parse(Request.QueryString["nIDDOC"].ToString());

        Response.ClearContent();
        Response.ClearHeaders();
        Response.Buffer = true;

        //switch (sTipo)
        //{
        //    case "AS_T"://Asunto de Bitácora de TAREA
        //        DOCASU_T oDocAS_T = DOCASU_T.Select(null, nIDDOC, true);
        //        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + oDocAS_T.t602_nombrearchivo + "\"");
        //        Response.BinaryWrite(oDocAS_T.t602_archivo);
        //        break;
        //    case "AC_T"://Acción de Bitácora de TAREA
        //        DOCACC_T oDocAC_T = DOCACC_T.Select(null, nIDDOC, true);
        //        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + oDocAC_T.t603_nombrearchivo + "\"");
        //        Response.BinaryWrite(oDocAC_T.t603_archivo);
        //        break;
        //    case "AS_PT"://Asunto de Bitácora de PT
        //        DOCASU_PT oDocAS_PT = DOCASU_PT.Select(null, nIDDOC, true);
        //        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + oDocAS_PT.t411_nombrearchivo + "\"");
        //        Response.BinaryWrite(oDocAS_PT.t411_archivo);
        //        break;
        //    case "AC_PT"://Acción de Bitácora de PT
        //        DOCACC_PT oDocAC_PT = DOCACC_PT.Select(null, nIDDOC, true);
        //        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + oDocAC_PT.t412_nombrearchivo + "\"");
        //        Response.BinaryWrite(oDocAC_PT.t412_archivo);
        //        break;
        //    case "AS"://Asunto de Bitácora
        //    case "AS_PE"://Asunto de Bitácora
        //        DOCASU oDocAS = DOCASU.Select(null, nIDDOC, true);

        //        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + oDocAS.t386_nombrearchivo + "\"");
        //        Response.BinaryWrite(oDocAS.t386_archivo);
        //        break;
        //    case "AC"://Acción de Bitácora
        //    case "AC_PE"://Acción de Bitácora
        //        DOCACC oDocAC = DOCACC.Select(null, nIDDOC, true);

        //        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + oDocAC.t387_nombrearchivo + "\"");
        //        Response.BinaryWrite(oDocAC.t387_archivo);
        //        break;
        //    case "IAP_T":
        //    case "T":
        //        DOCUT oDocT = DOCUT.Select(null, nIDDOC, true);

        //        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + oDocT.t363_nombrearchivo + "\"");
        //        Response.BinaryWrite(oDocT.t363_archivo);
        //        break;
        //    case "A": //Actividad
        //        DOCUA oDocA = DOCUA.Select(null, nIDDOC, true);

        //        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + oDocA.t365_nombrearchivo + "\"");
        //        Response.BinaryWrite(oDocA.t365_archivo);
        //        break;
        //    case "F": //Fase
        //        DOCUF oDocF = DOCUF.Select(null, nIDDOC, true);

        //        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + oDocF.t364_nombrearchivo + "\"");
        //        Response.BinaryWrite(oDocF.t364_archivo);
        //        break;
        //    case "PT": //Proyecto Técnico
        //        DOCUPT oDocPT = DOCUPT.Select(null, nIDDOC, true);

        //        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + oDocPT.t362_nombrearchivo + "\"");
        //        Response.BinaryWrite(oDocPT.t362_archivo);
        //        break;
        //    case "PE": //Proyecto Económico
        //    case "PSN": //Proyecto Económico
        //        DOCUPE oDocPE = DOCUPE.Select(null, nIDDOC, true);

        //        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + oDocPE.t368_nombrearchivo + "\"");
        //        Response.BinaryWrite(oDocPE.t368_archivo);
        //        break;
        //    case "HT": //Hito lineal
        //    case "HM": //Hito discontinuo
        //        DOCUH oDocH = DOCUH.Select(null, nIDDOC, true);

        //        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + oDocH.t366_nombrearchivo + "\"");
        //        Response.BinaryWrite(oDocH.t366_archivo);
        //        break;
        //    case "HF": //Hito de fecha
        //        DOCUHE oDocHE = DOCUHE.Select(null, nIDDOC, true);

        //        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + oDocHE.t367_nombrearchivo + "\"");
        //        Response.BinaryWrite(oDocHE.t367_archivo);
        //        break;
        //    case "OF": //ORDEN DE FACTURACIÓN
        //        DOCUOF oDocOF = DOCUOF.Select(null, nIDDOC, true);

        //        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + oDocOF.t624_nombrearchivo + "\"");
        //        Response.BinaryWrite(oDocOF.t624_archivo);
        //        break;
        //}

        Response.Flush();
        Response.Close();
        Response.End();
    }
}
