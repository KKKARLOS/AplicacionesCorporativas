using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ProyectoSubNodo
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ProyectoSubNodo 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t301_estado = 1,
			t301_denominacion = 2,
			t301_idproyecto = 3,
			t301_categoria = 4,
			t305_accesobitacora_pst = 5,
			t305_cualidad = 6,
			t314_idusuario_SAT = 7,
			t314_idusuario_SAA = 8,
			t301_externalizable = 9,
			t422_idmoneda = 10,
			denMoneda = 11,
            t305_idproyectosubnodo=12
        }

        internal ProyectoSubNodo(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un ProyectoSubNodo
        ///// </summary>
        //internal int Insert(Models.ProyectoSubNodo oProyectoSubNodo)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[11] {
        //            Param(enumDBFields.t301_estado, oProyectoSubNodo.t301_estado),
        //            Param(enumDBFields.t301_denominacion, oProyectoSubNodo.t301_denominacion),
        //            Param(enumDBFields.t301_idproyecto, oProyectoSubNodo.t301_idproyecto),
        //            Param(enumDBFields.t301_categoria, oProyectoSubNodo.t301_categoria),
        //            Param(enumDBFields.t305_accesobitacora_pst, oProyectoSubNodo.t305_accesobitacora_pst),
        //            Param(enumDBFields.t305_cualidad, oProyectoSubNodo.t305_cualidad),
        //            Param(enumDBFields.t314_idusuario_SAT, oProyectoSubNodo.t314_idusuario_SAT),
        //            Param(enumDBFields.t314_idusuario_SAA, oProyectoSubNodo.t314_idusuario_SAA),
        //            Param(enumDBFields.t301_externalizable, oProyectoSubNodo.t301_externalizable),
        //            Param(enumDBFields.t422_idmoneda, oProyectoSubNodo.t422_idmoneda),
        //            Param(enumDBFields.denMoneda, oProyectoSubNodo.denMoneda)
        //        };

        //        return (int)cDblib.Execute("_ProyectoSubNodo_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un ProyectoSubNodo a partir del id
        ///// </summary>
        internal Models.ProyectoSubNodo Select(int idPSN)
        {
            Models.ProyectoSubNodo oProyectoSubNodo = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                            Param(enumDBFields.t305_idproyectosubnodo, idPSN),
                        };
                dr = cDblib.DataReader("SUP_PROY_GET", dbparams);
                if (dr.Read())
                {
                    oProyectoSubNodo = new Models.ProyectoSubNodo();
                    oProyectoSubNodo.t301_estado = Convert.ToString(dr["t301_estado"]);
                    oProyectoSubNodo.t301_denominacion = Convert.ToString(dr["t301_denominacion"]);
                    oProyectoSubNodo.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oProyectoSubNodo.t301_categoria = Convert.ToString(dr["t301_categoria"]);
                    oProyectoSubNodo.t305_accesobitacora_pst = Convert.ToString(dr["t305_accesobitacora_pst"]);
                    oProyectoSubNodo.t305_accesobitacora_iap = Convert.ToString(dr["t305_accesobitacora_iap"]);
                    oProyectoSubNodo.t305_cualidad = Convert.ToString(dr["t305_cualidad"]);
                    oProyectoSubNodo.t314_idusuario_SAT = Convert.ToInt32(dr["t314_idusuario_SAT"]);
                    oProyectoSubNodo.t314_idusuario_SAA = Convert.ToInt32(dr["t314_idusuario_SAA"]);
                    oProyectoSubNodo.t301_externalizable = Convert.ToBoolean(dr["t301_externalizable"]);
                    oProyectoSubNodo.t422_idmoneda = Convert.ToString(dr["t422_idmoneda"]);
                    oProyectoSubNodo.denMoneda = Convert.ToString(dr["denMoneda"]);

                }
                return oProyectoSubNodo;

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
        ///// Actualiza un ProyectoSubNodo a partir del id
        ///// </summary>
        //internal int Update(Models.ProyectoSubNodo oProyectoSubNodo)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[11] {
        //            Param(enumDBFields.t301_estado, oProyectoSubNodo.t301_estado),
        //            Param(enumDBFields.t301_denominacion, oProyectoSubNodo.t301_denominacion),
        //            Param(enumDBFields.t301_idproyecto, oProyectoSubNodo.t301_idproyecto),
        //            Param(enumDBFields.t301_categoria, oProyectoSubNodo.t301_categoria),
        //            Param(enumDBFields.t305_accesobitacora_pst, oProyectoSubNodo.t305_accesobitacora_pst),
        //            Param(enumDBFields.t305_cualidad, oProyectoSubNodo.t305_cualidad),
        //            Param(enumDBFields.t314_idusuario_SAT, oProyectoSubNodo.t314_idusuario_SAT),
        //            Param(enumDBFields.t314_idusuario_SAA, oProyectoSubNodo.t314_idusuario_SAA),
        //            Param(enumDBFields.t301_externalizable, oProyectoSubNodo.t301_externalizable),
        //            Param(enumDBFields.t422_idmoneda, oProyectoSubNodo.t422_idmoneda),
        //            Param(enumDBFields.denMoneda, oProyectoSubNodo.denMoneda)
        //        };
                           
        //        return (int)cDblib.Execute("_ProyectoSubNodo_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un ProyectoSubNodo a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_ProyectoSubNodo_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los ProyectoSubNodo
        ///// </summary>
        //internal List<Models.ProyectoSubNodo> Catalogo(Models.ProyectoSubNodo oProyectoSubNodoFilter)
        //{
        //    Models.ProyectoSubNodo oProyectoSubNodo = null;
        //    List<Models.ProyectoSubNodo> lst = new List<Models.ProyectoSubNodo>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[11] {
        //            Param(enumDBFields.t301_estado, oTEMP_ProyectoSubNodoFilter.t301_estado),
        //            Param(enumDBFields.t301_denominacion, oTEMP_ProyectoSubNodoFilter.t301_denominacion),
        //            Param(enumDBFields.t301_idproyecto, oTEMP_ProyectoSubNodoFilter.t301_idproyecto),
        //            Param(enumDBFields.t301_categoria, oTEMP_ProyectoSubNodoFilter.t301_categoria),
        //            Param(enumDBFields.t305_accesobitacora_pst, oTEMP_ProyectoSubNodoFilter.t305_accesobitacora_pst),
        //            Param(enumDBFields.t305_cualidad, oTEMP_ProyectoSubNodoFilter.t305_cualidad),
        //            Param(enumDBFields.t314_idusuario_SAT, oTEMP_ProyectoSubNodoFilter.t314_idusuario_SAT),
        //            Param(enumDBFields.t314_idusuario_SAA, oTEMP_ProyectoSubNodoFilter.t314_idusuario_SAA),
        //            Param(enumDBFields.t301_externalizable, oTEMP_ProyectoSubNodoFilter.t301_externalizable),
        //            Param(enumDBFields.t422_idmoneda, oTEMP_ProyectoSubNodoFilter.t422_idmoneda),
        //            Param(enumDBFields.denMoneda, oTEMP_ProyectoSubNodoFilter.denMoneda)
        //        };

        //        dr = cDblib.DataReader("_ProyectoSubNodo_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oProyectoSubNodo = new Models.ProyectoSubNodo();
        //            oProyectoSubNodo.t301_estado=Convert.ToString(dr["t301_estado"]);
        //            oProyectoSubNodo.t301_denominacion=Convert.ToString(dr["t301_denominacion"]);
        //            oProyectoSubNodo.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);
        //            oProyectoSubNodo.t301_categoria=Convert.ToString(dr["t301_categoria"]);
        //            oProyectoSubNodo.t305_accesobitacora_pst=Convert.ToString(dr["t305_accesobitacora_pst"]);
        //            oProyectoSubNodo.t305_cualidad=Convert.ToString(dr["t305_cualidad"]);
        //            oProyectoSubNodo.t314_idusuario_SAT=Convert.ToInt32(dr["t314_idusuario_SAT"]);
        //            oProyectoSubNodo.t314_idusuario_SAA=Convert.ToInt32(dr["t314_idusuario_SAA"]);
        //            oProyectoSubNodo.t301_externalizable=Convert.ToBoolean(dr["t301_externalizable"]);
        //            oProyectoSubNodo.t422_idmoneda=Convert.ToString(dr["t422_idmoneda"]);
        //            oProyectoSubNodo.denMoneda=Convert.ToString(dr["denMoneda"]);

        //            lst.Add(oProyectoSubNodo);

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
                case enumDBFields.t305_idproyectosubnodo:
                    paramName = "@t305_idproyectosubnodo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t301_estado:
					paramName = "@t301_estado";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.t301_denominacion:
					paramName = "@t301_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 70;
					break;
				case enumDBFields.t301_idproyecto:
					paramName = "@t301_idproyecto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t301_categoria:
					paramName = "@t301_categoria";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.t305_accesobitacora_pst:
					paramName = "@t305_accesobitacora_pst";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.t305_cualidad:
					paramName = "@t305_cualidad";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.t314_idusuario_SAT:
					paramName = "@t314_idusuario_SAT";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t314_idusuario_SAA:
					paramName = "@t314_idusuario_SAA";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t301_externalizable:
					paramName = "@t301_externalizable";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t422_idmoneda:
					paramName = "@t422_idmoneda";
					paramType = SqlDbType.VarChar;
					paramSize = 5;
					break;
				case enumDBFields.denMoneda:
					paramName = "@denMoneda";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
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
