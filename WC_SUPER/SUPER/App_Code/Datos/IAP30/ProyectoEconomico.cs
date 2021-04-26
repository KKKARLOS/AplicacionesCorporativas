using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ProyectoEconomico
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ProyectoEconomico 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;		
		
        private enum enumDBFields : byte
        {
            idnodo = 1,
            sEstado = 2,
            sCategoria = 3,
            idcliente = 4,
            idResponsable = 5,
            numPE = 6,
            sDesPE = 7,
            sTipoBusqueda = 8,
            sCualidad = 9,
            nContrato = 10,
            nHorizontal = 11,
            nCNP = 12,
            nCSN1P = 13,
            nCSN2P = 14,
            nCSN3P = 15,
            nCSN4P = 16,
            nUsuario = 17,
            bMostrarBitacoricos = 18,
            nNaturaleza = 19,
            nModeloContratacion = 20,
            bMostrarJ = 21,
            bSoloFacturables = 22,
            t301_idproyecto = 23
        }

        internal ProyectoEconomico(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene un NodoPT a partir del id
        /// </summary>
        internal Models.ProyectoEconomico Select(int t301_idproyecto)
        {
            Models.ProyectoEconomico oEstadoPE = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t301_idproyecto, t301_idproyecto)
                };

                dr = cDblib.DataReader("SUP_PROYECTO_ESTADO", dbparams);
                if (dr.Read())
                {
                    oEstadoPE = new Models.ProyectoEconomico();
                    oEstadoPE.t301_estado = Convert.ToString(dr["t301_estado"]);
                }
                return oEstadoPE;
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
        /// Obtiene los proyectos económicos a los que se tiene acceso en el módulo técnico
        /// </summary>
        internal List<Models.ProyectoEconomico> CatalogoModuloTec(Nullable<int> idNodo, string sEstado, string sCategoria,
                                                        Nullable<int> idCliente, Nullable<int> idResponsable, Nullable<int> numPE, string sDesPE,
                                                        string sTipoBusqueda, string sCualidad, Nullable<int> nContrato, Nullable<int> nHorizontal,
                                                        Nullable<int> nCNP, Nullable<int> nCSN1P, Nullable<int> nCSN2P, Nullable<int> nCSN3P,
                                                        Nullable<int> nCSN4P, int nUsuario, bool bMostrarBitacoricos, Nullable<int> nNaturaleza,
                                                        Nullable<int> nModeloContratacion)
        {
            Models.ProyectoEconomico oProyectoEconomico = null;
            List<Models.ProyectoEconomico> lst = new List<Models.ProyectoEconomico>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[20] {
                    Param(enumDBFields.idnodo, idNodo),
                    Param(enumDBFields.sEstado, sEstado),
                    Param(enumDBFields.sCategoria, sCategoria),
                    Param(enumDBFields.idcliente, idCliente),
                    Param(enumDBFields.idResponsable, idResponsable),
                    Param(enumDBFields.numPE, numPE),
                    Param(enumDBFields.sDesPE, sDesPE),
                    Param(enumDBFields.sTipoBusqueda, sTipoBusqueda),
                    Param(enumDBFields.sCualidad, sCualidad),
                    Param(enumDBFields.nContrato, nContrato),
                    Param(enumDBFields.nHorizontal, nHorizontal),
                    Param(enumDBFields.nCNP, nCNP),
                    Param(enumDBFields.nCSN1P, nCSN1P),
                    Param(enumDBFields.nCSN2P, nCSN2P),
                    Param(enumDBFields.nCSN3P, nCSN3P),
                    Param(enumDBFields.nCSN4P, nCSN4P),
                    Param(enumDBFields.nUsuario, nUsuario),
                    Param(enumDBFields.bMostrarBitacoricos, bMostrarBitacoricos),
                    Param(enumDBFields.nNaturaleza, nNaturaleza),
                    Param(enumDBFields.nModeloContratacion, nModeloContratacion)
                };

                dr = cDblib.DataReader("SUP_GETPROYECTOS_MODULOTEC", dbparams);
                while (dr.Read())
                {
                    oProyectoEconomico = new Models.ProyectoEconomico();
                    oProyectoEconomico.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oProyectoEconomico.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oProyectoEconomico.t301_denominacion = Convert.ToString(dr["t301_denominacion"]);
                    oProyectoEconomico.t301_estado = Convert.ToString(dr["t301_estado"]);
                    oProyectoEconomico.t302_idcliente_proyecto = Convert.ToInt32(dr["t302_idcliente_proyecto"]);
                    oProyectoEconomico.t302_denominacion = Convert.ToString(dr["t302_denominacion"]);
                    oProyectoEconomico.t301_categoria = Convert.ToString(dr["t301_categoria"]);
                    oProyectoEconomico.t305_cualidad = Convert.ToString(dr["t305_cualidad"]);
                    oProyectoEconomico.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    oProyectoEconomico.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                    oProyectoEconomico.modo_lectura = Convert.ToInt32(dr["modo_lectura"]);
                    oProyectoEconomico.rtpt = Convert.ToInt32(dr["rtpt"]);
                    oProyectoEconomico.desmotivo = Convert.ToString(dr["desmotivo"]);
                    if (!Convert.IsDBNull(dr["Responsable"]))
                        oProyectoEconomico.responsable = Convert.ToString(dr["Responsable"]);
                    oProyectoEconomico.t422_idmoneda_proyecto = Convert.ToString(dr["t422_idmoneda_proyecto"]);

                    lst.Add(oProyectoEconomico);

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
        /// Obtiene los proyectos económicos para el administrador
        /// </summary>
        internal List<Models.ProyectoEconomico> CatalogoAdmin(Nullable<int> idNodo, string sEstado, string sCategoria,
                                                        Nullable<int> idCliente, Nullable<int> idResponsable, Nullable<int> numPE, string sDesPE,
                                                        string sTipoBusqueda, string sCualidad, Nullable<int> nContrato, Nullable<int> nHorizontal,
                                                        Nullable<int> nCNP, Nullable<int> nCSN1P, Nullable<int> nCSN2P, Nullable<int> nCSN3P,
                                                        Nullable<int> nCSN4P, bool bMostrarJ, bool bSoloFacturables, Nullable<int> nNaturaleza, 
                                                        Nullable<int> nModeloContratacion)
        {
            Models.ProyectoEconomico oProyectoEconomico = null;
            List<Models.ProyectoEconomico> lst = new List<Models.ProyectoEconomico>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[20] {
                    Param(enumDBFields.idnodo, idNodo),
                    Param(enumDBFields.sEstado, sEstado),
                    Param(enumDBFields.sCategoria, sCategoria),
                    Param(enumDBFields.idcliente, idCliente),
                    Param(enumDBFields.idResponsable, idResponsable),
                    Param(enumDBFields.numPE, numPE),
                    Param(enumDBFields.sDesPE, sDesPE),
                    Param(enumDBFields.sTipoBusqueda, sTipoBusqueda),
                    Param(enumDBFields.sCualidad, sCualidad),
                    Param(enumDBFields.nContrato, nContrato),
                    Param(enumDBFields.nHorizontal, nHorizontal),
                    Param(enumDBFields.nCNP, nCNP),
                    Param(enumDBFields.nCSN1P, nCSN1P),
                    Param(enumDBFields.nCSN2P, nCSN2P),
                    Param(enumDBFields.nCSN3P, nCSN3P),
                    Param(enumDBFields.nCSN4P, nCSN4P),
                    Param(enumDBFields.bMostrarJ, bMostrarJ),
                    Param(enumDBFields.bSoloFacturables, bSoloFacturables),
                    Param(enumDBFields.nNaturaleza, nNaturaleza),
                    Param(enumDBFields.nModeloContratacion, nModeloContratacion)
                };

                dr = cDblib.DataReader("SUP_GETPROYECTOS_ADMIN", dbparams);
                while (dr.Read())
                {
                    oProyectoEconomico = new Models.ProyectoEconomico();
                    oProyectoEconomico.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oProyectoEconomico.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oProyectoEconomico.t301_denominacion = Convert.ToString(dr["t301_denominacion"]);
                    oProyectoEconomico.t301_estado = Convert.ToString(dr["t301_estado"]);
                    oProyectoEconomico.t302_idcliente_proyecto = Convert.ToInt32(dr["t302_idcliente_proyecto"]);
                    oProyectoEconomico.t302_denominacion = Convert.ToString(dr["t302_denominacion"]);
                    oProyectoEconomico.t301_categoria = Convert.ToString(dr["t301_categoria"]);
                    oProyectoEconomico.t305_cualidad = Convert.ToString(dr["t305_cualidad"]);
                    oProyectoEconomico.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    oProyectoEconomico.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                    oProyectoEconomico.t303_ultcierreeco = Convert.ToInt32(dr["t303_ultcierreeco"]);
                    oProyectoEconomico.modo_lectura = Convert.ToInt32(dr["modo_lectura"]);
                    oProyectoEconomico.rtpt = Convert.ToInt32(dr["rtpt"]);
                    oProyectoEconomico.desmotivo = Convert.ToString(dr["desmotivo"]);
                    if (!Convert.IsDBNull(dr["Responsable"]))
                        oProyectoEconomico.responsable = Convert.ToString(dr["Responsable"]);
                    oProyectoEconomico.t422_idmoneda_proyecto = Convert.ToString(dr["t422_idmoneda_proyecto"]);

                    lst.Add(oProyectoEconomico);

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
                case enumDBFields.idnodo:
                    paramName = "@idnodo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.sEstado:
                    paramName = "@sEstado";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.sCategoria:
                    paramName = "@sCategoria";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.idcliente:
					paramName = "@idcliente";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                 case enumDBFields.idResponsable:
					paramName = "@idResponsable";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.numPE:
					paramName = "@numPE";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.t301_idproyecto:
                    paramName = "@t301_idproyecto";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;                                        
                case enumDBFields.sDesPE:
					paramName = "@sDesPE";
					paramType = SqlDbType.VarChar;
					paramSize = 70;
					break;
                case enumDBFields.sTipoBusqueda:
                    paramName = "@sTipoBusqueda";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.sCualidad:
					paramName = "@sCualidad";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
                case enumDBFields.nContrato:
                    paramName = "@nContrato";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.nHorizontal:
                    paramName = "@nHorizontal";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.nCNP:
                    paramName = "@nCNP";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.nCSN1P:
                    paramName = "@nCSN1P";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.nCSN2P:
                    paramName = "@nCSN2P";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.nCSN3P:
                    paramName = "@nCSN3P";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.nCSN4P:
                    paramName = "@nCSN4P";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.nUsuario:
                    paramName = "@nUsuario";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.bMostrarBitacoricos:
                    paramName = "@bMostrarBitacoricos";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.nNaturaleza:
                    paramName = "@nNaturaleza";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.nModeloContratacion:
                    paramName = "@nModeloContratacion";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.bMostrarJ:
                    paramName = "@bMostrarJ";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.bSoloFacturables:
                    paramName = "@bSoloFacturables";
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
