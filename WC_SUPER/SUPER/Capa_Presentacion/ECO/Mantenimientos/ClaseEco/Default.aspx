<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
	<script type="text/javascript">

	    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";

	</script>
    <center>
        <table id="Table2" cellpadding="3" style="width: 904px;text-align:left">
            <colgroup><col style="width:120px" /><col /></colgroup>
            <tr>
                <td>Grupo económico</td>
                <td><asp:DropDownList ID="cboGE" runat="server" style="width:250px; height:18px;" onchange="getSE(this.value)" AppendDataBoundItems="true">
                    <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Subgrupo económico</td>
                <td><asp:DropDownList ID="cboSE" runat="server" style="width:250px; height:18px;" onchange="getCE(this.value)" AppendDataBoundItems="true">
                    <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Concepto económico</td>
                <td><asp:DropDownList ID="cboCE" runat="server" style="width:250px; height:18px;" onchange="getClaseEco(this)" AppendDataBoundItems="true">
                    <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    <table style="width: 1000px;text-align:left;">
    <tr>
        <td>
		    <TABLE id="Table1" style="width: 980px; height: 17px; margin-top:5px;">
		        <colgroup>
		            <col style="width:375px"/>
		            <col style="width:60px"/>
		            <col style="width:65px"/>
		            <col style="width:100px"/>
		            <col style="width:60px"/>
		            <col style="width:60px"/>
		            <col style="width:45px"/>
		            <col style="width:50px"/>
		            <col style="width:50px"/>
		            <col style="width:50px"/>
		            <col style="width:60px"/>
		        </colgroup>
			    <TR class="TBLINI">
				    <td style="padding-left:20px;">Clase económica</TD>
				    <td>Activo</TD>
				    <td title="Presentable únicamente a los administradores">ADM</TD>
				    <td title="Necesidad de indicación: <%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %> - Proveedor">Necesidad</TD>
				    <td title="Dispara réplica">Réplica</TD>
				    <td title="Se tiene en cuenta en el decalaje y borrado de órdenes de facturación.">DBOF</TD>
				    <td title="Se tiene en cuenta para determinar el saldo de clientes para el cálculo de los gastos financieros.">CGF</TD>
				    <td title="Visible en los proyectos contratantes">VPC</TD>
				    <td title="Visible en los proyectos replicados sin gestión">VPSG</TD>
				    <td title="Visible en los proyectos replicados con gestión">VPCG</TD>
				    <td>Clonable</TD>
			    </TR>
		    </TABLE>
		    <DIV id="divCatalogo" style="overflow: auto; width: 996px; height:420px" runat="server">
		        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:980px">
		            <%=strTablaHTML%>
		        </div>
		    </DIV>
		    <TABLE id="Table4" style="width: 980px; height: 17px">
			    <TR class="TBLFIN">
				    <TD></TD>
			    </TR>
		    </TABLE>
            <center>
                <table style="width:250px;margin-top:5px;">
                <tr>
                    <td width="45%">
                        <button id="btnNuevo" type="button" onclick="nuevo()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                         onmouseover="se(this, 25);mostrarCursor(this);">
	                        <img src="../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
                        </button>	
                    </td>
                    <td width="10%"></td>
                    <td width="45%">
                        <button id="btnEliminar" type="button" onclick="eliminar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                         onmouseover="se(this, 25);mostrarCursor(this);">
	                        <img src="../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
                        </button>	
                    </td>
                </tr>
                </table>
            </center> 
        </td>
    </tr>
    </table>
    </center>
   <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "nuevo": 
				{
                    bEnviar = false;
                    nuevo();
					break;
				}
				case "eliminar": 
				{
                    bEnviar = false;
                    eliminar();
					break;
				}
				case "grabar": 
				{
                    bEnviar = false;
                    setTimeout("grabar();", 20);
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

