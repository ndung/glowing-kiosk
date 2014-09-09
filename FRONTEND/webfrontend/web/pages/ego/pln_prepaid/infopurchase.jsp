<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/egolayout.jsp" title="Info Voucher PLN">
    <s:layout-component name="contents">
        <div class="trans-box">
            <div class="trans-content">
                <s:errors />
                <s:messages />	
                <div class="login-title"><em>PEMBELIAN TOKEN PLN - KONFIRMASI</em></div>
                <s:form beanclass="com.ics.ssk.ego.web.EgoPurchasePln">
                    <s:hidden name="id" />
                    <div class="transbox">                        
                        <div class="lb-input">
                            <div class="trInput2">
                                NO METER
                            </div>
                            ${actionBean.message.additional1}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">
                                IDPEL
                            </div>
                            ${actionBean.message.additional3}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">NAMA</div>
                            ${actionBean.message.suffix}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">TARIF/DAYA</div>
                            ${actionBean.message.additional2}VA
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">RP BAYAR</div>
                            RP. <fmt:formatNumber type="currency" value="${actionBean.message.price}" pattern="###,###,###.###" />
                        </div>							
                        <c:if test="${actionBean.message.price > actionBean.message.currentCashNote}">
                            <div class="btnformTR">
                                <s:button name="cashPayment" class="mybutton" onclick="TINY.box.show({iframe:'egocashpayment.html?id=${actionBean.id}',post:'id=16',width:1000,height:450})">Bayar Cash</s:button>
                                <s:submit name="back" class="mybutton"/>
                            </div>
                        </c:if>
                        <c:if test="${actionBean.message.price <= actionBean.message.currentCashNote}">
                            <div class="lb-input">
                                <div class="trInput2">Cash</div>
                                <fmt:formatNumber type="currency" value="${actionBean.message.currentCashNote}" pattern="###,###,###.###" />
                            </div>
                            <c:if test="${actionBean.message.price < actionBean.message.currentCashNote}">
                                <div class="lb-input">
                                    <div class="trInput2">Change</div>
                                    <fmt:formatNumber type="currency" value="${actionBean.message.changeCashNote}" pattern="###,###,###.###" />
                                </div>  
                            </c:if>
                            <c:if test="${actionBean.message.changeNotPaid > 0}">
                                Maaf mesin tidak dapat mengeluarkan uang kembalian sebesar 
                                <fmt:formatNumber type="currency" value="${actionBean.message.changeAmount}" pattern="###,###,###.###" />.
                                Sisa uang sebesar <fmt:formatNumber type="currency" value="${actionBean.message.changeNotPaid}" pattern="###,###,###.###" />
                                akan didonasikan.        
                            </c:if>
                            <div class="btnformTR">
                                <s:submit id="buttonSend" name="exePayment" class="mybutton" value="next"/>
                                <s:submit name="cancel" class="mybutton"/>
                            </div>
                        </c:if>
                    </div>
                </s:form>
            </div>
        </div>
    </s:layout-component>
</s:layout-render>