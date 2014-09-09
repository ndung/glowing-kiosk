<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/egolayout.jsp" title="Complaint">
    <s:layout-component name="contents">
        <div class="trans-box">
            <div class="trans-content">
                <s:errors />	
                <div class="login-title"><em>Form Complaint</em></div>
                <s:form beanclass="com.hpal.web.ego.EgoComplaint" focus="">
                    <div class="transbox">
                        <div class="lb-input">
                            <div class="trInput3">Nama</div>						
                            <s:text name="customerName" class="inputs" />
                        </div>
                        <div class="lb-input">
                            <div class="trInput3">Email</div>						
                            <s:text name="customerEmail" class="inputs" />
                        </div>
                        <div class="lb-input">
                            <div class="trInput3">No.Telp</div>						
                            <s:text name="customerTelp" class="inputs" />
                        </div>
                        <div class="lb-input">
                            <div class="trInput3">Pertanyaan</div><s:textarea name="description" cols="10" class="inputs"></s:textarea>
                            </div>					
                            <script type="text/javascript"
                                    src="http://www.google.com/recaptcha/api/challenge?k=6LdX-tkSAAAAAN_zdcBZQo2ybyojmNdriI-8Vkqb">
                            </script>
                            <noscript>
                            <iframe src="http://www.google.com/recaptcha/api/noscript?k=6LdX-tkSAAAAAN_zdcBZQo2ybyojmNdriI-8Vkqb"
                                    height="300" width="500" frameborder="0"></iframe><br>
                        <s:textarea name="recaptcha_challenge_field" rows="3" cols="40"></s:textarea>
                        <s:hidden name="recaptcha_response_field" value="manual_challenge"/>
                        </noscript>
                        <div class="btnformTR">
                            <s:submit name="simpan" class="mybutton" onclick="return checkSubmit();" />
                            <s:submit name="back" class="mybutton"/>
                        </div>
                    </div>
                </s:form>
            </div>
        </div>
    </s:layout-component>
</s:layout-render>