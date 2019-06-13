function BasvuruRemove(bid) {

    $.ajax({
        type: "POST",
        url: "../Delete",
        contentType: 'application/json',
        data: JSON.stringify({ id: bid }),
        dataType: "json",
        success: function (veri) {
            if (veri == 1) {
                $.smallBox({
                    title: "BAŞARILI",
                    content: "<i class='fa fa-clock-o'></i> <i>Silme işlemi başarılı...</i>",
                    color: "#659265",
                    iconSmall: "fa fa-check fa-2x fadeInRight animated",
                    timeout: 4000
                });
                location.reload(); 
            } else {
                $.smallBox({
                    title: "HATA",
                    content: "<i class='fa fa-clock-o'></i> <i>Silme işleminde hata oluştu...</i>",
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
                content: "<i class='fa fa-clock-o'></i> <i>Silme işleminde hata oluştu...</i>",
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
function HizmetRemove(hid) {
    var rslt = false;
    $.ajax({
        type: "POST",
        url: "../../View_HizmetDetay/Delete",
        contentType: 'application/json',
        data: JSON.stringify({ id: hid }),
        dataType: "json",
        async: false,
        success: function (veri) {
            if (veri == 1) {
                $.smallBox({
                    title: "BAŞARILI",
                    content: "<i class='fa fa-clock-o'></i> <i>Silme işlemi başarılı...</i>",
                    color: "#659265",
                    iconSmall: "fa fa-check fa-2x fadeInRight animated",
                    timeout: 4000
                });
                rslt = true;
                //  location.reload();
            } else {
                $.smallBox({
                    title: "HATA",
                    content: "<i class='fa fa-clock-o'></i> <i>Silme işleminde hata oluştu...</i>",
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
                content: "<i class='fa fa-clock-o'></i> <i>Silme işleminde hata oluştu...</i>",
                color: "#C46A69",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
        }
       
    });
    return rslt;
}