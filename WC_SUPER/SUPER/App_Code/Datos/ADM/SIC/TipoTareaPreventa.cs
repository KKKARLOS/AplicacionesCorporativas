using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.ADM.SIC.Models;

/// <summary>
/// Summary description for TipoTareaPreventa
/// </summary>

namespace IB.SUPER.ADM.SIC.DAL
{

    internal class TipoTareaPreventa
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            ta219_idtipotareapreventa = 1,
            ta219_denominacion = 2,
            ta219_estadoactiva = 3,
            ta219_orden = 4,
            tablaTipoTareaPreventa = 5
    }

        internal TipoTareaPreventa(sqldblib.SqlServerSP extcDblib)
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
        /// grabar TiposTareaPreventa
        /// </summary>
        internal List<Models.TipoTareaPreventa> GrabarTareas(DataTable dtTipoTareas)
        {
            Models.TipoTareaPreventa oTipoTareaPreventa = null;
            List<Models.TipoTareaPreventa> lst = new List<Models.TipoTareaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.tablaTipoTareaPreventa, dtTipoTareas)
				};

                dr = cDblib.DataReader("SIC_TIPOTAREAPREVENTA_IUD", dbparams);                
                while (dr.Read())
                {
                    oTipoTareaPreventa = new Models.TipoTareaPreventa();
                    oTipoTareaPreventa.ta219_idtipotareapreventa = Convert.ToInt16(dr["ta219_idtipotareapreventa"]);
                    oTipoTareaPreventa.ta219_denominacion = Convert.ToString(dr["ta219_denominacion"]);
                    oTipoTareaPreventa.ta219_estadoactiva = Convert.ToBoolean(dr["ta219_activa"]);

                    lst.Add(oTipoTareaPreventa);

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
        /// Obtiene un TipoTareaPreventa a partir del id
        /// </summary>
        internal Models.TipoTareaPreventa Select(Int16 ta219_idtipotareapreventa)
        {
            Models.TipoTareaPreventa oTipoTareaPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta219_idtipotareapreventa, ta219_idtipotareapreventa)
				};

                dr = cDblib.DataReader("SIC_TipoTareaPreventa_SEL", dbparams);
                if (dr.Read())
                {
                    oTipoTareaPreventa = new Models.TipoTareaPreventa();
                    oTipoTareaPreventa.ta219_idtipotareapreventa = Convert.ToInt16(dr["ta219_idtipotareapreventa"]);
                    oTipoTareaPreventa.ta219_denominacion = Convert.ToString(dr["ta219_denominacion"]);
                    oTipoTareaPreventa.ta219_estadoactiva = Convert.ToBoolean(dr["ta219_activa"]);

                }
                return oTipoTareaPreventa;

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
        /// Obtiene todos los TipoTareaPreventa
        /// </summary>
        internal List<Models.TipoTareaPreventa> Catalogo()
        {
            Models.TipoTareaPreventa oTipoTareaPreventa = null;
            List<Models.TipoTareaPreventa> lst = new List<Models.TipoTareaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] { };

                dr = cDblib.DataReader("SIC_TipoTareaPreventa_CAT", dbparams);
                while (dr.Read())
                {
                    oTipoTareaPreventa = new Models.TipoTareaPreventa();
                    oTipoTareaPreventa.ta219_idtipotareapreventa = Convert.ToInt16(dr["ta219_idtipotareapreventa"]);
                    oTipoTareaPreventa.ta219_denominacion = Convert.ToString(dr["ta219_denominacion"]);
                    oTipoTareaPreventa.ta219_estadoactiva = Convert.ToBoolean(dr["ta219_activa"]);

                    lst.Add(oTipoTareaPreventa);

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
                case enumDBFields.ta219_idtipotareapreventa:
                    paramName = "@ta219_idtipotareapreventa";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 2;
                    break;
                case enumDBFields.ta219_denominacion:
                    paramName = "@ta219_denominacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.tablaTipoTareaPreventa:
                    paramName = "@TABLA";
                    paramType = SqlDbType.Structured;
                    paramSize = -1;
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
