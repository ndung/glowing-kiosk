<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/egolayout.jsp" title="Info Payment KAI">
    <s:layout-component name="contents">
        <div class="trans-box">
            <div class="trans-content">
                <s:errors />
                <s:messages />	
                <div class="login-title"><em>PEMBAYARAN KAI - KONFIRMASI</em></div>
                <s:form beanclass="com.ics.ssk.ego.web.KAIPayment" focus="">
                    <s:hidden name="id" />
                    <div class="transbox">
                        <div class="lb-input">
                            <div class="trInput2">TANGGAL</div>
                            <fmt:formatDate pattern="dd-MM-yyyy HH:mm:ss" value="${actionBean.message.date}" />
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">RESELLER</div>
                            ${actionBean.message.resellerId}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">KODE BOOKING</div>
                            ${actionBean.message.prefix}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">NAMA KERETA</div>
                            ${actionBean.message.additional1}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">RUTE</div>
                            ${actionBean.message.additional2}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">BERANGKAT</div>
                            ${actionBean.message.additional4}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">TIBA</div>
                            ${actionBean.message.additional5}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">NAMA PEMESAN</div>
                            ${actionBean.message.additional3}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">PENUMPANG</div>
                            ${actionBean.message.jumlahTagihan}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">KERETA</div>
                            ${actionBean.message.vouherNumber1}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">NO KURSI</div>
                            ${actionBean.message.additional6}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">RP BAYAR</div>
                            <fmt:formatNumber type="currency" value="${actionBean.message.amount}" pattern="###,###,###.###" />
                        </div>	
                        <div class="lb-input">
                            <div class="trInput2">RP ADMIN</div>
                            <fmt:formatNumber type="currency" value="${actionBean.message.fee}" pattern="###,###,###.###" />
                        </div>	
                        <div class="lb-input">
                            <div class="trInput2">RP TOTAL BAYAR</div>
                            <fmt:formatNumber type="currency" value="${actionBean.message.price}" pattern="###,###,###.###" />
                        </div>							
                        <div class="btnformTR">
                            <s:submit name="goToPin" value="next" class="mybutton" onclick="return checkSubmit();"/>
                            <s:submit name="cancel" class="mybutton" onclick="return checkSubmit();"/>
                        </div>
                    </div>
                </s:form>
            </div>
        </div>
    </s:layout-component>
</s:layout-render>