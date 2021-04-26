<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_DDImages_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
	<style type="text/css">
	</style>
	
<div id="dragDropContainer">
	<div id="listOfItems">
		<div>
			<p>Figuras disponibles</p>		
		<ul id="allItems">
			<li id="node1"><img src="../../../Images/imgColaborador.gif" title="Colaborador" /> Colaborador</li>
			<li id="node2"><img src="../../../Images/imgJefeProyecto.gif" title="Jefe" /> Jefe</li>
			<li id="node3"><img src="../../../Images/imgSubjefeProyecto.gif" title="Responsable técnico de proyecto económico" /> RTPE</li>
			<li id="node4"><img src="../../../Images/imgBitacorico.gif" title="Bitacórico" /> Bitacórico</li>
			<li id="node5"><img src="../../../Images/imgSecretaria.gif" title="Secretaria" /> Secretaria</li>
			<li id="node6"><img src="../../../Images/imgInvitado.gif" title="Invitado" /> Invitado</li>
		</ul>
	    </div>
	</div>	
	<div id="mainContainer">
		<!-- ONE <UL> for each "room" -->
		<div>
			<p>Team A</p>
			<ul id="box1">
				
			</ul>
		</div>
		<div>		
			<p>Team B</p>
			<ul id="box2"></ul>
		</div>
		<div>
			<p>Team C</p>
			<ul id="box3">
				
				
			</ul>
		</div>
		<div>
			<p>Team D</p>
			<ul id="box4"></ul>
		</div>
		<div>	
			<p>Team E</p>
			<ul id="box5">

			</ul>
		</div>
	</div>
</div>
<div id="footer">
	<input type="button" onclick="saveDragDropNodes()" value="Save">
</div>
<ul id="dragContent"></ul>
<div id="dragDropIndicator"><img src="../../../images/imgSeparador.gif"></div>
<div id="saveContent" style="position:absolute; top: 100px; left: 800px;"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

