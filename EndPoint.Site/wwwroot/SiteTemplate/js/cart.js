function AddToCart(productId) {
    console.log(productId);
    let count = 1;
    let model = { productId, count }
    ajaxFunc("/Cart/AddToCart", model, "POST",
        function (result) {
            console.log(result);
            if (result.isSuccess) {
                Toastify({
                    text: result.message,
                    className: "successs",
                    style: {
                        background: "linear-gradient(to right, #00b09b, #96c93d)",
                    }
                }).showToast();
               

            } else {
                $('.tp-login-btn').text("Login");
                $("#error-login").text(result.message);
                console.log(result.message);
            }
        },
        function (error) {
            $('.tp-login-btn').text("Login");
            $("#error-login").text(result.message)
            console.log(error);
        }
    );
}