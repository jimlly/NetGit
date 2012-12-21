// JScript 文件
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
//文件名
var specialFilename = "\:\\\/\*\?\"\<\>\|\""; 
var maxStrLength = 10000;


//**********************************************************
//* 正则表达式
//**********************************************************
// 国内邮政编码
var zipRegex = /\d{6}/;

// 移动电话(13xxxxxxxxx,15xxxxxxxxx)
//var mobileRegex =  /^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
var mobileRegex =  /^(1{1}\d{10})$/;

//固定电话或传真(3位或4位区号+‘-或没有’+7位或8位号码+1-8位分机号或没有)
//如：01012345678、010-12345678、0101234567、010-1234567、071612345678、0716-12345678、07161234567、0716-12345678
//    01012345678,0001、010-12345678,0001都是合法的
//var teleRegex = /^(((0\d{2,3})((-)|()))\d{7,8})(($)|(,\d{1,8}$))/;
var teleRegex = /^((0\d{2,3})\d{7,8})(($)|(,\d{1,8}$))/;


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

var OnlyNum = /^[\d]+$/;                //纯数字

//**********************************************************
//* 方法
//**********************************************************

//取得表单对象方法
$ = function(obj){return document.getElementById(obj);}

//验证失败时对象被选中，背景变色
//CheckedObj    要验证的对象
//ShowObj       显示提示信息的对象
//ShowMsg       显示的提示信息
function Failure(CheckedObj, ShowObj, ShowMsg)
{
    if(CheckedObj != null)
    {
//        CheckedObj.focus();
//        CheckedObj.select();
        CheckedObj.style.backgroundColor="#e6e6e6";    
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


//返回字符串的长度
function GetCharLength(str)
{
	return str.replace(/[^\x00-\xff]/g,"**").length;
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
    if(GetCharLength(str) > allowLength)
    {
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



//判断str中是否包含有specialCharacter中的字符
//specialCharacter为特殊(非法)字符
function contain(useraccount, specialChar)
{ 
    var i;
    for(i=0;i<specialChar.length;i++)
    {
        if(useraccount.value.indexOf(specialChar.charAt(i)) >= 0)
        {  
            return true;
        }
    }
}
//检查名字
function JudgeUserAccountNoSpace(useraccount, namemaxLength)
{ 
    
    //判断字符串的长度
    if( useraccount.value.length > namemaxLength)
    {
        return "最多只能输入" + namemaxLength + "个字符！您已经输入" + useraccount.value.length + "个字符数！" ;
    }
    
    //如果有特殊字符
    var specialChar = "\ \　\%\~\*\<\>\`\&\'\!\@\#\$\^\；\,\？\、\！\￥\+\=\……\"";
    if(contain(useraccount, specialChar))
    { 
        return "不能输入的特殊字符包括：空格、【%】【~】【*】【<】【>】【`】【&】【'】【\"】等";
    }
    return "";
}




//复制输入的内容到指定的对象
function CopyData(inputObj, optObj)
{
    if(inputObj != null && optObj != null)
    {
        optObj.value = inputObj.value;
    }
}
//返回字符串中某个字符或字符串出现的次数,其中mainStr为要查找的字符串,subStr为要检查的字符串或字符
function countInstances(mainStr, subStr)
{
    var count = 0;
    var offset = 0;
    do
    {
        offset = mainStr.indexOf(subStr, offset);
        if(offset != -1)
        {
            count++;
            offset += subStr.length;
        }
    }while(offset != -1)
    return count;
}

//用户姓名
function checkName(useraccount,spanObj)
{ 
    if(useraccount.value == "")
    {
         $("must").innerHTML = "";
         Failure(useraccount, spanObj,'不可以为空！');
         return false;      
    }
    
    var varInnerHtml = JudgeUserAccountNoSpace(useraccount, 20);
    if (varInnerHtml != "")
    { 
        //验证失败时的提示
//        useraccount.value = "";
        Failure(useraccount, spanObj,varInnerHtml);
        return false;
        
    }
    Success(useraccount, spanObj)
    return true;
}
//验证手机.
function checkMobile(useraccount,spanObj)
{
      if(useraccount.value == "")
      {
            Success(useraccount, spanObj)
            return true;
      }
      else if (useraccount.value.length > 11)
      {
            Failure(useraccount, spanObj,'手机号码过长！长度应为11位,您已输入' + useraccount.value.length + '位！');
            return false;
      }
      else if(mobileRegex.test(useraccount.value))
      {
            Success(useraccount, spanObj)
            return true;
      }
      else
      {
            Failure(useraccount, spanObj,'请输入合法的手机号码');
            return false;
      }

}

//传真固话.
function checkfaxphone(useraccount,spanObj)
{
    
      if(useraccount.value == "")
      {
            Success(useraccount, spanObj)
            return true;
      }
      else if (useraccount.value.length > 32)
      {
            Failure(useraccount, spanObj,'号码过长！号码的最大长度为32位,您已输入' + useraccount.value.length + '位！');
            return false;
      }
      else if(teleRegex.test(useraccount.value))
      {
            Success(useraccount, spanObj)
            return true;
      }
      else
      {
            Failure(useraccount, spanObj,'号码格式不正确！号码格式为：区号+电话号码,分机1-8位。例：01058851200,0001(全部只能为半角数字)');
            return false;
      }
}
//Email.
function checkEmail(useraccount,spanObj)
{
      if(useraccount.value == "")
      {
            Success(useraccount, spanObj)
            return true;
      }
      else if (useraccount.value.length > 50)
      {
            Failure(useraccount, spanObj,'Email的最大长度50个字符！您已输入' + useraccount.value.length);
            return false;
      }
      else if(emailRegex.test(useraccount.value))
      {
            Success(useraccount, spanObj)
            return true;
      }
      else
      {
            Failure(useraccount, spanObj,'Email的格式不正确！正确的例子：myname@163.com');
            return false;
      }
}
//网页.
function checkUrl(useraccount,spanObj)
{
     
      if (useraccount.value.length > 50)
      {
            Failure(useraccount, spanObj,'企业网址输入长度超过50个字符！您已输入' + useraccount.value.length);
            return false;
      }
      else if(useraccount.value == "")
      {
            Success(useraccount, spanObj)
            return true;
      }
      else if(urlRegex.test(useraccount.value))
      {
            Success(useraccount, spanObj)
            return true;
      }
      else
      {
            Failure(useraccount, spanObj,'企业网址输入不正确！　');
            return false;
      }
}

//检查备注的长度
function maxCheck(CheckObj,spanObj)
{   
    var length = CheckObj.value.length;
    
    if (length > 1000 && event.keyCode != 8)
    {   
        Failure(CheckObj, spanObj,'备注只能输入1000个字符！您已经输入了' + length + '个字符！');
        return false;
    }
    else
    {
        return true;
    }
}
//新建组名
function checkGroupName(useraccount,spanObj)
{ 
    var varInnerHtml = JudgeUserAccountNoSpace(useraccount, 10); 
    if (varInnerHtml != "")
    { 　
        $("must").innerHTML = "";
        Failure(useraccount, spanObj,varInnerHtml);
        return false;
        
    }
    else
    {
        Success(useraccount, spanObj)
        return true;
    }
}

//用于检查长度.
function checkstring(useraccount,spanObj,MaxLength)
{
    if ( useraccount.value.length > MaxLength)
    {
        Failure(useraccount, spanObj,"字数不得超过" + MaxLength + "个字符！您已经输入了" + useraccount.value.length + "个字符！");
        return false;
    }
    else
    {
        Success(useraccount, spanObj)
        return true;
    }
    
}

//检查保存时的页面状态是否合法.用于联系人信息.flag =0:为可编辑，flag=1：为只读.
function btnCheckcontactorInfo()
{
    
    var name = document.getElementById("Spanname");
    var phone = document.getElementById("Spanphone");
    var fax = document.getElementById("Spanfax");
    var mobile = document.getElementById("Spanmobile");
    var home = document.getElementById("Spanhome");
    var spanEmail = document.getElementById("spanEmail");
    var spanHttp = document.getElementById("spanHttp");
    var spanRemark = document.getElementById("spanRemark");

    var inputname = document.getElementById("txtName");
    var address = document.getElementById("spanAddress");
    
    
    checkName($("txtName"),name);
    checkMobile($("txtMobile"),mobile);
    checkfaxphone($("txtJobPhone"),phone);
    checkfaxphone($("txtJobFax"),fax);
    checkfaxphone($("txtHomePhone"),home);

    checkEmail($("txtMail"),spanEmail) ;
    checkUrl($("txtUrl"),spanHttp);
    checkstring($("txtAddress"),$("spanAddress"),50);
    checkstring($("txtCompany"),$("spanCompany"),50);
    checkstring($("txtJob"),$("spanJob"),20);
    maxCheck($("txtRemark"),$("spanRemark"));
    
    if(!(checkName($("txtName"),name) && checkMobile($("txtMobile"),mobile) && checkfaxphone($("txtJobPhone"),phone) && checkfaxphone($("txtJobFax"),fax) && checkfaxphone($("txtHomePhone"),home)))
    {
        alert("请仔细检查您的基本信息输入项！");
        return false;
    }
    else if(!(checkEmail($("txtMail"),spanEmail) && checkUrl($("txtUrl"),spanHttp) && checkstring($("txtAddress"),address,50) && checkstring($("txtCompany"),$("spanCompany"),50) && checkstring($("txtJob"),$("spanJob"),20) && maxCheck($("txtRemark"),$("spanRemark"))))
    {
        alert("请仔细检查您的详细信息输入项！");
//        EditDetail();
        var obj =  document.getElementById("info")
        var iaddressToggleHref = document.getElementById("iaddressToggleHref");
        if(obj.style.display=="none")
        {
            obj.style.display="block"
            iaddressToggleHref.innerText="关闭详细信息"
        }
            return false;
    }
    else
    {
        return true;
    }
}

var oldmyDate = new Date();
var oldtime = oldmyDate.getTime();

function FakesearchTypeinput()
{
    alert("您的操作太快了，请稍后再试！");
    return false;
}

//搜索输入栏.//1:姓名，2：公司名称，3：联系方式，4：号码属性
//flag 设为不同输入框的选择类型；flag = 0为组名，flag = 1,为4种搜索;flag = 2 为0：姓名，1:办公电话 2：传真 3：手机 4：家庭电话
function searchTypeinput(flag)
{   
   var ret = false;
   
   //用于校验固话， 传真.
   var reg =/[^\d\,]/g;
   var content = $("txtContent");
   var spansearch = $("spansearch");
   var searchtype = $("Select1");
   var which; 
     
   if( content.value == "")
   {
        Success(content, spansearch);
        ret = true;
   }
   else if(!checkstring(content,spansearch,50))
   {
        ret = false;
   }
   else
   {
        //flag = 0为组名
       if( flag == 0 )
       {
          which = 1;
       }
       //搜索：1姓名，2电话，3公司名称
       else if( flag == 1)
       {
            
            if(searchtype.value == 3 || searchtype.value == 1)
            {
                　which = 1;
            }
            else if( searchtype.value == 2 )
            {
                 which = 2;
            }
       }
       //搜索：1姓名，2办公电话，3传真，4手机，5家庭电话
       else if( flag == 2 )
       {
            if( searchtype.value == 1)
            {
                which = 1;
            }
            else if( searchtype.value == 4 )
            {
                which = 3;
            }
            else if( searchtype.value == 2 || searchtype.value == 3 || searchtype.value == 5)
            {
                which = 2;
            }
       }

        //1:姓名，6：公司名称
       if( which == 1)
       {

           if(contain(content, specialCharacter))
           {
                Failure(content, spansearch,"您输入的含有非法字符！");
                ret = false;
           }
           else
           {
                Success(content, spansearch);

                ret = true;             
          }
       }
       //2：办公电话，4：传真号，5：家庭电话，
       else if( which == 2 )
       {
            if(countInstances(content.value, ',') >= 2 )
            {
                Failure(content, spansearch,"您最多只能输入一个逗号！");
                ret = false;
            }
            else if(reg.test(content.value))
            {
                Failure(content, spansearch,"电话、传真只能由半角数字和逗号组成！");
                ret = false;
            }
            else
            {
                Success(content, spansearch)
                ret = true;
            }
                 
       }
       //3：手机号码
       else if( which == 3 )
       {    
           if(!(OnlyNum.test(content.value)))
           {
                Failure(content, spansearch,"手机号码只能由半角数字组成！");
                ret = false;
           }
           else
           {
                Success(content, spansearch)
                ret = true;
           }
       }
   }
   return ret;
}

//检查文件名.
function CheckspecialFilename(useraccount,spanObj)
{   
    if( useraccount.value == "")
    {
        Success(useraccount, spanObj);
        return true;
    }
    else if(!checkstring(useraccount,spanObj,50))
    {
        return false;
    }
    if(contain(useraccount,specialFilename) == true)
    {
        Failure(useraccount, spanObj,"文件名中不能含有< > * / \ ? | :等字符！");
        return false;
    }
    else
    {
        Success(useraccount, spanObj)
        return true;
    }
}