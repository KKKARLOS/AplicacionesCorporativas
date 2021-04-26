using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;
using System.Text.RegularExpressions;
using IB.Progress.Shared;

public partial class Default : System.Web.UI.Page
{
    public string strControl = "";
    private void Page_Load(object sender, System.EventArgs e)
    {
      
    }

    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {
        InitializeComponent();
        base.OnInit(e);

        try
        {
            int int_pinicio;
            int int_pfin;
            string strServer;
            string strDataBase;
            string strUid;
            string strPwd;

            //IB.Progress.Models.Estadisticas rf = new IB.Progress.Models.Estadisticas();

//            string pf = "ctl00$CPHC$";
//				obtengo de la cadena de conexión los parámetros para luego
//				modificar localizaciones 

            string strconexion = IB.Progress.Shared.Database.GetConStr();
            int_pfin = strconexion.IndexOf(";database=", 0);
            strServer = strconexion.Substring(7, int_pfin - 7);

            int_pinicio = int_pfin + 10;
            int_pfin = strconexion.IndexOf(";uid=", int_pinicio);
            strDataBase = strconexion.Substring(int_pinicio, int_pfin - int_pinicio);

            int_pinicio = int_pfin + 5;
            int_pfin = strconexion.IndexOf(";pwd=", int_pinicio);
            strUid = strconexion.Substring(int_pinicio, int_pfin - int_pinicio);

            int_pinicio = int_pfin + 5;
            int_pfin = strconexion.IndexOf(";Trusted_Connection=", int_pinicio);
            strPwd = strconexion.Substring(int_pinicio, int_pfin - int_pinicio);

            //creo un objeto ReportDocument
            ReportDocument rdEstadisticas = new ReportDocument();
            
            int iOpcion = int.Parse(Request.QueryString["opcion"].ToString());
            int iPantalla = int.Parse(Request.QueryString["pantalla"].ToString());
            int iModelo = 0;

            try
            {
                if (iPantalla == 1)
                {
                    if ((iOpcion >= 1 && iOpcion <= 6)) rdEstadisticas.Load(Server.MapPath(".") + @"\estadisticas_modelo_1.rpt");
                    if ((iOpcion >= 7 && iOpcion <= 18)) rdEstadisticas.Load(Server.MapPath(".") + @"\estadisticas_modelo_2.rpt");
                }
                if (iPantalla == 2) 
                {
                    if (iOpcion == 1 || iOpcion == 3 || iOpcion == 5 || iOpcion == 6 || iOpcion == 7 || iOpcion == 9 || iOpcion == 10 || iOpcion == 11 || (iOpcion >= 13 && iOpcion <= 19 && iOpcion != 16))
                        rdEstadisticas.Load(Server.MapPath(".") + @"\estadisticas_modelo_2.rpt");
                    else if (iOpcion == 25 || iOpcion == 31) rdEstadisticas.Load(Server.MapPath(".") + @"\estadisticas_modelo_4.rpt");
                    else rdEstadisticas.Load(Server.MapPath(".") + @"\estadisticas_modelo_3.rpt");
                }
            }
            catch (Exception ex)
            {
                rdEstadisticas.Close();
                rdEstadisticas.Dispose();
                Response.Write("Error al abrir el report: " + ex.Message);
            }

            try
            {
                rdEstadisticas.SetDatabaseLogon(strUid, strPwd, strServer, strDataBase);
            }
            catch (Exception ex)
            {
                rdEstadisticas.Close();
                rdEstadisticas.Dispose();
                Response.Write("Error al logarse al report: " + ex.Message);
            }

            //creo un objeto logon .

            CrystalDecisions.Shared.TableLogOnInfo tliCurrent;
            try
            {
                foreach (CrystalDecisions.CrystalReports.Engine.Table tbCurrent in rdEstadisticas.Database.Tables)
                {

                    //obtengo el logon por tabla
                    tliCurrent = tbCurrent.LogOnInfo;

                    tliCurrent.ConnectionInfo.DatabaseName = strDataBase;
                    tliCurrent.ConnectionInfo.UserID = strUid;
                    tliCurrent.ConnectionInfo.Password = strPwd;
                    tliCurrent.ConnectionInfo.ServerName = strServer;

                    //aplico los cambios hechos al objeto TableLogonInfo
                    tbCurrent.ApplyLogOnInfo(tliCurrent);
                }
            }
            catch (Exception ex)
            {
                rdEstadisticas.Close();
                rdEstadisticas.Dispose();
                Response.Write("Error al actualizar la localización: " + ex.Message);
            }

            string strTipoFormato = "PDF"; // Request.QueryString[pf + "FORMATO"]; // ESTO LO RECOGERE COMO PARAMETRO
            strControl = strTipoFormato;

            DiskFileDestinationOptions diskOpts = ExportOptions.CreateDiskFileDestinationOptions();
            ExportOptions exportOpts = new ExportOptions();

            try
            {
                IB.Progress.Models.Estadisticas rf = new IB.Progress.Models.Estadisticas();
                IB.Progress.Models.OtrasEstadisticasRRHH  rf2  = new IB.Progress.Models.OtrasEstadisticasRRHH();

                if (iPantalla==1)
                    rf = (IB.Progress.Models.Estadisticas)Session["FiltrosInforme"];
                else
                    rf2 = (IB.Progress.Models.OtrasEstadisticasRRHH )Session["FiltrosInforme"];
                
                IB.Progress.Models.ParamsRPT rfParamsRpt = (IB.Progress.Models.ParamsRPT)Session["ParamsRPT"];

                string sTitulo = String.Empty;

                if (iPantalla == 1)
                {
                    switch (iOpcion)
	                {
                        case 1:
                            sTitulo = "Profesionales dependientes evaluados";
                            break;
                        case 2:
                            sTitulo = "Profesionales dependientes evaluados";
                            break;
                        case 3:
                            sTitulo = "Profesionales dependientes no evaluados";
                            break;
                        case 4:
                            sTitulo = "Profesionales dependientes no evaluados";
                            break;
                        case 5:
                            sTitulo = "Profesionales dependientes";
                            break;
                        case 6:
                            sTitulo = "Profesionales dependientes";
                            break;
                        case 7:
                            sTitulo = "Profesionales dependientes con evaluaciones abiertas";
                            break;
                        case 8:
                            sTitulo = "Profesionales dependientes con evaluaciones abiertas";
                            break;
                        case 9:
                            sTitulo = "Profesionales dependientes con evaluaciones en curso";
                            break;
                        case 10:
                            sTitulo = "Profesionales dependientes con evaluaciones en curso";
                            break;
                        case 11:
                            sTitulo = "Profesionales dependientes con evaluaciones cerradas";
                            break;
                        case 12:
                            sTitulo = "Profesionales dependientes con evaluaciones cerradas";
                            break;
                        case 13:
                            sTitulo = "Profesionales dependientes con evaluaciones cerradas y firmadas";
                            break;
                        case 14:
                            sTitulo = "Profesionales dependientes con evaluaciones cerradas y firmadas";
                            break;
                        case 15:
                            sTitulo = "Profesionales dependientes con evaluaciones cerradas sin firmar";
                            break;
                        case 16:
                            sTitulo = "Profesionales dependientes con evaluaciones cerradas sin firmar";
                            break;
                        case 17:
                            sTitulo = "Profesionales dependientes con evaluaciones";
                            break;
                        case 18:
                            sTitulo = "Profesionales dependientes con evaluaciones";
                            break;
	                }
                }
                else if (iPantalla == 2)
                {
                    switch (iOpcion)
                    {
                        case 1:
                            sTitulo = "Profesionales-evaluaciones con evaluaciones abiertas";
                            break;

                        case 2:
                            sTitulo = "Evaluadores con evaluaciones abiertas";
                            break;

                        case 3:
                            sTitulo = "Profesionales-evaluaciones con evaluaciones en curso";
                            break;

                        case 4:
                            sTitulo = "Evaluadores con evaluacioens en curso";
                            break;

                        case 5:
                            sTitulo = "Profesionales-evaluaciones con evaluaciones cerradas";
                            break;
                        case 6:
                            sTitulo = "Profesionales-evaluaciones con evaluaciones cerradas válidas";
                            break;

                        case 7:
                            sTitulo = "Profesionales-evaluaciones con evaluaciones cerradas no válidas";
                            break;

                        case 8:
                            sTitulo = "Evaluadores con evaluaciones cerradas";
                            break;

                        case 9:
                            sTitulo = "Profesionales-evaluacioenes con evaluaciones cerradas firmadas";
                            break;

                        case 10:
                            sTitulo = "Profesionales-evaluaciones con evaluaciones cerradas firmadas válidas";
                            break;

                        case 11:
                            sTitulo = "Profesionales-evaluaciones con evaluaciones cerradas firmadas no válidas";
                            break;

                        case 12:
                            sTitulo = "Evaluadores con evaluaciones cerradas firmadas";
                            break;

                        case 13:
                            sTitulo = "Profesionales-evaluaciones con evaluaciones cerradas sin firmar";
                            break;

                        case 14:
                            sTitulo = "Profesionales-evaluaciones con evaluaciones cerradas sin firmar válidas";
                            break;

                        case 15:
                            sTitulo = "Profesionales-evaluaciones con evaluaciones cerradas sin firmar no válidas";
                            break;

                        case 16:
                            sTitulo = "Evaluadores con evaluaciones cerradas sin firmar";
                            break;

                        case 17:
                            sTitulo = "Total de profesionales-evaluaciones";
                            break;

                        case 18:
                            sTitulo = "Total de profesionales-evaluaciones con evaluaciones válidas";
                            break;

                        case 19:
                            sTitulo = "Total de profesionales-evaluaciones con evaluaciones no válidas";
                            break;

                        case 20:
                            sTitulo = "Total de evaluadores con evaluaciones";
                            break;

                        case 21:
                            sTitulo = "Evaluadores sin confirmar equipo";
                            break;

                        case 22:
                            sTitulo = "Evaluadores con el equipo confirmado";
                            break;

                        case 23:
                            sTitulo = "Total evaluadores";
                            break;

                        case 24:
                            sTitulo = "Profesionales sin evaluador";
                            break;

                        case 25:
                            sTitulo = "Profesionales con evaluaciones abiertas";
                            break;

                        case 26:
                            sTitulo = "Profesionales con evaluaciones en curso";
                            break;

                        case 27:
                            sTitulo = "Profesionales con evaluaciones cerradas";
                            break;

                        case 28:
                            sTitulo = "Profesionales con evaluaciones cerradas firmadas";
                            break;

                        case 29:
                            sTitulo = "Profesionales con evaluaciones cerradas sin firmar";
                            break;

                        case 30:
                            sTitulo = "Total profesionales con evaluaciones";
                            break;

                        case 31:
                            sTitulo = "Total profesionales sin evaluación";
                            break;
                    }
                }

                if (iPantalla == 1)
                {
                    if (iOpcion >= 1 && iOpcion <= 6)  iModelo = 1;
                    if (iOpcion >= 7 && iOpcion <= 18) iModelo = 2;
                }
                if (iPantalla == 2)
                {
                    if (iOpcion == 1 || iOpcion == 3 || iOpcion == 5 || iOpcion == 6 || iOpcion == 7 || iOpcion == 9 || iOpcion == 10 || iOpcion == 11 || (iOpcion >= 13 && iOpcion <= 19 && iOpcion !=16)) iModelo = 2;
                    else if (iOpcion == 25 || iOpcion == 31) iModelo = 4;
                    else iModelo = 3;
                }


                if (iModelo == 1)
                {
                    //Parámetros Modelo 1
                    rdEstadisticas.SetParameterValue("@opcion", iOpcion);
                    rdEstadisticas.SetParameterValue("@t932_idfoto", (rf.t932_idfoto == null) ? null : rf.t932_idfoto);
                    rdEstadisticas.SetParameterValue("@desde", rf.Desde);
                    rdEstadisticas.SetParameterValue("@hasta", rf.Hasta);                   
                    rdEstadisticas.SetParameterValue("@t001_fecantigu", rf.t001_fecantigu);
                    //rdEstadisticas.SetParameterValue("@t001_fecantigu", (rf.t001_fecantigu == null) ? null : rf.t001_fecantigu);
                    rdEstadisticas.SetParameterValue("@profundidad", rf.Profundidad);
                    rdEstadisticas.SetParameterValue("@t001_idficepi", rf.t001_idficepi);
                    rdEstadisticas.SetParameterValue("@t941_idcolectivo", (rf.t941_idcolectivo == null) ? null : rf.t941_idcolectivo);

                    rdEstadisticas.SetParameterValue("Situacion", rfParamsRpt.txtSituacion.ToString());
                    rdEstadisticas.SetParameterValue("Sexo", (rfParamsRpt.sexo == null) ? "" : rfParamsRpt.sexo.ToString());
                    rdEstadisticas.SetParameterValue("Evaluador", rfParamsRpt.txtEvaluador.ToString());
                    rdEstadisticas.SetParameterValue("Profundizacion", rfParamsRpt.txtProfundizacion.ToString());
                    //rdEstadisticas.SetParameterValue("AntiguedadReferencia", rf.t001_fecantigu.ToString());
                    if (iOpcion == 1 || iOpcion == 3 || iOpcion == 5)
                        rdEstadisticas.SetParameterValue("AntiguedadReferencia", "");
                    else
                        rdEstadisticas.SetParameterValue("AntiguedadReferencia", rf.t001_fecantigu.ToShortDateString());

                    rdEstadisticas.SetParameterValue("Colectivo", rfParamsRpt.txtColectivo.ToString());
                }
                else if (iModelo == 2)
                {

                   rdEstadisticas.SetParameterValue("@pantalla", iPantalla);

                   if (iPantalla == 1)
                   {
                        //Parámetros Modelo 1
                        rdEstadisticas.SetParameterValue("@opcion1", iOpcion);
                        rdEstadisticas.SetParameterValue("@t932_idfoto", (rf.t932_idfoto == null) ? null : rf.t932_idfoto);
                        rdEstadisticas.SetParameterValue("@desde", rf.Desde);
                        rdEstadisticas.SetParameterValue("@hasta", rf.Hasta);
                        rdEstadisticas.SetParameterValue("@t001_fecantigu", rf.t001_fecantigu);
                        //rdEstadisticas.SetParameterValue("@t001_fecantigu", (rf.t001_fecantigu == null) ? null : rf.t001_fecantigu);
                        rdEstadisticas.SetParameterValue("@profundidad", rf.Profundidad);
                        rdEstadisticas.SetParameterValue("@t001_idficepi", rf.t001_idficepi);
                        rdEstadisticas.SetParameterValue("@t941_idcolectivo", (rf.t941_idcolectivo == null) ? null : rf.t941_idcolectivo);
                        //Parámetros Modelo 2
                        rdEstadisticas.SetParameterValue("@opcion2", null);
                        rdEstadisticas.SetParameterValue("@desde_evaluaciones", null);
                        rdEstadisticas.SetParameterValue("@hasta_evaluaciones", null);
                        rdEstadisticas.SetParameterValue("@t001_fecantigu_evaluaciones", null);
                        rdEstadisticas.SetParameterValue("@estadoprofesional_evaluaciones", null);
                        rdEstadisticas.SetParameterValue("@t941_idcolectivo_evaluaciones", null);
                        rdEstadisticas.SetParameterValue("@t930_denominacionCR_evaluaciones", null);
                        rdEstadisticas.SetParameterValue("@t303_idnodo_evaluadores", null);
                        rdEstadisticas.SetParameterValue("@desde_colectivos", null);
                        rdEstadisticas.SetParameterValue("@hasta_colectivos", null);
                        rdEstadisticas.SetParameterValue("@t001_fecantigu_colectivos", null);
                        rdEstadisticas.SetParameterValue("@t303_idnodo_colectivos", null);
                        rdEstadisticas.SetParameterValue("@t941_idcolectivo_colectivos", null);
                        rdEstadisticas.SetParameterValue("@t001_idficepievaluador_colectivos", null);

                        rdEstadisticas.SetParameterValue("Submodelo", 1);
                        rdEstadisticas.SetParameterValue("Estado", "");
                        rdEstadisticas.SetParameterValue("CR", "");

                        if (iOpcion == 7 || iOpcion == 9 || iOpcion == 11 || iOpcion == 13 || iOpcion == 15 || iOpcion == 17)
                            rdEstadisticas.SetParameterValue("AntiguedadReferencia", "");
                        else
                            rdEstadisticas.SetParameterValue("AntiguedadReferencia", rf.t001_fecantigu.ToShortDateString());
                    }
                    else if (iPantalla == 2)
                    {
                        //Parámetros Modelo 1
                        rdEstadisticas.SetParameterValue("@opcion1", null);
                        rdEstadisticas.SetParameterValue("@t932_idfoto", null);
                        rdEstadisticas.SetParameterValue("@desde", null);
                        rdEstadisticas.SetParameterValue("@hasta", null);
                        rdEstadisticas.SetParameterValue("@t001_fecantigu", null);
                        rdEstadisticas.SetParameterValue("@profundidad", null);
                        rdEstadisticas.SetParameterValue("@t001_idficepi", null);
                        rdEstadisticas.SetParameterValue("@t941_idcolectivo", null);

                        //Parámetros Modelo 2

                        rdEstadisticas.SetParameterValue("@opcion2", iOpcion);
                        rdEstadisticas.SetParameterValue("@desde_evaluaciones", rf2.Desde);
                        rdEstadisticas.SetParameterValue("@hasta_evaluaciones", rf2.Hasta);
                        rdEstadisticas.SetParameterValue("@t001_fecantigu_evaluaciones", rf2.t001_fecantigu);
                        //rdEstadisticas.SetParameterValue("@t001_fecantigu_evaluaciones", "20141103");
                        //rdEstadisticas.SetParameterValue("@t001_fecantigu_evaluaciones", (rf2.t001_fecantigu == null) ? null : rf.t001_fecantigu);

                        rdEstadisticas.SetParameterValue("@estadoprofesional_evaluaciones", (rf2.estado == null) ? null : rf2.estado.ToString());
                        rdEstadisticas.SetParameterValue("@t941_idcolectivo_evaluaciones", (rf2.t941_idcolectivo == null) ? null : rf2.t941_idcolectivo);

                        rdEstadisticas.SetParameterValue("@t930_denominacionCR_evaluaciones", (rf2.t930_denominacionCR=="")? null : rf2.t930_denominacionCR);
                        rdEstadisticas.SetParameterValue("@t303_idnodo_evaluadores", (rf2.T303_idnodo_evaluadores == null) ? null : rf2.T303_idnodo_evaluadores);
                        rdEstadisticas.SetParameterValue("@desde_colectivos", rf2.DesdeColectivos);
                        rdEstadisticas.SetParameterValue("@hasta_colectivos", rf2.HastaColectivos);
                        rdEstadisticas.SetParameterValue("@t001_fecantigu_colectivos", rf2.T001_fecantiguColectivos);
                        //rdEstadisticas.SetParameterValue("@t001_fecantigu_colectivos", "20150216");
                        //rdEstadisticas.SetParameterValue("@t001_fecantigu_colectivos", (rf2.T001_fecantiguColectivos == null) ? null : rf2.T001_fecantiguColectivos);

                        rdEstadisticas.SetParameterValue("@t303_idnodo_colectivos", (rf2.T303_idnodo_colectivos == null) ? null : rf2.T303_idnodo_colectivos);
                        rdEstadisticas.SetParameterValue("@t941_idcolectivo_colectivos", (rf2.T941_idcolectivo_colectivos == null) ? null : rf2.T941_idcolectivo_colectivos);
                        rdEstadisticas.SetParameterValue("@t001_idficepievaluador_colectivos", rf2.t001_idficepi);

                        rdEstadisticas.SetParameterValue("Submodelo", 2);
                        rdEstadisticas.SetParameterValue("Estado", rfParamsRpt.txtEstado.ToString());
                        rdEstadisticas.SetParameterValue("CR", rfParamsRpt.txtCR_Evaluaciones.ToString());
                        //rdEstadisticas.SetParameterValue("AntiguedadReferencia", rf2.t001_fecantigu);
                        rdEstadisticas.SetParameterValue("AntiguedadReferencia", rf2.t001_fecantigu.ToShortDateString());
                    }
                    rdEstadisticas.SetParameterValue("Situacion", rfParamsRpt.txtSituacion.ToString());
                    rdEstadisticas.SetParameterValue("Sexo", (rfParamsRpt.sexo == null) ? "" : rfParamsRpt.sexo.ToString());
                    rdEstadisticas.SetParameterValue("Evaluador", rfParamsRpt.txtEvaluador.ToString());
                    rdEstadisticas.SetParameterValue("Profundizacion", rfParamsRpt.txtProfundizacion.ToString());
                    rdEstadisticas.SetParameterValue("Colectivo", rfParamsRpt.txtColectivo.ToString());
                }
                else if (iModelo == 3)
                {
                    rdEstadisticas.SetParameterValue("@opcion", iOpcion);
                    rdEstadisticas.SetParameterValue("@desde_evaluaciones", rf2.Desde);
                    rdEstadisticas.SetParameterValue("@hasta_evaluaciones", rf2.Hasta);
                    rdEstadisticas.SetParameterValue("@t001_fecantigu_evaluaciones", rf2.t001_fecantigu);
                    //rdEstadisticas.SetParameterValue("@t001_fecantigu_evaluaciones", (rf2.t001_fecantigu == null) ? null : rf.t001_fecantigu);

                    rdEstadisticas.SetParameterValue("@estadoprofesional_evaluaciones", (rf2.estado == null) ? null : rf2.estado.ToString());
                    rdEstadisticas.SetParameterValue("@t941_idcolectivo_evaluaciones", rf2.t941_idcolectivo);
                    rdEstadisticas.SetParameterValue("@t930_denominacionCR_evaluaciones", (rf2.t930_denominacionCR == "") ? null : rf2.t930_denominacionCR);
                    rdEstadisticas.SetParameterValue("@t303_idnodo_evaluadores", (rf2.T303_idnodo_evaluadores == null) ? null : rf2.T303_idnodo_evaluadores);
                    rdEstadisticas.SetParameterValue("@desde_colectivos", rf2.DesdeColectivos);
                    rdEstadisticas.SetParameterValue("@hasta_colectivos", rf2.HastaColectivos);
                    rdEstadisticas.SetParameterValue("@t001_fecantigu_colectivos", rf2.T001_fecantiguColectivos);
                    rdEstadisticas.SetParameterValue("@t303_idnodo_colectivos", (rf2.T303_idnodo_colectivos == null) ? null : rf2.T303_idnodo_colectivos);
                    rdEstadisticas.SetParameterValue("@t941_idcolectivo_colectivos", (rf2.T941_idcolectivo_colectivos == null) ? null : rf2.T941_idcolectivo_colectivos);
                    rdEstadisticas.SetParameterValue("@t001_idficepievaluador_colectivos", rf2.t001_idficepi);



                    if (iOpcion == 2 || iOpcion == 4 || iOpcion == 8 || iOpcion == 12 || iOpcion == 16 || iOpcion == 20)
                        rdEstadisticas.SetParameterValue("Submodelo", 1);

                    if (iOpcion >= 21 && iOpcion <= 24)
                        rdEstadisticas.SetParameterValue("Submodelo", 2);

                    if (iOpcion >= 25 && iOpcion <= 31)
                        rdEstadisticas.SetParameterValue("Submodelo", 3);

                    rdEstadisticas.SetParameterValue("Sexo", (rfParamsRpt.sexo == null) ? "" : rfParamsRpt.sexo.ToString());
                    rdEstadisticas.SetParameterValue("Estado", rfParamsRpt.txtEstado.ToString());
                    rdEstadisticas.SetParameterValue("CR_Evaluaciones", rfParamsRpt.txtCR_Evaluaciones.ToString());
                    rdEstadisticas.SetParameterValue("CR_Evaluadores", rfParamsRpt.txtCR_Evaluadores.ToString());
                    rdEstadisticas.SetParameterValue("CR_Profesionales", rfParamsRpt.txtCR_Profesionales.ToString());
                    //rdEstadisticas.SetParameterValue("AntiguedadReferencia", rf2.t001_fecantigu.ToString());
                    rdEstadisticas.SetParameterValue("AntiguedadReferencia", rf2.t001_fecantigu.ToShortDateString());
                    rdEstadisticas.SetParameterValue("AntiguedadReferenciaColectivo", rf2.T001_fecantiguColectivos.ToString());
                    rdEstadisticas.SetParameterValue("Evaluador", rfParamsRpt.txtEvaluador);
                    rdEstadisticas.SetParameterValue("Colectivo", rfParamsRpt.txtColectivoProgress.ToString());
                }
                else if (iModelo == 4)
                {
                    rdEstadisticas.SetParameterValue("@opcion", iOpcion);
                    rdEstadisticas.SetParameterValue("@desde_evaluaciones", rf2.Desde);
                    rdEstadisticas.SetParameterValue("@hasta_evaluaciones", rf2.Hasta);
                    rdEstadisticas.SetParameterValue("@t001_fecantigu_evaluaciones", rf2.t001_fecantigu);
                    //rdEstadisticas.SetParameterValue("@t001_fecantigu_evaluaciones", (rf2.t001_fecantigu == null) ? null : rf.t001_fecantigu);

                    rdEstadisticas.SetParameterValue("@estadoprofesional_evaluaciones", (rf2.estado == null) ? null : rf2.estado.ToString());
                    rdEstadisticas.SetParameterValue("@t941_idcolectivo_evaluaciones", rf2.t941_idcolectivo);
                    rdEstadisticas.SetParameterValue("@t930_denominacionCR_evaluaciones", (rf2.t930_denominacionCR == "") ? null : rf2.t930_denominacionCR);
                    rdEstadisticas.SetParameterValue("@t303_idnodo_evaluadores", (rf2.T303_idnodo_evaluadores == null) ? null : rf2.T303_idnodo_evaluadores);
                    rdEstadisticas.SetParameterValue("@desde_colectivos", rf2.DesdeColectivos);
                    rdEstadisticas.SetParameterValue("@hasta_colectivos", rf2.HastaColectivos);
                    rdEstadisticas.SetParameterValue("@t001_fecantigu_colectivos", rf2.T001_fecantiguColectivos);
                    rdEstadisticas.SetParameterValue("@t303_idnodo_colectivos", (rf2.T303_idnodo_colectivos == null) ? null : rf2.T303_idnodo_colectivos);
                    rdEstadisticas.SetParameterValue("@t941_idcolectivo_colectivos", (rf2.T941_idcolectivo_colectivos == null) ? null : rf2.T941_idcolectivo_colectivos);
                    rdEstadisticas.SetParameterValue("@t001_idficepievaluador_colectivos", rf2.t001_idficepi);

                    //if (iOpcion == 25 || iOpcion == 31) rdEstadisticas.SetParameterValue("Submodelo", 4);
                    rdEstadisticas.SetParameterValue("Submodelo", 4);

                    rdEstadisticas.SetParameterValue("Sexo", (rfParamsRpt.sexo == null) ? "" : rfParamsRpt.sexo.ToString());
                    rdEstadisticas.SetParameterValue("Estado", rfParamsRpt.txtEstado.ToString());
                    rdEstadisticas.SetParameterValue("CR_Evaluaciones", rfParamsRpt.txtCR_Evaluaciones.ToString());
                    rdEstadisticas.SetParameterValue("CR_Evaluadores", rfParamsRpt.txtCR_Evaluadores.ToString());
                    rdEstadisticas.SetParameterValue("CR_Profesionales", rfParamsRpt.txtCR_Profesionales.ToString());
                    //rdEstadisticas.SetParameterValue("AntiguedadReferencia", rf2.t001_fecantigu.ToString());
                    rdEstadisticas.SetParameterValue("AntiguedadReferencia", rf2.t001_fecantigu.ToShortDateString());
                    rdEstadisticas.SetParameterValue("AntiguedadReferenciaColectivo", rf2.T001_fecantiguColectivos.ToString());
                    rdEstadisticas.SetParameterValue("Evaluador", rfParamsRpt.txtEvaluador);
                    rdEstadisticas.SetParameterValue("Colectivo", rfParamsRpt.txtColectivoProgress.ToString());
                }
                rdEstadisticas.SetParameterValue("Titulo", sTitulo);
                rdEstadisticas.SetParameterValue("FechaDesde", rfParamsRpt.fecDesde.ToString());
                rdEstadisticas.SetParameterValue("FechaHasta", rfParamsRpt.fecHasta.ToString());              
            }
            catch (Exception ex)
            {
                rdEstadisticas.Close();
                rdEstadisticas.Dispose();
                Response.Write("Error al actualizar los parámetros del report: " + ex.Message);
            }
            try
            {
                System.IO.Stream oStream;
                byte[] byteArray = null;

                switch (strTipoFormato)
                {
                    //			PDF			
                    case "PDF":
                        oStream = rdEstadisticas.ExportToStream(ExportFormatType.PortableDocFormat);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        break;
                    case "EXC":
                        oStream = rdEstadisticas.ExportToStream(ExportFormatType.Excel);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        break;
                }
                

                // FIN

                //Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                switch (strTipoFormato)
                {
                    //			PDF			
                    case "PDF":
                        Response.ContentType = "application/pdf";
                        break;
                    case "EXC":
                        //		EXCEL
                        Response.ContentType = "application/vnd.ms-excel";
                        //Response.ContentType = "Application/x-msexcel";

                        break;
                }
                Response.Clear();

                //String nav = HttpContext.Current.Request.Browser.Browser.ToString();
                //if (nav.IndexOf("IE") == -1)
                //{
                    switch (strTipoFormato)
                    {
                        case "PDF":
                            //Response.AddHeader
                            //("Content-Disposition", "attachment;filename=Filename.pdf");
                            break;
                        case "EXC":
                            Response.AddHeader
                            ("Content-Disposition", "attachment;filename=Filename.xls");
                            break;
                    }
                //}
                
                Response.BinaryWrite(byteArray);

                Response.Flush();
                Response.Close();
                //HttpContext.Current.Response.End();
                Response.End();
            }            
            catch (Exception ex)
            {
                rdEstadisticas.Close();
                rdEstadisticas.Dispose();
                Response.Write("Error al exportar el report: " + ex.Message);
            }  
        }
        //handle any exceptions
        catch (Exception ex)
        {
            Response.Write("No se puede crear el report: " + ex.Message);
        }
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.Load += new System.EventHandler(this.Page_Load);
    }
    #endregion
    ~Default()
    {
        // Simply call Dispose(false).
        this.Dispose();
    }
}
