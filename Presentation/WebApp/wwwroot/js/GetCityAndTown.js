$(document).ready(function () {
    // Şehirleri getir
    $.ajax({
        url: '/Order/GetCity',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var cityDropdown = $('#city');
            cityDropdown.empty();
            cityDropdown.append('<option selected value="0">Select City</option>');
            $.each(data.data, function (index, city) {
                cityDropdown.append($('<option></option>').attr('value', city.cityId).text(city.cityName));
            });
        },
        error: function (xhr, status, error) {
            console.log('Şehirler yüklenirken bir hata oluştu: ' + error);
        }
    });

    // Şehir seçilince ilçeleri getir
    $('#city').on('change', function () {
        var selectedCityId = $(this).val();

        if (selectedCityId === "0") {
            $('#town').empty().append('<option value="0">Select Town</option>');
            return;
        }

        $.ajax({
            url: '/Order/GetTownByCityId',
            type: 'GET',
            data: { cityId: selectedCityId },
            dataType: 'json',
            success: function (data) {
                var townDropdown = $('#town');
                townDropdown.empty();
                townDropdown.append('<option selected value="0">Select Town</option>');
                $.each(data.data, function (index, town) {
                    townDropdown.append($('<option></option>').attr('value', town.townId).text(town.townName));
                });
            },
            error: function (xhr, status, error) {
                console.log('İlçeler yüklenirken bir hata oluştu: ' + error);
            }
        });
    });
});
