using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Aplicacion
/// </summary>
namespace IB.Progress.DAL
{
    public class Aplicacion
    {
        public Aplicacion()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
           
        }

        internal Aplicacion(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        internal IB.Progress.Models.Aplicacion Acceso()
        {
            IB.Progress.Models.Aplicacion oAplicacion = new IB.Progress.Models.Aplicacion();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] {
					
				};
                dr = cDblib.DataReader("PRO_ACCESO", dbparams);

                if (dr.Read())
                {
                    oAplicacion.Estado = bool.Parse(dr["t000_estado"].ToString());
                    oAplicacion.Motivo = dr["t000_motivo"].ToString();
                }
                return oAplicacion;
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


        private SqlParameter Param(ParameterDirection paramDirection, enumDBFields dbField, object value)
        {
            SqlParameter dbParam = null;
            string paramName = null;
            SqlDbType paramType = default(SqlDbType);
            int paramSize = 0;

            switch (dbField)
            {
               
            }

            dbParam = cDblib.dbParameter(paramName, paramType, paramSize);
            dbParam.Direction = paramDirection;
            if (paramDirection == ParameterDirection.Input | paramDirection == ParameterDirection.InputOutput) dbParam.Value = value;

            return dbParam;

        }
    }
}