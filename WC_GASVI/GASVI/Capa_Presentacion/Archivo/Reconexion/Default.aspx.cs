using System;
//para manejar listas
using System.Web.UI;

using GASVI.BLL;
using System.Text.RegularExpressions;

public partial class Reconexion : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {

            Master.TituloPagina = "Reconexión";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["GVT_IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("CargarProf"):
                sResultado += ObtenerDatos(aArgs[1], aArgs[2], aArgs[3]);
                break;
            //case ("SelecProf"):
            //    sResultado += SeleccionarProfesional(aArgs[1].ToString());
            //    break;
        }

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    private string ObtenerDatos(string strApellido1, string strApellido2, string strNombre)
    {
        try
        {
            return "OK@#@" + Profesional.ObtenerCatalogo(Utilidades.unescape(strApellido1), Utilidades.unescape(strApellido2), Utilidades.unescape(strNombre));
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los profesionales para la reconexión", ex);
        }
    }
    //private string SeleccionarProfesional(string strCodRed)
    //{
    //    try
    //    {
    //        string strOperacion = "OK@#@";

    //        Recurso objRec = new Recurso();

    //        bool bIdentificado = objRec.ObtenerRecurso(strCodRed, null);

    //        if (bIdentificado)
    //        {
    //            //Borro la lista de perfiles x Nodo que corresponden al usuario anterior
    //            HttpContext.Current.Session["GVT_Acceso_Nodos"] = null;
    //            Session["GVT_ADMINISTRADOR"] = objRec.Admin;
    //            Session["GVT_IDRED"] = strCodRed;
    //            Session["GVT_IDFICEPI"] = objRec.IdFicepi;
    //            Session["GVT_NOMBRE"] = objRec.Nombre;
    //            Session["GVT_APELLIDO1"] = objRec.Apellido1;
    //            Session["GVT_APELLIDO2"] = objRec.Apellido2;
    //            //Session["GVT_NUM_EMPLEADO"] = objRec.IdUsuario;
    //            Session["GVT_UsuarioActual"] = objRec.IdUsuario;
    //            if (objRec.esAdmin)
    //                Session["GVT_AdminActual"] = "A";
    //            else
    //                Session["GVT_AdminActual"] = "";
    //            //Session["GVT_FOTOUSUARIO"] = objRec.t001_foto;
    //            Recurso.CargarRoles(objRec.IdUsuario.ToString(), objRec.Admin);
    //            //Si es usuario único, se actualizan las variables de session y no se muestra la ventana modal de selección de CR.
    //            //if (ControlUsuarioUnico())
    //                strOperacion += Session["GVT_strServer"] + "Capa_Presentacion/Archivo/SeleccionActual/Default.aspx";
    //            //else
    //            //    strOperacion += Session["GVT_strServer"] + "Capa_Presentacion/Archivo/SeleccionActual/Default.aspx?m=1";
    //        }
    //        else
    //        {
    //            strOperacion = "ERROR@#@No se han podido obtener los datos del usuario '" + strCodRed + "'";
    //        }

    //        objRec = null;

    //        return strOperacion;
    //    }
    //    catch (Exception ex)
    //    {
    //        return "Error@#@" + Errores.mostrarError("Error al seleccionar el profesional", ex);
    //    }

    //}
    //private bool ControlUsuarioUnico()
    //{
    //    bool bUnico = false;
    //    SqlDataReader dr = Recurso.ObtenerUsuarios(Session["GVT_IDRED"].ToString());

    //    int i = 0;
    //    string sUsuario = "";
    //    while (dr.Read())
    //    {
    //        sUsuario = dr["NUM_EMPLEADO"].ToString();
    //        i++;
    //    }
    //    dr.Close();
    //    dr.Dispose();

    //    if (i == 1) //Un único usuario
    //    {
    //        //SqlDataReader dr1 = Recurso.ObtenerCRsAcceso(int.Parse(sUsuario));
    //        //Cargar lista de nodos accesibles
    //        List<NodoFigura> MisNodos = NODO.ListaAccesoUsuario();

    //        //string sPerfil = "";
    //        string sCR = "";
    //        string sDesCR = "";
    //        string sUMC = "";
    //        foreach (NodoFigura oNF in MisNodos)
    //        {
    //            //sPerfil = oNF.sFigura;
    //            sCR = oNF.nCodigo.ToString();
    //            sDesCR = oNF.sDescNodo;
    //            sUMC = oNF.nUltCierreEco.ToString();
    //        }

    //        if (MisNodos.Count == 1)//Acceso a un único CR
    //        {
    //            bUnico = true;
    //            //ActualizarCR(sUsuario, sCR, sDesCR, sUMC);
    //        }
    //    }

    //    return bUnico;
    //}
}
