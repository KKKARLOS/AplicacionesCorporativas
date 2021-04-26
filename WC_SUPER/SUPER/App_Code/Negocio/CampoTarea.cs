using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
//Para el ArrayList
using System.Collections;
//Para el StringBuilder
using System.Text;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de CampoTarea
    /// </summary>
    public class CampoTarea
    {
        #region Atributos y Propiedades complementarios
        private string _codAmbito;
        public string codAmbito
        {
            get { return _codAmbito; }
            set { _codAmbito = value; }
        }

        private string _denAmbito;
        public string denAmbito
        {
            get { return _denAmbito; }
            set { _denAmbito = value; }
        }

        //I->numerico, T->cadena, F->fecha, H->fecha y hora
        private string _tipoDato;
        public string tipoDato
        {
            get { return _tipoDato; }
            set { _tipoDato = value; }
        }

        private int _idCampo;
        public int idCampo
        {
            get { return _idCampo; }
            set { _idCampo = value; }
        }

        private string _denCampo;
        public string denCampo
        {
            get { return _denCampo; }
            set { _denCampo = value; }
        }
/*
        private int _t301_idproyecto;
        public int t301_idproyecto
        {
            get { return _t301_idproyecto; }
            set { _t301_idproyecto = value; }
        }

        private int _t302_idcliente;
        public int t302_idcliente
        {
            get { return _t302_idcliente; }
            set { _t302_idcliente = value; }
        }

        private int _t303_idnodo;
        public int t303_idnodo
        {
            get { return _t303_idnodo; }
            set { _t303_idnodo = value; }
        }
*/
        private string _denElemAmbito;
        public string denElemAmbito
        {
            get { return _denElemAmbito; }
            set { _denElemAmbito = value; }
        }

        #endregion
        public CampoTarea()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        //public static List<CampoTarea> GetAmbito(int t314_idusuario, int t001_idficepi, 
        //                                    bool privado, bool equipo, bool proyecto, bool cr, bool cliente, bool empresarial,
        //                                    ArrayList lProy, ArrayList lCli, ArrayList lCR)
        //{
        //    List<CampoTarea> MiLista = new List<CampoTarea>();
        //    SqlDataReader dr = SUPER.DAL.CampoTarea.GetAmbito(null, t314_idusuario, t001_idficepi,
        //                                                    privado, equipo, proyecto, cr, cliente, empresarial,
        //                                                    lProy, lCli, lCR);
        //    CampoTarea oCT;
        //    while (dr.Read())
        //    {
        //        oCT = new CampoTarea();
        //        oCT.idCampo = int.Parse(dr["t290_idcampo"].ToString());
        //        oCT.denCampo = dr["t290_denominacion"].ToString();
        //        oCT.codAmbito = dr["CodAmbito"].ToString();
        //        oCT.denAmbito = dr["Ambito"].ToString();
        //        oCT.denElemAmbito = dr["Nomambito"].ToString();
        //        //oCT.t301_idproyecto = int.Parse(dr["t301_idproyecto"].ToString());
        //        //oCT.t302_idcliente = int.Parse(dr["t302_idcliente"].ToString());
        //        //oCT.t303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
        //        oCT.tipoDato = dr["t291_idtipodato"].ToString();

        //        MiLista.Add(oCT);
        //    }
        //    dr.Close();
        //    dr.Dispose();

        //    return MiLista;
        //}
        //public static string GenerarExcel(ArrayList slCampos)
        //{
        //    string sRes = "";
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("<table>");
        //    SqlDataReader dr = SUPER.DAL.CampoTarea.GenerarExcel(null, slCampos);
        //    while (dr.Read())
        //    {
        //        sb.Append("<tr>");
        //        sb.Append("<td>");
        //            sb.Append(dr["t301_idproyecto"].ToString());
        //        sb.Append("</td>");

        //        sb.Append("<td>");
        //        sb.Append(dr["t301_denominacion"].ToString());
        //        sb.Append("</td>");

        //        sb.Append("<td>");
        //        sb.Append(dr["t331_despt"].ToString());
        //        sb.Append("</td>");

        //        sb.Append("<td>");
        //        sb.Append(dr["t334_desfase"].ToString());
        //        sb.Append("</td>");

        //        sb.Append("<td>");
        //        sb.Append(dr["t335_desactividad"].ToString());
        //        sb.Append("</td>");

        //        sb.Append("<td>");
        //        sb.Append(dr["t332_destarea"].ToString());
        //        sb.Append("</td>");

        //        sb.Append("<td>");
        //        sb.Append(dr["estado"].ToString());
        //        sb.Append("</td>");

        //        sb.Append("<td>");
        //        sb.Append(dr["denCampo"].ToString());
        //        sb.Append("</td>");

        //        sb.Append("<td>");
        //        sb.Append(dr["valCampo"].ToString());
        //        sb.Append("</td>");

        //        sb.Append("</tr>");
        //    }
        //    sb.Append("</table>");
        //    dr.Close();
        //    dr.Dispose();

        //    return sRes;
        //}
        //public static DataSet GenerarExcelDataSet(int t314_idusuario, ArrayList slCampos)
        //{
        //    return SUPER.DAL.CampoTarea.GenerarExcelDataSet(null, t314_idusuario, slCampos);
        //}

    }
}