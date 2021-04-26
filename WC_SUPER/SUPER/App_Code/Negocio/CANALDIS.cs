using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	public partial class CANALDIS
    {
        #region Atributos

        private string _codigo;
        public string codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        private string _denominacion;
        public string denominacion
        {
            get { return _denominacion; }
            set { _denominacion = value; }
        }
        #endregion
        #region Metodos
        public CANALDIS(string sCodigo, string sDenominacion)
        {
            this.codigo = sCodigo;
            this.denominacion = sDenominacion;
        }

        public static List<CANALDIS> ListaGlobal()
        {
            List<CANALDIS> oLista = new List<CANALDIS>();
            SqlDataReader dr = CANALDIS.Catalogo();
            while (dr.Read())
            {
                oLista.Add(new CANALDIS(dr["codigo"].ToString(), dr["denominacion"].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }
		
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();

            return SqlHelper.ExecuteSqlDataReader("SUP_CANALDIS", aParam);
        }

		#endregion
	}
}
