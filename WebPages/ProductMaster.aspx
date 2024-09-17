﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPagePrivate.master"
    AutoEventWireup="true" CodeFile="ProductMaster.aspx.cs" Inherits="WebPages_ItemCategory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../AspxStyles/js/Location.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid" width="50%" style="background-color: White">
        <br />
        <div class="row">
            <div class="col-md-12" align="center">
                <h2>Product Master</h2>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <hr style="border: 1px solid #960e00" />
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-1">
            </div>
            <div class="col-md-10">
                <div runat="server" id="divAddItems" visible="false">
                    <div class="row">
                        <div class="col-md-12" align="right">
                            <asp:Button ID="btnAddView" runat="server" Text="View" Width="70px" CausesValidation="false"
                                CssClass="btn btn-primary" TabIndex="16" OnClick="btnAddView_Click" />
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
                        <div class="col-md-4" runat="server" id="divBrnch">
                            <asp:Label ID="Label1" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                Text="*" ForeColor="red"></asp:Label>
                            <asp:Label ID="Label8" runat="server" CssClass="LabelStyles" Text="Branch : "></asp:Label>
                            <asp:DropDownList ID="ddlBranch" CssClass="form-control TextBox" runat="server" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranch"
                                CssClass="LabelStyleRequired" ErrorMessage="Select Branch" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                        </div>
                          <div class="col-md-4" runat="server" id="divMedReps">
                            <asp:Label ID="Label9" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                Text="*" ForeColor="red"></asp:Label>
                            <asp:Label ID="Label10" runat="server" CssClass="LabelStyles" Text="Medical Rep Name : "></asp:Label>
                            <asp:DropDownList ID="ddlMedReps" CssClass="form-control TextBox" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlMedReps"
                                CssClass="LabelStyleRequired" ErrorMessage="Select Medical Rep Name" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="Label23" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                Text="*" ForeColor="red"></asp:Label>
                            <asp:Label ID="Label7" runat="server" CssClass="LabelStyles" Text="Item Name : "></asp:Label>
                            <asp:TextBox TabIndex="1" runat="server" ID="txtProductName" CssClass="form-control TextBox"
                                placeholder="Item Name" AutoPostBack="True" OnTextChanged="txtProductName_TextChanged" />
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                Enabled="True" TargetControlID="txtProductName" ValidChars=" .',()-/">
                            </cc1:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProductName"
                                CssClass="LabelStyleRequired" ErrorMessage="Enter Item Name" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="Label4" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                Text="*" ForeColor="red"></asp:Label>
                            <asp:Label ID="Label5" runat="server" CssClass="LabelStyles" Text="Quantity :"></asp:Label>
                            <asp:TextBox TabIndex="1" runat="server" ID="txtQty" CssClass="form-control TextBox"
                                placeholder="Quantity" />
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom,Numbers"
                                Enabled="True" TargetControlID="txtQty" ValidChars="">
                            </cc1:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQty"
                                CssClass="LabelStyleRequired" ErrorMessage="Enter Quantity" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">

                            <asp:Label ID="Label6" runat="server" CssClass="LabelStyles" Text="Description : "></asp:Label>
                            <asp:TextBox TabIndex="1" runat="server" ID="txtDescription" CssClass="form-control TextBox"
                                placeholder="Description" TextMode="MultiLine" />
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                Enabled="True" TargetControlID="txtDescription" ValidChars=" .',()-/">
                            </cc1:FilteredTextBoxExtender>

                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <br />
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary"
                                TabIndex="3" OnClick="btnSubmit_Click" />
                            &nbsp;&nbsp;<asp:Button ID="btnReset" runat="server" CausesValidation="False" CssClass="btn btn-primary"
                                Text="Reset" TabIndex="4" OnClick="btnReset_Click" />
                        </div>
                    </div>
                    <br />
                    <br>
                </div>
                <div id="divViewItem" runat="server">
                    <div class="row">
                        <div class="col-md-12" align="right">
                            <asp:Button ID="BtnAdd" runat="server" Text="Add" Width="70px" CausesValidation="false"
                                CssClass="btn btn-primary" TabIndex="28" OnClick="BtnAdd_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <h4 align="left">Search By</h4>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="row">
                         
                        <div class="col-md-4">

                            <asp:Label ID="Label12" runat="server" CssClass="LabelStyles" Text="Item Name : "></asp:Label>
                            <asp:TextBox TabIndex="1" runat="server" ID="txtSrchProductName" CssClass="form-control TextBox"
                                placeholder="Item Name" />
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                Enabled="True" TargetControlID="txtSrchProductName" ValidChars=" .',()-/">
                            </cc1:FilteredTextBoxExtender>

                        </div>
                         <div class="col-md-4">
                        
                            <asp:Label ID="Label11" runat="server" CssClass="LabelStyles" Text="Medical Rep Name : "></asp:Label>
                            <asp:TextBox TabIndex="1" runat="server" ID="txtSrchMedicalName" CssClass="form-control TextBox"
                                placeholder="Medical Rep Name" />
                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                Enabled="True" TargetControlID="txtSrchMedicalName" ValidChars=" .',()-/">
                            </cc1:FilteredTextBoxExtender>
                           
                        </div>
                        <div class="col-md-4">

                            <asp:Label ID="Label14" runat="server" CssClass="LabelStyles" Text="Quantity : "></asp:Label>
                            <asp:TextBox TabIndex="1" runat="server" ID="txtSrchQty" CssClass="form-control TextBox"
                                placeholder="Quantity" />
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom,Numbers"
                                Enabled="True" TargetControlID="txtSrchQty" ValidChars="">
                            </cc1:FilteredTextBoxExtender>

                        </div>
                        <div class="col-md-4">

                            <asp:Label ID="Label16" runat="server" CssClass="LabelStyles" Text="Description : "></asp:Label>
                            <asp:TextBox TabIndex="1" runat="server" ID="txtSrchDescription" CssClass="form-control TextBox"
                                placeholder="Description" TextMode="MultiLine" />
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                Enabled="True" TargetControlID="txtSrchDescription" ValidChars=" .',()-/">
                            </cc1:FilteredTextBoxExtender>

                        </div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-12">
                                    <br />
                                </div>
                            </div>
                            <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Show "></asp:Label>
                            <asp:DropDownList ID="ddlShowTop" runat="server" CssClass="form-control" Width="40%"
                                TabIndex="19" Style="display: inline-block">
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
                            <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Records "></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-12">
                                    <br />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                                        TabIndex="20" OnClick="btnSearch_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="btn btn-primary"
                                        TabIndex="21" OnClick="btnRefresh_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12" align="center">
                            <asp:Label ID="lblRecordsCount" runat="server" Font-Bold="True" Text="Records Found out of"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" align="center" style="overflow-x: scroll">
                            <div>
                                <asp:GridView ID="gvDoctorDetail" runat="server" AutoGenerateColumns="False" CssClass="Admingridtable"
                                    EmptyDataText="No Records Found!!!" Width="100%" EnableModelValidation="True"
                                    OnRowCommand="gvItems_RowCommand" OnRowDataBound="gvItems_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" ToolTip="Edit" CausesValidation="false"
                                                    runat="server" CommandArgument='<%# Eval("Prod_Mid") + "," + Eval("Prod_Name") %>'
                                                    CommandName="btnEdit"> <i class="fa fa-pencil" style="color:Black;font-size:1.3em;"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <ControlStyle Width="20px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:Label ID="slno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="20px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category MID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCatMID" runat="server" Text='<%# Eval("Prod_Mid") %>' Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDoctorname" runat="server" Text='<%# Eval("Item_Name") %>' Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="150px" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="MP Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDoctorname1" runat="server" Text='<%# Eval("MP_Name") %>' Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDoctorLoc" runat="server" Text='<%# Eval("Prod_Qty") %>' Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMeasuringUnit" runat="server" Text='<%# Eval("Prod_Description") %>'
                                                    Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="110px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDel" CausesValidation="false" ToolTip="Delete" runat="server"
                                                    CommandArgument='<%# Eval("Prod_Mid") + "," + Eval("Prod_Name") %>' CommandName="btnDelete"> <i class="fa fa-trash" style="color:Red;font-size:1.5em;" onclick="return confirm('Are you sure want to delete? ');"></i></asp:LinkButton>

                                            </ItemTemplate>
                                            <ControlStyle Width="60px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-1">
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
            LoadAllFunctions1();
        });
        function LoadAllFunctions1() {
            $("#<% =txtProductName.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "../Items.ashx?q=" + document.getElementById('<% =txtProductName.ClientID %>').value,
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

    <script type="text/javascript">
        $(document).ready(function () {
            LoadAllFunctions();
        });
        function LoadAllFunctions() {
            $("#<% =txtSrchProductName.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "../Items.ashx?q=" + document.getElementById('<% =txtSrchProductName.ClientID %>').value,
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
