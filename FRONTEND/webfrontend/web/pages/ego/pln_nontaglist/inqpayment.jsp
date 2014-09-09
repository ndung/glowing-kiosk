<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/egolayout.jsp" title="PLN Non Taglist">
    <s:layout-component name="contents">
        <div class="trans-box">
            <div class="trans-content">
                <s:errors />
                <s:messages />	
                <div class="login-title"><em>Pembayaran PLN NonTaglist</em></div>
                <s:form beanclass="com.ics.ssk.ego.web.EgoPaymentPlnNontaglis">
                    <div class="transbox">
                        <div class="lb-input">
                            <div class="trInput3">No Registrasi</div>
                            <s:text id="numeric" name="customerId"/>
                        </div>
                        <div class="btnformTR">
                            <s:submit id="buttonSend" name="inquiry" value="next" class="mybutton"/>
                            <s:submit name="back" class="mybutton"/>
                        </div>
                    </div>
                </s:form>
            </div>
        </div>
    </s:layout-component>
</s:layout-render>