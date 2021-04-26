using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for EstimacionIAP
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class EstimacionIAP 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t314_idusuario = 1,
			t332_idtarea = 2,
			t336_ete = 3,
			t336_ffe = 4,
			t336_comentario = 5,
			t336_completado = 6
        }

        internal EstimacionIAP(sqldblib.SqlServerSP extcDblib)
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
        /// Actualiza un EstimacionIAP a partir del id
        /// </summary>
        internal int Update(Models.EstimacionIAP oEstimacionIAP)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[6] {
                    Param(enumDBFields.t314_idusuario, oEstimacionIAP.t314_idusuario),
                    Param(enumDBFields.t332_idtarea, oEstimacionIAP.t332_idtarea),
                    Param(enumDBFields.t336_ete, oEstimacionIAP.t336_ete),
                    Param(enumDBFields.t336_ffe, oEstimacionIAP.t336_ffe),
                    Param(enumDBFields.t336_comentario, oEstimacionIAP.t336_comentario),
                    Param(enumDBFields.t336_completado, oEstimacionIAP.t336_completado)
                };

                return (int)cDblib.Execute("SUP_ESTIMACIONIAPU", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
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
				case enumDBFields.t314_idusuario:
					paramName = "@t314_idusuario";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t332_idtarea:
					paramName = "@t332_idtarea";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t336_ete:
					paramName = "@t336_ete";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.t336_ffe:
					paramName = "@t336_ffe";
					paramType = SqlDbType.DateTime;
					paramSize = 4;
					break;
				case enumDBFields.t336_comentario:
					paramName = "@t336_comentario";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t336_completado:
					paramName = "@t336_completado";
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
