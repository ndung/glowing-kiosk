<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/mainlayout.jsp" title="Login">
	<s:layout-component name="contents">
		<div class='title'>Login</div>
		<hr width='350px' align='left'>
		<s:errors />
		<s:messages />
		<s:form beanclass="com.hpal.web.security.LoginUser" focus="">
			<table class='form'>
				<tr>
					<td><s:label for="username" /></td>
					<td><s:text name="username" size="25" /></td>
				</tr>
				<tr>
					<td><s:label for="password" /></td>
					<td><s:password name="password" size="25" /></td>
				</tr>
				<tr>
  					<td><s:label for="position" /></td>
  					<td>
  						<s:select name="position">
							<s:options-collection collection="${actionBean.positions}" value="value" label="description"/>
						</s:select>
  					</td>
  				</tr>	
				<tr>
					<td></td>
					<td><s:submit name="login" />
						<s:submit name="register" /></td>
				</tr>
			</table>
		</s:form>
	</s:layout-component>
</s:layout-render>