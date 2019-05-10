function ListIlce(CId) {
    var cityid = CId;
    $.ajax({
        type: "POST",
        url: 'GetIlIlce',
        contentType: 'application/json',
        data: JSON.stringify({ id: cityid }),
        dataType: "json",
        success: function (veri) {
            $("#ilce option").remove();
            $("#ilce").prop("disabled", false);

            $.each(veri.sonuc, function (index, item) {
              
                var optionhtml = '<option value="' + item.Value + '"> ' + item.Text + '</option>';
                $("#ilce").append(optionhtml);
              
                //$("#TownId").append(optionhtml);
            });
        },
        error: function () {
            // bu kısımda eğer ajax işlemi başarısız ise
            // hata mesajı verebiliriz.
            alert("İlçeler getirilirken  bir hata meydana geldi.!");
        },
        beforeSend: function () {

            // bu kısımda form postalanmadan önce yapılacak
            // işler belirlenebilir. mesela postalama başladığı
            // anda loading resmi görüntüleyebiliriz.
        },
        complete: function () {

            // bu kısımda form postalandıktan sonra yapılacak
            // işler belirlenebilir. mesela postalama bittiği
            // anda loading resmi gizleyebiliriz.
        }
    });

}