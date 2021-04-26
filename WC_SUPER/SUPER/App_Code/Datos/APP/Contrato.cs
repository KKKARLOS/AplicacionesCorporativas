using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using IB.SUPER.APP.Models;

namespace IB.SUPER.APP.DAL
{
    /// <summary>
    /// Descripción breve de Contrato
    /// </summary>
    internal class Contrato
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;

        private enum enumDBFields : byte
        {
            t306_idcontrato = 1
        }
        internal Contrato(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene los datos de un contrato
        /// </summary>
        internal List<Models.Contrato> Catalogo(int t306_idcontrato)
        {
            Models.Contrato oON = null;
            List<Models.Contrato> lst = new List<Models.Contrato>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t306_idcontrato, t306_idcontrato)
                };

                dr = cDblib.DataReader("SUP_CONTRATO_S2", dbparams);
                if (dr.Read())
                {
                    oON = new Models.Contrato();
                    oON.t306_idcontrato = Convert.ToInt32(dr["t306_idcontrato"]);
                    lst.Add(oON);

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
                case enumDBFields.t306_idcontrato:
                    paramName = "@t306_idcontrato";
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