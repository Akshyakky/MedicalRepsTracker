﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPagePrivate.master" AutoEventWireup="true" CodeFile="SampleConsumedReport.aspx.cs" Inherits="WebPages_SampleConsumedReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid" width="50%" style="background-color: White">
        <br />
        <div class="row">
            <div class="col-md-12" align="center">
                <h2>Sample Consumed Report</h2>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <hr style="border: 1px solid #960e00" />
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div runat="server" id="divStockIn">
                    <div runat="server" id="divSearch">
                        <br />
                        <div class="row">
                            <div class="col-md-12" align="left">
                                <asp:Label ID="Label41" runat="server" CssClass="LabelStyles"
                                    Text="Search By" Font-Size="large"></asp:Label>
                            </div>
                        </div>
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:Label ID="Label7" runat="server" CssClass="LabelStyles" Text="Product Name : "></asp:Label>

                                        <asp:TextBox TabIndex="1" runat="server" ID="txtProductName" CssClass="form-control TextBox"
                                            placeholder="Product Name" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                            Enabled="True" TargetControlID="txtProductName" ValidChars=" .',()-/" >
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="Label8" runat="server" CssClass="LabelStyles" Text="Medical Rep : "></asp:Label>


                                        <asp:TextBox TabIndex="2" runat="server" ID="txtmedrepName" CssClass="form-control TextBox"
                                            placeholder="Medical Rep Name" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                            Enabled="True" TargetControlID="txtmedrepName" ValidChars=" .',()-/">
                                        </cc1:FilteredTextBoxExtender>

                                    </div>
                                    <%--    <div class="col-md-4">
                                        <asp:Label ID="Label2" runat="server" CssClass="LabelStyles" Text="Chemist : "></asp:Label>
                                        <asp:TextBox TabIndex="3" runat="server" ID="txtInvNo" CssClass="form-control TextBox"
                                            placeholder="Invoice No." />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                            Enabled="True" TargetControlID="txtInvNo" ValidChars=" .',()-/">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>--%>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <div class="row">
                            <div class="col-md-4" visible="false" runat="server">
                                <asp:Label ID="Label1" runat="server" CssClass="LabelStyles" Text="Location : "></asp:Label>


                                <asp:TextBox TabIndex="3" runat="server" ID="txtSrchLocation" CssClass="form-control TextBox"
                                    placeholder="Location" />

                            </div>
                            <div class="col-md-4" style="z-index: 9999">
                                <asp:Label ID="Label3" runat="server" CssClass="LabelStyles" Text="Visit Date From : "></asp:Label>
                                <asp:TextBox TabIndex="4" runat="server" ID="txtFrom" CssClass="form-control TextBox"
                                    placeholder="DD/MM/YYYY" />
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom,Numbers"
                                    Enabled="True" TargetControlID="txtFrom" ValidChars="/">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:CalendarExtender ID="cc" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
                                </cc1:CalendarExtender>
                            </div>
                            <div class="col-md-4" style="z-index: 8888">
                                <asp:Label ID="Label4" runat="server" CssClass="LabelStyles" Text="To : "></asp:Label>
                                <asp:TextBox TabIndex="5" runat="server" ID="txtTo" CssClass="form-control TextBox"
                                    placeholder="DD/MM/YYYY" />
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom,Numbers"
                                    Enabled="True" TargetControlID="txtTo" ValidChars="/">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTo">
                                </cc1:CalendarExtender>
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
                        <br />
                        <div class="row">

                            <div class="col-md-4">
                                <br />
                                <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Show "></asp:Label>
                                <asp:DropDownList ID="ddlShowTop" runat="server" CssClass="form-control" Width="40%"
                                    TabIndex="6" Style="display: inline-block">
                                    <asp:ListItem>All</asp:ListItem>
                                    <asp:ListItem Selected="True">10</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>500</asp:ListItem>
                                    <asp:ListItem>1000</asp:ListItem>
                                    <asp:ListItem>2000</asp:ListItem>
                                    <asp:ListItem>5000</asp:ListItem>
                                    <asp:ListItem>10000</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="Label9" runat="server" Font-Bold="true" Text="Records "></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <br />
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                                    TabIndex="8" OnClick="btnSearch_Click" Style="margin-top: 5px" />
                                &nbsp;&nbsp;
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="btn btn-primary"
                                        TabIndex="9" OnClick="btnRefresh_Click" Style="margin-top: 5px" />
                                &nbsp;&nbsp;
                                    <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btn btn-primary"
                                        TabIndex="10" OnClick="btnExport_Click" Style="margin-top: 5px" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12" align="center">
                                <asp:Label ID="lblRecordsCount" runat="server" Font-Bold="true" Text="Records Found of"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row"  runat="server" id="dvview">
                        <div class="col-md-12" align="center">
                            <asp:Label ID="lblHeading" Visible="False" runat="server" Font-Bold="True" Text="Sample Consumed Report" Font-Size="Large"></asp:Label>
                        </div>
                        <div class="col-md-12" style="overflow-x: scroll">
                            <asp:GridView ID="gvStockOutReport" runat="server" AutoGenerateColumns="False" CssClass="Admingridtable"
                                EmptyDataText="No Records Found!!!" Width="100%" EnableModelValidation="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="slno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="20px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rep. Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvNo" runat="server" Text='<%# Eval("MP_Name")%>'
                                                Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="110px" />
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="Product Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupName" runat="server" Text='<%# Eval("MedTrans_ProdName")%>'
                                                Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                       
                                        </ItemTemplate>
                                        <ControlStyle Width="110px" />
                                    </asp:TemplateField>
                                
                                     <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvNo" runat="server" Text='<%# Eval("MedTrans_Qty")%>'
                                                Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="110px" />
                                    </asp:TemplateField>
                                 <%--   <asp:TemplateField HeaderText="Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Trans_Location")%>'
                                                Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="110px" />
                                    </asp:TemplateField>--%>


                                    <asp:TemplateField HeaderText="Location on MAP">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("LOC")%>'
                                                Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                            <br />
                                           <%-- <a href='https://www.google.com/maps/@<%#Eval("LOC") %>,14.25z' target="_blank">View

                                            </a> --%> <a href='https://www.google.com/maps/search/<%#Eval("LOC") %>/@<%#Eval("LOC") %>,17z' target="_blank">View

                                            </a>
                                        </ItemTemplate>
                                        <ControlStyle Width="130px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <script src="../autofiller/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../autofiller/jquery.min.js" type="text/javascript"></script>
    <script src="../ASPXStyles/js/jquery-2.1.4.min.js" type="text/javascript"></script>
    <link href="../AspxStyles/css/jquery-ui.css" rel="stylesheet" />
    <script src="../AspxStyles/js/jquery.min.js"></script>
    <script src="../AspxStyles/js/jquery-ui.js"></script>

        

    <script type="text/javascript">
         $(document).ready(function () {
            LoadAllFunctions();
        });
        function LoadAllFunctions() {
            $("#<% =txtSrchLocation.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "../Location.ashx?q=" + document.getElementById('<% =txtSrchLocation.ClientID %>').value,
                        dataType: "json",
                        success: function (data) {
                            response($.map(data, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (result) {
                            alert(JSON.stringify(result) + "Error......");
                        }
                    });
                }
            });
        }

    </script>
</asp:Content>


