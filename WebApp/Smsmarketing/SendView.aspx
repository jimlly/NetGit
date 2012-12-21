<%@ Page Language="C#" AutoEventWireup="true" Inherits="Smsmarketing_SendView" Codebehind="SendView.aspx.cs" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>短信营销－发件箱</title>
    <link href="../Images/content.css" rel="stylesheet" type="text/css" />
    <script defer="defer" src="../JS/SendList.js" type="text/javascript" language="javascript"></script>
    <script defer="defer" src="../JS/My97DatePicker/WdatePicker.js" type="text/javascript" language="javascript"></script>
    <style type="text/css">
.gridview_td_downline {
	border-bottom-width: 1px;
	border-bottom-style: solid;
	border-bottom-color: #D3D3D3;
}
.header_td_downline {
	 background-image:url(Images/tabColor.gif);
	 background:url(Images/tabColor.gif);
}
.rightYline {
	border-right-width: 1px;
	border-right-style: inset;
	border-right-color: #cccccc;
	background-image:url(../Images/tabColor.gif);
	border-bottom-width: 1px;
	border-bottom-style: solid;
	border-bottom-color: #D3D3D3;
	border-top-width: 1px;
	border-top-style: solid;
	border-top-color: #D3D3D3;

}
#f15{
	font-size: 14px;
	color: #1F4A65;
	font-weight: bold;
}
#f15 a{
	font-size: 14px;
	color: #1F4A65;
	font-weight: bold;
}
#f15 a:hover{
	font-size: 14px;
	color: #FF0000;
	font-weight: bold;
}
    </style>
    <script type="text/javascript" language="javascript">
    function redirect(id,status)
    {
        //5正在发送,//9发送完毕
        if( status == 5 || status == 9 )
        {
            window.location="Sendnote.aspx?MsgId="+id;
        }
        else
        {
        
        }
    }
    </script>
</head>
<body onload="onChange()">
    <form id="form1" runat="server" >
    <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#EAF2FB">
      <tr>
        <td width="2%">&nbsp;</td>
        <td width="83%" height="28" class="f15" id="f15"> 短信营销－发件箱</td>
        <td width="15%" class="f15" id="Td1">&nbsp;</td>
      </tr>
    </table> 
    <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="search">
                <tr>
                    <td class="search_list" style="height: 40px">
                        快速查询：<asp:DropDownList ID="txtSelectTime" runat="server" onchange="onChange()">
                            <asp:ListItem Value="1">当天</asp:ListItem>
                            <asp:ListItem Value="2">一周以内</asp:ListItem>
                            <asp:ListItem Value="3" Selected="True">一个月以内</asp:ListItem>
                            <asp:ListItem Value="4">任意时间段</asp:ListItem>
                        </asp:DropDownList>
                        短信内容<asp:TextBox ID="txtMessage" runat="server" Width="90px" onpropertychange="return CheckSub();"></asp:TextBox>
                    <span id ="PanelDate" style="display:none;">开始时间<input id="txtStartTime"  type="text" class="Wdate"  onpropertychange="return CheckDate();"   runat="Server" style="width: 80px"  readonly="readOnly"/>&nbsp;
                    结束时间<input id="txtEndTime"  type="text" class="Wdate"  onpropertychange="return CheckDate();"   runat="Server" style="width: 80px" readonly="readOnly"/></span>
                        <asp:Button ID="searchbut" runat="server" CssClass="blue" OnClientClick="return checkData()"
                            OnClick="searchbut_Click" Text="搜索" /></td>
                    <td align="left" style="color: #0a3c5c; text-decoration: underline" width="180">
                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="resf_Click"><img border="0" src="../Images/refresh.gif" alt="刷新"/></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
      <td>
       <asp:GridView ID="smsList" runat="server" HeaderStyle-CssClass="header_td_downline" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnRowDataBound="smsList_RowDataBound1" >
        <HeaderStyle CssClass="gridview_tr" BackColor="White" Font-Bold="True" ForeColor="Black" />
        <EmptyDataTemplate>
         <div style="vertical-align:top; border:none;">
                <div style="vertical-align:middle;text-align:center" >
                <strong style="font-size:15">没有任何记录！</strong>
                </div>          
            <emptydatarowstyle verticalalign="Top" />
        </div>
        </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField  HeaderText="发送时间" DataField="SendTime">
                      <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="gridview_td_downline" Width="15%"/>                        
                      <HeaderStyle CssClass="rightYline" HorizontalAlign="Left" Width="15%"/>                        
                    </asp:BoundField>
                     <asp:TemplateField HeaderText="信息内容">
                        <ItemTemplate>
                            <span title='<%# Eval("Message").ToString() %>'>
                                <%# Base.SubString(Eval("Message").ToString(),18) %>
                            </span>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"  VerticalAlign="Middle" CssClass="gridview_td_downline" Width="19%"/>
                         <HeaderStyle HorizontalAlign="Left" CssClass="rightYline"  Width="19%"/>
                     </asp:TemplateField>
                    <asp:TemplateField HeaderText="地区行业">
                           <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle" CssClass="gridview_td_downline" Width="19%"/>
                           <HeaderStyle HorizontalAlign="Center" CssClass="rightYline"  Width="19%"/>
                                <ItemTemplate>
                                   
                                </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:BoundField HeaderText="成功数量" DataField="SuccessCounts">
                        <ItemStyle HorizontalAlign="Left"  VerticalAlign="Middle" CssClass="gridview_td_downline" Width="8%"/>
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="8%"/>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="失败数量" DataField="FailedCounts">
                        <ItemStyle HorizontalAlign="Left"  VerticalAlign="Middle" CssClass="gridview_td_downline" Width="8%"/>
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="8%"/>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="总数量" DataField="SendCounts">
                        <ItemStyle HorizontalAlign="Left"  VerticalAlign="Middle" CssClass="gridview_td_downline" Width="6%"/>
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="6%"/>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="金额(元)" DataField="UsedValue">
                        <ItemStyle HorizontalAlign="Left"  VerticalAlign="Middle" CssClass="gridview_td_downline" Width="8%"/>
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="8%"/>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="状态">
                           <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle" CssClass="gridview_td_downline" Width="9%"/>
                           <HeaderStyle HorizontalAlign="Center" CssClass="rightYline"  Width="9%"/>
                                <ItemTemplate>
                                    <%# Base.GetStatus(Eval("SendStatus").ToString())%>
                                </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="详细记录" >
                        <ItemTemplate>
                            <a style="text-align:center;border:0" onclick="redirect('<%# AddrPublic.md5Encrypt(Eval("ID").ToString()) %>',<%# Eval("SendStatus") %>)" ><img src="<%# Base.getPicUrl(Eval("SendStatus").ToString()).ToString() %>" alt="详细记录" border="0" /></a>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"  VerticalAlign="Middle" CssClass="gridview_td_downline" Width="8%"/>
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="8%"/>
                    </asp:TemplateField>
                </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <EmptyDataRowStyle Font-Bold="True" HorizontalAlign="Center" />
            </asp:GridView>
      </td> 
  </tr>
</table>
       <asp:Literal ID="TipDiv" runat="server"></asp:Literal>&nbsp; <webdiyer:AspNetPager
            ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页"
            OnPageChanged="AspNetPager1_PageChanged" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Left"
            SubmitButtonText="GO" Width="101%" ShowInputBox="Always">
        </webdiyer:AspNetPager>
    
    </form>
</body>
</html>
