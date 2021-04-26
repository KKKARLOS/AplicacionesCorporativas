using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using IB.Progress.Models;
using IB.Progress.Shared;



namespace IB.Progress.DAL
{

    internal class ImagenHome
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            picture = 1            
        }

        internal ImagenHome(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        #region funciones publicas

       

        internal int Update(byte[] pic)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {					
                    Param(enumDBFields.picture, pic)
					
				};

                return (int)cDblib.Execute("PRO_IMAGENHOME_UPDATE", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la imagen de la home
        /// </summary>
        /// <returns></returns>
        internal byte[] Select()
        {
            IDataReader dr = null;
            byte[] img = new byte[0];
            try{
                dr  = cDblib.DataReader("PRO_IMAGENHOME_SELECT", null);

                if (dr.Read())
                {
                    img = (byte[])dr["t936_imagen_home"];
                }
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
            
            return img;
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
                case enumDBFields.picture:
                    paramName = "@imagen";
                    paramType = SqlDbType.VarBinary;
                    paramSize = -1;
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
