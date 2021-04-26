using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AsuntoPT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class AsuntoPT 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t331_idpt = 1, 
            T409_estado = 2,
            t384_idtipo = 3,
            T409_idasunto = 4,
			T409_alerta = 5,
			T409_desasunto = 6,
			T409_desasuntolong = 7,
			T409_dpto = 8,
			T409_etp = 9,
			T409_etr = 10,
			T409_fcreacion = 11,
			T409_ffin = 12,
			T409_flimite = 13,
			T409_fnotificacion = 14,
            T409_notificador = 15,
			T409_obs = 16,
			T409_prioridad = 17,
			T409_refexterna = 18,
			T409_registrador = 19,
			T409_responsable = 20,
			T409_severidad = 21,
			T409_sistema = 22,
			t384_destipo = 23
        }

        internal AsuntoPT(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
        //#region funciones publicas
        /// <summary>
        /// Inserta un AsuntoPT
        /// </summary>
        internal int Insert(Models.AsuntoPT oAsuntoPT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[20] {
                    Param(enumDBFields.t331_idpt, oAsuntoPT.t331_idpt),
                    Param(enumDBFields.T409_alerta, oAsuntoPT.T409_alerta),
                    Param(enumDBFields.T409_desasunto, oAsuntoPT.T409_desasunto),
                    Param(enumDBFields.T409_desasuntolong, oAsuntoPT.T409_desasuntolong),
                    Param(enumDBFields.T409_dpto, oAsuntoPT.T409_dpto),
                    Param(enumDBFields.T409_estado, oAsuntoPT.T409_estado),
                    Param(enumDBFields.T409_etp, oAsuntoPT.T409_etp),
                    Param(enumDBFields.T409_etr, oAsuntoPT.T409_etr),
                    Param(enumDBFields.T409_ffin, oAsuntoPT.T409_ffin),
                    Param(enumDBFields.T409_flimite, oAsuntoPT.T409_flimite),
                    Param(enumDBFields.T409_fnotificacion, oAsuntoPT.T409_fnotificacion),
                    Param(enumDBFields.T409_notificador, oAsuntoPT.T409_notificador),
                    Param(enumDBFields.T409_obs, oAsuntoPT.T409_obs),
                    Param(enumDBFields.T409_prioridad, oAsuntoPT.T409_prioridad),
                    Param(enumDBFields.T409_refexterna, oAsuntoPT.T409_refexterna),
                    Param(enumDBFields.T409_registrador, oAsuntoPT.T409_registrador),
                    Param(enumDBFields.T409_responsable, oAsuntoPT.T409_responsable),
                    Param(enumDBFields.T409_severidad, oAsuntoPT.T409_severidad),
                    Param(enumDBFields.T409_sistema, oAsuntoPT.T409_sistema),
                    Param(enumDBFields.t384_idtipo, oAsuntoPT.t384_idtipo)
                };
                return (int)cDblib.ExecuteScalar("SUP_ASUNTO_PT_I", dbparams);    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un AsuntoPT a partir del id
        /// </summary>
        internal Models.AsuntoPT Select(int t409_idasunto)
        {
            Models.AsuntoPT oAsuntoPT = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.T409_idasunto, t409_idasunto)
                };

                dr = cDblib.DataReader("SUP_ASUNTO_PT_S", dbparams);
                if (dr.Read())
                {
                    oAsuntoPT = new Models.AsuntoPT();
                    oAsuntoPT.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    oAsuntoPT.T409_alerta = Convert.ToString(dr["T409_alerta"]);
                    oAsuntoPT.T409_desasunto = Convert.ToString(dr["T409_desasunto"]);
                    oAsuntoPT.T409_desasuntolong = Convert.ToString(dr["T409_desasuntolong"]);
                    oAsuntoPT.T409_dpto = Convert.ToString(dr["T409_dpto"]);
                    oAsuntoPT.T409_estado = Convert.ToString(dr["T409_estado"]);
                    oAsuntoPT.T409_etp = Convert.ToDouble(dr["T409_etp"]);
                    oAsuntoPT.T409_etr = Convert.ToDouble(dr["T409_etr"]);
                    oAsuntoPT.T409_fcreacion = Convert.ToDateTime(dr["T409_fcreacion"]);
                    if (!Convert.IsDBNull(dr["T409_ffin"]))
                        oAsuntoPT.T409_ffin = Convert.ToDateTime(dr["T409_ffin"]);
                    if (!Convert.IsDBNull(dr["T409_flimite"]))
                        oAsuntoPT.T409_flimite = Convert.ToDateTime(dr["T409_flimite"]);
                    oAsuntoPT.T409_fnotificacion = Convert.ToDateTime(dr["T409_fnotificacion"]);
                    oAsuntoPT.T409_idasunto = Convert.ToInt32(dr["T409_idasunto"]);
                    oAsuntoPT.T409_notificador = Convert.ToString(dr["T409_notificador"]);
                    oAsuntoPT.T409_obs = Convert.ToString(dr["T409_obs"]);
                    oAsuntoPT.T409_prioridad = Convert.ToString(dr["T409_prioridad"]);
                    oAsuntoPT.T409_refexterna = Convert.ToString(dr["T409_refexterna"]);
                    oAsuntoPT.T409_registrador = Convert.ToInt32(dr["T409_registrador"]);
                    oAsuntoPT.T409_responsable = Convert.ToInt32(dr["T409_responsable"]);
                    oAsuntoPT.T409_severidad = Convert.ToString(dr["T409_severidad"]);
                    oAsuntoPT.T409_sistema = Convert.ToString(dr["T409_sistema"]);
                    oAsuntoPT.t384_idtipo = Convert.ToInt32(dr["t384_idtipo"]);
                    if (!Convert.IsDBNull(dr["Tipo"]))
                        oAsuntoPT.t384_destipo = Convert.ToString(dr["Tipo"]);
                    oAsuntoPT.Registrador = Convert.ToString(dr["Registrador"]);
                    oAsuntoPT.Responsable = Convert.ToString(dr["Responsable"]);
                }
                return oAsuntoPT;

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
        /// Actualiza un AsuntoPT a partir del id
        /// </summary>
        internal int Update(Models.AsuntoPT oAsuntoPT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[20] {
                    Param(enumDBFields.t331_idpt, oAsuntoPT.t331_idpt),
                    Param(enumDBFields.T409_alerta, oAsuntoPT.T409_alerta),
                    Param(enumDBFields.T409_desasunto, oAsuntoPT.T409_desasunto),
                    Param(enumDBFields.T409_desasuntolong, oAsuntoPT.T409_desasuntolong),
                    Param(enumDBFields.T409_dpto, oAsuntoPT.T409_dpto),
                    Param(enumDBFields.T409_estado, oAsuntoPT.T409_estado),
                    Param(enumDBFields.T409_etp, oAsuntoPT.T409_etp),
                    Param(enumDBFields.T409_etr, oAsuntoPT.T409_etr),
                    Param(enumDBFields.T409_ffin, oAsuntoPT.T409_ffin),
                    Param(enumDBFields.T409_flimite, oAsuntoPT.T409_flimite),
                    Param(enumDBFields.T409_fnotificacion, oAsuntoPT.T409_fnotificacion),
                    Param(enumDBFields.T409_idasunto, oAsuntoPT.T409_idasunto),
                    Param(enumDBFields.T409_notificador, oAsuntoPT.T409_notificador),
                    Param(enumDBFields.T409_obs, oAsuntoPT.T409_obs),
                    Param(enumDBFields.T409_prioridad, oAsuntoPT.T409_prioridad),
                    Param(enumDBFields.T409_refexterna, oAsuntoPT.T409_refexterna),
                    Param(enumDBFields.T409_responsable, oAsuntoPT.T409_responsable),
                    Param(enumDBFields.T409_severidad, oAsuntoPT.T409_severidad),
                    Param(enumDBFields.T409_sistema, oAsuntoPT.T409_sistema),
                    Param(enumDBFields.t384_idtipo, oAsuntoPT.t384_idtipo)
                };

                return (int)cDblib.Execute("SUP_ASUNTO_PT_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
        ///// <summary>
        ///// Elimina un AsuntoPT a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_AsuntoPT_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los AsuntoPT
        ///// </summary>
        //internal List<Models.AsuntoPT> Catalogo(Models.AsuntoPT oAsuntoPTFilter)
        //{
        //    Models.AsuntoPT oAsuntoPT = null;
        //    List<Models.AsuntoPT> lst = new List<Models.AsuntoPT>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[23] {
        //            Param(enumDBFields.t331_idpt, oTEMP_AsuntoPTFilter.t331_idpt),
        //            Param(enumDBFields.T409_alerta, oTEMP_AsuntoPTFilter.T409_alerta),
        //            Param(enumDBFields.T409_desasunto, oTEMP_AsuntoPTFilter.T409_desasunto),
        //            Param(enumDBFields.T409_desasuntolong, oTEMP_AsuntoPTFilter.T409_desasuntolong),
        //            Param(enumDBFields.T409_dpto, oTEMP_AsuntoPTFilter.T409_dpto),
        //            Param(enumDBFields.T409_estado, oTEMP_AsuntoPTFilter.T409_estado),
        //            Param(enumDBFields.T409_etp, oTEMP_AsuntoPTFilter.T409_etp),
        //            Param(enumDBFields.T409_etr, oTEMP_AsuntoPTFilter.T409_etr),
        //            Param(enumDBFields.T409_fcreacion, oTEMP_AsuntoPTFilter.T409_fcreacion),
        //            Param(enumDBFields.T409_ffin, oTEMP_AsuntoPTFilter.T409_ffin),
        //            Param(enumDBFields.T409_flimite, oTEMP_AsuntoPTFilter.T409_flimite),
        //            Param(enumDBFields.T409_fnotificacion, oTEMP_AsuntoPTFilter.T409_fnotificacion),
        //            Param(enumDBFields.T409_idasunto, oTEMP_AsuntoPTFilter.T409_idasunto),
        //            Param(enumDBFields.T409_notificador, oTEMP_AsuntoPTFilter.T409_notificador),
        //            Param(enumDBFields.T409_obs, oTEMP_AsuntoPTFilter.T409_obs),
        //            Param(enumDBFields.T409_prioridad, oTEMP_AsuntoPTFilter.T409_prioridad),
        //            Param(enumDBFields.T409_refexterna, oTEMP_AsuntoPTFilter.T409_refexterna),
        //            Param(enumDBFields.T409_registrador, oTEMP_AsuntoPTFilter.T409_registrador),
        //            Param(enumDBFields.T409_responsable, oTEMP_AsuntoPTFilter.T409_responsable),
        //            Param(enumDBFields.T409_severidad, oTEMP_AsuntoPTFilter.T409_severidad),
        //            Param(enumDBFields.T409_sistema, oTEMP_AsuntoPTFilter.T409_sistema),
        //            Param(enumDBFields.t384_idtipo, oTEMP_AsuntoPTFilter.t384_idtipo),
        //            Param(enumDBFields.t384_destipo, oTEMP_AsuntoPTFilter.t384_destipo)
        //        };

        //        dr = cDblib.DataReader("_AsuntoPT_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oAsuntoPT = new Models.AsuntoPT();
        //            oAsuntoPT.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
        //            oAsuntoPT.T409_alerta=Convert.ToString(dr["T409_alerta"]);
        //            oAsuntoPT.T409_desasunto=Convert.ToString(dr["T409_desasunto"]);
        //            oAsuntoPT.T409_desasuntolong=Convert.ToString(dr["T409_desasuntolong"]);
        //            oAsuntoPT.T409_dpto=Convert.ToString(dr["T409_dpto"]);
        //            oAsuntoPT.T409_estado=Convert.ToString(dr["T409_estado"]);
        //            oAsuntoPT.T409_etp=Convert.ToDouble(dr["T409_etp"]);
        //            oAsuntoPT.T409_etr=Convert.ToDouble(dr["T409_etr"]);
        //            oAsuntoPT.T409_fcreacion=Convert.ToDateTime(dr["T409_fcreacion"]);
        //            if(!Convert.IsDBNull(dr["T409_ffin"]))
        //                oAsuntoPT.T409_ffin=Convert.ToDateTime(dr["T409_ffin"]);
        //            if(!Convert.IsDBNull(dr["T409_flimite"]))
        //                oAsuntoPT.T409_flimite=Convert.ToDateTime(dr["T409_flimite"]);
        //            oAsuntoPT.T409_fnotificacion=Convert.ToDateTime(dr["T409_fnotificacion"]);
        //            oAsuntoPT.T409_idasunto=Convert.ToInt32(dr["T409_idasunto"]);
        //            oAsuntoPT.T409_notificador=Convert.ToString(dr["T409_notificador"]);
        //            oAsuntoPT.T409_obs=Convert.ToString(dr["T409_obs"]);
        //            oAsuntoPT.T409_prioridad=Convert.ToString(dr["T409_prioridad"]);
        //            oAsuntoPT.T409_refexterna=Convert.ToString(dr["T409_refexterna"]);
        //            oAsuntoPT.T409_registrador=Convert.ToInt32(dr["T409_registrador"]);
        //            oAsuntoPT.T409_responsable=Convert.ToInt32(dr["T409_responsable"]);
        //            oAsuntoPT.T409_severidad=Convert.ToString(dr["T409_severidad"]);
        //            oAsuntoPT.T409_sistema=Convert.ToString(dr["T409_sistema"]);
        //            oAsuntoPT.t384_idtipo=Convert.ToInt32(dr["t384_idtipo"]);
        //            if(!Convert.IsDBNull(dr["t384_destipo"]))
        //                oAsuntoPT.t384_destipo=Convert.ToString(dr["t384_destipo"]);

        //            lst.Add(oAsuntoPT);

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
        /// <summary>
        /// Obtiene todos los Asunto
        /// </summary>
        internal List<Models.AsuntoCat> Catalogo(int nPT, Nullable<int> TipoAsunto, Nullable<byte> Estado)
        {
            Models.AsuntoCat oAsunto = null;
            List<Models.AsuntoCat> lst = new List<Models.AsuntoCat>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t331_idpt, nPT),
                    Param(enumDBFields.T409_estado, Estado),
                    Param(enumDBFields.t384_idtipo, TipoAsunto)
                };
                dr = cDblib.DataReader("SUP_ASUNTO_PT_CAT", dbparams);
                while (dr.Read())
                {
                    oAsunto = new Models.AsuntoCat();
                    oAsunto.idAsunto = Convert.ToInt32(dr["t409_idasunto"]);
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
                    Param(enumDBFields.T409_idasunto, idAsunto)
				};

                cDblib.Execute("SUP_ASUNTO_PT_D", dbparams);
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
				case enumDBFields.t331_idpt:
					paramName = "@t331_idpt";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.T409_estado:
                    paramName = "@T409_estado";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.t384_idtipo:
                    paramName = "@t384_idtipo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.T409_idasunto:
                    paramName = "@T409_idasunto";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.T409_alerta:
                    paramName = "@T409_alerta";
                    paramType = SqlDbType.Text;
                    paramSize = 2147483647;
                    break;
                case enumDBFields.T409_desasunto:
                    paramName = "@T409_desasunto";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.T409_desasuntolong:
                    paramName = "@T409_desasuntolong";
                    paramType = SqlDbType.Text;
                    paramSize = 2147483647;
                    break;
                case enumDBFields.T409_dpto:
                    paramName = "@T409_dpto";
                    paramType = SqlDbType.Text;
                    paramSize = 2147483647;
                    break;
                case enumDBFields.T409_etp:
                    paramName = "@T409_etp";
                    paramType = SqlDbType.Float;
                    paramSize = 8;
                    break;
                case enumDBFields.T409_etr:
                    paramName = "@T409_etr";
                    paramType = SqlDbType.Float;
                    paramSize = 8;
                    break;
                case enumDBFields.T409_fcreacion:
                    paramName = "@T409_fcreacion";
                    paramType = SqlDbType.DateTime;
                    paramSize = 8;
                    break;
                case enumDBFields.T409_ffin:
                    paramName = "@T409_ffin";
                    paramType = SqlDbType.DateTime;
                    paramSize = 8;
                    break;
                case enumDBFields.T409_flimite:
                    paramName = "@T409_flimite";
                    paramType = SqlDbType.DateTime;
                    paramSize = 8;
                    break;
                case enumDBFields.T409_fnotificacion:
                    paramName = "@T409_fnotificacion";
                    paramType = SqlDbType.DateTime;
                    paramSize = 8;
                    break;
                case enumDBFields.T409_notificador:
                    paramName = "@T409_notificador";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.T409_obs:
                    paramName = "@T409_obs";
                    paramType = SqlDbType.Text;
                    paramSize = 2147483647;
                    break;
                case enumDBFields.T409_prioridad:
                    paramName = "@T409_prioridad";
                    paramType = SqlDbType.VarChar;
                    paramSize = 22;
                    break;
                case enumDBFields.T409_refexterna:
                    paramName = "@T409_refexterna";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.T409_registrador:
                    paramName = "@T409_registrador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.T409_responsable:
                    paramName = "@T409_responsable";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.T409_severidad:
                    paramName = "@T409_severidad";
                    paramType = SqlDbType.VarChar;
                    paramSize = 22;
                    break;
                case enumDBFields.T409_sistema:
                    paramName = "@T409_sistema";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.t384_destipo:
                    paramName = "@t384_destipo";
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
