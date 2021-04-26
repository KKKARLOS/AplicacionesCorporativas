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
using SUPER.BLL;

public partial class Calendario_getRango : System.Web.UI.Page
{
    public string strErrores;
    public string sFechaDesde = "", sFechaHasta = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
//                MostrarCalendario(DateTime.Today.Year*100 + DateTime.Today.Month);
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }

                if (Request.QueryString["desde"] != null)
                    sFechaDesde = Request.QueryString["desde"].ToString();

                if (Request.QueryString["hasta"] != null)
                    sFechaHasta = Request.QueryString["hasta"].ToString();
            }
            catch (Exception ex)
            {
                this.strErrores = SUPER.Capa_Negocio.Errores.mostrarError("Error al obtener el calendario", ex);
            }
        }
        else
        {
            //this.nAnno = int.Parse(this.txtAnno.Text);
        }
    }

    #region métodos antiguos
    //private void MostrarCalendario(int nAnnomes)
    //{
    //    generarTablaMes("tblMesIni1", Fechas.AddAnnomes(nAnnomes, -1));
    //    generarTablaMes("tblMesIni2", nAnnomes);
    //    generarTablaMes("tblMesFin1", Fechas.AddAnnomes(nAnnomes, -1));
    //    generarTablaMes("tblMesFin2", nAnnomes);
 
    //}//Fin de método
    //protected void generarTablaMes(string sTabla, int nAnnomes)
    //{
    //   // Table objTabla = (Table)this.FindControl("tblCalendarios");
    //    TableRow objFila;
    //    TableCell objCelda;


    //    int nIndiceItemDia = 0;

    //    Table objTablaMes = (Table)this.FindControl(sTabla);
    //    //  objTablaMes.Attributes.Add("border", "1");
    //    bool bEntraMes = false;
    //    int nIndiceFila = 1;
    //    int nIndiceColumna = 1;

    //    objFila = new TableRow();
    //    DateTime dFecha = Fechas.AnnomesAFecha(nAnnomes);
    //    int nDiasMes = DateTime.DaysInMonth(dFecha.Year,dFecha.Month);

    //    for (int i = nIndiceItemDia; i < nDiasMes; i++)
    //    {
    //        DateTime objFecha = dFecha.AddDays(i);

    //        if (!bEntraMes)
    //        {
    //            bEntraMes = true;
    //            //DayOfWeek objDiaSemana = objFecha.DayOfWeek;
    //            #region Día de la semana del día uno de mes.
    //            switch (objFecha.DayOfWeek)
    //            {
    //                case DayOfWeek.Monday:
    //                    {
    //                        nIndiceColumna = 1;
    //                        break;
    //                    }
    //                case DayOfWeek.Tuesday:
    //                    {
    //                        nIndiceColumna = 2;
    //                        break;
    //                    }
    //                case DayOfWeek.Wednesday:
    //                    {
    //                        nIndiceColumna = 3;
    //                        break;
    //                    }
    //                case DayOfWeek.Thursday:
    //                    {
    //                        nIndiceColumna = 4;
    //                        break;
    //                    }
    //                case DayOfWeek.Friday:
    //                    {
    //                        nIndiceColumna = 5;
    //                        break;
    //                    }
    //                case DayOfWeek.Saturday:
    //                    {
    //                        nIndiceColumna = 6;
    //                        break;
    //                    }
    //                case DayOfWeek.Sunday:
    //                    {
    //                        nIndiceColumna = 7;
    //                        break;
    //                    }
    //            }
    //            #endregion
    //        }

    //        if ((objFecha.Day == 1) && (nIndiceColumna > 1))
    //        {
    //            objCelda = new TableCell();
    //            objCelda.ColumnSpan = nIndiceColumna - 1;
    //            objCelda.Controls.Add(new LiteralControl(@"&nbsp;"));
    //            objFila.Controls.Add(objCelda);
    //        }

    //        objCelda = new TableCell();
    //        objCelda.SkinID = "TDCal";
    //        objCelda.Attributes.Add("onclick", "selFecha(this.id)");

    //        string sIdAux = FechaSinBarras(objFecha);
    //        objCelda.ID = sTabla +"td_"+ sIdAux;

    //        Label objLabel1 = new Label();
    //        objLabel1.ID = sTabla +"fec_" + sIdAux;

    //        objLabel1.SkinID = "Calendario";

    //        if (objFecha.DayOfWeek == DayOfWeek.Saturday || objFecha.DayOfWeek == DayOfWeek.Sunday) 
    //            objLabel1.SkinID = "CalendarioFinde";

    //        objLabel1.Text = objFecha.Day.ToString();
    //        //if (objDiaCal.nFestivo == 1) objLabel1.SkinID = "CalendarioFestivo";

    //        objCelda.Controls.Add(objLabel1);

    //        objFila.Controls.Add(objCelda);

    //        if (nIndiceColumna == 7)
    //        {
    //            objTablaMes.Controls.Add(objFila);
    //            objFila = new TableRow();
    //            nIndiceFila++;
    //            nIndiceColumna = 1;
    //        }
    //        else
    //        {
    //            nIndiceColumna++;
    //        }

    //        nIndiceItemDia++;

    //        if (i == nDiasMes - 1)
    //        {
    //            objTablaMes.Controls.Add(objFila);
    //        }

    //    }//Fin de bucle de días
    //    if (nIndiceFila == 5)
    //    {
    //        objFila = new TableRow();
    //        objCelda = new TableCell();
    //        objCelda.ColumnSpan = 7;
    //        //objCelda.Controls.Add(new LiteralControl(@"&nbsp;<br />&nbsp;"));
    //        objCelda.Controls.Add(new LiteralControl(@"&nbsp;"));
    //        objFila.Controls.Add(objCelda);
    //        objTablaMes.Controls.Add(objFila);
    //    }

    //}
    //public string FechaSinBarras(DateTime objFecha)
    //{
    //    string sDia;
    //    string sMes;
    //    string sAnno;

    //    sDia = objFecha.Day.ToString();
    //    if (sDia.Length == 1) sDia = "0" + sDia;
    //    sMes = objFecha.Month.ToString();
    //    if (sMes.Length == 1) sMes = "0" + sMes;
    //    sAnno = objFecha.Year.ToString();
    //    if (sAnno.Length == 2) sAnno = "20" + sAnno;

    //    return sDia + sMes + sAnno;
    //}
    #endregion
}
