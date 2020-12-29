$(document).ready(function () {
    $.ajax({
        url: 'Account/GetData',
        contentType: false,
        processData: false,
        type: 'Get',
        success: ImageSource
    });
    function ImageSource(passedData) {
        $("#img").attr("src", 'data:image/png;base64,' + passedData);
    };
});