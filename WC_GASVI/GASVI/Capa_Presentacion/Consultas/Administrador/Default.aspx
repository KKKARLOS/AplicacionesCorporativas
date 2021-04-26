<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Consultas_Administrador_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script language="javascript">
    var sOrigen = "<%=Request.QueryString["so"] %>";
</script>
<div style="margin-top:-15px; margin-left:40px;">
    <div style="text-align:center;background-image: url('../../../Images/imgFondo185.gif'); background-repeat:no-repeat;
        width:185px; height:23px; position:relative; top:12px; left:20px; padding-top:5px; text-align:center;
        font:bold 12px Arial; color:#5894ae;">Criterios de selección</div>
    <table style="width:900px; height:205px;"  cellpadding="0">
        <tr>
            <td style="background-image:url(../../../Images/Tabla/7.gif); height:6px; width:6px"></td>
            <td style="background-image:url(../../../Images/Tabla/8.gif); height:6px;"></td>
            <td style="background-image:url(../../../Images/Tabla/9.gif); height:6px; width:6px"></td>
        </tr>
        <tr>
            <td style="background-image:url(../../../Images/Tabla/4.gif); width:6px">&nbsp;</td>
            <td style="background-image:url(../../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                <!-- Inicio del contenido propio de la página -->
                <table style="width:890px; margin-top:10px;">
                	<colgroup>
				        <col style="width:750px;" />
                        <col style="width:140px;" />
			        </colgroup>
                    <tr>
                        <td>
                        <fieldset style="width:730px;">
				            <legend><label id="Label1" class="enlace" onclick="getPeriodo()">Periodo de tramitación</label>
				            <asp:TextBox ID="txtDesde" style="margin-left:15px;width:90px; vertical-align:middle; text-align:center;" Text="" readonly runat=server />
                                        <asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" ReadOnly=true runat=server />
                            <asp:TextBox ID="txtHasta" style="margin-left:5px; width:90px; vertical-align:middle; text-align:center;" Text="" readonly runat=server />
                                        <asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" ReadOnly=true runat=server />
                            </legend>
		                    <table style="width:720px; margin-top:10px; margin-bottom:10px;">
			                    <colgroup>
				                    <col style="width:300px;" />
				                    <col style="width:420px" />
			                    </colgroup>
			                    <tr style='vertical-align: text-top;'>				    
				                    <td>
				                        <fieldset style="width:250px; margin-left:10px;">
				                        <legend>Estados <img src="../../../images/botones/imgmarcar.gif" title="Marca todos los estados" style="cursor:pointer; margin-left:5px;" onclick="setEstados(1);" />
                                            <img src="../../../images/botones/imgdesmarcar.gif" title="Desmarca todos los estados" style="cursor:pointer; margin-left:5px;" onclick="setEstados(0);" />
				                        </legend>
				                        <table id="tblEstados" style="width:240px;">
				                        <colgroup>
				                            <col style="width:120px;" />
				                            <col style="width:120px;" />
				                        </colgroup>
				                        <tr>
				                            <td><asp:CheckBox ID="chkEstadoT" CssClass="check" runat="server" style="vertical-align:middle;" Checked />Tramitada</td>
				                            <td><asp:CheckBox ID="chkEstadoS" CssClass="check" runat="server" style="vertical-align:middle;" Checked/>Pagada</td>
				                        </tr>
				                        <tr>
				                            <td><asp:CheckBox ID="chkEstadoN" CssClass="check" runat="server" style="vertical-align:middle;" Checked/>Notificada</td>
				                            <td><asp:CheckBox ID="chkEstadoR" CssClass="check" runat="server" style="vertical-align:middle;" Checked/>Recuperada</td>
				                        </tr>
				                        <tr>
				                            <td><asp:CheckBox ID="chkEstadoA" CssClass="check" runat="server" style="vertical-align:middle;" Checked/>Aprobada</td>
				                            <td><asp:CheckBox ID="chkEstadoB" CssClass="check" runat="server" style="vertical-align:middle;" Checked/>No aprobada</td>
				                        </tr>
				                        <tr>
				                            <td><asp:CheckBox ID="chkEstadoL" CssClass="check" runat="server" style="vertical-align:middle;" Checked/>Aceptada</td>
				                            <td><asp:CheckBox ID="chkEstadoO" CssClass="check" runat="server" style="vertical-align:middle;" Checked/>No aceptada</td>
				                        </tr>
				                        <tr>
				                            <td><asp:CheckBox ID="chkEstadoC" CssClass="check" runat="server" style="vertical-align:middle;" Checked/>Contabilizada</td>
				                            <td><asp:CheckBox ID="chkEstadoX" CssClass="check" runat="server" style="vertical-align:middle;" Checked/>Anulada</td>
				                        </tr>
				                        </table>
				                        </fieldset>
                                        <br />
                                        <label style="width:50px; margin-left:5px;">Concepto</label>
                                        <asp:TextBox ID="txtConcepto" style="width:210px;" Text="" runat="server" MaxLength="50" />
				                    </td>
				                    <td>
				                        <fieldset style="width:385px; border:solid 1px navy">
				                        <legend>Motivos <img src="../../../images/botones/imgmarcar.gif" title="Marca todos los motivos" style="cursor:pointer; margin-left:5px;" onclick="setMotivos(1);" /><img src="../../../images/botones/imgdesmarcar.gif" title="Desmarca todos los motivos" style="cursor:pointer; margin-left:5px;" onclick="setMotivos(0);" /></legend>
                                            <table id="tblMotivos" style="width:385px;">
                                            <colgroup>
                                                <col style="width:100px;" />
                                                <col style="width:155px;" />
                                                <col style="width:130px;" />
                                            </colgroup>
                                            <tr>
                                                <td><asp:CheckBox ID="chkMotivo1" CssClass="check" runat="server" style="vertical-align:middle;" Checked="true" />Proyecto</td>
                                                <td><asp:CheckBox ID="chkMotivo4" CssClass="check" runat="server" style="vertical-align:middle;" Checked="true" />Actividad de gestión</td>
                                                <td><asp:CheckBox ID="chkMotivo2" CssClass="check" runat="server" style="vertical-align:middle;" Checked="true" />Actividad comercial</td>
                                            </tr>
                                            <tr>
                                                <td><asp:CheckBox ID="chkMotivo5" CssClass="check" runat="server" style="vertical-align:middle;" Checked="true" />Formación</td>
                                                <td><asp:CheckBox ID="chkMotivo3" CssClass="check" runat="server" style="vertical-align:middle;" Checked="true" />Ferias y exposiciones</td>
                                                <td><asp:CheckBox ID="chkMotivo6" CssClass="check" runat="server" style="vertical-align:middle;" Checked="true" />Comité de empresa</td>
                                            </tr>
                                            </table>
				                        </fieldset>
			                            <label id="lblCR" class="enlace" onclick="getCR()" style="width:70px;">C.R.</label>
                                        <asp:TextBox ID="txtCR" style="width:315px; margin-top:5px; margin-left:5px;" Text="" runat="server" ReadOnly="true" />
                                        <img id="img1" src='../../../Images/Botones/imgBorrar.gif' onclick="delCR()" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">
                                        <br />
			                            <label id="lblCli" class="enlace" onclick="getCli()" style="width:70px;">Cliente</label>
                                        <asp:TextBox ID="txtCli" style="width:315px; margin-top:5px; margin-left:5px;" Text="" runat="server" ReadOnly="true" />
                                        <img id="img3" src='../../../Images/Botones/imgBorrar.gif' onclick="delCli()" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">
                                        <br />
			                         
			                            <label id="lblProy" class="enlace" onclick="getPE()" style="width:70px;">Proyecto</label> 
                                        <asp:TextBox ID="txtProyecto" style="width:315px; margin-top:5px; margin-left:5px;" Text="" runat="server" ReadOnly="true" />
			                            <img id="imgGoma" src='../../../Images/Botones/imgBorrar.gif' title="Borra el filtro de proyecto" onclick="delPE()" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">
                                        <br />
			                            <label id="lblAprobador" class="enlace" onclick="getAprobador()" style="width:70px;">Aprobador</label> 
                                        <asp:TextBox ID="txtAprobador" style="width:315px; margin-top:5px; margin-left:5px;" Text="" runat="server" ReadOnly="true" />
			                            <img id="imgGomaAprobador" src='../../../Images/Botones/imgBorrar.gif' title="Borra el filtro de proyecto" onclick="delAprobador()" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">
                                        <br />
			                            <label id="lblBeneficiario" class="enlace" onclick="getBeneficiario()" style="width:70px;">Beneficiario</label> 
                                        <asp:TextBox ID="txtBeneficiario" style="width:315px; margin-top:5px; margin-left:5px;" Text="" runat="server" ReadOnly="true" />
			                            <img id="imgGomaBeneficiario" src='../../../Images/Botones/imgBorrar.gif' title="Borra el filtro de proyecto" onclick="delBeneficiario()" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server"><br />
				                    </td>
			                    </tr>
		                    </table>
				        </fieldset>
                        </td>
                        <td style="padding-top:10px;vertical-align: text-top;">
                            Referencia <asp:TextBox ID="txtReferencia" style="width:60px;" SkinID="numero" Text="" runat="server" onkeypress="javascript:if(event.keyCode==13){obtener();event.keyCode=0;}else{vtn2(event);}" />
                            <br />
                            <button id="btnObtener" type="button" onclick="obtener()" class="btnH25W90" style="margin-top:80px; margin-left:20px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                <img src="../../../../images/imgObtener.gif" /><span title="Obtener">Obtener</span>
                            </button>    

                        </td>
                    </tr>
                </table>
                <!-- Fin del contenido propio de la página -->
            </td>
            <td style="background-image:url(../../../Images/Tabla/6.gif); width:6px">&nbsp;</td>
        </tr>
        <tr>
            <td style="background-image:url(../../../Images/Tabla/1.gif); height:6px; width:6px"></td>
            <td style="background-image:url(../../../Images/Tabla/2.gif); height:6px"></td>
            <td style="background-image:url(../../../Images/Tabla/3.gif); height:6px; width:6px"></td>
        </tr>
    </table>
</div>
<table style="width:1000px;">
    <tr>
        <td style="padding-top:10px;">
	        <table id="tblTituloSolicitudes" style="width:980px;">
		        <colgroup>
	                <col style="width:30px; padding-left:3px;" />
			        <col style="width:20px; text-align:center;" />
			        <col style="width:60px; text-align:right; padding-right:10px;" />
                    <col style="width:70px;" />
                    <col style="width:70px;" />
			        <col style="width:150px;" />
			        <col style="width:180px;" />
			        <col style="width:140px;" />
			        <col style="width:140px;" />
			        <col style="width:50px;" />
			        <col style="width:70px; text-align:right; padding-right:2px;" />
		        </colgroup>
		        <tr class="TBLINI">
	                <td colspan="2">
                        <img src="../../../images/botones/imgMarcar.gif" onclick="mTabla('tblDatos')" title="Marca todas las líneas para ser exportadas" style="cursor:pointer; margin-top:1px;" />
		                <img src="../../../images/botones/imgDesmarcar.gif" onclick="dTabla('tblDatos')" title="Desmarca todas las líneas" style="cursor:pointer; margin-top:1px;" />   
                    </td>
			        <td title="Referencia">
                        <img style="cursor:pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6px" useMap="#img1" border="0">
					    <map id="img1" name="img1">
					        <area onclick="otx('tblDatos', 2, 0, '', '')" shape="RECT" coords="0,0,6,5" />
					        <area onclick="otx('tblDatos', 2, 1, '', '')" shape="RECT" coords="0,6,6,11" />
				        </map>&nbsp;Ref.
			        </td>
			        <td>
                        <img style="cursor:pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6px" useMap="#img2" border="0">
					    <map id="img2" name="img2">
					        <area onclick="otx('tblDatos', 3, 0, '', '')" shape="RECT" coords="0,0,6,5" />
					        <area onclick="otx('tblDatos', 3, 1, '', '')" shape="RECT" coords="0,6,6,11" />
				        </map>&nbsp;Estado
			        </td>
			        <td title="Fecha de tramitación">
                        <img style="cursor:pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6px" useMap="#img3" border="0">
					    <map id="img3" name="img3">
					        <area onclick="otx('tblDatos', 4, 0, 'fec', '')" shape="RECT" coords="0,0,6,5" />
					        <area onclick="otx('tblDatos', 4, 1, 'fec', '')" shape="RECT" coords="0,6,6,11" />
				        </map>&nbsp;F. Tram.
			        </td>
			        <td><img style="cursor:pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6px" useMap="#img4" border="0">
					    <map id="img4" name="img4">
					        <area onclick="otx('tblDatos', 5, 0, '', '')" shape="RECT" coords="0,0,6,5" />
					        <area onclick="otx('tblDatos', 5, 1, '', '')" shape="RECT" coords="0,6,6,11" />
				        </map>&nbsp;Beneficiario</td>
			        <td><img style="cursor:pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6px" useMap="#img5" border="0">
					    <map id="img5" name="img5">
					        <area onclick="otx('tblDatos', 6, 0, '', '')" shape="RECT" coords="0,0,6,5" />
					        <area onclick="otx('tblDatos', 6, 1, '', '')" shape="RECT" coords="0,6,6,11" />
				        </map>&nbsp;Concepto</td>
			        <td><img style="cursor:pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6px" useMap="#img6" border="0">
					    <map id="img6" name="img6">
					        <area onclick="otx('tblDatos', 7, 0, '', '')" shape="RECT" coords="0,0,6,5" />
					        <area onclick="otx('tblDatos', 7, 1, '', '')" shape="RECT" coords="0,6,6,11" />
				        </map>&nbsp;Motivo</td>
			        <td><img style="cursor:pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6px" useMap="#img7" border="0">
					    <map id="img7" name="img7">
					        <area onclick="otx('tblDatos', 8, 0, '', '')" shape="RECT" coords="0,0,6,5" />
					        <area onclick="otx('tblDatos', 8, 1, '', '')" shape="RECT" coords="0,6,6,11" />
				        </map>&nbsp;Proyecto</td>
			        <td title="Moneda"><img style="cursor:pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6px" useMap="#img8" border="0">
					    <map id="img8" name="img8">
					        <area onclick="otx('tblDatos', 9, 0, '', '')" shape="RECT" coords="0,0,6,5" />
					        <area onclick="otx('tblDatos', 9, 1, '', '')" shape="RECT" coords="0,6,6,11" />
				        </map>&nbsp;Mon</td>
			        <td><img style="cursor:pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6px" useMap="#img9" border="0">
					    <map id="img9" name="img9">
					        <area onclick="otx('tblDatos', 10, 0, 'num', '')" shape="RECT" coords="0,0,6,5" />
					        <area onclick="otx('tblDatos', 10, 1, 'num', '')" shape="RECT" coords="0,6,6,11" />
				        </map>&nbsp;Importe</td>
		        </tr>
	        </table>
	        <div id="divCatalogo" style="overflow-x:hidden; overflow:auto; width:996px; height:220px" runat="server" onscroll="scrollTablaSolicitudes()">
		        <div style="background-image:url(<%=Session["GVT_strServer"] %>Images/imgFT20.gif); width:980px;">
		            
		        </div>
	        </div>
	        <table id="Table2" style="width:980px;height:17px;">
		        <tr class="TBLFIN">
			        <td>&nbsp;</td>
		        </tr>
	        </table>
        </td>
    </tr>
    <tr>
        <td style="padding-top:5px; padding-left:0px;">
            <img alt="" src="../../../Images/imgTipoE.gif" class="ICO" />Estándar&nbsp;
            <img alt="" src="../../../Images/imgTipoB.gif" class="ICO" />Bono de transporte&nbsp;
            <img alt="" src="../../../Images/imgTipoP.gif" class="ICO" />Pago concertado
        </td>       
    </tr>
</table>

<asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnReferencia" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdAprobador" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdBeneficiario" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnCR" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnCli" runat="server" style="visibility:hidden" Text="" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript" language="javascript">
    function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
		    var strBoton = Botonera.botonID(eventArgument).toLowerCase();
		    switch (strBoton){
				case "pdf": //Boton exportar pdf
				{
					bEnviar = false;
					Exportar();
					break;
				}
		    }
	    }
	    var theform;
		theform = document.forms[0];
	    theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
	    theform.__EVENTARGUMENT.value = eventArgument;
	    if (bEnviar){
		    theform.submit();
	    }
//	    else{
//		    //Si se ha "cortado" el submit, se restablece el estado original de la botonera.
//		    $I("Botonera").restablecer();
//	    }
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

