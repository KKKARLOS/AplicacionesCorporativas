<%@ Page  Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Preventa_ParametrizacionPT_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";  
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";  
</script>
<center>
<table style="width:990px;text-align:left;" border="0">
    <colgroup>
        <col style="width:400px"/>
        <col style="width:590px"/>
    </colgroup>
    <tr>
        <td>
            <fieldset style="width:380px; height:90px;">
                <legend>Filtrado</legend>
                <label id="lblDenOC" style="width:58px;height:17px;" runat="server" class="texto" title="Organización comercial">Org. Com.</label>
                <asp:DropDownList id="cboCR" runat="server" Width="315px" onChange="getCatalogo()" AppendDataBoundItems="true"></asp:DropDownList>
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkAct" runat="server" Text="Mostrar Org.C. inactivas" onclick="mostrarActivas();" Width="180px" style="cursor:pointer" Checked="false" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkProf" runat="server" Text="Mostrar profesionales de baja" onclick="mostrarActivas2();" Width="200px" style="cursor:pointer" Checked="false" />
                        </td>
                    </tr>
                </table>
                <br /><br />
                <label id="Label1" style="width:58px;height:17px;" runat="server" class="enlace" title="Comerciales preventa" onclick="getProfesional()">Comercial</label>
                <asp:TextBox ID="txtProfesional" Width="295px" runat="server" readonly="true"></asp:TextBox>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="limpiarProf();" style="cursor:pointer; vertical-align:middle;" />
            </fieldset>
        </td>
        <td>
            <fieldset style="width:560px; height:90px;">
                <legend>Destino tarea preventa</legend>
                <br />
                <table style="width:560px;" cellpadding="2">
                    <tr>
                        <td>
                            <label id="lblProy" class="enlace" style="width:100px;height:17px" onclick="obtenerProyectos()">Proy. económico</label>
                            <asp:TextBox ID="txtNumPE" runat="server" SkinID="Numero" style="width:50px"  maxlength="8" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarPE();}else{vtn2(event);limpiar();}"/>
                            <asp:TextBox ID="txtPE" runat="server" style="width:370px" readonly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblPT" class="enlace" style="width:100px;height:17px" onclick="obtenerPTs()">Proy. técnico</label>
                            <asp:TextBox ID="txtNumPT" runat="server" SkinID="Numero" style="width:50px" maxlength="8" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarPT();}else{vtn2(event);limpiarPT();}"/>
                            <asp:TextBox ID="txtPT" runat="server" style="width:370px" readonly="true" />
                            <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="limpiarPT();" style="cursor:pointer; vertical-align:middle;" />
                        </td>
                    </tr>
                </table>               
            </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table id="tblTitulo" style="width:990px; height: 17px; margin-top:5px;" border="0">
                <colgroup>
                    <col style="width:20px"/>
                    <col style="width:300px"/>
                    <col style="width:290px"/>
                    <col style="width:250px"/>
                    <col style="width:70px"/>
                    <col style="width:60px"/>
                </colgroup>
                <tr>
                    <td colspan="4"></td>
                    <td  style="padding-left:12px;">
                        <img id="imgMarcar" src="../../../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcar(1)" />
                        <img id="imgDesmarcar" src="../../../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcar(0)" />
                    </td>
                </tr>
	            <tr class="TBLINI">
                    <td></td>
			        <td>
                        <IMG style="CURSOR: pointer" height="11px" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					    <MAP name="img1">
					        <AREA onclick="ot('tblDatos', 1, 0, '', 'scrollTablaAE()')" shape="RECT" coords="0,0,6,5">
					        <AREA onclick="ot('tblDatos', 1, 1, '', 'scrollTablaAE()')" shape="RECT" coords="0,6,6,11">
				        </MAP>Organización Comercial&nbsp;
				        <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
					        height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
			            <img style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)"
					        height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
                        
			        </td>
			        <td>
                        <IMG style="CURSOR: pointer" height="11px" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
					    <MAP name="img2">
					        <AREA onclick="ot('tblDatos', 2, 0, '', 'scrollTablaAE')" shape="RECT" coords="0,0,6,5">
					        <AREA onclick="ot('tblDatos', 2, 1, '', 'scrollTablaAE')" shape="RECT" coords="0,6,6,11">
				        </MAP>Profesional&nbsp;			            
				        <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',2,'divCatalogo','imgLupa2')"
					        height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
			            <IMG style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',2,'divCatalogo','imgLupa2',event)"
					        height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
		            </td>
			        <td>
			            Proyecto Técnico&nbsp;
				        <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa4')"
					        height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
			            <IMG style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa3',event)"
					        height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
                    </td>
			        <td title="Marcado de filas para asignación/desasignación de Proyecto Técnico">Operación</td>
                    <td style="margin-right:15px">Bloquear</td>
	            </tr>
            </table>
            <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width:986px; height:320px" onscroll="scrollTablaAE();">
                <div style='background-image:url(../../../../../Images/imgFT20.gif); width:990px'>
                    <%=strTablaHtml %>
                </div>
            </div>
            <table style="width: 990px; height: 17px;">
	            <tr class="TBLFIN"><td></td></tr>
            </table>
            
        </td>
    </tr>
</table>
<table style="width:300px;margin-top:15px">
    <tr>
        <td>
            <button id="btnAnadir" type="button" onclick="asignarPT()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                    onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../../images/botones/imgAnadir.gif" /><span title="Asigna el Proyecto Técnico seleccionado, a las filas marcadas en operación">Asignar</span>
            </button>	
        </td>
        <td>
            <button id="btnEliminar" type="button" onclick="eliminar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                    onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../../images/botones/imgEliminar.gif" /><span title="Elimina el Proyecto Técnico seleccionado, de las filas marcadas en operación">Eliminar</span>
            </button>	
        </td>         
        <td>
            <button id="btnObtenerParam" type="button" onclick="obtenerParam()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../../images/botones/imgObtener.gif" /><span title="Obtener todas las parametrizaciones">Obtener</span>
            </button>	
        </td>
    </tr>
</table>
</center>
<input type="hidden" id="hdnT305IdProy" runat="server" value="" />
<input type="hidden" id="hdnIDPT" runat="server" value="" />    
<input type="hidden" id="hdnIdProf" runat="server" value="" />    
<input type="hidden" id="hdnOCtodas" runat="server" value="" />    
<input type="hidden" id="hdnOCactivas" runat="server" value="" />    
<asp:textbox id="hdnMensajeError" runat="server" style="visibility:hidden"></asp:textbox>
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            switch (strBoton){
                case "grabar": 
                    {
                        bEnviar = false;
                        setTimeout("grabar();", 20);
                        break;
                    }
                //case "guia": 
                //    {
                //        bEnviar = false;
                //        mostrarGuia("OTC.pdf");
                //        break;
                //    }
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

