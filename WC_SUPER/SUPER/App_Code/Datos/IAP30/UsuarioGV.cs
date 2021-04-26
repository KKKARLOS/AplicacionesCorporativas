using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoIAP
/// </summary>

namespace IB.SUPER.IAP30.DAL
{
    internal class UsuarioGV
    {
        //public UsuarioGV()
        //{
        //    //
        //    // TODO: Agregar aquí la lógica del constructor
        //    //
        //}
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
        private enum enumDBFields : byte
        {
            t314_idusuario = 1,
        }

        internal UsuarioGV(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion

        #region funciones publicas
        internal Models.UsuarioGV Select(int t314_idusuario)
        {
            Models.UsuarioGV oU = null;
            IDataReader dr = null;
            
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t314_idusuario, t314_idusuario)
                };

                dr = cDblib.DataReader("GVT_DATOSUSUARIO_O", dbparams);
                if (dr.Read())
                {
                    oU = new Models.UsuarioGV();
                    //oUser.t337_esfuerzoenjor = Convert.ToDouble(dr["t337_esfuerzoenjor"]);
                    oU.t314_idusuario = t314_idusuario;
                    oU.Nombre = dr["Profesional"].ToString();
                    oU.t422_idmoneda = dr["t422_idmoneda"].ToString();
                    oU.t313_idempresa = (int)dr["t313_idempresa"];
                    oU.t313_denominacion = dr["t313_denominacion"].ToString();
                    oU.t303_idnodo = (int)dr["t303_idnodo"];
                    oU.t303_denominacion = dr["t303_denominacion"].ToString();
                    //Territorio
                    oU.T007_IDTERRFIS = byte.Parse(dr["t007_idterrfis"].ToString());
                    oU.T007_NOMTERRFIS = dr["t007_nomterrfis"].ToString();
                    oU.T007_ITERDC = decimal.Parse(dr["t007_iterdc"].ToString());
                    oU.T007_ITERMD = decimal.Parse(dr["t007_itermd"].ToString());
                    oU.T007_ITERDA = decimal.Parse(dr["t007_iterda"].ToString());
                    oU.T007_ITERDE = decimal.Parse(dr["t007_iterde"].ToString());
                    oU.T007_ITERK = decimal.Parse(dr["t007_iterk"].ToString());
                    //Oficina
                    oU.t010_idoficina = (short)dr["t010_idoficina_liquidadora"];
                    oU.t010_desoficina = dr["t010_desoficina"].ToString();
                    //Dieta
                    if (dr["t069_iddietakm"] != DBNull.Value)
                        oU.t069_iddietakm = (byte)dr["t069_iddietakm"];
                    oU.t069_descripcion = dr["T069_descripcion"].ToString();
                    oU.t069_icdc = decimal.Parse(dr["T069_icdc"].ToString());
                    oU.t069_icmd = decimal.Parse(dr["T069_icmd"].ToString());
                    oU.t069_icda = decimal.Parse(dr["T069_icda"].ToString());
                    oU.t069_icde = decimal.Parse(dr["T069_icde"].ToString());
                    oU.t069_ick = decimal.Parse(dr["T069_ick"].ToString());
                    if (dr["t010_idoficina_base"] != DBNull.Value)
                        oU.t010_idoficina_base = (int?)int.Parse(dr["t010_idoficina_base"].ToString());
                    oU.bAutorresponsable = ((int)dr["autorresponsable"] == 1) ? true : false;
                }
                return oU;

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
                case enumDBFields.t314_idusuario:
                    paramName = "@t314_idusuario";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
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