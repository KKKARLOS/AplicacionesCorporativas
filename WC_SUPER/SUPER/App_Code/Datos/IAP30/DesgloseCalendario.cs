using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for DesgloseCalendario
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class DesgloseCalendario 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            nIdCal = 1,
            nAnno = 2,
            dDesde = 3,
            dHasta = 4,
            t066_idcal = 5
        }

        internal DesgloseCalendario(sqldblib.SqlServerSP extcDblib)
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
        ///// Obtiene todos los DesgloseCalendario
        ///// </summary>;
        internal List<Models.DesgloseCalendario> ObtenerHoras(int nIdCal ,int nAnno )
        {
            Models.DesgloseCalendario oDesgloseCalendario = null;
            List<Models.DesgloseCalendario> lst = new List<Models.DesgloseCalendario>();
            SqlDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t066_idcal, nIdCal),
                    Param(enumDBFields.nAnno, nAnno)
                };

                dr = cDblib.DataReader("SUP_DESGLOSECALENDARIO_IAP30", dbparams);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        oDesgloseCalendario = new Models.DesgloseCalendario();
                        oDesgloseCalendario.t066_idcal = Convert.ToInt32(dr["t066_idcal"]);
                        oDesgloseCalendario.t067_dia = Convert.ToDateTime(dr["t067_dia"]);
                        oDesgloseCalendario.t067_horas = Convert.ToSingle(dr["t067_horas"]);
                        oDesgloseCalendario.t067_horasD = Math.Round(Convert.ToDouble(dr["t067_horas"]), 2);
                        oDesgloseCalendario.t067_festivo = Convert.ToInt32(dr["t067_festivo"]);

                        lst.Add(oDesgloseCalendario);

                    }
                }
                else
                {
                    /*DateTime objDate = new DateTime(nAnno, 1, 1);
                    while (objDate.Year == nAnno)
                    {
                        oDesgloseCalendario = new Models.DesgloseCalendario();
                        oDesgloseCalendario.t066_idcal = nIdCal;
                        oDesgloseCalendario.t067_dia = objDate;
                        oDesgloseCalendario.t067_horas = 0;
                        oDesgloseCalendario.t067_festivo = 0;

                        lst.Add(oDesgloseCalendario);
                        objDate = objDate.AddDays(1);
                    }*/
                    return null;
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

        internal List<Models.DesgloseCalendario> ObtenerHorasRango(int nIdCal, DateTime dDesde, DateTime dHasta)
        {
            Models.DesgloseCalendario oDesgloseCalendario = null;
            List<Models.DesgloseCalendario> lst = new List<Models.DesgloseCalendario>();
            SqlDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(enumDBFields.nIdCal, nIdCal),
                    Param(enumDBFields.nAnno, null),
                    Param(enumDBFields.dDesde, dDesde),
                    Param(enumDBFields.dHasta, dHasta)
                };

                dr = cDblib.DataReader("SUP_DESGLOSECALS", dbparams);

                while (dr.Read())
                {
                    oDesgloseCalendario = new Models.DesgloseCalendario();
                    oDesgloseCalendario.t066_idcal = Convert.ToInt32(dr["t066_idcal"]);
                    oDesgloseCalendario.t067_dia = Convert.ToDateTime(dr["t067_dia"]);
                    oDesgloseCalendario.t067_horas = Convert.ToSingle(dr["t067_horas"]);
                    oDesgloseCalendario.t067_festivo = Convert.ToInt32(dr["t067_festivo"]);

                    lst.Add(oDesgloseCalendario);

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

        internal List<Models.DesgloseCalendario> DetalleCalendarioSemana(int nIdCal, DateTime dDesde, DateTime dHasta)
        {
            Models.DesgloseCalendario oDesgloseCalendario = null;
            List<Models.DesgloseCalendario> lst = new List<Models.DesgloseCalendario>();
            SqlDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t066_idcal, nIdCal),
                    Param(enumDBFields.dDesde, dDesde),
                    Param(enumDBFields.dHasta, dHasta)
                };

                dr = cDblib.DataReader("SUP_DETALLECALENDARIOSEMANA_IAP30", dbparams);

                while (dr.Read())
                {
                    oDesgloseCalendario = new Models.DesgloseCalendario();
                    oDesgloseCalendario.t067_dia = Convert.ToDateTime(dr["t067_dia"]);
                    oDesgloseCalendario.t067_horas = Convert.ToSingle(dr["t067_horas"]);
                    oDesgloseCalendario.t067_festivo = Convert.ToInt32(dr["t067_festivo"]);

                    lst.Add(oDesgloseCalendario);

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
                case enumDBFields.nIdCal:
                    paramName = "@nIdCal";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.t066_idcal:
                    paramName = "@t066_idcal";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.nAnno:
                    paramName = "@nAnno";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
					break;
                case enumDBFields.dDesde:
                    paramName = "@dDesde";
                    paramType = SqlDbType.DateTime;
                    paramSize = 8;
                    break;
                case enumDBFields.dHasta:
                    paramName = "@dHasta";
                    paramType = SqlDbType.DateTime;
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
