using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for PlanifAgendaU
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class PlanifAgendaU 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t458_idPlanif = 1,
			t001_idficepi = 2,
			t001_idficepi_mod = 3,
			t458_fechamod = 4,
			t458_asunto = 5,
			t458_motivo = 6,
			t458_fechoraini = 7,
			t458_fechorafin = 8,
			t332_idtarea = 9,
			t458_privado = 10,
			t458_observaciones = 11
        }

        internal PlanifAgendaU(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un PlanifAgendaU
        ///// </summary>
        //internal int Insert(Models.PlanifAgendaU oPlanifAgendaU)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[16] {
        //            Param(enumDBFields.t458_idPlanif, oPlanifAgendaU.t458_idPlanif),
        //            Param(enumDBFields.t001_idficepi, oPlanifAgendaU.t001_idficepi),
        //            Param(enumDBFields.t001_idficepi_mod, oPlanifAgendaU.t001_idficepi_mod),
        //            Param(enumDBFields.t458_fechamod, oPlanifAgendaU.t458_fechamod),
        //            Param(enumDBFields.t458_asunto, oPlanifAgendaU.t458_asunto),
        //            Param(enumDBFields.t458_motivo, oPlanifAgendaU.t458_motivo),
        //            Param(enumDBFields.t458_fechoraini, oPlanifAgendaU.t458_fechoraini),
        //            Param(enumDBFields.t458_fechorafin, oPlanifAgendaU.t458_fechorafin),
        //            Param(enumDBFields.t332_idtarea, oPlanifAgendaU.t332_idtarea),
        //            Param(enumDBFields.t458_privado, oPlanifAgendaU.t458_privado),
        //            Param(enumDBFields.t458_observaciones, oPlanifAgendaU.t458_observaciones)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_PlanifAgendaU_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un PlanifAgendaU a partir del id
        ///// </summary>
        //internal Models.PlanifAgendaU Select()
        //{
        //    Models.PlanifAgendaU oPlanifAgendaU = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_PlanifAgendaU_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oPlanifAgendaU = new Models.PlanifAgendaU();
        //            oPlanifAgendaU.t458_idPlanif=Convert.ToInt32(dr["t458_idPlanif"]);
        //            oPlanifAgendaU.t001_idficepi=Convert.ToInt32(dr["t001_idficepi"]);
        //            oPlanifAgendaU.t001_idficepi_mod=Convert.ToInt32(dr["t001_idficepi_mod"]);
        //            oPlanifAgendaU.t458_fechamod=Convert.ToDateTime(dr["t458_fechamod"]);
        //            oPlanifAgendaU.t458_asunto=Convert.ToString(dr["t458_asunto"]);
        //            oPlanifAgendaU.t458_motivo=Convert.ToString(dr["t458_motivo"]);
        //            oPlanifAgendaU.t458_fechoraini=Convert.ToDateTime(dr["t458_fechoraini"]);
        //            oPlanifAgendaU.t458_fechorafin=Convert.ToDateTime(dr["t458_fechorafin"]);
        //            if(!Convert.IsDBNull(dr["t332_idtarea"]))
        //                oPlanifAgendaU.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oPlanifAgendaU.t458_privado=Convert.ToString(dr["t458_privado"]);
        //            oPlanifAgendaU.t458_observaciones=Convert.ToString(dr["t458_observaciones"]);
        //        }
        //        return oPlanifAgendaU;
				
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
        ///// Actualiza un PlanifAgendaU a partir del id
        ///// </summary>
        //internal int Update(Models.PlanifAgendaU oPlanifAgendaU)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[16] {
        //            Param(enumDBFields.t458_idPlanif, oPlanifAgendaU.t458_idPlanif),
        //            Param(enumDBFields.t001_idficepi, oPlanifAgendaU.t001_idficepi),
        //            Param(enumDBFields.t001_idficepi_mod, oPlanifAgendaU.t001_idficepi_mod),
        //            Param(enumDBFields.t458_fechamod, oPlanifAgendaU.t458_fechamod),
        //            Param(enumDBFields.t458_asunto, oPlanifAgendaU.t458_asunto),
        //            Param(enumDBFields.t458_motivo, oPlanifAgendaU.t458_motivo),
        //            Param(enumDBFields.t458_fechoraini, oPlanifAgendaU.t458_fechoraini),
        //            Param(enumDBFields.t458_fechorafin, oPlanifAgendaU.t458_fechorafin),
        //            Param(enumDBFields.t332_idtarea, oPlanifAgendaU.t332_idtarea),
        //            Param(enumDBFields.t458_privado, oPlanifAgendaU.t458_privado),
        //            Param(enumDBFields.t458_observaciones, oPlanifAgendaU.t458_observaciones)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_PlanifAgendaU_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un PlanifAgendaU a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_PlanifAgendaU_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los PlanifAgendaU
        ///// </summary>
        //internal List<Models.PlanifAgendaU> Catalogo(Models.PlanifAgendaU oPlanifAgendaUFilter)
        //{
        //    Models.PlanifAgendaU oPlanifAgendaU = null;
        //    List<Models.PlanifAgendaU> lst = new List<Models.PlanifAgendaU>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[16] {
        //            Param(enumDBFields.t458_idPlanif, oTEMP_PlanifAgendaUFilter.t458_idPlanif),
        //            Param(enumDBFields.t001_idficepi, oTEMP_PlanifAgendaUFilter.t001_idficepi),
        //            Param(enumDBFields.t001_idficepi_mod, oTEMP_PlanifAgendaUFilter.t001_idficepi_mod),
        //            Param(enumDBFields.t458_fechamod, oTEMP_PlanifAgendaUFilter.t458_fechamod),
        //            Param(enumDBFields.t458_asunto, oTEMP_PlanifAgendaUFilter.t458_asunto),
        //            Param(enumDBFields.t458_motivo, oTEMP_PlanifAgendaUFilter.t458_motivo),
        //            Param(enumDBFields.t458_fechoraini, oTEMP_PlanifAgendaUFilter.t458_fechoraini),
        //            Param(enumDBFields.t458_fechorafin, oTEMP_PlanifAgendaUFilter.t458_fechorafin),
        //            Param(enumDBFields.t332_idtarea, oTEMP_PlanifAgendaUFilter.t332_idtarea),
        //            Param(enumDBFields.t458_privado, oTEMP_PlanifAgendaUFilter.t458_privado),
        //            Param(enumDBFields.t458_observaciones, oTEMP_PlanifAgendaUFilter.t458_observaciones)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_PlanifAgendaU_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oPlanifAgendaU = new Models.PlanifAgendaU();
        //            oPlanifAgendaU.t458_idPlanif=Convert.ToInt32(dr["t458_idPlanif"]);
        //            oPlanifAgendaU.t001_idficepi=Convert.ToInt32(dr["t001_idficepi"]);
        //            oPlanifAgendaU.t001_idficepi_mod=Convert.ToInt32(dr["t001_idficepi_mod"]);
        //            oPlanifAgendaU.t458_fechamod=Convert.ToDateTime(dr["t458_fechamod"]);
        //            oPlanifAgendaU.t458_asunto=Convert.ToString(dr["t458_asunto"]);
        //            oPlanifAgendaU.t458_motivo=Convert.ToString(dr["t458_motivo"]);
        //            oPlanifAgendaU.t458_fechoraini=Convert.ToDateTime(dr["t458_fechoraini"]);
        //            oPlanifAgendaU.t458_fechorafin=Convert.ToDateTime(dr["t458_fechorafin"]);
        //            if(!Convert.IsDBNull(dr["t332_idtarea"]))
        //                oPlanifAgendaU.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oPlanifAgendaU.t458_privado=Convert.ToString(dr["t458_privado"]);
        //            oPlanifAgendaU.t458_observaciones=Convert.ToString(dr["t458_observaciones"]);

        //            lst.Add(oPlanifAgendaU);

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
				case enumDBFields.t458_idPlanif:
					paramName = "@t458_idPlanif";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t001_idficepi:
					paramName = "@t001_idficepi";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t001_idficepi_mod:
					paramName = "@t001_idficepi_mod";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t458_fechamod:
					paramName = "@t458_fechamod";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t458_asunto:
					paramName = "@t458_asunto";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t458_motivo:
					paramName = "@t458_motivo";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t458_fechoraini:
					paramName = "@t458_fechoraini";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t458_fechorafin:
					paramName = "@t458_fechorafin";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t332_idtarea:
					paramName = "@t332_idtarea";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t458_privado:
					paramName = "@t458_privado";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t458_observaciones:
					paramName = "@t458_observaciones";
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
