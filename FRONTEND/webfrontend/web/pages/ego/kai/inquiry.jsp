<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">

<%@ include file="/pages/taglibs.jsp" %>
<html>
    <head>
        <title>Ticketing Kereta API</title>
        <link REL="SHORTCUT ICON" HREF="img/ireload.ico" >
        <link href="css/kereta.css" rel="stylesheet" type="text/css">
        <link href="css/datePicker.css" rel="stylesheet" type="text/css">        
        <script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
        <script type="text/javascript" src="js/jquery.datePicker.js"></script>
        <script type="text/javascript" src="js/date.js"></script>

        <link href="css/jquery-ui.css" rel="stylesheet"/>
        <script src="js/jquery.js"></script>
        <script src="js/jquery-ui.min.js"></script>

        <link href="css/keyboard.css" rel="stylesheet"/>
        <script src="js/jquery.keyboard.js"></script>

        <script src="js/jquery.mousewheel.js"></script>
        <script src="js/jquery.keyboard.extension-typing.js"></script>
        <script src="js/jquery.keyboard.extension-autocomplete.js"></script>

        <script src="js/demo.js"></script>
        <script src="js/jquery.jatt.min.js"></script>
        <script src="js/jquery.chili-2.2.js"></script>
        <script src="js/recipes.js"></script>
        
        <script type="text/javascript" src="js/slidedown-menu2.js"></script>        
        <script src="js/carousel/jcarousellite_1.0.1.js" type="text/javascript"></script>
    </head>
    <body>
    <section class="area-content">
        <table border="0" width="100%">
            <tr>
                <td align="center">
                    <img src="img/logoEGO.png" alt="" height="40" width=160"/>
                    <h1>Ticketing Kereta API</h1>                    
                </td>
            </tr>
        </table>
        <table width="20%" border="0" align="center">
            <s:form beanclass="com.ics.ssk.ego.web.KAIInquiry">
                <tbody>
                    <tr>
                        <td colspan="3">
                            <s:errors />
                            <s:messages />	
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div class="item-page">
                                <h1>INPUT TANGGAL PERJALANAN KAI</h1>
                            </div>			
                        </td>
                    </tr>
                    <tr>
                        <td>Tanggal</td>
                    </tr>
                    <tr>
                        <td>
                            <s:select name="date" class="ComboInfoka">
                                <s:options-collection collection="${actionBean.dates}" value="value" label="description"/>
                            </s:select>
                        </td>
                    </tr>
                    <tr>
                        <td>Stasiun Asal</td>
                    </tr>
                    <tr>
                        <td>
                            <select class="ComboInfoka" name="asal">
                                <c:forEach items="${actionBean.stations}" var="group" >
                                    <optgroup label="${group.description}">
                                        <c:forEach items="${group.kaiStations}" var="station" >
                                            <c:if test="${station.original == 'Y'}">
                                                <option value="${station.id}#${station.description}" <c:if test="${station.id == actionBean.asal}">selected="selected"</c:if>>${station.description}</option>
                                            </c:if> 
                                        </c:forEach>
                                    </optgroup>
                                </c:forEach>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>Stasiun Tujuan</td>
                    </tr>
                    <tr>
                        <td>
                            <select class="ComboInfoka" name="tujuan">
                                <c:forEach items="${actionBean.stations}" var="group" >
                                    <optgroup label="${group.description}">
                                        <c:forEach items="${group.kaiStations}" var="station" >
                                            <c:if test="${station.destination == 'Y'}">
                                                <option value="${station.id}#${station.description}" <c:if test="${station.id == actionBean.tujuan}">selected="selected"</c:if>>${station.description}</option>
                                            </c:if> 
                                        </c:forEach>
                                    </optgroup>
                                </c:forEach>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" border="0" width="90%" align="center">
                                <tbody>
                                    <tr>
                                        <td width="30%" align="center">Dewasa</td>
                                        <td width="30%" align="center">Anak</td>
                                        <td width="30%" align="center">Infant</td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <s:text id="dewasaInput" style="width:50px" value="1" name="dewasa" />
                                            <!--
                                            s:select name="dewasa"  class="ComboInfokaA">
                                                s:options-collection collection="{actionBean.jumlahs}" value="value" label="description"/>
                                            /s:select>-->
                                        </td>
                                        <td align="center">
                                            <s:text id="anakInput" style="width:50px" value="0" name="anak" />
                                            <!--s:select name="anak"  class="ComboInfokaA">
                                                s:options-collection collection="{actionBean.jumlahs}" value="value" label="description"/>
                                            /s:select-->
                                        </td>
                                        <td align="center">
                                            <s:text id="bayiInput" style="width:50px" value="0" name="infant" />
                                            <!--s:select name="infant"  class="ComboInfokaA">
                                                s:options-collection collection="{actionBean.jumlahs}" value="value" label="description"/>
                                            /s:select>-->
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">

                        </td>
                    </tr>
                    <tr>		
                        <td align="center">
                            <s:submit name="tampilkan" class="button2" value="Tampilkan"/>
                            <s:submit name="kembali" class="button2" value="Kembali"/>
                        </td>
                    </tr>
                </tbody>
            </s:form>
        </table>
    </section>
</body>
</html>