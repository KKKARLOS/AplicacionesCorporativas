using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.APP.Models;

/// <summary>
/// Summary description for ALERTA
/// </summary>

namespace IB.SUPER.APP.DAL
{
    public class ALERTA
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;

        private enum enumDBFields : byte
        {
            t820_idalerta = 1
        }

        internal ALERTA(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        /// <summary>
        /// Obtiene todos los ALERTA
        /// </summary>
        internal List<Models.ALERTA> Lista()
        {
            Models.ALERTA oALERTA = null;
            List<Models.ALERTA> lst = new List<Models.ALERTA>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] {};

                dr = cDblib.DataReader("SUP_ALERTAS_C", dbparams);
                while (dr.Read())
                {
                    oALERTA = new Models.ALERTA();
                    oALERTA.t820_idalerta = Convert.ToByte(dr["t820_idalerta"]);
                    oALERTA.t820_denominacion = Convert.ToString(dr["t820_denominacion"]);
                    oALERTA.t821_idgrupoalerta = Convert.ToByte(dr["t821_idgrupoalerta"]);
                    oALERTA.t820_tipo = Convert.ToString(dr["t820_tipo"]);

                    lst.Add(oALERTA);

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
    }
}