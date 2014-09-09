<%@ include file="/pages/taglibs.jsp" %>

<s:layout-render name="/layout/mainlayout.jsp" title="Qu3ry">
	<s:layout-component name="contents">
		<div class='title'>Query</div>
		<hr width='350px' align='left'>
		<s:errors />
		<s:form beanclass="com.hpal.web.security.CounterNew" focus="">
			<table class='form'>
				<tr>
					<td><s:label for="date" /></td>
					<td><s:text name="date" class="date-pick dp-applied" readonly="readonly" onchange="this.form.submit()"/></td>
				</tr>
			</table>
		</s:form>
		<table class='form'>
			<tr>
				<td>T</td><td>=</td><td>${actionBean.telkomsel}</td>
			</tr>
			<tr>
				<td>NT</td><td>=</td><td>${actionBean.nonTelkomsel}</td>
			</tr>
		</table>
	</s:layout-component>
</s:layout-render>