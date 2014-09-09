#!/bin/sh
#
# This is a driver installer for eGalax Touch controller.
#
# Company: eGalax_eMPIA Technology Inc. (EETI)
# Version: 1.04.1705
# Released: 2009/05/05
# Support: touch_fae@eeti.com
#

version="1.04.1705"
driver="eGalaxTouch.tar.gz"
utility="eGalaxTouch"
caltool="TKCal"
installpath="/usr/local"
shortcutpath="/usr/bin"
xfilepath="/etc/X11/xorg.conf"
xfilepath2="/etc/X11/XF86Config-4"
xfilepath3="/etc/X11/XF86Config"
xmodulespath="/usr/X11R6/lib/modules/input"
xmodulespath2="/usr/lib/xorg/modules/input"
blacklistpath="/etc/modprobe.d/blacklist"
blacklistpath2="/etc/modprobe.d/blacklist-compat" # for Mandriva 2008
blacklistpath3="/etc/hotplug/blacklist"
rclocalpath="/etc/rc.d/rc.local"
identifier="EETI"
paramfile="/var/lib/eeti.param"
trash="/dev/null"
libpath="/usr/lib/libXfixes.so.3"
libpath2="/usr/X11R6/lib/libXfixes.so.3"

ShowTitle() {
    echo ""
    echo "(*) Linux driver installer for eGalax Touch controller "
    echo ""
}

CheckPermission() {
    echo -n "(I) Check user permission:"
    account=`whoami`
    if [ ${account} = "root" ]; then
        echo " ${account}, you are the supervisor."
    else
        echo " ${account}, you are NOT the supervisor."
        echo "(E) The root permission is required to run this installer."
        echo ""
        exit 1
    fi
}

CheckOldDaemon() {
    checkprocess="ps ax"
    processfile="process.info"
    ${checkprocess} > ${processfile} 2> ${trash}
    grep -q "tpaneld" ${processfile}
    if [ $? -eq 0 ]; then
        echo "(W) Found the old eGalax Touch daemon: tpaneld."
        echo "(E) It has to be removed before run this installer."
        echo ""
        rm -f ${processfile}
        exit 1
    fi
    grep -q "usbpnpd" ${processfile}
    if [ $? -eq 0 ]; then
        echo "(W) Found the old eGalax Touch daemon: usbpnpd."
        echo "(E) It has to be removed before run this installer."
        echo ""
        rm -f ${processfile}
        exit 1
    fi
    rm -f ${processfile}
}

CheckOldDriver() {
    olddriver="uninstall_TouchKit"
    olddriverpath=`find ${shortcutpath} -name ${olddriver}`
    if [ -z "${olddriverpath}" ]; then
        CheckOldDaemon
        echo "(I) Begin to setup the eGalax Touch driver."
    else
        echo "(W) Found the old eGalax Touch driver."
        echo "(E) It has to be removed before run this installer."
        ${olddriver}
    fi
}

ExtractDriver() {
    if [ -e ${installpath}/${driver} ]; then
        rm -f ${installpath}/${driver}
    fi
    temppath=`find ${installpath} -name ${utility}`
    driverpath=${temppath%/*}
    if [ -n "${driverpath}" ]; then
        rm -rf ${driverpath}
        echo "(I) Found and removed previous eGalax Touch driver."
    fi

    cp -f ${driver} ${installpath}
    ( cd ${installpath}; tar -zxf ${driver}; rm -f ${driver} )
    temppath=`find ${installpath} -name ${utility}`
    driverpath=${temppath%/*}
    echo "(I) Extract eGalax Touch driver archive to ${driverpath}."

    if [ -e ${libpath} ]; then
        rm -rf ${driverpath}/Tool
    elif [ -e ${libpath2} ]; then
        rm -rf ${driverpath}/Tool
    else
        echo "(I) Searching the shared library: libXfixes.so.3."
        sharedpath=`find /usr -name libXfixes.so.3`
        if [ -z "${sharedpath}" ]; then
            echo "(I) No shared library: libXfixes.so.3 found."
            fullpath=`find ${installpath} -name Utility_FC2`
            toolpath=${fullpath%/*}
            cp -f ${toolpath}/Utility_FC2 ${driverpath}/${utility}
            cp -f ${toolpath}/Cal_FC2 ${driverpath}/TKCal/${caltool}
            rm -rf ${toolpath}
            echo "(I) Replaced by another edition of utility."
        else
            rm -rf ${driverpath}/Tool
        fi
    fi

    if [ -d ${shortcutpath} ]; then
        echo "(I) Create ${utility} utility shortcut in ${shortcutpath}."
        chmod a+x ${driverpath}/${utility}
        ln -sf ${driverpath}/${utility} ${shortcutpath}
        echo "(I) Create ${caltool} tool shortcut in ${shortcutpath}."
        chmod a+x ${driverpath}/${caltool}/${caltool}
        ln -sf ${driverpath}/${caltool}/${caltool} ${shortcutpath}
    else
        echo "(W) The shortcut can NOT be created in ${shortcutpath}."
    fi
}

RemoveDriver() {
    if [ -e ${installpath}/${driver} ]; then
        rm -f ${installpath}/${driver}
    fi

    temppath=`find ${installpath} -name ${utility}`
    driverpath=${temppath%/*}
    if [ -z "${driverpath}" ]; then
        echo "(E) The driver archive has been removed already."
        echo ""
        exit 1
    elif [ -n "${driverpath}" ]; then
        rm -rf ${driverpath}
        echo "(I) Removed eGalax Touch driver archive from ${driverpath}."
    fi

    if [ -d ${shortcutpath} ]; then
        if [ -L ${shortcutpath}/${utility} ]; then
            rm -f ${shortcutpath}/${utility}
            echo "(I) Removed ${utility} utility shortcut."
        fi
        if [ -L ${shortcutpath}/${caltool} ]; then
            rm -f ${shortcutpath}/${caltool}
            echo "(I) Removed ${caltool} tool shortcut."
        fi
    fi
}

CheckXorgVersion() {
    checkx="X -version"
    xfile="X.info"
    ${checkx} > ${xfile} 2>&1
    grep -q "X.Org X Server 1.4." ${xfile}
    if [ $? -eq 0 ]; then
        grep -q "X.Org X Server 1.4.99" ${xfile}
        if [ $? -eq 0 ]; then
            echo "(I) Check X window version: 1.4.99.x" # FC9: 1.4.99.901
            xdirectory="x1499"
        else
            echo "(I) Check X window version: 1.4.x" # openSuSE 11 / Ubuntu 8.04: 1.4.0.90
            xdirectory="x14"
        fi
    else
        grep -q "X Window System Version 1.3." ${xfile} # FC8 / Ubuntu 7.10: 1.3.0
        if [ $? -eq 0 ]; then
            echo "(I) Check X window version: 1.3.x"
            xdirectory="x13"
        else
            grep -q "X.Org X Server 1.5." ${xfile} # Ubuntu 8.10
            if [ $? -eq 0 ]; then
                echo "(I) Check X window version: 1.5.x"
                xdirectory="x15"
            else
                grep -q "X.Org X Server 1.6." ${xfile} # Ubuntu 9.04
                if [ $? -eq 0 ]; then
                    echo "(I) Check X window version: 1.6.x"
                    xdirectory="x16"
                else
                    grep -q "X Window System Version 6.8.99.900" ${xfile} # Mandriva 2006
                    if [ $? -eq 0 ]; then
                        echo "(I) Check X window version: 6.8.99.900"
                        xmodule="egalax_drv.o"
                        xdirectory="x67"
                    else
                        echo "(I) Check X window version: 6.9.0 ~ 7.2.0"
                        xdirectory="x69"
                    fi
                fi
            fi
        fi
    fi
    rm -f ${xfile}
}

CopyXorgMudule() {
    if [ -e ${xmodulespath}/mouse_drv.*o ]; then
        mousepath=`find ${xmodulespath} -name mouse_drv.*o`
    elif [ -e ${xmodulespath2}/mouse_drv.*o ]; then
        mousepath=`find ${xmodulespath2} -name mouse_drv.*o`
    else
        echo "(I) Searching the X input modules directory."
        mousepath=`find /usr -name mouse_drv.*o`
        if [ -z "${mousepath}" ]; then
            RemoveDriver
            echo "(E) No X input modules directory found."
            echo ""
            exit 1
        fi
    fi

    mouse=${mousepath##*/}
    if [ ${mouse} = "mouse_drv.so" ]; then
        xmodule="egalax_drv.so"
        CheckXorgVersion
        xpath=${mousepath%/*}
    elif [ ${mouse} = "mouse_drv.o" ]; then
        xmodule="egalax_drv.o"
        xdirectory="x67"
        xpath=${xmodulespath}
        echo "(I) Check X window version: 6.7 ~ 6.8.2"
    else
        RemoveDriver
        echo "(E) Unknown mouse module!"
        echo ""
        exit 1
    fi

    if [ -e ${xpath}/${xmodule} ]; then
        rm -f ${xpath}/${xmodule}
    fi
    echo "(I) Copy X module: ${xdirectory}/${xmodule} to ${xpath}."
    cp -f ${driverpath}/Module/${xdirectory}/${xmodule} ${xpath}
}

RemoveXorgModule() {
    xmodule="egalax_drv.*o"
    if [ -e ${xmodulespath}/${xmodule} ]; then
        rm -f ${xmodulespath}/${xmodule}
    elif [ -e ${xmodulespath2}/${xmodule} ]; then
        rm -f ${xmodulespath2}/${xmodule}
    else
        echo "(I) Searching the X module of touch device."
        fullmodulepath=`find /usr -name ${xmodule}`
        if [ -z "${fullmodulepath}" ]; then
            RemoveDriver
            echo "(E) No X module of touch device found."
            echo ""
            exit 1
        elif [ -e ${fullmodulepath} ]; then
            rm -f ${fullmodulepath}
        fi
    fi
    echo "(I) Removed X module."
}

Add2Blacklist() {
    if [ -w ${blacklistpath} ]; then
        grep -q $1 ${blacklistpath}
        if [ $? -eq 1 ]; then
            filelines=`cat ${blacklistpath} | wc -l`
            echo "(I) Add kernel module $1 into ${blacklistpath}."
            sed -i ''${filelines}'a\# added by eGalax Touch installer\nblacklist '$1'' ${blacklistpath}
        else
            echo "(I) The kernel module $1 has been added in ${blacklistpath}."
        fi
    elif [ -w ${blacklistpath2} ]; then
        grep -q $1 ${blacklistpath2}
        if [ $? -eq 1 ]; then
            filelines=`cat ${blacklistpath2} | wc -l`
            echo "(I) Add kernel module $1 into ${blacklistpath2}."
            sed -i ''${filelines}'a\# added by eGalax Touch installer\nblacklist '$1'' ${blacklistpath2}
        else
            echo "(I) The kernel module $1 has been added in ${blacklistpath2}."
        fi
    elif [ -w ${blacklistpath3} ]; then
        grep -q $1 ${blacklistpath3}
        if [ $? -eq 1 ]; then
            filelines=`cat ${blacklistpath3} | wc -l`
            echo "(I) Add kernel module $1 into ${blacklistpath3}."
            sed -i ''${filelines}'a\# added by eGalax Touch installer\n'$1'' ${blacklistpath3}
        else
            echo "(I) The kernel module $1 has been added in ${blacklistpath3}."
        fi
    else
        echo "(I) No blacklist file found in /etc/modprobe.d or /etc/hotplug."
        if [ -w ${rclocalpath} ]; then
            grep -q $1 ${rclocalpath}
            if [ $? -eq 1 ]; then
                filelines=`cat ${rclocalpath} | wc -l`
                echo "(I) Add kernel module $1 into ${rclocalpath}."
                sed -i ''${filelines}'a\# added by eGalax Touch installer\nrmmod '$1'' ${rclocalpath}
            else
                echo "(I) The kernel module $1 has been added in ${rclocalpath}."
            fi
        else
            echo "(I) Skip to blacklist $1."
        fi
    fi
}

ShowBlacklistMenu() {
    echo "(I) It is highly recommended that add it into blacklist."
    echo -n "(Q) Do you want to add it into blacklist? (y/n) "
    while : ; do
        read yorn
        case $yorn in
            [Yy]) Add2Blacklist $1
               break;;
            [Nn]) # Nothing to do here.
               break;;
            *) echo "(I) Please choose [y] or [n]"
               echo -n "(A) ";;
        esac
    done
}

CheckModuleAndBlacklist() {
    checkmod="lsmod"
    modfile="mod.info"
    ${checkmod} > ${modfile} 2> ${trash}
    grep -q "usbtouchscreen" ${modfile}
    if [ $? -eq 0 ]; then
        echo "(I) Found inbuilt kernel module: usbtouchscreen."
        ShowBlacklistMenu "usbtouchscreen"
    else
        grep -q "touchkitusb" ${modfile}
        if [ $? -eq 0 ]; then
            echo "(I) Found inbuilt kernel module: touchkitusb."
            ShowBlacklistMenu "touchkitusb"
        fi
    fi
    rm -f ${modfile}
}

CheckInbuiltModule() {
    checkmod="lsmod"
    modfile="mod.info"
    ${checkmod} > ${modfile} 2> ${trash}
    grep -q "usbtouchscreen" ${modfile}
    if [ $? -eq 1 ]; then
        grep -q "touchkitusb" ${modfile}
        if [ $? -eq 1 ]; then
            echo "(W) No inbuilt kernel module for touch controller found."
            echo "(I) It is needed to build \"tkusb\" kernel module for touch controller."
            echo "(I) For details, see the document \"How to build module.pdf\"."
        else
            echo "(I) Note that the option \"Device\" \"/dev/input/mice\" for mouse"
            echo "    should be changed to \"Device\" \"/dev/input/mouseX\" to prevent"
            echo "    the mouse driver from reading."
            echo "(I) For details, see the document \"Driver Guide.pdf\"."
        fi
    else
        echo "(I) Note that the option \"Device\" \"/dev/input/mice\" for mouse"
        echo "    should be changed to \"Device\" \"/dev/input/mouseX\" to prevent"
        echo "    the mouse driver from reading."
        echo "(I) For details, see the document \"Driver Guide.pdf\"."
    fi
    rm -f ${modfile}
}

CheckUSBType() {
    usbfile="usb.info"
    checkusb="lsusb -v -d 0eef:0001"
    ${checkusb} > ${usbfile} 2> ${trash}
    grep -q "Human Interface Device" ${usbfile}
    if [ $? -eq 0 ]; then
        echo "(I) Found a HID compliant touch controller."
        CheckModuleAndBlacklist
    else
        grep -q "Vendor Specific Protocol" ${usbfile}
        if [ $? -eq 0 ]; then
            echo "(I) Found a non-HID compliant touch controller."
            CheckInbuiltModule
        else
            echo "(W) Unknown: Skip to check USB type."
            echo "(I) Note that the option \"SkipClick\" \"1\" should be appended in the touch"
            echo "    section of X configuration file for HID compliant touch controller."
            echo "    Besides, if the using touch controller is non-HID compliant controller,"
            echo "    the option \"Device\" \"/dev/input/mice\" for mouse should be changed to"
            echo "    \"Device\" \"/dev/input/mouseX\" to prevent the mouse driver from reading."
            echo "(I) For details, see the document \"Driver Guide.pdf\"."
        fi
    fi
    rm -f ${usbfile}
}

CheckInterface() {
    echo ""
    echo "(Q) Which interface controller do you use?"
    echo -n "(I) [1] RS232 [2] PS/2 [3] USB : "
    while : ; do
        read interface
        case $interface in
            1) echo "(Q) Which COM port will be connected? e.g. /dev/ttyS0 (COM1)"
               echo -n "(A) Please input: "
               read device # How to verify the input string???
               echo ""
               break;;
            2) echo "(I) Using interface: PS/2"
               echo "(I) Please make sure the kernel module for PS/2 controller is available."
               echo "(I) For details, see the document \"How to rebuild kernel.pdf\"."
               echo ""
               device="/dev/serio_raw0"
               break;;
            3) echo "(I) Using interface: USB"
               device="usbauto"
               CheckUSBType
               echo ""
               break;;
            *) echo "(I) Please choose [1], [2] or [3]"
               echo -n "(A) ";;
        esac
    done
}

RestoreBlacklist() {
    if [ -w ${blacklistpath} ]; then
        grep -q "blacklist usbtouchscreen" ${blacklistpath}
        if [ $? -eq 0 ]; then
            grep -q "# added by eGalax Touch installer" ${blacklistpath}
            if [ $? -eq 0 ]; then
                sed -i '/# added by eGalax Touch installer/,/blacklist usbtouchscreen/d' ${blacklistpath}
                echo "(I) Removed blacklist usbtouchscreen from ${blacklistpath}."
            fi
        else
            grep -q "blacklist touchkitusb" ${blacklistpath}
            if [ $? -eq 0 ]; then
                grep -q "# added by eGalax Touch installer" ${blacklistpath}
                if [ $? -eq 0 ]; then
                    sed -i '/# added by eGalax Touch installer/,/blacklist touchkitusb/d' ${blacklistpath}
                    echo "(I) Removed blacklist touchkitusb from ${blacklistpath}."
                fi
            fi
        fi
    elif [ -w ${blacklistpath2} ]; then
        grep -q "blacklist usbtouchscreen" ${blacklistpath2}
        if [ $? -eq 0 ]; then
            grep -q "# added by eGalax Touch installer" ${blacklistpath2}
            if [ $? -eq 0 ]; then
                sed -i '/# added by eGalax Touch installer/,/blacklist usbtouchscreen/d' ${blacklistpath2}
                echo "(I) Removed blacklist usbtouchscreen from ${blacklistpath2}."
            fi
        else
            grep -q "blacklist touchkitusb" ${blacklistpath2}
            if [ $? -eq 0 ]; then
                grep -q "# added by eGalax Touch installer" ${blacklistpath2}
                if [ $? -eq 0 ]; then
                    sed -i '/# added by eGalax Touch installer/,/blacklist touchkitusb/d' ${blacklistpath2}
                    echo "(I) Removed blacklist touchkitusb from ${blacklistpath2}."
                fi
            fi
        fi
    elif [ -w ${blacklistpath3} ]; then
        grep -q "usbtouchscreen" ${blacklistpath3}
        if [ $? -eq 0 ]; then
            grep -q "# added by eGalax Touch installer" ${blacklistpath3}
            if [ $? -eq 0 ]; then
                sed -i '/# added by eGalax Touch installer/,/usbtouchscreen/d' ${blacklistpath3}
                echo "(I) Removed usbtouchscreen from ${blacklistpath3}."
            fi
        else
            grep -q "touchkitusb" ${blacklistpath3}
            if [ $? -eq 0 ]; then
                grep -q "# added by eGalax Touch installer" ${blacklistpath3}
                if [ $? -eq 0 ]; then
                    sed -i '/# added by eGalax Touch installer/,/touchkitusb/d' ${blacklistpath3}
                    echo "(I) Removed touchkitusb from ${blacklistpath3}."
                fi
            fi
        fi
    elif [ -w ${rclocalpath} ]; then
        grep -q "rmmod usbtouchscreen" ${rclocalpath}
        if [ $? -eq 0 ]; then
            grep -q "# added by eGalax Touch installer" ${rclocalpath}
            if [ $? -eq 0 ]; then
                sed -i '/# added by eGalax Touch installer/,/rmmod usbtouchscreen/d' ${rclocalpath}
                echo "(I) Removed rmmod usbtouchscreen from ${rclocalpath}."
            fi
        else
            grep -q "rmmod touchkitusb" ${rclocalpath}
            if [ $? -eq 0 ]; then
                grep -q "# added by eGalax Touch installer" ${rclocalpath}
                if [ $? -eq 0 ]; then
                    sed -i '/# added by eGalax Touch installer/,/rmmod touchkitusb/d' ${rclocalpath}
                    echo "(I) Removed rmmod touchkitusb from ${rclocalpath}."
                fi
            fi
        fi
    fi
}

AddConfiguration() {
    echo "(I) Add touch configuration into $1."
    filelines=`cat $1 | wc -l`
    sed -i ''${filelines}'a\### Touch Configuration Beginning ###\
Section "InputDevice"\
        Identifier "'${identifier}'"\
        Driver "egalax"\
        Option "Device" "'$2'"\
        Option "Parameters" "'${paramfile}'"\
        Option "ScreenNo" "0"\
EndSection\
### Touch Configuration End ###' $1

    grep -q "Section \"ServerLayout\"" $1
    if [ $? -eq 0 ]; then
        sed -i '/Section "ServerLayout"/a\        InputDevice "'${identifier}'" "SendCoreEvents"' $1
    else
        echo "(W) No \"ServerLayout\" section found! It will be appended automatically."
        sed -i '/### Touch Configuration Beginning ###/a\
Section "ServerLayout"\
        Identifier "Default Layout"\
        InputDevice "'${identifier}'" "SendCoreEvents"\
EndSection\
' $1
        screenfile="screen.info"
        sed '/Section "Screen"/,/EndSection/!d' $1 > ${screenfile} 2>&1
        sed -i '/Identifier/!d' ${screenfile} 2>&1
        screen=`sed 's/Identifier/Screen/g' ${screenfile}`
        if [ -n "${screen}" ]; then
            sed -i '/Identifier "Default Layout"/a\'"${screen}"'' $1
        fi
        rm -f ${screenfile}
    fi

    checkx="X -version"
    xfile="X.info"
    ${checkx} > ${xfile} 2>&1
    grep -q "X.Org X Server 1.4.0.90" ${xfile}
    if [ $? -eq 0 ]; then
        grep -q "Build Operating System: Linux Ubuntu" ${xfile} # Add AutoMapping except Ubuntu 8.04
        if [ $? -eq 1 ]; then
            sed -i '/Option "ScreenNo"/a\        Option "AutoMapping" "1"' $1
        fi
    fi

    usbfile="usb.info"
    checkusb="lsusb -v -d 0eef:0001"
    ${checkusb} > ${usbfile} 2> ${trash}
    grep -q "Current Operating System: Linux asus" ${xfile} # for EPC series
    if [ $? -eq 0 ]; then
        grep -q "Human Interface Device" ${usbfile}
        if [ $? -eq 0 ]; then
            sed -i '/Option "ScreenNo"/a\        Option "HidOnEPC" "1"' $1
        else
            grep -q "Vendor Specific Protocol" ${usbfile}
            if [ $? -eq 1 ]; then
                echo "(I) Note that the option \"HidOnEPC\" \"1\" should be appended in the touch"
                echo "    section of X configuration file for HID compliant touch controller."
            fi
        fi
#    else
#        grep -q "Human Interface Device" ${usbfile}
#        if [ $? -eq 0 ]; then
#            sed -i '/Option "ScreenNo"/a\        Option "SkipClick" "1"' $1
#        fi
    fi
    rm -f ${usbfile}
    rm -f ${xfile}
}

RemoveConfiguration() {
    rm -f ${paramfile}

    sed -i '/"'${identifier}'" "SendCoreEvents"/d' $1
    sed -i '/### Touch Configuration Beginning ###/,/### Touch Configuration End ###/d' $1
    echo "(I) Removed touch configuration from $1."
}

EditConfigFile() {
    if [ -w ${xfilepath} ]; then
        echo "(I) Found X configuration file: ${xfilepath}."
        grep -q "### Touch Configuration Beginning ###" ${xfilepath}
        if [ $? -eq 1 ]; then
            AddConfiguration ${xfilepath} ${device}
        else
            RemoveConfiguration ${xfilepath}
            AddConfiguration ${xfilepath} ${device}
        fi
    elif [ -w ${xfilepath2} ]; then
        echo "(I) Found X configuration file: ${xfilepath2}."
        grep -q "### Touch Configuration Beginning ###" ${xfilepath2}
        if [ $? -eq 1 ]; then
            AddConfiguration ${xfilepath2} ${device}
        else
            RemoveConfiguration ${xfilepath2}
            AddConfiguration ${xfilepath2} ${device}
        fi
    elif [ -w ${xfilepath3} ]; then
        echo "(I) Found X configuration file: ${xfilepath3}."
        grep -q "### Touch Configuration Beginning ###" ${xfilepath3}
        if [ $? -eq 1 ]; then
            AddConfiguration ${xfilepath3} ${device}
        else
            RemoveConfiguration ${xfilepath3}
            AddConfiguration ${xfilepath3} ${device}
        fi
    else
        RemoveDriver
        RemoveXorgModule
        RestoreBlacklist
        echo "(E) No X configuration file found."
        echo ""
        exit 1
    fi
}

RestoreConfigFile() {
    if [ -w ${xfilepath} ]; then
        RemoveConfiguration ${xfilepath}
    elif [ -w ${xfilepath2} ]; then
        RemoveConfiguration ${xfilepath2}
    elif [ -w ${xfilepath3} ]; then
        RemoveConfiguration ${xfilepath3}
    else
        echo "(E) No X configuration file found."
        echo ""
        exit 1
    fi
}

clear
ShowTitle

if [ $# = 0 ]; then
    CheckPermission
    CheckOldDriver
    ExtractDriver
    CopyXorgMudule
    CheckInterface
    EditConfigFile
    echo ""
    echo "(I) Please reboot the system for some changes to take effect."
    echo "(I) After booting, type \"${utility}\" to do calibration."
elif [ $# = 1 ]; then
    if [ $1 = "uninstall" ]; then
        CheckPermission
        echo "(I) Begin to remove eGalax Touch driver."
        RemoveDriver
        RemoveXorgModule
        RestoreBlacklist
        RestoreConfigFile
        echo ""
        echo "(I) The eGalax Touch driver has been removed successfully."
        echo "(I) Please reboot the system for some changes to take effect."
    elif [ $1 = "version" ]; then
        echo "(I) Version: ${version}"
    else
        echo "(I) Usage: sh $0 or sh $0 uninstall"
    fi
else
    echo "(I) Usage: sh $0 or sh $0 uninstall"
fi

echo ""
