VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form DevProgMain 
   Caption         =   "ACR128 Device Programming"
   ClientHeight    =   8880
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   9585
   Icon            =   "DevProgMain.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   8880
   ScaleWidth      =   9585
   StartUpPosition =   2  'CenterScreen
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   8055
      Left            =   4200
      TabIndex        =   26
      Top             =   240
      Width           =   5175
      _ExtentX        =   9128
      _ExtentY        =   14208
      _Version        =   393217
      Enabled         =   -1  'True
      ScrollBars      =   2
      TextRTF         =   $"DevProgMain.frx":17A2
   End
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   7800
      TabIndex        =   20
      Top             =   8400
      Width           =   1575
   End
   Begin VB.CommandButton bReset 
      Caption         =   "&Reset"
      Height          =   375
      Left            =   6000
      TabIndex        =   19
      Top             =   8400
      Width           =   1575
   End
   Begin VB.CommandButton bClear 
      Caption         =   "C&lear Screen"
      Height          =   375
      Left            =   4200
      TabIndex        =   18
      Top             =   8400
      Width           =   1575
   End
   Begin VB.Frame frBuzzState 
      Caption         =   "LED and Buzzer Setting"
      Height          =   3975
      Left            =   120
      TabIndex        =   25
      Top             =   4800
      Width           =   3975
      Begin VB.CommandButton bSetBuzzState 
         Caption         =   "Set &Buzzer/LED State"
         Height          =   375
         Left            =   2040
         TabIndex        =   17
         Top             =   3360
         Width           =   1815
      End
      Begin VB.CommandButton bGetBuzzState 
         Caption         =   "Get B&uzzer/LED State"
         Height          =   375
         Left            =   120
         TabIndex        =   16
         Top             =   3360
         Width           =   1815
      End
      Begin VB.CheckBox cbBuzzLed8 
         Caption         =   "Enable Card Operation Blinking LED"
         Height          =   255
         Left            =   120
         TabIndex        =   15
         Top             =   2880
         Width           =   3255
      End
      Begin VB.CheckBox cbBuzzLed7 
         Caption         =   "Enable Exclusive Mode Status Buzzer"
         Height          =   255
         Left            =   120
         TabIndex        =   14
         Top             =   2520
         Width           =   3255
      End
      Begin VB.CheckBox cbBuzzLed6 
         Caption         =   "Enable RC531 Reset Indication Buzzer"
         Height          =   255
         Left            =   120
         TabIndex        =   13
         Top             =   2160
         Width           =   3375
      End
      Begin VB.CheckBox cbBuzzLed5 
         Caption         =   "Enable Card Insertion/Removal Events Buzzer"
         Height          =   255
         Left            =   120
         TabIndex        =   12
         Top             =   1800
         Width           =   3735
      End
      Begin VB.CheckBox cbBuzzLed4 
         Caption         =   "Enable PICC PPS Status Buzzer"
         Height          =   255
         Left            =   120
         TabIndex        =   11
         Top             =   1440
         Width           =   3495
      End
      Begin VB.CheckBox cbBuzzLed3 
         Caption         =   "Enable PICC Activation Status Buzzer"
         Height          =   255
         Left            =   120
         TabIndex        =   10
         Top             =   1080
         Width           =   3495
      End
      Begin VB.CheckBox cbBuzzLed2 
         Caption         =   "Enable PICC Polling Status LED"
         Height          =   255
         Left            =   120
         TabIndex        =   9
         Top             =   720
         Width           =   3615
      End
      Begin VB.CheckBox cbBuzzLed1 
         Caption         =   "Enable ICC Activation Status LED"
         Height          =   255
         Left            =   120
         TabIndex        =   8
         Top             =   360
         Width           =   3495
      End
   End
   Begin VB.Frame frBuzz 
      Caption         =   "Set Buzzer Duration (x10 mSec)"
      Height          =   1455
      Left            =   120
      TabIndex        =   23
      Top             =   3240
      Width           =   3975
      Begin VB.CommandButton bStartBuzz 
         Caption         =   "Start Buzzer Tone"
         Height          =   375
         Left            =   360
         TabIndex        =   30
         Top             =   360
         Width           =   1695
      End
      Begin VB.CommandButton bStopBuzz 
         Caption         =   "Stop Buzzer Tone"
         Height          =   375
         Left            =   2160
         TabIndex        =   29
         Top             =   360
         Width           =   1695
      End
      Begin VB.CommandButton bSetbuzzDur 
         Caption         =   "Set Buzzer &Duration"
         Height          =   375
         Left            =   2160
         TabIndex        =   7
         Top             =   840
         Width           =   1695
      End
      Begin VB.TextBox tBuzzDur 
         Height          =   285
         Left            =   1200
         MaxLength       =   3
         TabIndex        =   6
         Top             =   960
         Width           =   615
      End
      Begin VB.Label Label2 
         Caption         =   "Value"
         Height          =   255
         Left            =   600
         TabIndex        =   24
         Top             =   960
         Width           =   615
      End
   End
   Begin VB.Frame frLED 
      Caption         =   "LED Setting"
      Height          =   975
      Left            =   120
      TabIndex        =   22
      Top             =   2160
      Width           =   2175
      Begin VB.CheckBox cbGreen 
         Caption         =   "Green"
         Height          =   195
         Left            =   1200
         TabIndex        =   28
         Top             =   480
         Width           =   735
      End
      Begin VB.CheckBox cbRed 
         Caption         =   "Red"
         Height          =   195
         Left            =   240
         TabIndex        =   27
         Top             =   480
         Width           =   855
      End
   End
   Begin VB.CommandButton bSetLedSet 
      Caption         =   "&Set LED State"
      Height          =   375
      Left            =   2400
      TabIndex        =   5
      Top             =   2760
      Width           =   1695
   End
   Begin VB.CommandButton bGetLedSet 
      Caption         =   "&Get LED State"
      Height          =   375
      Left            =   2400
      TabIndex        =   4
      Top             =   2280
      Width           =   1695
   End
   Begin VB.CommandButton bgetFW 
      Caption         =   "Get &Firmware Version"
      Height          =   375
      Left            =   2400
      TabIndex        =   3
      Top             =   1680
      Width           =   1695
   End
   Begin VB.CommandButton bConnect 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   2400
      TabIndex        =   2
      Top             =   1200
      Width           =   1695
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initialize"
      Height          =   375
      Left            =   2400
      TabIndex        =   1
      Top             =   720
      Width           =   1695
   End
   Begin VB.ComboBox cbReader 
      Height          =   315
      Left            =   1440
      TabIndex        =   0
      Top             =   240
      Width           =   2655
   End
   Begin VB.Label Label1 
      BackStyle       =   0  'Transparent
      Caption         =   "Select Reader"
      Height          =   375
      Left            =   240
      TabIndex        =   21
      Top             =   360
      Width           =   1215
   End
End
Attribute VB_Name = "DevProgMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              DevProgMain.frm
'
'  Description:       This sample program outlines the steps on how to
'                     execute device-specific functions of ACR128
'
'  Author:            M.J.E.C. Castillo
'
'  Date:              June 2, 2008
'
'  Revision Trail:   (Date/Author/Description)
'
'======================================================================

Option Explicit

Dim retCode, Protocol, hContext, hCard, ReaderCount As Long
Dim sReaderList As String * 256
Dim sReaderGroup As String
Dim ConnActive As Boolean
Dim ioRequest As SCARD_IO_REQUEST
Dim SendLen, RecvLen, nBytesRet As Long
Dim SendBuff(0 To 262) As Byte
Dim RecvBuff(0 To 262) As Byte

Const INVALID_SW1SW2 = -450

Private Sub bClear_Click()

    mMsg.Text = ""

End Sub

Private Sub bConnect_Click()

  bgetFW.Enabled = True
  
  If ConnActive Then
  
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
  
  End If
    
    '1. Shared Connection
    retCode = SCardConnect(hContext, _
                        cbReader.Text, _
                        SCARD_SHARE_SHARED, _
                        SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1, _
                        hCard, _
                        Protocol)
    If retCode <> SCARD_S_SUCCESS Then
    
        'check if ACR128 SAM is used and use Direct Mode if SAM is not detected
        If InStr(cbReader.Text, "ACR128U SAM") > 0 Then
            
            '1. Direct Connection
             retCode = SCardConnect(hContext, _
                        cbReader.Text, _
                        SCARD_SHARE_DIRECT, _
                        0, _
                        hCard, _
                        Protocol)
                        
             If retCode <> SCARD_S_SUCCESS Then
                
                Call DisplayOut(1, retCode, "")
                ConnActive = False
                Exit Sub
                
             Else
             
                Call DisplayOut(0, 0, "Successful connection to " & cbReader.Text)
             
             End If
            
        Else
        
            Call DisplayOut(1, retCode, "")
            ConnActive = False
            Exit Sub
        
        End If
    
    Else
    
        Call DisplayOut(0, 0, "Successful connection to " & cbReader.Text)
    
    End If

  ConnActive = True
  bgetFW.Enabled = True
  frBuzz.Enabled = True
  frBuzzState.Enabled = True
  bGetLedSet.Enabled = True
  bSetLedSet.Enabled = True
  Call GetLEDState
  Call GetBuzzLEDState

End Sub

Private Sub bGetBuzzState_Click()

    Call GetBuzzLEDState

End Sub

Private Sub bgetFW_Click()

Dim tmpStr As String
Dim intIndx As Integer

    Call ClearBuffers
    SendBuff(0) = &H18
    SendBuff(1) = &H0
    SendLen = 2
    RecvLen = 35
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then

        Exit Sub
    
    End If
    
    'Interpret firmware data
    tmpStr = "Firmware Version: "
    
    For intIndx = 5 To 24
        
        If RecvBuff(intIndx) <> &H0 Then
        
            tmpStr = tmpStr & Chr(RecvBuff(intIndx))
        
        End If
        
    Next intIndx
    
    Call DisplayOut(3, 0, tmpStr)

End Sub

Private Sub bGetLedSet_Click()

    Call GetLEDState

End Sub

Private Sub bInit_Click()

Dim intIndx As Integer

  sReaderList = String(255, vbNullChar)
  ReaderCount = 255
     
  ' 1. Establish context and obtain hContext handle
  retCode = SCardEstablishContext(SCARD_SCOPE_USER, 0, 0, hContext)
  
  If retCode <> SCARD_S_SUCCESS Then
    
    Call DisplayOut(1, retCode, "")
    Exit Sub
    
  End If
  
  ' 2. List PC/SC card readers installed in the system
  retCode = SCardListReaders(hContext, sReaderGroup, sReaderList, ReaderCount)
  
  If retCode <> SCARD_S_SUCCESS Then
  
    Call DisplayOut(1, retCode, "")
    Exit Sub
    
  End If
  
  Call LoadListToControl(cbReader, sReaderList)
  cbReader.ListIndex = 0
  Call AddButtons

  'Look for ACR128 SAM and make it the default reader in the combobox
  For intIndx = 0 To cbReader.ListCount - 1
    
    cbReader.ListIndex = intIndx
    
    If InStr(cbReader.Text, "ACR128U SAM") > 0 Then
        
        Exit For
       
    End If
     
  Next intIndx
   
End Sub

Private Sub bQuit_Click()

  If ConnActive Then
  
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
    
  End If
  
  retCode = SCardReleaseContext(hContext)
  Unload Me

End Sub

Private Sub bReset_Click()

    If ConnActive Then
    
        retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    
    End If
    
    retCode = SCardReleaseContext(hCard)
    Call InitMenu

End Sub

Private Sub bSetbuzzDur_Click()

    If tBuzzDur.Text = "" Then
    
        tBuzzDur.Text = 1
        tBuzzDur.SelStart = 0
        tBuzzDur.SelLength = Len(tBuzzDur.Text)
        tBuzzDur.SetFocus
    
    End If
    
    If CInt(tBuzzDur.Text) > 255 Then
    
        tBuzzDur.Text = 255
        tBuzzDur.SelStart = 0
        tBuzzDur.SelLength = Len(tBuzzDur.Text)
        tBuzzDur.SetFocus
        Exit Sub
    
    End If
    
    Call ClearBuffers
    SendBuff(0) = &H28
    SendBuff(1) = &H1
    SendBuff(2) = CInt(tBuzzDur.Text)
    SendLen = 3
    RecvLen = 6
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If

End Sub

Private Sub bSetBuzzState_Click()

    Call ClearBuffers
    SendBuff(0) = &H21
    SendBuff(1) = &H1
    SendBuff(2) = &H0
    
    If cbBuzzLed1.Value = vbChecked Then
        
        SendBuff(2) = SendBuff(2) Or &H1
        
    End If
    
    If cbBuzzLed2.Value = vbChecked Then
        
        SendBuff(2) = SendBuff(2) Or &H2
        
    End If
    
    If cbBuzzLed3.Value = vbChecked Then
        
        SendBuff(2) = SendBuff(2) Or &H4
        
    End If
    
    If cbBuzzLed4.Value = vbChecked Then
        
        SendBuff(2) = SendBuff(2) Or &H8
        
    End If
    
    If cbBuzzLed5.Value = vbChecked Then
        
        SendBuff(2) = SendBuff(2) Or &H10
        
    End If
    
    If cbBuzzLed6.Value = vbChecked Then
        
        SendBuff(2) = SendBuff(2) Or &H20
        
    End If
    
    If cbBuzzLed7.Value = vbChecked Then
        
        SendBuff(2) = SendBuff(2) Or &H40
        
    End If
    
    If cbBuzzLed8.Value = vbChecked Then
        
        SendBuff(2) = SendBuff(2) Or &H80
        
    End If
    
    SendLen = 3
    RecvLen = 6
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If

End Sub

Private Sub bSetLedSet_Click()

    Call ClearBuffers
    SendBuff(0) = &H29
    SendBuff(1) = &H1
    
    If cbRed.Value = vbChecked Then
    
        SendBuff(2) = SendBuff(2) Or &H1
    
    End If
    
    If cbGreen.Value = vbChecked Then
    
        SendBuff(2) = SendBuff(2) Or &H2
    
    End If
    
    SendLen = 3
    RecvLen = 6
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
End Sub

Private Sub bStartBuzz_Click()

    Call ClearBuffers
    SendBuff(0) = &H28
    SendBuff(1) = &H1
    SendBuff(2) = &HFF
    SendLen = 3
    RecvLen = 6
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If

End Sub

Private Sub bStopBuzz_Click()

    Call ClearBuffers
    SendBuff(0) = &H28
    SendBuff(1) = &H1
    SendBuff(2) = &H0
    SendLen = 3
    RecvLen = 6
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If

End Sub

Private Sub Form_Load()

    Call InitMenu
    
End Sub

Private Sub InitMenu()

    ConnActive = False
    cbReader.Text = ""
    mMsg.Text = ""
    bConnect.Enabled = False
    bInit.Enabled = True
    bReset.Enabled = False
    bgetFW.Enabled = False
    frLED.Enabled = False
    cbBuzzLed1.Value = vbUnchecked
    cbBuzzLed2.Value = vbUnchecked
    cbBuzzLed3.Value = vbUnchecked
    cbBuzzLed4.Value = vbUnchecked
    cbBuzzLed5.Value = vbUnchecked
    cbBuzzLed6.Value = vbUnchecked
    cbBuzzLed7.Value = vbUnchecked
    cbBuzzLed8.Value = vbUnchecked
    tBuzzDur.Text = ""
    frBuzz.Enabled = False
    bGetLedSet.Enabled = False
    bSetLedSet.Enabled = False
    frBuzzState.Enabled = False
    cbGreen.Value = vbUnchecked
    cbRed.Value = vbUnchecked
    Call DisplayOut(0, 0, "Program ready")

End Sub

Private Sub DisplayOut(ByVal mType As Integer, ByVal msgCode As Long, ByVal PrintText As String)

  Select Case mType
  
    Case 0                   ' Notifications only
      mMsg.SelColor = &H4000
      
    Case 1                   ' Error Messages
      mMsg.SelColor = vbRed
      PrintText = GetScardErrMsg(retCode)
      
    Case 2
      mMsg.SelColor = vbBlack
      PrintText = "< " & PrintText
      
    Case 3
      mMsg.SelColor = vbBlack
      PrintText = "> " & PrintText
      
  End Select
  
  mMsg.SelText = PrintText & vbCrLf
  mMsg.SelStart = Len(mMsg.Text)
  mMsg.SelColor = vbBlack

End Sub

Private Sub AddButtons()

    bInit.Enabled = False
    bConnect.Enabled = True
    bReset.Enabled = True
    frLED.Enabled = True
    bClear.Enabled = True

End Sub

Private Sub ClearBuffers()

  Dim intIndx As Long
  
  For intIndx = 0 To 262
  
    RecvBuff(intIndx) = &H0
    SendBuff(intIndx) = &H0
    
  Next intIndx
  
End Sub

Private Function GetLEDState() As Long

    Call ClearBuffers
    SendBuff(0) = &H29
    SendBuff(1) = &H0
    SendLen = 2
    RecvLen = 6
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        GetLEDState = retCode
        Exit Function
    
    End If
    
    'interpret LED data
    Select Case RecvBuff(5)
    
        Case 0
            Call DisplayOut(3, 0, "Currently connected to SAM reader interface.")
            cbRed.Value = vbChecked
            cbGreen.Value = vbUnchecked
            
        Case 1
            Call DisplayOut(3, 0, "No PICC found.")
            cbRed.Value = vbChecked
            cbGreen.Value = vbUnchecked
        
        Case 2
            Call DisplayOut(3, 0, "PICC is present but not activated.")
            cbRed.Value = vbChecked
            cbGreen.Value = vbUnchecked
            
        Case 3
            Call DisplayOut(3, 0, "PICC is present and activated.")
            cbRed.Value = vbChecked
            cbGreen.Value = vbUnchecked
            
        Case 4
            Call DisplayOut(3, 0, "PICC is present and activated.")
            cbRed.Value = vbChecked
            cbGreen.Value = vbUnchecked
            
        Case 5
            Call DisplayOut(3, 0, "ICC is present and activated.")
            cbRed.Value = vbUnchecked
            cbGreen.Value = vbChecked
            
        Case 6
            Call DisplayOut(3, 0, "ICC is absent or not activated.")
            cbRed.Value = vbUnchecked
            cbGreen.Value = vbChecked
            
        Case 7
            Call DisplayOut(3, 0, "ICC is operating.")
            cbRed.Value = vbUnchecked
            cbGreen.Value = vbChecked
    
    End Select
    
    If (RecvBuff(5) And &H2) <> 0 Then
    
        cbGreen.Value = vbChecked
    
    Else
    
        cbGreen.Value = vbUnchecked
    
    End If
    
    If (RecvBuff(5) And &H1) Then
    
        cbRed.Value = vbChecked
        
    Else
    
        cbRed.Value = vbUnchecked
    
    End If
    
    GetLEDState = retCode

End Function

Private Function CallCardControl() As Long

Dim tmpStr As String
Dim intIndx As Integer

    'Display Apdu In
    tmpStr = "SCardControl: "
    
    For intIndx = 0 To SendLen - 1
        
        tmpStr = tmpStr & Format(Hex(SendBuff(intIndx)), "00") & " "
     
    Next intIndx
    
    Call DisplayOut(2, 0, tmpStr)
    
    retCode = SCardControl(hCard, _
                        IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND, _
                        SendBuff(0), _
                        SendLen, _
                        RecvBuff(0), _
                        RecvLen, _
                        nBytesRet)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Call DisplayOut(1, retCode, "")
    
    Else
    
        tmpStr = ""
        
        For intIndx = 0 To (RecvLen - 1)
        
            tmpStr = tmpStr & Format(Hex(RecvBuff(intIndx)), "00") & " "
        
        Next intIndx
        
        Call DisplayOut(3, 0, Trim(tmpStr))
    
    End If
    
    CallCardControl = retCode

End Function

Private Function GetBuzzLEDState() As Long

    Call ClearBuffers
    SendBuff(0) = &H21
    SendBuff(1) = &H0
    SendLen = 2
    RecvLen = 6
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        GetBuzzLEDState = retCode
        Exit Function
    
    End If
    
    'Interpret buzzer State Data
    If (RecvBuff(5) And &H1) <> 0 Then
    
        Call DisplayOut(3, 0, "ICC Activation Status LED is enabled.")
        cbBuzzLed1.Value = vbChecked
    
    Else
    
        Call DisplayOut(3, 0, "ICC Activation Status LED is disabled.")
        cbBuzzLed1.Value = vbUnchecked
    
    End If
    
    If (RecvBuff(5) And &H2) <> 0 Then
    
        Call DisplayOut(3, 0, "PICC Polling Status LED is enabled.")
        cbBuzzLed2.Value = vbChecked
        
    Else
    
        Call DisplayOut(3, 0, "PICC Polling Status LED is disabled.")
        cbBuzzLed2.Value = vbUnchecked
    
    End If
    
    If (RecvBuff(5) And &H4) <> 0 Then
    
        Call DisplayOut(3, 0, "PICC Activation Status Buzzer is enabled.")
        cbBuzzLed3.Value = vbChecked
        
    Else
    
        Call DisplayOut(3, 0, "PICC Activation Status Buzzer is disabled.")
        cbBuzzLed3.Value = vbUnchecked
    
    End If
    
    If (RecvBuff(5) And &H8) <> 0 Then
    
        Call DisplayOut(3, 0, "PICC PPS Status Buzzer is enabled.")
        cbBuzzLed4.Value = vbChecked
        
    Else
    
        Call DisplayOut(3, 0, "PICC PPS Status Buzzer is disabled.")
        cbBuzzLed4.Value = vbUnchecked
    
    End If
    
    If (RecvBuff(5) And &H10) <> 0 Then
    
        Call DisplayOut(3, 0, "Card Insertion and Removal Events Buzzer is enabled.")
        cbBuzzLed5.Value = vbChecked
        
    Else
    
        Call DisplayOut(3, 0, "Card Insertion and Removal Events Buzzer is disabled.")
        cbBuzzLed5.Value = vbUnchecked
    
    End If
    
    If (RecvBuff(5) And &H20) <> 0 Then
    
        Call DisplayOut(3, 0, "RC531 Reset Indication Buzzer is enabled.")
        cbBuzzLed6.Value = vbChecked
        
    Else
    
        Call DisplayOut(3, 0, "RC531 Reset Indication Buzzer is disabled.")
        cbBuzzLed6.Value = vbUnchecked
    
    End If
    
    If (RecvBuff(5) And &H40) <> 0 Then
    
        Call DisplayOut(3, 0, "Exclusive Mode Status Buzzer is enabled.")
        cbBuzzLed7.Value = vbChecked
        
    Else
    
        Call DisplayOut(3, 0, "Exclusive Mode Status Buzzer is disabled.")
        cbBuzzLed7.Value = vbUnchecked
    
    End If
    
    If (RecvBuff(5) And &H80) <> 0 Then
    
        Call DisplayOut(3, 0, "Card Operation Blinking LED is enabled.")
        cbBuzzLed8.Value = vbChecked
        
    Else
    
        Call DisplayOut(3, 0, "Card Operation Blinking LED is disabled.")
        cbBuzzLed8.Value = vbUnchecked
    
    End If
    
    GetBuzzLEDState = retCode

End Function

Private Sub tBuzzDur_GotFocus()

    tBuzzDur.SelStart = 0
    tBuzzDur.SelLength = Len(tBuzzDur.Text)
    
End Sub

Private Sub tBuzzDur_KeyPress(KeyAscii As Integer)

If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If


End Sub
