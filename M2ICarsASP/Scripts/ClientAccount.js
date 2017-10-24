
$(function () {
    $("#Birthday").datepicker({
        changeMonth: true,
        changeYear: true,
        regional: "fr",
        dateFormat: "dd/mm/yy"
    });

    $("#imageURL").change(function (evt) {
        var tgt = evt.target || window.event.srcElement,
            files = tgt.files;

        // FileReader support
        if (FileReader && files && files.length) {
            var fr = new FileReader();
            fr.onload = function () {
                document.getElementById("profileImg").src = fr.result;
            }
            fr.readAsDataURL(files[0]);
        }
    });
});