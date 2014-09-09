
=== File List ================================================================

TPANEL   EXE        driver
TKTEST   EXE        helper program of TPANEL.EXE  
TKCFG    EXE        helper program of TPANEL.EXE
TPANEL   INI        configurations (created by TPANEL.EXE if absent)
TP4PCAL  EXE        4-point calibration
TP25PCAL EXE        25-point calibration
TPDRAW   EXE        draw test
RCLICK   COM        right-click enabler


=== Installation =============================================================

Execute "install.bat [destination]", and it will do as follows

(1) copy files to hard disk, and
(2) modify/create C:\AUTOEXEC.BAT

ex. "INSTALL C:\" creates directory C:\TOUCHKIT into which files listed above
    will be copied, and C:\AUTOEXEC.BAT will be appended with:

    SET PATH=%PATH%;C:\TOUCHKIT
    TPANEL.EXE
    RCLICK.COM                                <--- (this line is optional)

note: Execute "TPANEL /?" for more details.

=== Uninstall ================================================================

(1) Restore C:\AUTOEXEC.BAT with

    copy C:\AUTOEXEC.TKT C:\AUTOEXEC.BAT

(2) Delete all files copied with

    deltree C:\TOUCHKIT                     <--- (where you installed driver)



=== Serial port configuration ====================================================
To avoid confusing the end-user, touchkit driver use the "virtual TKT" port name 
instead of the origional "com" port name. In order version driver, touchkit driver
parsers the "com1" and "com2" from the system environment. If the user need another
"com" port, for example "com5", all the enduser to do is set the environment variables
as set COM1 = IOADDR, IRQ. However, this new driver package use "TKT" instead of "com".

This suit supports at most two serial ports; which are TKT1 (IO:0x3F8 IRQ:4)
and TKT2 (IO:0x2F8 IRQ:3) by default. If your bios settings differ (ex. COM1
is of IO addr 3E8 and IRQ 4), add one more line (before "TPANEL") in
C:\AUTOEXEC.BAT to redirect:
    SET TKT1=3E8 4
    
Also, If the user needs the controller attaeched at another serial port ( for example, 
"com5" where IO address is 4F8, IRQ is 4 ), it need to redirect the system environment variable
by
        SET TKT1 = 4F8 4 
before launch "TPANEL.EXE" in the autoexec.BAT        

=== Calibration & Testing ====================================================

Suppose that touch controller is attached on TKT1, execute "tp4pcal TKT1";
and then touch the screen where blinking in turn. After calibration, test with
"tpdraw TKT1".

It is optional to add arguments such as "-g 1024x768" to specify screen 
resolution on calibration or testing.

    Resolutions supported:
	
    320x200, 640x200, 640x350, 640x480, 800x600, 1024x768, 1280x1024

note: Use 25-point calibration instead for higher accuracy.


=== Sound Prompt =============================================================

Edit "TPANEL.INI" and choose mode preferred.

syntax:

 Beep = none|down|up  [frequency]

 none: no sound; keep silent
 down: beep when you touch down
 up: beep when you touch up


=== Configure Serial Port ====================================================



=== BUG ======================================================================

(*) When driver or calibration utility starts up, touching the panel might 
lead to unexpectable result.

(*) INT 33h functions relative to drawing mouse arrow under graphical mode
are not implemented.


=== TRICKY ===================================================================

(*) Contents (at most 800 bytes) between [PromptBegin] and [PromptEnd] in
TPANEL.INI will be taken as driver brand.

