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
            <link href="css/tinybox.css" rel="stylesheet"/>

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
            <script src="js/carousel/jcarousellite_1.0.1.js" type="text/javascript"></script>

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

            <script type="text/javascript">
                $(document).ready(function(){
                    var biggestHeight = 0;
                    $('.equal_height').each(function(){
                        if($(this).height() > biggestHeight){
                            biggestHeight = $(this).height();
                        }
                    });
                    $('.equal_height').height(biggestHeight);

                    Date.firstDayOfWeek = 0;
                    Date.format = 'yyyy/mm/dd';
                    $('.date-pick').datePicker({startDate:'01/01/1945'});
                });
            </script>                

            <script type="text/javascript">
                $(document).ready(function(){
                    $('#buttonSend').click(function() {
                        $('#spinner').show();
                    });
                });
            </script>

        </head>
        <body>
            <div class="topbar"></div>                                                

            <div class="container">
                <div class="span-24 last" id="header">
                    <div class="tickers">
                        <div class="showticker">
                            <ul>
                                ${actionBean.sessionIklan}
                            </ul>
                        </div>
                    </div>
                </div>

                <div class="span-24 last" id="logoreload">
                    <img src="img/logoEGO.png" alt="" height="83" width="224"/>
                </div>

                <div class="clear span-24 last" id="box-operation">
                    <s:layout-component name="contents" />  
                    <div class="shadow"><img src="img/shadow.png" alt="" /></div>  
                </div>
                <!--		  
                <div class="clear span-24 last" id="logos">
                  <div class="logo"><img src="img/operators.png" alt="" /><br /><img src="img/payment.png" alt="" /></div>
                </div>-->
                <div id="spinner" class="spinner" style="display:none;">
                    <img id="img-spinner" src="img/ajaxLoader.gif" alt="Loading"/>
                </div>

                <div class="clear span-24 last" id="footer">
                    Copyright © 2013 SSK EGO ver. 1.0<br />ICS Developer Team. PT INDO CIPTA GUNA. All Right Reserved.
                </div>
            </div>

        </body>
    </html>
</s:layout-definition>