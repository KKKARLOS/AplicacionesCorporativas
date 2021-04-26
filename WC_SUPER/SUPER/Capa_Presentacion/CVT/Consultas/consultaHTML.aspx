<%@ Page Language="C#" AutoEventWireup="true" CodeFile="consultaHTML.aspx.cs" Inherits="Capa_Presentacion_CVT_Consultas_consultaHTML" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self">
   <title> ::: SUPER ::: - Visualización de Currículum Vitae</title>
   <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../Javascript/Funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/documentos.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/jquery.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/modal.js"type="text/Javascript"></script>
	<script language="javascript" type="text/javascript">
    function init() {
        if (!mostrarErrores()) return;
        ocultarProcesando();
        if ($I("ctl00$hdnRefreshPostback") == null) document.forms[0].appendChild(oRefreshPostback);
        $I("ctl00$hdnRefreshPostback").value = "S";
        window.focus();
    }

    function imprimir() {
        //top.iFrmImpresion.document.body.innerHTML = $I("divPrincipal").innerHTML;
        //top.iFrmImpresion.imprimirCV();
        window.frames.iFrmImpresion.document.body.innerHTML = $I("divPrincipal").innerHTML;
        window.frames.iFrmImpresion.imprimirCV();
    }
    function descargarDoc(nIDDoc) {
        try {

            var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/Documentos/Descargar/Default.aspx?";
            strEnlace += "nIDDOC=" + nIDDoc;

            mostrarProcesando();
            $I("iFrmSubida").src = strEnlace;
            setTimeout("ocultarProcesando();", 5000);
        } catch (e) {
            mostrarErrorAplicacion("Error al descargar el documento", e.message);
        }
    }
    function salir() {
        modalDialog.Close(window, null);
    }   
	</script>
	<style type="text/css">
	.tituloPrincipal{
	    margin-left:40px;
	    margin-top:50px;
	    width:90%;
	}
	.titulo1{
         font-weight: bold; 
         color: #336699;
         font-size: 16px; 
         /*text-decoration:underline;*/
         width: 100%;
         border-bottom: solid 2px #336699;
     }
     .titulo2{
         color: #336699;
         font-size: 13px; 
         font-weight: bold;
     }
     .titulo3{
         color: #336699;
         font-size: 12px;
     }
     .W95{
         width:95px;
     }
     .W510{
         width:510px;
     }
     
    @media print {    
        .ScrollingContent { display:block; }    
        .PrintingContent { display:block; }    
        #tblBotonera { display:none; }
    } 

     
	</style>
</head>
<body style="OVERFLOW: hidden; background-image:url('../../../Images/papel.gif'); background-repeat:repeat;" onload="init();">
<ucproc:Procesando ID="Procesando" runat="server" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <form id="form1" runat="server">
    <script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	</script>
	
    <div id="divPrincipal" class="ScrollingContent" style="overflow:auto; height:635px; width:710px; position:absolute; top:0px; left:0px; padding-bottom:15px;">
        <center>
            <div style="background-image:url('../../../Images/imgCabeceraCV.png'); height:39px; width:512px; margin-top:20px;"></div>
        </center>
        <div style="position:absolute; top:140px; right:90px; z-index:10px;">
            <% if (Session["FOTOUSUARIO"] != null)
            { %>
            <img title="" class="ICO" id="imgFoto" src="~/Capa_Presentacion/Inicio/ObtenerFoto.aspx" style="width:80px" runat="server" />
            <% }
            else
            { %>
            <img title="" id="imgFoto" align="right" class="ICO" src="../../../Images/imgTrans9x9.gif" />
            <% } %>
        </div>
        <div id="divDatosP"><%=strTablaDpHTML%></div>
        <div id="divDatosO" runat="server"><%=strTablaDoHTML%></div>
        <div id="divSinopsis" runat="server"><%=strTablaSinopsHTML%></div>
        <div id="divDocAsocia" runat="server"><%=strTablaDAPHTML%></div>
        <div id="divForma" runat="server">
            <table class='tituloPrincipal'>
                <tr><td>
                <label id='lblForma' class='titulo1'>Formación Académica</label>
                </td></tr>
            </table>               
            <div id="divFormaAcad" runat="server"><%=strTablaFormAcadHTML%></div>
        </div>    
        <div id="divExp" runat="server">
            <table class='tituloPrincipal'>
                <tr><td>
                <label id='lblExp' class='titulo1'>Experiencia profesional</label>
                </td></tr>
            </table>
            <div id="divExpIber" runat="server"><%=strTablaExpIberHTML%></div>
            <div id="divExpFuera" runat="server"><%=strTablaExpFueraHTML%></div>
        </div>         
        <div id="divAccionesForma" runat="server">
            <table class='tituloPrincipal'>
                <tr><td>
                <label id='lblAccionesForma' class='titulo1'>Acciones Formativas</label>
                </td></tr>
            </table>
            
            <div id="divCurRec" runat="server"><%=strTablaCurRecHTML%></div>
            <div id="divCurImp" runat="server"><%=strTablaCurImpHTML%></div>
        </div>
        
        <div id="DivCEREXAM" runat="server">
            <table class='tituloPrincipal'>
                <tr><td>
                <label id='lblCerti' class='titulo1'>Certificados/Exámenes</label>
                </td></tr>
            </table>          
            <div id="divCertExam" runat="server"><%=strTablaCertExamHTML%></div>
        </div>
        
        <div id="divIDIOM" runat="server">
            <table class='tituloPrincipal'>
                <tr><td>
                <label id='lblIdioma' class='titulo1'>Idiomas</label>
                </td></tr>
            </table>          
            <div id="divIdiomas" runat="server"><%=strTablaIdiomasHTML%></div>
        </div>
    </div>
    <table id="tblBotonera" style="margin-bottom:5px; width:40%;position:absolute; top: 650px; margin-left:200px;" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td align="center">
                <button id="btnImprimir" type="button" onclick="imprimir();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../Images/botones/imgImpresora.gif" /><span>Imprimir</span>
                </button>	  
            </td>
            <td align="center">
                <button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../Images/Botones/imgSalir.gif" /><span>Salir</span>
                </button>
            </td>
        </tr>
    </table>
    <input type="hidden" name="hdnFiltros" id="hdnFiltros" runat="server" value="" />
    </form>
    <div id="printingDIV" class="PrintingContent" style="height:100%; width:100%; display:none; overflow:visible"></div> 
    <input type="hidden" name="hdnErrores" id="hdnErrores" runat="server" value="" />
    <iframe id="iFrmSubida" name="iFrmSubida" frameborder="0" width="10px" height="10px" style="visibility:hidden;" ></iframe>
    <iframe id="iFrmImpresion" frameborder="0"  name="iFrmImpresion" src="PrintCV.aspx" width="10px" height="10px" style="visibility:visible;overflow:scroll;" ></iframe>
</body>
</html>
