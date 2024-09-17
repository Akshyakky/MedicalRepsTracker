<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPagePrivate.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="WebPages_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../files/assets/images/favicon.ico" type="image/x-icon">
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700,800" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Quicksand:500,700" rel="stylesheet">

    <%--<link rel="stylesheet" type="text/css" href="../AspxStyles/HomeStyles/bootstrap.min.css">--%>
    <link rel="stylesheet" type="text/css" href="../AspxStyles/HomeStyles/style.css">
    <link rel="stylesheet" type="text/css" href="../AspxStyles/HomeStyles/widget.css">
    <%--<link href="../AspxStyles/HomeStyles/font-awesome-n.min.css" rel="stylesheet" />--%>
    <style type="text/css">
        .blinking {
            animation: blinkingText 0.2s infinite;
        }

        @keyframes blinkingText {
            0% {
                color: #CC0000;
            }

            49% {
                color: #000b77;
            }

            50% {
                color: #000b77;
            }

            99% {
                color: #ff0000;
            }

            100% {
                color: #ff0000;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>
    <div class="container-fluid" width="50%" style="font-size: 14px; background-color: white">
        <br />
        <div class="row">
            <div class="col-md-2">
            </div>
            <div class="col-md-8" align="center">
                <h2>Home</h2>
            </div>
            <div class="col-md-2">
            </div>
        </div>
        <br />
        <!-- Main content -->
        <div class="row adminrowStyle">
            <div class="col-md-3">
            </div>
            <div class="col-md-6">
                <div class="card comp-card">
                    <div class="card-body">
                        <div class="row align-items-center">
                            <a href="StockOutReport.aspx">
                                <div class="col">
                                    <a href="StockOutReport.aspx">
                                        <h6 class="m-b-25">Today's Visit Count</h6>
                                    </a>
                                    <asp:label id="lblCount" runat="server" text="0" class="f-w-700 text-c-blue" font-size="Large"></asp:label>
                                    <br />
                                    <asp:label id="lblDate" runat="server" text="-" class="m-b-0"></asp:label>
                                </div>
                                <div class="col-auto">
                                    <a href="">
                                        <i class="fa fa-shopping-cart bg-c-blue"></i>
                                    </a>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
            </div>
        </div>
        <asp:updatepanel id="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="row adminrowStyle">
                   <div class="col-md-3"></div>

                    <div class="col-md-6" id="divReOrder" runat="server" visible="false">
                        <div class="comp-card" style="border: 1px solid rgba(0, 0, 0, 0.130);">
                            <div class="card-body" style="height: 650px">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <br />
                                                <h6 style="color: #CC3300" class="m-b-25 blinking">Location Wise Visit Count</h6>
                                                <br />
                                            </div>
                                            <div class="col-md-6" align="right">
                                                <div class="col-auto">
                                                    <i class="fa fa-shopping-cart bg-c-blue"></i>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div style="overflow-x: scroll;">
                                                    <asp:GridView ID="gvReOrder" runat="server" AutoGenerateColumns="False" CssClass="Admingridtable"
                                                        EmptyDataText="No Records Found!!!" Width="100%" EnableModelValidation="True" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvReOrder_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Location">
                                                                <ItemTemplate>
                                                                   <asp:Label ID="Label1" runat="server" Text='<%# Eval("Location")%>'
                                                Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                                                   <a href='https://www.google.com/maps/search/<%#Eval("Location") %>/@<%#Eval("Location") %>,17z' target="_blank">Click

                                            </a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl" runat="server" Text='<%# Eval("cnt")%>'
                                                                        Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                                                </ItemTemplate>
                                                                <ControlStyle Width="90px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerSettings Mode="NumericFirstLast" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <br />
                <br />
                <br />

            </ContentTemplate>
        </asp:updatepanel>

    </div>
</asp:Content>


