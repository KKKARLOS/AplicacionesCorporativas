<%@ Control %>
<%@ Import Namespace="rw"%>
<div style="width:35px; height:12px;">
    <%# DateTime.Parse(((ScheduleItem)Container).DataItem.ToString()).ToShortTimeString() %>
</div>
