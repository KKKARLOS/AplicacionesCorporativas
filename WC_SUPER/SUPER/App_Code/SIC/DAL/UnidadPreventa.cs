using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for UnidadPreventa
/// </summary>

namespace IB.SUPER.SIC.DAL
{

    internal class UnidadPreventa
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            ta199_idunidadpreventa = 1,
            ta199_denominacion = 2,
            ta199_estadoactiva = 3
        }

        internal UnidadPreventa(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un UnidadPreventa
        /// </summary>
        internal int Insert(Models.UnidadPreventa oUnidadPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.ta199_denominacion, oUnidadPreventa.ta199_denominacion),
					Param(enumDBFields.ta199_estadoactiva, oUnidadPreventa.ta199_estadoactiva)
				};

                return (int)cDblib.Execute("SUPER.SIC_UnidadPreventa_INS", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un UnidadPreventa a partir del id
        /// </summary>
        internal Models.UnidadPreventa Select(Int16 ta199_idunidadpreventa)
        {
            Models.UnidadPreventa oUnidadPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta199_idunidadpreventa, ta199_idunidadpreventa)
				};

                dr = cDblib.DataReader("SUPER.SIC_UnidadPreventa_SEL", dbparams);
                if (dr.Read())
                {
                    oUnidadPreventa = new Models.UnidadPreventa();
                    oUnidadPreventa.ta199_idunidadpreventa = Convert.ToInt16(dr["ta199_idunidadpreventa"]);
                    oUnidadPreventa.ta199_denominacion = Convert.ToString(dr["ta199_denominacion"]);
                    oUnidadPreventa.ta199_estadoactiva = Convert.ToBoolean(dr["ta199_estadoactiva"]);

                }
                return oUnidadPreventa;

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
        /// Actualiza un UnidadPreventa a partir del id
        /// </summary>
        internal int Update(Models.UnidadPreventa oUnidadPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.ta199_idunidadpreventa, oUnidadPreventa.ta199_idunidadpreventa),
					Param(enumDBFields.ta199_denominacion, oUnidadPreventa.ta199_denominacion),
					Param(enumDBFields.ta199_estadoactiva, oUnidadPreventa.ta199_estadoactiva)
				};

                return (int)cDblib.Execute("SUPER.SIC_UnidadPreventa_UPD", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un UnidadPreventa a partir del id
        /// </summary>
        /// <summary>
        /// Elimina un UnidadPreventa a partir del id
        /// </summary>
        internal int Delete(Int16 ta199_idunidadpreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta199_idunidadpreventa, ta199_idunidadpreventa)
				};

                return (int)cDblib.Execute("SIC_UNIDADPREVENTA_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los UnidadPreventa
        /// </summary>
        internal List<Models.UnidadPreventa> Catalogo(Models.UnidadPreventa oUnidadPreventaFilter)
        {
            Models.UnidadPreventa oUnidadPreventa = null;
            List<Models.UnidadPreventa> lst = new List<Models.UnidadPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.ta199_denominacion, oUnidadPreventaFilter.ta199_denominacion),
					Param(enumDBFields.ta199_estadoactiva, oUnidadPreventaFilter.ta199_estadoactiva)
				};

                dr = cDblib.DataReader("SUPER.SIC_UnidadPreventa_CAT", dbparams);
                while (dr.Read())
                {
                    oUnidadPreventa = new Models.UnidadPreventa();
                    oUnidadPreventa.ta199_idunidadpreventa = Convert.ToInt16(dr["ta199_idunidadpreventa"]);
                    oUnidadPreventa.ta199_denominacion = Convert.ToString(dr["ta199_denominacion"]);
                    oUnidadPreventa.ta199_estadoactiva = Convert.ToBoolean(dr["ta199_estadoactiva"]);

                    lst.Add(oUnidadPreventa);

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
                case enumDBFields.ta199_idunidadpreventa:
                    paramName = "@ta199_idunidadpreventa";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 2;
                    break;
                case enumDBFields.ta199_denominacion:
                    paramName = "@ta199_denominacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.ta199_estadoactiva:
                    paramName = "@ta199_estadoactiva";
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
