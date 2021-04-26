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
using SUPER.Capa_Datos;

/// <summary>
/// Summary description for Gasto
/// </summary>
public class Gasto
{
    #region Atributos privados

    private int _t001_idficepi;
    private string _t001_codred;
    //private int _t314_idusuario;
    private string _t001_fecbaja;
    private string _Profesional;
    private string _EnviarCorreo;
    private int _nKm;
    private decimal _Km;
    private decimal _Dietas;
    private decimal _Avion;
    private decimal _Comida;
    private decimal _Transp;
    private decimal _Peaje;
    private decimal _Hotel;
    private decimal _PagoConcertado;
    private decimal _Bono;
    private decimal _Total;

    #endregion
    #region Propiedades públicas

    public int t001_idficepi
    {
        get { return _t001_idficepi; }
        set { _t001_idficepi = value; }
    }
    public string Profesional
    {
        get { return _Profesional; }
        set { _Profesional = value; }
    }
    public string t001_codred
    {
        get { return _t001_codred; }
        set { _t001_codred = value; }
    }
    public string t001_fecbaja
    {
        get { return _t001_fecbaja; }
        set { _t001_fecbaja = value; }
    }
    public string EnviarCorreo
    {
        get { return _EnviarCorreo; }
        set { _EnviarCorreo = value; }
    }
    //public int t314_idusuario
    //{
    //    get { return _t314_idusuario; }
    //    set { _t314_idusuario = value; }
    //}
    public decimal Km
    {
        get { return _Km; }
        set { _Km = value; }
    }
    public int nKm
    {
        get { return _nKm; }
        set { _nKm = value; }
    }
    public decimal Dietas
    {
        get { return _Dietas; }
        set { _Dietas = value; }
    }
    public decimal Avion
    {
        get { return _Avion; }
        set { _Avion = value; }
    }
    public decimal Comida
    {
        get { return _Comida; }
        set { _Comida = value; }
    }
    public decimal Transp
    {
        get { return _Transp; }
        set { _Transp = value; }
    }
    public decimal Peaje
    {
        get { return _Peaje; }
        set { _Peaje = value; }
    }
    public decimal Hotel
    {
        get { return _Hotel; }
        set { _Hotel = value; }
    }
    public decimal PagoConcertado
    {
        get { return _PagoConcertado; }
        set { _PagoConcertado = value; }
    }
    public decimal Bono
    {
        get { return _Bono; }
        set { _Bono = value; }
    }
    public decimal Total
    {
        get { return _Total; }
        set { _Total = value; }
    }

    #endregion
    public Gasto()
    {
        _t001_idficepi = 0;
        _t001_codred = "";
        _Profesional = "";
        _t001_fecbaja = "";
        //_t314_idusuario = 0;
        _nKm = 0;
        _Km = 0;
        _Dietas = 0;
        _Avion = 0;
        _Comida = 0;
        _Transp = 0;
        _Peaje = 0;
        _Hotel = 0;
        _PagoConcertado = 0;
        _Bono = 0;
        _Total = 0;
    }
    public Gasto(int t001_idficepi, string t001_codred, string sFecBaja, string EnviarCorreo, string Profesional,
                int nKm, decimal Km, decimal Dietas, decimal Avion,
                decimal Comida, decimal Transp, decimal Peaje, decimal Hotel, decimal PagoConcertado, decimal Bono, decimal Total)
    {
        _t001_idficepi = t001_idficepi;
        _t001_codred = t001_codred;
        _Profesional = Profesional;
        _t001_fecbaja = sFecBaja;
        _EnviarCorreo = EnviarCorreo;
        //_t314_idusuario = 0;
        _nKm = nKm;
        _Km = Km;
        _Dietas = Dietas;
        _Avion = Avion;
        _Comida = Comida;
        _Transp = Transp;
        _Peaje = Peaje;
        _Hotel = Hotel;
        _PagoConcertado = PagoConcertado;
        _Bono = Bono;
        _Total = Total;
    }
    #region VISIBILIDAD DEL GASTO
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Obtiene todos los supervisados de un profesional
    /// </summary>
    /// -----------------------------------------------------------------------------
    public static SqlDataReader getSupervisadosTotal(int t001_idficepi, DateTime dtDesde)
    {
        SqlParameter[] aParam = new SqlParameter[2];
        aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
        aParam[0].Value = t001_idficepi;
        aParam[1] = new SqlParameter("@desde", SqlDbType.SmallDateTime, 4);
        aParam[1].Value = dtDesde;

        return SqlHelper.ExecuteSqlDataReader("PVG_SUPERVISADOS_TOTAL_S", aParam);
    }
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Obtiene los supervisados directos de un profesional
    /// </summary>
    /// -----------------------------------------------------------------------------
    public static SqlDataReader getSupervisados(int t001_idficepi, DateTime dtDesde)
    {
        SqlParameter[] aParam = new SqlParameter[2];
        aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
        aParam[0].Value = t001_idficepi;
        aParam[1] = new SqlParameter("@desde", SqlDbType.SmallDateTime, 4);
        aParam[1].Value = dtDesde;

        return SqlHelper.ExecuteSqlDataReader("PVG_SUPERVISADOS_S", aParam);
    }
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Devuelve una relación de profesionales y sus gastos de viaje
    /// </summary>
    /// -----------------------------------------------------------------------------
    public static SqlDataReader GetProfGasto(DateTime dtDesde, DateTime dtHasta)
    {
        SqlParameter[] aParam = new SqlParameter[2];
        aParam[0] = new SqlParameter("@desde", SqlDbType.SmallDateTime, 4);
        aParam[0].Value = dtDesde;
        aParam[1] = new SqlParameter("@hasta", SqlDbType.SmallDateTime, 4);
        aParam[1].Value = dtHasta;

        return SqlHelper.ExecuteSqlDataReader("PVG_GASTOSxPROFESIONAL", aParam);
    }
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Devuelve el acumulado de los gastos de todos los supervisados y descendientes
    /// de un profesional en un periodo determinado
    /// </summary>
    /// -----------------------------------------------------------------------------
    public static SqlDataReader getGastosAcumulados(int t001_idficepi, DateTime dtDesde, DateTime dtHasta)
    {
        SqlParameter[] aParam = new SqlParameter[3];
        aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
        aParam[0].Value = t001_idficepi;
        aParam[1] = new SqlParameter("@desde", SqlDbType.SmallDateTime, 4);
        aParam[1].Value = dtDesde;
        aParam[2] = new SqlParameter("@hasta", SqlDbType.SmallDateTime, 4);
        aParam[2].Value = dtHasta;

        return SqlHelper.ExecuteSqlDataReader("PVG_GASTOSxSUPERVISOR", aParam);
    }
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Devuelve el nº de supervisados y descendientes de un profesional 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public static int getNumSupervisados(int t001_idficepi)
    {
        SqlParameter[] aParam = new SqlParameter[1];
        aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
        aParam[0].Value = t001_idficepi;

        return System.Convert.ToInt32(SqlHelper.ExecuteScalar("PVG_NUM_SUPERVISADOS", aParam));
    }
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Devuelve una relación de proyectos y sus gastos de viaje en los que el profesional
    /// es responsable del proyecto y el proyecto tiene gastos de viaje
    /// </summary>
    /// -----------------------------------------------------------------------------
    public static SqlDataReader getProyectosResponsable(int t001_idficepi, int anomesD, int anomesH, int iVolMin, double dRatio)
    {
        SqlParameter[] aParam = new SqlParameter[5];
        aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
        aParam[0].Value = t001_idficepi;
        aParam[1] = new SqlParameter("@desde", SqlDbType.Int, 4);
        aParam[1].Value = anomesD;
        aParam[2] = new SqlParameter("@hasta", SqlDbType.Int, 4);
        aParam[2].Value = anomesH;
        aParam[3] = new SqlParameter("@VolMin", SqlDbType.Int, 4);
        aParam[3].Value = iVolMin;
        aParam[4] = new SqlParameter("@dRatio", SqlDbType.Real, 4);
        aParam[4].Value = dRatio;

        return SqlHelper.ExecuteSqlDataReader("PVG_PROYECTOS_RESPONSABLE_S", aParam);
    }
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Devuelve una relación de proyectos (top 30) y sus gastos de viaje en los que el profesional
    /// es responsable de supraestructura de proyecto y no de proyecto directamente
    /// Y el proyecto tiene gasto de viaje
    /// </summary>
    /// -----------------------------------------------------------------------------
    public static SqlDataReader getProyectosHeredados(int t001_idficepi, int anomesD, int anomesH, int iTop, double dRatio, int iVolMin)
    {
        SqlParameter[] aParam = new SqlParameter[6];
        aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
        aParam[0].Value = t001_idficepi;
        aParam[1] = new SqlParameter("@desde", SqlDbType.Int, 4);
        aParam[1].Value = anomesD;
        aParam[2] = new SqlParameter("@hasta", SqlDbType.Int, 4);
        aParam[2].Value = anomesH;
        aParam[3] = new SqlParameter("@iTop", SqlDbType.Int, 4);
        aParam[3].Value = iTop;
        aParam[4] = new SqlParameter("@dRatio", SqlDbType.Real, 4);
        aParam[4].Value = dRatio;
        aParam[5] = new SqlParameter("@VolMin", SqlDbType.Int, 4);
        aParam[5].Value = iVolMin;

        return SqlHelper.ExecuteSqlDataReader("PVG_PROYECTOS_HEREDADO_S", aParam);
    }

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Inserta un registro en la tabla T683_CRITERIOSVG.
    /// </summary>
    /// <returns></returns>
    /// <history>
    /// 	Creado por [sqladmin]	16/09/2011 9:30:45
    /// </history>
    /// -----------------------------------------------------------------------------
    public static void InsertCriterio(SqlTransaction tr, bool t683_profesionales, bool t683_supervisores, bool t683_proyectos,
                              int t683_top, int t683_volneg, double t683_ratio)
    {
        SqlParameter[] aParam = new SqlParameter[6];
        aParam[0] = new SqlParameter("@t683_profesionales", SqlDbType.Bit, 1);
        aParam[0].Value = t683_profesionales;
        aParam[1] = new SqlParameter("@t683_supervisores", SqlDbType.Bit, 1);
        aParam[1].Value = t683_supervisores;
        aParam[2] = new SqlParameter("t683_proyectos", SqlDbType.Bit, 1);
        aParam[2].Value = t683_proyectos;
        aParam[3] = new SqlParameter("@t683_top", SqlDbType.Int, 4);
        aParam[3].Value = t683_top;
        aParam[4] = new SqlParameter("@t683_volneg", SqlDbType.Int, 4);
        aParam[4].Value = t683_volneg;
        aParam[5] = new SqlParameter("@t683_ratio", SqlDbType.Real, 4);
        aParam[5].Value = t683_ratio;

        if (tr == null)
            SqlHelper.ExecuteNonQuery("PVG_CRITERIOSVG_I", aParam);
        else
            SqlHelper.ExecuteNonQueryTransaccion(tr, "PVG_CRITERIOSVG_I", aParam);
    }

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Elimina un registro de la tabla T683_CRITERIOSVG 
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	16/09/2011 9:30:45
    /// </history>
    /// -----------------------------------------------------------------------------
    public static int DeleteCriterio(SqlTransaction tr)
    {
        SqlParameter[] aParam = new SqlParameter[0];
        if (tr == null)
            return SqlHelper.ExecuteNonQuery("PVG_CRITERIOSVG_D", aParam);
        else
            return SqlHelper.ExecuteNonQueryTransaccion(tr, "PVG_CRITERIOSVG_D", aParam);
    }
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Lee un registro de la tabla T683_CRITERIOSVG 
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	16/09/2011 9:30:45
    /// </history>
    /// -----------------------------------------------------------------------------
    public static SqlDataReader getCriterio()
    {
        SqlParameter[] aParam = new SqlParameter[0];

        return SqlHelper.ExecuteSqlDataReader("PVG_CRITERIOSVG_S", aParam);
    }
    #endregion
}
