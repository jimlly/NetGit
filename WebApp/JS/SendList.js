// JScript 文件

function onChange()
    {
       var obj = document.getElementById("txtSelectTime"); 
       if(obj.selectedIndex != 3)
       {
           document.getElementById('txtStartTime').onfocus = "";
           document.getElementById('txtEndTime').onfocus="";
           document.getElementById('txtStartTime').value = "";
           document.getElementById('txtEndTime').value="";
           document.getElementById('PanelDate').style.display="none";
       }
       else
       {
           document.getElementById('txtStartTime').onfocus = selectDate;
           document.getElementById('txtEndTime').onfocus = selectDate;
           document.getElementById('PanelDate').style.display="";
       }
    }
    
function selectDate()
    {
      WdatePicker({skin:'whyGreen'})
    } 
    
function CheckSub()
    {
        var subject_= document.forms[0].txtMessage.value; 
        if(subject_ != "")
        {
            if (contain2(subject_, "~!#$%^&*()><\/'|"))
            { 
              alert("短信内容输入了非法字符!");
              document.forms[0].txtMessage.value="";
              return false;
            }
        }
  }

  function CheckUserName() {
      var subject_ = document.forms[0].txtUserName.value;
      if (subject_ != "") {
          if (contain2(subject_, "~!#$%^&*()><\/'|")) {
              alert("用户名输入了非法字符!");
              document.forms[0].txtUserName.value = "";
              return false;
          }
      }
  }
    
function contain(str)
    {
        //要屏蔽掉的特殊字符
        var specialCharacter = " \~\!\@\\#\$\%\^\&\*\(\)\'\>\<\\\/\|"; 
        var i;
        for(i=0;i<specialCharacter.length;i++){
            if(str.indexOf(specialCharacter.charAt(i)) >= 0)
            {
                return true;
            }
        }
    }

    function contain2(str) {
        //要屏蔽掉的特殊字符
        var specialCharacter = " \~\!\\#\$\%\^\&\*\(\)\'\>\<\\\/\|";
        var i;
        for (i = 0; i < specialCharacter.length; i++) {
            if (str.indexOf(specialCharacter.charAt(i)) >= 0) {
                return true;
            }
        }
    }
    
function CheckDate()
    {
       if(document.all.item("txtSelectTime").value == "4")
       {
          var start = document.getElementById('txtStartTime');
          var end =  document.getElementById('txtEndTime');
          if(document.forms[0].txtStartTime.value > document.forms[0].txtEndTime.value  && start.value != "" && end.value != "")
          {
             alert("结束日期不能小于开始日期！");
             document.forms[0].txtEndTime.value="";
             return false;
          }   
       }
    } 
    
function checkData()
    {
        if(document.getElementById("txtSelectTime").value == "4")
        {
            var start = document.getElementById('txtStartTime');
            var end =  document.getElementById('txtEndTime');
            if(start.value==""&&end.value=="")
            {
                alert("请选择时间段！");
                return false;
            }
            if(start.value > end.value  && start.value != "" && end.value != "")
            {
                alert("注意：开始时间不能大于结束时间！");
                return false;
            }   
        }
    }     
    
     //全选
function CheckAll(obj)
    {
        var objs = document.getElementsByTagName("input");	            
        for(var i=objs.length-1; i>=0; i--) 
        {            
            var e=objs[i];
            if(e.type=='checkbox')
            {
                if (e.name != "chkAll" && e.name != "chbAll" &&e.disabled==false)
                {
                    e.checked = obj.checked;
                }
            }             
        }     
    }
 
function confirmSelect() 
    {
         var obj = document.getElementById("selectArea"); 
         var objs = document.getElementsByTagName("input");
         var Flag=false;
         var isselect =false;
            
        for(var i=objs.length-1; i>=0; i--) 
        {
             var CurrCheckbox=objs[i];
            if(CurrCheckbox.type.toLowerCase() == "checkbox" )              
            {
                if(CurrCheckbox.checked)
                {
                    var state=objs[i].parentNode.parentNode.cells[12].innerHTML
                    if(state!="9" && state!="20" && state!="21" && state!="4"){
                        Flag=true;
                        break;
                    }
                    isselect = true;
                 }
            } 
        }
        if(Flag)
        {
            alert("您选择要批量删除的记录某些正在处理中，不允许此操作！");
            return false;
        }
        if(!isselect)
        {
            alert("请从上面列表中选择要批量删除的记录！");
            return false;
        }else if(isselect){
            return confirm("您真的要批量删除上面列表中选择的记录吗？");
        }else{
            return confirm("您真的要批量删除全部记录吗？");
        }
    }
    
function confirmSelect_() 
    {
        var obj = document.getElementById("selectArea"); 
        var objs = document.getElementsByTagName("input");
        var isselect =false;
        var Flag=false;    
            for(var i=objs.length-1; i>=0; i--) 
            {
                 var CurrCheckbox=objs[i];
                if(CurrCheckbox.type.toLowerCase() == "checkbox" )              
                {
	                if(CurrCheckbox.checked)
	                {
	                    var state=objs[i].parentNode.parentNode.cells[12].innerHTML
	                    if(state!="9" && state!="4"){
	                        Flag=true;
	                        break;
	                    }
	                    isselect = true;
	                 }
                } 
            }
            if(Flag)
            {
                alert("您选择要批量重发的记录某些正在处理中，不允许此操作！");
                return false;
            }
            if(!isselect)
            {
            alert("请从列表中选择要批量重发的记录！");
            return false;
            }else if(isselect) {
              return confirm("您真的要批量重发列表中选择的记录吗？");
            }else{
              return confirm("您真的要批量重发全部记录吗？");
            }
    }
    
function confirmSelect__() 
    {
        var obj = document.getElementById("selectArea"); 
        var objs = document.getElementsByTagName("input");
        var isselect =false;
        var Flag=false; 
        for(var i=objs.length-1; i>=0; i--) 
        {
            var CurrCheckbox=objs[i];
            if(CurrCheckbox.type.toLowerCase() == "checkbox" )              
            {
                if(CurrCheckbox.checked)
                {
                    var state=objs[i].parentNode.parentNode.cells[12].innerHTML
                    if(state!="9" && state!="20" && state!="21" && state!="4")
                    {
                        Flag=true;
                        break;
                    }
                    isselect = true;
                }
            } 
        }
        if(Flag)
        {
            alert("您选择要批量放入回收站的记录某些正在处理中，不允许此操作！");
            return false;
        }
        if(!isselect)
        {
            alert("请从上面列表中选择要批量放入回收站的记录！");
            return false;
        }
        else if(isselect)
        {
             return confirm("您真的要把列表中选择的记录放入回收站吗？");
        }
        else
        {
             return confirm("您真的要批量放入回收站全部记录吗？");
        } 
     }
    
function confirmBatchApproveMsg() 
    {
        var obj = document.getElementById("selectArea"); 
        var objs = document.getElementsByTagName("input");
        var isselect =false;
            for(var i=objs.length-1; i>=0; i--) 
            {
                 var CurrCheckbox=objs[i];
                if(CurrCheckbox.type.toLowerCase() == "checkbox" && CurrCheckbox.name != "chkAll")              
                {
	                if(CurrCheckbox.checked)
	                {
	                    isselect = true;
	                        break;
	                 }
                } 
            }
            if(!isselect)
            {
            alert("请从列表中选择要批量审核的记录！");
            return false;
            }
            else{
              return confirm("您确定要批量审核列表中选择的记录吗？");
            }
    }
     
function SingleResend(obj)
    {
        var state = obj.parentNode.parentNode.cells[12].innerHTML;
        var msgID = obj.parentNode.parentNode.cells[1].innerHTML;
        var result = SmsAjax.CanResend(msgID).value;
        if(result == 0)
        {
            alert("未发送完毕的记录，不允许此操作！具体请参照详细记录");
            return false;
        }
        if(parseInt(state)!=9)
        {
            alert("该记录正在处理中，不允许此操作！");
            return false;
        }
        else
        {
            return confirm("您真的要重发此条群发录吗？");
        }
    }
    
function SingleGabage(obj)
    {
        var state = obj.parentNode.parentNode.cells[12].innerHTML;
        if(parseInt(state)!=9 && parseInt(state)!=20 && parseInt(state)!=21 && parseInt(state)!=4)
        {
            alert("该记录正在处理中，不允许此操作！");
            return false;
        }
        else
        {
            return confirm("您真的要将此条记录放入垃圾箱吗？");
        }
    }

function SingleDelete(obj)
    {
        var state = obj.parentNode.parentNode.cells[12].innerHTML;
        if(parseInt(state)!=9 && parseInt(state)!=20 && parseInt(state)!=21 && parseInt(state)!=4)
        {
            alert("该记录正在处理中，不允许此操作！");
            return false;
        }
        else
        {
            return confirm("您真的要删除此条群发录吗？");
        }
    }
        
function returnDelete()
    {
        window.self.location= "../SendMsg/SendList.aspx?IsDelete="+document.getElementById("hdIsDelete").value;
    }

