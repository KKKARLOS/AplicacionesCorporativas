using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for UsuarioProyectoSubNodo
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class UsuarioProyectoSubNodo 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t305_idproyectosubnodo = 1,
			t314_idusuario = 2,
			t330_costecon = 3,
			t330_costerep = 4,
			t330_deriva = 5,
			t330_falta = 6,
			t330_fbaja = 7,
			t333_idperfilproy = 8
        }

        internal UsuarioProyectoSubNodo(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un UsuarioProyectoSubNodo
        ///// </summary>
        //internal int Insert(Models.UsuarioProyectoSubNodo oUsuarioProyectoSubNodo)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[8] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oUsuarioProyectoSubNodo.t305_idproyectosubnodo),
        //            Param(enumDBFields.t314_idusuario, oUsuarioProyectoSubNodo.t314_idusuario),
        //            Param(enumDBFields.t330_costecon, oUsuarioProyectoSubNodo.t330_costecon),
        //            Param(enumDBFields.t330_costerep, oUsuarioProyectoSubNodo.t330_costerep),
        //            Param(enumDBFields.t330_deriva, oUsuarioProyectoSubNodo.t330_deriva),
        //            Param(enumDBFields.t330_falta, oUsuarioProyectoSubNodo.t330_falta),
        //            Param(enumDBFields.t330_fbaja, oUsuarioProyectoSubNodo.t330_fbaja),
        //            Param(enumDBFields.t333_idperfilproy, oUsuarioProyectoSubNodo.t333_idperfilproy)
        //        };

        //        return (int)cDblib.Execute("_UsuarioProyectoSubNodo_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Obtiene un UsuarioProyectoSubNodo a partir del id
        /// </summary>
        internal Models.UsuarioProyectoSubNodo Select(int t305_idproyectosubnodo, int t314_idusuario)
        {
            Models.UsuarioProyectoSubNodo oUsuarioProyectoSubNodo = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t305_idproyectosubnodo, t305_idproyectosubnodo),
                    Param(enumDBFields.t314_idusuario, t314_idusuario)
                };

                dr = cDblib.DataReader("SUP_USUARIOPROYECTOSUBNODO_S", dbparams);
                if (dr.Read())
                {
                    oUsuarioProyectoSubNodo = new Models.UsuarioProyectoSubNodo();
                    oUsuarioProyectoSubNodo.t305_idproyectosubnodo = t305_idproyectosubnodo;
                    oUsuarioProyectoSubNodo.t314_idusuario = t314_idusuario;
                    oUsuarioProyectoSubNodo.t330_costecon = Convert.ToDecimal(dr["t330_costecon"]);
                    oUsuarioProyectoSubNodo.t330_costerep = Convert.ToDecimal(dr["t330_costerep"]);
                    oUsuarioProyectoSubNodo.t330_deriva = Convert.ToBoolean(dr["t330_deriva"]);
                    oUsuarioProyectoSubNodo.t330_falta = Convert.ToDateTime(dr["t330_falta"]);
                    if (!Convert.IsDBNull(dr["t330_fbaja"]))
                        oUsuarioProyectoSubNodo.t330_fbaja = Convert.ToDateTime(dr["t330_fbaja"]);
                    if (!Convert.IsDBNull(dr["t333_idperfilproy"]))
                        oUsuarioProyectoSubNodo.t333_idperfilproy = Convert.ToInt32(dr["t333_idperfilproy"]);

                }
                return oUsuarioProyectoSubNodo;

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
        ///// Actualiza un UsuarioProyectoSubNodo a partir del id
        ///// </summary>
        //internal int Update(Models.UsuarioProyectoSubNodo oUsuarioProyectoSubNodo)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[8] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oUsuarioProyectoSubNodo.t305_idproyectosubnodo),
        //            Param(enumDBFields.t314_idusuario, oUsuarioProyectoSubNodo.t314_idusuario),
        //            Param(enumDBFields.t330_costecon, oUsuarioProyectoSubNodo.t330_costecon),
        //            Param(enumDBFields.t330_costerep, oUsuarioProyectoSubNodo.t330_costerep),
        //            Param(enumDBFields.t330_deriva, oUsuarioProyectoSubNodo.t330_deriva),
        //            Param(enumDBFields.t330_falta, oUsuarioProyectoSubNodo.t330_falta),
        //            Param(enumDBFields.t330_fbaja, oUsuarioProyectoSubNodo.t330_fbaja),
        //            Param(enumDBFields.t333_idperfilproy, oUsuarioProyectoSubNodo.t333_idperfilproy)
        //        };

        //        return (int)cDblib.Execute("_UsuarioProyectoSubNodo_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Elimina un UsuarioProyectoSubNodo a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {


        //        return (int)cDblib.Execute("_UsuarioProyectoSubNodo_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Obtiene los proyectos y las fechas de alta y baja para un usuario
        /// </summary>
        internal List<Models.UsuarioProyectoSubNodo> CatalogoProyectosGasvi(int t314_idusuario)
        {
            Models.UsuarioProyectoSubNodo oUsuarioProyectoSubNodo = null;
            List<Models.UsuarioProyectoSubNodo> lst = new List<Models.UsuarioProyectoSubNodo>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t314_idusuario, t314_idusuario)
                };

                dr = cDblib.DataReader("GVT_GETFECHASUSUARIOPSN_CAT", dbparams);
                while (dr.Read())
                {
                    oUsuarioProyectoSubNodo = new Models.UsuarioProyectoSubNodo();
                    oUsuarioProyectoSubNodo.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oUsuarioProyectoSubNodo.t330_falta = Convert.ToDateTime(dr["t330_falta"]);
                    if (!Convert.IsDBNull(dr["t330_fbaja"]))
                        oUsuarioProyectoSubNodo.t330_fbaja = Convert.ToDateTime(dr["t330_fbaja"]);
                    lst.Add(oUsuarioProyectoSubNodo);

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
				case enumDBFields.t305_idproyectosubnodo:
					paramName = "@t305_idproyectosubnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t314_idusuario:
					paramName = "@t314_idusuario";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t330_costecon:
					paramName = "@t330_costecon";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.t330_costerep:
					paramName = "@t330_costerep";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.t330_deriva:
					paramName = "@t330_deriva";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t330_falta:
					paramName = "@t330_falta";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t330_fbaja:
					paramName = "@t330_fbaja";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t333_idperfilproy:
					paramName = "@t333_idperfilproy";
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
