using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
//PARA EL ACCESO A DATOS
using System.Data.SqlClient;
//Para el MemoryStream
using System.IO;
//Para el Encoding
using System.Text;
//PARA EL HttpContext
using System.Web;

namespace SUPER.BLL
{
    /// <summary>
    /// Descripción breve de IberDok
    /// </summary>
    public class IberDok
    {
        //public string idusuario { get; set; }
        //public string uidPedido { get; set; }
        //public string modelo { get; set; }
        //public string tipo { get; set; }
        //public string clase { get; set; }

        public IberDok()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        //public IberDok(string idusuario, string uiPedido, string modelo, string tipo, string clase)
        //{
        //    this.idusuario = idusuario;
        //    this.uidPedido = uidPedido;
        //    this.modelo=modelo;
        //    this.tipo=tipo;
        //    this.clase=clase;
        //}
        /// <summary>
        /// Una vez que hemos insertado los datos de una exportación de CV´s en la BBDD de IBERDOK, llámamos a este método
        /// para avisarle a IBERDOK que tiene que empezar a generar los documentos correspondientes
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="uiPedido"></param>
        /// <param name="modelo"></param>
        /// <param name="tipo"></param>
        /// <param name="clase"></param>
        public static void CrearPedido(string usuario, string uiPedido, string modelo, string tipo, string clase)
        {
            try
            {
                var client = new RestClient();
                client.EndPoint = @"http://iberdok/cvDis/services/ibermatica/generacionDocumentosDis.htm";
                client.Method = HttpVerb.POST;
                //client.PostData = "{idUsuario:admin,uidPedido:7149C6BD-B49D-418B-BD3D-73E88CFCA459,modelo:3473,tipo:PDF, clase:I}";}

                //string sParam = @"{""idUsuario"":""admin"",""uidPedido"":""7149C6BD-B49D-418B-BD3D-73E88CFCA459"",""modelo"":""3473"",""tipo"":""PDF"",""clase"":""I""}";
                string sParam = @"{""idUsuario"":""" + usuario + @"""";
                sParam += @",""uidPedido"":""" + uiPedido + @"""";
                sParam += @",""modelo"":""" + modelo + @"""";
                sParam += @",""tipo"":""" + tipo + @"""";
                sParam += @",""clase"":""" + clase + @"""}";

                //SUPER.DAL.Log.Insertar("SUPER.BLL.Negocio.IberDok.CrearPedido. sParam: " + sParam);

                //Prueba serializando con JSON
                //IberDok oPet = new IberDok(usuario, uiPedido, modelo, tipo, clase);
                //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IberDok));
                //MemoryStream mem = new MemoryStream();
                //ser.WriteObject(mem, oPet);
                //string sParam = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);

                client.PostData = sParam;

                var json = client.MakeRequest();
                //client.MakeRequest();
            }
            catch(Exception e)
            {
                SUPER.DAL.Log.Insertar("SUPER.BLL.Negocio.IberDok.CrearPedido. Error: " + e.Message);
            }
        }
        /// <summary>
        /// Obtiene la tabla de transformación de modelos entre SUPER e IBERDOK
        /// </summary>
        /// <returns></returns>
        public static List<ElementoLista> getModelosIberdok()
        {
            if (HttpContext.Current.Cache["Modelos_Iberdok"] == null)
            {
                List<ElementoLista> oLista = new List<ElementoLista>();
                SqlDataReader dr = SUPER.DAL.IBERDOK.Catalogo();
                while (dr.Read())
                {
                    oLista.Add(new ElementoLista(dr["t056_idSuper"].ToString(), dr["t056_idIBERDOK"].ToString()));
                }
                dr.Close();
                dr.Dispose();

                HttpContext.Current.Cache.Insert("Modelos_Iberdok", oLista, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);

                return oLista;
            }
            else
            {
                return (List<ElementoLista>)HttpContext.Current.Cache["Modelos_Iberdok"];
            }
        }
        /// <summary>
        /// Dado un código de modelo de plantilla en SUPER, devuelve el código de modelo en IBERDOK
        /// </summary>
        /// <param name="sIdModeloSUPER"></param>
        /// <returns></returns>
        public static string GetModelo(string sIdModeloSUPER)
        {
            string sIModeloIBERDOK = "3796";//Por defecto pongo la plantilla EuroPass

            List<ElementoLista> aLista = getModelosIberdok();
            foreach (ElementoLista oElem in aLista)
            {
                if (oElem.sValor == sIdModeloSUPER)
                {
                    sIModeloIBERDOK = oElem.sDenominacion;
                    break;
                }
            }
            return sIModeloIBERDOK;
        }

    }
}