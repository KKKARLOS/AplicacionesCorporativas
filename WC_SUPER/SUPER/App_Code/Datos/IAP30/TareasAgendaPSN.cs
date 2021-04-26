using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TareasAgendaPSN
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class TareasAgendaPSN 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t305_idproyectosubnodo = 1,
			t301_idproyecto = 2,
			t305_seudonimo = 3,
			t301_denominacion = 4,
			t303_denominacion = 5,
			t302_denominacion = 6,
			responsable = 7
        }

        internal TareasAgendaPSN(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
		#region funciones publicas
        ///// <summary>
        ///// Inserta un TareasAgendaPSN
        ///// </summary>
        //internal int Insert(Models.TareasAgendaPSN oTareasAgendaPSN)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[7] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oTareasAgendaPSN.t305_idproyectosubnodo),
        //            Param(enumDBFields.t301_idproyecto, oTareasAgendaPSN.t301_idproyecto),
        //            Param(enumDBFields.t305_seudonimo, oTareasAgendaPSN.t305_seudonimo),
        //            Param(enumDBFields.t301_denominacion, oTareasAgendaPSN.t301_denominacion),
        //            Param(enumDBFields.t303_denominacion, oTareasAgendaPSN.t303_denominacion),
        //            Param(enumDBFields.t302_denominacion, oTareasAgendaPSN.t302_denominacion),
        //            Param(enumDBFields.responsable, oTareasAgendaPSN.responsable)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_TareasAgendaPSN_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un TareasAgendaPSN a partir del id
        ///// </summary>
        //internal Models.TareasAgendaPSN Select()
        //{
        //    Models.TareasAgendaPSN oTareasAgendaPSN = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_TareasAgendaPSN_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oTareasAgendaPSN = new Models.TareasAgendaPSN();
        //            oTareasAgendaPSN.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oTareasAgendaPSN.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);
        //            oTareasAgendaPSN.t305_seudonimo=Convert.ToString(dr["t305_seudonimo"]);
        //            oTareasAgendaPSN.t301_denominacion=Convert.ToString(dr["t301_denominacion"]);
        //            oTareasAgendaPSN.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
        //            oTareasAgendaPSN.t302_denominacion=Convert.ToString(dr["t302_denominacion"]);
        //            if(!Convert.IsDBNull(dr["responsable"]))
        //                oTareasAgendaPSN.responsable=Convert.ToString(dr["responsable"]);

        //        }
        //        return oTareasAgendaPSN;
				
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
        ///// Actualiza un TareasAgendaPSN a partir del id
        ///// </summary>
        //internal int Update(Models.TareasAgendaPSN oTareasAgendaPSN)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[7] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oTareasAgendaPSN.t305_idproyectosubnodo),
        //            Param(enumDBFields.t301_idproyecto, oTareasAgendaPSN.t301_idproyecto),
        //            Param(enumDBFields.t305_seudonimo, oTareasAgendaPSN.t305_seudonimo),
        //            Param(enumDBFields.t301_denominacion, oTareasAgendaPSN.t301_denominacion),
        //            Param(enumDBFields.t303_denominacion, oTareasAgendaPSN.t303_denominacion),
        //            Param(enumDBFields.t302_denominacion, oTareasAgendaPSN.t302_denominacion),
        //            Param(enumDBFields.responsable, oTareasAgendaPSN.responsable)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_TareasAgendaPSN_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un TareasAgendaPSN a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_TareasAgendaPSN_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los TareasAgendaPSN
        ///// </summary>
        //internal List<Models.TareasAgendaPSN> Catalogo(Models.TareasAgendaPSN oTareasAgendaPSNFilter)
        //{
        //    Models.TareasAgendaPSN oTareasAgendaPSN = null;
        //    List<Models.TareasAgendaPSN> lst = new List<Models.TareasAgendaPSN>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[7] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oTEMP_TareasAgendaPSNFilter.t305_idproyectosubnodo),
        //            Param(enumDBFields.t301_idproyecto, oTEMP_TareasAgendaPSNFilter.t301_idproyecto),
        //            Param(enumDBFields.t305_seudonimo, oTEMP_TareasAgendaPSNFilter.t305_seudonimo),
        //            Param(enumDBFields.t301_denominacion, oTEMP_TareasAgendaPSNFilter.t301_denominacion),
        //            Param(enumDBFields.t303_denominacion, oTEMP_TareasAgendaPSNFilter.t303_denominacion),
        //            Param(enumDBFields.t302_denominacion, oTEMP_TareasAgendaPSNFilter.t302_denominacion),
        //            Param(enumDBFields.responsable, oTEMP_TareasAgendaPSNFilter.responsable)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_TareasAgendaPSN_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oTareasAgendaPSN = new Models.TareasAgendaPSN();
        //            oTareasAgendaPSN.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oTareasAgendaPSN.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);
        //            oTareasAgendaPSN.t305_seudonimo=Convert.ToString(dr["t305_seudonimo"]);
        //            oTareasAgendaPSN.t301_denominacion=Convert.ToString(dr["t301_denominacion"]);
        //            oTareasAgendaPSN.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
        //            oTareasAgendaPSN.t302_denominacion=Convert.ToString(dr["t302_denominacion"]);
        //            if(!Convert.IsDBNull(dr["responsable"]))
        //                oTareasAgendaPSN.responsable=Convert.ToString(dr["responsable"]);

        //            lst.Add(oTareasAgendaPSN);

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
				case enumDBFields.t305_idproyectosubnodo:
					paramName = "@t305_idproyectosubnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
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
				case enumDBFields.t301_denominacion:
					paramName = "@t301_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 70;
					break;
				case enumDBFields.t303_denominacion:
					paramName = "@t303_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t302_denominacion:
					paramName = "@t302_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 100;
					break;
				case enumDBFields.responsable:
					paramName = "@responsable";
					paramType = SqlDbType.VarChar;
					paramSize = 173;
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
