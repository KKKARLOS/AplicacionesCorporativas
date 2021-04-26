using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for FicherosManiobra
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class FicherosManiobra 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t722_idtipo = 1,
			t001_idficepi = 2,
			t722_fichero = 3
        }

        internal FicherosManiobra(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un FicherosManiobra
        ///// </summary>
        //internal int Insert(Models.FicherosManiobra oFicherosManiobra)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.t722_idtipo, oFicherosManiobra.t722_idtipo),
        //            Param(enumDBFields.t001_idficepi, oFicherosManiobra.t001_idficepi),
        //            Param(enumDBFields.t722_fichero, oFicherosManiobra.t722_fichero)
        //        };

        //        return (int)cDblib.Execute("_FicherosManiobra_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un FicherosManiobra a partir del id
        ///// </summary>
        //internal Models.FicherosManiobra Select()
        //{
        //    Models.FicherosManiobra oFicherosManiobra = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_FicherosManiobra_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oFicherosManiobra = new Models.FicherosManiobra();
        //            oFicherosManiobra.t722_idtipo=Convert.ToByte(dr["t722_idtipo"]);
        //            oFicherosManiobra.t001_idficepi=Convert.ToInt32(dr["t001_idficepi"]);
        //            if(!Convert.IsDBNull(dr["t722_fichero"]))
        //                oFicherosManiobra.t722_fichero=Convert.ToByte[](dr["t722_fichero"]);

        //        }
        //        return oFicherosManiobra;
				
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
        ///// Actualiza un FicherosManiobra a partir del id
        ///// </summary>
        //internal int Update(Models.FicherosManiobra oFicherosManiobra)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.t722_idtipo, oFicherosManiobra.t722_idtipo),
        //            Param(enumDBFields.t001_idficepi, oFicherosManiobra.t001_idficepi),
        //            Param(enumDBFields.t722_fichero, oFicherosManiobra.t722_fichero)
        //        };
                           
        //        return (int)cDblib.Execute("_FicherosManiobra_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un FicherosManiobra a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_FicherosManiobra_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los FicherosManiobra
        ///// </summary>
        //internal List<Models.FicherosManiobra> Catalogo(Models.FicherosManiobra oFicherosManiobraFilter)
        //{
        //    Models.FicherosManiobra oFicherosManiobra = null;
        //    List<Models.FicherosManiobra> lst = new List<Models.FicherosManiobra>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.t722_idtipo, oTEMP_FicherosManiobraFilter.t722_idtipo),
        //            Param(enumDBFields.t001_idficepi, oTEMP_FicherosManiobraFilter.t001_idficepi),
        //            Param(enumDBFields.t722_fichero, oTEMP_FicherosManiobraFilter.t722_fichero)
        //        };

        //        dr = cDblib.DataReader("_FicherosManiobra_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oFicherosManiobra = new Models.FicherosManiobra();
        //            oFicherosManiobra.t722_idtipo=Convert.ToByte(dr["t722_idtipo"]);
        //            oFicherosManiobra.t001_idficepi=Convert.ToInt32(dr["t001_idficepi"]);
        //            if(!Convert.IsDBNull(dr["t722_fichero"]))
        //                oFicherosManiobra.t722_fichero=Convert.ToByte[](dr["t722_fichero"]);

        //            lst.Add(oFicherosManiobra);

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
				case enumDBFields.t722_idtipo:
					paramName = "@t722_idtipo";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.t001_idficepi:
					paramName = "@t001_idficepi";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t722_fichero:
					paramName = "@t722_fichero";
					paramType = SqlDbType.Binary;
					paramSize = 2147483647;
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
