/**
 * CommonCheck.js
 * 用于页面共通校验
 * 2008-05-07 dengjs by YuanTel
 */

//**********************************************************
//* 全局变量定义
//**********************************************************
//要屏蔽掉的特殊字符
var specialCharacter = "\~\!\@\\#\$\%\^\&\*\(\)\'\>\<\\\/\|"; 
var maxStrLength = 10000;
//密码的最小长度和最大长度
var MinPasswordLength = 8;
var MaxPasswordLength = 20;
var MsgPasswordLengthError = "";

//**********************************************************
//* 正则表达式
//**********************************************************
// 国内邮政编码
var zipRegex = /\d{6}/;

// 移动电话(13xxxxxxxxx,15xxxxxxxxx)
//var mobileRegex =  /^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
var mobileRegex =  /^(1{1}\d{10})$/;
//var mobileRegex =  /^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1}))+\d{8})$/;

//固定电话或传真(3位或4位区号+‘-或没有’+7位或8位号码+1-8位分机号或没有)
//如：01012345678、010-12345678、0101234567、010-1234567、071612345678、0716-12345678、07161234567、0716-12345678
//    01012345678,0001、010-12345678,0001都是合法的
var teleRegex = /^(((0\d{2,3})((-)|()))\d{7,8})(($)|(,\d{1,8}$))/;

//网址
//如：http://www.fax99.com https://www.fax99.com ftp://www.fax99.com http://mail.fax99.com
//    http://www.fax99-fax99.com http://www.fax99.fax99.com http://www.fax99_fax99.com都是合法的。
var urlRegex = /^((http|https|ftp):(\/\/|\\\\)|())(((\w)|(-)(.))+[.]){1,}(com|net|cn|org|edu|gov|mil|arts|web)$/;

//Email
//第一位必须是字母，数字或_，中间可以包含字母，数字，_，.，-，必须有‘@’，‘@’以后可以包含字母，数字，_，-，
//然后是‘.’，‘.’后面只能是字母
//如：djs-007.1@djs-007_1.com
//var emailRegex = /^(\w)+((\w)|(-)|(.))+(@+((\w)|(-))+.)+((\w)|(-))/;
//var emailRegex = /^((\w)|-)+(@+)((\w)|-)+(.+)((\w)|-)+$/;
var emailRegex = /^([a-zA-Z0-9_\-])+([a-zA-Z0-9_\-\.])*@([a-zA-Z0-9_\-])+([a-zA-Z0-9_\-\.])*((\.[a-zA-Z0-9_\-]{1,}){1,})$/;

//身份证号 15位或18位数字 待修改
var idCardRegex = /^[1-9]{1}+[0-9]{13}?(([0-9X]$)|((\d){3}([0-9X])$))/;

//由数字、字母或者_组成的0-20个半角字符的字符串
var strRegex20 = /^([\w-]){0,20}$/;

//由数字、字母或者_组成的6-12个半角字符的字符串
var strRegex612 = /^(\w){6,12}$/;

//管理账号第一位只能是字母，其余可以是数字，字母和_，不能超过10位的半角字符
//var accoutRegex = /^[a-zA-Z]\w{0,9}$/;

//管理账只能是数字和字母组成
var accoutRegex = /^[a-zA-Z0-9]+$/;
                     
var password = /^(\w){0,32}$/;                      //由数字、26个英文字母或者下划线组成的字符串
var account = /^(\w){0,10}$/;                      //由数字、26个英文字母或者下划线组成的字符串

var numReg = /^([\d]){10,20}$/;
var numExtReg = /^([\d]){4,10}$/;
var OnlyNum = /^[\d]+$/;                //纯数字

//金额
var moneyRegex = /[\d.]/;
var moneyRegex1 = /^(([1-9]\d{0,6})|(0))(\.\d{1,2})?$/;

var num = /^[1-9][0-9]*$/;

var OnlyNum_10 = /^[\d]{10}$/;                //纯数字

var masterPageName = "ctl00_ContentPlaceHolder1_";

var Url_Random_Suffix = "t=" + Math.random();   //随机数URL后缀
//**********************************************************
//* 方法
//**********************************************************

//自定义函数，取得页面对象
$ = function(obj){return document.getElementById(obj);}

//选择产品时隐藏下一步按钮，防止网络慢时造成数据错误
function SelectProduct_Hidded()
{
    //$("btnCancel").parentNode.parentNode.style.display = "none";
    $("btnCancel").parentNode.innerHTML = "数据加载中，请稍后。。。";
    //$("btnCancel").parentNode.style.color = "Red";
}

//判断400号码是预占状态还是空闲状态
function JudgePhoneIsFree(phoneNo)
{
    var flag = AjaxCustomer.JudgePhoneIsFree(phoneNo).value;
    if(flag == 1)
    {
        return "要订购此号，请先尽快联系客服预留号码！";
    }
    else
    {
        return "";
    }
}

function DoClose()
{
    if(confirm("确认要关闭本页面吗？"))
    {
        window.close();
    }
}

//返回浏览历史页面
//Index -1：前一个页面；-2：前2个页面；依次类推
function GoHistory(Index)
{
    window.history.back(Index);
}

//返回到指定页面
function GoBackPage(PageUrl)
{
    var s;
    //如果包含有?
    if(PageUrl.indexOf('?') != -1)
    {
        s = "&";
    }
    else
    {
        s = "?";
    }
    //window.location.href = unescape(PageUrl) + s + "t=" + Math.random();
    window.location.href = unescape(PageUrl);
}

//初始化下拉框对象
function InitSelectOpt(opt)
{
    opt.length=0;
    opt.options.add(new Option("- 请选择",""));
}

//验证失败时对象被选中，背景变色
//CheckedObj    要验证的对象
//ShowObj       显示提示信息的对象
//ShowMsg       显示的提示信息
function Failure(CheckedObj, ShowObj, ShowMsg)
{
    if(CheckedObj != null)
    {
        CheckedObj.focus();
        CheckedObj.select();
        CheckedObj.style.backgroundColor="red";
    }

    if(ShowObj != null)
    {
        ShowObj.innerHTML = ShowMsg;
    }
}

//验证失败时对象被选中，背景变色
//CheckedObj    要验证的对象
//ShowObj       显示提示信息的对象
function Success(CheckedObj, ShowObj)
{
    if(CheckedObj != null)
    {
        CheckedObj.style.backgroundColor="";
    }
    
    if(ShowObj != null)
    {
        ShowObj.innerHTML = "";
    }
}

//检查密码是否是数字+字符的组合
//计算输入密码中数字和非数字字符的个数，如果个数都大于0，表示是数字+字符的组合，返回true；否则不是，返回false。
function CheckPassword(password)
{
    var count = password.length;
    var numCount = 0;
    var charCount = 0;
    for(i=0; i<count; i++)
    {
        if(OnlyNum.test(password.charAt(i)))
        {
            numCount = numCount + 1;
        }
        else
        {
            charCount = charCount + 1;
        }
    }
    if(numCount != 0 && charCount != 0)
    {
        return true;
    }
    else
    {
        return false;
    }
}

//判断是否为数字
function IsNum(persent)
{
    return num.test(persent);
} 



//检查折扣率
//CheckObj              要检查的对象
//ComparedObjValue      要比较的对象的值
//Msg                   返回检查的信息  为""时表示检查通过，否则不通过，显示信息。
function CheckPercent(CheckObj, ComparedObjValue, Msg)
{
    if(CheckObj == null || ComparedObjValue == null)
    {
        return "";
    }
    if(CheckObj.value == "")
    {
        Msg = Msg + "不能为空！";
        return Msg;
    }
    if(!moneyRegex1.test(CheckObj.value))
    {
        Msg = Msg + "必须为半角数字！";
        return Msg;
    }
    if(parseFloat(CheckObj.value) < parseFloat(ComparedObjValue) || parseFloat(CheckObj.value) > 100)
    {
        Msg = Msg + "不能低于代理商的折扣率[" + ComparedObjValue + "%]并且不能大于100%！";
        return Msg;
    }
    return "";
}

//CheckBox全选或全不选。
function setcheck(value) 
{
    var objs = document.getElementsByTagName("input");
    for(var i=0; i<objs.length; i++) 
    {
       if(objs[i].type.toLowerCase() == "checkbox" )
       {
           objs[i].checked = value;
       }
    }
}

//返回字符串的长度
function GetCharLength(str)
{
	return str.replace(/[^\x00-\xff]/g,"**").length;
}

//判断str中是否包含有specialCharacter中的字符
//specialCharacter为特殊(非法)字符
function contain(str)
{ 
    var i;
    for(i=0;i<specialCharacter.length;i++){
        if(str.indexOf(specialCharacter.charAt(i)) >= 0)
        {
            return true;
        }
    }
}

//判断指定的字符串是否合法
function CheckCharLength(CheckObj, allowLength){
    //判断输入是否是指定的特殊字符
    if(specialCharacter.indexOf(String.fromCharCode(event.keyCode)) >= 0)
    {
        return false;
    }
    //判断指定的字符串的长度是否在规定的长度以内
    var str = CheckObj.value + String.fromCharCode(event.keyCode);
    if(GetCharLength(str) > allowLength){
        return false;
    }
}

//按下回车时执行的函数
function BodykeyDown(CheckObj)          
{   
    //判断是否按下“回车键”
    if   (event.keyCode == 13)          
    {
        CheckObj.onclick();
    }   
}

//分页查询，转到按钮按下时对输入页码的检查
function CheckPageNo()
{
    var input = document.getElementById("txtPageNo").value;
    var msg = AjaxCustomer.CheckInput(input).value;
    if (msg != "") 
    {
        alert(msg);
        return false;
    }
}

//检查备注的长度
function maxCheck(CheckObj, maxLength)
{   
    var length = GetCharLength(CheckObj.value + String.fromCharCode(event.keyCode)); 
    if (length > maxLength && event.keyCode != 8)
    {
        alert("备注不能超过200个半角字符或者100个汉字！");
        CheckObj.select();
        return false;
    }
    else
    {
        return true;
    }
}

//初始化页面最大化
function MaxWindow()
{
    self.resizeTo(screen.availWidth, screen.availHeight);
    self.window.moveTo(0, 0);
    //self.focus();
}

//初始化页面时指定控件获得焦点
function HtmlPageLoad(objName)
{
    document.getElementById(objName).focus();
}

//判断str中是否包含有specialCharacter中的字符
//specialCharacter为特殊(非法)字符
function contain(str, specialChar)
{ 
    var i;
    for(i=0;i<specialChar.length;i++){
        if(str.indexOf(specialChar.charAt(i)) >= 0)
        {
            return true;
        }
    }
}

function JudgeUserAccount(useraccount, maxLength)
{
    var len = useraccount.length;
    //判断字符串的长度
    if (GetCharLength(useraccount) > maxLength)
    {
        return "只能输入" + maxLength + "个半角字符或" + Math.round(maxLength/2)  +"个全角字符";
    }
    //如果有特殊字符
    var specialChar = "\%\~\*\<\>\`\&\'\"";
    if(contain(useraccount, specialChar))
    {
        return "不能输入如：" + "【\%】【\~】【\*】【\<】【\>】【\`】【\&】【\'】【\"】" + "特殊字符";
    }
    return "";
}
function JudgeUserAccountNoSpace(useraccount, maxLength)
{ 
    var len = useraccount.length;
    //判断字符串的长度
    if (GetCharLength(useraccount) > maxLength)
    {
        return "只能输入" + maxLength + "个半角字符或" + Math.round(maxLength/2)  +"个全角字符";
    }
    //如果有特殊字符
    var specialChar = "\ \　\%\~\*\<\>\`\&\'\"";
    if(contain(useraccount, specialChar))
    {
        return "不能输入如：空格、" + "【\%】【\~】【\*】【\<】【\>】【\`】【\&】【\'】【\"】" + "特殊字符";
    }
    return "";
}

function  ShowSalePromotion(flag)
{
    if (flag) 
    {
        document.getElementById("spanSalePromotion").style.display = "";
    }
    else
    {
        document.getElementById("spanSalePromotion").style.display = "none";
    }
}

function ChooseSalePromotion(CheckObj)
{
    alert("您选择了"+CheckObj.value);
}

function Check(CheckObj)
{
    if (CheckObj.style.display == "none") 
    {
        ShowSalePromotion(false);
    }
    else
    {
        ShowSalePromotion(true);
    }
}

//将指定列的值用指定符号连接并返回（返回一列）
//cellIndex     指定列的列号
//inputType     要遍历的控件类型（一般为"checkbox"或者"radio"）
//character     用于链接的符号（如","）
function BtnSelectAndBack(cellIndex, inputType, character)
{
	var args = new Array();
	var flag = 1;
    with(document.forms[0]){
  	    for(i=0;i<length;i++){
	   	    if(elements[i].type == inputType && flag > 1){
		  	    if(elements[i].checked){
			        args.push(elements[i].parentNode.parentNode.cells[cellIndex].innerHTML);
			    }
			    flag = flag + 1;
		    }
	    }
    }
    var returnValue = args.join(character);
    if(returnValue == null || returnValue == ""){
        alert("没有选择任意行！");
        return;
    }
    window.returnValue = returnValue;
    window.close();    
}
//将指定列的值用指定符号连接并返回（返回两列）
//cellIndex1     指定列的列号
//cellIndex2     指定列的列号
//inputType      要遍历的控件类型（一般为"checkbox"或者"radio"）
//character1     用于链接的符号（如","）
//character2     用于链接的符号（如","）
function BtnSelectAndBack(firstName, cellIndex1, cellIndex2, inputType, character1, character2)
{
	var args1 = new Array();
	var args2 = new Array();
	var returnValue = "";
	
	//显示的值
    with(document.forms[0]){
  	    for(i=0;i<length;i++){
	   	    if(elements[i].type == inputType && elements[i].id != firstName){
		  	    if(elements[i].checked){
			        args1.push(elements[i].parentNode.parentNode.parentNode.cells[cellIndex1].innerHTML);
			        args2.push(elements[i].parentNode.parentNode.parentNode.cells[cellIndex2].innerHTML);
			    }
		    }
	    }
    }
    returnValue = args1.join(character1) + character2 + args2.join(character1);
    if(returnValue == null || returnValue == ""){
        alert("没有选择任意行！");
        return;
    }
    window.returnValue = returnValue;
    window.close(); 
}

//下一步按钮
function BtnGoNext()
{
	if (CheckSerFlagData())
	{
	    document.getElementById("btnShowPhoneNo").disabled = false;
	    document.getElementById("divFrist").style.display = "none";
	    document.getElementById("divSecond").style.display = "";
	}
}
//上一步按钮
function BtnGoUp()
{
	document.getElementById("divFrist").style.display = "";
	document.getElementById("divSecond").style.display = "none";
}

//检查Email（代理商Email检查）
//CheckObj          要检查的对象
//ComparedObjValue  要比较的对象的值（这里指旧的Email）
//MsgObj            显示错误信息的对象
function CheckAgentEmail(CheckObj, ComparedObjValue, MsgObj)
{
    if(CheckObj.value =="")
    {
        Failure(CheckObj, MsgObj, "电子邮箱不能为空！");
        return false;
    }
    
	if (CheckObj.value != ComparedObjValue)
	{
        if(!emailRegex.test(CheckObj.value))
        {
            Failure(CheckObj, MsgObj, "电子邮箱的格式不正确或者长度超过50个半角字符！正确的例子：myname@163.com");
            return false;
        }
	    var count = AjaxCustomer.JudgeAgentEmailIsExist(CheckObj.value).value;
	    if(count > 0)
	    {
	        Failure(CheckObj, MsgObj, "电子邮箱已经存在，请重新输入！");
	        return false;
	    }
	}
	else 
	{
	    if(ComparedObjValue != "" && !emailRegex.test(ComparedObjValue))
        {
            Failure(CheckObj, MsgObj, "电子邮箱的格式不正确或者长度超过50个半角字符！正确的例子：myname@163.com");
            return false;
        }
	}
	
	Success(CheckObj, MsgObj);
	return true;
}

//检查Email（终端用户Email检查）
//CheckObj          要检查的对象
//ComparedObjValue  要比较的对象的值（这里指旧的Email）
//MsgObj            显示错误信息的对象
function CheckEmail(CheckObj, ComparedObjValue, MsgObj)
{
    if(CheckObj.value =="")
    {
        Failure(CheckObj, MsgObj, "电子邮箱不能为空！");
        return false;
    }
    
	if (CheckObj.value != ComparedObjValue)
	{
        if(!emailRegex.test(CheckObj.value))
        {
            Failure(CheckObj, MsgObj, "电子邮箱的格式不正确或者长度超过50个半角字符！正确的例子：myname@163.com。");
            return false;
        }
	    var count = AjaxCustomer.JudgeEmailIsExist(CheckObj.value).value;
	    if(count > 0)
	    {
	        Failure(CheckObj, MsgObj, "电子邮箱已经存在，请重新输入！");
	        return false;
	    }
	}
	else 
	{
	    if(ComparedObjValue != "" && !emailRegex.test(ComparedObjValue))
        {
            Failure(CheckObj, MsgObj, "电子邮箱的格式不正确或者长度超过50个半角字符！正确的例子：myname@163.com");
            return false;
        }
	}
	
	Success(CheckObj, MsgObj);
	return true;
}

//检查用户名
//CheckObj      要检查的对象
//MsgObj        显示错误信息的对象
function CheckUserAccount(CheckObj, MsgObj)
{
    if(CheckObj.value == "")
    {
        Failure(CheckObj, MsgObj, "用户名不能为空！");
        return false;
    }
	if (CheckObj.value != "")
	{
        //Update By 邓劲松 2009-03-24 用户名必须是Email格式 Start
        //varInnerHtml = JudgeUserAccountNoSpace(CheckObj.value, 50);
        //if (varInnerHtml != "")
        //{
        //    Failure(CheckObj, MsgObj, "用户名" + varInnerHtml);
        //    return false;
        //}
        if(!emailRegex.test(CheckObj.value))
        {
            Failure(CheckObj, MsgObj, "用户名必须是合法的Email地址。正确的例子：myname@163.com。");
            return false;
        }
        //Update By 邓劲松 2009-03-24 用户名必须是Email格式 End
	    var count = AjaxCustomer.JudgeUserAccountIsExist(CheckObj.value).value;
	    if(count > 0)
	    {
	        Failure(CheckObj, MsgObj, "用户名已经存在，请重新输入！");
	        return false;
	    }
	}
	Success(CheckObj, MsgObj);
    return true;
}

//检查用户名
//CheckObj      要检查的对象
//id            所属代理商ID
//MsgObj        显示错误信息的对象
function CheckAgentUserAccount(CheckObj, id, MsgObj)
{
    if(CheckObj.value == "")
    {
        Failure(CheckObj, MsgObj, "用户名不能为空！");
        return false;
    }
	if (CheckObj.value != "")
	{
        var varInnerHtml = JudgeUserAccountNoSpace(CheckObj.value, 50);
        if (varInnerHtml != "")
        {
            Failure(CheckObj, MsgObj, "用户名" + varInnerHtml);
            return false;
        }
	    var count = AjaxCustomer.JudgeAgentUserAccountIsExist(CheckObj.value, id).value;
	    if(count > 0)
	    {
	        Failure(CheckObj, MsgObj, "用户名已经存在，请重新输入！");
	        return false;
	    }
	}
	Success(CheckObj, MsgObj);
    return true;
}

//参数说明ShowImage(fileType, usertype, id)
//fileType：表示查看的文件类型  licence：营业执照复印件 logo：Logo文件  identityCard：身份证复印件
//usertype：用户身份    a：代理商   c：终端用户
//id：用户加密后的SeqNo
function ShowImage(fileType, usertype, id)
{
    if(id == 0)
    {
        alert("没有图片！");
        return;
    }
    var strUrl = "../comm/image.aspx?fileType=" + fileType + "&userType=" + usertype + "&id=" + id + "&t=" + Math.random();
    var strWinowStyle = "dialogWidth:1000px;dialogHeight:800px;dialogLeft:1000px;scroll:yes;status:yes;status:no;help:no;";
    //window.showModalDialog(strUrl, window,strWinowStyle);
    window.open(strUrl);
}

//让对象显示或不显示
function DisPlayWarmInfo(obj)
{
    if(obj != null)
    {
        if(obj.style.display == "none")
        {
            obj.style.display = "";
        }
        else
        {
            obj.style.display = "none";
        }
    }
}

//复制输入的内容到指定的对象
function CopyData(inputObj, optObj)
{
    if(inputObj != null && optObj != null)
    {
        optObj.value = inputObj.value;
    }
}


function SelectRows()
{
//    var tb=this,tr,ee;
//    ee = e==null?event.srcElement:e.target;
//    if(ee.tagName!="TD")
//    {
//        return;
//    }
//    tr = ee.parentNode;
//    if(tb.selRow!=null) 
//    {
//        setTrReveal(tb.selRow,"background:white",1);
//    }
//    setTrReveal(tr,"background:#EAEAEA") 
//    tb.selRow=tr;
} 
function setTrReveal(tr,css,noDelay)
{ 
    if(!document.all) 
    {
        return tr.style.cssText+=";"+css 
    }
    for(i=0;i< tr.cells.length;i++)
    { 
        if(noDelay)
        { 
            tr.cells[i].style.cssText+=";"+css;
            continue;
        } 
        tr.cells[i].style.filter="progid:DXImageTransform.Microsoft.RevealTrans(duration=0.5,transition=16)" ;
        tr.cells[i].filters[0].apply() ;
        tr.cells[i].style.cssText+=";"+css ;
        tr.cells[i].filters[0].play();
    } 
} 

function GetHelpMessage(msg)
{
    ymPrompt.alert({title:'帮助',message:msg,width:500,height:250,useSlide:true})
}
