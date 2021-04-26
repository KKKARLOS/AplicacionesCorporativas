using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for CentroCoste
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class CentroCoste 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t317_idcencos = 1
        }

        internal CentroCoste(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un CentroCoste
        ///// </summary>
        //internal int Insert(Models.CentroCoste oCentroCoste)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[1] {
        //            Param(enumDBFields.t317_idcencos, oCentroCoste.t317_idcencos)
        //        };

        //        return (int)cDblib.Execute("_CentroCoste_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un CentroCoste a partir del id
        ///// </summary>
        //internal Models.CentroCoste Select()
        //{
        //    Models.CentroCoste oCentroCoste = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_CentroCoste_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oCentroCoste = new Models.CentroCoste();
        //            oCentroCoste.t317_idcencos=Convert.ToString(dr["t317_idcencos"]);

        //        }
        //        return oCentroCoste;
				
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
        ///// Actualiza un CentroCoste a partir del id
        ///// </summary>
        //internal int Update(Models.CentroCoste oCentroCoste)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[1] {
        //            Param(enumDBFields.t317_idcencos, oCentroCoste.t317_idcencos)
        //        };
                           
        //        return (int)cDblib.Execute("_CentroCoste_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un CentroCoste a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_CentroCoste_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los CentroCoste
        ///// </summary>
        //internal List<Models.CentroCoste> Catalogo(Models.CentroCoste oCentroCosteFilter)
        //{
        //    Models.CentroCoste oCentroCoste = null;
        //    List<Models.CentroCoste> lst = new List<Models.CentroCoste>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[1] {
        //            Param(enumDBFields.t317_idcencos, oTEMP_CentroCosteFilter.t317_idcencos)
        //        };

        //        dr = cDblib.DataReader("_CentroCoste_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oCentroCoste = new Models.CentroCoste();
        //            oCentroCoste.t317_idcencos=Convert.ToString(dr["t317_idcencos"]);

        //            lst.Add(oCentroCoste);

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
				case enumDBFields.t317_idcencos:
					paramName = "@t317_idcencos";
					paramType = SqlDbType.VarChar;
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
