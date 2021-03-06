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
    //Constante que define el tipo de registro en la tabla T447_FICHEROSMANIOBRA
    public const byte FicheroFacturasSAP = 1;
    public const byte FicheroDatos= 2;
    public const byte FicheroIAP = 1;

    //Constantes que definen el tipo de registro en la tabla T329_CLASECO
    public const int IngExtServProf = 35;//Ingresos Externos De Serv. Profesionales. Antes 207050 en TIPO_ING_GASTO; Hasta 23/11/2009 era la 26. Modificado a raiz de los cambios de clases y conceptos.
    public const int IngExtServProfGrupo = 32;//INGRESOS EXT.SERV.PROFESIONALES GRUPO. Antes el 227052 en TIPO_ING_GASTO

    public const int AjusteProdCont = 22; //Prod. Ajuste Con. Contrataci?n. Antes el 337600 en TIPO_ING_GASTO
    public const int nIdClaseObraEnCurso = 21; //id de la clase utilizada para realizar el paso del 20% de la obra en curso.
    public const int nIdClaseDotacionObraEnCurso = 41; //id de la clase utilizada para realizar el paso de la obra en curso antigua.
    //public const int nIdClienteIbermaticaSA = 8433; //id del cliente "Iberm?tica S.A." que se utiliza para la generaci?n de proyectos improductivos gen?ricos
    public const int nIdClaseProductividad = 2; //id de la clase utilizada para cargar los incentivos de productividad.
    public const int nIdClaseProductividadSS = 67; //id de la clase utilizada para cargar el coste de seguridad social de los incentivos de productividad.

    public const string sPrefijo = "ctl00$CPHC$"; //prefijo de los controles recogidos v?a Request.Form
    public const string sUsuarioProxy = "YXBsaWNhY2lvbl9zdXBlcg==";
    public const string sPwdUsuarioProxy = "QHBMMXN1UDNyJTIx";
    public const string sDominioUsuarioProxy = "SUJFUk1BVElDQQ==";

    public const int nNumElementosMaxCriterios = 100;//n?mero m?ximo de elementos para mostrar o no la pantalla de criterios con o sin b?squeda
    public const int nNumMaxTablaCatalogo = 1000;
    public const int nNumMaxTablaCatalogoProyectos = 500;

    //Valores por defecto para Nueva L?nea de Oferta (NLO) cuando el proyecto no tiene contrato
    public const int gIdNLO_Defecto = 55;
    public const string gsDenNLO_Defecto = "L?nea Oferta Tradicional";

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

    ///Im?genes en formato Base64 para incluir en los correos.
    //fondoEncabezamientoListas.gif
    public static string fondoEncabezamientoListas = "data:image/gif;base64,R0lGODlhAQARAIH/ADphcoOvw1iUrv///yH/C05FVFNDQVBFMi4wAwEBAAAh+QQAAAAAACwAAAAAAQARAAAIDQADBBBAsKDBggAABAQAOw==";
    //fondoTotalResListas.gif
    public static string fondoTotalResListas = "data:image/gif;base64,R0lGODlhAQARAIEAAM/h6LzU36++xf4BAiH/C05FVFNDQVBFMi4wAwEBAAAh+QQFFAADACwAAAAAAQARAAAIDQABAAhAsKDBggIEBAQAOw==";
    //imgOK.gif
    public static string imgOK = "data:image/gif;base64,R0lGODlhCgAKAIQAAEhOlkhOlUhOk0hOlEdNld3e60RKkEdNkZibxO7v9mZrp19ko9ja6VZbnkdNlEZMkEZMkkdNknB1rYmMu0lPlrq810ZMkVJYnJ6hx+fn8VpfoExSmP///wAAAAAAAAAAACH/C05FVFNDQVBFMi4wAwEBAAAh+QQBAAAcACwAAAAACgAKAAAIPgA5CBxIsODADRcMchgAoIFBCwEoCAQwMEIACBMBOChwAMCAChMJBDAQ4AECghgEBBCwwKAEAhQNMlCQoWBAADs=";
    //punteado.gif
    public static string punteado = "data:image/gif;base64,R0lGODlhCQAPAIEBADNmmf///wAAAAAAACH/C05FVFNDQVBFMi4wAwEBAAAh+QQBAAABACwAAAAACQAPAAAIGwADCBxIsKDBgwgTJgTAkCHBhg4VSpxIsaLBgAA7";
    //imgLogoAplicacion.gif //SUPER.net
    public static string imgLogoAplicacion = "data:image/gif;base64,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";
    //bckSinTrainera.gif //SUPER.net
    public static string bckSinTrainera = "data:image/gif;base64,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";
    //logoIbermatica2.gif //SUPER.net
    public static string logoIbermatica2 = "data:image/gif;base64,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";
    //imgFT16.gif //SUPER.net
    public static string imgFT16 = "data:image/gif;base64,R0lGODlhAgAgAIEAAP///+bu8gAAAAAAACH/C05FVFNDQVBFMi4wAwEBAAAh+QQAAAAAACwAAAAAAgAgAAAIFQADCBxIsKDBgwQBKFzIsKHDhwwDAgA7";
    //imgGrupoIbermatica.gif //SUPER.net
    public static string imgGrupoIbermatica = "data:image/gif;base64,R0lGODlhpwAbAIQfAJW0xEN7mMra4ejt8cLT3LXK1lKFoU2CnqvE0drk6miVrJ+8yoeqveHq71yMpXKcstbi56bAz32juNHf5vv6+t7n7bvP2k+Cnurv8UqAnE+Fn1WHokd+m1CDnlGEn////yH/C05FVFNDQVBFMi4wAwEBAAAh+QQBAAAfACwAAAAApwAbAAAI/wA/CBxIsKDBgwgTJmzAQMIDAA0USpxIsaLFixgzarQ4wQGHDBwCRNhIsuSAiBMplFzJEmEDCAkoPOBwIIOCAhQt4Dw4wOHBBAweoCRYgAGDnww2EDBYwIEDiQMYXADQsirGBgQQAJCgYIODBwgiKgiwAQHFBGM3/PQYwELBBg4CBBBQUILcDBAKEtgg1yzBBSGfJoSgIKQEq4gRJiAQgYECA3I5OGAQIe/ABQsGUBwQl8MGzQQrdN5AdyBcsocJMghwIMADvSEzOLAsEEAAyTsPQmDLoXRixBMQSHCgIeTtDRIKJGAp4MECAhMMMnAgIQLogQ/ADhVIwKkBCcsJRv9YYMG3QKAFCGBQSKFB8KW/WzYo6iCD3AMbFDBAYD5+xQoSbeffgBsJsMADrQVggH4FRFcQBRBi0EACEEwggAAEWKAhAgiMB8CHH2Im4gIRdIhAehcKMIGFFxLgoosCWOCijBnKmGKNOunEYQQegghAiQVs2KGIPn5o1JESOKSAAtklycCPBQgQ0QDXtTSABdPJpcEDEQiw3kARPODUmGNuYIAHHlxwQE0ctGmcm23KFZmcd2VwwAUXdKCnmjVlYGeffgbq55prBvpRnHLGqehH9iV6G5yQMiqon27eht9k8K2UQAES1ETdAl4eFIGlhRJ6J5557qlnB2iyqoEHerb/iqYGtMK6Kq2E0qrBqqryymuqHQDLqq1owvqqsQd4cKysyqKZJ7HFRtussq+uqV+VGA2wQGEHcBmeQg9k8OqwsSpLrrTFnjtstOuuii60zJb77rnpUjutnhoYQC+ssJ6pa63vturqmlRtRAADB3AAFoAVecQvu/8u+zCx7sYq77r1XpwuwA9zjDGv7J5pwAEGXLBrsCgHa+qp8grMarIJK5ApRgUUpsAC31rUlJt/EorqAR0QmjKewaL6s5qoqnyqqUYT7W69xp5c7r7NdvBmoX+yaoC+G6R5AUh0ht2mbABgWxEBD2jAQH8YYVAAAEEpQKYDG9RtpshbG3C3mXzbrF133oB7sPWqW/vtQZ+RCbom0kQTrcGdPif7JIcy2njhiitSaKGOHY4nIo8LFPAlRs2BZXZLEZ7UwIQJUAhT6xVUkIDssc9eQQO3y9767hBUiDlMt+NuYYYILAC3kmT+3cEGXy9fJt1+EXhRT7lJbz1JFJxEYQMYSjkABieNfv345Jdv/vnop6/++uy37/778Mcv//z012///fjnr//+/Pfv//8ADKAAB8i/gAAAOw==";
} 
