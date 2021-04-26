using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;

using System.Text.RegularExpressions;
using SUPER.BLL;
using SUPER.Capa_Negocio;
using SUPER.DAL;
using System.Data;

namespace SUPER.BLL
{
    public class Curriculum
    {
        public Curriculum()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        #region Propiedades y Atributos

        private int _T001_IDFICEPI;
        public int T001_IDFICEPI
        {
            get { return _T001_IDFICEPI; }
            set { _T001_IDFICEPI = value; }
        }
        
        private string _NOMBREPROFESIONAL;
        public string NOMBREPROFESIONAL
        {
            get { return _NOMBREPROFESIONAL; }
            set { _NOMBREPROFESIONAL = value; }
        }

        private string _T001_CIP;
        public string T001_CIP
        {
            get { return _T001_CIP; }
            set { _T001_CIP = value; }
        }

        private string _T001_FECNACIM;
        public string T001_FECNACIM
        {
            get { return _T001_FECNACIM; }
            set { _T001_FECNACIM = value; }
        }

        private string _T001_NACIONALIDAD;
        public string T001_NACIONALIDAD
        {
            get { return _T001_NACIONALIDAD; }
            set { _T001_NACIONALIDAD = value; }
        }


        private string _SEXO;
        public string SEXO
        {
            get { return _SEXO; }
            set { _SEXO = value; }
        }

        private byte[] _T001_FOTO;
        public byte[] T001_FOTO
        {
            get { return _T001_FOTO; }
            set { _T001_FOTO = value; }
        }

        private string _EMPRESA;
        public string EMPRESA
        {
            get { return _EMPRESA; }
            set { _EMPRESA = value; }
        }
        private string _T035_DESCRIPCION;
        public string T035_DESCRIPCION
        {
            get { return _T035_DESCRIPCION; }
            set { _T035_DESCRIPCION = value; }
        }
        private string _T392_DENOMINACION;
        public string T392_DENOMINACION
        {
            get { return _T392_DENOMINACION; }
            set { _T392_DENOMINACION = value; }
        }
        
        private string _T303_DENOMINACION;
        public string T303_DENOMINACION
        {
            get { return _T303_DENOMINACION; }
            set { _T303_DENOMINACION = value; }
        }

        private string _OFICINA;
        public string OFICINA
        {
            get { return _OFICINA; }
            set { _OFICINA = value; }
        }

        private string _ROL;
        public string ROL
        {
            get { return _ROL; }
            set { _ROL = value; }
        }

        private string _T009_DESCENTRAB;
        public string T009_DESCENTRAB
        {
            get { return _T009_DESCENTRAB; }
            set { _T009_DESCENTRAB = value; }
        }

        private string _T001_FECANTIGU;
        public string T001_FECANTIGU
        {
            get { return _T001_FECANTIGU; }
            set { _T001_FECANTIGU = value; }
        }


        private bool? _internacional;
        public bool? internacional
        {
            get { return _internacional; }
            set { _internacional = value; }
        }

        private int? _movilidad;
        public int? movilidad
        {
            get { return _movilidad; }
            set { _movilidad = value; }
        }

        private string _observaciones;
        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }

        private DateTime? _T165_FALTACV;
        public DateTime? T165_FALTACV
        {
            get { return _T165_FALTACV; }
            set { _T165_FALTACV = value; }
        }

        private int _CVFINALIZADO;
        public int CVFINALIZADO
        {
            get { return _CVFINALIZADO; }
            set { _CVFINALIZADO = value; }
        }
        private int _PROFESIONAL_CVEXCLUSION;
        public int PROFESIONAL_CVEXCLUSION
        {
            get { return _PROFESIONAL_CVEXCLUSION; }
            set { _PROFESIONAL_CVEXCLUSION = value; }
        }

        private int _RESPONSABLE_CVEXCLUSION;
        public int RESPONSABLE_CVEXCLUSION
        {
            get { return _RESPONSABLE_CVEXCLUSION; }
            set { _RESPONSABLE_CVEXCLUSION = value; }
        }

        private string _Pais;
        public string Pais
        {
            get { return _Pais; }
            set { _Pais = value; }
        }

        private int? _t173_idprovincia;
        public int? t173_idprovincia
        {
            get { return _t173_idprovincia; }
            set { _t173_idprovincia = value; }
        }

        private string _t173_denominacion;
        public string t173_denominacion
        {
            get { return _t173_denominacion; }
            set { _t173_denominacion = value; }
        }

        private DateTime? _t001_cvcompletado_prof;
        public DateTime? t001_cvcompletado_prof
        {
            get { return _t001_cvcompletado_prof; }
            set { _t001_cvcompletado_prof = value; }
        }
        private DateTime? _t001_cv_fechaactu;
        public DateTime? t001_cv_fechaactu
        {
            get { return _t001_cv_fechaactu; }
            set { _t001_cv_fechaactu = value; }
        }

        #endregion

        public static Curriculum MiCV(int idFicepi)
        {
            Curriculum o = new Curriculum();
            SqlDataReader dr = DAL.Curriculum.MiCVPersonalesOrganizativos(null, idFicepi);

            if (dr.Read())
            {
                if (dr["NOMBRECOMEVALUADO"] != DBNull.Value)
                    o.NOMBREPROFESIONAL = (string)dr["NOMBRECOMEVALUADO"];
                if (dr["T001_CIP"] != DBNull.Value)
                    o.T001_CIP = dr["T001_CIP"].ToString();
                if (dr["T001_NACIONALID"] != DBNull.Value)
                    o.T001_NACIONALIDAD = (string)dr["T001_NACIONALID"];
                if (dr["SEXO"] != DBNull.Value)
                    o.SEXO = (string)dr["SEXO"];
                if (dr["T001_FOTO"] != DBNull.Value)
                    o.T001_FOTO = (byte[])dr["T001_FOTO"];
                if (dr["T001_FECNACIM"] != DBNull.Value)
                    o.T001_FECNACIM = ((DateTime)dr["T001_FECNACIM"]).ToShortDateString();
                if (dr["EMPRESA"] != DBNull.Value)
                    o.EMPRESA = (string)dr["EMPRESA"];
                if (dr["T392_DENOMINACION"] != DBNull.Value)
                    o.T392_DENOMINACION = dr["T392_DENOMINACION"].ToString();
                if (dr["T303_DENOMINACION"] != DBNull.Value)
                    o.T303_DENOMINACION = (string)dr["T303_DENOMINACION"];
                if (dr["T010_DESOFICINA"] != DBNull.Value)
                    o.OFICINA = (string)dr["T010_DESOFICINA"];
                if (dr["ROL"] != DBNull.Value)
                    o.ROL = (string)dr["ROL"];
                if (dr["T035_DESCRIPCION"] != DBNull.Value)
                    o.T035_DESCRIPCION = dr["T035_DESCRIPCION"].ToString();
                if (dr["T001_FECANTIGU"] != DBNull.Value)
                    o.T001_FECANTIGU = ((DateTime)dr["T001_FECANTIGU"]).ToShortDateString();
                if (dr["T009_DESCENTRAB"] != DBNull.Value)
                    o.T009_DESCENTRAB = (string)dr["T009_DESCENTRAB"];
                if (dr["T001_CVINTERNACIONAL"] != DBNull.Value)
                    o.internacional = (bool)(dr["T001_CVINTERNACIONAL"]);
                if (dr["T001_CVMOVILIDAD"] != DBNull.Value)
                    o.movilidad = Int32.Parse(dr["T001_CVMOVILIDAD"].ToString());
                if (dr["T001_CVOBSERVA"] != DBNull.Value)
                    o.observaciones = (string)dr["T001_CVOBSERVA"];
                if (dr["CVFINALIZADO"] != DBNull.Value)
                    o.CVFINALIZADO = int.Parse(dr["CVFINALIZADO"].ToString());
                if (dr["PROFESIONAL_CVEXCLUSION"] != DBNull.Value)
                    o.PROFESIONAL_CVEXCLUSION = int.Parse(dr["PROFESIONAL_CVEXCLUSION"].ToString());
                if (dr["RESPONSABLE_CVEXCLUSION"] != DBNull.Value)
                    o.RESPONSABLE_CVEXCLUSION = int.Parse(dr["RESPONSABLE_CVEXCLUSION"].ToString());
                if (dr["PAIS"] != DBNull.Value)
                    o.Pais = (string)dr["PAIS"];
                if (dr["t173_idprovincia"] != DBNull.Value)
                    o.t173_idprovincia = int.Parse(dr["t173_idprovincia"].ToString());
                if (dr["t173_denominacion"] != DBNull.Value)
                    o.t173_denominacion = (string)dr["t173_denominacion"];
                if (dr["t001_cvcompletado_prof"] != DBNull.Value)
                    o.t001_cvcompletado_prof = (DateTime)dr["t001_cvcompletado_prof"];
                if (dr["t001_cv_fechaactu"] != DBNull.Value)
                    o.t001_cv_fechaactu = (DateTime)dr["t001_cv_fechaactu"];                                
            }
            else
            {

                throw (new NullReferenceException("No se ha obtenido ningún dato del Profesional"));
            }
            dr.Close();
            dr.Dispose();
            return o;
        }

        #region Combos
        public static List<ElementoLista> obtenerCboMovilidad()
        {
            SqlDataReader dr = DAL.Curriculum.obtenerMovilidad();
            List<ElementoLista> oLista = new List<ElementoLista>();
            string[] strTipo = null;
            if (dr.Read())
            {
                strTipo = Regex.Split(dr["TIPO"].ToString(), "@#@");
            }
            foreach (string oFun in strTipo)
            {
                string[] aValores = Regex.Split(oFun, "#/#");
                oLista.Add(new ElementoLista(aValores[0].ToString(), aValores[1].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }
        #endregion
        public static void Grabar(SqlTransaction tr,string sOpcion, int T001_IDFICEPI, Nullable<bool> T001_CVINTERNACIONAL, Nullable<int> T001_CVMOVILIDAD, string T001_CVOBSERVA)
        {              
            switch (sOpcion)
            {                       
                case "U":
                    DAL.Curriculum.UpdateDatosOrg(tr,T001_IDFICEPI,T001_CVINTERNACIONAL,T001_CVMOVILIDAD,T001_CVOBSERVA);
                    break;                                                                        
            }
        }
        public static string MiCVTareasPendientes(int iPantalla, int idFicepi, Nullable<int> Id, Nullable<int> Id2)
        {
            //05/08/2015 Mikel. De momento las tareas-plazo no van a estar activas por lo que comento el código
            //SqlDataReader dr = DAL.Curriculum.MiCVTareasPendientes(null, iPantalla, idFicepi, Id, Id2);
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            //while (dr.Read())
            //{
            //    sb.Append(dr["Seccion"].ToString() + "#dato#" + dr["SubSeccion"].ToString() + "#dato#" + dr["Caso"].ToString() + "#dato#" + dr["Flimite"].ToString() + "#dato#" +  Utilidades.escape(dr["Motivo"].ToString()) + "@##@"); // dr["Motivo"].ToString() 
            //}
            //dr.Close();
            //dr.Dispose();
            return sb.ToString();
        }
        public static string MiCVPendientes(int idFicepi)
        {
            SqlDataReader dr = DAL.Curriculum.MiCVPendientes(null, idFicepi);
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            string Estado = "";
            while (dr.Read())
            {
                switch (dr["IESTADO"].ToString())
                {
                    case ("1"):
                        Estado = "T";
                        break;
                    case ("0"):
                        Estado = "P";
                        break;
                    default://No tiene datos de la subSeccion o estan validados
                        Estado = "V";
                        break;
                }
                sb.Append(dr["Seccion"].ToString() + "#dato#" + dr["SubSeccion"].ToString() + "#dato#" + Estado + "@##@");
            }
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        public static string MiCVPersonalHtml(int idFicepi)
        {
            SqlDataReader dr = DAL.Curriculum.MiCVPersonalesOrganizativos(null, idFicepi);
            StringBuilder sb = new StringBuilder();
            sb.Append("");

            if (dr.Read())
            {
                if (dr["T001_FOTO"] != DBNull.Value)
                    System.Web.HttpContext.Current.Session["FOTOUSUARIO"] = dr["T001_FOTO"];
                else
                    System.Web.HttpContext.Current.Session["FOTOUSUARIO"] = null;

                sb.Append("<table class='tituloPrincipal' cellpadding='0' cellspacing='0' border='0'>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblDP' class='titulo1'>Datos Personales</label>");
                sb.Append("</td></tr>");
                sb.Append("</table>");
                sb.Append("<table style='margin-left:60px; margin-top:25px; width:590px;' cellpadding='1' cellspacing='0' border='0'>");
                sb.Append("<colgroup>");
                sb.Append(" <col style='width:95px;'/>");
                sb.Append(" <col style='width:495px;'/>");
                sb.Append("</colgroup>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblNombre' class='label' style='color:#336699;'>Nombre:</label>");
                sb.Append("</td><td>");
                sb.Append("<nobr id='nombre' class='NBR W450 label' onmouseover='TTip(event)'>" + dr["NOMBRECOMEVALUADO"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblNIF' class='label' style='color:#336699;'>NIF:</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='idNIF' class='label'>" + dr["T001_CIP"].ToString() + "</label>");
                sb.Append("</td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblFNacimiento' class='label' style='color:#336699;'>Fecha Nacimiento:</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='idNacimiento' class='label'>" + ((dr["T001_FECNACIM"]!=DBNull.Value)? ((DateTime)dr["T001_FECNACIM"]).ToShortDateString():"") + "</label>");
                sb.Append("</td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblNacionalidad' class='label' style='color:#336699;'>Nacionalidad:</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='idNacionalidad' class='label'>" + dr["T001_NACIONALID"].ToString() + "</label>");
                sb.Append("</td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblSexo' class='label' style='color:#336699;'>Sexo:</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='idSexo' class='label'>" + dr["SEXO"].ToString() + "</label>");
                sb.Append("</td></tr>");
                sb.Append("</table>");
                
                sb.Append("#@@#");

                sb.Append("<table id='tblDatosOrganizativos' class='tituloPrincipal' cellpadding='0' cellspacing='0' border='0'>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblDO' class='titulo1'>Datos Organizativos</label>");
                sb.Append("</td></tr>");
                sb.Append("</table>");
                sb.Append("<table style='margin-left:60px; margin-top:25px; width:590px;' cellpadding='1' cellspacing='0' border='0'>");
                sb.Append("<colgroup>");
                sb.Append(" <col style='width:170px;'/>");
                sb.Append(" <col style='width:420px;'/>");
                sb.Append("</colgroup>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblEmpresa' class='label' style='color:#336699;'>Empresa:</label>");
                sb.Append("</td><td>");
                sb.Append("<nobr id='Empresa' class='NBR W250 label' onmouseover='TTip(event)'>" + dr["EMPRESA"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblUnidadNegocio' class='label' style='color:#336699;'>Unidad de negocio:</label>");
                sb.Append("</td><td>");
                sb.Append("<nobr id='UnidadNegocio' class='NBR W250 label' onmouseover='TTip(event)'>" + dr["T392_DENOMINACION"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblCR' class='label' style='color:#336699;'>Centro de responsabilidad:</label>");
                sb.Append("</td><td>");
                sb.Append("<nobr id='CR' class='NBR W250 label' onmouseover='TTip(event)'>" + dr["T303_DENOMINACION"].ToString() + "</nobr>");
                sb.Append("</td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblFAntiguo' class='label' style='color:#336699;'>Fecha Antiguedad:</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='FAntiguo' class='label'>" + ((dr["T001_FECANTIGU"] != DBNull.Value) ? ((DateTime)dr["T001_FECANTIGU"]).ToShortDateString() : "") + "</label>");
                sb.Append("</td></tr>");
                //20/11/2015 Mikel. PPOO nos pide que no salga el rol ni el perfil de mercado
                //sb.Append("<tr><td>");
                //sb.Append("<label id='lblRolI' class='label' style='color:#336699;'>Rol interno:</label>");
                //sb.Append("</td><td>");
                //sb.Append("<nobr id='RolI' class='NBR W250 label' onmouseover='TTip(event)'>" + dr["ROL"].ToString() + "</nobr>");
                //sb.Append("</td></tr>");

                //sb.Append("<tr><td>");
                //sb.Append("<label id='lblPerfil' class='label' style='color:#336699;'>Perfil de mercado:</label>");
                //sb.Append("</td><td>");
                //sb.Append("<nobr id='Perfil' class='NBR W250 label' onmouseover='TTip(event)'>" + dr["T035_DESCRIPCION"].ToString() + "</nobr>");
                //sb.Append("</td></tr>");

                sb.Append("<tr><td>");
                sb.Append("<label id='lblOficina' class='label' style='color:#336699;'>Oficina:</label>");
                sb.Append("</td><td>");
                sb.Append("<nobr id='Oficina' class='NBR W250 label' onmouseover='TTip(event)'>" + dr["T010_DESOFICINA"].ToString() + "</nobr>");
                sb.Append("</td></tr>");

                sb.Append("<tr><td>");
                sb.Append("<label id='lblProvincia' class='label' style='color:#336699;'>Provincia:</label>");
                sb.Append("</td><td>");
                sb.Append("<nobr id='Provincia' class='NBR W250 label' onmouseover='TTip(event)'>" + dr["T009_DESCENTRAB"].ToString() + "</nobr>");
                sb.Append("</td></tr>");

                sb.Append("<tr><td>");
                sb.Append("<label id='lblPais' class='label' style='color:#336699;'>Pais:</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='Pais' class='label'>" + dr["PAIS"].ToString() + "</label>");
                sb.Append("</td></tr>");

                string sTInter = "";
                if (dr["T001_CVINTERNACIONAL"] != DBNull.Value)
                {
                    if ((bool)dr["T001_CVINTERNACIONAL"] == false)
                        sTInter = "No";
                    else
                        sTInter = "Si";
                }
                else
                    sTInter = "";

                sb.Append("<tr><td>");
                sb.Append("<label id='lblTInter' class='label' style='color:#336699;'>Interesado en Tray. internacional:</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='TInter' class='label'>" + sTInter + "</label>");
                sb.Append("</td></tr>");

                string sMGeo = "";
                if (dr["T001_CVMOVILIDAD"].ToString() == "1")
                    sMGeo = "Alto";
                else if (dr["T001_CVMOVILIDAD"].ToString() == "2")
                    sMGeo = "Medio";
                else if (dr["T001_CVMOVILIDAD"].ToString() == "3")
                    sMGeo = "Bajo";

                sb.Append("<tr><td>");
                sb.Append("<label id='lblMGeo' class='label' style='color:#336699;'>Movilidad Geográfica:</label>");
                sb.Append("</td><td>");
                sb.Append("<label id='MGeo' class='label'>" + sMGeo + "</label>");
                sb.Append("</td></tr>");

                sb.Append("<tr><td>");
                sb.Append("<label id='lblObserv' class='label' style='color:#336699;'>Observaciones:</label>");
                sb.Append("</td></tr><tr><td></td><td>");
                sb.Append("<asp:TextBox type='text' ID='Observa' SkinID='multi' TextMode='MultiLine' style='overflow-y:auto; overflow-x:hidden; width:585px; resize:none;' Rows='4' ReadOnly='true' BackColor='Transparent' BorderColor='Transparent'>" + dr["T001_CVOBSERVA"].ToString().Replace("<","&#60;").Replace("\r\n", "<br>") + "</asp:TextBox>");
                sb.Append("</td></tr>");
              
                sb.Append("</table>");

                sb.Append("#@@#");

                sb.Append("<table class='tituloPrincipal' cellpadding='0' cellspacing='0' border='0'>");
                sb.Append("<tr><td>");
                sb.Append("<label id='lblSinop' class='titulo1'>Sinopsis</label>");
                sb.Append("</td></tr>");
                sb.Append("</table>");
                sb.Append("<table style='margin-left:60px; margin-top:25px; width:590px;' cellpadding='1' cellspacing='0' border='0'>");
                sb.Append("<tr><td><asp:TextBox type='text' ID='sinopsis' SkinID='multi' TextMode='MultiLine' style='overflow-y:auto; overflow-x:hidden; width:585px; resize:none;' Rows='4' ReadOnly='true' BackColor='Transparent' BorderColor='Transparent'>" + dr["T185_SINOPSIS"].ToString().Replace("\r\n", "<br>") + "</asp:TextBox></td></tr></table>");
                
                sb.Append("#@@#");

                dr = DOCUIDFICEPI.Catalogo(idFicepi);
                if (dr.HasRows)
                {
                    sb.Append("<table id='tblArchivosAsociados' class='tituloPrincipal' cellpadding='0' cellspacing='0' border='0'>");
                    sb.Append("<tr><td>");
                    sb.Append("<label id='lblDAP' class='titulo1'>Archivos asociados</label>");
                    sb.Append("</td></tr>");
                    sb.Append("</table>");

                    sb.Append("<table style='margin-left:60px; margin-top:25px; width:590px;' cellpadding='1' cellspacing='0' border='0'>");
                    sb.Append("<colgroup>");
                    sb.Append("    <col style='width:590px;' />");
                    sb.Append("</colgroup>");

                    while (dr.Read())
                    {
                        sb.Append("<tr style='height:20px;' id='" + dr["t184_iddocucv"].ToString() + "' onmouseover='TTip(event)'>");

                        // Celda descripción

                        string sNomArchivo = dr["t184_descripcion"].ToString();// +Utilidades.TamanoArchivo((int)dr["bytes"]);

                        sb.Append(@"<td align='left'><img src='../../../images/imgArchivo.png' class='MANO' 
                                            onclick='descargarDoc(this.parentNode.parentNode.id);' 
                                            style='vertical-align:bottom; width:16px; height:16px;' title='Descargar " + sNomArchivo + "'>");

                        sb.Append("&nbsp;<nobr class='NBR' style='width:400px'>" + sNomArchivo + "</nobr></td>");

                    }
                    sb.Append("</table>");
                }
            }
            else
            {

                throw (new NullReferenceException("No se ha obtenido ningún dato del Profesional"));
            }
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        public static string cargarEstructuraConsulta()
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            DataSet ds = Estructura.GetEstructuraOrganizativa_DS(false, false);
            DataView aux = new DataView(ds.Tables[0]);
            aux.Sort = "DENOMINACION";
            foreach (DataRow oFila in aux.ToTable().Rows)
            {
                sb.Append("\tjs_estructura[" + i + "] = {\"nivel\":" + oFila["INDENTACION"].ToString() + ",\"sn4\":" + oFila["SN4"].ToString() + ",\"sn3\":" + oFila["SN3"].ToString() + ",\"sn2\":" + oFila["SN2"].ToString() + ",\"sn1\":" + oFila["SN1"].ToString() + ",\"nodo\":" + oFila["NODO"].ToString() + ",\"denominacion\":\"" + Utilidades.escape(oFila["denominacion"].ToString().Substring(0, oFila["denominacion"].ToString().LastIndexOf("(")).Replace((char)34, (char)39)) + "\"};\n");
                i++;
            }
            ds.Dispose();
            return sb.ToString();
        }

        public static string ObtenerTipoConcepto(int nTipo, string sTipoBusqueda, string sCadena)
        {
            string sResul = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader dr = null;

            dr = DAL.Curriculum.ObtenerCriterio(nTipo, sCadena, sTipoBusqueda.ToCharArray()[0]);

            sb.Append("<table id='tblDatos' class='texto MAM' style='WIDTH: 350px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:350px;' /></colgroup>" + (char)10);
            sb.Append("<tbody>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["codigo"].ToString() + "' title='" + dr["denominacion"].ToString() + "' ");
                sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)' style='height:20px;'>");
                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W320'>" + dr["denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString();
            
            return sResul;
        }
        /// <summary>
        /// Es igual que ObtenerTipoConcepto, solo que la altura de las filas es de 16px
        /// </summary>
        /// <param name="nTipo"></param>
        /// <param name="sTipoBusqueda"></param>
        /// <param name="sCadena"></param>
        /// <returns></returns>
        public static string ObtenerTipoConcepto16(int nTipo, string sTipoBusqueda, string sCadena)
        {
            string sResul = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader dr = null;

            dr = DAL.Curriculum.ObtenerCriterio(nTipo, sCadena, sTipoBusqueda.ToCharArray()[0]);

            sb.Append("<table id='tblDatos' class='texto MAM' style='WIDTH: 350px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:350px;' /></colgroup>" + (char)10);
            sb.Append("<tbody>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["codigo"].ToString() + "' title='" + dr["denominacion"].ToString() + "' ");
                if (nTipo == 3) 
                    sb.Append(" tipo='P' ");//Perfil
                else
                {
                    if (nTipo == 5) 
                        sb.Append(" tipo='E' ");//Entorno
                }
                sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)' style='height:16px;'>");
                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W320'>" + dr["denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString();

            return sResul;
        }

        public static DataTable getProfesionalesConsultaAvanzada(
                int idFicepi,
            //Datos personales - Organizativos
                string tipo,
                string estado,
                int? CR,
                int? SN1,
                int? SN2,
                int? SN3,
                int? SN4,
                short? centro,
                short? movilidad,
                bool? inttrayint,
                byte? gradodisp,
                decimal? limcoste,
                ArrayList profesionales,
                ArrayList perfiles,
            //Titulación
                byte? tipologia,
                bool? tics,
                byte? modalidad,
                ArrayList titulo_obl_cod,
                ArrayList titulo_obl_den,
                ArrayList titulo_opc_cod,
                ArrayList titulo_opc_den,
            //Idiomas
                ArrayList idioma_obl_cod,
                ArrayList idioma_obl_den,
                ArrayList idioma_opc_cod,
                ArrayList idioma_opc_den,
            //Formación
                int? num_horas,
                int? anno,
            ////Certificados
                ArrayList cert_obl_cod,
                ArrayList cert_obl_den,
                ArrayList cert_opc_cod,
                ArrayList cert_opc_den,
            ////Entidades certificadoras
                ArrayList entcert_obl_cod,
                ArrayList entcert_obl_den,
                ArrayList entcert_opc_cod,
                ArrayList entcert_opc_den,
            ////Entornos tecnológicos
                ArrayList entfor_obl_cod,
                ArrayList entfor_obl_den,
                ArrayList entfor_opc_cod,
                ArrayList entfor_opc_den,
            ////Cursos
                ArrayList curso_obl_cod,
                ArrayList curso_obl_den,
                ArrayList curso_opc_cod,
                ArrayList curso_opc_den,
            ////Experiencias profesionales
            //Cliente / Sector
                string cliente,
                int? sector,
                short? cantidad_expprof,
                byte? unidad_expprof,
                short? anno_expprof,
            //Contenido de Experiencias / Funciones
                ArrayList term_expfun,
                string op_logico,
                short? cantidad_expfun,
                byte? unidad_expfun,
                short? anno_expfun,
            //Experiencia profesional Perfil
                //string op_logico_perfil,
                ArrayList tbl_bus_perfil,
            //Experiencia profesional Perfil / Entorno tecnológico
                ArrayList tbl_bus_perfil_entorno,
            //Experiencia profesional Entorno tecnológico
                //string op_logico_entorno,
                ArrayList tbl_bus_entorno,
            //Experiencia profesional Entorno tecnológico / Perfil
                ArrayList tbl_bus_entorno_perfil
            )
        {
            SqlDataReader dr = SUPER.DAL.Curriculum.getProfesionalesConsultaAvanzada(null,
                idFicepi,
                //Datos personales - Organizativos
                tipo, estado, CR, SN1, SN2, SN3, SN4, centro, movilidad, inttrayint, gradodisp, limcoste, profesionales, perfiles,
                //Titulación
                tipologia, tics, modalidad, titulo_obl_cod, titulo_obl_den, titulo_opc_cod, titulo_opc_den,
                //Idiomas
                idioma_obl_cod, idioma_obl_den, idioma_opc_cod, idioma_opc_den,
                //Formación
                num_horas, anno,
                ////Certificados
                cert_obl_cod, cert_obl_den, cert_opc_cod, cert_opc_den,
                ////Entidades certificadoras
                entcert_obl_cod, entcert_obl_den, entcert_opc_cod, entcert_opc_den,
                ////Entornos tecnológicos
                entfor_obl_cod, entfor_obl_den, entfor_opc_cod, entfor_opc_den,
                ////Cursos
                curso_obl_cod, curso_obl_den, curso_opc_cod, curso_opc_den,
                ////Experiencias profesionales
                //Cliente / Sector
                cliente, sector, cantidad_expprof, unidad_expprof, anno_expprof,
                //Contenido de Experiencias / Funciones
                term_expfun, op_logico, cantidad_expfun, unidad_expfun, anno_expfun,
                //Experiencia profesional Perfil
                //op_logico_perfil,
                tbl_bus_perfil,
                //Experiencia profesional Perfil / Entorno tecnológico
                tbl_bus_perfil_entorno,
                //Experiencia profesional Entorno tecnológico
                //op_logico_entorno,
                tbl_bus_entorno,
                //Experiencia profesional Entorno tecnológico / Perfil
                tbl_bus_entorno_perfil);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("t001_idficepi", typeof(int)));
            dt.Columns.Add(new DataColumn("Profesional", typeof(string)));
            dt.Columns.Add(new DataColumn("baja", typeof(bool)));
            dt.Columns.Add(new DataColumn("t001_tiporecurso", typeof(string)));
            dt.Columns.Add(new DataColumn("t001_sexo", typeof(string)));
            dt.Columns.Add(new DataColumn("t303_denominacion", typeof(string)));
            dt.Columns.Add(new DataColumn("Perfil", typeof(string)));
            dt.Columns.Add(new DataColumn("Disponible", typeof(byte)));
            dt.Columns.Add(new DataColumn("provincia", typeof(string)));
            dt.Columns.Add(new DataColumn("pais", typeof(string)));
            dt.Columns.Add(new DataColumn("Pendiente", typeof(bool)));

            while (dr.Read())
            {
                DataRow row = dt.NewRow();
                row["t001_idficepi"] = dr["t001_idficepi"];
                row["Profesional"] = dr["Profesional"];
                row["baja"] = dr["baja"];
                row["t001_tiporecurso"] = dr["t001_tiporecurso"];
                row["t001_sexo"] = dr["t001_sexo"];
                row["t303_denominacion"] = dr["t303_denominacion"];
                row["Perfil"] = dr["Perfil"];
                row["Disponible"] = dr["Disponible"];
                row["provincia"] = dr["provincia"];
                row["pais"] = dr["pais"];
                row["Pendiente"] = dr["Pendiente"];
                dt.Rows.Add(row);
            }
            dr.Close();
            dr.Dispose();

            return dt;
        }
        public static DataTable getProfesionalesConsultaCadena(SqlTransaction tr, int idFicepi,
                        bool bTitulaciones, bool bIdiomas, bool bExperiencia, bool bCursos, bool bOtros, bool bCertExam, bool bCondicion,
                        Nullable<char> tipoProf, Nullable<char> estadoProf,
                        Nullable<int> idNodo, Nullable<int> sn1, Nullable<int> sn2, Nullable<int> sn3, Nullable<int> sn4,
                        Nullable<short> idCentrab, Nullable<short> cvMovilidad, Nullable<bool> cvInternacional,
                        Nullable<int> idCodPerfil, Nullable<byte> disponibilidad, Nullable<decimal> costeJornada,
                        string sCadena)
        {
            SqlDataReader dr = SUPER.DAL.Curriculum.getProfesionalesConsultaCadena(tr, idFicepi,
                        bTitulaciones, bIdiomas, bExperiencia, bCursos, bOtros, bCertExam, bCondicion,
                        tipoProf, estadoProf, idNodo, sn1, sn2, sn3, sn4,
                        idCentrab, cvMovilidad, cvInternacional, idCodPerfil, disponibilidad, costeJornada,
                        sCadena);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("t001_idficepi", typeof(int)));
            dt.Columns.Add(new DataColumn("Profesional", typeof(string)));
            dt.Columns.Add(new DataColumn("baja", typeof(bool)));
            dt.Columns.Add(new DataColumn("t001_tiporecurso", typeof(string)));
            dt.Columns.Add(new DataColumn("t001_sexo", typeof(string)));
            dt.Columns.Add(new DataColumn("t303_denominacion", typeof(string)));
            dt.Columns.Add(new DataColumn("Perfil", typeof(string)));
            dt.Columns.Add(new DataColumn("Disponible", typeof(byte)));
            dt.Columns.Add(new DataColumn("provincia", typeof(string)));
            dt.Columns.Add(new DataColumn("pais", typeof(string)));
            dt.Columns.Add(new DataColumn("Pendiente", typeof(bool)));

            while (dr.Read())
            {
                DataRow row = dt.NewRow();
                row["t001_idficepi"] = dr["t001_idficepi"];
                row["Profesional"] = dr["Profesional"];
                row["baja"] = dr["baja"];
                row["t001_tiporecurso"] = dr["t001_tiporecurso"];
                row["t001_sexo"] = dr["t001_sexo"];
                row["t303_denominacion"] = dr["t303_denominacion"];
                row["Perfil"] = dr["Perfil"];
                row["Disponible"] = dr["Disponible"];
                row["provincia"] = dr["provincia"];
                row["pais"] = dr["pais"];
                row["Pendiente"] = dr["Pendiente"];
                dt.Rows.Add(row);
            }
            dr.Close();
            dr.Dispose();

            return dt;
        }
        public static string ConsultaQuery(string sqlSelect)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<table id='tblDatos' style='width:550px;' cellpadding='0' cellspacing='0' border='0'>
                        <colgroup>
                            <col style='width:30px;' />
                            <col style='width:20px;' />
                            <col style='width:280px;' />
                            <col style='width:200px;' />
                            <col style='width:20px;' />
                        </colgroup>");

            SqlDataReader dr = DAL.Curriculum.ConsultaQuery(null,sqlSelect);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T001_IDFICEPI"].ToString() + "' selected='false' ");
                sb.Append("tipo='" + dr["T001_TIPORECURSO"].ToString() + "' ");
                sb.Append("sexo='" + dr["T001_SEXO"].ToString() + "' ");
                sb.Append("baja='" + dr["BAJA"].ToString() + "' ");

                sb.Append("nodo=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\" ");
                sb.Append("perfil=\"" + Utilidades.escape(dr["Perfil"].ToString()) + "\" ");
                sb.Append("disponible=\"" + dr["disponible"].ToString() + "\" ");
                sb.Append("provincia=\"" + Utilidades.escape(dr["provincia"].ToString()) + "\" ");
                sb.Append("pais=\"" + Utilidades.escape(dr["pais"].ToString()) + "\" ");
                sb.Append("estado=\"" + dr["Estado"].ToString() + "\" ");

                string sTooltip = "<label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString() + "<br><label style=width:70px;>Perfil:</label>" + dr["Perfil"].ToString() + "<br><label style=width:70px;>Disponibilidad:</label>" + dr["disponible"].ToString() + " %" + "<br><label style=width:70px;>Provincia:</label>" + dr["provincia"].ToString() + "<br><label style=width:70px;>País:</label>" + dr["pais"].ToString();
                sb.Append("tooltip=\"" + Utilidades.escape(sTooltip) + "\" ");
                //Datos de alertas 

                sb.Append(">");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td>" + dr["NOMBRE"].ToString() + "</td>");
                //20/11/2015 Mikel PPOO nos pide que no se muestre el perfil
                //sb.Append("<td>" + dr["Perfil"].ToString() + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            return sb.ToString();

        }

        #region Combos
        public static List<ElementoLista> obtenerTipoProfesional()
        {
            SqlDataReader dr = DAL.Curriculum.obtenerTipoProfesional();
            List<ElementoLista> oLista = new List<ElementoLista>();
            string[] strPrioridad = null;
            if (dr.Read())
            {
                strPrioridad = Regex.Split(dr["TIPOPROF"].ToString(), "@#@");
            }
            foreach (string oFun in strPrioridad)
            {
                string[] aValores = Regex.Split(oFun, "#/#");
                oLista.Add(new ElementoLista(aValores[0].ToString(), aValores[1].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }

        public static List<ElementoLista> obtenerMedidaTiempo()
        {
            SqlDataReader dr = DAL.Curriculum.obtenerMedidasTiempo();
            List<ElementoLista> oLista = new List<ElementoLista>();
            string[] strPrioridad = null;
            if (dr.Read())
            {
                strPrioridad = Regex.Split(dr["MEDIDA"].ToString(), "@#@");
            }
            foreach (string oFun in strPrioridad)
            {
                string[] aValores = Regex.Split(oFun, "#/#");
                oLista.Add(new ElementoLista(aValores[0].ToString(), aValores[1].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }

        public static List<ElementoLista> obtenerTipoExperiencia()
        {
            SqlDataReader dr = DAL.Curriculum.obtenerTipoExperiencia();
            List<ElementoLista> oLista = new List<ElementoLista>();
            string[] strPrioridad = null;
            if (dr.Read())
            {
                strPrioridad = Regex.Split(dr["TIPOEXP"].ToString(), "@#@");
            }
            foreach (string oFun in strPrioridad)
            {
                string[] aValores = Regex.Split(oFun, "#/#");
                oLista.Add(new ElementoLista(aValores[0].ToString(), aValores[1].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }

        public static List<ElementoLista> obtenerSituacionLaboral()
        {
            SqlDataReader dr = DAL.Curriculum.obtenerSituacionLaboral();
            List<ElementoLista> oLista = new List<ElementoLista>();
            string[] strPrioridad = null;
            if (dr.Read())
            {
                strPrioridad = Regex.Split(dr["SITLAB"].ToString(), "@#@");
            }
            foreach (string oFun in strPrioridad)
            {
                string[] aValores = Regex.Split(oFun, "#/#");
                oLista.Add(new ElementoLista(aValores[0].ToString(), aValores[1].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }
        public static List<ElementoLista> obtenerCentro(Nullable<bool> cvConsultaExternos, Nullable<bool> cvConsultaBaja)
        {
            SqlDataReader dr = DAL.Curriculum.obtenerCentros(cvConsultaExternos, cvConsultaBaja);
            List<ElementoLista> oLista = new List<ElementoLista>();
            while (dr.Read())
            {
                oLista.Add(new ElementoLista(dr["codigo"].ToString(), dr["denominacion"].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }
        public static List<ElementoLista> obtenerIdioma(Nullable<bool> cvConsultaExternos, Nullable<bool> cvConsultaBaja)
        {
            SqlDataReader dr = DAL.Curriculum.obtenerIdioma(cvConsultaExternos, cvConsultaBaja);
            List<ElementoLista> oLista = new List<ElementoLista>();
            while (dr.Read())
            {
                oLista.Add(new ElementoLista(dr["codigo"].ToString(), dr["denominacion"].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }
        
        public static List<ElementoLista> obtenerPerfil(Nullable<bool> cvConsultaExternos, Nullable<bool> cvConsultaBaja)
        {
            SqlDataReader dr = DAL.Curriculum.obtenerPerfil(cvConsultaExternos, cvConsultaBaja);
            List<ElementoLista> oLista = new List<ElementoLista>();
            while (dr.Read())
            {
                oLista.Add(new ElementoLista(dr["codigo"].ToString(), dr["denominacion"].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }
        
        public static List<ElementoLista> obtenerSector()
        {
            SqlDataReader dr = DAL.Curriculum.obtenerSector();
            List<ElementoLista> oLista = new List<ElementoLista>();
            while (dr.Read())
            {
                oLista.Add(new ElementoLista(dr["codigo"].ToString(), dr["denominacion"].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }
        public static List<ElementoLista> obtenerEntidadesCertificadoras()
        {
            SqlDataReader dr = DAL.Curriculum.obtenerEntidadesCertificadoras();
            List<ElementoLista> oLista = new List<ElementoLista>();
            while (dr.Read())
            {
                oLista.Add(new ElementoLista(dr["codigo"].ToString(), dr["denominacion"].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }

        #endregion
        public static string CvSinCompletar_C(bool bNoCV, bool bExcluirExternos)
        {
            StringBuilder sb = new StringBuilder();
            string sTooltip = "";
            sb.Append("<table id='tblDatosSC' class='MA' style='width:450px;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:20px;' />");
            sb.Append(" <col style='width:255px;' />");
            sb.Append(" <col style='width:65px;' />");
            sb.Append(" <col style='width:65px;' />");
            sb.Append(" <col style='width:45px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = DAL.Curriculum.CvSinCompletar_C(bNoCV, bExcluirExternos);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["T001_IDFICEPI"].ToString() + "' style='noWrap:true;' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("comentario=\"" + Utilidades.escape(dr["t165_comentario"].ToString()) + "\" ");

                sTooltip = "<label style=width:100px>Profesional:</label>" + dr["Profesional"].ToString();
                sTooltip += "<br><label style=width:100px>Empresa:</label>" + dr["EMPRESA"].ToString();
                sTooltip += "<br><label style=width:100px>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["NODO"].ToString();
                sTooltip += "<br><label style=width:100px>Evaluador:</label>" + dr["SUPERVISOR"].ToString();

                //sb.Append(" onmouseover=\'showTTE(\"" + Utilidades.escape(sTooltip) + "\",null,null,350)\' onMouseout=\"hideTTE()\" ");
                sb.Append(" ondblclick='ponerId(this.id)'>");

                sb.Append("<td style='text-align:center;'></td>");
                sb.Append("<td onmouseover=\'showTTE(\"" + Utilidades.escape(sTooltip) + "\",null,null,350)\' onMouseout=\"hideTTE()\"><nobr class='NBR W250'>" + dr["PROFESIONAL"].ToString() + "</nobr></td>");
                //sb.Append("<td><nobr class='NBR W250'>" + dr["PROFESIONAL"].ToString() + "</nobr></td>");
                sb.Append("<td>" + DateTime.Parse(dr["T001_FECALTA"].ToString()).ToShortDateString() + "</td>");
                if (dr["T001_FECBAJA"].ToString()=="")
                    sb.Append("<td></td>");
                else
                    sb.Append("<td>" + DateTime.Parse(dr["T001_FECBAJA"].ToString()).ToShortDateString() + "</td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        
        public static string FinalizacionCv(int idficepi, string comentario, string sEnviarCorreo)
        {
            bool bEnviarCorreo = false;
            try
            {
                if (sEnviarCorreo == "S")
                {
                    if (comentario.Trim() != "")
                        bEnviarCorreo = true;
                }
                #region Actualizar tabla

                DAL.Curriculum.FinalizacionCv(idficepi);//, comentario);

                #endregion

                #region Enviar por correo
                if (bEnviarCorreo)
                {
                    string sTexto = "";
                    string sAsunto = "";
                    string sTO = "";

                    sAsunto = "Alta del CV en la aplicación CVT";
                    sTexto = @" <LABEL class='TITULO'>Informarle que se ha procedido al alta de su CV en la aplicación de gestión de currículums de Ibermática CVT (módulo dentro de SuperNet), y que debe acceder a la herramienta para revisar y completar el contenido de su CV. </LABEL>";
                    sTexto += "<br><br><b>Otras observaciones:</b><br>";
                    sTexto += "<LABEL class='TITULO'>" + comentario + "</LABEL>";

                    ArrayList aListCorreo = new ArrayList();

                    //sTO = SUPER.Capa_Negocio.Recurso.obtenerCodRed(idficepi);
                    sTO = SUPER.DAL.Profesional.GetCuentaCorreo(idficepi);

                    string[] aMail = { sAsunto, sTexto, sTO, "" };
                    aListCorreo.Add(aMail);

                    Correo.EnviarCorreosCVT(aListCorreo);
                }
                #endregion

                return "OK@#@";
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al modificar los datos/ enviar correo.", ex);
            }
        }

        public static void consultaFicepi(int idFicepi, out bool? cvConsultaExternos, out bool? cvConsultaBaja, out bool? cvConsultaCoste) {
            cvConsultaExternos = null;
            cvConsultaBaja = null;
            cvConsultaCoste = null;
            SqlDataReader dr = DAL.Curriculum.consultaFicepi(idFicepi);
            if (dr.Read())
            {
                cvConsultaExternos = (bool?)bool.Parse(dr["T001_CVCONSULTAEXTERNOS"].ToString());
                cvConsultaBaja = (bool?)bool.Parse(dr["T001_CVCONSULTABAJA"].ToString());
                cvConsultaCoste = (bool?)bool.Parse(dr["T001_CVCONSULTACOSTE"].ToString());
            }
            dr.Close();
            dr.Dispose();
        }

        public static ArrayList getBotonesAMostrar(string sEstado, bool bEsMiCV, bool bEsValidador, bool esCertificado)
        {
            //El 04/05/2015 maría nos pide que no se muestre el botón Aparcar (Grabar en borrador)
            ArrayList aAcciones = new ArrayList();
            bool bEsEncargadoCV=HttpContext.Current.User.IsInRole("ECV");
            switch (sEstado)
            {
                case "R": 
                    #region No Interesante
                    if (bEsEncargadoCV)
                    {
                        aAcciones.Add(CVT.Accion.Validar);
                        aAcciones.Add(CVT.Accion.Pseudovalidar);
                        //if (!bEsMiCV)aAcciones.Add(CVT.Accion.Cumplimentar);
                    }
                    #endregion
                    break;
                case "B": 
                    #region Borrador
                    aAcciones.Add(CVT.Accion.Enviar);//Grabar
                    //if (bEsMiCV) aAcciones.Add(CVT.Accion.Aparcar);
                    if (bEsEncargadoCV)
                    {
                        if (!bEsMiCV) aAcciones.Add(CVT.Accion.Cumplimentar);
                        //aAcciones.Add(CVT.Accion.Pseudovalidar);
                    }
                    #endregion
                    break;
                case "S"://Pdte cumplimentar por el técnico con origen el encargado de CV
                case "T"://Pdte cumplimentar por el técnico con origen el Validador
                    #region Pdte cumplimentar
                    if (bEsValidador)
                    {
                        if (bEsMiCV) aAcciones.Add(CVT.Accion.Enviar); //Grabar
                        else aAcciones.Add(CVT.Accion.Validar);//Aceptar
                    }
                    else
                    {
                        aAcciones.Add(CVT.Accion.Enviar); //Grabar
                    }
                    #endregion
                    break;
                case "V":
                    #region Validado
                    aAcciones.Add(CVT.Accion.Enviar);//Grabar
                    if (bEsEncargadoCV)
                    {
                        if (!esCertificado)
                        {
                            if (!bEsMiCV)
                            {
                                //aAcciones.Add(CVT.Accion.Pseudovalidar);
                                aAcciones.Add(CVT.Accion.Cumplimentar);
                            }
                        } 
                    }
                    #endregion
                    break;
                case "O"://Pdte validar por el encargado de CV
                case "P"://Pdte validar por el Validador
                case "X"://Pseudovalidado por el Validador
                case "Y"://Pseudovalidado por el encargado de CV
                    #region Pdte Validar

                    if (bEsValidador)
                    {
                        if (bEsMiCV) aAcciones.Add(CVT.Accion.Enviar);//Grabar
                        else
                        {
                            aAcciones.Add(CVT.Accion.Validar);//Aceptar
                            aAcciones.Add(CVT.Accion.Pseudovalidar);
                            //aAcciones.Add(CVT.Accion.Cumplimentar);
                            //aAcciones.Add(CVT.Accion.Rechazar);
                        }
                    }
                    else
                    {
                        if (bEsEncargadoCV)
                        {
                            aAcciones.Add(CVT.Accion.Enviar);//Grabar 
                            if (!bEsMiCV)
                            {
                                if (!esCertificado)
                                {
                                    aAcciones.Add(CVT.Accion.Pseudovalidar);
                                    //aAcciones.Add(CVT.Accion.Rechazar);
                                    //aAcciones.Add(CVT.Accion.Cumplimentar);
                                }
                            }
                        }
                        else
                        {
                            aAcciones.Add(CVT.Accion.Enviar);//Grabar 
                        }
                    }
                    #endregion
                    break;
                case "Lectura": //Lectura (Cursos de origen 1 y 2)(Certificados validados)
                    aAcciones.Add(CVT.Accion.Lectura);
                    break;
            }
            return aAcciones;
        }

        public static string CatalogoPendienteValidar(int t001_idficepi, bool bEsECV)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='MA' style='width:890px;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:300px;' />");
            sb.Append(" <col style='width:245px;' />");
            sb.Append(" <col style='width:275px;' />");
            sb.Append(" <col style='width:70px;' />");
            //sb.Append(" <col style='width:70px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            int i = 0;
            SqlDataReader dr = DAL.Curriculum.CatalogoPendienteValidar(null, t001_idficepi, bEsECV);
            while (dr.Read())
            {
                sb.Append("<tr id='"+ i.ToString() +"' ");
                sb.Append("tipoItem='" + dr["tipoItem"].ToString() + "' ");
                sb.Append("idItem='" + dr["idItem"].ToString() + "' ");
                sb.Append("idf='" + dr["T001_IDFICEPI"].ToString() + "' ");
                sb.Append("epf='" + dr["T812_IDEXPPROFFICEPI"].ToString() + "' ");
                sb.Append("efp='" + dr["T813_IDEXPFICEPIPERFIL"].ToString() + "' ");
                sb.Append("idi='" + dr["t020_idcodidioma"].ToString() + "' ");
                sb.Append("onclick='ms(this);' ondblclick='md(this);'>");
                sb.Append("<td><nobr class='NBR W330' onmouseover='TTip(event);'>" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W275'>" + dr["desItem"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W275' onmouseover='TTip(event);'>" + dr["denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + ((dr["fecha"].ToString() == "") ? "" : DateTime.Parse(dr["fecha"].ToString()).ToShortDateString()) + "</td>");
                //06/08/2015 PPOO indica que no debe figurar validación
                //if (HttpContext.Current.User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                //{
                //    if (dr["Flimite"] != DBNull.Value) // Fecha Límite
                //        sb.Append("<td align='left'>" + DateTime.Parse(dr["Flimite"].ToString()).ToShortDateString() + "</td>");
                //    else sb.Append("<td></td>");
                //}
                //else sb.Append("<td></td>"); 
                sb.Append("</tr>");
                i++;
            }

            sb.Append("</tbody>");
            sb.Append("</table>");

            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }
        /// <summary>
        /// Catálogo de items de un CV pendientes de validar, cumplimentar o borrador
        /// 06/08/2015 PPOO nos pide que no figuren las Pdtes Validar 
        /// </summary>
        /// <param name="t001_idficepi"></param>
        /// <returns></returns>
        public static string CatalogoPendiente(int t001_idficepi)
        {
            //string sResponsable = "";
            StringBuilder sb = new StringBuilder();
            int i = 0;

            string sProfesional = SUPER.DAL.Profesional.GetNombreSuper(t001_idficepi);
            ArrayList slEncargadoCV = ListaEncargadosCV();
            ArrayList slValidadorEnForma = ListaValidadoresEnForma();

            sb.Append("<table id='tblPdtes' style='width:700px;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:200px;' />");
            sb.Append(" <col style='width:200px;' />");
            sb.Append(" <col style='width:100px;' />");
            sb.Append(" <col style='width:200px;' />");
            sb.Append("</colgroup>");
            //sb.Append("<tbody>");

            SqlDataReader dr = DAL.Curriculum.CatalogoPendiente(null, t001_idficepi);
            while (dr.Read())
            {
                //sb.Append("<tr id='" + i.ToString() + "' ");
                //sb.Append("onclick='ms(this);' ondblclick='md(this);'>");
                if (i % 2 == 0)
                    sb.Append("<tr class='fila FA'><td class='td1'>" + dr["Grupo"].ToString() + "</td>");
                else
                    sb.Append("<tr class='fila FB'><td class='td1'>" + dr["Grupo"].ToString() + "</td>");
                
                sb.Append("<td class='td2'><nobr class='NBR W200' onmouseover='TTip(event);'>" + dr["Elemento"].ToString() + "</nobr></td>");
                sb.Append("<td class='td3'>" + dr["Estado"].ToString().Trim() + "</td>");
                sb.Append("<td class='td4'>");
                sb.Append("<div style='overflow: auto; width:200px; height:48px;'>");
                sb.Append("<table id ='tblPdte" + i + "' width='184px'>");
                i++;
                switch (dr["IdFicepiResponsable"].ToString())
                {
                    case "-1":
                        //sResponsable = sEncargadoCV;
                        foreach (string sNombre in slEncargadoCV)
                        {
                            sb.Append("<tr style='height:16px;'><td><nobr class='NBR W180' onmouseover='TTip(event);'>" + sNombre + "</nobr></td></tr>");
                        }
                        break;
                    case "-2":
                        //sResponsable = sProfesional;
                        sb.Append("<tr style='height:16px;'><td></td></tr>");
                        sb.Append("<tr style='height:16px;'><td><nobr class='NBR W180' onmouseover='TTip(event);'>" + sProfesional + "</nobr></td></tr>");
                        break;
                    case "-3":
                        //sResponsable = sValidadorEnForma;
                        foreach (string sNombre in slValidadorEnForma)
                        {
                            sb.Append("<tr style='height:16px;'><td><nobr class='NBR W180' onmouseover='TTip(event);'>" + sNombre + "</nobr></td></tr>");
                        }
                        break;
                    default:
                        //sResponsable = dr["NomResponsable"].ToString();
                        sb.Append("<tr style='height:16px;'><td></td></tr>");
                        sb.Append("<tr style='height:16px;'><td><nobr class='NBR W180' onmouseover='TTip(event);'>" + dr["NomResponsable"].ToString() + "</nobr></td></tr>");
                        break;
                }

                sb.Append("</table></div></td></tr>");
            }
            //sb.Append("</tbody>");
            sb.Append("</table>");

            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        public static string cargarAdministradoresCurvit()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = DAL.Curriculum.CatalogoAdministradores();
      
            while(dr.Read())
            {
                sb.Append("<tr id='" + dr["T001_IDFICEPI"].ToString() + "' bd='' esconsultor='" + dr["ESCONSULTOR"].ToString() + "' onmousedown='ms(this);DD(event);' onclick='ms(this);cargarDatosConsultor(this);'>");
                sb.Append("<td><img src='../../../../Images/imgFN.gif'></td>");
                if (dr["T001_SEXO"].ToString() == "V")
                {
                    switch (dr["TIPO"].ToString())
                    {
                        case "E": sb.Append("<td><img src='../../../../Images/imgUsuEV.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'></td>"); break;
                        case "I": sb.Append("<td><img src='../../../../Images/imgUsuIV.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'></td>"); break;
                    }
                }
                else
                {
                    switch (dr["TIPO"].ToString())
                    {
                        case "E": sb.Append("<td><img src='../../../../Images/imgUsuEM.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'></td>"); break;
                        case "I": sb.Append("<td><img src='../../../../Images/imgUsuIM.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'></td>"); break;
                    }
                }
                sb.Append("<td ><nobr class='NBR W270'>" + dr["PROFESIONAL"] + "</nobr></td>");
                //figuras
                sb.Append("<td><div><ul id='box-" + dr["T001_IDFICEPI"].ToString() + "'>");
                if (dr["ESADMINISTRADOR"].ToString() == "1")
                    sb.Append("<li id='A' onmouseover='mcur(this);' value='1'><img src='../../../../Images/imgAdministador.gif' title='Administrador Curvit' ondragstart='return false;' onmouseover='mcur(this);' /></li>");
                if (dr["ESENCARGADO"].ToString() == "1")
                    sb.Append("<li id='E' onmouseover='mcur(this);' value='2'><img src='../../../../Images/imgEncargadoCV.gif' title='Encargado de Currículums' ondragstart='return false;' onmouseover='mcur(this);' /></li>");
                if (dr["ESCONSULTOR"].ToString() == "1")
                    sb.Append("<li id='C' onmouseover='mcur(this);' value='3'><img src='../../../../Images/imgConsultorCV.gif' title='Consultor de Currículums' ondragstart='return false;' onmouseover='mcur(this);' /></li>");

                sb.Append("</ul></div></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        public static string cargarConsultores()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbCons = new StringBuilder();
            int i = 0, idficepi = -1;
            sb.Append("var js_consultores = new Array();\n");
            SqlDataReader dr = DAL.Curriculum.CatalogoConsultores();

            while (dr.Read())
            {
                if (idficepi != int.Parse(dr["T001_IDFICEPI"].ToString()))
                {
                    if (sbCons.ToString() != "") {
                        sb.Append(sbCons.ToString() + "]};\n");
                        sbCons.Remove(0, sbCons.Length);
                    }
                    else if (i > 0) {
                        sb.Append("};\n");    
                    }
                    sb.Append("\tjs_consultores[" + i + "] = {\"idficepi\":" + dr["T001_IDFICEPI"].ToString() + ",\"nombre\":'" + dr["Profesional"].ToString() + "',\"bd\":'N',\"coste\":" + ((bool.Parse(dr["T001_CVCONSULTACOSTE"].ToString())) ? "1" : "0") + ",\"tipo\":" + ((bool.Parse(dr["T001_CVCONSULTAEXTERNOS"].ToString())) ? "1" : "0") + ",\"altabaja\":" + ((bool.Parse(dr["T001_CVCONSULTABAJA"].ToString())) ? "1" : "0"));// + "};\n");
                    if (sbCons.ToString() == "")
                        sbCons.Append(",\"estructura\":[{\"nivel\":" + dr["NIVEL"].ToString() + ",\"id\":" + dr["IDNODO"].ToString() + ",\"denominacion\":'" + dr["DENOMINACION"].ToString() + "',\"escom\":'" + dr["ESTRUCTURA"].ToString() + "'}");
                    else
                        sbCons.Append(",{\"nivel\":" + dr["NIVEL"].ToString() + ",\"id\":" + dr["IDNODO"].ToString() + ",\"denominacion\":'" + dr["DENOMINACION"].ToString() + "',\"escom\":'" + dr["ESTRUCTURA"].ToString() + "'}");

                    idficepi = int.Parse(dr["T001_IDFICEPI"].ToString());
                    i++;
                }
                else
                {
                    if (sbCons.ToString() == "")
                    {
                        sbCons.Append(",\"estructura\":[{\"nivel\":" + dr["NIVEL"].ToString() + ",\"id\":" + dr["IDNODO"].ToString() + ",\"denominacion\":'" + dr["DENOMINACION"].ToString() + "',\"escom\":'" + dr["ESTRUCTURA"].ToString() + "'}");
                    }
                    else {
                        sbCons.Append(",{\"nivel\":" + dr["NIVEL"].ToString() + ",\"id\":" + dr["IDNODO"].ToString() + ",\"denominacion\":'" + dr["DENOMINACION"].ToString() + "',\"escom\":'" + dr["ESTRUCTURA"].ToString() + "'}");
                    }
                }
            }
            if (sbCons.ToString() != "")
            {
                sb.Append(sbCons.ToString() + "]};\n");
                sbCons.Remove(0, sbCons.Length);
            }
            else if (i > 0)
            {
                sb.Append("};\n");
            }
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }

        /// <summary>
        /// Devuelve un churro con los nombres de los Encargados de CV separados por punto y coma
        /// </summary>
        /// <returns></returns>
        public static ArrayList ListaEncargadosCV()
        {
            ArrayList aLista = new ArrayList();
            SqlDataReader dr = DAL.Curriculum.CatalogoEncargadosCV();
            while (dr.Read())
            {
                aLista.Add(dr["PROFESIONAL"]);
            }
            dr.Close();
            dr.Dispose();
            return aLista;
        }
        /// <summary>
        /// Devuelve un churro con los nombres de los Encargados de CV separados por punto y coma
        /// </summary>
        /// <returns></returns>
        public static ArrayList ListaValidadoresEnForma()
        {
            ArrayList aLista = new ArrayList();
            SqlDataReader dr = DAL.Curriculum.CatalogoValidadoresEnForma();
            while (dr.Read())
            {
                aLista.Add(dr["PROFESIONAL"]);

            }
            dr.Close();
            dr.Dispose();
            return aLista;
        }
        /// <summary>
        /// Dado un profesional, devuelve una lista con las pantallas que tienen preferencia por defecto
        /// </summary>
        /// <param name="sIdFicepi"></param>
        /// <returns></returns>
        public static ArrayList ListaPreferenciasDefecto(string sIdFicepi)
        {
            ArrayList aLista = new ArrayList();
            int t001_idficepi = int.Parse(sIdFicepi);
            SqlDataReader dr = DAL.Curriculum.GetPreferenciasDefecto(t001_idficepi);
            while (dr.Read())
            {
                aLista.Add(dr["t463_idpantalla"].ToString() + "#" + dr["t462_idPrefUsuario"].ToString());
            }
            dr.Close();
            dr.Dispose();
            return aLista;
        }

        public static void updateFigurasFicepi(SqlTransaction tr, string slProfesionales, string sFigurasTodos) {
            string[] aProf = Regex.Split(slProfesionales, "#prof#");
            for (int i = 0; i < aProf.Length; i++)
            {
                if (aProf[i] != "")
                {
                    string[] aRegistro = Regex.Split(aProf[i], ",");
                    DAL.Curriculum.updateDatosVisiblesCVTFicepi(tr, int.Parse(aRegistro[0]), aRegistro[1] == "S", 
                                                                aRegistro[2] == "S", aRegistro[3] == "S");
                }
            }
            string[] aProf2 = Regex.Split(sFigurasTodos, "#prof#");
            for (int i = 0; i < aProf2.Length; i++)
            {
                if (aProf2[i] != "")
                {
                    string[] aRegistro = Regex.Split(aProf2[i], "#reg#");
                    //DAL.Curriculum.updateFigurasFicepi(tr, int.Parse(aRegistro[0]), aRegistro[1] == "1", aRegistro[2] == "1", aRegistro[3] == "1", aRegistro[4] == "1", aRegistro[5] == "1", aRegistro[6] == "1", aRegistro[7], aRegistro[8], aRegistro[9], aRegistro[10], aRegistro[11]);
                    DAL.Curriculum.updateFigurasFicepi(tr, int.Parse(aRegistro[0]), aRegistro[1] == "1", aRegistro[2] == "1", 
                                                    aRegistro[3] == "1", aRegistro[4], aRegistro[5], aRegistro[6], 
                                                    aRegistro[7], aRegistro[8]);
                }
            }
        }

        public static string CatalogoMisEvaluados(int t001_idficepi)
        {
            StringBuilder sb = new StringBuilder();
            string sTooltip = "";

            sb.Append(@"<table id='tblDatos' style='width:500px;' cellpadding='0' cellspacing='0' border='0'>
                        <colgroup>
                        <col style='width:20px' /> 
                        <col style='width:340px' />
                        <col style='width:60px' />
                        <col style='width:60px' />
                        <col style='width:20px;' />
                        </colgroup>
                        <tbody>");

            SqlDataReader dr = DAL.Curriculum.MisEvaluados(null, t001_idficepi);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("comp='" + ((dr["t001_cvcompletado_prof"] == DBNull.Value) ? "0" : "1") + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");

                sTooltip = "<label style=width:70px>Profesional:</label>" + dr["Profesional"].ToString() + "<br><label style=width:70px>Empresa:</label>" + dr["EMPRESA"].ToString() + "<br><label style=width:70px>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString();
                sb.Append("tooltip=\"" + Utilidades.escape(sTooltip) + "\" ");

                sb.Append(">");

                sb.Append("<td></td>");
                sb.Append("<td>" + dr["Profesional"].ToString() + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();

        }

        public static string setCompletadoProf(int t001_idficepi)
        {
            try
            {
                DAL.Curriculum.setCompletadoProf(null, t001_idficepi);
                //DAL.Curriculum.setRevisadoActualizadoCV(null, t001_idficepi);

                return "OK@#@";
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al grabar los datos.", ex);
            }
        }
        public static string RevisadoPerfilExper(int idExpprofficepi, string sCasos)
        {
            #region Inicio Transacción
            SqlConnection oConn;
            SqlTransaction tr;
            string sResul = "";
            try
            {
                oConn = SUPER.Capa_Negocio.Conexion.Abrir();
                tr = SUPER.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                throw (new Exception("Error al abrir la conexion", ex));
            }
            #endregion
            try
            {
                SqlDataReader dr = SUPER.DAL.EXPPROFFICEPI.RealizarValidacion(tr, idExpprofficepi);
                if (dr.Read())
                {
                    if (int.Parse(dr["perfiles"].ToString()) == 0)
                    {
                        dr.Close();
                        dr.Dispose();

                        SUPER.DAL.EXPPROFFICEPI.ActualizarFRealizTareasPlazo(tr, idExpprofficepi);
                        SUPER.Capa_Negocio.Conexion.CommitTransaccion(tr);
                        sResul = "OK@#@";
                    }
                    else
                    {
                        dr.Close();
                        dr.Dispose();

                        sResul = "OK@#@NOVALIDADO";
                    }
                }
                else
                {
                    dr.Close();
                    dr.Dispose();
                    sResul = "OK@#@";
                }
            }
            catch (Exception ex)
            {
                SUPER.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                throw ex;
            }
            finally
            {
                SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
            }
            return sResul;
        }
        public static string setRevisadoActualizadoCV(int t001_idficepi)
        {
            #region Inicio Transacción
            SqlConnection oConn;
            SqlTransaction tr;
            string sResul = "";
            try
            {
                oConn = SUPER.Capa_Negocio.Conexion.Abrir();
                tr = SUPER.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                throw (new Exception("Error al abrir la conexion", ex));
            }
            #endregion
            try
            {                
                SqlDataReader dr = SUPER.DAL.Curriculum.RealizarValidacion(null, t001_idficepi);
                if (dr.Read())
                {
                    if (int.Parse(dr["tareas_pendientes"].ToString()) == 0)
                    {
                        SUPER.DAL.Curriculum.setRevisadoActualizadoCV(tr, t001_idficepi);
                        Conexion.CommitTransaccion(tr);
                        sResul = "OK@#@";
                    }
                    else
                    {
                        sResul = "OK@#@NOVALIDADO";
                    }
                }
                else
                {
                    SUPER.DAL.Curriculum.setRevisadoActualizadoCV(tr, t001_idficepi);
                    Conexion.CommitTransaccion(tr);
                    sResul = "OK@#@";
                }
                dr.Close();
                dr.Dispose();
            }
            catch (Exception ex)
            {
                SUPER.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                throw ex;
            }
            finally
            {
                SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
            }
            return sResul;
        }
        

        public static string ElementosAsociadoAReasignar(SqlDataReader dr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblElementosAsociado' style='width:450px;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<tbody>");

            int i = 0;
            while (dr.Read())
            {
                sb.Append(@"<tr id='" + i.ToString() + @"'
                            codigo='" + dr["codigo"].ToString() + @"'
                            >");
                sb.Append("<td" + ((i == 0) ? " style='width:353px;'" : "") + "><nobr class='NBR W350' onmouseover='TTip()'>" + dr["denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td" + ((i == 0) ? " style='width:83px;'" : "") + ">" + dr["tipo"].ToString() + "</td>");
                sb.Append("</tr>");
                i++;
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();
        }

        //pestaña sinopsis
        public static string getSinopsis(int t001_idficepi)
        {
            string sinopsis = "";
            SqlDataReader dr = DAL.Curriculum.getSinopsis(null, t001_idficepi);
            if (dr.Read())
                if (dr["t185_sinopsis"] != DBNull.Value)
                    sinopsis = dr["t185_sinopsis"].ToString();
            return sinopsis;
        }

        public static void GrabarSinopsis(int t001_idficepi, string sinopsis)
        {
            DAL.Curriculum.Grabar(null,t001_idficepi, sinopsis);
        }

        //consultas/plantillas
        public static string cargarPlantillas()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Text.StringBuilder sbIberDok = new System.Text.StringBuilder();
            SqlDataReader dr = null;
            int i = 0, idPlantilla=0;
            dr = DAL.Curriculum.obtenerPlantillas();

            sb.Append("<table id='tblDatosPlantilla' style='WIDTH: 329px;'>");
            sb.Append("<colgroup><col style='width:20px'/><col style='width:309px'/></colgroup>");
            sbIberDok.Append("<table id='tblDatosPlantillaIberDok' style='WIDTH: 329px;'>");
            sbIberDok.Append("<colgroup><col style='width:20px'/><col style='width:309px'/></colgroup>");
            while (dr.Read())
            {
                idPlantilla = int.Parse(dr["T694_IDPLANTILLACV"].ToString());

                //Las plantillas para Word son de la 1 a la 5
                if (idPlantilla <= 5)
                {
                    sb.Append("<tr id='" + idPlantilla.ToString() + "' title='" + dr["T694_DESCRIPCION"].ToString() + "' style='height:20px; vertical-align:middle;'>");
                    sb.Append("<td><input id='rdbPlantilla" + idPlantilla.ToString() + "'");
                    //sb.Append(" name='plantillas' type='radio' onclick='cambioPlantilla(" + idPlantilla.ToString() + ");'");
                    sb.Append(" type='radio' style='height:12px;' onclick='cambioPlantilla(" + idPlantilla.ToString() + ");'");
                    if (i == 0)
                        sb.Append(" checked /></td>");
                    else
                        sb.Append(" /></td>");
                    sb.Append("<td style='padding-left:5px;'><nobr class='NBR W290' onmouseover='TTip(event)'><label style='cursor:pointer' onclick='cambioPlantilla(" + idPlantilla.ToString() + ")' >" + dr["T694_DESCRIPCION"].ToString() + "</label></nobr></td>");
                    sb.Append("</tr>");
                }
                //IBERDOK
                sbIberDok.Append("<tr id='ib" + idPlantilla.ToString() + "' title='" + dr["T694_DESCRIPCION"].ToString() + "' style='height:20px; vertical-align:middle;'>");
                sbIberDok.Append("<td><input id='rdbPlantillaIb" + idPlantilla.ToString() + "' type='radio' style='height:12px;'");
                sbIberDok.Append(" onclick='cambioPlantillaIb(" + idPlantilla.ToString() + ");'");
                if (i == 0)
                    sbIberDok.Append(" checked /></td>");
                else
                    sbIberDok.Append(" /></td>");

                sbIberDok.Append("<td style='padding-left:5px;'><nobr class='NBR W290' onmouseover='TTip(event)'><label style='cursor:pointer' onclick='cambioPlantillaIb(" + idPlantilla.ToString() + ")' >" + dr["T694_DESCRIPCION"].ToString() + "</label></nobr></td>");
                sbIberDok.Append("</tr>");
                
                i++;
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");
            sbIberDok.Append("</table>");
            return sb.ToString() + "@#@" + sbIberDok.ToString(); ;
        }

        public static DataSet exportarExcelAvanzada(string sListaProfSeleccionados, string sFiltros, Dictionary<string, string> htCampos)
        {
            return DAL.Curriculum.exportarExcelAvanzada(sListaProfSeleccionados.Replace(",","##"), sFiltros, htCampos);
        }

        public static string TextoLegal(int codigo)
        {
            string sTexto = "";
            SqlDataReader dr = DAL.Curriculum.getTextoLegal(null, codigo);
            if (dr.Read())
                if (dr["t182_texto"] != DBNull.Value)
                    sTexto = dr["t182_texto"].ToString();
            return sTexto;
        }
    }
}