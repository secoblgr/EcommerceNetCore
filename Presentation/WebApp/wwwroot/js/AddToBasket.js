function addToCart(id, totalPrice) {
    var model = {
        ProductId: id,
        Quantity: 1,
        TotalPrice: totalPrice,
    };
    console.log(id, totalPrice);
    console.log(model),
        $.ajax({
            url: '/Cart/AddToCartItem',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(model),
            success: function (result) {
                if (result.success) {
                    // alert("Product successfully added to basket!");
                    location.reload();
                } else {
                    alert("Hata: " + (result.error?.message || "Wrong Error"));
                }
            },
            error: function (error) {
                alert("Request error: " + error);
            }
        });
}
