using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Collections;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FORMULA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T454_FORMULA
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	03/02/2010 13:54:10	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FORMULA
	{
		#region Metodos

        public static SqlDataReader CatalogoGeneral()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            return SqlHelper.ExecuteSqlDataReader("SUP_FORMULA_CAT", aParam);
        }

        public static SqlDataReader ObtenerDetalleFormula(int t454_idformula)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t454_idformula", SqlDbType.Int, 4);
            aParam[0].Value = t454_idformula;

            return SqlHelper.ExecuteSqlDataReader("SUP_FORMULA_S_ARBOL", aParam);
        }
        public static SqlDataReader CatalogoLiterales()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            return SqlHelper.ExecuteSqlDataReader("SUP_FORMULA_LITERALES", aParam);
        }

        public static string GetLiteral(int t454_idformula)
        {
            string sLiteral = "";
            if (HttpContext.Current.Cache.Get("LiteralFormula") == null)
            {
                Hashtable htLiterales = new Hashtable();
                Hashtable htAcronimos = new Hashtable();
                SqlDataReader dr = FORMULA.CatalogoLiterales();
                while (dr.Read())
                {
                    htLiterales.Add((int)dr["t454_idformula"], dr["t454_literal"].ToString());
                    //if ((int)dr["t454_idformula"] == t454_idformula) sLiteral = dr["t454_literal"].ToString();
                    htAcronimos.Add((int)dr["t454_idformula"], dr["t454_acronimo"].ToString());
                }
                dr.Close();
                dr.Dispose();
                HttpContext.Current.Cache.Insert("LiteralFormula", htLiterales, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
                HttpContext.Current.Cache.Insert("AcronimoFormula", htAcronimos, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
                sLiteral = (string)htLiterales[t454_idformula];
            }
            else
            {
                Hashtable htLiterales = (Hashtable)HttpContext.Current.Cache.Get("LiteralFormula");
                sLiteral = (string)htLiterales[t454_idformula];
            }
            return sLiteral;
        }
        public static string GetAcronimo(int t454_idformula)
        {
            string sAcronimo = "";
            if (HttpContext.Current.Cache.Get("AcronimoFormula") == null)
            {
                Hashtable htAcronimos = new Hashtable();
                Hashtable htLiterales = new Hashtable();
                SqlDataReader dr = FORMULA.CatalogoLiterales();
                while (dr.Read())
                {
                    htAcronimos.Add((int)dr["t454_idformula"], dr["t454_acronimo"].ToString());
                    //if ((int)dr["t454_idformula"] == t454_idformula) sAcronimo = dr["t454_acronimo"].ToString();
                    htLiterales.Add((int)dr["t454_idformula"], dr["t454_literal"].ToString());
                }
                dr.Close();
                dr.Dispose();
                HttpContext.Current.Cache.Insert("AcronimoFormula", htAcronimos, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
                HttpContext.Current.Cache.Insert("LiteralFormula", htLiterales, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
                sAcronimo = (string)htAcronimos[t454_idformula];
            }
            else
            {
                Hashtable htAcronimos = (Hashtable)HttpContext.Current.Cache.Get("AcronimoFormula");
                sAcronimo = (string)htAcronimos[t454_idformula];
            }
            return sAcronimo;
        }

        public static string GetLiteralVF(string sFormula)
        {
            string sLiteral = "";
            switch (sFormula)
            {
                case "novencido": sLiteral = "Saldo de clientes no vencido"; break;
                case "saldovencido": sLiteral = "Saldo de clientes vencido"; break;
                case "menor60": sLiteral = "Saldo de clientes vencido menor o igual a 60 días"; break;
                case "menor90": sLiteral = "Saldo de clientes vencido menor o igual a 90 días"; break;
                case "menor120": sLiteral = "Saldo de clientes vencido menor o igual a 120 días"; break;
                case "mayor120": sLiteral = "Saldo de clientes vencido mayor de 120 días"; break;
            }
            return sLiteral;
        }
        public static string GetAcronimoVF(string sFormula)
        {
            string sAcronimo = "";
            switch (sFormula)
            {
                case "novencido": sAcronimo = "No Vencido"; break;
                case "saldovencido": sAcronimo = "Vencido"; break;
                case "menor60": sAcronimo = "Venc. <= 60"; break;
                case "menor90": sAcronimo = "Venc. <= 90"; break;
                case "menor120": sAcronimo = "Venc.<=120"; break;
                case "mayor120": sAcronimo = "Venc. > 120"; break;
            }
            return sAcronimo;
        }

        public static string GetLiteralAF(string sFormula)
        {
            string sLiteral = "";
            switch (sFormula)
            {
                case "saldo_OCyFA": sLiteral = "Obra en curso y facturación anticipada"; break;
                case "saldo_oc": sLiteral = "Obra en curso"; break;
                case "saldo_fa": sLiteral = "Facturación anticipada"; break;
                //case "factur": sLiteral = "Importe facturado"; break;
                case "saldo_cli": sLiteral = "Saldo de clientes"; break;
                //case "cobro": sLiteral = "Importe cobrado"; break;
                case "saldo_financ": sLiteral = "Saldo financiado"; break;
                case "saldo_cli_SF": sLiteral = "Saldo de clientes"; break;
                case "saldo_oc_SF": sLiteral = "Obra en curso"; break;
                case "saldo_fa_SF": sLiteral = "Facturación anticipada"; break;
                case "PMC": sLiteral = "Plazo medio de cobro"; break;
                case "saldo_cli_PMC": sLiteral = "Saldo de clientes"; break;
                case "saldo_oc_PMC": sLiteral = "Obra en curso"; break;
                case "saldo_fa_PMC": sLiteral = "Facturación anticipada"; break;
                case "saldo_financ_PMC": sLiteral = "Saldo financiado"; break;
                case "prodult12m_PMC": sLiteral = "Producción últimos doce meses"; break;
                case "costemensual": sLiteral = "Exceso de consumos de recursos financieros del mes"; break;
                case "saldo_cli_CF": sLiteral = "Saldo de clientes"; break;
                case "saldo_oc_CF": sLiteral = "Obra en curso"; break;
                case "saldo_fa_CF": sLiteral = "Facturación anticipada"; break;
                case "prodult12m_CF": sLiteral = "Producción últimos doce meses"; break;
                case "saldo_financ_CF": sLiteral = "Saldo financiado"; break;
                case "SFT": sLiteral = "Importe"; break;
                case "difercoste": sLiteral = "Diferencial"; break;
                //case "costeanual": sLiteral = "Extrapolación año"; break;
                case "costemensualacum": sLiteral = "Exceso de consumos de recursos financieros acumulados"; break;
            }
            return sLiteral;
        }
        public static string GetAcronimoAF(string sFormula)
        {
            string sAcronimo = "";
            switch (sFormula)
            {
                case "saldo_OCyFA": sAcronimo = "S. OCyFA"; break;
                case "saldo_oc": sAcronimo = "O. Curso"; break;
                case "saldo_fa": sAcronimo = "Fac. Ant."; break;
                //case "factur": sAcronimo = "I. Factur."; break;
                case "saldo_cli": sAcronimo = "S. Clientes"; break;
                //case "cobro": sAcronimo = "I. Cobrado"; break;
                case "saldo_financ": sAcronimo = "S. Finan."; break;
                case "saldo_cli_SF": sAcronimo = "S. Clientes"; break;
                case "saldo_oc_SF": sAcronimo = "O. Curso"; break;
                case "saldo_fa_SF": sAcronimo = "Fac. Ant."; break;
                case "PMC": sAcronimo = "PMC"; break;
                case "saldo_cli_PMC": sAcronimo = "S. Clientes"; break;
                case "saldo_oc_PMC": sAcronimo = "O. Curso"; break;
                case "saldo_fa_PMC": sAcronimo = "Fac. Ant."; break;
                case "saldo_financ_PMC": sAcronimo = "S. Finan."; break;
                case "prodult12m_PMC": sAcronimo = "Prod.12mes"; break;
                case "costemensual": sAcronimo = "C Fin. Mes"; break;
                case "saldo_cli_CF": sAcronimo = "S. Clientes"; break;
                case "saldo_oc_CF": sAcronimo = "O. Curso"; break;
                case "saldo_fa_CF": sAcronimo = "Fac. Ant."; break;
                case "prodult12m_CF": sAcronimo = "Prod.12mes"; break;
                case "saldo_financ_CF": sAcronimo = "S. Finan."; break;
                case "SFT": sAcronimo = "S.Finan Teo."; break;
                case "difercoste": sAcronimo = "Dif S. Finan"; break;
                //case "costeanual": sAcronimo = "C Finan Acu"; break;
                case "costemensualacum": sAcronimo = "C Fin. Acum."; break;

            }
            return sAcronimo;
        }

        #endregion
	}
}
