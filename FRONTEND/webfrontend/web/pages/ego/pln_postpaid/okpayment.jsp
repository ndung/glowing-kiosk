<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/egolayout.jsp" title="Info Payment PLN">
    <s:layout-component name="contents">
        <div class="trans-box">
            <div class="trans-content">
                <s:errors />
                <s:messages />	
                <div class="login-title"><em>PEMBAYARAN PLN - INFORMASI</em></div>
                <s:form beanclass="com.ics.ssk.ego.web.EgoPaymentPlnPostpaid">         
                    <s:hidden name="id" />
                    <div class="lb-input">
                        <div class="trInput2">IDPEL</div>
                        ${actionBean.message.customerId}
                    </div>
                    <div class="lb-input">
                        <div class="trInput2">NAMA</div>
                        ${actionBean.message.suffix}
                    </div>
                    <c:if test="${actionBean.message.status == 'Test'}">
                        <div class="lb-input">
                            <div class="trInput2">BL/TH</div>
                            ${actionBean.message.tahunBulanTag2} BULAN
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">TOTAL LBR TAGIHAN</div>
                            ${actionBean.message.additional6}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">JPA REF</div>
                            ${actionBean.message.vouherNumber1} &nbsp;
                        </div>	
                        <div class="lb-input">
                            <div class="trInput2">RP TAG PLN</div>
                            <fmt:formatNumber type="currency" value="${actionBean.message.amount}" pattern="###,###,###.###" />
                        </div>	
                        <div class="lb-input">
                            <div class="trInput2">ADMIN BANK</div>
                            <fmt:formatNumber type="currency" value="${actionBean.message.fee}" pattern="###,###,###.###" />
                        </div>	
                        <div class="lb-input">
                            <div class="trInput2">TOTAL BAYAR</div>
                            <fmt:formatNumber type="currency" value="${actionBean.message.price}" pattern="###,###,###.###" />
                        </div>
                    </c:if>			
                    <c:if test="${actionBean.message.status == 'Sukses'}">
                        <div class="statusTRX">
                            TRANSAKSI SUKSES !!!
                        </div>		
                    </c:if>                          
                    <c:if test="${actionBean.message.status != 'Sukses'}">
                        <div class="statusTRX">
                            ${actionBean.message.pesan}
                        </div>		
                    </c:if>       						
                    <div class="btnformTR">
                        <c:if test="${actionBean.message.status == 'Sukses'}">
                            <s:submit name="cetaku" id="cetaku" class="mybutton"/>
                        </c:if>
                        <s:submit name="back"  class="mybutton"/>
                    </div>
                </s:form>
            </div>
        </div>        
    </s:layout-component>
</s:layout-render>