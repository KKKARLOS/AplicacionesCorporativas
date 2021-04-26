using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using IB.SUPER.IAP30.Models;
/// <summary>
/// Summary description for AccionPT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    internal class AccionPT 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			T410_fcreacion = 1,
			T409_idasunto = 2,
			T410_alerta = 3,
			T410_avance = 4,
			T410_desaccion = 5,
			T410_desaccionlong = 6,
			T410_dpto = 7,
			T410_ffin = 8,
			T410_flimite = 9,
			T410_idaccion = 10,
			T410_obs = 11
        }

        internal AccionPT(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
        #region funciones publicas
        /// <summary>
        /// Inserta un AccionPT
        /// </summary>
        internal int Insert(Models.AccionPT oAccionPT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[10] {
                    Param(enumDBFields.T410_fcreacion, oAccionPT.T410_fcreacion),
                    Param(enumDBFields.T409_idasunto, oAccionPT.T409_idasunto),
                    Param(enumDBFields.T410_alerta, oAccionPT.T410_alerta),
                    Param(enumDBFields.T410_avance, oAccionPT.T410_avance),
                    Param(enumDBFields.T410_desaccion, oAccionPT.T410_desaccion),
                    Param(enumDBFields.T410_desaccionlong, oAccionPT.T410_desaccionlong),
                    Param(enumDBFields.T410_dpto, oAccionPT.T410_dpto),
                    Param(enumDBFields.T410_ffin, oAccionPT.T410_ffin),
                    Param(enumDBFields.T410_flimite, oAccionPT.T410_flimite),
                    Param(enumDBFields.T410_obs, oAccionPT.T410_obs)
                };

                return (int)cDblib.ExecuteScalar("SUP_ACCION_PT_I", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
        ///// <summary>
        ///// Obtiene un AccionPT a partir del id
        ///// </summary>
        internal Models.AccionPT Select(int t410_idaccion)
        {
            Models.AccionPT oAccionPT = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[1] {
                Param(enumDBFields.T410_idaccion, t410_idaccion),
                };
                dr = cDblib.DataReader("SUP_ACCION_PT_S", dbparams);
                if (dr.Read())
                {
                    oAccionPT = new Models.AccionPT();
                    oAccionPT.T410_fcreacion = Convert.ToDateTime(dr["T410_fcreacion"]);
                    oAccionPT.T409_idasunto = Convert.ToInt32(dr["T409_idasunto"]);
                    oAccionPT.T410_alerta = Convert.ToString(dr["T410_alerta"]);
                    oAccionPT.T410_avance = Convert.ToByte(dr["T410_avance"]);
                    oAccionPT.T410_desaccion = Convert.ToString(dr["T410_desaccion"]);
                    oAccionPT.T410_desaccionlong = Convert.ToString(dr["T410_desaccionlong"]);
                    oAccionPT.T410_dpto = Convert.ToString(dr["T410_dpto"]);
                    if (!Convert.IsDBNull(dr["T410_ffin"]))
                        oAccionPT.T410_ffin = Convert.ToDateTime(dr["T410_ffin"]);
                    if (!Convert.IsDBNull(dr["T410_flimite"]))
                        oAccionPT.T410_flimite = Convert.ToDateTime(dr["T410_flimite"]);
                    oAccionPT.T410_idaccion = Convert.ToInt32(dr["T410_idaccion"]);
                    oAccionPT.T410_obs = Convert.ToString(dr["T410_obs"]);
                    oAccionPT.t314_idusuario_responsable = Convert.ToInt32(dr["t314_idusuario_responsable"]);
                    oAccionPT.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oAccionPT.t301_denominacion = Convert.ToString(dr["t301_denominacion"]);
                    oAccionPT.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    oAccionPT.t331_despt = Convert.ToString(dr["t331_despt"]);
                }
                return oAccionPT;

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
        /// Actualiza un AccionPT a partir del id
        /// </summary>
        internal int Update(Models.AccionPT oAccionPT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[9] {
                    Param(enumDBFields.T410_alerta, oAccionPT.T410_alerta),
                    Param(enumDBFields.T410_avance, oAccionPT.T410_avance),
                    Param(enumDBFields.T410_desaccion, oAccionPT.T410_desaccion),
                    Param(enumDBFields.T410_desaccionlong, oAccionPT.T410_desaccionlong),
                    Param(enumDBFields.T410_dpto, oAccionPT.T410_dpto),
                    Param(enumDBFields.T410_ffin, oAccionPT.T410_ffin),
                    Param(enumDBFields.T410_flimite, oAccionPT.T410_flimite),
                    Param(enumDBFields.T410_idaccion, oAccionPT.T410_idaccion),
                    Param(enumDBFields.T410_obs, oAccionPT.T410_obs)
                };

                return (int)cDblib.Execute("SUP_ACCION_PT_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un AccionPT a partir del id
        /// </summary>
        internal void Delete(int idAccion)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.T410_idaccion, idAccion)
				};

                cDblib.Execute("SUP_ACCION_PT_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// Obtiene todos los AccionPT
        ///// </summary>
        //internal List<Models.AccionPT> Catalogo(Models.AccionPT oAccionPTFilter)
        //{
        //    Models.AccionPT oAccionPT = null;
        //    List<Models.AccionPT> lst = new List<Models.AccionPT>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[11] {
        //            Param(enumDBFields.T410_fcreacion, oTEMP_AccionPTFilter.T410_fcreacion),
        //            Param(enumDBFields.T409_idasunto, oTEMP_AccionPTFilter.T409_idasunto),
        //            Param(enumDBFields.T410_alerta, oTEMP_AccionPTFilter.T410_alerta),
        //            Param(enumDBFields.T410_avance, oTEMP_AccionPTFilter.T410_avance),
        //            Param(enumDBFields.T410_desaccion, oTEMP_AccionPTFilter.T410_desaccion),
        //            Param(enumDBFields.T410_desaccionlong, oTEMP_AccionPTFilter.T410_desaccionlong),
        //            Param(enumDBFields.T410_dpto, oTEMP_AccionPTFilter.T410_dpto),
        //            Param(enumDBFields.T410_ffin, oTEMP_AccionPTFilter.T410_ffin),
        //            Param(enumDBFields.T410_flimite, oTEMP_AccionPTFilter.T410_flimite),
        //            Param(enumDBFields.T410_idaccion, oTEMP_AccionPTFilter.T410_idaccion),
        //            Param(enumDBFields.T410_obs, oTEMP_AccionPTFilter.T410_obs)
        //        };

        //        dr = cDblib.DataReader("_AccionPT_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oAccionPT = new Models.AccionPT();
        //            oAccionPT.T410_fcreacion=Convert.ToDateTime(dr["T410_fcreacion"]);
        //            oAccionPT.T409_idasunto=Convert.ToInt32(dr["T409_idasunto"]);
        //            oAccionPT.T410_alerta=Convert.ToString(dr["T410_alerta"]);
        //            oAccionPT.T410_avance=Convert.ToByte(dr["T410_avance"]);
        //            oAccionPT.T410_desaccion=Convert.ToString(dr["T410_desaccion"]);
        //            oAccionPT.T410_desaccionlong=Convert.ToString(dr["T410_desaccionlong"]);
        //            oAccionPT.T410_dpto=Convert.ToString(dr["T410_dpto"]);
        //            if(!Convert.IsDBNull(dr["T410_ffin"]))
        //                oAccionPT.T410_ffin=Convert.ToDateTime(dr["T410_ffin"]);
        //            if(!Convert.IsDBNull(dr["T410_flimite"]))
        //                oAccionPT.T410_flimite=Convert.ToDateTime(dr["T410_flimite"]);
        //            oAccionPT.T410_idaccion=Convert.ToInt32(dr["T410_idaccion"]);
        //            oAccionPT.T410_obs=Convert.ToString(dr["T410_obs"]);

        //            lst.Add(oAccionPT);

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
		
        //#endregion

        internal List<Models.AccionPT> Catalogo(int nAsunto)
        {
            Models.AccionPT oAccion = null;
            List<Models.AccionPT> lst = new List<Models.AccionPT>();
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.T409_idasunto, nAsunto),
                };
                dr = cDblib.DataReader("SUP_ACCION_PT_C", dbparams);
                while (dr.Read())
                {
                    oAccion = new Models.AccionPT();
                    oAccion.T410_idaccion = Convert.ToInt32(dr["T410_idaccion"]);
                    oAccion.T410_desaccion = Convert.ToString(dr["T410_desaccion"]);
                    if (!Convert.IsDBNull(dr["T410_flimite"]))
                        oAccion.T410_flimite = Convert.ToDateTime(dr["T410_flimite"]);
                    oAccion.T410_avance = Convert.ToByte(dr["T410_avance"]);
                    if (!Convert.IsDBNull(dr["T410_ffin"]))
                        oAccion.T410_ffin = Convert.ToDateTime(dr["T410_ffin"]);
                    oAccion.t314_idusuario_responsable = Convert.ToInt32(dr["T409_responsable"]);
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
				case enumDBFields.T410_fcreacion:
					paramName = "@T410_fcreacion";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.T409_idasunto:
					paramName = "@T409_idasunto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.T410_alerta:
					paramName = "@T410_alerta";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.T410_avance:
					paramName = "@T410_avance";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.T410_desaccion:
					paramName = "@T410_desaccion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.T410_desaccionlong:
					paramName = "@T410_desaccionlong";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.T410_dpto:
					paramName = "@T410_dpto";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.T410_ffin:
					paramName = "@T410_ffin";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.T410_flimite:
					paramName = "@T410_flimite";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.T410_idaccion:
					paramName = "@T410_idaccion";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.T410_obs:
					paramName = "@T410_obs";
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
