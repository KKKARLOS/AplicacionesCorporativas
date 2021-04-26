using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TareasAgendaT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class TareasAgendaT 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			nivel = 1,
			tipo = 2,
			t334_idfase = 3,
			t335_idactividad = 4,
			t332_idtarea = 5,
			estado = 6,
			denominacion = 7,
			orden = 8
        }

        internal TareasAgendaT(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un TareasAgendaT
        ///// </summary>
        //internal int Insert(Models.TareasAgendaT oTareasAgendaT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[8] {
        //            Param(enumDBFields.nivel, oTareasAgendaT.nivel),
        //            Param(enumDBFields.tipo, oTareasAgendaT.tipo),
        //            Param(enumDBFields.t334_idfase, oTareasAgendaT.t334_idfase),
        //            Param(enumDBFields.t335_idactividad, oTareasAgendaT.t335_idactividad),
        //            Param(enumDBFields.t332_idtarea, oTareasAgendaT.t332_idtarea),
        //            Param(enumDBFields.estado, oTareasAgendaT.estado),
        //            Param(enumDBFields.denominacion, oTareasAgendaT.denominacion),
        //            Param(enumDBFields.orden, oTareasAgendaT.orden)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_TareasAgendaT_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un TareasAgendaT a partir del id
        ///// </summary>
        //internal Models.TareasAgendaT Select()
        //{
        //    Models.TareasAgendaT oTareasAgendaT = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_TareasAgendaT_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oTareasAgendaT = new Models.TareasAgendaT();
        //            if(!Convert.IsDBNull(dr["nivel"]))
        //                oTareasAgendaT.nivel=Convert.ToInt32(dr["nivel"]);
        //            oTareasAgendaT.tipo=Convert.ToString(dr["tipo"]);
        //            oTareasAgendaT.t334_idfase=Convert.ToInt32(dr["t334_idfase"]);
        //            oTareasAgendaT.t335_idactividad=Convert.ToInt32(dr["t335_idactividad"]);
        //            oTareasAgendaT.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oTareasAgendaT.estado=Convert.ToInt32(dr["estado"]);
        //            oTareasAgendaT.denominacion=Convert.ToString(dr["denominacion"]);
        //            if(!Convert.IsDBNull(dr["orden"]))
        //                oTareasAgendaT.orden=Convert.ToString(dr["orden"]);

        //        }
        //        return oTareasAgendaT;
				
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
        ///// Actualiza un TareasAgendaT a partir del id
        ///// </summary>
        //internal int Update(Models.TareasAgendaT oTareasAgendaT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[8] {
        //            Param(enumDBFields.nivel, oTareasAgendaT.nivel),
        //            Param(enumDBFields.tipo, oTareasAgendaT.tipo),
        //            Param(enumDBFields.t334_idfase, oTareasAgendaT.t334_idfase),
        //            Param(enumDBFields.t335_idactividad, oTareasAgendaT.t335_idactividad),
        //            Param(enumDBFields.t332_idtarea, oTareasAgendaT.t332_idtarea),
        //            Param(enumDBFields.estado, oTareasAgendaT.estado),
        //            Param(enumDBFields.denominacion, oTareasAgendaT.denominacion),
        //            Param(enumDBFields.orden, oTareasAgendaT.orden)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_TareasAgendaT_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un TareasAgendaT a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_TareasAgendaT_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los TareasAgendaT
        ///// </summary>
        //internal List<Models.TareasAgendaT> Catalogo(Models.TareasAgendaT oTareasAgendaTFilter)
        //{
        //    Models.TareasAgendaT oTareasAgendaT = null;
        //    List<Models.TareasAgendaT> lst = new List<Models.TareasAgendaT>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[8] {
        //            Param(enumDBFields.nivel, oTEMP_TareasAgendaTFilter.nivel),
        //            Param(enumDBFields.tipo, oTEMP_TareasAgendaTFilter.tipo),
        //            Param(enumDBFields.t334_idfase, oTEMP_TareasAgendaTFilter.t334_idfase),
        //            Param(enumDBFields.t335_idactividad, oTEMP_TareasAgendaTFilter.t335_idactividad),
        //            Param(enumDBFields.t332_idtarea, oTEMP_TareasAgendaTFilter.t332_idtarea),
        //            Param(enumDBFields.estado, oTEMP_TareasAgendaTFilter.estado),
        //            Param(enumDBFields.denominacion, oTEMP_TareasAgendaTFilter.denominacion),
        //            Param(enumDBFields.orden, oTEMP_TareasAgendaTFilter.orden)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_TareasAgendaT_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oTareasAgendaT = new Models.TareasAgendaT();
        //            if(!Convert.IsDBNull(dr["nivel"]))
        //                oTareasAgendaT.nivel=Convert.ToInt32(dr["nivel"]);
        //            oTareasAgendaT.tipo=Convert.ToString(dr["tipo"]);
        //            oTareasAgendaT.t334_idfase=Convert.ToInt32(dr["t334_idfase"]);
        //            oTareasAgendaT.t335_idactividad=Convert.ToInt32(dr["t335_idactividad"]);
        //            oTareasAgendaT.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oTareasAgendaT.estado=Convert.ToInt32(dr["estado"]);
        //            oTareasAgendaT.denominacion=Convert.ToString(dr["denominacion"]);
        //            if(!Convert.IsDBNull(dr["orden"]))
        //                oTareasAgendaT.orden=Convert.ToString(dr["orden"]);

        //            lst.Add(oTareasAgendaT);

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
				case enumDBFields.tipo:
					paramName = "@tipo";
					paramType = SqlDbType.VarChar;
					paramSize = 1;
					break;
				case enumDBFields.t334_idfase:
					paramName = "@t334_idfase";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t335_idactividad:
					paramName = "@t335_idactividad";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_idtarea:
					paramName = "@t332_idtarea";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.estado:
					paramName = "@estado";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.denominacion:
					paramName = "@denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 100;
					break;
				case enumDBFields.orden:
					paramName = "@orden";
					paramType = SqlDbType.VarChar;
					paramSize = 8000;
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
