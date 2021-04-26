<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" EnableViewState="False" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<style type="text/css">
    #tblDatos TD{border-right: solid 1px #A6C3D2; padding-right:2px; text-align:right;}
    #tblTotales TD{border-right: solid 1px #A6C3D2; padding-right:2px; text-align:right;}
</style>
<script type="text/javascript">
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";    
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var strEstructuraSN1 = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1) %>";
    var strEstructuraSN2 = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2) %>";
    var strEstructuraSubnodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>";
    var nEstructuraMinima = <%=nEstructuraMinima.ToString() %>;
    var nUtilidadPeriodo = <%=nUtilidadPeriodo.ToString() %>;
    var bRes1024 = <%=((bool)Session["DATOSRES1024"]) ? "true":"false" %>;
    var sSubnodos = "<%=sSubnodos %>";
    var bHayPreferencia = <%=sHayPreferencia %>;
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;

    <%=sCriterios %>
</script>
<img id="imgPestHorizontalAux" src="../../../../Images/imgPestHorizontal.gif" style="Z-INDEX: 0;position:absolute; left:40px; top:98px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
<div id="divOnline" align="left" style="width: 170px; position:absolute; top: 90px; left:205px;">
    <div align="center" style="background-image: url('../../../../Images/imgFondoCal3.gif'); background-repeat:no-repeat;
        width: 90px; height: 23px; position: relative; top: 12px; left: 10px; padding-top: 5px;">
        Base de cálculo
    </div>
    <table border="0" cellpadding="0" cellspacing="0" class="texto" style="table-layout:fixed; width:140px">
        <tr>
            <td background="../../../../Images/Tabla/7.gif" height="6" width="6">
            </td>
            <td background="../../../../Images/Tabla/8.gif" height="6">
            </td>
            <td background="../../../../Images/Tabla/9.gif" height="6" width="6">
            </td>
        </tr>
        <tr>
            <td background="../../../../Images/Tabla/4.gif" width="6">
                &nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding-top:5px; padding-left:5px; vertical-align:middle;">
                <!-- Inicio del contenido propio de la página -->
                <asp:RadioButtonList ID="rdbResultadoCalculo" SkinId="rbl" runat="server" RepeatColumns="2" style="margin-top:2px;" onclick="setResultadoOnline(1)">
                    <asp:ListItem Value="1" style="cursor:pointer;" Selected title="Obtiene la información en base a cálculos online. Información actualizada pero mayor tiempo de respuesta.">Online&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="0" style="cursor:pointer;" title="Obtiene la información en base a cálculos realizados a las 7 de la mañana. Menor tiempo de respuesta pero posibilidad de datos no actualizados.">7 AM</asp:ListItem>
                </asp:RadioButtonList>
                <!-- Fin del contenido propio de la página -->
            </td>
            <td background="../../../../Images/Tabla/6.gif" width="6">
                &nbsp;</td>
        </tr>
        <tr>
            <td background="../../../../Images/Tabla/1.gif" height="6" width="6">
            </td>
            <td background="../../../../Images/Tabla/2.gif" height="6">
            </td>
            <td background="../../../../Images/Tabla/3.gif" height="6" width="6">
            </td>
        </tr>
    </table>
</div>
<div id="divMonedaImportes" runat="server" style="position:absolute; top:128px; left:400px; visibility:hidden">
    <label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()">Importes</label> en <label id="lblMonedaImportes" runat="server" style="width:400px;">Dólares americanos</label>
</div>
<div id="divPestRetr" style="position:absolute; left:20px; top:98px; width:960px; height:560px; clip:rect(auto auto 0px auto)">
    <table style="width:960px; height:560px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
    <tr>
        <td>
            <table class="texto" style="width:940px; height:560px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                <tr>
		            <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../../Images/Tabla/5.gif" style="padding: 5px">
                        <!-- Inicio del contenido propio de la página -->
                        <table class="texto" style="width:930px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                        <colgroup>
                            <col style="width:190px;" />
                            <col style="width:120px;" />
                            <col style="width:155px;" />
                            <col style="width:155px;" />
                            <col style="width:155px;" />
                            <col style="width:55px;" />
                            <col style="width:100px;" />
                        </colgroup>
                        <tr style="height:40px;">
                            <td>Concepto eje<br />
                                    <asp:DropDownList id="cboConceptoEje" onchange="setLeyenda();setCombo()" runat="server" style="width:149px; vertical-align:middle;" CssClass="combo">
                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Estructura organizativa"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="Cliente"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="Naturaleza"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="Responsable de proyecto"></asp:ListItem>
										<asp:ListItem Value="10" Text="Proyecto"></asp:ListItem>
										<asp:ListItem Value="12" Text="Cualificación ventas"></asp:ListItem>
                                    </asp:DropDownList>
                            </td>
                            <td>
                                    Estado<br />
                                    <asp:DropDownList id="cboEstado" runat="server" Width="100px" CssClass="combo" >
					                <asp:ListItem Value="" Text=""></asp:ListItem>
					                <asp:ListItem Value="A" Text="Abierto"></asp:ListItem>
					                <asp:ListItem Value="C" Text="Cerrado"></asp:ListItem>
					                <asp:ListItem Value="H" Text="Histórico"></asp:ListItem>
					                <asp:ListItem Value="P" Text="Presupuestado"></asp:ListItem>
					                </asp:DropDownList>                            
											
                            </td>
                            <td>
                            Categoría<br /><asp:DropDownList id="cboCategoria" runat="server" Width="130px" onChange="setCombo()" CssClass="combo">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="P" Text="Producto"></asp:ListItem>
                                <asp:ListItem Value="S" Text="Servicio"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td><img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom;">
                                <img border='0' src='../../../../Images/imgCerrarAuto.gif' style="vertical-align: bottom; margin-left:30px;"
                                    title="Repliegue automático de la pestaña de criterios al obtener información">
                                <input id="chkCerrarAuto" runat="server" class="check" type="checkbox" checked />
                            </td>
                            <td>
                                <img src='../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom;">
                                <input type=checkbox id="chkActuAuto" class="check" runat="server" />
                            </td>
                            <td>
                                <button id="btnObtener" type="button" onclick="buscar()" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                    <img src="../../../../images/imgObtener.gif" /><span title="Obtener">Obtener</span>
                                </button>    
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label><img id="Img14" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divAmbito" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:40px; margin-top:0px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:260px;">
                                         <table id="tblAmbito" class="texto" style="width:260px;">
                                         <%=strHTMLAmbito%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label5" class="enlace" onclick="getCriterios(6)" runat="server">Sector</label><img id="Img1" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(6)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divSector" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblSector" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLSector%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td>
                                <FIELDSET style="width: 140px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label1" class="enlace" onclick="getPeriodo()">Periodo</label></LEGEND>
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
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 130px; height:60px; padding:5px;">
                                    <LEGEND title="Aplicable sólo entre diferentes criterios">Operador lógico</LEGEND>
                                    <asp:RadioButtonList ID="rdbOperador" SkinId="rbli" runat="server" RepeatColumns="2" style="margin-top:8px;" onclick="setOperadorLogico(true)">
                                        <asp:ListItem Value="1" style="cursor:pointer" Selected><img src='../../../../Images/imgY.gif' border='0' title="Criterios acumulados" style="cursor:pointer" onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="0" style="cursor:pointer"><img src='../../../../Images/imgO.gif' border='0' title="Criterios independientes" style="cursor:pointer" onclick="this.parentNode.click()"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </FIELDSET>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label2" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img2" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divResponsable" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblResponsable" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLResponsable%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label6" class="enlace" onclick="getCriterios(7)" runat="server">Segmento</label><img id="Img3" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(7)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divSegmento" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblSegmento" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLSegmento%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="3">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label3" class="enlace" onclick="getCriterios(3)" runat="server">Naturaleza</label><img id="Img4" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(3)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divNaturaleza" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblNaturaleza" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLNaturaleza%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label7" class="enlace" onclick="getCriterios(8)" runat="server">Cliente</label><img id="Img5" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(8)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divCliente" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblCliente" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLCliente%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label4" class="enlace" onclick="getCriterios(4)" runat="server">Modelo de contratación</label><img id="Img6" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(4)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divModeloCon" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblModeloCon" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLModeloCon%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="3">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label8" class="enlace" onclick="getCriterios(9)" runat="server">Contrato</label><img id="Img7" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(9)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divContrato" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblContrato" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLContrato%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>                            
                        </tr>
                        <tr>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label10" class="enlace" onclick="getCriterios(16)" runat="server">Proyecto</label><img id="Img15" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(16)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divProyecto" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblProyecto" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLProyecto%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label9" class="enlace" onclick="getCriterios(5)" runat="server">Horizontal</label><img id="Img8" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(5)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divHorizontal" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblHorizontal" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLHorizontal%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="3">
                                <FIELDSET id="fstCDP" runat="server" style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCDP" class="enlace" onclick="getCriterios(10)" runat="server">Qn</label><img id="Img9" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(10)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divQn" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblQn" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLQn%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <FIELDSET id="fstCSN1P" runat="server" style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCSN1P" class="enlace" onclick="getCriterios(11)" runat="server">Q1</label><img id="Img10" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(11)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divQ1" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblQ1" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLQ1%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET id="fstCSN2P" runat="server" style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCSN2P" class="enlace" onclick="getCriterios(12)" runat="server">Q2</label><img id="Img11" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(12)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divQ2" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblQ2" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLQ2%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="3" style="padding-top:1px; vertical-align:top;">  
                                <fieldset id="fldCualif" runat="server" style="width: 290px; height:60px; padding:5px;">
					            <legend><label id="lblCualif" class="enlace" onclick="getCriterios(20)" runat="server">Cualificación de las ventas</label><img id="Img16" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(20)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <DIV id="divCualif" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblCualif" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLCualif%>
                                         </table>
                                        </div>
                                    </DIV>
			                    </fieldset>	                                                    
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <FIELDSET id="fstCSN3P" runat="server" style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCSN3P" class="enlace" onclick="getCriterios(13)" runat="server">Q3</label><img id="Img12" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(13)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divQ3" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblQ3" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLQ3%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET id="fstCSN4P" runat="server" style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCSN4P" class="enlace" onclick="getCriterios(14)" runat="server">Q4</label><img id="Img13" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(14)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divQ4" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblQ4" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLQ4%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="3">
                                <div id="divOnline2" align="left" style="width: 170px;">
                                    <div align="center" style="background-image: url('../../../../Images/imgFondoCal3.gif'); background-repeat:no-repeat;
                                        width: 90px; height: 23px; position: relative; top: 12px; left: 10px; padding-top: 5px;">
                                        Base de cálculo</div>
                                    <table border="0" cellpadding="0" cellspacing="0" class="texto" style="table-layout:fixed; width:140px">
                                        <tr>
                                            <td background="../../../../Images/Tabla/7.gif" height="6" width="6">
                                            </td>
                                            <td background="../../../../Images/Tabla/8.gif" height="6">
                                            </td>
                                            <td background="../../../../Images/Tabla/9.gif" height="6" width="6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td background="../../../../Images/Tabla/4.gif" width="6">
                                                &nbsp;</td>
                                            <td background="../../../../Images/Tabla/5.gif" style="padding-top:5px; padding-left:5px; vertical-align:middle;">
                                                <!-- Inicio del contenido propio de la página -->
                                                <asp:RadioButtonList ID="rdbResultadoCalculo2" SkinId="rbl" runat="server" RepeatColumns="2" style="margin-top:2px;" onclick="setResultadoOnline(2)">
                                                    <asp:ListItem Value="1" style="cursor:pointer;" Selected title="Obtiene la información en base a cálculos online. Información actualizada pero mayor tiempo de respuesta.">Online&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="0" style="cursor:pointer;" title="Obtiene la información en base a cálculos realizados a las 7 de la mañana. Menor tiempo de respuesta pero posibilidad de datos no actualizados.">7 AM</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <!-- Fin del contenido propio de la página -->
                                            </td>
                                            <td background="../../../../Images/Tabla/6.gif" width="6">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td background="../../../../Images/Tabla/1.gif" height="6" width="6">
                                            </td>
                                            <td background="../../../../Images/Tabla/2.gif" height="6">
                                            </td>
                                            <td background="../../../../Images/Tabla/3.gif" height="6" width="6">
                                            </td>
                                        </tr>
                                    </table>
                                </div><br />  
                                <div id="divMonedaImportes2" runat="server" style="visibility:hidden;">
                                    <label id="lblLinkMonedaImportes2" class="enlace" onclick="getMonedaImportes()">Importes</label> en <label id="lblMonedaImportes2" runat="server" style="width:230px;">Dólares americanos</label>
                                </div>                                                                                  
                            </td>                            
                        </tr>                       
                        </table>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td background="../../../../Images/Tabla/6.gif" width="6">
                        &nbsp;</td>
                </tr>
                <tr>
				    <td background="../../../../Images/Tabla/1.gif" height="6" width="6">
				    </td>
                    <td background="../../../../Images/Tabla/2.gif" height="6">
                    </td>
                    <td background="../../../../Images/Tabla/3.gif" height="6" width="6">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
</div>
<table id="tblGeneral" class="texto" style="margin-top:55px; margin-left:-5px; margin-bottom:10px; width:1028px;">
    <tr>
        <td>
            <div id="divTablaTitulo" class="TBLINI" style="overflow:auto; width:1002px; height:17px;" runat="server">
            <table id="tblTitulo" style="width:984px; HEIGHT:17px;">
            <colgroup>
                <col style="width:352px;" />
                <col style="width:79px;" />
                <col style="width:79px;" />
                <col style="width:79px;" />
                <col style="width:79px;" />
                <col style="width:79px;" />
                <col style="width:79px;" />
                <col style="width:79px;" />
                <col style="width:79px;" />
            </colgroup>
	            <tr class="TBLINI" align="center">
					<td>&nbsp;Denominación</td>
					<td id="tituloV1" style="text-align:right;" title="Saldo anterior de obra en curso">S.Ant. O.C.</td>
					<td id="tituloV2" style="text-align:right;" title="Saldo anterior de facturación anticipada">S.Ant. F.A.</td>
					<td id="tituloV3" style="text-align:right";>Ingresos</td>
					<td id="tituloV4" style="text-align:right;">Producción</td>
					<td id="tituloV5" style="text-align:right;" title="Variación de obra en curso">Variac.O.C.</td>
					<td id="tituloV6" style="text-align:right;" title="Variación de facturación anticipada">Variac.F.A.</td>
					<td id="tituloV7" style="text-align:right;"title="Saldo final de obra en curso">S.Final O.C.</td>
					<td id="tituloV8" style="text-align:right; padding-right:2px;"title="Saldo final de facturación anticipada">S.Final F.A.</td>
	            </tr>
            </table>
            </div>
            <div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width:1002px; height:480px;" onscroll="scrollTablaDR()">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:984px;">
                <%=strTablaHTML%>
                </div>
            </div>
            <div id="divResultado" class="TBLFIN" style="overflow:auto; overflow-x:hidden; width:984px; height:17px;" runat="server">
            <table id="tblTotales" style="width:984px; HEIGHT: 17px; text-align: right;">
                <colgroup>
                <col style="width:352px;" />
                <col style="width:79px;" />
                <col style="width:79px;" />
                <col style="width:79px;" />
                <col style="width:79px;" />
                <col style="width:79px;" />
                <col style="width:79px;" />
                <col style="width:79px;" />
                <col style="width:79px;" />
                </colgroup>
	            <tr class="TBLFIN">
                    <td>&nbsp;</td>
                    <td id="totalV1" runat="server" style="color:Black; text-align:right;">0,00</td>
                    <td id="totalV2" runat="server" style="color:Black; text-align:right;">0,00</td>
                    <td id="totalV3" runat="server" style="color:Black; text-align:right;">0,00</td>
                    <td id="totalV4" runat="server" style="color:Black; text-align:right;">0,00</td>
                    <td id="totalV5" runat="server" style="color:Black; text-align:right;">0,00</td>
                    <td id="totalV6" runat="server" style="color:Black; text-align:right;">0,00</td>
                    <td id="totalV7" runat="server" style="color:Black; text-align:right;">0,00</td>
                    <td id="totalV8" runat="server" style="color:Black; text-align:right;">0,00</td>
	            </tr>
            </table>
            </div>
        </td>
    </tr>
</table>
<div style="margin-left:10px;">
    <nobr id="imgLeySN4" style="display:none"><img class="ICO" src="../../../../Images/imgSN4.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4) %>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySN3" style="display:none"><img class="ICO" src="../../../../Images/imgSN3.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySN2" style="display:none"><img class="ICO" src="../../../../Images/imgSN2.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySN1" style="display:none"><img class="ICO" src="../../../../Images/imgSN1.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeyNodo" style="display:none"><img class="ICO" src="../../../../Images/imgNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySubNodo" style="display:none"><img class="ICO" src="../../../../Images/imgSubNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeyCli" style="display:none"><img class="ICO" src="../../../../Images/imgClienteICO.gif" />&nbsp;Cliente&nbsp;&nbsp;</nobr>
    <nobr id="imgLeyRes" style="display:none">
        <img class="ICO" src="../../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
        <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo&nbsp;&nbsp;
    </nobr>
    <nobr id="imgLeyNat" style="display:none"><img class="ICO" src="../../../../Images/imgNaturaleza.gif" />&nbsp;Naturaleza de producción&nbsp;&nbsp;</nobr>
    <nobr id="imgLeyProy" style="display:none"><img class="ICO" src="../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' /><img class="ICO" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' /><img class="ICO" src="../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' /><img class="ICO" src="../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />&nbsp;Proyecto</nobr>
</div>

<asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
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

