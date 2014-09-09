VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form MainEncryption 
   Caption         =   "Using Encryption Options in ACOS3"
   ClientHeight    =   6645
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   8475
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   9
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "Encryption.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   6645
   ScaleWidth      =   8475
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   6720
      TabIndex        =   25
      Top             =   6000
      Width           =   1575
   End
   Begin VB.CommandButton bReset 
      Caption         =   "&Reset"
      Height          =   375
      Left            =   4800
      TabIndex        =   24
      Top             =   6000
      Width           =   1575
   End
   Begin VB.Frame fSubmit 
      Caption         =   "Code Submission"
      Height          =   1935
      Left            =   120
      TabIndex        =   17
      Top             =   4560
      Width           =   3855
      Begin VB.CommandButton bSubmit 
         Caption         =   "&Submit"
         Height          =   375
         Left            =   2160
         TabIndex        =   23
         Top             =   1320
         Width           =   1455
      End
      Begin VB.CommandButton bSet 
         Caption         =   "Set &Value"
         Height          =   375
         Left            =   360
         TabIndex        =   22
         Top             =   1320
         Width           =   1455
      End
      Begin VB.TextBox tValue 
         Height          =   315
         Left            =   1560
         MaxLength       =   8
         TabIndex        =   21
         Top             =   840
         Width           =   2055
      End
      Begin VB.ComboBox cbCode 
         Height          =   330
         ItemData        =   "Encryption.frx":17A2
         Left            =   1560
         List            =   "Encryption.frx":17B8
         TabIndex        =   20
         Top             =   360
         Width           =   2055
      End
      Begin VB.Label Label5 
         Caption         =   "Value"
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
         Left            =   480
         TabIndex        =   19
         Top             =   840
         Width           =   735
      End
      Begin VB.Label Label4 
         Caption         =   "Code"
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
         Left            =   480
         TabIndex        =   18
         Top             =   360
         Width           =   735
      End
   End
   Begin VB.Frame fKey 
      Caption         =   "Key Template"
      Height          =   1935
      Left            =   120
      TabIndex        =   11
      Top             =   2520
      Width           =   3855
      Begin VB.CommandButton bFormat 
         Caption         =   "&Format Card"
         Height          =   375
         Left            =   2055
         TabIndex        =   16
         Top             =   1320
         Width           =   1575
      End
      Begin VB.TextBox tTerminal 
         Height          =   315
         Left            =   1560
         TabIndex        =   15
         Top             =   840
         Width           =   2055
      End
      Begin VB.TextBox tCard 
         Height          =   315
         Left            =   1560
         TabIndex        =   14
         Top             =   360
         Width           =   2055
      End
      Begin VB.Label Label3 
         Caption         =   "Terminal Key"
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
         Left            =   240
         TabIndex        =   13
         Top             =   840
         Width           =   1215
      End
      Begin VB.Label Label2 
         Caption         =   "Card Key"
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
         Left            =   240
         TabIndex        =   12
         Top             =   360
         Width           =   975
      End
   End
   Begin VB.Frame fSecOption 
      Caption         =   "Security Option"
      Height          =   975
      Left            =   2280
      TabIndex        =   6
      Top             =   1440
      Width           =   1695
      Begin VB.OptionButton rb3DES 
         Caption         =   "3-DES"
         Height          =   255
         Left            =   240
         TabIndex        =   10
         Top             =   600
         Width           =   1095
      End
      Begin VB.OptionButton rbDES 
         Caption         =   "DES"
         Height          =   255
         Left            =   240
         TabIndex        =   9
         Top             =   240
         Width           =   1215
      End
   End
   Begin VB.Frame fEncrypt 
      Caption         =   "Encryption Option"
      Height          =   975
      Left            =   120
      TabIndex        =   5
      Top             =   1440
      Width           =   1935
      Begin VB.OptionButton rbEnc 
         Caption         =   "Encrypted"
         Height          =   255
         Left            =   240
         TabIndex        =   8
         Top             =   600
         Width           =   1335
      End
      Begin VB.OptionButton rbNoEnc 
         Caption         =   "Not Encrypted"
         Height          =   255
         Left            =   240
         TabIndex        =   7
         Top             =   240
         Width           =   1575
      End
   End
   Begin VB.CommandButton bConnect 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   2400
      TabIndex        =   4
      Top             =   840
      Width           =   1575
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initialize"
      Height          =   375
      Left            =   360
      TabIndex        =   3
      Top             =   840
      Width           =   1575
   End
   Begin VB.ComboBox cbReader 
      Height          =   330
      Left            =   1680
      TabIndex        =   2
      Top             =   240
      Width           =   2295
   End
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   5535
      Left            =   4080
      TabIndex        =   0
      Top             =   120
      Width           =   4335
      _ExtentX        =   7646
      _ExtentY        =   9763
      _Version        =   393217
      Enabled         =   -1  'True
      ScrollBars      =   2
      TextRTF         =   $"Encryption.frx":17FD
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
Attribute VB_Name = "MainEncryption"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              Encryption.frm
'
'  Description:       This sample program outlines the steps on how to
'                     use the encryption options in ACOS card using
'                     the PC/SC platform.
'
'  Author:            Jose Isagani R. Mission
'
'  Date:              May 28, 2004
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
Dim SendLen, RecvLen As Long
Dim SendBuff(0 To 262) As Byte
Dim RecvBuff(0 To 262) As Byte
Dim SessionKey(0 To 15) As Byte
Dim cKey(0 To 15) As Byte
Dim tKey(0 To 15) As Byte

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
  bReset.Enabled = False
  Call ClearTextFields
  fEncrypt.Enabled = False
  fSecOption.Enabled = False
  fKey.Enabled = False
  fSubmit.Enabled = False
  mMsg.Text = ""
  rbDES.Value = False
  rb3DES.Value = False
  rbNoEnc.Value = False
  rbEnc.Value = False
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

  tCard.Text = ""
  tTerminal.Text = ""
  tValue.Text = ""
  cbCode.ListIndex = -1

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

Private Function StartSession() As Long

  Dim indx As Integer
  Dim tmpStr As String

  Call ClearBuffers
  SendBuff(0) = &H80        ' CLA
  SendBuff(1) = &H84        ' INS
  SendBuff(2) = &H0        ' P1
  SendBuff(3) = &H0         ' P2
  SendBuff(4) = &H8         ' P3
  
  SendLen = &H5
  RecvLen = &HA
  tmpStr = ""
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(0, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    StartSession = retCode
    Exit Function
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx + SendBuff(4))), "00") & " "
  Next indx
  If tmpStr <> "90 00 " Then
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    StartSession = INVALID_SW1SW2
    Exit Function
  End If
  
  StartSession = retCode

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
  SendBuff(3) = &H0          ' P2
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

Private Function ValidTemplate(ByVal valType As Integer) As Boolean

  If Len(tCard.Text) < tCard.MaxLength Then
    tCard.SetFocus
    ValidTemplate = False
    Exit Function
  End If
  
  If Len(tTerminal.Text) < tTerminal.MaxLength Then
    tTerminal.SetFocus
    ValidTemplate = False
    Exit Function
  End If
  
  If valType = 1 Then     ' validation requires code input
    If Len(tValue.Text) < 8 Then
      tValue.SetFocus
      ValidTemplate = False
      Exit Function
    End If
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

Private Function GetResponse() As Long
  
  Dim indx As Integer
  Dim tmpStr As String

  Call ClearBuffers
  SendBuff(0) = &H80        ' CLA
  SendBuff(1) = &HC0        ' INS
  SendBuff(2) = &H0         ' P1
  SendBuff(3) = &H0         ' P2
  SendBuff(4) = &H8         ' Length of Data
  SendLen = 5
  RecvLen = &HA
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

Private Function Authenticate(ByRef DataIn() As Byte) As Long
  
  Dim indx As Integer
  Dim tmpStr As String

  Call ClearBuffers
  SendBuff(0) = &H80        ' CLA
  SendBuff(1) = &H82        ' INS
  SendBuff(2) = &H0         ' P1
  SendBuff(3) = &H0         ' P2
  SendBuff(4) = &H10        ' P3
  For indx = 0 To 15
    SendBuff(indx + 5) = DataIn(indx)
  Next indx
  SendLen = SendBuff(4) + 5
  RecvLen = &HA
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(0, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    Authenticate = retCode
    Exit Function
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
  Next indx
  If ACOSError(RecvBuff(0), RecvBuff(1)) Then
    Authenticate = INVALID_SW1SW2
    Exit Function
  End If
  If tmpStr <> "61 08 " Then
    Call DisplayOut(4, 0, "AUTHENTICATE command failed.")
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    Authenticate = INVALID_SW1SW2
    Exit Function
  End If

  Authenticate = retCode

End Function

Private Function SubmitCode(ByVal CodeType As Byte, ByRef DataIn() As Byte) As Long
  
  Dim indx As Integer
  Dim tmpStr As String

  Call ClearBuffers
  SendBuff(0) = &H80        ' CLA
  SendBuff(1) = &H20        ' INS
  SendBuff(2) = CodeType    ' P1
  SendBuff(3) = &H0         ' P2
  SendBuff(4) = &H8         ' P3
  For indx = 0 To 7
    SendBuff(indx + 5) = DataIn(indx)
  Next indx
  SendLen = SendBuff(4) + 5
  RecvLen = &H2
  For indx = 0 To SendLen - 1
    tmpStr = tmpStr & Format(Hex(SendBuff(indx)), "00") & " "
  Next indx
  retCode = SendAPDUandDisplay(0, tmpStr)
  If retCode <> SCARD_S_SUCCESS Then
    SubmitCode = retCode
    Exit Function
  End If
  tmpStr = ""
  For indx = 0 To 1
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
  Next indx
  If ACOSError(RecvBuff(0), RecvBuff(1)) Then
    SubmitCode = INVALID_SW1SW2
    Exit Function
  End If
  If tmpStr <> "90 00 " Then
    Call DisplayOut(4, 0, "SUBMIT CODE command failed.")
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    SubmitCode = INVALID_SW1SW2
    Exit Function
  End If

  SubmitCode = retCode

End Function

Private Function GetSessionKey() As Long

  Dim indx As Integer
  Dim tmpStr As String
  Dim CRnd(0 To 7) As Byte            ' Card random number
  Dim TRnd(0 To 7) As Byte            ' Terminal random number
  Dim tmpArray(0 To 31) As Byte
  Dim ReverseKey(0 To 15) As Byte     ' Reverse of Terminal Key

  ' 1. Get Card Key and Terminal Key from Key Template
  tmpStr = tCard.Text
  For indx = 0 To tCard.MaxLength - 1
    cKey(indx) = Asc(Mid(tmpStr, indx + 1, 1))
  Next indx
  tmpStr = tTerminal.Text
  For indx = 0 To tTerminal.MaxLength - 1
    tKey(indx) = Asc(Mid(tmpStr, indx + 1, 1))
  Next indx

  ' 2. Generate random number from card
  retCode = StartSession()
  If retCode <> SCARD_S_SUCCESS Then
    GetSessionKey = retCode
    Exit Function
  End If

  ' 3. Store the random number generated by the card to Crnd
  For indx = 0 To 7
    CRnd(indx) = RecvBuff(indx)
  Next indx

  ' 4. Encrypt Random No (CRnd) with Terminal Key (tKey)
  '    tmpArray will hold the 8-byte Enrypted number
  For indx = 0 To 7
    tmpArray(indx) = CRnd(indx)
  Next indx
  If rbDES.Value = True Then
    Call DES(tmpArray, tKey)
  Else
    Call TripleDES(tmpArray, tKey)
  End If

  ' 5. Issue Authenticate command using 8-byte Encrypted No (tmpArray)
  '    and Random Terminal number (TRnd)
  For indx = 0 To 7
    tmpArray(indx + 8) = TRnd(indx)
  Next indx
  retCode = Authenticate(tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    GetSessionKey = retCode
    Exit Function
  End If

  ' 6. Compute for Session Key
  If rbDES.Value = True Then
    
    ' 4.2a. for single DES
    ' prepare SessionKey
    ' SessionKey = DES (DES(RNDc, KC) XOR RNDt, KT)

    ' calculate DES(cRnd,cKey)
    For indx = 0 To 7
      tmpArray(indx) = CRnd(indx)
    Next indx
    Call DES(tmpArray, cKey)

    ' XOR the result with tRnd
    For indx = 0 To 7
      tmpArray(indx) = tmpArray(indx) Xor TRnd(indx)
    Next indx

    ' DES the result with tKey
    Call DES(tmpArray, tKey)

    ' temp now holds the SessionKey
    For indx = 0 To 7
      SessionKey(indx) = tmpArray(indx)
    Next indx
  Else
    
    ' 4.2b. for triple DES
    ' prepare SessionKey
    ' Left half SessionKey =  3DES (3DES (CRnd, cKey), tKey)
    ' Right half SessionKey = 3DES (TRnd, REV (tKey))

    ' tmpArray = 3DES (CRnd, cKey)
    For indx = 0 To 7
      tmpArray(indx) = CRnd(indx)
    Next indx
    Call TripleDES(tmpArray, cKey)

    ' tmpArray = 3DES (tmpArray, tKey)
    Call TripleDES(tmpArray, tKey)

    ' tmpArray holds the left half of SessionKey
    For indx = 0 To 7
      SessionKey(indx) = tmpArray(indx)
    Next indx

    ' compute ReverseKey of tKey
    ' just swap its left side with right side
    ' ReverseKey = right half of tKey + left half of tKey
    For indx = 0 To 7
      ReverseKey(indx) = tKey(8 + indx)
    Next indx
    For indx = 0 To 7
      ReverseKey(8 + indx) = tKey(indx)
    Next indx

    ' compute tmpArray = 3DES (TRnd, ReverseKey)
    For indx = 0 To 7
      tmpArray(indx) = TRnd(indx)
    Next indx
    Call TripleDES(tmpArray, ReverseKey)

    ' tmpArray holds the right half of SessionKey
    For indx = 0 To 7
      SessionKey(indx + 8) = tmpArray(indx)
    Next indx
  End If

GetSessionKey = retCode

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
  Call ClearTextFields
  fEncrypt.Enabled = True
  fKey.Enabled = True
  fSubmit.Enabled = True
  cbCode.ListIndex = 0
  rbDES.Value = True
  rbNoEnc.Value = True

End Sub

Private Sub bFormat_Click()
  Dim indx As Integer
  Dim tmpStr As String
  Dim tmpArray(0 To 31) As Byte
  
  ' 1. Validate data template
  If Not ValidTemplate(0) Then
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
  '    This step will define the Option registers and Security Option registers.
  '    Personalization bit is not set. Although Secret Codes may be set
  '    individually, this program adopts uniform encryption option for
  '    all codes to simplify coding. Issuer Code (IC) is not encrypted
  '    to remove risk of locking the ACOS card for wrong IC submission.
  If rbDES.Value = True Then    ' DES option only
    tmpArray(0) = &H0           ' 00h  3-DES is not set
  Else
    tmpArray(0) = &H2           ' 02h  3-DES is enabled
  End If
  If rbNoEnc.Value = True Then  ' Security Option register
    tmpArray(1) = &H0           ' 00h  Encryption is not set
  Else
    tmpArray(1) = &H7E          ' Encryption on all codes, except IC, enabled
  End If
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

  ' 7. Submit Issuer Code to write into FF 03
  retCode = SubmitIC()
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  ' 8. Select FF 03
  retCode = SelectFile(&HFF, &H3)
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

  ' 9. Write to FF 03
  If rbDES.Value = True Then    ' DES option uses 8-byte key
  
  '  9a.1. Record 02 for Card key
    tmpStr = tCard.Text
    For indx = 0 To 7
      tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &H2, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
  
  '  9a.2. Record 03 for Terminal key
    tmpStr = tTerminal.Text
    For indx = 0 To 7
      tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &H3, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
  Else                          ' 3-DES option uses 16-byte key

  '  9b.1. Write Record 02 for Left half of Card key
    tmpStr = tCard.Text
    For indx = 0 To 7           ' Left half of Card key
      tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &H2, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
  
  '  9b.2. Record 12 for Right half of Card key
    For indx = 8 To 15          ' Right half of Card key
      tmpArray(indx - 8) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &HC, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
  
  '  9b.3. Write Record 03 for Left half of Terminal key
    tmpStr = tTerminal.Text
    For indx = 0 To 7           ' Left half of Terminal key
      tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &H3, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
  
  '  9b.4. Record 13 for Right half of Terminal key
    For indx = 8 To 15          ' Right half of Terminal key
      tmpArray(indx - 8) = Asc(Mid(tmpStr, indx + 1, 1))
    Next indx
    retCode = writeRecord(0, &HD, &H8, &H8, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
  End If
  
  Call ClearTextFields
  Call DisplayOut(0, 0, "FF 03 is updated")

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

Private Sub bReset_Click()

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
  Dim tmpArray(0 To 31) As Byte
  Dim tmpByte As Byte
  
  ' 1. Validate data template
  If Not ValidTemplate(1) Then
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

  ' 4. Select FF 03
  retCode = SelectFile(&HFF, &H3)
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

  ' 5. Get Code Input
  tmpStr = tValue.Text
  For indx = 0 To 7
    tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
  Next indx
  
  Select Case cbCode.ListIndex
    Case 0
      tmpByte = &H1
      tmpStr = "PIN Code"
    Case 1
      tmpByte = &H5
      tmpStr = "Application Code 1"
    Case 2
      tmpByte = &H6
      tmpStr = "Application Code 2"
    Case 3
      tmpByte = &H7
      tmpStr = "Application Code 3"
    Case 4
      tmpByte = &H8
      tmpStr = "Application Code 4"
    Case 5
      tmpByte = &H9
      tmpStr = "Application Code 5"
  End Select
  
  ' 6. Write to corresponding record in FF 03
  retCode = writeRecord(0, tmpByte, &H8, &H8, tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If

  Call DisplayOut(0, 0, tmpStr & " changed successfully.")

End Sub

Private Sub bSubmit_Click()
  
  Dim indx As Integer
  Dim tmpStr As String
  Dim tmpArray(0 To 31) As Byte
  Dim tmpByte As Byte
  
  ' 1. Validate data template
  If Not ValidTemplate(1) Then
    Exit Sub
  End If
  
  ' 2. Check if card inserted is an ACOS card
  If Not CheckACOS Then
    Call DisplayOut(0, 0, "Please insert an ACOS card.")
    Exit Sub
  End If
  Call DisplayOut(0, 0, "ACOS card is detected.")
  
  ' 3. Get Code input
  tmpStr = tValue.Text
  For indx = 0 To 7
    tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
  Next indx
  If rbEnc.Value = True Then
    retCode = GetSessionKey
    If retCode <> SCARD_S_SUCCESS Then
      Exit Sub
    End If
    If rbDES.Value = True Then
      Call DES(tmpArray, SessionKey)
    Else
      Call TripleDES(tmpArray, SessionKey)
    End If
  End If
  
  Select Case cbCode.ListIndex
    Case 0
      tmpByte = &H6
      tmpStr = "PIN Code"
    Case 1
      tmpByte = &H1
      tmpStr = "Application Code 1"
    Case 2
      tmpByte = &H2
      tmpStr = "Application Code 2"
    Case 3
      tmpByte = &H3
      tmpStr = "Application Code 3"
    Case 4
      tmpByte = &H4
      tmpStr = "Application Code 4"
    Case 5
      tmpByte = &H5
      tmpStr = "Application Code 5"
  End Select
  
  ' 4. Submit Issuer Code
  retCode = SubmitCode(tmpByte, tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
    Exit Sub
  End If
  
  Call DisplayOut(0, 0, tmpStr & " submitted successfully.")

End Sub

Private Sub cbReader_Click()
  
  fSecOption.Enabled = False
  fEncrypt.Enabled = False
  fKey.Enabled = False
  Call ClearTextFields
  rbDES.Value = False
  rb3DES.Value = False
  rbNoEnc.Value = False
  rbEnc.Value = False
  cbCode.ListIndex = -1

  If ConnActive Then
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
  End If

End Sub

Private Sub Form_Load()

  Call InitMenu
  
End Sub

Private Sub rb3DES_Click()
  
  tCard.Text = ""
  tTerminal.Text = ""
  tCard.MaxLength = 16
  tTerminal.MaxLength = 16

End Sub

Private Sub rbDES_Click()

  tCard.Text = ""
  tTerminal.Text = ""
  tCard.MaxLength = 8
  tTerminal.MaxLength = 8
  
End Sub

Private Sub rbEnc_Click()

  tCard.Text = ""
  tTerminal.Text = ""
  rbDES.Value = True
  fSecOption.Enabled = True

End Sub

Private Sub rbNoEnc_Click()

  tCard.Text = ""
  tTerminal.Text = ""
  tCard.MaxLength = 8
  tTerminal.MaxLength = 8
  rbDES.Value = True
  rb3DES.Value = False
  fSecOption.Enabled = False
  
End Sub

Private Sub tCard_KeyUp(KeyCode As Integer, Shift As Integer)

  If (Len(tCard.Text) >= tCard.MaxLength) Then
    tTerminal.SetFocus
  End If

End Sub
