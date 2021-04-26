using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for FechasUsuarioPSN
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class FechasUsuarioPSN 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t305_idproyectosubnodo = 1,
			t330_falta = 2,
			t330_fbaja = 3
        }

        internal FechasUsuarioPSN(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un FechasUsuarioPSN
        ///// </summary>
        //internal int Insert(Models.FechasUsuarioPSN oFechasUsuarioPSN)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oFechasUsuarioPSN.t305_idproyectosubnodo),
        //            Param(enumDBFields.t330_falta, oFechasUsuarioPSN.t330_falta),
        //            Param(enumDBFields.t330_fbaja, oFechasUsuarioPSN.t330_fbaja)
        //        };

        //        return (int)cDblib.Execute("_FechasUsuarioPSN_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un FechasUsuarioPSN a partir del id
        ///// </summary>
        //internal Models.FechasUsuarioPSN Select()
        //{
        //    Models.FechasUsuarioPSN oFechasUsuarioPSN = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_FechasUsuarioPSN_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oFechasUsuarioPSN = new Models.FechasUsuarioPSN();
        //            oFechasUsuarioPSN.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oFechasUsuarioPSN.t330_falta=Convert.ToDateTime(dr["t330_falta"]);
        //            if(!Convert.IsDBNull(dr["t330_fbaja"]))
        //                oFechasUsuarioPSN.t330_fbaja=Convert.ToDateTime(dr["t330_fbaja"]);

        //        }
        //        return oFechasUsuarioPSN;
				
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
        ///// Actualiza un FechasUsuarioPSN a partir del id
        ///// </summary>
        //internal int Update(Models.FechasUsuarioPSN oFechasUsuarioPSN)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oFechasUsuarioPSN.t305_idproyectosubnodo),
        //            Param(enumDBFields.t330_falta, oFechasUsuarioPSN.t330_falta),
        //            Param(enumDBFields.t330_fbaja, oFechasUsuarioPSN.t330_fbaja)
        //        };
                           
        //        return (int)cDblib.Execute("_FechasUsuarioPSN_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un FechasUsuarioPSN a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_FechasUsuarioPSN_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los FechasUsuarioPSN
        ///// </summary>
        //internal List<Models.FechasUsuarioPSN> Catalogo(Models.FechasUsuarioPSN oFechasUsuarioPSNFilter)
        //{
        //    Models.FechasUsuarioPSN oFechasUsuarioPSN = null;
        //    List<Models.FechasUsuarioPSN> lst = new List<Models.FechasUsuarioPSN>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oTEMP_FechasUsuarioPSNFilter.t305_idproyectosubnodo),
        //            Param(enumDBFields.t330_falta, oTEMP_FechasUsuarioPSNFilter.t330_falta),
        //            Param(enumDBFields.t330_fbaja, oTEMP_FechasUsuarioPSNFilter.t330_fbaja)
        //        };

        //        dr = cDblib.DataReader("_FechasUsuarioPSN_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oFechasUsuarioPSN = new Models.FechasUsuarioPSN();
        //            oFechasUsuarioPSN.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oFechasUsuarioPSN.t330_falta=Convert.ToDateTime(dr["t330_falta"]);
        //            if(!Convert.IsDBNull(dr["t330_fbaja"]))
        //                oFechasUsuarioPSN.t330_fbaja=Convert.ToDateTime(dr["t330_fbaja"]);

        //            lst.Add(oFechasUsuarioPSN);

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
				case enumDBFields.t305_idproyectosubnodo:
					paramName = "@t305_idproyectosubnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t330_falta:
					paramName = "@t330_falta";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t330_fbaja:
					paramName = "@t330_fbaja";
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
