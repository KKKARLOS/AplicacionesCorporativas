using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace SUPER.Capa_Datos
{
    /// <summary>
    /// Descripción breve de CAMPOS
    /// </summary>
    public class GrupoProf
    {
       #region Propiedades y Atributos

        private int _t295_uidequipo;

        public int t295_uidequipo
        {
            get { return _t295_uidequipo; }
            set { _t295_uidequipo = value; }
        }

        private string _t295_denominacion;
        public string t295_denominacion
        {
            get { return _t295_denominacion; }
            set { _t295_denominacion = value; }
        }
        #endregion
        #region Constructor

        public GrupoProf()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion
        #region Metodos
        public static SqlDataReader Catalogo(int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int);
            aParam[0].Value = t001_idficepi;

            return SqlHelper.ExecuteSqlDataReader("SUP_GP_CAT", aParam);
        }
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_GP_C", aParam);
        }
        public static SqlDataReader Integrantes(string t295_uidequipo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t295_uidequipo", SqlDbType.UniqueIdentifier);
            aParam[0].Value = new Guid(t295_uidequipo);

            return SqlHelper.ExecuteSqlDataReader("SUP_GP_PROF_CAT", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T290_CAMPOS
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	02/10/2009 13:57:05
        /// </history>                        //4  t001_ficepi_owner
        //5  t305_idproyectosubnodo
        //6  t302_idcliente
        //7  t303_idnodo
        //8  t295_uidequipo
        /// -----------------------------------------------------------------------------
/*        
        public static int Insert(SqlTransaction tr, string t290_denominacion, int t001_idficepi_creador,
            string t291_idtipodato, Nullable<int> t001_ficepi_owner, Nullable<int> t305_idproyectosubnodo, Nullable<int> t302_idcliente,
            Nullable<int> t303_idnodo, Nullable<int> t295_uidequipo)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@t290_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t290_denominacion;
            aParam[1] = new SqlParameter("@t001_idficepi_creador", SqlDbType.Int, 4);
            aParam[1].Value = t001_idficepi_creador;
            aParam[2] = new SqlParameter("@t291_idtipodato", SqlDbType.Char, 1);
            aParam[2].Value = t291_idtipodato;
            aParam[3] = new SqlParameter("@t001_ficepi_owner", SqlDbType.Int, 4);
            aParam[3].Value = t001_ficepi_owner;
            aParam[4] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
            aParam[4].Value = t302_idcliente;
            aParam[5] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[5].Value = t303_idnodo;
            aParam[6] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[6].Value = t305_idproyectosubnodo;
            aParam[7] = new SqlParameter("@t295_uidequipo", SqlDbType.UniqueIdentifier, 16);
            aParam[7].Value = t295_uidequipo;            

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SUPER.Capa_Datos.SqlHelper.ExecuteScalar("SUP_CAMPOS_I", aParam));
            else
                return Convert.ToInt32(SUPER.Capa_Datos.SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CAMPOS_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T290_CAMPOS a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	02/10/2009 13:57:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t290_idcampo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t290_idcampo", SqlDbType.Int, 4);
            aParam[0].Value = t290_idcampo;

            if (tr == null)
                return SUPER.Capa_Datos.SqlHelper.ExecuteNonQuery("SUP_CAMPOS_D", aParam);
            else
                return SUPER.Capa_Datos.SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMPOS_D", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T290_CAMPOS,
        /// y devuelve una instancia u objeto del tipo T290_CAMPOS
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	02/10/2009 13:57:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static CAMPOS Select(SqlTransaction tr, int t290_idcampo)
        {
            CAMPOS o = new CAMPOS();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t290_idcampo", SqlDbType.Int, 4);
            aParam[0].Value = t290_idcampo;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_CAMPOS_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CAMPOS_S", aParam);


            if (dr.Read())
            {
                if (dr["t290_denominacion"] != DBNull.Value)
                    o.t290_denominacion = (string)dr["t290_denominacion"];

                if (dr["t001_idficepi_creador"] != DBNull.Value)
                    o.t001_idficepi_creador = (int)dr["t001_idficepi_creador"];

                if (dr["t001_ficepi_owner"] != DBNull.Value)
                    o.t001_ficepi_owner = (int)dr["t001_ficepi_owner"];

                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = (int)dr["t305_idproyectosubnodo"];

                if (dr["t302_idcliente"] != DBNull.Value)
                    o.t302_idcliente = (int)dr["t302_idcliente"];

                if (dr["t303_idnodo"] != DBNull.Value)
                    o.t303_idnodo = (int)dr["t303_idnodo"];

                if (dr["t291_idtipodato"] != DBNull.Value)
                    o.t291_idtipodato = (string)dr["t291_idtipodato"];
                
                if (dr["profesional_creador"] != DBNull.Value)
                    o.profesional_creador = (string)dr["profesional_creador"];

                if (dr["profesional_owner"] != DBNull.Value)
                    o.profesional_owner = (string)dr["profesional_owner"];

                if (dr["codAmbito"] != DBNull.Value)
                    o.codAmbito = (string)dr["codAmbito"];

                if (dr["denominacion_cliente"] != DBNull.Value)
                    o.denominacion_cliente = (string)dr["denominacion_cliente"];

                if (dr["denominacion_nodo"] != DBNull.Value)
                    o.denominacion_nodo = (string)dr["denominacion_nodo"];

                if (dr["denominacion_proyecto"] != DBNull.Value)
                    o.denominacion_proyecto = (string)dr["denominacion_proyecto"];

                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = (int)dr["t301_idproyecto"];

            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de CAMPO"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
*/
		#endregion
    }
}