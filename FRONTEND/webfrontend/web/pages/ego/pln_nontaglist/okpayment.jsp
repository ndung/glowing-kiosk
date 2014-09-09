<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/egolayout.jsp" title="Info PLN Non Taglist">
    <s:layout-component name="contents">
        <div class="trans-box">
            <div class="trans-content">
                <s:errors />
                <s:messages />	
                <div class="login-title"><em>PEMBAYARAN PLN NON TAGIHAN - INFORMASI</em></div>
                <s:form beanclass="com.ics.ssk.ego.web.EgoPaymentPlnNontaglis">
                    <s:hidden name="id" />
                    <div class="transbox">                                        
                        <div class="lb-input">
                            <div class="trInput2">NO REGISTRASI</div>
                            ${actionBean.message.customerId}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">NAMA</div>
                            ${actionBean.message.suffix}
                        </div>
                        <c:if test="${actionBean.message.status == 'Test'}">
                            <div class="lb-input">
                                <div class="trInput2">TRANSAKSI</div>
                                ${actionBean.message.additional2}
                            </div>
                            <div class="lb-input">
                                <div class="trInput2">JPA REF</div>
                                ${actionBean.message.vouherNumber1} &nbsp;
                            </div>
                            <c:if test="${actionBean.message.additional3 != ''}">
                                <div class="lb-input">
                                    <div class="trInput2">IDPEL</div>
                                    ${actionBean.message.additional3}
                                </div>
                            </c:if>					
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
                                TRANSAKSI SUKSES !!
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
                    </div>
                </s:form>
            </div>
        </div>        
    </s:layout-component>
</s:layout-render>