<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/mainlayout.jsp" title="Form Register">
	<s:layout-component name="contents">
		<div class="trans-box">
			<div class="trans-content">
				<s:errors />
				<s:messages />	
				<div class="login-title"><em>Daftar Reseller</em></div>
				<s:form beanclass="com.hpal.web.security.RegisterReseller">
				<div class="transbox">
					<div class="lb-input">
						<div class="trInput">Nama</div>
						<s:text name="reseller.nama" class="inputs" title="Masukkan nama lengkap"/>
					</div>
					<div class="lb-input">
						<div class="trInput">Alamat</div>
						<s:textarea name="reseller.alamat" class="inputs" />
					</div>
					<div class="lb-input">
						<div class="trInput">Kota</div>
						<s:text name="reseller.lokasi" class="inputs" />
					</div>
					<div class="lb-input">
						<div class="trInput">Kode Pos</div>
						<s:text name="reseller.kodeAreaId" class="inputs" />
					</div>	
					<div class="lb-input">
						<div class="trInput">Email</div>
						<s:text name="reseller.email" class="inputs" title="Masukkan Email secara lengkap" />
					</div>
					<div class="lb-input">
						<div class="trInput">Tanggal Lahir</div>
						<s:text name="reseller.date" class="date-pick inputs" readonly="readonly" />
					</div>	
					<div class="lb-input">
						<div class="trInput">Handphone</div>
						<s:text name="reseller.channelHp" class="inputs" title="No HP dalam format 0xxx. Contoh : 08562552423"/>
					</div>					
					<div class="btnformTR">
						<s:submit name="register" value="Daftar" class="mybutton"/>
			    		<s:submit name="login"    class="mybutton"/>
					</div>
				</div>
				</s:form>
			</div>
		</div>
	</s:layout-component>
</s:layout-render>