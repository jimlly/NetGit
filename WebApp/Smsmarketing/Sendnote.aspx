<%@ Page Language="C#" AutoEventWireup="true" Inherits="Smsmarketing_Sendnote" Codebehind="Sendnote.aspx.cs" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>短信营销－明细列表</title>
<link href="../Images/content.css" rel="stylesheet" type="text/css" />
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
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#EAF2FB">
  <tr>
    <td width="2%" style="height: 31px">&nbsp;</td>
    <td width="83%" class="f15" id="f15" style="height: 31px"> 短信营销－明细列表</td>
    <td width="15%" class="f15" id="Td1" style="height: 31px"><table border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td align="right" style="height: 16px; width: 17px;"><a href="SendView.aspx"><img src="../Images/comeback.gif" width="16" height="16" alt="返回" border="0"/></a></td>
        <td align="left" id="Td2" style="height: 16px;font-size: 14px;font-weight: bold;"><a href="SendView.aspx" border="0" style="height: 16px;font-size: 14px;font-weight:bold;color: #1F4A65;"> 返回</a></td>
      </tr>
    </table></td>
  </tr>
</table>
<table width="100%" height="3" border="0" cellpadding="0" cellspacing="0">
  <tr>
      <td>
       <asp:GridView ID="smsList" runat="server" GridLines="None"  HeaderStyle-CssClass="header_td_downline" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnRowDataBound="smsList_RowDataBound1" >
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
                      <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="gridview_td_downline" Width="25%"/>                        
                      <HeaderStyle CssClass="rightYline" HorizontalAlign="Left" Width="25%"/>                        
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="手机号码">
                        <ItemTemplate>
                            <%# Base.ShieldMobile(Eval("Mobile").ToString())%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"  VerticalAlign="Middle" CssClass="gridview_td_downline" Width="25%"/>
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="25%"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态">
                        <ItemTemplate>
                            <%# Base.GetSendStatus(Eval("State").ToString())%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left"  VerticalAlign="Middle" CssClass="gridview_td_downline" Width="25%"/>
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="25%"/>
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
        <asp:Literal ID="TipDiv" runat="server"></asp:Literal>&nbsp;<webdiyer:AspNetPager
            ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页"
            OnPageChanged="AspNetPager1_PageChanged" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Left"
            SubmitButtonText="GO" Width="101%">
        </webdiyer:AspNetPager>
        &nbsp;&nbsp;
    </form>
</body>
</html>