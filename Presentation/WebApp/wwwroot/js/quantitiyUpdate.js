function quantityUpdate(cartId, productId, cartItemId, quantity) {
    $.ajax({
        url: '/Cart/UpdateQuantity',
        type: 'POST',
        data: {
            cartId: cartId,
            productId: productId,
            quantity: quantity,
            cartItemId: cartItemId
        },
        success: function (response) {
            if (response.success) {
                location.reload();
            } else {
                alert('Bir hata oluştu: ' + response.error);
            }
        },
        error: function (xhr, status, error) {
            alert('Bir hata oluştu: ' + error);
        }
    });
}
