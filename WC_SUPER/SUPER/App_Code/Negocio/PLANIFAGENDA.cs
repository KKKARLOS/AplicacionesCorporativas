using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class PLANIFAGENDA
    {
        #region Propiedades y Atributos Auxiliares

        private string _sProfesional;
        public string sProfesional
        {
            get { return _sProfesional; }
            set { _sProfesional = value; }
        }
        private string _sCodRedProfesional;
        public string sCodRedProfesional
        {
            get { return _sCodRedProfesional; }
            set { _sCodRedProfesional = value; }
        }

        private string _sPromotor;
        public string sPromotor
        {
            get { return _sPromotor; }
            set { _sPromotor = value; }
        }
        private string _sCodRedPromotor;
        public string sCodRedPromotor
        {
            get { return _sCodRedPromotor; }
            set { _sCodRedPromotor = value; }
        }

        private string _sTarea;
        public string sTarea
        {
            get { return _sTarea; }
            set { _sTarea = value; }
        }

        #endregion

        #region Metodos

        public static PLANIFAGENDA Obtener(SqlTransaction tr, int t458_idPlanif)
        {
            PLANIFAGENDA o = new PLANIFAGENDA();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t458_idPlanif", SqlDbType.Int, 4);
            aParam[0].Value = t458_idPlanif;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_PLANIFAGENDA_O", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PLANIFAGENDA_O", aParam);

            if (dr.Read())
            {
                if (dr["t458_idPlanif"] != DBNull.Value)
                    o.t458_idPlanif = int.Parse(dr["t458_idPlanif"].ToString());
                if (dr["t001_idficepi"] != DBNull.Value)
                    o.t001_idficepi = int.Parse(dr["t001_idficepi"].ToString());
                if (dr["t001_idficepi_mod"] != DBNull.Value)
                    o.t001_idficepi_mod = int.Parse(dr["t001_idficepi_mod"].ToString());
                if (dr["t458_fechamod"] != DBNull.Value)
                    o.t458_fechamod = (DateTime)dr["t458_fechamod"];
                if (dr["t458_asunto"] != DBNull.Value)
                    o.t458_asunto = (string)dr["t458_asunto"];
                if (dr["t458_motivo"] != DBNull.Value)
                    o.t458_motivo = (string)dr["t458_motivo"];
                if (dr["t458_fechoraini"] != DBNull.Value)
                    o.t458_fechoraini = (DateTime)dr["t458_fechoraini"];
                if (dr["t458_fechorafin"] != DBNull.Value)
                    o.t458_fechorafin = (DateTime)dr["t458_fechorafin"];
                if (dr["t332_idtarea"] != DBNull.Value)
                    o.t332_idtarea = int.Parse(dr["t332_idtarea"].ToString());
                if (dr["t458_privado"] != DBNull.Value)
                    o.t458_privado = (string)dr["t458_privado"];
                if (dr["t458_observaciones"] != DBNull.Value)
                    o.t458_observaciones = (string)dr["t458_observaciones"];
                if (dr["t332_destarea"] != DBNull.Value)
                    o.sTarea = (string)dr["t332_destarea"];
                if (dr["Profesional"] != DBNull.Value)
                    o.sProfesional = (string)dr["Profesional"];
                if (dr["Promotor"] != DBNull.Value)
                    o.sPromotor = (string)dr["Promotor"];
                if (dr["codred_profesional"] != DBNull.Value)
                    o.sCodRedProfesional = (string)dr["codred_profesional"];
                if (dr["codred_promotor"] != DBNull.Value)
                    o.sCodRedPromotor = (string)dr["codred_promotor"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de PLANIFAGENDA"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static DataSet CatalogoPlanificacion(int t001_idficepi, DateTime t458_fechoraini, DateTime t458_fechorafin)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@t458_fechoraini", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = t458_fechoraini;
            aParam[2] = new SqlParameter("@t458_fechorafin", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = t458_fechorafin;

            return SqlHelper.ExecuteDataset("SUP_PLANIFAGENDA_CAT", aParam);
        }

        public static bool ObtenerDisponibilidadAgenda(SqlTransaction tr, int t001_idficepi, DateTime t458_fechoraini, DateTime t458_fechorafin, int t458_idPlanif)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@t458_fechoraini", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = t458_fechoraini;
            aParam[2] = new SqlParameter("@t458_fechorafin", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = t458_fechorafin;
            aParam[3] = new SqlParameter("@t458_idPlanif", SqlDbType.Int, 4);
            aParam[3].Value = t458_idPlanif;

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DISPONIBILIDADAGENDA", aParam)) == 0) ? true : false;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DISPONIBILIDADAGENDA", aParam)) == 0) ? true : false;
        }
        public static DataSet ObtenerPromotoresCitasRango(SqlTransaction tr, int t001_idficepi, DateTime t458_fechoraini, DateTime t458_fechorafin, int t458_idPlanif)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@t458_fechoraini", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = t458_fechoraini;
            aParam[2] = new SqlParameter("@t458_fechorafin", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = t458_fechorafin;
            aParam[3] = new SqlParameter("@t458_idPlanif", SqlDbType.Int, 4);
            aParam[3].Value = t458_idPlanif;

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_PROMOTORESAGENDA_CAT", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_PROMOTORESAGENDA_CAT", aParam);
        }

        public static SqlDataReader ObtenerTotalesPlanificacionSemanal(int t001_idficepi, Nullable<DateTime> dLunes, Nullable<DateTime> dMartes, Nullable<DateTime> dMiercoles, Nullable<DateTime> dJueves, Nullable<DateTime> dViernes, Nullable<DateTime> dSabado, Nullable<DateTime> dDomingo)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@dLunes", SqlDbType.DateTime, 8);
            aParam[1].Value = dLunes;
            aParam[2] = new SqlParameter("@dMartes", SqlDbType.DateTime, 8);
            aParam[2].Value = dMartes;
            aParam[3] = new SqlParameter("@dMiercoles", SqlDbType.DateTime, 8);
            aParam[3].Value = dMiercoles;
            aParam[4] = new SqlParameter("@dJueves", SqlDbType.DateTime, 8);
            aParam[4].Value = dJueves;
            aParam[5] = new SqlParameter("@dViernes", SqlDbType.DateTime, 8);
            aParam[5].Value = dViernes;
            aParam[6] = new SqlParameter("@dSabado", SqlDbType.DateTime, 8);
            aParam[6].Value = dSabado;
            aParam[7] = new SqlParameter("@dDomingo", SqlDbType.DateTime, 8);
            aParam[7].Value = dDomingo;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOAGENDASEMANA", aParam);
        }

        public static double ObtenerTotalesPlanificacionDia(int t001_idficepi, DateTime dDia)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@dDia", SqlDbType.DateTime, 8);
            aParam[1].Value = dDia;

            return Convert.ToDouble(SqlHelper.ExecuteScalar("SUP_CONSUMOAGENDADIA", aParam));
        }

        public static SqlDataReader ObtenerTareasAgenda_PSN(int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;

            return SqlHelper.ExecuteSqlDataReader("SUP_TAREASAGENDA_PSN", aParam);
        }
        public static SqlDataReader ObtenerTareasAgenda_PT(int t001_idficepi, int nPSN)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[1].Value = nPSN;

            return SqlHelper.ExecuteSqlDataReader("SUP_TAREASAGENDA_PT", aParam);
        }
        public static SqlDataReader ObtenerTareasAgenda_T(int t001_idficepi, int nPT)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@nPT", SqlDbType.Int, 4);
            aParam[1].Value = nPT;

            return SqlHelper.ExecuteSqlDataReader("SUP_TAREASAGENDA_T", aParam);
        }
        public static SqlDataReader ValidarTareaAgenda_T(int t001_idficepi, int t332_idtarea)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[1].Value = t332_idtarea;

            return SqlHelper.ExecuteSqlDataReader("SUP_VALIDARTAREAAGENDA", aParam);
        }

        public static SqlDataReader ObtenerFacturabilidadDisponibilidad(int t001_idficepi, DateTime dDesde, DateTime dHasta)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@dDesde", SqlDbType.DateTime, 8);
            aParam[1].Value = dDesde;
            aParam[2] = new SqlParameter("@dHasta", SqlDbType.DateTime, 8);
            aParam[2].Value = dHasta;

            return SqlHelper.ExecuteSqlDataReader("SUP_FACTUDISPO", aParam);
        }

        #endregion
    }
}
