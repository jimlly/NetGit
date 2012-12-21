<%@ Control Language="C#" AutoEventWireup="true" Inherits="Smsmarketing_AreaIndustryNumWUC" Codebehind="AreaIndustryNumWUC.ascx.cs" %>
<head id="Head1" runat="server">
<title>地区，行业，号码数量</title>
<script type="text/javascript" language="javascript">
   //提出表头
   function s()
   { 
         var t = document.getElementById("<%=NumList.ClientID%>"); 
         var t2 = t.cloneNode(true);
         for(i = t2.rows.length -1;i > 0;i--)
         {
              t2.deleteRow(i) ;            
              a.appendChild(t2);
               
          }
           t.deleteRow(0) ;
    }
</script>
</head>
<body onload="s()">
    <form id="form1" runat="server" >
    <span id = "a" style="height:20"></span>
        <div id = "b" style="overflow-y: scroll; height: 200px">
       <asp:GridView ID="NumList" runat="server"  Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnRowDataBound="NumList_RowDataBound" OnRowCreated="NumList_RowCreated" >
        <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
        <EmptyDataTemplate>
         <div style="vertical-align:top; border:none;">
                <div style="vertical-align:middle;text-align:center" >
                <strong style="font-size:15">没有任何记录！</strong>
                </div>          
            <emptydatarowstyle verticalalign="Top" />
        </div>
        </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField  HeaderText="主行业" DataField="MainName" >
                      <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="15%"/>                        
                      <HeaderStyle  HorizontalAlign="Left" Width="15%"/>                        
                    </asp:BoundField>
                    <asp:BoundField HeaderText="子行业" DataField="IndustryName">
                        <ItemStyle HorizontalAlign="Left"  VerticalAlign="Middle" Width="9%"/>
                        <HeaderStyle HorizontalAlign="Left"  Width="9%"/>
                    </asp:BoundField>
                </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <EmptyDataRowStyle Font-Bold="True" HorizontalAlign="Center" />
            </asp:GridView>
         </div>
     </form>
</body>
