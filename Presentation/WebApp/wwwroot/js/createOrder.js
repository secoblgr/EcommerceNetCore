function submitOrder() {
    // Razor'dan gelen değerleri HTML'den al
    var orderData = document.getElementById('orderData');
    var totalAmount = parseFloat(orderData.dataset.totalamount.replace(',', '.'));
    var shipping = parseFloat(orderData.dataset.shipping.replace(',', '.'));
    var cartId = parseInt(orderData.dataset.cartid);
    var total = totalAmount + shipping;

    // Form alanlarını al
    var name = $("#customerName").val().trim();
    var surname = $("#customerSurname").val().trim();
    var email = $("#customerEmail").val().trim();
    var phone = $("#customerPhone").val().trim();
    var cityId = $("#city").val();
    var townId = $("#town").val();
    var address = $("#shippingAddress").val().trim();

    // Alanlardan biri boşsa toastr ile uyarı göster
    if (!name || !surname || !email || !phone || cityId == "0" || townId == "0" || !address) {
        toastr.options = {
            "timeOut": "1000", // 1 saniye
        };
        toastr.error("Lütfen tüm alanları doldurunuz!", "Eksik Bilgi");
        return;
    }
    // Hepsi doluysa gönder
    var data = {
        CustomerName: name.toLocaleUpperCase(),
        CustomerSurname: surname.toLocaleUpperCase(),
        CustomerEmail: email,
        CustomerPhone: phone,
        CargoCityId: parseInt(cityId),
        CargoTownId: parseInt(townId),
        ShippingAddress: address.toLocaleUpperCase(),
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
            }, 1500); 
        },
        error: function (response) {
            toastr.options = {
                "timeOut": "1000",
            };
            toastr.error('Sipariş sırasında bir hata oluştu.', 'Hata');
        }
    });
}
