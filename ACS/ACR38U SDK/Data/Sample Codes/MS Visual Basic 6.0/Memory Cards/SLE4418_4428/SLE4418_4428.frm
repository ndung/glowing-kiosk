VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form SLE418_4428Main 
   Caption         =   "Using SLE4418/4428 in PC/SC"
   ClientHeight    =   5310
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   8625
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   9
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "SLE4418_4428.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   5310
   ScaleWidth      =   8625
   StartUpPosition =   2  'CenterScreen
   Begin VB.Frame fCardType 
      Caption         =   "Card Type"
      Height          =   1095
      Left            =   240
      TabIndex        =   17
      Top             =   600
      Width           =   1695
      Begin VB.OptionButton SLE4428 
         Caption         =   "SLE 4428"
         Height          =   255
         Left            =   240
         TabIndex        =   19
         Top             =   720
         Width           =   1215
      End
      Begin VB.OptionButton SLE4418 
         Caption         =   "SLE 4418"
         Height          =   210
         Left            =   240
         TabIndex        =   18
         Top             =   360
         Width           =   1095
      End
   End
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   6720
      TabIndex        =   7
      Top             =   4680
      Width           =   1695
   End
   Begin VB.CommandButton bReset 
      Caption         =   "R&eset"
      Height          =   375
      Left            =   4440
      TabIndex        =   6
      Top             =   4680
      Width           =   1695
   End
   Begin VB.Frame fFunction 
      Caption         =   "Functions"
      Height          =   3375
      Left            =   120
      TabIndex        =   5
      Top             =   1800
      Width           =   3735
      Begin VB.CommandButton bErrCtr 
         Caption         =   "Read Err C&tr"
         Height          =   375
         Left            =   2040
         TabIndex        =   23
         Top             =   2760
         Width           =   1455
      End
      Begin VB.CommandButton bSubmit 
         Caption         =   "&Submit Code"
         Height          =   375
         Left            =   240
         TabIndex        =   22
         Top             =   2760
         Width           =   1455
      End
      Begin VB.CommandButton bWriteProt 
         Caption         =   "Write &Protect"
         Height          =   375
         Left            =   2040
         TabIndex        =   21
         Top             =   2160
         Width           =   1455
      End
      Begin VB.CommandButton bReadProt 
         Caption         =   "Re&ad w/ Prot"
         Height          =   375
         Left            =   2040
         TabIndex        =   20
         Top             =   1560
         Width           =   1455
      End
      Begin VB.CommandButton bWrite 
         Caption         =   "&Write"
         Height          =   375
         Left            =   240
         TabIndex        =   16
         Top             =   2160
         Width           =   1455
      End
      Begin VB.CommandButton bRead 
         Caption         =   "&Read w/o Prot"
         Height          =   375
         Left            =   240
         TabIndex        =   15
         Top             =   1560
         Width           =   1455
      End
      Begin VB.TextBox tData 
         Height          =   375
         Left            =   720
         TabIndex        =   14
         Top             =   960
         Width           =   2775
      End
      Begin VB.TextBox tLen 
         Height          =   375
         Left            =   3000
         MaxLength       =   2
         TabIndex        =   13
         Top             =   360
         Width           =   495
      End
      Begin VB.TextBox tLoAdd 
         Height          =   375
         Left            =   1560
         MaxLength       =   2
         TabIndex        =   10
         Top             =   360
         Width           =   495
      End
      Begin VB.TextBox tHiAdd 
         Height          =   375
         Left            =   960
         MaxLength       =   2
         TabIndex        =   9
         Top             =   360
         Width           =   495
      End
      Begin VB.Label Label6 
         Caption         =   "Data"
         Height          =   255
         Left            =   120
         TabIndex        =   12
         Top             =   960
         Width           =   495
      End
      Begin VB.Label Label5 
         Caption         =   "Length"
         Height          =   255
         Left            =   2280
         TabIndex        =   11
         Top             =   360
         Width           =   615
      End
      Begin VB.Label Label4 
         Caption         =   "Address"
         Height          =   375
         Left            =   120
         TabIndex        =   8
         Top             =   360
         Width           =   735
      End
   End
   Begin VB.CommandButton bConnect 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   2160
      TabIndex        =   4
      Top             =   1320
      Width           =   1695
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initialize"
      Height          =   375
      Left            =   2160
      TabIndex        =   3
      Top             =   720
      Width           =   1695
   End
   Begin VB.ComboBox cbReader 
      Height          =   330
      Left            =   1680
      TabIndex        =   2
      Top             =   120
      Width           =   2175
   End
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   4215
      Left            =   3960
      TabIndex        =   1
      Top             =   120
      Width           =   4575
      _ExtentX        =   8070
      _ExtentY        =   7435
      _Version        =   393217
      Enabled         =   -1  'True
      TextRTF         =   $"SLE4418_4428.frx":17A2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
   End
   Begin VB.Label Label1 
      Caption         =   "Select Reader"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   1575
   End
End
Attribute VB_Name = "SLE418_4428Main"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              SLE4418_4428.frm
'
'  Description:       This sample program outlines the steps on how to
'                     program SLE4418/4428 memory cards using ACS readers
'                     in PC/SC platform.
'
'  Author:            Jose Isagani R. Mission
'
'  Date:              June 21, 2004
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

Private Sub ClearBuffers()

  Dim indx As Long
  
  For indx = 0 To 262
    RecvBuff(indx) = &H0
    SendBuff(indx) = &H0
  Next indx
  
End Sub

Private Sub InitMenu()

  cbReader.Clear
  bInit.Enabled = True
  fCardType.Enabled = False
  bConnect.Enabled = False
  bReset.Enabled = False
  fFunction.Enabled = False
  mMsg.Text = ""
  Call DisplayOut(0, 0, "Program ready")
  
End Sub

Private Sub ClearFields()
  
  tHiAdd.Text = ""
  tLoAdd.Text = ""
  tLen.Text = ""
  tData.Text = ""

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

  cbReader.Enabled = True
  bInit.Enabled = False
  fCardType.Enabled = True
  SLE4418.Value = True
  bConnect.Enabled = True
  bReset.Enabled = True

End Sub

Private Sub GetATR()
  Dim tmpStr As String
  Dim indx As Integer

  Call DisplayOut(2, 0, "Get ATR")
  Call ClearBuffers
  SendLen = 6
  RecvLen = 6
  SendBuff(0) = &HFF
  SendBuff(1) = &HA4
  SendBuff(2) = &H0
  SendBuff(3) = &H0
  SendBuff(4) = &H1
  SendBuff(5) = &H5     ' SLE4418/4428 value
  tmpStr = ""
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(1, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

End Sub

Private Function InputOK(ByVal checkType As Integer) As Boolean

  Dim tmpStr As String
  Dim indx As Integer
  
  InputOK = False
  Select Case checkType
    Case 0              ' for Read function
      If tHiAdd.Text = "" Then
        tHiAdd.SetFocus
        Exit Function
      End If
      If tLoAdd.Text = "" Then
        tLoAdd.SetFocus
        Exit Function
      End If
      If tLen.Text = "" Then
        tLen.SetFocus
        Exit Function
      End If
    Case 1              ' for Write function
      If tHiAdd.Text = "" Then
        tHiAdd.SetFocus
        Exit Function
      End If
      If tLoAdd.Text = "" Then
        tLoAdd.SetFocus
        Exit Function
      End If
      If tLen.Text = "" Then
        tLen.SetFocus
        Exit Function
      End If
      If tData.Text = "" Then
        tData.SetFocus
        Exit Function
      End If
    Case 2              ' for Verify function
      tHiAdd.Text = ""
      tLoAdd.Text = ""
      tLen.Text = ""
      If tData.Text = "" Then
        tData.SetFocus
        Exit Function
      End If
      tData.Text = UCase(tData.Text)
      tmpStr = ""
      For indx = 0 To Len(tData.Text)
        If Mid(tData.Text, indx + 1, 1) <> " " Then
          tmpStr = tmpStr & Mid(tData.Text, indx + 1, 1)
        End If
      Next indx
      If Len(tmpStr) <> 4 Then
        tData.SelStart = 0
        tData.SelLength = Len(tData.Text)
        tData.SetFocus
        Exit Function
      End If
      For indx = 0 To Len(tmpStr) - 1
        If Asc(Mid(tmpStr, indx + 1, 1)) < 48 Or Asc(Mid(tmpStr, indx + 1, 1)) > 57 Then
          If Asc(Mid(tmpStr, indx + 1, 1)) < 65 Or Asc(Mid(tmpStr, indx + 1, 1)) > 70 Then
            tData.SelStart = 0
            tData.SelLength = Len(tData.Text)
            tData.SetFocus
            Exit Function
          End If
        End If
      Next indx
  End Select
  InputOK = True

End Function


Private Function SendAPDUandDisplay(ByVal SendType As Integer, ByVal ApduIn As String) As Long

  Dim indx As Integer
  Dim tmpStr As String

  ioRequest.dwProtocol = Protocol
  ioRequest.cbPciLength = Len(ioRequest)
  Call DisplayOut(2, 0, ApduIn)
  tmpStr = ""
  
  retCode = SCardTransmit(hCard, _
                          ioRequest, _
                          SendBuff(0), _
                          SendLen, _
                          ioRequest, _
                          RecvBuff(0), _
                          RecvLen)
  If retCode <> SCARD_S_SUCCESS Then
    Call DisplayOut(1, retCode, "")
    SendAPDUandDisplay = retCode
    Exit Function
  Else
    Select Case SendType
      Case 0                  ' Display SW1/SW2 value
        For indx = RecvLen - 2 To RecvLen - 1
          tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
        Next indx
      Case 1                  ' Display ATR after checking SW1/SW2
        For indx = RecvLen - 2 To RecvLen - 1
          tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
        Next indx
        If tmpStr <> "90 00 " Then
          Call DisplayOut(1, 0, "Return bytes are not acceptable.")
        Else
          tmpStr = "ATR: "
          For indx = 0 To RecvLen - 3
            tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
          Next indx
        End If
      Case 2                  ' Display all data after checking SW1/SW2
        For indx = RecvLen - 2 To RecvLen - 1
          tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
        Next indx
        If tmpStr <> "90 00 " Then
          Call DisplayOut(1, 0, "Return bytes are not acceptable.")
        Else
          tmpStr = ""
          For indx = 0 To RecvLen - 1
            tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
          Next indx
        End If
    End Select
    Call DisplayOut(3, 0, tmpStr)
  End If
  SendAPDUandDisplay = retCode
  
End Function

Private Sub bConnect_Click()

  Dim cardType As Integer
  Dim i As Integer
  Dim tempstr As String
    
  If ConnActive Then
    Call DisplayOut(0, 0, "Connection is already active.")
    Exit Sub
  End If
  
  retCode = SCardConnect(hContext, _
                        cbReader.Text, _
                        SCARD_SHARE_SHARED, _
                        SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1, _
                        hCard, _
                        Protocol)
  If retCode <> SCARD_S_SUCCESS Then
    Call DisplayOut(1, retCode, "")
    ConnActive = False
    Exit Sub
  Else
    Call DisplayOut(0, 0, "Successful connection to " & cbReader.Text)
  End If
  
  Call ClearBuffers
  'Select Card Type
  SendBuff(0) = &HFF
  SendBuff(1) = &HA4
  SendBuff(2) = &H0
  SendBuff(3) = &H0
  SendBuff(4) = &H1
  SendBuff(5) = &H5
  
  SendLen = 6
  RecvLen = 255
  
  For i = 0 To SendLen - 1
    tempstr = tempstr & Format(Hex(SendBuff(i)), "00") & " "
  Next i
  retCode = SendAPDUandDisplay(2, tempstr)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If
  
  ConnActive = True
  Call ClearFields
  fFunction.Enabled = True
  
End Sub

Private Sub bErrCtr_Click()
  
  Dim indx As Integer
  Dim tmpStr As String
  
  ' 1. Clear all input fields
  Call ClearFields

  ' 2. Read input fields and pass data to card
  Call ClearBuffers
  SendBuff(0) = &HFF
  SendBuff(1) = &HB1
  SendBuff(2) = &H0
  SendBuff(3) = &H0
  SendBuff(4) = &H3
  SendLen = 5
  RecvLen = 5
  tmpStr = ""
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(2, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

End Sub

Private Sub bInit_Click()

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
  
End Sub

Private Sub bQuit_Click()

  If ConnActive Then
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
  End If
  retCode = SCardReleaseContext(hContext)
  Unload Me
  
End Sub

Private Sub bRead_Click()

  Dim indx As Integer
  Dim tmpStr As String
  
  ' 1. Check for all input fields
  If Not InputOK(0) Then
    Exit Sub
  End If

  ' 2. Read input fields and pass data to card
  tData.Text = ""
  Call ClearBuffers
  SendBuff(0) = &HFF
  SendBuff(1) = &HB0
  SendBuff(2) = CInt("&H" & Mid(tHiAdd.Text, 1, 2))
  SendBuff(3) = CInt("&H" & Mid(tLoAdd.Text, 1, 2))
  SendBuff(4) = CInt("&H" & Mid(tLen.Text, 1, 2))
  SendLen = 5
  RecvLen = SendBuff(4) + 2
  tmpStr = ""
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(2, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  ' 3. Display data read from card into Data textbox
  tmpStr = ""
  For indx = 0 To SendBuff(4) - 1
    tmpStr = tmpStr & Chr(RecvBuff(indx))
  Next indx
  tData.Text = tmpStr

End Sub

Private Sub bReadProt_Click()

  Dim indx, protLen As Integer
  Dim tmpStr As String
  
  ' 1. Check for all input fields
  If Not InputOK(0) Then
    Exit Sub
  End If

  ' 2. Read input fields and pass data to card
  tData.Text = ""
  Call ClearBuffers
  SendBuff(0) = &HFF
  SendBuff(1) = &HB2
  SendBuff(2) = CInt("&H" & Mid(tHiAdd.Text, 1, 2))
  SendBuff(3) = CInt("&H" & Mid(tLoAdd.Text, 1, 2))
  SendBuff(4) = CInt("&H" & Mid(tLen.Text, 1, 2))
  SendLen = 5
  protLen = 1 + (SendBuff(4) / 8)
  RecvLen = SendBuff(4) + protLen + 2
  tmpStr = ""
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(2, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  ' 3. Display data read from card into Data textbox
  tmpStr = ""
  For indx = 0 To SendBuff(4) - 1
    tmpStr = tmpStr & Chr(RecvBuff(indx))
  Next indx
  tData.Text = tmpStr

End Sub

Private Sub bReset_Click()

  SLE4418.Value = False
  SLE4428.Value = False
  bSubmit.Enabled = True
  bErrCtr.Enabled = True
  Call ClearFields
  If ConnActive Then
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
  End If
  retCode = SCardReleaseContext(hContext)
  Call InitMenu
  
End Sub

Private Sub bSubmit_Click()

  Dim indx As Integer
  Dim tmpStr As String
  
  ' 1. Check for all input fields
  If Not InputOK(2) Then
    Exit Sub
  End If

  ' 2. Read input fields and pass data to card
  tmpStr = ""
  For indx = 0 To Len(tData.Text)
    If Mid(tData.Text, indx + 1, 1) <> " " Then
      tmpStr = tmpStr & Mid(tData.Text, indx + 1, 1)
    End If
  Next indx
  Call ClearBuffers
  SendBuff(0) = &HFF
  SendBuff(1) = &H20
  SendBuff(2) = &H0
  SendBuff(3) = &H0
  SendBuff(4) = &H2
  For indx = 0 To 1
    SendBuff(indx + 5) = CInt("&H" & Mid(tmpStr, (indx * 2) + 1, 2))
  Next indx
  SendLen = SendBuff(4) + 5
  RecvLen = 5
  tmpStr = ""
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(2, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  tData.Text = ""

End Sub

Private Sub bWrite_Click()

  Dim indx As Integer
  Dim tmpStr As String
  
  ' 1. Check for all input fields
  If Not InputOK(1) Then
    Exit Sub
  End If

  ' 2. Read input fields and pass data to card
  tmpStr = tData.Text
  Call ClearBuffers
  SendBuff(0) = &HFF
  SendBuff(1) = &HD0
  SendBuff(2) = CInt("&H" & Mid(tHiAdd.Text, 1, 2))
  SendBuff(3) = CInt("&H" & Mid(tLoAdd.Text, 1, 2))
  SendBuff(4) = CInt("&H" & Mid(tLen.Text, 1, 2))
  For indx = 0 To Len(tmpStr) - 1
    If Asc(Mid(tmpStr, indx + 1, 1)) <> &H0 Then
      SendBuff(indx + 5) = Asc(Mid(tmpStr, indx + 1, 1))
    End If
  Next indx
  SendLen = SendBuff(4) + 5
  RecvLen = 2
  tmpStr = ""
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(0, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  tData.Text = ""

End Sub

Private Sub cbCardType_Click()
  
  fFunction.Enabled = False
  Call ClearFields
  If ConnActive Then
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
  End If

End Sub

Private Sub bWriteProt_Click()

  Dim indx As Integer
  Dim tmpStr As String
  
  ' 1. Check for all input fields
  If Not InputOK(1) Then
    Exit Sub
  End If

  ' 2. Read input fields and pass data to card
  tmpStr = tData.Text
  Call ClearBuffers
  SendBuff(0) = &HFF
  SendBuff(1) = &HD1
  SendBuff(2) = CInt("&H" & Mid(tHiAdd.Text, 1, 2))
  SendBuff(3) = CInt("&H" & Mid(tLoAdd.Text, 1, 2))
  SendBuff(4) = CInt("&H" & Mid(tLen.Text, 1, 2))
  For indx = 0 To Len(tmpStr) - 1
    If Asc(Mid(tmpStr, indx + 1, 1)) <> &H0 Then
      SendBuff(indx + 5) = Asc(Mid(tmpStr, indx + 1, 1))
    End If
  Next indx
  SendLen = SendBuff(4) + 5
  RecvLen = 2
  tmpStr = ""
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(0, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  tData.Text = ""

End Sub

Private Sub cbReader_Click()
  
  fFunction.Enabled = False
  Call ClearFields
  If ConnActive Then
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
  End If

End Sub

Private Sub Form_Load()

  Call InitMenu
  
End Sub

Private Sub SLE4418_Click()
  
  fFunction.Enabled = False
  Call ClearFields
  If ConnActive Then
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
  End If
  bSubmit.Enabled = False
  bErrCtr.Enabled = False

End Sub

Private Sub SLE4428_Click()
  
  fFunction.Enabled = False
  Call ClearFields
  If ConnActive Then
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
  End If
  bSubmit.Enabled = True
  bErrCtr.Enabled = True

End Sub

Private Sub tHiAdd_KeyPress(KeyAscii As Integer)
  
  If KeyAscii < 48 Or KeyAscii > 57 Then
    If KeyAscii < 65 Or KeyAscii > 70 Then
      If KeyAscii < 97 Or KeyAscii > 102 Then
        If KeyAscii <> 8 Then
          KeyAscii = 0
        End If
      End If
    End If
  End If

End Sub

Private Sub tHiAdd_KeyUp(KeyCode As Integer, Shift As Integer)
  
  tHiAdd.Text = UCase(tHiAdd.Text)
  If Len(tHiAdd.Text) > 1 Then
    tLoAdd.SetFocus
  End If

End Sub

Private Sub tLen_KeyPress(KeyAscii As Integer)
  
  If KeyAscii < 48 Or KeyAscii > 57 Then
    If KeyAscii < 65 Or KeyAscii > 70 Then
      If KeyAscii < 97 Or KeyAscii > 102 Then
        If KeyAscii <> 8 Then
          KeyAscii = 0
        End If
      End If
    End If
  End If

End Sub

Private Sub tLen_KeyUp(KeyCode As Integer, Shift As Integer)
  
  tLen.Text = UCase(tLen.Text)
  If Len(tLen.Text) > 1 Then
    tData.SetFocus
  End If
  

End Sub

Private Sub tLen_LostFocus()

  If tLen.Text <> "" Then
    tData.Text = ""
    tData.MaxLength = CInt("&H" & Mid(tLen.Text, 1, 2))
  Else
    tData.MaxLength = 0
  End If

End Sub

Private Sub tLoAdd_KeyPress(KeyAscii As Integer)
  
  If KeyAscii < 48 Or KeyAscii > 57 Then
    If KeyAscii < 65 Or KeyAscii > 70 Then
      If KeyAscii < 97 Or KeyAscii > 102 Then
        If KeyAscii <> 8 Then
          KeyAscii = 0
        End If
      End If
    End If
  End If

End Sub

Private Sub tLoAdd_KeyUp(KeyCode As Integer, Shift As Integer)
  
  tLoAdd.Text = UCase(tLoAdd.Text)
  If Len(tLoAdd.Text) > 1 Then
    tLen.SetFocus
  End If
  
End Sub
