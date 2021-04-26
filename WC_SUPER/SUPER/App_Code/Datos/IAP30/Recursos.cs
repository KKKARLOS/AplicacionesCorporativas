using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;
using SUPER.Capa_Negocio;

/// <summary>
/// Summary description for Recursos
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class Recursos 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            sIDRED = 1,
			t314_idusuario = 2
        }

        internal Recursos(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene un Recursos a partir del id
        /// </summary>
        internal Models.Recursos Select(string sIDRED, int t314_idusuario)
        {
            Models.Recursos oRecursos = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2]
                {
                    Param(enumDBFields.sIDRED, sIDRED),
					Param(enumDBFields.t314_idusuario, t314_idusuario)
                };
				dr = cDblib.DataReader("SUP_LOGIN_BAJA", dbparams);
				if (dr.Read())
				{
					oRecursos = new Models.Recursos();
					oRecursos.t001_IDFICEPI=Convert.ToInt32(dr["t001_IDFICEPI"]);
					oRecursos.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
					if(!Convert.IsDBNull(dr["NOMBRE"]))
						oRecursos.NOMBRE=Convert.ToString(dr["NOMBRE"]);
					if(!Convert.IsDBNull(dr["APELLIDO1"]))
						oRecursos.APELLIDO1=Convert.ToString(dr["APELLIDO1"]);
					if(!Convert.IsDBNull(dr["APELLIDO2"]))
						oRecursos.APELLIDO2=Convert.ToString(dr["APELLIDO2"]);
					if(!Convert.IsDBNull(dr["T009_IDCENTRAB"]))
						oRecursos.T009_IDCENTRAB=Convert.ToInt16(dr["T009_IDCENTRAB"]);
					if(!Convert.IsDBNull(dr["T009_DESCENTRAB"]))
						oRecursos.T009_DESCENTRAB=Convert.ToString(dr["T009_DESCENTRAB"]);
					if(!Convert.IsDBNull(dr["t303_idnodo"]))
						oRecursos.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
					if(!Convert.IsDBNull(dr["t303_ultcierreIAP"]))
						oRecursos.t303_ultcierreIAP=Convert.ToInt32(dr["t303_ultcierreIAP"]);
					if(!Convert.IsDBNull(dr["t303_denominacion"]))
						oRecursos.t303_denominacion=Convert.ToString(dr["t303_denominacion"]);
					if(!Convert.IsDBNull(dr["t399_figura"]))
						oRecursos.t399_figura=Convert.ToString(dr["t399_figura"]);
					if(!Convert.IsDBNull(dr["t399_figura_cvt"]))
						oRecursos.t399_figura_cvt=Convert.ToString(dr["t399_figura_cvt"]);
					oRecursos.t314_falta=Convert.ToDateTime(dr["t314_falta"]);
					if(!Convert.IsDBNull(dr["t314_fbaja"]))
						oRecursos.t314_fbaja=Convert.ToDateTime(dr["t314_fbaja"]);
					oRecursos.t314_jornadareducida=Convert.ToBoolean(dr["t314_jornadareducida"]);
					oRecursos.t314_horasjor_red=Convert.ToSingle(dr["t314_horasjor_red"]);
					if(!Convert.IsDBNull(dr["t314_fdesde_red"]))
						oRecursos.t314_fdesde_red=Convert.ToDateTime(dr["t314_fdesde_red"]);
					if(!Convert.IsDBNull(dr["t314_fhasta_red"]))
						oRecursos.t314_fhasta_red=Convert.ToDateTime(dr["t314_fhasta_red"]);
					oRecursos.t314_controlhuecos=Convert.ToBoolean(dr["t314_controlhuecos"]);
                    
                    string sFecUltImputac = USUARIO.ObtenerFecUltImputac(null, t314_idusuario);
                    if (sFecUltImputac != "")
                        oRecursos.fUltImputacion = DateTime.Parse(sFecUltImputac);
                    
					/*if(!Convert.IsDBNull(dr["fUltImputacion"]))
						oRecursos.fUltImputacion=Convert.ToDateTime(dr["fUltImputacion"]);*/
					oRecursos.IdCalendario=Convert.ToInt32(dr["IdCalendario"]);
					oRecursos.desCalendario=Convert.ToString(dr["desCalendario"]);
					oRecursos.t066_semlabL=Convert.ToInt32(dr["t066_semlabL"]);
					oRecursos.t066_semlabM=Convert.ToInt32(dr["t066_semlabM"]);
					oRecursos.t066_semlabX=Convert.ToInt32(dr["t066_semlabX"]);
					oRecursos.t066_semlabJ=Convert.ToInt32(dr["t066_semlabJ"]);
					oRecursos.t066_semlabV=Convert.ToInt32(dr["t066_semlabV"]);
					oRecursos.t066_semlabS=Convert.ToInt32(dr["t066_semlabS"]);
					oRecursos.t066_semlabD=Convert.ToInt32(dr["t066_semlabD"]);
					oRecursos.t001_codred=Convert.ToString(dr["t001_codred"]);
					oRecursos.t001_sexo=Convert.ToString(dr["t001_sexo"]);
					oRecursos.t314_crp=Convert.ToBoolean(dr["t314_crp"]);
					oRecursos.t314_accesohabilitado=Convert.ToBoolean(dr["t314_accesohabilitado"]);
					oRecursos.t314_diamante=Convert.ToBoolean(dr["t314_diamante"]);
					oRecursos.tipo=Convert.ToString(dr["tipo"]);
					oRecursos.t314_nsegmb=Convert.ToByte(dr["t314_nsegmb"]);
					if(!Convert.IsDBNull(dr["T010_CODWEATHER"]))
						oRecursos.T010_CODWEATHER=Convert.ToString(dr["T010_CODWEATHER"]);
					if(!Convert.IsDBNull(dr["T010_NOMWEATHER"]))
						oRecursos.T010_NOMWEATHER=Convert.ToString(dr["T010_NOMWEATHER"]);
					oRecursos.t314_carrusel1024=Convert.ToBoolean(dr["t314_carrusel1024"]);
					oRecursos.t314_avance1024=Convert.ToBoolean(dr["t314_avance1024"]);
					oRecursos.t314_resumen1024=Convert.ToBoolean(dr["t314_resumen1024"]);
					oRecursos.t314_datosres1024=Convert.ToBoolean(dr["t314_datosres1024"]);
					oRecursos.t314_fichaeco1024=Convert.ToBoolean(dr["t314_fichaeco1024"]);
					oRecursos.t314_segrenta1024=Convert.ToBoolean(dr["t314_segrenta1024"]);
					oRecursos.t314_avantec1024=Convert.ToBoolean(dr["t314_avantec1024"]);
					oRecursos.t314_estruct1024=Convert.ToBoolean(dr["t314_estruct1024"]);
					oRecursos.t314_fotopst1024=Convert.ToBoolean(dr["t314_fotopst1024"]);
					oRecursos.t314_plant1024=Convert.ToBoolean(dr["t314_plant1024"]);
					oRecursos.t314_const1024=Convert.ToBoolean(dr["t314_const1024"]);
					oRecursos.t314_iapfact1024=Convert.ToBoolean(dr["t314_iapfact1024"]);
					oRecursos.t314_iapdiario1024=Convert.ToBoolean(dr["t314_iapdiario1024"]);
					oRecursos.t314_cuadromando1024=Convert.ToBoolean(dr["t314_cuadromando1024"]);
					oRecursos.t314_importaciongasvi=Convert.ToByte(dr["t314_importaciongasvi"]);
					oRecursos.t314_recibirmails=Convert.ToBoolean(dr["t314_recibirmails"]);
					oRecursos.t314_defectoperiodificacion=Convert.ToBoolean(dr["t314_defectoperiodificacion"]);
					oRecursos.t314_multiventana=Convert.ToBoolean(dr["t314_multiventana"]);
					oRecursos.t422_idmoneda_VDC=Convert.ToString(dr["t422_idmoneda_VDC"]);
					oRecursos.t422_denominacionimportes_vdc=Convert.ToString(dr["t422_denominacionimportes_vdc"]);
					if(!Convert.IsDBNull(dr["t422_idmoneda_VDP"]))
						oRecursos.t422_idmoneda_VDP=Convert.ToString(dr["t422_idmoneda_VDP"]);
					if(!Convert.IsDBNull(dr["t422_denominacionimportes_vdp"]))
						oRecursos.t422_denominacionimportes_vdp=Convert.ToString(dr["t422_denominacionimportes_vdp"]);
					oRecursos.t422_denominacionimportes=Convert.ToString(dr["t422_denominacionimportes"]);
					oRecursos.t314_nuevogasvi=Convert.ToBoolean(dr["t314_nuevogasvi"]);
					oRecursos.t314_calculoonline=Convert.ToBoolean(dr["t314_calculoonline"]);
					oRecursos.t314_cargaestructura=Convert.ToBoolean(dr["t314_cargaestructura"]);

				}


				return oRecursos;
				
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
                case enumDBFields.sIDRED:
                    paramName = "@sIDRED";
                    paramType = SqlDbType.VarChar;
                    paramSize = 15;
                    break;
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
