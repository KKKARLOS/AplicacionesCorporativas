<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" EnableViewState="False" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<style>
#tblDatos TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
#tblTotales TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
</style>
<script type="text/javascript">

    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var strEstructuraSubnodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>";
    var nEstructuraMinima = <%=nEstructuraMinima.ToString() %>;
    var nUtilidadPeriodo = <%=nUtilidadPeriodo.ToString() %>;
    var bRes1024 = <%=((bool)Session["DATOSRES1024"]) ? "true":"false" %>;
    var sSubnodos = "<%=sSubnodos %>";
    var bHayPreferencia = <%=sHayPreferencia %>;
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;
    var nIDFicepiEntrada = <%=Session["IDFICEPI_ENTRADA"].ToString() %>;
    
    <%=sCriterios %>

</script>
<br /><br />
<div id="divTiempos" style="width: 400px; position: absolute; top:105px; left: 600px; display:none;">
</div>
<div id="div1024" style="Z-INDEX: 105; WIDTH: 32px; HEIGHT: 32px; POSITION: absolute; TOP: 93px; right: 10px;">
    <asp:Image ID="img1024" CssClass="MA" runat="server" Height="32" Width="32" ImageUrl="~/images/imgICO1024.gif" ondblclick="setResolucionPantalla()" ToolTip="Conmuta el tamaño de pantalla para adecuarla a la resolución 1024x768 o 1280x1024" />
</div>
<div id="divOnline" style="width: 170px; position:absolute; top: 90px; left:205px; text-align:left;">
    <div class="texto" style="background-image: url('../../../../Images/imgFondoCal3.gif'); text-align:center;
                                width: 90px; height: 23px; position: relative; background-repeat:no-repeat;
                                top: 12px; left:25px; padding-top:5px;">
        Base de cálculo
    </div>
        <table class="texto" style="width:140px; height:40px; table-layout:fixed;" border="0" cellspacing="0" cellpadding="0" align="center">
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
          </tr>
        <tr>
            <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding-top:5px; padding-left:5px;">
                <!-- Inicio del contenido propio de la página -->
                <asp:RadioButtonList ID="rdbResultadoCalculo" SkinId="rbl" runat="server" RepeatColumns="2" style="margin-top:2px;" onclick="setResultadoOnline(1)">
                    <asp:ListItem Value="1" style="cursor:pointer" Selected title="Obtiene la información en base a cálculos online. Información actualizada pero mayor tiempo de respuesta.">Online&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="0" style="cursor:pointer" title="Obtiene la información en base a cálculos realizados a las 7 de la mañana. Menor tiempo de respuesta pero posibilidad de datos no actualizados.">7 AM</asp:ListItem>
                </asp:RadioButtonList>
                <!-- Fin del contenido propio de la página -->
            </td>
            <td background="../../../../Images/Tabla/6.gif" width="6">&nbsp;</td>
        </tr>
        <tr>
            <td background="../../../../Images/Tabla/1.gif" height="6" width="6"></td>
            <td background="../../../../Images/Tabla/2.gif" height="6"></td>
            <td background="../../../../Images/Tabla/3.gif" height="6" width="6"></td>
        </tr>
    </table>
</div>
<div id="divMonedaImportes" runat="server" style="position:absolute; top:128px; left:400px; visibility:hidden">
    <label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()">Importes</label> en <label id="lblMonedaImportes" runat="server">Dólares americanos</label>
</div>
<img id="imgPestHorizontalAux" src="../../../../Images/imgPestHorizontal.gif" style="position:absolute; left:40px; top:98px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
<div id="divPestRetr" style="position:absolute; Z-INDEX: 2; left:20px; top:98px; width:960px; height:580px; clip:rect(auto auto 0px auto)">
    <table style="width:960px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
    <tr style="vertical-align:top;">
        <td>
            <table class="texto" style="width:940px; height:580px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                <tr>
		            <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../../Images/Tabla/5.gif" style="padding:5px">
                        <!-- Inicio del contenido propio de la página -->
                        <table class="texto" style="width:930px;">
                        <colgroup>
                            <col style="width:85px;" />
                            <col style="width:225px;" />
                            <col style="width:155px;" />
                            <col style="width:155px;" />
                            <col style="width:155px;" />
                            <col style="width:55px;" />
                            <col style="width:100px;" />
                        </colgroup>
                        <tr>
                            <td></td>
                            <td>Concepto eje<br />
                                <asp:DropDownList id="cboConceptoEje" onchange="nNivelEstructura=0;setLeyenda();setCombo()" runat="server" style="width:200px; vertical-align:middle;" CssClass="combo">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Estructura organizativa"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Cliente"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="Naturaleza"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="Responsable de proyecto"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="Proyecto"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>Categoría<br />
                                <asp:DropDownList id="cboCategoria" runat="server" Width="130px" onChange="setCombo()" CssClass="combo">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Producto"></asp:ListItem>
                                    <asp:ListItem Value="S" Text="Servicio"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>Cualidad<br />
                                <asp:DropDownList id="cboCualidad" runat="server" Width="130px" onChange="setCombo()" CssClass="combo">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="C" Text="Contratante"></asp:ListItem>
                                    <asp:ListItem Value="J" Text="Replicado sin gestión"></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Replicado con gestión"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom;">
                                <img border='0' src='../../../../Images/imgCerrarAuto.gif' style="vertical-align: bottom; margin-left:15px;"
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
                                    <LEGEND>
                                        <label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label>
                                        <img id="Img14" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:7px;">
                                    </LEGEND>
                                    <DIV id="divAmbito" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:36px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px;">
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
                                         <table id="tblSector" class="texto" style="width:260px;">
                                         <%=strHTMLSector%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td>
                                <fieldset style="width: 140px; height:60px; padding:5px;">
                                    <legend><label id="Label1" class="enlace" onclick="getPeriodo()">Periodo</label></legend>
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
                                         <table id="tblResponsable" class="texto" style="width:260px;">
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
                                         <table id="tblSegmento" class="texto" style="width:260px;">
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
                                         <table id="tblNaturaleza" class="texto" style="width:260px;">
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
                                         <table id="tblCliente" class="texto" style="width:260px;">
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
                                         <table id="tblModeloCon" class="texto" style="width:260px;">
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
                                         <table id="tblContrato" class="texto" style="width:260px;">
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
                                    <DIV id="divProyecto" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:36px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px;">
                                         <table id="tblProyecto" class="texto" style="width:260px;">
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
                                         <table id="tblHorizontal" class="texto" style="width:260px;">
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
                                         <table id="tblQn" class="texto" style="width:260px;">
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
                                         <table id="tblQ1" class="texto" style="width:260px;">
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
                                         <table id="tblQ2" class="texto" style="width:260px;">
                                         <%=strHTMLQ2%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="3" rowspan="2" style="padding-top:1px;">
                                <FIELDSET style="width: 290px; height:124px; padding:5px;">
                                    <LEGEND title="Magnitudes económicas a presentar">Valores</LEGEND>
                                    <DIV id="divValores" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:110px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT22.gif'); width:260px;">
                                             <table id="tblValores" class="texto" style="width:260px;">
                                                 <tr style="height:20px;" id="V1">
                                                    <td>
                                                        <asp:CheckBox id="chkV1" onclick="setCombo()" runat="server" style="vertical-align:middle; height:16px;" checked="true" />
                                                        Volumen de negocio
                                                    </td>
                                                 </tr>
                                                 <tr style="height:22px;" id="V2"><td><asp:CheckBox id="chkV2" onclick="setCombo()" runat="server" style="vertical-align:middle; height:16px;" checked="true" /> Ingresos</td></tr>
                                                 <tr style="height:22px;" id="V3"><td><asp:CheckBox id="chkV3" onclick="setCombo()" runat="server" style="vertical-align:middle; height:16px;" checked="true" /> Ingresos netos</td></tr>
                                                 <tr style="height:22px;" id="V4"><td><asp:CheckBox id="chkV4" onclick="setCombo()" runat="server" style="vertical-align:middle; height:16px;" checked="true" /> Gastos</td></tr>
                                                 <tr style="height:22px;" id="V5"><td><asp:CheckBox id="chkV5" onclick="setCombo()" runat="server" style="vertical-align:middle; height:16px;" checked="true" /> Margen</td></tr>
                                                 <tr style="height:22px;" id="V6"><td><asp:CheckBox id="chkV6" onclick="setCombo()" runat="server" style="vertical-align:middle; height:16px;" checked="true" /> Ratio</td></tr>
                                                 <tr style="height:22px;" id="V7"><td><asp:CheckBox id="chkV7" onclick="setCombo()" runat="server" style="vertical-align:middle; height:16px;" /> Cobros</td></tr>
                                                 <tr style="height:22px;" id="V8"><td><asp:CheckBox id="chkV8" onclick="setCombo()" runat="server" style="vertical-align:middle; height:16px;" /> Otros consumos</td></tr>
                                                 <tr style="height:22px;" id="V9"><td><asp:CheckBox id="chkV9" onclick="setCombo()" runat="server" style="vertical-align:middle; height:16px;" /> Consumo de profesionales</td></tr>
                                             </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                        </tr>                        
                        <tr>
                            <td colspan="2">
                                <FIELDSET id="fstCSN3P" runat="server" style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCSN3P" class="enlace" onclick="getCriterios(13)" runat="server">Q3</label><img id="Img12" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(13)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divQ3" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblQ3" class="texto" style="width:260px;">
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
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260;">
                                         <table id="tblQ4" class="texto" style="width:260px;">
                                         <%=strHTMLQ4%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>                        
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                            <td colspan="2">
                                <div id="divOnline2" style="width:170px; margin-left:50px;">
                                    <div align="center" style="background-image: url('../../../../Images/imgFondoCal3.gif'); background-repeat:no-repeat;
                                        width:90px; height:23px; position:relative; top:12px; left:10px; padding-top:5px;">
                                        Base de cálculo
                                    </div>
                                    <table border="0" cellpadding="0" cellspacing="0" class="texto" style="table-layout:fixed; width:140px">
                                        <tr>
                                            <td background="../../../../Images/Tabla/7.gif" height="6" width="6"></td>
                                            <td background="../../../../Images/Tabla/8.gif" height="6"></td>
                                            <td background="../../../../Images/Tabla/9.gif" height="6" width="6"></td>
                                        </tr>
                                        <tr>
                                            <td background="../../../../Images/Tabla/4.gif" width="6">
                                                &nbsp;</td>
                                            <td background="../../../../Images/Tabla/5.gif" style="padding-top:5px; padding-left:5px; vertical-align:middle;">
                                                <!-- Inicio del contenido propio de la página -->
                                                <asp:RadioButtonList ID="rdbResultadoCalculo2" SkinId="rbl" runat="server" RepeatColumns="2" style="margin-top:2px;" onclick="setResultadoOnline(2)">
                                                    <asp:ListItem Value="1" style="cursor:pointer; vertical-align:middle;" Selected title="Obtiene la información en base a cálculos online. Información actualizada pero mayor tiempo de respuesta.">Online&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="0" style="cursor:pointer; vertical-align:middle;" title="Obtiene la información en base a cálculos realizados a las 7 de la mañana. Menor tiempo de respuesta pero posibilidad de datos no actualizados.">7 AM</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <!-- Fin del contenido propio de la página -->
                                            </td>
                                            <td background="../../../../Images/Tabla/6.gif" width="6">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td background="../../../../Images/Tabla/1.gif" height="6" width="6"></td>
                                            <td background="../../../../Images/Tabla/2.gif" height="6"></td>
                                            <td background="../../../../Images/Tabla/3.gif" height="6" width="6"></td>
                                        </tr>
                                    </table>
                                </div>                            
                            </td>
                            <td colspan="3" style="text-align:left; vertical-align:middle;"><br />
                                <div id="divMonedaImportes2" runat="server" style="visibility:hidden;">
                                    <label id="lblLinkMonedaImportes2" class="enlace" onclick="getMonedaImportes()">Importes</label> en 
                                    <label id="lblMonedaImportes2" style="width:230px;" runat="server">Dólares americanos</label>
                                </div>
                            </td>
                        </tr>	
                        </table>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td background="../../../../Images/Tabla/6.gif" width="6">&nbsp;</td>
                </tr>
                <tr>
				    <td background="../../../../Images/Tabla/1.gif" height="6" width="6"></td>
                    <td background="../../../../Images/Tabla/2.gif" height="6"></td>
                    <td background="../../../../Images/Tabla/3.gif" height="6" width="6"></td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
</div>
<table id="tblGeneral" class="texto" border="0" style="height:20px; width:990px; margin-top:40px; margin-left:10px; margin-bottom:10px;">
    <tr>
        <td>
            <div id="divTablaTitulo" style="overflow-x:hidden; width:970px; height:17px; background-image: url('../../../../images/fondoEncabezamientoListas.gif');" runat="server">
            <table id="tblTitulo" style="width:1270px; height:17px">
            <colgroup>
                <col style="width:370px;" />
                <col style="width:100px;" />
                <col style="width:100px;" />
                <col style="width:100px;" />
                <col style="width:100px;" />
                <col style="width:100px;" />
                <col style="width:100px;" />
                <col style="width:100px;" />
                <col style="width:100px;" />
                <col style="width:100px;" />
            </colgroup>
	            <tr class="TBLINI" style="height:17px;">
					<td>&nbsp;Denominación</td>
					<td id="tituloV1" style="text-align:right;">Vol. negocio</td>
					<td id="tituloV2" style="text-align:right;">Ingresos</td>
					<td id="tituloV3" style="text-align:right;">Ingresos netos</td>
					<td id="tituloV4" style="text-align:right;">Gastos</td>
					<td id="tituloV5" style="text-align:right;">Margen</td>
					<td id="tituloV6" style="text-align:right;">Ratio</td>
					<td id="tituloV7" style="text-align:right;">Cobros</td>
					<td id="tituloV8" style="text-align:right;" title="Otros consumos">Otros cons.</td>
					<td id="tituloV9" style="text-align:right;">Consumo prof.</td>
	            </tr>
            </table>
            </div>
            <DIV id="divCatalogo" style="OVERFLOW-X:auto; overflow-y:scroll; width: 986px; height:460px;" onscroll="scrollTablaDR()">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:970px;">
                <%=strTablaHTML%>
                </div>
            </DIV>
            <DIV id="divResultado" class="TBLFIN" style="overflow-x:hidden; width: 970px; height:17px;" runat="server">
            <TABLE id="tblTotales" style="WIDTH:1270px; HEIGHT:17px; text-align:right;">
                <colgroup>
                    <col style="width:370px;" />
                    <col style="width:100px;" />
                    <col style="width:100px;" />
                    <col style="width:100px;" />
                    <col style="width:100px;" />
                    <col style="width:100px;" />
                    <col style="width:100px;" />
                    <col style="width:100px;" />
                    <col style="width:100px;" />
                    <col style="width:100px;" />
                </colgroup>
	            <TR class="TBLFIN">
                    <td>&nbsp;</td>
                    <td id="totalV1" runat="server" style="color:Black; text-align:right;">0,00</td>
                    <td id="totalV2" runat="server" style="color:Black; text-align:right;">0,00</td>
                    <td id="totalV3" runat="server" style="color:Black; text-align:right;">0,00</td>
                    <td id="totalV4" runat="server" style="color:Black; text-align:right;">0,00</td>
                    <td id="totalV5" runat="server" style="color:Black; text-align:right;">0,00</td>
                    <td id="totalV6" runat="server" style="color:Black; text-align:right;">0,00</td>
                    <td id="totalV7" runat="server" style="color:Black; text-align:right;">0,00</td>
                    <td id="totalV8" runat="server" style="color:Black; text-align:right;">0,00</td>
                    <td id="totalV9" runat="server" style="color:Black; text-align:right;">0,00</td>
	            </TR>
            </TABLE>
            </DIV>
        </td>
    </tr>
</table>
<div style="margin-left:10px;">
    <span id="imgLeySN4" style="display:none"><img class="ICO" src="../../../../Images/imgSN4.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4) %>&nbsp;&nbsp;</span>
    <nobr id="imgLeySN3" style="display:none"><img class="ICO" src="../../../../Images/imgSN3.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySN2" style="display:none"><img class="ICO" src="../../../../Images/imgSN2.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySN1" style="display:none"><img class="ICO" src="../../../../Images/imgSN1.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeyNodo" style="display:none"><img class="ICO" src="../../../../Images/imgNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySubNodo" style="display:none"><img class="ICO" src="../../../../Images/imgSubNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeyCli" style="display:none"><img class="ICO" src="../../../../Images/imgClienteICO.gif" />&nbsp;Cliente&nbsp;&nbsp;</nobr>
    <nobr id="imgLeyRes" style="display:none"><img class="ICO" src="../../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                  <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo&nbsp;&nbsp;</nobr>
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

