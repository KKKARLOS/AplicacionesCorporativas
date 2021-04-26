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
//
using SUPER.Capa_Datos;
namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Summary description for PlantProy
    /// </summary>
    public class PlantProy
    {
        #region Atributos
        private int _id;
        private string _tipo;
        private string _desTipo;
        private string _ambito;
        private string _desAmbito;
        private string _descripcion;
        private bool _activo;
        private int _promotor;
        private int _codune;
        private string _desune;
        private string _obs;
        #endregion

        #region Propiedades
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string tipo
        {
            get { return _tipo; }
            set
            {
                _tipo = value;
                switch (value)
                {
                    case "E": _desTipo = "EMPRESARIAL"; break;
                    case "D": _desTipo = "DEPARTAMENTAL"; break;
                    case "P": _desTipo = "PERSONAL"; break;
                    default: _desTipo = "TIPO " + value + " NO CONTEMPLADO"; break;
                }
            }
        }
        public string desTipo
        {
            get { return _desTipo; }
            set { _desTipo = value; }
        }
        public string ambito
        {
            get { return _ambito; }
            set
            {
                _ambito = value;
                switch (value)
                {
                    case "E": _desAmbito = "PROY.ECO."; break;
                    case "T": _desAmbito = "PROY.TEC."; break;
                    default: _desAmbito = "AMBITO " + value + " NO CONTEMPLADO"; break;
                }
            }
        }
        public string desAmbito
        {
            get { return _desAmbito; }
            set { _desAmbito = value; }
        }
        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        public bool activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
        public int promotor
        {
            get { return _promotor; }
            set { _promotor = value; }
        }
        public int codune
        {
            get { return _codune; }
            set { _codune = value; }
        }
        public string desune
        {
            get { return _desune; }
            set { _desune = value; }
        }
        public string obs
        {
            get { return _obs; }
            set { _obs = value; }
        }

        #endregion
        //Constructores
        public PlantProy()
        {//Constructor
            this.id = -1;
            this.tipo = "";
            this.ambito = "";
            this.descripcion = "";
            this.activo = true;
            this.promotor = -1;
            this.codune = -1;
            this.desune = "";
            this.obs = "";
        }
        public PlantProy(int nId)
        {//Constructor
            this.id = nId;
        }
        public PlantProy(int nId, string sDes, bool bActivo, string sTipo, string sAmbito, int nCodUne, string sDesUne)
        {//Constructor
            this.id = nId;
            this.descripcion = sDes;
            this.activo = bActivo;
            this.tipo = sTipo;
            this.ambito = sAmbito;
            this.promotor = -1;
            this.codune = nCodUne;
            this.desune = sDesUne;
            this.obs = "";
        }
        public PlantProy(int nId, string sDes, bool bActivo, string sTipo, int nPromotor, int nCodUne, string sDesUne, string sAmbito, string sObs)
        {//Constructor
            this.id = nId;
            this.descripcion = sDes;
            this.activo = bActivo;
            this.tipo = sTipo;
            this.ambito = sAmbito;
            this.promotor = nPromotor;
            this.codune = nCodUne;
            this.desune = sDesUne;
            this.obs = sObs;
        }
        //Metodos
        public static SqlDataReader Catalogo(int nOrden, int nAscDesc, string sTipo, int nCodUne, int nPromotor, string sOrigen, 
                                             bool bEmp, bool bDep, bool bPer)
        {//Obtención del catalogo usando un DataReader 
            SqlParameter[] aParam = new SqlParameter[9];
            aParam[0] = new SqlParameter("@nOrden", SqlDbType.Int);
            aParam[0].Value = nOrden;
            aParam[1] = new SqlParameter("@nAscDesc", SqlDbType.Int);
            aParam[1].Value = nAscDesc;
            aParam[2] = new SqlParameter("@sTipo", SqlDbType.VarChar, 2);
            aParam[2].Value = sTipo;
            if (nCodUne == -1)
            {
                aParam[3] = new SqlParameter("@nUser", SqlDbType.Int, 2);
                aParam[3].Value = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
            }
            else
            {
                aParam[3] = new SqlParameter("@nCR", SqlDbType.Int, 2);
                aParam[3].Value = nCodUne;
            }
            aParam[4] = new SqlParameter("@nPromotor", SqlDbType.Int, 4);
            aParam[4].Value = nPromotor;
            aParam[5] = new SqlParameter("@sOrigen", SqlDbType.Char, 1);
            aParam[5].Value = sOrigen;
            aParam[6] = new SqlParameter("@bEmp", SqlDbType.Bit, 1);
            aParam[6].Value = bEmp;
            aParam[7] = new SqlParameter("@bDep", SqlDbType.Bit, 1);
            aParam[7].Value = bDep;
            aParam[8] = new SqlParameter("@bPer", SqlDbType.Bit, 1);
            aParam[8].Value = bPer;

            if (nCodUne == -1)
            {
                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    return SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLASCATA3", aParam);
                else
                    return SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLASCATA2", aParam);
            }
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLASCATA", aParam);
        }
        public static SqlDataReader CatalogoPlantillasPE(int idNodo, int nUsuario, string sTipo)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@idNodo", SqlDbType.Int);
            aParam[1] = new SqlParameter("@nUsuario", SqlDbType.Int);
            aParam[2] = new SqlParameter("@sTipo", SqlDbType.Char, 1);

            aParam[0].Value = idNodo;
            aParam[1].Value = nUsuario;
            aParam[2].Value = sTipo;

            return SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLASCATA_PE", aParam);
        }

        public void Obtener(int nIdPlant)
        {
            int iActivo = 0;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sIdPlant", SqlDbType.Int, 4);
            aParam[0].Value = nIdPlant;
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLAS", aParam);

            if (dr.Read())
            {
                this.id = nIdPlant;
                this.tipo = dr["t338_tipo"].ToString();
                this.ambito = dr["t338_ambito"].ToString();
                this.descripcion = dr["t338_denominacion"].ToString();

                iActivo = int.Parse(dr["t338_estado"].ToString());
                if (iActivo == 0) this.activo = false;
                else this.activo = true;

                this.promotor = int.Parse(dr["promotor"].ToString());
                this.codune = int.Parse(dr["cod_une"].ToString());
                this.desune = dr["nom_une"].ToString();
                this.obs = dr["t338_descripcion"].ToString();
            }
            dr.Close();
            dr.Dispose();
        }
        public static PlantProy Select(int nIdPlant)
        {
            PlantProy oPlant = new PlantProy();
            int iActivo = 0;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sIdPlant", SqlDbType.Int, 4);
            aParam[0].Value = nIdPlant;
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLAS", aParam);

            if (dr.Read())
            {
                oPlant.id = nIdPlant;
                oPlant.tipo = dr["t338_tipo"].ToString();
                oPlant.ambito = dr["t338_ambito"].ToString();
                oPlant.descripcion = dr["t338_denominacion"].ToString();

                iActivo = int.Parse(dr["t338_estado"].ToString());
                if (iActivo == 0) oPlant.activo = false;
                else oPlant.activo = true;

                oPlant.promotor = int.Parse(dr["promotor"].ToString());
                oPlant.codune = int.Parse(dr["cod_une"].ToString());
                oPlant.desune = dr["nom_une"].ToString();
                oPlant.obs = dr["t338_descripcion"].ToString();
            }
            dr.Close();
            dr.Dispose();
            return oPlant;
        }
        public static void Eliminar(int nIdPlant)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdPlant", SqlDbType.Int, 4);
            aParam[0].Value = nIdPlant;

            SqlHelper.ExecuteNonQuery("SUP_PLANTILLAD", aParam);
        }
        public static void Modificar(int nIdPlant, string sAmbito, string desc, int estado, int promotor, int codune, string obs)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@nIdPlant", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@sDesPlant", SqlDbType.VarChar, 50);
            aParam[2] = new SqlParameter("@nEstado", SqlDbType.Bit, 1);
            aParam[3] = new SqlParameter("@sAmbito", SqlDbType.VarChar, 1);
            aParam[4] = new SqlParameter("@nPromotor", SqlDbType.Int, 4);
            aParam[5] = new SqlParameter("@nCodUne", SqlDbType.Int, 2);
            aParam[6] = new SqlParameter("@sObs", SqlDbType.Text);

            aParam[0].Value = nIdPlant;
            aParam[1].Value = desc;
            aParam[2].Value = estado;
            aParam[3].Value = sAmbito;
            aParam[4].Value = promotor;
            aParam[5].Value = codune;
            aParam[6].Value = obs;

            SqlHelper.ExecuteNonQuery("SUP_PLANTILLAU", aParam);
        }
        public static int Insertar(SqlTransaction tr, string tipo, string desc, int estado, string ambito, int promotor, 
                                    int codune, string obs)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@sTipo", SqlDbType.VarChar, 1);
            aParam[1] = new SqlParameter("@sDesPlant", SqlDbType.VarChar, 50);
            aParam[2] = new SqlParameter("@nEstado", SqlDbType.Bit, 1);
            aParam[3] = new SqlParameter("@sAmbito", SqlDbType.VarChar, 1);
            aParam[4] = new SqlParameter("@nPromotor", SqlDbType.Int, 4);
            aParam[5] = new SqlParameter("@nCodUne", SqlDbType.Int, 2);
            aParam[6] = new SqlParameter("@sObs", SqlDbType.Text);

            aParam[0].Value = tipo;
            aParam[1].Value = desc;
            aParam[2].Value = estado;
            aParam[3].Value = ambito;
            aParam[4].Value = promotor;
            aParam[5].Value = codune;
            aParam[6].Value = obs;

            return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PLANTILLAI", aParam));
        }
    }
}