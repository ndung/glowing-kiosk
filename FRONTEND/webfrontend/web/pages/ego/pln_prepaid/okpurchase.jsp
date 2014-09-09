<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/egolayout.jsp" title="Info Purchase Electric">
    <s:layout-component name="contents">
        <div class="trans-box">
            <div class="trans-content">
                <s:errors />
                <s:messages />	
                <div class="login-title"><em>PEMBELIAN TOKEN PLN - INFORMASI</em></div>
                <s:form beanclass="com.ics.ssk.ego.web.EgoPurchasePln" focus="" name="myform">
                    <s:hidden name="id" />
                    <div class="transbox">
                        <div class="lb-input">
                            <div class="trInput2">TANGGAL</div>
                            <fmt:formatDate pattern="dd-MM-yyyy HH:mm:ss" value="${actionBean.message.date}" />
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">NO METER</div>
                            ${actionBean.message.additional1}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">IDPEL</div>
                            ${actionBean.message.additional3}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">NAMA</div>
                            ${actionBean.message.suffix}
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
                            <c:if test="${actionBean.message.status == 'Sukses'}">
                                <s:submit name="cetaku" id="cetak" class="mybutton"/>
                            </c:if>
                            <c:if test="${actionBean.message.status == 'Suspect'}">
                                <s:submit name="advice" class="mybutton"/>
                            </c:if>
                            <s:submit name="back"  class="mybutton"/>
                        </div>
                    </div>
                </s:form>
            </div>
        </div>
    </s:layout-component>
</s:layout-render>