<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var strServer = "<%=Session["strServer"]%>"; 
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var strEstructuraSubnodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>";
    var nEstructuraMinima = <%=nEstructuraMinima.ToString() %>; 
    <%=sCriterios %>            
</script>
<center>
    <table id="tblCatGen" style="width: 980px; text-align:left;"  cellpadding="5px" cellspacing="0" border="0">
        <colgroup> 
            <col width="980px"/>
        </colgroup> 
        <tr>
            <td valign="top">
                <fieldset style="width:960px;height:160px;" id="fldResumen" runat="server">
                <legend title="">&nbsp;Criterios de selección&nbsp;</legend> 
	                <table id="criteriosSeleccion" style="text-align:left; margin-top:10px; width: 940px" border="0">
	                <colgroup> 
                        <col width="310px"/>
                        <col width="320px"/>  
                        <col width="200px"/> 
                        <col width="110px"/>                          
                    </colgroup>         
	                <tr>     
                        <td>
                            <fieldset style="width: 290px; height:80px; padding:5px;">
                                <legend><label id="lblProfesional" class="enlace" onclick="getCriterios(27)" runat="server">Profesional</label><img id="Img5" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(27)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                <div id="divProfesional" style="overflow-X:hidden; overflow-y:auto; WIDTH: 276px; height:56px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                     <table id="tblProfesional" class="texto" style="width:260px;text-align:left">
                                     <%=strHTMLProfesional%>
                                     </table>
                                    </div>
                                </div>
                            </fieldset>
                        </td>
                        <td align="center">                                                                                       
                            <fieldset style="width: 290px; height:80px; padding:5px;text-align:left">
                                <legend>
                                    <label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label>
                                    <img id="Img14" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:7px;">
                                </legend>
                                <div id="divAmbito" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:56px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px;">
                                     <table id="tblAmbito" class="texto" style="width:260px;text-align:left">
                                     <%=strHTMLAmbito%>
                                     </table>
                                    </div>
                                </div>
                            </fieldset>                        
                        </td>
                        <td align="center">
                            <fieldset style="width: 140px; height:80px; text-align: left; padding:5px;">
                                <legend><label>Periodo</label></legend>
                                    <table style="width:135px;margin-top:10px" cellpadding="6px" >
                                        <colgroup><col style="width:35px;"/><col style="width:100px;"/></colgroup>
                                        <tr>
                                            <td>Desde</td>
                                            <td>
                                                <asp:TextBox ID="txtFechaInicio" runat="server" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="VerFecha('D');" goma=0 />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Hasta</td>
                                            <td>
                                                <asp:TextBox ID="txtFechaFin" runat="server" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="VerFecha('H');" goma=0 />
                                            </td>
                                        </tr>							
                                    </table>
                            </fieldset>	                          
                        </td>   
                        <td align="center">
                            <button id="btnObtener" type="button" onclick="buscar()" class="btnH25W90" runat="server" style="float:right" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/imgObtener.gif" /><span title="Obtener">Obtener</span></button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                                <table style="width:940px;margin-top:10px;text-align:left" cellpadding="8px" >
	                                <colgroup> 
                                        <col width="310px"/>
                                        <col width="320px"/>  
                                        <col width="310px"/>                        
                                    </colgroup>  
                                    <tr>
                                        <td>
                                        Con más de&nbsp;&nbsp;<asp:TextBox ID="txtTareasVencidas" runat="server" style="width:30px; text-align:right; padding-right:2px;" onfocus="fn(this,5,0)" MaxLength="5" Text="20" />&nbsp;&nbsp;tareas vencidas
                                        </td>
                                        <td>
                                        <input type="checkbox" id="chkMiEquipoDirecto" class="check" checked runat="server" />&nbsp;&nbsp;Mi equipo directo
						                </td>
                                        <td align="center">
                                        <input type="checkbox" id="chkTareasFueraPlazoSinHacer" class="check" checked runat="server" />&nbsp;&nbsp;Con tareas sin hacer fuera de plazo a día de hoy
						                </td>   
                                    </tr>                                        											
                                </table>                          
                        </td>                                                                   
                    </tr>
                    </table>                                             
                </fieldset>
            </td>
        </tr> 
        <tr>
            <td colspan="2">
                    <table style="width: 100%">
                    <tr>
                        <td>                           
                            <table id="Cab" style="text-align:left; margin-top:5px; width: 970px" border="0">
                            <colgroup> 
                                <col width="230px"/>
                                <col width="250px"/> 
                                <col width="250px"/>
                                <col width="80px"/> 
                                <col width="80px"/> 
                                <col width="80px"/>  
                            </colgroup>         
                            <tr class="TBLINI">     
                                <td style="padding-left:5px;">Centro de responsabilidad</td>
                                <td style="padding-left:5px;">Profesional</td>                                 
                                <td>Evaluador</td> 
                                <td style="text-align:right" title="Nº de tareas realizadas">
                                    <IMG style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgN1" border="0" />
				                    <MAP name="imgN1">
				                        <AREA onclick="ot('tblDatos', 3, 0, 'num')" shape="RECT" coords="0,0,6,5">
				                        <AREA onclick="ot('tblDatos', 3, 1, 'num')" shape="RECT" coords="0,6,6,11">
			                        </MAP>NTR
                                </td>
                                <td style="text-align:right;" title="Nº de tareas realizadas fuera de plazo">
                                    <IMG style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgN2" border="0" />
				                    <MAP name="imgN2">
				                        <AREA onclick="ot('tblDatos', 4, 0, 'num')" shape="RECT" coords="0,0,6,5">
				                        <AREA onclick="ot('tblDatos', 4, 1, 'num')" shape="RECT" coords="0,6,6,11">
			                        </MAP>NTRFP
                                </td>
                                <td style="text-align:right;padding-right:5px;" title="Nº de tareas sin hacer fuera de plazo a día de hoy">
                                    <IMG style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgN3" border="0" />
   				                    <MAP name="imgN3">
				                        <AREA onclick="ot('tblDatos', 5, 0, 'num')" shape="RECT" coords="0,0,6,5">
				                        <AREA onclick="ot('tblDatos', 5, 1, 'num')" shape="RECT" coords="0,6,6,11">
			                        </MAP>NTFPDH
                                </td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>                        
                            <div id="divCatalogo" style="overflow-x: hidden; overflow-y: auto; width: 986px; height: 300px;" onscroll="scrollTabla()">
                                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif');width: 970px;">
                                </div>
                            </div>
                         </td>
                    </tr>                       
                    <tr>
                        <td>                                    
                            <table id="tblResultado" style="height:17px; width:970px;">
                                <tr class="TBLFIN">
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                         </td>
                    </tr>  
                    <tr>
	                    <td style="padding-top: 5px;">
                            &nbsp;<img class="ICO" src="../../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo&nbsp;&nbsp;&nbsp;
	                    </td>
	                </tr>
                    </table>                        
            </td>
        </tr>                                   
    </table>  
</center>	
<input type="hidden" name="hdnErrores" id="hdnErrores" value="" />
<asp:TextBox ID="hdnDesde" style="visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnHasta" style="visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnProfesionales" style="visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnIdEstructura" style="visibility:hidden" Text="" readonly="true" runat="server" />
<iframe id="iFrmDescarga" frameborder="0" name="iFrmDescarga" width="10px" height="10px" style="visibility:hidden" ></iframe>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

            switch (strBoton) {
                case "excel": 
                    {
                        bEnviar = false;
                        generarExcel();
                        //setTimeout("generarExcel();", 100);
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
-->
</script>
</asp:Content>


