<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_PanelTareas_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<style type="text/css">
.flechadown { 
    background-image: url('../../UserControls/AccionesPendientes/Images/imgFlecha_down.png'); 
    background-repeat:no-repeat; 
    vertical-align:middle;
    margin-right:2px; 
    cursor:pointer; 
    width:10px;
    height:16px;
    border: 0px;
}
.flecharight { 
    background-image: url('../../UserControls/AccionesPendientes/Images/imgFlecha_right.png'); 
    background-repeat:no-repeat; 
    vertical-align:middle;
    margin-right:2px; 
    cursor:pointer; 
    width:10px;
    height:16px;
    border: 0px;
}
.flagred { 
    background-image: url('../../UserControls/AccionesPendientes/Images/imgFlag_red.png');
    vertical-align:middle;
    margin:0px;
    width:16px;
    height:16px;
    border: 0px;
}
.flagyellow { 
    background-image: url('../../UserControls/AccionesPendientes/Images/imgFlag_yellow.png');
    vertical-align:middle;
    margin:0px;
    width:16px;
    height:16px;
    border: 0px;
}
.TablaModulo { width:390px; margin-left:4px; margin-top:3px; }
.CabeceraModulo { width:382px; margin-left:4px; height:16px; vertical-align:middle;font-family:Arial; font-size:9pt; font-weight:bold; color:#569bbd; vertical-align:middle; }
.Link { color:Blue; text-decoration:underline; cursor:pointer; }
.Expand_top { background-image: url('../../UserControls/AccionesPendientes/Images/imgFondoExpand_top.png'); background-repeat:no-repeat; }
.Expand_body { background-image: url('../../UserControls/AccionesPendientes/Images/imgFondoExpand_body.png'); background-repeat:repeat; vertical-align:top; }
.Expand_body div { display:block; }
.Expand_bottom { background-image: url('../../UserControls/AccionesPendientes/Images/imgFondoExpand_bottom.png'); background-repeat:no-repeat; }
.Collapse_top { background-image: url('../../UserControls/AccionesPendientes/Images/imgFondoCollapse_top.png'); background-repeat:no-repeat; }
.Collapse_body { background-image: url('../../UserControls/AccionesPendientes/Images/imgFondoCollapse_body.png'); background-repeat:repeat; vertical-align:top; }
.Collapse_body div { display:none; }
.Collapse_bottom { background-image: url('../../UserControls/AccionesPendientes/Images/imgFondoCollapse_bottom.png'); background-repeat:no-repeat; }
</style>
<script language="javascript" type="text/javascript">
function setModulo(nModulo, oImg){
    try{
        //alert(nModulo + " " + oImg.getAttribute("src"));
        if (oImg.className == "flechadown") {
            //alert("Contraer grupo: " + nModulo);
            Contraer(nModulo);
        } else {
            //alert("Expandir grupo: " + nModulo);
            for (var i = 1; i < 1000; i++) {
                if ($I("modulo_" + i.toString()) == null)
                    break;
                    
                if (i == nModulo)
                    Expandir(i);
                else
                    Contraer(i);
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar los módulos.", e.message);
    }
}
function Expandir(nModulo) {
    try{
        var oTablaModulo = $I("modulo_" + nModulo.toString());
        oTablaModulo.rows[0].cells[0].className = "Expand_top";
        oTablaModulo.rows[1].cells[0].className = "Expand_body";
        oTablaModulo.rows[2].cells[0].className = "Expand_bottom";
        $I("imgFlecha_modulo_" + nModulo.toString()).className = "flechadown";
        var oDiv = oTablaModulo.rows[1].cells[0].getElementsByTagName("DIV")[0];
        oDiv.style.height = (Math.min(oDiv.children[0].rows.length, (540 - (nCountModulos * 32)) / 20) * 20).toString() + "px";
    } catch (e) {
        mostrarErrorAplicacion("Error al expandir un módulo.", e.message);
    }
}
function Contraer(nModulo) {
    try{
        var oTablaModulo = $I("modulo_" + nModulo.toString());
        oTablaModulo.rows[0].cells[0].className = "Collapse_top";
        oTablaModulo.rows[1].cells[0].className = "Collapse_body";
        oTablaModulo.rows[2].cells[0].className = "Collapse_bottom";
        $I("imgFlecha_modulo_" + nModulo.toString()).className = "flecharight";
    } catch (e) {
        mostrarErrorAplicacion("Error al contraer un módulo.", e.message);
    }
}

</script>
<div id="divAP" style="position:absolute; top: 115px; left: 600px; width:400px; height: auto;">
<table style="width:400px;">
    <tr style="height:4px;">
        <td background="../../UserControls/AccionesPendientes/Images/imgFondoGeneral_top.png"></td>
    </tr>
    <tr>
        <td style="background-image: url('../../UserControls/AccionesPendientes/Images/imgFondoGeneral_body.png'); background-repeat:repeat; vertical-align:top;">
            <!-- Inicio de Tareas pendientes -->
            <table style="width:390px; margin-left:4px;">
                <tr style="height:28px;background-image: url('../../UserControls/AccionesPendientes/Images/imgTituloTP.png'); background-repeat:no-repeat;">
                    <td style="font-family:Arial; font-size:10pt; font-weight:bold; color:White; padding-left:6px";> Acciones Pendientes</td>
                </tr>
            </table>
            <!-- Fin de Tareas pendientes -->

            <!-- Inicio de Capa abierta PGE -->
            <table id="modulo_1" class="TablaModulo">
                <tr style="height:4px;">
                    <td class="Collapse_top"></td>
                </tr>
                <tr>
                    <td class="Collapse_body">
                        <!-- Inicio de módulo PGE -->
                        <table class="CabeceraModulo">
                            <tr>
                                <td><img id="imgFlecha_modulo_1" src="../../../Images/imgSeparador.gif" class="flechadown" onclick="setModulo(1, this)" /><label onclick="setModulo(1, this.previousSibling)" style="cursor:pointer;">PGE (5)</label></td>
                                <td style="width:20px;"><img src="../../../Images/imgSeparador.gif" class="flagred" /></td>
                            </tr>
                        </table>
                        <!-- Fin de módulo PGE -->
                        <!-- Inicio de Tareas de PGE -->
                        <div style="width:360px; margin-left:20px; margin-top:3px; height:60px; overflow-x:hidden; overflow:auto;">
                        <table style="width:344px;">
                            <colgroup>
                                <col style="width:20px;" >
                                <col style="width:324px;" >
                            </colgroup>
                            <tr style="height:20px;">
                                <td><img src="../../../Images/imgSeparador.gif" class="flagred" /></td>
                                <td class="Link">Proyectos no cerrados</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Proyectos pendientes de reasignar</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Asignación de criterios estadísticos obligatorios</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Cuarta alerta</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Quinta alerta</td>
                            </tr>
                        </table>
                        </div>
                        <!-- Fin de Tareas de PGE -->
                    </td>
                </tr>
                <tr style="height:4px;">
                    <td class="Collapse_bottom"></td>
                </tr>
            </table>
            <!-- Fin de Capa abierta -->

            <!-- Inicio de Capa cerrada CVT -->
            <table id="modulo_2" class="TablaModulo">
                <tr style="height:4px;">
                    <td class="Collapse_top"></td>
                </tr>
                <tr>
                    <td class="Collapse_body">
                        <!-- Inicio de módulo CVT -->
                        <table class="CabeceraModulo">
                            <tr>
                                <td><img id="imgFlecha_modulo_2" src="../../../Images/imgSeparador.gif" class="flecharight" onclick="setModulo(2, this)" /><label onclick="setModulo(2, this.previousSibling)" class="Link">CVT (3)</label></td>
                                <td style="width:20px;"></td>
                            </tr>
                        </table>
                        <!-- Fin de módulo CVT -->
                        <!-- Inicio de Tareas de CVT -->
                        <div style="width:360px; margin-left:20px; height:60px; overflow-x:hidden; overflow:auto;">
                        <table style="width:344px;">
                            <colgroup>
                                <col style="width:20px;" >
                                <col style="width:324px;" >
                            </colgroup>
                            <tr style="height:20px;">
                                <td></td>
                                <td>Proyectos no cerrados</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td>Proyectos pendientes de reasignar</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td>Asignación de criterios estadísticos obligatorios</td>
                            </tr>
                        </table>
                        </div>
                        <!-- Fin de Tareas de CVT -->
                    </td>
                </tr>
                <tr style="height:4px;">
                    <td class="Collapse_bottom"></td>
                </tr>
            </table>
            <!-- Fin de Capa abierta -->

            <!-- Inicio de Capa cerrada PST -->
            <table id="modulo_3" class="TablaModulo">
                <tr style="height:4px;">
                    <td class="Collapse_top"></td>
                </tr>
                <tr>
                    <td class="Collapse_body">
                        <!-- Inicio de módulo PST -->
                        <table class="CabeceraModulo">
                            <tr>
                                <td><img id="imgFlecha_modulo_3" src="../../../Images/imgSeparador.gif" class="flecharight" onclick="setModulo(3, this)" /><label onclick="setModulo(3, this.previousSibling)" style="cursor:pointer;">PST (25)</label></td>
                                <td style="width:20px;"><img src="../../../Images/imgSeparador.gif" class="flagyellow" /></td>
                            </tr>
                        </table>
                        <!-- Fin de módulo PST -->
                        <!-- Inicio de Tareas de PST -->
                        <div style="width:360px; margin-left:20px; height:60px; overflow-x:hidden; overflow:auto;">
                        <table style="width:344px;">
                            <colgroup>
                                <col style="width:20px;" >
                                <col style="width:324px;" >
                            </colgroup>
                            <tr style="height:20px;">
                                <td><img src="../../../Images/imgSeparador.gif" class="flagyellow" /></td>
                                <td class="Link">Proyectos no cerrados</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Proyectos pendientes de reasignar</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Asignación de criterios estadísticos obligatorios</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Cuarta alerta</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Quinta alerta</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 6</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 7</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 8</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 9</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 10</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 11</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 12</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 13</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 14</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 15</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 16</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 17</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 18</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 19</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 20</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 21</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 22</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 23</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 24</td>
                            </tr>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Alerta 25</td>
                            </tr>
                        </table>
                        </div>
                        <!-- Fin de Tareas de PST -->
                    </td>
                </tr>
                <tr style="height:4px;">
                    <td class="Collapse_bottom"></td>
                </tr>
            </table>
            <!-- Fin de Capa abierta -->

            <!-- Inicio de Capa cerrada IAP -->
            <table id="modulo_4" class="TablaModulo">
                <tr style="height:4px;">
                    <td class="Collapse_top"></td>
                </tr>
                <tr>
                    <td class="Collapse_body">
                        <!-- Inicio de módulo IAP -->
                        <table class="CabeceraModulo">
                            <tr>
                                <td><img id="imgFlecha_modulo_4" src="../../../Images/imgSeparador.gif" class="flecharight" onclick="setModulo(4, this)" /><label onclick="setModulo(4, this.previousSibling)" style="cursor:pointer;">IAP (1)</label></td>
                                <td style="width:20px;"></td>
                            </tr>
                        </table>
                        <!-- Fin de módulo IAP -->
                        <!-- Inicio de Tareas de IAP -->
                        <div style="width:360px; margin-left:20px; height:60px; overflow-x:hidden; overflow:auto;">
                        <table style="width:344px;">
                            <colgroup>
                                <col style="width:20px;" >
                                <col style="width:324px;" >
                            </colgroup>
                            <tr style="height:20px;">
                                <td></td>
                                <td class="Link">Imputaciones pendientes</td>
                            </tr>
                        </table>
                        </div>
                        <!-- Fin de Tareas de IAP -->
                    </td>
                </tr>
                <tr style="height:4px;">
                    <td class="Collapse_bottom"></td>
                </tr>
            </table>
            <!-- Fin de Capa abierta -->
        </td>
    </tr>
    <tr style="height:4px;">
        <td background="../../UserControls/AccionesPendientes/Images/imgFondoGeneral_bottom.png"></td>
    </tr>
 </table>
</div>
<script language="javascript" type="text/javascript">
<!--
    var nCountModulos = 4;
    Expandir(1);
-->
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

