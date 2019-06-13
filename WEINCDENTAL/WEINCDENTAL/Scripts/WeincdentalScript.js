﻿function ListIlce(CId) {
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


function HastaTaburcuGeriAl(bid) {
    $.ajax({
        type: "POST",
        url: "../TaburcuGerial",
        contentType: 'application/json',
        data: JSON.stringify({ id: bid }),
        dataType: "json",
        success: function (veri) {
            if (veri == 1) {
                $.smallBox({
                    title: "BAŞARILI",
                    content: "<i class='fa fa-clock-o'></i> <i>Hasta taburcusu geri alındı...</i>",
                    color: "#659265",
                    iconSmall: "fa fa-check fa-2x fadeInRight animated",
                    timeout: 4000
                });
                location.reload();
            } else {
                $.smallBox({
                    title: "HATA",
                    content: "<i class='fa fa-clock-o'></i> <i>Taburcu geri alma işleminde hata oluştu...</i>",
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
                content: "<i class='fa fa-clock-o'></i> <i>Taburcu geri alma işleminde hata oluştu...</i>",
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

function TblEkle(tblList,tblNo,dkod, hkod, had, hfiyat,user,bid) {
   
        var row = $('#newHizmet').closest('tr').clone();
        row.find("#HKod").text(hkod);
        row.find("#HAd").text(had);
        row.find("#DKod").text(dkod);
        row.find("#Fiyat").text(hfiyat);
        row.find("#CDurum").text('Tek Diş');
        $('#newHizmet').closest('tr').after(row);
        // var btnName = tblNo.toString();
    $('input[type="button"]', row).removeClass('AddNew').addClass('btn btn-labeled btn-danger RemoveRow').val('Sil')
        .attr('name', tblNo);
        
    tblNo++;

        {
            var date = new Date();
            var day = date.getDate();
            var month = date.getMonth();
            month = month + 1;
            if ((String(day)).length == 1)
                day = '0' + day;
            if ((String(month)).length == 1)
                month = '0' + month;

            var saat = date.getHours();
            var dak = date.getMinutes();
            var zmn =
                date.getFullYear() +

                    "-" +
                    month +
                    "-" + day +
                    " " + saat +
                    ":" + dak;
            // alert(zmn);
            
           
            tblList.push({
                "t_basvuruid": bid,
                "t_hizmetkodu": hkod,
                "t_diskodu": dkod,
                "t_cene": 9,
                "t_parca": 0,
                "t_yetiskinmi": true,
                "t_odemevarmi": false,
                "t_firmaid": 1,
                "t_createuser": user,
                "t_aktif": 1,
                "t_islemtarihi": zmn,
                "t_createdate": zmn
            });

    }
    var result = [tblList, tblNo];
    return result;
}
