using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AccionT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    internal class AccionT 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			T601_fcreacion = 1,
			T600_idasunto = 2,
			T601_avance = 3,
			T601_desaccion = 4,
			T601_ffin = 5,
			T601_flimite = 6,
			T601_idaccion = 7,
			T600_responsable = 8,
            T601_alerta=9,
            T601_dpto=10,
            T601_desaccionlong=11,
            T601_obs=12
        }

        internal AccionT(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un AccionT
        /// </summary>
        internal int Insert(Models.AccionT oAccionT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[10] {
                    Param(enumDBFields.T601_fcreacion, oAccionT.T601_fcreacion),
                    Param(enumDBFields.T600_idasunto, oAccionT.T600_idasunto),
                    Param(enumDBFields.T601_alerta, oAccionT.T601_alerta),
                    Param(enumDBFields.T601_avance, oAccionT.T601_avance),
                    Param(enumDBFields.T601_desaccion, oAccionT.T601_desaccion),
                    Param(enumDBFields.T601_desaccionlong, oAccionT.T601_desaccionlong),
                    Param(enumDBFields.T601_dpto, oAccionT.T601_dpto),
                    Param(enumDBFields.T601_ffin, oAccionT.T601_ffin),
                    Param(enumDBFields.T601_flimite, oAccionT.T601_flimite),
                    Param(enumDBFields.T601_obs, oAccionT.T601_obs)
                };

                return (int)cDblib.ExecuteScalar("SUP_ACCION_T_I", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un AccionT a partir del id
        /// </summary>
        internal Models.AccionT Select(int t601_idaccion)
        {
            Models.AccionT oAccionT = null;
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.T601_idaccion, t601_idaccion),
                };
                dr = cDblib.DataReader("SUP_ACCION_T_S", dbparams);
                if (dr.Read())
                {
                    oAccionT = new Models.AccionT();
                    oAccionT.T601_fcreacion = Convert.ToDateTime(dr["T601_fcreacion"]);
                    oAccionT.T600_idasunto = Convert.ToInt32(dr["T600_idasunto"]);
                    oAccionT.T601_alerta = Convert.ToString(dr["T601_alerta"]);
                    oAccionT.T601_avance = Convert.ToByte(dr["T601_avance"]);
                    oAccionT.T601_desaccion = Convert.ToString(dr["T601_desaccion"]);
                    oAccionT.T601_desaccionlong = Convert.ToString(dr["T601_desaccionlong"]);
                    oAccionT.T601_dpto = Convert.ToString(dr["T601_dpto"]);
                    if (!Convert.IsDBNull(dr["T601_ffin"]))
                        oAccionT.T601_ffin = Convert.ToDateTime(dr["T601_ffin"]);
                    if (!Convert.IsDBNull(dr["T601_flimite"]))
                        oAccionT.T601_flimite = Convert.ToDateTime(dr["T601_flimite"]);
                    oAccionT.T601_idaccion = Convert.ToInt32(dr["T601_idaccion"]);
                    oAccionT.T601_obs = Convert.ToString(dr["T601_obs"]);
                    oAccionT.t314_idusuario_responsable = Convert.ToInt32(dr["t314_idusuario_responsable"]);
                    oAccionT.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oAccionT.t301_denominacion = Convert.ToString(dr["t301_denominacion"]);
                    oAccionT.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    oAccionT.t331_despt = Convert.ToString(dr["t331_despt"]);
                    oAccionT.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    oAccionT.t332_destarea = Convert.ToString(dr["t332_destarea"]);
                    oAccionT.t600_desasunto = Convert.ToString(dr["t600_desasunto"]);
                }
                return oAccionT;

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
        /// Actualiza un AccionT a partir del id
        /// </summary>
        internal int Update(Models.AccionT oAccionT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[9] {
                    Param(enumDBFields.T601_alerta, oAccionT.T601_alerta),
                    Param(enumDBFields.T601_avance, oAccionT.T601_avance),
                    Param(enumDBFields.T601_desaccion, oAccionT.T601_desaccion),
                    Param(enumDBFields.T601_desaccionlong, oAccionT.T601_desaccionlong),
                    Param(enumDBFields.T601_dpto, oAccionT.T601_dpto),
                    Param(enumDBFields.T601_ffin, oAccionT.T601_ffin),
                    Param(enumDBFields.T601_flimite, oAccionT.T601_flimite),
                    Param(enumDBFields.T601_idaccion, oAccionT.T601_idaccion),
                    Param(enumDBFields.T601_obs, oAccionT.T601_obs)
                };

                return (int)cDblib.Execute("SUP_ACCION_T_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un AccionT a partir del id
        /// </summary>
        internal int Delete(int idAccion)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.T601_idaccion, idAccion)
				};
                return (int)cDblib.Execute("SUP_ACCION_T_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los AccionT
        /// </summary>
        internal List<Models.AccionT> Catalogo(int nAsunto)
        {
            Models.AccionT oAccion = null;
            List<Models.AccionT> lst = new List<Models.AccionT>();
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.T600_idasunto, nAsunto),
                };
                dr = cDblib.DataReader("SUP_ACCION_T_C", dbparams);
                while (dr.Read())
                {
                    oAccion = new Models.AccionT();
                    oAccion.T601_idaccion = Convert.ToInt32(dr["T601_idaccion"]);
                    oAccion.T601_desaccion = Convert.ToString(dr["T601_desaccion"]);
                    if (!Convert.IsDBNull(dr["T601_flimite"]))
                        oAccion.T601_flimite = Convert.ToDateTime(dr["T601_flimite"]);
                    oAccion.T601_avance = Convert.ToByte(dr["T601_avance"]);
                    if (!Convert.IsDBNull(dr["T601_ffin"]))
                        oAccion.T601_ffin = Convert.ToDateTime(dr["T601_ffin"]);
                    oAccion.t314_idusuario_responsable = Convert.ToInt32(dr["T600_responsable"]);
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
				case enumDBFields.T601_fcreacion:
					paramName = "@T601_fcreacion";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.T600_idasunto:
					paramName = "@T600_idasunto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.T601_avance:
					paramName = "@T601_avance";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.T601_desaccion:
					paramName = "@T601_desaccion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.T601_ffin:
					paramName = "@T601_ffin";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.T601_flimite:
					paramName = "@T601_flimite";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.T601_idaccion:
					paramName = "@T601_idaccion";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.T600_responsable:
					paramName = "@T600_responsable";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.T601_alerta:
                    paramName = "@T601_alerta";
                    paramType = SqlDbType.Text;
                    paramSize = 2147483647;
                    break;
                case enumDBFields.T601_dpto:
                    paramName = "@T601_dpto";
                    paramType = SqlDbType.Text;
                    paramSize = 2147483647;
                    break;
                case enumDBFields.T601_desaccionlong:
                    paramName = "@T601_desaccionlong";
                    paramType = SqlDbType.Text;
                    paramSize = 2147483647;
                    break;
                case enumDBFields.T601_obs:
                    paramName = "@T601_obs";
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
