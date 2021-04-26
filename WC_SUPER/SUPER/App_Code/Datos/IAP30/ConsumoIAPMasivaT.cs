using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoIAPMasivaT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ConsumoIAPMasivaT 
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
			t332_estado = 6,
			denominacion = 7,
			t332_impiap = 8,
			t323_regjornocompleta = 9,
			t323_regfes = 10,
			t331_obligaest = 11,
			orden = 12,
			t301_estado = 13,
            nUsuario = 14,
            nPT = 15,
            dUMC = 16,
            dInicioImput = 17,
            dFinImput = 18
        }

        internal ConsumoIAPMasivaT(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los ConsumoIAPMasivaT
        /// </summary>
        internal List<Models.ConsumoIAPMasivaT> Catalogo(Int32 nUsuario, Int32 nPT, DateTime ultimoMesCerrado, Nullable<DateTime> fechaInicio, Nullable<DateTime> fechaFin)
        {
            Models.ConsumoIAPMasivaT oConsumoIAPMasivaT = null;
            List<Models.ConsumoIAPMasivaT> lst = new List<Models.ConsumoIAPMasivaT>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(enumDBFields.nUsuario, nUsuario),
                    Param(enumDBFields.nPT, nPT),
                    Param(enumDBFields.dUMC, ultimoMesCerrado),
                    Param(enumDBFields.dInicioImput, fechaInicio),
                    Param(enumDBFields.dFinImput, fechaFin)
                };

                dr = cDblib.DataReader("SUP_CONSUMOIAPMASIVA_T", dbparams);
                while (dr.Read())
                {
                    oConsumoIAPMasivaT = new Models.ConsumoIAPMasivaT();
                    if (!Convert.IsDBNull(dr["nivel"]))
                        oConsumoIAPMasivaT.nivel = Convert.ToInt32(dr["nivel"]);
                    oConsumoIAPMasivaT.tipo = Convert.ToString(dr["tipo"]);
                    oConsumoIAPMasivaT.t334_idfase = Convert.ToInt32(dr["t334_idfase"]);
                    oConsumoIAPMasivaT.t335_idactividad = Convert.ToInt32(dr["t335_idactividad"]);
                    oConsumoIAPMasivaT.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    oConsumoIAPMasivaT.t332_estado = Convert.ToInt32(dr["t332_estado"]);
                    oConsumoIAPMasivaT.denominacion = Convert.ToString(dr["denominacion"]);
                    oConsumoIAPMasivaT.t332_impiap = Convert.ToInt32(dr["t332_impiap"]);
                    oConsumoIAPMasivaT.t323_regjornocompleta = Convert.ToInt32(dr["t323_regjornocompleta"]);
                    oConsumoIAPMasivaT.t323_regfes = Convert.ToInt32(dr["t323_regfes"]);
                    oConsumoIAPMasivaT.t331_obligaest = Convert.ToInt32(dr["t331_obligaest"]);
                    if (!Convert.IsDBNull(dr["orden"]))
                        oConsumoIAPMasivaT.orden = Convert.ToString(dr["orden"]);
                    oConsumoIAPMasivaT.t301_estado = Convert.ToString(dr["t301_estado"]);

                    lst.Add(oConsumoIAPMasivaT);

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
        ///// <summary>
        ///// Inserta un ConsumoIAPMasivaT
        ///// </summary>
        //internal int Insert(Models.ConsumoIAPMasivaT oConsumoIAPMasivaT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.nivel, oConsumoIAPMasivaT.nivel),
        //            Param(enumDBFields.tipo, oConsumoIAPMasivaT.tipo),
        //            Param(enumDBFields.t334_idfase, oConsumoIAPMasivaT.t334_idfase),
        //            Param(enumDBFields.t335_idactividad, oConsumoIAPMasivaT.t335_idactividad),
        //            Param(enumDBFields.t332_idtarea, oConsumoIAPMasivaT.t332_idtarea),
        //            Param(enumDBFields.t332_estado, oConsumoIAPMasivaT.t332_estado),
        //            Param(enumDBFields.denominacion, oConsumoIAPMasivaT.denominacion),
        //            Param(enumDBFields.t332_impiap, oConsumoIAPMasivaT.t332_impiap),
        //            Param(enumDBFields.t323_regjornocompleta, oConsumoIAPMasivaT.t323_regjornocompleta),
        //            Param(enumDBFields.t323_regfes, oConsumoIAPMasivaT.t323_regfes),
        //            Param(enumDBFields.t331_obligaest, oConsumoIAPMasivaT.t331_obligaest),
        //            Param(enumDBFields.orden, oConsumoIAPMasivaT.orden),
        //            Param(enumDBFields.t301_estado, oConsumoIAPMasivaT.t301_estado)
        //        };

        //        return (int)cDblib.Execute("_ConsumoIAPMasivaT_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un ConsumoIAPMasivaT a partir del id
        ///// </summary>
        //internal Models.ConsumoIAPMasivaT Select()
        //{
        //    Models.ConsumoIAPMasivaT oConsumoIAPMasivaT = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_ConsumoIAPMasivaT_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oConsumoIAPMasivaT = new Models.ConsumoIAPMasivaT();
        //            if(!Convert.IsDBNull(dr["nivel"]))
        //                oConsumoIAPMasivaT.nivel=Convert.ToInt32(dr["nivel"]);
        //            oConsumoIAPMasivaT.tipo=Convert.ToString(dr["tipo"]);
        //            oConsumoIAPMasivaT.t334_idfase=Convert.ToInt32(dr["t334_idfase"]);
        //            oConsumoIAPMasivaT.t335_idactividad=Convert.ToInt32(dr["t335_idactividad"]);
        //            oConsumoIAPMasivaT.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oConsumoIAPMasivaT.t332_estado=Convert.ToInt32(dr["t332_estado"]);
        //            oConsumoIAPMasivaT.denominacion=Convert.ToString(dr["denominacion"]);
        //            oConsumoIAPMasivaT.t332_impiap=Convert.ToInt32(dr["t332_impiap"]);
        //            oConsumoIAPMasivaT.t323_regjornocompleta=Convert.ToInt32(dr["t323_regjornocompleta"]);
        //            oConsumoIAPMasivaT.t323_regfes=Convert.ToInt32(dr["t323_regfes"]);
        //            oConsumoIAPMasivaT.t331_obligaest=Convert.ToInt32(dr["t331_obligaest"]);
        //            if(!Convert.IsDBNull(dr["orden"]))
        //                oConsumoIAPMasivaT.orden=Convert.ToString(dr["orden"]);
        //            oConsumoIAPMasivaT.t301_estado=Convert.ToString(dr["t301_estado"]);

        //        }
        //        return oConsumoIAPMasivaT;
				
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
        ///// Actualiza un ConsumoIAPMasivaT a partir del id
        ///// </summary>
        //internal int Update(Models.ConsumoIAPMasivaT oConsumoIAPMasivaT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[13] {
        //            Param(enumDBFields.nivel, oConsumoIAPMasivaT.nivel),
        //            Param(enumDBFields.tipo, oConsumoIAPMasivaT.tipo),
        //            Param(enumDBFields.t334_idfase, oConsumoIAPMasivaT.t334_idfase),
        //            Param(enumDBFields.t335_idactividad, oConsumoIAPMasivaT.t335_idactividad),
        //            Param(enumDBFields.t332_idtarea, oConsumoIAPMasivaT.t332_idtarea),
        //            Param(enumDBFields.t332_estado, oConsumoIAPMasivaT.t332_estado),
        //            Param(enumDBFields.denominacion, oConsumoIAPMasivaT.denominacion),
        //            Param(enumDBFields.t332_impiap, oConsumoIAPMasivaT.t332_impiap),
        //            Param(enumDBFields.t323_regjornocompleta, oConsumoIAPMasivaT.t323_regjornocompleta),
        //            Param(enumDBFields.t323_regfes, oConsumoIAPMasivaT.t323_regfes),
        //            Param(enumDBFields.t331_obligaest, oConsumoIAPMasivaT.t331_obligaest),
        //            Param(enumDBFields.orden, oConsumoIAPMasivaT.orden),
        //            Param(enumDBFields.t301_estado, oConsumoIAPMasivaT.t301_estado)
        //        };
                           
        //        return (int)cDblib.Execute("_ConsumoIAPMasivaT_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un ConsumoIAPMasivaT a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_ConsumoIAPMasivaT_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
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
				case enumDBFields.t332_estado:
					paramName = "@t332_estado";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.denominacion:
					paramName = "@denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 100;
					break;
				case enumDBFields.t332_impiap:
					paramName = "@t332_impiap";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t323_regjornocompleta:
					paramName = "@t323_regjornocompleta";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t323_regfes:
					paramName = "@t323_regfes";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t331_obligaest:
					paramName = "@t331_obligaest";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.orden:
					paramName = "@orden";
					paramType = SqlDbType.VarChar;
					paramSize = 8000;
					break;
				case enumDBFields.t301_estado:
					paramName = "@t301_estado";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
                case enumDBFields.nUsuario:
                    paramName = "@nUsuario";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.nPT:
                    paramName = "@nPT";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.dUMC:
                    paramName = "@dUMC";
                    paramType = SqlDbType.SmallDateTime;
                    paramSize = 8;
                    break;
                case enumDBFields.dInicioImput:
                    paramName = "@dInicioImput";
                    paramType = SqlDbType.SmallDateTime;
                    paramSize = 8;
                    break;
                case enumDBFields.dFinImput:
                    paramName = "@dFinImput";
                    paramType = SqlDbType.SmallDateTime;
                    paramSize = 8;
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
