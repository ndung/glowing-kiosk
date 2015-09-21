#!/bin/bash

xset -dpms
xset s off
openbox-session &

while true; do
    sudo java -Dcom.sun.javafx.virtualKeyboard=javafx -Dcom.sun.javafx.touch=true -jar /home/ndung/kiosk/FrontEnd/kioskfx.jar
done
