VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form ConfigureATR 
   Caption         =   "Configure ATR"
   ClientHeight    =   7905
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   8355
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   9
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "ConfigureATR.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   7905
   ScaleWidth      =   8355
   StartUpPosition =   2  'CenterScreen
   Begin VB.TextBox t1 
      Alignment       =   2  'Center
      BackColor       =   &H00E0E0E0&
      Height          =   330
      Index           =   9
      Left            =   2520
      MaxLength       =   2
      TabIndex        =   17
      Top             =   5160
      Width           =   615
   End
   Begin VB.TextBox t1 
      Alignment       =   2  'Center
      BackColor       =   &H00E0E0E0&
      Height          =   330
      Index           =   8
      Left            =   1920
      MaxLength       =   2
      TabIndex        =   16
      Top             =   5160
      Width           =   615
   End
   Begin VB.TextBox t1 
      Alignment       =   2  'Center
      BackColor       =   &H00E0E0E0&
      Height          =   330
      Index           =   7
      Left            =   1320
      MaxLength       =   2
      TabIndex        =   15
      Top             =   5160
      Width           =   615
   End
   Begin VB.TextBox t1 
      Alignment       =   2  'Center
      BackColor       =   &H00E0E0E0&
      Height          =   330
      Index           =   6
      Left            =   720
      MaxLength       =   2
      TabIndex        =   14
      Top             =   5160
      Width           =   615
   End
   Begin VB.TextBox t1 
      Alignment       =   2  'Center
      BackColor       =   &H00E0E0E0&
      Height          =   330
      Index           =   5
      Left            =   120
      MaxLength       =   2
      TabIndex        =   13
      Top             =   5160
      Width           =   615
   End
   Begin VB.TextBox Text2 
      Alignment       =   2  'Center
      Enabled         =   0   'False
      Height          =   330
      Left            =   2520
      TabIndex        =   29
      Top             =   3240
      Width           =   615
   End
   Begin VB.TextBox Text1 
      Alignment       =   2  'Center
      Enabled         =   0   'False
      Height          =   330
      Left            =   2520
      TabIndex        =   28
      Top             =   4080
      Width           =   615
   End
   Begin VB.TextBox t1 
      Alignment       =   2  'Center
      BackColor       =   &H00E0E0E0&
      Enabled         =   0   'False
      Height          =   330
      Index           =   2
      Left            =   1320
      MaxLength       =   2
      TabIndex        =   10
      Top             =   4800
      Width           =   615
   End
   Begin VB.TextBox t1 
      Alignment       =   2  'Center
      BackColor       =   &H00E0E0E0&
      Height          =   330
      Index           =   3
      Left            =   1920
      MaxLength       =   2
      TabIndex        =   11
      Top             =   4800
      Width           =   615
   End
   Begin VB.TextBox t1 
      Alignment       =   2  'Center
      BackColor       =   &H00E0E0E0&
      Height          =   330
      Index           =   4
      Left            =   2520
      MaxLength       =   2
      TabIndex        =   12
      Top             =   4800
      Width           =   615
   End
   Begin VB.TextBox t1 
      Alignment       =   2  'Center
      BackColor       =   &H00E0E0E0&
      Height          =   330
      Index           =   10
      Left            =   120
      MaxLength       =   2
      TabIndex        =   18
      Top             =   5520
      Width           =   615
   End
   Begin VB.TextBox t1 
      Alignment       =   2  'Center
      BackColor       =   &H00E0E0E0&
      Height          =   330
      Index           =   11
      Left            =   720
      MaxLength       =   2
      TabIndex        =   19
      Top             =   5520
      Width           =   615
   End
   Begin VB.TextBox t1 
      Alignment       =   2  'Center
      BackColor       =   &H00E0E0E0&
      Height          =   330
      Index           =   14
      Left            =   2520
      MaxLength       =   2
      TabIndex        =   22
      Top             =   5520
      Width           =   615
   End
   Begin VB.ComboBox cbo_nobytes 
      Enabled         =   0   'False
      Height          =   330
      ItemData        =   "ConfigureATR.frx":17A2
      Left            =   120
      List            =   "ConfigureATR.frx":17D9
      TabIndex        =   26
      Top             =   4080
      Width           =   2295
   End
   Begin VB.TextBox t1 
      Alignment       =   2  'Center
      BackColor       =   &H00E0E0E0&
      Height          =   330
      Index           =   13
      Left            =   1920
      MaxLength       =   2
      TabIndex        =   21
      Top             =   5520
      Width           =   615
   End
   Begin VB.TextBox t1 
      Alignment       =   2  'Center
      BackColor       =   &H00E0E0E0&
      Height          =   330
      Index           =   12
      Left            =   1320
      MaxLength       =   2
      TabIndex        =   20
      Top             =   5520
      Width           =   615
   End
   Begin VB.TextBox t1 
      Alignment       =   2  'Center
      BackColor       =   &H00E0E0E0&
      Enabled         =   0   'False
      Height          =   330
      Index           =   1
      Left            =   720
      MaxLength       =   2
      TabIndex        =   9
      Top             =   4800
      Width           =   615
   End
   Begin VB.ComboBox cbo_rate 
      Enabled         =   0   'False
      Height          =   330
      ItemData        =   "ConfigureATR.frx":1821
      Left            =   120
      List            =   "ConfigureATR.frx":1834
      TabIndex        =   25
      Top             =   3240
      Width           =   2295
   End
   Begin VB.CommandButton bUpdateATR 
      Caption         =   "&Update ATR"
      Enabled         =   0   'False
      Height          =   375
      Left            =   120
      TabIndex        =   23
      Top             =   6000
      Width           =   1935
   End
   Begin VB.TextBox t1 
      Alignment       =   2  'Center
      BackColor       =   &H00E0E0E0&
      Enabled         =   0   'False
      Height          =   330
      Index           =   0
      Left            =   120
      MaxLength       =   2
      TabIndex        =   8
      Top             =   4800
      Width           =   615
   End
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   7575
      Left            =   3240
      TabIndex        =   7
      Top             =   120
      Width           =   4935
      _ExtentX        =   8705
      _ExtentY        =   13361
      _Version        =   393217
      ScrollBars      =   2
      TextRTF         =   $"ConfigureATR.frx":185B
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
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   120
      TabIndex        =   6
      Top             =   7320
      Width           =   1935
   End
   Begin VB.CommandButton bReset 
      Caption         =   "&Reset"
      Height          =   375
      Left            =   120
      TabIndex        =   5
      Top             =   6840
      Width           =   1935
   End
   Begin VB.CommandButton bGetATR 
      Caption         =   "&Get ATR"
      Height          =   375
      Left            =   120
      TabIndex        =   4
      Top             =   2400
      Width           =   1935
   End
   Begin VB.CommandButton bConnect 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   120
      TabIndex        =   3
      Top             =   1800
      Width           =   1935
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initialize"
      Height          =   375
      Left            =   120
      TabIndex        =   2
      Top             =   1200
      Width           =   1935
   End
   Begin VB.ComboBox cbReader 
      Height          =   330
      Left            =   120
      TabIndex        =   1
      Top             =   600
      Width           =   2295
   End
   Begin VB.Label Label6 
      Alignment       =   2  'Center
      Caption         =   "TA"
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
      Left            =   2520
      TabIndex        =   32
      Top             =   2880
      Width           =   615
   End
   Begin VB.Label Label5 
      Caption         =   "Historical Bytes"
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
      TabIndex        =   31
      Top             =   4560
      Width           =   1575
   End
   Begin VB.Label Label4 
      Alignment       =   2  'Center
      Caption         =   "T0"
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
      Left            =   2520
      TabIndex        =   30
      Top             =   3720
      Width           =   615
   End
   Begin VB.Label Label3 
      Caption         =   "No. of Historical Bytes"
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
      TabIndex        =   27
      Top             =   3720
      Width           =   2295
   End
   Begin VB.Label Label2 
      Caption         =   "Card Baud Rate"
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
      TabIndex        =   24
      Top             =   2880
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
      TabIndex        =   0
      Top             =   240
      Width           =   1455
   End
End
Attribute VB_Name = "ConfigureATR"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              ConfigureATR.frm
'
'  Description:       This sample program outlines the steps on how to
'                     change the ATR of a smart card using the PC/SC platform.
'                     You can also change the Card Baud Rate and the Historical Bytes of the card.
'
'  NOTE:            Historical Bytes valid value must be 0 to 9 and A,B,C,D,E,F only. e.g.(11,99,AE,AA,FF etc)
'                   if historical byte is leave blank the profram will assume it as 00.
'                   After you update the ATR you have to initialize and connect to the device
'                   before you can see the updated ATR.
'
'  Author:            Fernando G. Robles
'
'  Date:              November 09, 2005
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
Const INVALID_SW1SW2 = -450



Private Sub InitMenu()

  'Initialize objects
  cbReader.Clear
  bInit.Enabled = True
  bConnect.Enabled = False
  bGetATR.Enabled = False
  bReset.Enabled = False
  bUpdateATR.Enabled = False
  cbo_rate.Enabled = False
  cbo_nobytes.Enabled = False
  mMsg.Text = ""
  
  Call DisplayOut(0, 0, "Program ready")
  
End Sub

Private Sub DisplayOut(ByVal mType As Integer, ByVal msgCode As Long, ByVal PrintText As String)

  'Function that display all messages to the RichEdit object.
  
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
  bGetATR.Enabled = True
  
End Sub

Private Sub bGetATR_Click()

  Dim ReaderLen, dwState, ATRLen, indx As Long
  Dim ATRVal(0 To 31) As Byte
  Dim tmpStr As String
  
  
  'Get ATR Function.
  
  ATRLen = 32
  
  Call DisplayOut(2, 0, "Invoke SCardStatus")
  ' 1. Invoke SCardStatus using hCard handle
  '    and valid reader name
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
    
  End If

  ' 2. Format ATRVal returned and display string as ATR value
  tmpStr = ""
  For indx = 0 To ATRLen - 1
    
    tmpStr = tmpStr & Format(Hex(ATRVal(indx)), "00") & " "
    
  Next indx
  
    Text1.Text = Format(Hex(ATRVal(1)), "00")
    Text2.Text = Format(Hex(ATRVal(2)), "00")
  
  
  Call DisplayOut(3, 0, tmpStr)

  ' 3. Interpret dwActProtocol returned and display as active protocol
  Select Case Protocol
  
    Case 1
      tmpStr = "T=0"
      
    Case 2
      tmpStr = "T=1"
      
  End Select
  
  Call DisplayOut(3, 0, "Active protocol: " & tmpStr)
  bUpdateATR.Enabled = True
  cbo_rate.Enabled = True
  cbo_nobytes.Enabled = True
  
End Sub

Private Sub bInit_Click()

  'Initialize Function

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



Private Sub bUpdateATR_Click()
Dim tmpStr As String
Dim indx As Integer
Dim tmpAPDU(0 To 35) As Byte
Dim num_historical_byte As Integer
Dim ctr As Integer
  
  'Select FF07 file
  retCode = SelectFile(&HFF, &H7)
  
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
  
  'Submit IC code
  retCode = SubmitIC()
  
  If retCode <> SCARD_S_SUCCESS Then
  
    Exit Sub
    
  End If
  
  'Updating the ATR Value.
  num_historical_byte = cbo_nobytes.ListIndex
  
  tmpAPDU(0) = CInt("&H" & Right("00" & Text2.Text, 2)) 'Card Baud Rate parameter.

  If num_historical_byte = 16 Then
  
    tmpAPDU(1) = &HFF 'restoring to it original historical bytes
    
  Else
  
    tmpAPDU(1) = num_historical_byte
    
  End If

  ctr = 2
  If num_historical_byte < 16 Then
    
    For indx = 0 To num_historical_byte - 1
    
      tmpAPDU(ctr) = CInt("&H" & Right("00" & t1(indx), 2))
      ctr = ctr + 1
      
    Next indx
    
  End If
  
  For indx = ctr To 35
  
    tmpAPDU(indx) = &H0
    
  Next indx
  
   retCode = writeRecord(0, 36, tmpAPDU)

End Sub

Private Sub cbo_nobytes_Click()
Dim i As Integer
    
    Select Case cbo_nobytes.ListIndex
    
        Case 0: Text1.Text = "B0"
        Case 1: Text1.Text = "B1"
        Case 2: Text1.Text = "B2"
        Case 3: Text1.Text = "B3"
        Case 4: Text1.Text = "B4"
        Case 5: Text1.Text = "B5"
        Case 6: Text1.Text = "B6"
        Case 7: Text1.Text = "B7"
        Case 8: Text1.Text = "B8"
        Case 9: Text1.Text = "B9"
        Case 10: Text1.Text = "BA"
        Case 11: Text1.Text = "BB"
        Case 12: Text1.Text = "BC"
        Case 13: Text1.Text = "BD"
        Case 14: Text1.Text = "BE"
        Case 15: Text1.Text = "BF"
        Case 16: Text1.Text = "BE"
        
    End Select
    
    For i = 0 To 14
    
        t1(i).Text = ""
        t1(i).BackColor = &HE0E0E0
        
    Next i
    
    For i = 0 To cbo_nobytes.ListIndex - 1
    
        If cbo_nobytes.ListIndex < 16 Then
        t1(i).Enabled = True
        t1(i).BackColor = vbWhite
        End If
        
    Next i
    
    For i = i To 14
    
        t1(i).Enabled = False
        t1(i).BackColor = &HE0E0E0
        
    Next i
    
End Sub

Private Sub cbo_rate_Click()
    Select Case cbo_rate.ListIndex
    
        Case 0: Text2.Text = "11"
        Case 1: Text2.Text = "92"
        Case 2: Text2.Text = "93"
        Case 3: Text2.Text = "94"
        Case 4: Text2.Text = "95"
        
    End Select
End Sub

Private Sub cbReader_Click()

  bGetATR.Enabled = False
  
  If ConnActive Then
  
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
    
  End If
  
End Sub

Private Sub Form_Load()

  Call InitMenu
  
End Sub

Private Sub Text1_Change()

    ' The 2nd character of Text1.text is the Number of
    ' historical bytes to be written on the ACOS card.
    
    Select Case Text1.Text
    
        Case "B0": cbo_nobytes.ListIndex = 0
        Case "B1": cbo_nobytes.ListIndex = 1
        Case "B2": cbo_nobytes.ListIndex = 2
        Case "B3": cbo_nobytes.ListIndex = 3
        Case "B4": cbo_nobytes.ListIndex = 4
        Case "B5": cbo_nobytes.ListIndex = 5
        Case "B6": cbo_nobytes.ListIndex = 6
        Case "B7": cbo_nobytes.ListIndex = 7
        Case "B8": cbo_nobytes.ListIndex = 8
        Case "B9": cbo_nobytes.ListIndex = 9
        Case "BA": cbo_nobytes.ListIndex = 10
        Case "BB": cbo_nobytes.ListIndex = 11
        Case "BC": cbo_nobytes.ListIndex = 12
        Case "BD": cbo_nobytes.ListIndex = 13
        Case "BF": cbo_nobytes.ListIndex = 15
        Case Else
            If cbo_nobytes.ListIndex = 16 Then
            
                cbo_nobytes.ListIndex = 16
                
            Else
            
                cbo_nobytes.ListIndex = 14
                
            End If
            
    End Select
    
End Sub

Private Sub Text2_Change()

    'Text2.text is the value of card baud rate written on the card.
    Select Case Text2.Text
    
        Case "11": cbo_rate.ListIndex = 0
        Case "92": cbo_rate.ListIndex = 1
        Case "93": cbo_rate.ListIndex = 2
        Case "94": cbo_rate.ListIndex = 3
        Case "95": cbo_rate.ListIndex = 4
        
    End Select
    
End Sub


Private Function SubmitIC() As Long    ' Submit IC Function.

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

  'SELECT file function.

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
Private Sub ClearBuffers()

  Dim indx As Long
  
  For indx = 0 To 262
  
    RecvBuff(indx) = &H0
    SendBuff(indx) = &H0
    
  Next indx
  
End Sub
Private Function SendAPDUandDisplay(ByVal SendType As Integer, ByVal ApduIn As String) As Long
' Function that execute APDU commands.

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


Private Function writeRecord(ByVal RecNo As Byte, ByVal DataLen As Byte, ByRef ApduIn() As Byte) As Long
'Write Function
  Dim indx As Integer
  Dim tmpStr As String

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


