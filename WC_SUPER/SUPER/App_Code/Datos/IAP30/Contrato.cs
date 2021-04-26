using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Contrato
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class Contrato 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t306_idcontrato = 1,
            t302_denominacion = 2,
            t302_idcliente = 3,
			t377_denominacion = 4,
            t314_idusuario = 5,
            t303_idnodo = 6,
            bMostrarTodos = 7,
            sTipoBusq = 8

        }

        internal Contrato(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene un Contrato a partir del id
        /// </summary>
        internal Models.Contrato ObtenerExtensionPadre(int t306_idcontrato)
        {
            Models.Contrato oContrato = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.t306_idcontrato, t306_idcontrato),
				};

                dr = cDblib.DataReader("SUP_CONTRATO_EXTEN_PADRE", dbparams);
				if (dr.Read())
				{
					oContrato = new Models.Contrato();
					oContrato.t306_idcontrato=Convert.ToInt32(dr["t306_idcontrato"]);
                    oContrato.t377_denominacion = Convert.ToString(dr["t377_denominacion"]);
                    oContrato.t302_idcliente_contrato = Convert.ToInt32(dr["t302_idcliente"]);
                    oContrato.t302_denominacion = Convert.ToString(dr["t302_denominacion"]);                    					
				}
				return oContrato;
				
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

		/// <summary>
        /// Obtiene todos los Contrato para un usuario
        /// </summary>
        internal List<Models.Contrato> CatalogoUsu(int t314_idusuario, Nullable<int> t303_idnodo, bool bMostrarTodos, Nullable<int> t306_idcontrato, string t377_denominacion, string sTipoBusq, Nullable<int> t302_idcliente)
		{
			Models.Contrato oContrato = null;
            List<Models.Contrato> lst = new List<Models.Contrato>();
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[7] {


					Param(enumDBFields.t314_idusuario, t314_idusuario),
					Param(enumDBFields.t303_idnodo, t303_idnodo),
					Param(enumDBFields.bMostrarTodos, bMostrarTodos),
					Param(enumDBFields.t306_idcontrato, t306_idcontrato),
					Param(enumDBFields.t377_denominacion, t377_denominacion),
					Param(enumDBFields.sTipoBusq, sTipoBusq),
					Param(enumDBFields.t302_idcliente, t302_idcliente)
				};

                dr = cDblib.DataReader("SUP_GETCONTRATO_VISION_PROY_USU", dbparams);
				while (dr.Read())
				{
					oContrato = new Models.Contrato();
					oContrato.t306_idcontrato=Convert.ToInt32(dr["t306_idcontrato"]);
					oContrato.t302_idcliente_contrato=Convert.ToInt32(dr["t302_idcliente_contrato"]);
					oContrato.t302_denominacion=Convert.ToString(dr["t302_denominacion"]);
					oContrato.t377_denominacion=Convert.ToString(dr["t377_denominacion"]);
					oContrato.t377_idextension=Convert.ToInt32(dr["t377_idextension"]);
					if(!Convert.IsDBNull(dr["importe_servicio"]))
						oContrato.importe_servicio=Convert.ToDecimal(dr["importe_servicio"]);
					if(!Convert.IsDBNull(dr["importe_producto"]))
						oContrato.importe_producto=Convert.ToDecimal(dr["importe_producto"]);
					if(!Convert.IsDBNull(dr["pendiente_servicio"]))
						oContrato.pendiente_servicio=Convert.ToDecimal(dr["pendiente_servicio"]);
					if(!Convert.IsDBNull(dr["pendiente_producto"]))
						oContrato.pendiente_producto=Convert.ToDecimal(dr["pendiente_producto"]);

                    lst.Add(oContrato);

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

        /// <summary>
        /// Obtiene todos los Contrato para un administrador
        /// </summary>
        internal List<Models.Contrato> CatalogoADM(int t314_idusuario, Nullable<int> t303_idnodo, bool bMostrarTodos, Nullable<int> t306_idcontrato, string t377_denominacion, string sTipoBusq, Nullable<int> t302_idcliente)
        {
            Models.Contrato oContrato = null;
            List<Models.Contrato> lst = new List<Models.Contrato>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[7] {


					Param(enumDBFields.t314_idusuario, t314_idusuario),
					Param(enumDBFields.t303_idnodo, t303_idnodo),
					Param(enumDBFields.bMostrarTodos, bMostrarTodos),
					Param(enumDBFields.t306_idcontrato, t306_idcontrato),
					Param(enumDBFields.t377_denominacion, t377_denominacion),
					Param(enumDBFields.sTipoBusq, sTipoBusq),
					Param(enumDBFields.t302_idcliente, t302_idcliente)
				};

                dr = cDblib.DataReader("SUP_GETCONTRATO_VISION_PROY_ADM", dbparams);
                while (dr.Read())
                {
                    oContrato = new Models.Contrato();
                    oContrato.t306_idcontrato = Convert.ToInt32(dr["t306_idcontrato"]);
                    oContrato.t302_idcliente_contrato = Convert.ToInt32(dr["t302_idcliente_contrato"]);
                    oContrato.t302_denominacion = Convert.ToString(dr["t302_denominacion"]);
                    oContrato.t377_denominacion = Convert.ToString(dr["t377_denominacion"]);
                    oContrato.t377_idextension = Convert.ToInt32(dr["t377_idextension"]);
                    if (!Convert.IsDBNull(dr["importe_servicio"]))
                        oContrato.importe_servicio = Convert.ToDecimal(dr["importe_servicio"]);
                    if (!Convert.IsDBNull(dr["importe_producto"]))
                        oContrato.importe_producto = Convert.ToDecimal(dr["importe_producto"]);
                    if (!Convert.IsDBNull(dr["pendiente_servicio"]))
                        oContrato.pendiente_servicio = Convert.ToDecimal(dr["pendiente_servicio"]);
                    if (!Convert.IsDBNull(dr["pendiente_producto"]))
                        oContrato.pendiente_producto = Convert.ToDecimal(dr["pendiente_producto"]);

                    lst.Add(oContrato);

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
				case enumDBFields.t306_idcontrato:
					paramName = "@t306_idcontrato";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t302_idcliente:
					paramName = "@t302_idcliente";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t302_denominacion:
					paramName = "@t302_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 100;
					break;
				case enumDBFields.t377_denominacion:
					paramName = "@t377_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 70;
					break;
                case enumDBFields.t314_idusuario:
                    paramName = "@t314_idusuario";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t303_idnodo:
                    paramName = "@t303_idnodo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.bMostrarTodos:
                    paramName = "@bMostrarTodos";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.sTipoBusq:
                    paramName = "@sTipoBusq";
                    paramType = SqlDbType.Char;
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
