using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web;
using SUPER.Capa_Datos;
using SUPER.Capa_Negocio;

namespace SUPER.BLL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : ESCENARIOSCAB
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T789_ESCENARIOSCAB
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	17/04/2012 12:48:57	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ESCENARIOSCAB
	{
        #region Propiedades y Atributos

        private int _t789_idescenario;
        public int t789_idescenario
        {
            get { return _t789_idescenario; }
            set { _t789_idescenario = value; }
        }

        private string _t789_denominacion;
        public string t789_denominacion
        {
            get { return _t789_denominacion; }
            set { _t789_denominacion = value; }
        }

        private int _t001_idficepi_creador;
        public int t001_idficepi_creador
        {
            get { return _t001_idficepi_creador; }
            set { _t001_idficepi_creador = value; }
        }

        private DateTime _t789_fcreacion;
        public DateTime t789_fcreacion
        {
            get { return _t789_fcreacion; }
            set { _t789_fcreacion = value; }
        }

        private int? _t305_idproyectosubnodo;
        public int? t305_idproyectosubnodo
        {
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
        }

        private int? _t306_idcontrato;
        public int? t306_idcontrato
        {
            get { return _t306_idcontrato; }
            set { _t306_idcontrato = value; }
        }

        private byte? _t316_idmodalidad;
        public byte? t316_idmodalidad
        {
            get { return _t316_idmodalidad; }
            set { _t316_idmodalidad = value; }
        }

        private string _t789_modelocoste;
        public string t789_modelocoste
        {
            get { return _t789_modelocoste; }
            set { _t789_modelocoste = value; }
        }

        private string _t789_modelotarif;
        public string t789_modelotarif
        {
            get { return _t789_modelotarif; }
            set { _t789_modelotarif = value; }
        }

        #endregion

        #region Propiedades y Atributos adicionales

        private string _Creador;
        public string Creador
        {
            get { return _Creador; }
            set { _Creador = value; }
        }

        private string _ResponsableProyecto;
        public string ResponsableProyecto
        {
            get { return _ResponsableProyecto; }
            set { _ResponsableProyecto = value; }
        }

        private int? _t301_idproyecto;
        public int? t301_idproyecto
        {
            get { return _t301_idproyecto; }
            set { _t301_idproyecto = value; }
        }

        private string _t301_denominacion;
        public string t301_denominacion
        {
            get { return _t301_denominacion; }
            set { _t301_denominacion = value; }
        }
        

        #endregion

        #region Constructor


        public ESCENARIOSCAB()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

		#region Metodos

        public static ESCENARIOSCAB Obtener(SqlTransaction tr, int t789_idescenario)
        {
            ESCENARIOSCAB o = new ESCENARIOSCAB();
            SqlDataReader dr = Capa_Datos.ESCENARIOSCAB.Obtener(tr, t789_idescenario);

            if (dr.Read())
            {
                if (dr["t789_idescenario"] != DBNull.Value)
                    o.t789_idescenario = int.Parse(dr["t789_idescenario"].ToString());
                if (dr["t789_denominacion"] != DBNull.Value)
                    o.t789_denominacion = (string)dr["t789_denominacion"];
                if (dr["t001_idficepi_creador"] != DBNull.Value)
                    o.t001_idficepi_creador = int.Parse(dr["t001_idficepi_creador"].ToString());
                if (dr["Creador"] != DBNull.Value)
                    o.Creador = (string)dr["Creador"];
                if (dr["t789_fcreacion"] != DBNull.Value)
                    o.t789_fcreacion = (DateTime)dr["t789_fcreacion"];
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
                if (dr["Responsable"] != DBNull.Value)
                    o.ResponsableProyecto = (string)dr["Responsable"];
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = int.Parse(dr["t301_idproyecto"].ToString());
                if (dr["t301_denominacion"] != DBNull.Value)
                    o.t301_denominacion = (string)dr["t301_denominacion"];
                if (dr["t306_idcontrato"] != DBNull.Value)
                    o.t306_idcontrato = int.Parse(dr["t306_idcontrato"].ToString());
                if (dr["t316_idmodalidad"] != DBNull.Value)
                    o.t316_idmodalidad = byte.Parse(dr["t316_idmodalidad"].ToString());
                if (dr["t789_modelocoste"] != DBNull.Value)
                    o.t789_modelocoste = (string)dr["t789_modelocoste"];
                if (dr["t789_modelotarif"] != DBNull.Value)
                    o.t789_modelotarif = (string)dr["t789_modelotarif"];

            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de ESCENARIOSCAB"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static string CrearEscenario(string t789_denominacion,
                                    int t001_idficepi_creador,
                                    Nullable<int> t305_idproyectosubnodo,
                                    Nullable<int> t306_idcontrato,
                                    Nullable<byte> t316_idmodalidad,
                                    string t789_modelocoste,
                                    string t789_modelotarif,
                                    int nDesde,
                                    int nHasta,
                                    string sPartidas)
        {
            string sResul = "";
            int nIDEscenario;

            SqlConnection oConn = null;
            SqlTransaction tr = null;
            #region Abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                return "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            }
            #endregion

            try
            {
                #region Datos Cabecera
                nIDEscenario = Capa_Datos.ESCENARIOSCAB.Insertar(tr, Utilidades.unescape(t789_denominacion),
                                                                t001_idficepi_creador,
                                                                t305_idproyectosubnodo,
                                                                t306_idcontrato,
                                                                t316_idmodalidad,
                                                                t789_modelocoste,
                                                                t789_modelotarif);

                Capa_Datos.ESCENARIOMES.InsertarMeses(tr, nIDEscenario, nDesde, nHasta);
                Capa_Datos.ESCENARIOSPAR.InsertarEnCreacionEscenario(tr, nIDEscenario, sPartidas);

                #endregion

                Conexion.CommitTransaccion(tr);
                sResul = "OK@#@" + nIDEscenario.ToString("#,###");
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = "Error@#@" + Errores.mostrarError("Error al grabar la cabecera del escenario.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
            return sResul;
        }

        public static string ObtenerPlanningEscenario(int t789_idescenario, string t422_idmoneda)
        {
            try
            {
                StringBuilder sbTM = new StringBuilder();
                StringBuilder sbTMF1 = new StringBuilder();
                StringBuilder sbTMF2 = new StringBuilder();
                StringBuilder sbBF = new StringBuilder();
                StringBuilder sbBM = new StringBuilder();
                StringBuilder sbPM = new StringBuilder();
                StringBuilder sbPMF1 = new StringBuilder();
                StringBuilder sbPMF2 = new StringBuilder();
                StringBuilder sbPMF3 = new StringBuilder();
                StringBuilder sbPMF4 = new StringBuilder();
                StringBuilder sbPMF5 = new StringBuilder();
                StringBuilder sbPMF6 = new StringBuilder();
                Hashtable htDatosMes = new Hashtable();

                bool bColgroupCreado = false;
                int nWidthBM = 0;
                string sGrupo = "";
                DataSet ds = SUPER.Capa_Datos.ESCENARIOSCAB.ObtenerDatosEscenario(null, t789_idescenario, t422_idmoneda);
                foreach (DataRow oFila in ds.Tables[2].Rows) //tabla de datos y comentarios
                {
                    htDatosMes.Add(oFila["t437_idpartidaeco"].ToString()
                                      + "/" + oFila["t795_anomes"].ToString()
                                      + "/" + oFila["codigo"].ToString()
                                      + "/" + oFila["codigo2"].ToString(), new DATOESCENARIO((int)oFila["t437_idpartidaeco"],
                                                                                             (int)oFila["t795_anomes"],
                                                                                             (int)oFila["codigo"],
                                                                                             (oFila["codigo2"].ToString()=="")?0:(int)oFila["codigo2"],
                                                                                             (int)oFila["iddato"],
                                                                                             double.Parse(oFila["unidades"].ToString()),
                                                                                             decimal.Parse(oFila["importe"].ToString()),
                                                                                             oFila["comentario"].ToString())
                                                                                             );
                }

                foreach (DataRow oFila in ds.Tables[0].Rows)
                {
                    if (!bColgroupCreado)
                    {
                        bColgroupCreado = true;

                        #region tblTituloMovil
                        nWidthBM = ds.Tables[1].Rows.Count * 130;
                        sbTM.Append("<table id='tblTituloMovil' style='width:" + nWidthBM.ToString() + "px;' cellpadding='0' cellspacing='0' border='0'>");
                        sbTM.Append("    <colgroup>");
                        #endregion

                        #region tblBodyFijo
                        sbBF.Append("<table id='tblBodyFijo' style='width:590px;' cellpadding='0' cellspacing='0' border='0'>");
                        sbBF.Append("<colgroup>");
                        sbBF.Append("   <col style='width:150px;' />");
                        sbBF.Append("   <col style='width:125px;' />");
                        sbBF.Append("   <col style='width:125px;' />");
                        sbBF.Append("   <col style='width:50px;' />");
                        sbBF.Append("   <col style='width:50px;' />");
                        sbBF.Append("   <col style='width:90px;' />");
                        sbBF.Append("</colgroup>");

                        #endregion

                        #region tblBodyMovil
                        sbBM.Append("<table id='tblBodyMovil' style='width:" + nWidthBM.ToString() + "px;' cellpadding='0' cellspacing='0' border='0'>");
                        sbBM.Append("    <colgroup>");
                        #endregion

                        #region tblPieMovil
                        sbPM.Append("<table id='tblPieMovil' style='width:" + nWidthBM.ToString() + "px;' cellpadding='0' cellspacing='0' border='0'>");
                        sbPM.Append("    <colgroup>");
                        #endregion

                        #region Creacion Colgroups Móviles
                        sbTMF1.Append("<tr style='height:17px;'>");
                        sbTMF2.Append("<tr style='height:17px;'>");

                        sbPMF1.Append("<tr class='TBLFIN' style='height:17px;'>");
                        sbPMF2.Append("<tr class='TBLFIN' style='height:17px;'>");
                        sbPMF3.Append("<tr class='TBLFIN' style='height:17px;'>");
                        sbPMF4.Append("<tr class='TBLFIN' style='height:17px;'>");
                        sbPMF5.Append("<tr class='TBLFIN' style='height:17px;'>");
                        sbPMF6.Append("<tr class='TBLFIN' style='height:17px;'>");

                        foreach (DataRow oFilaMes in ds.Tables[1].Rows)//Tabla de meses
                        {
                            sbTM.Append("   <col style='width:60px;' />");
                            sbTM.Append("   <col style='width:70px;' />");

                            sbTMF1.Append("   <td colspan='2' id='" + oFilaMes["t795_idescenariomes"].ToString() + "' ");
                            if (oFilaMes["t795_comentario"].ToString() != "")
                            {
                                sbTMF1.Append("class='comMes' ");
                                sbTMF1.Append("onmouseover='showTTE(\"" + Utilidades.escape(oFilaMes["t795_comentario"].ToString()) + "\",\"Comentario\")' onMouseout=\"hideTTE()\" ");
                            }
                            sbTMF1.Append("style='width:130px;' ");
                            sbTMF1.Append("comentario=\"" + Utilidades.escape(oFilaMes["t795_comentario"].ToString()) + "\" ");
                            sbTMF1.Append("estado='" + oFilaMes["estado_mes"].ToString() + "' ");
                            sbTMF1.Append("anomes='" + oFilaMes["t795_anomes"].ToString() + "'>" + Fechas.AnnomesAFechaDescLarga((int)oFilaMes["t795_anomes"]) + "</td>");

                            sbTMF2.Append("   <td style='width:60px;'>Unidades</td>");
                            sbTMF2.Append("   <td style='width:70px;'>Importe</td>");

                            sbBM.Append("   <col style='width:60px;' />");
                            sbBM.Append("   <col style='width:70px;' />");

                            sbPM.Append("   <col style='width:130px;' />");
                            sbPMF1.Append("   <td>0,00</td>");
                            sbPMF2.Append("   <td>0,00</td>");
                            sbPMF3.Append("   <td>0,00</td>");
                            sbPMF4.Append("   <td>0,00</td>");
                            sbPMF5.Append("   <td>0,00</td>");
                            sbPMF6.Append("   <td>0,00</td>");

                        }

                        sbTM.Append("</colgroup>");

                        sbTMF1.Append("</tr>");
                        sbTMF2.Append("</tr>");
                        sbTM.Append(sbTMF1.ToString());
                        sbTM.Append(sbTMF2.ToString());
                        sbTM.Append("</table>");

                        sbBM.Append("</colgroup>");

                        sbPMF1.Append("</tr>");
                        sbPMF2.Append("</tr>");
                        sbPMF3.Append("</tr>");
                        sbPMF4.Append("</tr>");
                        sbPMF5.Append("</tr>");
                        sbPMF6.Append("</tr>");
                        sbPM.Append(sbPMF1.ToString());
                        sbPM.Append(sbPMF2.ToString());
                        sbPM.Append(sbPMF3.ToString());
                        sbPM.Append(sbPMF4.ToString());
                        sbPM.Append(sbPMF5.ToString());
                        sbPM.Append(sbPMF6.ToString());
                        sbPM.Append("</colgroup>");

                        #endregion
                    }

                    #region tblBodyFijo
                    if (oFila["t326_denominacion"].ToString() != ""
                        && oFila["t326_denominacion"].ToString() != sGrupo)
                    {
                        sGrupo = oFila["t326_denominacion"].ToString();
                        sbBF.Append("<tr style='height:20px;' bd='' ");
                        sbBF.Append("tipo='' ");
                        sbBF.Append("idPartida='0' ");
                        sbBF.Append(">");
                        sbBF.Append("<td><nobr class='NBR W140' onmouseover='TTip(event)'>" + oFila["t326_denominacion"].ToString() + "<nobr></td>");//
                        //sbBF.Append("<td></td>");
                        sbBF.Append("<td colspan='2'></td>");
                        sbBF.Append("<td></td>");
                        sbBF.Append("<td></td>");
                        sbBF.Append("<td style='border-right: 2px solid #A6C3D2;'></td>");
                        sbBF.Append("</tr>");

                        sbBM.Append("<tr style='height:20px;' bd=''>");
                        foreach (DataRow oFilaMes in ds.Tables[1].Rows)
                        {
                            sbBM.Append("<td style='width:60px;'></td>");
                            sbBM.Append("<td style='width:70px;'></td>");
                        }
                        sbBM.Append("</tr>");
                    }

                    sbBF.Append("<tr style='height:20px;' bd='' ");
                    sbBF.Append("idPartida='" + oFila["t437_idpartidaeco"].ToString() + "' ");
                    sbBF.Append("codigo='" + oFila["codigo"].ToString() + "' ");
                    sbBF.Append("codigo2='" + oFila["codigo2"].ToString() + "' ");
                    sbBF.Append("necesidad='" + oFila["necesidad"].ToString() + "' ");
                    sbBF.Append("tipo='" + oFila["tipo"].ToString() + "' ");
                    sbBF.Append("coste_tarifa='" + oFila["coste_tarifa"].ToString() + "' ");

                    //if ((int)oFila["t437_idpartidaeco"] == -4)
                    //    sbBF.Append("idnodo='" + oFila["codigo2"].ToString() + "'");
                    //if ((int)oFila["t437_idpartidaeco"] == -5)
                    //    sbBF.Append("idproveedor='" + oFila["codigo2"].ToString() + "'");

                    sbBF.Append(">");
                    //sbTB.Append("<td>" + oFila["t326_denominacion"].ToString() + "</td>");
                    sbBF.Append("<td><nobr class='NBR W115' style='margin-left:25px;' onmouseover='TTip(event)'>" + oFila["t437_denominacion"].ToString() + "<nobr></td>");//
                    //if ((int)oFila["t437_idpartidaeco"] == -2
                    //    || (int)oFila["t437_idpartidaeco"] == -3
                    //    //|| (int)oFila["t437_idpartidaeco"] == -7
                    //    || (int)oFila["t437_idpartidaeco"] == -8)
                    //if ((int)oFila["orden_2"] == 0)
                    if (oFila["tipo"].ToString()=="P"
                        || (int)oFila["t437_idpartidaeco"] == -2
                        || (int)oFila["t437_idpartidaeco"] == -3
                        || (int)oFila["t437_idpartidaeco"] == -8)
                    {
                        if (oFila["tipo"].ToString() == "P")
                        {
                            sbBF.Append("<td colspan='2' style='text-align:right'>");
                            if ((int)oFila["t437_idpartidaeco"] == -2
                                || (int)oFila["t437_idpartidaeco"] == -3
                                || (int)oFila["t437_idpartidaeco"] == -7
                                || (int)oFila["t437_idpartidaeco"] == -8)
                            {
                                sbBF.Append("<img src='../../../../Images/imgAddDel.png' style='cursor:pointer;margin-left:3px;' onclick='addFila(this)' />");
                            }
                            else
                            {
                                sbBF.Append("<img src='../../../../Images/imgAdd.png' style='cursor:pointer;margin-left:3px;' onclick='addFila(this)' />");
                                sbBF.Append("<img src='../../../../Images/imgDel.png' style='cursor:pointer;margin-left:3px;margin-right:3px;' onclick='delFila(this)' />");
                            }
                            sbBF.Append("</td>");
                        }
                        else sbBF.Append("<td colspan='2'><nobr class='NBR W230' onmouseover='TTip(event)'>" + oFila["descripcion"].ToString() + "<nobr></td>");//
                    }
                    else if (oFila["necesidad"].ToString() == "")
                    {
                        sbBF.Append("<td colspan='2'><nobr class='NBR W230' onmouseover='TTip(event)' onClick='cctxt(this, 50)' style='cursor:pointer;' >" + oFila["motivo"].ToString() + "<nobr></td>");//
                    }
                    else
                    {
                        sbBF.Append("<td><nobr class='NBR W115' onmouseover='TTip(event)' ");
                        if ((int)oFila["t437_idpartidaeco"] != -7)
                        {
                            sbBF.Append("onClick='cctxt(this, 50)' style='cursor:pointer;' ");
                        }
                        sbBF.Append(">" + oFila["motivo"].ToString() + "<nobr></td>");//
                        sbBF.Append("<td><nobr class='NBR W115' onmouseover='TTip(event)'>" + oFila["descripcion"].ToString() + "<nobr></td>");//
                    }
                    sbBF.Append("<td class='num'>" + (((decimal)oFila["coste_tarifa"] == 0) ? "" : decimal.Parse(oFila["coste_tarifa"].ToString()).ToString("N")) + "</td>");
                    sbBF.Append("<td class='num'>" + (((double)oFila["num_unidades"] == 0) ? "" : double.Parse(oFila["num_unidades"].ToString()).ToString("N")) + "</td>");
                    sbBF.Append("<td class='num' style='border-right: 2px solid #A6C3D2;'>" + (((decimal)oFila["total_presupuesto"] == 0) ? "" : decimal.Parse(oFila["total_presupuesto"].ToString()).ToString("N")) + "</td>");
                    sbBF.Append("</tr>");

                    #endregion

                    #region tblBodyMovil
                    sbBM.Append("<tr style='height:20px;' ");
                    sbBM.Append("idPartida='" + oFila["t437_idpartidaeco"].ToString() + "' ");
                    sbBM.Append("codigo='" + oFila["codigo"].ToString() + "' >");

                    foreach (DataRow oFilaMes in ds.Tables[1].Rows)
                    {
                        DATOESCENARIO oDE = (DATOESCENARIO)htDatosMes[oFila["t437_idpartidaeco"].ToString()
                                                                      + "/" + oFilaMes["t795_anomes"].ToString()
                                                                      + "/" + oFila["codigo"].ToString()
                                                                      + "/" + oFila["codigo2"].ToString()];
                        if (oDE != null)
                        {
                            sbBM.Append("<td style='width:60px;' ");

                            if (oDE.PosicionCom == "I")
                            {
                                sbBM.Append("bd='' ");
                                sbBM.Append("iddato='" + oDE.dato.ToString() + "' ");
                                if ((bool)oFila["editable"])
                                {
                                    sbBM.Append("onDblClick='cc(this, 1)' onClick='sf(this)' ");
                                }

                                if (oDE.comentario != "")
                                {
                                    sbBM.Append("class='" + (((bool)oFila["editable"]) ? "MA " : "") + "comTD' ");
                                    sbBM.Append("comentario=\"" + Utilidades.escape(oDE.comentario) + "\" ");
                                    sbBM.Append("onmouseover='showTTE(\"" + Utilidades.escape(oDE.comentario) + "\",\"Comentario\")' onMouseout=\"hideTTE()\"");
                                }
                                else
                                {
                                    sbBM.Append("class='" + (((bool)oFila["editable"]) ? "MA " : "") + "' ");
                                    sbBM.Append("comentario=\"\" ");
                                }
                            }
                            sbBM.Append(">" + ((oDE.unidades == 0) ? "" : oDE.unidades.ToString("#,###.##")) + "</td>");
                            
                            sbBM.Append("<td style='width:70px;' ");
                            if (oDE.PosicionCom == "D")
                            {
                                sbBM.Append("bd='' ");
                                sbBM.Append("iddato='" + oDE.dato.ToString() + "' ");
                                if ((bool)oFila["editable"])
                                {
                                    sbBM.Append("onDblClick='cc(this, 1)' onClick='sf(this)' ");
                                }

                                if (oDE.comentario != "")
                                {
                                    sbBM.Append("class='" + (((bool)oFila["editable"]) ? "MA " : "") + "comTD' ");
                                    sbBM.Append("comentario=\"" + Utilidades.escape(oDE.comentario) + "\" ");
                                    sbBM.Append("onmouseover='showTTE(\"" + Utilidades.escape(oDE.comentario) + "\",\"Comentario\")' onMouseout=\"hideTTE()\"");
                                }
                                else
                                {
                                    sbBM.Append("class='" + (((bool)oFila["editable"]) ? "MA " : "") + "' ");
                                    sbBM.Append("comentario=\"\" ");
                                }
                            }

                            sbBM.Append(">" + ((oDE.importe == 0) ? "" : oDE.importe.ToString("N")) + "</td>");
                        }
                        else
                        {
                            sbBM.Append("<td style='width:60px;' ");
                            if ((int)oFila["t437_idpartidaeco"] == -2
                                    || (int)oFila["t437_idpartidaeco"] == -3
                                    || (int)oFila["t437_idpartidaeco"] == -7
                                    || (int)oFila["t437_idpartidaeco"] == -8
                                )
                            {
                                sbBM.Append("bd='' ");
                                sbBM.Append("iddato='' ");
                                if ((bool)oFila["editable"])
                                {
                                    sbBM.Append("onDblClick='cc(this, 1)' onClick='sf(this)' ");
                                }
                                sbBM.Append("class='" + (((bool)oFila["editable"]) ? "MA " : "") + "' ");
                                sbBM.Append("comentario=\"\" ");
                            }
                            sbBM.Append("></td>");

                            sbBM.Append("<td style='width:70px;' ");
                            if ((int)oFila["t437_idpartidaeco"] != -2
                                    && (int)oFila["t437_idpartidaeco"] != -3
                                    && (int)oFila["t437_idpartidaeco"] != -7
                                    && (int)oFila["t437_idpartidaeco"] != -8)
                            {
                                sbBM.Append("bd='' ");
                                sbBM.Append("iddato='' ");
                                if ((bool)oFila["editable"])
                                {
                                    sbBM.Append("onDblClick='cc(this, 1)' onClick='sf(this)' ");
                                }
                                sbBM.Append("class='" + (((bool)oFila["editable"]) ? "MA " : "") + "' ");
                                sbBM.Append("comentario=\"\" ");
                            }
                            sbBM.Append("></td>");
                        }
                    }

                    sbBM.Append("</tr>");
                    #endregion
                }

                sbBF.Append("</table>");
                sbBM.Append("</table>");
                sbPM.Append("</table>");

                return "OK@#@"
                    + sbTM.ToString() + "{{septabla}}"
                    + "<div style=\"background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:590px; height:auto;\">" + sbBF.ToString() + "</div>" + "{{septabla}}"
                    + "<div style=\"background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:" + nWidthBM.ToString() + "px; height:auto;\">" + sbBM.ToString() + "</div>" + "{{septabla}}"
                    + sbPM.ToString() + "{{septabla}}";
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al obtener el planning del escenario.", ex);
            }
        }

        public static string Grabar(int t789_idescenario, string sDatosMeses, string sDatosFijos, string sDatosMovil, string sIDsMesesBorrados)//, string strAnomes, string strPartidas, string strMotivos
        {
            string sResul = "";
            int nIDEM = -1;

            SqlConnection oConn = null;
            SqlTransaction tr = null;
            #region Abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                return "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            }
            #endregion

            try
            {
                if (sIDsMesesBorrados != "")
                    Capa_Datos.ESCENARIOMES.BorrarMesesEscenario(tr, sIDsMesesBorrados);

                #region 1º Asegurarnos que existen los meses, las partidas y los motivospartida
                Hashtable oHT_USUARIOS = Capa_Negocio.ESCENARIOSCAB_USUARIO.ObtenerUsuariosEscenario(tr, t789_idescenario);
                Hashtable oHT_MESES = Capa_Negocio.ESCENARIOMES.ObtenerCatalogoEscenario(tr, t789_idescenario);
                Hashtable oHT_PARTIDAS = Capa_Negocio.ESCENARIOSPAR.ObtenerCatalogoEscenario(tr, t789_idescenario);
                Hashtable oHT_MOTIVOS = Capa_Negocio.MOTIVOPARTIDA.ObtenerCatalogoEscenario(tr, t789_idescenario);
                #endregion

                #region 2º Meses
                if (sDatosMeses != "")
                {
                    string[] aDatosMeses = Regex.Split(sDatosMeses, "{sepreg}");
                    foreach (string oMes in aDatosMeses)
                    {
                        if (oMes == "") continue;
                        string[] aMes = Regex.Split(oMes, "{sepdato}");
                        ///aMes[0] = idescenariomes
                        ///aMes[1] = anomes
                        ///aMes[2] = comentario
                        if (oHT_MESES[int.Parse(aMes[1])] == null) //no existe el mes, hay que crearlo.
                        {
                            nIDEM = Capa_Datos.ESCENARIOMES.InsertarMes(tr, t789_idescenario, int.Parse(aMes[1]), Utilidades.unescape(aMes[2]));
                            oHT_MESES.Add(int.Parse(aMes[1]), new int[] { nIDEM, int.Parse(aMes[1]) });
                        }
                        else
                        {
                            int[] nDatos = ((int[])oHT_MESES[int.Parse(aMes[1])]);
                            nIDEM = nDatos[0];
                            Capa_Datos.ESCENARIOMES.ActualizarComentario(tr, nIDEM, Utilidades.unescape(aMes[2]));
                        }
                    }
                }
                #endregion

                #region 3º Partidas
                if (sDatosFijos != "")
                {
                    string[] aDatosFijos = Regex.Split(sDatosFijos, "{sepreg}");
                    foreach (string oPartida in aDatosFijos)
                    {
                        if (oPartida == "") continue;
                        string[] aPartida = Regex.Split(oPartida, "{sepdato}");
                        ///aPartida[0] = idPartida
                        ///aPartida[1] = tipo
                        ///aPartida[2] = idMotivoPartida
                        ///aPartida[3] = codigo2 (nodo o proveedor)
                        ///aPartida[4] = necesidad
                        ///aPartida[5] = motivo
                        
                        /* 1º Comprobamos si existe el motivopartida, si existe, lo actualizamos.
                         * Si no existe, buscamos el idescenariopartida correspondiente a la partida
                         y al escenario y creamos el motivopartida.*/
                        int nIDMP = -1;
                        int nIdEscenarioPar = -1;
                        if (oHT_MOTIVOS[int.Parse(aPartida[2])] == null)
                        {
                            //Para crear el motivopartida, necesitamos el idescenariopar, 
                            //que si no existe también hay que crearlo
                            if (oHT_PARTIDAS[int.Parse(aPartida[0])] == null)//no existe el escenariopartida
                            {
                                nIdEscenarioPar = Capa_Datos.ESCENARIOSPAR.InsertarPartida(tr, t789_idescenario, int.Parse(aPartida[0]));
                                oHT_PARTIDAS.Add(int.Parse(aPartida[0]), new int[] { nIdEscenarioPar, int.Parse(aPartida[0]) });
                            }
                            else
                            {
                                int[] nDatos = ((int[])oHT_PARTIDAS[int.Parse(aPartida[0])]);
                                nIdEscenarioPar = nDatos[0];
                            }
                            nIDMP = Capa_Datos.MOTIVOPARTIDA.InsertarMotivo(tr, nIdEscenarioPar,
                                (aPartida[4] == "N") ? (int?)int.Parse(aPartida[3]) : null,
                                (aPartida[4] == "P") ? (int?)int.Parse(aPartida[3]) : null,
                                Utilidades.unescape(aPartida[5]));
                            oHT_MOTIVOS.Add(nIDMP, nIDMP);
                        }
                        else
                        {
                            nIDMP = (int)oHT_MOTIVOS[int.Parse(aPartida[2])];
                            int[] nDatos2 = ((int[])oHT_PARTIDAS[int.Parse(aPartida[0])]);
                            nIdEscenarioPar = nDatos2[0];
                            Capa_Datos.MOTIVOPARTIDA.ActualizarMotivo(tr, nIDMP, nIdEscenarioPar,
                                (aPartida[3] == "") ? null : (int?)int.Parse(aPartida[3]),
                                (aPartida[4] == "") ? null : (int?)int.Parse(aPartida[4]),
                                Utilidades.unescape(aPartida[5]));
                        }
                    }
                }
                #endregion

                #region 4º Datos movil
                if (sDatosMovil != "")
                {
                    string[] aDatosMovil = Regex.Split(sDatosMovil, "{sepreg}");
                    foreach (string oDato in aDatosMovil)
                    {
                        if (oDato == "") continue;
                        string[] aDato = Regex.Split(oDato, "{sepdato}");
                        ///aDato[0] = idPartida
                        ///aDato[1] = codigo o motivopartida
                        ///aDato[2] = iddato
                        ///aDato[3] = anomes
                        ///aDato[4] = valor
                        ///aDato[5] = comentario
                        ///aDato[6] = coste_tarifa
                        ///aDato[7] = codigo2
                        ///aDato[8] = necesidad

                        int[] nDatos = ((int[])oHT_MESES[int.Parse(aDato[3])]);
                        nIDEM = nDatos[0];
                        if (aDato[4] == "") aDato[4] = "0";
                        switch (int.Parse(aDato[0]))
                        {
                            #region clases especiales
                            case -2: //Consumo de profesionales (T794)
                                if (double.Parse(aDato[4]) == 0 && aDato[5].Trim() == "" && aDato[2] != "")
                                    Capa_Datos.ESCENARIOCONSPERMES.Eliminar(tr, int.Parse(aDato[2]));
                                else
                                {
                                    //1º hay que obtener o crear el t731_idescenariousuario
                                    int nEscenarioUsuario = -1;
                                    if (oHT_USUARIOS[int.Parse(aDato[1])] == null)
                                    {
                                        nEscenarioUsuario = Capa_Datos.ESCENARIOSCAB_USUARIO.InsertarUsuario(tr, t789_idescenario,
                                            int.Parse(aDato[1]), DateTime.Now, null, null);
                                        oHT_USUARIOS.Add(int.Parse(aDato[1]), new int[] { nEscenarioUsuario, int.Parse(aDato[1]) });
                                    }
                                    else
                                    {
                                        nEscenarioUsuario = ((int[])oHT_USUARIOS[int.Parse(aDato[1])])[0];
                                    }

                                    Capa_Datos.ESCENARIOCONSPERMES.ActualizarInsertarUsuario(tr, (aDato[2] == "") ? null : (int?)int.Parse(aDato[2]),
                                        nIDEM, nEscenarioUsuario, (aDato[4] == "") ? 0 : double.Parse(aDato[4]),
                                        decimal.Parse(aDato[6]), Utilidades.unescape(aDato[5]));
                                }
                                break;
                            case -3: //Consumo por nivel (T796)
                                if (double.Parse(aDato[4]) == 0 && aDato[5].Trim() == "" && aDato[2] != "")
                                    Capa_Datos.ESCENARIOCONSNIVELMES.Eliminar(tr, int.Parse(aDato[2]));
                                else
                                    Capa_Datos.ESCENARIOCONSNIVELMES.ActualizarInsertarUsuario(tr, (aDato[2] == "") ? null : (int?)int.Parse(aDato[2]),
                                        nIDEM, int.Parse(aDato[1]), (aDato[4] == "") ? 0 : double.Parse(aDato[4]),
                                        Utilidades.unescape(aDato[5]));
                                break;
                            case -7: //Producción por profesional (T798)
                                if (double.Parse(aDato[4]) == 0 && aDato[5].Trim() == "" && aDato[2] != "")
                                    Capa_Datos.ESCENARIOPRODUCPROF.Eliminar(tr, int.Parse(aDato[2]));
                                else
                                    Capa_Datos.ESCENARIOPRODUCPROF.ActualizarInsertarUsuario(tr, (aDato[2] == "") ? null : (int?)int.Parse(aDato[2]),
                                        nIDEM, int.Parse(aDato[1]), int.Parse(aDato[7]), (aDato[4] == "") ? 0 : double.Parse(aDato[4]),
                                        Utilidades.unescape(aDato[5]));
                                break;
                            case -8: //Producción por perfil (T797)
                                if (double.Parse(aDato[4]) == 0 && aDato[5].Trim() == "" && aDato[2] != "")
                                    Capa_Datos.ESCENARIOPRODUCPERF.Eliminar(tr, int.Parse(aDato[2]));
                                else
                                    Capa_Datos.ESCENARIOPRODUCPERF.ActualizarInsertarUsuario(tr, (aDato[2] == "") ? null : (int?)int.Parse(aDato[2]),
                                        nIDEM, int.Parse(aDato[1]), (aDato[4] == "") ? 0 : double.Parse(aDato[4]),
                                        Utilidades.unescape(aDato[5]));
                                break;
                            #endregion
                            #region Escenariodatoeco
                            default:
                                if (double.Parse(aDato[4]) == 0 && aDato[5].Trim() == "" && aDato[2] != "")
                                    Capa_Datos.ESCENARIODATOECO.Eliminar(tr, int.Parse(aDato[2]));
                                else
                                {
                                    int nIDMP = -1;
                                    int nIdEscenarioPar = -1;
                                    if (oHT_MOTIVOS[int.Parse(aDato[1])] == null)
                                    {
                                        //Para crear el motivopartida, necesitamos el idescenariopar, 
                                        //que si no existe también hay que crearlo
                                        if (oHT_PARTIDAS[int.Parse(aDato[0])] == null)//no existe el escenariopartida
                                        {
                                            nIdEscenarioPar = Capa_Datos.ESCENARIOSPAR.InsertarPartida(tr, t789_idescenario, int.Parse(aDato[0]));
                                            oHT_PARTIDAS.Add(int.Parse(aDato[0]), new int[] { nIdEscenarioPar, int.Parse(aDato[0]) });
                                        }
                                        else
                                        {
                                            int[] nDatos2 = (int[])oHT_PARTIDAS[int.Parse(aDato[0])];
                                            nIdEscenarioPar = nDatos2[0];
                                        }
                                        nIDMP = Capa_Datos.MOTIVOPARTIDA.InsertarMotivo(tr, nIdEscenarioPar,
                                            (aDato[8] == "N") ? (int?)int.Parse(aDato[7]) : null,
                                            (aDato[8] == "P") ? (int?)int.Parse(aDato[7]) : null,
                                            Utilidades.unescape(aDato[5]));
                                        oHT_MOTIVOS.Add(nIDMP, nIDMP);
                                    }
                                    else
                                    {
                                        nIDMP = (int)oHT_MOTIVOS[int.Parse(aDato[1])];
                                    }

                                    Capa_Datos.ESCENARIODATOECO.ActualizarInsertarUsuario(tr, (aDato[2] == "") ? null : (int?)int.Parse(aDato[2]),
                                        nIDEM, nIDMP, (aDato[4] == "") ? 0 : decimal.Parse(aDato[4]),
                                        Utilidades.unescape(aDato[5]));
                                }
                                break;
                            #endregion
                        }
                    }
                }

                #endregion

                Conexion.CommitTransaccion(tr);
                sResul = "OK@#@";
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = "Error@#@" + Errores.mostrarError("Error al grabar el planning del escenario.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
            return sResul;
        }

		#endregion
	}

    public class DATOESCENARIO
    {
        #region Propiedades y Atributos
        private int _t437_idpartidaeco;
        public int t437_idpartidaeco
        {
            get { return _t437_idpartidaeco; }
            set { _t437_idpartidaeco = value; }
        }
        private int _t795_anomes;
        public int t795_anomes
        {
            get { return _t795_anomes; }
            set { _t795_anomes = value; }
        }
        private int _codigo;
        public int codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        private int _codigo2;
        public int codigo2
        {
            get { return _codigo2; }
            set { _codigo2 = value; }
        }
        private int _dato;
        public int dato
        {
            get { return _dato; }
            set { _dato = value; }
        }
        private double _unidades;
        public double unidades
        {
            get { return _unidades; }
            set { _unidades = value; }
        }
        private decimal _importe;
        public decimal importe
        {
            get { return _importe; }
            set { _importe = value; }
        }
        private string _comentario;
        public string comentario
        {
            get { return _comentario; }
            set { _comentario = value; }
        }

        private string _PosicionCom;
        public string PosicionCom
        {
            get { return _PosicionCom; }
            set { _PosicionCom = value; }
        }

        #endregion

        public DATOESCENARIO(int t437_idpartidaeco,
            int t795_anomes,
            int codigo,
            int codigo2,
            int dato,
            double unidades,
            decimal importe,
            string comentario)
        {
			this.t437_idpartidaeco = t437_idpartidaeco;
            this.t795_anomes = t795_anomes;
            this.codigo = codigo;
            this.codigo2 = codigo2;
            this.dato = dato;
            this.unidades = unidades;
            this.importe = importe;
            this.comentario = comentario;

            if (t437_idpartidaeco == -2
                    || t437_idpartidaeco == -3
                    || t437_idpartidaeco == -7
                    || t437_idpartidaeco == -8)
            {
                this.PosicionCom = "I";
            }
            else
            {
                this.PosicionCom = "D";
            }
		}

    }
}
