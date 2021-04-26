<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Absentismo_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
    var nAnoMesActual = <%=nAnoMesActual %>;
-->
</script>

<fieldset id="flsCriterios">
<legend>Criterios de selección</legend>   
<table id="tblCriterios" style="" cellpadding="2px">
    <colgroup>
        <col style="width:320px;" />
        <col style="width:320px;" />
        <col style="width:320px;" />
    </colgroup>
    <tr>
        <td>
            <img id="imgAM" src="../../../Images/btnAntRegOff.gif" />
            <asp:TextBox ID="txtMesBase" readonly="true" runat="server" Text=""></asp:TextBox>
            <img id="imgSM" src="../../../Images/btnSigRegOff.gif" />
        </td>
        <td>&nbsp;</td>
        <td>
            <button id="btnObtener" type="button" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../images/imgObtener.gif" /><span title="Obtener">Obtener</span>
            </button>    
        </td>
    </tr>
    <tr>
        <td>
            <fieldset style="width: 290px; height:60px; padding:5px;">
                <legend>
                    <label id="lblCentro" class="enlace" onclick="getCriterios(25)" runat="server">Centro de trabajo</label>
                    <img id="Img14" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(25)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:7px;">
                </legend>
                <div id="divCentro" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:36px; margin-top:2px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px;">
                     <table id="tblCentroTrabajo" class="texto" style="width:260px;">
                     <%//=strHTMLAmbito%>
                     </table>
                    </div>
                </div>
            </fieldset>
        </td>
        <td>
            <fieldset style="width: 290px; height:60px; padding:5px;">
                <legend>
                    <label id="lblEvaluador" class="enlace" onclick="getCriterios(24)" runat="server">Evaluador</label>
                    <img id="Img1" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(24)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:7px;">
                </legend>
                <div id="divEvaluador" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:36px; margin-top:2px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px;">
                     <table id="tblSupervisor" class="texto" style="width:260px;">
                     <%//=strHTMLAmbito%>
                     </table>
                    </div>
                </div>
            </fieldset>
        </td>
        <td>
            <fieldset style="width: 290px; height:60px; padding:5px;">
                <legend>
                    <label id="lblProyecto" class="enlace" onclick="getCriterios(16)" runat="server">Proyecto</label>
                    <img id="Img2" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(16)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:7px;">
                </legend>
                <div id="divProyecto" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:36px; margin-top:2px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px;">
                     <table id="tblProyecto" class="texto" style="width:260px;">
                     <%//=strHTMLAmbito%>
                     </table>
                    </div>
                </div>
            </fieldset>
        </td>
    </tr>
</table>
</fieldset>
<table style="width:1000px; margin-top:10px;" cellpadding="5">
    <TR>
	    <TD>
		    <table style="width:970px; height:17px">
            <colgroup>
                <col style='width:325px;' />
                <col style='width:325px;' />
                <col style='width:140px;' />
                <col style='width:60px;' />
                <col style='width:60px;' />
                <col style='width:60px;' />
            </colgroup>
	            <TR align="center" class="texto" style="height:20px;">
                    <td colspan="3"></td>
                    <td colspan="3" class="colTabla1">Nº jornadas de baja</td>
	            </TR>
			    <TR class="TBLINI">
			        <td><IMG style="CURSOR: pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
						    <MAP name="img1">
						        <AREA onclick="ot('tblDatos', 0, 0, '', '')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 0, 1, '', '')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;Profesional</td>
			        <td><IMG style="CURSOR: pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
						    <MAP name="img2">
						        <AREA onclick="ot('tblDatos', 1, 0, '', '')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 1, 1, '', '')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;Evaluador</td>
			        <td><IMG style="CURSOR: pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6" useMap="#img3" border="0">
						    <MAP name="img3">
						        <AREA onclick="ot('tblDatos', 2, 0, '', '')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 2, 1, '', '')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;Centro</td>
			        <td align="center" title="Estándar"><IMG style="CURSOR: pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6" useMap="#img4" border="0">
						    <MAP name="img4">
						        <AREA onclick="ot('tblDatos', 3, 0, 'num', '')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 3, 1, 'num', '')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;Iberper</td>
			        <td align="center" title="Jornadas económicas registradas"><IMG style="CURSOR: pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6" useMap="#img5" border="0">
						    <MAP name="img5">
						        <AREA onclick="ot('tblDatos', 4, 0, 'num', '')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 4, 1, 'num', '')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;PGE</td>
			        <td align="center" title="Jornadas IAP imputadas"><IMG style="CURSOR: pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6" useMap="#img6" border="0">
						    <MAP name="img6">
						        <AREA onclick="ot('tblDatos', 5, 0, 'num', '')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 5, 1, 'num', '')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;IAP</td>
			    </TR>
		    </TABLE>
		    <div id="divCatalogo" style="overflow: auto; width: 986px; height: 340px;">
                <div style="background-image:url('../../../Images/imgFT20.gif'); width:970px">
                </div>
            </div>
            <TABLE style="width:970px; height:17px">
			    <TR class="TBLFIN"><TD>&nbsp;</TD></TR>
		    </TABLE><br />
	    </TD>
    </TR>
</TABLE>
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
                case "excel":
                    {
                        bEnviar = false;
                        excel();
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

