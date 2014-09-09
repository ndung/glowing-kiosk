<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/egolayout.jsp" title="Info Payment Telco">
    <s:layout-component name="contents">
        <div class="trans-box">
            <div class="trans-content">
                <s:errors />
                <s:messages />	
                <div class="login-title"><em>Pembayaran Telco - Informasi</em></div>
                <s:form beanclass="com.ics.ssk.ego.web.EgoPaymentTelco">
                    <s:hidden name="id" />
                    <div class="transbox">
                        <div class="lb-input">
                            <div class="trInput2">NOMOR TELEPON</div>
                            ${actionBean.message.prefix} - ${actionBean.message.customerId}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">NAMA</div>
                            ${actionBean.message.additional1}
                        </div>                        							
                        <div class="statusTRX">
                            <c:if test="${actionBean.message.status == 'Sukses'}">
                                TRANSAKSI SUKSES !!!
                            </c:if>
                            <c:if test="${actionBean.message.status != 'Sukses'}">
                                ${actionBean.message.pesan}
                            </c:if>			  						  			
                        </div>							
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