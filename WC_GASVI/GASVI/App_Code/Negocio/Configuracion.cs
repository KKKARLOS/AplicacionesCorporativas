using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security; //para gestion de roles
using GASVI.DAL;
using Microsoft.JScript;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace GASVI.BLL
{
	public partial class Configuracion
    {
        #region Propiedades
        
        #endregion

        public Configuracion()
		{
			
		}

        public static void setMoneda(int idFicepi, string sValor)
        {
            DAL.Configuracion.UpdateMoneda(null,
                                                idFicepi,
                                                sValor);
        }

        public static void setAviso(int idFicepi, bool bAviso)
        {
            DAL.Configuracion.UpdateAviso(null, idFicepi, bAviso);
        }
        public static void setMotivo(int idFicepi, string sValor)
        {
            DAL.Configuracion.UpdateMotivo(null,
                                                idFicepi,
                                                (sValor=="")? null:(byte?)byte.Parse(sValor));
        }
        public static void setEmpresa(int idFicepi, int idEmpresa)
        {
            DAL.Configuracion.UpdateEmpresa(null, idFicepi, idEmpresa);
        }
    }
}