using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IB.SUPER.SIC.DAL
{

    internal class Ayudas
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;

        private enum enumDBFields : byte
        {
            filtro = 1,
            t001_idficepi = 2,
            tipo_itemorigen = 3,
            actuocomoadministrador = 4
        }

        internal Ayudas(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        internal List<IB.SUPER.APP.Models.KeyValue2> Buscar(BLL.Ayudas.enumAyuda ayuda, string filtro)
        {
            IDataReader dr = null;
            List<IB.SUPER.APP.Models.KeyValue2> lst = new List<IB.SUPER.APP.Models.KeyValue2>();

            string nomproc = "";

            switch (ayuda)
            {
                case BLL.Ayudas.enumAyuda.cuentasCRM:
                    nomproc = "SIC_AYUDACUENTAS_CAT";
                    break;
                case BLL.Ayudas.enumAyuda.accionesPreventa:
                    nomproc = "SIC_AYUDAACCIONESPREVENTA_CAT";
                    break;

                case BLL.Ayudas.enumAyuda.SIC_AYUDA3UNIDADESPREVENTA_CAT:
                    nomproc = "SIC_AYUDA3UNIDADESPREVENTA_CAT";
                    break;
                case BLL.Ayudas.enumAyuda.SIC_AYUDA3AREASPREVENTA_CAT:
                    nomproc = "SIC_AYUDA3AREASPREVENTA_CAT";
                    break;
                case BLL.Ayudas.enumAyuda.SIC_AYUDA3SUBAREASPREVENTA_CAT:
                    nomproc = "SIC_AYUDA3SUBAREASPREVENTA_CAT";
                    break;


                //case BLL.Ayudas.enumAyuda.unidadesPreventa:
                //    nomproc = "SIC_AYUDAUNIDADES_CAT";
                //    break;
            }

            if (filtro.Trim().Length == 0) filtro = null;
            SqlParameter[] dbparams = new SqlParameter[1] {
        		Param(enumDBFields.filtro, filtro)
		    };

            try
            {
                dr = cDblib.DataReader(nomproc, dbparams);
                while (dr.Read())
                {
                    lst.Add(new IB.SUPER.APP.Models.KeyValue2(Convert.ToString(dr["id"]), Convert.ToString(dr["value"])));
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

        internal List<IB.SUPER.APP.Models.KeyValue2> BuscarConFicepi(BLL.Ayudas.enumAyuda ayuda, string filtro, int? t001_idficepi, bool admin)
        {
            IDataReader dr = null;
            List<IB.SUPER.APP.Models.KeyValue2> lst = new List<IB.SUPER.APP.Models.KeyValue2>();

            string nomproc = "";

            if (filtro.Trim().Length == 0) filtro = null;
            List<SqlParameter> dbparams = new List<SqlParameter> {
        		Param(enumDBFields.filtro, filtro),
                Param(enumDBFields.t001_idficepi, t001_idficepi),
                
		    };
 
            
            switch (ayuda)
            {
                case BLL.Ayudas.enumAyuda.SIC_AYUDA1UNIDADESPREVENTA_CAT:
                    nomproc = "SIC_AYUDA1UNIDADESPREVENTA_CAT";
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    break;
                case BLL.Ayudas.enumAyuda.SIC_AYUDA1AREASPREVENTA_CAT:
                    nomproc = "SIC_AYUDA1AREASPREVENTA_CAT";
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    break;
                case BLL.Ayudas.enumAyuda.SIC_AYUDA1SUBAREASPREVENTA_CAT:
                    nomproc = "SIC_AYUDA1SUBAREASPREVENTA_CAT";
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    break;

                case BLL.Ayudas.enumAyuda.SIC_AYUDA4UNIDADESPREVENTA_CAT:
                    nomproc = "SIC_AYUDA4UNIDADESPREVENTA_CAT";
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, null));
                    break;
                case BLL.Ayudas.enumAyuda.SIC_AYUDA4AREASPREVENTA_CAT:
                    nomproc = "SIC_AYUDA4AREASPREVENTA_CAT";
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    break;
                case BLL.Ayudas.enumAyuda.SIC_AYUDA4SUBAREASPREVENTA_CAT:
                    nomproc = "SIC_AYUDA4SUBAREASPREVENTA_CAT";
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    break;

                case BLL.Ayudas.enumAyuda.SIC_AYUDA1TEMORIGEN_O_CAT:
                    nomproc = "SIC_AYUDA1ITEMORIGEN_CAT";
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    dbparams.Add(Param(enumDBFields.tipo_itemorigen, "O"));
                    break;
                case BLL.Ayudas.enumAyuda.SIC_AYUDA1TEMORIGEN_E_CAT:
                    nomproc = "SIC_AYUDA1ITEMORIGEN_CAT";
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    dbparams.Add(Param(enumDBFields.tipo_itemorigen, "E"));
                    break;
                case BLL.Ayudas.enumAyuda.SIC_AYUDA1TEMORIGEN_P_CAT:
                    nomproc = "SIC_AYUDA1ITEMORIGEN_CAT";
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    dbparams.Add(Param(enumDBFields.tipo_itemorigen, "P"));
                    break;
                case BLL.Ayudas.enumAyuda.SIC_AYUDA1TEMORIGEN_S_CAT:
                    nomproc = "SIC_AYUDA1ITEMORIGEN_CAT";
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    dbparams.Add(Param(enumDBFields.tipo_itemorigen, "S"));
                    break;

                case BLL.Ayudas.enumAyuda.SIC_AYUDA2UNIDADESPREVENTA_CAT:
                    nomproc = "SIC_AYUDA2UNIDADESPREVENTA_CAT";
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    break;
                case BLL.Ayudas.enumAyuda.SIC_AYUDA2AREASPREVENTA_CAT:
                    nomproc = "SIC_AYUDA2AREASPREVENTA_CAT";
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    break;
                case BLL.Ayudas.enumAyuda.SIC_AYUDA2SUBAREASPREVENTA_CAT:
                    nomproc = "SIC_AYUDA2SUBAREASPREVENTA_CAT";
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    break;
                case BLL.Ayudas.enumAyuda.SIC_AYUDA2TEMORIGEN_O_CAT:
                    nomproc = "SIC_AYUDA2ITEMORIGEN_CAT";
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    dbparams.Add(Param(enumDBFields.tipo_itemorigen, "O"));
                    break;
                case BLL.Ayudas.enumAyuda.SIC_AYUDA2TEMORIGEN_E_CAT:
                    nomproc = "SIC_AYUDA2ITEMORIGEN_CAT";
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    dbparams.Add(Param(enumDBFields.tipo_itemorigen, "E"));
                    break;
                case BLL.Ayudas.enumAyuda.SIC_AYUDA2TEMORIGEN_P_CAT:
                    nomproc = "SIC_AYUDA2ITEMORIGEN_CAT";
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    dbparams.Add(Param(enumDBFields.tipo_itemorigen, "P"));
                    break;
                case BLL.Ayudas.enumAyuda.SIC_AYUDA2TEMORIGEN_S_CAT:
                    nomproc = "SIC_AYUDA2ITEMORIGEN_CAT";
                    dbparams.Add(Param(enumDBFields.actuocomoadministrador, admin));
                    dbparams.Add(Param(enumDBFields.tipo_itemorigen, "S"));
                    break;


                case BLL.Ayudas.enumAyuda.SIC_AYUDA3ITEMORIGEN_O_CAT:
                    nomproc = "SIC_AYUDA3ITEMORIGEN_CAT";
                    dbparams.Add(Param(enumDBFields.tipo_itemorigen, "O"));
                    break;
                case BLL.Ayudas.enumAyuda.SIC_AYUDA3ITEMORIGEN_E_CAT:
                    nomproc = "SIC_AYUDA3ITEMORIGEN_CAT";
                    dbparams.Add(Param(enumDBFields.tipo_itemorigen, "E"));
                    break;
                case BLL.Ayudas.enumAyuda.SIC_AYUDA3ITEMORIGEN_P_CAT:
                    nomproc = "SIC_AYUDA3ITEMORIGEN_CAT";
                    dbparams.Add(Param(enumDBFields.tipo_itemorigen, "P"));
                    break;

            }

            try
            {
                dr = cDblib.DataReader(nomproc, dbparams.ToArray());
                while (dr.Read())
                {
                    lst.Add(new IB.SUPER.APP.Models.KeyValue2(Convert.ToString(dr["id"]), Convert.ToString(dr["value"])));
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
                case enumDBFields.filtro:
                    paramName = "@filtro";
                    paramType = SqlDbType.VarChar;
                    paramSize = 250;
                    break;
                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.tipo_itemorigen:
                    paramName = "@tipo_itemorigen";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.actuocomoadministrador:
                    paramName = "@actuocomoadministrador";
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