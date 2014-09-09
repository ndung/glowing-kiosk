VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form MainCreateFiles 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Creating Files in ACOS card"
   ClientHeight    =   4845
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   7935
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   9
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "CreateFiles.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4845
   ScaleWidth      =   7935
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   960
      TabIndex        =   7
      Top             =   4080
      Width           =   1695
   End
   Begin VB.CommandButton bReset 
      Caption         =   "&Reset"
      Height          =   375
      Left            =   960
      TabIndex        =   6
      Top             =   3360
      Width           =   1695
   End
   Begin VB.CommandButton bCreate 
      Caption         =   "Create &Files"
      Height          =   375
      Left            =   960
      TabIndex        =   5
      Top             =   2640
      Width           =   1695
   End
   Begin VB.CommandButton bConnect 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   960
      TabIndex        =   4
      Top             =   1920
      Width           =   1695
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initialize"
      Height          =   375
      Left            =   960
      TabIndex        =   3
      Top             =   1200
      Width           =   1695
   End
   Begin VB.ComboBox cbReader 
      Height          =   330
      Left            =   120
      TabIndex        =   2
      Top             =   600
      Width           =   2535
   End
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   4575
      Left            =   2880
      TabIndex        =   0
      Top             =   120
      Width           =   4935
      _ExtentX        =   8705
      _ExtentY        =   8070
      _Version        =   393217
      Enabled         =   -1  'True
      ScrollBars      =   2
      TextRTF         =   $"CreateFiles.frx":17A2
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
      TabIndex        =   1
      Top             =   240
      Width           =   1455
   End
End
Attribute VB_Name = "MainCreateFiles"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              MainCreateFiles.frm
'
'  Description:       This sample program outlines the steps on how to
'                     create user-defined files in ACOS smart card
'                     using the PC/SC platform.
'
'  Author:            Jose Isagani R. Mission
'
'  Date:              May 24, 2004
'
'  Revision Trail:   (Date/Author/Description)
'  (June 24, 2008 / M.J.E.C. Castillo / Added File Access Flag bit to FF 04)
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
  bCreate.Enabled = False
  bReset.Enabled = False
  mMsg.Text = ""
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

Private Function writeRecord(ByVal caseType As Integer, ByVal RecNo As Byte, ByVal maxLen As Byte, _
                             ByVal DataLen As Byte, ByRef ApduIn() As Byte) As Long

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
  SendBuff(4) = DataLen     ' Length of Data
  
  For indx = 0 To DataLen - 1
    
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
  bCreate.Enabled = True

End Sub

Private Sub bCreate_Click()
  
  Dim indx As Integer
  Dim tmpStr As String
  Dim tmpArray(0 To 31) As Byte

  ' 1. Send IC Code
  retCode = SubmitIC()
  
  If retCode <> SCARD_S_SUCCESS Then
  
    Exit Sub
    
  End If

  ' 2. Select FF 02
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

  ' 3. Write to FF 02
  '    This will create 3 User files, no Option registers and
  '    Security Option registers defined, Personalization bit
  '    is not set
  tmpArray(0) = &H0      ' 00    Option registers
  tmpArray(1) = &H0      ' 00    Security option register
  tmpArray(2) = &H3      ' 03    No of user files
  tmpArray(3) = &H0      ' 00    Personalization bit
  retCode = writeRecord(0, &H0, &H4, &H4, tmpArray)
  
  If retCode <> SCARD_S_SUCCESS Then
  
    Exit Sub
    
  End If
  
  Call DisplayOut(0, 0, "FF 02 is updated")

  ' 4. Perform a reset for changes in the ACOS to take effect
  retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
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
  
    Call DisplayOut(0, 0, "Card reset is successful.")
    
  End If

  ' 5. Select FF 04
  retCode = SelectFile(&HFF, &H4)
  
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

  ' 6. Send IC Code
  retCode = SubmitIC()
  
  If retCode <> SCARD_S_SUCCESS Then
  
    Exit Sub
    
  End If

  ' 7. Write to FF 04
  ' 7.1. Write to first record of FF 04
  tmpArray(0) = &H5       ' 5     Record length
  tmpArray(1) = &H3       ' 3     No of records
  tmpArray(2) = &H0       ' 00    Read security attribute
  tmpArray(3) = &H0       ' 00    Write security attribute
  tmpArray(4) = &HAA      ' AA    File identifier
  tmpArray(5) = &H11      ' 11    File identifier
  tmpArray(6) = &H0       ' File Access Flag
  retCode = writeRecord(0, &H0, &H7, &H7, tmpArray)
  
  If retCode <> SCARD_S_SUCCESS Then
  
    Exit Sub
    
  End If
  
  Call DisplayOut(0, 0, "User File AA 11 is defined")

  ' 7.2. Write to second record of FF 04
  tmpArray(0) = &HA       ' 10    Record length
  tmpArray(1) = &H2       ' 2     No of records
  tmpArray(2) = &H0       ' 00    Read security attribute
  tmpArray(3) = &H0       ' 00    Write security attribute
  tmpArray(4) = &HBB      ' BB    File identifier
  tmpArray(5) = &H22      ' 22    File identifier
  tmpArray(6) = &H0       ' File Access Flag
  retCode = writeRecord(0, &H1, &H7, &H7, tmpArray)
  
  If retCode <> SCARD_S_SUCCESS Then
  
    Exit Sub
    
  End If
  
  Call DisplayOut(0, 0, "User File BB 22 is defined")

  ' 7.3. Write to third record of FF 04
  tmpArray(0) = &H6       ' 6     Record length
  tmpArray(1) = &H4       ' 4     No of records
  tmpArray(2) = &H0       ' 00    Read security attribute
  tmpArray(3) = &H0       ' 00    Write security attribute
  tmpArray(4) = &HCC      ' CC    File identifier
  tmpArray(5) = &H33      ' 33    File identifier
  tmpArray(6) = &H0       ' File Access Flag
  retCode = writeRecord(0, &H2, &H7, &H7, tmpArray)
  If retCode <> SCARD_S_SUCCESS Then
  
    Exit Sub
    
  End If
  
  Call DisplayOut(0, 0, "User File CC 33 is defined")

  ' 8. Select 3 User Files created previously for validation
  ' 8.1. Select User File AA 11
  retCode = SelectFile(&HAA, &H11)
  
  If retCode <> SCARD_S_SUCCESS Then
  
    Exit Sub
    
  End If
  
  tmpStr = ""
  
  For indx = 0 To 1
  
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    
  Next indx
  
  If tmpStr <> "91 00 " Then
  
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    Exit Sub
    
  End If
  
  Call DisplayOut(0, 0, "User File AA 11 is selected")

  ' 8.2. Select User File BB 22
  retCode = SelectFile(&HBB, &H22)
  
  If retCode <> SCARD_S_SUCCESS Then
  
    Exit Sub
    
  End If
  
  tmpStr = ""
  
  For indx = 0 To 1
  
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    
  Next indx
  
  If tmpStr <> "91 01 " Then
  
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    Exit Sub
    
  End If
  
  Call DisplayOut(0, 0, "User File BB 22 is selected")

  ' 8.3. Select User File CC 33
  retCode = SelectFile(&HCC, &H33)
  
  If retCode <> SCARD_S_SUCCESS Then
  
    Exit Sub
    
  End If
  
  tmpStr = ""
  
  For indx = 0 To 1
  
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    
  Next indx
  
  If tmpStr <> "91 02 " Then
  
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    Exit Sub
    
  End If
  
  Call DisplayOut(0, 0, "User File CC 33 is selected")

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

Private Sub cbReader_Click()

  bCreate.Enabled = False
  
  If ConnActive Then
  
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
    
  End If

End Sub

Private Sub Form_Load()

  Call InitMenu

End Sub
