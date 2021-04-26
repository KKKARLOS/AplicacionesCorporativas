using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_Pruebas_TestsvcCVT_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        svcSUPER.IsvcSUPERClient osvcSUPER = new svcSUPER.IsvcSUPERClient();
        byte[] oDoc = null;

        Response.ClearContent();
        Response.ClearHeaders();
        Response.Buffer = true;
        Response.ContentType = "application/octet-stream";
        //oDoc = osvcSUPER.obtenerCurriculum("Hola mundo !");

        if (osvcSUPER != null && osvcSUPER.State != System.ServiceModel.CommunicationState.Closed)
        {
            if (osvcSUPER.State != System.ServiceModel.CommunicationState.Faulted) osvcSUPER.Close();
            else if (osvcSUPER.State != System.ServiceModel.CommunicationState.Closed) osvcSUPER.Abort();
        }

        Response.BinaryWrite(oDoc);

        if (Response.IsClientConnected)
            Response.Flush();

        Response.Close();
    }
}
