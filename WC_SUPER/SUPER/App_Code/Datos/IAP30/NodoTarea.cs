using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for NodoTarea
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class NodoTarea 
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t332_idtarea = 1
        }

        internal NodoTarea(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
        #endregion
	
        #region funciones publicas

        ///// <summary>
        ///// Inserta un NodoTarea
        ///// </summary>
        //internal int Insert(Models.NodoTarea oNodoTarea)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[1] {
        //            Param(enumDBFields.t303_idnodo, oNodoTarea.t303_idnodo)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_NodoTarea_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un NodoTarea a partir del id
        ///// </summary>
        internal Models.NodoTarea Select(int t332_idtarea)
        {
            Models.NodoTarea oNodoTarea = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t332_idtarea, t332_idtarea)
                };

                dr = cDblib.DataReader("SUP_GET_NODO_TAREA", dbparams);
                if (dr.Read())
                {
                    oNodoTarea = new Models.NodoTarea();
                    oNodoTarea.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);

                }
                return oNodoTarea;

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
        ///// Actualiza un NodoTarea a partir del id
        ///// </summary>
        //internal int Update(Models.NodoTarea oNodoTarea)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[1] {
        //            Param(enumDBFields.t303_idnodo, oNodoTarea.t303_idnodo)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_NodoTarea_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un NodoTarea a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_NodoTarea_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los NodoTarea
        ///// </summary>
        //internal List<Models.NodoTarea> Catalogo(Models.NodoTarea oNodoTareaFilter)
        //{
        //    Models.NodoTarea oNodoTarea = null;
        //    List<Models.NodoTarea> lst = new List<Models.NodoTarea>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[1] {
        //            Param(enumDBFields.t303_idnodo, oTEMP_NodoTareaFilter.t303_idnodo)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_NodoTarea_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oNodoTarea = new Models.NodoTarea();
        //            oNodoTarea.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);

        //            lst.Add(oNodoTarea);

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
                case enumDBFields.t332_idtarea:
                    paramName = "@nTarea";
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
