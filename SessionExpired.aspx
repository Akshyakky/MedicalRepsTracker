<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPagePublic.master"
    AutoEventWireup="true" CodeFile="SessionExpired.aspx.cs" Inherits="SessionExpired" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <style>
       
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="Server">
    <div class="a">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <table align="center" width="90%">
                        <tr>
                            <td align="center" height="100">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/ASPXImages/SessionExpire.png" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">&nbsp;
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="20pt" Text="Session Expired..."
                                CssClass="LabelStyle"></asp:Label>
                                <br />
                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="18pt" Text="Please Try Again..."
                                    CssClass="LabelStyle"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" height="40">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" height="40">
                                <asp:LinkButton ID="lnkLogin" runat="server" CssClass="LabelStyleDisp"
                                    Font-Bold="True" OnClick="lnkLogin_Click">Click Here To Re-Login</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" height="100">&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
