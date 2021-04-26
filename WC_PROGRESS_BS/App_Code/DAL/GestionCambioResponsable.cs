using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

/// <summary>
/// Summary description for GestionCambioResponsable
/// </summary>

namespace IB.Progress.DAL
{

    internal class GestionCambioResponsable
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t937_estadopeticion = 0,
            t001_apellido1 = 1,
            t001_apellido2 = 2,
            t001_nombre = 3,
            idpeticion = 4,
            idficepidestino= 5,
            idficepifin= 6, 
            idficepiinteresado = 7, 
            

        }

        internal GestionCambioResponsable(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        public GestionCambioResponsable()
        {

            //lo dejo pero de momento no se usa
        }

        #endregion

        #region funciones publicas



        internal List<Models.GestionCambioResponsable> CatalogoCambioResponsable(Nullable<int> estado, string apellido1, string apellido2, string nombre)
        {
            Models.GestionCambioResponsable oProfesional = null;
            List<Models.GestionCambioResponsable> returnList = new List<Models.GestionCambioResponsable>();
            
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(enumDBFields.t937_estadopeticion, (estado==null) ? null : estado.ToString()),                                      
                    Param(enumDBFields.t001_apellido1, apellido1.ToString()),
                    Param(enumDBFields.t001_apellido2, apellido2.ToString()),
                    Param(enumDBFields.t001_nombre, nombre.ToString())
                    
                };
                dr = cDblib.DataReader("PRO_GESTIONARCAMBIORESPONSABLE_GET", dbparams);
                while (dr.Read())
                {
                    oProfesional = new Models.GestionCambioResponsable();
                    if (!Convert.IsDBNull(dr["t937_idpetcambioresp"]))
                        oProfesional.T937_idpetcambioresp = int.Parse(dr["t937_idpetcambioresp"].ToString());
                     if (!Convert.IsDBNull(dr["t001_idficepi_interesado"]))
                        oProfesional.T001_idficepi_interesado = int.Parse(dr["t001_idficepi_interesado"].ToString());
                     if (!Convert.IsDBNull(dr["t001_idficepi_resporigen"]))
                         oProfesional.T001_idficepi_resporigen = int.Parse(dr["t001_idficepi_resporigen"].ToString());                                        
                    if (!Convert.IsDBNull(dr["interesado"]))
                        oProfesional.Interesado = dr["interesado"].ToString();
                    if (!Convert.IsDBNull(dr["t937_estadopeticion"]))
                        oProfesional.T937_estadopeticion = int.Parse(dr["t937_estadopeticion"].ToString());
                    if (!Convert.IsDBNull(dr["t937_fechainipeticion"]))
                        oProfesional.T937_fechainipeticion = DateTime.Parse(dr["t937_fechainipeticion"].ToString());
                    if (!Convert.IsDBNull(dr["t937_fechacambioestado"]))
                        oProfesional.T937_fechacambioestado = DateTime.Parse(dr["t937_fechacambioestado"].ToString());
                    if (!Convert.IsDBNull(dr["t937_fecharechazo"]))
                        oProfesional.T937_fecharechazo = DateTime.Parse(dr["t937_fecharechazo"].ToString());
                    if (!Convert.IsDBNull(dr["resporigen"]))
                        oProfesional.Resporigen = dr["resporigen"].ToString();

                    if (!Convert.IsDBNull(dr["t001_idficepi_respdestino"]))
                        oProfesional.T001_idficepi_respdestino = int.Parse(dr["t001_idficepi_respdestino"].ToString()); 

                    if (!Convert.IsDBNull(dr["respdestino"]))
                        oProfesional.Respdestino = dr["respdestino"].ToString();
                    if (!Convert.IsDBNull(dr["t937_comentario_resporigen"]))
                        oProfesional.T937_comentario_resporigen = dr["t937_comentario_resporigen"].ToString();
                    if (!Convert.IsDBNull(dr["t937_comentario_respdestino"]))
                        oProfesional.T937_comentario_respdestino = dr["t937_comentario_respdestino"].ToString();

                    if (!Convert.IsDBNull(dr["nombreinteresado"]))
                        oProfesional.nombreprofesional = dr["nombreinteresado"].ToString();

                    if (!Convert.IsDBNull(dr["correo_interesado"]))
                        oProfesional.correo_interesado = dr["correo_interesado"].ToString();

                    if (!Convert.IsDBNull(dr["nombre_resporigen"]))
                        oProfesional.nombre_resporigen = dr["nombre_resporigen"].ToString();
                    if (!Convert.IsDBNull(dr["correo_resporigen"]))
                        oProfesional.correo_resporigen = dr["correo_resporigen"].ToString();
                    if (!Convert.IsDBNull(dr["nombreapellidos_interesado"]))
                        oProfesional.nombreapellidos_interesado = dr["nombreapellidos_interesado"].ToString();

                    if (!Convert.IsDBNull(dr["nombre_respdestino"]))
                        oProfesional.nombre_respdestino = dr["nombre_respdestino"].ToString();

                    if (!Convert.IsDBNull(dr["correo_respdestino"]))
                        oProfesional.correo_respdestino = dr["correo_respdestino"].ToString();

                    if (!Convert.IsDBNull(dr["sexo_respdestino"]))
                        oProfesional.sexo_respdestino = dr["sexo_respdestino"].ToString();

                    if (!Convert.IsDBNull(dr["sexo_interesado"]))
                        oProfesional.sexo_interesado = dr["sexo_interesado"].ToString();

                    if (!Convert.IsDBNull(dr["sexo_resporigen"]))
                        oProfesional.sexo_resporigen = dr["sexo_resporigen"].ToString();


                    if (!Convert.IsDBNull(dr["nombreapellidos_respdestino"]))
                        oProfesional.nombreapellidos_respdestino = dr["nombreapellidos_respdestino"].ToString();

                    if (!Convert.IsDBNull(dr["estadebaja_respdestino"]))
                        oProfesional.estadebaja_respdestino = byte.Parse(dr["estadebaja_respdestino"].ToString());
                    
                    

                    returnList.Add(oProfesional);
                }
                return returnList;
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


        internal int GestionAnulacionAsignacion(Nullable<int> idpeticion, int idficepifin, int estadopeticion, Nullable<int> idficepidestino)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
					Param(enumDBFields.idpeticion, idpeticion.ToString())	,
				    Param(enumDBFields.t937_estadopeticion, estadopeticion.ToString()),
                    Param(enumDBFields.idficepifin,  idficepifin.ToString()),
                    Param(enumDBFields.idficepidestino,   (idficepidestino==null)?null:idficepidestino.ToString())
                   
                   
				};

                return (int)cDblib.Execute("PRO_GESTIONARCAMBIORESPONSABLE_UPD", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        internal int CambioEvalprogress(int idficepiInteresado, Nullable<int> idficepiDestino)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.idficepiinteresado, idficepiInteresado.ToString())	,
                    Param(enumDBFields.idficepidestino, idficepiDestino.ToString())
				
				};

                return (int)cDblib.Execute("PRO_CAMBIOEVALPROGRESS_UPD", dbparams);
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

                case enumDBFields.t937_estadopeticion:
                    paramName = "@t937_estadopeticion";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;               

                case enumDBFields.t001_apellido1:
                    paramName = "@t001_apellido1";
                    paramType = SqlDbType.VarChar;
                    paramSize = 25;
                    break;
                case enumDBFields.t001_apellido2:
                    paramName = "@t001_apellido2";
                    paramType = SqlDbType.VarChar;
                    paramSize = 25;
                    break;
                case enumDBFields.t001_nombre:
                    paramName = "@t001_nombre";
                    paramType = SqlDbType.VarChar;
                    paramSize = 20;
                    break;

                case enumDBFields.idpeticion:
                    paramName = "@t937_idpetcambioresp";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.idficepidestino:
                    paramName = "@idficepi_destino";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.idficepifin:
                    paramName = "@idficepifin";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.idficepiinteresado:
                    paramName = "@idficepi_interesado";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;       
            }

            //if (strValue == null) strValue = DBNull.Value;

            dbParam = cDblib.dbParameter(paramName, paramType, paramSize);
            dbParam.Direction = paramDirection;
            if (paramDirection == ParameterDirection.Input | paramDirection == ParameterDirection.InputOutput) dbParam.Value = value;

            return dbParam;

        }

        #endregion

    }

}
