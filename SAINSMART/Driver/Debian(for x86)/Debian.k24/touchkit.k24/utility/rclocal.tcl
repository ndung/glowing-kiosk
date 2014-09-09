#!/bin/sh
# the next line restarts using tclsh \
	exec tclsh "$0" "$@"


### procedures ###
#{

proc AddPrjSection {output} {
	global BeginTag EndTag MODULE_NAME PRJNAME
	global TPANELD_NAME USBPNPD_NAME
	
	puts $output "$BeginTag"
	if { [file exists /proc/bus/usb] } {
		puts $output "	insmod /lib/modules/tkusb.o" 
		puts $output "	/usr/bin/$USBPNPD_NAME"
	}
	puts $output "	/usr/bin/$TPANELD_NAME"
	puts $output "$EndTag"
}

#}

####################################
#  main()
####################################

source utility/library.tcl

if { $argc } {
	#uninstall
	set Action -1
} else {
	#install
	set Action 1
}

set tool_path "include/"

if {[LoadGlobalSetting ${tool_path}]} {
	puts "Can not load global settings"
	exit 1
}

## files to be modified
set rclocal rc.local

set BeginTag	"## ${PRJNAME} section begin (Please do NOT edit this section!!) ##"
set EndTag		"## ${PRJNAME} section end ##"

if { [catch {exec find /etc -name $rclocal | head -n 1} rval] } {
	puts $rval
	exit 1
}
if { ! [string compare $rval ""] } {
	set RCS "/etc/rcS.d"
	set INITD "/etc/init.d"
	if { [file exists $RCS] && [file isdirectory $RCS] } {
		#for Debain :~~
		set rval ${INITD}/rc.local
		catch {
			exec echo "#!/bin/sh" > $rval
			exec echo " " >> $rval
			exec chmod 755 $rval
			exec ln -s $rval ${RCS}/Src.local
		}
	} else {
		puts "** \[ ERROR !! \]"
		puts "** find $rclocal: file not found"
		exit 1
	}
}
set RCLOCAL	$rval
puts "(*) Update system starting up script \[$RCLOCAL\]"
if { [catch {open $RCLOCAL r} input] } {
	puts "** \[ ERROR !! \]"
	puts "** open $rclocal: $input"
	exit 1
}
set output [open /tmp/$rclocal.tmp w]

set state NotFound
while { [gets $input line] >= 0 } {
	switch $state {
	NotFound {
		if { [string first $BeginTag $line] == 0 } {
			if { $Action > 0 } {
				AddPrjSection $output
			}
			set state Found
		} else {
			puts $output $line
		}
	}
	Found {
		if { [string first $EndTag $line] == 0 } {
			set state Update	
		}	 
	}
	Update {
		puts $output $line
	}
	}
}

if { ![string compare "NotFound" $state] } {
	if { $Action > 0 } {
		AddPrjSection $output
	}
}
if { ![string compare "Found" $state] } {
	puts "** \[ WARNING !! \]"
	puts "** \"$EndTag\" not found"
	puts "**"
	puts "** ($RCLOCAL might be truncated)"
}
close $output
close $input
if { [catch {file rename -force $RCLOCAL $RCLOCAL.bak} rval] } {
	puts "** \[ ERROR !! \]"
	puts "** rename $RCLOCAL as $RCLOCAL.bak: $rval"
	exit 1	
}
if { [catch {file copy -force /tmp/$rclocal.tmp $RCLOCAL} rval] } {
	puts "** \[ ERROR !! \]"
	puts "** copy /tmp/$rclocal.tmp to $RCLOCAL: $rval"
	exit 1	
}
if { [catch {file attributes $RCLOCAL -permissions 0755} rval] } {
	puts "** \[ ERROR !! \]"
	puts "** chmod 755 $RCLOCAL: $rval"
	exit 1	
}
exit 0

	
							
