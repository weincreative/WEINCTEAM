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

// Başvuru ekranından taburcu işlemi
function HastaTaburcuEt(bid) {
    $.ajax({
        type: "POST",
        url: "../Taburcu",
        contentType: 'application/json',
        data: JSON.stringify({ id: bid }),
        dataType: "json",
        success: function (veri) {
            if (veri == 1) {
                $.smallBox({
                    title: "BAŞARILI",
                    content: "<i class='fa fa-clock-o'></i> <i>Hasta taburcu edildi...</i>",
                    color: "#659265",
                    iconSmall: "fa fa-check fa-2x fadeInRight animated",
                    timeout: 4000
                });
                location.reload();
            } else {
                $.smallBox({
                    title: "HATA",
                    content: "<i class='fa fa-clock-o'></i> <i>Taburcu işleminde hata oluştu...</i>",
                    color: "#C46A69",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
            }


        },
        error: function () {
            // bu kısımda eğer ajax işlemi başarısız ise
            // hata mesajı verebiliriz.
            $.smallBox({
                title: "HATA",
                content: "<i class='fa fa-clock-o'></i> <i>Taburcu işleminde hata oluştu...</i>",
                color: "#C46A69",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
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
