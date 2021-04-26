using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DesgloseRol
/// </summary>

namespace IB.Progress.DAL
{

    internal class DesgloseRol
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t001_idficepi = 1,
            parentesco = 2
           
        }

        internal DesgloseRol(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        public DesgloseRol()
        {

            //lo dejo pero de momento no se usa
        }

        #endregion

        #region funciones publicas



        internal List<Models.DesgloseRol> catalogoDesgloseRol(int t001_idficepi, int parentesco)
        {
            Models.DesgloseRol oDesgloseRol = null;
            List<Models.DesgloseRol> returnList = new List<Models.DesgloseRol>();
            
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t001_idficepi, t001_idficepi.ToString()),
                    Param(enumDBFields.parentesco, parentesco.ToString())                   
                };
                dr = cDblib.DataReader("PRO_DESGLOSEROLESDESCENDIENTES", dbparams);
                while (dr.Read())
                {
                    oDesgloseRol = new Models.DesgloseRol();
                    if (!Convert.IsDBNull(dr["profesional"]))
                        oDesgloseRol.Profesional = dr["profesional"].ToString();

                    if (!Convert.IsDBNull(dr["t004_idrol"]))
                        oDesgloseRol.t004_idrol_actual = byte.Parse(dr["t004_idrol"].ToString());

                    if (!Convert.IsDBNull(dr["tipo"]))
                        oDesgloseRol.Tipo = dr["tipo"].ToString();

                    if (!Convert.IsDBNull(dr["t004_desrol"]))
                        oDesgloseRol.DesRol = dr["t004_desrol"].ToString();

                    if (!Convert.IsDBNull(dr["numero"]))
                        oDesgloseRol.Parentesco = int.Parse(dr["numero"].ToString());

                    returnList.Add(oDesgloseRol);
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
                    paramType = SqlDbType.SmallInt;
                    paramSize = 2;
                    break;

                case enumDBFields.parentesco:
                    paramName = "@parentesco";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 2;
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
