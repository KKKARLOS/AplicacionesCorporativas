<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Ayuda_Requisitos_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<br />
<center>
	<DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 738px; height:570px">
        <div align="left" style="width: 700px">
        <table border="0" class="texto" width="700px" cellspacing="0" cellpadding="0">
          <tr>
            <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
          </tr>
          <tr>
            <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../Images/Tabla/5.gif" style="padding:15px">
            <!-- Inicio del contenido propio de la página -->
            <br />
            <lu>
                <li><b>Navegadores soportados: </b>SUPER está homologado para Internet Explorer 11, Chrome 56, Mozilla Firefox 52 y Safari 5.1.7. Cualquier otro navegador o versión diferente a la especificada, puede dar lugar a situaciones inesperadas.<br /><br /></li>
                <li><b>Exportación a Excel:</b> Para poder generar y abrir el archivo Excel, además de tener instalado Microsoft Excel en la máquina local, en las políticas de seguridad de su navegador debe configurar el parámetro "Inicializar y activar la secuencia de comandos de los controles de Active X no marcados como seguros", tanto en el apartado "Seguridad --> Intranet local" si accede desde la Intranet como en el "Seguridad --> Internet" si el acceso se produce desde la Extranet.<br /><br /></li>
                <li><b>Exportación a PDF:</b> Para poder generar y abrir el fichero PDF, se requiere de tener instalado Adobe Reader en la máquina local.<br /><br /></li>
                <li><b>Visualización de textos de ayuda:</b> Se requiere de tener instalado Adobe Reader en la máquina local.<br /><br /></li>
                <li><b>Visualización de vídeos de ayuda:</b> Para la visualización de vídeos de ayuda, deberá tener instalada una herramienta reproductora de vídeos en formato WMF. Si dispone de Windows Media Player (estándar en Ibermática), en función de la tarjeta de vídeo que tenga instalada en su ordenador, puede que los colores de los vídeos se vean distorsionados (en rojo generalmente). Para solucionar esto, deberá configurar la "aceleración de vídeo" al mínimo posible. Esto se hace desde el menú "Herramientas" (Tools si dispone de la versión inglesa) del Windows Media Player, opción "Rendimiento" (Performance si dispone de la versión inglesa).<br /><br /></li>
            </lu> 
            <!-- Fin del contenido propio de la página -->
            </td>
            <td width="6" background="../../../Images/Tabla/6.gif">&nbsp;</td>
          </tr>
          <tr>
            <td width="6" height="6" background="../../../Images/Tabla/1.gif"></td>
            <td height="6" background="../../../Images/Tabla/2.gif"></td>
            <td width="6" height="6" background="../../../Images/Tabla/3.gif"></td>
          </tr>
        </table>
        </div>
        <br />
    </DIV>
</center>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

