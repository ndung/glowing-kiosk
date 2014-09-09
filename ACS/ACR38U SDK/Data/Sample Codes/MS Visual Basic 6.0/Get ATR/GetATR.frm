VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form MainGetATR 
   Caption         =   "Getting ATR"
   ClientHeight    =   4365
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7335
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   9
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "GetATR.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   4365
   ScaleWidth      =   7335
   StartUpPosition =   2  'CenterScreen
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   4095
      Left            =   2640
      TabIndex        =   7
      Top             =   120
      Width           =   4575
      _ExtentX        =   8070
      _ExtentY        =   7223
      _Version        =   393217
      Enabled         =   -1  'True
      ScrollBars      =   2
      TextRTF         =   $"GetATR.frx":17A2
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
      Left            =   840
      TabIndex        =   6
      Top             =   3600
      Width           =   1575
   End
   Begin VB.CommandButton bReset 
      Caption         =   "&Reset"
      Height          =   375
      Left            =   840
      TabIndex        =   5
      Top             =   3000
      Width           =   1575
   End
   Begin VB.CommandButton bGetATR 
      Caption         =   "&Get ATR"
      Height          =   375
      Left            =   840
      TabIndex        =   4
      Top             =   2400
      Width           =   1575
   End
   Begin VB.CommandButton bConnect 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   840
      TabIndex        =   3
      Top             =   1800
      Width           =   1575
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initialize"
      Height          =   375
      Left            =   840
      TabIndex        =   2
      Top             =   1200
      Width           =   1575
   End
   Begin VB.ComboBox cbReader 
      Height          =   330
      Left            =   120
      TabIndex        =   1
      Top             =   600
      Width           =   2295
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
Attribute VB_Name = "MainGetATR"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              MainGetATR.frm
'
'  Description:       This sample program outlines the steps on how to
'                     get the ATR from a smart card using the PC/SC platform.,
'
'  Author:            Jose Isagani R. Mission
'
'  Date:              May 21, 2004
'
'  Revision Trail:   (Date/Author/Description)
'
'======================================================================

Option Explicit

Dim retCode, Protocol, hContext, hCard, ReaderCount As Long
Dim sReaderList As String * 256
Dim sReaderGroup As String
Dim ConnActive As Boolean


Private Sub InitMenu()

  cbReader.Clear
  bInit.Enabled = True
  bConnect.Enabled = False
  bGetATR.Enabled = False
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
  Call DisplayOut(3, 0, tmpStr)

  ' 3. Interpret dwActProtocol returned and display as active protocol
  Select Case Protocol
    Case 1
      tmpStr = "T=0"
    Case 2
      tmpStr = "T=1"
  End Select
  Call DisplayOut(3, 0, "Active protocol: " & tmpStr)
  
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

  bGetATR.Enabled = False
  If ConnActive Then
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
  End If
  
End Sub

Private Sub Form_Load()

  Call InitMenu
  
End Sub
