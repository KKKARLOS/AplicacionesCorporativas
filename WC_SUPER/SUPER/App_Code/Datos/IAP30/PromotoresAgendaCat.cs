using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for PromotoresAgendaCat
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class PromotoresAgendaCat 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t458_idPlanif = 1,
            t001_idficepi_mod = 2,
			Motivo = 3,
			t458_fechoraini = 4,
			t458_fechorafin = 5,
			Profesional = 6,
			t001_codred_promotor = 7,
            t001_idficepi = 8
			
        }

        internal PromotoresAgendaCat(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un PromotoresAgendaCat
        ///// </summary>
        //internal int Insert(Models.PromotoresAgendaCat oPromotoresAgendaCat)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[7] {
        //            Param(enumDBFields.t458_idPlanif, oPromotoresAgendaCat.t458_idPlanif),
        //            Param(enumDBFields.t001_idficepi_mod, oPromotoresAgendaCat.t001_idficepi_mod),
        //            Param(enumDBFields.Motivo, oPromotoresAgendaCat.Motivo),
        //            Param(enumDBFields.t458_fechoraini, oPromotoresAgendaCat.t458_fechoraini),
        //            Param(enumDBFields.t458_fechorafin, oPromotoresAgendaCat.t458_fechorafin),
        //            Param(enumDBFields.Profesional, oPromotoresAgendaCat.Profesional),
        //            Param(enumDBFields.t001_codred_promotor, oPromotoresAgendaCat.t001_codred_promotor)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_PromotoresAgendaCat_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un PromotoresAgendaCat a partir del id
        ///// </summary>
        //internal Models.PromotoresAgendaCat Select()
        //{
        //    Models.PromotoresAgendaCat oPromotoresAgendaCat = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_PromotoresAgendaCat_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oPromotoresAgendaCat = new Models.PromotoresAgendaCat();
        //            oPromotoresAgendaCat.t458_idPlanif=Convert.ToInt32(dr["t458_idPlanif"]);
        //            oPromotoresAgendaCat.t001_idficepi_mod=Convert.ToInt32(dr["t001_idficepi_mod"]);
        //            oPromotoresAgendaCat.Motivo=Convert.ToString(dr["Motivo"]);
        //            oPromotoresAgendaCat.t458_fechoraini=Convert.ToDateTime(dr["t458_fechoraini"]);
        //            oPromotoresAgendaCat.t458_fechorafin=Convert.ToDateTime(dr["t458_fechorafin"]);
        //            oPromotoresAgendaCat.Profesional=Convert.ToString(dr["Profesional"]);
        //            oPromotoresAgendaCat.t001_codred_promotor=Convert.ToString(dr["t001_codred_promotor"]);

        //        }
        //        return oPromotoresAgendaCat;
				
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
        ///// Actualiza un PromotoresAgendaCat a partir del id
        ///// </summary>
        //internal int Update(Models.PromotoresAgendaCat oPromotoresAgendaCat)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[7] {
        //            Param(enumDBFields.t458_idPlanif, oPromotoresAgendaCat.t458_idPlanif),
        //            Param(enumDBFields.t001_idficepi_mod, oPromotoresAgendaCat.t001_idficepi_mod),
        //            Param(enumDBFields.Motivo, oPromotoresAgendaCat.Motivo),
        //            Param(enumDBFields.t458_fechoraini, oPromotoresAgendaCat.t458_fechoraini),
        //            Param(enumDBFields.t458_fechorafin, oPromotoresAgendaCat.t458_fechorafin),
        //            Param(enumDBFields.Profesional, oPromotoresAgendaCat.Profesional),
        //            Param(enumDBFields.t001_codred_promotor, oPromotoresAgendaCat.t001_codred_promotor)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_PromotoresAgendaCat_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un PromotoresAgendaCat a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_PromotoresAgendaCat_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Obtiene todos los PromotoresAgendaCat
        /// </summary>
        internal List<Models.PromotoresAgendaCat> Catalogo(Models.PromotoresAgendaCat oPromotoresAgendaCatFilter)
        {
            Models.PromotoresAgendaCat oPromotoresAgendaCat = null;
            List<Models.PromotoresAgendaCat> lst = new List<Models.PromotoresAgendaCat>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(enumDBFields.t458_idPlanif, oPromotoresAgendaCatFilter.t458_idPlanif),
                    Param(enumDBFields.t458_fechoraini, oPromotoresAgendaCatFilter.t458_fechoraini),
                    Param(enumDBFields.t458_fechorafin, oPromotoresAgendaCatFilter.t458_fechorafin),
                    Param(enumDBFields.t001_idficepi, oPromotoresAgendaCatFilter.t001_idficepi)
                };

                dr = cDblib.DataReader("SUP_PROMOTORESAGENDA_CAT", dbparams);
                while (dr.Read())
                {
                    oPromotoresAgendaCat = new Models.PromotoresAgendaCat();
                    oPromotoresAgendaCat.t458_idPlanif = Convert.ToInt32(dr["t458_idPlanif"]);
                    oPromotoresAgendaCat.t001_idficepi_mod = Convert.ToInt32(dr["t001_idficepi_mod"]);
                    oPromotoresAgendaCat.Motivo = Convert.ToString(dr["Motivo"]);
                    oPromotoresAgendaCat.t458_fechoraini = Convert.ToDateTime(dr["t458_fechoraini"]);
                    oPromotoresAgendaCat.t458_fechorafin = Convert.ToDateTime(dr["t458_fechorafin"]);
                    oPromotoresAgendaCat.Profesional = Convert.ToString(dr["Profesional"]);
                    oPromotoresAgendaCat.t001_codred_promotor = Convert.ToString(dr["t001_codred_promotor"]);

                    lst.Add(oPromotoresAgendaCat);

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
				case enumDBFields.t458_idPlanif:
					paramName = "@t458_idPlanif";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
				case enumDBFields.t001_idficepi_mod:
					paramName = "@t001_idficepi_mod";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.Motivo:
					paramName = "@Motivo";
					paramType = SqlDbType.VarChar;
					paramSize = 100;
					break;
				case enumDBFields.t458_fechoraini:
					paramName = "@t458_fechoraini";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t458_fechorafin:
					paramName = "@t458_fechorafin";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.Profesional:
					paramName = "@Profesional";
					paramType = SqlDbType.VarChar;
					paramSize = 73;
					break;
				case enumDBFields.t001_codred_promotor:
					paramName = "@t001_codred_promotor";
					paramType = SqlDbType.VarChar;
					paramSize = 15;
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
