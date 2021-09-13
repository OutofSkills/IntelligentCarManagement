
function scrollToDrivers() {
    var element = document.getElementById("available-drivers");
    if (!element) {
        console.warn('element was not found', elementId);
        return false;
    }
    element.scrollIntoView();
}