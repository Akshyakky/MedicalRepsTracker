<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oops.aspx.cs" Inherits="oops" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AYUSH</title>
    <link rel="icon" href="../ASPXStyles/images/tab.png" />
    <style type="text/css">
        .whoops, .RakthaErrorBottom, .RakthaErrorLink .right, .RakthaErrorLink .right:hover
        {
            background: url(ASPXImages/error-bg.jpg) no-repeat left top;
        }
        
        #Emain-div
        {
            margin: 0px;
            padding: 0px;
            width: 100%;
            padding-top: 50px;
        }
        
        #Emain-div-inner
        {
            padding: 0px;
            margin: 0px auto;
            width: 640px;
            height: 385px;
        }
        
        .RakthaErrorTop
        {
            width: 100%;
            height: 60px;
        }
        
        .whoops
        {
            padding: 0px;
            height: 35px;
            background-position: -30px -24px;
        }
        
        .RakthaErrorTop h1
        {
            font-family: Georgia, "Times New Roman" , Times, serif;
            color: #E40000;
            font-size: 18px;
            line-height: 25px;
            margin: 0;
            padding-left: 50px;
        }
        
        .RakthaErrorBottom
        {
            float: left;
            padding: 0px;
            width: 640px;
            height: 295px;
            background-position: 280px -90px;
        }
        
        .RakthaErrorText
        {
            float: left;
            width: 330px;
            height: 245px;
        }
        
        .RakthaErrorLink
        {
            float: left;
            width: 380px;
            height: 40px;
        }
        
        .RakthaErrorLink .left
        {
            float: left;
            width: auto;
            height: 40px;
            font-family: Century Gothic;
            line-height: 40px;
            color: #e16500;
            font-size: 17px;
        }
        
        .RakthaErrorLink .left span, .RakthaErrorLink .left span a
        {
            font-weight: bold;
            color: #FF1010;
            text-decoration: none;
        }
        
        .RakthaErrorLink .right
        {
            float: right;
            width: 26px;
            height: 26px;
            margin-top: 10px;
            background-position: 0 0;
            -webkit-transition: all 0.3s ease;
            -moz-transition: all 0.3s ease;
            -o-transition: all 0.3s ease;
            transition: all 0.3s ease;
        }
        
        .RakthaErrorLink .right:hover
        {
            background-position: 0px -52px;
        }
        
        .RakthaErrorText h2
        {
            text-align: justify;
            padding-left: 50px;
            line-height: 26px;
            color: #CB5FFD;
            font-size: 15px;
            margin-top: 50px;
            font-family: Georgia, "Times New Roman" , Times, serif;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Emain-div">
            <div id="Emain-div-inner">
                <div class="RakthaErrorTop">
                    <div class="whoops">
                    </div>
                    <h1>
                        I may have broken something</h1>
                </div>
                <div class="RakthaErrorBottom">
                    <div class="RakthaErrorText">
                        <h2>
                            We're sorry - an fault has occurred.
                            <br />
                            The glitch has been logged and
                            <br />
                            will be looked into shortly.</h2>
                    </div>
                    <div class="RakthaErrorLink" style="cursor: pointer" id="dvOnline" runat="server" visible="false">
                        <div class="left">
                            Please return to the <span><a href="#">Home Page</a></span>
                            and retry
                        </div>
                    </div>
                    <div class="RakthaErrorLink" style="cursor: pointer" id="dvOffline" runat="server">
                        <div class="left">
                            Please return to the <span><a href="../WebPages/Login.aspx">Login Page</a></span>
                            and retry
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
