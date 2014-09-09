VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form IICMain 
   Caption         =   "IIC"
   ClientHeight    =   6015
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
   Icon            =   "IICSample.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   6015
   ScaleWidth      =   8625
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   6720
      TabIndex        =   12
      Top             =   5400
      Width           =   1695
   End
   Begin VB.CommandButton bReset 
      Caption         =   "R&eset"
      Height          =   375
      Left            =   4440
      TabIndex        =   11
      Top             =   5400
      Width           =   1695
   End
   Begin VB.Frame fFunction 
      Caption         =   "Functions"
      Height          =   3495
      Left            =   120
      TabIndex        =   7
      Top             =   2400
      Width           =   3735
      Begin VB.CommandButton bWrite 
         Caption         =   "&Write"
         Height          =   375
         Left            =   2040
         TabIndex        =   22
         Top             =   2880
         Width           =   1455
      End
      Begin VB.CommandButton bRead 
         Caption         =   "&Read"
         Height          =   375
         Left            =   240
         TabIndex        =   21
         Top             =   2880
         Width           =   1455
      End
      Begin VB.TextBox tData 
         Height          =   375
         Left            =   720
         TabIndex        =   20
         Top             =   2280
         Width           =   2775
      End
      Begin VB.TextBox tLen 
         Height          =   375
         Left            =   1080
         MaxLength       =   2
         TabIndex        =   19
         Top             =   1680
         Width           =   495
      End
      Begin VB.TextBox tLoAdd 
         Height          =   375
         Left            =   2280
         MaxLength       =   2
         TabIndex        =   16
         Top             =   1080
         Width           =   495
      End
      Begin VB.TextBox tHiAdd 
         Height          =   375
         Left            =   1680
         MaxLength       =   2
         TabIndex        =   15
         Top             =   1080
         Width           =   495
      End
      Begin VB.TextBox tBitAdd 
         Height          =   375
         Left            =   1080
         MaxLength       =   1
         TabIndex        =   14
         Top             =   1080
         Width           =   495
      End
      Begin VB.CommandButton bSet 
         Caption         =   "&Set Page Size"
         Height          =   375
         Left            =   2040
         TabIndex        =   10
         Top             =   480
         Width           =   1455
      End
      Begin VB.ComboBox cbPageSize 
         Height          =   330
         ItemData        =   "IICSample.frx":17A2
         Left            =   720
         List            =   "IICSample.frx":17B5
         TabIndex        =   8
         Top             =   480
         Width           =   975
      End
      Begin VB.Label Label6 
         Caption         =   "Data"
         Height          =   255
         Left            =   240
         TabIndex        =   18
         Top             =   2280
         Width           =   495
      End
      Begin VB.Label Label5 
         Caption         =   "Length"
         Height          =   255
         Left            =   240
         TabIndex        =   17
         Top             =   1680
         Width           =   735
      End
      Begin VB.Label Label4 
         Caption         =   "Address"
         Height          =   375
         Left            =   240
         TabIndex        =   13
         Top             =   1080
         Width           =   735
      End
      Begin VB.Label Label3 
         Caption         =   "Size"
         Height          =   255
         Left            =   240
         TabIndex        =   9
         Top             =   480
         Width           =   375
      End
   End
   Begin VB.CommandButton bConnect 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   2160
      TabIndex        =   6
      Top             =   1920
      Width           =   1695
   End
   Begin VB.ComboBox cbCardType 
      Height          =   330
      ItemData        =   "IICSample.frx":17E6
      Left            =   2160
      List            =   "IICSample.frx":180E
      TabIndex        =   5
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
      Height          =   4935
      Left            =   4080
      TabIndex        =   1
      Top             =   120
      Width           =   4455
      _ExtentX        =   7858
      _ExtentY        =   8705
      _Version        =   393217
      Enabled         =   -1  'True
      TextRTF         =   $"IICSample.frx":1883
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
   Begin VB.Label Label2 
      Caption         =   "Select Card Type"
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
      TabIndex        =   4
      Top             =   1320
      Width           =   1695
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
Attribute VB_Name = "IICMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              IICSample.frm
'
'  Description:       This sample program outlines the steps on how to
'                     program IIC memory cards using ACS readers
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
  cbCardType.Enabled = False
  bInit.Enabled = True
  bConnect.Enabled = False
  bReset.Enabled = False
  fFunction.Enabled = False
  mMsg.Text = ""
  Call DisplayOut(0, 0, "Program ready")
  
End Sub

Private Sub ClearFields()
  
  cbPageSize.ListIndex = -1
  tBitAdd.Text = ""
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
  cbCardType.Enabled = True
  cbCardType.ListIndex = 0
  bConnect.Enabled = True
  bReset.Enabled = True

End Sub

Private Function InputOK(ByVal checkType As Integer, ByVal opType As Integer) As Boolean

  InputOK = False
  If checkType = 1 Then    ' For 17-bit address input
    If tBitAdd.Text = "" Then
      tBitAdd.SetFocus
      Exit Function
    End If
  End If
  If tHiAdd.Text = "" Then
    tHiAdd.SetFocus
    Exit Function
  End If
  If tLoAdd.Text = "" Then
    tLoAdd.SetFocus
    Exit Function
  End If
  If opType = 1 Then       ' For Write Operation
    If tData.Text = "" Then
      tData.SetFocus
      Exit Function
    End If
  End If

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
          For indx = 0 To RecvLen - 3
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

  ConnActive = True
  fFunction.Enabled = True
  cbPageSize.ListIndex = 0
  If cbCardType.ListIndex = 11 Then
    tBitAdd.Enabled = True
  Else
    tBitAdd.Enabled = False
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
  If cbCardType.ListIndex = 11 Then
    indx = 1
  Else
    indx = 0
  End If
  If Not InputOK(indx, 0) Then
    Exit Sub
  End If

  ' 2. Read input fields and pass data to card
  tData.Text = ""
  Call ClearBuffers
  SendBuff(0) = &HFF
  If ((cbCardType.ListIndex = 11) And (tBitAdd.Text = "1")) Then
    SendBuff(1) = &HB1
  Else
    SendBuff(1) = &HB0
  End If
  SendBuff(2) = CInt("&H" & Mid(tHiAdd.Text, 1, 2))
  SendBuff(3) = CInt("&H" & Mid(tLoAdd.Text, 1, 2))
  SendBuff(4) = CInt("&H" & Mid(tLen.Text, 1, 2))
  SendLen = 5
  RecvLen = SendBuff(4) + 2
  tmpStr = ""
  For indx = 0 To 5
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

  cbCardType.ListIndex = -1
  Call ClearFields
  If ConnActive Then
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
  End If
  retCode = SCardReleaseContext(hContext)
  Call InitMenu
  
End Sub

Private Sub bSet_Click()

  Dim indx As Integer
  Dim tmpStr As String

  Call ClearBuffers
  SendBuff(0) = &HFF
  SendBuff(1) = &H1
  SendBuff(2) = &H0
  SendBuff(3) = &H0
  SendBuff(4) = &H1
  Select Case cbPageSize.ListIndex
    Case 0
      SendBuff(5) = &H3
    Case 1
      SendBuff(5) = &H4
    Case 2
      SendBuff(5) = &H5
    Case 3
      SendBuff(5) = &H6
    Case 4
      SendBuff(5) = &H7
  End Select
  tmpStr = ""
  SendLen = 6
  RecvLen = 2
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(0, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If


End Sub

Private Sub bWrite_Click()

  Dim indx As Integer
  Dim tmpStr As String
  
  ' 1. Check for all input fields
  If cbCardType.ListIndex = 11 Then
    indx = 1
  Else
    indx = 0
  End If
  If Not InputOK(indx, 1) Then
    Exit Sub
  End If

  ' 2. Read input fields and pass data to card
  tmpStr = tData.Text
  Call ClearBuffers
  SendBuff(0) = &HFF
  If ((cbCardType.ListIndex = 11) And (tBitAdd.Text = "1")) Then
    SendBuff(1) = &HD1
  Else
    SendBuff(1) = &HD0
  End If
  SendBuff(2) = CInt("&H" & Mid(tHiAdd.Text, 1, 2))
  SendBuff(3) = CInt("&H" & Mid(tLoAdd.Text, 1, 2))
  SendBuff(4) = CInt("&H" & Mid(tLen.Text, 1, 2))
  For indx = 0 To Len(tmpStr) - 1
    SendBuff(indx + 5) = Asc(Mid(tmpStr, indx + 1, 1))
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

Private Sub tBitAdd_KeyPress(KeyAscii As Integer)

  If KeyAscii < 48 Or KeyAscii > 49 Then
    If KeyAscii <> 8 Then
      KeyAscii = 0
    End If
  End If
  
End Sub

Private Sub tBitAdd_KeyUp(KeyCode As Integer, Shift As Integer)
  
  tBitAdd.Text = UCase(tBitAdd.Text)
  If Len(tBitAdd.Text) > 0 Then
    tHiAdd.SetFocus
  End If
  
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
