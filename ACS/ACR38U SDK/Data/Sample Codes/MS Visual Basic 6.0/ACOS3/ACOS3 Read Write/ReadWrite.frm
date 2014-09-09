VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form MainReadWrite 
   Caption         =   "Reading and Writing to ACOS3 card"
   ClientHeight    =   7095
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   9720
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   9
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "ReadWrite.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   7095
   ScaleWidth      =   9720
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton bQuuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   2640
      TabIndex        =   14
      Top             =   6360
      Width           =   1695
   End
   Begin VB.CommandButton bReset 
      Caption         =   "&Reset"
      Height          =   375
      Left            =   600
      TabIndex        =   13
      Top             =   6360
      Width           =   1695
   End
   Begin VB.TextBox tData 
      Height          =   375
      Left            =   240
      TabIndex        =   12
      Top             =   5640
      Width           =   4095
   End
   Begin VB.Frame fFunction 
      Height          =   2055
      Left            =   1920
      TabIndex        =   7
      Top             =   3000
      Width           =   2415
      Begin VB.CommandButton bWrite 
         Caption         =   "&Write"
         Height          =   375
         Left            =   480
         TabIndex        =   16
         Top             =   1320
         Width           =   1455
      End
      Begin VB.CommandButton bRead 
         Caption         =   "R&ead"
         Height          =   375
         Left            =   480
         TabIndex        =   15
         Top             =   600
         Width           =   1455
      End
   End
   Begin VB.Frame fUserFile 
      Caption         =   "User File"
      Height          =   2055
      Left            =   120
      TabIndex        =   6
      Top             =   3000
      Width           =   1695
      Begin VB.OptionButton rbCC33 
         Caption         =   "CC 33"
         Height          =   255
         Left            =   360
         TabIndex        =   10
         Top             =   1440
         Width           =   975
      End
      Begin VB.OptionButton rbBB22 
         Caption         =   "BB 22"
         Height          =   210
         Left            =   360
         TabIndex        =   9
         Top             =   960
         Width           =   1095
      End
      Begin VB.OptionButton rbAA11 
         Caption         =   "AA 11"
         Height          =   255
         Left            =   360
         TabIndex        =   8
         Top             =   480
         Width           =   1095
      End
   End
   Begin VB.CommandButton bFormat 
      Caption         =   "&Format Card"
      Height          =   375
      Left            =   2640
      TabIndex        =   5
      Top             =   2400
      Width           =   1695
   End
   Begin VB.CommandButton bConnect 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   2640
      TabIndex        =   4
      Top             =   1680
      Width           =   1695
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initialize"
      Height          =   375
      Left            =   2640
      TabIndex        =   3
      Top             =   960
      Width           =   1695
   End
   Begin VB.ComboBox cbReader 
      Height          =   330
      Left            =   1800
      TabIndex        =   2
      Top             =   360
      Width           =   2535
   End
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   6855
      Left            =   4560
      TabIndex        =   0
      Top             =   120
      Width           =   5055
      _ExtentX        =   8916
      _ExtentY        =   12091
      _Version        =   393217
      Enabled         =   -1  'True
      ScrollBars      =   2
      TextRTF         =   $"ReadWrite.frx":17A2
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
      TabIndex        =   11
      Top             =   5280
      Width           =   2055
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
      Top             =   360
      Width           =   1455
   End
End
Attribute VB_Name = "MainReadWrite"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              ReadWrite.frm
'
'  Description:       This sample program outlines the steps on how to
'                     format the ACOS card and how to  read or write
'                     data into it using the PC/SC platform.
'
'  Author:            Jose Isagani R. Mission
'
'  Date:              May 25, 2004
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
  bFormat.Enabled = False
  bReset.Enabled = False
  fUserFile.Enabled = False
  fFunction.Enabled = False
  mMsg.Text = ""
  tData.Text = ""
  tData.Enabled = False
  rbAA11.Value = False
  rbBB22.Value = False
  rbCC33.Value = False
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
  fUserFile.Enabled = True
  rbAA11.Value = True
  fFunction.Enabled = True
  tData.Enabled = True
  tData.Text = ""
  tData.MaxLength = 10

End Sub

Private Sub bFormat_Click()
  
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
  tmpArray(0) = &HA       ' 10    Record length
  tmpArray(1) = &H3       ' 3     No of records
  tmpArray(2) = &H0       ' 00    Read security attribute
  tmpArray(3) = &H0       ' 00    Write security attribute
  tmpArray(4) = &HAA      ' AA    File identifier
  tmpArray(5) = &H11      ' 11    File identifier
  tmpArray(6) = &H0       ' File Access Flag:
  
  retCode = writeRecord(0, &H0, &H7, &H7, tmpArray)
  
  If retCode <> SCARD_S_SUCCESS Then
  
    Exit Sub
    
  End If
  
  Call DisplayOut(0, 0, "User File AA 11 is defined")

  ' 7.2. Write to second record of FF 04
  tmpArray(0) = &H10      ' 16    Record length
  tmpArray(1) = &H2       ' 2     No of records
  tmpArray(2) = &H0       ' 00    Read security attribute
  tmpArray(3) = &H0       ' 00    Write security attribute
  tmpArray(4) = &HBB      ' BB    File identifier
  tmpArray(5) = &H22      ' 22    File identifier
  tmpArray(6) = &H0       ' File Access Flag:
  
  retCode = writeRecord(0, &H1, &H7, &H7, tmpArray)
  
  If retCode <> SCARD_S_SUCCESS Then
  
    Exit Sub
    
  End If
  
  Call DisplayOut(0, 0, "User File BB 22 is defined")

  ' 7.3. Write to third record of FF 04
  tmpArray(0) = &H20      ' 32    Record length
  tmpArray(1) = &H4       ' 4     No of records
  tmpArray(2) = &H0       ' 00    Read security attribute
  tmpArray(3) = &H0       ' 00    Write security attribute
  tmpArray(4) = &HCC      ' CC    File identifier
  tmpArray(5) = &H33      ' 33    File identifier
  tmpArray(6) = &H0       ' File Access Flag:
  
  retCode = writeRecord(0, &H2, &H7, &H7, tmpArray)
  
  If retCode <> SCARD_S_SUCCESS Then
  
    Exit Sub
    
  End If
  
  Call DisplayOut(0, 0, "User File CC 33 is defined")

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

Private Sub bQuuit_Click()

  If ConnActive Then
  
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
    
  End If
  
  retCode = SCardReleaseContext(hContext)
  Unload Me


End Sub

Private Sub bRead_Click()

  Dim indx As Integer
  Dim tmpStr, ChkStr As String
  Dim HiAddr, LoAddr, dataLen As Byte
  
  ' 1. Check User File selected by user
  If rbAA11.Value = True Then
  
    HiAddr = &HAA
    LoAddr = &H11
    dataLen = &HA
    ChkStr = "91 00 "
    
  End If

  If rbBB22.Value = True Then
  
    HiAddr = &HBB
    LoAddr = &H22
    dataLen = &H10
    ChkStr = "91 01 "
    
  End If

  If rbCC33.Value = True Then
  
    HiAddr = &HCC
    LoAddr = &H33
    dataLen = &H20
    ChkStr = "91 02 "
    
  End If
  
  ' 2. Select User File
  retCode = SelectFile(HiAddr, LoAddr)
  
  If retCode <> SCARD_S_SUCCESS Then
  
    Exit Sub
    
  End If
  
  tmpStr = ""
  
  For indx = 0 To 1
  
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    
  Next indx
  
  If tmpStr <> ChkStr Then
  
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    Exit Sub
    
  End If

  ' 3. Read First Record of User File selected
  retCode = readRecord(&H0, dataLen)
  
  If retCode <> SCARD_S_SUCCESS Then
  
    Exit Sub
    
  End If

  ' 4. Display data read from card to textbox
  tmpStr = ""
  indx = 0
  
  While (RecvBuff(indx) <> &H0)
  
    If indx < tData.MaxLength Then
    
      tmpStr = tmpStr & Chr(RecvBuff(indx))
      
    End If
    
    indx = indx + 1
    
  Wend
  
  tData.Text = tmpStr
  Call DisplayOut(0, 0, "Data read from card is displayed in Text Box.")

End Sub

Private Sub bReset_Click()

  If ConnActive Then
  
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
    
  End If
  
  retCode = SCardReleaseContext(hContext)
  Call InitMenu

End Sub

Private Sub bWrite_Click()

  Dim indx As Integer
  Dim tmpStr, ChkStr As String
  Dim HiAddr, LoAddr, dataLen As Byte
  Dim tmpArray(0 To 56) As Byte
  
  ' 1. Validate input template
  If tData.Text = "" Then
  
    tData.SetFocus
    Exit Sub
    
  End If
  
  ' 2. Check User File selected by user
  If rbAA11.Value = True Then
  
    HiAddr = &HAA
    LoAddr = &H11
    dataLen = &HA
    ChkStr = "91 00 "
    
  End If

  If rbBB22.Value = True Then
  
    HiAddr = &HBB
    LoAddr = &H22
    dataLen = &H10
    ChkStr = "91 01 "
    
  End If

  If rbCC33.Value = True Then
  
    HiAddr = &HCC
    LoAddr = &H33
    dataLen = &H20
    ChkStr = "91 02 "
    
  End If
  
  ' 3. Select User File
  retCode = SelectFile(HiAddr, LoAddr)
  
  If retCode <> SCARD_S_SUCCESS Then
  
    Exit Sub
    
  End If
  
  tmpStr = ""
  
  For indx = 0 To 1
  
    tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00") & " "
    
  Next indx
  
  If tmpStr <> ChkStr Then
  
    Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
    Exit Sub
    
  End If
  
  ' 4. Write data from text box to card
  tmpStr = tData.Text
  
  For indx = 0 To Len(tmpStr) - 1
  
    tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
    
  Next indx
  
  retCode = writeRecord(1, &H0, dataLen, Len(tmpStr), tmpArray)
  
  If retCode <> SCARD_S_SUCCESS Then
  
    Exit Sub
    
  End If
  
  Call DisplayOut(0, 0, "Data read from Text Box is written to card.")
  
  
End Sub

Private Sub cbReader_Click()

  bFormat.Enabled = False
  tData.Text = ""
  tData.Enabled = False
  rbAA11.Value = False
  rbBB22.Value = False
  rbCC33.Value = False
  fUserFile.Enabled = False
  fFunction.Enabled = False
  
  If ConnActive Then
  
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
    
  End If

End Sub

Private Sub Form_Load()
  
  Call InitMenu

End Sub

Private Sub rbAA11_Click()

  tData.Text = ""
  tData.MaxLength = 10
  
End Sub

Private Sub rbBB22_Click()

  tData.Text = ""
  tData.MaxLength = 16

End Sub

Private Sub rbCC33_Click()

  tData.Text = ""
  tData.MaxLength = 32

End Sub
