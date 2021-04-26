using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for NotasIAP
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class NotasIAP 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t332_idtarea = 1,
			t332_notas1 = 2,
			t332_notas2 = 3,
			t332_notas3 = 4,
			t332_notas4 = 5
        }

        internal NotasIAP(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
        //#region funciones publicas
        ///// <summary>
        ///// Inserta un NotasIAP
        ///// </summary>
        //internal int Insert(Models.NotasIAP oNotasIAP)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[4] {
        //            Param(enumDBFields.t332_notas1, oNotasIAP.t332_notas1),
        //            Param(enumDBFields.t332_notas2, oNotasIAP.t332_notas2),
        //            Param(enumDBFields.t332_notas3, oNotasIAP.t332_notas3),
        //            Param(enumDBFields.t332_notas4, oNotasIAP.t332_notas4)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_NotasIAP_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un NotasIAP a partir del id
        ///// </summary>
        //internal Models.NotasIAP Select()
        //{
        //    Models.NotasIAP oNotasIAP = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_NotasIAP_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oNotasIAP = new Models.NotasIAP();
        //            oNotasIAP.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oNotasIAP.t332_notas1=Convert.ToString(dr["t332_notas1"]);
        //            oNotasIAP.t332_notas2=Convert.ToString(dr["t332_notas2"]);
        //            oNotasIAP.t332_notas3=Convert.ToString(dr["t332_notas3"]);
        //            oNotasIAP.t332_notas4=Convert.ToString(dr["t332_notas4"]);

        //        }
        //        return oNotasIAP;
				
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
        /// Actualiza un NotasIAP a partir del id
        /// </summary>
        internal int Update(Models.NotasIAP oNotasIAP)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(enumDBFields.t332_idtarea, oNotasIAP.t332_idtarea),
                    Param(enumDBFields.t332_notas1, oNotasIAP.t332_notas1),
                    Param(enumDBFields.t332_notas2, oNotasIAP.t332_notas2),
                    Param(enumDBFields.t332_notas3, oNotasIAP.t332_notas3),
                    Param(enumDBFields.t332_notas4, oNotasIAP.t332_notas4)
                };

                return (int)cDblib.Execute("SUP_NOTASIAP_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
        ///// <summary>
        ///// Elimina un NotasIAP a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_NotasIAP_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los NotasIAP
        ///// </summary>
        //internal List<Models.NotasIAP> Catalogo(Models.NotasIAP oNotasIAPFilter)
        //{
        //    Models.NotasIAP oNotasIAP = null;
        //    List<Models.NotasIAP> lst = new List<Models.NotasIAP>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[5] {
        //            Param(enumDBFields.t332_idtarea, oTEMP_NotasIAPFilter.t332_idtarea),
        //            Param(enumDBFields.t332_notas1, oTEMP_NotasIAPFilter.t332_notas1),
        //            Param(enumDBFields.t332_notas2, oTEMP_NotasIAPFilter.t332_notas2),
        //            Param(enumDBFields.t332_notas3, oTEMP_NotasIAPFilter.t332_notas3),
        //            Param(enumDBFields.t332_notas4, oTEMP_NotasIAPFilter.t332_notas4)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_NotasIAP_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oNotasIAP = new Models.NotasIAP();
        //            oNotasIAP.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
        //            oNotasIAP.t332_notas1=Convert.ToString(dr["t332_notas1"]);
        //            oNotasIAP.t332_notas2=Convert.ToString(dr["t332_notas2"]);
        //            oNotasIAP.t332_notas3=Convert.ToString(dr["t332_notas3"]);
        //            oNotasIAP.t332_notas4=Convert.ToString(dr["t332_notas4"]);

        //            lst.Add(oNotasIAP);

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
				case enumDBFields.t332_idtarea:
					paramName = "@t332_idtarea";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_notas1:
					paramName = "@t332_notas1";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t332_notas2:
					paramName = "@t332_notas2";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t332_notas3:
					paramName = "@t332_notas3";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t332_notas4:
					paramName = "@t332_notas4";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
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
