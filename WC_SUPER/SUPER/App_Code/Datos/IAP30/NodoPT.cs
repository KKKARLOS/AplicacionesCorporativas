using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for NodoPT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class NodoPT 
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t331_idpt = 1
        }

        internal NodoPT(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion
	
        //#region funciones publicas
        ///// <summary>
        ///// Inserta un NodoPT
        ///// </summary>
        //internal int Insert(Models.NodoPT oNodoPT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[1] {
        //            Param(enumDBFields.t303_idnodo, oNodoPT.t303_idnodo)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_NodoPT_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Obtiene un NodoPT a partir del id
        /// </summary>
        internal Models.NodoPT Select(int t331_idpt)
        {
            Models.NodoPT oNodoPT = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t331_idpt, t331_idpt)
                };

                dr = cDblib.DataReader("SUP_GET_NODO_PT", dbparams);
                if (dr.Read())
                {
                    oNodoPT = new Models.NodoPT();
                    oNodoPT.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);

                }
                return oNodoPT;

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
		
        ///// <summary>
        ///// Actualiza un NodoPT a partir del id
        ///// </summary>
        //internal int Update(Models.NodoPT oNodoPT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[1] {
        //            Param(enumDBFields.t303_idnodo, oNodoPT.t303_idnodo)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_NodoPT_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un NodoPT a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_NodoPT_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los NodoPT
        ///// </summary>
        //internal List<Models.NodoPT> Catalogo(Models.NodoPT oNodoPTFilter)
        //{
        //    Models.NodoPT oNodoPT = null;
        //    List<Models.NodoPT> lst = new List<Models.NodoPT>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[1] {
        //            Param(enumDBFields.t303_idnodo, oTEMP_NodoPTFilter.t303_idnodo)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_NodoPT_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oNodoPT = new Models.NodoPT();
        //            oNodoPT.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);

        //            lst.Add(oNodoPT);

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
                case enumDBFields.t331_idpt:
                    paramName = "@nPT";
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
