<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/egolayout.jsp" title="Voucher PLN">
    <s:layout-component name="contents">
        <div class="trans-box">
            <div class="trans-content">
                <s:errors />
                <s:messages />	
                <div class="login-title"><em>Pembelian Voucher PLN</em></div>
                <s:form beanclass="com.ics.ssk.ego.web.EgoPurchasePln">
                    <s:hidden name="amount" />
                    <div class="transbox">
                        <!--
                        <div class="lb-input">
                            <div class="trInput3">Tipe</div>
                            s:select name="suffix" class="inputs">
                                s:options-collection collection="${actionBean.suffixs}" value="value" label="description"/>
                            /s:select>
                        </div>-->
                        <div class="lb-input">
                            <div class="trInput3">IDPEL/METER ID</div>
                            <s:text id="numeric" name="customerId"/>
                        </div>
                        <!--
                        <div class="lb-input">
                            <div class="trInput3">NILAI</div>
                            s:text id="numeric" name="amount"/>
                        </div>-->
                        <div class="btnformTR">
                            <s:submit id="buttonSend" name="inquiry" value="next" class="mybutton"/>
                            <s:submit name="back" value="back" class="mybutton" />                            
                        </div>
                    </div>
                </s:form>
            </div>
        </div>
    </s:layout-component>
</s:layout-render>