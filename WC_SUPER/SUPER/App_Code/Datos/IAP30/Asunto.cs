using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Asunto
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class Asunto 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            t301_idproyecto = 1,
			t305_idproyectosubnodo = 2,
			T382_alerta = 3,
			T382_desasunto = 4,
			T382_desasuntolong = 5,
			T382_dpto = 6,
			T382_estado = 7,
			T382_etp = 8,
			T382_etr = 9,
			T382_fcreacion = 10,
			T382_ffin = 11,
			T382_flimite = 12,
			T382_fnotificacion = 13,
			T382_idasunto = 14,
			T382_notificador = 15,
			T382_obs = 16,
			T382_prioridad = 17,
			T382_refexterna = 18,
			T382_registrador = 19,
			T382_responsable = 20,
			T382_severidad = 21,
			T382_sistema = 22,
			T384_idtipo = 23,
			T384_destipo = 24
        }

        internal Asunto(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un Asunto
        /// </summary>
        internal int Insert(Models.Asunto oAsunto)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[20] {
                    Param(enumDBFields.t305_idproyectosubnodo, oAsunto.t305_idproyectosubnodo),
                    Param(enumDBFields.T382_alerta, oAsunto.T382_alerta),
                    Param(enumDBFields.T382_desasunto, oAsunto.T382_desasunto),
                    Param(enumDBFields.T382_desasuntolong, oAsunto.T382_desasuntolong),
                    Param(enumDBFields.T382_dpto, oAsunto.T382_dpto),
                    Param(enumDBFields.T382_estado,  byte.Parse(oAsunto.T382_estado)),
                    Param(enumDBFields.T382_etp, oAsunto.T382_etp),
                    Param(enumDBFields.T382_etr, oAsunto.T382_etr),
                    Param(enumDBFields.T382_ffin, oAsunto.T382_ffin),
                    Param(enumDBFields.T382_flimite, oAsunto.T382_flimite),
                    Param(enumDBFields.T382_fnotificacion, oAsunto.T382_fnotificacion),
                    Param(enumDBFields.T382_notificador, oAsunto.T382_notificador),
                    Param(enumDBFields.T382_obs, oAsunto.T382_obs),
                    Param(enumDBFields.T382_prioridad,  byte.Parse(oAsunto.T382_prioridad)),
                    Param(enumDBFields.T382_refexterna, oAsunto.T382_refexterna),
                    Param(enumDBFields.T382_registrador, oAsunto.T382_registrador),
                    Param(enumDBFields.T382_responsable, oAsunto.T382_responsable),
                    Param(enumDBFields.T382_severidad,  byte.Parse(oAsunto.T382_severidad)),
                    Param(enumDBFields.T382_sistema, oAsunto.T382_sistema),
                    Param(enumDBFields.T384_idtipo, oAsunto.T384_idtipo),
                };
                //return (int)cDblib.Execute("SUP_ASUNTO_I", dbparams);
                return (int)cDblib.ExecuteScalar("SUP_ASUNTO_I", dbparams);               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un Asunto a partir del id
        /// </summary>
        internal Models.Asunto Select(int t382_idasunto) 
        {
            Models.Asunto oAsuntoPE = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.T382_idasunto, t382_idasunto)
                };

                dr = cDblib.DataReader("SUP_ASUNTO_S", dbparams);
                if (dr.Read())
                {
                    oAsuntoPE = new Models.Asunto();
                    oAsuntoPE.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    oAsuntoPE.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oAsuntoPE.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oAsuntoPE.T382_alerta = Convert.ToString(dr["T382_alerta"]);
                    oAsuntoPE.T382_desasunto = Convert.ToString(dr["T382_desasunto"]);
                    oAsuntoPE.T382_desasuntolong = Convert.ToString(dr["T382_desasuntolong"]);
                    oAsuntoPE.T382_dpto = Convert.ToString(dr["T382_dpto"]);
                    oAsuntoPE.T382_estado = Convert.ToString(dr["T382_estado"]);
                    oAsuntoPE.T382_etp = Convert.ToDouble(dr["T382_etp"]);
                    oAsuntoPE.T382_etr = Convert.ToDouble(dr["T382_etr"]);
                    oAsuntoPE.T382_fcreacion = Convert.ToDateTime(dr["T382_fcreacion"]);
                    if (!Convert.IsDBNull(dr["T382_ffin"]))
                        oAsuntoPE.T382_ffin = Convert.ToDateTime(dr["T382_ffin"]);
                    if (!Convert.IsDBNull(dr["T382_flimite"]))
                        oAsuntoPE.T382_flimite = Convert.ToDateTime(dr["T382_flimite"]);
                    oAsuntoPE.T382_fnotificacion = Convert.ToDateTime(dr["T382_fnotificacion"]);
                    oAsuntoPE.T382_idasunto = Convert.ToInt32(dr["T382_idasunto"]);
                    oAsuntoPE.T382_notificador = Convert.ToString(dr["T382_notificador"]);
                    oAsuntoPE.T382_obs = Convert.ToString(dr["T382_obs"]);
                    oAsuntoPE.T382_prioridad = Convert.ToString(dr["T382_prioridad"]);
                    oAsuntoPE.T382_refexterna = Convert.ToString(dr["T382_refexterna"]);
                    oAsuntoPE.T382_registrador = Convert.ToInt32(dr["T382_registrador"]);
                    oAsuntoPE.T382_responsable = Convert.ToInt32(dr["T382_responsable"]);
                    oAsuntoPE.T382_severidad = Convert.ToString(dr["T382_severidad"]);
                    oAsuntoPE.T382_sistema = Convert.ToString(dr["T382_sistema"]);
                    oAsuntoPE.T384_idtipo = Convert.ToInt32(dr["T384_idtipo"]);
                    if (!Convert.IsDBNull(dr["Tipo"]))
                        oAsuntoPE.T384_destipo = Convert.ToString(dr["Tipo"]);
                    oAsuntoPE.Registrador = Convert.ToString(dr["Registrador"]);
                    oAsuntoPE.Responsable = Convert.ToString(dr["Responsable"]);
                }
                return oAsuntoPE;

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
        /// Actualiza un Asunto a partir del id
        /// </summary>
        
        internal int Update(Models.Asunto oAsunto)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[19] {                    
                    Param(enumDBFields.T382_alerta, oAsunto.T382_alerta),
                    Param(enumDBFields.T382_desasunto, oAsunto.T382_desasunto),
                    Param(enumDBFields.T382_desasuntolong, oAsunto.T382_desasuntolong),
                    Param(enumDBFields.T382_dpto, oAsunto.T382_dpto),
                    Param(enumDBFields.T382_estado, byte.Parse(oAsunto.T382_estado)),
                    Param(enumDBFields.T382_etp, oAsunto.T382_etp),
                    Param(enumDBFields.T382_etr, oAsunto.T382_etr),
                    Param(enumDBFields.T382_ffin, oAsunto.T382_ffin),
                    Param(enumDBFields.T382_flimite, oAsunto.T382_flimite),
                    Param(enumDBFields.T382_fnotificacion, oAsunto.T382_fnotificacion),
                    Param(enumDBFields.T382_idasunto, oAsunto.T382_idasunto),
                    Param(enumDBFields.T382_notificador, oAsunto.T382_notificador),
                    Param(enumDBFields.T382_obs, oAsunto.T382_obs),
                    Param(enumDBFields.T382_prioridad,  byte.Parse(oAsunto.T382_prioridad)),
                    Param(enumDBFields.T382_refexterna, oAsunto.T382_refexterna),
                    Param(enumDBFields.T382_responsable, oAsunto.T382_responsable),
                    Param(enumDBFields.T382_severidad,  byte.Parse(oAsunto.T382_severidad)),
                    Param(enumDBFields.T382_sistema, oAsunto.T382_sistema),
                    Param(enumDBFields.T384_idtipo, oAsunto.T384_idtipo),
                };

                return (int)cDblib.Execute("SUP_ASUNTO_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
        ///// <summary>
        ///// Elimina un Asunto a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_Asunto_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los Asunto
        ///// </summary>
        //internal List<Models.Asunto> Catalogo(Models.Asunto oAsuntoFilter)
        //{
        //    Models.Asunto oAsunto = null;
        //    List<Models.Asunto> lst = new List<Models.Asunto>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[24] {
        //            Param(enumDBFields.t301_idproyecto, oTEMP_AsuntoFilter.t301_idproyecto),
        //            Param(enumDBFields.t305_idproyectosubnodo, oTEMP_AsuntoFilter.t305_idproyectosubnodo),
        //            Param(enumDBFields.T382_alerta, oTEMP_AsuntoFilter.T382_alerta),
        //            Param(enumDBFields.T382_desasunto, oTEMP_AsuntoFilter.T382_desasunto),
        //            Param(enumDBFields.T382_desasuntolong, oTEMP_AsuntoFilter.T382_desasuntolong),
        //            Param(enumDBFields.T382_dpto, oTEMP_AsuntoFilter.T382_dpto),
        //            Param(enumDBFields.T382_estado, oTEMP_AsuntoFilter.T382_estado),
        //            Param(enumDBFields.T382_etp, oTEMP_AsuntoFilter.T382_etp),
        //            Param(enumDBFields.T382_etr, oTEMP_AsuntoFilter.T382_etr),
        //            Param(enumDBFields.T382_fcreacion, oTEMP_AsuntoFilter.T382_fcreacion),
        //            Param(enumDBFields.T382_ffin, oTEMP_AsuntoFilter.T382_ffin),
        //            Param(enumDBFields.T382_flimite, oTEMP_AsuntoFilter.T382_flimite),
        //            Param(enumDBFields.T382_fnotificacion, oTEMP_AsuntoFilter.T382_fnotificacion),
        //            Param(enumDBFields.T382_idasunto, oTEMP_AsuntoFilter.T382_idasunto),
        //            Param(enumDBFields.T382_notificador, oTEMP_AsuntoFilter.T382_notificador),
        //            Param(enumDBFields.T382_obs, oTEMP_AsuntoFilter.T382_obs),
        //            Param(enumDBFields.T382_prioridad, oTEMP_AsuntoFilter.T382_prioridad),
        //            Param(enumDBFields.T382_refexterna, oTEMP_AsuntoFilter.T382_refexterna),
        //            Param(enumDBFields.T382_registrador, oTEMP_AsuntoFilter.T382_registrador),
        //            Param(enumDBFields.T382_responsable, oTEMP_AsuntoFilter.T382_responsable),
        //            Param(enumDBFields.T382_severidad, oTEMP_AsuntoFilter.T382_severidad),
        //            Param(enumDBFields.T382_sistema, oTEMP_AsuntoFilter.T382_sistema),
        //            Param(enumDBFields.T384_idtipo, oTEMP_AsuntoFilter.T384_idtipo),
        //            Param(enumDBFields.T384_destipo, oTEMP_AsuntoFilter.T384_destipo)
        //        };

        //        dr = cDblib.DataReader("_Asunto_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oAsunto = new Models.Asunto();
        //            oAsunto.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);
        //            oAsunto.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oAsunto.T382_alerta=Convert.ToString(dr["T382_alerta"]);
        //            oAsunto.T382_desasunto=Convert.ToString(dr["T382_desasunto"]);
        //            oAsunto.T382_desasuntolong=Convert.ToString(dr["T382_desasuntolong"]);
        //            oAsunto.T382_dpto=Convert.ToString(dr["T382_dpto"]);
        //            oAsunto.T382_estado=Convert.ToString(dr["T382_estado"]);
        //            oAsunto.T382_etp=Convert.ToDouble(dr["T382_etp"]);
        //            oAsunto.T382_etr=Convert.ToDouble(dr["T382_etr"]);
        //            oAsunto.T382_fcreacion=Convert.ToDateTime(dr["T382_fcreacion"]);
        //            if(!Convert.IsDBNull(dr["T382_ffin"]))
        //                oAsunto.T382_ffin=Convert.ToDateTime(dr["T382_ffin"]);
        //            if(!Convert.IsDBNull(dr["T382_flimite"]))
        //                oAsunto.T382_flimite=Convert.ToDateTime(dr["T382_flimite"]);
        //            oAsunto.T382_fnotificacion=Convert.ToDateTime(dr["T382_fnotificacion"]);
        //            oAsunto.T382_idasunto=Convert.ToInt32(dr["T382_idasunto"]);
        //            oAsunto.T382_notificador=Convert.ToString(dr["T382_notificador"]);
        //            oAsunto.T382_obs=Convert.ToString(dr["T382_obs"]);
        //            oAsunto.T382_prioridad=Convert.ToString(dr["T382_prioridad"]);
        //            oAsunto.T382_refexterna=Convert.ToString(dr["T382_refexterna"]);
        //            oAsunto.T382_registrador=Convert.ToInt32(dr["T382_registrador"]);
        //            oAsunto.T382_responsable=Convert.ToInt32(dr["T382_responsable"]);
        //            oAsunto.T382_severidad=Convert.ToString(dr["T382_severidad"]);
        //            oAsunto.T382_sistema=Convert.ToString(dr["T382_sistema"]);
        //            oAsunto.T384_idtipo=Convert.ToInt32(dr["T384_idtipo"]);
        //            if(!Convert.IsDBNull(dr["T384_destipo"]))
        //                oAsunto.T384_destipo=Convert.ToString(dr["T384_destipo"]);

        //            lst.Add(oAsunto);

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
                case enumDBFields.t301_idproyecto:
                    paramName = "@t301_idproyecto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t305_idproyectosubnodo:
					paramName = "@t305_idproyectosubnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.T382_alerta:
					paramName = "@T382_alerta";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.T382_desasunto:
					paramName = "@T382_desasunto";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.T382_desasuntolong:
					paramName = "@T382_desasuntolong";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.T382_dpto:
					paramName = "@T382_dpto";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.T382_estado:
					paramName = "@T382_estado";
					paramType = SqlDbType.VarChar;
					paramSize = 19;
					break;
				case enumDBFields.T382_etp:
					paramName = "@T382_etp";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.T382_etr:
					paramName = "@T382_etr";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.T382_fcreacion:
					paramName = "@T382_fcreacion";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.T382_ffin:
					paramName = "@T382_ffin";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.T382_flimite:
					paramName = "@T382_flimite";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.T382_fnotificacion:
					paramName = "@T382_fnotificacion";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.T382_idasunto:
					paramName = "@T382_idasunto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.T382_notificador:
					paramName = "@T382_notificador";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.T382_obs:
					paramName = "@T382_obs";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.T382_prioridad:
					paramName = "@T382_prioridad";
					paramType = SqlDbType.VarChar;
					paramSize = 22;
					break;
				case enumDBFields.T382_refexterna:
					paramName = "@T382_refexterna";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.T382_registrador:
					paramName = "@T382_registrador";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.T382_responsable:
					paramName = "@T382_responsable";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.T382_severidad:
					paramName = "@T382_severidad";
					paramType = SqlDbType.VarChar;
					paramSize = 22;
					break;
				case enumDBFields.T382_sistema:
					paramName = "@T382_sistema";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.T384_idtipo:
					paramName = "@T384_idtipo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.T384_destipo:
					paramName = "@T384_destipo";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
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
