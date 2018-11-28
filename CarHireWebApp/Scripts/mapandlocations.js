var map;
var geocoder;
var locationArray;
var distancesArray = [];
function initialize() {    
    var mapOptions = {
        center: { lat: 53.553362, lng: -3.515624 },
        zoom: 6
    };
    map = new google.maps.Map(document.getElementById('map-canvas'),
        mapOptions);
    
    var markers = [];

    var input = /** @type {HTMLInputElement} */(
    document.getElementById('addressInputTxt'));
    //map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    //Don't refresh the page when enter is pressed
    google.maps.event.addDomListener(input, 'keydown', function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
        }
    });

    var searchBox = new google.maps.places.SearchBox(
/** @type {HTMLInputElement} */(input));

    // Listen for the event fired when the user selects an item from the
    // pick list. Retrieve the matching places for that item.
    google.maps.event.addListener(searchBox, 'places_changed', function () {
        var places = searchBox.getPlaces();
        
        if (places.length == 0) {
            return;
        }
        for (var i = 0, marker; marker = markers[i]; i++) {
            marker.setMap(null);
        }
        
        // For each place, get the icon, place name, and location.
        markers = [];
        var bounds = new google.maps.LatLngBounds();
        for (var i = 0, place; place = places[i]; i++) {

            var extendPoint1 = new google.maps.LatLng(place.geometry.location.lat() + 0.03, place.geometry.location.lng() + 0.03);
            var extendPoint2 = new google.maps.LatLng(place.geometry.location.lat() - 0.03, place.geometry.location.lng() - 0.03);
            bounds.extend(extendPoint1);
            bounds.extend(extendPoint2);

            searchDistance(place.geometry.location.lat(), place.geometry.location.lng());
        }

        //Refresh to update markers with the distance away.
        initialize();
        map.fitBounds(bounds);
    });
    // [END region_getplaces]
    
    // Bias the SearchBox results towards places that are within the bounds of the
    // current map's viewport.
    google.maps.event.addListener(map, 'bounds_changed', function () {
        var bounds = map.getBounds();
        searchBox.setBounds(bounds);
    });

    locationArray = JSON.parse(JSONLocations);
    var openingTimesArray = JSON.parse(JSONOpeningTimes);
    var holidayOpeningTimesArray = JSON.parse(JSONHolidayOpeningTimes);

    var tableLayout;

    var infowindow;
    var markerPos;
    var marker;
    var image = 'Images/Car Icon.png';

    for (var i in locationArray) {
        markerPos = new google.maps.LatLng(locationArray[i].Latitude, locationArray[i].Longitude);
        marker = new google.maps.Marker(
            {
                position: markerPos,
                map: map,
                title: locationArray[i].LocationName,
                icon: image
            }
        );
        infowindow = new google.maps.InfoWindow({ maxWidth: 300 });
        tableLayout =               "<table class=\"table table-condensed\">";
        tableLayout = tableLayout + "<tr>";
        tableLayout = tableLayout + "<th>";
        tableLayout = tableLayout + "<span class=\"mappopuptitle\"><a href=\"http://localhost:2443/AvailableVehicles?LocationID=" + locationArray[i].LocationID + "\">" + locationArray[i].LocationName + "</span>";
        tableLayout = tableLayout + "</th>";
        tableLayout = tableLayout + "</tr>";
        tableLayout = tableLayout + "<tr>";
        tableLayout = tableLayout + "<th>";
        tableLayout = tableLayout + "<span class=\"rightfloat\">" + locationArray[i].OwnerName + "</span>";
        tableLayout = tableLayout + "</th>";
        tableLayout = tableLayout + "</tr>";
        tableLayout = tableLayout + "</table>";

        tableLayout = tableLayout + "<table class=\"table table-condensed\">";
        //Only show distance if location is entered
        if (distancesArray.length > 0) {
            tableLayout = tableLayout + "<tr>";
            tableLayout = tableLayout + "<td>";
            tableLayout = tableLayout + "<span class=\"boldtext\">Distance:</span>";
            tableLayout = tableLayout + "</td>";
            tableLayout = tableLayout + "<td>" + distancesArray[i] + " Miles</td>";
            tableLayout = tableLayout + "</tr>";
        }
        tableLayout = tableLayout + "<tr>";
        tableLayout = tableLayout + "<td>";
        tableLayout = tableLayout + "<span class=\"boldtext\">Address:</span>";
        tableLayout = tableLayout + "</td>";
        tableLayout = tableLayout + "<td>" + locationArray[i].AddressLine1 + " " + locationArray[i].AddressLine2 + " "
            + " " + locationArray[i].City + " " + locationArray[i].Country + "</td>";
        tableLayout = tableLayout + "</tr>";
        tableLayout = tableLayout + "<tr>";
        tableLayout = tableLayout + "<td>";
        tableLayout = tableLayout + "<span class=\"boldtext\">Phone:</span>";
        tableLayout = tableLayout + "</td>";
        tableLayout = tableLayout + "<td>" + locationArray[i].PhoneNo + "</td>";
        tableLayout = tableLayout + "</tr>";
        tableLayout = tableLayout + "<tr>";
        tableLayout = tableLayout + "<td>";
        tableLayout = tableLayout + "<span class=\"boldtext\">Postcode:</span>";
        tableLayout = tableLayout + "</td>";
        tableLayout = tableLayout + "<td>" + locationArray[i].ZipOrPostcode + "</td>";
        tableLayout = tableLayout + "</tr>";
        tableLayout = tableLayout + "</table>";

        tableLayout = tableLayout + "<table id=\"openingTimesHdrTbl\" class=\"table table-condensed\">";
        tableLayout = tableLayout + "<tr>";
        tableLayout = tableLayout + "<th>";
        tableLayout = tableLayout + "<span class=\"mappopuptitle\">Opening Times</span>";
        tableLayout = tableLayout + "</th>";
        tableLayout = tableLayout + "</tr>";
        tableLayout = tableLayout + "</table>";

        tableLayout = tableLayout + "<font color =\"red\" size=\"1\">*Red = Holiday Times</font>"

        tableLayout = tableLayout + "<table id=\"openingTimesTbl\" class=\"table table-condensed\">";

        var holidaySelected = false;
        for (var ot in openingTimesArray) {
            //Check if holidays are in the current week and are for the correct location and override normal opening time if they are.
            for (var hot in holidayOpeningTimesArray) {
                if (holidayOpeningTimesArray[hot].LocationID == openingTimesArray[ot].LocationID
                    && openingTimesArray[ot].LocationID == locationArray[i].LocationID
                    && holidayOpeningTimesArray[hot].DayOfWeek == openingTimesArray[ot].DayOfWeek
                    && holidayOpeningTimesArray[hot].HolidayStartDateStr != "") {

                    tableLayout = tableLayout + "<tr>";
                    tableLayout = tableLayout + "<td>";
                    tableLayout = tableLayout + "<span class=\"boldtext\">" + holidayOpeningTimesArray[hot].DayOfWeek + ":</span>";
                    tableLayout = tableLayout + "</td>";
                    tableLayout = tableLayout + "<td><font color =\"red\">" + holidayOpeningTimesArray[hot].HolidayOpenTimeStr + "</font></td>";
                    tableLayout = tableLayout + "<td><font color =\"red\">" + holidayOpeningTimesArray[hot].HolidayCloseTimeStr + "*</font></td>";
                    tableLayout = tableLayout + "</tr>";
                    holidaySelected = true;
                }
            }
            if (openingTimesArray[ot].LocationID == locationArray[i].LocationID && holidaySelected == false) {

                tableLayout = tableLayout + "<tr>";
                tableLayout = tableLayout + "<td>";
                tableLayout = tableLayout + "<span class=\"boldtext\">" + openingTimesArray[ot].DayOfWeek + ":</span>";
                tableLayout = tableLayout + "</td>";
                if (openingTimesArray[ot].Closed != true) {
                    tableLayout = tableLayout + "<td>" + openingTimesArray[ot].OpenTimeStr + "</td>";
                    tableLayout = tableLayout + "<td>" + openingTimesArray[ot].CloseTimeStr + "</td>";
                }
                else {
                    tableLayout = tableLayout + "<td>Closed</td>";
                    tableLayout = tableLayout + "<td>Closed</td>";
                }
                tableLayout = tableLayout + "</tr>";
            }

            holidaySelected = false;
        }

        tableLayout = tableLayout + "</table>";

        makeInfoWindowEvent(map, infowindow, tableLayout, marker);
        marker.push;
    }
}
google.maps.event.addDomListener(window, 'load', initialize);

function makeInfoWindowEvent(map, infowindow, contentString, marker) {
    google.maps.event.addListener(marker, 'click', function () {
        infowindow.setContent(contentString);
        infowindow.open(map, marker);
    });
}

function toggleTable() {
    var lTable = document.getElementById("openingTimesHdrTbl");
    lTable.style.cssText = "table table-condensed";
}

function searchDistance(searchedLat, searchedLong) {
    distancesArray = [];
    Number.prototype.toRad = function () {
        return this * Math.PI / 180;
    }

    for (var i in locationArray) {
        distanceBetween2Points(searchedLat, locationArray[i].Latitude, searchedLong, locationArray[i].Longitude);
    }
}

function distanceBetween2Points(lat1, lat2, lon1, lon2) {

    var R = 3961; //radius of the earth in miles
    var x1 = lat2 - lat1;
    var dLat = x1.toRad();
    var x2 = lon2 - lon1;
    var dLon = x2.toRad();

    var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
                    Math.cos(lat1.toRad()) * Math.cos(lat2.toRad()) *
                    Math.sin(dLon / 2) * Math.sin(dLon / 2);
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = R * c;

    d = Math.round(d * 10) / 10
    distancesArray.push(d);
}