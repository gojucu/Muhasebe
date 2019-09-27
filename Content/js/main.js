

var islemler = {
    UrunKaydet: function () {


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
        var araToplam = $(".AraToplam", frm).val();
        var kdvToplam = $(".KdvToplam", frm).val();
        var genelToplam = $(".GenelToplam", frm).val();

        var kategoriler = $("#kategoriler", frm).val();



        var fatura = {
            id: id,
            aciklama: aciklama,
            musteriId: musteriId,
            irsaliye: irsaliye,
            duzenlemeTarih: duzenlemeTarih,
            vadeTarihi: vadeTarihi,
            faturaNoSeri: faturaNoSeri,
            faturaNoSira: faturaNoSira,
            faturaDovizi: faturaDovizi,
            araToplam: araToplam,
            kdvToplam: kdvToplam,
            genelToplam: genelToplam
        }

        $.ajax({
            url: "/panel/FaturaEkle",
            async: false,
            data: { fatura, kategoriler },
            type: "post",
            success: function (sonucu) {

                var faturaId = sonucu;
                $("#urunler > tbody > tr").each(function (index, item) {
                    var Id = $(this).find(".altId").val();
                    var hizmetUrunId = $(this).find(".HizmetUrunID").val();
                    var miktar = $(this).find(".Miktar").val();
                    var birimFiyat = $(this).find(".BirimFiyat").val();
                    var vergi = $(this).find(".Vergi").val();
                    var toplam = $(this).find(".Toplam").val();
                    

                    var hizmetUrunFatura = {
                        Id: Id,
                        faturaId: faturaId,
                        hizmetUrunId: hizmetUrunId,
                        miktar: miktar,
                        birimFiyat: birimFiyat,
                        vergi: vergi,
                        toplam: toplam
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
        return false;
    },

    TahsilatKaydet: function () {
        var frm = $("#tahsilatEkleKaydetForm");
        var id = $("#Id",frm).val();
        var faturaId = $("#FaturaID", frm).val();
        var tarih = $("#Tarih", frm).val();
        var meblag = $("#Meblag", frm).val();
        var hesapId = $("#HesapID", frm).val();
        var aciklama = $("#Aciklama", frm).val();

        var islemler = {
            id: id,
            faturaId: faturaId,
            tarih: tarih,
            meblag: meblag,
            hesapId: hesapId,
            aciklama: aciklama
        }
        $.ajax({
            url: "/Panel/TahsilatEkle",
            async: false,
            data: islemler,
            type: "post",
            success: function (sonucu) {
                if (sonucu) {

                } else {
                    bootbox.alert({
                        message: "Hata.. Lütfen girdiğiniz bilgileri tekrar kontrol ediniz."

                    })
                }}
        })
    }
}
