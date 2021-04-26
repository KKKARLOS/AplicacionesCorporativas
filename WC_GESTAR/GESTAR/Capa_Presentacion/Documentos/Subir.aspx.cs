using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Web;
using System.IO;
using System.Text;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using GESTAR.Capa_Negocio;

namespace GESTAR
{
    public partial class Subir : System.Web.UI.Page
    {
        protected byte[] binaryImage;
        protected MemoryStream msFichero;
        public string strAutor = "Autor";
        public string EsPostBack = "false";

        private void Page_Load(object sender, System.EventArgs e)
        {
            txtArchivoOld.Attributes.Add("readonly", "readonly");
            txtAutor.Attributes.Add("readonly", "readonly");
            Session["bSubido"] = false;

            //if (Session["CATEG_SUPER"].ToString() == "A") strAutor = "<label name='lblAutor' class='enlace' onclick='obtenerAutor()'>Autor</label>";

            try
            {
                if (!Page.IsPostBack)
                {
                    hdnsTipo.Text = Request.QueryString["sTipo"].ToString();
                    hdnnItem.Text = Request.QueryString["nItem"].ToString();
                    hdnsAccion.Text = Request.QueryString["sAccion"].ToString();

                    hdnModoLectura.Text = Session["MODOLECTURA"].ToString();

                    if (hdnsAccion.Text == "I")
                    {
                        txtNumEmpleado.Text = Session["IDFICEPI"].ToString();
                        txtAutor.Text = Session["NOMBRE"].ToString();
                    }
                    else if (hdnsAccion.Text == "U")
                    {
                        switch (hdnsTipo.Text){
                            case "A":
                                DOCAREA oDocA = DOCAREA.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                                hdnnIDDOC.Text = oDocA.t083_iddocut.ToString();
                                txtDescripcion.Text = oDocA.t083_descripcion;
                                txtLink.Text = oDocA.t083_weblink;
                                txtArchivoOld.Text = oDocA.t083_nombrearchivo;

                                if (oDocA.t083_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocA.t083_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;

                                txtNumEmpleado.Text = oDocA.t083_autor.ToString();
                                txtAutor.Text = oDocA.DesAutor + "   (" + oDocA.t083_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocA.DesAutorModif + "   (" + oDocA.t083_fechamodif.ToString() + ")";

                                //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                                //if (oDocT.t063_autor.ToString() != Session["NUM_EMPLEADO_ENTRADA"].ToString() && Session["CATEG_SUPER"].ToString() != "A")
                                //{
                                //    chkPrivado.Enabled = false;
                                //    chkLectura.Enabled = false;
                                //    chkGestion.Enabled = false;
                                //}
                                break;

                            case "D":
                                DOCDEFICIENCIA oDocD = DOCDEFICIENCIA.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                                hdnnIDDOC.Text = oDocD.t084_iddocut.ToString();
                                txtDescripcion.Text = oDocD.t084_descripcion;
                                txtLink.Text = oDocD.t084_weblink;
                                txtArchivoOld.Text = oDocD.t084_nombrearchivo;

                                if (oDocD.t084_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocD.t084_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;

                                txtNumEmpleado.Text = oDocD.t084_autor.ToString();
                                txtAutor.Text = oDocD.DesAutor + "   (" + oDocD.t084_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocD.DesAutorModif + "   (" + oDocD.t084_fechamodif.ToString() + ")";
                                break;
                            case "PT":
                                /*DOCUPT oDocPT = DOCUPT.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                                hdnnIDDOC.Text = oDocPT.t062_iddocupt.ToString();
                                txtDescripcion.Text = oDocPT.t062_descripcion;
                                txtLink.Text = oDocPT.t062_weblink;
                                txtArchivoOld.Text = oDocPT.t062_nombrearchivo;

                                if (oDocPT.t062_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocPT.t062_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;
                                if (oDocPT.t062_tipogestion) chkGestion.Checked = true;
                                else chkGestion.Checked = false;

                                txtNumEmpleado.Text = oDocPT.t062_autor.ToString();
                                txtAutor.Text = oDocPT.DesAutor + "   (" + oDocPT.t062_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocPT.DesAutorModif + "   (" + oDocPT.t062_fechamodif.ToString() + ")";

                                //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                                if (oDocPT.t062_autor.ToString() != Session["NUM_EMPLEADO_ENTRADA"].ToString() && Session["CATEG_SUPER"].ToString() != "A")
                                {
                                    chkPrivado.Enabled = false;
                                    chkLectura.Enabled = false;
                                    chkGestion.Enabled = false;
                                }
                                */
                                break;
                            case "PE":
                                /*DOCUPE oDocPE = DOCUPE.Select(null, int.Parse(Request.QueryString["nIDDOC"].ToString()), false);
                                hdnnIDDOC.Text = oDocPE.t068_iddocupe.ToString();
                                txtDescripcion.Text = oDocPE.t068_descripcion;
                                txtLink.Text = oDocPE.t068_weblink;
                                txtArchivoOld.Text = oDocPE.t068_nombrearchivo;

                                if (oDocPE.t068_privado) chkPrivado.Checked = true;
                                else chkPrivado.Checked = false;
                                if (oDocPE.t068_modolectura) chkLectura.Checked = true;
                                else chkLectura.Checked = false;
                                if (oDocPE.t068_tipogestion) chkGestion.Checked = true;
                                else chkGestion.Checked = false;

                                txtNumEmpleado.Text = oDocPE.t068_autor.ToString();
                                txtAutor.Text = oDocPE.DesAutor + "   (" + oDocPE.t068_fecha.ToString() + ")";
                                txtAutorModif.Text = oDocPE.DesAutorModif + "   (" + oDocPE.t068_fechamodif.ToString() + ")";

                                //Si el usuario no es ni el autor ni es administrador, se deshabilitan las características.
                                if (oDocPE.t068_autor.ToString() != Session["NUM_EMPLEADO_ENTRADA"].ToString() && Session["CATEG_SUPER"].ToString() != "A")
                                {
                                    chkPrivado.Enabled = false;
                                    chkLectura.Enabled = false;
                                    chkGestion.Enabled = false;
                                }
                                */
                                break;
                        }
                    }
                }
                else
                {
                    Session["bSubido"] = true;
                    Grabar();
                    EsPostBack = "true";
                }
            }
            catch (System.OutOfMemoryException)
            {
                //Si el archivo a subir es demasiado grande, se produce un error por falta de memoria.
                //La ventana de la barra de progreso ya avisa al usuario de esta situación y cierra esta ventana.
            }
            catch (ConservaException cex)
            {
                this.hdnError.Value = Utilidades.MsgErrorConserva("W", cex);
            }
            catch (Exception ex)
            {
                this.hdnError.Value = "Error: " + ex.Message + (char)10;
                if (ex.InnerException != null)
                    this.hdnError.Value += "Detalle error: " + ex.InnerException.Message;
            }
        }

        private void Grabar()
        {
            byte[] ArchivoEnBinario = new Byte[0];
            string sNombre = "";
            bool bPrivado = false;
            bool bLectura = false;
            long? idContentServer = null;

            //Create el PostedFile
            HttpPostedFile Archivo = txtArchivo.PostedFile;

            //if (Archivo.ContentLength > 0)
            //{
            //    ArchivoEnBinario = new Byte[Archivo.ContentLength]; //Crear el array de bytes con la longitud del archivo
            //    Archivo.InputStream.Read(ArchivoEnBinario, 0, Archivo.ContentLength); //Forzar al control del archivo a cargar los datos en el array
            //}

            if (hdnsAccion.Text == "I")
            {
                if (Archivo.ContentLength > 0)
                {
                    string sArchivo = Archivo.FileName;
                    int nPos = sArchivo.LastIndexOf("\\");
                    sNombre = sArchivo.Substring(nPos + 1, sArchivo.Length - nPos - 1);
                }
            }
            else //U
            {
                if (Archivo.FileName != "")
                {
                    string sArchivo = Archivo.FileName;
                    int nPos = sArchivo.LastIndexOf("\\");
                    sNombre = sArchivo.Substring(nPos + 1, sArchivo.Length - nPos - 1);
                }
                else sNombre = txtArchivoOld.Text;
            }

            #region Obtengo el id del archivo en Atenea.
            //Si no hay nombre de archivo lo dejamos en NULL porque puede ser
            //que el registro no tuviera documento o que lo tuviera pero ahora el usuario lo está borrando
            if (this.hdnContentServer.Value != "" && sNombre != "")
                idContentServer = long.Parse(this.hdnContentServer.Value);
            #endregion
            #region Obtengo el contenido del archivo y lo guardo en ATENEA
            if (Archivo.ContentLength > 0)
            {
                ArchivoEnBinario = new Byte[Archivo.ContentLength]; //Crear el array de bytes con la longitud del archivo
                Archivo.InputStream.Read(ArchivoEnBinario, 0, Archivo.ContentLength); //Forzar al control del archivo a cargar los datos en el array

                //Si he seleccionado un archivo, cargo el archivo en el ContenteServer y obtengo su identificador

                if (this.hdnContentServer.Value != "")
                {//Cuando hemos cargado la página ya teníamos un Id de documento en ATENEA
                    idContentServer = long.Parse(this.hdnContentServer.Value);
                    if (sNombre == txtArchivoOld.Text)
                    {//Si el nombre del nuevo archivo es el mismo que el inicial
                        IB.Conserva.ConservaHelper.ActualizarContenidoDocumento((long)idContentServer, ArchivoEnBinario);
                    }
                    else
                    {//El archivo a cargar es dierente
                        IB.Conserva.ConservaHelper.ActualizarDocumento((long)idContentServer, ArchivoEnBinario, sNombre);
                    }
                }
                else
                    idContentServer = IB.Conserva.ConservaHelper.SubirDocumento(sNombre, ArchivoEnBinario);
            }
            #endregion


            if (chkPrivado.Checked) bPrivado = true;
            if (chkLectura.Checked) bLectura = true;

            //controlar que no se haya subido archivo: e.g. solo link
            switch (hdnsTipo.Text)
            {
                case "A": //Area
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            DOCAREA.Insert(null, int.Parse(hdnnItem.Text), idContentServer, txtDescripcion.Text, txtLink.Text, sNombre,  bPrivado, bLectura, int.Parse(txtNumEmpleado.Text));
                            break;
                        case "U":
                            DOCAREA.Update(null, int.Parse(hdnnIDDOC.Text), idContentServer, int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre,  bPrivado, bLectura, int.Parse(Session["IDFICEPI"].ToString()));
                            break;
                    }
                    break;
                case "D": //Deficiencia
                    switch (hdnsAccion.Text)
                    {
                        case "I":
                            DOCDEFICIENCIA.Insert(null, int.Parse(hdnnItem.Text), idContentServer, txtDescripcion.Text, txtLink.Text, sNombre,  bPrivado, bLectura, int.Parse(txtNumEmpleado.Text));
                            break;
                        case "U":
                            DOCDEFICIENCIA.Update(null, int.Parse(hdnnIDDOC.Text), idContentServer, int.Parse(hdnnItem.Text), txtDescripcion.Text, txtLink.Text, sNombre, bPrivado, bLectura, int.Parse(Session["IDFICEPI"].ToString()));
                            break;
                    }
                    break;
            }
            
        }
    }
}