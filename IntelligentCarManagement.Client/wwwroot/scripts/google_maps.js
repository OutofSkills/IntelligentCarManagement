
let map, infoWindow, geocoder;
let pickUpMarker, destinationMarker;
const center = { lat: 47.3737746, lng: 28.3859587 };

// Initialize the map in the selected div using it's id and set it's initial location
function initialize() {
    var options = {
        zoom: 8, center: center,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById
        ("map"), options);

    infoWindow = new google.maps.InfoWindow();
    geocoder = new google.maps.Geocoder();

    pickUpMarker = new google.maps.Marker({
        map: map,
        draggable: false,
        icon: '/images/icons/red_MarkerP.png'
    });
    destinationMarker = new google.maps.Marker({
        map: map,
        draggable: false,
        icon: '/images/icons/blue_MarkerD.png'
    });

    google.maps.event.addDomListener(window, 'resize', function () {
         map.setCenter(center);
    });
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
                        // Set the marker and fill the input
                        renderAddress(pos, pickUpMarker);
                        setPickUpAddress(pos.lat, pos.lng);
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

// Create a bounding box with sides ~10km away from the center point
const defaultAutocompleteBounds = {
    north: center.lat + 0.1,
    south: center.lat - 0.1,
    east: center.lng + 0.1,
    west: center.lng - 0.1,
};

function autocompletePickUp() {
    const input = document.getElementById("pickUpLocation");

    const options = {
        bounds: defaultAutocompleteBounds,
        componentRestrictions: { country: "md" },
        fields: ["address_components", "geometry", "icon", "name"],
        strictBounds: false,
        types: ["establishment"],
    };
    const autocomplete = new google.maps.places.Autocomplete(input, options);
    autocomplete.bindTo("bounds", map);

    autocomplete.addListener('place_changed', function () {
        const place = autocomplete.getPlace();
        if (!place.geometry) {
            // User entered the name of a Place that was not suggested and
            // pressed the Enter key, or the Place Details request failed.
            window.alert('No details available for input: \'' + place.name + '\'');
            return;
        }
        renderAddress(place.geometry.location, pickUpMarker);
        setPickUpAddress(place.geometry.location.lat(), place.geometry.location.lng());
    });
}


// Autocomplete selected input
function autocompleteDestination() {
    const input = document.getElementById("destination");

    const options = {
        bounds: defaultAutocompleteBounds,
        componentRestrictions: { country: "md" },
        fields: ["address_components", "geometry", "icon", "name"],
        strictBounds: false,
        types: ["establishment"],
    };
    const autocomplete = new google.maps.places.Autocomplete(input, options);
    autocomplete.bindTo("bounds", map);

    autocomplete.addListener('place_changed', function () {
        const place = autocomplete.getPlace();
        if (!place.geometry) {
            // User entered the name of a Place that was not suggested and
            // pressed the Enter key, or the Place Details request failed.
            window.alert('No details available for input: \'' + place.name + '\'');
            return;
        }
        renderAddress(place.geometry.location, destinationMarker);
        setDestinationAddress(place.geometry.location.lat(), place.geometry.location.lng());
    });
}

// Add a marker or change its position to the given place 
function renderAddress(location, marker) {
    map.setCenter(location, 10);
    marker.setPosition(location);
    marker.setVisible(true);
    fitMarkerBounds();
}

// Fit the markers on the map
function fitMarkerBounds() {
    if (pickUpMarker.getPosition() == undefined || destinationMarker.getPosition() == undefined)
        return;

    bounds = new google.maps.LatLngBounds();
    report(pickUpMarker.getPosition().lat());
    bounds.extend(pickUpMarker.getPosition());
    bounds.extend(destinationMarker.getPosition());
    map.fitBounds(bounds);
}

// Converts lat long coordinates to address
async function convertPositionAddress(lat, lng) {
    let address;
    const latlng = {
        lat: lat,
        lng: lng,
    };

    let response = await geocoder.geocode({ location: latlng });
    if (response.results[0]) {
        address =  response.results[0].formatted_address;
    } else {
        window.alert("No results found");
    }  

    return address;
}

var GLOBAL = {};
GLOBAL.DotNetReference = null;
GLOBAL.SetDotnetReference = function (pDotNetReference) {
    GLOBAL.DotNetReference = pDotNetReference;
};

// Asigns to the PickUp input the covnerted address
async function setPickUpAddress(lat, lng) {
    pickUpInput = document.getElementById("pickUpLocation");
    pickUpValue = await convertPositionAddress(lat, lng);
    pickUpInput.value = pickUpValue.toString();

    GLOBAL.DotNetReference.invokeMethodAsync("AssignPickUpCoordinates", lat + ';' + lng);

    var event = new Event('change');
    pickUpInput.dispatchEvent(event);
}
// Asigns to the Destination input the covnerted address
async function setDestinationAddress(lat, lng) {
    destinationInput = document.getElementById("destination");
    destinationValue = await convertPositionAddress(lat, lng);
    destinationInput.value = destinationValue.toString();

    GLOBAL.DotNetReference.invokeMethodAsync("AssignDestinationCoordinates", lat + ';' + lng);

    var event = new Event('change');
    destinationInput.dispatchEvent(event);
}

// Add Pick-Up and Destination marker on the Map
function initMarkers(pickLatLng, destLatLng) {
    pickUpMarker.setPosition({ lat: parseFloat(pickLatLng[0]), lng: parseFloat(pickLatLng[1]) });
    pickUpMarker.setVisible(true);

    destinationMarker.setPosition({ lat: parseFloat(destLatLng[0]), lng: parseFloat(destLatLng[1]) });
    destinationMarker.setVisible(true);

    fitMarkerBounds();
}
