<%@ Page Language="C#" AutoEventWireup="true" Inherits="SendMsg_SendDetailList" Codebehind="SendDetailList.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../Images/content.css" rel="stylesheet" type="text/css" />
    <script defer="defer" src="../JS/SendList.js" type="text/javascript" language="javascript"></script>
    <style type="text/css">
    .hidden { display:none;}
    .gridview_td_downline {
	    border-bottom-width: 1px;
	    border-bottom-style: solid;
	    border-bottom-color: #D3D3D3;
    }
    .header_td_downline {
	     background-image:url(../Images/tabColor.gif);
	     background:url(../Images/tabColor.gif);
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
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#EAF2FB">
      <tr>
        <td width="2%">&nbsp;</td>
        <td width="83%" height="28" class="f15" id="f15">短信群发-明细列表</td>
        <td width="15%" class="f15" id="Td1"><img src="../Images/comeback.gif" alt="" /><a href="javascript:returnDelete();" style="font-size:120%;vertical-align:top;">返回</a></td>
      </tr>
    </table> 
<table width="100%" height="3" border="0" cellpadding="0" cellspacing="0">
        <div>
            <asp:GridView ID="GroupSendDetailList" runat="server" Width="100%" AutoGenerateColumns="False"
                BorderWidth="1px" DataKeyNames="MsgID" GridLines="Horizontal" Style="vertical-align: middle;
                text-align: center" BackColor="White" BorderColor="Silver" ForeColor="Black" HeaderStyle-CssClass="header_td_downline" CellPadding="4" OnRowDataBound="GroupSendDetailList_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="MsgID" HeaderText="批次号">
                      <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="gridview_td_downline" Width="20%"/>                        
                      <HeaderStyle CssClass="rightYline" HorizontalAlign="Left" Width="15%"/>        
                    </asp:BoundField>
                    <asp:BoundField DataField="Mobile" HeaderText="手机号码">
                      <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="gridview_td_downline" Width="20%"/>                        
                      <HeaderStyle CssClass="rightYline" HorizontalAlign="Left" Width="15%"/>        
                    </asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="收件人">
                      <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="gridview_td_downline" Width="20%"/>                        
                      <HeaderStyle CssClass="rightYline" HorizontalAlign="Left" Width="15%"/>        
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="发送时间">
                        <ItemTemplate>
                            <%# Base.GetCurrentTime(Eval("SendTime").ToString())%>
                        </ItemTemplate>
                      <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="gridview_td_downline" Width="20%"/>                        
                      <HeaderStyle CssClass="rightYline" HorizontalAlign="Left" Width="15%"/>        
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态">
                        <ItemTemplate>
                            <%# Base.GetSendStatus(Eval("State").ToString())%>
                        </ItemTemplate>
                      <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="gridview_td_downline" Width="20%"/>                        
                      <HeaderStyle CssClass="rightYline" HorizontalAlign="Left" Width="15%"/>        
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="gridview_tr" BackColor="White" />
                <EmptyDataTemplate>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
</table>
        <asp:Literal ID="TipDiv" runat="server"></asp:Literal>&nbsp;<webdiyer:AspNetPager
            ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页"
            OnPageChanged="AspNetPager1_PageChanged" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Left"
            SubmitButtonText="GO" Width="101%">
        </webdiyer:AspNetPager>
        <asp:HiddenField ID="hdIsDelete" runat="server" Value="0" />
        <asp:HiddenField ID="hdTotalRows" runat="server" Value="0" />
    </form>
</body>
</html>
