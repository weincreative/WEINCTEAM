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

function TblEkle(tblList, tblNo, dkod, hkod, had, hfiyat, user, bid, ceneid, ceneAD, yetiskin) {

    var row = $('#newHizmet').closest('tr').clone();
    row.find("#HKod").text(hkod);
    row.find("#HAd").text(had);
    row.find("#DKod").text(dkod);
    row.find("#Fiyat").text(hfiyat);
    row.find("#CDurum").text(ceneAD);
    $('#newHizmet').closest('tr').after(row);
    $('input[type="button"]', row).removeClass('AddNew').addClass('btn btn-labeled btn-danger RemoveRow').val('Sil')
        .attr('name', tblNo);
    //$('select', row).attr('disabled', false);

    //row.on('click', '.FirmaDrop', function () {
    //    //Some code

    //    $(this).change(function () {
    //        var asd = this.value;
    //        alert(asd);
    //    });
    //});

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
        //alert(ceneid);

        tblList.push({
            "t_basvuruid": bid,
            "t_hizmetkodu": hkod,
            "t_diskodu": dkod,
            "t_cene": ceneid,
            "t_parca": 0,
            "t_yetiskinmi": yetiskin,
            "t_odemevarmi": false,
            "t_firmaid": 1,
            "t_createuser": user,
            "t_aktif": 1,
            "t_islemtarihi": zmn,
            "t_createdate": zmn,
            't_borcdurum': true
        });

    }
    var result = [tblList, tblNo];
    return result;
}

function RandTblEkle(RandtblList, RandtblNo, rt_id, rt_basvuru, rt_tc, rt_title, rt_aciklama, rt_baslangicsaat, rt_bitissaat, rt_classname, rt_icon, rt_allday, rt_createuser, rt_createdate, rt_basvurudr, rt_aktif) {
    RandtblNo++;
    var tempTc = rt_title.split(" ");
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

    if (rt_allday != false) {
        {
            RandtblList.push({
                "t_id": rt_id,
                "t_basvuru": "",//rt_basvuru
                "t_tc": tempTc[0],
                "t_title": rt_title,
                "t_aciklama": rt_aciklama,
                "t_baslangicsaat": rt_baslangicsaat,
                "t_bitissaat": rt_baslangicsaat,
                "t_classname": rt_classname,
                "t_icon": rt_icon,
                "t_allday": rt_allday,
                "t_createuser": rt_createuser,
                "t_createdate": zmn,//rt_createdate
                "t_basvurudr": rt_basvurudr,
                "t_aktif": rt_aktif
            });
        }
    } else {
        {
            RandtblList.push({
                "t_id": rt_id,
                "t_basvuru": "",//rt_basvuru
                "t_tc": tempTc[0],
                "t_title": rt_title,
                "t_aciklama": rt_aciklama,
                "t_baslangicsaat": rt_baslangicsaat,
                "t_bitissaat": rt_bitissaat,
                "t_classname": rt_classname,
                "t_icon": rt_icon,
                "t_allday": rt_allday,
                "t_createuser": rt_createuser,
                "t_createdate": zmn,//rt_createdate
                "t_basvurudr": rt_basvurudr,
                "t_aktif": rt_aktif
            });
        }
    }
    var RandResult = [RandtblList, RandtblNo];
    return RandResult;
}

function GetDisHareket(bid) {
    var result;
    $.ajax({
        type: 'GET',
        url: '../../View_HizmetDetay/_DisHHareket',
        data: { id: bid },
        async: false,
        success: function (veri) {
            result = veri;
        }
    });
    return result;
}

function GetNewHizmet() {
    var result;
    $.ajax({
        type: 'GET',
        url: '../_NewHareket',
        async: false,
        success: function (veri) {
            result = veri;
        }
    });
    return result;
}

function RandevuSil(Rrid) {
    $.ajax({
        type: "POST",
        url: "RandevuPasif",
        contentType: 'application/json',
        data: JSON.stringify({ rid: Rrid }),
        dataType: "json",
        success: function (veri) {
            if (veri == 1) {
                $.smallBox({
                    title: "BAŞARILI",
                    content: "<i class='fa fa-clock-o'></i> <i>Randevu Silindi ...</i>",
                    color: "#659265",
                    iconSmall: "fa fa-check fa-2x fadeInRight animated",
                    timeout: 4000
                });
                location.reload();
            } else {
                $.smallBox({
                    title: "HATA",
                    content: "<i class='fa fa-clock-o'></i> <i>Randevu Silinirken hata oluştu...</i>",
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
                content: "<i class='fa fa-clock-o'></i> <i>Randevu İşleminde hata oluştu...</i>",
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

function RandevuTEMIZOlustur(_RandtblList) {
    $.ajax({
        type: 'POST',
        data: JSON.stringify({ hst_randevu: _RandtblList }),
        url: 'Create',
        contentType: "application/json",
        success: function (veri) {
            if (veri) {
                $.smallBox({
                    title: "BAŞARILI",
                    content: "<i class='fa fa-clock-o'></i> <i>Randevu Eklendi ...</i>",
                    color: "#659265",
                    iconSmall: "fa fa-check fa-2x fadeInRight animated",
                    timeout: 4000
                });
                RandtblList = [];
                RandtblNo = 0;
                location.reload();
            }
        },
        error: function () {
            // bu kısımda eğer ajax işlemi başarısız ise
            // hata mesajı verebiliriz.
            $.smallBox({
                title: "HATA",
                content: "<i class='fa fa-clock-o'></i> <i>Randevu Ekleme İşleminde hata oluştu...</i>",
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

function RandevuOlustur(_RandtblList) {
    $.ajax({
        type: 'POST',
        data: JSON.stringify({ hst_randevu: _RandtblList }),
        url: 'Create',
        contentType: "application/json",
        success: function (veri) {
            if (veri) {
                $.smallBox({
                    title: "BAŞARILI",
                    content: "<i class='fa fa-clock-o'></i> <i>Randevu Eklendi ...</i>",
                    color: "#659265",
                    iconSmall: "fa fa-check fa-2x fadeInRight animated",
                    timeout: 4000
                });
                RandtblList = [];
                RandtblNo = 0;
                //location.reload();
            }
        },
        error: function () {
            // bu kısımda eğer ajax işlemi başarısız ise
            // hata mesajı verebiliriz.
            $.smallBox({
                title: "HATA",
                content: "<i class='fa fa-clock-o'></i> <i>Randevu Ekleme İşleminde hata oluştu...</i>",
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

function RandevuDuzelt(_RandtblList) {
    $.ajax({
        type: 'POST',
        data: JSON.stringify({ hst_randevu: _RandtblList }),
        url: 'Edit',
        contentType: "application/json",
        success: function (veri) {
            if (veri) {
                $.smallBox({
                    title: "BAŞARILI",
                    content: "<i class='fa fa-clock-o'></i> <i>Randevu Eklendi ...</i>",
                    color: "#659265",
                    iconSmall: "fa fa-check fa-2x fadeInRight animated",
                    timeout: 4000
                });
                RandtblList = [];
                RandtblNo = 0;
                //location.reload();
            }
        },
        error: function () {
            // bu kısımda eğer ajax işlemi başarısız ise
            // hata mesajı verebiliriz.
            $.smallBox({
                title: "HATA",
                content: "<i class='fa fa-clock-o'></i> <i>Randevu Ekleme İşleminde hata oluştu...</i>",
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

