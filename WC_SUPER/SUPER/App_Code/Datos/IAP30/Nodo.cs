using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Nodo
/// </summary>

namespace IB.SUPER.IAP30.DAL
{

    internal class Nodo
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t303_idnodo = 1
        }

        internal Nodo(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        #region funciones publicas        

        /// <summary>
        /// Obtiene todos los Nodo
        /// </summary>
        internal List<Models.Nodo> Catalogo()
        {
            Models.Nodo oNodo = null;
            List<Models.Nodo> lst = new List<Models.Nodo>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] {
                    
                };

                dr = cDblib.DataReader("SUP_NODO_C1", dbparams);
                while (dr.Read())
                {
                    oNodo = new Models.Nodo();
                    oNodo.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    oNodo.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                    oNodo.t303_cierreIAPestandar = Convert.ToBoolean(dr["t303_cierreIAPestandar"]);
                    oNodo.t303_ultcierreIAP = Convert.ToInt32(dr["t303_ultcierreIAP"]);
                    oNodo.t303_cierreECOestandar = Convert.ToBoolean(dr["t303_cierreECOestandar"]);
                    oNodo.t303_ultcierreECO = Convert.ToInt32(dr["t303_ultcierreECO"]);
                    oNodo.t303_denominacion_CNP = Convert.ToString(dr["t303_denominacion_CNP"]);
                    oNodo.t303_obligatorio_CNP = Convert.ToInt32(dr["t303_obligatorio_CNP"]);
                    oNodo.t391_denominacion_CSN1P = Convert.ToString(dr["t391_denominacion_CSN1P"]);
                    oNodo.t391_obligatorio_CSN1P = Convert.ToInt32(dr["t391_obligatorio_CSN1P"]);
                    oNodo.t392_denominacion_CSN2P = Convert.ToString(dr["t392_denominacion_CSN2P"]);
                    oNodo.t392_obligatorio_CSN2P = Convert.ToInt32(dr["t392_obligatorio_CSN2P"]);
                    oNodo.t393_denominacion_CSN3P = Convert.ToString(dr["t393_denominacion_CSN3P"]);
                    oNodo.t393_obligatorio_CSN3P = Convert.ToInt32(dr["t393_obligatorio_CSN3P"]);
                    oNodo.t394_denominacion_CSN4P = Convert.ToString(dr["t394_denominacion_CSN4P"]);
                    oNodo.t394_obligatorio_CSN4P = Convert.ToInt32(dr["t394_obligatorio_CSN4P"]);
                    oNodo.t303_margencesionprof = Convert.ToSingle(dr["t303_margencesionprof"]);
                    oNodo.t422_idmoneda = Convert.ToString(dr["t422_idmoneda"]);
                    oNodo.t422_denominacion = Convert.ToString(dr["t422_denominacion"]);

                    lst.Add(oNodo);

                }
                return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }

        internal Models.Nodo Select(Int32 t303_idnodo)
        {
            Models.Nodo oNodo = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t303_idnodo, t303_idnodo)
                };

                dr = cDblib.DataReader("SUP_NODO_S", dbparams);
                if (dr.Read())
                {
                    oNodo = new Models.Nodo();
                    oNodo.t303_ultcierreECO = Convert.ToInt32(dr["t303_ultcierreeco"]);
                    oNodo.t303_modelocostes = dr["t303_modelocostes"].ToString();
                    oNodo.t303_modelotarifas = dr["t303_modelotarifas"].ToString();
                    oNodo.t303_desglose = (bool)dr["t303_desglose"];
                    oNodo.t314_idusuario_responsable = Convert.ToInt32(dr["t314_idusuario_responsable"]);
                }
                return oNodo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }

        #endregion

        #region funciones privadas
        private SqlParameter Param(enumDBFields dbField, object value)
        {
            SqlParameter dbParam = null;
            string paramName = null;
            SqlDbType paramType = default(SqlDbType);
            int paramSize = 0;
            ParameterDirection paramDirection = ParameterDirection.Input;

            switch (dbField)
            {
                case enumDBFields.t303_idnodo:
                    paramName = "@t303_idnodo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                
            }


            dbParam = cDblib.dbParameter(paramName, paramType, paramSize);
            dbParam.Direction = paramDirection;
            if (paramDirection == ParameterDirection.Input | paramDirection == ParameterDirection.InputOutput) dbParam.Value = value;

            return dbParam;

        }

        #endregion

    }

}
