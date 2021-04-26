using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace IB.SUPER.DAL
{
    /// <summary>
    /// Descripción breve de bsUsuario
    /// </summary>
    internal class bsUsuario
    {

        #region variables privadas y constructor
        private IB.sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t314_idusuario = 1,
            t001_idficepi = 2, 
            t001_idficepi_pc= 3

        }

        internal bsUsuario(IB.sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        public bsUsuario()
        {
           
        }

       /// <summary>
       /// Obtiene las acciones pendientes
       /// </summary>
       /// <param name="t314_idusuario"></param>
       /// <param name="t001_idficepi"></param>
       /// <returns></returns>
        internal List<IB.SUPER.Models.bsUsuario> CatalogoAccionesPendientes(Nullable<Int32> t314_idusuario, Nullable<Int32> t001_idficepi, Nullable<Int32> t001_idficepi_pc)
        {
            IB.SUPER.Models.bsUsuario oUsuario = null;
            List<Models.bsUsuario> returnlist = new List<Models.bsUsuario>();

            IDataReader dr = null;
           
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                        Param(enumDBFields.t314_idusuario, t314_idusuario),
                        Param(enumDBFields.t001_idficepi, t001_idficepi),
                        Param(enumDBFields.t001_idficepi_pc, t001_idficepi_pc),

                    };

                dr = cDblib.DataReader("SUP_ACCIONESPENDIENTES", dbparams);

                while (dr.Read())
                {
                    if ((byte)dr["criticidad"] == 2)
                    {
                        switch (dr["modulo"].ToString())
                        {
                            case "PGE": HttpContext.Current.Session["BloquearPGEByAcciones"] = true; break;
                            case "PST": HttpContext.Current.Session["BloquearPSTByAcciones"] = true; break;
                            case "IAP": HttpContext.Current.Session["BloquearIAPByAcciones"] = true; break;                            
                        }
                    }

                    oUsuario = new IB.SUPER.Models.bsUsuario();

                    if (!Convert.IsDBNull(dr["denominacion"]))
                        oUsuario.Denominacion = dr["denominacion"].ToString();

                    if (!Convert.IsDBNull(dr["codigo"]))
                        oUsuario.codigo = int.Parse(dr["codigo"].ToString());

                    if (HttpContext.Current.Session["BloquearPGEByAcciones"] != null)
                    {
                        if ((bool)HttpContext.Current.Session["BloquearPGEByAcciones"])
                            oUsuario.BloquearPGEByAcciones = "1";
                        else
                            oUsuario.BloquearPGEByAcciones = "0";
                    }

                    if (HttpContext.Current.Session["BloquearPSTByAcciones"] != null)
                    {
                        if ((bool)HttpContext.Current.Session["BloquearPSTByAcciones"])
                            oUsuario.BloquearPSTByAcciones = "1";
                        else
                            oUsuario.BloquearPSTByAcciones = "0";
                    }

                    if (HttpContext.Current.Session["BloquearIAPByAcciones"] != null)
                    {
                        if ((bool)HttpContext.Current.Session["BloquearIAPByAcciones"])
                            oUsuario.BloquearIAPByAcciones = "1";
                        else
                            oUsuario.BloquearIAPByAcciones = "0";
                    }
                  
                    returnlist.Add(oUsuario);

                }

                return returnlist;

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

                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi_cvt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                    case enumDBFields.t001_idficepi_pc:
                    paramName = "@t001_idficepi_pc";
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