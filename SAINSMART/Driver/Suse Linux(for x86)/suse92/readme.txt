Release Note for Touchkit driver for Linux:
This driver support constant touch and auto right click features.
By default, both of these features were Enabled. To Disable it, pls
create a file "/etc/tpaneld.ini" and change the attribute inside this ini file.
for example.

AutorightClick = Enabled
AutoRightClickTime = 500
ConstTouchArea = 50
XEventDelay = 200
ConstTouch = Enabled
