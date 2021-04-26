using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using IB.Progress.Models;

namespace IB.Progress.DAL
{

    internal class Foto
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t932_idfoto = 1,
            t932_denominacion = 2,
            t001_idficepi = 3,
            lt932_idfoto = 4
        }

        internal Foto(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        public Foto()
        {

            //lo dejo pero de momento no se usa
        }

        #endregion

        #region funciones publicas


        /// <summary>
        /// Obtiene todas las fotos
        /// </summary>
        internal List<Models.Foto> Catalogo()
        {
            Models.Foto oFoto = null;
            List<Models.Foto> lst = new List<Models.Foto>();
            IDataReader dr = null;

            try
            {
                dr = cDblib.DataReader("PRO_FOTO", null);
                while (dr.Read())
                {
                    oFoto = new Models.Foto();
                    oFoto.t932_idfoto = Convert.ToInt16(dr["t932_idfoto"]);
                    oFoto.t932_denominacion = Convert.ToString(dr["t932_denominacion"]);
                    oFoto.t932_fechafoto = Convert.ToDateTime(dr["t932_fechafoto"]);
                    oFoto.nombre = Convert.ToString(dr["NOMBRE"]);
                    lst.Add(oFoto);
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

        internal int Insert(int idficepi, string t932_denominacion)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.t001_idficepi, idficepi.ToString()),
					Param(enumDBFields.t932_denominacion, t932_denominacion)
				};

                return (int)cDblib.ExecuteScalar("PRO_PUTFOTO", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        internal int Delete(int t932_idfoto)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.t932_idfoto, t932_idfoto.ToString())
				};

                return (int)cDblib.ExecuteScalar("PRO_FOTO_D", dbparams);
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
                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi_creador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t932_denominacion:
                    paramName = "@t932_denominacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.t932_idfoto:
                    paramName = "@t932_idfoto";
                    paramType = SqlDbType.SmallInt;
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
