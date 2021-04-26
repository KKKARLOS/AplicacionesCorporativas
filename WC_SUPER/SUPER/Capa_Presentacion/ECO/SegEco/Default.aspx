<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/UCGusano.ascx" TagName="UCGusano" TagPrefix="ucgus" %>
<%@ Import Namespace="System.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var usu_actual = "<%=Session["UsuarioActual"].ToString() %>";
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    bLectura = <%=((bool)Session["MODOLECTURA_PROYECTOSUBNODO"]) ? "true":"false" %>;
    var sOrigen = "<%=sOrigen%>";
    var nAnoMesActual = <%=DateTime.Now.Year * 100 + DateTime.Now.Month %>;
    var bRes1024 = <%=((bool)Session["CARRUSEL1024"]) ? "true":"false" %>;
    //var es_DIS = <%=(User.IsInRole("DIS"))? "true":"false" %>;
    //var sMOSTRAR_SOLODIS = "<%=ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"].ToString() %>";
    var sMONEDA_VDP = "<%=(Session["MONEDA_VDP"]==null)?"":Session["MONEDA_VDP"].ToString() %>";
    var nAltura = "<%=nAltura.ToString()%>";
    //alert(sMONEDA_VDP);
    var bAlertasActivadas = <%=((bool)Session["ALERTASPROY_ACTIVAS"]) ? "true":"false" %>;
-->
</script>
<style type="text/css">
.textoR2{color:#cc0000}
</style>
<ucgus:UCGusano ID="UCGusano1" runat="server" />
<div id="div1024" style="z-index: 105; WIDTH: 32px; HEIGHT: 32px; POSITION: absolute; TOP: 93px; right: 10px;">
    <asp:Image ID="img1024" CssClass="MA" runat="server" Height="32" Width="32" ImageUrl="~/images/imgICO1024.gif" ondblclick="setResolucionPantalla()" ToolTip="Conmuta el tamaño de pantalla para adecuarla a la resolución 1024x768 o 1280x1024" />
</div>
<div id="divMonedaImportes" runat="server" style="position:absolute; top:238px; left:32px; visibility:hidden">
<label id="lblLinkMonedaImportes" class="enlace" style="vertical-align:bottom;" onclick="getMonedaImportes()">Importes</label>&nbsp;&nbsp;<label style="vertical-align:top;"> en </label>&nbsp;&nbsp;<label id="lblMonedaImportes" style="vertical-align:top;" runat="server"></label></div>
<table id="tblSuperior" style="width:1230px; margin-left:10px; margin-top:0px;" border="0">
    <colgroup>
        <col style="width:640px;" />
        <col style="width:220px;" />
        <col style="width:210px;" />
        <col style="width:160px;" />
    </colgroup>
    <tr style="vertical-align:top; height:105px;">
        <td>
            <fieldset id="flsIdentificacion" style="width: 610px;">
			    <legend>Identificación</legend>   
                <table id="tblIdentificacion" style="width:600px; margin:3px;" cellpadding="3">
                    <colgroup>
                        <col style="width:80px;" />
                        <col style="width:520px;" />
                    </colgroup>
                    <tr>
                        <td><label id="lblProy" class="enlace" onclick="getPE()">Proyecto</label></td>
                        <td><asp:TextBox ID="txtNumPE" style="width:60px;" SkinID="Numero" Text="" runat="server" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarPE();}else{vtn2(event);setNumPE(1);}" />
                            <asp:TextBox ID="txtDesPE" style="width:440px;" Text="" MaxLength="70" runat="server" onkeypress="if(event.keyCode==13){buscarDesPE();event.keyCode=0;}else{setNumPE(2);}" /></td>
                    </tr>
                    <tr>
                        <td>Responsable</td>
                        <td><asp:TextBox ID="txtResponsable" style="width:503px;" Text="" runat="server" readonly="true" /></td>
                    </tr>
                    <tr>
                        <td>Cliente</td>
                        <td><asp:TextBox ID="txtCliente" style="width:503px;" Text="" runat="server" readonly="true" /></td>
                    </tr>
                </table>			    
			</fieldset>     
	    </td>
        <td>
            <fieldset style="width: 180px;">
			    <legend>Proyecto</legend>
                <table style="width:170px;margin:3px;" cellpadding="3">
                    <colgroup>
                        <col style="width:70px;" />
                        <col style="width:100px;" />
                    </colgroup>
                    <tr>
                        <td>Total</td>
                        <td><asp:TextBox ID="txtTotProyecto" style="width:80px;" SkinID="Numero" Text="" readonly="true" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Margen</td>
                        <td><asp:TextBox ID="txtMrgProyecto" style="width:80px;" SkinID="Numero" Text="" readonly="true" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Rentabilidad</td>
                        <td><asp:TextBox ID="txtRentProyecto" style="width:50px;" SkinID="Numero" Text="" readonly="true" runat="server" /> %</td>
                    </tr>
                </table>			    
			</fieldset>     
        </td>
        <td>
            <fieldset id="fstContrato" style="width: 180px; padding-bottom:0px;">
			    <legend id="lgdContrato">Contrato </legend>
                <table style="width:170px;margin:3px; margin-bottom:2px;" cellpadding="1">
                    <colgroup>
                        <col style="width:70px;" />
                        <col style="width:100px;" />
                    </colgroup>
                    <tr>
                        <td>Total</td>
                        <td><asp:TextBox ID="txtTotContrato" style="width:80px;" SkinID="Numero" Text="" readonly="true" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Margen</td>
                        <td><asp:TextBox ID="txtMrgContrato" style="width:80px;" SkinID="Numero" Text="" readonly="true" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Rentabilidad</td>
                        <td><asp:TextBox ID="txtRentContrato" style="width:50px;" SkinID="Numero" Text="" readonly="true" runat="server" /> %</td>
                    </tr>
                    <tr>
                        <td id="cldTPPAC" title="Total producción de todos los proyectos asociados al contrato"><label id="lblTPPAC" class="texto">TPPAC</label></td>
                        <td><asp:TextBox ID="txtProducido" style="width:80px;" SkinID="Numero" Text="" readonly="true" runat="server" /></td>
                    </tr>
                </table>			    
			</fieldset>     
        </td>
        <td rowspan="2" align="center"><img id="imgEstado" src="../../../images/imgProyAbierto.gif" style="visibility:hidden;"></td>
    </tr>
</table>
<table id="tabla" cellpadding="1" style="width:100%" align="center" border="0">
	<tr>
		<td>
            <table id="tblIconos" style="width: 1225px;" border="0">
                <colgroup>
                    <col style="width:80px;" />
                    <col style="width:528px;" />
                    <col style="width:167px;" />
                    <col style="width:370px;" />
                    <col style="width:80px;" />
                </colgroup>
            <tr style="height:62px; vertical-align:bottom;">
            <td>
                &nbsp;
                <img id="imgNE1" src='../../../images/imgNE1off.gif' class="ne" onclick="setNE(1);"><img id="imgNE2" src='../../../images/imgNE2off.gif' class="ne" onclick="setNE(2);"/><img id="imgNE3" src='../../../images/imgNE3off.gif' class="ne" onclick="setNE(3);"/><img id="imgNE4" src='../../../images/imgNE4off.gif' class="ne" onclick="setNE(4);"/>
            </td>
            <td>
                <table style="width:340px">
                    <colgroup>
                        <col style="width:45px" />
                        <col style="width:45px" />
                        <col style="width:45px" />
                        <col style="width:45px" />
                        <col style="width:40px" />
                        <col style="width:35px" />
                        <col style="width:35px" />
                        <col style="width:35px" />
                        <col style="width:35px" />
                    </colgroup>
                      <tr> 
                        <td><img id="imgConsumo3x" class="ICO MANO" src="../../../Images/imgConsumo3x.gif" title="Muestra los consumos al tercer nivel" onclick="mostrarNivelAux('C', 3)" /></td>
                        <td><img id="imgProduccion3x" class="ICO MANO" src="../../../Images/imgProduccion3x.gif" title="Muestra la producción al tercer nivel" onclick="mostrarNivelAux('P', 3)" /></td>
                        <td><img id="imgIngresos3x" class="ICO MANO" src="../../../Images/imgIngresos3x.gif" title="Muestra los ingresos al tercer nivel" onclick="mostrarNivelAux('I', 3)" /></td>
                        <td><img id="imgCobros3x" class="ICO MANO" src="../../../Images/imgCobros3x.gif" title="Muestra los cobros al tercer nivel" onclick="mostrarNivelAux('O', 3)" /></td>
                        <td><img id="imgImpGasvi" class="ICO MANO" src="../../../Images/imgIconoAvion.gif" title="Imputaciones GASVI" onclick="accesoDirecto(9)" style="margin-left:10px;" /></td>
                        <td><img id="imgConsumo" class="ICO MANO" src="../../../Images/imgConsumo.gif" title="Consumos" onclick="accesoDirecto(1)" /></td>
                        <td><img id="imgProduccion" class="ICO MANO" src="../../../Images/imgProduccion.gif" title="Producción" onclick="accesoDirecto(2)" /></td>
                        <td><img id="imgIngresos" class="ICO MANO" src="../../../Images/imgIngresos.gif" title="Ingresos" onclick="accesoDirecto(3)" /></td>
                        <td><img id="imgCobros" class="ICO MANO" src="../../../Images/imgCobros.gif" title="Cobros" onclick="accesoDirecto(4)" /></td>
                      </tr>
                      <tr> 
                        <td><img id="imgConsumo4x" class="ICO MANO" src="../../../Images/imgConsumo4x.gif" title="Muestra los consumos al cuarto nivel" onclick="mostrarNivelAux('C', 4)" /></td>
                        <td><img id="imgProduccion4x" class="ICO MANO" src="../../../Images/imgProduccion4x.gif" title="Muestra la producción al cuarto nivel" onclick="mostrarNivelAux('P', 4)" /></td>
                        <td><img id="imgIngresos4x" class="ICO MANO" src="../../../Images/imgIngresos4x.gif" title="Muestra los ingresos al cuarto nivel" onclick="mostrarNivelAux('I', 4)" /></td>
                        <td><img id="imgCobros4x" class="ICO MANO" src="../../../Images/imgCobros4x.gif" title="Muestra los cobros al cuarto nivel" onclick="mostrarNivelAux('O', 4)" /></td>
                        <td><img id="imgProduccionAvance" class="ICO MANO" src="../../../Images/imgProduccionAvance.gif" title="Avance técnico" onclick="accesoDirecto(10)" style="margin-left:10px;" /></td>
                        <td><img id="imgConsumoProf" class="ICO MANO" src="../../../Images/imgConsumoProf.gif" title="Consumos por profesional" onclick="accesoDirecto(5)" /></td>
                        <td><img id="imgProduccionProf" class="ICO MANO" src="../../../Images/imgProduccionProf.gif" title="Producción por profesional" onclick="accesoDirecto(6)" /></td>
                        <td><img id="imgConsumoNivel" class="ICO MANO" src="../../../Images/imgConsumoNivel.gif" title="Consumos por nivel" onclick="accesoDirecto(7)" /></td>
                        <td><img id="imgProduccionPerfil" class="ICO MANO" src="../../../Images/imgProduccionPerfil.gif" title="Producción por perfil" onclick="accesoDirecto(8)" /></td>
                      </tr>
                </table>
            </td>
            <td><img id="imgME" src="../../../Images/imgMesEstrella2.gif" border="0" title="Posiciona en el primer mes abierto" onclick="setME()" style="cursor:pointer;" /></td>
            <td><div id="divCualidadPSN" style="width:120px; height:40px; display:inline;"><asp:Image ID="imgCualidadPSN" runat="server" Height="40" Width="120" ImageUrl="~/images/imgSeparador.gif" /></div>
                <div id="divDialogos" onclick="getDialogosProy();">
                    <img id="imgDialogos" src="../../../Images/imgIconoDialogos16.png" />
                    <label id="lblCountDialogosAbiertos"></label>
                    <div id="divCountLeer"></div>
                    <div id="divCountResponder"></div>
                </div>
            </td>
            <td>
            <img id="imgCaution" src="../../../Images/imgCaution.gif" border="0" style="display:none;" />
            </td>
            </tr>
            </table>
            <table id="tblTitulo" style="width: 1225px; height: 17px; text-align:right;" cellpadding="0" >
                <colgroup>
                <col style="width:390px;" />
                <col style="width:85px;" />
                <col style="width:85px;" />
                <col style="width:30px;" />
                <col style="width:65px;" />
                <col style="width:30px;" />
                <col style="width:65px;" />
                <col style="width:85px;" />
                <col style="width:130px;" />
                <col style="width:130px;" />
                <col style="width:130px;" />
                </colgroup>
                <tr class="TBLINI">
                    <td style="text-align:left;padding-left:25px;">Grupo / Subgrupo / Concepto / Clase</td>
                    <td><label id="lblMesM2"></label></td>
                    <td><label id="lblMesM1"></label></td>
                    <td>
                        <img id="imgPM" src="../../../Images/imgFlPri.gif" onclick="cambiarMes('P')" style="cursor: pointer;" border="0" />
                        <img id="imgAM" src="../../../Images/imgFlAnt.gif" onclick="cambiarMes('A')" style="cursor: pointer;" border="0" />
                    </td>
                    <td>
                        <label id="lblMes0" style="width:58px; margin-right:3px;"></label>
                    </td>
                    <td>
                        <img id="imgSM" src="../../../Images/imgFlSig.gif" onclick="cambiarMes('S')" style="cursor: pointer;" border="0" />
                        <img id="imgUM" src="../../../Images/imgFlUlt.gif" onclick="cambiarMes('U')" style="cursor: pointer;" border="0" />
                    </td>
                    <td><label id="lblMesP1"></label></td>
                    <td><label id="lblMesP2"></label></td>
                    <td><label id="lblIA"></label></td>
                    <td><label id="lblIP"></label></td>
                    <td style="padding-right:2px;"><label id="lblTP"></label></td>
                </tr>
            </table>
            <div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width:1241px;" runat="server">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:1225px">
                </div>
            </div>
            <table id="tblResultado" style="width: 1225px; height: 17px; text-align:right;" border="0">
                <colgroup>
                <col style='width:390px;' />
                <col style='width:85px;' />
                <col style='width:85px;' />
                <col style='width:95px;' />
                <col style='width:95px;' />
                <col style='width:85px;' />
                <col style='width:130px;' />
                <col style='width:130px;' />
                <col style='width:130px;' />
                </colgroup>
                <tr class="TBLFIN">
                    <td style='text-align:left;'>&nbsp;&nbsp;Margen de contribución</td>
                    <td><label id='txtTotM2'></label></td>
                    <td><label id='txtTotM1'></label></td>
                    <td><label id='txtTot0'></label></td>
                    <td><label id='txtTotP1'></label></td>
                    <td><label id='txtTotP2'></label></td>
                    <td><label id='txtTotIA'></label></td>
                    <td><label id='txtTotIP'></label></td>
                    <td style='text-align:right; padding-right:2px;'><label id='txtTotTP'></label></td>
                </tr>
            </table>
            <table id="tblRentabilidad" style="width: 1225px; height: 17px; text-align:right;">
                <colgroup>
                <col style='width:390px;' />
                <col style='width:85px;' />
                <col style='width:85px;' />
                <col style='width:95px;' />
                <col style='width:95px;' />
                <col style='width:85px;' />
                <col style='width:130px;' />
                <col style='width:130px;' />
                <col style='width:130px;' />
                </colgroup>
                <tr class="TBLFIN">
                    <td style='text-align:left;'>&nbsp;&nbsp;Rentabilidad&nbsp;&nbsp;(%)</td>
                    <td><label id='txtRM2'></label></td>
                    <td><label id='txtRM1'></label></td>
                    <td><label id='txtR0'></label></td>
                    <td><label id='txtRP1'></label></td>
                    <td><label id='txtRP2'></label></td>
                    <td><label id='txtRIA'></label></td>
                    <td><label id='txtRIP'></label></td>
                    <td style='text-align:right; padding-right:2px;'><label id='txtRTP'></label></td>
                </tr>
            </table>
            <table id="tblIngNetos" style="width: 1225px; height: 17px; text-align:right;">
                <colgroup>
                <col style='width:390px;' />
                <col style='width:85px;' />
                <col style='width:85px;' />
                <col style='width:95px;' />
                <col style='width:95px;' />
                <col style='width:85px;' />
                <col style='width:130px;' />
                <col style='width:130px;' />
                <col style='width:130px;' />
                </colgroup>
                <tr class="TBLFIN">
                    <td style='text-align:left;'>&nbsp;&nbsp;Ingresos netos</td>
                    <td><label id='txtINM2'></label></td>
                    <td><label id='txtINM1'></label></td>
                    <td><label id='txtIN0'></label></td>
                    <td><label id='txtINP1'></label></td>
                    <td><label id='txtINP2'></label></td>
                    <td><label id='txtINIA'></label></td>
                    <td><label id='txtINIP'></label></td>
                    <td style='text-align:right; padding-right:2px;'><label id='txtINTP'></label></td>
                </tr>
            </table>
            <table id="tblObraCurso" style="width: 1225px; height: 17px; text-align:right;">
                <colgroup>
                <col style='width:390px;' />
                <col style='width:85px;' />
                <col style='width:85px;' />
                <col style='width:95px;' />
                <col style='width:95px;' />
                <col style='width:85px;' />
                <col style='width:130px;' />
                <col style='width:130px;' />
                <col style='width:130px;' />
                </colgroup>
                <tr class="TBLFIN">
                    <td style='text-align:left;'>&nbsp;&nbsp;Obra en curso</td>
                    <td><label id='txtOCM2'></label></td>
                    <td><label id='txtOCM1'></label></td>
                    <td><label id='txtOC0'></label></td>
                    <td><label id='txtOCP1'></label></td>
                    <td><label id='txtOCP2'></label></td>
                    <td><label id='txtOCIA'></label></td>
                    <td><label id='txtOCIP'></label></td>
                    <td style='text-align:right; padding-right:2px;'><label id='txtOCTP'></label></td>
                </tr>
            </table>
            <table id="tblSaldoClientes" style="width: 1225px; height: 17px; text-align:right;"> 
                <colgroup>
                <col style='width:390px;' />
                <col style='width:85px;' />
                <col style='width:85px;' />
                <col style='width:95px;' />
                <col style='width:95px;' />
                <col style='width:85px;' />
                <col style='width:130px;' />
                <col style='width:130px;' />
                <col style='width:130px;' />
                </colgroup>
                <tr class="TBLFIN">
                    <td style='text-align:left;'>&nbsp;&nbsp;Saldo de clientes</td>
                    <td><label id='txtSCM2'></label></td>
                    <td><label id='txtSCM1'></label></td>
                    <td><label id='txtSC0'></label></td>
                    <td><label id='txtSCP1'></label></td>
                    <td><label id='txtSCP2'></label></td>
                    <td><label id='txtSCIA'></label></td>
                    <td><label id='txtSCIP'></label></td>
                    <td style='text-align:right; padding-right:2px;'><label id='txtSCTP'></label></td>
                </tr>
            </table>        
            </td>
    </tr>
</table>
<div id="divMeses" style="z-index: 9999; visibility: hidden; width: 190px; height: 500px; position: absolute; top: 260px; left: 550px">						
	<table class="texto tblborder" cellpadding="10" style="width:100%; background-color:#bcd4df; text-align:center;">
		<tr>
			<td align="center"><b><font size="3">Meses</font></b>
			</td>
		</tr>
	</table>
	<table class="texto tblborder" cellpadding="10" style="width:100%; text-align:center; background-color:#D8E5EB;">
        <tr>
            <td>
                <div id="divCatalogoMeses" style="overflow:auto; overflow-x:hidden; width:166px; height:240px">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif); width:150px;">
                        <table id='tblMeses' style='width:150px; text-align:center; font-weight:bold;'>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align:center;">	
                <center>
                <button id="btnCancelar" type="button" onclick="$I('divMeses').style.visibility = 'hidden';" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../images/botones/imgCancelar.gif" /><span title="Cancelar sin seleccionar ningun mes">Cancelar</span>
                </button>	
                </center>
            </td>
        </tr>	    
    </table>
</div>

<div id='hdnDIVTableAux'></div>
<asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnModeloCoste" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnModeloTarificacion" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnCualidadProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnAnnoPIG" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnAnotarProd" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="ListaPSN" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="nDesdeREP" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="nHastaREP" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnEsReplicable" runat="server" style="visibility:hidden" Text="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {

        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

            switch (strBoton) {
                case "insertarmes": 
				{
                    bEnviar = false;
                    insertarmes();
					break;
				}
				case "replica": 
				{
                    bEnviar = false;
                    replica();
					break;
				}
				case "cerrarmes": 
				{
                    bEnviar = false;
                    cerrarmes();
					break;
				}
				case "borrarmes": 
				{
                    bEnviar = false;
                    borrarmes();
					break;
				}
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("DetalleEconomico.pdf");
					break;
				}
				case "resumeneco": 
				{
                    bEnviar = false;
//                    location.href = "../ResumenEcoProy/Default.aspx";
	                document.forms["aspnetForm"].method = "POST";
	                document.forms["aspnetForm"].action = "../ResumenEcoProy/Default.aspx";
	                document.forms["aspnetForm"].submit();
					break;
				}
				case "clonar": 
				{
                    bEnviar = false;
                    clonarmes();
					break;
				}
				case "icotras": 
				{
                    bEnviar = false;
                    traspasoiap();
					break;
	            }
    	        case "graficovg":
	            {
	                bEnviar = false;
	                crearlineabase();
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
-->
</script>
</asp:Content>

