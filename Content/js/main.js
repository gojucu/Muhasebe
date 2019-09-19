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

var islemler = {
    UrunKaydet: function () {

        ////alert("burda")
        ////$("#urunler > tbody > tr").each(function (index, item) {
        ////    var hizmetUrunId = $(this).find(".HizmetUrunID").val();
        ////    var miktar = $(this).find(".Miktar").val();
        ////    var birimFiyat = $(this).find(".BirimFiyat").val();
        ////    var vergi = $(this).find(".Vergi").val();
        ////    alert($(item).find(".HizmetUrunID").val() + " aşağı gitmedi")
        ////    alert(hizmetUrunId)
        ////})
        //return false

        var frm = $("#faturaEkleKaydetForm");
        var id = $("#Id", frm).val();
        var aciklama = $("#Aciklama", frm).val();
        var musteriId = $("#MusteriID", frm).val();
        var irsaliye = $("#Irsaliye", frm).val();
        var duzenlemeTarih = $("#DuzenlemeTarih", frm).val();
        var vadeTarihi = $("#VadeTarihi", frm).val();
        var faturaNoSeri = $("#FaturaNoSeri", frm).val();
        var faturaNoSira = $("#FaturaNoSira", frm).val();
        var faturaDovizi = $("#FaturaDovizi", frm).val();
        var data = urunlerTablo
                   .rows()
                   .data();


            var fatura = {
                id: id,
                aciklama: aciklama,
                musteriId: musteriId,
                irsaliye: irsaliye,
                duzenlemeTarih: duzenlemeTarih,
                vadeTarihi: vadeTarihi,
                faturaNoSeri: faturaNoSeri,
                faturaNoSira: faturaNoSira,
                faturaDovizi: faturaDovizi
        }
        
            $.ajax({
                url: "/panel/FaturaEkle",
                async: false,
                data: { fatura},
                type: "post",
                success: function (sonucu) {

                        var faturaId = sonucu;
                        alert(sonucu)
                    $("#urunler > tbody > tr").each(function (index, item) {

                            var hizmetUrunId = $(this).find(".HizmetUrunID").val();
                            var miktar = $(this).find(".Miktar").val();
                            var birimFiyat = $(this).find(".BirimFiyat").val();
                            var vergi = $(this).find(".Vergi").val();

                        //alert($(item).find(".HizmetUrunID").val())
                        // alert($(item).find(".Vergi").val())

                            var hizmetUrunFatura = {
                                faturaId: faturaId,
                                hizmetUrunId: hizmetUrunId,
                                miktar: miktar,
                                birimFiyat: birimFiyat,
                                vergi: vergi
                            }

                            $.ajax({
                                url: "/panel/HizmetUrunFaturaEkle",
                                async: false,
                                data: { hizmetUrunFatura },
                                type: "post",
                                success: function (sonucu) {
                                    if (sonucu) {

                                    } else {
                                        bootbox.alert({
                                            message: "Hata.. Lütfen girdiğiniz bilgileri tekrar kontrol ediniz."

                                        })
                                    }
                                }

                            })
                        })

                }

        })
        //  $("#urunler tbody tr").each(function () {
        //    var hizmetUrunId = $(this).find(".HizmetUrunID").val();
        //    var miktar = $(this).find(".Miktar").val();
        //    var birimFiyat = $(this).find(".BirimFiyat").val();
        //    var vergi = $(this).find(".Vergi").val();

        //    var hizmetUrunFatura = {
        //        faturaId: faturaId,
        //        hizmetUrunId: "5",
        //        miktar: miktar,
        //        birimFiyat: birimFiyat,
        //        vergi: vergi

        //    }

        //    $.ajax({
        //        url: "/panel/HizmetUrunFaturaEkle",
        //        async: false,
        //        data: { hizmetUrunFatura },
        //        type: "post",
        //        success: function (sonucu) {
        //            if (sonucu) {
        //                alert(sonucu)
        //            } else {
        //                bootbox.alert({
        //                    message: "Hata.. Lütfen girdiğiniz bilgileri tekrar kontrol ediniz."

        //                })
        //            }
        //        }

        //    })
        //})

        return false;
    }
}
