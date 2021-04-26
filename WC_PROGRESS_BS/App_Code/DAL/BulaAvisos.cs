using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using IB.Progress.Models;
using IB.Progress.Shared;



namespace IB.Progress.DAL
{

    internal class BulaAvisos
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

        internal BulaAvisos(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        public BulaAvisos()
        {

            //lo dejo pero de momento no se usa
        }

        #endregion

        #region funciones publicas

       
        /// <summary>
        /// Obtiene todos los profesionales
        /// </summary>
        internal Models.ComunidadProgress Catalogo()
        {
            Models.ComunidadProgress oComunidad = new Models.ComunidadProgress();
            Models.Personas oProfesionales = null;
            Models.CR oCR = null;
            
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[0];

                dr = cDblib.DataReader("PRO_BULAAVISOSPROGRESS_CAT", dbparams);
                while (dr.Read())
                {
                    oProfesionales = new Models.Personas();

                    if (!Convert.IsDBNull(dr["t001_idficepi"]))
                        oProfesionales.T001_idficepi = short.Parse(dr["t001_idficepi"].ToString());

                    if (!Convert.IsDBNull(dr["profesional"]))
                        oProfesionales.Profesional = dr["profesional"].ToString();


                    oComunidad.SelectPersonas.Add(oProfesionales);

                }


                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        oProfesionales = new Models.Personas();

                        if (!Convert.IsDBNull(dr["t001_idficepi"]))
                            oProfesionales.T001_idficepi = short.Parse(dr["t001_idficepi"].ToString());

                        if (!Convert.IsDBNull(dr["profesional"]))
                            oProfesionales.Profesional = dr["profesional"].ToString();


                        oComunidad.SelectPersonas2.Add(oProfesionales);
                    }

                }


                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        oCR = new Models.CR();

                        if (!Convert.IsDBNull(dr["t303_idnodo"]))
                            oCR.T303_idnodo = short.Parse(dr["t303_idnodo"].ToString());

                        if (!Convert.IsDBNull(dr["t303_denominacion"]))
                            oCR.T303_denominacion = dr["t303_denominacion"].ToString();


                        oComunidad.SelectCR.Add(oCR);
                    }

                }

                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        oCR = new Models.CR();

                        if (!Convert.IsDBNull(dr["t303_idnodo"]))
                            oCR.T303_idnodo = short.Parse(dr["t303_idnodo"].ToString());

                        if (!Convert.IsDBNull(dr["t303_denominacion"]))
                            oCR.T303_denominacion = dr["t303_denominacion"].ToString();


                        oComunidad.SelectCR2.Add(oCR);
                    }

                }

             
                return oComunidad;

            }
            catch (Exception ex)
            {
                throw new IBException(102, "Ocurrió un error obteniendo los datos de bula de avisos.", ex);
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

                return (int)cDblib.Execute("PRO_BULAAVISOSPROGRESS_UPD", dbparams);
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
