using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for EmpresasProfesional
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class EmpresasProfesional 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t313_idempresa = 1,
			t313_denominacion = 2,
			t007_idterrfis = 3,
			t007_nomterrfis = 4,
			T007_ITERDC = 5,
			T007_ITERMD = 6,
			T007_ITERDA = 7,
			T007_ITERDE = 8,
			T007_ITERK = 9
        }

        internal EmpresasProfesional(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un EmpresasProfesional
        ///// </summary>
        //internal int Insert(Models.EmpresasProfesional oEmpresasProfesional)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[9] {
        //            Param(enumDBFields.t313_idempresa, oEmpresasProfesional.t313_idempresa),
        //            Param(enumDBFields.t313_denominacion, oEmpresasProfesional.t313_denominacion),
        //            Param(enumDBFields.t007_idterrfis, oEmpresasProfesional.t007_idterrfis),
        //            Param(enumDBFields.t007_nomterrfis, oEmpresasProfesional.t007_nomterrfis),
        //            Param(enumDBFields.T007_ITERDC, oEmpresasProfesional.T007_ITERDC),
        //            Param(enumDBFields.T007_ITERMD, oEmpresasProfesional.T007_ITERMD),
        //            Param(enumDBFields.T007_ITERDA, oEmpresasProfesional.T007_ITERDA),
        //            Param(enumDBFields.T007_ITERDE, oEmpresasProfesional.T007_ITERDE),
        //            Param(enumDBFields.T007_ITERK, oEmpresasProfesional.T007_ITERK)
        //        };

        //        return (int)cDblib.Execute("_EmpresasProfesional_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un EmpresasProfesional a partir del id
        ///// </summary>
        //internal Models.EmpresasProfesional Select()
        //{
        //    Models.EmpresasProfesional oEmpresasProfesional = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_EmpresasProfesional_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oEmpresasProfesional = new Models.EmpresasProfesional();
        //            oEmpresasProfesional.t313_idempresa=Convert.ToInt32(dr["t313_idempresa"]);
        //            oEmpresasProfesional.t313_denominacion=Convert.ToString(dr["t313_denominacion"]);
        //            if(!Convert.IsDBNull(dr["t007_idterrfis"]))
        //                oEmpresasProfesional.t007_idterrfis=Convert.ToInt16(dr["t007_idterrfis"]);
        //            oEmpresasProfesional.t007_nomterrfis=Convert.ToString(dr["t007_nomterrfis"]);
        //            oEmpresasProfesional.T007_ITERDC=Convert.ToDecimal(dr["T007_ITERDC"]);
        //            oEmpresasProfesional.T007_ITERMD=Convert.ToDecimal(dr["T007_ITERMD"]);
        //            oEmpresasProfesional.T007_ITERDA=Convert.ToDecimal(dr["T007_ITERDA"]);
        //            oEmpresasProfesional.T007_ITERDE=Convert.ToDecimal(dr["T007_ITERDE"]);
        //            oEmpresasProfesional.T007_ITERK=Convert.ToDecimal(dr["T007_ITERK"]);

        //        }
        //        return oEmpresasProfesional;
				
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
        ///// Actualiza un EmpresasProfesional a partir del id
        ///// </summary>
        //internal int Update(Models.EmpresasProfesional oEmpresasProfesional)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[9] {
        //            Param(enumDBFields.t313_idempresa, oEmpresasProfesional.t313_idempresa),
        //            Param(enumDBFields.t313_denominacion, oEmpresasProfesional.t313_denominacion),
        //            Param(enumDBFields.t007_idterrfis, oEmpresasProfesional.t007_idterrfis),
        //            Param(enumDBFields.t007_nomterrfis, oEmpresasProfesional.t007_nomterrfis),
        //            Param(enumDBFields.T007_ITERDC, oEmpresasProfesional.T007_ITERDC),
        //            Param(enumDBFields.T007_ITERMD, oEmpresasProfesional.T007_ITERMD),
        //            Param(enumDBFields.T007_ITERDA, oEmpresasProfesional.T007_ITERDA),
        //            Param(enumDBFields.T007_ITERDE, oEmpresasProfesional.T007_ITERDE),
        //            Param(enumDBFields.T007_ITERK, oEmpresasProfesional.T007_ITERK)
        //        };
                           
        //        return (int)cDblib.Execute("_EmpresasProfesional_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un EmpresasProfesional a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_EmpresasProfesional_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los EmpresasProfesional
        ///// </summary>
        //internal List<Models.EmpresasProfesional> Catalogo(Models.EmpresasProfesional oEmpresasProfesionalFilter)
        //{
        //    Models.EmpresasProfesional oEmpresasProfesional = null;
        //    List<Models.EmpresasProfesional> lst = new List<Models.EmpresasProfesional>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[9] {
        //            Param(enumDBFields.t313_idempresa, oTEMP_EmpresasProfesionalFilter.t313_idempresa),
        //            Param(enumDBFields.t313_denominacion, oTEMP_EmpresasProfesionalFilter.t313_denominacion),
        //            Param(enumDBFields.t007_idterrfis, oTEMP_EmpresasProfesionalFilter.t007_idterrfis),
        //            Param(enumDBFields.t007_nomterrfis, oTEMP_EmpresasProfesionalFilter.t007_nomterrfis),
        //            Param(enumDBFields.T007_ITERDC, oTEMP_EmpresasProfesionalFilter.T007_ITERDC),
        //            Param(enumDBFields.T007_ITERMD, oTEMP_EmpresasProfesionalFilter.T007_ITERMD),
        //            Param(enumDBFields.T007_ITERDA, oTEMP_EmpresasProfesionalFilter.T007_ITERDA),
        //            Param(enumDBFields.T007_ITERDE, oTEMP_EmpresasProfesionalFilter.T007_ITERDE),
        //            Param(enumDBFields.T007_ITERK, oTEMP_EmpresasProfesionalFilter.T007_ITERK)
        //        };

        //        dr = cDblib.DataReader("_EmpresasProfesional_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oEmpresasProfesional = new Models.EmpresasProfesional();
        //            oEmpresasProfesional.t313_idempresa=Convert.ToInt32(dr["t313_idempresa"]);
        //            oEmpresasProfesional.t313_denominacion=Convert.ToString(dr["t313_denominacion"]);
        //            if(!Convert.IsDBNull(dr["t007_idterrfis"]))
        //                oEmpresasProfesional.t007_idterrfis=Convert.ToInt16(dr["t007_idterrfis"]);
        //            oEmpresasProfesional.t007_nomterrfis=Convert.ToString(dr["t007_nomterrfis"]);
        //            oEmpresasProfesional.T007_ITERDC=Convert.ToDecimal(dr["T007_ITERDC"]);
        //            oEmpresasProfesional.T007_ITERMD=Convert.ToDecimal(dr["T007_ITERMD"]);
        //            oEmpresasProfesional.T007_ITERDA=Convert.ToDecimal(dr["T007_ITERDA"]);
        //            oEmpresasProfesional.T007_ITERDE=Convert.ToDecimal(dr["T007_ITERDE"]);
        //            oEmpresasProfesional.T007_ITERK=Convert.ToDecimal(dr["T007_ITERK"]);

        //            lst.Add(oEmpresasProfesional);

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
				case enumDBFields.t313_idempresa:
					paramName = "@t313_idempresa";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t313_denominacion:
					paramName = "@t313_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t007_idterrfis:
					paramName = "@t007_idterrfis";
					paramType = SqlDbType.SmallInt;
					paramSize = 2;
					break;
				case enumDBFields.t007_nomterrfis:
					paramName = "@t007_nomterrfis";
					paramType = SqlDbType.VarChar;
					paramSize = 25;
					break;
				case enumDBFields.T007_ITERDC:
					paramName = "@T007_ITERDC";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.T007_ITERMD:
					paramName = "@T007_ITERMD";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.T007_ITERDA:
					paramName = "@T007_ITERDA";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.T007_ITERDE:
					paramName = "@T007_ITERDE";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.T007_ITERK:
					paramName = "@T007_ITERK";
					paramType = SqlDbType.Money;
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
