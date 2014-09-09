Attribute VB_Name = "kyt_type2"
Public Declare Function EnablePort Lib "kyt_type2.dll" (ByVal port As String, ByVal size As Byte, _
    ByVal parity As Byte, ByVal stopbit As Byte, ByVal BaudRate As Long, ByVal control As Byte) As Boolean
Public Declare Function DisablePort Lib "kyt_type2.dll" () As Boolean:
Public Declare Function exe_cmd Lib "kyt_type2.dll" (ByVal pb_cmd As Byte) As Long:
Public Declare Function chk_res Lib "kyt_type2.dll" (ByVal pbp_stat As Any, ByVal pbp_prc_no As Any, ByVal pi_errno As Any) As Long:
Public Declare Sub exe_stop Lib "kyt_type2.dll" ()
Public Declare Sub call_src_ver Lib "kyt_type2.dll" ()
Public Declare Sub delay Lib "kyt_type2.dll" (ByVal plu_time As Long) 'millisecond
Public Declare Function clr Lib "kyt_type2.dll" () As Long
Public Declare Function rqt Lib "kyt_type2.dll" () As Long
Public Declare Function set_baudrate Lib "kyt_type2.dll" (ByVal pb_pm As Byte) As Long
Public Declare Function fw_version Lib "kyt_type2.dll" () As Long


Public al_res As Long
 Public ab_old_prc_no As Byte
 Public al_errno As Long
 Public ah_res As Boolean

Public Sub disabledPort()
 ah_res = DisablePort()
 
 If ah_res = False Then
    MsgBox "[DisablePort] FALSE"
 Else
    MsgBox "[DisablePort] GOOD"
 End If
End Sub

Public Sub EnabledBaudrate19200()
 Dim as_stat(10) As Byte
 Dim as_prc_no(10) As Byte
 ab_old_prc_no = set_baudrate(81)

 Do
    al_res = chk_res(VarPtr(as_stat(0)), VarPtr(as_prc_no(0)), VarPtr(al_errno))
    delay (10)
 Loop While al_res <> 1

 If as_prc_no(0) <> ab_old_prc_no Then
    MsgBox ("Exception handling ---------- ")
 ElseIf al_errno <> 0 Then
    MsgBox ("Exception handling ---------- ")
 Else
    '
    'Etc processing, "as_stat" variable check ...

    delay (100)
    ah_res = DisablePort()
    delay (100)
    ah_res = EnablePort("COM2", 8, 0, 0, 19200, 0)
    
    If ah_res = False Then
        MsgBox "[set_baudrate 19200] FALSE"
    Else
       MsgBox "[set_baudrate 19200] GOOD"
    End If
 End If
End Sub

Public Sub EnabledPort19200()
    ah_res = EnablePort("COM2", 0, 0, 8, 19200, 0)
    
    If ah_res = False Then
        MsgBox "[Enable Port 19200] FALSE"
    Else
        MsgBox "[Enable Port 19200] GOOD"
    End If
End Sub

Public Sub enablePort9600()
    ah_res = EnablePort("COM2", 0, 0, 8, 9600, 0)
    
    If ah_res = False Then
        MsgBox "[Enable Port 9600] False"
    Else
        MsgBox "[Enable Port 9600] GOOD"
    End If
End Sub

Public Sub TakeOut_Card()
On Error GoTo eror
 Dim as_stat(10) As Byte
 Dim as_prc_no(10) As Byte

 ab_old_prc_no = exe_cmd(64)

 Do
    al_res = chk_res(VarPtr(as_stat(0)), VarPtr(as_prc_no(0)), VarPtr(al_errno))
    delay (10)
 Loop While al_res <> 1
 If as_prc_no(0) <> 0 Then
    'MsgBox "Cannot Take Out Card, Could Not Found Device"
 End If
' MsgBox "[issue]    " & Chr$(13) & Chr$(10) _
' & "as_stat  :" & Hex(as_stat(0)) & "," & Hex(as_stat(1)) & Chr$(13) & Chr$(10) _
' & "al_errno :" & al_errno & Chr$(13) & Chr$(10) _
' & "as_prc_no:" & as_prc_no(0)
eror:
  MsgBox Err.Number & " : " & Err.Description & vbCrLf & "ALIAS" & vbCrLf & "Couldn't found Device"
End Sub
