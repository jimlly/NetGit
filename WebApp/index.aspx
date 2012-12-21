<%@ Page Language="C#" AutoEventWireup="true" Inherits="index" Codebehind="index.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="JS/ymPrompt/ymPrompt.js"></script>
    <link href="JS/ymPrompt/skin/qq/ymPrompt.css"  rel="stylesheet"  type="text/css" />
    <title>短信营销</title>    
</head>
<%--<frameset rows="93,*,30" cols="*" framespacing="0" frameborder="no" border="0">
  <frame src="Frame/top.htm?<%=GetUrl()[0] %>" name="topFrame" scrolling="No" noresize="noresize" id="topFrame" />
  <frame src="Frame/middle.htm" name="mainFrame" id="mainFrame" />
  <frame src="Frame/down.htm?<%=GetUrl()[1] %>" name="bottomFrame" scrolling="No" noresize="noresize" id="bottomFrame" />
  <noframes>
    <body>您的浏览器无法处理框架！</body>
  </noframes>
</frameset>--%>
<body style="margin:0 0; height:540px">
<table style="width:100%" align="center" cellpadding="0" cellspacing="0">
<tr><td>
  <iframe src="Frame/top.htm?<%=GetUrl()[0] %>" frameborder="0" name="topFrame" width="99%" height="95px" scrolling="no"  id="topFrame" ></iframe>
  <iframe src="Frame/middle.htm" name="mainFrame" frameborder="0" id="mainFrame" width="99%" height="600px" scrolling="no"></iframe>
  <iframe src="Frame/down.htm?<%=GetUrl()[1] %>" frameborder="0" name="bottomFrame" width="99%" height="30px" scrolling="no" id="bottomFrame" ></iframe></td></tr></table>

</body>
</html>
