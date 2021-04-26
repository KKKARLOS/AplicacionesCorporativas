using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.ADM.SIC.Models;

/// <summary>
/// Summary description for SubareaPreventa
/// </summary>

namespace IB.SUPER.ADM.SIC.DAL
{

    internal class SubareaPreventa
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            ta201_idsubareapreventa = 1,
            ta201_denominacion = 2,
            ta201_estadoactiva = 3,
            //ta201_asignacionlider = 4,
            ta201_permitirautoasignacionlider = 4,
            ta200_idareapreventa = 5,
            t001_idficepi_responsable = 6
        }

        internal SubareaPreventa(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un SubareaPreventa
        /// </summary>
        internal int Insert(Models.SubareaPreventa oSubareaPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
					Param(enumDBFields.ta201_denominacion, oSubareaPreventa.ta201_denominacion),
					Param(enumDBFields.ta201_estadoactiva, oSubareaPreventa.ta201_estadoactiva),
					//Param(enumDBFields.ta201_asignacionlider, oSubareaPreventa.ta201_asignacionlider),
                    Param(enumDBFields.ta201_permitirautoasignacionlider, oSubareaPreventa.ta201_permitirautoasignacionlider),
					Param(enumDBFields.ta200_idareapreventa, oSubareaPreventa.ta200_idareapreventa),
					Param(enumDBFields.t001_idficepi_responsable, oSubareaPreventa.t001_idficepi_responsable)
				};

                return (int)cDblib.ExecuteScalar("SIC_SUBAREAPREVENTA_I", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un SubareaPreventa a partir del id
        /// </summary>
        internal Models.SubareaPreventa SelectPorDenominacion(string ta201_denominacion, Int32 ta200_idareapreventa)
        {
            Models.SubareaPreventa oSubareaPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.ta201_denominacion, ta201_denominacion),
                    Param(enumDBFields.ta200_idareapreventa, ta200_idareapreventa)
                };

                dr = cDblib.DataReader("SIC_SUBAREAPREVENTA_SN", dbparams);
                if (dr.Read())
                {
                    oSubareaPreventa = new Models.SubareaPreventa();
                    oSubareaPreventa.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);

                }
                return oSubareaPreventa;

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
        /// Obtiene un SubareaPreventa a partir del id
        /// </summary>
        internal Models.SubareaPreventa Select2(Int32 ta201_idsubareapreventa)
        {
            Models.SubareaPreventa oSubareaPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta201_idsubareapreventa, ta201_idsubareapreventa)
				};

                dr = cDblib.DataReader("SIC_SUBAREAPREVENTA_S", dbparams);
                if (dr.Read())
                {
                    oSubareaPreventa = new Models.SubareaPreventa();
                    oSubareaPreventa.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    oSubareaPreventa.ta201_denominacion = Convert.ToString(dr["ta201_denominacion"]);
                    oSubareaPreventa.ta201_estadoactiva = Convert.ToBoolean(dr["ta201_estadoactiva"]);
                    //oSubareaPreventa.ta201_asignacionlider = Convert.ToString(dr["ta201_asignacionlider"]);
                    oSubareaPreventa.ta201_permitirautoasignacionlider = Convert.ToBoolean(dr["ta201_permitirautoasignacionlider"]);
                    oSubareaPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);
                    oSubareaPreventa.t001_idficepi_responsable = Convert.ToInt32(dr["t001_idficepi_responsable"]);
                    oSubareaPreventa.Responsable = Convert.ToString(dr["Responsable"]);

                    oSubareaPreventa.ta199_idunidadpreventa = Convert.ToInt16(dr["ta199_idunidadpreventa"]);
                    oSubareaPreventa.ta199_denominacion = Convert.ToString(dr["ta199_denominacion"]);

                    oSubareaPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);
                    oSubareaPreventa.ta200_denominacion = Convert.ToString(dr["ta200_denominacion"]);

                }
                return oSubareaPreventa;

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
        /// Actualiza un SubareaPreventa a partir del id
        /// </summary>
        internal int Update(Models.SubareaPreventa oSubareaPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[6] {
					Param(enumDBFields.ta201_idsubareapreventa, oSubareaPreventa.ta201_idsubareapreventa),
					Param(enumDBFields.ta201_denominacion, oSubareaPreventa.ta201_denominacion),
					Param(enumDBFields.ta201_estadoactiva, oSubareaPreventa.ta201_estadoactiva),
					//Param(enumDBFields.ta201_asignacionlider, oSubareaPreventa.ta201_asignacionlider),
                    Param(enumDBFields.ta201_permitirautoasignacionlider, oSubareaPreventa.ta201_permitirautoasignacionlider),
					Param(enumDBFields.ta200_idareapreventa, oSubareaPreventa.ta200_idareapreventa),
					Param(enumDBFields.t001_idficepi_responsable, oSubareaPreventa.t001_idficepi_responsable)
				};

                return (int)cDblib.Execute("SIC_SUBAREAPREVENTA_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un SubareaPreventa a partir del id
        /// </summary>
        internal int Delete(Int32 ta201_idsubareapreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta201_idsubareapreventa, ta201_idsubareapreventa)
				};

                return (int)cDblib.Execute("SIC_SUBAREAPREVENTA_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los SubareaPreventa
        /// </summary>
        internal List<Models.SubareaPreventa> Catalogo(Models.SubareaPreventa oSubareaPreventaFilter)
        {
            Models.SubareaPreventa oSubareaPreventa = null;
            List<Models.SubareaPreventa> lst = new List<Models.SubareaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[6] {
					Param(enumDBFields.ta200_idareapreventa, oSubareaPreventaFilter.ta200_idareapreventa),
					Param(enumDBFields.ta201_idsubareapreventa, oSubareaPreventaFilter.ta201_idsubareapreventa),
					Param(enumDBFields.ta201_denominacion, oSubareaPreventaFilter.ta201_denominacion),
					Param(enumDBFields.ta201_estadoactiva, oSubareaPreventaFilter.ta201_estadoactiva),
					//Param(enumDBFields.ta201_asignacionlider, oSubareaPreventaFilter.ta201_asignacionlider),
                    Param(enumDBFields.ta201_permitirautoasignacionlider, oSubareaPreventa.ta201_permitirautoasignacionlider),
					Param(enumDBFields.t001_idficepi_responsable, oSubareaPreventaFilter.t001_idficepi_responsable)
				};

                dr = cDblib.DataReader("SIC_SUBAREAPREVENTA_C", dbparams);
                while (dr.Read())
                {
                    oSubareaPreventa = new Models.SubareaPreventa();
                    oSubareaPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);
                    oSubareaPreventa.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    oSubareaPreventa.ta201_denominacion = Convert.ToString(dr["ta201_denominacion"]);
                    oSubareaPreventa.ta201_estadoactiva = Convert.ToBoolean(dr["ta201_estadoactiva"]);
                    //oSubareaPreventa.ta201_asignacionlider = Convert.ToString(dr["ta201_asignacionlider"]);
                    oSubareaPreventa.ta201_permitirautoasignacionlider = Convert.ToBoolean(dr["ta201_permitirautoasignacionlider"]);
                    oSubareaPreventa.t001_idficepi_responsable = Convert.ToInt32(dr["t001_idficepi_responsable"]);

                    lst.Add(oSubareaPreventa);

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

        /// <summary>
        /// Obtiene un SubareaPreventa a partir del id
        /// </summary>
        internal Models.SubareaPreventa Select(Int32 ta201_idsubareapreventa)
        {
            Models.SubareaPreventa oSubareaPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta201_idsubareapreventa, ta201_idsubareapreventa)
				};

                dr = cDblib.DataReader("SIC_SUBAREAPREVENTA_S", dbparams);
                if (dr.Read())
                {
                    oSubareaPreventa = new Models.SubareaPreventa();
                    oSubareaPreventa.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    oSubareaPreventa.ta201_denominacion = Convert.ToString(dr["ta201_denominacion"]);
                    oSubareaPreventa.ta201_estadoactiva = Convert.ToBoolean(dr["ta201_estadoactiva"]);
                    oSubareaPreventa.ta201_permitirautoasignacionlider = Convert.ToBoolean(dr["ta201_permitirautoasignacionlider"]);
                    oSubareaPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);
                    oSubareaPreventa.t001_idficepi_responsable = Convert.ToInt32(dr["t001_idficepi_responsable"]);

                }
                return oSubareaPreventa;

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
                case enumDBFields.ta201_idsubareapreventa:
                    paramName = "@ta201_idsubareapreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta201_denominacion:
                    paramName = "@ta201_denominacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 75;
                    break;
                case enumDBFields.ta201_estadoactiva:
                    paramName = "@ta201_estadoactiva";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                //case enumDBFields.ta201_asignacionlider:
                //    paramName = "@ta201_asignacionlider";
                //    paramType = SqlDbType.Char;
                //    paramSize = 1;
                //    break;
                case enumDBFields.ta201_permitirautoasignacionlider:
                    paramName = "@ta201_permitirautoasignacionlider";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.ta200_idareapreventa:
                    paramName = "@ta200_idareapreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t001_idficepi_responsable:
                    paramName = "@t001_idficepi_responsable";
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
