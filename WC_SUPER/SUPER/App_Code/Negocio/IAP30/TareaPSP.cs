using System;
using System.Collections.Generic;
using System.Collections;
using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;
using SUPERANTIGUO = SUPER;

/// <summary>
/// Summary description for TareaPSP
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class TareaPSP : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("828bfa92-afaf-49c0-95d4-bac9314c1151");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public TareaPSP()
			: base()
        {
			//OpenDbConn();
        }
		
		public TareaPSP(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        

        public Models.TareaPSP Select(int idTarea)
        {
            OpenDbConn();

            DAL.TareaPSP cTareaPSP = new DAL.TareaPSP(cDblib);
            return cTareaPSP.Select(idTarea);
        }

        public string ControlLimiteEsfuerzos(int nTarea, double nHoras, DateTime dDia, Hashtable htTareasSuperanCLE)
        {
            OpenDbConn();

            BLL.TareaIAPS TareaIAPSBLL = new BLL.TareaIAPS(cDblib);

            System.Text.StringBuilder sbEx = new System.Text.StringBuilder();
            Models.TareaCLE oTareaCLE = new Models.TareaCLE();
            Models.TareaPSP oTarea = new Models.TareaPSP();
            Models.TareaIAPS oTareaIAPS = new Models.TareaIAPS();            

            double fHorasAcumuladas;

            ArrayList aListCorreo = new ArrayList();             

            try
            {                
                
                oTarea = Select(nTarea);
                oTareaIAPS = TareaIAPSBLL.Select(nTarea);

                fHorasAcumuladas = (float)oTareaIAPS.nConsumidoHoras + nHoras;

                if (oTarea.t332_cle > 0 && fHorasAcumuladas > oTarea.t332_cle)
                {
                    if (oTarea.t332_tipocle == "I")
                    {
                        //Inserto registro para que el proceso nocturno avise de la situación a cada RTPT de la tarea
                        //De momento lo hago por trigger
                        //SqlDataReader dr2 = RTPT.Catalogo(oTarea.t331_idpt, null, 2, 0);
                        //while (dr2.Read())
                        //{
                        //    idRTPT = int.Parse(dr2["t314_idusuario"].ToString());
                        //    Consumo.InsertarCorreo(tr, 12, true, false, idRTPT, nTarea, null, "", oTarea.num_proyecto);
                        //}
                        //dr2.Close();
                        //dr2.Dispose();
                    }
                    else if (oTarea.t332_tipocle == "B")
                    {
                        ///Indicación de que con la imputación realizada se va a
                        ///sobrepasar el límite de esfuerzos y cortar la transacción.
                        sbEx.Append("<br />Se ha sobrepasado el límite de horas máximo permitido ");
                        sbEx.Append("para la tarea '" + oTarea.t332_idtarea.ToString() + " " + oTarea.t332_destarea);
                        sbEx.Append("'.<br />En la fecha de imputación (" + dDia.ToShortDateString() + "), el exceso es ya de " + double.Parse((fHorasAcumuladas - oTarea.t332_cle).ToString()).ToString("N") + " horas. ");
                        sbEx.Append("<br />Para poder imputar más horas a dicha tarea, ponte en contacto con el responsable de la misma.");
                    }
                    else if (oTarea.t332_tipocle == "X")
                    {


                        oTareaCLE = new Models.TareaCLE();
                        oTareaCLE.idtarea = nTarea;
                        oTareaCLE.fecha = dDia;
                        oTareaCLE.limite = oTarea.t332_cle;
                        oTareaCLE.consumo = fHorasAcumuladas;

                        ArrayList lstDestinatarios = ObtenerDestinatario(nTarea);

                        ArrayList lstMails = new ArrayList();

                        foreach (Models.TareaCTIAP tareaCTIAP in lstDestinatarios)
                        {

                            lstMails.Add(GenerarCorreoSuperaETPR(tareaCTIAP.MAIL,
                                                     tareaCTIAP.t301_idproyecto.ToString("#,###") + " " + tareaCTIAP.t301_denominacion,
                                                     tareaCTIAP.t331_despt, tareaCTIAP.t334_desfase, tareaCTIAP.t335_desactividad,
                                                     tareaCTIAP.t332_idtarea.ToString("#,###") + " " + tareaCTIAP.t332_destarea,
                                                     dDia.ToString(), oTarea.t332_cle.ToString("N"), fHorasAcumuladas.ToString("N")));
                           
                        }

                        oTareaCLE.destinatariosMail = lstMails;

                        if (htTareasSuperanCLE.ContainsKey(nTarea)) htTareasSuperanCLE[nTarea] = oTareaCLE;
                        else htTareasSuperanCLE.Add(nTarea, oTareaCLE);
                    }
                }                


            return sbEx.ToString();

            }
            catch (Exception ex)
            {                
                throw ex;
            }
            finally
            {
                TareaIAPSBLL.Dispose();

            }
            
        }
        public static ArrayList ObtenerDestinatario(int t332_idtarea)
        {
            BLL.TareaCTIAP bTarea = new BLL.TareaCTIAP();
            try
            {
                ArrayList lstDestinatarios = new ArrayList();

                lstDestinatarios = bTarea.getRTPT(t332_idtarea);

                return lstDestinatarios;
                
            }
            catch (Exception ex)
            {
                IB.SUPER.Shared.LogError.LogearError("Error en control supera ETPR", ex);
                throw ex;
            }
            finally
            {
                bTarea.Dispose();
            }
        }        

        
        private static string[] GenerarCorreoSuperaETPR(string sTO, string sProy, string sProyTec, string sFase, string sActiv, 
                                                        string sTarea, string sFecha, string sLimite, string sConsumo)
        {
            string sAsunto = "", sTexto = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                sAsunto = "Superación de límite de esfuerzo en tarea.";

                sb.Append("<BR>SUPER te informa de que se ha producido una imputación de consumo a tarea en IAP que ha superado el límite de esfuerzo establecido.");
                sb.Append("<BR><BR><label style='width:120px'>Proyecto económico: </label>" + sProy + "<br>");
                sb.Append("<label style='width:120px'>Proyecto Técnico: </label>" + sProyTec + "<br>");
                if (sFase != "") sb.Append("<label style='width:120px'>Fase: </label>" + sFase + "<br>");
                if (sActiv != "") sb.Append("<label style='width:120px'>Actividad: </label>" + sActiv + "<br>");
                sb.Append("<label style='width:120px'>Tarea: </label><b>" + sTarea + "</b><br>");
                sb.Append("<label style='width:120px'>Fecha: </label>" + sFecha.Substring(0, 10) + "<br>");
                sb.Append("<label style='width:120px'>Límite: </label>" + sLimite + " horas<br><br>");
                sb.Append("<label style='width:120px'>Consumo acumulado: </label>" + sConsumo + " horas<br><br>");
                sTexto = sb.ToString();

                string[] aMail = { sAsunto, sTexto, sTO };
                return aMail;

            }
            catch (Exception ex)
            {
                //sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo de imputación IAP a tarea con traspaso IAP ya realizado.", ex);
                IB.SUPER.Shared.LogError.LogearError("Error al enviar correo de imputación de consumo a tarea en IAP cuyo consumo supera el límite de esfuerzo establecido.\n" + sTexto, ex);
                throw ex;
            }
        }

        #endregion          
		
		#region Conexion base de datos y dispose
        private void OpenDbConn()
        {
            if (cDblib == null)
                cDblib = new IB.sqldblib.SqlServerSP(Shared.Database.GetConStr(), classOwnerID);
        }
        private void AttachDbConn(sqldblib.SqlServerSP extcDblib)
        {
            cDblib = extcDblib;
        }
        private void Dispose(bool disposing)
        {
            if (!this.disposed && disposing) if (cDblib != null && cDblib.OwnerID.Equals(classOwnerID)) cDblib.Dispose();
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~TareaPSP()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
