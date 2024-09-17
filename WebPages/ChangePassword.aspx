<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPagePrivate.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="WebPages_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <div class="container-fluid" width="50%" style="background-color: White">
        <br />
        <div class="row">
            <div class="col-md-12" align="center">
                <h2>Change Password</h2>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <hr style="border: 1px solid #960e00" />
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12" align="center">
                <asp:Label ID="Label41" runat="server" CssClass="LabelStyles" Style="color: red"
                    Text="(*) Fields are Mandatory"></asp:Label>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-1">
            </div>
            <div class="col-md-10">
                <div class="row">
                    <div class="col-md-5 aligntext">
                        <asp:Label ID="Label20" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                            Text="*" ForeColor="red"></asp:Label>
                        <asp:Label ID="Label3" runat="server" CssClass="LabelStyles" Text="Current Password : "></asp:Label>
                    </div>
                    <div class="col-md-5 aligntext1">
                        <asp:TextBox TabIndex="1" runat="server" ID="txtCurrentPassword" TextMode="Password"
                            CssClass="form-control TextBox" placeholder="Current Password" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCurrentPassword"
                            CssClass="LabelStyleRequired" ErrorMessage="Enter Current Password" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-2">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-5 aligntext">
                        <asp:Label ID="Label4" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                            Text="*" ForeColor="red"></asp:Label>
                        <asp:Label ID="Label5" runat="server" CssClass="LabelStyles" Text="New Password : "></asp:Label>
                    </div>
                    <div class="col-md-5 aligntext1">
                        <asp:TextBox TabIndex="2" runat="server" ID="txtNewPassword" TextMode="Password" CssClass="form-control TextBox"
                            placeholder="New Password" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNewPassword"
                            CssClass="LabelStyleRequired" ErrorMessage="Enter New Password" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-2">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-5 aligntext">
                        <asp:Label ID="Label23" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                            Text="*" ForeColor="red"></asp:Label>
                        <asp:Label ID="Label7" runat="server" CssClass="LabelStyles" Text="Confirm Password : "></asp:Label>
                    </div>
                    <div class="col-md-5 aligntext1">

                        <asp:TextBox TabIndex="3" runat="server" ID="txtConfirmPassword" TextMode="Password" CssClass="form-control TextBox"
                            placeholder="Confirm Password" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtConfirmPassword"
                            CssClass="LabelStyleRequired" Display="Dynamic" ErrorMessage="Enter Confirm Password" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password Mismatch"
                            ControlToCompare="txtNewPassword" Display="Dynamic" ControlToValidate="txtConfirmPassword"></asp:CompareValidator>
                        <br />
                        <asp:Label ID="lblIncCurPwd" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                            Text="Incorrect Current Password" Visible="false" ForeColor="red"></asp:Label>
                    </div>
                    <div class="col-md-2">
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-5">
                    </div>
                    <div class="col-md-5" align="left">
                        <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="btn btn-primary"
                            Width="75px" TabIndex="4" OnClick="btnSubmit_Click" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReset" Text="Reset" runat="server" CausesValidation="false" CssClass="btn btn-primary"
                            Width="75px" TabIndex="5" OnClick="btnReset_Click" />
                    </div>
                    <div class="col-md-2" align="left">
                    </div>
                </div>
            </div>
            <div class="col-md-1">
            </div>

        </div>
        <br />
        <br />
        <br />
    </div>
</asp:Content>


