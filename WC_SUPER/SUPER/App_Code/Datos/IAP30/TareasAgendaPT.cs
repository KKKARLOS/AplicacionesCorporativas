using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TareasAgendaPT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class TareasAgendaPT 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			nivel = 1,
			t331_idpt = 2,
			t331_despt = 3,
			t331_orden = 4
        }

        internal TareasAgendaPT(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un TareasAgendaPT
        ///// </summary>
        //internal int Insert(Models.TareasAgendaPT oTareasAgendaPT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[4] {
        //            Param(enumDBFields.nivel, oTareasAgendaPT.nivel),
        //            Param(enumDBFields.t331_idpt, oTareasAgendaPT.t331_idpt),
        //            Param(enumDBFields.t331_despt, oTareasAgendaPT.t331_despt),
        //            Param(enumDBFields.t331_orden, oTareasAgendaPT.t331_orden)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_TareasAgendaPT_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un TareasAgendaPT a partir del id
        ///// </summary>
        //internal Models.TareasAgendaPT Select()
        //{
        //    Models.TareasAgendaPT oTareasAgendaPT = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_TareasAgendaPT_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oTareasAgendaPT = new Models.TareasAgendaPT();
        //            oTareasAgendaPT.nivel=Convert.ToInt32(dr["nivel"]);
        //            oTareasAgendaPT.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
        //            oTareasAgendaPT.t331_despt=Convert.ToString(dr["t331_despt"]);
        //            oTareasAgendaPT.t331_orden=Convert.ToInt32(dr["t331_orden"]);

        //        }
        //        return oTareasAgendaPT;
				
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
        ///// Actualiza un TareasAgendaPT a partir del id
        ///// </summary>
        //internal int Update(Models.TareasAgendaPT oTareasAgendaPT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[4] {
        //            Param(enumDBFields.nivel, oTareasAgendaPT.nivel),
        //            Param(enumDBFields.t331_idpt, oTareasAgendaPT.t331_idpt),
        //            Param(enumDBFields.t331_despt, oTareasAgendaPT.t331_despt),
        //            Param(enumDBFields.t331_orden, oTareasAgendaPT.t331_orden)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_TareasAgendaPT_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un TareasAgendaPT a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_TareasAgendaPT_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los TareasAgendaPT
        ///// </summary>
        //internal List<Models.TareasAgendaPT> Catalogo(Models.TareasAgendaPT oTareasAgendaPTFilter)
        //{
        //    Models.TareasAgendaPT oTareasAgendaPT = null;
        //    List<Models.TareasAgendaPT> lst = new List<Models.TareasAgendaPT>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[4] {
        //            Param(enumDBFields.nivel, oTEMP_TareasAgendaPTFilter.nivel),
        //            Param(enumDBFields.t331_idpt, oTEMP_TareasAgendaPTFilter.t331_idpt),
        //            Param(enumDBFields.t331_despt, oTEMP_TareasAgendaPTFilter.t331_despt),
        //            Param(enumDBFields.t331_orden, oTEMP_TareasAgendaPTFilter.t331_orden)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_TareasAgendaPT_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oTareasAgendaPT = new Models.TareasAgendaPT();
        //            oTareasAgendaPT.nivel=Convert.ToInt32(dr["nivel"]);
        //            oTareasAgendaPT.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
        //            oTareasAgendaPT.t331_despt=Convert.ToString(dr["t331_despt"]);
        //            oTareasAgendaPT.t331_orden=Convert.ToInt32(dr["t331_orden"]);

        //            lst.Add(oTareasAgendaPT);

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
				case enumDBFields.nivel:
					paramName = "@nivel";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t331_idpt:
					paramName = "@t331_idpt";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t331_despt:
					paramName = "@t331_despt";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t331_orden:
					paramName = "@t331_orden";
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
