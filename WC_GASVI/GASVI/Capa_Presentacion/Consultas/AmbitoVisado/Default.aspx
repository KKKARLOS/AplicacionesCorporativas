<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Consultas_AmbitoVisado_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script language="javascript">
    var sOpcion = "<%=sOpcion %>";
</script>
<div style="margin-top:-15px; margin-left:40px;">
    <div style="text-align:center;background-image: url('../../../Images/imgFondo185.gif'); background-repeat:no-repeat;
        width:185px; height:23px; position:relative; top:12px; left:20px; padding-top:5px; text-align:center;
        font:bold 12px Arial; color:#5894ae;">Criterios de selección</div>
    <table style="width:900px; height:160px;"  cellpadding="0">
        <tr>
            <td style="background-image:url(../../../Images/Tabla/7.gif); height:6px; width:6px"></td>
            <td style="background-image:url(../../../Images/Tabla/8.gif); height:6px;"></td>
            <td style="background-image:url(../../../Images/Tabla/9.gif); height:6px; width:6px"></td>
        </tr>
        <tr>
            <td style="background-image:url(../../../Images/Tabla/4.gif); width:6px">&nbsp;</td>
            <td style="background-image:url(../../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                <!-- Inicio del contenido propio de la página -->
                <table style="width:890px; margin-top:10px;" cellpadding="0"> 
                	<colgroup>
				        <col style="width:750px;" />
                        <col style="width:140px;" />
			        </colgroup>
                    <tr>
                        <td>
                        <fieldset style="width:730px;">
				            <legend><label id="lblPeriodo" class="enlace" onclick="getPeriodo()" runat="server">Periodo de aprobación</label>
				            <asp:TextBox ID="txtDesde" style="margin-left:15px;width:90px; vertical-align:middle; text-align:center;" Text="" readonly runat=server />
                                        <asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" ReadOnly=true runat=server />
                            <asp:TextBox ID="txtHasta" style="margin-left:5px; width:90px; vertical-align:middle; text-align:center;" Text="" readonly runat=server />
                                        <asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" ReadOnly=true runat=server />
                            </legend>
		                    <table style="width:720px; margin-bottom:10px;">
			                    <colgroup>
				                    <col style="width:300px;" />
				                    <col style="width:420px" />
			                    </colgroup>
			                    <tr>				    
				                    <td>
				                        <fieldset style="width:270px; border:solid 1px navy">
				                        <legend>Motivos <img src="../../../images/botones/imgmarcar.gif" title="Marca todos los motivos" style="cursor:pointer; margin-left:5px;" onclick="setMotivos(1);" /><img src="../../../images/botones/imgdesmarcar.gif" title="Desmarca todos los motivos" style="cursor:pointer; margin-left:5px;" onclick="setMotivos(0);" /></legend>
                                            <table id="tblMotivos" style="width:270px;">
                                            <colgroup>
                                                <col style="width:135px;" />
                                                <col style="width:135px;" />
                                            </colgroup>
                                            <tr>
                                                <td><asp:CheckBox ID="chkMotivo1" CssClass="check" runat="server" style="vertical-align:middle;" onclick="obtener()" Checked="true" />Proyecto</td>
                                                <td><asp:CheckBox ID="chkMotivo4" CssClass="check" runat="server" style="vertical-align:middle;" onclick="obtener()" Checked="true" />Actividad de gestión</td>
                                            </tr>
                                            <tr>
                                                <td><asp:CheckBox ID="chkMotivo2" CssClass="check" runat="server" style="vertical-align:middle;" onclick="obtener()" Checked="true" />Actividad comercial</td>
                                                <td><asp:CheckBox ID="chkMotivo5" CssClass="check" runat="server" style="vertical-align:middle;" onclick="obtener()" Checked="true" />Formación</td>
                                            </tr>
                                            <tr>
                                                <td><asp:CheckBox ID="chkMotivo3" CssClass="check" runat="server" style="vertical-align:middle;" onclick="obtener()" Checked="true" />Ferias y exposiciones</td>
                                                <td><asp:CheckBox ID="chkMotivo6" CssClass="check" runat="server" style="vertical-align:middle;" onclick="obtener()" Checked="true" />Comité de empresa</td>
                                            </tr>
                                            </table>
				                        </fieldset>
				                    </td>
				                    <td>
			                            <label id="lblCR" class="enlace" onclick="getCR()" style="width:70px;">C.R.</label>
                                        <asp:TextBox ID="txtCR" style="width:315px; margin-top:5px; margin-left:5px;" Text="" runat="server" ReadOnly="true" />
                                        <img id="img1" src='../../../Images/Botones/imgBorrar.gif' onclick="delCR()" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">
                                        <br />
			                            <label id="lblRP" class="enlace" onclick="getRP()" style="width:70px;" title="Responsable de proyecto">Resp.Proy.</label>
                                        <asp:TextBox ID="txtRP" style="width:315px; margin-top:5px; margin-left:5px;" Text="" runat="server" ReadOnly="true" />
                                        <img id="img2" src='../../../Images/Botones/imgBorrar.gif' onclick="delRP()" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">
                                        <br />
			                            <label id="lblCli" class="enlace" onclick="getCli()" style="width:70px;">Cliente</label>
                                        <asp:TextBox ID="txtCli" style="width:315px; margin-top:5px; margin-left:5px;" Text="" runat="server" ReadOnly="true" />
                                        <img id="img3" src='../../../Images/Botones/imgBorrar.gif' onclick="delCli()" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">
                                        <br />
			                            <label id="lblProy" class="enlace" onclick="getPE()" style="width:70px;">Proyecto</label> 
                                        <asp:TextBox ID="txtProyecto" style="width:315px; margin-top:5px; margin-left:5px;" Text="" runat="server" ReadOnly="true" />
			                            <img id="imgGoma" src='../../../Images/Botones/imgBorrar.gif' onclick="delPE()" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">
                                        <br />
			                            <label id="lblBeneficiario" class="enlace" onclick="getBeneficiario()" style="width:70px;">Beneficiario</label> 
                                        <asp:TextBox ID="txtBeneficiario" style="width:315px; margin-top:5px; margin-left:5px;" Text="" runat="server" ReadOnly="true" />
			                            <img id="imgGomaBeneficiario" src='../../../Images/Botones/imgBorrar.gif' title="Borra el filtro de proyecto" onclick="delBeneficiario()" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">
				                    </td>
			                    </tr>
		                    </table>
				        </fieldset>
                        </td>
                        <td style="padding-top:10px;vertical-align: text-top;">
                        Referencia <asp:TextBox ID="txtReferencia" style="width:60px;" SkinID="numero" Text="" runat="server" onkeypress="javascript:if(event.keyCode==13){obtener();event.keyCode=0;}else{vtn2(event);}" />
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
			        <col style="width:60px;" />
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
	                <td colspan="2"><img src="../../../images/botones/imgMarcar.gif" onclick="mTabla('tblDatos')" title="Marca todas las líneas para ser exportadas" style="cursor:pointer; margin-top:1px;" />
		            <img src="../../../images/botones/imgDesmarcar.gif" onclick="dTabla('tblDatos')" title="Desmarca todas las líneas" style="cursor:pointer; margin-top:1px;" />   
                    </td>
			        <td style="text-align:right; padding-right:10px;" title="Referencia">Ref.</td>
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
	        <div id="divCatalogo" style="overflow-x:hidden; overflow:auto; width:996px; height:300px" runat="server" onscroll="scrollTablaSolicitudes()">
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
    <label style="width:70px; visibility:hidden;">Concepto</label>
    <asp:TextBox ID="txtConcepto" style="width:315px; margin-top:3px; margin-left:8px; visibility:hidden;" Text="" runat="server" onkeypress="javascript:if(event.keyCode==13){obtener();event.keyCode=0;}" MaxLength="50" />
    <asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnReferencia" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnIdAprobador" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnIdBeneficiario" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnCR" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnRP" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnCli" runat="server" style="visibility:hidden" Text="" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript" language="javascript">
<!--
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
    	
-->
</script>
</asp:Content>

