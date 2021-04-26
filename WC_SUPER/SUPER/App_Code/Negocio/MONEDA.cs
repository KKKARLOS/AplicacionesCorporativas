using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Collections.Generic;
using SUPER.Capa_Datos;
using SUPER.BLL;
//using System.Xml;
//using System.Net;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : MONEDA
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la vista: Z_MONEDAS
    /// </summary>
    /// <history>
    /// 	Creado por [dotofean]	22/11/2006 9:37:14	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class MONEDA
    {

        #region Propiedades y Atributos

        protected string _t422_idmoneda;
        public string t422_idmoneda
        {
            get { return _t422_idmoneda; }
            set { _t422_idmoneda = value; }
        }
        protected string _t422_denominacion;
        public string t422_denominacion
        {
            get { return _t422_denominacion; }
            set { _t422_denominacion = value; }
        }
        protected string _t422_denominacionimportes;
        public string t422_denominacionimportes
        {
            get { return _t422_denominacionimportes; }
            set { _t422_denominacionimportes = value; }
        }

        protected decimal _t422_tipocambio;
        public decimal t422_tipocambio
        {
            get { return _t422_tipocambio; }
            set { _t422_tipocambio = value; }
        }
        
		

        #endregion

        #region Constructores

        public MONEDA()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados seg√∫n el tipo de dato.
        }
        public MONEDA(string t422_idmoneda, string t422_denominacion, string t422_denominacionimportes, decimal t422_tipocambio)
        {
            this.t422_idmoneda = t422_idmoneda;
            this.t422_denominacion = t422_denominacion;
            this.t422_denominacionimportes = t422_denominacionimportes;
            this.t422_tipocambio = t422_tipocambio;
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un cat·logo de registros de la vista Z_MONEDAS
        /// </summary>
        /// <history>
        /// 	Creado por [dopeotca]	22/11/2006 9:37:14
        /// </history>
        /// -----------------------------------------------------------------------------

        public static SqlDataReader CatalogoSAP()
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@entorno", SqlDbType.Char, 1, Utilidades.Entorno);

            return SqlHelper.ExecuteSqlDataReader("SUP_MONEDA_C", aParam);
        }
        public static SqlDataReader CatalogoSUPER()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            return SqlHelper.ExecuteSqlDataReader("SUP_MONEDASUPER_C", aParam);
        }
        public static SqlDataReader CatalogoMan(Nullable<bool> t422_estado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t422_estadovisibilidad", SqlDbType.Bit, 1, t422_estado);

            return SqlHelper.ExecuteSqlDataReader("SUP_MONEDASUPER_CAT", aParam);
        }
        
        public static SqlDataReader ObtenerMonedasVDP()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_MONEDA_VDP", aParam);
        }
        public static SqlDataReader ObtenerMonedasVDC()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_MONEDA_VDC", aParam);
        }
        public static SqlDataReader ObtenerMonedasVCM()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_MONEDA_VCM", aParam);
        }
        public static SqlDataReader ObtenerMonedasGestionarProyectos()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_MONEDA_GESTIONPROY", aParam);
        }
        public static SqlDataReader CatalogoMonedasCosteUsu()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            return SqlHelper.ExecuteSqlDataReader("SUP_MONEDA_COSTEUSU_C", aParam);
        }

        public static List<ElementoLista> ListaMonedasCosteUsu()
        {
            List<ElementoLista> oLista = new List<ElementoLista>();
            SqlDataReader dr = CatalogoMonedasCosteUsu();
            while (dr.Read())
            {
                oLista.Add(new ElementoLista(dr["t422_idmoneda"].ToString(), dr["t422_denominacion"].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }

        public static List<MONEDA> ListaActivas()
        {
            if (HttpContext.Current.Cache["Lista_Monedas"] == null)
            {
                List<MONEDA> oLista = new List<MONEDA>();
                SqlDataReader dr = MONEDA.CatalogoSUPER();
                MONEDA oElem;
                while (dr.Read())
                {
                    oElem = new MONEDA(dr["t422_idmoneda"].ToString(),dr["t422_denominacion"].ToString(),dr["t422_denominacionimportes"].ToString(),(decimal)dr["t422_tipocambio"]);
                    oLista.Add(oElem);
                }
                dr.Close();
                dr.Dispose();

                HttpContext.Current.Cache.Insert("Lista_Monedas", oLista, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
                return oLista;
            }
            else
            {
                return (List<MONEDA>)HttpContext.Current.Cache["Lista_Monedas"];
            }
        }
        public static string getDenominacion(string t422_idmoneda)
        {
            string sRes = "";
            List<MONEDA> listaMonedas = MONEDA.ListaActivas();
            foreach (MONEDA oMoneda in listaMonedas)
            {
                if (oMoneda.t422_idmoneda == t422_idmoneda)
                {
                    sRes = oMoneda.t422_denominacion;
                    break;
                }
            }
            return sRes;
        }
        public static string getDenominacionImportes(string t422_idmoneda)
        {
            string sRes = "";
            List<MONEDA> listaMonedas = MONEDA.ListaActivas();
            foreach (MONEDA oMoneda in listaMonedas)
            {
                if (oMoneda.t422_idmoneda == t422_idmoneda)
                {
                    sRes = oMoneda.t422_denominacionimportes;
                    break;
                }
            }
            return sRes;
        }

        public static int Update(SqlTransaction tr, string t422_idmoneda, string t422_denominacion, bool t422_estado, Nullable<decimal> t422_tipocambio, Nullable<decimal> t422_tipocambiosiguiente, Nullable<int> t422_anomessiguiente, string t422_denominacionimportes, bool t422_estadovisibilidad)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.Text, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@t422_denominacion", SqlDbType.Text, 50, t422_denominacion);
            aParam[i++] = ParametroSql.add("@t422_estado", SqlDbType.Bit, 1, t422_estado);
            aParam[i++] = ParametroSql.add("@t422_tipocambio", SqlDbType.SmallMoney, 4, t422_tipocambio);
            aParam[i++] = ParametroSql.add("@t422_tipocambiosiguiente", SqlDbType.SmallMoney, 4, t422_tipocambiosiguiente);
            aParam[i++] = ParametroSql.add("@t422_anomessiguiente", SqlDbType.Int, 4, t422_anomessiguiente);
            aParam[i++] = ParametroSql.add("@t422_denominacionimportes", SqlDbType.Text, 50, t422_denominacionimportes);
            aParam[i++] = ParametroSql.add("@t422_estadovisibilidad", SqlDbType.Bit, 1, t422_estadovisibilidad);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_MONEDA_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_MONEDA_U", aParam);
        }

        //public static DataTable ObtenerTipoCambioBCE()
        //{
        //    XmlTextReader xmlReader;

        //    DataTable dt = new DataTable("Monedas");
        //    dt.Columns.Add("Moneda", typeof(string));
        //    dt.Columns.Add("TipoCambio", typeof(decimal));

        //    try
        //    {
        //        //Read data from the XML-file over the interNET
        //        xmlReader = new XmlTextReader("http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
        //    }
        //    catch (WebException)
        //    {
        //        //throw new WebException("There has been a mistake with collect the xmlfile from the net!");
        //        throw new WebException("Error al obtener los tipos de cambio del Banco Central Europeo.");
        //    }

        //    try
        //    {
        //        while (xmlReader.Read())
        //        {
        //            if (xmlReader.Name != "")
        //            {
        //                //Check that there are node call gesmes:name
        //                if (xmlReader.Name == "gesmes:name")
        //                {
        //                    string _author = xmlReader.ReadString();
        //                }

        //                for (int i = 0; i < xmlReader.AttributeCount; i++)
        //                {
        //                    //Check that there are node call Cube
        //                    if (xmlReader.Name == "Cube")
        //                    {
        //                        ////Check that there are 1 attribut, then get the date
        //                        //if (xmlReader.AttributeCount == 1)
        //                        //{
        //                        //    xmlReader.MoveToAttribute("time");

        //                        //    DateTime tim = DateTime.Parse(xmlReader.Value);
        //                        //    newRowVa = null;
        //                        //    DataRow newRowCo = null;

        //                        //    newRowVa = dsVa.Exchance.NewRow();
        //                        //    newRowVa["Date"]= tim;
        //                        //    dsVa.Exchance.Rows.Add(newRowVa);

        //                        //    newRowCo = dsVa.Country.NewRow();
        //                        //    newRowCo["Initial"]= "EUR";
        //                        //    newRowCo["Name"]= convert.MoneyName("EUR");		// Find Country name from ISO code
        //                        //    newRowCo["Rate"]= 1.0;
        //                        //    dsVa.Country.Rows.Add(newRowCo);

        //                        //    newRowCo.SetParentRow(newRowVa);	// Make Key to subtable
        //                        //}

        //                        //If the number of attributs are 2, so get the ExchangeRate-node
        //                        if (xmlReader.AttributeCount == 2)
        //                        {
        //                            xmlReader.MoveToAttribute("currency");
        //                            string cur = xmlReader.Value;

        //                            xmlReader.MoveToAttribute("rate");
        //                            decimal rat = decimal.Parse(xmlReader.Value.Replace(".", ",")); // I am using "," as a decimal symbol

        //                            DataRow newRow = dt.NewRow();

        //                            newRow["Moneda"] = cur;
        //                            //newRowCo["Name"]= convert.MoneyName(cur);
        //                            newRow["TipoCambio"] = rat;
        //                            dt.Rows.Add(newRow);
        //                        }

        //                        xmlReader.MoveToNextAttribute();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (WebException)
        //    {
        //        throw new WebException("ConexiÛn de internet cortada.");
        //    }

        //    return dt;
        //}
        public static int ActualizarModoTipoCambioBCE(SqlTransaction tr, byte t725_modotipocambioBCE)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t725_modotipocambioBCE", SqlDbType.TinyInt, 1, t725_modotipocambioBCE);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PARAMETRIZACIONSUPER_U_MTCA", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PARAMETRIZACIONSUPER_U_MTCA", aParam);
        }

        #endregion
    }
}