using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using SUPER.Capa_Negocio;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Usuario
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class Usuario 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t001_IDFICEPI = 1,
			t314_idusuario = 2,
			t001_cip = 3,
			NOMBRE = 4,
			APELLIDO1 = 5,
			APELLIDO2 = 6,
			T009_IDCENTRAB = 7,
			T009_DESCENTRAB = 8,
			t303_idnodo = 9,
			t303_ultcierreIAP = 10,
			t303_denominacion = 11,
			t399_figura = 12,
			t399_figura_per = 13,
			t399_figura_cvt = 14,
			t314_falta = 15,
			t314_fbaja = 16,
			t314_jornadareducida = 17,
			t314_horasjor_red = 18,
			t314_fdesde_red = 19,
			t314_fhasta_red = 20,
			t314_controlhuecos = 21,
			fUltImputacion = 22,
			IdCalendario = 23,
			desCalendario = 24,
			t066_semlabL = 25,
			t066_semlabM = 26,
			t066_semlabX = 27,
			t066_semlabJ = 28,
			t066_semlabV = 29,
			t066_semlabS = 30,
			t066_semlabD = 31,
			t001_codred = 32,
			t001_sexo = 33,
			t314_crp = 34,
			t314_accesohabilitado = 35,
			t314_diamante = 36,
			tipo = 37,
			t314_nsegmb = 38,
			T010_CODWEATHER = 39,
			T010_NOMWEATHER = 40,
			t314_carrusel1024 = 41,
			t314_avance1024 = 42,
			t314_resumen1024 = 43,
			t314_datosres1024 = 44,
			t314_fichaeco1024 = 45,
			t314_segrenta1024 = 46,
			t314_avantec1024 = 47,
			t314_estruct1024 = 48,
			t314_fotopst1024 = 49,
			t314_plant1024 = 50,
			t314_const1024 = 51,
			t314_iapfact1024 = 52,
			t314_iapdiario1024 = 53,
			t314_cuadromando1024 = 54,
			t314_importaciongasvi = 55,
			t314_recibirmails = 56,
			t314_defectoperiodificacion = 57,
			t314_multiventana = 58,
			t001_botonfecha = 59,
			t422_idmoneda_VDC = 60,
			t422_denominacionimportes_vdc = 61,
			t422_idmoneda_VDP = 62,
			t422_denominacionimportes_vdp = 63,
			t314_nuevogasvi = 64,
			t314_calculoonline = 65,
			t314_cargaestructura = 66,
			CVFinalizado = 67,
			PROFESIONAL_CVEXCLUSION = 68,
			RESPONSABLE_CVEXCLUSION = 69,
            sIDRED = 70
        }

        internal Usuario(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un Usuario
        ///// </summary>
        //internal int Insert(Models.Usuario oUsuario)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[69] {
        //            Param(enumDBFields.t001_IDFICEPI, oUsuario.t001_IDFICEPI),
        //            Param(enumDBFields.t314_idusuario, oUsuario.t314_idusuario),
        //            Param(enumDBFields.t001_cip, oUsuario.t001_cip),
        //            Param(enumDBFields.NOMBRE, oUsuario.NOMBRE),
        //            Param(enumDBFields.APELLIDO1, oUsuario.APELLIDO1),
        //            Param(enumDBFields.APELLIDO2, oUsuario.APELLIDO2),
        //            Param(enumDBFields.T009_IDCENTRAB, oUsuario.T009_IDCENTRAB),
        //            Param(enumDBFields.T009_DESCENTRAB, oUsuario.T009_DESCENTRAB),
        //            Param(enumDBFields.t303_idnodo, oUsuario.t303_idnodo),
        //            Param(enumDBFields.t303_ultcierreIAP, oUsuario.t303_ultcierreIAP),
        //            Param(enumDBFields.t303_denominacion, oUsuario.t303_denominacion),
        //            Param(enumDBFields.t399_figura, oUsuario.t399_figura),
        //            Param(enumDBFields.t399_figura_per, oUsuario.t399_figura_per),
        //            Param(enumDBFields.t399_figura_cvt, oUsuario.t399_figura_cvt),
        //            Param(enumDBFields.t314_falta, oUsuario.t314_falta),
        //            Param(enumDBFields.t314_fbaja, oUsuario.t314_fbaja),
        //            Param(enumDBFields.t314_jornadareducida, oUsuario.t314_jornadareducida),
        //            Param(enumDBFields.t314_horasjor_red, oUsuario.t314_horasjor_red),
        //            Param(enumDBFields.t314_fdesde_red, oUsuario.t314_fdesde_red),
        //            Param(enumDBFields.t314_fhasta_red, oUsuario.t314_fhasta_red),
        //            Param(enumDBFields.t314_controlhuecos, oUsuario.t314_controlhuecos),
        //            Param(enumDBFields.fUltImputacion, oUsuario.fUltImputacion),
        //            Param(enumDBFields.IdCalendario, oUsuario.IdCalendario),
        //            Param(enumDBFields.desCalendario, oUsuario.desCalendario),
        //            Param(enumDBFields.t066_semlabL, oUsuario.t066_semlabL),
        //            Param(enumDBFields.t066_semlabM, oUsuario.t066_semlabM),
        //            Param(enumDBFields.t066_semlabX, oUsuario.t066_semlabX),
        //            Param(enumDBFields.t066_semlabJ, oUsuario.t066_semlabJ),
        //            Param(enumDBFields.t066_semlabV, oUsuario.t066_semlabV),
        //            Param(enumDBFields.t066_semlabS, oUsuario.t066_semlabS),
        //            Param(enumDBFields.t066_semlabD, oUsuario.t066_semlabD),
        //            Param(enumDBFields.t001_codred, oUsuario.t001_codred),
        //            Param(enumDBFields.t001_sexo, oUsuario.t001_sexo),
        //            Param(enumDBFields.t314_crp, oUsuario.t314_crp),
        //            Param(enumDBFields.t314_accesohabilitado, oUsuario.t314_accesohabilitado),
        //            Param(enumDBFields.t314_diamante, oUsuario.t314_diamante),
        //            Param(enumDBFields.tipo, oUsuario.tipo),
        //            Param(enumDBFields.t314_nsegmb, oUsuario.t314_nsegmb),
        //            Param(enumDBFields.T010_CODWEATHER, oUsuario.T010_CODWEATHER),
        //            Param(enumDBFields.T010_NOMWEATHER, oUsuario.T010_NOMWEATHER),
        //            Param(enumDBFields.t314_carrusel1024, oUsuario.t314_carrusel1024),
        //            Param(enumDBFields.t314_avance1024, oUsuario.t314_avance1024),
        //            Param(enumDBFields.t314_resumen1024, oUsuario.t314_resumen1024),
        //            Param(enumDBFields.t314_datosres1024, oUsuario.t314_datosres1024),
        //            Param(enumDBFields.t314_fichaeco1024, oUsuario.t314_fichaeco1024),
        //            Param(enumDBFields.t314_segrenta1024, oUsuario.t314_segrenta1024),
        //            Param(enumDBFields.t314_avantec1024, oUsuario.t314_avantec1024),
        //            Param(enumDBFields.t314_estruct1024, oUsuario.t314_estruct1024),
        //            Param(enumDBFields.t314_fotopst1024, oUsuario.t314_fotopst1024),
        //            Param(enumDBFields.t314_plant1024, oUsuario.t314_plant1024),
        //            Param(enumDBFields.t314_const1024, oUsuario.t314_const1024),
        //            Param(enumDBFields.t314_iapfact1024, oUsuario.t314_iapfact1024),
        //            Param(enumDBFields.t314_iapdiario1024, oUsuario.t314_iapdiario1024),
        //            Param(enumDBFields.t314_cuadromando1024, oUsuario.t314_cuadromando1024),
        //            Param(enumDBFields.t314_importaciongasvi, oUsuario.t314_importaciongasvi),
        //            Param(enumDBFields.t314_recibirmails, oUsuario.t314_recibirmails),
        //            Param(enumDBFields.t314_defectoperiodificacion, oUsuario.t314_defectoperiodificacion),
        //            Param(enumDBFields.t314_multiventana, oUsuario.t314_multiventana),
        //            Param(enumDBFields.t001_botonfecha, oUsuario.t001_botonfecha),
        //            Param(enumDBFields.t422_idmoneda_VDC, oUsuario.t422_idmoneda_VDC),
        //            Param(enumDBFields.t422_denominacionimportes_vdc, oUsuario.t422_denominacionimportes_vdc),
        //            Param(enumDBFields.t422_idmoneda_VDP, oUsuario.t422_idmoneda_VDP),
        //            Param(enumDBFields.t422_denominacionimportes_vdp, oUsuario.t422_denominacionimportes_vdp),
        //            Param(enumDBFields.t314_nuevogasvi, oUsuario.t314_nuevogasvi),
        //            Param(enumDBFields.t314_calculoonline, oUsuario.t314_calculoonline),
        //            Param(enumDBFields.t314_cargaestructura, oUsuario.t314_cargaestructura),
        //            Param(enumDBFields.CVFinalizado, oUsuario.CVFinalizado),
        //            Param(enumDBFields.PROFESIONAL_CVEXCLUSION, oUsuario.PROFESIONAL_CVEXCLUSION),
        //            Param(enumDBFields.RESPONSABLE_CVEXCLUSION, oUsuario.RESPONSABLE_CVEXCLUSION)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_Usuario_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un Usuario a partir del id
        ///// </summary>
        internal Models.Usuario ObtenerRecurso(string sIDRED, int? t314_idusuario)
        {
            Models.Usuario oUsuario = null;
            SqlDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.sIDRED,sIDRED),
			        Param(enumDBFields.t314_idusuario, t314_idusuario),
				};

                dr = cDblib.DataReader("SUP_LOGIN", dbparams);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        oUsuario = new Models.Usuario();
                        oUsuario.t001_IDFICEPI = Convert.ToInt32(dr["t001_IDFICEPI"]);
                        oUsuario.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
                        oUsuario.t001_cip = Convert.ToString(dr["t001_cip"]);
                        if (!Convert.IsDBNull(dr["NOMBRE"]))
                            oUsuario.NOMBRE = Convert.ToString(dr["NOMBRE"]);
                        if (!Convert.IsDBNull(dr["APELLIDO1"]))
                            oUsuario.APELLIDO1 = Convert.ToString(dr["APELLIDO1"]);
                        if (!Convert.IsDBNull(dr["APELLIDO2"]))
                            oUsuario.APELLIDO2 = Convert.ToString(dr["APELLIDO2"]);
                        if (!Convert.IsDBNull(dr["T009_IDCENTRAB"]))
                            oUsuario.T009_IDCENTRAB = Convert.ToInt16(dr["T009_IDCENTRAB"]);
                        if (!Convert.IsDBNull(dr["T009_DESCENTRAB"]))
                            oUsuario.T009_DESCENTRAB = Convert.ToString(dr["T009_DESCENTRAB"]);
                        if (!Convert.IsDBNull(dr["t303_idnodo"]))
                            oUsuario.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                        if (!Convert.IsDBNull(dr["t303_ultcierreIAP"]))
                            oUsuario.t303_ultcierreIAP = Convert.ToInt32(dr["t303_ultcierreIAP"]);
                        if (!Convert.IsDBNull(dr["t303_denominacion"]))
                            oUsuario.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                        if (!Convert.IsDBNull(dr["t399_figura"]))
                            oUsuario.t399_figura = Convert.ToString(dr["t399_figura"]);
                        if (!Convert.IsDBNull(dr["t399_figura_per"]))
                            oUsuario.t399_figura_per = Convert.ToString(dr["t399_figura_per"]);
                        if (!Convert.IsDBNull(dr["t399_figura_cvt"]))
                            oUsuario.t399_figura_cvt = Convert.ToString(dr["t399_figura_cvt"]);
                        oUsuario.t314_falta = Convert.ToDateTime(dr["t314_falta"]);
                        if (!Convert.IsDBNull(dr["t314_fbaja"]))
                            oUsuario.t314_fbaja = Convert.ToDateTime(dr["t314_fbaja"]);
                        oUsuario.t314_jornadareducida = Convert.ToBoolean(dr["t314_jornadareducida"]);
                        oUsuario.t314_horasjor_red = Convert.ToSingle(dr["t314_horasjor_red"]);
                        if (!Convert.IsDBNull(dr["t314_fdesde_red"]))
                            oUsuario.t314_fdesde_red = Convert.ToDateTime(dr["t314_fdesde_red"]);
                        if (!Convert.IsDBNull(dr["t314_fhasta_red"]))
                            oUsuario.t314_fhasta_red = Convert.ToDateTime(dr["t314_fhasta_red"]);
                        oUsuario.t314_controlhuecos = Convert.ToBoolean(dr["t314_controlhuecos"]);

                        if (!Convert.IsDBNull(dr["fUltImputacion"]))
                        {
                            string sFecUltImputac = USUARIO.ObtenerFecUltImputac(null, oUsuario.t314_idusuario);
                            if (sFecUltImputac != "")
                                oUsuario.fUltImputacion = DateTime.Parse(sFecUltImputac);
                        }

                        oUsuario.IdCalendario = Convert.ToInt32(dr["IdCalendario"]);
                        oUsuario.desCalendario = Convert.ToString(dr["desCalendario"]);
                        oUsuario.t066_semlabL = Convert.ToInt32(dr["t066_semlabL"]);
                        oUsuario.t066_semlabM = Convert.ToInt32(dr["t066_semlabM"]);
                        oUsuario.t066_semlabX = Convert.ToInt32(dr["t066_semlabX"]);
                        oUsuario.t066_semlabJ = Convert.ToInt32(dr["t066_semlabJ"]);
                        oUsuario.t066_semlabV = Convert.ToInt32(dr["t066_semlabV"]);
                        oUsuario.t066_semlabS = Convert.ToInt32(dr["t066_semlabS"]);
                        oUsuario.t066_semlabD = Convert.ToInt32(dr["t066_semlabD"]);
                        oUsuario.t001_codred = Convert.ToString(dr["t001_codred"]);
                        oUsuario.t001_sexo = Convert.ToString(dr["t001_sexo"]);
                        oUsuario.t314_crp = Convert.ToBoolean(dr["t314_crp"]);
                        oUsuario.t314_accesohabilitado = Convert.ToBoolean(dr["t314_accesohabilitado"]);
                        oUsuario.t314_diamante = Convert.ToBoolean(dr["t314_diamante"]);
                        oUsuario.tipo = Convert.ToString(dr["tipo"]);
                        oUsuario.t314_nsegmb = Convert.ToByte(dr["t314_nsegmb"]);
                        if (!Convert.IsDBNull(dr["T010_CODWEATHER"]))
                            oUsuario.T010_CODWEATHER = Convert.ToString(dr["T010_CODWEATHER"]);
                        if (!Convert.IsDBNull(dr["T010_NOMWEATHER"]))
                            oUsuario.T010_NOMWEATHER = Convert.ToString(dr["T010_NOMWEATHER"]);
                        oUsuario.t314_carrusel1024 = Convert.ToBoolean(dr["t314_carrusel1024"]);
                        oUsuario.t314_avance1024 = Convert.ToBoolean(dr["t314_avance1024"]);
                        oUsuario.t314_resumen1024 = Convert.ToBoolean(dr["t314_resumen1024"]);
                        oUsuario.t314_datosres1024 = Convert.ToBoolean(dr["t314_datosres1024"]);
                        oUsuario.t314_fichaeco1024 = Convert.ToBoolean(dr["t314_fichaeco1024"]);
                        oUsuario.t314_segrenta1024 = Convert.ToBoolean(dr["t314_segrenta1024"]);
                        oUsuario.t314_avantec1024 = Convert.ToBoolean(dr["t314_avantec1024"]);
                        oUsuario.t314_estruct1024 = Convert.ToBoolean(dr["t314_estruct1024"]);
                        oUsuario.t314_fotopst1024 = Convert.ToBoolean(dr["t314_fotopst1024"]);
                        oUsuario.t314_plant1024 = Convert.ToBoolean(dr["t314_plant1024"]);
                        oUsuario.t314_const1024 = Convert.ToBoolean(dr["t314_const1024"]);
                        oUsuario.t314_iapfact1024 = Convert.ToBoolean(dr["t314_iapfact1024"]);
                        oUsuario.t314_iapdiario1024 = Convert.ToBoolean(dr["t314_iapdiario1024"]);
                        oUsuario.t314_cuadromando1024 = Convert.ToBoolean(dr["t314_cuadromando1024"]);
                        oUsuario.t314_importaciongasvi = Convert.ToByte(dr["t314_importaciongasvi"]);
                        oUsuario.t314_recibirmails = Convert.ToBoolean(dr["t314_recibirmails"]);
                        oUsuario.t314_defectoperiodificacion = Convert.ToBoolean(dr["t314_defectoperiodificacion"]);
                        oUsuario.t314_multiventana = Convert.ToBoolean(dr["t314_multiventana"]);
                        oUsuario.t001_botonfecha = Convert.ToString(dr["t001_botonfecha"]);
                        oUsuario.t422_idmoneda_VDC = Convert.ToString(dr["t422_idmoneda_VDC"]);
                        oUsuario.t422_denominacionimportes_vdc = Convert.ToString(dr["t422_denominacionimportes_vdc"]);
                        if (!Convert.IsDBNull(dr["t422_idmoneda_VDP"]))
                            oUsuario.t422_idmoneda_VDP = Convert.ToString(dr["t422_idmoneda_VDP"]);
                        if (!Convert.IsDBNull(dr["t422_denominacionimportes_vdp"]))
                            oUsuario.t422_denominacionimportes_vdp = Convert.ToString(dr["t422_denominacionimportes_vdp"]);
                        oUsuario.t314_nuevogasvi = Convert.ToBoolean(dr["t314_nuevogasvi"]);
                        oUsuario.t314_calculoonline = Convert.ToBoolean(dr["t314_calculoonline"]);
                        oUsuario.t314_cargaestructura = Convert.ToBoolean(dr["t314_cargaestructura"]);
                        oUsuario.CVFinalizado = Convert.ToBoolean(dr["CVFinalizado"]);
                        if (!Convert.IsDBNull(dr["PROFESIONAL_CVEXCLUSION"]))
                            oUsuario.PROFESIONAL_CVEXCLUSION = Convert.ToByte(dr["PROFESIONAL_CVEXCLUSION"]);
                        if (!Convert.IsDBNull(dr["RESPONSABLE_CVEXCLUSION"]))
                            oUsuario.RESPONSABLE_CVEXCLUSION = Convert.ToByte(dr["RESPONSABLE_CVEXCLUSION"]);
                    }

                }
                return oUsuario;
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
        ///// Obtiene un Usuario a partir del id
        ///// </summary>
        internal Models.Usuario ObtenerRecursoReducido(int t314_idusuario)
        {
            Models.Usuario oUsuario = null;
            SqlDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {                    
                    Param(enumDBFields.t314_idusuario, t314_idusuario),
                };

                dr = cDblib.DataReader("SUP_LOGIN_IAP30", dbparams);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        oUsuario = new Models.Usuario();
                        oUsuario.t001_IDFICEPI = Convert.ToInt32(dr["t001_idficepi"]);
                        oUsuario.t314_idusuario = t314_idusuario;                        
                        if (!Convert.IsDBNull(dr["t001_nombre"]))
                            oUsuario.NOMBRE = Convert.ToString(dr["t001_nombre"]);
                        if (!Convert.IsDBNull(dr["t001_apellido1"]))
                            oUsuario.APELLIDO1 = Convert.ToString(dr["t001_apellido1"]);
                        if (!Convert.IsDBNull(dr["t001_apellido2"]))
                            oUsuario.APELLIDO2 = Convert.ToString(dr["t001_apellido2"]);
                        /*if (!Convert.IsDBNull(dr["t303_idnodo"]))
                            oUsuario.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);*/
                        if (!Convert.IsDBNull(dr["t303_ultcierreIAP"]))
                            oUsuario.t303_ultcierreIAP = Convert.ToInt32(dr["t303_ultcierreIAP"]);
                        /*if (!Convert.IsDBNull(dr["t303_denominacion"]))
                            oUsuario.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);*/
                        oUsuario.t314_falta = Convert.ToDateTime(dr["t314_falta"]);
                        if (!Convert.IsDBNull(dr["t314_fbaja"]))
                            oUsuario.t314_fbaja = Convert.ToDateTime(dr["t314_fbaja"]);
                        oUsuario.t314_jornadareducida = Convert.ToBoolean(dr["t314_jornadareducida"]);
                        oUsuario.t314_horasjor_red = Convert.ToSingle(dr["t314_horasjor_red"]);
                        if (!Convert.IsDBNull(dr["t314_fdesde_red"]))
                            oUsuario.t314_fdesde_red = Convert.ToDateTime(dr["t314_fdesde_red"]);
                        if (!Convert.IsDBNull(dr["t314_fhasta_red"]))
                            oUsuario.t314_fhasta_red = Convert.ToDateTime(dr["t314_fhasta_red"]);
                        oUsuario.t314_controlhuecos = Convert.ToBoolean(dr["t314_controlhuecos"]);

                        
                        string sFecUltImputac = USUARIO.ObtenerFecUltImputac(null, oUsuario.t314_idusuario);
                        if (sFecUltImputac != "")
                            oUsuario.fUltImputacion = DateTime.Parse(sFecUltImputac);
                        

                        oUsuario.IdCalendario = Convert.ToInt32(dr["t066_idcal"]);
                        oUsuario.desCalendario = Convert.ToString(dr["t066_descal"]);
                        oUsuario.t066_semlabL = Convert.ToInt32(dr["t066_semlabL"]);
                        oUsuario.t066_semlabM = Convert.ToInt32(dr["t066_semlabM"]);
                        oUsuario.t066_semlabX = Convert.ToInt32(dr["t066_semlabX"]);
                        oUsuario.t066_semlabJ = Convert.ToInt32(dr["t066_semlabJ"]);
                        oUsuario.t066_semlabV = Convert.ToInt32(dr["t066_semlabV"]);
                        oUsuario.t066_semlabS = Convert.ToInt32(dr["t066_semlabS"]);
                        oUsuario.t066_semlabD = Convert.ToInt32(dr["t066_semlabD"]);
                        oUsuario.t001_codred = Convert.ToString(dr["t001_codred"]);
                        oUsuario.t001_sexo = Convert.ToString(dr["t001_sexo"]);
                           
                    }

                }
                return oUsuario;
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

        internal Models.Usuario GetFecUltImputacion(int t314_idusuario)
        {
            Models.Usuario oUsuario = null;
            SqlDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
			        Param(enumDBFields.t314_idusuario, t314_idusuario),
				};

                dr = cDblib.DataReader("SUP_CONSUMOIAPMAXS", dbparams);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        oUsuario = new Models.Usuario();
                        oUsuario.t314_idusuario = t314_idusuario;
                        if (!Convert.IsDBNull(dr["t337_fecha"]))
                            oUsuario.fUltImputacion = DateTime.Parse(dr["t337_fecha"].ToString());
                    }

                }
                return oUsuario;
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
        ///// Actualiza un Usuario a partir del id
        ///// </summary>
        //internal int Update(Models.Usuario oUsuario)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[69] {
        //            Param(enumDBFields.t001_IDFICEPI, oUsuario.t001_IDFICEPI),
        //            Param(enumDBFields.t314_idusuario, oUsuario.t314_idusuario),
        //            Param(enumDBFields.t001_cip, oUsuario.t001_cip),
        //            Param(enumDBFields.NOMBRE, oUsuario.NOMBRE),
        //            Param(enumDBFields.APELLIDO1, oUsuario.APELLIDO1),
        //            Param(enumDBFields.APELLIDO2, oUsuario.APELLIDO2),
        //            Param(enumDBFields.T009_IDCENTRAB, oUsuario.T009_IDCENTRAB),
        //            Param(enumDBFields.T009_DESCENTRAB, oUsuario.T009_DESCENTRAB),
        //            Param(enumDBFields.t303_idnodo, oUsuario.t303_idnodo),
        //            Param(enumDBFields.t303_ultcierreIAP, oUsuario.t303_ultcierreIAP),
        //            Param(enumDBFields.t303_denominacion, oUsuario.t303_denominacion),
        //            Param(enumDBFields.t399_figura, oUsuario.t399_figura),
        //            Param(enumDBFields.t399_figura_per, oUsuario.t399_figura_per),
        //            Param(enumDBFields.t399_figura_cvt, oUsuario.t399_figura_cvt),
        //            Param(enumDBFields.t314_falta, oUsuario.t314_falta),
        //            Param(enumDBFields.t314_fbaja, oUsuario.t314_fbaja),
        //            Param(enumDBFields.t314_jornadareducida, oUsuario.t314_jornadareducida),
        //            Param(enumDBFields.t314_horasjor_red, oUsuario.t314_horasjor_red),
        //            Param(enumDBFields.t314_fdesde_red, oUsuario.t314_fdesde_red),
        //            Param(enumDBFields.t314_fhasta_red, oUsuario.t314_fhasta_red),
        //            Param(enumDBFields.t314_controlhuecos, oUsuario.t314_controlhuecos),
        //            Param(enumDBFields.fUltImputacion, oUsuario.fUltImputacion),
        //            Param(enumDBFields.IdCalendario, oUsuario.IdCalendario),
        //            Param(enumDBFields.desCalendario, oUsuario.desCalendario),
        //            Param(enumDBFields.t066_semlabL, oUsuario.t066_semlabL),
        //            Param(enumDBFields.t066_semlabM, oUsuario.t066_semlabM),
        //            Param(enumDBFields.t066_semlabX, oUsuario.t066_semlabX),
        //            Param(enumDBFields.t066_semlabJ, oUsuario.t066_semlabJ),
        //            Param(enumDBFields.t066_semlabV, oUsuario.t066_semlabV),
        //            Param(enumDBFields.t066_semlabS, oUsuario.t066_semlabS),
        //            Param(enumDBFields.t066_semlabD, oUsuario.t066_semlabD),
        //            Param(enumDBFields.t001_codred, oUsuario.t001_codred),
        //            Param(enumDBFields.t001_sexo, oUsuario.t001_sexo),
        //            Param(enumDBFields.t314_crp, oUsuario.t314_crp),
        //            Param(enumDBFields.t314_accesohabilitado, oUsuario.t314_accesohabilitado),
        //            Param(enumDBFields.t314_diamante, oUsuario.t314_diamante),
        //            Param(enumDBFields.tipo, oUsuario.tipo),
        //            Param(enumDBFields.t314_nsegmb, oUsuario.t314_nsegmb),
        //            Param(enumDBFields.T010_CODWEATHER, oUsuario.T010_CODWEATHER),
        //            Param(enumDBFields.T010_NOMWEATHER, oUsuario.T010_NOMWEATHER),
        //            Param(enumDBFields.t314_carrusel1024, oUsuario.t314_carrusel1024),
        //            Param(enumDBFields.t314_avance1024, oUsuario.t314_avance1024),
        //            Param(enumDBFields.t314_resumen1024, oUsuario.t314_resumen1024),
        //            Param(enumDBFields.t314_datosres1024, oUsuario.t314_datosres1024),
        //            Param(enumDBFields.t314_fichaeco1024, oUsuario.t314_fichaeco1024),
        //            Param(enumDBFields.t314_segrenta1024, oUsuario.t314_segrenta1024),
        //            Param(enumDBFields.t314_avantec1024, oUsuario.t314_avantec1024),
        //            Param(enumDBFields.t314_estruct1024, oUsuario.t314_estruct1024),
        //            Param(enumDBFields.t314_fotopst1024, oUsuario.t314_fotopst1024),
        //            Param(enumDBFields.t314_plant1024, oUsuario.t314_plant1024),
        //            Param(enumDBFields.t314_const1024, oUsuario.t314_const1024),
        //            Param(enumDBFields.t314_iapfact1024, oUsuario.t314_iapfact1024),
        //            Param(enumDBFields.t314_iapdiario1024, oUsuario.t314_iapdiario1024),
        //            Param(enumDBFields.t314_cuadromando1024, oUsuario.t314_cuadromando1024),
        //            Param(enumDBFields.t314_importaciongasvi, oUsuario.t314_importaciongasvi),
        //            Param(enumDBFields.t314_recibirmails, oUsuario.t314_recibirmails),
        //            Param(enumDBFields.t314_defectoperiodificacion, oUsuario.t314_defectoperiodificacion),
        //            Param(enumDBFields.t314_multiventana, oUsuario.t314_multiventana),
        //            Param(enumDBFields.t001_botonfecha, oUsuario.t001_botonfecha),
        //            Param(enumDBFields.t422_idmoneda_VDC, oUsuario.t422_idmoneda_VDC),
        //            Param(enumDBFields.t422_denominacionimportes_vdc, oUsuario.t422_denominacionimportes_vdc),
        //            Param(enumDBFields.t422_idmoneda_VDP, oUsuario.t422_idmoneda_VDP),
        //            Param(enumDBFields.t422_denominacionimportes_vdp, oUsuario.t422_denominacionimportes_vdp),
        //            Param(enumDBFields.t314_nuevogasvi, oUsuario.t314_nuevogasvi),
        //            Param(enumDBFields.t314_calculoonline, oUsuario.t314_calculoonline),
        //            Param(enumDBFields.t314_cargaestructura, oUsuario.t314_cargaestructura),
        //            Param(enumDBFields.CVFinalizado, oUsuario.CVFinalizado),
        //            Param(enumDBFields.PROFESIONAL_CVEXCLUSION, oUsuario.PROFESIONAL_CVEXCLUSION),
        //            Param(enumDBFields.RESPONSABLE_CVEXCLUSION, oUsuario.RESPONSABLE_CVEXCLUSION)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_Usuario_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un Usuario a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_Usuario_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los Usuario
        ///// </summary>
        //internal List<Models.Usuario> Catalogo(Models.Usuario oUsuarioFilter)
        //{
        //    Models.Usuario oUsuario = null;
        //    List<Models.Usuario> lst = new List<Models.Usuario>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[69] {
        //            Param(enumDBFields.t001_IDFICEPI, oTEMP_UsuarioFilter.t001_IDFICEPI),
        //            Param(enumDBFields.t314_idusuario, oTEMP_UsuarioFilter.t314_idusuario),
        //            Param(enumDBFields.t001_cip, oTEMP_UsuarioFilter.t001_cip),
        //            Param(enumDBFields.NOMBRE, oTEMP_UsuarioFilter.NOMBRE),
        //            Param(enumDBFields.APELLIDO1, oTEMP_UsuarioFilter.APELLIDO1),
        //            Param(enumDBFields.APELLIDO2, oTEMP_UsuarioFilter.APELLIDO2),
        //            Param(enumDBFields.T009_IDCENTRAB, oTEMP_UsuarioFilter.T009_IDCENTRAB),
        //            Param(enumDBFields.T009_DESCENTRAB, oTEMP_UsuarioFilter.T009_DESCENTRAB),
        //            Param(enumDBFields.t303_idnodo, oTEMP_UsuarioFilter.t303_idnodo),
        //            Param(enumDBFields.t303_ultcierreIAP, oTEMP_UsuarioFilter.t303_ultcierreIAP),
        //            Param(enumDBFields.t303_denominacion, oTEMP_UsuarioFilter.t303_denominacion),
        //            Param(enumDBFields.t399_figura, oTEMP_UsuarioFilter.t399_figura),
        //            Param(enumDBFields.t399_figura_per, oTEMP_UsuarioFilter.t399_figura_per),
        //            Param(enumDBFields.t399_figura_cvt, oTEMP_UsuarioFilter.t399_figura_cvt),
        //            Param(enumDBFields.t314_falta, oTEMP_UsuarioFilter.t314_falta),
        //            Param(enumDBFields.t314_fbaja, oTEMP_UsuarioFilter.t314_fbaja),
        //            Param(enumDBFields.t314_jornadareducida, oTEMP_UsuarioFilter.t314_jornadareducida),
        //            Param(enumDBFields.t314_horasjor_red, oTEMP_UsuarioFilter.t314_horasjor_red),
        //            Param(enumDBFields.t314_fdesde_red, oTEMP_UsuarioFilter.t314_fdesde_red),
        //            Param(enumDBFields.t314_fhasta_red, oTEMP_UsuarioFilter.t314_fhasta_red),
        //            Param(enumDBFields.t314_controlhuecos, oTEMP_UsuarioFilter.t314_controlhuecos),
        //            Param(enumDBFields.fUltImputacion, oTEMP_UsuarioFilter.fUltImputacion),
        //            Param(enumDBFields.IdCalendario, oTEMP_UsuarioFilter.IdCalendario),
        //            Param(enumDBFields.desCalendario, oTEMP_UsuarioFilter.desCalendario),
        //            Param(enumDBFields.t066_semlabL, oTEMP_UsuarioFilter.t066_semlabL),
        //            Param(enumDBFields.t066_semlabM, oTEMP_UsuarioFilter.t066_semlabM),
        //            Param(enumDBFields.t066_semlabX, oTEMP_UsuarioFilter.t066_semlabX),
        //            Param(enumDBFields.t066_semlabJ, oTEMP_UsuarioFilter.t066_semlabJ),
        //            Param(enumDBFields.t066_semlabV, oTEMP_UsuarioFilter.t066_semlabV),
        //            Param(enumDBFields.t066_semlabS, oTEMP_UsuarioFilter.t066_semlabS),
        //            Param(enumDBFields.t066_semlabD, oTEMP_UsuarioFilter.t066_semlabD),
        //            Param(enumDBFields.t001_codred, oTEMP_UsuarioFilter.t001_codred),
        //            Param(enumDBFields.t001_sexo, oTEMP_UsuarioFilter.t001_sexo),
        //            Param(enumDBFields.t314_crp, oTEMP_UsuarioFilter.t314_crp),
        //            Param(enumDBFields.t314_accesohabilitado, oTEMP_UsuarioFilter.t314_accesohabilitado),
        //            Param(enumDBFields.t314_diamante, oTEMP_UsuarioFilter.t314_diamante),
        //            Param(enumDBFields.tipo, oTEMP_UsuarioFilter.tipo),
        //            Param(enumDBFields.t314_nsegmb, oTEMP_UsuarioFilter.t314_nsegmb),
        //            Param(enumDBFields.T010_CODWEATHER, oTEMP_UsuarioFilter.T010_CODWEATHER),
        //            Param(enumDBFields.T010_NOMWEATHER, oTEMP_UsuarioFilter.T010_NOMWEATHER),
        //            Param(enumDBFields.t314_carrusel1024, oTEMP_UsuarioFilter.t314_carrusel1024),
        //            Param(enumDBFields.t314_avance1024, oTEMP_UsuarioFilter.t314_avance1024),
        //            Param(enumDBFields.t314_resumen1024, oTEMP_UsuarioFilter.t314_resumen1024),
        //            Param(enumDBFields.t314_datosres1024, oTEMP_UsuarioFilter.t314_datosres1024),
        //            Param(enumDBFields.t314_fichaeco1024, oTEMP_UsuarioFilter.t314_fichaeco1024),
        //            Param(enumDBFields.t314_segrenta1024, oTEMP_UsuarioFilter.t314_segrenta1024),
        //            Param(enumDBFields.t314_avantec1024, oTEMP_UsuarioFilter.t314_avantec1024),
        //            Param(enumDBFields.t314_estruct1024, oTEMP_UsuarioFilter.t314_estruct1024),
        //            Param(enumDBFields.t314_fotopst1024, oTEMP_UsuarioFilter.t314_fotopst1024),
        //            Param(enumDBFields.t314_plant1024, oTEMP_UsuarioFilter.t314_plant1024),
        //            Param(enumDBFields.t314_const1024, oTEMP_UsuarioFilter.t314_const1024),
        //            Param(enumDBFields.t314_iapfact1024, oTEMP_UsuarioFilter.t314_iapfact1024),
        //            Param(enumDBFields.t314_iapdiario1024, oTEMP_UsuarioFilter.t314_iapdiario1024),
        //            Param(enumDBFields.t314_cuadromando1024, oTEMP_UsuarioFilter.t314_cuadromando1024),
        //            Param(enumDBFields.t314_importaciongasvi, oTEMP_UsuarioFilter.t314_importaciongasvi),
        //            Param(enumDBFields.t314_recibirmails, oTEMP_UsuarioFilter.t314_recibirmails),
        //            Param(enumDBFields.t314_defectoperiodificacion, oTEMP_UsuarioFilter.t314_defectoperiodificacion),
        //            Param(enumDBFields.t314_multiventana, oTEMP_UsuarioFilter.t314_multiventana),
        //            Param(enumDBFields.t001_botonfecha, oTEMP_UsuarioFilter.t001_botonfecha),
        //            Param(enumDBFields.t422_idmoneda_VDC, oTEMP_UsuarioFilter.t422_idmoneda_VDC),
        //            Param(enumDBFields.t422_denominacionimportes_vdc, oTEMP_UsuarioFilter.t422_denominacionimportes_vdc),
        //            Param(enumDBFields.t422_idmoneda_VDP, oTEMP_UsuarioFilter.t422_idmoneda_VDP),
        //            Param(enumDBFields.t422_denominacionimportes_vdp, oTEMP_UsuarioFilter.t422_denominacionimportes_vdp),
        //            Param(enumDBFields.t314_nuevogasvi, oTEMP_UsuarioFilter.t314_nuevogasvi),
        //            Param(enumDBFields.t314_calculoonline, oTEMP_UsuarioFilter.t314_calculoonline),
        //            Param(enumDBFields.t314_cargaestructura, oTEMP_UsuarioFilter.t314_cargaestructura),
        //            Param(enumDBFields.CVFinalizado, oTEMP_UsuarioFilter.CVFinalizado),
        //            Param(enumDBFields.PROFESIONAL_CVEXCLUSION, oTEMP_UsuarioFilter.PROFESIONAL_CVEXCLUSION),
        //            Param(enumDBFields.RESPONSABLE_CVEXCLUSION, oTEMP_UsuarioFilter.RESPONSABLE_CVEXCLUSION)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_Usuario_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oUsuario = new Models.Usuario();
        //            oUsuario.t001_IDFICEPI=Convert.ToInt32(dr["t001_IDFICEPI"]);
        //            oUsuario.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            oUsuario.t001_cip=Convert.ToString(dr["t001_cip"]);
        //            if(!Convert.IsDBNull(dr["NOMBRE"]))
        //                oUsuario.NOMBRE=Convert.ToString(dr["NOMBRE"]);
        //            if(!Convert.IsDBNull(dr["APELLIDO1"]))
        //                oUsuario.APELLIDO1=Convert.ToString(dr["APELLIDO1"]);
        //            if(!Convert.IsDBNull(dr["APELLIDO2"]))
        //                oUsuario.APELLIDO2=Convert.ToString(dr["APELLIDO2"]);
        //            if(!Convert.IsDBNull(dr["T009_IDCENTRAB"]))
        //                oUsuario.T009_IDCENTRAB=Convert.ToInt16(dr["T009_IDCENTRAB"]);
        //            if(!Convert.IsDBNull(dr["T009_DESCENTRAB"]))
        //                oUsuario.T009_DESCENTRAB=Convert.ToString(dr["T009_DESCENTRAB"]);
        //            if(!Convert.IsDBNull(dr["t303_idnodo"]))
        //                oUsuario.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
        //            if(!Convert.IsDBNull(dr["t303_ultcierreIAP"]))
        //                oUsuario.t303_ultcierreIAP=Convert.ToInt32(dr["t303_ultcierreIAP"]);
        //            if(!Convert.IsDBNull(dr["t303_denominacion"]))
        //                oUsuario.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
        //            if(!Convert.IsDBNull(dr["t399_figura"]))
        //                oUsuario.t399_figura=Convert.ToString(dr["t399_figura"]);
        //            if(!Convert.IsDBNull(dr["t399_figura_per"]))
        //                oUsuario.t399_figura_per=Convert.ToString(dr["t399_figura_per"]);
        //            if(!Convert.IsDBNull(dr["t399_figura_cvt"]))
        //                oUsuario.t399_figura_cvt=Convert.ToString(dr["t399_figura_cvt"]);
        //            oUsuario.t314_falta=Convert.ToDateTime(dr["t314_falta"]);
        //            if(!Convert.IsDBNull(dr["t314_fbaja"]))
        //                oUsuario.t314_fbaja=Convert.ToDateTime(dr["t314_fbaja"]);
        //            oUsuario.t314_jornadareducida=Convert.ToBoolean(dr["t314_jornadareducida"]);
        //            oUsuario.t314_horasjor_red=Convert.ToSingle(dr["t314_horasjor_red"]);
        //            if(!Convert.IsDBNull(dr["t314_fdesde_red"]))
        //                oUsuario.t314_fdesde_red=Convert.ToDateTime(dr["t314_fdesde_red"]);
        //            if(!Convert.IsDBNull(dr["t314_fhasta_red"]))
        //                oUsuario.t314_fhasta_red=Convert.ToDateTime(dr["t314_fhasta_red"]);
        //            oUsuario.t314_controlhuecos=Convert.ToBoolean(dr["t314_controlhuecos"]);
        //            if(!Convert.IsDBNull(dr["fUltImputacion"]))
        //                oUsuario.fUltImputacion=Convert.ToDateTime(dr["fUltImputacion"]);
        //            oUsuario.IdCalendario=Convert.ToInt32(dr["IdCalendario"]);
        //            oUsuario.desCalendario=Convert.ToString(dr["desCalendario"]);
        //            oUsuario.t066_semlabL=Convert.ToInt32(dr["t066_semlabL"]);
        //            oUsuario.t066_semlabM=Convert.ToInt32(dr["t066_semlabM"]);
        //            oUsuario.t066_semlabX=Convert.ToInt32(dr["t066_semlabX"]);
        //            oUsuario.t066_semlabJ=Convert.ToInt32(dr["t066_semlabJ"]);
        //            oUsuario.t066_semlabV=Convert.ToInt32(dr["t066_semlabV"]);
        //            oUsuario.t066_semlabS=Convert.ToInt32(dr["t066_semlabS"]);
        //            oUsuario.t066_semlabD=Convert.ToInt32(dr["t066_semlabD"]);
        //            oUsuario.t001_codred=Convert.ToString(dr["t001_codred"]);
        //            oUsuario.t001_sexo=Convert.ToString(dr["t001_sexo"]);
        //            oUsuario.t314_crp=Convert.ToBoolean(dr["t314_crp"]);
        //            oUsuario.t314_accesohabilitado=Convert.ToBoolean(dr["t314_accesohabilitado"]);
        //            oUsuario.t314_diamante=Convert.ToBoolean(dr["t314_diamante"]);
        //            oUsuario.tipo=Convert.ToString(dr["tipo"]);
        //            oUsuario.t314_nsegmb=Convert.ToByte(dr["t314_nsegmb"]);
        //            if(!Convert.IsDBNull(dr["T010_CODWEATHER"]))
        //                oUsuario.T010_CODWEATHER=Convert.ToString(dr["T010_CODWEATHER"]);
        //            if(!Convert.IsDBNull(dr["T010_NOMWEATHER"]))
        //                oUsuario.T010_NOMWEATHER=Convert.ToString(dr["T010_NOMWEATHER"]);
        //            oUsuario.t314_carrusel1024=Convert.ToBoolean(dr["t314_carrusel1024"]);
        //            oUsuario.t314_avance1024=Convert.ToBoolean(dr["t314_avance1024"]);
        //            oUsuario.t314_resumen1024=Convert.ToBoolean(dr["t314_resumen1024"]);
        //            oUsuario.t314_datosres1024=Convert.ToBoolean(dr["t314_datosres1024"]);
        //            oUsuario.t314_fichaeco1024=Convert.ToBoolean(dr["t314_fichaeco1024"]);
        //            oUsuario.t314_segrenta1024=Convert.ToBoolean(dr["t314_segrenta1024"]);
        //            oUsuario.t314_avantec1024=Convert.ToBoolean(dr["t314_avantec1024"]);
        //            oUsuario.t314_estruct1024=Convert.ToBoolean(dr["t314_estruct1024"]);
        //            oUsuario.t314_fotopst1024=Convert.ToBoolean(dr["t314_fotopst1024"]);
        //            oUsuario.t314_plant1024=Convert.ToBoolean(dr["t314_plant1024"]);
        //            oUsuario.t314_const1024=Convert.ToBoolean(dr["t314_const1024"]);
        //            oUsuario.t314_iapfact1024=Convert.ToBoolean(dr["t314_iapfact1024"]);
        //            oUsuario.t314_iapdiario1024=Convert.ToBoolean(dr["t314_iapdiario1024"]);
        //            oUsuario.t314_cuadromando1024=Convert.ToBoolean(dr["t314_cuadromando1024"]);
        //            oUsuario.t314_importaciongasvi=Convert.ToByte(dr["t314_importaciongasvi"]);
        //            oUsuario.t314_recibirmails=Convert.ToBoolean(dr["t314_recibirmails"]);
        //            oUsuario.t314_defectoperiodificacion=Convert.ToBoolean(dr["t314_defectoperiodificacion"]);
        //            oUsuario.t314_multiventana=Convert.ToBoolean(dr["t314_multiventana"]);
        //            oUsuario.t001_botonfecha=Convert.ToString(dr["t001_botonfecha"]);
        //            oUsuario.t422_idmoneda_VDC=Convert.ToString(dr["t422_idmoneda_VDC"]);
        //            oUsuario.t422_denominacionimportes_vdc=Convert.ToString(dr["t422_denominacionimportes_vdc"]);
        //            if(!Convert.IsDBNull(dr["t422_idmoneda_VDP"]))
        //                oUsuario.t422_idmoneda_VDP=Convert.ToString(dr["t422_idmoneda_VDP"]);
        //            if(!Convert.IsDBNull(dr["t422_denominacionimportes_vdp"]))
        //                oUsuario.t422_denominacionimportes_vdp=Convert.ToString(dr["t422_denominacionimportes_vdp"]);
        //            oUsuario.t314_nuevogasvi=Convert.ToBoolean(dr["t314_nuevogasvi"]);
        //            oUsuario.t314_calculoonline=Convert.ToBoolean(dr["t314_calculoonline"]);
        //            oUsuario.t314_cargaestructura=Convert.ToBoolean(dr["t314_cargaestructura"]);
        //            oUsuario.CVFinalizado=Convert.ToBoolean(dr["CVFinalizado"]);
        //            if(!Convert.IsDBNull(dr["PROFESIONAL_CVEXCLUSION"]))
        //                oUsuario.PROFESIONAL_CVEXCLUSION=Convert.ToByte(dr["PROFESIONAL_CVEXCLUSION"]);
        //            if(!Convert.IsDBNull(dr["RESPONSABLE_CVEXCLUSION"]))
        //                oUsuario.RESPONSABLE_CVEXCLUSION=Convert.ToByte(dr["RESPONSABLE_CVEXCLUSION"]);

        //            lst.Add(oUsuario);

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
				case enumDBFields.t001_cip:
					paramName = "@t001_cip";
					paramType = SqlDbType.VarChar;
					paramSize = 15;
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
				case enumDBFields.t399_figura_per:
					paramName = "@t399_figura_per";
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
				case enumDBFields.t001_botonfecha:
					paramName = "@t001_botonfecha";
					paramType = SqlDbType.Char;
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
				case enumDBFields.CVFinalizado:
					paramName = "@CVFinalizado";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.PROFESIONAL_CVEXCLUSION:
					paramName = "@PROFESIONAL_CVEXCLUSION";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.RESPONSABLE_CVEXCLUSION:
					paramName = "@RESPONSABLE_CVEXCLUSION";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.sIDRED:
					paramName = "@sIDRED";
					paramType = SqlDbType.VarChar;
					paramSize = 15;
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
