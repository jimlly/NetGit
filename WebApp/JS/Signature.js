function $(obj) {
    return document.getElementById(obj);
}

//获取字符串的长度(一个汉字相当于两个英文字符)
function getLen(str) { return str.replace(/[^\x00-\xff]/g, "**").length; }

//js获取url参数的function
function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");

    var paraObj = {}
    for (i = 0; i < paraString.length; i++) {
        j = paraString[i];

        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);

    }

    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}

//检查非法字符
function contain(str, charset) {
    var i;
    for (i = 0; i < charset.length; i++) {
        if (str.indexOf(charset.charAt(i)) >= 0) {
            return true;
        }
    }
}

//获取某个节点的坐标
var getPosition = function (el) {
    var ua = navigator.userAgent.toLowerCase();
    var isOpera = (ua.indexOf('opera') != -1);
    var isIE = (ua.indexOf('msie') != -1 && !isOpera); // not opera spoof 

    if (el.parentNode === null || el.style.display == 'none') {
        return false;
    }

    var parent = null;
    var pos = [];
    var box;

    if (el.getBoundingClientRect)    //IE 
    {
        box = el.getBoundingClientRect();
        var scrollTop = Math.max(document.documentElement.scrollTop, document.body.scrollTop);
        var scrollLeft = Math.max(document.documentElement.scrollLeft, document.body.scrollLeft);

        return { x: box.left + scrollLeft, y: box.top + scrollTop };
    }
    else if (document.getBoxObjectFor)    // gecko 
    {
        box = document.getBoxObjectFor(el);

        var borderLeft = (el.style.borderLeftWidth) ? parseInt(el.style.borderLeftWidth) : 0;
        var borderTop = (el.style.borderTopWidth) ? parseInt(el.style.borderTopWidth) : 0;

        pos = [box.x - borderLeft, box.y - borderTop];
    }
    else    // safari & opera 
    {
        pos = [el.offsetLeft, el.offsetTop];
        parent = el.offsetParent;
        if (parent != el) {
            while (parent) {
                pos[0] += parent.offsetLeft;
                pos[1] += parent.offsetTop;
                parent = parent.offsetParent;
            }
        }
        if (ua.indexOf('opera') != -1
            || (ua.indexOf('safari') != -1 && el.style.position == 'absolute')) {
            pos[0] -= document.body.offsetLeft;
            pos[1] -= document.body.offsetTop;
        }
    }

    if (el.parentNode) { parent = el.parentNode; }
    else { parent = null; }

    while (parent && parent.tagName != 'BODY' && parent.tagName != 'HTML') { // account for any scrolled ancestors 
        pos[0] -= parent.scrollLeft;
        pos[1] -= parent.scrollTop;

        if (parent.parentNode) { parent = parent.parentNode; }
        else { parent = null; }
    }
    return { x: pos[0], y: pos[1] };
};

function changeName(obj, sealID) {
    if (obj.innerHTML.substring(1, 6).toUpperCase() != "INPUT") {
        if (obj.innerHTML.trim() == "") {
            obj.innerHTML = "<input id=\"newName\" type=\"text\" style=\"width:60px;\" maxlength=\"30\" onblur=\"getNewName(this,'" + sealID + "')\" onkeydown=\"if(event.keyCode==13){this.onblur();}\" title=\"键入签章名称，再点击空白处保存修改\"/>";
        }
        else {
            obj.innerHTML = "<input id=\"newName\" type=\"text\" size=\"" + getLen(obj.innerHTML.trim()) + "\" maxlength=\"30\" onblur=\"getNewName(this,'" + sealID + "')\" onkeydown=\"if(event.keyCode==13){this.onblur();}\"  value=\"" + obj.innerHTML.trim() + "\" title=\"更改签章名称，再点击空白处保存修改\" />";
        }
        document.getElementById("newName").focus();
        document.getElementById("newName").select();
    }
}

function getNewName(obj, sealID) {
    if (obj.value.trim() != "") {
        var str = "";
        if (contain(obj.value, "~!@#$%^&*()><\/'|")) {
            alert("注意：输入了~!@#$%^&*()><\/'|等非法字符！");
            obj.value = "";
        }
        else {
            str = obj.value.trim();
        }
        obj.parentNode.title = "名称：\"" + obj.value.trim() + "\"，点击可修改签章名称，再点击空白处保存修改";
        obj.parentNode.innerHTML = str;
    }
    else {
        obj.parentNode.title = "点击此处添加签章名称，再点击空白处保存修改";
        obj.parentNode.innerHTML = "";
    }
    WebAPP.ElecSignature.SignatureManager.UpdateUserSealName(sealID, obj.value.trim());
}

function changeRemark(obj, sealID) {
    if (obj.innerHTML.substring(1, 6).toUpperCase() != "INPUT") {
        if (obj.innerHTML.trim() == "") {
            obj.innerHTML = "<input id=\"newRemark\" type=\"text\"  maxlength=\"60\" onblur=\"getNetRemark(this,'" + sealID + "')\" onkeydown=\"if(event.keyCode==13){this.onblur();}\" title=\"键入签章说明，再点击空白处保存修改\"/>";
        }
        else {
            obj.innerHTML = "<input id=\"newRemark\" type=\"text\" size=\"" + getLen(obj.innerHTML.trim()) + "\" maxlength=\"60\" onblur=\"getNetRemark(this,'" + sealID + "')\" onkeydown=\"if(event.keyCode==13){this.onblur();}\" value=\"" + obj.innerHTML.trim() + "\" title=\"更改签章说明，再点击空白处保存修改\" />";
        }
        document.getElementById("newRemark").focus();
        document.getElementById("newRemark").select();
    }
}

function getNetRemark(obj, sealID) {
    if (obj.value.trim() != "") {
        var str = "";
        if (contain(obj.value, "~!@#$%^&*()><\/'|")) {
            alert("注意：输入了~!@#$%^&*()><\/'|等非法字符！");
            obj.value = "";
        }
        else {
            str = obj.value.trim();
        }
        obj.parentNode.title = "说明：\"" + obj.value.trim() + "\"，点击可修改说明，再点击空白处保存修改";
        obj.parentNode.innerHTML = str;
    }
    else {
        obj.parentNode.title = "点击此处添加说明，再点击空白处保存修改";
        obj.parentNode.innerHTML = "";
    }
    WebAPP.ElecSignature.SignatureManager.UpdateUserSealRemark(sealID, obj.value.trim());
}

function ShowPicFile(picUrl) {
    var strUrl = "showpic.html?picurl=" + picUrl;
    var chkWindow;
    var strWidth = "550";
    var strHeight = "550";
    var strWinowStyle = 'status:no;menu:no;help:0;dialogWidth:' + strWidth + 'px ; dialogHeight:' + strHeight + 'px';
    window.showModalDialog(strUrl, window, strWinowStyle);
}


