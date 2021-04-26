using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ProyectoConsumoIAP
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ProyectoConsumoIAP 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t305_idproyectosubnodo = 1,
			t305_seudonimo = 2,
			t301_idproyecto = 3
        }

        internal ProyectoConsumoIAP(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un ProyectoConsumoIAP
        ///// </summary>
        //internal int Insert(Models.ProyectoConsumoIAP oProyectoConsumoIAP)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oProyectoConsumoIAP.t305_idproyectosubnodo),
        //            Param(enumDBFields.t305_seudonimo, oProyectoConsumoIAP.t305_seudonimo),
        //            Param(enumDBFields.t301_idproyecto, oProyectoConsumoIAP.t301_idproyecto)
        //        };

        //        return (int)cDblib.Execute("_ProyectoConsumoIAP_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un ProyectoConsumoIAP a partir del id
        ///// </summary>
        //internal Models.ProyectoConsumoIAP Select()
        //{
        //    Models.ProyectoConsumoIAP oProyectoConsumoIAP = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_ProyectoConsumoIAP_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oProyectoConsumoIAP = new Models.ProyectoConsumoIAP();
        //            oProyectoConsumoIAP.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            if(!Convert.IsDBNull(dr["t305_seudonimo"]))
        //                oProyectoConsumoIAP.t305_seudonimo=Convert.ToString(dr["t305_seudonimo"]);
        //            oProyectoConsumoIAP.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);

        //        }
        //        return oProyectoConsumoIAP;
				
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
        ///// Actualiza un ProyectoConsumoIAP a partir del id
        ///// </summary>
        //internal int Update(Models.ProyectoConsumoIAP oProyectoConsumoIAP)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oProyectoConsumoIAP.t305_idproyectosubnodo),
        //            Param(enumDBFields.t305_seudonimo, oProyectoConsumoIAP.t305_seudonimo),
        //            Param(enumDBFields.t301_idproyecto, oProyectoConsumoIAP.t301_idproyecto)
        //        };
                           
        //        return (int)cDblib.Execute("_ProyectoConsumoIAP_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un ProyectoConsumoIAP a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_ProyectoConsumoIAP_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los ProyectoConsumoIAP
        ///// </summary>
        //internal List<Models.ProyectoConsumoIAP> Catalogo(Models.ProyectoConsumoIAP oProyectoConsumoIAPFilter)
        //{
        //    Models.ProyectoConsumoIAP oProyectoConsumoIAP = null;
        //    List<Models.ProyectoConsumoIAP> lst = new List<Models.ProyectoConsumoIAP>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oTEMP_ProyectoConsumoIAPFilter.t305_idproyectosubnodo),
        //            Param(enumDBFields.t305_seudonimo, oTEMP_ProyectoConsumoIAPFilter.t305_seudonimo),
        //            Param(enumDBFields.t301_idproyecto, oTEMP_ProyectoConsumoIAPFilter.t301_idproyecto)
        //        };

        //        dr = cDblib.DataReader("_ProyectoConsumoIAP_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oProyectoConsumoIAP = new Models.ProyectoConsumoIAP();
        //            oProyectoConsumoIAP.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            if(!Convert.IsDBNull(dr["t305_seudonimo"]))
        //                oProyectoConsumoIAP.t305_seudonimo=Convert.ToString(dr["t305_seudonimo"]);
        //            oProyectoConsumoIAP.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);

        //            lst.Add(oProyectoConsumoIAP);

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
				case enumDBFields.t305_seudonimo:
					paramName = "@t305_seudonimo";
					paramType = SqlDbType.VarChar;
					paramSize = 103;
					break;
				case enumDBFields.t301_idproyecto:
					paramName = "@t301_idproyecto";
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
