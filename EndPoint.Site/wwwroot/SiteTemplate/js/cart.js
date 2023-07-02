function AddToCart(productId) {
    let count = 1;
    let model = { productId, count }
    ajaxFunc("/Cart/AddToCart", model, "POST",
        function (result) {
            if (result.isSuccess) {
                Toastify({
                    text: result.message,
                    className: "successs",
                    style: {
                        background: "linear-gradient(to right, #00b09b, #96c93d)",
                    }
                }).showToast();
                GetListCart();

            } else {
                $('.tp-login-btn').text("Login");
                $("#error-login").text(result.message);
                console.log(result.message);
            }
        },
        function (error) {
            $('.tp-login-btn').text("Login");
            console.log(error);
        }
    );
}


function RemoveCart(Id) {
    var model = {
        'Id': Id,
    };

    ajaxFunc("/Cart/Remove", model, "POST",
        function (result) {
            if (result.isSuccess) {
                Toastify({
                    text: result.message,
                    className: "successs",
                    style: {
                        background: "linear-gradient(to right, #00b09b, #96c93d)",
                    }
                }).showToast();
                GetListCart();

            } else {
                $('.tp-login-btn').text("Login");
                $("#error-login").text(result.message);
                console.log(result.message);
            }
        },
        function (error) {
            $('.tp-login-btn').text("Login");
            console.log(error);
        }
    );
}

function GetListCart() {
    var base_url = window.location.origin;
    $.ajax(base_url+'/Cart/CartViewComponent',
        {
            dataType: 'html', // type of response data
            timeout: 500,     // timeout milliseconds

            success: function (html, status, xhr) {   // success callback function     
                $("#viewcomponent-cart").html(html);
                BacketCount();
                GetListCartTable();
            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback

            }
        });
}

function BacketCount() {
    var base_url = window.location.origin;
    $.ajax(base_url + '/Cart/BacketViewComponent',
        {
            dataType: 'html', // type of response data
            timeout: 500,     // timeout milliseconds

            success: function (html, status, xhr) {   // success callback function     
                $("#cart-count").html(html);
            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback

            }
        });
}

function GetListCartTable() {
    var base_url = window.location.origin;
    $.ajax(base_url + '/Cart/CartTableViewComponent',
        {
            dataType: 'html', // type of response data
            timeout: 500,     // timeout milliseconds

            success: function (html, status, xhr) {   // success callback function     
                $("#CartTableContainer").html(html);

            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback

            }
        });
}


