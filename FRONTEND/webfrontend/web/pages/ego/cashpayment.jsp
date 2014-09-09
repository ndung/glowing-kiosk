<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/popuplayout.jsp" title="Cash Payment">
    <s:layout-component name="contents">
        <div class="trans-box">
            <div class="trans-content">
                <s:errors />
                <s:messages />	
                <div class="login-title"><em>Pembayaran Dengan Uang Cash</em></div>                
                <s:form beanclass="com.ics.ssk.ego.web.EgoCashPayment" name="myform">
                    <s:hidden name="id" />
                    <c:if test="${actionBean.sspRun == true}">
                        Masukkan uang Anda satu per satu ke dalam mesin.
                        <div class="transbox">                        
                            <div class="lb-input">
                                <div class="trInput2">Jumlah Pembayaran</div>
                                <fmt:formatNumber type="currency" value="${actionBean.message.price}" pattern="###,###,###.###" />
                            </div>          
                            <br/>
                            <div class="lb-input">
                                <div class="trInput2">Jumlah Uang Masuk</div>
                                <fmt:formatNumber type="currency" value="${actionBean.message.currentCashNote}" pattern="###,###,###.###" />
                            </div>                        
                            <br/>
                            <div class="btnformTR">                             
                                <s:button id="buttonCancel" name="cancel" class="mybutton"/>                            
                            </div>
                        </div>
                    </c:if>      
                    <c:if test="${actionBean.sspRun == false}">
                        Maaf mesin penerima uang sementara rusak. Silahkan hubungi call centre.
                        <div class="btnformTR">                             
                            <s:button onclick="javascript:parent.TINY.box.hide();" name="cancel" class="mybutton"/>
                        </div>
                    </c:if>          
                </s:form>
            </div>
        </div>
    </s:layout-component>
</s:layout-render>
