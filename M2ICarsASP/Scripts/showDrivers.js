var directionsDisplay;
var directionsService;

function initMap() {
    directionsDisplay = new google.maps.DirectionsRenderer();
    directionService = new google.maps.DirectionsService();

    var map = new google.maps.Map(document.getElementById('map'), {        
        zoom: 13
    });

    directionsDisplay.setMap(map);

    $(".driver").each(function (i, e) {        
        var driverPos = JSON.parse($(e).attr("pos"));
        console.log(driverPos);
        addMarker(driverPos, $(e).text(), map);
    });
    calcRoute();
}

function calcRoute() {
    var start = document.getElementById('departure').textContent;
    var end = document.getElementById('arrival').textContent;
    var request = {
        origin: start,
        destination: end,
        travelMode: 'DRIVING'
    };
    directionService.route(request, function (result, status) {
        if (status == 'OK') {
            directionsDisplay.setDirections(result);
        } else
            console.log(status);
    });
}

function addMarker(location, title, map) {
    // Add the marker at the clicked location, and add the next-available label
    // from the array of alphabetical characters.
    var marker = new google.maps.Marker({
        position: location,
        label: title,
        icon: "http://localhost:64544/Content/Images/carIcon.png",
        map: map
    });
}

