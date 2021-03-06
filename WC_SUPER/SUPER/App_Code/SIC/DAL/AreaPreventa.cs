using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for AreaPreventa
/// </summary>

namespace IB.SUPER.SIC.DAL
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
            t331_idpt = 6,
            filtro = 7,
            t001_idficepi = 8,
            listaFigurasArea = 9, 
            actuocomoadministrador = 10
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

                dr = cDblib.DataReader("SIC_AREAPREVENTA_S1", dbparams);
                if (dr.Read())
                {
                    oAreaPreventa = new Models.AreaPreventa();
                    oAreaPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);
                    oAreaPreventa.ta200_denominacion = Convert.ToString(dr["ta200_denominacion"]);
                    oAreaPreventa.ta200_estadoactiva = Convert.ToBoolean(dr["ta200_estadoactiva"]);
                    if (!Convert.IsDBNull(dr["t001_idficepi_responsable"]))
                    oAreaPreventa.t001_idficepi_responsable = Convert.ToInt32(dr["t001_idficepi_responsable"]);
                    oAreaPreventa.ta199_idunidadpreventa = Convert.ToInt16(dr["ta199_idunidadpreventa"]);
                    if (!Convert.IsDBNull(dr["ta199_denominacion"]))
                        oAreaPreventa.ta199_denominacion = Convert.ToString(dr["ta199_denominacion"]);
                    if (!Convert.IsDBNull(dr["t331_idpt"]))
                        oAreaPreventa.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    if (!Convert.IsDBNull(dr["Responsable"]))
                        oAreaPreventa.responsable = Convert.ToString(dr["Responsable"]);

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

        internal List<Models.AreaPreventa> getFiguras_Area(Int32 ta200_idareapreventa)
        {
            Models.AreaPreventa oAreaPreventa = null;
            List<Models.AreaPreventa> lst = new List<Models.AreaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta200_idareapreventa, ta200_idareapreventa)
				};

                dr = cDblib.DataReader("SIC_GETFIGURAS_AREA", dbparams);
                while (dr.Read())
                {
                    oAreaPreventa = new Models.AreaPreventa();                    
                    oAreaPreventa.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    if (!Convert.IsDBNull(dr["ta202_figura"]))
                        oAreaPreventa.ta202_figura = Convert.ToString(dr["ta202_figura"]);
                    if (!Convert.IsDBNull(dr["profesional"]))
                        oAreaPreventa.profesional = Convert.ToString(dr["profesional"]);

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


        internal List<Models.AreaPreventa> getAreasByFicepi(int t001_idficepi, bool actuocomoadministrador)
        {
            Models.AreaPreventa oAreaPreventa = null;
            List<Models.AreaPreventa> lst = new List<Models.AreaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {					
					Param(enumDBFields.t001_idficepi, t001_idficepi),					
                    Param(enumDBFields.actuocomoadministrador, actuocomoadministrador)	
				};

                dr = cDblib.DataReader("SIC_GETAREASPORFICEPI_CAT", dbparams);
                while (dr.Read())
                {
                    oAreaPreventa = new Models.AreaPreventa();
                    oAreaPreventa.ta200_idareapreventa = Convert.ToInt32(dr["id"]);
                    oAreaPreventa.ta200_denominacion = Convert.ToString(dr["value"]);                                        
                    oAreaPreventa.responsable = Convert.ToString(dr["responsable"]);
                    //oAreaPreventa.ta199_idunidadpreventa = Convert.ToInt16(dr["ta199_idunidadpreventa"]);
                    oAreaPreventa.ta199_denominacion = Convert.ToString(dr["ta199_denominacion"]);                    
                    oAreaPreventa.accesoadetalle = Convert.ToBoolean(dr["accesoadetalle"]);

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

        internal List<Models.AreaPreventa> getareassubareasppl(int t001_idficepi)
        {
            Models.AreaPreventa oAreaPreventa = null;
            List<Models.AreaPreventa> lst = new List<Models.AreaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {					
					Param(enumDBFields.t001_idficepi, t001_idficepi)                    
				};

                dr = cDblib.DataReader("SIC_GETAREASSUBAREASPPL", dbparams);
                while (dr.Read())
                {
                    oAreaPreventa = new Models.AreaPreventa();
                    oAreaPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);
                    oAreaPreventa.ta200_denominacion = Convert.ToString(dr["ta200_denominacion"]);
                    oAreaPreventa.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    oAreaPreventa.ta201_denominacion = Convert.ToString(dr["ta201_denominacion"]);
                    
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


        internal List<Models.AreaPreventa> getpplporsubareaparaficepi (int t001_idficepi)
        {
            Models.AreaPreventa oAreaPreventa = null;
            List<Models.AreaPreventa> lst = new List<Models.AreaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {					
					Param(enumDBFields.t001_idficepi, t001_idficepi)                    
				};

                dr = cDblib.DataReader("SIC_GETPPLPORSUBAREAPARAFICEPI", dbparams);
                while (dr.Read())
                {
                    oAreaPreventa = new Models.AreaPreventa();                                        
                    oAreaPreventa.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    oAreaPreventa.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oAreaPreventa.profesional = Convert.ToString(dr["posiblelider"]);
                    
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


        internal int grabarArea(Models.AreaPreventa oArea)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
					Param(enumDBFields.ta200_idareapreventa , oArea.ta200_idareapreventa),															
                    Param(enumDBFields.ta200_denominacion, oArea.ta200_denominacion),					
                    Param(enumDBFields.ta200_estadoactiva, null),					
                    Param(enumDBFields.t001_idficepi_responsable, oArea.t001_idficepi_responsable),
                    Param(enumDBFields.ta199_idunidadpreventa, null)
                    

				};
                return (int)cDblib.Execute("SIC_AREAPREVENTA_U", dbparams);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal int grabarFigurasArea(int ta200_idareapreventa, DataTable listaFigurasArea)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.ta200_idareapreventa, ta200_idareapreventa),
                    Param(enumDBFields.listaFigurasArea, listaFigurasArea)
                    
                };

                return (int)cDblib.ExecuteScalar("SIC_AREAPREVENTA_FIGURAS_IUD", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
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

                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.filtro:
                    paramName = "@filtro";
                    paramType = SqlDbType.VarChar;
                    paramSize = 250;
                    break;

                case enumDBFields.listaFigurasArea:
                    paramName = "@TABLAFIGURAS";
                    paramType = SqlDbType.Structured;
                    paramSize = 0;
                    break;

                case enumDBFields.actuocomoadministrador:
                    paramName = "@actuocomoadministrador";
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
