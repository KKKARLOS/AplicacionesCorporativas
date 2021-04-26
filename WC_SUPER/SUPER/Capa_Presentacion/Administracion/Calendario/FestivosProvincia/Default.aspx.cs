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
    double nTotalHoras = 0;
    int nAnno;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.

            Master.sbotonesOpcionOn = "4,29,31";
            Master.sbotonesOpcionOff = "4,29";

            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            //Poniendo el siguiente atributo a true, se incluye el fichero javascript propio
            //de la carpeta hija "Functions/funciones.js"
            Master.bFuncionesLocales = true;
            Master.FicherosCSS.Add("Capa_Presentacion/Administracion/Calendario/FestivosProvincia/Calendario.css");
            Master.TituloPagina = "Desglose horario del calendario festivo de la provincia seleccionada";
            cargarComboPaises();
            cargarComboProvinciasPais(66);

            if (!Page.IsPostBack)
            {
                try
                {
                    this.txtAnno.Text = DateTime.Now.Year.ToString();
                    this.nAnno = int.Parse(this.txtAnno.Text);
                    MostrarCalendario(int.Parse(DateTime.Now.Year.ToString()));
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener el desglose horario de festivos por provincia. ", ex);
                }

            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    private void MostrarCalendario(int iAno)
    {
        //this.nAnno = int.Parse(this.txtAnno.Text);

        Table objTabla = (Table)this.tblContenedor.FindControl("tblCalendarios");
        TableRow objFila;
        TableCell objCelda;

        //Calendario objCal = Calendario.Obtener(5);
        Calendario objCal = new Calendario(0,"","",0,0,1,1,1,1,1,1,1,"",null,null,null,"",null,null);
        objCal.PintarDias(iAno);

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

    protected string Grabar(string sCodProvincia, string strDias, string sAno)
    {
        string sResul = "";

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
            // Eliminar todos los dias festivos de la provincia
            Calendario.EliminarFestivos(tr, int.Parse(sCodProvincia), int.Parse(sAno));

            //Insertamos los días festivos de la provincia
            string[] aDias = Regex.Split(strDias, "##");

            foreach (string oDia in aDias)
            {
                if (oDia == "") continue;
                // grabar
                DateTime objFecha = new DateTime(int.Parse(oDia.Substring(6, 4)), int.Parse(oDia.Substring(3, 2)), int.Parse(oDia.Substring(0, 2)));
                Calendario.InsertarFestivos(tr, int.Parse(sCodProvincia), objFecha);
            }
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar.", ex);
        }
        try
        {
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

    protected string cargarFestivos(string sCodProvincia, string sAno)
    {
        string sResul = "";
        int iAnoMax = DateTime.Now.Year;
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //Compruebo si hay festivos por provincia para ese año. Si hay, los cojo, sino cojo los del mayor año anterior al solicitado
            SqlDataReader dr1 = SUPER.Capa_Negocio.Calendario.GetAnoFestivosProvincia(int.Parse(sCodProvincia), int.Parse(sAno));
            if (dr1.Read())
            {
                iAnoMax = int.Parse(dr1["nAnoMax"].ToString());
            }
            dr1.Close();
            dr1.Dispose();

            SqlDataReader dr = SUPER.Capa_Negocio.Calendario.ObtenerFestivosProvincia(int.Parse(sCodProvincia), iAnoMax); //Mostrar todos todos las provincias relacionadas a un país determinado

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
		switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                string strUrl = HistorialNavegacion.Leer();
                try
                {
                    Response.Redirect(strUrl, true);
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
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("cargarFestivos"):
                sResultado += cargarFestivos(aArgs[1], aArgs[2]);
                break;
            case ("provinciasPais"):
                sResultado += cargarProvinciasPais(aArgs[1]);
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
    public void cargarComboPaises()
    {
        cboPais.DataSource = SUPER.DAL.PAIS.Catalogo();
        cboPais.DataValueField = "identificador";
        cboPais.DataTextField = "denominacion";
        cboPais.DataBind();
        cboPais.SelectedValue = "66"; // Por defecto España
    }
    private void cargarComboProvinciasPais(int iID)
    {
        cboProvincia.DataValueField = "identificador";
        cboProvincia.DataTextField = "denominacion";
        cboProvincia.DataSource = SUPER.DAL.PAIS.Provincias(iID);
        cboProvincia.DataBind();
        cboProvincia.Items.Insert(0, new ListItem("", ""));
    }
    private string cargarProvinciasPais(string sID)
    {
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader dr = SUPER.DAL.PAIS.Provincias(int.Parse(sID)); //Mostrar todos todos las provincias relacionadas a un país determinado

            while (dr.Read())
            {
                sb.Append(dr["identificador"].ToString() + "##" + dr["denominacion"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "N@#@" + Errores.mostrarError("Error al obtener las provincias fiscales de un determinado país", ex);
        }
    }
    protected void imgAnterior_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        this.nAnno = int.Parse(this.txtAnno.Text) - 1;
        this.txtAnno.Text = this.nAnno.ToString();
        MostrarCalendario(int.Parse(this.txtAnno.Text));
    }

    protected void imgSiguiente_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        this.nAnno = int.Parse(this.txtAnno.Text) + 1;
        this.txtAnno.Text = this.nAnno.ToString();
        MostrarCalendario(int.Parse(this.txtAnno.Text));
    }
}
