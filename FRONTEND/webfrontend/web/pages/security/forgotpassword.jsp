<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/mainlayout.jsp" title="Forgot Password">
	<s:layout-component name="contents">
		<div class="trans-box">
			<div class="trans-content">
				<s:errors />	
				<div class="login-title"><em>Lupa ID dan Password</em></div>
				<s:form beanclass="com.hpal.web.security.ForgotPassword" focus="">
				<div class="transbox">
					<div class="lb-input">
						<div class="trInput3">Handphone</div><s:text name="nomor" class="inputs" title="Masukkan nomor handphone anda yang terdaftar di system kami" />
					</div>
					<div class="lb-input">
						<div class="trInput3">Tanggal Lahir</div><s:text name="date" class="date-pick inputs" readonly="readonly" />
					</div>
					<div class="btnformTR">
						<s:submit name="kirim" value="Kirim" class="mybutton"/>
			    		<s:submit name="batal" value="Batal" class="mybutton"/>
					</div>
				</div>
				</s:form>
			</div>
		</div>
	</s:layout-component>
</s:layout-render>