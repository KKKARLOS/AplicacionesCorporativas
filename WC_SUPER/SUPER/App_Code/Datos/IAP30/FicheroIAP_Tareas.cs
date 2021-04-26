using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for FicheroIAP_Tareas
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class FicheroIAP_Tareas 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t332_idtarea = 1,
			t332_destarea = 2,
			t331_idpt = 3,
			t331_estado = 4,
			t332_estado = 5,
			t332_cle = 6,
			t332_tipocle = 7,
			t332_impiap = 8,
			t305_idproyectosubnodo = 9,
			t332_fiv = 10,
			t332_ffv = 11,
			t323_denominacion = 12,
			t323_regjornocompleta = 13,
			t323_regfes = 14,
			t331_obligaest = 15,
			t301_estado = 16
        }

        internal FicheroIAP_Tareas(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un FicheroIAP_Tareas
        ///// </summary>
        //internal int Insert(Models.FicheroIAP_Tareas oFicheroIAP_Tareas)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[16] {
        //            Param(enumDBFields.t332_idtarea, oFicheroIAP_Tareas.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oFicheroIAP_Tareas.t332_destarea),
        //            Param(enumDBFields.t331_idpt, oFicheroIAP_Tareas.t331_idpt),
        //            Param(enumDBFields.t331_estado, oFicheroIAP_Tareas.t331_estado),
        //            Param(enumDBFields.t332_estado, oFicheroIAP_Tareas.t332_estado),
        //            Param(enumDBFields.t332_cle, oFicheroIAP_Tareas.t332_cle),
        //            Param(enumDBFields.t332_tipocle, oFicheroIAP_Tareas.t332_tipocle),
        //            Param(enumDBFields.t332_impiap, oFicheroIAP_Tareas.t332_impiap),
        //            Param(enumDBFields.t305_idproyectosubnodo, oFicheroIAP_Tareas.t305_idproyectosubnodo),
        //            Param(enumDBFields.t332_fiv, oFicheroIAP_Tareas.t332_fiv),
        //            Param(enumDBFields.t332_ffv, oFicheroIAP_Tareas.t332_ffv),
        //            Param(enumDBFields.t323_denominacion, oFicheroIAP_Tareas.t323_denominacion),
        //            Param(enumDBFields.t323_regjornocompleta, oFicheroIAP_Tareas.t323_regjornocompleta),
        //            Param(enumDBFields.t323_regfes, oFicheroIAP_Tareas.t323_regfes),
        //            Param(enumDBFields.t331_obligaest, oFicheroIAP_Tareas.t331_obligaest),
        //            Param(enumDBFields.t301_estado, oFicheroIAP_Tareas.t301_estado)
        //        };

        //        return (int)cDblib.Execute("_FicheroIAP_Tareas_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un FicheroIAP_Tareas a partir del id
        ///// </summary>
        //internal Models.FicheroIAP_Tareas Select()
        //{
        //    Models.FicheroIAP_Tareas oFicheroIAP_Tareas = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_FicheroIAP_Tareas_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oFicheroIAP_Tareas = new Models.FicheroIAP_Tareas();
        //            oFicheroIAP_Tareas.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oFicheroIAP_Tareas.t332_destarea=Convert.ToString(dr["t332_destarea"]);
        //            oFicheroIAP_Tareas.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
        //            oFicheroIAP_Tareas.t331_estado=Convert.ToByte(dr["t331_estado"]);
        //            oFicheroIAP_Tareas.t332_estado=Convert.ToByte(dr["t332_estado"]);
        //            oFicheroIAP_Tareas.t332_cle=Convert.ToSingle(dr["t332_cle"]);
        //            oFicheroIAP_Tareas.t332_tipocle=Convert.ToString(dr["t332_tipocle"]);
        //            oFicheroIAP_Tareas.t332_impiap=Convert.ToBoolean(dr["t332_impiap"]);
        //            oFicheroIAP_Tareas.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oFicheroIAP_Tareas.t332_fiv=Convert.ToDateTime(dr["t332_fiv"]);
        //            if(!Convert.IsDBNull(dr["t332_ffv"]))
        //                oFicheroIAP_Tareas.t332_ffv=Convert.ToDateTime(dr["t332_ffv"]);
        //            oFicheroIAP_Tareas.t323_denominacion=Convert.ToString(dr["t323_denominacion"]);
        //            oFicheroIAP_Tareas.t323_regjornocompleta=Convert.ToBoolean(dr["t323_regjornocompleta"]);
        //            oFicheroIAP_Tareas.t323_regfes=Convert.ToBoolean(dr["t323_regfes"]);
        //            oFicheroIAP_Tareas.t331_obligaest=Convert.ToBoolean(dr["t331_obligaest"]);
        //            oFicheroIAP_Tareas.t301_estado=Convert.ToString(dr["t301_estado"]);

        //        }
        //        return oFicheroIAP_Tareas;
				
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
        ///// Actualiza un FicheroIAP_Tareas a partir del id
        ///// </summary>
        //internal int Update(Models.FicheroIAP_Tareas oFicheroIAP_Tareas)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[16] {
        //            Param(enumDBFields.t332_idtarea, oFicheroIAP_Tareas.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oFicheroIAP_Tareas.t332_destarea),
        //            Param(enumDBFields.t331_idpt, oFicheroIAP_Tareas.t331_idpt),
        //            Param(enumDBFields.t331_estado, oFicheroIAP_Tareas.t331_estado),
        //            Param(enumDBFields.t332_estado, oFicheroIAP_Tareas.t332_estado),
        //            Param(enumDBFields.t332_cle, oFicheroIAP_Tareas.t332_cle),
        //            Param(enumDBFields.t332_tipocle, oFicheroIAP_Tareas.t332_tipocle),
        //            Param(enumDBFields.t332_impiap, oFicheroIAP_Tareas.t332_impiap),
        //            Param(enumDBFields.t305_idproyectosubnodo, oFicheroIAP_Tareas.t305_idproyectosubnodo),
        //            Param(enumDBFields.t332_fiv, oFicheroIAP_Tareas.t332_fiv),
        //            Param(enumDBFields.t332_ffv, oFicheroIAP_Tareas.t332_ffv),
        //            Param(enumDBFields.t323_denominacion, oFicheroIAP_Tareas.t323_denominacion),
        //            Param(enumDBFields.t323_regjornocompleta, oFicheroIAP_Tareas.t323_regjornocompleta),
        //            Param(enumDBFields.t323_regfes, oFicheroIAP_Tareas.t323_regfes),
        //            Param(enumDBFields.t331_obligaest, oFicheroIAP_Tareas.t331_obligaest),
        //            Param(enumDBFields.t301_estado, oFicheroIAP_Tareas.t301_estado)
        //        };
                           
        //        return (int)cDblib.Execute("_FicheroIAP_Tareas_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un FicheroIAP_Tareas a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_FicheroIAP_Tareas_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los FicheroIAP_Tareas
        ///// </summary>
        //internal List<Models.FicheroIAP_Tareas> Catalogo(Models.FicheroIAP_Tareas oFicheroIAP_TareasFilter)
        //{
        //    Models.FicheroIAP_Tareas oFicheroIAP_Tareas = null;
        //    List<Models.FicheroIAP_Tareas> lst = new List<Models.FicheroIAP_Tareas>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[16] {
        //            Param(enumDBFields.t332_idtarea, oTEMP_FicheroIAP_TareasFilter.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oTEMP_FicheroIAP_TareasFilter.t332_destarea),
        //            Param(enumDBFields.t331_idpt, oTEMP_FicheroIAP_TareasFilter.t331_idpt),
        //            Param(enumDBFields.t331_estado, oTEMP_FicheroIAP_TareasFilter.t331_estado),
        //            Param(enumDBFields.t332_estado, oTEMP_FicheroIAP_TareasFilter.t332_estado),
        //            Param(enumDBFields.t332_cle, oTEMP_FicheroIAP_TareasFilter.t332_cle),
        //            Param(enumDBFields.t332_tipocle, oTEMP_FicheroIAP_TareasFilter.t332_tipocle),
        //            Param(enumDBFields.t332_impiap, oTEMP_FicheroIAP_TareasFilter.t332_impiap),
        //            Param(enumDBFields.t305_idproyectosubnodo, oTEMP_FicheroIAP_TareasFilter.t305_idproyectosubnodo),
        //            Param(enumDBFields.t332_fiv, oTEMP_FicheroIAP_TareasFilter.t332_fiv),
        //            Param(enumDBFields.t332_ffv, oTEMP_FicheroIAP_TareasFilter.t332_ffv),
        //            Param(enumDBFields.t323_denominacion, oTEMP_FicheroIAP_TareasFilter.t323_denominacion),
        //            Param(enumDBFields.t323_regjornocompleta, oTEMP_FicheroIAP_TareasFilter.t323_regjornocompleta),
        //            Param(enumDBFields.t323_regfes, oTEMP_FicheroIAP_TareasFilter.t323_regfes),
        //            Param(enumDBFields.t331_obligaest, oTEMP_FicheroIAP_TareasFilter.t331_obligaest),
        //            Param(enumDBFields.t301_estado, oTEMP_FicheroIAP_TareasFilter.t301_estado)
        //        };

        //        dr = cDblib.DataReader("_FicheroIAP_Tareas_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oFicheroIAP_Tareas = new Models.FicheroIAP_Tareas();
        //            oFicheroIAP_Tareas.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oFicheroIAP_Tareas.t332_destarea=Convert.ToString(dr["t332_destarea"]);
        //            oFicheroIAP_Tareas.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
        //            oFicheroIAP_Tareas.t331_estado=Convert.ToByte(dr["t331_estado"]);
        //            oFicheroIAP_Tareas.t332_estado=Convert.ToByte(dr["t332_estado"]);
        //            oFicheroIAP_Tareas.t332_cle=Convert.ToSingle(dr["t332_cle"]);
        //            oFicheroIAP_Tareas.t332_tipocle=Convert.ToString(dr["t332_tipocle"]);
        //            oFicheroIAP_Tareas.t332_impiap=Convert.ToBoolean(dr["t332_impiap"]);
        //            oFicheroIAP_Tareas.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oFicheroIAP_Tareas.t332_fiv=Convert.ToDateTime(dr["t332_fiv"]);
        //            if(!Convert.IsDBNull(dr["t332_ffv"]))
        //                oFicheroIAP_Tareas.t332_ffv=Convert.ToDateTime(dr["t332_ffv"]);
        //            oFicheroIAP_Tareas.t323_denominacion=Convert.ToString(dr["t323_denominacion"]);
        //            oFicheroIAP_Tareas.t323_regjornocompleta=Convert.ToBoolean(dr["t323_regjornocompleta"]);
        //            oFicheroIAP_Tareas.t323_regfes=Convert.ToBoolean(dr["t323_regfes"]);
        //            oFicheroIAP_Tareas.t331_obligaest=Convert.ToBoolean(dr["t331_obligaest"]);
        //            oFicheroIAP_Tareas.t301_estado=Convert.ToString(dr["t301_estado"]);

        //            lst.Add(oFicheroIAP_Tareas);

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
				case enumDBFields.t331_idpt:
					paramName = "@t331_idpt";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t331_estado:
					paramName = "@t331_estado";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.t332_estado:
					paramName = "@t332_estado";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.t332_cle:
					paramName = "@t332_cle";
					paramType = SqlDbType.Real;
					paramSize = 8;
					break;
				case enumDBFields.t332_tipocle:
					paramName = "@t332_tipocle";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.t332_impiap:
					paramName = "@t332_impiap";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t305_idproyectosubnodo:
					paramName = "@t305_idproyectosubnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_fiv:
					paramName = "@t332_fiv";
					paramType = SqlDbType.DateTime;
					paramSize = 4;
					break;
				case enumDBFields.t332_ffv:
					paramName = "@t332_ffv";
					paramType = SqlDbType.DateTime;
					paramSize = 4;
					break;
				case enumDBFields.t323_denominacion:
					paramName = "@t323_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t323_regjornocompleta:
					paramName = "@t323_regjornocompleta";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t323_regfes:
					paramName = "@t323_regfes";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t331_obligaest:
					paramName = "@t331_obligaest";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t301_estado:
					paramName = "@t301_estado";
					paramType = SqlDbType.Char;
					paramSize = 1;
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
