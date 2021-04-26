using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Collections.Generic;

namespace SUPER.DAL
{
    public partial class EXPPROFFICEPI
    {
        //public static SqlDataReader getProfesionales(SqlTransaction tr, int t301_idproyecto, int t808_idexpprof)
        //{
        //    SqlParameter[] aParam = new SqlParameter[2];
        //    int i = 0;
        //    aParam[i++] = ParametroSql.add("@t301_idproyecto", SqlDbType.Int, 4, t301_idproyecto);
        //    aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
        //    if (tr == null)
        //        return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROFFICEPI_C2", aParam);
        //    else
        //        return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROFFICEPI_C2", aParam);
        //}
        public static List<IB.SUPER.APP.Models.ExpProfFicepi> getProfesionales(SqlTransaction tr, int t808_idexpprof)
        {
            IB.SUPER.APP.Models.ExpProfFicepi oProf = null;
            List<IB.SUPER.APP.Models.ExpProfFicepi> lst = new List<IB.SUPER.APP.Models.ExpProfFicepi>();
            SqlDataReader dr = null;
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROFFICEPI_CAT", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROFFICEPI_CAT", aParam);
            while (dr.Read())
            {
                oProf = new IB.SUPER.APP.Models.ExpProfFicepi();
                oProf.t812_idexpprofficepi = Convert.ToInt32(dr["t812_idexpprofficepi"]);
                oProf.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                if (dr["t001_idficepi_validador"].ToString() != "")
                    oProf.t001_idficepi_validador = Convert.ToInt32(dr["t001_idficepi_validador"]);
                if (dr["t819_idplantillacvt"].ToString() != "")
                    oProf.idPlantilla = Convert.ToInt32(dr["t819_idplantillacvt"]);
                if (dr["dPrimerConsumo"].ToString() != "")
                    oProf.anomesPrimerConsumo = SUPER.Capa_Negocio.Fechas.FechaAAnnomes((DateTime)dr["dPrimerConsumo"]);
                if (dr["dUltimoConsumo"].ToString() != "")
                    oProf.anomesUltimoConsumo = SUPER.Capa_Negocio.Fechas.FechaAAnnomes((DateTime)dr["dUltimoConsumo"]);

                if (dr["t812_finicio"].ToString() != "")
                    oProf.finicio = Convert.ToDateTime(dr["t812_finicio"]);
                if (dr["t812_ffin"].ToString() != "")
                    oProf.ffin = Convert.ToDateTime(dr["t812_ffin"]);

                oProf.tipo = Convert.ToString(dr["tipo"]);
                oProf.sexo = Convert.ToString(dr["t001_sexo"]);
                oProf.profesional = Convert.ToString(dr["Profesional"]);
                oProf.denValidador = Convert.ToString(dr["denValidador"]);
                oProf.perfil = Convert.ToString(dr["t819_denominacion"]);
                oProf.oficina = Convert.ToString(dr["t010_desoficina"]);
                oProf.visibleCV = Convert.ToString(dr["t812_visiblecv"]);

                if (dr["baja"].ToString() == "0")
                    oProf.baja = false;
                else
                    oProf.baja = true;

                if (dr["esfuerzoenjor"].ToString() != "")
                    oProf.esfuerzoJornadas = Convert.ToDouble(dr["esfuerzoenjor"]);

                lst.Add(oProf);

            }
            dr.Close();

            return lst;

        }

        public static List<IB.SUPER.APP.Models.ExpProfFicepi> getProfesionales(SqlTransaction tr, int t301_idproyecto, int t808_idexpprof)
        {
            IB.SUPER.APP.Models.ExpProfFicepi oProf = null;
            List<IB.SUPER.APP.Models.ExpProfFicepi> lst = new List<IB.SUPER.APP.Models.ExpProfFicepi>();
            SqlDataReader dr = null;
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t301_idproyecto", SqlDbType.Int, 4, t301_idproyecto);
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
            if (tr == null)
                dr= SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROFFICEPI_C2", aParam);
            else
                dr= SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROFFICEPI_C2", aParam);
            while (dr.Read())
            {
                oProf = new IB.SUPER.APP.Models.ExpProfFicepi();
                oProf.t812_idexpprofficepi = Convert.ToInt32(dr["t812_idexpprofficepi"]);
                oProf.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                if (dr["t001_idficepi_validador"].ToString() != "")
                    oProf.t001_idficepi_validador = Convert.ToInt32(dr["t001_idficepi_validador"]);
                if (dr["t819_idplantillacvt"].ToString() != "")
                    oProf.idPlantilla = Convert.ToInt32(dr["t819_idplantillacvt"]);
                if (dr["dPrimerConsumo"].ToString() != "")
                    oProf.anomesPrimerConsumo = SUPER.Capa_Negocio.Fechas.FechaAAnnomes((DateTime)dr["dPrimerConsumo"]);
                if (dr["dUltimoConsumo"].ToString() != "")
                    oProf.anomesUltimoConsumo = SUPER.Capa_Negocio.Fechas.FechaAAnnomes((DateTime)dr["dUltimoConsumo"]); 

                if (dr["t812_finicio"].ToString() != "")
                    oProf.finicio = Convert.ToDateTime(dr["t812_finicio"]);
                if (dr["t812_ffin"].ToString() != "")
                    oProf.ffin = Convert.ToDateTime(dr["t812_ffin"]);

                oProf.tipo = Convert.ToString(dr["tipo"]);
                oProf.sexo = Convert.ToString(dr["t001_sexo"]);
                oProf.profesional = Convert.ToString(dr["Profesional"]);
                oProf.denValidador = Convert.ToString(dr["denValidador"]);
                oProf.perfil = Convert.ToString(dr["t819_denominacion"]);
                oProf.oficina = Convert.ToString(dr["t010_desoficina"]);
                oProf.visibleCV = Convert.ToString(dr["t812_visiblecv"]);

                if (dr["baja"].ToString()=="0")
                    oProf.baja =false;
                else
                    oProf.baja = true;

                if (dr["esfuerzoenjor"].ToString() != "")
                    oProf.esfuerzoJornadas = Convert.ToDouble(dr["esfuerzoenjor"]);

                lst.Add(oProf);

            }
            dr.Close();
            
            return lst;

        }
        public static int Insert(SqlTransaction tr, string t812_visiblecv, Nullable<DateTime> t812_finicio,
                                Nullable<DateTime> t812_ffin, int t001_idficepi, int t808_idexpprof, int t001_idficepiu)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t812_visiblecv", SqlDbType.Char, 1, t812_visiblecv == "" ? null : t812_visiblecv);
            aParam[i++] = ParametroSql.add("@t812_finicio", SqlDbType.SmallDateTime, 4, t812_finicio);
            aParam[i++] = ParametroSql.add("@t812_ffin", SqlDbType.SmallDateTime, 4, t812_ffin);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
            aParam[i++] = ParametroSql.add("@t001_idficepiu", SqlDbType.Int, 4, t001_idficepiu);
            //aParam[i++] = ParametroSql.add("@t819_idplantillacvt", SqlDbType.Int, 4, t819_idplantillacvt);

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_EXPPROFFICEPI_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_EXPPROFFICEPI_INS", aParam));
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T812_EXPPROFFICEPI.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	01/08/2012 12:59:16
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t812_idexpprofficepi, string t812_visiblecv, Nullable<DateTime> t812_finicio, 
                                 Nullable<DateTime> t812_ffin, int t001_idficepi, int t808_idexpprof, Nullable<int> t001_idficepi_validador, 
                                 Nullable<int> t819_idplantillacvt)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t812_idexpprofficepi", SqlDbType.Int, 4, t812_idexpprofficepi);
            aParam[i++] = ParametroSql.add("@t812_visiblecv", SqlDbType.Char, 1, t812_visiblecv == "" ? null : t812_visiblecv);
            aParam[i++] = ParametroSql.add("@t812_finicio", SqlDbType.SmallDateTime, 4, t812_finicio);
            aParam[i++] = ParametroSql.add("@t812_ffin", SqlDbType.SmallDateTime, 4, t812_ffin);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
            aParam[i++] = ParametroSql.add("@t001_idficepi_validador", SqlDbType.Int, 4, t001_idficepi_validador);
            aParam[i++] = ParametroSql.add("@t819_idplantillacvt", SqlDbType.Int, 4, t819_idplantillacvt);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_EXPPROFFICEPI_U2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPPROFFICEPI_U2", aParam);
        }
        /// <summary>
        /// Comprueba si existe algun registro en T812_EXPPROFFICEPI correspondiente a la experiencia profesional y asociada
        /// a otro profesional
        /// </summary>
        public static bool bHayOtroProfesional(SqlTransaction tr, int idExpProf, int idFicepi)
        {
            bool bRes = false;
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, idExpProf);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi);

            int returnValue;
            if (tr == null)
                returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_EXPPROFFICEPI_C3", aParam));
            else
                returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_EXPPROFFICEPI_C3", aParam));

            if (returnValue > 0)
                bRes = true;

            return bRes;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina registros de la tabla T812_EXPPROFFICEPI correspondientes a una experiencia profesional y un profesional
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static int Borrar(SqlTransaction tr, int t808_idexpprof, int idFicepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_EXPPROFFICEPI_D2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPPROFFICEPI_D2", aParam);
        }
        public static int PedirBorrado(SqlTransaction tr, int t812_idexpprofficepi, int t001_idficepi_petbor, string t812_motivo_petbor, bool t812_borradoPlantilla)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t812_idexpprofficepi", SqlDbType.Int, 4, t812_idexpprofficepi);
            aParam[i++] = ParametroSql.add("@t001_idficepi_petbor", SqlDbType.Int, 4, t001_idficepi_petbor);
            aParam[i++] = ParametroSql.add("@t812_motivo_petbor", SqlDbType.Text, 500, t812_motivo_petbor);
            aParam[i++] = ParametroSql.add("@t812_borradoPlantilla", SqlDbType.Bit, 1, t812_borradoPlantilla);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_EXPPROFFICEPI_PD", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPPROFFICEPI_PD", aParam);
        }
        /// <summary>
        /// Obtiene las experiencias de profesionales que han solicitado su borrado
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t314_idusuario"></param>
        /// <returns></returns>
        public static SqlDataReader CatalogoPdtesBorrar(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROFFICEPI_BORRAR_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROFFICEPI_BORRAR_CAT", aParam);
        }
        public static int QuitarPeticionBorrado(SqlTransaction tr, int t812_idexpprofficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t812_idexpprofficepi", SqlDbType.Int, 4, t812_idexpprofficepi);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_EXPPROFFICEPI_UB", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPPROFFICEPI_UB", aParam);
        }
        /// <summary>
        /// Establece la visibilidad de una experiencia. Si se pone a N tambien limpia los campos de petición de borrado
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t812_idexpprofficepi"></param>
        /// <param name="t812_visiblecv"></param>
        /// <param name="t819_idplantillacvt"></param>
        /// <returns></returns>
        public static int SetVisibilidad(SqlTransaction tr, int t812_idexpprofficepi, string t812_visiblecv, Nullable<int> t819_idplantillacvt)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t812_idexpprofficepi", SqlDbType.Int, 4, t812_idexpprofficepi);
            aParam[i++] = ParametroSql.add("@t812_visiblecv", SqlDbType.Char, 1, t812_visiblecv == "" ? null : t812_visiblecv);
            aParam[i++] = ParametroSql.add("@t819_idplantillacvt", SqlDbType.Int, 4, t819_idplantillacvt);
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_EXPPROFFICEPI_VISIBILIDAD_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPPROFFICEPI_VISIBILIDAD_U", aParam);
        }
        public static int BorrarPerfiles(SqlTransaction tr, int t812_idexpprofficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t812_idexpprofficepi", SqlDbType.Int, 4, t812_idexpprofficepi);
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_EXPFICEPIPERFIL_D_ALL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPFICEPIPERFIL_D_ALL", aParam);
        }
        public static int ActualizarFRealizTareasPlazo(SqlTransaction tr, int t812_idexpprofficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t812_idexpprofficepi", SqlDbType.Int, 4, t812_idexpprofficepi);
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_UPD_FREALIZ_TAR_EXPPROF", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_UPD_FREALIZ_TAR_EXPPROF", aParam);
        }
        public static SqlDataReader RealizarValidacion(SqlTransaction tr, int t812_idexpprofficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t812_idexpprofficepi", SqlDbType.Int, 4, t812_idexpprofficepi);
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_VAL_TAREAS_FICEPIPERFIL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_VAL_TAREAS_FICEPIPERFIL", aParam);
        }
 
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene la fecha más baja de los perfiles de un profesional en una experiencia profesional
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static DateTime? getFechaMinPerfiles(SqlTransaction tr, int t812_idexpprofficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1]{
                ParametroSql.add("@t812_idexpprofficepi", SqlDbType.Int, 4, t812_idexpprofficepi)
            };
            object dFechaMin = null;
            if (tr == null)
                dFechaMin = SqlHelper.ExecuteScalar("SUP_CVT_EXPPROFFICEPI_FINMIN", aParam);
            else
                dFechaMin = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_EXPPROFFICEPI_FINMIN", aParam);

            return (dFechaMin != DBNull.Value) ? (DateTime?)dFechaMin : null;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene la fecha más alta de los perfiles de un profesional en una experiencia profesional
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static DateTime? getFechaMaxPerfiles(SqlTransaction tr, int t812_idexpprofficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1]{
                ParametroSql.add("@t812_idexpprofficepi", SqlDbType.Int, 4, t812_idexpprofficepi)
            };
            object dFechaMax = null;
            if (tr == null)
                dFechaMax = SqlHelper.ExecuteScalar("SUP_CVT_EXPPROFFICEPI_FINMAX", aParam);
            else
                dFechaMax = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_EXPPROFFICEPI_FINMAX", aParam);

            //return (dFechaMax != DBNull.Value) ? (DateTime?)dFechaMax : null;
            DateTime? dtRes = null;
            if (dFechaMax.ToString() != "")
            {
                if ((dFechaMax.ToString().Substring(0, 10) != "31/12/2070"))
                    dtRes = (DateTime?)dFechaMax;
            }
            return dtRes;
        }

        public static int UpdateFechas(SqlTransaction tr, int t812_idexpprofficepi, Nullable<DateTime> t812_finicio,
                                        Nullable<DateTime> t812_ffin)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t812_idexpprofficepi", SqlDbType.Int, 4, t812_idexpprofficepi);
            aParam[i++] = ParametroSql.add("@t812_finicio", SqlDbType.SmallDateTime, 4, t812_finicio);
            aParam[i++] = ParametroSql.add("@t812_ffin", SqlDbType.SmallDateTime, 4, t812_ffin);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_EXPPROFFICEPI_FECHAS_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPPROFFICEPI_FECHAS_U", aParam);
        }


    }
}