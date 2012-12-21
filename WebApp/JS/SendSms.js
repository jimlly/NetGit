// JScript 文件
//检查群发录文件格式
function CheckAddressFile(obj) {
    var allow_ext = new Array("txt", "xls", "csv");
    if (obj.value != "") {
        if ((in_array(get_extname(obj.value), allow_ext)) == true) {
            return true;
        }
        else {
            alert(get_extname(obj.value) + " 不支持该格式的群发录文件！");
            //清空file
            obj.outerHTML = "<input type=\"file\" name=\"" + obj.name + "\" onkeydown=\"javascript:this.blur();\" onpaste=\"return false\" onpropertychange=\"CheckAddressFile(this)\" />";
            return false;
        }
    }
}
//校验群发录文件后缀是否符合要求
function in_array(stringToSearch, arrayToSearch) {
    for (s = 0; s < arrayToSearch.length; s++) {
        thisEntry = arrayToSearch[s].toString();
        if (thisEntry == stringToSearch) {
            return true;
        }
    }
    return false;
}
//取得上传文件的后缀名
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
//判断上传文件是否为空
function CheckFileIsNull() {
    var fileCount = 0;
    with (document.forms[0]) {
        for (i = 0; i < length; i++) {
            if (elements[i].type == "file") {
                if (elements[i].value != "") {
                    fileCount++;
                    break;
                }
            }
        }
    }
    if (fileCount > 0) {
        return true;
    } else {
        alert('对不起，您还没有选择群发录文件');
        return false;
    }
}


//设定发送时间
function SetSendtime() {
    var returned_time = document.SendSms.txtSendTime.value; alert(returned_time);
    if (returned_time != null && returned_time != "") {
        document.SendSms.txtSendTime.value = returned_time;
        return false;
    }
    else {
        document.SendSms.txtSendTime.value = '定时发送';
        return false;
    }
}


function getvalue(obj) {
    if (obj.value != "") {
        var str = "";
        if (obj.value.length > 200) {
            str = obj.value.substring(0, 200) + '.';
        }
        else {
            str = obj.value;
        }
    }
    else {
        return false;
    }
}

//显示手机号码方式
function DisplaySingle() {
    document.getElementById("SingleSend").style.display = "";
    document.getElementById("MultiSend").style.display = "none";
    document.getElementById("Multi_Address").style.display = "none";
    document.getElementById("Multi_File").style.display = "none";

    if (document.getElementById("rdSingleImport").checked) {
        document.getElementById("Single_Import").style.display = "";
        document.getElementById("Single_Input").style.display = "none";
        document.getElementById("hfSendType").value = "11";
    }
    else {
        document.getElementById("Single_Import").style.display = "none";
        document.getElementById("Single_Input").style.display = "";
        document.getElementById("hfSendType").value = "12";
    }
    return false;
}

//显示群发导入
function DisplayMulti() {
    document.getElementById("MultiSend").style.display = "";
    document.getElementById("SingleSend").style.display = "none";
    document.getElementById("Single_Import").style.display = "none";
    document.getElementById("Single_Input").style.display = "none";

    if (document.getElementById("rdAddr").checked) {
        document.getElementById("Multi_Address").style.display = "";
        document.getElementById("Multi_File").style.display = "none";
        document.getElementById("hfSendType").value = "21";
    }
    else {
        document.getElementById("Multi_Address").style.display = "none";
        document.getElementById("Multi_File").style.display = "";
        document.getElementById("hfSendType").value = "22";
    }
    return false;
}

//是否同意SP协议
function IsAgree() {
    document.getElementById("btnSend").disabled = !document.getElementById("chkAgree").checked
}

//表单验证
function CheckInput() {
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
        else {
            var blackDic = document.getElementById("hfBlackDic").value;
            if (blackDic == "1") {
                var keyWord = BlackDictionary.Parse(content).value;
                if (keyWord.length > 0) {
                    alert('短信内容包含禁用关键字【' + keyWord + '】');
                    return false;
                }
            }
        }
    }
}

//下载TxT范例
function downTxT() {
    location.href = "../Doc/Txt.rar";
    return false;
}

//下载CSV范例
function downCSV() {
    location.href = "../Doc/Csv.rar";
    return false;
}

//添加群发录
function AddItem() {
    var lst1 = document.getElementById("lstSrc");
    var lstindex = lst1.selectedIndex;
    if (lstindex < 0)
        return false;
    var v = lst1.options[lstindex].value;
    var t = lst1.options[lstindex].text;
    var lst2 = document.getElementById("lstDes");
    lst2.options[lst2.options.length] = new Option(t, v, true, true);
    lst1.options[lstindex].parentNode.removeChild(lst1.options[lstindex]);
    return false;
}

//删除群发录
function DelItem() {
    var lst1 = document.getElementById("lstSrc");
    var lst2 = document.getElementById("lstDes");
    var lstindex = lst2.selectedIndex;
    if (lstindex >= 0) {
        var v = lst2.options[lstindex].value + ";";
        lst1.options[lst1.options.length] = new Option(lst2.options[lstindex].text, lst2.options[lstindex].value, true, true);
        lst2.options[lstindex].parentNode.removeChild(lst2.options[lstindex]);
    }
    return false;
}

//获取群发录列表
function GetAllItems() {
    var lst = document.getElementById("lstDes");
    var hd = document.getElementById("hfAddressBook");
    var len = lst.options.length;
    for (var i = 0; i < len; i++) {
        if (i == 0) {
            hd.value = lst.options[i].value;
        }
        else {
            hd.value = hd.value + "," + lst.options[i].value;
        }
    }
    return false;
}

//打开文件导入对话框
function ShowFileDialog() {
    var width = 400;
    var height = 150;
    var url = "FileImport.aspx";
    var style = 'status:no;menu:no;help:0;dialogWidth:' + width + 'px ; dialogHeight:' + height + 'px';
    var val = window.showModalDialog(url, window, style);

    if (typeof (val) == "undefined" || val == "undefined") {

    }
    else {
        if (val == "-1") {
            alert('文件不是有效的模板!');
        }
        else if (val == "-2") {
            alert('文件数据不合法!');
        }
        else if (val == "-3") {
            alert('读取文件失败!');
        }
        else {
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
            document.getElementById("txtMobileNums").value = val1 + val.replace(/;/g, ";\r\n");
        }
    }
}

//返回文件路径
function GetReturnValue(obj) {
    //    var returnval = "";
    //    var file = document.getElementById("SrcFile").value;
    //    if(file.length < 1)
    //    {
    //        alert('您没有选择通讯录文件!');
    //  	    return;
    //    }
    //    if(ExtName(file) == false)
    //    {
    //        alert('系统不支持您上传文件的类型!');
    //        return;
    //    }
    window.returnValue = obj;
    window.close();
}

function ExtName(obj) {
    //为了避免转义反斜杠出问题，这里将对其进行转换
    var re = /(\\+)/g;
    var filename = obj.replace(re, "#");
    //对路径字符串进行剪切截取
    var one = filename.split("#");
    //获取数组中最后一个，即文件名
    var two = one[one.length - 1];
    //再对文件名进行截取，以取得后缀名
    var three = two.split(".");
    //获取截取的最后一个字符串，即为后缀名
    var last = three[three.length - 1];
    //添加需要判断的后缀名类型
    var tp = "txt,Txt,TXT,csv,Csv,CSV";
    //返回符合条件的后缀名在字符串中的位置
    var rs = tp.indexOf(last);
    //如果返回的结果大于或等于0，说明包含允许上传的文件类型
    if (rs >= 0) {
        return true;
    } else {
        return false;
    }
}
 
