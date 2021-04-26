using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
//Para el ArrayList
using System.Collections;

namespace SUPER.Capa_Datos
{
    /// <summary>
    /// Descripción breve de CAMPOS
    /// </summary>
    public class CAMPOS
    {
        #region Propiedades y Atributos

        private int _t290_idcampo;
        public int t290_idcampo
        {
            get { return _t290_idcampo; }
            set { _t290_idcampo = value; }
        }

        private string _t290_denominacion;
        public string t290_denominacion
        {
            get { return _t290_denominacion; }
            set { _t290_denominacion = value; }
        }

        private int _t001_idficepi_creador;
        public int t001_idficepi_creador
        {
            get { return _t001_idficepi_creador; }
            set { _t001_idficepi_creador = value; }
        }

        private string _profesional_creador;
        public string profesional_creador
        {
            get { return _profesional_creador; }
            set { _profesional_creador = value; }
        }

        private string _t291_idtipodato;
        public string t291_idtipodato
        {
            get { return _t291_idtipodato; }
            set { _t291_idtipodato = value; }
        }

        private int? _t001_ficepi_owner;
        public int? t001_ficepi_owner
        {
            get { return _t001_ficepi_owner; }
            set { _t001_ficepi_owner = value; }
        }

        private string _profesional_owner;
        public string profesional_owner
        {
            get { return _profesional_owner; }
            set { _profesional_owner = value; }
        }
        private string _denominacion_proyecto;
        public string denominacion_proyecto
        {
            get { return _denominacion_proyecto; }
            set { _denominacion_proyecto = value; }
        }
        private string _denominacion_cliente;
        public string denominacion_cliente
        {
            get { return _denominacion_cliente; }
            set { _denominacion_cliente = value; }
        }
        private string _denominacion_nodo;
        public string denominacion_nodo
        {
            get { return _denominacion_nodo; }
            set { _denominacion_nodo = value; }
        }
        private int? _t305_idproyectosubnodo;
        public int? t305_idproyectosubnodo
        {
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
        }

        private int? _t301_idproyecto;
        public int? t301_idproyecto
        {
            get { return _t301_idproyecto; }
            set { _t301_idproyecto = value; }
        }

        private int? _t302_idcliente;
        public int? t302_idcliente
        {
            get { return _t302_idcliente; }
            set { _t302_idcliente = value; }
        }

        private int? _t303_idnodo;
        public int? t303_idnodo
        {
            get { return _t303_idnodo; }
            set { _t303_idnodo = value; }
        }
        private string _codAmbito;
        public string codAmbito
        {
            get { return _codAmbito; }
            set { _codAmbito = value; }
        }
        #endregion
       #region Metodos

        public static SqlDataReader Catalogo(int iFicepiEntrada, Nullable<int> nCodAmbito, string nCodTipo, int nT305_idproyectosubnodo, ArrayList lstCamposPT)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi_entrada", SqlDbType.Int, 4, iFicepiEntrada),
                ParametroSql.add("@nAmbito", SqlDbType.Int, 4, nCodAmbito),
                ParametroSql.add("@nTipo", SqlDbType.Char, 1, nCodTipo),
                ParametroSql.add("@nT305_idproyectosubnodo", SqlDbType.Int, 3, nT305_idproyectosubnodo),
                ParametroSql.add("@TABLACAMPOSPT", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListCod(lstCamposPT))
            };
            return SqlHelper.ExecuteSqlDataReader("SUP_CAMPOS_CAT", aParam);
        }

        public static SqlDataReader Obtener()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado. 
            return SUPER.Capa_Datos.SqlHelper.ExecuteSqlDataReader("SUP_CAMPOS_C", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T290_CAMPOS
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	02/10/2009 13:57:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t290_denominacion, int t001_idficepi_creador,
                                string t291_idtipodato, int iAmbito, Nullable<int> t001_ficepi_owner, Nullable<int> t305_idproyectosubnodo, 
                                string t295_uidequipo)
                            {
                                SqlParameter[] aParam = new SqlParameter[7];
                                aParam[0] = new SqlParameter("@t290_denominacion", SqlDbType.Text, 50);
                                aParam[0].Value = t290_denominacion;
                                aParam[1] = new SqlParameter("@t001_idficepi_creador", SqlDbType.Int, 4);
                                aParam[1].Value = t001_idficepi_creador;
                                aParam[2] = new SqlParameter("@t291_idtipodato", SqlDbType.Char, 1);
                                aParam[2].Value = t291_idtipodato;
                                aParam[3] = new SqlParameter("@iAmbito", SqlDbType.Int, 4);
                                aParam[3].Value = iAmbito;
                                aParam[4] = new SqlParameter("@t001_ficepi_owner", SqlDbType.Int, 4);
                                aParam[4].Value = t001_ficepi_owner;
                                aParam[5] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
                                aParam[5].Value = t305_idproyectosubnodo;
                                aParam[6] = new SqlParameter("@t295_uidequipo", SqlDbType.UniqueIdentifier, 16);

                                if (t295_uidequipo == null) aParam[6].Value = null;
                                else aParam[6].Value = new Guid(t295_uidequipo);

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

		#endregion
    }
}