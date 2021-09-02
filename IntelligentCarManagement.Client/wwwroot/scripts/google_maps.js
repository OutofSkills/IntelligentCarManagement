
let map, infoWindow;
// Initialize the map in the selected div using it's id and set it's initial location
function initialize() {
    var latlng = new google.maps.LatLng(45.716948, -74.003563);
    var options = {
        zoom: 14, center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById
        ("map"), options);
    infoWindow = new google.maps.InfoWindow();
}

// Get the current location of the device and set it to a textbox
function getCurrentLocation() {
    // Try HTML5 geolocation.
    navigator.permissions.query({ name: 'geolocation' }).then(function (result) {
        if (result.state == 'granted') {
            report(result.state);
            // If the permission is granted then retrieve the location
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(
                    (position) => {
                        const pos = {
                            lat: position.coords.latitude,
                            lng: position.coords.longitude,
                        };
                        report('Lat: ' + pos.lat);
                        report('Long: ' + pos.lng);

                        addMarker(pos);
                        map.setCenter(pos);
                    }
                );
            } else {
                // Browser doesn't support Geolocation
                handleLocationError(false, infoWindow, map.getCenter());
            }
        } else if (result.state == 'prompt') {
            report(result.state);
        } else if (result.state == 'denied') {
            report(result.state);
        }
        result.onchange = function () {
            report(result.state);
        }
    });
}

// Show a window with an error message.
function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(
        browserHasGeolocation
            ? "Error: The Geolocation service failed."
            : "Error: Allow browser location."
    );
    infoWindow.open(map);
}

function report(message) {
    console.log(message);
}

// Get the current location of the device and set it to a textbox
function getPickUpLocation() {
    const center = { lat: 47.3737746, lng: 28.3859587 };
    // Create a bounding box with sides ~10km away from the center point
    const defaultBounds = {
        north: center.lat + 0.1,
        south: center.lat - 0.1,
        east: center.lng + 0.1,
        west: center.lng - 0.1,
    };
    const input = document.getElementById("pickUpLocation");
    const options = {
        bounds: defaultBounds,
        componentRestrictions: { country: "md" },
        fields: ["address_components", "geometry", "icon", "name"],
        strictBounds: false,
        types: ["establishment"],
    };
    const autocomplete = new google.maps.places.Autocomplete(input, options);

    autocomplete.bindTo("bounds", map);
}

// Get the current location of the device and set it to a textbox
function getDestinationLocation() {
    const center = { lat: 47.3737746, lng: 28.3859587 };
    // Create a bounding box with sides ~10km away from the center point
    const defaultBounds = {
        north: center.lat + 0.1,
        south: center.lat - 0.1,
        east: center.lng + 0.1,
        west: center.lng - 0.1,
    };
    const input = document.getElementById("destination");
    const options = {
        bounds: defaultBounds,
        componentRestrictions: { country: "md" },
        fields: ["address_components", "geometry", "icon", "name"],
        strictBounds: false,
        types: ["establishment"],
    };
    const autocomplete = new google.maps.places.Autocomplete(input, options);

    autocomplete.bindTo("bounds", map);
}

// Adds a marker to the map and push to the array.
function addMarker(location) {
    const marker = new google.maps.Marker({
        position: location,
        map: map,
    });
}
