using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ValidarTareaAgenda
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ValidarTareaAgenda 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t332_idtarea = 1,
			t332_destarea = 2,
            t001_idficepi = 3
        }

        internal ValidarTareaAgenda(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un ValidarTareaAgenda
        ///// </summary>
        //internal int Insert(Models.ValidarTareaAgenda oValidarTareaAgenda)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[2] {
        //            Param(enumDBFields.t332_idtarea, oValidarTareaAgenda.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oValidarTareaAgenda.t332_destarea)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_ValidarTareaAgenda_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Obtiene un ValidarTareaAgenda a partir del id
        /// </summary>
        internal Models.ValidarTareaAgenda Select(Int32 idFicepi, Int32 idTarea)
        {
            Models.ValidarTareaAgenda oValidarTareaAgenda = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t001_idficepi, idFicepi),
                    Param(enumDBFields.t332_idtarea, idTarea)
                };

                dr = cDblib.DataReader("SUP_VALIDARTAREAAGENDA", dbparams);
                if (dr.Read())
                {
                    oValidarTareaAgenda = new Models.ValidarTareaAgenda();
                    oValidarTareaAgenda.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    oValidarTareaAgenda.t332_destarea = Convert.ToString(dr["t332_destarea"]);

                }
                return oValidarTareaAgenda;

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
        ///// Actualiza un ValidarTareaAgenda a partir del id
        ///// </summary>
        //internal int Update(Models.ValidarTareaAgenda oValidarTareaAgenda)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[2] {
        //            Param(enumDBFields.t332_idtarea, oValidarTareaAgenda.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oValidarTareaAgenda.t332_destarea)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_ValidarTareaAgenda_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Elimina un ValidarTareaAgenda a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {


        //        return (int)cDblib.Execute("SUPER.IAP30_ValidarTareaAgenda_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los ValidarTareaAgenda
        ///// </summary>
        //internal List<Models.ValidarTareaAgenda> Catalogo(Models.ValidarTareaAgenda oValidarTareaAgendaFilter)
        //{
        //    Models.ValidarTareaAgenda oValidarTareaAgenda = null;
        //    List<Models.ValidarTareaAgenda> lst = new List<Models.ValidarTareaAgenda>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[2] {
        //            Param(enumDBFields.t332_idtarea, oValidarTareaAgendaFilter.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oValidarTareaAgendaFilter.t332_destarea)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_ValidarTareaAgenda_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oValidarTareaAgenda = new Models.ValidarTareaAgenda();
        //            oValidarTareaAgenda.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
        //            oValidarTareaAgenda.t332_destarea = Convert.ToString(dr["t332_destarea"]);

        //            lst.Add(oValidarTareaAgenda);

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
				case enumDBFields.t332_idtarea:
					paramName = "@t332_idtarea";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_destarea:
					paramName = "@t332_destarea";
					paramType = SqlDbType.VarChar;
					paramSize = 100;
					break;
                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
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
