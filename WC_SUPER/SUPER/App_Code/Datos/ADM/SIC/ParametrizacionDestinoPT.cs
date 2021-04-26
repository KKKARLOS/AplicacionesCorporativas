using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.ADM.SIC.Models;

/// <summary>
/// Summary description for ParametrizacionDestinoPT
/// </summary>

namespace IB.SUPER.ADM.SIC.DAL
{

    internal class ParametrizacionDestinoPT
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            ta212_idorganizacioncomercial = 1,
            t001_idficepi_comercial = 2,
            t331_idpt = 3,
            ta212_activa = 4,
            bMostrarProfBaja = 5,
            ta213_nocambioautomatico = 6
        }

        internal ParametrizacionDestinoPT(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un ParametrizacionDestinoPT
        /// </summary>
        internal int Insert(Models.ParametrizacionDestinoPT oParametrizacionDestinoPT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(enumDBFields.ta212_idorganizacioncomercial, oParametrizacionDestinoPT.ta212_idorganizacioncomercial),
                    Param(enumDBFields.t001_idficepi_comercial, oParametrizacionDestinoPT.t001_idficepi_comercial),
                    Param(enumDBFields.t331_idpt, oParametrizacionDestinoPT.t331_idpt),
                    Param(enumDBFields.ta213_nocambioautomatico, oParametrizacionDestinoPT.ta213_nocambioautomatico)
                };

                return (int)cDblib.Execute("SIC_ParametrizacionDestinoPT_I", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un ParametrizacionDestinoPT a partir del id
        /// </summary>
        //internal Models.ParametrizacionDestinoPT Select(Int32 ta212_idorganizacioncomercial, Int32 t001_idficepi_comercial)
        //{
        //    Models.ParametrizacionDestinoPT oParametrizacionDestinoPT = null;
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[2] {
        //            Param(enumDBFields.ta212_idorganizacioncomercial, ta212_idorganizacioncomercial),
        //            Param(enumDBFields.t001_idficepi_comercial, t001_idficepi_comercial)
        //        };

        //        dr = cDblib.DataReader("SIC_ParametrizacionDestinoPT_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oParametrizacionDestinoPT = new Models.ParametrizacionDestinoPT();
        //            oParametrizacionDestinoPT.ta212_idorganizacioncomercial = Convert.ToInt32(dr["ta212_idorganizacioncomercial"]);
        //            oParametrizacionDestinoPT.t001_idficepi_comercial = Convert.ToInt32(dr["t001_idficepi_comercial"]);
        //            oParametrizacionDestinoPT.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);

        //        }
        //        return oParametrizacionDestinoPT;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dr != null)
        //        {
        //            if (!dr.IsClosed) dr.Close();
        //            dr.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Actualiza un ParametrizacionDestinoPT a partir del id
        /// </summary>
        internal int Update(Models.ParametrizacionDestinoPT oParametrizacionDestinoPT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(enumDBFields.ta212_idorganizacioncomercial, oParametrizacionDestinoPT.ta212_idorganizacioncomercial),
                    Param(enumDBFields.t001_idficepi_comercial, oParametrizacionDestinoPT.t001_idficepi_comercial),
                    Param(enumDBFields.t331_idpt, oParametrizacionDestinoPT.t331_idpt),
                    Param(enumDBFields.ta213_nocambioautomatico, oParametrizacionDestinoPT.ta213_nocambioautomatico)
                };

                return (int)cDblib.Execute("SIC_ParametrizacionDestinoPT_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un ParametrizacionDestinoPT a partir del id
        /// </summary>
        internal int Delete(Int32 ta212_idorganizacioncomercial, Int32 t001_idficepi_comercial)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.ta212_idorganizacioncomercial, ta212_idorganizacioncomercial),
                    Param(enumDBFields.t001_idficepi_comercial, t001_idficepi_comercial)
                };

                return (int)cDblib.Execute("SIC_ParametrizacionDestinoPT_DEL", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los ParametrizacionDestinoPT
        /// </summary>
        internal List<Models.ParametrizacionDestinoPT> Catalogo(Nullable<bool> bSoloActivas, Nullable<int> ta212_idorganizacioncomercial,
                                                                Nullable<int> t001_idficepi, bool bMostrarProfBaja)
        {
            Models.ParametrizacionDestinoPT oP = null;
            List<Models.ParametrizacionDestinoPT> lst = new List<Models.ParametrizacionDestinoPT>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
					Param(enumDBFields.ta212_activa, bSoloActivas),
					Param(enumDBFields.ta212_idorganizacioncomercial, ta212_idorganizacioncomercial),
					Param(enumDBFields.t001_idficepi_comercial, t001_idficepi),
					Param(enumDBFields.bMostrarProfBaja, bMostrarProfBaja)
				};

                dr = cDblib.DataReader("SIC_ParametrizacionDestinoPT_C", dbparams);
                while (dr.Read())
                {
                    oP = new Models.ParametrizacionDestinoPT();
                    oP.ta212_idorganizacioncomercial = Convert.ToInt32(dr["ta212_idorganizacioncomercial"]);
                    oP.t001_idficepi_comercial = Convert.ToInt32(dr["t001_idficepi"]);
                    if (dr["t331_idpt"].ToString() != "")
                        oP.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);

                    oP.denOC = dr["ta212_denominacion"].ToString();
                    oP.denProfesional = dr["Profesional"].ToString();
                    oP.denPT = dr["t331_despt"].ToString();
                    if (dr["baja"].ToString() == "1")
                        oP.baja = true;
                    else
                        oP.baja = false;

                    if (!Convert.IsDBNull(dr["ta213_nocambioautomatico"]))                        
                        oP.ta213_nocambioautomatico = bool.Parse(dr["ta213_nocambioautomatico"].ToString());
                                                                   
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

        /// <summary>
        /// Obtiene todos los ParametrizacionDestinoPT
        /// </summary>
        internal List<Models.ParametrizacionDestinoPT> CatParametrizaciones()
        {
            Models.ParametrizacionDestinoPT oP = null;
            List<Models.ParametrizacionDestinoPT> lst = new List<Models.ParametrizacionDestinoPT>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[0];

                dr = cDblib.DataReader("SIC_PARAMETRIZACIONDESTINOPT_TOTAL", dbparams);
                while (dr.Read())
                {
                    oP = new Models.ParametrizacionDestinoPT();
                    oP.ta212_idorganizacioncomercial = Convert.ToInt32(dr["ta212_idorganizacioncomercial"]);
                    oP.t001_idficepi_comercial = Convert.ToInt32(dr["t001_idficepi_comercial"]);
                    if (dr["t331_idpt"].ToString() != "")
                        oP.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);

                    oP.denOC = dr["organizacioncomercial"].ToString();
                    oP.denProfesional = dr["comercial"].ToString();
                    oP.denPT = dr["proyectotecnico"].ToString();                   

                    if (!Convert.IsDBNull(dr["nocambioautomatico"]))
                        oP.ta213_nocambioautomatico = bool.Parse(dr["nocambioautomatico"].ToString());

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
                case enumDBFields.ta212_idorganizacioncomercial:
                    paramName = "@ta212_idorganizacioncomercial";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t001_idficepi_comercial:
                    paramName = "@t001_idficepi_comercial";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t331_idpt:
                    paramName = "@t331_idpt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta212_activa:
                    paramName = "@ta212_activa";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.bMostrarProfBaja:
                    paramName = "@bMostrarProfBaja";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;

                case enumDBFields.ta213_nocambioautomatico:
                    paramName = "@ta213_nocambioautomatico";
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
