using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TareaProyectoTecnico
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class TareaProyectoTecnico 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t331_IDPT = 1,
			dVigIni = 2,
			dVigFin = 3,
			dPlanIni = 4,
			dPlanFin = 5,
			nPlanEstimado = 6,
			dPrevFin = 7,
			nPrevEstimado = 8,
			nPresupuesto = 9
        }

        internal TareaProyectoTecnico(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un TareaProyectoTecnico
        ///// </summary>
        //internal int Insert(Models.TareaProyectoTecnico oTareaProyectoTecnico)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[9] {
        //            Param(enumDBFields.t331_IDPT, oTareaProyectoTecnico.t331_IDPT),
        //            Param(enumDBFields.dVigIni, oTareaProyectoTecnico.dVigIni),
        //            Param(enumDBFields.dVigFin, oTareaProyectoTecnico.dVigFin),
        //            Param(enumDBFields.dPlanIni, oTareaProyectoTecnico.dPlanIni),
        //            Param(enumDBFields.dPlanFin, oTareaProyectoTecnico.dPlanFin),
        //            Param(enumDBFields.nPlanEstimado, oTareaProyectoTecnico.nPlanEstimado),
        //            Param(enumDBFields.dPrevFin, oTareaProyectoTecnico.dPrevFin),
        //            Param(enumDBFields.nPrevEstimado, oTareaProyectoTecnico.nPrevEstimado),
        //            Param(enumDBFields.nPresupuesto, oTareaProyectoTecnico.nPresupuesto)
        //        };

        //        return (int)cDblib.Execute("_TareaProyectoTecnico_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un TareaProyectoTecnico a partir del id
        ///// </summary>
        //internal Models.TareaProyectoTecnico Select()
        //{
        //    Models.TareaProyectoTecnico oTareaProyectoTecnico = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_TareaProyectoTecnico_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oTareaProyectoTecnico = new Models.TareaProyectoTecnico();
        //            oTareaProyectoTecnico.t331_IDPT=Convert.ToInt32(dr["t331_IDPT"]);
        //            if(!Convert.IsDBNull(dr["dVigIni"]))
        //                oTareaProyectoTecnico.dVigIni=Convert.ToDateTime(dr["dVigIni"]);
        //            if(!Convert.IsDBNull(dr["dVigFin"]))
        //                oTareaProyectoTecnico.dVigFin=Convert.ToDateTime(dr["dVigFin"]);
        //            if(!Convert.IsDBNull(dr["dPlanIni"]))
        //                oTareaProyectoTecnico.dPlanIni=Convert.ToDateTime(dr["dPlanIni"]);
        //            if(!Convert.IsDBNull(dr["dPlanFin"]))
        //                oTareaProyectoTecnico.dPlanFin=Convert.ToDateTime(dr["dPlanFin"]);
        //            if(!Convert.IsDBNull(dr["nPlanEstimado"]))
        //                oTareaProyectoTecnico.nPlanEstimado=Convert.ToDouble(dr["nPlanEstimado"]);
        //            if(!Convert.IsDBNull(dr["dPrevFin"]))
        //                oTareaProyectoTecnico.dPrevFin=Convert.ToDateTime(dr["dPrevFin"]);
        //            if(!Convert.IsDBNull(dr["nPrevEstimado"]))
        //                oTareaProyectoTecnico.nPrevEstimado=Convert.ToDouble(dr["nPrevEstimado"]);
        //            if(!Convert.IsDBNull(dr["nPresupuesto"]))
        //                oTareaProyectoTecnico.nPresupuesto=Convert.ToDecimal(dr["nPresupuesto"]);

        //        }
        //        return oTareaProyectoTecnico;
				
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
        ///// Actualiza un TareaProyectoTecnico a partir del id
        ///// </summary>
        //internal int Update(Models.TareaProyectoTecnico oTareaProyectoTecnico)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[9] {
        //            Param(enumDBFields.t331_IDPT, oTareaProyectoTecnico.t331_IDPT),
        //            Param(enumDBFields.dVigIni, oTareaProyectoTecnico.dVigIni),
        //            Param(enumDBFields.dVigFin, oTareaProyectoTecnico.dVigFin),
        //            Param(enumDBFields.dPlanIni, oTareaProyectoTecnico.dPlanIni),
        //            Param(enumDBFields.dPlanFin, oTareaProyectoTecnico.dPlanFin),
        //            Param(enumDBFields.nPlanEstimado, oTareaProyectoTecnico.nPlanEstimado),
        //            Param(enumDBFields.dPrevFin, oTareaProyectoTecnico.dPrevFin),
        //            Param(enumDBFields.nPrevEstimado, oTareaProyectoTecnico.nPrevEstimado),
        //            Param(enumDBFields.nPresupuesto, oTareaProyectoTecnico.nPresupuesto)
        //        };
                           
        //        return (int)cDblib.Execute("_TareaProyectoTecnico_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un TareaProyectoTecnico a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_TareaProyectoTecnico_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los TareaProyectoTecnico
        ///// </summary>
        //internal List<Models.TareaProyectoTecnico> Catalogo(Models.TareaProyectoTecnico oTareaProyectoTecnicoFilter)
        //{
        //    Models.TareaProyectoTecnico oTareaProyectoTecnico = null;
        //    List<Models.TareaProyectoTecnico> lst = new List<Models.TareaProyectoTecnico>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[9] {
        //            Param(enumDBFields.t331_IDPT, oTEMP_TareaProyectoTecnicoFilter.t331_IDPT),
        //            Param(enumDBFields.dVigIni, oTEMP_TareaProyectoTecnicoFilter.dVigIni),
        //            Param(enumDBFields.dVigFin, oTEMP_TareaProyectoTecnicoFilter.dVigFin),
        //            Param(enumDBFields.dPlanIni, oTEMP_TareaProyectoTecnicoFilter.dPlanIni),
        //            Param(enumDBFields.dPlanFin, oTEMP_TareaProyectoTecnicoFilter.dPlanFin),
        //            Param(enumDBFields.nPlanEstimado, oTEMP_TareaProyectoTecnicoFilter.nPlanEstimado),
        //            Param(enumDBFields.dPrevFin, oTEMP_TareaProyectoTecnicoFilter.dPrevFin),
        //            Param(enumDBFields.nPrevEstimado, oTEMP_TareaProyectoTecnicoFilter.nPrevEstimado),
        //            Param(enumDBFields.nPresupuesto, oTEMP_TareaProyectoTecnicoFilter.nPresupuesto)
        //        };

        //        dr = cDblib.DataReader("_TareaProyectoTecnico_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oTareaProyectoTecnico = new Models.TareaProyectoTecnico();
        //            oTareaProyectoTecnico.t331_IDPT=Convert.ToInt32(dr["t331_IDPT"]);
        //            if(!Convert.IsDBNull(dr["dVigIni"]))
        //                oTareaProyectoTecnico.dVigIni=Convert.ToDateTime(dr["dVigIni"]);
        //            if(!Convert.IsDBNull(dr["dVigFin"]))
        //                oTareaProyectoTecnico.dVigFin=Convert.ToDateTime(dr["dVigFin"]);
        //            if(!Convert.IsDBNull(dr["dPlanIni"]))
        //                oTareaProyectoTecnico.dPlanIni=Convert.ToDateTime(dr["dPlanIni"]);
        //            if(!Convert.IsDBNull(dr["dPlanFin"]))
        //                oTareaProyectoTecnico.dPlanFin=Convert.ToDateTime(dr["dPlanFin"]);
        //            if(!Convert.IsDBNull(dr["nPlanEstimado"]))
        //                oTareaProyectoTecnico.nPlanEstimado=Convert.ToDouble(dr["nPlanEstimado"]);
        //            if(!Convert.IsDBNull(dr["dPrevFin"]))
        //                oTareaProyectoTecnico.dPrevFin=Convert.ToDateTime(dr["dPrevFin"]);
        //            if(!Convert.IsDBNull(dr["nPrevEstimado"]))
        //                oTareaProyectoTecnico.nPrevEstimado=Convert.ToDouble(dr["nPrevEstimado"]);
        //            if(!Convert.IsDBNull(dr["nPresupuesto"]))
        //                oTareaProyectoTecnico.nPresupuesto=Convert.ToDecimal(dr["nPresupuesto"]);

        //            lst.Add(oTareaProyectoTecnico);

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
				case enumDBFields.dVigIni:
					paramName = "@dVigIni";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.dVigFin:
					paramName = "@dVigFin";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.dPlanIni:
					paramName = "@dPlanIni";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.dPlanFin:
					paramName = "@dPlanFin";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.nPlanEstimado:
					paramName = "@nPlanEstimado";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.dPrevFin:
					paramName = "@dPrevFin";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.nPrevEstimado:
					paramName = "@nPrevEstimado";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.nPresupuesto:
					paramName = "@nPresupuesto";
					paramType = SqlDbType.Money;
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
