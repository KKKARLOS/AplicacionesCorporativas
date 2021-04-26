<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Simple.aspx.cs" Inherits="Capa_Presentacion_CVT_Consultas_Simple" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    //    var sIdPreferenciaBasica = "<>%=//sIdPreferenciaBasica %>";        
    //    var sIdPreferenciaAvanzada = "<>%=//sIdPreferenciaAvanzada %>";        
    //    var sIdPreferenciaCadena = "<>%=//sIdPreferenciaCadena %>";        
    <%=sEstructura %>
    <%=sCriterios %>
    var es_DIS = <%=(User.IsInRole("DIS"))? "true":"false" %>;
</script>
<%--<div style="margin-left:400px;">
    <img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de todas las preferencias" onclick="getCatalogoPreferencias('T')" style="cursor:pointer; vertical-align:bottom;">&nbsp;
    <img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia('T')" style="cursor:pointer; vertical-align:bottom;">&nbsp;
</div>--%>
<div id="divTotal" style="z-index:10; position:absolute; left:0px; top:0px; width:1200px; height:800px; background-image: url(../../../../Images/imgFondoOscurecido.png); background-repeat:repeat; visibility:hidden;" runat="server">
    <div id="divTexto" style="position:absolute; top:240px; left:250px;">
    
        <table border="0" cellspacing="0" cellpadding="0" style="width:550px; height:250px; margin-top:5px;">
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/9.gif">
                <img id="imgPopUpCloseA" src="<%=Session["strServer"] %>Capa_Presentacion/UserControls/Msg/Images/popup_close.png" style="cursor:pointer; position:absolute; top: -9px; right:-10px; z-index: 9999;" onclick="ocultarFondo();" />
            </td>
          </tr>
          <tr>
            <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding:3px; vertical-align:top;">
            <!-- Inicio del contenido propio de la página -->
            <div id="divOpciones">
                <div style="height:80px">
                    <label id="lblFondoOscurecido" style="margin-top: 10px;"></label>
                    <br />
                    <br />
                    <label id="lblFondoOscurecido1" ></label>
                </div>
                <fieldset style="width:280px; margin-bottom:10px; padding-top:3px; margin-top: 40px;margin-left: 139px;">
                    <legend id="lgndEntrega">Método de entrega</legend>
                    <asp:RadioButtonList ID="rdbTipoExpFO" onclick="cambiarTextoFondo();"  runat="server" RepeatColumns="3" SkinId="rbl"  style="float:left; margin-top:3px;">
                        <asp:ListItem style="cursor:pointer;margin-right:10px;" Value="0"><img src="../../../../Images/imgMonitor.png" border='0' onclick="cambiarTextoFondo(0);" title="On-line" style="cursor:pointer;margin-top: -2px;" ></asp:ListItem>
                        <asp:ListItem style="cursor:pointer;" Selected=true Value="1"><img src='../../../../Images/Botones/imgEmail.png' border='0' onclick="cambiarTextoFondo(1);" title="Correo" style="cursor:pointer;margin-top: -2px;" ></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RadioButtonList ID="rdbTipoExpFOIB" onclick="cambiarTextoFondoIB();"  runat="server" RepeatColumns="3" SkinId="rbl"  style="float:left; margin-top:3px;">
                        <asp:ListItem style="cursor:pointer;margin-right:10px;" Value="0">
                            <img src="../../../../Images/imgAccesoW.gif" border='0' onclick="cambiarTextoFondoIB(0);" title="Editable" style="cursor:pointer;margin-top: -2px;" >
                        </asp:ListItem>
                        <asp:ListItem style="cursor:pointer;" Selected=true Value="1">
                            <img src='../../../../Images/adobe.png' border='0' onclick="cambiarTextoFondoIB(1);" title="PDF" style="cursor:pointer;margin-top: -2px;" >
                        </asp:ListItem>
                    </asp:RadioButtonList>
                    <label id="lblTipoExpFO" style="margin-top:8px; margin-left:60px">Diferido por correo</label>
                </fieldset>
                
                <div style="text-align:center;margin-top:39px">
                     <button id="btnContinuar" style="display:inline" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus" onclick="exportar();" onmouseover="se(this, 25);">
                        <img src="../../../../Images/imgFlechaDrOff.gif" /><span>Continuar</span>
                    </button>
                     <button id="btnCancelar"  style="display:inline; margin-left:20px" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus" onclick="ocultarFondo();" onmouseover="se(this, 25);">
                        <img src="../../../../Images/Botones/imgCancelar.gif" /><span>Cancelar</span>
                    </button>
                </div>
            </div>
            <div id="divAyuda">
            <span style="text-decoration:underline;"> GUÍA PRÁCTICA</span><br /><br />
                Cadena correcta es aquella que se identifica con luz verde. <br />
Cadena no completa es aquella que se identifica con luz amarilla.<br />
Cadena incorrecta se identifica con luz roja.<br /><br /><br />


Ejemplos correctos:<br /><br />

<span style="color:Blue">Inglés AND Francés</span><br /><br />

Entendiendo que la cadena a buscar estuviera en idiomas, buscaría los profesionales que en la denominación del idioma de su currículum, 
tuvieran el inglés y el francés.<br /><br /><br />


<span style="color:Blue">(Inglés OR Francés) AND Italiano</span><br /><br />

Entendiendo que la cadena a buscar estuviera en idiomas, buscaría los profesionales que en la denominación del idioma 
tuvieran el inglés o el francés, además del italiano.<br /><br /><br />


<span style="color:Blue">Licenciad? </span><br /><br />

El símbolo de interrogación consigue que, si la cadena a buscar estuviera en formación, se buscaran los profesionales 
que tuvieran en su formación la palabra escrita, sustituyendo la interrogación por cualquier otro carácter, es decir, 
Licenciado, Licenciada, Licenciadi, …..<br /><br /><br />


<span style="color:Blue">Arquite*</span><br /><br />

De forma casi similar al caso anterior, el símbolo asterisco buscaría todos los profesionales que en su formación 
tuvieran alguna palabra que empezara por Arquite.<br /><br /><br />


<span style="color:Blue">"Licenciado en Informática" OR "Ingeniero de caminos" </span><br /><br />

La utilización de comillas, da la siguiente posibilidad: Suponiendo que la frase estuviera en titulación académica, 
buscaría todos los profesionales que en el mencionado apartado tuvieran alguna de las dos frases, tal y como se han escrito.<br /><br /><br />



Nota: No se diferencian mayúsculas, minúsculas ni tildes.

            </div>
            <div id="divAyudaCadena">
            <span style="text-decoration:underline;"> GUÍA PRÁCTICA</span><br /><br />
                En el apartado cadena se puede introducir el término que se desee buscar.<br />
                Si se desea buscar por varios términos, se deben introducir separados por punto y coma.<br /><br /><br />

                Ejemplo:<br /><br />

                <span style="color:Blue">Inglés;Francés</span><br /><br /><br />

                Las casillas de verificación del apartado Cadena indican en qué items del CV se van a buscar los términos 
                introducidos en la caja de texto. Es obligatorio marcar al menos una de las casillas<br /><br />
                Las casillas son acumulativas entre si. Es decir, si están marcadas Formación y Experiencia, buscará aquellos 
                profesionales cuyo CV contenga el término indicado en la formación o en las experiencias profesionales.<br /><br />
                Situando el cursor sobre cada una de las casillas se visualiza sobre qué items se realiza la búsqueda.<br /><br />
                Por ejemplo si se marca la casilla Formación, la búsqueda se realizará en los siguientes campos:<br />
                    <ul style="margin-left:30px;">
                        <li>Denominación del título</li>
                        <li>Centro de obtención del título</li>
                        <li>Especialidad del título</li>
                        <li>Observaciones del título</li>
                    </ul>
                <br /><br /><br />
                Nota: No se diferencian mayúsculas, minúsculas ni tildes.
            </div>
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

    </div>
</div>

<img id="imgPestHorizontalBasica" src="../../../../Images/imgPestBasica.png" alt="" 
    style="Z-INDEX: 1;position:absolute; left:35px; top:125px; cursor:pointer;" onclick="HideShowPest('basica')" />
<div id="divPestRetrBasica" style="Z-INDEX: 1;position:absolute; left:20px; top:125px; width:970px; height:520px; clip:rect(auto auto 0px auto);">
    <table style="width:870px;" cellpadding="0" cellspacing="0" border="0">
    <tr valign="top">
        <td>
            <table style="width:850px; height:520px;" cellpadding="0" cellspacing="0" border="0">
                <tr>
		            <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../../Images/Tabla/5.gif" style="padding: 3px" valign="top">
                        <!-- Inicio del contenido propio de la página -->
                        <table style="width:840px;">
                            <tr>
                                <td align="right" style="padding-right:10px; padding-top:3px;">
<%--                                    <img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias('B')" style="cursor:pointer; vertical-align:bottom;">&nbsp;
                                    <img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia('B')" style="cursor:pointer; vertical-align:bottom;">&nbsp;
                                    <img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia('B')" style="cursor:pointer; vertical-align:bottom;">&nbsp;--%>
                                    <img src='../../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="limpiar();" style="cursor:pointer; vertical-align:bottom;">                                
                                    &nbsp;&nbsp;&nbsp;
                                    <button id="btnObtener" type="button" onclick="buscarSimpleAux()" class="btnH25W85" hidefocus="hidefocus" onmouseover="mostrarCursor(this)" runat="server" style="display:inline;">
                                        <span><img src="../../../../images/imgObtener.gif" alt="" />Obtener</span>
                                    </button><!--
                                    <button id="btnLimpiar" type="button" onclick="limpiar()" class="btnH25W85" hidefocus="hidefocus" onmouseover="mostrarCursor(this)" runat="server" style="margin-left:10px; display:inline;">
                                        <span><img src="../../../../images/Botones/imgLimpiar2.gif" alt="" />Limpiar</span>
                                    </button>-->
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <fieldset style="width:820px;">
                                        <legend>Datos Personales-Organizativos</legend>
                                        <table id="tblDatosGenerales" style="width:820px; margin-top:5px;" cellpadding="3" cellspacing="0" border="0">
                                        <colgroup>
                                            <col style="width:100px;" />
                                            <col style="width:330px;" />
                                            <col style="width:90px;" />
                                            <col style="width:300px;" />
                                        </colgroup>
                                        <tr>
                                            <td><label class="enlace" onclick="abrirProfesional();">Profesional</label></td>
                                            <td><asp:TextBox ID="txtProfesional" name="txtProfesional" style="width:260px;" Text="" runat="server" ReadOnly="true" filterName="Profesional"/><img id="Img1" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarProfesional();" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/></td>
                                            <td><label id="lblSN4" runat="server">SN4</label></td>
                                            <td><asp:DropDownList id="cboSN4" name="cboSN4" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo" onchange="cargarCombo(1,this[this.selectedIndex].value);" filterName="SN4">
                                                    </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td><label style="display:none;">Perfil de mercado</label></td>
                                            <td><asp:DropDownList id="cboPerfilPro" name="cboPerfilPro" runat="server" style="width:280px; display:none;" AppendDataBoundItems="true" CssClass="combo" filterName="Perfil de mercado">
                                                    <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                    </asp:DropDownList></td>
                                            <td><label id="lblSN3" runat="server">SN3</label></td>
                                            <td><asp:DropDownList id="cboSN3" name="cboSN3" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo"  onchange="cargarCombo(2,this[this.selectedIndex].value);" filterName="SN3">
                                                    </asp:DropDownList> </td>
                                        </tr>
                                        <tr>
                                            <td><label title="Centro de trabajo físico">Centro</label></td>
                                            <td><asp:DropDownList id="cboCentro" name="cboCentro" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo" filterName="Centro">
                                                    <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                    </asp:DropDownList></td>
                                            <td><label id="lblSN2" runat="server">SN2</label></td>
                                            <td><asp:DropDownList id="cboSN2" name="cboSN2" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo"  onchange="cargarCombo(3,this[this.selectedIndex].value);" filterName="SN2">
                                                    </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td><label title="Interesado en trayectoria internacional">Trayectoria Int.</label></td>
                                            <td><asp:DropDownList id="cboIntTrayInt" name="cboIntTrayInt" runat="server" style="width:40px;" AppendDataBoundItems="true" CssClass="combo" filterName="Trayectoria Int.">
                                                        <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                        <asp:ListItem Text="Sí" Value="True"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                                    </asp:DropDownList></td>
                                            <td><label id="lblSN1" runat="server">SN1</label></td>
                                            <td><asp:DropDownList id="cboSN1" name="cboSN1" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo"  onchange="cargarCombo(4,this[this.selectedIndex].value);" filterName="SN1">
                                                    </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td><label title="Disponibilidad para la movilidad geográfica">Movilidad geog.</label></td>
                                            <td><asp:DropDownList id="cboMovilidad" name="cboMovilidad" runat="server" style="width:80px;" AppendDataBoundItems="true" CssClass="combo" filterName="Disp. Movilidad">
                                                    <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                    </asp:DropDownList></td>
                                            <td><label id="lblCR" runat="server">CR</label></td>
                                            <td><asp:DropDownList id="cboCR" name="cboCR" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo" onchange="cargarCombo(5,this[this.selectedIndex].value);" filterName="CR">
                                                    </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td><label title="% disponibilidad del profesional según la aplicación 'Gestión de Disponibles'">Grado Disp. &gt;=</label></td>
                                            <td><asp:TextBox ID="txtGradoDisp" name="txtGradoDisp" style="width:20px;" CssClass="txtNumM" onfocus="fn(this, 3, 0)" onkeyup="CheckValue(this, 100, 0);" SkinID=numero Text="" runat="server" filterName="Grado Disp."/></td>
                                            <td colspan="2">
                                                <div id="divEstado" runat="server">
                                                        <label style="width:87px;">Estado</label>
                                                        <asp:DropDownList id="cboEstado" name="cboEstado" runat="server" style="width:70px;" AppendDataBoundItems="true" CssClass="combo" filterName="Estado">
                                                            <asp:ListItem Text=""></asp:ListItem>
                                                            <asp:ListItem Selected="True" Text="Activos" Value="A"></asp:ListItem>
                                                            <asp:ListItem Text="Baja" Value="B"></asp:ListItem>
                                                        </asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2"><div id="divTipo" runat="server">
                                                        <label style="width:97px;">Tipo Profesional</label>
                                                        <asp:DropDownList id="cboTipo" name="cboTipo" runat="server" style="width:80px;" AppendDataBoundItems="true" CssClass="combo" filterName="Tipo Profesional">
                                                            <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                            <asp:ListItem Text="Interno" Value="I"></asp:ListItem>
                                                            <asp:ListItem Text="Externo" Value="X"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div></td>
                                            <td colspan="2"><div id="divCoste" runat="server">
                                                        <label style="width:87px;" title="Límite de coste por jornada en Euros">Coste &lt=;</label>
                                                        <asp:TextBox ID="txtLimCoste" name="txtLimCoste" style="width:60px;" CssClass="txtNumM" onfocus="fn(this, 5, 0)" SkinID=numero Text="" runat="server" filterName="Coste"/>
                                                        <label title="Euros por jornada">€ / j</label>
                                                    </div></td>
                                        </tr>
                                        </table>
                                    </fieldset>
                                    <fieldset style="width:820px; margin-top:5px;">
                                        <legend>Formación</legend>
                                        <table id="tblDatosFormacion" style="width:820px; margin-top:5px;" cellpadding="3" cellspacing="0" border="0">
                                        <colgroup>
                                            <col style="width:100px;" />
                                            <col style="width:330px;" />
                                            <col style="width:90px;" />
                                            <col style="width:100px;" />
                                            <col style="width:200px;" />
                                        </colgroup>
                                        <tr>
                                            <td>Título Académico</td>
                                            <td><asp:TextBox ID="txtTitulo" name="txtTitulo" style="width:260px;" Text="" runat="server" watermarktext="Ej: Licenciatura en Informática"  filterName="Titulo Acad."/><img src="../../../../Images/imgSugerencia.png" style="vertical-align:middle; margin-left:3px;" title="En función del texto introducido, el sistema le sugerirá titulaciones existentes" /></td>
                                            <td>Idioma</td>
                                            <td><asp:DropDownList id="cboIdioma" name="cboIdioma" runat="server" style="width:100px;" AppendDataBoundItems="true" CssClass="combo"  filterName="Idioma">
                                                    <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                    </asp:DropDownList>
                                             </td>
                                             <td>
                                                <asp:CheckBox ID="chkTituloAcre" runat="server" Text="Título acreditativo" TextAlign="Right" style="margin-left:20px;cursor:pointer;" />
                                             </td>
                                        </tr>
                                        <tr>
                                            <td>Tipología</td>
                                            <td><asp:DropDownList id="cboTipologia" name="cboTipologia" runat="server" style="width:130px;" AppendDataBoundItems="true" CssClass="combo" filterName="Tipologia">
                                                    <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                    </asp:DropDownList></td>
                                            <td>Nivel idioma</td>
                                            <td colspan="2"><asp:DropDownList id="cboNivel" name="cboNivel" runat="server" style="width:100px;" AppendDataBoundItems="true" CssClass="combo" filterName="Nivel idioma">
                                                    <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                    </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Modalidad</td>
                                            <td><asp:DropDownList id="cboModalidad" name="cboModalidad" runat="server" style="width:130px;" AppendDataBoundItems="true" CssClass="combo" filterName="Modalidad">
                                                    <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                    </asp:DropDownList></td>
                                            <td colspan="3" rowspan="3" style="vertical-align:top;">
                                                    <fieldset style="width: 310px; height:50px;">
                                                        <legend>
                                                            <label id="Label6" style="width: 195px;" class="enlace" onclick="getCriteriosConSim(1)" runat="server">Entorno tecnológicos/funcionales</label>
                                                            <asp:radiobuttonlist id="rdlFormacionEntorno" style="display:inline" runat="server" Width="100px" CssClass="radio texto" RepeatLayout="Flow" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal">
						                                        <asp:ListItem Value="A" Selected="True">&nbsp;Y&nbsp;&nbsp;</asp:ListItem>
						                                        <asp:ListItem Value="0">&nbsp;O&nbsp;&nbsp;</asp:ListItem>
						                                    </asp:radiobuttonlist>    
						                                    <img id="Img3" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriteriosSimple(1)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
						                                </legend>
                                                        <div id="divEntTecFor" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 306px; height:32px; margin-top:2px">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:290px; height:auto;">
                                                                <table id="tblEntTecFor" class="texto" style="width:290px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" filterName="Entorno tecnológico/funcional formacion">
                                                                     <%=strHTMLEntTecFor%>
                                                                 </table>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                              
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><label>TIC's</label></td>
                                            <td><asp:DropDownList id="cboTics" name="cboTics" runat="server" style="width:40px;" AppendDataBoundItems="true" CssClass="combo" filterName="TIC's">
                                                        <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                        <asp:ListItem Text="Sí" Value="True"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                                    </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>Certificación</td>
                                            <td><asp:TextBox ID="txtCertificacion" name="txtCertificacion" style="width:260px;" Text="" watermarktext="Ej: Certificación interna en ITIL" runat="server" filterName="Certificación"/><img src="../../../../Images/imgSugerencia.png" style="vertical-align:middle; margin-left:3px;" title="En función del texto introducido, el sistema le sugerirá certificaciones existentes" /></td>
                                        </tr>
                                        </table>
                                    </fieldset>
                                    <fieldset style="width:820px; margin-top:5px;">
                                        <legend>Experiencia profesional</legend>
                                        <table id="tblDatosExperiencia" style="width:820px; margin-top:5px;" cellpadding="3" cellspacing="0" border="0">
                                        <colgroup>
                                            <col style="width:100px;" />
                                            <col style="width:330px;" />
                                            <col style="width:90px;" />
                                            <col style="width:300px;" />
                                        </colgroup>
                                        <tr>
                                            <td>Cliente</td>
                                            <td><asp:TextBox ID="txtCuenta" name="txtCuenta" style="width:260px;" Text="" runat="server" watermarktext="Ej: FUNDACION ONCE"  filterName="Cliente"/><img src="../../../../Images/imgSugerencia.png" style="vertical-align:middle; margin-left:3px;" title="En función del texto introducido, el sistema le sugerirá cuentas de clientes existentes" /></td>
                                            <td colspan="2" rowspan="3" style="vertical-align:top;">
                                                    <fieldset style="width: 310px; height:50px; padding:5px;">
                                                        <legend>
                                                            <label id="Label8" style="width: 195px;" class="enlace" onclick="getCriteriosConSim(2)" runat="server">Entorno tecnológicos/funcionales</label>
                                                            <asp:radiobuttonlist id="rdlExperienciaEntorno" runat="server" style="display:inline"  Width="100px" CssClass="radio texto" RepeatLayout="Flow" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal">
						                                        <asp:ListItem Value="A" Selected="True">&nbsp;Y&nbsp;&nbsp;</asp:ListItem>
						                                        <asp:ListItem Value="O">&nbsp;O&nbsp;&nbsp;</asp:ListItem>
						                                    </asp:radiobuttonlist>
                                                            <img id="Img7" alt="" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriteriosSimple(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
                                                        </legend>
                                                        <div id="divEntTecExp" style="overflow-x:hidden; overflow-y:auto; WIDTH: 306px; height:32px; margin-top:2px">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:290px; height:auto;">
                                                                <table id="tblEntTecExp" class="texto" style="width:290px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" filterName="Entorno Tecnológico Funcional Experiencia">
                                                                    <%=strHTMLEntTecExp%>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </fieldset>

						                            
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Sector</td>
                                            <td><asp:DropDownList id="cboSector" name="cboSector" runat="server" style="width:130px;" AppendDataBoundItems="true" CssClass="combo" filterName="Sector">
                                                    <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                    </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>Perfil</td>
                                            <td><asp:DropDownList id="cboPerfilExp" name="cboPerfilExp" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo" filterName="Perfil">
                                                    <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                    </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td><span title="Mínimo tiempo trabajado por el profesional">Tiempo mínimo</span></td>
                                            <td>
                                                <asp:TextBox ID="txtCanTiempo" name="txtCanTiempo" style="width:30px; vertical-align:middle;" CssClass="txtNumM" onkeypress="vtn2(event)" SkinID="Numero" MaxLength="5" Text="" runat="server"  filterName="Minimo de"/>
                                                <asp:DropDownList id="cboMedTiempo" name="cboMedTiempo" runat="server" style="width:70px; vertical-align:middle;" AppendDataBoundItems="true" CssClass="combo" filterName="tiempo">
                                                    <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td><span title="Con experiencia profesional a partir de">Año &gt;=</span></td>
                                            <td><asp:DropDownList id="cboAnoInicio" name="cboAnoInicio" runat="server" style="width:50px;" AppendDataBoundItems="true" CssClass="combo" filterName="Año inicio">
                                                    <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                                                    </asp:DropDownList> </td>
                                        </tr>
                                        </table>
                                    </fieldset>
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

<img id="imgPestHorizontalAvanzada" src="../../../../Images/imgPestAvanzada.png" alt="" 
    style="Z-INDEX: 1;position:absolute; left:110px; top:125px; cursor:pointer;" onclick="HideShowPest('avanzada')" />
<div id="divPestRetrAvanzada" style="Z-INDEX: 1;position:absolute; left:20px; top:125px; width:970px; height:520px; clip:rect(auto auto 0px auto);">
    <table style="width:870px;" cellpadding="0" cellspacing="0" border="0">
    <tr valign="top">
        <td>
            <table style="width:860px; height:520px;" cellpadding="0" cellspacing="0" border="0">
                <tr>
		            <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../../Images/Tabla/5.gif" style="padding: 3px" valign="top">
                        <!-- Inicio del contenido propio de la página -->
                        <table style="width:850px;">
                            <tr>
                                <td align="right" style="padding-right:10px; padding-top:5px;">
<%--                                    <img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias('A')" style="cursor:pointer; vertical-align:bottom;">&nbsp;
                                    <img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia('A')" style="cursor:pointer; vertical-align:bottom;">&nbsp;
                                    <img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia('A')" style="cursor:pointer; vertical-align:bottom;">&nbsp;--%>
                                    <img src='../../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="limpiarAvanzada();" style="cursor:pointer; vertical-align:bottom;">                                
                                    &nbsp;&nbsp;&nbsp;
                                    <button id="btnObtenerAvan" type="button" onclick="buscarAvanzada()" class="btnH25W85" hidefocus="hidefocus" onmouseover="mostrarCursor(this)" runat="server" style="display:inline;">
                                        <span><img src="../../../../images/imgObtener.gif" alt="" />Obtener</span>
                                    </button>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divAvanzada" style="width:845px; height:470px; overflow-y:auto; overflow-x:hidden;">
                                        <!-- Datos Personales-Organizativos -->
                                        <fieldset style="width:815px; padding-left:10px; padding-right:0px;">
                                        <legend>Datos Personales-Organizativos</legend>
                                            <table class="texto" style="width:800px;" border="0">
                                            <colgroup>
                                                <col style="width:100px;" />
                                                <col style="width:300px;" />
                                                <col style="width:100px;" />
                                                <col style="width:300px;" />
                                            </colgroup>
                                            <tr>
                                                <td colspan="2">
                                                    <fieldset style="width:367px; height:50px; margin-bottom:5px;">
                                                        <legend>
								                            <label id="Label1" style="width:85px;" class="enlace" onclick="getCriterios(27)" runat="server">Profesionales</label>
								                            <img id="Img4" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(27)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
							                            </legend>
                                                        <div id="divAvanProf" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 366px; height:32px; margin-top:2px">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:350px; height:auto;">
                                                                <table id="tblAvanProf" class="texto" style="width:350px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                                     <%=strHTMLAvanProf%>
                                                                 </table>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </td>
                                                <td colspan="2">
                                                    <fieldset style="width: 367px; height:50px; margin-bottom:5px; display:none;">
                                                        <legend>
								                            <label id="Label2" style="width:290px;" class="enlace" onclick="getCriterios(3)" runat="server">El CV debe tener ALGUNO de los siguientes perfiles</label>
								                            <img id="Img5" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(3)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
							                            </legend>
                                                        <div id="divAvanPerf" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 366px; height:32px; margin-top:2px">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:350px; height:auto;">
                                                                <table id="tblAvanPerf" class="texto" style="width:350px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                                     <%=strHTMLAvanPerf%>
                                                                 </table>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td><label title="Centro de trabajo físico">Centro</label></td>
                                                <td>
                                                    <asp:DropDownList id="cboAvanCentro" name="cboAvanCentro" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo" filterName="Centro">
                                                        <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td><label id="lblAvanSN2" runat="server">SN2</label></td>
                                                <td>
                                                    <asp:DropDownList id="cboAvanSN2" name="cboAvanSN2" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo"  onchange="cargarComboAvanzado(3,this[this.selectedIndex].value);" filterName="SN2"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td><label title="Interesado en trayectoria internacional">Trayectoria Int.</label></td>
                                                <td>
                                                    <asp:DropDownList id="cboAvanIntTrayInt" name="cboAvanIntTrayInt" runat="server" style="width:40px;" AppendDataBoundItems="true" CssClass="combo" filterName="Trayectoria Int.">
                                                        <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                        <asp:ListItem Text="Sí" Value="True"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td><label id="lblAvanSN1" runat="server">SN1</label></td>
                                                <td>
                                                    <asp:DropDownList id="cboAvanSN1" name="cboAvanSN1" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo"  onchange="cargarComboAvanzado(4,this[this.selectedIndex].value);" filterName="SN1"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td><label title="Disponibilidad para la movilidad geográfica">Movilidad geog.</label></td>
                                                <td>
                                                    <asp:DropDownList id="cboAvanMovilidad" name="cboAvanMovilidad" runat="server" style="width:80px;" AppendDataBoundItems="true" CssClass="combo" filterName="Disp. Movilidad">
                                                        <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td><label id="lblAvanCR" runat="server">CR</label></td>
                                                <td>
                                                    <asp:DropDownList id="cboAvanCR" name="cboAvanCR" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo" onchange="cargarComboAvanzado(5,this[this.selectedIndex].value);" filterName="CR"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td><label title="% disponibilidad del profesional según la aplicación 'Gestión de Disponibles'">Grado Disp. &gt;=</label></td>
                                                <td><asp:TextBox ID="txtAvanGradoDisp" name="txtAvanGradoDisp" style="width:20px;" CssClass="txtNumM" onfocus="fn(this, 3, 0)" onkeyup="CheckValue(this, 100, 0);" SkinID=numero Text="" runat="server" filterName="Grado Disp."/></td>
                                                <td colspan="2">
                                                    <div id="divAvanEstado" runat="server">
                                                        <label style="width:97px;">Estado</label>
                                                        <asp:DropDownList id="cboAvanEstado" name="cboAvanEstado" runat="server" style="width:70px;" AppendDataBoundItems="true" CssClass="combo" filterName="Estado">
                                                            <asp:ListItem Text=""></asp:ListItem>
                                                            <asp:ListItem Selected="True" Text="Activos" Value="A"></asp:ListItem>
                                                            <asp:ListItem Text="Baja" Value="B"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <div id="divAvanTipo" runat="server">
                                                        <label style="width:97px;">Tipo Profesional</label>
                                                        <asp:DropDownList id="cboAvanTipo" name="cboAvanTipo" runat="server" style="width:80px;" AppendDataBoundItems="true" CssClass="combo" filterName="Tipo Profesional">
                                                            <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                            <asp:ListItem Text="Interno" Value="I"></asp:ListItem>
                                                            <asp:ListItem Text="Externo" Value="X"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                                <td colspan="2">
                                                    <div id="divAvanCoste" runat="server">
                                                            <label style="width:97px;" title="Límite de coste por jornada en Euros">Coste &lt;=</label>
                                                            <asp:TextBox ID="txtAvanLimCoste" name="txtAvanLimCoste" style="width:60px;" CssClass="txtNumM" onfocus="fn(this, 5, 0)" SkinID=numero Text="" runat="server" filterName="Coste"/>
                                                            <label title="Euros por jornada">€ / j</label>
                                                    </div>
                                                    <label id="lblAvanSN4" runat="server">SN4</label>
                                                    <label id="lblAvanSN3" runat="server">SN3</label>
                                                    <asp:DropDownList id="cboAvanSN4" name="cboAvanSN4" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo" onchange="cargarComboAvanzado(1,this[this.selectedIndex].value);" filterName="SN4"></asp:DropDownList>
                                                    <asp:DropDownList id="cboAvanSN3" name="cboAvanSN3" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo" onchange="cargarComboAvanzado(1,this[this.selectedIndex].value);" filterName="SN4"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            </table>
                                        </fieldset>
                                        <!-- Titulación -->
                                        <fieldset style="width:815px; margin-top:5px; padding-left:10px; padding-right:0px;">
                                        <legend>Formación académica</legend>
                                            <table class="texto" style="width:800px;" border="0">
                                            <colgroup>
                                                <col style="width:100px;" />
                                                <col style="width:300px;" />
                                                <col style="width:100px;" />
                                                <col style="width:300px;" />
                                            </colgroup>
                                            <tr>
                                                <td colspan="2">
                                                    <fieldset style="width:367px; height:80px; margin-bottom:3px;">
                                                        <legend>
								                            <label id="Label3" style="width:310px;" class="enlace" onclick="getCriterios(4)" runat="server">El CV debe tener las siguientes titulaciones académicas</label>
								                            <img id="Img6" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(4)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
							                            </legend>
                                                        <div id="divAvanTitAcaObl" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 366px; height:32px; margin-top:2px; margin-bottom:5px;">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:350px; height:auto;">
                                                                <table id="tblAvanTitObl" class="texto" style="width:350px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                                     <%=strHTMLAvanTitObl%>
                                                                 </table>
                                                            </div>
                                                        </div>
                                                        <img id="imgAvanTitAcaObl" alt="" src="../../../../Images/imgOpcional.gif" />
                                                        <asp:TextBox ID="txtAvanTitAcaObl" runat="server" style="width:330px;"></asp:TextBox>
                                                    </fieldset>
                                                </td>
                                                <td colspan="2">
                                                    <fieldset style="width:367px; height:80px; margin-bottom:3px;">
                                                        <legend>
								                            <label id="Label5" style="width:330px;" class="enlace" onclick="getCriterios(41)" runat="server">El CV debe tener ALGUNA de las siguientes titulaciones acad.</label>
								                            <img id="Img9" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(41)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
							                            </legend>
                                                        <div id="divAvanTitAcaOpc" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 366px; height:32px; margin-top:2px; margin-bottom:5px;">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:350px; height:auto;">
                                                                <table id="tblAvanTitOpc" class="texto" style="width:350px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                                     <%=strHTMLAvanTitOpc%>
                                                                 </table>
                                                            </div>
                                                        </div>
                                                        <img id="imgAvanTitAcaOpc" alt="" src="../../../../Images/imgOpcional.gif" />
                                                        <asp:TextBox ID="txtAvanTitAcaOpc" runat="server" style="width:330px;"></asp:TextBox>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">Tipología
                                                    <asp:DropDownList id="cboAvanTipologia" name="cboAvanTipologia" runat="server" style="width:130px; margin-left:5px;" AppendDataBoundItems="true" CssClass="combo" filterName="Tipologia">
                                                        <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <label style="margin-left:20px;">TIC's</label>
                                                    <asp:DropDownList id="cboAvanTics" name="cboAvanTics" runat="server" style="width:40px;margin-left:5px;" AppendDataBoundItems="true" CssClass="combo" filterName="TIC's">
                                                            <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                            <asp:ListItem Text="Sí" Value="True"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </td>
                                                <td colspan="2">Modalidad
                                                    <asp:DropDownList id="cboAvanModalidad" name="cboAvanModalidad" runat="server" style="width:130px; margin-left:5px;" AppendDataBoundItems="true" CssClass="combo" filterName="Modalidad">
                                                        <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                 </td>
                                            </tr>
                                            </table>
                                        </fieldset>
                                        <!-- Formación  -->
                                        <fieldset style="width:815px; margin-top:5px; padding-left:10px; padding-right:0px;">
                                        <legend>Acciones formativas y certificados</legend>
                                            <table class="texto" style="width:800px;" border="0">
                                            <colgroup>
                                                <col style="width:100px;" />
                                                <col style="width:300px;" />
                                                <col style="width:100px;" />
                                                <col style="width:300px;" />
                                            </colgroup>
                                            <tr>
                                            <td colspan="2">
                                                <fieldset style="width:367px; height:80px; margin-bottom:3px;">
                                                    <legend>
							                            <label id="Label10" style="width:245px;" class="enlace" onclick="getCriterios(6)" runat="server">El CV debe tener los siguientes certificados</label>
							                            <img id="Img19" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(6)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
						                            </legend>
                                                    <div id="divAvanCertObl" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 366px; height:32px; margin-top:2px; margin-bottom:5px;">
                                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:350px; height:auto;">
                                                            <table id="tblAvanCertObl" class="texto" style="width:350px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                                 <%=strHTMLAvanCertObl%>
                                                             </table>
                                                        </div>
                                                    </div>
                                                    <img id="imgAvanCertObl" alt="" src="../../../../Images/imgOpcional.gif" />
                                                    <asp:TextBox ID="txtAvanCertObl" runat="server" style="width:330px;"></asp:TextBox>
                                                </fieldset>
                                            </td>
                                            <td colspan="2">
                                                <fieldset style="width:367px; height:80px; margin-bottom:3px;">
                                                    <legend>
							                            <label id="Label11" style="width:310px;" class="enlace" onclick="getCriterios(61)" runat="server">El CV debe tener ALGUNO de los siguientes certificados</label>
							                            <img id="Img20" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(61)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
						                            </legend>
                                                    <div id="divAvanCertOpc" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 366px; height:32px; margin-top:2px; margin-bottom:5px;">
                                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:350px; height:auto;">
                                                            <table id="tblAvanCertOpc" class="texto" style="width:350px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                                 <%=strHTMLAvanCertOpc%>
                                                             </table>
                                                        </div>
                                                    </div>
                                                    <img id="imgAvanCertOpc" alt="" src="../../../../Images/imgOpcional.gif" />
                                                    <asp:TextBox ID="txtAvanCertOpc" runat="server" style="width:330px;"></asp:TextBox>
                                                </fieldset>
                                            </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                <fieldset style="width:367px; height:80px; margin-bottom:3px;">
                                                    <legend>
							                            <label id="Label21" style="width:310px;" class="enlace" onclick="getCriterios(15)" runat="server">El CV debe tener las siguientes entidades certificadoras</label>
							                            <img id="Img23" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(15)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
						                            </legend>
                                                    <div id="divAvanCertEntObl" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 366px; height:32px; margin-top:2px; margin-bottom:5px;">
                                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:350px; height:auto;">
                                                            <table id="tblAvanCertEntObl" class="texto" style="width:350px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                                 <%=strHTMLAvanCertEntObl%>
                                                             </table>
                                                        </div>
                                                    </div>
                                                    <img id="imgAvanCertEntObl" alt="" src="../../../../Images/imgOpcional.gif" />
                                                    <asp:TextBox ID="txtAvanCertEntObl" runat="server" style="width:330px;"></asp:TextBox>
                                                </fieldset>
                                                </td>
                                                <td colspan="2">
                                                <fieldset style="width:367px; height:80px; margin-bottom:3px;">
                                                    <legend>
							                            <label id="Label22" style="width:330px;" class="enlace" onclick="getCriterios(151)" runat="server">El CV debe tener ALGUNA de las siguientes ent. certificadoras</label>
							                            <img id="Img24" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(151)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
						                            </legend>
                                                    <div id="divAvanCertEntOpc" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 366px; height:32px; margin-top:2px; margin-bottom:5px;">
                                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:350px; height:auto;">
                                                            <table id="tblAvanCertEntOpc" class="texto" style="width:350px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                                 <%=strHTMLAvanCertEntOpc%>
                                                             </table>
                                                        </div>
                                                    </div>
                                                    <img id="imgAvanCertEntOpc" alt="" src="../../../../Images/imgOpcional.gif" />
                                                    <asp:TextBox ID="txtAvanCertEntOpc" runat="server" style="width:330px;"></asp:TextBox>
                                                </fieldset>
                                                </td>
                                            </tr>
<%--                                            <tr>
                                                <td colspan="4">
                                                    <span title="Año de obtención del certificado">Año &ge;</span>
                                                    <asp:DropDownList id="cboAvanAnoInicio" name="cboAvanAnoInicio" runat="server" style="width:50px;" AppendDataBoundItems="true" CssClass="combo" filterName="Año inicio">
                                                        <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                                                    </asp:DropDownList> 
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td colspan="2">
                                                    <fieldset style="width: 367px; height:80px; margin-bottom:3px;">
                                                        <legend>
                                                            <label id="Label4" style="width: 300px;" class="enlace" onclick="getCriterios(5)" runat="server">El CV debe tener los siguientes entornos tecnológicos</label>
                                                            <img id="Img8" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(5)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
                                                        </legend>
                                                        <div id="divAvanEntTecForObl" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 366px; height:32px; margin-top:2px; margin-bottom:5px;">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:350px; height:auto;">
                                                                <table id="tblAvanEntTecForObl" class="texto" style="width:350px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" filterName="Entorno tecnológico/funcional formacion">
                                                                     <%=strHTMLAvanEntTecForObl%>
                                                                 </table>
                                                            </div>
                                                        </div>
                                                        <img id="imgAvanForEntObl" alt="" src="../../../../Images/imgOpcional.gif" />
                                                        <asp:TextBox ID="txtAvanForEntObl" runat="server" style="width:330px;"></asp:TextBox>
                                                    </fieldset>
                                                </td>
                                                <td colspan="2">
                                                    <fieldset style="width: 367px; height:80px; margin-bottom:3px;">
                                                        <legend>
                                                            <label id="Label19" style="width: 330px;" class="enlace" onclick="getCriterios(51)" runat="server" title="El CV debe tener ALGUNO de los siguientes entornos tecnológicos">El CV debe tener ALGUNO de los siguientes entornos tecno.</label>
                                                            <img id="Img28" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(51)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
                                                        </legend>
                                                        <div id="divAvanEntTecForOpc" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 366px; height:32px; margin-top:2px; margin-bottom:5px;">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:350px; height:auto;">
                                                                <table id="tblAvanEntTecForOpc" class="texto" style="width:350px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" filterName="Entorno tecnológico/funcional formacion">
                                                                     <%=strHTMLAvanEntTecForOpc%>
                                                                 </table>
                                                            </div>
                                                        </div>
                                                        <img id="imgAvanForEntOpc" alt="" src="../../../../Images/imgOpcional.gif" />
                                                        <asp:TextBox ID="txtAvanForEntOpc" runat="server" style="width:330px;"></asp:TextBox>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <fieldset style="width:367px; height:80px; margin-bottom:3px;">
                                                        <legend>
							                                <label id="Label12" style="width:220px;" class="enlace" onclick="getCriterios(14)" runat="server">El CV debe tener los siguientes cursos</label>
							                                <img id="Img21" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(14)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
						                                </legend>
                                                        <div id="divAvanCursoObl" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 366px; height:32px; margin-top:2px; margin-bottom:5px;">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:350px; height:auto;">
                                                                <table id="tblAvanCursoObl" class="texto" style="width:350px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                                     <%=strHTMLAvanCursoObl%>
                                                                 </table>
                                                            </div>
                                                        </div>
                                                        <img id="imgAvanCursoObl" alt="" src="../../../../Images/imgOpcional.gif" />
                                                        <asp:TextBox ID="txtAvanCursoObl" runat="server" style="width:330px;"></asp:TextBox>
                                                    </fieldset>
                                                </td>
                                                <td colspan="2">
                                                    <fieldset style="width:367px; height:80px; margin-bottom:3px;">
                                                        <legend>
							                                <label id="Label13" style="width:285px;" class="enlace" onclick="getCriterios(141)" runat="server">El CV debe tener ALGUNO de los siguientes cursos</label>
							                                <img id="Img22" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(141)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
						                                </legend>
                                                        <div id="divAvanCursoOpc" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 366px; height:32px; margin-top:2px; margin-bottom:5px;">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:350px; height:auto;">
                                                                <table id="tblAvanCursoOpc" class="texto" style="width:350px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                                     <%=strHTMLAvanCursoOpc%>
                                                                 </table>
                                                            </div>
                                                        </div>
                                                        <img id="imgAvanCursoOpc" alt="" src="../../../../Images/imgOpcional.gif" />
                                                        <asp:TextBox ID="txtAvanCursoOpc" runat="server" style="width:330px;"></asp:TextBox>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <span title="Nº de horas mínimas invertidas por el profesional">Tiempo mínimo para cursos (horas)</span>
                                                    <asp:TextBox ID="txtAvanCanTiempo" name="txtAvanCanTiempo" style="width:30px; vertical-align:middle;" CssClass="txtNumM" onkeypress="vtn2(event)" SkinID="Numero" MaxLength="5" Text="" runat="server"  filterName="Minimo de"></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                    <span title="Año de obtención del certificado y realización del curso">Año de certificados y cursos &gt;=</span>
                                                    <asp:DropDownList id="cboAvanAnoCurso" name="cboAvanAnoCurso" runat="server" style="width:50px;" AppendDataBoundItems="true" CssClass="combo" filterName="Año inicio">
                                                        <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                                                    </asp:DropDownList> 
                                                </td>
                                            </tr>
                                            
                                            </table>
                                        </fieldset>
                                        <!-- Idiomas -->
                                        <fieldset style="width:815px; margin-top:5px; padding-left:10px; padding-right:0px;">
                                        <legend>Idiomas</legend>
                                            <table class="texto" style="width:800px;" border="0">
                                            <colgroup>
                                                <col style="width:100px;" />
                                                <col style="width:300px;" />
                                                <col style="width:100px;" />
                                                <col style="width:300px;" />
                                            </colgroup>
                                                <tr>
                                                <td colspan="2">
                                                    <fieldset style="width:367px; height:105px;">
                                                        <legend>
								                            <label id="Label7" style="width:225px;" class="enlace" onclick="getCriterios(7)" runat="server">El CV debe tener los siguientes idiomas</label>
								                            <img id="Img30" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(7)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
							                            </legend>
                                                        <table id="tblAvanCabIdioObl" class="texto" style="width:350px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                             <colgroup>
                                                                <col style="width:140px;" />
                                                                <col style="width:60px;" />
                                                                <col style="width:60px;" />
                                                                <col style="width:55px;" />
                                                                <col style="width:35px;" />
                                                             </colgroup>
                                                             <tr class="TBLINI">
                                                                <td>&nbsp;Idioma</td>
                                                                <td>Lectura</td>
                                                                <td>Escritura</td>
                                                                <td>Oral</td>
                                                                <td>Título</td>
                                                             </tr>
                                                        </table>
                                                        <div id="divAvanIdioObl" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 366px; height:40px; margin-bottom:5px;">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:350px; height:auto;">
                                                                 <table id="tblAvanIdioObl" class="texto" style="width:350px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                                 <colgroup>
                                                                    <col style="width:140px;" />
                                                                    <col style="width:60px;" />
                                                                    <col style="width:60px;" />
                                                                    <col style="width:55px;" />
                                                                    <col style="width:35px;" />
                                                                 </colgroup>
                                                                     <%=strHTMLAvanIdioObl%>
                                                                 </table>
                                                            </div>
                                                        </div>
                                                        <%--<img id="imgAvanIdioObl" alt="" src="../../../../Images/imgOpcional.gif" style="visibility:hidden;" />
                                                        <asp:TextBox ID="txtAvanIdioObl" runat="server" style="width:330px;visibility:hidden;"></asp:TextBox>
                                                        <br />--%>
                                                        <img id="imgAvanIdioTitObl" alt="" src="../../../../Images/imgOpcional.gif" />
                                                        Título
                                                        <asp:TextBox ID="txtAvanIdioTitObl" runat="server" style="width:302px;"></asp:TextBox>
                                                        
                                                    </fieldset>
                                                </td>
                                                <td colspan="2">
                                                    <fieldset style="width:367px; height:105px;">
                                                        <legend>
								                            <label id="Label9" style="width:290px;" class="enlace" onclick="getCriterios(71)" runat="server">El CV debe tener ALGUNO de los siguientes idiomas</label>
								                            <img id="Img31" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(71)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
							                            </legend>
                                                        <table id="tblAvanCabIdioOpc" class="texto" style="width:350px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                             <colgroup>
                                                                <col style="width:140px;" />
                                                                <col style="width:60px;" />
                                                                <col style="width:60px;" />
                                                                <col style="width:55px;" />
                                                                <col style="width:35px;" />
                                                             </colgroup>
                                                             <tr class="TBLINI">
                                                                <td>&nbsp;Idioma</td>
                                                                <td>Lectura</td>
                                                                <td>Escritura</td>
                                                                <td>Oral</td>
                                                                <td>Título</td>
                                                             </tr>
                                                        </table>
                                                        <div id="divAvanIdioOpc" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 366px; height:40px; margin-top:2px; margin-bottom:5px;">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:350px; height:auto;">
                                                                <table id="tblAvanIdioOpc" class="texto" style="width:350px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                                 <colgroup>
                                                                    <col style="width:140px;" />
                                                                    <col style="width:60px;" />
                                                                    <col style="width:60px;" />
                                                                    <col style="width:55px;" />
                                                                    <col style="width:35px;" />
                                                                 </colgroup>
                                                                     <%=strHTMLAvanIdioOpc%>
                                                                 </table>
                                                            </div>
                                                        </div>
                                                        <%--<img id="imgAvanIdioOpc" alt="" src="../../../../Images/imgOpcional.gif" style="visibility:hidden;" />
                                                        <asp:TextBox ID="txtAvanIdioOpc" runat="server" style="width:330px;visibility:hidden;"></asp:TextBox>
                                                        <br />--%>
                                                        <img id="imgAvanIdioTitOpc" alt="" src="../../../../Images/imgOpcional.gif" />
                                                        Título
                                                        <asp:TextBox ID="txtAvanIdioTitOpc" runat="server" style="width:302px;"></asp:TextBox>
                                                    </fieldset>
                                                </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        <!-- Experiencias profesionales -->
                                        <fieldset style="width:815px; margin-top:5px; padding-left:10px; padding-right:0px;">
                                        <legend>Experiencias profesionales</legend>
                                        <fieldset style="width:767px; height:40px; padding-top:10px; margin-bottom:5px;">
                                            <legend>Cliente / Sector</legend>
                                            <span>Cliente</span>
                                            <asp:TextBox ID="txtAvanCuenta" name="txtAvanCuenta" style="width:220px;" Text="" runat="server" watermarktext="Ej: FUNDACION ONCE"  filterName="Cliente"/><img src="../../../../Images/imgSugerencia.png" style="vertical-align:middle; margin-left:3px;" title="En función del texto introducido, el sistema le sugerirá cuentas de clientes existentes" />
                                            <span style="margin-left:15px;">Sector</span>
                                            <asp:DropDownList id="cboAvanSector" name="cboAvanSector" runat="server" style="width:120px;" AppendDataBoundItems="true" CssClass="combo" filterName="Sector">
                                                <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <span style="margin-left:15px;">Tiempo mínimo</span> 
                                            <asp:TextBox ID="txtAvanExpCanTiempo" name="txtAvanExpCanTiempo" style="width:30px; vertical-align:middle;" CssClass="txtNumM" onkeypress="vtn2(event)" SkinID="Numero" MaxLength="5" Text="" runat="server"  filterName="Minimo de"/>
                                            <asp:DropDownList id="cboAvanExpMedTiempo" name="cboAvanExpMedTiempo" runat="server" style="width:70px; vertical-align:middle;" AppendDataBoundItems="true" CssClass="combo" filterName="tiempo">
                                                <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <span style="margin-left:15px;" title="Con experiencia profesional a partir de">Año &gt;=</span>
                                            <asp:DropDownList id="cboAvanExpAnoInicio" name="cboAvanExpAnoInicio" runat="server" style="width:50px;" AppendDataBoundItems="true" CssClass="combo" filterName="Año inicio">
                                                <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                                            </asp:DropDownList> 
                                        </fieldset>
                                        <fieldset style="width:767px; height:40px; padding-top:10px; margin-bottom:5px;">
                                            <legend>Contenido de Experiencias / Funciones</legend>
                                            <img id="imgAvanBuscarFun" alt="" src="../../../../Images/imgOpcional.gif" />
                                            <asp:TextBox ID="txtBuscarEF" name="txtBuscarEF" style="width:220px;" Text="" runat="server"/>
                                             <asp:radiobuttonlist id="rdbBuscarEF" style="display:inline" runat="server" Width="100px" CssClass="radio texto" RepeatLayout="Flow" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal">
		                                        <asp:ListItem Value="Y" Selected="True">&nbsp;Y&nbsp;&nbsp;</asp:ListItem>
		                                        <asp:ListItem Value="O">&nbsp;O&nbsp;&nbsp;</asp:ListItem>
		                                    </asp:radiobuttonlist>
                                            <span style="margin-left:131px;">Tiempo mínimo</span> 
                                            <asp:TextBox ID="txtAvanExpFunCanTiempo" name="txtAvanExpFunCanTiempo" style="width:30px; vertical-align:middle;" CssClass="txtNumM" onkeypress="vtn2(event)" SkinID="Numero" MaxLength="5" Text="" runat="server"  filterName="Minimo de"/>
                                            <asp:DropDownList id="cboAvanExpFunMedTiempo" name="cboAvanExpFunMedTiempo" runat="server" style="width:70px; vertical-align:middle;" AppendDataBoundItems="true" CssClass="combo" filterName="tiempo">
                                                <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <span style="margin-left:15px;" title="Con experiencia profesional a partir de">Año &gt;=</span>
                                            <asp:DropDownList id="cboAvanExpFunAnoInicio" name="cboAvanExpFunAnoInicio" runat="server" style="width:50px;" AppendDataBoundItems="true" CssClass="combo" filterName="Año inicio">
                                                <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                                            </asp:DropDownList> 
                                        </fieldset>
                                        <!-- Perfil -->
                                        <fieldset style="width:767px; padding-top:10px; margin-bottom:5px;">
                                            <legend>
                                                <label title="Búsqueda de perfiles con los hayan trabajado">Perfil</label>
                                                <asp:radiobuttonlist id="rdbBuscarAvanPerfil" style="display:inline; margin-left:10px;" runat="server" Width="100px" CssClass="radio texto" RepeatLayout="Flow" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal">
		                                            <asp:ListItem Value="Y" Selected="True">&nbsp;Y&nbsp;&nbsp;</asp:ListItem>
		                                            <asp:ListItem Value="O">&nbsp;O&nbsp;&nbsp;</asp:ListItem>
		                                        </asp:radiobuttonlist>
                                                <img id="Img25" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(201)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
                                            </legend>
                                            <table style="width:100%" border="0">
                                                <tr>
                                                    <td style="width: 40px;"><img src="../../../../Images/imgAddDelTabla.png" onclick="getPerfilesAux('PER');" class="MANO" title="Añadir/eliminar los perfiles a buscar"/></td>
                                                    <td style="padding-left: 5px;">
                                                        <table id="Table6" class="texto" style="width:435px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                             <colgroup>
                                                                <col style="width:250px;" />
                                                                <col style="width:130px;" />
                                                                <col style="width:55px;" />
                                                             </colgroup>
                                                             <tr class="TBLINI">
                                                                <td>&nbsp;Perfil</td>
                                                                <td>Tiempo mínimo</td>
                                                                <td>A&ntilde;o >=</td>
                                                             </tr>
                                                        </table>
                                                        <div id="div1" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 451px; height:40px;">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:435px; height:auto;">
                                                                 <table id="tblEPAvan_Perfil" class="texto" style="width:435px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                                                                 <colgroup>
                                                                    <col style="width:250px;" />
                                                                    <col style="width:130px;" />
                                                                    <col style="width:55px;" />
                                                                 </colgroup>
                                                                 </table>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table> 
                                        </fieldset>
                                        <!-- Perfil / Entorno tecnológico -->
                                        <fieldset style="width:767px; padding-top:10px; margin-bottom:5px;">
                                            <legend>
                                                <label title="Búsqueda de perfiles que hayan trabajado en unos entornos tecnológicos en concreto con esos perfiles">Perfil / Entorno tecnológico</label>
                                                <img id="Img26" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(202)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
                                            </legend>
                                            <table style="width:100%" border="0">
                                                <tr>
                                                    <td style="width: 40px;"><img src="../../../../Images/imgAddDelTabla.png" onclick="getPerfilesAux('PERENT');" class="MANO" title="Añadir/eliminar los perfiles y entornos a buscar"/></td>
                                                    <td style="padding-left: 5px;">
                                                        <table class="tituloDoble" style="width:700px; height:34px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                             <colgroup>
                                                                <col style="width:160px;" />
                                                                <col style="width:120px;" />
                                                                <col style="width:80px;" />
                                                                <col style="width:70px;" />
                                                                <col style="width:120px;" />
                                                                <col style="width:80px;" />
                                                                <col style="width:70px;" />
                                                             </colgroup>
                                                             <tr style="height:17px; vertical-align:middle;">
                                                                <td rowspan="2">&nbsp;Perfil</td>
                                                                <td colspan="3">Debe tener los siguientes entornos</td>
                                                                <td colspan="3">Y alguno de los siguientes entornos</td>
                                                             </tr>
                                                             <tr style="height:17px; vertical-align:middle;">
                                                                <td>Denominación</td>
                                                                <td title="Tiempo mínimo">T. min.</td>
                                                                <td>A&ntilde;o >=</td>
                                                                <td>Denominación</td>
                                                                <td title="Tiempo mínimo">T. min.</td>
                                                                <td>A&ntilde;o >=</td>
                                                             </tr>
                                                        </table>
                                                        <div style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 716px; height:80px;">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT40.gif'); width:700px; height:auto;">
                                                                 <table id="tblEPAvan_PerfilEntorno" class="texto" style="width:700px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                                 <colgroup>
                                                                    <col style="width:160px;" />
                                                                    <col style="width:270px;" />
                                                                    <col style="width:270px;" />
                                                                 </colgroup>
                                                                 </table>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table> 
                                        </fieldset>
                                        <!-- Entorno tecnológico -->
                                        <fieldset style="width:767px; padding-top:10px; margin-bottom:5px;">
                                            <legend>
                                                <label title="Búsqueda de entornos tecnológicos en los que hayan trabajado">Entorno tecnológico</label>
                                                 <asp:radiobuttonlist id="rdbBuscarAvanEntorno" style="display:inline; margin-left:10px;" runat="server" Width="100px" CssClass="radio texto" RepeatLayout="Flow" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal">
		                                            <asp:ListItem Value="Y" Selected="True">&nbsp;Y&nbsp;&nbsp;</asp:ListItem>
		                                            <asp:ListItem Value="O">&nbsp;O&nbsp;&nbsp;</asp:ListItem>
		                                        </asp:radiobuttonlist>
                                                <img id="Img27" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(203)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
                                            </legend>
                                            <table style="width:100%" border="0">
                                                <tr>
                                                    <td style="width: 40px;"><img src="../../../../Images/imgAddDelTabla.png" onclick="getPerfilesAux('ENT');" class="MANO" title="Añadir/eliminar los entornos a buscar"/></td>
                                                    <td style="padding-left: 5px;">
                                                        <table id="Table13" class="texto" style="width:435px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                             <colgroup>
                                                                <col style="width:250px;" />
                                                                <col style="width:130px;" />
                                                                <col style="width:55px;" />
                                                             </colgroup>
                                                             <tr class="TBLINI">
                                                                <td>&nbsp;Entorno</td>
                                                                <td>Tiempo mínimo</td>
                                                                <td>A&ntilde;o >=</td>
                                                             </tr>
                                                        </table>
                                                        <div id="div6" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 451px; height:40px;">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:435px; height:auto;">
                                                                 <table id="tblEPAvan_Entorno" class="texto" style="width:435px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                                                                 <colgroup>
                                                                    <col style="width:250px;" />
                                                                    <col style="width:130px;" />
                                                                    <col style="width:55px;" />
                                                                 </colgroup>
                                                                 </table>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table> 
                                        </fieldset>
                                        <!-- Entorno tecnológico / Perfil -->
                                        <fieldset style="width:767px; padding-top:10px; margin-bottom:5px;">
                                            <legend>
                                                <label title="Búsqueda de entornos tecnológicos en los que hayan trabajado con unos perfiles en concreto">Entorno tecnológico / Perfil</label>
                                                <img id="Img29" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(204)" alt="" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"/>
                                            </legend>
                                            <table style="width:100%" border="0">
                                                <tr>
                                                    <td style="width: 40px;"><img src="../../../../Images/imgAddDelTabla.png" onclick="getPerfilesAux('ENTPER');" class="MANO" title="Añadir/eliminar los entornos y perfiles a buscar" /></td>
                                                    <td style="padding-left: 5px;">
                                                        <table class="tituloDoble" style="width:700px; height:34px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                             <colgroup>
                                                                <col style="width:160px;" />
                                                                <col style="width:120px;" />
                                                                <col style="width:80px;" />
                                                                <col style="width:70px;" />
                                                                <col style="width:120px;" />
                                                                <col style="width:80px;" />
                                                                <col style="width:70px;" />
                                                             </colgroup>
                                                             <tr style="height:17px; vertical-align:middle;">
                                                                <td rowspan="2">&nbsp;Entorno</td>
                                                                <td colspan="3">Debe tener los siguientes perfiles</td>
                                                                <td colspan="3">Y alguno de los siguientes perfiles</td>
                                                             </tr>
                                                             <tr style="height:17px; vertical-align:middle;">
                                                                <td>Denominación</td>
                                                                <td title="Tiempo mínimo">T. min.</td>
                                                                <td>A&ntilde;o >=</td>
                                                                <td>Denominación</td>
                                                                <td title="Tiempo mínimo">T. min.</td>
                                                                <td>A&ntilde;o >=</td>
                                                             </tr>
                                                        </table>
                                                        <div style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 716px; height:80px;">
                                                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT40.gif'); width:700px; height:auto;">
                                                                 <table id="tblEPAvan_EntornoPerfil" class="texto" style="width:700px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0" >
                                                                 <colgroup>
                                                                    <col style="width:160px;" />
                                                                    <col style="width:270px;" />
                                                                    <col style="width:270px;" />
                                                                 </colgroup>
                                                                 </table>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table> 
                                        </fieldset>
                                        </fieldset>  
                                        <br />
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

<img id="imgPestHorizontalCadena" src="../../../../Images/imgPestCadena.png" alt="" 
    style="Z-INDEX: 1;position:absolute; left:213px; top:125px; cursor:pointer;" onclick="HideShowPest('cadena')" />
<div id="divPestRetrCadena" style="Z-INDEX: 1;position:absolute; left:20px; top:125px; width:970px; height:440px; clip:rect(auto auto 0px auto);">
    <table style="width:870px;" cellpadding="0" cellspacing="0" border="0">
    <tr valign="top">
        <td>
            <table style="width:850px; height:440px;" cellpadding="0" cellspacing="0" border="0">
                <tr>
		            <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../../Images/Tabla/5.gif" style="padding: 3px" valign="top">
                        <!-- Inicio del contenido propio de la página -->
                        <div align="right" style="padding-right:10px; padding-top:5px;">
<%--                            <img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias('C')" style="cursor:pointer; vertical-align:bottom;">&nbsp;
                            <img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia('C')" style="cursor:pointer; vertical-align:bottom;">&nbsp;
                            <img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia('C')" style="cursor:pointer; vertical-align:bottom;">&nbsp;--%>
                            <img src='../../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="limpiarCadena();" style="cursor:pointer; vertical-align:bottom;">                                
                            &nbsp;&nbsp;&nbsp;
                            <button id="btnObtenerCadena" type="button" onclick="generarCadena()" class="btnH25W85" hidefocus="hidefocus" onmouseover="mostrarCursor(this)" runat="server" style="display:inline;">
                                <span><img src="../../../../images/imgObtener.gif" alt="" />Obtener</span>
                            </button>
                        </div>
                        <fieldset id="fldDP">
                            <legend>Datos Personales-Organizativos</legend>
                            <table style="width:820px; margin-top:5px;" cellpadding="3" cellspacing="0" border="0">
                            <colgroup>
                                <col style="width:100px;" />
                                <col style="width:330px;" />
                                <col style="width:90px;" />
                                <col style="width:300px;" />
                            </colgroup>
                            <tr>
								<td><label style="display:none;">Perfil de mercado</label></td>
                                <td>
									<asp:DropDownList id="cboPerfilProC" name="cboPerfilProC" runat="server" style="width:280px;display:none;" AppendDataBoundItems="true" CssClass="combo" filterName="Perfil de mercado">
                                        <asp:ListItem Selected="True" Text=""></asp:ListItem>
									</asp:DropDownList>
								</td>
                                <td><label id="lblSN4C" runat="server">SN4</label></td>
                                <td><asp:DropDownList id="cboSNC4" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo" onchange="cargarComboC(1,this[this.selectedIndex].value);" filterName="SN4">
                                        </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td><label title="Centro de trabajo físico">Centro</label></td>
                                <td>
									<asp:DropDownList id="cboCentroC" name="cboCentroC" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo" filterName="Centro">
                                        <asp:ListItem Selected="True" Text=""></asp:ListItem>
									</asp:DropDownList>
								</td>
                                <td><label id="lblSN3C" runat="server">SN3</label></td>
                                <td><asp:DropDownList id="cboSNC3" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo"  onchange="cargarComboC(2,this[this.selectedIndex].value);" filterName="SN3">
                                        </asp:DropDownList> </td>
                            </tr>
                            <tr>
								<td><label title="Interesado en trayectoria internacional">Trayectoria Int.</label></td>
                                <td>
									<asp:DropDownList id="cboIntTrayIntC" name="cboIntTrayIntC" runat="server" style="width:40px;" AppendDataBoundItems="true" CssClass="combo" filterName="Trayectoria Int.">
                                            <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                            <asp:ListItem Text="Sí" Value="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                    </asp:DropDownList>
								</td>
                                <td><label id="lblSN2C" runat="server">SN2</label></td>
                                <td>
									<asp:DropDownList id="cboSNC2" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo"  onchange="cargarComboC(3,this[this.selectedIndex].value);" filterName="SN2">
									</asp:DropDownList>
								</td>
                            </tr>
                            <tr> 
                                <td><label title="Disponibilidad para la movilidad geográfica">Movilidad geog.</label></td>
                                <td>
									<asp:DropDownList id="cboMovilidadC" name="cboMovilidadC" runat="server" style="width:80px;" AppendDataBoundItems="true" CssClass="combo" filterName="Disp. Movilidad">
                                        <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                    </asp:DropDownList>
								</td>
                                <td><label id="lblSN1C" runat="server">SN1</label></td>
                                <td>
									<asp:DropDownList id="cboSNC1" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo"  onchange="cargarComboC(4,this[this.selectedIndex].value);" filterName="SN1">
                                    </asp:DropDownList>
								</td>
                            </tr>
                            <tr>
								<td><label title="% disponibilidad del profesional según la aplicación 'Gestión de Disponibles'">Grado Disp. &gt;=</label></td>
                                <td><asp:TextBox ID="txtGradoDispC" style="width:20px;" CssClass="txtNumM" onfocus="fn(this, 3, 0)" onkeyup="CheckValue(this, 100, 0);" SkinID=numero Text="" runat="server" filterName="Grado Disp."/></td>
                                <td><label id="lblCRC" runat="server">CR</label></td>
                                <td>
									<asp:DropDownList id="cboCRC" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo" onchange="cargarComboC(5,this[this.selectedIndex].value);" filterName="CR">
                                    </asp:DropDownList>
								</td>
                            </tr>
                            <tr>
								<td colspan="2">
									<div id="divTipoC" runat="server">
										<label style="width:97px;">Tipo Profesional</label>
										<asp:DropDownList id="cboTipoC" name="cboTipoC" runat="server" style="width:80px;" AppendDataBoundItems="true" CssClass="combo" filterName="Tipo Profesional">
											<asp:ListItem Selected="True" Text=""></asp:ListItem>
											<asp:ListItem Text="Interno" Value="I"></asp:ListItem>
											<asp:ListItem Text="Externo" Value="X"></asp:ListItem>
										</asp:DropDownList>
									</div>
								</td>
                                <td colspan="2">
                                    <div id="divEstadoC" runat="server">
                                            <label style="width:87px;">Estado</label>
                                            <asp:DropDownList id="cboEstadoC" name="cboEstadoC" runat="server" style="width:70px;" AppendDataBoundItems="true" CssClass="combo" filterName="Estado">
                                                <asp:ListItem Text=""></asp:ListItem>
                                                <asp:ListItem Selected="True" Text="Activos" Value="A"></asp:ListItem>
                                                <asp:ListItem Text="Baja" Value="B"></asp:ListItem>
                                            </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
									<div id="divCosteC" runat="server">
										<label style="width:97px;" title="Límite de coste por jornada en Euros">Coste &lt;=</label>
                                        <asp:TextBox ID="txtLimCosteC" name="txtLimCosteC" style="width:60px;" CssClass="txtNumM" onfocus="fn(this, 5, 0)" SkinID=numero Text="" runat="server" filterName="Coste"/>
                                        <label title="Euros por jornada">€ / j</label>
									</div>
								</td>
                            </tr>
                            </table>
                        </fieldset>
                        <br />
                        <fieldset id="fldCadenas">
                            <legend>
                                <label>Cadena</label>
                                <asp:radiobuttonlist id="rdbCadena" runat="server" style="display:inline; margin-top:5px;" Width="100px" CssClass="radio texto" RepeatLayout="Flow" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="A" Selected="True">&nbsp;Y&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="O">&nbsp;O&nbsp;&nbsp;</asp:ListItem>
                                </asp:radiobuttonlist>
                                <label>(El operador lógico se aplica para cada uno de los elementos de la cadena separados por punto y coma)</label>
                            </legend>
                            <div style="margin-left:40px; margin-top:10px;">
                                <table style="width:710px;">
                                    <colgroup><col style="width:510px;"/><col style="width:200px;"/></colgroup>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTextoCadena" TextMode="MultiLine" SkinID="Multi" onkeypress="quitarCaracteres(event)" Columns="100" Rows="6" MaxLength="500" style="height:125px; width:500px;" runat="server"></asp:TextBox><br />
                                        </td>
                                        <td>
                                            <input type="checkbox" id="chkCadFormacion" style="margin-top:3px; " /><img id="imgCadFA" alt="" src="../../../../Images/imgOpcional.gif" /><label id="lblFACadena" style="vertical-align:top; margin-top:6px; margin-left:5px;">Formación académica</label><br />
                                            <input type="checkbox" id="chkCadCursos" style="margin-top:3px; " /><img id="imgCadCU" alt="" src="../../../../Images/imgOpcional.gif" /><label id="lblCUCadena" style="vertical-align:top;  margin-top:6px; margin-left:5px;">Acciones formativas</label><br />
                                            <input type="checkbox" id="chkCertExam" style="margin-top:3px; " /><img id="imgCadCE" alt="" src="../../../../Images/imgOpcional.gif" /><label id="lblCECadena" style="vertical-align:top; margin-top:6px; margin-left:5px;">Certificados/Exámenes</label><br />                                            
                                            <input type="checkbox" id="chkCadIdioma" style="margin-top:3px; " /><img id="imgCadID" alt="" src="../../../../Images/imgOpcional.gif" /><label id="lblIDCadena" style="vertical-align:top;  margin-top:6px; margin-left:5px;">Idiomas</label><br />
                                            <input type="checkbox" id="chkCadExperiencia" style="margin-top:3px;" /><img id="imgCadEX" alt="" src="../../../../Images/imgOpcional.gif" /><label id="lblEXCadena" style="vertical-align:top;  margin-top:6px; margin-left:5px;">Experiencia</label><br />
                                            <input type="checkbox" id="chkCadOtros" style="margin-top:3px; " /><img id="imgCadOT" alt="" src="../../../../Images/imgOpcional.gif" /><label id="lblOTCadena" style="vertical-align:top; margin-top:6px; margin-left:5px;">Otros</label><br />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <label>Si necesitas ayuda para establecer los filtros de búsqueda, pulsa <span class="enlace" onclick="mostrarFondoAyuda('CADENA')";>aquí</span>.</label>
                        </fieldset>
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

<img id="imgPestHorizontalQuery" src="../../../../Images/imgPestQuery.png" alt="" 
    style="Z-INDEX: 1;position:absolute; left:300px; top:125px; cursor:pointer;" onclick="HideShowPest('query')" />
<div id="divPestRetrQuery" 
    style="Z-INDEX: 1;position:absolute; left:20px; top:125px; width:970px; height:520px; clip:rect(auto auto 0px auto);">
    <table style="width:870px;" cellpadding="0" cellspacing="0" border="0">
    <tr valign="top">
        <td>
            <table style="width:850px; height:520px;" cellpadding="0" cellspacing="0" border="0">
                <tr>
		            <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../../Images/Tabla/5.gif" style="padding: 3px" valign="top">
                        <!-- Inicio del contenido propio de la página -->
                        <div align="right" style="padding-right:10px; padding-top:5px;">
<%--                            <img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias('C')" style="cursor:pointer; vertical-align:bottom;">&nbsp;
                            <img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia('C')" style="cursor:pointer; vertical-align:bottom;">&nbsp;
                            <img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia('C')" style="cursor:pointer; vertical-align:bottom;">&nbsp;--%>
                            <img src='../../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="limpiarQuery();" style="cursor:pointer; vertical-align:bottom;">                                
                            &nbsp;&nbsp;&nbsp;
                            <button id="btnObtenerQuery" type="button" onclick="generarQuery()" disabled="true" class="btnH25W85" hidefocus="hidefocus" onmouseover="mostrarCursor(this)" runat="server" style="display:inline;">
                                <span><img src="../../../../images/imgObtener.gif" alt="" />Obtener</span>
                            </button>
                        </div>
                        <fieldset id="fldDPQuery" style="height:170px;">
                            <legend>Datos Personales-Organizativos</legend>
                            <table style="width:820px; margin-top:5px;" cellpadding="3" cellspacing="0" border="0">
                            <colgroup>
                                <col style="width:100px;" />
                                <col style="width:330px;" />
                                <col style="width:90px;" />
                                <col style="width:300px;" />
                            </colgroup>
                            <tr>
								<td><label style="display:none;">Perfil de mercado</label></td>
                                <td>
									<asp:DropDownList id="cboPerfilProQ" name="cboPerfilProQ" runat="server" style="width:280px;display:none;" AppendDataBoundItems="true" CssClass="combo" filterName="Perfil de mercado">
                                        <asp:ListItem Selected="True" Text=""></asp:ListItem>
									</asp:DropDownList>
								</td>
                                <td><label id="lblSN4Q" runat="server">SN4</label></td>
                                <td><asp:DropDownList id="cboSNQ4" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo" onchange="cargarComboQ(1,this[this.selectedIndex].value);" filterName="SN4">
                                        </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td><label title="Centro de trabajo físico">Centro</label></td>
                                <td>
									<asp:DropDownList id="cboCentroQ" name="cboCentroQ" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo" filterName="Centro">
                                        <asp:ListItem Selected="True" Text=""></asp:ListItem>
									</asp:DropDownList>
								</td>
                                <td><label id="lblSN3Q" runat="server">SN3</label></td>
                                <td><asp:DropDownList id="cboSNQ3" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo"  onchange="cargarComboQ(2,this[this.selectedIndex].value);" filterName="SN3">
                                        </asp:DropDownList> </td>
                            </tr>
                            <tr>
								<td><label title="Interesado en trayectoria internacional">Trayectoria Int.</label></td>
                                <td>
									<asp:DropDownList id="cboIntTrayIntQ" name="cboIntTrayIntQ" runat="server" style="width:40px;" AppendDataBoundItems="true" CssClass="combo" filterName="Trayectoria Int.">
                                            <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                            <asp:ListItem Text="Sí" Value="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                    </asp:DropDownList>
								</td>
                                <td><label id="lblSN2Q" runat="server">SN2</label></td>
                                <td>
									<asp:DropDownList id="cboSNQ2" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo"  onchange="cargarComboQ(3,this[this.selectedIndex].value);" filterName="SN2">
									</asp:DropDownList>
								</td>
                            </tr>
                            <tr> 
                                <td><label title="Disponibilidad para la movilidad geográfica">Movilidad geog.</label></td>
                                <td>
									<asp:DropDownList id="cboMovilidadQ" name="cboMovilidadQ" runat="server" style="width:80px;" AppendDataBoundItems="true" CssClass="combo" filterName="Disp. Movilidad">
                                        <asp:ListItem Selected="True" Text=""></asp:ListItem>
                                    </asp:DropDownList>
								</td>
                                <td><label id="lblSN1Q" runat="server">SN1</label></td>
                                <td>
									<asp:DropDownList id="cboSNQ1" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo"  onchange="cargarComboQ(4,this[this.selectedIndex].value);" filterName="SN1">
                                    </asp:DropDownList>
								</td>
                            </tr>
                            <tr>
								<td><label title="% disponibilidad del profesional según la aplicación 'Gestión de Disponibles'">Grado Disp. &gt;=</label></td>
                                <td><asp:TextBox ID="txtGradoDispQ" style="width:20px;" CssClass="txtNumM" onfocus="fn(this, 3, 0)" onkeyup="CheckValue(this, 100, 0);" SkinID=numero Text="" runat="server" filterName="Grado Disp."/></td>
                                <td><label id="lblCRQ" runat="server">CR</label></td>
                                <td>
									<asp:DropDownList id="cboCRQ" runat="server" style="width:280px;" AppendDataBoundItems="true" CssClass="combo" onchange="cargarComboQ(5,this[this.selectedIndex].value);" filterName="CR">
                                    </asp:DropDownList>
								</td>
                            </tr>
                            <tr>
								<td colspan="2">
                                    <table style="width:425px;">
                                        <colgroup><col style="width:225px;" /><col style="width:200px;" /></colgroup>
                                        <tr>
                                            <td>
									            <div id="divTipoQ" runat="server">
										            <label style="width:97px;">Tipo Profesional</label>
										            <asp:DropDownList id="cboTipoQ" name="cboTipoQ" runat="server" style="width:80px;" AppendDataBoundItems="true" CssClass="combo" filterName="Tipo Profesional">
											            <asp:ListItem Selected="True" Text=""></asp:ListItem>
											            <asp:ListItem Text="Interno" Value="I"></asp:ListItem>
											            <asp:ListItem Text="Externo" Value="X"></asp:ListItem>
										            </asp:DropDownList>
									            </div>
                                            </td>
                                            <td>
									            <div id="divCosteQ" runat="server">
										            <label style="width:50px;" title="Límite de coste por jornada en Euros">Coste &lt;=</label>
                                                    <asp:TextBox ID="txtLimCosteQ" name="txtLimCosteQ" style="width:60px;" CssClass="txtNumM" onfocus="fn(this, 5, 0)" SkinID="numero" Text="" runat="server" filterName="Coste"/>
                                                    <label title="Euros por jornada">€ / j</label>
									            </div>
                                            </td>
                                        </tr>
                                    </table>
								</td>
                                <td colspan="2">
                                    <div id="divEstadoQ" runat="server">
                                            <label style="width:87px;">Estado</label>
                                            <asp:DropDownList id="cboEstadoQ" name="cboEstadoQ" runat="server" style="width:70px;" AppendDataBoundItems="true" CssClass="combo" filterName="Estado">
                                                <asp:ListItem Text=""></asp:ListItem>
                                                <asp:ListItem Selected="True" Text="Activos" Value="A"></asp:ListItem>
                                                <asp:ListItem Text="Baja" Value="B"></asp:ListItem>
                                            </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                            </table>
                        </fieldset>
                        <fieldset id="fldCadenasQuery">
                        <legend>
                            <label>Cadena</label>
                            <asp:radiobuttonlist id="rdbOperador" runat="server" style="display:inline"  Width="100px" CssClass="radio texto" RepeatLayout="Flow" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal">
                                <asp:ListItem Value="A" Selected="True">&nbsp;Y&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="O">&nbsp;O&nbsp;&nbsp;</asp:ListItem>
                            </asp:radiobuttonlist>
                            <label>(El operador lógico se aplica para cada una de las secciones entre ellas)</label>
                            </legend>
                        <div id="filtrosCadenaQuery">
                            <div style="margin-top:5px">
                                <img id="imgQueFA" alt="" src="../../../../Images/imgOpcional.gif" />
                                <label id ="lblFAQuery">Formación académica</label>
                                <input id="txtCadenaFA" maxlength="70" class="txtM" type="text" validado="1" onkeyup="validarCadena(this.id,'imgSFA');" onkeypress="quitarCaracteres(event)"/>
                                <img class="semaforo" id="imgSFA" alt="Semáforo" src="../../../../Images/imgSemaforo16h.gif" />
                            </div>
                            <div>
                                <img id="imgQueCR" alt="" src="../../../../Images/imgOpcional.gif" />
                                <label id="lblCRQuery" title="Acciones formativas recibidas">Ac. formativas recibidas</label>
                                <input id="txtCadenaCR" maxlength="70" class="txtM" type="text" validado="1" name="txtCadenaCR" onclick="activarBotones('H');" onkeyup="validarCadena(this.id,'imgSCR');" onkeypress="quitarCaracteres(event)"/>
                                <img class="semaforo" id="imgSCR" alt="Semáforo" src="../../../../Images/imgSemaforo16h.gif" />
                            </div>
                            <div>
                                <img id="imgQueCI" alt="" src="../../../../Images/imgOpcional.gif" />
                                <label id="lblCIQuery" title="Acciones formativas impartidas">Ac. formativas impartidas</label>
                                <input id="txtCadenaCI" maxlength="70" class="txtM" type="text" validado="1" name="txtCadenaCI" onclick="activarBotones('H');" onkeyup="validarCadena(this.id,'imgSCI');" onkeypress="quitarCaracteres(event)"/>
                                <img class="semaforo" id="imgSCI" alt="Semáforo" src="../../../../Images/imgSemaforo16h.gif" />
                            </div>
                            <div>
                                <img id="imgQueCE" alt="" src="../../../../Images/imgOpcional.gif" />
                                <label id="lblCertQuery">Certificados</label>
                                <input id="txtCadenaCERT" maxlength="70" class="txtM" type="text" validado="1" name="txtCadenaCERT" onclick="activarBotones('H');" onkeyup="validarCadena(this.id,'imgSCERT');" onkeypress="quitarCaracteres(event)"/>
                                <img class="semaforo" id="imgSCERT" alt="Semáforo" src="../../../../Images/imgSemaforo16h.gif" />
                            </div>
                            <div>
                                <img id="imgQueEXA" alt="" src="../../../../Images/imgOpcional.gif" />
                                <label id="lblExamenQuery">Exámenes</label>
                                <input id="txtCadenaEX" maxlength="70" class="txtM" type="text" validado="1" name="txtCadenaEX" onclick="activarBotones('H');" onkeyup="validarCadena(this.id,'imgSEX');" onkeypress="quitarCaracteres(event)"/>
                                <img class="semaforo" id="imgSEX" alt="Semáforo" src="../../../../Images/imgSemaforo16h.gif" />
                            </div>
                            <div>
                                <img id="imgQueID" alt="" src="../../../../Images/imgOpcional.gif" />
                                <label id="lblIdiomaQuery">Idiomas</label>
                                <input id="txtCadenaID" maxlength="70" class="txtM" type="text" validado="1" name="txtCadenaID" onclick="activarBotones('H');" onkeyup="validarCadena(this.id,'imgSID');" onkeypress="quitarCaracteres(event)"/>
                                <img class="semaforo" id="imgSID" alt="Semáforo" src="../../../../Images/imgSemaforo16h.gif" />
                            </div>
                            <div>
                                <img id="imgQueEXP" alt="" src="../../../../Images/imgOpcional.gif" />
                                <label id="lblExperienciaQuery">Experiencia profesional</label>
                                <input id="txtCadenaEXP" maxlength="70" class="txtM" type="text" validado="1" name="txtCadenaEXP" onclick="activarBotones('H');" onkeyup="validarCadena(this.id,'imgSEXP');" onkeypress="quitarCaracteres(event)"/>
                                <img class="semaforo" id="imgSEXP" alt="Semáforo" src="../../../../Images/imgSemaforo16h.gif" />
                            </div>
                            <div>
                                <img id="imgQueOT" alt="" src="../../../../Images/imgOpcional.gif" />
                                <label id="lblOTQuery">Otros</label>
                                <input id="txtCadenaOT" maxlength="70" class="txtM" type="text" validado="1" onclick="activarBotones('H');" onkeyup="validarCadena(this.id,'imgSOT');" onkeypress="quitarCaracteres(event)"/>
                                <img class="semaforo" id="imgSOT" alt="Semáforo" src="../../../../Images/imgSemaforo16h.gif" />
                            </div>
                        </div> 
                        <div id="caracteresReservados" style="text-align:center">
                            <button id="btnAnd" type="button" class="btnH25W35" hidefocus="hidefocus" onmouseover="mostrarCursor(this)">
                                AND
                            </button>
                            <button id="btnOr"  type="button" class="btnH25W35" hidefocus="hidefocus" onmouseover="mostrarCursor(this)">
                                OR
                            </button>
                            <button id="btnPA" type="button" class="btnH25W35" hidefocus="hidefocus" onmouseover="mostrarCursor(this)">
                                (
                            </button>
                            <button id="btnPC" type="button" class="btnH25W35" hidefocus="hidefocus" onmouseover="mostrarCursor(this)">
                                )
                            </button>
                            <button id="btnInte"  type="button" class="btnH25W35" hidefocus="hidefocus" onmouseover="mostrarCursor(this)">
                                ?
                            </button>
                            <button id="btnAste" type="button" class="btnH25W35" hidefocus="hidefocus" onmouseover="mostrarCursor(this)">
                                *
                            </button>
                            <button id="btnComilla" type="button" class="btnH25W35" hidefocus="hidefocus" onmouseover="mostrarCursor(this)">
                                "
                             </button>
                        </div>
                        <label style="margin-top:3px;">Si necesitas ayuda para confeccionar las cadenas, pulsa <span class="enlace" onclick="mostrarFondoAyuda('QUERY')";>aquí</span>.</label>

                        </fieldset>
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


<div style="float:left; width:600px; margin-top:18px; margin-left:5px">
    <table id="tblTitulo" style="width:570px; height:17px;  float:left;">
        <colgroup>
            <col style="width:50px;" />
            <col style="width:280px;" />
            <col style="width:240px;" />
        </colgroup>
        <tr class="TBLINI">
            <td>
                <img src="../../../../images/botones/imgmarcar.gif" onclick="mdTabla(1)" title="Marca todas las líneas para ser procesadas" style="cursor:pointer; margin-left:2px;" />
                <img src="../../../../images/botones/imgdesmarcar.gif" onclick="mdTabla(0)" title="Desmarca todas las líneas" style="cursor:pointer;" />   
            </td>
            <td>Profesional</td>
            <td></td>
        </tr>  
    </table>
    <img id="imgExcel" src="../../../../Images/imgExcelAnim.gif" alt="Exporta a Excel el contenido de la tabla" class="MANO" onclick="mostrarProcesando();setTimeout('excel();',500);" style="float:left; padding-top:5px;"/>
    <div id="divCatalogo" style="overflow-x:hidden; overflow-y:auto; width:586px; height:480px;" runat="server" name="divCatalogo" onscroll="scrollTablaProf()">
    <div style="background-image:url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:570px; height:auto;">
    </div>
    </div>
    <table id="tblResultado" style="width:570px; height:17px; margin-bottom:5px;">
    <tr class="TBLFIN">
        <td style="padding-left:3px;">
        </td>
    </tr>
    </table>
    <img class="ICO" src="../../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
    <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
    <img class="ICO" src="../../../../Images/imgWarning.png" />&nbsp;Pendiente de Cumplimentar
</div>
<div style ="float:left; width:380px; height:500px; margin-left:5px;">
    <eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="380px" 
				MultiPageID="mpContenido" 
				ClientSideOnLoad="CrearPestanas" 
				ClientSideOnItemClick="getPestana">
		<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
			<Items>
				<eo:TabItem ToolTip="" Width="90"></eo:TabItem>
                <eo:TabItem ToolTip="" Width="90"></eo:TabItem>
				<eo:TabItem ToolTip="" Width="90"></eo:TabItem>
				<eo:TabItem ToolTip="" Width="90"></eo:TabItem>
			</Items>
			</TopGroup>
		<LookItems>
				<eo:TabItem ItemID="_Default" 
					LeftIcon-Url="~/Images/Pestanas/normal_left.gif"
					LeftIcon-HoverUrl="~/Images/Pestanas/hover_left.gif"
					LeftIcon-SelectedUrl="~/Images/Pestanas/selected_left.gif"
					Image-Url="~/Images/Pestanas/normal_bg.gif"
					Image-HoverUrl="~/Images/Pestanas/hover_bg.gif" 
					Image-SelectedUrl="~/Images/Pestanas/selected_bg.gif" 
					RightIcon-Url="~/Images/Pestanas/normal_right.gif"
					RightIcon-HoverUrl="~/Images/Pestanas/hover_right.gif"
					RightIcon-SelectedUrl="~/Images/Pestanas/selected_right.gif"
					NormalStyle-CssClass="TabItemNormal"
					HoverStyle-CssClass="TabItemHover"
					SelectedStyle-CssClass="TabItemSelected"
					DisabledStyle-CssClass="TabItemDisabled"
					Image-Mode="TextBackground" Image-BackgroundRepeat="RepeatX">
				</eo:TabItem>
		</LookItems>
	</eo:TabStrip>	
    <eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="380px" Height="470px">
        <eo:PageView ID="PageView1" CssClass="PageView" runat="server" style="height:470px;">				
        <!-- Pestaña 1 -->
            <fieldset style="width:345px;" id="fldFiltros" runat="server">
                <legend id="lgnFiltros">Parámetros de exportación</legend>
                <table style="width:340px; margin-top:5px; margin-bottom:5px;" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" Text="" style="vertical-align:middle;cursor:pointer" enabled="false"  Checked /><label>Datos personales</label><img id="selectorDP" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;" onclick="MostrarSubMenu('fltDP');" title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Datos Personales."/>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkFORM" runat="server" Text="" style="vertical-align:middle;cursor:pointer" onclick="actDesactFormacion();" Checked />
                            <label>Acciones formativas</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkDO" runat="server" Text="" style="vertical-align:middle;cursor:pointer" onclick="actDesactDO();" Checked />
                            <label>Datos organizativos</label>
                            <img id="selectorDO" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;"  
                                onclick="MostrarSubMenu('fltDO');" title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Datos Organizativos."/>
                        </td>
                         <td>
                             <asp:CheckBox ID="chkCURREC" runat="server" Text="" style="margin-left:20px;vertical-align:middle;cursor:pointer" onclick="actDesFormacion(this);" Checked />
                             <label>Recibidas</label>
                             <img id="selectorCR" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;" 
                                 onclick="MostrarSubMenu('fltFRCURREC');"  title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Acciones formativas Recibidas."/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkSinopsis" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked /><label>Sinopsis</label>
                        </td>
                         <td>
                            <asp:CheckBox ID="chkCURIMP" runat="server" Text="" style="margin-left:20px;vertical-align:middle;cursor:pointer" onclick="actDesFormacion(this);" Checked /><label>Impartidas</label><img id="selectorCI" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;" onclick="MostrarSubMenu('fltFRCURIMP');"  title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Acciones formativas Impartidas."/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkFORACA" runat="server" Text="" onclick="actDesHijos(this)" style="vertical-align:middle;cursor:pointer" Checked /><label>Formación académica</label><img id="selectorFA" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;" onclick="MostrarSubMenu('fltFRFORACA');"  title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Formación Academica."/>
                        </td>
                         <td>
                            <asp:CheckBox ID="chkCERT" runat="server" Text="" onclick="actDesHijos(this)" style="vertical-align:middle;cursor:pointer" Checked /><label>Certificados</label><img id="selectorCert" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;"  onclick="MostrarSubMenu('fltCERT');"  title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Certificados."/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkEXP" runat="server" Text="" style="vertical-align:middle;cursor:pointer" onclick="actDesactExp();" Checked /><label>Experiencia profesional</label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkEXAM" runat="server" Text="" onclick="actDesHijos(this)" style="vertical-align:middle;cursor:pointer" Checked /><label>Exámenes</label><img id="selectorExam" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;"  onclick="MostrarSubMenu('fltEXAM');"  title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Exámenes."/>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkEXPFUE" runat="server" Text="" style="margin-left:20px;vertical-align:middle;cursor:pointer" onclick="actDesExp(this);" Checked /><label>Fuera de Ibermática</label><img id="selectorExpF" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;"  onclick="MostrarSubMenu('fltEXPFUE');" title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Experiencia Fuera de Ibermática."/>
                        </td>
                        <td>
                           <asp:CheckBox ID="chkIdiomas" runat="server" Text="" onclick="actDesHijos(this)" style="vertical-align:middle;cursor:pointer" Checked /><label>Idiomas</label><img id="selectorI" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;"  onclick="MostrarSubMenu('fltIdiomas');"  title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Idiomas."/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="chkEXPIBE" runat="server" Text="" style="margin-left:20px;vertical-align:middle;cursor:pointer" onclick="actDesExp(this);" Checked /><label>En Ibermática</label><img id="selectorExpI" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;"  onclick="MostrarSubMenu('fltEXPIBE');" title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Experiencia en Ibermática."/>
                        </td>
                    </tr>	
                    <tr>
                        <td style="padding-top:15px;">
                            <asp:CheckBox ID="chkRestringir" runat="server" Text="" style="vertical-align:middle; cursor:pointer;" />
                            <label id ="lblFiltros" style="vertical-align:top; margin-top:6px; cursor:pointer;" onclick="this.previousSibling.click();" title="Exporta únicamente la información filtrada en la consulta">Filtros establecidos</label>
                        </td>    
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br /><img id="selector" src="../../../../Images/arrow-down.png" style="margin-right:2px"/><label id="leyenda">Selecciona los campos que quieres exportar.</label>
                        </td>
                    </tr>
                    </table>
            </fieldset>
            <fieldset style="width:345px;margin-top:15px">
                <legend>Formato</legend>
                <table style="width: 329px; height: 17px; margin-top:5px;">
		            <tr class="TBLINI">
			            <td style="padding-left:25px; padding-top:2px">Plantilla</td>
		            </tr>
	            </table>    
                <div id="divPlantilla" runat="server" style="overflow: auto; width: 345px; height:100px;" align="left">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:329px">
                    </div>
                </div>    
                <table style="width: 329px; height: 17px; margin-bottom:5px">
	                <tr class="TBLFIN">
		                <td></td>
	                </tr>
                </table>
            </fieldset>
            <fieldset style="float:left;width:345px;  padding-top:3px;margin-top:15px;">
                <legend>Documento</legend>
                <asp:RadioButtonList ID="rdbFormato" SkinId="rbli" style="padding-left:10px; margin-top:3px; float:left" runat="server" RepeatColumns="2">
                    <asp:ListItem Value="WORD" style="cursor:pointer;margin-right:10px;"><img src='../../../../Images/word.jpg' border='0' onclick="cambiarCheck('rdbFormato', 0);" title="Word" style="cursor:pointer" ></asp:ListItem>
                    <asp:ListItem Value="PDF" Selected="True" style="cursor:pointer;"><img src='../../../../Images/adobe.png' border='0' onclick="cambiarCheck('rdbFormato', 1);" title="PDF" style="cursor:pointer" ></asp:ListItem>
                </asp:RadioButtonList>

                 <asp:RadioButtonList ID="rdbDoc" runat="server" RepeatColumns="2" SkinId="rbl"  style="margin-left:50px;float:left; margin-top:3px;">
                        <asp:ListItem style="cursor:pointer;"  Selected=true Value="0" Text="Uno&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem style="cursor:pointer;" Value="1" Text="Uno por profesional&nbsp;&nbsp;&nbsp;" />
                </asp:RadioButtonList>
            </fieldset>
        </eo:PageView>
        <eo:PageView ID="PageView2" CssClass="PageView" runat="server" style="height:470px;">				
        <!-- Pestaña 2 -->
            <table style="width:340px; margin-top:5px; margin-bottom:5px;" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <asp:CheckBox ID="chkExInfo" runat="server" Text="" style="vertical-align:middle;cursor:pointer"  Checked /><label>Información general</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkExFA" runat="server" Text="" style="vertical-align:middle;cursor:pointer"  Checked />
                        <label>Formación académica</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkExAF" runat="server" Text="" style="vertical-align:middle;cursor:pointer" onclick="actDesactFormacionExcel();" Checked />
                        <label>Acciones formativas</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkExCR" runat="server" Text="" style="margin-left:20px;vertical-align:middle;cursor:pointer" Checked /><label>Recibidas</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkExCI" runat="server" Text="" style="margin-left:20px;vertical-align:middle;cursor:pointer"  Checked /><label>Impartidas</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkExCertExam" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked /><label>Certificados/Exámenes</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkExID" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked /><label>Idiomas</label>
                    </td>
                </tr>	
                <tr style="height:17px"></tr>
                <tr>
                    <td>
                        <label style=" margin-left:5px">Información sobre las experiencias profesionales:</label>
                    </td>
                </tr>
                <tr style="height:3px"></tr>
                <tr>
                    <td style="padding-left: 20px;">
                        <asp:CheckBox ID="chkExEXPCLI" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked /><label>Clientes - Experiencias</label>
                    </td>
                </tr>		
                <tr>
                    <td style="padding-left: 20px;">
                        <asp:CheckBox ID="chkExEXPCLIPERF" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked /><label>Clientes - Experiencias - Perfiles - Entornos</label>
                    </td>
                </tr>		
                <tr>
                    <td style="padding-left: 20px;">
                        <asp:CheckBox ID="chkExPERF" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked /><label>Perfiles</label>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 20px;">
                        <asp:CheckBox ID="chkExENT" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked /><label>Entornos</label>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 20px;">
                        <asp:CheckBox ID="chkExENTPERF" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked /><label>Entornos - Perfiles</label>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 20px;">
                        <asp:CheckBox ID="chkExENTEXP" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked /><label>Entornos - Experiencia</label>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top:15px;">
                        <asp:CheckBox ID="chkExFS" runat="server" Text="" style="vertical-align:middle;cursor:pointer"/>
                        <label id="lblFiltroExcel" style="cursor:pointer;" onclick="this.previousSibling.click();" title="Exporta únicamente la información filtrada en la consulta">Filtros establecidos</label>
                    </td>    
                </tr>
                </table>
        </eo:PageView>
        <eo:PageView ID="PageView3" CssClass="PageView" runat="server" style="height:470px;">
        <!-- Pestaña 3 Exportación IBERDOK-->
            <fieldset style="width:345px;" id="fldFiltrosIB" runat="server">
                <legend id="lgnFiltrosIB">Parámetros de exportación</legend>
                <table style="width:340px; margin-top:5px; margin-bottom:5px;" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" Text="" style="vertical-align:middle;cursor:pointer" enabled="false"  Checked /><label>Datos personales</label><img id="selectorDPIB" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;" onclick="MostrarSubMenu('fltDP');" title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Datos Personales."/>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkFORMIB" runat="server" Text="" style="vertical-align:middle;cursor:pointer" onclick="actDesactFormacionIB();" Checked />
                            <label>Acciones formativas</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkDOIB" runat="server" Text="" style="vertical-align:middle;cursor:pointer" onclick="actDesactDOIB();" Checked />
                            <label>Datos organizativos</label>
                            <img id="selectorDOIB" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;"  
                                onclick="MostrarSubMenu('fltDO');" title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Datos Organizativos."/>
                        </td>
                         <td>
                             <asp:CheckBox ID="chkCURRECIB" runat="server" Text="" style="margin-left:20px;vertical-align:middle;cursor:pointer" onclick="actDesFormacionIB(this);" Checked />
                             <label>Recibidas</label>
                             <img id="selectorCRIB" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;" 
                                 onclick="MostrarSubMenu('fltFRCURREC');"  title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Acciones formativas Recibidas."/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkSinopsisIB" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked /><label>Sinopsis</label>
                        </td>
                         <td>
                            <asp:CheckBox ID="chkCURIMPIB" runat="server" Text="" style="margin-left:20px;vertical-align:middle;cursor:pointer" onclick="actDesFormacionIB(this);" Checked /><label>Impartidas</label><img id="selectorCIIB" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;" onclick="MostrarSubMenu('fltFRCURIMP');"  title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Acciones formativas Impartidas."/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkFORACAIB" runat="server" Text="" onclick="actDesHijos(this)" style="vertical-align:middle;cursor:pointer" Checked /><label>Formación académica</label><img id="selectorFAIB" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;" onclick="MostrarSubMenu('fltFRFORACA');"  title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Formación Academica."/>
                        </td>
                         <td>
                            <asp:CheckBox ID="chkCERTIB" runat="server" Text="" onclick="actDesHijos(this)" style="vertical-align:middle;cursor:pointer" Checked /><label>Certificados</label><img id="selectorCertIB" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;"  onclick="MostrarSubMenu('fltCERT');"  title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Certificados."/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkEXPIB" runat="server" Text="" style="vertical-align:middle;cursor:pointer" onclick="actDesactExpIB();" Checked /><label>Experiencia profesional</label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkEXAMIB" runat="server" Text="" onclick="actDesHijos(this)" style="vertical-align:middle;cursor:pointer" Checked /><label>Exámenes</label><img id="selectorExamIB" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;"  onclick="MostrarSubMenu('fltEXAM');"  title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Exámenes."/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkEXPFUEIB" runat="server" Text="" style="margin-left:20px;vertical-align:middle;cursor:pointer" onclick="actDesExpIB(this);" Checked /><label>Fuera de Ibermática</label><img id="selectorExpFIB" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;"  onclick="MostrarSubMenu('fltEXPFUE');" title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Experiencia Fuera de Ibermática."/>
                        </td>
                        <td>
                           <asp:CheckBox ID="chkIdiomasIB" runat="server" Text="" onclick="actDesHijos(this)" style="vertical-align:middle;cursor:pointer" Checked /><label>Idiomas</label><img id="selectorIIB" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;"  onclick="MostrarSubMenu('fltIdiomas');"  title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Idiomas."/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="chkEXPIBEIB" runat="server" Text="" style="margin-left:20px;vertical-align:middle;cursor:pointer" onclick="actDesExpIB(this);" Checked /><label>En Ibermática</label><img id="selectorExpIIB" src="../../../../Images/arrow-down.png" style="cursor:pointer; margin-left:2px;"  onclick="MostrarSubMenu('fltEXPIBE');" title="Selecciona los campos del CV que quieres exportar y visualizar del apartado Experiencia en Ibermática."/>
                        </td>
                    </tr>	
                    <tr>
                        <td style="padding-top:15px;">
                            <asp:CheckBox ID="chkRestringirIB" runat="server" Text="" style="vertical-align:middle;cursor:pointer" />
                            <label id ="lblFiltrosIB" style="vertical-align:top; margin-top:6px; cursor:pointer;" onclick="this.previousSibling.click();" title="Exporta únicamente la información filtrada en la consulta">Filtros establecidos</label>
                        </td>    
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br /><img id="selectorIB" src="../../../../Images/arrow-down.png" style="margin-right:2px"/><label id="leyendaIB">Selecciona los campos que quieres exportar.</label>
                        </td>
                    </tr>
                    </table>
            </fieldset>
            <fieldset style="width:345px;margin-top:15px">
                <legend>Formato</legend>
                <table style="width: 329px; height: 17px; margin-top:5px;">
		            <tr class="TBLINI">
			            <td style="padding-left:25px; padding-top:2px">Plantilla</td>
		            </tr>
	            </table>    
                <div id="divPlantillaIB" runat="server" style="overflow: auto; width: 345px; height:100px;" align="left">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:329px">
                    </div>
                </div>    
                <table style="width: 329px; height: 17px; margin-bottom:5px">
	                <tr class="TBLFIN">
		                <td></td>
	                </tr>
                </table>
            </fieldset>
            <fieldset style="float:left;width:345px;  padding-top:3px;margin-top:15px;">
                <legend>Documento</legend>
                <asp:RadioButtonList ID="rdbFormatoIB" SkinId="rbli" style="padding-left:10px; margin-top:3px; float:left" runat="server" RepeatColumns="2">
                    <asp:ListItem Value="IBERDOK" Selected="True" style="cursor:pointer;margin-right:10px;"><img src='../../../../Images/imgAccesoW.gif' border='0' onclick="cambiarCheck('rdbFormatoIB', 0);" title="Editable" style="cursor:pointer" ></asp:ListItem>
                    <asp:ListItem Value="PDF" style="cursor:pointer;"><img src='../../../../Images/adobe.png' border='0' onclick="cambiarCheck('rdbFormatoIB', 1);" title="PDF" style="cursor:pointer" ></asp:ListItem>
                </asp:RadioButtonList>

                 <asp:RadioButtonList ID="rdbDocIB" runat="server" RepeatColumns="2" SkinId="rbl"  style="margin-left:50px;float:left; margin-top:3px;">
                        <asp:ListItem style="cursor:pointer;"  Selected="True" Value="0" Text="Uno&nbsp;&nbsp;&nbsp;" />
                        <asp:ListItem style="cursor:pointer;" Value="1" Text="Uno por profesional&nbsp;&nbsp;&nbsp;" />
                </asp:RadioButtonList>
            </fieldset>
        </eo:PageView>
        <eo:PageView ID="PageView4" CssClass="PageView" runat="server" style="height:468px;">
        <!-- Pestaña 4 Exportación de documentación asociada a un CV-->
		    <eo:TabStrip runat="server" id="tsPestanasDoc" ControlSkinID="None" Width="368px" 
			    MultiPageID="mpContenido2" 
			    ClientSideOnLoad="CrearPestanasDoc" 
			    ClientSideOnItemClick="getPestanaDoc">
			    <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
				    <Items>
						    <eo:TabItem Text-Html="F. Acad." ToolTip="Exportar documentos sobre Formación Académica" Width="85"></eo:TabItem>
						    <eo:TabItem Text-Html="A. Form." ToolTip="Exportar documentos sobre Acciones Formativas" Width="85"></eo:TabItem>
						    <eo:TabItem Text-Html="Certif." ToolTip="Exportar documentos de Certificados" Width="85"></eo:TabItem>
						    <eo:TabItem Text-Html="Idioma" ToolTip="Exportar documentos de los títulos en Idiomas" Width="85"></eo:TabItem>
				    </Items>
			    </TopGroup>
			    <LookItems>
				    <eo:TabItem ItemID="_Default" 
					    LeftIcon-Url="~/Images/Pestanas/normal_left.gif"
					    LeftIcon-HoverUrl="~/Images/Pestanas/hover_left.gif"
					    LeftIcon-SelectedUrl="~/Images/Pestanas/selected_left.gif"
					    Image-Url="~/Images/Pestanas/normal_bg.gif"
					    Image-HoverUrl="~/Images/Pestanas/hover_bg.gif" 
					    Image-SelectedUrl="~/Images/Pestanas/selected_bg.gif" 
					    RightIcon-Url="~/Images/Pestanas/normal_right.gif"
					    RightIcon-HoverUrl="~/Images/Pestanas/hover_right.gif"
					    RightIcon-SelectedUrl="~/Images/Pestanas/selected_right.gif"
					    NormalStyle-CssClass="TabItemNormal"
					    HoverStyle-CssClass="TabItemHover"
					    SelectedStyle-CssClass="TabItemSelected"
					    DisabledStyle-CssClass="TabItemDisabled"
					    Image-Mode="TextBackground" Image-BackgroundRepeat="RepeatX">
				    </eo:TabItem>
			    </LookItems>
		    </eo:TabStrip>	
            <eo:MultiPage runat="server" id="mpContenido2" CssClass="FMP" Width="100%" Height="418px">
                <eo:PageView ID="PageView6" CssClass="PageView" runat="server">					        
	            <!-- Pestaña 1 Formación académica-->
                    <label style="text-decoration:underline; font-size:12px; font-weight:bold; margin-top:2px; margin-left:100px;">
                        Formación Académica
                    </label>
                    <table style="margin-top:5px;">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkFATodos" runat="server" Text="" style="vertical-align:middle;cursor:pointer;" Checked="false" 
                                    onclick="setVisibilidadDoc('FA');" />
                            </td>
                            <td>
                                Exportar todos los documentos anexados por los profesionales seleccionados en formación académica
                            </td>
                        </tr>
                    </table>
                    <div id="divFAExportPadre" style="visibility:visible;">
                    <table style="width: 340px; height: 17px; margin-top:5px;">
                        <tr class="TBLINI">
                            <td>
                                <img src="../../../../images/botones/imgmarcar.gif" onclick="mdTablaDoc('tblFAExport',1)" title="Marca todos los títulos" style="cursor:pointer; margin-left:2px;" />
                                <img src="../../../../images/botones/imgdesmarcar.gif" onclick="mdTablaDoc('tblFAExport',0)" title="Desmarca todos los títulos" style="cursor:pointer;" />   
                                <label title="Muestra la relación de titulaciones académicas que tienen los profesionales a partir del criterio de búsqueda utilizado en las consultas. Selecciona el documento a exportar, aunque no significa que el profesional seleccionado tenga el documento acreditativo.">
                                    Títulos encontrados por criterio de búsqueda
                                </label>
                            </td>
                        </tr>
                    </table>
                    <div id="divFAExport" style="overflow-x:hidden; overflow-y:auto; WIDTH: 356px; height:320px;" runat="server">
                        <div style="background-image:url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:340px; height:auto;">
                            <%=strTablaHTMLFiltroFA%>
                        </div>
                    </div>
                    <table id="Table3" style="height:17px; width:340px;">
                        <tr class="TBLFIN">
                            <td>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <input type="hidden" id="hdnListaFAExport" runat="server" value=""/>
                    <input type="hidden" id="hdnNombreFA"  runat="server" value=""/>
                    <input type="hidden" id="hdnExportarFA"  runat="server" value="N"/>
                </eo:PageView>
                <eo:PageView ID="PageView7" CssClass="PageView" runat="server">
                <!-- Pestaña 2 Cursos-->
                    <label style="text-decoration:underline; font-size:12px; font-weight:bold; margin-top:2px; margin-left:100px;">
                        Acciones Formativas
                    </label>
                    <table style="margin-top:5px;">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkCursoTodos" runat="server" Text="" style="vertical-align:middle;cursor:pointer;" Checked="false" 
                                    onclick="setVisibilidadDoc('CURSO');" />
                            </td>
                            <td>
                                Exportar todos los documentos anexados por los profesionales seleccionados en acciones formativas
                            </td>
                        </tr>
                    </table>
                    <div id="divCursoExportPadre" style="visibility:visible;">
                    <table style="width: 340px; height: 17px; margin-top:5px;">
                        <tr class="TBLINI">
                            <td>
                                <img src="../../../../images/botones/imgmarcar.gif" onclick="mdTablaDoc('tblCursoExport',1)" title="Marca todos los certificados" style="cursor:pointer; margin-left:2px;" />
                                <img src="../../../../images/botones/imgdesmarcar.gif" onclick="mdTablaDoc('tblCursoExport',0)" title="Desmarca todos los certificados" style="cursor:pointer;" />   
                                <label title="Muestra la relación de acciones formativas que tienen los profesionales a partir del criterio de búsqueda utilizado en las consultas. Selecciona el documento a exportar, aunque no significa que el profesional seleccionado tenga el documento acreditativo.">
                                    Acciones formativas por criterio de búsqueda
                                </label>
                            </td>
                        </tr>
                    </table>
                    <div id="divCursoExport" style="overflow-x:hidden; overflow-y:auto; WIDTH: 356px; height:320px;" runat="server">
                        <div style="background-image:url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:340px; height:auto;">
                            <%=strTablaHTMLFiltroCurso%>
                        </div>
                    </div>
                    <table id="Table4" style="height:17px; width:340px;">
                        <tr class="TBLFIN">
                            <td>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <input type="hidden" id="hdnListaCursosExport" runat="server" value=""/>
                    <input type="hidden" id="hdnNombreCurso"  runat="server" value=""/>
                    <input type="hidden" id="hdnExportarCurso"  runat="server" value="N"/>
                </eo:PageView>
                <eo:PageView ID="PageView8" CssClass="PageView" runat="server">
                <!-- Pestaña 3 Certificados-->
                    <label style="text-decoration:underline; font-size:12px; font-weight:bold; margin-top:2px; margin-left:100px;">
                        Certificaciones
                    </label>
                    <table style="margin-top:5px;">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkCertTodos" runat="server" Text="" style="vertical-align:middle;cursor:pointer;" Checked="false" 
                                    onclick="setVisibilidadDoc('CERT');" />
                            </td>
                            <td>
                                Exportar todos los documentos anexados por los profesionales seleccionados en certificados
                            </td>
                        </tr>
                    </table>
                    <div id="divCertExportPadre" style="visibility:visible;">
                    <table style="width: 340px; height: 17px; margin-top:5px;">
                        <tr class="TBLINI">
                            <td>
                                <img src="../../../../images/botones/imgmarcar.gif" onclick="mdTablaDoc('tblCertExport',1)" title="Marca todos los certificados" style="cursor:pointer; margin-left:2px;" />
                                <img src="../../../../images/botones/imgdesmarcar.gif" onclick="mdTablaDoc('tblCertExport',0)" title="Desmarca todos los certificados" style="cursor:pointer;" />   
                                <label title="Muestra la relación de certificados que tienen los profesionales a partir del criterio de búsqueda utilizado en las consultas. Selecciona el documento a exportar, aunque no significa que el profesional seleccionado tenga el documento acreditativo.">
                                    Certificados encontrados por criterio de búsqueda
                                </label>
                            </td>
                        </tr>
                    </table>
                    <div id="divCertExport" style="overflow-x:hidden; overflow-y:auto; WIDTH: 356px; height:320px;" runat="server">
                        <div style="background-image:url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:340px; height:auto;">
                            <%=strTablaHTMLFiltroCert%>
                        </div>
                    </div>
                    <table id="Table2" style="height:17px; width:340px;">
                        <tr class="TBLFIN">
                            <td>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <!--<input type="hidden" id="hdnListaFicepisExport"  runat="server" value=""/>-->
                    <input type="hidden" id="hdnListaCertificadosExport" runat="server" value=""/>
                    <input type="hidden" id="hdnNombreCert"  runat="server" value=""/>
                    <input type="hidden" id="hdnExportarCert"  runat="server" value="N"/>
                </eo:PageView>
                <eo:PageView ID="PageView9" CssClass="PageView" runat="server">					        
	            <!-- Pestaña 4 Idioma-->
                    <label style="text-decoration:underline; font-size:12px; font-weight:bold; margin-top:2px; margin-left:100px;">
                        Idiomas
                    </label>
                    <table style="margin-top:5px;">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkIdiomaTodos" runat="server" Text="" style="vertical-align:middle;cursor:pointer;" Checked="false" 
                                    onclick="setVisibilidadDoc('IDIOMA');" />
                            </td>
                            <td>
                                Exportar todos los documentos anexados por los profesionales seleccionados en idiomas
                            </td>
                        </tr>
                    </table>
                    <div id="divIdiomaExportPadre" style="visibility:visible;">
                    <table style="width: 340px; height: 17px; margin-top:5px;">
                        <tr class="TBLINI">
                            <td>
                                <img src="../../../../images/botones/imgmarcar.gif" onclick="mdTablaDoc('tblIdiomaExport',1)" title="Marca todos los títulos" style="cursor:pointer; margin-left:2px;" />
                                <img src="../../../../images/botones/imgdesmarcar.gif" onclick="mdTablaDoc('tblIdiomaExport',0)" title="Desmarca todos los títulos" style="cursor:pointer;" />   
                                <label title="Muestra la relación de títulos en idiomas que tienen los profesionales a partir del criterio de búsqueda utilizado en las consultas. Selecciona el documento a exportar, aunque no significa que el profesional seleccionado tenga el documento acreditativo.">
                                    Títulos encontrados por criterio de búsqueda
                                </label>
                            </td>
                        </tr>
                    </table>
                    <div id="divIdiomaExport" style="overflow-x:hidden; overflow-y:auto; WIDTH: 356px; height:320px;" runat="server">
                        <div style="background-image:url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:340px; height:auto;">
                            <%=strTablaHTMLFiltroIdioma%>
                        </div>
                    </div>
                    <table id="Table5" style="height:17px; width:340px;">
                        <tr class="TBLFIN">
                            <td>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <input type="hidden" id="hdnListaIdiomasExport" runat="server" value=""/>
                    <input type="hidden" id="hdnNombreIdioma"  runat="server" value=""/>
                    <input type="hidden" id="hdnExportarTitIdioma"  runat="server" value="N"/>
                </eo:PageView>
           </eo:MultiPage>
           
             <button id="btnRefrescar" type="button" class="btnH25W365" style="margin-top:3px; margin-left:1px;" runat="server" hidefocus="hidefocus" 
                        onclick="refrescarDocs();" onmouseover="se(this, 25);" 
                        title="Restringe las certificaciones/titulaciones a las que posean los profesionales seleccionados">
                <img src="../../../../Images/imgFlechaDrOff.gif" /><span>Refrescar en función de los profesionales seleccionados</span>
            </button>
           
        </eo:PageView>
    </eo:MultiPage>
    <fieldset id="fldExportIberDok" style="float:left; width:368px;height:37px; margin-top:5px; padding-top:3px; visibility:hidden;">
    <legend>Método de entrega</legend>
        <table style="width:165px; margin-top:5px; margin-left:10px;">
            <tr>
                <td style="text-align:center; width:25px;">
                    <img id="imgSobre" src='../../../../Images/Botones/imgEmail.png' border='0' title="Correo">
                </td>
                <td style="width:140px;">
                    <label id="lblTipoExp3">Diferido por correo</label>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset id="fldExportCV" style="float:left; width:368px; height:37px; margin-top:5px; padding-top:3px; visibility:hidden;">
            <legend>Método de entrega</legend>
                <asp:RadioButtonList ID="rdbTipoExp" onclick="cambiarLabel();" runat="server" RepeatColumns="3" SkinId="rbl" style="margin-top:3px; margin-left:10px;">
                        <asp:ListItem style="cursor:pointer; margin-right:10px;" Value="0">
                            <img src="../../../../Images/imgMonitor.png" onclick="cambiarLabel(0);" border='0' title="On-line" style="cursor:pointer;" >
                            <label style="vertical-align:top;">On-line</label>
                        </asp:ListItem>
                        <asp:ListItem style="cursor:pointer;" Selected=true Value="1">
                            <img src='../../../../Images/Botones/imgEmail.png' onclick="cambiarLabel(1);" border='0' title="Correo" style="cursor:pointer;" >
                            <label style="vertical-align:top;">Diferido por correo</label>
                        </asp:ListItem>
                </asp:RadioButtonList>
                <label id="lblTipoExp" style="visibility:hidden; width:1px;"></label>                
        </fieldset>
    <fieldset id="fldExportCert" style="float:left; width:368px;height:37px; margin-top:5px; padding-top:3px; visibility:hidden;">
        <legend>Método de entrega</legend>
            <table style="width:305px; margin-top:5px; margin-left:10px;">
                <tr>
                    <td style="text-align:center; width:25px;">
                        <img id="imgSobre" src='../../../../Images/Botones/imgEmail.png' border='0' title="Correo">
                    </td>
                    <td style="width:280px;">
                        <label id="lblTipoExp2">Diferido por correo a través de PAQEXPRESS</label>
                    </td>
                </tr>
            </table>
    </fieldset>

</div>
<div id="divFondoFiltros" style="z-index:10; position:absolute; left:620px; top:0px; width:340px; background-image: url(../../../../Images/imgFondoOscurecido.png); background-repeat:repeat; display:none;" runat="server">
    <div id="divMotivo" style="position:absolute; top:200px; left:15px;">
        <table border="0" cellspacing="0" cellpadding="0" style="width:365px;margin-top:5px;">
            <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
            </tr>
            <tr>
            <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding:3px; vertical-align:top;">
                <!-- Inicio del contenido propio de la página -->
                <table id="fltDP" width="360px;" style="display:none;">
                    <tr>
                        <td>
                            <label style="font-weight: bold; font-size: 12pt; color: #28406c;">Datos personales</label>
                        </td>
                        <td>
                            <img id="imgCerrarDP" src="../../../../Images/imgCerrarCapa.png" alt="Cerrar" onclick="MostrarSubMenu('fltDP');" style="cursor:pointer; margin-left:100px;"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkNombreApe" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked Enabled=false/><label style="vertical-align:middle;">Apellidos y nombre</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkFoto" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked /><label style="vertical-align:middle;">Foto</label>
                        </td>
                    </tr>
                       
                    <tr>
                        <td>
                            <asp:CheckBox id="chkNIF" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked /><label style="vertical-align:middle;">NIF</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkFNacimiento" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked /><label style="vertical-align:middle;">Fecha nacimiento</label>
                        </td>                     
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkNacionalidad" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked /><label style="vertical-align:middle;">Nacionalidad</label>
                        </td>                        
                        <td>
                            <asp:CheckBox id="chkSexo" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked /><label style="vertical-align:middle;">Sexo</label>
                        </td>
                    </tr>
                </table>
                <table id="fltDO" width="360px;" style="display:none;">
                    <tr>
                        <td>
                            <label style="font-weight: bold; font-size: 12pt; color: #28406c;">Datos organizativos</label>
                        </td>
                        <td>
                            <img id="imgCerrarDO" src="../../../../Images/imgCerrarCapa.png" alt="Cerrar" onclick="MostrarSubMenu('fltDO');" style="cursor:pointer; margin-left:100px;"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkEmpresa" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosDO();"/><label style="vertical-align:middle;">Empresa</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkUnidNegocio" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosDO();"/><label style="vertical-align:middle;">Unidad negocio</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkCR" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosDO();"/><label style="vertical-align:middle;">Centro responsabilidad</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkAntiguedad" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosDO();"/><label style="vertical-align:middle;">Fecha antiguedad</label>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td>
                            <asp:CheckBox id="chkRol" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosDO();"/><label style="vertical-align:middle;">Rol</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkPerfil" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosDO();"/><label style="vertical-align:middle;">Perfil</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkOficina" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosDO();"/><label style="vertical-align:middle;">Oficina</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkProvincia" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosDO();"/><label style="vertical-align:middle;">Provincia</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkPais" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosDO();"/><label style="vertical-align:middle;">Pais</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkTrayectoria" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosDO();"/><label style="vertical-align:middle;">Trayectoria internacional</label>
                        </td>
                    </tr>
                    <tr>                        
                        <td>
                            <asp:CheckBox id="chkMovilidad" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosDO();"/><label style="vertical-align:middle;">Movilidad geográfica</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkObservacion" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosDO();"/><label style="vertical-align:middle;">Observaciones</label>
                        </td>
                    </tr>
                </table>
                <table id="fltFRFORACA" width="360px;" style="display:none;">
                    <tr>
                        <td>
                            <label style="font-weight: bold; font-size: 12pt; color: #28406c;">Formación académica</label>
                        </td>
                        <td>
                            <img id="img10" src="../../../../Images/imgCerrarCapa.png" alt="Cerrar" onclick="MostrarSubMenu('fltFRFORACA');" style="cursor:pointer; margin-left:100px;"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkDesFORACA" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked Enabled=false/><label style="vertical-align:middle;">Titulación</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkTipo" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosFORACA();"/><label style="vertical-align:middle;">Tipo</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkModalidad" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosFORACA();"/><label style="vertical-align:middle;">Modalidad</label>
                        </td>                        
                        <td>
                            <asp:CheckBox id="chkTic" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosFORACA();"/><label style="vertical-align:middle;">Tic</label>
                        </td>                   
                    </tr>
                    <tr>  
                        <td>
                            <asp:CheckBox id="chkEspecialidad" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosFORACA();"/><label style="vertical-align:middle;">Especialidad</label>
                        </td>                      
                        <td>
                            <asp:CheckBox id="chkCentroFORACA" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosFORACA();"/><label style="vertical-align:middle;">Centro</label>
                        </td>
                    </tr>
                    <tr>  
                        <td>
                            <asp:CheckBox id="chkFInicio" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosFORACA();"/><label style="vertical-align:middle;">Fecha inicio</label>
                        </td>                      
                        <td>
                            <asp:CheckBox id="chkFFin" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosFORACA();"/><label style="vertical-align:middle;">Fecha fin</label>
                        </td>
                    </tr>
                </table>
                <table id="fltFRCURREC" width="360px;" style="display:none;">
                    <tr>
                        <td colspan="2">
                            <label style="font-weight: bold; font-size: 12pt; color: #28406c;">Acciones formativas recibidas</label>
                            <img id="img11" src="../../../../Images/imgCerrarCapa.png" alt="Cerrar" onclick="MostrarSubMenu('fltFRCURREC');" style="cursor:pointer; margin-left:50px;"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkTituloCur" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked Enabled="false"/><label style="vertical-align:middle;">Título</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkTipoCur" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURREC();"/><label style="vertical-align:middle;">Tipo</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkModalCur" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURREC();"/><label style="vertical-align:middle;">Modalidad</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkProvedCur" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURREC();"/><label style="vertical-align:middle;">Centro</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkEntTecCur" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURREC();"/><label style="vertical-align:middle;">Entorno tecnológico/funcional</label>
                        </td>                        
                        <td>
                            <asp:CheckBox id="chkProvCur" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURREC();"/><label style="vertical-align:middle;">Provincia</label>
                        </td>                   
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkHorasCur" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURREC();"/><label style="vertical-align:middle;">Horas</label>
                        </td>                        
                        <td>
                            <asp:CheckBox id="chkFIniCur" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURREC();"/><label style="vertical-align:middle;">Fecha inicio</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkFFinCur" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURREC();"/><label style="vertical-align:middle;">Fecha fin</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkConteCur" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURREC();"/><label style="vertical-align:middle;">Contenido</label>
                        </td>
                    </tr>   
                </table>
                <table id="fltFRCURIMP" width="360px;" style="display:none;">
                    <tr>
                        <td colspan="2">
                            <label style="font-weight: bold; font-size: 12pt; color: #28406c;">Acciones formativas impartidas</label>
                            <img id="img17" src="../../../../Images/imgCerrarCapa.png" alt="Cerrar" onclick="MostrarSubMenu('fltFRCURIMP');" style="cursor:pointer; margin-left:40px;"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkTituloCurImp" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked Enabled="false"/><label style="vertical-align:middle;">Título</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkTipoCurImp" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURIMP();"/><label style="vertical-align:middle;">Tipo</label>
                        </td>
                    </tr><tr>
                        <td>
                            <asp:CheckBox id="chkModalCurImp" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURIMP();"/><label style="vertical-align:middle;">Modalidad</label>
                        </td> 
                        <td>
                            <asp:CheckBox id="chkProvedCurImp" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURIMP();"/><label style="vertical-align:middle;">Centro</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkEntTecCurImp" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURIMP();"/><label style="vertical-align:middle;">Entorno tecnológico/funcional</label>
                        </td>               
                        <td>
                            <asp:CheckBox id="chkProvCurImp" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURIMP();"/><label style="vertical-align:middle;">Provincia</label>
                        </td>                   
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkHorasCurImp" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURIMP();"/><label style="vertical-align:middle;">Horas</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkFIniCurImp" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURIMP();"/><label style="vertical-align:middle;">Fecha inicio</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkFFinCurImp" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURIMP();"/><label style="vertical-align:middle;">Fecha fin</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkConteCurImp" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCURIMP();"/><label style="vertical-align:middle;">Contenido</label>
                        </td>
                    </tr>   
                </table>
                <table id="fltCERT" width="360px;" style="display:none;">
                    <tr>
                        <td>
                            <label style="font-weight: bold; font-size: 12pt; color: #28406c;">Certificados</label>
                        </td>
                        <td>
                            <img id="img12" src="../../../../Images/imgCerrarCapa.png" alt="Cerrar" onclick="MostrarSubMenu('fltCERT');" style="cursor:pointer; margin-left:100px;"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkCertDenomi" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked Enabled="false"/><label style="vertical-align:middle;">Denominación</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkCertProv" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCERT();"/><label style="vertical-align:middle;">Ent.Certificadora</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkCertEntTec" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCERT();"/><label style="vertical-align:middle;">Entorno tecnológico/funcional</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkCertFObten" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCERT();"/><label style="vertical-align:middle;">Fecha obtención</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkCertFCadu" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosCERT();"/><label style="vertical-align:middle;">Fecha caducidad</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkCertTipo" runat="server" Text="" style="vertical-align:middle;cursor:pointer; visibility:hidden;" Checked onclick="actDesactDatosCERT();"/><label style="vertical-align:middle;visibility:hidden">Tipo</label>
                        </td>
                    </tr>
                       
                </table>
                <table id="fltEXAM" width="360px;" style="display:none;">
                    <tr>
                        <td>
                            <label style="font-weight: bold; font-size: 12pt; color: #28406c;">Exámenes</label>
                        </td>
                        <td>
                            <img id="img2" src="../../../../Images/imgCerrarCapa.png" alt="Cerrar" onclick="MostrarSubMenu('fltEXAM');" style="cursor:pointer; margin-left:100px;"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkExamDenomi" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked Enabled="false"/><label style="vertical-align:middle;">Denominación</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkExamProv" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXAM();"/><label style="vertical-align:middle;">Ent.Certificadora</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkExamEntTec" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXAM();"/><label style="vertical-align:middle;">Entorno tecnológico/funcional</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkExamFObten" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXAM();"/><label style="vertical-align:middle;">Fecha obtención</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkExamFCadu" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXAM();"/><label style="vertical-align:middle;">Fecha caducidad</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkExamTipo" runat="server" Text="" style="vertical-align:middle;cursor:pointer; visibility:hidden;" Checked onclick="actDesactDatosEXAM();"/><label style="vertical-align:middle;visibility:hidden;">Tipo</label>
                        </td>
                    </tr>
                       
                </table>
                <table id="fltIdiomas" width="360px;" style="display:none;">
                    <tr>
                        <td>
                            <label style="font-weight: bold; font-size: 12pt; color: #28406c;">Idiomas</label>
                        </td>
                        <td>
                            <img id="img13" src="../../../../Images/imgCerrarCapa.png" alt="Cerrar" onclick="MostrarSubMenu('fltidiomas');" style="cursor:pointer; margin-left:100px;"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkDescriIdioma" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked Enabled="false"/><label style="vertical-align:middle;">Idioma</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkLectura" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosIDIOMAS();"/><label style="vertical-align:middle;">Lectura</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkEscritura" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosIDIOMAS();"/><label style="vertical-align:middle;">Escritura</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkOral" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosIDIOMAS();"/><label style="vertical-align:middle;">Oral</label>
                        </td>
                        </tr>
                        <tr>
                        <td>
                            <asp:CheckBox id="chkTitIdioma" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosIDIOMAS();"/><label style="vertical-align:middle;">Título</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkTitIdiomaObt" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosIDIOMAS();"/><label style="vertical-align:middle;">Fecha obtención</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkTitCentro" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosIDIOMAS();"/><label style="vertical-align:middle;">Centro</label>
                        </td>
                    </tr>
                       
                </table>
                <table id="fltEXPFUE" width="360px;" style="display:none;">
                    <tr>
                        <td>
                            <label style="font-weight: bold; font-size: 12pt; color: #28406c;">Experiencia Fuera</label>
                        </td>
                        <td>
                            <img id="img14" src="../../../../Images/imgCerrarCapa.png" alt="Cerrar" onclick="MostrarSubMenu('fltEXPFUE');" style="cursor:pointer; margin-left:100px;"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkEXPFUEDenomi" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked Enabled="false"/><label style="vertical-align:middle;">Experiencia</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkEXPFUEDescri" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPFUE();"/><label style="vertical-align:middle;">Descripción</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkEXPFUEACSACT" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPFUE();"/><label style="vertical-align:middle;" title="Área de Conocimiento Sectorial y Área de Conocimiento Tecnológico">ACS - ACT</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkEXPFUEEmpOri" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPFUE();"/><label style="vertical-align:middle;">Empresa contratante</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkEXPFUECli" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPFUE();"/><label style="vertical-align:middle;">Cliente</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkEXPFUESector" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPFUE();"/><label style="vertical-align:middle;">Sector</label>
                        </td>
                        </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkEXPFUESegmen" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPFUE();"/><label style="vertical-align:middle;">Segmento</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkEXPFUEPerfil" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPFUE();"/><label style="vertical-align:middle;">Perfil</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkEXPFUEFunci" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPFUE();"/><label style="vertical-align:middle;">Función</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkEXPFUEEntor" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPFUE();"/><label style="vertical-align:middle;">Entorno tecnológico/funcional</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkEXPFUEFIni" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPFUE();"/><label style="vertical-align:middle;">Fecha inicio</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkEXPFUEFFin" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPFUE();"/><label style="vertical-align:middle;">Fecha fin</label>
                        </td>
                        </tr>
                        <tr>
                        <td>
                            <asp:CheckBox id="chkEXPFUENmes" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPFUE();"/><label style="vertical-align:middle;">Nº meses</label>
                        </td>
                        </tr>
                </table>
                <table id="fltEXPIBE" width="360px;" style="display:none;">
                    <tr>
                        <td colspan="2">
                            <label style="font-weight: bold; font-size: 12pt; color: #28406c;">Experiencia en Ibermatica</label>
                            <img id="img15" src="../../../../Images/imgCerrarCapa.png" alt="Cerrar" onclick="MostrarSubMenu('fltEXPIBE');" style="cursor:pointer; margin-left:80px;"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkEXPIBEDenomi" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked Enabled="false"/><label style="vertical-align:middle;">Experiencia</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkEXPIBEDescri" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPIBE();"/><label style="vertical-align:middle;">Descripción</label>
                        </td>    
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkEXPIBEACSACT" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPIBE();"/><label style="vertical-align:middle;"  title="Área de Conocimiento Sectorial y Área de Conocimiento Tecnológico">ACS - ACT</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkEXPIBECli" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPIBE();"/><label style="vertical-align:middle;">Cliente</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkEXPIBESector" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPIBE();"/><label style="vertical-align:middle;">Sector</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkEXPIBESegmen" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPIBE();"/><label style="vertical-align:middle;">Segmento</label>
                        </td>
                    </tr>    
                    <tr>
                        <td>
                            <asp:CheckBox id="chkEXPIBEPerfil" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPIBE();"/><label style="vertical-align:middle;">Perfil</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkEXPIBEFunci" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPIBE();"/><label style="vertical-align:middle;">Función</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkEXPIBEEntor" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPIBE();"/><label style="vertical-align:middle;">Entorno tecnológico/funcional</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkEXPIBEFIni" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPIBE();"/><label style="vertical-align:middle;">Fecha inicio</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox id="chkEXPIBEFFin" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPIBE();"/><label style="vertical-align:middle;">Fecha fin</label>
                        </td>
                        <td>
                            <asp:CheckBox id="chkEXPIBENmes" runat="server" Text="" style="vertical-align:middle;cursor:pointer" Checked onclick="actDesactDatosEXPIBE();"/><label style="vertical-align:middle;">Nº meses</label>
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
    </div>
</div>

<div id="divNoDatos" style="z-index:0; position:absolute; left:40px; top:0px; width:450px; background-image: url(../../../../Images/imgFondoOscurecido.png); background-repeat:repeat; display:none;" runat="server">
    <div id="divNoDatos1" style="position:absolute; top:350px; left:15px;">
        <table border="0" cellspacing="0" cellpadding="0" style="width:455px;margin-top:5px;">
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
          </tr>
          <tr>
            <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding:3px; vertical-align:top;">
            <!-- Inicio del contenido propio de la página -->
                <table id="Table1" width="450px;">
                <tr>
                    <td>
                        <label>No se han encontrado CV's que cumplan los criterios establecidos de tu ámbito de visión</label>
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
    </div>
</div>

<div id="divPendientes" title="Datos pendientes de cumplimentar" style="display:none; width:750px; height:600px;">
    <table style="width:720px; border-collapse:collapse; height:17px; table-layout:fixed;" cellpadding='0' cellspacing="0" border="0">
        <tr>
            <td style="width:700px;">
                <table style="width:700px; border-collapse:collapse; height:17px; table-layout:fixed;" cellpadding='0' cellspacing="0" border="0">
                    <tr class="TBLINI">
                        <td style="width:200px;">&nbsp;Grupo</td>
                        <td style="width:200px;">Elemento</td>
                        <td style="width:100px;">Estado</td>
                        <td style="width:200px;">Responsable</td>
                    </tr>
                </table>
            </td>
            <td style="width:20px;">
                <img id="imgExcelPdte" src="../../../../Images/imgExcelAnim.gif" alt="Exporta a Excel el contenido de la tabla" class="MANO" onclick="mostrarProcesando();setTimeout('excelPdte();',500);"/>
            </td>
        </tr>
    </table>
    <div id="divPdteCab" style="overflow-x: hidden; overflow-y: auto; width: 716px; height: 440px;" align="left">
        <div id="divPdte" style="visibility:visible; width:700px;">
        </div>
    </div>
    <table style="width: 700px; border-collapse: collapse; height: 17px;" cellspacing="0" cellpadding="0" border="0">
        <tr class="TBLFIN">
            <td>
            </td>
        </tr>
    </table>
</div>   

<input type="hidden" id="hdnProfesional"  runat="server" value=""/>
<input type="hidden" id="hdnDestinatarios"  runat="server" value=""/>
<input type="hidden" id="hdnDestinatarioIdFicepi"  runat="server" value=""/>
<input type="hidden" id="hdnNombreProfesionales"  runat="server" value=""/>
<input type="hidden" id="hdnTitulo"  runat="server" value=""/>
<input type="hidden" id="hdnCertificacion"  runat="server" value=""/>
<input type="hidden" id="hdnCuenta"  runat="server" value=""/>
<input type="hidden" id="hdnSN4"  runat="server" value=""/>
<input type="hidden" id="hdnSN3"  runat="server" value=""/>
<input type="hidden" id="hdnSN2"  runat="server" value=""/>
<input type="hidden" id="hdnSN1"  runat="server" value=""/>
<input type="hidden" id="hdnCR"  runat="server" value=""/>
<input type="hidden" id="hdnIdFicepis" runat="server" value=""/>
<input type="hidden" id="hdnIdFicepisTotal" runat="server" value=""/>

<input type="hidden" id="hdnIdTitulo"  runat="server" value=""/>
<input type="hidden" id="hdnIdCertificado"  runat="server" value=""/>
<input type="hidden" id="hdnIdCliente"  runat="server" value=""/>

<input type="hidden" id="hdnIdEntornoFormacion"  runat="server" value=""/>
<input type="hidden" id="hdnIdEntornoExp"  runat="server" value=""/>   

<input type="hidden" id="hdnPlantilla"  runat="server" value="1"/>   

<input type="hidden" id="hdnFiltros" runat="server" value="" />
<input type="hidden" id="hdnCamposExcel" runat="server" value="" />
<input type="hidden" id="hdnNumExp" runat="server" value="" />
<input type="hidden" id="segMediaCV" runat="server" value="" />
<input type="hidden" id="hdnTrackingId" runat="server" value="" />
<input type="hidden" id="hdnTipoExp" runat="server" value="0" />

<input type="hidden" id="hdnBuscarDocs"  runat="server" value="S"/>
<input type="hidden" id="hdnCriterios" runat="server" value=""/>
<input type="hidden" id="hdnCamposWord" runat="server" value=""/>

<div class="ocultarcapa">
    <div id="divhdnExcel">
        <table id="hdnExcel"></table> 
    </div>
</div>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<iframe id="iFrmDescarga" frameborder="0" name="iFrmDescarga" width="10px" height="10px" style="visibility:hidden" ></iframe>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

            switch (strBoton) {
                case "guia":
                    {
                        bEnviar = false;
                        mostrarGuia("GuiaConsultasCVT.pdf");
                        break;
                    }
                case "exportar":
                    {
                        bEnviar = false;
                        mostrarFondo();
                        break;
                    }
            }
        }

        var theform = document.forms[0];
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) theform.submit();
    }
</script>
</asp:Content>

