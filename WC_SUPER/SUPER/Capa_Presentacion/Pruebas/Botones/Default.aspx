<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"  %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<br /><br />
	<table width="800px" cellSpacing="0" cellPadding="0" border="0">
	<tr>
	    <td width="200px">
	    <table height="20" cellSpacing="0" cellPadding="0" border="0">
		    <tr style="CURSOR: pointer" onclick="alert()">
			    <td width="7"><IMG src="../../../images/imgBtnIzda.gif" width="7"></td>
			    <td vAlign="middle" align="center" width="20" background="../../../images/bckBoton.gif"><A hideFocus href="#"><IMG src="../../../images/imgAceptar.gif" align="absMiddle" border="0"></A></td>
			    <td class="txtBot" width="40" background="../../../images/bckBoton.gif"><A class="txtBot" hideFocus href="#">&nbsp;&nbsp;Aceptar</A></td>
			    <td width="7"><IMG src="../../../images/imgBtnDer.gif" width="7"></td>
		    </tr>
	    </table>
	    </td>
	    <td width="200px">
        <button id="btnAceptar" type="button" onclick="alert()"  onmouseover="mcur(this)" style="width:85px" title="Aceptar">
            <span><img src="../../../images/imgAceptar.gif" />&nbsp;&nbsp;Aceptar</span>
        </button>    
	    </td>
	    <td width="200px">

        <button id="Button2" type="button" onclick="return;alert()" onmouseover="mc(this)" style="width:50px">
            <span><img src="../../../images/imgSI.gif" />&nbsp;&nbsp;Sí</span>
        </button>    
	    </td>
	    <td width="200px">
        <button id="Button1" type="button" onclick="return;alert()" onmouseover="mc(this)" style="width:50px">
            <span><img src="../../../images/imgNO.gif" />&nbsp;&nbsp;No</span>
        </button>    
	    </td>
	</tr>
	<tr>
	    <td width="200px"></td>
	    <td width="200px" onclick="setOp($I('btnAceptar'), 100);" style="cursor:pointer"><br />Habilitar</td>
	    <td width="200px"></td>
	    <td width="200px"></td>
	</tr>
	<tr>
	    <td width="200px"></td>
	    <td onclick="setOp($I('btnAceptar'), 50);" style="cursor: pointer" width="200px">Deshabilitar</td>
	    <td width="200px"></td>
	    <td width="200px"></td>
	</tr>
	<tr>
	    <td width="200px"></td>
	    <td width="200px"></td>
	    <td width="200px"></td>
	    <td width="200px"></td>
	</tr>
	</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

