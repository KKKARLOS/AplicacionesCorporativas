using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AsuntoT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class AsuntoT 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t332_idtarea = 1,
			T600_alerta = 2,
			T600_desasunto = 3,
			T600_desasuntolong = 4,
			T600_dpto = 5,
			T600_estado = 6,
			T600_etp = 7,
			T600_etr = 8,
			T600_fcreacion = 9,
			T600_ffin = 10,
			T600_flimite = 11,
			T600_fnotificacion = 12,
			T600_idasunto = 13,
			T600_notificador = 14,
			T600_obs = 15,
			T600_prioridad = 16,
			T600_refexterna = 17,
			T600_registrador = 18,
			T600_responsable = 19,
			T600_severidad = 20,
			T600_sistema = 21,
			t384_idtipo = 22,
			t384_destipo = 23,
            t600_idasunto = 24
        }

        internal AsuntoT(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un AsuntoT
        /// </summary>
        internal int Insert(Models.AsuntoT oAsuntoT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[20] {
                    Param(enumDBFields.t332_idtarea, oAsuntoT.t332_idtarea),
                    Param(enumDBFields.T600_alerta, oAsuntoT.T600_alerta),
                    Param(enumDBFields.T600_desasunto, oAsuntoT.T600_desasunto),
                    Param(enumDBFields.T600_desasuntolong, oAsuntoT.T600_desasuntolong),
                    Param(enumDBFields.T600_dpto, oAsuntoT.T600_dpto),
                    Param(enumDBFields.T600_estado, oAsuntoT.T600_estado),
                    Param(enumDBFields.T600_etp, oAsuntoT.T600_etp),
                    Param(enumDBFields.T600_etr, oAsuntoT.T600_etr),
                    Param(enumDBFields.T600_ffin, oAsuntoT.T600_ffin),
                    Param(enumDBFields.T600_flimite, oAsuntoT.T600_flimite),
                    Param(enumDBFields.T600_fnotificacion, oAsuntoT.T600_fnotificacion),
                    Param(enumDBFields.T600_notificador, oAsuntoT.T600_notificador),
                    Param(enumDBFields.T600_obs, oAsuntoT.T600_obs),
                    Param(enumDBFields.T600_prioridad, oAsuntoT.T600_prioridad),
                    Param(enumDBFields.T600_refexterna, oAsuntoT.T600_refexterna),
                    Param(enumDBFields.T600_registrador, oAsuntoT.T600_registrador),
                    Param(enumDBFields.T600_responsable, oAsuntoT.T600_responsable),
                    Param(enumDBFields.T600_severidad, oAsuntoT.T600_severidad),
                    Param(enumDBFields.T600_sistema, oAsuntoT.T600_sistema),
                    Param(enumDBFields.t384_idtipo, oAsuntoT.t384_idtipo)
                };

                return (int)cDblib.ExecuteScalar("SUP_ASUNTO_T_I", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
        ///// <summary>
        ///// Obtiene un AsuntoT a partir del id
        ///// </summary>
        internal Models.AsuntoT Select(int t600_idasunto)
        {
            Models.AsuntoT oAsuntoT = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t600_idasunto, t600_idasunto)
                };

                dr = cDblib.DataReader("SUP_ASUNTO_T_S", dbparams);
                if (dr.Read())
                {
                    oAsuntoT = new Models.AsuntoT();
                    oAsuntoT.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    oAsuntoT.T600_alerta = Convert.ToString(dr["T600_alerta"]);
                    oAsuntoT.T600_desasunto = Convert.ToString(dr["T600_desasunto"]);
                    oAsuntoT.T600_desasuntolong = Convert.ToString(dr["T600_desasuntolong"]);
                    oAsuntoT.T600_dpto = Convert.ToString(dr["T600_dpto"]);
                    oAsuntoT.T600_estado = Convert.ToString(dr["T600_estado"]);
                    oAsuntoT.T600_etp = Convert.ToDouble(dr["T600_etp"]);
                    oAsuntoT.T600_etr = Convert.ToDouble(dr["T600_etr"]);
                    oAsuntoT.T600_fcreacion = Convert.ToDateTime(dr["T600_fcreacion"]);
                    if (!Convert.IsDBNull(dr["T600_ffin"]))
                        oAsuntoT.T600_ffin = Convert.ToDateTime(dr["T600_ffin"]);
                    if (!Convert.IsDBNull(dr["T600_flimite"]))
                        oAsuntoT.T600_flimite = Convert.ToDateTime(dr["T600_flimite"]);
                    oAsuntoT.T600_fnotificacion = Convert.ToDateTime(dr["T600_fnotificacion"]);
                    oAsuntoT.T600_idasunto = Convert.ToInt32(dr["T600_idasunto"]);
                    oAsuntoT.T600_notificador = Convert.ToString(dr["T600_notificador"]);
                    oAsuntoT.T600_obs = Convert.ToString(dr["T600_obs"]);
                    oAsuntoT.T600_prioridad = Convert.ToString(dr["T600_prioridad"]);
                    oAsuntoT.T600_refexterna = Convert.ToString(dr["T600_refexterna"]);
                    oAsuntoT.T600_registrador = Convert.ToInt32(dr["T600_registrador"]);
                    oAsuntoT.T600_responsable = Convert.ToInt32(dr["T600_responsable"]);
                    oAsuntoT.T600_severidad = Convert.ToString(dr["T600_severidad"]);
                    oAsuntoT.T600_sistema = Convert.ToString(dr["T600_sistema"]);
                    oAsuntoT.t384_idtipo = Convert.ToInt32(dr["t384_idtipo"]);
                    if (!Convert.IsDBNull(dr["Tipo"]))
                        oAsuntoT.t384_destipo = Convert.ToString(dr["Tipo"]);
                    oAsuntoT.Registrador = Convert.ToString(dr["Registrador"]);
                    oAsuntoT.Responsable = Convert.ToString(dr["Responsable"]);
                }
                return oAsuntoT;

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
        /// Actualiza un AsuntoT a partir del id
        /// </summary>
        internal int Update(Models.AsuntoT oAsuntoT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[20] {
                    Param(enumDBFields.t332_idtarea, oAsuntoT.t332_idtarea),
                    Param(enumDBFields.T600_alerta, oAsuntoT.T600_alerta),
                    Param(enumDBFields.T600_desasunto, oAsuntoT.T600_desasunto),
                    Param(enumDBFields.T600_desasuntolong, oAsuntoT.T600_desasuntolong),
                    Param(enumDBFields.T600_dpto, oAsuntoT.T600_dpto),
                    Param(enumDBFields.T600_estado, oAsuntoT.T600_estado),
                    Param(enumDBFields.T600_etp, oAsuntoT.T600_etp),
                    Param(enumDBFields.T600_etr, oAsuntoT.T600_etr),
                    Param(enumDBFields.T600_ffin, oAsuntoT.T600_ffin),
                    Param(enumDBFields.T600_flimite, oAsuntoT.T600_flimite),
                    Param(enumDBFields.T600_fnotificacion, oAsuntoT.T600_fnotificacion),
                    Param(enumDBFields.T600_idasunto, oAsuntoT.T600_idasunto),
                    Param(enumDBFields.T600_notificador, oAsuntoT.T600_notificador),
                    Param(enumDBFields.T600_obs, oAsuntoT.T600_obs),
                    Param(enumDBFields.T600_prioridad, oAsuntoT.T600_prioridad),
                    Param(enumDBFields.T600_refexterna, oAsuntoT.T600_refexterna),
                    Param(enumDBFields.T600_responsable, oAsuntoT.T600_responsable),
                    Param(enumDBFields.T600_severidad, oAsuntoT.T600_severidad),
                    Param(enumDBFields.T600_sistema, oAsuntoT.T600_sistema),
                    Param(enumDBFields.t384_idtipo, oAsuntoT.t384_idtipo)
                };

                return (int)cDblib.Execute("SUP_ASUNTO_T_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// Elimina un AsuntoT a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {


        //        return (int)cDblib.Execute("_AsuntoT_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los AsuntoT
        ///// </summary>
        //internal List<Models.AsuntoT> Catalogo(Models.AsuntoT oAsuntoTFilter)
        //{
        //    Models.AsuntoT oAsuntoT = null;
        //    List<Models.AsuntoT> lst = new List<Models.AsuntoT>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[23] {
        //            Param(enumDBFields.t332_idtarea, oTEMP_AsuntoTFilter.t332_idtarea),
        //            Param(enumDBFields.T600_alerta, oTEMP_AsuntoTFilter.T600_alerta),
        //            Param(enumDBFields.T600_desasunto, oTEMP_AsuntoTFilter.T600_desasunto),
        //            Param(enumDBFields.T600_desasuntolong, oTEMP_AsuntoTFilter.T600_desasuntolong),
        //            Param(enumDBFields.T600_dpto, oTEMP_AsuntoTFilter.T600_dpto),
        //            Param(enumDBFields.T600_estado, oTEMP_AsuntoTFilter.T600_estado),
        //            Param(enumDBFields.T600_etp, oTEMP_AsuntoTFilter.T600_etp),
        //            Param(enumDBFields.T600_etr, oTEMP_AsuntoTFilter.T600_etr),
        //            Param(enumDBFields.T600_fcreacion, oTEMP_AsuntoTFilter.T600_fcreacion),
        //            Param(enumDBFields.T600_ffin, oTEMP_AsuntoTFilter.T600_ffin),
        //            Param(enumDBFields.T600_flimite, oTEMP_AsuntoTFilter.T600_flimite),
        //            Param(enumDBFields.T600_fnotificacion, oTEMP_AsuntoTFilter.T600_fnotificacion),
        //            Param(enumDBFields.T600_idasunto, oTEMP_AsuntoTFilter.T600_idasunto),
        //            Param(enumDBFields.T600_notificador, oTEMP_AsuntoTFilter.T600_notificador),
        //            Param(enumDBFields.T600_obs, oTEMP_AsuntoTFilter.T600_obs),
        //            Param(enumDBFields.T600_prioridad, oTEMP_AsuntoTFilter.T600_prioridad),
        //            Param(enumDBFields.T600_refexterna, oTEMP_AsuntoTFilter.T600_refexterna),
        //            Param(enumDBFields.T600_registrador, oTEMP_AsuntoTFilter.T600_registrador),
        //            Param(enumDBFields.T600_responsable, oTEMP_AsuntoTFilter.T600_responsable),
        //            Param(enumDBFields.T600_severidad, oTEMP_AsuntoTFilter.T600_severidad),
        //            Param(enumDBFields.T600_sistema, oTEMP_AsuntoTFilter.T600_sistema),
        //            Param(enumDBFields.t384_idtipo, oTEMP_AsuntoTFilter.t384_idtipo),
        //            Param(enumDBFields.t384_destipo, oTEMP_AsuntoTFilter.t384_destipo)
        //        };

        //        dr = cDblib.DataReader("_AsuntoT_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oAsuntoT = new Models.AsuntoT();
        //            oAsuntoT.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oAsuntoT.T600_alerta=Convert.ToString(dr["T600_alerta"]);
        //            oAsuntoT.T600_desasunto=Convert.ToString(dr["T600_desasunto"]);
        //            oAsuntoT.T600_desasuntolong=Convert.ToString(dr["T600_desasuntolong"]);
        //            oAsuntoT.T600_dpto=Convert.ToString(dr["T600_dpto"]);
        //            oAsuntoT.T600_estado=Convert.ToString(dr["T600_estado"]);
        //            oAsuntoT.T600_etp=Convert.ToDouble(dr["T600_etp"]);
        //            oAsuntoT.T600_etr=Convert.ToDouble(dr["T600_etr"]);
        //            oAsuntoT.T600_fcreacion=Convert.ToDateTime(dr["T600_fcreacion"]);
        //            if(!Convert.IsDBNull(dr["T600_ffin"]))
        //                oAsuntoT.T600_ffin=Convert.ToDateTime(dr["T600_ffin"]);
        //            if(!Convert.IsDBNull(dr["T600_flimite"]))
        //                oAsuntoT.T600_flimite=Convert.ToDateTime(dr["T600_flimite"]);
        //            oAsuntoT.T600_fnotificacion=Convert.ToDateTime(dr["T600_fnotificacion"]);
        //            oAsuntoT.T600_idasunto=Convert.ToInt32(dr["T600_idasunto"]);
        //            oAsuntoT.T600_notificador=Convert.ToString(dr["T600_notificador"]);
        //            oAsuntoT.T600_obs=Convert.ToString(dr["T600_obs"]);
        //            oAsuntoT.T600_prioridad=Convert.ToString(dr["T600_prioridad"]);
        //            oAsuntoT.T600_refexterna=Convert.ToString(dr["T600_refexterna"]);
        //            oAsuntoT.T600_registrador=Convert.ToInt32(dr["T600_registrador"]);
        //            oAsuntoT.T600_responsable=Convert.ToInt32(dr["T600_responsable"]);
        //            oAsuntoT.T600_severidad=Convert.ToString(dr["T600_severidad"]);
        //            oAsuntoT.T600_sistema=Convert.ToString(dr["T600_sistema"]);
        //            oAsuntoT.t384_idtipo=Convert.ToInt32(dr["t384_idtipo"]);
        //            if(!Convert.IsDBNull(dr["t384_destipo"]))
        //                oAsuntoT.t384_destipo=Convert.ToString(dr["t384_destipo"]);

        //            lst.Add(oAsuntoT);

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
		
        #endregion

        /// <summary>
        /// Obtiene todos los Asunto
        /// </summary>
        internal List<Models.AsuntoCat> Catalogo(int nTarea, Nullable<int> TipoAsunto, Nullable<byte> Estado)
        {
            Models.AsuntoCat oAsunto = null;
            List<Models.AsuntoCat> lst = new List<Models.AsuntoCat>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t332_idtarea, nTarea),
                    Param(enumDBFields.T600_estado, Estado),
                    Param(enumDBFields.t384_idtipo, TipoAsunto)
                };
                dr = cDblib.DataReader("SUP_ASUNTO_TAREA_CAT", dbparams);
                while (dr.Read())
                {
                    oAsunto = new Models.AsuntoCat();
                    oAsunto.idAsunto = Convert.ToInt32(dr["t600_idasunto"]);
                    oAsunto.desAsunto = Convert.ToString(dr["desAsunto"]);
                    oAsunto.desTipo = Convert.ToString(dr["desTipo"]);
                    oAsunto.severidad = Convert.ToString(dr["severidad"]);
                    oAsunto.prioridad = Convert.ToString(dr["prioridad"]);
                    oAsunto.estado = Convert.ToString(dr["estado"]);
                    if (!Convert.IsDBNull(dr["fLimite"]))
                        oAsunto.fLimite = Convert.ToDateTime(dr["fLimite"]);
                    oAsunto.fNotificacion = Convert.ToDateTime(dr["fNotificacion"]);
                    oAsunto.idUserResponsable = Convert.ToInt32(dr["idUserResponsable"]);
                    lst.Add(oAsunto);
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

        internal void Borrar(int idAsunto)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.T600_idasunto, idAsunto)
				};

                cDblib.Execute("SUP_ASUNTO_T_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
				case enumDBFields.t332_idtarea:
					paramName = "@t332_idtarea";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.T600_alerta:
					paramName = "@T600_alerta";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.T600_desasunto:
					paramName = "@T600_desasunto";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.T600_desasuntolong:
					paramName = "@T600_desasuntolong";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.T600_dpto:
					paramName = "@T600_dpto";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.T600_estado:
					paramName = "@T600_estado";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.T600_etp:
					paramName = "@T600_etp";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.T600_etr:
					paramName = "@T600_etr";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.T600_fcreacion:
					paramName = "@T600_fcreacion";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.T600_ffin:
					paramName = "@T600_ffin";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.T600_flimite:
					paramName = "@T600_flimite";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.T600_fnotificacion:
					paramName = "@T600_fnotificacion";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.T600_idasunto:
					paramName = "@T600_idasunto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.T600_notificador:
					paramName = "@T600_notificador";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.T600_obs:
					paramName = "@T600_obs";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.T600_prioridad:
					paramName = "@T600_prioridad";
					paramType = SqlDbType.VarChar;
					paramSize = 22;
					break;
				case enumDBFields.T600_refexterna:
					paramName = "@T600_refexterna";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.T600_registrador:
					paramName = "@T600_registrador";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.T600_responsable:
					paramName = "@T600_responsable";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.T600_severidad:
					paramName = "@T600_severidad";
					paramType = SqlDbType.VarChar;
					paramSize = 22;
					break;
				case enumDBFields.T600_sistema:
					paramName = "@T600_sistema";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t384_idtipo:
					paramName = "@t384_idtipo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t384_destipo:
					paramName = "@t384_destipo";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
                case enumDBFields.t600_idasunto:
                    paramName = "@T600_idasunto";
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
