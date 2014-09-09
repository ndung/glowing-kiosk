VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form mainPolling 
   Caption         =   "Polling Sample"
   ClientHeight    =   8565
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   9870
   Icon            =   "PollingMain.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   8565
   ScaleWidth      =   9870
   StartUpPosition =   2  'CenterScreen
   Begin VB.Timer PollTimer 
      Enabled         =   0   'False
      Interval        =   500
      Left            =   9360
      Top             =   120
   End
   Begin MSComctlLib.StatusBar sbMsg 
      Align           =   2  'Align Bottom
      Height          =   375
      Left            =   0
      TabIndex        =   32
      Top             =   8190
      Width           =   9870
      _ExtentX        =   17410
      _ExtentY        =   661
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   4
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Alignment       =   1
            Object.Width           =   2999
            MinWidth        =   2999
         EndProperty
         BeginProperty Panel2 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Object.Width           =   5644
            MinWidth        =   5644
         EndProperty
         BeginProperty Panel3 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Alignment       =   1
            Object.Width           =   2999
            MinWidth        =   2999
         EndProperty
         BeginProperty Panel4 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Object.Width           =   5556
            MinWidth        =   5556
         EndProperty
      EndProperty
   End
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   7215
      Left            =   4440
      TabIndex        =   31
      Top             =   240
      Width           =   5295
      _ExtentX        =   9340
      _ExtentY        =   12726
      _Version        =   393217
      Enabled         =   -1  'True
      ScrollBars      =   2
      TextRTF         =   $"PollingMain.frx":17A2
   End
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   8040
      TabIndex        =   30
      Top             =   7680
      Width           =   1695
   End
   Begin VB.CommandButton bReset 
      Caption         =   "&Reset"
      Height          =   375
      Left            =   6240
      TabIndex        =   29
      Top             =   7680
      Width           =   1695
   End
   Begin VB.CommandButton bClear 
      Caption         =   "C&lear Screen"
      Height          =   375
      Left            =   4440
      TabIndex        =   28
      Top             =   7680
      Width           =   1695
   End
   Begin VB.CommandButton bAutoPoll 
      Caption         =   "Start &Auto Detection"
      Height          =   375
      Left            =   2160
      TabIndex        =   27
      Top             =   7680
      Width           =   1935
   End
   Begin VB.CommandButton bManPoll 
      Caption         =   "&Manual Card Detection"
      Height          =   375
      Left            =   120
      TabIndex        =   26
      Top             =   7680
      Width           =   1935
   End
   Begin VB.CommandButton bSetPollOpt 
      Caption         =   "&Set Polling Option"
      Height          =   375
      Left            =   2160
      TabIndex        =   25
      Top             =   6720
      Width           =   1935
   End
   Begin VB.CommandButton bReadPollOpt 
      Caption         =   "Read &Polling Option"
      Height          =   375
      Left            =   2160
      TabIndex        =   24
      Top             =   6240
      Width           =   1935
   End
   Begin VB.Frame frPICCInt 
      Caption         =   "Poll Interval"
      Height          =   1455
      Left            =   120
      TabIndex        =   19
      Top             =   6120
      Width           =   1455
      Begin VB.OptionButton opt25 
         Caption         =   "2.5 sec"
         Height          =   315
         Left            =   120
         TabIndex        =   23
         Top             =   1080
         Width           =   1095
      End
      Begin VB.OptionButton opt1 
         Caption         =   "1sec"
         Height          =   255
         Left            =   120
         TabIndex        =   22
         Top             =   840
         Width           =   1215
      End
      Begin VB.OptionButton opt500 
         Caption         =   "500 msec"
         Height          =   255
         Left            =   120
         TabIndex        =   21
         Top             =   600
         Width           =   1215
      End
      Begin VB.OptionButton opt250 
         Caption         =   "250 msec"
         Height          =   255
         Left            =   120
         TabIndex        =   20
         Top             =   360
         Width           =   1095
      End
   End
   Begin VB.Frame frPollOpt 
      Caption         =   "Contactless Polling Options"
      Height          =   1935
      Left            =   120
      TabIndex        =   12
      Top             =   4080
      Width           =   3975
      Begin VB.CheckBox cbPollOpt6 
         Caption         =   "Enforce ISO 14443A Part4"
         Height          =   255
         Left            =   120
         TabIndex        =   18
         Top             =   1560
         Width           =   3735
      End
      Begin VB.CheckBox cbPollOpt5 
         Caption         =   "Test Mode"
         Height          =   255
         Left            =   120
         TabIndex        =   17
         Top             =   1320
         Width           =   3615
      End
      Begin VB.CheckBox cbPollOpt4 
         Caption         =   "Activate the PICC when detected"
         Height          =   255
         Left            =   120
         TabIndex        =   16
         Top             =   1080
         Width           =   3615
      End
      Begin VB.CheckBox cbPollOpt3 
         Caption         =   "Turn off antenna field if PICC is inactive"
         Height          =   255
         Left            =   120
         TabIndex        =   15
         Top             =   840
         Width           =   3735
      End
      Begin VB.CheckBox cbPollOpt2 
         Caption         =   "Turn off antenna field if no PICC within range"
         Height          =   255
         Left            =   120
         TabIndex        =   14
         Top             =   600
         Width           =   3615
      End
      Begin VB.CheckBox cbPollOpt1 
         Caption         =   "Automatic PICC Polling"
         Height          =   255
         Left            =   120
         TabIndex        =   13
         Top             =   360
         Width           =   3495
      End
   End
   Begin VB.CommandButton bSetExMode 
      Caption         =   "&Set Mode Configuration"
      Height          =   375
      Left            =   2160
      TabIndex        =   11
      Top             =   3600
      Width           =   1935
   End
   Begin VB.CommandButton bGetExMode 
      Caption         =   "Read Current &Mode"
      Height          =   375
      Left            =   120
      TabIndex        =   7
      Top             =   3600
      Width           =   1935
   End
   Begin VB.Frame frCurrMode 
      Caption         =   "Current Mode"
      Height          =   1095
      Left            =   120
      TabIndex        =   10
      Top             =   2400
      Width           =   3975
      Begin VB.OptionButton optExNotActive 
         Caption         =   "Exclusive Mode is not Active"
         Height          =   255
         Left            =   120
         TabIndex        =   5
         Top             =   360
         Width           =   3375
      End
      Begin VB.OptionButton optExActive 
         Caption         =   "Exclusive Mode is Active"
         Height          =   255
         Left            =   120
         TabIndex        =   6
         Top             =   720
         Width           =   3495
      End
   End
   Begin VB.Frame frExMode 
      Caption         =   "Configuaration Setting"
      Height          =   1095
      Left            =   120
      TabIndex        =   9
      Top             =   1200
      Width           =   3975
      Begin VB.OptionButton optEither 
         Caption         =   "Either ICC or PICC interface can be activated"
         Height          =   255
         Left            =   120
         TabIndex        =   4
         Top             =   720
         Width           =   3495
      End
      Begin VB.OptionButton optBoth 
         Caption         =   "Both ICC_PICC interfaces can be activated"
         Height          =   255
         Left            =   120
         TabIndex        =   3
         Top             =   360
         Width           =   3495
      End
   End
   Begin VB.CommandButton bConnect 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   2160
      TabIndex        =   2
      Top             =   720
      Width           =   1935
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initialize"
      Height          =   375
      Left            =   120
      TabIndex        =   1
      Top             =   720
      Width           =   1935
   End
   Begin VB.ComboBox cbReader 
      Height          =   315
      Left            =   1200
      TabIndex        =   0
      Top             =   240
      Width           =   2895
   End
   Begin VB.Label Label1 
      BackStyle       =   0  'Transparent
      Caption         =   "Select Reader"
      Height          =   495
      Left            =   120
      TabIndex        =   8
      Top             =   360
      Width           =   1215
   End
End
Attribute VB_Name = "mainPolling"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              PollingMain.frm
'
'  Description:       This sample program outlines the steps on how to
'                     execute card detection polling functions using ACR128
'
'  Author:            M.J.E.C. Castillo
'
'  Date:              June 2, 2008
'
'  Revision Trail:   (Date/Author/Description)
'
'======================================================================

Option Explicit

Dim retCode, Protocol, hContext, hCard, ReaderCount, pollCase As Long
Dim sReaderList As String * 256
Dim sReaderGroup As String
Dim ConnActive, autoDet, dualPoll As Boolean
Dim ioRequest As SCARD_IO_REQUEST
Dim SendLen, RecvLen, nBytesRet As Long
Dim SendBuff(0 To 262) As Byte
Dim RecvBuff(0 To 262) As Byte
Dim RdrState As SCARD_READERSTATE

Const INVALID_SW1SW2 = -450

Private Sub bAutoPoll_Click()

Dim intIndx As Integer

   ' pollCase legend
   ' 1 = Both ICC and PICC can poll, but only one at a time
   ' 2 = Only ICC can poll
   ' 3 = Both reader can be polled
   
   If autoDet Then
   
        autoDet = False
        bAutoPoll.Caption = "Start &Auto Detection"
        PollTimer.Enabled = False
        Call DisplayOut(5, 0, "")
        Call DisplayOut(6, 0, "")
        Exit Sub
   
   End If
   
   'Always use a valid connection for Card Control commands
   retCode = CallCardConnect(1)
   
   If retCode <> SCARD_S_SUCCESS Then
    
        bAutoPoll.Caption = "Start &Auto Detection"
        autoDet = False
        Exit Sub
    
   End If
   
   GetExMode (0)
   
   'Either ICC or PICC can be polled at any one time
   If RecvBuff(5) <> 0 Then
   
        Call ReadPollingOption(0)
    
        'Auto PICC polling is enabled
        If (RecvBuff(5) And &H1) <> 0 Then
    
            'Either ICC and PICC can be polled
            pollCase = 1
        
        Else
    
            'Only ICC can be polled
            pollCase = 2
        
        End If
   
   'Both ICC and PICC can be enabled at the same time
   Else
   
        Call ReadPollingOption(0)
    
        'Auto PICC polling is enabled
        If (RecvBuff(5) And &H1) <> 0 Then
    
            'Both ICC and PICC can be polled
            pollCase = 3
        
        Else
    
            'Only ICC can be polled
            pollCase = 2
        
        End If
   
   End If
   
   Select Case pollCase
   
        Case 1
            Call DisplayOut(0, 0, "Either reader can detect cards, but not both.")
    
        Case 2
            Call DisplayOut(0, 0, "Automatic PICC polling is disabled, only ICC can detect card.")
        
        Case 3
            Call DisplayOut(0, 0, "Both ICC and PICC readers can automatically detect card.")
   
   End Select
   
   autoDet = True
   PollTimer.Enabled = True
   bAutoPoll.Caption = "End &Auto Detection"

End Sub

Private Sub bClear_Click()

    mMsg.Text = ""

End Sub

Private Sub bConnect_Click()

    Call CallCardConnect(1)
    
    If retCode <> SCARD_S_SUCCESS Then
        
        Exit Sub
        
    End If
    
    ConnActive = True
    Call AddButtons(1)

End Sub

Private Sub bGetExMode_Click()

    Call GetExMode(1)
    
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
    Call AddButtons(0)

    'Look for ACR128 SAM and make it the default reader in the combobox
    For intIndx = 0 To cbReader.ListCount - 1
    
        cbReader.ListIndex = intIndx
    
        If InStr(cbReader.Text, "ACR128U SAM") > 0 Then
        
            Exit For
       
        End If
     
    Next intIndx

End Sub

Private Sub bManPoll_Click()

Dim intIndx As Integer

    'Always use a valid connection for Card Control commands
    retCode = CallCardConnect(1)
    
    If retCode <> SCARD_S_SUCCESS Then
        
        Exit Sub
        
    End If
    
    Call ReadPollingOption(0)
    
    If (RecvBuff(5) And &H1) <> 0 Then
    
        Call DisplayOut(0, 0, "Turn off automatic PICC polling in the device before using this function.")
    
    Else
    
        Call ClearBuffers
        SendBuff(0) = &H22
        SendBuff(1) = &H1
        SendBuff(2) = &HA
        SendLen = 3
        RecvLen = 6
        retCode = CallCardControl
        
        If retCode <> SCARD_S_SUCCESS Then
            
            Exit Sub
        
        End If
        
        If (RecvBuff(5) And &H1) <> 0 Then
            
            Call DisplayOut(6, 0, "No card within range.")
            
        Else
            
            Call DisplayOut(6, 0, "Card is detected.")
            
        End If
    
    End If
    

End Sub

Private Sub bQuit_Click()

    If ConnActive Then
  
        retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
        ConnActive = False
    
    End If
  
    retCode = SCardReleaseContext(hContext)
    Unload Me

End Sub

Private Sub bReadPollOpt_Click()

    Call ReadPollingOption(1)

End Sub

Private Sub bReset_Click()

    If ConnActive Then
    
        retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    
    End If
  
    retCode = SCardReleaseContext(hCard)
    Call InitMenu

End Sub

Private Sub bSetExMode_Click()

    Call ClearBuffers
    SendBuff(0) = &H2B
    SendBuff(1) = &H1
    
    If optBoth.Value = True Then
    
        SendBuff(2) = &H0
    
    Else
        
        SendBuff(2) = &H1
    
    End If
    
    SendLen = 3
    RecvLen = 7
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
        
        Exit Sub
        
    End If

End Sub

Private Sub bSetPollOpt_Click()

    Call ClearBuffers
    SendBuff(0) = &H23
    SendBuff(1) = &H1
    
    If cbPollOpt1.Value = vbChecked Then
    
        SendBuff(2) = SendBuff(2) Or &H1
    
    End If
    
    If cbPollOpt2.Value = vbChecked Then
    
        SendBuff(2) = SendBuff(2) Or &H2
    
    End If
    
    If cbPollOpt3.Value = vbChecked Then
    
        SendBuff(2) = SendBuff(2) Or &H4
    
    End If
    
    If cbPollOpt4.Value = vbChecked Then
    
        SendBuff(2) = SendBuff(2) Or &H8
    
    End If
    
    If opt500.Value = True Then
    
        SendBuff(2) = SendBuff(2) Or &H10
    
    End If
    
    If opt1.Value = True Then
    
        SendBuff(2) = SendBuff(2) Or &H20
    
    End If
    
    If opt25.Value = True Then
    
        SendBuff(2) = SendBuff(2) Or &H10
        SendBuff(2) = SendBuff(2) Or &H20
    
    End If
    
    If cbPollOpt5.Value = vbChecked Then
    
        SendBuff(2) = SendBuff(2) Or &H40
    
    End If
    
    If cbPollOpt6.Value = vbChecked Then
    
        SendBuff(2) = SendBuff(2) Or &H80
    
    End If
    
    SendLen = 3
    RecvLen = 7
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
    autoDet = False
    dualPoll = False
    Call DisplayOut(0, 0, "Program ready")
    bConnect.Enabled = False
    bReset.Enabled = False
    bInit.Enabled = True
    frExMode.Enabled = False
    frCurrMode.Enabled = False
    bSetExMode.Enabled = False
    bGetExMode.Enabled = False
    frPollOpt.Enabled = False
    cbPollOpt1.Value = vbUnchecked
    cbPollOpt2.Value = vbUnchecked
    cbPollOpt3.Value = vbUnchecked
    cbPollOpt4.Value = vbUnchecked
    cbPollOpt5.Value = vbUnchecked
    cbPollOpt6.Value = vbUnchecked
    frPICCInt.Enabled = False
    bReadPollOpt.Enabled = False
    bSetPollOpt.Enabled = False
    bManPoll.Enabled = False
    bAutoPoll.Enabled = False
    sbMsg.Panels(1) = "ICC Reader Status"
    sbMsg.Panels(3) = "PICC Reader Status"

End Sub

Private Sub DisplayOut(ByVal mType As Integer, ByVal msgCode As Long, ByVal PrintText As String)

  Select Case mType
  
    ' Notifications only
    Case 0
      mMsg.SelColor = &H4000
      mMsg.SelText = PrintText & vbCrLf
      
    ' Error Messages
    Case 1
      mMsg.SelColor = vbRed
      PrintText = GetScardErrMsg(retCode)
      mMsg.SelText = PrintText & vbCrLf
      
    ' input data
    Case 2
      mMsg.SelColor = vbBlack
      PrintText = "< " & PrintText
      mMsg.SelText = PrintText & vbCrLf
      
    ' output data
    Case 3
      mMsg.SelColor = vbBlack
      PrintText = "> " & PrintText
      mMsg.SelText = PrintText & vbCrLf
      
    'for ACOS1 error
    Case 4
      mMsg.SelColor = vbRed
      mMsg.SelText = PrintText & vbCrLf
      
    Case 5
      sbMsg.Panels(2) = PrintText
      
    Case 6
      sbMsg.Panels(4) = PrintText
      
  End Select

  mMsg.SelStart = Len(mMsg.Text)
  mMsg.SelColor = vbBlack
 

End Sub

Private Sub AddButtons(ByVal reqType As Integer)

    Select Case reqType
    
        Case 0
            bInit.Enabled = False
            bConnect.Enabled = True
            bReset.Enabled = True
            
        Case 1
            frExMode.Enabled = True
            frCurrMode.Enabled = True
            bSetExMode.Enabled = True
            bGetExMode.Enabled = True
            frPollOpt.Enabled = True
            frPICCInt.Enabled = True
            bReadPollOpt.Enabled = True
            bSetPollOpt.Enabled = True
            bManPoll.Enabled = True
            bAutoPoll.Enabled = True
        
    End Select

End Sub

Private Function CallCardConnect(reqType As Integer) As Long

Dim intIndx As Integer

    If ConnActive Then
    
        retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    
    End If
    
    'Shared Connection
    retCode = SCardConnect(hContext, _
                        cbReader.Text, _
                        SCARD_SHARE_SHARED, _
                        SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1, _
                        hCard, _
                        Protocol)
                        
    If retCode <> SCARD_S_SUCCESS Then
        
        'Connect to SAM Reader
        If reqType = 1 Then
        
            'check if ACR128 SAM is used and use Direct Mode if SAM is not detected
            intIndx = 0
            cbReader.ListIndex = intIndx
            
            While InStr(cbReader.Text, "ACR128U SAM") = 0
                
                If intIndx = cbReader.ListCount Then
                
                    Call DisplayOut(0, 0, "Cannot find ACR128 SAM reader.")
                
                End If
                
                intIndx = intIndx + 1
                cbReader.ListIndex = intIndx
                
            Wend
            
            'Direct Connection
            retCode = SCardConnect(hContext, _
                        cbReader.Text, _
                        SCARD_SHARE_DIRECT, _
                        0, _
                        hCard, _
                        Protocol)
            
            If retCode <> SCARD_S_SUCCESS Then
            
                Call DisplayOut(1, retCode, "")
                ConnActive = False
                CallCardConnect = retCode
                Exit Function
            
            Else
            
                Call DisplayOut(0, 0, "Successful connection to " & cbReader.Text)
            
            End If
        
        Else
        
            Call DisplayOut(1, retCode, "")
            ConnActive = False
            CallCardConnect = retCode
            Exit Function
        
        End If
    
    Else
    
        Call DisplayOut(0, 0, "Successful connection to " & cbReader.Text)
        CallCardConnect = retCode
    
    End If

End Function

Private Sub ClearBuffers()

  Dim intIndx As Long
  
  For intIndx = 0 To 262
  
    RecvBuff(intIndx) = &H0
    SendBuff(intIndx) = &H0
    
  Next intIndx
  
End Sub

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
        
        For intIndx = 0 To RecvLen - 1
        
            tmpStr = tmpStr & Format(Hex(RecvBuff(intIndx)), "00") & " "
        
        Next intIndx
        
        Call DisplayOut(3, 0, Trim(tmpStr))
    
    End If
    
    CallCardControl = retCode

End Function

Private Sub GetExMode(reqType As Integer)

Dim tmpStr As String
Dim intIndx As Integer

    Call ClearBuffers
    SendBuff(0) = &H2B
    SendBuff(1) = &H0
    SendLen = 2
    RecvLen = 7
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    'Interpret Configuration Setting and Current Mode
    tmpStr = ""
    
    For intIndx = 0 To 4
    
        tmpStr = tmpStr & Format(Hex(RecvBuff(intIndx)), "00")
    
    Next intIndx

    If tmpStr = "E100000002" Then
    
        If reqType = 1 Then
        
            If RecvBuff(5) = 0 Then
            
                optBoth.Value = True
            
            Else
                
                optEither.Value = True
            
            End If
            
            If RecvBuff(6) = 0 Then
            
                optExNotActive.Value = True
                
            Else
            
                optExActive.Value = True
            
            End If
        
        End If
    
    Else
    
        Call DisplayOut(4, 0, "Wrong return values from device.")
    
    End If
    
End Sub

Private Sub ReadPollingOption(reqType As Integer)

    Call ClearBuffers
    SendBuff(0) = &H23
    SendBuff(1) = &H0
    SendLen = 2
    RecvLen = 6
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
        
        Exit Sub
        
    End If
    
    If reqType = 1 Then
    
        'Interpret PICC Polling Setting
        If (RecvBuff(5) And &H1) <> 0 Then
        
            Call DisplayOut(3, 0, "Automatic PICC polling is enabled.")
            cbPollOpt1.Value = vbChecked
        
        Else
            
            Call DisplayOut(3, 0, "Automatic PICC polling is disabled.")
            cbPollOpt1.Value = vbUnchecked
        
        End If
        
        If (RecvBuff(5) And &H2) <> 0 Then
        
            Call DisplayOut(3, 0, "Antenna off when no PICC found is enabled.")
            cbPollOpt2.Value = vbChecked
        
        Else
            
            Call DisplayOut(3, 0, "Antenna off when no PICC found is disabled.")
            cbPollOpt2.Value = vbUnchecked
        
        End If
        
        If (RecvBuff(5) And &H4) <> 0 Then
        
            Call DisplayOut(3, 0, "Antenna off when PICC is inactive is enabled.")
            cbPollOpt3.Value = vbChecked
        
        Else
            
            Call DisplayOut(3, 0, "Antenna off when PICC is inactive is disabled.")
            cbPollOpt3.Value = vbUnchecked
        
        End If
        
        If (RecvBuff(5) And &H8) <> 0 Then
        
            Call DisplayOut(3, 0, "Activate PICC when detected is enabled.")
            cbPollOpt4.Value = vbChecked
        
        Else
            
            Call DisplayOut(3, 0, "Activate PICC when detected is disabled.")
            cbPollOpt4.Value = vbUnchecked
        
        End If
        
        If (((RecvBuff(5) And &H10) = 0) And ((RecvBuff(5) And &H20) = 0)) Then
        
            Call DisplayOut(3, 0, "Poll interval is 250 msec.")
            opt250.Value = True
        
        End If
        
        If (((RecvBuff(5) And &H10) <> 0) And ((RecvBuff(5) And &H20) = 0)) Then
        
            Call DisplayOut(3, 0, "Poll interval is 500 msec.")
            opt500.Value = True
        
        End If
        
        If (((RecvBuff(5) And &H10) = 0) And ((RecvBuff(5) And &H20) <> 0)) Then
        
            Call DisplayOut(3, 0, "Poll interval is 1 sec.")
            opt1.Value = True
        
        End If
        
        If (((RecvBuff(5) And &H10) <> 0) And ((RecvBuff(5) And &H20) <> 0)) Then
        
            Call DisplayOut(3, 0, "Poll interval is 2.5 sec.")
            opt25.Value = True
        
        End If
        
        If (RecvBuff(5) And &H40) <> 0 Then
        
            Call DisplayOut(3, 0, "Test Mode is enabled.")
            cbPollOpt5.Value = vbChecked
            
        Else
        
            Call DisplayOut(3, 0, "Test Mode is disabled.")
            cbPollOpt5.Value = vbUnchecked
        
        End If
        
        If (RecvBuff(5) And &H80) <> 0 Then
        
            Call DisplayOut(3, 0, "ISO14443A Part4 is enforced.")
            cbPollOpt6.Value = vbChecked
            
        Else
        
            Call DisplayOut(3, 0, "ISO14443A Part4 is not enforced.")
            cbPollOpt6.Value = vbUnchecked
        
        End If
    
    End If

End Sub

Private Sub PollTimer_Timer()

Dim intIndx As Integer

    Select Case pollCase
          
      'Automatic PICC polling is disabled, only ICC can detect card
        Case 2
            'Connect to ICC reader
            Call DisplayOut(6, 0, "Auto Polling is disabled.")
            intIndx = 0
            cbReader.ListIndex = intIndx
            
            While InStr(cbReader.Text, "ACR128U ICC") = 0
            
                If intIndx = cbReader.ListCount Then
                
                    Call DisplayOut(0, 0, "Cannot find ACR128 ICC reader.")
                    PollTimer.Enabled = False
                
                End If
                
                intIndx = intIndx + 1
                cbReader.ListIndex = intIndx
            
            Wend
            
            RdrState.RdrName = cbReader.Text
            retCode = SCardGetStatusChange(hContext, _
                                           0, _
                                           RdrState, _
                                           1)
                                           
            If retCode <> SCARD_S_SUCCESS Then
            
                Call DisplayOut(1, retCode, "")
                PollTimer.Enabled = False
                Exit Sub
                
            End If
            
            If (RdrState.RdrEventState And SCARD_STATE_PRESENT) <> 0 Then
            
                Call DisplayOut(5, 0, "Card is inserted.")
                
            Else
            
                Call DisplayOut(5, 0, "Card is removed.")
            
            End If
            
            'Both ICC and PICC readers can automatically detect card
            Case 1, 3
                'Attempt to poll ICC reader
                intIndx = 0
                cbReader.ListIndex = intIndx
                
                While InStr(cbReader.Text, "ACR128U ICC") = 0
                
                    If intIndx = cbReader.ListCount Then
                    
                        Call DisplayOut(0, 0, "Cannot find ACR128 ICC reader.")
                        PollTimer.Enabled = False
                    
                    End If
                
                    intIndx = intIndx + 1
                    cbReader.ListIndex = intIndx
                    
                Wend
                
                RdrState.RdrName = cbReader.Text
                retCode = SCardGetStatusChange(hContext, _
                                           0, _
                                           RdrState, _
                                           1)
                
                If retCode = SCARD_S_SUCCESS Then
                
                    If (RdrState.RdrEventState And SCARD_STATE_PRESENT) <> 0 Then
                    
                        Call DisplayOut(5, 0, "Card is inserted.")
                    
                    Else
                    
                        Call DisplayOut(5, 0, "Card is removed.")
                    
                    End If
                
                End If
                
                'Attempt to poll PICC reader
                intIndx = 0
                cbReader.ListIndex = intIndx
                
                While InStr(cbReader.Text, "ACR128U PICC") = 0
                
                    If intIndx = cbReader.ListCount Then
                    
                        Call DisplayOut(0, 0, "Cannot find ACR128 PICC reader.")
                        PollTimer.Enabled = False
                    
                    End If
                
                    intIndx = intIndx + 1
                    cbReader.ListIndex = intIndx
                    
                Wend
                
                RdrState.RdrName = cbReader.Text
                retCode = SCardGetStatusChange(hContext, _
                                           0, _
                                           RdrState, _
                                           1)
                
                If retCode = SCARD_S_SUCCESS Then
                
                    If (RdrState.RdrEventState And SCARD_STATE_PRESENT) <> 0 Then
                    
                        Call DisplayOut(6, 0, "Card is detected.")
                    
                    Else
                    
                        Call DisplayOut(6, 0, "No card within range.")
                    
                    End If
                
                End If

    End Select
    
End Sub
