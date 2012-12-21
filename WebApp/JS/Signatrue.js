// JScript 文件
var strReg = /^[^@\/\'\\\"#$%&\^\*]+$/;

function CheckInput()
{
    var title = document.getElementById("txtTitle").value;
    if(title == "")
    {
        alert('请填写签名标题');
        return false;
    }
    else
    {
        if(title.length > 10)
        {
            alert('签名标题的长度不能超过10个字符！');
            return false;
        }
        else
        {
            if(!strReg.test(title))
            {
                alert('签名标题含有非法字符！');
                return false; 
            }
        }
    }
    var content = document.getElementById("txtContent").value;
    if(content == "")
    {
        alert('请填写签名内容');
        return false;
    }
    else
    {
        if(content.length > 50)
        {
            alert('签名内容的长度不能超过50个字符！');
            return false;
        }
        else
        {
            if(!strReg.test(content))
            {
                alert('签名内容含有非法字符！');
                return false; 
            }
        }
    }
}

function Cancel()
{
    document.getElementById("txtTitle").value = "";
    document.getElementById("txtContent").value = "";
}