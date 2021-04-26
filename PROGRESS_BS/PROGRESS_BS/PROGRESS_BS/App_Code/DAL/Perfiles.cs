using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using IB.Progress.Models;
using IB.Progress.Shared;

/// <summary>
/// Summary description for ROLIB
/// </summary>

namespace IB.Progress.DAL
{

    internal class Perfiles
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t001_idficepi = 1,
            profesional = 2,
            lista = 3,
            contenedor = 4

        }

        internal Perfiles(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        public Perfiles()
        {

            //lo dejo pero de momento no se usa
        }

        #endregion

        #region funciones publicas

       /// <summary>
       /// Obtiene el catálogo de la pantalla de administración "Perfiles"
       /// </summary>
       /// <returns></returns>
        internal Models.Perfiles Catalogo()
        {
            
            Models.Perfiles oPerfiles = new Models.Perfiles();            
            Models.PersonasPerfiles oPersonasPerfiles = null;
            
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[0];

                dr = cDblib.DataReader("PRO_FIGURASPROGRESS_CAT", dbparams);

                //Select 1
                while (dr.Read())
                {
                    oPersonasPerfiles = new Models.PersonasPerfiles();

                    if (!Convert.IsDBNull(dr["t001_idficepi"]))
                        oPersonasPerfiles.T001_idficepi = short.Parse(dr["t001_idficepi"].ToString());

                    if (!Convert.IsDBNull(dr["profesional"]))
                        oPersonasPerfiles.Profesional = dr["profesional"].ToString();


                    oPerfiles.SelectPersonas.Add(oPersonasPerfiles);

                }

                //Select 2
                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        oPersonasPerfiles = new Models.PersonasPerfiles();

                        if (!Convert.IsDBNull(dr["t001_idficepi"]))
                            oPersonasPerfiles.T001_idficepi = short.Parse(dr["t001_idficepi"].ToString());

                        if (!Convert.IsDBNull(dr["profesional"]))
                            oPersonasPerfiles.Profesional = dr["profesional"].ToString();

                        if (!Convert.IsDBNull(dr["t939_figura"]))
                            oPersonasPerfiles.T939_figura = dr["t939_figura"].ToString();

                        oPerfiles.SelectPersonas2.Add(oPersonasPerfiles);
                    }

                }

                //Select 3
                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        oPersonasPerfiles = new Models.PersonasPerfiles();

                        if (!Convert.IsDBNull(dr["t001_idficepi"]))
                            oPersonasPerfiles.T001_idficepi = short.Parse(dr["t001_idficepi"].ToString());

                        if (!Convert.IsDBNull(dr["profesional"]))
                            oPersonasPerfiles.Profesional = dr["profesional"].ToString();

                        

                        oPerfiles.SelectPersonas3.Add(oPersonasPerfiles);
                    }

                }


                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        oPersonasPerfiles = new Models.PersonasPerfiles();

                        if (!Convert.IsDBNull(dr["t001_idficepi"]))
                            oPersonasPerfiles.T001_idficepi = short.Parse(dr["t001_idficepi"].ToString());

                        if (!Convert.IsDBNull(dr["profesional"]))
                            oPersonasPerfiles.Profesional = dr["profesional"].ToString();

                      
                        oPerfiles.SelectPersonas4.Add(oPersonasPerfiles);
                    }

                }

 
                return oPerfiles;

            }
            catch (Exception ex)
            {
                throw new IBException(102, "Ocurrió un error obteniendo los datos de los perfiles.", ex);
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



        internal int Update(int contenedor, string lista)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.contenedor, contenedor.ToString()),
                    Param(enumDBFields.lista, lista.ToString())	
				
				};

                return (int)cDblib.Execute("PRO_FIGURASPROGRESS_UPD", dbparams);
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
                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profesional:
                    paramName = "@profesional";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;

                case enumDBFields.lista:
                    paramName = "@lista";
                    paramType = SqlDbType.VarChar;
                    break;

                case enumDBFields.contenedor:
                    paramName = "@contenedor";
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
