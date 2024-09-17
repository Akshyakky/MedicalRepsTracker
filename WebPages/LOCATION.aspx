<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOCATION.aspx.cs" Inherits="LOCATION_" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js" type="text/javascript"></script>
    <link href="https://bootswatch.com/flatly/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAWyCb1Xq7gDRWSWRnOAVF3VsBz9TQW-og" type="text/javascript"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/gmaps.js/0.4.24/gmaps.min.js" type="text/javascript">
    <script async src="//jsfiddle.net/dannymarkov/ubrvm4ao/embed/"></script>
    </script>
    <style>
        .container {
            max-width: 980px;
            text-align: center;
            margin: 20px auto;
        }

        h1 {
            margin-bottom: 20px;
        }

        #geocoding_form {
            margin: 40px auto 40px;
        }

        .input-group {
            margin-left: 4%;
        }

        .find-me.btn:focus {
            border-color: transparent;
            outline: 0;
        }

        .coordinates .btnsave{
            font-size: 18px;
            opacity: 0;
            margin-bottom: 40px;
        }

        .no-browser-support {
            font-size: 18px;
            opacity: 0;
        }

        .coordinates b:first-child {
            margin-right: 15px;
        }

        .visible {
            opacity: 1;
        }

        .map-overlay {
            max-width: 600px;
            height: 400px;
            margin: 0 auto;
            background-color: #fff;
            position: relative;
            border-radius: 2px;
        }

        #map {
            max-width: 550px;
            height: 400px;
            margin: 0 auto;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" />

        <div>

            <div class="container">

                <h1>Geolocation Demo</h1>


                <div class="form-group">
                    <div class="col-xs-12 col-md-6 col-md-offset-3">
                        <%--<button type="button" class="find-me btn btn-info btn-block">Find My Location</button>--%>
                    </div>
                </div>
             


                <p class="no-browser-support">Sorry, the Geolocation API isn't supported in Your browser.</p>
                <p class="coordinates">Latitude: <b class="latitude">00</b> Longitude: <b class="longitude">00</b></p>
                   <div class="col-xs-12 col-md-6 col-md-offset-3">
                    <asp:Button Text="Save" runat="server" OnClick="Unnamed2_Click1" CssClass="btnsave"/>
                </div>
                <asp:HiddenField ID="hdlatitude" runat="server" />
                <asp:HiddenField ID="hdlongitude" runat="server" />
                <div class="map-overlay">
                    <div id="map"></div>
                </div>
                <%-- <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:Button Text="Address" runat="server" OnClick="Unnamed2_Click" />
                        <br />
                        <asp:Label ID="lblLocation" runat="server"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>

        </div>
      <%--  <script>
            var findMeButton = $('.find-me');

            // Check if the browser has support for the Geolocation API
            if (!navigator.geolocation) {

                findMeButton.addClass("disabled");
                $('.no-browser-support').addClass("visible");

            } else {

                findMeButton.on('click', function (e) {

                    e.preventDefault();

                    navigator.geolocation.getCurrentPosition(function (position) {

                        // Get the coordinates of the current possition.
                        var lat = position.coords.latitude;
                        var lng = position.coords.longitude;

                        $('.latitude').text(lat.toFixed(6));
                        $('.longitude').text(lng.toFixed(6));
                        $('.coordinates').addClass('visible');

                        $("#hdlatitude").val(lat.toFixed(6));
                        $("#hdlongitude").val(lng.toFixed(6));
                        $('.btnsave').addClass("visible");

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

                });

            }
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

                    $("#hdlatitude").val(lat.toFixed(6));
                    $("#hdlongitude").val(lng.toFixed(6));


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
    </form>
</body>
</html>
