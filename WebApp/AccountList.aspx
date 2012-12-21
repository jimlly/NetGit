<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountList" Codebehind="AccountList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户金额账户信息</title>    
    <link href="Styles/table.css" type="text/css"  rel="Stylesheet" /> 
    <script type="text/javascript">
        function ApplyStyle(s) {
            document.getElementById("mytab").className = s.innerText;
        }
    </script>  
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div><h4>您的金额账户详情如下：</h4></div>
        <div>         
        <asp:Repeater ID="rpt" runat="server">
        <HeaderTemplate>
        <table width="99%" id="mytab"  border="1" class="t1">
            <thead>
                <th width="18%">
                    账户名称
                </th>
                <th width="15%">
                    折扣（%）
                </th>
                <th width="20%">
                    账户余额（元）
                </th>
                <th width="25%" >
                    付费类别
                </th >                            
                <th width="21%">
                    到期日
                </th>
            </thead>
            <tbody>
        </HeaderTemplate>
        <ItemTemplate>           
            <tr  class="a1"> 
                <td width="18%">
                    <%#Eval("NickName") %>
                </td>
                <td width="15%">
                    <%#Eval("Percents") %>%
                </td>
                <td width="20%">
                    <%# Global.ChangeRateFromDb(((long)Eval("Balance")+(long)Eval("DonatedBalance"))) %>
                </td>
                <td width="25%">
                    <%#Eval("FeeTypeIndexDesc") %>
                </td>                            
                <td  width="21%">
                    <%# (short)Eval("HaveTerm")==1 ? Eval("EndDate") : "无期限"%>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></tbody><tfoot><tr><td colspan='5'>共<%=totalcount%>条记录　　<%= msg %></td></tr></tfoot></table></FooterTemplate>
        </asp:Repeater>        
        </div>        
    </div>
    </form>
</body>
</html>
