using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de TituloIdiomaFic
    /// </summary>
    public partial class TituloIdiomaFic
    {
        #region Metodos

        public static SqlDataReader Catalogo(int t001_idficepi, int t020_idcodidioma)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t020_idcodidioma", SqlDbType.Int, 4, t020_idcodidioma);
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TITULOIDIOMAFIC_CAT", aParam);
        }
        public static int Insert(string t021_titulo, Nullable<DateTime> t021_fecha, string t021_observa, string t021_centro, char t839_idestado, 
                                int t001_idficepi, int t020_idcodidioma, string sNombre, string t835_motivort,
                                int t001_idficepiu, Nullable<long> t2_iddocumento)
        {
            SqlParameter[] aParam = new SqlParameter[11];
            int i = 0;
            
            aParam[i++] = ParametroSql.add("@t021_titulo", SqlDbType.VarChar, 100, t021_titulo.Trim());
            aParam[i++] = ParametroSql.add("@t021_fecha", SqlDbType.SmallDateTime, 10, t021_fecha);
            aParam[i++] = ParametroSql.add("@t021_observa", SqlDbType.Text, 2147483647, t021_observa);
            aParam[i++] = ParametroSql.add("@t021_centro", SqlDbType.VarChar, 100, t021_centro.Trim());
            aParam[i++] = ParametroSql.add("@t839_idestado", SqlDbType.Char, 1, t839_idestado);
            aParam[i++] = ParametroSql.add("@t835_motivort", SqlDbType.Text, 2147483647, t835_motivort);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t001_idficepiu", SqlDbType.Int, 4, t001_idficepiu);
            aParam[i++] = ParametroSql.add("@t020_idcodidioma", SqlDbType.Int, 4, t020_idcodidioma);
            //aParam[i++] = ParametroSql.add("@doc", SqlDbType.Image, 2147483647, doc);
            aParam[i++] = ParametroSql.add("@sNombre", SqlDbType.VarChar, 250, sNombre.Trim());
            aParam[i++] = ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento);

            return SqlHelper.ExecuteNonQuery("SUP_CVT_TITULOIDIOMAFIC_INS", aParam);
        }
        public static int Update(Nullable<int> t021_idtituloidioma, string t021_titulo, Nullable<DateTime> t021_fecha, string t021_observa, 
                                string t021_centro, int t001_idficepi, int t020_idcodidioma, string sNombre, 
                                bool cambioDoc, char t839_idestado, string t835_motivort, int t001_idficepiu, char t839_idestado_ini,
                                Nullable<long> t2_iddocumento)
        {
            SqlParameter[] aParam = new SqlParameter[14];
            int i = 0;

            aParam[i++] = ParametroSql.add("@t021_idtituloidioma", SqlDbType.Int, 4, t021_idtituloidioma);
            aParam[i++] = ParametroSql.add("@t021_titulo", SqlDbType.VarChar, 100, t021_titulo.Trim());
            aParam[i++] = ParametroSql.add("@t021_fecha", SqlDbType.SmallDateTime, 10, t021_fecha);
            aParam[i++] = ParametroSql.add("@t021_observa", SqlDbType.Text, 2147483647, t021_observa);
            aParam[i++] = ParametroSql.add("@t021_centro", SqlDbType.VarChar, 100, t021_centro.Trim());
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t001_idficepiu", SqlDbType.Int, 4, t001_idficepiu);
            aParam[i++] = ParametroSql.add("@t020_idcodidioma", SqlDbType.Int, 4, t020_idcodidioma);
            //aParam[i++] = ParametroSql.add("@doc", SqlDbType.Image, 2147483647, doc);
            aParam[i++] = ParametroSql.add("@sNombre", SqlDbType.VarChar, 250, sNombre.Trim());
            aParam[i++] = ParametroSql.add("@cambioDoc", SqlDbType.Bit, 1, cambioDoc);
            aParam[i++] = ParametroSql.add("@t839_idestado", SqlDbType.Char, 1, t839_idestado);
            aParam[i++] = ParametroSql.add("@t835_motivort", SqlDbType.Text, 2147483647, t835_motivort);
            aParam[i++] = ParametroSql.add("@t839_idestado_ini", SqlDbType.Char, 1, t839_idestado_ini);
            aParam[i++] = ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento);

            return SqlHelper.ExecuteNonQuery("SUP_CVT_TITULOIDIOMAFIC_UPD", aParam);
        }
        public static SqlDataReader Detalle(int t021_idtituloidioma)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t021_idtituloidioma", SqlDbType.Int, 4, t021_idtituloidioma);
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TITULOIDIOMAFIC_SEL", aParam);
        }

        public static int Delete(SqlTransaction tr,int t021_idtituloidioma)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;

            aParam[i++] = ParametroSql.add("@t021_idtituloidioma", SqlDbType.Int, 4, t021_idtituloidioma);

            if (tr == null)
                return   Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_TITULOIDIOMAFIC_DEL", aParam));
            else
                return  Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_TITULOIDIOMAFIC_DEL", aParam));
             
        }

        public static SqlDataReader CatalogoCentro(string sDenominacion)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@denominacion", SqlDbType.VarChar, 100, sDenominacion);

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_TITULOIDIOMACENTRO", aParam);
        }

        public static void UpdatearDoc(SqlTransaction tr, int t021_idtituloidioma, int t001_idficepi, string sDenDoc, Nullable<long> t2_iddocumento)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                    ParametroSql.add("@t021_idtituloidioma", SqlDbType.Int, 4, t021_idtituloidioma),
                    ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                    ParametroSql.add("@t021_ndoc", SqlDbType.VarChar, 250, sDenDoc),
                    ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento)
                };
            if (tr == null)
                SqlHelper.ExecuteScalar("SUP_CVT_TITULOIDIOMAFIC_DOC_U", aParam);
            else
                SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_TITULOIDIOMAFIC_DOC_U", aParam);
        }
        #endregion
    }
}