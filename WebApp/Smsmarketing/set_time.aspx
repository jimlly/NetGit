<%@ Page Language="C#" AutoEventWireup="true" Inherits="set_time" Codebehind="set_time.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>选择定时发送时间</title>
    <style type="text/css">
body {
	background-color: #D4D0C8;
}
.c_fieldset {
	padding: 0,10,5,10;
	text-align: center;
	width: 150px;
}
.c_legend {
	font-family: Tahoma;
	font-size: 11px;
	padding-bottom: 5px;
}
.c_frameborder {
	border-left: 2px inset #D4D0C8;
	border-top: 2px inset #D4D0C8;
	border-right: 2px inset #FFFFFF;
	border-bottom: 2px inset #FFFFFF;
	background-color: #FFFFFF;
	overflow: hidden;
	font-family: "Tahoma";
	font-size: 10px;
	width:180px;
	height:120px;
}
.c_frameborder td {
	width: 23px;
	height: 16px;
	font-family: "Tahoma";
	font-size: 11px;
	text-align: center;
	cursor: default;
}
.c_frameborder .selected {
	background-color:#0A246A;
	width:12px;
	height:12px;
	color:white;
}
.c_frameborder span {
	width:12px;
	height:12px;
}
.c_arrow {
	width: 16px;
	height: 8px;
	font-family: "Webdings";
	font-size: 7px;
	line-height: 2px;
	padding-left: 2px;
	cursor: default;
}
.c_year {
	font-family: "Tahoma";
	font-size: 11px;
	cursor: default;
	width:55px;
	height:19px;
}
.c_month {
	width:75px;
	height:20px;
	font:11px "Tahoma";
}
.c_dateHead {
	background-color:#808080;
	color:#D4D0C8;
}
.m_fieldset {
	padding: 0,10,5,10;
	text-align: center;
	width: 150px;
}
.m_legend {
	font-family: Tahoma;
	font-size: 11px;
	padding-bottom: 5px;
}
.m_frameborder {
	border-left: 2px inset #D4D0C8;
	border-top: 2px inset #D4D0C8;
	border-right: 2px inset #FFFFFF;
	border-bottom: 2px inset #FFFFFF;
	width: 170px;
	height: 19px;
	background-color: #FFFFFF;
	overflow: hidden;
	text-align: right;
	font-family: "Tahoma";
	font-size: 12px;
}
.m_arrow {
	width: 16px;
	height: 8px;
	font-family: "Webdings";
	font-size: 7px;
	line-height: 2px;
	padding-left: 2px;
	cursor: default;
}
.m_input {
	width: 18px;
	height: 14px;
	border: 0px solid black;
	font-family: "Tahoma";
	font-size: 9px;
	text-align: right;
	ime-mode:disabled;
}
</style>
    <script language="javascript" type="text/javascript">
function ok_choose()
{
		submit1_date = c.getDate()
		submit1_year = submit1_date.split("/")[0]
		submit1_month = submit1_date.split("/")[1]
		submit1_day = submit1_date.split("/")[2]
		
		submit1_month = ((submit1_month < 10) ? '0' : '') + submit1_month
		submit1_day = ((submit1_day < 10) ? '0' : '') + submit1_day
		
		window.returnValue = submit1_year + "-" + submit1_month + "-" + submit1_day + " " + m.getTime()
		window.close()
}


function ok_now()
{
        MyDate = new Date()
		real_date = MyDate.getMonth() + 1
		my_month = ((real_date < 10) ? '0' : '') + real_date
		my_date = ((MyDate.getDate() < 10) ? '0' : '') + MyDate.getDate()
		
		my_hour = ((MyDate.getHours() < 10) ? '0' : '') + MyDate.getHours()
		my_minute = ((MyDate.getMinutes() < 10) ? '0' : '') + MyDate.getMinutes()
		my_second = ((MyDate.getSeconds() < 10) ? '0' : '') + MyDate.getSeconds()
		
		window.returnValue = MyDate.getYear() + "-" +  my_month + "-" +  my_date + " " +  my_hour + ":" +  my_minute + ":" +  my_second
	    window.close();
}
</script>
</head>
<body>
    <form id="form1" runat="server">
<script language="javascript" type="text/javascript">
//	Written by cloudchen, 2004/03/15
function minute(name,fName)
{
	this.name = name;
	this.fName = fName || "m_input";
	this.timer = null;
	this.fObj = null;
	
	this.toString = function()
	{
		var objDate = new Date();
		var sMinute_Common = "class=\"m_input\" maxlength=\"2\" name=\""+this.fName+"\" onfocus=\""+this.name+".setFocusObj(this)\" onblur=\""+this.name+".setTime(this)\" onkeyup=\""+this.name+".prevent(this)\" onkeypress=\"if (!/[0-9]/.test(String.fromCharCode(event.keyCode)))event.keyCode=0\" onpaste=\"return false\" ondragenter=\"return false\"";
		var sButton_Common = "class=\"m_arrow\" onfocus=\"this.blur()\" onmouseup=\""+this.name+".controlTime()\" disabled"
		var str = "";
		str += "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">"
		str += "<tr>"
		str += "<td>"
		str += "<div class=\"m_frameborder\">"
		str += "<input radix=\"24\" value=\""+this.formatTime(objDate.getHours())+"\" "+sMinute_Common+">:"
		str += "<input radix=\"60\" value=\""+this.formatTime(objDate.getMinutes())+"\" "+sMinute_Common+">:"
		str += "<input radix=\"60\" value=\""+this.formatTime(objDate.getSeconds())+"\" "+sMinute_Common+">"
		str += "</div>"
		str += "</td>"
		str += "<td>"
		str += "<table border=\"0\" cellspacing=\"2\" cellpadding=\"0\">"
		str += "<tr><td><button id=\""+this.fName+"_up\" "+sButton_Common+">5</button></td></tr>"
		str += "<tr><td><button id=\""+this.fName+"_down\" "+sButton_Common+">6</button></td></tr>"
		str += "</table>"
		str += "</td>"
		str += "</tr>"
		str += "</table>"
		return str;
	}
	this.play = function()
	{
		this.timer = setInterval(this.name+".playback()",1000);
	}
	this.formatTime = function(sTime)
	{
		sTime = ("0"+sTime);
		return sTime.substr(sTime.length-2);
	}
	this.playback = function()
	{
		var objDate = new Date();
		var arrDate = [objDate.getHours(),objDate.getMinutes(),objDate.getSeconds()];
		var objMinute = document.getElementsByName(this.fName);
		for (var i=0;i<objMinute.length;i++)
		{
			objMinute[i].value = this.formatTime(arrDate[i])
		}
	}
	this.prevent = function(obj)
	{
		clearInterval(this.timer);
		this.setFocusObj(obj);
		var value = parseInt(obj.value,10);
		var radix = parseInt(obj.radix,10)-1;
		if (obj.value>radix||obj.value<0)
		{
			obj.value = obj.value.substr(0,1);
		}
	}
	this.controlTime = function(cmd)
	{
		event.cancelBubble = true;
		if (!this.fObj) return;
		clearInterval(this.timer);
		var cmd = event.srcElement.innerText=="5"?true:false;
		var i = parseInt(this.fObj.value,10);
		var radix = parseInt(this.fObj.radix,10)-1;
		if (i==radix&&cmd)
		{
			i = 0;
		}
		else if (i==0&&!cmd)
		{
			i = radix;
		}
		else
		{
			cmd?i++:i--;
		}
		this.fObj.value = this.formatTime(i);
		this.fObj.select();
	}
	this.setTime = function(obj)
	{
		obj.value = this.formatTime(obj.value);
	}
	this.setFocusObj = function(obj)
	{
		eval(this.fName+"_up").disabled = eval(this.fName+"_down").disabled = false;
		this.fObj = obj;
	}
	this.getTime = function()
	{
		var arrTime = new Array(2);
		for (var i=0;i<document.getElementsByName(this.fName).length;i++)
		{
			arrTime[i] = document.getElementsByName(this.fName)[i].value;
		}
		return arrTime.join(":");
	}
}
//	Written by cloudchen, 2004/03/16
function calendar(name,fName)
{
	this.name = name;
	this.fName = fName || "calendar";
	this.year = new Date().getFullYear();
	this.month = new Date().getMonth();
	this.date = new Date().getDate();
	//private
	this.toString = function()
	{
		var str = "";
		str += "<table border=\"0\" cellspacing=\"3\" cellpadding=\"0\" onselectstart=\"return false\">";
		str += "<tr>";
		str += "<td>";
		str += this.drawMonth();
		str += "</td>";
		str += "<td align=\"right\">";
		str += this.drawYear();
		str += "</td>";
		str += "</tr>";
		str += "<tr>";
		str += "<td colspan=\"2\">";
		str += "<div class=\"c_frameborder\">";
		str += "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"c_dateHead\">";
		str += "<tr>";
		str += "<td>日</td><td>一</td><td>二</td><td>三</td><td>四</td><td>五</td><td>六</td>";
		str += "</tr>";
		str += "</table>";
		str += this.drawDate();
		str += "</div>";
		str += "</td>";
		str += "</tr>";
		str += "</table>";
		return str;
	}
	//private
	this.drawYear = function()
	{
		var str = "";
		str += "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
		str += "<tr>";
		str += "<td>";
		str += "<input class=\"c_year\" maxlength=\"4\" value=\""+this.year+"\" name=\""+this.fName+"\" id=\""+this.fName+"_year\" readonly>";
		//DateField
		str += "<input type=\"hidden\" name=\""+this.fName+"\" value=\""+this.date+"\" id=\""+this.fName+"_date\">";
		str += "</td>";
		str += "<td>";
		str += "<table cellspacing=\"2\" cellpadding=\"0\" border=\"0\">";
		str += "<tr>";
		str += "<td><button class=\"c_arrow\" onfocus=\"this.blur()\" onclick=\"event.cancelBubble=true;document.getElementById('"+this.fName+"_year').value++;"+this.name+".redrawDate()\">5</button></td>";
		str += "</tr>";
		str += "<tr>";
		str += "<td><button class=\"c_arrow\" onfocus=\"this.blur()\" onclick=\"event.cancelBubble=true;document.getElementById('"+this.fName+"_year').value--;"+this.name+".redrawDate()\">6</button></td>";
		str += "</tr>";
		str += "</table>";
		str += "</td>";
		str += "</tr>";
		str += "</table>";
		return str;
	}
	//priavate
	this.drawMonth = function()
	{
		var aMonthName = ["一","二","三","四","五","六","七","八","九","十","十一","十二"];
		var str = "";
		str += "<select class=\"c_month\" name=\""+this.fName+"\" id=\""+this.fName+"_month\" onchange=\""+this.name+".redrawDate()\">";
		for (var i=0;i<aMonthName.length;i++) {
			str += "<option value=\""+(i+1)+"\" "+(i==this.month?"selected":"")+">"+aMonthName[i]+"月</option>";
		}
		str += "</select>";
		return str;
	}
	//private
	this.drawDate = function()
	{
		var str = "";
		var fDay = new Date(this.year,this.month,1).getDay();
		var fDate = 1-fDay;
		var lDay = new Date(this.year,this.month+1,0).getDay();
		var lDate = new Date(this.year,this.month+1,0).getDate();
		str += "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" id=\""+this.fName+"_dateTable"+"\">";
		for (var i=1,j=fDate;i<7;i++)
		{
			str += "<tr>";
			for (var k=0;k<7;k++)
			{
				str += "<td><span"+(j==this.date?" class=\"selected\"":"")+" onclick=\""+this.name+".redrawDate(this.innerText)\">"+(isDate(j++))+"</span></td>";
			}
			str += "</tr>";
		}
		str += "</table>";
		return str;

		function isDate(n)
		{
			return (n>=1&&n<=lDate)?n:"";
		}
	}
	//public
	this.redrawDate = function(d)
	{
		this.year = document.getElementById(this.fName+"_year").value;
		this.month = document.getElementById(this.fName+"_month").value-1;
		this.date = d || this.date;
		document.getElementById(this.fName+"_year").value = this.year;
		document.getElementById(this.fName+"_month").selectedIndex = this.month;
		document.getElementById(this.fName+"_date").value = this.date;
		if (this.date>new Date(this.year,this.month+1,0).getDate()) this.date = new Date(this.year,this.month+1,0).getDate();
		document.getElementById(this.fName+"_dateTable").outerHTML = this.drawDate();
	}
	//public
	this.getDate = function(delimiter)
	{
		if (!delimiter) delimiter = "/";
		var aValue = [this.year,(this.month+1),this.date];
		return aValue.join(delimiter);
	}
}
</script>
    </form>
    <table border="0" width="100%">
  <tr> 
    <td width="27%" rowspan="4"> <fieldset class="c_fieldset">
      <legend class="c_legend">日期(Date)</legend>
      <!-- 调用日历 -->
      <script>
			var c = new calendar("c");
			document.write(c);
			</script>
      <!-- 调用日历 -->
      </fieldset></td>
    <td width="9%" rowspan="5">&nbsp; </td>
    <td width="64%" height="46">&nbsp; </td>
  </tr>
  <tr> 
    <td><input type="button" name="ok" value=" 确 定 " onclick="ok_choose()"/></td>
  </tr>
  <tr>
    <td><input type="button" name="cur" value=" 现 在 " onclick="ok_now()"/></td>
  </tr>
  <tr> 
    <td><input type="button" name="cancel" value=" 取 消 " onclick="window.close()"/></td>
  </tr>
  <tr> 
    <td width="27%"> <fieldset class="m_fieldset">
      <legend class="m_legend">时间(Time)</legend>
      <!-- 调用时间钟 -->
      <script type="text/javascript">
			var m = new minute("m");
			m.play();
			document.write(m);
			</script>
      <!-- 调用时间钟 -->
      </fieldset></td>
    <td width="64%">&nbsp;</td>
  </tr>
</table>
</body>
</html>
