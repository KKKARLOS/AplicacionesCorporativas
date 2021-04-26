using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for SubareaPreventa
/// </summary>

namespace IB.SUPER.SIC.DAL
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
            ta201_asignacionlider = 4,
            ta200_idareapreventa = 5,
            t001_idficepi_responsable = 6,
            t001_idficepi = 7,
            filtro = 8,
            listaFiguras = 9,
            ta201_permitirautoasignacionlider = 10,
            actuocomoadministrador = 11,
            tablappl = 12,
            ta199_idunidadpreventa = 13

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

                dr = cDblib.DataReader("SIC_SUBAREAPREVENTA_S1", dbparams);
                if (dr.Read())
                {
                    oSubareaPreventa = new Models.SubareaPreventa();
                    oSubareaPreventa.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    oSubareaPreventa.ta201_denominacion = Convert.ToString(dr["ta201_denominacion"]);
                    oSubareaPreventa.ta201_estadoactiva = Convert.ToBoolean(dr["ta201_estadoactiva"]);
                    if (!Convert.IsDBNull(dr["ta201_permitirautoasignacionlider"]))
                        oSubareaPreventa.ta201_permitirautoasignacionlider = Convert.ToBoolean(dr["ta201_permitirautoasignacionlider"]);
                    oSubareaPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);
                    if (!Convert.IsDBNull(dr["t001_idficepi_responsable"]))
                        oSubareaPreventa.t001_idficepi_responsable = Convert.ToInt32(dr["t001_idficepi_responsable"]);
                    oSubareaPreventa.responsable = Convert.ToString(dr["Responsable"]);

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


        internal List<IB.SUPER.APP.Models.KeyValue> ObtenerAreasDeUnidad(BLL.Ayudas.enumAyuda enumlst, int t001_idficepi, int ta199_idunidadpreventa, bool admin)
        {
            IDataReader dr = null;
            List<IB.SUPER.APP.Models.KeyValue> lst = new List<IB.SUPER.APP.Models.KeyValue>();

            string nomproc = "";

            List<SqlParameter> dbparams = new List<SqlParameter> {
        		Param(enumDBFields.filtro, ""),
                Param(enumDBFields.ta199_idunidadpreventa, ta199_idunidadpreventa),
		    };

            switch (enumlst)
            {
                case BLL.Ayudas.enumAyuda.SIC_AYUDA1AREASPREVENTA_CAT:
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    dbparams.Add(Param(enumDBFields.t001_idficepi, t001_idficepi));
                    nomproc = "SIC_AYUDA1AREASPREVENTA_CAT";
                    break;

                case BLL.Ayudas.enumAyuda.SIC_AYUDA2AREASPREVENTA_CAT:
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    dbparams.Add(Param(enumDBFields.t001_idficepi, t001_idficepi));
                    nomproc = "SIC_AYUDA2AREASPREVENTA_CAT";
                    break;

                case BLL.Ayudas.enumAyuda.SIC_AYUDA3AREASPREVENTA_CAT:
                    nomproc = "SIC_AYUDA3AREASPREVENTA_CAT";
                    break;

                case BLL.Ayudas.enumAyuda.SIC_AYUDA4AREASPREVENTA_CAT:
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    dbparams.Add(Param(enumDBFields.t001_idficepi, t001_idficepi));
                    nomproc = "SIC_AYUDA4AREASPREVENTA_CAT";
                    break;

            }



            try
            {
                dr = cDblib.DataReader(nomproc, dbparams.ToArray());
                while (dr.Read())
                {
                    lst.Add(new IB.SUPER.APP.Models.KeyValue(Convert.ToInt32(dr["id"]), Convert.ToString(dr["value"])));
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

        internal List<IB.SUPER.APP.Models.KeyValue> ObtenerSubareasDeArea(BLL.Ayudas.enumAyuda enumlst, int t001_idficepi, int ta200_idareapreventa, bool admin)
        {
            IDataReader dr = null;
            List<IB.SUPER.APP.Models.KeyValue> lst = new List<IB.SUPER.APP.Models.KeyValue>();

            string nomproc = "";

            List<SqlParameter> dbparams = new List<SqlParameter> {
        		Param(enumDBFields.filtro, ""),
                Param(enumDBFields.ta200_idareapreventa, ta200_idareapreventa),
		    };

            switch (enumlst)
            {
                case BLL.Ayudas.enumAyuda.SIC_AYUDA1SUBAREASPREVENTA_CAT:
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    dbparams.Add(Param(enumDBFields.t001_idficepi, t001_idficepi));
                    nomproc = "SIC_AYUDA1SUBAREASPREVENTA_CAT";
                    break;

                case BLL.Ayudas.enumAyuda.SIC_AYUDA2SUBAREASPREVENTA_CAT:
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    dbparams.Add(Param(enumDBFields.t001_idficepi, t001_idficepi));
                    nomproc = "SIC_AYUDA2SUBAREASPREVENTA_CAT";
                    break;

                case BLL.Ayudas.enumAyuda.SIC_AYUDA3SUBAREASPREVENTA_CAT:
                    nomproc = "SIC_AYUDA3SUBAREASPREVENTA_CAT";
                    break;

                case BLL.Ayudas.enumAyuda.SIC_AYUDA4SUBAREASPREVENTA_CAT:
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    dbparams.Add(Param(enumDBFields.t001_idficepi, t001_idficepi));
                    nomproc = "SIC_AYUDA4SUBAREASPREVENTA_CAT";
                    break;
            }

            try
            {
                dr = cDblib.DataReader(nomproc, dbparams.ToArray());
                while (dr.Read())
                {
                    lst.Add(new IB.SUPER.APP.Models.KeyValue(Convert.ToInt32(dr["id"]), Convert.ToString(dr["value"])));
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

        //internal List<IB.SUPER.APP.Models.KeyValue> ObtenerAreasDeSubareasDeFicepi(int? t001_idficepi, string filtro, bool admin)
        //{
        //    IDataReader dr = null;
        //    List<IB.SUPER.APP.Models.KeyValue> lst = new List<IB.SUPER.APP.Models.KeyValue>();


        //    if (filtro.Trim().Length == 0) filtro = null;
        //    SqlParameter[] dbparams = new SqlParameter[3] {
        //        Param(enumDBFields.filtro, filtro),
        //        Param(enumDBFields.t001_idficepi, t001_idficepi),
        //        Param(enumDBFields.actuocomoadministrador, admin)
        //    };

        //    try
        //    {
        //        dr = cDblib.DataReader("SIC_GETAREASDESUBAREAYFICEPI_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            lst.Add(new IB.SUPER.APP.Models.KeyValue(Convert.ToInt32(dr["ta200_idareapreventa"]), Convert.ToString(dr["ta200_denominacion"])));
        //        }

        //        return lst;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dr != null)
        //        {
        //            if (!dr.IsClosed) dr.Close();
        //            dr.Dispose();
        //        }
        //    }
        //}

        //internal List<IB.SUPER.APP.Models.KeyValue> ObtenerSubareasPorAreaFicepi(int ta200_idareapreventa, int? t001_idficepi, string filtro, bool admin)
        //{
        //    IDataReader dr = null;
        //    List<IB.SUPER.APP.Models.KeyValue> lst = new List<IB.SUPER.APP.Models.KeyValue>();


        //    if (filtro.Trim().Length == 0) filtro = null;
        //    SqlParameter[] dbparams = new SqlParameter[4] {
        //        Param(enumDBFields.filtro, filtro),
        //        Param(enumDBFields.t001_idficepi, t001_idficepi),
        //        Param(enumDBFields.ta200_idareapreventa, ta200_idareapreventa),
        //        Param(enumDBFields.actuocomoadministrador, admin)
        //    };

        //    try
        //    {
        //        dr = cDblib.DataReader("SIC_GETSUBAREASPORAREAYFICEPI_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            lst.Add(new IB.SUPER.APP.Models.KeyValue(Convert.ToInt32(dr["ta201_idsubareapreventa"]), Convert.ToString(dr["ta201_denominacion"])));
        //        }

        //        return lst;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dr != null)
        //        {
        //            if (!dr.IsClosed) dr.Close();
        //            dr.Dispose();
        //        }
        //    }
        //}

        internal List<Models.SubareaPreventa> getFiguras_SubArea(Int32 ta201_idsubareapreventa)
        {
            Models.SubareaPreventa oSubAreaPreventa = null;
            List<Models.SubareaPreventa> lst = new List<Models.SubareaPreventa>();
            IDataReader dr = null;
            int idficepi = 0;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta201_idsubareapreventa, ta201_idsubareapreventa)
				};

                dr = cDblib.DataReader("SIC_GETFIGURAS_SUBAREA", dbparams);
                while (dr.Read())
                {
                    oSubAreaPreventa = new Models.SubareaPreventa();

                    oSubAreaPreventa.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    if (!Convert.IsDBNull(dr["ta203_figura"]))
                        oSubAreaPreventa.ta203_figura = Convert.ToString(dr["ta203_figura"]);
                    if (!Convert.IsDBNull(dr["profesional"]))
                        oSubAreaPreventa.profesional = Convert.ToString(dr["profesional"]);

                    idficepi = Convert.ToInt32(dr["t001_idficepi"]);

                    lst.Add(oSubAreaPreventa);
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

        internal List<Models.SubareaPreventa> getSubAreasByFicepi(Int32 ta200_idareapreventa, Int32 t001_idficepi, bool actuocomoadministrador)
        {
            Models.SubareaPreventa oSubAreaPreventa = null;
            List<Models.SubareaPreventa> lst = new List<Models.SubareaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.ta200_idareapreventa, ta200_idareapreventa),
                    Param(enumDBFields.t001_idficepi, t001_idficepi),
                    Param(enumDBFields.actuocomoadministrador, actuocomoadministrador)
				};

                dr = cDblib.DataReader("SIC_GETSUBAREASPORAREAYFICEPI_CAT", dbparams);
                while (dr.Read())
                {
                    oSubAreaPreventa = new Models.SubareaPreventa();
                    oSubAreaPreventa.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    oSubAreaPreventa.ta201_denominacion = Convert.ToString(dr["ta201_denominacion"]);
                    oSubAreaPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);
                    oSubAreaPreventa.responsable = Convert.ToString(dr["responsable"]);
                    oSubAreaPreventa.ta200_denominacion = Convert.ToString(dr["ta200_denominacion"]);
                    oSubAreaPreventa.accesoAdetalle = Convert.ToBoolean(dr["AccesoaDetalle"]);
                    oSubAreaPreventa.mantenimientoDeFiguras = Convert.ToBoolean(dr["MantenimientoDeFiguras"]);

                    lst.Add(oSubAreaPreventa);

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


        internal int grabarSubArea(Models.SubareaPreventa oSubArea)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[6] {
					Param(enumDBFields.ta201_idsubareapreventa , oSubArea.ta201_idsubareapreventa),															
                    Param(enumDBFields.ta201_denominacion, oSubArea.ta201_denominacion),					
                    Param(enumDBFields.ta201_estadoactiva, oSubArea.ta201_estadoactiva),	//hay que pasar null				
                    Param(enumDBFields.t001_idficepi_responsable, oSubArea.t001_idficepi_responsable),
                    Param(enumDBFields.ta201_permitirautoasignacionlider, oSubArea.ta201_permitirautoasignacionlider),
                    Param(enumDBFields.ta200_idareapreventa, null)//hay que pasar null
                    

				};
                return (int)cDblib.Execute("SIC_SUBAREAPREVENTA_U", dbparams);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal int grabarFigurasSubArea(int ta201_idsubareapreventa, DataTable listaFiguras)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.ta201_idsubareapreventa, ta201_idsubareapreventa),
                    Param(enumDBFields.listaFiguras, listaFiguras)
                    
                };

                return (int)cDblib.ExecuteScalar("SIC_SUBAREAPREVENTA_FIGURAS_IUD", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void grabarppldesubarea(int ta201_idsubareapreventa, DataTable tablappl)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.ta201_idsubareapreventa, ta201_idsubareapreventa),
                    Param(enumDBFields.tablappl, tablappl)
                    
                };

                cDblib.Execute("SIC_PPLDESUBAREA_U", dbparams);
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
                case enumDBFields.ta201_asignacionlider:
                    paramName = "@ta201_asignacionlider";
                    paramType = SqlDbType.Char;
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

                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.filtro:
                    paramName = "@filtro";
                    paramType = SqlDbType.VarChar;
                    paramSize = 750;
                    break;

                case enumDBFields.ta201_permitirautoasignacionlider:
                    paramName = "@ta201_permitirautoasignacionlider";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;

                case enumDBFields.listaFiguras:
                    paramName = "@TABLAFIGURAS";
                    paramType = SqlDbType.Structured;
                    paramSize = 0;
                    break;

                case enumDBFields.actuocomoadministrador:
                    paramName = "@actuocomoadministrador";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;

                case enumDBFields.tablappl:
                    paramName = "@TABLAPPL";
                    paramType = SqlDbType.Structured;
                    paramSize = 100;
                    break;

                case enumDBFields.ta199_idunidadpreventa:
                    paramName = "@ta199_idunidadpreventa";
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
