using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace IB.Services.Super.Negocio
{
    public class CONSULTA
    {
        #region Propiedades y Atributos

        private int _t472_idconsulta;
        public int t472_idconsulta
        {
            get { return _t472_idconsulta; }
            set { _t472_idconsulta = value; }
        }

        private int _t314_idusuario;
        public int t314_idusuario
        {
            get { return _t314_idusuario; }
            set { _t314_idusuario = value; }
        }

        private string _t472_procalm;
        public string t472_procalm
        {
            get { return _t472_procalm; }
            set { _t472_procalm = value; }
        }

        private bool _t472_estado;
        public bool t472_estado
        {
            get { return _t472_estado; }
            set { _t472_estado = value; }
        }

        private bool _t473_estado;
        public bool t473_estado
        {
            get { return _t473_estado; }
            set { _t473_estado = value; }
        }


        #endregion
        public CONSULTA()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static CONSULTA GetDatos(SqlTransaction tr, string t472_clavews, int t314_idusuario)
        {
            CONSULTA o = new CONSULTA();
            //o.t472_procalm = "Antes de DAL.CONSULTA.Select";
            //SqlDataReader dr = IB.Services.Super.DAL.CONSULTA.Select(tr, t472_clavews, t314_idusuario);
            SqlDataReader dr = IB.Services.Super.DAL.CONSULTA.Select(tr, t472_clavews);

            if (dr.Read())
            {
                o.t472_idconsulta = int.Parse(dr["t472_idconsulta"].ToString());
                //if (dr["t314_idusuario"] != DBNull.Value)
                //    o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
                //else
                //    o.t314_idusuario = -1;
                o.t314_idusuario = t314_idusuario;

                if (dr["t472_estado"] != DBNull.Value)
                    o.t472_estado = (bool)dr["t472_estado"];
                //if (dr["t473_estado"] != DBNull.Value)
                    //o.t473_estado = (bool)dr["t473_estado"];
                o.t473_estado = true;
                if (dr["t472_procalm"] != DBNull.Value)
                    o.t472_procalm = dr["t472_procalm"].ToString();
            }
            else
            {
                //o.t472_procalm = "DAL.CONSULTA.Select (ZZZ_MIKEL_SUP_CONSULTAPERSONAL_S2) no devuelve registros";
                o.t472_idconsulta = -1;
                //throw (new NullReferenceException("No se ha obtenido ningun dato de CONSULTA"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        public static string ejecutarConsultaDS(int t314_idusuario, string sProdAlm, List<PARAMETRO> aParametros)
        {
            StringBuilder sb = new StringBuilder();
            string sAux = "", sPrimer = "";
            //string sPaso = "Inicio\r\n";
            //try
            //{
                object[] aObjetos = new object[aParametros.Count + 1];
                aObjetos[0] = t314_idusuario;
                int v = 1;
                foreach (PARAMETRO oParametro in aParametros)
                {
                    //sPaso += "Parametro: " + oParametro.t474_textoparametro + " Valor:" + oParametro.valor + "\r\n";
                    switch (oParametro.t474_tipoparametro)
                    {
                        case "A": aObjetos[v] = int.Parse(oParametro.valor); break;
                        case "M": aObjetos[v] = double.Parse(oParametro.valor.Replace(".", ",")); break;
                        case "B": aObjetos[v] = (oParametro.valor == "1") ? true : false; break;
                        default: aObjetos[v] = oParametro.valor; break;
                    }

                    v++;
                }
                //sPaso += "Antes de EjecutarConsultaDS\r\n";
                DataSet ds = IB.Services.Super.DAL.CONSULTA.EjecutarConsultaDS(sProdAlm, aObjetos);

                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    sb.Append(@"<cons>");
                    bool bTitulos = false;

                    for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                    {
                        if (!bTitulos)
                        {
                            sb.Append("<cabecera>");
                            for (int x = 0; x < ds.Tables[i].Columns.Count; x++)
                            {
                                //sb.Append("<cab" + x.ToString() + ">" + ds.Tables[i].Columns[x].ColumnName + "</cab" + x.ToString() + ">");
                                sb.Append("<col>" + IB.Services.Super.Globales.Utilidades.textoXml(ds.Tables[i].Columns[x].ColumnName) + "</col>");
                            }
                            sb.Append("</cabecera>");
                            bTitulos = true;
                        }
                        sb.Append("<linea>");
                        for (int x = 0; x < ds.Tables[i].Columns.Count; x++)
                        {
                            sAux = ds.Tables[i].Rows[j][x].ToString();

                            if (ds.Tables[i].Columns[x].DataType.Name == "text" && sAux.Trim() != "")
                            {//Para el contenido de campos de tipo Text hacemos transformaciones para que no falle la exportación a Excel
                                //sAux = sAux.Replace("<", " < ");
                                //sAux = sAux.Replace(">", " > ");
                                sAux = sAux.Trim();
                                sPrimer = sAux.Substring(0, 1);
                                switch (sPrimer)
                                {
                                    case "-":
                                    case "+":
                                    case "=":
                                        sAux = "(" + sPrimer + ")" + sAux.Substring(1);
                                        break;
                                }
                                sAux = IB.Services.Super.Globales.Utilidades.textoXml(sAux);
                            }
                            //sb.Append("<c" + x.ToString() + ">" + sAux + "</c" + x.ToString() + ">");
                            sb.Append("<col>" + sAux + "</col>");
                        }
                        sb.Append("</linea>");
                    }
                    sb.Append("</cons>");
                }
            //}
            //catch (Exception e)
            //{
            //    throw new Exception(sPaso + e.Message);
            //}
            return sb.ToString();
        }
    }
}
