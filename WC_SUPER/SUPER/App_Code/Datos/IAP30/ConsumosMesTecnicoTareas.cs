using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumosMesTecnicoTareas
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ConsumosMesTecnicoTareas
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            t314_idusuario = 1,
            dDesde = 2,
            dHasta = 3
        }

        internal ConsumosMesTecnicoTareas(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los ConsumosMesTecnicoTareas
        /// </summary>
		internal List<Models.ConsumosMesTecnicoTareas> Catalogo(int t314_idusuario, DateTime dDesde, DateTime dHasta)
		{
            Models.ConsumosMesTecnicoTareas oConsumosMesTecnicoTareas = null;

            List<Models.ConsumosMesTecnicoTareas> lst = new List<Models.ConsumosMesTecnicoTareas>();
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.t314_idusuario,t314_idusuario),
					Param(enumDBFields.dDesde, dDesde),
					Param(enumDBFields.dHasta, dHasta)
				};

                dr = cDblib.DataReader("IAP_CONSUMOS_TECNICO_TAREAS", dbparams);
				while (dr.Read())
				{
					oConsumosMesTecnicoTareas= new Models.ConsumosMesTecnicoTareas();
					oConsumosMesTecnicoTareas.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);
					oConsumosMesTecnicoTareas.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
					oConsumosMesTecnicoTareas.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
					oConsumosMesTecnicoTareas.t334_idfase=Convert.ToInt32(dr["t334_idfase"]);
					oConsumosMesTecnicoTareas.t335_idactividad=Convert.ToInt32(dr["t335_idactividad"]);
					oConsumosMesTecnicoTareas.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
					oConsumosMesTecnicoTareas.t301_denominacion=Convert.ToString(dr["t301_denominacion"]);
					if(!Convert.IsDBNull(dr["Cualidad"]))
						oConsumosMesTecnicoTareas.Cualidad=Convert.ToString(dr["Cualidad"]);
					oConsumosMesTecnicoTareas.t302_denominacion=Convert.ToString(dr["t302_denominacion"]);
					oConsumosMesTecnicoTareas.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
					oConsumosMesTecnicoTareas.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
					oConsumosMesTecnicoTareas.T331_despt=Convert.ToString(dr["T331_despt"]);
					oConsumosMesTecnicoTareas.t334_desfase=Convert.ToString(dr["t334_desfase"]);
					oConsumosMesTecnicoTareas.t335_desactividad=Convert.ToString(dr["t335_desactividad"]);
					oConsumosMesTecnicoTareas.t332_destarea=Convert.ToString(dr["t332_destarea"]);
					if(!Convert.IsDBNull(dr["TotalHorasReportadas"]))
						oConsumosMesTecnicoTareas.TotalHorasReportadas=Convert.ToDouble(dr["TotalHorasReportadas"]);
					if(!Convert.IsDBNull(dr["TotalJornadasReportadas"]))
						oConsumosMesTecnicoTareas.TotalJornadasReportadas=Convert.ToDouble(dr["TotalJornadasReportadas"]);

                    lst.Add(oConsumosMesTecnicoTareas);

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
                case enumDBFields.t314_idusuario:
                    paramName = "@t314_idusuario";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.dDesde:
                    paramName = "@dDesde";
					paramType = SqlDbType.DateTime;
					paramSize = 4;
					break;
                case enumDBFields.dHasta:
                    paramName = "@dHasta";
                    paramType = SqlDbType.DateTime;
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
