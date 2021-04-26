using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using IB.Progress.Models;

namespace IB.Progress.DAL
{

    internal class Colectivo
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t941_idcolectivo = 1,
            t941_denominacion = 2
        }

        internal Colectivo(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        public Colectivo()
        {

            //lo dejo pero de momento no se usa
        }

        #endregion

        #region funciones publicas


        /// <summary>
        /// Obtiene todos los Colectivos
        /// </summary>
        internal List<Models.Colectivo> Catalogo()
        {
            Models.Colectivo oColectivo = null;
            List<Models.Colectivo> lst = new List<Models.Colectivo>();
            IDataReader dr = null;

            try
            {
                //SqlParameter[] dbparams = new SqlParameter[0];

//                dr = cDblib.DataReader("PRO_COLECTIVO", dbparams);
                dr = cDblib.DataReader("PRO_COLECTIVO", null);
                while (dr.Read())
                {
                    oColectivo = new Models.Colectivo();
                    oColectivo.t941_idcolectivo = Convert.ToInt16(dr["t941_idcolectivo"]);
                    oColectivo.t941_denominacion = Convert.ToString(dr["t941_denominacion"]);
                    lst.Add(oColectivo);
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
        #endregion
    }
}
