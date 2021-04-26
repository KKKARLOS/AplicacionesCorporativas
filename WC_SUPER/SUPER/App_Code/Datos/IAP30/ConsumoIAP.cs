using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoIAP
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ConsumoIAP 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t332_idtarea = 1,
			t314_idusuario = 2,
			t337_fecha = 3,
			t337_esfuerzo = 4,
			t337_esfuerzoenjor = 5,
			t337_comentario = 6,
			t337_fecmodif = 7,
			t314_idusuario_modif = 8,
            dDesde=9,
            dHasta=10
        }

        internal ConsumoIAP(sqldblib.SqlServerSP extcDblib)
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
        ///// Obtiene un Consumo IAP de un profesional en una tarea en una fecha
        ///// </summary>
        internal Models.ConsumoIAP Select(Int32 idtarea, Int32 idusuario, DateTime fecha)
        {
            Models.ConsumoIAP oConsumoIAP = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t332_idtarea, idtarea),
                    Param(enumDBFields.t314_idusuario, idusuario),
                    Param(enumDBFields.t337_fecha, fecha.Date)
                };

                dr = cDblib.DataReader("SUP_CONSUMOIAP_S", dbparams);
                if (dr.Read())
                {
                    oConsumoIAP = new Models.ConsumoIAP();
                    oConsumoIAP.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    oConsumoIAP.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
                    oConsumoIAP.t337_fecha = Convert.ToDateTime(dr["t337_fecha"]);
                    oConsumoIAP.t337_esfuerzo = Convert.ToSingle(dr["t337_esfuerzo"]);
                    oConsumoIAP.t337_esfuerzoenjor = Convert.ToDouble(dr["t337_esfuerzoenjor"]);
                    oConsumoIAP.t337_comentario = Convert.ToString(dr["t337_comentario"]);
                    oConsumoIAP.t337_fecmodif = Convert.ToDateTime(dr["t337_fecmodif"]);
                    if (!Convert.IsDBNull(dr["t314_idusuario_modif"]))
                        oConsumoIAP.t314_idusuario_modif = Convert.ToInt32(dr["t314_idusuario_modif"]);

                }
                return oConsumoIAP;

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
        /// Elimina un Consumo IAP de un profesional en una tarea en una fecha
        /// </summary>
        internal int Delete(int t332_idtarea, int t314_idusuario, DateTime t337_fecha)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t332_idtarea, t332_idtarea),
                    Param(enumDBFields.t314_idusuario, t314_idusuario),
                    Param(enumDBFields.t337_fecha, t337_fecha.Date)
                };

                return (int)cDblib.Execute("SUP_CONSUMOIAP_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Elimina consumos de un profesional en un rango de fechas
        /// </summary>
        internal int DeleteRango(int t314_idusuario, DateTime dDesde, DateTime dHasta)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t314_idusuario, t314_idusuario),
                    Param(enumDBFields.dDesde, dDesde),
                    Param(enumDBFields.dHasta, dHasta)
                };

                return (int)cDblib.Execute("SUP_CONSUMOIAP_RANGO_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Elimina consumos de un profesional en una tarea en un rango de fechas
        /// </summary>
        internal int DeleteTareaRango(int t314_idusuario, int t332_idtarea, DateTime dDesde, DateTime dHasta)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(enumDBFields.t314_idusuario, t314_idusuario),
                    Param(enumDBFields.t332_idtarea, t332_idtarea),
                    Param(enumDBFields.dDesde, dDesde),
                    Param(enumDBFields.dHasta, dHasta)
                };

                return (int)cDblib.Execute("SUP_CONSUMOIAP_TAREA_RANGO_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inserta, si se puede por CLE, un consumo de un usuario en una tarea en una fecha
        /// </summary>
        /// <param name="oConsumoIAP"></param>
        /// <returns>1-> La insert se ha realizado, 0-> La insert no se ha realizado porque supera el control de límite de esfuerzo</returns>
        internal int Insert(Models.ConsumoIAP oConsumoIAP)
        {
            try
            {                
                SqlParameter[] dbparams = new SqlParameter[8] {
                    Param(enumDBFields.t332_idtarea, oConsumoIAP.t332_idtarea),
                    Param(enumDBFields.t314_idusuario, oConsumoIAP.t314_idusuario),
                    Param(enumDBFields.t337_fecha, oConsumoIAP.t337_fecha.Date),
                    Param(enumDBFields.t337_esfuerzo, oConsumoIAP.t337_esfuerzo),
                    Param(enumDBFields.t337_esfuerzoenjor, oConsumoIAP.t337_esfuerzoenjor),
                    Param(enumDBFields.t337_comentario, oConsumoIAP.t337_comentario),
                    Param(enumDBFields.t337_fecmodif, oConsumoIAP.t337_fecmodif),
                    Param(enumDBFields.t314_idusuario_modif, oConsumoIAP.t314_idusuario_modif)
                };

                //return (int)cDblib.Execute("SUP_CONSUMOIAP_I", dbparams);
                //Uso Desc en vez de Execute para recojer el valor devuelto
                //1-> La insert se ha realizado
                //0-> La insert no se ha realizado porque supera el control de límite de esfuerzo
                return (int)(cDblib.ExecuteScalar("SUP_CONSUMOIAP_I", dbparams));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene los consumos de un profesional en una fecha
        /// </summary>
        internal Models.ConsumoIAP SelectFecha(int t314_idusuario, DateTime t337_fecha)
        {
            Models.ConsumoIAP oConsumoIAP = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t314_idusuario, t314_idusuario),
                    Param(enumDBFields.t337_fecha, t337_fecha.Date)
                };

                dr = cDblib.DataReader("SUP_CONSUMOIAP_FECHA_S", dbparams);
                if (dr.Read())
                {
                    oConsumoIAP = new Models.ConsumoIAP();
                    if (!Convert.IsDBNull(dr["t337_esfuerzo"])) oConsumoIAP.t337_esfuerzo = Convert.ToSingle(dr["t337_esfuerzo"]);
                    if (!Convert.IsDBNull(dr["t337_esfuerzoenjor"])) oConsumoIAP.t337_esfuerzoenjor = Convert.ToDouble(dr["t337_esfuerzoenjor"]);

                }
                return oConsumoIAP;

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
        /// Obtiene los consumos de un profesional en una fecha
        /// </summary>
        internal Models.ConsumoIAP SelectAcumulados(int t314_idusuario, int t332_idtarea)
        {
            Models.ConsumoIAP oConsumoIAP = null;
            IDataReader dr = null;

            try
            {
                oConsumoIAP = new Models.ConsumoIAP();
                oConsumoIAP.t337_fecha = DateTime.Parse("01/01/1900");
                oConsumoIAP.t337_esfuerzo = 0;

                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t314_idusuario, t314_idusuario),
                    Param(enumDBFields.t332_idtarea, t332_idtarea)
                };

                dr = cDblib.DataReader("SUP_CONSUMOIAP_MAXTAREA_S", dbparams);
                if (dr.Read())
                {
                    oConsumoIAP.t337_esfuerzo = Convert.ToSingle(dr["horas"]);
                    if (dr["fecha"] == "") oConsumoIAP.t337_fecha = DateTime.Parse("01/01/1900");
                    else oConsumoIAP.t337_fecha = Convert.ToDateTime(dr["fecha"]);
                }
                return oConsumoIAP;

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
        /// Actualiza un ConsumoIAP a partir del id
        /// </summary>
        internal int Update(Models.ConsumoIAP oConsumoIAP)
        {
            try
            {                
                SqlParameter[] dbparams = new SqlParameter[8] {
                    Param(enumDBFields.t332_idtarea, oConsumoIAP.t332_idtarea),
                    Param(enumDBFields.t314_idusuario, oConsumoIAP.t314_idusuario),
                    Param(enumDBFields.t337_fecha, oConsumoIAP.t337_fecha.Date),
                    Param(enumDBFields.t337_esfuerzo, oConsumoIAP.t337_esfuerzo),
                    Param(enumDBFields.t337_esfuerzoenjor, oConsumoIAP.t337_esfuerzoenjor),
                    Param(enumDBFields.t337_comentario, oConsumoIAP.t337_comentario),
                    Param(enumDBFields.t337_fecmodif, oConsumoIAP.t337_fecmodif),
                    Param(enumDBFields.t314_idusuario_modif, oConsumoIAP.t314_idusuario_modif)
                };

                return (int)cDblib.Execute("SUP_CONSUMOIAP_U", dbparams);
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
				case enumDBFields.t332_idtarea:
					paramName = "@t332_idtarea";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t314_idusuario:
					paramName = "@t314_idusuario";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t337_fecha:
					paramName = "@t337_fecha";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t337_esfuerzo:
					paramName = "@t337_esfuerzo";
					paramType = SqlDbType.Real;
					paramSize = 8;
					break;
				case enumDBFields.t337_esfuerzoenjor:
					paramName = "@t337_esfuerzoenjor";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.t337_comentario:
					paramName = "@t337_comentario";
					paramType = SqlDbType.VarChar;
					paramSize = 7500;
					break;
				case enumDBFields.t337_fecmodif:
					paramName = "@t337_fecmodif";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t314_idusuario_modif:
					paramName = "@t314_idusuario_modif";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.dDesde:
                    paramName = "@dDesde";
                    paramType = SqlDbType.DateTime;
                    paramSize = 8;
                    break;
                case enumDBFields.dHasta:
                    paramName = "@dHasta";
                    paramType = SqlDbType.DateTime;
                    paramSize = 8;
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
