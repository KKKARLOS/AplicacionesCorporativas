<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">

    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    var nAnoMesActual = <%=nAnoMes %>;
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var strEstructuraSubnodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>";
    var nUtilidadPeriodo = <%=nUtilidadPeriodo.ToString() %>;
    var bRes1024 = <%=((bool)Session["AVANCE1024"]) ? "true":"false" %>;
    var sSubnodos = "<%=sSubnodos %>";
    var bHayPreferencia = <%=sHayPreferencia %>;
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;
    var nIDFicepiEntrada = <%=Session["IDFICEPI_ENTRADA"].ToString() %>;

    <%=sCriterios %>

</script>
<div id="divTiempos" style="width: 400px; position: absolute; top:135px; left: 600px; display:none;">
</div>
<div id="div1024" style="z-index: 105; width: 32px; height: 32px; position: absolute; top: 93px; right: 10px;">
    <asp:Image ID="img1024" CssClass="MA" runat="server" Height="32" width="32" ImageUrl="~/images/imgICO1024.gif" ondblclick="setResolucionPantalla()" ToolTip="Conmuta el tama�o de pantalla para adecuarla a la resoluci�n 1024x768 o 1280x1024" />
</div>
<div id="divOnline" align="left" style="width: 170px; position:absolute; top: 117px; left:205px; z-index:0;">
    <div align="center" style="background-image: url('../../../Images/imgFondoCal3.gif'); background-repeat:no-repeat;
        width: 90px; height: 23px; position: relative; top: 12px; left: 10px; padding-top: 5px;">
        Base de c�lculo</div>
    <table border="0" cellpadding="0" cellspacing="0" class="texto" style="table-layout:fixed; width:140px">
        <tr>
            <td background="../../../Images/Tabla/7.gif" height="6" width="6">
            </td>
            <td background="../../../Images/Tabla/8.gif" height="6">
            </td>
            <td background="../../../Images/Tabla/9.gif" height="6" width="6">
            </td>
        </tr>
        <tr>
            <td background="../../../Images/Tabla/4.gif" width="6">
                &nbsp;</td>
            <td background="../../../Images/Tabla/5.gif" style="padding-top:5px; padding-left:5px; vertical-align:middle;">
                <!-- Inicio del contenido propio de la p�gina -->
                <asp:RadioButtonList ID="rdbResultadoCalculo" SkinId="rbl" runat="server" RepeatColumns="2" style="margin-top:2px;" onclick="setResultadoOnline(1)">
                    <asp:ListItem Value="1" style="cursor:pointer;" Selected title="Obtiene la informaci�n en base a c�lculos online. Informaci�n actualizada pero mayor tiempo de respuesta.">Online&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="0" style="cursor:pointer;" title="Obtiene la informaci�n en base a c�lculos realizados a las 7 de la ma�ana. Menor tiempo de respuesta pero posibilidad de datos no actualizados.">7 AM</asp:ListItem>
                </asp:RadioButtonList>
                <!-- Fin del contenido propio de la p�gina -->
            </td>
            <td background="../../../Images/Tabla/6.gif" width="6">
                &nbsp;</td>
        </tr>
        <tr>
            <td background="../../../Images/Tabla/1.gif" height="6" width="6">
            </td>
            <td background="../../../Images/Tabla/2.gif" height="6">
            </td>
            <td background="../../../Images/Tabla/3.gif" height="6" width="6">
            </td>
        </tr>
    </table>
</div>
<div id="divMonedaImportes" runat="server" style="position:absolute; top:140px; left:393px; visibility:hidden">
    <label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()">Importes</label> en <label id="lblMonedaImportes" runat="server">D�lares americanos</label>
</div>
<%
    if (!(bool)Session["AVANCE1024"]){
%>
<center>
<%
    }
%>
<img id="imgPestHorizontalAux" src="../../../Images/imgPestHorizontal.gif" style="position:absolute; left:40px; top:125px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
<div id="divPestRetr" style="position:absolute; left:20px; top:125px; width:960px; height:440px; clip:rect(auto auto 0 auto)">
    <table style="width:960px;text-align:left">
    <tr>
        <td>
            <table class="texto" style="width:940px; height:440px; table-layout:fixed;" cellpadding="0" cellspacing="0">
                <tr>
		            <td background="../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../Images/Tabla/5.gif" style="padding: 5px">
                        <!-- Inicio del contenido propio de la p�gina -->
                        <table style="width:930px; text-align:left">
                        <colgroup>
                            <col style="width:310px;" />
                            <col style="width:155px;" />
                            <col style="width:155px;" />
                            <col style="width:155px;" />
                            <col style="width:55px;" />
                            <col style="width:100px;" />
                        </colgroup>
                        <tr>
                            <td style="padding-left:185px;">Estado<br /><asp:DropDownList id="cboEstado" runat="server" width="100px" onChange="setCombo()">
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                            <asp:ListItem Value="A" Text="Abierto"></asp:ListItem>
                            <asp:ListItem Value="C" Text="Cerrado"></asp:ListItem>
                            <asp:ListItem Value="H" Text="Hist�rico"></asp:ListItem>
                            <asp:ListItem Value="P" Text="Presupuestado"></asp:ListItem>
                            </asp:DropDownList>
                            </td>
                            <td>
                            Categor�a<br /><asp:DropDownList id="cboCategoria" runat="server" width="130px" onChange="setCombo()" CssClass="combo">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="P" Text="Producto"></asp:ListItem>
                                <asp:ListItem Value="S" Text="Servicio"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            Cualidad<br /><asp:DropDownList id="cboCualidad" runat="server" width="130px" onChange="setCombo()" CssClass="combo">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="C" Text="Contratante"></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Replicado con gesti�n"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td><img src='../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el cat�logo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom;">
                                <img border='0' src='../../../Images/imgCerrarAuto.gif' style="vertical-align: bottom; margin-left:30px;"
                                    title="Repliegue autom�tico de la pesta�a de criterios al obtener informaci�n">
                                <input id="chkCerrarAuto" runat="server" class="check" type="checkbox" checked />
                            </td>
                            <td>
                                <img src='../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la informaci�n autom�ticamente al cambiar el valor de alg�n criterio de selecci�n" style="vertical-align:bottom;">
                                <input type="checkbox" id="chkActuAuto" class="check" runat="server" />
                            </td>
                            <td>
                                <button id="btnObtener" type="button" onclick="buscar()" hidefocus="hidefocus" onmouseover="mostrarCursor(this)" runat="server" class="btnH25W90">
                                    <span><img src="../../../Images/imgObtener.gif" />&nbsp;Obtener</span>
                                </button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">�mbito</label><img id="Img14" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divAmbito" style="overflow:auto; overflow-x:hidden; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px">
                                         <table id="tblAmbito" class="texto" style="width:260px;">
                                         <%=strHTMLAmbito%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label5" class="enlace" onclick="getCriterios(6)" runat="server">Sector</label><img id="Img1" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(6)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divSector" style="overflow:auto; overflow-x:hidden; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblSector" class="texto" style="width:260px;">
                                         <%=strHTMLSector%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td>
                                <fieldset style="width: 140px; height:50px; visibility:hidden;">
                                    <legend><label id="Label1" class="enlace" onclick="getPeriodo()">Periodo</label></legend>
                                        Inicio&nbsp;<asp:TextBox ID="txtDesde" style="margin-left:5px; width:90px; vertical-align:middle;" Text="" readonly runat="server" />
                                        <asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" /><br />
                                        Fin&nbsp;<asp:TextBox ID="txtHasta" style="margin-left:15px; width:90px; vertical-align:middle;" Text="" readonly runat="server" />
                                        <asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset style="width: 130px; height:50px;">
                                    <legend title="Aplicable s�lo entre diferentes criterios">Operador l�gico</legend>
                                    <asp:RadioButtonList ID="rdbOperador" SkinId="rbli" runat="server" RepeatColumns="2" style="margin-top:8px;" onclick="setOperadorLogico(true)">
                                        <asp:ListItem Value="1" style="cursor:pointer" Selected><img src='../../../Images/imgY.gif' border='0' title="Criterios acumulados" style="cursor:pointer" hidefocus=hidefocus onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="0" style="cursor:pointer"><img src='../../../Images/imgO.gif' border='0' title="Criterios independientes" style="cursor:pointer" hidefocus=hidefocus onclick="this.parentNode.click()"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label2" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img2" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divResponsable" style="overflow:auto; overflow-x:hidden; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblResponsable" class="texto" style="width:260px;">
                                         <%=strHTMLResponsable%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label6" class="enlace" onclick="getCriterios(7)" runat="server">Segmento</label><img id="Img3" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(7)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divSegmento" style="overflow:auto; overflow-X:hidden; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblSegmento" class="texto" style="width:260px;">
                                         <%=strHTMLSegmento%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="3">
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label3" class="enlace" onclick="getCriterios(3)" runat="server">Naturaleza</label><img id="Img4" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(3)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divNaturaleza" style="overflow:auto; overflow-x:hidden; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblNaturaleza" class="texto" style="width:260px;">
                                         <%=strHTMLNaturaleza%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label7" class="enlace" onclick="getCriterios(8)" runat="server">Cliente</label><img id="Img5" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(8)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divCliente" style="overflow:auto; overflow-x:hidden; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblCliente" class="texto" style="width:260px;">
                                         <%=strHTMLCliente%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label4" class="enlace" onclick="getCriterios(4)" runat="server">Modelo de contrataci�n</label><img id="Img6" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(4)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divModeloCon" style="overflow:auto; overflow-x:hidden; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblModeloCon" class="texto" style="width:260px;">
                                         <%=strHTMLModeloCon%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="3">
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label8" class="enlace" onclick="getCriterios(9)" runat="server">Contrato</label><img id="Img7" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(9)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divContrato" style="overflow:auto; overflow-x:hidden; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblContrato" class="texto" style="width:260px;">
                                         <%=strHTMLContrato%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>                            
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label10" class="enlace" onclick="getCriterios(16)" runat="server">Proyecto</label><img id="Img15" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(16)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divProyecto" style="overflow:auto; overflow-x:hidden; width: 276px; height:36px;">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px">
                                         <table id="tblProyecto" class="texto" style="width:260px;">
                                         <%=strHTMLProyecto%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label9" class="enlace" onclick="getCriterios(5)" runat="server">Horizontal</label><img id="Img8" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(5)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divHorizontal" style="overflow:auto; overflow-x:hidden; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblHorizontal" class="texto" style="width:260px;">
                                         <%=strHTMLHorizontal%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="3">
                                <fieldset id="fstCDP" runat="server" style="width: 290px; height:50px;">
                                    <legend><label id="lblCDP" class="enlace" onclick="getCriterios(10)" runat="server">Qn</label><img id="Img9" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(10)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divQn" style="overflow:auto; overflow-x:hidden; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblQn" class="texto" style="width:260px;">
                                         <%=strHTMLQn%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset id="fstCSN1P" runat="server" style="width: 290px; height:50px;">
                                    <legend><label id="lblCSN1P" class="enlace" onclick="getCriterios(11)" runat="server">Q1</label><img id="Img10" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(11)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divQ1" style="overflow-X:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblQ1" class="texto" style="width:260px;">
                                         <%=strHTMLQ1%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset id="fstCSN2P" runat="server" style="width: 290px; height:50px;">
                                    <legend><label id="lblCSN2P" class="enlace" onclick="getCriterios(12)" runat="server">Q2</label><img id="Img11" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(12)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divQ2" style="overflow-X:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblQ2" class="texto" style="width:260px;">
                                         <%=strHTMLQ2%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="3">
                                <div id="divOnline2" align="left" style="width: 170px;">
                                    <div align="center" style="background-image: url('../../../Images/imgFondoCal3.gif'); background-repeat:no-repeat;
                                        width: 90px; height: 23px; position: relative; top: 12px; left: 10px; padding-top: 5px;">
                                        Base de c�lculo</div>
                                    <table border="0" cellpadding="0" cellspacing="0" class="texto" style="table-layout:fixed; width:140px">
                                        <tr>
                                            <td background="../../../Images/Tabla/7.gif" height="6" width="6">
                                            </td>
                                            <td background="../../../Images/Tabla/8.gif" height="6">
                                            </td>
                                            <td background="../../../Images/Tabla/9.gif" height="6" width="6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td background="../../../Images/Tabla/4.gif" width="6">
                                                &nbsp;</td>
                                            <td background="../../../Images/Tabla/5.gif" style="padding-top:5px; padding-left:5px; vertical-align:middle;">
                                                <!-- Inicio del contenido propio de la p�gina -->
                                                <asp:RadioButtonList ID="rdbResultadoCalculo2" SkinId="rbl" runat="server" RepeatColumns="2" style="margin-top:2px;" onclick="setResultadoOnline(2)">
                                                    <asp:ListItem Value="1" style="cursor:pointer;" Selected title="Obtiene la informaci�n en base a c�lculos online. Informaci�n actualizada pero mayor tiempo de respuesta.">Online&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="0" style="cursor:pointer;" title="Obtiene la informaci�n en base a c�lculos realizados a las 7 de la ma�ana. Menor tiempo de respuesta pero posibilidad de datos no actualizados.">7 AM</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <!-- Fin del contenido propio de la p�gina -->
                                            </td>
                                            <td background="../../../Images/Tabla/6.gif" width="6">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td background="../../../Images/Tabla/1.gif" height="6" width="6">
                                            </td>
                                            <td background="../../../Images/Tabla/2.gif" height="6">
                                            </td>
                                            <td background="../../../Images/Tabla/3.gif" height="6" width="6">
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset id="fstCSN3P" runat="server" style="width: 290px; height:50px;">
                                    <legend><label id="lblCSN3P" class="enlace" onclick="getCriterios(13)" runat="server">Q3</label><img id="Img12" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(13)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divQ3" style="overflow-X:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblQ3" class="texto" style="width:260px;">
                                         <%=strHTMLQ3%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset id="fstCSN4P" runat="server" style="width: 290px; height:50px;">
                                    <legend><label id="lblCSN4P" class="enlace" onclick="getCriterios(14)" runat="server">Q4</label><img id="Img13" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(14)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divQ4" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblQ4" class="texto" style="width:260px;">
                                         <%=strHTMLQ4%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="3">
                                <div id="divMonedaImportes2" runat="server" style="visibility:hidden">
                                    <label id="lblLinkMonedaImportes2" class="enlace" style="vertical-align:bottom" onclick="getMonedaImportes()">Importes</label>&nbsp;&nbsp;<label style="vertical-align:top"> en </label>&nbsp;&nbsp;<label id="lblMonedaImportes2" style="vertical-align:top" runat="server">D�lares americanos</label>
                                </div>
                            </td>
                        </tr>
                        </table>
                        <!-- Fin del contenido propio de la p�gina -->
                    </td>
                    <td background="../../../Images/Tabla/6.gif" width="6">
                        &nbsp;</td>
                </tr>
                <tr>
				    <td background="../../../Images/Tabla/1.gif" height="6" width="6">
				    </td>
                    <td background="../../../Images/Tabla/2.gif" height="6">
                    </td>
                    <td background="../../../Images/Tabla/3.gif" height="6" width="6">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
</div>
<%
    if (!(bool)Session["AVANCE1024"]){
%>
</center>
<%
    }
%>
<br /><br />
<table id="tblGeneral" style="margin-top:10px; margin-left:10px; width:1230px; text-align:left" cellpadding="0" border="0">
<colgroup>
	<col style="width:375px"/>
	<col style="width:855px"/>
</colgroup>	
    <tr>
        <td style="vertical-align:bottom;" align="left">
            <div id="divTituloFijo" style="width: 375px;" runat="server">
            <table id='tblTituloFijo' style="width: 375px; height: 34px; z-index:5;">
                <colgroup>
                    <col style="width:120px"/>
                    <col style="width:255px"/>                    
                </colgroup>
	            <tr class="texto" align="center">
                    <td colspan="2" align="left">
                        <img title="Mes anterior" onclick="cambiarMes(-1)" src="../../../Images/btnAntRegOff.gif" style="cursor: pointer;vertical-align:bottom" />
                        <asp:TextBox ID="txtMesVisible" style="width:90px; text-align:center; vertical-align:super" readonly=true runat="server" Text=""></asp:TextBox>
                        <img title="Siguiente mes" onclick="cambiarMes(1)" src="../../../Images/btnSigRegOff.gif" style="cursor: pointer;vertical-align:bottom" />
                        &nbsp;&nbsp;&nbsp;
                    </td>
	            </tr>
	            <tr id="tblTitulo" class="TBLINI" align="center">
					<td style="text-align:right;"><IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblBodyFijo',3,'divTituloFijo','imgLupa1',event,'setBuscarDescriFija()')"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"><IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblBodyFijo',3,'divTituloFijo','imgLupa1','setBuscarDescriFija()')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
							&nbsp;N�&nbsp;&nbsp;
					</td>
					<td style="text-align:left;">&nbsp;&nbsp;Proyecto<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblBodyFijo',4,'divTituloFijo','imgLupa2','setBuscarDescriFija()')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"><IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblBodyFijo',4,'divTituloFijo','imgLupa2',event,'setBuscarDescriFija()')"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
					</td>                    
	            </tr>
            </table>
            </div>
		</td>
		<td style="vertical-align:bottom;" align="left">
            <div id="divTituloMovil" style="overflow:hidden; width: 846px;" runat="server">
            <table id="tblTituloMovil" style="width: 1185px; height: 34px; z-index:5;">
                <colgroup>                   
                    <col style="width:70px"/>
                    <col style="width:60px"/>                    
                    <col style="width:60px"/>
                    <col style="width:100px"/>
                    
                    <col style="width:60px"/>
                    <col style="width:65px"/>
                    <col style="width:65px"/>
                    <col style="width:65px"/>
                    <col style="width:60px"/>
                    
                    <col style="width:70px"/>
                    <col style="width:70px"/>
                    <col style="width:60px"/>
                    <col style="width:40px"/>
                    
                    <col style="width:40px"/>
                    <col style="width:100px"/>
                    
                    <col style="width:80px"/>
                    <col style="width:80px"/>
                    <col style="width:40px"/>
                </colgroup>
	            <tr class="texto" align="center">
                    <td colspan="4" class="colTabla">Planificado</td>
                    <td colspan="5" class="colTabla">IAP</td>
                    <td colspan="4" class="colTabla">Previsto</td>
                    <td colspan="2" class="colTabla">Avance</td>
                    <td colspan="3" class="colTabla1">Econ�mico</td>
	            </tr>
	            <tr id="tblTitulo2" class="TBLINI" align="center">                   
                    <td>Total</td>
                    <td>Inicio</td>
                    <td>Fin</td>
                    <td><label id="lblPresupuesto" title="Importe presupuestado" style="width:100px">Imp. Presup.</label></td>
                    
                    <td>Mes</td>
                    <td title="Acumulado">Acumul.</td>
                    <td title="Pendiente estimado. Equivale a la suma de las estimaciones menos las imputaciones a tareas con estimaci�n.">Pend. Est.</td>
                    <td title="Total estimado">Total Est.</td>
                    <td title="Fin estimado">Fin Est.</td>
                    
                    <td>Total</td>
                    <td>Pendiente</td>
                    <td>Fin</td>
                    <td>%</td>		                    
                    <td>%</td>		
                    <td><label id="lblProducido" title="Importe producido" style="width:100px">Imp. Produc.</label></td>                    
                    <td title="Producido hasta el mes seleccionado">Prod. a mes</td>
                    <td title="Total proyecto">Total proy.</td>
                    <td title="% Desviaci�n ">%</td>
	            </tr>
            </table>
            </div>
		</td>
	</tr>	
	<tr>
		<td style="vertical-align:top;">
			<div id="divBodyFijo" style="width:375px; height:660px; overflow:hidden;" runat="server">
				<div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:375px">
                <%=strTblBodyFijoHTML%>
				</div>
			</div>
            <table id="tblResultado" style="width: 375px ;height:16px;">
	            <tr class="TBLFIN" style="height:16px;">
		            <td>&nbsp;</td>
	            </tr>
            </table>			
		</td>
		<td style="vertical-align:top;">
			<div id="divBodyMovil" style="width:860px; height:676px; overflow-x:scroll;overflow-y:scroll;" runat="server" onscroll="setScroll();scrollTablaProy();">
				<div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:1185px">
                <%=strTblBodyMovilHTML%>
				</div>
			</div>
		</td>
	</tr>	
</table>
<br />
<div style="position:absolute; bottom:5; margin-left:15px">
    <table style="width:940px">
        <colgroup>
            <col style="width:100px" />
            <col style="width:90px" />
            <col style="width:210px" />
            <col style="width:540px" />
        </colgroup>
          <tr> 
            <td><img class="ICO" src="../../../Images/imgProducto.gif" />Producto</td>
            <td><img class="ICO" src="../../../Images/imgServicio.gif" />Servicio</td>
            <td></td>
            <td rowspan="3">
            </td>
          </tr>
          <tr>
            <td><img class="ICO" src="../../../Images/imgIconoContratante.gif" />Contratante</td>
            <td colspan="2"><img class="ICO" src="../../../Images/imgIconoRepPrecio.gif" />Replicado con gesti�n propia</td>
          </tr>
          <tr>
            <td><img class="ICO" src="../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto</td>
                <td><img class="ICO" src="../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />Cerrado</td>
                <td><img class="ICO" src="../../../Images/imgIconoProyHistorico.gif" title='Proyecto hist�rico' />Hist�rico&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <img class="ICO" src="../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />Presupuestado
            </td>
          </tr>
    </table>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			switch (strBoton){
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
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("AvanceGlobal.pdf");
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

</script>
</asp:Content>

