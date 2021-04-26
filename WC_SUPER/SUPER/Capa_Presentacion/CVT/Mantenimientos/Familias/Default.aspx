<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Gestión de familias para consultas de CVT</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>  
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>  
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <style type="text/css">
        #tsPestanasGen table { table-layout:auto; }
    </style>
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bSalir = false;
        var gsNombreProfesional = "<% =sNombreProfesional %>";
        mostrarProcesando();
        //Variables a devolver a la estructura.
        //var sRecargar="F";
    </script>    

    <div style="margin-left:5px; margin-top:10px;">
    <eo:TabStrip runat="server" id="tsPestanasGen" ControlSkinID="None" Width="985px" 
        MultiPageID="mpContenido" 
        ClientSideOnLoad="CrearPestanas" 
        ClientSideOnItemClick="getPestana">
	    <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
		    <Items>
			    <eo:TabItem Text-Html="Perfiles" Width="100"></eo:TabItem>
			    <eo:TabItem Text-Html="Entornos" Width="100"></eo:TabItem>
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
    <eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="985px" Height="540px">
	    <eo:PageView ID="PageView1" CssClass="PageView" runat="server" >	
            <table style="width:960px; height:520px;" border="0">
            <colgroup>
                <col style="width:540px;" />
                <col style="width:20px;" />
                <col style="width:400px;" />
            </colgroup>
                <tr>
                    <td>
                        <fieldset style="width:530px; text-align:left; height:510px;">  
                        <legend>Familias</legend>   
                            <table style="width:520px; margin-left:5px;" border="0"> 
                                <tr>
                                    <td>
                                        <fieldset style="width:510px; text-align:left; height:151px;">
                                        <legend>Privadas</legend>  
                                            <table style="width:510px;" border="0"> 
                                            <colgroup><col style="width:480px;" /><col style="width:30px;" /></colgroup>
                                                <tr>
                                                    <td>
                                                        <table id="Table3" style="height:17px; width:450px;">
                                                            <tr class="TBLINI">
                                                                <td style="padding-left:25px">
                                                                    Denominación				    				    
                                                                </td>	
                                                            </tr>
                                                        </table>       
                                                        <div id="divFamPerPri" style="overflow-x:hidden; overflow-y:auto; width:466px; height:100px;" runat="server"  name="divCatalogo">
                                                            <div style="background-image:url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:450px; height:auto;">
                                                                <%=strTablaHTMLPerfPri%>
                                                            </div>
                                                        </div>
                                                        <table id="Table5" style="height:17px; width:450px;"><tr class="TBLFIN"><td></td></tr></table>
                                                    </td>
                                                    <td>
                                                        <table style="width:30px; height:120px;" border="0">
                                                            <tr style="height:40px;">
                                                                <td>
                                                                    <button id="btnAddFamPerPri" type="button" title="Añadir familia" onclick="addFamPerfil(1)" class="btnH25W25" style="display:inline; padding-left:4px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                        <img src="../../../../Images/imgAdd.png" />
                                                                    </button>    
                                                                </td>
                                                            </tr>
                                                            <tr style="height:40px;">
                                                                <td>
                                                                    <button id="btnDelFamPerPri" type="button" title="Eliminar familia" onclick="delFamPerfil(1)" class="btnH25W25" style="display:inline;padding-left:4px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                        <img src="../../../../Images/imgDel.png" />
                                                                        <span title="Eliminar familia"></span>
                                                                    </button>    
                                                                </td>
                                                            </tr>
                                                            <tr style="height:40px;">
                                                                <td>
                                                                    <button id="btPubFamPerPri" type="button" title="Publicar familia" onclick="publicarFamPerfPri()" class="btnH25W25" style="display:inline;padding-left:4px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                        <img src="../../../../Images/imgPublicada.gif" />
                                                                        <span title="Publicar familia"></span>
                                                                    </button>    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                             </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset style="width:510px; text-align:left; height:151px;">
                                        <legend>Públicas</legend>  
                                            <table style="width:510px;" border="0"> 
                                            <colgroup><col style="width:480px;" /><col style="width:30px;" /></colgroup>
                                                <tr>
                                                    <td>
                                                        <table id="Table6" style="height:17px; width:450px;" cellpadding="0" cellspacing="0">
                                                            <tr class="TBLINI">
	                                                            <td style="width:350px;">&nbsp;Denominación</td>	
	                                                            <td style="width:100px;">Modificada por</td>	
                                                            </tr>
                                                        </table>       
                                                        <div id="divFamPerPub" style="overflow-x:hidden; overflow-y:auto; width:466px; height:100px;" runat="server"  name="divCatalogo">
                                                            <div id="divCapaPer" style="background-image:url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:450px; height:auto;">
                                                                <%=strTablaHTMLPerfPub%>
                                                            </div>
                                                        </div>
                                                        <table id="Table8" style="height:17px; width:450px;"><tr class="TBLFIN"><td></td></tr></table>
                                                    </td>
                                                    <td>
                                                        <table style="width:30px;" border="0">
                                                            <tr style="height:40px;">
                                                                <td>
                                                                    <button id="btnAddFamPerPub" type="button" title="Añadir familia" onclick="addFamPerfil(2)" class="btnH25W25" style="display:inline; padding-left:4px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                        <img src="../../../../Images/imgAdd.png" />
                                                                    </button>    
                                                                </td>
                                                            </tr>
                                                            <tr style="height:40px;">
                                                                <td>
                                                                    <button id="btnDelFamPerPub" type="button" title="Eliminar familia" onclick="delFamPerfil(2)" class="btnH25W25" style="display:inline;padding-left:4px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                        <img src="../../../../Images/imgDel.png" />
                                                                        <span title="Eliminar familia"></span>
                                                                    </button>    
                                                                </td>
                                                            </tr>
                                                            <tr style="height:40px;">
                                                                <td>
                                                                    <button id="btnImpPerPub" type="button" title="Copiar al grupo de familias privadas" onclick="importarFamPerfPub()" class="btnH25W25" style="display:inline;padding-left:4px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                        <img src="../../../../Images/Botones/imgReplica.gif" />
                                                                        <span title="Importar familia"></span>
                                                                    </button>    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                             </table>
                                         </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset style="width:510px; text-align:left; height:151px;">
                                        <legend>Ajenas</legend>  
                                            <table style="width:510px;" border="0"> 
                                            <colgroup><col style="width:480px;" /><col style="width:30px;" /></colgroup>
                                                <tr>
                                                    <td>
                                                        <table id="Table9" style="height:17px; width:450px;" cellpadding="0" cellspacing="0">
                                                            <tr class="TBLINI">
	                                                            <td style="width:350px;">&nbsp;Denominación</td>	
	                                                            <td style="width:100px;">Autor</td>	
                                                            </tr>
                                                        </table>       
                                                        <div id="divFamPerAje" style="overflow-x:hidden; overflow-y:auto; width:466px; height:100px;" runat="server"  name="divCatalogo">
                                                            <div style="background-image:url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:450px; height:auto;">
                                                                <%=strTablaHTMLPerfAje%>
                                                            </div>
                                                        </div>
                                                        <table id="Table11" style="height:17px; width:450px;"><tr class="TBLFIN"><td></td></tr></table>
                                                    </td>
                                                    <td>
                                                        <table style="width:30px;" border="0">
                                                            <tr>
                                                                <td>
                                                                    <button id="btnImpPerAje" type="button" title="Copiar al grupo de familias privadas" onclick="importarFamPerfAje()" class="btnH25W25" style="display:inline;padding-left:4px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                        <img src="../../../../Images/Botones/imgReplica.gif" />
                                                                        <span title="Importar familia"></span>
                                                                    </button>    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                
                                                </tr>
                                             </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>                 
                    </td>
                    <td></td>
                    <td>
                        <fieldset style="width:395px; text-align:left; height:510px;">  
                        <legend><label id="lblPerfiles" class="texto" style="font-weight: bold;">Perfiles de la familia</label></legend>   
                            <table id="Table1" style="height:17px; width:370px; margin-top:2px;">
                                <tr class="TBLINI">
	                                <td style="padding-left:5px">
		                                Denominación				    				    
		                            </td>	
                                </tr>
                            </table>       
                            <div id="divPerFam" style="overflow-x:hidden; overflow-y:auto; width:386px; height:464px;" runat="server">
                                <div style="background-image:url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:370px; height:auto;">
                                    <table id='tblPerFam' style='width:370px;' mantenimiento='0'>
                                        <colgroup><col style="width:20px;" /><col style="width:350px;" /></colgroup>
                                    </table>
                                </div>
                            </div>
                            <table id="Table2" style="height:17px; width:370px;"><tr class="TBLFIN"><td></td></tr></table>
                        </fieldset>                 
                    </td>
                </tr>
            </table>
        </eo:PageView>
	    <eo:PageView ID="PageView2" CssClass="PageView" runat="server">	
            <table style="width:960px; height:520px;" border="0">
            <colgroup>
                <col style="width:540px;" />
                <col style="width:20px;" />
                <col style="width:400px;" />
            </colgroup>
                <tr>
                    <td>
                        <fieldset style="width:530px; text-align:left; height:510px;">  
                        <legend>Familias</legend>   
                            <table style="width:520px; margin-left:5px;" border="0"> 
                                <tr>
                                    <td>
                                        <fieldset style="width:510px; text-align:left; height:151px;">
                                        <legend>Privadas</legend>  
                                            <table style="width:510px;" border="0"> 
                                            <colgroup><col style="width:480px;" /><col style="width:30px;" /></colgroup>
                                                <tr>
                                                    <td>
                                                        <table id="Table4" style="height:17px; width:450px;">
                                                            <tr class="TBLINI">
                                                                <td style="padding-left:25px">
                                                                    Denominación				    				    
                                                                </td>	
                                                            </tr>
                                                        </table>       
                                                        <div id="divFamEntPri" style="overflow-x:hidden; overflow-y:auto; width:466px; height:100px;" runat="server"  name="divCatalogo">
                                                            <div style="background-image:url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:450px; height:auto;">
                                                            </div>
                                                        </div>
                                                        <table id="Table7" style="height:17px; width:450px;"><tr class="TBLFIN"><td></td></tr></table>
                                                    </td>
                                                    <td>
                                                        <table style="width:30px; height:120px;" border="0">
                                                            <tr style="height:40px;">
                                                                <td>
                                                                    <button id="Button1" type="button" title="Añadir familia" onclick="addFamEntorno(1)" class="btnH25W25" style="display:inline; padding-left:4px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                        <img src="../../../../Images/imgAdd.png" />
                                                                    </button>    
                                                                </td>
                                                            </tr>
                                                            <tr style="height:40px;">
                                                                <td>
                                                                    <button id="Button2" type="button" title="Eliminar familia" onclick="delFamEntorno(1)" class="btnH25W25" style="display:inline;padding-left:4px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                        <img src="../../../../Images/imgDel.png" />
                                                                        <span title="Eliminar familia"></span>
                                                                    </button>    
                                                                </td>
                                                            </tr>
                                                            <tr style="height:40px;">
                                                                <td>
                                                                    <button id="Button3" type="button" title="Publicar familia" onclick="publicarFamEntPri()" class="btnH25W25" style="display:inline;padding-left:4px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                        <img src="../../../../Images/imgPublicada.gif" />
                                                                        <span title="Publicar familia"></span>
                                                                    </button>    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                             </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset style="width:510px; text-align:left; height:151px;">
                                        <legend>Públicas</legend>  
                                            <table style="width:510px;" border="0"> 
                                            <colgroup><col style="width:480px;" /><col style="width:30px;" /></colgroup>
                                                <tr>
                                                    <td>
                                                        <table id="Table10" style="height:17px; width:450px;" cellpadding="0" cellspacing="0">
                                                            <tr class="TBLINI">
	                                                            <td style="width:350px;">&nbsp;Denominación</td>	
	                                                            <td style="width:100px;">Modificada por</td>	
                                                            </tr>
                                                        </table>       
                                                        <div id="divFamEntPub" style="overflow-x:hidden; overflow-y:auto; width:466px; height:100px;" runat="server"  name="divCatalogo">
                                                            <div style="background-image:url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:450px; height:auto;">
                                                            </div>
                                                        </div>
                                                        <table id="Table12" style="height:17px; width:450px;"><tr class="TBLFIN"><td></td></tr></table>
                                                    </td>
                                                    <td>
                                                        <table style="width:30px;" border="0">
                                                            <tr style="height:40px;">
                                                                <td>
                                                                    <button id="Button4" type="button" title="Añadir familia" onclick="addFamEntorno(2)" class="btnH25W25" style="display:inline; padding-left:4px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                        <img src="../../../../Images/imgAdd.png" />
                                                                    </button>    
                                                                </td>
                                                            </tr>
                                                            <tr style="height:40px;">
                                                                <td>
                                                                    <button id="Button5" type="button" title="Eliminar familia" onclick="delFamEntorno(2)" class="btnH25W25" style="display:inline;padding-left:4px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                        <img src="../../../../Images/imgDel.png" />
                                                                        <span title="Eliminar familia"></span>
                                                                    </button>    
                                                                </td>
                                                            </tr>
                                                            <tr style="height:40px;">
                                                                <td>
                                                                    <button id="Button6" type="button" title="Copiar al grupo de familias privadas" onclick="importarFamEntPub()" class="btnH25W25" style="display:inline;padding-left:4px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                        <img src="../../../../Images/Botones/imgReplica.gif" />
                                                                        <span title="Importar familia"></span>
                                                                    </button>    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                             </table>
                                         </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset style="width:510px; text-align:left; height:151px;">
                                        <legend>Ajenas</legend>  
                                            <table style="width:510px;" border="0"> 
                                            <colgroup><col style="width:480px;" /><col style="width:30px;" /></colgroup>
                                                <tr>
                                                    <td>
                                                        <table id="Table13" style="height:17px; width:450px;" cellpadding="0" cellspacing="0">
                                                            <tr class="TBLINI">
	                                                            <td style="width:350px;">&nbsp;Denominación</td>	
	                                                            <td style="width:100px;">Autor</td>	
                                                            </tr>
                                                        </table>       
                                                        <div id="divFamEntAje" style="overflow-x:hidden; overflow-y:auto; width:466px; height:100px;" runat="server"  name="divCatalogo">
                                                            <div style="background-image:url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:450px; height:auto;">
                                                            </div>
                                                        </div>
                                                        <table id="Table14" style="height:17px; width:450px;"><tr class="TBLFIN"><td></td></tr></table>
                                                    </td>
                                                    <td>
                                                        <table style="width:30px;" border="0">
                                                            <tr>
                                                                <td>
                                                                    <button id="Button7" type="button" title="Copiar al grupo de familias privadas" onclick="importarFamEntAje()" class="btnH25W25" style="display:inline;padding-left:4px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                        <img src="../../../../Images/Botones/imgReplica.gif" />
                                                                        <span title="Importar familia"></span>
                                                                    </button>    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                
                                                </tr>
                                             </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>                 
                    </td>
                    <td></td>
                    <td>
                        <fieldset style="width:395px; text-align:left; height:510px;">  
                        <legend><label id="lblEntornos" class="texto" style="font-weight: bold;">Entornos de la familia</label></legend>   
                            <table id="Table15" style="height:17px; width:370px; margin-top:2px;">
                                <tr class="TBLINI">
	                                <td style="padding-left:5px">
		                                Denominación				    				    
		                            </td>	
                                </tr>
                            </table>       
                            <div id="divEntFam" style="overflow-x:hidden; overflow-y:auto; width:386px; height:464px;" runat="server">
                                <div style="background-image:url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:370px; height:auto;">
                                    <table id='Table16' style='width:370px;' mantenimiento='0'>
                                        <colgroup><col style="width:20px;" /><col style="width:350px;" /></colgroup>
                                    </table>
                                </div>
                            </div>
                            <table id="Table17" style="height:17px; width:370px;"><tr class="TBLFIN"><td></td></tr></table>
                        </fieldset>                 
                    </td>
                </tr>
            </table>
        </eo:PageView>
    </eo:MultiPage>
    </div>
    <center>
    <table style="width:95px; margin-top:10px;" border="0">
	    <tr> 
            <td>
		        <button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline; ">
			        <img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
		        </button>
		    </td>
	      </tr>
    </table>
    </center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <input type="hidden" id="hdnIdTipo" value="" runat="server"/>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
    <div class="clsDragWindow" id="DW" noWrap></div>
</form>
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        //			if (eventTarget == "Botonera"){
        //				var strBoton = $I("Botonera").botonID(eventArgument).toLowerCase();
        //				//alert("strBoton: "+ strBoton);
        //				switch (strBoton){
        //					case "regresar": //Boton Anadir
        //					{
        //					    comprobarGrabarOtrosDatos();
        //						bEnviar = true;
        //						break;
        //					}
        //				}
        //			}

        var theform;
        if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
            theform = document.forms[0];
        }
        else {
            theform = document.forms["frmPrincipal"];
        }

        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) {
            theform.submit();
        }
        else {
            $I("Botonera").restablecer();
        }
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
</body>
</html>
