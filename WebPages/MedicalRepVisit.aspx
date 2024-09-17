<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPagePrivate.master" AutoEventWireup="true" CodeFile="MedicalRepVisit.aspx.cs" Inherits="WebPages_MedicalRepVisit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .coordinates
        {
            font-size: 18px;
            opacity: 0;
            margin-bottom: 40px;
        }

        .no-browser-support
        {
            font-size: 18px;
            opacity: 0;
        }

        .find-me.btn:focus
        {
            border-color: transparent;
            outline: 0;
        }

        .visible
        {
            opacity: 1;
        }
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js" type="text/javascript"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAWyCb1Xq7gDRWSWRnOAVF3VsBz9TQW-og" type="text/javascript"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/gmaps.js/0.4.24/gmaps.min.js" type="text/javascript" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="container-fluid" width="50%" style="background-color: White">
        <br />
        <div class="row">
            <div class="col-md-12" align="center">
                <h2>Medical Reps Visits</h2>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <hr style="border: 1px solid #960e00" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12" align="center">
                <button type="button" class="find-me btn btn-primary">Find My Location</button>
                <p class="no-browser-support">Sorry, the Geolocation API isn't supported in Your browser.</p>
                <p class="coordinates">Latitude: <b class="latitude">00</b> Longitude: <b class="longitude">00</b></p>
                <asp:HiddenField ID="hdlatitude" runat="server" />
                <asp:HiddenField ID="hdlongitude" runat="server" />
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-1">
            </div>
            <div class="col-md-10">
                <div runat="server" id="divAddItems">

                    <div class="row">
                        <div class="col-md-12" align="center">
                            <asp:Label ID="Label41" runat="server" CssClass="LabelStyles" Style="color: red"
                                Text="(*) Fields are Mandatory"></asp:Label>
                        </div>
                    </div>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row" style="border: solid; border-width: thin; ; padding-bottom: 10px;">

                                <div class="col-md-4">

                                    <asp:Label ID="Label25" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                        Text="*" ForeColor="red"></asp:Label>
                                    <asp:Label ID="Label26" runat="server" CssClass="LabelStyles" Text="Have you worked with RSM or Branch Manager?"></asp:Label>
                                    <asp:RadioButtonList ID="rbdRSMorBM" runat="server" AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="rbdRSMorBM_SelectedIndexChanged">

                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                        <asp:ListItem>Branch Manager</asp:ListItem>
                                        <asp:ListItem>RSM</asp:ListItem>
                                        <asp:ListItem>Both</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="rdbType"
                                        CssClass="LabelStyleRequired" ErrorMessage="Select Yes/No" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                </div>


                                <div class="col-md-4" runat="server" id="divBranchM" visible="false">
                                    <asp:Label ID="Label27" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                        Text="*" ForeColor="red"></asp:Label>
                                    <asp:Label ID="Label28" runat="server" CssClass="LabelStyles" Text="Branch Manager : "></asp:Label>
                                    <asp:DropDownList ID="ddlBM" CssClass="form-control TextBox" runat="server" TabIndex="2"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlBM"
                                        CssClass="LabelStyleRequired" ErrorMessage="Select Branch Manager" SetFocusOnError="True" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4" runat="server" id="divRSM" visible="false">
                                    <asp:Label ID="Label29" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                        Text="*" ForeColor="red"></asp:Label>
                                    <asp:Label ID="Label30" runat="server" CssClass="LabelStyles" Text="RSM : "></asp:Label>
                                    <asp:DropDownList ID="ddlRSM" CssClass="form-control TextBox" runat="server" TabIndex="2"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlRSM"
                                        CssClass="LabelStyleRequired" ErrorMessage="Select RSM" SetFocusOnError="True" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>

                            </div>
                            <%-- <script type="text/javascript" language="javascript">
                                   Sys.Application.add_load(LoadAllFunctions);
                                </script> --%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="row">

                                <div class="col-md-4">
                                    <asp:Label ID="Label23" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                        Text="*" ForeColor="red"></asp:Label>
                                    <asp:Label ID="Label7" runat="server" CssClass="LabelStyles" Text="Select Doctor/<br>Chemist/Stockiest :"></asp:Label>
                                    <asp:RadioButtonList ID="rdbType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdbType_SelectedIndexChanged" TabIndex="1">
                                        <asp:ListItem Selected="True"> Doctor</asp:ListItem>
                                        <asp:ListItem>Chemist</asp:ListItem>
                                        <asp:ListItem>Stockiest</asp:ListItem>
                                    </asp:RadioButtonList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdbType"
                                        CssClass="LabelStyleRequired" ErrorMessage="Select Doctor/Chemist/Stockiest" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>


                                <div class="col-md-4" runat="server" id="divDoctor">
                                    <asp:Label ID="Label1" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                        Text="*" ForeColor="red"></asp:Label>
                                    <asp:Label ID="Label6" runat="server" CssClass="LabelStyles" Text="Doctor : "></asp:Label>
                                    <asp:TextBox ID="txtDoctor" runat="server" ForeColor="Black" CssClass="form-control TextBox" AutoPostBack="True"
                                        MaxLength="10" TabIndex="2" placeholder="Doctor" OnTextChanged="txtDoctor_TextChanged"></asp:TextBox>
                                    <%--  <asp:DropDownList ID="ddlDoctor" CssClass="form-control TextBox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDoctor_SelectedIndexChanged" TabIndex="2"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDoctor"
                                        CssClass="LabelStyleRequired" ErrorMessage="Select Doctor" SetFocusOnError="True" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-md-4" runat="server" id="DivAskMeetChemist">
                                    <asp:Label ID="Label12" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                        Text="*" ForeColor="red"></asp:Label>
                                    <asp:Label ID="Label14" runat="server" CssClass="LabelStyles" Text="Ask to meet Chemist :"></asp:Label>
                                    <asp:RadioButtonList ID="rdbAMeet" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdbAMeet_SelectedIndexChanged" TabIndex="3">
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="rdbAMeet"
                                        CssClass="LabelStyleRequired" ErrorMessage="Select Yes/No" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4" runat="server" id="divChemist" visible="false">
                                    <asp:Label ID="Label11" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                        Text="*" ForeColor="red"></asp:Label>
                                    <asp:Label ID="Label13" runat="server" CssClass="LabelStyles" Text="Chemist : "></asp:Label>
                                    <asp:DropDownList ID="ddlChemist" CssClass="form-control TextBox" runat="server" TabIndex="4"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlChemist"
                                        CssClass="LabelStyleRequired" ErrorMessage="Select Chemist" SetFocusOnError="True" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4" runat="server" id="divStockiest" visible="false">
                                    <asp:Label ID="Label15" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                        Text="*" ForeColor="red"></asp:Label>
                                    <asp:Label ID="Label17" runat="server" CssClass="LabelStyles" Text="Stockiest : "></asp:Label>
                                    <asp:DropDownList ID="ddlStockiest" CssClass="form-control TextBox" runat="server" TabIndex="5"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlStockiest"
                                        CssClass="LabelStyleRequired" ErrorMessage="Select Stockiest" SetFocusOnError="True" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>

                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>

                            <div class="row">
                                <div class="col-md-4" style="z-index: 9999;">
                                    <asp:Label ID="Label4" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                        Text="*" ForeColor="red"></asp:Label>
                                    <asp:Label ID="Label5" runat="server" CssClass="LabelStyles" Text="Visit Date : "></asp:Label>
                                    <asp:TextBox TabIndex="6" runat="server" ID="txtVisitDate" CssClass="form-control TextBox"
                                        placeholder="Visit Date" OnTextChanged="txtVisitDate_TextChanged" />
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,Numbers"
                                        Enabled="True" TargetControlID="txtVisitDate" ValidChars="/">
                                    </cc1:FilteredTextBoxExtender>
                                    <cc1:CalendarExtender ID="cc" OnClientDateSelectionChanged="checkDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtVisitDate">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtVisitDate"
                                        CssClass="LabelStyleRequired" ErrorMessage="Enter Visit Date" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator2" runat="server" ValidationGroup="Submit" ControlToValidate="txtVisitDate" CssClass="LabelStyleValidation"
                                        ErrorMessage="Enter Valid Date (dd/MM/yyyy)" ForeColor="red" SetFocusOnError="true"
                                        ValidationExpression="(0[1-9]|[12][0-9]|3[01])[-/](0[1-9]|1[012])[-/]\d{4}"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="Label16" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                        Text="*" ForeColor="red"></asp:Label>
                                    <asp:Label ID="Label18" runat="server" CssClass="LabelStyles" Text="Visit Time :"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlTime" runat="server" TabIndex="7">
                                        <asp:ListItem Value="0">HH</asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>

                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlMints" runat="server" TabIndex="8">
                                        <asp:ListItem Value="0">MM</asp:ListItem>
                                        <asp:ListItem>00</asp:ListItem>

                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>45</asp:ListItem>

                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlAMPM" runat="server" TabIndex="9">
                                        <asp:ListItem>AM</asp:ListItem>
                                        <asp:ListItem>PM</asp:ListItem>


                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlTime"
                                        CssClass="LabelStyleRequired" ErrorMessage="Enter Hour" SetFocusOnError="True" ValidationGroup="Submit" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlMints"
                                        CssClass="LabelStyleRequired" ErrorMessage="Enter Mints" SetFocusOnError="True" ValidationGroup="Submit" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div id="Div1" class="col-md-4" runat="server" visible="false">
                                    <asp:Label ID="Label2" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                        Text="*" ForeColor="red"></asp:Label>
                                    <asp:Label ID="Label3" runat="server" CssClass="LabelStyles" Text="Location "></asp:Label>
                                    <asp:TextBox TabIndex="10" runat="server" ID="txtLocation" CssClass="form-control TextBox"
                                        placeholder="Location" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtLocation"
                                        CssClass="LabelStyleRequired" ErrorMessage="Enter Location" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-4">
                                    <asp:Label ID="Label19" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                        Text="*" ForeColor="red"></asp:Label>
                                    <asp:Label ID="Label21" runat="server" CssClass="LabelStyles" Text="Select Medicine Given : "></asp:Label>
                                    <asp:RadioButtonList ID="rdbMedGiven" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdbMedGiven_SelectedIndexChanged" TabIndex="11">
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="rdbMedGiven"
                                        CssClass="LabelStyleRequired" ErrorMessage="Select Medicine Given" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>

                            </div>
                            <br />
                            <div class="row" runat="server" id="divMedDetails" visible="false">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-6" align="center">

                                    <h4 style="color: #3274b9">Add Medicine Details</h4>

                                </div>
                                <div class="col-md-3">
                                </div>
                            </div>
                            <br />
                        </ContentTemplate>
                      
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="row" runat="server" id="divMedDetails1" visible="false">

                                <div class="col-md-4">
                                    <asp:Label ID="Label111" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                        Text="*" ForeColor="red"></asp:Label>
                                    <asp:Label ID="Label10" runat="server" CssClass="LabelStyles" Text="Medicine Name : "></asp:Label>
                                    <asp:TextBox ID="txtMedicine" runat="server" ForeColor="Black" CssClass="form-control TextBox"
                                        MaxLength="10" TabIndex="12" placeholder="Medicine Name" OnTextChanged="txtMedicine_TextChanged" AutoPostBack="True"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtMobile_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters" TargetControlID="txtMedicine" ValidChars="  ' . -">
                                    </cc1:FilteredTextBoxExtender>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtMedicine"
                                        CssClass="LabelStyleValidation" SetFocusOnError="true" Display="Dynamic" ErrorMessage="Enter Medicine Name" ValidationGroup="Add"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="Label8" runat="server" CssClass="LabelStyleValidation" Font-Bold="True"
                                        Text="*" ForeColor="red"></asp:Label>
                                    <asp:Label ID="Label9" runat="server" CssClass="LabelStyles" Text="Quantity : "></asp:Label>
                                    <asp:TextBox TabIndex="13" runat="server" ID="txtQty" CssClass="form-control TextBox"
                                        placeholder="Quantity" OnTextChanged="txtQty_TextChanged" />

                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom,Numbers"
                                        Enabled="True" TargetControlID="txtQty" ValidChars=" .',()-/">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtQty"
                                        CssClass="LabelStyleRequired" ErrorMessage="Enter Quantity" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <br />
                                    <asp:Label ID="Label22" runat="server" CssClass="LabelStyles" Text="Available Quantity : " ForeColor="#FF0066" Visible="true"></asp:Label>
                                    <asp:Label ID="lblAvailQty" runat="server" CssClass="LabelStyles" Text="0" ForeColor="#FF0066" Visible="true"></asp:Label>
                                    <asp:Label ID="Label20" runat="server" CssClass="LabelStyles" Text="0" ForeColor="#FF0066" Visible="False"></asp:Label>
                                </div>
                            </div>

                            <div class="row" runat="server" visible="false" id="divMedicineView">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-12" align="right">
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" Width="70px" CssClass="btn btn-primary"
                                                TabIndex="16" OnClick="btnAdd_Click1" ValidationGroup="Add" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12" style="overflow-x: scroll">
                                            <asp:GridView ID="gvedicineDetails" runat="server" AutoGenerateColumns="False" CssClass="Admingridtable"
                                                EmptyDataText="No Records Found!!!" Width="100%" EnableModelValidation="True" OnRowCommand="gvStockOutItem_RowCommand" OnRowDeleting="gvStockOutItem_RowDeleting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="slno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ControlStyle Width="20px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Medicine Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProdMid" runat="server" Text='<%# Eval("ProductMid")%>' Style="word-break: break-all; word-wrap: break-word;"
                                                                Visible="false"></asp:Label>
                                                            <asp:Label ID="lblProdName" runat="server" Text='<%# Eval("ProductName")%>' Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                                        </ItemTemplate>
                                                        <ControlStyle Width="150px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty")%>' Style="word-break: break-all; word-wrap: break-word;"></asp:Label>
                                                        </ItemTemplate>
                                                        <ControlStyle Width="110px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remove">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDel" CausesValidation="false" ToolTip="Remove" runat="server"
                                                                CommandName="Delete" CommandArgument='<%# Eval("ProductMid")%>'> <i class="fa fa-minus" style="color:Red;font-size:1.5em;" onclick="return confirm('Are you sure want to remove? ');"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ControlStyle Width="20px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <br />

                        </ContentTemplate>
                          <Triggers>
                          <asp:PostBackTrigger ControlID="btnAdd" />
                        </Triggers>
                    </asp:UpdatePanel>


                    <div class="row" style="border: solid; border-width: thin; padding-top: 10px; padding-bottom: 10px;">
                        <br />
                        <div class="col-md-4"></div>
                        <div class="col-md-4">
                            <asp:Label ID="Label24" runat="server" CssClass="LabelStyles" Text="Comments  : "></asp:Label>
                            <asp:TextBox TabIndex="14" runat="server" ID="txtComments" CssClass="form-control TextBox"
                                placeholder="Comments" TextMode="MultiLine" />
                        </div>
                        <div class="col-md-4"></div>
                        <br />
                    </div>
                    <br />
                    <div class="row">
                        <br />
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary"
                                TabIndex="15" OnClick="btnSubmit_Click" ValidationGroup="Submit" />
                            &nbsp;&nbsp;<asp:Button ID="btnReset" runat="server" CausesValidation="False" CssClass="btn btn-primary"
                                Text="Reset" TabIndex="16" OnClick="btnReset_Click" />
                        </div>
                    </div>
                    <br />

                </div>

            </div>
            <div class="col-md-1">
            </div>
        </div>
    </div>

    <%--   <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDKYwbvCA7TBFyfqYhTOLwi6PT6N61oSag&libraries=places"></script>
    <script type="text/javascript">
        var source, destination;

        var directionsService = new google.maps.DirectionsService();
        google.maps.event.addDomListener(window, 'load', function () {
            new google.maps.places.SearchBox(document.getElementById('ctl00_Body_txtLocation'));
            


        });
    </script>--%>


    <script>
        var iii = 0;
        window.setInterval(function () {
            //alert('test');
            getlocatio();
        }, 5000);
        getlocatio();

        var findMeButton = $('.find-me');

        // Check if the browser has support for the Geolocation API
        //else {

        // findMeButton.on('click', function (e) {
        function getlocatio() {
            // e.preventDefault();
            iii++;
            if (!navigator.geolocation) {

                findMeButton.addClass("disabled");
                $('.no-browser-support').addClass("visible");

            } else {
                navigator.geolocation.getCurrentPosition(function (position) {

                    // Get the coordinates of the current possition.
                    var lat = position.coords.latitude;
                    var lng = position.coords.longitude;

                    $('.latitude').text(lat.toFixed(6));
                    $('.longitude').text(lng.toFixed(6) + '-' + iii);
                    $('.coordinates').addClass('visible');

                    $("#ctl00_Body_hdlatitude").val(lat.toFixed(6));
                    $("#ctl00_Body_hdlongitude").val(lng.toFixed(6));


                    // Create a new map and place a marker at the device location.
                    //var map = new GMaps({
                    //    el: '#map',
                    //    lat: lat,
                    //    lng: lng
                    //});

                    //map.addMarker({
                    //    lat: lat,
                    //    lng: lng
                    //});

                });
            }



        }
    </script>

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
            $("#<% =txtMedicine.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "../Items.ashx?q=" + document.getElementById('<% =txtMedicine.ClientID %>').value,
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

            $("#<% =txtDoctor.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "../Doctors.ashx?q=" + document.getElementById('<% =txtDoctor.ClientID %>').value,
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



            $("#<% =txtLocation.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "../Location.ashx?q=" + document.getElementById('<% =txtLocation.ClientID %>').value,
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

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            LoadAllFunctions(); // re-bind your jQuery events here
        });

    </script>
    <%--   <script type="text/javascript">
         //$(document).ready(function () {
         //    LoadAllFunctions();
         //});

         window.onload = function () {
             LoadDoctorFunctions();
         };
         //On UpdatePanel Refresh
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         if (prm != null) {
             prm.add_endRequest(function (sender, e) {
                 if (sender._postBackSettings.panelsToUpdate != null) {
                     LoadDoctorFunctions();
                 }
             });
         };
         function LoadDoctorFunctions() {
            
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            LoadAllFunctions1();
        });
        function LoadAllFunctions1() {
           
        }

    </script>--%>
    <script type="text/javascript"> function checkDate(sender, args) {
     if (sender._selectedDate > new Date()) {
         alert("You cannot select a day earlier than today!"); sender._selectedDate = new Date(); // set the date back to the current date 
         sender._textbox.set_Value(sender._selectedDate.format(sender._format))
     }
 } </script>
    <br />
</asp:Content>


