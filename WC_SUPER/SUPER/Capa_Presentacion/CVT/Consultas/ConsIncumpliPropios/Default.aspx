<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var strServer = "<%=Session["strServer"]%>";       
</script>
<center>
<br />
    <table id="tblCatGen" style="width: 1020px; text-align:left;"  cellpadding="5px" cellspacing="0" border="0">
        <colgroup> 
            <col width="770px"/>
            <col width="250px"/>
        </colgroup> 
        <tr>
            <td valign="top">
                <fieldset style="width:99%;height:125px;" id="fldResumen" runat="server">
                <legend title="">&nbsp;Resumen de incumplimientos&nbsp;</legend> 
	                <table id="resumenCab" style="text-align:right; margin-top:10px; width: 740px" border="0">
	                <colgroup> 
                        <col width="440px"/>
                        <col width="100px"/> 
                        <col width="100px"/> 
                        <col width="100px"/>
                    </colgroup>         
	                <tr class="TBLINI">     
                        <td style="text-align:left;padding-left:5px;">Denominación de la tarea</td>
                        <td style="text-align:right" title="Nº de tareas realizadas">NTR</td>
                        <td style="text-align:right;" title="Nº de tareas realizadas fuera de plazo">NTRFP</td>
                        <td style="text-align:right;padding-right:5px;" title="Nº de tareas sin hacer fuera de plazo a día de hoy">NTFPDH</td>                        
                    </tr>
                    </table>
                    <div id="resumenDet2" style="height:60px">
	                    <table id="resumenDet" style="text-align:right;width: 740px;" border="0">
	                    <colgroup> 
                            <col width="440px"/>
                            <col width="100px"/> 
                            <col width="100px"/> 
                            <col width="100px"/> 
                        </colgroup> 
                        </table>
                    </div>
                    <div id="divTotales">
	                    <table id="Totales" style="text-align:right; width:740px" border="0"> 
	                    <colgroup> 
                            <col width="440px"/>
                            <col width="100px"/> 
                            <col width="100px"/> 
                            <col width="100px"/> 
                        </colgroup>	                                     
                        <tr class="TBLFIN">
                            <td colspan="4">&nbsp;</td>
                        </tr>
                        </table>  
                    </div>                                                    
                </fieldset>
            </td>
            <td valign="top" align="center">
                <fieldset style="width: 140px; height:125px; text-align: left; padding:5px;">
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
                            <tr>
                                <td colspan="2" align="center">
                                    <button id="btnObtener" type="button" onclick="buscar()" class="btnH25W90" hidefocus="hidefocus" onmouseover="mostrarCursor(this)" runat="server" style="display:inline;">
                                        <span><img src="../../../../images/imgObtener.gif" title="Obtener" />Obtener</span>
                                    </button>
							    </td>
                            </tr>											
                        </table>
                </fieldset>	                
            </td>                      
        </tr> 
        <tr>
            <td colspan="2">
                <fieldset style="width:95%;height:390px;" runat="server">
                <legend title="">&nbsp;Detalle de incumplimientos&nbsp;</legend> 
                    <table style="width: 100%">
                    <tr>
                        <td>                           
                            <table id="detalleCab1" style="text-align:left; margin-top:10px; width: 930px; display:block" border="0">
                            <colgroup> 
                                <col width="350px"/>
                                <col width="380px"/> 
                                <col width="100px"/>
                                <col width="100px"/>  
                            </colgroup>         
                            <tr class="TBLINI">     
                                <td>Apartado del CV</td>
                                <td>Registro</td>                                 
                                <td style="text-align:center">F.Límite</td> 
                                <td style="text-align:center">F.Realización</td>
                            </tr>
                            </table>
                            <table id="detalleCab2" style="text-align:left; margin-top:10px; width: 930px; display:none" border="0">
                            <colgroup> 
                                <col width="250px"/>
                                <col width="200px"/>
                                <col width="280px"/> 
                                <col width="100px"/>
                                <col width="100px"/>  
                            </colgroup>         
                            <tr class="TBLINI">     
                                <td>Profesional</td>
                                <td title="Área del CV a validar">Área CV a validar</td>
                                <td>Registro</td>                                 
                                <td style="text-align:center">F.Límite</td> 
                                <td style="text-align:center">F.Realización</td>
                            </tr>
                            </table>                             
                            <table id="detalleCab3" style="text-align:left; margin-top:10px; width: 930px; display:none" border="0">
                            <colgroup> 
                                <col width="350px"/>
                                <col width="380px"/> 
                                <col width="100px"/>
                                <col width="100px"/>  
                            </colgroup>         
                            <tr class="TBLINI">     
                                <td>Proyecto</td>
                                <td>Cliente</td>                                 
                                <td style="text-align:center">F.Límite</td> 
                                <td style="text-align:center">F.Realización</td>
                            </tr>
                            </table>                                                                                   
                        </td>
                    </tr>
                    <tr>
                        <td>                        
                            <div id="divCatalogo" style="overflow-x: hidden; overflow-y: auto; width: 946px; height: 320px;">
                                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif');width: 930px;">
                                </div>
                            </div>
                         </td>
                    </tr>                       
                    <tr>
                        <td>                                    
                            <table id="tblResultado" style="height:17px; width:930px;">
                                <tr class="TBLFIN">
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                         </td>
                    </tr>  
                    </table>                        
                </fieldset>
            </td>
        </tr>                                   
    </table>  
</center>	
<input type="hidden" name="hdnErrores" id="hdnErrores" value="" />
<asp:TextBox ID="hdnDesde" style="visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnHasta" style="visibility:hidden" Text="" readonly="true" runat="server" />
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


