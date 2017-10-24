$(function () {
    $("#notifBtn").on("click", function (e) {
        e.preventDefault();
        $("#notif").fadeToggle("fast");
    })
});