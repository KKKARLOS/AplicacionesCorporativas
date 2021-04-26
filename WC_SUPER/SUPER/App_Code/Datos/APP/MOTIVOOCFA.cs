using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using IB.SUPER.APP.Models;

/// <summary>
/// Summary description for MOTIVOOCFA
/// </summary>

namespace IB.SUPER.APP.DAL
{
    internal class MOTIVOOCFA
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t840_idmotivo = 1,
            t820_tipo = 2,
            t840_descripcion = 3
        }

        internal MOTIVOOCFA(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un MOTIVOOCFA
        /// </summary>
        internal int Insert(Models.MOTIVOOCFA oMOTIVOOCFA)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t820_tipo, oMOTIVOOCFA.t820_tipo),
                    Param(enumDBFields.t840_descripcion, oMOTIVOOCFA.t840_descripcion)
                };

                return (int)cDblib.Execute("SUP_MOTIVOOCFA_INS", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un MOTIVOOCFA a partir del id
        /// </summary>
        internal Models.MOTIVOOCFA Select(Int32 t840_idmotivo)
        {
            Models.MOTIVOOCFA oMOTIVOOCFA = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t840_idmotivo, t840_idmotivo)
                };

                dr = cDblib.DataReader("SUP_MOTIVOOCFA_SEL", dbparams);
                if (dr.Read())
                {
                    oMOTIVOOCFA = new Models.MOTIVOOCFA();
                    oMOTIVOOCFA.t840_idmotivo = Convert.ToInt32(dr["t840_idmotivo"]);
                    oMOTIVOOCFA.t820_tipo = Convert.ToString(dr["t820_tipo"]);
                    oMOTIVOOCFA.t840_descripcion = Convert.ToString(dr["t840_descripcion"]);

                }
                return oMOTIVOOCFA;

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
        /// Actualiza un MOTIVOOCFA a partir del id
        /// </summary>
        internal int Update(Models.MOTIVOOCFA oMOTIVOOCFA)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t840_idmotivo, oMOTIVOOCFA.t840_idmotivo),
                    Param(enumDBFields.t820_tipo, oMOTIVOOCFA.t820_tipo),
                    Param(enumDBFields.t840_descripcion, oMOTIVOOCFA.t840_descripcion)
                };

                return (int)cDblib.Execute("SUP_MOTIVOOCFA_UPD", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un MOTIVOOCFA a partir del id
        /// </summary>
        internal int Delete(Int32 t840_idmotivo)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t840_idmotivo, t840_idmotivo)
                };

                return (int)cDblib.Execute("SUP_MOTIVOOCFA_DEL", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los MOTIVOOCFA
        /// </summary>
        internal List<Models.MOTIVOOCFA> Catalogo(Models.MOTIVOOCFA oMOTIVOOCFAFilter)
        {
            Models.MOTIVOOCFA oMOTIVOOCFA = null;
            List<Models.MOTIVOOCFA> lst = new List<Models.MOTIVOOCFA>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t820_tipo, oMOTIVOOCFAFilter.t820_tipo)
                };

                dr = cDblib.DataReader("SUP_MOTIVOOCFA_CAT", dbparams);
                while (dr.Read())
                {
                    oMOTIVOOCFA = new Models.MOTIVOOCFA();
                    oMOTIVOOCFA.t840_idmotivo = Convert.ToInt32(dr["t840_idmotivo"]);
                    oMOTIVOOCFA.t820_tipo = Convert.ToString(dr["t820_tipo"]);
                    oMOTIVOOCFA.t840_descripcion = Convert.ToString(dr["t840_descripcion"]);
                    oMOTIVOOCFA.desTipo= Convert.ToString(dr["desTipo"]);

                    lst.Add(oMOTIVOOCFA);

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
                case enumDBFields.t840_idmotivo:
                    paramName = "@t840_idmotivo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t820_tipo:
                    paramName = "@t820_tipo";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.t840_descripcion:
                    paramName = "@t840_descripcion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 70;
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
