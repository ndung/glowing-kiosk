<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">

<%@ include file="/pages/taglibs.jsp" %>
<html>
    <head>
        <title>Ticketing Kereta API</title>
        <link REL="SHORTCUT ICON" HREF="img/ireload.ico" >
        <link href="css/kereta.css" rel="stylesheet" type="text/css">
        <link href="css/datePicker.css" rel="stylesheet" type="text/css">
        <script type="text/javascript" src="js/countdown/countdown.js"></script>
        <script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
        <script type="text/javascript" src="js/jquery.datePicker.js"></script>
        <script type="text/javascript" src="js/date.js"></script>

        <link href="css/jquery-ui.css" rel="stylesheet"/>
        <script src="js/jquery.js"></script>
        <script src="js/jquery-ui.min.js"></script>

        <link href="css/keyboard.css" rel="stylesheet"/>
        <script src="js/jquery.keyboard.js"></script>

        <script src="js/jquery.mousewheel.js"></script>
        <script src="js/jquery.keyboard.extension-typing.js"></script>
        <script src="js/jquery.keyboard.extension-autocomplete.js"></script>

        <script src="js/demo.js"></script>
        <script src="js/jquery.jatt.min.js"></script>
        <script src="js/jquery.chili-2.2.js"></script>
        <script src="js/recipes.js"></script>
        
        <script type="text/javascript" src="js/slidedown-menu2.js"></script>        
        <script src="js/carousel/jcarousellite_1.0.1.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(document).ready(
            function(){
                $('.date-pick').datePicker({clickInput:true, startDate:'01/01/1945'})
            });
        
        </script>
    </head>
    <body>
    <section class="area-content">
        <s:form beanclass="com.ics.ssk.ego.web.KAIPassenger">
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
            <table align="center" class="itJadwalText" width="890px">
                <tr>
                    <td colspan="3">
                        <s:errors />
                        <s:messages />	
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div class="item-page">
                            <h1>INPUT DATA PENUMPANG KA</h1>
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
                    <s:form beanclass="com.ics.ssk.ego.web.KAIPassenger">

                        <tr class="itHaederTable">
                            <td colspan="3">Penumpang Dewasa</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width:30px" class="bold">No.</td>
                                            <td class="bold">Nama <span class="required">*</span></td>
                                            <td class="bold">ID(KTP,SIM,Passport) <span class="required">*</span></td>                                            
                                            <td class="bold">No Telp <span class="required">*</span></td>
                                        </tr>
                                        <tr>
                                            <td align="right">1.</td>
                                            <td><s:text id="name_adult_1" style="width:400px" maxlength="64" name="dewasaNama1" /></td>
                                            <td><s:text id="id_adult_1" style="width:294px" maxlength="24" name="dewasaKtp1" /></td>                                            
                                            <td><s:text id="numeric" name="contactPhone" /></td>
                                        </tr>
                                        <c:if test="${actionBean.dewasa > 1}">
                                            <tr>
                                                <td align="right">2.</td>
                                                <td><s:text style="width:400px" maxlength="64" id="name_adult_2" name="dewasaNama2" /></td>
                                                <td><s:text style="width:294px" maxlength="24" id="id_adult_2" name="dewasaKtp2" /></td>                                                
                                                <td></td>
                                            </tr>
                                        </c:if>
                                        <c:if test="${actionBean.dewasa > 2}">
                                            <tr>
                                                <td align="right">3.</td>
                                                <td><s:text style="width:400px" maxlength="64" id="name_adult_3" name="dewasaNama3" /></td>
                                                <td><s:text style="width:294px" maxlength="24" id="id_adult_3" name="dewasaKtp3" /></td>                                                
                                                <td></td>
                                            </tr>
                                        </c:if>
                                        <c:if test="${actionBean.dewasa > 3}">
                                            <tr>
                                                <td align="right">4.</td>
                                                <td><s:text style="width:400px" maxlength="64" id="name_adult_4" name="dewasaNama4" /></td>
                                                <td><s:text style="width:294px" maxlength="24" id="id_adult_4" name="dewasaKtp4" /></td>                                                
                                                <td></td>
                                            </tr>
                                        </c:if>				
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        <c:if test="${actionBean.anak > 0}">
                            <tr class="itHaederTable">
                                <td colspan="3">Penumpang Anak</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td style="width:30px" class="bold">No.</td>
                                                <td class="bold">Nama <span class="required">*</span></td>                                                
                                            </tr>
                                            <tr>
                                                <td align="right">1.</td>
                                                <td><s:text style="width:710px" maxlength="64" id="name_child_1" name="anakNama1"/></td>                                                
                                            </tr>
                                            <c:if test="${actionBean.anak > 1}">
                                                <tr>
                                                    <td align="right">2.</td>
                                                    <td><s:text style="width:710px" maxlength="64" id="name_child_2" name="anakNama2"/></td>                                                    
                                                </tr>
                                            </c:if>
                                            <c:if test="${actionBean.anak > 3}">
                                                <tr>
                                                    <td align="right">3.</td>
                                                    <td><s:text style="width:710px" maxlength="64" id="name_child_3" name="anakNama3"/></td>                                                    
                                                </tr>
                                            </c:if>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                        </c:if>
                        <c:if test="${actionBean.infant > 0}">
                            <tr class="itHaederTable">
                                <td colspan="3">Penumpang Infant</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td style="width:30px" class="bold">No.</td>
                                                <td class="bold">Nama <span class="required">*</span></td>                                                
                                            </tr>
                                            <tr>
                                                <td align="right">1.</td>
                                                <td><s:text style="width:710px" maxlength="64" id="name_infant_1" name="bayiNama1" /></td>                                                
                                            </tr>
                                            <c:if test="${actionBean.infant > 1}">
                                                <tr>
                                                    <td align="right">2.</td>
                                                    <td><s:text style="width:710px" maxlength="64" id="name_infant_2" name="bayiNama2" /></td>                                                    
                                                </tr>
                                            </c:if>
                                            <c:if test="${actionBean.infant > 2}">
                                                <tr>
                                                    <td align="right">3.</td>
                                                    <td><s:text style="width:710px" maxlength="64" id="name_infant_3" name="bayiNama3" /></td>                                                    
                                                </tr>
                                            </c:if>
                                            <c:if test="${actionBean.infant > 3}">
                                                <tr>
                                                    <td align="right">4.</td>
                                                    <td><s:text style="width:710px" maxlength="64" id="name_infant_4" name="bayiNama4" /></td>                                                    
                                                </tr>
                                            </c:if>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                        </c:if>
                        <!--
                        <tr>
                            <td align="center" colspan="3">
                                <script type="text/javascript" src="https://www.google.com/recaptcha/api/challenge?k=6LdX-tkSAAAAAN_zdcBZQo2ybyojmNdriI-8Vkqb"></script>
                                <noscript>
                                    <iframe src="https://www.google.com/recaptcha/api/noscript?k=6LdX-tkSAAAAAN_zdcBZQo2ybyojmNdriI-8Vkqb" height="300" width="500" frameborder="0"></iframe><br>
                                    s:textarea name="recaptcha_challenge_field" rows="3" cols="40">/s:textarea>
                                    s:hidden name="recaptcha_response_field" value="manual_challenge"/>
                                </noscript>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>-->	
                        <tr>
                            <td height="30" align="center" colspan="3">
                                <input type="submit" class="button2" value="Kembali" name="home">&nbsp;&nbsp;&nbsp;&nbsp;<input type="submit" class="button2" value="Lanjutkan" name="booking">
                            </td>
                        </tr>
                    </s:form>
                </tbody>
            </table>
        </s:form>
    </section>
</body>
</html>