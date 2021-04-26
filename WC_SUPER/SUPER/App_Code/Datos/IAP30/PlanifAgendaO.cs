using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for PlanifAgendaO
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class PlanifAgendaO 
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
			t458_observaciones = 11,
			t332_destarea = 12,
			Profesional = 13,
			Promotor = 14,
			codred_profesional = 15,
			codred_promotor = 16
        }

        internal PlanifAgendaO(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un PlanifAgendaO
        ///// </summary>
        //internal int Insert(Models.PlanifAgendaO oPlanifAgendaO)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[16] {
        //            Param(enumDBFields.t458_idPlanif, oPlanifAgendaO.t458_idPlanif),
        //            Param(enumDBFields.t001_idficepi, oPlanifAgendaO.t001_idficepi),
        //            Param(enumDBFields.t001_idficepi_mod, oPlanifAgendaO.t001_idficepi_mod),
        //            Param(enumDBFields.t458_fechamod, oPlanifAgendaO.t458_fechamod),
        //            Param(enumDBFields.t458_asunto, oPlanifAgendaO.t458_asunto),
        //            Param(enumDBFields.t458_motivo, oPlanifAgendaO.t458_motivo),
        //            Param(enumDBFields.t458_fechoraini, oPlanifAgendaO.t458_fechoraini),
        //            Param(enumDBFields.t458_fechorafin, oPlanifAgendaO.t458_fechorafin),
        //            Param(enumDBFields.t332_idtarea, oPlanifAgendaO.t332_idtarea),
        //            Param(enumDBFields.t458_privado, oPlanifAgendaO.t458_privado),
        //            Param(enumDBFields.t458_observaciones, oPlanifAgendaO.t458_observaciones),
        //            Param(enumDBFields.t332_destarea, oPlanifAgendaO.t332_destarea),
        //            Param(enumDBFields.Profesional, oPlanifAgendaO.Profesional),
        //            Param(enumDBFields.Promotor, oPlanifAgendaO.Promotor),
        //            Param(enumDBFields.codred_profesional, oPlanifAgendaO.codred_profesional),
        //            Param(enumDBFields.codred_promotor, oPlanifAgendaO.codred_promotor)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_PlanifAgendaO_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un PlanifAgendaO a partir del id
        ///// </summary>
        //internal Models.PlanifAgendaO Select()
        //{
        //    Models.PlanifAgendaO oPlanifAgendaO = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_PlanifAgendaO_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oPlanifAgendaO = new Models.PlanifAgendaO();
        //            oPlanifAgendaO.t458_idPlanif=Convert.ToInt32(dr["t458_idPlanif"]);
        //            oPlanifAgendaO.t001_idficepi=Convert.ToInt32(dr["t001_idficepi"]);
        //            oPlanifAgendaO.t001_idficepi_mod=Convert.ToInt32(dr["t001_idficepi_mod"]);
        //            oPlanifAgendaO.t458_fechamod=Convert.ToDateTime(dr["t458_fechamod"]);
        //            oPlanifAgendaO.t458_asunto=Convert.ToString(dr["t458_asunto"]);
        //            oPlanifAgendaO.t458_motivo=Convert.ToString(dr["t458_motivo"]);
        //            oPlanifAgendaO.t458_fechoraini=Convert.ToDateTime(dr["t458_fechoraini"]);
        //            oPlanifAgendaO.t458_fechorafin=Convert.ToDateTime(dr["t458_fechorafin"]);
        //            if(!Convert.IsDBNull(dr["t332_idtarea"]))
        //                oPlanifAgendaO.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oPlanifAgendaO.t458_privado=Convert.ToString(dr["t458_privado"]);
        //            oPlanifAgendaO.t458_observaciones=Convert.ToString(dr["t458_observaciones"]);
        //            oPlanifAgendaO.t332_destarea=Convert.ToString(dr["t332_destarea"]);
        //            oPlanifAgendaO.Profesional=Convert.ToString(dr["Profesional"]);
        //            oPlanifAgendaO.Promotor=Convert.ToString(dr["Promotor"]);
        //            oPlanifAgendaO.codred_profesional=Convert.ToString(dr["codred_profesional"]);
        //            oPlanifAgendaO.codred_promotor=Convert.ToString(dr["codred_promotor"]);

        //        }
        //        return oPlanifAgendaO;
				
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
        ///// Actualiza un PlanifAgendaO a partir del id
        ///// </summary>
        //internal int Update(Models.PlanifAgendaO oPlanifAgendaO)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[16] {
        //            Param(enumDBFields.t458_idPlanif, oPlanifAgendaO.t458_idPlanif),
        //            Param(enumDBFields.t001_idficepi, oPlanifAgendaO.t001_idficepi),
        //            Param(enumDBFields.t001_idficepi_mod, oPlanifAgendaO.t001_idficepi_mod),
        //            Param(enumDBFields.t458_fechamod, oPlanifAgendaO.t458_fechamod),
        //            Param(enumDBFields.t458_asunto, oPlanifAgendaO.t458_asunto),
        //            Param(enumDBFields.t458_motivo, oPlanifAgendaO.t458_motivo),
        //            Param(enumDBFields.t458_fechoraini, oPlanifAgendaO.t458_fechoraini),
        //            Param(enumDBFields.t458_fechorafin, oPlanifAgendaO.t458_fechorafin),
        //            Param(enumDBFields.t332_idtarea, oPlanifAgendaO.t332_idtarea),
        //            Param(enumDBFields.t458_privado, oPlanifAgendaO.t458_privado),
        //            Param(enumDBFields.t458_observaciones, oPlanifAgendaO.t458_observaciones),
        //            Param(enumDBFields.t332_destarea, oPlanifAgendaO.t332_destarea),
        //            Param(enumDBFields.Profesional, oPlanifAgendaO.Profesional),
        //            Param(enumDBFields.Promotor, oPlanifAgendaO.Promotor),
        //            Param(enumDBFields.codred_profesional, oPlanifAgendaO.codred_profesional),
        //            Param(enumDBFields.codred_promotor, oPlanifAgendaO.codred_promotor)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_PlanifAgendaO_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un PlanifAgendaO a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_PlanifAgendaO_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los PlanifAgendaO
        ///// </summary>
        //internal List<Models.PlanifAgendaO> Catalogo(Models.PlanifAgendaO oPlanifAgendaOFilter)
        //{
        //    Models.PlanifAgendaO oPlanifAgendaO = null;
        //    List<Models.PlanifAgendaO> lst = new List<Models.PlanifAgendaO>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[16] {
        //            Param(enumDBFields.t458_idPlanif, oTEMP_PlanifAgendaOFilter.t458_idPlanif),
        //            Param(enumDBFields.t001_idficepi, oTEMP_PlanifAgendaOFilter.t001_idficepi),
        //            Param(enumDBFields.t001_idficepi_mod, oTEMP_PlanifAgendaOFilter.t001_idficepi_mod),
        //            Param(enumDBFields.t458_fechamod, oTEMP_PlanifAgendaOFilter.t458_fechamod),
        //            Param(enumDBFields.t458_asunto, oTEMP_PlanifAgendaOFilter.t458_asunto),
        //            Param(enumDBFields.t458_motivo, oTEMP_PlanifAgendaOFilter.t458_motivo),
        //            Param(enumDBFields.t458_fechoraini, oTEMP_PlanifAgendaOFilter.t458_fechoraini),
        //            Param(enumDBFields.t458_fechorafin, oTEMP_PlanifAgendaOFilter.t458_fechorafin),
        //            Param(enumDBFields.t332_idtarea, oTEMP_PlanifAgendaOFilter.t332_idtarea),
        //            Param(enumDBFields.t458_privado, oTEMP_PlanifAgendaOFilter.t458_privado),
        //            Param(enumDBFields.t458_observaciones, oTEMP_PlanifAgendaOFilter.t458_observaciones),
        //            Param(enumDBFields.t332_destarea, oTEMP_PlanifAgendaOFilter.t332_destarea),
        //            Param(enumDBFields.Profesional, oTEMP_PlanifAgendaOFilter.Profesional),
        //            Param(enumDBFields.Promotor, oTEMP_PlanifAgendaOFilter.Promotor),
        //            Param(enumDBFields.codred_profesional, oTEMP_PlanifAgendaOFilter.codred_profesional),
        //            Param(enumDBFields.codred_promotor, oTEMP_PlanifAgendaOFilter.codred_promotor)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_PlanifAgendaO_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oPlanifAgendaO = new Models.PlanifAgendaO();
        //            oPlanifAgendaO.t458_idPlanif=Convert.ToInt32(dr["t458_idPlanif"]);
        //            oPlanifAgendaO.t001_idficepi=Convert.ToInt32(dr["t001_idficepi"]);
        //            oPlanifAgendaO.t001_idficepi_mod=Convert.ToInt32(dr["t001_idficepi_mod"]);
        //            oPlanifAgendaO.t458_fechamod=Convert.ToDateTime(dr["t458_fechamod"]);
        //            oPlanifAgendaO.t458_asunto=Convert.ToString(dr["t458_asunto"]);
        //            oPlanifAgendaO.t458_motivo=Convert.ToString(dr["t458_motivo"]);
        //            oPlanifAgendaO.t458_fechoraini=Convert.ToDateTime(dr["t458_fechoraini"]);
        //            oPlanifAgendaO.t458_fechorafin=Convert.ToDateTime(dr["t458_fechorafin"]);
        //            if(!Convert.IsDBNull(dr["t332_idtarea"]))
        //                oPlanifAgendaO.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oPlanifAgendaO.t458_privado=Convert.ToString(dr["t458_privado"]);
        //            oPlanifAgendaO.t458_observaciones=Convert.ToString(dr["t458_observaciones"]);
        //            oPlanifAgendaO.t332_destarea=Convert.ToString(dr["t332_destarea"]);
        //            oPlanifAgendaO.Profesional=Convert.ToString(dr["Profesional"]);
        //            oPlanifAgendaO.Promotor=Convert.ToString(dr["Promotor"]);
        //            oPlanifAgendaO.codred_profesional=Convert.ToString(dr["codred_profesional"]);
        //            oPlanifAgendaO.codred_promotor=Convert.ToString(dr["codred_promotor"]);

        //            lst.Add(oPlanifAgendaO);

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
				case enumDBFields.t332_destarea:
					paramName = "@t332_destarea";
					paramType = SqlDbType.VarChar;
					paramSize = 100;
					break;
				case enumDBFields.Profesional:
					paramName = "@Profesional";
					paramType = SqlDbType.VarChar;
					paramSize = 73;
					break;
				case enumDBFields.Promotor:
					paramName = "@Promotor";
					paramType = SqlDbType.VarChar;
					paramSize = 73;
					break;
				case enumDBFields.codred_profesional:
					paramName = "@codred_profesional";
					paramType = SqlDbType.VarChar;
					paramSize = 15;
					break;
				case enumDBFields.codred_promotor:
					paramName = "@codred_promotor";
					paramType = SqlDbType.VarChar;
					paramSize = 15;
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
