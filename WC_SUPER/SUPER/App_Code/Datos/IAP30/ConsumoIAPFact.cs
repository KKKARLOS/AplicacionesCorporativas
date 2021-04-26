using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoIAPFact
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ConsumoIAPFact 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t301_idproyecto = 1,
			t305_seudonimo = 2,
			t332_idtarea = 3,
			t332_destarea = 4,
			t332_facturable = 5,
			t332_orden = 6,
			t331_despt = 7,
			t335_desactividad = 8,
			t334_desfase = 9,
			t332_etpl = 10,
			t336_etp = 11,
			horas_planificadas_periodo = 12,
			horas_tecnico_periodo = 13,
			horas_otros_periodo = 14,
			horas_total_periodo = 15,
			horas_planificadas_finperiodo = 16,
			horas_tecnico_finperiodo = 17,
			horas_otros_finperiodo = 18,
			horas_total_finperiodo = 19
        }

        internal ConsumoIAPFact(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un ConsumoIAPFact
        ///// </summary>
        //internal int Insert(Models.ConsumoIAPFact oConsumoIAPFact)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[19] {
        //            Param(enumDBFields.t301_idproyecto, oConsumoIAPFact.t301_idproyecto),
        //            Param(enumDBFields.t305_seudonimo, oConsumoIAPFact.t305_seudonimo),
        //            Param(enumDBFields.t332_idtarea, oConsumoIAPFact.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oConsumoIAPFact.t332_destarea),
        //            Param(enumDBFields.t332_facturable, oConsumoIAPFact.t332_facturable),
        //            Param(enumDBFields.t332_orden, oConsumoIAPFact.t332_orden),
        //            Param(enumDBFields.t331_despt, oConsumoIAPFact.t331_despt),
        //            Param(enumDBFields.t335_desactividad, oConsumoIAPFact.t335_desactividad),
        //            Param(enumDBFields.t334_desfase, oConsumoIAPFact.t334_desfase),
        //            Param(enumDBFields.t332_etpl, oConsumoIAPFact.t332_etpl),
        //            Param(enumDBFields.t336_etp, oConsumoIAPFact.t336_etp),
        //            Param(enumDBFields.horas_planificadas_periodo, oConsumoIAPFact.horas_planificadas_periodo),
        //            Param(enumDBFields.horas_tecnico_periodo, oConsumoIAPFact.horas_tecnico_periodo),
        //            Param(enumDBFields.horas_otros_periodo, oConsumoIAPFact.horas_otros_periodo),
        //            Param(enumDBFields.horas_total_periodo, oConsumoIAPFact.horas_total_periodo),
        //            Param(enumDBFields.horas_planificadas_finperiodo, oConsumoIAPFact.horas_planificadas_finperiodo),
        //            Param(enumDBFields.horas_tecnico_finperiodo, oConsumoIAPFact.horas_tecnico_finperiodo),
        //            Param(enumDBFields.horas_otros_finperiodo, oConsumoIAPFact.horas_otros_finperiodo),
        //            Param(enumDBFields.horas_total_finperiodo, oConsumoIAPFact.horas_total_finperiodo)
        //        };

        //        return (int)cDblib.Execute("_ConsumoIAPFact_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un ConsumoIAPFact a partir del id
        ///// </summary>
        //internal Models.ConsumoIAPFact Select()
        //{
        //    Models.ConsumoIAPFact oConsumoIAPFact = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_ConsumoIAPFact_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oConsumoIAPFact = new Models.ConsumoIAPFact();
        //            oConsumoIAPFact.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);
        //            oConsumoIAPFact.t305_seudonimo=Convert.ToString(dr["t305_seudonimo"]);
        //            oConsumoIAPFact.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oConsumoIAPFact.t332_destarea=Convert.ToString(dr["t332_destarea"]);
        //            oConsumoIAPFact.t332_facturable=Convert.ToBoolean(dr["t332_facturable"]);
        //            oConsumoIAPFact.t332_orden=Convert.ToInt32(dr["t332_orden"]);
        //            oConsumoIAPFact.t331_despt=Convert.ToString(dr["t331_despt"]);
        //            oConsumoIAPFact.t335_desactividad=Convert.ToString(dr["t335_desactividad"]);
        //            oConsumoIAPFact.t334_desfase=Convert.ToString(dr["t334_desfase"]);
        //            oConsumoIAPFact.t332_etpl=Convert.ToDouble(dr["t332_etpl"]);
        //            oConsumoIAPFact.t336_etp=Convert.ToDouble(dr["t336_etp"]);
        //            oConsumoIAPFact.horas_planificadas_periodo=Convert.ToDouble(dr["horas_planificadas_periodo"]);
        //            oConsumoIAPFact.horas_tecnico_periodo=Convert.ToDouble(dr["horas_tecnico_periodo"]);
        //            oConsumoIAPFact.horas_otros_periodo=Convert.ToDouble(dr["horas_otros_periodo"]);
        //            oConsumoIAPFact.horas_total_periodo=Convert.ToDouble(dr["horas_total_periodo"]);
        //            oConsumoIAPFact.horas_planificadas_finperiodo=Convert.ToDouble(dr["horas_planificadas_finperiodo"]);
        //            oConsumoIAPFact.horas_tecnico_finperiodo=Convert.ToDouble(dr["horas_tecnico_finperiodo"]);
        //            oConsumoIAPFact.horas_otros_finperiodo=Convert.ToDouble(dr["horas_otros_finperiodo"]);
        //            oConsumoIAPFact.horas_total_finperiodo=Convert.ToDouble(dr["horas_total_finperiodo"]);

        //        }
        //        return oConsumoIAPFact;
				
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
        ///// Actualiza un ConsumoIAPFact a partir del id
        ///// </summary>
        //internal int Update(Models.ConsumoIAPFact oConsumoIAPFact)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[19] {
        //            Param(enumDBFields.t301_idproyecto, oConsumoIAPFact.t301_idproyecto),
        //            Param(enumDBFields.t305_seudonimo, oConsumoIAPFact.t305_seudonimo),
        //            Param(enumDBFields.t332_idtarea, oConsumoIAPFact.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oConsumoIAPFact.t332_destarea),
        //            Param(enumDBFields.t332_facturable, oConsumoIAPFact.t332_facturable),
        //            Param(enumDBFields.t332_orden, oConsumoIAPFact.t332_orden),
        //            Param(enumDBFields.t331_despt, oConsumoIAPFact.t331_despt),
        //            Param(enumDBFields.t335_desactividad, oConsumoIAPFact.t335_desactividad),
        //            Param(enumDBFields.t334_desfase, oConsumoIAPFact.t334_desfase),
        //            Param(enumDBFields.t332_etpl, oConsumoIAPFact.t332_etpl),
        //            Param(enumDBFields.t336_etp, oConsumoIAPFact.t336_etp),
        //            Param(enumDBFields.horas_planificadas_periodo, oConsumoIAPFact.horas_planificadas_periodo),
        //            Param(enumDBFields.horas_tecnico_periodo, oConsumoIAPFact.horas_tecnico_periodo),
        //            Param(enumDBFields.horas_otros_periodo, oConsumoIAPFact.horas_otros_periodo),
        //            Param(enumDBFields.horas_total_periodo, oConsumoIAPFact.horas_total_periodo),
        //            Param(enumDBFields.horas_planificadas_finperiodo, oConsumoIAPFact.horas_planificadas_finperiodo),
        //            Param(enumDBFields.horas_tecnico_finperiodo, oConsumoIAPFact.horas_tecnico_finperiodo),
        //            Param(enumDBFields.horas_otros_finperiodo, oConsumoIAPFact.horas_otros_finperiodo),
        //            Param(enumDBFields.horas_total_finperiodo, oConsumoIAPFact.horas_total_finperiodo)
        //        };
                           
        //        return (int)cDblib.Execute("_ConsumoIAPFact_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un ConsumoIAPFact a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_ConsumoIAPFact_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los ConsumoIAPFact
        ///// </summary>
        //internal List<Models.ConsumoIAPFact> Catalogo(Models.ConsumoIAPFact oConsumoIAPFactFilter)
        //{
        //    Models.ConsumoIAPFact oConsumoIAPFact = null;
        //    List<Models.ConsumoIAPFact> lst = new List<Models.ConsumoIAPFact>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[19] {
        //            Param(enumDBFields.t301_idproyecto, oTEMP_ConsumoIAPFactFilter.t301_idproyecto),
        //            Param(enumDBFields.t305_seudonimo, oTEMP_ConsumoIAPFactFilter.t305_seudonimo),
        //            Param(enumDBFields.t332_idtarea, oTEMP_ConsumoIAPFactFilter.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oTEMP_ConsumoIAPFactFilter.t332_destarea),
        //            Param(enumDBFields.t332_facturable, oTEMP_ConsumoIAPFactFilter.t332_facturable),
        //            Param(enumDBFields.t332_orden, oTEMP_ConsumoIAPFactFilter.t332_orden),
        //            Param(enumDBFields.t331_despt, oTEMP_ConsumoIAPFactFilter.t331_despt),
        //            Param(enumDBFields.t335_desactividad, oTEMP_ConsumoIAPFactFilter.t335_desactividad),
        //            Param(enumDBFields.t334_desfase, oTEMP_ConsumoIAPFactFilter.t334_desfase),
        //            Param(enumDBFields.t332_etpl, oTEMP_ConsumoIAPFactFilter.t332_etpl),
        //            Param(enumDBFields.t336_etp, oTEMP_ConsumoIAPFactFilter.t336_etp),
        //            Param(enumDBFields.horas_planificadas_periodo, oTEMP_ConsumoIAPFactFilter.horas_planificadas_periodo),
        //            Param(enumDBFields.horas_tecnico_periodo, oTEMP_ConsumoIAPFactFilter.horas_tecnico_periodo),
        //            Param(enumDBFields.horas_otros_periodo, oTEMP_ConsumoIAPFactFilter.horas_otros_periodo),
        //            Param(enumDBFields.horas_total_periodo, oTEMP_ConsumoIAPFactFilter.horas_total_periodo),
        //            Param(enumDBFields.horas_planificadas_finperiodo, oTEMP_ConsumoIAPFactFilter.horas_planificadas_finperiodo),
        //            Param(enumDBFields.horas_tecnico_finperiodo, oTEMP_ConsumoIAPFactFilter.horas_tecnico_finperiodo),
        //            Param(enumDBFields.horas_otros_finperiodo, oTEMP_ConsumoIAPFactFilter.horas_otros_finperiodo),
        //            Param(enumDBFields.horas_total_finperiodo, oTEMP_ConsumoIAPFactFilter.horas_total_finperiodo)
        //        };

        //        dr = cDblib.DataReader("_ConsumoIAPFact_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oConsumoIAPFact = new Models.ConsumoIAPFact();
        //            oConsumoIAPFact.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);
        //            oConsumoIAPFact.t305_seudonimo=Convert.ToString(dr["t305_seudonimo"]);
        //            oConsumoIAPFact.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oConsumoIAPFact.t332_destarea=Convert.ToString(dr["t332_destarea"]);
        //            oConsumoIAPFact.t332_facturable=Convert.ToBoolean(dr["t332_facturable"]);
        //            oConsumoIAPFact.t332_orden=Convert.ToInt32(dr["t332_orden"]);
        //            oConsumoIAPFact.t331_despt=Convert.ToString(dr["t331_despt"]);
        //            oConsumoIAPFact.t335_desactividad=Convert.ToString(dr["t335_desactividad"]);
        //            oConsumoIAPFact.t334_desfase=Convert.ToString(dr["t334_desfase"]);
        //            oConsumoIAPFact.t332_etpl=Convert.ToDouble(dr["t332_etpl"]);
        //            oConsumoIAPFact.t336_etp=Convert.ToDouble(dr["t336_etp"]);
        //            oConsumoIAPFact.horas_planificadas_periodo=Convert.ToDouble(dr["horas_planificadas_periodo"]);
        //            oConsumoIAPFact.horas_tecnico_periodo=Convert.ToDouble(dr["horas_tecnico_periodo"]);
        //            oConsumoIAPFact.horas_otros_periodo=Convert.ToDouble(dr["horas_otros_periodo"]);
        //            oConsumoIAPFact.horas_total_periodo=Convert.ToDouble(dr["horas_total_periodo"]);
        //            oConsumoIAPFact.horas_planificadas_finperiodo=Convert.ToDouble(dr["horas_planificadas_finperiodo"]);
        //            oConsumoIAPFact.horas_tecnico_finperiodo=Convert.ToDouble(dr["horas_tecnico_finperiodo"]);
        //            oConsumoIAPFact.horas_otros_finperiodo=Convert.ToDouble(dr["horas_otros_finperiodo"]);
        //            oConsumoIAPFact.horas_total_finperiodo=Convert.ToDouble(dr["horas_total_finperiodo"]);

        //            lst.Add(oConsumoIAPFact);

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
				case enumDBFields.t301_idproyecto:
					paramName = "@t301_idproyecto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t305_seudonimo:
					paramName = "@t305_seudonimo";
					paramType = SqlDbType.VarChar;
					paramSize = 70;
					break;
				case enumDBFields.t332_idtarea:
					paramName = "@t332_idtarea";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_destarea:
					paramName = "@t332_destarea";
					paramType = SqlDbType.VarChar;
					paramSize = 100;
					break;
				case enumDBFields.t332_facturable:
					paramName = "@t332_facturable";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t332_orden:
					paramName = "@t332_orden";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t331_despt:
					paramName = "@t331_despt";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t335_desactividad:
					paramName = "@t335_desactividad";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t334_desfase:
					paramName = "@t334_desfase";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t332_etpl:
					paramName = "@t332_etpl";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.t336_etp:
					paramName = "@t336_etp";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.horas_planificadas_periodo:
					paramName = "@horas_planificadas_periodo";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.horas_tecnico_periodo:
					paramName = "@horas_tecnico_periodo";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.horas_otros_periodo:
					paramName = "@horas_otros_periodo";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.horas_total_periodo:
					paramName = "@horas_total_periodo";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.horas_planificadas_finperiodo:
					paramName = "@horas_planificadas_finperiodo";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.horas_tecnico_finperiodo:
					paramName = "@horas_tecnico_finperiodo";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.horas_otros_finperiodo:
					paramName = "@horas_otros_finperiodo";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.horas_total_finperiodo:
					paramName = "@horas_total_finperiodo";
					paramType = SqlDbType.Float;
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
