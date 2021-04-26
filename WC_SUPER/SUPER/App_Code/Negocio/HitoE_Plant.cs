using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : HITOE_PLANT
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: t369_HITOE_PLANT
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	19/11/2007 15:41:11	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class HITOE_PLANT
	{
        #region Propiedades y Atributos

        private int _t369_idhito;
        public int t369_idhito
        {
            get { return _t369_idhito; }
            set { _t369_idhito = value; }
        }

        private string _t369_deshito;
        public string t369_deshito
        {
            get { return _t369_deshito; }
            set { _t369_deshito = value; }
        }

        private string _t369_deshitolong;
        public string t369_deshitolong
        {
            get { return _t369_deshitolong; }
            set { _t369_deshitolong = value; }
        }

        private bool _t369_alerta;
        public bool t369_alerta
        {
            get { return _t369_alerta; }
            set { _t369_alerta = value; }
        }

        private short _t369_orden;
        public short t369_orden
        {
            get { return _t369_orden; }
            set { _t369_orden = value; }
        }

        private int _t338_idplantilla;
        public int t338_idplantilla
        {
            get { return _t338_idplantilla; }
            set { _t338_idplantilla = value; }
        }
        #endregion

        #region Constructores

        public HITOE_PLANT()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla t369_HITOE_PLANT.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOARHUMI]	19/11/2007 8:58:31
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t369_deshito, string t369_deshitolong, bool t369_alerta, short t369_orden, int t338_idplantilla)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t369_deshito", SqlDbType.Text, 50);
            aParam[0].Value = t369_deshito;
            aParam[1] = new SqlParameter("@t369_deshitolong", SqlDbType.Text, 2147483647);
            aParam[1].Value = t369_deshitolong;
            aParam[2] = new SqlParameter("@t369_alerta", SqlDbType.Bit, 1);
            aParam[2].Value = t369_alerta;
            aParam[3] = new SqlParameter("@t369_orden", SqlDbType.SmallInt, 2);
            aParam[3].Value = t369_orden;
            aParam[4] = new SqlParameter("@t338_idplantilla", SqlDbType.Int, 4);
            aParam[4].Value = t338_idplantilla;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_HITOE_PLANT_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_HITOE_PLANT_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla t369_HITOE_PLANT.
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	19/11/2007 8:58:31
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t369_idhito, string t369_deshito, string t369_deshitolong, Nullable<bool> t369_alerta, short t369_orden)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t369_idhito", SqlDbType.Int, 4);
            aParam[0].Value = t369_idhito;
            aParam[1] = new SqlParameter("@t369_deshito", SqlDbType.Text, 50);
            aParam[1].Value = t369_deshito;
            aParam[2] = new SqlParameter("@t369_deshitolong", SqlDbType.Text, 2147483647);
            aParam[2].Value = t369_deshitolong;
            aParam[3] = new SqlParameter("@t369_alerta", SqlDbType.Bit, 1);
            aParam[3].Value = t369_alerta;
            aParam[4] = new SqlParameter("@t369_orden", SqlDbType.SmallInt, 2);
            aParam[4].Value = t369_orden;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_HITOE_PLANT_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_HITOE_PLANT_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla t369_HITOE_PLANT a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	19/11/2007 8:58:31
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t369_idhito)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t369_idhito", SqlDbType.Int, 4);
            aParam[0].Value = t369_idhito;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_HITOE_PLANT_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_HITOE_PLANT_D", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla t369_HITOE_PLANT,
        /// y devuelve una instancia u objeto del tipo HITOE_PLANT
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOARHUMI]	19/11/2007 8:58:31
        /// </history>
        /// -----------------------------------------------------------------------------
        public static HITOE_PLANT Select(SqlTransaction tr, int t369_idhito)
        {
            //string sAlerta;
            HITOE_PLANT o = new HITOE_PLANT();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t369_idhito", SqlDbType.Int, 4);
            aParam[0].Value = t369_idhito;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_HITOE_PLANT_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_HITOE_PLANT_S", aParam);

            if (dr.Read())
            {
                if (dr["t369_idhito"] != DBNull.Value)
                    o.t369_idhito = (int)dr["t369_idhito"];
                if (dr["t369_deshito"] != DBNull.Value)
                    o.t369_deshito = (string)dr["t369_deshito"];
                if (dr["t369_deshitolong"] != DBNull.Value)
                    o.t369_deshitolong = (string)dr["t369_deshitolong"];

                if (dr["t369_alerta"] != DBNull.Value)
                {
                    //sAlerta = dr["t369_alerta"].ToString();
                    //if (sAlerta == "1")
                    //    o.t369_alerta = true;
                    //else
                    //    o.t369_alerta = false;
                    o.t369_alerta = (bool)dr["t369_alerta"];
                }
                if (dr["t369_orden"] != DBNull.Value)
                    o.t369_orden = short.Parse(dr["t369_orden"].ToString());
                if (dr["t338_idplantilla"] != DBNull.Value)
                    o.t338_idplantilla = (int)dr["t338_idplantilla"];

            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de HITOE_PLANT"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla t369_HITOE_PLANT.
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	19/11/2007 8:58:31
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoHitos(int iCodPlant)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdPlant", SqlDbType.Int);
            aParam[0].Value = iCodPlant;

            return SqlHelper.ExecuteSqlDataReader("SUP_HITOE_PLANT_CATA", aParam);
        }

        public static SqlDataReader CatalogoTareasPlantilla(int iCodPlant)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdPlant", SqlDbType.SmallInt);
            aParam[0].Value = iCodPlant;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_HITOE_PLANT_TAR_CATA", aParam);

            return dr;
        }

        //Devuelve una cadena con las tareas de plantilla del código de hito en plantilla
        public static string fgListaTareasPlantilla(SqlTransaction tr, int idItemHitoPl)
        {
            string sRes = "";
            SqlDataReader drH = HITOE_PLANT_TAREA.SelectByt369_idhito(tr, idItemHitoPl);
            while (drH.Read())
            {
                sRes += drH["codTarea"].ToString() + "##";
            }
            drH.Close(); drH.Dispose();
            return sRes;
        }
        #endregion
    }
}
