using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IB.Progress.DAL
{
    /// <summary>
    /// Descripción breve de TramitarSalidas
    /// </summary>
    internal class TramitarSalidas
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            listapersonas = 1,
            t001_idficepi_respdestino = 2,
            t937_comentario_resporigen = 3,
            estado = 4,
            idpeticion = 5,
            nombreevaluadordestino = 6,
            correoevaluadordestino = 7,
            idpeticiones = 8,
            t001_idficepi = 9
        }

        internal TramitarSalidas(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        public TramitarSalidas()
        {

            //lo dejo pero de momento no se usa
        }

        #endregion

        #region funciones publicas


        /// <summary>
        /// Inserta tramitación de cambio de responsable
        /// </summary>
        internal List<string> Insert(string listaProfesionales, int t001_idficepi_respdestino, string t937_comentario_resporigen)
        {

            //Parámetros de salida
            SqlParameter correoevaluadordestino = null, nombreevaluadordestino = null;
            List<string> datos = new List<string>();
            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] 
                {
				    Param(ParameterDirection.Input,enumDBFields.listapersonas, listaProfesionales),
                    Param(ParameterDirection.Input,enumDBFields.t001_idficepi_respdestino, t001_idficepi_respdestino.ToString()),                                    
                    Param(ParameterDirection.Input,enumDBFields.t937_comentario_resporigen, t937_comentario_resporigen) ,
                    correoevaluadordestino = Param(ParameterDirection.Output, enumDBFields.correoevaluadordestino, null),
                    nombreevaluadordestino = Param(ParameterDirection.Output, enumDBFields.nombreevaluadordestino, null)               
				};
                
                object resultado = cDblib.Desc("PRO_TRAMITACIONCAMBIORESPONSABLE_INS", dbparams);
                
                //Devolvemos una lista de strings con el nombre y el correo del evaluador destino.
                datos.Add(correoevaluadordestino.Value.ToString());
                datos.Add(nombreevaluadordestino.Value.ToString());
                return datos;                                
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }


        internal void Update(string idpeticiones, int t001_idficepi)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(ParameterDirection.Input,enumDBFields.t001_idficepi, t001_idficepi),
					Param(ParameterDirection.Input,enumDBFields.idpeticiones, idpeticiones)					
				};

                cDblib.Execute("PRO_ANULARSALIDA_UPD", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal int SolicitarMediacion(int idpeticion)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(ParameterDirection.Input,enumDBFields.idpeticion, idpeticion.ToString())					
				};

                return (int)cDblib.Execute("PRO_SOLICITARMEDIACION_UPD", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region funciones privadas
        private SqlParameter Param(ParameterDirection paramDirection, enumDBFields dbField, object value)
        {
            SqlParameter dbParam = null;
            string paramName = null;
            SqlDbType paramType = default(SqlDbType);
            int paramSize = 0;
            

            switch (dbField)
            {
                case enumDBFields.listapersonas:
                    paramName = "@listapersonas";
                    paramType = SqlDbType.VarChar;                
                    break;

                case enumDBFields.t001_idficepi_respdestino:
                    paramName = "@t001_idficepi_respdestino";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.t937_comentario_resporigen:
                    paramName = "@t937_comentario_resporigen";
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


                case enumDBFields.correoevaluadordestino:
                    paramName = "@correoevaluadordestino";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;

                case enumDBFields.nombreevaluadordestino:
                    paramName = "@nombreevaluadordestino";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;

                case enumDBFields.idpeticiones:
                    paramName = "@listapeticiones";
                    paramType = SqlDbType.VarChar;                
                    break;

                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
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