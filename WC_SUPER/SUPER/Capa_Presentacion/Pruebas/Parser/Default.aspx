<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_Alert_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script language=javascript>

function mostrarErrores(){
    try{
	    return true;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los errores.", e.message);
    }
}

//var strHTML = "<tr><td>a2</td><td>b2</td><td>c2</td></tr><tr><td>a3</td><td>b3</td><td>c3</td></tr><tr><td>a4</td><td>b4</td><td>c4</td></tr>";
var strHTML = "<p>AAAAAAAAAAAAAAAAAAAAAAA</p><p>BBBBBBBBBBBBBBBBBBBB</p><p style='color:red'>CCCCCCCCCCCCCCCC</p>";
function init(){
alert("antes");
    HTMLtoDOM(strHTML, document);
alert("despues");
}
</script>
<table id="tblDatos" width="300px" border=2>
<colgroup>
    <col style="width:100px" />
    <col style="width:100px" />
    <col style="width:100px" />
</colgroup>
<tr>
    <td>a1</td>
    <td>b1</td>
    <td>c2</td>
</tr>
</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

