@echo off

set old_path=%path%

:search for writable directory
set temp_file=_xx__zyq

:noTEMP
echo hi > c:\%temp_file%
if not exist c:\%temp_file% goto test_D
del c:\%temp_file%
set temp_dir=C:\
goto tmp_ok

:test_D
echo hi > d:\%temp_file%
if not exist d:\%temp_file% goto test_A
del d:\%temp_file%
set temp_dir=D:\
goto tmp_ok

:test_A
echo hi > a:\%temp_file%
if not exist a:\%temp_file% goto fail
del a:\%temp_file%
set temp_dir=A:\
goto tmp_ok

:fail
echo Sorry, I can not find any writable drive.
goto end

:tmp_ok

if m%1 == m goto hint

:DIRTOOL will change directory to %1 in full-path style if possible
utility\DIRTOOL %1 > %temp_dir%%temp_file%.bat
if errorlevel 1 goto end
:DIRTOOL-gened xxs_e_t.bat will setenv SRC_PATH and DEST_PATH for me :p
call %temp_dir%%temp_file%.bat
del %temp_dir%%temp_file%.bat

mkdir %DEST_PATH%touchkit
echo Copy files...
copy /y %SRC_PATH%binary\*.* %DEST_PATH%touchkit > nul
copy /y %SRC_PATH%readme.txt %DEST_PATH%touchkit > nul
echo Copy files...Done

if exist C:\AUTOEXEC.TKT copy /y C:\AUTOEXEC.TKT C:\AUTOEXEC.BAT > nul
if not exist C:\AUTOEXEC.BAT goto no_auto_1
	copy /y C:\AUTOEXEC.BAT C:\AUTOEXEC.TKT > nul
:no_auto_1
echo Append settings to C:\AUTOEXEC.BAT...
if not exist C:\AUTOEXEC.BAT goto no_auto_2
	echo (Original C:\AUTOEXEC.BAT is saved as AUTOEXEC.TKT)
:no_auto_2
echo. >> C:\AUTOEXEC.BAT
echo set path=%%path%%;%DEST_PATH%touchkit >> C:\AUTOEXEC.BAT
echo rem Add your own serial port configuration here, ex. SET TKT1=3E8 4 >> C:\AUTOEXEC.BAT
echo TPANEL.EXE >> C:\AUTOEXEC.BAT
echo rem Execute "TPANEL /?" to list options >> C:\AUTOEXEC.BAT
choice /N "Do you want to activate Right Button Tool? (Y,N)"
if errorlevel 2 goto noRBT
	echo RCLICK.COM >> C:\AUTOEXEC.BAT
	goto RBTdone
:noRBT
	echo rem RCLICK.COM >> C:\AUTOEXEC.BAT
:RBTdone
echo rem Right Button Tool >> C:\AUTOEXEC.BAT
echo.
echo Installation OK, please REBOOT.
goto end

:hint
cls
echo *************************************
echo *                                   *
echo *    TouchKit for DOS Installer     *
echo *                                   *
echo *************************************
echo.
echo syntax: INSTALL dest_dir
echo.
echo ex. "INSTALL C:\DIR" will install driver into C:\DIR\touchkit
echo.
goto end
:end
set path=%old_path%
set old_path=
set temp_file=
set temp_dir=
set SRC_PATH=
set DEST_PATH=
echo.

