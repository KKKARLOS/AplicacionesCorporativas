using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.SIC.Models;

namespace IB.SUPER.SIC.DAL
{
    internal class Usuario
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t314_loginhermes = 1,
            t001_idficepi = 2,
            rowid = 3,
            itemorigen = 4

        }

        internal Usuario(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        internal Models.UsuarioCRM Autenticar(string login_hermes)
        {

            Models.UsuarioCRM o = new Models.UsuarioCRM();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t314_loginhermes, login_hermes)
                };

                dr = cDblib.DataReader("SIC_LOGIN_CRM", dbparams);
                if (dr.Read())
                {
                    if (!Convert.IsDBNull(dr["t001_IDFICEPI"]))
                    {
                        o.IDFICEPI_PC_ACTUAL = Convert.ToInt32(dr["t001_IDFICEPI"]);
                        o.IDFICEPI_ENTRADA = o.IDFICEPI_PC_ACTUAL;
                    }
                    if (!Convert.IsDBNull(dr["APELLIDO1"]))
                        o.APELLIDO1 = Convert.ToString(dr["APELLIDO1"]);
                    if (!Convert.IsDBNull(dr["APELLIDO2"]))
                        o.APELLIDO2 = Convert.ToString(dr["APELLIDO2"]);
                    if (!Convert.IsDBNull(dr["NOMBRE"]))
                        o.NOMBRE = Convert.ToString(dr["NOMBRE"]);
                    if (!Convert.IsDBNull(dr["t001_codred"]))
                        o.IDRED = Convert.ToString(dr["t001_codred"]);
                    o.DES_EMPLEADO_ENTRADA = o.APELLIDO1 + " " + o.APELLIDO2 + ", " + o.NOMBRE;
                }
                return o;

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

        public int ObtenerIdItemPorRowId(string rowid, string itemorigen)
        {

            SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.rowid, rowid),
                    Param(enumDBFields.itemorigen, itemorigen),
                };

            return int.Parse(cDblib.Desc("SIC_BUSCA_IDITEM_POR_ROWID", dbparams));


        }

        internal string obtenerNombreComercial(string t314_loginhermes)
        {

            SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t314_loginhermes, t314_loginhermes)
                };

            return cDblib.Desc("SIC_NOMCOMERCIAL_S", dbparams);

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
                case enumDBFields.t314_loginhermes:
                    paramName = "@t314_loginhermes";
                    paramType = SqlDbType.VarChar;
                    paramSize = 25;
                    break;

                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.itemorigen:
                    paramName = "@tipo_item";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.rowid:
                    paramName = "@rowid";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
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