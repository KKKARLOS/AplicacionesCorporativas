using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AsuntoCat
/// </summary>

namespace IB.SUPER.IAP30.DAL
{

    internal class AsuntoCat
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            num_proyecto = 1,
            t305_idproyectosubnodo = 2,
            T382_alerta = 3,
            T382_desasunto = 4,
            T382_desasuntolong = 5,
            T382_dpto = 6,
            T382_estado = 7,
            T382_etp = 8,
            T382_etr = 9,
            T382_fcreacion = 10,
            T382_ffin = 11,
            T382_flimite = 12,
            T382_fnotificacion = 13,
            T382_idasunto = 14,
            T382_notificador = 15,
            T382_obs = 16,
            T382_prioridad = 17,
            T382_refexterna = 18,
            T382_registrador = 19,
            T382_responsable = 20,
            T382_severidad = 21,
            T382_sistema = 22,
            T384_idtipo = 23,
            T384_destipo = 24
        }

        internal AsuntoCat(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los Asunto
        /// </summary>
        internal List<Models.AsuntoCat> Catalogo(int nPSN, Nullable<int> TipoAsunto, Nullable<byte> Estado)
        {
            Models.AsuntoCat oAsunto = null;
            List<Models.AsuntoCat> lst = new List<Models.AsuntoCat>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t305_idproyectosubnodo, nPSN),
                    Param(enumDBFields.T382_estado, Estado),
                    Param(enumDBFields.T384_idtipo, TipoAsunto)
                };
                dr = cDblib.DataReader("SUP_ASUNTO_CAT", dbparams);
                while (dr.Read())
                {
                    oAsunto = new Models.AsuntoCat();
                    oAsunto.idAsunto = Convert.ToInt32(dr["T382_idasunto"]);
                    oAsunto.desAsunto = Convert.ToString(dr["desAsunto"]);
                    oAsunto.desTipo = Convert.ToString(dr["desTipo"]);
                    oAsunto.severidad = Convert.ToString(dr["severidad"]);
                    oAsunto.prioridad = Convert.ToString(dr["prioridad"]);
                    oAsunto.estado = Convert.ToString(dr["estado"]);
                    if (!Convert.IsDBNull(dr["fLimite"]))
                        oAsunto.fLimite = Convert.ToDateTime(dr["fLimite"]);
                    oAsunto.fNotificacion = Convert.ToDateTime(dr["fNotificacion"]);
                    oAsunto.idUserResponsable = Convert.ToInt32(dr["idUserResponsable"]);
                    lst.Add(oAsunto);
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

        internal void Borrar(int idAsunto)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.T382_idasunto, idAsunto)
				};

                cDblib.Execute("SUP_ASUNTO_D", dbparams);
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
                case enumDBFields.T382_alerta:
                    paramName = "@T382_alerta";
                    paramType = SqlDbType.Text;
                    paramSize = 2147483647;
                    break;
                case enumDBFields.T382_desasunto:
                    paramName = "@T382_desasunto";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.T382_desasuntolong:
                    paramName = "@T382_desasuntolong";
                    paramType = SqlDbType.Text;
                    paramSize = 2147483647;
                    break;
                case enumDBFields.T382_dpto:
                    paramName = "@T382_dpto";
                    paramType = SqlDbType.Text;
                    paramSize = 2147483647;
                    break;
                case enumDBFields.T382_estado:
                    paramName = "@T382_estado";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.T382_etp:
                    paramName = "@T382_etp";
                    paramType = SqlDbType.Float;
                    paramSize = 8;
                    break;
                case enumDBFields.T382_etr:
                    paramName = "@T382_etr";
                    paramType = SqlDbType.Float;
                    paramSize = 8;
                    break;
                case enumDBFields.T382_fcreacion:
                    paramName = "@T382_fcreacion";
                    paramType = SqlDbType.DateTime;
                    paramSize = 8;
                    break;
                case enumDBFields.T382_ffin:
                    paramName = "@T382_ffin";
                    paramType = SqlDbType.DateTime;
                    paramSize = 8;
                    break;
                case enumDBFields.T382_flimite:
                    paramName = "@T382_flimite";
                    paramType = SqlDbType.DateTime;
                    paramSize = 8;
                    break;
                case enumDBFields.T382_fnotificacion:
                    paramName = "@T382_fnotificacion";
                    paramType = SqlDbType.DateTime;
                    paramSize = 8;
                    break;
                case enumDBFields.T382_idasunto:
                    paramName = "@T382_idasunto";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.T382_notificador:
                    paramName = "@T382_notificador";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.T382_obs:
                    paramName = "@T382_obs";
                    paramType = SqlDbType.Text;
                    paramSize = 2147483647;
                    break;
                case enumDBFields.T382_prioridad:
                    paramName = "@T382_prioridad";
                    paramType = SqlDbType.VarChar;
                    paramSize = 22;
                    break;
                case enumDBFields.T382_refexterna:
                    paramName = "@T382_refexterna";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.T382_registrador:
                    paramName = "@T382_registrador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.T382_responsable:
                    paramName = "@T382_responsable";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.T382_severidad:
                    paramName = "@T382_severidad";
                    paramType = SqlDbType.VarChar;
                    paramSize = 22;
                    break;
                case enumDBFields.T382_sistema:
                    paramName = "@T382_sistema";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.T384_idtipo:
                    paramName = "@T384_idtipo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.T384_destipo:
                    paramName = "@T384_destipo";
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
