using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for ReceptoresAvisos
/// </summary>

namespace IB.SUPER.SIC.DAL
{

    internal class ReceptoresAvisos
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t001_idficepi = 1,
            t399_avisopreventa = 2,
        }

        internal ReceptoresAvisos(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        #region funciones publicas

        /// <summary>
        /// Actualiza un ReceptoresAvisos a partir del id
        /// </summary>
        internal int Update(Models.ReceptoresAvisos oReceptoresAvisos)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t001_idficepi, oReceptoresAvisos.t001_idficepi),
                    Param(enumDBFields.t399_avisopreventa, oReceptoresAvisos.t399_avisopreventa)
                };

                return (int)cDblib.Execute("SIC_ReceptoresAvisos_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los ReceptoresAvisos de preventa
        /// </summary>
        internal List<Models.ReceptoresAvisos> Catalogo()
        {
            Models.ReceptoresAvisos oP = null;
            List<Models.ReceptoresAvisos> lst = new List<Models.ReceptoresAvisos>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] {};

                dr = cDblib.DataReader("SIC_ReceptoresAvisos_C", dbparams);
                while (dr.Read())
                {
                    oP = new Models.ReceptoresAvisos();
                    oP.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oP.denProfesional = dr["Profesional"].ToString();
                    oP.t399_avisopreventa = (bool)dr["t399_avisopreventa"];

                    lst.Add(oP);

                }
                return lst;

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
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t399_avisopreventa:
                    paramName = "@t399_avisopreventa";
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
