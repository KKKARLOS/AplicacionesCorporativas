using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TipoAsuntoCat
/// </summary>

namespace IB.SUPER.IAP30.DAL
{

    internal class TipoAsuntoCat
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t384_destipo = 1,
            t384_idtipo = 2,
            t384_orden = 3,
            nOrden = 4,
            nAscDesc = 5
        }

        internal TipoAsuntoCat(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los TipoAsunto
        /// </summary>
        internal List<Models.TipoAsuntoCat> Catalogo(Models.TipoAsuntoCat oTipoAsuntoFilter)
        {
            Models.TipoAsuntoCat oTipoAsunto = null;
            List<Models.TipoAsuntoCat> lst = new List<Models.TipoAsuntoCat>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(enumDBFields.t384_destipo, oTipoAsuntoFilter.t384_destipo),
                    Param(enumDBFields.t384_idtipo, oTipoAsuntoFilter.t384_idtipo),
                    Param(enumDBFields.t384_orden, oTipoAsuntoFilter.t384_orden),
                    Param(enumDBFields.nOrden, oTipoAsuntoFilter.nOrden),
                    Param(enumDBFields.nAscDesc, oTipoAsuntoFilter.nAscDesc)
                };

                dr = cDblib.DataReader("SUP_TIPOASUNTO_C", dbparams);
                while (dr.Read())
                {
                    oTipoAsunto = new Models.TipoAsuntoCat();
                    oTipoAsunto.t384_destipo = Convert.ToString(dr["T384_destipo"]);
                    oTipoAsunto.t384_idtipo = Convert.ToInt32(dr["T384_idtipo"]);
                    oTipoAsunto.t384_orden = Convert.ToByte(dr["T384_orden"]);

                    lst.Add(oTipoAsunto);

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
                case enumDBFields.t384_destipo:
                    paramName = "@T384_destipo";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.t384_idtipo:
                    paramName = "@T384_idtipo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t384_orden:
                    paramName = "@T384_orden";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.nOrden:
                    paramName = "@nOrden";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.nAscDesc:
                    paramName = "@nAscDesc";
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
