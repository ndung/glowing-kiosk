<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/egolayout.jsp" title="Payment PDAM">
    <s:layout-component name="contents">
        <div class="trans-box">
            <div class="trans-content">
                <s:errors />
                <s:messages />	
                <div class="login-title"><em>PEMBAYARAN TAGIHAN PDAM</em></div>
                <s:form beanclass="com.ics.ssk.ego.web.EgoPaymentPdam">
                    <s:hidden name="productId" />
                    <div class="transbox">                        
                        <div class="lb-input">
                            <div class="trInput3">NO REKENING</div>						
                            <s:text id="numeric" name="customerId" />
                        </div>
                   </div>
                    <div class="btnformTR">
                        <s:submit id="buttonSend" name="inquiry" value="next" class="mybutton"/>
                        <s:submit name="back" value="back" class="mybutton" />
                    </div>   
                </s:form>
            </div>
        </div>
    </s:layout-component>
</s:layout-render>