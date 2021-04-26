using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using IB.SUPER.SIC.Models;
using System.Collections;

/// <summary>
/// Descripción breve de ProyectoTecnico
/// </summary>
namespace IB.SUPER.SIC.DAL
{

    internal class ProyectoTecnico
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t331_idpt = 1,
            t305_idproyectosubnodo = 2,
            t301_idproyecto = 3,
            t331_despt = 4,
            t301_denominacion = 5

        }

        internal ProyectoTecnico(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        #region funciones publicas

        internal Models.ProyectoTecnico Select(Int32 t331_idpt)
        {
            Models.ProyectoTecnico oPT = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.t331_idpt, t331_idpt)
				};

                dr = cDblib.DataReader("SUP_PT_S", dbparams);
                if (dr.Read())
                {
                    oPT = new Models.ProyectoTecnico();
                    oPT.t331_idpt = t331_idpt;
                    oPT.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oPT.t301_idproyecto = Convert.ToInt32(dr["num_proyecto"]);
                    oPT.t301_denominacion = Convert.ToString(dr["nom_proyecto"]);
                    oPT.t331_despt = Convert.ToString(dr["t331_despt"]);

                }
                return oPT;

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
                case enumDBFields.t331_idpt:
                    paramName = "@t331_idpt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t305_idproyectosubnodo:
                    paramName = "@t305_idproyectosubnodo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t301_idproyecto:
                    paramName = "@t301_idproyecto";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t331_despt:
                    paramName = "@t331_despt";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;

                case enumDBFields.t301_denominacion:
                    paramName = "@t301_denominacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 70;
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