<%@ Control Language="c#" %>
<%@ Import Namespace="rw"%>
<div id='<%# DataBinder.Eval(((ScheduleItem)Container).DataItem,"ID") %>' style="width:100%; height :100%;cursor:hand; margin:0px;" onclick="mostrarReserva('<%# DataBinder.Eval(((ScheduleItem)Container).DataItem,"ID") %>');" title="<%# DataBinder.Eval( ((ScheduleItem)Container).DataItem ,"Motivo") %>">
<label style="width:120px;text-overflow:ellipsis;overflow:hidden"><nobr><%# DataBinder.Eval(((ScheduleItem)Container).DataItem, "Motivo")%></nobr></label>
</div>