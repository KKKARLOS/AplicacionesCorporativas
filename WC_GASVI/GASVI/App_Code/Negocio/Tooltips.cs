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
	public partial class Tooltips
    {
        #region Propiedades y Atributos

        private string _t671_dietacompleta;
        public string t671_dietacompleta
        {
            get { return _t671_dietacompleta; }
            set { _t671_dietacompleta = value; }
        }

        private string _t671_mediadieta;
        public string t671_mediadieta
        {
            get { return _t671_mediadieta; }
            set { _t671_mediadieta = value; }
        }

        private string _t671_dietaespecial;
        public string t671_dietaespecial
        {
            get { return _t671_dietaespecial; }
            set { _t671_dietaespecial = value; }
        }

        private string _t671_dietaalojamiento;
        public string t671_dietaalojamiento
        {
            get { return _t671_dietaalojamiento; }
            set { _t671_dietaalojamiento = value; }
        }
        private string _t671_peajes;
        public string t671_peajes
        {
            get { return _t671_peajes; }
            set { _t671_peajes = value; }
        }

        private string _t671_comidas;
        public string t671_comidas
        {
            get { return _t671_comidas; }
            set { _t671_comidas = value; }
        }

        private string _t671_transporte;
        public string t671_transporte
        {
            get { return _t671_transporte; }
            set { _t671_transporte = value; }
        }

        private string _t671_disposiciones;
        public string t671_disposiciones
        {
            get { return _t671_disposiciones; }
            set { _t671_disposiciones = value; }
        }

        private string _t671_kmsestandar;
        public string t671_kmsestandar
        {
            get { return _t671_kmsestandar; }
            set { _t671_kmsestandar = value; }
        }        

        #endregion

        public Tooltips()
		{
			
		}

        public static string ObtenerTexto(string sOrigen)
        {
            string texto = "";
            SqlDataReader dr = DAL.Tooltips.ObtenerTexto();
            if (dr.Read())
            {
                switch (sOrigen)
                {
                    case "0":
                        texto = dr["t671_dietacompleta"].ToString(); //"Dieta completa";
                        break;
                    case "1":
                        texto = dr["t671_mediadieta"].ToString(); //"Media dieta";
                        break;
                    case "2":
                        texto = dr["t671_dietaespecial"].ToString(); //"Dieta especial";
                        break;
                    case "3":
                        texto = dr["t671_dietaalojamiento"].ToString(); //"Dieta de alojamiento";
                        break;
                    case "4":
                        texto = dr["t671_peajes"].ToString(); //"Peaje y aparcamiento";
                        break;
                    case "5":
                        texto = dr["t671_comidas"].ToString(); //"Comidas e invitaciones";
                        break;
                    case "6":
                        texto = dr["t671_transporte"].ToString(); //"Trasporte";
                        break;
                    case "7":
                        texto = dr["t671_disposiciones"].ToString(); //"Dispositivos generales";
                        break;
                    case "8":
                        texto = dr["t671_kmsestandar"].ToString(); //"Distancias estándar";
                        break;
                }
            }

            return texto;
        }

        public static Tooltips ObtenerToolTipsAll()
        {
            Tooltips o = new Tooltips();
            SqlDataReader dr = DAL.Tooltips.ObtenerTexto();
            if (dr.Read())
            {
                o.t671_dietacompleta = dr["t671_dietacompleta"].ToString();  //"Dieta completa";
                o.t671_mediadieta = dr["t671_mediadieta"].ToString(); //"Media dieta";
                o.t671_dietaespecial = dr["t671_dietaespecial"].ToString(); //"Dieta especial";
                o.t671_dietaalojamiento = dr["t671_dietaalojamiento"].ToString(); //"Dieta de alojamiento";
                o.t671_peajes = dr["t671_peajes"].ToString(); //"Peaje y aparcamiento";
                o.t671_comidas = dr["t671_comidas"].ToString(); //"Comidas e invitaciones";
                o.t671_transporte = dr["t671_transporte"].ToString(); //"Trasporte";
                o.t671_disposiciones = dr["t671_disposiciones"].ToString(); //"Dispositivos generales";
                o.t671_kmsestandar = dr["t671_kmsestandar"].ToString(); //"Distancias estándar";
            }
            return o;
        }

        public static void Grabar(string sDatos, string sOrigen)
        {
            if (sDatos != "")
            {
                //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                DAL.Tooltips.UpdateTooltips(null, Utilidades.unescape(sDatos), short.Parse(sOrigen));
            }
        }
	}
}