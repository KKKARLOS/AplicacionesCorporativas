using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.APP.Models;

/// <summary>
/// Summary description for Accion
/// </summary>

namespace IB.SUPER.APP.DAL
{

    /// <summary>
    /// Descripción breve de LineaOferta
    /// </summary>
    internal class LineaOferta
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {

            bMostrarInactivos = 1
        }

        internal LineaOferta(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion
        internal List<Models.LineaOferta> Catalogo(bool bMostrarInactivos)
        {
            Models.LineaOferta oLO = null;
            List<Models.LineaOferta> lst = new List<Models.LineaOferta>();
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.bMostrarInactivos, bMostrarInactivos),
                };
                dr = cDblib.DataReader("SUP_GET_LINEASOFERTA", dbparams);
                while (dr.Read())
                {
                    oLO = new Models.LineaOferta();
                    oLO.indentacion = short.Parse(dr["indentacion"].ToString());
                    oLO.ta212_idorganizacioncomercial = Convert.ToInt32(dr["ta212_idorganizacioncomercial"]);
                    oLO.t195_idlineaoferta = Convert.ToInt32(dr["t195_idlineaoferta"]);
                    oLO.ta212_denominacion = Convert.ToString(dr["ta212_denominacion"]);
                    oLO.t195_denominacion = Convert.ToString(dr["t195_denominacion"]);
                    oLO.activo = bool.Parse(dr["activo"].ToString());

                    lst.Add(oLO);
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
                case enumDBFields.bMostrarInactivos:
                    paramName = "@bMostrarInactivos";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
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