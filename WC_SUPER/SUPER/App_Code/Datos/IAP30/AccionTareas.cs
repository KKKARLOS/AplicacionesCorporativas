using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AccionTareas
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{    
    internal class AccionTareas 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		

        private enum enumDBFields : byte
        {
            t383_idaccion = 1,
            t332_idtarea = 2
            //t332_destarea = 2,
            //t332_orden = 3,
            //t332_etpl = 4,
            //t332_fipl = 5,
            //t332_ffpl = 6,
            //t332_etpr = 7,
            //t332_ffpr = 8,
            //Consumo = 9,
            //t332_avanceauto = 10,
            //t332_avance = 11,
            //Estado = 12,
            //num_proyecto = 13,
            //nom_proyecto = 14,
            //t331_idpt = 15,
            //t331_despt = 16,
            //t334_idfase = 17,
            //t334_desfase = 18,
            //t335_idactividad = 19,
            //t335_desactividad = 20
        }

        internal AccionTareas(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un AccionTareas
        /// </summary>
        internal int Insert(Models.AccionTareas oAccionTareas)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t332_idtarea, oAccionTareas.t332_idtarea),
                    Param(enumDBFields.t383_idaccion, oAccionTareas.t383_idaccion) 
                };

                return (int)cDblib.Execute("SUP_ACCIONTAREAS_I_SNE", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
        ///// <summary>
        ///// Obtiene un AccionTareas a partir del id
        ///// </summary>
        //internal Models.AccionTareas Select()
        //{
        //    Models.AccionTareas oAccionTareas = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_AccionTareas_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oAccionTareas = new Models.AccionTareas();
        //            oAccionTareas.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oAccionTareas.t332_destarea=Convert.ToString(dr["t332_destarea"]);
        //            oAccionTareas.t332_orden=Convert.ToInt32(dr["t332_orden"]);
        //            oAccionTareas.t332_etpl=Convert.ToDouble(dr["t332_etpl"]);
        //            if(!Convert.IsDBNull(dr["t332_fipl"]))
        //                oAccionTareas.t332_fipl=Convert.ToString(dr["t332_fipl"]);
        //            if(!Convert.IsDBNull(dr["t332_ffpl"]))
        //                oAccionTareas.t332_ffpl=Convert.ToString(dr["t332_ffpl"]);
        //            oAccionTareas.t332_etpr=Convert.ToDouble(dr["t332_etpr"]);
        //            if(!Convert.IsDBNull(dr["t332_ffpr"]))
        //                oAccionTareas.t332_ffpr=Convert.ToString(dr["t332_ffpr"]);
        //            oAccionTareas.Consumo=Convert.ToDouble(dr["Consumo"]);
        //            oAccionTareas.t332_avanceauto=Convert.ToBoolean(dr["t332_avanceauto"]);
        //            if(!Convert.IsDBNull(dr["t332_avance"]))
        //                oAccionTareas.t332_avance=Convert.ToDouble(dr["t332_avance"]);
        //            if(!Convert.IsDBNull(dr["Estado"]))
        //                oAccionTareas.Estado=Convert.ToString(dr["Estado"]);
        //            oAccionTareas.num_proyecto=Convert.ToInt32(dr["num_proyecto"]);
        //            oAccionTareas.nom_proyecto=Convert.ToString(dr["nom_proyecto"]);
        //            oAccionTareas.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
        //            oAccionTareas.t331_despt=Convert.ToString(dr["t331_despt"]);
        //            oAccionTareas.t334_idfase=Convert.ToInt32(dr["t334_idfase"]);
        //            oAccionTareas.t334_desfase=Convert.ToString(dr["t334_desfase"]);
        //            oAccionTareas.t335_idactividad=Convert.ToInt32(dr["t335_idactividad"]);
        //            oAccionTareas.t335_desactividad=Convert.ToString(dr["t335_desactividad"]);

        //        }
        //        return oAccionTareas;
				
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
        ///// Actualiza un AccionTareas a partir del id
        ///// </summary>
        //internal int Update(Models.AccionTareas oAccionTareas)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[20] {
        //            Param(enumDBFields.t332_idtarea, oAccionTareas.t332_idtarea),
        //            Param(enumDBFields.t332_destarea, oAccionTareas.t332_destarea),
        //            Param(enumDBFields.t332_orden, oAccionTareas.t332_orden),
        //            Param(enumDBFields.t332_etpl, oAccionTareas.t332_etpl),
        //            Param(enumDBFields.t332_fipl, oAccionTareas.t332_fipl),
        //            Param(enumDBFields.t332_ffpl, oAccionTareas.t332_ffpl),
        //            Param(enumDBFields.t332_etpr, oAccionTareas.t332_etpr),
        //            Param(enumDBFields.t332_ffpr, oAccionTareas.t332_ffpr),
        //            Param(enumDBFields.Consumo, oAccionTareas.Consumo),
        //            Param(enumDBFields.t332_avanceauto, oAccionTareas.t332_avanceauto),
        //            Param(enumDBFields.t332_avance, oAccionTareas.t332_avance),
        //            Param(enumDBFields.Estado, oAccionTareas.Estado),
        //            Param(enumDBFields.num_proyecto, oAccionTareas.num_proyecto),
        //            Param(enumDBFields.nom_proyecto, oAccionTareas.nom_proyecto),
        //            Param(enumDBFields.t331_idpt, oAccionTareas.t331_idpt),
        //            Param(enumDBFields.t331_despt, oAccionTareas.t331_despt),
        //            Param(enumDBFields.t334_idfase, oAccionTareas.t334_idfase),
        //            Param(enumDBFields.t334_desfase, oAccionTareas.t334_desfase),
        //            Param(enumDBFields.t335_idactividad, oAccionTareas.t335_idactividad),
        //            Param(enumDBFields.t335_desactividad, oAccionTareas.t335_desactividad)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_AccionTareas_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Elimina un AccionTareas a partir del id de la acción y de la tarea
        /// </summary>
        internal int Delete(Models.AccionTareas oAccionTareas)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t332_idtarea, oAccionTareas.t332_idtarea),
                    Param(enumDBFields.t383_idaccion, oAccionTareas.t383_idaccion) 
                };

                return (int)cDblib.Execute("[SUP_ACCIONTAREAS_D]", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los AccionTareas
        /// </summary>
        internal List<Models.AccionTareas> Catalogo(Int32 t383_idaccion)
        {
            Models.AccionTareas oAccionTareas = null;
            List<Models.AccionTareas> lst = new List<Models.AccionTareas>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t383_idaccion, t383_idaccion)
                };

                dr = cDblib.DataReader("SUP_ACCIONTAREAS_SByT383_idaccion", dbparams);
                while (dr.Read())
                {
                    oAccionTareas = new Models.AccionTareas();
                    oAccionTareas.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
                    oAccionTareas.t332_destarea=Convert.ToString(dr["t332_destarea"]);
                    oAccionTareas.t332_orden=Convert.ToInt32(dr["t332_orden"]);
                    oAccionTareas.t332_etpl=Convert.ToDouble(dr["t332_etpl"]);

                    if(!Convert.IsDBNull(dr["t332_fipl"]))
                        oAccionTareas.t332_fipl = Convert.ToDateTime(dr["t332_fipl"]);

                    if(!Convert.IsDBNull(dr["t332_ffpl"]))
                        oAccionTareas.t332_ffpl = Convert.ToDateTime(dr["t332_ffpl"]);
                    oAccionTareas.t332_etpr=Convert.ToDouble(dr["t332_etpr"]);
                    if(!Convert.IsDBNull(dr["t332_ffpr"]))
                        oAccionTareas.t332_ffpr = Convert.ToDateTime(dr["t332_ffpr"]);
                    oAccionTareas.Consumo=Convert.ToDouble(dr["Consumo"]);
                    oAccionTareas.t332_avanceauto=Convert.ToBoolean(dr["t332_avanceauto"]);
                    if(!Convert.IsDBNull(dr["t332_avance"]))
                        oAccionTareas.t332_avance=Convert.ToDouble(dr["t332_avance"]);
                    if(!Convert.IsDBNull(dr["Estado"]))
                        oAccionTareas.Estado=Convert.ToString(dr["Estado"]);
                    oAccionTareas.num_proyecto=Convert.ToInt32(dr["num_proyecto"]);
                    oAccionTareas.nom_proyecto=Convert.ToString(dr["nom_proyecto"]);
                    oAccionTareas.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
                    oAccionTareas.t331_despt=Convert.ToString(dr["t331_despt"]);
                    oAccionTareas.t334_idfase=Convert.ToInt32(dr["t334_idfase"]);
                    oAccionTareas.t334_desfase=Convert.ToString(dr["t334_desfase"]);
                    oAccionTareas.t335_idactividad=Convert.ToInt32(dr["t335_idactividad"]);
                    oAccionTareas.t335_desactividad=Convert.ToString(dr["t335_desactividad"]);

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
                case enumDBFields.t383_idaccion:
                    paramName = "@T383_idaccion";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t332_idtarea:
                    paramName = "@t332_idtarea";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
        //        case enumDBFields.t332_destarea:
        //            paramName = "@t332_destarea";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 100;
        //            break;
        //        case enumDBFields.t332_orden:
        //            paramName = "@t332_orden";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.t332_etpl:
        //            paramName = "@t332_etpl";
        //            paramType = SqlDbType.Float;
        //            paramSize = 8;
        //            break;
        //        case enumDBFields.t332_fipl:
        //            paramName = "@t332_fipl";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 30;
        //            break;
        //        case enumDBFields.t332_ffpl:
        //            paramName = "@t332_ffpl";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 30;
        //            break;
        //        case enumDBFields.t332_etpr:
        //            paramName = "@t332_etpr";
        //            paramType = SqlDbType.Float;
        //            paramSize = 8;
        //            break;
        //        case enumDBFields.t332_ffpr:
        //            paramName = "@t332_ffpr";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 30;
        //            break;
        //        case enumDBFields.Consumo:
        //            paramName = "@Consumo";
        //            paramType = SqlDbType.Float;
        //            paramSize = 8;
        //            break;
        //        case enumDBFields.t332_avanceauto:
        //            paramName = "@t332_avanceauto";
        //            paramType = SqlDbType.Bit;
        //            paramSize = 1;
        //            break;
        //        case enumDBFields.t332_avance:
        //            paramName = "@t332_avance";
        //            paramType = SqlDbType.Float;
        //            paramSize = 8;
        //            break;
        //        case enumDBFields.Estado:
        //            paramName = "@Estado";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 10;
        //            break;
        //        case enumDBFields.num_proyecto:
        //            paramName = "@num_proyecto";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.nom_proyecto:
        //            paramName = "@nom_proyecto";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 70;
        //            break;
        //        case enumDBFields.t331_idpt:
        //            paramName = "@t331_idpt";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.t331_despt:
        //            paramName = "@t331_despt";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 50;
        //            break;
        //        case enumDBFields.t334_idfase:
        //            paramName = "@t334_idfase";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.t334_desfase:
        //            paramName = "@t334_desfase";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 50;
        //            break;
        //        case enumDBFields.t335_idactividad:
        //            paramName = "@t335_idactividad";
        //            paramType = SqlDbType.Int;
        //            paramSize = 4;
        //            break;
        //        case enumDBFields.t335_desactividad:
        //            paramName = "@t335_desactividad";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 50;
        //            break;
            }


            dbParam = cDblib.dbParameter(paramName, paramType, paramSize);
            dbParam.Direction = paramDirection;
            if (paramDirection == ParameterDirection.Input | paramDirection == ParameterDirection.InputOutput) dbParam.Value = value;

            return dbParam;

        }
		
        #endregion
    
    }
}
