<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" EnableViewState="False" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
    var strEstructuraSubnodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>";
    var nEstructuraMinima = <%=nEstructuraMinima.ToString() %>;
    var nUtilidadPeriodo = <%=nUtilidadPeriodo.ToString() %>;
    var bRes1024 = <%=((bool)Session["DATOSRES1024"]) ? "true":"false" %>;
    var sSubnodos = "<%=sSubnodos %>";
    var bHayPreferencia = <%=sHayPreferencia %>;
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;
    var nIDFicepiEntrada = <%=Session["IDFICEPI_ENTRADA"].ToString() %>;
//    var es_DIS = <%=(User.IsInRole("DIS"))? "true":"false" %>;	
//    var usu_actual = "<%=Session["UsuarioActual"].ToString() %>";      

</script>

<img id="imgPestHorizontalAux" src="../../../../Images/imgPestHorizontal.gif" style="Z-INDEX: 1;position:absolute; left:40px; top:98px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
<div id="divPestRetr" style="position:absolute; left:20px; top:98px; width:960px; height:580px; clip:rect(auto auto 0px auto);Z-INDEX: 1;">
    <table style="width:960px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
    <tr style="vertical-align:top;">
        <td>
            <table class="texto" style="width:940px; height:280px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
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
                            <td colspan="2">Disponibilidad<br />
                                <asp:DropDownList id="cboDisponibilidad" runat="server" style="width:200px;vertical-align:middle;" onChange="setCombo()"  CssClass="combo">
                                    <asp:ListItem Value="0" Text="Todos"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Sólo disponibles"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Sólo sobrecargados"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td><input type="checkbox" id="chkMisProyectos" class="check" runat="server" />&nbsp;<label style="vertical-align:middle; cursor:pointer;" onclick="$I('chkMisProyectos').click()" title="Proyectos en los que soy responsable, delegado o colaborador">Mis proyectos</label>
                            </td>
                            <td>
                            </td>
                            <td>
                                <img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom;">
                                <img border='0' src='../../../../Images/imgCerrarAuto.gif' style="vertical-align: bottom; margin-left:15px;"
                                    title="Repliegue automático de la pestaña de criterios al obtener información">
                                <input id="chkCerrarAuto" runat="server" class="check" type="checkbox" checked />
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
                                    <LEGEND><label id="lblRol" class="enlace" onclick="getCriterios(23)" runat="server">Rol</label><img id="Img1" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(20)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divRol" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblRol" class="texto" style="width:260px;">
                                         <%=strHTMLRol%>
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
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblSupervisor" class="enlace" onclick="getCriterios(24)" runat="server">Evaluador</label><img id="Img2" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(21)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divSupervisor" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblSupervisor" class="texto" style="width:260px;">
                                         <%=strHTMLSupervisor%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCentroTrabajo" class="enlace" onclick="getCriterios(25)" runat="server">Centro de trabajo</label><img id="Img3" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(22)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divCentroTrabajo" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblCentroTrabajo" class="texto" style="width:260px;">
                                         <%=strHTMLCentroTrabajo%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="3">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblOficina" class="enlace" onclick="getCriterios(26)" runat="server">Oficina</label><img id="Img4" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(23)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divOficina" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblOficina" class="texto" style="width:260px;">
                                         <%=strHTMLOficina%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblProfesional" class="enlace" onclick="getCriterios(27)" runat="server">Profesional</label><img id="Img5" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(24)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divProfesional" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblProfesional" class="texto" style="width:260px;">
                                         <%=strHTMLProfesional%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblResponsable" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img6" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divResponsable" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblResponsable" class="texto" style="width:260px;">
                                         <%=strHTMLResponsable%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>                            
                            </td>
                            <td colspan="3">
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
<br />
<center>
<table id="tblProyecto" style="width:1200px; margin-top:5px;  margin-left:20px; text-align:left" cellpadding="0">
    <colgroup>
	    <col style="width:400px"/>
	    <col style="width:800px"/>
    </colgroup>	
	    <tr>
		    <td style="vertical-align:bottom;" align="left" >
			    <div id="divTituloFijo" style="width: 400px;" runat="server">
			    <table id='tblTituloFijo' style="width: 400px;">
				    <colgroup>
					    <col style="width:400px;" />		
				    </colgroup>
                    <tr>
                        <td>
                            &nbsp;
                            <img id="imgNE1" src='../../../../images/imgNE1off.gif' class="ne" onclick="mostrarProcesando();setTimeout('setNE(1)',100);"><img id="imgNE2" src='../../../../images/imgNE2off.gif' class="ne" onclick="mostrarProcesando();setTimeout('setNE(2)',100);">
                        </td>
                    </tr>
				    <tr id="tblTitulo" class="TBLINI">
					    <td align="left">&nbsp;&nbsp;Profesional&nbsp;-&nbsp;Disponibilidad&nbsp;/&nbsp;Proyecto
						    <img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblBodyFijo',1,'divTituloFijo','imgLupa2','setBuscarDescriFija()')"
							    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"/> 
						    <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblBodyFijo',1,'divTituloFijo','imgLupa2', event,'setBuscarDescriFija()')"
							    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"/>
					    </td>			
				    </tr>
			    </table>
			    </div>
		    </td>
		    <td style="vertical-align:bottom;" align="left">
			    <div id="divTituloMovil" style="overflow:hidden; width: 540px;"  runat="server">
			    <%=strTituloMovilHTML%>
			    </div>
		    </td>
	    </tr>		
	    <tr>
		    <td style="vertical-align:top;" class='tdbl'>
			    <div id="divBodyFijo" style="width:400px; height:500px; overflow:hidden;border-bottom: solid 1px #A6C3D2; " runat="server">
                    <%=strBodyFijoHTML%>
			    </div>	
                <table id="tblResultado" style="width: 400px ;height:16px;">
                    <tr class="TBLFIN" style="height:16px;">
	                    <td>&nbsp;</td>
                    </tr>
                </table>				    			    
		    </td>
		    <td style="vertical-align:top;">
			    <div id="divBodyMovil" style="width:556px; height:516px; overflow-x:auto;overflow-y:scroll;" runat="server" onscroll="scrollTabla();setScroll();">
				    <%=strBodyMovilHTML%>
			    </div>
		    </td>
	    </tr>	
	    <tr>
            <td style="padding-top: 5px;">
                &nbsp;<img class="ICO" src="../../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
                <img id="imgForaneo" class="ICO" src="../../../../Images/imgUsuFVM.gif" runat="server" />
                <label id="lblForaneo" runat="server">Foráneo</label>
            </td>
            <td>
                <input type="text" class="OutCal" readonly="readonly" style="margin-left:40px;" />&nbsp;Sin calendario            
            </td>
        </tr>
</table>
</center>
 
<asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" /><br />
<asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnFecha" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />

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

