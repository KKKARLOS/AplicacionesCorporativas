using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for PosicionGV
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class PosicionGV 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t420_idreferencia = 1,
			t421_fechadesde = 2,
			t421_fechahasta = 3,
			t421_destino = 4,
			t421_comentariopos = 5,
			t421_ncdieta = 6,
			t421_nmdieta = 7,
			t421_nedieta = 8,
			t421_nadieta = 9,
			t421_nkms = 10,
			t615_iddesplazamiento = 11,
			t421_peajepark = 12,
			t421_comida = 13,
			t421_transporte = 14,
			t421_hotel = 15
        }

        internal PosicionGV(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        //#region funciones publicas
        ///// <summary>
        ///// Inserta un PosicionGV
        ///// </summary>
        //internal int Insert(Models.PosicionGV oPosicionGV)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[15] {
        //            Param(enumDBFields.t420_idreferencia, oPosicionGV.t420_idreferencia),
        //            Param(enumDBFields.t421_fechadesde, oPosicionGV.t421_fechadesde),
        //            Param(enumDBFields.t421_fechahasta, oPosicionGV.t421_fechahasta),
        //            Param(enumDBFields.t421_destino, oPosicionGV.t421_destino),
        //            Param(enumDBFields.t421_comentariopos, oPosicionGV.t421_comentariopos),
        //            Param(enumDBFields.t421_ncdieta, oPosicionGV.t421_ncdieta),
        //            Param(enumDBFields.t421_nmdieta, oPosicionGV.t421_nmdieta),
        //            Param(enumDBFields.t421_nedieta, oPosicionGV.t421_nedieta),
        //            Param(enumDBFields.t421_nadieta, oPosicionGV.t421_nadieta),
        //            Param(enumDBFields.t421_nkms, oPosicionGV.t421_nkms),
        //            Param(enumDBFields.t615_iddesplazamiento, oPosicionGV.t615_iddesplazamiento),
        //            Param(enumDBFields.t421_peajepark, oPosicionGV.t421_peajepark),
        //            Param(enumDBFields.t421_comida, oPosicionGV.t421_comida),
        //            Param(enumDBFields.t421_transporte, oPosicionGV.t421_transporte),
        //            Param(enumDBFields.t421_hotel, oPosicionGV.t421_hotel)
        //        };

        //        return (int)cDblib.Execute("_PosicionGV_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        internal bool EsDesplazamientoECOenVUP(int t615_iddesplazamiento)
        {
            IDataReader dr = null;
            int iRes = 0;
            bool bRes = false;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                            Param(enumDBFields.t615_iddesplazamiento, t615_iddesplazamiento),
                        };

                dr = cDblib.DataReader("GVT_ESDESPLAZAMIENTOVUP", dbparams);
                if (dr.Read())
                {
                    iRes = Convert.ToInt32(dr[0]);
                }
                if (iRes == 1)
                    bRes = true;

                return bRes;
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

        ///// <summary>
        ///// Actualiza un PosicionGV a partir del id
        ///// </summary>
        //internal int Update(Models.PosicionGV oPosicionGV)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[15] {
        //            Param(enumDBFields.t420_idreferencia, oPosicionGV.t420_idreferencia),
        //            Param(enumDBFields.t421_fechadesde, oPosicionGV.t421_fechadesde),
        //            Param(enumDBFields.t421_fechahasta, oPosicionGV.t421_fechahasta),
        //            Param(enumDBFields.t421_destino, oPosicionGV.t421_destino),
        //            Param(enumDBFields.t421_comentariopos, oPosicionGV.t421_comentariopos),
        //            Param(enumDBFields.t421_ncdieta, oPosicionGV.t421_ncdieta),
        //            Param(enumDBFields.t421_nmdieta, oPosicionGV.t421_nmdieta),
        //            Param(enumDBFields.t421_nedieta, oPosicionGV.t421_nedieta),
        //            Param(enumDBFields.t421_nadieta, oPosicionGV.t421_nadieta),
        //            Param(enumDBFields.t421_nkms, oPosicionGV.t421_nkms),
        //            Param(enumDBFields.t615_iddesplazamiento, oPosicionGV.t615_iddesplazamiento),
        //            Param(enumDBFields.t421_peajepark, oPosicionGV.t421_peajepark),
        //            Param(enumDBFields.t421_comida, oPosicionGV.t421_comida),
        //            Param(enumDBFields.t421_transporte, oPosicionGV.t421_transporte),
        //            Param(enumDBFields.t421_hotel, oPosicionGV.t421_hotel)
        //        };

        //        return (int)cDblib.Execute("_PosicionGV_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Elimina un PosicionGV a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {


        //        return (int)cDblib.Execute("_PosicionGV_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los PosicionGV
        ///// </summary>
        //internal List<Models.PosicionGV> Catalogo(Models.PosicionGV oPosicionGVFilter)
        //{
        //    Models.PosicionGV oPosicionGV = null;
        //    List<Models.PosicionGV> lst = new List<Models.PosicionGV>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[15] {
        //            Param(enumDBFields.t420_idreferencia, oTEMP_PosicionGVFilter.t420_idreferencia),
        //            Param(enumDBFields.t421_fechadesde, oTEMP_PosicionGVFilter.t421_fechadesde),
        //            Param(enumDBFields.t421_fechahasta, oTEMP_PosicionGVFilter.t421_fechahasta),
        //            Param(enumDBFields.t421_destino, oTEMP_PosicionGVFilter.t421_destino),
        //            Param(enumDBFields.t421_comentariopos, oTEMP_PosicionGVFilter.t421_comentariopos),
        //            Param(enumDBFields.t421_ncdieta, oTEMP_PosicionGVFilter.t421_ncdieta),
        //            Param(enumDBFields.t421_nmdieta, oTEMP_PosicionGVFilter.t421_nmdieta),
        //            Param(enumDBFields.t421_nedieta, oTEMP_PosicionGVFilter.t421_nedieta),
        //            Param(enumDBFields.t421_nadieta, oTEMP_PosicionGVFilter.t421_nadieta),
        //            Param(enumDBFields.t421_nkms, oTEMP_PosicionGVFilter.t421_nkms),
        //            Param(enumDBFields.t615_iddesplazamiento, oTEMP_PosicionGVFilter.t615_iddesplazamiento),
        //            Param(enumDBFields.t421_peajepark, oTEMP_PosicionGVFilter.t421_peajepark),
        //            Param(enumDBFields.t421_comida, oTEMP_PosicionGVFilter.t421_comida),
        //            Param(enumDBFields.t421_transporte, oTEMP_PosicionGVFilter.t421_transporte),
        //            Param(enumDBFields.t421_hotel, oTEMP_PosicionGVFilter.t421_hotel)
        //        };

        //        dr = cDblib.DataReader("_PosicionGV_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oPosicionGV = new Models.PosicionGV();
        //            oPosicionGV.t420_idreferencia=Convert.ToInt32(dr["t420_idreferencia"]);
        //            oPosicionGV.t421_fechadesde=Convert.ToDateTime(dr["t421_fechadesde"]);
        //            oPosicionGV.t421_fechahasta=Convert.ToDateTime(dr["t421_fechahasta"]);
        //            oPosicionGV.t421_destino=Convert.ToString(dr["t421_destino"]);
        //            oPosicionGV.t421_comentariopos=Convert.ToString(dr["t421_comentariopos"]);
        //            oPosicionGV.t421_ncdieta=Convert.ToByte(dr["t421_ncdieta"]);
        //            oPosicionGV.t421_nmdieta=Convert.ToByte(dr["t421_nmdieta"]);
        //            oPosicionGV.t421_nedieta=Convert.ToByte(dr["t421_nedieta"]);
        //            oPosicionGV.t421_nadieta=Convert.ToByte(dr["t421_nadieta"]);
        //            oPosicionGV.t421_nkms=Convert.ToInt16(dr["t421_nkms"]);
        //            if(!Convert.IsDBNull(dr["t615_iddesplazamiento"]))
        //                oPosicionGV.t615_iddesplazamiento=Convert.ToInt32(dr["t615_iddesplazamiento"]);
        //            oPosicionGV.t421_peajepark=Convert.ToDecimal(dr["t421_peajepark"]);
        //            oPosicionGV.t421_comida=Convert.ToDecimal(dr["t421_comida"]);
        //            oPosicionGV.t421_transporte=Convert.ToDecimal(dr["t421_transporte"]);
        //            oPosicionGV.t421_hotel=Convert.ToDecimal(dr["t421_hotel"]);

        //            lst.Add(oPosicionGV);

        //        }
        //        return lst;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dr != null)
        //        {
        //            if (!dr.IsClosed) dr.Close();
        //            dr.Dispose();
        //        }
        //    }
        //}

        //#endregion

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
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t421_comentariopos:
					paramName = "@t421_comentariopos";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
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
				case enumDBFields.t421_nedieta:
					paramName = "@t421_nedieta";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.t421_nadieta:
					paramName = "@t421_nadieta";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.t421_nkms:
					paramName = "@t421_nkms";
					paramType = SqlDbType.SmallInt;
					paramSize = 2;
					break;
				case enumDBFields.t615_iddesplazamiento:
					paramName = "@t615_iddesplazamiento";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t421_peajepark:
					paramName = "@t421_peajepark";
					paramType = SqlDbType.SmallMoney;
					paramSize = 4;
					break;
				case enumDBFields.t421_comida:
					paramName = "@t421_comida";
					paramType = SqlDbType.SmallMoney;
					paramSize = 4;
					break;
				case enumDBFields.t421_transporte:
					paramName = "@t421_transporte";
					paramType = SqlDbType.SmallMoney;
					paramSize = 4;
					break;
				case enumDBFields.t421_hotel:
					paramName = "@t421_hotel";
					paramType = SqlDbType.SmallMoney;
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
