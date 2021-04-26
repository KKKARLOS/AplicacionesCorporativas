using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.ADM.SIC.Models;

/// <summary>
/// Summary description for AreaPreventa
/// </summary>

namespace IB.SUPER.ADM.SIC.DAL
{

    internal class AreaPreventa
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            ta200_idareapreventa = 1,
            ta200_denominacion = 2,
            ta200_estadoactiva = 3,
            t001_idficepi_responsable = 4,
            ta199_idunidadpreventa = 5,
            t331_idpt = 6
        }

        internal AreaPreventa(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un AreaPreventa
        /// </summary>
        internal int Insert(Models.AreaPreventa oAreaPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
					Param(enumDBFields.ta199_idunidadpreventa, oAreaPreventa.ta199_idunidadpreventa),
					Param(enumDBFields.ta200_denominacion, oAreaPreventa.ta200_denominacion),
					Param(enumDBFields.ta200_estadoactiva, oAreaPreventa.ta200_estadoactiva),
					Param(enumDBFields.t001_idficepi_responsable, oAreaPreventa.t001_idficepi_responsable),
					Param(enumDBFields.t331_idpt, oAreaPreventa.t331_idpt)
				};
                return (int)cDblib.ExecuteScalar("SIC_AREAPREVENTA_I", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un AreaPreventa a partir del id unidad de preventa y denominación
        /// </summary>
        internal Models.AreaPreventa SelectPorDenominacion(string ta200_denominacion)
        {
            Models.AreaPreventa oAreaPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {                    
                    Param(enumDBFields.ta200_denominacion, ta200_denominacion)
                };

                dr = cDblib.DataReader("SIC_AREAPREVENTA_SN", dbparams);
                if (dr.Read())
                {
                    oAreaPreventa = new Models.AreaPreventa();
                    oAreaPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);

                }
                return oAreaPreventa;

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
        /// Obtiene un AreaPreventa a partir del id
        /// </summary>
        internal Models.AreaPreventa Select(Int32 ta200_idareapreventa)
        {
            Models.AreaPreventa oAreaPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta200_idareapreventa, ta200_idareapreventa)
				};

                dr = cDblib.DataReader("SIC_AREAPREVENTA_S", dbparams);
                if (dr.Read())
                {
                    oAreaPreventa = new Models.AreaPreventa();
                    oAreaPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);
                    oAreaPreventa.ta200_denominacion = Convert.ToString(dr["ta200_denominacion"]);
                    oAreaPreventa.ta200_estadoactiva = Convert.ToBoolean(dr["ta200_estadoactiva"]);
                    oAreaPreventa.t001_idficepi_responsable = Convert.ToInt32(dr["t001_idficepi_responsable"]);
                    oAreaPreventa.ta199_idunidadpreventa = Convert.ToInt16(dr["ta199_idunidadpreventa"]);
                    if (!Convert.IsDBNull(dr["t331_idpt"]))
                        oAreaPreventa.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);

                }
                return oAreaPreventa;

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
        /// Obtiene un AreaPreventa a partir del id
        /// </summary>
        internal Models.AreaPreventa Select2(Int32 ta200_idareapreventa)
        {
            Models.AreaPreventa oAreaPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta200_idareapreventa, ta200_idareapreventa)
				};

                dr = cDblib.DataReader("SIC_AREAPREVENTA_S", dbparams);
                if (dr.Read())
                {
                    oAreaPreventa = new Models.AreaPreventa();
                    oAreaPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);
                    oAreaPreventa.ta200_denominacion = Convert.ToString(dr["ta200_denominacion"]);
                    oAreaPreventa.ta200_estadoactiva = Convert.ToBoolean(dr["ta200_estadoactiva"]);
                    oAreaPreventa.t001_idficepi_responsable = Convert.ToInt32(dr["t001_idficepi_responsable"]);
                    oAreaPreventa.ta199_idunidadpreventa = Convert.ToInt16(dr["ta199_idunidadpreventa"]);
                    if (!Convert.IsDBNull(dr["t331_idpt"]))
                        oAreaPreventa.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    oAreaPreventa.Responsable = Convert.ToString(dr["Responsable"]);
                }
                return oAreaPreventa;

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
        /// Actualiza un AreaPreventa a partir del id
        /// </summary>
        internal int Update(Models.AreaPreventa oAreaPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[6] {
					Param(enumDBFields.ta200_idareapreventa, oAreaPreventa.ta200_idareapreventa),
					Param(enumDBFields.ta200_denominacion, oAreaPreventa.ta200_denominacion),
					Param(enumDBFields.ta200_estadoactiva, oAreaPreventa.ta200_estadoactiva),
					Param(enumDBFields.t001_idficepi_responsable, oAreaPreventa.t001_idficepi_responsable),
					Param(enumDBFields.ta199_idunidadpreventa, oAreaPreventa.ta199_idunidadpreventa),
                    Param(enumDBFields.t331_idpt, oAreaPreventa.t331_idpt)				
            };

                return (int)cDblib.Execute("SIC_AREAPREVENTA_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un AreaPreventa a partir del id
        /// </summary>
        internal int Delete(Int32 ta200_idareapreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta200_idareapreventa, ta200_idareapreventa)
				};

                return (int)cDblib.Execute("SIC_AREAPREVENTA_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los AreaPreventa
        /// </summary>
        internal List<Models.AreaPreventa> Catalogo(Models.AreaPreventa oAreaPreventaFilter)
        {
            Models.AreaPreventa oAreaPreventa = null;
            List<Models.AreaPreventa> lst = new List<Models.AreaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
					Param(enumDBFields.ta200_denominacion, oAreaPreventaFilter.ta200_denominacion),
					Param(enumDBFields.ta200_estadoactiva, oAreaPreventaFilter.ta200_estadoactiva),
					Param(enumDBFields.t001_idficepi_responsable, oAreaPreventaFilter.t001_idficepi_responsable),
					Param(enumDBFields.ta199_idunidadpreventa, oAreaPreventaFilter.ta199_idunidadpreventa),
					Param(enumDBFields.t331_idpt, oAreaPreventaFilter.t331_idpt)
				};

                dr = cDblib.DataReader("SUPER.SIC_AreaPreventa_CAT", dbparams);
                while (dr.Read())
                {
                    oAreaPreventa = new Models.AreaPreventa();
                    oAreaPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);
                    oAreaPreventa.ta200_denominacion = Convert.ToString(dr["ta200_denominacion"]);
                    oAreaPreventa.ta200_estadoactiva = Convert.ToBoolean(dr["ta200_estadoactiva"]);
                    oAreaPreventa.t001_idficepi_responsable = Convert.ToInt32(dr["t001_idficepi_responsable"]);
                    oAreaPreventa.ta199_idunidadpreventa = Convert.ToInt16(dr["ta199_idunidadpreventa"]);
                    if (!Convert.IsDBNull(dr["t331_idpt"]))
                        oAreaPreventa.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);

                    lst.Add(oAreaPreventa);

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
                case enumDBFields.ta200_idareapreventa:
                    paramName = "@ta200_idareapreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta200_denominacion:
                    paramName = "@ta200_denominacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.ta200_estadoactiva:
                    paramName = "@ta200_estadoactiva";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.t001_idficepi_responsable:
                    paramName = "@t001_idficepi_responsable";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta199_idunidadpreventa:
                    paramName = "@ta199_idunidadpreventa";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 2;
                    break;
                case enumDBFields.t331_idpt:
                    paramName = "@t331_idpt";
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
