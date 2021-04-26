using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AccionTareasPT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class AccionTareasPT 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t332_idtarea = 1,
			t410_idaccion = 2
        }

        internal AccionTareasPT(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
        #region funciones publicas
        /// <summary>
        /// Inserta un AccionTareasPT
        /// </summary>
        internal int Insert(Models.AccionTareasPT oAccionTareasPT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t332_idtarea, oAccionTareasPT.t332_idtarea),
                    Param(enumDBFields.t410_idaccion, oAccionTareasPT.t410_idaccion) 
                };

                return (int)cDblib.Execute("SUP_ACCIONTAREAS_PT_I_SNE", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
        ///// <summary>
        ///// Obtiene un AccionTareasPT a partir del id
        ///// </summary>
        //internal Models.AccionTareasPT Select()
        //{
        //    Models.AccionTareasPT oAccionTareasPT = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_AccionTareasPT_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oAccionTareasPT = new Models.AccionTareasPT();
        //            oAccionTareasPT.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oAccionTareasPT.t332_destarea=Convert.ToString(dr["t332_destarea"]);
        //            oAccionTareasPT.t332_orden=Convert.ToInt32(dr["t332_orden"]);
        //            oAccionTareasPT.t332_etpl=Convert.ToDouble(dr["t332_etpl"]);
        //            if(!Convert.IsDBNull(dr["t332_fipl"]))
        //                oAccionTareasPT.t332_fipl=Convert.ToString(dr["t332_fipl"]);
        //            if(!Convert.IsDBNull(dr["t332_ffpl"]))
        //                oAccionTareasPT.t332_ffpl=Convert.ToString(dr["t332_ffpl"]);
        //            oAccionTareasPT.t332_etpr=Convert.ToDouble(dr["t332_etpr"]);
        //            if(!Convert.IsDBNull(dr["t332_ffpr"]))
        //                oAccionTareasPT.t332_ffpr=Convert.ToString(dr["t332_ffpr"]);
        //            oAccionTareasPT.Consumo=Convert.ToDouble(dr["Consumo"]);
        //            oAccionTareasPT.t332_avanceauto=Convert.ToBoolean(dr["t332_avanceauto"]);
        //            if(!Convert.IsDBNull(dr["t332_avance"]))
        //                oAccionTareasPT.t332_avance=Convert.ToDouble(dr["t332_avance"]);
        //            if(!Convert.IsDBNull(dr["Estado"]))
        //                oAccionTareasPT.Estado=Convert.ToString(dr["Estado"]);
        //            oAccionTareasPT.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
        //            oAccionTareasPT.t331_despt=Convert.ToString(dr["t331_despt"]);
        //            oAccionTareasPT.t301_denominacion=Convert.ToString(dr["t301_denominacion"]);
        //            oAccionTareasPT.t334_idfase=Convert.ToInt32(dr["t334_idfase"]);
        //            oAccionTareasPT.t334_desfase=Convert.ToString(dr["t334_desfase"]);
        //            oAccionTareasPT.t335_idactividad=Convert.ToInt32(dr["t335_idactividad"]);
        //            oAccionTareasPT.t335_desactividad=Convert.ToString(dr["t335_desactividad"]);

        //        }
        //        return oAccionTareasPT;
				
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
        ///// Actualiza un AccionTareasPT a partir del id
        ///// </summary>
        //internal int Update(Models.AccionTareasPT oAccionTareasPT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[19] {
        //            Param(enumDBFields.t332_idtarea, oAccionTareasPT.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oAccionTareasPT.t332_destarea),
        //            Param(enumDBFields.t332_orden, oAccionTareasPT.t332_orden),
        //            Param(enumDBFields.t332_etpl, oAccionTareasPT.t332_etpl),
        //            Param(enumDBFields.t332_fipl, oAccionTareasPT.t332_fipl),
        //            Param(enumDBFields.t332_ffpl, oAccionTareasPT.t332_ffpl),
        //            Param(enumDBFields.t332_etpr, oAccionTareasPT.t332_etpr),
        //            Param(enumDBFields.t332_ffpr, oAccionTareasPT.t332_ffpr),
        //            Param(enumDBFields.Consumo, oAccionTareasPT.Consumo),
        //            Param(enumDBFields.t332_avanceauto, oAccionTareasPT.t332_avanceauto),
        //            Param(enumDBFields.t332_avance, oAccionTareasPT.t332_avance),
        //            Param(enumDBFields.Estado, oAccionTareasPT.Estado),
        //            Param(enumDBFields.t331_idpt, oAccionTareasPT.t331_idpt),
        //            Param(enumDBFields.t331_despt, oAccionTareasPT.t331_despt),
        //            Param(enumDBFields.t301_denominacion, oAccionTareasPT.t301_denominacion),
        //            Param(enumDBFields.t334_idfase, oAccionTareasPT.t334_idfase),
        //            Param(enumDBFields.t334_desfase, oAccionTareasPT.t334_desfase),
        //            Param(enumDBFields.t335_idactividad, oAccionTareasPT.t335_idactividad),
        //            Param(enumDBFields.t335_desactividad, oAccionTareasPT.t335_desactividad)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_AccionTareasPT_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Elimina un AccionTareasPT a partir del id
        /// </summary>
        internal int Delete(Models.AccionTareasPT oAccionTareas)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t332_idtarea, oAccionTareas.t332_idtarea),
                    Param(enumDBFields.t410_idaccion, oAccionTareas.t410_idaccion) 
                };

                return (int)cDblib.Execute("[SUP_ACCIONTAREAS_PT_D]", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// Obtiene todos los AccionTareasPT
        ///// </summary>
        //internal List<Models.AccionTareasPT> Catalogo(Models.AccionTareasPT oAccionTareasPTFilter)
        //{
        //    Models.AccionTareasPT oAccionTareasPT = null;
        //    List<Models.AccionTareasPT> lst = new List<Models.AccionTareasPT>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[19] {
        //            Param(enumDBFields.t332_idtarea, oTEMP_AccionTareasPTFilter.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oTEMP_AccionTareasPTFilter.t332_destarea),
        //            Param(enumDBFields.t332_orden, oTEMP_AccionTareasPTFilter.t332_orden),
        //            Param(enumDBFields.t332_etpl, oTEMP_AccionTareasPTFilter.t332_etpl),
        //            Param(enumDBFields.t332_fipl, oTEMP_AccionTareasPTFilter.t332_fipl),
        //            Param(enumDBFields.t332_ffpl, oTEMP_AccionTareasPTFilter.t332_ffpl),
        //            Param(enumDBFields.t332_etpr, oTEMP_AccionTareasPTFilter.t332_etpr),
        //            Param(enumDBFields.t332_ffpr, oTEMP_AccionTareasPTFilter.t332_ffpr),
        //            Param(enumDBFields.Consumo, oTEMP_AccionTareasPTFilter.Consumo),
        //            Param(enumDBFields.t332_avanceauto, oTEMP_AccionTareasPTFilter.t332_avanceauto),
        //            Param(enumDBFields.t332_avance, oTEMP_AccionTareasPTFilter.t332_avance),
        //            Param(enumDBFields.Estado, oTEMP_AccionTareasPTFilter.Estado),
        //            Param(enumDBFields.t331_idpt, oTEMP_AccionTareasPTFilter.t331_idpt),
        //            Param(enumDBFields.t331_despt, oTEMP_AccionTareasPTFilter.t331_despt),
        //            Param(enumDBFields.t301_denominacion, oTEMP_AccionTareasPTFilter.t301_denominacion),
        //            Param(enumDBFields.t334_idfase, oTEMP_AccionTareasPTFilter.t334_idfase),
        //            Param(enumDBFields.t334_desfase, oTEMP_AccionTareasPTFilter.t334_desfase),
        //            Param(enumDBFields.t335_idactividad, oTEMP_AccionTareasPTFilter.t335_idactividad),
        //            Param(enumDBFields.t335_desactividad, oTEMP_AccionTareasPTFilter.t335_desactividad)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_AccionTareasPT_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oAccionTareasPT = new Models.AccionTareasPT();
        //            oAccionTareasPT.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oAccionTareasPT.t332_destarea=Convert.ToString(dr["t332_destarea"]);
        //            oAccionTareasPT.t332_orden=Convert.ToInt32(dr["t332_orden"]);
        //            oAccionTareasPT.t332_etpl=Convert.ToDouble(dr["t332_etpl"]);
        //            if(!Convert.IsDBNull(dr["t332_fipl"]))
        //                oAccionTareasPT.t332_fipl=Convert.ToString(dr["t332_fipl"]);
        //            if(!Convert.IsDBNull(dr["t332_ffpl"]))
        //                oAccionTareasPT.t332_ffpl=Convert.ToString(dr["t332_ffpl"]);
        //            oAccionTareasPT.t332_etpr=Convert.ToDouble(dr["t332_etpr"]);
        //            if(!Convert.IsDBNull(dr["t332_ffpr"]))
        //                oAccionTareasPT.t332_ffpr=Convert.ToString(dr["t332_ffpr"]);
        //            oAccionTareasPT.Consumo=Convert.ToDouble(dr["Consumo"]);
        //            oAccionTareasPT.t332_avanceauto=Convert.ToBoolean(dr["t332_avanceauto"]);
        //            if(!Convert.IsDBNull(dr["t332_avance"]))
        //                oAccionTareasPT.t332_avance=Convert.ToDouble(dr["t332_avance"]);
        //            if(!Convert.IsDBNull(dr["Estado"]))
        //                oAccionTareasPT.Estado=Convert.ToString(dr["Estado"]);
        //            oAccionTareasPT.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
        //            oAccionTareasPT.t331_despt=Convert.ToString(dr["t331_despt"]);
        //            oAccionTareasPT.t301_denominacion=Convert.ToString(dr["t301_denominacion"]);
        //            oAccionTareasPT.t334_idfase=Convert.ToInt32(dr["t334_idfase"]);
        //            oAccionTareasPT.t334_desfase=Convert.ToString(dr["t334_desfase"]);
        //            oAccionTareasPT.t335_idactividad=Convert.ToInt32(dr["t335_idactividad"]);
        //            oAccionTareasPT.t335_desactividad=Convert.ToString(dr["t335_desactividad"]);

        //            lst.Add(oAccionTareasPT);

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
        /// Obtiene todos los AccionTareas
        /// </summary>
        internal List<Models.AccionTareasPT> Catalogo(Int32 t410_idaccion)
        {
            Models.AccionTareasPT oAccionTareas = null;
            List<Models.AccionTareasPT> lst = new List<Models.AccionTareasPT>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t410_idaccion, t410_idaccion)
                };

                dr = cDblib.DataReader("SUP_ACCIONTAREAS_PT_SByT410_idaccion", dbparams);
                while (dr.Read())
                {
                    oAccionTareas = new Models.AccionTareasPT();
                    oAccionTareas.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    oAccionTareas.t332_destarea = Convert.ToString(dr["t332_destarea"]);
                    oAccionTareas.t332_orden = Convert.ToInt32(dr["t332_orden"]);
                    oAccionTareas.t332_etpl = Convert.ToDouble(dr["t332_etpl"]);
                    if (!Convert.IsDBNull(dr["t332_fipl"]))
                        oAccionTareas.t332_fipl = Convert.ToDateTime(dr["t332_fipl"]);
                    if (!Convert.IsDBNull(dr["t332_ffpl"]))
                        oAccionTareas.t332_ffpl = Convert.ToDateTime(dr["t332_ffpl"]);
                    oAccionTareas.t332_etpr = Convert.ToDouble(dr["t332_etpr"]);
                    if (!Convert.IsDBNull(dr["t332_ffpr"]))
                        oAccionTareas.t332_ffpr = Convert.ToDateTime(dr["t332_ffpr"]);
                    oAccionTareas.Consumo = Convert.ToDouble(dr["Consumo"]);
                    oAccionTareas.t332_avanceauto = Convert.ToBoolean(dr["t332_avanceauto"]);
                    if (!Convert.IsDBNull(dr["t332_avance"]))
                        oAccionTareas.t332_avance = Convert.ToDouble(dr["t332_avance"]);
                    if (!Convert.IsDBNull(dr["Estado"]))
                        oAccionTareas.Estado = Convert.ToString(dr["Estado"]);
                    //oAccionTareas.num_proyecto = Convert.ToInt32(dr["num_proyecto"]);
                    oAccionTareas.t301_denominacion = Convert.ToString(dr["t301_denominacion"]);
                    oAccionTareas.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    oAccionTareas.t331_despt = Convert.ToString(dr["t331_despt"]);
                    oAccionTareas.t334_idfase = Convert.ToInt32(dr["t334_idfase"]);
                    oAccionTareas.t334_desfase = Convert.ToString(dr["t334_desfase"]);
                    oAccionTareas.t335_idactividad = Convert.ToInt32(dr["t335_idactividad"]);
                    oAccionTareas.t335_desactividad = Convert.ToString(dr["t335_desactividad"]);

                    lst.Add(oAccionTareas);

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
                case enumDBFields.t332_idtarea:
                    paramName = "@t332_idtarea";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t410_idaccion:
                    paramName = "@t410_idaccion";
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
