<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">

<%@ include file="/pages/taglibs.jsp" %>
<html>
    <head>
        <title>Ticketing Kereta API</title>
        <link REL="SHORTCUT ICON" HREF="img/ireload.ico" >
        <link href="css/kereta.css" rel="stylesheet" type="text/css">
        <script>
            function checkChecking()
            {
                var prim = document.getElementById("persetujuan");
                if (prim.checked == true) {
                    return true;
                } else {
                    alert("Anda harus menyetujui persetujuan yang ada");
                    return false;
                }
            }    
        </script>
        <script type="text/javascript" src="js/countdown/countdown.js"></script>
    </head>
    <body>
    <section class="area-content">
        <table border="0" width="100%">
            <tr>
                <td align="center">
                    <img src="img/logoEGO.png" alt="" height="40" width=160"/>
                    <h1>Ticketing Kereta API</h1>                    
                </td>
            </tr>
        </table>
        <table width="20%" border="0" align="right">
            <tr>
                <td colspan="3">
                    <div class="item-page">
                        <h1>Waktu Booking</h1>
                    </div>			
                </td>
            </tr>
            <tr>
                <td align="center">
                    <script type="application/javascript">
                        function countdownComplete(){
                        alert("Waktu Booking Anda Habis. Silahkan Mengulang Kembali.");
                        window.location.href="bookkaidatetime.html";
                        }

                        var myCountdown2 = new Countdown({
                        time: ${actionBean.detik}, 
                        width:200, 
                        height:80,
                        style:"flip",
                        onComplete : countdownComplete, // Specify a function to call when done
                        rangeHi:"minute"	// <- no comma on last item!
                        });



                    </script>
                </td>
            </tr>
        </table>
        <table align="center" class="itJadwalText" width="800px">
            <tr>
                <td colspan="3">
                    <div class="item-page">
                        <h1>Reservasi KA</h1>
                    </div>			
                </td>
            </tr>
            <tbody>
                <tr class="itHaederTable">
                    <td height="40" colspan="3">Informasi Reservasi Kereta Api</td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td width="75">Nama KA</td>
                    <td width="5">:</td>
                    <td><span class="TitleRelasiJadwalA">${actionBean.jadwal.name}</span> <span class="TitleRelasiJadwalB">(${actionBean.jadwal.kode})</span></td>
                </tr>
                <tr>
                    <td>Tanggal KA</td>
                    <td>:</td>
                    <td>${actionBean.jadwal.dateStart}</td>
                </tr>
                <tr>
                    <td>Berangkat</td>
                    <td>:</td>
                    <td>${actionBean.asalName} (${actionBean.asal}).&nbsp;&nbsp;${actionBean.jadwal.dateStart}&nbsp;-&nbsp;${actionBean.jadwal.timeStart}</td>
                </tr>
                <tr>
                    <td>Tiba</td>
                    <td>:</td>
                    <td>${actionBean.tujuanName} (${actionBean.tujuan}).&nbsp;&nbsp;${actionBean.jadwal.dateEnd}&nbsp;-&nbsp;${actionBean.jadwal.timeEnd}</td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td>Kelas </td>
                    <td>:</td>
                    <td>${actionBean.jadwal.clazz}</td>
                </tr>
                <tr>
                    <td>Tarif</td>
                    <td>:</td>
                    <td>
                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                            <tbody>
                                <tr title="" class="itRowTableBlank">
                                    <td height="24">Rp. <fmt:formatNumber type="currency" value="${actionBean.jadwal.priceAdult}" pattern="###,###,###.###" /> (Dewasa)</td>
                                </tr>
                                <tr title="" class="itRowTableBlank">
                                    <td height="24">Rp. <fmt:formatNumber type="currency" value="${actionBean.jadwal.priceChild}" pattern="###,###,###.###" /> (Anak)</td>
                                </tr>
                                <tr title="" class="itRowTableBlank">
                                    <td height="24">Rp. <fmt:formatNumber type="currency" value="${actionBean.jadwal.priceInfant}" pattern="###,###,###.###" /> (Infant)</td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td>Dewasa</td>
                    <td>:</td>
                    <td>${actionBean.dewasa}</td>
                </tr>
                <tr>
                    <td>Anak</td>
                    <td>:</td>
                    <td>${actionBean.anak}</td>
                </tr>
                <tr>
                    <td>Infant</td>
                    <td>:</td>
                    <td>${actionBean.infant}</td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr class="itHaederTable">
                    <td colspan="3">Ketentuan Reservasi dan Pembayaran</td>
                </tr>
                <tr class="itRowTable4">
                    <td colspan="3">
                        <font color="#FF0000">- Reservasi dapat dilakukan 6 jam sebelum kereta berangkat</font><br>
                        <font color="#FF0000">- Harga dan ketersediaan tempat duduk sewaktu waktu dapat berubah</font><br>
                        <font color="#FF0000">- Pastikan anda mencetak struk pembayaran dari PT Indo Cipta Guna untuk ditukarkan dengan tiket di stasiun online</font>
                    </td>
                </tr>
                <!--
                        <tr>
                                <td colspan="3">&nbsp;</td>
                        </tr>
                -->	
                <tr>
                    <td colspan="3">
                        <table width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td width="25">
                                        <input type="checkbox" value="true" name="persetujuan" id="persetujuan">
                                    </td>
                                    <td>
                                        <div align="justify">Dengan ini saya setuju dan mematuhi ketentuan di atas<div></div></div>
                                    </td>
                                </tr>
                                <!--
                                <tr>
                                  <td align="center">&nbsp;</td>
                                  <td>
                                    <b><u><a style="color:#000000" href="javascript:toggle_detail();" id="displayText_detail">Detil Syarat dan Ketentuan Reservasi [show]</a></u></b>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="2"><script language="javascript">function toggle_detail(){var ele_detail = document.getElementById("toggleText_detail");var text_detail = document.getElementById("displayText_detail");if(ele_detail.style.display == "block"){ele_detail.style.display = "none";text_detail.innerHTML = "Detil Syarat dan Ketentuan Reservasi [show]";} else {ele_detail.style.display = "block";text_detail.innerHTML = "Detil Syarat dan Ketentuan Reservasi [hide]";}}</script><div style="display: none" id="toggleText_detail"><table width="100%" border="0">
                                  <tbody><tr>
                                    <td valign="top" align="center" colspan="3">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td valign="top" align="center" colspan="3"><strong>PERSYARATAN DAN KETENTUAN ANGKUTAN PENUMPANG KERETA API</strong></td>
                                  </tr>
                                  <tr>
                                    <td valign="top" colspan="3">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">1.</td>
                                    <td valign="top" align="justify" colspan="2">Dengan ini Anda  
                                menyatakan persetujuan terhadap Persyaratan dan Ketentuan Angkutan 
                                Penumpang  Kereta Api termasuk tetapi tidak terbatas ketentuan 
                                reservasi</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">2.</td>
                                    <td valign="top" align="justify" colspan="2">Anda akan mematuhi  
                                persyaratan dan ketentuan Reservasi , termasuk pembayaran, mematuhi 
                                semua  aturan dan pembatasan mengenai ketersediaan tarif serta 
                                bertanggung jawab untuk  semua biaya yang timbul dari penggunaan 
                                fasilitas Reservasi Online Tiket Kereta  Api</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">3.</td>
                                    <td valign="top" align="justify" colspan="2">PT Indo Cipta Guna
                                  berhak atas kebijaksanaan untuk mengubah, menyesuaikan, 
                                menambah atau  menghapus salah satu syarat dan kondisi yang tercantum di
                                 sini, dan / atau  mengubah, menangguhkan atau menghentikan setiap aspek
                                 dari Reservasi Online Tiket Kereta Api. PT Indo Cipta Guna  
                                tidak diwajibkan untuk menyediakan pemberitahuan sebelum 
                                memasukkan  salah satu perubahan di atas dan / atau modifikasi ke dalam 
                                Reservasi Online Tiket Kereta Api</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">4.</td>
                                    <td valign="top" align="justify" colspan="2">PT Indo Cipta Guna
                                  akan menggunakan informasi pribadi yang Anda berikan melalui
                                  fasilitas ini hanya untuk tujuan reservasi dan pencatatan database 
                                pelanggan PT Indo Cipta Guna</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">5.</td>
                                    <td valign="top" align="justify" colspan="2">Anda tidak dibenarkan  
                                menggunakan fasilitas ini untuk tujuan yang melanggar hukum atau 
                                dilarang,  termasuk tetapi tidak terbatas untuk membuat reservasi yang 
                                tidak sah,  spekulatif, palsu atau penipuan atau menjualnya kembali 
                                secara tidak sah. PT Indo Cipta Guna dapat membatalkan 
                                atau menghentikan penggunaan  Anda atas fasilitas ini setiap saat tanpa 
                                pemberitahuan jika dicurigai</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">6.</td>
                                    <td valign="top" align="justify" colspan="2">PT Indo Cipta Guna
                                  tidak menjamin bahwa Reservasi Online Tiket Kereta Api akan 
                                bebas kesalahan, bebas dari virus atau  elemen lain yang berbahaya</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">7.</td>
                                    <td valign="top" colspan="2">Syarat dan ketentuan  fasilitas ini 
                                diatur dan ditafsirkan sesuai peraturan internal PT Indo Cipta Guna dan
                                peraturan perundang-undangan yang berlaku di Republik  Indonesia</td>
                                  </tr>
                                  <tr>
                                    <td valign="top" colspan="3">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td valign="top" colspan="3"><strong>Ketentuan Umum</strong></td>
                                  </tr>
                                  <tr>
                                    <td width="20" valign="top">1. </td>
                                    <td width="20" valign="top" align="justify" colspan="2">Tarif sudah termasuk asuransi</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">2.</td>
                                    <td valign="top" align="justify" colspan="2">Tarif berlaku berada 
                                pada rentang tarif batas  atas (TBA) dan tarif batas bawah (TBB)  yang 
                                ditetapkan oleh Direksi PT Kereta Api Indonesia (Persero) berdasarkan 
                                Peraturan Menteri Perhubungan Republik  Indonesia dan dapat berubah 
                                sewaktu-waktu dalam rentang tbb-tba dimaksud</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">3.</td>
                                    <td valign="top" align="justify" colspan="2">Semua penumpang 
                                dikenakan tarif dewasa dan  berhak atas nomor tempat duduk kecuali pada 
                                Kereta Api Komuter dapat  diberlakukan bebas tempat duduk</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">4.</td>
                                    <td valign="top" align="justify" colspan="2">Khusus Anak usia 
                                dibawah 3 tahun pada kereta api  jarak jauh dan menengah berhak atas 
                                reduksi tarif sebesar 90% jika tidak  mengambil tempat duduk sendiri</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">5.</td>
                                    <td valign="top" align="justify" colspan="2">Penumpang berusia diatas 60 tahun berhak atas  reduksi tarif sebesar 20 %</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">6.</td>
                                    <td valign="top" align="justify" colspan="2">Ketentuan tentang tarif reduksi lainnya  ditetapkan oleh Direksi PT Kereta Api Indonesia (Persero)</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">7.</td>
                                    <td valign="top" align="justify" colspan="2">Tiket hanya berlaku untuk pengangkutan dari  stasiun keberangkatan ke stasiun kedatangan sebagaimana tercantum dalam tiket</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">8.</td>
                                    <td valign="top" align="justify" colspan="2">Tiket berlaku dan sah apabila :</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">&nbsp;</td>
                                    <td valign="top" align="justify">a.</td>
                                    <td valign="top" align="justify">Dipergunakan oleh penumpang yang 
                                namanya  tercantum pada tiket dibuktikan dengan kartu identitas 
                                penumpang yang  bersangkutan dan tidak dapat dipindah tangankan</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">&nbsp;</td>
                                    <td valign="top" align="justify">b.</td>
                                    <td valign="top" align="justify">Nama dan nomor kereta api, tanggal 
                                dan jam  keberangkatan, kelas dan relasi yang tercantum dalam tiket 
                                telah sesuai dengan  Kereta Api yang dinaiki</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">9.</td>
                                    <td valign="top" align="justify" colspan="2">Kedapatan tidak memiliki tiket yang sah diatas  KA diturunkan dari kereta api pada kesempatan pertama</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">&nbsp;</td>
                                    <td valign="top" align="justify" colspan="2">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td valign="top" colspan="3"><strong>Ketentuan Reservasi Online</strong></td>
                                  </tr>
                                  <tr>
                                    <td valign="top">1.</td>
                                    <td valign="top" align="justify" colspan="2">Fasilitas Reservasi 
                                Online Tiket Kereta Api hanya berlaku untuk  Perjalanan Kereta Api yang 
                                tercantum dalam Sistem Reservasi Online Tiket Kereta Api</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">2.</td>
                                    <td valign="top" align="justify" colspan="2">Reservasi Online dapat dilakukan untuk  perjalanan H-90 s.d 6 jam sebelum jadwal keberangkatan Kereta Api</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">3.</td>
                                    <td valign="top" align="justify" colspan="2">Bukti Pembayaran Tiket 
                                Kereta Api akan  dikeluarkan setelah pembayaran disetujui. Berisi data 
                                yang sesuai dengan data  pemesanan tiket. Oleh karena itu, setelah
                                melakukan reservasi, Anda harus selalu memeriksa data pemesanan 
                                sebagaimana yang tercetak dalam struk</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">4.</td>
                                    <td valign="top" align="justify" colspan="2">Pembayaran dapat 
                                dilakukan dengan menggunakan pilihan pembayaran yang disediakan secara online</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">5.</td>
                                    <td valign="top" align="justify" colspan="2">Batas waktu Pembayaran untuk transaksi reservasi  tiket KA online adalah 3 jam</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">6.</td>
                                    <td valign="top" align="justify" colspan="2">Apabila dalam batas 
                                waktu 3 jam tidak dilakukan  pembayaran maka seat yang telah dipesan 
                                akan dilepas kembali dan kode booking  menjadi tidak valid</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">&nbsp;</td>
                                    <td valign="top" align="justify" colspan="2">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td valign="top" colspan="3"><strong>Ketentuan Boarding</strong></td>
                                  </tr>
                                  <tr>
                                    <td valign="top" align="justify" colspan="3">Pada saat melakukan 
                                boarding  semua penumpang berusia diatas 17 tahun wajib menunjukan bukti
                                 identitas diri  yang resmi (KTP/SIM/Pasport/ID Lainnya), dengan nama 
                                tertera pada kartu  identitas sama dengan yang tertera pada tiket. 
                                Apabila tidak dapat menunjukan  bukti identitas yang sama dengan nama 
                                yang tertera pada tiket tidak diperkenankan  masuk</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">&nbsp;</td>
                                    <td valign="top" align="justify" colspan="2">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td valign="top" colspan="3"><strong>Ketentuan Penukaran Tiket</strong></td>
                                  </tr>
                                  <tr>
                                    <td valign="top">1.</td>
                                    <td valign="top" align="justify" colspan="2">Anda harus mencetak 
                                Bukti Pembayaran Tiket  Kereta Api dan menukarkannya dengan  Tiket di 
                                loket Stasiun paling lambat 1 jam sebelum jadwal keberangkatan yang 
                                tertera pada tiket</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">2.</td>
                                    <td valign="top" align="justify" colspan="2">Dalam hal bukti 
                                Pembayaran disajikan dalam  bentuk elektronik (contoh sms notifikasi, 
                                email) dan tidak memungkinkan untuk  dicetak terlebih dahulu, maka pada 
                                saat  menukarkan, penumpang harus dapat menunjukan bukti transaksi 
                                tersebut dan Kartu  Identitas asli yang sesuai dengan data penumpang 
                                pada bukti pembayaran  tersebut serta menyerahkan fotokopi kartu 
                                identitasnya</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">3.</td>
                                    <td valign="top" align="justify" colspan="2">Bukti Pembayaran Tiket 
                                Kereta Api hanya  referensi status tiket, status valid  adalah status 
                                yang ditunjukkan dalam sistem Tiketing PT Kereta Api Indonesia  
                                (Persero)</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">4.</td>
                                    <td valign="top" align="justify" colspan="2">Apabila pembayaran 
                                dilakukan dengan menggunakan  Kartu Kredit maka Pemegang kartu kredit 
                                harus menjadi bagian dari penumpang  bepergian, PT Kereta Api Indonesia 
                                (Persero) memiliki hak untuk menolak  penumpang untuk melakukan 
                                perjalanan jika nama pemegang kartu tidak termasuk  dalam daftar nama 
                                penumpang</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">&nbsp;</td>
                                    <td valign="top" align="justify" colspan="2">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td valign="top" colspan="3"><strong>Ketentuan Pembatalan dan Perubahan Jadwal</strong></td>
                                  </tr>
                                  <tr>
                                    <td valign="top">1.</td>
                                    <td valign="top" align="justify" colspan="2">Permohonan pembatalan dapat dilakukan di semua  stasiun online</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">2.</td>
                                    <td valign="top" align="justify" colspan="2">Permohonan pembatalan 
                                tiket dapat dilakukan selambat-lambatnya  30 menit sebelum jadwal 
                                keberangkatan kereta api sebagaimana tercantum dalam  tiket yang telah 
                                dibeli</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">3.</td>
                                    <td valign="top" align="justify" colspan="2">Permohonan pembatalan 
                                kurang dari 30 menit  sebelum jadwal keberangkatan kereta api maka tiket
                                 hangus, tidak ada  pengembalian bea</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">4.</td>
                                    <td valign="top" align="justify" colspan="2">Pengembalian bea tiket 
                                dapat dapat dilakukan  secara tunai atau ditransfer ke rekening pemohon 
                                pembatalan dengan biaya  transfer ditanggung KAI</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">5.</td>
                                    <td valign="top" align="justify" colspan="2">Bea pembatalan akan 
                                ditransfer atau dapat  diambil secara tunai di stasiun yang telah 
                                ditentukan pada hari ke-30 sampai dengan  hari ke-45 setelah permohonan 
                                pembatalan</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">6.</td>
                                    <td valign="top" align="justify" colspan="2">Pembatalan tiket maupun perubahan jadwal  dikenakan bea administrasi sebesar 25% dari harga tiket diluar bea pesan</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">7.</td>
                                    <td valign="top" align="justify" colspan="2">Pemohon mengisi 
                                formulir pembatalan tiket dan  melampirkan tiket yang akan dibatalkan 
                                beserta fotokopi kartu identitas yang  sesuai dengan nama yang tercetak 
                                pada tiket</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">8.</td>
                                    <td valign="top" align="justify" colspan="2">Formulir pembatalan 
                                terdiri dari rangkap 2,  lembar pertama untuk KAI, lembar kedua 
                                diberikan kepada Pemohon pembatalan dan dipergunakan sebagai bukti pada 
                                saat  pengambilan bea pembatalan jika pilihan pengembalian bea secara 
                                tunai</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">9.</td>
                                    <td valign="top" align="justify" colspan="2">Pembatalan yang 
                                diakibatkan tidak  terselenggaranya angkutan karena alasan operasional 
                                maka bea tiket diluar bea  pesan dikembalikan penuh</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">&nbsp;</td>
                                    <td valign="top" align="justify" colspan="2">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td valign="top" colspan="3"><strong>Lain-Lain</strong></td>
                                  </tr>
                                  <tr>
                                    <td valign="top">1.</td>
                                    <td valign="top" align="justify" colspan="2">Perhitungan biaya pembatalan, perubahan jadwal  dan reduksi tarif dilakukan pembulatan ke atas pada kelipatan Rp 1.000,-</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">2.</td>
                                    <td valign="top" align="justify" colspan="2">Berat bagasi tangan 
                                yang boleh dibawa ke dalam  kabin kereta untuk tiap penumpang maksimum 
                                20 Kg dengan volume maksimum 100 dm3  ,kelebihan berat begasi dikenakan 
                                bea tambahan</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">3.</td>
                                    <td valign="top" align="justify" colspan="2">Barang yang tidak 
                                diperbolehkan diangkut sebagai  bagasi tangan adalah binatang, narkotika
                                 psikotoprika dan zat adiktif lainnya,  senjata api dan senjata tajam, 
                                semua barang yang mudah menyala/meledak,  barang-barang yang karena 
                                sifatnya dapat mengganggu/merusak kesehatan, berbau  busuk, 
                                barang-barang yang menurut pertimbangan pegawai karena keadaan dan  
                                besarnya tidak pantas diangkut sebagai bagasi tangan, barang-barang yang
                                 dilarang undang-undang</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">4.</td>
                                    <td valign="top" align="justify" colspan="2">Larangan pengangkutan 
                                bagi orang dalam keadaan  mabuk dan orang yang dapat mengganggu atau 
                                membahayakan penumpang lain, orang  yang dihinggapi penyakit menular 
                                atau orang yang menurut undang-undang dapat  dikenakan peraturan 
                                pengasingan untuk kesehatannya</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">5.</td>
                                    <td valign="top" align="justify" colspan="2">Semua Perjalanan Kereta Api adalah perjalanan  Bebas Rokok, tidak diperkenankan merokok di seluruh rangkaian Kereta Api</td>
                                  </tr>
                                  <tr>
                                    <td valign="top">&nbsp;</td>
                                    <td valign="top" align="justify" colspan="2">&nbsp;</td>
                                  </tr>
                                  </tbody></table>
                                </div>
                                </td>
                                </tr>
                                -->
                            </tbody>
                        </table>
                    </td>
                </tr>
                <!--
                <tr>
                <td colspan="3">&nbsp;</td>
                </tr>
                -->
                <tr>
                    <td height="30" align="center" colspan="3">

                        <s:form beanclass="com.ics.ssk.ego.web.KAIConfirmation" onsubmit="return checkChecking();">
                            <s:hidden name="keySession" />
                            <input type="submit" class="button2" value="Lanjutkan" name="booking">
                        </s:form>
                    </td>
                </tr>
            </tbody>
        </table>
    </section>
</body>
</html>