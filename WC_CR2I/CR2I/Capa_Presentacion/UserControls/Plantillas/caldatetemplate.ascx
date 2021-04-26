<%@ Control %>
<%@ Import Namespace="rw"%>
<div style="width:95px;">
    <%# DateTime.Parse(((ScheduleItem)Container).DataItem.ToString()).ToShortDateString() %>
</div>