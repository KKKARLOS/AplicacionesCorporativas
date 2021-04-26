<%@ Page Language="c#" Inherits="CR2I.Capa_Presentacion.obtenerRecurso" CodeFile="obtenerRecurso.aspx.cs" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title><%=strTitulo%></title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"> 
	<link rel="stylesheet" href="../App_Themes/Corporativo/Corporativo.css" type="text/css" />
	<script language="JavaScript" src="../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../Javascript/funcionesRecurso.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
	var intOpcion = "";
	var intSession = 30;  //Tiempo de caducidad en minutos.
    var strServer = "";
    
		function controlarSession(){
			if (intSession == 1){
				cerrarVentana();
			}else{
				intSession--;
				setTimeout("controlarSession()",60000);
			}
		}


		function init(){
//			if (strErrores != ""){
//				alert(strErrores);
		    //	    	}

		    if (nName == "chrome") {
		        //alert("Es Chrome");
		        window.resizeTo(450, 370);
		    }
		    intOpcion = $I("hdnOpcion").value;
		    strServer = $I("hdnServer").value;
		    ocultarProcesando();
			aLinks = abc.getElementsByTagName("A");
			Opciones = tblOpciones.getElementsByTagName("TR");
			$I("abc").style.visibility = "visible";
			controlarSession();
			try {
			    $I("txtApellido").focus();
			} catch (e) { }   
			
		}

		function mostrarRecursoAp() {
		    mostrarProcesando();
		    setTimeout("mostrarRecursoAux(1,'" + $I("txtApellido").value + "')", 30);
		}
		function mostrarRecurso(sIni) {
		    mostrarProcesando();
		    setTimeout("mostrarRecursoAux(1,'" + sIni + "')", 30);
		}
		
		function mostrarRecursoAux(intValor,strInicial){
			strUrl = document.location.toString();
			intPos = strUrl.indexOf("obtenerRecurso.aspx");
			strUrlPag = strUrl.substring(0,intPos)+"obtenerDatos.aspx";
			strUrlPag += "?intOpcion=" + intValor +"&strInicial="+ strInicial;
			//alert(strUrlPag);
			strTable = unescape(sendHttp(strUrlPag));
			//alert(strTable);
			if (strTable != "Error")
				$I("divCatalogo").innerHTML = strTable;
			else
				alert("No se han podido obtener los datos");
				
			$I("txtApellido").value = "";
			ocultarProcesando();
			
			for (i=0;i<aLinks.length;i++){
			    if (document.all) {
			        if (aLinks[i].innerText == strInicial) {
			            //aLinks[i].setAttribute("class", "linkInicialesSelec");
			            aLinks[i].className = "linkInicialesSelec";
			        }
			        else {
			            //aLinks[i].setAttribute("class", "linkIniciales");
			            aLinks[i].className = "linkIniciales";
			        }
				}
				else{
				    if (aLinks[i].textContent == strInicial) 
				        aLinks[i].setAttribute("class","linkInicialesSelec");
				    else 
				        aLinks[i].setAttribute("class","linkIniciales");
				}
			}
			Opciones = $I("tblOpciones").getElementsByTagName("TR");
		}
	-->
	</script>
</head>
<body style="overflow:hidden; width:450px; height:330px;" onload="init();">
<form id="Form1" method="post" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<br />
<table style="width:414px;" align="center" border="0">
	<tbody>
		<tr>
			<td class="texto">
			    <center>Apellido1
				<asp:TextBox class="textareaTexto" onkeypress="javascript:if(event.keyCode==13){mostrarRecursoAp();event.keyCode=0;}" 
				    id="txtApellido" runat="server" style="width:100px; border: #315d6b 1px solid;PADDING: 0px 0px 0px 2px;FONT-SIZE: 11px;TOP: 0px;LEFT: 0px;MARGIN: 1px 1px 1px 4px; FONT-FAMILY: Arial, Helvetica, sans-serif;  BACKGROUND-IMAGE: url(../images/fondoTxt.gif); HEIGHT: 14px;">
				</asp:TextBox>
				</center>
		    </td>
		</tr>
		<tr>
			<td>
				<%
				string strEstilo;
				Response.Write("<div id='abc' class='texto' style='padding:5px;visibility:hidden'>");
				for (int intIndice=65;intIndice<=78;intIndice++){
					if (strInicial == ((char)intIndice).ToString()){
						strEstilo = "linkInicialesSelec";
					}else{
						strEstilo = "linkIniciales";
					}
			%>
				<a class="<%=strEstilo%>" style="cursor: pointer" onclick='javascript:mostrarRecurso("<%=(char)intIndice%>")' ><%=(char)intIndice%></a>&nbsp;
				<%
				}
				if(strInicial == "Ñ"){
					strEstilo = "linkInicialesSelec";
				}else{
					strEstilo = "linkIniciales";
				}
			%>
				<a class="<%=strEstilo%>" style="cursor: pointer" onclick='javascript:mostrarRecurso("Ñ")'>Ñ</a>&nbsp;&nbsp;
				<%
				for (int intIndice=79;intIndice<=90;intIndice++){
					if (strInicial == ((char)intIndice).ToString()){
						strEstilo = "linkInicialesSelec";
					}else{
						strEstilo = "linkIniciales";
					}
			%>
				<a class="<%=strEstilo%>" style="cursor: pointer" onclick='javascript:mostrarRecurso("<%=(char)intIndice%>")'><%=(char)intIndice%></a>&nbsp;
				<%  
				}
				Response.Write("</div>");
			%>
				<table id="tblTitulo" style='text-align:left; height:17px; width:396px;' border="0">
					<tr class="TBLINI">
						<td style="width:80%;">
						    <img style="cursor: pointer" onclick="buscarDescripcion('tblOpciones',0,'divCatalogo','imgLupa1',event)"
								height="11" src="../Images/imgLupa.gif" width="20"> 
							<img id="imgLupa1" style="display: none; cursor: pointer; height:11px; width:20px;" onclick="buscarSiguiente('tblOpciones',0,'divCatalogo','imgLupa1')" src="../Images/imgLupaMas.gif"> &nbsp;<%=strColumna%>
						</td>
						<td style="width:20%;">
						    <img onmouseover="javascript:bMover=true;scrollUp('divCatalogo',16);" style="cursor:pointer; height:8px; width:11px;" onmouseout="javascript:bMover=false;" src="../Images/imgFleUp.gif" >
					    </td>
					</tr>
				</table>
				<div id="divCatalogo" style="OVERFLOW-X: hidden; overflow-Y: auto; width: 412px; height: 176px; text-align:left;" name="divCatalogo">
					<asp:repeater id="rptOpciones" runat="server" EnableViewState="False">
						<HeaderTemplate>
							<table id="tblOpciones" class="texto" style=" width: 396px; border-collapse: collapse;">
						</HeaderTemplate>
						<ItemTemplate>
							<tr class="FA" id='<%# Eval("T001_CIP") %>' idFicepi='<%# Eval("T001_IDFICEPI") %>' onclick="marcarUnaFila('tblOpciones',this.id,this.rowIndex)" ondblclick="aceptarClick(this.rowIndex)" style="cursor: pointer;">
								<td style="padding-left:5px"><label style="width:390px; text-overflow:ellipsis;overflow:hidden" title='<%# Eval("DESCRIPCION") %>'><nobr><%# Eval("DESCRIPCION") %></nobr></label></td>
							</tr>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tr class="FB" id='<%# Eval("T001_CIP") %>' idFicepi='<%# Eval("T001_IDFICEPI") %>' onclick="marcarUnaFila('tblOpciones',this.id,this.rowIndex)" ondblclick="aceptarClick(this.rowIndex)" style="cursor: pointer;">
								<td style="padding-left:5px"><label style="width:390px; text-overflow:ellipsis;overflow:hidden" title='<%# Eval("DESCRIPCION") %>'><nobr><%# Eval("DESCRIPCION") %></nobr></label></td>
							</tr>
						</AlternatingItemTemplate>
						<FooterTemplate>
                            </table>
                        </FooterTemplate> 
                        </asp:repeater>
                </div>
                <table id="tblResultado" style='text-align:left; height:17px; width:396px;' border="0">
	                <tr class="TBLFIN">
		                <td colspan="2">
		                    <img height="1" src="../Images/imgSeparador.gif" width="80%" border="0" />
			                <img onmouseover="javascript:bMover=true;moverTablaDown()" style="cursor:pointer; height:8px; width:11px;" onmouseout="javascript:bMover=false;" src="../Images/imgFleDown.gif" />
				        </td>
	                </tr>
	                <tr style="height:5px;">
		                <td colspan="2">
		                    <img onmouseover="javascript:bMover=true;scrollDown('divCatalogo',16);" onmouseout="javascript:bMover=false;" style="height:7px;" src="../Images/imgSeparador.gif">
				        </td>
	                </tr>
                </table>
            </td>
        </tr>
    </tbody>
</table>
<table align="center" border="0" style="width:300px;">
    <tr>
        <td>
            <button id="btnAceptar" type="button" onclick="aceptar()" style="width:85px; margin-left:40px;" hidefocus=hidefocus>
                <span><img src="../images/imgAceptar.gif" />&nbsp;Aceptar</span>
            </button>    
        </td>
        <td>
            <button id="btnCancelar" type="button" onclick="cerrarVentana()" style="width:85px; margin-left:10px;" hidefocus=hidefocus>
                <span><img src="../images/Botones/imgCancelar.gif" />&nbsp;Cancelar</span>
            </button>    
        </td>
    </tr>
</table>
<input type="hidden" id="hdnOpcion" runat="server" />
<input type="hidden" id="hdnServer" runat="server" />
</form>
<script type="text/javascript">
<!--
-->
	</script>
</body>
</html>
