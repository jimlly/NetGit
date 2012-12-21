<%@ Page Language="C#" AutoEventWireup="true" Inherits="SendMsg_CheckList" Codebehind="CheckList.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>短信营销审批</title>
    <link href="../Images/content.css" rel="stylesheet" type="text/css" />
    
    <script defer="defer" src="../JS/SendList.js" type="text/javascript" language="javascript" ></script>

    <script defer="defer" src="../JS/My97DatePicker/WdatePicker.js" type="text/javascript"
        language="javascript"></script>

    <script type="text/javascript" language="javascript">
        /**//*  
        *    消息构造  
        */  
        function CLASS_MSN_MESSAGE(id,width,height,caption,title,message,target,action){  
            this.id     = id;  
            this.title  = title;  
            this.caption= caption;  
            this.message= message;  
            this.target = target;  
            this.action = action;  
            this.width    = width?width:200;  
            this.height = height?height:120;  
            this.timeout= 150;  
            this.speed    = 20; 
            this.step    = 1; 
            this.right    = screen.width -1;  
            this.bottom = screen.height; 
            this.left    = this.right - this.width; 
            this.top    = this.bottom - this.height; 
            this.timer    = 0; 
            this.pause    = false;
            this.close    = false;
            this.autoHide    = true;
        }  
          
        /**//*  
        *    隐藏消息方法  
        */  
        CLASS_MSN_MESSAGE.prototype.hide = function(){  
            if(this.onunload()){  

                var offset  = this.height>this.bottom-this.top?this.height:this.bottom-this.top; 
                var me  = this;  

                if(this.timer>0){   
                    window.clearInterval(me.timer);  
                }  

                var fun = function(){  
                    if(me.pause==false||me.close){
                        var x  = me.left; 
                        var y  = 0; 
                        var width = me.width; 
                        var height = 0; 
                        if(me.offset>0){ 
                            height = me.offset; 
                        } 
             
                        y  = me.bottom - height; 
             
                        if(y>=me.bottom){ 
                            window.clearInterval(me.timer);  
                            me.Pop.hide();  
                        } else { 
                            me.offset = me.offset - me.step;  
                        } 
                        me.Pop.show(x,y,width,height);    
                    }             
                }  

                this.timer = window.setInterval(fun,this.speed)      
            }  
        }  
          
        /**//*  
        *    消息卸载事件，可以重写  
        */  
        CLASS_MSN_MESSAGE.prototype.onunload = function() {  
            return true;  
        }  
        /**//*  
        *    消息命令事件，要实现自己的连接，请重写它  
        *  
        */  
        CLASS_MSN_MESSAGE.prototype.oncommand = function(){  
            //this.close = true;
            this.hide();  
	        window.open("http://www.qpsh.com");
	        
           
        } 
        /**//*  
        *    消息显示方法  
        */  
        CLASS_MSN_MESSAGE.prototype.show = function(){  

            var oPopup = window.createPopup(); //IE5.5+  
           
            this.Pop = oPopup;  
          
            var w = this.width;  
            var h = this.height;  
          
            var str = "<DIV style='BORDER-RIGHT: #455690 1px solid; BORDER-TOP: #a6b4cf 1px solid; Z-INDEX: 99999; LEFT: 0px; BORDER-LEFT: #a6b4cf 1px solid; WIDTH: " + w + "px; BORDER-BOTTOM: #455690 1px solid; POSITION: absolute; TOP: 0px; HEIGHT: " + h + "px; BACKGROUND-COLOR: #c9d3f3'>"  
                str += "<TABLE style='BORDER-TOP: #ffffff 1px solid; BORDER-LEFT: #ffffff 1px solid' cellSpacing=0 cellPadding=0 width='100%' bgColor=#cfdef4 border=0>"  
                str += "<TR>"  
                str += "<TD style='FONT-SIZE: 12px;COLOR: #0f2c8c' width=30 height=24></TD>"  
                str += "<TD style='PADDING-LEFT: 4px; FONT-WEIGHT: normal; FONT-SIZE: 12px; COLOR: #1f336b; PADDING-TOP: 4px' vAlign=center width='100%'>" + this.caption + "</TD>"  
                str += "<TD style='PADDING-RIGHT: 2px; PADDING-TOP: 2px' vAlign=center align=right width=19>"  
                str += "<SPAN title=关闭 style='FONT-WEIGHT: bold; FONT-SIZE: 12px; CURSOR: hand; COLOR: red; MARGIN-RIGHT: 4px' id='btSysClose' >×</SPAN></TD>"  
                str += "</TR>"  
                str += "<TR>"  
                str += "<TD style='PADDING-RIGHT: 1px;PADDING-BOTTOM: 1px' colSpan=3 height=" + (h-28) + ">"  
                str += "<DIV align=center style='BORDER-RIGHT: #b9c9ef 1px solid; PADDING-RIGHT: 8px; BORDER-TOP: #728eb8 1px solid; PADDING-LEFT: 8px; FONT-SIZE: 12px; PADDING-BOTTOM: 8px; BORDER-LEFT: #728eb8 1px solid; WIDTH: 100%; COLOR: #1f336b; PADDING-TOP: 8px; BORDER-BOTTOM: #b9c9ef 1px solid; HEIGHT: 100%'>" + this.title + "<BR><BR>"  
                str += "<DIV style='WORD-BREAK: break-all' align=center><A href='javascript:void(0)' hidefocus=false id='btCommand'><FONT color=#ff0000>" + this.message + "</FONT></A><A  href=\"javascript:;\"  hidefocus=false id='ommand'><FONT color=#ff0000>查看</FONT></A></DIV>"  
                str += "</DIV>"  
                str += "</TD>"  
                str += "</TR>"  
                str += "</TABLE>"  
                str += "</DIV>"  
          
            oPopup.document.body.innerHTML = str; 
            
          
            this.offset  = 0; 
            var me  = this;  

            oPopup.document.body.onmouseover = function(){me.pause=true;}
            oPopup.document.body.onmouseout = function(){me.pause=false;}

            var fun = function(){  
                var x  = me.left; 
                var y  = 0; 
                var width    = me.width; 
                var height    = me.height; 

                    if(me.offset>me.height){ 
                        height = me.height; 
                    } else { 
                        height = me.offset; 
                    } 

                y  = me.bottom - me.offset; 
                if(y<=me.top){ 
                    me.timeout--; 
                    if(me.timeout==0){ 
                        window.clearInterval(me.timer);  
                        if(me.autoHide){
                            me.hide(); 
                        }
                    } 
                } else { 
                    me.offset = me.offset + me.step; 
                } 
                me.Pop.show(x,y,width,height);    

            }  
          
            this.timer = window.setInterval(fun,this.speed)      
          
             
          
            var btClose = oPopup.document.getElementById("btSysClose");  
          
            btClose.onclick = function(){  
                me.close = true;
                me.hide();  
            }  
          
            var btCommand = oPopup.document.getElementById("btCommand");  
            btCommand.onclick = function(){  
                me.oncommand();  
            }    
	         var ommand = oPopup.document.getElementById("ommand");  
              ommand.onclick = function(){  
               //this.close = true;
            me.hide();  
//	        window.location.href=ommand.href;	         
	        window.location.href=window.location.href;
	        window.focus();
            }   
        }  
        /**//* 
        ** 设置速度方法 
        **/ 
        CLASS_MSN_MESSAGE.prototype.speed = function(s){ 
            var t = 20; 
            try { 
                t = praseInt(s); 
            } catch(e){} 
            this.speed = t; 
        } 
        /**//* 
        ** 设置步长方法 
        **/ 
        CLASS_MSN_MESSAGE.prototype.step = function(s){ 
            var t = 1; 
            try { 
                t = praseInt(s); 
            } catch(e){} 
            this.step = t; 
        } 
          
        CLASS_MSN_MESSAGE.prototype.rect = function(left,right,top,bottom){ 
            try { 
                this.left        = left    !=null?left:this.right-this.width; 
                this.right        = right    !=null?right:this.left +this.width; 
                this.bottom        = bottom!=null?(bottom>screen.height?screen.height:bottom):screen.height; 
                this.top        = top    !=null?top:this.bottom - this.height; 
            } catch(e){} 
        } 
        
        function Show()
        {
            var count = ManageAjax.GetSmsCount().value;
            if(parseInt(count) > 0)
            {
                var MSG1 = new CLASS_MSN_MESSAGE("aa",200,160,"短信营销提示：","您有 " + count + " 条未审核的短信","");  
                MSG1.rect(null,null,null,screen.height-50); 
                MSG1.speed = 6; 
                MSG1.step = 2; 
                MSG1.show();  
            }
        }

        window.setInterval("Show()",10000); 
    </script>

    <style type="text/css"> 
        #winpop { width:200px; height:0px; position:absolute; right:0; bottom:0; border:1px solid #999999; margin:0; padding:1px; overflow:hidden;display:none; background:#FFFFFF} 
        #winpop .title { width:100%; height:20px; line-height:20px; background:#FFCC00; font-weight:bold; text-align:center; font-size:12px;} 
        #winpop .con { width:100%; height:80px; line-height:80px; font-weight:bold; font-size:12px; color:#FF0000; text-decoration:underline; text-align:center} 
        #silu { font-size:13px; color:#999999; position:absolute; right:0;bottom:0px; text-align:right; text-decoration:underline; line-height:22px;} 
        .close { position:absolute; right:4px; top:-1px; color:#FFFFFF; cursor:pointer} 
    </style>
</head>
<body onload="onChange()">
    <form id="form1" runat="server">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="search" >
                <tr>
                    <td class="search_list" style="height: 40px">
                        快速查询：<asp:DropDownList ID="txtSelectTime" runat="server" onchange="onChange()">
                            <asp:ListItem Value="1">当天</asp:ListItem>
                            <asp:ListItem Value="2">一周以内</asp:ListItem>
                            <asp:ListItem Value="3" Selected="True">一个月以内</asp:ListItem>
                            <asp:ListItem Value="4">任意时间段</asp:ListItem>
                        </asp:DropDownList>
                        <span id ="PanelDate" style="display:none;">开始时间<input id="txtStartTime"  type="text" class="Wdate"  onpropertychange="return CheckDate();"   runat="Server" style="width: 80px"  readonly="readOnly"/>&nbsp;
                    结束时间<input id="txtEndTime"  type="text" class="Wdate"  onpropertychange="return CheckDate();"   runat="Server" style="width: 80px" readonly="readOnly"/></span>
                        短信内容<asp:TextBox ID="txtMessage" runat="server" Width="90px" onpropertychange="return CheckSub();"></asp:TextBox>
                        用户名<asp:TextBox ID="txtUserName" runat="server" Width="90px" onpropertychange="return CheckUserName();"></asp:TextBox>
                        审核状态<asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Value="-1">所有</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">等待审核</asp:ListItem>
                            <asp:ListItem Value="1">审核通过</asp:ListItem>
                            <asp:ListItem Value="20">违规信息</asp:ListItem>
                            <asp:ListItem Value="21">损害他人利益</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="searchbut" runat="server" CssClass="blue" OnClientClick="return checkData()"
                            OnClick="searchbut_Click" Text="搜索" /></td>
                    <td align="center" style="color: #0a3c5c; text-decoration: underline" width="40">
                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="resf_Click"><img border="0" src="../Images/refresh.gif" alt="页面刷新"/></asp:LinkButton></td>
                </tr>
            </table>
        </div>
        <div id="Div2" align="center">
            <asp:GridView ID="GroupSendList" runat="server" Width="100%" AutoGenerateColumns="False"
                BorderWidth="1px" DataKeyNames="MsgID" OnRowCommand="GroupSendList_RowCommand"
                OnRowDataBound="GroupSendList_RowDataBound" OnRowCreated="GroupSendList_RowCreated"
                GridLines="Horizontal" OnRowDeleting="GroupSendList_RowDeleting" 
                EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField HeaderText="全选">
                        <HeaderTemplate>
                            <input id="chkAll" name="chkAll" type="checkbox" onclick="CheckAll(this)" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="header" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="MsgID" HeaderText="批次号">
                    </asp:BoundField>
                    <asp:BoundField DataField="newUserAccount" HeaderText="用户名" />
                    <asp:BoundField DataField="SendCounts" HeaderText="总条数">
                    </asp:BoundField>
                    <asp:BoundField DataField="SendTime" HeaderText="发送时间">
                    </asp:BoundField>
                    <asp:BoundField DataField="Message" HeaderText="信息内容">
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="状态">
                        <ItemTemplate>
                            <%# Base.GetStatus(Eval("State").ToString())%>
                        </ItemTemplate>
                        <HeaderStyle CssClass="rightYline" />
                        <ItemStyle CssClass="gridview_td_downline" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="通道号">
                        <ItemTemplate>
                            <%# Base.GetChannelName(int.Parse(Eval("ChannelID").ToString()))%>
                        </ItemTemplate>
                        <HeaderStyle CssClass="rightYline" />
                        <ItemStyle CssClass="gridview_td_downline" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ApprovalTime" HeaderText="审核时间">
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbPass" runat="server" CommandName="Pass">通过</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlReason" runat="server">
                                <asp:ListItem Selected="True" Value="20">违规信息</asp:ListItem>
                                <asp:ListItem Value="21">损害他人利益</asp:ListItem>
                            </asp:DropDownList>
                            <asp:LinkButton ID="lbRefuse" runat="server" CommandName="Refuse">拒绝</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="State" Visible="False"></asp:BoundField>
                </Columns>
                <HeaderStyle CssClass="gridview_tr" />
                <EmptyDataTemplate>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
        <asp:Literal ID="TipDiv" runat="server"></asp:Literal>&nbsp;<webdiyer:AspNetPager
            ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页"
            OnPageChanged="AspNetPager1_PageChanged" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Left"
            SubmitButtonText="GO" Width="101%">
        </webdiyer:AspNetPager>
        <table style="background-color:#F7F7F7; width:100%" border="0" cellspacing="0" cellpadding="0">
            <tr style="display:none">
                <td width="9%" height="30" align="right" valign="middle" nowrap="nowrap">
                    &nbsp;</td>
                <td width="91%" align="right" valign="middle">
                    状态选择：
                    <asp:DropDownList ID="selectArea" runat="server">
                        <asp:ListItem Selected="True" Value="1">通过</asp:ListItem>
                        <asp:ListItem Value="20">违规信息</asp:ListItem>
                        <asp:ListItem Value="21">损害他人利益</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;<asp:Button ID="btnBatch" runat="server" Text="批量审核" OnClick="btnBatch_Click" OnClientClick="return confirmBatchApproveMsg();" />
                    &nbsp;&nbsp; &nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdTotalRows" runat="server" Value="0" />
    </form>
</body>
</html>
