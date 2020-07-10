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
            "t_totalborc": 0,
            "t_aktif": 1,
            "t_islemtarihi": zmn,
            "t_createdate": zmn,
            't_borcdurum': true,
            't_yapildi': false
        });

    }
    var result = [tblList, tblNo];
    return result;
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

function GetVezneHareket(bid) {

    var result;
    $.ajax({
        type: 'GET',
        url: '../../View_HizmetDetay/_VezneHHareket',
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

function GetNewVezne(htc) {
    var result;
    $.ajax({
        type: 'GET',
        url: '_Vezne',
        data: { tc: htc },
        async: false,
        success: function (veri) {
            result = veri;
        }
    });
    return result;
}

function GetNewPacs(htc, had, hbasDate, hbitDate) {
    var result;
    $.ajax({
        type: 'GET',
        url: '_PartialPacs',
        data: { tc: htc, ad: had, basDate: hbasDate, bitDate: hbitDate },
        async: false,
        success: function (veri) {
            result = veri;
        }
    });
    return result;
}

////////////RANDEVU/////////////
function RandTblEkle(RandtblList, RandtblNo, rt_id, rt_basvuru, rt_tc, rt_title, rt_aciklama, rt_baslangicsaat, rt_bitissaat, rt_classname, rt_icon, rt_allday, rt_createuser, rt_createdate, rt_basvurudr, rt_aktif) {
    RandtblNo++;
    var tempTc = rt_title.split(" ");
    var date = new Date();
    var day = date.getDate();
    var month = date.getMonth();
    var tempClassName = null;
    month = month + 1;

    if (rt_classname.length <= 2) {
        tempClassName = rt_classname[1];
    } else {
        tempClassName = rt_classname;
    }

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
                "t_classname": tempClassName,
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
                "t_classname": tempClassName,
                "t_icon": rt_icon,
                "t_allday": rt_allday,
                "t_createuser": rt_createuser,
                "t_createdate": zmn,//rt_createdate
                "t_basvurudr": rt_basvurudr,
                "t_aktif": rt_aktif
            });
        }
    }
    tempClassName = null;
    var RandResult = [RandtblList, RandtblNo];
    return RandResult;
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
            }
        },
        error: function () {
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

//hst_randevu/HastaListele
function RandevuHastaListele() {


    var e = document.getElementById("Hastalar");
    var seciliHasta = e.options[e.selectedIndex].innerHTML;
    if (seciliHasta != null || seciliHasta.length > 0 || seciliHasta != "--Hasta Seçiniz--") {
        $('#title').val(seciliHasta);
    }

    $('#Hastalar').on("change", function () {
        var e = document.getElementById("Hastalar");
        var seciliHasta = e.options[e.selectedIndex].innerHTML;
        $('#title').val(seciliHasta);
    });


}
function AjaxCall(url, data, type) {
    return $.ajax({
        url: url,
        type: type ? type : 'GET',
        data: data,
        contentType: 'application/json'
    });
}
///////////////////////////////////

////////////////CAGRIEKRANI///////////////////
//cagriekrani/BolumleriGetir
function CagriEkraniBolumListele() {
    AjaxCall('/CagriEkrani/BolumleriGetir', null).done(function (response) {
        if (response.length > 0) {
            $('#Bolumler').html('');
            var options = '';
            options += '<option id="" value="">Bölüm Seçiniz</option>';
            response.forEach(function (entry) {
                options += '<option id="' + entry.Index + '" value="' + entry.bolumlerID + '">' + entry.bolumlerADI + '</option>';
            });
            $('#Bolumler').append(options);
        }
    }).fail(function (error) {
        $.smallBox({
            title: "HATA",
            content: "<i class='fa fa-clock-o'></i> <i>Çağrı Ekranı Bölüm Listelemede hata oluştu...</i>",
            color: "#818F9A",
            iconSmall: "fa fa-times fa-2x fadeInRight animated",
            timeout: 4000
        });
    });

    //$('#Bolumler').on("change", function () {
    //    var listelenecekHValue = $("input[name='hastaSec']:checked").val();
    //    var seciliBolum = $('#Bolumler').val();
    //    //setInterval(function () {
    //    //    // KODLARI BURAYA YAZ
    //    //}, 10000);
    //        $.ajax({
    //            url: '/CagriEkrani/PartialCagriEkrani',
    //            type: 'GET',
    //            data: { bsid: parseInt(seciliBolum), hcid: parseInt(listelenecekHValue) },
    //            async: false,
    //            success: function (partialView) {
    //                $("#dt_basic3_3").dataTable().fnDestroy();
    //                $('#cagriEkraniList').html(partialView);
    //                $('#cagriEkraniList').show();
    //            }
    //        });


    //});

    $('#cagriListesiYenile').click(function () {
        var listelenecekHValue = $("input[name='hastaSec']:checked").val();
        var seciliBolum = $('#Bolumler').val();
        $.ajax({
            url: '/CagriEkrani/PartialCagriEkrani',
            type: 'GET',
            data: { bsid: parseInt(seciliBolum), hcid: parseInt(listelenecekHValue) },
            async: true,
            success: function (partialView) {
                $("#dt_basic3_3").dataTable().fnDestroy();
                $('#cagriEkraniList').html(partialView);
                $('#cagriEkraniList').show();
            }
        });


    });




}
function AjaxCall(url, data, type) {
    return $.ajax({
        url: url,
        type: type ? type : 'GET',
        data: data,
        contentType: 'application/json'
    });
}


///////////////////////////////////////


function PlanlamaYapildi(PHid) {
    $.ajax({
        type: "POST",
        url: "../../View_HizHareket/PlanlaYap",
        contentType: 'application/json',
        data: JSON.stringify({ id: PHid }),
        dataType: "json",
        async: false,
        success: function (veri) {

            if (veri == 1) {
                $.smallBox({
                    title: "BAŞARILI",
                    content: "<i class='fa fa-clock-o'></i> <i>Planlama İşlemi Yapıldı Olarak Eklendi Başarılı...</i>",
                    color: "#659265",
                    iconSmall: "fa fa-check fa-2x fadeInRight animated",
                    timeout: 4000
                });
                //  location.reload();
            } else {
                $.smallBox({
                    title: "HATA",
                    content: "<i class='fa fa-clock-o'></i> <i>Planlama Onaylanmasında Hata Oluştu...</i>" + "Hata: " + veri.message,
                    color: "#C46A69",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });

            }

        }
    });
}


function PlanlamaYapilmadi(PHid) {
    $.ajax({
        type: "POST",
        url: "../../View_HizHareket/PlanlaYapma",
        contentType: 'application/json',
        data: JSON.stringify({ id: PHid }),
        dataType: "json",
        async: false,
        success: function (veri) {

            if (veri == 1) {
                $.smallBox({
                    title: "BAŞARILI",
                    content: "<i class='fa fa-clock-o'></i> <i>Planlama Onaylanması Kaldırıldı...</i>",
                    color: "#659265",
                    iconSmall: "fa fa-check fa-2x fadeInRight animated",
                    timeout: 4000
                });
                //  location.reload();
            } else {
                $.smallBox({
                    title: "HATA",
                    content: "<i class='fa fa-clock-o'></i> <i>Planlama Onaylanmasını Kaldıramadım Hata Oluştu...</i>" + "Hata: " + veri.message,
                    color: "#C46A69",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });

            }

        }

    });
}

//Yetkilendirme Wizard
function WizardValidations() {
    var initializeDuallistbox = $('#initializeDuallistbox').bootstrapDualListbox({
        nonSelectedListLabel: 'Yetki Listesi',
        selectedListLabel: 'Seçilen Yetkiler',
        preserveSelectionOnMove: 'moved',
        moveOnSelect: false,
        infoTextEmpty: 'Empty list'


        //nonSelectedFilter: 'ion ([7-9]|[1][0-2])'
    });
}
// Home/YetkiAyarlar Yetkilendirme Wizard
function Yetkiler(uid) {
    var usid = uid;
    $.ajax({
        type: "POST",
        url: '../Yetki/GetYetkis',
        contentType: 'application/json',
        data: JSON.stringify({ userId: usid }),
        dataType: "json",
        success: function (veri) {
            $("#initializeDuallistbox option").remove();
            $("#bootstrap-duallistbox-nonselected-list_duallistbox_demo2 option").remove();
            // $("#initializeDuallistbox").prop("disabled", false);

            $.each(veri.sonuc, function (index, item) {

                var optionhtml = '<option value="' + item.Value + '"> ' + item.Text + '</option>';
                $("#bootstrap-duallistbox-nonselected-list_duallistbox_demo2").append(optionhtml);
                $("#initializeDuallistbox").append(optionhtml);
                var demo1 = $('select[name="duallistbox_demo2"]').bootstrapDualListbox('refresh', true);

            });
        },
        error: function () {
            // bu kısımda eğer ajax işlemi başarısız ise
            // hata mesajı verebiliriz.
            alert("Yetkiler getirilirken  bir hata meydana geldi.!");
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

function YetkiCreate(userid, yetkilist) {
    var result;
    $.ajax({
        type: 'POST',
        url: '../UserYetki/YetkisCreate',
        data: { userId: userid, yetkiIdList: yetkilist },
        async: false,
        success: function (veri) {
            result = veri;
        }
    });
    return result;

}



//İstatistik View

function ValidateControl() {
    $('#order-form').validate({
        rules: {
            startdate: {
                required: true
            },
            finishdate: {
                required: true
            }
        },
        messages: {
            startdate: {
                required: '* Zorunlu alan.'
            },
            finishdate: {
                required: '* Zorunlu alan.'
            }
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element.parent());
        }

    });
}

function GetHizmet(startdate, finishdate, hid) {

    var result;
    $.ajax({
        type: "GET",
        url: "../../Istatistik/_Hizmet",
        data: { baslangic: startdate, bitis: finishdate },
        async: false,
        success: function (veri) {
            result = veri;

        },
        error: function (e) {
            console.log(e);
            $('#Loading').button('reset');
            // bu kısımda eğer ajax işlemi başarısız ise
            // hata mesajı verebiliriz.

        },
        beforeSend: function () {
             $('#Loading').button('loading');
            // business logic...
         
            // bu kısımda form postalanmadan önce yapılacak
            // işler belirlenebilir. mesela postalama başladığı
            // anda loading resmi görüntüleyebiliriz.
        },
        complete: function () {
            $('#Loading').button('reset');
            // bu kısımda form postalandıktan sonra yapılacak
            // işler belirlenebilir. mesela postalama bittiği
            // anda loading resmi gizleyebiliriz.
        }

    });
    return result;
}