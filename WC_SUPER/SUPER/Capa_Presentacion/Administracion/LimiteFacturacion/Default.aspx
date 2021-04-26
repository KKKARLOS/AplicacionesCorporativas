<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Administradores" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
<center>
<br /><br />
<fieldset style="width: 140px; height:50px;">
    <legend>Año</legend>
    <table align="center" style="margin-top:7px;">
    <tr>
        <td>
            <img title="Año anterior" onclick="setAnno('A')" src="../../../Images/btnAntRegOff.gif" style="cursor: pointer" />
            <asp:TextBox id="txtAnno" style="width:32px; text-align:center;vertical-align:top" readonly="true" runat="server" Text=""></asp:TextBox>
            <img title="Siguiente año" onclick="setAnno('S')" src="../../../Images/btnSigRegOff.gif" style="cursor: pointer" />
        </td>
    </tr>
    </table>
</fieldset>
<br /><br />
<label class="label">Fecha límite para permitir tramitar órdenes de facturación<br />cuya fecha factura corresponda al mes anterior al actual.</label>
<br /><br />
<table id="tblTitulo" style="width: 240px; height: 17px; margin-top:20px">
<colgroup><col style='width: 100px' /><col style='width: 140px' /></colgroup>
    <tr class="TBLINI">
        <td style="padding-left:5px">Facturas de</td>
        <td>Fecha límite</td>  
    </tr>
</table>

<div style="background-image:url(<%=Session["strServer"] %>Images/imgFT26.gif); width:240px">
<table id="tblDatos" align="center" class="texto" width="240px">
<colgroup><col style='width: 100px' /><col style='width: 65px' /><col style='width:75px' /></colgroup>
		<TR style="height:26px">
			<td>
                <asp:Label id="lblFechaMes01" runat="server" Width="40px">&nbsp;Enero</asp:Label>
            </td>
            <td>
                <asp:textbox id="txtFechaMes01" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="aG(this)" runat="server" goma="0"></asp:textbox>
            </td>
            <td>                
                <asp:dropdownlist id="cboHora01" runat="server" CssClass="combo" style="width:60px; vertical-align:middle;" onchange="aG(this)">
                </asp:dropdownlist>
			</td>
		</TR>	
		<TR style="height:26px">
			<td>
                <asp:Label id="lblFechaMes02" runat="server" Width="40px">&nbsp;Febrero</asp:Label>
            </td>
            <td>            
                <asp:textbox id="txtFechaMes02" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="aG(this)" runat="server" goma="0"></asp:textbox>
            </td>
            <td>                   
                <asp:dropdownlist id="cboHora02" runat="server" CssClass="combo" style="width:60px; vertical-align:middle;" onchange="aG(this)">
	            </asp:dropdownlist>
			</td>
		</TR>	
		<TR style="height:26px">
			<td>
                <asp:Label id="lblFechaMes03" runat="server" Width="40px">&nbsp;Marzo</asp:Label>
            </td>
            <td>
                <asp:textbox id="txtFechaMes03" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="aG(this)" runat="server" goma="0"></asp:textbox>
            </td>
            <td>                   
                <asp:dropdownlist id="cboHora03" runat="server" CssClass="combo" style="width:60px; vertical-align:middle;" onchange="aG(this)">
                </asp:dropdownlist>
			</td>
		</TR>		
		<TR style="height:26px">
			<td>
                <asp:Label id="lblFechaMes04" runat="server" Width="40px">&nbsp;Abril</asp:Label>
            </td>
            <td>           
                <asp:textbox id="txtFechaMes04" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="aG(this)" runat="server" goma="0"></asp:textbox>
            </td>
            <td>                   
                <asp:dropdownlist id="cboHora04" runat="server" CssClass="combo" style="width:60px; vertical-align:middle;" onchange="aG(this)">
                </asp:dropdownlist>
			</td>
		</TR>		
		<TR style="height:26px">
			<td>
                <asp:Label id="lblFechaMes05" runat="server" Width="40px">&nbsp;Mayo</asp:Label>
            </td>
            <td>                  
                <asp:textbox id="txtFechaMes05" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="aG(this)" runat="server" goma="0"></asp:textbox>
            </td>
            <td>                   
                <asp:dropdownlist id="cboHora05" runat="server" CssClass="combo" style="width:60px; vertical-align:middle;" onchange="aG(this)">
                </asp:dropdownlist>
			</td>
		</TR>		
		<TR style="height:26px">
			<td>
                <asp:Label id="lblFechaMes06" runat="server" Width="40px">&nbsp;Junio</asp:Label>
            </td>
            <td>                    
                <asp:textbox id="txtFechaMes06" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="aG(this)" runat="server" goma="0"></asp:textbox>
            </td>
            <td>                    
                <asp:dropdownlist id="cboHora06" runat="server" CssClass="combo" style="width:60px; vertical-align:middle;" onchange="aG(this)">
                </asp:dropdownlist>
			</td>
		</TR>		
		<TR style="height:26px">
			<td>
                <asp:Label id="lblFechaMes07" runat="server" Width="40px">&nbsp;Julio</asp:Label>
            </td>
            <td>                 
                <asp:textbox id="txtFechaMes07" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="aG(this)" runat="server" goma="0"></asp:textbox>
            </td>
            <td>                   
                <asp:dropdownlist id="cboHora07" runat="server" CssClass="combo" style="width:60px; vertical-align:middle;" onchange="aG(this)">
                </asp:dropdownlist>
			</td>
		</TR>		
		<TR style="height:26px">
			<td>
                <asp:Label id="lblFechaMes08" runat="server" Width="40px">&nbsp;Agosto</asp:Label>
            </td>
            <td>   
                <asp:textbox id="txtFechaMes08" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="aG(this)" runat="server" goma="0"></asp:textbox>
            </td>
            <td>                   
                <asp:dropdownlist id="cboHora08" runat="server" CssClass="combo" style="width:60px; vertical-align:middle;" onchange="aG(this)">
                </asp:dropdownlist>
			</td>
		</TR>		
		<TR style="height:26px">
			<td>
                <asp:Label id="lblFechaMes09" runat="server" Width="40px">&nbsp;Septiembre</asp:Label>
            </td>
            <td>               
                <asp:textbox id="txtFechaMes09" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="aG(this)" runat="server" goma="0"></asp:textbox>
            </td>
            <td>                   
                <asp:dropdownlist id="cboHora09" runat="server" CssClass="combo" style="width:60px; vertical-align:middle;" onchange="aG(this)">
                </asp:dropdownlist>
			</td>
		</TR>				
		<TR style="height:26px">
			<td>
                <asp:Label id="lblFechaMes10" runat="server" Width="40px">&nbsp;Octubre</asp:Label>
            </td>
            <td>               
                <asp:textbox id="txtFechaMes10" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="aG(this)" runat="server" goma="0"></asp:textbox>
            </td>
            <td>                   
                <asp:dropdownlist id="cboHora10" runat="server" CssClass="combo" style="width:60px; vertical-align:middle;" onchange="aG(this)">
                </asp:dropdownlist>
			</td>
		</TR>				
		<TR style="height:26px">
			<td>
                <asp:Label id="lblFechaMes11" runat="server" Width="40px">&nbsp;Noviembre</asp:Label>
            </td>
            <td>                    
                <asp:textbox id="txtFechaMes11" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="aG(this)" runat="server" goma="0"></asp:textbox>
            </td>
            <td>                   
                <asp:dropdownlist id="cboHora11" runat="server" CssClass="combo" style="width:60px; vertical-align:middle;" onchange="aG(this)">
                </asp:dropdownlist>
			</td>
		</TR>				
		<TR style="height:26px">
			<td>            
                <asp:Label id="lblFechaMes12" runat="server" Width="40px">&nbsp;Diciembre</asp:Label>
            </td>
            <td>                   
                <asp:textbox id="txtFechaMes12" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="aG(this)" runat="server" goma="0"></asp:textbox>
            </td>
            <td>                    
                <asp:dropdownlist id="cboHora12" runat="server" CssClass="combo" style="width:60px; vertical-align:middle;" onchange="aG(this)">
                </asp:dropdownlist>
			</td>
		</TR>											
    </table>
    </div>
    <table style="WIDTH: 240px; HEIGHT: 17px; margin-bottom:0px;"  cellpadding="0" cellSpacing="0" border="0">
        <TR class="TBLFIN">
            <TD></TD>
        </TR>
    </table>
    </center>

</asp:Content>
<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
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
		if (bEnviar) theform.submit();

	}
</script>
</asp:Content>

