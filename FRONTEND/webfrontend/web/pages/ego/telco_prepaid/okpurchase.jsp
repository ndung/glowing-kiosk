<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/egolayout.jsp" title="Info Purchase Telco">
    <s:layout-component name="contents">
        <div class="trans-box">
            <div class="trans-content">
                <s:errors />
                <s:messages />	
                <div class="login-title"><em>Pembelian Pulsa Elektrik - Informasi</em></div>
                <s:form beanclass="com.ics.ssk.ego.web.EgoPurchaseTelco" focus="">
                    <s:hidden name="id" />
                    <div class="transbox">
                        <div class="lb-input">
                            <div class="trInput2">Tanggal</div>
                            <fmt:formatDate pattern="dd-MM-yyyy HH:mm:ss" value="${actionBean.message.date}" />
                        </div>                        
                        <div class="lb-input">
                            <div class="trInput2">Product</div>
                            ${actionBean.message.productName}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">Number</div>
                            ${actionBean.message.prefix} - ${actionBean.message.customerId}
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">Denominasi</div>
                            <fmt:formatNumber type="currency" value="${actionBean.message.amount}" pattern="###,###,###.###" />
                        </div>
                        <div class="lb-input">
                            <div class="trInput2">Harga Cetak</div>
                            <fmt:formatNumber type="currency" value="${actionBean.message.price}" pattern="###,###,###.###" />
                        </div>	
                        <div class="lb-input">
                            <div class="trInput2">Voucher</div>
                            ${actionBean.message.voucherNumber1} &nbsp;
                        </div>
                        <div class="statusTRX">
                            Transaksi ${actionBean.message.status} !!
                        </div>							
                        <div class="btnformTR">                            
                            <s:submit name="back"  class="mybutton"/>
                        </div>
                    </div>
                </s:form>
            </div>
        </div>
    </s:layout-component>
</s:layout-render>