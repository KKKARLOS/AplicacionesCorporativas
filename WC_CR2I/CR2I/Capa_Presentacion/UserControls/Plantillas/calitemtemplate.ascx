<%@ Control %>
<%@ Import Namespace="rw"%>
<div id='<%# DataBinder.Eval(((ScheduleItem)Container).DataItem,"ID") %>' 
    style="width:100%; cursor:pointer;" 
    onclick="mostrarReserva('<%# DataBinder.Eval(((ScheduleItem)Container).DataItem,"ID") %>');" 
    title="<%# DataBinder.Eval( ((ScheduleItem)Container).DataItem ,"Motivo") %>">
        <div class="NBR" style="width:70px; height:auto; overflow:hidden;">
            <%# DataBinder.Eval( ((ScheduleItem)Container).DataItem ,"Task") %>
        </div>
</div>