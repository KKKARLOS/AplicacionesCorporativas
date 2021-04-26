<%@ Control Language="c#" %>
<%@ Import Namespace="rw"%>
<div id='<%# DataBinder.Eval(((ScheduleItem)Container).DataItem,"ID") %>' 
    class="NBR" style="overflow:hidden;text-align:middle; width:110px; height :100%;cursor:pointer; margin:0px;" 
    onclick="mostrarReserva('<%# DataBinder.Eval(((ScheduleItem)Container).DataItem,"ID") %>');" 
    title="<%# DataBinder.Eval( ((ScheduleItem)Container).DataItem ,"Motivo") %>">
<%# DataBinder.Eval(((ScheduleItem)Container).DataItem, "Motivo")%>
</div>