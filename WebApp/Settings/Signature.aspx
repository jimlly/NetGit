<%@ Page Language="C#" AutoEventWireup="true" Inherits="Settings_Signature" CodeBehind="Signature.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>����ǩ������</title>
    <link href="../Images/content.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../Images/list.js"></script>
    <%--    <script language="JavaScript" type="text/javascript" src="../Js/Signatrue.js"></script>--%>
    <style type="text/css">
<!--
.STYLE9 {
	font-size: 14px;
	color: #000000;
	font-weight: bold;
}.STYLE10 {
	font-size: 12px;
	color: #000000;
	background-color: #F0F7FF;
	border: 1px solid #637EAB;
}
.STYLE10 a {
	font-size: 12px;
	color: #CE0000;
}
.hidden { display:none;}
-->
</style>
    <script type="text/javascript">

        var strReg = /^[^@\%\~\*\<\>\`\&\'\"]+$/;
        ///^[^@\/\'\\\"#$%&\^\*]+$/;
        function CheckInput() {
            var content = document.getElementById("txtContent").value.replace(/(^\s*)|(\s*$)/g, "");
            if (content == "") {
                alert('����дǩ������');
                return false;
            }
            else {
                if (content.length < 2 || content.length > 4) {
                    alert('ǩ�����ݵĳ�����2-4���ַ�֮�䣡');
                    document.getElementById("txtContent").focus();
                    return false;
                }
                else {
                    if (!strReg.test(content)) {
                        alert("ǩ�����ݲ��ܰ����磺��\%����\~����\*����\<����\>����\`����\&����\'����\"�����ַ�");
                        return false;
                    }
                }
            }
            //    var result = Settings_Signature.AddSignatrue(title,content).value;
            //    if(result == 1)
            //    {
            //        alert('�����ɹ���');
            //        document.getElementById("txtTitle").length = 0;
            //        document.getElementById("txtTitle").length = 0;
            //    }
            //    else
            //    {
            //        alert('����ʧ�ܣ�');
            //    }
        }

        function Cancel() {
            document.getElementById("txtContent").value = "";
        }

        function DeleteConfirm() {
            return confirm("��ȷ��Ҫɾ������ǩ����");
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="left">
        <tr>
            <td style="width: 444px">
                <table width="600" border="0" align="left" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="height: 1px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="102" align="left" valign="top" style="width: 300px">
                <table width="600" border="0" cellpadding="5" cellspacing="0" >
                     <tr>
                        <td width="70" align="left" valign="bottom" class="STYLE9">
                            ����ǩ��
                        </td>
                        <td width="510" align="left" valign="bottom">
                            ( ����������10��)
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" valign="top">
                            <table width="100%" border="0" cellpadding="5" cellspacing="0" class="STYLE10">
                                <%--<tr>
                                        <td width="14%" align="right">
                                            <strong>ǩ�����⣺</strong></td>
                                        <td width="86%">
                                            <asp:TextBox ID="txtTitle" runat="server" Width="355px"></asp:TextBox>
                                            <span style="color: #ff3300">*</span> ���10���ַ�����</td>
                                    </tr>--%>
                                <tr>
                                    <td align="right">
                                        <strong>ǩ�����ݣ�</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox name="textarea" runat="server" ID="txtContent" Width="355px"></asp:TextBox><span
                                            style="color: #ff0000" >&nbsp;*</span> 2-4���ַ�
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" valign="top">
                            <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" Text="�ύ" />&nbsp;&nbsp;
                            <input id="btnCancel" type="button" value="���" onclick="Cancel()" />
                        </td>
                    </tr>
                    <tr>
                        <td height="31" colspan="2" align="left" valign="bottom">
                            <strong>����ǩ����</strong>
                        </td>
                    </tr>
                    <tr>
                        <td height="35" colspan="2" align="left" class="STYLE10">
                            <asp:GridView ID="gvSignatrue" runat="server" Width="588px" AutoGenerateColumns="False"
                                OnRowCommand="gvSignatrue_RowCommand" OnRowDeleting="gvSignatrue_RowDeleting"
                                GridLines="None" OnRowEditing="gvSignatrue_RowEditing" ShowHeader="False" OnRowDataBound="gvSignatrue_RowDataBound">
                                <Columns>
                                    <asp:BoundField HeaderText="����" ShowHeader="False">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SignContent" HeaderText="����" ShowHeader="False">
                                        <ItemStyle Width="400px" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEdit" runat="server" CommandName="Edit">�޸�</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("ID")%>'
                                                OnClientClick="return DeleteConfirm()">ɾ��</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ID" ShowHeader="False">
                                        <FooterStyle CssClass="hidden" />
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SignContent" ShowHeader="False">
                                        <FooterStyle CssClass="hidden" />
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:BoundField>
                                </Columns>
                                <SelectedRowStyle BackColor="White" />
                            </asp:GridView>
                            <asp:HiddenField ID="hfID" runat="server" />
                            <%--<asp:Table ID="tblSignList" runat="server" Width="589px">
                                    <asp:TableRow runat="server">
                                        <asp:TableCell Width="0px" runat="server"></asp:TableCell>
                                        <asp:TableCell runat="server"></asp:TableCell>
                                        <asp:TableCell runat="server"></asp:TableCell>
                                        <asp:TableCell Width="30">
                                            <span onclick="">�޸�</span>
                                        </asp:TableCell>
                                        <asp:TableCell Width = "30">
                                            <asp:HyperLink runat="server">ɾ��</asp:HyperLink>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server">
                                        <asp:TableCell Width="0px" runat="server"></asp:TableCell>
                                        <asp:TableCell runat="server"></asp:TableCell>
                                        <asp:TableCell runat="server"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server">
                                        <asp:TableCell Width="0px" runat="server"></asp:TableCell>
                                        <asp:TableCell runat="server"></asp:TableCell>
                                        <asp:TableCell runat="server"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
