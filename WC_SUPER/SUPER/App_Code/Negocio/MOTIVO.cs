using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using SUPER.Capa_Datos;

namespace SUPER.BLL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : MOTIVO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T423_MOTIVO
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	28/02/2011 9:19:14	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class MOTIVO
    {
        #region Metodos

        public static List<ElementoLista> ObtenerMotivos()
        {
            List<ElementoLista> oLista = new List<ElementoLista>();
            SqlDataReader dr = SUPER.Capa_Datos.MOTIVO.Catalogo(null, "", null, null, 2, 0);
            while (dr.Read()){
                oLista.Add(new ElementoLista(dr["t423_idmotivo"].ToString(), dr["t423_denominacion"].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }
        #endregion
    }
}
