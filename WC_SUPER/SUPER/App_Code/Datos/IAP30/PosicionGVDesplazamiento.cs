using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for PosicionGVDesplazamiento
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class PosicionGVDesplazamiento 
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
			t421_comentariopos = 15,
			t615_iddesplazamiento = 16,
			t615_destino = 17,
			t615_fechoraida = 18,
			t615_fechoravuelta = 19
        }

        internal PosicionGVDesplazamiento(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un PosicionGVDesplazamiento
        ///// </summary>
        //internal int Insert(Models.PosicionGVDesplazamiento oPosicionGVDesplazamiento)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[19] {
        //            Param(enumDBFields.t421_idposiciongv, oPosicionGVDesplazamiento.t421_idposiciongv),
        //            Param(enumDBFields.t420_idreferencia, oPosicionGVDesplazamiento.t420_idreferencia),
        //            Param(enumDBFields.t421_fechadesde, oPosicionGVDesplazamiento.t421_fechadesde),
        //            Param(enumDBFields.t421_fechahasta, oPosicionGVDesplazamiento.t421_fechahasta),
        //            Param(enumDBFields.t421_destino, oPosicionGVDesplazamiento.t421_destino),
        //            Param(enumDBFields.t421_ncdieta, oPosicionGVDesplazamiento.t421_ncdieta),
        //            Param(enumDBFields.t421_nmdieta, oPosicionGVDesplazamiento.t421_nmdieta),
        //            Param(enumDBFields.t421_nadieta, oPosicionGVDesplazamiento.t421_nadieta),
        //            Param(enumDBFields.t421_nedieta, oPosicionGVDesplazamiento.t421_nedieta),
        //            Param(enumDBFields.t421_nkms, oPosicionGVDesplazamiento.t421_nkms),
        //            Param(enumDBFields.t421_peajepark, oPosicionGVDesplazamiento.t421_peajepark),
        //            Param(enumDBFields.t421_comida, oPosicionGVDesplazamiento.t421_comida),
        //            Param(enumDBFields.t421_transporte, oPosicionGVDesplazamiento.t421_transporte),
        //            Param(enumDBFields.t421_hotel, oPosicionGVDesplazamiento.t421_hotel),
        //            Param(enumDBFields.t421_comentariopos, oPosicionGVDesplazamiento.t421_comentariopos),
        //            Param(enumDBFields.t615_iddesplazamiento, oPosicionGVDesplazamiento.t615_iddesplazamiento),
        //            Param(enumDBFields.t615_destino, oPosicionGVDesplazamiento.t615_destino),
        //            Param(enumDBFields.t615_fechoraida, oPosicionGVDesplazamiento.t615_fechoraida),
        //            Param(enumDBFields.t615_fechoravuelta, oPosicionGVDesplazamiento.t615_fechoravuelta)
        //        };

        //        return (int)cDblib.Execute("_PosicionGVDesplazamiento_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un PosicionGVDesplazamiento a partir del id
        ///// </summary>
        //internal Models.PosicionGVDesplazamiento Select()
        //{
        //    Models.PosicionGVDesplazamiento oPosicionGVDesplazamiento = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_PosicionGVDesplazamiento_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oPosicionGVDesplazamiento = new Models.PosicionGVDesplazamiento();
        //            oPosicionGVDesplazamiento.t421_idposiciongv=Convert.ToInt32(dr["t421_idposiciongv"]);
        //            oPosicionGVDesplazamiento.t420_idreferencia=Convert.ToInt32(dr["t420_idreferencia"]);
        //            oPosicionGVDesplazamiento.t421_fechadesde=Convert.ToDateTime(dr["t421_fechadesde"]);
        //            oPosicionGVDesplazamiento.t421_fechahasta=Convert.ToDateTime(dr["t421_fechahasta"]);
        //            oPosicionGVDesplazamiento.t421_destino=Convert.ToString(dr["t421_destino"]);
        //            oPosicionGVDesplazamiento.t421_ncdieta=Convert.ToByte(dr["t421_ncdieta"]);
        //            oPosicionGVDesplazamiento.t421_nmdieta=Convert.ToByte(dr["t421_nmdieta"]);
        //            oPosicionGVDesplazamiento.t421_nadieta=Convert.ToByte(dr["t421_nadieta"]);
        //            oPosicionGVDesplazamiento.t421_nedieta=Convert.ToByte(dr["t421_nedieta"]);
        //            oPosicionGVDesplazamiento.t421_nkms=Convert.ToInt16(dr["t421_nkms"]);
        //            oPosicionGVDesplazamiento.t421_peajepark=Convert.ToDecimal(dr["t421_peajepark"]);
        //            oPosicionGVDesplazamiento.t421_comida=Convert.ToDecimal(dr["t421_comida"]);
        //            oPosicionGVDesplazamiento.t421_transporte=Convert.ToDecimal(dr["t421_transporte"]);
        //            oPosicionGVDesplazamiento.t421_hotel=Convert.ToDecimal(dr["t421_hotel"]);
        //            oPosicionGVDesplazamiento.t421_comentariopos=Convert.ToString(dr["t421_comentariopos"]);
        //            if(!Convert.IsDBNull(dr["t615_iddesplazamiento"]))
        //                oPosicionGVDesplazamiento.t615_iddesplazamiento=Convert.ToInt32(dr["t615_iddesplazamiento"]);
        //            if(!Convert.IsDBNull(dr["t615_destino"]))
        //                oPosicionGVDesplazamiento.t615_destino=Convert.ToString(dr["t615_destino"]);
        //            if(!Convert.IsDBNull(dr["t615_fechoraida"]))
        //                oPosicionGVDesplazamiento.t615_fechoraida=Convert.ToDateTime(dr["t615_fechoraida"]);
        //            if(!Convert.IsDBNull(dr["t615_fechoravuelta"]))
        //                oPosicionGVDesplazamiento.t615_fechoravuelta=Convert.ToDateTime(dr["t615_fechoravuelta"]);

        //        }
        //        return oPosicionGVDesplazamiento;
				
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
		
        ///// <summary>
        ///// Actualiza un PosicionGVDesplazamiento a partir del id
        ///// </summary>
        //internal int Update(Models.PosicionGVDesplazamiento oPosicionGVDesplazamiento)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[19] {
        //            Param(enumDBFields.t421_idposiciongv, oPosicionGVDesplazamiento.t421_idposiciongv),
        //            Param(enumDBFields.t420_idreferencia, oPosicionGVDesplazamiento.t420_idreferencia),
        //            Param(enumDBFields.t421_fechadesde, oPosicionGVDesplazamiento.t421_fechadesde),
        //            Param(enumDBFields.t421_fechahasta, oPosicionGVDesplazamiento.t421_fechahasta),
        //            Param(enumDBFields.t421_destino, oPosicionGVDesplazamiento.t421_destino),
        //            Param(enumDBFields.t421_ncdieta, oPosicionGVDesplazamiento.t421_ncdieta),
        //            Param(enumDBFields.t421_nmdieta, oPosicionGVDesplazamiento.t421_nmdieta),
        //            Param(enumDBFields.t421_nadieta, oPosicionGVDesplazamiento.t421_nadieta),
        //            Param(enumDBFields.t421_nedieta, oPosicionGVDesplazamiento.t421_nedieta),
        //            Param(enumDBFields.t421_nkms, oPosicionGVDesplazamiento.t421_nkms),
        //            Param(enumDBFields.t421_peajepark, oPosicionGVDesplazamiento.t421_peajepark),
        //            Param(enumDBFields.t421_comida, oPosicionGVDesplazamiento.t421_comida),
        //            Param(enumDBFields.t421_transporte, oPosicionGVDesplazamiento.t421_transporte),
        //            Param(enumDBFields.t421_hotel, oPosicionGVDesplazamiento.t421_hotel),
        //            Param(enumDBFields.t421_comentariopos, oPosicionGVDesplazamiento.t421_comentariopos),
        //            Param(enumDBFields.t615_iddesplazamiento, oPosicionGVDesplazamiento.t615_iddesplazamiento),
        //            Param(enumDBFields.t615_destino, oPosicionGVDesplazamiento.t615_destino),
        //            Param(enumDBFields.t615_fechoraida, oPosicionGVDesplazamiento.t615_fechoraida),
        //            Param(enumDBFields.t615_fechoravuelta, oPosicionGVDesplazamiento.t615_fechoravuelta)
        //        };
                           
        //        return (int)cDblib.Execute("_PosicionGVDesplazamiento_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un PosicionGVDesplazamiento a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_PosicionGVDesplazamiento_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los PosicionGVDesplazamiento
        ///// </summary>
        //internal List<Models.PosicionGVDesplazamiento> Catalogo(Models.PosicionGVDesplazamiento oPosicionGVDesplazamientoFilter)
        //{
        //    Models.PosicionGVDesplazamiento oPosicionGVDesplazamiento = null;
        //    List<Models.PosicionGVDesplazamiento> lst = new List<Models.PosicionGVDesplazamiento>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[19] {
        //            Param(enumDBFields.t421_idposiciongv, oTEMP_PosicionGVDesplazamientoFilter.t421_idposiciongv),
        //            Param(enumDBFields.t420_idreferencia, oTEMP_PosicionGVDesplazamientoFilter.t420_idreferencia),
        //            Param(enumDBFields.t421_fechadesde, oTEMP_PosicionGVDesplazamientoFilter.t421_fechadesde),
        //            Param(enumDBFields.t421_fechahasta, oTEMP_PosicionGVDesplazamientoFilter.t421_fechahasta),
        //            Param(enumDBFields.t421_destino, oTEMP_PosicionGVDesplazamientoFilter.t421_destino),
        //            Param(enumDBFields.t421_ncdieta, oTEMP_PosicionGVDesplazamientoFilter.t421_ncdieta),
        //            Param(enumDBFields.t421_nmdieta, oTEMP_PosicionGVDesplazamientoFilter.t421_nmdieta),
        //            Param(enumDBFields.t421_nadieta, oTEMP_PosicionGVDesplazamientoFilter.t421_nadieta),
        //            Param(enumDBFields.t421_nedieta, oTEMP_PosicionGVDesplazamientoFilter.t421_nedieta),
        //            Param(enumDBFields.t421_nkms, oTEMP_PosicionGVDesplazamientoFilter.t421_nkms),
        //            Param(enumDBFields.t421_peajepark, oTEMP_PosicionGVDesplazamientoFilter.t421_peajepark),
        //            Param(enumDBFields.t421_comida, oTEMP_PosicionGVDesplazamientoFilter.t421_comida),
        //            Param(enumDBFields.t421_transporte, oTEMP_PosicionGVDesplazamientoFilter.t421_transporte),
        //            Param(enumDBFields.t421_hotel, oTEMP_PosicionGVDesplazamientoFilter.t421_hotel),
        //            Param(enumDBFields.t421_comentariopos, oTEMP_PosicionGVDesplazamientoFilter.t421_comentariopos),
        //            Param(enumDBFields.t615_iddesplazamiento, oTEMP_PosicionGVDesplazamientoFilter.t615_iddesplazamiento),
        //            Param(enumDBFields.t615_destino, oTEMP_PosicionGVDesplazamientoFilter.t615_destino),
        //            Param(enumDBFields.t615_fechoraida, oTEMP_PosicionGVDesplazamientoFilter.t615_fechoraida),
        //            Param(enumDBFields.t615_fechoravuelta, oTEMP_PosicionGVDesplazamientoFilter.t615_fechoravuelta)
        //        };

        //        dr = cDblib.DataReader("_PosicionGVDesplazamiento_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oPosicionGVDesplazamiento = new Models.PosicionGVDesplazamiento();
        //            oPosicionGVDesplazamiento.t421_idposiciongv=Convert.ToInt32(dr["t421_idposiciongv"]);
        //            oPosicionGVDesplazamiento.t420_idreferencia=Convert.ToInt32(dr["t420_idreferencia"]);
        //            oPosicionGVDesplazamiento.t421_fechadesde=Convert.ToDateTime(dr["t421_fechadesde"]);
        //            oPosicionGVDesplazamiento.t421_fechahasta=Convert.ToDateTime(dr["t421_fechahasta"]);
        //            oPosicionGVDesplazamiento.t421_destino=Convert.ToString(dr["t421_destino"]);
        //            oPosicionGVDesplazamiento.t421_ncdieta=Convert.ToByte(dr["t421_ncdieta"]);
        //            oPosicionGVDesplazamiento.t421_nmdieta=Convert.ToByte(dr["t421_nmdieta"]);
        //            oPosicionGVDesplazamiento.t421_nadieta=Convert.ToByte(dr["t421_nadieta"]);
        //            oPosicionGVDesplazamiento.t421_nedieta=Convert.ToByte(dr["t421_nedieta"]);
        //            oPosicionGVDesplazamiento.t421_nkms=Convert.ToInt16(dr["t421_nkms"]);
        //            oPosicionGVDesplazamiento.t421_peajepark=Convert.ToDecimal(dr["t421_peajepark"]);
        //            oPosicionGVDesplazamiento.t421_comida=Convert.ToDecimal(dr["t421_comida"]);
        //            oPosicionGVDesplazamiento.t421_transporte=Convert.ToDecimal(dr["t421_transporte"]);
        //            oPosicionGVDesplazamiento.t421_hotel=Convert.ToDecimal(dr["t421_hotel"]);
        //            oPosicionGVDesplazamiento.t421_comentariopos=Convert.ToString(dr["t421_comentariopos"]);
        //            if(!Convert.IsDBNull(dr["t615_iddesplazamiento"]))
        //                oPosicionGVDesplazamiento.t615_iddesplazamiento=Convert.ToInt32(dr["t615_iddesplazamiento"]);
        //            if(!Convert.IsDBNull(dr["t615_destino"]))
        //                oPosicionGVDesplazamiento.t615_destino=Convert.ToString(dr["t615_destino"]);
        //            if(!Convert.IsDBNull(dr["t615_fechoraida"]))
        //                oPosicionGVDesplazamiento.t615_fechoraida=Convert.ToDateTime(dr["t615_fechoraida"]);
        //            if(!Convert.IsDBNull(dr["t615_fechoravuelta"]))
        //                oPosicionGVDesplazamiento.t615_fechoravuelta=Convert.ToDateTime(dr["t615_fechoravuelta"]);

        //            lst.Add(oPosicionGVDesplazamiento);

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
					paramSize = 8;
					break;
				case enumDBFields.t421_fechahasta:
					paramName = "@t421_fechahasta";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t421_destino:
					paramName = "@t421_destino";
					paramType = SqlDbType.VarChar;
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
				case enumDBFields.t421_comentariopos:
					paramName = "@t421_comentariopos";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t615_iddesplazamiento:
					paramName = "@t615_iddesplazamiento";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t615_destino:
					paramName = "@t615_destino";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t615_fechoraida:
					paramName = "@t615_fechoraida";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t615_fechoravuelta:
					paramName = "@t615_fechoravuelta";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
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
