using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using SUPER.Capa_Negocio;
using System.Threading;
using System.Net.Mail;
using System.ComponentModel;

public partial class Capa_Presentacion_Pruebas_Thread_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
//    protected void Button1_Click(object sender, EventArgs e)
//    {

//        ThreadStart ts = new ThreadStart(GenerarCorreo);
//        Thread workerThread = new Thread(ts);
//        workerThread.Start();

//        //BackgroundWorker worker = new BackgroundWorker();
//        //worker.DoWork += new DoWorkEventHandler(GenerarCorreo);
//        //worker.RunWorkerAsync();

//    }

//    private void GenerarCorreo()
////    private void GenerarCorreo(object sender, DoWorkEventArgs e)
//    {
//        Thread.Sleep(30000);

//        ArrayList aListCorreo = new ArrayList();
//        StringBuilder sbuilder = new StringBuilder();
//        //        string sMensaje = "";
//        string sAsunto = "Test Thread Sleep 30000";
//        string sTO = "dogapena";

//        string[] aMail = { sAsunto, "Texto de prueba", sTO };
//        aListCorreo.Add(aMail);

//        Correo.EnviarCorreos(aListCorreo);


//    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        BackgroundWorker bw = new BackgroundWorker();
        bw.DoWork += GenerarCorreo2;
        bw.RunWorkerAsync();
        //ThreadStart ts = new ThreadStart(GenerarCorreo);
        //Thread workerThread = new Thread(ts);
        //workerThread.Start();

        //BackgroundWorker worker = new BackgroundWorker();
        //worker.DoWork += new DoWorkEventHandler(GenerarCorreo);
        //worker.RunWorkerAsync();

    }
    private static void GenerarCorreo2(object sender, DoWorkEventArgs e)
    //    private void GenerarCorreo(object sender, DoWorkEventArgs e)
    {
        Thread.Sleep(30000);

        ArrayList aListCorreo = new ArrayList();
        StringBuilder sbuilder = new StringBuilder();
        //        string sMensaje = "";
        string sAsunto = "Test BackgroundWorker Sleep 30000";
        string sTO = "dogapena";

        string[] aMail = { sAsunto, "Texto de prueba", sTO };
        aListCorreo.Add(aMail);

        Correo.EnviarCorreos(aListCorreo);


    }

}
