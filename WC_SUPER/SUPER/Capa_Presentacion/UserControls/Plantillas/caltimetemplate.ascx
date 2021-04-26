<%@ Control Language="c#" %>
<%@ Import Namespace="rw"%>
<div>
    <label style="width:35px"><%# DateTime.Parse(((ScheduleItem)Container).DataItem.ToString()).ToShortTimeString() %></label>
</div>