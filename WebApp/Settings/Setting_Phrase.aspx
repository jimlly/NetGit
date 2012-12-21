<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Setting_Phrase.aspx.cs"
    Inherits="WebApp.Settings.Setting_Phrase" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>���ö�������</title>
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
                alert('����д��������');
                return false;
            }
            else {
                if (content.length > 50) {
                    alert('�������ݵĳ��Ȳ��ܳ���50���ַ���');
                    return false;
                }
                else {
                    if (!strReg.test(content)) {
                        alert("�������ݲ��ܰ����磺��\%����\~����\*����\<����\>����\`����\&����\'����\"�����ַ�");
                        return false;
                    }
                }
                return true;
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
            return confirm("��ȷ��Ҫɾ������������");
        }

    </script>
    <script type="text/javascript">
        function immediately() {
            var element = document.getElementById("txtContent");
            if ("\v" == "v") {
                element.onpropertychange = webChange;
            } else {
                element.addEventListener("input", webChange, false);
            }
            function webChange() {
                if (element.value) {
                    if (element.value.length > 50) {
                        //element.value = element.value.substr(0, 4);
                        document.getElementById("txtContent").value = element.value.substr(0, 50);
                    }
                };
            }
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
                <table width="600" border="0" cellpadding="5" cellspacing="0" style="height: 240px">
                    <tr>
                        <td width="70" align="left" valign="bottom" class="STYLE9">
                            ���ö���
                        </td>
                        <td width="510" align="left">
                            ( ����������10��)
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" valign="bottom">
                            <table width="100%" height="120" border="0" cellpadding="5" cellspacing="0" class="STYLE10">
                                <tr>
                                    <td height="92" align="right">
                                        <strong>�������ݣ�</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox name="textarea" runat="server" ID="txtContent" Height="80px" MaxLength="50" TextMode="MultiLine"
                                            Width="355px"></asp:TextBox>&nbsp; <span style="color: #ff0000">*</span> ���50���ַ�����
                                    </td>
                                    <script type="text/javascript">
                                        immediately();
                                    </script>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" valign="bottom">
                            <asp:Button ID="btnOK" runat="server" Text="�ύ" OnClick="btnOK_Click" />&nbsp;&nbsp;
                            <input id="btnCancel" type="button" value="���" onclick="Cancel()" />
                        </td>
                    </tr>
                    <tr>
                        <td height="31" colspan="2" align="left" valign="bottom">
                            <strong>���ж��</strong>
                        </td>
                    </tr>
                    <tr>
                        <td height="35" colspan="2" align="left" class="STYLE10">
                            <asp:GridView ID="gvPhrase" runat="server" Width="588px" AutoGenerateColumns="False"
                                GridLines="None" ShowHeader="False" OnRowCommand="gvPhrase_RowCommand" OnRowEditing="gvPhrase_RowEditing"
                                OnRowDataBound="gvPhrase_RowDataBound" 
                                onrowdeleting="gvPhrase_RowDeleting">
                                <Columns>
                                    <asp:BoundField HeaderText="����" ShowHeader="False">
                                        <ItemStyle Width="50px" />
                                    </asp:BoundField>
                                    <asp:TemplateField ItemStyle-Width="300">
                                        <ItemTemplate>
                                           <span title='<%# Eval("Phrase").ToString()%>'> <%# Eval("Phrase").ToString().Length > 20 ? Eval("Phrase").ToString().Substring(0, 20) + "..." : Eval("Phrase").ToString()%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="50" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEdit" runat="server" CommandName="Edit">�޸�</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="50">
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
                                    <asp:BoundField DataField="Phrase" ShowHeader="False">
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
