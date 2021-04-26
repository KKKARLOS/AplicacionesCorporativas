using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoIAPMasivaPT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ConsumoIAPMasivaPT 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			nivel = 1,
			t331_idpt = 2,
			t331_despt = 3,
			t331_orden = 4,
            nUsuario = 5,
            nPSN = 6,
            dUMC = 7,
            dInicioImput = 8,
            dFinImput = 9
        }

        internal ConsumoIAPMasivaPT(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los ConsumoIAPMasivaPT
        /// </summary>
        internal List<Models.ConsumoIAPMasivaPT> Catalogo(Int32 nUsuario, Int32 nPSN, DateTime ultimoMesCerrado, Nullable<DateTime> fechaInicio, Nullable<DateTime> fechaFin)
        {
            Models.ConsumoIAPMasivaPT oConsumoIAPMasivaPT = null;
            List<Models.ConsumoIAPMasivaPT> lst = new List<Models.ConsumoIAPMasivaPT>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(enumDBFields.nUsuario, nUsuario),
                    Param(enumDBFields.nPSN, nPSN),
                    Param(enumDBFields.dUMC, ultimoMesCerrado),
                    Param(enumDBFields.dInicioImput, fechaInicio),
                    Param(enumDBFields.dFinImput, fechaFin)
                };

                dr = cDblib.DataReader("SUP_CONSUMOIAPMASIVA_PT", dbparams);
                while (dr.Read())
                {
                    oConsumoIAPMasivaPT = new Models.ConsumoIAPMasivaPT();
                    oConsumoIAPMasivaPT.nivel = Convert.ToInt32(dr["nivel"]);
                    oConsumoIAPMasivaPT.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    oConsumoIAPMasivaPT.t331_despt = Convert.ToString(dr["t331_despt"]);
                    oConsumoIAPMasivaPT.t331_orden = Convert.ToInt32(dr["t331_orden"]);

                    lst.Add(oConsumoIAPMasivaPT);

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
        ///// Inserta un ConsumoIAPMasivaPT
        ///// </summary>
        //internal int Insert(Models.ConsumoIAPMasivaPT oConsumoIAPMasivaPT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[4] {
        //            Param(enumDBFields.nivel, oConsumoIAPMasivaPT.nivel),
        //            Param(enumDBFields.t331_idpt, oConsumoIAPMasivaPT.t331_idpt),
        //            Param(enumDBFields.t331_despt, oConsumoIAPMasivaPT.t331_despt),
        //            Param(enumDBFields.t331_orden, oConsumoIAPMasivaPT.t331_orden)
        //        };

        //        return (int)cDblib.Execute("_ConsumoIAPMasivaPT_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un ConsumoIAPMasivaPT a partir del id
        ///// </summary>
        //internal Models.ConsumoIAPMasivaPT Select()
        //{
        //    Models.ConsumoIAPMasivaPT oConsumoIAPMasivaPT = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_ConsumoIAPMasivaPT_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oConsumoIAPMasivaPT = new Models.ConsumoIAPMasivaPT();
        //            oConsumoIAPMasivaPT.nivel=Convert.ToInt32(dr["nivel"]);
        //            oConsumoIAPMasivaPT.t331_idpt=Convert.ToInt32(dr["t331_idpt"]);
        //            oConsumoIAPMasivaPT.t331_despt=Convert.ToString(dr["t331_despt"]);
        //            oConsumoIAPMasivaPT.t331_orden=Convert.ToInt32(dr["t331_orden"]);

        //        }
        //        return oConsumoIAPMasivaPT;
				
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
        ///// Actualiza un ConsumoIAPMasivaPT a partir del id
        ///// </summary>
        //internal int Update(Models.ConsumoIAPMasivaPT oConsumoIAPMasivaPT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[4] {
        //            Param(enumDBFields.nivel, oConsumoIAPMasivaPT.nivel),
        //            Param(enumDBFields.t331_idpt, oConsumoIAPMasivaPT.t331_idpt),
        //            Param(enumDBFields.t331_despt, oConsumoIAPMasivaPT.t331_despt),
        //            Param(enumDBFields.t331_orden, oConsumoIAPMasivaPT.t331_orden)
        //        };
                           
        //        return (int)cDblib.Execute("_ConsumoIAPMasivaPT_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un ConsumoIAPMasivaPT a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_ConsumoIAPMasivaPT_DEL", dbparams);
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
				case enumDBFields.t331_idpt:
					paramName = "@t331_idpt";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t331_despt:
					paramName = "@t331_despt";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t331_orden:
					paramName = "@t331_orden";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.nUsuario:
                    paramName = "@nUsuario";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.nPSN:
                    paramName = "@nPSN";
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
