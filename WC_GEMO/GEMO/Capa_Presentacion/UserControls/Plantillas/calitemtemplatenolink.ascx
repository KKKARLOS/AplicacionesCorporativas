<%@ Control Language="c#" %>
<%@ Import Namespace="rw"%>
<div id='<%# DataBinder.Eval(((ScheduleItem)Container).DataItem,"ID") %>' style="width:100%;height:100%;" title="<%# DataBinder.Eval( ((ScheduleItem)Container).DataItem ,"Motivo") %>">
<label style="width:90px;text-overflow:ellipsis;overflow:hidden"><nobr><%# DataBinder.Eval( ((ScheduleItem)Container).DataItem ,"Motivo") %></nobr></label>
</div>