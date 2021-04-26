using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using IB.SUPER.Shared;

public partial class Capa_Presentacion_Pruebas_formaPM_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = "";

        try
        {
            //default.aspx?aWQ9NzU4Mw== --> id=7583
            Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());
            id = ht["id"].ToString(); 

            string script1 = "IB.vars.idpersona = '" + id + "';";
            script1 += "IB.vars.codred = '" + Session["IDRED"].ToString() + "'";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);

        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al cargar la pantalla de prueba", ex);

            string script2 = "IB.vars.error = 'Parámetros incorrectos en la carga de la pantalla';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script2", script2, true);
        } 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Persona>  buscar(string filtro)
    {
        List<Persona> lst = new List<Persona>();
        Persona oPersona;

        try
        {
            for (int i = 0; i < 15; i++) {
                oPersona = new Persona();
                oPersona.idficepi = i;
                oPersona.nombre = "nombre" + i.ToString();
                oPersona.apellido = "apellido" + i.ToString();
                lst.Add(oPersona);
            
            }
            return lst;
        }
        catch (Exception ex)
        {
            LogError.LogearError("Ocurrió un error al obetener la lista de personas", ex);
            throw new Exception(System.Uri.EscapeDataString("Ocurrió un error al obetener la lista de personas"));
        }
        finally
        {
            
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string actualizar(Persona datos)
    {
        return datos.idficepi.ToString() + " " + datos.nombre + " " + datos.apellido;
    }
}





public class Persona {

    public int idficepi { get; set; }
    public string nombre { get; set; }
    public string apellido { get; set; }

}