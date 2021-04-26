<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>

<%@ Import Namespace="GEMO.BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<img id="imgPestHorizontalAux" src="../../../../Images/imgPestHorizontal.gif" style="z-index:1; position:absolute; left:40px; top:125px; cursor:pointer;" onclick="mostrarOcultarPestVertical(); mostrarOcultarIconoExcel();" />
<div id="divPestRetr"  style="position:absolute; left:22px; top:125px; width:965px; height:240px; clip:rect(auto auto 0px auto)">
    <table style="width:960px;">
        <tr valign="top">
            <td>
                <table style="width:940px; height:240px;" cellpadding="0">
                    <tr>
                        <td style="width:6px; background-image:url(../../../../Images/Tabla/4.gif)">&nbsp;</td>
                        <td style="padding:5px; background-image:url(../../../../Images/Tabla/5.gif)" valign="top">
                            <!-- Inicio del contenido propio de la página -->                           
                             <table style="width: 920px; margin-top:5px;" cellpadding="2" cellspacing="1" border="0">
                                <tr>
                                    <td>                  
                                        <table style="width:900px;">
                                        <colgroup>
                                            <col style="width:80px;"/>
                                            <col style="width:155px;"/>
                                            <col style="width:125px;"/>
                                            <col style="width:170px;"/>
                                            <col style="width:170px;"/>
                                            <col style="width:200px;"/>
                                        </colgroup>
                                            <tr>
                                                <td >
                                                    Cód.País&nbsp;<asp:TextBox ID="txtPrefijo" style="width:25px;" Text="" MaxLength="3" onkeypress='vtn2(event);' CssClass="txtNumM" SkinID="numero" runat="server" />
                                                </td>
                                                <td>
                                                    Línea&nbsp;<asp:TextBox id="txtNumLinea" onkeypress="if(event.keyCode==13){event.keyCode=0;bPestana=true;buscar();}else{vtn2(event);}" SkinID=numero tabIndex="1" Width="105px" runat="server" MaxLength="13"></asp:textbox>
                                                </td>
                                                <td>
                                                    Extensión&nbsp;<asp:TextBox id="txtNumExt" onkeypress="if(event.keyCode==13){event.keyCode=0;bPestana=true;buscar();}else{vtn2(event);}" SkinID=numero tabIndex="2" Width="50px" runat="server" MaxLength="6"></asp:textbox>
                                                </td>
                                                <td>
                                                    IMEI&nbsp;<asp:TextBox id="txtIMEI" onkeypress="if(event.keyCode==13){event.keyCode=0;buscar();}" tabIndex="3" Width="130px" runat="server" MaxLength="20"></asp:textbox>
                                                </td>
                                                <td>
                                                    ICC&nbsp;<asp:TextBox id="txtICC" onkeypress="if(event.keyCode==13){event.keyCode=0;buscar();}" tabIndex="4" Width="130px" runat="server" MaxLength="20"></asp:textbox>
                                                </td>
                                                <td>
                                                    <table align="center" style="width:100%;">
                                                    <tr>
                                                        <td width="51%" align="right" valign="middle"> 
                                                            <img alt="" class="ICO" style="padding-right:5px; cursor:pointer;" src="../../../../Images/imgPrefRefrescar.gif" title="Borra los criterios seleccionados" onclick="Limpiar();" />
                                                            <img alt="" class="ICO" src="../../../../Images/imgObtenerAuto.gif" title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" />
                                                            <input type="checkbox" id="chkActuAuto" class="check" runat="server" style="cursor:pointer;" />                                                            
                                                        </td>
                                                        <td width="49%" align="right" valign="middle">                                                                                                             
		                                                    <button id="btnObtener" type="button" onclick="bPestana=true;buscar();" class="btnH25W95" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" runat="server">
		                                                    <img src="../../../../images/imgObtener.gif" /><span title="Obtener">&nbsp;Obtener</span></button>                                                              
                                                        </td>
                                                    </tr>
                                                    </table>                                                
                                                </td>                                                
                                            </tr>                                
                                        </table>       
                                    </td>  
                                 </tr>
                                <tr>
                                    <td>
                                        <table align="center" style="width:920px; margin-top:5px;">
                                            <tr>
                                                <td>
                                                    <fieldset style="width: 285px; height:50px">
                                                        <legend><label id="lblEmpresa" class="enlace" onclick="getCriterios(1)" runat="server">Empresa</label>
                                                        <img alt="" class="ICO" id="Img6" src="../../../../Images/Botones/imgBorrar.gif" onclick="delCriterios(1)" runat="server" style="cursor:pointer; margin-left:10px;" /></legend>
                                                        <div id="divEmpresa" style="overflow-x:hidden; overflow-y:auto; width:276px; height:32px; margin-top:2px">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                                                <table id="tblEmpresa" style="width:260px;">
                                                                    <%//=strHTMLEmpresa%>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </td>
                                                <td>
                                                    <fieldset style="width: 285px; height:50px">
                                                        <legend><label id="lblResponsable" class="enlace" onclick="getCriterios(2)" runat="server" title="Responsable de la línea">Responsable</label>
                                                        <img alt="" class="ICO" id="Img7" src="../../../../Images/Botones/imgBorrar.gif" onclick="delCriterios(2)" runat="server" style="cursor:pointer; margin-left:10px;" /></legend>
                                                        <div id="divResponsable" style="overflow-x:hidden; overflow-y:auto; width:276px; height:32px; margin-top:2px">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                                                <table id="tblResponsable" style="width:260px;">
                                                                    <%//=strHTMLResponsable%>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </td>
                                                <td>
                                                    <fieldset style="width: 285px; height:50px">
                                                        <legend>
                                                            <label id="lblBeneficiario" class="enlace" onclick="getCriterios(3)" runat="server" title="Beneficiario de la línea">Beneficiario</label>
                                                            <img alt="" class="ICO" id="Img8" src="../../../../Images/Botones/imgBorrar.gif" onclick="delCriterios(3)" runat="server" style="cursor:pointer; margin-left:10px;" />
                                                        </legend>
                                                        <div id="divBeneficiario" style="overflow-x:hidden; overflow-y:auto; width:276px; height:32px; margin-top:2px">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                                                 <table id="tblBeneficiario" style="width:260px;">
                                                                    <%//=strHTMLBeneficiario%>
                                                                 </table>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>                           
                                                <td>
                                                    <fieldset style="width: 285px; height:50px">
                                                        <legend>
                                                            <label id="lblCR" class="enlace" onclick="getCriterios(4)" runat="server">Departamento</label>
                                                            <img alt="" class="ICO" id="Img9" src="../../../../Images/Botones/imgBorrar.gif" onclick="delCriterios(4)" runat="server" style="cursor:pointer; margin-left:10px;" />
                                                        </legend>
                                                        <div id="divCR" style="overflow-x:hidden; overflow-y:auto; width:276px; height:32px; margin-top:2px">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                                                <table id="tblCR" style="width:260px;">
                                                                    <%//=strHTMLCR%>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </td>
                                                <td>
                                                    <fieldset style="width: 285px; height:50px">
                                                        <legend>
                                                            <label id="lblEstado" class="enlace" onclick="getCriterios(5)" runat="server">Estado</label>
                                                            <img alt="" class="ICO" id="Img10" src="../../../../Images/Botones/imgBorrar.gif" onclick="delCriterios(5)" runat="server" style="cursor:pointer; margin-left:10px;" />
                                                        </legend>
                                                        <div id="divEstado" style="overflow-x:hidden; overflow-y:auto; width:276px; height:32px; margin-top:2px">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                                                <table id="tblEstado" style="width:260px;">
                                                                    <%//=strHTMLEstado%>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </td>
                                                <td>
                                                    <fieldset style="width: 285px; height:50px">
                                                        <legend>
                                                            <label id="lblMedio" class="enlace" onclick="getCriterios(6)" runat="server">Medio</label>
                                                            <img alt="" class="ICO" id="Img11" src="../../../../Images/Botones/imgBorrar.gif" border="0" onclick="delCriterios(6)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;" />
                                                        </legend>
                                                        <div id="divMedio" style="overflow-x:hidden; overflow-y:auto; width:276px; height:32px; margin-top:2px">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                                                <table id="tblMedio" style="width:260px;">
                                                                    <%//=strHTMLMedio%>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </td>
                                            </tr>                 
                                        </table>     
                                                                     
                                    <!-- Fin del contenido propio de la página -->
                                 </td>
                                </tr>
                             </table>

                            <!-- Fin del contenido propio de la página -->
                        </td>
                        <td style="width:6px; background-image:url(../../../../Images/Tabla/6.gif)">&nbsp;</td>
                    </tr>  
                                 
                    <tr>
	                    <td style="width:6px;height:6px;background-image:url(../../../../Images/Tabla/1.gif)"></td>
                        <td style="height:6px;background-image:url(../../../../Images/Tabla/2.gif)"></td>
                        <td style="width:6px;height:6px;background-image:url(../../../../Images/Tabla/3.gif)"></td>
                     </tr>  
                </table>
            </td>
        </tr> 
    </table>
</div>
<table id="tblGeneral" style="width:970px;" align="center">
    <tr>
        <td>
<%--            <fieldset align="center" style="margin-top:10px">
                <legend>Líneas</legend>  --%>
               <table id="tblTitulo" style="width:954px; margin-top:10px; height:17px">
                    <colgroup>
                        <col style="width:20px;" />
                        <col style="width:40px;" />
                        <col style="width:104px;" />
                        <col style="width:80px;" />
                        <col style="width:355px;" />
                        <col style="width:355px;" />
                    </colgroup>
                    <tr>
                    <td colspan="5">
                    
                    </td>
                    </tr>
                    <tr class="TBLINI" align="left">
                        <td colspan="2" style="padding-left:5px;">Cód.País</td>	
	                    <td>
	                        <img alt="" class="ICO" style="display:none; cursor:pointer" height="11px" src="../../../../Images/imgFlechas.gif" width="6px" usemap="#img1" />
	                        <map id="img1">
	                            <area alt="" onclick="ot('tblDatos', 2, 0, '', 'scrollTablaLineas()')" shape="RECT" coords="0,0,6,5" />
	                            <area alt="" onclick="ot('tblDatos', 2, 1, '', 'scrollTablaLineas()')" shape="RECT" coords="0,6,6,11" />
                            </map>Línea
                            <img alt="" class="ICO" id="imgLupa1" style="display:none; cursor:pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
		                        height="11px" src="../../../../Images/imgLupaMas.gif" width="20px" tipolupa="2" /> 
		                    <img alt="" class="ICO" style="cursor:pointer; display:none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)"
		                        height="11px" src="../../../../Images/imgLupa.gif" width="20px" tipolupa="1" />
	                    </td>
                        <td>
                            <img alt="" class="ICO" style="display:none; cursor:pointer" height="11px" src="../../../../Images/imgFlechas.gif" width="6px" usemap="#img2" />
	                        <map id="img2">
	                            <area alt="" onclick="ot('tblDatos', 3, 0, '', 'scrollTablaLineas()')" shape="RECT" coords="0,0,6,5" />
	                            <area alt="" onclick="ot('tblDatos', 3, 1, '', 'scrollTablaLineas()')" shape="RECT" coords="0,6,6,11" />
                            </map>Extension
                        </td>
                        <td>
                            <img alt="" class="ICO" style="display:none; cursor:pointer" height="11px" src="../../../../Images/imgFlechas.gif" width="6px" usemap="#img3" />
	                        <map id="img3">
	                            <area alt="" onclick="ot('tblDatos', 4, 0, '', 'scrollTablaLineas()')" shape="RECT" coords="0,0,6,5" />
	                            <area alt="" onclick="ot('tblDatos', 4, 1, '', 'scrollTablaLineas()')" shape="RECT" coords="0,6,6,11" />
                            </map>Responsable
                        </td>
                        <td>
                            <img alt="" class="ICO" style="display:none; cursor:pointer" height="11px" src="../../../../Images/imgFlechas.gif" width="6px" usemap="#img4" />
	                        <map id="img4">
	                            <area alt="" onclick="ot('tblDatos', 5, 0, '', 'scrollTablaLineas()')" shape="RECT" coords="0,0,6,5" />
	                            <area alt="" onclick="ot('tblDatos', 5, 1, '', 'scrollTablaLineas()')" shape="RECT" coords="0,6,6,11" />
                            </map>Beneficiario / Departamento
                        </td>
	                </tr>	            
                </table>
                <div id="divCatalogo" style="overflow-x:hidden; overflow-y:auto; width:970px; height:480px;" onscroll="scrollTablaLineas();" runat="server">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:954px">
                        <%=strTablaHTML%>
                    </div>
                </div>
                <table id="TABLE2" style="height:17px" width="954px" align="left">
                    <tr class="TBLFIN">
                        <td align="left" style="padding-left:3px;">Nº de líneas:
                            <asp:Label id="lblNumLineas" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
<%--            </fieldset> --%>
        </td>
    </tr>
    <tr>          
        <td style="padding-top:15px;">
            <img alt="" src="../../../../Images/imgPreactiva.gif" class="ICO" />Preactiva&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../../Images/imgActiva.gif" class="ICO" />Activa&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../../Images/imgBloqueada.gif" class="ICO" />Bloqueada&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../../Images/imgPreinactiva.gif" class="ICO" />Preinactiva&nbsp;&nbsp;&nbsp;
            <img alt="" src="../../../../Images/imgInactiva.gif" class="ICO" />Inactiva&nbsp;&nbsp;&nbsp;
        </td>       
    </tr>
</table>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
    <script type="text/javascript">
        function __doPostBack(eventTarget, eventArgument) {
            var bEnviar = true;
            if (eventTarget.split("$")[2] == "Botonera") {
                var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			    switch (strBoton){
				    case "nuevo": 
				    {
                        bEnviar = false;
                        Nuevo();
					    break;
				    }
				    case "eliminar": 
				    {
                        bEnviar = false;
                        eliminar();
					    break;
				    }
			    }
		    }

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

