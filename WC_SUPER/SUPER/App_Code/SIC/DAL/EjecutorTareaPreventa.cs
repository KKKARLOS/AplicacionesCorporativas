using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for EjecutorTareaPreventa
/// </summary>

namespace IB.SUPER.SIC.DAL
{

    internal class EjecutorTareaPreventa
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            ta207_idtareapreventa = 1,
            t001_idficepi_ejecutor = 2,
            ta214_estado = 3
        }

        internal EjecutorTareaPreventa(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un EjecutorTareaPreventa
        /// </summary>
        internal int Insert(Models.EjecutorTareaPreventa oEjecutorTareaPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.ta207_idtareapreventa, oEjecutorTareaPreventa.ta207_idtareapreventa),
					Param(enumDBFields.t001_idficepi_ejecutor, oEjecutorTareaPreventa.t001_idficepi_ejecutor),
					Param(enumDBFields.ta214_estado, oEjecutorTareaPreventa.ta214_estado)
				};

                return (int)cDblib.Execute("SUPER.SIC_EjecutorTareaPreventa_INS", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un EjecutorTareaPreventa a partir del id
        /// </summary>
        internal Models.EjecutorTareaPreventa Select(Int32 ta207_idtareapreventa, Int32 t001_idficepi_ejecutor)
        {
            Models.EjecutorTareaPreventa oEjecutorTareaPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.ta207_idtareapreventa, ta207_idtareapreventa),
					Param(enumDBFields.t001_idficepi_ejecutor, t001_idficepi_ejecutor)
				};

                dr = cDblib.DataReader("SUPER.SIC_EjecutorTareaPreventa_SEL", dbparams);
                if (dr.Read())
                {
                    oEjecutorTareaPreventa = new Models.EjecutorTareaPreventa();
                    oEjecutorTareaPreventa.ta207_idtareapreventa = Convert.ToInt32(dr["ta207_idtareapreventa"]);
                    oEjecutorTareaPreventa.t001_idficepi_ejecutor = Convert.ToInt32(dr["t001_idficepi_ejecutor"]);
                    oEjecutorTareaPreventa.ta214_estado = Convert.ToString(dr["ta209_estado"]);

                }
                return oEjecutorTareaPreventa;

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
        /// Actualiza un EjecutorTareaPreventa a partir del id
        /// </summary>
        internal int Update(Models.EjecutorTareaPreventa oEjecutorTareaPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.ta207_idtareapreventa, oEjecutorTareaPreventa.ta207_idtareapreventa),
					Param(enumDBFields.t001_idficepi_ejecutor, oEjecutorTareaPreventa.t001_idficepi_ejecutor),
					Param(enumDBFields.ta214_estado, oEjecutorTareaPreventa.ta214_estado)
				};

                return (int)cDblib.Execute("SUPER.SIC_EjecutorTareaPreventa_UPD", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un EjecutorTareaPreventa a partir del id
        /// </summary>
        internal int Delete(Int32 ta207_idtareapreventa, Int32 t001_idficepi_ejecutor)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.ta207_idtareapreventa, ta207_idtareapreventa),
					Param(enumDBFields.t001_idficepi_ejecutor, t001_idficepi_ejecutor)
				};

                return (int)cDblib.Execute("SUPER.SIC_EjecutorTareaPreventa_DEL", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los EjecutorTareaPreventa
        /// </summary>
        internal List<Models.EjecutorTareaPreventa> Catalogo(Models.EjecutorTareaPreventa oEjecutorTareaPreventaFilter)
        {
            Models.EjecutorTareaPreventa oEjecutorTareaPreventa = null;
            List<Models.EjecutorTareaPreventa> lst = new List<Models.EjecutorTareaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta214_estado, oEjecutorTareaPreventaFilter.ta214_estado)
				};

                dr = cDblib.DataReader("SUPER.SIC_EjecutorTareaPreventa_CAT", dbparams);
                while (dr.Read())
                {
                    oEjecutorTareaPreventa = new Models.EjecutorTareaPreventa();
                    oEjecutorTareaPreventa.ta207_idtareapreventa = Convert.ToInt32(dr["ta207_idtareapreventa"]);
                    oEjecutorTareaPreventa.t001_idficepi_ejecutor = Convert.ToInt32(dr["t001_idficepi_ejecutor"]);
                    oEjecutorTareaPreventa.ta214_estado = Convert.ToString(dr["ta209_estado"]);

                    lst.Add(oEjecutorTareaPreventa);

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
                case enumDBFields.ta207_idtareapreventa:
                    paramName = "@ta207_idtareapreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t001_idficepi_ejecutor:
                    paramName = "@t001_idficepi_ejecutor";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta214_estado:
                    paramName = "@ta214_estado";
                    paramType = SqlDbType.Char;
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
