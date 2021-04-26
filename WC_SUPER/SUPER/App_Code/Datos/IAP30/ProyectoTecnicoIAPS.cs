using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ProyectoTecnicoIAPS
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ProyectoTecnicoIAPS 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t331_IDPT = 1,
			dPrimerConsumo = 2,
			dUltimoConsumo = 3,
			dFinEstimado = 4,
			nTotalEstimado = 5,
			nConsumidoHoras = 6,
			nConsumidoJornadas = 7,
			nPendienteEstimado = 8,
			nAvanceTeorico = 9
        }

        internal ProyectoTecnicoIAPS(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
        //#region funciones publicas
        ///// <summary>
        ///// Inserta un ProyectoTecnicoIAPS
        ///// </summary>
        //internal int Insert(Models.ProyectoTecnicoIAPS oProyectoTecnicoIAPS)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[9] {
        //            Param(enumDBFields.t331_IDPT, oProyectoTecnicoIAPS.t331_IDPT),
        //            Param(enumDBFields.dPrimerConsumo, oProyectoTecnicoIAPS.dPrimerConsumo),
        //            Param(enumDBFields.dUltimoConsumo, oProyectoTecnicoIAPS.dUltimoConsumo),
        //            Param(enumDBFields.dFinEstimado, oProyectoTecnicoIAPS.dFinEstimado),
        //            Param(enumDBFields.nTotalEstimado, oProyectoTecnicoIAPS.nTotalEstimado),
        //            Param(enumDBFields.nConsumidoHoras, oProyectoTecnicoIAPS.nConsumidoHoras),
        //            Param(enumDBFields.nConsumidoJornadas, oProyectoTecnicoIAPS.nConsumidoJornadas),
        //            Param(enumDBFields.nPendienteEstimado, oProyectoTecnicoIAPS.nPendienteEstimado),
        //            Param(enumDBFields.nAvanceTeorico, oProyectoTecnicoIAPS.nAvanceTeorico)
        //        };

        //        return (int)cDblib.Execute("_ProyectoTecnicoIAPS_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un ProyectoTecnicoIAPS a partir del id
        ///// </summary>
        //internal Models.ProyectoTecnicoIAPS Select()
        //{
        //    Models.ProyectoTecnicoIAPS oProyectoTecnicoIAPS = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_ProyectoTecnicoIAPS_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oProyectoTecnicoIAPS = new Models.ProyectoTecnicoIAPS();
        //            oProyectoTecnicoIAPS.t331_IDPT=Convert.ToInt32(dr["t331_IDPT"]);
        //            if(!Convert.IsDBNull(dr["dPrimerConsumo"]))
        //                oProyectoTecnicoIAPS.dPrimerConsumo=Convert.ToDateTime(dr["dPrimerConsumo"]);
        //            if(!Convert.IsDBNull(dr["dUltimoConsumo"]))
        //                oProyectoTecnicoIAPS.dUltimoConsumo=Convert.ToDateTime(dr["dUltimoConsumo"]);
        //            if(!Convert.IsDBNull(dr["dFinEstimado"]))
        //                oProyectoTecnicoIAPS.dFinEstimado=Convert.ToDateTime(dr["dFinEstimado"]);
        //            if(!Convert.IsDBNull(dr["nTotalEstimado"]))
        //                oProyectoTecnicoIAPS.nTotalEstimado=Convert.ToDouble(dr["nTotalEstimado"]);
        //            if(!Convert.IsDBNull(dr["nConsumidoHoras"]))
        //                oProyectoTecnicoIAPS.nConsumidoHoras=Convert.ToDouble(dr["nConsumidoHoras"]);
        //            if(!Convert.IsDBNull(dr["nConsumidoJornadas"]))
        //                oProyectoTecnicoIAPS.nConsumidoJornadas=Convert.ToDouble(dr["nConsumidoJornadas"]);
        //            if(!Convert.IsDBNull(dr["nPendienteEstimado"]))
        //                oProyectoTecnicoIAPS.nPendienteEstimado=Convert.ToDouble(dr["nPendienteEstimado"]);
        //            if(!Convert.IsDBNull(dr["nAvanceTeorico"]))
        //                oProyectoTecnicoIAPS.nAvanceTeorico=Convert.ToInt32(dr["nAvanceTeorico"]);

        //        }
        //        return oProyectoTecnicoIAPS;
				
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
		
        ///// <summary>
        ///// Actualiza un ProyectoTecnicoIAPS a partir del id
        ///// </summary>
        //internal int Update(Models.ProyectoTecnicoIAPS oProyectoTecnicoIAPS)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[9] {
        //            Param(enumDBFields.t331_IDPT, oProyectoTecnicoIAPS.t331_IDPT),
        //            Param(enumDBFields.dPrimerConsumo, oProyectoTecnicoIAPS.dPrimerConsumo),
        //            Param(enumDBFields.dUltimoConsumo, oProyectoTecnicoIAPS.dUltimoConsumo),
        //            Param(enumDBFields.dFinEstimado, oProyectoTecnicoIAPS.dFinEstimado),
        //            Param(enumDBFields.nTotalEstimado, oProyectoTecnicoIAPS.nTotalEstimado),
        //            Param(enumDBFields.nConsumidoHoras, oProyectoTecnicoIAPS.nConsumidoHoras),
        //            Param(enumDBFields.nConsumidoJornadas, oProyectoTecnicoIAPS.nConsumidoJornadas),
        //            Param(enumDBFields.nPendienteEstimado, oProyectoTecnicoIAPS.nPendienteEstimado),
        //            Param(enumDBFields.nAvanceTeorico, oProyectoTecnicoIAPS.nAvanceTeorico)
        //        };
                           
        //        return (int)cDblib.Execute("_ProyectoTecnicoIAPS_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un ProyectoTecnicoIAPS a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_ProyectoTecnicoIAPS_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los ProyectoTecnicoIAPS
        ///// </summary>
        //internal List<Models.ProyectoTecnicoIAPS> Catalogo(Models.ProyectoTecnicoIAPS oProyectoTecnicoIAPSFilter)
        //{
        //    Models.ProyectoTecnicoIAPS oProyectoTecnicoIAPS = null;
        //    List<Models.ProyectoTecnicoIAPS> lst = new List<Models.ProyectoTecnicoIAPS>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[9] {
        //            Param(enumDBFields.t331_IDPT, oTEMP_ProyectoTecnicoIAPSFilter.t331_IDPT),
        //            Param(enumDBFields.dPrimerConsumo, oTEMP_ProyectoTecnicoIAPSFilter.dPrimerConsumo),
        //            Param(enumDBFields.dUltimoConsumo, oTEMP_ProyectoTecnicoIAPSFilter.dUltimoConsumo),
        //            Param(enumDBFields.dFinEstimado, oTEMP_ProyectoTecnicoIAPSFilter.dFinEstimado),
        //            Param(enumDBFields.nTotalEstimado, oTEMP_ProyectoTecnicoIAPSFilter.nTotalEstimado),
        //            Param(enumDBFields.nConsumidoHoras, oTEMP_ProyectoTecnicoIAPSFilter.nConsumidoHoras),
        //            Param(enumDBFields.nConsumidoJornadas, oTEMP_ProyectoTecnicoIAPSFilter.nConsumidoJornadas),
        //            Param(enumDBFields.nPendienteEstimado, oTEMP_ProyectoTecnicoIAPSFilter.nPendienteEstimado),
        //            Param(enumDBFields.nAvanceTeorico, oTEMP_ProyectoTecnicoIAPSFilter.nAvanceTeorico)
        //        };

        //        dr = cDblib.DataReader("_ProyectoTecnicoIAPS_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oProyectoTecnicoIAPS = new Models.ProyectoTecnicoIAPS();
        //            oProyectoTecnicoIAPS.t331_IDPT=Convert.ToInt32(dr["t331_IDPT"]);
        //            if(!Convert.IsDBNull(dr["dPrimerConsumo"]))
        //                oProyectoTecnicoIAPS.dPrimerConsumo=Convert.ToDateTime(dr["dPrimerConsumo"]);
        //            if(!Convert.IsDBNull(dr["dUltimoConsumo"]))
        //                oProyectoTecnicoIAPS.dUltimoConsumo=Convert.ToDateTime(dr["dUltimoConsumo"]);
        //            if(!Convert.IsDBNull(dr["dFinEstimado"]))
        //                oProyectoTecnicoIAPS.dFinEstimado=Convert.ToDateTime(dr["dFinEstimado"]);
        //            if(!Convert.IsDBNull(dr["nTotalEstimado"]))
        //                oProyectoTecnicoIAPS.nTotalEstimado=Convert.ToDouble(dr["nTotalEstimado"]);
        //            if(!Convert.IsDBNull(dr["nConsumidoHoras"]))
        //                oProyectoTecnicoIAPS.nConsumidoHoras=Convert.ToDouble(dr["nConsumidoHoras"]);
        //            if(!Convert.IsDBNull(dr["nConsumidoJornadas"]))
        //                oProyectoTecnicoIAPS.nConsumidoJornadas=Convert.ToDouble(dr["nConsumidoJornadas"]);
        //            if(!Convert.IsDBNull(dr["nPendienteEstimado"]))
        //                oProyectoTecnicoIAPS.nPendienteEstimado=Convert.ToDouble(dr["nPendienteEstimado"]);
        //            if(!Convert.IsDBNull(dr["nAvanceTeorico"]))
        //                oProyectoTecnicoIAPS.nAvanceTeorico=Convert.ToInt32(dr["nAvanceTeorico"]);

        //            lst.Add(oProyectoTecnicoIAPS);

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
				case enumDBFields.t331_IDPT:
					paramName = "@t331_IDPT";
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
