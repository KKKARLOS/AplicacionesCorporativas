using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for FestivosCals
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class FestivosCals 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t067_dia = 1,
            nIdCal = 2, 
            dUMC =3,
            t066_idcal = 4,
            diaDesde = 5,
            diaHasta = 6
        }

        internal FestivosCals(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un FestivosCals
        ///// </summary>
        //internal int Insert(Models.FestivosCals oFestivosCals)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[1] {
        //            Param(enumDBFields.t067_dia, oFestivosCals.t067_dia)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_FestivosCals_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un FestivosCals a partir del id
        ///// </summary>
        //internal Models.FestivosCals Select()
        //{
        //    Models.FestivosCals oFestivosCals = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_FestivosCals_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oFestivosCals = new Models.FestivosCals();
        //            oFestivosCals.t067_dia=Convert.ToDateTime(dr["t067_dia"]);

        //        }
        //        return oFestivosCals;
				
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
        ///// Actualiza un FestivosCals a partir del id
        ///// </summary>
        //internal int Update(Models.FestivosCals oFestivosCals)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[1] {
        //            Param(enumDBFields.t067_dia, oFestivosCals.t067_dia)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_FestivosCals_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un FestivosCals a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_FestivosCals_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los FestivosCals
        ///// </summary>
        internal List<Models.FestivosCals> Catalogo(int nIdCal, DateTime dUMC)
        {
            Models.FestivosCals oFestivosCals = null;
            List<Models.FestivosCals> lst = new List<Models.FestivosCals>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.nIdCal, nIdCal),
                    Param(enumDBFields.dUMC, dUMC)
                };

                dr = cDblib.DataReader("SUP_FESTIVOSCALS", dbparams);
                while (dr.Read())
                {
                    oFestivosCals = new Models.FestivosCals();
                    oFestivosCals.t067_dia = Convert.ToDateTime(dr["t067_dia"]);

                    lst.Add(oFestivosCals);

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

        //// <summary>
        ///// Obtiene todos los FestivosCals
        ///// </summary>
        internal List<Models.FestivosCals> CatalogoRango(int nIdCal, DateTime fechaIni, DateTime fechaFin)
        {
            Models.FestivosCals oFestivosCals = null;
            List<Models.FestivosCals> lst = new List<Models.FestivosCals>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t066_idcal, nIdCal),
                    Param(enumDBFields.diaDesde, fechaIni),
                    Param(enumDBFields.diaHasta, fechaFin)
                };

                dr = cDblib.DataReader("[SUP_FESTIVOSCALENDARIOENTREFECHAS_IAP30]", dbparams);
                while (dr.Read())
                {
                    oFestivosCals = new Models.FestivosCals();
                    oFestivosCals.t067_dia = Convert.ToDateTime(dr["t067_dia"]);

                    lst.Add(oFestivosCals);

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
				case enumDBFields.t067_dia:
					paramName = "@t067_dia";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.nIdCal:
					paramName = "@nIdCal";
                    paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.dUMC:
					paramName = "@dUMC";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
                case enumDBFields.t066_idcal:
                    paramName = "@t066_idcal";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.diaDesde:
                    paramName = "@diadesde";
                    paramType = SqlDbType.Date;
                    paramSize = 8;
                    break;
                case enumDBFields.diaHasta:
                    paramName = "@diahasta";
                    paramType = SqlDbType.Date;
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
