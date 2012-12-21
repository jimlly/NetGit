<%@ Page Language="C#" AutoEventWireup="true" Inherits="SendMsg_List" ErrorPage="~/SendMsg/SendList.aspx" Codebehind="SendList.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
<link href="../Images/content.css" rel="stylesheet" type="text/css" />

    <script defer="defer" src="../JS/SendList.js" type="text/javascript" language="javascript"></script>

    <script defer="defer" src="../JS/My97DatePicker/WdatePicker.js" type="text/javascript"
        language="javascript"></script>
        
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
    <script type="text/javascript" language="jscript">
    function NoneData()
    {
        document.getElementById("NoneData").style.display="none";
    }
    </script>
</head>
<body onload="onChange();">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#EAF2FB">
      <tr>
        <td width="2%">&nbsp;</td>
        <td width="83%" height="28" class="f15" id="f15"> 
            <asp:Literal ID="Literal1" runat="server"></asp:Literal></td>
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
        <div id="Div2" align="center" style="width:100%">
            <asp:GridView ID="GroupSendList" runat="server" Width="100%" AutoGenerateColumns="False"
                BorderWidth="0px" DataKeyNames="MsgID" OnRowCommand="GroupSendList_RowCommand"
                OnRowDataBound="GroupSendList_RowDataBound" OnRowCreated="GroupSendList_RowCreated"
                GridLines="Horizontal" OnRowDeleting="GroupSendList_RowDeleting" BackColor="White" CellPadding="0" ForeColor="Black" EmptyDataText="没有任何记录！" EmptyDataRowStyle-HorizontalAlign="center" EmptyDataRowStyle-Font-Bold="true">
                
                <Columns>
                    <asp:TemplateField HeaderText="全选">
                        <HeaderTemplate>
                            <input id="chkAll" name="chkAll" type="checkbox" onclick="CheckAll(this)" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="3%"/>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="gridview_td_downline" Width="3%"/>   
                    </asp:TemplateField>
                    <asp:BoundField DataField="MsgID" HeaderText="批次号">
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="8%"/>
                        <ItemStyle Width="100px" CssClass="gridview_td_downline" HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="信息内容">
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="8%"/>
                        <ItemStyle Width="230px" CssClass="gridview_td_downline" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <span title='<%# Eval("Message").ToString() %>'>
                                <%# Base.SubString(Eval("Message").ToString(),18) %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发送时间">
                        <ItemTemplate>
                            <%# Base.GetCurrentTime(Eval("SendTime").ToString())%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="8%"/>
                        <ItemStyle Width="100px" Wrap="False" CssClass="gridview_td_downline" HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="SendCounts" HeaderText="总数量">
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="6%"/>
                        <ItemStyle Width="100px" CssClass="gridview_td_downline" HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="发送结果">
                        <ItemTemplate>
                            <span title="发送成功/发送失败">
                                <%# Eval("Success").ToString() + " / " + Eval("Failed").ToString()%>
                            </span>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="8%"/>
                        <ItemStyle Width="70px" CssClass="gridview_td_downline" HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Amount" HeaderText="金额(元)">
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="8%"/>
                        <ItemStyle Width="100px" CssClass="gridview_td_downline" HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="状态">
                        <ItemTemplate>
                            <%# Base.GetStatus(Eval("State").ToString())%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="8%"/>
                        <ItemStyle Width="110px" CssClass="gridview_td_downline" HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="重发">
                        <ItemTemplate>
                            <asp:DropDownList ID="ResendState" runat="server" Width="80px">
                                <asp:ListItem Value="0">发送完成</asp:ListItem>
                                <asp:ListItem Value="2">发送成功</asp:ListItem>
                                <asp:ListItem Value="9">发送失败</asp:ListItem>
                            </asp:DropDownList>
                            <asp:LinkButton ID="lbResend" runat="server" CommandName="Resend" OnClientClick='return SingleResend(this)'
                                ToolTip=""><img src="../Images/resent.gif" style="border:0px" alt="只有状态为----处理完毕才能重发！"/></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="8%"/>
                        <ItemStyle Width="110px" Wrap="False" CssClass="gridview_td_downline" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="转发">
                        <ItemTemplate>
                            <a href="SendSms.aspx?Messge=<%# Page.Server.UrlEncode(Eval("Message").ToString()) %>">
                                <img src="../Images/edit.gif" style="border: 0px" alt="转发" /></a>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="4%"/>
                        <ItemStyle Width="70px" CssClass="gridview_td_downline" HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="垃圾箱">
                        <ItemTemplate>
                            &nbsp;<asp:LinkButton ID="lbDeleteToGabage" runat="server" CommandName="Gabage" OnClientClick='return SingleGabage(this)'
                                ToolTip=""><img src="../Images/dell.gif" style="border:0px" alt="只有状态为----处理完毕才能放入垃圾箱！" /></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="6%"/>
                        <ItemStyle Width="50px" CssClass="gridview_td_downline" HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="彻底删除">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbDelete" runat="server" CommandName="Delete" OnClientClick='return SingleDelete(this)'
                                ToolTip=""><img src="../Images/del.gif" style="border:0px" alt="只有状态为----处理完毕才能删除！"/></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="8%"/>
                        <ItemStyle Width="70px" CssClass="gridview_td_downline" HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="State">
                        <HeaderStyle CssClass="hidden" />
                        <ItemStyle CssClass="hidden" />
                        <FooterStyle CssClass="hidden" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="详细记录">
                        <ItemTemplate>
                            <a href="SendDetailList.aspx?MsgID=<%# Eval("MsgID") %>&&IsDelete=<%#Request.QueryString["IsDelete"]%>">
                                <img src="../Images/xiangxi3.gif" style="border: 0px" alt="详细记录" /></a>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" CssClass="rightYline" Width="8%"/>
                        <ItemStyle Width="70px" CssClass="gridview_td_downline" HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="gridview_tr" BackColor="White" Font-Bold="True" ForeColor="Black" />
                            <EmptyDataTemplate>
                                    <div style="vertical-align:top; border:none;">
                                        <div style="vertical-align:middle;text-align:center" >
                                        <strong style="font-size:15">没有任何记录！</strong>
                                        </div>          
                                    <emptydatarowstyle verticalalign="Top" />
                                    </div>
                            </EmptyDataTemplate>
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <EmptyDataRowStyle Font-Bold="True" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
        <asp:Literal ID="TipDiv" runat="server"></asp:Literal>&nbsp;<webdiyer:AspNetPager
            ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页"
            OnPageChanged="AspNetPager1_PageChanged" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Left"
            SubmitButtonText="GO" Width="101%">
        </webdiyer:AspNetPager>
        <%--        <div id="basicbar" style="width: 927px" >
            <ul>
                <li>状态选择：<asp:DropDownList ID="selectArea" runat="server" Width="80px">
                    <asp:ListItem Value="0">发送完成</asp:ListItem>
                    <asp:ListItem Value="2">发送成功</asp:ListItem>
                    <asp:ListItem Value="3">发送失败</asp:ListItem>
                </asp:DropDownList>
                    &nbsp;<asp:Button ID="batchdelete" runat="server" OnClick="batchdelete_Click" Text="批量删除"
                        OnClientClick='return confirmSelect()' ToolTip="只有上面列表记录状态为----发送完毕才能批量删除！" />
                    &nbsp;<asp:Button ID="BatchReSend" runat="server" OnClick="BatchReSend_Click" Text="批量重发"
                        ToolTip="只有上面列表记录状态为----发送完毕才能批量重发！" OnClientClick="return confirmSelect_()" />
                    &nbsp;
                    <asp:Button ID="BatchToGabage" runat="server" Text="垃圾回收" OnClick="BatchToGabage_Click"
                        ToolTip="只有上面列表记录状态为----发送完毕才能批量放入垃圾箱！" OnClientClick="return confirmSelect__()" /></li></ul>
        </div>--%>
        <div id ="NoneData">
        <table style="background-color:#F7F7F7; width:100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="9%" height="30" align="right" valign="middle" nowrap="nowrap">
                    &nbsp;</td>
                <td width="91%" align="right" valign="middle">
                    <%--状态选择：
                    <asp:DropDownList ID="selectArea" runat="server" Width="80px">
                        <asp:ListItem Value="0">发送完成</asp:ListItem>
                        <asp:ListItem Value="2">发送成功</asp:ListItem>
                        <asp:ListItem Value="3">发送失败</asp:ListItem>
                    </asp:DropDownList>--%>
                    &nbsp;<asp:Button ID="batchdelete" runat="server" OnClick="batchdelete_Click" Text="批量删除"
                        OnClientClick='return confirmSelect()' ToolTip="只有上面列表记录状态为----发送完毕才能批量删除！" />
                    &nbsp;<asp:Button ID="BatchReSend" runat="server" OnClick="BatchReSend_Click" Text="批量重发"
                        ToolTip="只有上面列表记录状态为----发送完毕才能批量重发！" OnClientClick="return confirmSelect_()" />
                    &nbsp;
                    <asp:Button ID="BatchToGabage" runat="server" Text="垃圾回收" OnClick="BatchToGabage_Click"
                        ToolTip="只有上面列表记录状态为----发送完毕才能批量放入垃圾箱！" OnClientClick="return confirmSelect__()" />
                </td>
            </tr>
        </table>
        </div>
        <asp:HiddenField ID="hdTotalRows" runat="server" Value="0" />
    </form>
</body>
</html>
