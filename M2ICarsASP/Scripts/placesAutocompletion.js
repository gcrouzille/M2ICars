// This example displays an address form, using the autocomplete feature
// of the Google Places API to help users fill in the information.

// This example requires the Places library. Include the libraries=places
// parameter when you first load the API. For example:
// <script src="https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY&libraries=places">

var directionsDisplay;
var directionsService;
var map, arrival, departure;

function initAutocomplete() {
    directionsDisplay = new google.maps.DirectionsRenderer();
    directionService = new google.maps.DirectionsService();

    var mapOptions = {
        center: { lat: 48.864716, lng: 2.349014 },
        zoom: 13
    }
    map = new google.maps.Map(document.getElementById('map'), mapOptions);

    directionsDisplay.setMap(map);

    // Create the autocomplete object, restricting the search to geographical
    // location types.
    departure = new google.maps.places.Autocomplete(
        /** @type {!HTMLInputElement} */(document.getElementById('departure')),
        { types: ['geocode'] });

    arrival = new google.maps.places.Autocomplete(
        /** @type {!HTMLInputElement} */(document.getElementById('arrival')),
        { types: ['geocode'] });

    geolocate();    

    // On resitue la map quand on tape une adresse
    departure.addListener('place_changed', function () {
        var place = departure.getPlace();
        if (arrival.getPlace() == undefined)
            relocate(place);
        else
            calcRoute();
    });
    arrival.addListener('place_changed', function () {
        var place = arrival.getPlace();
        if (departure.getPlace() == undefined)
            relocate(place);
        else
            calcRoute();
    });
}

function relocate(place) {
    //If the place has a geometry, then present it on a map.
    if (place.geometry.viewport) {
        map.fitBounds(place.geometry.viewport);
    } else {
        map.setCenter(place.geometry.location);
        map.setZoom(17);  // Why 17? Because it looks good.
    }
}

function calcRoute() {
    var start = document.getElementById('departure').value;
    var end = document.getElementById('arrival').value;
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

function geolocate() {
   //Try HTML5 geolocation.
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };

            map.setCenter(pos);
        }, function () {
            handleLocationError(true, infoWindow, map.getCenter());
        });
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(browserHasGeolocation ?
        'Error: The Geolocation service failed.' :
        'Error: Your browser doesn\'t support geolocation.');
}

