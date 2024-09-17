<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPagePrivate.master" AutoEventWireup="true" CodeFile="VisitsChart.aspx.cs" Inherits="WebPages_VisitsChart" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid" width="100%" style="font-size: 14px; background-color: white">

        <br />
        <div class="row">
            <div class="col-md-12" align="center">
                <h2>Location Wise Visits Report</h2>
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
                            <div class="col-md-12" align="center">
                                <asp:Label ID="Label1" runat="server" CssClass="LabelStyles" Style="color: red"
                                    Text="(*) Fields are Mandatory"></asp:Label>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-4" align="left">
                                <asp:Label ID="Label41" runat="server" CssClass="LabelStyles"
                                    Text="Search By" Font-Size="large"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-4">
                                  <asp:Label ID="Label23" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                Text="*" ForeColor="red"></asp:Label>
                                <asp:Label ID="Label7" runat="server" CssClass="LabelStyles" Text="Branch Name"></asp:Label>

                                <asp:DropDownList ID="ddlBranch" CssClass="form-control TextBox" runat="server" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranch"
                                    CssClass="LabelStyleRequired" ErrorMessage="Select Branch" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>


                            <div class="col-md-4">
                                  <asp:Label ID="Label5" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                Text="*" ForeColor="red"></asp:Label>
                                <asp:Label ID="Label2" runat="server" CssClass="LabelStyles" Text="Medical Rep Name"></asp:Label>

                                <asp:DropDownList ID="ddlMedRep" CssClass="form-control TextBox" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlMedRep"
                                    CssClass="LabelStyleRequired" ErrorMessage="Select Medical Rep" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4" style="z-index: 9999">
                                  <asp:Label ID="Label6" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                Text="*" ForeColor="red"></asp:Label>
                                <asp:Label ID="Label3" runat="server" CssClass="LabelStyles" Text="Visit Date From : "></asp:Label>
                                <asp:TextBox TabIndex="4" runat="server" ID="txtFrom" CssClass="form-control TextBox"
                                    placeholder="DD/MM/YYYY" />
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom,Numbers"
                                    Enabled="True" TargetControlID="txtFrom" ValidChars="/">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:CalendarExtender ID="cc" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrom">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select From Date" ControlToValidate="txtFrom"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4" style="z-index: 8888">
                                  <asp:Label ID="Label8" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                Text="*" ForeColor="red"></asp:Label>
                                <asp:Label ID="Label4" runat="server" CssClass="LabelStyles" Text="To : "></asp:Label>
                                <asp:TextBox TabIndex="5" runat="server" ID="txtTo" CssClass="form-control TextBox"
                                    placeholder="DD/MM/YYYY" />
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom,Numbers"
                                    Enabled="True" TargetControlID="txtTo" ValidChars="/">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTo">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select To Date" ControlToValidate="txtTo"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-8">
                            <br />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                                TabIndex="8" OnClick="btnSearch_Click" Style="margin-top: 5px" />
                            &nbsp;&nbsp;
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="btn btn-primary"
                                        TabIndex="9" OnClick="btnRefresh_Click" CausesValidation="false" Style="margin-top: 5px" />

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <hr style="border: 1px solid #960e00" />
                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-md-12">
                <asp:Panel ID="pnlInstitutionChart" runat="server">
                    <div class="row">
                        <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                            <div id="div_LocationWiseChart">
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>


