using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for CabeceraGV
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class CabeceraGV 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t420_idreferencia = 1,
			t431_idestado = 2,
			t420_concepto = 3,
			t001_idficepi_solicitante = 4,
			t314_idusuario_interesado = 5,
			t423_idmotivo = 6,
			t420_justificantes = 7,
			t305_idproyectosubnodo = 8,
			t422_idmoneda = 9,
			t420_comentarionota = 10,
			t420_anotaciones = 11,
			t420_importeanticipo = 12,
			t420_fanticipo = 13,
			t420_lugaranticipo = 14,
			t420_importedevolucion = 15,
			t420_fdevolucion = 16,
			t420_lugardevolucion = 17,
			t420_aclaracionesanticipo = 18,
			t420_pagadotransporte = 19,
			t420_pagadohotel = 20,
			t420_pagadootros = 21,
			t420_aclaracionepagado = 22,
			t313_idempresa = 23,
			t007_idterrfis = 24,
			t420_impdico = 25,
			t420_impmdco = 26,
			t420_impalco = 27,
			t420_impkmco = 28,
			t420_impdeco = 29,
			t420_impdiex = 30,
			t420_impmdex = 31,
			t420_impalex = 32,
			t420_impkmex = 33,
			t420_impdeex = 34,
			t010_idoficina = 35,
			t420_idreferencia_lote = 36,
			t175_idcc = 37
        }

        internal CabeceraGV(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
        #region funciones publicas
        /// <summary>
        /// Inserta un CabeceraGV
        /// </summary>
        internal int Insert(Models.CabeceraGV oCabeceraGV)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[35] {
                    Param(enumDBFields.t431_idestado, oCabeceraGV.t431_idestado),
                    Param(enumDBFields.t420_concepto, oCabeceraGV.t420_concepto),
                    Param(enumDBFields.t001_idficepi_solicitante, oCabeceraGV.t001_idficepi_solicitante),
                    Param(enumDBFields.t314_idusuario_interesado, oCabeceraGV.t314_idusuario_interesado),
                    Param(enumDBFields.t423_idmotivo, oCabeceraGV.t423_idmotivo),
                    Param(enumDBFields.t420_justificantes, oCabeceraGV.t420_justificantes),
                    Param(enumDBFields.t305_idproyectosubnodo, oCabeceraGV.t305_idproyectosubnodo),
                    Param(enumDBFields.t422_idmoneda, oCabeceraGV.t422_idmoneda),
                    Param(enumDBFields.t420_comentarionota, oCabeceraGV.t420_comentarionota),
                    Param(enumDBFields.t420_anotaciones, oCabeceraGV.t420_anotaciones),
                    Param(enumDBFields.t420_importeanticipo, oCabeceraGV.t420_importeanticipo),
                    Param(enumDBFields.t420_fanticipo, oCabeceraGV.t420_fanticipo),
                    Param(enumDBFields.t420_lugaranticipo, oCabeceraGV.t420_lugaranticipo),
                    Param(enumDBFields.t420_importedevolucion, oCabeceraGV.t420_importedevolucion),
                    Param(enumDBFields.t420_fdevolucion, oCabeceraGV.t420_fdevolucion),
                    Param(enumDBFields.t420_lugardevolucion, oCabeceraGV.t420_lugardevolucion),
                    Param(enumDBFields.t420_aclaracionesanticipo, oCabeceraGV.t420_aclaracionesanticipo),
                    Param(enumDBFields.t420_pagadotransporte, oCabeceraGV.t420_pagadotransporte),
                    Param(enumDBFields.t420_pagadohotel, oCabeceraGV.t420_pagadohotel),
                    Param(enumDBFields.t420_pagadootros, oCabeceraGV.t420_pagadootros),
                    Param(enumDBFields.t420_aclaracionepagado, oCabeceraGV.t420_aclaracionepagado),
                    Param(enumDBFields.t313_idempresa, oCabeceraGV.t313_idempresa),
                    Param(enumDBFields.t007_idterrfis, oCabeceraGV.t007_idterrfis),
                    Param(enumDBFields.t420_impdico, oCabeceraGV.t420_impdico),
                    Param(enumDBFields.t420_impmdco, oCabeceraGV.t420_impmdco),
                    Param(enumDBFields.t420_impalco, oCabeceraGV.t420_impalco),
                    Param(enumDBFields.t420_impkmco, oCabeceraGV.t420_impkmco),
                    Param(enumDBFields.t420_impdeco, oCabeceraGV.t420_impdeco),
                    Param(enumDBFields.t420_impdiex, oCabeceraGV.t420_impdiex),
                    Param(enumDBFields.t420_impmdex, oCabeceraGV.t420_impmdex),
                    Param(enumDBFields.t420_impalex, oCabeceraGV.t420_impalex),
                    Param(enumDBFields.t420_impkmex, oCabeceraGV.t420_impkmex),
                    Param(enumDBFields.t420_impdeex, oCabeceraGV.t420_impdeex),
                    Param(enumDBFields.t010_idoficina, oCabeceraGV.t010_idoficina),
                    Param(enumDBFields.t420_idreferencia_lote, oCabeceraGV.t420_idreferencia_lote)
                    //,Param(enumDBFields.t175_idcc, oCabeceraGV.t175_idcc)
                };

                //return (int)cDblib.Execute("GVT_CABECERAGV_INS", dbparams);
                return (int)cDblib.ExecuteScalar("GVT_CABECERAGV_INS", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void GestionarAutorresponsabilidad(int t420_idreferencia)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t420_idreferencia, t420_idreferencia)
                };

                cDblib.ExecuteScalar("GVT_AUTORRESPONSABILIDAD_APROBAR", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///// <summary>
        ///// Obtiene un CabeceraGV a partir del id
        ///// </summary>
        //internal Models.CabeceraGV Select()
        //{
        //    Models.CabeceraGV oCabeceraGV = null;
        //    IDataReader dr = null;

        //    try
        //    {


        //        dr = cDblib.DataReader("_CabeceraGV_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oCabeceraGV = new Models.CabeceraGV();
        //            oCabeceraGV.t420_idreferencia=Convert.ToInt32(dr["t420_idreferencia"]);
        //            oCabeceraGV.t431_idestado=Convert.ToString(dr["t431_idestado"]);
        //            oCabeceraGV.t420_concepto=Convert.ToString(dr["t420_concepto"]);
        //            oCabeceraGV.t001_idficepi_solicitante=Convert.ToInt32(dr["t001_idficepi_solicitante"]);
        //            oCabeceraGV.t314_idusuario_interesado=Convert.ToInt32(dr["t314_idusuario_interesado"]);
        //            oCabeceraGV.t423_idmotivo=Convert.ToByte(dr["t423_idmotivo"]);
        //            if(!Convert.IsDBNull(dr["t420_justificantes"]))
        //                oCabeceraGV.t420_justificantes=Convert.ToBoolean(dr["t420_justificantes"]);
        //            if(!Convert.IsDBNull(dr["t305_idproyectosubnodo"]))
        //                oCabeceraGV.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oCabeceraGV.t422_idmoneda=Convert.ToString(dr["t422_idmoneda"]);
        //            oCabeceraGV.t420_comentarionota=Convert.ToString(dr["t420_comentarionota"]);
        //            oCabeceraGV.t420_anotaciones=Convert.ToString(dr["t420_anotaciones"]);
        //            oCabeceraGV.t420_importeanticipo=Convert.ToDecimal(dr["t420_importeanticipo"]);
        //            if(!Convert.IsDBNull(dr["t420_fanticipo"]))
        //                oCabeceraGV.t420_fanticipo=Convert.ToDateTime(dr["t420_fanticipo"]);
        //            oCabeceraGV.t420_lugaranticipo=Convert.ToString(dr["t420_lugaranticipo"]);
        //            oCabeceraGV.t420_importedevolucion=Convert.ToDecimal(dr["t420_importedevolucion"]);
        //            if(!Convert.IsDBNull(dr["t420_fdevolucion"]))
        //                oCabeceraGV.t420_fdevolucion=Convert.ToDateTime(dr["t420_fdevolucion"]);
        //            oCabeceraGV.t420_lugardevolucion=Convert.ToString(dr["t420_lugardevolucion"]);
        //            oCabeceraGV.t420_aclaracionesanticipo=Convert.ToString(dr["t420_aclaracionesanticipo"]);
        //            oCabeceraGV.t420_pagadotransporte=Convert.ToDecimal(dr["t420_pagadotransporte"]);
        //            oCabeceraGV.t420_pagadohotel=Convert.ToDecimal(dr["t420_pagadohotel"]);
        //            oCabeceraGV.t420_pagadootros=Convert.ToDecimal(dr["t420_pagadootros"]);
        //            oCabeceraGV.t420_aclaracionepagado=Convert.ToString(dr["t420_aclaracionepagado"]);
        //            oCabeceraGV.t313_idempresa=Convert.ToInt32(dr["t313_idempresa"]);
        //            oCabeceraGV.t007_idterrfis=Convert.ToByte(dr["t007_idterrfis"]);
        //            oCabeceraGV.t420_impdico=Convert.ToDecimal(dr["t420_impdico"]);
        //            oCabeceraGV.t420_impmdco=Convert.ToDecimal(dr["t420_impmdco"]);
        //            oCabeceraGV.t420_impalco=Convert.ToDecimal(dr["t420_impalco"]);
        //            oCabeceraGV.t420_impkmco=Convert.ToDecimal(dr["t420_impkmco"]);
        //            oCabeceraGV.t420_impdeco=Convert.ToDecimal(dr["t420_impdeco"]);
        //            oCabeceraGV.t420_impdiex=Convert.ToDecimal(dr["t420_impdiex"]);
        //            oCabeceraGV.t420_impmdex=Convert.ToDecimal(dr["t420_impmdex"]);
        //            oCabeceraGV.t420_impalex=Convert.ToDecimal(dr["t420_impalex"]);
        //            oCabeceraGV.t420_impkmex=Convert.ToDecimal(dr["t420_impkmex"]);
        //            oCabeceraGV.t420_impdeex=Convert.ToDecimal(dr["t420_impdeex"]);
        //            if(!Convert.IsDBNull(dr["t010_idoficina"]))
        //                oCabeceraGV.t010_idoficina=Convert.ToInt16(dr["t010_idoficina"]);
        //            if(!Convert.IsDBNull(dr["t420_idreferencia_lote"]))
        //                oCabeceraGV.t420_idreferencia_lote=Convert.ToInt32(dr["t420_idreferencia_lote"]);
        //            if(!Convert.IsDBNull(dr["t175_idcc"]))
        //                oCabeceraGV.t175_idcc=Convert.ToString(dr["t175_idcc"]);

        //        }
        //        return oCabeceraGV;

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
        ///// Actualiza un CabeceraGV a partir del id
        ///// </summary>
        //internal int Update(Models.CabeceraGV oCabeceraGV)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[37] {
        //            Param(enumDBFields.t420_idreferencia, oCabeceraGV.t420_idreferencia),
        //            Param(enumDBFields.t431_idestado, oCabeceraGV.t431_idestado),
        //            Param(enumDBFields.t420_concepto, oCabeceraGV.t420_concepto),
        //            Param(enumDBFields.t001_idficepi_solicitante, oCabeceraGV.t001_idficepi_solicitante),
        //            Param(enumDBFields.t314_idusuario_interesado, oCabeceraGV.t314_idusuario_interesado),
        //            Param(enumDBFields.t423_idmotivo, oCabeceraGV.t423_idmotivo),
        //            Param(enumDBFields.t420_justificantes, oCabeceraGV.t420_justificantes),
        //            Param(enumDBFields.t305_idproyectosubnodo, oCabeceraGV.t305_idproyectosubnodo),
        //            Param(enumDBFields.t422_idmoneda, oCabeceraGV.t422_idmoneda),
        //            Param(enumDBFields.t420_comentarionota, oCabeceraGV.t420_comentarionota),
        //            Param(enumDBFields.t420_anotaciones, oCabeceraGV.t420_anotaciones),
        //            Param(enumDBFields.t420_importeanticipo, oCabeceraGV.t420_importeanticipo),
        //            Param(enumDBFields.t420_fanticipo, oCabeceraGV.t420_fanticipo),
        //            Param(enumDBFields.t420_lugaranticipo, oCabeceraGV.t420_lugaranticipo),
        //            Param(enumDBFields.t420_importedevolucion, oCabeceraGV.t420_importedevolucion),
        //            Param(enumDBFields.t420_fdevolucion, oCabeceraGV.t420_fdevolucion),
        //            Param(enumDBFields.t420_lugardevolucion, oCabeceraGV.t420_lugardevolucion),
        //            Param(enumDBFields.t420_aclaracionesanticipo, oCabeceraGV.t420_aclaracionesanticipo),
        //            Param(enumDBFields.t420_pagadotransporte, oCabeceraGV.t420_pagadotransporte),
        //            Param(enumDBFields.t420_pagadohotel, oCabeceraGV.t420_pagadohotel),
        //            Param(enumDBFields.t420_pagadootros, oCabeceraGV.t420_pagadootros),
        //            Param(enumDBFields.t420_aclaracionepagado, oCabeceraGV.t420_aclaracionepagado),
        //            Param(enumDBFields.t313_idempresa, oCabeceraGV.t313_idempresa),
        //            Param(enumDBFields.t007_idterrfis, oCabeceraGV.t007_idterrfis),
        //            Param(enumDBFields.t420_impdico, oCabeceraGV.t420_impdico),
        //            Param(enumDBFields.t420_impmdco, oCabeceraGV.t420_impmdco),
        //            Param(enumDBFields.t420_impalco, oCabeceraGV.t420_impalco),
        //            Param(enumDBFields.t420_impkmco, oCabeceraGV.t420_impkmco),
        //            Param(enumDBFields.t420_impdeco, oCabeceraGV.t420_impdeco),
        //            Param(enumDBFields.t420_impdiex, oCabeceraGV.t420_impdiex),
        //            Param(enumDBFields.t420_impmdex, oCabeceraGV.t420_impmdex),
        //            Param(enumDBFields.t420_impalex, oCabeceraGV.t420_impalex),
        //            Param(enumDBFields.t420_impkmex, oCabeceraGV.t420_impkmex),
        //            Param(enumDBFields.t420_impdeex, oCabeceraGV.t420_impdeex),
        //            Param(enumDBFields.t010_idoficina, oCabeceraGV.t010_idoficina),
        //            Param(enumDBFields.t420_idreferencia_lote, oCabeceraGV.t420_idreferencia_lote),
        //            Param(enumDBFields.t175_idcc, oCabeceraGV.t175_idcc)
        //        };

        //        return (int)cDblib.Execute("_CabeceraGV_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Elimina un CabeceraGV a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {


        //        return (int)cDblib.Execute("_CabeceraGV_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los CabeceraGV
        ///// </summary>
        //internal List<Models.CabeceraGV> Catalogo(Models.CabeceraGV oCabeceraGVFilter)
        //{
        //    Models.CabeceraGV oCabeceraGV = null;
        //    List<Models.CabeceraGV> lst = new List<Models.CabeceraGV>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[37] {
        //            Param(enumDBFields.t420_idreferencia, oTEMP_CabeceraGVFilter.t420_idreferencia),
        //            Param(enumDBFields.t431_idestado, oTEMP_CabeceraGVFilter.t431_idestado),
        //            Param(enumDBFields.t420_concepto, oTEMP_CabeceraGVFilter.t420_concepto),
        //            Param(enumDBFields.t001_idficepi_solicitante, oTEMP_CabeceraGVFilter.t001_idficepi_solicitante),
        //            Param(enumDBFields.t314_idusuario_interesado, oTEMP_CabeceraGVFilter.t314_idusuario_interesado),
        //            Param(enumDBFields.t423_idmotivo, oTEMP_CabeceraGVFilter.t423_idmotivo),
        //            Param(enumDBFields.t420_justificantes, oTEMP_CabeceraGVFilter.t420_justificantes),
        //            Param(enumDBFields.t305_idproyectosubnodo, oTEMP_CabeceraGVFilter.t305_idproyectosubnodo),
        //            Param(enumDBFields.t422_idmoneda, oTEMP_CabeceraGVFilter.t422_idmoneda),
        //            Param(enumDBFields.t420_comentarionota, oTEMP_CabeceraGVFilter.t420_comentarionota),
        //            Param(enumDBFields.t420_anotaciones, oTEMP_CabeceraGVFilter.t420_anotaciones),
        //            Param(enumDBFields.t420_importeanticipo, oTEMP_CabeceraGVFilter.t420_importeanticipo),
        //            Param(enumDBFields.t420_fanticipo, oTEMP_CabeceraGVFilter.t420_fanticipo),
        //            Param(enumDBFields.t420_lugaranticipo, oTEMP_CabeceraGVFilter.t420_lugaranticipo),
        //            Param(enumDBFields.t420_importedevolucion, oTEMP_CabeceraGVFilter.t420_importedevolucion),
        //            Param(enumDBFields.t420_fdevolucion, oTEMP_CabeceraGVFilter.t420_fdevolucion),
        //            Param(enumDBFields.t420_lugardevolucion, oTEMP_CabeceraGVFilter.t420_lugardevolucion),
        //            Param(enumDBFields.t420_aclaracionesanticipo, oTEMP_CabeceraGVFilter.t420_aclaracionesanticipo),
        //            Param(enumDBFields.t420_pagadotransporte, oTEMP_CabeceraGVFilter.t420_pagadotransporte),
        //            Param(enumDBFields.t420_pagadohotel, oTEMP_CabeceraGVFilter.t420_pagadohotel),
        //            Param(enumDBFields.t420_pagadootros, oTEMP_CabeceraGVFilter.t420_pagadootros),
        //            Param(enumDBFields.t420_aclaracionepagado, oTEMP_CabeceraGVFilter.t420_aclaracionepagado),
        //            Param(enumDBFields.t313_idempresa, oTEMP_CabeceraGVFilter.t313_idempresa),
        //            Param(enumDBFields.t007_idterrfis, oTEMP_CabeceraGVFilter.t007_idterrfis),
        //            Param(enumDBFields.t420_impdico, oTEMP_CabeceraGVFilter.t420_impdico),
        //            Param(enumDBFields.t420_impmdco, oTEMP_CabeceraGVFilter.t420_impmdco),
        //            Param(enumDBFields.t420_impalco, oTEMP_CabeceraGVFilter.t420_impalco),
        //            Param(enumDBFields.t420_impkmco, oTEMP_CabeceraGVFilter.t420_impkmco),
        //            Param(enumDBFields.t420_impdeco, oTEMP_CabeceraGVFilter.t420_impdeco),
        //            Param(enumDBFields.t420_impdiex, oTEMP_CabeceraGVFilter.t420_impdiex),
        //            Param(enumDBFields.t420_impmdex, oTEMP_CabeceraGVFilter.t420_impmdex),
        //            Param(enumDBFields.t420_impalex, oTEMP_CabeceraGVFilter.t420_impalex),
        //            Param(enumDBFields.t420_impkmex, oTEMP_CabeceraGVFilter.t420_impkmex),
        //            Param(enumDBFields.t420_impdeex, oTEMP_CabeceraGVFilter.t420_impdeex),
        //            Param(enumDBFields.t010_idoficina, oTEMP_CabeceraGVFilter.t010_idoficina),
        //            Param(enumDBFields.t420_idreferencia_lote, oTEMP_CabeceraGVFilter.t420_idreferencia_lote),
        //            Param(enumDBFields.t175_idcc, oTEMP_CabeceraGVFilter.t175_idcc)
        //        };

        //        dr = cDblib.DataReader("_CabeceraGV_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oCabeceraGV = new Models.CabeceraGV();
        //            oCabeceraGV.t420_idreferencia=Convert.ToInt32(dr["t420_idreferencia"]);
        //            oCabeceraGV.t431_idestado=Convert.ToString(dr["t431_idestado"]);
        //            oCabeceraGV.t420_concepto=Convert.ToString(dr["t420_concepto"]);
        //            oCabeceraGV.t001_idficepi_solicitante=Convert.ToInt32(dr["t001_idficepi_solicitante"]);
        //            oCabeceraGV.t314_idusuario_interesado=Convert.ToInt32(dr["t314_idusuario_interesado"]);
        //            oCabeceraGV.t423_idmotivo=Convert.ToByte(dr["t423_idmotivo"]);
        //            if(!Convert.IsDBNull(dr["t420_justificantes"]))
        //                oCabeceraGV.t420_justificantes=Convert.ToBoolean(dr["t420_justificantes"]);
        //            if(!Convert.IsDBNull(dr["t305_idproyectosubnodo"]))
        //                oCabeceraGV.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oCabeceraGV.t422_idmoneda=Convert.ToString(dr["t422_idmoneda"]);
        //            oCabeceraGV.t420_comentarionota=Convert.ToString(dr["t420_comentarionota"]);
        //            oCabeceraGV.t420_anotaciones=Convert.ToString(dr["t420_anotaciones"]);
        //            oCabeceraGV.t420_importeanticipo=Convert.ToDecimal(dr["t420_importeanticipo"]);
        //            if(!Convert.IsDBNull(dr["t420_fanticipo"]))
        //                oCabeceraGV.t420_fanticipo=Convert.ToDateTime(dr["t420_fanticipo"]);
        //            oCabeceraGV.t420_lugaranticipo=Convert.ToString(dr["t420_lugaranticipo"]);
        //            oCabeceraGV.t420_importedevolucion=Convert.ToDecimal(dr["t420_importedevolucion"]);
        //            if(!Convert.IsDBNull(dr["t420_fdevolucion"]))
        //                oCabeceraGV.t420_fdevolucion=Convert.ToDateTime(dr["t420_fdevolucion"]);
        //            oCabeceraGV.t420_lugardevolucion=Convert.ToString(dr["t420_lugardevolucion"]);
        //            oCabeceraGV.t420_aclaracionesanticipo=Convert.ToString(dr["t420_aclaracionesanticipo"]);
        //            oCabeceraGV.t420_pagadotransporte=Convert.ToDecimal(dr["t420_pagadotransporte"]);
        //            oCabeceraGV.t420_pagadohotel=Convert.ToDecimal(dr["t420_pagadohotel"]);
        //            oCabeceraGV.t420_pagadootros=Convert.ToDecimal(dr["t420_pagadootros"]);
        //            oCabeceraGV.t420_aclaracionepagado=Convert.ToString(dr["t420_aclaracionepagado"]);
        //            oCabeceraGV.t313_idempresa=Convert.ToInt32(dr["t313_idempresa"]);
        //            oCabeceraGV.t007_idterrfis=Convert.ToByte(dr["t007_idterrfis"]);
        //            oCabeceraGV.t420_impdico=Convert.ToDecimal(dr["t420_impdico"]);
        //            oCabeceraGV.t420_impmdco=Convert.ToDecimal(dr["t420_impmdco"]);
        //            oCabeceraGV.t420_impalco=Convert.ToDecimal(dr["t420_impalco"]);
        //            oCabeceraGV.t420_impkmco=Convert.ToDecimal(dr["t420_impkmco"]);
        //            oCabeceraGV.t420_impdeco=Convert.ToDecimal(dr["t420_impdeco"]);
        //            oCabeceraGV.t420_impdiex=Convert.ToDecimal(dr["t420_impdiex"]);
        //            oCabeceraGV.t420_impmdex=Convert.ToDecimal(dr["t420_impmdex"]);
        //            oCabeceraGV.t420_impalex=Convert.ToDecimal(dr["t420_impalex"]);
        //            oCabeceraGV.t420_impkmex=Convert.ToDecimal(dr["t420_impkmex"]);
        //            oCabeceraGV.t420_impdeex=Convert.ToDecimal(dr["t420_impdeex"]);
        //            if(!Convert.IsDBNull(dr["t010_idoficina"]))
        //                oCabeceraGV.t010_idoficina=Convert.ToInt16(dr["t010_idoficina"]);
        //            if(!Convert.IsDBNull(dr["t420_idreferencia_lote"]))
        //                oCabeceraGV.t420_idreferencia_lote=Convert.ToInt32(dr["t420_idreferencia_lote"]);
        //            if(!Convert.IsDBNull(dr["t175_idcc"]))
        //                oCabeceraGV.t175_idcc=Convert.ToString(dr["t175_idcc"]);

        //            lst.Add(oCabeceraGV);

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
				case enumDBFields.t420_idreferencia:
					paramName = "@t420_idreferencia";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t431_idestado:
					paramName = "@t431_idestado";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.t420_concepto:
					paramName = "@t420_concepto";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t001_idficepi_solicitante:
					paramName = "@t001_idficepi_solicitante";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t314_idusuario_interesado:
					paramName = "@t314_idusuario_interesado";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t423_idmotivo:
					paramName = "@t423_idmotivo";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.t420_justificantes:
					paramName = "@t420_justificantes";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t305_idproyectosubnodo:
					paramName = "@t305_idproyectosubnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t422_idmoneda:
					paramName = "@t422_idmoneda";
					paramType = SqlDbType.VarChar;
					paramSize = 5;
					break;
				case enumDBFields.t420_comentarionota:
					paramName = "@t420_comentarionota";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t420_anotaciones:
					paramName = "@t420_anotaciones";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t420_importeanticipo:
					paramName = "@t420_importeanticipo";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.t420_fanticipo:
					paramName = "@t420_fanticipo";
					paramType = SqlDbType.DateTime;
					paramSize = 4;
					break;
				case enumDBFields.t420_lugaranticipo:
					paramName = "@t420_lugaranticipo";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t420_importedevolucion:
					paramName = "@t420_importedevolucion";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.t420_fdevolucion:
					paramName = "@t420_fdevolucion";
					paramType = SqlDbType.DateTime;
					paramSize = 4;
					break;
				case enumDBFields.t420_lugardevolucion:
					paramName = "@t420_lugardevolucion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t420_aclaracionesanticipo:
					paramName = "@t420_aclaracionesanticipo";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t420_pagadotransporte:
					paramName = "@t420_pagadotransporte";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.t420_pagadohotel:
					paramName = "@t420_pagadohotel";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.t420_pagadootros:
					paramName = "@t420_pagadootros";
					paramType = SqlDbType.Money;
					paramSize = 8;
					break;
				case enumDBFields.t420_aclaracionepagado:
					paramName = "@t420_aclaracionepagado";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.t313_idempresa:
					paramName = "@t313_idempresa";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t007_idterrfis:
					paramName = "@t007_idterrfis";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.t420_impdico:
					paramName = "@t420_impdico";
					paramType = SqlDbType.SmallMoney;
					paramSize = 4;
					break;
				case enumDBFields.t420_impmdco:
					paramName = "@t420_impmdco";
					paramType = SqlDbType.SmallMoney;
					paramSize = 4;
					break;
				case enumDBFields.t420_impalco:
					paramName = "@t420_impalco";
					paramType = SqlDbType.SmallMoney;
					paramSize = 4;
					break;
				case enumDBFields.t420_impkmco:
					paramName = "@t420_impkmco";
					paramType = SqlDbType.SmallMoney;
					paramSize = 4;
					break;
				case enumDBFields.t420_impdeco:
					paramName = "@t420_impdeco";
					paramType = SqlDbType.SmallMoney;
					paramSize = 4;
					break;
				case enumDBFields.t420_impdiex:
					paramName = "@t420_impdiex";
					paramType = SqlDbType.SmallMoney;
					paramSize = 4;
					break;
				case enumDBFields.t420_impmdex:
					paramName = "@t420_impmdex";
					paramType = SqlDbType.SmallMoney;
					paramSize = 4;
					break;
				case enumDBFields.t420_impalex:
					paramName = "@t420_impalex";
					paramType = SqlDbType.SmallMoney;
					paramSize = 4;
					break;
				case enumDBFields.t420_impkmex:
					paramName = "@t420_impkmex";
					paramType = SqlDbType.SmallMoney;
					paramSize = 4;
					break;
				case enumDBFields.t420_impdeex:
					paramName = "@t420_impdeex";
					paramType = SqlDbType.SmallMoney;
					paramSize = 4;
					break;
				case enumDBFields.t010_idoficina:
					paramName = "@t010_idoficina";
					paramType = SqlDbType.SmallInt;
					paramSize = 2;
					break;
				case enumDBFields.t420_idreferencia_lote:
					paramName = "@t420_idreferencia_lote";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t175_idcc:
					paramName = "@t175_idcc";
					paramType = SqlDbType.VarChar;
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
