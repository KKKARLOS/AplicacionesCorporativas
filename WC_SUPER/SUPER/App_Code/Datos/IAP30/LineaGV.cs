using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

namespace IB.SUPER.IAP30.DAL
{
    internal class LineaGV
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
        private enum enumDBFields : byte
        {
            t421_idposiciongv = 1,
			t420_idreferencia = 2,
            t421_fechadesde = 3,
            t421_fechahasta = 4,
            t421_destino = 5,
            t421_ncdieta = 6,
            t421_nmdieta = 7,
            t421_nadieta = 8,
            t421_nedieta = 9,
            t421_nkms = 10,
            t421_peajepark = 11,
            t421_comida = 12,
            t421_transporte = 13,
            t421_hotel = 14,
            t615_iddesplazamiento = 15,
            t421_comentariopos = 16
        }

        internal LineaGV(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion

        #region Funciones públicas
        internal int Insert(Models.LineaGV oLinea)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[15] {
                    Param(enumDBFields.t420_idreferencia, oLinea.idCabecera),
                    Param(enumDBFields.t421_fechadesde, oLinea.desde),
                    Param(enumDBFields.t421_fechahasta, oLinea.hasta),
                    Param(enumDBFields.t421_destino, oLinea.destino),
                    Param(enumDBFields.t421_comentariopos, oLinea.anotacion),
                    Param(enumDBFields.t421_ncdieta, oLinea.dietaCompleta),
                    Param(enumDBFields.t421_nmdieta, oLinea.mediaDieta),
                    Param(enumDBFields.t421_nedieta, oLinea.dietaEspecial),
                    Param(enumDBFields.t421_nadieta, oLinea.dietaAlojamiento),
                    Param(enumDBFields.t421_nkms, oLinea.numKm),
                    Param(enumDBFields.t615_iddesplazamiento, oLinea.idECO),
                    Param(enumDBFields.t421_peajepark, oLinea.peaje),
                    Param(enumDBFields.t421_comida, oLinea.comida),
                    Param(enumDBFields.t421_transporte, oLinea.transporte),
                    Param(enumDBFields.t421_hotel, oLinea.hotel)
                };

                return (int)cDblib.Execute("GVT_POSICIONGV_INS", dbparams);
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
                case enumDBFields.t421_idposiciongv:
                    paramName = "@t421_idposiciongv";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t420_idreferencia:
                    paramName = "@t420_idreferencia";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t421_fechadesde:
                    paramName = "@t421_fechadesde";
                    paramType = SqlDbType.DateTime;
                    paramSize = 4;
                    break;
                case enumDBFields.t421_fechahasta:
                    paramName = "@t421_fechahasta";
                    paramType = SqlDbType.DateTime;
                    paramSize = 4;
                    break;
                case enumDBFields.t421_destino:
                    paramName = "@t421_destino";
                    paramType = SqlDbType.Char;
                    paramSize = 50;
                    break;
                case enumDBFields.t421_ncdieta:
                    paramName = "@t421_ncdieta";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.t421_nmdieta:
                    paramName = "@t421_nmdieta";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.t421_nadieta:
                    paramName = "@t421_nadieta";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.t421_nedieta:
                    paramName = "@t421_nedieta";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.t421_nkms:
                    paramName = "@t421_nkms";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 2;
                    break;
                case enumDBFields.t421_peajepark:
                    paramName = "@t421_peajepark";
                    paramType = SqlDbType.Money;
                    paramSize = 8;
                    break;
                case enumDBFields.t421_comida:
                    paramName = "@t421_comida";
                    paramType = SqlDbType.Money;
                    paramSize = 8;
                    break;
                case enumDBFields.t421_transporte:
                    paramName = "@t421_transporte";
                    paramType = SqlDbType.Money;
                    paramSize = 8;
                    break;
                case enumDBFields.t421_hotel:
                    paramName = "@t421_hotel";
                    paramType = SqlDbType.Money;
                    paramSize = 8;
                    break;
                case enumDBFields.t615_iddesplazamiento:
                    paramName = "@t615_iddesplazamiento";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t421_comentariopos:
                    paramName = "@t421_comentariopos";
                    paramType = SqlDbType.Text;
                    paramSize = 2147483647;
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