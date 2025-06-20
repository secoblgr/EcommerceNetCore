function submitOrder() {
    // Razor'dan gelen değerleri HTML'den al
    var orderData = document.getElementById('orderData');
    var totalAmount = parseFloat(orderData.dataset.totalamount.replace(',', '.'));
    var shipping = parseFloat(orderData.dataset.shipping.replace(',', '.'));
    var cartId = parseInt(orderData.dataset.cartid);
    var total = totalAmount + shipping;

    var data = {
        CustomerName: $("#customerName").val().toLocaleUpperCase(),
        CustomerSurname: $("#customerSurname").val().toLocaleUpperCase(),
        CustomerEmail: $("#customerEmail").val(),
        CustomerPhone: $("#customerPhone").val(),
        CargoCityId: parseInt($("#city").val()),
        CargoTownId: parseInt($("#town").val()),
        ShippingAddress: $("#shippingAddress").val().toLocaleUpperCase(),
        TotalAmount: total,
    };
    $.ajax({
        type: 'POST',
        url: '/Order/CreateOrder',
        data: { dto: data, cartId: cartId },
        dataType: 'json',
        success: function (response) {
            toastr.success('Siparişiniz başarılı bir şekilde oluşturuldu!', 'Başarılı');
            setTimeout(function () {
                window.location.href = '/Home/Index';
            }, 2000); // 2 saniye sonra yönlendir
        },
        error: function (response) {
            toastr.error('Sipariş sırasında bir hata oluştu.', 'Hata');
            console.error(response.responseText);
        }
    });
}
