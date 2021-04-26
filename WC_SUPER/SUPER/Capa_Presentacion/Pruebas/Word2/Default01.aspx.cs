using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Globalization;
using NetOffice;
using Word = NetOffice.WordApi;
using NetOffice.WordApi.Enums;
using SUPER.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using SUPER.Capa_Datos;

public partial class Capa_Presentacion_Pruebas_Word2_Default01 : System.Web.UI.Page
{
    Word.Application wordApplication = null;// = new Word.Application();
    string sPathDocs = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlParameter[] aParam = new SqlParameter[1];
        string[] idficepi = Regex.Split((string)"12157", ",");
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("CODIGOINT", typeof(int)));
        for (int x = 0; x < idficepi.Length; x++)
        {
            DataRow row = dt.NewRow();
            row["CODIGOINT"] = idficepi[x];
            dt.Rows.Add(row);
        }
        aParam[0] = ParametroSql.add("@tbl_idficepi", SqlDbType.Structured, 8000, dt);
        DataSet ds = SqlHelper.ExecuteDataset("SUP_CVT_PLANTILLA_CVC", aParam);

        
        sPathDocs = Request.PhysicalApplicationPath + "Capa_Presentacion\\Pruebas\\Word2\\docs\\";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        // start word and turn off msg boxes 
        //Word.Application wordApplication = null;// = new Word.Application();

        try
        {
            wordApplication = new Word.Application();
            wordApplication.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            wordApplication.Visible = false;

            // add a new document
            Object oDoc = sPathDocs + @"CVofertasAT02.doc";
            Word.Document newDocument = wordApplication.Documents.Add(oDoc);

            ArrayList aList = new ArrayList();
            aList.Add(new string[] { "Pepe", "111111111A", "GERENTE", "25/11/1949" });
            aList.Add(new string[] { "María", "222222222B", "COMERCIAL", "15/02/1982" });
            aList.Add(new string[] { "Nerea", "333333333C", "PROGRAMADOR SENIOR", "24/06/1972" });
            aList.Add(new string[] { "Xabi", "444444444H", "PROGRAMADOR JUNIOR", "14/01/1981" });

            //Crea un rango en base al bookmark "MkDatosPersonales"
            Word.Range rngDP = newDocument.Bookmarks["MkDatosPersonales"].Range;
            CargarDatosPersonales(rngDP, (string[])aList[0]);
            Word.Range rngFA = newDocument.Bookmarks["MkTablaFormacionAcademica"].Range;
            CargarFormacionAcademica(rngFA, aList);

            /*
            //Copia el "contenido" de la tabla (incluída) de la primera tabla (comienza índice 1) del bookmark "Tabla01"
            Word.Range tblrng = newDocument.Bookmarks["Tabla02"].Range.Tables[1].Range;
            tblrng.Copy();


            Word.Selection currentSelection;

            for (int i = 0; i < aList.Count; i++)
            {
                if (i == 0) //la tabla existente y copiada
                {
                    ReemplazarDatos(tblrng, (string[])aList[i]);
                }
                else //resto de tablas pegadas
                {
                    //Redimensiona/reposiciona el rango dos caracteres después, con el mismo inicio y final
                    rng.SetRange(rng.End + 2, rng.End + 2);
                    //y se selecciona el rango
                    rng.Select();
                    //Como se ha establecido el mismo inicio y final, el rango seleccionado es un punto de inserción
                    currentSelection = wordApplication.Selection;
                    // Test to see if selection is an insertion point. 
                    if (currentSelection.Type == WdSelectionType.wdSelectionIP)
                    {
                        currentSelection.TypeParagraph();
                    }

                    //Redimensiona/reposiciona el rango dos caracteres después, con el mismo inicio y final
                    rng.SetRange(rng.End + 2, rng.End + 2);
                    //y se selecciona el rango
                    //                rng.Select();
                    //Pegamos el rango copiado anteriormente, que contiene la tabla.
                    rng.Paste();
                    //rng.Select();
                    ReemplazarDatos(rng, (string[])aList[i]);
                }
            }
            */
            //Para que borre los marcadores que pudiera haber.
            foreach (Word.Bookmark bkmk in newDocument.Bookmarks)
            {
                bkmk.Delete();
            }

            // we save the document as .doc for compatibility with all word versions
            string documentFile = string.Format("{0}\\Prueba{1}", sPathDocs, ".doc");
            double wordVersion = Convert.ToDouble(wordApplication.Version, CultureInfo.InvariantCulture);
            if (wordVersion >= 12.0)
                newDocument.SaveAs(documentFile, WdSaveFormat.wdFormatDocumentDefault);
            else
                newDocument.SaveAs(documentFile);

            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;

            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + "Prueba.doc" + "\"");
            Response.BinaryWrite(SUPER.Capa_Negocio.Utilidades.FileToByteArray(documentFile));

            Response.Flush();
            //Response.Close();
            //Response.End();

        }
        catch (Exception)
        {

        }
        finally
        {
            // close word and dispose reference
            if (wordApplication != null)
            {
                wordApplication.Quit();
                wordApplication.Dispose();
            }
        }
    }

    private void CargarDatosPersonales(Word.Range rng_datos, string[] aDatos)
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
    private void CargarFormacionAcademica(Word.Range rng_datos, ArrayList aList)
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

    private void ReemplazarDatos(Word.Range rng_datos, string[] aDatos)
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
