<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" validateRequest=false AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
-->
</script>
<div id="estructura_dia" class="texto" style="position:absolute; background-color: #FFFFFF;
         border-style:solid;border-width:2px;border-color:navy;
         left:360px;
         top:200px; 
         width:260px;z-index:3;visibility:hidden;PADDING:10px;">
         <div align="center"><b>Estructura fichero de entrada</b></div><br>
        - Los campos del fichero deben estar separados por tabuladores:<br><br>
        1.- Identificador de tarea.<br>
        2.- Identificador de usuario.<br>
        3.- Fecha (DD/MM/AAAA).<br>
        4.- Esfuerzo en horas.<br>
        5.- Comentarios<br>
</div>
<div id="estructura_masiva" class="texto" style="position:absolute; background-color: #FFFFFF;
         border-style:solid;border-width:2px;border-color:navy;
         left:360px;
         top:200px; 
         width:260px;z-index:3;visibility:hidden;PADDING:10px;">
         <div align="center"><b>Estructura fichero de entrada</b></div><br>
        - Los campos del fichero deben estar separados por tabuladores:<br><br>
        1.- Identificador de tarea.<br>
        2.- Identificador de usuario.<br>
        3.- Fecha desde (DD/MM/AAAA).<br>
        4.- Fecha hasta (DD/MM/AAAA).<br>
        5.- Esfuerzo en horas.<br>
        6.- Permitir imputar festivos o no laborables<br>
        7.- Comentarios<br>        
</div>
<table id="nombreProyecto" style="width:960px;margin-left:20px" align="left">
    <tr>
        <td>
            <center>
            <table border="0" cellspacing="0" cellpadding="0" style="text-align:left">
              <tr>
                <td width="6px" height="6px" background="../../../Images/Tabla/7.gif"></td>
                <td height="6px" background="../../../Images/Tabla/8.gif"></td>
                <td width="6px" height="6" background="../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6px" background="../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../Images/Tabla/5.gif" style="padding:5px">
                    <table id="tblDatos2" class="texto"  cellspacing="7px" cellpadding="4px" width="730px">
                        <colgroup>
                            <col style="width:170px;" />
                            <col style="width:50px;" />
                            <col style="width:170px;" />
                            <col style="width:150px;" />
                            <col style="width:50px;" />
                            <col style="width:170px;" />
                        </colgroup>
                        <tr>
		                    <td colspan="5" style="vertical-align:middle">Fichero&nbsp;&nbsp;
		                        <input id="uplTheFile" type="file"  style="width:500px" name="uplTheFile" size="88" class="txtIF" runat="server" onchange="HabCarga();return;LeerFichero(this.value);" contenteditable="false"/>
		                    </td>
                            <td align="right">
						        <button id="btnCargar" type="button" disabled onclick="LeerFichero($I('uplTheFile').value);" class="btnH25W95" runat="server" hidefocus="hidefocus" 
							         onmouseover="se(this, 25);mostrarCursor(this);">
							        <img src="../../../images/botones/imgMicroscopio.gif" /><span>Analizar</span>
						        </button>	
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3"><label onmouseover="mostrar();" onmouseout="ocultar();">Ver formato del fichero de entrada</label></td>
                            <td colspan="2">Formato&nbsp;&nbsp;
							    <asp:DropDownList ID="cboFormato" runat="server" Width="125px" Enabled=false onChange="">
							        <asp:ListItem Value="F">Fichero de texto</asp:ListItem>
							        <asp:ListItem Value="X">Fichero XML</asp:ListItem>
							    </asp:DropDownList>					
						    </td>                            
                            <td align="right">
						        <button id="btnVisualizar" type="button" disabled onclick="Visualizar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
							         onmouseover="se(this, 25);mostrarCursor(this);">
							        <img src="../../../images/imgOjos.gif" /><span>Visualizar</span>
						        </button>	
                            </td>
                        </tr>		                        
                        <tr>
                            <td>Nº de filas&nbsp;<label id='valproc' runat="server"></label>:</td>
                            <td id="cldLinProc" runat="server" style="text-align:right;">0</td>
                            <td></td>
                            <td colspan="3">Último fichero cargado:&nbsp;&nbsp;
                            <asp:Label id="lblFileName" Runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Nº de filas correctas:</td>
                            <td id="cldLinOK" runat="server" style="text-align:right;">0</td>
                            <td></td>
                            <td>Nº de filas erróneas:</td>
                            <td id="cldLinErr" runat="server" align="center">0</td>
                            <td style="text-align:right;">
                            	<fieldset style="width: 120px; height:35px; padding:5px;">
								    <legend title="Aplicable sólo entre diferentes criterios">Tipo de imputación</legend>                        
								    <asp:RadioButtonList ID="rdbImputacion" runat="server" Repeatdirection="horizontal" SkinID="rbl" onclick="setImputacion()">
								    <asp:ListItem Selected=true Value="D" Text="Diaria&nbsp;&nbsp;" />
								    <asp:ListItem Value="M" Text="Masiva" />
								    </asp:RadioButtonList>
							    </fieldset>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="6px" background="../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
              <tr>
                <td width="6px" height="6" background="../../../Images/Tabla/1.gif"></td>
                <td height="6px" background="../../../Images/Tabla/2.gif"></td>
                <td width="6px" height="6" background="../../../Images/Tabla/3.gif"></td>
              </tr>
            </table>
            </center>
            <br />
        </td>
    </tr>
    <tr>
	    <td align="left">
	        <div id="fls">
            <%=sCab1.Value%>	
            <%=sPie1.Value%>        
		    </div>
	    </td>
    </tr>
    <tr>
        <td>
            <fieldset style='width:950px; height:170px;'>
            <legend>Relación de filas erróneas en el proceso de grabación</legend>
            <table style='margin-top:5px; width:930px; height:17px'>
            <colgroup>
            <col style='width:70px;'/>
            <col style='width:860px;'/>
            </colgroup>
            <tr class='TBLINI'>
            <td style="padding-left:3px;">Nº Línea</td>
            <td>Error</td>
            </tr>
            </table>
            <div id='divErroresGrab' style='overflow: auto; overflow-x: hidden; width: 946px; height: 120px;' align='left' >
			    <div id='divGrab' style="background-image:url('../../../Images/imgFT16.gif');width: 930px;" runat="server">
				    <table id='tblErroresGrab' class='texto' style='width: 930px;'>
				    <colgroup>
				    <col style='width:70px'/>
				    <col style='width:860px'/>
				    </colgroup>
				    </table>
			    </div>
            </div>
            <table style='height:17px;width:930px' align='left'>
            <tr class='TBLFIN'><td>&nbsp;</td></tr>
            </table>
            </fieldset>       
        </td>
    </tr>
    
    
</table>
<input type="hidden" id="hdnErrores" value="<%=sErrores%>" />
<input type="hidden" runat="server" id="hdnNumfacts" value="0" />
<input type="hidden" runat="server" id="hdnIniciado" value="F" />
<input type="hidden" runat="server" id="sCab1" value="" />
<input type="hidden" runat="server" id="sCab2" value="" />
<input type="hidden" runat="server" id="sFLS" value="" />
<input type="hidden" runat="server" id="sPie1" value="" />
<input type="hidden" runat="server" id="sPie2" value="" />

<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

            switch (strBoton) {
                case "procesar":
                    {
                        bEnviar = false;
                        mostrarProcesando();
                        setTimeout("procesar();", 20);
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
