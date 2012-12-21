// JScript 文件

//#region
//#endregion
function ShowContactorDialog()
{
    var width = 360;
    var height = 335;
    var url = "../AddressBook/GroupPerson.aspx";
    var style = 'status:no;menu:no;help:0;dialogWidth:'+width+'px ; dialogHeight:'+height+'px';
  	var val = window.showModalDialog(url, window,style);
  	if(val.length == "undefined")
  	{}
  	else
  	{
        var val1 = document.getElementById("txtMobileNums").value;
        if(val1.length > 0)
        {
            if(val1.endsWith(";"))
            {
                val1 += "\r\n";
            }
            else if(val1.endsWith(";\r\n"))
            {}
            else
            {
                val1 += ";\r\n";
            }
        }
        document.getElementById("txtMobileNums").value = val1 + val;
  	}
}

function GetReturnValue()
{
    var returnval = "";
    var gridview = document.getElementById("gvGroupList");
    var isAll = document.getElementById("chbAll").checked;
    for(var i = 1;i < gridview.rows.length;i++)
    {
        var checkbox = gridview.rows[i].getElementsByTagName("INPUT"); 
        if(isAll)
        {
                returnval = returnval + gridview.rows[i].cells[2].innerHTML + "," + gridview.rows[i].cells[1].innerHTML + ";\r\n";
        }
        else
        {
            if(checkbox[0].checked)
            {
                returnval = returnval + gridview.rows[i].cells[2].innerHTML + "," + gridview.rows[i].cells[1].innerHTML + ";\r\n";
            }
        }
    }
    if(returnval.length == 0)
    {
        alert('请选择联系人!');
    }
    else
    {
        window.returnValue = returnval.substring(0,returnval.length - 1); 
        window.close(); 
    }
}