using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

/// <summary>
/// Summary description for ESPACIOACUERDO
/// </summary>
namespace SUPER.Capa_Negocio
{
    public partial class ESPACIOACUERDO
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Comprueba si existe un espacio de acuerdo para el que ya se ha pedido aceptación
        /// </summary>
        /// -----------------------------------------------------------------------------
        //public static bool Existe(SqlTransaction tr, int t301_idproyecto)
        //{
        //    bool bRes=false;
        //    SqlParameter[] aParam = new SqlParameter[1];
        //    aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
        //    aParam[0].Value = t301_idproyecto;
        //    SqlDataReader dr;
        //    if (tr == null)
        //        dr = SqlHelper.ExecuteSqlDataReader("SUP_ESPACIOACUERDO_S2", aParam);
        //    else
        //        dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ESPACIOACUERDO_S2", aParam);
        //    if (dr.Read())
        //        bRes = true;
        //    dr.Close();
        //    dr.Dispose();

        //    return bRes;
        //}
        public static bool TieneDocumentacion(SqlTransaction tr, int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ESPACIOACUERDO_DOCS", aParam)) > 0) ? true : false;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ESPACIOACUERDO_DOCS", aParam)) > 0) ? true : false;

        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T638_ESPACIOACUERDO.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	20/12/2010 15:44:09
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update2(SqlTransaction tr, int t638_idAcuerdo, bool t638_tipoIAP, bool t638_tiporesproy, bool t638_tipocliente,
                                bool t638_tipoimpfijo, bool t638_tipootras, string t638_textootras, string t638_periodicidad,
                                string t638_aconsiderar, bool t638_conciliacion, string t638_tipoconciliacion, string t638_contacto,
                                Nullable<int> t314_idusuario_findatos, Nullable<DateTime> t638_ffin, bool t638_facturaSA)
        {
            SqlParameter[] aParam = new SqlParameter[15];
            aParam[0] = new SqlParameter("@t638_idAcuerdo", SqlDbType.Int, 4);
            aParam[0].Value = t638_idAcuerdo;
            aParam[1] = new SqlParameter("@t638_tipoIAP", SqlDbType.Bit, 1);
            aParam[1].Value = t638_tipoIAP;
            aParam[2] = new SqlParameter("@t638_tiporesproy", SqlDbType.Bit, 1);
            aParam[2].Value = t638_tiporesproy;
            aParam[3] = new SqlParameter("@t638_tipocliente", SqlDbType.Bit, 1);
            aParam[3].Value = t638_tipocliente;
            aParam[4] = new SqlParameter("@t638_tipoimpfijo", SqlDbType.Bit, 1);
            aParam[4].Value = t638_tipoimpfijo;
            aParam[5] = new SqlParameter("@t638_tipootras", SqlDbType.Bit, 1);
            aParam[5].Value = t638_tipootras;
            aParam[6] = new SqlParameter("@t638_textootras", SqlDbType.Text, 2147483647);
            aParam[6].Value = t638_textootras;
            aParam[7] = new SqlParameter("@t638_periodicidad", SqlDbType.Text, 50);
            aParam[7].Value = t638_periodicidad;
            aParam[8] = new SqlParameter("@t638_aconsiderar", SqlDbType.Text, 2147483647);
            aParam[8].Value = t638_aconsiderar;
            aParam[9] = new SqlParameter("@t638_conciliacion", SqlDbType.Bit, 1);
            aParam[9].Value = t638_conciliacion;
            aParam[10] = new SqlParameter("@t638_tipoconciliacion", SqlDbType.Char, 1);
            aParam[10].Value = t638_tipoconciliacion;
            aParam[11] = new SqlParameter("@t638_contacto", SqlDbType.Text, 250);
            aParam[11].Value = t638_contacto;
            aParam[12] = new SqlParameter("@t314_idusuario_findatos", SqlDbType.Int, 4);
            aParam[12].Value = t314_idusuario_findatos;
            aParam[13] = new SqlParameter("@t638_ffin", SqlDbType.SmallDateTime, 4);
            aParam[13].Value = t638_ffin;
            aParam[14] = new SqlParameter("@t638_facturaSA", SqlDbType.Bit, 1);
            aParam[14].Value = t638_facturaSA;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ESPACIOACUERDO_U2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESPACIOACUERDO_U2", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Acepta un espacio de acuerdo
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	20/12/2010 15:44:09
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Aceptar(SqlTransaction tr, int t638_idAcuerdo, Nullable<int> t314_idusuario_aceptacion, Nullable<DateTime> t638_facept)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t638_idAcuerdo", SqlDbType.Int, 4);
            aParam[0].Value = t638_idAcuerdo;
            aParam[1] = new SqlParameter("@t314_idusuario_aceptacion", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_aceptacion;
            aParam[2] = new SqlParameter("@t638_facept", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = t638_facept;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ESPACIOACUERDO_U3", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESPACIOACUERDO_U3", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Deniega un espacio de acuerdo
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	20/12/2010 15:44:09
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Denegar(SqlTransaction tr, int t638_idAcuerdo, Nullable<int> t314_idusuario_denegacion, 
                                  Nullable<DateTime> t638_fdeneg, string sMensDeneg)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t638_idAcuerdo", SqlDbType.Int, 4);
            aParam[0].Value = t638_idAcuerdo;
            aParam[1] = new SqlParameter("@t314_idusuario_denegacion", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_denegacion;
            aParam[2] = new SqlParameter("@t638_fdeneg", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = t638_fdeneg;
            aParam[3] = new SqlParameter("@t638_desdeneg", SqlDbType.Text, 2147483647);
            aParam[3].Value = sMensDeneg;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ESPACIOACUERDO_U4", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESPACIOACUERDO_U4", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina los espacios de acuerdo de un proyecto
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	20/12/2010 15:44:09
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete2(SqlTransaction tr, int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ESPACIOACUERDO_D2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESPACIOACUERDO_D2", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene el último espacio de acuerdo del proyecto que se pasa como parámetro
        /// </summary>
        /// <returns>DataSet</returns>
        /// <history>
        /// 	Creado por [sqladmin]	20/12/2010 15:44:09
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Select1(SqlTransaction tr, int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;


            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ESPACIOACUERDO_S2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ESPACIOACUERDO_S2", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Selecciona los registros de la tabla T638_ESPACIOACUERDO dependientes de un proyecto económico
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(SqlTransaction tr, int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;


            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ESPACIOACUERDO_C2", aParam);//SUP_ESPACIOACUERDO_SByT301_idproyecto
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ESPACIOACUERDO_C2", aParam);
        }
    }
}