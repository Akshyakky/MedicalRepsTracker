<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPagePublic.master"
    AutoEventWireup="true" CodeFile="RepsLogin.aspx.cs" MaintainScrollPositionOnPostback="true"
    Inherits="WebPages_Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <style type="text/css">
        .CaptchaLableStyle
        {
            font-family: Rage;
            font-size: xx-large;
            color: #0C71C8;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid" width="50%" style="background-color: White">
        <br />
        <%-- <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>--%>
        <div class="row">
            <div class="col-md-2">
            </div>
            <div class="col-md-8" align="center">
                <div align="center" style="border: 1px inset #960e00; width: 75%; border-radius: 4px 4px;">
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-8" align="center">
                            <br />
                            <h2>
                               Medical Reps Login</h2>
                        </div>
                        <div class="col-md-2">
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12" align="center">
                            <asp:Label ID="Label41" runat="server" CssClass="LabelStyles" Style="color: red"
                                Text="(*) Fields are Mandatory" Font-Size="10pt"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-8" align="center">
                            <span class="input-group-addon email-addon"><i class="fa fa-user"></i></span>
                            <asp:TextBox runat="server" class="form-control" placeholder="Username" ID="txtUserName"
                                Width="65%" Style="display: inline-block" />
                            <asp:Label ID="Label148" runat="server" Font-Bold="true" CssClass="LabelStyleValidation"
                                Text="*" ForeColor="red"></asp:Label>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Enter Username"
                                ControlToValidate="txtUserName" runat="server" ForeColor="Red" />
                        </div>
                        <div class="col-md-2">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-8" align="center">
                            <span class="input-group-addon password-addon"><i class="fa fa-lock"></i></span>
                            <asp:TextBox runat="server" class="form-control" placeholder="Password" ID="txtPassword"
                                Width="65%" Style="display: inline-block" TextMode="Password" />
                            <asp:Label ID="Label1" runat="server" Font-Bold="true" CssClass="LabelStyleValidation"
                                Text="*" ForeColor="red"></asp:Label>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Enter Password"
                                ControlToValidate="txtPassword" runat="server" ForeColor="Red" />
                        </div>
                        <div class="col-md-2">
                        </div>
                    </div>
                    <div class="row" id="div_Captcha" runat="server" visible="false">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-8">
                            <div style="border: 1px dotted #960e00; width: 65%; padding: 1%;">
                                <div class="row">
                                    <div class="col-md-6 aligntext">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblRandomNumber" CssClass="CaptchaLableStyle" Font-Bold="true" runat="server"
                                                    Text="0 + 0 "></asp:Label>
                                                <asp:Label ID="lblSum" runat="server" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 aligntext1">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:TextBox runat="server" MaxLength="2" CssClass="CaptchaLableStyle" class="form-control"
                                                    ID="txtCaptcha" Width="65%" />
                                                <asp:Label ID="Label2" runat="server" Font-Bold="true" CssClass="LabelStyleValidation"
                                                    Text="*" ForeColor="red"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 aligntext">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <span><i class="fa fa-refresh"></i>
                                                    <asp:LinkButton ID="lnkBtnRefresh" CausesValidation="false" ToolTip="Refresh Captcha"
                                                        Height="30px" Font-Bold="true" runat="server" OnClick="lnkBtnRefresh_Click">Refresh</asp:LinkButton></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 aligntext1">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <%--  <asp:ImageButton ID="pmgBtnRefresh" CausesValidation="false" ImageUrl="~/ADMIN/ASPXImages/refresh2.png"
                                            Height="10%" Width="20%" runat="server" OnClick="pmgBtnRefresh_Click" />--%>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                    Enabled="True" TargetControlID="txtCaptcha">
                                                </cc1:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCaptcha"
                                                    CssClass="LabelStyleRequired" ErrorMessage="Enter Captcha" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-8">
                            &nbsp; &nbsp;
                            <asp:Button ID="btnSignIn" Text="Login" runat="server" CssClass="btn btn-primary"
                                OnClick="btnSignIn_Click" />
                            &nbsp; &nbsp;
                            <asp:Button ID="btnReset" Text="Reset" CausesValidation="false" runat="server" CssClass="btn btn-primary"
                                OnClick="btnReset_Click" />
                        </div>
                        <div class="col-md-2">
                        </div>
                    </div>
                    <br />
                </div>
            </div>
            <div class="col-md-2">
            </div>
        </div>
        <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
        <br />
    </div>
</asp:Content>
