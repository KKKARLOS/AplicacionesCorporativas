using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Historial
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class Historial 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t431_denominacion = 1,
			t659_fecha = 2,
			Profesional = 3,
			t659_motivo = 4
        }

        internal Historial(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un Historial
        ///// </summary>
        //internal int Insert(Models.Historial oHistorial)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[4] {
        //            Param(enumDBFields.t431_denominacion, oHistorial.t431_denominacion),
        //            Param(enumDBFields.t659_fecha, oHistorial.t659_fecha),
        //            Param(enumDBFields.Profesional, oHistorial.Profesional),
        //            Param(enumDBFields.t659_motivo, oHistorial.t659_motivo)
        //        };

        //        return (int)cDblib.Execute("_Historial_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un Historial a partir del id
        ///// </summary>
        //internal Models.Historial Select()
        //{
        //    Models.Historial oHistorial = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_Historial_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oHistorial = new Models.Historial();
        //            if(!Convert.IsDBNull(dr["t431_denominacion"]))
        //                oHistorial.t431_denominacion=Convert.ToString(dr["t431_denominacion"]);
        //            oHistorial.t659_fecha=Convert.ToDateTime(dr["t659_fecha"]);
        //            if(!Convert.IsDBNull(dr["Profesional"]))
        //                oHistorial.Profesional=Convert.ToString(dr["Profesional"]);
        //            oHistorial.t659_motivo=Convert.ToString(dr["t659_motivo"]);

        //        }
        //        return oHistorial;
				
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
        ///// Actualiza un Historial a partir del id
        ///// </summary>
        //internal int Update(Models.Historial oHistorial)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[4] {
        //            Param(enumDBFields.t431_denominacion, oHistorial.t431_denominacion),
        //            Param(enumDBFields.t659_fecha, oHistorial.t659_fecha),
        //            Param(enumDBFields.Profesional, oHistorial.Profesional),
        //            Param(enumDBFields.t659_motivo, oHistorial.t659_motivo)
        //        };
                           
        //        return (int)cDblib.Execute("_Historial_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un Historial a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_Historial_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Obtiene todos los Historial
        /// </summary>
        //internal List<Models.Historial> Catalogo(Models.Historial oHistorialFilter)
        //{
        //    Models.Historial oHistorial = null;
        //    List<Models.Historial> lst = new List<Models.Historial>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[4] {
        //            Param(enumDBFields.t431_denominacion, oTEMP_HistorialFilter.t431_denominacion),
        //            Param(enumDBFields.t659_fecha, oTEMP_HistorialFilter.t659_fecha),
        //            Param(enumDBFields.Profesional, oTEMP_HistorialFilter.Profesional),
        //            Param(enumDBFields.t659_motivo, oTEMP_HistorialFilter.t659_motivo)
        //        };

        //        dr = cDblib.DataReader("_Historial_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oHistorial = new Models.Historial();
        //            if (!Convert.IsDBNull(dr["t431_denominacion"]))
        //                oHistorial.t431_denominacion = Convert.ToString(dr["t431_denominacion"]);
        //            oHistorial.t659_fecha = Convert.ToDateTime(dr["t659_fecha"]);
        //            if (!Convert.IsDBNull(dr["Profesional"]))
        //                oHistorial.Profesional = Convert.ToString(dr["Profesional"]);
        //            oHistorial.t659_motivo = Convert.ToString(dr["t659_motivo"]);

        //            lst.Add(oHistorial);

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
				case enumDBFields.t431_denominacion:
					paramName = "@t431_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 20;
					break;
				case enumDBFields.t659_fecha:
					paramName = "@t659_fecha";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.Profesional:
					paramName = "@Profesional";
					paramType = SqlDbType.VarChar;
					paramSize = 150;
					break;
				case enumDBFields.t659_motivo:
					paramName = "@t659_motivo";
					paramType = SqlDbType.VarChar;
					paramSize = 500;
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
