VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form MainGetATR 
   Caption         =   "Get ATR Sample"
   ClientHeight    =   5340
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   10890
   Icon            =   "GetATRMain.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   5340
   ScaleWidth      =   10890
   StartUpPosition =   2  'CenterScreen
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   5055
      Left            =   4080
      TabIndex        =   8
      Top             =   120
      Width           =   6615
      _ExtentX        =   11668
      _ExtentY        =   8916
      _Version        =   393217
      Enabled         =   -1  'True
      ScrollBars      =   2
      TextRTF         =   $"GetATRMain.frx":17A2
   End
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   2280
      TabIndex        =   6
      Top             =   4800
      Width           =   1575
   End
   Begin VB.CommandButton bReset 
      Caption         =   "&Reset"
      Height          =   375
      Left            =   2280
      TabIndex        =   5
      Top             =   4320
      Width           =   1575
   End
   Begin VB.CommandButton bClear 
      Caption         =   "C&lear Screen"
      Height          =   375
      Left            =   2280
      TabIndex        =   4
      Top             =   3840
      Width           =   1575
   End
   Begin VB.CommandButton bGetAtr 
      Caption         =   "&Get ATR"
      Height          =   375
      Left            =   2280
      TabIndex        =   3
      Top             =   1680
      Width           =   1575
   End
   Begin VB.CommandButton bConnect 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   2280
      TabIndex        =   2
      Top             =   1200
      Width           =   1575
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initialize"
      Height          =   375
      Left            =   2280
      TabIndex        =   1
      Top             =   720
      Width           =   1575
   End
   Begin VB.ComboBox cbReader 
      Height          =   315
      Left            =   1440
      TabIndex        =   0
      Top             =   240
      Width           =   2415
   End
   Begin VB.Label Label1 
      BackStyle       =   0  'Transparent
      Caption         =   "Select Reader"
      Height          =   495
      Left            =   240
      TabIndex        =   7
      Top             =   360
      Width           =   1095
   End
End
Attribute VB_Name = "MainGetATR"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              GetATRMain.frm
'
'  Description:       This sample program outlines the steps on how to
'                     get ATR from cards using ACR128
'
'  Author:            M.J.E.C. Castillo
'
'  Date:              June 2, 2008
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
Dim SendLen, RecvLen, ATRLen As Long
Dim SendBuff(0 To 262) As Byte
Dim RecvBuff(0 To 262) As Byte
Dim ATRVal(0 To 128) As Byte

Const INVALID_SW1SW2 = -450

Private Sub bClear_Click()

    mMsg.Text = ""

End Sub

Private Sub bConnect_Click()
  
  If ConnActive Then
  
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
  
  End If
    
    '1. Shared Connection
    retCode = SCardConnect(hContext, _
                        cbReader.Text, _
                        SCARD_SHARE_SHARED, _
                        SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1, _
                        hCard, _
                        Protocol)
    If retCode <> SCARD_S_SUCCESS Then
    
        'check if ACR128 SAM is used and use Direct Mode if SAM is not detected
        If InStr(cbReader.Text, "ACR128U SAM") > 0 Then
            
            '1. Direct Connection
             retCode = SCardConnect(hContext, _
                        cbReader.Text, _
                        SCARD_SHARE_DIRECT, _
                        0, _
                        hCard, _
                        Protocol)
                        
             If retCode <> SCARD_S_SUCCESS Then
                
                Call DisplayOut(1, retCode, "")
                ConnActive = False
                Exit Sub
                
             Else
             
                Call DisplayOut(0, 0, "Successful connection to " & cbReader.Text)
             
             End If
            
        Else
        
            Call DisplayOut(1, retCode, "")
            ConnActive = False
            Exit Sub
        
        End If
    
    Else
    
        Call DisplayOut(0, 0, "Successful connection to " & cbReader.Text)
    
    End If

  ConnActive = True

End Sub

Private Sub bGetAtr_Click()

Dim ReaderLen, State As Long
Dim tmpStr As String
Dim intIndx As Integer
Dim tmpWord As Long

    Call DisplayOut(0, 0, "Invoke SCardStatus")
    '1. Invoke SCardStatus using hCard handle and valid reader name
    tmpWord = 32
    ATRLen = tmpWord
       
    retCode = SCardStatus(hCard, _
                         cbReader.Text, _
                         ReaderLen, _
                         State, _
                         Protocol, _
                         ATRVal(0), _
                         ATRLen)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Call DisplayOut(1, retCode, "")
    
    Else
    
        '2. Format ATRVal returned and display string as ATR value
        tmpStr = "ATR Length: " & CInt(ATRLen)
        Call DisplayOut(3, 0, tmpStr)
        tmpStr = "ATR Value: "
        
        For intIndx = 0 To CInt(ATRLen) - 1
        
            tmpStr = tmpStr & Format(Hex(ATRVal(intIndx)), "00") & " "
        
        Next intIndx
        
        Call DisplayOut(3, 0, tmpStr)
        
        '3. Interpret dwActProtocol returned and display as active protocol
        tmpStr = "Active Protocol: "
        
        Select Case CInt(Protocol)
        
            Case 1
                tmpStr = tmpStr & "T=0"
                
            Case 2
                If InStr(cbReader.Text, "ACR128U PICC") > 0 Then
                
                    tmpStr = tmpStr & "T=CL"
                    
                Else
                
                    tmpStr = tmpStr & "T=1"
                    
                End If
                
            Case Else
                tmpStr = "No protocol is defined."
        
        End Select
        
        Call DisplayOut(3, 0, tmpStr)
        Call InterpretATR
    
    End If

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
  Call AddButtons

  'Look for ACR128 SAM and make it the default reader in the combobox
  For intIndx = 0 To cbReader.ListCount - 1
    
    cbReader.ListIndex = intIndx
    
    If InStr(cbReader.Text, "ACR128U SAM") > 0 Then
        
        Exit For
       
    End If
     
  Next intIndx

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

    mMsg.Text = ""
    
    If ConnActive Then
        retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    End If
    
    retCode = SCardReleaseContext(hCard)
    Call InitMenu

End Sub

Private Sub Form_Load()

    Call InitMenu

End Sub

Private Sub InitMenu()

    ConnActive = False
    Call DisplayOut(0, 0, "Program ready")
    bConnect.Enabled = False
    bGetAtr.Enabled = False
    bReset.Enabled = False
    bInit.Enabled = True

End Sub

Private Sub DisplayOut(ByVal mType As Integer, ByVal msgCode As Long, ByVal PrintText As String)

  Select Case mType
  
    ' Notifications only
    Case 0
      mMsg.SelColor = &H4000
      
    ' Error Messages
    Case 1
      mMsg.SelColor = vbRed
      PrintText = GetScardErrMsg(retCode)
      
    'input data
    Case 2
      mMsg.SelColor = vbBlack
      PrintText = "< " & PrintText
      
    'output data
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
    bGetAtr.Enabled = True

End Sub

Private Sub ClearBuffers()

  Dim intIndx As Long
  
  For intIndx = 0 To 262
  
    RecvBuff(intIndx) = &H0
    SendBuff(intIndx) = &H0
    
  Next intIndx
  
End Sub

Private Sub InterpretATR()

Dim RIDVal, cardName, sATRStr, lATRStr, tmpVal As String
Dim indx, indx2 As Integer

    '4. Interpret ATR and guess card
    ' 4.1. Mifare cards using ISO 14443 Part 3 Supplemental Document
    If CInt(ATRLen) > 14 Then
    
        RIDVal = ""
        sATRStr = ""
        lATRStr = ""
        
        For indx = 7 To 11
        
            RIDVal = RIDVal & Format(Hex(ATRVal(indx)), "00")
            
        Next indx
        
        
        For indx = 0 To 4
            
            'shift bit to right
            tmpVal = ATRVal(indx)
            For indx2 = 1 To 4
        
                tmpVal = tmpVal / 2
        
            Next indx2
        
            If ((indx = 1) And (tmpVal = 8)) Then
            
                lATRStr = lATRStr & "8X"
                sATRStr = sATRStr & "8X"
            
            Else
            
                If indx = 4 Then
                
                    lATRStr = lATRStr & Format(Hex(ATRVal(indx)), "00")
                    
                Else
                
                    lATRStr = lATRStr & Format(Hex(ATRVal(indx)), "00")
                    sATRStr = sATRStr & Format(Hex(ATRVal(indx)), "00")
                
                End If
            
            End If
        
        Next indx
        
        If RIDVal = "A000000306" Then
        
            cardName = ""
            
            Select Case ATRVal(12)
            
                Case 0
                    cardName = "No card information"
                    
                Case 1
                    cardName = "ISO 14443 A, Part1 Card Type"
                    
                Case 2
                    cardName = "ISO 14443 A, Part2 Card Type"
                    
                Case 3
                    cardName = "ISO 14443 A, Part3 Card Type"
                
                Case 5
                    cardName = "ISO 14443 B, Part1 Card Type"
                
                Case 6
                    cardName = "ISO 14443 B, Part2 Card Type"
                    
                Case 7
                    cardName = "ISO 14443 B, Part3 Card Type"
                    
                Case 9
                    cardName = "ISO 15693, Part1 Card Type"
                    
                Case 10
                    cardName = "ISO 15693, Part2 Card Type"
                    
                Case 11
                    cardName = "ISO 15693, Part3 Card Type"
                    
                Case 12
                    cardName = "ISO 15693, Part4 Card Type"
                    
                Case 13
                    cardName = "Contact Card (7816-10) IIC Card Type"
                    
                Case 14
                    cardName = "Contact Card (7816-10) Extended IIC Card Type"
                    
                Case 15
                    cardName = "Contact Card (7816-10) 2WBP Card Type"
                    
                Case 16
                    cardName = "Contact Card (7816-10) 3WBP Card Type"
                    
                Case Else
                    cardName = "Undefined card"
            
            End Select
        
        End If
        
        If ATRVal(12) = &H3 Then
        
            If ATRVal(13) = &H0 Then
            
                Select Case ATRVal(14)
                
                    Case &H1
                        cardName = cardName & ": Mifare Standard 1K"
                        
                    Case &H2
                        cardName = cardName & ": Mifare Standard 4K"
                        
                    Case &H3
                        cardName = cardName & ": Mifare Ultra light"
                        
                    Case &H4
                        cardName = cardName & ": SLE55R_XXXX"
                        
                    Case &H6
                        cardName = cardName & ": SR176"
                        
                    Case &H7
                        cardName = cardName & ": SRI X4K"
                        
                    Case &H8
                        cardName = cardName & ": AT88RF020"
                        
                    Case &H9
                        cardName = cardName & ": AT88SC0204CRF"
                        
                    Case &HA
                        cardName = cardName & ": AT88SC0808CRF"
                        
                    Case &HB
                         cardName = cardName & ": AT88SC1616CRF"
                         
                    Case &HC
                        cardName = cardName & ": AT88SC3216CRF"
                        
                    Case &HD
                        cardName = cardName & ": AT88SC6416CRF"
                        
                    Case &HE
                        cardName = cardName & ": SRF55V10P"
                        
                    Case &HF
                        cardName = cardName & ": SRF55V02P"
                        
                    Case &H10
                        cardName = cardName & ": SRF55V10S"
                        
                    Case &H11
                        cardName = cardName & ": SRF55V02S"
                        
                    Case &H12
                        cardName = cardName & ": TAG IT"
                        
                    Case &H13
                        cardName = cardName & ": LRI512"
                        
                    Case &H14
                        cardName = cardName & ": ICODESLI"
                        
                    Case &H15
                        cardName = cardName & ": TEMPSENS"
        
                    Case &H16
                        cardName = cardName & ": I.CODE1"
                        
                    Case &H17
                        cardName = cardName & ": PicoPass 2K"
                        
                    Case &H18
                        cardName = cardName & ": PicoPass 2KS"
                        
                    Case &H19
                        cardName = cardName & ": PicoPass 16K"
                        
                    Case &H1A
                        cardName = cardName & ": PicoPass 16KS"
                        
                    Case &H1B
                        cardName = cardName & ": PicoPass 16K(8x2)"
                        
                    Case &H1C
                        cardName = cardName & ": PicoPass 16KS(8x2)"

                    Case &H1D
                        cardName = cardName & ": PicoPass 32KS(16+16)"
            
                    Case &H1E
                        cardName = cardName & ": PicoPass 32KS(16+8x2)"
            
                    Case &H1F
                        cardName = cardName & ": PicoPass 32KS(8x2+16)"
            
                    Case &H20
                        cardName = cardName & ": PicoPass 32KS(8x2+8x2)"
            
                    Case &H21
                        cardName = cardName & ": LRI64"
            
                    Case &H22
                        cardName = cardName & ": I.CODE UID"
                
                    Case &H23
                        cardName = cardName & ": I.CODE EPC"
            
                    Case &H24
                        cardName = cardName & ": LRI12"
            
                    Case &H25
                        cardName = cardName & ": LRI128"
            
                    Case &H26
                        cardName = cardName & ": Mifare Mini"
            
                End Select
            
            Else
            
                If ATRVal(13) = &HFF Then
                
                    Select Case ATRVal(14)
                    
                        Case &H9
                            cardName = cardName & ": Mifare Mini"
                    
                    End Select
                
                End If
            
            End If
            
            Call DisplayOut(3, 0, cardName & " is detected.")
        
        End If
        
    End If
    
    '4.2. Mifare DESFire card using ISO 14443 Part 4
    If CInt(ATRLen) = 11 Then
    
        RIDVal = ""
        
        For indx = 4 To 9
        
            RIDVal = RIDVal & Format(Hex(RecvBuff(indx)), "00")
        
        Next indx
        
        If RIDVal = "067577810280" Then
        
            Call DisplayOut(3, 0, "Mifare DESFire is detected.")
        
        End If
    
    End If
    
    '4.3. Other cards using ISO 14443 Part 4
    If CInt(ATRLen) = 17 Then
    
        RIDVal = ""
        
        For indx = 4 To 15
        
            RIDVal = RIDVal & Format(Hex(RecvBuff(indx)), "00")
        
        Next indx
        
        If RIDVal = "50122345561253544E3381C3" Then
        
            Call DisplayOut(3, 0, "ST19XRC8E is detected.")
        
        End If
    
    End If
    
    '4.4. other cards using ISO 14443 Type A or B
    If lATRStr = "3B8X800150" Then
    
        Call DisplayOut(3, 0, "ISO 14443B is detected.")
        
    Else
    
        If sATRStr = "3B8X8001" Then
        
            Call DisplayOut(3, 0, "ISO 14443A is detected.")
        
        End If
    
    End If
    
End Sub
