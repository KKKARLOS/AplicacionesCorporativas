using System;
using System.Data;
using System.Globalization;
using System.IO;
using Ionic.Zip;
using NetOffice.WordApi.Enums;
using Word = NetOffice.WordApi;
using SUPER.Capa_Negocio;
using System.Collections;

public partial class Capa_Presentacion_Pruebas_Word2_Default : System.Web.UI.Page
{
    static Word.Application wordApplication = null;
    static Word.Document newDocument = null;
    static bool bErrores = false;
    static int numPags = 0;
    static int idIdioma = 0;
    public static string sPathDocsCV = "d:\\tmp\\word\\";
    public static string sPathGuardarCV = "d:\\tmp\\wordresult\\";
    static float widthImg = 70;
    private static Hashtable htCampos = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            htCampos = new Hashtable();
            wordApplication = new Word.Application();
            newDocument = new Word.Document();

            //DataSet ds = SUPER.DAL.Curriculum.ObtenerPlantillaAT(null, "12157,1568,1202,1624,3510", false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            DataSet ds = SUPER.DAL.Curriculum.ObtenerPlantillaCVC(null, "1568,1624", false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            //DataSet ds = SUPER.DAL.Curriculum.ObtenerPlantillaPPTR(null, "12157,1568,1202,1624", false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            //DataSet ds = SUPER.DAL.Curriculum.ObtenerPlantillaPPTC(null, "12157,1568,1202,1624", false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            //DataSet ds = SUPER.DAL.Curriculum.ObtenerPlantillaEP(null, "12157,1568,1202,1624", false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            //DataSet ds = SUPER.DAL.Curriculum.ObtenerProfParaCVWord02(null, "12157,1568,1202", false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            //DataSet ds = SUPER.DAL.Curriculum.ObtenerProfParaCVWord03(null, "12157,1568,1202", false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            //DataSet ds = SUPER.DAL.Curriculum.ObtenerProfParaCVWord04(null, "12157,1568,1624", false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            //DataSet ds = SUPER.DAL.Curriculum.ObtenerProfParaCVWord05(null, "12157,1568,1202", false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            //if (Request.QueryString["tipo"].ToString() == "1")
            //    CargarCVMismoDoc(ds);
            //else
            //    CargarCVDifDoc(ds);
          
            string formato = ".doc";
            
            //datos personales
            htCampos.Add("nif", 0);
            htCampos.Add("perfil", 1);
            htCampos.Add("fnacim", 1);
            htCampos.Add("nacionalidad", 1);
            htCampos.Add("sexo", 1);
            htCampos.Add("empresa", 1);
            htCampos.Add("sn2", 1);
            htCampos.Add("nodo", 1);
            htCampos.Add("fantiguedad", 1);
            htCampos.Add("rol", 1);
            htCampos.Add("oficina", 1);
            htCampos.Add("provincia", 1);
            htCampos.Add("pais", 1);
            htCampos.Add("trayinter", 1);
            htCampos.Add("dispmovilidad", 1);
            htCampos.Add("observa", 1);
            htCampos.Add("foto", 1);

            //sinopsis
            htCampos.Add("sinopsis", 1);

            //formacion academica
            htCampos.Add("FA", 1);
            htCampos.Add("FAmodalidad", 1);
            htCampos.Add("FAespecialidad", 1);
            htCampos.Add("FAtipo", 0);
            htCampos.Add("FAtic", 1);
            htCampos.Add("FAcentro", 0);
            htCampos.Add("FAfinicio", 1);
            htCampos.Add("FAffin", 1);

            //certificaciones

            htCampos.Add("CERT", 1);
            htCampos.Add("CTentidad", 1);
            htCampos.Add("CTentorno", 1);
            htCampos.Add("CTfobtencion", 0);
            htCampos.Add("CTfcaducidad", 1);

            //Cursos recibidos

            htCampos.Add("CR", 0);
            htCampos.Add("CRtipo", 1);
            htCampos.Add("CRhoras", 1);
            htCampos.Add("CRfinicio", 0);
            htCampos.Add("CRffin", 0);
            htCampos.Add("CRcentro", 1);
            htCampos.Add("CRentorno", 1);
            htCampos.Add("CRcontenido", 1);
            htCampos.Add("CRprovincia", 1);
            htCampos.Add("CRmodalidad", 1);
            
            //Cursos impartidos

            htCampos.Add("CI", 1);
            htCampos.Add("CItipo", 1);
            htCampos.Add("CIhoras", 1);
            htCampos.Add("CIfinicio", 0);
            htCampos.Add("CIffin", 0);
            htCampos.Add("CIventro", 1);
            htCampos.Add("CIentorno", 1);
            htCampos.Add("CIcontenido", 1);
            htCampos.Add("CIprovincia", 1);
            htCampos.Add("CImodalidad", 1);

            //exámenes

            htCampos.Add("EXAM", 1);
            htCampos.Add("EXentidad", 1);
            htCampos.Add("EXentorno", 1);
            htCampos.Add("EXfobtencion", 0);
            htCampos.Add("EXfcaducidad", 1);

            //EXPERIENCIAS
            //en iber
            htCampos.Add("EPIfinicio",1);
            htCampos.Add("EPIffin",1);
            htCampos.Add("EPIdescripcion",1);
            htCampos.Add("EPIareacono",1);
            htCampos.Add("EPIcliente",0);
            htCampos.Add("EPIsecsecc",1);
            htCampos.Add("EPIempresa",1);
            htCampos.Add("EPIsecsece", 1);
            //perfil en iber
            htCampos.Add("EPIperfil",1);
            htCampos.Add("EPIareatec",1);
            htCampos.Add("EPIfuncion",1);
            htCampos.Add("EPIPfinicio",1);
            htCampos.Add("EPIPffi",1);

            //fuera de iber
            htCampos.Add("EPFfinicio", 1);
            htCampos.Add("EPFffin", 1);
            htCampos.Add("EPFdescripcion", 0);
            htCampos.Add("EPFareacono", 1);
            htCampos.Add("EPFcliente", 0);
            htCampos.Add("EPFsecsecc", 1);
            htCampos.Add("EPFempresa", 1);
            htCampos.Add("EPFsecsece", 1);
            //perfil fuera
            htCampos.Add("EPFperfil",0);
            htCampos.Add("EPFareatec",1);
            htCampos.Add("EPFfuncion",1);
            htCampos.Add("EPFPfinicio",1);
            htCampos.Add("EPFPffi", 1);

            //idioma 
            htCampos.Add("IDcentro",0);
            htCampos.Add("IDfecha",1);
            htCampos.Add("IDtitulo",1);
            htCampos.Add("IDescrito",0);
            htCampos.Add("IDoral",1);
            htCampos.Add("IDcomprension", 1);

            

            if (Request.QueryString["tipo"].ToString() == "1")
                CargarCVMismoDocCamExportar(ds, formato, "Formato_CV_completo_formato_Word.doc");
            else
                CargarCVDifDocCamExportar(ds, formato, "Formato_CV_completo_formato_Word.doc");
        }
        catch (Exception)
        {
            bErrores = true;
        }
    }

    public static byte[] CargarCVMismoDocCamExportar(DataSet ds, string sExtension, string nomPlantilla)
    {
        byte[] result = null;
        string documentFile = "";
        numPags = 0;
        wordApplication = new Word.Application();
        wordApplication.DisplayAlerts = WdAlertLevel.wdAlertsNone;
        wordApplication.Visible = true;
        // add a new document

        newDocument = new Word.Document();
        Object oDoc = sPathDocsCV + nomPlantilla;
        newDocument = wordApplication.Documents.Add(oDoc);

        for (int i = 0; i < ds.Tables["DatosPersonales"].Rows.Count; i++)
        {
            if (i != 0)
            {
                object prueba = WdBreakType.wdSectionBreakNextPage;
                wordApplication.Selection.InsertBreak(WdBreakType.wdSectionBreakNextPage);
                wordApplication.Selection.InsertFile(sPathDocsCV + nomPlantilla);
            }

            CargarDatosCVCCE(ds, i, 0);
            Word.Range rng_datos = newDocument.Bookmarks["MKPlantilla"].Range;
            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            wordApplication.Selection.Delete();
            wordApplication.Selection.Delete();
            //Borrar marcadores
            while (newDocument.Bookmarks.Count != 0)
            {
                newDocument.Bookmarks[1].Delete();

            }
        }

        // Guardar o abrir documento
        documentFile = string.Format("{0}Curriculums{1}", sPathGuardarCV, sExtension);
        wordApplication.ActiveWindow.View.Type = WdViewType.wdPrintView;//para poner la vista de diseño de impresion
        if (sExtension == ".pdf")
            newDocument.SaveAs(documentFile, WdSaveFormat.wdFormatPDF);  //PDF
        else
        {
            sExtension = GetDefaultExtension(wordApplication, sExtension);
            if (sExtension == ".doc")
                newDocument.SaveAs(documentFile, WdSaveFormat.wdFormatTemplate97);  //Word 97-2003
            else
                newDocument.SaveAs(documentFile, WdSaveFormat.wdFormatDocumentDefault);  //Word 2007, que es el que está instalado en el servidor IBSERVIOFFICE
        }

        ds.Dispose();



        // close word and dispose reference
        if (wordApplication != null)
        {
            wordApplication.Documents.Close(Word.Enums.WdSaveOptions.wdDoNotSaveChanges);
            wordApplication.Quit();
            wordApplication.Dispose();
        }
        if (documentFile.ToString() != "")
        {
            //result = FileToByteArray(documentFile);
            result = System.IO.File.ReadAllBytes(documentFile.ToString());
            if (result != null)
            {
                string[] files = Directory.GetFiles(sPathGuardarCV);
                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }
        }

        return result;
    }

    public static byte[] CargarCVDifDocCamExportar(DataSet ds, string sExtension,  string nomPlantilla)
    {
        //string sPathDocsCV = System.Configuration.ConfigurationSettings.AppSettings["sPathDocsCV"].ToString();
        //string sPathGuardarCV = System.Configuration.ConfigurationSettings.AppSettings["sPathGuardarCV"].ToString();
        string documentFile = "";


        wordApplication = new Word.Application();
        //wordApplication.DisplayAlerts = WdAlertLevel.wdAlertsNone;
        //wordApplication.Visible = true;
    
    // add a new document
    Object oDoc = sPathDocsCV + nomPlantilla;
    for (int i = 0; i < ds.Tables["DatosPersonales"].Rows.Count; i++)
    {

        newDocument = wordApplication.Documents.Add(oDoc);
        CargarDatosCVCCE(ds, i, 0);
        Word.Range rng_datos = newDocument.Bookmarks["MKPlantilla"].Range;
        rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
        rng_datos.Select();
        //Borrar marcadores
        while (newDocument.Bookmarks.Count != 0)
        {
            newDocument.Bookmarks[1].Delete();
        }

        wordApplication.Selection.Delete();//por si se hubieran quedado saltos de linea 
        wordApplication.Selection.Delete();
        wordApplication.ActiveWindow.View.Type = WdViewType.wdPrintView;//para poner la vista de diseño de impresion

        // Guardar documento
        documentFile = string.Format("{0}" + ds.Tables["DatosPersonales"].Rows[i]["Profesional"].ToString() + "{1}", sPathGuardarCV, sExtension);
        double wordVersion = Convert.ToDouble(wordApplication.Version, CultureInfo.InvariantCulture);
        if (wordVersion >= 12.0)
            newDocument.SaveAs(documentFile, WdSaveFormat.wdFormatDocumentDefault);
        else
            newDocument.SaveAs(documentFile);
    }  
    if (wordApplication != null)
    {
        wordApplication.Documents.Close(Word.Enums.WdSaveOptions.wdDoNotSaveChanges);
        wordApplication.Quit();
        wordApplication.Dispose();
    }
    ds.Dispose();

    //comprimimos la carpeta que contiene los CV y devolvemos el resultado (en byte[])
    return CompressFile(sPathGuardarCV);

          

    }


    #region PlantillaCVCompleto
    private static void CargarDatosCVCCE(DataSet ds, int i, int nDocs)
    {
        string profesional = ""; //para el mensaje de error

        //Datos personales
       
            profesional = ds.Tables["DatosPersonales"].Rows[i]["Profesional"].ToString();
            CargarDatosPersonalesCVCCE(ds.Tables["DatosPersonales"].Rows[i]);
       

        //Sinopsis

            CargarSinopsisCVCCE(ds.Tables["DatosPersonales"].Rows[i]);
       
        //Formación academica
         DataView dvFA = new DataView(ds.Tables["FormacionAcademica"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
            CargarFormacionAcademicaCVCCE(dvFA);
       
        //Certificados
         DataView dvCER = new DataView(ds.Tables["Certificados"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
            CargarCertificadosCVCCE(dvCER);
       
         
        //Experiencias
        
            DataView dvEXP = new DataView(ds.Tables["Experiencia"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
            DataView dvEXPPER = new DataView(ds.Tables["PerfilExperiencia"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
            CargarExperienciasCVCCE(dvEXP, dvEXPPER);
       

        //Idiomas
         DataView dvIDI = new DataView(ds.Tables["Idiomas"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
            CargarIdiomasCVCCE(dvIDI);
        

        //Cursos
        
            DataView dvCurImp = new DataView(ds.Tables["CursosImpartidos"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
            DataView dvCurRec = new DataView(ds.Tables["CursosRecibidos"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
            DataView dvExam = new DataView(ds.Tables["Examenes"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
            CargarCursosCVCCE(dvCurImp, dvCurRec, dvExam);
        

        //Para el pie de página
        
            CargarPieCVCCE(ds.Tables["DatosPersonales"].Rows[i], i + 1, nDocs); //a la i le sumamos 1 ya que los indices van de 1 a n (no de 0 a n-1)
        

    }

    private static void CargarDatosPersonalesCVCCE(DataRow oFila)
    {
        Word.Range rng_datos = newDocument.Bookmarks["MKDatosPersonales"].Range;
        Word.Find fnd = rng_datos.Find; //rango dónde buscar
        fnd.Text = "{nombre}";
        fnd.Replacement.Text = oFila["Profesional"].ToString();
        ExecuteReplace(fnd);

        if (htCampos["nif"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueNif"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{nif}";
            fnd.Replacement.Text = oFila["t001_cip"].ToString();
            ExecuteReplace(fnd);
        }

        if (htCampos["perfil"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloquePerfil"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{perfil}";
            fnd.Replacement.Text = oFila["t035_descripcion"].ToString();
            ExecuteReplace(fnd);
        }

        if (htCampos["fnacim"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueFNacimiento"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{fNacimiento}";
            fnd.Replacement.Text = (oFila["t001_fecnacim"].ToString() != "") ? ((DateTime)oFila["t001_fecnacim"]).ToShortDateString() : "";
            ExecuteReplace(fnd);
        }
        if (htCampos["nacionalidad"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueNacionalidad"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{nacionalidad}";
            fnd.Replacement.Text = oFila["T001_NACIONALID"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["sexo"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueSexo"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{sexo}";
            fnd.Replacement.Text = oFila["SEXO"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["empresa"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueEmpresa"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{empresa}";
            fnd.Replacement.Text = oFila["EMPRESA"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["sn2"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueSN2"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{sn2}";
            fnd.Replacement.Text = oFila["T392_DENOMINACION"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["nodo"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueNodo"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{nodo}";
            fnd.Replacement.Text = oFila["T303_DENOMINACION"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["fantiguedad"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueFAntiguedad"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{fAntiguedad}";
            fnd.Replacement.Text = (oFila["T001_FECANTIGU"].ToString() != "") ? ((DateTime)oFila["T001_FECANTIGU"]).ToShortDateString() : "";
            ExecuteReplace(fnd);
        }
        if (htCampos["rol"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueRol"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{rol}";
            fnd.Replacement.Text = oFila["ROL"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["oficina"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueOficina"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{oficina}";
            fnd.Replacement.Text = oFila["T010_DESOFICINA"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["provincia"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueProvincia"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{provincia}";
            fnd.Replacement.Text = oFila["t173_denominacion"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["pais"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloquePais"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{pais}";
            fnd.Replacement.Text = oFila["PAIS"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["trayinter"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueTrayInter"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{trayInter}";
            fnd.Replacement.Text = oFila["trayInter"].ToString();
            ExecuteReplace(fnd);
        }

        if (htCampos["dispmovilidad"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueDispMovilidad"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{dispMovilidad}";
            fnd.Replacement.Text = oFila["dispMovilidad"].ToString();
            ExecuteReplace(fnd);
        }

        if (htCampos["observa"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueObserva"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            if (oFila["t001_cvobserva"].ToString().Length < 250) // Normal execution
            {
                fnd.Text = "{observa}";
                fnd.Replacement.Text = oFila["t001_cvobserva"].ToString();
                ExecuteReplace(fnd);
            }

            else
            {
                rng_datos.Select();
                object replaceAll = WdReplace.wdReplaceAll;
                object findMe = "{observa}";
                object replaceMe = oFila["t001_cvobserva"].ToString();
                wordApplication.Selection.Find.ClearFormatting();
                wordApplication.Selection.Find.Text = (string)findMe;
                wordApplication.Selection.Find.Replacement.ClearFormatting();
                wordApplication.Selection.Find.Execute(findMe);

                wordApplication.Selection.Text = (string)replaceMe;
                rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                rng_datos.Select();
            }
        }

        fnd.Text = "{foto}";
        fnd.Replacement.Text = "";
        ExecuteFind(fnd);
        rng_datos.Select();
        rng_datos.SetRange(rng_datos.Start, rng_datos.End);
        bool bHayFoto = false;
        if (oFila["t001_foto"] != DBNull.Value && htCampos["foto"].ToString() == "1")
        {
            bHayFoto = true;
            System.IO.File.WriteAllBytes(sPathDocsCV + "temp\\foto.jpg", (byte[])oFila["t001_foto"]);
        }
        if (bHayFoto)
        {
            Word.Selection currentSelection = wordApplication.Selection;
            currentSelection.MoveRight();
            Word.InlineShape img = currentSelection.InlineShapes.AddPicture(sPathDocsCV + "temp\\foto.jpg");
            img.Height = 70;
            img.Width = 70;
            System.IO.File.Delete(sPathDocsCV + "temp\\foto.jpg");
        }
        ExecuteReplace(fnd);


            }
       
    private static void CargarPieCVCCE(DataRow oFila, int i, int nDocs)
    {
        Word.Sections sections = newDocument.Sections;
        i = (nDocs == 0) ? i : 1;
        sections[i].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].LinkToPrevious = false;
        Word.Range footerRange = sections[i].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;

        footerRange.Font.Size = 8;
        footerRange.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
        footerRange.Text = @"Historial Profesional de " + oFila["Profesional"].ToString();
        sections[i].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].PageNumbers.RestartNumberingAtSection = true;
        footerRange.SetRange(footerRange.End - 1, footerRange.End - 1);
        footerRange.Select();
        wordApplication.Selection.TypeParagraph();

        int numPagsA = newDocument.ComputeStatistics(WdStatistic.wdStatisticPages, false);
        Object CurrentPage = WdFieldType.wdFieldPage;
        wordApplication.Selection.Fields.Add(wordApplication.Selection.Range, CurrentPage);
        wordApplication.Selection.TypeText("/" + (numPagsA - numPags));
        wordApplication.Selection.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        numPags = numPagsA; //guardamos el número de páginas total para poder calcular el número de páginas de cada cv.

    }

    private static void CargarSinopsisCVCCE(DataRow oFila )
    {
        if (oFila["T185_SINOPSIS"].ToString() == "" || htCampos["sinopsis"].ToString() == "0")
        {
            Word.Range rngS = newDocument.Bookmarks["MKBloqueSinopsis"].Range;
            rngS.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKSinopsis"].Range;
            rng_datos.Select();
            wordApplication.Selection.TypeText(oFila["T185_SINOPSIS"].ToString());
        }
    }

    #region Formación académica CVC CE

    private static void CargarFormacionAcademicaCVCCE(DataView dv)
    {
        if (dv.Count == 0 || htCampos["FA"].ToString() == "0")
        {
            Word.Range rngTC = newDocument.Bookmarks["MKBloqueFormacionAcademica"].Range;
            rngTC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKFormacionAcademica"].Range;
            Word.Range tblrng = rng_datos.Tables[1].Range;
            tblrng.Cut();


            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 0;
            foreach (DataRowView oFila in dv)
            {
                if (i > 0) wordApplication.Selection.TypeParagraph();
                wordApplication.Selection.Paste();
                ReemplazarDatosFACVCCE(newDocument.Bookmarks["MKFormacionAcademica"].Range, oFila);
                i++;
            }
            wordApplication.Selection.Delete();
        }
    }

    private static void ReemplazarDatosFACVCCE(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{titulo}";
        fnd.Replacement.Text = oFila["T019_DESCRIPCION"].ToString();
        ExecuteReplace(fnd);

        if (htCampos["FAtipo"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueFATipo"].Range;
            rng_datosN.Select();
            wordApplication.Selection.Delete();
            newDocument.Bookmarks["MKBloqueFATipo"].Delete();
            //wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{tipo}";
            fnd.Replacement.Text = oFila["T019_TIPO"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["FAmodalidad"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueFAModalidad"].Range;
            rng_datosN.Select();
            wordApplication.Selection.Delete();
            newDocument.Bookmarks["MKBloqueFAModalidad"].Delete();
        }
        else
        {
            fnd.Text = "{modalidad}";
            fnd.Replacement.Text = oFila["T019_MODALIDAD"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["FAtic"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueFATic"].Range;
            rng_datosN.Select();
            wordApplication.Selection.Delete();
            newDocument.Bookmarks["MKBloqueFATic"].Delete();
        }
        else
        {
            fnd.Text = "{tic}";
            fnd.Replacement.Text = oFila["T019_TIC"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["FAespecialidad"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueFAEspecialidad"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{especialidad}";
            fnd.Replacement.Text = oFila["T012_ESPECIALIDAD"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["FAcentro"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueFACentro"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{centro}";
            fnd.Replacement.Text = oFila["T012_CENTRO"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["FAfinicio"].ToString() == "0" && htCampos["FAffin"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueFecha"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{duracion}";
            if (htCampos["FAfinicio"].ToString() == "0")
                fnd.Replacement.Text = oFila["T012_FIN"].ToString();
            else if (htCampos["FAffin"].ToString() == "0")
                fnd.Replacement.Text = oFila["T012_INICIO"].ToString();
            else
                fnd.Replacement.Text = oFila["T012_INICIO"].ToString() + " - " + oFila["T012_FIN"].ToString();
            ExecuteReplace(fnd);
        }
        rng_idi.SetRange(rng_idi.End - 1, rng_idi.End - 1);
        rng_idi.Select();
    }

    #endregion

    #region Certificados
    private static void CargarCertificadosCVCCE(DataView dv)
    {
        if (dv.Count == 0 || htCampos["CERT"].ToString() == "0")
        {
            Word.Range rngTC = newDocument.Bookmarks["MKBloqueCertificaciones"].Range;
            rngTC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKCertificaciones"].Range;
            Word.Range tblrng = rng_datos.Tables[1].Range;
            tblrng.Cut();

            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 0;
            foreach (DataRowView oFila in dv)
            {
                if (i > 0) wordApplication.Selection.TypeParagraph();
                wordApplication.Selection.Paste();          //Pega la tabla
                ReemplazarDatosCertificadosCVCCE(newDocument.Bookmarks["MKCertificaciones"].Range, oFila);
                i++;
            }
            wordApplication.Selection.Delete();
        }
    }

    private static void ReemplazarDatosCertificadosCVCCE(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{titulo}";
        fnd.Replacement.Text = oFila["TITULO"].ToString();
        ExecuteReplace(fnd);
        if (htCampos["CTentidad"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCertEntidad"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{entidad}";
            fnd.Replacement.Text = oFila["PROVEEDOR"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["CTentorno"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCertEntorno"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{entorno}";
            fnd.Replacement.Text = oFila["ENTORNO"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["CTfobtencion"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCertFObtencion"].Range;
            rng_datosN.Select();
            wordApplication.Selection.Delete();
            newDocument.Bookmarks["MKBloqueCertFObtencion"].Delete();
            //wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{fObtencion}";
            fnd.Replacement.Text = oFila["FOBTENCION"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["CTfcaducidad"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCertFCaducidad"].Range;
            rng_datosN.Select();
            wordApplication.Selection.Delete();
            newDocument.Bookmarks["MKBloqueCertFCaducidad"].Delete();
            //wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{fCaducidad}";
            fnd.Replacement.Text = oFila["FCADUCIDAD"].ToString();
            ExecuteReplace(fnd);
        }
    }

    #endregion

    #region Experiencias profesionales
    private static void CargarExperienciasCVCCE(DataView dv, DataView dvP)
    {
        if (dv.Count == 0)
        {
            Word.Range rngC = newDocument.Bookmarks["MKBloqueExpProf"].Range;
            rngC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKExpProf"].Range;
            Word.Range tblrng = rng_datos.Tables[1].Range;
            tblrng.Copy();

            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 0;
            foreach (DataRowView oFila in dv)
            {
                if (i > 0)
                {
                    rng_datos = newDocument.Bookmarks["MKExpProf"].Range;
                    rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                    rng_datos.Select();
                    wordApplication.Selection.TypeParagraph();
                    wordApplication.Selection.Paste();
                }
                DataView perfil = new DataView(dvP.ToTable(), "t808_idexpprof= " + oFila["T808_IDEXPPROF"].ToString(), "FFIN, FINICIO", DataViewRowState.CurrentRows);
                if (oFila["TIPOEXP"].ToString() == "0")
                    ReemplazarDatosExperienciasIberCVCCE(newDocument.Bookmarks["MKExpProf"].Range, oFila, perfil, i + 1);
                else
                    ReemplazarDatosExperienciasFueraCVCCE(newDocument.Bookmarks["MKExpProf"].Range, oFila, perfil, i + 1);
                i++;
            }
            wordApplication.Selection.Delete();
        }
    }

    private static void ReemplazarDatosExperienciasIberCVCCE(Word.Range rng_exp, DataRowView oFila, DataView perfil, int nExp)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_exp.Find;
        if (htCampos["EPIfinicio"].ToString() == "0" && htCampos["EPIffin"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueExpFecha"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{fecha_exp}";
            if (htCampos["EPIfinicio"].ToString() == "0")
                fnd.Replacement.Text = oFila["FFIN"].ToString();
            else if (htCampos["EPIffin"].ToString() == "0")
                fnd.Replacement.Text = oFila["FINICIO"].ToString();
            else
                fnd.Replacement.Text = oFila["FINICIO"].ToString() + " - " + ((oFila["FFIN"].ToString() == "") ? "Actualidad" : oFila["FFIN"].ToString().ToString());
            ExecuteReplace(fnd);
        }
       
        fnd.Text = "{titulo_exp}";
        fnd.Replacement.Text = oFila["T808_DENOMINACION"].ToString();
        ExecuteReplace(fnd);

        if (htCampos["EPIdescripcion"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueExpDescripcion"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            if (oFila["T808_DESCRIPCION"].ToString().Length < 250) // Normal execution
            {
                fnd.Text = "{descripcion}";
                fnd.Replacement.Text = oFila["T808_DESCRIPCION"].ToString();
                ExecuteReplace(fnd);
            }
            else
            {
                rng_exp.Select();
                object replaceAll = WdReplace.wdReplaceAll;
                object findMe = "{descripcion}";
                object replaceMe = oFila["T808_DESCRIPCION"].ToString();
                wordApplication.Selection.Find.ClearFormatting();
                wordApplication.Selection.Find.Text = (string)findMe;
                wordApplication.Selection.Find.Replacement.ClearFormatting();
                wordApplication.Selection.Find.Execute(findMe);
                wordApplication.Selection.Text = (string)replaceMe;
                rng_exp.SetRange(rng_exp.End - 1, rng_exp.End - 1);
                rng_exp.Select();
            }
        }


        if (htCampos["EPIareacono"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueExpAreaCono"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{areaconocimiento}";
            fnd.Replacement.Text = oFila["SECTTEC"].ToString();
            ExecuteReplace(fnd);
        }


        if (htCampos["EPIcliente"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueExpCli"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{cliente}";
            fnd.Replacement.Text = oFila["CLIENTE"].ToString();
            ExecuteReplace(fnd);
        }

        if (htCampos["EPIsecsecc"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueExpSecSegC"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{secSegCli}";
            fnd.Replacement.Text = oFila["SECTORCLI"].ToString() + ((oFila["SECTORCLI"].ToString() != "" && oFila["SEGMENTOCLI"].ToString() != "") ? " - " : "") + oFila["SEGMENTOCLI"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["EPIempresa"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueExpEmpresa"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{empresaCon}";
            fnd.Replacement.Text = oFila["EMPRESACON"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["EPIsecsece"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueExpSecSegE"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{secSegEmC}";
            fnd.Replacement.Text = oFila["SECTOREMP"].ToString() + ((oFila["SECTOREMP"].ToString() != "" && oFila["SEGMENTOEMP"].ToString() != "") ? " - " : "") + oFila["SEGMENTOEMP"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["EPIperfil"].ToString() == "1")
            cumplimentarPerfilesExperienciasIberCVCCE(perfil, nExp);
        else{
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueExpPerfil"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
    }

    private static void ReemplazarDatosExperienciasFueraCVCCE(Word.Range rng_exp, DataRowView oFila, DataView perfil, int nExp)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_exp.Find;
        if (htCampos["EPFfinicio"].ToString() == "0" && htCampos["EPFffin"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueExpFecha"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{fecha_exp}";
            if (htCampos["EPFfinicio"].ToString() == "0")
                fnd.Replacement.Text = oFila["FFIN"].ToString();
            else if (htCampos["EPFffin"].ToString() == "0")
                fnd.Replacement.Text = oFila["FINICIO"].ToString();
            else
                fnd.Replacement.Text = oFila["FINICIO"].ToString() + " - " + ((oFila["FFIN"].ToString() == "") ? "Actualidad" : oFila["FFIN"].ToString().ToString());
            ExecuteReplace(fnd);
        }

        fnd.Text = "{titulo_exp}";
        fnd.Replacement.Text = oFila["T808_DENOMINACION"].ToString();
        ExecuteReplace(fnd);

        if (htCampos["EPFdescripcion"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueExpDescripcion"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            if (oFila["T808_DESCRIPCION"].ToString().Length < 250) // Normal execution
            {
                fnd.Text = "{descripcion}";
                fnd.Replacement.Text = oFila["T808_DESCRIPCION"].ToString();
                ExecuteReplace(fnd);
            }
            else
            {
                rng_exp.Select();
                object replaceAll = WdReplace.wdReplaceAll;
                object findMe = "{descripcion}";
                object replaceMe = oFila["T808_DESCRIPCION"].ToString();
                wordApplication.Selection.Find.ClearFormatting();
                wordApplication.Selection.Find.Text = (string)findMe;
                wordApplication.Selection.Find.Replacement.ClearFormatting();
                wordApplication.Selection.Find.Execute(findMe);
                wordApplication.Selection.Text = (string)replaceMe;
                rng_exp.SetRange(rng_exp.End - 1, rng_exp.End - 1);
                rng_exp.Select();
            }
        }


        if (htCampos["EPFareacono"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueExpAreaCono"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{areaconocimiento}";
            fnd.Replacement.Text = oFila["SECTTEC"].ToString();
            ExecuteReplace(fnd);
        }


        if (htCampos["EPFcliente"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueExpCli"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{cliente}";
            fnd.Replacement.Text = oFila["CLIENTE"].ToString();
            ExecuteReplace(fnd);
        }

        if (htCampos["EPFsecsecc"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueExpSecSegC"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{secSegCli}";
            fnd.Replacement.Text = oFila["SECTORCLI"].ToString() + ((oFila["SECTORCLI"].ToString() != "" && oFila["SEGMENTOCLI"].ToString() != "") ? " - " : "") + oFila["SEGMENTOCLI"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["EPFempresa"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueExpEmpresa"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{empresaCon}";
            fnd.Replacement.Text = oFila["EMPRESACON"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["EPFsecsece"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueExpSecSegE"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{secSegEmC}";
            fnd.Replacement.Text = oFila["SECTOREMP"].ToString() + ((oFila["SECTOREMP"].ToString() != "" && oFila["SEGMENTOEMP"].ToString() != "") ? " - " : "") + oFila["SEGMENTOEMP"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["EPFperfil"].ToString() == "1")
            cumplimentarPerfilesExperienciasFueraCVCCE(perfil, nExp);
        else
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueExpPerfil"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
    }

    private static void cumplimentarPerfilesExperienciasIberCVCCE(DataView perfiles, int nExp)
    {
        Word.Range rng_datos = newDocument.Bookmarks["MKExpProf"].Range;

        if (perfiles.Count == 0)
        {
            rng_datos.Delete();
        }
        else
        {
            int fila = rng_datos.Tables[nExp].Rows.Count;
            //int nExp = 1;
            foreach (DataRowView oFila in perfiles)
            {
                //fila perfil
                if (fila > rng_datos.Tables[nExp].Rows.Count) rng_datos.Tables[nExp].Rows.Add(); //dejamos la primera fila para los estilos y las columnas. Tenemos que hacerlo así porque no podemos copiar de dos marcadores y mantener lo que se copió la primera vez-
                Word.Row oRow = rng_datos.Tables[nExp].Rows[fila];
                oRow.Cells[2].Select();
                wordApplication.Selection.TypeText("Perfil");
                oRow.Cells[3].Select();
                wordApplication.Selection.TypeText(oFila["DESCRIPCION"].ToString());
                fila++;
                
                //fila fechas
                if (htCampos["EPIPfinicio"].ToString() == "1" || htCampos["EPIPffi"].ToString() == "1")
                {
                    rng_datos.Tables[nExp].Rows.Add();
                    oRow = rng_datos.Tables[nExp].Rows[fila];
                    oRow.Cells[2].Select();
                    if (htCampos["EPIPfinicio"].ToString() == "1" && htCampos["EPIPffi"].ToString() == "1")
                    {
                        wordApplication.Selection.TypeText("Fecha inicio y fin");
                        oRow.Cells[3].Select();
                        wordApplication.Selection.TypeText(oFila["FINICIO"].ToString() + " - " + ((oFila["FFIN"].ToString() == "") ? "Actualidad" : oFila["FFIN"].ToString()));
                    }
                    else if (htCampos["EPIPfinicio"].ToString() == "1")
                    {
                        wordApplication.Selection.TypeText("Fecha inicio");
                        oRow.Cells[3].Select();
                        wordApplication.Selection.TypeText(oFila["FINICIO"].ToString());

                    }
                    else if (htCampos["EPIPffi"].ToString() == "1")
                    {
                        wordApplication.Selection.TypeText("Fecha fin");
                        oRow.Cells[3].Select();
                        wordApplication.Selection.TypeText(((oFila["FFIN"].ToString() == "") ? "Actualidad" : oFila["FFIN"].ToString()));

                    }
                    fila++;
                }
                //fila funciones
                if (htCampos["EPIfuncion"].ToString() == "0")
                {
                    rng_datos.Tables[nExp].Rows.Add();
                    oRow = rng_datos.Tables[nExp].Rows[fila];
                    oRow.Cells[2].Select();
                    wordApplication.Selection.TypeText("Funciones");
                    oRow.Cells[3].Select();
                    wordApplication.Selection.TypeText(oFila["FUNCION"].ToString());
                    fila++;
                }
                //fila áreas y tecnologías
                if (htCampos["EPIareatec"].ToString() == "0")
                {
                    rng_datos.Tables[nExp].Rows.Add();
                    oRow = rng_datos.Tables[nExp].Rows[fila];
                    oRow.Cells[2].Select();
                    wordApplication.Selection.TypeText("Áreas  y Tecnologías");
                    oRow.Cells[3].Select();
                    wordApplication.Selection.TypeText(oFila["AREATEC"].ToString());
                    fila++;
                }
            }
        }

    }

    private static void cumplimentarPerfilesExperienciasFueraCVCCE(DataView perfiles, int nExp)
    {
        Word.Range rng_datos = newDocument.Bookmarks["MKExpProf"].Range;

        if (perfiles.Count == 0)
        {
            rng_datos.Delete();
        }
        else
        {
            int fila = rng_datos.Tables[nExp].Rows.Count;
            //int nExp = 1;
            foreach (DataRowView oFila in perfiles)
            {
                //fila perfil
                if (fila > rng_datos.Tables[nExp].Rows.Count) rng_datos.Tables[nExp].Rows.Add(); //dejamos la primera fila para los estilos y las columnas. Tenemos que hacerlo así porque no podemos copiar de dos marcadores y mantener lo que se copió la primera vez-
                Word.Row oRow = rng_datos.Tables[nExp].Rows[fila];
                oRow.Cells[2].Select();
                wordApplication.Selection.TypeText("Perfil");
                oRow.Cells[3].Select();
                wordApplication.Selection.TypeText(oFila["DESCRIPCION"].ToString());
                fila++;

                //fila fechas
                if (htCampos["EPFPfinicio"].ToString() == "1" || htCampos["EPFPffi"].ToString() == "1")
                {
                    rng_datos.Tables[nExp].Rows.Add();
                    oRow = rng_datos.Tables[nExp].Rows[fila];
                    oRow.Cells[2].Select();
                    if (htCampos["EPFPfinicio"].ToString() == "1" && htCampos["EPFPffi"].ToString() == "1")
                    {
                        wordApplication.Selection.TypeText("Fecha inicio y fin");
                        oRow.Cells[3].Select();
                        wordApplication.Selection.TypeText(oFila["FINICIO"].ToString() + " - " + ((oFila["FFIN"].ToString() == "") ? "Actualidad" : oFila["FFIN"].ToString()));
                    }
                    else if (htCampos["EPFPfinicio"].ToString() == "1")
                    {
                        wordApplication.Selection.TypeText("Fecha inicio");
                        oRow.Cells[3].Select();
                        wordApplication.Selection.TypeText(oFila["FINICIO"].ToString());

                    }
                    else if (htCampos["EPFPffi"].ToString() == "1")
                    {
                        wordApplication.Selection.TypeText("Fecha fin");
                        oRow.Cells[3].Select();
                        wordApplication.Selection.TypeText(((oFila["FFIN"].ToString() == "") ? "Actualidad" : oFila["FFIN"].ToString()));

                    }
                    fila++;
                }
                //fila funciones
                if (htCampos["EPFfuncion"].ToString() == "0")
                {
                    rng_datos.Tables[nExp].Rows.Add();
                    oRow = rng_datos.Tables[nExp].Rows[fila];
                    oRow.Cells[2].Select();
                    wordApplication.Selection.TypeText("Funciones");
                    oRow.Cells[3].Select();
                    wordApplication.Selection.TypeText(oFila["FUNCION"].ToString());
                    fila++;
                }
                //fila áreas y tecnologías
                if (htCampos["EPFareatec"].ToString() == "0")
                {
                    rng_datos.Tables[nExp].Rows.Add();
                    oRow = rng_datos.Tables[nExp].Rows[fila];
                    oRow.Cells[2].Select();
                    wordApplication.Selection.TypeText("Áreas  y Tecnologías");
                    oRow.Cells[3].Select();
                    wordApplication.Selection.TypeText(oFila["AREATEC"].ToString());
                    fila++;
                }
            }
        }

    }

    #endregion

    #region Idiomas
    private static void CargarIdiomasCVCCE(DataView dv)
    {
        if (dv.Count == 0)
        {
            Word.Range rngC = newDocument.Bookmarks["MKBloqueIdioma"].Range;
            rngC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKIdioma"].Range;
            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 2;
            foreach (DataRowView oFila in dv)
            {
                if (i != 2) rng_datos.Tables[1].Rows.Add(); //tenemos una fila como muestra para la letra y estilos
                ReemplazarDatosIdiomaCVCCE(rng_datos.Tables[1].Rows[i], oFila);
                i++;
            }
            if (htCampos["IDcentro"].ToString() == "0")
                rng_datos.Tables[1].Columns[7].Delete();
            if (htCampos["IDfecha"].ToString() == "0")
                rng_datos.Tables[1].Columns[6].Delete();
            if (htCampos["IDtitulo"].ToString() == "0")
                rng_datos.Tables[1].Columns[5].Delete();
            if (htCampos["IDescrito"].ToString() == "0")
                rng_datos.Tables[1].Columns[4].Delete();
            if (htCampos["IDoral"].ToString() == "0")
                rng_datos.Tables[1].Columns[3].Delete();
            if (htCampos["IDcomprension"].ToString() == "0")
                rng_datos.Tables[1].Columns[2].Delete();
        }
    }

    private static void ReemplazarDatosIdiomaCVCCE(Word.Row oRow, DataRowView oFila)
    {
        if (idIdioma == int.Parse(oFila["T020_IDCODIDIOMA"].ToString()) && htCampos["IDtitulo"].ToString() == "1")
        {
            oRow.Cells[5].Select();
            wordApplication.Selection.TypeText(oFila["T021_TITULO"].ToString());
            oRow.Cells[6].Select();
            wordApplication.Selection.TypeText(oFila["T021_FECHA"].ToString());
            oRow.Cells[7].Select();
            wordApplication.Selection.TypeText(oFila["T021_CENTRO"].ToString());
        }
        else
        {
            oRow.Cells[1].Select();
            wordApplication.Selection.TypeText(oFila["T020_DESCRIPCION"].ToString());
            oRow.Cells[2].Select();
            wordApplication.Selection.TypeText(oFila["T013_LECTURA"].ToString());
            oRow.Cells[3].Select();
            wordApplication.Selection.TypeText(oFila["T013_ORAL"].ToString());
            oRow.Cells[4].Select();
            wordApplication.Selection.TypeText(oFila["T013_ESCRITURA"].ToString());
            if (htCampos["IDtitulo"].ToString() == "1")
            {
                oRow.Cells[5].Select();
                wordApplication.Selection.TypeText(oFila["T021_TITULO"].ToString());
                oRow.Cells[6].Select();
                wordApplication.Selection.TypeText(oFila["T021_FECHA"].ToString());
                oRow.Cells[7].Select();
                wordApplication.Selection.TypeText(oFila["T021_CENTRO"].ToString());

            }
        }
        idIdioma = int.Parse(oFila["T020_IDCODIDIOMA"].ToString());
    }
    #endregion

    #region Cursos
    private static void CargarCursosCVCCE(DataView dvCursosImp, DataView dvCursosRec, DataView dvExamenes)
    {
        if ((dvCursosImp.Count == 0 || htCampos["CI"].ToString() == "0") && (dvCursosRec.Count == 0 || htCampos["CR"].ToString() == "0") && (dvExamenes.Count == 0 || htCampos["EXAM"].ToString() == "0"))
        {
            Word.Range rngFC = newDocument.Bookmarks["MKBloqueFormacionComp"].Range;
            rngFC.Delete();
        }
        else
        {
            if (dvCursosImp.Count == 0 || htCampos["CI"].ToString() == "0")
            {
                Word.Range rngCI = newDocument.Bookmarks["MKCursosImpartidos"].Range;
                rngCI.Delete();
            }
            else
            {
                Word.Range tblrng = newDocument.Bookmarks["MKCursosImpartidos"].Range.Tables[1].Range;
                tblrng.Cut();
                Word.Range rng_datos = newDocument.Bookmarks["MKCursosImpartidos"].Range;
                //rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                tblrng.Select();
                int iImp = 0;
                foreach (DataRowView oFila in dvCursosRec)
                {
                    if (iImp > 0) wordApplication.Selection.TypeParagraph();
                    wordApplication.Selection.Paste();          //Pega la tabla
                    ReemplazarDatosCursosICVCCE(newDocument.Bookmarks["MKCursosImpartidos"].Range, oFila);
                    iImp++;
                }
                wordApplication.Selection.Delete();
            }
            if (dvCursosRec.Count == 0 || htCampos["CR"].ToString() == "0")
            {
                Word.Range rngCR = newDocument.Bookmarks["MKCursosRecibidos"].Range;
                rngCR.Delete();
            }
            else
            {
                Word.Range tblrng = newDocument.Bookmarks["MKCursosRecibidos"].Range.Tables[1].Range;
                tblrng.Cut();

                Word.Range rng_datos = newDocument.Bookmarks["MKCursosRecibidos"].Range;
                rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                tblrng.Select();
                int iRec = 0;
                foreach (DataRowView oFila in dvCursosRec)
                {
                    if (iRec > 0) wordApplication.Selection.TypeParagraph();
                    wordApplication.Selection.Paste();          //Pega la tabla
                    ReemplazarDatosCursosRCVCCE(newDocument.Bookmarks["MKCursosRecibidos"].Range, oFila);
                    iRec++;
                }
                wordApplication.Selection.Delete();

            }
            if (dvExamenes.Count == 0 || htCampos["EXAM"].ToString() == "0")
            {
                Word.Range rngEx = newDocument.Bookmarks["MKCursosExamenes"].Range;
                rngEx.Delete();
            }
            else
            {
                Word.Range tblrng = newDocument.Bookmarks["MKCursosExamenes"].Range.Tables[1].Range;
                tblrng.Cut();

                Word.Range rng_datos = newDocument.Bookmarks["MKCursosExamenes"].Range;
                tblrng.SetRange(tblrng.End - 1, tblrng.End - 1);
                tblrng.Select();
                int iEx = 0;
                foreach (DataRowView oFila in dvExamenes)
                {
                    if (iEx > 0) wordApplication.Selection.TypeParagraph();
                    wordApplication.Selection.Paste();          //Pega la tabla
                    ReemplazarDatosExamenesCVCCE(newDocument.Bookmarks["MKCursosExamenes"].Range, oFila);
                    iEx++;
                }
            }

        }

    }

    private static void ReemplazarDatosCursosRCVCCE(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{titulo}";
        fnd.Replacement.Text = oFila["T574_TITULO"].ToString();
        ExecuteReplace(fnd);
        if (htCampos["CRtipo"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCurRecTipo"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{tipo}";
            fnd.Replacement.Text = oFila["TIPO"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["CRhoras"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCurRecHoras"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{horas}";
            fnd.Replacement.Text = oFila["T574_HORAS"].ToString();
            ExecuteReplace(fnd);
        }

        if (htCampos["CRfinicio"].ToString() == "0" && htCampos["CRffin"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCurRecFechas"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{duracion}";
            if (htCampos["CRfinicio"].ToString() == "0")
                fnd.Replacement.Text = oFila["FFIN"].ToString();
            else if (htCampos["CRffin"].ToString() == "0")
                fnd.Replacement.Text = oFila["FINICIO"].ToString();
            else
                fnd.Replacement.Text = oFila["FINICIO"].ToString() + " - " + oFila["FFIN"].ToString();
            ExecuteReplace(fnd);
        }

       
        if (htCampos["CRcentro"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCurRecCentro"].Range;
            rng_datosN.Select();
            wordApplication.Selection.Delete();
            newDocument.Bookmarks["MKBloqueCurRecCentro"].Delete();
            //wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{centro}";
            fnd.Replacement.Text = oFila["PROVEEDOR"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["CRprovincia"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCurRecProvin"].Range;
            rng_datosN.Select();
            wordApplication.Selection.Delete();
            newDocument.Bookmarks["MKBloqueCurRecProvin"].Delete();
            //wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{provincia}";
            fnd.Replacement.Text = oFila["PROVINCIA"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["CRentorno"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCurRecEntorno"].Range;
            rng_datosN.Select();
            wordApplication.Selection.Delete();
            newDocument.Bookmarks["MKBloqueCurRecEntorno"].Delete();
        }
        else
        {
            fnd.Text = "{entorno}";
            fnd.Replacement.Text = oFila["ENTORNO"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["CRmodalidad"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCurRecModalidad"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{modalidad}";
            fnd.Replacement.Text = oFila["MODALIDAD"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["CRcontenido"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCurRecContenido"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            if (oFila["T574_CONTENIDO"].ToString().Length < 250) // Normal execution
            {
                fnd.Text = "{contenido}";
                fnd.Replacement.Text = oFila["T574_CONTENIDO"].ToString();
                ExecuteReplace(fnd);
            }

            else  // Some real simple logic!!
            {
                rng_idi.Select();
                object replaceAll = WdReplace.wdReplaceAll;
                object findMe = "{contenido}";
                object replaceMe = oFila["T574_CONTENIDO"].ToString();
                wordApplication.Selection.Find.ClearFormatting();
                wordApplication.Selection.Find.Text = (string)findMe;
                wordApplication.Selection.Find.Replacement.ClearFormatting();
                wordApplication.Selection.Find.Execute(findMe);
                wordApplication.Selection.Text = (string)replaceMe;
            }
        }
        rng_idi.SetRange(rng_idi.End - 1, rng_idi.End - 1);
        rng_idi.Select();

    }
    
    private static void ReemplazarDatosCursosICVCCE(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{titulo}";
        fnd.Replacement.Text = oFila["T574_TITULO"].ToString();
        ExecuteReplace(fnd);
        if (htCampos["CItipo"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCurRecTipo"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{tipo}";
            fnd.Replacement.Text = oFila["TIPO"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["CIhoras"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCurRecHoras"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{horas}";
            fnd.Replacement.Text = oFila["T574_HORAS"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["CIfechas"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCurRecFechas"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{duracion}";
            fnd.Replacement.Text = oFila["FINICIO"].ToString() + " - " + oFila["FFIN"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["CIcentro"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCurRecCentro"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{centro}";
            fnd.Replacement.Text = oFila["PROVEEDOR"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["CIprovincia"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCurRecProvin"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{provincia}";
            fnd.Replacement.Text = oFila["PROVINCIA"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["CIentorno"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCurRecEntorno"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{entorno}";
            fnd.Replacement.Text = oFila["ENTORNO"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["CImodalidad"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCurRecModalidad"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{modalidad}";
            fnd.Replacement.Text = oFila["MODALIDAD"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["CIcontenido"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueCurRecContenido"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            if (oFila["T574_CONTENIDO"].ToString().Length < 250) // Normal execution
            {
                fnd.Text = "{contenido}";
                fnd.Replacement.Text = oFila["T574_CONTENIDO"].ToString();
                ExecuteReplace(fnd);
            }

            else  // Some real simple logic!!
            {
                rng_idi.Select();
                object replaceAll = WdReplace.wdReplaceAll;
                object findMe = "{contenido}";
                object replaceMe = oFila["T574_CONTENIDO"].ToString();
                wordApplication.Selection.Find.ClearFormatting();
                wordApplication.Selection.Find.Text = (string)findMe;
                wordApplication.Selection.Find.Replacement.ClearFormatting();
                wordApplication.Selection.Find.Execute(findMe);
                wordApplication.Selection.Text = (string)replaceMe;
                rng_idi.SetRange(rng_idi.End - 1, rng_idi.End - 1);
                rng_idi.Select();
            }
        }

    }

    #endregion

    #region Exámenes
    private static void ReemplazarDatosExamenesCVCCE(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{titulo}";
        fnd.Replacement.Text = oFila["TITULO"].ToString();
        ExecuteReplace(fnd);
        if (htCampos["EXentidad"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueFCExamEntidad"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{entidad}";
            fnd.Replacement.Text = oFila["PROVEEDOR"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["EXentorno"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueFCExamEntorno"].Range;
            rng_datosN.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            fnd.Text = "{entorno}";
            fnd.Replacement.Text = oFila["ENTORNO"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["EXfobtencion"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueFCExamFObtencion"].Range;
            rng_datosN.Select();
            wordApplication.Selection.Delete();
            newDocument.Bookmarks["MKBloqueFCExamFObtencion"].Delete();
        }
        else
        {
            fnd.Text = "{fObtencion}";
            fnd.Replacement.Text = oFila["FOBTENCION"].ToString();
            ExecuteReplace(fnd);
        }
        if (htCampos["EXfcaducidad"].ToString() == "0")
        {
            Word.Range rng_datosN = newDocument.Bookmarks["MKBloqueFCExamFCaducidad"].Range;
            rng_datosN.Select();
            wordApplication.Selection.Delete();
            newDocument.Bookmarks["MKBloqueFCExamFCaducidad"].Delete();
        }
        else
        {
            fnd.Text = "{fCaducidad}";
            fnd.Replacement.Text = oFila["FCADUCIDAD"].ToString();
            ExecuteReplace(fnd);
        }
        rng_idi.SetRange(rng_idi.End - 1, rng_idi.End - 1);
        rng_idi.Select();
    }
    #endregion

    #endregion PlantillaCVCompleto














    public static byte[] CargarCVMismoDoc(DataSet ds)
    {
        string documentFile = "";
        string sPathDocs = "d:\\tmp\\word\\";
        string sPathGuardar = "d:\\tmp\\word\\CV\\";
        byte[] result = null;
        try
        {
            numPags = 0;
            wordApplication = new Word.Application();
            wordApplication.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            wordApplication.Visible = true;
            // add a new document
            //switch plantilla
            Object oDoc = sPathDocs + @"Formato_CV_ofertas_AT_v4.doc";
            //Object oDoc = sPathDocs + @"Formato_CV_completo_formato_Word.doc";
            //Object oDoc = sPathDocs + @"Formato_CV_oferta_ppt_resumen.doc";
            //Object oDoc = sPathDocs + @"Formato_CV_oferta_ppt_completa.doc";
            //Object oDoc = sPathDocs + @"Europass_a_partir_de_informacion_de_CVT.doc";
            newDocument = wordApplication.Documents.Add(oDoc);
            for (int i = 0; i < ds.Tables["DatosPersonales"].Rows.Count; i++)
            {
                if (i != 0)
                {
                    object prueba = WdBreakType.wdSectionBreakNextPage;
                    wordApplication.Selection.InsertBreak(WdBreakType.wdSectionBreakNextPage);
                    //switch plantilla
                    wordApplication.Selection.InsertFile(sPathDocs + @"Formato_CV_ofertas_AT_v4.doc");
                    //wordApplication.Selection.InsertFile(sPathDocs + @"Formato_CV_completo_formato_Word.doc");
                    //wordApplication.Selection.InsertFile(sPathDocs + @"Formato_CV_oferta_ppt_resumen.doc");
                    //wordApplication.Selection.InsertFile(sPathDocs + @"Formato_CV_oferta_ppt_completa.doc");
                    //wordApplication.Selection.InsertFile(sPathDocs + @"Europass_a_partir_de_informacion_de_CVT.doc");
                }
                //switch plantilla
                CargarDatosAT(ds, i, 0);
                //CargarDatosCVC(ds, i, 0);
                //CargarDatosPPTR(ds, i, 0);
                //CargarDatosPPTC(ds, i, 0);
                //CargarDatosEP(ds, i, 0);
                Word.Range rng_datos = newDocument.Bookmarks["MKPlantilla"].Range;
                rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                rng_datos.Select();
                wordApplication.Selection.Delete();
                wordApplication.Selection.Delete();
                //Borrar marcadores
                while (newDocument.Bookmarks.Count != 0)
                {
                    newDocument.Bookmarks[1].Delete();
                }
            }
            // Guardar o abrir documento
            documentFile = string.Format("{0}Curriculums{1}", sPathGuardar, ".doc");
            wordApplication.ActiveWindow.View.Type = WdViewType.wdPrintView;

            double wordVersion = Convert.ToDouble(wordApplication.Version, CultureInfo.InvariantCulture);
            if (wordVersion >= 12.0)
                newDocument.SaveAs(documentFile, WdSaveFormat.wdFormatDocumentDefault);
            else
                newDocument.SaveAs(documentFile);
            ds.Dispose();
        }
        catch (Exception)
        {
            bErrores = true;
        }
        finally
        {
            // close word and dispose reference
            if (wordApplication != null)
            {
                wordApplication.Documents.Close(Word.Enums.WdSaveOptions.wdDoNotSaveChanges);
                wordApplication.Quit();
                wordApplication.Dispose();
            }
        }

        if (!bErrores)
        {
            result = System.IO.File.ReadAllBytes(documentFile.ToString());
            //result = FileToByteArray(documentFile);
            string[] files = Directory.GetFiles(sPathGuardar);
            foreach (string file in files)
            {
                File.Delete(file);
            }
        }
        return result;
    }

    public static byte[] CargarCVDifDoc(DataSet ds)
    {
        string sPathDocs = "d:\\tmp\\word\\";
        string sPathGuardar = "d:\\tmp\\word\\CV\\";
        string documentFile = "";
        wordApplication = new Word.Application();

        //wordApplication.DisplayAlerts = WdAlertLevel.wdAlertsNone;
        //wordApplication.Visible = true;
        // add a new document
        //Object oDoc = sPathDocs + @"Formato_CV_ofertas_AT_v4.doc";
        //Object oDoc = sPathDocs + @"Formato_CV_completo_formato_Word.doc";
        Object oDoc = sPathDocs + @"Formato_CV_oferta_ppt_resumen.doc";
        //Object oDoc = sPathDocs + @"Formato_CV_oferta_ppt_completa.doc";
        //Object oDoc = sPathDocs + @"Europass_a_partir_de_informacion_de_CVT.doc";
        for (int i = 0; i < ds.Tables["DatosPersonales"].Rows.Count; i++)
        {
            try
            {
                numPags = 0;
                newDocument = wordApplication.Documents.Add(oDoc);
                //CargarDatosAT(ds, i, 1);
                //CargarDatosCVC(ds, i, 1);
                //CargarDatosPPTR(ds, i, 1);
                //CargarDatosPPTC(ds, i, 1);
                CargarDatosEP(ds, i, 1);
                Word.Range rng_datos = newDocument.Bookmarks["MKPlantilla"].Range;
                rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                rng_datos.Select();
                //Borrar marcadores
                while (newDocument.Bookmarks.Count != 0)
                {
                    newDocument.Bookmarks[1].Delete();
                }
                // Guardar o abrir documento
                documentFile = string.Format("{0}" + ds.Tables["DatosPersonales"].Rows[i]["Profesional"].ToString() + "{1}", sPathGuardar, ".doc");
                wordApplication.ActiveWindow.View.Type = WdViewType.wdPrintView;
                double wordVersion = Convert.ToDouble(wordApplication.Version, CultureInfo.InvariantCulture);
                if (wordVersion >= 12.0)
                    newDocument.SaveAs(documentFile, WdSaveFormat.wdFormatDocumentDefault);
                else
                    newDocument.SaveAs(documentFile);
            }
            catch (Exception)
            {
                bErrores = true;
            }

        }

        if (wordApplication != null)
        {
            wordApplication.Documents.Close(Word.Enums.WdSaveOptions.wdDoNotSaveChanges);
            wordApplication.Quit();
            wordApplication.Dispose();
        }
        ds.Dispose();
        //comprimimos la carpeta que contiene los CV y devolvemos el resultado (en byte[])
        return CompressFile(sPathGuardar);
    }
    
    #region Plantilla europass

    public static void CargarDatosEP(DataSet ds, int i, int nDocs)
    {
        CargarDatosPersonalesEP(ds.Tables["DatosPersonales"].Rows[i]);

        //SINOPSIS
        CargarSinopsisEP(ds.Tables["DatosPersonales"].Rows[i]);

        //Formación academica
        DataView dvFA = new DataView(ds.Tables["FormacionAcademica"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarFormacionAcademicaEP(dvFA);

        //Certificados
        DataView dvCER = new DataView(ds.Tables["Certificados"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarCertificadosEP(dvCER);

        //Experiencias
        DataView dvEXP = new DataView(ds.Tables["Experiencia"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        DataView dvEXPPER = new DataView(ds.Tables["PerfilExperiencia"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarExperienciasEP(dvEXP, dvEXPPER);

        //Idiomas
        DataView dvIDI = new DataView(ds.Tables["Idiomas"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarIdiomasEP(dvIDI);

        //Cursos
        DataView dvCurRec = new DataView(ds.Tables["CursosRecibidos"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarCursosREP(dvCurRec);

        DataView dvCurImp = new DataView(ds.Tables["CursosImpartidos"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarCursosIEP(dvCurImp);

        //Para el pie de página
        CargarPieEP(ds.Tables["DatosPersonales"].Rows[i], i + 1, nDocs); //a la i le sumamos 1 ya que los indices van de 1 a n (no de 0 a n-1)


    }

    public static void CargarDatosPersonalesEP(DataRow oFila)
    {
        string sPathDocs = "d:\\tmp\\word\\";

        Word.Range rng_datos = newDocument.Bookmarks["MKDatosPersonales"].Range;
        Word.Find fnd = rng_datos.Find; //rango dónde buscar

        fnd.Text = "{nombre}";
        fnd.Replacement.Text = oFila["Profesional"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{nif}";
        fnd.Replacement.Text = oFila["t001_cip"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{sexo}";
        fnd.Replacement.Text = oFila["sexo"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{nacionalidad}";
        fnd.Replacement.Text = oFila["T001_NACIONALID"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{perfil}";
        fnd.Replacement.Text = oFila["t035_descripcion"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{fNacimiento}";
        fnd.Replacement.Text = (oFila["t001_fecnacim"].ToString() != "") ? ((DateTime)oFila["t001_fecnacim"]).ToShortDateString() : "";
        ExecuteReplace(fnd);

        fnd.Text = "{FOTO}";
        fnd.Replacement.Text = "";
        ExecuteFind(fnd);
        rng_datos.Select();
        rng_datos.SetRange(rng_datos.Start, rng_datos.End);

        bool bHayFoto = false;
        if (oFila["t001_foto"] != DBNull.Value)
        {
            bHayFoto = true;
            System.IO.File.WriteAllBytes(sPathDocs + "..\\temp\\foto.jpg", (byte[])oFila["t001_foto"]);
        }

        if (bHayFoto)
        {
            Word.Selection currentSelection = wordApplication.Selection;
            currentSelection.MoveRight();
            Word.InlineShape img = currentSelection.InlineShapes.AddPicture(sPathDocs + "..\\temp\\foto.jpg");
            img.Height = tamanoImg(img);
            img.Width = widthImg;
            System.IO.File.Delete(sPathDocs + "..\\temp\\foto.jpg");
        }
        ExecuteReplace(fnd);
    }

    public static void CargarPieEP(DataRow oFila, int i, int nDocs)
    {
        Word.Sections sections = newDocument.Sections;
        i = (nDocs == 0) ? i : 1;
        sections[i].Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].LinkToPrevious = false;
        Word.Range headerRange = sections[i].Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
        Word.Find fnd = headerRange.Find;

        fnd.Text = "{profesional}";
        fnd.Replacement.Text = oFila["Profesional"].ToString();
        ExecuteReplace(fnd);

        Word.Range footerRange = sections[i].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;

        sections[i].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].PageNumbers.RestartNumberingAtSection = true;
        footerRange.SetRange(footerRange.End - 1, footerRange.End - 1);
        footerRange.Select();
        wordApplication.Selection.TypeText("Página ");
        int numPagsA = newDocument.ComputeStatistics(WdStatistic.wdStatisticPages, false);
        Object CurrentPage = WdFieldType.wdFieldPage;
        wordApplication.Selection.Fields.Add(wordApplication.Selection.Range, CurrentPage);
        wordApplication.Selection.TypeText("/" + (numPagsA - numPags));
        wordApplication.Selection.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
        numPags = numPagsA; //guardamos el número de páginas total para poder calcular el número de páginas de cada cv.

    }

    public static void CargarSinopsisEP(DataRow oFila)
    {
        Word.Range rng_datos = newDocument.Bookmarks["MKSinopsis"].Range;
        rng_datos.Select();
        wordApplication.Selection.TypeText(oFila["T185_SINOPSIS"].ToString());
    }

    #region Formación académicaEP

    public static void CargarFormacionAcademicaEP(DataView dv)
    {
        if (dv.Count == 0)
        {
            Word.Range rngTC = newDocument.Bookmarks["MKFormacionAcademica"].Range;
            rngTC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKFormacionAcademica"].Range;
            Word.Range tblrng = rng_datos.Tables[1].Range;
            tblrng.Cut();

            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();

            foreach (DataRowView oFila in dv)
            {
                wordApplication.Selection.Paste();
                ReemplazarDatosFAEP(newDocument.Bookmarks["MKFormacionAcademica"].Range, oFila);
            }
        }
    }

    public static void ReemplazarDatosFAEP(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{duracion}";
        fnd.Replacement.Text = oFila["T012_INICIO"].ToString() + " - " + oFila["T012_FIN"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{t019_titulo}";
        fnd.Replacement.Text = oFila["T019_DESCRIPCION"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{t012_centro}";
        fnd.Replacement.Text = oFila["T012_CENTRO"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{t019_tipo}";
        fnd.Replacement.Text = oFila["T019_TIPO"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{t012_especialidad}";
        fnd.Replacement.Text = oFila["T012_ESPECIALIDAD"].ToString();
        ExecuteReplace(fnd);
    }

    #endregion

    #region Certificados
    public static void CargarCertificadosEP(DataView dv)
    {
        if (dv.Count == 0)
        {
            Word.Range rngTC = newDocument.Bookmarks["MKCertificaciones"].Range;
            rngTC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKCertificaciones"].Range;
            Word.Range tblrng = rng_datos.Tables[1].Range;
            tblrng.Cut();

            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();

            foreach (DataRowView oFila in dv)
            {
                wordApplication.Selection.Paste();          //Pega la tabla
                ReemplazarDatosCertificadosEP(newDocument.Bookmarks["MKCertificaciones"].Range, oFila);
            }
        }
    }

    public static void ReemplazarDatosCertificadosEP(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{fObtencion}";
        fnd.Replacement.Text = oFila["FOBTENCION"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{titulo}";
        fnd.Replacement.Text = oFila["TITULO"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{Proveedor}";
        fnd.Replacement.Text = oFila["PROVEEDOR"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{t036_descripcion}";
        fnd.Replacement.Text = oFila["T036_DESCRIPCION"].ToString();
        ExecuteReplace(fnd);
    }

    #endregion

    #region Experiencias profesionales
 
    public static void CargarExperienciasEP(DataView dvE, DataView dvP)
    {
        if (dvE.Count == 0)
        {
            Word.Range rngC = newDocument.Bookmarks["MKBloqueExpProf"].Range;
            rngC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKExpProf"].Range;
            Word.Range tblrng = rng_datos.Tables[1].Range;
            tblrng.Cut();

            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 0;
            foreach (DataRowView oFila in dvE)
            {
                if (i > 0)
                {
                    rng_datos = newDocument.Bookmarks["MKExpProf"].Range;
                    rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                    rng_datos.Select();
                }
                wordApplication.Selection.Paste();
                DataView perfil = new DataView(dvP.ToTable(), "t808_idexpprof= " + oFila["T808_IDEXPPROF"].ToString(), "FFIN, FINICIO", DataViewRowState.CurrentRows);
                ReemplazarDatosExperienciasEP(newDocument.Bookmarks["MKExpProf"].Range, oFila, perfil);
                i++;
            }
        }
    }

    public static void ReemplazarDatosExperienciasEP(Word.Range rng_exp, DataRowView oFila, DataView perfil)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_exp.Find;

        fnd.Text = "{fecha_exp}";
        fnd.Replacement.Text = oFila["FINICIO"].ToString() + "-" + ((oFila["FFIN"].ToString() == "") ? "Hoy" : oFila["FFIN"].ToString());
        ExecuteReplace(fnd);

        if (oFila["PERFILES"].ToString().Length < 250) // Normal execution
        {
            fnd.Text = "{perfil_exp}";
            fnd.Replacement.Text = oFila["PERFILES"].ToString();
            ExecuteReplace(fnd);
        }

        else
        {
            rng_exp.Select();
            object replaceAll = WdReplace.wdReplaceAll;
            object findMe = "{perfil_exp}";
            object replaceMe = oFila["PERFILES"].ToString();
            wordApplication.Selection.Find.ClearFormatting();
            wordApplication.Selection.Find.Text = (string)findMe;
            wordApplication.Selection.Find.Replacement.ClearFormatting();
            wordApplication.Selection.Find.Execute(findMe);

            wordApplication.Selection.Text = (string)replaceMe;
            rng_exp.SetRange(rng_exp.End - 1, rng_exp.End - 1);
            rng_exp.Select();
        }

        fnd.Text = "{empresa}";
        fnd.Replacement.Text = oFila["EMPRESA"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{numProyecto}";
        fnd.Replacement.Text = oFila["numProy"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{t808_denominacion}";
        fnd.Replacement.Text = oFila["t808_denominacion"].ToString();
        ExecuteReplace(fnd);

        if (oFila["t808_descripcion"].ToString().Length < 250) // Normal execution
        {
            fnd.Text = "{t808_descripcion}";
            fnd.Replacement.Text = oFila["t808_descripcion"].ToString();
            ExecuteReplace(fnd);
        }

        else
        {
            rng_exp.Select();
            object replaceAll = WdReplace.wdReplaceAll;
            object findMe = "{t808_descripcion}";
            object replaceMe = oFila["t808_descripcion"].ToString();
            wordApplication.Selection.Find.ClearFormatting();
            wordApplication.Selection.Find.Text = (string)findMe;
            wordApplication.Selection.Find.Replacement.ClearFormatting();
            wordApplication.Selection.Find.Execute(findMe);

            wordApplication.Selection.Text = (string)replaceMe;
            rng_exp.SetRange(rng_exp.End - 1, rng_exp.End - 1);
            rng_exp.Select();
        }

        cumplimentarPerfilesExperienciasEP(perfil);

        fnd.Text = "{cliente}";
        fnd.Replacement.Text = oFila["CLIENTE"].ToString();
        ExecuteReplace(fnd);

        if (oFila["AREATEC"].ToString().Length < 250) // Normal execution
        {
            fnd.Text = "{tecnologias_exp}";
            fnd.Replacement.Text = oFila["AREATEC"].ToString();
            ExecuteReplace(fnd);
        }

        else
        {
            rng_exp.Select();
            object replaceAll = WdReplace.wdReplaceAll;
            object findMe = "{tecnologias_exp}";
            object replaceMe = oFila["AREATEC"].ToString();
            wordApplication.Selection.Find.ClearFormatting();
            wordApplication.Selection.Find.Text = (string)findMe;
            wordApplication.Selection.Find.Replacement.ClearFormatting();
            wordApplication.Selection.Find.Execute(findMe);

            wordApplication.Selection.Text = (string)replaceMe;
            rng_exp.SetRange(rng_exp.End - 1, rng_exp.End - 1);
            rng_exp.Select();
        }

        fnd.Text = "{sectorSeg}";
        fnd.Replacement.Text = oFila["SECTORCLI"].ToString() + ((oFila["SECTORCLI"].ToString() != "" && oFila["SEGMENTOCLI"].ToString() != "") ? " - " : "") + oFila["SEGMENTOCLI"].ToString();
        ExecuteReplace(fnd);
    }

    public static void cumplimentarPerfilesExperienciasEP(DataView perfiles)
    {
        Word.Range rng_datos = newDocument.Bookmarks["MKPerfil"].Range;

        if (perfiles.Count == 0)
        {
            rng_datos.Delete();
        }
        else
        {
            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 0;
            foreach (DataRowView oFila in perfiles)
            {
                if (i > 0) { wordApplication.Selection.TypeParagraph(); wordApplication.Selection.TypeParagraph(); }
                if (perfiles.Count > 1)
                {
                    wordApplication.Selection.TypeText(oFila["DESCRIPCION"].ToString().ToUpper());
                    wordApplication.Selection.TypeParagraph();
                }
                wordApplication.Selection.TypeText(oFila["FUNCION"].ToString());
                i++;
            }
        }
        newDocument.Bookmarks["MKPerfil"].Delete();
    }
    #endregion

    #region Idiomas
    public static void CargarIdiomasEP(DataView dv)
    {
        if (dv.Count == 0)
        {
            Word.Range rngC = newDocument.Bookmarks["MKBloqueIdioma"].Range;
            rngC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKIdioma"].Range;
            Word.Range tbl_datos = rng_datos.Tables[1].Range;
            rng_datos.SetRange(rng_datos.Start, rng_datos.End - 6);
            rng_datos.Select();
            rng_datos.Copy();
            int i = 5;
            foreach (DataRowView oFila in dv)
            {
                if (i != 5)
                {
                    rng_datos = newDocument.Bookmarks["MKIdioma"].Range;
                    rng_datos.SetRange(rng_datos.End - 6, rng_datos.End - 6);
                    rng_datos.Select();
                    wordApplication.Selection.Paste(); 
                }
                ReemplazarDatosIdiomaEP(newDocument.Bookmarks["MKIdioma"].Range, oFila);
                i++;
            }
            rng_datos = newDocument.Bookmarks["MKIdioma"].Range;
            rng_datos.SetRange(rng_datos.End - 5, rng_datos.End - 5);
            rng_datos.Select();
            wordApplication.Selection.TypeBackspace();
        }
    }

    public static void ReemplazarDatosIdiomaEP(Word.Range rng_datos, DataRowView oFila)
    {
        Word.Find fnd = rng_datos.Find;

        fnd.Text = "{t020_descripcion}";
        fnd.Replacement.Text = oFila["T020_DESCRIPCION"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{t013_lectura}";
        fnd.Replacement.Text = oFila["T013_LECTURA"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{t013_escritura}";
        fnd.Replacement.Text = oFila["T013_ESCRITURA"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{t013_oral}";
        fnd.Replacement.Text = oFila["T013_ORAL"].ToString();
        ExecuteReplace(fnd);

        if (oFila["T021_TITULO"].ToString() != "")
        {
            fnd.Text = "{t021_titulo}";
            fnd.Replacement.Text = oFila["T021_TITULO"].ToString();
            ExecuteReplace(fnd);
        }
        else
        {
            rng_datos.SetRange(rng_datos.End - 22, rng_datos.End - 6);
            rng_datos.Select();
            wordApplication.Selection.TypeBackspace();
        }
        //oRow.Cells[1].Select();
        //wordApplication.Selection.TypeText(oFila["T020_DESCRIPCION"].ToString());
        //oRow.Cells[2].Select();
        //wordApplication.Selection.TypeText(oFila["T013_LECTURA"].ToString());
        //oRow.Cells[3].Select();
        //wordApplication.Selection.TypeText(oFila["T013_ORAL"].ToString());
        //oRow.Cells[4].Select();
        //wordApplication.Selection.TypeText(oFila["T013_ESCRITURA"].ToString());
        //oRow.Cells[5].Select();
        //wordApplication.Selection.TypeText(oFila["T021_TITULO"].ToString());
    }
    #endregion

    #region Cursos
    public static void CargarCursosREP(DataView dvCursosRec)
    {
        if (dvCursosRec.Count == 0)
        {
            Word.Range rngCR = newDocument.Bookmarks["MKCursosRecibidos"].Range;
            rngCR.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKCursosRecibidos"].Range;
            Word.Range tblrng = rng_datos.Tables[1].Range;
            tblrng.Cut();

            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            tblrng.Select();

            foreach (DataRowView oFila in dvCursosRec)
            {
                wordApplication.Selection.Paste();          //Pega la tabla
                ReemplazarDatosCursosREP(newDocument.Bookmarks["MKCursosRecibidos"].Range, oFila);
            }
        }
    }

    public static void ReemplazarDatosCursosREP(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{t574_ffin}";
        fnd.Replacement.Text = oFila["FFIN"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{t574_horas}";
        fnd.Replacement.Text = oFila["T574_HORAS"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{t574_titulo}";
        fnd.Replacement.Text = oFila["T574_TITULO"].ToString();
        ExecuteReplace(fnd);
        
        fnd.Text = "{Proveedor}";
        fnd.Replacement.Text = oFila["PROVEEDOR"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{t036_descripcion}";
        fnd.Replacement.Text = oFila["T036_DESCRIPCION"].ToString();
        ExecuteReplace(fnd);
    }

    public static void CargarCursosIEP(DataView dvCursosImp)
    {
        if (dvCursosImp.Count == 0)
        {
            Word.Range rng_BCI = newDocument.Bookmarks["MKBloqueCursosImpartidos"].Range;
            rng_BCI.Select();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            int i = 1;
            Word.Range rng_datos = newDocument.Bookmarks["MKCursosImpartidos"].Range;
            foreach (DataRowView oFila in dvCursosImp)
            {
                if (i != 1)
                    rng_datos.Tables[1].Rows.Add();
                rng_datos.Tables[1].Rows[i].Cells[2].Select();
                wordApplication.Selection.TypeText(oFila["t574_titulo"].ToString());
                i++;
            }
        }
    }
    #endregion

    #endregion Plantilla europass

 

    #region Plantilla oferta ppt completo
    public static void CargarDatosPPTC(DataSet ds, int i, int nDocs)
    {
        CargarDatosPersonalesPPTC(ds.Tables["DatosPersonales"].Rows[i]);

        //SINOPSIS
        CargarSinopsisPPTC(ds.Tables["DatosPersonales"].Rows[i]);

        //Formación academica
        DataView dvFA = new DataView(ds.Tables["FormacionAcademica"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarFormacionAcademicaPPTC(dvFA);

        //Certificados
        DataView dvCER = new DataView(ds.Tables["Certificados"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarCertificadosPPTC(dvCER);

        //Experiencias
        DataView dvEXP = new DataView(ds.Tables["Experiencia"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        DataView dvEXPPER = new DataView(ds.Tables["PerfilExperiencia"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarExperienciasPPTC(dvEXP, dvEXPPER);

        //Idiomas
        DataView dvIDI = new DataView(ds.Tables["Idiomas"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarIdiomasPPTC(dvIDI);

        //Cursos
        DataView dvCurRec = new DataView(ds.Tables["CursosRecibidos"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        DataView dvCurImp = new DataView(ds.Tables["CursosImpartidos"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarCursosPPTC(dvCurImp, dvCurRec);

        //Para el pie de página
        CargarPiePPTC(ds.Tables["DatosPersonales"].Rows[i], i + 1, nDocs); //a la i le sumamos 1 ya que los indices van de 1 a n (no de 0 a n-1)


    }

    public static void CargarDatosPersonalesPPTC(DataRow oFila)
    {
        string sPathDocs = "d:\\tmp\\word\\";
        Word.Range rng_datos = newDocument.Bookmarks["MKDatosPersonales"].Range;
        Word.Find fnd = rng_datos.Find; //rango dónde buscar

        fnd.Text = "{nombre}";
        fnd.Replacement.Text = oFila["Profesional"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{nif}";
        fnd.Replacement.Text = oFila["t001_cip"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{perfil}";
        fnd.Replacement.Text = oFila["t035_descripcion"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{fNacimiento}";
        fnd.Replacement.Text = (oFila["t001_fecnacim"].ToString() != "") ? ((DateTime)oFila["t001_fecnacim"]).ToShortDateString() : "";
        ExecuteReplace(fnd);
        
        fnd.Text = "{a_exp}";
        fnd.Replacement.Text = "FALTA AÑO DE EXPERIENCIA";
        ExecuteReplace(fnd);
        
        fnd.Text = "{foto}";
        fnd.Replacement.Text = "";
        ExecuteFind(fnd);
        rng_datos.Select();
        rng_datos.SetRange(rng_datos.Start, rng_datos.End);

        bool bHayFoto = false;
        if (oFila["t001_foto"] != DBNull.Value)
        {
            bHayFoto = true;
            System.IO.File.WriteAllBytes(sPathDocs + "..\\temp\\foto.jpg", (byte[])oFila["t001_foto"]);
        }

        if (bHayFoto)
        {
            Word.Selection currentSelection = wordApplication.Selection;
            currentSelection.MoveRight();
            Word.InlineShape img = currentSelection.InlineShapes.AddPicture(sPathDocs + "..\\temp\\foto.jpg");
            img.Height = 70;
            img.Width = 70;
            System.IO.File.Delete(sPathDocs + "..\\temp\\foto.jpg");
        }
        ExecuteReplace(fnd);
    }

    public static void CargarSinopsisPPTC(DataRow oFila)
    {
        if (oFila["T185_SINOPSIS"].ToString() == "")
        {
            Word.Range rngS = newDocument.Bookmarks["MKBloqueSinopsis"].Range;
            rngS.Delete();
            wordApplication.Selection.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKSinopsis"].Range;
            rng_datos.Select();
            wordApplication.Selection.TypeText(oFila["T185_SINOPSIS"].ToString());
        }

    }

    public static void CargarPiePPTC(DataRow oFila, int i, int nDocs)
    {
        Word.Section section = newDocument.Sections[i];
        Word.Range footerRange = section.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
        Word.Find fnd = footerRange.Find;

        fnd.Text = "{fecha}";
        fnd.Replacement.Text = DateTime.Now.ToShortDateString();
        ExecuteReplace(fnd);
    }

    #region Formación académica PPTC

    public static void CargarFormacionAcademicaPPTC(DataView dv)
    {
        Word.Range rng_datos = newDocument.Bookmarks["MKBloqueFormacionAcademica"].Range;
        rng_datos.Select();
        if (dv.Count == 0)
        {
            wordApplication.Selection.TypeBackspace();
            wordApplication.Selection.Delete();
        }
        else
        {
           
            int i = 1;
            foreach (DataRowView oFila in dv)
            {
                if (i != 1) rng_datos.Tables[1].Rows.Add(); //tenemos una fila como muestra para la letra y estilos
                ReemplazarDatosFAPPTC(rng_datos.Cells, oFila);
                i++;
            }
        }
    }

    public static void ReemplazarDatosFAPPTC(Word.Cells oCells, DataRowView oFila)
    {
        oCells[2].Select();
        wordApplication.Selection.TypeText(oFila["T012_INICIO"].ToString() + " - " + oFila["T012_FIN"].ToString());
        oCells[3].Select();
        wordApplication.Selection.TypeText(oFila["T019_DESCRIPCION"].ToString());
        oCells[4].Select();
        wordApplication.Selection.TypeText(oFila["T012_CENTRO"].ToString());
    }

    #endregion

    #region Certificados
    public static void CargarCertificadosPPTC(DataView dv)
    {
        Word.Range rng_datos = newDocument.Bookmarks["MKBloqueCertificaciones"].Range;
        if (dv.Count == 0)
        {
            rng_datos.Select();
            wordApplication.Selection.TypeBackspace();
            wordApplication.Selection.Delete();
        }
        else
        {
            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 1;
            foreach (DataRowView oFila in dv)
            {
                if (i != 1) rng_datos.Tables[1].Rows.Add(); //tenemos una fila como muestra para la letra y estilos
                ReemplazarDatosCertificadosPPTC(rng_datos.Tables[1].Rows[i].Cells, oFila);
                i++;
            }

        }
    }

    public static void ReemplazarDatosCertificadosPPTC(Word.Cells oCells, DataRowView oFila)
    {
        oCells[2].Select();
        wordApplication.Selection.TypeText(oFila["FOBTENCION"].ToString());
        oCells[3].Select();
        wordApplication.Selection.TypeText(oFila["TITULO"].ToString());
        oCells[5].Select();
        wordApplication.Selection.TypeText(oFila["PROVEEDOR"].ToString());
        oCells[6].Select();
        wordApplication.Selection.TypeText(oFila["ENTORNO"].ToString());
    }

    #endregion

    #region Experiencias profesionales
    public static void CargarExperienciasPPTC(DataView dv,DataView dvP)
    {
        if (dv.Count == 0)
        {
            Word.Range rngC = newDocument.Bookmarks["MKBloqueExpProf"].Range;
            rngC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKExpProf"].Range;
            rng_datos.Cut();
            //rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 0;
            foreach (DataRowView oFila in dv)
            {
                wordApplication.Selection.Paste();
                DataView perfiles = new DataView(dvP.ToTable(), "t808_idexpprof= " + oFila["T808_IDEXPPROF"].ToString(), "FFIN, FINICIO", DataViewRowState.CurrentRows);
                ReemplazarDatosExperienciasPPTC(newDocument.Bookmarks["MKExpProf"].Range, oFila, perfiles);
                rng_datos = newDocument.Bookmarks["MKExpProf"].Range;
                rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                rng_datos.Select();
                if (i == 0) wordApplication.Selection.TypeParagraph();
                i++;
            }
            wordApplication.Selection.Delete();
            wordApplication.Selection.Delete();
            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            wordApplication.Selection.Font.Size = 16;
        }
    }

    public static void ReemplazarDatosExperienciasPPTC(Word.Range rng_exp, DataRowView oFila, DataView perfiles)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_exp.Find;

        fnd.Text = "{duracion}";
        fnd.Replacement.Text = oFila["FINICIO"].ToString() + " - " + ((oFila["FFIN"].ToString() == "") ? "Actualidad" : oFila["FFIN"].ToString());
        ExecuteReplace(fnd);

        fnd.Text = "{cliente}";
        fnd.Replacement.Text = oFila["CLIENTE"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{t808_denominacion}";
        fnd.Replacement.Text = oFila["T808_DENOMINACION"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{sector-seg-cli}";
        fnd.Replacement.Text = oFila["SECTORCLI"].ToString() + ((oFila["SECTORCLI"].ToString() != "" && oFila["SEGMENTOCLI"].ToString() != "") ? " - " : "") + oFila["SEGMENTOCLI"].ToString();
        ExecuteReplace(fnd);

        if (oFila["T808_DESCRIPCION"].ToString().Length < 250) // Normal execution
        {
            fnd.Text = "{t808_descripcion}";
            fnd.Replacement.Text = oFila["T808_DESCRIPCION"].ToString();
            ExecuteReplace(fnd);
        }
        else
        {
            rng_exp.Select();
            object replaceAll = WdReplace.wdReplaceAll;
            object findMe = "{t808_descripcion}";
            object replaceMe = oFila["T808_DESCRIPCION"].ToString();
            wordApplication.Selection.Find.ClearFormatting();
            wordApplication.Selection.Find.Text = (string)findMe;
            wordApplication.Selection.Find.Replacement.ClearFormatting();
            wordApplication.Selection.Find.Execute(findMe);
            wordApplication.Selection.Text = (string)replaceMe;
            rng_exp.SetRange(rng_exp.End - 1, rng_exp.End - 1);
            rng_exp.Select();
        }
        cumplimentarPerfilesExperienciasPPTC(perfiles);

    }

    public static void cumplimentarPerfilesExperienciasPPTC(DataView perfiles)
    {
        Word.Range rng_datos = newDocument.Bookmarks["MKPerfil"].Range;

        if (perfiles.Count == 0)
        {
            rng_datos.Delete();
        }
        else
        {
            int i = rng_datos.Tables[1].Rows.Count;
            foreach (DataRowView oFila in perfiles)
            {
                if (i > 6) { rng_datos.Tables[1].Rows.Add(); }
                Word.Row oRow = rng_datos.Tables[1].Rows[i];
                oRow.Cells[2].Select();
                wordApplication.Selection.TypeText(oFila["DESCRIPCION"].ToString());
                oRow.Cells[3].Select();
                wordApplication.Selection.TypeText(oFila["FUNCION"].ToString());
                oRow.Cells[4].Select();
                wordApplication.Selection.TypeText(oFila["AREATEC"].ToString());
                i++;
            }
        }
        newDocument.Bookmarks["MKPerfil"].Delete();
    }

    #endregion

    #region Idiomas
    public static void CargarIdiomasPPTC(DataView dv)
    {
        if (dv.Count == 0)
        {
            Word.Range rngC = newDocument.Bookmarks["MKBloqueIdioma"].Range;
            rngC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKIdioma"].Range;
            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 5;
            foreach (DataRowView oFila in dv)
            {
                if (i != 5) rng_datos.Tables[1].Rows.Add(); //tenemos una fila como muestra para la letra y estilos
                ReemplazarDatosIdiomaPPTC(rng_datos.Tables[1].Rows[i], oFila);
                i++;
            }
        }
    }

    public static void ReemplazarDatosIdiomaPPTC(Word.Row oRow, DataRowView oFila)
    {
        if (idIdioma == int.Parse(oFila["T020_IDCODIDIOMA"].ToString()))
        {
            oRow.Cells[5].Select();
            wordApplication.Selection.TypeText(oFila["T021_TITULO"].ToString());
            oRow.Cells[6].Select();
            wordApplication.Selection.TypeText(oFila["T021_FECHA"].ToString());
            oRow.Cells[7].Select();
            wordApplication.Selection.TypeText(oFila["T021_CENTRO"].ToString());
        }
        else
        {
            oRow.Cells[1].Select();
            wordApplication.Selection.TypeText(oFila["T020_DESCRIPCION"].ToString());
            oRow.Cells[2].Select();
            wordApplication.Selection.TypeText(oFila["T013_LECTURA"].ToString());
            oRow.Cells[3].Select();
            wordApplication.Selection.TypeText(oFila["T013_ORAL"].ToString());
            oRow.Cells[4].Select();
            wordApplication.Selection.TypeText(oFila["T013_ESCRITURA"].ToString());
            oRow.Cells[5].Select();
            wordApplication.Selection.TypeText(oFila["T021_TITULO"].ToString());
            oRow.Cells[6].Select();
            wordApplication.Selection.TypeText(oFila["T021_FECHA"].ToString());
            oRow.Cells[7].Select();
            wordApplication.Selection.TypeText(oFila["T021_CENTRO"].ToString());
        }
        idIdioma = int.Parse(oFila["T020_IDCODIDIOMA"].ToString());
    }
    #endregion

    #region Cursos
    public static void CargarCursosPPTC(DataView dvCursosImp, DataView dvCursosRec)
    {
        if (dvCursosImp.Count == 0 && dvCursosRec.Count == 0)
        {
            Word.Range rngFC = newDocument.Bookmarks["MKBloqueFormacionComp"].Range;
            rngFC.Delete();
        }
        else
        {
            if (dvCursosImp.Count == 0)
            {
                Word.Range rngCI = newDocument.Bookmarks["MKCursosImpartidos"].Range;
                rngCI.Delete();
            }
            else
            {
                Word.Range tblrng = newDocument.Bookmarks["MKCursosImpartidosContenido"].Range;
                tblrng.Cut();
                tblrng.Select();
                foreach (DataRowView oFila in dvCursosRec)
                {
                    wordApplication.Selection.Paste();          //Pega la tabla
                    ReemplazarDatosCursosPPTC(newDocument.Bookmarks["MKCursosImpartidos"].Range, oFila, "Impartido");
                }
                wordApplication.Selection.Delete();
            }
            if (dvCursosRec.Count == 0)
            {
                Word.Range rngCR = newDocument.Bookmarks["MKCursosRecibidos"].Range;
                rngCR.Delete();
            }
            else
            {
                Word.Range tblrng = newDocument.Bookmarks["MKCursosRecibidosContenido"].Range;
                tblrng.Cut();
                tblrng.Select();
                foreach (DataRowView oFila in dvCursosRec)
                {
                    wordApplication.Selection.Paste();          //Pega la tabla
                    ReemplazarDatosCursosPPTC(newDocument.Bookmarks["MKCursosRecibidos"].Range, oFila, "Recibido");
                }
                wordApplication.Selection.Delete();
            }
        }

    }

    public static void ReemplazarDatosCursosPPTC(Word.Range rng_idi, DataRowView oFila, string origen)
    {
        Word.Range rng_datos = null;
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{titulo}";
        fnd.Replacement.Text = oFila["T574_TITULO"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{nH}";
        fnd.Replacement.Text = oFila["T574_HORAS"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{fecha}";
        fnd.Replacement.Text = oFila["FFIN"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{centro}";
        fnd.Replacement.Text = oFila["PROVEEDOR"].ToString();
        ExecuteReplace(fnd);

        if (oFila["T574_CONTENIDO"].ToString().Length < 250) // Normal execution
        {
            fnd.Text = "{contenido}";
            fnd.Replacement.Text = oFila["T574_CONTENIDO"].ToString();
            ExecuteReplace(fnd);
        }

        else  // Some real simple logic!!
        {
            rng_idi.Select();
            object replaceAll = WdReplace.wdReplaceAll;
            object findMe = "{contenido}";
            object replaceMe = oFila["T574_CONTENIDO"].ToString();
            wordApplication.Selection.Find.ClearFormatting();
            wordApplication.Selection.Find.Text = (string)findMe;
            wordApplication.Selection.Find.Replacement.ClearFormatting();
            wordApplication.Selection.Find.Execute(findMe);
            wordApplication.Selection.Text = (string)replaceMe;
            if (origen =="Recibido")
                rng_datos = newDocument.Bookmarks["MKCursosRecibidosContenido"].Range;
            else
                rng_datos = newDocument.Bookmarks["MKCursosImpartidosContenido"].Range;
            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            wordApplication.Selection.TypeText(" ");
            wordApplication.Selection.TypeBackspace();
            wordApplication.Selection.TypeBackspace();

        }
        
    }
    #endregion

    #endregion Plantilla oferta ppt completo
     
    
    #region Plantilla oferta ppt resumen
    public static void CargarDatosPPTR(DataSet ds, int i, int nDocs)
    {
        CargarDatosPersonalesPPTR(ds.Tables["DatosPersonales"].Rows[i]);

        //Formación academica
        DataView dvFA = new DataView(ds.Tables["FormacionAcademica"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarFormacionAcademicaPPTR(dvFA);

        //Certificados
        DataView dvCER = new DataView(ds.Tables["Certificados"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarCertificadosPPTR(dvCER);

        //Experiencias
        DataView dvEXP = new DataView(ds.Tables["Experiencia"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        DataView dvEXPPER = new DataView(ds.Tables["PerfilExperiencia"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarExperienciasPPTR(dvEXP, dvEXPPER);

        //Cursos
        DataView dvCurRec = new DataView(ds.Tables["CursosRecibidos"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarCursosPPTR(dvCurRec);

        //Idiomas
        DataView dvIDI = new DataView(ds.Tables["Idiomas"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarIdiomasPPTR(dvIDI);

        //Para el pie de página
        CargarPiePPTR(ds.Tables["DatosPersonales"].Rows[i], i + 1, nDocs); //a la i le sumamos 1 ya que los indices van de 1 a n (no de 0 a n-1)


    }

    public static void CargarDatosPersonalesPPTR(DataRow oFila)
    {
        string sPathDocs = "d:\\tmp\\word\\";

        Word.Range rng_datos = newDocument.Bookmarks["MKDatosPersonales"].Range;
        Word.Find fnd = rng_datos.Find; //rango dónde buscar

        fnd.Text = "{nombre}";
        fnd.Replacement.Text = oFila["Profesional"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{perfil}";
        fnd.Replacement.Text = oFila["t035_descripcion"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{fNacimiento}";
        fnd.Replacement.Text = (oFila["t001_fecnacim"].ToString() != "") ? ((DateTime)oFila["t001_fecnacim"]).ToShortDateString() : "";
        ExecuteReplace(fnd);

        fnd.Text = "{foto}";
        fnd.Replacement.Text = "";
        ExecuteFind(fnd);
        rng_datos.Select();
        rng_datos.SetRange(rng_datos.Start, rng_datos.End);

        bool bHayFoto = false;
        if (oFila["t001_foto"] != DBNull.Value)
        {
            bHayFoto = true;
            System.IO.File.WriteAllBytes(sPathDocs + "..\\temp\\foto.jpg", (byte[])oFila["t001_foto"]);
        }

        if (bHayFoto)
        {
            Word.Selection currentSelection = wordApplication.Selection;
            currentSelection.MoveRight();
            Word.InlineShape img = currentSelection.InlineShapes.AddPicture(sPathDocs + "..\\temp\\foto.jpg");
            img.Height = 50;
            img.Width = 50;
            System.IO.File.Delete(sPathDocs + "..\\temp\\foto.jpg");
        }
        ExecuteReplace(fnd);
    }

    public static void CargarPiePPTR(DataRow oFila, int i, int nDocs)
    {
        Word.Section section = newDocument.Sections[i];
        Word.Range footerRange = section.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
        Word.Find fnd = footerRange.Find;

        fnd.Text = "{fecha}";
        fnd.Replacement.Text = DateTime.Now.ToShortDateString();
        ExecuteReplace(fnd);
    }

    #region Formación académica PPTR

    public static void CargarFormacionAcademicaPPTR(DataView dv)
    {
        Word.Range rng_datos = newDocument.Bookmarks["MKBloqueFormacionAcademica"].Range;
        rng_datos.Select();

        if (dv.Count == 0)
        {
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            //rng_datos.Select();
            rng_datos.Copy();
            int i = 1;
            foreach (DataRowView oFila in dv)
            {
                if (i != 1)
                {
                    rng_datos = newDocument.Bookmarks["MKBloqueFormacionAcademica"].Range;
                    rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                    rng_datos.Select();
                    wordApplication.Selection.TypeText(" ");
                    wordApplication.Selection.TypeBackspace();
                    wordApplication.Selection.Paste();
                }
                ReemplazarDatosFAPPTR(newDocument.Bookmarks["MKDatosPersonales"].Range, oFila, i);
                i++;
            }
        }
    }

    public static void ReemplazarDatosFAPPTR(Word.Range rng_datos, DataRowView oFila, int i)
    {
        rng_datos.Select();
        Word.Find fnd = rng_datos.Find;
        
        fnd.Text = "{Titulacion}";
        if (i ==1)
            fnd.Replacement.Text = "Titulación";
        else
            fnd.Replacement.Text = "";
        ExecuteReplace(fnd);

        fnd.Text = "{duracion}";
        fnd.Replacement.Text = oFila["T012_INICIO"].ToString() + " - " + oFila["T012_FIN"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{titulo}";
        fnd.Replacement.Text = oFila["T019_DESCRIPCION"].ToString();
        ExecuteReplace(fnd);
    }


    #endregion

    #region Certificados
    public static void CargarCertificadosPPTR(DataView dv)
    {
        Word.Range rng_datos = newDocument.Bookmarks["MKBloqueCertificaciones"].Range;
        rng_datos.Select();
        if (dv.Count == 0)
        {
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            rng_datos.Copy();
            int i = 1;
            foreach (DataRowView oFila in dv)
            {
                if (i != 1)
                {
                    rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                    rng_datos.Select(); 
                    wordApplication.Selection.TypeText(" ");
                    wordApplication.Selection.TypeBackspace();
                    wordApplication.Selection.Paste(); 
                }
                ReemplazarDatosCertificadosPPTR(newDocument.Bookmarks["MKDatosPersonales"].Range, oFila, i);
                i++;
            }
        }
    }

    public static void ReemplazarDatosCertificadosPPTR(Word.Range rng_datos, DataRowView oFila, int i)
    {
        rng_datos.Select();
        Word.Find fnd = rng_datos.Find;
        
        fnd.Text = "{Certificaciones}";
        if (i ==1)
            fnd.Replacement.Text = "Certificaciones";
        else
            fnd.Replacement.Text = "";
        ExecuteReplace(fnd);

        fnd.Text = "{fecha}";
        fnd.Replacement.Text = oFila["FOBTENCION"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{titulo_cert}";
        fnd.Replacement.Text = oFila["TITULO"].ToString();
        ExecuteReplace(fnd);

        //rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
        //rng_datos.Select();
    }
    #endregion

    #region Experiencias profesionales
    public static void CargarExperienciasPPTR(DataView dv, DataView dvP)
    {
        if (dv.Count == 0)
        {
            Word.Range rngC = newDocument.Bookmarks["MKBloqueExpProf"].Range;
            rngC.Select();
            wordApplication.Selection.Delete(); 
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKExpProf"].Range;
            rng_datos.Cut();
            rng_datos.Select();
            int i = 0;
            foreach (DataRowView oFila in dv)
            {
                if (i > 0)
                {
                    rng_datos = newDocument.Bookmarks["MKBloqueExpProf"].Range;
                    rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                    rng_datos.Select();                
                }
                wordApplication.Selection.Paste();
                DataView perfil = new DataView(dvP.ToTable(), "t808_idexpprof= " + oFila["T808_IDEXPPROF"].ToString(), "FFIN, FINICIO", DataViewRowState.CurrentRows);
                ReemplazarDatosExperienciasPPTR(newDocument.Bookmarks["MKBloqueExpProf"].Range, oFila, perfil);
                i++;
            }
        }
    }

    public static void ReemplazarDatosExperienciasPPTR(Word.Range rng_exp, DataRowView oFila, DataView perfiles)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_exp.Find;

        fnd.Text = "{duracion}";
        fnd.Replacement.Text = oFila["FINICIO"].ToString() + " - " + ((oFila["FFIN"].ToString() == "") ? "Actualidad" : oFila["FFIN"].ToString());
        ExecuteReplace(fnd);

        fnd.Text = "{cliente_exp}";
        fnd.Replacement.Text = oFila["CLIENTE"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{titulo_exp}";
        fnd.Replacement.Text = oFila["T808_DENOMINACION"].ToString();
        ExecuteReplace(fnd);

        if (oFila["T808_DESCRIPCION"].ToString().Length < 250) // Normal execution
        {
            fnd.Text = "{descripcion}";
            fnd.Replacement.Text = oFila["T808_DESCRIPCION"].ToString();
            ExecuteReplace(fnd);
        }
        else
        {
            rng_exp.Select();
            object replaceAll = WdReplace.wdReplaceAll;
            object findMe = "{descripcion}";
            object replaceMe = oFila["T808_DESCRIPCION"].ToString();
            wordApplication.Selection.Find.ClearFormatting();
            wordApplication.Selection.Find.Text = (string)findMe;
            wordApplication.Selection.Find.Replacement.ClearFormatting();
            wordApplication.Selection.Find.Execute(findMe);
            wordApplication.Selection.Text = (string)replaceMe;
            rng_exp.SetRange(rng_exp.End - 1, rng_exp.End - 1);
            rng_exp.Select();
        }

        fnd.Text = "{areaconocimiento}";
        fnd.Replacement.Text = oFila["AREATEC"].ToString();
        ExecuteReplace(fnd);
        cumplimentarPerfilesExperienciasPPTR(perfiles);

    }

    public static void cumplimentarPerfilesExperienciasPPTR(DataView perfiles)
    {
        Word.Range rng_datos = newDocument.Bookmarks["MKPerfil"].Range;

        if (perfiles.Count == 0)
        {
            rng_datos.Delete();
        }
        else
        {
            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 0;
            foreach (DataRowView oFila in perfiles)
            {
                if (i > 0) { wordApplication.Selection.TypeParagraph(); wordApplication.Selection.TypeParagraph(); }
                wordApplication.Selection.TypeText(oFila["DESCRIPCION"].ToString().ToUpper());
                wordApplication.Selection.TypeParagraph();
                wordApplication.Selection.Font.Size = 8;
                wordApplication.Selection.TypeText(oFila["FUNCION"].ToString());
                wordApplication.Selection.Font.Size = 9;
                i++;
            }
        }
        newDocument.Bookmarks["MKPerfil"].Delete();
    }
    #endregion

    #region Idiomas
    public static void CargarIdiomasPPTR(DataView dv)
    {
        if (dv.Count == 0)
        {
            Word.Range rngC = newDocument.Bookmarks["MKBloqueIdioma"].Range;
            rngC.Select();
            wordApplication.Selection.Delete();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKIdioma"].Range;
            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 2;
            foreach (DataRowView oFila in dv)
            {
                if (i != 2) rng_datos.Tables[1].Rows.Add(); //tenemos una fila como muestra para la letra y estilos
                ReemplazarDatosIdiomaPPTR(rng_datos.Tables[1].Rows[i], oFila);
                i++;
            }
        }
    }

    public static void ReemplazarDatosIdiomaPPTR(Word.Row oRow, DataRowView oFila)
    {
        oRow.Cells[1].Select();
        wordApplication.Selection.TypeText(oFila["T020_DESCRIPCION"].ToString());
        oRow.Cells[2].Select();
        wordApplication.Selection.TypeText(oFila["T013_LECTURA"].ToString());
        oRow.Cells[3].Select();
        wordApplication.Selection.TypeText(oFila["T013_ORAL"].ToString());
        oRow.Cells[4].Select();
        wordApplication.Selection.TypeText(oFila["T013_ESCRITURA"].ToString());
        oRow.Cells[5].Select();
        wordApplication.Selection.TypeText(oFila["T021_TITULO"].ToString());
    }
    #endregion

    #region Cursos
    public static void CargarCursosPPTR(DataView dvCursosRec)
    {
        if (dvCursosRec.Count == 0)
        {
            Word.Range rngFC = newDocument.Bookmarks["MKBloqueFormacionComp"].Range;
            rngFC.Select();
            wordApplication.Selection.Delete();
            wordApplication.Selection.TypeBackspace();
        }
        else
        {
            Word.Range tblrng = newDocument.Bookmarks["MKCursosRecibidos"].Range;
            tblrng.Select();
            tblrng.Cut();
            foreach (DataRowView oFila in dvCursosRec)
            {
                wordApplication.Selection.Paste();          //Pega la tabla
                ReemplazarDatosCursosPPTR(newDocument.Bookmarks["MKBloqueFormacionComp"].Range, oFila);
            }
            //wordApplication.Selection.Delete();
        }
    }

    public static void ReemplazarDatosCursosPPTR(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{titulo}";
        fnd.Replacement.Text = oFila["T574_TITULO"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{numH}";
        fnd.Replacement.Text = oFila["T574_HORAS"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{fecha}";
        fnd.Replacement.Text = oFila["FFIN"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{centro}";
        fnd.Replacement.Text = oFila["PROVEEDOR"].ToString();
        ExecuteReplace(fnd);

    }
    #endregion

    #endregion Plantilla oferta ppt resumen

    
     
    #region PlantillaCVCompleto
    public static void CargarDatosCVC(DataSet ds, int i, int nDocs)
    {
        CargarDatosPersonalesCVC(ds.Tables["DatosPersonales"].Rows[i]);

        //SINOPSIS
        CargarSinopsisCVC(ds.Tables["DatosPersonales"].Rows[i]);

        //Formación academica
        DataView dvFA = new DataView(ds.Tables["FormacionAcademica"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarFormacionAcademicaCVC(dvFA);

        //Certificados
        DataView dvCER = new DataView(ds.Tables["Certificados"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarCertificadosCVC(dvCER);

        //Experiencias
        DataView dvEXP = new DataView(ds.Tables["Experiencia"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        DataView dvEXPPER = new DataView(ds.Tables["PerfilExperiencia"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarExperienciasCVC(dvEXP, dvEXPPER);

        //Idiomas
        DataView dvIDI = new DataView(ds.Tables["Idiomas"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarIdiomasCVC(dvIDI);

        //Cursos
        DataView dvCurImp = new DataView(ds.Tables["CursosImpartidos"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        DataView dvCurRec = new DataView(ds.Tables["CursosRecibidos"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        DataView dvExam = new DataView(ds.Tables["Examenes"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarCursosCVC(dvCurImp, dvCurRec, dvExam);

        //Para el pie de página
        CargarPieCVC(ds.Tables["DatosPersonales"].Rows[i], i + 1, nDocs); //a la i le sumamos 1 ya que los indices van de 1 a n (no de 0 a n-1)


    }

    public static void CargarDatosPersonalesCVC(DataRow oFila)
    {
        string sPathDocs = "d:\\tmp\\word\\";

        Word.Range rng_datos = newDocument.Bookmarks["MKDatosPersonales"].Range;
        Word.Find fnd = rng_datos.Find; //rango dónde buscar

        fnd.Text = "{nombre}";
        fnd.Replacement.Text = oFila["Profesional"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{nif}";
        fnd.Replacement.Text = oFila["t001_cip"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{perfil}";
        fnd.Replacement.Text = oFila["t035_descripcion"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{fNacimiento}";
        fnd.Replacement.Text = (oFila["t001_fecnacim"].ToString() != "") ? ((DateTime)oFila["t001_fecnacim"]).ToShortDateString() : "";
        ExecuteReplace(fnd);

        fnd.Text = "{nacionalidad}";
        fnd.Replacement.Text = oFila["T001_NACIONALID"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{sexo}";
        fnd.Replacement.Text = oFila["SEXO"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{empresa}";
        fnd.Replacement.Text = oFila["EMPRESA"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{sn2}";
        fnd.Replacement.Text = oFila["T392_DENOMINACION"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{nodo}";
        fnd.Replacement.Text = oFila["T303_DENOMINACION"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{fAntiguedad}";
        fnd.Replacement.Text = (oFila["T001_FECANTIGU"].ToString() != "") ? ((DateTime)oFila["T001_FECANTIGU"]).ToShortDateString() : "";
        ExecuteReplace(fnd);

        fnd.Text = "{rol}";
        fnd.Replacement.Text = oFila["ROL"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{oficina}";
        fnd.Replacement.Text = oFila["T010_DESOFICINA"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{provincia}";
        fnd.Replacement.Text = oFila["t173_denominacion"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{pais}";
        fnd.Replacement.Text = oFila["PAIS"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{trayInter}";
        fnd.Replacement.Text = oFila["trayInter"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{dispMovilidad}";
        fnd.Replacement.Text = oFila["dispMovilidad"].ToString();
        ExecuteReplace(fnd);

        if (oFila["t001_cvobserva"].ToString().Length < 250) // Normal execution
        {
            fnd.Text = "{observa}";
            fnd.Replacement.Text = oFila["t001_cvobserva"].ToString();
            ExecuteReplace(fnd);
        }

        else
        {
            rng_datos.Select();
            object replaceAll = WdReplace.wdReplaceAll;
            object findMe = "{observa}";
            object replaceMe = oFila["t001_cvobserva"].ToString();
            wordApplication.Selection.Find.ClearFormatting();
            wordApplication.Selection.Find.Text = (string)findMe;
            wordApplication.Selection.Find.Replacement.ClearFormatting();
            wordApplication.Selection.Find.Execute(findMe);

            wordApplication.Selection.Text = (string)replaceMe;
            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
        }

        fnd.Text = "{foto}";
        fnd.Replacement.Text = "";
        ExecuteFind(fnd);
        rng_datos.Select();
        rng_datos.SetRange(rng_datos.Start, rng_datos.End);

        bool bHayFoto = false;
        if (oFila["t001_foto"] != DBNull.Value)
        {
            bHayFoto = true;
            System.IO.File.WriteAllBytes(sPathDocs + "..\\temp\\foto.jpg", (byte[])oFila["t001_foto"]);
        }


        if (bHayFoto)
        {
            Word.Selection currentSelection = wordApplication.Selection;
            currentSelection.MoveRight();
            Word.InlineShape img = currentSelection.InlineShapes.AddPicture(sPathDocs + "..\\temp\\foto.jpg");
            img.Height = 70;
            img.Width = 70;
            System.IO.File.Delete(sPathDocs + "..\\temp\\foto.jpg");
        }
        ExecuteReplace(fnd);
    }

    public static void CargarPieCVC(DataRow oFila, int i, int nDocs)
    {
        Word.Sections sections = newDocument.Sections;
        i = (nDocs == 0) ? i : 1;
        sections[i].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].LinkToPrevious = false;
        Word.Range footerRange = sections[i].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;

        footerRange.Font.Size = 8;
        footerRange.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
        footerRange.Text = @"Historial Profesional de " + oFila["Profesional"].ToString();
        sections[i].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].PageNumbers.RestartNumberingAtSection = true;
        footerRange.SetRange(footerRange.End - 1, footerRange.End - 1);
        footerRange.Select();
        wordApplication.Selection.TypeParagraph();

        int numPagsA = newDocument.ComputeStatistics(WdStatistic.wdStatisticPages, false);
        Object CurrentPage = WdFieldType.wdFieldPage;
        wordApplication.Selection.Fields.Add(wordApplication.Selection.Range, CurrentPage);
        wordApplication.Selection.TypeText("/" + (numPagsA - numPags));
        wordApplication.Selection.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        numPags = numPagsA; //guardamos el número de páginas total para poder calcular el número de páginas de cada cv.

    }

    public static void CargarSinopsisCVC(DataRow oFila)
    {
        Word.Range rng_datos = newDocument.Bookmarks["MKSinopsis"].Range;
        rng_datos.Select();
        wordApplication.Selection.TypeText(oFila["T185_SINOPSIS"].ToString());
    }

    #region Formación académica CVC

    public static void CargarFormacionAcademicaCVC(DataView dv)
    {
        if (dv.Count == 0)
        {
            Word.Range rngTC = newDocument.Bookmarks["MKBloqueFormacionAcademica"].Range;
            rngTC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKFormacionAcademica"].Range;
            Word.Range tblrng = rng_datos.Tables[1].Range;
            tblrng.Cut();


            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 0;
            foreach (DataRowView oFila in dv)
            {
                if (i > 0) wordApplication.Selection.TypeParagraph();
                wordApplication.Selection.Paste();
                ReemplazarDatosFACVC(newDocument.Bookmarks["MKFormacionAcademica"].Range, oFila);
                i++;
            }
            wordApplication.Selection.Delete();
        }
    }

    public static void ReemplazarDatosFACVC(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{titulo}";
        fnd.Replacement.Text = oFila["T019_DESCRIPCION"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{tipo}";
        fnd.Replacement.Text = oFila["T019_TIPO"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{modalidad}";
        fnd.Replacement.Text = oFila["T019_MODALIDAD"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{tic}";
        fnd.Replacement.Text = oFila["T019_TIC"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{especialidad}";
        fnd.Replacement.Text = oFila["T012_ESPECIALIDAD"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{centro}";
        fnd.Replacement.Text = oFila["T012_CENTRO"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{duracion}";
        fnd.Replacement.Text = oFila["T012_INICIO"].ToString() + " - " + oFila["T012_FIN"].ToString();
        ExecuteReplace(fnd);
    }

    #endregion

    #region Certificados
    public static void CargarCertificadosCVC(DataView dv)
    {
        if (dv.Count == 0)
        {
            Word.Range rngTC = newDocument.Bookmarks["MKBloqueCertificaciones"].Range;
            rngTC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKCertificaciones"].Range;
            Word.Range tblrng = rng_datos.Tables[1].Range;
            tblrng.Cut();

            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 0;
            foreach (DataRowView oFila in dv)
            {
                if (i > 0) wordApplication.Selection.TypeParagraph();
                wordApplication.Selection.Paste();          //Pega la tabla
                ReemplazarDatosCertificadosCVC(newDocument.Bookmarks["MKCertificaciones"].Range, oFila);
                i++;
            }
            wordApplication.Selection.Delete();
        }
    }

    public static void ReemplazarDatosCertificadosCVC(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{titulo}";
        fnd.Replacement.Text = oFila["TITULO"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{entidad}";
        fnd.Replacement.Text = oFila["PROVEEDOR"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{entorno}";
        fnd.Replacement.Text = oFila["ENTORNO"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{fObtencion}";
        fnd.Replacement.Text = oFila["FOBTENCION"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{fCaducidad}";
        fnd.Replacement.Text = oFila["FCADUCIDAD"].ToString();
        ExecuteReplace(fnd);
    }

    #endregion

    #region Experiencias profesionales
    public static void CargarExperienciasCVC(DataView dv, DataView dvP)
    {
        if (dv.Count == 0)
        {
            Word.Range rngC = newDocument.Bookmarks["MKBloqueExpProf"].Range;
            rngC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKExpProf"].Range;
            Word.Range tblrng = rng_datos.Tables[1].Range;
            tblrng.Copy();

            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 0;
            foreach (DataRowView oFila in dv)
            {
                if (i > 0)
                {
                    rng_datos = newDocument.Bookmarks["MKExpProf"].Range;
                    rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                    rng_datos.Select();
                    wordApplication.Selection.TypeParagraph();
                    wordApplication.Selection.Paste();
                }
                DataView perfil = new DataView(dvP.ToTable(), "t808_idexpprof= " + oFila["T808_IDEXPPROF"].ToString(), "FFIN, FINICIO", DataViewRowState.CurrentRows);
                ReemplazarDatosExperienciasCVC(newDocument.Bookmarks["MKExpProf"].Range, oFila, perfil, i+1);
                i++;
            }
            wordApplication.Selection.Delete();
        }
    }

    public static void ReemplazarDatosExperienciasCVC(Word.Range rng_exp, DataRowView oFila, DataView perfil, int nExp)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_exp.Find;

        fnd.Text = "{fecha_exp}";
        fnd.Replacement.Text = DateTime.Parse(oFila["FINICIO"].ToString()).Year + " - " + ((oFila["FFIN"].ToString() == "") ? "Actualidad" : DateTime.Parse(oFila["FFIN"].ToString()).Year.ToString());
        ExecuteReplace(fnd);

        fnd.Text = "{titulo_exp}";
        fnd.Replacement.Text = oFila["T808_DENOMINACION"].ToString();
        ExecuteReplace(fnd);

        if (oFila["T808_DESCRIPCION"].ToString().Length < 250) // Normal execution
        {
            fnd.Text = "{descripcion}";
            fnd.Replacement.Text = oFila["T808_DESCRIPCION"].ToString();
            ExecuteReplace(fnd);
        }
        else 
        {
            rng_exp.Select();
            object replaceAll = WdReplace.wdReplaceAll;
            object findMe = "{descripcion}";
            object replaceMe = oFila["T808_DESCRIPCION"].ToString();
            wordApplication.Selection.Find.ClearFormatting();
            wordApplication.Selection.Find.Text = (string)findMe;
            wordApplication.Selection.Find.Replacement.ClearFormatting();
            wordApplication.Selection.Find.Execute(findMe);
            wordApplication.Selection.Text = (string)replaceMe;
            rng_exp.SetRange(rng_exp.End - 1, rng_exp.End - 1);
            rng_exp.Select();
        }

        fnd.Text = "{areaconocimiento}";
        fnd.Replacement.Text = oFila["SECTTEC"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{cliente}";
        fnd.Replacement.Text = oFila["CLIENTE"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{secSegCli}";
        fnd.Replacement.Text = oFila["SECTORCLI"].ToString() + ((oFila["SECTORCLI"].ToString() != "" && oFila["SEGMENTOCLI"].ToString() != "") ? " - " : "") + oFila["SEGMENTOCLI"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{empresaCon}";
        fnd.Replacement.Text = oFila["EMPRESACON"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{secSegEmC}";
        fnd.Replacement.Text = oFila["SECTOREMP"].ToString() + ((oFila["SECTOREMP"].ToString() != "" && oFila["SEGMENTOEMP"].ToString() != "") ? " - " : "") + oFila["SEGMENTOEMP"].ToString();
        ExecuteReplace(fnd);

        cumplimentarPerfilesExperienciasCVC(perfil, nExp);
    }

    public static void cumplimentarPerfilesExperienciasCVC(DataView perfiles, int nExp)
    {
        Word.Range rng_datos = newDocument.Bookmarks["MKExpProf"].Range;

        if (perfiles.Count == 0)
        {
            rng_datos.Delete();
        }
        else
        {
            int fila = 8;
            //int nExp = 1;
            foreach (DataRowView oFila in perfiles)
            {
                if (fila > 8) rng_datos.Tables[nExp].Rows.Add(); //dejamos la primera fila para los estilos y las columnas. Tenemos que hacerlo así porque no podemos copiar de dos marcadores y mantener lo que se copió la primera vez-
                Word.Row oRow = rng_datos.Tables[nExp].Rows[fila];
                oRow.Cells[2].Select();
                wordApplication.Selection.TypeText("Perfil");
                oRow.Cells[3].Select();
                wordApplication.Selection.TypeText(oFila["DESCRIPCION"].ToString());
                fila++;
                rng_datos.Tables[nExp].Rows.Add();
                oRow = rng_datos.Tables[nExp].Rows[fila];
                oRow.Cells[2].Select();
                wordApplication.Selection.TypeText("Fecha inicio y fin");
                oRow.Cells[3].Select();
                wordApplication.Selection.TypeText(DateTime.Parse(oFila["FINICIO"].ToString()).Year + " - " + ((oFila["FFIN"].ToString() == "") ? "Actualidad" : DateTime.Parse(oFila["FFIN"].ToString()).Year.ToString()));
                fila++;
                rng_datos.Tables[nExp].Rows.Add();
                oRow = rng_datos.Tables[nExp].Rows[fila];
                oRow.Cells[2].Select();
                wordApplication.Selection.TypeText("Funciones");
                oRow.Cells[3].Select();
                wordApplication.Selection.TypeText(oFila["FUNCION"].ToString());
                fila++;
                rng_datos.Tables[nExp].Rows.Add();
                oRow = rng_datos.Tables[nExp].Rows[fila];
                oRow.Cells[2].Select();
                wordApplication.Selection.TypeText("Áreas  y Tecnologías");
                oRow.Cells[3].Select();
                wordApplication.Selection.TypeText(oFila["AREATEC"].ToString());
                fila++;
            }
        }
        
    }
    #endregion

    #region Idiomas
    public static void CargarIdiomasCVC(DataView dv)
    {
        if (dv.Count == 0)
        {
            Word.Range rngC = newDocument.Bookmarks["MKBloqueIdioma"].Range;
            rngC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKIdioma"].Range;
            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 2;
            foreach (DataRowView oFila in dv)
            {
                if (i != 2) rng_datos.Tables[1].Rows.Add(); //tenemos una fila como muestra para la letra y estilos
                ReemplazarDatosIdiomaCVC(rng_datos.Tables[1].Rows[i], oFila);
                i++;
            }
        }
    }

    public static void ReemplazarDatosIdiomaCVC(Word.Row oRow, DataRowView oFila)
    {
        if (idIdioma == int.Parse(oFila["T020_IDCODIDIOMA"].ToString()))
        {
            oRow.Cells[5].Select();
            wordApplication.Selection.TypeText(oFila["T021_TITULO"].ToString());
            oRow.Cells[6].Select();
            wordApplication.Selection.TypeText(oFila["T021_FECHA"].ToString());
            oRow.Cells[7].Select();
            wordApplication.Selection.TypeText(oFila["T021_CENTRO"].ToString());
        }
        else
        {
            oRow.Cells[1].Select();
            wordApplication.Selection.TypeText(oFila["T020_DESCRIPCION"].ToString());
            oRow.Cells[2].Select();
            wordApplication.Selection.TypeText(oFila["T013_LECTURA"].ToString());
            oRow.Cells[3].Select();
            wordApplication.Selection.TypeText(oFila["T013_ORAL"].ToString());
            oRow.Cells[4].Select();
            wordApplication.Selection.TypeText(oFila["T013_ESCRITURA"].ToString());
            oRow.Cells[5].Select();
            wordApplication.Selection.TypeText(oFila["T021_TITULO"].ToString());
            oRow.Cells[6].Select();
            wordApplication.Selection.TypeText(oFila["T021_FECHA"].ToString());
            oRow.Cells[7].Select();
            wordApplication.Selection.TypeText(oFila["T021_CENTRO"].ToString());
        }
        idIdioma = int.Parse(oFila["T020_IDCODIDIOMA"].ToString());
    }
    #endregion

    #region Cursos
    public static void CargarCursosCVC(DataView dvCursosImp, DataView dvCursosRec, DataView dvExamenes)
    {
        if (dvCursosImp.Count == 0 && dvCursosRec.Count == 0 && dvExamenes.Count == 0)
        {
            Word.Range rngFC = newDocument.Bookmarks["MKBloqueFormacionComp"].Range;
            rngFC.Delete();
        }
        else
        {
            if (dvCursosImp.Count == 0)
            {
                Word.Range rngCI = newDocument.Bookmarks["MKCursosImpartidos"].Range;
                rngCI.Delete();
            }
            else
            {
                Word.Range tblrng = newDocument.Bookmarks["MKCursosImpartidos"].Range.Tables[1].Range;
                tblrng.Cut();
                Word.Range rng_datos = newDocument.Bookmarks["MKCursosImpartidos"].Range;
                //rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                tblrng.Select();
                int iImp = 0;
                foreach (DataRowView oFila in dvCursosRec)
                {
                    if (iImp > 0) wordApplication.Selection.TypeParagraph();
                    wordApplication.Selection.Paste();          //Pega la tabla
                    ReemplazarDatosCursosCVC(newDocument.Bookmarks["MKCursosImpartidos"].Range, oFila);
                    iImp++;
                }
                wordApplication.Selection.Delete();
            }
            if (dvCursosRec.Count == 0)
            {
                Word.Range rngCR = newDocument.Bookmarks["MKCursosRecibidos"].Range;
                rngCR.Delete();
            }
            else
            {
                Word.Range tblrng = newDocument.Bookmarks["MKCursosRecibidos"].Range.Tables[1].Range;
                tblrng.Cut();

                Word.Range rng_datos = newDocument.Bookmarks["MKCursosRecibidos"].Range;
                rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                tblrng.Select();
                int iRec = 0;
                foreach (DataRowView oFila in dvCursosRec)
                {
                    if (iRec > 0) wordApplication.Selection.TypeParagraph();
                    wordApplication.Selection.Paste();          //Pega la tabla
                    ReemplazarDatosCursosCVC(newDocument.Bookmarks["MKCursosRecibidos"].Range, oFila);
                    iRec++;
                }
                wordApplication.Selection.Delete();

            }
            if (dvExamenes.Count == 0)
            {
                Word.Range rngEx = newDocument.Bookmarks["MKCursosExamenes"].Range;
                rngEx.Delete();
            }
            else
            {
                Word.Range tblrng = newDocument.Bookmarks["MKCursosExamenes"].Range.Tables[1].Range;
                tblrng.Cut();

                Word.Range rng_datos = newDocument.Bookmarks["MKCursosExamenes"].Range;
                tblrng.SetRange(tblrng.End - 1, tblrng.End - 1);
                tblrng.Select();
                int iEx = 0;
                foreach (DataRowView oFila in dvExamenes)
                {
                    if (iEx > 0) wordApplication.Selection.TypeParagraph();
                    wordApplication.Selection.Paste();          //Pega la tabla
                    ReemplazarDatosExamenesCVC(newDocument.Bookmarks["MKCursosExamenes"].Range, oFila);
                    iEx++;
                }
            }

        }

    }

    public static void ReemplazarDatosCursosCVC(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{titulo}";
        fnd.Replacement.Text = oFila["T574_TITULO"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{tipo}";
        fnd.Replacement.Text = oFila["TIPO"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{horas}";
        fnd.Replacement.Text = oFila["T574_HORAS"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{duracion}";
        fnd.Replacement.Text = oFila["FINICIO"].ToString() + " - " + oFila["FFIN"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{centro}";
        fnd.Replacement.Text = oFila["PROVEEDOR"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{provincia}";
        fnd.Replacement.Text = oFila["PROVINCIA"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{entorno}";
        fnd.Replacement.Text = oFila["ENTORNO"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{modalidad}";
        fnd.Replacement.Text = oFila["MODALIDAD"].ToString();
        ExecuteReplace(fnd);

        if (oFila["T574_CONTENIDO"].ToString().Length < 250) // Normal execution
        {
            fnd.Text = "{contenido}";
            fnd.Replacement.Text = oFila["T574_CONTENIDO"].ToString();
            ExecuteReplace(fnd);
        }

        else  // Some real simple logic!!
        {
            rng_idi.Select();
            object replaceAll = WdReplace.wdReplaceAll;
            object findMe = "{contenido}";
            object replaceMe = oFila["T574_CONTENIDO"].ToString();
            wordApplication.Selection.Find.ClearFormatting();
            wordApplication.Selection.Find.Text = (string)findMe;
            wordApplication.Selection.Find.Replacement.ClearFormatting();
            wordApplication.Selection.Find.Execute(findMe);
            wordApplication.Selection.Text = (string)replaceMe;
            rng_idi.SetRange(rng_idi.End - 1, rng_idi.End - 1);
            rng_idi.Select();
        }



    }
    #endregion

    #region Exámenes
    public static void ReemplazarDatosExamenesCVC(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{titulo}";
        fnd.Replacement.Text = oFila["TITULO"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{entidad}";
        fnd.Replacement.Text = oFila["PROVEEDOR"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{entorno}";
        fnd.Replacement.Text = oFila["ENTORNO"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{fObtencion}";
        fnd.Replacement.Text = oFila["FOBTENCION"].ToString();
        ExecuteReplace(fnd);


        fnd.Text = "{fCaducidad}";
        fnd.Replacement.Text = oFila["FCADUCIDAD"].ToString();
        ExecuteReplace(fnd);
    }
    #endregion

    #endregion PlantillaCVCompleto
   
   
    #region PlantillaAT

    public static void CargarDatosAT(DataSet ds, int i, int nDocs)
    {
        CargarDatosPersonalesAT(ds.Tables["DatosPersonales"].Rows[i]);

        //SINOPSIS
        CargarSinopsisAT(ds.Tables["DatosPersonales"].Rows[i]);

        //Formación academica
        DataView dvFA = new DataView(ds.Tables["FormacionAcademica"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarFormacionAcademicaAT(dvFA);

        //Certificados
        DataView dvCER = new DataView(ds.Tables["Certificados"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarCertificadosAT(dvCER);

        //Experiencias
        DataView dvEXP = new DataView(ds.Tables["Experiencia"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        DataView dvEXPPER = new DataView(ds.Tables["PerfilExperiencia"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarExperienciasAT(dvEXP,dvEXPPER);

        //Idiomas
        DataView dvIDI = new DataView(ds.Tables["Idiomas"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarIdiomasAT(dvIDI);

        //Cursos
        DataView dvCurImp = new DataView(ds.Tables["CursosImpartidos"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        DataView dvCurRec = new DataView(ds.Tables["CursosRecibidos"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        DataView dvExam = new DataView(ds.Tables["Examenes"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[i]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
        CargarCursosAT(dvCurImp, dvCurRec, dvExam);

        //Para el pie de página
        CargarPieAT(ds.Tables["DatosPersonales"].Rows[i], i + 1, nDocs); //a la i le sumamos 1 ya que los indices van de 1 a n (no de 0 a n-1)


    }

    public static void CargarDatosPersonalesAT(DataRow oFila)
    {
        string sPathDocs = "d:\\tmp\\word\\";

        Word.Range rng_datos = newDocument.Bookmarks["MKDatosPersonales"].Range;
        Word.Find fnd = rng_datos.Find; //rango dónde buscar

        fnd.Text = "{nombre}";
        fnd.Replacement.Text = oFila["Profesional"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{nif}";
        fnd.Replacement.Text = oFila["t001_cip"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{perfil}";
        fnd.Replacement.Text = oFila["t035_descripcion"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{fnacimiento}";
        fnd.Replacement.Text = (oFila["t001_fecnacim"].ToString() != "") ? ((DateTime)oFila["t001_fecnacim"]).ToShortDateString() : "";
        ExecuteReplace(fnd);

        fnd.Text = "{foto}";
        fnd.Replacement.Text = "";
        ExecuteFind(fnd);
        rng_datos.Select();
        rng_datos.SetRange(rng_datos.Start, rng_datos.End);

        bool bHayFoto = false;
        if (oFila["t001_foto"] != DBNull.Value)
        {
            bHayFoto = true;
            System.IO.File.WriteAllBytes(sPathDocs + "..\\temp\\foto.jpg", (byte[])oFila["t001_foto"]);
        }

        if (bHayFoto)
        {
            Word.Selection currentSelection = wordApplication.Selection;
            currentSelection.MoveRight();
            Word.InlineShape img = currentSelection.InlineShapes.AddPicture(sPathDocs + "..\\temp\\foto.jpg");
            img.Height = 70;
            img.Width = 70;
            System.IO.File.Delete(sPathDocs + "..\\temp\\foto.jpg");
        }
        ExecuteReplace(fnd);
    }

    public static void CargarPieAT(DataRow oFila, int i, int nDocs)
    {
        Word.Sections sections = newDocument.Sections;
        i = (nDocs == 0) ? i : 1;
        sections[i].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].LinkToPrevious = false;
        Word.Range footerRange = sections[i].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;

        footerRange.Font.Size = 8;
        footerRange.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
        footerRange.Text = @"Historial Profesional de
" + oFila["Profesional"].ToString();
        sections[i].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].PageNumbers.RestartNumberingAtSection = true;
        footerRange.SetRange(footerRange.End - 1, footerRange.End - 1);
        footerRange.Select();
        wordApplication.Selection.TypeParagraph();

        int numPagsA = newDocument.ComputeStatistics(WdStatistic.wdStatisticPages, false);
        Object CurrentPage = WdFieldType.wdFieldPage;
        wordApplication.Selection.Fields.Add(wordApplication.Selection.Range, CurrentPage);
        wordApplication.Selection.TypeText("/" + (numPagsA - numPags));
        wordApplication.Selection.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        numPags = numPagsA; //guardamos el número de páginas total para poder calcular el número de páginas de cada cv.

    }

    public static void CargarSinopsisAT(DataRow oFila)
    {
        if (oFila["T185_SINOPSIS"].ToString() == "")
        {
            Word.Range rngS = newDocument.Bookmarks["MKBloqueSinopsis"].Range;
            rngS.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKSinopsis"].Range;
            rng_datos.Select();
            wordApplication.Selection.TypeText(oFila["T185_SINOPSIS"].ToString());
        }
    }

    #region Formación académicaAT

    public static void CargarFormacionAcademicaAT(DataView dv)
    {
        if (dv.Count == 0)
        {
            Word.Range rngTC = newDocument.Bookmarks["MKBloqueFormacionAcademica"].Range;
            rngTC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKFormacionAcademica"].Range;
            Word.Range tblrng = rng_datos.Tables[1].Range;
            tblrng.Cut();


            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();

            foreach (DataRowView oFila in dv)
            {
                wordApplication.Selection.Paste();
                ReemplazarDatosFAAT(newDocument.Bookmarks["MKFormacionAcademica"].Range, oFila);
            }
        }
    }

    public static void ReemplazarDatosFAAT(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{Fecha}";
        fnd.Replacement.Text = oFila["T012_INICIO"].ToString() + " - " + oFila["T012_FIN"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{Titulo}";
        fnd.Replacement.Text = oFila["T019_DESCRIPCION"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{Centro}";
        fnd.Replacement.Text = oFila["T012_CENTRO"].ToString();
        ExecuteReplace(fnd);
    }

    #endregion

    #region Certificados
    public static void CargarCertificadosAT(DataView dv)
    {
        if (dv.Count == 0)
        {
            Word.Range rngTC = newDocument.Bookmarks["MKBloqueCertificaciones"].Range;
            rngTC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKCertificaciones"].Range;
            Word.Range tblrng = rng_datos.Tables[1].Range;
            tblrng.Cut();

            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();

            foreach (DataRowView oFila in dv)
            {
                wordApplication.Selection.Paste();          //Pega la tabla
                ReemplazarDatosCertificadosAT(newDocument.Bookmarks["MKCertificaciones"].Range, oFila);
            }
        }
    }

    public static void ReemplazarDatosCertificadosAT(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{Fecha}";
        fnd.Replacement.Text = oFila["FOBTENCION"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{Titulo}";
        fnd.Replacement.Text = oFila["TITULO"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{Proveedor}";
        fnd.Replacement.Text = oFila["PROVEEDOR"].ToString();
        ExecuteReplace(fnd);
    }

    #endregion

    #region Experiencias profesionales
    public static void CargarExperienciasAT(DataView dvE, DataView dvP)
    {
        if (dvE.Count == 0)
        {
            Word.Range rngC = newDocument.Bookmarks["MKBloqueExpProf"].Range;
            rngC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKExpProf"].Range;
            Word.Range tblrng = rng_datos.Tables[1].Range;
            tblrng.Cut();

            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 0;
            foreach (DataRowView oFila in dvE)
            {
                if (i > 0)
                {
                    rng_datos = newDocument.Bookmarks["MKExpProf"].Range;
                    rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                    rng_datos.Select();
                    wordApplication.Selection.TypeParagraph();
                }
                wordApplication.Selection.Paste();
                DataView perfil = new DataView(dvP.ToTable(), "t808_idexpprof= " + oFila["T808_IDEXPPROF"].ToString(), "FFIN, FINICIO", DataViewRowState.CurrentRows);
                ReemplazarDatosExperienciasAT(newDocument.Bookmarks["MKExpProf"].Range, oFila, perfil);
                i++;
            }
        }
    }

    public static void ReemplazarDatosExperienciasAT(Word.Range rng_exp, DataRowView oFila, DataView perfil)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_exp.Find;

        fnd.Text = "{fecha_exp}";
        fnd.Replacement.Text = oFila["FINICIO"].ToString() + "-" + ((oFila["FFIN"].ToString() == "") ? "Hoy" : oFila["FFIN"].ToString());
        ExecuteReplace(fnd);

        if (oFila["PERFILES"].ToString().Length < 250) // Normal execution
        {
            fnd.Text = "{perfil_exp}";
            fnd.Replacement.Text = oFila["PERFILES"].ToString();
            ExecuteReplace(fnd);
        }

        else
        {
            rng_exp.Select();
            object replaceAll = WdReplace.wdReplaceAll;
            object findMe = "{perfil_exp}";
            object replaceMe = oFila["PERFILES"].ToString();
            wordApplication.Selection.Find.ClearFormatting();
            wordApplication.Selection.Find.Text = (string)findMe;
            wordApplication.Selection.Find.Replacement.ClearFormatting();
            wordApplication.Selection.Find.Execute(findMe);

            wordApplication.Selection.Text = (string)replaceMe;
            rng_exp.SetRange(rng_exp.End - 1, rng_exp.End - 1);
            rng_exp.Select();
        }

        cumplimentarPerfilesExperienciasAT(perfil);
        //fnd.Text = "{funciones_exp}";
        //fnd.Replacement.Text = oFila["T813_FUNCION"].ToString();
        //ExecuteReplace(fnd);

        fnd.Text = "{cliente_exp}";
        fnd.Replacement.Text = oFila["CLIENTE"].ToString();
        ExecuteReplace(fnd);

        if (oFila["AREATEC"].ToString().Length < 250) // Normal execution
        {
            fnd.Text = "{tecnologias_exp}";
            fnd.Replacement.Text = oFila["AREATEC"].ToString();
            ExecuteReplace(fnd);
        }

        else
        {
            rng_exp.Select();
            object replaceAll = WdReplace.wdReplaceAll;
            object findMe = "{tecnologias_exp}";
            object replaceMe = oFila["AREATEC"].ToString();
            wordApplication.Selection.Find.ClearFormatting();
            wordApplication.Selection.Find.Text = (string)findMe;
            wordApplication.Selection.Find.Replacement.ClearFormatting();
            wordApplication.Selection.Find.Execute(findMe);

            wordApplication.Selection.Text = (string)replaceMe;
            rng_exp.SetRange(rng_exp.End - 1, rng_exp.End - 1);
            rng_exp.Select();
        }

        if (oFila["T808_DESCRIPCION"].ToString().Length < 250) // Normal execution
        {
            fnd.Text = "{proyecto_exp}";
            fnd.Replacement.Text = oFila["T808_DESCRIPCION"].ToString();
            ExecuteReplace(fnd);
        }

        else
        {
            rng_exp.Select();
            object replaceAll = WdReplace.wdReplaceAll;
            object findMe = "{proyecto_exp}";
            object replaceMe = oFila["T808_DESCRIPCION"].ToString();
            wordApplication.Selection.Find.ClearFormatting();
            wordApplication.Selection.Find.Text = (string)findMe;
            wordApplication.Selection.Find.Replacement.ClearFormatting();
            wordApplication.Selection.Find.Execute(findMe);

            wordApplication.Selection.Text = (string)replaceMe;
            rng_exp.SetRange(rng_exp.End - 1, rng_exp.End - 1);
            rng_exp.Select();
        }
    }

    public static void cumplimentarPerfilesExperienciasAT(DataView perfiles)
    {
        Word.Range rng_datos = newDocument.Bookmarks["MKPerfil"].Range;

        if (perfiles.Count == 0)
        {
            rng_datos.Delete();
        }
        else
        {
            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 0;
            foreach (DataRowView oFila in perfiles)
            {
                if (i > 0) { wordApplication.Selection.TypeParagraph(); wordApplication.Selection.TypeParagraph(); }
                if (perfiles.Count > 1)
                {
                    wordApplication.Selection.TypeText(oFila["DESCRIPCION"].ToString().ToUpper());
                    wordApplication.Selection.TypeParagraph();
                }
                wordApplication.Selection.TypeText(oFila["FUNCION"].ToString());
                i++;
            }
        }
        newDocument.Bookmarks["MKPerfil"].Delete();
    }


    #endregion

    #region Idiomas
    public static void CargarIdiomasAT(DataView dv)
    {
        if (dv.Count == 0)
        {
            Word.Range rngC = newDocument.Bookmarks["MKBloqueIdioma"].Range;
            rngC.Delete();
        }
        else
        {
            Word.Range rng_datos = newDocument.Bookmarks["MKIdioma"].Range;
            rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
            rng_datos.Select();
            int i = 2;
            foreach (DataRowView oFila in dv)
            {
                if (i != 2) rng_datos.Tables[1].Rows.Add(); //tenemos una fila como muestra para la letra y estilos
                ReemplazarDatosIdiomaAT(rng_datos.Tables[1].Rows[i], oFila);
                i++;
            }
        }
    }

    public static void ReemplazarDatosIdiomaAT(Word.Row oRow, DataRowView oFila)
    {
        oRow.Cells[1].Select();
        wordApplication.Selection.TypeText(oFila["T020_DESCRIPCION"].ToString());
        oRow.Cells[2].Select();
        wordApplication.Selection.TypeText(oFila["T013_LECTURA"].ToString());
        oRow.Cells[3].Select();
        wordApplication.Selection.TypeText(oFila["T013_ORAL"].ToString());
        oRow.Cells[4].Select();
        wordApplication.Selection.TypeText(oFila["T013_ESCRITURA"].ToString());
        oRow.Cells[5].Select();
        wordApplication.Selection.TypeText(oFila["T021_TITULO"].ToString());
    }
    #endregion

    #region Cursos
    public static void CargarCursosAT(DataView dvCursosImp, DataView dvCursosRec, DataView dvExamenes)
    {
        if (dvCursosImp.Count == 0 && dvCursosRec.Count == 0 && dvExamenes.Count == 0)
        {
            Word.Range rngFC = newDocument.Bookmarks["MKBloqueFormacionComp"].Range;
            rngFC.Delete();
        }
        else
        {
            if (dvCursosImp.Count == 0)
            {
                Word.Range rngCI = newDocument.Bookmarks["MKCursosImpartidos"].Range;
                rngCI.Delete();
            }
            else
            {
                Word.Range tblrng = newDocument.Bookmarks["MKCursosImpartidos"].Range.Tables[1].Range;
                tblrng.Cut();
                Word.Range rng_datos = newDocument.Bookmarks["MKCursosImpartidos"].Range;
                //rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                tblrng.Select();

                foreach (DataRowView oFila in dvCursosRec)
                {
                    wordApplication.Selection.Paste();          //Pega la tabla
                    ReemplazarDatosCursosAT(newDocument.Bookmarks["MKCursosImpartidos"].Range, oFila);
                }
            }
            if (dvCursosRec.Count == 0)
            {
                Word.Range rngCR = newDocument.Bookmarks["MKCursosRecibidos"].Range;
                rngCR.Delete();
            }
            else
            {
                Word.Range tblrng = newDocument.Bookmarks["MKCursosRecibidos"].Range.Tables[1].Range;
                tblrng.Cut();

                Word.Range rng_datos = newDocument.Bookmarks["MKCursosRecibidos"].Range;
                rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                tblrng.Select();

                foreach (DataRowView oFila in dvCursosRec)
                {
                    wordApplication.Selection.Paste();          //Pega la tabla
                    ReemplazarDatosCursosAT(newDocument.Bookmarks["MKCursosRecibidos"].Range, oFila);
                }

            }
            if (dvExamenes.Count == 0)
            {
                Word.Range rngEx = newDocument.Bookmarks["MKCursosExamenes"].Range;
                rngEx.Delete();
            }
            else
            {
                Word.Range tblrng = newDocument.Bookmarks["MKCursosExamenes"].Range.Tables[1].Range;
                tblrng.Select();
                tblrng.Cut();

                Word.Range rng_datos = newDocument.Bookmarks["MKCursosExamenes"].Range;
                rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                rng_datos.Select();
               

                foreach (DataRowView oFila in dvExamenes)
                {
                    wordApplication.Selection.Paste();          //Pega la tabla
                    ReemplazarDatosExamenesAT(newDocument.Bookmarks["MKCursosExamenes"].Range, oFila);
                }
            }

        }

    }

    public static void ReemplazarDatosCursosAT(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{Fecha}";
        fnd.Replacement.Text = oFila["FFIN"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{horas}";
        fnd.Replacement.Text = oFila["T574_HORAS"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{Titulo}";
        fnd.Replacement.Text = oFila["T574_TITULO"].ToString();
        ExecuteReplace(fnd);
        fnd.Text = "{Proveedor}";
        fnd.Replacement.Text = oFila["PROVEEDOR"].ToString();
        ExecuteReplace(fnd);
    }
    #endregion

    #region Exámenes
    public static void ReemplazarDatosExamenesAT(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;

        fnd.Text = "{fecha}";
        fnd.Replacement.Text = oFila["FOBTENCION"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{titulo}";
        fnd.Replacement.Text = oFila["TITULO"].ToString();
        ExecuteReplace(fnd);
        fnd.Text = "{numero}";
        fnd.Replacement.Text = oFila["ABREVIATURA"].ToString();
        ExecuteReplace(fnd);
    }
    #endregion

    #endregion PlantillaAT
     

    #region Funciones para buscar y reemplazar

    private static Boolean ExecuteReplace(Word.Find find)
    {
        return ExecuteReplace(find, WdReplace.wdReplaceAll);
    }

    private static Boolean ExecuteReplace(Word.Find find, Object replaceOption)
    {
        // Simple wrapper around Find.Execute:


        Object findText = Type.Missing;
        Object matchCase = Type.Missing;
        Object matchWholeWord = Type.Missing;
        Object matchWildcards = Type.Missing;
        Object matchSoundsLike = Type.Missing;
        Object matchAllWordForms = Type.Missing;
        Object forward = Type.Missing;
        Object wrap = Type.Missing;
        Object format = Type.Missing;
        Object replaceWith = Type.Missing;
        Object replace = replaceOption;


        return find.Execute(findText, matchCase,
            matchWholeWord, matchWildcards, matchSoundsLike,
            matchAllWordForms, forward, wrap, format,
            replaceWith, replace);
    }

    private static Boolean ExecuteFind(Word.Find find)
    {
        return ExecuteFind(find, Type.Missing, Type.Missing);
    }

    private static Boolean ExecuteFind(
      Word.Find find, Object wrapFind, Object forwardFind)
    {
        // Simple wrapper around Find.Execute:
        Object findText = Type.Missing;
        Object matchCase = Type.Missing;
        Object matchWholeWord = Type.Missing;
        Object matchWildcards = Type.Missing;
        Object matchSoundsLike = Type.Missing;
        Object matchAllWordForms = Type.Missing;
        Object forward = forwardFind;
        Object wrap = wrapFind;

        return find.Execute(findText, matchCase,
            matchWholeWord, matchWildcards, matchSoundsLike,
            matchAllWordForms, forward, wrap);
    }

    #endregion

    #region Funcion para comprimir documentos

    public static byte[] CompressFile(string path)
    {
        string sPathDocs = "d:\\tmp\\word\\";

        DirectoryInfo directorySelected = new DirectoryInfo(path);
        ZipFile zip = new ZipFile(sPathDocs.Replace("\\", @"\") + "CV.zip");
        zip.AddDirectory(path);
        zip.Save();

        //byte[] cv = FileToByteArray(sPathDocs.Replace("\\", @"\") + "CV.zip");
        byte[] cv = System.IO.File.ReadAllBytes(sPathDocs.ToString() + "CV.zip");
        File.Delete(sPathDocs + "CV.zip");

        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            File.Delete(file);
        }
        return cv;
    }

    public static int tamanoImg(Word.InlineShape img)
    {
        return (int)(widthImg * img.Height / img.Width);
    }

    #endregion


    public static string GetDefaultExtension(Word.Application application, string sExtensionFile)
    {
        double Version = Convert.ToDouble(application.Version, CultureInfo.InvariantCulture);
        if (Version >= 12.00 && sExtensionFile == ".docx")
            return ".docx";
        else
            return ".doc";
    }
}