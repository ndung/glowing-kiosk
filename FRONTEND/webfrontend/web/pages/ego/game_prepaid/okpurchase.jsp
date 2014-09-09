<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/egolayout.jsp" title="PEMBELIAN VOUCHER GAME">
    <s:layout-component name="contents">
        <div class="trans-box">
            <div class="trans-content">
                <s:errors />
                <s:messages />	
                <div class="login-title"><em>PEMBELIAN VOUCHER GAME - INFORMASI</em></div>
                <s:form beanclass="com.ics.ssk.ego.web.EgoPurchaseGame" focus="">
                    <s:hidden name="id" />
                    <div class="transbox">                        
                        <div class="lb-input">
                            <div class="trInput2">NOMOR TELEPON</div>
                            ${actionBean.message.customerId}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">NAMA GAME</div>
                            ${actionBean.message.additional1}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">QUANTITY</div>
                            <fmt:formatNumber type="currency" value="${actionBean.message.total}" pattern="###,###,#00" />
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">RP SUBTOTAL</div>
                            <fmt:formatNumber type="currency" value="${actionBean.message.amount}" pattern="###,###,###.###" />
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">RP ADMIN FEE</div>
                            <fmt:formatNumber type="currency" value="${actionBean.message.fee}" pattern="###,###,###.###" />
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">RP TOTAL</div>
                            <fmt:formatNumber type="currency" value="${actionBean.message.price}" pattern="###,###,###.###" />
                        </div>
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
                            <s:submit name="back"  class="mybutton" />
                        </div>
                    </div>
                </s:form>
            </div>
        </div>
    </s:layout-component>
</s:layout-render>