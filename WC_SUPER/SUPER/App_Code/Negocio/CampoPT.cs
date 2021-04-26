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
    /// Descripción breve de CampoPT
    /// </summary>
    public class CampoPT
    {
        public CampoPT()
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
        //public static DataSet GenerarExcelDataSet(int t314_idusuario, ArrayList slCampos)
        //{
        //    return SUPER.DAL.CampoTarea.GenerarExcelDataSet(null, t314_idusuario, slCampos);
        //}
    }
}