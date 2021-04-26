using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IB.SUPER.SIC.DAL
{

    internal class Listas
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;

        private enum enumDBFields : byte
        {
            t001_idficepi = 1,
            ta206_idsolicitudpreventa = 2,
            origen = 3,
            ta206_iditemorigen = 4,
            origenMenu = 5
        }

        internal Listas(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene la lista de clave-valor para la tabla correspondiente
        /// </summary>
        internal List<IB.SUPER.APP.Models.KeyValue> Select(string lista, int? filtrarPor)
        {

            IDataReader dr = null;
            List<IB.SUPER.APP.Models.KeyValue> lst = new List<IB.SUPER.APP.Models.KeyValue>();

            string nomproc = "";
            string nomkeyfield = "";
            string nomvaluefield = "";
           
            string paramName = "";
            SqlParameter dbParam = null;
            SqlParameter[] dbParams = null;

            switch (lista)
            {
                case "TA199_UNIDADPREVENTA":
                    nomproc = "SIC_UNIDADPREVENTA_C";
                    nomkeyfield = "ta199_idunidadpreventa";
                    nomvaluefield = "ta199_denominacion";
                    break;

                case "TA200_AREAPREVENTA":
                    nomproc = "SIC_AREAPREVENTA_C2";
                    nomkeyfield = "ta200_idareapreventa";
                    nomvaluefield = "ta200_denominacion";
                    if (filtrarPor != null) paramName = "ta199_idunidadpreventa";
                    break;

                case "TA201_SUBAREAPREVENTA":
                    nomproc = "SIC_SUBAREAPREVENTA_C2";
                    nomkeyfield = "ta201_idsubareapreventa";
                    nomvaluefield = "ta201_denominacion";
                    if (filtrarPor != null) paramName = "ta200_idareapreventa";
                    break;

                case "TA205_TIPOACCIONPREVENTA":
                    nomproc = "SIC_TIPOACCIONPREVENTA_C";
                    nomkeyfield = "ta205_idtipoaccionpreventa";
                    nomvaluefield = "ta205_denominacion";
                    switch (filtrarPor)
                    {
                        case 0: paramName = "ta205_origen_on"; break;
                        case 1: paramName = "ta205_origen_partida"; break;
                        case 2: paramName = "ta205_origen_super"; break;
                    }
                    filtrarPor = 1;
                    break;
                case "TA211_TIPODOCUMENTO":
                    nomproc = "SIC_TIPODOCUMENTO_C";
                    nomkeyfield = "ta211_idtipodocumento";
                    nomvaluefield = "ta211_denominacion";
                    break;
            }

            if (paramName == "")
            {
                dbParams = new SqlParameter[0];
            }
            else
            {
                dbParam = cDblib.dbParameter(paramName, SqlDbType.Int, 4);
                dbParam.Direction = ParameterDirection.Input;
                dbParam.Value = filtrarPor;
                dbParams = new SqlParameter[1] { dbParam };
            }

            try
            {
                dr = cDblib.DataReader(nomproc, dbParams);
                if (lista == "TA201_SUBAREAPREVENTA")
                {
                    while (dr.Read())
                    {
                        lst.Add(new IB.SUPER.APP.Models.KeyValue(Convert.ToInt32(dr[nomkeyfield]), Convert.ToString(dr[nomvaluefield]), Convert.ToBoolean(dr["ta201_obligalider"])));
                    }
                }
                else {
                    while (dr.Read())
                    {
                        lst.Add(new IB.SUPER.APP.Models.KeyValue(Convert.ToInt32(dr[nomkeyfield]), Convert.ToString(dr[nomvaluefield])));
                    }
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
        
        internal List<Models.Estructura> SelectEstructura(int t001_idficepi,string origenMenu)
        {

            IDataReader dr = null;
            List<Models.Estructura> lst = new List<Models.Estructura>();
            Models.Estructura oEst = null;


            SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.t001_idficepi, t001_idficepi),
                    Param(enumDBFields.origenMenu, origenMenu)
            };


            try
            {
                dr = cDblib.DataReader("SIC_GETESTRUCTURA_PREVENTA_PARA_INSERCCIONES", dbparams);
                while (dr.Read())
                {
                    oEst = new Models.Estructura();
                    oEst.ta199_idunidadpreventa = Convert.ToInt32(dr["ta199_idunidadpreventa"]);
                    oEst.ta199_denominacion = Convert.ToString(dr["ta199_denominacion"]);
                    oEst.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);
                    oEst.ta200_denominacion = Convert.ToString(dr["ta200_denominacion"]);
                    oEst.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    oEst.ta201_denominacion = Convert.ToString(dr["ta201_denominacion"]);
                    if (!Convert.IsDBNull(dr["ta201_obligalider"]))
                        oEst.ta201_obligalider = Convert.ToBoolean(dr["ta201_obligalider"]);

                    lst.Add(oEst);
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

        internal List<Models.TipoAccionPreventa> SelectTipoAccionPreventa()
        {
            IDataReader dr = null;
            List<Models.TipoAccionPreventa> lst = new List<Models.TipoAccionPreventa>();
            Models.TipoAccionPreventa o;

            try
            {
                dr = cDblib.DataReader("SIC_TIPOACCIONPREVENTA_C", null);

                while (dr.Read())
                {
                    o = new Models.TipoAccionPreventa();
                    o.ta205_idtipoaccionpreventa = Convert.ToInt16(dr["ta205_idtipoaccionpreventa"]);
                    o.ta205_denominacion = Convert.ToString(dr["ta205_denominacion"]);
                    if (!Convert.IsDBNull(dr["ta205_plazominreq"])) o.ta205_plazominreq = Convert.ToInt16(dr["ta205_plazominreq"]);
                    lst.Add(o);
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
        internal List<Models.TipoAccionPreventa> SelectTipoAccionFiltrada(string itemorigen, int ta206_iditemorigen)
        {
            IDataReader dr = null;
            List<Models.TipoAccionPreventa> lst = new List<Models.TipoAccionPreventa>();
            Models.TipoAccionPreventa o;

            if (itemorigen == "E") itemorigen = "O"; //Las extensiones se tratan como oportunidades

            SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.ta206_iditemorigen, ta206_iditemorigen),
                    Param(enumDBFields.origen, itemorigen)
			};

            try
            {
                dr = cDblib.DataReader("SIC_TIPOACCIONPREVENTA_C2", dbparams);

                while (dr.Read())
                {
                    o = new Models.TipoAccionPreventa();
                    o.ta205_idtipoaccionpreventa = Convert.ToInt16(dr["ta205_idtipoaccionpreventa"]);
                    o.ta205_denominacion = Convert.ToString(dr["ta205_denominacion"]);
                    if (!Convert.IsDBNull(dr["ta205_plazominreq"]))  o.ta205_plazominreq = Convert.ToInt16(dr["ta205_plazominreq"]);
                    lst.Add(o);
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

        internal List<Models.SubareaPreventa> GetListSubareas()
        {
            IDataReader dr = null;
            List<Models.SubareaPreventa> lst = new List<Models.SubareaPreventa>();
            Models.SubareaPreventa o;

            try
            {
                dr = cDblib.DataReader("SIC_SUBAREAPREVENTA_C3", null);

                while (dr.Read())
                {
                    o = new Models.SubareaPreventa();
                    o.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    o.ta201_denominacion = Convert.ToString(dr["ta201_denominacion"]);
                    o.responsable = Convert.ToString(dr["responsable"]);

                    lst.Add(o);
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
                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta206_idsolicitudpreventa:
                    paramName = "@ta206_idsolicitudpreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.origen:
                    paramName = "@origen";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.ta206_iditemorigen:
                    paramName = "@ta206_iditemorigen";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.origenMenu:
                    paramName = "@origenMenu";
                    paramType = SqlDbType.Char;
                    paramSize = 3;
                    break;
            }


            dbParam = cDblib.dbParameter(paramName, paramType, paramSize);
            dbParam.Direction = paramDirection;
            if (paramDirection == ParameterDirection.Input | paramDirection == ParameterDirection.InputOutput) dbParam.Value = value;

            return dbParam;

        }
    }
        #endregion
}
