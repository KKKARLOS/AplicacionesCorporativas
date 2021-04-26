using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TareaBitacora
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class TareaBitacora 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
        private enum enumDBFields : byte
        {
			cod_tarea = 1,
			nom_tarea = 2,
			cod_pt = 3,
			nom_pt = 4,
			cod_pe = 5,
			nom_pe = 6,
			t301_estado = 7,
			t305_idproyectosubnodo = 8,
			cod_une = 9,
			t332_orden = 10,
			t332_acceso_iap = 11
        }

        internal TareaBitacora(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un TareaBitacora
        ///// </summary>
        //internal int Insert(Models.TareaBitacora oTareaBitacora)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[11] {
        //            Param(enumDBFields.cod_tarea, oTareaBitacora.cod_tarea),
        //            Param(enumDBFields.nom_tarea, oTareaBitacora.nom_tarea),
        //            Param(enumDBFields.cod_pt, oTareaBitacora.cod_pt),
        //            Param(enumDBFields.nom_pt, oTareaBitacora.nom_pt),
        //            Param(enumDBFields.cod_pe, oTareaBitacora.cod_pe),
        //            Param(enumDBFields.nom_pe, oTareaBitacora.nom_pe),
        //            Param(enumDBFields.t301_estado, oTareaBitacora.t301_estado),
        //            Param(enumDBFields.t305_idproyectosubnodo, oTareaBitacora.t305_idproyectosubnodo),
        //            Param(enumDBFields.cod_une, oTareaBitacora.cod_une),
        //            Param(enumDBFields.t332_orden, oTareaBitacora.t332_orden),
        //            Param(enumDBFields.t332_acceso_iap, oTareaBitacora.t332_acceso_iap)
        //        };

        //        return (int)cDblib.Execute("_TareaBitacora_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Obtiene un TareaBitacora a partir del id
        /// </summary>
        internal Models.TareaBitacora Select(int idTarea)
        {
            Models.TareaBitacora oTareaBitacora = null;
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                            Param(enumDBFields.cod_tarea, idTarea),
                        };
                dr = cDblib.DataReader("SUP_TAREA_P0_GRAL_S", dbparams);
                if (dr.Read())
                {
                    oTareaBitacora = new Models.TareaBitacora();
                    oTareaBitacora.cod_tarea = Convert.ToInt32(dr["t332_idtarea"]);
                    oTareaBitacora.nom_tarea = Convert.ToString(dr["t332_destarea"]);
                    oTareaBitacora.cod_pt = Convert.ToInt32(dr["t331_idpt"]);
                    oTareaBitacora.nom_pt = Convert.ToString(dr["t331_despt"]);
                    oTareaBitacora.cod_pe = Convert.ToInt32(dr["num_proyecto"]);
                    oTareaBitacora.nom_pe = Convert.ToString(dr["nom_proyecto"]);
                    oTareaBitacora.t301_estado = Convert.ToString(dr["t301_estado"]);
                    oTareaBitacora.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oTareaBitacora.cod_une = Convert.ToInt32(dr["t303_idnodo"]);
                    oTareaBitacora.t332_orden = Convert.ToInt32(dr["t332_orden"]);
                    oTareaBitacora.t332_acceso_iap = Convert.ToString(dr["t332_acceso_iap"]);
                    //oTareaBitacora.t331_acceso_iap = Convert.ToString(dr["t331_acceso_iap"]);
                    //oTareaBitacora.t305_accesobitacora_pst = Convert.ToString(dr["t305_accesobitacora_pst"]);
                    oTareaBitacora.nom_fase = Convert.ToString(dr["t334_desfase"]);
                    oTareaBitacora.nom_actividad = Convert.ToString(dr["t335_desactividad"]);
                }
                return oTareaBitacora;
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
        ///// Actualiza un TareaBitacora a partir del id
        ///// </summary>
        //internal int Update(Models.TareaBitacora oTareaBitacora)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[11] {
        //            Param(enumDBFields.cod_tarea, oTareaBitacora.cod_tarea),
        //            Param(enumDBFields.nom_tarea, oTareaBitacora.nom_tarea),
        //            Param(enumDBFields.cod_pt, oTareaBitacora.cod_pt),
        //            Param(enumDBFields.nom_pt, oTareaBitacora.nom_pt),
        //            Param(enumDBFields.cod_pe, oTareaBitacora.cod_pe),
        //            Param(enumDBFields.nom_pe, oTareaBitacora.nom_pe),
        //            Param(enumDBFields.t301_estado, oTareaBitacora.t301_estado),
        //            Param(enumDBFields.t305_idproyectosubnodo, oTareaBitacora.t305_idproyectosubnodo),
        //            Param(enumDBFields.cod_une, oTareaBitacora.cod_une),
        //            Param(enumDBFields.t332_orden, oTareaBitacora.t332_orden),
        //            Param(enumDBFields.t332_acceso_iap, oTareaBitacora.t332_acceso_iap)
        //        };
                           
        //        return (int)cDblib.Execute("_TareaBitacora_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un TareaBitacora a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_TareaBitacora_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los TareaBitacora
        ///// </summary>
        //internal List<Models.TareaBitacora> Catalogo(Models.TareaBitacora oTareaBitacoraFilter)
        //{
        //    Models.TareaBitacora oTareaBitacora = null;
        //    List<Models.TareaBitacora> lst = new List<Models.TareaBitacora>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[11] {
        //            Param(enumDBFields.cod_tarea, oTEMP_TareaBitacoraFilter.cod_tarea),
        //            Param(enumDBFields.nom_tarea, oTEMP_TareaBitacoraFilter.nom_tarea),
        //            Param(enumDBFields.cod_pt, oTEMP_TareaBitacoraFilter.cod_pt),
        //            Param(enumDBFields.nom_pt, oTEMP_TareaBitacoraFilter.nom_pt),
        //            Param(enumDBFields.cod_pe, oTEMP_TareaBitacoraFilter.cod_pe),
        //            Param(enumDBFields.nom_pe, oTEMP_TareaBitacoraFilter.nom_pe),
        //            Param(enumDBFields.t301_estado, oTEMP_TareaBitacoraFilter.t301_estado),
        //            Param(enumDBFields.t305_idproyectosubnodo, oTEMP_TareaBitacoraFilter.t305_idproyectosubnodo),
        //            Param(enumDBFields.cod_une, oTEMP_TareaBitacoraFilter.cod_une),
        //            Param(enumDBFields.t332_orden, oTEMP_TareaBitacoraFilter.t332_orden),
        //            Param(enumDBFields.t332_acceso_iap, oTEMP_TareaBitacoraFilter.t332_acceso_iap)
        //        };

        //        dr = cDblib.DataReader("_TareaBitacora_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oTareaBitacora = new Models.TareaBitacora();
        //            oTareaBitacora.cod_tarea=Convert.ToInt32(dr["cod_tarea"]);
        //            oTareaBitacora.nom_tarea=Convert.ToString(dr["nom_tarea"]);
        //            oTareaBitacora.cod_pt=Convert.ToInt32(dr["cod_pt"]);
        //            oTareaBitacora.nom_pt=Convert.ToString(dr["nom_pt"]);
        //            oTareaBitacora.cod_pe=Convert.ToInt32(dr["cod_pe"]);
        //            oTareaBitacora.nom_pe=Convert.ToString(dr["nom_pe"]);
        //            oTareaBitacora.t301_estado=Convert.ToString(dr["t301_estado"]);
        //            oTareaBitacora.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oTareaBitacora.cod_une=Convert.ToInt32(dr["cod_une"]);
        //            oTareaBitacora.t332_orden=Convert.ToInt32(dr["t332_orden"]);
        //            oTareaBitacora.t332_acceso_iap=Convert.ToString(dr["t332_acceso_iap"]);

        //            lst.Add(oTareaBitacora);

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
        /// <summary>
        /// Obtiene todos las tareas de un PT que tienen bitácora
        /// </summary>
        internal List<Models.TareaBitacora> Catalogo(int idPT)
        {
            Models.TareaBitacora oTarea = null;
            List<Models.TareaBitacora> lst = new List<Models.TareaBitacora>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.cod_pt, idPT),
                };
                dr = cDblib.DataReader("SUP_T_BIT_C", dbparams);
                while (dr.Read())
                {
                    oTarea = new Models.TareaBitacora();
                    //oTarea.cod_pt = Convert.ToInt32(dr["cod_pt"]);
                    //oTarea.nom_pt = Convert.ToString(dr["nom_pt"]);
                    //oTarea.cod_pe = Convert.ToInt32(dr["cod_pe"]);
                    //oTarea.nom_pe = Convert.ToString(dr["nom_pe"]);
                    oTarea.cod_tarea = Convert.ToInt32(dr["cod_tarea"]);
                    oTarea.nom_tarea = Convert.ToString(dr["nom_tarea"]);
                    oTarea.t332_acceso_iap = Convert.ToString(dr["t332_acceso_iap"]);

                    lst.Add(oTarea);
                }
                return lst;
            }
            catch (Exception ex) { throw ex; }
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
				case enumDBFields.cod_tarea:
                    paramName = "@nIdTarea";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.nom_tarea:
					paramName = "@nom_tarea";
					paramType = SqlDbType.VarChar;
					paramSize = 100;
					break;
				case enumDBFields.cod_pt:
                    paramName = "@t331_idpt";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.nom_pt:
					paramName = "@nom_pt";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.cod_pe:
					paramName = "@cod_pe";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.nom_pe:
					paramName = "@nom_pe";
					paramType = SqlDbType.VarChar;
					paramSize = 70;
					break;
				case enumDBFields.t301_estado:
					paramName = "@t301_estado";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.t305_idproyectosubnodo:
					paramName = "@t305_idproyectosubnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.cod_une:
					paramName = "@cod_une";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_orden:
					paramName = "@t332_orden";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_acceso_iap:
					paramName = "@t332_acceso_iap";
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
