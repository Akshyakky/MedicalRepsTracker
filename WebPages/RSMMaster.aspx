<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPagePrivate.master" AutoEventWireup="true" CodeFile="RSMMaster.aspx.cs" Inherits="WebPages_RSMMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../AspxStyles/js/Location.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" Runat="Server">
     <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <div class="container-fluid" width="50%" style="background-color: White">
            <br />
        <div class="row">
            <div class="col-md-12" align="center">
                <h2>
                    RSM Master</h2>
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
                        <div class="col-md-4">
                            <asp:Label ID="Label23" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                Text="*" ForeColor="red"></asp:Label>
                            <asp:Label ID="Label7" runat="server" CssClass="LabelStyles" Text="Region Name : "></asp:Label>
                            <asp:TextBox TabIndex="1" runat="server" ID="txtRegionName" CssClass="form-control TextBox"
                                placeholder="Region Name" />
                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                Enabled="True" TargetControlID="txtRegionName" ValidChars=" .',()-/">
                            </cc1:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRegionName"
                                CssClass="LabelStyleRequired" ErrorMessage="Enter Region Name" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                          <div class="col-md-4">
                            <asp:Label ID="Label4" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                Text="*" ForeColor="red"></asp:Label>
                            <asp:Label ID="Label5" runat="server" CssClass="LabelStyles" Text="RSM Name : "></asp:Label>
                            <asp:TextBox TabIndex="2" runat="server" ID="txtRSMName" CssClass="form-control TextBox"
                                placeholder="RSM Name" />
                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                Enabled="True" TargetControlID="txtRSMName" ValidChars=" .',()-/">
                            </cc1:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRSMName"
                                CssClass="LabelStyleRequired" ErrorMessage="Enter RSM Name" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                            <div class="col-md-4">
                            <asp:Label ID="Label8" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                Text="*" ForeColor="red"></asp:Label>
                            <asp:Label ID="Label9" runat="server" CssClass="LabelStyles" Text="Username : "></asp:Label>
                            <asp:TextBox TabIndex="3" runat="server" ID="txtUsername" CssClass="form-control TextBox"
                                placeholder="Username" MaxLength="35" />
                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                Enabled="True" TargetControlID="txtUsername" ValidChars=" .',()-/">
                            </cc1:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtUsername"
                                CssClass="LabelStyleRequired" ErrorMessage="Enter Username" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <br />
                     <div class="row">
                   
                        
                     
                          <div class="col-md-4" runat="server" id="DivPassword">
                            <asp:Label ID="Label11" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                Text="*" ForeColor="red"></asp:Label>
                            <asp:Label ID="Label13" runat="server" CssClass="LabelStyles" Text="Password: "></asp:Label>
                            <asp:TextBox TabIndex="4" runat="server" ID="txtPassword" CssClass="form-control TextBox"
                                placeholder="Password" TextMode="Password" />
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                Enabled="True" TargetControlID="txtPassword" ValidChars="< ><.><'><,><(><)>">
                            </cc1:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPassword"
                                CssClass="LabelStyleRequired" ErrorMessage="Enter Password" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                          <div class="col-md-4" runat="server" id="DivRenterpassword">
                            <asp:Label ID="Label6" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                Text="*" ForeColor="red"></asp:Label>
                            <asp:Label ID="Label10" runat="server" CssClass="LabelStyles" Text="Re Enter Password: "></asp:Label>
                            <asp:TextBox TabIndex="5" runat="server" ID="txtCnfrmPaswrd" CssClass="form-control TextBox"
                                placeholder="Re Enter Password" TextMode="Password" />
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                Enabled="True" TargetControlID="txtCnfrmPaswrd" ValidChars="< ><.><'><,><(><)>">
                            </cc1:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCnfrmPaswrd"
                                CssClass="LabelStyleRequired" ErrorMessage="Re Enter Password" SetFocusOnError="True"></asp:RequiredFieldValidator>
                               <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword"
                                    ControlToValidate="txtCnfrmPaswrd" ErrorMessage="Password Mismatch"></asp:CompareValidator>
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
                            <h4 align="left">
                                Search By</h4>
                        </div>
                    </div>
                    <br />
                            <br />
                    <div class="row">
                        <div class="col-md-4">
                        
                            <asp:Label ID="Label12" runat="server" CssClass="LabelStyles" Text="Region Name : "></asp:Label>
                            <asp:TextBox TabIndex="1" runat="server" ID="txtSrchRegionName" CssClass="form-control TextBox"
                                placeholder="Region Name" />
                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                Enabled="True" TargetControlID="txtSrchRegionName" ValidChars=" .',()-/">
                            </cc1:FilteredTextBoxExtender>
                           
                        </div>
                         
                           <div class="col-md-4">
                        
                            <asp:Label ID="Label1" runat="server" CssClass="LabelStyles" Text="RSM Name : "></asp:Label>
                            <asp:TextBox TabIndex="2" runat="server" ID="txtSearchRSMName" CssClass="form-control TextBox"
                                placeholder="RSM Name" />
                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                Enabled="True" TargetControlID="txtSearchRSMName" ValidChars=" .',()-/">
                            </cc1:FilteredTextBoxExtender>
                           
                        </div>
                             <div class="col-md-4">
                           
                                 <asp:Label ID="Label20" runat="server" CssClass="LabelStyles" Text="Username : "></asp:Label>
                            <asp:TextBox ID="txtSrchUsername" runat="server" ForeColor="Black" CssClass="form-control TextBox"
                                MaxLength="10" TabIndex="3" placeholder="Username" ></asp:TextBox>
                               <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                Enabled="True" TargetControlID="txtSrchUsername" ValidChars=" .',()-/">
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
                            <asp:Label ID="lblRecordsCount" runat="server" Font-Bold="true" Text="Records Found of"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" align="center" style="overflow-x: scroll">
                            <div>
                                <asp:GridView ID="gvRSMDetail" runat="server" AutoGenerateColumns="False" CssClass="Admingridtable"
                                    EmptyDataText="No Records Found!!!" Width="100%" EnableModelValidation="True"
                                    OnRowCommand="gvItems_RowCommand" onrowdatabound="gvItems_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" ToolTip="Edit" CausesValidation="false"
                                                    runat="server" CommandArgument='<%# Eval("RSM_Mid") + "," + Eval("RSM_UserName") %>'
                                                    CommandName="btnEdit"> <i class="fa fa-pencil" style="color:Black;font-size:1.3em;" onclick="return confirm('Are you sure want to edit? ');"></i></asp:LinkButton>
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
                                                <asp:Label ID="lblCatMID" runat="server" Text='<%# Eval("RSM_Mid") %>' Style="word-break: break-all;
                                                    word-wrap: break-word;"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Region Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegionName" runat="server" Text='<%# Eval("RSM_RegionName") %>' Style="word-break: break-all;
                                                    word-wrap: break-word;"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="150px" />
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderText="RSM Name">
                                          <ItemTemplate>
                                                <asp:Label ID="lblRSMName" runat="server" Text='<%# Eval("RSM_Name") %>' Style="word-break: break-all;
                                                    word-wrap: break-word;"></asp:Label>
                                            </ItemTemplate>
                                           
                                        </asp:TemplateField>
                                              <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDoctorLoc" runat="server" Text='<%# Eval("RSM_UserName") %>' Style="word-break: break-all;
                                                    word-wrap: break-word;"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="150px" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDel" CausesValidation="false" ToolTip="Delete" runat="server"
                                                    CommandArgument='<%# Eval("RSM_Mid") + "," + Eval("RSM_UserName") %>' CommandName="btnDelete"> <i class="fa fa-trash" style="color:Red;font-size:1.5em;" onclick="return confirm('Are you sure want to delete? ');"></i></asp:LinkButton>
                                      
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
     <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyDKYwbvCA7TBFyfqYhTOLwi6PT6N61oSag&libraries=places"></script>  
    <script type="text/javascript">
        var source, destination;

        var directionsService = new google.maps.DirectionsService();
        google.maps.event.addDomListener(window, 'load', function () {
            new google.maps.places.SearchBox(document.getElementById('ctl00_Body_txtLocation'));
            new google.maps.places.SearchBox(document.getElementById('ctl00_Body_txtSrchLocation'));


        });
</script>  
</asp:Content>

