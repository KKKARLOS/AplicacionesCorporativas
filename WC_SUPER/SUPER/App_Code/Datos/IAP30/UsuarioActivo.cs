using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for UsuarioActivo
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class UsuarioActivo 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t314_idusuario = 1,
			Profesional = 2,
			t303_idnodo = 3,
			t001_sexo = 4,
			t001_idficepi = 5,
			EMPRESA = 6,
			t303_denominacion = 7,
			tipo = 8
        }

        internal UsuarioActivo(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un UsuarioActivo
        ///// </summary>
        //internal int Insert(Models.UsuarioActivo oUsuarioActivo)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[8] {
        //            Param(enumDBFields.t314_idusuario, oUsuarioActivo.t314_idusuario),
        //            Param(enumDBFields.Profesional, oUsuarioActivo.Profesional),
        //            Param(enumDBFields.t303_idnodo, oUsuarioActivo.t303_idnodo),
        //            Param(enumDBFields.t001_sexo, oUsuarioActivo.t001_sexo),
        //            Param(enumDBFields.t001_idficepi, oUsuarioActivo.t001_idficepi),
        //            Param(enumDBFields.EMPRESA, oUsuarioActivo.EMPRESA),
        //            Param(enumDBFields.t303_denominacion, oUsuarioActivo.t303_denominacion),
        //            Param(enumDBFields.tipo, oUsuarioActivo.tipo)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_UsuarioActivo_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un UsuarioActivo a partir del id
        ///// </summary>
        //internal Models.UsuarioActivo Select()
        //{
        //    Models.UsuarioActivo oUsuarioActivo = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_UsuarioActivo_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oUsuarioActivo = new Models.UsuarioActivo();
        //            oUsuarioActivo.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            if(!Convert.IsDBNull(dr["Profesional"]))
        //                oUsuarioActivo.Profesional=Convert.ToString(dr["Profesional"]);
        //            if(!Convert.IsDBNull(dr["t303_idnodo"]))
        //                oUsuarioActivo.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
        //            oUsuarioActivo.t001_sexo=Convert.ToString(dr["t001_sexo"]);
        //            oUsuarioActivo.t001_idficepi=Convert.ToInt32(dr["t001_idficepi"]);
        //            if(!Convert.IsDBNull(dr["EMPRESA"]))
        //                oUsuarioActivo.EMPRESA=Convert.ToString(dr["EMPRESA"]);
        //            oUsuarioActivo.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
        //            if(!Convert.IsDBNull(dr["tipo"]))
        //                oUsuarioActivo.tipo=Convert.ToString(dr["tipo"]);

        //        }
        //        return oUsuarioActivo;
				
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
        ///// Actualiza un UsuarioActivo a partir del id
        ///// </summary>
        //internal int Update(Models.UsuarioActivo oUsuarioActivo)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[8] {
        //            Param(enumDBFields.t314_idusuario, oUsuarioActivo.t314_idusuario),
        //            Param(enumDBFields.Profesional, oUsuarioActivo.Profesional),
        //            Param(enumDBFields.t303_idnodo, oUsuarioActivo.t303_idnodo),
        //            Param(enumDBFields.t001_sexo, oUsuarioActivo.t001_sexo),
        //            Param(enumDBFields.t001_idficepi, oUsuarioActivo.t001_idficepi),
        //            Param(enumDBFields.EMPRESA, oUsuarioActivo.EMPRESA),
        //            Param(enumDBFields.t303_denominacion, oUsuarioActivo.t303_denominacion),
        //            Param(enumDBFields.tipo, oUsuarioActivo.tipo)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_UsuarioActivo_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un UsuarioActivo a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_UsuarioActivo_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los UsuarioActivo
        ///// </summary>
        //internal List<Models.UsuarioActivo> Catalogo(Models.UsuarioActivo oUsuarioActivoFilter)
        //{
        //    Models.UsuarioActivo oUsuarioActivo = null;
        //    List<Models.UsuarioActivo> lst = new List<Models.UsuarioActivo>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[8] {
        //            Param(enumDBFields.t314_idusuario, oUsuarioActivoFilter.t314_idusuario),
        //            Param(enumDBFields.Profesional, oUsuarioActivoFilter.Profesional),
        //            Param(enumDBFields.t303_idnodo, oUsuarioActivoFilter.t303_idnodo),
        //            Param(enumDBFields.t001_sexo, oUsuarioActivoFilter.t001_sexo),
        //            Param(enumDBFields.t001_idficepi, oUsuarioActivoFilter.t001_idficepi),
        //            Param(enumDBFields.EMPRESA, oUsuarioActivoFilter.EMPRESA),
        //            Param(enumDBFields.t303_denominacion, oUsuarioActivoFilter.t303_denominacion),
        //            Param(enumDBFields.tipo, oUsuarioActivoFilter.tipo)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_UsuarioActivo_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oUsuarioActivo = new Models.UsuarioActivo();
        //            oUsuarioActivo.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            if(!Convert.IsDBNull(dr["Profesional"]))
        //                oUsuarioActivo.Profesional=Convert.ToString(dr["Profesional"]);
        //            if(!Convert.IsDBNull(dr["t303_idnodo"]))
        //                oUsuarioActivo.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
        //            oUsuarioActivo.t001_sexo=Convert.ToString(dr["t001_sexo"]);
        //            oUsuarioActivo.t001_idficepi=Convert.ToInt32(dr["t001_idficepi"]);
        //            if(!Convert.IsDBNull(dr["EMPRESA"]))
        //                oUsuarioActivo.EMPRESA=Convert.ToString(dr["EMPRESA"]);
        //            oUsuarioActivo.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
        //            if(!Convert.IsDBNull(dr["tipo"]))
        //                oUsuarioActivo.tipo=Convert.ToString(dr["tipo"]);

        //            lst.Add(oUsuarioActivo);

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
				case enumDBFields.t314_idusuario:
					paramName = "@t314_idusuario";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.Profesional:
					paramName = "@Profesional";
					paramType = SqlDbType.VarChar;
					paramSize = 150;
					break;
				case enumDBFields.t303_idnodo:
					paramName = "@t303_idnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t001_sexo:
					paramName = "@t001_sexo";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.t001_idficepi:
					paramName = "@t001_idficepi";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.EMPRESA:
					paramName = "@EMPRESA";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t303_denominacion:
					paramName = "@t303_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.tipo:
					paramName = "@tipo";
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
