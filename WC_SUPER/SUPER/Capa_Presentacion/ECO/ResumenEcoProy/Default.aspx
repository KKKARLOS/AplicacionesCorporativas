<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" EnableEventValidation="false"  ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<style type="text/css">
.bordeI {border-left: solid 1px #A6C3D2; padding-left:1px;}
.bordeD {border-right: solid 1px #A6C3D2; padding-right:1px;}
</style>
<script type="text/javascript">
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var strEstructuraSubnodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>";
    var bRes1024 = <%=((bool)Session["RESUMEN1024"]) ? "true":"false" %>;
    var nUtilidadPeriodo = <%=nUtilidadPeriodo.ToString() %>;
    var sSubnodos = "<%=sSubnodos %>";
    var bHayPreferencia = <%=sHayPreferencia %>;
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;

    <%=sCriterios %>
</script>
<div id="div1024" style="Z-INDEX: 105; width: 32px; HEIGHT: 32px; POSITION: absolute; TOP: 93px; right: 10px;">
    <asp:Image ID="img1024" CssClass="MA" runat="server" Height="32" Width="32" ImageUrl="~/images/imgICO1024.gif" ondblclick="setResolucionPantalla()" ToolTip="Conmuta el tamaño de pantalla para adecuarla a la resolución 1024x768 o 1280x1024" />
</div>
<div id="divOnline" align="left" style="width: 170px; position:absolute; top: 115px; left:205px;">
    <div align="center" style="background-image: url('../../../Images/imgFondoCal3.gif');background-repeat:no-repeat;
        width: 90px; height: 23px; position: relative; top: 12px; left: 10px; padding-top: 5px;">
        Base de cálculo</div>
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
                <!-- Inicio del contenido propio de la página -->
                <asp:RadioButtonList ID="rdbResultadoCalculo" SkinId="rbl" runat="server" RepeatColumns="2" style="margin-top:2px;" onclick="setResultadoOnline()">
                    <asp:ListItem Value="1" selected title="Obtiene la información en base a cálculos online. Información actualizada pero mayor tiempo de respuesta.">Online&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                    <asp:ListItem Value="0" title="Obtiene la información en base a cálculos realizados a las 7 de la mañana. Menor tiempo de respuesta pero posibilidad de datos no actualizados.">7 AM</asp:ListItem>
                </asp:RadioButtonList>
                <!-- Fin del contenido propio de la página -->
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
<div id="divMonedaImportes" runat="server" style="position:absolute; top:155px; left:400px; visibility:hidden">
    <label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()">Importes</label> en <label id="lblMonedaImportes" runat="server">Dólares americanos</label>
</div>
<img id="imgPestHorizontalAux" src="../../../Images/imgPestHorizontal.gif" style="Z-INDEX:0; position:absolute; left:40px; top:125px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
<div id="divPestRetr" style="position:absolute; left:20px; top:125px; width:960px; height:440px; clip:rect(auto auto 0 auto)">
    <table style="width:960px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
    <tr>
        <td>
            <table class="texto" style=" width:940px; height:440px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                <tr>
		            <td background="../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../Images/Tabla/5.gif" style="padding: 5px">
                        <!-- Inicio del contenido propio de la página -->
                        <table class="texto" style="width:930px;">
                        <colgroup>
                            <col style="width:310px;" />
                            <col style="width:155px;" />
                            <col style="width:155px;" />
                            <col style="width:155px;" />
                            <col style="width:55px;" />
                            <col style="width:100px;" />
                        </colgroup>
                        <tr>
                            <td style="padding-left:185px;">Estado<br /><asp:DropDownList id="cboEstado" runat="server" Width="100px" onChange="setCombo()">
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
                            Cualidad<br /><asp:DropDownList id="cboCualidad" runat="server" Width="130px" onChange="setCombo()" CssClass="combo">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="C" Text="Contratante"></asp:ListItem>
                                    <asp:ListItem Value="J" Text="Replicado sin gestión"></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Replicado con gestión"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td><img src='../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom;">
                                <img border='0' src='../../../Images/imgCerrarAuto.gif' style="vertical-align: bottom; margin-left:30px;"
                                    title="Repliegue automático de la pestaña de criterios al obtener información">
                                <input id="chkCerrarAuto" runat="server" class="check" type="checkbox" checked />
                            </td>
                            <td>
                                <img src='../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom;">
                                <input type=checkbox id="chkActuAuto" class="check" runat="server" />
                            </td>
                            <td align="left">
                                <button id="btnObtener" type="button" onclick="buscar()" hidefocus="hidefocus" onmouseover="mostrarCursor(this)" runat="server" class="btnH25W90">
                                    <span><img src="../../../Images/imgObtener.gif" />&nbsp;Obtener</span>
                                </button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label><img id="Img14" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divAmbito" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px">
                                         <table id="tblAmbito" style="width:260px;">
                                         <%=strHTMLAmbito%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label5" class="enlace" onclick="getCriterios(6)" runat="server">Sector</label><img id="Img1" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(6)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divSector" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblSector" style="width:260px;">
                                         <%=strHTMLSector%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td>
                                <fieldset style="width: 140px; height:50px;">
                                    <legend><label id="Label1" class="enlace" onclick="getPeriodo()">Periodo</label></legend>
                                        Inicio&nbsp;<asp:TextBox ID="txtDesde" style="margin-left:5px;width:90px; vertical-align:middle;" Text="" readonly="true" runat="server" />
                                        <asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" /><br />
                                        Fin&nbsp;<asp:TextBox ID="txtHasta" style="margin-left:15px; width:90px; vertical-align:middle;" Text="" readonly="true" runat="server" />
                                        <asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset style="width: 130px; height:50px;">
                                    <legend title="Aplicable sólo entre diferentes criterios">Operador lógico</legend>
                                    <asp:RadioButtonList ID="rdbOperador" SkinId="rbli" runat="server" RepeatColumns="2" style="margin-top:8px;" onclick="setOperadorLogico(true)">
                                        <asp:ListItem Value="1" Selected><img src='../../../Images/imgY.gif' border='0' title="Criterios acumulados" style="cursor:pointer" hidefocus=hidefocus onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="0"><img src='../../../Images/imgO.gif' border='0' title="Criterios independientes" style="cursor:pointer" hidefocus=hidefocus onclick="this.parentNode.click()"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label2" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img2" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divResponsable" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblResponsable" style="width:260px;">
                                         <%=strHTMLResponsable%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label6" class="enlace" onclick="getCriterios(7)" runat="server">Segmento</label><img id="Img3" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(7)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divSegmento" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblSegmento" style="width:260px;">
                                         <%=strHTMLSegmento%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="3">
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label3" class="enlace" onclick="getCriterios(3)" runat="server">Naturaleza</label><img id="Img4" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(3)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divNaturaleza" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblNaturaleza" style="width:260px;">
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
                                    <div id="divCliente" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblCliente" style="width:260px;">
                                         <%=strHTMLCliente%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label4" class="enlace" onclick="getCriterios(4)" runat="server">Modelo de contratación</label><img id="Img6" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(4)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divModeloCon" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblModeloCon" style="width:260px;">
                                         <%=strHTMLModeloCon%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="3">
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label8" class="enlace" onclick="getCriterios(9)" runat="server">Contrato</label><img id="Img7" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(9)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divContrato" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblContrato" style="width:260px;">
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
                                    <div id="divProyecto" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px">
                                         <table id="tblProyecto" style="width:260px;">
                                         <%=strHTMLProyecto%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label9" class="enlace" onclick="getCriterios(5)" runat="server">Horizontal</label><img id="Img8" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(5)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divHorizontal" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblHorizontal" style="width:260px;">
                                         <%=strHTMLHorizontal%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="3">
                                <fieldset id="fstCDP" runat="server" style="width: 290px; height:50px;">
                                    <legend><label id="lblCDP" class="enlace" onclick="getCriterios(10)" runat="server">Qn</label><img id="Img9" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(10)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divQn" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblQn" style="width:260px;">
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
                                    <div id="divQ1" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblQ1" style="width:260px;">
                                         <%=strHTMLQ1%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset id="fstCSN2P" runat="server" style="width: 290px; height:50px;">
                                    <legend><label id="lblCSN2P" class="enlace" onclick="getCriterios(12)" runat="server">Q2</label><img id="Img11" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(12)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divQ2" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblQ2" style="width:260px;">
                                         <%=strHTMLQ2%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset id="fstCSN3P" runat="server" style="width: 290px; height:50px;">
                                    <legend><label id="lblCSN3P" class="enlace" onclick="getCriterios(13)" runat="server">Q3</label><img id="Img12" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(13)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divQ3" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblQ3" style="width:260px;">
                                         <%=strHTMLQ3%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset id="fstCSN4P" runat="server" style="width: 290px; height:50px;">
                                    <legend><label id="lblCSN4P" class="enlace" onclick="getCriterios(14)" runat="server">Q4</label><img id="Img13" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(14)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divQ4" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblQ4" style="width:260px;">
                                         <%=strHTMLQ4%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="3">
                            </td>
                        </tr>
                        </table>
                        <!-- Fin del contenido propio de la página -->
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
<table id="tblGeneral" style="width:1250px; margin-top:50px; text-align:left;">
    <tr>
        <td>
            <table id="tblTitulo" style="width: 1230px; height:17px;" border="0">
                <colgroup>
                    <col style="width:115px;" />
                    <col style="width:256px;" />
                    <col style="width:74px;" />
                    <col style="width:245px;" />
                    <col style="width:90px;" />
                    <col style="width:90px;" />
                    <col style="width:90px;" />
                    <col style="width:90px;" />
                    <col style="width:90px;" />
                    <col style="width:90px;" />
                </colgroup>
	            <tr class="TBLINI">
					<td style="text-align:right;">
					    <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa1',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						<img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa1')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						<img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					    <map name="img1">
					        <area onclick="ot('tblDatos', 3, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					        <area onclick="ot('tblDatos', 3, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				        </map>&nbsp;Nº&nbsp;&nbsp;
					</td>
					<td style="text-align:left;"><img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
						    <map name="img2">
						        <area onclick="ot('tblDatos', 4, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						        <area onclick="ot('tblDatos', 4, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					        </map>&nbsp;Proyecto&nbsp;<img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa2')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa2',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
					</td>
                    <td>&nbsp;
                        <img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img3" border="0">
					    <map name="img3">
					        <area onclick="ot('tblDatos', 5, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					        <area onclick="ot('tblDatos', 5, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				        </map><label id="lblContrato" title="Contrato">Contrato</label>
                    </td>
                    <td><img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img4" border="0">
						    <map name="img4">
						        <area onclick="ot('tblDatos', 6, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						        <area onclick="ot('tblDatos', 6, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					        </map><label id="lblCliente" title="Cliente">Cliente</label>
                    </td>
                    <td title="Producción externa" style="text-align:right; padding-right:2px;">
                        <img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img5" border="0">
					    <map name="img5">
					        <area onclick="ot('tblDatos', 7, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					        <area onclick="ot('tblDatos', 7, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				        </map>&nbsp;Prod. Exter.</td>
                    <td title="Producción interna" style="text-align:right; padding-right:2px;">
                        <img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img6" border="0">
					    <map name="img6">
					        <area onclick="ot('tblDatos', 8, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					        <area onclick="ot('tblDatos', 8, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				        </map>&nbsp;Prod. Inter.</td>
                    <td style="text-align:right; padding-right:2px;">
                        <img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img7" border="0">
					    <map name="img7">
					        <area onclick="ot('tblDatos', 9, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					        <area onclick="ot('tblDatos', 9, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				        </map>&nbsp;Consumo</td>
                    <td style="text-align:right; padding-right:2px;">
                        <img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img8" border="0">
					    <map name="img8">
					        <area onclick="ot('tblDatos', 10, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					        <area onclick="ot('tblDatos', 10, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				        </map>&nbsp;Ing. netos</td>
                    <td style="text-align:right; padding-right:2px;">
                        <img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img9" border="0">
					    <map name="img9">
					        <area onclick="ot('tblDatos', 11, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					        <area onclick="ot('tblDatos', 11, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				        </map>&nbsp;Margen</td>
                    <td style="text-align:right; padding-right:2px;">
                        <img style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img10" border="0">
					    <map name="img10">
					        <area onclick="ot('tblDatos', 12, 0, 'por', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					        <area onclick="ot('tblDatos', 12, 1, 'por', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				        </map>&nbsp;Rentabilidad
                    </td>
	            </tr>
            </table>
            <div id="divCatalogo" style="overflow-x:hidden; overflow-y:auto; width: 1246px; height:660px;" onscroll="scrollTablaProy();">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:1230px">
                    <%=strTablaHTML%>
                </div>
            </div>
            <table id="tblTotales" style="width: 1230px; height: 17px; margin-bottom:3px; text-align:right;">
                <colgroup>
                    <col style="width:108px;"/>
                    <col style="width:265px;"/>
                    <col style="width:70px;"/>
                    <col style="width:245px;"/>
                    <col style="width:91px;"/>
                    <col style="width:90px;"/>
                    <col style="width:90px;"/>
                    <col style="width:90px;"/>
                    <col style="width:91px;"/>
                    <col style="width:90px;"/>
                </colgroup>
	            <tr class="TBLFIN">
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="bordeD">&nbsp;</td>
                    <td id="totProdExt" runat="server" class="bordeD" style="padding-left:2px;">0,00</td>
                    <td id="totProdInt" runat="server" class="bordeD" style="padding-left:2px;">0,00</td>
                    <td id="totConsumo" runat="server" class="bordeD" style="padding-left:2px;">0,00</td>
                    <td id="totIngNetos" runat="server" class="bordeD" style="padding-left:2px;">0,00</td>
                    <td id="totMargen" runat="server" class="bordeD" style="padding-left:2px;">0,00</td>
                    <td id="totRentabilidad" runat="server" class="bordeD" style="padding-left:2px;">0,00</td>
	            </tr>
            </table>
            <table style="width:700px;">
                <colgroup>
                    <col style="width:100px" />
                    <col style="width:90px" />
                    <col style="width:510px" />
                </colgroup>
	              <tr> 
	                <td><img class="ICO" src="../../../Images/imgProducto.gif" />Producto</td>
                    <td><img class="ICO" src="../../../Images/imgServicio.gif" />Servicio</td>
                    <td></td>
	              </tr>
	              <tr>
	                <td><img  class="ICO" src="../../../Images/imgIconoContratante.gif" />Contratante</td>
                    <td><img class="ICO" src="../../../Images/imgIconoRepJor.gif" />Replicado</td>
                    <td><img class="ICO" src="../../../Images/imgIconoRepPrecio.gif" />Replicado con gestión propia</td>
                  </tr>
	              <tr>
	                <td><img class="ICO" src="../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto</td>
                    <td><img class="ICO" src="../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />Cerrado</td>
                    <td>
                        <img class="ICO" src="../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />Histórico
                        <img class="ICO" src="../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' style="margin-left:20px;" />Presupuestado
                    </td>
                  </tr>
            </table>
        </td>
    </tr>
</table>
<asp:TextBox ID="nPSN" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="ML" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="ListaPSN" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="MonedaPSN" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="origen" runat="server" style="visibility:hidden" Text="resumen" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
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
				case "excel": 
				{
                    bEnviar = false;
                    mostrarProcesando();
                    setTimeout("excel();", 20);
					break;
				}
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("ResumenEconomico.pdf");
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

