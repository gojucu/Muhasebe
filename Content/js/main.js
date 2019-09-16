/*
 * jQuery File Upload Plugin JS Example
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2010, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * https://opensource.org/licenses/MIT
 */

/* global $, window */

$(function () {
    'use strict';

    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload({
        // Uncomment the following to send cross-domain cookies:
        //xhrFields: {withCredentials: true},
        url: 'server/asp_net/UploadHandler_4_0.aspx'
    });

    // Enable iframe cross-domain access via redirect option:
    $('#fileupload').fileupload(
        'option',
        'redirect',
        window.location.href.replace(
            /\/[^\/]*$/,
            '/cors/result.html?%s'
        )
    );

    if (window.location.hostname === 'blueimp.github.io') {
        // Demo settings:
        $('#fileupload').fileupload('option', {
            url: '//jquery-file-upload.appspot.com/',
            // Enable image resizing, except for Android and Opera,
            // which actually support image resizing, but fail to
            // send Blob objects via XHR requests:
            disableImageResize: /Android(?!.*Chrome)|Opera/
                .test(window.navigator.userAgent),
            maxFileSize: 999000,
            acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i
        });
        // Upload server status check for browsers with CORS support:
        if ($.support.cors) {
            $.ajax({
                url: '//jquery-file-upload.appspot.com/',
                type: 'HEAD'
            }).fail(function () {
                $('<div class="alert alert-danger"/>')
                    .text('Upload server currently unavailable - ' +
                            new Date())
                    .appendTo('#fileupload');
            });
        }
    } else {
        // Load existing files:
        $('#fileupload').addClass('fileupload-processing');
        $.ajax({
            // Uncomment the following to send cross-domain cookies:
            //xhrFields: {withCredentials: true},
            url: $('#fileupload').fileupload('option', 'url'),
            dataType: 'json',
            context: $('#fileupload')[0]
        }).always(function () {
            $(this).removeClass('fileupload-processing');
        }).done(function (result) {
            $(this).fileupload('option', 'done')
                .call(this, $.Event('done'), {result: result});
        });
    }

});
UrunKaydet: function () {


    var frm = $("#urunEkleKaydetForm");
    var id = $("#id", frm).val();
    var ustid = $("#ustid", frm).val();
    var kodu = $("#kodu", frm).val();
    var ad = $("#ad", frm).val();
    var aktif = $("#aktif", frm).is(":checked") ? true : false;
    var anaSayfadaGoster = $("#anaSayfadaGoster", frm).is(":checked") ? true : false;
    var populer = $("#populer", frm).is(":checked") ? true : false;
    var yeniBitisTarihi = $("#yeniBitisTarihi", frm).val();
    var kisaAciklama = $("#kisaAciklama", frm).val();
    var fiyat = $("#fiyat", frm).val();
    var fiyatUsd = $("#fiyatUsd", frm).val();
    var fiyatEuro = $("#fiyatEuro", frm).val();
    var fiyatDiger = $("#fiyatDiger", frm).val();
    var indirimOrani = $("#indirimOrani", frm).val();
    var aciklama = $("#aciklama").val();
    var aramaKriterleri = $("#aciklama", frm).val();
    var filtreler = $("#filtreler", frm).val();
    var resim1 = $("#r1", frm).attr("src");
    var resim2 = $("#r2", frm).attr("src");
    var resim3 = $("#r3", frm).attr("src");
    var renkid = $("#renkid", frm).val();
    var data = renkTablo
        .rows()
        .data();


    alert(aciklama)
    alert(kisaAciklama)

    if (data.length == 0) {

        var deneme = "0";
        var rengi = $("#renkid").val();
        var rengii = rengi || deneme;

        if (rengii == "0") {
            var urunler = {
                id: id,
                ad: ad,
                resim1: resim1,
                resim2: resim2,
                resim3: resim3,
                kisaAciklama: kisaAciklama,
                fiyat: fiyat,
                aktif: aktif,
                silindi: false,
                ustid: ustid,
                bedenler: "",
                kodu: kodu,
                aciklama: aciklama,
                populer: populer,
                yeniBitisTarihi: yeniBitisTarihi,
                indirimOrani: indirimOrani,
                anaSayfadaGoster: anaSayfadaGoster,
                renkid: 0,
                filtreler: filtreler,
                aramaKriterleri: aramaKriterleri,
                fiyatUsd: fiyatUsd,
                fiyatEuro: fiyatEuro,
                fiyatDiger: fiyatDiger
            }
        } else {
            alert(rengii);
            var bedeni = $(".bedeni").val().replace(/%2C/, ",");
            var bedenii = bedeni || deneme;

            var urunler = {
                id: id,
                ad: ad,
                resim1: resim1,
                resim2: resim2,
                resim3: resim3,
                kisaAciklama: kisaAciklama,
                fiyat: fiyat,
                aktif: aktif,
                silindi: false,
                ustid: ustid,
                bedenler: bedenii,
                kodu: kodu,
                aciklama: aciklama,
                populer: populer,
                yeniBitisTarihi: yeniBitisTarihi,
                indirimOrani: indirimOrani,
                anaSayfadaGoster: anaSayfadaGoster,
                renkid: rengii,
                filtreler: filtreler,
                aramaKriterleri: aramaKriterleri,
                fiyatUsd: fiyatUsd,
                fiyatEuro: fiyatEuro,
                fiyatDiger: fiyatDiger
            }

        }
        
        $.ajax({
            url: "/panel/urunEkleDuzenle",
            async: false,
            data: { urunler },
            type: "post",
            success: function (sonucu) {
                if (sonucu) {
                    alert(sonucu)
                } else {
                    bootbox.alert({
                        message: "Hata.. Lütfen girdiğiniz bilgileri tekrar kontrol ediniz."

                    })
                }
            }

        })
    } else {

        $("#renkler tbody tr").each(function () {

            var rengi = $(this).find(".rengi").val();
            var bedeni = $(this).find(".bedeni").val().replace(/%2C/, ",");
            var resler = [];
            $(this).find(".resEklen").each(function () {
                resler.push($(this).attr("src"))
            });
            var urunler = {
                id: id,
                ad: ad,
                resim1: resler[0],
                resim2: resler[1],
                resim3: resler[2],
                kisaAciklama: kisaAciklama,
                fiyat: fiyat,
                aktif: aktif,
                silindi: false,
                ustid: ustid,
                bedenler: bedeni,
                kodu: kodu,
                aciklama: aciklama,
                populer: populer,
                yeniBitisTarihi: yeniBitisTarihi,
                indirimOrani: indirimOrani,
                anaSayfadaGoster: anaSayfadaGoster,
                renkid: rengi,
                filtreler: filtreler,
                aramaKriterleri: aramaKriterleri,
                fiyatUsd: fiyatUsd,
                fiyatEuro: fiyatEuro,
                fiyatDiger: fiyatDiger
            }
            $.ajax({
                url: "/panel/urunEkleDuzenle",
                async: false,
                data: { urunler },
                type: "post",
                success: function (sonucu) {
                    if (sonucu) {
                        alert(sonucu)
                    } else {
                        bootbox.alert({
                            message: "Hata.. Lütfen girdiğiniz bilgileri tekrar kontrol ediniz."

                        })
                    }
                }
            })
        })
    }
    return false;
}