// ajax function for retrive special step and other request
function ajaxFunc(url, data, type, callback, error) {
    ajaxFunc(url, data, callback, error, true);
}
// ajax function for retrive special step and other request
function ajaxFunc(url, data, type, callback, error, async) {
    $.ajax({
        type: type,
        //beforeSend: function (xhr) {
        //    xhr.setRequestHeader("RequestVerificationToken",
        //        $('input:hidden[name="__RequestVerificationToken"]').val());
        //},
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(data),
        success: callback,
        error: error,
        async: async
    });
}

var successToastify = Toastify({
    node: $("#success-notification-content").clone().removeClass("hidden")[0],
    duration: 3000,
    newWindow: true,
    close: true,
    gravity: "top",
    position: "right",
    stopOnFocus: true
});

var dangerToastify = Toastify({
    node: $("#failed-notification-content").clone().removeClass("hidden")[0],
    duration: 3000,
    newWindow: true,
    close: true,
    gravity: "top",
    position: "right",
    stopOnFocus: true
});
