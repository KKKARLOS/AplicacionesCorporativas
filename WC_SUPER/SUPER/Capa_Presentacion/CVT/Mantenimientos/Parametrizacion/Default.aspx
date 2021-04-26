<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Parametrizacion" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
    <script language="javascript" type="text/javascript">
        $I("procesando").style.top = '420px';
    </script>
    <center>	
    <table style="width:900px;text-align:left;margin-left:100px" border="0">
    <colgroup><col style="width:900px;" /></colgroup>
        <tr>
            <td>
                <div style="width:900px">
                    <div align="center" style="background-image: url('../../../../Images/imgFondo100.gif');background-repeat:no-repeat; 
                        width:100px; height:23px; position:relative; top:20px; left:20px; padding-top:7px;" class="texto"> 
                        &nbsp;Notificaciones CVT
                    </div>
                    <table border="0" cellpadding="0" cellspacing="0" class="texto">
                        <tr>
                            <td style="background-image:url('../../../../Images/Tabla/7.gif'); height:6px; width:6px;"></td>
                            <td style="background-image:url('../../../../Images/Tabla/8.gif'); height:6px;"></td>
                            <td style="background-image:url('../../../../Images/Tabla/9.gif'); height:6px; width:6px;"></td>
                        </tr>
                        <tr>
                            <td style="background-image:url('../../../../Images/Tabla/4.gif'); width:6px;">&nbsp;</td>
                            <td style="background-image:url('../../../../Images/Tabla/5.gif'); padding:5px">
                                <!-- Inicio del contenido propio de la página -->
                                <fieldset style="width: 740px; height:60px; margin-top:20px">
                                <legend>Actualización masiva</legend>                                  
                                <table border="0" cellpadding="3px" cellspacing="0" class="texto" width="710px">
                                    <colgroup>
                                        <col style="width:360px;" />
                                        <col style="width:350px;" />
                                    </colgroup>
                                    <tr>
                                        <td style="vertical-align:middle;"  title="Fecha en la que el proceso nocturno creará a todos los profesionales no excluidos de actualizar su CV la tarea de 'actualización del CV' "><label style="margin-top:15px">Fecha proceso: </label><asp:TextBox ID="txt_fproceso_act_masi" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="aG()"></asp:TextBox></td>
                                        <td align="right">
                                            <button id="btnCronologia" type="button" onclick="getCronologia()" class="btnH25W105" hidefocus=hidefocus runat=server>
                                                <span><img src="../../../../Images/imgHorario.gif" alt="Cronología de actualizaciones realizadas" />&nbsp;Cronología</span>
                                            </button>
                                        </td>
                                    </tr>
                                </table>
                                </fieldset>                              
                                <fieldset style="width: 740px; height:180px; margin-top:20px">
                                    <legend>Plazos de ejecución tareas</legend>                                     
                                    <table border="0" cellpadding="3px" cellspacing="0" class="texto" width="710px" style="margin-top:10px;margin-left:15px">
                                    <colgroup>
                                        <col style="width:500px;" />
                                        <col style="width:210px;" />
                                    </colgroup>                                
                                        <tr class="TBLINI">
                                            <td>Tarea</td>
                                            <td align="center">Nº de días para su realización</td>
                                        </tr>
                                   </table>
                                   <table border="0" cellpadding="3px" cellspacing="0" class="texto" width="710px" style="margin-left:15px">
                                   <colgroup>
                                        <col style="width:510px;" />
                                        <col style="width:200px;" />
                                   </colgroup>                                                                                                
                                        <tr class="FA">
                                            <td>- Actualización masiva</td>
                                            <td align="center"><input id="txt_ndias_act_masi" type="text" style="width:50px;" maxlength="4" class="txtNumM" onkeyup="aG()" onfocus="fn(this,4,0);" runat="server" /></td>
                                        </tr>
                                        <tr class="FB">
                                            <td>- Enviar a validar un registro en estado 'Pdte de cumplimentar'</td>
                                            <td align="center"><input id="txt_ndias_envi_validar" type="text" style="width:50px;" maxlength="4" class="txtNumM" onkeyup="aG()" onfocus="fn(this,4,0);" runat="server" /></td>
                                        </tr>

                                        <tr class="FA">
                                            <td>- Validar un registro de un CV</td>
                                            <td align="center"><input id="txt_ndias_validar_reg" type="text" style="width:50px;" maxlength="4" class="txtNumM" onkeyup="aG()" onfocus="fn(this,4,0);" runat="server" /></td>
                                        </tr>
                                        <tr class="FB">
                                            <td>- Cualificar un proyecto para CVT</td>
                                            <td align="center"><input id="txt_ndias_cualifi_proy" type="text" style="width:50px;" maxlength="4" class="txtNumM" onkeyup="aG()" onfocus="fn(this,4,0);" runat="server" /></td>
                                        </tr>
                                        
                                        <tr class="FA">
                                            <td>- Peticiones de borrado</td>
                                            <td align="center"><input id="txt_ndias_peticion_bor" type="text" style="width:50px;" maxlength="4" class="txtNumM" onkeyup="aG()" onfocus="fn(this,4,0);" runat="server" /></td>
                                        </tr>
                                        <tr class="FB">
                                            <td>- Alta de una nueva experiencia (Nº de días transcurridos desde la primera imputación al proyecto)</td>
                                            <td align="center"><input id="txt_ndias_alta_exp" type="text" style="width:50px;" maxlength="4" class="txtNumM" onkeyup="aG()" onfocus="fn(this,4,0);" runat="server" /></td>
                                        </tr>                                                                             
                                    </table>                                            
                                </fieldset> 
                                <fieldset style="width: 740px; height:90px; margin-top:20px">
                                    <legend>Acciones planificadas</legend>                                     
                                    <table border="0" cellpadding="3px" cellspacing="0" class="texto" width="710px" style="margin-top:10px;margin-left:15px">
                                        <colgroup>
                                            <col style="width:410px;" />
                                            <col style="width:150px;" />
                                            <col style="width:150px;" />
                                        </colgroup>                                                         
                                        <tr class="TBLINI">
                                            <td>Acción</td>
                                            <td>F.Últ.Envío</td>
                                            <td align="center">Nº de días</td>
                                        </tr>
                                   </table>  
                                   <table border="0" cellpadding="3px" cellspacing="0" class="texto" width="710px" style="margin-left:15px">
                                       <colgroup>
                                            <col style="width:410px;" />
                                            <col style="width:150px;" />
                                            <col style="width:150px;" />
                                       </colgroup>                                                                                                
                                        <tr class="FA">
                                            <td>- Tareas pendientes de realizar y no vencidas</td>
                                            <td><asp:TextBox ID="txt_fultenvio_tar_ven_noven" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="aG()" ></asp:TextBox></td>
                                            <td align="center"><input id="txt_ndias_tar_ven_noven" type="text" style="width:50px;" maxlength="4" class="txtNumM" onkeyup="aG()" onfocus="fn(this,4,0);" runat="server" /></td>
                                        </tr> 
                                       <tr class="FB">
                                            <td>- Tareas vencidas de mi equipo</td>
                                            <td><asp:TextBox ID="txt_fultenvio_tar_ven_mieq" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="aG()"></asp:TextBox></td>
                                            <td align="center"><input id="txt_ndias_tar_ven_mieq" type="text" style="width:50px;" maxlength="4" class="txtNumM" onkeyup="aG()" onfocus="fn(this,4,0);" runat="server" /></td>
                                        </tr>                                         
                                    </table>                                                                             
                                </fieldset>                                                                
    
                                <!-- Fin del contenido propio de la página -->
                            </td>
                            <td style="background-image:url('../../../../Images/Tabla/6.gif'); width:6px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="background-image:url('../../../../Images/Tabla/1.gif'); height:6px; width:6px;"></td>
                            <td style="background-image:url('../../../../Images/Tabla/2.gif'); height:6px; "></td>
                            <td style="background-image:url('../../../../Images/Tabla/3.gif'); height:6px; width:6px;"></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera"></asp:Content>

<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
    <script language="javascript" type="text/javascript">
        function __doPostBack(eventTarget, eventArgument) {
            var bEnviar = true;
            if (eventTarget.split("$")[2] == "Botonera") {
                var strBoton = Botonera.botonID(eventArgument).toLowerCase();
                //alert("strBoton: "+ strBoton);
                switch (strBoton) {
                    case "grabar":
                        {
                            bEnviar = false;
                            grabar();
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

