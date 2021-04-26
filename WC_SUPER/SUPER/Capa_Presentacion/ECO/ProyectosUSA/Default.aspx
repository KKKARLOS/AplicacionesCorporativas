<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="USA" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
<script type="text/javascript">
<!--
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var bEsSAT = <%=(bEsSAT)? "true":"false" %>;
    var bEsSAA = <%=(bEsSAA)? "true":"false" %>;
    var strEstructuraNodoLarga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
-->
</script>
<center>
<table class="texto" style="width:500px;text-align:left">
    <colgroup>
        <col style="width:250px;" />
        <col style="width:250px;" />
    </colgroup>
    <tr>
        <td >
            <fieldset id="fldFiguro" style="width: 200px; height:45px;">
            <legend>Proyectos en los que figuro como</legend>
            <table style="width:180px; margin-top:5px; margin-left:30px;" cellpadding="5">
                <colgroup><col style="width:180px;" /></colgroup>
                <tr>
                    <td>
                        <label id="Label1" style="width:25px" title="Soporte titular de soporte administrativo de proyecto económico">SAT</label>
                        <input type="checkbox" id="chkSAT" runat="server" class="check" onclick="getProyectos()" title="Usuario titular de soporte administrativo de proyecto económico"/>
                        <label id="Label2" style="width:25px; margin-left:50px;" title="Soporte alternativo de soporte administrativo de proyecto económico">SAA</label>
                        <input type="checkbox" id="chkSAA" runat="server" class="check" onclick="getProyectos()" title="Usuario alternativo de soporte administrativo de proyecto económico"/>
                    </td>
                </tr>
            </table>   
            </fieldset>
        </td>
        <td>
            <fieldset id="fldSituacion" style="width:250px; height:45px;">
            <legend>Situación del proyecto</legend>
            <table style="width:250px; margin-top:5px;" cellpadding="5">
                <colgroup><col style="width:250px;" /></colgroup>
                <tr>
                    <td>
                        <label id="Label3" style="width:85px" title="Proyecto marcado como externalizable pero sin SAT ni SAA asignado">Externalizable</label>
                        <input type="checkbox" id="chkExternalizable" runat="server" class="check" onclick="getProyectos()" title="Proyecto marcado como externalizable pero sin SAT ni SAA asignado" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <label id="Label4" style="width:85px" title="Proyecto marcado como externalizable y con SAT y SAA asignado">Externalizado</label>
                        <input type="checkbox" id="chkExternalizado" runat="server" class="check" onclick="getProyectos()" title="Proyecto marcado como externalizable y con SAT y SAA asignado" />
                    </td>
                </tr>
            </table>   
            </fieldset>
        </td>
    </tr>
    </table>
<table class="texto" style="width:990px; margin-top:10px; text-align:left">
<tr>
    <td>
	    <table id="tblTitulo" style="width: 970px; height: 17px">
	        <colgroup>
	            <col style="width:20px" />
	            <col style="width:20px" />
	            <col style="width:20px" />
	            <col style="width:65px" />
			    <col style="width:295px;" />
			    <col style="width:150px;" />
			    <col style="width:150px;" />
			    <col style="width:150px;" />
			    <col style="width:100px;" />
	        </colgroup>
		    <tr class="TBLINI">
		        <td></td>
		        <td></td>
		        <td colspan="2" align=right><IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa1',event)"
						height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa1')"
						height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						<IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					    <MAP name="img1">
					        <AREA onclick="ot('tblDatos', 3, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					        <AREA onclick="ot('tblDatos', 3, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				        </MAP>&nbsp;Nº&nbsp;&nbsp;
				</td>
				<td><IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
					    <MAP name="img2">
					        <AREA onclick="ot('tblDatos', 4, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					        <AREA onclick="ot('tblDatos', 4, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				        </MAP>&nbsp;Proyecto&nbsp;
				        <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa2')"
						    height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
						<IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa2',event)"
						    height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
				</td>
			    <td><IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img3" border="0">
					    <MAP name="img3">
					        <AREA onclick="ot('tblDatos', 5, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					        <AREA onclick="ot('tblDatos', 5, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				        </MAP>&nbsp;Cliente&nbsp;
				        <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',5,'divCatalogo','imgLupa3')"
						    height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
						<IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',5,'divCatalogo','imgLupa3',event)"
						    height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
				</td>
			    <td title="Soporte Administrativo Titular">
			        <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img4" border="0">
				    <MAP name="img4">
				        <AREA onclick="ot('tblDatos', 6, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
				        <AREA onclick="ot('tblDatos', 6, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
			        </MAP>&nbsp;SAT&nbsp;
			        <IMG id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',6,'divCatalogo','imgLupa4')"
					    height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
					<IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',6,'divCatalogo','imgLupa4',event)"
					    height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
				</td>
			    <td  title="Soporte Administrativo Auxiliar">
			        <IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img5" border="0">
				    <MAP name="img5">
				        <AREA onclick="ot('tblDatos', 7, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
				        <AREA onclick="ot('tblDatos', 7, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
			        </MAP>&nbsp;SAA&nbsp;
			        <IMG id="imgLupa5" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',7,'divCatalogo','imgLupa5')"
					    height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
					<IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',7,'divCatalogo','imgLupa5',event)"
					    height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
				</td>
			    <td title="ültimo mes cerrado">UMC</td>
		    </tr>
	    </table>
        <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 986px; height:430px" onscroll="scrollTablaProy()">
            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:970px">                
            </div>
        </div>
        <table style="width: 970px; height: 17px">
            <tr class="TBLFIN">
                <td></td>
            </tr>
        </table>
        
    <table style="width:320px; margin-top:10px;">
        <colgroup>
            <col style="width:100px" />
            <col style="width:220px" />
        </colgroup>
          <tr> 
            <td><img class="ICO" src="../../../images/imgProducto.gif" />Producto</td>
            <td><img class="ICO" src="../../../images/imgServicio.gif" />Servicio</td>
          </tr>
          <tr>
            <td><img class="ICO" src="../../../images/imgIconoContratante.gif" />Contratante</td>
            <td></td>
          </tr>
          <tr>
            <td><img class="ICO" src="../../../images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto</td>
            <td><img class="ICO" src="../../../images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />Presupuestado</td>
          </tr>
    </table>
    </td>
</tr>
</table>
</center>

<uc_mmoff:mmoff ID="mmoff1" runat="server" />
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

