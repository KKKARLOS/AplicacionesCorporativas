using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace IB.Services.Super.Negocio
{
    public class PARAMETRO
    {
        #region Propiedades y Atributos

        private int _t472_idconsulta;
        public int t472_idconsulta
        {
            get { return _t472_idconsulta; }
            set { _t472_idconsulta = value; }
        }

        private string _t474_textoparametro;
        public string t474_textoparametro
        {
            get { return _t474_textoparametro; }
            set { _t474_textoparametro = value; }
        }

        private string _t474_nombreparametro;
        public string t474_nombreparametro
        {
            get { return _t474_nombreparametro; }
            set { _t474_nombreparametro = value; }
        }

        private string _t474_tipoparametro;
        public string t474_tipoparametro
        {
            get { return _t474_tipoparametro; }
            set { _t474_tipoparametro = value; }
        }

        private bool _t474_opcional;
        public bool t474_opcional
        {
            get { return _t474_opcional; }
            set { _t474_opcional = value; }
        }

        private string _t474_visible;
        public string t474_visible
        {
            get { return _t474_visible; }
            set { _t474_visible = value; }
        }

        private string _t474_valordefecto;
        public string t474_valordefecto
        {
            get { return _t474_valordefecto; }
            set { _t474_valordefecto = value; }
        }

        private string _valor;
        public string valor
        {
            get { return _valor; }
            set { _valor = value; }
        }


        #endregion
        public PARAMETRO()
        {
        }
        public PARAMETRO(string t474_textoparametro, string valor)
        {
            this.t474_textoparametro = t474_textoparametro;
            this.valor = valor;
        }
        /// <summary>
        /// Obtiene la lista de parametros del procedimiento almacenado y asigna el valor por defecto para cada uno de ellos
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t472_idconsulta"></param>
        /// <returns></returns>
        public static List<PARAMETRO> GetParametros(SqlTransaction tr, int t472_idconsulta)
        {
            List<PARAMETRO> oLista = new List<PARAMETRO>();
            PARAMETRO oElem;
            SqlDataReader dr = IB.Services.Super.DAL.PARAMETRO.GetParametros(tr, t472_idconsulta);
            while (dr.Read())
            {
                oElem = new PARAMETRO();
                oElem.t474_textoparametro = dr["t474_textoparametro"].ToString();
                oElem.t474_nombreparametro = dr["t474_nombreparametro"].ToString();
                oElem.t474_tipoparametro = dr["t474_tipoparametro"].ToString();
                oElem.t474_opcional = (bool)dr["t474_opcional"];
                oElem.t474_visible = dr["t474_visible"].ToString();
                oElem.t474_valordefecto = dr["t474_valordefecto"].ToString();
                oElem.valor = oElem.t474_valordefecto;
                oLista.Add(oElem);
            }
            dr.Close();
            dr.Dispose();

            return oLista;
        }
        public static PARAMETRO BuscarParametro(List<PARAMETRO> oListaParam, string sParametro)
        {
            return oListaParam.Find(delegate(PARAMETRO item) { return (item.t474_textoparametro == sParametro); });
        }
    }
}
