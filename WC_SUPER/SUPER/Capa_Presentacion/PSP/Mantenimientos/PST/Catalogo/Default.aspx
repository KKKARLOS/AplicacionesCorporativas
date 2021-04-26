<%@ Page  Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Mantenimientos_PST_Catalogo_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";  
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";  
    //Para el comportamiento de los calendarios
    var btnCal = "<%=Session["BTN_FECHA"].ToString() %>";
</script>
<center>
<table style="width: 990px;text-align:left;">
    <tr>
        <td>
            <label id="lblNodo" style="width:399px;height:17px" runat="server" class="enlace" onclick="getNodo();">Nodo</label>
            <asp:TextBox ID="hdnIdNodo" runat="server" style="width:1px;visibility:hidden" Text="" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:DropDownList id="cboCR" runat="server" Width="400px" onChange="setCombo()" AppendDataBoundItems=true></asp:DropDownList>
            <asp:TextBox ID="txtDesNodo" style="width:400px;" Text="" readonly="true" runat="server" />
        &nbsp;&nbsp;&nbsp;
        <asp:CheckBox ID="chkAct" runat="server" Text="Mostrar inactivas " onclick="mostrarActivas();" Width="300px" style="cursor:pointer" Checked=false />
    </td>
</tr>
    <tr>
        <td>
            <table id="tblTitulo" style="width:970px; height: 17px; margin-top:5px;">
                <colgroup>
                    <col style="width:120px"/>
                    <col style="width:255px"/>
                    <col style="width:50px"/>
                    <col style="width:80px"/>
                    <col style="width:60px"/>
                    <col style="width:80px"/>
                    <col style="width:40px"/>
                    <col style="width:285px"/>
                </colgroup>
	            <TR class="TBLINI">
			    <td width="120px">&nbsp;
                    <IMG style="CURSOR: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#imgCod" border="0"> 
			        <MAP name="imgCod">
				        <AREA onclick="ordenarTabla(3,0)" shape="RECT" coords="0,0,6,5">
				        <AREA onclick="ordenarTabla(3,1)" shape="RECT" coords="0,6,6,11">
			        </MAP>					
			        &nbsp;Código&nbsp;
				    <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
					    height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
			        <IMG style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)"
					    height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
			    </td>
			    <td width="255px"><IMG style="CURSOR: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#imgDes" border="0"> 
			        <MAP name="imgDes">
				        <AREA onclick="ordenarTabla(4,0)" shape="RECT" coords="0,0,6,5">
				        <AREA onclick="ordenarTabla(4,1)" shape="RECT" coords="0,6,6,11">
			        </MAP>					
			        &nbsp;Denominación&nbsp;
				    <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',2,'divCatalogo','imgLupa2')"
					    height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
			        <IMG style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',2,'divCatalogo','imgLupa2',event)"
					    height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
		        </td>
                
                <td width="50px" align="right">
                    &nbsp;Horas&nbsp;
                    <!--<asp:TextBox ID="txtHoras" style="width:40px" runat="server" SkinID="numero"/>-->
                </td>
                <td width="80px">
                    &nbsp;Presupuesto&nbsp;
                    <!--<asp:TextBox ID="txtPresupuesto" runat="server" style="width:70px" SkinID="numero" onFocus="fn(this,7,2);"/>-->
                </td>
                <td width="60px">
                    &nbsp;Moneda&nbsp;
                    <!--<asp:TextBox ID="txtMoneda" runat="server" style="width:40px"/>-->
                </td>
			    <td width="80px" title="Fecha de referencia"><IMG style="CURSOR: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#imgFec" border="0"> 
			        <MAP name="imgFec">
				        <AREA onclick="ordenarTabla(9,0)" shape="RECT" coords="0,0,6,5">
				        <AREA onclick="ordenarTabla(9,1)" shape="RECT" coords="0,6,6,11">
			        </MAP>
			        &nbsp;FR&nbsp;
				    <IMG id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa4')"
					    height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
			        <IMG style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',6,'divCatalogo','imgLupa4',event)"
					    height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
                </td>
			    <td width="40px" title="OTC activa">Activa</td>
			    <td width="285px"><IMG style="CURSOR: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#imgCli" border="0"> 
			        <MAP name="imgCli">
				        <AREA onclick="ordenarTabla(6,0)" shape="RECT" coords="0,0,6,5">
				        <AREA onclick="ordenarTabla(6,1)" shape="RECT" coords="0,6,6,11">
			        </MAP>					
			        &nbsp;Cliente&nbsp;
				    <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',5,'divCatalogo','imgLupa3')"
					    height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
			        <IMG style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',8,'divCatalogo','imgLupa3', event)"
					    height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
		        </td>
	            </TR>
            </table>
            <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 986px; height:440px" onscroll="scrollTablaAE();">
                <div style='background-image:url(../../../../../Images/imgFT20.gif); width:970px'>
                    <%=strTablaHtml %>
                </div>
            </div>
            <table style="width: 970px; height: 17px;">
	            <tr class="TBLFIN"><td></td></tr>
            </table>
            
        </td>
    </tr>
</table>
<table style="width:250px; margin-top:15px">
<tr>
    <td>
        <button id="btnAnadir" type="button" onclick="nuevo()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
             onmouseover="se(this, 25);mostrarCursor(this);">
            <img src="../../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
        </button>	
    </td>
    <td>
        <button id="btnEliminar" type="button" onclick="eliminar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
             onmouseover="se(this, 25);mostrarCursor(this);">
            <img src="../../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
        </button>	
    </td>
</tr>
</table>
</center>

<asp:textbox id="hdnMensajeError" runat="server" style="visibility:hidden"></asp:textbox>
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
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
//				case "nuevo": 
//				{
//                    bEnviar = false;
//                    nuevo();
//					break;
//				}
//				case "eliminar": 
//				{
//                    bEnviar = false;
//					if (confirm("¿Estás conforme?")){
//                        eliminar();
//                    }
//					break;
//				}
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("OTC.pdf");
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

