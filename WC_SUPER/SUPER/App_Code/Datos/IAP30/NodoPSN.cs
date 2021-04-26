using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for NodoPSN
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class NodoPSN 
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
                t305_idproyectosubnodo = 1
        }

        internal NodoPSN(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
        #endregion
	
        #region funciones publicas
    //    /// <summary>
    //    /// Inserta un NodoPSN
    //    /// </summary>
    //    internal int Insert(Models.NodoPSN oNodoPSN)
    //    {
    //        try
    //        {
    //            SqlParameter[] dbparams = new SqlParameter[1] {
    //                Param(enumDBFields.t303_idnodo, oNodoPSN.t303_idnodo)
    //            };

    //            return (int)cDblib.Execute("SUPER.IAP30_NodoPSN_INS", dbparams);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

		
    //    /// <summary>
         ////Obtiene un NodoPSN a partir del id
         ////</summary>
        internal Models.NodoPSN Select(int t305_idproyectosubnodo)
        {
            Models.NodoPSN oNodoPSN = null;
            IDataReader dr = null;

            try
            {
				
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t305_idproyectosubnodo, t305_idproyectosubnodo)
                };
                dr = cDblib.DataReader("SUP_GET_VARIOS_PSN", dbparams);
                if (dr.Read())
                {
                    oNodoPSN = new Models.NodoPSN();
                    oNodoPSN.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    oNodoPSN.t301_estado = Convert.ToString(dr["t301_estado"]);
                }
                return oNodoPSN;
				
            }
           catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }
		
    //    /// <summary>
    //    /// Actualiza un NodoPSN a partir del id
    //    /// </summary>
    //    internal int Update(Models.NodoPSN oNodoPSN)
    //    {
    //        try
    //        {
    //            SqlParameter[] dbparams = new SqlParameter[1] {
    //                Param(enumDBFields.t303_idnodo, oNodoPSN.t303_idnodo)
    //            };
                           
    //            return (int)cDblib.Execute("SUPER.IAP30_NodoPSN_UPD", dbparams);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
		
    //    /// <summary>
    //    /// Elimina un NodoPSN a partir del id
    //    /// </summary>
    //    internal int Delete()
    //    {
    //        try
    //        {
				
            
    //            return (int)cDblib.Execute("SUPER.IAP30_NodoPSN_DEL", dbparams);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    /// <summary>
    //    /// Obtiene todos los NodoPSN
    //    /// </summary>
    //    internal List<Models.NodoPSN> Catalogo(Models.NodoPSN oNodoPSNFilter)
    //    {
    //        Models.NodoPSN oNodoPSN = null;
    //        List<Models.NodoPSN> lst = new List<Models.NodoPSN>();
    //        IDataReader dr = null;

    //        try
    //        {
    //            SqlParameter[] dbparams = new SqlParameter[1] {
    //                Param(enumDBFields.t303_idnodo, oTEMP_NodoPSNFilter.t303_idnodo)
    //            };

    //            dr = cDblib.DataReader("SUPER.IAP30_NodoPSN_CAT", dbparams);
    //            while (dr.Read())
    //            {
    //                oNodoPSN = new Models.NodoPSN();
    //                oNodoPSN.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);

    //                lst.Add(oNodoPSN);

    //            }
    //            return lst;
			
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //        finally
    //        {
    //            if (dr != null)
    //            {
    //                if (!dr.IsClosed) dr.Close();
    //                dr.Dispose();
    //            }
    //        }
    //    }
		
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
                    paramName = "@nPSN";
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
