using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GESTAR.Capa_Datos;

namespace GESTAR.Capa_Negocio
{
	/// <summary>
	/// Descripci�n breve de CR.
	/// </summary>
	public class CR
	{
		public CR()
		{
			//
			// TODO: agregar aqu� la l�gica del constructor
			//
		}
        public static SqlDataReader Catalogo()
        {
            SqlDataReader drCR = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                "GESTAR_CR");
            return drCR;
        }
        public static SqlDataReader Catalogo(int iArea)
        {
            SqlDataReader drCR = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                "GESTAR_CR_TEXT", iArea);
            return drCR;
        }
	}
}
