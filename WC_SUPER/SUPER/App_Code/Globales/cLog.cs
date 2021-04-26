using System;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace SUPER.Capa_Negocio
{
    public class cLog
    {
        public cLog()
        {
        }

        public void put(string sLinea)
        {
            //string sFile = HttpContext.Current.Request.PhysicalApplicationPath + "Upload\\" + "logApp.txt";
            //FileStream fs = new FileStream(sFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            //StreamWriter w = new StreamWriter(fs); // create a stream writer 
            //w.BaseStream.Seek(0, SeekOrigin.End); // set the file pointer to the end of file 
            //w.WriteLine(sLinea);
            //w.Flush(); // update underlying file
            //w.Close();
            SUPER.DAL.Log.Insertar(sLinea);
        }

    }
}
