Attribute VB_Name = "Module1"
Public Declare Function EnablePort Lib "kyt_20xx.dll" (ByVal port As String, ByVal size As Byte, _
    ByVal parity As Byte, ByVal stopbit As Byte, ByVal BaudRate As Long, ByVal control As Byte) As Boolean
Public Declare Function DisablePort Lib "kyt_20xx.dll" () As Boolean:
Public Declare Function exe_cmd Lib "kyt_20xx.dll" (ByVal pb_cmd As Byte) As Long:
Public Declare Function chk_res Lib "kyt_20xx.dll" (ByVal pbp_stat As Any, ByVal pbp_prc_no As Any,ByVal pi_errno As Any) As Long:
Public Declare Sub exe_stop Lib "kyt_20xx.dll" ()
Public Declare Sub call_src_ver Lib "kyt_20xx.dll" ()
Public Declare Sub delay Lib "kyt_20xx.dll" (ByVal plu_time As Long) 'millisecond
Public Declare Function clr Lib "kyt_20xx.dll" () As Long
Public Declare Function rqt Lib "kyt_20xx.dll" () As Long
Public Declare Function issue Lib "kyt_20xx.dll" (ByVal pb_stk As Byte) As Long
Public Declare Function set_baudrate Lib "kyt_20xx.dll" (ByVal pb_pm As Byte) As Long
Public Declare Function set_issue Lib "kyt_20xx.dll" (ByVal pb_pm As Byte) As Long
Public Declare Function card_capture Lib "kyt_20xx.dll" () As Long
Public Declare Function card_wait Lib "kyt_20xx.dll" (ByVal pb_pm As Byte) As Long
Public Declare Function fw_version Lib "kyt_20xx.dll" () As Long

'@.1 Notice: Variables Declaration 
'
'  pbp_stat   => Dim NAME(10) As Byte 
'  pbp_prc_no => Dim NAME(10) As Byte 
'  pi_errno   => Dim NAME As Long 
'
'@.2 Notice
'  delay() function 
'       - Temporary function for visual basic.
'       - Parameter is millisecond
