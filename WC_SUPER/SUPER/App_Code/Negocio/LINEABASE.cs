using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SUPER.Capa_Datos;
using System.Collections;
using System.Text.RegularExpressions;
using SUPER.BLL;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : LINEABASE
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T685_LINEABASE
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/02/2012 9:58:56	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class LINEABASE
	{
		#region Propiedades y Atributos

		private int _t685_idlineabase;
		public int t685_idlineabase
		{
			get {return _t685_idlineabase;}
			set { _t685_idlineabase = value ;}
		}

		private string _t685_denominacion;
		public string t685_denominacion
		{
			get {return _t685_denominacion;}
			set { _t685_denominacion = value ;}
		}

		private DateTime _t685_fecha;
		public DateTime t685_fecha
		{
			get {return _t685_fecha;}
			set { _t685_fecha = value ;}
		}

		private int _t314_idusuario_promotor;
		public int t314_idusuario_promotor
		{
			get {return _t314_idusuario_promotor;}
			set { _t314_idusuario_promotor = value ;}
		}

        private string _Promotor;
        public string Promotor
        {
            get { return _Promotor; }
            set { _Promotor = value; }
        }

        private int _t305_idproyectosubnodo;
        public int t305_idproyectosubnodo
        {
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
        }

        private int _numero_lb;
        public int numero_lb
        {
            get { return _numero_lb; }
            set { _numero_lb = value; }
        }

        private int _count_lb;
        public int count_lb
        {
            get { return _count_lb; }
            set { _count_lb = value; }
        }

		#endregion

		#region Constructor

		public LINEABASE() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

        public static LINEABASE Obtener(int t685_idlineabase)
        {
            return Capa_Datos.LINEABASE.Obtener(null, t685_idlineabase);
        }

        public static int ObtenerUltimaLineaBase(int t305_idproyectosubnodo)
        {
            return Capa_Datos.LINEABASE.ObtenerUltimaLineaBase(null, t305_idproyectosubnodo);
        }
        public static int ObtenerMesDefecto_old(int t685_idlineabase)
        {
            int nMesActual = Fechas.FechaAAnnomes(DateTime.Today);
            int nMesMinimo = Fechas.FechaAAnnomes(DateTime.Today);
            int nMesMaximo = 0;// Fechas.FechaAAnnomes(DateTime.Today);

            SqlDataReader dr = Capa_Datos.LINEABASE.ObtenerMeses(null, t685_idlineabase);
            while (dr.Read())
            {
                if ((int)dr["anomes"] < nMesMinimo) nMesMinimo = (int)dr["anomes"];
                if ((int)dr["anomes"] > nMesMaximo) nMesMaximo = (int)dr["anomes"];
            }
            dr.Close();
            dr.Dispose();

            if (nMesMaximo == 0) nMesMaximo = nMesMinimo;

            if (nMesMaximo < nMesActual) return nMesMaximo;
            else if (nMesMinimo > nMesActual) return nMesMinimo;
            else return nMesActual;
        }

        public static int ObtenerMesDefecto(int t685_idlineabase)
        {
            int nMesMaximoC = 190001;// Fechas.FechaAAnnomes(DateTime.Today);

            SqlDataReader dr = Capa_Datos.LINEABASE.ObtenerMeses(null, t685_idlineabase);
            while (dr.Read())
            {
                if ((int)dr["anomes"] > nMesMaximoC) nMesMaximoC = (int)dr["anomes"];
            }
            dr.Close();
            dr.Dispose();

            return nMesMaximoC;
        }
        public static DataSet ObtenerDatosValorGanado(int t685_idlineabase, int t325_anomes_referencia, string t422_idmoneda)
        {
            return Capa_Datos.LINEABASE.ObtenerDatosValorGanado(null, t685_idlineabase, t325_anomes_referencia, t422_idmoneda);
        }
        public static string ObtenerCatalogoByPSN(int t305_idproyectosubnodo)
        {
            StringBuilder sb = new StringBuilder();

            #region Cabecera tabla HTML
            sb.Append("<table id='tblDatos' class='texto MA' border='0' style='width:660px; table-layout:fixed; border-collapse:collapse;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:300px;' />");
            sb.Append("    <col style='width:60px;' />");
            sb.Append("    <col style='width:300px;' />");
            sb.Append("</colgroup>");
            #endregion

            SqlDataReader dr = SUPER.Capa_Datos.LINEABASE.CatalogoByPSN(null, t305_idproyectosubnodo);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t685_idlineabase"].ToString() + "' style='height:20px;' ondblclick='aceptarClick(this.id)'>");

                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W290' onmouseover='TTip(event)'>" + dr["t685_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + ((DateTime)dr["t685_fecha"]).ToShortDateString() + "</td>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W290' onmouseover='TTip(event)'>" + dr["Promotor"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");

            return sb.ToString();
        }
        public static string ObtenerCatalogoByPSNCreacionLB(int t305_idproyectosubnodo)
        {
            StringBuilder sb = new StringBuilder();

            #region Cabecera tabla HTML
            sb.Append("<table id='tblLineas' style='width:530px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:260px;' />");
            sb.Append("    <col style='width:70px;' />");
            sb.Append("    <col style='width:200px;' />");
            sb.Append("</colgroup>");
            #endregion

            SqlDataReader dr = SUPER.Capa_Datos.LINEABASE.CatalogoByPSN(null, t305_idproyectosubnodo);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t685_idlineabase"].ToString() + "'>");

                sb.Append("<td><nobr class='NBR W210' onmouseover='TTip(event)'>" + dr["t685_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + ((DateTime)dr["t685_fecha"]).ToShortDateString() + "</td>");
                sb.Append("<td><nobr class='NBR W190' onmouseover='TTip(event)'>" + dr["Promotor"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");

            return sb.ToString();
        }

        public static int CrearLineaBase(int t305_idproyectosubnodo, int t314_idusuario, string t685_denominacion, int t001_idficepi, string t422_idmoneda)
        {
            string sDatosLBAnterior = "";
            string sDatosLBNueva = "";
            string sObservaciones = "";
            bool bExisteAnterior = false;

            //1º Obtener la última línea base (si existe) para obtener los datos necesarios para la creación de la observación.
            int nLineaBase = LINEABASE.ObtenerUltimaLineaBase(t305_idproyectosubnodo);
            if (nLineaBase > 0)
            {
                bExisteAnterior = true;
                DataSet ds = LINEABASE.ObtenerDatosValorGanado(nLineaBase, Fechas.FechaAAnnomes(DateTime.Now), t422_idmoneda);
                sDatosLBAnterior = "Coste Total Planificado (BAC) de línea base anterior = " + decimal.Parse(ds.Tables[0].Rows[0]["BAC"].ToString()).ToString("N") + " " + t422_idmoneda + (char)10 + (char)13; // "<br>";//  <NNN.NNN€>";
                sDatosLBAnterior += "Duración de proyecto en línea base anterior: " + ds.Tables[0].Rows.Count.ToString() + " meses, finalizando en " + Fechas.AnnomesAFechaDescLarga((int)ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["anomes"]) + ".";
            }

            //2º Crear la nueva línea base.
            int nIDLB = Capa_Datos.LINEABASE.Insertar(null, Utilidades.unescape(t685_denominacion), t314_idusuario, t305_idproyectosubnodo);
            DataSet ds1 = LINEABASE.ObtenerDatosValorGanado(nIDLB, Fechas.FechaAAnnomes(DateTime.Now), t422_idmoneda);
            sDatosLBNueva = "Creada línea base "+ (char)34 + "{{nombre}}" + (char)34 +@"." + (char)10 + (char)13; // <br>";
            sDatosLBNueva += "Coste Total Planificado (BAC) = " + decimal.Parse(ds1.Tables[0].Rows[0]["BAC"].ToString()).ToString("N") + " " + t422_idmoneda + (char)10 + (char)13; //"<br>";//  <NNN.NNN€>";
            sDatosLBNueva += "Duración de proyecto en línea base: " + ds1.Tables[0].Rows.Count.ToString() + " meses, finalizando en " + Fechas.AnnomesAFechaDescLarga((int)ds1.Tables[0].Rows[ds1.Tables[0].Rows.Count - 1]["anomes"]) + ".";

            sObservaciones = sDatosLBNueva;
            if (bExisteAnterior)
            {
                //Nota: poniendo: sObservaciones += (char)10 + (char)13... añade dos caracteres "23" antes del salto de línea.
                sObservaciones = sObservaciones + (char)10 + (char)13 + " " + (char)10 + (char)13 + " " + (char)10 + (char)13 + sDatosLBAnterior;
            }

            byte[] data = Encoding.Default.GetBytes(sObservaciones);
            sObservaciones = Encoding.UTF8.GetString(data).Replace("{{nombre}}", Utilidades.unescape(t685_denominacion));
            //3º Creación de la observación automática.
            SUPER.BLL.OBSERVACIONES_LB.Insertar(t305_idproyectosubnodo, t001_idficepi, true, sObservaciones);

            return nIDLB;
        }
        public static int EliminarLineaBase(int t685_idlineabase, int t001_idficepi, string t422_idmoneda)
        {
            string sObservaciones = "";

            //1º Obtener los datos de la línea base para obtener los datos necesarios para la creación de la observación.
            LINEABASE oLB = LINEABASE.Obtener(t685_idlineabase);
            sObservaciones = "Eliminada l&iacute;nea base \"" + oLB.t685_denominacion + "\" que hab&iacute;a sido creada el " + oLB.t685_fecha.ToShortDateString() + ", con Coste Total Planificado (BAC) = ";

            DataSet ds = LINEABASE.ObtenerDatosValorGanado(t685_idlineabase, Fechas.FechaAAnnomes(DateTime.Now), t422_idmoneda);
            sObservaciones += decimal.Parse(ds.Tables[0].Rows[0]["BAC"].ToString()).ToString("N") + " " + t422_idmoneda;

            //2º Creación de la observación automática.
            SUPER.BLL.OBSERVACIONES_LB.Insertar(oLB.t305_idproyectosubnodo, t001_idficepi, true, sObservaciones);

            //3º Eliminar la línea base
            return Capa_Datos.LINEABASE.Eliminar(null, t685_idlineabase);
        }

        public static string ObtenerDesgloseLB(int t685_idlineabase, string t422_idmoneda)
        {
                StringBuilder sbTM = new StringBuilder();
                StringBuilder sbTMF1 = new StringBuilder();
                StringBuilder sbTMF2 = new StringBuilder();
                StringBuilder sbBF = new StringBuilder();
                StringBuilder sbBM = new StringBuilder();
                Hashtable htDatosMes = new Hashtable();

                bool bColgroupCreado = false;
                int nWidthBM = 0;
                //string sGrupo = "";
                DataSet ds = SUPER.Capa_Datos.LINEABASE.ObtenerDesgloseLB(null, t685_idlineabase, t422_idmoneda);
                foreach (DataRow oFila in ds.Tables[2].Rows) //tabla de datos
                {
                    htDatosMes.Add(oFila["anomes"].ToString()
                                      + "/" + oFila["idclase"].ToString(), new DATODESGLOSELB((int)oFila["anomes"],
                                                                                             (int)oFila["idclase"],
                                                                                             decimal.Parse(oFila["importe_lb"].ToString()),
                                                                                             decimal.Parse(oFila["importe_real"].ToString()))
                                                                                             );
                }

                int nConcepto = -1, nSubgrupo = -1, nGrupo = -1;//nClase = -1, 
                //string sHTML = "";
                foreach (DataRow oFila in ds.Tables[0].Rows)
                {
                    //sHTML = "";
                    if (!bColgroupCreado)
                    {
                        bColgroupCreado = true;

                        #region tblTituloMovil
                        nWidthBM = ds.Tables[1].Rows.Count * 150;
                        sbTM.Append("<table id='tblTituloMovil' style='width:" + nWidthBM.ToString() + "px;' cellpadding='0' cellspacing='0' border='0'>");
                        sbTM.Append("    <colgroup>");
                        #endregion

                        #region tblBodyFijo
                        sbBF.Append("<table id='tblBodyFijo' style='width:480px;' cellpadding='0' cellspacing='0' border='0'>");
                        sbBF.Append("<colgroup>");
                        sbBF.Append("   <col style='width:300px;' />");
                        sbBF.Append("   <col style='width:90px;' />");
                        sbBF.Append("   <col style='width:90px;' />");
                        sbBF.Append("</colgroup>");

                        #endregion

                        #region tblBodyMovil
                        sbBM.Append("<table id='tblBodyMovil' style='width:" + nWidthBM.ToString() + "px;' cellpadding='0' cellspacing='0' border='0'>");
                        sbBM.Append("    <colgroup>");
                        #endregion

                        #region Creacion Colgroups Móviles
                        sbTMF1.Append("<tr style='height:17px;'>");
                        sbTMF2.Append("<tr style='height:17px;'>");

                        foreach (DataRow oFilaMes in ds.Tables[1].Rows)//Tabla de meses
                        {
                            sbTM.Append("   <col style='width:75px;' />");
                            sbTM.Append("   <col style='width:75px;' />");

                            sbTMF1.Append("   <td colspan='2' style='width:150px;'>" + Fechas.AnnomesAFechaDescLarga((int)oFilaMes["anomes"]) + "</td>");

                            sbTMF2.Append("   <td style='width:75px;'>L&iacute;nea base</td>");
                            sbTMF2.Append("   <td style='width:75px;'>Real</td>");

                            sbBM.Append("   <col style='width:75px;' />");
                            sbBM.Append("   <col style='width:75px;' />");
                        }

                        sbTM.Append("</colgroup>");

                        sbTMF1.Append("</tr>");
                        sbTMF2.Append("</tr>");
                        sbTM.Append(sbTMF1.ToString());
                        sbTM.Append(sbTMF2.ToString());
                        sbTM.Append("</table>");

                        sbBM.Append("</colgroup>");

                        #endregion
                    }

                    #region tblBodyFijo
                    if (nGrupo != int.Parse(oFila["t326_idgrupoeco"].ToString()))
                    {
                        nGrupo = int.Parse(oFila["t326_idgrupoeco"].ToString());
                        nSubgrupo = int.Parse(oFila["t327_idsubgrupoeco"].ToString());
                        nConcepto = int.Parse(oFila["t328_idconceptoeco"].ToString());
                        string[] aTablas = LINEABASE.CrearFilaGrupo(oFila, ds.Tables[1]);
                        sbBF.Append(aTablas[0]);
                        sbBM.Append(aTablas[1]);
                    }else if (nSubgrupo != int.Parse(oFila["t327_idsubgrupoeco"].ToString())){
                        nSubgrupo = int.Parse(oFila["t327_idsubgrupoeco"].ToString());
                        nConcepto = int.Parse(oFila["t328_idconceptoeco"].ToString());
                        string[] aTablas = LINEABASE.CrearFilaSubgrupo(oFila, ds.Tables[1]);
                        sbBF.Append(aTablas[0]);
                        sbBM.Append(aTablas[1]);
                    }
                    else if (nConcepto != int.Parse(oFila["t328_idconceptoeco"].ToString()))
                    {
                        nConcepto = int.Parse(oFila["t328_idconceptoeco"].ToString());
                        string[] aTablas = LINEABASE.CrearFilaConcepto(oFila, ds.Tables[1]);
                        sbBF.Append(aTablas[0]);
                        sbBM.Append(aTablas[1]);
                    }

                    sbBF.Append("<tr style='height:20px;' ");
                    sbBF.Append("sTipo='CL' ");
                    sbBF.Append("idGrupo='" + oFila["t326_idgrupoeco"].ToString() + "' ");
                    sbBF.Append("idSubgrupo='" + oFila["t327_idsubgrupoeco"].ToString() + "' ");
                    sbBF.Append("idConcepto='" + oFila["t328_idconceptoeco"].ToString() + "' ");
                    sbBF.Append("idClase='" + oFila["idclase"].ToString() + "' ");
                    sbBF.Append(">");
                    sbBF.Append("<td style='text-align:left;'><nobr class='NBR W215' style='margin-left:75px;' onmouseover='TTip(event)'>" + oFila["t329_denominacion"].ToString() + "<nobr></td>");//
                    sbBF.Append("<td>" + (((decimal)oFila["importe_lb"]==0)?"": decimal.Parse(oFila["importe_lb"].ToString()).ToString("N")) + "</td>");
                    sbBF.Append("<td style='border-right: 2px solid #A6C3D2;'>" + (((decimal)oFila["importe_real"] == 0) ? "" : decimal.Parse(oFila["importe_real"].ToString()).ToString("N")) + "</td>");
                    sbBF.Append("</tr>");

                    #endregion

                    #region tblBodyMovil
                    //sbBM.Append("<tr style='height:20px;'>");
                    sbBM.Append("<tr style='height:20px;' ");
                    sbBM.Append("sTipo='CL' ");
                    sbBM.Append("idGrupo='" + oFila["t326_idgrupoeco"].ToString() + "' ");
                    sbBM.Append("idSubgrupo='" + oFila["t327_idsubgrupoeco"].ToString() + "' ");
                    sbBM.Append("idConcepto='" + oFila["t328_idconceptoeco"].ToString() + "' ");
                    sbBM.Append("idClase='" + oFila["idclase"].ToString() + "' ");
                    sbBM.Append(">");

                    foreach (DataRow oFilaMes in ds.Tables[1].Rows)
                    {
                        DATODESGLOSELB oDE = (DATODESGLOSELB)htDatosMes[oFilaMes["anomes"].ToString()
                                                                      + "/" + oFila["idclase"].ToString()];
                        if (oDE != null)
                        {
                            sbBM.Append("<td style='width:75px;'>" + ((oDE.importe_lb == 0) ? "" : oDE.importe_lb.ToString("N")) + "</td>");
                            sbBM.Append("<td style='width:75px;'>" + ((oDE.importe_real == 0) ? "" : oDE.importe_real.ToString("N")) + "</td>");
                        }
                        else
                        {
                            sbBM.Append("<td style='width:75px;'></td>");
                            sbBM.Append("<td style='width:75px;'></td>");
                        }
                    }

                    sbBM.Append("</tr>");
                    #endregion
                }

                sbBF.Append("</table>");
                sbBM.Append("</table>");

                return sbTM.ToString() + "{{septabla}}"
                    + "<div style=\"background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:480px; height:auto;\">" + sbBF.ToString() + "</div>" + "{{septabla}}"
                    + "<div style=\"background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:" + nWidthBM.ToString() + "px; height:auto;\">" + sbBM.ToString() + "</div>" + "{{septabla}}";
        }
        public static string ObtenerProyectosConLineaBase(int t314_idusuario,
            string t301_categoria,
            string t301_estado,
            string t305_cualidad,
            string sClientes,
            string sResponsables,
            string sNaturalezas,
            string sHorizontal,
            string sModeloContrato,
            string sContrato,
            string sIDEstructura,
            string sSectores,
            string sSegmentos,
            bool bComparacionLogica,
            string sCNP,
            string sCSN1P,
            string sCSN2P,
            string sCSN3P,
            string sCSN4P,
            string sPSN,
            string sModulo,
            bool bAdministrador
        )
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblDatos' class='MA' style='width:960px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:65px' />");
            sb.Append("<col style='width:355px' />");
            sb.Append("<col style='width:220px' />");
            sb.Append("<col style='width:230px' />");//
            sb.Append("<col style='width:30px' />");//
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = Capa_Datos.LINEABASE.ObtenerProyectosConLineaBase(t314_idusuario, t301_estado, t301_categoria, t305_cualidad,
                sClientes, sResponsables, sNaturalezas, sHorizontal, sModeloContrato, sContrato, sIDEstructura, sSectores, sSegmentos,
                bComparacionLogica, sCNP, sCSN1P, sCSN2P, sCSN3P, sCSN4P, sPSN, sModulo, bAdministrador);

            while (dr.Read())
            {
                sb.Append("<tr style='height:20px' id=\"");
                sb.Append(dr["t305_idproyectosubnodo"].ToString());
                sb.Append("///");
                sb.Append(dr["modo_lectura"].ToString());
                sb.Append("///");
                sb.Append(dr["rtpt"].ToString());
                sb.Append("///");
                sb.Append(dr["t422_idmoneda_proyecto"].ToString());
                sb.Append("\" ");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("moneda_proyecto='" + dr["t422_idmoneda_proyecto"].ToString() + "' ");
                sb.Append("responsable=\"" + Utilidades.escape(dr["responsable_proyecto"].ToString()) + "\" ");
                sb.Append(">");

                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right; padding-right:10px;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W350' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />&nbsp;&nbsp;Informaci&oacute;n] body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["responsable_proyecto"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdvg(this.parentNode.parentNode.id)'>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W210'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W220'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:right; margin-right:3px;'>" + dr["numero_lb"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string ObtenerProyectosConLineaBaseAvan(int t314_idusuario, string t301_categoria, string t301_estado, string t305_cualidad, string sClientes, string sResponsables,
                                                        string sNaturalezas, string sHorizontal, string sModeloContrato, string sContrato, string sIDEstructura,
                                                        string sSectores, string sSegmentos, bool bComparacionLogica, string sCNP, string sCSN1P, string sCSN2P, string sCSN3P, string sCSN4P,
                                                        string sPSN, string sModulo, bool bAdministrador, string sMesRef)
        {
            StringBuilder sb = new StringBuilder();
            double dCPI = 0, dSPI = 0;
            string sColorCPI = "", sColorSPI = "";

            sb.Append("<table id='tblDatos' class='MA' style='width:970px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:20px' />");//Seleccionar fila
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:60px' />");//Id Proyecto
            sb.Append("<col style='width:275px' />");//Proyecto
            sb.Append("<col style='width:165px' />");//Cliente
            sb.Append("<col style='width:160px' />");//CR
            sb.Append("<col style='width:30px' />");//Nº de lineas base
            sb.Append("<col style='width:40px' />");//CPI
            sb.Append("<col style='width:40px' />");//SPI
            sb.Append("<col style='width:40px' />");//%EV
            sb.Append("<col style='width:80px' />");//CV
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            //PENDIENTE DE HABLAR CON IÑIGO para ver qué hacer con el añomes. Mientras tanto cojo el último mes cerrado de la empresa
            int iAnoMes = 0;
            if (sMesRef == null) {
                PARAMETRIZACIONSUPER oPar = PARAMETRIZACIONSUPER.Select(null);
                iAnoMes = oPar.t725_ultcierreempresa_ECO;
            } else iAnoMes = int.Parse(sMesRef);                    

            SqlDataReader dr = Capa_Datos.LINEABASE.ObtenerProyectosConLineaBaseAvan(t314_idusuario, t301_estado, t301_categoria, t305_cualidad,
                sClientes, sResponsables, sNaturalezas, sHorizontal, sModeloContrato, sContrato, sIDEstructura, sSectores, sSegmentos,
                bComparacionLogica, sCNP, sCSN1P, sCSN2P, sCSN3P, sCSN4P, sPSN, sModulo, bAdministrador, iAnoMes, "EUR");

            while (dr.Read())
            {
                dCPI = Math.Round(double.Parse(dr["CPI"].ToString()), 2);
                dSPI = Math.Round(double.Parse(dr["SPI"].ToString()), 2);

                sb.Append("<tr style='height:20px' id=\"");
                sb.Append(dr["t305_idproyectosubnodo"].ToString());
                sb.Append("///");
                sb.Append(dr["modo_lectura"].ToString());
                sb.Append("///");
                sb.Append(dr["rtpt"].ToString());
                sb.Append("///");
                sb.Append(dr["t422_idmoneda_proyecto"].ToString());
                sb.Append("\" ");
                sb.Append(" cpi=" + dCPI);
                sb.Append(" spi=" + dSPI);
                sb.Append(" categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("moneda_proyecto='" + dr["t422_idmoneda_proyecto"].ToString() + "' ");
                sb.Append("responsable=\"" + Utilidades.escape(dr["responsable_proyecto"].ToString()) + "\" ");
                sb.Append(">");

                sb.Append("<td><input type='checkbox' class='check' style='cursor:pointer' /></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right; padding-right:10px;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W270' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />&nbsp;&nbsp;Informaci&oacute;n] body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["responsable_proyecto"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdvg(this.parentNode.parentNode.id)'>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W160'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W160'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:right; margin-right:3px;'>" + dr["numero_lb"].ToString() + "</td>");

                
                if (dCPI >= 0.95)
                    sColorCPI = "background-color:#00ff00;";//verde
                else
                {
                    if (dCPI < 0.9)
                        sColorCPI = "background-color:#F45C5C;";//rojo
                    else
                        sColorCPI = "background-color:yellow;";
                }
                if (dSPI >= 0.95)
                    sColorSPI = "background-color:#00ff00;";
                else
                {
                    if (dSPI < 0.9)
                        sColorSPI = "background-color:#F45C5C;";
                    else
                        sColorSPI = "background-color:yellow;";
                }

                sb.Append("<td style='text-align:right; margin-right:3px; "+ sColorCPI + "'>" + dCPI.ToString("#,##0.##") + "</td>");
                sb.Append("<td style='text-align:right; margin-right:3px;" + sColorSPI + "''>" + dSPI.ToString("#,##0.##") + "</td>");
                sb.Append("<td style='text-align:right; margin-right:3px;'>" + double.Parse(dr["avance_tecnico_teorico_a_mes"].ToString()).ToString("#,##0") + "</td>");
                sb.Append("<td style='text-align:right; margin-right:3px;'>" + double.Parse(dr["CV"].ToString()).ToString("#,###.##") + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        public static DataSet getDataSetParaExcel(int t685_idlineabase, 
            int t325_anomes_referencia, 
            int t305_idproyectosubnodo, 
            string t422_idmoneda,
            int nIAP,
            int nEXT,
            int nOCO,
            int nIAPCPI,
            int nEXTCPI,
            int nOCOCPI)
        {
            DataSet ds = LINEABASE.ObtenerDatosValorGanado(t685_idlineabase, t325_anomes_referencia, t422_idmoneda);
            ds.Tables[0].TableName = "DatosEconomicos";

            DataSet ds1 = SUPER.Capa_Datos.LINEABASE.ObtenerDS(null, t685_idlineabase);
            ds1.Tables[0].TableName = "DetalleLineaBase";
            ds.Tables.Add(ds1.Tables["DetalleLineaBase"].Copy());

            DataSet ds2 = SUPER.Capa_Datos.RECONOCIMIENTOLB.ObtenerDatosReconocimientoDS(null, t685_idlineabase, false, t422_idmoneda);
            ds2.Tables[0].TableName = "ReconocimientoLB";
            ds.Tables.Add(ds2.Tables["ReconocimientoLB"].Copy());

            DataSet ds3 = SUPER.Capa_Datos.LINEABASE.ObtenerDesgloseLB_XLS(null, t685_idlineabase, t422_idmoneda);
            ds3.Tables[0].TableName = "Desglose";
            ds.Tables.Add(ds3.Tables["Desglose"].Copy());

            DataSet ds4 = SUPER.DAL.OBSERVACIONES_LB.CatalogoDS(t305_idproyectosubnodo);
            ds4.Tables[0].TableName = "Observaciones";
            ds.Tables.Add(ds4.Tables["Observaciones"].Copy());

            ds1.Dispose();
            ds2.Dispose();
            ds3.Dispose();
            ds4.Dispose();

            return ds;
        }


        #endregion

        public static string[] CrearFilaGrupo(DataRow oFila, DataTable dtMeses)
        {
            string[] aResul = new string[2];
            StringBuilder sbBF = new StringBuilder();
            StringBuilder sbBM = new StringBuilder();

            aResul = CrearFila(oFila, dtMeses, "G");
            sbBF.Append(aResul[0]);
            sbBM.Append(aResul[1]);
            aResul = CrearFila(oFila, dtMeses, "S");
            sbBF.Append(aResul[0]);
            sbBM.Append(aResul[1]);
            aResul = CrearFila(oFila, dtMeses, "C");
            sbBF.Append(aResul[0]);
            sbBM.Append(aResul[1]);

            return new string[] { sbBF.ToString(), sbBM.ToString() };
        }
        public static string[] CrearFilaSubgrupo(DataRow oFila, DataTable dtMeses)
        {
            string[] aResul = new string[2];
            StringBuilder sbBF = new StringBuilder();
            StringBuilder sbBM = new StringBuilder();

            aResul = CrearFila(oFila, dtMeses, "S");
            sbBF.Append(aResul[0]);
            sbBM.Append(aResul[1]);
            aResul = CrearFila(oFila, dtMeses, "C");
            sbBF.Append(aResul[0]);
            sbBM.Append(aResul[1]);

            return new string[] { sbBF.ToString(), sbBM.ToString() };
        }
        public static string[] CrearFilaConcepto(DataRow oFila, DataTable dtMeses)
        {
            string[] aResul = new string[2];
            StringBuilder sbBF = new StringBuilder();
            StringBuilder sbBM = new StringBuilder();

            aResul = CrearFila(oFila, dtMeses, "C");
            sbBF.Append(aResul[0]);
            sbBM.Append(aResul[1]);

            return new string[] { sbBF.ToString(), sbBM.ToString() };
        }
        public static string[] CrearFila(DataRow oFila, DataTable dtMeses, string sTipo)
        {
            StringBuilder sbBF = new StringBuilder();
            StringBuilder sbBM = new StringBuilder();

            sbBF.Append("<tr style='height:20px;'");
            sbBF.Append("sTipo='" + sTipo + "' ");
            sbBF.Append("idGrupo='" + oFila["t326_idgrupoeco"].ToString() + "' ");
            sbBF.Append("idSubgrupo='" + oFila["t327_idsubgrupoeco"].ToString() + "' ");
            sbBF.Append("idConcepto='" + oFila["t328_idconceptoeco"].ToString() + "' ");
            sbBF.Append("idClase='" + oFila["idclase"].ToString() + "' ");
            sbBF.Append(">");
            switch (sTipo)
            {
                case "G": sbBF.Append("<td style='text-align:left;'><nobr class='NBR W290' onmouseover='TTip(event)'>" + oFila["t326_denominacion"].ToString() + "<nobr></td>"); break;
                case "S": sbBF.Append("<td style='text-align:left;'><nobr class='NBR W265' style='margin-left:25px;' onmouseover='TTip(event)'>" + oFila["t327_denominacion"].ToString() + "<nobr></td>"); break;
                case "C": sbBF.Append("<td style='text-align:left;'><nobr class='NBR W240' style='margin-left:50px;' onmouseover='TTip(event)'>" + oFila["t328_denominacion"].ToString() + "<nobr></td>"); break;
            }

            //sbBF.Append("<td>" + decimal.Parse(oFila["importe_lb"].ToString()).ToString("N") + "</td>");
            //sbBF.Append("<td>" + decimal.Parse(oFila["importe_real"].ToString()).ToString("N") + "</td>");
            sbBF.Append("<td></td>");
            sbBF.Append("<td style='border-right: 2px solid #A6C3D2;'></td>");
            sbBF.Append("</tr>");

            //sbBM.Append("<tr style='height:20px;'>");
            sbBM.Append("<tr style='height:20px;'");
            sbBM.Append("sTipo='" + sTipo + "' ");
            sbBM.Append("idGrupo='" + oFila["t326_idgrupoeco"].ToString() + "' ");
            sbBM.Append("idSubgrupo='" + oFila["t327_idsubgrupoeco"].ToString() + "' ");
            sbBM.Append("idConcepto='" + oFila["t328_idconceptoeco"].ToString() + "' ");
            sbBM.Append("idClase='" + oFila["idclase"].ToString() + "' ");
            sbBM.Append(">");
            foreach (DataRow oFilaMes in dtMeses.Rows)
            {
                sbBM.Append("<td style='width:75px;'></td>");
                sbBM.Append("<td style='width:75px;'></td>");
            }
            sbBM.Append("</tr>");

            return new string[] {sbBF.ToString(), sbBM.ToString()};
        }
    }

    public partial class CONSISTENCIALB
    {
		#region Propiedades y Atributos

        private decimal _jornadas_PST;
        public decimal jornadas_PST
		{
            get { return _jornadas_PST; }
            set { _jornadas_PST = value; }
		}

        private decimal _jornadas_PGE;
        public decimal jornadas_PGE
        {
            get { return _jornadas_PGE; }
            set { _jornadas_PGE = value; }
        }

        private decimal _jornadas_ESTPGE;
        public decimal jornadas_ESTPGE
        {
            get { return _jornadas_ESTPGE; }
            set { _jornadas_ESTPGE = value; }
        }

        private decimal _jornadas_PGESJI;
        public decimal jornadas_PGESJI
        {
            get { return _jornadas_PGESJI; }
            set { _jornadas_PGESJI = value; }
        }

        private float _Indicador_Jornadas;
        public float Indicador_Jornadas
		{
            get { return _Indicador_Jornadas; }
            set { _Indicador_Jornadas = value; }
		}

        private int _num_tareas;
        public int num_tareas
		{
            get { return _num_tareas; }
            set { _num_tareas = value; }
		}

        private int _num_tareas_prevision;
        public int num_tareas_prevision
        {
            get { return _num_tareas_prevision; }
            set { _num_tareas_prevision = value; }
        }

        private float _Indicador_Tareas;
        public float Indicador_Tareas
        {
            get { return _Indicador_Tareas; }
            set { _Indicador_Tareas = value; }
        }

		#endregion

        public static CONSISTENCIALB Obtener(int t305_idproyectosubnodo)
        {
            Capa_Negocio.CONSISTENCIALB o = new Capa_Negocio.CONSISTENCIALB();

            SqlDataReader dr = Capa_Datos.CONSISTENCIALB.ObtenerAnalisis(null, t305_idproyectosubnodo);
            if (dr.Read())
            {
                if (dr["jornadas_PST"] != DBNull.Value)
                    o.jornadas_PST = decimal.Parse(dr["jornadas_PST"].ToString());
                if (dr["jornadas_PGE"] != DBNull.Value)
                    o.jornadas_PGE = decimal.Parse(dr["jornadas_PGE"].ToString());
                if (dr["jornadas_ESTPGE"] != DBNull.Value)
                    o.jornadas_ESTPGE = decimal.Parse(dr["jornadas_ESTPGE"].ToString());
                if (dr["jornadas_PGESJI"] != DBNull.Value)
                    o.jornadas_PGESJI = decimal.Parse(dr["jornadas_PGESJI"].ToString());
                if (dr["Indicador_Jornadas"] != DBNull.Value)
                    o.Indicador_Jornadas = float.Parse(dr["Indicador_Jornadas"].ToString());
                if (dr["num_tareas"] != DBNull.Value)
                    o.num_tareas = int.Parse(dr["num_tareas"].ToString());
                if (dr["num_tareas_prevision"] != DBNull.Value)
                    o.num_tareas_prevision = int.Parse(dr["num_tareas_prevision"].ToString());
                if (dr["Indicador_Tareas"] != DBNull.Value)
                    o.Indicador_Tareas = float.Parse(dr["Indicador_Tareas"].ToString());
            }
            dr.Close();
            dr.Dispose();

            return o;
        }
    }
    public class DATODESGLOSELB
    {
        #region Propiedades y Atributos
        private int _anomes;
        public int anomes
        {
            get { return _anomes; }
            set { _anomes = value; }
        }
        private int _idclase;
        public int idclase
        {
            get { return _idclase; }
            set { _idclase = value; }
        }
        private decimal _importe_lb;
        public decimal importe_lb
        {
            get { return _importe_lb; }
            set { _importe_lb = value; }
        }
        private decimal _importe_real;
        public decimal importe_real
        {
            get { return _importe_real; }
            set { _importe_real = value; }
        }

        #endregion

        public DATODESGLOSELB(int anomes,
            int idclase,
            decimal importe_lb,
            decimal importe_real)
        {
            this.anomes = anomes;
            this.idclase = idclase;
            this.importe_lb = importe_lb;
            this.importe_real = importe_real;
        }
    }

}
