using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for AccionPreventa
/// </summary>

namespace IB.SUPER.SIC.DAL
{

    internal class AyudaProfesionales
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t001_apellido1 = 1,
            t001_apellido2 = 2,
            t001_nombre = 3,
            ta201_idsubareapreventa,
            idred,
	        t000_codigo,
            t001_idficepi,
            actuocomoadministrador
        }

        internal AyudaProfesionales(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los AccionPreventa
        /// </summary>
        internal List<Models.ProfesionalSimple> FicepiGeneral(string t001_nombre, string t001_apellido1, string t001_apellido2)
        {
            Models.ProfesionalSimple oProf = null;
            List<Models.ProfesionalSimple> lst = new List<Models.ProfesionalSimple>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.t001_nombre, t001_nombre),
                    Param(enumDBFields.t001_apellido1, t001_apellido1),
                    Param(enumDBFields.t001_apellido2, t001_apellido2)
				};

                dr = cDblib.DataReader("SIC_GETFICEPI", dbparams);

                while (dr.Read())
                {
                    oProf = new Models.ProfesionalSimple();

                    oProf.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProf.profesional = Convert.ToString(dr["profesional"]);
                    oProf.t001_sexo = Convert.ToString(dr["t001_sexo"]);
                    oProf.nombreprofesional = Convert.ToString(dr["nombreprofesional"]);
                    oProf.nombreapellidosprofesional = Convert.ToString(dr["nombreapellidosprofesional"]);
                    oProf.correo_profesional = Convert.ToString(dr["correo_profesional"]);

                    lst.Add(oProf);

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
        /// Obtiene los lideres de un subarea
        /// </summary>
        internal List<Models.ProfesionalSimple> LideresSubarea(int ta201_idsubareapreventa)
        {
            Models.ProfesionalSimple oProf = null;
            List<Models.ProfesionalSimple> lst = new List<Models.ProfesionalSimple>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.ta201_idsubareapreventa, ta201_idsubareapreventa)
				};

                dr = cDblib.DataReader("SIC_GETPOSIBLESLIDERES_SUBAREA_CAT", dbparams);

                while (dr.Read())
                {
                    oProf = new Models.ProfesionalSimple();

                    oProf.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProf.profesional = Convert.ToString(dr["profesional"]);
                    oProf.t001_sexo = Convert.ToString(dr["t001_sexo"]);
                    oProf.nombreprofesional = Convert.ToString(dr["nombreprofesional"]);
                    oProf.nombreapellidosprofesional = Convert.ToString(dr["nombreapellidosprofesional"]);
                    oProf.correo_profesional = Convert.ToString(dr["correo_profesional"]);

                    lst.Add(oProf);

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
        /// Obtiene todos los posibles lideres de las subareas activas
        /// </summary>
        internal List<Models.ProfesionalSimple> Lideres(string proc, string t001_nombre, string t001_apellido1, string t001_apellido2)
        {
            Models.ProfesionalSimple oProf = null;
            List<Models.ProfesionalSimple> lst = new List<Models.ProfesionalSimple>();
            IDataReader dr = null;

            if (proc.Trim().Length == 0) proc = "SIC_GETLIDERES_CAT";

            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
					Param(enumDBFields.t001_nombre, t001_nombre),
                    Param(enumDBFields.t001_apellido1, t001_apellido1),
                    Param(enumDBFields.t001_apellido2, t001_apellido2),
                    Param(enumDBFields.t001_idficepi,(int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"]),
                };

                dr = cDblib.DataReader(proc, dbparams);

                while (dr.Read())
                {
                    oProf = new Models.ProfesionalSimple();

                    oProf.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProf.profesional = Convert.ToString(dr["profesional"]);
                    oProf.t001_sexo = Convert.ToString(dr["t001_sexo"]);
                    oProf.nombreprofesional = Convert.ToString(dr["nombreprofesional"]);
                    oProf.nombreapellidosprofesional = Convert.ToString(dr["nombreapellidosprofesional"]);
                    oProf.correo_profesional = Convert.ToString(dr["correo_profesional"]);

                    lst.Add(oProf);

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

        internal List<Models.ProfesionalSimple> LideresAmbitoVision(string t001_nombre, string t001_apellido1, string t001_apellido2, bool admin)
        {
            Models.ProfesionalSimple oProf = null;
            List<Models.ProfesionalSimple> lst = new List<Models.ProfesionalSimple>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(enumDBFields.t001_nombre, t001_nombre),
                    Param(enumDBFields.t001_apellido1, t001_apellido1),
                    Param(enumDBFields.t001_apellido2, t001_apellido2),
                    Param(enumDBFields.t001_idficepi,(int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"]),
                    Param(enumDBFields.actuocomoadministrador, admin),

                };

                dr = cDblib.DataReader("SIC_GET_HLP_LIDERES_PD", dbparams);

                while (dr.Read())
                {
                    oProf = new Models.ProfesionalSimple();

                    oProf.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProf.profesional = Convert.ToString(dr["profesional"]);
                    oProf.t001_sexo = Convert.ToString(dr["t001_sexo"]);
                    oProf.nombreprofesional = Convert.ToString(dr["nombreprofesional"]);
                    oProf.nombreapellidosprofesional = Convert.ToString(dr["nombreapellidosprofesional"]);
                    oProf.correo_profesional = Convert.ToString(dr["correo_profesional"]);

                    lst.Add(oProf);

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



        internal List<Models.ProfesionalSimple> LideresPreventaConAmbitoVision(string proc, string t001_nombre, string t001_apellido1, string t001_apellido2, bool admin)
        {
            Models.ProfesionalSimple oProf = null;
            List<Models.ProfesionalSimple> lst = new List<Models.ProfesionalSimple>();
            IDataReader dr = null;

            if (proc.Trim().Length == 0) proc = "";

            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(enumDBFields.t001_nombre, t001_nombre),
                    Param(enumDBFields.t001_apellido1, t001_apellido1),
                    Param(enumDBFields.t001_apellido2, t001_apellido2),
                    Param(enumDBFields.t001_idficepi,(int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"]),
                    Param(enumDBFields.actuocomoadministrador, admin)
                };

                dr = cDblib.DataReader(proc, dbparams);

                while (dr.Read())
                {
                    oProf = new Models.ProfesionalSimple();

                    oProf.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProf.profesional = Convert.ToString(dr["profesional"]);
                    oProf.t001_sexo = Convert.ToString(dr["t001_sexo"]);
                    oProf.nombreprofesional = Convert.ToString(dr["nombreprofesional"]);
                    oProf.nombreapellidosprofesional = Convert.ToString(dr["nombreapellidosprofesional"]);
                    oProf.correo_profesional = Convert.ToString(dr["correo_profesional"]);

                    lst.Add(oProf);

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
        /// Obtiene todos los AccionPreventa
        /// </summary>
        internal List<Models.ProfesionalSimple> Comerciales(string t001_nombre, string t001_apellido1, string t001_apellido2)
        {
            Models.ProfesionalSimple oProf = null;
            List<Models.ProfesionalSimple> lst = new List<Models.ProfesionalSimple>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.t001_nombre, t001_nombre),
                    Param(enumDBFields.t001_apellido1, t001_apellido1),
                    Param(enumDBFields.t001_apellido2, t001_apellido2)
				};

                dr = cDblib.DataReader("SIC_COMERCIALES_CAT", dbparams);

                while (dr.Read())
                {
                    oProf = new Models.ProfesionalSimple();

                    oProf.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProf.profesional = Convert.ToString(dr["profesional"]);
                    oProf.nombreprofesional = Convert.ToString(dr["nombreprofesional"]);
                    oProf.nombreapellidosprofesional = Convert.ToString(dr["nombreapellidosprofesional"]);

                    lst.Add(oProf);

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
        /// Obtiene todos los AccionPreventa
        /// </summary>
        internal List<Models.ProfesionalSimple> PromotoresAccion(string t001_nombre, string t001_apellido1, string t001_apellido2)
        {
            Models.ProfesionalSimple oProf = null;
            List<Models.ProfesionalSimple> lst = new List<Models.ProfesionalSimple>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.t001_nombre, t001_nombre),
                    Param(enumDBFields.t001_apellido1, t001_apellido1),
                    Param(enumDBFields.t001_apellido2, t001_apellido2)
				};

                dr = cDblib.DataReader("SIC_PROMOTORESACCION_CAT", dbparams);

                while (dr.Read())
                {
                    oProf = new Models.ProfesionalSimple();

                    oProf.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProf.profesional = Convert.ToString(dr["profesional"]);
                    oProf.nombreprofesional = Convert.ToString(dr["nombreprofesional"]);
                    oProf.nombreapellidosprofesional = Convert.ToString(dr["nombreapellidosprofesional"]);

                    lst.Add(oProf);

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


        internal List<Models.ProfesionalSimple> UsuariosSuper(string idred, int t000_codigo)
        {
            Models.ProfesionalSimple oProf = null;
            List<Models.ProfesionalSimple> lst = new List<Models.ProfesionalSimple>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.idred, idred),
                    Param(enumDBFields.t000_codigo, t000_codigo)
    
				};

                dr = cDblib.DataReader("SUP_ACCESOUSUARIO", dbparams);

                while (dr.Read()){}
                
                if (dr.NextResult()) {
                    while (dr.Read())
                    {
                        oProf = new Models.ProfesionalSimple();

                        oProf.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
                        oProf.profesional = Convert.ToString(dr["profesional"]);
                        oProf.empresa = Convert.ToString(dr["Empresa"]);
                        if (!Convert.IsDBNull(dr["t303_denominacion"]))
                            oProf.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);

                        lst.Add(oProf);
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
                case enumDBFields.t001_apellido1:
                    paramName = "@t001_apellido1";
                    paramType = SqlDbType.VarChar;
                    paramSize = 25;
                    break;
                case enumDBFields.t001_apellido2:
                    paramName = "@t001_apellido2";
                    paramType = SqlDbType.VarChar;
                    paramSize = 25;
                    break;
                case enumDBFields.t001_nombre:
                    paramName = "@t001_nombre";
                    paramType = SqlDbType.VarChar;
                    paramSize = 20;
                    break;
                case enumDBFields.ta201_idsubareapreventa:
                    paramName = "@ta201_idsubareapreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.idred:
                    paramName = "@sIDRED";
                    paramType = SqlDbType.VarChar;
                    paramSize = 25;
                    break;

                case enumDBFields.t000_codigo:
                    paramName = "@T000_CODIGO";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
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
