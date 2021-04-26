<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    var nAnoMesActual = <%=nAnoMes %>;
    var nUtilidadPeriodo = <%=nUtilidadPeriodo.ToString() %>;
    var bRes1024 = <%=((bool)Session["AVANTEC1024"]) ? "true":"false" %>;
    var sSubnodos = "<%=sSubnodos %>";
    var bHayPreferencia = <%=sHayPreferencia %>;
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;
    var nIDFicepiEntrada = <%=Session["IDFICEPI_ENTRADA"].ToString() %>;
    <%=sCriterios %>
</script>
<div id="divTiempos" style="width: 400px; position: absolute; top:135px; left: 700px; display:none;">
</div>
<div id="div1024" style="z-index: 105; width: 32px; height: 32px; position: absolute; top: 93px; right: 10px;">
    <asp:Image ID="img1024" CssClass="MA" runat="server" height="32" width="32" ImageUrl="~/images/imgICO1024.gif" ondblclick="setResolucionPantalla()" ToolTip="Conmuta el tamaño de pantalla para adecuarla a la resolución 1024x768 o 1280x1024" />
</div>
<%
    if (!(bool)Session["AVANTEC1024"]){
%>
<center>
<%
    }
%>
<div id="divMonedaImportes" runat="server" style="position:absolute; top:140px; left:388px; visibility:hidden;display:inline;width:300px;height:17px">
<label id="lblLinkMonedaImportes" class="enlace" style="vertical-align:bottom" onclick="getMonedaImportes()">Importes</label>&nbsp;&nbsp;<label style="vertical-align:top"> en </label>&nbsp;&nbsp;<label id="lblMonedaImportes" style="vertical-align:top" runat="server">Dólares americanos</label>
</div>
<img id="imgPestHorizontalAux" src="../../../../Images/imgPestHorizontal.gif" style="z-index: 0;position:absolute; left:40px; top:125px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
<div id="divPestRetr" style="position:absolute; left:20px; top:125px; width:960px; height:438px; clip:rect(auto auto 0 auto)">
    <table style="width:960px;text-align:left" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td>
            <table class="texto" style="width:940px; height:438px;" cellpadding="0" cellspacing="0" border="0">
                <tr>
		            <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../../Images/Tabla/5.gif" style="padding: 5px">
                        <!-- Inicio del contenido propio de la página -->
                        <table class="texto" style="width:930px; text-align:left">
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
                            <asp:ListItem Value="H" Text="Histórico"></asp:ListItem>
                            <asp:ListItem Value="P" Text="Presupuestado"></asp:ListItem>
                            </asp:DropDownList>
                            </td>
                            <td>
                            Categoría<br /><asp:DropDownList id="cboCategoria" runat="server" width="130px" onChange="setCombo()" CssClass="combo">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="P" Text="Producto"></asp:ListItem>
                                <asp:ListItem Value="S" Text="Servicio"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            Cualidad<br /><asp:DropDownList id="cboCualidad" runat="server" width="130px" onChange="setCombo()" CssClass="combo">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="C" Text="Contratante"></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Replicado con gestión"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td><img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom;">
                                <img border='0' src='../../../../Images/imgCerrarAuto.gif' style="vertical-align: bottom; margin-left:30px;"
                                    title="Repliegue automático de la pestaña de criterios al obtener información">
                                <input id="chkCerrarAuto" runat="server" class="check" type="checkbox" checked />
                            </td>
                            <td>
                                <img src='../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom;">
                                <input type="checkbox" id="chkActuAuto" class="check" runat="server" />
                            </td>
                            <td align="left">
                                <button id="btnObtener" type="button" onclick="buscar()" hidefocus="hidefocus" onmouseover="mostrarCursor(this)" runat="server" class="btnH25W90">
                                    <span><img src="../../../../Images/imgObtener.gif" />&nbsp;Obtener</span>
                                </button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label><img id="Img14" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divAmbito" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
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
                                    <legend><label id="Label5" class="enlace" onclick="getCriterios(6)" runat="server">Sector</label><img id="Img1" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(6)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divSector" style="overflow-X:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
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
                                    <legend><label id="Label3" class="enlace" onclick="getPeriodo()">Periodo</label></legend>
                                        Inicio&nbsp;<asp:TextBox ID="txtDesde" style="margin-left:5px;width:90px; vertical-align:middle;" Text="" readonly runat="server" />
                                        <asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" /><br />
                                        Fin&nbsp;<asp:TextBox ID="txtHasta" style="margin-left:15px; width:90px; vertical-align:middle;" Text="" readonly runat="server" />
                                        <asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset style="width: 130px; height:50px;">
                                    <legend title="Aplicable sólo entre diferentes criterios">Operador lógico</legend>
                                    <asp:RadioButtonList ID="rdbOperador" SkinID="rbli" runat="server" RepeatColumns="2" style="margin-top:8px;" onclick="setOperadorLogico(true)">
                                        <asp:ListItem Value="1" Selected><img src='../../../../Images/imgY.gif' border='0' title="Criterios acumulados" style="cursor:pointer" hidefocus=hidefocus onclick="seleccionar(this.parentNode)">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="0"><img src='../../../../Images/imgO.gif' border='0' title="Criterios independientes" style="cursor:pointer" hidefocus=hidefocus onclick="seleccionar(this.parentNode)"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label6" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img2" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divResponsable" style="overflow-X:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
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
                                    <legend><label id="Label7" class="enlace" onclick="getCriterios(7)" runat="server">Segmento</label><img id="Img3" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(7)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divSegmento" style="overflow-X:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
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
                                    <legend><label id="Label8" class="enlace" onclick="getCriterios(3)" runat="server">Naturaleza</label><img id="Img4" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(3)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divNaturaleza" style="overflow-X:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
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
                                    <legend><label id="Label9" class="enlace" onclick="getCriterios(8)" runat="server">Cliente</label><img id="Img5" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(8)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divCliente" style="overflow-X:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
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
                                    <legend><label id="Label10" class="enlace" onclick="getCriterios(4)" runat="server">Modelo de contratación</label><img id="Img6" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(4)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divModeloCon" style="overflow-X:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
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
                                    <legend><label id="Label11" class="enlace" onclick="getCriterios(9)" runat="server">Contrato</label><img id="Img7" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(9)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divContrato" style="overflow-X:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
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
                                    <legend><label id="Label12" class="enlace" onclick="getCriterios(16)" runat="server">Proyecto</label><img id="Img15" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(16)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divProyecto" style="overflow-X:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblProyecto" class="texto" style="width:260px;">
                                         <%=strHTMLProyecto%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label13" class="enlace" onclick="getCriterios(5)" runat="server">Horizontal</label><img id="Img8" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(5)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divHorizontal" style="overflow-X:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
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
                                    <legend><label id="lblCDP" class="enlace" onclick="getCriterios(10)" runat="server">Qn</label><img id="Img9" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(10)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divQn" style="overflow-X:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
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
                                    <legend><label id="lblCSN1P" class="enlace" onclick="getCriterios(11)" runat="server">Q1</label><img id="Img10" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(11)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
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
                                    <legend><label id="lblCSN2P" class="enlace" onclick="getCriterios(12)" runat="server">Q2</label><img id="Img11" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(12)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
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
                                <div id="divMonedaImportes2" runat="server" style="visibility:hidden">
                                    <label id="lblLinkMonedaImportes2" class="enlace" onclick="getMonedaImportes()">Importes</label> en <label id="lblMonedaImportes2" runat="server">Dólares americanos</label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset id="fstCSN3P" runat="server" style="width: 290px; height:50px;">
                                    <legend><label id="lblCSN3P" class="enlace" onclick="getCriterios(13)" runat="server">Q3</label><img id="Img12" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(13)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
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
                                    <legend><label id="lblCSN4P" class="enlace" onclick="getCriterios(14)" runat="server">Q4</label><img id="Img13" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(14)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divQ4" style="overflow-X:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblQ4" class="texto" style="width:260px;">
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
<%
    if (!(bool)Session["AVANTEC1024"]){
%>
</center>
<%
    }
%>
<br /><br />
<table id="tblGeneral" style="width:1510px; margin-top:5px; margin-left:10px; text-align:left" cellpadding="0">
<colgroup>
	<col style="width:375px"/>
	<col style="width:1135px"/>
</colgroup>	
    <tr>
        <td style="vertical-align:bottom;" align="left">
            <div id="divTituloFijo" class="divTitulo" style="width: 375px;" runat="server">
            <table id='tblTituloFijo' style="width: 375px; height: 34px; z-index:5;">
                <colgroup>
                    <col style="width:120px"/>
                    <col style="width:255px"/>                    
                </colgroup>
	            <tr class="texto" align="center">
                    <td colspan="2" align="left">
                        <img title="Mes anterior" onclick="cambiarMes(-1)" src="../../../../Images/btnAntRegOff.gif" style="cursor: pointer;vertical-align:bottom" />
                        <asp:TextBox ID="txtMesVisible" style="width:90px; text-align:center; vertical-align:super" readonly=true runat="server" Text=""></asp:TextBox>
                        <img title="Siguiente mes" onclick="cambiarMes(1)" src="../../../../Images/btnSigRegOff.gif" style="cursor: pointer;vertical-align:bottom" />
                        &nbsp;&nbsp;&nbsp;
                    </td>
	            </tr>
	            <tr id="tblTitulo" class="TBLINI" align="center">
					<td style="text-align:right;"><IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblBodyFijo',3,'divTituloFijo','imgLupa1',event,'setBuscarDescriFija()')"
							height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"><IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblBodyFijo',3,'divTituloFijo','imgLupa1','setBuscarDescriFija()')"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
							&nbsp;Nº&nbsp;&nbsp;
					</td>
					<td style="text-align:left;">Proyecto&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblBodyFijo',4,'divTituloFijo','imgLupa2','setBuscarDescriFija()')"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"><IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblBodyFijo',4,'divTituloFijo','imgLupa2',event,'setBuscarDescriFija()')"
							height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
					</td>                    
	            </tr>
            </table>
            </div>
		</td>
		<td style="vertical-align:bottom;" align="left">
            <div id="divTituloMovil" style="overflow:hidden; width: 846px;" runat="server">
            <table id="tblTituloMovil" style="width: 1135px; height: 34px; z-index:5;">
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
                    
                    <col style="width:50px"/>
                    <col style="width:50px"/>
                    <col style="width:50px"/>
                </colgroup>
	            <tr class="texto" align="center">
                    <td colspan="4" class="colTabla">Planificado</td>
                    <td colspan="5" class="colTabla">IAP</td>
                    <td colspan="4" class="colTabla">Previsto</td>
                    <td colspan="2" class="colTabla">Avance</td>
                    <td colspan="3" class="colTabla1">Indicadores</td>				
	            </tr>		
	            <tr id="tblTitulo2" class="TBLINI" align="center">                   
                    <td>Total</td>
                    <td>Inicio</td>
                    <td>Fin</td>
                    <td><label id="lblPresupuesto" title="Importe presupuestado" style="width:100px">Imp. Presup.</label></td>
                    
                    <td>Mes</td>
                    <td title="Acumulado">Acumul.</td>
                    <td title="Pendiente estimado. Equivale a la suma de las estimaciones menos las imputaciones a tareas con estimación.">Pend. Est.</td>
                    <td title="Total estimado">Total Est.</td>
                    <td title="Fin estimado">Fin Est.</td>
                    
                    <td>Total</td>
                    <td>Pendiente</td>
                    <td>Fin</td>
                    <td>%</td>		
                    
                    <td>%</td>		
                    <td><label id="lblProducido" title="Importe producido" style="width:100px">Imp. Produc.</label></td>
                    
                    <td><label id="Label1" title="% Consumido: relación entres los esfuerzos consumidos y los planificados">% Con.</label></td>
                    <td><label id="Label2" title="% Desviación de esfuerzos: relación entre los esfuerzos previstos y planificados">% DE.</label></td>
                    <td><label id="Label4" title="% Desviación de plazos: relación entre los plazos previstos y planificados">% DP.</label></td>
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
				<div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:1135px">
                <%=strTblBodyMovilHTML%>
				</div>
			</div>
		</td>
	</tr>	
</table>
<br />
<div style="position:absolute; margin-bottom:2px; margin-left:15px">
    <table width="940px">
        <colgroup>
            <col style="width:100px" />
            <col style="width:90px" />
            <col style="width:210px" />
            <col style="width:540px" />
        </colgroup>
          <tr> 
            <td><img border="0" src="../../../../Images/imgProducto.gif" /><span style="vertical-align:super">&nbsp;Producto</span></td>
            <td><img border="0" src="../../../../Images/imgServicio.gif" /><span style="vertical-align:super">&nbsp;Servicio</span></td>
            <td></td>
            <td rowspan="3" style="vertical-align:top;">
            </td>
          </tr>
          <tr><td><img border="0" src="../../../../Images/imgIconoContratante.gif" /><span style="vertical-align:super">&nbsp;Contratante</span></td>
                <td colspan="2"><img border="0" src="../../../../Images/imgIconoRepPrecio.gif" /><span style="vertical-align:super">&nbsp;Replicado con gestión propia</span></td>
          </tr>
          <tr>
            <td style="vertical-align:top;"><img border="0" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' /><span style="vertical-align:super">&nbsp;Abierto</span></td>
            <td><img border="0" src="../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' /><span style="vertical-align:super">&nbsp;Cerrado</span></td>
            <td><img border="0" src="../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' /><span style="vertical-align:super">&nbsp;Histórico&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                <img border="0" src="../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />
                <span style="vertical-align:super">&nbsp;Presupuestado</span>
            </td>
          </tr>
    </table>
</div>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			switch (strBoton){
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("AvanceTecnico.pdf");
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

