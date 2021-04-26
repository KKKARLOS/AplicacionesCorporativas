using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using IB.SUPER.APP.Models;

/// <summary>
/// Summary description for Nodo
/// </summary>

namespace IB.SUPER.APP.DAL
{

    internal class Nodo
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t303_idnodo = 1
        }

        internal Nodo(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los Nodo
        /// </summary>
        internal List<Models.NodoBasico> Catalogo()
        {
            Models.NodoBasico oNodo = null;
            List<Models.NodoBasico> lst = new List<Models.NodoBasico>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] {

                };

                dr = cDblib.DataReader("SUP_NODO_C2", dbparams);
                while (dr.Read())
                {
                    oNodo = new Models.NodoBasico();
                    oNodo.identificador = Convert.ToInt32(dr["t303_idnodo"]);
                    oNodo.denominacion = Convert.ToString(dr["t303_denominacion"]);

                    lst.Add(oNodo);

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
        /// Obtiene todos los Nodos activos cuya parametrización indica que sus contratos no pasan automáticamente por el interfaz de HERMES
        /// </summary>
        internal List<Models.NodoBasico> CatalogoNoHermes()
        {
            Models.NodoBasico oNodo = null;
            List<Models.NodoBasico> lst = new List<Models.NodoBasico>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] {

                };

                dr = cDblib.DataReader("SUP_NODO_NOHERMES_C", dbparams);
                while (dr.Read())
                {
                    oNodo = new Models.NodoBasico();
                    oNodo.identificador = Convert.ToInt32(dr["t303_idnodo"]);
                    oNodo.denominacion = Convert.ToString(dr["t303_denominacion"]);

                    lst.Add(oNodo);

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

    }

}
