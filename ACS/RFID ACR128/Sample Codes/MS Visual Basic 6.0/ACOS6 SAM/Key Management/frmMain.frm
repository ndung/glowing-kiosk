VERSION 5.00
Object = "{BDC217C8-ED16-11CD-956C-0000C04E4C0A}#1.1#0"; "TABCTL32.OCX"
Begin VB.Form frm_main 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Key Management Sample"
   ClientHeight    =   7875
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   11430
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "frmMain.frx":0000
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   7875
   ScaleWidth      =   11430
   StartUpPosition =   2  'CenterScreen
   Begin VB.ListBox lst_Log 
      Appearance      =   0  'Flat
      Height          =   7635
      Left            =   5805
      TabIndex        =   13
      Top             =   135
      Width           =   5535
   End
   Begin TabDlg.SSTab SSTab 
      Height          =   7635
      Left            =   105
      TabIndex        =   0
      Top             =   135
      Width           =   5595
      _ExtentX        =   9869
      _ExtentY        =   13467
      _Version        =   393216
      Tabs            =   2
      TabsPerRow      =   2
      TabHeight       =   520
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      TabCaption(0)   =   "SAM Initialization"
      TabPicture(0)   =   "frmMain.frx":17A2
      Tab(0).ControlEnabled=   -1  'True
      Tab(0).Control(0)=   "Frame1"
      Tab(0).Control(0).Enabled=   0   'False
      Tab(0).ControlCount=   1
      TabCaption(1)   =   "ACOS Card Initialization"
      TabPicture(1)   =   "frmMain.frx":17BE
      Tab(1).ControlEnabled=   0   'False
      Tab(1).Control(0)=   "Frame2"
      Tab(1).Control(0).Enabled=   0   'False
      Tab(1).ControlCount=   1
      Begin VB.Frame Frame2 
         Caption         =   "Generated Keys"
         Height          =   7065
         Left            =   -74790
         TabIndex        =   31
         Top             =   390
         Width           =   5190
         Begin VB.TextBox txt_PIN 
            Appearance      =   0  'Flat
            Height          =   285
            Left            =   1800
            MaxLength       =   16
            TabIndex        =   17
            Top             =   1875
            Width           =   1650
         End
         Begin VB.OptionButton opt_3DES 
            Caption         =   "3 DES"
            Enabled         =   0   'False
            Height          =   195
            Left            =   3255
            TabIndex        =   19
            Top             =   2415
            Width           =   1620
         End
         Begin VB.OptionButton opt_DES 
            Caption         =   "DES"
            Enabled         =   0   'False
            Height          =   195
            Left            =   1785
            TabIndex        =   18
            Top             =   2415
            Width           =   1080
         End
         Begin VB.CommandButton cmd_GenerateKey 
            Caption         =   "Generate Keys"
            Enabled         =   0   'False
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   375
            Left            =   1785
            TabIndex        =   20
            Top             =   6480
            Width           =   1440
         End
         Begin VB.CommandButton cmd_ListReaders_SLT 
            Caption         =   "List Readers"
            Height          =   375
            Left            =   3555
            TabIndex        =   14
            Top             =   330
            Width           =   1440
         End
         Begin VB.ComboBox cmbSLT 
            Height          =   315
            Left            =   360
            Style           =   2  'Dropdown List
            TabIndex        =   15
            Top             =   360
            Width           =   3000
         End
         Begin VB.CommandButton cmd_Connect_SLT 
            Caption         =   "Connect"
            Height          =   375
            Left            =   3555
            TabIndex        =   16
            Top             =   855
            Width           =   1440
         End
         Begin VB.CommandButton cmd_SaveKeys 
            Caption         =   "Save Keys"
            Enabled         =   0   'False
            Height          =   375
            Left            =   3405
            TabIndex        =   21
            Top             =   6480
            Width           =   1440
         End
         Begin VB.Label lbl_r_Krd 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BorderStyle     =   1  'Fixed Single
            ForeColor       =   &H00000000&
            Height          =   285
            Left            =   3435
            TabIndex        =   55
            Top             =   5955
            Width           =   1575
         End
         Begin VB.Label lbl_r_Kcf 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BorderStyle     =   1  'Fixed Single
            ForeColor       =   &H00000000&
            Height          =   285
            Left            =   3435
            TabIndex        =   54
            Top             =   5430
            Width           =   1575
         End
         Begin VB.Label lbl_r_Kcr 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BorderStyle     =   1  'Fixed Single
            ForeColor       =   &H00000000&
            Height          =   285
            Left            =   3435
            TabIndex        =   53
            Top             =   4905
            Width           =   1575
         End
         Begin VB.Label lbl_r_Kd 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BorderStyle     =   1  'Fixed Single
            ForeColor       =   &H00000000&
            Height          =   285
            Left            =   3435
            TabIndex        =   52
            Top             =   4380
            Width           =   1575
         End
         Begin VB.Label Label2 
            Caption         =   "ACOS Card PIN"
            Height          =   285
            Left            =   345
            TabIndex        =   50
            Top             =   1920
            Width           =   3150
         End
         Begin VB.Label lbl_r_Kt 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BorderStyle     =   1  'Fixed Single
            ForeColor       =   &H00000000&
            Height          =   285
            Left            =   3435
            TabIndex        =   49
            Top             =   3855
            Width           =   1575
         End
         Begin VB.Label lbl_r_Kc 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BorderStyle     =   1  'Fixed Single
            ForeColor       =   &H00000000&
            Height          =   285
            Left            =   3435
            TabIndex        =   48
            Top             =   3330
            Width           =   1575
         End
         Begin VB.Label lbl_Krd 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BorderStyle     =   1  'Fixed Single
            ForeColor       =   &H00000000&
            Height          =   285
            Left            =   1800
            TabIndex        =   47
            Top             =   5955
            Width           =   1575
         End
         Begin VB.Label lbl_Kcf 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BorderStyle     =   1  'Fixed Single
            ForeColor       =   &H00000000&
            Height          =   285
            Left            =   1800
            TabIndex        =   46
            Top             =   5430
            Width           =   1575
         End
         Begin VB.Label lbl_Kcr 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BorderStyle     =   1  'Fixed Single
            ForeColor       =   &H00000000&
            Height          =   285
            Left            =   1800
            TabIndex        =   45
            Top             =   4905
            Width           =   1575
         End
         Begin VB.Label lbl_Kd 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BorderStyle     =   1  'Fixed Single
            ForeColor       =   &H00000000&
            Height          =   285
            Left            =   1800
            TabIndex        =   44
            Top             =   4380
            Width           =   1575
         End
         Begin VB.Label lbl_Kt 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BorderStyle     =   1  'Fixed Single
            ForeColor       =   &H00000000&
            Height          =   285
            Left            =   1800
            TabIndex        =   43
            Top             =   3855
            Width           =   1575
         End
         Begin VB.Label lbl_Kc 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BorderStyle     =   1  'Fixed Single
            ForeColor       =   &H00000000&
            Height          =   285
            Left            =   1800
            TabIndex        =   42
            Top             =   3330
            Width           =   1575
         End
         Begin VB.Label lbl_IC 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BorderStyle     =   1  'Fixed Single
            ForeColor       =   &H00000000&
            Height          =   285
            Left            =   1800
            TabIndex        =   41
            Top             =   2820
            Width           =   3210
         End
         Begin VB.Label Label9 
            Caption         =   "Issuer Code (IC)"
            Height          =   285
            Left            =   330
            TabIndex        =   40
            Top             =   2865
            Width           =   3150
         End
         Begin VB.Label Label11 
            Caption         =   "Card Key (Kc)"
            Height          =   285
            Left            =   330
            TabIndex        =   39
            Top             =   3375
            Width           =   3150
         End
         Begin VB.Label Label12 
            Caption         =   "Terminal Key (Kt)"
            Height          =   285
            Left            =   330
            TabIndex        =   38
            Top             =   3900
            Width           =   3150
         End
         Begin VB.Label Label13 
            Caption         =   "Debit Key (Kd)"
            Height          =   285
            Left            =   330
            TabIndex        =   37
            Top             =   4425
            Width           =   3150
         End
         Begin VB.Label Label14 
            Caption         =   "Credit Key (Kcr)"
            Height          =   285
            Left            =   330
            TabIndex        =   36
            Top             =   4950
            Width           =   3150
         End
         Begin VB.Label Label15 
            Caption         =   "Certify Key (Kcf)"
            Height          =   285
            Left            =   330
            TabIndex        =   35
            Top             =   5475
            Width           =   3150
         End
         Begin VB.Label Label16 
            Caption         =   "Revoke Debit Key (Krd)"
            Height          =   390
            Left            =   330
            TabIndex        =   34
            Top             =   5940
            Width           =   1320
            WordWrap        =   -1  'True
         End
         Begin VB.Label lbl_CardSN 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BorderStyle     =   1  'Fixed Single
            ForeColor       =   &H00000000&
            Height          =   285
            Left            =   1800
            TabIndex        =   32
            Top             =   1410
            Width           =   3210
         End
         Begin VB.Label Label17 
            Caption         =   "Card Serial Number"
            Height          =   285
            Left            =   345
            TabIndex        =   33
            Top             =   1500
            Width           =   3150
         End
      End
      Begin VB.Frame Frame1 
         Height          =   7065
         Left            =   210
         TabIndex        =   22
         Top             =   390
         Width           =   5190
         Begin VB.TextBox txtSAMGPIN 
            Appearance      =   0  'Flat
            Enabled         =   0   'False
            Height          =   285
            Left            =   1800
            MaxLength       =   16
            TabIndex        =   4
            Top             =   1620
            Width           =   1650
         End
         Begin VB.CommandButton cmd_InitSAM 
            Caption         =   "Initialize SAM"
            Enabled         =   0   'False
            BeginProperty Font 
               Name            =   "MS Sans Serif"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   375
            Left            =   3390
            TabIndex        =   12
            Top             =   6480
            Width           =   1440
         End
         Begin VB.CommandButton cmd_ListReaders_SAM 
            Caption         =   "List Readers"
            Height          =   375
            Left            =   3555
            TabIndex        =   1
            Top             =   405
            Width           =   1440
         End
         Begin VB.ComboBox cmbSAM 
            Height          =   315
            Left            =   360
            Style           =   2  'Dropdown List
            TabIndex        =   2
            Top             =   435
            Width           =   3000
         End
         Begin VB.CommandButton cmd_Connect_SAM 
            Caption         =   "Connect"
            Enabled         =   0   'False
            Height          =   375
            Left            =   3555
            TabIndex        =   3
            Top             =   930
            Width           =   1440
         End
         Begin VB.TextBox txt_IC 
            Appearance      =   0  'Flat
            Enabled         =   0   'False
            Height          =   285
            Left            =   1800
            MaxLength       =   32
            TabIndex        =   5
            Top             =   2655
            Width           =   3045
         End
         Begin VB.TextBox txtKc 
            Appearance      =   0  'Flat
            Enabled         =   0   'False
            Height          =   285
            Left            =   1800
            MaxLength       =   32
            TabIndex        =   6
            Top             =   3225
            Width           =   3045
         End
         Begin VB.TextBox txtKt 
            Appearance      =   0  'Flat
            Enabled         =   0   'False
            Height          =   285
            Left            =   1800
            MaxLength       =   32
            TabIndex        =   7
            Top             =   3750
            Width           =   3045
         End
         Begin VB.TextBox txtKd 
            Appearance      =   0  'Flat
            Enabled         =   0   'False
            Height          =   285
            Left            =   1800
            MaxLength       =   32
            TabIndex        =   8
            Top             =   4275
            Width           =   3045
         End
         Begin VB.TextBox txtKcr 
            Appearance      =   0  'Flat
            Enabled         =   0   'False
            Height          =   285
            Left            =   1800
            MaxLength       =   32
            TabIndex        =   9
            Top             =   4800
            Width           =   3045
         End
         Begin VB.TextBox txtKcf 
            Appearance      =   0  'Flat
            Enabled         =   0   'False
            Height          =   285
            Left            =   1800
            MaxLength       =   32
            TabIndex        =   10
            Top             =   5325
            Width           =   3045
         End
         Begin VB.TextBox txtKrd 
            Appearance      =   0  'Flat
            Enabled         =   0   'False
            Height          =   285
            Left            =   1800
            MaxLength       =   32
            TabIndex        =   11
            Top             =   5850
            Width           =   3045
         End
         Begin VB.Label Label10 
            Caption         =   " SAM Master Keys"
            Height          =   195
            Left            =   1770
            TabIndex        =   51
            Top             =   2190
            Width           =   1350
         End
         Begin VB.Line Line1 
            X1              =   195
            X2              =   4995
            Y1              =   2280
            Y2              =   2280
         End
         Begin VB.Label Label18 
            Caption         =   "SAM GLOBAL PIN"
            Height          =   285
            Left            =   330
            TabIndex        =   30
            Top             =   1665
            Width           =   3150
         End
         Begin VB.Label Label1 
            Caption         =   "Issuer Code (IC)"
            Height          =   285
            Left            =   330
            TabIndex        =   29
            Top             =   2700
            Width           =   3150
         End
         Begin VB.Label Label3 
            Caption         =   "Card Key (Kc)"
            Height          =   285
            Left            =   330
            TabIndex        =   28
            Top             =   3270
            Width           =   3150
         End
         Begin VB.Label Label4 
            Caption         =   "Terminal Key (Kt)"
            Height          =   285
            Left            =   330
            TabIndex        =   27
            Top             =   3795
            Width           =   3150
         End
         Begin VB.Label Label5 
            Caption         =   "Debit Key (Kd)"
            Height          =   285
            Left            =   330
            TabIndex        =   26
            Top             =   4320
            Width           =   3150
         End
         Begin VB.Label Label6 
            Caption         =   "Credit Key (Kcr)"
            Height          =   285
            Left            =   330
            TabIndex        =   25
            Top             =   4845
            Width           =   3150
         End
         Begin VB.Label Label7 
            Caption         =   "Certify Key (Kcf)"
            Height          =   285
            Left            =   330
            TabIndex        =   24
            Top             =   5370
            Width           =   3150
         End
         Begin VB.Label Label8 
            Caption         =   "Revoke Debit Key (Krd)"
            Height          =   390
            Left            =   330
            TabIndex        =   23
            Top             =   5835
            Width           =   1320
            WordWrap        =   -1  'True
         End
      End
   End
End
Attribute VB_Name = "frm_main"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'===================================================================================================
'
'   Project Name :  KeyManagement
'
'   Company      :  Advanced Card Systems LTD.
'
'   Author       :  Richard C. Siman
'
'   Date         :  January 23, 2007
'
'   Description  :  This sample shows how to initialize an ACOS SAM and use it to generate keys
'                   for ACOS card, based from the card serial number.
'
'   Initial Step :  1.  Press List Readers (SAM Initialization).
'                   2.  Choose the SAM reader where you insert your SAM card.
'                   3.  Press Connect.
'                   4.  Enter 8 bytes global PIN (hex format).
'                   5.  If you haven't initialize the SAM card yet, select algorithm to use (DES/3DES).
'                          otherwise proceed to step 8 to initialize keys of new ACOS card.
'                   6.  Enter 16 bytes Master Key to be use for key generation of
'                          each type of ACOS Keys (e.g IC, PIN, Card Key....).
'                   7.  Press Initialize SAM button.
'                   8.  Press ACOS Card Initialization Tab.
'                   9.  Press List Readers (ACOS Card Initialization).
'                   10. Choose the slot reader where you insert your ACOS card.
'                   11. Press Connect.
'                   12. Select Algorithm Reference to use (DES/3DES)
'                   13. Press Generate Keys.
'                   14. Press Save Keys.
'
'   NOTE:
'                   Please note that once the ACOS card was initialized or the IC was changed
'                   from it's default key (0x41 0x43 0x4F 0x53 0x54 0x45 0x53 0x54) it is not possible to
'                   save keys unless you change it's IC key back to default value.
'
'   Revision     :
'
'               Name                    Date            Brief Description of Revision
'
'=====================================================================================================


Option Explicit
Dim G_retCode, G_hContext, G_hCard, G_hCardSAM, G_Protocol As Long
Dim G_DevName, G_DevNameSAM As String
Dim G_ioRequest As SCARD_IO_REQUEST
Dim G_AlgoRef, G_TLV_LEN As Byte



Private Sub cmd_Connect_SAM_Click()
    
    'Establish SAM Reader Connection
    
    'Get Reader Name
    G_DevNameSAM = cmbSAM.List(cmbSAM.ListIndex)
    
    'Disconnect
    G_retCode = SCardDisconnect(G_hCardSAM, SCARD_UNPOWER_CARD)

    'Connect
    G_retCode = SCardConnect(G_hContext, _
                        G_DevNameSAM, _
                        SCARD_SHARE_EXCLUSIVE, _
                        SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1, _
                        G_hCardSAM, _
                        G_Protocol)


    If G_retCode <> SCARD_S_SUCCESS Then
        lst_Log.AddItem "SAM SCardConnect Error: " & GetScardErrMsg(G_retCode)
        lst_Log.ListIndex = lst_Log.NewIndex
        Exit Sub
    Else
        lst_Log.AddItem "SAM SCardConnect Success"
        lst_Log.ListIndex = lst_Log.NewIndex
    End If
    
    'Enable PIN and Master Keys Inputbox
    txtSAMGPIN.Enabled = True
    
    opt_DES.Enabled = True
    opt_3DES.Enabled = True
    
    txt_IC.Enabled = True
    txtKc.Enabled = True
    txtKcf.Enabled = True
    txtKcr.Enabled = True
    txtKd.Enabled = True
    txtKrd.Enabled = True
    txtKt.Enabled = True
    
    cmd_InitSAM.Enabled = True
    
End Sub


Private Sub cmd_Connect_SLT_Click()
    
    'Establish Reader Connection
    
    'Get Reader Name
    G_DevName = cmbSLT.List(cmbSLT.ListIndex)
    
    'Disconnect
    G_retCode = SCardDisconnect(G_hCard, SCARD_UNPOWER_CARD)

    'Connect
    G_retCode = SCardConnect(G_hContext, _
                        G_DevName, _
                        SCARD_SHARE_EXCLUSIVE, _
                        SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1, _
                        G_hCard, _
                        G_Protocol)


    If G_retCode <> SCARD_S_SUCCESS Then
        lst_Log.AddItem "SCardConnect Error: " & GetScardErrMsg(G_retCode)
        lst_Log.ListIndex = lst_Log.NewIndex
        Exit Sub
    Else
        lst_Log.AddItem "SCardConnect Success"
        lst_Log.ListIndex = lst_Log.NewIndex
    End If
    
    'Enable buttons
    cmd_GenerateKey.Enabled = True
    cmd_SaveKeys.Enabled = True
    

End Sub

Private Sub cmd_GenerateKey_Click()
Dim SendBuff() As Byte
Dim RecvBuff() As Byte
Dim RecvLen As Long
Dim SN, GeneratedKey As String
Dim i As Integer
    
    
    'Select Issuer DF
    ReDim SendBuff(0 To 6) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H0
    SendBuff(1) = &HA4
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H2
    SendBuff(5) = &H11
    SendBuff(6) = &H0
    
    lst_Log.AddItem "SAM < 00 A4 00 00 02"
    lst_Log.AddItem "    <<11 00"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SAM(SendBuff, 7, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "612D" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If

    'Submit Issuer PIN
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H0
    SendBuff(1) = &H20
    SendBuff(2) = &H0
    SendBuff(3) = &H1
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(txtSAMGPIN, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(txtSAMGPIN, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(txtSAMGPIN, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(txtSAMGPIN, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(txtSAMGPIN, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(txtSAMGPIN, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(txtSAMGPIN, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(txtSAMGPIN, 15, 2))


    lst_Log.AddItem "SAM < 00 20 00 01 08"
    lst_Log.AddItem "    <<" & Mid(txtSAMGPIN, 1, 2) _
                             & Mid(txtSAMGPIN, 3, 2) _
                             & Mid(txtSAMGPIN, 5, 2) _
                             & Mid(txtSAMGPIN, 7, 2) _
                             & Mid(txtSAMGPIN, 9, 2) _
                             & Mid(txtSAMGPIN, 11, 2) _
                             & Mid(txtSAMGPIN, 13, 2) _
                             & Mid(txtSAMGPIN, 15, 2)
    
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If


    'Get Card Serial Number
    'Select FF00
    ReDim SendBuff(0 To 6) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HA4
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H2
    SendBuff(5) = &HFF
    SendBuff(6) = &H0
    
    
    lst_Log.AddItem "MCU < 80 A4 00 00 02"
    lst_Log.AddItem "    <<FF 00"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 7, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If

    'Read FF 00 to retrieve card serial number
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 9) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HB2
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    
    lst_Log.AddItem "MCU < 80 B2 00 00 08"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 10
    
    If Send_APDU_SLT(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve Card Serial Number
                SN = ""
    
                For i = 0 To 7
                    SN = SN & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                lst_Log.AddItem "MCU >> " & SN
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                lbl_CardSN.Caption = Replace(SN, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    'Generate Key
    'Generate IC Using 1st SAM Master Key (KeyID=81)
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H88
    SendBuff(2) = &H0
    SendBuff(3) = &H81 'KeyID
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(lbl_CardSN.Caption, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(lbl_CardSN.Caption, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(lbl_CardSN.Caption, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(lbl_CardSN.Caption, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(lbl_CardSN.Caption, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(lbl_CardSN.Caption, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(lbl_CardSN.Caption, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(lbl_CardSN.Caption, 15, 2))

    lst_Log.AddItem "SAM < 80 88 00 81 08"
    lst_Log.AddItem "    <<" & Mid(lbl_CardSN.Caption, 1, 2) _
                             & Mid(lbl_CardSN.Caption, 3, 2) _
                             & Mid(lbl_CardSN.Caption, 5, 2) _
                             & Mid(lbl_CardSN.Caption, 7, 2) _
                             & Mid(lbl_CardSN.Caption, 9, 2) _
                             & Mid(lbl_CardSN.Caption, 11, 2) _
                             & Mid(lbl_CardSN.Caption, 13, 2) _
                             & Mid(lbl_CardSN.Caption, 15, 2)
    lst_Log.ListIndex = lst_Log.NewIndex

    RecvLen = 2

    If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    'Get Response to Retrieve Generated Key
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 9) As Byte

    SendBuff(0) = &H0
    SendBuff(1) = &HC0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    
    lst_Log.AddItem "SAM < 00 C0 00 00 08"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 10
    
    If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve Generated Key
                GeneratedKey = ""
    
                For i = 0 To 7
                    GeneratedKey = GeneratedKey & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                lst_Log.AddItem "SAM >> " & GeneratedKey
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                lbl_IC.Caption = Replace(GeneratedKey, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    'Generate Card Key (Kc) Using 2nd SAM Master Key (KeyID=82)
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H88
    SendBuff(2) = &H0
    SendBuff(3) = &H82 'KeyID
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(lbl_CardSN.Caption, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(lbl_CardSN.Caption, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(lbl_CardSN.Caption, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(lbl_CardSN.Caption, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(lbl_CardSN.Caption, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(lbl_CardSN.Caption, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(lbl_CardSN.Caption, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(lbl_CardSN.Caption, 15, 2))

    lst_Log.AddItem "SAM < 80 88 00 82 08"
    lst_Log.AddItem "    <<" & Mid(lbl_CardSN.Caption, 1, 2) _
                             & Mid(lbl_CardSN.Caption, 3, 2) _
                             & Mid(lbl_CardSN.Caption, 5, 2) _
                             & Mid(lbl_CardSN.Caption, 7, 2) _
                             & Mid(lbl_CardSN.Caption, 9, 2) _
                             & Mid(lbl_CardSN.Caption, 11, 2) _
                             & Mid(lbl_CardSN.Caption, 13, 2) _
                             & Mid(lbl_CardSN.Caption, 15, 2)
    lst_Log.ListIndex = lst_Log.NewIndex

    RecvLen = 2

    If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    'Get Response to Retrieve Generated Key
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 9) As Byte

    SendBuff(0) = &H0
    SendBuff(1) = &HC0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    
    lst_Log.AddItem "SAM < 00 C0 00 00 08"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 10
    
    If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve Generated Key
                GeneratedKey = ""
    
                For i = 0 To 7
                    GeneratedKey = GeneratedKey & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                lst_Log.AddItem "SAM >> " & GeneratedKey
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                lbl_Kc.Caption = Replace(GeneratedKey, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    
    
    'If Algorithm Reference = 3DES then Generate Right Half of Card Key (Kc) Using 2nd SAM Master Key (KeyID=82)
    If G_AlgoRef = 0 Then
        
        ReDim SendBuff(0 To 12) As Byte
        ReDim RecvBuff(0 To 1) As Byte
    
        
    
        SendBuff(0) = &H80
        SendBuff(1) = &H88
        SendBuff(2) = &H0
        SendBuff(3) = &H82 'KeyID
        SendBuff(4) = &H8
        
        'compliment the card serial number to generate right half key for 3DES algorithm
        SendBuff(5) = (CInt("&H" & Mid(lbl_CardSN.Caption, 1, 2)) Xor &HFF)
        SendBuff(6) = (CInt("&H" & Mid(lbl_CardSN.Caption, 3, 2)) Xor &HFF)
        SendBuff(7) = (CInt("&H" & Mid(lbl_CardSN.Caption, 5, 2)) Xor &HFF)
        SendBuff(8) = (CInt("&H" & Mid(lbl_CardSN.Caption, 7, 2)) Xor &HFF)
        SendBuff(9) = (CInt("&H" & Mid(lbl_CardSN.Caption, 9, 2)) Xor &HFF)
        SendBuff(10) = (CInt("&H" & Mid(lbl_CardSN.Caption, 11, 2)) Xor &HFF)
        SendBuff(11) = (CInt("&H" & Mid(lbl_CardSN.Caption, 13, 2)) Xor &HFF)
        SendBuff(12) = (CInt("&H" & Mid(lbl_CardSN.Caption, 15, 2)) Xor &HFF)
    
        lst_Log.AddItem "SAM < 80 88 00 82 08"
        lst_Log.AddItem "    <<" & Right("00" & Hex(SendBuff(5)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(6)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(7)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(8)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(9)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(10)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(11)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(12)), 2)
        lst_Log.ListIndex = lst_Log.NewIndex
    
        RecvLen = 2
    
        If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
               If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
               End If
        Else
               Exit Sub
        End If
        
        
        'Get Response to Retrieve Generated Key
        ReDim SendBuff(0 To 4) As Byte
        ReDim RecvBuff(0 To 9) As Byte
    
        SendBuff(0) = &H0
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        
        lst_Log.AddItem "SAM < 00 C0 00 00 08"
        lst_Log.ListIndex = lst_Log.NewIndex
        
        RecvLen = 10
        
        If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then
        
               If Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    'Retrieve Generated Key
                    GeneratedKey = ""
        
                    For i = 0 To 7
                        GeneratedKey = GeneratedKey & Right("00" & Hex(RecvBuff(i)), 2) & " "
                    Next i
                    lst_Log.AddItem "SAM >> " & GeneratedKey
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    
                    lbl_r_Kc.Caption = Replace(GeneratedKey, " ", "")
                    
               End If
               
        Else
               Exit Sub
        End If
        
    Else
    
        lbl_r_Kc.Caption = ""
    
    End If
    
    
    
    'Generate Terminal Key (Kt) Using 3rd SAM Master Key (KeyID=83)
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H88
    SendBuff(2) = &H0
    SendBuff(3) = &H83 'KeyID
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(lbl_CardSN.Caption, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(lbl_CardSN.Caption, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(lbl_CardSN.Caption, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(lbl_CardSN.Caption, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(lbl_CardSN.Caption, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(lbl_CardSN.Caption, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(lbl_CardSN.Caption, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(lbl_CardSN.Caption, 15, 2))

    lst_Log.AddItem "SAM < 80 88 00 83 08"
    lst_Log.AddItem "    <<" & Mid(lbl_CardSN.Caption, 1, 2) _
                             & Mid(lbl_CardSN.Caption, 3, 2) _
                             & Mid(lbl_CardSN.Caption, 5, 2) _
                             & Mid(lbl_CardSN.Caption, 7, 2) _
                             & Mid(lbl_CardSN.Caption, 9, 2) _
                             & Mid(lbl_CardSN.Caption, 11, 2) _
                             & Mid(lbl_CardSN.Caption, 13, 2) _
                             & Mid(lbl_CardSN.Caption, 15, 2)
    lst_Log.ListIndex = lst_Log.NewIndex

    RecvLen = 2

    If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    'Get Response to Retrieve Generated Key
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 9) As Byte

    SendBuff(0) = &H0
    SendBuff(1) = &HC0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    
    lst_Log.AddItem "SAM < 00 C0 00 00 08"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 10
    
    If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve Generated Key
                GeneratedKey = ""
    
                For i = 0 To 7
                    GeneratedKey = GeneratedKey & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                lst_Log.AddItem "SAM >> " & GeneratedKey
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                lbl_Kt.Caption = Replace(GeneratedKey, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    
    
    'If Algorithm Reference = 3DES then Generate Right Half of Terminal Key (Kt) Using 3rd SAM Master Key (KeyID=83)
    If G_AlgoRef = 0 Then
        
        ReDim SendBuff(0 To 12) As Byte
        ReDim RecvBuff(0 To 1) As Byte
    
        
    
        SendBuff(0) = &H80
        SendBuff(1) = &H88
        SendBuff(2) = &H0
        SendBuff(3) = &H83 'KeyID
        SendBuff(4) = &H8
        
        'compliment the card serial number to generate right half key for 3DES algorithm
        SendBuff(5) = (CInt("&H" & Mid(lbl_CardSN.Caption, 1, 2)) Xor &HFF)
        SendBuff(6) = (CInt("&H" & Mid(lbl_CardSN.Caption, 3, 2)) Xor &HFF)
        SendBuff(7) = (CInt("&H" & Mid(lbl_CardSN.Caption, 5, 2)) Xor &HFF)
        SendBuff(8) = (CInt("&H" & Mid(lbl_CardSN.Caption, 7, 2)) Xor &HFF)
        SendBuff(9) = (CInt("&H" & Mid(lbl_CardSN.Caption, 9, 2)) Xor &HFF)
        SendBuff(10) = (CInt("&H" & Mid(lbl_CardSN.Caption, 11, 2)) Xor &HFF)
        SendBuff(11) = (CInt("&H" & Mid(lbl_CardSN.Caption, 13, 2)) Xor &HFF)
        SendBuff(12) = (CInt("&H" & Mid(lbl_CardSN.Caption, 15, 2)) Xor &HFF)
    
        lst_Log.AddItem "SAM < 80 88 00 83 08"
        lst_Log.AddItem "    <<" & Right("00" & Hex(SendBuff(5)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(6)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(7)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(8)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(9)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(10)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(11)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(12)), 2)
        lst_Log.ListIndex = lst_Log.NewIndex
    
        RecvLen = 2
    
        If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
               If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
               End If
        Else
               Exit Sub
        End If
        
        
        'Get Response to Retrieve Generated Key
        ReDim SendBuff(0 To 4) As Byte
        ReDim RecvBuff(0 To 9) As Byte
    
        SendBuff(0) = &H0
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        
        lst_Log.AddItem "SAM < 00 C0 00 00 08"
        lst_Log.ListIndex = lst_Log.NewIndex
        
        RecvLen = 10
        
        If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then
        
               If Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    'Retrieve Generated Key
                    GeneratedKey = ""
        
                    For i = 0 To 7
                        GeneratedKey = GeneratedKey & Right("00" & Hex(RecvBuff(i)), 2) & " "
                    Next i
                    lst_Log.AddItem "SAM >> " & GeneratedKey
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    
                    lbl_r_Kt.Caption = Replace(GeneratedKey, " ", "")
                    
               End If
               
        Else
               Exit Sub
        End If
        
    Else
    
        lbl_r_Kt.Caption = ""
    
    End If
    
    
    
    
    
    
    
    'Generate Debit Key (Kd) Using 4th SAM Master Key (KeyID=84)
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H88
    SendBuff(2) = &H0
    SendBuff(3) = &H84 'KeyID
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(lbl_CardSN.Caption, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(lbl_CardSN.Caption, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(lbl_CardSN.Caption, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(lbl_CardSN.Caption, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(lbl_CardSN.Caption, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(lbl_CardSN.Caption, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(lbl_CardSN.Caption, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(lbl_CardSN.Caption, 15, 2))

    lst_Log.AddItem "SAM < 80 88 00 84 08"
    lst_Log.AddItem "    <<" & Mid(lbl_CardSN.Caption, 1, 2) _
                             & Mid(lbl_CardSN.Caption, 3, 2) _
                             & Mid(lbl_CardSN.Caption, 5, 2) _
                             & Mid(lbl_CardSN.Caption, 7, 2) _
                             & Mid(lbl_CardSN.Caption, 9, 2) _
                             & Mid(lbl_CardSN.Caption, 11, 2) _
                             & Mid(lbl_CardSN.Caption, 13, 2) _
                             & Mid(lbl_CardSN.Caption, 15, 2)
    lst_Log.ListIndex = lst_Log.NewIndex

    RecvLen = 2

    If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    'Get Response to Retrieve Generated Key
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 9) As Byte

    SendBuff(0) = &H0
    SendBuff(1) = &HC0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    
    lst_Log.AddItem "SAM < 00 C0 00 00 08"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 10
    
    If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve Generated Key
                GeneratedKey = ""
    
                For i = 0 To 7
                    GeneratedKey = GeneratedKey & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                lst_Log.AddItem "SAM >> " & GeneratedKey
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                lbl_Kd.Caption = Replace(GeneratedKey, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    'If Algorithm Reference = 3DES then Generate Right Half of Debit Key (Kd) Using 4th SAM Master Key (KeyID=84)
    If G_AlgoRef = 0 Then
        
        ReDim SendBuff(0 To 12) As Byte
        ReDim RecvBuff(0 To 1) As Byte
    
        
    
        SendBuff(0) = &H80
        SendBuff(1) = &H88
        SendBuff(2) = &H0
        SendBuff(3) = &H84 'KeyID
        SendBuff(4) = &H8
        
        'compliment the card serial number to generate right half key for 3DES algorithm
        SendBuff(5) = (CInt("&H" & Mid(lbl_CardSN.Caption, 1, 2)) Xor &HFF)
        SendBuff(6) = (CInt("&H" & Mid(lbl_CardSN.Caption, 3, 2)) Xor &HFF)
        SendBuff(7) = (CInt("&H" & Mid(lbl_CardSN.Caption, 5, 2)) Xor &HFF)
        SendBuff(8) = (CInt("&H" & Mid(lbl_CardSN.Caption, 7, 2)) Xor &HFF)
        SendBuff(9) = (CInt("&H" & Mid(lbl_CardSN.Caption, 9, 2)) Xor &HFF)
        SendBuff(10) = (CInt("&H" & Mid(lbl_CardSN.Caption, 11, 2)) Xor &HFF)
        SendBuff(11) = (CInt("&H" & Mid(lbl_CardSN.Caption, 13, 2)) Xor &HFF)
        SendBuff(12) = (CInt("&H" & Mid(lbl_CardSN.Caption, 15, 2)) Xor &HFF)
    
        lst_Log.AddItem "SAM < 80 88 00 84 08"
        lst_Log.AddItem "    <<" & Right("00" & Hex(SendBuff(5)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(6)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(7)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(8)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(9)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(10)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(11)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(12)), 2)
        lst_Log.ListIndex = lst_Log.NewIndex
    
        RecvLen = 2
    
        If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
               If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
               End If
        Else
               Exit Sub
        End If
        
        
        'Get Response to Retrieve Generated Key
        ReDim SendBuff(0 To 4) As Byte
        ReDim RecvBuff(0 To 9) As Byte
    
        SendBuff(0) = &H0
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        
        lst_Log.AddItem "SAM < 00 C0 00 00 08"
        lst_Log.ListIndex = lst_Log.NewIndex
        
        RecvLen = 10
        
        If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then
        
               If Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    'Retrieve Generated Key
                    GeneratedKey = ""
        
                    For i = 0 To 7
                        GeneratedKey = GeneratedKey & Right("00" & Hex(RecvBuff(i)), 2) & " "
                    Next i
                    lst_Log.AddItem "SAM >> " & GeneratedKey
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    
                    lbl_r_Kd.Caption = Replace(GeneratedKey, " ", "")
                    
               End If
               
        Else
               Exit Sub
        End If
        
    Else
    
        lbl_r_Kd.Caption = ""
    
    End If
    
    
    
    
    
    
    'Generate Credit Key (Kcr) Using 5th SAM Master Key (KeyID=85)
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H88
    SendBuff(2) = &H0
    SendBuff(3) = &H85 'KeyID
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(lbl_CardSN.Caption, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(lbl_CardSN.Caption, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(lbl_CardSN.Caption, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(lbl_CardSN.Caption, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(lbl_CardSN.Caption, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(lbl_CardSN.Caption, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(lbl_CardSN.Caption, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(lbl_CardSN.Caption, 15, 2))

    lst_Log.AddItem "SAM < 80 88 00 85 08"
    lst_Log.AddItem "    <<" & Mid(lbl_CardSN.Caption, 1, 2) _
                             & Mid(lbl_CardSN.Caption, 3, 2) _
                             & Mid(lbl_CardSN.Caption, 5, 2) _
                             & Mid(lbl_CardSN.Caption, 7, 2) _
                             & Mid(lbl_CardSN.Caption, 9, 2) _
                             & Mid(lbl_CardSN.Caption, 11, 2) _
                             & Mid(lbl_CardSN.Caption, 13, 2) _
                             & Mid(lbl_CardSN.Caption, 15, 2)
    lst_Log.ListIndex = lst_Log.NewIndex

    RecvLen = 2

    If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    'Get Response to Retrieve Generated Key
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 9) As Byte

    SendBuff(0) = &H0
    SendBuff(1) = &HC0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    
    lst_Log.AddItem "SAM < 00 C0 00 00 08"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 10
    
    If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve Generated Key
                GeneratedKey = ""
    
                For i = 0 To 7
                    GeneratedKey = GeneratedKey & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                lst_Log.AddItem "SAM >> " & GeneratedKey
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                lbl_Kcr.Caption = Replace(GeneratedKey, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    
    'If Algorithm Reference = 3DES then Generate Right Half of Credit Key (Kcr) Using 5th SAM Master Key (KeyID=85)
    If G_AlgoRef = 0 Then
        
        ReDim SendBuff(0 To 12) As Byte
        ReDim RecvBuff(0 To 1) As Byte
    
        
    
        SendBuff(0) = &H80
        SendBuff(1) = &H88
        SendBuff(2) = &H0
        SendBuff(3) = &H85 'KeyID
        SendBuff(4) = &H8
        
        'compliment the card serial number to generate right half key for 3DES algorithm
        SendBuff(5) = (CInt("&H" & Mid(lbl_CardSN.Caption, 1, 2)) Xor &HFF)
        SendBuff(6) = (CInt("&H" & Mid(lbl_CardSN.Caption, 3, 2)) Xor &HFF)
        SendBuff(7) = (CInt("&H" & Mid(lbl_CardSN.Caption, 5, 2)) Xor &HFF)
        SendBuff(8) = (CInt("&H" & Mid(lbl_CardSN.Caption, 7, 2)) Xor &HFF)
        SendBuff(9) = (CInt("&H" & Mid(lbl_CardSN.Caption, 9, 2)) Xor &HFF)
        SendBuff(10) = (CInt("&H" & Mid(lbl_CardSN.Caption, 11, 2)) Xor &HFF)
        SendBuff(11) = (CInt("&H" & Mid(lbl_CardSN.Caption, 13, 2)) Xor &HFF)
        SendBuff(12) = (CInt("&H" & Mid(lbl_CardSN.Caption, 15, 2)) Xor &HFF)
    
        lst_Log.AddItem "SAM < 80 88 00 85 08"
        lst_Log.AddItem "    <<" & Right("00" & Hex(SendBuff(5)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(6)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(7)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(8)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(9)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(10)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(11)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(12)), 2)
        lst_Log.ListIndex = lst_Log.NewIndex
    
        RecvLen = 2
    
        If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
               If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
               End If
        Else
               Exit Sub
        End If
        
        
        'Get Response to Retrieve Generated Key
        ReDim SendBuff(0 To 4) As Byte
        ReDim RecvBuff(0 To 9) As Byte
    
        SendBuff(0) = &H0
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        
        lst_Log.AddItem "SAM < 00 C0 00 00 08"
        lst_Log.ListIndex = lst_Log.NewIndex
        
        RecvLen = 10
        
        If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then
        
               If Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    'Retrieve Generated Key
                    GeneratedKey = ""
        
                    For i = 0 To 7
                        GeneratedKey = GeneratedKey & Right("00" & Hex(RecvBuff(i)), 2) & " "
                    Next i
                    lst_Log.AddItem "SAM >> " & GeneratedKey
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    
                    lbl_r_Kcr.Caption = Replace(GeneratedKey, " ", "")
                    
               End If
               
        Else
               Exit Sub
        End If
        
    Else
    
        lbl_r_Kcr.Caption = ""
    
    End If
    
    
    'Generate Certify Key (Kcf) Using 6th SAM Master Key (KeyID=86)
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H88
    SendBuff(2) = &H0
    SendBuff(3) = &H86 'KeyID
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(lbl_CardSN.Caption, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(lbl_CardSN.Caption, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(lbl_CardSN.Caption, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(lbl_CardSN.Caption, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(lbl_CardSN.Caption, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(lbl_CardSN.Caption, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(lbl_CardSN.Caption, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(lbl_CardSN.Caption, 15, 2))

    lst_Log.AddItem "SAM < 80 88 00 86 08"
    lst_Log.AddItem "    <<" & Mid(lbl_CardSN.Caption, 1, 2) _
                             & Mid(lbl_CardSN.Caption, 3, 2) _
                             & Mid(lbl_CardSN.Caption, 5, 2) _
                             & Mid(lbl_CardSN.Caption, 7, 2) _
                             & Mid(lbl_CardSN.Caption, 9, 2) _
                             & Mid(lbl_CardSN.Caption, 11, 2) _
                             & Mid(lbl_CardSN.Caption, 13, 2) _
                             & Mid(lbl_CardSN.Caption, 15, 2)
    lst_Log.ListIndex = lst_Log.NewIndex

    RecvLen = 2

    If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    'Get Response to Retrieve Generated Key
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 9) As Byte

    SendBuff(0) = &H0
    SendBuff(1) = &HC0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    
    lst_Log.AddItem "SAM < 00 C0 00 00 08"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 10
    
    If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve Generated Key
                GeneratedKey = ""
    
                For i = 0 To 7
                    GeneratedKey = GeneratedKey & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                lst_Log.AddItem "SAM >> " & GeneratedKey
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                lbl_Kcf.Caption = Replace(GeneratedKey, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    
    'If Algorithm Reference = 3DES then Generate Right Half of Certify Key (Kcf) Using 6th SAM Master Key (KeyID=86)
    If G_AlgoRef = 0 Then
        
        ReDim SendBuff(0 To 12) As Byte
        ReDim RecvBuff(0 To 1) As Byte
    
        
    
        SendBuff(0) = &H80
        SendBuff(1) = &H88
        SendBuff(2) = &H0
        SendBuff(3) = &H86 'KeyID
        SendBuff(4) = &H8
        
        'compliment the card serial number to generate right half key for 3DES algorithm
        SendBuff(5) = (CInt("&H" & Mid(lbl_CardSN.Caption, 1, 2)) Xor &HFF)
        SendBuff(6) = (CInt("&H" & Mid(lbl_CardSN.Caption, 3, 2)) Xor &HFF)
        SendBuff(7) = (CInt("&H" & Mid(lbl_CardSN.Caption, 5, 2)) Xor &HFF)
        SendBuff(8) = (CInt("&H" & Mid(lbl_CardSN.Caption, 7, 2)) Xor &HFF)
        SendBuff(9) = (CInt("&H" & Mid(lbl_CardSN.Caption, 9, 2)) Xor &HFF)
        SendBuff(10) = (CInt("&H" & Mid(lbl_CardSN.Caption, 11, 2)) Xor &HFF)
        SendBuff(11) = (CInt("&H" & Mid(lbl_CardSN.Caption, 13, 2)) Xor &HFF)
        SendBuff(12) = (CInt("&H" & Mid(lbl_CardSN.Caption, 15, 2)) Xor &HFF)
    
        lst_Log.AddItem "SAM < 80 88 00 86 08"
        lst_Log.AddItem "    <<" & Right("00" & Hex(SendBuff(5)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(6)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(7)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(8)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(9)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(10)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(11)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(12)), 2)
        lst_Log.ListIndex = lst_Log.NewIndex
    
        RecvLen = 2
    
        If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
               If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
               End If
        Else
               Exit Sub
        End If
        
        
        'Get Response to Retrieve Generated Key
        ReDim SendBuff(0 To 4) As Byte
        ReDim RecvBuff(0 To 9) As Byte
    
        SendBuff(0) = &H0
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        
        lst_Log.AddItem "SAM < 00 C0 00 00 08"
        lst_Log.ListIndex = lst_Log.NewIndex
        
        RecvLen = 10
        
        If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then
        
               If Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    'Retrieve Generated Key
                    GeneratedKey = ""
        
                    For i = 0 To 7
                        GeneratedKey = GeneratedKey & Right("00" & Hex(RecvBuff(i)), 2) & " "
                    Next i
                    lst_Log.AddItem "SAM >> " & GeneratedKey
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    
                    lbl_r_Kcf.Caption = Replace(GeneratedKey, " ", "")
                    
               End If
               
        Else
               Exit Sub
        End If
        
    Else
    
        lbl_r_Kcf.Caption = ""
    
    End If
    
    
    'Generate Revoke Debit Key (Krd) Using 7th SAM Master Key (KeyID=87)
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H88
    SendBuff(2) = &H0
    SendBuff(3) = &H87 'KeyID
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(lbl_CardSN.Caption, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(lbl_CardSN.Caption, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(lbl_CardSN.Caption, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(lbl_CardSN.Caption, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(lbl_CardSN.Caption, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(lbl_CardSN.Caption, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(lbl_CardSN.Caption, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(lbl_CardSN.Caption, 15, 2))

    lst_Log.AddItem "SAM < 80 88 00 87 08"
    lst_Log.AddItem "    <<" & Mid(lbl_CardSN.Caption, 1, 2) _
                             & Mid(lbl_CardSN.Caption, 3, 2) _
                             & Mid(lbl_CardSN.Caption, 5, 2) _
                             & Mid(lbl_CardSN.Caption, 7, 2) _
                             & Mid(lbl_CardSN.Caption, 9, 2) _
                             & Mid(lbl_CardSN.Caption, 11, 2) _
                             & Mid(lbl_CardSN.Caption, 13, 2) _
                             & Mid(lbl_CardSN.Caption, 15, 2)
    lst_Log.ListIndex = lst_Log.NewIndex

    RecvLen = 2

    If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    'Get Response to Retrieve Generated Key
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 9) As Byte

    SendBuff(0) = &H0
    SendBuff(1) = &HC0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    
    lst_Log.AddItem "SAM < 00 C0 00 00 08"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 10
    
    If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve Generated Key
                GeneratedKey = ""
    
                For i = 0 To 7
                    GeneratedKey = GeneratedKey & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                lst_Log.AddItem "SAM >> " & GeneratedKey
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                lbl_Krd.Caption = Replace(GeneratedKey, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    'If Algorithm Reference = 3DES then Generate Right Half of Revoke Debit Key (Krd) Using 7th SAM Master Key (KeyID=87)
    If G_AlgoRef = 0 Then
        
        ReDim SendBuff(0 To 12) As Byte
        ReDim RecvBuff(0 To 1) As Byte
    
        
    
        SendBuff(0) = &H80
        SendBuff(1) = &H88
        SendBuff(2) = &H0
        SendBuff(3) = &H87 'KeyID
        SendBuff(4) = &H8
        
        'compliment the card serial number to generate right half key for 3DES algorithm
        SendBuff(5) = (CInt("&H" & Mid(lbl_CardSN.Caption, 1, 2)) Xor &HFF)
        SendBuff(6) = (CInt("&H" & Mid(lbl_CardSN.Caption, 3, 2)) Xor &HFF)
        SendBuff(7) = (CInt("&H" & Mid(lbl_CardSN.Caption, 5, 2)) Xor &HFF)
        SendBuff(8) = (CInt("&H" & Mid(lbl_CardSN.Caption, 7, 2)) Xor &HFF)
        SendBuff(9) = (CInt("&H" & Mid(lbl_CardSN.Caption, 9, 2)) Xor &HFF)
        SendBuff(10) = (CInt("&H" & Mid(lbl_CardSN.Caption, 11, 2)) Xor &HFF)
        SendBuff(11) = (CInt("&H" & Mid(lbl_CardSN.Caption, 13, 2)) Xor &HFF)
        SendBuff(12) = (CInt("&H" & Mid(lbl_CardSN.Caption, 15, 2)) Xor &HFF)
    
        lst_Log.AddItem "SAM < 80 88 00 87 08"
        lst_Log.AddItem "    <<" & Right("00" & Hex(SendBuff(5)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(6)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(7)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(8)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(9)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(10)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(11)), 2) & " " _
                                 & Right("00" & Hex(SendBuff(12)), 2)
        lst_Log.ListIndex = lst_Log.NewIndex
    
        RecvLen = 2
    
        If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
               If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
               End If
        Else
               Exit Sub
        End If
        
        
        'Get Response to Retrieve Generated Key
        ReDim SendBuff(0 To 4) As Byte
        ReDim RecvBuff(0 To 9) As Byte
    
        SendBuff(0) = &H0
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        
        lst_Log.AddItem "SAM < 00 C0 00 00 08"
        lst_Log.ListIndex = lst_Log.NewIndex
        
        RecvLen = 10
        
        If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then
        
               If Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    'Retrieve Generated Key
                    GeneratedKey = ""
        
                    For i = 0 To 7
                        GeneratedKey = GeneratedKey & Right("00" & Hex(RecvBuff(i)), 2) & " "
                    Next i
                    lst_Log.AddItem "SAM >> " & GeneratedKey
                    lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    
                    lbl_r_Krd.Caption = Replace(GeneratedKey, " ", "")
                    
               End If
               
        Else
               Exit Sub
        End If
        
    Else
    
        lbl_r_Krd.Caption = ""
    
    End If
    
    

End Sub

Private Sub cmd_InitSAM_Click()
Dim SendBuff() As Byte
Dim RecvBuff() As Byte
Dim RecvLen As Long

    'Check if null and check length if correct
    If txtSAMGPIN.Text = "" Then
        lst_Log.AddItem "Input Required"
        lst_Log.ListIndex = lst_Log.NewIndex
        txtSAMGPIN.SetFocus
        Exit Sub
    End If
    
    If Len(txtSAMGPIN.Text) <> 16 Then
        lst_Log.AddItem "Invalid Input Length"
        lst_Log.ListIndex = lst_Log.NewIndex
        txtSAMGPIN.SetFocus
        Exit Sub
    End If
        
    
    If Not CheckInput(txt_IC) Then Exit Sub
    If Not CheckInput(txtKc) Then Exit Sub
    If Not CheckInput(txtKt) Then Exit Sub
    If Not CheckInput(txtKd) Then Exit Sub
    If Not CheckInput(txtKcr) Then Exit Sub
    If Not CheckInput(txtKcf) Then Exit Sub
    If Not CheckInput(txtKrd) Then Exit Sub
    
    'Clear Card's EEPROM
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H30
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H0
    
    lst_Log.AddItem "SAM < 80 30 00 00 00"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = False Then
        lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
        lst_Log.ListIndex = lst_Log.NewIndex
        Exit Sub
    Else
        lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
        lst_Log.ListIndex = lst_Log.NewIndex
    End If

    'Reset
    cmd_Connect_SAM_Click

    'Create MF
    ReDim SendBuff(0 To 18) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H0
    SendBuff(1) = &HE0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &HE
    SendBuff(5) = &H62
    SendBuff(6) = &HC
    SendBuff(7) = &H80
    SendBuff(8) = &H2
    SendBuff(9) = &H2C
    SendBuff(10) = &H0
    SendBuff(11) = &H82
    SendBuff(12) = &H2
    SendBuff(13) = &H3F
    SendBuff(14) = &HFF
    SendBuff(15) = &H83
    SendBuff(16) = &H2
    SendBuff(17) = &H3F
    SendBuff(18) = &H0
    
    lst_Log.AddItem "SAM < 00 E0 00 00 0E"
    lst_Log.AddItem "    <<62 0C 80 02 2C 00 82 02 02 3F FF 83 02 3F 00"
    lst_Log.ListIndex = lst_Log.NewIndex
        
    RecvLen = 2
        
    If Send_APDU_SAM(SendBuff, 19, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If

    
    
    'Create EF1 to store PIN's
    'FDB=0C MRL=0A NOR=01 READ=NONE WRITE=IC
    ReDim SendBuff(0 To 31) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    
    SendBuff(0) = &H0
    SendBuff(1) = &HE0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H1B
    SendBuff(5) = &H62
    SendBuff(6) = &H19
    SendBuff(7) = &H83
    SendBuff(8) = &H2
    SendBuff(9) = &HFF
    SendBuff(10) = &HA
    SendBuff(11) = &H88
    SendBuff(12) = &H1
    SendBuff(13) = &H1
    SendBuff(14) = &H82
    SendBuff(15) = &H6
    SendBuff(16) = &HC
    SendBuff(17) = &H0
    SendBuff(18) = &H0
    SendBuff(19) = &HA
    SendBuff(20) = &H0
    SendBuff(21) = &H1
    SendBuff(22) = &H8C
    SendBuff(23) = &H8
    SendBuff(24) = &H7F
    SendBuff(25) = &HFF
    SendBuff(26) = &HFF
    SendBuff(27) = &HFF
    SendBuff(28) = &HFF
    SendBuff(29) = &H27
    SendBuff(30) = &H27
    SendBuff(31) = &HFF
    
    lst_Log.AddItem "SAM < 00 E0 00 00 1B"
    lst_Log.AddItem "    <<62 19 83 02 FF 0A 88 01 01 82 06 0C 00 00 0A 00"
    lst_Log.AddItem "    <<01 8C 08 7F FF FF FF FF 27 27 FF"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SAM(SendBuff, 32, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If

        
    'Set Global PIN's
    ReDim SendBuff(0 To 14) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    
    SendBuff(0) = &H0
    SendBuff(1) = &HDC
    SendBuff(2) = &H1
    SendBuff(3) = &H4
    SendBuff(4) = &HA
    SendBuff(5) = &H1
    SendBuff(6) = &H88
    SendBuff(7) = CInt("&H" & Mid(txtSAMGPIN.Text, 1, 2))
    SendBuff(8) = CInt("&H" & Mid(txtSAMGPIN.Text, 3, 2))
    SendBuff(9) = CInt("&H" & Mid(txtSAMGPIN.Text, 5, 2))
    SendBuff(10) = CInt("&H" & Mid(txtSAMGPIN.Text, 7, 2))
    SendBuff(11) = CInt("&H" & Mid(txtSAMGPIN.Text, 9, 2))
    SendBuff(12) = CInt("&H" & Mid(txtSAMGPIN.Text, 11, 2))
    SendBuff(13) = CInt("&H" & Mid(txtSAMGPIN.Text, 13, 2))
    SendBuff(14) = CInt("&H" & Mid(txtSAMGPIN.Text, 15, 2))
    
    lst_Log.AddItem "SAM < 00 DC 01 04 0A"
    lst_Log.AddItem "    <<01 88 " & Mid(txtSAMGPIN.Text, 1, 2) & " " _
                                   & Mid(txtSAMGPIN.Text, 3, 2) & " " _
                                   & Mid(txtSAMGPIN.Text, 5, 2) & " " _
                                   & Mid(txtSAMGPIN.Text, 7, 2) & " " _
                                   & Mid(txtSAMGPIN.Text, 9, 2) & " " _
                                   & Mid(txtSAMGPIN.Text, 11, 2) & " " _
                                   & Mid(txtSAMGPIN.Text, 13, 2) & " " _
                                   & Mid(txtSAMGPIN.Text, 15, 2)
                                   
    lst_Log.ListIndex = lst_Log.NewIndex
            
    RecvLen = 2
            
    If Send_APDU_SAM(SendBuff, 15, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    
    'Create Next DF DRT01: 1100
    
    ReDim SendBuff(0 To 47) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    
    SendBuff(0) = &H0
    SendBuff(1) = &HE0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H2B
    SendBuff(5) = &H62
    SendBuff(6) = &H29
    SendBuff(7) = &H82
    SendBuff(8) = &H1
    SendBuff(9) = &H38
    SendBuff(10) = &H83
    SendBuff(11) = &H2
    SendBuff(12) = &H11
    SendBuff(13) = &H0
    SendBuff(14) = &H8A
    SendBuff(15) = &H1
    SendBuff(16) = &H1
    SendBuff(17) = &H8C
    SendBuff(18) = &H8
    SendBuff(19) = &H7F
    SendBuff(20) = &H3
    SendBuff(21) = &H3
    SendBuff(22) = &H3
    SendBuff(23) = &H3
    SendBuff(24) = &H3
    SendBuff(25) = &H3
    SendBuff(26) = &H3
    SendBuff(27) = &H8D
    SendBuff(28) = &H2
    SendBuff(29) = &H41
    SendBuff(30) = &H3
    SendBuff(31) = &H80
    SendBuff(32) = &H2
    SendBuff(33) = &H3
    SendBuff(34) = &H20
    SendBuff(35) = &HAB
    SendBuff(36) = &HB
    SendBuff(37) = &H84
    SendBuff(38) = &H1
    SendBuff(39) = &H88
    SendBuff(40) = &HA4
    SendBuff(41) = &H6
    SendBuff(42) = &H83
    SendBuff(43) = &H1
    SendBuff(44) = &H81
    SendBuff(45) = &H95
    SendBuff(46) = &H1
    SendBuff(47) = &HFF
    
    lst_Log.AddItem "SAM < 00 E0 00 00 2B"
    lst_Log.AddItem "    <<62 29 82 01 38 83 02 11 00 8A 01 01 8C 08 7F 03"
    lst_Log.AddItem "    <<03 03 03 03 03 03 8D 02 41 03 20 AB 0B 84 01 88"
    lst_Log.AddItem "    <<A4 06 83 01 81 95 01 FF"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SAM(SendBuff, 48, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    'Create Key File EF2 1101
    'MRL=16 NOR=08
    
    ReDim SendBuff(0 To 33) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    
    SendBuff(0) = &H0
    SendBuff(1) = &HE0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H1D
    SendBuff(5) = &H62
    SendBuff(6) = &H1B
    SendBuff(7) = &H82
    SendBuff(8) = &H5
    SendBuff(9) = &HC
    SendBuff(10) = &H41
    SendBuff(11) = &H0
    SendBuff(12) = &H16
    SendBuff(13) = &H8
    SendBuff(14) = &H83
    SendBuff(15) = &H2
    SendBuff(16) = &H11
    SendBuff(17) = &H1
    SendBuff(18) = &H88
    SendBuff(19) = &H1
    SendBuff(20) = &H2
    SendBuff(21) = &H8A
    SendBuff(22) = &H1
    SendBuff(23) = &H1
    SendBuff(24) = &H8C
    SendBuff(25) = &H8
    SendBuff(26) = &H7F
    SendBuff(27) = &H3
    SendBuff(28) = &H3
    SendBuff(29) = &H3
    SendBuff(30) = &H3
    SendBuff(31) = &H3
    SendBuff(32) = &H3
    SendBuff(33) = &H3
    
    lst_Log.AddItem "SAM < 00 E0 00 00 1D"
    lst_Log.AddItem "    <<62 1B 82 05 0C 41 00 16 08 83 02 11 01 88 01 02"
    lst_Log.AddItem "    <<8A 01 01 8C 08 7F 03 03 03 03 03 03 03"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SAM(SendBuff, 34, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    'Append Record To EF2, Define 8 Key Records in EF2 - Master Keys
    '1st Master key, key ID=81, key type=03, int/ext authenticate, usage counter = FF FF
    ReDim SendBuff(0 To 26) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    
    SendBuff(0) = &H0
    SendBuff(1) = &HE2
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H16
    SendBuff(5) = &H81 'Key ID
    SendBuff(6) = &H3
    SendBuff(7) = &HFF
    SendBuff(8) = &HFF
    SendBuff(9) = &H88
    SendBuff(10) = &H0
    SendBuff(11) = CInt("&H" & Mid(txt_IC.Text, 1, 2))
    SendBuff(12) = CInt("&H" & Mid(txt_IC.Text, 3, 2))
    SendBuff(13) = CInt("&H" & Mid(txt_IC.Text, 5, 2))
    SendBuff(14) = CInt("&H" & Mid(txt_IC.Text, 7, 2))
    SendBuff(15) = CInt("&H" & Mid(txt_IC.Text, 9, 2))
    SendBuff(16) = CInt("&H" & Mid(txt_IC.Text, 11, 2))
    SendBuff(17) = CInt("&H" & Mid(txt_IC.Text, 13, 2))
    SendBuff(18) = CInt("&H" & Mid(txt_IC.Text, 15, 2))
    SendBuff(19) = CInt("&H" & Mid(txt_IC.Text, 17, 2))
    SendBuff(20) = CInt("&H" & Mid(txt_IC.Text, 19, 2))
    SendBuff(21) = CInt("&H" & Mid(txt_IC.Text, 21, 2))
    SendBuff(22) = CInt("&H" & Mid(txt_IC.Text, 23, 2))
    SendBuff(23) = CInt("&H" & Mid(txt_IC.Text, 25, 2))
    SendBuff(24) = CInt("&H" & Mid(txt_IC.Text, 27, 2))
    SendBuff(25) = CInt("&H" & Mid(txt_IC.Text, 29, 2))
    SendBuff(26) = CInt("&H" & Mid(txt_IC.Text, 31, 2))

    
    lst_Log.AddItem "SAM < 00 E2 00 00 16"
    lst_Log.AddItem "    <<81 03 FF FF 88 00 " _
                                         & Mid(txt_IC.Text, 1, 2) & " " _
                                         & Mid(txt_IC.Text, 3, 2) & " " _
                                         & Mid(txt_IC.Text, 5, 2) & " " _
                                         & Mid(txt_IC.Text, 7, 2) & " " _
                                         & Mid(txt_IC.Text, 9, 2) & " " _
                                         & Mid(txt_IC.Text, 11, 2) & " " _
                                         & Mid(txt_IC.Text, 13, 2) & " " _
                                         & Mid(txt_IC.Text, 15, 2) & " " _
                                         & Mid(txt_IC.Text, 17, 2) & " " _
                                         & Mid(txt_IC.Text, 19, 2)
     
     lst_Log.AddItem "    <<" & Mid(txt_IC.Text, 21, 2) & " " _
                                         & Mid(txt_IC.Text, 23, 2) & " " _
                                         & Mid(txt_IC.Text, 25, 2) & " " _
                                         & Mid(txt_IC.Text, 27, 2) & " " _
                                         & Mid(txt_IC.Text, 29, 2) & " " _
                                         & Mid(txt_IC.Text, 31, 2)
    
    lst_Log.ListIndex = lst_Log.NewIndex

    RecvLen = 2

    If Send_APDU_SAM(SendBuff, 27, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
       Exit Sub
    End If
                                             
    
    
                                             
    
    
    '2nd Master key, key ID=82, key type=03, int/ext authenticate, usage counter = FF FF
    ReDim SendBuff(0 To 26) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    
    SendBuff(0) = &H0
    SendBuff(1) = &HE2
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H16
    SendBuff(5) = &H82 'Key ID
    SendBuff(6) = &H3
    SendBuff(7) = &HFF
    SendBuff(8) = &HFF
    SendBuff(9) = &H88
    SendBuff(10) = &H0
    SendBuff(11) = CInt("&H" & Mid(txtKc.Text, 1, 2))
    SendBuff(12) = CInt("&H" & Mid(txtKc.Text, 3, 2))
    SendBuff(13) = CInt("&H" & Mid(txtKc.Text, 5, 2))
    SendBuff(14) = CInt("&H" & Mid(txtKc.Text, 7, 2))
    SendBuff(15) = CInt("&H" & Mid(txtKc.Text, 9, 2))
    SendBuff(16) = CInt("&H" & Mid(txtKc.Text, 11, 2))
    SendBuff(17) = CInt("&H" & Mid(txtKc.Text, 13, 2))
    SendBuff(18) = CInt("&H" & Mid(txtKc.Text, 15, 2))
    SendBuff(19) = CInt("&H" & Mid(txtKc.Text, 17, 2))
    SendBuff(20) = CInt("&H" & Mid(txtKc.Text, 19, 2))
    SendBuff(21) = CInt("&H" & Mid(txtKc.Text, 21, 2))
    SendBuff(22) = CInt("&H" & Mid(txtKc.Text, 23, 2))
    SendBuff(23) = CInt("&H" & Mid(txtKc.Text, 25, 2))
    SendBuff(24) = CInt("&H" & Mid(txtKc.Text, 27, 2))
    SendBuff(25) = CInt("&H" & Mid(txtKc.Text, 29, 2))
    SendBuff(26) = CInt("&H" & Mid(txtKc.Text, 31, 2))

    lst_Log.AddItem "SAM < 00 E2 00 00 16"
    lst_Log.AddItem "    <<82 03 FF FF 88 00 " _
                                         & Mid(txtKc.Text, 1, 2) & " " _
                                         & Mid(txtKc.Text, 3, 2) & " " _
                                         & Mid(txtKc.Text, 5, 2) & " " _
                                         & Mid(txtKc.Text, 7, 2) & " " _
                                         & Mid(txtKc.Text, 9, 2) & " " _
                                         & Mid(txtKc.Text, 11, 2) & " " _
                                         & Mid(txtKc.Text, 13, 2) & " " _
                                         & Mid(txtKc.Text, 15, 2) & " " _
                                         & Mid(txtKc.Text, 17, 2) & " " _
                                         & Mid(txtKc.Text, 19, 2)
     
     lst_Log.AddItem "    <<" & Mid(txtKc.Text, 21, 2) & " " _
                                         & Mid(txtKc.Text, 23, 2) & " " _
                                         & Mid(txtKc.Text, 25, 2) & " " _
                                         & Mid(txtKc.Text, 27, 2) & " " _
                                         & Mid(txtKc.Text, 29, 2) & " " _
                                         & Mid(txtKc.Text, 31, 2)
    
    lst_Log.ListIndex = lst_Log.NewIndex

    RecvLen = 2

    If Send_APDU_SAM(SendBuff, 27, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
       Exit Sub
    End If
                                             
    
    
    '3rd Master key, key ID=83, key type=03, int/ext authenticate, usage counter = FF FF
    ReDim SendBuff(0 To 26) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    
    SendBuff(0) = &H0
    SendBuff(1) = &HE2
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H16
    SendBuff(5) = &H83 'Key ID
    SendBuff(6) = &H3
    SendBuff(7) = &HFF
    SendBuff(8) = &HFF
    SendBuff(9) = &H88
    SendBuff(10) = &H0
    SendBuff(11) = CInt("&H" & Mid(txtKt.Text, 1, 2))
    SendBuff(12) = CInt("&H" & Mid(txtKt.Text, 3, 2))
    SendBuff(13) = CInt("&H" & Mid(txtKt.Text, 5, 2))
    SendBuff(14) = CInt("&H" & Mid(txtKt.Text, 7, 2))
    SendBuff(15) = CInt("&H" & Mid(txtKt.Text, 9, 2))
    SendBuff(16) = CInt("&H" & Mid(txtKt.Text, 11, 2))
    SendBuff(17) = CInt("&H" & Mid(txtKt.Text, 13, 2))
    SendBuff(18) = CInt("&H" & Mid(txtKt.Text, 15, 2))
    SendBuff(19) = CInt("&H" & Mid(txtKt.Text, 17, 2))
    SendBuff(20) = CInt("&H" & Mid(txtKt.Text, 19, 2))
    SendBuff(21) = CInt("&H" & Mid(txtKt.Text, 21, 2))
    SendBuff(22) = CInt("&H" & Mid(txtKt.Text, 23, 2))
    SendBuff(23) = CInt("&H" & Mid(txtKt.Text, 25, 2))
    SendBuff(24) = CInt("&H" & Mid(txtKt.Text, 27, 2))
    SendBuff(25) = CInt("&H" & Mid(txtKt.Text, 29, 2))
    SendBuff(26) = CInt("&H" & Mid(txtKt.Text, 31, 2))
    
    lst_Log.AddItem "SAM < 00 E2 00 00 16"
    lst_Log.AddItem "    <<83 03 FF FF 88 00 " _
                                         & Mid(txtKt.Text, 1, 2) & " " _
                                         & Mid(txtKt.Text, 3, 2) & " " _
                                         & Mid(txtKt.Text, 5, 2) & " " _
                                         & Mid(txtKt.Text, 7, 2) & " " _
                                         & Mid(txtKt.Text, 9, 2) & " " _
                                         & Mid(txtKt.Text, 11, 2) & " " _
                                         & Mid(txtKt.Text, 13, 2) & " " _
                                         & Mid(txtKt.Text, 15, 2) & " " _
                                         & Mid(txtKt.Text, 17, 2) & " " _
                                         & Mid(txtKt.Text, 19, 2)
     
     lst_Log.AddItem "    <<" & Mid(txtKt.Text, 21, 2) & " " _
                                         & Mid(txtKt.Text, 23, 2) & " " _
                                         & Mid(txtKt.Text, 25, 2) & " " _
                                         & Mid(txtKt.Text, 27, 2) & " " _
                                         & Mid(txtKt.Text, 29, 2) & " " _
                                         & Mid(txtKt.Text, 31, 2)
    
    lst_Log.ListIndex = lst_Log.NewIndex

    RecvLen = 2

    If Send_APDU_SAM(SendBuff, 27, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
       Exit Sub
    End If
                                             
    
    
     
    '4th Master key, key ID=84, key type=03, int/ext authenticate, usage counter = FF FF
    ReDim SendBuff(0 To 26) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    
    SendBuff(0) = &H0
    SendBuff(1) = &HE2
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H16
    SendBuff(5) = &H84 'Key ID
    SendBuff(6) = &H3
    SendBuff(7) = &HFF
    SendBuff(8) = &HFF
    SendBuff(9) = &H88
    SendBuff(10) = &H0
    SendBuff(11) = CInt("&H" & Mid(txtKd.Text, 1, 2))
    SendBuff(12) = CInt("&H" & Mid(txtKd.Text, 3, 2))
    SendBuff(13) = CInt("&H" & Mid(txtKd.Text, 5, 2))
    SendBuff(14) = CInt("&H" & Mid(txtKd.Text, 7, 2))
    SendBuff(15) = CInt("&H" & Mid(txtKd.Text, 9, 2))
    SendBuff(16) = CInt("&H" & Mid(txtKd.Text, 11, 2))
    SendBuff(17) = CInt("&H" & Mid(txtKd.Text, 13, 2))
    SendBuff(18) = CInt("&H" & Mid(txtKd.Text, 15, 2))
    SendBuff(19) = CInt("&H" & Mid(txtKd.Text, 17, 2))
    SendBuff(20) = CInt("&H" & Mid(txtKd.Text, 19, 2))
    SendBuff(21) = CInt("&H" & Mid(txtKd.Text, 21, 2))
    SendBuff(22) = CInt("&H" & Mid(txtKd.Text, 23, 2))
    SendBuff(23) = CInt("&H" & Mid(txtKd.Text, 25, 2))
    SendBuff(24) = CInt("&H" & Mid(txtKd.Text, 27, 2))
    SendBuff(25) = CInt("&H" & Mid(txtKd.Text, 29, 2))
    SendBuff(26) = CInt("&H" & Mid(txtKd.Text, 31, 2))
        
    lst_Log.AddItem "SAM < 00 E2 00 00 16"
    lst_Log.AddItem "    <<84 03 FF FF 88 00 " _
                                         & Mid(txtKd.Text, 1, 2) & " " _
                                         & Mid(txtKd.Text, 3, 2) & " " _
                                         & Mid(txtKd.Text, 5, 2) & " " _
                                         & Mid(txtKd.Text, 7, 2) & " " _
                                         & Mid(txtKd.Text, 9, 2) & " " _
                                         & Mid(txtKd.Text, 11, 2) & " " _
                                         & Mid(txtKd.Text, 13, 2) & " " _
                                         & Mid(txtKd.Text, 15, 2) & " " _
                                         & Mid(txtKd.Text, 17, 2) & " " _
                                         & Mid(txtKd.Text, 19, 2)
     
     lst_Log.AddItem "    <<" & Mid(txtKd.Text, 21, 2) & " " _
                                         & Mid(txtKd.Text, 23, 2) & " " _
                                         & Mid(txtKd.Text, 25, 2) & " " _
                                         & Mid(txtKd.Text, 27, 2) & " " _
                                         & Mid(txtKd.Text, 29, 2) & " " _
                                         & Mid(txtKd.Text, 31, 2)
    
    lst_Log.ListIndex = lst_Log.NewIndex

    RecvLen = 2

    If Send_APDU_SAM(SendBuff, 27, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
       Exit Sub
    End If
                                             
    
    
    '5th Master key, key ID=85, key type=03, int/ext authenticate, usage counter = FF FF
    ReDim SendBuff(0 To 26) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    
    SendBuff(0) = &H0
    SendBuff(1) = &HE2
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H16
    SendBuff(5) = &H85 'Key ID
    SendBuff(6) = &H3
    SendBuff(7) = &HFF
    SendBuff(8) = &HFF
    SendBuff(9) = &H88
    SendBuff(10) = &H0
    SendBuff(11) = CInt("&H" & Mid(txtKcr.Text, 1, 2))
    SendBuff(12) = CInt("&H" & Mid(txtKcr.Text, 3, 2))
    SendBuff(13) = CInt("&H" & Mid(txtKcr.Text, 5, 2))
    SendBuff(14) = CInt("&H" & Mid(txtKcr.Text, 7, 2))
    SendBuff(15) = CInt("&H" & Mid(txtKcr.Text, 9, 2))
    SendBuff(16) = CInt("&H" & Mid(txtKcr.Text, 11, 2))
    SendBuff(17) = CInt("&H" & Mid(txtKcr.Text, 13, 2))
    SendBuff(18) = CInt("&H" & Mid(txtKcr.Text, 15, 2))
    SendBuff(19) = CInt("&H" & Mid(txtKcr.Text, 17, 2))
    SendBuff(20) = CInt("&H" & Mid(txtKcr.Text, 19, 2))
    SendBuff(21) = CInt("&H" & Mid(txtKcr.Text, 21, 2))
    SendBuff(22) = CInt("&H" & Mid(txtKcr.Text, 23, 2))
    SendBuff(23) = CInt("&H" & Mid(txtKcr.Text, 25, 2))
    SendBuff(24) = CInt("&H" & Mid(txtKcr.Text, 27, 2))
    SendBuff(25) = CInt("&H" & Mid(txtKcr.Text, 29, 2))
    SendBuff(26) = CInt("&H" & Mid(txtKcr.Text, 31, 2))
    
    lst_Log.AddItem "SAM < 00 E2 00 00 16"
    lst_Log.AddItem "    <<85 03 FF FF 88 00 " _
                                         & Mid(txtKcr.Text, 1, 2) & " " _
                                         & Mid(txtKcr.Text, 3, 2) & " " _
                                         & Mid(txtKcr.Text, 5, 2) & " " _
                                         & Mid(txtKcr.Text, 7, 2) & " " _
                                         & Mid(txtKcr.Text, 9, 2) & " " _
                                         & Mid(txtKcr.Text, 11, 2) & " " _
                                         & Mid(txtKcr.Text, 13, 2) & " " _
                                         & Mid(txtKcr.Text, 15, 2) & " " _
                                         & Mid(txtKcr.Text, 17, 2) & " " _
                                         & Mid(txtKcr.Text, 19, 2)
     
     lst_Log.AddItem "    <<" & Mid(txtKcr.Text, 21, 2) & " " _
                                         & Mid(txtKcr.Text, 23, 2) & " " _
                                         & Mid(txtKcr.Text, 25, 2) & " " _
                                         & Mid(txtKcr.Text, 27, 2) & " " _
                                         & Mid(txtKcr.Text, 29, 2) & " " _
                                         & Mid(txtKcr.Text, 31, 2)
    
    lst_Log.ListIndex = lst_Log.NewIndex

    RecvLen = 2

    If Send_APDU_SAM(SendBuff, 27, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
       Exit Sub
    End If
                                             
    
    
    
    '6th Master key, key ID=86, key type=03, int/ext authenticate, usage counter = FF FF
    ReDim SendBuff(0 To 26) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    
    SendBuff(0) = &H0
    SendBuff(1) = &HE2
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H16
    SendBuff(5) = &H86 'Key ID
    SendBuff(6) = &H3
    SendBuff(7) = &HFF
    SendBuff(8) = &HFF
    SendBuff(9) = &H88
    SendBuff(10) = &H0
    SendBuff(11) = CInt("&H" & Mid(txtKcf.Text, 1, 2))
    SendBuff(12) = CInt("&H" & Mid(txtKcf.Text, 3, 2))
    SendBuff(13) = CInt("&H" & Mid(txtKcf.Text, 5, 2))
    SendBuff(14) = CInt("&H" & Mid(txtKcf.Text, 7, 2))
    SendBuff(15) = CInt("&H" & Mid(txtKcf.Text, 9, 2))
    SendBuff(16) = CInt("&H" & Mid(txtKcf.Text, 11, 2))
    SendBuff(17) = CInt("&H" & Mid(txtKcf.Text, 13, 2))
    SendBuff(18) = CInt("&H" & Mid(txtKcf.Text, 15, 2))
    SendBuff(19) = CInt("&H" & Mid(txtKcf.Text, 17, 2))
    SendBuff(20) = CInt("&H" & Mid(txtKcf.Text, 19, 2))
    SendBuff(21) = CInt("&H" & Mid(txtKcf.Text, 21, 2))
    SendBuff(22) = CInt("&H" & Mid(txtKcf.Text, 23, 2))
    SendBuff(23) = CInt("&H" & Mid(txtKcf.Text, 25, 2))
    SendBuff(24) = CInt("&H" & Mid(txtKcf.Text, 27, 2))
    SendBuff(25) = CInt("&H" & Mid(txtKcf.Text, 29, 2))
    SendBuff(26) = CInt("&H" & Mid(txtKcf.Text, 31, 2))
    
    lst_Log.AddItem "SAM < 00 E2 00 00 16"
    lst_Log.AddItem "    <<86 03 FF FF 88 0 " _
                                         & Mid(txtKcf.Text, 1, 2) & " " _
                                         & Mid(txtKcf.Text, 3, 2) & " " _
                                         & Mid(txtKcf.Text, 5, 2) & " " _
                                         & Mid(txtKcf.Text, 7, 2) & " " _
                                         & Mid(txtKcf.Text, 9, 2) & " " _
                                         & Mid(txtKcf.Text, 11, 2) & " " _
                                         & Mid(txtKcf.Text, 13, 2) & " " _
                                         & Mid(txtKcf.Text, 15, 2) & " " _
                                         & Mid(txtKcf.Text, 17, 2) & " " _
                                         & Mid(txtKcf.Text, 19, 2)
     
     lst_Log.AddItem "    <<" & Mid(txtKcf.Text, 21, 2) & " " _
                                         & Mid(txtKcf.Text, 23, 2) & " " _
                                         & Mid(txtKcf.Text, 25, 2) & " " _
                                         & Mid(txtKcf.Text, 27, 2) & " " _
                                         & Mid(txtKcf.Text, 29, 2) & " " _
                                         & Mid(txtKcf.Text, 31, 2)
    
    lst_Log.ListIndex = lst_Log.NewIndex

    RecvLen = 2

    If Send_APDU_SAM(SendBuff, 27, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
       Exit Sub
    End If
                                             
    
    
    '7th Master key, key ID=87, key type=03, int/ext authenticate, usage counter = FF FF
    ReDim SendBuff(0 To 26) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    
    SendBuff(0) = &H0
    SendBuff(1) = &HE2
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H16
    SendBuff(5) = &H87 'Key ID
    SendBuff(6) = &H3
    SendBuff(7) = &HFF
    SendBuff(8) = &HFF
    SendBuff(9) = &H88
    SendBuff(10) = &H0
    SendBuff(11) = CInt("&H" & Mid(txtKrd.Text, 1, 2))
    SendBuff(12) = CInt("&H" & Mid(txtKrd.Text, 3, 2))
    SendBuff(13) = CInt("&H" & Mid(txtKrd.Text, 5, 2))
    SendBuff(14) = CInt("&H" & Mid(txtKrd.Text, 7, 2))
    SendBuff(15) = CInt("&H" & Mid(txtKrd.Text, 9, 2))
    SendBuff(16) = CInt("&H" & Mid(txtKrd.Text, 11, 2))
    SendBuff(17) = CInt("&H" & Mid(txtKrd.Text, 13, 2))
    SendBuff(18) = CInt("&H" & Mid(txtKrd.Text, 15, 2))
    SendBuff(19) = CInt("&H" & Mid(txtKrd.Text, 17, 2))
    SendBuff(20) = CInt("&H" & Mid(txtKrd.Text, 19, 2))
    SendBuff(21) = CInt("&H" & Mid(txtKrd.Text, 21, 2))
    SendBuff(22) = CInt("&H" & Mid(txtKrd.Text, 23, 2))
    SendBuff(23) = CInt("&H" & Mid(txtKrd.Text, 25, 2))
    SendBuff(24) = CInt("&H" & Mid(txtKrd.Text, 27, 2))
    SendBuff(25) = CInt("&H" & Mid(txtKrd.Text, 29, 2))
    SendBuff(26) = CInt("&H" & Mid(txtKrd.Text, 31, 2))

    lst_Log.AddItem "SAM < 00 E2 00 00 16"
    lst_Log.AddItem "    <<87 03 FF FF 88 00 " _
                                         & Mid(txtKrd.Text, 1, 2) & " " _
                                         & Mid(txtKrd.Text, 3, 2) & " " _
                                         & Mid(txtKrd.Text, 5, 2) & " " _
                                         & Mid(txtKrd.Text, 7, 2) & " " _
                                         & Mid(txtKrd.Text, 9, 2) & " " _
                                         & Mid(txtKrd.Text, 11, 2) & " " _
                                         & Mid(txtKrd.Text, 13, 2) & " " _
                                         & Mid(txtKrd.Text, 15, 2) & " " _
                                         & Mid(txtKrd.Text, 17, 2) & " " _
                                         & Mid(txtKrd.Text, 19, 2)
     
     lst_Log.AddItem "    <<" & Mid(txtKrd.Text, 21, 2) & " " _
                                         & Mid(txtKrd.Text, 23, 2) & " " _
                                         & Mid(txtKrd.Text, 25, 2) & " " _
                                         & Mid(txtKrd.Text, 27, 2) & " " _
                                         & Mid(txtKrd.Text, 29, 2) & " " _
                                         & Mid(txtKrd.Text, 31, 2)
    
    lst_Log.ListIndex = lst_Log.NewIndex

    RecvLen = 2

    If Send_APDU_SAM(SendBuff, 27, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
            Exit Sub
    End If
                                             
    
    
    
End Sub



Private Sub cmd_ListReaders_SAM_Click()
    
    'Initialize List of Available Readers
    
    Dim sReaderList As String * 256
    Dim ReaderCount As Long
    Dim sReaderGroup As String
    
    sReaderList = String(255, vbNullChar)
    
    cmbSAM.Clear
    
    ReaderCount = 255
       
    ' 1. Establish context and obtain G_hContext handle
    G_retCode = SCardEstablishContext(SCARD_SCOPE_USER, 0, 0, G_hContext)
        
    If G_retCode <> SCARD_S_SUCCESS Then
        lst_Log.AddItem "SAM SCardEstablisG_hContext Error: " & GetScardErrMsg(G_retCode)
        lst_Log.ListIndex = lst_Log.NewIndex
        Exit Sub
    Else
        lst_Log.AddItem "SAM SCardEstablisG_hContext Success"
        lst_Log.ListIndex = lst_Log.NewIndex
    End If
    
    ' 2. List PC/SC card readers installed in the system
    G_retCode = SCardListReaders(G_hContext, "", sReaderList, ReaderCount)
    
    If G_retCode <> SCARD_S_SUCCESS Then
        lst_Log.AddItem "SAM SCardListReaders Error: " & GetScardErrMsg(G_retCode)
        lst_Log.ListIndex = lst_Log.NewIndex
        Exit Sub
    Else
        lst_Log.AddItem "SAM SCardListReaders Success"
        lst_Log.ListIndex = lst_Log.NewIndex
    End If
    
    'Load Available Readers
    LoadListToControl cmbSAM, sReaderList
      
    If cmbSAM.ListCount > 0 Then
        cmbSAM.ListIndex = 0
        cmd_Connect_SAM.Enabled = True
    Else
        lst_Log.AddItem "No Reader Found"
        lst_Log.ListIndex = lst_Log.NewIndex
    End If
    
End Sub

Private Sub cmd_ListReaders_SLT_Click()
    
    'Initialize List of Available Readers
    
    Dim sReaderList As String * 256
    Dim ReaderCount As Long
    Dim sReaderGroup As String
    
    sReaderList = String(255, vbNullChar)
    
    cmbSLT.Clear
    
    ReaderCount = 255
       
    ' 1. Establish context and obtain G_hContext handle
    G_retCode = SCardEstablishContext(SCARD_SCOPE_USER, 0, 0, G_hContext)
        
    If G_retCode <> SCARD_S_SUCCESS Then
        lst_Log.AddItem "SCardEstablisG_hContext Error: " & GetScardErrMsg(G_retCode)
        lst_Log.ListIndex = lst_Log.NewIndex
        Exit Sub
    Else
        lst_Log.AddItem "SCardEstablisG_hContext Success"
        lst_Log.ListIndex = lst_Log.NewIndex
    End If
    
    ' 2. List PC/SC card readers installed in the system
    G_retCode = SCardListReaders(G_hContext, "", sReaderList, ReaderCount)
    
    If G_retCode <> SCARD_S_SUCCESS Then
        lst_Log.AddItem "SCardListReaders Error: " & GetScardErrMsg(G_retCode)
        lst_Log.ListIndex = lst_Log.NewIndex
        Exit Sub
    Else
        lst_Log.AddItem "SCardListReaders Success"
        lst_Log.ListIndex = lst_Log.NewIndex
    End If
    
    'Load Available Readers
    LoadListToControl cmbSLT, sReaderList
      
    If cmbSLT.ListCount > 0 Then
        cmbSLT.ListIndex = 0
        cmd_Connect_SLT.Enabled = True
    Else
        lst_Log.AddItem "Reader Not Found"
        lst_Log.ListIndex = lst_Log.NewIndex
    End If
    

End Sub

Private Sub cmd_SaveKeys_Click()
Dim SendBuff() As Byte
Dim RecvBuff() As Byte
Dim RecvLen As Long

    'Check User PIN
    If Not CheckInput(txt_PIN, 16) Then Exit Sub
    
    'Update Personalization File (FF02)
    
    'Select FF02
    ReDim SendBuff(0 To 6) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HA4
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H2
    SendBuff(5) = &HFF
    SendBuff(6) = &H2
    
    lst_Log.AddItem "MCU < 80 A4 00 00 02"
    lst_Log.AddItem "    <<FF 02"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 7, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    

    'Submit Default IC
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H20
    SendBuff(2) = &H7
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    SendBuff(5) = &H41
    SendBuff(6) = &H43
    SendBuff(7) = &H4F
    SendBuff(8) = &H53
    SendBuff(9) = &H54
    SendBuff(10) = &H45
    SendBuff(11) = &H53
    SendBuff(12) = &H54
    
    lst_Log.AddItem "MCU < 80 20 07 00 08"
    lst_Log.AddItem "    <<41 43 4F 53 54 45 53 54"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If

    
    'Update FF02 record 0
    ReDim SendBuff(0 To 8) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HD2
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H4
    
    If G_AlgoRef = 0 Then
        SendBuff(5) = &HFF ' INQ_AUT, TRNS_AUT, REV_DEB, DEB_PIN, DEB_MAC, Account, 3-DES, PIN_ALT Enabled
    Else
        SendBuff(5) = &HFD ' INQ_AUT, TRNS_AUT, REV_DEB, DEB_PIN, DEB_MAC, Account, 3-DES, PIN_ALT Enabled
    End If
    
    SendBuff(6) = &H40 'PIN was encrypted and the PIN should be submitted in the submit code command must be encrypted with the current session key.
    SendBuff(7) = &H0 'No User File Created
    SendBuff(8) = &H0
    
    
    
    lst_Log.AddItem "MCU < 80 D2 00 00 04"
    lst_Log.AddItem "    <<" & Right("00" & Hex(SendBuff(5)), 2) & " 40 00 00"
                             
                             
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If


    'Reset
    cmd_Connect_SLT_Click

    

    'Update Card Keys (FF03)
    
    'Select FF03
    ReDim SendBuff(0 To 6) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HA4
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H2
    SendBuff(5) = &HFF
    SendBuff(6) = &H3
    
    lst_Log.AddItem "MCU < 80 A4 00 00 02"
    lst_Log.AddItem "    <<FF 03"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 7, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    'Submit Default IC
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H20
    SendBuff(2) = &H7
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    SendBuff(5) = &H41
    SendBuff(6) = &H43
    SendBuff(7) = &H4F
    SendBuff(8) = &H53
    SendBuff(9) = &H54
    SendBuff(10) = &H45
    SendBuff(11) = &H53
    SendBuff(12) = &H54
    
    lst_Log.AddItem "MCU < 80 20 07 00 08"
    lst_Log.AddItem "    <<41 43 4F 53 54 45 53 54"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If

    'Update FF03 record 0 (IC)
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HD2
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(lbl_IC.Caption, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(lbl_IC.Caption, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(lbl_IC.Caption, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(lbl_IC.Caption, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(lbl_IC.Caption, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(lbl_IC.Caption, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(lbl_IC.Caption, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(lbl_IC.Caption, 15, 2))
    
    
    lst_Log.AddItem "MCU < 80 D2 00 00 08"
    lst_Log.AddItem "    <<" & Mid(lbl_IC.Caption, 1, 2) & " " _
                             & Mid(lbl_IC.Caption, 3, 2) & " " _
                             & Mid(lbl_IC.Caption, 5, 2) & " " _
                             & Mid(lbl_IC.Caption, 7, 2) & " " _
                             & Mid(lbl_IC.Caption, 9, 2) & " " _
                             & Mid(lbl_IC.Caption, 11, 2) & " " _
                             & Mid(lbl_IC.Caption, 13, 2) & " " _
                             & Mid(lbl_IC.Caption, 15, 2)
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    'Update FF03 record 1 (PIN)
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HD2
    SendBuff(2) = &H1
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(txt_PIN.Text, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(txt_PIN.Text, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(txt_PIN.Text, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(txt_PIN.Text, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(txt_PIN.Text, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(txt_PIN.Text, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(txt_PIN.Text, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(txt_PIN.Text, 15, 2))
    
    
    lst_Log.AddItem "MCU < 80 D2 01 00 08"
    lst_Log.AddItem "    <<" & Mid(txt_PIN.Text, 1, 2) & " " _
                             & Mid(txt_PIN.Text, 3, 2) & " " _
                             & Mid(txt_PIN.Text, 5, 2) & " " _
                             & Mid(txt_PIN.Text, 7, 2) & " " _
                             & Mid(txt_PIN.Text, 9, 2) & " " _
                             & Mid(txt_PIN.Text, 11, 2) & " " _
                             & Mid(txt_PIN.Text, 13, 2) & " " _
                             & Mid(txt_PIN.Text, 15, 2)
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    'Update FF03 record 2 (Kc)
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HD2
    SendBuff(2) = &H2
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(lbl_Kc.Caption, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(lbl_Kc.Caption, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(lbl_Kc.Caption, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(lbl_Kc.Caption, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(lbl_Kc.Caption, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(lbl_Kc.Caption, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(lbl_Kc.Caption, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(lbl_Kc.Caption, 15, 2))
    
    
    lst_Log.AddItem "MCU < 80 D2 02 00 08"
    lst_Log.AddItem "    <<" & Mid(lbl_Kc.Caption, 1, 2) & " " _
                             & Mid(lbl_Kc.Caption, 3, 2) & " " _
                             & Mid(lbl_Kc.Caption, 5, 2) & " " _
                             & Mid(lbl_Kc.Caption, 7, 2) & " " _
                             & Mid(lbl_Kc.Caption, 9, 2) & " " _
                             & Mid(lbl_Kc.Caption, 11, 2) & " " _
                             & Mid(lbl_Kc.Caption, 13, 2) & " " _
                             & Mid(lbl_Kc.Caption, 15, 2)
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    If G_AlgoRef = 0 Then
        'If Algorithm Reference = 3DES Update FF03 record 0x0C Right Half (Kc)
        ReDim SendBuff(0 To 12) As Byte
        ReDim RecvBuff(0 To 1) As Byte
    
        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &HC
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_r_Kc.Caption, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_r_Kc.Caption, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_r_Kc.Caption, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_r_Kc.Caption, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_r_Kc.Caption, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_r_Kc.Caption, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_r_Kc.Caption, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_r_Kc.Caption, 15, 2))
        
        
        lst_Log.AddItem "MCU < 80 D2 0C 00 08"
        lst_Log.AddItem "    <<" & Mid(lbl_r_Kc.Caption, 1, 2) & " " _
                                 & Mid(lbl_r_Kc.Caption, 3, 2) & " " _
                                 & Mid(lbl_r_Kc.Caption, 5, 2) & " " _
                                 & Mid(lbl_r_Kc.Caption, 7, 2) & " " _
                                 & Mid(lbl_r_Kc.Caption, 9, 2) & " " _
                                 & Mid(lbl_r_Kc.Caption, 11, 2) & " " _
                                 & Mid(lbl_r_Kc.Caption, 13, 2) & " " _
                                 & Mid(lbl_r_Kc.Caption, 15, 2)
                                 
        lst_Log.ListIndex = lst_Log.NewIndex
        
        RecvLen = 2
        
        If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
               If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                    lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
               End If
        Else
               Exit Sub
        End If
    End If
    
    
    'Update FF03 record 3 (Kt)
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HD2
    SendBuff(2) = &H3
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(lbl_Kt.Caption, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(lbl_Kt.Caption, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(lbl_Kt.Caption, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(lbl_Kt.Caption, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(lbl_Kt.Caption, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(lbl_Kt.Caption, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(lbl_Kt.Caption, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(lbl_Kt.Caption, 15, 2))
    
    
    lst_Log.AddItem "MCU < 80 D2 03 00 08"
    lst_Log.AddItem "    <<" & Mid(lbl_Kt.Caption, 1, 2) & " " _
                             & Mid(lbl_Kt.Caption, 3, 2) & " " _
                             & Mid(lbl_Kt.Caption, 5, 2) & " " _
                             & Mid(lbl_Kt.Caption, 7, 2) & " " _
                             & Mid(lbl_Kt.Caption, 9, 2) & " " _
                             & Mid(lbl_Kt.Caption, 11, 2) & " " _
                             & Mid(lbl_Kt.Caption, 13, 2) & " " _
                             & Mid(lbl_Kt.Caption, 15, 2)
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    
    If G_AlgoRef = 0 Then
        'If Algorithm Reference = 3DES Update FF03 record 0x0D Right Half (Kt)
        ReDim SendBuff(0 To 12) As Byte
        ReDim RecvBuff(0 To 1) As Byte
    
        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &HD
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_r_Kt.Caption, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_r_Kt.Caption, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_r_Kt.Caption, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_r_Kt.Caption, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_r_Kt.Caption, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_r_Kt.Caption, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_r_Kt.Caption, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_r_Kt.Caption, 15, 2))
        
        
        lst_Log.AddItem "MCU < 80 D2 0D 00 08"
        lst_Log.AddItem "    <<" & Mid(lbl_r_Kt.Caption, 1, 2) & " " _
                                 & Mid(lbl_r_Kt.Caption, 3, 2) & " " _
                                 & Mid(lbl_r_Kt.Caption, 5, 2) & " " _
                                 & Mid(lbl_r_Kt.Caption, 7, 2) & " " _
                                 & Mid(lbl_r_Kt.Caption, 9, 2) & " " _
                                 & Mid(lbl_r_Kt.Caption, 11, 2) & " " _
                                 & Mid(lbl_r_Kt.Caption, 13, 2) & " " _
                                 & Mid(lbl_r_Kt.Caption, 15, 2)
                                 
        lst_Log.ListIndex = lst_Log.NewIndex
        
        RecvLen = 2
        
        If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
               If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                    lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
               End If
        Else
               Exit Sub
        End If
    End If
    
    
    'Select FF06
    ReDim SendBuff(0 To 6) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HA4
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H2
    SendBuff(5) = &HFF
    SendBuff(6) = &H6
    
    lst_Log.AddItem "MCU < 80 A4 00 00 02"
    lst_Log.AddItem "    <<FF 06"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 7, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    

    'Update FF06 record 0 (Kd)
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HD2
    If G_AlgoRef = 0 Then
        SendBuff(2) = &H4
    Else
        SendBuff(2) = &H0
    End If
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(lbl_Kd.Caption, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(lbl_Kd.Caption, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(lbl_Kd.Caption, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(lbl_Kd.Caption, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(lbl_Kd.Caption, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(lbl_Kd.Caption, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(lbl_Kd.Caption, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(lbl_Kd.Caption, 15, 2))
    
    
    lst_Log.AddItem "MCU < 80 D2 " & Right("00" & Hex(SendBuff(2)), 2) & " 00 08"
    lst_Log.AddItem "    <<" & Mid(lbl_Kd.Caption, 1, 2) & " " _
                             & Mid(lbl_Kd.Caption, 3, 2) & " " _
                             & Mid(lbl_Kd.Caption, 5, 2) & " " _
                             & Mid(lbl_Kd.Caption, 7, 2) & " " _
                             & Mid(lbl_Kd.Caption, 9, 2) & " " _
                             & Mid(lbl_Kd.Caption, 11, 2) & " " _
                             & Mid(lbl_Kd.Caption, 13, 2) & " " _
                             & Mid(lbl_Kd.Caption, 15, 2)
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    'Update FF06 record 1 (Kcr)
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HD2
    
    If G_AlgoRef = 0 Then
        SendBuff(2) = &H5
    Else
        SendBuff(2) = &H1
    End If
    
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(lbl_Kcr.Caption, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(lbl_Kcr.Caption, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(lbl_Kcr.Caption, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(lbl_Kcr.Caption, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(lbl_Kcr.Caption, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(lbl_Kcr.Caption, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(lbl_Kcr.Caption, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(lbl_Kcr.Caption, 15, 2))
    
    
    lst_Log.AddItem "MCU < 80 D2 " & Right("00" & Hex(SendBuff(2)), 2) & " 00 08"
    lst_Log.AddItem "    <<" & Mid(lbl_Kcr.Caption, 1, 2) & " " _
                             & Mid(lbl_Kcr.Caption, 3, 2) & " " _
                             & Mid(lbl_Kcr.Caption, 5, 2) & " " _
                             & Mid(lbl_Kcr.Caption, 7, 2) & " " _
                             & Mid(lbl_Kcr.Caption, 9, 2) & " " _
                             & Mid(lbl_Kcr.Caption, 11, 2) & " " _
                             & Mid(lbl_Kcr.Caption, 13, 2) & " " _
                             & Mid(lbl_Kcr.Caption, 15, 2)
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    'Update FF06 record 2 (Kcf)
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HD2
    If G_AlgoRef = 0 Then
        SendBuff(2) = &H6
    Else
        SendBuff(2) = &H2
    End If
    
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(lbl_Kcf.Caption, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(lbl_Kcf.Caption, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(lbl_Kcf.Caption, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(lbl_Kcf.Caption, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(lbl_Kcf.Caption, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(lbl_Kcf.Caption, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(lbl_Kcf.Caption, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(lbl_Kcf.Caption, 15, 2))
    
    
    lst_Log.AddItem "MCU < 80 D2 " & Right("00" & Hex(SendBuff(2)), 2) & " 00 08"
    lst_Log.AddItem "    <<" & Mid(lbl_Kcf.Caption, 1, 2) & " " _
                             & Mid(lbl_Kcf.Caption, 3, 2) & " " _
                             & Mid(lbl_Kcf.Caption, 5, 2) & " " _
                             & Mid(lbl_Kcf.Caption, 7, 2) & " " _
                             & Mid(lbl_Kcf.Caption, 9, 2) & " " _
                             & Mid(lbl_Kcf.Caption, 11, 2) & " " _
                             & Mid(lbl_Kcf.Caption, 13, 2) & " " _
                             & Mid(lbl_Kcf.Caption, 15, 2)
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
     'Update FF06 record 3 (Krd)
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HD2
    If G_AlgoRef = 0 Then
        SendBuff(2) = &H7
    Else
        SendBuff(2) = &H3
    End If
    
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(lbl_Krd.Caption, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(lbl_Krd.Caption, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(lbl_Krd.Caption, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(lbl_Krd.Caption, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(lbl_Krd.Caption, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(lbl_Krd.Caption, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(lbl_Krd.Caption, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(lbl_Krd.Caption, 15, 2))
    
    lst_Log.AddItem "MCU < 80 D2 " & Right("00" & Hex(SendBuff(2)), 2) & " 00 08"
    lst_Log.AddItem "    <<" & Mid(lbl_Krd.Caption, 1, 2) & " " _
                             & Mid(lbl_Krd.Caption, 3, 2) & " " _
                             & Mid(lbl_Krd.Caption, 5, 2) & " " _
                             & Mid(lbl_Krd.Caption, 7, 2) & " " _
                             & Mid(lbl_Krd.Caption, 9, 2) & " " _
                             & Mid(lbl_Krd.Caption, 11, 2) & " " _
                             & Mid(lbl_Krd.Caption, 13, 2) & " " _
                             & Mid(lbl_Krd.Caption, 15, 2)
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    
    
    
    'If Algorithm Reference = 3DES then update Right Half of the Keys
    If G_AlgoRef = 0 Then
    
        'Update FF06 record 0 (Kd) right half
        ReDim SendBuff(0 To 12) As Byte
        ReDim RecvBuff(0 To 1) As Byte
    
        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_r_Kd.Caption, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_r_Kd.Caption, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_r_Kd.Caption, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_r_Kd.Caption, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_r_Kd.Caption, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_r_Kd.Caption, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_r_Kd.Caption, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_r_Kd.Caption, 15, 2))
        
        
        lst_Log.AddItem "MCU < 80 D2 00 00 08"
        lst_Log.AddItem "    <<" & Mid(lbl_r_Kd.Caption, 1, 2) & " " _
                                 & Mid(lbl_r_Kd.Caption, 3, 2) & " " _
                                 & Mid(lbl_r_Kd.Caption, 5, 2) & " " _
                                 & Mid(lbl_r_Kd.Caption, 7, 2) & " " _
                                 & Mid(lbl_r_Kd.Caption, 9, 2) & " " _
                                 & Mid(lbl_r_Kd.Caption, 11, 2) & " " _
                                 & Mid(lbl_r_Kd.Caption, 13, 2) & " " _
                                 & Mid(lbl_r_Kd.Caption, 15, 2)
                                 
        lst_Log.ListIndex = lst_Log.NewIndex
        
        RecvLen = 2
        
        If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
               If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                    lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
               End If
        Else
               Exit Sub
        End If
        
        
        'Update FF06 record 1 (Kcr) right half
        ReDim SendBuff(0 To 12) As Byte
        ReDim RecvBuff(0 To 1) As Byte
    
        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &H1
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_r_Kcr.Caption, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_r_Kcr.Caption, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_r_Kcr.Caption, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_r_Kcr.Caption, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_r_Kcr.Caption, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_r_Kcr.Caption, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_r_Kcr.Caption, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_r_Kcr.Caption, 15, 2))
        
        
        lst_Log.AddItem "MCU < 80 D2 01 00 08"
        lst_Log.AddItem "    <<" & Mid(lbl_r_Kcr.Caption, 1, 2) & " " _
                                 & Mid(lbl_r_Kcr.Caption, 3, 2) & " " _
                                 & Mid(lbl_r_Kcr.Caption, 5, 2) & " " _
                                 & Mid(lbl_r_Kcr.Caption, 7, 2) & " " _
                                 & Mid(lbl_r_Kcr.Caption, 9, 2) & " " _
                                 & Mid(lbl_r_Kcr.Caption, 11, 2) & " " _
                                 & Mid(lbl_r_Kcr.Caption, 13, 2) & " " _
                                 & Mid(lbl_r_Kcr.Caption, 15, 2)
                                 
        lst_Log.ListIndex = lst_Log.NewIndex
        
        RecvLen = 2
        
        If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
               If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                    lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
               End If
        Else
               Exit Sub
        End If
        
        
        'Update FF06 record 2 (Kcf) right half
        ReDim SendBuff(0 To 12) As Byte
        ReDim RecvBuff(0 To 1) As Byte
    
        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &H2
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_r_Kcf.Caption, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_r_Kcf.Caption, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_r_Kcf.Caption, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_r_Kcf.Caption, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_r_Kcf.Caption, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_r_Kcf.Caption, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_r_Kcf.Caption, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_r_Kcf.Caption, 15, 2))
        
        
        lst_Log.AddItem "MCU < 80 D2 02 00 08"
        lst_Log.AddItem "    <<" & Mid(lbl_r_Kcf.Caption, 1, 2) & " " _
                                 & Mid(lbl_r_Kcf.Caption, 3, 2) & " " _
                                 & Mid(lbl_r_Kcf.Caption, 5, 2) & " " _
                                 & Mid(lbl_r_Kcf.Caption, 7, 2) & " " _
                                 & Mid(lbl_r_Kcf.Caption, 9, 2) & " " _
                                 & Mid(lbl_r_Kcf.Caption, 11, 2) & " " _
                                 & Mid(lbl_r_Kcf.Caption, 13, 2) & " " _
                                 & Mid(lbl_r_Kcf.Caption, 15, 2)
                                 
        lst_Log.ListIndex = lst_Log.NewIndex
        
        RecvLen = 2
        
        If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
               If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                    lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
               End If
        Else
               Exit Sub
        End If
        
        
         'Update FF06 record 3 (Krd) right half
        ReDim SendBuff(0 To 12) As Byte
        ReDim RecvBuff(0 To 1) As Byte
    
        SendBuff(0) = &H80
        SendBuff(1) = &HD2
        SendBuff(2) = &H3
        SendBuff(3) = &H0
        SendBuff(4) = &H8
        SendBuff(5) = CInt("&H" & Mid(lbl_r_Krd.Caption, 1, 2))
        SendBuff(6) = CInt("&H" & Mid(lbl_r_Krd.Caption, 3, 2))
        SendBuff(7) = CInt("&H" & Mid(lbl_r_Krd.Caption, 5, 2))
        SendBuff(8) = CInt("&H" & Mid(lbl_r_Krd.Caption, 7, 2))
        SendBuff(9) = CInt("&H" & Mid(lbl_r_Krd.Caption, 9, 2))
        SendBuff(10) = CInt("&H" & Mid(lbl_r_Krd.Caption, 11, 2))
        SendBuff(11) = CInt("&H" & Mid(lbl_r_Krd.Caption, 13, 2))
        SendBuff(12) = CInt("&H" & Mid(lbl_r_Krd.Caption, 15, 2))
        
        lst_Log.AddItem "MCU < 80 D2 03 00 08"
        lst_Log.AddItem "    <<" & Mid(lbl_r_Krd.Caption, 1, 2) & " " _
                                 & Mid(lbl_r_Krd.Caption, 3, 2) & " " _
                                 & Mid(lbl_r_Krd.Caption, 5, 2) & " " _
                                 & Mid(lbl_r_Krd.Caption, 7, 2) & " " _
                                 & Mid(lbl_r_Krd.Caption, 9, 2) & " " _
                                 & Mid(lbl_r_Krd.Caption, 11, 2) & " " _
                                 & Mid(lbl_r_Krd.Caption, 13, 2) & " " _
                                 & Mid(lbl_r_Krd.Caption, 15, 2)
                                 
        lst_Log.ListIndex = lst_Log.NewIndex
        
        RecvLen = 2
        
        If Send_APDU_SLT(SendBuff, 13, RecvLen, RecvBuff) = True Then
               If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                    lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Exit Sub
               Else
                    lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
               End If
        Else
               Exit Sub
        End If
    
    End If
    
    
    'Select FF05
    ReDim SendBuff(0 To 6) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HA4
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H2
    SendBuff(5) = &HFF
    SendBuff(6) = &H5
    
    lst_Log.AddItem "MCU < 80 A4 00 00 02"
    lst_Log.AddItem "    <<FF 05"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 7, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    

    'Initialize FF05 Account File
    ReDim SendBuff(0 To 8) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HD2
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H4
    SendBuff(5) = &H0
    SendBuff(6) = &H0
    SendBuff(7) = &H0
    SendBuff(8) = &H0
    
    
    
    
    lst_Log.AddItem "MCU < 80 D2 " & Right("00" & Hex(SendBuff(2)), 2) & " 00 04"
    lst_Log.AddItem "    <<00 00 00 00"
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    SendBuff(0) = &H80
    SendBuff(1) = &HD2
    SendBuff(2) = &H1
    SendBuff(3) = &H0
    SendBuff(4) = &H4
    SendBuff(5) = &H0
    SendBuff(6) = &H0
    SendBuff(7) = &H1
    SendBuff(8) = &H0
    
    
    
    
    lst_Log.AddItem "MCU < 80 D2 " & Right("00" & Hex(SendBuff(2)), 2) & " 00 04"
    lst_Log.AddItem "    <<00 00 01 00"
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    SendBuff(0) = &H80
    SendBuff(1) = &HD2
    SendBuff(2) = &H2
    SendBuff(3) = &H0
    SendBuff(4) = &H4
    SendBuff(5) = &H0
    SendBuff(6) = &H0
    SendBuff(7) = &H0
    SendBuff(8) = &H0
    
    
    
    
    lst_Log.AddItem "MCU < 80 D2 " & Right("00" & Hex(SendBuff(2)), 2) & " 00 04"
    lst_Log.AddItem "    <<00 00 00 00"
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    
    SendBuff(0) = &H80
    SendBuff(1) = &HD2
    SendBuff(2) = &H3
    SendBuff(3) = &H0
    SendBuff(4) = &H4
    SendBuff(5) = &H0
    SendBuff(6) = &H0
    SendBuff(7) = &H1
    SendBuff(8) = &H0
    
    
    
    
    lst_Log.AddItem "MCU < 80 D2 " & Right("00" & Hex(SendBuff(2)), 2) & " 00 04"
    lst_Log.AddItem "    <<00 00 01 00"
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    'Set Max Balance to 98 96 7F = 9,999,999
    SendBuff(0) = &H80
    SendBuff(1) = &HD2
    SendBuff(2) = &H4
    SendBuff(3) = &H0
    SendBuff(4) = &H4
    SendBuff(5) = &H98
    SendBuff(6) = &H96
    SendBuff(7) = &H7F
    SendBuff(8) = &H0
    
    
    
    
    lst_Log.AddItem "MCU < 80 D2 " & Right("00" & Hex(SendBuff(2)), 2) & " 00 04"
    lst_Log.AddItem "    <<98 96 7F 00"
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    SendBuff(0) = &H80
    SendBuff(1) = &HD2
    SendBuff(2) = &H5
    SendBuff(3) = &H0
    SendBuff(4) = &H4
    SendBuff(5) = &H0
    SendBuff(6) = &H0
    SendBuff(7) = &H0
    SendBuff(8) = &H0
    
    
    
    
    lst_Log.AddItem "MCU < 80 D2 " & Right("00" & Hex(SendBuff(2)), 2) & " 00 04"
    lst_Log.AddItem "    <<00 00 00 00"
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    
    SendBuff(0) = &H80
    SendBuff(1) = &HD2
    SendBuff(2) = &H6
    SendBuff(3) = &H0
    SendBuff(4) = &H4
    SendBuff(5) = &H0
    SendBuff(6) = &H0
    SendBuff(7) = &H0
    SendBuff(8) = &H0
    
    
    
    
    lst_Log.AddItem "MCU < 80 D2 " & Right("00" & Hex(SendBuff(2)), 2) & " 00 04"
    lst_Log.AddItem "    <<00 00 00 00"
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    SendBuff(0) = &H80
    SendBuff(1) = &HD2
    SendBuff(2) = &H7
    SendBuff(3) = &H0
    SendBuff(4) = &H4
    SendBuff(5) = &H0
    SendBuff(6) = &H0
    SendBuff(7) = &H0
    SendBuff(8) = &H0
    
    
    
    
    lst_Log.AddItem "MCU < 80 D2 " & Right("00" & Hex(SendBuff(2)), 2) & " 00 04"
    lst_Log.AddItem "    <<00 00 00 00"
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    
End Sub

Private Sub Form_Load()
    
    'Initialization
    opt_DES.Value = True
    G_AlgoRef = 1
    
End Sub

Private Sub opt_3DES_Click()
    
    'Set Algo Ref to 3DES
    G_AlgoRef = 0

End Sub

Private Sub opt_DES_Click()
    
    'Set Algo Ref to DES
    G_AlgoRef = 1
    
End Sub


Public Function Send_APDU_SAM(ByRef SendBuff() As Byte, ByVal SendLen As Long, ByRef RecvLen As Long, ByRef RecvBuff() As Byte) As Boolean

    'Send APDU to SAM Reader
     
    G_ioRequest.dwProtocol = G_Protocol
    G_ioRequest.cbPciLength = Len(G_ioRequest)
      
    G_retCode = SCardTransmit(G_hCardSAM, _
                              G_ioRequest, _
                              SendBuff(0), _
                              SendLen, _
                              G_ioRequest, _
                              RecvBuff(0), _
                              RecvLen)
      
      If G_retCode <> SCARD_S_SUCCESS Then
        lst_Log.AddItem "SAM SCardTransmit Error: " & GetScardErrMsg(G_retCode)
        lst_Log.ListIndex = lst_Log.NewIndex
        Exit Function
      End If
    
      Send_APDU_SAM = True

End Function

Public Function Send_APDU_SLT(ByRef SendBuff() As Byte, ByVal SendLen As Long, ByRef RecvLen As Long, ByRef RecvBuff() As Byte) As Boolean

    'Send APDU to Slot Reader
     
    G_ioRequest.dwProtocol = G_Protocol
    G_ioRequest.cbPciLength = Len(G_ioRequest)
      
    G_retCode = SCardTransmit(G_hCard, _
                              G_ioRequest, _
                              SendBuff(0), _
                              SendLen, _
                              G_ioRequest, _
                              RecvBuff(0), _
                              RecvLen)
      
      If G_retCode <> SCARD_S_SUCCESS Then
        lst_Log.AddItem "SCardTransmit Error: " & GetScardErrMsg(G_retCode)
        lst_Log.ListIndex = lst_Log.NewIndex
        Exit Function
      End If
    
      Send_APDU_SLT = True

End Function

Private Sub txt_IC_KeyPress(KeyAscii As Integer)
    
    'Verify Input
    KeyAscii = KeyVerify_Hex(KeyAscii)
    
End Sub

Private Sub txt_PIN_KeyPress(KeyAscii As Integer)
        
    'Verify Input
    KeyAscii = KeyVerify_Hex(KeyAscii)

End Sub

Private Sub txtKc_KeyPress(KeyAscii As Integer)
    
    'Verify Input
    KeyAscii = KeyVerify_Hex(KeyAscii)
    
End Sub

Private Sub txtKcf_KeyPress(KeyAscii As Integer)
    
    'Verify Input
    KeyAscii = KeyVerify_Hex(KeyAscii)
    
End Sub

Private Sub txtKcr_KeyPress(KeyAscii As Integer)
    
    'Verify Input
    KeyAscii = KeyVerify_Hex(KeyAscii)
    
End Sub

Private Sub txtKd_KeyPress(KeyAscii As Integer)
    
    'Verify Input
    KeyAscii = KeyVerify_Hex(KeyAscii)
    
End Sub

Private Sub txtKrd_KeyPress(KeyAscii As Integer)
    
    'Verify Input
    KeyAscii = KeyVerify_Hex(KeyAscii)
    
End Sub

Private Sub txtKt_KeyPress(KeyAscii As Integer)
    
    'Verify Input
    KeyAscii = KeyVerify_Hex(KeyAscii)
    
End Sub


Private Sub txtSAMGPIN_KeyPress(KeyAscii As Integer)
    
    'Verify Input
    KeyAscii = KeyVerify_Hex(KeyAscii)
    
End Sub


Public Function CheckInput(ByRef TextInput As TextBox, Optional ValidLen As Byte = 32) As Boolean
    
    'Check if null and check length if correct
    If TextInput.Text = "" Then
        lst_Log.AddItem "Input Required"
        lst_Log.ListIndex = lst_Log.NewIndex
        TextInput.SetFocus
        Exit Function
    End If
    
    If Len(TextInput.Text) <> ValidLen Then
        lst_Log.AddItem "Invalid Input Length"
        lst_Log.ListIndex = lst_Log.NewIndex
        TextInput.SetFocus
        Exit Function
    End If
    
    CheckInput = True
    
End Function


Public Function KeyVerify_Hex(ByVal Key As Integer) As Integer

'=========================================
'  Routine for Verifying the Inputed
'  Character 0-9 and A-F
'=========================================

    If Key >= 48 And Key <= 57 Or Key >= 65 And Key <= 70 Then
             Key = Key
    ElseIf Key >= 97 And Key <= 102 Then
             Key = (Asc(UCase(Chr(Key))))
    ElseIf Key = 8 Then
             Key = Key
    Else
             Key = 7
    End If

    KeyVerify_Hex = Key
    
End Function

