using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using GASVI.DAL;

/// <summary>
/// Summary description for Constantes
/// </summary>
public class Constantes
{
	public Constantes()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public const string sPrefijo = "ctl00$CPHC$"; //prefijo de los controles recogidos vía Request.Form
    public const string sUsuarioProxy = "YXBsaWNhY2lvbl9zdXBlcg==";
    public const string sPwdUsuarioProxy = "QHBMMXN1UDNyJTIx";
    public const string sDominioUsuarioProxy = "SUJFUk1BVElDQQ==";

    public const int nNumElementosMaxCriterios = 100;//número máximo de elementos para mostrar o no la pantalla de criterios con o sin búsqueda
    public const int nNumMaxTablaCatalogo = 1000;
    public const int nNumMaxTablaCatalogoProyectos = 500;

    public static int nNumeroMinimoKmsECO
    {
        get { return ObtenerNumeroMinimoKmsECO(); }
    }
    public static int ObtenerNumeroMinimoKmsECO()
    {
        int nMinimo = 0;
        if (HttpContext.Current.Cache.Get("MinimoKmsECO") == null)
        {
            //Hashtable htModulos = new Hashtable();
            SqlDataReader dr = PARAMETRIZACIONECO.Obtener(null);
            while (dr.Read())
            {
                nMinimo = int.Parse(dr["t723_minkms"].ToString());
            }
            dr.Close();
            dr.Dispose();
            HttpContext.Current.Cache.Insert("MinimoKmsECO", nMinimo, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
        }
        else
        {
            nMinimo = (int)HttpContext.Current.Cache.Get("MinimoKmsECO");
        }
        return nMinimo;
    }

} 
