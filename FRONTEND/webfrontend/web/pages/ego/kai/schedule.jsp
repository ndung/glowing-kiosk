<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">

<%@ include file="/pages/taglibs.jsp" %>
<html>
    <head>
        <title>Ticketing Kereta API</title>
        <link REL="SHORTCUT ICON" HREF="img/ireload.ico" >
        <link href="css/kereta.css" rel="stylesheet" type="text/css">
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

        <table align="center" class="itJadwalText">
            <tr>
                <td colspan="3">
                    <div class="item-page">
                        <h1>Info Jadwal dan Reservasi</h1>
                    </div>			
                </td>
            </tr>
            <tbody>
                <tr>
                    <td colspan="3">
                        <span class="TitleRelasiJadwalA">${actionBean.asalName}</span>
                        <span class="TitleRelasiJadwalB">(${actionBean.asal})</span>
                        <span class="TitleRelasiJadwalA"> - ${actionBean.tujuanName}</span>
                        <span class="TitleRelasiJadwalB">(${actionBean.tujuan})</span>
                    </td>
                </tr>
                <tr>
                    <td>${actionBean.tanggal}</td>
                    <td width="50">&nbsp;</td>
                    <td align="right">Dewasa : ${actionBean.dewasa} &nbsp;&nbsp;&nbsp; Anak : ${actionBean.anak} &nbsp;&nbsp;&nbsp; Infant : ${actionBean.infant}</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                            <tbody>
                                <tr class="itHaederTable">
                                    <td rowspan="2">KERETA API</td>
                                    <td colspan="2">JAM / TANGGAL</td>
                                    <td width="42%" rowspan="2">TARIF</td>
                                </tr>
                                <tr class="itHaederTable">
                                    <td width="15%">BERANGKAT</td>
                                    <td width="15%">TIBA</td>
                                </tr>
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                </tr>
                                <c:forEach items="${actionBean.jadwals}" var="kereta" >
                                    <tr title="${kereta.name} - Naomor KA : ${kereta.kode} " class="itRowTable0">
                                        <td height="25" colspan="4"><b>${kereta.name}</b></td>
                                    </tr>

                                    <tr class="itRowTable2">
                                        <td valign="top">
                                            <table border="0">
                                                <tbody>
                                                    <tr title="${kereta.name} - Nomor KA : ${kereta.kode} " class="itRowTableBlank">
                                                        <td width="40%" height="25">${kereta.kode}</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td valign="top" align="center">
                                            <table border="0">
                                                <tbody>
                                                    <tr title="" class="itRowTableBlank">
                                                        <td height="25" align="center">${kereta.timeStart}<br>${kereta.dateStart}</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td valign="top" align="center">
                                            <table border="0">
                                                <tbody>
                                                    <tr title="" class="itRowTableBlank">
                                                        <td height="25" align="center">${kereta.timeEnd}<br>${kereta.dateEnd}</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td valign="top" align="center">
                                            <table width="100%" border="0">
                                                <tbody>
                                                    <c:forEach items="${kereta.jadwals}" var="jadwal" >
                                                        <tr title="90" class="itRowTable1">
                                                            <td height="24">${jadwal.clazz} - Dewasa</td>
                                                            <td width="10">Rp.</td>
                                                            <td width="40" align="right"><fmt:formatNumber type="currency" value="${jadwal.priceAdult}" pattern="###,###,###.###" /></td>
                                                            <td width="90" align="center">
                                                                <c:if test="${jadwal.available == 1}">
                                                                    <s:form beanclass="com.ics.ssk.ego.web.KAISchedule" name="form_${jadwal.id}" id="form_${jadwal.id}">
                                                                        <s:hidden name="keySession" />
                                                                        <input type="hidden" value="${jadwal.id}" name="code">
                                                                        <input type="submit" class="button2" value="Booking" name="booking">
                                                                    </s:form>
                                                                </c:if>
                                                                <c:if test="${jadwal.available == 0}">
                                                                    <font color="#FF0000">Habis</font>
                                                                </c:if>
                                                            </td>
                                                        </tr>
                                                        <tr title="90" class="itRowTable1">
                                                            <td height="24">${jadwal.clazz} - Anak</td>
                                                            <td width="10">Rp.</td>
                                                            <td width="40" align="right"><fmt:formatNumber type="currency" value="${jadwal.priceChild}" pattern="###,###,###.###" /></td>
                                                            <td width="90" align="center">
                                                                <b>Sisa : ${jadwal.seat} Kursi</b>
                                                            </td>
                                                        </tr>
                                                        <tr title="90" class="itRowTable1">
                                                            <td height="24">${jadwal.clazz} - Infant</td>
                                                            <td width="10">Rp.</td>
                                                            <td width="40" align="right"><fmt:formatNumber type="currency" value="${jadwal.priceInfant}" pattern="###,###,###.###" /></td>
                                                        </tr>
                                                    </c:forEach>					
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">&nbsp;</td>
                                    </tr>
                                </c:forEach>
                                <tr>
                                    <td colspan="4">
                                        <br><font color="#FF0000">- Reservasi dapat dilakukan 6 jam sebelum kereta berangkat</font><br><font color="#FF0000">- Harga dan ketersediaan tempat duduk sewaktu waktu dapat berubah</font><br><font color="#FF0000">- Pastikan anda mencetak struk pembayaran dari PT Indo Cipta Guna untuk ditukarkan dengan tiket di stasiun online</font>
                                    </td>
                                </tr>                                
                            </tbody>
                        </table>
                    </td>
                </tr>
                <s:form beanclass="com.ics.ssk.ego.web.KAISchedule">
                <tr>
                    <td height="30" align="center" colspan="3">
                        <s:submit name="kembali" class="button2" value="Kembali"/>
                    </td>
                </tr>
                </s:form>
            </tbody>
        </table>
    </section>
</body>
</html>