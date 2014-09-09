<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/egolayout.jsp" title="Cash Payment">
    <s:layout-component name="contents">
        <div class="trans-box">
            <div class="trans-content">
                <s:errors />
                <s:messages />	
                <div class="login-title"><em>Pembatalan Transaksi</em></div>                
                <s:form beanclass="com.ics.ssk.ego.web.EgoCashCancel" name="myform">
                    Silahkan ambil kembali uang Anda satu per satu dari mesin
                    <s:hidden name="id" />
                    <div class="transbox">                                                
                        <br/>
                        <div class="btnformTR">                             
                            <s:submit name="back" class="mybutton"/>
                        </div>
                    </div>				
                </s:form>
            </div>
        </div>
    </s:layout-component>
</s:layout-render>
