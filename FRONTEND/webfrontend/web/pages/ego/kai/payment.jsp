<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">

<%@ include file="/pages/taglibs.jsp" %>
<html>
    <head>
        <title>Ticketing Kereta API</title>
        <link REL="SHORTCUT ICON" HREF="img/ireload.ico" >
        <link href="css/kereta.css" rel="stylesheet" type="text/css">        
        <script type="text/javascript" src="js/tinybox.js"></script>
        <link href="css/tinybox.css" rel="stylesheet"/>

        <link href="css/jquery-ui.css" rel="stylesheet"/>
        <script src="js/jquery.js"></script>
        <script src="js/jquery-ui.min.js"></script>

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
        <table align="center" class="itJadwalText" width="890px">
            <tr>
                <td colspan="3">
                    <div class="item-page">
                        <h1>PEMBAYARAN KA</h1>
                    </div>			
                </td>
            </tr>
            <tbody>
                <tr class="itHaederTable">
                    <td height="40" colspan="3">Info Perjalanan</td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table style="width:100%">
                            <tbody>
                                <tr>
                                    <td class="bold" width="20%"><b>Tanggal</b></td>
                                    <td class="bold" width="10%"><b>No. Kereta</b></td>
                                    <td class="bold" width="20%"><b>Nama Kereta</b></td>
                                    <td class="bold" width="25%"><b>Berangkat</b></td>
                                    <td class="bold" width="25%"><b>Tiba</b></td>
                                </tr>
                                <tr>
                                    <td>${actionBean.jadwal.dateStart}</td>
                                    <td>${actionBean.jadwal.kode}</td>
                                    <td>${actionBean.jadwal.name}</td>
                                    <td>${actionBean.jadwal.dateStart}, ${actionBean.jadwal.timeStart}<br>${actionBean.asalName} (${actionBean.asal})</td>
                                    <td>${actionBean.jadwal.dateEnd}, ${actionBean.jadwal.timeEnd}<br>${actionBean.tujuanName} (${actionBean.tujuan})</td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr class="itHaederTable">
                    <td height="40" colspan="3">Info Booking</td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table style="width:100%">
                            <tbody>
                                <tr>
                                    <td class="bold"><b>No Booking Anda : ${actionBean.book}</b></td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr class="itHaederTable">
                    <td colspan="3">Info Penumpang</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table style="width:100%">
                            <tbody>
                                <tr>
                                    <td class="bold" width="25%"><b>Nama Penumpang</b></td>
                                    <td class="bold" width="25%"><b>Nomor Identitas</b></td>
                                    <td class="bold" width="25%"><b>Tipe Penumpang</b></td>
                                    <td class="bold" width="25%"><b>Tempat Duduk Penumpang</b></td>	
                                </tr>
                                <c:forEach items="${actionBean.penumpangs}" var="penumpang" >
                                    <tr>
                                        <td>${penumpang.nama}</td>
                                        <td>${penumpang.identitas}</td>
                                        <td>${penumpang.tipe}</td>
                                        <td>${penumpang.seat}</td>
                                    </tr>
                                </c:forEach>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr class="itHaederTable">
                    <td colspan="3">Info Harga</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table style="width:100%">
                            <tbody>
                                <tr>
                                    <td class="bold"><b>Jenis Penumpang</b></td>
                                    <td class="bold"><b>Jumlah Penumpang</b></td>
                                    <td class="bold" colspan="2"><b>Harga Satuan</b></td>
                                    <td class="bold" colspan="2"><b>Total Harga</b></td>
                                </tr>
                                <tr>
                                    <td>Dewasa</td>
                                    <td>${actionBean.dewasa}</td>
                                    <td>Rp.</td>
                                    <td align="right"><fmt:formatNumber type="currency" value="${actionBean.jadwal.priceAdult}" pattern="###,###,###.###" /></td>
                                    <td>Rp.</td>
                                    <td align="right"><fmt:formatNumber type="currency" value="${actionBean.priceAdult}" pattern="###,###,###.###" /></td>
                                </tr>
                                <tr>				
                                    <td>Anak</td>
                                    <td>${actionBean.anak}</td>
                                    <td>Rp.</td>
                                    <td align="right"><fmt:formatNumber type="currency" value="${actionBean.jadwal.priceChild}" pattern="###,###,###.###" /></td>
                                    <td>Rp.</td>
                                    <td align="right"><fmt:formatNumber type="currency" value="${actionBean.priceChild}" pattern="###,###,###.###" /></td>
                                </tr>
                                <tr>				
                                    <td>Infant</td>
                                    <td>${actionBean.infant}</td>
                                    <td>Rp.</td>
                                    <td align="right"><fmt:formatNumber type="currency" value="${actionBean.jadwal.priceInfant}" pattern="###,###,###.###" /></td>
                                    <td>Rp.</td>
                                    <td align="right"><fmt:formatNumber type="currency" value="${actionBean.priceInfant}" pattern="###,###,###.###" /></td>
                                </tr>
                                <tr>
                                    <td colspan="4">Biaya Admin</td>
                                    <td>Rp.</td>
                                    <td align="right"><span id="ibook_pay_type"><fmt:formatNumber type="currency" value="${actionBean.biaya}" pattern="###,###,###.###" /></span></td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="4"><b>HARGA TOTAL :</b></td>
                                    <td><b>Rp.</b></td>
                                    <td align="right"><span id="total_pay"><b><fmt:formatNumber type="currency" value="${actionBean.total}" pattern="###,###,###.###" /></b></span></td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr class="itHaederTable">
                    <td colspan="3">Pembayaran</td>
                </tr>    
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <s:form beanclass="com.ics.ssk.ego.web.KAIPayment">
                <s:hidden name="id" />
                <table style="width:100%">
                    <tbody>
                        <c:if test="${actionBean.message.price > actionBean.message.currentCashNote}">
                            <tr><td height="30" align="center" colspan="3">
                                    <div class="btnformTR">
                                        <s:button name="cashPayment" class="button2" onclick="TINY.box.show({iframe:'egocashpayment.html?id=${actionBean.id}',post:'id=16',width:1000,height:450})">Bayar Cash</s:button>
                                        <s:submit name="back" class="button2"/>
                                    </div>  
                                </td></tr>
                        </c:if>
                        <c:if test="${actionBean.message.price <= actionBean.message.currentCashNote}">
                            <tr>
                                <td class="bold"><b>Cash</b></td>
                                <td class="bold"><b>: <fmt:formatNumber type="currency" value="${actionBean.message.currentCashNote}" pattern="###,###,###.###" /></b></td>
                            </tr>
                            <c:if test="${actionBean.message.price < actionBean.message.currentCashNote}">
                                <tr>
                                    <td class="bold"><b>Change</b></td>                                        
                                    <td class="bold"><b>: <fmt:formatNumber type="currency" value="${actionBean.message.changeCashNote}" pattern="###,###,###.###" /></b></td>                                    
                                </tr>
                            </c:if>
                            <c:if test="${actionBean.message.changeNotPaid > 0}">
                                <tr><td/>
                                    <td height="30" align="center" colspan="3">
                                        Maaf mesin tidak dapat mengeluarkan uang kembalian sebesar 
                                        <fmt:formatNumber type="currency" value="${actionBean.message.changeAmount}" pattern="###,###,###.###" />.
                                        Sisa uang sebesar <fmt:formatNumber type="currency" value="${actionBean.message.changeNotPaid}" pattern="###,###,###.###" />
                                        akan didonasikan.
                                    </td></tr>
                            </c:if>                                    
                            <tr><td height="30" align="center" colspan="3">    
                                    <div class="btnformTR">
                                        <s:submit id="buttonSend" name="exePayment" class="button2" value="next"/>
                                        <s:submit name="cancel" class="button2"/>
                                    </div>
                                </td></tr>
                         </c:if>
                    </tbody>
                </table>
            </s:form>
            </tbody>
        </table>
    </section>
</body>
</html>