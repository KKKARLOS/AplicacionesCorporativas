<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript" language="javascript">
var bNuevoGasvi = <% =((bool)Session["GVT_NUEVOGASVI"])? "true":"false" %>;
var bAdministrador = <% =(User.IsInRole("A"))? "true":"false" %>;
var nNotasPendientes = <% =nNotasPendientes %>;
var bBono = <% =((bool)bBono)? "true":"false" %>;
var bPago = <% =((bool)bPago)? "true":"false" %>;
var bMiAmbito = <% =(User.IsInRole("I") || nNotasVisadas > 0)? "true":"false" %>;
var bIsInRoleT = <% =(User.IsInRole("T"))? "true":"false" %>;
var bIsInRoleS = <% =(User.IsInRole("S"))? "true":"false" %>;

/* Inicio código ICONOS PEQUEÑOS */
//if (!bIsInRoleT || !bIsInRoleS || !bAdministrador){
//    jQuery(document).ready(function($){        
//        $('div#divNE .cornerLink').on("hover", function(){
//            alert("Nueva solicitud Estándar");
//        })

//    })
//    jQuery(document).ready(function($){
//        $('div#divNMP').off('hover');
//    })
//    jQuery(document).ready(function($){
//        $('div#divBT').off('hover');

//        $('div#divBT').on("hover", function(){
//            alert("bono transporte");
//        })

//    })
//    jQuery(document).ready(function($){
//        $('div#divPC').off('hover');
//    })
//}else{
//    if (!bNuevoGasvi){
//        jQuery(document).ready(function($){
//            $('div#divNE').off('hover');
//        })
//        jQuery(document).ready(function($){
//            $('div#divNMP').off('hover');
//        })
//        if (bBono){
//            jQuery(document).ready(function($){                
//                $('div#divBT').on("hover", function(){
//                    alert("bono transporte");
//                })
//            })
//        }
//        if (bPago){
//            jQuery(document).ready(function($){
//                $('div#divPC').off('hover');
//            })
//        }
//    }
//}

//jQuery(document).ready(function($){
//    $('div#divMC').imgbubbles({factor:1.3})
//})

//if (bMiAmbito){
//    jQuery(document).ready(function($){
//	    $('div#divCO').imgbubbles({factor:1.3})
//    })
//}

//if (bAdministrador){
//    jQuery(document).ready(function($){
//	    $('div#divMarcoADM').imgbubbles({factor:1.3})
//    })
//}

//if (nNotasPendientes > 0){
//    jQuery(document).ready(function($){
//	    $('div#divMarcoAcciones').imgbubbles({factor:1.3})
//    })
//}

/* Fin código ICONOS PEQUEÑOS */
</script>
<%--<div id="divMarcoNueva">
    <div id="divNE" class="bubblewrap"><li><img id="imgNE" src="../../Images/imgNE.gif" alt="Estándar" title="Estándar"/></li></div> <!--  onclick="nuevo('NE');" -->
    <div id="divNMP" class="bubblewrap"><li><img id="imgNMP" src="../../Images/imgNMP.gif" alt="Multiproyecto" title="Multiproyecto" /></li></div> <!--  onclick="nuevo('NMP');" -->
    <div id="divBT" class="bubblewrap"><li><img id="imgBT" src="../../Images/imgBT.gif" alt="Bono de transporte" title="Bono de transporte" /></li></div> <!--  onclick="nuevo('BT');" -->
    <div id="divPC" class="bubblewrap"><li><img id="imgPC" src="../../Images/imgPC.gif" alt="Pago concertado" title="Pago concertado" /></li></div> <!--  onclick="nuevo('PC');" -->
</div>
<div id="divMarcoConsulta">
    <div id="divMC" class="bubblewrap"><li><img id="imgMC" src="../../Images/imgMN.gif" alt="Mis solicitudes" title="Mis solicitudes" onclick="goMisSolicitudes();" /></li></div>
    <div id="divCO" class="bubblewrap"><li><img id="imgCO" src="../../Images/imgNMA.gif" alt="Solicitudes de mi ámbito" title="Solicitudes de mi ámbito"/></li></div>
</div>
<div id="divMarcoAcciones" class="bubblewrap">
    <li>
        <img id="imgAP" src="../../Images/imgAP.gif" title="Visados" alt="Visados"/>
    </li>    
    <div id="divNumAP"></div>
</div>
<div id="divMarcoADM" class="bubblewrap">
    <li><img id="imgADM" src="../../Images/imgADM.gif" title="Administración" alt="Administración"/></li>
</div>--%>



    <div id="divMarcoNueva">
        <h2>Nueva solicitud</h2>
        <div id="divNE">
            <img id="imgNE" alt="Solicitud Estándar" src="../../Images/imgNuevaSolicitudStandad.png"  />
            <a href="#"  class="cornerLink">Estándar</a>
        </div>

        <div id="divNMP">
            <img id="imgNMP" alt="Solicitud Multiproyecto" src="../../Images/imgNuevaSolicitudMultiple.png" />
            <a href="#" class="cornerLink">Multiproyecto</a>
        </div>

        <div id="divBT">
            <img id="imgBT" alt="Solicitud Bono transporte" src="../../Images/imgBonoTransporte.png"  />
            <a href="#" class="cornerLink">Bono transporte</a>
        </div>

        <div id="divPC">
            <img id="imgPC" alt="Solicitud Pago concertado" src="../../Images/imgPagoConcertado.png" />
            <a href="#" class="cornerLink">Pago concertado</a>
        </div>
    </div>


    <div id="divMarcoConsulta">
        <h2>Consultas</h2>
        <div id="divMC">          
            <img id="imgMC" alt="Mis solicitudes" src="../../Images/imgConsultasSolicitud.png" onmouseover="this.src = '../../images/imgConsultasSolicitudColor.png'" onmouseout="this.src = '../../images/imgConsultasSolicitud.png'" onclick="goMisSolicitudes();" />
            <a href="#" class="cornerLink">Mis solicitudes</a>
        </div>
        <div id="divCO">
            <img id="imgCO" alt="Solicitudes de mi ámbito" src="../../Images/imgConsultasSolicitudAmbito.png" />
            <a href="#" class="cornerLink">Solicitudes de mi ámbito</a>
        </div>
    </div>

    <div id="divMarcoAcciones">  
        <h2>Acciones pendientes</h2>      
        <img id="imgAP" src="../../Images/imgAccionesPendientes.png" alt="Acciones pendientes"  />
        <%--<a href="#" class="cornerLink">Acciones pendientes</a>--%>
        <div id="divNumAP"></div>

    </div>
    <div id="divMarcoADM">    
        <h2>Administración</h2>    
        <img id="imgADM" src="../../Images/imgAdministracion.png"  alt="Administración" />
      <%--  <a href="#" class="cornerLink">Administración</a>--%>
    </div>
    


    <!--Tabla -->
    <div id="divAbiertas">
    <div style="text-align:center;background-image: url('../../Images/imgFondo200.gif'); background-repeat: no-repeat;
        width:200px; height:23px; position:relative; top:12px; left:20px; padding-top:5px; text-align:center;
        font:bold 12px Arial; color:#5894ae;" title="Solicitudes aparcadas, en trámite o pagadas en los últimos treinta días.">Mis últimas tramitaciones</div>
    <table style="width:990px; height:280px;" cellpadding="0">
        <tr>
            <td style="background-image:url(../../Images/Tabla/7.gif); height:6px; width:6px;"></td>
            <td style="background-image:url(../../Images/Tabla/8.gif);"></td>
            <td style="background-image:url(../../Images/Tabla/9.gif); height:6px; width:6px; "></td>
        </tr>
        <tr>
            <td style="background-image:url(../../Images/Tabla/4.gif); ">&nbsp;</td>
            <td style="background-image:url(../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                <!-- Inicio del contenido propio de la página -->
		        <table id="tblTituloAbiertas" style="width:950px;height:17px; margin-top:10px;">
			        <colgroup>					
				        <col style="width:20px; text-align:center;" />
				        <col style="width:60px; text-align:right; padding-right:10px;" />
                        <col style="width:70px;" />
                        <col style="width:70px;" />
				        <col style="width:150px;" />
				        <col style="width:180px;" />
				        <col style="width:140px;" />
				        <col style="width:140px;" />
				        <col style="width:50px;" />
				        <col style="width:70px; " />
			        </colgroup>
			        <tr class="TBLINI">				    
				        <td></td>
				        <td title="Referencia">Ref.</td>
				        <td>Estado</td>
				        <td title="Fecha de tramitación">F. Tram.</td>
				        <td>Beneficiario</td>
				        <td>Concepto</td>
				        <td>Motivo</td>
				        <td>Proyecto</td>
				        <td>Moneda</td>
				        <td style="text-align:right; padding-right:2px;">Importe</td>
			        </tr>
		        </table>
		        <div id="divCatalogo" style="overflow-x:hidden; overflow:auto; width:966px; height:200px" runat="server">
			        <div style="background-image:url(<%=Session["GVT_strServer"] %>Images/imgFT20.gif); width:950px;">
			            <%=strTablaHTML%>
			        </div>
		        </div>
		       
                <!-- Fin del contenido propio de la página -->
            </td>
            <td style="background-image:url(../../Images/Tabla/6.gif); width:6px">&nbsp;</td>
        </tr>
        <tr>
            <td style="background-image:url(../../Images/Tabla/1.gif); height:6px; width:6px"></td>
            <td style="background-image:url(../../Images/Tabla/2.gif); height:6px;"></td>
            <td style="background-image:url(../../Images/Tabla/3.gif); height:6px; width:6px"></td>
        </tr>
    </table>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">

</asp:Content>

