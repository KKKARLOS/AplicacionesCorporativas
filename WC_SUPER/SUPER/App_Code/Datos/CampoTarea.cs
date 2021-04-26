using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
//Para el ArrayList
using System.Collections;
//Para el Dictionary
using System.Collections.Generic;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de CampoTarea
    /// </summary>
    public class CampoTarea
    {
        public CampoTarea()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        //public static SqlDataReader GetAmbito(SqlTransaction tr, int t314_idusuario, int t001_idficepi, 
        //                                      bool privado, bool equipo, bool proyecto, bool cr, bool cliente, bool empresarial,
        //                                      ArrayList lProy, ArrayList lCli, ArrayList lCR)
        //{
        //    SqlParameter[] aParam = new SqlParameter[]{  
        //        ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
        //        ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
        //        ParametroSql.add("@AmbPrivado", SqlDbType.Bit, 1, privado),
        //        ParametroSql.add("@AmbEquipo", SqlDbType.Bit, 1, equipo),
        //        ParametroSql.add("@AmbProy", SqlDbType.Bit, 1, proyecto),
        //        ParametroSql.add("@AmbCR", SqlDbType.Bit, 1, cr),
        //        ParametroSql.add("@AmbCliente", SqlDbType.Bit, 1, cliente),
        //        ParametroSql.add("@AmbPublico", SqlDbType.Bit, 1, empresarial),
        //        ParametroSql.add("@TABLAPROY", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListCod(lProy)),
        //        ParametroSql.add("@TABLACR", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListCod(lCR)),
        //        ParametroSql.add("@TABLACLIENTE", SqlDbType.Structured, SqlHelper.GetDataTableFromArrayListCod(lCli))
        //    };
        //    // Ejecuta la query y devuelve el numero de registros modificados.
        //    if (tr == null)
        //        return SqlHelper.ExecuteSqlDataReader("SUP_CAMPOTAREA_CAT", aParam);
        //    else
        //        return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CAMPOTAREA_CAT", aParam);
        //}
        //public static DataSet GenerarExcelDataSet(SqlTransaction tr, int t314_idusuario, ArrayList slCampos)
        //{
        //    SqlParameter[] aParam = new SqlParameter[]{
        //        ParametroSql.add("@nUsuario", SqlDbType.Int, 4, t314_idusuario),
        //        ParametroSql.add("@TABLACAMPOS", SqlDbType.Structured, GetDataFromArrayList(slCampos)),
        //    };

        //    if (tr == null)
        //        return SqlHelper.ExecuteDataset("SUP_GETTAREAS_CAMPOS", aParam);
        //    else
        //        return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_GETTAREAS_CAMPOS", aParam);
        //}

        //private static DataTable GetDataFromArrayList(ArrayList aLista)
        //{
        //    /*
        //     * Cada elemento de aLista viene en el siguiente formato
        //     * [0] -> id campo
        //     * [1] -> exportar el campo
        //     * [2] -> tipo de campo
        //     * [3] -> Si tipoCampo="T" (texto) caracter que indica Inicia o Contiene
        //     *          sino valor desde
        //     * [4] -> Si tipoCampo="T" (texto) cadena de búsqueda
        //     *          sino valor hasta
        //     * */
        //    decimal dImp = 0;
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add(new DataColumn("t290_idcampo", typeof(int)));
        //    //dt.Columns.Add(new DataColumn("int_desde", typeof(int)));
        //    //dt.Columns.Add(new DataColumn("int_hasta", typeof(int)));
        //    dt.Columns.Add(new DataColumn("cant_desde", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("cant_hasta", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("fec_desde", typeof(DateTime)));
        //    dt.Columns.Add(new DataColumn("fec_hasta", typeof(DateTime)));
        //    dt.Columns.Add(new DataColumn("texto", typeof(string)));
        //    dt.Columns.Add(new DataColumn("modobusca", typeof(string)));
        //    dt.Columns.Add(new DataColumn("visible", typeof(bool)));
        //    if (aLista.Count > 0)
        //    {
        //        #region Prueba
        //        //foreach (Dictionary<string, object> oElem in aLista)
        //        //{
        //        //    if (oElem["id"].ToString() != "")
        //        //    {
        //        //        DataRow row = dt.NewRow();
        //        //        row["t290_idcampo"] = oElem["id"];
        //        //        switch(oElem["t"].ToString())
        //        //        {
        //        //            case "I":
        //        //                row["int_desde"] = oElem["v1"];
        //        //                row["int_hasta"] = oElem["v2"];
        //        //                break;
        //        //            case "F":
        //        //            case "H":
        //        //                row["fec_desde"] = oElem["v1"];
        //        //                row["fec_hasta"] = oElem["v2"];
        //        //                break;
        //        //            case "T":
        //        //                row["texto"] = oElem["v1"];
        //        //                break;
        //        //        }
        //        //        row["visible"] = true;

        //        //        dt.Rows.Add(row);
        //        //    }
        //        //}
        //        #endregion
        //        foreach (string[] oElem in aLista)
        //        {
        //            DataRow row = dt.NewRow();
        //            row["t290_idcampo"] = oElem[0];
        //            switch (oElem[1].ToString())
        //            {
        //                case "I":
        //                    if (oElem[3] != "")
        //                    {
        //                        dImp = decimal.Parse(oElem[3]);
        //                        row["cant_desde"] = dImp;
        //                    }
        //                    if (oElem[4] != "")
        //                    {
        //                        dImp = decimal.Parse(oElem[4]);
        //                        row["cant_hasta"] = dImp;
        //                    }
        //                    break;
        //                case "F":
        //                case "H":
        //                    if (oElem[3] != "") row["fec_desde"] = oElem[3];
        //                    if (oElem[4] != "") row["fec_hasta"] = oElem[4];
        //                    break;
        //                case "T":
        //                    row["texto"] = oElem[4];
        //                    row["modobusca"] = oElem[3];
        //                    break;
        //            }
        //            if (oElem[2]=="1")
        //                row["visible"] = true;
        //            else
        //                row["visible"] = false;

        //            dt.Rows.Add(row);
        //        }
                
        //    }
        //    return dt;
        //}
    }
}