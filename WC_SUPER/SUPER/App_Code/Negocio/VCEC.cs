using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : VCEC
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T435_VCEC
    /// </summary>
    /// <history>
    /// 	Creado por [DOARHUMI]	04/08/2009 13:19:23	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class VCEC
    {
        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T435_VCEC.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOARHUMI]	20/08/2009 13:19:23
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t435_valor, bool t435_estado, int t345_idcec, byte t435_orden)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t435_valor", SqlDbType.Text, 25);
            aParam[0].Value = t435_valor;
            aParam[1] = new SqlParameter("@t435_estado", SqlDbType.Bit, 1);
            aParam[1].Value = t435_estado;
            aParam[2] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
            aParam[2].Value = t345_idcec;
            aParam[3] = new SqlParameter("@t435_orden", SqlDbType.TinyInt, 1);
            aParam[3].Value = t435_orden;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_VCEC_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_VCEC_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T435_VCEC.
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	20/08/2009 13:19:23
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t435_idvcec, string t435_valor, bool t435_estado, int t345_idcec, byte t435_orden)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t435_idvcec", SqlDbType.Int, 4);
            aParam[0].Value = t435_idvcec;
            aParam[1] = new SqlParameter("@t435_valor", SqlDbType.Text, 25);
            aParam[1].Value = t435_valor;
            aParam[2] = new SqlParameter("@t435_estado", SqlDbType.Bit, 1);
            aParam[2].Value = t435_estado;
            aParam[3] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
            aParam[3].Value = t345_idcec;
            aParam[4] = new SqlParameter("@t435_orden", SqlDbType.TinyInt, 1);
            aParam[4].Value = t435_orden;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_VCEC_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_VCEC_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T435_VCEC a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	20/08/2009 13:19:23
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t435_idvcec)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t435_idvcec", SqlDbType.Int, 4);
            aParam[0].Value = t435_idvcec;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_VCEC_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_VCEC_D", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T435_VCEC.
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	20/08/2009 13:19:23
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t435_idvcec, string t435_valor, Nullable<bool> t435_estado, Nullable<int> t345_idcec, 
                                             Nullable<byte> t435_orden, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@t435_idvcec", SqlDbType.Int, 4);
            aParam[0].Value = t435_idvcec;
            aParam[1] = new SqlParameter("@t435_valor", SqlDbType.Text, 25);
            aParam[1].Value = t435_valor;
            aParam[2] = new SqlParameter("@t435_estado", SqlDbType.Bit, 1);
            aParam[2].Value = t435_estado;
            aParam[3] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
            aParam[3].Value = t345_idcec;
            aParam[4] = new SqlParameter("@t435_orden", SqlDbType.TinyInt, 1);
            aParam[4].Value = t435_orden;

            aParam[5] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[5].Value = nOrden;
            aParam[6] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[6].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_VCEC_C", aParam);
        }


        public static SqlDataReader CatalogoCorporativosByNodo(int t303_idnodo, Nullable<bool> bActivos)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@idNodo", SqlDbType.Int, 2);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@bActivos", SqlDbType.Bit, 1);
            aParam[1].Value = bActivos;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_VCEC_SByNodo", aParam);
        }
        public static SqlDataReader CatalogoCorporativosByListaNodo(string slNodos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@idsNodo", SqlDbType.VarChar, 1000);
            aParam[0].Value = slNodos;
            return SqlHelper.ExecuteSqlDataReader("SUP_VCEC_SByListaNodo", aParam);
        }
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_VCEEC", aParam);
        }
        public static SqlDataReader Asociados_A_Nodos(int t345_idcec, int t435_idvcec, bool bValorAsignado)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t345_idcec", SqlDbType.Int, 4);
            aParam[0].Value = t345_idcec;
            aParam[1] = new SqlParameter("@t435_idvcec", SqlDbType.Int, 4);
            aParam[1].Value = t435_idvcec;
            aParam[2] = new SqlParameter("@bValorAsignado", SqlDbType.Bit, 1);
            aParam[2].Value = bValorAsignado;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_VCEC_NODOS", aParam);
        }
        #endregion
    }
}
