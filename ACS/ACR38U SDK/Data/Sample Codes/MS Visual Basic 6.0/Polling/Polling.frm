VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form MainPolling 
   Caption         =   "Card Detection Polling"
   ClientHeight    =   3585
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4590
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   9
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "Polling.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   3585
   ScaleWidth      =   4590
   StartUpPosition =   2  'CenterScreen
   Begin VB.Timer PollTimer 
      Enabled         =   0   'False
      Interval        =   100
      Left            =   240
      Top             =   2520
   End
   Begin VB.CommandButton bEnd 
      Caption         =   "&End Polling"
      Height          =   375
      Left            =   240
      TabIndex        =   7
      Top             =   1800
      Width           =   1575
   End
   Begin VB.CommandButton bStart 
      Caption         =   "&Start Polling"
      Height          =   375
      Left            =   240
      TabIndex        =   6
      Top             =   1080
      Width           =   1575
   End
   Begin MSComctlLib.StatusBar sbMsg 
      Align           =   2  'Align Bottom
      Height          =   375
      Left            =   0
      TabIndex        =   5
      Top             =   3210
      Width           =   4590
      _ExtentX        =   8096
      _ExtentY        =   661
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   1
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Object.Width           =   8308
            MinWidth        =   8308
         EndProperty
      EndProperty
   End
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   2640
      TabIndex        =   4
      Top             =   2520
      Width           =   1575
   End
   Begin VB.CommandButton bReset 
      Caption         =   "&Reset"
      Height          =   375
      Left            =   2640
      TabIndex        =   3
      Top             =   1800
      Width           =   1575
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initialize"
      Height          =   375
      Left            =   2640
      TabIndex        =   2
      Top             =   1080
      Width           =   1575
   End
   Begin VB.ComboBox cbReader 
      Height          =   330
      Left            =   1680
      TabIndex        =   1
      Top             =   360
      Width           =   2535
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
      Top             =   360
      Width           =   1455
   End
End
Attribute VB_Name = "MainPolling"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              Polling.frm
'
'  Description:       This sample program outlines the steps on how to
'                     automatically detect the insertion and removal
'                     of the smart card from the smartcard reader using
'                     the PC/SC platform.
'
'  Author:            Jose Isagani R. Mission
'
'  Date:              May 21, 2004
'
'  Revision Trail:    (Date/Author/Description)
'
'======================================================================
Option Explicit

Dim retCode, Protocol, hContext, hCard, ReaderCount As Long
Dim RdrState As SCARD_READERSTATE
Dim sReaderList As String * 256
Dim sReaderGroup As String

Private Sub InitMenu()

  cbReader.Clear
  bInit.Enabled = True
  bReset.Enabled = False
  bStart.Enabled = False
  bEnd.Enabled = False
  PollTimer.Enabled = False
  Call DisplayOut("Program ready")
  
End Sub

Private Sub DisplayOut(ByVal PrintText As String)

  sbMsg.Panels(1).Text = PrintText

End Sub

Private Sub AddButtons()

  bInit.Enabled = False
  bReset.Enabled = True
  bStart.Enabled = True
  Call DisplayOut("Ready for card detection polling.")

End Sub

Private Function CheckCard() As Boolean
  
  ' Get status change to determine if card is present or not
  RdrState.RdrName = cbReader.Text
  retCode = SCardGetStatusChange(hContext, 0, RdrState, 1)
  If retCode <> SCARD_S_SUCCESS Then
    Call DisplayOut(GetScardErrMsg(retCode))
    CheckCard = False
    Exit Function
  Else
   If (Int(RdrState.RdrEventState / 32) Mod 2) Then
     CheckCard = True
   Else
     CheckCard = False
   End If
 End If
 
End Function

Private Sub bEnd_Click()
  
  bStart.Enabled = True
  bEnd.Enabled = False
  PollTimer.Enabled = False
  Call DisplayOut("Card detection polling terminated")

End Sub

Private Sub bInit_Click()

  sReaderList = String(255, vbNullChar)
  ReaderCount = 255
     
  ' 1. Establish context and obtain hContext handle
  retCode = SCardEstablishContext(SCARD_SCOPE_USER, 0, 0, hContext)
  If retCode <> SCARD_S_SUCCESS Then
    Call DisplayOut(GetScardErrMsg(retCode))
    Exit Sub
  End If
  
  ' 2. List PC/SC card readers installed in the system
  retCode = SCardListReaders(hContext, sReaderGroup, sReaderList, ReaderCount)
  If retCode <> SCARD_S_SUCCESS Then
    Call DisplayOut(GetScardErrMsg(retCode))
    Exit Sub
  End If
  Call LoadListToControl(cbReader, sReaderList)
  cbReader.ListIndex = 0

  Call AddButtons
  
End Sub

Private Sub bQuit_Click()

  retCode = SCardReleaseContext(hContext)
  Unload Me
  
End Sub

Private Sub bReset_Click()

  retCode = SCardReleaseContext(hContext)
  Call InitMenu
  
End Sub

Private Sub bStart_Click()
  
  Call DisplayOut("")
  bStart.Enabled = False
  bEnd.Enabled = True
  PollTimer.Enabled = True

End Sub


Private Sub cbReader_Click()
  
  bStart.Enabled = True
  bEnd.Enabled = False
  PollTimer.Enabled = False
  Call DisplayOut("Select reader and Start")


End Sub

Private Sub Form_Load()

  Call InitMenu
  
End Sub

Private Sub PollTimer_Timer()
   
   If CheckCard() Then
    Call DisplayOut("Card is inserted.")
   Else
    Call DisplayOut("Card is removed.")
   End If

End Sub
