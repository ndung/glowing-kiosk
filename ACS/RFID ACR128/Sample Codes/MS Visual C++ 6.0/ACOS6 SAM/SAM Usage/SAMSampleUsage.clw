; CLW file contains information for the MFC ClassWizard

[General Info]
Version=1
LastClass=ACCOUNT
LastTemplate=CTabCtrl
NewFileInclude1=#include "stdafx.h"
NewFileInclude2=#include "SAMSampleUsage.h"

ClassCount=6
Class1=CSAMSampleUsageApp
Class2=CSAMSampleUsageDlg
Class3=CAboutDlg

ResourceCount=5
Resource1=IDD_SAMSAMPLEUSAGE_DIALOG
Resource2=IDR_MAINFRAME
Resource3=IDD_ABOUTBOX
Class5=SECURITY
Resource4=IDD_ACCOUNT_DIALOG
Class6=MyTabCtrl
Class4=ACCOUNT
Resource5=IDD_SECURITY_DIALOG

[CLS:CSAMSampleUsageApp]
Type=0
HeaderFile=SAMSampleUsage.h
ImplementationFile=SAMSampleUsage.cpp
Filter=N

[CLS:CSAMSampleUsageDlg]
Type=0
HeaderFile=SAMSampleUsageDlg.h
ImplementationFile=SAMSampleUsageDlg.cpp
Filter=D
LastObject=IDC_LIST1
BaseClass=CDialog
VirtualFilter=dWC

[CLS:CAboutDlg]
Type=0
HeaderFile=SAMSampleUsageDlg.h
ImplementationFile=SAMSampleUsageDlg.cpp
Filter=D
LastObject=IDOK

[DLG:IDD_ABOUTBOX]
Type=1
Class=CAboutDlg
ControlCount=4
Control1=IDC_STATIC,static,1342177283
Control2=IDC_STATIC,static,1342308480
Control3=IDC_STATIC,static,1342308352
Control4=IDOK,button,1342373889

[DLG:IDD_SAMSAMPLEUSAGE_DIALOG]
Type=1
Class=CSAMSampleUsageDlg
ControlCount=2
Control1=IDC_TAB1,SysTabControl32,1342177280
Control2=IDC_LIST1,listbox,1352728833

[DLG:IDD_ACCOUNT_DIALOG]
Type=1
Class=ACCOUNT
ControlCount=11
Control1=IDC_STATIC,static,1342308352
Control2=IDC_EDIT1,edit,1350633600
Control3=IDC_STATIC,static,1342308352
Control4=IDC_EDIT2,edit,1350633600
Control5=IDC_BUTTON1,button,1342242816
Control6=IDC_STATIC,static,1342308352
Control7=IDC_EDIT3,edit,1350631552
Control8=IDC_BUTTON2,button,1342242816
Control9=IDC_STATIC,static,1342308352
Control10=IDC_BUTTON3,button,1342242816
Control11=IDC_EDIT4,edit,1350631552

[CLS:ACCOUNT]
Type=0
HeaderFile=ACCOUNT.h
ImplementationFile=ACCOUNT.cpp
BaseClass=CDialog
Filter=D
VirtualFilter=dWC
LastObject=ACCOUNT

[DLG:IDD_SECURITY_DIALOG]
Type=1
Class=SECURITY
ControlCount=17
Control1=IDC_BUTTON1,button,1342242816
Control2=IDC_STATIC,static,1342308352
Control3=IDC_COMBO1,combobox,1344340226
Control4=IDC_STATIC,static,1342308352
Control5=IDC_COMBO2,combobox,1344340226
Control6=IDC_BUTTON2,button,1342242816
Control7=IDC_RADIO1,button,1342308361
Control8=IDC_RADIO2,button,1342308361
Control9=IDC_STATIC,static,1342308352
Control10=IDC_EDIT1,edit,1350631552
Control11=IDC_BUTTON3,button,1342242816
Control12=IDC_STATIC,static,1342308352
Control13=IDC_EDIT2,edit,1350631552
Control14=IDC_BUTTON4,button,1342242816
Control15=IDC_STATIC,static,1342308352
Control16=IDC_EDIT3,edit,1350631552
Control17=IDC_BUTTON5,button,1342242816

[CLS:SECURITY]
Type=0
HeaderFile=SECURITY.h
ImplementationFile=SECURITY.cpp
BaseClass=CDialog
Filter=D
VirtualFilter=dWC
LastObject=IDC_BUTTON5

[CLS:MyTabCtrl]
Type=0
HeaderFile=MyTabCtrl.h
ImplementationFile=MyTabCtrl.cpp
BaseClass=CTabCtrl
Filter=W
VirtualFilter=UWC
LastObject=MyTabCtrl

