VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form MainACOSBin 
   Caption         =   "Using Binary Files in ACOS3"
   ClientHeight    =   6525
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   9480
   Icon            =   "ACOSBinProg.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   6525
   ScaleWidth      =   9480
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   7680
      TabIndex        =   18
      Top             =   6000
      Width           =   1695
   End
   Begin VB.CommandButton bReset 
      Caption         =   "R&eset"
      Height          =   375
      Left            =   5880
      TabIndex        =   17
      Top             =   6000
      Width           =   1695
   End
   Begin VB.CommandButton bClear 
      Caption         =   "C&lear"
      Height          =   375
      Left            =   4080
      TabIndex        =   16
      Top             =   6000
      Width           =   1695
   End
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   5775
      Left            =   4080
      TabIndex        =   28
      Top             =   120
      Width           =   5295
      _ExtentX        =   9340
      _ExtentY        =   10186
      _Version        =   393217
      Enabled         =   -1  'True
      ScrollBars      =   2
      TextRTF         =   $"ACOSBinProg.frx":17A2
   End
   Begin VB.Frame frReadWrite 
      Caption         =   "Read and Write to Binary File"
      Height          =   3495
      Left            =   120
      TabIndex        =   23
      Top             =   2880
      Width           =   3855
      Begin VB.CommandButton bBinWrite 
         Caption         =   "&Write Binary"
         Height          =   375
         Left            =   2160
         TabIndex        =   15
         Top             =   2880
         Width           =   1575
      End
      Begin VB.CommandButton bBinRead 
         Caption         =   "&Read Binary"
         Height          =   375
         Left            =   2160
         TabIndex        =   14
         Top             =   2400
         Width           =   1575
      End
      Begin VB.TextBox tData 
         Height          =   975
         Left            =   120
         MultiLine       =   -1  'True
         TabIndex        =   13
         Top             =   1320
         Width           =   3615
      End
      Begin VB.TextBox tLen 
         Height          =   285
         Left            =   3120
         MaxLength       =   2
         TabIndex        =   12
         Top             =   720
         Width           =   495
      End
      Begin VB.TextBox tOffset2 
         Height          =   285
         Left            =   1440
         MaxLength       =   2
         TabIndex        =   11
         Top             =   720
         Width           =   495
      End
      Begin VB.TextBox tOffset1 
         Height          =   285
         Left            =   960
         MaxLength       =   2
         TabIndex        =   10
         Top             =   720
         Width           =   495
      End
      Begin VB.TextBox tFID2 
         Height          =   285
         Left            =   1440
         MaxLength       =   2
         TabIndex        =   9
         Top             =   360
         Width           =   495
      End
      Begin VB.TextBox tFID1 
         Height          =   285
         Left            =   960
         MaxLength       =   2
         TabIndex        =   8
         Top             =   360
         Width           =   495
      End
      Begin VB.Label Label7 
         Caption         =   "Data"
         Height          =   255
         Left            =   120
         TabIndex        =   27
         Top             =   1080
         Width           =   735
      End
      Begin VB.Label Label6 
         Caption         =   "Length"
         Height          =   255
         Left            =   2520
         TabIndex        =   26
         Top             =   720
         Width           =   855
      End
      Begin VB.Label Label5 
         Caption         =   "File Offset"
         Height          =   255
         Left            =   120
         TabIndex        =   25
         Top             =   720
         Width           =   735
      End
      Begin VB.Label Label4 
         Caption         =   "File ID"
         Height          =   255
         Left            =   120
         TabIndex        =   24
         Top             =   360
         Width           =   495
      End
   End
   Begin VB.Frame frFormat 
      Caption         =   "Card Format Routine"
      Height          =   1215
      Left            =   120
      TabIndex        =   20
      Top             =   1560
      Width           =   3855
      Begin VB.CommandButton bFormat 
         Caption         =   "&Format Card"
         Height          =   375
         Left            =   2160
         TabIndex        =   7
         Top             =   600
         Width           =   1575
      End
      Begin VB.TextBox tFileLen2 
         Height          =   285
         Left            =   1440
         MaxLength       =   2
         TabIndex        =   6
         Top             =   720
         Width           =   495
      End
      Begin VB.TextBox tFileLen1 
         Height          =   285
         Left            =   960
         MaxLength       =   2
         TabIndex        =   5
         Top             =   720
         Width           =   495
      End
      Begin VB.TextBox tFileID2 
         Height          =   285
         Left            =   1440
         MaxLength       =   2
         TabIndex        =   4
         Top             =   360
         Width           =   495
      End
      Begin VB.TextBox tFileID1 
         Height          =   285
         Left            =   960
         MaxLength       =   2
         TabIndex        =   3
         Top             =   360
         Width           =   495
      End
      Begin VB.Label Label3 
         Caption         =   "Length"
         Height          =   255
         Left            =   120
         TabIndex        =   22
         Top             =   840
         Width           =   615
      End
      Begin VB.Label Label2 
         Caption         =   "File ID"
         Height          =   255
         Left            =   120
         TabIndex        =   21
         Top             =   360
         Width           =   1095
      End
   End
   Begin VB.CommandButton bConnect 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   2400
      TabIndex        =   2
      Top             =   1080
      Width           =   1575
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initialize"
      Height          =   375
      Left            =   2400
      TabIndex        =   1
      Top             =   600
      Width           =   1575
   End
   Begin VB.ComboBox cbReader 
      Height          =   315
      Left            =   1200
      TabIndex        =   0
      Top             =   120
      Width           =   2775
   End
   Begin VB.Label Label1 
      Caption         =   "Select Reader"
      Height          =   255
      Left            =   120
      TabIndex        =   19
      Top             =   240
      Width           =   1575
   End
End
Attribute VB_Name = "MainACOSBin"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              ACOSBinProg.frm
'
'  Description:       This sample program outlines the steps on how to
'                     implement the binary file support in ACOS3-24K
'
'  Author:              M.J.E.C. Castillo
'
'  Date:                June 23, 2008
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

Private Sub InitMenu()

    ConnActive = False
    cbReader.Clear
    Call DisplayOut(0, 0, "Program Ready")
    bInit.Enabled = True
    bConnect.Enabled = False
    bReset.Enabled = False
    tFileID1.Text = ""
    tFileID2.Text = ""
    tFileLen1.Text = ""
    tFileLen2.Text = ""
    frFormat.Enabled = False
    tFID1.Text = ""
    tFID2.Text = ""
    tOffset1.Text = ""
    tOffset2.Text = ""
    tLen.Text = ""
    tData.Text = ""
    frReadWrite.Enabled = False

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
    
    Case 4
      mMsg.SelColor = vbRed
      
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

Private Function SendAPDUandDisplay(ByVal SendType As Integer) As Long

  Dim indx As Integer
  Dim tmpStr As String

  ioRequest.dwProtocol = Protocol
  ioRequest.cbPciLength = Len(ioRequest)
  
  'display Apdu in
  tmpStr = ""
  
  For indx = 0 To SendLen - 1
  
    tmpStr = tmpStr & Right$("00" & Hex(SendBuff(indx)), 2) & " "
  
  Next indx
  
  Call DisplayOut(2, 0, tmpStr)

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
  
    tmpStr = ""
    
    Select Case SendType
    
      ' Display SW1/SW2 value
      Case 0
        
        For indx = RecvLen - 2 To RecvLen - 1
          
          tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2) & " "
          
        Next indx
        
        If Trim(tmpStr) <> "90 00" Then
            
            Call DisplayOut(4, 0, "Return bytes are not acceptable.")
        
        End If
        
      'Display ATR after checking SW1/SW2
      Case 1
        For indx = RecvLen - 2 To RecvLen - 1
          
          tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2) & " "
          
        Next indx
        
        If Trim(tmpStr) <> "90 00" Then
          
          Call DisplayOut(1, 0, "Return bytes are not acceptable.")
          
        Else
        
          tmpStr = "ATR: "
          
          For indx = 0 To RecvLen - 3
            
            tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2) & " "
            
          Next indx
          
        End If
        
      'Display all data
      Case 2
        For indx = 0 To RecvLen - 1
          
          tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2) & " "
          
        Next indx
        
    End Select
    
    Call DisplayOut(3, 0, Trim(tmpStr))
  End If
  SendAPDUandDisplay = retCode
  
End Function

Private Sub ClearBuffers()

  Dim intIndx As Long
  
  For intIndx = 0 To 262
  
    RecvBuff(intIndx) = &H0
    SendBuff(intIndx) = &H0
    
  Next intIndx
  
End Sub


Private Function SubmitIC()

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
    
    retCode = SendAPDUandDisplay(0)
    
    If retCode <> SCARD_S_SUCCESS Then
    
      SubmitIC = retCode
      Exit Function
      
    End If
    
    tmpStr = ""
    
    For indx = 0 To 1
    
      tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2) & " "
      
    Next indx
    
    If Trim(tmpStr) <> "90 00" Then
    
      Call DisplayOut(0, 0, "Return string is invalid. Value: " & tmpStr)
      retCode = INVALID_SW1SW2
      SubmitIC = retCode
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
  
  retCode = SendAPDUandDisplay(2)

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
  SendLen = &H5
  RecvLen = SendBuff(4) + 2
  
  retCode = SendAPDUandDisplay(0)
  
  If retCode <> SCARD_S_SUCCESS Then
  
    readRecord = retCode
    Exit Function
    
  End If
  
  tmpStr = ""
  
  For indx = 0 To 1
  
    tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx + SendBuff(4))), 2) & " "
    
  Next indx
  
  If tmpStr <> "90 00 " Then
  
    Call DisplayOut(2, 0, "Return string is invalid. Value: " & tmpStr)
    retCode = INVALID_SW1SW2
    
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
    
    SendLen = maxLen + 5
    RecvLen = &H2
    
    retCode = SendAPDUandDisplay(0)
    
    If retCode <> SCARD_S_SUCCESS Then
    
      writeRecord = retCode
      Exit Function
      
    End If
    
    tmpStr = ""
    
    For indx = 0 To 1
    
      tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2) & " "
      
    Next indx
    
    If Trim(tmpStr) <> "90 00" Then
    
      Call DisplayOut(3, 0, "Return string is invalid. Value: " & tmpStr)
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
  
  SendLen = dataLen + 5
  RecvLen = &H2
  
  retCode = SendAPDUandDisplay(0)
  
  If retCode <> SCARD_S_SUCCESS Then
  
    writeRecord = retCode
    Exit Function
    
  End If
  
  tmpStr = ""
  
  For indx = 0 To 1
  
    tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2) & " "
    
  Next indx
  
  If Trim(tmpStr) <> "90 00" Then
  
    Call DisplayOut(3, 0, "Return string is invalid. Value: " & tmpStr)
    retCode = INVALID_SW1SW2
    writeRecord = retCode
    Exit Function
    
  End If
  
  writeRecord = retCode

End Function

Private Sub getBinaryData()

Dim tmpStr As String
Dim indx As Integer
Dim tmpLen As Integer

    '1. Send IC code
    retCode = SubmitIC
    If retCode <> SCARD_S_SUCCESS Then
    
        Call DisplayOut(4, 0, "Insert ACOS3-24K card on contact card reader.")
        Exit Sub
    
    End If
    
    'select FF 04
    retCode = SelectFile(&HFF, &H4)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    tmpStr = ""
    
    For indx = 0 To 1
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2) & " "
    
    Next indx
    
    If Trim(tmpStr) <> "90 00" Then
        
        Call DisplayOut(2, 0, "The return string is invalid. Value: " & tmpStr)
        retCode = INVALID_SW1SW2
        Exit Sub
    
    End If
    
    'read first record
    retCode = readRecord(&H0, &H7)
    If retCode <> SCARD_S_SUCCESS Then
    
        Call DisplayOut(4, 0, "Card may not have been formatted yet.")
        Exit Sub
    
    End If
    
    'provide parameter to data input box
    tFID1.Text = Right$("00" & Hex(RecvBuff(4)), 2)
    tFID2.Text = Right$("00" & Hex(RecvBuff(5)), 2)
    tmpLen = RecvBuff(1)
    tmpLen = tmpLen + (RecvBuff(0) * 256)
    tData.MaxLength = tmpLen

End Sub

Private Function readBinary(ByVal hiByte As Byte, ByVal loByte As Byte, ByVal dataLen As Byte) As Long

Dim indx As Integer
Dim tmpStr As String

    Call ClearBuffers
    SendBuff(0) = &H80           'CLA
    SendBuff(1) = &HB0           'INS
    SendBuff(2) = hiByte         'P1    High Byte Address
    SendBuff(3) = loByte         'P2    Low Byte Address
    SendBuff(4) = dataLen        'P3    Length of data to be read
    
    SendLen = &H5
    RecvLen = SendBuff(4) + 2
    
    retCode = SendAPDUandDisplay(0)
    If retCode <> SCARD_S_SUCCESS Then
    
        readBinary = retCode
        Exit Function
    
    End If
    
    readBinary = retCode

End Function

Private Function writeBinary(caseType As Integer, ByVal hiByte As Byte, ByVal loByte As Byte, ByVal dataLen As Byte, DataIn() As Byte) As Integer

Dim indx As Integer
Dim tmpStr As String

    'If card data is to be erased before writing new data
    If caseType = 1 Then

        '1. Re-initialize card values to $00
        Call ClearBuffers
        SendBuff(0) = &H80                  'CLA
        SendBuff(1) = &HD0                  'INS
        SendBuff(2) = hiByte                'P1     High Byte address
        SendBuff(3) = loByte                'P2     Low byte address
        SendBuff(4) = dataLen               'P3     length of data to be read
        
        For indx = 0 To dataLen - 1
        
            SendBuff(indx + 5) = &H0
        
        Next indx
        
        SendLen = dataLen + 5
        RecvLen = &H2
        
        retCode = SendAPDUandDisplay(2)
        If retCode <> SCARD_S_SUCCESS Then
        
            writeBinary = retCode
            Exit Function
        
        End If
       
        tmpStr = ""
        For indx = 0 To 1
        
            tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2) & " "
        
        Next indx
        
        If Trim(tmpStr) <> "90 00" Then
        
            Call DisplayOut(3, 0, "The return string is invalid. Value: " & tmpStr)
            retCode = INVALID_SW1SW2
            writeBinary = retCode
            Exit Function
        
        End If
        
    End If
 
    'write data to card
    Call ClearBuffers
    SendBuff(0) = &H80          'CLA
    SendBuff(1) = &HD0          'INS
    SendBuff(2) = hiByte        'P1: High Byte Address
    SendBuff(3) = loByte        'P2: Low Byte Address
    SendBuff(4) = dataLen       'P3: Length of data to be read
   
    For indx = 0 To dataLen - 1
    
        SendBuff(indx + 5) = DataIn(indx)
    
    Next indx
    
    SendLen = dataLen + 5
    RecvLen = &H2
    
    retCode = SendAPDUandDisplay(0)
    If retCode <> SCARD_S_SUCCESS Then
    
        writeBinary = retCode
        Exit Function
    
    End If
    
    tmpStr = ""
    For indx = 0 To 1
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2) & " "
    
    Next indx
    
    If Trim(tmpStr) <> "90 00" Then
    
        Call DisplayOut(3, 0, "The return string is invalid. Value: " & tmpStr)
        retCode = INVALID_SW1SW2
        writeBinary = retCode
        Exit Function
    
    End If
    
    writeBinary = retCode

End Function

Private Sub bBinRead_Click()

Dim indx, tmpLen As Integer
Dim tmpStr As String
Dim hiByte, loByte, FileID1, FileID2 As Byte

    'validate input
    If tFID1.Text = "" Then
    
        tFID1.SetFocus
        Exit Sub
    
    End If
    
    If tFID2.Text = "" Then
    
        tFID2.SetFocus
        Exit Sub
    
    End If
    
    If tOffset2.Text = "" Then
    
        tOffset2.SetFocus
        Exit Sub
    
    End If
    
    If tLen.Text = "" Then
    
        tLen.SetFocus
        Exit Sub
    
    End If
    
    Call ClearBuffers
    FileID1 = CInt("&H" & tFID1.Text)
    FileID2 = CInt("&H" & tFID2.Text)
    
    If tOffset1.Text = "" Then
    
        hiByte = &H0
        
    Else
    
        hiByte = CInt("&H" & tOffset1.Text)
    
    End If
    
    loByte = CInt("&H" & tOffset2.Text)
    tmpLen = CInt("&H" & tLen.Text)
    
    'Select user file
    retCode = SelectFile(FileID1, FileID2)

    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    tmpStr = ""
    For indx = 0 To 1

        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2) & " "
    
    Next indx

    If Trim(tmpStr) <> "91 00" Then
    
        Call DisplayOut(2, 0, "The return string is invalid. Value: " & tmpStr)
        retCode = INVALID_SW1SW2
        Exit Sub
    
    End If
    
    'read binary
    retCode = readBinary(hiByte, loByte, tmpLen)
    If retCode <> SCARD_S_SUCCESS Then
    
        Call DisplayOut(4, 0, "Card may not have been formatted yet.")
        Exit Sub
    
    End If
    
    tmpStr = ""
    indx = 0
    
    While RecvBuff(indx) <> &H0
    
        If indx < tData.MaxLength Then
        
            tmpStr = tmpStr & Chr(RecvBuff(indx))
        
        End If
        
        indx = indx + 1
    
    Wend
    
    tData.Text = tmpStr

End Sub

Private Sub bClear_Click()

    mMsg.Text = ""

End Sub

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
    frFormat.Enabled = True
    frReadWrite.Enabled = True
    Call getBinaryData

End Sub

Private Sub bFormat_Click()

Dim tmpStr As String
Dim indx As Integer
Dim tmpArray(0 To 31) As Byte

    'validate input
    If tFileID1.Text = "" Then
    
        tFileID1.SetFocus
        Exit Sub
    
    End If
    
    If tFileID2.Text = "" Then
    
        tFileID2.SetFocus
        Exit Sub
    
    End If
    
    If tFileLen2.Text = "" Then
    
        tFileLen2.SetFocus
        Exit Sub
    
    End If
    
    'send IC code
    retCode = SubmitIC
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Call DisplayOut(4, 0, "Insert ACOS3-24K card on contact card reader.")
        Exit Sub
    
    End If
    
    'select FF 02
    retCode = SelectFile(&HFF, &H2)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    tmpStr = ""
    
    For indx = 0 To 1
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2) & " "
    
    Next indx
    
    If Trim(tmpStr) <> "90 00" Then
    
        Call DisplayOut(2, 0, "The return string is invalid. Value: " & tmpStr)
        retCode = INVALID_SW1SW2
        Exit Sub
    
    End If
    
    '3. Write to FF 02
    '   This will create 1 binary file, no Option registers and
    '   Security Option registers defined, Personalization bit
    '   is not set
    tmpArray(0) = &H0       '00 option registers
    tmpArray(1) = &H0       '00 security option register
    tmpArray(2) = &H1       '01 no. of user file
    tmpArray(3) = &H0       '00 personalization bit
    
    retCode = writeRecord(0, &H0, &H4, &H4, tmpArray)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    Call DisplayOut(0, 0, "File FF 02 is updated.")
    
    '4. perform a reset for changes in ACOS3 to take effect
    ConnActive = False
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Call DisplayOut(0, retCode, "")
        Exit Sub
    
    End If
    
    retCode = SCardConnect(hContext, _
                          cbReader.Text, _
                          SCARD_SHARE_SHARED, _
                          SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1, _
                          hCard, _
                          Protocol)
                          
    If retCode <> SCARD_S_SUCCESS Then
    
        Call DisplayOut(0, retCode, "")
        Exit Sub
    
    End If
    
    Call DisplayOut(3, 0, "Card reset is successful.")
    ConnActive = True
    
    '5. select FF 04
    retCode = SelectFile(&HFF, &H4)
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    tmpStr = ""
    
    For indx = 0 To 1
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2) & " "
    
    Next indx
    
    If Trim(tmpStr) <> "90 00" Then
    
        Call DisplayOut(2, 0, "The return string is invalid. Value: " & tmpStr)
        retCode = INVALID_SW1SW2
        Exit Sub
    
    End If
    
    '6. send IC code
    retCode = SubmitIC
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    'write to FF 04
    '7.1. Write to first record of FF 04
    If tFileLen1.Text = "" Then
    
        tmpArray(0) = &H0                           'File Length: High Byte
    
    Else
    
        tmpArray(0) = CInt("&H" & tFileLen1.Text)   'File Length: High Byte
   
    End If
         
        tmpArray(1) = CInt("&H" & tFileLen2.Text)   'File Length: Low Byte
        tmpArray(2) = &H0                           '00    Read security attribute
        tmpArray(3) = &H0                           '00    Write security attribute
        tmpArray(4) = CInt("&H" & tFileID1.Text)    'file Identifier
        tmpArray(5) = CInt("&H" & tFileID2.Text)    'file Identifier
        tmpArray(6) = &H80                          'File Access Flag: Binary File Type
        
        retCode = writeRecord(0, &H0, &H7, &H7, tmpArray)
        If retCode <> SCARD_S_SUCCESS Then
        
            Exit Sub
        
        End If
        
        tmpStr = ""
        tmpStr = tFileID1.Text & " " & tFileID2.Text
        Call DisplayOut(0, 0, "User File " & tmpStr & " is defined.")
        
End Sub

Private Sub bInit_Click()

    sReaderList = String(255, vbNullChar)
    ReaderCount = 255

    '1. Establish context and obtain hContext handle
    retCode = SCardEstablishContext(SCARD_SCOPE_USER, _
                                    0, _
                                    0, _
                                    hContext)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Call DisplayOut(1, retCode, "")
        Exit Sub
    
    End If
    
    '2. List PC/SC card readers installed in the system
    retCode = SCardListReaders(hContext, _
                               sReaderGroup, _
                               sReaderList, _
                               ReaderCount)
  
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
    
    End If
    
    retCode = SCardReleaseContext(hCard)
    Call InitMenu

End Sub

Private Sub Form_Load()

    Call InitMenu

End Sub

Private Sub bBinWrite_Click()

Dim indx, tmpLen As Integer
Dim tmpStr As String
Dim hiByte, loByte, FileID1, FileID2 As Byte
Dim tmpArray(0 To 255) As Byte
Dim charArray(0 To 255) As String

    'validate input
    If tFID1.Text = "" Then
    
        tFID1.SetFocus
        Exit Sub
    
    End If
    
    If tFID2.Text = "" Then
    
        tFID2.SetFocus
        Exit Sub
    
    End If
    
    If tOffset2.Text = "" Then
    
        tOffset2.SetFocus
        Exit Sub
    
    End If
    
    If tLen.Text = "" Then
    
        tLen.SetFocus
        Exit Sub
    
    End If
    
    If tData.Text = "" Then
    
        tData.SetFocus
        Exit Sub
    
    End If
    
    Call ClearBuffers
    FileID1 = CInt("&H" & tFID1.Text)
    FileID2 = CInt("&H" & tFID2.Text)
    
    If tOffset1.Text = "" Then
    
        hiByte = &H0
        
    Else
    
        hiByte = CInt("&H" & tOffset1.Text)
    
    End If
    
    loByte = CInt("&H" & tOffset2.Text)
    tmpLen = CInt("&H" & tLen.Text)
    
    '1.select user file
    retCode = SelectFile(FileID1, FileID2)
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    tmpStr = ""
    For indx = 0 To 1
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2) & " "
    
    Next indx
    
    If Trim(tmpStr) <> "91 00" Then
    
        Call DisplayOut(2, 0, "The return string is invalid. Value: " & tmpStr)
        retCode = INVALID_SW1SW2
        Exit Sub
    
    End If
    
    'write input data to card
    tmpStr = tData.Text
    
    'change tmpStr to character array
    For indx = 1 To Len(tmpStr)
    
        charArray(indx) = Mid$(tmpStr, indx, 1)
    
    Next indx
    
    For indx = 0 To Len(tmpStr) - 1
    
        tmpArray(indx) = Asc(charArray(indx + 1))
    
    Next indx
    
    retCode = writeBinary(1, hiByte, loByte, tmpLen, tmpArray)
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
   End If

End Sub

'check user input

Private Sub tData_GotFocus()

       tData.SelStart = 0
       tData.SelLength = Len(tData.Text)

End Sub

Private Sub tFID1_Change()

    If Len(tFID1.Text) > 1 Then
    
       tFID2.SetFocus
    
    End If

End Sub

Private Sub tFID1_GotFocus()

       tFID1.SelStart = 0
       tFID1.SelLength = Len(tFID1.Text)

End Sub

Private Sub tFID1_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tFID2_Change()

    If Len(tFID2.Text) > 1 Then
    
       tOffset1.SetFocus
    
    End If

End Sub

Private Sub tFID2_GotFocus()

       tFID2.SelStart = 0
       tFID2.SelLength = Len(tFID2.Text)

End Sub

Private Sub tFID2_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tFileID1_Change()

    If Len(tFileID1.Text) > 1 Then
    
       tFileID2.SetFocus
    
    End If

End Sub

Private Sub tFileID1_GotFocus()

       tFileID1.SelStart = 0
       tFileID1.SelLength = Len(tFileID1.Text)

End Sub

Private Sub tFileID1_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tFileID2_Change()

    If Len(tFileID2.Text) > 1 Then
    
       tFileLen1.SetFocus
    
    End If

End Sub

Private Sub tFileID2_GotFocus()

       tFileID2.SelStart = 0
       tFileID2.SelLength = Len(tFileID2.Text)

End Sub

Private Sub tFileID2_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tFileLen1_Change()

    If Len(tFileLen1.Text) > 1 Then
    
       tFileLen2.SetFocus
    
    End If

End Sub

Private Sub tFileLen1_GotFocus()

       tFileLen1.SelStart = 0
       tFileLen1.SelLength = Len(tFileLen1.Text)

End Sub

Private Sub tFileLen1_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tFileLen2_GotFocus()

       tFileLen2.SelStart = 0
       tFileLen2.SelLength = Len(tFileLen2.Text)

End Sub

Private Sub tFileLen2_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tLen_Change()

    If Len(tLen.Text) > 1 Then
    
       tData.SetFocus
    
    End If

End Sub

Private Sub tLen_GotFocus()

       tLen.SelStart = 0
       tLen.SelLength = Len(tLen.Text)

End Sub

Private Sub tLen_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tOffset1_Change()

    If Len(tOffset1.Text) > 1 Then
    
       tOffset2.SetFocus
    
    End If

End Sub

Private Sub tOffset1_GotFocus()

       tOffset1.SelStart = 0
       tOffset1.SelLength = Len(tOffset1.Text)

End Sub

Private Sub tOffset1_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tOffset2_Change()

    If Len(tOffset2.Text) > 1 Then
    
       tLen.SetFocus
    
    End If

End Sub

Private Sub tOffset2_GotFocus()

       tOffset2.SelStart = 0
       tOffset2.SelLength = Len(tOffset2.Text)

End Sub

Private Sub tOffset2_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub
