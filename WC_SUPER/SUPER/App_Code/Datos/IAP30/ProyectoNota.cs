using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ProyectoNota
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ProyectoNota 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            t314_idusuario = 1
        }

        internal ProyectoNota(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un ProyectoNota
        ///// </summary>
        //internal int Insert(Models.ProyectoNota oProyectoNota)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[11] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oProyectoNota.t305_idproyectosubnodo),
        //            Param(enumDBFields.t301_idproyecto, oProyectoNota.t301_idproyecto),
        //            Param(enumDBFields.t301_denominacion, oProyectoNota.t301_denominacion),
        //            Param(enumDBFields.t305_seudonimo, oProyectoNota.t305_seudonimo),
        //            Param(enumDBFields.t305_cualidad, oProyectoNota.t305_cualidad),
        //            Param(enumDBFields.t301_estado, oProyectoNota.t301_estado),
        //            Param(enumDBFields.Responsable_Proyecto, oProyectoNota.Responsable_Proyecto),
        //            Param(enumDBFields.Aprobador, oProyectoNota.Aprobador),
        //            Param(enumDBFields.Sexo_Aprobador, oProyectoNota.Sexo_Aprobador),
        //            Param(enumDBFields.t303_denominacion, oProyectoNota.t303_denominacion),
        //            Param(enumDBFields.t302_denominacion, oProyectoNota.t302_denominacion)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_ProyectoNota_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un ProyectoNota a partir del id
        ///// </summary>
        //internal Models.ProyectoNota Select()
        //{
        //    Models.ProyectoNota oProyectoNota = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_ProyectoNota_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oProyectoNota = new Models.ProyectoNota();
        //            oProyectoNota.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oProyectoNota.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);
        //            oProyectoNota.t301_denominacion=Convert.ToString(dr["t301_denominacion"]);
        //            oProyectoNota.t305_seudonimo=Convert.ToString(dr["t305_seudonimo"]);
        //            oProyectoNota.t305_cualidad=Convert.ToString(dr["t305_cualidad"]);
        //            oProyectoNota.t301_estado=Convert.ToString(dr["t301_estado"]);
        //            if(!Convert.IsDBNull(dr["Responsable_Proyecto"]))
        //                oProyectoNota.Responsable_Proyecto=Convert.ToString(dr["Responsable_Proyecto"]);
        //            if(!Convert.IsDBNull(dr["Aprobador"]))
        //                oProyectoNota.Aprobador=Convert.ToString(dr["Aprobador"]);
        //            if(!Convert.IsDBNull(dr["Sexo_Aprobador"]))
        //                oProyectoNota.Sexo_Aprobador=Convert.ToString(dr["Sexo_Aprobador"]);
        //            oProyectoNota.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
        //            oProyectoNota.t302_denominacion=Convert.ToString(dr["t302_denominacion"]);

        //        }
        //        return oProyectoNota;
				
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
        ///// Actualiza un ProyectoNota a partir del id
        ///// </summary>
        //internal int Update(Models.ProyectoNota oProyectoNota)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[11] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oProyectoNota.t305_idproyectosubnodo),
        //            Param(enumDBFields.t301_idproyecto, oProyectoNota.t301_idproyecto),
        //            Param(enumDBFields.t301_denominacion, oProyectoNota.t301_denominacion),
        //            Param(enumDBFields.t305_seudonimo, oProyectoNota.t305_seudonimo),
        //            Param(enumDBFields.t305_cualidad, oProyectoNota.t305_cualidad),
        //            Param(enumDBFields.t301_estado, oProyectoNota.t301_estado),
        //            Param(enumDBFields.Responsable_Proyecto, oProyectoNota.Responsable_Proyecto),
        //            Param(enumDBFields.Aprobador, oProyectoNota.Aprobador),
        //            Param(enumDBFields.Sexo_Aprobador, oProyectoNota.Sexo_Aprobador),
        //            Param(enumDBFields.t303_denominacion, oProyectoNota.t303_denominacion),
        //            Param(enumDBFields.t302_denominacion, oProyectoNota.t302_denominacion)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_ProyectoNota_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un ProyectoNota a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_ProyectoNota_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los ProyectoNota
        ///// </summary>
        internal List<Models.ProyectoNota> Catalogo(int t314_idusuario)
        {
            Models.ProyectoNota oProyectoNota = null;
            List<Models.ProyectoNota> lst = new List<Models.ProyectoNota>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t314_idusuario, t314_idusuario)
                };

                dr = cDblib.DataReader("GVT_GETPROYECTOSCREARNOTA_CAT", dbparams);
                while (dr.Read())
                {
                    oProyectoNota = new Models.ProyectoNota();
                    oProyectoNota.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oProyectoNota.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oProyectoNota.t301_denominacion = Convert.ToString(dr["t301_denominacion"]);
                    oProyectoNota.t305_seudonimo = Convert.ToString(dr["t305_seudonimo"]);
                    oProyectoNota.t305_cualidad = Convert.ToString(dr["t305_cualidad"]);
                    oProyectoNota.t301_estado = Convert.ToString(dr["t301_estado"]);
                    if (!Convert.IsDBNull(dr["Responsable_Proyecto"]))
                        oProyectoNota.Responsable_Proyecto = Convert.ToString(dr["Responsable_Proyecto"]);
                    if (!Convert.IsDBNull(dr["Aprobador"]))
                        oProyectoNota.Aprobador = Convert.ToString(dr["Aprobador"]);
                    if (!Convert.IsDBNull(dr["Sexo_Aprobador"]))
                        oProyectoNota.Sexo_Aprobador = Convert.ToString(dr["Sexo_Aprobador"]);
                    oProyectoNota.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                    oProyectoNota.t302_denominacion = Convert.ToString(dr["t302_denominacion"]);

                    lst.Add(oProyectoNota);

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
                case enumDBFields.t314_idusuario:
                    paramName = "@t314_idusuario";
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
