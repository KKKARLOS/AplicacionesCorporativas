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
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_IAP_Calendario_Horario : System.Web.UI.Page
{
    public string strErrores;
    int nAnno;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            string sCalAux = Utilidades.decodpar(Request.QueryString["nCalendario"]);
            string sAnnoAux = Utilidades.decodpar(Request.QueryString["nAnno"]);
            if (sCalAux != null)
            {
                this.hdnIDCalendario.Text = sCalAux;
            }
            else
            {
                this.hdnIDCalendario.Text = "0";
            }

            if (sAnnoAux != null)
            {
                this.txtAnno.Text = sAnnoAux;
            }
            else
            {
                this.txtAnno.Text = DateTime.Now.Year.ToString();
            }

            this.nAnno = int.Parse(this.txtAnno.Text);

            try
            {
                MostrarCalendario();
            }
            catch (Exception ex)
            {
                this.strErrores = Errores.mostrarError("Error al obtener el desglose horario", ex);
            }
        }
        else
        {
            this.nAnno = int.Parse(this.txtAnno.Text);
        }
        this.txtAnno.ReadOnly = true;
    }

    private void MostrarCalendario()
    {
        this.nAnno = int.Parse(this.txtAnno.Text);

        Table objTabla = (Table)this.FindControl("tblCalendarios");
        TableRow objFila;
        TableCell objCelda;

        Calendario objCal = Calendario.Obtener(int.Parse(this.hdnIDCalendario.Text), this.nAnno);

        this.lblDesCalendario.Text = objCal.sDesCal;
        objCal.ObtenerHoras(this.nAnno);

        int nIndiceItemDia = 0;

        for (int nIndiceMes = 1; nIndiceMes <= 12; nIndiceMes++)
        {
            Table objTablaMes = (Table)objTabla.FindControl("tblMes" + nIndiceMes.ToString());
            bool bEntraMes = false;
            int nIndiceFila = 1;
            int nIndiceColumna = 1;

            objFila = new TableRow();

            for (int i = nIndiceItemDia; i < objCal.aHorasDia.Count; i++)
            {
                DiaCal objDiaCal = (DiaCal)objCal.aHorasDia[i];
                DateTime objFecha = objDiaCal.dFecha;
                if (objFecha.Month != nIndiceMes)
                {
                    objTablaMes.Controls.Add(objFila);
                    if (nIndiceFila < 6)
                    {
                        objFila = new TableRow();
                        objCelda = new TableCell();
                        objCelda.ColumnSpan = 7;
                        objCelda.Controls.Add(new LiteralControl(@"&nbsp;<br />&nbsp;"));
                        objFila.Controls.Add(objCelda);
                        objTablaMes.Controls.Add(objFila);
                    }
                    break;
                }

                if (!bEntraMes)
                {
                    bEntraMes = true;
                    DayOfWeek objDiaSemana = objFecha.DayOfWeek;
                    #region día de la semana del día uno de mes.
                    switch (objDiaSemana)
                    {
                        case DayOfWeek.Monday:
                            {
                                nIndiceColumna = 1;
                                break;
                            }
                        case DayOfWeek.Tuesday:
                            {
                                nIndiceColumna = 2;
                                break;
                            }
                        case DayOfWeek.Wednesday:
                            {
                                nIndiceColumna = 3;
                                break;
                            }
                        case DayOfWeek.Thursday:
                            {
                                nIndiceColumna = 4;
                                break;
                            }
                        case DayOfWeek.Friday:
                            {
                                nIndiceColumna = 5;
                                break;
                            }
                        case DayOfWeek.Saturday:
                            {
                                nIndiceColumna = 6;
                                break;
                            }
                        case DayOfWeek.Sunday:
                            {
                                nIndiceColumna = 7;
                                break;
                            }
                    }
                    #endregion
                }


                if ((objFecha.Day == 1) && (nIndiceColumna > 1))
                {
                    objCelda = new TableCell();
                    objCelda.ColumnSpan = nIndiceColumna - 1;
                    objCelda.Controls.Add(new LiteralControl(@"&nbsp;"));
                    objFila.Controls.Add(objCelda);
                }

                objCelda = new TableCell();
                objCelda.SkinID = "TDCal";
                Label objLabel1 = new Label();

                objLabel1.SkinID = "Calendario";
                bool bEstiloFinde = false;
                switch (nIndiceColumna)
                {
                    case 1:
                        {
                            if (objCal.nSemLabL == 0) bEstiloFinde = true;
                            break;
                        }
                    case 2:
                        {
                            if (objCal.nSemLabM == 0) bEstiloFinde = true;
                            break;
                        }
                    case 3:
                        {
                            if (objCal.nSemLabX == 0) bEstiloFinde = true;
                            break;
                        }
                    case 4:
                        {
                            if (objCal.nSemLabJ == 0) bEstiloFinde = true;
                            break;
                        }
                    case 5:
                        {
                            if (objCal.nSemLabV == 0) bEstiloFinde = true;
                            break;
                        }
                    case 6:
                        {
                            if (objCal.nSemLabS == 0) bEstiloFinde = true;
                            break;
                        }
                    case 7:
                        {
                            if (objCal.nSemLabD == 0) bEstiloFinde = true;
                            break;
                        }
                }
                if (bEstiloFinde) objLabel1.SkinID = "CalendarioFinde";

                objLabel1.Text = objFecha.Day.ToString();
                if (objDiaCal.nFestivo == 1) objLabel1.SkinID = "CalendarioFestivo";

                objCelda.Controls.Add(objLabel1);

                objCelda.Controls.Add(new LiteralControl(@"<br />"));

                Label objLabel2 = new Label();
                objLabel2.SkinID = "Horas";
                objLabel2.Text = objDiaCal.nHoras.ToString("#.###,##");
                if (objLabel2.Text == "") objLabel2.Text = "&nbsp;";

                objCelda.Controls.Add(objLabel2);

                objFila.Controls.Add(objCelda);

                if (nIndiceColumna == 7)
                {
                    objTablaMes.Controls.Add(objFila);
                    objFila = new TableRow();
                    nIndiceFila++;
                    nIndiceColumna = 1;
                }
                else
                {
                    nIndiceColumna++;
                }

                nIndiceItemDia++;

                if ((objFecha.AddDays(1).Month != nIndiceMes) && (nIndiceColumna == 1))
                {
                    objFila = new TableRow();
                    objCelda = new TableCell();
                    objCelda.ColumnSpan = 7;
                    objCelda.Controls.Add(new LiteralControl(@"&nbsp;<br />&nbsp;"));
                    objFila.Controls.Add(objCelda);
                    objTablaMes.Controls.Add(objFila);
                }
                else if (i == objCal.aHorasDia.Count - 1)
                {
                    objTablaMes.Controls.Add(objFila);
                }

            }//Fin de bucle de días
        }//Fin de bucle de meses


        #region Control si ha datos años anterior y siguiente
        //this.imgAnterior.Visible = true;
        //this.imgSiguiente.Visible = true;
        //Calendario objCalAnt = Calendario.Obtener(int.Parse(this.hdnIDCalendario.Text));
        //objCalAnt.ObtenerHoras(this.nAnno-1);
        //double nCountAnt = 0;
        //for (int i = 0; i < 10; i++) nCountAnt += ((DiaCal)objCalAnt.aHorasDia[i]).nHoras;
        //if (nCountAnt == 0) this.imgAnterior.Visible = false;

        //Calendario objCalPos = Calendario.Obtener(int.Parse(this.hdnIDCalendario.Text));
        //objCalPos.ObtenerHoras(this.nAnno + 1);
        //double nCountPos = 0;
        //for (int i = 0; i < 10; i++) nCountPos += ((DiaCal)objCalPos.aHorasDia[i]).nHoras;
        //if (nCountPos == 0) this.imgSiguiente.Visible = false;
        #endregion

    }//Fin de método

    protected void imgAnterior_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        this.nAnno = int.Parse(this.txtAnno.Text) - 1;
        this.txtAnno.Text = this.nAnno.ToString();
        MostrarCalendario();
    }

    protected void imgSiguiente_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        this.nAnno = int.Parse(this.txtAnno.Text) + 1;
        this.txtAnno.Text = this.nAnno.ToString();
        MostrarCalendario();
    }


}
