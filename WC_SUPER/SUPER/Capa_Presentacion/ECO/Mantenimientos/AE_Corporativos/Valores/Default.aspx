<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var strEstructuraNodoLarga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
</script>
<img id="imgPestHorizontalAux" src="../../../../../Images/imgPestHorizontal.gif" style="z-index:0; position:absolute; left:40px; top:125px; cursor:pointer;" onclick="mostrarOcultarPestVertical(); mostrarOcultarIconoExcel();" />
<div id="divPestRetr"  style="position:absolute; left:10px; top:125px; width:960px; height:140px; clip:rect(auto auto 0px auto)">
    <table style="width:960px;text-align:left;" ">
        <tr valign="top">
            <td>
                <table style="width:940px; height:140px;" cellpadding="0">
                    <tr>
                        <td style="width:6px; background-image:url(../../../../../Images/Tabla/4.gif)">&nbsp;</td>
                        <td style="padding:5px; background-image:url(../../../../../Images/Tabla/5.gif)" valign="top">
                            <!-- Inicio del contenido propio de la página -->                           
                             <table style="width: 920px; margin-top:5px;" cellpadding="2" cellspacing="1" border="0">
                                <tr>
                                    <td>                  
                                        <table align="right" style="width:200px">
                                        <tr>
                                            <td width="51%" align="right" valign="middle"> 
                                                <img alt="" class="ICO" style="padding-right:5px; cursor:pointer;" src="../../../../../Images/imgPrefRefrescar.gif" title="Borra los criterios seleccionados" onclick="Limpiar();" />
                                                <img alt="" class="ICO" src="../../../../../Images/imgObtenerAuto.gif" title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" />
                                                <input type="checkbox" id="chkActuAuto" class="check" runat="server" style="cursor:pointer;" />                                                            
                                            </td>
                                            <td width="49%" align="right" valign="middle">                                                                                                             
		                                        <button id="btnObtener" type="button" onclick="bPestana=true;buscar();" class="btnH25W95" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" runat="server">
		                                        <img src="../../../../../Images/imgObtener.gif" /><span title="Obtener">&nbsp;Obtener</span></button>                                                              
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
                                                    <fieldset style="width: 285px; height:60px">
                                                        <legend>
                                                            <label id="lblCR" class="enlace" onclick="getCriterios(36)" runat="server"><%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %></label>
                                                            <img alt="" class="ICO" id="Img9" src="../../../../../Images/Botones/imgBorrar.gif" onclick="delCriterios(36)" runat="server" style="cursor:pointer; margin-left:10px;" />
                                                        </legend>
                                                        <div id="divCR" style="overflow-x:hidden; overflow-y:auto; width:276px; height:48px; margin-top:2px">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                                                <table id="tblCR" style="width:260px;">
                                                                    <%//=strHTMLCR%>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </td>
                                                <td>
                                                    <fieldset style="width: 285px; height:60px">
                                                        <legend>
                                                            <label id="lblCEEC" class="enlace" onclick="getCriterios(40)" runat="server">CEEC</label>
                                                            <img alt="" class="ICO" id="Img10" src="../../../../../Images/Botones/imgBorrar.gif" onclick="delCriterios(40)" runat="server" style="cursor:pointer; margin-left:10px;" />
                                                        </legend>
                                                        <div id="divEstado" style="overflow-x:hidden; overflow-y:auto; width:276px; height:48px; margin-top:2px">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                                                <table id="tblCEEC" style="width:260px;">
                                                                    <%//=strHTMLCEEC%>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </td>
                                                <td>
                                                    <fieldset style="width: 285px; height:60px">
                                                        <legend>
                                                            <label id="lblValores" class="enlace" onclick="getCriterios(41)" runat="server">Valores</label>
                                                            <img alt="" class="ICO" id="Img11" src="../../../../../Images/Botones/imgBorrar.gif" border="0" onclick="delCriterios(41)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;" />
                                                        </legend>
                                                        <div id="divMedio" style="overflow-x:hidden; overflow-y:auto; width:276px; height:48px; margin-top:2px">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                                                <table id="tblValores" style="width:260px;">
                                                                    <%//=strHTMLValores%>
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
                        <td style="width:6px; background-image:url(../../../../../Images/Tabla/6.gif)">&nbsp;</td>
                    </tr>  
                                 
                    <tr>
	                    <td style="width:6px;height:6px;background-image:url(../../../../../Images/Tabla/1.gif)"></td>
                        <td style="height:6px;background-image:url(../../../../../Images/Tabla/2.gif)"></td>
                        <td style="width:6px;height:6px;background-image:url(../../../../../Images/Tabla/3.gif)"></td>
                     </tr>  
                </table>
            </td>
        </tr> 
    </table>
</div>
<table id="tblGeneral" style="width:960px;" align="center">
    <tr>
        <td>
<%--            <fieldset align="center" style="margin-top:10px">
                <legend>Líneas</legend>  --%>

               <table id="tblTitulo" style="width:960px; margin-top:20px; height:17px">
                    <colgroup>
                        <col style="width:110px;" />
                        <col style="width:200px;"/>
                        <col style="width:250px;"/>
                        <col style="width:250px;"/>
                        <col style="width:150px;"/>
                    </colgroup>
                	<tr class="TBLINI">
					    <td style="text-align:right;">
					        <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa1',event)"
							    height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						    <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa1')"
							    height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						    <img style="CURSOR: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					        <map name="img1">
					            <area onclick="ot('tblDatos', 3, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					            <area onclick="ot('tblDatos', 3, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				            </map>&nbsp;Nº&nbsp;&nbsp;
					    </td>
					    <td style="text-align:left;"><img style="CURSOR: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
						        <map name="img2">
						            <area onclick="ot('tblDatos', 4, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						            <area onclick="ot('tblDatos', 4, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					            </map>&nbsp;Proyecto&nbsp;<img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa2')"
							    height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa2',event)"
							    height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1">
					    </td>
                        <td>&nbsp;
                            <img style="CURSOR: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#img3" border="0">
					        <map name="img3">
					            <area onclick="ot('tblDatos', 5, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					            <area onclick="ot('tblDatos', 5, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				            </map>&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %></td>
                        <td><img style="CURSOR: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#img4" border="0">
						        <map name="img4">
						            <area onclick="ot('tblDatos', 6, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						            <area onclick="ot('tblDatos', 6, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					            </map>&nbsp;CEEC</td>
                        <td>
                            <img style="CURSOR: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#img5" border="0">
					        <map name="img5">
					            <area onclick="ot('tblDatos', 7, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					            <area onclick="ot('tblDatos', 7, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				            </map>&nbsp;Valor
                        </td>
	                </tr>	            
                </table>
                <div id="divCatalogo" style="overflow-x:hidden; overflow-y:auto; width:976px; height:500px;" onscroll="scrollTablaProy();" runat="server">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:960px">
                        <%=strTablaHTML%>
                    </div>
                </div>
                <table id="TABLE2" style="height:17px" width="960px" align="left">
                    <tr class="TBLFIN">
                        <td align="left" style="padding-left:3px;">Nº de proyectos:
                            <asp:Label id="lblNumLineas" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
<%--            </fieldset> --%>
        </td>
    </tr>
</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				//case "nuevo": 
				//{
                //    bEnviar = false;
                //    Nuevo();
				//	break;
				//}
				//case "eliminar": 
				//{
                //    bEnviar = false;
                //    eliminar();
				//	break;
				//}
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

