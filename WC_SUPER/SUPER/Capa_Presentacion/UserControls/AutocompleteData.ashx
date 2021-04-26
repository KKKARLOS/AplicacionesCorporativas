<%@ WebHandler Language="C#" Class="AutocompleteData"%>
using System;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using SUPER.DAL;
using SUPER.Capa_Negocio;

public class AutocompleteData : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
 
    public void ProcessRequest(HttpContext context)
    {
        string opcion = context.Request.QueryString["opcion"];
        string sqlParam = Utilidades.unescape(context.Request.QueryString["query"]);
        StringBuilder sbID = new StringBuilder();
        StringBuilder sbNombre = new StringBuilder();
        //StringBuilder sbTipo = new StringBuilder();
        //StringBuilder sbModalidad = new StringBuilder();
        //StringBuilder sbTic = new StringBuilder();
        StringBuilder sbOtrosParamentros = new StringBuilder();
        SqlDataReader dr = null;
        
        switch (opcion)
        {
            case "proveedor":
                
                dr = CuentasCVT.CatalogoProv(sqlParam, "C");
                while (dr.Read())
                {
                    sbID.Append(((sbID.Length > 0) ? "," : "") + "'" + dr["Id"].ToString() + "'");
                    sbNombre.Append(((sbNombre.Length > 0) ? "," : "") + "\"" + dr["Descripcion"].ToString().Replace("\"", "'") + "\"");
                }
                
                break;
                
            case "certificado":
                string sEntidadCert = "", sEntornoTec = "";
                int? eC = (context.Request.QueryString["entCert"]=="") ? null : (int?)int.Parse(context.Request.QueryString["entCert"].ToString());
                bool? valC = (context.Request.QueryString["valido"] == "") ? null : (bool?)Convert.ToBoolean(context.Request.QueryString["valido"].ToString());
                int idFicepiC = int.Parse(context.Request.QueryString["idFicepi"].ToString());
                dr = Certificado.obtenerCertificadoEntidadCert(eC, sqlParam, valC, idFicepiC);
                StringBuilder sbAbrev = new StringBuilder();
                StringBuilder sbValido = new StringBuilder();
                StringBuilder sbEntCert = new StringBuilder();
                StringBuilder sbEntorno = new StringBuilder();
                while (dr.Read())
                {
                    sEntidadCert = dr["T576_IDCRITERIO"].ToString() + "##" + dr["t576_nombre"].ToString().Replace("\"", "'");
                    sEntornoTec = dr["T036_IDCODENTORNO"].ToString() + "##" + dr["t036_descripcion"].ToString().Replace("\"", "'");

                    sbID.Append(((sbID.Length > 0) ? "," : "") + "'" + dr["T582_IDCERTIFICADO"].ToString() + "'");
                    sbNombre.Append(((sbNombre.Length > 0) ? "," : "") + "\"" + dr["T582_NOMBRE"].ToString().Replace("\"", "'") + "\"");
                    sbAbrev.Append(((sbAbrev.Length > 0) ? "," : "," + Environment.NewLine + "abreviatura:[") + "\"" + dr["T582_ABREV"].ToString().Replace("\"", "'") + "\"");
                    sbValido.Append(((sbValido.Length > 0) ? "," : "," + Environment.NewLine + "valido:[") + "\"" + dr["T582_VALIDO"].ToString().Replace("\"", "'") + "\"");
                    //sbEntCert.Append(((sbEntCert.Length > 0) ? "," : "," + Environment.NewLine + "entcert:[") + "'" + dr["T576_IDCRITERIO"].ToString() + "'");
                    //sbEntorno.Append(((sbEntorno.Length > 0) ? "," : "," + Environment.NewLine + "entorno:[") + "'" + dr["T036_IDCODENTORNO"].ToString() + "'");
                    sbEntCert.Append(((sbEntCert.Length > 0) ? "," : "," + Environment.NewLine + "entcert:[") + "'" + sEntidadCert + "'");
                    sbEntorno.Append(((sbEntorno.Length > 0) ? "," : "," + Environment.NewLine + "entorno:[") + "'" + sEntornoTec + "'");
                }
                if (sbAbrev.Length > 0) sbAbrev.Append("]");
                if (sbValido.Length > 0) sbValido.Append("]");
                if (sbEntCert.Length > 0) sbEntCert.Append("]");
                if (sbEntorno.Length > 0) sbEntorno.Append("]");
                sbOtrosParamentros.Append(sbAbrev.ToString() + sbValido.ToString() + sbEntCert.ToString() + sbEntorno.ToString());
                break;
            
            case "examen":

                string[] aArgs=System.Text.RegularExpressions.Regex.Split(context.Request.QueryString["datos"].ToString(),"@#@");

                int? eC1 = (aArgs[0] == "") ? null : (int?)int.Parse(aArgs[0]);
                int? eT = (aArgs[1] == "") ? null : (int?)int.Parse(aArgs[1]);
                int? idCert = (aArgs[2] == "") ? null : (int?)int.Parse(aArgs[2]);
                bool? valE = (context.Request.QueryString["valido"] == "") ? null : (bool?)Convert.ToBoolean(context.Request.QueryString["valido"].ToString());
                int idFicepiE = int.Parse(context.Request.QueryString["idFicepi"].ToString());
                dr = Examen.obtenerExamenEntidadCertEntorno(eC1, eT, sqlParam, idFicepiE, valE, idCert);
                while (dr.Read())
                {
                    sbID.Append(((sbID.Length > 0) ? "," : "") + "'" + dr["T583_IDEXAMEN"].ToString() + "'");
                    sbNombre.Append(((sbNombre.Length > 0) ? "," : "") + "\"" + dr["T583_NOMBRE"].ToString().Replace("\"", "'") + "\"");
                }
                break;
            case "titulaciones":

                string t001_idficepi = context.Request.QueryString["t001_idficepi"];
                dr = Titulacion.CatalogoByNombre(null,int.Parse(t001_idficepi), sqlParam);
                while (dr.Read())
                {
                    sbID.Append(((sbID.Length > 0) ? "," : "") + "'" + dr["t019_idcodtitulo"].ToString() + "'");
                    //sbID.Append(((sbID.Length > 0) ? "," : "") + "'" + dr["Codigos"].ToString() + "'");
                    sbNombre.Append(((sbNombre.Length > 0) ? "," : "") + "\"" + dr["t019_descripcion"].ToString().Replace("\"", "'") + "\"");
                    //sbNombre.Append(((sbNombre.Length > 0) ? "," : "") + "'" + dr["t019_descripcion"].ToString() + "'");
                    //sbTipo.Append(((sbTipo.Length > 0) ? "," : "") + "'" + dr["t019_tipo"].ToString() + "'");
                    //sbModalidad.Append(((sbModalidad.Length > 0) ? "," : "") + "'" + dr["t019_modalidad"].ToString() + "'");
                    //sbTic.Append(((sbTic.Length > 0) ? "," : "") + "'" + (((bool)dr["t019_tic"])? "1":"0") + "'");
                }
                break;

            case "consultaTitulaciones":

                dr = Titulacion.consultaTitulaciones(null, sqlParam);
                while (dr.Read())
                {
                    sbID.Append(((sbID.Length > 0) ? "," : "") + "'" + dr["t019_idcodtitulo"].ToString() + "'");
                    sbNombre.Append(((sbNombre.Length > 0) ? "," : "") + "\"" + dr["t019_descripcion"].ToString().Replace("\"", "'") + "\"");
                }
                break;

            case "cuentas":

                dr = Curriculum.consultaCuenta(sqlParam);
                while (dr.Read())
                {
                    sbID.Append(((sbID.Length > 0) ? "," : "") + "'" + dr["codigo"].ToString() + "'");
                    sbNombre.Append(((sbNombre.Length > 0) ? "," : "") + "\"" + dr["denominacion"].ToString().Replace("\"", "'") + "\"");
                }
                break;

            case "certificados":

                dr = Curriculum.consultaCertificado(sqlParam);
                while (dr.Read())
                {
                    sbID.Append(((sbID.Length > 0) ? "," : "") + "'" + dr["codigo"].ToString() + "'");
                    sbNombre.Append(((sbNombre.Length > 0) ? "," : "") + "\"" + dr["denominacion"].ToString().Replace("\"", "'") + "\"");
                }
                break;

            case "tituloEspecialidad":

                string paramIdCodTitulo = context.Request.QueryString["titulo"];
                if (paramIdCodTitulo != "")
                {
                    dr = TituloFicepi.CatalogoEspecialidad(sqlParam, short.Parse(paramIdCodTitulo));
                    while (dr.Read())
                    {
                        sbID.Append(((sbID.Length > 0) ? "," : "") + "''");
                        sbNombre.Append(((sbNombre.Length > 0) ? "," : "") + "\"" + dr["t012_especialidad"].ToString().Replace("\"", "'") + "\"");
                    }
                }
                break;
            case "tituloIdioma":
                string paramIdCodIdio = context.Request.QueryString["idcodidioma"];
                if (paramIdCodIdio != "")
                {
                    dr = TituloFicepi.CatalogoTituloIdiomas(sqlParam, short.Parse(paramIdCodIdio));
                    while (dr.Read())
                    {
                        sbID.Append(((sbID.Length > 0) ? "," : "") + "''");
                        sbNombre.Append(((sbNombre.Length > 0) ? "," : "") + "\"" + dr["t021_titulo"].ToString().Replace("\"", "'") + "\"");
                    }
                }
                break;
            case "tituloCentro":

                //string paramIdCodTit = context.Request.QueryString["titulo"];
                //if (paramIdCodTit != "")
                //{
                    //, short.Parse(paramIdCodTit)
                    dr = TituloFicepi.CatalogoCentro(sqlParam);
                    while (dr.Read())
                    {
                        sbID.Append(((sbID.Length > 0) ? "," : "") + "''");
                        sbNombre.Append(((sbNombre.Length > 0) ? "," : "") + "\"" + dr["t012_centro"].ToString().Replace("\"", "'") + "\"");
                    }
                //}
                break;
            case "tituloIdiomaCentro":
                dr = TituloIdiomaFic.CatalogoCentro(sqlParam);
                while (dr.Read())
                {
                    sbID.Append(((sbID.Length > 0) ? "," : "") + "''");
                    sbNombre.Append(((sbNombre.Length > 0) ? "," : "") + "\"" + dr["T021_CENTRO"].ToString().Replace("\"", "'") + "\"");
                }
                break;
            case "cuentaCVT":
                StringBuilder sbSegmento = new StringBuilder();
                StringBuilder sbSector = new StringBuilder();
                //Quito el parámetro origen porque no veo que se use en ningún sitio
                //int? origen = (context.Request.QueryString["origen"] == "") ? null : (int?)int.Parse(context.Request.QueryString["origen"].ToString());
                //dr = SUPER.BLL.CuentasCVT.CatalogoCuentaCVT(sqlParam, origen);
                dr = SUPER.BLL.CuentasCVT.CatalogoCuentaCVT(sqlParam);
                while (dr.Read())
                {
                    sbID.Append(((sbID.Length > 0) ? "," : "") + "'" + dr["T811_IDCUENTA"].ToString() + "'");
                    sbNombre.Append(((sbNombre.Length > 0) ? "," : "") + "\"" + dr["T811_DENOMINACION"].ToString().Replace("\"", "'") + "\"");
                    sbSegmento.Append(((sbSegmento.Length > 0) ? "," : "," + Environment.NewLine + "segmento:[") + "'" + dr["T484_IDSEGMENTO"].ToString() + "'");
                    sbSector.Append(((sbSector.Length > 0) ? "," : "," + Environment.NewLine + "sector:[") + "'" + dr["T483_IDSECTOR"].ToString() + "'");
                }
                if (sbSegmento.Length > 0) sbSegmento.Append("]");
                if (sbSector.Length > 0) sbSector.Append("]");
                sbOtrosParamentros.Append(sbSegmento.ToString() + sbSector.ToString());

                break;   

             case "responsables":

                dr = Calendario.getResponsablesCalendario(sqlParam);
                     while (dr.Read())
                {
                    sbID.Append(((sbID.Length > 0) ? "," : "") + "'" + dr["t001_idficepi_responsable"].ToString() + "'");
                    sbNombre.Append(((sbNombre.Length > 0) ? "," : "") + "\"" + dr["nombreResponsable"].ToString().Replace("\"", "'") + "\"");
                }
               
                break;         
        }
        if (dr != null){
            dr.Close();
            dr.Dispose();
        }
        context.Response.Write("{query:\"" + sqlParam + "\"," + Environment.NewLine +
                                "suggestions:[" + sbNombre.ToString() + "]," + Environment.NewLine +
                                "data:[" + sbID.ToString() + "]" + sbOtrosParamentros.ToString() + "}");
    }
     public bool IsReusable {
        get {
            return false;
        }
    }
}