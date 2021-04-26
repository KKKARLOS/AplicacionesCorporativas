using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.APP.Models;

/// <summary>
/// Summary description for PRIOALERTAS
/// </summary>

namespace IB.SUPER.APP.DAL
{

    internal class PRIOALERTAS
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t820_idalerta_1 = 1,
            t820_idalerta_2 = 2,
            t820_idalerta_g = 3
        }

        internal PRIOALERTAS(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un PRIOALERTAS
        /// </summary>
        internal int Insert(Models.PRIOALERTAS oPRIOALERTAS)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t820_idalerta_1, oPRIOALERTAS.t820_idalerta_1),
                    Param(enumDBFields.t820_idalerta_2, oPRIOALERTAS.t820_idalerta_2),
                    Param(enumDBFields.t820_idalerta_g, oPRIOALERTAS.t820_idalerta_g)
                };

                return (int)cDblib.Execute("SUP_PRIOALERTAS_INS", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Actualiza un PRIOALERTAS a partir del id
        /// </summary>
        internal int Update(Models.PRIOALERTAS oPRIOALERTAS)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t820_idalerta_1, oPRIOALERTAS.t820_idalerta_1),
                    Param(enumDBFields.t820_idalerta_2, oPRIOALERTAS.t820_idalerta_2),
                    Param(enumDBFields.t820_idalerta_g, oPRIOALERTAS.t820_idalerta_g)
                };

                return (int)cDblib.Execute("SUP_PRIOALERTAS_UPD", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Obtiene todos los PRIOALERTAS
        /// </summary>
        internal List<Models.PRIOALERTAS> Catalogo()
        {
            Models.PRIOALERTAS oPRIOALERTAS = null;
            List<Models.PRIOALERTAS> lst = new List<Models.PRIOALERTAS>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] {};

                dr = cDblib.DataReader("SUP_PRIOALERTAS_C", dbparams);
                while (dr.Read())
                {
                    oPRIOALERTAS = new Models.PRIOALERTAS();

                    oPRIOALERTAS.t820_idalerta_1 = Convert.ToByte(dr["t820_idalerta_1"]);
                    oPRIOALERTAS.t820_idalerta_2 = Convert.ToByte(dr["t820_idalerta_2"]);
                    oPRIOALERTAS.t820_idalerta_g = Convert.ToByte(dr["t820_idalerta_g"]);

                    oPRIOALERTAS.denAlert1 = dr["denAlert1"].ToString();
                    oPRIOALERTAS.denAlert2 = dr["denAlert2"].ToString();
                    oPRIOALERTAS.denAlertG = dr["denAlertG"].ToString();


                    oPRIOALERTAS.grupo1 = Convert.ToByte(dr["grupo1"]);
                    oPRIOALERTAS.grupo2 = Convert.ToByte(dr["grupo2"]);
                    oPRIOALERTAS.grupoG = Convert.ToByte(dr["grupoG"]);

                    oPRIOALERTAS.denGrupo1 = dr["denGrupo1"].ToString();

                    lst.Add(oPRIOALERTAS);

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
                case enumDBFields.t820_idalerta_1:
                    paramName = "@t820_idalerta_1";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.t820_idalerta_2:
                    paramName = "@t820_idalerta_2";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.t820_idalerta_g:
                    paramName = "@t820_idalerta_g";
                    paramType = SqlDbType.TinyInt;
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
