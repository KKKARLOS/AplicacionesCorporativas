<%@ Control Language="c#" %>
<%@ Import Namespace="rw"%>
<div id='<%# DataBinder.Eval(((ScheduleItem)Container).DataItem,"ID") %>' 
    class="NBR" style="overflow:hidden; text-align:middle; width:105px; height:100%; margin:0px;" 
    title="<%# DataBinder.Eval( ((ScheduleItem)Container).DataItem ,"Motivo") %>">
<%# DataBinder.Eval( ((ScheduleItem)Container).DataItem ,"Motivo") %>
</div>