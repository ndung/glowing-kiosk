<%@ include file="/layout/taglibs.jsp" %>

<s:layout-definition>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
    <html>
        <head>
            <title>SSK EGO - ${title}</title>
            <link REL="SHORTCUT ICON" HREF="img/ireload.ico" />

            <link rel="stylesheet" href="css/screen.css" type="text/css" media="screen, projection"/>
            <link rel="stylesheet" href="css/custom.css" type="text/css" media="screen, projection"/>
            <link rel="stylesheet" href="css/print.css" type="text/css" media="print"/>
            <link rel="stylesheet" href="css/datePicker.css" type="text/css" media="screen"/>

            <script type="text/javascript" src="js/tinybox.js"></script>

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

            <script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
            <script type="text/javascript" src="js/slidedown-menu2.js"></script>
            <script type="text/javascript" src="js/jquery.datePicker.js"></script>
            <script type="text/javascript" src="js/date.js"></script>
            <script type="text/javascript" src="js/jquery.simplemodal.js"></script>
            <script src="js/carousel/jcarousellite_1.0.1.js" type="text/javascript"></script>

            <script type="text/javaScript">                
                function timedRefresh(timeoutPeriod) {
                    if (${actionBean.message.price}<=${actionBean.message.currentCashNote}){
                        parent.location.href="egopostingtrx.html?id=${actionBean.message.stan}";
                        parent.TINY.box.hide();
                    }else{
                        setTimeout("location.reload(true);",timeoutPeriod);
                    }
                }
            </script>

            <script type="text/javascript">
                $(document).ready(function(){
                    $('#buttonCancel').click(function() {                        
                        parent.location.href="egocashcancel.html?id=${actionBean.message.stan}";
                        parent.TINY.box.hide();
                    });
                });
            </script>

            <script type="text/javascript">
                $(function() {
                    $(".showticker").jCarouselLite({
                        vertical: true,
                        hoverPause:true,
                        visible: 3,
                        auto:5000,
                        speed:300,
                        scroll: 1
                    });
                });
            </script>

            <script type="text/javascript">
                var submitting = false; 

                function checkSubmit() { 
                    if(submitting) { 
                        alert('Transaksi sedang diproses silahkan tunggu...'); 
                        return false; 
                    } else { 
                        submitting = true; 
                        return true; 
                    } 
                }
            </script>

            <!-- Tickers -->

        </head>
        <body onload="JavaScript:timedRefresh(1000);">                      
            <div class="container">

                <div class="clear span-24 last" id="box-operation">
                    <s:layout-component name="contents" />                                  
                </div>
                <!--		  
          <div class="clear span-24 last" id="logos">
            <div class="logo"><img src="img/operators.png" alt="" /><br /><img src="img/payment.png" alt="" /></div>
          </div>-->
                <div class="clear span-24 last" id="footer">                                
                </div>
            </div>

        </body>
    </html>
</s:layout-definition>