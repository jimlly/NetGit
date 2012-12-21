<%@ Page Language="C#" AutoEventWireup="true" Inherits="Manage_Default" Codebehind="Manage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>短信营销管理</title>
    <link href="../Images/content.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            &nbsp;<table style="width: 264px">
                <tr>
                    <td rowspan="3" style="width: 87px" valign="top">
                        <div style="width: 100px; height: 100px">
            <asp:Menu ID="Menu1" runat="server" Height="21px" Width="91px" OnMenuItemClick="Menu1_MenuItemClick">
                <Items>
                    <asp:MenuItem Text="发送状况" Value="0" Target=""></asp:MenuItem>
                    <asp:MenuItem Text="开放时间" Value="1" Target="OpenTime"></asp:MenuItem>
                    <asp:MenuItem Text="群发量控制" Value="2" Target="MaxCount"></asp:MenuItem>
                    <asp:MenuItem Text="关键字管理" Value="3" Target="KeyWords"></asp:MenuItem>
                </Items>
            </asp:Menu>
                        </div>
                    </td>
                    <td colspan="2" rowspan="2">
                        <div style="width: 100px; height: 100px">
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="SendStatus" runat="server">
                    <table width="400" height="200" cellpadding="0" cellspacing="0">
                        <tr valign="top">
                            <td class="TabArea" style="width: 179px">
                                <div style="width: 636px; height: 21px">
                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal></div>
                                <br />
                                <div style="width: 634px; height: 264px">
                                    <asp:GridView ID="gvStatus" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="TimeZone" HeaderText="时间段" />
                                            <asp:BoundField DataField="Succeed" HeaderText="成功数量" />
                                            <asp:BoundField DataField="Failed" HeaderText="失败数量" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="OpenTime" runat="server">
                    <table width="400" height="50" cellpadding="0" cellspacing="0">
                        <tr valign="top">
                            <td class="TabArea" style="width: 600px">
                                <div style="width: 362px; height: 24px">
                                    开放时间：每天
                                    <asp:DropDownList ID="ddlBeginTime" runat="server">
                                    </asp:DropDownList>
                                    到 &nbsp;<asp:DropDownList ID="ddlEndTime" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="MultiCount" runat="server">
                    <table width="400" height="50" cellpadding="0" cellspacing="0">
                        <tr valign="top">
                            <td class="TabArea" style="width: 179px; height: 78px;">
                                <div style="width: 472px; height: 26px">
                                    一次性提交短信量不超过<asp:TextBox ID="txtCount" runat="server" Width="81px"></asp:TextBox>条</div>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="KeyWords" runat="server">
                    <table width="400" height="200" cellpadding="0" cellspacing="0">
                        <tr valign="top">
                            <td class="TabArea" style="width: 600px">
                                <div style="width: 396px; height: 167px">
                                    <asp:GridView ID="gvKeyWords" runat="server" AutoGenerateColumns="False" Width="250px" OnRowCancelingEdit="gvKeyWords_RowCancelingEdit" OnRowDeleting="gvKeyWords_RowDeleting" OnRowEditing="gvKeyWords_RowEditing" OnRowUpdating="gvKeyWords_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField HeaderText="关键字">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem,"KeyWord")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtKeyWord" Text='<%# DataBinder.Eval(Container.DataItem,"KeyWord")%>' runat="server" Width="145px"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:CommandField HeaderText="操作" ShowEditButton="True" ShowDeleteButton="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="100px" />
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <div style="width: 328px; height: 23px">
                                        <asp:TextBox ID="txtAdd" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="添加" /></div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView></div>
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td colspan="2">
            <asp:Button ID="btnSubmit" Text="确定" runat="server" OnClick="btnSubmit_Click" Visible="False"></asp:Button></td>
                </tr>
            </table>
            &nbsp;&nbsp;
        </div>
    </form>
</body>
</html>
