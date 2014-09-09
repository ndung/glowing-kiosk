VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form SimplePCSC 
   Caption         =   "Simple PCSC"
   ClientHeight    =   9810
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6540
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   9
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "SimplePCSC.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   9810
   ScaleWidth      =   6540
   StartUpPosition =   2  'CenterScreen
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   3180
      Left            =   330
      TabIndex        =   0
      Top             =   6345
      Width           =   5970
      _ExtentX        =   10530
      _ExtentY        =   5609
      _Version        =   393217
      Enabled         =   -1  'True
      ScrollBars      =   2
      TextRTF         =   $"SimplePCSC.frx":17A2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
   End
   Begin VB.Frame Frame1 
      Height          =   9615
      Left            =   90
      TabIndex        =   2
      Top             =   90
      Width           =   6360
      Begin VB.Frame fApdu 
         Caption         =   "APDU Input"
         BeginProperty Font 
            Name            =   "Tahoma"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   1215
         Left            =   3000
         TabIndex        =   17
         Top             =   3720
         Width           =   3135
         Begin VB.TextBox tDataIn 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   9
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   375
            Left            =   120
            TabIndex        =   19
            Top             =   360
            Width           =   2895
         End
         Begin VB.Label Label4 
            Caption         =   "(Use HEX values only)"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   9
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   255
            Left            =   120
            TabIndex        =   18
            Top             =   720
            Width           =   2895
         End
      End
      Begin VB.CommandButton bClear 
         Caption         =   "Clear Output Window"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   510
         Left            =   3090
         TabIndex        =   15
         Top             =   5640
         Width           =   2550
      End
      Begin VB.CommandButton bRelease 
         Caption         =   "SCardReleaseContext"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   510
         Left            =   225
         TabIndex        =   14
         Top             =   5640
         Width           =   2550
      End
      Begin VB.CommandButton bEnd 
         Caption         =   "SCardEndTransaction"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   510
         Left            =   240
         TabIndex        =   13
         Top             =   4470
         Width           =   2550
      End
      Begin VB.CommandButton bTransmit 
         Caption         =   "SCardTransmit"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   510
         Left            =   240
         TabIndex        =   12
         Top             =   3885
         Width           =   2550
      End
      Begin VB.CommandButton bStatus 
         Caption         =   "SCardStatus"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   510
         Left            =   255
         TabIndex        =   11
         Top             =   3300
         Width           =   2550
      End
      Begin VB.CommandButton bBeginTran 
         Caption         =   "SCardBeginTransaction"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   510
         Left            =   255
         TabIndex        =   10
         Top             =   2700
         Width           =   2550
      End
      Begin VB.CommandButton bInit 
         Caption         =   "SCardEstablishContext"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   510
         Left            =   255
         TabIndex        =   7
         Top             =   900
         Width           =   2550
      End
      Begin VB.CommandButton bConnect 
         Caption         =   "SCardConnect"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   510
         Left            =   255
         TabIndex        =   6
         Top             =   2100
         Width           =   2550
      End
      Begin VB.CommandButton bDisconnect 
         Caption         =   "SCardDisconnect"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   510
         Left            =   240
         TabIndex        =   5
         Top             =   5055
         Width           =   2550
      End
      Begin VB.CommandButton bQuit 
         Caption         =   "&Quit"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   510
         Left            =   3090
         TabIndex        =   4
         Top             =   5055
         Width           =   2550
      End
      Begin VB.CommandButton bList 
         Caption         =   "SCardListReaders"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   510
         Left            =   255
         TabIndex        =   3
         Top             =   1500
         Width           =   2550
      End
      Begin VB.Frame Frame2 
         Caption         =   "Port"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   810
         Left            =   3135
         TabIndex        =   8
         Top             =   915
         Width           =   2970
         Begin VB.ComboBox cbReader 
            BeginProperty Font 
               Name            =   "Arial"
               Size            =   9
               Charset         =   0
               Weight          =   700
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   345
            Left            =   225
            TabIndex        =   9
            Top             =   285
            Width           =   2535
         End
      End
      Begin VB.Label Label1 
         Caption         =   "This is a simple PCSC application."
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   330
         Left            =   210
         TabIndex        =   16
         Top             =   345
         Width           =   5115
      End
   End
   Begin VB.Label Label2 
      Caption         =   "String Value of Data"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   240
      TabIndex        =   1
      Top             =   5280
      Width           =   2055
   End
End
Attribute VB_Name = "SimplePCSC"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'///////////////////////////////////////////////////////////////////////////
'
' Company  : ADVANCED CARD SYSTEMS, LTD.
'
' Name     : SimplePCSC
'
' Author   : Fernando G. Robles
'
'  Date    : 08/17/2005
'
' Revision Trail: Date/Author/Description
' 06September2005/J.I.R. Mission/ Allows APDU input
'//////////////////////////////////////////////////////////////////////////////



Option Explicit

Dim retCode, Protocol, hContext, hCard, ReaderCount As Long
Dim sReaderList As String * 256
Dim sReaderGroup As String
Dim diFlag As Boolean
Dim ioRequest As SCARD_IO_REQUEST
Dim SendLen, RecvLen As Long
Dim SendBuff(0 To 262) As Byte
Dim RecvBuff(0 To 262) As Byte

Const INVALID_SW1SW2 = -450

Private Sub ClearBuffers()

  Dim indx As Long
  
  For indx = 0 To 262
    RecvBuff(indx) = &H0
    SendBuff(indx) = &H0
  Next indx
  
End Sub
Private Sub DisableAPDUInput()
  
  tDataIn.Text = ""
  fApdu.Enabled = False
  
End Sub

Private Sub InitMenu()

  cbReader.Clear
  bInit.Enabled = True
  bList.Enabled = False
  bConnect.Enabled = False
  bBeginTran.Enabled = False
  bStatus.Enabled = False
  bTransmit.Enabled = False
  bEnd.Enabled = False
  bDisconnect.Enabled = False
  bRelease.Enabled = False
  mMsg.Text = ""
  Call DisplayOut(0, 0, "Program ready")
  Call DisableAPDUInput
  
End Sub

Private Sub DisplayOut(ByVal mType As Integer, ByVal msgCode As Long, ByVal PrintText As String)

  Select Case mType
    Case 0                                  ' Notifications only
      mMsg.SelColor = &H4000
    Case 1                                  ' Error Messages
      mMsg.SelColor = vbRed
      PrintText = GetScardErrMsg(retCode)
    Case 2                                  ' Input data
      mMsg.SelColor = vbBlack
      PrintText = "< " & PrintText
    Case 3                                  ' Output data
      mMsg.SelColor = vbBlack
      PrintText = "> " & PrintText
    Case 4                                  ' Critical Errors
      mMsg.SelColor = vbRed
  End Select
  
  mMsg.SelText = PrintText & vbCrLf
  mMsg.SelStart = Len(mMsg.Text)
  mMsg.SelColor = vbBlack
 

End Sub
Private Function TrimInput(ByVal textInp As String) As String
  Dim tmpStr As String
  Dim indx As Integer
  
  tmpStr = ""
  textInp = Trim(textInp)
  
  For indx = 1 To Len(textInp)
    If Mid(textInp, indx, 1) <> Chr(32) Then
      tmpStr = tmpStr & Mid(textInp, indx, 1)
    End If
  Next indx
  diFlag = False
  If (Len(tmpStr) Mod 2 <> 0) Then
    diFlag = True
  End If
  
  TrimInput = tmpStr
  
End Function

Private Function SendAPDU() As Long

  Dim indx As Integer
  Dim tmpStr As String

  ioRequest.dwProtocol = Protocol
  ioRequest.cbPciLength = Len(ioRequest)
  tmpStr = ""
  RecvLen = 262
  
  retCode = SCardTransmit(hCard, _
                          ioRequest, _
                          SendBuff(0), _
                          SendLen, _
                          ioRequest, _
                          RecvBuff(0), _
                          RecvLen)
  If retCode <> SCARD_S_SUCCESS Then
    Call DisplayOut(1, retCode, "")
    SendAPDU = retCode
    Exit Function
  End If
  
  SendAPDU = retCode
  
End Function


Private Sub bBeginTran_Click()
    retCode = SCardBeginTransaction(hCard)
    If retCode <> SCARD_S_SUCCESS Then
        Call DisplayOut(1, retCode, "")
        Exit Sub
    Else
        Call DisplayOut(0, 0, "SCardBeginTransaction...OK")
    
    End If
    fApdu.Enabled = True
    tDataIn.SetFocus
    bBeginTran.Enabled = False
    bStatus.Enabled = True
    bTransmit.Enabled = True
    bEnd.Enabled = True
    bDisconnect.Enabled = False
    
End Sub

Private Sub bClear_Click()
    mMsg.Text = ""
End Sub

Private Sub bConnect_Click()

 
  'Connect to selected reader using hContext handle
  'and obtain valid hCard handle
  retCode = SCardConnect(hContext, _
                        cbReader.Text, _
                        SCARD_SHARE_EXCLUSIVE, _
                        SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1, _
                        hCard, _
                        Protocol)
  If retCode <> SCARD_S_SUCCESS Then
    Call DisplayOut(1, retCode, "")
    Exit Sub
  Else
    Call DisplayOut(0, 0, "SCardConnect...OK")
    
  End If

  
  bList.Enabled = False
  bConnect.Enabled = False
  bBeginTran.Enabled = True
  bDisconnect.Enabled = True
  bRelease.Enabled = False
  

End Sub

Private Sub bFormat_Click()

End Sub

Private Sub bDisconnect_Click()
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    
    If retCode <> SCARD_S_SUCCESS Then
        Call DisplayOut(1, retCode, "")
        Exit Sub
    Else
        Call DisplayOut(0, 0, "SCardDisconnect...OK")
    
    End If
    
  bList.Enabled = True
  bConnect.Enabled = True
  bBeginTran.Enabled = False
  bDisconnect.Enabled = False
  bRelease.Enabled = True
    
End Sub

Private Sub bEnd_Click()
    retCode = SCardEndTransaction(hCard, SCARD_LEAVE_CARD)
    If retCode <> SCARD_S_SUCCESS Then
        Call DisplayOut(1, retCode, "")
        Exit Sub
    Else
        Call DisplayOut(0, 0, "SCardEndTransaction...OK")
    
    End If
  
    Call DisableAPDUInput
    
    bBeginTran.Enabled = True
    bStatus.Enabled = False
    bTransmit.Enabled = False
    bEnd.Enabled = False
    bDisconnect.Enabled = True
    
End Sub

Private Sub bInit_Click()

  sReaderList = String(255, vbNullChar)
  ReaderCount = 255
     
  'Establish context and obtain hContext handle
  retCode = SCardEstablishContext(SCARD_SCOPE_USER, 0, 0, hContext)
  If retCode <> SCARD_S_SUCCESS Then
    Call DisplayOut(1, retCode, "")
    Exit Sub
  Else
    Call DisplayOut(0, 0, "SCardEstablishContext... OK")
    
  End If
  
  bInit.Enabled = False
  bList.Enabled = True
  bRelease.Enabled = True
  
End Sub

Private Sub bList_Click()
'List PC/SC card readers installed in the system
  retCode = SCardListReaders(hContext, sReaderGroup, sReaderList, ReaderCount)
  If retCode <> SCARD_S_SUCCESS Then
    Call DisplayOut(1, retCode, "")
    Exit Sub
  Else
    Call DisplayOut(0, 0, "SCardListReaders... OK")
    
  End If
  Call LoadListToControl(cbReader, sReaderList)
  cbReader.ListIndex = 0
  
  bConnect.Enabled = True
  
End Sub

Private Sub bQuit_Click()

  retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
  retCode = SCardReleaseContext(hContext)
  Unload Me

End Sub

Private Sub bRelease_Click()
    
    retCode = SCardReleaseContext(hContext)
    
    If retCode <> SCARD_S_SUCCESS Then
        Call DisplayOut(1, retCode, "")
        Exit Sub
    Else
        Call DisplayOut(0, 0, "SCardReleaseContext...OK")
    
    End If
  
  bInit.Enabled = True
  bList.Enabled = False
  bConnect.Enabled = False
  bRelease.Enabled = False
  cbReader.Clear
    
End Sub

Private Sub bStatus_Click()
  Dim ReaderLen, dwState, ATRLen, indx As Long
  Dim ATRVal(0 To 31) As Byte
  Dim tmpStr As String
  ATRLen = 32
    retCode = SCardStatus(hCard, _
                         cbReader.Text, _
                         ReaderLen, _
                         dwState, _
                         Protocol, _
                         ATRVal(0), _
                         ATRLen)
  If retCode <> SCARD_S_SUCCESS Then
    Call DisplayOut(1, retCode, "")
    Exit Sub
  Else
    Call DisplayOut(0, 0, "SCardStatus...OK")
  
  End If

  'Format ATRVal returned and display string as ATR value
  tmpStr = ""
  For indx = 0 To ATRLen - 1
    tmpStr = tmpStr & Format(Hex(ATRVal(indx)), "00") & " "
  Next indx
  Call DisplayOut(3, 0, tmpStr)

  'Interpret dwstate returned and display as state
  Select Case dwState
    Case 0
      tmpStr = "SCARD_UNKNOWN"
    Case 1
      tmpStr = "SCARD_ABSENT"
    Case 2
        tmpStr = "SCARD_PRESENT"
    Case 3
        tmpStr = "SCARD_SWALLOWED"
    Case 4
        tmpStr = "SCARD_POWERED"
    Case 5
        tmpStr = "SCARD_NEGOTIABLE"
    Case 6
        tmpStr = "SCARD_SPECIFIC"
  End Select
  Call DisplayOut(3, 0, "Reader State: " & tmpStr)

  'Interpret dwActProtocol returned and display as active protocol
  Select Case Protocol
    Case 1
      tmpStr = "T=0"
    Case 2
      tmpStr = "T=1"
  End Select
  Call DisplayOut(3, 0, "Active protocol: " & tmpStr)
 
End Sub

Private Sub bTransmit_Click()
  Dim indx As Integer
  Dim tmpStr As String
  
  If tDataIn = "" Then
    Call DisplayOut(4, 0, "No data input")
    Exit Sub
  End If
  
  tmpStr = TrimInput(tDataIn.Text)
  If Len(tmpStr) < 10 Then
    Call DisplayOut(4, 0, "Insufficient data input")
    Exit Sub
  End If
  If diFlag Then
    Call DisplayOut(4, 0, "Invalid data input, uneven number of characters")
    tDataIn.SetFocus
    Exit Sub
  End If
  
  Call ClearBuffers
  For indx = 0 To 4
    SendBuff(indx) = CInt("&H" & Mid(tmpStr, indx * 2 + 1, 2))
  Next indx
  
  ' if APDU length < 6 then P3 is Le
  If Len(tmpStr) < 12 Then
    For indx = 0 To 4
      SendBuff(indx) = CInt("&H" & Mid(tmpStr, indx * 2 + 1, 2))
    Next indx
    SendLen = &H5
    RecvLen = SendBuff(4) + 2
    tmpStr = ""
    For indx = 0 To 4
      tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
    Next indx
    Call DisplayOut(2, 0, tmpStr)
    retCode = SendAPDU()
    If retCode = SCARD_S_SUCCESS Then
      tmpStr = ""
      For indx = 0 To RecvLen - 1
        tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
      Next indx
      Call DisplayOut(3, 0, tmpStr)
    End If
  Else
    For indx = 0 To 4
      SendBuff(indx) = CInt("&H" & Mid(tmpStr, indx * 2 + 1, 2))
    Next indx
    SendLen = &H5 + SendBuff(4)
    If Len(tmpStr) < SendLen * 2 Then
      Call DisplayOut(4, 0, "Invalid data input, insufficient data length")
      tDataIn.SetFocus
      Exit Sub
    End If
    For indx = 0 To SendBuff(4) - 1
      SendBuff(indx + 5) = CInt("&H" & Mid(tmpStr, (indx + 5) * 2 + 1, 2))
    Next indx
    RecvLen = &H2
    tmpStr = ""
    For indx = 0 To SendLen - 1
      tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
    Next indx
    Call DisplayOut(2, 0, tmpStr)
    retCode = SendAPDU()
    If retCode = SCARD_S_SUCCESS Then
      tmpStr = ""
      For indx = 0 To RecvLen - 1
        tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
      Next indx
      Call DisplayOut(3, 0, tmpStr)
    End If
  End If
  
End Sub

Private Sub cbReader_Click()

  retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)

End Sub

Private Sub Form_Load()
  
  Call InitMenu

End Sub

Private Sub tDataIn_KeyPress(KeyAscii As Integer)

  If KeyAscii < 48 Or KeyAscii > 57 Then
    If KeyAscii < 65 Or KeyAscii > 70 Then
      If KeyAscii < 97 Or KeyAscii > 102 Then
        If KeyAscii <> 8 Then
          If KeyAscii <> 32 Then
            KeyAscii = 0
          End If
        End If
      End If
    End If
  End If
  
End Sub
