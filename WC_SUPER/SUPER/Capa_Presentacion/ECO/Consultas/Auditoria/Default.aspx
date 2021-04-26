<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_Consultas_Auditoria_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<style>
    #tblTitulo TD{border-right: solid 1px #A6C3D2; padding-right:1px; text-align:right;}
    #tblDatos TD{border-right: solid 1px #A6C3D2; padding-right:1px; text-align:right;}
    #tblResultado TD{border-right: solid 1px #A6C3D2; padding-right:1px; text-align:right;}
</style>
<script type="text/javascript">

    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var strEstructuraSubnodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>";
    var nEstructuraMinima = <%=nEstructuraMinima.ToString() %>;
    var nUtilidadPeriodo = <%=nUtilidadPeriodo.ToString() %>;
    var bRes1024 = <%=((bool)Session["FICHAECO1024"]) ? "true":"false" %>;
    var sSubnodos = "<%=sSubnodos %>";
    var bHayPreferencia = <%=sHayPreferencia %>;
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;
    
    <%=sCriterios %>

</script>
<br /><br />
<div id="divMonedaImportes" runat="server" style="position:absolute; top:128px; left:400px; visibility:hidden">
    <label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()">Importes</label> en <label id="lblMonedaImportes" runat="server" style="width:400px;">Dólares americanos</label>
</div>
<table id="tblCriterios" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
    <td height="6" background="../../../../Images/Tabla/8.gif"></td>
    <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
  </tr>
  <tr>
    <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
    <td background="../../../../Images/Tabla/5.gif" style="padding:5px;padding-left:20px">
        <!-- Inicio del contenido propio de la página -->
        <table class="texto" style="width:930px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
        <colgroup>
            <col style="width:310px;" />
            <col style="width:155px;" />
            <col style="width:155px;" />
            <col style="width:155px;" />
            <col style="width:55px;" />
            <col style="width:100px;" />
        </colgroup>
            <tr>
                <td>
                    <fieldset style="width: 290px; height:30px; padding:5px;">
                        <legend>Tipo actualización</legend>
                        <asp:CheckBox id="chkIns" runat="server" style="vertical-align:middle;" checked="true" /> Inserción
                        <asp:CheckBox id="chkDel" runat="server" style="vertical-align:middle; margin-left:15px;" checked="true" /> Borrado
                        <asp:CheckBox id="chkMod" runat="server" style="vertical-align:middle;margin-left:15px;" checked="true" /> Modificación
                    </fieldset>
                </td>
                <td>
                Categoría<br /><asp:DropDownList id="cboCategoria" runat="server" Width="130px" CssClass="combo">
                    <asp:ListItem Value="" Text=""></asp:ListItem>
                    <asp:ListItem Value="P" Text="Producto"></asp:ListItem>
                    <asp:ListItem Value="S" Text="Servicio"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                Cualidad<br /><asp:DropDownList id="cboCualidad" runat="server" Width="130px" CssClass="combo">
                        <asp:ListItem Value="" Text=""></asp:ListItem>
                        <asp:ListItem Value="C" Text="Contratante"></asp:ListItem>
                        <asp:ListItem Value="J" Text="Replicado sin gestión"></asp:ListItem>
                        <asp:ListItem Value="P" Text="Replicado con gestión"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom;">
                    <img border='0' src='../../../../Images/imgCerrarAuto.gif' style="vertical-align: bottom; margin-left:30px; visibility:hidden;"
                        title="Repliegue automático de la pestaña de criterios al obtener información">
                    <input id="chkCerrarAuto" runat="server" class="check" type="checkbox" checked="checked" style="visibility:hidden;" />
                </td>
                <td>
                    <img src='../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom; visibility:hidden;">
                    <input type=checkbox id="chkActuAuto" class="check" runat="server" style="visibility:hidden;"/>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset style="width: 290px; height:60px; padding:5px;">
                        <legend><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label><img id="Img14" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divAmbito" style="overflow-x:hidden; overflow-y:auto; WIDTH: 276px; height:36px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px;">
                                <table id="tblAmbito" class="texto" style="width:260px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                <%=strHTMLAmbito%>
                                </table>
                            </div>
                        </div>
                    </fieldset>
                </td>
                <td colspan="2">
                    <fieldset style="width: 290px; height:60px; padding:5px;">
                        <legend><label id="Label5" class="enlace" onclick="getCriterios(6)" runat="server">Sector</label><img id="Img1" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(6)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divSector" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                <table id="tblSector" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                <%=strHTMLSector%>
                                </table>
                            </div>
                        </div>
                    </fieldset>
                </td>
                <td>
                    <fieldset style="width:135px; height:60px; padding:5px;">
                        <legend>
                            <label id="Label1" class="enlace" onclick="getPeriodo()">Periodo cierre</label>
                            <img id="Img16" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delPeriodo()" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;">
                        </legend>
                            <table style="width:135px;" cellpadding="3px" >
                                <colgroup><col style="width:35px;"/><col style="width:100px;"/></colgroup>
                                <tr>
                                    <td>Inicio</td>
                                    <td>
                                        <asp:TextBox ID="txtDesde" style="width:90px;" Text="" readonly="true" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fin</td>
                                    <td>
                                        <asp:TextBox ID="txtHasta" style="width:90px;" Text="" readonly="true" runat="server" />
                                    </td>
                                </tr>
                            </table>
                    </fieldset>
                </td>
                <td colspan="2">
                    <fieldset style="width:135px; height:60px; padding:5px;">
                        <legend><label id="lblPerAct" class="texto">Periodo actualización</label></legend>
                            <table style="width:135px;" cellpadding="3px" >
                                <colgroup><col style="width:35px;"/><col style="width:95px;"/></colgroup>
                                <tr>
                                    <td>Inicio</td>
                                    <td>
                                        <asp:TextBox ID="txtDesdeAct" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="VerFecha('D');" goma="0"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fin</td>
                                    <td>
                                        <asp:TextBox ID="txtHastaAct" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="VerFecha('H');" goma="0"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <FIELDSET style="width: 290px; height:60px; padding:5px;">
                        <LEGEND><label id="Label2" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img2" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <div id="divResponsable" style="overflow-x:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                <table id="tblResponsable" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                <%=strHTMLResponsable%>
                                </table>
                            </div>
                        </div>
                    </FIELDSET>
                </td>
                <td colspan="2">
                    <FIELDSET style="width: 290px; height:60px; padding:5px;">
                        <LEGEND><label id="Label6" class="enlace" onclick="getCriterios(7)" runat="server">Segmento</label><img id="Img3" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(7)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <div id="divSegmento" style="overflow-x:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                <table id="tblSegmento" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                <%=strHTMLSegmento%>
                                </table>
                            </div>
                        </div>
                    </FIELDSET>
                </td>
                <td colspan="3">
                    <FIELDSET style="width: 290px; height:60px; padding:5px;">
                        <LEGEND><label id="Label3" class="enlace" onclick="getCriterios(3)" runat="server">Naturaleza</label><img id="Img4" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(3)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <div id="divNaturaleza" style="overflow-x:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                <table id="tblNaturaleza" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                <%=strHTMLNaturaleza%>
                                </table>
                            </div>
                        </div>
                    </FIELDSET>
                </td>
            </tr>
            <tr>
                <td>
                    <FIELDSET style="width: 290px; height:60px; padding:5px;">
                        <LEGEND><label id="Label7" class="enlace" onclick="getCriterios(8)" runat="server">Cliente</label><img id="Img5" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(8)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <div id="divCliente" style="overflow-x:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                <table id="tblCliente" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                <%=strHTMLCliente%>
                                </table>
                            </div>
                        </div>
                    </FIELDSET>
                </td>
                <td colspan="2">
                    <FIELDSET style="width: 290px; height:60px; padding:5px;">
                        <LEGEND><label id="Label4" class="enlace" onclick="getCriterios(4)" runat="server">Modelo de contratación</label><img id="Img6" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(4)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <div id="divModeloCon" style="overflow-x:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                <table id="tblModeloCon" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                <%=strHTMLModeloCon%>
                                </table>
                            </div>
                        </div>
                    </FIELDSET>
                </td>
                <td colspan="3">
                    <FIELDSET style="width: 290px; height:60px; padding:5px;">
                        <LEGEND><label id="Label8" class="enlace" onclick="getCriterios(9)" runat="server">Contrato</label><img id="Img7" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(9)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <div id="divContrato" style="overflow-x:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                <table id="tblContrato" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                <%=strHTMLContrato%>
                                </table>
                            </div>
                        </div>
                    </FIELDSET>
                </td>                            
            </tr>
            <tr>
                <td>
                    <FIELDSET style="width: 290px; height:60px; padding:5px;">
                        <LEGEND><label id="Label10" class="enlace" onclick="getCriterios(16)" runat="server">Proyecto</label><img id="Img15" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(16)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <div id="divProyecto" style="overflow-x:hidden; overflow-y:auto; WIDTH: 276px; height:36px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px;">
                                <table id="tblProyecto" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                <%=strHTMLProyecto%>
                                </table>
                            </div>
                        </div>
                    </FIELDSET>
                </td>
                <td colspan="2">
                    <FIELDSET style="width: 290px; height:60px; padding:5px;">
                        <LEGEND><label id="Label9" class="enlace" onclick="getCriterios(5)" runat="server">Horizontal</label><img id="Img8" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(5)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <div id="divHorizontal" style="overflow-x:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                <table id="tblHorizontal" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                <%=strHTMLHorizontal%>
                                </table>
                            </div>
                        </div>
                    </FIELDSET>
                </td>
                <td colspan="3">
                    <FIELDSET id="fstCDP" runat="server" style="width: 290px; height:60px; padding:5px;">
                        <LEGEND><label id="lblCDP" class="enlace" onclick="getCriterios(10)" runat="server">Qn</label><img id="Img9" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(10)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <div id="divQn" style="overflow-x:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                <table id="tblQn" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                <%=strHTMLQn%>
                                </table>
                            </div>
                        </div>
                    </FIELDSET>
                </td>
            </tr>
            <tr>
                <td>
                    <FIELDSET id="fstCSN1P" runat="server" style="width: 290px; height:60px; padding:5px;">
                        <LEGEND><label id="lblCSN1P" class="enlace" onclick="getCriterios(11)" runat="server">Q1</label><img id="Img10" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(11)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <div id="divQ1" style="overflow-x:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                <table id="tblQ1" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                <%=strHTMLQ1%>
                                </table>
                            </div>
                        </div>
                    </FIELDSET>
                </td>
                <td colspan="2">
                    <FIELDSET id="fstCSN2P" runat="server" style="width: 290px; height:60px; padding:5px;">
                        <LEGEND><label id="lblCSN2P" class="enlace" onclick="getCriterios(12)" runat="server">Q2</label><img id="Img11" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(12)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <div id="divQ2" style="overflow-x:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                <table id="tblQ2" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                <%=strHTMLQ2%>
                                </table>
                            </div>
                        </div>
                    </FIELDSET>
                </td>
                <td colspan="3">
                    <FIELDSET id="fstCSN3P" runat="server" style="width: 290px; height:60px; padding:5px;">
                        <LEGEND><label id="lblCSN3P" class="enlace" onclick="getCriterios(13)" runat="server">Q3</label><img id="Img12" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(13)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <div id="divQ3" style="overflow-x:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                <table id="tblQ3" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                <%=strHTMLQ3%>
                                </table>
                            </div>
                        </div>
                    </FIELDSET>
                </td>
            </tr>
            <tr>
                <td>
                    <FIELDSET id="fstCSN4P" runat="server" style="width: 290px; height:60px; padding:5px;">
                        <LEGEND><label id="lblCSN4P" class="enlace" onclick="getCriterios(14)" runat="server">Q4</label><img id="Img13" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(14)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <div id="divQ4" style="overflow-x:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                <table id="tblQ4" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                <%=strHTMLQ4%>
                                </table>
                            </div>
                        </div>
                    </FIELDSET>
                </td>
                <td colspan="2">
                </td>
                <td>
                    <FIELDSET style="width:100px; height:60px; padding:5px;">
                        <LEGEND title="Aplicable sólo entre diferentes criterios">Operador lógico</LEGEND>
                        <asp:RadioButtonList ID="rdbOperador" SkinId="rbli" runat="server" RepeatColumns="2" style="margin-top:8px;" onclick="setOperadorLogico(true)">
                            <asp:ListItem Value="1" Selected><img src='../../../../Images/imgY.gif' border='0' title="Criterios acumulados" style="cursor:pointer;" onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0"><img src='../../../../Images/imgO.gif' border='0' title="Criterios independientes" style="cursor:pointer;" onclick="this.parentNode.click()"></asp:ListItem>
                        </asp:RadioButtonList>
                    </FIELDSET>
                </td>
                <td colspan="2">
		            <fieldset id="FIELDSET2" class="fld" style="height:25px; width:60px; padding-left:10px; margin-left:5px;" runat="server"> 
		            <legend class="Tooltip" title="Formato">&nbsp;Formato&nbsp;</legend>
						    <img src="../../../../Images/botones/imgExcel.gif" style="cursor:pointer; margin-left:20px;" title="Excel">
	                </fieldset>
                    <div id="divMonedaImportes2" runat="server" style="visibility:hidden;">
                        <label id="lblLinkMonedaImportes2" class="enlace" onclick="getMonedaImportes()">Importes</label> en <label id="lblMonedaImportes2" runat="server">Dólares americanos</label>
                    </div>
                    <button id="btnObtener" type="button" onclick="buscar()" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                        <img src="../../../../images/imgObtener.gif" /><span title="Obtener">Obtener</span>
                    </button>    
                </td>
            </tr>
        </table>
        <!-- Fin del contenido propio de la página -->
    </td>
    <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
  </tr>
  <tr>
    <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
    <td height="6" background="../../../../Images/Tabla/2.gif"></td>
    <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
  </tr>
</table>

<asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>
