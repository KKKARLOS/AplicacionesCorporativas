using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
//Para poder usar el HttpContext
using System.Web;

namespace SUPER.Capa_Negocio
{
    public partial class USUARIOPROYECTOSUBNODO
    {
        #region Propiedades y Atributos

        private string _sProfesional;
        public string sProfesional
        {
            get { return _sProfesional; }
            set { _sProfesional = value; }
        }
        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T330_USUARIOPROYECTOSUBNODO.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	05/06/2008 11:39:23
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void Insert(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario, decimal t330_costecon, decimal t330_costerep, bool t330_deriva, DateTime t330_falta, Nullable<DateTime> t330_fbaja, Nullable<int> t333_idperfilproy)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t330_costecon", SqlDbType.Money, 8);
            aParam[2].Value = t330_costecon;
            aParam[3] = new SqlParameter("@t330_costerep", SqlDbType.Money, 8);
            aParam[3].Value = t330_costerep;
            aParam[4] = new SqlParameter("@t330_deriva", SqlDbType.Bit, 1);
            aParam[4].Value = t330_deriva;
            aParam[5] = new SqlParameter("@t330_falta", SqlDbType.SmallDateTime, 4);
            aParam[5].Value = t330_falta;
            aParam[6] = new SqlParameter("@t330_fbaja", SqlDbType.SmallDateTime, 4);
            aParam[6].Value = t330_fbaja;
            aParam[7] = new SqlParameter("@t333_idperfilproy", SqlDbType.Int, 4);
            aParam[7].Value = t333_idperfilproy;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_USUARIOPROYECTOSUBNODO_IN", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIOPROYECTOSUBNODO_IN", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T330_USUARIOPROYECTOSUBNODO.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	05/06/2008 11:39:23
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario, Nullable<decimal> t330_costecon, Nullable<decimal> t330_costerep, bool t330_deriva, DateTime t330_falta, Nullable<DateTime> t330_fbaja, Nullable<int> t333_idperfilproy)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@t330_costecon", SqlDbType.Money, 8);
            aParam[2].Value = t330_costecon;
            aParam[3] = new SqlParameter("@t330_costerep", SqlDbType.Money, 8);
            aParam[3].Value = t330_costerep;
            aParam[4] = new SqlParameter("@t330_deriva", SqlDbType.Bit, 1);
            aParam[4].Value = t330_deriva;
            aParam[5] = new SqlParameter("@t330_falta", SqlDbType.SmallDateTime, 4);
            aParam[5].Value = t330_falta;
            aParam[6] = new SqlParameter("@t330_fbaja", SqlDbType.SmallDateTime, 4);
            aParam[6].Value = t330_fbaja;
            aParam[7] = new SqlParameter("@t333_idperfilproy", SqlDbType.Int, 4);
            aParam[7].Value = t333_idperfilproy;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_USUARIOPROYECTOSUBNODO_UP", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIOPROYECTOSUBNODO_UP", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo los profesionales asociados al proyectosubnodo
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoUsuarios(int t305_idproyectosubnodo, string sCualidad, bool bMostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
            aParam[1].Value = sCualidad;
            aParam[2] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[2].Value = bMostrarBajas;
            //Si el foraneo está asociado al proyecto hay que mostrarlo aunque la parametrización diga que no
            //aParam[3] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            //aParam[3].Value = (bool)HttpContext.Current.Session["FORANEOS"];

            return SqlHelper.ExecuteSqlDataReader("SUP_USUARIOPROYECTOSUBNODO_CD", aParam);
        }
        public static SqlDataReader CatalogoUsuariosRepJ(int t305_idproyectosubnodo, bool bMostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[1].Value = bMostrarBajas;

            return SqlHelper.ExecuteSqlDataReader("SUP_USUARIOPROYECTOSUBNODO_CD_REPJ", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene el costecon y coste rep de un usuario en un proyectosubnodo
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader GetCoste(int t305_idproyectosubnodo, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            return SqlHelper.ExecuteSqlDataReader("SUP_USUARIOPROYECTOSUBNODO_COSTE", aParam);
        }
        public static bool AsociadoDeAltaProyecto(SqlTransaction tr, int nProy,  int nIdRecurso)
        {
            bool bAsociado = false;

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nProy", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdRecurso", SqlDbType.Int, 4);

            aParam[0].Value = nProy;
            aParam[1].Value = nIdRecurso;

            int nResul = 0;
            if (tr != null)
                nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_RECASOCPROY_ALTA", aParam));
            else
                nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_RECASOCPROY_ALTA", aParam));

            if (nResul > 0) bAsociado = true;

            return bAsociado;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo los profesionales asociados al proyectosubnodo
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoUsuariosPerfil(int t305_idproyectosubnodo, bool bConPerfil)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@bConPerfil", SqlDbType.Bit, 1);
            aParam[1].Value = bConPerfil;

            return SqlHelper.ExecuteSqlDataReader("SUP_USUARIOPSN_PERFIL_CAT", aParam);
        }
        #endregion
    }
}
