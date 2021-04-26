using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for NodoProyUsu
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class NodoProyUsu 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			nUsuario = 1,
            bMostrarBitacoricos = 2,
            bSoloActivos = 3

        }

        internal NodoProyUsu(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los NodoProyUsu
        /// </summary>
        internal List<Models.NodoProyUsu> Catalogo(int nUsuario, bool bMostrarBitacoricos, bool bSoloActivos)
		{
			Models.NodoProyUsu oNodoProyUsu = null;
            List<Models.NodoProyUsu> lst = new List<Models.NodoProyUsu>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.nUsuario, nUsuario),
					Param(enumDBFields.bMostrarBitacoricos, bMostrarBitacoricos),
					Param(enumDBFields.bSoloActivos, bSoloActivos)
                };

				dr = cDblib.DataReader("SUP_GETVISION_NODOS_PROY_USUARIO_TEC", dbparams);
				while (dr.Read())
				{
					oNodoProyUsu = new Models.NodoProyUsu();
					oNodoProyUsu.IDENTIFICADOR=Convert.ToInt32(dr["IDENTIFICADOR"]);
					oNodoProyUsu.DENOMINACION=Convert.ToString(dr["DENOMINACION"]);
					oNodoProyUsu.t303_ultcierreeco=Convert.ToInt32(dr["t303_ultcierreeco"]);
					oNodoProyUsu.ORDEN=Convert.ToInt32(dr["ORDEN"]);
					oNodoProyUsu.SN4=Convert.ToInt32(dr["SN4"]);
					oNodoProyUsu.SN3=Convert.ToInt32(dr["SN3"]);
					oNodoProyUsu.SN2=Convert.ToInt32(dr["SN2"]);
					oNodoProyUsu.SN1=Convert.ToInt32(dr["SN1"]);
					oNodoProyUsu.NODO=Convert.ToInt32(dr["NODO"]);
					oNodoProyUsu.DES_SN4=Convert.ToString(dr["DES_SN4"]);
					oNodoProyUsu.DES_SN3=Convert.ToString(dr["DES_SN3"]);
					oNodoProyUsu.DES_SN2=Convert.ToString(dr["DES_SN2"]);
					oNodoProyUsu.DES_SN1=Convert.ToString(dr["DES_SN1"]);
					oNodoProyUsu.DES_NODO=Convert.ToString(dr["DES_NODO"]);
					oNodoProyUsu.t303_denominacion_CNP=Convert.ToString(dr["t303_denominacion_CNP"]);
					oNodoProyUsu.t303_obligatorio_CNP=Convert.ToInt32(dr["t303_obligatorio_CNP"]);
					oNodoProyUsu.t391_denominacion_CSN1P=Convert.ToString(dr["t391_denominacion_CSN1P"]);
					oNodoProyUsu.t391_obligatorio_CSN1P=Convert.ToInt32(dr["t391_obligatorio_CSN1P"]);
					oNodoProyUsu.t392_denominacion_CSN2P=Convert.ToString(dr["t392_denominacion_CSN2P"]);
					oNodoProyUsu.t392_obligatorio_CSN2P=Convert.ToInt32(dr["t392_obligatorio_CSN2P"]);
					oNodoProyUsu.t393_denominacion_CSN3P=Convert.ToString(dr["t393_denominacion_CSN3P"]);
					oNodoProyUsu.t393_obligatorio_CSN3P=Convert.ToInt32(dr["t393_obligatorio_CSN3P"]);
					oNodoProyUsu.t394_denominacion_CSN4P=Convert.ToString(dr["t394_denominacion_CSN4P"]);
					oNodoProyUsu.t394_obligatorio_CSN4P=Convert.ToInt32(dr["t394_obligatorio_CSN4P"]);

                    lst.Add(oNodoProyUsu);

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
                case enumDBFields.bSoloActivos:
                    paramName = "@bSoloActivos";
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
