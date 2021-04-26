using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace IB.Progress.DAL
{
    /// <summary>
    /// Descripción breve de GestionarIncorporaciones
    /// </summary>
    internal class GestionarIncorporaciones
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            idpeticion = 1,
            t937_comentario_respdestino = 2,
            estado = 3,
            idficepi_fin = 4,
            t001_idficepi = 5,
            listapeticiones = 6
        }

        internal GestionarIncorporaciones(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        public GestionarIncorporaciones()
        {

            //lo dejo pero de momento no se usa
        }

        #endregion

        #region funciones publicas




        internal int RechazarIncorporacion(string listapeticiones, string MotivoRechazo)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.listapeticiones, listapeticiones.ToString())	,
                    Param(enumDBFields.t937_comentario_respdestino, MotivoRechazo)
				
				};

                return (int)cDblib.Execute("PRO_RECHAZAR_INCORPORACION_UPD", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void AceptarIncorporacion(int t001_idficepi, string listapeticiones )
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.t001_idficepi, t001_idficepi),
                    Param(enumDBFields.listapeticiones, listapeticiones.ToString())
				
				};

                cDblib.Execute("PRO_ACEPTAR_INCORPORACION_UPD", dbparams);
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
                
                case enumDBFields.t937_comentario_respdestino:
                    paramName = "@t937_comentario_respdestino";
                    paramType = SqlDbType.VarChar;
                    paramSize = 750;
                    break;

                case enumDBFields.estado:
                    paramName = "@estado";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.idpeticion:
                    paramName = "@t937_idpetcambioresp";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.idficepi_fin:
                    paramName = "@t001_idficepi_fin";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;


                case enumDBFields.listapeticiones:
                    paramName = "@listapeticiones";
                    paramType = SqlDbType.VarChar;                
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