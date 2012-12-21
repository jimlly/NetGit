<%@ Page Language="C#" AutoEventWireup="true" Inherits="SendMsg_SendSms" CodeBehind="SendSms.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../Images/content.css" rel="stylesheet" type="text/css" />
    <script defer="defer" src="../JS/SendSms.js" type="text/javascript" language="javascript"></script>
    <script defer="defer" src="../JS/json.js" type="text/javascript" language="javascript"></script>
    <script defer="defer" src="../JS/AddressBook.js" type="text/javascript" language="javascript"></script>
    <script defer="defer" src="../JS/My97DatePicker/WdatePicker.js" type="text/javascript"
        language="javascript"></script>
    <script src="../JS/maxlength.js" type="text/javascript"></script>
    <script src="../JS/ymPrompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../JS/ymPrompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
</head>
<script language="javascript" type="text/javascript">
    //屏蔽js错误
    function killerrors() {
        return true;
    }
    window.onerror = killerrors;
</script>
<script type="text/javascript">
    function login() {
        var width = 670;
        var height = 580;
        var style = 'status:no;menu:no;help:0;dialogWidth:' + width + 'px ; dialogHeight:' + height + 'px';
        var val = window.showModalDialog('addressbook.aspx', window, style);

        if (typeof (val) == "undefined" || val == "undefined") {

        }
        else {
            var obj = eval(val);
            var PhoneNo = "", Property = "", Name = "", str = "";
            for (var i = 0; i < obj.length; i++) {
                if (obj[i].PhoneNo != "") {
                    if (obj[i].Property == "2") {
                        str += obj[i].PhoneNo + "," + obj[i].Name + ";\r\n";
                    }
                }
                else {
                    continue;
                }
            }
            var val1 = document.getElementById("txtMobileNums").value;
            if (val1.length > 0) {
                if (val1.endsWith(";")) {
                    val1 += "\r\n";
                }
                else if (val1.endsWith(";\r\n"))
                { }
                else {
                    val1 += ";\r\n";
                }
            }
            document.getElementById("txtMobileNums").value = val1 + str;
        }
    }
    function SetCharCount() {
        document.getElementById("charCount").innerHTML = SmsAjax.GetCharCount().value;
    }

    function get(textarea) {
        var rangeData = { text: "", start: 0, end: 0 };

        if (textarea.setSelectionRange) { // W3C	
            textarea.focus();
            rangeData.start = textarea.selectionStart;
            rangeData.end = textarea.selectionEnd;
            rangeData.text = (rangeData.start != rangeData.end) ? textarea.value.substring(rangeData.start, rangeData.end) : "";
        } else if (document.selection) { // IE
            textarea.focus();
            var i,
				oS = document.selection.createRange(),
            // Don't: oR = textarea.createTextRange()
				oR = document.body.createTextRange();
            oR.moveToElementText(textarea);

            rangeData.text = oS.text;
            rangeData.bookmark = oS.getBookmark();

            // object.moveStart(sUnit [, iCount]) 
            // Return Value: Integer that returns the number of units moved.
            for (i = 0; oR.compareEndPoints('StartToStart', oS) < 0 && oS.moveStart("character", -1) !== 0; i++) {
                // Why? You can alert(textarea.value.length)
                if (textarea.value.charAt(i) == '\r') {
                    i++;
                }
            }
            rangeData.start = i;
            rangeData.end = rangeData.text.length + rangeData.start;
        }

        return rangeData;
    }

    function set(textarea, rangeData) {
        var oR, start, end;
        if (!rangeData) {
            alert("You must get cursor position first.")
        }
        textarea.focus();
        if (textarea.setSelectionRange) { // W3C
            textarea.setSelectionRange(rangeData.start, rangeData.end);
        } else if (textarea.createTextRange) { // IE
            oR = textarea.createTextRange();

            // Fixbug : ues moveToBookmark()
            // In IE, if cursor position at the end of textarea, the set function don't work
            if (textarea.value.length === rangeData.start) {
                //alert('hello')
                oR.collapse(false);
                oR.select();
            } else {
                oR.moveToBookmark(rangeData.bookmark);
                oR.select();
            }
        }
    }

    function add(textarea, rangeData, text) {
        var oValue, nValue, oR, sR, nStart, nEnd, st;
        this.set(textarea, rangeData);

        if (textarea.setSelectionRange) { // W3C
            oValue = textarea.value;
            nValue = oValue.substring(0, rangeData.start) + text + oValue.substring(rangeData.end);
            nStart = nEnd = rangeData.start + text.length;
            st = textarea.scrollTop;
            textarea.value = nValue;
            // Fixbug:
            // After textarea.values = nValue, scrollTop value to 0
            if (textarea.scrollTop != st) {
                textarea.scrollTop = st;
            }
            textarea.setSelectionRange(nStart, nEnd);
        } else if (textarea.createTextRange) { // IE
            sR = document.selection.createRange();
            sR.text = text;
            sR.setEndPoint('StartToEnd', sR);
            sR.select();
        }
    }
    var tx,pos;
    function openPhrasePanel() {
        var targetPage = "";
        var pageTitle = "";
        tx = document.getElementById("txtContent");
        pos = get(tx);
        this.ymPrompt.win({ message: "../Settings/PhraseSelect.aspx", handler: callback, width: 380, height: 260, title: "常用短语", maxBtn: false, minBtn: false, iframe: true, useSlide: false, winAlpha: 1.0 });
    }

    function callback(tp) {
        if (tp!="close") {
            set(tx, pos);
            add(tx, pos, tp);
            textCounter();
        }
    }
</script>
<body onload="SetCharCount();">
    <form id="SendSms" method="post" enctype="multipart/form-data" runat="server">
    <div>
        <table width="760">
            <!--按手机号码发送-->
            <tr id="SingleSend">
                <td align="right" valign="top" style="width: 84px">
                    手机号码：
                </td>
                <td align="left" style="height: 29px">
                    <table style="width: 560px; height: 115px">
                        <tr>
                            <td colspan="2" rowspan="3" style="width: 283px">
                                <asp:TextBox ID="txtMobileNums" runat="server" TextMode="MultiLine" Width="420px"
                                    Height="113px"></asp:TextBox>
                            </td>
                            <td style="width: 334px; height: 14px;">
                            </td>
                        </tr>
                        <tr valign="middle">
                            <td style="width: 334px; height: 39px;">
                                <%--<span id="AddrImport" style="cursor: pointer;" onclick="login()">
                                    <img src="../Images/noteinto.gif" alt="" width="16" height="14" border="0" />
                                    从通讯录导入号码</span><br />--%>
                                <br />
                                <span id="FileImport" style="cursor: pointer" onclick="ShowFileDialog()">
                                    <img src="../Images/fileinto.gif" alt="" width="16" height="14" border="0" />
                                    从文件导入号码</span>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 334px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <font style="color: Red">注：输入格式为“手机号码,姓名”，多个接收人之间必须用分号隔开，例如：“号码1,姓名1;号码2,姓名2”。标点符号均为英文格式，“,
                                    姓名”为可选项，可不输入！ </font>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top" style="width: 84px">
                    短信内容：
                </td>
                <td style="width: 296px">
                    <table style="width: 560px; height: 15px" border="0">
                        <tr>
                            <%--<asp:TextBox ID="txtContent" runat="server" Height="104px" TextMode="MultiLine" Width="480px"
                                MaxLength="480"></asp:TextBox>--%>
                            <td>
                                <textarea runat="server" style="width: 420px; height: 104px; word-break: break-all"
                                    id="txtContent" maxlength="480"></textarea>
                            </td>
                            <td style="width: 334px;">
                                <span style="cursor: pointer" onclick="javascript:openPhrasePanel();">
                                    <img src="../Images/phrase.gif" alt="" height="14" border="0" />
                                    插入短语</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlSignatrue" runat="server" Width="154px">
                                </asp:DropDownList>
                                &nbsp;短信内容最多输入480个字符,&nbsp; 还可以输入&nbsp; <span id="lblCount" style="color: Red;">480</span>&nbsp;个字符
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" valign="middle" style="width: 84px">
                    <%--<asp:CheckBox ID="chkFixedTime" runat="server" Text="定时发送" />--%>
                    定时发送：
                </td>
                <td align="left">
                    <%--<asp:TextBox ID="txtSendTime" runat="server" Width="225px">定时发送</asp:TextBox>(设置时间不能比当前时间早，不能大于1个月)&nbsp;--%>
                    <input type="text" id="txtSendTime" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                        class="Wdate" style="width: 150px" />(设置时间不能比当前时间早，不能大于1个月)
                </td>
            </tr>
            <tr>
                <td align="right" valign="middle" style="width: 84px">
                    <input type="checkbox" id="chkAgree" onclick="IsAgree()" checked="checked" />
                </td>
                <td align="left">
                    <asp:HyperLink ID="hnkInfo" runat="server" NavigateUrl="../SmsInfo.htm" Target="_blank">同意SP信息源安全责任协议</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td align="right" valign="middle" style="width: 84px">
                </td>
                <td align="left">
                    <asp:Button ID="btnSend" runat="server" Text="发送" OnClick="btnSend_Click" OnClientClick="return CheckInput();" />
                </td>
            </tr>
            <tr>
                <td align="right" valign="middle" style="width: 84px">
                    说明：
                </td>
                <td align="left">
                    1、如果您的短信内容超过<span id="charCount"></span>个字符,我们的系统将自动切成多条发送<br />
                    2、请不要发布非法、色情、恐怖、教唆犯罪、诽谤他人、危害国家安全及SP业务宣传广告的信息。否则，由此产生的一切后果由发送当事人承担.此账户的金额我司将会无条件取消，账户将无条件的永久冻结。
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfSendType" runat="server" Value="11" />
        <asp:HiddenField ID="hfAddressBook" runat="server" Value="" />
        <asp:HiddenField ID="hfNumber" runat="server" />
        <asp:HiddenField ID="hfFile" runat="server" Value="" />
        <asp:HiddenField ID="hfBlackDic" runat="server" />
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    //当状态改变的时候执行的函数
    function handle() {
        //document.getElementById('msg').innerHTML='输入的文字长度为：'+document.getElementById('txt').value.length;
    }
    //firefox下检测状态改变只能用oninput,且需要用addEventListener来注册事件。
    if (/msie/i.test(navigator.userAgent))    //ie浏览器
    {
        document.getElementById('<%=txtContent.ClientID %>').onpropertychange = handle
    } else {//非ie浏览器，比如Firefox
        document.getElementById('<%=txtContent.ClientID %>').addEventListener("input", handle, false);
    }
</script>
<script type="text/javascript"> 
<!--
    function getOs() {//判断浏览器类型 
        var OsObject = "";
        if (navigator.userAgent.indexOf("MSIE") > 0) {
            return "MSIE";
        }
        if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
            return "Firefox";
        }
        if (isSafari = navigator.userAgent.indexOf("Safari") > 0) {
            return "Safari";
        }
        if (isCamino = navigator.userAgent.indexOf("Camino") > 0) {
            return "Camino";
        }
        if (isMozilla = navigator.userAgent.indexOf("Gecko/") > 0) {
            return "Gecko";
        }

    }

    if (navigator.userAgent.indexOf("MSIE") > 0) {
        document.getElementById('<%=txtContent.ClientID %>').attachEvent("onpropertychange", textCounter);
    } else if (navigator.userAgent.indexOf("Firefox") > 0) {
        document.getElementById('<%=txtContent.ClientID %>').addEventListener("input", textCounter, false);
    } else {
        document.getElementById('<%=txtContent.ClientID %>').addEventListener("input", textCounter, false);
    }
    function txChange() {
        alert("testie");
    }
    function txChange2() {
        alert("testfirefox");
    }

    function textCounter() {
        var text = document.getElementById('<%=txtContent.ClientID %>');
        var label = document.getElementById('lblCount');
        if (text.value.length <= 480) {
            label.innerHTML = 480 - text.value.length;
        }
        else {
            text.value = text.value.substring(0, 480);
        }
    }
</script>


