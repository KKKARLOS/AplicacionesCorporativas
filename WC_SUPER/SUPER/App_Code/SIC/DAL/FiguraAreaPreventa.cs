using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for FiguraAreaPreventa
/// </summary>

namespace IB.SUPER.SIC.DAL
{

    internal class FiguraAreaPreventa
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            ta200_idareapreventa = 1,
            t001_idficepi = 2,
            ta202_figura = 3
        }

        internal FiguraAreaPreventa(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los AreaPreventa
        /// </summary>
        /// <summary>
        /// Obtiene todos los FiguraAreaPreventa
        /// </summary>
        internal List<Models.FiguraAreaPreventa> ObtenerFigurasAreaUsuario(int ta200_idareapreventa, int t001_idficepi)
        {
            Models.FiguraAreaPreventa oFiguraAreaPreventa = null;
            List<Models.FiguraAreaPreventa> lst = new List<Models.FiguraAreaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.ta200_idareapreventa, ta200_idareapreventa),
					Param(enumDBFields.t001_idficepi, t001_idficepi)
				};

                dr = cDblib.DataReader("SIC_FIGURAAREAPREVENTA_S1", dbparams);
                while (dr.Read())
                {
                    oFiguraAreaPreventa = new Models.FiguraAreaPreventa();
                    oFiguraAreaPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);
                    oFiguraAreaPreventa.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oFiguraAreaPreventa.ta202_figura = Convert.ToString(dr["ta202_figura"]);

                    lst.Add(oFiguraAreaPreventa);

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
        /// Inserta un FiguraAreaPreventa
        /// </summary>
        internal int Insert(Models.FiguraAreaPreventa oFiguraAreaPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.ta200_idareapreventa, oFiguraAreaPreventa.ta200_idareapreventa),
					Param(enumDBFields.t001_idficepi, oFiguraAreaPreventa.t001_idficepi),
					Param(enumDBFields.ta202_figura, oFiguraAreaPreventa.ta202_figura)
				};

                return (int)cDblib.Execute("SUPER.SIC_FiguraAreaPreventa_INS", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un FiguraAreaPreventa a partir del id
        /// </summary>
        internal Models.FiguraAreaPreventa Select(Int32 ta200_idareapreventa, Int32 t001_idficepi)
        {
            Models.FiguraAreaPreventa oFiguraAreaPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.ta200_idareapreventa, ta200_idareapreventa),
					Param(enumDBFields.t001_idficepi, t001_idficepi)
				};

                dr = cDblib.DataReader("SUPER.SIC_FiguraAreaPreventa_SEL", dbparams);
                if (dr.Read())
                {
                    oFiguraAreaPreventa = new Models.FiguraAreaPreventa();
                    oFiguraAreaPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);
                    oFiguraAreaPreventa.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oFiguraAreaPreventa.ta202_figura = Convert.ToString(dr["ta202_figura"]);

                }
                return oFiguraAreaPreventa;

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
        /// Actualiza un FiguraAreaPreventa a partir del id
        /// </summary>
        internal int Update(Models.FiguraAreaPreventa oFiguraAreaPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.ta200_idareapreventa, oFiguraAreaPreventa.ta200_idareapreventa),
					Param(enumDBFields.t001_idficepi, oFiguraAreaPreventa.t001_idficepi),
					Param(enumDBFields.ta202_figura, oFiguraAreaPreventa.ta202_figura)
				};

                return (int)cDblib.Execute("SUPER.SIC_FiguraAreaPreventa_UPD", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un FiguraAreaPreventa a partir del id
        /// </summary>
        internal int Delete(Int32 ta200_idareapreventa, Int32 t001_idficepi)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.ta200_idareapreventa, ta200_idareapreventa),
					Param(enumDBFields.t001_idficepi, t001_idficepi)
				};

                return (int)cDblib.Execute("SUPER.SIC_FiguraAreaPreventa_DEL", dbparams);
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
                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta202_figura:
                    paramName = "@ta202_figura";
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
