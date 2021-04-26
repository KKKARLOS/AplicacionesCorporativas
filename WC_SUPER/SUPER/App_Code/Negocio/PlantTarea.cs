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
//
using SUPER.Capa_Datos;
namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Summary description for PlantTarea
    /// </summary>
    public class PlantTarea
    {
        #region Atributos
        /*
    private int _idTarea;
    private int _idPlant;
    private int _idProyTec;
    private int _idFase;
    private int _idActividad;
    private string _sTipo;//P-> proyecto técnico, F-> fase, A-> actividad, T-> tarea
    private string _sDesTipo;
    private string _descripcion;
    */
        #endregion
        #region Propiedades
        /*
    public int idTarea
    {
        get { return _idTarea; }
        set { _idTarea = value; }
    }
    public int idPlant
    {
        get { return _idPlant; }
        set { _idPlant = value; }
    }
    public int idProyTec
    {
        get { return _idProyTec; }
        set { _idProyTec = value; }
    }
    public int idFase
    {
        get { return _idFase; }
        set { _idFase = value; }
    }
    public int idActividad
    {
        get { return _idActividad; }
        set { _idActividad = value; }
    }
    public string tipo
    {
        get { return _sTipo; }
        set
        {
            _sTipo = value;
            switch (value)
            {
                case "P": _sDesTipo = "P.T."; break;
                case "F": _sDesTipo = "FASE"; break;
                case "A": _sDesTipo = "ACTI."; break;
                case "T": _sDesTipo = "TAREA"; break;
                default: _sDesTipo = "TIPO " + value + " NO CONTEMPLADO"; break;
            }
        }
    }
    public string desTipo
    {
        get { return _sDesTipo; }
        set { _sDesTipo = value; }
    }
    public string descripcion
    {
        get { return _descripcion; }
        set { _descripcion = value; }
    }*/
        #endregion
        public PlantTarea()
        {
            // TODO: Add constructor logic here
        }
        //Metodos
        public static SqlDataReader Catalogo(int iPlant)
        {//Obtención del catalogo usando un DataReader
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPlant", SqlDbType.Int);
            aParam[0].Value = iPlant;

            return SqlHelper.ExecuteSqlDataReader("SUP_PLANT_TARSCATA", aParam);
        }
        public static SqlDataReader CatalogoHitos(int iPlant)
        {//Obtención del catalogo usando un DataReader
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdPlant", SqlDbType.Int);
            aParam[0].Value = iPlant;

            return SqlHelper.ExecuteSqlDataReader("SUP_HITOE_PLANT_CATA", aParam);
        }

        #region Borrar
        public static int Borrar(SqlTransaction tr, string sTipo, int t339_iditems)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t339_tipoitem", SqlDbType.Char, 1);
            aParam[0].Value = sTipo;
            aParam[1] = new SqlParameter("@t339_iditems", SqlDbType.Int, 4);
            aParam[1].Value = t339_iditems;

            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("SUP_ITEMSPLANTILLA_D", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ITEMSPLANTILLA_D", aParam);

            return returnValue;
        }
        #endregion
    }
}