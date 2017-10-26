var map;
function initAutocomplete() {

    geolocate()
    var mapOptions = {
        center: { lat: 48.864716, lng: 2.349014 },
        zoom: 13
    }

    map = new google.maps.Map(document.getElementById("map"), mapOptions);

    $(".client").each(function (i, e) {
        var clientPos = JSON.parse($(e).attr("pos"));
        console.log(clientPos);
        addMarker(clientPos, "", map);
    });
}

    function addMarker(location, title, map) {
        // Add the marker at the clicked location, and add the next-available label
        // from the array of alphabetical characters.
        var marker = new google.maps.Marker({
            position: location,
            label: title,
            icon: "http://localhost:64544/Content/Images/client.png",
            map: map
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

