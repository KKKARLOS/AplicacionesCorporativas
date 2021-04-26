using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Accion
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{    
    internal class Accion 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {

            t383_idaccion = 1,
			t382_idasunto = 2,
			t383_desaccion = 3,
            t383_desaccionlong = 4,
            t383_fcreacion = 5,
			t383_flimite = 6,
            t383_ffin = 7,
			t383_avance = 8,
            t383_obs = 9,
            t383_dpto = 10,
            t383_alerta = 11
        }

        internal Accion(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
        #region funciones publicas
        ///// <summary>
        /// Inserta un Accion
        /// </summary>
        internal int Insert(Models.Accion oAccion)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[10] {
                    Param(enumDBFields.t383_fcreacion, oAccion.t383_fcreacion),
                    Param(enumDBFields.t382_idasunto, oAccion.t382_idasunto),
                    Param(enumDBFields.t383_alerta, oAccion.t383_alerta),
                    Param(enumDBFields.t383_avance, oAccion.t383_avance),
                    Param(enumDBFields.t383_desaccion, oAccion.t383_desaccion),
                    Param(enumDBFields.t383_desaccionlong, oAccion.t383_desaccionlong),
                    Param(enumDBFields.t383_dpto, oAccion.t383_dpto),
                    Param(enumDBFields.t383_ffin, oAccion.t383_ffin),
                    Param(enumDBFields.t383_flimite, oAccion.t383_flimite),
                    Param(enumDBFields.t383_obs, oAccion.t383_obs),
                };

                return (int)cDblib.ExecuteScalar("[SUP_ACCION_I]", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un Accion a partir del id
        /// </summary>
        internal Models.Accion Select(Int32 t383_idaccion)
        {
            Models.Accion oAccion = null;
            IDataReader dr = null;

            SqlParameter[] dbparams = new SqlParameter[1] {
                Param(enumDBFields.t383_idaccion, t383_idaccion),
            };
            try
            {
                dr = cDblib.DataReader("SUP_ACCION_S", dbparams);
                if (dr.Read())
                {
                    oAccion = new Models.Accion();
                    oAccion.t383_fcreacion = Convert.ToDateTime(dr["T383_fcreacion"]);
                    oAccion.t382_idasunto = Convert.ToInt32(dr["T382_idasunto"]);
                    oAccion.t383_alerta = Convert.ToString(dr["T383_alerta"]);
                    oAccion.t383_avance = Convert.ToByte(dr["T383_avance"]);
                    oAccion.t383_desaccion = Convert.ToString(dr["T383_desaccion"]);
                    oAccion.t383_desaccionlong = Convert.ToString(dr["T383_desaccionlong"]);
                    oAccion.t383_dpto = Convert.ToString(dr["T383_dpto"]);

                    if (!Convert.IsDBNull(dr["T383_ffin"]))
                        oAccion.t383_ffin = Convert.ToDateTime(dr["T383_ffin"]);
                    if (!Convert.IsDBNull(dr["T383_flimite"]))
                        oAccion.t383_flimite = Convert.ToDateTime(dr["T383_flimite"]);
                    oAccion.t383_idaccion = Convert.ToInt32(dr["T383_idaccion"]);
                    oAccion.t383_obs = Convert.ToString(dr["T383_obs"]);
                }
                return oAccion;

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
        /// Actualiza un Accion a partir del id
        /// </summary>
        internal int Update(Models.Accion oAccion)
        {
            try
            {

                SqlParameter[] dbparams = new SqlParameter[9] {
                    Param(enumDBFields.t383_alerta, oAccion.t383_alerta),
                    Param(enumDBFields.t383_avance, oAccion.t383_avance),
                    Param(enumDBFields.t383_desaccion, oAccion.t383_desaccion),
                    Param(enumDBFields.t383_desaccionlong, oAccion.t383_desaccionlong),
                    Param(enumDBFields.t383_dpto, oAccion.t383_dpto),
                    Param(enumDBFields.t383_ffin, oAccion.t383_ffin),
                    Param(enumDBFields.t383_flimite, oAccion.t383_flimite),
                    Param(enumDBFields.t383_idaccion, oAccion.t383_idaccion),
                    Param(enumDBFields.t383_obs, oAccion.t383_obs),
                };

                return (int)cDblib.Execute("SUP_ACCION_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
        ///// <summary>
        ///// Elimina un Accion a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_Accion_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los Accion
        ///// </summary>
        //internal List<Models.Accion> Catalogo(Models.Accion oAccionFilter)
        //{
        //    Models.Accion oAccion = null;
        //    List<Models.Accion> lst = new List<Models.Accion>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[8] {
        //            Param(enumDBFields.T383_fcreacion, oTEMP_AccionFilter.T383_fcreacion),
        //            Param(enumDBFields.T382_idasunto, oTEMP_AccionFilter.T382_idasunto),
        //            Param(enumDBFields.T383_avance, oTEMP_AccionFilter.T383_avance),
        //            Param(enumDBFields.T383_desaccion, oTEMP_AccionFilter.T383_desaccion),
        //            Param(enumDBFields.T383_ffin, oTEMP_AccionFilter.T383_ffin),
        //            Param(enumDBFields.T383_flimite, oTEMP_AccionFilter.T383_flimite),
        //            Param(enumDBFields.T383_idaccion, oTEMP_AccionFilter.T383_idaccion),
        //            Param(enumDBFields.T382_responsable, oTEMP_AccionFilter.T382_responsable)
        //        };

        //        dr = cDblib.DataReader("_Accion_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oAccion = new Models.Accion();
        //            oAccion.T383_fcreacion=Convert.ToDateTime(dr["T383_fcreacion"]);
        //            oAccion.T382_idasunto=Convert.ToInt32(dr["T382_idasunto"]);
        //            oAccion.T383_avance=Convert.ToByte(dr["T383_avance"]);
        //            oAccion.T383_desaccion=Convert.ToString(dr["T383_desaccion"]);
        //            if(!Convert.IsDBNull(dr["T383_ffin"]))
        //                oAccion.T383_ffin=Convert.ToDateTime(dr["T383_ffin"]);
        //            if(!Convert.IsDBNull(dr["T383_flimite"]))
        //                oAccion.T383_flimite=Convert.ToDateTime(dr["T383_flimite"]);
        //            oAccion.T383_idaccion=Convert.ToInt32(dr["T383_idaccion"]);
        //            oAccion.T382_responsable=Convert.ToInt32(dr["T382_responsable"]);

        //            lst.Add(oAccion);

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
        /// <summary>
        /// Obtiene todos las acciones de un Asunto
        /// </summary>
        internal List<Models.Accion> Catalogo(int nAsunto)
        {
            Models.Accion oAccion = null;
            List<Models.Accion> lst = new List<Models.Accion>();
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t382_idasunto, nAsunto),
                };
                dr = cDblib.DataReader("SUP_ACCION_CAT", dbparams);
                while (dr.Read())
                {
                    oAccion = new Models.Accion();
                    oAccion.t383_idaccion = Convert.ToInt32(dr["idAccion"]);
                    oAccion.t383_desaccion = Convert.ToString(dr["desAccion"]);
                    if (!Convert.IsDBNull(dr["fLimite"]))
                        oAccion.t383_flimite = Convert.ToDateTime(dr["fLimite"]);
                    oAccion.t383_avance = Convert.ToByte(dr["nAvance"]);
                    if (!Convert.IsDBNull(dr["fFin"]))
                        oAccion.t383_ffin = Convert.ToDateTime(dr["fFin"]);
                    lst.Add(oAccion);
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

        internal void Delete(int idAccion)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t383_idaccion, idAccion)
				};

                cDblib.Execute("SUP_ACCION_D", dbparams);
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
				case enumDBFields.t383_idaccion:
					paramName = "@T383_idaccion";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.t382_idasunto:
					paramName = "@T382_idasunto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;

				case enumDBFields.t383_desaccion:
					paramName = "@T383_desaccion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;

				case enumDBFields.t383_desaccionlong:
					paramName = "@T383_desaccionlong";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;

				case enumDBFields.t383_fcreacion:
					paramName = "@T383_fcreacion";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;

				case enumDBFields.t383_ffin:
					paramName = "@T383_ffin";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;

				case enumDBFields.t383_flimite:
					paramName = "@T383_flimite";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;

                case enumDBFields.t383_avance:
					paramName = "@T383_avance";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;

				case enumDBFields.t383_obs:
					paramName = "@T383_obs";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;

				case enumDBFields.t383_dpto:
					paramName = "@T383_dpto";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;

				case enumDBFields.t383_alerta:
					paramName = "@T383_alerta";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
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
