using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.SIC.Models
{
    public class TareaCatRequestFilter
    {

        public TareaCatRequestFilter()
        {
        }
        public Int32 ta207_idtareapreventa { get; set; }
        public Int32 ta204_idaccionpreventa { get; set; }
        public DateTime ta207_fechafinestipulada { get; set; }
        public DateTime ta207_fechacreacion { get; set; }
        public DateTime ta207_fechafinreal { get; set; }
        public String ta207_estado { get; set; }
        public String ta207_denominacion { get; set; }
        public String ta204_observaciones { get; set; }
        public Int32 ta201_idsubareapreventa { get; set; }
        public Int32 t001_idficepi_lider { get; set; }
        public Int32 t001_idficepi_promotor { get; set; }
        public Int32 ta205_idtipoaccionpreventa { get; set; }
        public Int32 ta206_idsolicitudpreventa { get; set; }

        public String tipoAccion { get; set; }
        public String unidadPreventa { get; set; }
        public String areaPreventa { get; set; }
        public String subareaPreventa { get; set; }
        public String lider { get; set; }
        public String promotor { get; set; }
        public String comercial { get; set; }
        public Int32 ta206_iditemorigen { get; set; }
        public String ta206_itemorigen { get; set; }
        public String den_item { get; set; }
        public String ta205_denominacion { get; set; }
        public String ta201_denominacion { get; set; }
        public String ta200_denominacion { get; set; }
        public String ta199_denominacion { get; set; }
        public String ta206_denominacion { get; set; }
        public String den_cuenta { get; set; }
        public String den_unidadcomercial { get; set; }
        
        public Boolean ta208_negrita { get; set; }        
    }


    public class TareaCatHistoricoFilter
    {

        public string estado { get; set; }
        public string estadoParticipacion { get; set; }
        public string itemorigen { get; set; }
        public string iditemorigen { get; set; }       
        public string ffinDesde { get; set; }
        public string ffinHasta { get; set; }
        public string promotor { get; set; }
        public string comercial { get; set; }
        public string[] lideres { get; set; }
        public string[] clientes { get; set; }
        public string[] acciones { get; set; }
        public string[] unidades { get; set; }
        public string[] areas { get; set; }
        public string[] subareas { get; set; }

        public TareaCatHistoricoFilter()
        {
            estado = null;
            itemorigen = null;
            iditemorigen = null;           
            ffinDesde = null;
            ffinHasta = null;
            promotor = null;
            comercial = null;
            lideres = null;
            clientes = null;
            acciones = null;
            unidades = null;
            areas = null;
            subareas = null;
        }

    }

   
}