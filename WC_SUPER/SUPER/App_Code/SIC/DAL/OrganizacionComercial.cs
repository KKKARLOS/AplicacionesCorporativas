using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for OrganizacionComercial
/// </summary>

namespace IB.SUPER.SIC.DAL
{

    internal class OrganizacionComercial
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            ta212_idorganizacioncomercial = 1,
            ta212_denominacion = 2,
            ta212_codigoexterno = 3,
            ta212_activa = 4
        }

        internal OrganizacionComercial(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un OrganizacionComercial
        /// </summary>
        internal int Insert(Models.OrganizacionComercial oOrganizacionComercial)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.ta212_denominacion, oOrganizacionComercial.ta212_denominacion),
					Param(enumDBFields.ta212_codigoexterno, oOrganizacionComercial.ta212_codigoexterno),
					Param(enumDBFields.ta212_activa, oOrganizacionComercial.ta212_activa)
				};

                return (int)cDblib.Execute("SIC_OrganizacionComercial_INS", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un OrganizacionComercial a partir del id
        /// </summary>
        internal Models.OrganizacionComercial Select(Int32 ta212_idorganizacioncomercial)
        {
            Models.OrganizacionComercial oOrganizacionComercial = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta212_idorganizacioncomercial, ta212_idorganizacioncomercial)
				};

                dr = cDblib.DataReader("SIC_OrganizacionComercial_SEL", dbparams);
                if (dr.Read())
                {
                    oOrganizacionComercial = new Models.OrganizacionComercial();
                    oOrganizacionComercial.ta212_idorganizacioncomercial = Convert.ToInt32(dr["ta212_idorganizacioncomercial"]);
                    oOrganizacionComercial.ta212_denominacion = Convert.ToString(dr["ta212_denominacion"]);
                    oOrganizacionComercial.ta212_codigoexterno = Convert.ToString(dr["ta212_codigoexterno"]);
                    if (!Convert.IsDBNull(dr["ta212_activa"]))
                        oOrganizacionComercial.ta212_activa = Convert.ToBoolean(dr["ta212_activa"]);

                }
                return oOrganizacionComercial;

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

        /// <summary>
        /// Actualiza un OrganizacionComercial a partir del id
        /// </summary>
        internal int Update(Models.OrganizacionComercial oOrganizacionComercial)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
					Param(enumDBFields.ta212_idorganizacioncomercial, oOrganizacionComercial.ta212_idorganizacioncomercial),
					Param(enumDBFields.ta212_denominacion, oOrganizacionComercial.ta212_denominacion),
					Param(enumDBFields.ta212_codigoexterno, oOrganizacionComercial.ta212_codigoexterno),
					Param(enumDBFields.ta212_activa, oOrganizacionComercial.ta212_activa)
				};

                return (int)cDblib.Execute("SIC_OrganizacionComercial_UPD", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un OrganizacionComercial a partir del id
        /// </summary>
        internal int Delete(Int32 ta212_idorganizacioncomercial)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta212_idorganizacioncomercial, ta212_idorganizacioncomercial)
				};

                return (int)cDblib.Execute("SIC_OrganizacionComercial_DEL", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los OrganizacionComercial
        /// </summary>
        internal List<Models.OrganizacionComercial> Catalogo(Nullable<bool> ta212_activa)
        {
            Models.OrganizacionComercial oOrganizacionComercial = null;
            List<Models.OrganizacionComercial> lst = new List<Models.OrganizacionComercial>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta212_activa, ta212_activa)
				};

                dr = cDblib.DataReader("SIC_OrganizacionComercial_C", dbparams);
                while (dr.Read())
                {
                    oOrganizacionComercial = new Models.OrganizacionComercial();
                    oOrganizacionComercial.ta212_idorganizacioncomercial = Convert.ToInt32(dr["ta212_idorganizacioncomercial"]);
                    oOrganizacionComercial.ta212_denominacion = Convert.ToString(dr["ta212_denominacion"]);
                    oOrganizacionComercial.ta212_codigoexterno = Convert.ToString(dr["ta212_codigoexterno"]);
                    if (!Convert.IsDBNull(dr["ta212_activa"]))
                        oOrganizacionComercial.ta212_activa = Convert.ToBoolean(dr["ta212_activa"]);

                    lst.Add(oOrganizacionComercial);

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
                case enumDBFields.ta212_idorganizacioncomercial:
                    paramName = "@ta212_idorganizacioncomercial";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta212_denominacion:
                    paramName = "@ta212_denominacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.ta212_codigoexterno:
                    paramName = "@ta212_codigoexterno";
                    paramType = SqlDbType.VarChar;
                    paramSize = 15;
                    break;
                case enumDBFields.ta212_activa:
                    paramName = "@ta212_activa";
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
