<%@ Control Language="C#" ClassName="mmoff" %>
<table id="tblMsg" style="opacity:0; filter:alpha(opacity='0'); display:none; z-index: 9999; position: absolute; top: 300px; left: 400px; width:100px;">
<colgroup>
    <col style="width:6px;" />
    <col style="width:25px;" />
    <col style="width:auto;" />
    <col style="width:11px;" />
</colgroup>
    <tr> 
        <td id="tdMsg1" class="tdMsg1Inf"></td>
        <td colspan="2" id="tdMsg2" class="tdMsg2Inf"></td>
        <td id="tdMsg3" class="tdMsg3Inf" style="cursor:pointer;" onclick="hide_mmoff();"><img id="imgPopUpClose" src="<%=Session["strServer"] %>Capa_Presentacion/UserControls/Msg/Images/popup_close.png" style="cursor:pointer; position:absolute; top: -9px; right:-10px; z-index: 9999;" onclick="hide_mmoff();" /></td>
    </tr>
    <tr id="trMsg">
        <td id="tdMsg4" class="tdMsg4Inf"></td>
        <td id="tdIcono"><img id="imgIconoMsg" style="margin-top:1px;" /></td>
        <td id="tdMsg5"></td>
        <td id="tdMsg6" class="tdMsg6Inf"></td>
    </tr>
    <tr>
        <td id="tdMsg7" class="tdMsg7Inf"></td>
        <td colspan="2" id="tdMsg8" class="tdMsg8Inf"></td>
        <td id="tdMsg9" class="tdMsg9Inf"></td>
    </tr>
</table>
<img id="imgPopUpCloseIE" src="<%=Session["strServer"] %>Capa_Presentacion/UserControls/Msg/Images/popup_close.png" style="display:none; cursor:pointer; z-index: 9999;" onclick="hide_mmoff();" />
<script language="JavaScript" src="<%=Session["strServer"] %>Capa_Presentacion/UserControls/Msg/Functions/funciones.js" type="text/Javascript"></script>
<script type="text/javascript" language="javascript">
    var oimgIconoMsg = $I("imgIconoMsg");
    var otdMsg1 = $I("tdMsg1");
    var otdMsg2 = $I("tdMsg2");
    var otdMsg3 = $I("tdMsg3");
    var otdMsg4 = $I("tdMsg4");
    var otdMsg5 = $I("tdMsg5");
    var otdMsg6 = $I("tdMsg6");
    var otdMsg7 = $I("tdMsg7");
    var otdMsg8 = $I("tdMsg8");
    var otdMsg9 = $I("tdMsg9");

    function setEstiloMMOff(sTipo){
        switch(sTipo.toLowerCase()){
            case "inf":
            case "infper":
                $I("imgIconoMsg").setAttribute("src", "<%=Session["strServer"] %>Capa_Presentacion/UserControls/Msg/Images/Information/imgIcono.png");
                otdMsg1.className = "tdMsg1Inf";
                otdMsg2.className = "tdMsg2Inf";
                otdMsg3.className = "tdMsg3Inf";
                otdMsg4.className = "tdMsg4Inf";
                $I("trMsg").setAttribute("style", "background-color:#d1e4f3;color:#3b3019;font-family:Arial;font-size:9pt;");
                otdMsg6.className = "tdMsg6Inf";
                otdMsg7.className = "tdMsg7Inf";
                otdMsg8.className = "tdMsg8Inf";
                otdMsg9.className = "tdMsg9Inf";
                break;
            case "war":
            case "warper":
                $I("imgIconoMsg").setAttribute("src", "<%=Session["strServer"] %>Capa_Presentacion/UserControls/Msg/Images/Warning/imgIcono.png");
                otdMsg1.className = "tdMsg1War";
                otdMsg2.className = "tdMsg2War";
                otdMsg3.className = "tdMsg3War";
                otdMsg4.className = "tdMsg4War";
                $I("trMsg").setAttribute("style", "background-color:#fff5af;color:#3b3019;font-family:Arial;font-size:9pt;");
                otdMsg6.className = "tdMsg6War";
                otdMsg7.className = "tdMsg7War";
                otdMsg8.className = "tdMsg8War";
                otdMsg9.className = "tdMsg9War";
                break;
            case "suc":
            case "sucper":
                $I("imgIconoMsg").setAttribute("src", "<%=Session["strServer"] %>Capa_Presentacion/UserControls/Msg/Images/Success/imgIcono.png");
                otdMsg1.className = "tdMsg1Suc";
                otdMsg2.className = "tdMsg2Suc";
                otdMsg3.className = "tdMsg3Suc";
                otdMsg4.className = "tdMsg4Suc";
                $I("trMsg").setAttribute("style", "background-color:#d0ec7b;color:#3b3019;font-family:Arial;font-size:9pt;");
                otdMsg6.className = "tdMsg6Suc";
                otdMsg7.className = "tdMsg7Suc";
                otdMsg8.className = "tdMsg8Suc";
                otdMsg9.className = "tdMsg9Suc";
                break;
            case "err":
                $I("imgIconoMsg").setAttribute("src", "<%=Session["strServer"] %>Capa_Presentacion/UserControls/Msg/Images/Error/imgIcono.png");
                otdMsg1.className = "tdMsg1Err";
                otdMsg2.className = "tdMsg2Err";
                otdMsg3.className = "tdMsg3Err";
                otdMsg4.className = "tdMsg4Err";
                $I("trMsg").setAttribute("style", "background-color:#fccac1;color:#3b3019;font-family:Arial;font-size:9pt;");
                otdMsg6.className = "tdMsg6Err";
                otdMsg7.className = "tdMsg7Err";
                otdMsg8.className = "tdMsg8Err";
                otdMsg9.className = "tdMsg9Err";
                break;
        }
    }
    //setEstiloMMOff('Inf');
</script>
