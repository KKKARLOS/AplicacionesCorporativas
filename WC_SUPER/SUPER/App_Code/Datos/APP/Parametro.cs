using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using IB.SUPER.APP.Models;

/// <summary>
/// Summary description for Nodo
/// </summary>

namespace IB.SUPER.APP.DAL
{

    internal class Parametro
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            codTabla = 1
        }

        internal Parametro(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene el valor y la definición del primer parámetro de una tabla
        /// </summary>
        internal Models.Parametro GetDatos(int idTabla)
        {
            Models.Parametro oParam = new Models.Parametro();
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.codTabla, idTabla)
                };

                dr = cDblib.DataReader("PAR_LINEA_S", dbparams);
                if (dr.Read())
                {
                    //oParam.codTabla = idTabla;
                    oParam.valor = Convert.ToString(dr["t191_valor"]);
                    oParam.denominacion = Convert.ToString(dr["t191_denominacion"]);
                }
                return oParam;

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
        /// <summary>
        /// Obtiene una lista de valores de parámetros de una tabla
        /// </summary>
        /// <param name="oPar"></param>
        /// <returns></returns>
        internal List<Models.Parametro> Catalogo(int idTabla)
        {
            Models.Parametro oNodo = null;
            List<Models.Parametro> lst = new List<Models.Parametro>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.codTabla, idTabla)
                };

                dr = cDblib.DataReader("PAR_LINEAS_C", dbparams);
                while (dr.Read())
                {
                    oNodo = new Models.Parametro();
                    oNodo.codTabla = idTabla;
                    oNodo.codParametro = Convert.ToInt32(dr["t191_valor"]);
                    oNodo.denominacion = Convert.ToString(dr["t191_denominacion"]);

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
                case enumDBFields.codTabla:
                    paramName = "@idParam";
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