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
using EO.Web; 
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;

    int nAnno;
    double nTotalHoras = 0;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 3;
            Master.sbotonesOpcionOn = "4,26,27,28,29,31,6";
            Master.sbotonesOpcionOff = "4";
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            //Poniendo el siguiente atributo a true, se incluye el fichero javascript propio
            //de la carpeta hija "Functions/funciones.js"
            Master.bFuncionesLocales = true;
            Master.FicherosCSS.Add("Capa_Presentacion/Administracion/Calendario/Horario/Calendario.css");
            Master.TituloPagina = "Desglose horario de calendario";

            if (!Page.IsPostBack)
            {
                string sCalAux = Request.QueryString["nCalendario"];
                if (sCalAux != null)
                {
                    this.hdnIDCalendario.Text = sCalAux;
                }
                else
                {
                    this.hdnIDCalendario.Text = "0";
                }

                this.txtAnno.Text = DateTime.Now.Year.ToString();
                this.nAnno = int.Parse(this.txtAnno.Text);
                try
                {
                    MostrarCalendario();
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener el desglose horario", ex);
                }

            }
            else
            {
                this.nAnno = int.Parse(this.txtAnno.Text);
            }

            this.txtFecIni.Attributes.Add("readonly", "readonly");
            this.txtFecFin.Attributes.Add("readonly", "readonly");
            this.txtAnno.Attributes.Add("readonly", "readonly");

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    private void MostrarCalendario()
    {
        int nJornHabiles = 0;
        this.nAnno = int.Parse(this.txtAnno.Text);

        Table objTabla = (Table)this.tblContenedor.FindControl("tblCalendarios");
        TableRow objFila;
        TableCell objCelda;

        Calendario objCal = Calendario.Obtener(int.Parse(this.hdnIDCalendario.Text), this.nAnno);

        this.lblDesCalendario.Text = objCal.sDesCal;
        this.txtJV.Text = objCal.njvacac.ToString();
        this.txtHL.Text = objCal.nhlacv.ToString();

        hdnCodProvincia.Text = objCal.nCodProvincia.ToString();

        if (objCal.nSemLabL == 1) this.chkSemLabL.Checked = true;
        if (objCal.nSemLabM == 1) this.chkSemLabM.Checked = true;;
        if (objCal.nSemLabX == 1) this.chkSemLabX.Checked = true;;
        if (objCal.nSemLabJ == 1) this.chkSemLabJ.Checked = true;;
        if (objCal.nSemLabV == 1) this.chkSemLabV.Checked = true;;
        if (objCal.nSemLabS == 1) this.chkSemLabS.Checked = true;;
        if (objCal.nSemLabD == 1) this.chkSemLabD.Checked = true;;
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
                    objCelda.Controls.Add(new  LiteralControl(@"&nbsp;"));
                    objFila.Controls.Add(objCelda);
                }

                objCelda = new TableCell();
                objCelda.SkinID = "TDCal";
                objCelda.Attributes.Add("onclick", "selFecha(this.id)");

                string sIdAux = FechaSinBarras(objFecha);
                objCelda.ID = "td_" + sIdAux;

                Label objLabel1 = new Label();
                objLabel1.ID = "fec_" + sIdAux;

                objLabel1.SkinID = "Calendario";
                switch (nIndiceColumna)
                {
                    case 1:
                        {
                            if (objCal.nSemLabL == 0) objLabel1.SkinID = "CalendarioFinde";
                            break;
                        }
                    case 2:
                        {
                            if (objCal.nSemLabM == 0) objLabel1.SkinID = "CalendarioFinde";
                            break;
                        }
                    case 3:
                        {
                            if (objCal.nSemLabX == 0) objLabel1.SkinID = "CalendarioFinde";
                            break;
                        }
                    case 4:
                        {
                            if (objCal.nSemLabJ == 0) objLabel1.SkinID = "CalendarioFinde";
                            break;
                        }
                    case 5:
                        {
                            if (objCal.nSemLabV == 0) objLabel1.SkinID = "CalendarioFinde";
                            break;
                        }
                    case 6:
                        {
                            if (objCal.nSemLabS == 0) objLabel1.SkinID = "CalendarioFinde";
                            break;
                        }
                    case 7:
                        {
                            if (objCal.nSemLabD == 0) objLabel1.SkinID = "CalendarioFinde";
                            break;
                        }
                }

                objLabel1.Text = objFecha.Day.ToString();
                if (objDiaCal.nFestivo == 1) objLabel1.SkinID = "CalendarioFestivo";
                objCelda.Controls.Add(objLabel1);

                //Miro si es una jornada hábili o no
                if (objLabel1.SkinID != "CalendarioFestivo" && objLabel1.SkinID != "CalendarioFinde")
                    nJornHabiles++;

                objCelda.Controls.Add(new LiteralControl(@"<br />"));

                Label objLabel2 = new Label();
                objLabel2.ID = "hor_" + sIdAux;
                objLabel2.SkinID = "Horas";
                nTotalHoras = nTotalHoras + objDiaCal.nHoras;
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


        this.lblTotalHoras.Text = nTotalHoras.ToString("N");
        this.txtJH.Text = nJornHabiles.ToString();
        int nAux = nJornHabiles;
        if (objCal.njvacac != null)
            nAux = nAux - (int)objCal.njvacac;
        this.txtJD.Text = nAux.ToString();

    }//Fin de método

    public string FechaSinBarras(DateTime objFecha)
    {
        string sDia;
        string sMes;
        string sAnno;

        sDia = objFecha.Day.ToString();
        if (sDia.Length == 1) sDia = "0" + sDia;
        sMes = objFecha.Month.ToString();
        if (sMes.Length == 1) sMes = "0" + sMes;
        sAnno = objFecha.Year.ToString();
        if (sAnno.Length == 2) sAnno = "20" + sAnno;

        return sDia + sMes + sAnno;
    }

    protected string Grabar(string sIDCal, string sAnno, string sSemLab, string strHoras, string sJorVac, string sHorLab)
    {
        string sResul = "";

        Calendario objCal = Calendario.Obtener(int.Parse(sIDCal), int.Parse(sAnno));

        ArrayList aHoras = objCal.aHorasDia;
        DiaCal objDiaCal;

        string[] aDias = Regex.Split(strHoras, "##");

        foreach (string oDia in aDias)
        {
            string[] aValores = Regex.Split(oDia, "//");

            string strID = aValores[0];
            string strValor = aValores[1];
            if (strValor.Trim() == "") strValor = "0";
            string strFecha = strID.Substring(15, 8);
            DateTime objFecha = new DateTime(int.Parse(strFecha.Substring(4, 4)), int.Parse(strFecha.Substring(2, 2)), int.Parse(strFecha.Substring(0, 2)));
            objDiaCal = new DiaCal(int.Parse(this.hdnIDCalendario.Text), objFecha, double.Parse(strValor), int.Parse(aValores[2]));
            aHoras.Add(objDiaCal);
        }

        objCal.aHorasDia = aHoras;

        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }

        try
        {
            string[] aSemLab = Regex.Split(sSemLab, "//");
            objCal.nSemLabL = int.Parse(aSemLab[0]);
            objCal.nSemLabM = int.Parse(aSemLab[1]);
            objCal.nSemLabX = int.Parse(aSemLab[2]);
            objCal.nSemLabJ = int.Parse(aSemLab[3]);
            objCal.nSemLabV = int.Parse(aSemLab[4]);
            objCal.nSemLabS = int.Parse(aSemLab[5]);
            objCal.nSemLabD = int.Parse(aSemLab[6]);
            //Mikel 10/09/2007 le asigno el usuario modificador porque sino da error el trigger del proc almacenado PSP_CALENDARIOU
            objCal.nModificador = int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString());

            objCal.Modificar(tr);

            if (sJorVac != "") objCal.njvacac = int.Parse(sJorVac);
            if (sHorLab != "") objCal.nhlacv = int.Parse(sHorLab.Replace(".",""));
            objCal.UpdateJornadas(tr, int.Parse(sAnno));

            objCal.InsertarHoras(tr, int.Parse(sAnno));

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar el desglose horario", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    protected string Borrar(string sIDCal, string sAnno)
    {
        string sResul = "";

        try
        {
            DiaCal.Eliminar(int.Parse(sIDCal), int.Parse(sAnno));
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al borrar el desglose horario", ex);
        }
        return sResul;
    }

    protected void Regresar()
    {
        try
        {
            Response.Redirect(HistorialNavegacion.Leer(), true);
        }
        catch (System.Threading.ThreadAbortException) { }
    }
	protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
	{
        int iPos;
		switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                string strUrl = HistorialNavegacion.Leer();
                if (Request.QueryString["nCalendario"].ToString() != "0")
                {
                    iPos = strUrl.IndexOf("?nCalendario=0");
                    if (iPos != -1)
                    {
                        string sAux = strUrl.Substring(0, iPos);
                        sAux += "?nCalendario=" + Request.QueryString["nCalendario"].ToString();
                        //sAux += strUrl.Substring(iPos + 14);
                        strUrl = sAux;
                    }
                }

                try
                {
                    Response.Redirect(strUrl, true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
        }
    }
    
    protected void imgAnterior_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        this.nAnno = int.Parse(this.txtAnno.Text) - 1;
        this.txtAnno.Text = this.nAnno.ToString();
        MostrarCalendario();
        this.txtFecIni.Text = "";
        this.txtFecFin.Text = "";
        this.txtHoras.Text = "";
        foreach (ListItem oDia in this.chklstDias.Items)
        {
            oDia.Selected = false;
        }
    }

    protected void imgSiguiente_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        this.nAnno = int.Parse(this.txtAnno.Text) + 1;
        this.txtAnno.Text = this.nAnno.ToString();
        MostrarCalendario();
        this.txtFecIni.Text = "";
        this.txtFecFin.Text = "";
        this.txtHoras.Text = "";
        foreach (ListItem oDia in this.chklstDias.Items)
        {
            oDia.Selected = false;
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
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6]);
                break;
            case ("cargarFestivos"):
                sResultado += cargarFestivos(aArgs[1], aArgs[2]);
                break;
            case ("borrar"):
                sResultado += Borrar(aArgs[1], aArgs[2]);
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
    protected string cargarFestivos(string sCodProvincia, string sAno)
    {
        string sResul = "";

        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader dr = SUPER.Capa_Negocio.Calendario.ObtenerFestivosProvincia(int.Parse(sCodProvincia), int.Parse(sAno)); //Mostrar todos todos las provincias relacionadas a un país determinado

            while (dr.Read())
            {
                DateTime objDate = (DateTime)dr["t061_dia"];
                string sDia = objDate.Day.ToString();
                if (sDia.Length == 1) sDia = "0" + sDia;
                string sMes = objDate.Month.ToString();
                if (sMes.Length == 1) sMes = "0" + sMes;
                string sFecha = sDia + sMes;//+objDate.Year.ToString();

                sb.Append(sFecha + "///");
            }
            dr.Close();
            dr.Dispose();
            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al cargar el calendario festivo de una provincia", ex);
        }
        return sResul;
    }
}
