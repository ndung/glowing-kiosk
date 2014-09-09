<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/mainlayout.jsp" title="Change PIN">
	<s:layout-component name="contents">
		<div class="trans-box">
			<div class="trans-content">
				<s:errors />	
				<div class="login-title"><em>Ubah PIN</em></div>
				<s:form beanclass="com.hpal.web.security.ChangePassword" focus="">
				<div class="transbox">
					<s:hidden name="reseller" />
					<div class="lb-input">
						<div class="trInput3">PIN Lama</div><s:password name="password" class="inputs" size="30" />
					</div>
					<div class="lb-input">
						<div class="trInput3">PIN Baru</div><s:password name="newpin" class="inputs" size="30" />
					</div>
					<div class="lb-input">
						<div class="trInput3">Re-PIN</div><s:password name="repassword" class="inputs" size="30" />
					</div>
					<div class="btnformTR">
						<s:submit name="save" class="mybutton"/>
			    		<s:submit name="back" class="mybutton"/>
					</div>
				</div>
				</s:form>
			</div>
		</div>
	</s:layout-component>
</s:layout-render>