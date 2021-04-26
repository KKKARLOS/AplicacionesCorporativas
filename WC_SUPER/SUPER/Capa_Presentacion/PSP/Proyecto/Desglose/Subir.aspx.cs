using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
using SUPER.Capa_Negocio;

using System.Xml;
using System.Xml.XPath;

namespace SUPER
{
    public class ItemsProyecto
    {
        #region Atributos privados

        private int _codPT;
        private int _codFase;
        private int _codActiv;
        private int _codTarea;
        private string _nombre;
        private string _descripcion;
        private string _tipo;
        private int _orden;
        //private DateTime _FIPL;
        private string _FIPL;
        private string _FFPL;
        private decimal _ETPL;
        private string _PRIMER_CONSUMO;
        private string _FFPR;
        private decimal _ETPR;
        //private decimal _AVANCE;
        private decimal _Consumido;
        private string _situacion;
        private bool _facturable;
        private int _margen;
        private bool _borrar;
        private Decimal _EsfuerzoHoras;

        #endregion
        #region Propiedades públicas

        public int codPT
        {
            get { return _codPT; }
            set { _codPT = value; }
        }
        public int codFase
        {
            get { return _codFase; }
            set { _codFase = value; }
        }
        public int codActiv
        {
            get { return _codActiv; }
            set { _codActiv = value; }
        }
        public int codTarea
        {
            get { return _codTarea; }
            set { _codTarea = value; }
        }
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        public string tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        public int orden
        {
            get { return _orden; }
            set { _orden = value; }
        }
        public string FIPL
        {
            get { return _FIPL; }
            set { _FIPL = value; }
        }
        public string FFPL
        {
            get { return _FFPL; }
            set { _FFPL = value; }
        }
        public decimal ETPL
        {
            get { return _ETPL; }
            set { _ETPL = value; }
        }
        public string PRIMER_CONSUMO
        {
            get { return _PRIMER_CONSUMO; }
            set { _PRIMER_CONSUMO = value; }
        }
        public string FFPR
        {
            get { return _FFPR; }
            set { _FFPR = value; }
        }
        public decimal ETPR
        {
            get { return _ETPR; }
            set { _ETPR = value; }
        }
        //public decimal AVANCE
        //{
        //    get { return _AVANCE; }
        //    set { _AVANCE = value; }
        //}
        public decimal Consumido
        {
            get { return _Consumido; }
            set { _Consumido = value; }
        }
        public string situacion
        {
            get { return _situacion; }
            set { _situacion = value; }
        }
        public bool facturable
        {
            get { return _facturable; }
            set { _facturable = value; }
        }
        public int margen
        {
            get { return _margen; }
            set { _margen = value; }
        }
        public bool borrar
        {
            get { return _borrar; }
            set { _borrar = value; }
        }
        public Decimal EsfuerzoHoras
        {
            get { return _EsfuerzoHoras; }
            set { _EsfuerzoHoras = value; }
        }

        #endregion
        public ItemsProyecto()
        {
            _codPT = 0;
            _codFase = 0;
            _codActiv = 0;
            _codTarea = 0;
            _nombre = "";
            _descripcion = "";
            _tipo="";
            _orden=0;
            _FIPL = "";
            _FFPL="";
            _ETPL=0;
            _PRIMER_CONSUMO="";
            _FFPR="";
            _ETPR=0;
            //_AVANCE=0;
            _Consumido = 0;
            _situacion="";
            _facturable=true;
            _margen=0;
            _borrar = true;
        }
        public ItemsProyecto(int codPT, int codFase, int codActiv, int codTarea, string nombre, string descripcion, string tipo, int orden,
                             string FIPL, string FFPL, Decimal ETPL, string PRIMER_CONSUMO, string FFPR,
                             Decimal ETPR, Decimal Consumido, string situacion, bool facturable, int margen)
        {
            _codPT = codPT;
            _codFase = codFase;
            _codActiv = codActiv;
            _codTarea = codTarea;
            _nombre = nombre;
            _descripcion = descripcion;
            _tipo = tipo;
            _orden = orden;
            _FIPL = FIPL;
            _FFPL = FFPL;
            _ETPL = ETPL;
            _PRIMER_CONSUMO = (PRIMER_CONSUMO=="") ? FIPL : PRIMER_CONSUMO;
            _FFPR = (FFPR == "") ? FFPL : FFPR;
            _ETPR = ETPR;
            //_AVANCE = (AVANCE > 1) ? 1 : AVANCE;//OpenProj no permite avances superiores al 100%
            _Consumido = Consumido;
            _situacion = situacion;
            _facturable = facturable;
            _margen = margen;
            _borrar = true;
            _EsfuerzoHoras = (ETPR == 0) ? ETPL : ETPR;
        }
    }
    public partial class Subir : System.Web.UI.Page//, ICallbackEventHandler
    {
        protected byte[] binaryImage;
        protected MemoryStream msFichero;
        public string EsPostBack = "false";
        public string hayConsumos = "false", sPathCompleto="";
        public string Accion = "1";//1->Importar, 2->Exportar
        private XmlDocument docxml = new XmlDocument();
        private string en = "http://schemas.microsoft.com/project";
        //private string _callbackResultado = null;
        private Hashtable htItems, htPTs, htFs, htAs, htHFs;

        private void Page_Load(object sender, System.EventArgs e)
        {
            Session["bSubido"] = false;

            try
            {
                if (!Page.IsCallback)
                {
                    if (!Page.IsPostBack)
                    {
                        hdnPSN.Value = Request.QueryString["sPSN"].ToString();
                        //hdnConsumos.Value = Request.QueryString["Cons"].ToString();
                        if (Request.QueryString["Cons"].ToString() == "S")
                            hayConsumos = "true";
                    }
                    else
                    {
                        Session["bSubido"] = true;
                        if (this.hdnAccion.Text == "1")
                            this.hdnResul.Value = Importar(this.chkEstr.Checked);
                        if (this.hdnAccion.Text == "2")
                            this.hdnResul.Value = Exportar(hdnPSN.Value.ToString(), this.chkRecursos.Checked);
                        EsPostBack = "true";
                    }
                }
            }
            catch (System.OutOfMemoryException)
            {
                //Si el archivo a subir es demasiado grande, se produce un error por
                //falta de memoria. La ventana de la barra de progreso ya avisa al usuario de
                //esta situación y cierre esta ventana.
            }
        }
        /// <summary>
        /// Genera la estructura técnica de un proyecto en SUPER, borrando la que hubiera previamente
        /// </summary>
        private string Importar_Old()
        {
            StringBuilder sb = new StringBuilder();
            int idPSN = -1, iMargen = 0, iCodUne = -1, iNumProy = -1, iPos, iAux;
            int iPT = -1, iFase = -1, iActiv = -1, iTarea = -1, iOrden = 1;
            string sSituacion = "1", sAux, sTipo, sFiniPL = "", sFfinPL = "";//sMargen = "0", 
            bool bFacturable = false;//, bEsHito = false;
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region cargo el fichero
            string strFileNameOnServer = Server.MapPath(".") + @"\" + Session["IDFICEPI_ENTRADA"] + @".xml";
            HttpPostedFile selectedFile = txtArchivo.PostedFile;
            try
            {
                selectedFile.SaveAs(strFileNameOnServer);
                docxml.Load(strFileNameOnServer);
                if (!flEstructuraCorrecta(docxml))
                    return "Error@#@La estructura a importar no es correcta.";
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al guardar el fichero XML", ex);
            }
            #endregion
            #region Abro transaccion
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            }
            #endregion
            idPSN = int.Parse(this.hdnPSN.Value);
            if (PROYECTOSUBNODO.TieneConsumos(tr, idPSN))
                return "Error@#@El proyecto tiene consumos. No se puede eliminar la estructura actual.";
            try
            {
                EstrProy.BorrarEstructura(tr, idPSN);
                bFacturable = PROYECTOSUBNODO.GetFacturable(tr, idPSN);

                XmlNodeList TAREAS = docxml.GetElementsByTagName("Tasks");
                XmlNodeList LISTA = ((XmlElement)TAREAS[0]).GetElementsByTagName("Task");

                foreach (XmlElement NODO in LISTA)
                {
                    #region recojo datos del XML
                    XmlNodeList sID = NODO.GetElementsByTagName("ID");
                    XmlNodeList sNAME = NODO.GetElementsByTagName("Name");
                    XmlNodeList sOUTLINELEVEL = NODO.GetElementsByTagName("OutlineLevel");
                    XmlNodeList sWBS = NODO.GetElementsByTagName("WBS");
                    XmlNodeList sStart = NODO.GetElementsByTagName("Start");
                    XmlNodeList sFinish = NODO.GetElementsByTagName("Finish");
                    XmlNodeList sNota = NODO.GetElementsByTagName("Notes");
                    XmlNodeList sMilestone = NODO.GetElementsByTagName("Milestone");
                    #endregion
                    #region calculo de la identación del elemento
                    if (sMilestone.Count != 0 && sMilestone[0].InnerText == "1")
                    {
                        //bEsHito = true;
                        sTipo = "HF";
                        iMargen = 0;
                    }
                    else
                    {
                        if (sWBS.Count == 0) sTipo = "T";
                        else sTipo = sWBS[0].InnerText;
                        if (iOrden == 1)
                        {
                            sTipo = "P";
                            iMargen = 0;
                        }
                        if (sTipo == "PT")
                        {
                            sTipo = "P";
                            iMargen = 0;
                        }
                    }
                    switch (sTipo)
                    {
                        case "P":
                            iMargen = 0;
                            break;
                        case "F":
                            iMargen = 20;
                            break;
                        case "A":
                            if (sOUTLINELEVEL[0].InnerText == "2")
                                iMargen = 20;
                            else
                                if (sOUTLINELEVEL[0].InnerText == "3")
                                    iMargen = 40;
                                else
                                {//es un error -> lo traduzco a tarea
                                    sTipo = "T";
                                    iMargen = 20;
                                }
                            break;
                        case "T":
                            if (sOUTLINELEVEL[0].InnerText == "2")
                                iMargen = 20;
                            else
                                if (sOUTLINELEVEL[0].InnerText == "3")
                                    iMargen = 40;
                                else
                                {
                                    if (sOUTLINELEVEL[0].InnerText == "4")
                                        iMargen = 60;
                                    else
                                    {
                                        //es un error -> lo traduzco a tarea
                                        iMargen = 20;
                                    }
                                }
                            break;
                        case "HF":
                            iMargen = 20;
                            break;
                        default://No trae tipo -> lo traduzco a tarea
                            iMargen = 20;
                            sTipo = "T";
                            break;
                    }
                    #endregion
                    sFiniPL = sStart[0].InnerText.Substring(8, 2) + "/" + sStart[0].InnerText.Substring(5, 2) + "/" + sStart[0].InnerText.Substring(0, 4);
                    sFfinPL = sFinish[0].InnerText.Substring(8, 2) + "/" + sFinish[0].InnerText.Substring(5, 2) + "/" + sFinish[0].InnerText.Substring(0, 4);
                    #region Cálculo de códigos padre
                    switch (sTipo)
                    {
                        case "P":
                            iFase = -1;
                            iActiv = -1;
                            break;
                        case "F":
                            iActiv = -1;
                            break;
                        case "A":
                            if (iMargen != 40) iFase = -1;
                            break;
                        case "T":
                            if (iMargen == 40)
                                iFase = -1;
                            else
                                if (iMargen != 60) { iFase = -1; iActiv = -1; }
                            break;
                        //case "HT":
                        //case "HF":
                        //case "HM":
                        //    iHito = int.Parse(aElem[7]);
                        //    if (sEstado == "D") sCadenaBorrado += sTipo + "@#@" + iHito.ToString() + @"##";//hito
                        //    break;
                    }
                    #endregion
                    sAux = EstrProy.Insertar(tr, iCodUne, iNumProy, idPSN, sTipo, sNAME[0].InnerText, iPT, iFase, iActiv, iMargen, iOrden++,
                                             sFiniPL, sFfinPL, 0, "", "", 0, bFacturable, false, true, sSituacion, sNota[0].InnerText, 0);
                    #region obtención del código del elemento grabado
                    iPos = sAux.IndexOf("##");
                    iAux = int.Parse(sAux.Substring(0, iPos));
                    switch (sTipo)
                    {
                        case "P":
                            iPT = iAux;
                            break;
                        case "F":
                            iFase = iAux;
                            break;
                        case "A":
                            iActiv = iAux;
                            break;
                        case "T":
                            iTarea = iAux;
                            //Hay que guardar las tareas que quedan pendientes, ya que luego hay que actualizar el estado en pantalla
                            //bEstadoTarea = TAREAPSP.bFaltanValoresAE(tr, (short)iCodUne, iAux);
                            //if (bEstadoTarea)
                            //{
                            //    //actualizo el estado de la tarea
                            //    int iUsuario = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
                            //    TAREAPSP.Modificar(tr, iTarea, sDesc, iPT, iActiv, iOrden, sFiniPL, sFfinPL, fDuracion, sFiniV,
                            //                       sFfinV, iUsuario, fPresupuesto, 2, bFacturable);
                            //    sAvisos = "Se han insertado tareas que quedan en estado Pendiente ya que el C.R. tiene atributos estadísticos\nobligatorios para los que la tarea no tiene valores asignados";
                            //    if (sTareasPendientes == "") sTareasPendientes = iAux.ToString();
                            //    else sTareasPendientes += "//" + iAux.ToString();
                            //}
                            break;
                        //case "HT":
                        //    iHito = iAux;
                        //    break;
                    }
                    //if (sTipo.Substring(0, 1) == "H")
                    //{
                    //    AsociarTareasHitos(tr, iT305IdProy, iPT, iFase, iActiv, iTarea, iHito, iMargen);
                    //}
                    #endregion
                }
                File.Delete(strFileNameOnServer);
                //this.Controls.Add(LoadControl("~/Capa_Presentacion/UserControls/Mensaje.ascx"));

                //Cierro transaccion
                Conexion.CommitTransaccion(tr);

                return "OK@#@";
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al cargar la estructura a partir del fichero XML", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
        }
        /// <summary>
        /// Genera la estructura técnica de un proyecto en SUPER, sobre un proyecto ya existente
        /// Se borran los item que están en SUPER y no están en OpenProj (salvo que tengan consumos)
        /// Si el ítem es nuevo en OpenProj - > se inserta en SUPER
        /// Si el ítem tiene codigo (<Numero1></Numero1>) se updatea en SUPER
        /// </summary>
        private string Importar(bool bBorrarEstructura)
        {
            StringBuilder sb = new StringBuilder();
            int idPSN = -1, iMargen = 0, iCodUne = -1, iNumProy = -1, iPos, iAux;
            int iPT = -1, iFase = -1, iActiv = -1, iTarea = -1, iHito=-1, iOrden=1;
            string sSituacion = "1", sAux, sTipo, sFiniPL="", sFfinPL="", sAccion="", sCodSuperItem="";//sMargen = "0", 
            bool bFacturable = false, bHayQueUpdatear=false;//, bEsHito = false
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region cargo el fichero
            string strFileNameOnServer =  Server.MapPath(".") + @"\" + Session["IDFICEPI_ENTRADA"] + @".xml";
            HttpPostedFile selectedFile = txtArchivo.PostedFile;
            try
            {
                selectedFile.SaveAs(strFileNameOnServer);
                docxml.Load(strFileNameOnServer);
                if (!flEstructuraCorrecta(docxml))
                    return "Error@#@La estructura a importar no es correcta.";
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al guardar el fichero XML", ex);
            }
            #endregion
            #region Abro transaccion
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            }
            #endregion
            idPSN = int.Parse(this.hdnPSN.Value);
            try
            {
                if (bBorrarEstructura)
                {
                    if (PROYECTOSUBNODO.TieneConsumos(tr, idPSN))
                    {
                        Conexion.CerrarTransaccion(tr);
                        return "Error@#@El proyecto tiene consumos. No se puede eliminar la estructura actual.";
                    }
                    else
                        EstrProy.BorrarEstructura(tr, idPSN);
                }
                bFacturable = PROYECTOSUBNODO.GetFacturable(tr, idPSN);
                #region Cargo una hashtable por cada tipo de item de la estructura técnica del proyecto económico
                if (!bBorrarEstructura)
                {
                    htPTs = new Hashtable();
                    htFs = new Hashtable();
                    htAs = new Hashtable();
                    htItems = new Hashtable();
                    htHFs = new Hashtable();
                    SqlDataReader dr1 = OpenProj.GetEstructura(null, idPSN);
                    while (dr1.Read())
                    {
                        switch (dr1["tipo"].ToString())
                        {
                            case "T":
                                htItems.Add((int)dr1["codTarea"], new ItemsProyecto((int)dr1["codPT"], (int)dr1["codFase"], (int)dr1["codActiv"], (int)dr1["codTarea"],
                                                                                    dr1["nombre"].ToString(), dr1["descripcion"].ToString(),
                                                                                    dr1["tipo"].ToString(),
                                                                                    (int)dr1["orden"],
                                                                                    dr1["FIPL"].ToString(),
                                                                                    dr1["FFPL"].ToString(),
                                                                                    decimal.Parse(dr1["ETPL"].ToString()),
                                                                                    dr1["PRIMER_CONSUMO"].ToString(),
                                                                                    dr1["FFPR"].ToString(),
                                                                                    decimal.Parse(dr1["ETPR"].ToString()),
                                                                                    decimal.Parse(dr1["Consumido"].ToString()),
                                                                                    dr1["SITUACION"].ToString(),
                                                                                    ((int)dr1["FACTURABLE"] == 0) ? false : true,
                                                                                    (int)dr1["MARGEN"]));
                                break;
                            case "P":
                                htPTs.Add((int)dr1["codPT"], new ItemsProyecto((int)dr1["codPT"], (int)dr1["codFase"], (int)dr1["codActiv"], (int)dr1["codTarea"],
                                                                                    dr1["nombre"].ToString(), dr1["descripcion"].ToString(),
                                                                                    dr1["tipo"].ToString(),
                                                                                    (int)dr1["orden"],
                                                                                    dr1["FIPL"].ToString(),
                                                                                    dr1["FFPL"].ToString(),
                                                                                    decimal.Parse(dr1["ETPL"].ToString()),
                                                                                    dr1["PRIMER_CONSUMO"].ToString(),
                                                                                    dr1["FFPR"].ToString(),
                                                                                    decimal.Parse(dr1["ETPR"].ToString()),
                                                                                    decimal.Parse(dr1["Consumido"].ToString()),
                                                                                    dr1["SITUACION"].ToString(),
                                                                                    ((int)dr1["FACTURABLE"] == 0) ? false : true,
                                                                                    (int)dr1["MARGEN"]));
                                break;
                            case "F":
                                htFs.Add((int)dr1["codFase"], new ItemsProyecto((int)dr1["codPT"], (int)dr1["codFase"], (int)dr1["codActiv"], (int)dr1["codTarea"],
                                                                                    dr1["nombre"].ToString(), dr1["descripcion"].ToString(),
                                                                                    dr1["tipo"].ToString(),
                                                                                    (int)dr1["orden"],
                                                                                    dr1["FIPL"].ToString(),
                                                                                    dr1["FFPL"].ToString(),
                                                                                    decimal.Parse(dr1["ETPL"].ToString()),
                                                                                    dr1["PRIMER_CONSUMO"].ToString(),
                                                                                    dr1["FFPR"].ToString(),
                                                                                    decimal.Parse(dr1["ETPR"].ToString()),
                                                                                    decimal.Parse(dr1["Consumido"].ToString()),
                                                                                    dr1["SITUACION"].ToString(),
                                                                                    ((int)dr1["FACTURABLE"] == 0) ? false : true,
                                                                                    (int)dr1["MARGEN"]));
                                break;
                            case "A":
                                htAs.Add((int)dr1["codActiv"], new ItemsProyecto((int)dr1["codPT"], (int)dr1["codFase"], (int)dr1["codActiv"], (int)dr1["codTarea"],
                                                                                    dr1["nombre"].ToString(), dr1["descripcion"].ToString(),
                                                                                    dr1["tipo"].ToString(),
                                                                                    (int)dr1["orden"],
                                                                                    dr1["FIPL"].ToString(),
                                                                                    dr1["FFPL"].ToString(),
                                                                                    decimal.Parse(dr1["ETPL"].ToString()),
                                                                                    dr1["PRIMER_CONSUMO"].ToString(),
                                                                                    dr1["FFPR"].ToString(),
                                                                                    decimal.Parse(dr1["ETPR"].ToString()),
                                                                                    decimal.Parse(dr1["Consumido"].ToString()),
                                                                                    dr1["SITUACION"].ToString(),
                                                                                    ((int)dr1["FACTURABLE"] == 0) ? false : true,
                                                                                    (int)dr1["MARGEN"]));
                                break;
                            case "HF":
                                htHFs.Add((int)dr1["codTarea"], new ItemsProyecto((int)dr1["codPT"], (int)dr1["codFase"], (int)dr1["codActiv"], (int)dr1["codTarea"],
                                                                                    dr1["nombre"].ToString(), dr1["descripcion"].ToString(),
                                                                                    dr1["tipo"].ToString(),
                                                                                    (int)dr1["orden"],
                                                                                    dr1["FIPL"].ToString(),
                                                                                    dr1["FFPL"].ToString(),
                                                                                    decimal.Parse(dr1["ETPL"].ToString()),
                                                                                    dr1["PRIMER_CONSUMO"].ToString(),
                                                                                    dr1["FFPR"].ToString(),
                                                                                    decimal.Parse(dr1["ETPR"].ToString()),
                                                                                    decimal.Parse(dr1["Consumido"].ToString()),
                                                                                    dr1["SITUACION"].ToString(),
                                                                                    ((int)dr1["FACTURABLE"] == 0) ? false : true,
                                                                                    (int)dr1["MARGEN"]));
                                break;
                        }
                    }
                    dr1.Close();
                    dr1.Dispose();
                }
                #endregion

                XmlNodeList TAREAS = docxml.GetElementsByTagName("Tasks");
                XmlNodeList LISTA = ((XmlElement)TAREAS[0]).GetElementsByTagName("Task");

                #region Inserto y/o updateo los items del proyecto en función de lo que leo en el XML de OpenProj
                foreach (XmlElement NODO in LISTA)
                {
                    #region recojo datos del XML
                    XmlNodeList sID = NODO.GetElementsByTagName("ID");
                    XmlNodeList sNAME = NODO.GetElementsByTagName("Name");
                    XmlNodeList sOUTLINELEVEL = NODO.GetElementsByTagName("OutlineLevel");
                    XmlNodeList sWBS = NODO.GetElementsByTagName("WBS");
                    XmlNodeList sStart = NODO.GetElementsByTagName("Start");
                    XmlNodeList sFinish = NODO.GetElementsByTagName("Finish");
                    XmlNodeList sDuration = NODO.GetElementsByTagName("Duration");
                    XmlNodeList sNota = NODO.GetElementsByTagName("Notes");
                    XmlNodeList sMilestone = NODO.GetElementsByTagName("Milestone");
                    sCodSuperItem="";
                    XmlNodeList sAttribExten = NODO.GetElementsByTagName("ExtendedAttribute");
                    if (sAttribExten.Count != 0)
                    {
                        bool bHayCodigoSUPER = false;
                        XmlNodeList lAtributos = sAttribExten[0].ChildNodes;
                        foreach (XmlNode oNodo in lAtributos)
                        {
                            if (oNodo.Name == "FieldID")
                            {
                                if (oNodo.InnerText == "188743767")
                                    bHayCodigoSUPER = true;

                            }
                            else
                            {
                                if (bHayCodigoSUPER)
                                {
                                    if (oNodo.Name == "Value")
                                    {
                                        sCodSuperItem = oNodo.InnerText;
                                        bHayCodigoSUPER = false;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    #region calculo de la identación del elemento
                    if (sMilestone.Count != 0 && sMilestone[0].InnerText == "1")
                    {
                        //bEsHito = true;
                        sTipo = "HF";
                        iMargen = 0;
                    }
                    else
                    {
                        if (sWBS.Count == 0) sTipo = "T";
                        else sTipo = sWBS[0].InnerText;
                        if (iOrden == 1)
                        {
                            sTipo = "P";
                            iMargen = 0;
                        }
                        if (sTipo == "PT")
                        {
                            sTipo = "P";
                            iMargen = 0;
                        }
                    }
                    switch(sTipo)
                    {
                        case "P":
                            iMargen = 0;
                            break;
                        case "F":
                            iMargen = 20;
                            break;
                        case "A":
                            if (sOUTLINELEVEL[0].InnerText == "2")
                                iMargen = 20;
                            else
                                if (sOUTLINELEVEL[0].InnerText == "3")
                                    iMargen = 40;
                                else
                                {//es un error -> lo traduzco a tarea
                                    sTipo = "T";
                                    iMargen = 20;
                                }
                            break;
                        case "T":
                            if (sOUTLINELEVEL[0].InnerText == "2")
                                iMargen = 20;
                            else
                                if (sOUTLINELEVEL[0].InnerText == "3")
                                    iMargen = 40;
                                else
                                {
                                    if (sOUTLINELEVEL[0].InnerText == "4")
                                        iMargen = 60;
                                    else
                                    {
                                        //es un error -> lo traduzco a tarea
                                        iMargen = 20;
                                    }
                                }
                            break;
                        case "HF":
                            iMargen = 20;
                            break;
                        default://No trae tipo -> lo traduzco a tarea
                            iMargen = 20;
                            sTipo = "T";
                            break;
                    }
                    #endregion
                    sFiniPL = sStart[0].InnerText.Substring(8, 2) + "/" + sStart[0].InnerText.Substring(5, 2) + "/" + sStart[0].InnerText.Substring(0, 4);
                    sFfinPL = sFinish[0].InnerText.Substring(8, 2) + "/" + sFinish[0].InnerText.Substring(5, 2) + "/" + sFinish[0].InnerText.Substring(0, 4);
                    #region Cálculo de códigos padre
                    switch (sTipo)
                    {
                        case "P":
                            iFase = -1;
                            iActiv = -1;
                            break;
                        case "F":
                            iActiv = -1;
                            break;
                        case "A":
                            if (iMargen != 40) iFase = -1;
                            break;
                        case "T":
                            if (iMargen == 40)
                                iFase = -1;
                            else
                                if (iMargen != 60) { iFase = -1; iActiv = -1; }
                            break;
                        //case "HT":
                        //case "HF":
                        //case "HM":
                        //    iHito = int.Parse(aElem[7]);
                        //    if (sEstado == "D") sCadenaBorrado += sTipo + "@#@" + iHito.ToString() + @"##";//hito
                        //    break;
                    }
                    #endregion
                    if (sCodSuperItem == "" || bBorrarEstructura)
                        sAccion = "I";
                    else
                    {
                        if (sCodSuperItem == "0.0" || sCodSuperItem == "0,0")
                            sAccion = "I";
                        else
                        {
                            sAccion = "U";
                            //El item esta en OpenProj -> marco en la hashtable para que no lo borre
                            #region obtención del código del elemento updateado
                            sAux = sCodSuperItem.Replace(",", ".");
                            iAux = int.Parse(sAux);
                            ItemsProyecto oItemAux = new ItemsProyecto();
                            switch (sTipo)
                            {
                                case "P":
                                    iPT = iAux;
                                    oItemAux = (ItemsProyecto)htPTs[iPT];
                                    oItemAux.borrar = false;
                                    htPTs[iPT] = oItemAux;
                                    break;
                                case "F":
                                    iFase = iAux;
                                    oItemAux = (ItemsProyecto)htFs[iFase];
                                    oItemAux.borrar = false;
                                    htFs[iFase] = oItemAux;
                                    break;
                                case "A":
                                    iActiv = iAux;
                                    oItemAux = (ItemsProyecto)htAs[iActiv];
                                    oItemAux.borrar = false;
                                    htAs[iActiv] = oItemAux;
                                    break;
                                case "T":
                                    iTarea = iAux;
                                    oItemAux = (ItemsProyecto)htItems[iTarea];
                                    oItemAux.borrar = false;
                                    htItems[iTarea] = oItemAux;
                                    break;
                                case "HF":
                                    iHito = iAux;
                                    oItemAux = (ItemsProyecto)htHFs[iHito];
                                    oItemAux.borrar = false;
                                    htHFs[iHito] = oItemAux;
                                    break;
                            }
                            //if (sTipo.Substring(0, 1) == "H")
                            //{
                            //    AsociarTareasHitos(tr, iT305IdProy, iPT, iFase, iActiv, iTarea, iHito, iMargen);
                            //}
                            #endregion
                        }
                    }
                    if (sAccion == "I")
                    {
                        sAux = EstrProy.Insertar(tr, iCodUne, iNumProy, idPSN, sTipo, sNAME[0].InnerText, iPT, iFase, iActiv, iMargen, iOrden,
                                                 sFiniPL, sFfinPL, (double)flDuracionSUPER(sDuration[0].InnerText), 
                                                 "", "", 0, bFacturable, false, true, sSituacion, sNota[0].InnerText, 0);
                        #region obtención del código del elemento grabado
                        iPos = sAux.IndexOf("##");
                        iAux = int.Parse(sAux.Substring(0, iPos));
                        switch (sTipo)
                        {
                            case "P":
                                iPT = iAux;
                                break;
                            case "F":
                                iFase = iAux;
                                break;
                            case "A":
                                iActiv = iAux;
                                break;
                            case "T":
                                iTarea = iAux;
                                //Hay que guardar las tareas que quedan pendientes, ya que luego hay que actualizar el estado en pantalla
                                //bEstadoTarea = TAREAPSP.bFaltanValoresAE(tr, (short)iCodUne, iAux);
                                //if (bEstadoTarea)
                                //{
                                //    //actualizo el estado de la tarea
                                //    int iUsuario = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
                                //    TAREAPSP.Modificar(tr, iTarea, sDesc, iPT, iActiv, iOrden, sFiniPL, sFfinPL, fDuracion, sFiniV,
                                //                       sFfinV, iUsuario, fPresupuesto, 2, bFacturable);
                                //    sAvisos = "Se han insertado tareas que quedan en estado Pendiente ya que el C.R. tiene atributos estadísticos\nobligatorios para los que la tarea no tiene valores asignados";
                                //    if (sTareasPendientes == "") sTareasPendientes = iAux.ToString();
                                //    else sTareasPendientes += "//" + iAux.ToString();
                                //}
                                break;
                            //case "HT":
                            //    iHito = iAux;
                            //    break;
                        }
                        //if (sTipo.Substring(0, 1) == "H")
                        //{
                        //    AsociarTareasHitos(tr, iT305IdProy, iPT, iFase, iActiv, iTarea, iHito, iMargen);
                        //}
                        #endregion
                    }
                    else
                    {//Hay que updatear el item (si hay algún cambio)
                        bHayQueUpdatear=false;

                        #region Mira si hay algún datos distinto para ver si hay que updatear el registro en la BBDD
                        ItemsProyecto oItem = new ItemsProyecto();
                        switch(sTipo)
                        {
                            case "T":
                                oItem = (ItemsProyecto)htItems[iTarea];
                                break;
                            case "P":
                                oItem = (ItemsProyecto)htPTs[iPT];
                                break;
                            case "F":
                                oItem = (ItemsProyecto)htFs[iFase];
                                break;
                            case "A":
                                oItem = (ItemsProyecto)htAs[iActiv];
                                break;
                            case "HF":
                                oItem = (ItemsProyecto)htHFs[iHito];
                                break;
                        }
                        if (oItem.nombre != sNAME[0].InnerText)
                            bHayQueUpdatear=true;
                        else
                        {
                            if (oItem.descripcion != sNota[0].InnerText)
                                bHayQueUpdatear = true;
                            else
                            {
                                if (sTipo == "T")
                                {
                                    if (oItem.PRIMER_CONSUMO.Substring(0, 10) != sFiniPL)
                                        bHayQueUpdatear = true;
                                    else
                                    {
                                        if (oItem.FFPR.Substring(0, 10) != sFfinPL)
                                            bHayQueUpdatear = true;
                                        else
                                        {
                                            if (flDuracionOpenProj(oItem.ETPR, "", "") != sDuration[0].InnerText)
                                                bHayQueUpdatear = true;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        if (bHayQueUpdatear)
                        {
                            OpenProj.Modificar(tr, iCodUne, idPSN, sTipo, sNAME[0].InnerText, iPT, iFase, iActiv, iTarea, iHito, iMargen, iOrden,
                                                  sFiniPL, sFfinPL, flDuracionSUPER(sDuration[0].InnerText), sNota[0].InnerText);
                        }
                    }
                    iOrden++;
                }
                #endregion

                #region borro los items que estando en SUPER no están en OpenProj (si es tarea que no tenga consumo)
                if (!bBorrarEstructura)
                {
                    ItemsProyecto oItemD = new ItemsProyecto();
                    //Borrado de Proyectos Técnicos
                    foreach (DictionaryEntry item in htPTs)
                    {
                        oItemD = (ItemsProyecto)item.Value;
                        if (oItemD.borrar)
                            ProyTec.Eliminar(tr, oItemD.codPT);
                    }
                    //Borrado de Fases
                    foreach (DictionaryEntry item in htFs)
                    {
                        oItemD = (ItemsProyecto)item.Value;
                        if (oItemD.borrar)
                            FASEPSP.Delete(tr, oItemD.codFase);
                    }
                    //Borrado de Actividades
                    foreach (DictionaryEntry item in htAs)
                    {
                        oItemD = (ItemsProyecto)item.Value;
                        if (oItemD.borrar)
                            ACTIVIDADPSP.Delete(tr, oItemD.codActiv);
                    }
                    //Borrado de Hitos de fecha
                    foreach (DictionaryEntry item in htHFs)
                    {
                        oItemD = (ItemsProyecto)item.Value;
                        if (oItemD.borrar)
                            HITOPSP.Delete(tr, "HF", oItemD.codTarea);
                    }
                    //Borrado de Tareas que no tengan consumos
                    foreach (DictionaryEntry item in htItems)
                    {
                        oItemD = (ItemsProyecto)item.Value;
                        if (oItemD.borrar)
                        {
                            if (oItemD.Consumido == 0)
                                TAREAPSP.Delete(tr, oItemD.codTarea);
                        }
                    }
                }
                #endregion

                File.Delete(strFileNameOnServer);

                //Cierro transaccion
                Conexion.CommitTransaccion(tr);

                return "OK@#@IMP@#@";
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                return "Error@#@" + Errores.mostrarError("Error al cargar la estructura a partir del fichero XML", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
        }
        private bool flEstructuraCorrecta(XmlDocument xmlDoc)
        {
            //Dejamos el código de validación para más adelante
            return false;
        }

        private string Exportar(string sPSN, bool bIncluirRecursos)
        {
            /* Crea una cadena XML con los elementos de la estructura técnica del proyectosubnodo que se pasa como parámetro.
             * 
             * */
            int idPSN = -1, iOrden = 1, iAssignmentUID = 1, iNumDias = 0, iNumDiasBase=0;//, iUID=0
            StringBuilder sb = new StringBuilder();
            string sCodItem, sRes = "OK@#@EXP", sDenProy = "", sCodProy = "", sIniProy = "", sFinProy = "", sResponsable = "", sPdte = "";
            string sMaxUnits = "0.01000000000000000020816681711721685132943093776702880859375";
            decimal dParticipacion = 0, dParticipacionBase = 0, dEsfuerzo = 0, dEsfuerzoBase=0, dEsfAcum = 0;
            string sCodUser = "";
            try
            {
                if (sPSN != "")
                {
                    #region Obtengo datos del proyectos a exportar
                    idPSN = int.Parse(sPSN);
                    SqlDataReader drP = PROYECTO.fgGetDatosProy4(idPSN);
                    if (drP.Read())
                    {
                        sCodProy = drP["t301_idproyecto"].ToString();
                        sDenProy = "Proyecto " + int.Parse(sCodProy).ToString("#,###") + ". " + drP["t301_denominacion"].ToString();
                        sIniProy = drP["t301_fiprev"].ToString();
                        sFinProy = drP["t301_ffprev"].ToString();
                        sResponsable = drP["Profesional"].ToString();
                    }
                    drP.Close();
                    drP.Dispose();
                    #endregion
                }
                if (sCodProy != "")
                {
                    //docxml.LoadXml(@"<?xml version='1.0' encoding='UTF-8' standalone='yes'?><Project xmlns='http://schemas.microsoft.com/project'></Project>");
                    #region Meto la cabecera del XML leyéndola de los datos de la plantilla guardados en la tabla T681_PLANTILLA_OPENPROJ
                    string sArchivoPlant = Server.MapPath(".") + @"\Plantilla_" + Session["IDFICEPI_ENTRADA"] + @".xml";
                    SqlDataReader dr = OpenProj.GetPlantilla(null, 1);
                    if (dr.Read())
                    {
                        // read in using GetValue and cast to byte array                
                        //byte[] fileData = (byte[])dr.GetValue(0);
                        byte[] fileData;
                        if (dr["t2_iddocumento"].ToString() != "")
                        {
                            //fileData = SUPER.BLL.ContentServer.ObtenerDocumento((long)dr.GetValue(0));
                            fileData = IB.Conserva.ConservaHelper.ObtenerDocumento((long)dr.GetValue(0)).content;
                            // write bytes to disk as file                
                            using (System.IO.FileStream fs = new System.IO.FileStream(sArchivoPlant, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite))
                            {
                                // use a binary writer to write the bytes to disk                    
                                using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs))
                                {
                                    bw.Write(fileData);
                                    bw.Close();
                                }
                            }
                        }
                        else
                        {
                            //fileData = (byte[])dr.GetValue(1);
                            throw new Exception("No se ha encontrado la plantilla de OpenProj en el repositorio de documentos");
                        }

                    }
                    dr.Close();
                    dr.Dispose();
                    docxml.Load(sArchivoPlant);
                    File.Delete(sArchivoPlant);

                    XmlNode nodoRaiz = docxml.DocumentElement;
                    XmlNodeList LN = docxml.GetElementsByTagName("Name");
                    XmlNode nodoAux = LN.Item(0);
                    nodoAux.InnerText = "Proyecto " + sCodProy;

                    LN = docxml.GetElementsByTagName("Title");
                    nodoAux = LN.Item(0);
                    nodoAux.InnerText = sDenProy;

                    LN = docxml.GetElementsByTagName("Manager");
                    nodoAux = LN.Item(0);
                    nodoAux.InnerText = sResponsable;

                    LN = docxml.GetElementsByTagName("StartDate");
                    nodoAux = LN.Item(0);
                    nodoAux.InnerText = flGetFechaOpenProj(sIniProy, "ID");

                    LN = docxml.GetElementsByTagName("FinishDate"); 
                    nodoAux = LN.Item(0);
                    nodoAux.InnerText = flGetFechaOpenProj(sFinProy, "ID");

                    LN = docxml.GetElementsByTagName("CurrentDate"); 
                    nodoAux = LN.Item(0);
                    nodoAux.InnerText = flGetFechaOpenProj(DateTime.Now.ToShortDateString(), "IJ");

                    //Pongo calendario 24x7 porque sino tiene en cuenta los fines de semana y recalcula la fecha de fin de las tareas
                    LN = docxml.GetElementsByTagName("CalendarUID");
                    nodoAux = LN.Item(0);
                    nodoAux.InnerText = OpenProj.fgGetCalendario();//"2";

                    System.Xml.XmlElement Tasks = docxml.CreateElement("Tasks", en);
                    System.Xml.XmlElement Assignments = docxml.CreateElement("Assignments", en);
                    System.Xml.XmlElement Resources = docxml.CreateElement("Resources", en);
                    #endregion

                    #region Cargo una hashtable con la estructura técnica del proyecto económico
                    //htItems = new Hashtable();
                    //SqlDataReader dr1 = EstrProy.GetEstructura(null, idPSN);
                    //while (dr1.Read())
                    //{
                    //    htItems.Add(iUID++, new ItemsProyecto((int)dr1["codPT"], (int)dr1["codFase"], (int)dr1["codActiv"], (int)dr1["codTarea"],
                    //                                                        dr1["nombre"].ToString(),
                    //                                                        dr1["tipo"].ToString(),
                    //                                                        (int)dr1["orden"],
                    //                                                        dr1["FIPL"].ToString(), //DateTime.Parse(dr["FIPL"].ToString()),
                    //                                                        dr1["FFPL"].ToString(),
                    //                                                        decimal.Parse(dr1["ETPL"].ToString()),
                    //                                                        dr1["PRIMER_CONSUMO"].ToString(),
                    //                                                        dr1["FFPR"].ToString(),
                    //                                                        decimal.Parse(dr1["ETPR"].ToString()),
                    //                                                        decimal.Parse(dr1["AVANCE"].ToString()),
                    //                                                        dr1["SITUACION"].ToString(),
                    //                                                        ((int)dr1["FACTURABLE"]==0) ? false : true,
                    //                                                        (int)dr1["MARGEN"]));
                    //}
                    //dr1.Close();
                    //dr1.Dispose();
                    #endregion

                    #region cargo los recursos asociados a todas las tareas del proyecto
                    //Cargo un recurso ficticio para tareas no asignadas a recursos
                    //Resources.AppendChild(CrearResourceXml(docxml, "0", "0", "N", "No asignado", sMaxUnits, "3", "0", "3", "0", "0", "0"));
                    Resources.AppendChild(CrearResourceXml(docxml, "1", "1", ".", " ", sMaxUnits, "3", "0", "3", "0", "0", "0"));

                    //Resources.AppendChild(CrearResourceXml(docxml, "1", "1", "Perdiguero", "0.01000000000000000020816681711721685132943093776702880859375", "3", "0", "3", "0", "0", "10000"));
                    if (bIncluirRecursos)
                    {
                        //Para cada recurso hay que meter un calendario
                        XmlNodeList Calendarios = docxml.GetElementsByTagName("Calendars");
                        XmlNode nodoCalendario = Calendarios.Item(0);//Calendarios.Count - 1

                        SqlDataReader dr2 = OpenProj.GetProfesionales(null, idPSN, true);
                        while (dr2.Read())
                        {
                            sCodUser = dr2["t314_idusuario"].ToString();
                            System.Xml.XmlNode Calendario =
                                        nodoCalendario.AppendChild(CrearCalendarioUsuarioXml(docxml, sCodUser, dr2["Profesional"].ToString(), "2"));
                            Resources.AppendChild(CrearResourceXml(docxml, sCodUser, sCodUser, sCodUser, dr2["Profesional"].ToString(), sMaxUnits,
                                                                    "3", "0", "3", "0", "0", "0"));
                        }
                        dr2.Close();
                        dr2.Dispose();
                    }
                    #endregion

                    #region Meto los items del proyecto
                    bool bHayRecursos = false;
                    SqlDataReader dr1 = OpenProj.GetEstructura(null, idPSN);
                    while (dr1.Read())
                    //foreach (int iKey in htItems.Keys)
                    {
                        //ItemsProyecto oItem = new ItemsProyecto();
                        //oItem = (ItemsProyecto)htItems[iKey];
                        ItemsProyecto oItem = new ItemsProyecto((int)dr1["codPT"], (int)dr1["codFase"], (int)dr1["codActiv"], (int)dr1["codTarea"],
                                                                dr1["nombre"].ToString(), dr1["descripcion"].ToString(), dr1["tipo"].ToString(),
                                                                iOrden++,/*(int)dr1["orden"],*/ dr1["FIPL"].ToString(), dr1["FFPL"].ToString(),
                                                                decimal.Parse(dr1["ETPL"].ToString()), dr1["PRIMER_CONSUMO"].ToString(),
                                                                dr1["FFPR"].ToString(), decimal.Parse(dr1["ETPR"].ToString()),
                                                                decimal.Parse(dr1["Consumido"].ToString()), dr1["SITUACION"].ToString(),
                                                                ((int)dr1["FACTURABLE"]==0) ? false : true, (int)dr1["MARGEN"]);
                        sCodItem = "";
                        string sIdent = flGetIdentacion(oItem.margen);
                        switch (oItem.tipo)
                        {
                            case "P":
                                sCodItem = oItem.codPT.ToString();
                                break;
                            case "F":
                                sCodItem = oItem.codFase.ToString();
                                break;
                            case "A":
                                sCodItem = oItem.codActiv.ToString();
                                break;
                            case "T":
                            case "HF":
                                sCodItem = oItem.codTarea.ToString();
                                break;
                        }
                        //if (sCodItem == "143637")
                        //    sCodItem = sCodItem;
                        if (sCodItem != "")
                        {
                            //if (oItem.codTarea == 148600) oItem.Consumido = 6;
                            //if (oItem.Consumido != 0)
                                sPdte = flPdteOpenProj(oItem.ETPR, oItem.Consumido);
                            //else
                                //sPdte = "";
                            bHayRecursos = false;
                            sCodUser = "0";
                            dEsfAcum = 0;
                            iNumDiasBase = flDuracionDias(oItem.FIPL, oItem.FFPL);
                            iNumDias = flDuracionDias(oItem.PRIMER_CONSUMO, oItem.FFPR);
                            dEsfuerzo = oItem.EsfuerzoHoras;
                            dEsfuerzoBase = oItem.ETPL;
                            //Hay que dividir por el nº de dias de duración de la tarea
                            if (iNumDiasBase != 0)
                                dParticipacionBase = dEsfuerzoBase / (iNumDiasBase * 8);
                            else
                                dParticipacionBase = 0;
                            if (iNumDias != 0)
                                dParticipacion = dEsfuerzo / (iNumDias * 8);
                            else
                                dParticipacion = 0;

                            Tasks.AppendChild(
                                CrearTareaXml(docxml, sCodItem, oItem.orden.ToString(), oItem.nombre, oItem.descripcion, oItem.tipo, 
                                              sIdent, "0", flGetFechaOpenProj(oItem.PRIMER_CONSUMO, "IJ"), 
                                              flGetFechaOpenProj(oItem.FFPR, "FJ"),
                                              flDuracionOpenProj(0, oItem.PRIMER_CONSUMO, oItem.FFPR), 
                                              "7", "0", "2", sPdte, oItem.Consumido,
                                              oItem.FIPL, oItem.FFPL, oItem.ETPL, oItem.ETPR, dParticipacion)
                                      );

                            #region Meto los recursos asociados a cada tarea y las asignaciones de la situación actual
                            //Resources.AppendChild(CrearResourceXml(docxml, "1", "1", "Perdiguero", "0.01000000000000000020816681711721685132943093776702880859375", "3", "0", "3", "0", "0", "10000"));
                            if (bIncluirRecursos)
                            {
                                #region Incluyendo recursos
                                if (oItem.tipo == "T")
                                {//De momento como no tengo claro lo que hay que hacer con los recursos, solo los calculo para las tareas
                                    //Luego, si hace falta, ya pondré los de PT, F y A. Según la reunión del 8/9/2011 con Iñigo Garro
                                    //basta con asociar los recursos a las tareas

                                    //Leer de BBDD los recursos asignados a la tarea
                                    SqlDataReader dr3 = OpenProj.GetProfesionalesTarea(null, int.Parse(sCodItem), true);
                                    while (dr3.Read())
                                    {
                                        bHayRecursos = true;
                                        sCodUser = dr3["t314_idusuario"].ToString();
                                        //Segundo hay que calcular el porcentaje de participación del usuario en la tarea
                                        //  Para ello yo haria t336_etp / oItem.ETPR (si fuera división por cero, devolver cero)
                                        if (dr3["t336_etp"].ToString() == "") dEsfuerzo = 0;
                                        else dEsfuerzo = decimal.Parse(dr3["t336_etp"].ToString());
                                        dEsfAcum += dEsfuerzo;
                                        //if (oItem.ETPR == 0 || dr3["t336_etp"].ToString()=="") 
                                        //    dParticipacion = 0;
                                        //else 
                                        //    dParticipacion = decimal.Parse(dr3["t336_etp"].ToString()) / oItem.ETPR;

                                        //Hay que dividir el esfuerzo en horas por el nº de dias de duración de la tarea
                                        if (iNumDiasBase != 0)
                                            dParticipacionBase = dEsfuerzo / (iNumDiasBase * 8);
                                        else
                                            dParticipacionBase = 0;
                                        if (iNumDias != 0)
                                            dParticipacion = dEsfuerzo / (iNumDias * 8);
                                        else
                                            dParticipacion = 0;

                                        Assignments.AppendChild(
                                            CrearAssignmentXml(docxml, oItem, iAssignmentUID.ToString(), sCodUser, dEsfuerzo,
                                                               dParticipacion, dParticipacionBase, sPdte));
                                        iAssignmentUID++;
                                    }
                                    dr3.Close();
                                    dr3.Dispose();
                                    if (bHayRecursos)
                                    {
                                        //Si la suma de los esfuerzos asignados a la tarea no llegan al esfuerzo total de la tarea
                                        //metemos las horas que faltan al recurso imaginario (Sino, no respeta el esfuerzo de la tarea)
                                        if (dEsfAcum < oItem.EsfuerzoHoras)
                                        {
                                            sCodUser = "0";
                                            dEsfuerzo = oItem.EsfuerzoHoras - dEsfAcum;
                                            if (iNumDias != 0)
                                                dParticipacion = dEsfuerzo / (iNumDias * 8);
                                            else
                                                dParticipacion = 0;
                                            Assignments.AppendChild(
                                                CrearAssignmentXml(docxml, oItem, iAssignmentUID.ToString(), sCodUser,
                                                                   dEsfuerzo, dParticipacion, dParticipacionBase, sPdte));
                                            iAssignmentUID++;
                                        }
                                    }
                                    else
                                    {
                                        //meto los elementos Assignement que contiene para cada item del proyecto un elemento por cada día del intervalo
                                        Assignments.AppendChild(
                                                CrearAssignmentXml(docxml, oItem, iAssignmentUID.ToString(), sCodUser,
                                                                   dEsfuerzo, dParticipacion, dParticipacionBase, sPdte));
                                        iAssignmentUID++;
                                    }
                                }
                                else
                                {//El item no es una tarea -> asignamos recurso ficticio ¿seguro que es necesario?
                                    Assignments.AppendChild(
                                                CrearAssignmentXml(docxml, oItem, iAssignmentUID.ToString(), "1",
                                                                   dEsfuerzo, dParticipacion, dParticipacionBase, sPdte));
                                    iAssignmentUID++;
                                }
                            }
                            #endregion
                            else
                            {//En el criterio de exportación hemos marcado "No coger recursos" -> asignamos recurso ficticio
                                Assignments.AppendChild(
                                                CrearAssignmentXml(docxml, oItem, iAssignmentUID.ToString(), "1",
                                                                   dEsfuerzo, dParticipacion, dParticipacionBase, sPdte));
                                iAssignmentUID++;
                            }
                            #endregion
                        }
                    }
                    dr1.Close();
                    dr1.Dispose();
                    #endregion

                    nodoRaiz.AppendChild(Tasks);
                    nodoRaiz.AppendChild(Resources);
                    nodoRaiz.AppendChild(Assignments);

                    string sNumProy = PROYECTO.flGetNumProy(null, idPSN);
                    string sArchivoSalida = Session["IDFICEPI_ENTRADA"].ToString() + "_Proyecto_" + sNumProy + ".xml";
                    string sAux = Server.MapPath(@"/SUPER/Upload/") + @"\" + sArchivoSalida;
                    File.Delete(sAux);
                    docxml.Save(sAux);
                    sPathCompleto = sArchivoSalida;
                    //this.hdnArchivo.Text = sArchivoSalida;
                }
                return sRes;
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al generar el fichero XML con la estructura del proyecto.", ex);
            }
        }
        private XmlElement CrearCalendarioUsuarioXml(XmlDocument doc, string sUID, string sName, string sBaseUID)
        {
            System.Xml.XmlElement Calendar = doc.CreateElement("Calendar", en);

            System.Xml.XmlElement UID = doc.CreateElement("UID", en);
            UID.InnerText = sUID;
            Calendar.AppendChild(UID);

            System.Xml.XmlElement Name = doc.CreateElement("Name", en);
            Name.InnerText = sName;
            Calendar.AppendChild(Name);

            System.Xml.XmlElement BaseCalendarUID = doc.CreateElement("BaseCalendarUID", en);
            BaseCalendarUID.InnerText = sBaseUID;
            Calendar.AppendChild(BaseCalendarUID);

            return Calendar;
        }
        private XmlElement CrearTimephasedDataXml(XmlDocument doc, string sType, string sUID, string sStart, string sFinish, 
                                                  string sUnit, string sValue)
        {
            System.Xml.XmlElement TimephasedData = doc.CreateElement("TimephasedData", en);

            System.Xml.XmlElement Type = doc.CreateElement("Type", en);
            Type.InnerText = sType;
            TimephasedData.AppendChild(Type);

            System.Xml.XmlElement UID = doc.CreateElement("UID", en);
            UID.InnerText = sUID;
            TimephasedData.AppendChild(UID);

            System.Xml.XmlElement Start = doc.CreateElement("Start", en);
            Start.InnerText = sStart;
            TimephasedData.AppendChild(Start);

            System.Xml.XmlElement Finish = doc.CreateElement("Finish", en);
            Finish.InnerText = sFinish;
            TimephasedData.AppendChild(Finish);

            System.Xml.XmlElement Unit = doc.CreateElement("Unit", en);
            Unit.InnerText = sUnit;
            TimephasedData.AppendChild(Unit);

            System.Xml.XmlElement Value = doc.CreateElement("Value", en);
            Value.InnerText = sValue;
            TimephasedData.AppendChild(Value);

            return TimephasedData;
        }

        //private XmlElement CrearAssignmentXml(XmlDocument doc, string sUID, string sTaskUID, string sResourceUID, string sWork,
        //                                      string sUnits, string sFIPL, string sFFPL, string sFIPR, string sFFPR, string sPdte)
        private XmlElement CrearAssignmentXml(XmlDocument doc, ItemsProyecto oItem, string sUID, string sResourceUID, decimal dEsfuerzo,
                                              decimal dParticipacion, decimal dParticipacionBase, string sPdte)
        {
            string sWork = flDuracionOpenProj(dEsfuerzo, "", "");
            string sUnits = dParticipacion.ToString().Replace(",", ".");
            System.Xml.XmlElement Assignment = doc.CreateElement("Assignment", en);

            System.Xml.XmlElement UID = doc.CreateElement("UID", en);
            UID.InnerText = sUID;
            Assignment.AppendChild(UID);

            System.Xml.XmlElement TaskUID = doc.CreateElement("TaskUID", en);
            TaskUID.InnerText = oItem.orden.ToString();
            Assignment.AppendChild(TaskUID);

            System.Xml.XmlElement ResourceUID = doc.CreateElement("ResourceUID", en);
            ResourceUID.InnerText = sResourceUID;
            Assignment.AppendChild(ResourceUID);

            System.Xml.XmlElement RemainingWork = doc.CreateElement("RemainingWork", en);
            if (sPdte != "")
                RemainingWork.InnerText = sPdte;
            else
                RemainingWork.InnerText = sWork;
            Assignment.AppendChild(RemainingWork);

            System.Xml.XmlElement Units = doc.CreateElement("Units", en);
            Units.InnerText = "1";// sUnits; //Para que no aparezca el % en el Gantt
            Assignment.AppendChild(Units);

            System.Xml.XmlElement Work = doc.CreateElement("Work", en);
            Work.InnerText = sWork;
            Assignment.AppendChild(Work);

            // esto debería crear día a día en función del rango de fechas. Ojo con el comienzo es un día antes que el comienzo real
            if (oItem.PRIMER_CONSUMO != "" && oItem.FFPR != "")
            {
                string sHorasDia = "PT0H0M0S";
                DateTime dtAux = DateTime.Parse(oItem.PRIMER_CONSUMO);
                DateTime dtIni = DateTime.Parse(oItem.PRIMER_CONSUMO);
                DateTime dtFin = DateTime.Parse(oItem.FFPR);
                int nDifDias = Fechas.DateDiff("day", dtIni, dtFin); //* 24;
                decimal dRestoHoras = 0;
                //Nº de horas que se dedica a la tarea por día
                decimal dHorasDia = 8 * dParticipacion;
                //metemos dias correspondientes al grado de avance y luego los restantes hasta el final son líneas del estado actual
                #region Grado de avance Type=2
                if (oItem.Consumido != 0 && dHorasDia != 0)
                {
                    //Nº de días de avance es lo consumido / nº de horas dedicadas por día
                    nDifDias = int.Parse(Math.Floor(oItem.Consumido / dHorasDia).ToString());
                    sHorasDia = flPorcentajeDia(sUnits);
                    dRestoHoras = oItem.Consumido - (nDifDias * dHorasDia);
                    for (int i = 0; i < nDifDias; i++)
                    {
                        Assignment.AppendChild(CrearTimephasedDataXml(doc, "2", sUID,
                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "IJ"),
                                                flGetFechaOpenProj(dtAux.AddDays(1).ToShortDateString(), "IJ"), "3", sHorasDia));
                        dtAux = dtAux.AddDays(1);
                    }
                    if (dRestoHoras > 0)
                    {
                        dtAux = dtAux.AddDays(-1);
                        Assignment.AppendChild(CrearTimephasedDataXml(doc, "2", sUID,
                                                flGetFechaOpenProj(dtAux.AddDays(-1).ToShortDateString(), "IJ"),
                                                flGetFechaOpenProj(dtAux.ToShortDateString(), dRestoHoras.ToString()), "3", sHorasDia));
                    }
                    dtIni = dtAux;
                }
                #endregion
                #region Estado actual del proyecto Type=1
                if (oItem.Consumido == 0)
                {
                    if (nDifDias == 0)
                    {
                        Assignment.AppendChild(CrearTimephasedDataXml(doc, "1", sUID,
                                                flGetFechaOpenProj(dtAux.AddDays(-1).ToShortDateString(), "FD"),
                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "FD"), "3", sWork));
                    }
                    else
                    {
                        sHorasDia = flPorcentajeDia(sUnits);
                        for (int i = 0; i <= nDifDias; i++)
                        {
                            Assignment.AppendChild(CrearTimephasedDataXml(doc, "1", sUID,
                                                    flGetFechaOpenProj(dtAux.AddDays(-1).ToShortDateString(), "FD"),
                                                    flGetFechaOpenProj(dtAux.ToShortDateString(), "FD"), "3", sHorasDia));
                            dtAux = dtAux.AddDays(1);
                        }
                    }
                }
                else
                {
                    nDifDias = Fechas.DateDiff("day", dtIni, dtFin);
                    if (nDifDias == 0)
                    {
                        Assignment.AppendChild(CrearTimephasedDataXml(doc, "1", sUID,
                                                flGetFechaOpenProj(dtAux.AddDays(-1).ToShortDateString(), "FD"),
                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "FD"), "3", sWork));
                    }
                    else
                    {
                        sHorasDia = flPorcentajeDia(sUnits);
                        for (int i = 0; i <= nDifDias; i++)
                        {
                            Assignment.AppendChild(CrearTimephasedDataXml(doc, "1", sUID,
                                                    flGetFechaOpenProj(dtAux.AddDays(-1).ToShortDateString(), "FD"),
                                                    flGetFechaOpenProj(dtAux.ToShortDateString(), "FD"), "3", sHorasDia));
                            dtAux = dtAux.AddDays(1);
                        }
                    }
                }
                #endregion
                #region Línea base. Type=4
                sWork = flDuracionOpenProj(oItem.ETPL, "", "");
                sUnits = dParticipacionBase.ToString().Replace(",", ".");

                dtAux = DateTime.Parse(oItem.FIPL);
                dtIni = DateTime.Parse(oItem.FIPL);
                dtFin = DateTime.Parse(oItem.FFPL);
                nDifDias = Fechas.DateDiff("day", dtIni, dtFin); //* 24;
                if (nDifDias == 0)
                {
                    //Assignment.AppendChild(CrearTimephasedDataXml(doc, "4", sUID,
                    //                        flGetFechaOpenProj(dtAux.AddDays(-1).ToShortDateString(), "FD"),
                    //                        flGetFechaOpenProj(dtAux.ToShortDateString(), "FD"), "3", sWork));
                    Assignment.AppendChild(CrearTimephasedDataXml(doc, "4", sUID,
                                            flGetFechaOpenProj(dtAux.ToShortDateString(), "IJ"),
                                            flGetFechaOpenProj(dtAux.ToShortDateString(), "FJ"), "3", sWork));
                }
                else
                {
                    sHorasDia = flPorcentajeDia(sUnits);
                    for (int i = 0; i <= nDifDias; i++)
                    {
                        Assignment.AppendChild(CrearTimephasedDataXml(doc, "4", sUID,
                                                flGetFechaOpenProj(dtAux.ToShortDateString(), "IJ"),
                                                flGetFechaOpenProj(dtAux.AddDays(1).ToShortDateString(), "IJ"),
                                                "3", sHorasDia));
                        dtAux = dtAux.AddDays(1);
                    }
                }
                #endregion
            }
            //Assignment.AppendChild(CrearTimephasedDataXml(doc, "1", sUID, "2011-07-25T22:00:00", "2011-07-26T22:00:00", "3", "PT8H0M0S"));
            //Assignment.AppendChild(CrearTimephasedDataXml(doc, "1", sUID, "2011-07-26T22:00:00", "2011-07-27T22:00:00", "3", "PT8H0M0S"));
            //Assignment.AppendChild(CrearTimephasedDataXml(doc, "1", sUID, "2011-07-27T22:00:00", "2011-07-28T22:00:00", "3", "PT8H0M0S"));
            return Assignment;
        }

        private XmlElement CrearResourceXml(XmlDocument doc, string sUID, string sID, string sInitials, string sName,
                            string sMaxUnits, string sAccrueAt, string sStandardRate, string sStandardRateFormat,
                            string sCost, string sOvertimeRate, string sCostPerUse)
        {
            System.Xml.XmlElement Resource = doc.CreateElement("Resource", en);

            System.Xml.XmlElement UID = doc.CreateElement("UID", en);
            UID.InnerText = sUID;
            Resource.AppendChild(UID);

            System.Xml.XmlElement ID = doc.CreateElement("ID", en);
            ID.InnerText = sID;
            Resource.AppendChild(ID);

            System.Xml.XmlElement Name = doc.CreateElement("Name", en);
            Name.InnerText = sName;
            Resource.AppendChild(Name);

            System.Xml.XmlElement Initials = doc.CreateElement("Initials", en);
            Name.InnerText = sName;
            Resource.AppendChild(Name);

            System.Xml.XmlElement MaxUnits = doc.CreateElement("MaxUnits", en);
            MaxUnits.InnerText = sMaxUnits;
            Resource.AppendChild(MaxUnits);

            System.Xml.XmlElement AccrueAt = doc.CreateElement("AccrueAt", en);
            AccrueAt.InnerText = sAccrueAt;
            Resource.AppendChild(AccrueAt);

            System.Xml.XmlElement StandardRate = doc.CreateElement("StandardRate", en);
            StandardRate.InnerText = sStandardRate;
            Resource.AppendChild(StandardRate);

            System.Xml.XmlElement StandardRateFormat = doc.CreateElement("StandardRateFormat", en);
            StandardRateFormat.InnerText = sStandardRateFormat;
            Resource.AppendChild(StandardRateFormat);

            System.Xml.XmlElement Cost = doc.CreateElement("Cost", en);
            Cost.InnerText = sCost;
            Resource.AppendChild(Cost);

            System.Xml.XmlElement OvertimeRate = doc.CreateElement("OvertimeRate", en);
            OvertimeRate.InnerText = sOvertimeRate;
            Resource.AppendChild(OvertimeRate);

            System.Xml.XmlElement CostPerUse = doc.CreateElement("CostPerUse", en);
            CostPerUse.InnerText = sCostPerUse;
            Resource.AppendChild(CostPerUse);

            System.Xml.XmlElement CalendarUID = doc.CreateElement("CalendarUID", en);
            CalendarUID.InnerText = OpenProj.fgGetCalendario();//"2";//Es el ID del calendario en el que se trabaja todos los días y 8 horas al día
            Resource.AppendChild(CalendarUID);

            return Resource;
        }

        /// <summary>
        /// Genera un nodo XML para añadir al nodo Task
        /// En este nodo se indicará para el atributo Numero1 el código de ítem que le correponde en SUPER
        /// </summary>
        private XmlElement CrearExtendedAttributeXml(XmlDocument doc, string sUID, string sFieldID, string sValue)
        {
            System.Xml.XmlElement ExtendedAttribute = doc.CreateElement("ExtendedAttribute", en);

            System.Xml.XmlElement UID = doc.CreateElement("UID", en);
            UID.InnerText = sUID;
            ExtendedAttribute.AppendChild(UID);

            System.Xml.XmlElement FieldID = doc.CreateElement("FieldID", en);
            FieldID.InnerText = sFieldID;
            ExtendedAttribute.AppendChild(FieldID);

            System.Xml.XmlElement Value = doc.CreateElement("Value", en);
            Value.InnerText = sValue;
            ExtendedAttribute.AppendChild(Value);

            return ExtendedAttribute;
        }
        private XmlElement CrearLineaBaseTareaXml(XmlDocument doc, string sNumber, string sStart, string sFinish, string sWork)
        {
            System.Xml.XmlElement Baseline = doc.CreateElement("Baseline", en);

            System.Xml.XmlElement Number = doc.CreateElement("Number", en);
            Number.InnerText = sNumber;
            Baseline.AppendChild(Number);

            System.Xml.XmlElement Start = doc.CreateElement("Start", en);
            Start.InnerText = sStart;
            Baseline.AppendChild(Start);

            System.Xml.XmlElement Finish = doc.CreateElement("Finish", en);
            Finish.InnerText = sFinish;
            Baseline.AppendChild(Finish);

            System.Xml.XmlElement Work = doc.CreateElement("Work", en);
            Work.InnerText = sWork;
            Baseline.AppendChild(Work);

            return Baseline;
        }
        private XmlElement CrearTareaXml(XmlDocument doc, string sUID, string sID, string sName, string sObs, string sWBS, string sOutlineLevel, 
                                         string sPriority, string sStart, string sFinish, string sDuration, string sDurationFormat,
                                         string sIsSubproject, string sFixedCostAccrual, string sPdte, decimal Consumido,
                                         string sFIPL, string sFFPL, decimal dETPL, decimal dETPR, decimal dParticipacion)
        {
            bool bLinBase = false;
            System.Xml.XmlElement Task = doc.CreateElement("Task", en);

            System.Xml.XmlElement UID = doc.CreateElement("UID", en);
            //El código del item se lo paso en el campo Numero1, porque el valor de este campo lo reescribe OpenProj al grabar
            //UID.InnerText = sUID;
            UID.InnerText = sID;
            Task.AppendChild(UID);

            System.Xml.XmlElement ID = doc.CreateElement("ID", en);
            ID.InnerText = sID;
            Task.AppendChild(ID);

            System.Xml.XmlElement Name = doc.CreateElement("Name", en);
            Name.InnerText = sName;
            Task.AppendChild(Name);

            //Creo que es el que indica que la tarea sea de "Duración fijada" (para que tenga un trabajo(esfuerzo) diferente en horas a la duración)
            System.Xml.XmlElement Type = doc.CreateElement("Type", en);
            Type.InnerText = "1";
            Task.AppendChild(Type);

            System.Xml.XmlElement WBS = doc.CreateElement("WBS", en);
            WBS.InnerText = sWBS;
            Task.AppendChild(WBS);

            System.Xml.XmlElement OutlineLevel = doc.CreateElement("OutlineLevel", en);
            OutlineLevel.InnerText = sOutlineLevel;
            Task.AppendChild(OutlineLevel);

            System.Xml.XmlElement Priority = doc.CreateElement("Priority", en);
            Priority.InnerText = sPriority;
            Task.AppendChild(Priority);

            System.Xml.XmlElement Start = doc.CreateElement("Start", en);
            Start.InnerText = sStart;
            Task.AppendChild(Start);

            System.Xml.XmlElement Finish = doc.CreateElement("Finish", en);
            Finish.InnerText = sFinish;
            Task.AppendChild(Finish);

            System.Xml.XmlElement Duration = doc.CreateElement("Duration", en);
            Duration.InnerText = sDuration;
            Task.AppendChild(Duration);

            System.Xml.XmlElement DurationFormat = doc.CreateElement("DurationFormat", en);
            DurationFormat.InnerText = sDurationFormat;
            Task.AppendChild(DurationFormat);

            //para indicarle que es un Hito
            if (sWBS == "HF")
            {
                System.Xml.XmlElement Milestone = doc.CreateElement("Milestone", en);
                Milestone.InnerText = "1";
                Task.AppendChild(Milestone);
            }

            //para indicarle el grado de avance de la tarea
            //if (sPdte != "" && sPdte != "PT0H0M0S")
            if (Consumido != 0 && dParticipacion != 0)
            {
                System.Xml.XmlElement Work = doc.CreateElement("Work", en);
                Work.InnerText = "PT0H0M0S";
                Task.AppendChild(Work);

                System.Xml.XmlElement Stop = doc.CreateElement("Stop", en);
                Stop.InnerText = flFechaAvance(Consumido, sStart, dParticipacion);
                Task.AppendChild(Stop);
            }

            System.Xml.XmlElement IsSubproject = doc.CreateElement("IsSubproject", en);
            IsSubproject.InnerText = sIsSubproject;
            Task.AppendChild(IsSubproject);

            System.Xml.XmlElement FixedCostAccrual = doc.CreateElement("FixedCostAccrual", en);
            FixedCostAccrual.InnerText = sFixedCostAccrual;
            Task.AppendChild(FixedCostAccrual);

            //para indicarle el grado de avance de la tarea
            if (Consumido != 0)
            {
                System.Xml.XmlElement RemainingDuration = doc.CreateElement("RemainingDuration", en);
                //RemainingDuration.InnerText = sPdte;
                decimal dPendiente = (dETPR - Consumido) / (8 * dParticipacion);
                RemainingDuration.InnerText = flDuracionOpenProj(dPendiente, "", "");
                Task.AppendChild(RemainingDuration);
            }

            //Para indicarle que la tarea empiece en una fecha diferente a la de inicio del proyetco
            System.Xml.XmlElement ConstraintType = doc.CreateElement("ConstraintType", en);
            ConstraintType.InnerText = "4";
            Task.AppendChild(ConstraintType);

            //Para indicarle que coja el calendario en el que todos los días son laborables
            System.Xml.XmlElement CalendarUID = doc.CreateElement("CalendarUID", en);
            CalendarUID.InnerText = OpenProj.fgGetCalendario();//"2";
            Task.AppendChild(CalendarUID);

            //para indicarle la fecha de inicio de la tarea
            System.Xml.XmlElement ConstraintDate = doc.CreateElement("ConstraintDate", en);
            ConstraintDate.InnerText = sStart;
            Task.AppendChild(ConstraintDate);

            //para indicarle la descripción de la tarea
            if (sObs.Trim() != "")
            {
                System.Xml.XmlElement Notes = doc.CreateElement("Notes", en);
                Notes.InnerText = sObs;
                Task.AppendChild(Notes);
            }
            //Creo un nodo nuevo donde guardar el código del item (por si luego quiero importar)
            Task.AppendChild(CrearExtendedAttributeXml(doc, "1", "188743767", sUID));

            //Miro si es necesario guardar linea base
            //if (sWBS == "T")
            //{
                string sStartIni = "", sFinishIni = "";
                sStartIni = flGetFechaOpenProj(sFIPL, "IJ");
                if (sStartIni != sStart) bLinBase = true;
                else
                {
                    sFinishIni = flGetFechaOpenProj(sFFPL, "FJ");
                    if (sFinishIni != sFinish) bLinBase = true;
                    else
                    {
                        if (dETPL != dETPR) bLinBase = true;
                    }
                }
                if (bLinBase)
                {
                    Task.AppendChild(
                        CrearLineaBaseTareaXml(doc, "0", sStartIni, sFinishIni, flDuracionOpenProj(dETPL, "", ""))
                                     );
                }
            //}

            return Task;
        }
        //private XmlElement CrearDiasLineaBaseXml(XmlDocument doc, XmlElement Assignment, ItemsProyecto oItem, string sUID, string sResourceUID, decimal dEsfuerzo,
        //                                         decimal dParticipacion, string sPdte)
        //{
        //    string sWork = flDuracionOpenProj(dEsfuerzo, "", "");
        //    string sUnits = dParticipacion.ToString().Replace(",", ".");
        //    // esto debería crear día a día en función del rango de fechas. Ojo con el comienzo es un día antes que el comienzo real
        //    if (oItem.FIPL != "" && oItem.FFPL != "")
        //    {
        //        sWork = flDuracionOpenProj(oItem.ETPL, "", "");
        //        sUnits = dParticipacion.ToString().Replace(",", ".");

        //        DateTime dtAux = DateTime.Parse(oItem.FIPL);
        //        DateTime dtIni = DateTime.Parse(oItem.FIPL);
        //        DateTime dtFin = DateTime.Parse(oItem.FFPL);
        //        int nDifDias = Fechas.DateDiff("day", dtIni, dtFin); //* 24;
        //        if (nDifDias == 0)
        //        {
        //            Assignment.AppendChild(CrearTimephasedDataXml(doc, "4", sUID,
        //                                    flGetFechaOpenProj(dtAux.AddDays(-1).ToShortDateString(), "ID"),
        //                                    flGetFechaOpenProj(dtAux.ToShortDateString(), "FD"), "3", sWork));
        //        }
        //        else
        //        {
        //            string sHorasDia = flPorcentajeDia(sUnits);
        //            for (int i = 0; i <= nDifDias; i++)
        //            {
        //                Assignment.AppendChild(CrearTimephasedDataXml(doc, "4", sUID,
        //                                        flGetFechaOpenProj(dtAux.AddDays(-1).ToShortDateString(), "ID"),
        //                                        flGetFechaOpenProj(dtAux.ToShortDateString(), "FD"),
        //                                        "3", sHorasDia));
        //                dtAux = dtAux.AddDays(1);
        //            }
        //        }
        //    }
        //    return Assignment;
        //}

        private string flRellenar(int nNum)
        {
            string sRes;
            if (nNum < 10)
                sRes = "0" + nNum.ToString();
            else
                sRes = nNum.ToString();
            return sRes;
        }
        private string flGetIdentacion(int iMargen)
        {
            string sRes = "1";
            switch (iMargen)
            {
                case 0:
                    sRes = "1";
                    break;
                case 20:
                    sRes = "2";
                    break;
                case 40:
                    sRes = "3";
                    break;
                case 60:
                    sRes = "4";
                    break;
            }
            return sRes;
        }
        //Devuelve una fecha en formato OpenProj
        private string flGetFechaOpenProj(string sFecha, string sTipo)
        {
            //string sRes = "1970-01-01T01:00:00";
            DateTime dtHoy = DateTime.Now;
            string sRes = dtHoy.Year.ToString() + "-" + flRellenar(dtHoy.Month)+ "-" + flRellenar(dtHoy.Day)+ "T01:00:00";
            if (sFecha != "")
            {
                DateTime dtAux = DateTime.Parse(sFecha);
                //sRes = dtAux.Year.ToString() + "-" +
                //       (dtAux.Month < 10) ? "0" + dtAux.Month.ToString() : dtAux.Month.ToString() + "-" + 
                //       (dtAux.Day < 10) ? "0" + dtAux.Day.ToString() : dtAux.Day.ToString();
                sRes = dtAux.Year.ToString() + "-" + flRellenar(dtAux.Month) + "-" + flRellenar(dtAux.Day);
                switch (sTipo)
                {
                    case "ID"://Inicio día
                        //sRes += "T01:00:00";
                        sRes += "T08:00:00";
                        break;
                    case "FD"://Fin día
                        sRes += "T22:00:00";
                        //sRes += "T16:00:00";
                        break;
                    case "IJ"://Inicio jornada
                        sRes += "T08:00:00";
                        break;
                    case "FJ"://Fin jornada
                        sRes += "T16:00:00";
                        break;
                    default://En tipo hemos pasado un nº de horas
                        decimal dHoras = decimal.Parse(sTipo);
                        int nHoras = (int)Math.Floor(dHoras);
                        int nMinutos = (int)(60 * (dHoras - nHoras));
                        string sHoras = (8 + nHoras).ToString();
                        if (sHoras.Length == 1) sHoras = "0" + sHoras;
                        string sMinutos = nMinutos.ToString();
                        if (sMinutos.Length == 1) sMinutos = "0" + sMinutos;
                        sRes += "T" + sHoras + ":" + sMinutos + ":00";
                        break;

                }
            }
            return sRes;
        }
        //Si hay Esfuerzo Total Previsto se toma ese valor. Sino la diferencia entre fecha primer consumo y fecha fin prevista
        //(si no hay fecha de fin prevista, se toma la fecha de fin planificada)
        private int flDuracionDias(string sFechaIni, string sFechaFin)
        {
            int iRes = 0;
            if (sFechaIni != "" && sFechaFin != "")
            {
                DateTime dtIni = DateTime.Parse(sFechaIni);
                DateTime dtFin = DateTime.Parse(sFechaFin);
                iRes = (Fechas.DateDiff("day", dtIni, dtFin) + 1);
            }
            return iRes;
        }
        private string flDuracionOpenProj(decimal dEtpr, string sFechaIni, string sFechaFin)
        {
            string sRes = "PT0H0M0S";
            if (dEtpr == 0)
            {
                if (sFechaIni != "" && sFechaFin != "")
                {
                    DateTime dtIni = DateTime.Parse(sFechaIni);
                    DateTime dtFin = DateTime.Parse(sFechaFin);
                    int nDifDias = (Fechas.DateDiff("day", dtIni, dtFin) + 1) * 8;
                    if (nDifDias >= 0)
                        sRes = "PT" + nDifDias.ToString() + "H0M0S";
                }
            }
            else
            {
                int nHoras = 0, nMinutos = 0;
                decimal dMinutos = 0;

                nHoras = (int)System.Math.Floor(dEtpr);
                dMinutos = dEtpr - nHoras;
                nMinutos = (int)System.Math.Floor(dMinutos * 60);

                sRes = "PT" + nHoras.ToString() + "H" + nMinutos.ToString() + "M0S";
            }
            return sRes;
        }
        //Dado el esfuerzo total previso y lo consumido, devuelve el pendiente de realizar
        private string flPdteOpenProj(decimal dEtpr, decimal dConsumido)
        {
            string sRes = "";
            decimal dPdte = dEtpr - dConsumido, dMinutos=0;
            if (dPdte > 0)
            {
                int nHoras = 0, nMinutos = 0;

                nHoras = (int)System.Math.Floor(dPdte);
                dMinutos = dPdte - nHoras;
                nMinutos = (int)System.Math.Floor(dMinutos * 60);

                sRes = "PT" + nHoras.ToString() + "H" + nMinutos.ToString() + "M0S";
            }
            else
            {
                if (dPdte < 0)//se ha consumido mas de lo presupuestado -> ponemos cero como pendiente
                    sRes = "PT0H0M0S";
            }
            return sRes;
        }
        //Devuelve la fecha de Stop según la fecha de inicio de la tarea y las horas consumidas
        //(se supone que cada día son 8 horas)
        private string flFechaAvance(decimal Consumido, string sFechaIni, decimal dParticipacion)
        {
            string sRes = "";
            if (sFechaIni != "")
            {
                if (Consumido == 0 || dParticipacion==0)
                {
                    sRes = flGetFechaOpenProj(sFechaIni, "FD");
                }
                else
                {
                    int nDias=0, nHoras = 0, nMinutos = 0;
                    decimal dMinutos = 0, dHorasDia = 8 * dParticipacion;
                    DateTime dtAux = DateTime.Parse(sFechaIni);
                    //En función de lo consumido y de lo que trabaja cada dia, calculo el nº de días de avance
                    nDias = (int)System.Math.Floor(Consumido / dHorasDia);
                    dtAux = dtAux.AddDays(nDias);
                    //Lo que queda será el avance en horas, minutos
                    nHoras = (int)System.Math.Floor(Consumido - (nDias * dHorasDia));
                    dtAux = dtAux.AddHours(nHoras);

                    dMinutos = Consumido - (nDias * dHorasDia) - nHoras;
                    nMinutos = (int)System.Math.Floor(dMinutos * 60);
                    dtAux = dtAux.AddMinutes(nMinutos);

                    sRes = dtAux.Year.ToString() + "-" + flRellenar(dtAux.Month) + "-" + flRellenar(dtAux.Day) + "T" +
                           flRellenar(dtAux.Hour) + ":" + flRellenar(dtAux.Minute) + ":00";
                }
            }
            return sRes;
        }
        //Dado un porcentaje (de participación de un usuario en una tarea)
        //Devuelve el tiempo que le debe dedicar (teniendo en cuenta que la jornada la tomamos como de 8 horas)
        private string flPorcentajeDia(string sPorcentaje)
        {
            string sRes = "PT0H0M0S";
            if (sPorcentaje != "")
            {
                sPorcentaje = sPorcentaje.Replace(".", ",");
                decimal dPorcentaje = decimal.Parse(sPorcentaje);
                if (dPorcentaje == 1)
                {
                    sRes = "PT8H0M0S";
                }
                else
                {
                    if (dPorcentaje > 0)
                    {
                        decimal dSegundos = 8 * 60 * 60 * dPorcentaje;
                        int nHoras = (int)System.Math.Floor(dSegundos / (60 * 60));

                        decimal dAux = dSegundos - (nHoras * 60 * 60);
                        int nMinutos = (int)System.Math.Floor(dAux / 60);

                        dAux = dAux - (nMinutos * 60);
                        int nSegundos = (int)System.Math.Floor(dAux);
                        sRes = "PT" + nHoras.ToString() + "H" + nMinutos.ToString() + "M" + nSegundos.ToString() + "S";

                    }
                }
            }
            return sRes;
        }
        //Dada una duración en formato OpenProj lo traslada a horas en decimal
        private decimal flDuracionSUPER(string sDuracion)
        {
            decimal dRes=0;

            if (sDuracion != "")
            {
                sDuracion=sDuracion.Substring(2);
                string[] aH = Regex.Split(sDuracion, "H");
                dRes = decimal.Parse(aH[0]);
                //Paso los minutos
                string[] aM = Regex.Split(aH[1], "M");
                dRes+= decimal.Parse(aM[0]) / 60;
            }
            return dRes;
        }
    }
}