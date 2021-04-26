//	Written by Ricaute Jiménez Sánchez
//	PopCalendarFunctions 1.5.4
//	email : ricaj0625@yahoo.com
//	last updated 10 de Noviembre de 2004

var __PopCalValidCalendarRanges = []
var __PopCalLastControl = ""
var __PopCalTemporal = new String()
var __PopCalPageIsValid = true
var __BlankField = ""
var __ShowMessage = false

function __PopCalValidateOnSubmit(val, args)
{
	if (!__PopCalPageIsValid)
	{
		args.IsValid = false
	}
	else
	{
		args.IsValid = __PopCalValidateControl()
	}
	if (!args.IsValid)
	{
		if (__BlankField!="")
		{
			__PopCalendarBlankField(__BlankField)
		}
	}
	__PopCalPageIsValid = true
}

function __PopCalSetFocus(o)
{
	o.keyboard=true
	o.eventKey=0
	//o.onchange=null
	/*****************************
	* Código modificado para saber cuando
	* se cambia el valor del input para
	* poder realizar alguna acción
	*****************************/
	//try{ o.click() }catch(e){}
	if (o.oldValue == undefined)
    o.oldValue = o.value;

	if (o.oldValue!=o.value) try{ o.fireEvent("onchange") }catch(e){}
	// otra forma: try{ o.onchange() }catch(e){}
	//try{ o.blur() }catch(e){}
	o.oldValue=o.value
}

function __PopCalValidateKey(o, e)
{
	__PopCalLastControl = o.id
	__PopCalHideAllMessage()
	if (o.value!="")
	{
		 if (e.target)
		{
			o.eventKey = e.which
		}
		else		
		{
			o.eventKey = window.event.keyCode
		}
		if (o.eventKey==13)
		{
			if (e.target)
			{
				o.VerifyValue = o.value
				__PopCalFormatControl(o)
				if (o.value=="")
				{
					o.value = o.VerifyValue
					o.VerifyValue = null
				}
			}
			else
			{
				__PopCalPageIsValid = __PopCalValidateControl()
				event.returnValue = __PopCalPageIsValid
				if (!__PopCalPageIsValid)
				{
					if (__BlankField!="")
					{
						__PopCalendarWaitBlankField(document.getElementById(__BlankField))
					}
				}
			}
		}
	}
	return (true)
}

function __PopCalSetBlur(o, e)
{
	__PopCalPageIsValid = __PopCalValidateControl()
}

function __PopCalGetYYYYMMDD(o)
{
	if (o)
	{
		var _PopCal = eval(o.getAttribute("Calendar"))
		if (_PopCal)
		{
			return _PopCal.formatDate(o.value, o.getAttribute("Format"), "yyyy-mm-dd")
		}
	}
	return ""
}

function __PopCalGetFromYYYYMMDD(o)
{
	var _DateFrom = ""
	if (o)
	{
		if (__PopCalTemporal.indexOf("," + o.id.toLowerCase() + ",")!=-1) return _DateFrom
		__PopCalTemporal += (o.id.toLowerCase() + ",")

		var _PopCal = eval(o.getAttribute("Calendar"))
		for (var i=0; i < __PopCalValidCalendarRanges.length ; i++) 
		{
			var _Range = __PopCalValidCalendarRanges[i]
			if (_Range.Control == o.id)
			{
				if (_Range.FromRange=="Hoy")
				{
					_DateFrom = _PopCal.formatDate("Hoy", "yyyy-mm-dd", "yyyy-mm-dd")
				}
				else if (_Range.FromRange.substr(0,2) == "C:")
				{
					_DateFrom = __PopCalGetYYYYMMDD(document.getElementById(_Range.FromRange.substr(2)))
					if (_DateFrom=="")
					{
						_DateFrom = __PopCalGetFromYYYYMMDD(document.getElementById(_Range.FromRange.substr(2)))
					}
				}
				else
				{
					_DateFrom = _Range.FromRange
				}
				if (_DateFrom!="")
				{
					_DateFrom = _PopCal.addDays(_DateFrom, "yyyy-mm-dd", _Range.FromIncrement)
				}
				break
			}	
		}
	}
	return _DateFrom
}

function __PopCalGetToYYYYMMDD(o)
{
	var _DateTo = ""
	if (o)
	{
		if (__PopCalTemporal.indexOf("," + o.id.toLowerCase() + ",")!=-1) return _DateTo
		__PopCalTemporal += (o.id.toLowerCase() + ",")

		var _PopCal = eval(o.getAttribute("Calendar"))
		for (var i=0; i < __PopCalValidCalendarRanges.length ; i++) 
		{
			var _Range = __PopCalValidCalendarRanges[i]
			if (_Range.Control == o.id)
			{
				if (_Range.ToRange=="Hoy")
				{
					_DateTo = _PopCal.formatDate("Hoy", "yyyy-mm-dd", "yyyy-mm-dd")
				}
				else if (_Range.ToRange.substr(0,2) == "C:")
				{
					_DateTo = __PopCalGetYYYYMMDD(document.getElementById(_Range.ToRange.substr(2)))
					if (_DateTo=="")
					{
						_DateTo = __PopCalGetToYYYYMMDD(document.getElementById(_Range.ToRange.substr(2)))
					}
				}
				else
				{
					_DateTo = _Range.ToRange
				}
				if (_DateTo!="")
				{
					_DateTo = _PopCal.addDays(_DateTo, "yyyy-mm-dd", _Range.ToIncrement)
				}
				break
			}		
		}
	}
	return _DateTo
}

function __PopCalShowCalendar(_o, _span)
{ 
	var o = document.getElementById(_o)
	if (!o) return
	var _PopCal = eval(o.getAttribute("Calendar"))
	//var _PopCal = o.getAttribute("Calendar")
	var _format = o.getAttribute("Format")
	var _from = ""
	var _to = ""
	o.oldValue = o.value
	
	for (var i=0; i < __PopCalValidCalendarRanges.length ; i++) 
	{
		var _Range = __PopCalValidCalendarRanges[i]
		if (_Range.Control == o.id)
		{
			__PopCalTemporal = ","
			var _DateFrom = __PopCalGetFromYYYYMMDD(o)
			_from = _PopCal.formatDate(_DateFrom, "yyyy-mm-dd", _format)

			__PopCalTemporal = ","
			var _DateTo = __PopCalGetToYYYYMMDD(o)
			_to = _PopCal.formatDate(_DateTo, "yyyy-mm-dd", _format)

			break
		}
	}
	
	_PopCal.ControlAlignLeft = null
	o.eventKey=0
	o.keyboard = false
	__PopCalLastControl = o.id
	if (o.getAttribute("Buffer")=="true")
	{
		o.BufferValue = o.value
		_PopCal.ControlAlignLeft = _span
	}
	//En IAP no utilizo el 'id' ya que el valor importante es el 'name'
	//_PopCal.show(o, _format, _from, _to, "__PopCalSelectDate('" + o.id + "')")
	_PopCal.show(o, _format, _from, _to, "__PopCalSelectDate('" + o.name + "')")
	__PopCalHideAllMessage()
}

function __PopCalHideAllMessage()
{
	if (!__ShowMessage) return
	for (var i=0; i < __PopCalValidCalendarRanges.length ; i++) 
	{
		var o = document.getElementById(__PopCalValidCalendarRanges[i].Control)
		if (o)
		{
			if (o.getAttribute("Buffer")!="true")
			{
				if (o.TimeOutBlank != null)
				{
					o.value = ""
					o.oldValue = ""
					window.clearTimeout(o.TimeOutBlank)
					o.TimeOutBlank = null
				}
				var _v = document.getElementById(o.getAttribute("Calendar") + "_MessageError")
				if (_v)
				{
					if (_v.style.display!='none')
					{
						__PopCalHideMessage(_v)
					}
				}
			}
		}
	}
	__ShowMessage = false
}

function __PopCalSelectDate(_o)
{
    var o = document.getElementById(_o)
    if (!o) return
	if (o.value!=o.oldValue)
	{
		if (o.getAttribute("Buffer")=="true")
		{
			if (o.BufferValue != o.value)
			{
				var _PopCal = PopCalCalendarVisible()
				if (_PopCal) _PopCal.hide()
				eval(o.getAttribute("PostBack").toString().replace('9999x99x99',o.value))
			}
		}
		else 
		{
			__PopCalValidateDependencies(o)
		}
	}
}

function __PopCalFormatControl(o)
{
	if (o.value!="")
	{
		var _PopCal = eval(o.getAttribute("Calendar"))
		if (_PopCal)
		{
			var _format = o.getAttribute("Format")
			var _Sep = __PopCalGetSeparator(o.value)
			var sRetVal = _PopCal.formatDate(o.value, __PopCalReplaceSeparators(_format, _Sep), _format)
			if (_format.indexOf("mmmm")!=-1)
			{
				if (sRetVal=="") sRetVal = _PopCal.formatDate(o.value, __PopCalReplaceSeparators(_format.replace("mmmm", "mmm"), _Sep), _format)
				if (sRetVal=="") sRetVal = _PopCal.formatDate(o.value, __PopCalReplaceSeparators(_format.replace("mmmm", "mm"), _Sep), _format)
			}
			else if (_format.indexOf("mmm")!=-1)
			{
				if (sRetVal=="") sRetVal = _PopCal.formatDate(o.value, __PopCalReplaceSeparators(_format.replace("mmm", "mmmm"), _Sep), _format)
				if (sRetVal=="") sRetVal = _PopCal.formatDate(o.value, __PopCalReplaceSeparators(_format.replace("mmm", "mm"), _Sep), _format)
			}
			else
			{
				if (sRetVal=="") sRetVal = _PopCal.formatDate(o.value, __PopCalReplaceSeparators(_format.replace("mm", "mmmm"), _Sep), _format)
				if (sRetVal=="") sRetVal = _PopCal.formatDate(o.value, __PopCalReplaceSeparators(_format.replace("mm", "mmm"), _Sep), _format)
			}
			if (sRetVal=="") sRetVal = _PopCal.formatDate(o.value, __PopCalReplaceSeparators("yyyy-mm-dd", _Sep), _format)
			o.value = sRetVal
			return (sRetVal!="")
		}
	}
	return (true)
}

function __PopCalGetTopLeft(_obj)
{
	var t = _obj.offsetTop + _obj.offsetHeight
	var l = _obj.offsetLeft
	var objParent = _obj.offsetParent
	while(objParent.tagName!="BODY")
	{
		t += objParent.offsetTop - objParent.scrollTop
		l += objParent.offsetLeft - objParent.scrollLeft
		if (objParent.tagName=="DIV") 
		{
			if (!isNaN(parseInt(objParent.style.borderTopWidth,10))) t += parseInt(objParent.style.borderTopWidth,10)
			if (!isNaN(parseInt(objParent.style.borderLeftWidth,10))) l += parseInt(objParent.style.borderLeftWidth,10)
		}
		objParent = objParent.offsetParent
	}
	return new Array(t,l)
}

function __PopCalShowMessageWaitForControl(_o, _m)
{
	var o = document.getElementById(_o)
	if (o)
	{
		var c = document.getElementById(o.getAttribute("CalendarControl"))
		if (!c) c = document.getElementById(o.getAttribute("Calendar") + "_Control")
		if (c)
		{
			window.setTimeout("__PopCalShowMessage('" + _o + "', '" + _m + "',true)",250)
		}
		else
		{
			window.setTimeout("__PopCalShowMessageWaitForControl('" + _o + "', '" + _m + "')",10)
		}
	}
	else
	{
		window.setTimeout("__PopCalShowMessageWaitForControl('" + _o + "', '" + _m + "')",10)
	}
}

function __PopCalShowMessage(_o, _m, _focus)
{
	var o = document.getElementById(_o)
	if (!o) return
	__ShowMessage = true
	var _v = document.getElementById(o.getAttribute("Calendar") + "_MessageError")
	if (_v.getAttribute("ShowMessageBox")=="true")
	{
		__PopCalAlert(_m)
		__PopCalendarBlankField(o.id)
	}
	else
	{
		var _PopCal = eval(o.getAttribute("Calendar"))
		if (_PopCal)
		{
			if ((!_v.popupOverMessage)&&(_PopCal.ie)&&(_PopCal.ieVersion>=5.5))
			{
				PopCalWriteHTML("<iframe id='popupOverMessage" + _PopCal.id + "' scrolling=no frameborder=0 style='position:absolute;left:0px;top:0px;width:0px;height:0px;z-index:+10000;display:none;filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);'></iframe>")
				_v.popupOverMessage = document.getElementById("popupOverMessage" + _PopCal.id)
			}
		}
		var _tl
		if (_v.getAttribute("MessageAlignment")=='RightCalendarControl')
		{
			var c = document.getElementById(o.getAttribute("CalendarControl"))
			if (!c) c = document.getElementById(o.getAttribute("Calendar") + "_Control")
			_tl = __PopCalGetTopLeft(c)
			_tl[0] -= (c.offsetHeight - 1)
			_tl[1] += (c.offsetWidth + 10)
			if ((_v.style.padding=='2px')||(_v.style.padding=='2px 2px 2px 2px')) _tl[0] -= 4
		}
		else
		{
			_tl = __PopCalGetTopLeft(o)
			if ((_v.style.padding=='2px')||(_v.style.padding=='2px 2px 2px 2px')) _tl[0] += 4
		}
		_v.innerHTML = _m
		_v.style.top = _tl[0] + 1
		_v.style.left = _tl[1]
		_v.style.whiteSpace = 'nowrap'
		_v.style.zIndex=+100000
		_v.style.display = ''
		if (_v.popupOverMessage)
		{
			_v.popupOverMessage.style.top = parseInt(_v.style.top,10)
			_v.popupOverMessage.style.left = parseInt(_v.style.left,10)
			_v.popupOverMessage.style.height = _v.offsetHeight
			_v.popupOverMessage.style.width = _v.offsetWidth
			_v.popupOverMessage.style.display = ''
		}
		_v.timeOut = window.setTimeout('__PopCalHideMessage(document.getElementById("' + _v.id +'"))', 5000)
		__PopCalendarWaitBlankField(o)
	}
	if (_focus) __PopCalControlFocus(o)
}

function __PopCalHideMessage(_v)
{
	if (_v.timeOut) window.clearTimeout(_v.timeOut)
	_v.timeOut = null
	
	if (_v.style.display != 'none')
	{
		_v.style.display = 'none'
		_v.innerHTML = ""
		_v.style.top = 0
		_v.style.left = 0
	}
	if (_v.popupOverMessage)
	{
		if (_v.popupOverMessage.style.display != 'none')
		{
			_v.popupOverMessage.style.top = 0
			_v.popupOverMessage.style.left = 0
			_v.popupOverMessage.style.display = 'none'
		}
	}
}

function __PopCalValidateControl()
{
	if (__PopCalLastControl=="") return (true)
	var o = document.getElementById(__PopCalLastControl)
	if (!o) return(true)
	var _PopCal = eval(o.getAttribute("Calendar"))
	if (!_PopCal) return(true)
	var _format = o.getAttribute("Format")
	var _ValidControl = document.getElementById(o.getAttribute("Calendar") + "_MessageError")
	var _ValidateControl = document.getElementById(o.getAttribute("Calendar") + "_Validate")

	__PopCalLastControl = ""
	o.eventKey=0
	o.VerifyValue  = o.value
	if (!__PopCalFormatControl(o))
	{
		o.value = o.VerifyValue 
		o.oldValue = o.VerifyValue
		o.VerifyValue  = null
		var _invalidDate = o.getAttribute("InvalidDateMessage") 
		if ((_invalidDate=="")||(_invalidDate==null)) _invalidDate = "Invalid Date"
		__PopCalShowMessage(o.id, _invalidDate, true)
		if (_ValidateControl) _ValidateControl.errormessage = _invalidDate
		return (false)
	}

	if ((o.value != "")&&(o.oldValue != o.value))
	{
		var __Holiday = false
		var _date = new Date()
		_date = _PopCal.getDate(o.value, _format)
		if (_PopCal.calendarInstance.showHolidays=="1")
		{
			if (_PopCal.calendarInstance.selectHoliday=="0")
			{
				var _holiday = o.getAttribute("HolidayMessage") 
				if ((_holiday=="")||(_holiday==null))
				{
					_holiday = "Disabled Holidays"
				}
				for (var k=0;k<_PopCal.HolidaysCounter;k++)
				{
					if (_PopCal.Holidays[k].tipo==1)
					{
						__Holiday = false
						if (_PopCal.Holidays[k].type == "Type 1")
						{
							__Holiday = PopCalValidateType1(_date, _PopCal.Holidays[k])
						}
						else if (_PopCal.Holidays[k].type == "Type 2")
						{
							__Holiday = PopCalValidateType2(_date, _PopCal.Holidays[k])
						}
						if (__Holiday)
						{
							o.oldValue = o.value
							__PopCalShowMessage(o.id, _holiday, true)
							if (_ValidateControl) _ValidateControl.errormessage = _holiday
							return (false)
						}
					}
				}
				var _DomingoPascuas = PopCalDomingoPascuas(_date.getFullYear())
				var _HolidayDate = new Date()
				if (_PopCal.calendarInstance.addCarnival=="1")
				{
					_HolidayDate = new Date(_DomingoPascuas - (47 * 86400000))
					if (_HolidayDate.toString()==_date.toString())
					{
						o.oldValue = o.value
						__PopCalShowMessage(o.id, _holiday, true)
						if (_ValidateControl) _ValidateControl.errormessage = _holiday
						return (false)
					} 
				}
				if (_PopCal.calendarInstance.addGoodFriday=="1")
				{
					_HolidayDate = new Date(_DomingoPascuas - (2 * 86400000))
					if (_HolidayDate.toString()==_date.toString())
					{
						o.oldValue = o.value
						__PopCalShowMessage(o.id, _holiday, true)
						if (_ValidateControl) _ValidateControl.errormessage = _holiday
						return (false)
					} 
				}
			}
		}
		
		if (_PopCal.calendarInstance.selectWeekend=="0")
		{
			var _weekend = o.getAttribute("WeekendMessage") 
			if ((_weekend=="")||(_weekend==null)) _weekend = "Disabled Weekends"
			_date = _PopCal.getDate(o.value, _format)
			if ("06".indexOf(_date.getDay().toString())!=-1)
			{
				o.oldValue = o.value
				__PopCalShowMessage(o.id, _weekend, true)
				if (_ValidateControl) _ValidateControl.errormessage = _weekend
				return (false)
			}
		}
		return (__PopCalValidateRanges(o))
	}
	return(true)
}

function __PopCalValidateRanges(o)
{
	var _PopCal = eval(o.getAttribute("Calendar"))
	var _format = o.getAttribute("Format")
	var _ValidControl = document.getElementById(o.getAttribute("Calendar") + "_MessageError")
	var _ValidateControl = document.getElementById(o.getAttribute("Calendar") + "_Validate")
	
	var _Range = null
	var _c = null
	var _value = _PopCal.formatDate(o.value, _format, "yyyy-mm-dd")
	if (o.oldValue!=o.value)
	{
		o.oldValue = ""
		for (var i=0; i < __PopCalValidCalendarRanges.length ; i++) 
		{
			_Range = __PopCalValidCalendarRanges[i]
			if (_Range.Control == o.id)
			{
				break
			}
			_Range = null
		}
		if (_Range!=null)
		{
			__PopCalTemporal = ","
			var _DateFrom = __PopCalGetFromYYYYMMDD(o)
			if (_DateFrom!="")
			{
				if (_value < _DateFrom)
				{
					o.oldValue = o.value
					var _OutRange = _Range.FromMessage
					if (_OutRange=="") _OutRange = "Out of Range"	
					__PopCalShowMessage(o.id, _OutRange, true)
					if (_ValidateControl) _ValidateControl.errormessage = _OutRange
					return (false)
				}
			}

			__PopCalTemporal = ","
			var _DateTo = __PopCalGetToYYYYMMDD(o)
			if (_DateTo!="")
			{
				if (_value > _DateTo)
				{
					o.oldValue = o.value
					var _OutRange = _Range.ToMessage
					if (_OutRange=="") _OutRange = "Out of Range"
					__PopCalShowMessage(o.id, _OutRange, true)
					if (_ValidateControl) _ValidateControl.errormessage = _OutRange
					return (false)
				}
			}
		}
		return (__PopCalValidateDependencies(o))
	}
	o.oldValue = ""
	return (true)
}

function __PopCalValidateDependencies(o)
{
	var oFocus = null
	for (var i=0; i < __PopCalValidCalendarRanges.length ; i++) 
	{
		var _Range = __PopCalValidCalendarRanges[i]
		if (document.getElementById(_Range.Control) != null && document.getElementById(_Range.Control).getAttribute("Buffer")!="true")
		{
			if (_Range.Control!=o.id)
			{
				
				if (_Range.FromRange == "C:" + o.id)
				{
					if (!__PopCalValidateFromRange(_Range, o))
					{
						if (oFocus==null)
						{
							oFocus = document.getElementById(_Range.Control)
						}
					}
				}
				if (_Range.ToRange == "C:" + o.id)
				{
					if (!__PopCalValidateToRange(_Range, o))
					{
						if (oFocus==null)
						{
							oFocus = document.getElementById(_Range.Control)
						}
					}
				}
			}
		}
	}
	if (oFocus!=null)
	{
		oFocus.oldValue = ""
		__PopCalControlFocus(oFocus)
		return (false)
	}
	else if (!o.keyboard)
	{
		o.oldValue = ""
		__PopCalControlFocus(o)
	}
	return (true)
}

function __PopCalValidateFromRange(_Range, o)
{
	var oControl = document.getElementById(_Range.Control)
	if (!oControl) return (true)
	if (oControl.getAttribute("Calendar")=="") return (true)
	var _PopCal = eval(oControl.getAttribute("Calendar"))		
	var _format = oControl.getAttribute("Format")
	var _ValidControl = document.getElementById(oControl.getAttribute("Calendar") + "_MessageError")
	var _ValidateControl = document.getElementById(o.getAttribute("Calendar") + "_Validate")
	
	
	var _value = _PopCal.formatDate(oControl.value, _format, "yyyy-mm-dd")
	if (_value=="") return (true)

	_PopCal = eval(o.getAttribute("Calendar"))
	_format = o.getAttribute("Format")
	var _DateFrom = _PopCal.formatDate(o.value, _format, "yyyy-mm-dd")

	
	if (_value < _DateFrom)
	{
		oControl.oldValue = oControl.value
		
		var _OutRange = _Range.FromMessage
		if (_OutRange=="") _OutRange = "Out of Range"
		__PopCalShowMessage(oControl.id, _OutRange, false)
		if (_ValidateControl) _ValidateControl.errormessage = _OutRange
		return (false)
	}
	return (true)
}

function __PopCalValidateToRange(_Range, o)
{
	var oControl = document.getElementById(_Range.Control)
	if (!oControl) return (true)
	if (oControl.getAttribute("Calendar")=="") return (true)
	var _PopCal = eval(oControl.getAttribute("Calendar"))		
	var _format = oControl.getAttribute("Format")
	var _value = _PopCal.formatDate(oControl.value, _format, "yyyy-mm-dd")
	var _ValidControl = document.getElementById(oControl.getAttribute("Calendar") + "_MessageError")
	var _ValidateControl = document.getElementById(o.getAttribute("Calendar") + "_Validate")

	if (_value=="") return (true)

	_PopCal = eval(o.getAttribute("Calendar"))
	_format = o.getAttribute("Format")
	var _DateTo = _PopCal.formatDate(o.value, _format, "yyyy-mm-dd")

	if (_value > _DateTo)
	{
		var _OutRange = _Range.ToMessage
		if (_OutRange=="") _OutRange = "Out of Range"
		__PopCalShowMessage(oControl.id, _OutRange, false)
		if (_ValidateControl) _ValidateControl.errormessage = _OutRange
		return (false)
	}
	return (true)
	
}

function __PopCalAlert(_alert)
{
	__PopCalHideElement(document.getElementById("popupOverCalendar"))
	__PopCalHideElement(document.getElementById("popupSuperShadowRight"))
	__PopCalHideElement(document.getElementById("popupSuperShadowBottom"))
	__PopCalHideElement(document.getElementById("popupOverShadow"))
	alert(_alert)
}

function __PopCalHideElement(_obj)
{
	if (_obj)
	{
		_obj.style.visibility="hidden"
		_obj.style.left = 0
		_obj.style.top = 0
	}
}

function __PopCalendarWaitBlankField(o)
{
	__BlankField = o.id
	if (o.select) o.select()
	o.TimeOutBlank = window.setTimeout("__PopCalendarBlankField('" + o.id + "')", 500)
}

function __PopCalendarBlankField(_o)
{
	var o = document.getElementById(_o)
	if (!o) return
	if (o.TimeOutBlank) window.clearTimeout(o.TimeOutBlank)
	o.TimeOutBlank = null
	__BlankField = ""
	o.value = ""
	o.oldValue = ""
	__PopCalControlFocus(o)
	if (o.select) o.select()
}

function __PopCalObjectCalendarRange()
{
	this.Control = ""
	this.FromRange = ""
	this.FromIncrement = 0
	this.FromMessage = ""
	this.ToRange = ""
	this.ToIncrement = 0
	this.ToMessage = ""
}

function __PopCalAddCalendarRange(_Control, _FromRange, _FromIncrement, _FromMessage, _ToRange, _ToIncrement, _ToMessage)
{
	var _Range = new __PopCalObjectCalendarRange()
	_Range.Control = _Control
	_Range.FromRange = _FromRange
	_Range.FromIncrement = _FromIncrement
	_Range.FromMessage = _FromMessage
	_Range.ToRange = _ToRange
	_Range.ToIncrement = _ToIncrement
	_Range.ToMessage = _ToMessage
	if (_Range.FromRange=='') _Range.FromRange = '1900-01-01'
	if (_Range.ToRange=='') _Range.ToRange = '2099-12-31'
	__PopCalValidCalendarRanges[__PopCalValidCalendarRanges.length] = _Range
}

function __PopCalGetSeparator(_f)
{
	if (_f.indexOf("-")!=-1) return "-"
	else if (_f.indexOf("/")!=-1) return "/"
	return "."
}

function __PopCalReplaceSeparators(_f, _s)
{
	var _r = _f
	_r = _r.split('.').join(_s)
	_r = _r.split('-').join(_s)
	_r = _r.split('/').join(_s)
	return _r
}

function __PopCalControlFocus(o)
{
	try
	{
		o.focus();
		return (true)
	}
	catch (e)
	{
		return (false)
	}
}