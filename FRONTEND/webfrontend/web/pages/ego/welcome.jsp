<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/egolayout.jsp" title="Welcome">
    <s:layout-component name="contents">        
        <div class="trans-box">
            <div class="trans-content">
                <s:messages/>
                <div class="login-title"><em>Self Service Kiosk EGO</em></div>
                <div class="transmenu">
                    <c:forEach var="menu" items="${actionBean.list}">
                        <a href="${menu.link}" class="TRbutton">${menu.title}</a>
                    </c:forEach>
                    <c:if test="${actionBean.back != null}">
                        <a href="?group=${actionBean.back}" class="TRbutton">Back</a>
                    </c:if>
                </div>
            </div>
        </div>
    </s:layout-component>
</s:layout-render>