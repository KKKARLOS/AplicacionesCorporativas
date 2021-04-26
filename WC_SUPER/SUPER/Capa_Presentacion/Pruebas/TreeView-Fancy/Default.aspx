<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="TreeViewFancy" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<style type="text/css"> 
  #draggableSample, #droppableSample { 
    height:100px; 
    padding:0.5em; 
    width:150px; 
    border:1px solid #AAAAAA; 
  } 
  #draggableSample { 
    background-color: silver; 
    color:#222222; 
  } 
  #droppableSample { 
    background-color: maroon; 
    color: white; 
  } 
  #droppableSample.drophover { 
    border: 1px solid green; 
  } 
</style> 

<script type="text/javascript">
</script>
<br />
	<div>
		<label for="skinswitcher">Skin:</label> <select id="skinswitcher"></select>
	</div>	
  <!-- Definition of context menu --> 
  <ul id="myMenu" class="contextMenu ui-helper-hidden"> 
    <li class="edit"><a href="#edit">Edit</a></li> 
    <li class="cut"><a href="#cut">Cut</a></li> 
    <li class="copy"><a href="#copy">Copy</a></li> 
    <li class="paste"><a href="#paste">Paste</a></li> 
    <li class="ui-state-disabled"><a href="#delete">Delete</a></li> 
    <li>---</li> 
    <li class="quit"><a href="#quit">Quit</a></li> 
    <li><a href="#save"><span class="ui-icon ui-icon-disk"></span>Save</a></li> 
  </ul> 


	
	<div id="tree" style="width: 500px; height:460px;">
		<ul id="treeData" style="display: none;">
        <%=sTreeView%>   
		</ul>
	</div>
	
<table id="tblBotonera" style="width:780px; height:30px; margin-top:30px" border="0">
<colgroup>
    <col style="width: 130px;" />
    <col style="width: 130px;" />
    <col style="width: 130px;" />
    <col style="width: 130px;" />
    <col style="width: 130px;" />
    <col style="width: 130px;" />    
</colgroup>
    <tr style="vertical-align: top;">
    <td>
		<button id="btnSelect" type="button" class="btnH25W110" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgProcesar.gif" /><span>Nodo activo</span>
		</button>	        
    </td>    	
	<td>
		<button id="btnRemove" type="button" class="btnH25W110" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgProcesar.gif" /><span>Borrar activo</span>
		</button>	 	
	</td>
	<td>
		<button id="btnCreateNode" type="button" class="btnH25W110" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgProcesar.gif" /><span>Crear nodo1</span>
		</button>	 	
	</td>
	<td>    
		<button id="btnCreateNode2" type="button" class="btnH25W110" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgProcesar.gif" /><span>Crear nodo1</span>
		</button>		    
    </td> 
    <td></td>
    <td></td> 
    </tr>     
</table>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">

</asp:Content>


