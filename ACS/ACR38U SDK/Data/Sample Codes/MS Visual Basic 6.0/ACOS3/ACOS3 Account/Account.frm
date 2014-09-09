VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form MainAccount 
   Caption         =   "Using ACOS3 Account Files"
   ClientHeight    =   7515
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   9615
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   9
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "Account.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   7515
   ScaleWidth      =   9615
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   7320
      TabIndex        =   26
      Top             =   6600
      Width           =   1695
   End
   Begin VB.CommandButton bReset 
      Caption         =   "&Reset"
      Height          =   375
      Left            =   5280
      TabIndex        =   25
      Top             =   6600
      Width           =   1695
   End
   Begin VB.Frame fFunction 
      Caption         =   "Account Functions"
      Height          =   2415
      Left            =   240
      TabIndex        =   14
      Top             =   4920
      Width           =   3975
      Begin VB.CheckBox chk_dbc 
         Caption         =   "Require Debit Certificate"
         Height          =   255
         Left            =   240
         TabIndex        =   27
         Top             =   960
         Width           =   2415
      End
      Begin VB.TextBox tValue 
         Height          =   315
         Left            =   1440
         MaxLength       =   8
         TabIndex        =   24
         Top             =   360
         Width           =   1575
      End
      Begin VB.CommandButton bRevDeb 
         Caption         =   "Re&voke Debit"
         Height          =   375
         Left            =   2160
         TabIndex        =   23
         Top             =   1920
         Width           =   1575
      End
      Begin VB.CommandButton bDebit 
         Caption         =   "&Debit"
         Height          =   375
         Left            =   240
         TabIndex        =   22
         Top             =   1920
         Width           =   1575
      End
      Begin VB.CommandButton bInquire 
         Caption         =   "I&nquire Balance"
         Height          =   375
         Left            =   2160
         TabIndex        =   21
         Top             =   1440
         Width           =   1575
      End
      Begin VB.CommandButton bCredit 
         Caption         =   "Cr&edit"
         Height          =   375
         Left            =   240
         TabIndex        =   20
         Top             =   1440
         Width           =   1575
      End
      Begin VB.Label Label6 
         Caption         =   "Amount"
         Height          =   255
         Left            =   360
         TabIndex        =   19
         Top             =   360
         Width           =   735
      End
   End
   Begin VB.Frame fKeys 
      Caption         =   "Security Keys"
      Height          =   2295
      Left            =   240
      TabIndex        =   9
      Top             =   2520
      Width           =   3975
      Begin VB.TextBox kRevDeb 
         Height          =   315
         Left            =   1560
         TabIndex        =   18
         Top             =   1800
         Width           =   2175
      End
      Begin VB.TextBox kCertify 
         Height          =   315
         Left            =   1560
         TabIndex        =   17
         Top             =   1320
         Width           =   2175
      End
      Begin VB.TextBox kCredit 
         Height          =   315
         Left            =   1560
         TabIndex        =   16
         Top             =   360
         Width           =   2175
      End
      Begin VB.TextBox kDebit 
         Height          =   315
         Left            =   1560
         TabIndex        =   15
         Top             =   840
         Width           =   2175
      End
      Begin VB.Label Label5 
         Caption         =   "Revoke Debit"
         Height          =   255
         Left            =   240
         TabIndex        =   13
         Top             =   1800
         Width           =   1095
      End
      Begin VB.Label Label4 
         Caption         =   "Certify"
         Height          =   255
         Left            =   240
         TabIndex        =   12
         Top             =   1320
         Width           =   735
      End
      Begin VB.Label Label3 
         Caption         =   "Credit"
         Height          =   255
         Left            =   240
         TabIndex        =   11
         Top             =   360
         Width           =   855
      End
      Begin VB.Label Label2 
         Caption         =   "Debit"
         Height          =   255
         Left            =   240
         TabIndex        =   10
         Top             =   840
         Width           =   735
      End
   End
   Begin VB.Frame fSecOption 
      Caption         =   "Security Option"
      Height          =   1335
      Left            =   240
      TabIndex        =   6
      Top             =   1080
      Width           =   2055
      Begin VB.OptionButton rb3DES 
         Caption         =   "3-DES"
         Height          =   255
         Left            =   480
         TabIndex        =   8
         Top             =   840
         Width           =   975
      End
      Begin VB.OptionButton rbDES 
         Caption         =   "DES"
         Height          =   210
         Left            =   480
         TabIndex        =   7
         Top             =   480
         Width           =   975
      End
   End
   Begin VB.CommandButton bFormat 
      Caption         =   "&Format Card"
      Height          =   375
      Left            =   2520
      TabIndex        =   5
      Top             =   2040
      Width           =   1695
   End
   Begin VB.CommandButton bConnect 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   2520
      TabIndex        =   4
      Top             =   1440
      Width           =   1695
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initialize"
      Height          =   375
      Left            =   2520
      TabIndex        =   3
      Top             =   840
      Width           =   1695
   End
   Begin VB.ComboBox cbReader 
      Height          =   330
      Left            =   1680
      TabIndex        =   2
      Top             =   240
      Width           =   2535
   End
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   6015
      Left            =   4440
      TabIndex        =   0
      Top             =   120
      Width           =   5055
      _ExtentX        =   8916
      _ExtentY        =   10610
      _Version        =   393217
      Enabled         =   -1  'True
      ScrollBars      =   2
      TextRTF         =   $"Account.frx":17A2
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
      Height          =   375
      Left            =   120
      TabIndex        =   1
      Top             =   240
      Width           =   1455
   End
End
Attribute VB_Name = "MainAccount"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              Account.frm
'
'  Description:       This sample program outlines the steps on how to
'                     use the Account File functionalities of ACOS
'                     using the PC/SC platform.
'
'  Author:            Jose Isagani R. Mission
'
'  Date:              May 26, 2004
'
'  Revision Trail:   (Date/Author/Description)
'   11/09/2005      Fernando G. Robles      Added Debit Certification
'
'======================================================================
Option Explicit

Dim retCode, Protocol, hContext, hCard, ReaderCount As Long
Dim sReaderList As String * 256
Dim sReaderGroup As String
Dim ConnActive As Boolean
Dim ioRequest As SCARD_IO_REQUEST
Dim SendLen, RecvLen As Long
Dim SendBuff(0 To 262) As Byte
Dim RecvBuff(0 To 262) As Byte

Const INVALID_SW1SW2 = -450

' this routine will encrypt 8-byte data with 8-byte key
' the result is stored in data
Public Sub DES(Data() As Byte, key() As Byte)
    Call Chain_DES(Data(0), key(0), ALGO_DES, 1, DATA_ENCRYPT)
End Sub

' this routine will use 3DES algo to encrypt 8-byte data with 16-byte key
' the result is stored in data
Public Sub TripleDES(Data() As Byte, key() As Byte)
    Call Chain_DES(Data(0), key(0), ALGO_3DES, 1, DATA_ENCRYPT)
End Sub


' MAC as defined in ACOS manual
' receives 8-byte Key and 16-byte Data
' result is stored in Data
Public Sub mac(Data() As Byte, key() As Byte)
Dim i As Integer

    DES Data, key
    For i = 0 To 7
        Data(i) = Data(i) Xor Data(i + 8)
    Next
    DES Data, key
End Sub

' Triple MAC as defined in ACOS manual
' receives 16-byte Key and 16-byte Data
' result is stored in Data
Public Sub TripleMAC(Data() As Byte, key() As Byte)
Dim i As Integer

    TripleDES Data, key
    For i = 0 To 7
        Data(i) = Data(i) Xor Data(i + 8)
    Next
    TripleDES Data, key
End Sub

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
  bConnect.Enabled = False
  bFormat.Enabled = False
  bReset.Enabled = False
  Call ClearTextFields
  fSecOption.Enabled = False
  fKeys.Enabled = False
  fFunction.Enabled = False
  mMsg.Text = ""
  rbDES.Value = False
  rb3DES.Value = False
  Call DisplayOut(0, 0, "Program ready")
  
End Sub

Private Sub DisplayOut(ByVal mType As Integer, ByVal msgCode As Long, ByVal PrintText As String)

  Select Case mType
    Case 0                          ' Notifications only
      mMsg.SelColor = &H4000
    Case 1                          ' PC/SC Error Messages
      mMsg.SelColor = vbRed
      PrintText = GetScardErrMsg(retCode)
    Case 2
      mMsg.SelColor = vbBlack       ' Input APDU command
      PrintText = "< " & PrintText
    Case 3
      mMsg.SelColor = vbBlack       ' Output data
      PrintText = "> " & PrintText
    Case 4
      mMsg.SelColor = vbRed         ' Notifications on red font
  End Select
  
  mMsg.SelText = PrintText & vbCrLf
  mMsg.SelStart = Len(mMsg.Text)
  mMsg.SelColor = vbBlack
 

End Sub

Private Sub AddButtons()

  bInit.Enabled = False
  bConnect.Enabled = True
  bReset.Enabled = True

End Sub

Private Sub ClearTextFields()

  kCredit.Text = ""
  kDebit.Text = ""
  kCertify.Text = ""
  kRevDeb.Text = ""
  tValue.Text = ""
  chk_dbc.Value = 0

End Sub

Private Function SendAPDUandDisplay(ByVal SendType As Integer, ByVal ApduIn As String) As Long

  Dim indx As Integer
  Dim tmpStr As String

  ioRequest.dwProtocol = Protocol
  ioRequest.cbPciLength = Len(ioRequest)
  Call DisplayOut(2, 0, ApduIn)
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
    SendAPDUandDisplay = retCode
    Exit Function
  Else
    Select Case SendType
      Case 0                  ' Read all data received
        For indx = 0 To RecvLen - 1
          tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
        Next indx
      Case 1                  ' Read ATR after checking SW1/SW2
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
      Case 2                  ' Read data after checking SW1/SW2
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

Private Function SubmitIC() As Long

  Dim indx As Integer
  Dim tmpStr As String

  Call ClearBuffers
  SendBuff(0) = &H80        ' CLA
  SendBuff(1) = &H20        ' INS
  SendBuff(2) = &H7         ' P1
  SendBuff(3) = &H0         ' P2
  SendBuff(4) = &H8         ' P3
  SendBuff(5) = &H41        ' A
  SendBuff(6) = &H43        ' C
  SendBuff(7) = &H4F        ' O
  SendBuff(8) = &H53        ' S
  SendBuff(9) = &H54        ' T
  SendBuff(10) = &H45       ' E
  SendBuff(11) = &H53       ' S
  SendBuff(12) = &H54       ' T
  
  SendLen = &HD
  RecvLen = &H2
  tmpStr = ""
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(0, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    SubmitIC = retCode
    Exit Function
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
  Next indx
  If tmpStr <> "90 00 " Then
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    SubmitIC = INVALID_SW1SW2
    Exit Function
  End If
  
  SubmitIC = retCode

End Function

Private Function SelectFile(ByVal HiAddr As Byte, ByVal LoAddr As Byte) As Long

  Dim indx As Integer
  Dim tmpStr As String

  Call ClearBuffers
  SendBuff(0) = &H80        ' CLA
  SendBuff(1) = &HA4        ' INS
  SendBuff(2) = &H0         ' P1
  SendBuff(3) = &H0         ' P2
  SendBuff(4) = &H2         ' P3
  SendBuff(5) = HiAddr      ' Value of High Byte
  SendBuff(6) = LoAddr      ' Value of Low Byte
  
  SendLen = &O7
  RecvLen = &H2
  tmpStr = ""
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(0, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    SelectFile = retCode
    Exit Function
  End If
  
  SelectFile = retCode

End Function

Private Function readRecord(ByVal RecNo As Byte, ByVal dataLen As Byte) As Long
  
  Dim indx As Integer
  Dim tmpStr As String
  
  ' 1. Read data from card
  Call ClearBuffers
  SendBuff(0) = &H80        ' CLA
  SendBuff(1) = &HB2        ' INS
  SendBuff(2) = RecNo       ' Record No
  SendBuff(3) = &H0         ' P2
  SendBuff(4) = dataLen     ' Length of Data
  SendLen = 5
  RecvLen = SendBuff(4) + 2
  tmpStr = ""
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(0, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    readRecord = retCode
    Exit Function
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx + SendBuff(4))), "00") & " "
  Next indx
  If tmpStr <> "90 00 " Then
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    readRecord = INVALID_SW1SW2
    Exit Function
  End If
  
  readRecord = retCode

End Function

Private Function writeRecord(ByVal caseType As Integer, ByVal RecNo As Byte, ByVal maxLen As Byte, _
                             ByVal dataLen As Byte, ByRef ApduIn() As Byte) As Long

  Dim indx As Integer
  Dim tmpStr As String

  If caseType = 1 Then   ' If card data is to be erased before writing new data
    ' 1. Re-initialize card values to $00
    Call ClearBuffers
    SendBuff(0) = &H80        ' CLA
    SendBuff(1) = &HD2        ' INS
    SendBuff(2) = RecNo       ' Record No
    SendBuff(3) = &H0         ' P2
    SendBuff(4) = maxLen     ' Length of Data
    For indx = 0 To maxLen - 1
      SendBuff(indx + 5) = &H0
    Next indx
    SendLen = SendBuff(4) + 5
    RecvLen = &H2
    tmpStr = ""
    For indx = 0 To SendLen - 1
      tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
    Next indx
    retCode = SendAPDUandDisplay(0, tmpStr)
    If retCode <> SCARD_S_SUCCESS Then
      writeRecord = retCode
      Exit Function
    End If
    tmpStr = ""
    For indx = 0 To 1
      tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    Next indx
    If tmpStr <> "90 00 " Then
      Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
      writeRecord = INVALID_SW1SW2
      Exit Function
    End If
  End If
  
  ' 2. Write data to card
  Call ClearBuffers
  SendBuff(0) = &H80        ' CLA
  SendBuff(1) = &HD2        ' INS
  SendBuff(2) = RecNo       ' Record No
  SendBuff(3) = &H0         ' P2
  SendBuff(4) = dataLen     ' Length of Data
  For indx = 0 To dataLen - 1
    SendBuff(indx + 5) = ApduIn(indx)
  Next indx
  SendLen = SendBuff(4) + 5
  RecvLen = &H2
  tmpStr = ""
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(0, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    writeRecord = retCode
    Exit Function
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
  Next indx
  If tmpStr <> "90 00 " Then
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    writeRecord = INVALID_SW1SW2
    Exit Function
  End If
  
  writeRecord = retCode

End Function

Private Function ValidTemplate() As Boolean

  If Len(kCredit.Text) < kCredit.MaxLength Then
    kCredit.SetFocus
    kCredit.SelStart = Len(kCredit.Text)
    ValidTemplate = False
    Exit Function
  End If
  
  If Len(kDebit.Text) < kDebit.MaxLength Then
    kDebit.SelStart = Len(kDebit.Text)
    kDebit.SetFocus
    ValidTemplate = False
    Exit Function
  End If
  
  If Len(kCertify.Text) < kCertify.MaxLength Then
    kCertify.SelStart = Len(kCertify.Text)
    kCertify.SetFocus
    ValidTemplate = False
    Exit Function
  End If
  
  If Len(kRevDeb.Text) < kRevDeb.MaxLength Then
    kRevDeb.SelStart = Len(kRevDeb.Text)
    kRevDeb.SetFocus
    ValidTemplate = False
    Exit Function
  End If
  
  ValidTemplate = True

End Function

Private Function CheckACOS() As Boolean

  Dim indx As Integer
  Dim tmpStr As String

 ' 1. Reconnect reader to accommodate change of cards
  If ConnActive Then
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
  End If
  retCode = SCardConnect(hContext, _
                        cbReader.Text, _
                        SCARD_SHARE_EXCLUSIVE, _
                        SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1, _
                        hCard, _
                        Protocol)
  If retCode <> SCARD_S_SUCCESS Then
    Call DisplayOut(1, retCode, "")
    ConnActive = False
    CheckACOS = False
    Exit Function
  End If
  ConnActive = True

  ' 2. Check for File FF 00
  retCode = SelectFile(&HFF, &H0)
  If retCode <> SCARD_S_SUCCESS Then
    CheckACOS = False
    Exit Function
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
  Next indx
  If tmpStr <> "90 00 " Then
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    CheckACOS = False
    Exit Function
  End If
  
  ' 3. Check for File FF 01
  retCode = SelectFile(&HFF, &H1)
  If retCode <> SCARD_S_SUCCESS Then
    CheckACOS = False
    Exit Function
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
  Next indx
  If tmpStr <> "90 00 " Then
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    CheckACOS = False
    Exit Function
  End If
  
  ' 4. Check for File FF 02
  retCode = SelectFile(&HFF, &H2)
  If retCode <> SCARD_S_SUCCESS Then
    CheckACOS = False
    Exit Function
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
  Next indx
  If tmpStr <> "90 00 " Then
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    CheckACOS = False
    Exit Function
  End If
  
  CheckACOS = True

End Function

Private Function ACOSError(ByVal Sw1 As Byte, ByVal Sw2 As Byte) As Boolean
  
  ' Check for error returned by ACOS card
  ACOSError = True
  If ((Sw1 = &H62) And (Sw2 = &H81)) Then
    Call DisplayOut(4, 0, "Account data may be corrupted.")
      Exit Function
  End If
  If (Sw1 = &H63) Then
    Call DisplayOut(4, 0, "MAC cryptographic checksum is wrong.")
      Exit Function
  End If
  If ((Sw1 = &H69) And (Sw2 = &H66)) Then
    Call DisplayOut(4, 0, "Command not available or option bit not set.")
      Exit Function
  End If
  If ((Sw1 = &H69) And (Sw2 = &H82)) Then
    Call DisplayOut(4, 0, "Security status not satisfied. Secret code, IC or PIN not submitted.")
      Exit Function
  End If
  If ((Sw1 = &H69) And (Sw2 = &H83)) Then
    Call DisplayOut(4, 0, "The specified code is locked.")
      Exit Function
  End If
  If ((Sw1 = &H69) And (Sw2 = &H85)) Then
    Call DisplayOut(4, 0, "Preceding transaction was not DEBIT or mutual authentication has not been completed.")
      Exit Function
  End If
  If ((Sw1 = &H69) And (Sw2 = &HF0)) Then
    Call DisplayOut(4, 0, "Data in account is inconsistent. No access unless in Issuer mode.")
      Exit Function
  End If
  If ((Sw1 = &H6A) And (Sw2 = &H82)) Then
    Call DisplayOut(4, 0, "Account does not exist.")
      Exit Function
  End If
  If ((Sw1 = &H6A) And (Sw2 = &H83)) Then
    Call DisplayOut(4, 0, "Record not found or file too short.")
      Exit Function
  End If
  If ((Sw1 = &H6A) And (Sw2 = &H86)) Then
    Call DisplayOut(4, 0, "P1 or P2 is incorrect.")
      Exit Function
  End If
  If ((Sw1 = &H6B) And (Sw2 = &H20)) Then
    Call DisplayOut(4, 0, "Invalid amount in DEBIT/CREDIT command.")
      Exit Function
  End If
  If (Sw1 = &H6C) Then
    Call DisplayOut(4, 0, "Issue GET RESPONSE with P3 = " & Hex(Sw2) & " to get response data.")
      Exit Function
  End If
  If (Sw1 = &H6D) Then
    Call DisplayOut(4, 0, "Unknown INS.")
      Exit Function
  End If
  If (Sw1 = &H6E) Then
    Call DisplayOut(4, 0, "Unknown CLA.")
      Exit Function
  End If
  If ((Sw1 = &H6F) And (Sw2 = &H10)) Then
    Call DisplayOut(4, 0, "Account Transaction Counter at maximum. No more transaction possible.")
      Exit Function
  End If

  ACOSError = False
  
End Function

Private Function InquireAccount(ByVal keyNo As Byte, ByRef DataIn() As Byte) As Long
  
  Dim indx As Integer
  Dim tmpStr As String

  Call ClearBuffers
  SendBuff(0) = &H80        ' CLA
  SendBuff(1) = &HE4        ' INS
  SendBuff(2) = keyNo       ' Key No
  SendBuff(3) = &H0         ' P2
  SendBuff(4) = &H4      ' Length of Data
  For indx = 0 To 3
    SendBuff(indx + 5) = DataIn(indx)
  Next indx
  SendLen = SendBuff(4) + 5
  RecvLen = &H2
  tmpStr = ""
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(0, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    InquireAccount = retCode
    Exit Function
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
  Next indx
  If ACOSError(RecvBuff(0), RecvBuff(1)) Then
    InquireAccount = INVALID_SW1SW2
    Exit Function
  End If
  If tmpStr <> "61 19 " Then     ' SW1/SW2 must be equal to 6119h
    Call DisplayOut(4, 0, "INQUIRE ACCOUNT command failed.")
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    InquireAccount = INVALID_SW1SW2
    Exit Function
  End If

  InquireAccount = retCode
  
End Function

Private Function GetResponse(ByVal LE As String) As Long
  
  Dim indx As Integer
  Dim tmpStr As String

  Call ClearBuffers
  SendBuff(0) = &H80        ' CLA
  SendBuff(1) = &HC0        ' INS
  SendBuff(2) = &H0         ' P1
  SendBuff(3) = &H0         ' P2
  SendBuff(4) = LE         ' Length of Data
  SendLen = 5
  RecvLen = SendBuff(4) + 2
  tmpStr = ""
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(0, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    GetResponse = retCode
    Exit Function
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx + SendBuff(4))), "00") & " "
  Next indx
  If ACOSError(RecvBuff(SendBuff(4)), RecvBuff(SendBuff(4) + 1)) Then
    GetResponse = INVALID_SW1SW2
    Exit Function
  End If
  If tmpStr <> "90 00 " Then
    Call DisplayOut(4, 0, "GET RESPONSE command failed.")
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    GetResponse = INVALID_SW1SW2
    Exit Function
  End If

  GetResponse = retCode

End Function

Private Function CreditAmount(ByRef CreditData() As Byte) As Long
  
  Dim indx As Integer
  Dim tmpStr As String

  Call ClearBuffers
  SendBuff(0) = &H80        ' CLA
  SendBuff(1) = &HE2        ' INS
  SendBuff(2) = &H0         ' P1
  SendBuff(3) = &H0         ' P2
  SendBuff(4) = &HB         ' P3
  For indx = 0 To 11
    SendBuff(indx + 5) = CreditData(indx)
  Next indx
  SendLen = SendBuff(4) + 5
  RecvLen = &H2
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(0, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    CreditAmount = retCode
    Exit Function
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
  Next indx
  If ACOSError(RecvBuff(0), RecvBuff(1)) Then
    CreditAmount = INVALID_SW1SW2
    Exit Function
  End If
  If tmpStr <> "90 00 " Then
    Call DisplayOut(4, 0, "CREDIT AMOUNT command failed.")
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    CreditAmount = INVALID_SW1SW2
    Exit Function
  End If

  CreditAmount = retCode

End Function

Private Function DebitAmount(ByRef DebitData() As Byte) As Long
  
  Dim indx As Integer
  Dim tmpStr As String

  Call ClearBuffers
  SendBuff(0) = &H80        ' CLA
  SendBuff(1) = &HE6        ' INS
  SendBuff(2) = &H0         ' P1
  SendBuff(3) = &H0         ' P2
  SendBuff(4) = &HB         ' P3
  For indx = 0 To 11
    SendBuff(indx + 5) = DebitData(indx)
  Next indx
  SendLen = SendBuff(4) + 5
  RecvLen = &H2
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(0, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    DebitAmount = retCode
    Exit Function
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
  Next indx
  If ACOSError(RecvBuff(0), RecvBuff(1)) Then
    DebitAmount = INVALID_SW1SW2
    Exit Function
  End If
  If tmpStr <> "90 00 " Then
    Call DisplayOut(4, 0, "DEBIT AMOUNT command failed.")
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    DebitAmount = INVALID_SW1SW2
    Exit Function
  End If

  DebitAmount = retCode

End Function


Private Function DebitAmountwithDBC(ByRef DebitData() As Byte) As Long
  
  Dim indx As Integer
  Dim tmpStr As String

  Call ClearBuffers
  SendBuff(0) = &H80        ' CLA
  SendBuff(1) = &HE6        ' INS
  SendBuff(2) = &H1          ' P1
  SendBuff(3) = &H0         ' P2
  SendBuff(4) = &HB         ' P3
  For indx = 0 To 11
    SendBuff(indx + 5) = DebitData(indx)
  Next indx
  SendLen = SendBuff(4) + 5
  RecvLen = &H2
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(0, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    DebitAmountwithDBC = retCode
    Exit Function
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
  Next indx
  If ACOSError(RecvBuff(0), RecvBuff(1)) Then
    DebitAmountwithDBC = INVALID_SW1SW2
    Exit Function
  End If
  If tmpStr <> "61 04 " Then
    Call DisplayOut(4, 0, "DEBIT AMOUNT command failed.")
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    DebitAmountwithDBC = INVALID_SW1SW2
    Exit Function
  End If

  DebitAmountwithDBC = retCode

End Function


Private Function RevokeDebit(ByRef RevDebData() As Byte) As Long
  
  Dim indx As Integer
  Dim tmpStr As String

  Call ClearBuffers
  SendBuff(0) = &H80        ' CLA
  SendBuff(1) = &HE8        ' INS
  SendBuff(2) = &H0         ' P1
  SendBuff(3) = &H0         ' P2
  SendBuff(4) = &H4         ' P3
  For indx = 0 To 4
    SendBuff(indx + 5) = RevDebData(indx)
  Next indx
  SendLen = SendBuff(4) + 5
  RecvLen = &H2
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(0, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    RevokeDebit = retCode
    Exit Function
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
  Next indx
  If ACOSError(RecvBuff(0), RecvBuff(1)) Then
    RevokeDebit = INVALID_SW1SW2
    Exit Function
  End If
  If tmpStr <> "90 00 " Then
    Call DisplayOut(4, 0, "REVOKE DEBIT command failed.")
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    RevokeDebit = INVALID_SW1SW2
    Exit Function
  End If

  RevokeDebit = retCode

End Function

Private Sub bConnect_Click()

  If ConnActive Then
    Call DisplayOut(0, 0, "Connection is already active.")
    Exit Sub
  End If
  
  Call DisplayOut(2, 0, "Invoke SCardConnect")
  ' 1. Connect to selected reader using hContext handle
  '    and obtain valid hCard handle
  retCode = SCardConnect(hContext, _
                        cbReader.Text, _
                        SCARD_SHARE_EXCLUSIVE, _
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
  bFormat.Enabled = True
  fSecOption.Enabled = True
  fKeys.Enabled = True
  fFunction.Enabled = True
  Call ClearTextFields
  rbDES.Value = True
  kCredit.MaxLength = 8
  kDebit.MaxLength = 8
  kCertify.MaxLength = 8
  kRevDeb.MaxLength = 8

End Sub

Private Sub bCredit_Click()
  
  Dim indx As Integer
  Dim tmpStr As String
  Dim tmpArray(0 To 31) As Byte
  Dim Amount, tmpVal As Long
  Dim tmpKey(0 To 15) As Byte       ' Credit key to verify MAC
  Dim TTREFc(0 To 3) As Byte
  Dim ATREF(0 To 5) As Byte

  ' 1. Check if Credit key and valid Transaction value are provided
  If Len(kCredit.Text) < kCredit.MaxLength Then
    kCredit.SetFocus
    Exit Sub
  End If
  If tValue.Text = "" Then
    tValue.SetFocus
    Exit Sub
  End If
  If Not IsNumeric(tValue.Text) Then
    tValue.SetFocus
    tValue.SelStart = 0
    tValue.SelLength = Len(tValue.Text)
    Exit Sub
  End If
  If CLng(tValue.Text) > 16777215 Then
    tValue.Text = "16777215"
    tValue.SetFocus
    Exit Sub
  End If
  
  ' 2. Check if card inserted is an ACOS card
  If Not CheckACOS Then
    Call DisplayOut(0, 0, "Please insert an ACOS card.")
    Exit Sub
  End If
  Call DisplayOut(0, 0, "ACOS card is detected.")
  
  ' 3. Issue INQUIRE ACCOUNT command using any arbitrary data and Credit key
  '    Arbitrary data is 1111h
  For indx = 0 To 3
    tmpArray(indx) = &H1
  Next indx
  retCode = InquireAccount(&H2, tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  ' 4. Issue GET RESPONSE command with Le = 19h or 25 bytes
  retCode = GetResponse("&H19")
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  ' 5. Store ACOS card values for TTREFc and ATREF
  For indx = 0 To 3
    TTREFc(indx) = RecvBuff(indx + 17)
  Next indx
  For indx = 0 To 5
    ATREF(indx) = RecvBuff(indx + 8)
  Next indx

  ' 6. Prepare MAC data block: E2 + AMT + TTREFc + ATREF + 00 + 00
  '    use tmpArray as the data block
  Amount = CLng(tValue.Text)
  tmpArray(0) = &HE2
  tmpVal = Int(Amount / 256)
  tmpVal = Int(tmpVal / 256)
  tmpArray(1) = tmpVal Mod 256                  ' Amount MSByte
  tmpVal = Int(Amount / 256)
  tmpArray(2) = tmpVal Mod 256                  ' Amount Middle Byte
  tmpArray(3) = Amount Mod 256                  ' Amount LSByte
  For indx = 0 To 3
    tmpArray(indx + 4) = TTREFc(indx)
  Next indx
  For indx = 0 To 5
    tmpArray(indx + 8) = ATREF(indx)
  Next indx
  tmpArray(13) = tmpArray(13) + 1               ' increment last byte of ATREF
  tmpArray(14) = &H0
  tmpArray(15) = &H0

  ' 7. Generate applicable MAC values, MAC result will be stored in tmpArray
  tmpStr = kCredit.Text
  For indx = 0 To Len(tmpStr) - 1
    tmpKey(indx) = Asc(Mid(tmpStr, indx + 1, 1))
  Next indx
  If rbDES.Value = True Then
    Call mac(tmpArray, tmpKey)
  Else
    Call TripleMAC(tmpArray, tmpKey)
  End If

  ' 8. Format Credit command data and execute credit command
  '    Using tmpArray, the first four bytes are carried over
  tmpVal = Int(Amount / 256)
  tmpVal = Int(tmpVal / 256)
  tmpArray(4) = tmpVal Mod 256                  ' Amount MSByte
  tmpVal = Int(Amount / 256)
  tmpArray(5) = tmpVal Mod 256                  ' Amount Middle Byte
  tmpArray(6) = Amount Mod 256                  ' Amount LSByte
  For indx = 0 To 3
    tmpArray(indx + 7) = ATREF(indx)
  Next indx
  retCode = CreditAmount(tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  Call DisplayOut(3, 0, "Credit transaction completed")
  Call ClearTextFields


End Sub

Private Sub bDebit_Click()
  
  Dim indx, i As Integer
  Dim tmpStr As String
  Dim tmpArray(0 To 31) As Byte
  Dim Amount, tmpVal As Long
  Dim tmpKey(0 To 15) As Byte       ' Debit key to verify MAC
  Dim TTREFd(0 To 3) As Byte
  Dim ATREF(0 To 5) As Byte
  Dim tmpBalance(0 To 3) As Long
  Dim new_balance As Long
  ' 1. Check if Debit key and valid Transaction value are provided
  If Len(kDebit.Text) < kDebit.MaxLength Then
    kDebit.SetFocus
    Exit Sub
  End If
  If tValue.Text = "" Then
    tValue.SetFocus
    Exit Sub
  End If
  If Not IsNumeric(tValue.Text) Then
    tValue.SetFocus
    tValue.SelStart = 0
    tValue.SelLength = Len(tValue.Text)
    Exit Sub
  End If
  If CLng(tValue.Text) > 16777215 Then
    tValue.Text = "16777215"
    tValue.SetFocus
    Exit Sub
  End If
  
  ' 2. Check if card inserted is an ACOS card
  If Not CheckACOS Then
    Call DisplayOut(0, 0, "Please insert an ACOS card.")
    Exit Sub
  End If
  Call DisplayOut(0, 0, "ACOS card is detected.")
  
  ' 3. Issue INQUIRE ACCOUNT command using any arbitrary data and Credit key
  '    Arbitrary data is 1111h
  For indx = 0 To 3
    tmpArray(indx) = &H1
  Next indx
  retCode = InquireAccount(&H2, tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  ' 4. Issue GET RESPONSE command with Le = 19h or 25 bytes
  retCode = GetResponse("&H19")
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  tmpBalance(1) = RecvBuff(7)
  tmpBalance(2) = RecvBuff(6)
  tmpBalance(2) = tmpBalance(2) * 256
  tmpBalance(3) = RecvBuff(5)
  tmpBalance(3) = tmpBalance(3) * 256
  tmpBalance(3) = tmpBalance(3) * 256
  tmpBalance(0) = tmpBalance(1) + tmpBalance(2) + tmpBalance(3)


  ' 5. Store ACOS card values for TTREFd and ATREF
  For indx = 0 To 3
    TTREFd(indx) = RecvBuff(indx + 21)
  Next indx
  For indx = 0 To 5
    ATREF(indx) = RecvBuff(indx + 8)
  Next indx

  ' 6. Prepare MAC data block: E6 + AMT + TTREFd + ATREF + 00 + 00
  '    use tmpArray as the data block
  Amount = CLng(tValue.Text)
  tmpArray(0) = &HE6
  tmpVal = Int(Amount / 256)
  tmpVal = Int(tmpVal / 256)
  tmpArray(1) = tmpVal Mod 256                  ' Amount MSByte
  tmpVal = Int(Amount / 256)
  tmpArray(2) = tmpVal Mod 256                  ' Amount Middle Byte
  tmpArray(3) = Amount Mod 256                  ' Amount LSByte
  For indx = 0 To 3
    tmpArray(indx + 4) = TTREFd(indx)
  Next indx
  For indx = 0 To 5
    tmpArray(indx + 8) = ATREF(indx)
  Next indx
  tmpArray(13) = tmpArray(13) + 1               ' increment last byte of ATREF
  tmpArray(14) = &H0
  tmpArray(15) = &H0

  ' 7. Generate applicable MAC values, MAC result will be stored in tmpArray
  tmpStr = kDebit.Text
  For indx = 0 To Len(tmpStr) - 1
    tmpKey(indx) = Asc(Mid(tmpStr, indx + 1, 1))
  Next indx
  If rbDES.Value = True Then
    Call mac(tmpArray, tmpKey)
  Else
    Call TripleMAC(tmpArray, tmpKey)
  End If

  ' 8. Format Debit command data and execute debit command
  '    Using tmpArray, the first four bytes are carried over
  tmpVal = Int(Amount / 256)
  tmpVal = Int(tmpVal / 256)
  tmpArray(4) = tmpVal Mod 256                  ' Amount MSByte
  tmpVal = Int(Amount / 256)
  tmpArray(5) = tmpVal Mod 256                  ' Amount Middle Byte
  tmpArray(6) = Amount Mod 256                  ' Amount LSByte
  For indx = 0 To 5
    tmpArray(indx + 7) = ATREF(indx)
  Next indx
  
  If chk_dbc.Value = 0 Then 'Without Debit Certificate
    
    retCode = DebitAmount(tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
    
  Else 'With Debit Certificate
    
    retCode = DebitAmountwithDBC(tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
    
    'Issue GET RESPONSE command with Le = 4h
    retCode = GetResponse("&H4")
    If retCode <> SCARD_S_SUCCESS Then
        Exit Sub
    End If
    
    'Prepare MAC data block: 01 + New Balance + ATC + TTREFD + 00 + 00 + 00
    '    use tmpArray as the data block
    
    Amount = CLng(tValue.Text)
    new_balance = tmpBalance(0) - Amount
    tmpArray(0) = &H1
    
    tmpVal = Int(new_balance / 256)
    tmpVal = Int(tmpVal / 256)
    tmpArray(1) = tmpVal Mod 256                  ' Amount MSByte
    tmpVal = Int(new_balance / 256)
    tmpArray(2) = tmpVal Mod 256                  ' Amount Middle Byte
    tmpArray(3) = new_balance Mod 256                  ' Amount LSByte
    
    tmpVal = Int(Amount / 256)
    tmpVal = Int(tmpVal / 256)
    tmpArray(4) = tmpVal Mod 256                  ' Amount MSByte
    tmpVal = Int(Amount / 256)
    tmpArray(5) = tmpVal Mod 256                  ' Amount Middle Byte
    tmpArray(6) = Amount Mod 256                  ' Amount LSByte
    tmpArray(7) = ATREF(4)
    tmpArray(8) = ATREF(5) + 1                    ' Increment ATC after every transaction
    
    For indx = 0 To 3
        tmpArray(indx + 9) = TTREFd(indx)
    Next indx
    tmpArray(13) = &H0
    tmpArray(14) = &H0
    tmpArray(15) = &H0

    'Generate applicable MAC values, MAC result will be stored in tmpArray
    tmpStr = kDebit.Text
    For indx = 0 To Len(tmpStr) - 1
        tmpKey(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    If rbDES.Value = True Then
        Call mac(tmpArray, tmpKey)
    Else
        Call TripleMAC(tmpArray, tmpKey)
    End If
    
    For i = 0 To 3
        If RecvBuff(i) <> tmpArray(i) Then
            Call DisplayOut(0, 0, "Debit Certificate Failed.")
            chk_dbc.Value = 0
            Exit Sub
        
        End If
    Next i
        
        Call DisplayOut(3, 0, "Debit Certificate Verified.")
        
    
  End If
  
  Call DisplayOut(3, 0, "Debit transaction completed")
  Call ClearTextFields

End Sub

Private Sub bFormat_Click()

  Dim indx As Integer
  Dim tmpStr As String
  Dim tmpArray(0 To 31) As Byte
  
  ' 1. Validate data template
  If Not ValidTemplate Then
    Exit Sub
  End If
  
  ' 2. Check if card inserted is an ACOS card
  If Not CheckACOS Then
    Call DisplayOut(0, 0, "Please insert an ACOS card.")
    Exit Sub
  End If
  Call DisplayOut(0, 0, "ACOS card is detected.")
  
  ' 3. Submit Issuer Code
  retCode = SubmitIC()
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  ' 4. Select FF 02
  retCode = SelectFile(&HFF, &H2)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
  Next indx
  If tmpStr <> "90 00 " Then
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    Exit Sub
  End If

  ' 5. Write to FF 02
  '    This step will define the Option registers,
  '    Security Option registers and Personalization bit
  '    are not set
  If rbDES.Value = True Then    ' DES option only
    tmpArray(0) = &H29          ' 29h  Only REV_DEB, DEB_MAC and Account bits are set
  Else
    tmpArray(0) = &H2B          ' 2Bh  REV_DEB, DEB_MAC, 3-DES and Account bits are set
  End If
  tmpArray(1) = &H0             ' 00    Security option register
  tmpArray(2) = &H3             ' 00    No of user files
  tmpArray(3) = &H0             ' 00    Personalization bit
  retCode = writeRecord(0, &H0, &H4, &H4, tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If
  Call DisplayOut(0, 0, "FF 02 is updated")

  ' 6. Perform a reset for changes in the ACOS to take effect
  retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
  ConnActive = False
  retCode = SCardConnect(hContext, _
                        cbReader.Text, _
                        SCARD_SHARE_EXCLUSIVE, _
                        SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1, _
                        hCard, _
                        Protocol)
  If retCode <> SCARD_S_SUCCESS Then
    Call DisplayOut(1, retCode, "")
    ConnActive = False
    Exit Sub
  End If
  Call DisplayOut(0, 0, "Account files are enabled.")
  ConnActive = True

  ' 7. Submit Issuer Code to write into FF 05 and FF 06
  retCode = SubmitIC()
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  ' 8. Select FF 05
  retCode = SelectFile(&HFF, &H5)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
  Next indx
  If tmpStr <> "90 00 " Then
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    Exit Sub
  End If

  ' 9. Write to FF 05
  ' 9.1. Record 00
  tmpArray(0) = &H0          ' TRANSTYP 0
  tmpArray(1) = &H0          ' (3 bytes
  tmpArray(2) = &H0          '  reserved for
  tmpArray(3) = &H0          '  BALANCE 0)
  retCode = writeRecord(0, &H0, &H4, &H4, tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  ' 9.2.Record 01
  tmpArray(0) = &H0          ' (2 bytes reserved
  tmpArray(1) = &H0          '  for ATC 0)
  tmpArray(2) = &H1          ' Set CHECKSUM 0
  tmpArray(3) = &H0          ' 00h
  retCode = writeRecord(0, &H1, &H4, &H4, tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  ' 9.3. Record 02
  tmpArray(0) = &H0          ' TRANSTYP 1
  tmpArray(1) = &H0          ' (3 bytes
  tmpArray(2) = &H0          '  reserved for
  tmpArray(3) = &H0          '  BALANCE 1)
  retCode = writeRecord(0, &H2, &H4, &H4, tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  ' 9.4.Record 03
  tmpArray(0) = &H0          ' (2 bytes reserved
  tmpArray(1) = &H0          '  for ATC 1)
  tmpArray(2) = &H1          ' Set CHECKSUM 1
  tmpArray(3) = &H0          ' 00h
  retCode = writeRecord(0, &H3, &H4, &H4, tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  ' 9.5.Record 04
  tmpArray(0) = &HFF          ' (3 bytes
  tmpArray(1) = &HFF          '  initialized for
  tmpArray(2) = &HFF          '  MAX BALANCE)
  tmpArray(3) = &H0           ' 00h
  retCode = writeRecord(0, &H4, &H4, &H4, tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If
 
  ' 9.6.Record 05
  tmpArray(0) = &H0           ' (4 bytes
  tmpArray(1) = &H0           '  reserved
  tmpArray(2) = &H0           '  for
  tmpArray(3) = &H0           '  AID)
  retCode = writeRecord(0, &H5, &H4, &H4, tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If
 
  ' 9.7.Record 06
  tmpArray(0) = &H0           ' (4 bytes
  tmpArray(1) = &H0           '  reserved
  tmpArray(2) = &H0           '  for
  tmpArray(3) = &H0           '  TTREF_C)
  retCode = writeRecord(0, &H6, &H4, &H4, tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  ' 9.8.Record 07
  tmpArray(0) = &H0           ' (4 bytes
  tmpArray(1) = &H0           '  reserved
  tmpArray(2) = &H0           '  for
  tmpArray(3) = &H0           '  TTREF_D)
  retCode = writeRecord(0, &H7, &H4, &H4, tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If
  Call DisplayOut(0, 0, "FF 05 is updated")

  ' 10. Select FF 06
  retCode = SelectFile(&HFF, &H6)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
  Next indx
  If tmpStr <> "90 00 " Then
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    Exit Sub
  End If

  ' 11. Write to FF 05
  If rbDES.Value = True Then    ' DES option uses 8-byte key
  
  '  11a.1. Record 00 for Debit key
    tmpStr = kDebit.Text
    For indx = 0 To 7
      tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &H0, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
   
   ' 11a.2. Record 01 for Credit key
    tmpStr = kCredit.Text
    For indx = 0 To 7
      tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &H1, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
   
   ' 11a.3. Record 02 for Certify key
    tmpStr = kCertify.Text
    For indx = 0 To 7
      tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &H2, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If

   ' 11a.4. Record 03 for Revoke Debit key
    tmpStr = kRevDeb.Text
    For indx = 0 To 7
      tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &H3, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
  Else                          ' 3-DES option uses 16-byte key
  
  '  11b.1. Record 04 for Left half of Debit key
    tmpStr = kDebit.Text
    For indx = 0 To 7           ' Left half of Debit key
      tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &H4, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
  
  '  11b.2. Record 00 for Right half of Debit key
    For indx = 8 To 15          ' Right half of Debit key
      tmpArray(indx - 8) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &H0, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
  
  '  11b.3. Record 05 for Left half of Credit key
    tmpStr = kCredit.Text
    For indx = 0 To 7           ' Left half of Credit key
      tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &H5, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
  
  '  11b.4. Record 01 for Right half of Credit key
    For indx = 8 To 15          ' Right half of Credit key
      tmpArray(indx - 8) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &H1, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
  
  '  11b.5. Record 06 for Left half of Certify key
    tmpStr = kCertify.Text
    For indx = 0 To 7           ' Left half of Certify key
      tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &H6, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
  
  '  11b.6. Record 02 for Right half of Certify key
    For indx = 8 To 15          ' Right half of Certify key
      tmpArray(indx - 8) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &H2, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
  
  '  11b.7. Record 07 for Left half of Revoke Debit key
    tmpStr = kRevDeb.Text
    For indx = 0 To 7           ' Left half of Revoke Debit key
      tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &H7, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
  
  '  11b.8. Record 03 for Right half of Revoke Debit key
    For indx = 8 To 15          ' Right half of Revoke Debit key
      tmpArray(indx - 8) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &H3, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
  End If
  
  Call ClearTextFields
  Call DisplayOut(0, 0, "FF 06 is updated")

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

Private Sub bInquire_Click()

  Dim indx As Integer
  Dim tmpStr As String
  Dim tmpArray(0 To 31) As Byte
  Dim tmpBalance(0 To 3) As Long
  Dim tmpKey(0 To 15) As Byte       ' certify key to verify MAC
  Dim LastTran As Byte
  Dim TTREFc(0 To 3) As Byte
  Dim TTREFd(0 To 3) As Byte
  Dim ATREF(0 To 5) As Byte

  ' 1. Check if Certify key is provided
  If Len(kCertify.Text) < kCertify.MaxLength Then
    kCertify.SetFocus
    Exit Sub
  End If
  
  ' 2. Check if card inserted is an ACOS card
  If Not CheckACOS Then
    Call DisplayOut(0, 0, "Please insert an ACOS card.")
    Exit Sub
  End If
  Call DisplayOut(0, 0, "ACOS card is detected.")
  
  ' 3. Issue INQUIRE ACCOUNT command using any arbitrary data and Certify key
  '    Arbitrary data is 1111h
  For indx = 0 To 3
    tmpArray(indx) = &H1
  Next indx
  retCode = InquireAccount(&H2, tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  ' 4. Issue GET RESPONSE command with Le = 19h or 25 bytes
  retCode = GetResponse("&H19")
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  ' 5. Check integrity of data returned by card
  ' 5.1. Build MAC input data
  ' 5.1.1. Extract the info from ACOS card in Dataout
    LastTran = RecvBuff(4)
    tmpBalance(1) = RecvBuff(7)
    tmpBalance(2) = RecvBuff(6)
    tmpBalance(2) = tmpBalance(2) * 256
    tmpBalance(3) = RecvBuff(5)
    tmpBalance(3) = tmpBalance(3) * 256
    tmpBalance(3) = tmpBalance(3) * 256
    tmpBalance(0) = tmpBalance(1) + tmpBalance(2) + tmpBalance(3)
    For indx = 0 To 3
      TTREFc(indx) = RecvBuff(indx + 17)
    Next indx
    For indx = 0 To 3
      TTREFd(indx) = RecvBuff(indx + 21)
    Next indx
    For indx = 0 To 5
      ATREF(indx) = RecvBuff(indx + 8)
    Next indx

  ' 5.1.2. Move data from ACOS card as input to MAC calculations
    tmpArray(4) = RecvBuff(4)          ' 4 BYTE MAC + LAST TRANS TYPE
    For indx = 0 To 2                  ' Copy BALANCE
      tmpArray(indx + 5) = RecvBuff(indx + 5)
    Next indx
    For indx = 0 To 5                  ' Copy ATREF
      tmpArray(indx + 8) = RecvBuff(indx + 8)
    Next indx
    tmpArray(14) = &H0
    tmpArray(15) = &H0
    For indx = 0 To 3                  ' Copy TTREFc
      tmpArray(indx + 16) = TTREFc(indx)
    Next indx
    For indx = 0 To 3                  ' Copy TTREFd
      tmpArray(indx + 20) = TTREFd(indx)
    Next indx

  ' 5.2. Generate applicable MAC values
  tmpStr = kCertify.Text
  For indx = 0 To Len(tmpStr) - 1
    tmpKey(indx) = Asc(Mid(tmpStr, indx + 1, 1))
  Next indx
  If rbDES.Value = True Then
    Call mac(tmpArray, tmpKey)
  Else
    Call TripleMAC(tmpArray, tmpKey)
  End If

  ' 5.3. Compare MAC values
  For indx = 0 To 3
    If tmpArray(indx) <> RecvBuff(indx) Then
      Call DisplayOut(4, 0, "MAC is incorrect, data integrity is jeopardized.")
      Exit For
    End If
  Next indx
  
  ' 6. Display relevant data from ACOS card
  Select Case LastTran
    Case 1
      tmpStr = "DEBIT"
    Case 2
      tmpStr = "REVOKE DEBIT"
    Case 3
      tmpStr = "CREDIT"
    Case Else
      tmpStr = "NOT DEFINED"
  End Select
  Call DisplayOut(3, 0, "Last transaction is " & tmpStr & ".")
  Call ClearTextFields
  tValue.Text = Format(tmpBalance(0), "")

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
    ConnActive = False
  End If
  retCode = SCardReleaseContext(hContext)
  Call InitMenu

End Sub

Private Sub bRevDeb_Click()
  
  Dim indx As Integer
  Dim tmpStr As String
  Dim tmpArray(0 To 31) As Byte
  Dim Amount, tmpVal As Long
  Dim tmpKey(0 To 15) As Byte       ' Revoke Debit key to verify MAC
  Dim TTREFd(0 To 3) As Byte
  Dim ATREF(0 To 5) As Byte

  ' 1. Check if Debit key and valid Transaction value are provided
  If Len(kRevDeb.Text) < kRevDeb.MaxLength Then
    kRevDeb.SetFocus
    Exit Sub
  End If
  If tValue.Text = "" Then
    tValue.SetFocus
    Exit Sub
  End If
  If Not IsNumeric(tValue.Text) Then
    tValue.SetFocus
    tValue.SelStart = 0
    tValue.SelLength = Len(tValue.Text)
    Exit Sub
  End If
  If CLng(tValue.Text) > 16777215 Then
    tValue.Text = "16777215"
    tValue.SetFocus
    Exit Sub
  End If
  
  ' 2. Check if card inserted is an ACOS card
  If Not CheckACOS Then
    Call DisplayOut(0, 0, "Please insert an ACOS card.")
    Exit Sub
  End If
  Call DisplayOut(0, 0, "ACOS card is detected.")
  
  ' 3. Issue INQUIRE ACCOUNT command using any arbitrary data and Revoke Debit key
  '    Arbitrary data is 1111h
  For indx = 0 To 3
    tmpArray(indx) = &H1
  Next indx
  retCode = InquireAccount(&H2, tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  ' 4. Issue GET RESPONSE command with Le = 19h or 25 bytes
  retCode = GetResponse("&H19")
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  ' 5. Store ACOS card values for TTREFd and ATREF
  For indx = 0 To 3
    TTREFd(indx) = RecvBuff(indx + 21)
  Next indx
  For indx = 0 To 5
    ATREF(indx) = RecvBuff(indx + 8)
  Next indx

  ' 5. Store ACOS card values for TTREFd and ATREF
  For indx = 0 To 3
    TTREFd(indx) = RecvBuff(indx + 21)
  Next indx
  For indx = 0 To 5
    ATREF(indx) = RecvBuff(indx + 8)
  Next indx

  ' 6. Prepare MAC data block: E8 + AMT + TTREFd + ATREF + 00 + 00
  '    use tmpArray as the data block
  Amount = CLng(tValue.Text)
  tmpArray(0) = &HE8
  tmpVal = Int(Amount / 256)
  tmpVal = Int(tmpVal / 256)
  tmpArray(1) = tmpVal Mod 256                  ' Amount MSByte
  tmpVal = Int(Amount / 256)
  tmpArray(2) = tmpVal Mod 256                  ' Amount Middle Byte
  tmpArray(3) = Amount Mod 256                  ' Amount LSByte
  For indx = 0 To 3
    tmpArray(indx + 4) = TTREFd(indx)
  Next indx
  For indx = 0 To 5
    tmpArray(indx + 8) = ATREF(indx)
  Next indx
  tmpArray(13) = tmpArray(13) + 1               ' increment last byte of ATREF
  tmpArray(14) = &H0
  tmpArray(15) = &H0

  ' 7. Generate applicable MAC values, MAC result will be stored in tmpArray
  tmpStr = kRevDeb.Text
  For indx = 0 To Len(tmpStr) - 1
    tmpKey(indx) = Asc(Mid(tmpStr, indx + 1, 1))
  Next indx
  If rbDES.Value = True Then
    Call mac(tmpArray, tmpKey)
  Else
    Call TripleMAC(tmpArray, tmpKey)
  End If

  ' 8. Execute Revoke Debit command data and execute credit command
  '    Using tmpArray, the first four bytes storing the MAC value are carried over
  retCode = RevokeDebit(tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  Call DisplayOut(3, 0, "Revoke Debit transaction completed")
  Call ClearTextFields

End Sub

Private Sub cbReader_Click()
  
  bFormat.Enabled = False
  fSecOption.Enabled = False
  fKeys.Enabled = False
  fFunction.Enabled = False
  Call ClearTextFields
  rbDES.Value = False
  rb3DES.Value = False

  If ConnActive Then
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
  End If

End Sub

Private Sub Form_Load()

  Call InitMenu
  
End Sub

Private Sub kCertify_KeyUp(KeyCode As Integer, Shift As Integer)

  If (Len(kCertify.Text) >= kCertify.MaxLength) Then
    kRevDeb.SetFocus
  End If

End Sub

Private Sub kCredit_KeyUp(KeyCode As Integer, Shift As Integer)

  If (Len(kCredit.Text) >= kCredit.MaxLength) Then
    kDebit.SetFocus
  End If

End Sub

Private Sub kDebit_KeyUp(KeyCode As Integer, Shift As Integer)

  If (Len(kDebit.Text) >= kDebit.MaxLength) Then
    kCertify.SetFocus
  End If

End Sub

Private Sub rb3DES_Click()
  
  Call ClearTextFields
  kCredit.MaxLength = 16
  kDebit.MaxLength = 16
  kCertify.MaxLength = 16
  kRevDeb.MaxLength = 16

End Sub

Private Sub rbDES_Click()
  
  Call ClearTextFields
  kCredit.MaxLength = 8
  kDebit.MaxLength = 8
  kCertify.MaxLength = 8
  kRevDeb.MaxLength = 8

End Sub
