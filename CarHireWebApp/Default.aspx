<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CarHireWebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--<style type="text/css">
      html, body, #map-canvas { height: 500px; margin: 20px; padding: 0;}
    </style>--%>
    <%--<script type="text/javascript"
        src="https://maps.googleapis.com/maps/api/js?key=YOURKEY">
    </script>--%>
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&signed_in=true&libraries=places"></script>
    <script type="text/javascript">
        var JSONLocations = '<%=new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(locations)%>';
        var JSONOpeningTimes = '<%=new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(openingTimes)%>';
        var JSONHolidayOpeningTimes = '<%=new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(holidayOpeningTimes)%>';

        function viewMap() {
            document.getElementById("map-canvas").style.display = "block";
            google.maps.event.trigger(map, "resize");
        }

        function hideMap() {
            document.getElementById("map-canvas").style.display = "none";
        }
    </script>
    <script src="Scripts/mapandlocations.js" type="text/javascript"></script>

    <script type="text/javascript">
        var myImages = new Array();
        var ctr = 0;


        myImages[0] = 'Images/Cars Lined Up.JPG';
        myImages[1] = 'Images/Car Driving Into Sunset.jpg';
        myImages[2] = 'Images/Nice Road.jpg';


        function changeImage(img) {
            var img = document.getElementById('slideImage');
            if (img) {

                $('#slideImage').fadeOut(500, function () {
                    $('#slideImage').fadeIn(500);
                });

                img.src = myImages[ctr];
                var pictureClick = document.getElementById('slideshowHyperlink');
                var pictureTxt = document.getElementById("pictureText");
                //Sets the page that the user is redirect to depending on which picture is clicked
                if (ctr == 0) {
                    pictureClick.setAttribute('href', 'http://localhost:2443/Vehicles');
                    pictureTxt.innerHTML = "Take a look at the different vehicles available.";
                }
                else if (ctr == 1) {
                    pictureClick.setAttribute('href', 'http://localhost:2443/AvailableVehicles');
                    pictureTxt.innerHTML = "Find a location near to you and book your vehicle.";
                }
                else if (ctr == 2) {
                    pictureClick.setAttribute('href', 'http://localhost:2443/ViewLocations');
                    pictureTxt.innerHTML = "Choose from a wide variety of different locations from across the globe.";
                }


                if (ctr == 2) ctr = 0; else ctr++;
            }
            myTimeout();
        }


        function myTimeout() {
            var t = setTimeout('changeImage();', 5000);
        }

        $(function () {

            $("h2")
                .wrapInner("<span>")

            $("h2 br")
                .before("<span class='spacer'>")
                .after("<span class='spacer'>");

        });
	</script>

    <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <div class="spacer"></div>
    <input class="form-control" type="text" id="addressInputTxt" placeholder="Enter your address for nearest dealer" onfocus="viewMap()" style="width: 350px;" />

    <div id="map-canvas" class="imagesize"></div>

    <div class="spacer"></div>
    <a id="slideshowHyperlink" href="http://localhost:2443/ViewLocations"">
        <div class="image">
            <p><img id="slideImage" class="imagesize" alt="slideshow" src="Images/Nice Road.jpg" /></p>

            <h2><span id="pictureText">Choose from a wide variety of different locations from across the globe</span></h2>
        </div>
    </a>

    <script type="text/javascript">
        myTimeout();
	</script>
</asp:Content>
