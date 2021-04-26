using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for FiguraNodo
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class FiguraNodo 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t303_idnodo = 1,
			t314_idusuario = 2,
			t308_figura = 3
        }

        internal FiguraNodo(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un FiguraNodo
        ///// </summary>
        //internal int Insert(Models.FiguraNodo oFiguraNodo)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.t303_idnodo, oFiguraNodo.t303_idnodo),
        //            Param(enumDBFields.t314_idusuario, oFiguraNodo.t314_idusuario),
        //            Param(enumDBFields.t308_figura, oFiguraNodo.t308_figura)
        //        };

        //        return (int)cDblib.Execute("_FiguraNodo_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un FiguraNodo a partir del id
        ///// </summary>
        //internal Models.FiguraNodo Select()
        //{
        //    Models.FiguraNodo oFiguraNodo = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_FiguraNodo_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oFiguraNodo = new Models.FiguraNodo();
        //            oFiguraNodo.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
        //            oFiguraNodo.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            oFiguraNodo.t308_figura=Convert.ToString(dr["t308_figura"]);

        //        }
        //        return oFiguraNodo;
				
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
        ///// Actualiza un FiguraNodo a partir del id
        ///// </summary>
        //internal int Update(Models.FiguraNodo oFiguraNodo)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.t303_idnodo, oFiguraNodo.t303_idnodo),
        //            Param(enumDBFields.t314_idusuario, oFiguraNodo.t314_idusuario),
        //            Param(enumDBFields.t308_figura, oFiguraNodo.t308_figura)
        //        };
                           
        //        return (int)cDblib.Execute("_FiguraNodo_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un FiguraNodo a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_FiguraNodo_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los FiguraNodo
        ///// </summary>
        internal List<Models.FiguraNodo> Catalogo(Models.FiguraNodo oFiguraNodoFilter)
        {
            Models.FiguraNodo oFiguraNodo = null;
            List<Models.FiguraNodo> lst = new List<Models.FiguraNodo>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t303_idnodo, oFiguraNodoFilter.t303_idnodo),
                    Param(enumDBFields.t314_idusuario, oFiguraNodoFilter.t314_idusuario),
                    Param(enumDBFields.t308_figura, oFiguraNodoFilter.t308_figura)
                };

                dr = cDblib.DataReader("SUP_FIGURANODO_C", dbparams);
                while (dr.Read())
                {
                    oFiguraNodo = new Models.FiguraNodo();
                    oFiguraNodo.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    oFiguraNodo.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
                    oFiguraNodo.t308_figura = Convert.ToString(dr["t308_figura"]);

                    lst.Add(oFiguraNodo);

                }
                return lst;
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
				case enumDBFields.t303_idnodo:
					paramName = "@t303_idnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t314_idusuario:
					paramName = "@t314_idusuario";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t308_figura:
					paramName = "@t308_figura";
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
