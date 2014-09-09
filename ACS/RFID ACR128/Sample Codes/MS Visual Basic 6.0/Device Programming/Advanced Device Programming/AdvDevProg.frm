VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form MainAdvDevProg 
   Caption         =   "Advance Device Programming"
   ClientHeight    =   8490
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   14760
   Icon            =   "AdvDevProg.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   8490
   ScaleWidth      =   14760
   StartUpPosition =   1  'CenterOwner
   Begin VB.Timer PollTimer 
      Enabled         =   0   'False
      Interval        =   500
      Left            =   14400
      Top             =   0
   End
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   13080
      TabIndex        =   46
      Top             =   7920
      Width           =   1455
   End
   Begin VB.CommandButton bReset 
      Caption         =   "&Reset"
      Height          =   375
      Left            =   11520
      TabIndex        =   45
      Top             =   7920
      Width           =   1455
   End
   Begin VB.CommandButton bClear 
      Caption         =   "C&lear Screen"
      Height          =   375
      Left            =   9960
      TabIndex        =   44
      Top             =   7920
      Width           =   1455
   End
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   4935
      Left            =   9840
      TabIndex        =   103
      Top             =   2880
      Width           =   4815
      _ExtentX        =   8493
      _ExtentY        =   8705
      _Version        =   393217
      ScrollBars      =   2
      TextRTF         =   $"AdvDevProg.frx":17A2
   End
   Begin VB.Frame frRefIS 
      Caption         =   "Refresh Interface Status"
      Height          =   1335
      Left            =   9840
      TabIndex        =   99
      Top             =   1440
      Width           =   4815
      Begin VB.CommandButton bRefIS 
         Caption         =   "Re&fresh Interface"
         Height          =   375
         Left            =   2760
         TabIndex        =   43
         Top             =   720
         Width           =   1815
      End
      Begin VB.OptionButton rbRIS3 
         Caption         =   "SAM Interface"
         Height          =   255
         Left            =   240
         TabIndex        =   102
         Top             =   960
         Width           =   2055
      End
      Begin VB.OptionButton rbRIS2 
         Caption         =   "PICC Interface"
         Height          =   255
         Left            =   240
         TabIndex        =   101
         Top             =   600
         Width           =   1695
      End
      Begin VB.OptionButton rbRIS1 
         Caption         =   "ICC Interface"
         Height          =   255
         Left            =   240
         TabIndex        =   100
         Top             =   240
         Width           =   2175
      End
   End
   Begin VB.Frame frReg 
      Caption         =   "RC531 Register Setting"
      Height          =   1215
      Left            =   9840
      TabIndex        =   96
      Top             =   120
      Width           =   4815
      Begin VB.CommandButton bSetReg 
         Caption         =   "Set New Value"
         Height          =   375
         Left            =   2760
         TabIndex        =   42
         Top             =   720
         Width           =   1935
      End
      Begin VB.CommandButton bGetReg 
         Caption         =   "Get Current value"
         Height          =   375
         Left            =   2760
         TabIndex        =   41
         Top             =   240
         Width           =   1935
      End
      Begin VB.TextBox tRegVal 
         Height          =   285
         Left            =   240
         MaxLength       =   2
         TabIndex        =   40
         Top             =   720
         Width           =   495
      End
      Begin VB.TextBox tRegNum 
         Height          =   285
         Left            =   240
         MaxLength       =   2
         TabIndex        =   39
         Top             =   360
         Width           =   495
      End
      Begin VB.Label Label25 
         Caption         =   "Register Value"
         Height          =   255
         Left            =   840
         TabIndex        =   98
         Top             =   720
         Width           =   1695
      End
      Begin VB.Label Label24 
         Caption         =   "Register Number"
         Height          =   255
         Left            =   840
         TabIndex        =   97
         Top             =   360
         Width           =   1815
      End
   End
   Begin VB.Frame frPPS 
      Caption         =   "PPS Setting (Communication Speed)"
      Height          =   3255
      Left            =   4560
      TabIndex        =   83
      Top             =   5040
      Width           =   5175
      Begin VB.CommandButton bSetPPS 
         Caption         =   "Set PPS Value"
         Height          =   375
         Left            =   3360
         TabIndex        =   38
         Top             =   2640
         Width           =   1575
      End
      Begin VB.CommandButton bGetPPS 
         Caption         =   "Get Current Setting"
         Height          =   375
         Left            =   1680
         TabIndex        =   37
         Top             =   2640
         Width           =   1575
      End
      Begin VB.Frame frCurrSpeed 
         Caption         =   "Current Speed"
         Height          =   2175
         Left            =   2760
         TabIndex        =   90
         Top             =   360
         Width           =   2055
         Begin VB.OptionButton rbCurr5 
            Caption         =   "No Auto PPS"
            Height          =   255
            Left            =   120
            TabIndex        =   95
            Top             =   1800
            Width           =   1695
         End
         Begin VB.OptionButton rbCurr4 
            Caption         =   "848 kbps"
            Height          =   255
            Left            =   120
            TabIndex        =   94
            Top             =   1440
            Width           =   1815
         End
         Begin VB.OptionButton rbCurr3 
            Caption         =   "424 kbps"
            Height          =   195
            Left            =   120
            TabIndex        =   93
            Top             =   1080
            Width           =   1695
         End
         Begin VB.OptionButton rbCurr2 
            Caption         =   "212 kbps"
            Height          =   255
            Left            =   120
            TabIndex        =   92
            Top             =   720
            Width           =   1815
         End
         Begin VB.OptionButton rbCurr1 
            Caption         =   "106 kbps"
            Height          =   255
            Left            =   120
            TabIndex        =   91
            Top             =   360
            Width           =   1575
         End
      End
      Begin VB.Frame frMaxSpeed 
         Caption         =   "Maximum Speed"
         Height          =   2175
         Left            =   360
         TabIndex        =   84
         Top             =   360
         Width           =   2055
         Begin VB.OptionButton rbMax5 
            Caption         =   "No Auto PPS"
            Height          =   255
            Left            =   120
            TabIndex        =   89
            Top             =   1800
            Width           =   1695
         End
         Begin VB.OptionButton rbMax4 
            Caption         =   "848 kbps"
            Height          =   255
            Left            =   120
            TabIndex        =   88
            Top             =   1440
            Width           =   1815
         End
         Begin VB.OptionButton rbMax3 
            Caption         =   "424 kbps"
            Height          =   255
            Left            =   120
            TabIndex        =   87
            Top             =   1080
            Width           =   1695
         End
         Begin VB.OptionButton rbMax2 
            Caption         =   "212 kbps"
            Height          =   255
            Left            =   120
            TabIndex        =   86
            Top             =   720
            Width           =   1815
         End
         Begin VB.OptionButton rbMax1 
            Caption         =   "106 kbps"
            Height          =   255
            Left            =   120
            TabIndex        =   85
            Top             =   360
            Width           =   1815
         End
      End
   End
   Begin VB.Frame frPolling 
      Caption         =   "Polling for Specific PICC Types"
      Height          =   2175
      Left            =   4560
      TabIndex        =   77
      Top             =   2760
      Width           =   5175
      Begin VB.CommandButton bPoll 
         Caption         =   "Start Auto &Detection"
         Height          =   375
         Left            =   3000
         TabIndex        =   36
         Top             =   1200
         Width           =   1935
      End
      Begin VB.CommandButton bSetPSet 
         Caption         =   "Set PICC T&ype"
         Height          =   375
         Left            =   3000
         TabIndex        =   35
         Top             =   720
         Width           =   1935
      End
      Begin VB.CommandButton bGetPSet 
         Caption         =   "Get Curre&nt Setting"
         Height          =   375
         Left            =   3000
         TabIndex        =   34
         Top             =   240
         Width           =   1935
      End
      Begin VB.TextBox tMsg 
         Height          =   285
         Left            =   960
         TabIndex        =   82
         Top             =   1680
         Width           =   3975
      End
      Begin VB.OptionButton rbType3 
         Caption         =   "ISO14443 Types A/B"
         Height          =   255
         Left            =   240
         TabIndex        =   80
         Top             =   1080
         Width           =   2535
      End
      Begin VB.OptionButton rbType2 
         Caption         =   "ISO14443 Type B only"
         Height          =   255
         Left            =   240
         TabIndex        =   79
         Top             =   720
         Width           =   2655
      End
      Begin VB.OptionButton rbType1 
         Caption         =   "ISO14443 Type A only"
         Height          =   255
         Left            =   240
         TabIndex        =   78
         Top             =   360
         Width           =   2415
      End
      Begin VB.Label Label23 
         Caption         =   "Message"
         Height          =   255
         Left            =   240
         TabIndex        =   81
         Top             =   1680
         Width           =   735
      End
   End
   Begin VB.Frame frPICC 
      Caption         =   "PICC Settings"
      Height          =   2535
      Left            =   4560
      TabIndex        =   64
      Top             =   120
      Width           =   5175
      Begin VB.CommandButton bSetPICC 
         Caption         =   "Set PICC &Options"
         Height          =   375
         Left            =   3360
         TabIndex        =   33
         Top             =   1920
         Width           =   1575
      End
      Begin VB.CommandButton bGetPICC 
         Caption         =   "Get &PICC Setting"
         Height          =   375
         Left            =   3360
         TabIndex        =   32
         Top             =   1440
         Width           =   1575
      End
      Begin VB.TextBox tPICC12 
         Height          =   285
         Left            =   1800
         MaxLength       =   2
         TabIndex        =   31
         Top             =   2160
         Width           =   495
      End
      Begin VB.TextBox tPICC11 
         Height          =   285
         Left            =   1800
         MaxLength       =   2
         TabIndex        =   30
         Top             =   1800
         Width           =   495
      End
      Begin VB.TextBox tPICC10 
         Height          =   285
         Left            =   1800
         MaxLength       =   2
         TabIndex        =   29
         Top             =   1440
         Width           =   495
      End
      Begin VB.TextBox tPICC9 
         Height          =   285
         Left            =   1800
         MaxLength       =   2
         TabIndex        =   28
         Top             =   1080
         Width           =   495
      End
      Begin VB.TextBox tPICC8 
         Height          =   285
         Left            =   1800
         MaxLength       =   2
         TabIndex        =   27
         Top             =   720
         Width           =   495
      End
      Begin VB.TextBox tPICC7 
         Height          =   285
         Left            =   1800
         MaxLength       =   2
         TabIndex        =   26
         Top             =   360
         Width           =   495
      End
      Begin VB.TextBox tPICC6 
         Height          =   285
         Left            =   240
         MaxLength       =   2
         TabIndex        =   25
         Top             =   2160
         Width           =   495
      End
      Begin VB.TextBox tPICC5 
         Height          =   285
         Left            =   240
         MaxLength       =   2
         TabIndex        =   24
         Top             =   1800
         Width           =   495
      End
      Begin VB.TextBox tPICC4 
         Height          =   285
         Left            =   240
         MaxLength       =   2
         TabIndex        =   23
         Top             =   1440
         Width           =   495
      End
      Begin VB.TextBox tPICC3 
         Height          =   285
         Left            =   240
         MaxLength       =   2
         TabIndex        =   22
         Top             =   1080
         Width           =   495
      End
      Begin VB.TextBox tPICC2 
         Height          =   285
         Left            =   240
         MaxLength       =   2
         TabIndex        =   21
         Top             =   720
         Width           =   495
      End
      Begin VB.TextBox tPICC1 
         Height          =   285
         Left            =   240
         MaxLength       =   2
         TabIndex        =   20
         Top             =   360
         Width           =   495
      End
      Begin VB.Label Label22 
         Caption         =   "RX_A2"
         Height          =   255
         Left            =   2400
         TabIndex        =   76
         Top             =   2160
         Width           =   855
      End
      Begin VB.Label Label21 
         Caption         =   "COND_A2"
         Height          =   255
         Left            =   2400
         TabIndex        =   75
         Top             =   1800
         Width           =   855
      End
      Begin VB.Label Label20 
         Caption         =   "MOD_A2"
         Height          =   255
         Left            =   2400
         TabIndex        =   74
         Top             =   1440
         Width           =   855
      End
      Begin VB.Label Label19 
         Caption         =   "RX_A1"
         Height          =   255
         Left            =   2400
         TabIndex        =   73
         Top             =   1080
         Width           =   735
      End
      Begin VB.Label Label18 
         Caption         =   "COND_A1"
         Height          =   255
         Left            =   2400
         TabIndex        =   72
         Top             =   720
         Width           =   735
      End
      Begin VB.Label Label17 
         Caption         =   "MOD_A1"
         Height          =   255
         Left            =   2400
         TabIndex        =   71
         Top             =   360
         Width           =   855
      End
      Begin VB.Label Label16 
         Caption         =   "RX_B2"
         Height          =   255
         Left            =   840
         TabIndex        =   70
         Top             =   2160
         Width           =   735
      End
      Begin VB.Label Label15 
         Caption         =   "COND_B2"
         Height          =   255
         Left            =   840
         TabIndex        =   69
         Top             =   1800
         Width           =   855
      End
      Begin VB.Label Label14 
         Caption         =   "MOD_B2"
         Height          =   255
         Left            =   840
         TabIndex        =   68
         Top             =   1440
         Width           =   855
      End
      Begin VB.Label Label13 
         Caption         =   "RX_B1"
         Height          =   255
         Left            =   840
         TabIndex        =   67
         Top             =   1080
         Width           =   735
      End
      Begin VB.Label Label12 
         Caption         =   "COND_B1"
         Height          =   255
         Left            =   840
         TabIndex        =   66
         Top             =   720
         Width           =   735
      End
      Begin VB.Label Label11 
         Caption         =   "MOD_B1"
         Height          =   255
         Left            =   840
         TabIndex        =   65
         Top             =   360
         Width           =   855
      End
   End
   Begin VB.Frame frErrHand 
      Caption         =   "PICC T=CL Data Exchange Error Handling"
      Height          =   1335
      Left            =   120
      TabIndex        =   61
      Top             =   6960
      Width           =   4335
      Begin VB.CommandButton bSetEH 
         Caption         =   "Set Error &Handling"
         Height          =   375
         Left            =   2400
         TabIndex        =   19
         Top             =   840
         Width           =   1695
      End
      Begin VB.CommandButton bGetEH 
         Caption         =   "Get Current &Value"
         Height          =   375
         Left            =   2400
         TabIndex        =   18
         Top             =   360
         Width           =   1695
      End
      Begin VB.TextBox tPi2Pc 
         Height          =   285
         Left            =   240
         MaxLength       =   1
         TabIndex        =   17
         Top             =   840
         Width           =   495
      End
      Begin VB.TextBox tPc2Pi 
         Height          =   285
         Left            =   240
         MaxLength       =   1
         TabIndex        =   16
         Top             =   360
         Width           =   495
      End
      Begin VB.Label Label10 
         Caption         =   "PICC to PCD"
         Height          =   255
         Left            =   840
         TabIndex        =   63
         Top             =   840
         Width           =   1095
      End
      Begin VB.Label Label9 
         Caption         =   "PCD to PICC"
         Height          =   255
         Left            =   840
         TabIndex        =   62
         Top             =   360
         Width           =   1215
      End
   End
   Begin VB.Frame frTransSet 
      Caption         =   "Tranceiver Settings"
      Height          =   2295
      Left            =   120
      TabIndex        =   55
      Top             =   4560
      Width           =   4335
      Begin VB.CommandButton bSetTranSet 
         Caption         =   "Set &Tranceiver Options"
         Height          =   375
         Left            =   2280
         TabIndex        =   15
         Top             =   1680
         Width           =   1815
      End
      Begin VB.CommandButton bGetTranSet 
         Caption         =   "G&et Current Setting"
         Height          =   375
         Left            =   2280
         TabIndex        =   14
         Top             =   1200
         Width           =   1815
      End
      Begin VB.TextBox tTxMode 
         Height          =   285
         Left            =   240
         MaxLength       =   2
         TabIndex        =   13
         Top             =   1800
         Width           =   495
      End
      Begin VB.TextBox tRecGain 
         Height          =   285
         Left            =   240
         MaxLength       =   2
         TabIndex        =   12
         Top             =   1440
         Width           =   495
      End
      Begin VB.CheckBox cbFilter 
         Caption         =   "        LP Filter (On/Off)"
         Height          =   255
         Left            =   240
         TabIndex        =   56
         Top             =   1080
         Width           =   2055
      End
      Begin VB.TextBox tSetup 
         Height          =   285
         Left            =   240
         MaxLength       =   2
         TabIndex        =   11
         Top             =   720
         Width           =   495
      End
      Begin VB.TextBox tFStop 
         Height          =   285
         Left            =   240
         MaxLength       =   2
         TabIndex        =   10
         Top             =   360
         Width           =   495
      End
      Begin VB.Label Label8 
         Caption         =   "TX Mode"
         Height          =   255
         Left            =   840
         TabIndex        =   60
         Top             =   1800
         Width           =   1455
      End
      Begin VB.Label Label7 
         Caption         =   "Receiver Gain"
         Height          =   255
         Left            =   840
         TabIndex        =   59
         Top             =   1440
         Width           =   1455
      End
      Begin VB.Label Label6 
         Caption         =   "Setup time (x10 ms)"
         Height          =   255
         Left            =   840
         TabIndex        =   58
         Top             =   720
         Width           =   1695
      End
      Begin VB.Label Label5 
         Caption         =   "Field Stop Time (x5 ms)"
         Height          =   255
         Left            =   840
         TabIndex        =   57
         Top             =   360
         Width           =   1815
      End
   End
   Begin VB.Frame frAntenna 
      Caption         =   "Antenna Field Setting"
      Height          =   1215
      Left            =   120
      TabIndex        =   52
      Top             =   3240
      Width           =   4335
      Begin VB.CommandButton bSetAS 
         Caption         =   "Set &Antenna Option"
         Height          =   375
         Left            =   2520
         TabIndex        =   9
         Top             =   720
         Width           =   1575
      End
      Begin VB.CommandButton bGetAS 
         Caption         =   "Get C&urrent Setting"
         Height          =   375
         Left            =   2520
         TabIndex        =   8
         Top             =   240
         Width           =   1575
      End
      Begin VB.OptionButton rbAntOff 
         Caption         =   "Antenna OFF"
         Height          =   255
         Left            =   120
         TabIndex        =   54
         Top             =   720
         Width           =   1455
      End
      Begin VB.OptionButton rbAntOn 
         Caption         =   "Antenna ON"
         Height          =   255
         Left            =   120
         TabIndex        =   53
         Top             =   360
         Width           =   1695
      End
   End
   Begin VB.Frame frFWI 
      Caption         =   "FWI, Polling Timeout and Transmit Frame Size"
      Height          =   1455
      Left            =   120
      TabIndex        =   48
      Top             =   1680
      Width           =   4335
      Begin VB.CommandButton bSetFWI 
         Caption         =   "&Set Options"
         Height          =   375
         Left            =   2520
         TabIndex        =   7
         Top             =   840
         Width           =   1575
      End
      Begin VB.CommandButton bGetFWI 
         Caption         =   "&Get Current Values"
         Height          =   375
         Left            =   2520
         TabIndex        =   6
         Top             =   360
         Width           =   1575
      End
      Begin VB.TextBox tFS 
         Height          =   285
         Left            =   240
         MaxLength       =   2
         TabIndex        =   5
         Top             =   1080
         Width           =   495
      End
      Begin VB.TextBox tPollTO 
         Height          =   285
         Left            =   240
         MaxLength       =   2
         TabIndex        =   4
         Top             =   720
         Width           =   495
      End
      Begin VB.TextBox tFWI 
         Height          =   285
         Left            =   240
         MaxLength       =   2
         TabIndex        =   3
         Top             =   360
         Width           =   495
      End
      Begin VB.Label Label4 
         Caption         =   "Transmit Frame Size"
         Height          =   255
         Left            =   840
         TabIndex        =   51
         Top             =   1080
         Width           =   1575
      End
      Begin VB.Label Label3 
         Caption         =   "Polling timeout"
         Height          =   255
         Left            =   840
         TabIndex        =   50
         Top             =   720
         Width           =   1215
      End
      Begin VB.Label Label2 
         Caption         =   "FWI value"
         Height          =   255
         Left            =   840
         TabIndex        =   49
         Top             =   360
         Width           =   975
      End
   End
   Begin VB.CommandButton bConnect 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   2880
      TabIndex        =   2
      Top             =   1200
      Width           =   1575
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initialize"
      Height          =   375
      Left            =   2880
      TabIndex        =   1
      Top             =   720
      Width           =   1575
   End
   Begin VB.ComboBox cbReader 
      Height          =   315
      Left            =   1320
      TabIndex        =   0
      Top             =   240
      Width           =   3135
   End
   Begin VB.Label Label1 
      Caption         =   "Select Reader"
      Height          =   255
      Left            =   240
      TabIndex        =   47
      Top             =   360
      Width           =   1335
   End
End
Attribute VB_Name = "MainAdvDevProg"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              AdvDevProg.frm
'
'  Description:       This sample program outlines the steps on how to
'                     execute advanced device-specific functions of ACR128
'
'  Author:            M.J.E.C. Castillo
'
'  Date:              June 13, 2008
'
'  Revision Trail:   (Date/Author/Description)
'
'======================================================================

Option Explicit

Const INVALID_SW1SW2 = -450

Dim retCode, Protocol, hContext, hCard, ReaderCount As Long
Dim sReaderList As String * 256
Dim sReaderGroup As String
Dim ConnActive, autoDet As Boolean
Dim ioRequest As SCARD_IO_REQUEST
Dim RdrState As SCARD_READERSTATE
Dim SendLen, RecvLen, nBytesRet As Long
Dim SendBuff(0 To 262) As Byte
Dim RecvBuff(0 To 262) As Byte
Dim reqType As Integer

Private Sub InitMenu()

    ConnActive = False
    autoDet = False
    PollTimer.Enabled = False
    cbReader.Clear
    mMsg.Text = ""
    Call DisplayOut(0, 0, "Program ready")
    bConnect.Enabled = False
    bInit.Enabled = True
    bReset.Enabled = False
    tFWI.Text = ""
    tPollTO.Text = ""
    tFS.Text = ""
    frFWI.Enabled = False
    rbAntOn.Enabled = False
    rbAntOff.Enabled = False
    frAntenna.Enabled = False
    cbFilter.Value = vbUnchecked
    tRecGain.Text = ""
    frTransSet.Enabled = False
    tPICC1.Text = ""
    tPICC2.Text = ""
    tPICC3.Text = ""
    tPICC4.Text = ""
    tPICC5.Text = ""
    tPICC6.Text = ""
    tPICC7.Text = ""
    tPICC8.Text = ""
    tPICC9.Text = ""
    tPICC10.Text = ""
    tPICC11.Text = ""
    tPICC12.Text = ""
    frPICC.Enabled = False
    tMsg.Text = ""
    rbType1.Value = False
    rbType2.Value = False
    rbType3.Value = False
    frPolling.Enabled = False
    frErrHand.Enabled = False
    frMaxSpeed.Enabled = False
    frCurrSpeed.Enabled = False
    frPPS.Enabled = False
    tRegNum.Text = ""
    tRegVal.Text = ""
    frReg.Enabled = False
    rbRIS1.Enabled = False
    rbRIS2.Enabled = False
    rbRIS3.Enabled = False
    frRefIS.Enabled = False

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

End Sub

Private Sub ClearBuffers()

  Dim intIndx As Long
  
  For intIndx = 0 To 262
  
    RecvBuff(intIndx) = &H0
    SendBuff(intIndx) = &H0
    
  Next intIndx
  
End Sub

Private Function CallCardControl() As Long

Dim tmpStr As String
Dim intIndx As Integer

    'Display Apdu In
    tmpStr = "SCardControl: "
    
    For intIndx = 0 To SendLen - 1
        
        'checks if hex value ends in A since VB6 does not format it properly
        If InStr(Hex(SendBuff(intIndx)), "A") = 2 Then
            
            tmpStr = tmpStr & Hex(SendBuff(intIndx)) & " "
            
        Else
    
            tmpStr = tmpStr & Right$("00" & Hex(SendBuff(intIndx)), 2) & " "
        
        End If
     
    Next intIndx
    
    Call DisplayOut(2, 0, tmpStr)
    
    retCode = SCardControl(hCard, _
                        IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND, _
                        SendBuff(0), _
                        SendLen, _
                        RecvBuff(0), _
                        RecvLen, _
                        nBytesRet)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Call DisplayOut(1, retCode, "")
    
    Else
    
        tmpStr = ""
        
        For intIndx = 0 To (RecvLen - 1)
        
            tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(intIndx)), 2) & " "
        
        Next intIndx
        
        Call DisplayOut(3, 0, Trim(tmpStr))
    
    End If
    
    CallCardControl = retCode

End Function

Private Sub ReadPollingOption()

    Call ClearBuffers
    SendBuff(0) = &H23
    SendBuff(1) = &H0
    SendLen = 2
    RecvLen = 6
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If

End Sub


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
    frFWI.Enabled = True
    frAntenna.Enabled = True
    frTransSet.Enabled = True
    frPICC.Enabled = True
    frPolling.Enabled = True
    rbType3.Value = True
    frErrHand.Enabled = True
    frPPS.Enabled = True
    frReg.Enabled = True
    frRefIS.Enabled = True
    rbRIS3.Value = True
    rbRIS1.Enabled = True
    rbRIS2.Enabled = True
    rbRIS3.Enabled = True
    rbType1.Value = True
    rbType2.Value = True
    rbType3.Value = True
    rbAntOff.Enabled = True
    rbAntOn.Enabled = True
    frMaxSpeed.Enabled = True
    frCurrSpeed.Enabled = True

End Sub

Private Sub bGetAS_Click()

Dim tmpStr As String
Dim indx As Integer

    Call ClearBuffers
    SendBuff(0) = &H25
    SendBuff(1) = &H0
    SendLen = 2
    RecvLen = 6
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    tmpStr = ""
    
    For indx = 0 To 4
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2)
    
    Next indx
    
    If tmpStr = "E100000001" Then
    
        'Interpret response data
        If RecvBuff(5) = 0 Then
        
            rbAntOff.Value = True
            
        Else
        
            rbAntOn.Value = True
        
        End If
    
    Else
    
        rbAntOff.Value = False
        rbAntOn.Value = False
        Call DisplayOut(3, 0, "Invalid response")
    
    End If

End Sub

Private Sub bGetEH_Click()

Dim tmpStr As String
Dim tmpVal As Integer
Dim indx As Integer

    Call ClearBuffers
    SendBuff(0) = &H2C
    SendBuff(1) = &H0
    SendLen = 2
    RecvLen = 7
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    tmpStr = ""
    
    For indx = 0 To 4
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2)
    
    Next indx
    
    If ((tmpStr = "E100000002") And (RecvBuff(6) = &H7F)) Then
    
        'Interpret response data
        tmpVal = RecvBuff(5)
        
        'shift bit to right
        For indx = 1 To 4
        
            tmpVal = tmpVal / 2
        
        Next indx
        
        tPc2Pi.Text = CStr(tmpVal)
        tmpVal = RecvBuff(5) And &H3
        tPi2Pc.Text = CStr(tmpVal)
    
    Else
    
        tPc2Pi.Text = ""
        tPi2Pc.Text = ""
        Call DisplayOut(3, 0, "Invalid response")
    
    End If

End Sub

Private Sub bGetFWI_Click()

Dim tmpStr As String
Dim indx As Integer

    Call ClearBuffers
    SendBuff(0) = &H1F
    SendBuff(1) = &H0
    SendLen = 2
    RecvLen = 8
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    tmpStr = ""
    
    For indx = 0 To 4
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2)
    
    Next indx
    
    If tmpStr = "E100000003" Then
    
        'Interpret response data
        tFWI.Text = Right$("00" & Hex(RecvBuff(5)), 2)
        tPollTO.Text = Right$("00" & Hex(RecvBuff(6)), 2)
        tFS.Text = Right$("00" & Hex(RecvBuff(7)), 2)
        
    Else
    
        tFWI.Text = ""
        tPollTO.Text = ""
        tFS.Text = ""
        Call DisplayOut(3, 0, "Invalid response")
    
    End If

End Sub

Private Sub bGetPICC_Click()

Dim tmpStr As String
Dim indx As Integer

    Call ClearBuffers
    SendBuff(0) = &H2A
    SendBuff(1) = &H0
    SendLen = 2
    RecvLen = 17
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    tmpStr = ""
    
    For indx = 0 To 4
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2)
    
    Next indx
    
    'MsgBox retCode
    If tmpStr = "E10000000C" Then
    
        'interpret response data
        tPICC1.Text = Right$("00" & Hex(RecvBuff(5)), 2)
        tPICC2.Text = Right$("00" & Hex(RecvBuff(6)), 2)
        tPICC3.Text = Right$("00" & Hex(RecvBuff(7)), 2)
        tPICC4.Text = Right$("00" & Hex(RecvBuff(8)), 2)
        tPICC5.Text = Right$("00" & Hex(RecvBuff(9)), 2)
        tPICC6.Text = Right$("00" & Hex(RecvBuff(10)), 2)
        tPICC7.Text = Right$("00" & Hex(RecvBuff(11)), 2)
        tPICC8.Text = Right$("00" & Hex(RecvBuff(12)), 2)
        tPICC9.Text = Right$("00" & Hex(RecvBuff(13)), 2)
        tPICC10.Text = Right$("00" & Hex(RecvBuff(14)), 2)
        tPICC11.Text = Right$("00" & Hex(RecvBuff(15)), 2)
        tPICC12.Text = Right$("00" & Hex(RecvBuff(16)), 2)
    
    Else
    
        tPICC1.Text = ""
        tPICC2.Text = ""
        tPICC3.Text = ""
        tPICC4.Text = ""
        tPICC5.Text = ""
        tPICC6.Text = ""
        tPICC7.Text = ""
        tPICC8.Text = ""
        tPICC9.Text = ""
        tPICC10.Text = ""
        tPICC11.Text = ""
        tPICC12.Text = ""
        Call DisplayOut(3, 0, "Invalid response")
    
    End If

End Sub

Private Sub bGetPPS_Click()

Dim tmpStr As String
Dim indx As Integer

    Call ClearBuffers
    SendBuff(0) = &H24
    SendBuff(1) = &H0
    SendLen = 2
    RecvLen = 7
    retCode = CallCardControl
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    tmpStr = ""
    For indx = 0 To 4
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2)
    
    Next indx
    
    If tmpStr = "E100000002" Then
    
        'interpret response data
        Select Case RecvBuff(5)
        
            Case 0
                rbMax1.Value = True
                
            Case 1
                rbMax2.Value = True
                
            Case 2
                rbMax3.Value = True
                
            Case 3
                rbMax4.Value = True
            
            Case Else
                rbMax5.Value = True
        
        End Select
        
        Select Case RecvBuff(6)
        
            Case 0
                rbCurr1.Value = True
                
            Case 1
                rbCurr2.Value = True
                
            Case 2
                rbCurr3.Value = True
                
            Case 3
                rbCurr4.Value = True
                
            Case Else
                rbCurr5.Value = True
        
        End Select
        
    Else
    
        rbMax1.Value = False
        rbMax2.Value = False
        rbMax3.Value = False
        rbMax4.Value = False
        rbMax5.Value = False
        rbCurr1.Value = False
        rbCurr2.Value = False
        rbCurr3.Value = False
        rbCurr4.Value = False
        rbCurr5.Value = False
        Call DisplayOut(3, 0, "Invalid response")
        
    End If

End Sub

Private Sub bGetPSet_Click()

Dim tmpStr As String
Dim indx As Integer

    Call ClearBuffers
    SendBuff(0) = &H20
    SendBuff(1) = &H0
    SendBuff(3) = &HFF
    SendLen = 4
    RecvLen = 6
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    tmpStr = ""
    
    For indx = 0 To 4
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2)
    
    Next indx
    
    If tmpStr <> "E100000001" Then
    
        tMsg.Text = "Invalid Card Detected"
        Exit Sub
    
    End If
    
    'interpret status
    Select Case RecvBuff(5)
    
        Case 1
            rbType1.Value = True
            
        Case 2
            rbType2.Value = True
            
        Case Else
            rbType3.Value = True
    
    End Select

End Sub

Private Sub bGetReg_Click()

Dim tmpStr As String
Dim indx As Integer

    If tRegNum = "" Then
    
        tRegNum.SetFocus
        Exit Sub
    
    End If

    Call ClearBuffers
    SendBuff(0) = &H19
    SendBuff(1) = &H1
    SendBuff(2) = CInt("&H" & tRegNum.Text)
    SendLen = 3
    RecvLen = 6
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    tmpStr = ""
    For indx = 0 To 4
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2)
    
    Next indx
    
    If tmpStr = "E100000001" Then
    
        'interpret response data
        tRegVal.Text = Right$("00" & Hex(RecvBuff(5)), 2)
        
    Else
    
        tRegVal.Text = ""
        Call DisplayOut(3, 0, "Invalid response")
    
    End If

End Sub

Private Sub bGetTranSet_Click()

Dim tmpStr As String
Dim indx As Integer
Dim tmpVal As Integer

    Call ClearBuffers
    SendBuff(0) = &H20
    SendBuff(1) = &H1
    SendLen = 2
    RecvLen = 9
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    tmpStr = ""
    
    For indx = 0 To 5
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2)
    
    Next indx
    
    If tmpStr = "E10000000406" Then
    
        'Interpret response data
        tmpVal = RecvBuff(6)
        
        'shift bit to right
        For indx = 1 To 4
            
         tmpVal = tmpVal / 2
         
        Next indx
        
        tFStop.Text = CStr(tmpVal)
        tmpVal = RecvBuff(6) And &HF
        tSetup.Text = CStr(tmpVal)
        
        If (RecvBuff(7) And &H4) <> 0 Then
        
            cbFilter.Value = vbChecked
        
        Else
        
            cbFilter.Value = vbUnchecked
        
        End If
        
        tmpVal = RecvBuff(7) And &H3
        tRecGain.Text = CStr(tmpVal)
        tTxMode.Text = Right$("00" & Hex(RecvBuff(8)), 2)
    
    Else
    
        tFStop.Text = ""
        tSetup.Text = ""
        cbFilter.Value = vbUnchecked
        tRecGain.Text = ""
        tTxMode.Text = ""
        Call DisplayOut(3, 0, "Invalid response")
    
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

  'Look for ACR128 SAM and make it the default reader in the combobox
  For intIndx = 0 To cbReader.ListCount - 1
    
    cbReader.ListIndex = intIndx
    
    If InStr(cbReader.Text, "ACR128U SAM") > 0 Then
        
        Exit For
       
    End If
     
  Next intIndx

End Sub

Private Sub bPoll_Click()

    If autoDet Then
    
        autoDet = False
        bPoll.Caption = "Start Auto &Detection"
        PollTimer.Enabled = False
        tMsg.Text = "Polling Stopped..."
        Exit Sub
    
    End If
    
    tMsg.Text = "Polling Started..."
    autoDet = True
    PollTimer.Enabled = True
    bPoll.Caption = "End Auto &Detection"

End Sub

Private Sub bQuit_Click()

  If ConnActive Then
  
    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    ConnActive = False
    
  End If
  
  retCode = SCardReleaseContext(hContext)
  Unload Me

End Sub

Private Sub bRefIS_Click()

Dim tmpStr As String
Dim indx As Integer

    If ConnActive Then
    
        retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    
    End If
    
    indx = 0
    cbReader.ListIndex = indx
    
    While InStr(cbReader.Text, "ACR128U SAM") = 0
    
        If indx = cbReader.ListCount Then
        
            Call DisplayOut(0, 0, "Cannot find ACR128 SAM reader.")
            ConnActive = False
            Exit Sub
        
        End If
        
        indx = indx + 1
        cbReader.ListIndex = indx
    
    Wend
    
    '1. For SAM Refresh, connect to SAM Interface in direct mode
    If rbRIS1.Value = True Then
    
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
    
        '2. For other interfaces, connect to SAM Interface in direct or shared mode
        retCode = SCardConnect(hContext, _
                        cbReader.Text, _
                        SCARD_SHARE_SHARED Or SCARD_SHARE_DIRECT, _
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
    
    End If
    
    Call ClearBuffers
    SendBuff(0) = &H2D
    SendBuff(1) = &H1
    
    If rbRIS1.Value = True Then
    
        'bit 0
        SendBuff(2) = &H1
    
    End If
    
    If rbRIS2.Value = True Then
    
        'bit 1
        SendBuff(2) = &H2
    
    End If
    
    If rbRIS3.Value = True Then
    
        'bit 2
        SendBuff(2) = &H4
    
    End If
    
    SendLen = 3
    RecvLen = 6
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    For indx = 0 To 4
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2)
    
    Next indx
    
    If tmpStr <> "E100000001" Then
    
        'interpret response data
        Call DisplayOut(3, 0, "Invalid response")
        Exit Sub
    
    End If
    
    '3. For SAM interface, disconnect and connect to SAM Interface in direct or shared mode
    If rbRIS3.Value = True Then
    
        If ConnActive Then
        
            retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
        
        End If
        
        indx = 0
        cbReader.ListIndex = indx
        
        While InStr(cbReader.Text, "ACR128U SAM") = 0
        
            If indx = cbReader.ListCount Then
            
                Call DisplayOut(0, 0, "Cannot find ACR128 SAM reader.")
                ConnActive = False
                Exit Sub
            
            End If
            indx = indx + 1
            cbReader.ListIndex = indx
        
        Wend
        
        retCode = SCardConnect(hContext, _
                        cbReader.Text, _
                        SCARD_SHARE_SHARED Or SCARD_SHARE_DIRECT, _
                        0, _
                        hCard, _
                        Protocol)
        
        If retCode <> SCARD_S_SUCCESS Then
        
            Call DisplayOut(1, retCode, "")
            ConnActive = False
            Exit Sub
        
        End If
    
    Else
    
        Call DisplayOut(0, 0, "Successful connection to " & cbReader.Text)
    
    End If

End Sub

Private Sub bReset_Click()

    If ConnActive Then
    
        retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    
    End If
    
    retCode = SCardReleaseContext(hCard)
    Call InitMenu

End Sub

Private Sub bSetAS_Click()

Dim tmpStr As String
Dim indx As Integer

    Call ReadPollingOption
    If (RecvBuff(5) And &H1) <> 0 Then
    
        Call DisplayOut(0, 0, "Turn off automatic PICC polling in the device before using this function.")
        Exit Sub
    
    End If
    
    Call ClearBuffers
    SendBuff(0) = &H25
    SendBuff(1) = &H1
    
    If rbAntOff.Value = True Then
    
        SendBuff(2) = &H0
    
    Else
    
        rbAntOff.SetFocus
        Exit Sub
    
    End If
    
    SendLen = 3
    RecvLen = 6
    retCode = CallCardControl
     
    If retCode <> SCARD_S_SUCCESS Then
     
        Exit Sub
     
    End If
    
    tmpStr = ""
    
    For indx = 0 To 4
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2)
    
    Next indx
    
    If tmpStr <> "E100000001" Then
    
        Call DisplayOut(3, 0, "Invalid response")
    
    End If

End Sub

Private Sub bSetEH_Click()

Dim tmpStr As String
Dim indx As Integer
Dim tmpVal As Integer

    If tPc2Pi.Text = "" Then
    
        tPc2Pi.SetFocus
        Exit Sub
    
    End If
    
    If CInt(tPc2Pi.Text) > 3 Then
    
        tPc2Pi.Text = "3"
        tPc2Pi.SetFocus
        Exit Sub
    
    End If
    
    If tPi2Pc.Text = "" Then
    
        tPi2Pc.SetFocus
        Exit Sub
    
    End If
    
    If CInt(tPi2Pc.Text) > 3 Then
    
        tPi2Pc.Text = "3"
        tPi2Pc.SetFocus
        Exit Sub
    
    End If
    
    Call ClearBuffers
    SendBuff(0) = &H2C
    SendBuff(1) = &H2
    
    'shift bit to left
    tmpVal = CInt(tPc2Pi.Text)
    For indx = 1 To 4
    
        tmpVal = tmpVal * 2
    
    Next indx
    
    SendBuff(2) = tmpVal
    SendBuff(2) = SendBuff(2) + (tPi2Pc.Text)
    SendBuff(3) = &H7F
    SendLen = 4
    RecvLen = 7
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    tmpStr = ""
    
    For indx = 0 To 4
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2)
    
    Next indx
    
    If tmpStr <> "E100000002" Then
    
        Call DisplayOut(3, 0, "Invalid response")
    
    End If

End Sub

Private Sub bSetFWI_Click()

Dim tmpStr As String
Dim indx As Integer

    If tFWI.Text = "" Then
    
        tFWI.SetFocus
        Exit Sub
    
    End If
    
    If tPollTO.Text = "" Then
    
        tPollTO.SetFocus
        Exit Sub
    
    End If
    
    If tFS.Text = "" Then
    
        tFS.SetFocus
        Exit Sub
    
    End If
    
    Call ClearBuffers
    SendBuff(0) = &H1F
    SendBuff(1) = &H3
    SendBuff(2) = "&H" & CStr(tFWI.Text)
    SendBuff(3) = "&H" & CStr(tPollTO.Text)
    SendBuff(4) = "&H" & CStr(tFS.Text)
    SendLen = 5
    RecvLen = 8
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    tmpStr = ""
    
    For indx = 0 To 4
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2)
    
    Next indx
    
    If tmpStr <> "E100000003" Then
    
        Call DisplayOut(3, 0, "Invalid response")
    
    End If

End Sub

Private Sub bSetPICC_Click()

Dim tmpStr As String
Dim indx As Integer

    If tPICC1.Text = "" Then
        
        tPICC1.SetFocus
        Exit Sub
    
    End If
    
    If tPICC2.Text = "" Then
        
        tPICC2.SetFocus
        Exit Sub
    
    End If

    If tPICC3.Text = "" Then
        
        tPICC3.SetFocus
        Exit Sub
    
    End If

    If tPICC4.Text = "" Then
        
        tPICC4.SetFocus
        Exit Sub
    
    End If
    
    If tPICC5.Text = "" Then
        
        tPICC5.SetFocus
        Exit Sub
    
    End If

    If tPICC6.Text = "" Then
        
        tPICC6.SetFocus
        Exit Sub
    
    End If

    If tPICC7.Text = "" Then
        
        tPICC7.SetFocus
        Exit Sub
    
    End If

    If tPICC8.Text = "" Then
        
        tPICC8.SetFocus
        Exit Sub
    
    End If

    If tPICC9.Text = "" Then
        
        tPICC9.SetFocus
        Exit Sub
    
    End If

    If tPICC10.Text = "" Then
        
        tPICC10.SetFocus
        Exit Sub
    
    End If

    If tPICC11.Text = "" Then
        
        tPICC11.SetFocus
        Exit Sub
    
    End If

    If tPICC12.Text = "" Then
        
        tPICC12.SetFocus
        Exit Sub
    
    End If

    Call ClearBuffers
    SendBuff(0) = &H2A
    SendBuff(1) = &HC
    SendBuff(2) = CInt("&H" & tPICC1.Text)
    SendBuff(3) = CInt("&H" & tPICC2.Text)
    SendBuff(4) = CInt("&H" & tPICC3.Text)
    SendBuff(5) = CInt("&H" & tPICC4.Text)
    SendBuff(6) = CInt("&H" & tPICC5.Text)
    SendBuff(7) = CInt("&H" & tPICC6.Text)
    SendBuff(8) = CInt("&H" & tPICC7.Text)
    SendBuff(9) = CInt("&H" & tPICC8.Text)
    SendBuff(10) = CInt("&H" & tPICC9.Text)
    SendBuff(11) = CInt("&H" & tPICC10.Text)
    SendBuff(12) = CInt("&H" & tPICC11.Text)
    SendBuff(13) = CInt("&H" & tPICC12.Text)
    SendLen = 14
    RecvLen = 17
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    For indx = 0 To 4
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2)
    
    Next indx
    
    If tmpStr <> "E10000000C" Then
    
        Call DisplayOut(3, 0, "Invalid response")
    
    End If
    
End Sub

Private Sub bSetPPS_Click()

Dim tmpStr As String
Dim indx As Integer

    If (rbMax1.Value = False And rbMax2.Value = False And rbMax3.Value = False And rbMax4.Value = False And rbMax5.Value = False) Then
        
        rbMax3.Value = True
        
    End If
    
    If (rbCurr1.Value = False And rbCurr2.Value = False And rbCurr3.Value = False And rbCurr4.Value = False And rbCurr5.Value = False) Then
    
        rbCurr3.Value = True
    
    End If
    
    Call ClearBuffers
    SendBuff(0) = &H24
    SendBuff(1) = &H1
    
    If rbMax1.Value = True Then
    
        SendBuff(2) = &H0
    
    End If
    
    If rbMax2.Value = True Then
    
        SendBuff(2) = &H1
    
    End If
    
    If rbMax3.Value = True Then
    
        SendBuff(2) = &H2
    
    End If
    
    If rbMax4.Value = True Then
    
        SendBuff(2) = &H3
    
    End If
    
    If rbMax5.Value = True Then
    
        SendBuff(2) = &HFF
    
    End If
    
    SendLen = 3
    RecvLen = 7
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    tmpStr = ""
    For indx = 0 To 4
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2)
    
    Next indx
    
    If tmpStr <> "E100000002" Then
    
        Call DisplayOut(3, 0, "Invalid response")
    
    End If

End Sub

Private Sub bSetPSet_Click()

    If rbType1.Value = True Then
    
        reqType = 1
        
    Else
    If rbType2.Value = True Then
    
        reqType = 2
    
    Else
    If rbType3.Value = True Then
    
        reqType = 3
        
    Else
    
        rbType1.SetFocus
        Exit Sub
    
    End If
        
    End If
    
    End If
    
    Call ClearBuffers
    SendBuff(0) = &H20
    SendBuff(1) = &H2
    
    Select Case reqType
    
        Case 1
            SendBuff(2) = &H1
            
        Case 2
            SendBuff(2) = &H2
            
        Case Else
            SendBuff(2) = &H3
    
    End Select
    
    SendBuff(3) = &HFF
    SendLen = 4
    RecvLen = 5
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
End Sub

Private Sub bSetReg_Click()

Dim tmpStr As String
Dim indx As Integer

    If tRegNum.Text = "" Then
    
        tRegNum.SetFocus
        Exit Sub
    
    End If
    
    If tRegVal.Text = "" Then
    
        tRegVal.SetFocus
        Exit Sub
    
    End If
    
    Call ClearBuffers
    SendBuff(0) = &H1A
    SendBuff(1) = &H2
    SendBuff(2) = CInt("&H" & tRegNum.Text)
    SendBuff(3) = CInt("&H" & tRegVal.Text)
    SendLen = 4
    RecvLen = 6
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
        
        Exit Sub
        
    End If
    
    tmpStr = ""
    For indx = 0 To 4
    
        tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(indx)), 2)
    
    Next indx
    
    If tmpStr = "E100000001" Then
    
        'interpret response data
        tRegVal.Text = Right$("00" & Hex(RecvBuff(5)), 2)
        
    Else
    
        tRegNum.Text = ""
        tRegVal.Text = ""
        Call DisplayOut(3, 0, "Invalid response")
    
    End If

End Sub

Private Sub bSetTranSet_Click()

Dim tempVal As Integer
Dim indx As Integer

    If tFStop.Text = "" Then
    
        tFStop.SetFocus
        Exit Sub
    
    End If
    
    If tSetup.Text = "" Then
    
        tSetup.SetFocus
        Exit Sub
    
    End If
    
    If tRecGain.Text = "" Then
    
        tRecGain.SetFocus
        Exit Sub
    
    End If
    
    If tTxMode.Text = "" Then
    
        tTxMode.SetFocus
        Exit Sub
    
    End If
    
    If CInt(tFStop.Text) > 15 Then
    
        tFStop.Text = "15"
        tFStop.SetFocus
        Exit Sub
    
    End If
    
    If CInt(tSetup.Text) > 15 Then
    
        tSetup.Text = "15"
        tSetup.SetFocus
        Exit Sub
    
    End If
    
    If CInt(tRecGain.Text) > 3 Then
    
        tRecGain.Text = "3"
        tRecGain.SetFocus
        Exit Sub
    
    End If
    
    Call ClearBuffers
    SendBuff(0) = &H20
    SendBuff(1) = &H4
    SendBuff(2) = &H6
    
    'shift bit to left
    tempVal = CInt(tFStop.Text)
    
    For indx = 1 To 4
    
        tempVal = tempVal * 2
    
    Next indx
    
    SendBuff(3) = tempVal
    SendBuff(3) = SendBuff(3) + CInt(tSetup.Text)
    
    If cbFilter.Value = vbChecked Then
    
        SendBuff(4) = &H4
    
    End If
    
    SendBuff(4) = SendBuff(4) + CInt(tRecGain.Text)
    SendBuff(5) = CInt("&H" & tTxMode.Text)
    SendLen = 6
    RecvLen = 5
    retCode = CallCardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    If RecvBuff(0) <> &HE1 Then
    
        Call DisplayOut(3, 0, "Invalid response")
    
    End If
    
End Sub

Private Sub Form_Load()

    Call InitMenu

End Sub


Private Sub PollTimer_Timer()

Dim indx As Integer
Dim tmpStr As String

    indx = 0
    cbReader.ListIndex = indx
    
    While InStr(cbReader.Text, "ACR128U PICC") = 0
    
        If indx = cbReader.ListCount Then
        
            Call DisplayOut(0, 0, "Cannot find ACR128 PICC reader.")
            PollTimer.Enabled = False
            Exit Sub
        
        End If
        
        indx = indx + 1
        cbReader.ListIndex = indx
    
    Wend
    
    RdrState.RdrName = cbReader.Text
    retCode = SCardGetStatusChange(hContext, _
                                   0, _
                                   RdrState, _
                                   1)
                                   
    If retCode = SCARD_S_SUCCESS Then
    
        If (RdrState.RdrEventState And SCARD_STATE_PRESENT) <> 0 Then
        
            Select Case reqType
            
                Case 1
                    tmpStr = "ISO14443 Type A card"
                    
                Case 2
                    tmpStr = "ISO14443 Type B card"
                    
                Case Else
                    tmpStr = "ISO14443 card"
            
            End Select
            
            tMsg.Text = tmpStr & " is detected"
        
        Else
    
            tMsg.Text = "No card within range"
        
        End If
    
    End If

End Sub

Private Sub tFS_GotFocus()

    tFS.SelStart = 0
    tFS.SelLength = Len(tFS.Text)

End Sub

'user input control

Private Sub tFS_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If

    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tFStop_Change()
    
    If Len(tFStop.Text) = tFWI.MaxLength Then
    
       tSetup.SetFocus
    
    End If
    
End Sub

Private Sub tFStop_GotFocus()

    tFStop.SelStart = 0
    tFStop.SelLength = Len(tFStop.Text)

End Sub

Private Sub tFStop_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tFWI_Change()

    If Len(tFWI.Text) = tFWI.MaxLength Then
    
       tPollTO.SetFocus
    
    End If

End Sub

Private Sub tFWI_GotFocus()

    tFWI.SelStart = 0
    tFWI.SelLength = Len(tFWI.Text)

End Sub

Private Sub tFWI_KeyPress(KeyAscii As Integer)
    
    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub


Private Sub tPc2Pi_Change()

    If Len(tPc2Pi.Text) = tFWI.MaxLength Then
    
       tPi2Pc.SetFocus
    
    End If

End Sub

Private Sub tPc2Pi_GotFocus()

    tPc2Pi.SelStart = 0
    tPc2Pi.SelLength = Len(tPc2Pi.Text)

End Sub

Private Sub tPc2Pi_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tPi2Pc_GotFocus()

    tPi2Pc.SelStart = 0
    tPi2Pc.SelLength = Len(tPi2Pc.Text)

End Sub

Private Sub tPi2Pc_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tPICC1_Change()

    If Len(tPICC1.Text) = tFWI.MaxLength Then
    
       tPICC2.SetFocus
    
    End If

End Sub

Private Sub tPICC1_GotFocus()

    tPICC1.SelStart = 0
    tPICC1.SelLength = Len(tPICC1.Text)

End Sub

Private Sub tPICC10_GotFocus()

    tPICC10.SelStart = 0
    tPICC10.SelLength = Len(tPICC10.Text)

End Sub

Private Sub tPICC11_GotFocus()

    tPICC11.SelStart = 0
    tPICC11.SelLength = Len(tPICC11.Text)

End Sub

Private Sub tPICC12_GotFocus()

    tPICC12.SelStart = 0
    tPICC12.SelLength = Len(tPICC12.Text)

End Sub

Private Sub tPICC2_Change()

    If Len(tPICC2.Text) = tFWI.MaxLength Then
    
       tPICC3.SetFocus
    
    End If

End Sub

Private Sub tPICC2_GotFocus()

    tPICC2.SelStart = 0
    tPICC2.SelLength = Len(tPICC2.Text)

End Sub

Private Sub tPICC3_Change()

    If Len(tPICC3.Text) = tFWI.MaxLength Then
    
       tPICC4.SetFocus
    
    End If

End Sub

Private Sub tPICC3_GotFocus()

    tPICC3.SelStart = 0
    tPICC3.SelLength = Len(tPICC3.Text)

End Sub

Private Sub tPICC4_Change()

    If Len(tPICC4.Text) = tFWI.MaxLength Then
    
       tPICC5.SetFocus
    
    End If

End Sub

Private Sub tPICC4_GotFocus()

    tPICC4.SelStart = 0
    tPICC4.SelLength = Len(tPICC4.Text)

End Sub

Private Sub tPICC5_Change()

    If Len(tPICC5.Text) = tFWI.MaxLength Then
    
       tPICC6.SetFocus
    
    End If

End Sub

Private Sub tPICC5_GotFocus()

    tPICC5.SelStart = 0
    tPICC5.SelLength = Len(tPICC5.Text)

End Sub

Private Sub tPICC6_Change()

    If Len(tPICC6.Text) = tFWI.MaxLength Then
    
       tPICC7.SetFocus
    
    End If

End Sub

Private Sub tPICC6_GotFocus()

    tPICC6.SelStart = 0
    tPICC6.SelLength = Len(tPICC6.Text)

End Sub

Private Sub tPICC7_Change()

    If Len(tPICC7.Text) = tFWI.MaxLength Then
    
       tPICC8.SetFocus
    
    End If

End Sub

Private Sub tPICC7_GotFocus()

    tPICC7.SelStart = 0
    tPICC7.SelLength = Len(tPICC7.Text)

End Sub

Private Sub tPICC8_Change()

    If Len(tPICC8.Text) = tFWI.MaxLength Then
    
       tPICC9.SetFocus
    
    End If

End Sub

Private Sub tPICC8_GotFocus()

    tPICC8.SelStart = 0
    tPICC8.SelLength = Len(tPICC8.Text)

End Sub

Private Sub tPICC9_Change()

    If Len(tPICC9.Text) = tFWI.MaxLength Then
    
       tPICC10.SetFocus
    
    End If

End Sub

Private Sub tPICC10_Change()

    If Len(tPICC10.Text) = tFWI.MaxLength Then
    
       tPICC11.SetFocus
    
    End If

End Sub

Private Sub tPICC11_Change()

    If Len(tPICC11.Text) = tFWI.MaxLength Then
    
       tPICC12.SetFocus
    
    End If

End Sub

Private Sub tPICC1_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If

    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tPICC10_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If

    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tPICC11_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If

    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tPICC12_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If

    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tPICC2_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If

    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tPICC3_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If

    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If
    
End Sub

Private Sub tPICC4_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If

    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tPICC5_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If

    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tPICC6_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If

    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tPICC7_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If

    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tPICC8_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If

    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tPICC9_GotFocus()

    tPICC9.SelStart = 0
    tPICC9.SelLength = Len(tPICC9.Text)

End Sub

Private Sub tPICC9_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If

    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tPollTO_Change()

    If Len(tPollTO.Text) = tFWI.MaxLength Then
    
       tFS.SetFocus
    
    End If

End Sub

Private Sub tPollTO_GotFocus()

    tPollTO.SelStart = 0
    tPollTO.SelLength = Len(tPollTO.Text)

End Sub

Private Sub tPollTO_KeyPress(KeyAscii As Integer)
 
    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tRecGain_Change()

    If Len(tRecGain.Text) = tFWI.MaxLength Then
    
       tTxMode.SetFocus
    
    End If

End Sub

Private Sub tRecGain_GotFocus()

    tRecGain.SelStart = 0
    tRecGain.SelLength = Len(tRecGain.Text)

End Sub

Private Sub tRecGain_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tRegNum_Change()

    If Len(tRegNum.Text) = tFWI.MaxLength Then
    
       tRegVal.SetFocus
    
    End If


End Sub

Private Sub tRegNum_GotFocus()

    tRegNum.SelStart = 0
    tRegNum.SelLength = Len(tRegNum.Text)

End Sub

Private Sub tRegNum_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If

    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tRegVal_GotFocus()

    tRegVal.SelStart = 0
    tRegVal.SelLength = Len(tRegVal.Text)

End Sub

Private Sub tRegVal_KeyPress(KeyAscii As Integer)

     If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If

    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tSetup_Change()

    If Len(tSetup.Text) = tFWI.MaxLength Then
    
       tRecGain.SetFocus
    
    End If

End Sub

Private Sub tSetup_GotFocus()

    tSetup.SelStart = 0
    tSetup.SelLength = Len(tSetup.Text)

End Sub

Private Sub tSetup_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tTxMode_GotFocus()

    tTxMode.SelStart = 0
    tTxMode.SelLength = Len(tTxMode.Text)

End Sub

Private Sub tTxMode_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If

    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub
