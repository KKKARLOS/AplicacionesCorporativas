using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoIAPMasivaPSN
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ConsumoIAPMasivaPSN 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t305_idproyectosubnodo = 1,
			t301_idproyecto = 2,
			t305_seudonimo = 3,
			t301_denominacion = 4,
			t303_denominacion = 5,
			t302_denominacion = 6,
			responsable = 7,
			t301_estado = 8,
            nUsuario = 9,
            dUMC = 10,
            dInicioImput = 11,
            dFinImput = 12
        }

        internal ConsumoIAPMasivaPSN(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los ConsumoIAPMasivaPSN
        /// </summary>
        /// ObtenerTareasImpMasiva_PSN((hdnIdUsuario.Text != "0") ? int.Parse(hdnIdUsuario.Text) : (int)Session["UsuarioActual"], Fechas.AnnomesAFecha((int)Session["UMC_IAP"]), (hdnInicioImpu.Text == "") ? null : (DateTime?)DateTime.Parse(hdnInicioImpu.Text), (hdnFinImpu.Text == "") ? null : (DateTime?)DateTime.Parse(hdnFinImpu.Text));
        internal List<Models.ConsumoIAPMasivaPSN> Catalogo(Int32 nUsuario, DateTime ultimoMesCerrado, Nullable<DateTime> fechaInicio, Nullable<DateTime> fechaFin)
        {
            Models.ConsumoIAPMasivaPSN oConsumoIAPMasivaPSN = null;
            List<Models.ConsumoIAPMasivaPSN> lst = new List<Models.ConsumoIAPMasivaPSN>();
            IDataReader dr = null;

            try
            {

              
                SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(enumDBFields.nUsuario, nUsuario),
                    Param(enumDBFields.dUMC, ultimoMesCerrado),
                    Param(enumDBFields.dInicioImput, fechaInicio),
                    Param(enumDBFields.dFinImput, fechaFin)
                    
                };

                dr = cDblib.DataReader("SUP_CONSUMOIAPMASIVA_PSN", dbparams);
                while (dr.Read())
                {
                    oConsumoIAPMasivaPSN = new Models.ConsumoIAPMasivaPSN();
                    oConsumoIAPMasivaPSN.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oConsumoIAPMasivaPSN.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oConsumoIAPMasivaPSN.t305_seudonimo = Convert.ToString(dr["t305_seudonimo"]);
                    oConsumoIAPMasivaPSN.t301_denominacion = Convert.ToString(dr["t301_denominacion"]);
                    oConsumoIAPMasivaPSN.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                    oConsumoIAPMasivaPSN.t302_denominacion = Convert.ToString(dr["t302_denominacion"]);
                    if (!Convert.IsDBNull(dr["responsable"]))
                        oConsumoIAPMasivaPSN.responsable = Convert.ToString(dr["responsable"]);
                    oConsumoIAPMasivaPSN.t301_estado = Convert.ToString(dr["t301_estado"]);

                    lst.Add(oConsumoIAPMasivaPSN);

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
        ///// Inserta un ConsumoIAPMasivaPSN
        ///// </summary>
        //internal int Insert(Models.ConsumoIAPMasivaPSN oConsumoIAPMasivaPSN)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[8] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oConsumoIAPMasivaPSN.t305_idproyectosubnodo),
        //            Param(enumDBFields.t301_idproyecto, oConsumoIAPMasivaPSN.t301_idproyecto),
        //            Param(enumDBFields.t305_seudonimo, oConsumoIAPMasivaPSN.t305_seudonimo),
        //            Param(enumDBFields.t301_denominacion, oConsumoIAPMasivaPSN.t301_denominacion),
        //            Param(enumDBFields.t303_denominacion, oConsumoIAPMasivaPSN.t303_denominacion),
        //            Param(enumDBFields.t302_denominacion, oConsumoIAPMasivaPSN.t302_denominacion),
        //            Param(enumDBFields.responsable, oConsumoIAPMasivaPSN.responsable),
        //            Param(enumDBFields.t301_estado, oConsumoIAPMasivaPSN.t301_estado)
        //        };

        //        return (int)cDblib.Execute("_ConsumoIAPMasivaPSN_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un ConsumoIAPMasivaPSN a partir del id
        ///// </summary>
        //internal Models.ConsumoIAPMasivaPSN Select()
        //{
        //    Models.ConsumoIAPMasivaPSN oConsumoIAPMasivaPSN = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_ConsumoIAPMasivaPSN_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oConsumoIAPMasivaPSN = new Models.ConsumoIAPMasivaPSN();
        //            oConsumoIAPMasivaPSN.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oConsumoIAPMasivaPSN.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);
        //            oConsumoIAPMasivaPSN.t305_seudonimo=Convert.ToString(dr["t305_seudonimo"]);
        //            oConsumoIAPMasivaPSN.t301_denominacion=Convert.ToString(dr["t301_denominacion"]);
        //            oConsumoIAPMasivaPSN.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
        //            oConsumoIAPMasivaPSN.t302_denominacion=Convert.ToString(dr["t302_denominacion"]);
        //            if(!Convert.IsDBNull(dr["responsable"]))
        //                oConsumoIAPMasivaPSN.responsable=Convert.ToString(dr["responsable"]);
        //            oConsumoIAPMasivaPSN.t301_estado=Convert.ToString(dr["t301_estado"]);

        //        }
        //        return oConsumoIAPMasivaPSN;
				
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
        ///// Actualiza un ConsumoIAPMasivaPSN a partir del id
        ///// </summary>
        //internal int Update(Models.ConsumoIAPMasivaPSN oConsumoIAPMasivaPSN)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[8] {
        //            Param(enumDBFields.t305_idproyectosubnodo, oConsumoIAPMasivaPSN.t305_idproyectosubnodo),
        //            Param(enumDBFields.t301_idproyecto, oConsumoIAPMasivaPSN.t301_idproyecto),
        //            Param(enumDBFields.t305_seudonimo, oConsumoIAPMasivaPSN.t305_seudonimo),
        //            Param(enumDBFields.t301_denominacion, oConsumoIAPMasivaPSN.t301_denominacion),
        //            Param(enumDBFields.t303_denominacion, oConsumoIAPMasivaPSN.t303_denominacion),
        //            Param(enumDBFields.t302_denominacion, oConsumoIAPMasivaPSN.t302_denominacion),
        //            Param(enumDBFields.responsable, oConsumoIAPMasivaPSN.responsable),
        //            Param(enumDBFields.t301_estado, oConsumoIAPMasivaPSN.t301_estado)
        //        };
                           
        //        return (int)cDblib.Execute("_ConsumoIAPMasivaPSN_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un ConsumoIAPMasivaPSN a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_ConsumoIAPMasivaPSN_DEL", dbparams);
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
				case enumDBFields.t305_idproyectosubnodo:
					paramName = "@t305_idproyectosubnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t301_idproyecto:
					paramName = "@t301_idproyecto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t305_seudonimo:
					paramName = "@t305_seudonimo";
					paramType = SqlDbType.VarChar;
					paramSize = 70;
					break;
				case enumDBFields.t301_denominacion:
					paramName = "@t301_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 70;
					break;
				case enumDBFields.t303_denominacion:
					paramName = "@t303_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t302_denominacion:
					paramName = "@t302_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 100;
					break;
				case enumDBFields.responsable:
					paramName = "@responsable";
					paramType = SqlDbType.VarChar;
					paramSize = 173;
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
