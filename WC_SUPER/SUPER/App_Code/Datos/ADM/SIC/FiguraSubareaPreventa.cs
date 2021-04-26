using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.ADM.SIC.Models;

/// <summary>
/// Summary description for FiguraSubareaPreventa
/// </summary>

namespace IB.SUPER.ADM.SIC.DAL
{
    internal class FiguraSubareaPreventa
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;
        private enum enumDBFields : byte
        {
            ta201_idsubareapreventa = 1,
            t001_idficepi = 2,
            ta203_figura = 3,
            datatable=4
        }

        internal FiguraSubareaPreventa(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los FiguraSubareaPreventa
        /// </summary>
        internal List<Models.FiguraSubareaPreventa> ObtenerFigurasSubareaUsuario(int ta201_idsubareapreventa, int t001_idficepi)
        {
            Models.FiguraSubareaPreventa oFiguraSubareaPreventa = null;
            List<Models.FiguraSubareaPreventa> lst = new List<Models.FiguraSubareaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.ta201_idsubareapreventa, ta201_idsubareapreventa),
					Param(enumDBFields.t001_idficepi, t001_idficepi)
				};

                dr = cDblib.DataReader("SIC_FIGURASUBAREAPREVENTA_S1", dbparams);
                while (dr.Read())
                {
                    oFiguraSubareaPreventa = new Models.FiguraSubareaPreventa();
                    oFiguraSubareaPreventa.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    oFiguraSubareaPreventa.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oFiguraSubareaPreventa.ta203_figura = Convert.ToString(dr["ta203_figura"]);

                    lst.Add(oFiguraSubareaPreventa);

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
        /// Obtiene los posibles lideres de un subarea
        /// </summary>
        internal List<int> ObtenerLideresSubarea(int ta201_idsubareapreventa)
        {
            List<int> lst = new List<int>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.ta201_idsubareapreventa, ta201_idsubareapreventa)
				};

                dr = cDblib.DataReader("SIC_GETLIDERES_SUBAREA", dbparams);

                while (dr.Read())
                {
                    lst.Add(Convert.ToInt32(dr["t001_idficepi"]));
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
        /// Inserta un FiguraSubareaPreventa
        /// </summary>
        //internal int Insert(Models.FiguraSubareaPreventa oFiguraSubareaPreventa)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.ta201_idsubareapreventa, oFiguraSubareaPreventa.ta201_idsubareapreventa),
        //            Param(enumDBFields.t001_idficepi, oFiguraSubareaPreventa.t001_idficepi),
        //            Param(enumDBFields.ta203_figura, oFiguraSubareaPreventa.ta203_figura)
        //        };

        //        return (int)cDblib.Execute("SIC_SUBAREAPREVENTA_FIGURAS_I", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Obtiene un FiguraSubareaPreventa a partir del id
        /// </summary>
        internal Models.FiguraSubareaPreventa Select(Int32 ta201_idsubareapreventa, Int32 t001_idficepi, String ta203_figura)
        {
            Models.FiguraSubareaPreventa oFiguraSubareaPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.ta201_idsubareapreventa, ta201_idsubareapreventa),
					Param(enumDBFields.t001_idficepi, t001_idficepi),
					Param(enumDBFields.ta203_figura, ta203_figura)
				};

                dr = cDblib.DataReader("SUPER.SIC_FiguraSubareaPreventa_SEL", dbparams);
                if (dr.Read())
                {
                    oFiguraSubareaPreventa = new Models.FiguraSubareaPreventa();
                    oFiguraSubareaPreventa.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    oFiguraSubareaPreventa.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oFiguraSubareaPreventa.ta203_figura = Convert.ToString(dr["ta203_figura"]);

                }
                return oFiguraSubareaPreventa;

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
        /// Elimina un FiguraSubareaPreventa a partir del id
        /// </summary>
        //internal int Delete(Int32 ta201_idsubareapreventa, Int32 t001_idficepi)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[2] {
        //            Param(enumDBFields.ta201_idsubareapreventa, ta201_idsubareapreventa),
        //            Param(enumDBFields.t001_idficepi, t001_idficepi)
        //        };

        //        return (int)cDblib.Execute("SIC_SUBAREAPREVENTA_FIGURAS_USUARIO_D", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //internal int DeleteFigura(Int32 ta201_idsubareapreventa, Int32 t001_idficepi, String ta203_figura)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.ta201_idsubareapreventa, ta201_idsubareapreventa),
        //            Param(enumDBFields.t001_idficepi, t001_idficepi),
        //            Param(enumDBFields.ta203_figura, ta203_figura)
        //        };

        //        return (int)cDblib.Execute("SIC_SUBAREAPREVENTA_FIGURAS_D", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        internal void ActualizarFiguras(Int32 ta201_idsubareapreventa, DataTable dtFiguras)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.ta201_idsubareapreventa, ta201_idsubareapreventa),
                    Param(enumDBFields.datatable, dtFiguras)
				};

                cDblib.Execute("SIC_SUBAREAPREVENTA_FIGURAS_IUD", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los FiguraSubareaPreventa
        /// </summary>
        internal List<Models.FiguraSubareaPreventa> Catalogo(Models.FiguraSubareaPreventa oFiguraSubareaPreventaFilter)
        {
            Models.FiguraSubareaPreventa oFiguraSubareaPreventa = null;
            List<Models.FiguraSubareaPreventa> lst = new List<Models.FiguraSubareaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.ta201_idsubareapreventa, oFiguraSubareaPreventaFilter.ta201_idsubareapreventa),
					Param(enumDBFields.t001_idficepi, oFiguraSubareaPreventaFilter.t001_idficepi),
					Param(enumDBFields.ta203_figura, oFiguraSubareaPreventaFilter.ta203_figura)
				};

                dr = cDblib.DataReader("SUPER.SIC_FiguraSubareaPreventa_CAT", dbparams);
                while (dr.Read())
                {
                    oFiguraSubareaPreventa = new Models.FiguraSubareaPreventa();
                    oFiguraSubareaPreventa.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    oFiguraSubareaPreventa.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oFiguraSubareaPreventa.ta203_figura = Convert.ToString(dr["ta203_figura"]);

                    lst.Add(oFiguraSubareaPreventa);

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
        /// Obtiene todos los FiguraSubareaPreventa
        /// </summary>
        internal List<Models.FiguraSubareaPreventa> Catalogo(Int32 ta201_idsubareapreventa)
        {
            Models.FiguraSubareaPreventa oFiguraSubareaPreventa = null;
            List<Models.FiguraSubareaPreventa> lst = new List<Models.FiguraSubareaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta201_idsubareapreventa, ta201_idsubareapreventa),
				};

                dr = cDblib.DataReader("SIC_SUBAREAPREVENTA_FIGURAS_C", dbparams);
                while (dr.Read())
                {
                    oFiguraSubareaPreventa = new Models.FiguraSubareaPreventa();
                    oFiguraSubareaPreventa.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    oFiguraSubareaPreventa.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oFiguraSubareaPreventa.ta203_figura = Convert.ToString(dr["ta203_figura"]);

                    oFiguraSubareaPreventa.sexo = Convert.ToString(dr["t001_sexo"]);
                    oFiguraSubareaPreventa.tipoProf = Convert.ToString(dr["tipo"]);
                    oFiguraSubareaPreventa.profesional = Convert.ToString(dr["Profesional"]);
                    oFiguraSubareaPreventa.orden = Convert.ToInt32(dr["orden"]);

                    lst.Add(oFiguraSubareaPreventa);

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
                case enumDBFields.ta201_idsubareapreventa:
                    paramName = "@ta201_idsubareapreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta203_figura:
                    paramName = "@ta203_figura";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.datatable:
                    paramName = "@TABLAFIGURAS";
                    paramType = SqlDbType.Structured;
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
