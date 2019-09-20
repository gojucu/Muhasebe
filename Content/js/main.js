

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
            data: { fatura },
            type: "post",
            success: function (sonucu) {

                var faturaId = sonucu;
                $("#urunler > tbody > tr").each(function (index, item) {
                    var Id = $(this).find(".altId").val();
                    var hizmetUrunId = $(this).find(".HizmetUrunID").val();
                    var miktar = $(this).find(".Miktar").val();
                    var birimFiyat = $(this).find(".BirimFiyat").val();
                    var vergi = $(this).find(".Vergi").val();
                    

                    var hizmetUrunFatura = {
                        Id: Id,
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
        return false;
    }
}
