<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/mainlayout.jsp" title="Login">
	<s:layout-component name="contents">
		<s:form beanclass="com.hpal.web.security.Login" focus="">
		<div class="login-box">
		<div class="lb-left">
			Anda ingin melakukan transaksi tanpa registrasi? Silahkan klik <a href="ego.html">EGO!</a></div></div>			
			<div class="login-box">
				<div class="lb-left equal_height">
			    	<div class="lb-content">
					<s:errors />
					<s:messages />
			        	<div class="login-title"><em>Halaman Login</em></div>
			          	<div class="lb-input">
				            <div class="txtInput">ID</div>
				            <s:text name="username" title="Masukkan ID anda sesuai dengan ID yang anda miliki" class="inputs" />
				            <div class="txtInput">PIN</div>
				            <s:password name="password" title="Masukkan PIN anda sesuai dengan PIN yang anda miliki" class="inputs" />

						</div>
					</div>
				</div>
				<div class="lb-right equal_height">
					<div class="lb-buttons">
			    		<s:submit name="login" class="mybutton"/>
			    		<s:submit name="register" class="mybutton"/>
			    		<s:submit name="lupa" value="Lupa ??" class="mybutton"/>
			    	</div>
				</div>
				<br class="clear" />
			</div>
		</s:form>
	</s:layout-component>
</s:layout-render>