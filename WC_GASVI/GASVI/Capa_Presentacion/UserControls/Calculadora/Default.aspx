<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Calculadora_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: GASVI ::: - Calculadora</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>    
    <link rel="stylesheet" type="text/css" href="calculadora.css" />
    <script type="text/javascript" language="JavaScript" src="../../../Javascript/funciones.js" ></script>
    <script type="text/javascript" language="JavaScript" src="Functions/calculadora.js" ></script>
</head>
<body style="overflow:hidden;">
<form name="calculator" style="">
<table style="width:165px; height:250px; table-layout:fixed;" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <map id="MapPulsacion" name="MapPulsacion">
  	            <area shape="rect" coords="20,52,78,69" onclick="llevar();" title="LLeva el resultado a la casilla activa (F10)" /><!-- alt="Llevar" -->
                <area shape="rect" coords="121,52,145,69" onclick="hide_calc();" /><!-- alt="OFF" -->
                <area shape="rect" coords="20,81,44,98" onclick="clear_mem();" /><!-- alt="MC" -->
                <area shape="rect" coords="54,81,78,98" onclick="push_mem();" /><!-- alt="M+" -->
                <area shape="rect" coords="88,81,112,98" onclick="minus_mem();" /><!-- alt="M-" -->
                <area shape="rect" coords="121,81,145,98" onclick="pop_mem();" /><!-- alt="MR" -->
                <area shape="rect" coords="20,107,44,124" onclick="clearDisplay();" /><!-- alt="AC" -->
                <area shape="rect" coords="54,107,78,124" onclick="teclaC();" /><!-- alt="C" -->
                <area shape="rect" coords="88,107,112,124" onclick="enter('/');" /><!-- alt="/" -->
                <area shape="rect" coords="121,107,145,124" onclick="enter('*');" /><!-- alt="*" -->
                <area shape="rect" coords="20,134,44,151" onclick="enter(7);" /><!-- alt="7" -->
                <area shape="rect" coords="54,134,78,151" onclick="enter(8);" /><!-- alt="8" -->
                <area shape="rect" coords="88,133,112,150" onclick="enter(9);" /><!-- alt="9" -->
                <area shape="rect" coords="121,134,145,151" onclick="enter('-');" /><!-- alt="-" -->
                <area shape="rect" coords="21,160,45,177" onclick="enter(4);" /><!-- alt="4" -->
                <area shape="rect" coords="54,160,78,177" onclick="enter(5);" /><!-- alt="5" -->
                <area shape="rect" coords="88,160,112,177" onclick="enter(6);" /><!-- alt="6" -->
                <area shape="rect" coords="121,160,145,177" onclick="enter('+');" /><!-- alt="+" -->
                <area shape="rect" coords="20,187,44,204" onclick="enter(1);" /><!-- alt="1" -->
                <area shape="rect" coords="54,187,78,204" onclick="enter(2);" /><!-- alt="2" -->
                <area shape="rect" coords="88,187,112,204" onclick="enter(3);" /><!-- alt="3" -->
                <area shape="rect" coords="20,213,78,232" onclick="enter(0);" /><!-- alt="0" -->
                <area shape="rect" coords="88,213,111,232" onclick="enter('.');" /><!-- alt="." -->
                <area shape="rect" coords="121,187,145,231" onclick="calc();" /><!-- alt="=" -->
            </map>
            <img border="0" src="images/imgFondoCalculadora.gif" width="165" height="250" usemap="#MapPulsacion" />
            <img border="0" id="imgTapaLlevar" src="images/imgTapaLlevar.gif" style="width:67px; height:27; z-index:2; display:block; position:absolute; left:16px; top:47px;" border="1" />
            <img border="0" id="imgCursorMover" src="images/imgCursorMover.gif" style="width:19px; height:19; z-index:2; position:absolute; left:90px; top:52px;" onmouseover="this.style.cursor='move'" onmouseout="this.style.cursor='default'" border="0" onmousedown="popupCalculadora_DragDrop(event);" ondragstart="return false;" /><!--  -->
			<div class="numeric-display-holder">
				<div id="lcd-symbols"></div>
				<input
					value="0"
					class="n_display"
					type="text"
					name="expr"
					readonly=readonly />
					<!--onClick="blur(this)"-->
				</div>
        </td>
    </tr>
</table>
</form>
</body>
</html>
