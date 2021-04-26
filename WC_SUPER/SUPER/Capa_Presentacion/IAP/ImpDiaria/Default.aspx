<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" ClientTarget="uplevel" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_IAP_ImpDiaria_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
document.onkeydown = KeyDown;
function KeyDown(evt)  
{    
	var evt = (evt) ? evt : ((event) ? event : null);    
	var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);    
	if ((evt.keyCode == 13) && ((node.type=="text") || (node.type=="radio")))     
	{     
	    try{
	        var oNodo = getNextElement(node);
	        //oNodo.focus();
	        setTimeout(function(){oNodo.focus()},10); 
	    }catch(e){};       
		return false;        
	}  
}    

function getNextElement(field)  
{    
	var form = field.form;     
	for ( var e = 0; e < form.elements.length; e++) 
	{         
		if (field == form.elements[e]) 
		{             
			break;         
		}     
	}     
	e++;     
	while (form.elements[e % form.elements.length].type == "hidden")      
	{         
		e++;     
	}     
	return form.elements[e % form.elements.length]; 
} 
var bSalir = false;
var bLectura = false;
var controlhuecos   = <%=Session["CONTROLHUECOS"].ToString().ToLower()%>;
var num_empleado    = <%=Session["NUM_EMPLEADO_IAP"]%>;
var tipo_jornada    = <%=Session["JORNADA_REDUCIDA"].ToString().ToLower()%>;
var nHorasRed       = "<%=Session["NHORASRED"]%>";
var fec_des_red     = "<%=Session["FECDESRED"]%>";
var fec_has_red     = "<%=Session["FECHASRED"]%>";
var bControlSig     = false;
var bControlAnt     = false;
var bDetalle        = false;
var aFila           = null;

var intTotalColumna;
var intPrimerDiaSemana = eval(<%=intPrimerDiaSemana%>);
var intUltimoDiaSemana = eval(intPrimerDiaSemana + <%=intDiasEnSemana%>);

var bRes1024 = <%=((bool)Session["IAPDIARIO1024"]) ? "true":"false" %>;		
var strFecAltaAux = "<%=Session["FEC_ALTA"]%>";
	if (strFecAltaAux == ""){
		var objFecAlta = new Date(2000,0,1);  
	}else{
		var aFecAlta = strFecAltaAux.split("/");
		var objFecAlta = new Date(aFecAlta[2],eval(aFecAlta[1]-1),aFecAlta[0]);  
	}
var intFecAlta = objFecAlta.getTime();

var strAuxUltimoDia = "<%=Session["FEC_ULT_IMPUTACION"]%>";
	if (strAuxUltimoDia == ""){
		var strUltImputac = new Date(2000,0,1);  
	}else{
		var aUltimoDia = strAuxUltimoDia.split("/");
		var strUltImputac = new Date(aUltimoDia[2],eval(aUltimoDia[1]-1),aUltimoDia[0]);  
	}

var intPrimerDiaSemana	= <%=intPrimerDiaSemana%>;
var strFechaActual= "<%=strFechaActual%>";

var aHorasJornada = <%=strHorasJornada%>;
var aHorasProy = new Array();
<%=strHorasProyecto%>

var aFestivos = new Array(<%=strDiasFestivos%>);

var sDesde     = "<%=dDesde.Value.ToShortDateString()%>";
var sHasta     = "<%=dHasta.Value.ToShortDateString()%>";
var aDiasSemana     = <%=strDias%>;
var aFechas         = <%=strFechas%>;
var intUltDia		= <%=intUltimoDia%>;
var intMes			= <%=intMes%>;
var intAnno			= <%=intAnno%>;
var aSemLab	        = "<%=Session["aSemLab"] %>".split(",");

var objUltMesCerrado= AnnomesAFecha("<%=Session["UMC_IAP"]%>").add("mo", 1).add("d", -1);
var objUltDiaSemana = new Date(intAnno,eval(intMes-1),intUltDia); 
	/* No puede haber imputaciones posteriores a los 2 meses del último cierre */
	var intDiferencia = objUltDiaSemana.getTime() - objUltMesCerrado.getTime();
	if (intDiferencia > 5184000000){ //60 días en milisegundos.
		var bMesCerrado = true;
	}else var bMesCerrado = false;


function semanaSiguiente(){
	try{	
	    var strFestivos = "";
	    mostrarProcesando();
	    var strRango = "Semana del <%=intNuevoPrimerDia%> al <%=intNuevoUltimoDia%> de "+ MonthNames[<%=intNuevoMes%>] +" - <%=intNuevoAnno%>";

	    var strParametros = "ipd="+ codpar("<%=intNuevoPrimerDia%>");
	    strParametros	+= "&iud="+ codpar("<%=intNuevoUltimoDia%>");
	    strParametros	+= "&ipds="+ codpar("<%=intPrimerDiaSS%>");   // De 0 a 6.
	    strParametros	+= "&ides="+ codpar("<%=intNuevoDiasEnSemana%>");
	    strParametros	+= "&im="+ codpar("<%=intNuevoMes%>");  // De 0 a 11
	    strParametros	+= "&ia="+ codpar("<%=intNuevoAnno%>");
	    strParametros	+= "&sr="+ codpar(strRango);
    	
	    var intPos = location.href.indexOf("Default");
        var strUrl = location.href.substring(0,intPos);
	    location.href = strUrl + "Default.aspx?"+ strParametros;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a la semana siguiente", e.message);
	}
}

function semanaAnterior(){
	try{	
	    var strFestivos = "";
	    mostrarProcesando();
	    var strRango = "Semana del <%=intAnteriorPrimerDia%> al <%=intAnteriorUltimoDia%> de "+ MonthNames[<%=intAnteriorMes%>] +" - <%=intAnteriorAnno%>";

	    var strParametros = "ipd="+ codpar("<%=intAnteriorPrimerDia%>");
	    strParametros	+= "&iud="+ codpar("<%=intAnteriorUltimoDia%>");
	    strParametros	+= "&ipds="+ codpar("<%=intAnteriorPrimerDiaSS%>");   // De 0 a 6.
	    strParametros	+= "&ides="+ codpar("<%=intAnteriorDiasEnSemana%>");
	    strParametros	+= "&im="+ codpar("<%=intAnteriorMes%>");  // De 0 a 11
	    strParametros	+= "&ia="+ codpar("<%=intAnteriorAnno%>");
	    strParametros	+= "&sr="+ codpar(strRango);

	    var intPos		= location.href.indexOf("Default");
        var strUrl		= location.href.substring(0,intPos);
	    location.href	= strUrl + "Default.aspx?"+ strParametros;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a la semana anterior", e.message);
	}
}


function controlSemAntSig(){
	//control para habilitar los botones de navegación.
	var objSigSem 		= new Date(<%=intNuevoAnno%>,<%=intNuevoMes%>,<%=intNuevoPrimerDia%>);
	var objAntSem 		= new Date(<%=intAnteriorAnno%>,<%=intAnteriorMes%>,<%=intAnteriorUltimoDia%>);
	var objMesCerrado 	= AnnomesAFecha("<%=Session["UMC_IAP"]%>").add("mo", 1).add("d", -1);
	var strDiaSigLab	= "<%=dDiaSigLab%>";
	var aAux 			= strDiaSigLab.split("/");
	var objDiaSigLab 	= new Date(aAux[2],eval(aAux[1]-1),aAux[0]);
	var bHueco 			= false;

	aTotales = $I("tblResultado").getElementsByTagName("INPUT");
	for (i=aTotales.length-2; i >= 0; i--){
	    if (aSemLab[i] == 0) continue;
		strLetra = aTotales[i].id.substring(aTotales[i].id.length-1,aTotales[i].id.length);
		if (aTotales[i].value == "0,00"){
			if ((strLetra != "D") && (strLetra != "S")){
				for (j=0; j<7; j++){
					if (aLetra[j] == strLetra){
						var intIndice = j;
						break;
					}
				}
				var intDiasSemana = aDiasSemana[intIndice];
				var bFestivo = false;
				for (j=0;j<aFestivos.length;j++){
					if (aFestivos[j] == intDiasSemana){
						bFestivo = true;
						break;
					}
				}
				if (!bFestivo && aSemLab[intIndice]=="1"){//Si no es festivo y es día laboral
					bHueco = true;
					break;
				}
			}
		}
	}

    //alert("objDiaSigLab="+objDiaSigLab+"\nobjSigSem="+objSigSem+"\nstrUltImputac="+strUltImputac+"\nbHueco="+bHueco+"\nobjSigSem.getTime()="+objSigSem.getTime()+"\nobjDiaSigLab.getTime()="+objDiaSigLab.getTime());
	if ((objSigSem <= strUltImputac) || (!bHueco) || (objSigSem.getTime() == objDiaSigLab.getTime())){
		/* Antes de habilitar el botón, controlar que no se pase
		   los dos meses desde el último mes cerrado. */
//		if (!bMesCerrado) $I("tblSiguiente").filters.alpha.opacity = 100;
		if (!bMesCerrado) setOp($I("tblSiguiente"), 100);
	}
	if (objAntSem > objMesCerrado){
//		$I("tblAnterior").filters.alpha.opacity = 100;
		setOp($I("tblAnterior"), 100);
	}
}
</script>
<div id="div1024" style="z-index: 105; width: 32px; height: 32px; position: absolute; top: 93px; right: 10px;">
    <asp:Image ID="img1024" CssClass="MA" runat="server" Height="32px" Width="32px" ImageUrl="~/images/imgICO1024.gif" ondblclick="setResolucionPantalla()" ToolTip="Conmuta el tamaño de pantalla para adecuarla a la resolución 1024x768 o 1280x1024" />
</div>
<center>
<table id="tbl2" style="height:45px;width:1230px" cellpadding="4" >
    <colgroup>
        <col style="width:165px" /><col style="width:200px" /><col style="width:24px" /><col style="width:280px" /><col style="width:24px" /><col  /><col />
    </colgroup>
  <tr>
    <td style="vertical-align:bottom">
        <img id="imgNE1" src='../../../images/imgNE1on.gif' class="ne" onclick="setNE(1);"><img id="imgNE2" src='../../../images/imgNE2off.gif' class="ne" onclick="setNE(2);"><img id="imgNE3" src='../../../images/imgNE3off.gif' class="ne" onclick="setNE(3);">
    </td>
    <td style="text-align:bottom">
        <img id="btnBitacora" src="../../../images/imgTrans9x9.gif" border="0" title="Acceso a la bitácora" style="cursor:pointer" onclick="mostrarBitacora();" />
    </td>
	<td id="tblAnterior"><a onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('btnAntReg','','../../../images/btnAntRegOff.gif',1);mostrarCursor(this.parentNode)"><img name="btnAntReg" onclick="javascript:semanaAnteriorAux()" border="0" src="../../../images/btnAntRegOn.gif" style="width:24px;height:20px" title="Semana Anterior"></a></td>
    <td style="text-align:center;font-size:14px;"><%=strRango%></td>
	<td id="tblSiguiente">
	    <a onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('btnSigReg','','../../../images/btnSigRegOff.gif',1);mostrarCursor(this.parentNode)"><img name="btnSigReg" onclick="javascript:semanaSiguienteAux()" border="0" src="../../../images/btnSigRegOn.gif" style="width:24px;height:20px" title="Semana Siguiente"></a>
    </td>
    <td style="padding-left:100px;">
		<button id="btnGasvi" type="button" onclick="mostrarGasviAux();" class="btnH25W160" runat="server" hidefocus="hidefocus" 
			 onmouseover="se(this, 25);mostrarCursor(this);">
			<img src="../../../images/imgIconoAvion.gif" /><span>Crear solicitud GASVI</span>
		</button>		   
    </td>
  </tr>
</table>
<script type="text/javascript" language="Javascript">
    setOp($I("tblAnterior"), 30);
    setOp($I("tblSiguiente"), 30);
</script>
</center>
<table id="tbl1" style="width:100%; margin-bottom:10px;">
    <tr>
        <td>
            <table id="tblTitulo" style="width:1230px">
                <colgroup>
                    <col style="width:445px" />
                    <col style="width:145px" />
                    <col style="width:100px" />
                    <col style="width:39px" />
                    <col style="width:39px" />
                    <col style="width:39px" />
                    <col style="width:39px" />
                    <col style="width:39px" />
                    <col style="width:40px" />
                    <col style="width:40px" />
                    <col style="width:65px" />
                    <col style="width:65px" />
                    <col style="width:25px" />
                    <col style="width:55px" />
                    <col style="width:55px" />
                </colgroup>
                <tr class="TBLINI" style="height:17px; text-align:center;"> 
                    <td style="text-align:left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <img style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa2',event);"
										                height="11" src="../../../Images/imgLupa.gif" width="20"> 
		                <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa2');"
										                height="11" src="../../../Images/imgLupaMas.gif" width="20">&nbsp;Denominación
	                </td>
                    <td style="text-align:center">OTC</td>
                    <td style="text-align:center">OTL</td>
                    <td style="text-align:right;">L. <%=aDiasSemana[0]%></td>
                    <td style="text-align:right;">M. <%=aDiasSemana[1]%></td>
                    <td style="text-align:right;">X. <%=aDiasSemana[2]%></td>
                    <td style="text-align:right;">J. <%=aDiasSemana[3]%></td>
                    <td style="text-align:right;">V. <%=aDiasSemana[4]%></td>
                    <td style="text-align:right;">S. <%=aDiasSemana[5]%></td>
                    <td style="text-align:right;">D. <%=aDiasSemana[6]%></td>
                    <td style="text-align:center" title="Esfuerto total estimado">E.T.E.</td>
                    <td style="text-align:center" title="Fecha de fin estimada">F.F.E.</td>
                    <td style="text-align:right;" title="Tarea finalizada">Fin.</td>
                    <td style="text-align:center"title="Esfuerzo acumulado total">E.A.T.</td>
                    <td style="text-align:center" title="Esfuerzo pendiente según estimación">E.P.</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
          <div id="divCatalogo" style='width:1246px; height:682px; overflow:auto; overflow-x:hidden;'>
             <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif);width:1230px;">    
                <%=strTablaHTML%>
            </div>
          </div>
        </td>
    </tr>  
    <tr>
        <td>
            <table id="tblResultado" style="width:1230px; text-align:right;">
                <colgroup>
                    
                    <col style="width:449px;" />
                    <col style="width:148px;" />
                    <col style="width:100px;" />
                    <col style="width:39px" />
                    <col style="width:40px" />
                    <col style="width:40px" />
                    <col style="width:40px" />
                    <col style="width:39px" />
                    <col style="width:39px" />
                    <col style="width:39px" />
                    <col style="width:65px" />
                    <col style="width:65px" />
                    <col style="width:25px" />
                    <col style="width:55px" />
                    <col style="width:48px" />
                </colgroup>
	            <tr class="TBLFIN" style="height:17px;">
		            <td>&nbsp;</td>
		            <td>&nbsp;</td>
		            <td>&nbsp;</td>
		            <td>
		                <input type="text" class="txtNumL" id="txtTotalL" style="width:32px; padding-right:3px;" 
		                    value="<% if ((intPrimerDiaSemana < 1) && (intTotal >= 1)){ Response.Write(aTotalDias[0].ToString("N")); } %>" readonly="readonly"/>
		            </td>
		            <td><input type="text" class="txtNumL" id="txtTotalM" style="width:32px; padding-right:4px;" value="<% if ((intPrimerDiaSemana < 2) && (intTotal >= 2)){ Response.Write(aTotalDias[1].ToString("N")); } %>" readonly="readonly"/></td>
		            <td><input type="text" class="txtNumL" id="txtTotalX" style="width:32px; padding-right:4px;" value="<% if ((intPrimerDiaSemana < 3) && (intTotal >= 3)){ Response.Write(aTotalDias[2].ToString("N")); } %>" readonly="readonly"/></td>
		            <td><input type="text" class="txtNumL" id="txtTotalJ" style="width:32px; padding-right:4px;" value="<% if ((intPrimerDiaSemana < 4) && (intTotal >= 4)){ Response.Write(aTotalDias[3].ToString("N")); } %>" readonly="readonly"/></td>
		            <td><input type="text" class="txtNumL" id="txtTotalV" style="width:32px; padding-right:5px;" value="<% if ((intPrimerDiaSemana < 5) && (intTotal >= 5)){ Response.Write(aTotalDias[4].ToString("N")); } %>" readonly="readonly"/></td>
		            <td><input type="text" class="txtNumL" id="txtTotalS" style="width:32px; padding-right:5px;" value="<% if ((intPrimerDiaSemana < 6) && (intTotal >= 6)){ Response.Write(aTotalDias[5].ToString("N")); } %>" readonly="readonly"/></td>
		            <td><input type="text" class="txtNumL" id="txtTotalD" style="width:32px; padding-right:5px;" value="<% if ((intPrimerDiaSemana < 7) && (intTotal >= 7)){ Response.Write(aTotalDias[6].ToString("N")); } %>" readonly="readonly"/></td>
		            <td>&nbsp;</td>
		            <td>&nbsp;</td>
		            <td>&nbsp;</td>
		            <td>&nbsp;</td>
		            <td>&nbsp;</td>
	            </tr>
            </table>
        </td>
    </tr>  
    <tr>
    <td><div style='margin-top:3px'>
        <img class="ICO" src="../../../Images/imgIconoProyAbierto.gif" style="margin-left:5px;" title='Proyecto abierto' />Abierto
        &nbsp;&nbsp;<img class="ICO" src="../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />Cerrado
        <input type="text" class="VacacionesL" name="text1" style=" border:#315d6b 1px solid; margin-left:10px;" readonly="readonly" />&nbsp;Vacaciones
        <input type="text" class="FesImpL" name="text2" style=" border:#315d6b 1px solid;" readonly="readonly" />&nbsp;Festivos
        &nbsp;<input type="text" class="OutProyL" name="text3" readonly="readonly" />&nbsp;Fuera de proyecto
        &nbsp;<input type="text" class="OutVigL" name="text4" readonly="readonly" />&nbsp;Fuera de vigencia
        &nbsp;<input type="text" class="LabImpComL" name="text5" readonly="readonly" title="Comentario a la imputación, indicaciones del responsable o estimaciones del profesional" />&nbsp;Comentarios
        <label class="blue" style=" margin-left:15px;">Estimación obligatoria</label>
        &nbsp;&nbsp;<label class="cerrada">Cerrada</label>
        &nbsp;&nbsp;<label class="finalizada">Finalizada</label>
        &nbsp;&nbsp;<label class="paralizada">Paralizada</label>
        &nbsp;&nbsp;<label class="pendiente">Pendiente</label> 
        </div>   
    </td>
    </tr>
</table>    


<input type="text" style="visibility:hidden" id="hdnInputActivo" name="hdnInputActivo" value="" />
<input type="hidden" name="Gasvi" id="hdnAccGasvi" value="N" runat="server" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {

        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            switch (strBoton) {
                case "comentariobot":
                    {
                        bEnviar = false;
                        setTimeout("mostrarComentario()", 100);
                        break;
                    }
                case "tareabot":
                    {
                        bEnviar = false;
                        setTimeout("mostrarTarea()", 100);
                        break;
                    }
                case "jornada":
                    {
                        bEnviar = false;
                        setTimeout("imputarJornada()", 100);
                        break;
                    }
                case "semana":
                    {
                        bEnviar = false;
                        setTimeout("imputarSemana()", 100);
                        break;
                    }
                case "grabar":
                    {
                        bEnviar = false;
                        //if ($I("hdnInputActivo").value != "") $I($I("hdnInputActivo").value).blur();
                        setTimeout("grabarAux('grabar')", 100);
                        break;
                    }
                case "grabarreg":
                    {
                        bEnviar = false;
                        if ($I("hdnInputActivo").value != "") $I($I("hdnInputActivo").value).blur();
                        setTimeout("grabarAux('grabarreg')", 100);
                        break;
                    }
                case "grabarss":
                    {
                        bEnviar = false;
                        if ($I("hdnInputActivo").value != "") $I($I("hdnInputActivo").value).blur();
                        setTimeout("grabarSiguienteAux()", 100);
                        break;
                    }
                case "regresar":
                    {
                        if (bCambios) {
                            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war",330).then(function (answer) {
                                if (answer) {                                    
                                    if ($I("hdnInputActivo").value != "") $I($I("hdnInputActivo").value).blur();
                                    bEnviar = false;
                                    bSalir=true;
                                    setTimeout("grabarAux('grabarreg');", 100);
                                }
                                else {
                                    bEnviar = true;
                                    bCambios = false;                               
                                    fSubmit(bEnviar,eventTarget,eventArgument);
                                }
                            });
                        } else fSubmit(true,eventTarget,eventArgument);
                        break;
                    }
                    //				case "bitacora": 
                    //				{
                    //					bEnviar = false;
                    //    				mostrarBitacoraPE();
                    //   					break;
                    //				}
                    //				case "bitacorapt": 
                    //				{
                    //					bEnviar = false;
                    //    				mostrarBitacoraPT();
                    //   					break;
                    //				}
                    //				case "bitacorat": 
                    //				{
                    //					bEnviar = false;
                    //    				mostrarBitacoraT();
                    //   					break;
                    //				}
                case "guia":
                    {
                        bEnviar = false;
                        setTimeout("mostrarGuia('ImputacionCalendario.pdf');", 100);
                        break;
                    }
            }
            if (strBoton!="regresar") fSubmit(bEnviar,eventTarget,eventArgument)
        }             
    }
    function fSubmit(bEnviar,eventTarget,eventArgument)
    {
        var theform = document.forms[0];
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) theform.submit();
    }
    function WebForm_CallbackComplete() {
        for (var i = 0; i < __pendingCallbacks.length; i++) {
            callbackObject = __pendingCallbacks[i];
            if (callbackObject && callbackObject.xmlRequest && (callbackObject.xmlRequest.readyState == 4)) {
                WebForm_ExecuteCallback(callbackObject);
                if (!__pendingCallbacks[i].async) {
                    __synchronousCallBackIndex = -1;
                }
                __pendingCallbacks[i] = null;
                var callbackFrameID = "__CALLBACKFRAME" + i;
                var xmlRequestFrame = document.getElementById(callbackFrameID);
                if (xmlRequestFrame) {
                    xmlRequestFrame.parentNode.removeChild(xmlRequestFrame);
                }
            }
        }
    }
</script>
</asp:Content>

