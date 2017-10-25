$(function () {
    $("#notifBtn").on("click", function (e) {
        e.preventDefault();
        $("#notif").fadeToggle("fast");
    });

    $(".notifLink").on("click", function (e) {
        e.preventDefault();
        document.getElementById("notif").removeChild(e.target.parentElement);
        var notifId = $(e.target).attr("notifId");
        console.log(notifId);
        $.ajax({
            url: 'http://localhost:64548/api/Notifications/'+notifId,
            type: 'DELETE',
            success: function (result) {
                console.log("réussi ! ");
            }
        });
    });
});