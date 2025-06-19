function deleteFromCart(CartItemId) {
    $.ajax({
        url: '/Cart/DeleteCartItem',
        type: 'POST',
        data: { id: CartItemId },
        success: function (response) {
            if (response.success) {
                location.reload();
            } else {
                alert("Error:" + response.error);
            }
        },
        error: function (error) {
            alert("Error" + error);
        }
    });
}
