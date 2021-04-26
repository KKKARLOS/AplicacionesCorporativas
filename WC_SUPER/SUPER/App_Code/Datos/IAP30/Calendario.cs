using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Calendario
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class Calendario 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t066_idcal = 1,
            anno =2
        }

        internal Calendario(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene los datos generales de un calendario determinado,
        /// correspondientes a la tabla t066_CALENDARIO, y devuelve una
        /// instancia u objeto del tipo Calendario       
        ///// </summary>
        internal Models.Calendario getCalendario(Int32 idCal, Int32 nAnno)
        {
            Models.Calendario oCalendario = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t066_idcal, idCal),
                    Param(enumDBFields.anno, nAnno)
                };

                dr = cDblib.DataReader("SUP_CALENDARIOS", dbparams);
                if (dr.Read())
                {
                    oCalendario = new Models.Calendario();
                    oCalendario.t066_idcal = Convert.ToInt32(dr["t066_idcal"]);
                    oCalendario.t066_descal = Convert.ToString(dr["t066_descal"]);
                    oCalendario.t066_estado = Convert.ToInt32(dr["t066_estado"]);
                    oCalendario.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    oCalendario.t066_tipocal = Convert.ToString(dr["t066_tipocal"]);
                    oCalendario.t066_semlabL = Convert.ToInt32(dr["t066_semlabL"]);
                    oCalendario.t066_semlabM = Convert.ToInt32(dr["t066_semlabM"]);
                    oCalendario.t066_semlabX = Convert.ToInt32(dr["t066_semlabX"]);
                    oCalendario.t066_semlabJ = Convert.ToInt32(dr["t066_semlabJ"]);
                    oCalendario.t066_semlabV = Convert.ToInt32(dr["t066_semlabV"]);
                    oCalendario.t066_semlabS = Convert.ToInt32(dr["t066_semlabS"]);
                    oCalendario.t066_semlabD = Convert.ToInt32(dr["t066_semlabD"]);
                    oCalendario.t066_obs = Convert.ToString(dr["t066_obs"]);
                }
                return oCalendario;

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
                case enumDBFields.t066_idcal:
                    paramName = "@nIdCal";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.anno:
                    paramName = "@nAnno";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
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
