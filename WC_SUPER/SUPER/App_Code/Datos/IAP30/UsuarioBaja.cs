using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for UsuarioBaja
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class UsuarioBaja 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t001_IDFICEPI = 1,
			t314_idusuario = 2,
			NOMBRE = 3,
			APELLIDO1 = 4,
			APELLIDO2 = 5,
			T009_IDCENTRAB = 6,
			T009_DESCENTRAB = 7,
			t303_idnodo = 8,
			t303_ultcierreIAP = 9,
			t303_denominacion = 10,
			t399_figura = 11,
			t399_figura_cvt = 12,
			t314_falta = 13,
			t314_fbaja = 14,
			t314_jornadareducida = 15,
			t314_horasjor_red = 16,
			t314_fdesde_red = 17,
			t314_fhasta_red = 18,
			t314_controlhuecos = 19,
			fUltImputacion = 20,
			IdCalendario = 21,
			desCalendario = 22,
			t066_semlabL = 23,
			t066_semlabM = 24,
			t066_semlabX = 25,
			t066_semlabJ = 26,
			t066_semlabV = 27,
			t066_semlabS = 28,
			t066_semlabD = 29,
			t001_codred = 30,
			t001_sexo = 31,
			t314_crp = 32,
			t314_accesohabilitado = 33,
			t314_diamante = 34,
			tipo = 35,
			t314_nsegmb = 36,
			T010_CODWEATHER = 37,
			T010_NOMWEATHER = 38,
			t314_carrusel1024 = 39,
			t314_avance1024 = 40,
			t314_resumen1024 = 41,
			t314_datosres1024 = 42,
			t314_fichaeco1024 = 43,
			t314_segrenta1024 = 44,
			t314_avantec1024 = 45,
			t314_estruct1024 = 46,
			t314_fotopst1024 = 47,
			t314_plant1024 = 48,
			t314_const1024 = 49,
			t314_iapfact1024 = 50,
			t314_iapdiario1024 = 51,
			t314_cuadromando1024 = 52,
			t314_importaciongasvi = 53,
			t314_recibirmails = 54,
			t314_defectoperiodificacion = 55,
			t314_multiventana = 56,
			t422_idmoneda_VDC = 57,
			t422_denominacionimportes_vdc = 58,
			t422_idmoneda_VDP = 59,
			t422_denominacionimportes_vdp = 60,
			t422_denominacionimportes = 61,
			t314_nuevogasvi = 62,
			t314_calculoonline = 63,
			t314_cargaestructura = 64
        }

        internal UsuarioBaja(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
		#region funciones publicas
        ///// <summary>
        ///// Inserta un UsuarioBaja
        ///// </summary>
        //internal int Insert(Models.UsuarioBaja oUsuarioBaja)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[64] {
        //            Param(enumDBFields.t001_IDFICEPI, oUsuarioBaja.t001_IDFICEPI),
        //            Param(enumDBFields.t314_idusuario, oUsuarioBaja.t314_idusuario),
        //            Param(enumDBFields.NOMBRE, oUsuarioBaja.NOMBRE),
        //            Param(enumDBFields.APELLIDO1, oUsuarioBaja.APELLIDO1),
        //            Param(enumDBFields.APELLIDO2, oUsuarioBaja.APELLIDO2),
        //            Param(enumDBFields.T009_IDCENTRAB, oUsuarioBaja.T009_IDCENTRAB),
        //            Param(enumDBFields.T009_DESCENTRAB, oUsuarioBaja.T009_DESCENTRAB),
        //            Param(enumDBFields.t303_idnodo, oUsuarioBaja.t303_idnodo),
        //            Param(enumDBFields.t303_ultcierreIAP, oUsuarioBaja.t303_ultcierreIAP),
        //            Param(enumDBFields.t303_denominacion, oUsuarioBaja.t303_denominacion),
        //            Param(enumDBFields.t399_figura, oUsuarioBaja.t399_figura),
        //            Param(enumDBFields.t399_figura_cvt, oUsuarioBaja.t399_figura_cvt),
        //            Param(enumDBFields.t314_falta, oUsuarioBaja.t314_falta),
        //            Param(enumDBFields.t314_fbaja, oUsuarioBaja.t314_fbaja),
        //            Param(enumDBFields.t314_jornadareducida, oUsuarioBaja.t314_jornadareducida),
        //            Param(enumDBFields.t314_horasjor_red, oUsuarioBaja.t314_horasjor_red),
        //            Param(enumDBFields.t314_fdesde_red, oUsuarioBaja.t314_fdesde_red),
        //            Param(enumDBFields.t314_fhasta_red, oUsuarioBaja.t314_fhasta_red),
        //            Param(enumDBFields.t314_controlhuecos, oUsuarioBaja.t314_controlhuecos),
        //            Param(enumDBFields.fUltImputacion, oUsuarioBaja.fUltImputacion),
        //            Param(enumDBFields.IdCalendario, oUsuarioBaja.IdCalendario),
        //            Param(enumDBFields.desCalendario, oUsuarioBaja.desCalendario),
        //            Param(enumDBFields.t066_semlabL, oUsuarioBaja.t066_semlabL),
        //            Param(enumDBFields.t066_semlabM, oUsuarioBaja.t066_semlabM),
        //            Param(enumDBFields.t066_semlabX, oUsuarioBaja.t066_semlabX),
        //            Param(enumDBFields.t066_semlabJ, oUsuarioBaja.t066_semlabJ),
        //            Param(enumDBFields.t066_semlabV, oUsuarioBaja.t066_semlabV),
        //            Param(enumDBFields.t066_semlabS, oUsuarioBaja.t066_semlabS),
        //            Param(enumDBFields.t066_semlabD, oUsuarioBaja.t066_semlabD),
        //            Param(enumDBFields.t001_codred, oUsuarioBaja.t001_codred),
        //            Param(enumDBFields.t001_sexo, oUsuarioBaja.t001_sexo),
        //            Param(enumDBFields.t314_crp, oUsuarioBaja.t314_crp),
        //            Param(enumDBFields.t314_accesohabilitado, oUsuarioBaja.t314_accesohabilitado),
        //            Param(enumDBFields.t314_diamante, oUsuarioBaja.t314_diamante),
        //            Param(enumDBFields.tipo, oUsuarioBaja.tipo),
        //            Param(enumDBFields.t314_nsegmb, oUsuarioBaja.t314_nsegmb),
        //            Param(enumDBFields.T010_CODWEATHER, oUsuarioBaja.T010_CODWEATHER),
        //            Param(enumDBFields.T010_NOMWEATHER, oUsuarioBaja.T010_NOMWEATHER),
        //            Param(enumDBFields.t314_carrusel1024, oUsuarioBaja.t314_carrusel1024),
        //            Param(enumDBFields.t314_avance1024, oUsuarioBaja.t314_avance1024),
        //            Param(enumDBFields.t314_resumen1024, oUsuarioBaja.t314_resumen1024),
        //            Param(enumDBFields.t314_datosres1024, oUsuarioBaja.t314_datosres1024),
        //            Param(enumDBFields.t314_fichaeco1024, oUsuarioBaja.t314_fichaeco1024),
        //            Param(enumDBFields.t314_segrenta1024, oUsuarioBaja.t314_segrenta1024),
        //            Param(enumDBFields.t314_avantec1024, oUsuarioBaja.t314_avantec1024),
        //            Param(enumDBFields.t314_estruct1024, oUsuarioBaja.t314_estruct1024),
        //            Param(enumDBFields.t314_fotopst1024, oUsuarioBaja.t314_fotopst1024),
        //            Param(enumDBFields.t314_plant1024, oUsuarioBaja.t314_plant1024),
        //            Param(enumDBFields.t314_const1024, oUsuarioBaja.t314_const1024),
        //            Param(enumDBFields.t314_iapfact1024, oUsuarioBaja.t314_iapfact1024),
        //            Param(enumDBFields.t314_iapdiario1024, oUsuarioBaja.t314_iapdiario1024),
        //            Param(enumDBFields.t314_cuadromando1024, oUsuarioBaja.t314_cuadromando1024),
        //            Param(enumDBFields.t314_importaciongasvi, oUsuarioBaja.t314_importaciongasvi),
        //            Param(enumDBFields.t314_recibirmails, oUsuarioBaja.t314_recibirmails),
        //            Param(enumDBFields.t314_defectoperiodificacion, oUsuarioBaja.t314_defectoperiodificacion),
        //            Param(enumDBFields.t314_multiventana, oUsuarioBaja.t314_multiventana),
        //            Param(enumDBFields.t422_idmoneda_VDC, oUsuarioBaja.t422_idmoneda_VDC),
        //            Param(enumDBFields.t422_denominacionimportes_vdc, oUsuarioBaja.t422_denominacionimportes_vdc),
        //            Param(enumDBFields.t422_idmoneda_VDP, oUsuarioBaja.t422_idmoneda_VDP),
        //            Param(enumDBFields.t422_denominacionimportes_vdp, oUsuarioBaja.t422_denominacionimportes_vdp),
        //            Param(enumDBFields.t422_denominacionimportes, oUsuarioBaja.t422_denominacionimportes),
        //            Param(enumDBFields.t314_nuevogasvi, oUsuarioBaja.t314_nuevogasvi),
        //            Param(enumDBFields.t314_calculoonline, oUsuarioBaja.t314_calculoonline),
        //            Param(enumDBFields.t314_cargaestructura, oUsuarioBaja.t314_cargaestructura)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_UsuarioBaja_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un UsuarioBaja a partir del id
        ///// </summary>
        //internal Models.UsuarioBaja Select()
        //{
        //    Models.UsuarioBaja oUsuarioBaja = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_UsuarioBaja_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oUsuarioBaja = new Models.UsuarioBaja();
        //            oUsuarioBaja.t001_IDFICEPI=Convert.ToInt32(dr["t001_IDFICEPI"]);
        //            oUsuarioBaja.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            if(!Convert.IsDBNull(dr["NOMBRE"]))
        //                oUsuarioBaja.NOMBRE=Convert.ToString(dr["NOMBRE"]);
        //            if(!Convert.IsDBNull(dr["APELLIDO1"]))
        //                oUsuarioBaja.APELLIDO1=Convert.ToString(dr["APELLIDO1"]);
        //            if(!Convert.IsDBNull(dr["APELLIDO2"]))
        //                oUsuarioBaja.APELLIDO2=Convert.ToString(dr["APELLIDO2"]);
        //            if(!Convert.IsDBNull(dr["T009_IDCENTRAB"]))
        //                oUsuarioBaja.T009_IDCENTRAB=Convert.ToInt16(dr["T009_IDCENTRAB"]);
        //            if(!Convert.IsDBNull(dr["T009_DESCENTRAB"]))
        //                oUsuarioBaja.T009_DESCENTRAB=Convert.ToString(dr["T009_DESCENTRAB"]);
        //            if(!Convert.IsDBNull(dr["t303_idnodo"]))
        //                oUsuarioBaja.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
        //            if(!Convert.IsDBNull(dr["t303_ultcierreIAP"]))
        //                oUsuarioBaja.t303_ultcierreIAP=Convert.ToInt32(dr["t303_ultcierreIAP"]);
        //            if(!Convert.IsDBNull(dr["t303_denominacion"]))
        //                oUsuarioBaja.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
        //            if(!Convert.IsDBNull(dr["t399_figura"]))
        //                oUsuarioBaja.t399_figura=Convert.ToString(dr["t399_figura"]);
        //            if(!Convert.IsDBNull(dr["t399_figura_cvt"]))
        //                oUsuarioBaja.t399_figura_cvt=Convert.ToString(dr["t399_figura_cvt"]);
        //            oUsuarioBaja.t314_falta=Convert.ToDateTime(dr["t314_falta"]);
        //            if(!Convert.IsDBNull(dr["t314_fbaja"]))
        //                oUsuarioBaja.t314_fbaja=Convert.ToDateTime(dr["t314_fbaja"]);
        //            oUsuarioBaja.t314_jornadareducida=Convert.ToBoolean(dr["t314_jornadareducida"]);
        //            oUsuarioBaja.t314_horasjor_red=Convert.ToSingle(dr["t314_horasjor_red"]);
        //            if(!Convert.IsDBNull(dr["t314_fdesde_red"]))
        //                oUsuarioBaja.t314_fdesde_red=Convert.ToDateTime(dr["t314_fdesde_red"]);
        //            if(!Convert.IsDBNull(dr["t314_fhasta_red"]))
        //                oUsuarioBaja.t314_fhasta_red=Convert.ToDateTime(dr["t314_fhasta_red"]);
        //            oUsuarioBaja.t314_controlhuecos=Convert.ToBoolean(dr["t314_controlhuecos"]);
        //            if(!Convert.IsDBNull(dr["fUltImputacion"]))
        //                oUsuarioBaja.fUltImputacion=Convert.ToDateTime(dr["fUltImputacion"]);
        //            oUsuarioBaja.IdCalendario=Convert.ToInt32(dr["IdCalendario"]);
        //            oUsuarioBaja.desCalendario=Convert.ToString(dr["desCalendario"]);
        //            oUsuarioBaja.t066_semlabL=Convert.ToInt32(dr["t066_semlabL"]);
        //            oUsuarioBaja.t066_semlabM=Convert.ToInt32(dr["t066_semlabM"]);
        //            oUsuarioBaja.t066_semlabX=Convert.ToInt32(dr["t066_semlabX"]);
        //            oUsuarioBaja.t066_semlabJ=Convert.ToInt32(dr["t066_semlabJ"]);
        //            oUsuarioBaja.t066_semlabV=Convert.ToInt32(dr["t066_semlabV"]);
        //            oUsuarioBaja.t066_semlabS=Convert.ToInt32(dr["t066_semlabS"]);
        //            oUsuarioBaja.t066_semlabD=Convert.ToInt32(dr["t066_semlabD"]);
        //            oUsuarioBaja.t001_codred=Convert.ToString(dr["t001_codred"]);
        //            oUsuarioBaja.t001_sexo=Convert.ToString(dr["t001_sexo"]);
        //            oUsuarioBaja.t314_crp=Convert.ToBoolean(dr["t314_crp"]);
        //            oUsuarioBaja.t314_accesohabilitado=Convert.ToBoolean(dr["t314_accesohabilitado"]);
        //            oUsuarioBaja.t314_diamante=Convert.ToBoolean(dr["t314_diamante"]);
        //            oUsuarioBaja.tipo=Convert.ToString(dr["tipo"]);
        //            oUsuarioBaja.t314_nsegmb=Convert.ToByte(dr["t314_nsegmb"]);
        //            if(!Convert.IsDBNull(dr["T010_CODWEATHER"]))
        //                oUsuarioBaja.T010_CODWEATHER=Convert.ToString(dr["T010_CODWEATHER"]);
        //            if(!Convert.IsDBNull(dr["T010_NOMWEATHER"]))
        //                oUsuarioBaja.T010_NOMWEATHER=Convert.ToString(dr["T010_NOMWEATHER"]);
        //            oUsuarioBaja.t314_carrusel1024=Convert.ToBoolean(dr["t314_carrusel1024"]);
        //            oUsuarioBaja.t314_avance1024=Convert.ToBoolean(dr["t314_avance1024"]);
        //            oUsuarioBaja.t314_resumen1024=Convert.ToBoolean(dr["t314_resumen1024"]);
        //            oUsuarioBaja.t314_datosres1024=Convert.ToBoolean(dr["t314_datosres1024"]);
        //            oUsuarioBaja.t314_fichaeco1024=Convert.ToBoolean(dr["t314_fichaeco1024"]);
        //            oUsuarioBaja.t314_segrenta1024=Convert.ToBoolean(dr["t314_segrenta1024"]);
        //            oUsuarioBaja.t314_avantec1024=Convert.ToBoolean(dr["t314_avantec1024"]);
        //            oUsuarioBaja.t314_estruct1024=Convert.ToBoolean(dr["t314_estruct1024"]);
        //            oUsuarioBaja.t314_fotopst1024=Convert.ToBoolean(dr["t314_fotopst1024"]);
        //            oUsuarioBaja.t314_plant1024=Convert.ToBoolean(dr["t314_plant1024"]);
        //            oUsuarioBaja.t314_const1024=Convert.ToBoolean(dr["t314_const1024"]);
        //            oUsuarioBaja.t314_iapfact1024=Convert.ToBoolean(dr["t314_iapfact1024"]);
        //            oUsuarioBaja.t314_iapdiario1024=Convert.ToBoolean(dr["t314_iapdiario1024"]);
        //            oUsuarioBaja.t314_cuadromando1024=Convert.ToBoolean(dr["t314_cuadromando1024"]);
        //            oUsuarioBaja.t314_importaciongasvi=Convert.ToByte(dr["t314_importaciongasvi"]);
        //            oUsuarioBaja.t314_recibirmails=Convert.ToBoolean(dr["t314_recibirmails"]);
        //            oUsuarioBaja.t314_defectoperiodificacion=Convert.ToBoolean(dr["t314_defectoperiodificacion"]);
        //            oUsuarioBaja.t314_multiventana=Convert.ToBoolean(dr["t314_multiventana"]);
        //            oUsuarioBaja.t422_idmoneda_VDC=Convert.ToString(dr["t422_idmoneda_VDC"]);
        //            oUsuarioBaja.t422_denominacionimportes_vdc=Convert.ToString(dr["t422_denominacionimportes_vdc"]);
        //            if(!Convert.IsDBNull(dr["t422_idmoneda_VDP"]))
        //                oUsuarioBaja.t422_idmoneda_VDP=Convert.ToString(dr["t422_idmoneda_VDP"]);
        //            if(!Convert.IsDBNull(dr["t422_denominacionimportes_vdp"]))
        //                oUsuarioBaja.t422_denominacionimportes_vdp=Convert.ToString(dr["t422_denominacionimportes_vdp"]);
        //            oUsuarioBaja.t422_denominacionimportes=Convert.ToString(dr["t422_denominacionimportes"]);
        //            oUsuarioBaja.t314_nuevogasvi=Convert.ToBoolean(dr["t314_nuevogasvi"]);
        //            oUsuarioBaja.t314_calculoonline=Convert.ToBoolean(dr["t314_calculoonline"]);
        //            oUsuarioBaja.t314_cargaestructura=Convert.ToBoolean(dr["t314_cargaestructura"]);

        //        }
        //        return oUsuarioBaja;
				
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
        ///// Actualiza un UsuarioBaja a partir del id
        ///// </summary>
        //internal int Update(Models.UsuarioBaja oUsuarioBaja)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[64] {
        //            Param(enumDBFields.t001_IDFICEPI, oUsuarioBaja.t001_IDFICEPI),
        //            Param(enumDBFields.t314_idusuario, oUsuarioBaja.t314_idusuario),
        //            Param(enumDBFields.NOMBRE, oUsuarioBaja.NOMBRE),
        //            Param(enumDBFields.APELLIDO1, oUsuarioBaja.APELLIDO1),
        //            Param(enumDBFields.APELLIDO2, oUsuarioBaja.APELLIDO2),
        //            Param(enumDBFields.T009_IDCENTRAB, oUsuarioBaja.T009_IDCENTRAB),
        //            Param(enumDBFields.T009_DESCENTRAB, oUsuarioBaja.T009_DESCENTRAB),
        //            Param(enumDBFields.t303_idnodo, oUsuarioBaja.t303_idnodo),
        //            Param(enumDBFields.t303_ultcierreIAP, oUsuarioBaja.t303_ultcierreIAP),
        //            Param(enumDBFields.t303_denominacion, oUsuarioBaja.t303_denominacion),
        //            Param(enumDBFields.t399_figura, oUsuarioBaja.t399_figura),
        //            Param(enumDBFields.t399_figura_cvt, oUsuarioBaja.t399_figura_cvt),
        //            Param(enumDBFields.t314_falta, oUsuarioBaja.t314_falta),
        //            Param(enumDBFields.t314_fbaja, oUsuarioBaja.t314_fbaja),
        //            Param(enumDBFields.t314_jornadareducida, oUsuarioBaja.t314_jornadareducida),
        //            Param(enumDBFields.t314_horasjor_red, oUsuarioBaja.t314_horasjor_red),
        //            Param(enumDBFields.t314_fdesde_red, oUsuarioBaja.t314_fdesde_red),
        //            Param(enumDBFields.t314_fhasta_red, oUsuarioBaja.t314_fhasta_red),
        //            Param(enumDBFields.t314_controlhuecos, oUsuarioBaja.t314_controlhuecos),
        //            Param(enumDBFields.fUltImputacion, oUsuarioBaja.fUltImputacion),
        //            Param(enumDBFields.IdCalendario, oUsuarioBaja.IdCalendario),
        //            Param(enumDBFields.desCalendario, oUsuarioBaja.desCalendario),
        //            Param(enumDBFields.t066_semlabL, oUsuarioBaja.t066_semlabL),
        //            Param(enumDBFields.t066_semlabM, oUsuarioBaja.t066_semlabM),
        //            Param(enumDBFields.t066_semlabX, oUsuarioBaja.t066_semlabX),
        //            Param(enumDBFields.t066_semlabJ, oUsuarioBaja.t066_semlabJ),
        //            Param(enumDBFields.t066_semlabV, oUsuarioBaja.t066_semlabV),
        //            Param(enumDBFields.t066_semlabS, oUsuarioBaja.t066_semlabS),
        //            Param(enumDBFields.t066_semlabD, oUsuarioBaja.t066_semlabD),
        //            Param(enumDBFields.t001_codred, oUsuarioBaja.t001_codred),
        //            Param(enumDBFields.t001_sexo, oUsuarioBaja.t001_sexo),
        //            Param(enumDBFields.t314_crp, oUsuarioBaja.t314_crp),
        //            Param(enumDBFields.t314_accesohabilitado, oUsuarioBaja.t314_accesohabilitado),
        //            Param(enumDBFields.t314_diamante, oUsuarioBaja.t314_diamante),
        //            Param(enumDBFields.tipo, oUsuarioBaja.tipo),
        //            Param(enumDBFields.t314_nsegmb, oUsuarioBaja.t314_nsegmb),
        //            Param(enumDBFields.T010_CODWEATHER, oUsuarioBaja.T010_CODWEATHER),
        //            Param(enumDBFields.T010_NOMWEATHER, oUsuarioBaja.T010_NOMWEATHER),
        //            Param(enumDBFields.t314_carrusel1024, oUsuarioBaja.t314_carrusel1024),
        //            Param(enumDBFields.t314_avance1024, oUsuarioBaja.t314_avance1024),
        //            Param(enumDBFields.t314_resumen1024, oUsuarioBaja.t314_resumen1024),
        //            Param(enumDBFields.t314_datosres1024, oUsuarioBaja.t314_datosres1024),
        //            Param(enumDBFields.t314_fichaeco1024, oUsuarioBaja.t314_fichaeco1024),
        //            Param(enumDBFields.t314_segrenta1024, oUsuarioBaja.t314_segrenta1024),
        //            Param(enumDBFields.t314_avantec1024, oUsuarioBaja.t314_avantec1024),
        //            Param(enumDBFields.t314_estruct1024, oUsuarioBaja.t314_estruct1024),
        //            Param(enumDBFields.t314_fotopst1024, oUsuarioBaja.t314_fotopst1024),
        //            Param(enumDBFields.t314_plant1024, oUsuarioBaja.t314_plant1024),
        //            Param(enumDBFields.t314_const1024, oUsuarioBaja.t314_const1024),
        //            Param(enumDBFields.t314_iapfact1024, oUsuarioBaja.t314_iapfact1024),
        //            Param(enumDBFields.t314_iapdiario1024, oUsuarioBaja.t314_iapdiario1024),
        //            Param(enumDBFields.t314_cuadromando1024, oUsuarioBaja.t314_cuadromando1024),
        //            Param(enumDBFields.t314_importaciongasvi, oUsuarioBaja.t314_importaciongasvi),
        //            Param(enumDBFields.t314_recibirmails, oUsuarioBaja.t314_recibirmails),
        //            Param(enumDBFields.t314_defectoperiodificacion, oUsuarioBaja.t314_defectoperiodificacion),
        //            Param(enumDBFields.t314_multiventana, oUsuarioBaja.t314_multiventana),
        //            Param(enumDBFields.t422_idmoneda_VDC, oUsuarioBaja.t422_idmoneda_VDC),
        //            Param(enumDBFields.t422_denominacionimportes_vdc, oUsuarioBaja.t422_denominacionimportes_vdc),
        //            Param(enumDBFields.t422_idmoneda_VDP, oUsuarioBaja.t422_idmoneda_VDP),
        //            Param(enumDBFields.t422_denominacionimportes_vdp, oUsuarioBaja.t422_denominacionimportes_vdp),
        //            Param(enumDBFields.t422_denominacionimportes, oUsuarioBaja.t422_denominacionimportes),
        //            Param(enumDBFields.t314_nuevogasvi, oUsuarioBaja.t314_nuevogasvi),
        //            Param(enumDBFields.t314_calculoonline, oUsuarioBaja.t314_calculoonline),
        //            Param(enumDBFields.t314_cargaestructura, oUsuarioBaja.t314_cargaestructura)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_UsuarioBaja_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un UsuarioBaja a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_UsuarioBaja_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los UsuarioBaja
        ///// </summary>
        //internal List<Models.UsuarioBaja> Catalogo(Models.UsuarioBaja oUsuarioBajaFilter)
        //{
        //    Models.UsuarioBaja oUsuarioBaja = null;
        //    List<Models.UsuarioBaja> lst = new List<Models.UsuarioBaja>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[64] {
        //            Param(enumDBFields.t001_IDFICEPI, oTEMP_UsuarioBajaFilter.t001_IDFICEPI),
        //            Param(enumDBFields.t314_idusuario, oTEMP_UsuarioBajaFilter.t314_idusuario),
        //            Param(enumDBFields.NOMBRE, oTEMP_UsuarioBajaFilter.NOMBRE),
        //            Param(enumDBFields.APELLIDO1, oTEMP_UsuarioBajaFilter.APELLIDO1),
        //            Param(enumDBFields.APELLIDO2, oTEMP_UsuarioBajaFilter.APELLIDO2),
        //            Param(enumDBFields.T009_IDCENTRAB, oTEMP_UsuarioBajaFilter.T009_IDCENTRAB),
        //            Param(enumDBFields.T009_DESCENTRAB, oTEMP_UsuarioBajaFilter.T009_DESCENTRAB),
        //            Param(enumDBFields.t303_idnodo, oTEMP_UsuarioBajaFilter.t303_idnodo),
        //            Param(enumDBFields.t303_ultcierreIAP, oTEMP_UsuarioBajaFilter.t303_ultcierreIAP),
        //            Param(enumDBFields.t303_denominacion, oTEMP_UsuarioBajaFilter.t303_denominacion),
        //            Param(enumDBFields.t399_figura, oTEMP_UsuarioBajaFilter.t399_figura),
        //            Param(enumDBFields.t399_figura_cvt, oTEMP_UsuarioBajaFilter.t399_figura_cvt),
        //            Param(enumDBFields.t314_falta, oTEMP_UsuarioBajaFilter.t314_falta),
        //            Param(enumDBFields.t314_fbaja, oTEMP_UsuarioBajaFilter.t314_fbaja),
        //            Param(enumDBFields.t314_jornadareducida, oTEMP_UsuarioBajaFilter.t314_jornadareducida),
        //            Param(enumDBFields.t314_horasjor_red, oTEMP_UsuarioBajaFilter.t314_horasjor_red),
        //            Param(enumDBFields.t314_fdesde_red, oTEMP_UsuarioBajaFilter.t314_fdesde_red),
        //            Param(enumDBFields.t314_fhasta_red, oTEMP_UsuarioBajaFilter.t314_fhasta_red),
        //            Param(enumDBFields.t314_controlhuecos, oTEMP_UsuarioBajaFilter.t314_controlhuecos),
        //            Param(enumDBFields.fUltImputacion, oTEMP_UsuarioBajaFilter.fUltImputacion),
        //            Param(enumDBFields.IdCalendario, oTEMP_UsuarioBajaFilter.IdCalendario),
        //            Param(enumDBFields.desCalendario, oTEMP_UsuarioBajaFilter.desCalendario),
        //            Param(enumDBFields.t066_semlabL, oTEMP_UsuarioBajaFilter.t066_semlabL),
        //            Param(enumDBFields.t066_semlabM, oTEMP_UsuarioBajaFilter.t066_semlabM),
        //            Param(enumDBFields.t066_semlabX, oTEMP_UsuarioBajaFilter.t066_semlabX),
        //            Param(enumDBFields.t066_semlabJ, oTEMP_UsuarioBajaFilter.t066_semlabJ),
        //            Param(enumDBFields.t066_semlabV, oTEMP_UsuarioBajaFilter.t066_semlabV),
        //            Param(enumDBFields.t066_semlabS, oTEMP_UsuarioBajaFilter.t066_semlabS),
        //            Param(enumDBFields.t066_semlabD, oTEMP_UsuarioBajaFilter.t066_semlabD),
        //            Param(enumDBFields.t001_codred, oTEMP_UsuarioBajaFilter.t001_codred),
        //            Param(enumDBFields.t001_sexo, oTEMP_UsuarioBajaFilter.t001_sexo),
        //            Param(enumDBFields.t314_crp, oTEMP_UsuarioBajaFilter.t314_crp),
        //            Param(enumDBFields.t314_accesohabilitado, oTEMP_UsuarioBajaFilter.t314_accesohabilitado),
        //            Param(enumDBFields.t314_diamante, oTEMP_UsuarioBajaFilter.t314_diamante),
        //            Param(enumDBFields.tipo, oTEMP_UsuarioBajaFilter.tipo),
        //            Param(enumDBFields.t314_nsegmb, oTEMP_UsuarioBajaFilter.t314_nsegmb),
        //            Param(enumDBFields.T010_CODWEATHER, oTEMP_UsuarioBajaFilter.T010_CODWEATHER),
        //            Param(enumDBFields.T010_NOMWEATHER, oTEMP_UsuarioBajaFilter.T010_NOMWEATHER),
        //            Param(enumDBFields.t314_carrusel1024, oTEMP_UsuarioBajaFilter.t314_carrusel1024),
        //            Param(enumDBFields.t314_avance1024, oTEMP_UsuarioBajaFilter.t314_avance1024),
        //            Param(enumDBFields.t314_resumen1024, oTEMP_UsuarioBajaFilter.t314_resumen1024),
        //            Param(enumDBFields.t314_datosres1024, oTEMP_UsuarioBajaFilter.t314_datosres1024),
        //            Param(enumDBFields.t314_fichaeco1024, oTEMP_UsuarioBajaFilter.t314_fichaeco1024),
        //            Param(enumDBFields.t314_segrenta1024, oTEMP_UsuarioBajaFilter.t314_segrenta1024),
        //            Param(enumDBFields.t314_avantec1024, oTEMP_UsuarioBajaFilter.t314_avantec1024),
        //            Param(enumDBFields.t314_estruct1024, oTEMP_UsuarioBajaFilter.t314_estruct1024),
        //            Param(enumDBFields.t314_fotopst1024, oTEMP_UsuarioBajaFilter.t314_fotopst1024),
        //            Param(enumDBFields.t314_plant1024, oTEMP_UsuarioBajaFilter.t314_plant1024),
        //            Param(enumDBFields.t314_const1024, oTEMP_UsuarioBajaFilter.t314_const1024),
        //            Param(enumDBFields.t314_iapfact1024, oTEMP_UsuarioBajaFilter.t314_iapfact1024),
        //            Param(enumDBFields.t314_iapdiario1024, oTEMP_UsuarioBajaFilter.t314_iapdiario1024),
        //            Param(enumDBFields.t314_cuadromando1024, oTEMP_UsuarioBajaFilter.t314_cuadromando1024),
        //            Param(enumDBFields.t314_importaciongasvi, oTEMP_UsuarioBajaFilter.t314_importaciongasvi),
        //            Param(enumDBFields.t314_recibirmails, oTEMP_UsuarioBajaFilter.t314_recibirmails),
        //            Param(enumDBFields.t314_defectoperiodificacion, oTEMP_UsuarioBajaFilter.t314_defectoperiodificacion),
        //            Param(enumDBFields.t314_multiventana, oTEMP_UsuarioBajaFilter.t314_multiventana),
        //            Param(enumDBFields.t422_idmoneda_VDC, oTEMP_UsuarioBajaFilter.t422_idmoneda_VDC),
        //            Param(enumDBFields.t422_denominacionimportes_vdc, oTEMP_UsuarioBajaFilter.t422_denominacionimportes_vdc),
        //            Param(enumDBFields.t422_idmoneda_VDP, oTEMP_UsuarioBajaFilter.t422_idmoneda_VDP),
        //            Param(enumDBFields.t422_denominacionimportes_vdp, oTEMP_UsuarioBajaFilter.t422_denominacionimportes_vdp),
        //            Param(enumDBFields.t422_denominacionimportes, oTEMP_UsuarioBajaFilter.t422_denominacionimportes),
        //            Param(enumDBFields.t314_nuevogasvi, oTEMP_UsuarioBajaFilter.t314_nuevogasvi),
        //            Param(enumDBFields.t314_calculoonline, oTEMP_UsuarioBajaFilter.t314_calculoonline),
        //            Param(enumDBFields.t314_cargaestructura, oTEMP_UsuarioBajaFilter.t314_cargaestructura)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_UsuarioBaja_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oUsuarioBaja = new Models.UsuarioBaja();
        //            oUsuarioBaja.t001_IDFICEPI=Convert.ToInt32(dr["t001_IDFICEPI"]);
        //            oUsuarioBaja.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            if(!Convert.IsDBNull(dr["NOMBRE"]))
        //                oUsuarioBaja.NOMBRE=Convert.ToString(dr["NOMBRE"]);
        //            if(!Convert.IsDBNull(dr["APELLIDO1"]))
        //                oUsuarioBaja.APELLIDO1=Convert.ToString(dr["APELLIDO1"]);
        //            if(!Convert.IsDBNull(dr["APELLIDO2"]))
        //                oUsuarioBaja.APELLIDO2=Convert.ToString(dr["APELLIDO2"]);
        //            if(!Convert.IsDBNull(dr["T009_IDCENTRAB"]))
        //                oUsuarioBaja.T009_IDCENTRAB=Convert.ToInt16(dr["T009_IDCENTRAB"]);
        //            if(!Convert.IsDBNull(dr["T009_DESCENTRAB"]))
        //                oUsuarioBaja.T009_DESCENTRAB=Convert.ToString(dr["T009_DESCENTRAB"]);
        //            if(!Convert.IsDBNull(dr["t303_idnodo"]))
        //                oUsuarioBaja.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
        //            if(!Convert.IsDBNull(dr["t303_ultcierreIAP"]))
        //                oUsuarioBaja.t303_ultcierreIAP=Convert.ToInt32(dr["t303_ultcierreIAP"]);
        //            if(!Convert.IsDBNull(dr["t303_denominacion"]))
        //                oUsuarioBaja.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
        //            if(!Convert.IsDBNull(dr["t399_figura"]))
        //                oUsuarioBaja.t399_figura=Convert.ToString(dr["t399_figura"]);
        //            if(!Convert.IsDBNull(dr["t399_figura_cvt"]))
        //                oUsuarioBaja.t399_figura_cvt=Convert.ToString(dr["t399_figura_cvt"]);
        //            oUsuarioBaja.t314_falta=Convert.ToDateTime(dr["t314_falta"]);
        //            if(!Convert.IsDBNull(dr["t314_fbaja"]))
        //                oUsuarioBaja.t314_fbaja=Convert.ToDateTime(dr["t314_fbaja"]);
        //            oUsuarioBaja.t314_jornadareducida=Convert.ToBoolean(dr["t314_jornadareducida"]);
        //            oUsuarioBaja.t314_horasjor_red=Convert.ToSingle(dr["t314_horasjor_red"]);
        //            if(!Convert.IsDBNull(dr["t314_fdesde_red"]))
        //                oUsuarioBaja.t314_fdesde_red=Convert.ToDateTime(dr["t314_fdesde_red"]);
        //            if(!Convert.IsDBNull(dr["t314_fhasta_red"]))
        //                oUsuarioBaja.t314_fhasta_red=Convert.ToDateTime(dr["t314_fhasta_red"]);
        //            oUsuarioBaja.t314_controlhuecos=Convert.ToBoolean(dr["t314_controlhuecos"]);
        //            if(!Convert.IsDBNull(dr["fUltImputacion"]))
        //                oUsuarioBaja.fUltImputacion=Convert.ToDateTime(dr["fUltImputacion"]);
        //            oUsuarioBaja.IdCalendario=Convert.ToInt32(dr["IdCalendario"]);
        //            oUsuarioBaja.desCalendario=Convert.ToString(dr["desCalendario"]);
        //            oUsuarioBaja.t066_semlabL=Convert.ToInt32(dr["t066_semlabL"]);
        //            oUsuarioBaja.t066_semlabM=Convert.ToInt32(dr["t066_semlabM"]);
        //            oUsuarioBaja.t066_semlabX=Convert.ToInt32(dr["t066_semlabX"]);
        //            oUsuarioBaja.t066_semlabJ=Convert.ToInt32(dr["t066_semlabJ"]);
        //            oUsuarioBaja.t066_semlabV=Convert.ToInt32(dr["t066_semlabV"]);
        //            oUsuarioBaja.t066_semlabS=Convert.ToInt32(dr["t066_semlabS"]);
        //            oUsuarioBaja.t066_semlabD=Convert.ToInt32(dr["t066_semlabD"]);
        //            oUsuarioBaja.t001_codred=Convert.ToString(dr["t001_codred"]);
        //            oUsuarioBaja.t001_sexo=Convert.ToString(dr["t001_sexo"]);
        //            oUsuarioBaja.t314_crp=Convert.ToBoolean(dr["t314_crp"]);
        //            oUsuarioBaja.t314_accesohabilitado=Convert.ToBoolean(dr["t314_accesohabilitado"]);
        //            oUsuarioBaja.t314_diamante=Convert.ToBoolean(dr["t314_diamante"]);
        //            oUsuarioBaja.tipo=Convert.ToString(dr["tipo"]);
        //            oUsuarioBaja.t314_nsegmb=Convert.ToByte(dr["t314_nsegmb"]);
        //            if(!Convert.IsDBNull(dr["T010_CODWEATHER"]))
        //                oUsuarioBaja.T010_CODWEATHER=Convert.ToString(dr["T010_CODWEATHER"]);
        //            if(!Convert.IsDBNull(dr["T010_NOMWEATHER"]))
        //                oUsuarioBaja.T010_NOMWEATHER=Convert.ToString(dr["T010_NOMWEATHER"]);
        //            oUsuarioBaja.t314_carrusel1024=Convert.ToBoolean(dr["t314_carrusel1024"]);
        //            oUsuarioBaja.t314_avance1024=Convert.ToBoolean(dr["t314_avance1024"]);
        //            oUsuarioBaja.t314_resumen1024=Convert.ToBoolean(dr["t314_resumen1024"]);
        //            oUsuarioBaja.t314_datosres1024=Convert.ToBoolean(dr["t314_datosres1024"]);
        //            oUsuarioBaja.t314_fichaeco1024=Convert.ToBoolean(dr["t314_fichaeco1024"]);
        //            oUsuarioBaja.t314_segrenta1024=Convert.ToBoolean(dr["t314_segrenta1024"]);
        //            oUsuarioBaja.t314_avantec1024=Convert.ToBoolean(dr["t314_avantec1024"]);
        //            oUsuarioBaja.t314_estruct1024=Convert.ToBoolean(dr["t314_estruct1024"]);
        //            oUsuarioBaja.t314_fotopst1024=Convert.ToBoolean(dr["t314_fotopst1024"]);
        //            oUsuarioBaja.t314_plant1024=Convert.ToBoolean(dr["t314_plant1024"]);
        //            oUsuarioBaja.t314_const1024=Convert.ToBoolean(dr["t314_const1024"]);
        //            oUsuarioBaja.t314_iapfact1024=Convert.ToBoolean(dr["t314_iapfact1024"]);
        //            oUsuarioBaja.t314_iapdiario1024=Convert.ToBoolean(dr["t314_iapdiario1024"]);
        //            oUsuarioBaja.t314_cuadromando1024=Convert.ToBoolean(dr["t314_cuadromando1024"]);
        //            oUsuarioBaja.t314_importaciongasvi=Convert.ToByte(dr["t314_importaciongasvi"]);
        //            oUsuarioBaja.t314_recibirmails=Convert.ToBoolean(dr["t314_recibirmails"]);
        //            oUsuarioBaja.t314_defectoperiodificacion=Convert.ToBoolean(dr["t314_defectoperiodificacion"]);
        //            oUsuarioBaja.t314_multiventana=Convert.ToBoolean(dr["t314_multiventana"]);
        //            oUsuarioBaja.t422_idmoneda_VDC=Convert.ToString(dr["t422_idmoneda_VDC"]);
        //            oUsuarioBaja.t422_denominacionimportes_vdc=Convert.ToString(dr["t422_denominacionimportes_vdc"]);
        //            if(!Convert.IsDBNull(dr["t422_idmoneda_VDP"]))
        //                oUsuarioBaja.t422_idmoneda_VDP=Convert.ToString(dr["t422_idmoneda_VDP"]);
        //            if(!Convert.IsDBNull(dr["t422_denominacionimportes_vdp"]))
        //                oUsuarioBaja.t422_denominacionimportes_vdp=Convert.ToString(dr["t422_denominacionimportes_vdp"]);
        //            oUsuarioBaja.t422_denominacionimportes=Convert.ToString(dr["t422_denominacionimportes"]);
        //            oUsuarioBaja.t314_nuevogasvi=Convert.ToBoolean(dr["t314_nuevogasvi"]);
        //            oUsuarioBaja.t314_calculoonline=Convert.ToBoolean(dr["t314_calculoonline"]);
        //            oUsuarioBaja.t314_cargaestructura=Convert.ToBoolean(dr["t314_cargaestructura"]);

        //            lst.Add(oUsuarioBaja);

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
				case enumDBFields.t001_IDFICEPI:
					paramName = "@t001_IDFICEPI";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t314_idusuario:
					paramName = "@t314_idusuario";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.NOMBRE:
					paramName = "@NOMBRE";
					paramType = SqlDbType.VarChar;
					paramSize = 20;
					break;
				case enumDBFields.APELLIDO1:
					paramName = "@APELLIDO1";
					paramType = SqlDbType.VarChar;
					paramSize = 25;
					break;
				case enumDBFields.APELLIDO2:
					paramName = "@APELLIDO2";
					paramType = SqlDbType.VarChar;
					paramSize = 25;
					break;
				case enumDBFields.T009_IDCENTRAB:
					paramName = "@T009_IDCENTRAB";
					paramType = SqlDbType.SmallInt;
					paramSize = 2;
					break;
				case enumDBFields.T009_DESCENTRAB:
					paramName = "@T009_DESCENTRAB";
					paramType = SqlDbType.VarChar;
					paramSize = 40;
					break;
				case enumDBFields.t303_idnodo:
					paramName = "@t303_idnodo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t303_ultcierreIAP:
					paramName = "@t303_ultcierreIAP";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t303_denominacion:
					paramName = "@t303_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t399_figura:
					paramName = "@t399_figura";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.t399_figura_cvt:
					paramName = "@t399_figura_cvt";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.t314_falta:
					paramName = "@t314_falta";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t314_fbaja:
					paramName = "@t314_fbaja";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t314_jornadareducida:
					paramName = "@t314_jornadareducida";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_horasjor_red:
					paramName = "@t314_horasjor_red";
					paramType = SqlDbType.Real;
					paramSize = 8;
					break;
				case enumDBFields.t314_fdesde_red:
					paramName = "@t314_fdesde_red";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t314_fhasta_red:
					paramName = "@t314_fhasta_red";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t314_controlhuecos:
					paramName = "@t314_controlhuecos";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.fUltImputacion:
					paramName = "@fUltImputacion";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.IdCalendario:
					paramName = "@IdCalendario";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.desCalendario:
					paramName = "@desCalendario";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t066_semlabL:
					paramName = "@t066_semlabL";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t066_semlabM:
					paramName = "@t066_semlabM";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t066_semlabX:
					paramName = "@t066_semlabX";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t066_semlabJ:
					paramName = "@t066_semlabJ";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t066_semlabV:
					paramName = "@t066_semlabV";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t066_semlabS:
					paramName = "@t066_semlabS";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t066_semlabD:
					paramName = "@t066_semlabD";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t001_codred:
					paramName = "@t001_codred";
					paramType = SqlDbType.VarChar;
					paramSize = 15;
					break;
				case enumDBFields.t001_sexo:
					paramName = "@t001_sexo";
					paramType = SqlDbType.Char;
					paramSize = 1;
					break;
				case enumDBFields.t314_crp:
					paramName = "@t314_crp";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_accesohabilitado:
					paramName = "@t314_accesohabilitado";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_diamante:
					paramName = "@t314_diamante";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.tipo:
					paramName = "@tipo";
					paramType = SqlDbType.VarChar;
					paramSize = 1;
					break;
				case enumDBFields.t314_nsegmb:
					paramName = "@t314_nsegmb";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.T010_CODWEATHER:
					paramName = "@T010_CODWEATHER";
					paramType = SqlDbType.VarChar;
					paramSize = 8;
					break;
				case enumDBFields.T010_NOMWEATHER:
					paramName = "@T010_NOMWEATHER";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t314_carrusel1024:
					paramName = "@t314_carrusel1024";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_avance1024:
					paramName = "@t314_avance1024";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_resumen1024:
					paramName = "@t314_resumen1024";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_datosres1024:
					paramName = "@t314_datosres1024";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_fichaeco1024:
					paramName = "@t314_fichaeco1024";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_segrenta1024:
					paramName = "@t314_segrenta1024";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_avantec1024:
					paramName = "@t314_avantec1024";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_estruct1024:
					paramName = "@t314_estruct1024";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_fotopst1024:
					paramName = "@t314_fotopst1024";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_plant1024:
					paramName = "@t314_plant1024";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_const1024:
					paramName = "@t314_const1024";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_iapfact1024:
					paramName = "@t314_iapfact1024";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_iapdiario1024:
					paramName = "@t314_iapdiario1024";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_cuadromando1024:
					paramName = "@t314_cuadromando1024";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_importaciongasvi:
					paramName = "@t314_importaciongasvi";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.t314_recibirmails:
					paramName = "@t314_recibirmails";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_defectoperiodificacion:
					paramName = "@t314_defectoperiodificacion";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_multiventana:
					paramName = "@t314_multiventana";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t422_idmoneda_VDC:
					paramName = "@t422_idmoneda_VDC";
					paramType = SqlDbType.VarChar;
					paramSize = 5;
					break;
				case enumDBFields.t422_denominacionimportes_vdc:
					paramName = "@t422_denominacionimportes_vdc";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t422_idmoneda_VDP:
					paramName = "@t422_idmoneda_VDP";
					paramType = SqlDbType.VarChar;
					paramSize = 5;
					break;
				case enumDBFields.t422_denominacionimportes_vdp:
					paramName = "@t422_denominacionimportes_vdp";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t422_denominacionimportes:
					paramName = "@t422_denominacionimportes";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t314_nuevogasvi:
					paramName = "@t314_nuevogasvi";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_calculoonline:
					paramName = "@t314_calculoonline";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t314_cargaestructura:
					paramName = "@t314_cargaestructura";
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
