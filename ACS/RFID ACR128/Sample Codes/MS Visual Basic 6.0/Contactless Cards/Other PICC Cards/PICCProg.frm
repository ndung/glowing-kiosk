VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form MainPICCProg 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Programming Other PICC Cards"
   ClientHeight    =   6135
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   9255
   Icon            =   "PICCProg.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6135
   ScaleWidth      =   9255
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   7560
      TabIndex        =   15
      Top             =   5640
      Width           =   1575
   End
   Begin VB.CommandButton bReset 
      Caption         =   "&Reset"
      Height          =   375
      Left            =   5880
      TabIndex        =   14
      Top             =   5640
      Width           =   1575
   End
   Begin VB.CommandButton bClear 
      Caption         =   "C&lear"
      Height          =   375
      Left            =   4200
      TabIndex        =   13
      Top             =   5640
      Width           =   1575
   End
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   5415
      Left            =   4200
      TabIndex        =   26
      Top             =   120
      Width           =   4935
      _ExtentX        =   8705
      _ExtentY        =   9551
      _Version        =   393217
      ScrollBars      =   2
      TextRTF         =   $"PICCProg.frx":17A2
   End
   Begin VB.Frame frSendApdu 
      Caption         =   "Send Card Command"
      Height          =   3255
      Left            =   120
      TabIndex        =   18
      Top             =   2760
      Width           =   3975
      Begin VB.CommandButton bSend 
         Caption         =   "&Send Card Command"
         Height          =   375
         Left            =   1920
         TabIndex        =   12
         Top             =   2640
         Width           =   1935
      End
      Begin VB.TextBox tData 
         Height          =   975
         Left            =   120
         MultiLine       =   -1  'True
         TabIndex        =   11
         Top             =   1440
         Width           =   3735
      End
      Begin VB.TextBox tLe 
         Height          =   285
         Left            =   2520
         MaxLength       =   2
         TabIndex        =   10
         Top             =   720
         Width           =   495
      End
      Begin VB.TextBox tLc 
         Height          =   285
         Left            =   2040
         MaxLength       =   2
         TabIndex        =   9
         Top             =   720
         Width           =   495
      End
      Begin VB.TextBox tP2 
         Height          =   285
         Left            =   1560
         MaxLength       =   2
         TabIndex        =   8
         Top             =   720
         Width           =   495
      End
      Begin VB.TextBox tP1 
         Height          =   285
         Left            =   1080
         MaxLength       =   2
         TabIndex        =   7
         Top             =   720
         Width           =   495
      End
      Begin VB.TextBox tINS 
         Height          =   285
         Left            =   600
         MaxLength       =   2
         TabIndex        =   6
         Top             =   720
         Width           =   495
      End
      Begin VB.TextBox tCLA 
         Height          =   285
         Left            =   120
         MaxLength       =   2
         TabIndex        =   5
         Top             =   720
         Width           =   495
      End
      Begin VB.Label Label8 
         Caption         =   "Data In"
         Height          =   255
         Left            =   120
         TabIndex        =   25
         Top             =   1200
         Width           =   1575
      End
      Begin VB.Label Label7 
         Caption         =   "Le"
         Height          =   255
         Left            =   2640
         TabIndex        =   24
         Top             =   480
         Width           =   255
      End
      Begin VB.Label Label6 
         Caption         =   "Lc"
         Height          =   255
         Left            =   2160
         TabIndex        =   23
         Top             =   480
         Width           =   255
      End
      Begin VB.Label Label5 
         Caption         =   "P2"
         Height          =   255
         Left            =   1680
         TabIndex        =   22
         Top             =   480
         Width           =   255
      End
      Begin VB.Label Label4 
         Caption         =   "P1"
         Height          =   255
         Left            =   1200
         TabIndex        =   21
         Top             =   480
         Width           =   255
      End
      Begin VB.Label Label3 
         Caption         =   "INS"
         Height          =   255
         Left            =   720
         TabIndex        =   20
         Top             =   480
         Width           =   375
      End
      Begin VB.Label Label2 
         Caption         =   "CLA"
         Height          =   255
         Left            =   240
         TabIndex        =   19
         Top             =   480
         Width           =   375
      End
   End
   Begin VB.Frame frGetData 
      Caption         =   "Get Data Function"
      Height          =   975
      Left            =   120
      TabIndex        =   17
      Top             =   1680
      Width           =   3975
      Begin VB.CommandButton bGetData 
         Caption         =   "&Get Data"
         Height          =   375
         Left            =   2160
         TabIndex        =   4
         Top             =   360
         Width           =   1695
      End
      Begin VB.CheckBox cbIso14443A 
         Caption         =   "ISO 14443 A Card"
         Height          =   255
         Left            =   120
         TabIndex        =   3
         Top             =   480
         Width           =   1815
      End
   End
   Begin VB.CommandButton bConnect 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   2280
      TabIndex        =   2
      Top             =   1200
      Width           =   1815
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initialize"
      Height          =   375
      Left            =   2280
      TabIndex        =   1
      Top             =   720
      Width           =   1815
   End
   Begin VB.ComboBox cbReader 
      Height          =   315
      Left            =   1320
      TabIndex        =   0
      Top             =   240
      Width           =   2775
   End
   Begin VB.Label Label1 
      Caption         =   "Select Reader"
      Height          =   255
      Left            =   120
      TabIndex        =   16
      Top             =   240
      Width           =   1335
   End
End
Attribute VB_Name = "MainPICCProg"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              PICCProg.frm
'
'  Description:       This sample program outlines the steps on how to
'                     transact with other PICC cards using ACR128
'
'  Author:            M.J.E.C. Castillo
'
'  Date:              June 18, 2008
'
'  Revision Trail:   (Date/Author/Description)
'
'======================================================================

Option Explicit

Const INVALID_SW1SW2 = -450

Dim retCode, Protocol, hContext, hCard, ReaderCount As Long
Dim sReaderList As String * 256
Dim sReaderGroup As String
Dim ConnActive, validATS As Boolean
Dim ioRequest As SCARD_IO_REQUEST
Dim RdrState As SCARD_READERSTATE
Dim SendLen, RecvLen As Long
Dim SendBuff(0 To 262) As Byte
Dim RecvBuff(0 To 262) As Byte

Private Sub InitMenu()

    ConnActive = False
    validATS = False
    cbReader.Clear
    mMsg.Text = ""
    Call DisplayOut(0, 0, "Program Ready")
    bConnect.Enabled = False
    bInit.Enabled = True
    bReset.Enabled = False
    cbIso14443A.Value = vbUnchecked
    frGetData.Enabled = False
    tCLA.Text = ""
    tINS.Text = ""
    tP1.Text = ""
    tP2.Text = ""
    tLc.Text = ""
    tLe.Text = ""
    tData.Text = ""
    frSendApdu.Enabled = False

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
      
    'for card error
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

Private Sub ClearBuffers()

  Dim intIndx As Long
  
  For intIndx = 0 To 262
  
    RecvBuff(intIndx) = &H0
    SendBuff(intIndx) = &H0
    
  Next intIndx
  
End Sub

Private Function TrimInput(trimType As Integer, strIn As String) As String

Dim indx As Integer
Dim tmpStr As String
Dim charArray(0 To 99) As String

    'place each character of string to an array
    For indx = 1 To Len(strIn)
    
        charArray(indx) = Mid$(strIn, indx, 1)
    
    Next indx

    tmpStr = ""
    strIn = Trim(strIn)

    Select Case trimType
    
        Case 0
            For indx = 1 To Len(strIn)
            
                If (charArray(indx) <> Chr(13)) And (charArray(indx) <> Chr(10)) Then
                
                    tmpStr = tmpStr + charArray(indx)
                
                End If
            
            Next indx
    
        Case 1
            For indx = 1 To Len(strIn)
            
                If charArray(indx) <> " " Then
                
                    tmpStr = tmpStr + charArray(indx)
                
                End If
            
            Next indx
    
    End Select
    
    TrimInput = tmpStr

End Function

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
        
        If Trim(tmpStr) <> "90 00 " Then
          
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
        
      'Interpret SW1/SW2
      Case 3
          For indx = RecvLen - 2 To RecvLen - 1
          
            If InStr(Hex(RecvBuff(indx)), "A") = 2 Then
            
                tmpStr = tmpStr & Hex(RecvBuff(indx)) & " "
            
            Else
    
                tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2) & " "
        
            End If
          
          Next indx
          
          If Trim(tmpStr) = "6A 81" Then
          
            Call DisplayOut(4, 0, "The function is not supported.")
            SendAPDUandDisplay = retCode
            Exit Function
          
          End If
          
          validATS = True
        
    End Select
    
    Call DisplayOut(3, 0, tmpStr)
  End If
  SendAPDUandDisplay = retCode
  
End Function

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
   frGetData.Enabled = True
   frSendApdu.Enabled = True
   tCLA.SetFocus
   
End Sub

Private Sub bGetData_Click()

Dim indx As Integer
Dim tmpStr As String

    validATS = False
    Call ClearBuffers
    SendBuff(0) = &HFF          'CLA
    SendBuff(1) = &HCA          'INS
    
    If cbIso14443A.Value = vbChecked Then
    
        SendBuff(2) = &H1       'P1 : ISO 14443 A Card
        
    Else
    
        SendBuff(2) = &H0       'P1 : Other cards
    
    End If
    
    SendBuff(3) = &H0           'P2
    SendBuff(4) = &H0           'Le: Full length
    
    SendLen = SendBuff(4) + 5
    RecvLen = &HFF
    
    retCode = SendAPDUandDisplay(3)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
        
    End If
    
    'Interpret and display return values
    If validATS Then
    
        If cbIso14443A.Value = vbChecked Then
        
            tmpStr = "UID: "
        
        Else
        
            tmpStr = "ATS: "
        
        End If
        
        For indx = 0 To RecvLen - 3
        
            tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2) & " "
        
        Next indx
        
        Call DisplayOut(3, 0, Trim(tmpStr))
    
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

  'Look for ACR128 PICC and make it the default reader in the combobox
  For intIndx = 0 To cbReader.ListCount - 1
    
    cbReader.ListIndex = intIndx
    
    If InStr(cbReader.Text, "ACR128U PICC") > 0 Then
        
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

    If ConnActive Then
    
        retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    
    End If
    
    retCode = SCardReleaseContext(hCard)
    Call InitMenu

End Sub

Private Sub bSend_Click()

Dim tmpData As String
Dim directCmd As Boolean
Dim indx As Integer

    directCmd = True
    
    'validate input
    If tCLA.Text = "" Then
    
        tCLA.Text = "00"
        tCLA.SetFocus
        Exit Sub
    
    End If
    
    tmpData = ""
    
    Call ClearBuffers
    SendBuff(0) = CInt("&H" & tCLA.Text)        'CLA
    
    If tINS.Text <> "" Then
    
        SendBuff(1) = CInt("&H" & tINS.Text)    'INS
    
    End If
    
    If tP1.Text <> "" Then
    
        directCmd = False
    
    End If
    
    If directCmd = False Then
    
        SendBuff(2) = CInt("&H" & tP1.Text)     'P1
    
        If tP2.Text = "" Then
        
            tP2.Text = "00"                     'P2 : Ask user to confirm
            tP2.SetFocus
            Exit Sub
        
        Else
        
            SendBuff(3) = CInt("&H" & tP2.Text) 'P2
            
        End If
        
        If tLc.Text <> "" Then
        
            SendBuff(4) = CInt("&H" & tLc.Text) 'Lc
            
            'Process Data In if Lc > 0
            If SendBuff(4) > 0 Then
                
                tmpData = TrimInput(0, tData.Text)
                tmpData = TrimInput(1, tmpData)
                
                'Check if Data In is consistent with Lc value
                If SendBuff(4) > Fix((Len(tmpData) / 2)) Then
                
                    tData.SetFocus
                    Exit Sub
                
                End If
                
                For indx = 0 To SendBuff(4) - 1
                
                    'Format Data In
                    SendBuff(indx + 5) = CInt("&H" & Mid$(tmpData, ((indx * 2) + 1), 2))
                
                Next indx
                
                If tLe.Text <> "" Then
                
                    SendBuff(SendBuff(4) + 5) = CInt("&H" & tLe.Text)   'Le
                
                End If
            
            Else
            
                If tLe.Text <> "" Then
                
                    SendBuff(5) = CInt("&H" & tLe.Text)                 'Le
                
                End If
            
            End If
        
        Else
        
            If tLe.Text <> "" Then
            
                SendBuff(4) = CInt("&H" & tLe.Text)                     'Le
            
            End If
        
        End If
        
    End If

    If directCmd = True Then
    
        If tINS.Text = "" Then
        
            SendLen = &H1
            
        Else
        
            SendLen = &H2
        
        End If
    
    Else
    
        If tLc.Text = "" Then
        
            If tLe.Text <> "" Then
            
                SendLen = 5
                
            Else
            
                SendLen = 4
            
            End If
        
        Else
        
            If tLe.Text = "" Then
            
                SendLen = SendBuff(4) + 5
                
            Else
            
                SendLen = SendBuff(4) + 6
            
            End If
        
        End If
    
    End If
    
    RecvLen = &HFF
    
    retCode = SendAPDUandDisplay(2)
     
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If

End Sub

Private Sub Form_Load()

    Call InitMenu

End Sub

'for user input control

Private Sub tCLA_Change()

    If Len(tCLA.Text) > 1 Then
    
       tINS.SetFocus
    
    End If

End Sub

Private Sub tCLA_GotFocus()

       tCLA.SelStart = 0
       tCLA.SelLength = Len(tCLA.Text)

End Sub

Private Sub tINS_GotFocus()

       tINS.SelStart = 0
       tINS.SelLength = Len(tINS.Text)

End Sub

Private Sub tP1_GotFocus()

       tP1.SelStart = 0
       tP1.SelLength = Len(tP1.Text)

End Sub

Private Sub tP2_GotFocus()

       tP2.SelStart = 0
       tP2.SelLength = Len(tP2.Text)

End Sub

Private Sub tLe_GotFocus()

       tLe.SelStart = 0
       tLe.SelLength = Len(tLe.Text)

End Sub

Private Sub tLc_GotFocus()

       tLc.SelStart = 0
       tLc.SelLength = Len(tLc.Text)

End Sub

Private Sub tData_GotFocus()

       tData.SelStart = 0
       tData.SelLength = Len(tData.Text)

End Sub

Private Sub tCLA_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tData_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF  ", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If
    
    tData.MaxLength = CInt(tLc.Text) * 2

End Sub

Private Sub tINS_Change()

    If Len(tINS.Text) > 1 Then
    
       tP1.SetFocus
    
    End If

End Sub

Private Sub tINS_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tLc_Change()

    If Len(tLc.Text) > 1 Then
    
       tLe.SetFocus
    
    End If

End Sub

Private Sub tLc_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tLe_Change()

    If Len(tLe.Text) > 1 Then
    
       tData.SetFocus
    
    End If

End Sub

Private Sub tLe_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tP1_Change()

    If Len(tP1.Text) > 1 Then
    
       tP2.SetFocus
    
    End If

End Sub

Private Sub tP1_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tP2_Change()

    If Len(tP2.Text) > 1 Then
    
       tLc.SetFocus
    
    End If

End Sub

Private Sub tP2_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub
