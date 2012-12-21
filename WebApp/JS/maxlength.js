
function onmyinput(o,maxLength)  
{  
if(o.value.length>= maxLength)  
{  
if(o.value.length> maxLength)  
value = o.value.substring(0,maxLength);  
return false;  
}  
return true;  
}  
function mygetclipdata()  
{  
if(!document.all)  
{  
netscape.security.PrivilegeManager.enablePrivilege('UniversalXPConnect');  
var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);  
var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);  
trans.addDataFlavor('text/unicode');  
var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);  
clip.getData(trans,clip.kGlobalClipboard);  
var str=new Object();  
var strLength=new Object();  
trans.getTransferData("text/unicode",str,strLength);   
if (str)  
str=str.value.QueryInterface(Components.interfaces.nsISupportsString);  
var pastetext;  
if (str)  
pastetext=str.data.substring(0,strLength.value / 2);  
return pastetext;  
}  
else
{  
return window.clipboardData.getData("Text");  
}  
}  
function mysetclipdata(o)  
{  
if(!document.all)  
{  
netscape.security.PrivilegeManager.enablePrivilege('UniversalXPConnect');  
var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);  
var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);  
trans.addDataFlavor("text/unicode");  
var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);  
str.data=o;  
trans.setTransferData("text/unicode",str,o.length*2);  
var clipid=Components.interfaces.nsIClipboard;  
clip.setData(trans,null,clipid.kGlobalClipboard);  
}  
else
{  
window.clipboardData.setData("Text",o);  
}  
}  
function onmypaste(o, maxLength)  
{  
var nMaxLen=o.getAttribute? parseInt(maxLength):"";  
   
if(!document.all)  
{  
// alert("不可能执行的代码");  
}  
else
{  

if(document.selection.createRange().text.length>0)  
{  
   var ovalueandclipboarddata = o.value +window.clipboardData.getData("Text");  
   if(o.getAttribute && ovalueandclipboarddata.length-document.selection.createRange().text.length>nMaxLen)  
   {  
    if(window.clipboardData.getData("Text").substring(0,document.selection.createRange().text.length+nMaxLen-o.value.length)!="")  
     window.clipboardData.setData("Text",window.clipboardData.getData("Text").substring(0,document.selection.createRange().text.length+nMaxLen-o.value.length));  
    else
     return false;  
   }  
}  
else
{  
   var ovalueandclipboarddata = o.value +window.clipboardData.getData("Text");  
   if(o.getAttribute && ovalueandclipboarddata.length>nMaxLen)  
   {  
    if(ovalueandclipboarddata.substring(0,nMaxLen-o.value.length)!="")  
     window.clipboardData.setData("Text",ovalueandclipboarddata.substring(0,nMaxLen-o.value.length));  
    else
     return false;  
   }  
}  
return true;  
}  
}  
function onmykeypress(o, maxLength)  
{  
if(!document.all)  
{  
var nMaxLen=o.getAttribute? parseInt(maxLength):"";  

if(onmykeypress.caller.arguments[0].ctrlKey == true)  
{  
   if(onmykeypress.caller.arguments[0].which==118)  
   {  

    if(o.selectionStart<o.selectionEnd)  
    {  
     var ovalueandclipboarddata = o.value + mygetclipdata();  
     if(o.getAttribute && (ovalueandclipboarddata.length-o.selectionEnd + o.selectionStart>nMaxLen))  
     {  
      if(mygetclipdata().substring(0,o.selectionEnd - o.selectionStart+nMaxLen-o.value.length)!="")  
       mysetclipdata(mygetclipdata().substring(0,o.selectionEnd - o.selectionStart+nMaxLen-o.value.length));  
      else
       return false;  
     }  
    }  
    else
    {  
     var ovalueandclipboarddata = o.value +mygetclipdata();  
     if(o.getAttribute && ovalueandclipboarddata.length>nMaxLen)  
     {  
      if(ovalueandclipboarddata.substring(0,nMaxLen-o.value.length)!="")  
       mysetclipdata(ovalueandclipboarddata.substring(0,nMaxLen-o.value.length));  
      else
       return false;  
     }  
    }  
    return true;  

   }  
}  


if(onmykeypress.caller.arguments[0].which==0 || onmykeypress.caller.arguments[0].which==8)  
   return true;  
if(o.value.length>= maxLength)  
{  
   if(o.selectionStart<o.selectionEnd)  
    return true;  
   if(o.value.length> maxLength)  
    o.value = o.value.substring(0, maxLength);  
   return false;  
}  
else
   return true;  
   
}  
else
{  
if(document.selection.createRange().text.length>0)  
   return true;  
if(o.value.length>= maxLength)  
   return false;  
else
   return true;  
}  
}   

window.onload = init_MaxLength; 
function init_MaxLength ()   
{  
var textAreaObj = document.getElementsByTagName("textarea");  
var maxLength;  
for (var i = 0; i < textAreaObj.length; i++) {  
      maxLength = textAreaObj[i].getAttribute("maxLength") == null ? 0 : textAreaObj[i].getAttribute("maxLength");  
      if (maxLength == 0) continue;  
          textAreaObj[i].onpaste = function(){ return onmypaste(this,maxLength)};  
          textAreaObj[i].onkeypress = function(){ return onmykeypress(this,maxLength)};  
          
}  
}