using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace IB.SUPER.DAL
{
    /// <summary>
    /// Descripción breve de bsUsuariosAvisos
    /// </summary>
    internal class bsUsuariosAvisos
    {

        #region variables privadas y constructor
        private IB.sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t314_idusuario = 1,
            t448_idaviso = 2
           
        }

        internal bsUsuariosAvisos(IB.sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        public bsUsuariosAvisos()
        {
            
        }

        /// <summary>
        /// Selecciona los registros de la tabla T449_USUARIOAVISOS
        /// </summary>
        /// <param name="t314_idusuario"></param>
        /// <returns></returns>
        internal List<IB.SUPER.Models.bsUsuariosAvisos> Select(int t314_idusuario)
        {
            IB.SUPER.Models.bsUsuariosAvisos oUsuariosAvisos= null;
            List<Models.bsUsuariosAvisos> returnlist = new List<Models.bsUsuariosAvisos>();
            
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                        Param(enumDBFields.t314_idusuario, t314_idusuario)
                    };

                dr = cDblib.DataReader("SUP_USUARIOAVISOS_SByT314_idusuario", dbparams);

                int contador = 0;

                while (dr.Read())
                {
                    contador++;
                    oUsuariosAvisos = new IB.SUPER.Models.bsUsuariosAvisos();

                    if (!Convert.IsDBNull(dr["t448_idaviso"]))
                        oUsuariosAvisos.t448_idaviso = Convert.ToInt32(dr["t448_idaviso"]);
                    if (!Convert.IsDBNull(dr["t448_titulo"]))
                        oUsuariosAvisos.t448_titulo = dr["t448_titulo"].ToString();
                    if (!Convert.IsDBNull(dr["t448_texto"]))
                        oUsuariosAvisos.t448_texto = dr["t448_texto"].ToString();
                    if (!Convert.IsDBNull(dr["t448_IAP"]))
                        oUsuariosAvisos.t448_IAP = bool.Parse(dr["t448_IAP"].ToString());
                    if (!Convert.IsDBNull(dr["t448_PGE"]))
                        oUsuariosAvisos.t448_PGE = bool.Parse(dr["t448_PGE"].ToString());
                    if (!Convert.IsDBNull(dr["t448_PST"]))
                        oUsuariosAvisos.t448_PST = bool.Parse(dr["t448_PST"].ToString());

                    oUsuariosAvisos.numAvisos = contador;
                    returnlist.Add(oUsuariosAvisos);
                
                }
                return returnlist;

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


        internal int CountByUsuario(Int32 t314_idusuario)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {					
                    Param(enumDBFields.t314_idusuario, t314_idusuario)
				};

                return (int)cDblib.Execute("SUP_USUARIOAVISOS_CountByT314_idusuario", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un un aviso de un usuario a partir del id
        /// </summary>
        internal int Delete(Int32 t448_idaviso, Int32 t314_idusuario)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.t448_idaviso, t448_idaviso),
                    Param(enumDBFields.t314_idusuario, t314_idusuario),
				};

                return (int)cDblib.Execute("SUP_USUARIOAVISOS_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

                case enumDBFields.t448_idaviso:
                    paramName = "@t448_idaviso";
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