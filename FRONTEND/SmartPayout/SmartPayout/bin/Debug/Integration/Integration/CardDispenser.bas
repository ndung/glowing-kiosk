Attribute VB_Name = "CardDispenser"
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

'Variable Declaration
Public ah_res As Boolean
Public RespPort As String
Public al_res As Long
Public ab_old_prc_no As Byte
Public as_stat(10) As Byte
Public as_prc_no(10) As Byte
Public al_errno As Long
Public CardDispensing As String
Public CardDispensingErr As String

Public Sub callVersion()
    call_src_ver
End Sub

Public Function DisabledCardDispenserPort() As Boolean
    ah_res = DisablePort()
    If ah_res = False Then
        RespPort = "[DisablePort] FALSE"
        DisabledCardDispenserPort = False
    Else
        RespPort = "[DisablePort] GOOD"
        DisabledCardDispenserPort = True
    End If
End Function

Public Function EnabledCardDispenserPort19200() As Boolean
    ah_res = EnablePort("COM1", 8, 0, 0, 19200, 0)
    If ah_res = False Then
        RespPort = "[EnablePort 19200] FALSE"
        EnabledCardDispenserPort19200 = False
    Else
        RespPort = "[EnablePort 19200] GOOD"
        EnabledCardDispenserPort19200 = True
    End If
    'frmMain.Text1.Text = RespPort
End Function

Public Function EnabledCardDispenserPort9600() As Boolean
    ah_res = EnablePort("COM9", 8, 0, 0, 9600, 0)
    If ah_res = False Then
        Call WriteLog("enable port 9600 card dispenser : false")
        RespPort = "[EnablePort 9600] FALSE"
        EnabledCardDispenserPort9600 = False
    Else
        Call WriteLog("enable port 9600 card dispenser : good")
        RespPort = "[EnablePort 9600] GOOD"
        EnabledCardDispenserPort9600 = True
    End If
    'frmMain.Text1.text = RespPort
End Function

Public Function TakeOutCard() As String
    '0x40
    CardDispenserCmd (64)
End Function

Public Function ClearMotorJam() As Boolean
    '0x30
    CardDispenserCmd (48)
End Function

Public Function GetCardDispenserStatus() As String
Dim statusCode As String
    '0x31
    
    '0x80 : GOOD '128
    '0xC0 : Busy '192
    '0xA0 : back sensor detection '160
    '0x90 : motor fail/ card jam '144
    '0x88 : front sensor detection '136
    '0x84 : finish sensor '132
    '0x82 : warning sensor '130
    '0x81 : card empty '129
    
    ab_old_prc_no = exe_cmd(49)
 
    Do
        al_res = chk_res(VarPtr(as_stat(0)), VarPtr(as_prc_no(0)), VarPtr(al_errno))
        delay (10)
    Loop While al_res <> 1
 
    CardDispensing = "cmd get card dispenser status:" & " as_stat :" & Hex(as_stat(0)) & "," & Hex(as_stat(1)) & "; al_errno :" & al_errno & "; as_prc_no :" & as_prc_no(0)
    Call WriteLog(CardDispensing)
 
    If as_prc_no(0) <> ab_old_prc_no Then
        Call WriteLog("card dispenser exception: ")
        Call WriteLog("as_prc_no(0) = " & as_prc_no(0))
        Call WriteLog("ab_old_prc_no = " & ab_old_prc_no)
        CardDispensingErr = "Exception handling"
    End If
 
    If al_errno <> 0 Then
        Call WriteLog("card dispenser exception: ")
        Call WriteLog("al_errno = " & al_errno)
        CardDispensingErr = "Exception handling"
    End If
    
    statusCode = Hex(as_stat(0))
    
    If statusCode = "80" Then
        GetCardDispenserStatus = "00"
    ElseIf statusCode = "C0" Then
        GetCardDispenserStatus = "B1"
    ElseIf statusCode = "A0" Then
        GetCardDispenserStatus = "B2"
    ElseIf statusCode = "90" Then
        GetCardDispenserStatus = "B3"
    ElseIf statusCode = "88" Then
        GetCardDispenserStatus = "B4"
    ElseIf statusCode = "84" Then
        GetCardDispenserStatus = "B5"
    ElseIf statusCode = "82" Then
        GetCardDispenserStatus = "B6"
    ElseIf statusCode = "81" Then
        GetCardDispenserStatus = "B7"
    Else
        GetCardDispenserStatus = RightJustify(statusCode, 2)
    End If
    
End Function

Public Function SetCardIssuedLength() As String
    CardDispenserCmd (240) '0xF0 : Card drop (default)
    'CardDispenserCmd (241) 0xF1 : 3mm
    'CardDispenserCmd (242) 0xF2 : 36mm
    'CardDispenserCmd (243) 0xF3 : 54mm
    'CardDispenserCmd (244) 0xF4 : 64mm
End Function
Public Function CardDispenserCmd(ByVal pbCmd As Byte) As String
    ab_old_prc_no = exe_cmd(pbCmd)
 
    Do
        al_res = chk_res(VarPtr(as_stat(0)), VarPtr(as_prc_no(0)), VarPtr(al_errno))
        delay (10)
    Loop While al_res <> 1
 
    CardDispensing = "cmd resp :" & pbCmd & "; as_stat :" & Hex(as_stat(0)) & "," & Hex(as_stat(1)) & "; al_errno :" & al_errno & "; as_prc_no :" & as_prc_no(0)
    Call WriteLog(CardDispensing)
 
    If as_prc_no(0) <> ab_old_prc_no Then
        Call WriteLog("card dispenser exception: ")
        Call WriteLog("as_prc_no(0) = " & as_prc_no(0))
        Call WriteLog("ab_old_prc_no = " & ab_old_prc_no)
        CardDispensingErr = "Exception handling"
    End If
 
    If al_errno <> 0 Then
        Call WriteLog("card dispenser exception: ")
        Call WriteLog("al_errno = " & al_errno)
        CardDispensingErr = "Exception handling"
    End If
    
    CardDispenserCmd = al_errno
    
    If al_errno = 0 Then ' No Error
        CardDispenserCmd = "00"
    ElseIf al_errno = 1 Then ' No Ack Error
        CardDispenserCmd = "01"
    ElseIf al_errno = 2 Then 'Timeout error
        CardDispenserCmd = "02"
    ElseIf al_errno = 3 Then 'NAK error
        CardDispenserCmd = "03"
    ElseIf al_errno = 2000 Then 'Negative error
        CardDispenserCmd = "04"
    ElseIf al_errno = 102 Then 'Compulsion termination error
        CardDispenserCmd = "05"
    ElseIf al_errno = 106 Then 'Packet frame error
        CardDispenserCmd = "06"
    ElseIf al_errno = 107 Then 'BCC error
        CardDispenserCmd = "07"
    Else
        CardDispenserCmd = "08"
    End If
End Function
