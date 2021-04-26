using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for TipoAccionPreventa
/// </summary>

namespace IB.SUPER.SIC.DAL
{

    internal class TipoAccionPreventa
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            ta205_idtipoaccionpreventa = 1,
            ta205_denominacion = 2,
            ta205_origen_on = 3,
            ta205_origen_partida = 4,
            ta205_origen_super = 5,
            ta205_estadoactiva = 6
        }

        internal TipoAccionPreventa(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un TipoAccionPreventa
        /// </summary>
        internal int Insert(Models.TipoAccionPreventa oTipoAccionPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
					Param(enumDBFields.ta205_denominacion, oTipoAccionPreventa.ta205_denominacion),
					Param(enumDBFields.ta205_origen_on, oTipoAccionPreventa.ta205_origen_on),
					Param(enumDBFields.ta205_origen_partida, oTipoAccionPreventa.ta205_origen_partida),
					Param(enumDBFields.ta205_origen_super, oTipoAccionPreventa.ta205_origen_super),
					Param(enumDBFields.ta205_estadoactiva, oTipoAccionPreventa.ta205_estadoactiva)
				};

                return (int)cDblib.Execute("SUPER.SIC_TipoAccionPreventa_INS", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un TipoAccionPreventa a partir del id
        /// </summary>
        internal Models.TipoAccionPreventa Select(Int16 ta205_idtipoaccionpreventa)
        {
            Models.TipoAccionPreventa oTipoAccionPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta205_idtipoaccionpreventa, ta205_idtipoaccionpreventa)
				};

                dr = cDblib.DataReader("SUPER.SIC_TipoAccionPreventa_SEL", dbparams);
                if (dr.Read())
                {
                    oTipoAccionPreventa = new Models.TipoAccionPreventa();
                    oTipoAccionPreventa.ta205_idtipoaccionpreventa = Convert.ToInt16(dr["ta205_idtipoaccionpreventa"]);
                    oTipoAccionPreventa.ta205_denominacion = Convert.ToString(dr["ta205_denominacion"]);
                    oTipoAccionPreventa.ta205_origen_on = Convert.ToBoolean(dr["ta205_origen_on"]);
                    oTipoAccionPreventa.ta205_origen_partida = Convert.ToBoolean(dr["ta205_origen_partida"]);
                    oTipoAccionPreventa.ta205_origen_super = Convert.ToBoolean(dr["ta205_origen_super"]);
                    oTipoAccionPreventa.ta205_estadoactiva = Convert.ToBoolean(dr["ta205_estadoactiva"]);

                }
                return oTipoAccionPreventa;

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
        /// Actualiza un TipoAccionPreventa a partir del id
        /// </summary>
        internal int Update(Models.TipoAccionPreventa oTipoAccionPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[6] {
					Param(enumDBFields.ta205_idtipoaccionpreventa, oTipoAccionPreventa.ta205_idtipoaccionpreventa),
					Param(enumDBFields.ta205_denominacion, oTipoAccionPreventa.ta205_denominacion),
					Param(enumDBFields.ta205_origen_on, oTipoAccionPreventa.ta205_origen_on),
					Param(enumDBFields.ta205_origen_partida, oTipoAccionPreventa.ta205_origen_partida),
					Param(enumDBFields.ta205_origen_super, oTipoAccionPreventa.ta205_origen_super),
					Param(enumDBFields.ta205_estadoactiva, oTipoAccionPreventa.ta205_estadoactiva)
				};

                return (int)cDblib.Execute("SUPER.SIC_TipoAccionPreventa_UPD", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un TipoAccionPreventa a partir del id
        /// </summary>
        internal int Delete(Int16 ta205_idtipoaccionpreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta205_idtipoaccionpreventa, ta205_idtipoaccionpreventa)
				};

                return (int)cDblib.Execute("SUPER.SIC_TipoAccionPreventa_DEL", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los TipoAccionPreventa
        /// </summary>
        internal List<Models.TipoAccionPreventa> Catalogo(Models.TipoAccionPreventa oTipoAccionPreventaFilter)
        {
            Models.TipoAccionPreventa oTipoAccionPreventa = null;
            List<Models.TipoAccionPreventa> lst = new List<Models.TipoAccionPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
					Param(enumDBFields.ta205_denominacion, oTipoAccionPreventaFilter.ta205_denominacion),
					Param(enumDBFields.ta205_origen_on, oTipoAccionPreventaFilter.ta205_origen_on),
					Param(enumDBFields.ta205_origen_partida, oTipoAccionPreventaFilter.ta205_origen_partida),
					Param(enumDBFields.ta205_origen_super, oTipoAccionPreventaFilter.ta205_origen_super),
					Param(enumDBFields.ta205_estadoactiva, oTipoAccionPreventaFilter.ta205_estadoactiva)
				};

                dr = cDblib.DataReader("SUPER.SIC_TipoAccionPreventa_CAT", dbparams);
                while (dr.Read())
                {
                    oTipoAccionPreventa = new Models.TipoAccionPreventa();
                    oTipoAccionPreventa.ta205_idtipoaccionpreventa = Convert.ToInt16(dr["ta205_idtipoaccionpreventa"]);
                    oTipoAccionPreventa.ta205_denominacion = Convert.ToString(dr["ta205_denominacion"]);
                    oTipoAccionPreventa.ta205_origen_on = Convert.ToBoolean(dr["ta205_origen_on"]);
                    oTipoAccionPreventa.ta205_origen_partida = Convert.ToBoolean(dr["ta205_origen_partida"]);
                    oTipoAccionPreventa.ta205_origen_super = Convert.ToBoolean(dr["ta205_origen_super"]);
                    oTipoAccionPreventa.ta205_estadoactiva = Convert.ToBoolean(dr["ta205_estadoactiva"]);

                    lst.Add(oTipoAccionPreventa);

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
                case enumDBFields.ta205_idtipoaccionpreventa:
                    paramName = "@ta205_idtipoaccionpreventa";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 2;
                    break;
                case enumDBFields.ta205_denominacion:
                    paramName = "@ta205_denominacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.ta205_origen_on:
                    paramName = "@ta205_origen_on";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.ta205_origen_partida:
                    paramName = "@ta205_origen_partida";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.ta205_origen_super:
                    paramName = "@ta205_origen_super";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.ta205_estadoactiva:
                    paramName = "@ta205_estadoactiva";
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
