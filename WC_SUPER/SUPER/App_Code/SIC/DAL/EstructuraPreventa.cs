using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using IB.SUPER.SIC.Models;

namespace IB.SUPER.SIC.DAL
{
    /// <summary>
    /// Descripción breve de EstructuraPreventa
    /// </summary>
    internal class EstructuraPreventa
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            MostrarInactivos = 1
        }

        internal EstructuraPreventa(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        internal List<Models.EstructuraPreventa> Arbol(bool bMostrarInactivos)
        {
            Models.EstructuraPreventa oElem = null;
            List<Models.EstructuraPreventa> lst = new List<Models.EstructuraPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.MostrarInactivos, bMostrarInactivos)
				};

                dr = cDblib.DataReader("SIC_GETESTRUCTURA_PREVENTA", dbparams);
                while (dr.Read())
                {
                    oElem = new Models.EstructuraPreventa();
                    oElem.unidad = Convert.ToInt16(dr["unidad"]);
                    oElem.area = Convert.ToInt32(dr["area"]);
                    oElem.subarea = Convert.ToInt32(dr["subarea"]);
                    oElem.estado = Convert.ToBoolean(dr["ESTADO"]);
                    oElem.indentacion = Convert.ToInt16(dr["INDENTACION"]);
                    oElem.denominacion = dr["DENOMINACION"].ToString();
                    lst.Add(oElem);

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
                case enumDBFields.MostrarInactivos:
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