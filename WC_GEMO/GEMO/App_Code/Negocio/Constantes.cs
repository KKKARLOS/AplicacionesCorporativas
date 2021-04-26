using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

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

    //Constantes que definen el tipo de registro en la tabla T329_CLASECO
    public const int IngExtServProf = 35;//Ingresos Externos De Serv. Profesionales. Antes 207050 en TIPO_ING_GASTO; Hasta 23/11/2009 era la 26. Modificado a raiz de los cambios de clases y conceptos.
    public const int IngExtServProfGrupo = 32;//INGRESOS EXT.SERV.PROFESIONALES GRUPO. Antes el 227052 en TIPO_ING_GASTO

    public const int AjusteProdCont = 22; //Prod. Ajuste Con. Contratación. Antes el 337600 en TIPO_ING_GASTO
    public const int nIdClaseObraEnCurso = 21; //id de la clase utilizada para realizar el paso del 20% de la obra en curso.
    public const int nIdClienteIbermaticaSA = 8433; //id del cliente "Ibermática S.A." que se utiliza para la generación de proyectos improductivos genéricos

    public const string sPrefijo = "ctl00$CPHC$"; //prefijo de los controles recogidos vía Request.Form
    public const string sUsuarioProxy = "YXBsaWNhY2lvbl9zdXBlcg==";
    public const string sPwdUsuarioProxy = "QHBMMXN1UDNyJTIx";
    public const string sDominioUsuarioProxy = "SUJFUk1BVElDQQ==";

    public const int nNumElementosMaxCriterios = 100;//número máximo de elementos para mostrar o no la pantalla de criterios con o sin búsqueda
    public const int nNumMaxTablaCatalogo = 1000;
    public const int nNumMaxTablaCatalogoProyectos = 500;
} 
