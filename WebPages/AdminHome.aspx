    <%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPagePrivate.master" AutoEventWireup="true" CodeFile="AdminHome.aspx.cs" Inherits="WebPages_Default" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <link rel="icon" href="../files/assets/images/favicon.ico" type="image/x-icon">
        <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700,800" rel="stylesheet">
        <link href="https://fonts.googleapis.com/css?family=Quicksand:500,700" rel="stylesheet">

        <%--<link rel="stylesheet" type="text/css" href="../AspxStyles/HomeStyles/bootstrap.min.css">--%>
        <link rel="stylesheet" type="text/css" href="../AspxStyles/HomeStyles/style.css">
        <link rel="stylesheet" type="text/css" href="../AspxStyles/HomeStyles/widget.css">
        <%--<link href="../AspxStyles/HomeStyles/font-awesome-n.min.css" rel="stylesheet" />--%>
        <style type="text/css">
            .blinking
            {
                animation: blinkingText 0.2s infinite;
            }

            @keyframes blinkingText
            {
                0%
                {
                    color: #CC0000;
                }

                49%
                {
                    color: #000b77;
                }

                50%
                {
                    color: #000b77;
                }

                99%
                {
                    color: #ff0000;
                }

                100%
                {
                    color: #ff0000;
                }
            }
        </style>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                                <a href="#">
                                <div class="col" align="center">
                                     <h6 class="m-b-25"> <asp:Label ID="lblUserName" runat="server" Text="0" class="f-w-700 text-c-blue" Font-Size="X-Large"></asp:Label></h6>
                               
                                   </a>
                                    <asp:Label ID="lblDate" runat="server" Text="-" class="m-b-0"></asp:Label>
                                </div>
                                <div class="col-auto">
                                      <a href="">
                                    <i class="fa fa-user bg-c-blue"></i>
                                          </a>
                                </div>
                             
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="row adminrowStyle">
                        <div class="col-md-3"></div>
                        <div class="col-md-6" id="div1" runat="server">
                            <div class="comp-card" style="border: 1px solid rgba(0, 0, 0, 0.130);">
                                <div class="card-body" style="height: 650px">
                                    <div class="row">
                                        <div class="col-md-7">
                                            <br />
                                            <a href=""><h6 class="m-b-25">Medical Reps Wise Visit List</h6></a>
                                              <asp:Label ID="lblTdyDate" runat="server" Text="-" class="m-b-0"></asp:Label>
                                        </div>
                                        <div class="col-md-5" align="right">
                                            <div class="col-auto">
                                                <a href=""><i class="fa fa-shopping-cart bg-c-blue"></i></a>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">

                                        <div class="col-md-12">
                                            <div style="overflow-x: scroll;">
                                                <asp:GridView ID="gvCatWiseStock" runat="server" AutoGenerateColumns="False" CssClass="Admingridtable"
                                                    EmptyDataText="No Records Found!!!" Width="100%" EnableModelValidation="True" AllowPaging="true" OnPageIndexChanging="gvCatWiseStock_PageIndexChanging" PageSize="10">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Med. Reps Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCat" runat="server" Text='<%# Eval("MP_Name")%>'
                                                                    Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                                            </ItemTemplate>
                                                            <ControlStyle Width="200px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Visit Count">
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
                                    <%--<div class="row">
                                        <div class="col-md-12" align="right">
                                            <br />
                                            <asp:LinkButton ID="lnkViewMore" runat="server" CssClass="f-w-700 text-c-blue" OnClick="lnkViewMore_Click">View More >></asp:LinkButton>
                                        </div>
                                    </div>--%>
                               
                                </div>
                            </div>
                        </div>

                 

                    </div>
                    <br />
                    <br />
                    <br />

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </asp:Content>

