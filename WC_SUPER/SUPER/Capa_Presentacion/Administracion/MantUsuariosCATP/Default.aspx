<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
	<script type="text/javascript">
	<!--
        var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";		
	    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";   
	-->
	</script>
    <style type="text/css">
        #tblDatos tr { height:20px; }
        #tblDatos td {
	        border-collapse: separate;
            border-spacing: 0px;
	        padding: 0px 2px 0px 2px;
        }     
        .colTablaD
        {
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #569BBD;
            border-top-style: solid;
            border-top-width: 1px;
            border-top-color: #569BBD;
            background-color: #E4EFF3;
        }        
    </style>
<center>
       <table style="width:846px;text-align:left">
			<colgroup>
					<col style="width: 10px" />
					<col style="width: 15px;"/>
					<col style="width: 435px" />
			        <col style="width: 35px" />
			        <col style="width: 35px" />
			        <col style="width: 35px" />
			        <col style="width: 35px" />
			        <col style="width: 35px" />
			        <col style="width: 35px" />
			        <col style="width: 35px" />
			        <col style="width: 45px" />
	                <col style="width: 45px" />	
	                <col style="width: 25px" />				
	                <col style="width: 16px" />
			</colgroup>
            <tr>
            <td colspan="3"><img id="imgUsuariosPlus2" border="0" onclick="getProfAsig()" src="../../../Images/imgUsuariosPlus2.gif" style="cursor:pointer" title="A�adir profesionales" onmouseover="mostrarCursor(this)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <img id="imgUsuariosMinus" border="0" onclick="eliminarProfAsig()" src="../../../Images/imgUsuariosMinus.gif" style="cursor:pointer" title="Eliminaci�n de los profesionales seleccionados" onmouseover="mostrarCursor(this)" /></td>
            <td colspan="8" valign="middle" class="colTabla" style="font-size:13px;text-align:center">&nbsp;Relaci�n de horas contratadas</td>
		    <td colspan="2" class="colTablaD" style="padding-right:5px; vertical-align:middle; text-align:right;">
	            <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />
	            &nbsp;
	            <img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selecci�n a todas las filas" onclick="DesmarcarTodo('tblDatos')" />  									        
		    </td>
		    <td>  
		    </td>
            </tr>
            <tr>
                <td colspan="14" style="padding-left:0px;">
                    <table id="tblAsignados" style="width: 830px; height: 17px;">
                    <colgroup>
                            <col style="width: 10px" />
                            <col style="width: 15px;"/>
					        <col style="width: 435px" />
					        <col style="width: 35px" />
					        <col style="width: 35px" />
					        <col style="width: 35px" />
					        <col style="width: 35px" />
					        <col style="width: 35px" />
					        <col style="width: 35px" />
					        <col style="width: 35px" />
					        <col style="width: 45px" />
			                <col style="width: 45px" />	
			                <col style="width: 35px" />			        
                    </colgroup>
                    <tr class="TBLINI">
                        <td>&nbsp;</td>
                        <td align="center">&nbsp;</td>
                        <td style="padding-left: 2px;"><IMG style="cursor: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgEmp" border="0">
					        <MAP name="imgEmp">
					            <AREA onclick="ot('tblDatos', 2, 0, '', 'scrollTablaProf()')" shape="RECT" coords="0,0,6,5">
					            <AREA onclick="ot('tblDatos', 2, 1, '', 'scrollTablaProf()')" shape="RECT" coords="0,6,6,11">
				            </MAP>Profesional
				            <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',2,'divProfAsig','imgLupa1')" height="11" src="../../../Images/imgLupaMas.gif" tipolupa="2" width="20">
							<img style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',2,'divProfAsig','imgLupa1',event)" height="11" src="../../../Images/imgLupa.gif" tipolupa="1" width="20">				            
				        </td>

                        <td style="text-align:center"><label title="Lunes">L</label></td>
                        <td style="text-align:center"><label title="Martes">M</label></td>
						<td style="text-align:center"><label title="Mi�rcoles">X</label></td>
						<td style="text-align:center"><label title="Jueves">J</label></td>
						<td style="text-align:center"><label title="Viernes">V</label></td>
						<td style="text-align:center"><label title="S�bado">S</label></td>
						<td style="text-align:center"><label title="Domingo">D</label></td>
						<td style="text-align:center"><label title="Semana">SEM</label></td>
						<td style="text-align:center"><label title="Mes">MES</label></td>
						<td style="text-align:center" title="Indica si el d�a 1 de cada mes se env�a un correo al profesional avisando de la disponibilidad del resumen mensual">Aviso</td>
                    </tr>
                    </table>
                    <div id="divProfAsig" style="overflow: auto; overflow-x: hidden; width: 846px; height:450px;" runat="server" onscroll="scrollTablaProf()">
                        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:830px">
						<%=strTablaUsuariosCATP%>
                        </div>
                    </div>
                    <table style="width: 830px; height: 17px; margin-bottom: 3px;">
                        <tr class="TBLFIN">
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
       </table>
</center>  
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
</asp:Content>
<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	        //alert("strBoton: "+ strBoton);
			switch (strBoton){			
				case "grabar": 
				{
					bEnviar = false;
					grabar();
					break;
				}
			}
		}

		var theform = document.forms[0];
		theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		theform.__EVENTARGUMENT.value = eventArgument;
		if (bEnviar) {
		    theform.submit();
		}
	}
-->
</script>
</asp:Content>

