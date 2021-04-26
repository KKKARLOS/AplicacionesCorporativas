using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using GASVI.DAL;
using System.Text;
using System.Text.RegularExpressions;

namespace GASVI.BLL
{
    public class Estructura
    {
        public enum sTipoElem
        {
            SUBNODO,
            NODO,
            SUPERNODO1,
            SUPERNODO2,
            SUPERNODO3,
            SUPERNODO4
        }

        #region Propiedades
        protected byte _Cod;
        protected string _DesC;
        protected string _DesL;
        protected bool _bUtilizado;

        public byte nCodigo
	    {
            get { return _Cod; }
            set { _Cod = value; }
	    }
        public string sDesCorta
        {
            get { return _DesC; }
            set { _DesC = value; }
        }
        public string sDesLarga
        {
            get { return _DesL; }
            set { _DesL = value; }
        }
        public bool bUtilizado
        {
            get { return _bUtilizado; }
            set { _bUtilizado = value; }
        }

        #endregion

        public Estructura()
	    {
		    //
		    // TODO: Add constructor logic here
		    //
        }

        #region Métodos
        public static List<Estructura> ListaGlobal()
        {
            if (HttpContext.Current.Cache["Lista_Estructura"] == null)
            {
                List<Estructura> oLista = new List<Estructura>();
                SqlParameter[] aParam = new SqlParameter[0];
                SqlDataReader dr = DAL.Estructura.GetDatosEstructura();
                Estructura oElem;
                while (dr.Read())
                {
                    oElem = new Estructura();
                    oElem.nCodigo = byte.Parse(dr["t400_idestructura"].ToString());
                    oElem.sDesCorta = dr["t400_dencorta"].ToString();
                    oElem.sDesLarga = dr["t400_denlarga"].ToString();
                    oElem.bUtilizado = (bool)dr["t400_utilizado"];
                    oLista.Add(oElem);
                }
                dr.Close();
                dr.Dispose();

                HttpContext.Current.Cache.Insert("Lista_Estructura", oLista, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
                return oLista;
            }
            else
            {
                return (List<Estructura>)HttpContext.Current.Cache["Lista_Estructura"];
            }
        }
        public static List<Estructura> ListaGlobalActiva()
        {
            if (HttpContext.Current.Cache["Lista_Estructura"] == null)
            {
                List<Estructura> oLista = new List<Estructura>();
                SqlParameter[] aParam = new SqlParameter[0];
                SqlDataReader dr = DAL.Estructura.ListaActiva();
                Estructura oElem;
                while (dr.Read())
                {
                    oElem = new Estructura();
                    oElem.nCodigo = byte.Parse(dr["t400_idestructura"].ToString());
                    oElem.sDesCorta = dr["t400_dencorta"].ToString();
                    oElem.sDesLarga = dr["t400_denlarga"].ToString();
                    oElem.bUtilizado = (bool)dr["t400_utilizado"];
                    oLista.Add(oElem);
                }
                dr.Close();
                dr.Dispose();

                HttpContext.Current.Cache.Insert("Lista_Estructura", oLista, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
                return oLista;
            }
            else
            {
                return (List<Estructura>)HttpContext.Current.Cache["Lista_Estructura"];
            }
        }

        public static string getDefCorta(sTipoElem sTE)
        {
            string sRes = "";
            int iInd = 0;
            List<Estructura> listaEstructura = Estructura.ListaGlobal();
            switch (sTE)
            {
                case sTipoElem.SUBNODO:
                    iInd = 1;
                    break;
                case sTipoElem.NODO:
                    iInd = 2;
                    break;
                case sTipoElem.SUPERNODO1:
                    iInd = 3;
                    break;
                case sTipoElem.SUPERNODO2:
                    iInd = 4;
                    break;
                case sTipoElem.SUPERNODO3:
                    iInd = 5;
                    break;
                case sTipoElem.SUPERNODO4:
                    iInd = 6;
                    break;
            }
            foreach (Estructura oEL in listaEstructura)
            {
                if (oEL.nCodigo == iInd)
                {
                    sRes = oEL.sDesCorta;
                    break;
                }
            }
            return sRes.Replace((char)34, (char)39);
        }
        public static string getDefLarga(sTipoElem sTE)
        {
            string sRes = "";
            int iInd = 0;
            List<Estructura> listaEstructura = Estructura.ListaGlobal();
            switch (sTE)
            {
                case sTipoElem.SUBNODO:
                    iInd = 1;
                    break;
                case sTipoElem.NODO:
                    iInd = 2;
                    break;
                case sTipoElem.SUPERNODO1:
                    iInd = 3;
                    break;
                case sTipoElem.SUPERNODO2:
                    iInd = 4;
                    break;
                case sTipoElem.SUPERNODO3:
                    iInd = 5;
                    break;
                case sTipoElem.SUPERNODO4:
                    iInd = 6;
                    break;
            }
            foreach (Estructura oEL in listaEstructura)
            {
                if (oEL.nCodigo == iInd)
                {
                    sRes = oEL.sDesLarga;
                    break;
                }
            }
            return sRes.Replace((char)34, (char)39);
        }

        public static DataSet getEstructura(int bInactivos)
        {
            //Define a DataSet
            DataSet ds = new DataSet();
            string id = "";
            string idParent = "";
            int fila = 0;
            //Create the DataTable object
            DataTable table = ds.Tables.Add("Folders");
            DataColumn folderID =
                table.Columns.Add("FolderID", typeof(string));
            DataColumn parentFolderID =
                table.Columns.Add("ParentFolderID", typeof(string));
            DataColumn sortOrder =
                table.Columns.Add("SortOrder", typeof(float));
            DataColumn folderName =
                table.Columns.Add("FolderName", typeof(string));
            SqlDataReader dr = DAL.Estructura.getEstructura(bInactivos);

            while (dr.Read())
            {
                if (fila < 11)
                {
                    id = dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "-" + dr["NODO"].ToString() + "-" + dr["SUBNODO"].ToString();
                    // pendiente mejorar si va bien
                    if (dr["SN3"].ToString() == "0")
                        idParent = "";
                    else if (dr["SN2"].ToString() == "0")
                        idParent = dr["SN4"].ToString() + "-0-0-0-0-0";
                    else if (dr["SN1"].ToString() == "0")
                        idParent = dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-0-0-0-0";
                    else if (dr["NODO"].ToString() == "0")
                        idParent = dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-0-0-0";
                    else if (dr["SUBNODO"].ToString() == "0")
                        idParent = dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "-0-0";
                    else
                        idParent = dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "-" + dr["NODO"].ToString() + "-0";
                    if (id != "" && idParent != "" && dr["DENOMINACION"].ToString().Replace("'", "&#39;") != "")
                        table.Rows.Add(new object[] { id, (idParent == "")? null: idParent, 1, dr["DENOMINACION"].ToString().Replace("'", "&#39;") });
                    fila++;
                }
            }
                //The following code adds a calculated column and set the
                //sort order. If you populate the DataSet from your back
                //end database, you should do this in your SQL statement, 
                //which should be (presumably) more efficient

                //Add a calculated column that concatenates "FolderID"
                //and "SortOrder" into a single value. For example, 
                //FolderID 1 and SortOrder 3 results in a combined string
                //value "1,3". This value is being populated into
                //each TreeNode's Value property, so that later on
                //we will be able to tell each TreeNode's folder ID and
                //sort order without consulting the data source again
                DataColumn nodeData =
                    table.Columns.Add("NodeData", typeof(string));
                nodeData.Expression = "Convert(FolderID, 'System.String') + ',' + Convert(SortOrder, 'System.String')";

                //Setting the sort order
                table.DefaultView.Sort = "SortOrder Asc";

                //Define relations
                DataRelation r = ds.Relations.Add(folderID, parentFolderID);
                r.Nested = true;

                return ds;
            }
       
        public static string getEstructura4(int bInactivos)
        {
            //Define a DataSet
            SqlDataReader dr = DAL.Estructura.getEstructura(bInactivos);
            StringBuilder sb = new StringBuilder();
            int ident = 0;
            while (dr.Read())
            {
                 if (ident == 0)
                 {
                     sb.Append("[{title: '" + dr["DENOMINACION"].ToString().Replace("'", "&#39;") + "', key:'"  + dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "-" + dr["NODO"].ToString() + "-" + dr["SUBNODO"].ToString()  + "' ");
                 }
                else if (ident < int.Parse(dr["INDENTACION"].ToString()))
                {
                    sb.Append(",children:[");
                    sb.Append("{title: '" + dr["DENOMINACION"].ToString().Replace("'", "&#39;") + "', key:'" + dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "-" + dr["NODO"].ToString() + "-" + dr["SUBNODO"].ToString() + "' ");
                    //sb.Append("<li id='" + dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "-" + dr["NODO"].ToString() + "-" + dr["SUBNODO"].ToString() + "'>");
                    //sb.Append(dr["DENOMINACION"].ToString().Replace("'", "&#39;"));
                }
                else if (ident == int.Parse(dr["INDENTACION"].ToString()))
                {
                    sb.Append("},{title: '" + dr["DENOMINACION"].ToString().Replace("'", "&#39;") + "', key:'" + dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "-" + dr["NODO"].ToString() + "-" + dr["SUBNODO"].ToString() + "' ");
                    //sb.Append("<li id='" + dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "-" + dr["NODO"].ToString() + "-" + dr["SUBNODO"].ToString() + "'>");
                    //sb.Append(dr["DENOMINACION"].ToString().Replace("'", "&#39;"));
                }
                else if (ident > int.Parse(dr["INDENTACION"].ToString()))
                {
                    while (ident > int.Parse(dr["INDENTACION"].ToString()))
                    {
                        sb.Append("}]");
                        ident--;
                    }
                    //sb.Append("<li id='" + dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "-" + dr["NODO"].ToString() + "-" + dr["SUBNODO"].ToString() + "'>");
                    //sb.Append(dr["DENOMINACION"].ToString().Replace("'", "&#39;"));
                    sb.Append("},{title: '" + dr["DENOMINACION"].ToString().Replace("'", "&#39;") + "', key:'" + dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "-" + dr["NODO"].ToString() + "-" + dr["SUBNODO"].ToString() + "' ");
                }
                ident = int.Parse(dr["INDENTACION"].ToString());
            }
            while (ident > 1)
            {
                sb.Append("}]");
                ident--;
            }
            sb.Append("}]");
            return sb.ToString();
        }

        public static string getEstructuraCenCos(int bInactivos)
        {
            //Define a DataSet
            SqlDataReader dr = DAL.Estructura.getEstructuraCenCos(bInactivos);
            StringBuilder sb = new StringBuilder();
            int ident = 0;
            int identCierre = 0;
            while (dr.Read())
            {
                string key = "";
                switch (dr["INDENTACION"].ToString())
                {
                    case "1":
                        key = "SN4@#sep#@" + dr["DENOMINACION"].ToString().Replace("'", "&#39;") + "@#sep#@1";
                        break;
                    case "2":
                        key = "SN3@#sep#@" + dr["DENOMINACION"].ToString().Replace("'", "&#39;") + "@#sep#@2";
                        break;
                    case "3":
                        key = "SN2@#sep#@" + dr["DENOMINACION"].ToString().Replace("'", "&#39;") + "@#sep#@3";
                        break;
                    case "4":
                        key = "SN1@#sep#@" + dr["DENOMINACION"].ToString().Replace("'", "&#39;") + "@#sep#@4";
                        break;
                    case "5":
                        if (dr["idNodoSubNodo"].ToString() != "0") //si es un nodo representativo
                            key = "ND@#sep#@" +  dr["DENOMINACION"].ToString().Replace("'", "&#39;") + "@#sep#@5@#sep#@" +dr["idNodoSubNodo"].ToString() + "@#sep#@";
                        else
                            key = "NDNR@#sep#@" + dr["DENOMINACION"].ToString().Replace("'", "&#39;") + "@#sep#@5"; //nodo no representativo
                        break;
                    case "6":
                        key = "SN@#sep#@" + dr["DENOMINACION"].ToString().Replace("'", "&#39;") + "@#sep#@6@#sep#@" + dr["idNodoSubNodo"].ToString() + "@#sep#@";
                        break;
                    case "7":
                        key = "CC@#sep#@" + dr["idNodoSubNodo"].ToString() + "@#sep#@7@#sep#@" + ((dr["estado"].ToString() == "True") ? "1" : "0") + "@#sep#@";
                        break;
                }
           
                    
                if (ident == 0)
                {
                    sb.Append("[{title: '" + dr["DENOMINACION"].ToString().Replace("'", "&#39;") + "', key:'" + key + "' ");
                }
                else if (ident < int.Parse(dr["INDENTACION"].ToString()))
                {
                    sb.Append(",children:[");
                    sb.Append("{title: '" + dr["DENOMINACION"].ToString().Replace("'", "&#39;") + "', key:'" + key + "' ");
                }
                else if (ident == int.Parse(dr["INDENTACION"].ToString()))
                {
                    sb.Append("},{title: '" + dr["DENOMINACION"].ToString().Replace("'", "&#39;") + "', key:'" + key + "' ");
                }
                else if (ident > int.Parse(dr["INDENTACION"].ToString()))
                {
                    while (identCierre > int.Parse(dr["INDENTACION"].ToString()))
                    {
                        sb.Append("}]");
                        identCierre--;
                    }
                    sb.Append("},{title: '" + dr["DENOMINACION"].ToString().Replace("'", "&#39;") + "', key:'" + key + "' ");
                }
                if (identCierre == 5 && int.Parse(dr["INDENTACION"].ToString()) == 7 && ident != 7)
                    identCierre = int.Parse(dr["INDENTACION"].ToString()) - 1;
                else if (ident != 7)
                    identCierre = int.Parse(dr["INDENTACION"].ToString());
                ident = int.Parse(dr["INDENTACION"].ToString());
            }
            while (identCierre > 1)
            {
                sb.Append("}]");
                identCierre--;
            }
            sb.Append("}]");
            return sb.ToString();
        }
        #endregion
     }
}
