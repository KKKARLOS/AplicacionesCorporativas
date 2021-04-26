<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="USA" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
<script type="text/javascript">
<!--
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
-->
</script>
<table style="width:980px; text-align:left; margin-left:10px;">
    <colgroup>
        <col style="width:490px;" />
        <col style="width:490px;" />
    </colgroup>
    <tr>
        <td>
            <fieldset id="fldUserOrigen" style="width:465px; height:50px;">
            <legend>Origen</legend>
                <table style="width:460px;">
                    <colgroup>
                        <col style="width:70px;" />
                        <col style="width:390px;" />
                    </colgroup>
                    <tr>
                        <td></td>
                        <td>
                            <label id="Label1" style="width:25px" title="Soporte titular de soporte administrativo de proyecto económico">SAT</label>
                            <input type="checkbox" id="chkSAT" class="check" onclick="getProyectosAux()" title="Usuario titular de soporte administrativo de proyecto económico"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <label id="Label2" style="width:25px" title="Soporte alternativo de soporte administrativo de proyecto económico">SAA</label>
                            <input type="checkbox" id="chkSAA" class="check" onclick="getProyectosAux()" title="Usuario alternativo de soporte administrativo de proyecto económico"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblOrigen" style="width:70px">Profesional</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOrigen" style="width:380px;" Text="" runat="server" readonly="true" />
                        </td>
                    </tr>
                </table>   
            </fieldset>
        </td>
        <td>
            <fieldset id="fldUserDestino" style="width:463px; height:50px;">
            <legend>Destino</legend>
                <label id="lblDestino" style="width:70px; margin-top:15px; margin-left:3px;">Profesional</label>
                <asp:TextBox ID="txtDestino" style="width:380px;" Text="" runat="server" readonly="true" />
            </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <img id="imgMarcar" src="../../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcar(1)" />
            <img id="imgDesmarcar" src="../../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcar(0)" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
		    <table id="tblTitulo" style="width: 960px; height: 17px">
		        <colgroup>
		            <col style="width:20px" />
		            <col style="width:20px" />
		            <col style="width:20px" />
		            <col style="width:20px" />
				    <col style="width:300px;" />
				    <col style="width:290px;" />
				    <col style="width:290px;" />
		        </colgroup>
			    <tr class="TBLINI">
			        <td></td>
			        <td></td>
			        <td></td>
			        <td></td>
				    <td>
				        <img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					        <map name="img1">
					            <area onclick="ot('tblDatos', 4, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					            <area onclick="ot('tblDatos', 4, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				            </map>&nbsp;Proyecto&nbsp;
				         <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa1')"
						    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
						 <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa1' ,event)"
						    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
				    </td>
				    <td>Soporte administrativo titular</td>
				    <td>Soporte administrativo alternativo</td>
			    </tr>
		    </table>
            <div id="divCatalogo" style="overflow: auto; overflow-x:hidden; width: 976px; height:400px;" onscroll="scrollTablaProy()">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:960px">
                    <table id='tblDatos'></table>
                </div>
            </div>
            <table style="width: 960px; height: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<input type="hidden" id="hdnIdUserOrigen" value="" runat="server"/>
<input type="hidden" id="hdnIdUserDestino" value="" runat="server"/>
<input type="hidden" id="hdnLectura" value="S" runat="server"/>
</asp:Content>
<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            switch (strBoton) {			
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
		if (bEnviar) theform.submit();
	}
-->
</script>
</asp:Content>

