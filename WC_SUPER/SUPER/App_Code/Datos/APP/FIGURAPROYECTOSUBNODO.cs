using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.APP.Models;

/// <summary>
/// Summary description for FIGURAPROYECTOSUBNODO
/// </summary>

namespace IB.SUPER.APP.DAL
{

    internal class FIGURAPROYECTOSUBNODO
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t305_idproyectosubnodo = 1,
            t314_idusuario = 2,
            t310_figura = 3
        }

        internal FIGURAPROYECTOSUBNODO(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un FIGURAPROYECTOSUBNODO
        /// </summary>
        internal int Insert(Models.FIGURAPROYECTOSUBNODO oFIGURAPROYECTOSUBNODO)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t305_idproyectosubnodo, oFIGURAPROYECTOSUBNODO.t305_idproyectosubnodo),
                    Param(enumDBFields.t314_idusuario, oFIGURAPROYECTOSUBNODO.t314_idusuario),
                    Param(enumDBFields.t310_figura, oFIGURAPROYECTOSUBNODO.t310_figura)
                };

                return (int)cDblib.Execute("SUP_FIGURAPROYECTOSUBNODO_I", dbparams);
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
                case enumDBFields.t305_idproyectosubnodo:
                    paramName = "@t305_idproyectosubnodo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t314_idusuario:
                    paramName = "@t314_idusuario";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t310_figura:
                    paramName = "@t310_figura";
                    paramType = SqlDbType.Char;
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
