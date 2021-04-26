<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" EnableViewState="False" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">

    <script type="text/javascript">

    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
    var sSubnodos = "<%=sSubnodos %>";
    var bHayPreferencia = <%=sHayPreferencia %>;
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;
    
    <%=sCriterios %>

</script>
<br /><br />
<img id="imgPestHorizontalAux" src="../../../../Images/imgPestHorizontal.gif" style="Z-INDEX: 0;position:absolute; left:40px; top:98px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
<div id="divPestRetr" style="position:absolute; left:20px; top:98px; width:960px; height:500px; clip:rect(auto auto 0 auto)">
    <table style="width:960px; table-layout:fixed;" cellpadding="0">
    <tr style="vertical-align:top">
        <td>
            <table class="texto" style="width:940px; height:500px; table-layout:fixed;" cellpadding="0" >
                <tr>
		            <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../../Images/Tabla/5.gif" style="padding: 5px">
                        <!-- Inicio del contenido propio de la página -->
                        <table class="texto" style="width:930px;">
                        <colgroup>
                            <col style="width:100px;" />
                            <col style="width:210px;" />
                            <col style="width:155px;" />
                            <col style="width:155px;" />
                            <col style="width:155px;" />
                            <col style="width:55px;" />
                            <col style="width:100px;" />
                        </colgroup>
                        <tr>
                            <td></td>
                            <td>Estado<br />
                                <asp:DropDownList id="cboEstado" runat="server" CssClass="combo">
                                    <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="A" Text="Abierto"></asp:ListItem>
                                    <asp:ListItem Value="C" Text="Cerrado"></asp:ListItem>
                                    <asp:ListItem Value="H" Text="Histórico"></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Presupuestado"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                Categoría<br />
                                <asp:DropDownList id="cboCategoria" CssClass="combo" runat="server" onChange="setCombo()">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Producto"></asp:ListItem>
                                    <asp:ListItem Value="S" Text="Servicio"></asp:ListItem>
                                </asp:DropDownList>
                                
                            </td>
                            <td>
                                Cualidad<br />
                                <asp:DropDownList id="cboCualidad" runat="server" onChange="setCombo()" CssClass="combo">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="C" Text="Contratante"></asp:ListItem>
                                    <asp:ListItem Value="J" Text="Replicado sin gestión"></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Replicado con gestión"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td><img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom;">
                                <img border='0' src='../../../../Images/imgCerrarAuto.gif' style="vertical-align: bottom; margin-left:30px;"
                                    title="Repliegue automático de la pestaña de criterios al obtener información">
                                <input id="chkCerrarAuto" runat="server" class="check" type="checkbox" checked="checked" />
                            </td>
                            <td>
                                <img src='../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom;">
                                <input type="checkbox" id="chkActuAuto" class="check" runat="server" />
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
                                    <LEGEND><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label>
                                    <img id="Img14" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:7px;"></LEGEND>
                                    <DIV id="divAmbito" style="overflow-y:auto; OVERFLOW-X:hidden; WIDTH: 276px; height:36px; margin-top:2px">
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
                                    <DIV id="divSector" style="overflow-y:auto; OVERFLOW-X:hidden; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblSector" class="texto" style="width:260px;">
                                         <%=strHTMLSector%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td>
                                <FIELDSET style="width: 140px; height:60px; padding:5px;">
                                    <LEGEND>Año</LEGEND>
                                    <table style="margin-top:7px; margin-left:20px;">
                                    <tr>
                                        <td>
                                            <img title="Año anterior" onclick="setAnno('A')" src="../../../../Images/btnAntRegOff.gif" style="cursor: pointer" />
                                        </td> 
                                        <td>   
                                            <asp:TextBox id="txtAnno" style="width:32px; text-align:center;" readonly="true" runat="server" Text=""></asp:TextBox>
                                        </td> 
                                        <td>   
                                            <img title="Siguiente año" onclick="setAnno('S')" src="../../../../Images/btnSigRegOff.gif" style="cursor: pointer" />
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
                            <td colspan="3" style="padding-top:1px;">
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
                                <FIELDSET id="fstCSN4P" runat="server" style="width: 290px; height:60px; padding:5px; visibility:hidden;">
                                    <LEGEND><label id="lblCSN4P" class="enlace" onclick="getCriterios(14)" runat="server">Q4</label><img id="Img13" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(14)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divQ4" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblQ4" class="texto" style="width:260px;">
                                         <%=strHTMLQ4%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="3"></td>
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

<table id="tblGeneral" class="texto" style="width:1000px; visibility:hidden;">
    <colgroup>
        <col style="width:495px" />
        <col style="width:505px" />
    </colgroup>
    <tr>
        <td>
            <asp:CHART id="Chart1" runat="server" Palette="BrightPastel" Visible="false"
                BackColor="243, 223, 193" Width="495px" Height="250px" BorderDashStyle="Solid" 
                BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1" BorderlineColor="#1A3B69" BorderlineDashStyle="Solid" 
                BorderlineWidth="2" ImageStorageMode="UseImageLocation" ImageLocation="~/TempImagesGraficos/ChartPic_#SEQ(300,3)">
                <legends>
                    <asp:legend LegendStyle="Row" IsTextAutoFit="False" DockedToChartArea="ChartArea1" Docking="Bottom" 
                        IsDockedInsideChartArea="False" Name="Default" BackColor="Transparent" Font="Arial, 8pt, style=Bold" Alignment="Center">
                    </asp:legend>
                </legends>
                <BorderSkin SkinStyle="Emboss" BackImageTransparentColor="Transparent" BorderWidth="0" PageColor="236, 240, 238" />
                <chartareas>
                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="OldLace" 
                        ShadowColor="Transparent" BackGradientStyle="TopBottom">
                        <area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
                        <axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
                            <LabelStyle Font="Arial, 8.25pt, style=Bold" Format="C0" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </axisy>
                        <axisx LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8">
                            <LabelStyle Font="Arial, 8.25pt, style=Bold" IsEndLabelVisible="False" IsStaggered="False" Interval="1" IntervalOffset="NotSet" TruncatedLabels="True" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </axisx>
                    </asp:ChartArea>
                </chartareas>
            </asp:CHART>
        </td>
        <td>
            <asp:CHART id="Chart2" runat="server" Palette="BrightPastel" Visible="false" 
                BackColor="#F3DFC1" Width="505px" Height="250px" BorderDashStyle="Solid"
                BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1" BorderlineColor="#1A3B69" BorderlineDashStyle="Solid" 
                BorderlineWidth="2" ImageStorageMode="UseImageLocation" ImageLocation="~/TempImagesGraficos/ChartPic_#SEQ(300,3)">
                <legends>
                    <asp:legend LegendStyle="Row" IsTextAutoFit="False" DockedToChartArea="ChartArea1" Docking="Bottom" 
                        IsDockedInsideChartArea="False" Name="Default" BackColor="Transparent" Font="Arial, 8.25pt, style=Bold" Alignment="Center">
                    </asp:legend>
                </legends>
                <BorderSkin SkinStyle="Emboss" BackImageTransparentColor="Transparent" BorderWidth="0" PageColor="236, 240, 238" />
                <chartareas>
                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="OldLace" 
                        ShadowColor="Transparent" BackGradientStyle="TopBottom">
                        <area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
                        <axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
                            <LabelStyle Font="Arial, 8.25pt, style=Bold" Format="C0" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </axisy>
                        <axisx LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
                            <LabelStyle Font="Arial, 8.25pt, style=Bold" IsEndLabelVisible="False" IsStaggered="False" Interval="1" IntervalOffset="NotSet" TruncatedLabels="True" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </axisx>
                    </asp:ChartArea>
                </chartareas>
            </asp:CHART>
        </td>
    </tr>
    <tr>
        <td>
            <asp:CHART id="Chart3" oncustomizelegend="Chart3_CustomizeLegend" runat="server" Palette="BrightPastel" BackColor="#F3DFC1" 
                    Visible="false" Width="495px" Height="250px" BorderDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2" 
                    BorderColor="181, 64, 1" BorderlineColor="#1A3B69" BorderlineDashStyle="Solid" BorderlineWidth="2" 
                    ImageStorageMode="UseImageLocation" ImageLocation="~/TempImagesGraficos/ChartPic_#SEQ(300,3)">
                <legends>
                    <asp:legend LegendStyle="Row" IsTextAutoFit="False" DockedToChartArea="ChartArea1" Docking="Bottom" 
                        IsDockedInsideChartArea="False" Name="Default" BackColor="Transparent" Font="Arial, 8.25pt, style=Bold" Alignment="Center">
                    </asp:legend>
                </legends>
                <BorderSkin SkinStyle="Emboss" BackImageTransparentColor="Transparent" BorderWidth="0" PageColor="236, 240, 238" />
                <chartareas>
                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                        <area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
                        <axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
                            <LabelStyle Font="Arial, 8.25pt, style=Bold" Format="C0" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </axisy>
                        <axisx LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
                            <LabelStyle Font="Arial, 8.25pt, style=Bold" IsEndLabelVisible="False" IsStaggered="False" Interval="1" 
                                IntervalOffset="NotSet" TruncatedLabels="True" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </axisx>
                    </asp:ChartArea>
                </chartareas>
            </asp:CHART>
        </td>
        <td>
            <asp:CHART id="Chart4" oncustomizelegend="Chart4_CustomizeLegend" runat="server" Palette="BrightPastel" BackColor="#F3DFC1" Visible="false" 
                Width="505px" Height="250px" BorderDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1" 
                BorderlineColor="#1A3B69" BorderlineDashStyle="Solid" BorderlineWidth="2" ImageStorageMode="UseImageLocation" 
                ImageLocation="~/TempImagesGraficos/ChartPic_#SEQ(300,3)">
                <legends>
                    <asp:legend LegendStyle="Row" IsTextAutoFit="False" DockedToChartArea="ChartArea1" Docking="Bottom" IsDockedInsideChartArea="False" 
                        Name="Default" BackColor="Transparent" Font="Arial, 8.25pt, style=Bold" Alignment="Center"></asp:legend>
                </legends>
                <BorderSkin SkinStyle="Emboss" BackImageTransparentColor="Transparent" BorderWidth="0" PageColor="236, 240, 238" />
                <chartareas>
                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                        <area3dstyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False" WallWidth="0" IsClustered="False" />
                        <axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
                            <LabelStyle Font="Arial, 8.25pt, style=Bold" Format="C0" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </axisy>
                        <axisx LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
                            <LabelStyle Font="Arial, 8.25pt, style=Bold" IsEndLabelVisible="False" IsStaggered="False" Interval="1" 
                                IntervalOffset="NotSet" TruncatedLabels="True" />
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </axisx>
                    </asp:ChartArea>
                </chartareas>
            </asp:CHART>
        </td>
    </tr>
</table>
<input id="hdnCriterios" type="hidden" runat="server" />
<input id="hdnReponerCri" type="hidden" runat="server" />
<input id="hdnInicio" type="hidden" runat="server" value="T" />
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

