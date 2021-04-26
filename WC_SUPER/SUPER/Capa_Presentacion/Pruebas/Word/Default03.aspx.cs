using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Globalization;
using NetOffice;
using Word = NetOffice.WordApi;
using NetOffice.WordApi.Enums;

using System.Data;

public partial class Capa_Presentacion_Pruebas_Word_Default03 : System.Web.UI.Page
{
    Word.Application wordApplication = null;// = new Word.Application();
    Word.Document newDocument = null;
    string sPathDocs = "";
    string documentFile = "";
    bool bErrores = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        sPathDocs = Request.PhysicalApplicationPath + "Capa_Presentacion\\Pruebas\\Word\\docs\\";

        // start word and turn off msg boxes 
        //Word.Application wordApplication = null;// = new Word.Application();

        try
        {
            wordApplication = new Word.Application();
            wordApplication.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            wordApplication.Visible = true;

            // add a new document
            Object oDoc = sPathDocs + @"CVofertasAT02.doc";
            newDocument = wordApplication.Documents.Add(oDoc);

            DataSet ds = SUPER.DAL.Curriculum.ObtenerProfParaCVWord02(null, "1568", false, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            //Crea un rango en base al bookmark "MkDatosPersonales"
            Word.Range rngDP = newDocument.Bookmarks["MkDatosPersonales"].Range;
            //CargarDatosPersonales(rngDP, (string[])aList[0]);
            CargarDatosPersonales(rngDP, ds.Tables["DatosPersonales"].Rows[0]);

            Word.Range rngFA = newDocument.Bookmarks["MkTablaFormacionAcademica"].Range;
            DataView dvFA = new DataView(ds.Tables["FormacionAcademica"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[0]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
            //CargarFormacionAcademica(rngFA, aList);
            CargarFormacionAcademica(rngFA, dvFA);

            Word.Range rngCER = newDocument.Bookmarks["MkTablaCertificaciones"].Range;
            DataView dvCER = new DataView(ds.Tables["Certificados"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[0]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
            CargarCertificados(rngCER, dvCER);

            Word.Range rngEXP = newDocument.Bookmarks["MkTablaExperiencia"].Range;
            DataView dvEXP = new DataView(ds.Tables["Experiencia"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[0]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
            CargarExperiencias(rngEXP, dvEXP);

            Word.Range rngIDI = newDocument.Bookmarks["MkTablaIdioma"].Range;
            DataView dvIDI = new DataView(ds.Tables["Idiomas"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[0]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
            CargarIdiomas(rngIDI, dvIDI);

            Word.Range rngComPer = newDocument.Bookmarks["MkCompetenciasPersonales"].Range;
            DataView dvComPer = new DataView(ds.Tables["CompetenciasPersonales"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[0]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
            CargarCompeteciasPersonales(rngComPer, dvComPer);

            Word.Range rngComTec = newDocument.Bookmarks["MkCompetenciasTecnicas"].Range;
            DataView dvComTec = new DataView(ds.Tables["CompetenciasTecnicas"], "t001_idficepi = " + ds.Tables["DatosPersonales"].Rows[0]["t001_idficepi"].ToString(), "t001_idficepi", DataViewRowState.CurrentRows);
            CargarCompeteciasTecnicas(rngComTec, dvComTec);

            //Para que borre los marcadores que pudiera haber.
            foreach (Word.Bookmark bkmk in newDocument.Bookmarks)
            {
                bkmk.Delete();
            }

            // we save the document as .doc for compatibility with all word versions
            documentFile = string.Format("{0}Prueba{1}", sPathDocs, ".doc");
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
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;

            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + "Prueba.doc" + "\"");
            Response.BinaryWrite(SUPER.Capa_Negocio.Utilidades.FileToByteArray(documentFile));

            Response.Flush();
            Response.Close();
            Response.End();
        }
    }

    private void CargarDatosPersonales_old(Word.Range rng_datos, string[] aDatos)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_datos.Find;
        //Clear existing formatting;
        fnd.ClearFormatting();
        fnd.Replacement.ClearFormatting();

        fnd.Text = "{nombre}";
        fnd.Replacement.Text = aDatos[0];// "Nombre Profesional";
        ExecuteReplace(fnd);

        fnd.Text = "{nif}";
        fnd.Replacement.Text = aDatos[1];// "123456789Z";
        ExecuteReplace(fnd);

        fnd.Text = "{perfil}";
        fnd.Replacement.Text = aDatos[2];// "GERENTE";
        ExecuteReplace(fnd);

        fnd.Text = "{fnacimiento}";
        fnd.Replacement.Text = aDatos[3];// "15/02/1973";
        ExecuteReplace(fnd);

        fnd.Text = "{foto}";
        fnd.Replacement.Text = "";// "15/02/1973";
        ExecuteFind(fnd);
        rng_datos.Select();
        rng_datos.SetRange(rng_datos.Start, rng_datos.End);

        Word.Selection currentSelection = wordApplication.Selection;
        currentSelection.MoveRight();
        currentSelection.InlineShapes.AddPicture("D:\\Proyectos\\PRUEBAS\\WordTest01\\images\\01.jpg");

        ExecuteReplace(fnd);
    }
    private void CargarDatosPersonales(Word.Range rng_datos, DataRow oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_datos.Find;
        //Clear existing formatting;
        fnd.ClearFormatting();
        fnd.Replacement.ClearFormatting();

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
        fnd.Replacement.Text = ((DateTime)oFila["t001_fecnacim"]).ToShortDateString();
        ExecuteReplace(fnd);

        fnd.Text = "{foto}";
        fnd.Replacement.Text = "";// "15/02/1973";
        ExecuteFind(fnd);
        rng_datos.Select();
        rng_datos.SetRange(rng_datos.Start, rng_datos.End);

        bool bHayFoto = false;
        if (oFila["t001_foto"] != DBNull.Value)
        {
            bHayFoto = true;
            //sPathDocs
            SUPER.Capa_Negocio.Utilidades.ByteArrayToFile(sPathDocs +"..\\temp\\foto.jpg", (byte[])oFila["t001_foto"]);
        }

        if (bHayFoto)
        {
            Word.Selection currentSelection = wordApplication.Selection;
            currentSelection.MoveRight();
            //currentSelection.InlineShapes.AddPicture("D:\\Proyectos\\PRUEBAS\\WordTest01\\images\\01.jpg");
            currentSelection.InlineShapes.AddPicture(sPathDocs + "..\\temp\\foto.jpg");
            System.IO.File.Delete(sPathDocs + "..\\temp\\foto.jpg");
        }
        ExecuteReplace(fnd);
    }

    private void CargarFormacionAcademica_old(Word.Range rng_datos, ArrayList aList)
    {
        // aDatos
        foreach (string[] aDatos in aList)
        {
            rng_datos.Tables[1].Rows.Add();
            rng_datos.Tables[1].Rows.Last.Cells[1].Select();
            wordApplication.Selection.TypeText(aDatos[0]);
            rng_datos.Tables[1].Rows.Last.Cells[2].Select();
            wordApplication.Selection.TypeText(aDatos[1]);
            rng_datos.Tables[1].Rows.Last.Cells[3].Select();
            wordApplication.Selection.TypeText(aDatos[3]);
        }
    }
    private void CargarFormacionAcademica(Word.Range rng_datos, DataView dv)
    {
        // aDatos
        foreach (DataRowView oFila in dv)
        {
            rng_datos.Tables[1].Rows.Add();
            rng_datos.Tables[1].Rows.Last.Cells[1].Select();
            wordApplication.Selection.TypeText(oFila["T019_DESCRIPCION"].ToString());
            rng_datos.Tables[1].Rows.Last.Cells[2].Select();
            wordApplication.Selection.TypeText(oFila["T012_CENTRO"].ToString());
            rng_datos.Tables[1].Rows.Last.Cells[3].Select();
            wordApplication.Selection.TypeText(oFila["T012_INICIO"].ToString() + " - " + oFila["T012_FIN"].ToString());
        }
    }
    
    private void CargarCertificados(Word.Range rng_datos, DataView dv)
    {
        // aDatos
        foreach (DataRowView oFila in dv)
        {
            rng_datos.Tables[1].Rows.Add();
            rng_datos.Tables[1].Rows.Last.Cells[1].Select();
            wordApplication.Selection.TypeText(oFila["TITULO"].ToString());
            rng_datos.Tables[1].Rows.Last.Cells[2].Select();
            wordApplication.Selection.TypeText(oFila["PROVEEDOR"].ToString());
            rng_datos.Tables[1].Rows.Last.Cells[3].Select();
            wordApplication.Selection.TypeText(oFila["FOBTENCION"].ToString());
        }
    }
    private void CargarExperiencias(Word.Range rng_datos, DataView dv)
    {
        Word.Range tblrng = rng_datos.Tables[1].Range;
        tblrng.Cut();

        if (dv.Count == 0)
        {

        }
        else
        {
            rng_datos.SetRange(rng_datos.End-1, rng_datos.End-1);
            rng_datos.Select();
            //Como se ha establecido el mismo inicio y final, el rango seleccionado es un punto de inserción
            //Word.Selection currentSelection = wordApplication.Selection;
            // Test to see if selection is an insertion point. 
            //if (wordApplication.Selection.Type == WdSelectionType.wdSelectionIP)
            //{
            //    wordApplication.Selection.TypeParagraph();
            //    //wordApplication.Selection.MoveRight(Word.Enums.WdUnits.wdCharacter, 1); 
            //    //wordApplication.Selection.TypeText("0");
            //    //wordApplication.Selection.TypeParagraph();
            //}

            int i = 0;
            foreach (DataRowView oFila in dv)
            {
                //wordApplication.Selection.TypeText(i.ToString());
                //wordApplication.Selection.MoveRight(Word.Enums.WdUnits.wdCharacter, 1);
                if (i>0) wordApplication.Selection.TypeParagraph();
                wordApplication.Selection.Paste();
                //rng_datos.SetRange(rng_datos.Start, rng_datos.End);
                //rng_datos.Select();
                ReemplazarDatosExperiencias(newDocument.Bookmarks["MkTablaExperiencia"].Range.Tables.Last().Range, oFila);
                i++;
            }
        }
        // aDatos
    }
    private void ReemplazarDatosExperiencias(Word.Range rng_exp, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_exp.Find;
        //Clear existing formatting;
        fnd.ClearFormatting();
        fnd.Replacement.ClearFormatting();

        fnd.Text = "{fecha_exp}";
        fnd.Replacement.Text = oFila["T812_FINICIO"].ToString() + " - " + oFila["T812_FFIN"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{funcion_exp}";
        fnd.Replacement.Text = oFila["T035_DESCRIPCION"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{cliente_exp}";
        fnd.Replacement.Text = oFila["CLIENTE"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{tareas_exp}";
        fnd.Replacement.Text = oFila["T813_FUNCION"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{tecnologias_exp}";
        fnd.Replacement.Text = oFila["ENTORNO"].ToString();
        ExecuteReplace(fnd);
    }

    private void CargarIdiomas(Word.Range rng_datos, DataView dv)
    {
        Word.Range tblrng = rng_datos.Tables[1].Range;
        tblrng.Cut();

        if (dv.Count == 0)
        {

        }
        else
        {
            rng_datos.SetRange(rng_datos.End-1, rng_datos.End-1);
            rng_datos.Select();

            int i = 0;
            byte nIdioma = 0;
            foreach (DataRowView oFila in dv)
            {
                //wordApplication.Selection.TypeText(i.ToString());
                //wordApplication.Selection.MoveRight(Word.Enums.WdUnits.wdCharacter, 1);
                if (i>0) wordApplication.Selection.TypeParagraph();
                if (byte.Parse(oFila["T020_IDCODIDIOMA"].ToString()) != nIdioma) //Si el idioma es diferente al anterior
                {
                    wordApplication.Selection.Paste();          //Pega la tabla
                    nIdioma = byte.Parse(oFila["T020_IDCODIDIOMA"].ToString());
                    if (oFila["T021_IDTITULOIDIOMA"] == DBNull.Value) //Si no hay titulación, borro la fila
                    {
                        newDocument.Bookmarks["MkTablaIdioma"].Range.Tables.Last().Rows.Last().Delete();
                    }
                    ReemplazarDatosIdioma(newDocument.Bookmarks["MkTablaIdioma"].Range.Tables.Last().Range, oFila);
                }
                else
                {
                    newDocument.Bookmarks["MkTablaIdioma"].Range.Tables.Last().Rows.Add();
                    newDocument.Bookmarks["MkTablaIdioma"].Range.Tables.Last().Rows.Last().Cells[2].Range.Select();
                    wordApplication.Selection.TypeText(oFila["T021_TITULO"].ToString());
                }
                
                //rng_datos.SetRange(rng_datos.Start, rng_datos.End);
                //rng_datos.Select();
                i++;
            }
        }
        // aDatos
    }
    private void ReemplazarDatosIdioma(Word.Range rng_idi, DataRowView oFila)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_idi.Find;
        //Clear existing formatting;
        fnd.ClearFormatting();
        fnd.Replacement.ClearFormatting();

        fnd.Text = "{idioma}";
        fnd.Replacement.Text = oFila["T020_DESCRIPCION"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{nivel_lectura}";
        fnd.Replacement.Text = oFila["T013_LECTURA"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{nivel_escritura}";
        fnd.Replacement.Text = oFila["T013_ESCRITURA"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{nivel_conversacion}";
        fnd.Replacement.Text = oFila["T013_ORAL"].ToString();
        ExecuteReplace(fnd);

        fnd.Text = "{titulo_idioma}";
        fnd.Replacement.Text = oFila["T021_TITULO"].ToString();
        ExecuteReplace(fnd);
    }

    private void CargarCompeteciasPersonales(Word.Range rng_datos, DataView dv)
    {
        Word.Paragraph oParagraph = rng_datos.Paragraphs.Add();
        oParagraph.Range.ListFormat.ApplyBulletDefault();

        int i=0;
        foreach (DataRowView oFila in dv)
        {
            string sDato = oFila["t574_titulo"].ToString();
            if (i < dv.Table.Rows.Count - 1){
                sDato = sDato + "\n";
            }
            oParagraph.Range.InsertBefore(sDato);
            i++;
        }
    }
    private void CargarCompeteciasTecnicas(Word.Range rng_datos, DataView dv)
    {
        Word.Paragraph oParagraph = rng_datos.Paragraphs.Add();
        oParagraph.Range.ListFormat.ApplyBulletDefault();

        int i = 0;
        foreach (DataRowView oFila in dv)
        {
            string sDato = oFila["T036_DESCRIPCION"].ToString();
            if (i < dv.Table.Rows.Count - 1)
            {
                sDato = sDato + "\n";
            }
            oParagraph.Range.InsertBefore(sDato);
            i++;
        }
    }

    private void CargarExperiencias_old(Word.Range rng_datos, DataView dv)
    {
        Word.Range tblrng = rng_datos.Tables[1].Range;
        tblrng.Copy();

        if (dv.Count == 0)
        {

        }
        else
        {

            int i = 0;
            foreach (DataRowView oFila in dv)
            {
                //Redimensiona/reposiciona el rango dos caracteres después, con el mismo inicio y final
                rng_datos.SetRange(rng_datos.End - 1, rng_datos.End - 1);
                ////y se selecciona el rango
                rng_datos.Select();

                //Como se ha establecido el mismo inicio y final, el rango seleccionado es un punto de inserción
                Word.Selection currentSelection = wordApplication.Selection;
                // Test to see if selection is an insertion point. 
                if (currentSelection.Type == WdSelectionType.wdSelectionIP)
                {
                    currentSelection.TypeParagraph();
                    wordApplication.Selection.MoveRight(Word.Enums.WdUnits.wdCharacter, 1);
                    wordApplication.Selection.TypeText(i.ToString());
                    wordApplication.Selection.MoveRight(Word.Enums.WdUnits.wdCharacter, 1);
                    //                    wordApplication.Selection.TypeText(" ");
                    //rng_datos.SetRange(rng_datos.End+1, rng_datos.End+1);
                    ////y se selecciona el rango
                    //rng_datos.Select();
                    //currentSelection.TypeParagraph();
                    //currentSelection.TypeParagraph();
                }
                //                //Redimensiona/reposiciona el rango dos caracteres después, con el mismo inicio y final
                //                rng_datos.SetRange(rng_datos.End + 2, rng_datos.End + 2);
                //y se selecciona el rango
                //                rng.Select();
                //Pegamos el rango copiado anteriormente, que contiene la tabla.
                //                currentSelection = wordApplication.Selection;
                //                currentSelection.Select();
                currentSelection = wordApplication.Selection;
                currentSelection.Paste();
                //rng_datos.Paste();
                //rng.Select();
                rng_datos.SetRange(rng_datos.End + 1, rng_datos.End + 1);

                //                rng_datos.Tables[rng_datos.Tables.Count].Rows[1].Cells[2].Select();
                //                wordApplication.Selection.TypeText(oFila["T812_FINICIO"].ToString());

                //rng_datos.Tables[1].Rows.Add();
                //rng_datos.Tables[1].Rows.Last.Cells[1].Select();
                //wordApplication.Selection.TypeText(oFila["TITULO"].ToString());
                //rng_datos.Tables[1].Rows.Last.Cells[2].Select();
                //wordApplication.Selection.TypeText(oFila["PROVEEDOR"].ToString());
                //rng_datos.Tables[1].Rows.Last.Cells[3].Select();
                //wordApplication.Selection.TypeText(oFila["FOBTENCION"].ToString());
                i++;
            }
        }
        // aDatos
    }

    private void ReemplazarDatos_old(Word.Range rng_datos, string[] aDatos)
    {
        //Ejemplo de Buscar y reemplazar el nombre dentro del rango.
        Word.Find fnd = rng_datos.Find;
        //Clear existing formatting;
        fnd.ClearFormatting();
        fnd.Replacement.ClearFormatting();

        fnd.Text = "{nombre}";
        fnd.Replacement.Text = aDatos[0];// "Nombre Profesional";
        ExecuteReplace(fnd);

        fnd.Text = "{nif}";
        fnd.Replacement.Text = aDatos[1];// "123456789Z";
        ExecuteReplace(fnd);

        fnd.Text = "{perfil}";
        fnd.Replacement.Text = aDatos[2];// "GERENTE";
        ExecuteReplace(fnd);

        fnd.Text = "{fnacim}";
        fnd.Replacement.Text = aDatos[3];// "15/02/1973";
        ExecuteReplace(fnd);

        fnd.Text = "{foto}";
        fnd.Replacement.Text = "";// "15/02/1973";
        ExecuteFind(fnd);
        rng_datos.Select();
        rng_datos.SetRange(rng_datos.Start, rng_datos.End);

        Word.Selection currentSelection = wordApplication.Selection;
        currentSelection.MoveRight();
        currentSelection.InlineShapes.AddPicture("D:\\Proyectos\\PRUEBAS\\WordTest01\\images\\01.jpg");

        ExecuteReplace(fnd);

        //TableRow theRow = theTable.Elements<TableRow>().Last();
        //Word.Row oLastRow = newDocument.Bookmarks["Tabla01"].Range.Tables[1].Rows.Last;
        // Word.Row oLastRow = rng_datos.Tables[1].Rows.Last;
        rng_datos.Tables[1].Rows.Add();
        rng_datos.Tables[1].Rows.Last.Cells[1].Select();
        wordApplication.Selection.TypeText("Texto en la primera celda");
        rng_datos.Tables[1].Rows.Last.Cells[2].Select();
        wordApplication.Selection.TypeText("Texto en la segunda celda");
        rng_datos.Tables[1].Rows.Last.Cells[3].Select();
        wordApplication.Selection.TypeText("Texto en la tercera celda");
        //oLastRow
        //TableRow rowCopy = (TableRow)theRow.CloneNode(true);
        //rowCopy.Descendants<TableCell>().ElementAt(0).Append(new Paragraph(new Run(new Text(order.Contact.ToString()))));
        //rowCopy.Descendants<TableCell>().ElementAt(1).Append(new Paragraph(new Run(new Text(order.NameOfProduct.ToString()))));
        //rowCopy.Descendants<TableCell>().ElementAt(2).Append(new Paragraph(new Run(new Text(order.Amount.ToString()))));
        //theTable.AppendChild(rowCopy);
        //Word.Range rowrng = oLastRow.Range;
        //rowrng.Copy();

    }
    private Boolean ExecuteReplace(Word.Find find)
    {
        return ExecuteReplace(find, WdReplace.wdReplaceAll);
    }
    private Boolean ExecuteReplace(Word.Find find, Object replaceOption)
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
        Object matchKashida = Type.Missing;
        Object matchDiacritics = Type.Missing;
        Object matchAlefHamza = Type.Missing;
        Object matchControl = Type.Missing;

        return find.Execute(findText, matchCase,
            matchWholeWord, matchWildcards, matchSoundsLike,
            matchAllWordForms, forward, wrap, format,
            replaceWith, replace, matchKashida,
            matchDiacritics, matchAlefHamza, matchControl);
    }

    private Boolean ExecuteFind(Word.Find find)
    {
        return ExecuteFind(find, Type.Missing, Type.Missing);
    }

    private Boolean ExecuteFind(
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
        Object format = Type.Missing;
        Object replaceWith = Type.Missing;
        Object replace = Type.Missing;
        Object matchKashida = Type.Missing;
        Object matchDiacritics = Type.Missing;
        Object matchAlefHamza = Type.Missing;
        Object matchControl = Type.Missing;

        return find.Execute(findText, matchCase,
            matchWholeWord, matchWildcards, matchSoundsLike,
            matchAllWordForms, forward, wrap, format,
            replaceWith, replace, matchKashida,
            matchDiacritics, matchAlefHamza, matchControl);
    }

}
