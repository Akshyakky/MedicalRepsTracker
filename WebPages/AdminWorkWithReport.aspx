<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPagePrivate.master" AutoEventWireup="true" CodeFile="AdminWorkWithReport.aspx.cs" Inherits="WebPages_StockOutReport" %>

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
                <h2>RSM or BM Work With Medical Rep Report</h2>
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
                                        <asp:Label ID="Label8" runat="server" CssClass="LabelStyles" Text="Medical Rep Name : "></asp:Label>


                                        <asp:TextBox TabIndex="1" runat="server" ID="txtmedrepName" CssClass="form-control TextBox"
                                            placeholder="Medical Rep Name" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                            Enabled="True" TargetControlID="txtmedrepName" ValidChars=" .',()-/">
                                        </cc1:FilteredTextBoxExtender>

                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="Label7" runat="server" CssClass="LabelStyles" Text="RSM Name : "></asp:Label>

                                        <asp:TextBox TabIndex="1" runat="server" ID="txtSrchRSMName" CssClass="form-control TextBox"
                                            placeholder="RSM Name" />
                                      
                                    </div>

                                    <div class="col-md-4" runat="server">
                                        <asp:Label ID="Label1" runat="server" CssClass="LabelStyles" Text="BM Name : "></asp:Label>


                                        <asp:TextBox TabIndex="1" runat="server" ID="txtSrchBMName" CssClass="form-control TextBox"
                                            placeholder="BM Name" />
                                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                            Enabled="True" TargetControlID="txtSrchBMName" ValidChars=" .',()-/">
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
                            <div class="col-md-4">
                                 <asp:Label ID="Label5" runat="server" CssClass="LabelStyles" Text="Work With : "></asp:Label>
                                <asp:RadioButtonList ID="rbdWorkWith" runat="server">
                                    <asp:ListItem Selected="True">Both</asp:ListItem>
                                    <asp:ListItem>Branch Manager</asp:ListItem>
                                    <asp:ListItem>RSM</asp:ListItem>
                                 </asp:RadioButtonList>
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
                                    TabIndex="7" Style="display: inline-block">
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
                    <div class="row" runat="server" id="dvview">
                        <div class="col-md-12" align="center">
                            <asp:Label ID="lblHeading" Visible="False" runat="server" Font-Bold="True" Text="RSM or BM Work With Medical Rep Report" Font-Size="Large"></asp:Label>
                        </div>
                        <div class="col-md-12" style="overflow-x: scroll">
                            <asp:GridView ID="gvStockOutReport" runat="server" AutoGenerateColumns="False" CssClass="Admingridtable"
                                EmptyDataText="No Records Found!!!" Width="100%" EnableModelValidation="True" OnRowDataBound="gvStockOutReport_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <asp:Label ID="slno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="20px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rep. Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvNo" runat="server" Text='<%# Eval("RepName")%>'
                                                Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="110px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RSM Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSource2" runat="server" Text='<%# Eval("RSMName")%>'
                                                Style="word-break: break-all; word-wrap: break-word;"></asp:Label>

                                        </ItemTemplate>
                                        <ControlStyle Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BM Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSource3" runat="server" Text='<%# Eval("BMName")%>'
                                                Style="word-break: break-all; word-wrap: break-word;"></asp:Label>

                                        </ItemTemplate>
                                        <ControlStyle Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Visit Date & time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupName" runat="server" Text='<%# Eval("visitDate")%>'
                                                Style="word-break: break-all; word-wrap: break-word;"></asp:Label><br />
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Visittime")%>'
                                                Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="110px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Trans_Status")%>'
                                                Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="110px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Comments">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("Trans_Extra1")%>' ReadOnly="True" TextMode="MultiLine"></asp:TextBox>

                                        </ItemTemplate>
                                        <ControlStyle Width="110px" />
                                    </asp:TemplateField>


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
  

   
</asp:Content>

