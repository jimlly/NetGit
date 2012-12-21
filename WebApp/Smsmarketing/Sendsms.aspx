<%@ Page Language="C#" AutoEventWireup="true" Inherits="Sms_SendSms" CodeBehind="SendSms.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>营销短信发送</title>
    <script defer="defer" src="../JS/SendSms.js" type="text/javascript" language="javascript"></script>
    <script defer="defer" src="../JS/My97DatePicker/WdatePicker.js" type="text/javascript"
        language="javascript"></script>
    <link href="../Images/content.css" rel="stylesheet" type="text/css" />
    <script src="../JS/maxlength.js" type="text/javascript"></script>
    <script src="../JS/ymPrompt/ymPrompt.js" type="text/javascript"></script>
    <link href="../JS/ymPrompt/skin/qq/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function in_array(stringToSearch, arrayToSearch) {
            for (s = 0; s < arrayToSearch.length; s++) {
                thisEntry = arrayToSearch[s].toString();
                if (thisEntry == stringToSearch) {
                    return true;
                }
            }
            return false;
        }
        function get_extname(s) {
            var s = new String(s)
            s = s.toLowerCase();
            if ((s.lastIndexOf(".") > -1) && ((s.length - 1) > s.lastIndexOf("."))) {
                return s.substring(s.lastIndexOf(".") + 1);
            }
            else {
                return false;
            }
        }

        function SelectFirst() {
            if (document.getElementById("rbArea").checked) {
                for (var i = 1; i <= 5; i++) {
                    document.getElementById("DDL_Provice" + i).value = "-1";
                    document.getElementById("DDL_City" + i).length = 0;
                    document.getElementById("DDL_City" + i).options.add(new Option("全部城市", "-1"));
                    document.getElementById("ddl_hangye" + i).value = "-1";
                    document.getElementById("totalamount" + i).innerHTML = "";
                }

                document.getElementById("AreaFirst").style.display = "";
                document.getElementById("IndustryFirst").style.display = "none";
            }
            else {
                for (var i = 1; i <= 5; i++) {
                    document.getElementById("ddlIndustry" + i).value = "-1";
                    document.getElementById("ddlProvince" + i).length = 0;
                    document.getElementById("ddlArea" + i).length = 0;
                    document.getElementById("ddlProvince" + i).options.add(new Option("全部省份", "-1"));
                    document.getElementById("ddlArea" + i).options.add(new Option("全部地区", "-1"));
                    document.getElementById("Amount" + i).innerHTML = "";
                }

                document.getElementById("AreaFirst").style.display = "none";
                document.getElementById("IndustryFirst").style.display = "";
            }
            document.getElementById("hd1").value = "";
            document.getElementById("hd2").value = "";
            document.getElementById("hd3").value = "";
            document.getElementById("hd4").value = "";
            document.getElementById("hd5").value = "";
            document.getElementById("Hidmg").value = "";
            document.getElementById("totalnum").innerHTML = "";
            document.getElementById("txtEnd").value = "";
            special1 = 0;
            special2 = 0;
            special3 = 0;
            special4 = 0;
            special5 = 0;
            specialall = 0;
            line1 = 0;
            line2 = 0;
            line3 = 0;
            line4 = 0;
            line5 = 0;
            allline = 0;

            return false;
        }

        //检查输入的数字是否符合要求
        function checkNum(obj, styleFlag) {
            var linenum = /^[0]\d{0,20}$/;
            var extention = /^\d{0,6}$/;
            var resend = /^\d{1}$/;
            var mobile = /^[1]\d{0,11}$/;
            var expression = /[^\d]/;
            if (event.keyCode != 8) {
                switch (styleFlag) {
                    case 1:
                        expression = linenum;
                        break;
                    case 2:
                        expression = extention;
                        break;
                    case 3:
                        expression = resend;
                        break;
                    case 4:
                        expression = mobile;
                        break;
                    default:
                        expression = expression;
                        break;
                }
                if (!expression.test(obj.value + String.fromCharCode(event.keyCode))) {
                    return false;
                }
            }
        }
        function specialCharCheck(str) {
            //要屏蔽掉的特殊字符
            var specialCharacter = "~!@#$%^&*()><\/'|";
            var i;
            for (i = 0; i < specialCharacter.length; i++) {
                if (str.indexOf(specialCharacter.charAt(i)) >= 0) {
                    return true;
                }
            }
        }
        //验证
        function CheckForm() {
            var txtStart = document.getElementById("txtStart").value;
            var txtEnd = document.getElementById("txtEnd").value;
            if (txtStart == '' || txtEnd == '') {
                alert("请填写号码起始数!");
                return false;
            }
            if (!/^[1-9]{1}[0-9]{0,}$/.test(txtStart)) {
                alert("起始行数应为数字且大于0!");
                return false;
            }
            if (!/^[1-9]{1}[0-9]{0,}$/.test(txtEnd)) {
                alert("结束行数应为数字且大于0!");
                return false;
            }
            if (parseInt(txtStart) > parseInt(txtEnd)) {
                alert("结束数字必须大于开始数字!");
                return false;
            }

            if (document.getElementById("rbArea").checked) {
                if (parseInt(txtStart) > allline || parseInt(txtEnd) > allline) {
                    alert("不能超过现有号码数量!");
                    return false;
                }
            }
            else {
                if (parseInt(txtStart) > specialall || parseInt(txtEnd) > specialall) {
                    alert("不能超过现有号码数量!");
                    return false;
                }
            }

            var content = document.getElementById("txtContent").value;
            if (content == "") {
                alert('请填写短信内容');
                return false;
            }
            else {
                var ddl = document.getElementById("ddlSignatrue");
                if (ddl.options[ddl.selectedIndex].value != "0") {
                    content += "【" + ddl.options[ddl.selectedIndex].value + "】";
                }
                if (content.length > 480) {
                    alert('短信内容和签名总长度超出最大字符限制！');
                    return false;
                }
                else if (document.getElementById("hfBlackDic").value == "1") {
                    if (BlackDictionary.Parse(content).value.length > 0) {
                        alert('短信内容包含禁用关键字！');
                        return false;
                    }
                }
                //                }
            }
        }

        //得到指定字符串的长度
        function GetLength(str) {
            return str.replace(/[^\x00-\xff]/g, "**").length;
        }

        //------------------------------------new add begin----------------------------------------
        var cityname = '';
        var spanname = '';
        var hebinjieguo = "";
        var spanhangye = '';
        ddlhangyeid = "";
        ddlcityid = "";
        ddlprovinceid = "";
        //保存每行选择的号码数量
        var line1 = 0
        var line2 = 0
        var line3 = 0
        var line4 = 0
        var line5 = 0
        var allline = 0

        //绑定城市
        function cityResult(ddlprovince, ddlcitynum) {
            cityname = "DDL_City" + ddlcitynum
            spanname = "shownum" + ddlcitynum;
            hebinjieguo = "totalamount" + ddlcitynum;
            ddlhangyeid = 'ddl_hangye' + ddlcitynum;
            ddlcityid = "DDL_City" + ddlcitynum;
            ddlprovinceid = "DDL_Provice" + ddlcitynum;
            var hid = document.getElementById("hd" + ddlcitynum);
            var res = AjaxCityMethod.NewGetCityList(ddlprovince.value);

            if (res.value != null) {
                document.all(cityname).length = 0;
                var ds = res.value;
                document.all(cityname).options.add(new Option("全部城市", "-1"))
                if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
                    for (var i = 0; i < ds.Tables[0].Rows.length; i++) {
                        var name = ds.Tables[0].Rows[i].AreaName;
                        var id = ds.Tables[0].Rows[i].AreaID;
                        document.all(cityname).options.add(new Option(name, id));
                    }
                }
            }
            if (ddlprovince.value != "-1") {
                hid.value = ddlprovince.value + '|' + document.getElementById(ddlcityid).value + '|' + document.getElementById(ddlhangyeid).value;
                var res1 = AjaxCityMethod.NewGetNumAmout(ddlprovince.value + '|' + document.getElementById(ddlcityid).value + '|' + document.getElementById(ddlhangyeid).value);
                if (res1.value != null) {
                    document.getElementById(hebinjieguo).innerHTML = "共有<span style='color:Red;font-weight: bold;'>" + res1.value + "</span>个号码";
                    eval("line" + ddlcitynum + "=" + res1.value);
                    allline = countnumber();
                }
            }
            else {
                hid.value = "";
                document.getElementById(hebinjieguo).innerHTML = "";
                eval("line" + ddlcitynum + "=0");
                allline = countnumber();
            }
            return
        }
        //显示城市号码数
        function GetCityNumCount(ddlcity, spancitynumber) {
            spanname = "shownum" + spancitynumber;
            hebinjieguo = "totalamount" + spancitynumber;
            ddlhangyeid = 'ddl_hangye' + spancitynumber;
            ddlcityid = "DDL_City" + spancitynumber;
            ddlprovinceid = "DDL_Provice" + spancitynumber;
            ddlprovince = document.getElementById(ddlprovinceid);
            var hid = document.getElementById("hd" + spancitynumber);
            if (document.getElementById(ddlprovinceid).value != "-1") {
                hid.value = ddlprovince.value + '|' + document.getElementById(ddlcityid).value + '|' + document.getElementById(ddlhangyeid).value;
                var res1 = AjaxCityMethod.NewGetNumAmout(document.getElementById(ddlprovinceid).value + '|' + document.getElementById(ddlcityid).value + '|' + document.getElementById(ddlhangyeid).value);
                if (res1.value != null) {
                    document.getElementById(hebinjieguo).innerHTML = "共有<span style='color:Red;font-weight: bold;'>" + res1.value + "</span>个号码";
                    eval("line" + spancitynumber + "=" + res1.value);
                    allline = countnumber();
                }
                else {
                    hid.value = "";
                    document.getElementById(hebinjieguo).innerHTML = "";
                    eval("line" + spancitynumber + "=0");
                    allline = countnumber();
                }
            }
        }
        //行业
        function GethangyeNumCount(ddlhangye, spanhangyenum) {
            spanhangye = 'hynumber' + spanhangyenum;
            hebinjieguo = "totalamount" + spanhangyenum;
            ddlcityid = 'DDL_City' + spanhangyenum;
            ddlhangyeid = 'ddl_hangye' + spanhangyenum;
            ddlprovinceid = "DDL_Provice" + spanhangyenum;
            ddlprovince = document.getElementById(ddlprovinceid);
            var hid = document.getElementById("hd" + spanhangyenum);
            var ddlcityvalue = document.getElementById(ddlcityid).value;
            if (ddlcityvalue == '') {
                ddlcityvalue = 0;
            }
            if (document.getElementById(ddlprovinceid).value != "-1") {
                hid.value = ddlprovince.value + '|' + document.getElementById(ddlcityid).value + '|' + document.getElementById(ddlhangyeid).value;
                var res1 = AjaxCityMethod.NewGetNumAmout(document.getElementById(ddlprovinceid).value + '|' + ddlcityvalue + '|' + document.getElementById(ddlhangyeid).value);
                if (res1.value != null) {
                    document.getElementById(hebinjieguo).innerHTML = "共有<span style='color:Red;font-weight: bold;'>" + res1.value + "</span>个号码";
                    eval("line" + spanhangyenum + "=" + res1.value);
                    allline = countnumber();
                }
            }
            else {
                hid.value = "";
                document.getElementById(hebinjieguo).innerHTML = "";
                eval("line" + spanhangyenum + "=0");
                allline = countnumber();
            }

        }
        function countnumber() {
            var total = 0;
            total = line1 + line2 + line3 + line4 + line5;
            document.getElementById('totalnum').innerHTML = total;
            document.getElementById("txtEnd").value = total;
            return total;
        }

        //======================Add By Zhuxiang 2009-08-07 (BEGIN)=====================================

        //根据所选行业关联出有号码的省市
        function GetProvinceByIndustry(ddlIndustry, index) {
            Province = "ddlProvince" + index
            Area = "ddlArea" + index;
            hebinjieguo = "Amount" + index;
            var hid = document.getElementById("hd" + index);
            var res = AjaxCityMethod.GetProvinceListByIndustry(ddlIndustry.value);

            if (res.value != null) {
                document.all(Province).length = 0;
                var ds = res.value;
                document.all(Province).options.add(new Option("全部省份", "-1"))
                if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
                    for (var i = 0; i < ds.Tables[0].Rows.length; i++) {
                        var name = ds.Tables[0].Rows[i].AreaName;
                        var id = ds.Tables[0].Rows[i].AreaID;
                        document.all(Province).options.add(new Option(name, id));
                    }
                }
                document.all(Area).length = 0;
                document.all(Area).options.add(new Option("全部地区", "-1"))
            }
            if (ddlIndustry.value != "-1") {
                hid.value = document.getElementById(Province).value + '|' + document.getElementById(Area).value + '|' + ddlIndustry.value;
                var res1 = AjaxCityMethod.GetNumberAmount(ddlIndustry.value, document.getElementById(Province).value, document.getElementById(Area).value);
                if (res1.value != null) {
                    document.getElementById(hebinjieguo).innerHTML = "共有<span style='color:Red;font-weight: bold;'>" + res1.value + "</span>个号码";
                    eval("special" + index + "=" + res1.value);
                    specialall = NumberAmount();
                }
            }
            else {
                hid.value = "";
                document.getElementById(hebinjieguo).innerHTML = "";
                eval("special" + index + "=0");
                specialall = NumberAmount();
            }
            return
        }

        //根据所选行业、省市关联出有号码的地区
        function GetAreaByProvinceAndIndustry(ddlIndustry, ddlProvince, index) {
            Area = "ddlArea" + index
            hebinjieguo = "Amount" + index;
            var hid = document.getElementById("hd" + index);
            var res = AjaxCityMethod.GetAreaListByProvinceAndIndustry(ddlIndustry.value, ddlProvince.value);

            if (res.value != null) {
                document.all(Area).length = 0;
                var ds = res.value;
                document.all(Area).options.add(new Option("全部地区", "-1"))
                if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
                    for (var i = 0; i < ds.Tables[0].Rows.length; i++) {
                        var name = ds.Tables[0].Rows[i].AreaName;
                        var id = ds.Tables[0].Rows[i].AreaID;
                        document.all(Area).options.add(new Option(name, id));
                    }
                }
            }
            if (ddlProvince.value != "-1") {
                hid.value = ddlProvince.value + '|' + document.getElementById(Area).value + '|' + ddlIndustry.value;
                var res1 = AjaxCityMethod.GetNumberAmount(ddlIndustry.value, ddlProvince.value, document.getElementById(Area).value);
                if (res1.value != null) {
                    document.getElementById(hebinjieguo).innerHTML = "共有<span style='color:Red;font-weight: bold;'>" + res1.value + "</span>个号码";
                    eval("special" + index + "=" + res1.value);
                    specialall = NumberAmount();
                }
            }
            else {
                hid.value = "";
                document.getElementById(hebinjieguo).innerHTML = "";
                eval("special" + index + "=0");
                specialall = NumberAmount();
            }
            return
        }

        //根据所选行业地区显示号码数
        function GetNumberAmountByIPA(ddlIndustry, ddlProvince, ddlArea, index) {
            hebinjieguo = "Amount" + index;
            var hid = document.getElementById("hd" + index);
            if (ddlArea.value != "-1") {
                hid.value = ddlProvince.value + '|' + ddlArea.value + '|' + ddlIndustry.value;
                var res1 = AjaxCityMethod.GetNumberAmount(ddlIndustry.value, ddlProvince.value, ddlArea.value);
                if (res1.value != null) {
                    document.getElementById(hebinjieguo).innerHTML = "共有<span style='color:Red;font-weight: bold;'>" + res1.value + "</span>个号码";
                    eval("special" + index + "=" + res1.value);
                    specialall = NumberAmount();
                }
                else {
                    hid.value = "";
                    document.getElementById(hebinjieguo).innerHTML = "";
                    eval("special" + index + "=0");
                    specialall = NumberAmount();
                }
            }
        }

        //特殊行业的号码数量
        var special1 = 0
        var special2 = 0
        var special3 = 0
        var special4 = 0
        var special5 = 0
        var specialall = 0

        function NumberAmount() {
            var total = 0;
            total = special1 + special2 + special3 + special4 + special5;
            document.getElementById('totalnum').innerHTML = total;
            document.getElementById("txtEnd").value = total;
            return total;
        }
        //======================Add By Zhuxiang 2009-08-07 (END)=======================================
        //------------------------------------new add end   ----------------------------------------
        //发送前检查
        function checkGetValue() {
            var sum = 0;
            //            var checkagree = document.getElementById("Checkagree");
            //var sendtime = document.getElementById("chkTime");
            var txttime = document.getElementById("txtSendTime");
            var endnum = document.getElementById("txtEnd");
            var startnum = document.getElementById("txtStart");

            //            if (checkagree.checked == false) {
            //                alert("对不起,您不同意SP信息源安全责任协议,无法发送!");
            //                return false;
            //            }

            if (document.getElementById("rbArea").checked) {
                if (allline == 0) {
                    alert('你选择的组合没有号码');
                    return false;
                }
            }
            else {
                if (specialall == 0) {
                    alert('你选择的组合没有号码');
                    return false;
                }
            }

            if (CheckForm() == false) {
                return false;
            }

            var content = document.getElementById("txtContent").value;
            if (content == "") {
                alert('请填写短信内容');
                return false;
            }
            else {
                var ddl = document.getElementById("ddlSignatrue");
                if (ddl.options[ddl.selectedIndex].value != "0") {
                    content += "【" + ddl.options[ddl.selectedIndex].value + "】";
                }
                if (content.length > 480) {
                    alert('短信内容和签名总长度超出最大字符限制！');
                    return false;
                }
            }
//            if (sendtime.checked == true) {
//                if (txttime.value == "" || txttime.value == "定时发送") {
//                    alert("请选择时间");
//                    return false;
//                }
//            }

            var str = ";";
            for (var i = 1; i < 6; i++) {
                var name = "hd" + i;
                if (document.getElementById(name).value != "" && document.getElementById(name).value != "undefined") {
                    var line = document.getElementById(name).value;
                    //                if( line.substring(0,2) != "-1")
                    //                {
                    str += document.getElementById(name).value;
                    str += ";"
                    //                } 
                }
            }
            if (str.length > 1) {
                str = str.substring(1, str.length - 1);
            }
            //拼接五组中选的
            document.getElementById("Hidmg").value = str;
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
        var tx, pos;
        function openPhrasePanel() {
            var targetPage = "";
            var pageTitle = "";
            tx = document.getElementById("txtContent");
            pos = get(tx);
            this.ymPrompt.win({ message: "../Settings/PhraseSelect.aspx", handler: callback, width: 380, height: 260, title: "常用短语", maxBtn: false, minBtn: false, iframe: true, useSlide: false, winAlpha: 1.0 });
        }

        function callback(tp) {
            if (tp != "close") {
                set(tx, pos);
                add(tx, pos, tp);
                textCounter();
            }
        }
    </script>
</head>
<body onload="SetCharCount();">
    <form id="upMoreFile" autocomplete="off" method="post" enctype="multipart/form-data"
    runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td style="width: 968px">
                <table width="600" border="0" align="left" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="1">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="102" align="left" valign="top" style="width: 968px">
                <table width="673" border="0" cellpadding="5" cellspacing="0">
                    <tr>
                        <td width="135" align="right" valign="bottom">
                            城市和行业选择：&nbsp;
                        </td>
                        <td width="518" align="left">
                            &nbsp;<asp:RadioButton ID="rbArea" runat="server" Text="按地区属性" Checked="true" GroupName="select"
                                onclick='javascript:SelectFirst();' />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="rbIndustry" runat="server" Text="按行业属性" GroupName="select" onclick='javascript:SelectFirst();' />
                            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp;
                            <asp:LinkButton ID="NumAreaLinkButton" runat="server" ForeColor="Red" OnClick="NumAreaLinkButton_Click"
                                Font-Size="Larger">号码分布图</asp:LinkButton>
                        </td>
                    </tr>
                    <tr id="AreaFirst">
                        <td align="right" valign="top">
                        </td>
                        <td align="left" valign="top">
                            <div id="hangyeandarea">
                                <table width="511" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="138" height="32" align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="DDL_Provice1" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="DDL_City1" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="shownum1" style="display: none; color: Red"></span>
                                        </td>
                                        <td width="138" align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddl_hangye1" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="hynumber1" style="display: none"></span>
                                        </td>
                                        <td width="97" align="center" bgcolor="#FFFFFF">
                                            <span id="totalamount1"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="32" align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="DDL_Provice2" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="DDL_City2" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="shownum2" style="display: none; color: Red"></span>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddl_hangye2" runat="server" Width="119px">
                                            </asp:DropDownList>
                                            <span id="hynumber2" style="display: none"></span>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <span id="totalamount2"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" bgcolor="#FFFFFF" style="height: 32px">
                                            <asp:DropDownList ID="DDL_Provice3" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF" style="height: 32px">
                                            <asp:DropDownList ID="DDL_City3" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="shownum3" style="display: none; color: Red"></span>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF" style="height: 32px">
                                            <asp:DropDownList ID="ddl_hangye3" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="hynumber3" style="display: none"></span>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF" style="height: 32px">
                                            <span id="totalamount3"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="32" align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="DDL_Provice4" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="DDL_City4" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="shownum4" style="display: none; color: Red"></span>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddl_hangye4" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="hynumber4" style="display: none"></span>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <span id="totalamount4"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="32" align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="DDL_Provice5" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="DDL_City5" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="shownum5" style="display: none; color: Red"></span>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddl_hangye5" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="hynumber5" style="display: none"></span>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <span id="totalamount5"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="width: 169px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 150px">
                                            &nbsp; &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr id="IndustryFirst" style="display: none;">
                        <td align="right" valign="top">
                        </td>
                        <td align="left" valign="top">
                            <div id="Div1">
                                <table width="511" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="138" height="32" align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddlIndustry1" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddlProvince1" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="ShowAmount1" style="display: none; color: Red"></span>
                                        </td>
                                        <td width="138" align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddlArea1" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="Span2" style="display: none"></span>
                                        </td>
                                        <td width="97" align="center" bgcolor="#FFFFFF">
                                            <span id="Amount1"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="32" align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddlIndustry2" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddlProvince2" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="Span4" style="display: none; color: Red"></span>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddlArea2" runat="server" Width="119px">
                                            </asp:DropDownList>
                                            <span id="Span5" style="display: none"></span>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <span id="Amount2"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="32" align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddlIndustry3" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddlProvince3" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="Span7" style="display: none; color: Red"></span>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddlArea3" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="Span8" style="display: none"></span>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <span id="Amount3"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="32" align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddlIndustry4" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddlProvince4" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="Span10" style="display: none; color: Red"></span>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddlArea4" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="Span11" style="display: none"></span>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <span id="Amount4"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="32" align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddlIndustry5" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddlProvince5" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="Span13" style="display: none; color: Red"></span>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <asp:DropDownList ID="ddlArea5" runat="server" Width="120px">
                                            </asp:DropDownList>
                                            <span id="Span14" style="display: none"></span>
                                        </td>
                                        <td align="center" bgcolor="#FFFFFF">
                                            <span id="Amount5"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="width: 169px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 150px">
                                            &nbsp; &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="bottom">
                            可发送的号码数量：
                        </td>
                        <td align="left">
                            <span id="totalnum" style="color: Red; font-weight: bold;"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="bottom">
                            发送号码范围：
                        </td>
                        <td align="left">
                            起始行数：
                            <script>
                                function vaildIntegerNumber(evnt) {
                                    evnt = evnt || window.event;
                                    var keyCode = window.event ? evnt.keyCode : evnt.which;
                                    return keyCode >= 48 && keyCode <= 57 || keyCode == 8;
                                }
                            </script>
                            <asp:TextBox ID="txtStart" runat="server" onkeypress="return vaildIntegerNumber(event);"
                                ondrag="return false" MaxLength="8" Style="ime-mode: Disabled;" onpaste="return false">1</asp:TextBox>
                            结束行数： &nbsp;
                            <asp:TextBox ID="txtEnd" runat="server" MaxLength="8" ondrag="return false" onkeypress="return vaildIntegerNumber(event);"
                                onpaste="return false" Style="ime-mode: Disabled;">1</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            短信内容：<asp:HiddenField ID="Hidmg" runat="server" />
                            <input id="hd5" name="hid" runat="server" type="hidden" style="width: 21px" /><input
                                id="hd4" name="hid" runat="server" type="hidden" style="width: 21px" /><input id="hd3"
                                    name="hid" runat="server" type="hidden" style="width: 23px" /><input id="hd2" name="hid"
                                        runat="server" type="hidden" style="width: 17px" /><input id="hd1" name="hid" runat="server"
                                            type="hidden" style="width: 19px" />
                        </td>
                        <td align="left">
                            <%--<asp:TextBox ID="txtContent" runat="server" Height="104px" MaxLength="200" TextMode="MultiLine"
                                    Width="445px" onpropertychange="textCounter();"></asp:TextBox><br />
                                短信内容最多输入200个字符,&nbsp; 还可以输入&nbsp;<asp:Label ID="lblCount" runat="server" Text="200"
                                    ForeColor="red"></asp:Label>&nbsp;个字符</td>
                                    <td style="width: 296px">--%>
                            <table style="width: 527px; height: 15px" border="0">
                                <tr>
                                    <%--<asp:TextBox ID="txtContent" runat="server" Height="104px" TextMode="MultiLine" Width="480px"
                                        MaxLength="480" ></asp:TextBox>--%>
                                    <td>
                                    <textarea runat="server" style="width: 420px; height: 104px;word-break:  break-all" id="txtContent" wrap="soft"
                                        maxlength="480"></textarea></td>
                                        <td style="width: 334px;">
                                <span style="cursor: pointer" onclick="javascript:openPhrasePanel();" >
                                    <img src="../Images/phrase.gif" alt="" height="14" border="0" />
                                    插入短语</span>
                            </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlSignatrue" runat="server" Width="154px">
                                        </asp:DropDownList>
                                        &nbsp;短信内容最多输入480个字符,&nbsp; 还可以输入&nbsp;<span id="lblCount" style="color: Red;">480</span>&nbsp;个字符
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <%--<input id="chkFixedTime" type="checkbox" name="chkFixedTime" checked="CHECKED" />--%>
                            <%--<asp:CheckBox ID="chkTime" Text="定时发送" runat="server" />--%>
                            定时发送：
                        </td>
                        <td align="left">
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
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSend" runat="server" OnClick="bt_Send_Click" Text="发送" OnClientClick="return checkGetValue();" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <strong>说明：</strong>
                        </td>
                        <td align="left">
                            <p>
                                1、如果您的短信内容超过<span id="charCount"></span>个字符,我们的系统将自动切成多条发送<br />
                                2、请不要发布非法、色情、恐怖、教唆犯罪、诽谤他人、危害国家安全及SP业务宣传广告 的信息。否则，由此产生的一切后果由发送当事人承担.此账户的金额我司将会无条件取消，账户将无条件的永久冻结。<br />
                            </p>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfBlackDic" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
<script language="JavaScript">
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
<script language="JavaScript"> 
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
