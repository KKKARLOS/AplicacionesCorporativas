using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TareaIAPMasiva
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class TareaIAPMasiva 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			/*t305_idproyectosubnodo = 1,
			t331_idpt = 2,
			denominacion = 3,
			t323_regfes = 4,
			t323_regjornocompleta = 5,
			t331_obligaest = 6,
			t334_idfase = 7,
			t335_idactividad = 8,
			t332_idtarea = 9,
			t332_estado = 10,
			t332_impiap = 11,
			t301_estado = 12,*/
            nUsuario = 1,
            dUMCIAP = 2,
            dDesde = 3,
            dHasta = 4,
            idTarea = 5,
            bMostrarPSN = 6,
            bMostrarPT = 7,
            bMostrarF = 8,
            bMostrarA = 9,
            bMostrarT = 10
        }

        internal TareaIAPMasiva(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        //#region funciones publicas

        /// <summary>
        /// Obtiene un TareaIAPMasiva a partir del id
        /// </summary>
        /*public Models.TareaIAPMasiva Select(Int32 nUsuario, Int32 idTarea, DateTime dUMC)
        {
            Models.TareaIAPMasiva oTareaIAPMasiva = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.nUsuario, nUsuario),
                    Param(enumDBFields.t332_idtarea, idTarea),
                    Param(enumDBFields.dUMC, dUMC)
                };

                dr = cDblib.DataReader("SUP_IAP_TAREA_C", dbparams);
                if (dr.Read())
                {
                    oTareaIAPMasiva = new Models.TareaIAPMasiva();
                    oTareaIAPMasiva.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oTareaIAPMasiva.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    oTareaIAPMasiva.denominacion = Convert.ToString(dr["denominacion"]);
                    oTareaIAPMasiva.t323_regfes = Convert.ToInt32(dr["t323_regfes"]);
                    oTareaIAPMasiva.t323_regjornocompleta = Convert.ToInt32(dr["t323_regjornocompleta"]);
                    oTareaIAPMasiva.t331_obligaest = Convert.ToInt32(dr["t331_obligaest"]);
                    oTareaIAPMasiva.t334_idfase = Convert.ToInt32(dr["t334_idfase"]);
                    oTareaIAPMasiva.t335_idactividad = Convert.ToInt32(dr["t335_idactividad"]);
                    oTareaIAPMasiva.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    oTareaIAPMasiva.t332_estado = Convert.ToByte(dr["t332_estado"]);
                    oTareaIAPMasiva.t332_impiap = Convert.ToBoolean(dr["t332_impiap"]);
                    oTareaIAPMasiva.t301_estado = Convert.ToString(dr["t301_estado"]);

                }
                return oTareaIAPMasiva;

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
        }*/

        public Models.TareaIAPMasiva Select(Int32 nUsuario, Int32 idTarea, DateTime dUMC, Nullable<DateTime> fechaInicio, Nullable<DateTime> fechaFin)
        {
            Models.TareaIAPMasiva oTareaIAPMasiva = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[10] {

                    Param(enumDBFields.nUsuario, nUsuario),
                    Param(enumDBFields.dDesde, fechaInicio),
                    Param(enumDBFields.dHasta, fechaFin),
                    Param(enumDBFields.dUMCIAP, dUMC),
                    Param(enumDBFields.idTarea, idTarea),
                    Param(enumDBFields.bMostrarPSN, 0),
                    Param(enumDBFields.bMostrarPT, 0),
                    Param(enumDBFields.bMostrarF, 0),
                    Param(enumDBFields.bMostrarA, 0),
                    Param(enumDBFields.bMostrarT, 1)
                };

                dr = cDblib.DataReader("SUP_ESTRUCTURAIMPUTABLEENTREFECHAS_IAP30", dbparams);
                if (dr.Read())
                {
                    oTareaIAPMasiva = new Models.TareaIAPMasiva();
                    oTareaIAPMasiva.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    if (!Convert.IsDBNull(dr["t331_idpt"]))
                        oTareaIAPMasiva.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);                    
                    oTareaIAPMasiva.denominacion = Convert.ToString(dr["denominacion"]);
                    if (!Convert.IsDBNull(dr["t323_regfes"]))
                        oTareaIAPMasiva.t323_regfes = Convert.ToInt32(dr["t323_regfes"]);
                    if (!Convert.IsDBNull(dr["t323_regjornocompleta"]))
                        oTareaIAPMasiva.t323_regjornocompleta = Convert.ToInt32(dr["t323_regjornocompleta"]);
                    if (!Convert.IsDBNull(dr["t331_obligaest"]))
                        oTareaIAPMasiva.t331_obligaest = Convert.ToInt32(dr["t331_obligaest"]);
                    if (!Convert.IsDBNull(dr["t334_idfase"]))
                        oTareaIAPMasiva.t334_idfase = Convert.ToInt32(dr["t334_idfase"]);
                    if (!Convert.IsDBNull(dr["t335_idactividad"]))
                        oTareaIAPMasiva.t335_idactividad = Convert.ToInt32(dr["t335_idactividad"]);
                    if (!Convert.IsDBNull(dr["t332_idtarea"]))
                        oTareaIAPMasiva.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    if (!Convert.IsDBNull(dr["fechainicioimputacionpermitida"]))
                        oTareaIAPMasiva.fechaInicioImpPermitida = Convert.ToDateTime(dr["fechainicioimputacionpermitida"]);
                    if (!Convert.IsDBNull(dr["fechafinimputacionpermitida"]))
                        oTareaIAPMasiva.fechaFinImpPermitida = Convert.ToDateTime(dr["fechafinimputacionpermitida"]);
                    /*oTareaIAPMasiva.t332_estado = Convert.ToByte(dr["t332_estado"]);
                    oTareaIAPMasiva.t332_impiap = Convert.ToBoolean(dr["t332_impiap"]);
                    oTareaIAPMasiva.t301_estado = Convert.ToString(dr["t301_estado"]);*/

                }
                return oTareaIAPMasiva;

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
        ///// Inserta un TareaIAPMasiva
        ///// </summary>
        //internal int Insert(Models.TareaIAPMasiva oTareaIAPMasiva)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[12] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oTareaIAPMasiva.t305_idproyectosubnodo),
        //            Param(enumDBFields.t331_idpt, oTareaIAPMasiva.t331_idpt),
        //            Param(enumDBFields.denominacion, oTareaIAPMasiva.denominacion),
        //            Param(enumDBFields.t323_regfes, oTareaIAPMasiva.t323_regfes),
        //            Param(enumDBFields.t323_regjornocompleta, oTareaIAPMasiva.t323_regjornocompleta),
        //            Param(enumDBFields.t331_obligaest, oTareaIAPMasiva.t331_obligaest),
        //            Param(enumDBFields.t334_idfase, oTareaIAPMasiva.t334_idfase),
        //            Param(enumDBFields.t335_idactividad, oTareaIAPMasiva.t335_idactividad),
        //            Param(enumDBFields.t332_idtarea, oTareaIAPMasiva.t332_idtarea),
        //            Param(enumDBFields.t332_estado, oTareaIAPMasiva.t332_estado),
        //            Param(enumDBFields.t332_impiap, oTareaIAPMasiva.t332_impiap),
        //            Param(enumDBFields.t301_estado, oTareaIAPMasiva.t301_estado)
        //        };

        //        return (int)cDblib.Execute("_TareaIAPMasiva_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        ///// <summary>
        ///// Actualiza un TareaIAPMasiva a partir del id
        ///// </summary>
        //internal int Update(Models.TareaIAPMasiva oTareaIAPMasiva)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[12] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oTareaIAPMasiva.t305_idproyectosubnodo),
        //            Param(enumDBFields.t331_idpt, oTareaIAPMasiva.t331_idpt),
        //            Param(enumDBFields.denominacion, oTareaIAPMasiva.denominacion),
        //            Param(enumDBFields.t323_regfes, oTareaIAPMasiva.t323_regfes),
        //            Param(enumDBFields.t323_regjornocompleta, oTareaIAPMasiva.t323_regjornocompleta),
        //            Param(enumDBFields.t331_obligaest, oTareaIAPMasiva.t331_obligaest),
        //            Param(enumDBFields.t334_idfase, oTareaIAPMasiva.t334_idfase),
        //            Param(enumDBFields.t335_idactividad, oTareaIAPMasiva.t335_idactividad),
        //            Param(enumDBFields.t332_idtarea, oTareaIAPMasiva.t332_idtarea),
        //            Param(enumDBFields.t332_estado, oTareaIAPMasiva.t332_estado),
        //            Param(enumDBFields.t332_impiap, oTareaIAPMasiva.t332_impiap),
        //            Param(enumDBFields.t301_estado, oTareaIAPMasiva.t301_estado)
        //        };

        //        return (int)cDblib.Execute("_TareaIAPMasiva_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Elimina un TareaIAPMasiva a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {


        //        return (int)cDblib.Execute("_TareaIAPMasiva_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los TareaIAPMasiva
        ///// </summary>
        //internal List<Models.TareaIAPMasiva> Catalogo(Models.TareaIAPMasiva oTareaIAPMasivaFilter)
        //{
        //    Models.TareaIAPMasiva oTareaIAPMasiva = null;
        //    List<Models.TareaIAPMasiva> lst = new List<Models.TareaIAPMasiva>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[12] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oTEMP_TareaIAPMasivaFilter.t305_idproyectosubnodo),
        //            Param(enumDBFields.t331_idpt, oTEMP_TareaIAPMasivaFilter.t331_idpt),
        //            Param(enumDBFields.denominacion, oTEMP_TareaIAPMasivaFilter.denominacion),
        //            Param(enumDBFields.t323_regfes, oTEMP_TareaIAPMasivaFilter.t323_regfes),
        //            Param(enumDBFields.t323_regjornocompleta, oTEMP_TareaIAPMasivaFilter.t323_regjornocompleta),
        //            Param(enumDBFields.t331_obligaest, oTEMP_TareaIAPMasivaFilter.t331_obligaest),
        //            Param(enumDBFields.t334_idfase, oTEMP_TareaIAPMasivaFilter.t334_idfase),
        //            Param(enumDBFields.t335_idactividad, oTEMP_TareaIAPMasivaFilter.t335_idactividad),
        //            Param(enumDBFields.t332_idtarea, oTEMP_TareaIAPMasivaFilter.t332_idtarea),
        //            Param(enumDBFields.t332_estado, oTEMP_TareaIAPMasivaFilter.t332_estado),
        //            Param(enumDBFields.t332_impiap, oTEMP_TareaIAPMasivaFilter.t332_impiap),
        //            Param(enumDBFields.t301_estado, oTEMP_TareaIAPMasivaFilter.t301_estado)
        //        };

        //        dr = cDblib.DataReader("_TareaIAPMasiva_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oTareaIAPMasiva = new Models.TareaIAPMasiva();
        //            oTareaIAPMasiva.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oTareaIAPMasiva.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
        //            oTareaIAPMasiva.denominacion=Convert.ToString(dr["denominacion"]);
        //            oTareaIAPMasiva.t323_regfes=Convert.ToInt32(dr["t323_regfes"]);
        //            oTareaIAPMasiva.t323_regjornocompleta=Convert.ToInt32(dr["t323_regjornocompleta"]);
        //            oTareaIAPMasiva.t331_obligaest=Convert.ToInt32(dr["t331_obligaest"]);
        //            oTareaIAPMasiva.t334_idfase=Convert.ToInt32(dr["t334_idfase"]);
        //            oTareaIAPMasiva.t335_idactividad=Convert.ToInt32(dr["t335_idactividad"]);
        //            oTareaIAPMasiva.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oTareaIAPMasiva.t332_estado=Convert.ToByte(dr["t332_estado"]);
        //            oTareaIAPMasiva.t332_impiap=Convert.ToBoolean(dr["t332_impiap"]);
        //            oTareaIAPMasiva.t301_estado=Convert.ToString(dr["t301_estado"]);

        //            lst.Add(oTareaIAPMasiva);

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
                /*case enumDBFields.t305_idproyectosubnodo:
					paramName = "@t305_idproyectosubnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t331_idpt:
					paramName = "@t331_idpt";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.denominacion:
					paramName = "@denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 100;
					break;
				case enumDBFields.t323_regfes:
					paramName = "@t323_regfes";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t323_regjornocompleta:
					paramName = "@t323_regjornocompleta";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t331_obligaest:
					paramName = "@t331_obligaest";
					paramType = SqlDbType.Int;
					paramSize = 4;
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
					paramName = "@nTarea";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_estado:
					paramName = "@t332_estado";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.t332_impiap:
					paramName = "@t332_impiap";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t301_estado:
					paramName = "@t301_estado";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;*/
                case enumDBFields.nUsuario:
                    paramName = "@nUsuario";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.dUMCIAP:
                    paramName = "@dUMCIAP";
                    paramType = SqlDbType.Date;
                    paramSize = 8;
                    break;
                case enumDBFields.dDesde:
                    paramName = "@dDesde";
                    paramType = SqlDbType.Date;
                    paramSize = 8;
                    break;
                case enumDBFields.dHasta:
                    paramName = "@dHasta";
                    paramType = SqlDbType.Date;
                    paramSize = 8;
                    break;
                case enumDBFields.idTarea:
                    paramName = "@idTarea";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.bMostrarPSN:
                    paramName = "@bMostrarPSN";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.bMostrarPT:
                    paramName = "@bMostrarPT";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.bMostrarF:
                    paramName = "@bMostrarF";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.bMostrarA:
                    paramName = "@bMostrarA";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.bMostrarT:
                    paramName = "@bMostrarT";
                    paramType = SqlDbType.Bit;
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
