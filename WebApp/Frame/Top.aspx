<%@ Page Language="C#" AutoEventWireup="true" Inherits="Frame_Top" Codebehind="Top.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>无标题文档</title>
<!--    <script type="text/javascript" src="../JS/ymPrompt/ymPrompt.js"></script>
    <link href="../JS/ymPrompt/skin/qq/ymPrompt.css"  rel="stylesheet"  type="text/css" />-->
    <script type="text/javascript">

        var url = "";
        function GetUrl() {
            var URLParams = new Object();
            var aParams = document.location.search.substr(1).split('&');
            for (i = 0; i < aParams.length; i++) {
                var aParam = aParams[i].split('=');
                if (aParam[0] == "LogoName") {
                    document.getElementById('LogoPic').src = "../Logo/" + aParam[1];
                }
                else {
                    url = aParam[1];
                }
            }
        }

        function Exit() {

            parent.window.location.href = "../logout.aspx";
        }

        function ChangePwd() {
            parent.ymPrompt.win({ message: 'Frame/EditPassword.aspx', width: 570, height: 350, title: '修改密码', maxBtn: false, minBtn: false, iframe: true, useSlide: false, winAlpha: 1.0 });
        }
    </script>
</head>
<style type="text/css">
    /*公用CSS*/
    BODY
    {
        border-top-width: 0px;
        padding-right: 0px;
        padding-left: 0px;
        border-left-width: 0px;
        border-bottom-width: 0px;
        padding-bottom: 0px;
        margin: 0px;
        padding-top: 0px;
        border-right-width: 0px;
    }
    
    UL
    {
        border-top-width: 0px;
        padding-right: 0px;
        padding-left: 0px;
        border-left-width: 0px;
        border-bottom-width: 0px;
        padding-bottom: 0px;
        margin: 0px;
        padding-top: 0px;
        border-right-width: 0px;
    }
    OL
    {
        border-top-width: 0px;
        padding-right: 0px;
        padding-left: 0px;
        border-left-width: 0px;
        border-bottom-width: 0px;
        padding-bottom: 0px;
        margin: 0px;
        padding-top: 0px;
        border-right-width: 0px;
    }
    LI
    {
        border-top-width: 0px;
        padding-right: 0px;
        padding-left: 0px;
        border-left-width: 0px;
        border-bottom-width: 0px;
        padding-bottom: 0px;
        margin: 0px;
        padding-top: 0px;
        border-right-width: 0px;
    }
    P
    {
        border-top-width: 0px;
        padding-right: 0px;
        padding-left: 0px;
        border-left-width: 0px;
        border-bottom-width: 0px;
        padding-bottom: 0px;
        margin: 0px;
        padding-top: 0px;
        border-right-width: 0px;
    }
    FORM
    {
        border-top-width: 0px;
        padding-right: 0px;
        padding-left: 0px;
        border-left-width: 0px;
        border-bottom-width: 0px;
        padding-bottom: 0px;
        margin: 0px;
        padding-top: 0px;
        border-right-width: 0px;
    }
    FIELDSET
    {
        border-top-width: 0px;
        padding-right: 0px;
        padding-left: 0px;
        border-left-width: 0px;
        border-bottom-width: 0px;
        padding-bottom: 0px;
        margin: 0px;
        padding-top: 0px;
        border-right-width: 0px;
    }
    TABLE
    {
        border-top-width: 0px;
        padding-right: 0px;
        padding-left: 0px;
        border-left-width: 0px;
        border-bottom-width: 0px;
        padding-bottom: 0px;
        margin: 0px;
        padding-top: 0px;
        border-right-width: 0px;
    }
    TD
    {
        border-top-width: 0px;
        padding-right: 0px;
        padding-left: 0px;
        border-left-width: 0px;
        border-bottom-width: 0px;
        padding-bottom: 0px;
        margin: 0px;
        padding-top: 0px;
        border-right-width: 0px;
    }
    IMG
    {
        border-top-width: 0px;
        padding-right: 0px;
        padding-left: 0px;
        border-left-width: 0px;
        border-bottom-width: 0px;
        padding-bottom: 0px;
        margin: 0px;
        padding-top: 0px;
        border-right-width: 0px;
    }
    DIV
    {
        border-top-width: 0px;
        padding-right: 0px;
        padding-left: 0px;
        border-left-width: 0px;
        border-bottom-width: 0px;
        padding-bottom: 0px;
        margin: 0px;
        padding-top: 0px;
        border-right-width: 0px;
    }
    BODY
    {
        font-size: 13px;
        color: #000;
        font-family: "宋体";
        background-color: #fff;
    }
    UL
    {
        list-style-type: none;
    }
    OL
    {
        list-style-type: none;
    }
    SELECT
    {
        vertical-align: middle;
    }
    INPUT
    {
        vertical-align: middle;
    }
    IMG
    {
        vertical-align: middle;
    }
    SELECT
    {
        vertical-align: middle;
    }
    FORM
    {
        padding-right: 0px;
        padding-left: 0px;
        padding-bottom: 0px;
        margin: 0px;
        padding-top: 0px;
    }
    A
    {
        color: #1e50a2;
        text-decoration: underline;
    }
    A:hover
    {
        color: #c9171e;
    }
    
    .clear
    {
        clear: both;
    }
    /*头部CSS开始*/
    .header
    {
        width: 100%;
        height: 70px;
        background-image: url(../Images/bg_2.jpg);
        background-repeat: repeat-x;
        background-position: 0px 0px;
        margin-top: 0px;
        margin-right: 0px;
        margin-bottom: 10px;
        margin-left: 0px;
    }
    .header .logo
    {
        float: left;
        margin-left: 0px !important;
        width: 136px;
        margin-top: 13px;
    }
    #position
    {
        margin-top: -10px;
        font-size: 12px;
        margin-left: 0px;
        width: 100%;
        color: #333;
        line-height: 22px;
        margin-right: 0px;
        height: 22px;
        _margin-top: -10px;
        background-image: url(../Images/bg_2.jpg);
        background-repeat: repeat-x;
        background-position: 0px -72px;
        margin-bottom: 0px;
        border-bottom-width: 1px;
        border-bottom-style: solid;
        border-bottom-color: #45a8d9;
    }
    #position A
    {
        color: #333;
        text-decoration: none;
    }
    #position A:hover
    {
        color: #c9171e;
    }
    .PL /*头部当前位置*/ /*头部当前位置*/
    {
        float: left;
        width: 580px;
        padding-left: 15px;
    }
    #quit
    {
        margin-top: 0px;
        font-size: 14px;
        float: right;
        margin-right: 0px;
        padding-right: 15px;
    }
    #quit A
    {
        margin-left: -5px !important;
        font-size: 13px;
    }
    #quit A:hover
    {
        text-decoration: underline;
    }
    /*头部CSS结束*/
</style>
<body onload="GetUrl()">
    <div class="header">
        <!--头部开始-->
        <div class="logo">
            <img id="LogoPic" src="" height="42" /></div>
        <!--头部结束-->
    </div>
    <!--</iframe>-->
    <div id="position">
        <div class="PL">
            当前位置：<a class="black" href="###">短信群发</a><!-- &gt; <a 
href="#">发件箱</a>-->
        </div>
        <div id="quit">
            <a href="#" onclick="ChangePwd()">·修改密码</a> <a href="#" onclick="Exit();">·退出登录</a>
        </div>
    </div>
</body>
</html>