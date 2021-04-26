using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TareaIAPS
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class TareaIAPS 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            t332_idtarea = 1,
			dPrimerConsumo = 2,
			dUltimoConsumo = 3,
			dFinEstimado = 4,
			nTotalEstimado = 5,
			nConsumidoHoras = 6,
			nConsumidoJornadas = 7,
			nPendienteEstimado = 8,
			nAvanceTeorico = 9,
            nIdTarea=10
        }

        internal TareaIAPS(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene un TareaIAPS a partir del id
        /// </summary>
        internal Models.TareaIAPS Select(int t332_idtarea)
        {
            Models.TareaIAPS oTareaIAPS = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.nIdTarea, t332_idtarea)
                };

                dr = cDblib.DataReader("SUP_TAREAIAPS", dbparams);
                if (dr.Read())
                {
                    oTareaIAPS = new Models.TareaIAPS();
                    oTareaIAPS.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    if (!Convert.IsDBNull(dr["dPrimerConsumo"]))
                        oTareaIAPS.dPrimerConsumo = Convert.ToDateTime(dr["dPrimerConsumo"]);
                    if (!Convert.IsDBNull(dr["dUltimoConsumo"]))
                        oTareaIAPS.dUltimoConsumo = Convert.ToDateTime(dr["dUltimoConsumo"]);
                    if (!Convert.IsDBNull(dr["dFinEstimado"]))
                        oTareaIAPS.dFinEstimado = Convert.ToDateTime(dr["dFinEstimado"]);
                    if (!Convert.IsDBNull(dr["nTotalEstimado"]))
                        oTareaIAPS.nTotalEstimado = Convert.ToDouble(dr["nTotalEstimado"]);
                    if (!Convert.IsDBNull(dr["nConsumidoHoras"]))
                        oTareaIAPS.nConsumidoHoras = Convert.ToDouble(dr["nConsumidoHoras"]);
                    if (!Convert.IsDBNull(dr["nConsumidoJornadas"]))
                        oTareaIAPS.nConsumidoJornadas = Convert.ToDouble(dr["nConsumidoJornadas"]);
                    if (!Convert.IsDBNull(dr["nPendienteEstimado"]))
                        oTareaIAPS.nPendienteEstimado = Convert.ToDouble(dr["nPendienteEstimado"]);
                    if (!Convert.IsDBNull(dr["nAvanceTeorico"]))
                        oTareaIAPS.nAvanceTeorico = Convert.ToInt32(dr["nAvanceTeorico"]);

                }
                return oTareaIAPS;

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
                case enumDBFields.t332_idtarea:
                    paramName = "@t332_idtarea";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.nIdTarea:
                    paramName = "@nIdTarea";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.dPrimerConsumo:
					paramName = "@dPrimerConsumo";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.dUltimoConsumo:
					paramName = "@dUltimoConsumo";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.dFinEstimado:
					paramName = "@dFinEstimado";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.nTotalEstimado:
					paramName = "@nTotalEstimado";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.nConsumidoHoras:
					paramName = "@nConsumidoHoras";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.nConsumidoJornadas:
					paramName = "@nConsumidoJornadas";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.nPendienteEstimado:
					paramName = "@nPendienteEstimado";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.nAvanceTeorico:
					paramName = "@nAvanceTeorico";
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
