using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using SUPER.BLL;

public partial class Capa_Presentacion_CVT_Consultas_consultaHTML : System.Web.UI.Page
{
    public string strTablaDpHTML = "", strTablaDoHTML = "", strTablaDAPHTML = "", strTablaFormAcadHTML = "", strTablaCurRecHTML = "", strTablaCurImpHTML = "";//strTablaFormHTML = "",
    public string strTablaCertExamHTML = "", strTablaIdiomasHTML = "", strTablaExpHTML = "", strTablaExpIberHTML = "", strTablaExpFueraHTML = "", strTablaSinopsHTML= "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                //string[] sFiltros = Regex.Split(Session["FILTROS_HTML"].ToString(), "{filtro}");

                
                //string[] sArbol = Regex.Split(sFiltros[1], "{valor}");//desglose

                //string strFormato = sFiltros[18];//Formato a exportar
                int idFicepi =  int.Parse(Utilidades.decodpar(Request.QueryString["idficepi"].ToString()));
                string[] sDatos = Regex.Split(Curriculum.MiCVPersonalHtml(idFicepi), "#@@#");//ficepi

                #region Datos Personales

                this.strTablaDpHTML = sDatos[0];//Datos Personales

                #endregion

                #region Datos Organizativos

                //if (sArbol[0] == "0") divDatosO.Style.Add("display", "none");//Datos Organizativos
                //else
                //{
                this.strTablaDoHTML = sDatos[1];//Datos Organizativos
                //}

                #endregion

                #region Sinopsis

                //if (sArbol[10] == "0") divSinopsis.Style.Add("display", "none");//Sinopsis
                //else
                //{
                this.strTablaSinopsHTML = sDatos[2];//Sinopsis
                //}

                #endregion

                #region Documentos Asociados al Profesional

                this.strTablaDAPHTML = sDatos[3]; //Documentos Asociados al Profesional

                #endregion

                #region Formacion

                //if (sArbol[2] == "0") //Si en el arbol Formacion no hay que mostrar no cargo las subsecciones
                //    divForma.Style.Add("display", "none");//Formacion
                //else
                //{
                #region Formacion Academica
                    //bool? bTic = null;
                    //if (sFiltros[7] != "")
                    //{
                    //    if (sFiltros[7].ToUpper()=="TRUE")
                    //        bTic=true;
                    //    else
                    //        bTic=false;
                    //}
                    ////if (sArbol[1] == "0") divFormaAcad.Style.Add("display", "none");//Formacion Academica
                    //else
                    //{
                        //string datos = TituloFicepi.MiCvTitulacionHTML(int.Parse(sFiltros[0]),//ficepi
                        //                                                int.Parse(sFiltros[2]),//Utilizar criterios
                        //                                                (sFiltros[3] == "") ? null : sFiltros[3],//t019_descripcion
                        //                                                (sFiltros[4] == "") ? null: (int?)int.Parse(sFiltros[4]),//t019_idCodTitulo
                        //                                                (sFiltros[5] == "") ? null : (int?)int.Parse(sFiltros[5]),//t019_tipo
                        //                                                bTic,//(sFiltros[7] == "") ? null : (byte?)byte.Parse(sFiltros[7]),//t019_tic
                        //                                                (sFiltros[6] == "") ? null : (int?)int.Parse(sFiltros[6])//t019_modalidad
                        //                                                );

                        string datosTit = TituloFicepi.MiCvTitulacionHTML(idFicepi,//ficepi
                                                        0,//Utilizar criterios
                                                        null,//t019_descripcion
                                                        null,//t019_idCodTitulo
                                                        null,//t019_tipo
                                                        null,//(sFiltros[7] == "") ? null : (byte?)byte.Parse(sFiltros[7]),//t019_tic
                                                        null//t019_modalidad
                                                        );

                        //Si no hay datos se oculta
                        if (datosTit != "")
                            this.strTablaFormAcadHTML = datosTit;
                        else
                            divFormaAcad.Style.Add("display", "none");
                    //}

                    #endregion
                //}
                #endregion

                #region Experiencia Profesional

                //if (sArbol[7] == "0")//Si en el arbol Experiencia no hay que mostrar no cargo las subsecciones
                //{
                //    divExp.Style.Add("display", "none");//Experiencia profesional
                //}
                //else
                //{
                    #region Secciones Experiencia profesional

                    #region Experiencia Profesional En Ibermatica


                    //if (sArbol[9] == "0") divExpIber.Style.Add("display", "none");//Experiencia en Ibermatica
                    //else
                    //{
                        //string datos = EXPPROF.MiCVExpProfEnIbermaticaHTML(int.Parse(sFiltros[0]),//ficepi
                        //                                                    int.Parse(sFiltros[2]),//Utilizar criterios
                        //                                                    (sFiltros[13] == "") ? null : sFiltros[13].ToString(),//Nombrecuenta
                        //                                                    (sFiltros[14] == "") ? null : (int?)int.Parse(sFiltros[14]),//idcuenta
                        //                                                    (sFiltros[15] == "") ? null : (int?)int.Parse(sFiltros[15]),//t483_idsector
                        //                                                    (sFiltros[16] == "") ? null : (int?)int.Parse(sFiltros[16]),//t035_codperfile
                        //                                                    (sFiltros[17] == "") ? null : sFiltros[17].ToString()//Cadena IDs Entornos tecnologicos
                        //                                                    );
                        string datosExpIb = EXPPROF.MiCVExpProfEnIbermaticaHTML(idFicepi,//ficepi
                                                                0,//Utilizar criterios
                                                                null,//Nombrecuenta
                                                                null,//idcuenta
                                                                null,//t483_idsector
                                                                null,//t035_codperfile
                                                                null//Cadena IDs Entornos tecnologicos
                                                                );

                        //Si no hay datos se oculta
                        if (datosExpIb != "")
                            this.strTablaExpIberHTML = datosExpIb;
                        else
                            divExpIber.Style.Add("display", "none");
                    //}

                    #endregion

                    #region Experiencia Profesional Fuera Ibermatica


                    //if (sArbol[8] == "0") divExpFuera.Style.Add("display", "none");//Experiencia Fuera de Ibermatica 
                    //else
                    //{
                        string datosExpF = EXPPROF.MiCVExpProfFueraIbermaticaHTML(idFicepi,//ficepi
                                                                            0,//Utilizar criterios
                                                                            null,//Nombrecuenta
                                                                            null,//idcuenta
                                                                            null,//t483_idsector
                                                                            null,//t035_codperfile
                                                                            null//Cadena IDs Entornos tecnologicos
                                                                            );
                        //Si no hay datos se oculta
                        if (datosExpF != "")
                            this.strTablaExpFueraHTML = datosExpF;
                        else
                            divExpFuera.Style.Add("display", "none");
                    //}

                    #endregion
                    #endregion
                //}
 
                #endregion

                //if (sArbol[1] == "0") //Si en el arbol Formacion no hay que mostrar no cargo las subsecciones
                //    divAccionesForma.Style.Add("display", "none");//Formacion
                //else
                //{
                #region Cursos Recibidos

                //if (sArbol[3] == "0") divCurRec.Style.Add("display", "none");//Cursos Recibidos
                //else
                //{
                    string datosFRec = Curso.MiCVFormacionRecibidaHTML(idFicepi,//ficepi
                                                                    0,//Utilizar criterios
                                                                    null//Cadena IDs Entornos tecnologicos
                                                                    );
                    //Si no hay datos se oculta
                    if (datosFRec != "")
                        this.strTablaCurRecHTML = datosFRec;
                    else
                        divCurRec.Style.Add("display", "none");
                //}

                #endregion

                #region Cursos Impartidos

                //if (sArbol[4] == "0") divCurImp.Style.Add("display", "none");//Cursos Impartidos
                //else
                //{
                    string datosFImp = Curso.MiCVFormacionImpartidaHTML(idFicepi,//ficepi
                                                                    0,//Utilizar criterios
                                                                    null//Cadena IDs Entornos tecnologicos
                                                                    );
                    //string datos = Curso.MiCVFormacionImpartidaHTML(int.Parse(sFiltros[0]),//ficepi
                    //                                int.Parse(sFiltros[2]),//Utilizar criterios
                    //                                (sFiltros[10] == "") ? null : sFiltros[10]//Cadena IDs Entornos tecnologicos
                    //                                );

                    //Si no hay datos se oculta
                    if (datosFImp != "")
                        this.strTablaCurImpHTML = datosFImp;
                    else
                        divCurImp.Style.Add("display", "none");
                //}

                #endregion
                //}
                #region Certificados/Examenes

                //if (sArbol[5] == "0" && sArbol[11] == "0") DivCEREXAM.Style.Add("display", "none");//Certificados/Examenes
                //else
                //{
                string datosCertEx = Examen.MiCVFormacionCertExamHTML(idFicepi,//ficepi
                                                                0,//Utilizar criterios
                                                                null,//IDs Certificado
                                                                null,//Nombre certificado
                                                                null,//Cadena IDs Entornos tecnologicos
                                                                null//origenConsulta
                                                                );
                    //Si no hay datos se oculta
                if (datosCertEx != "")
                    this.strTablaCertExamHTML = datosCertEx;
                else
                    divCertExam.Style.Add("display", "none");
                //}

                #endregion

                #region Idiomas/Titulos Idiomas

                //if (sArbol[6] == "0") divIDIOM.Style.Add("display", "none");//Idiomas
                //else
                //{
                    string datosId = Idioma.MiCvIdiomasHTML(idFicepi,//ficepi
                                                        0,//Utilizar criterios
                                                        null,//t020_IdCodIdioma
                                                        null//nivelidioma
                                                        );
                    //Si no hay datos se oculta
                    if (datosId != "")
                        this.strTablaIdiomasHTML = datosId;
                    else
                        divIdiomas.Style.Add("display", "none");
                //}

                #endregion

                //Si todas las secciones de Formacion estan vacias se oculta la cabecera:
//                if ((this.strTablaFormAcadHTML == "") && (this.strTablaCurRecHTML == "") && (this.strTablaCurImpHTML == "") && (this.strTablaCertExamHTML == "") && (this.strTablaIdiomasHTML == ""))

                //if ( (this.strTablaCurRecHTML == "") && (this.strTablaCurImpHTML == "") )
                //    divAccionesForma.Style.Add("display", "none");//Acciones formativas
            }
            catch (Exception ex)
            {
                hdnErrores.Value = Errores.mostrarError("Error al cargar la pagina", ex);
            }
        }
        

    }
}