VERSION 5.00
Object = "{BDC217C8-ED16-11CD-956C-0000C04E4C0A}#1.1#0"; "TABCTL32.OCX"
Begin VB.Form frmMain 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "SAM Sample Usage"
   ClientHeight    =   7800
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   11340
   Icon            =   "frmMain.frx":0000
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   7800
   ScaleWidth      =   11340
   StartUpPosition =   2  'CenterScreen
   Begin TabDlg.SSTab SSTab1 
      Height          =   7530
      Left            =   120
      TabIndex        =   0
      Top             =   135
      Width           =   3855
      _ExtentX        =   6800
      _ExtentY        =   13282
      _Version        =   393216
      Tabs            =   2
      TabHeight       =   520
      TabCaption(0)   =   "Security"
      TabPicture(0)   =   "frmMain.frx":17A2
      Tab(0).ControlEnabled=   -1  'True
      Tab(0).Control(0)=   "Frame1"
      Tab(0).Control(0).Enabled=   0   'False
      Tab(0).ControlCount=   1
      TabCaption(1)   =   "Account"
      TabPicture(1)   =   "frmMain.frx":17BE
      Tab(1).ControlEnabled=   0   'False
      Tab(1).Control(0)=   "Frame2"
      Tab(1).Control(0).Enabled=   0   'False
      Tab(1).ControlCount=   1
      Begin VB.Frame Frame2 
         Height          =   7020
         Left            =   -74850
         TabIndex        =   25
         Top             =   360
         Width           =   3570
         Begin VB.TextBox txtMaxBalance 
            Alignment       =   1  'Right Justify
            Appearance      =   0  'Flat
            Enabled         =   0   'False
            Height          =   285
            Left            =   210
            MaxLength       =   16
            TabIndex        =   30
            Top             =   720
            Width           =   3195
         End
         Begin VB.CommandButton cmdDebit 
            Caption         =   "Debit"
            Height          =   480
            Left            =   210
            TabIndex        =   18
            Top             =   6165
            Width           =   1815
         End
         Begin VB.TextBox txtDebitAmount 
            Alignment       =   1  'Right Justify
            Appearance      =   0  'Flat
            Height          =   285
            Left            =   210
            MaxLength       =   7
            TabIndex        =   17
            Top             =   5715
            Width           =   3195
         End
         Begin VB.CommandButton cmdCredit 
            Caption         =   "Credit"
            Height          =   480
            Left            =   210
            TabIndex        =   16
            Top             =   4365
            Width           =   1815
         End
         Begin VB.TextBox txtCreditAmount 
            Alignment       =   1  'Right Justify
            Appearance      =   0  'Flat
            Height          =   285
            Left            =   210
            MaxLength       =   7
            TabIndex        =   15
            Top             =   3915
            Width           =   3195
         End
         Begin VB.CommandButton cmdInqAccount 
            Caption         =   "Inquire Account"
            Height          =   480
            Left            =   210
            TabIndex        =   14
            Top             =   1935
            Width           =   1815
         End
         Begin VB.TextBox txtInqAmount 
            Alignment       =   1  'Right Justify
            Appearance      =   0  'Flat
            Enabled         =   0   'False
            Height          =   285
            Left            =   210
            MaxLength       =   16
            TabIndex        =   26
            Top             =   1485
            Width           =   3195
         End
         Begin VB.Label Label8 
            Caption         =   "Max Balance"
            Height          =   285
            Left            =   210
            TabIndex        =   31
            Top             =   450
            Width           =   3150
         End
         Begin VB.Label Label7 
            Caption         =   "Debit Amount"
            Height          =   285
            Left            =   210
            TabIndex        =   29
            Top             =   5445
            Width           =   3150
         End
         Begin VB.Label Label6 
            Caption         =   "Credit Amount"
            Height          =   285
            Left            =   210
            TabIndex        =   28
            Top             =   3645
            Width           =   3150
         End
         Begin VB.Label Label5 
            Caption         =   "Balance"
            Height          =   285
            Left            =   210
            TabIndex        =   27
            Top             =   1215
            Width           =   3150
         End
      End
      Begin VB.Frame Frame1 
         Height          =   6900
         Left            =   195
         TabIndex        =   19
         Top             =   480
         Width           =   3495
         Begin VB.CommandButton cmd_SubmitPIN 
            Caption         =   "Submit PIN (ciphered)"
            Enabled         =   0   'False
            Height          =   480
            Left            =   165
            TabIndex        =   10
            Top             =   4965
            Width           =   1815
         End
         Begin VB.TextBox txtSAMGPIN 
            Appearance      =   0  'Flat
            Height          =   285
            Left            =   1695
            MaxLength       =   16
            TabIndex        =   7
            Top             =   3360
            Width           =   1665
         End
         Begin VB.OptionButton opt_DES 
            Caption         =   "DES"
            Height          =   195
            Left            =   180
            TabIndex        =   5
            Top             =   3030
            Width           =   1080
         End
         Begin VB.OptionButton opt_3DES 
            Caption         =   "3 DES"
            Height          =   195
            Left            =   1290
            TabIndex        =   6
            Top             =   3045
            Width           =   1620
         End
         Begin VB.TextBox txt_PIN 
            Appearance      =   0  'Flat
            Height          =   285
            Left            =   1695
            MaxLength       =   16
            TabIndex        =   9
            Top             =   4545
            Width           =   1650
         End
         Begin VB.CommandButton cmd_MutualAuth 
            Caption         =   "Mutual Authentication"
            Enabled         =   0   'False
            Height          =   480
            Left            =   165
            TabIndex        =   8
            Top             =   3780
            Width           =   1815
         End
         Begin VB.ComboBox cmbSLT 
            Height          =   315
            Left            =   180
            Style           =   2  'Dropdown List
            TabIndex        =   2
            Top             =   1140
            Width           =   3210
         End
         Begin VB.CommandButton cmd_Connect 
            Caption         =   "Connect"
            Enabled         =   0   'False
            Height          =   480
            Left            =   165
            TabIndex        =   4
            Top             =   2325
            Width           =   1815
         End
         Begin VB.ComboBox cmbSAM 
            Height          =   315
            Left            =   180
            Style           =   2  'Dropdown List
            TabIndex        =   3
            Top             =   1860
            Width           =   3210
         End
         Begin VB.CommandButton cmd_ListReaders 
            Caption         =   "List Readers"
            Height          =   480
            Left            =   195
            TabIndex        =   1
            Top             =   285
            Width           =   1815
         End
         Begin VB.TextBox txt_New_PIN 
            Appearance      =   0  'Flat
            Height          =   285
            Left            =   1695
            MaxLength       =   16
            TabIndex        =   11
            Top             =   5700
            Width           =   1650
         End
         Begin VB.CommandButton cmd_Change_PIN 
            Caption         =   "Change PIN (ciphered)"
            Enabled         =   0   'False
            Height          =   480
            Left            =   165
            TabIndex        =   12
            Top             =   6120
            Width           =   1815
         End
         Begin VB.Label Label18 
            Caption         =   "SAM GLOBAL PIN"
            Height          =   285
            Left            =   195
            TabIndex        =   24
            Top             =   3405
            Width           =   3150
         End
         Begin VB.Label Label3 
            Caption         =   "ACOS Card PIN"
            Height          =   285
            Left            =   240
            TabIndex        =   23
            Top             =   4590
            Width           =   3150
         End
         Begin VB.Label Label2 
            Caption         =   "Card Reader"
            Height          =   225
            Left            =   180
            TabIndex        =   22
            Top             =   870
            Width           =   1035
         End
         Begin VB.Label Label1 
            Caption         =   "SAM Reader"
            Height          =   225
            Left            =   180
            TabIndex        =   21
            Top             =   1620
            Width           =   1035
         End
         Begin VB.Label Label4 
            Caption         =   "ACOS New PIN"
            Height          =   285
            Left            =   240
            TabIndex        =   20
            Top             =   5745
            Width           =   3150
         End
      End
   End
   Begin VB.ListBox lst_Log 
      Appearance      =   0  'Flat
      Height          =   7440
      ItemData        =   "frmMain.frx":17DA
      Left            =   4080
      List            =   "frmMain.frx":17DC
      TabIndex        =   13
      TabStop         =   0   'False
      Top             =   225
      Width           =   7125
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'===================================================================================================
'
'   Project Name :  SAM_Sample_Usage
'
'   Company      :  Advanced Card Systems LTD.
'
'   Author       :  Richard C. Siman
'
'   Date         :  January 29, 2007
'
'   Description  :  This sample shows how ACOS SAM is use for Mutual Authentication and to perform
'                   encrypted PIN submission. And secure E-Purse Function.
'
'   Initial Step :  1.  Press List Readers.
'                   2.  Choose the SAM reader and ACOS card reader.
'                   3.  Press Connect.
'                   4.  Select Algorithm Reference to use (DES/3DES)
'                   5.  Enter SAM Global PIN. (PIN used in KeyManagement sample, SAM Initialization)
'                   6.  Press Mutual Authentication.
'                   7.  Enter ACOS Card PIN. (PIN used in KeyManagement sample, ACOS Card Initialization)
'                   8.  Press Submit PIN.
'                   9.  If you don't want to change your current PIN go to step 10.
'                       To changed current PIN, enter the desired new PIN and press Change PIN
'                   10. To check current card balance (e-purse) press Inquire Account.
'                   11. To credit amount to the card e-purse enter the amount to credit and press Credit.
'                   12. To dedit amount to the card e-purse enter the amount to dedit and press Dedit.
'
'   NOTE:
'                   Please note that this sample program assumes that the SAM and ACOS card were already
'                   initialized using KeyManagement Sample program.
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



Private Sub cmd_Connect_Click()
        
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
    
    cmd_MutualAuth.Enabled = True

End Sub

Private Sub cmd_ListReaders_Click()
    
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
    LoadListToControl cmbSLT, sReaderList
      
    If cmbSAM.ListCount > 0 And cmbSLT.ListCount > 0 Then
        cmbSAM.ListIndex = 0
        cmbSLT.ListIndex = 0
        cmd_Connect.Enabled = True
    Else
        lst_Log.AddItem "No Reader Found"
        lst_Log.ListIndex = lst_Log.NewIndex
    End If

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


Private Sub cmd_MutualAuth_Click()

Dim SendBuff() As Byte
Dim RecvBuff() As Byte
Dim RecvLen As Long
Dim SN, tempStr As String
Dim i As Integer
    
    
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
                
                SN = Replace(SN, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
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

    'Submit Issuer PIN (SAM Global PIN)
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
  
  
    'Diversify Kc
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H72
    SendBuff(2) = &H4
    SendBuff(3) = &H82
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(SN, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(SN, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(SN, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(SN, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(SN, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(SN, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(SN, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(SN, 15, 2))
    
    
    lst_Log.AddItem "SAM < 80 72 04 82 08"
    lst_Log.AddItem "    <<" & Mid(SN, 1, 2) & " " _
                             & Mid(SN, 3, 2) & " " _
                             & Mid(SN, 5, 2) & " " _
                             & Mid(SN, 7, 2) & " " _
                             & Mid(SN, 9, 2) & " " _
                             & Mid(SN, 11, 2) & " " _
                             & Mid(SN, 13, 2) & " " _
                             & Mid(SN, 15, 2)
    
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


    'Diversify Kt
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H72
    SendBuff(2) = &H3
    SendBuff(3) = &H83
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(SN, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(SN, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(SN, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(SN, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(SN, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(SN, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(SN, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(SN, 15, 2))
    
    
    lst_Log.AddItem "SAM < 80 72 03 83 08"
    lst_Log.AddItem "    <<" & Mid(SN, 1, 2) & " " _
                             & Mid(SN, 3, 2) & " " _
                             & Mid(SN, 5, 2) & " " _
                             & Mid(SN, 7, 2) & " " _
                             & Mid(SN, 9, 2) & " " _
                             & Mid(SN, 11, 2) & " " _
                             & Mid(SN, 13, 2) & " " _
                             & Mid(SN, 15, 2)
    
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


    'Get Challenge
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 9) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H84
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    
    lst_Log.AddItem "MCU < 80 84 00 00 08"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 10
    
    If Send_APDU_SLT(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve RNDc
                tempStr = ""
    
                For i = 0 To 7
                    tempStr = tempStr & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                
                lst_Log.AddItem "MCU >> " & tempStr
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                tempStr = Replace(tempStr, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    'Prepare ACOS authentication
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte
    
    SendBuff(0) = &H80
    SendBuff(1) = &H78
    
    If opt_DES.Value = True Then
        SendBuff(2) = &H1
    ElseIf opt_3DES.Value = True Then
        SendBuff(2) = &H0
    End If
    
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(tempStr, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(tempStr, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(tempStr, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(tempStr, 15, 2))
    
    
    lst_Log.AddItem "SAM < 80 78 " & Right("00" & Hex(SendBuff(2)), 2) & " 00 08"
    lst_Log.AddItem "    <<" & Mid(tempStr, 1, 2) & " " _
                             & Mid(tempStr, 3, 2) & " " _
                             & Mid(tempStr, 5, 2) & " " _
                             & Mid(tempStr, 7, 2) & " " _
                             & Mid(tempStr, 9, 2) & " " _
                             & Mid(tempStr, 11, 2) & " " _
                             & Mid(tempStr, 13, 2) & " " _
                             & Mid(tempStr, 15, 2)
    
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SAM(SendBuff, 13, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6110" Then
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
    
    
    'Get Response to get result + RNDt
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 17) As Byte

    SendBuff(0) = &H0
    SendBuff(1) = &HC0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H10
    
    lst_Log.AddItem "SAM < 00 C0 00 00 10"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = &H12
    
    If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(16)), 2) & Right("00" & Hex(RecvBuff(17)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(16)), 2) & Right("00" & Hex(RecvBuff(17)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve Result + RNDt
                tempStr = ""
    
                For i = 0 To 15
                    tempStr = tempStr & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                
                lst_Log.AddItem "SAM >> " & tempStr
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(16)), 2) & Right("00" & Hex(RecvBuff(17)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                tempStr = Replace(tempStr, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    'Authenticate
    ReDim SendBuff(0 To 20) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H82
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H10
    SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(tempStr, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(tempStr, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(tempStr, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(tempStr, 15, 2))
    SendBuff(13) = CInt("&H" & Mid(tempStr, 17, 2))
    SendBuff(14) = CInt("&H" & Mid(tempStr, 19, 2))
    SendBuff(15) = CInt("&H" & Mid(tempStr, 21, 2))
    SendBuff(16) = CInt("&H" & Mid(tempStr, 23, 2))
    SendBuff(17) = CInt("&H" & Mid(tempStr, 25, 2))
    SendBuff(18) = CInt("&H" & Mid(tempStr, 27, 2))
    SendBuff(19) = CInt("&H" & Mid(tempStr, 29, 2))
    SendBuff(20) = CInt("&H" & Mid(tempStr, 31, 2))
    
    
    lst_Log.AddItem "MCU < 80 82 00 00 10"
    lst_Log.AddItem "    <<" & Mid(tempStr, 1, 2) & " " _
                             & Mid(tempStr, 3, 2) & " " _
                             & Mid(tempStr, 5, 2) & " " _
                             & Mid(tempStr, 7, 2) & " " _
                             & Mid(tempStr, 9, 2) & " " _
                             & Mid(tempStr, 11, 2) & " " _
                             & Mid(tempStr, 13, 2) & " " _
                             & Mid(tempStr, 15, 2) & " " _
                             & Mid(tempStr, 17, 2) & " " _
                             & Mid(tempStr, 19, 2) & " " _
                             & Mid(tempStr, 21, 2) & " " _
                             & Mid(tempStr, 23, 2) & " " _
                             & Mid(tempStr, 25, 2) & " " _
                             & Mid(tempStr, 27, 2) & " " _
                             & Mid(tempStr, 29, 2) & " " _
                             & Mid(tempStr, 31, 2)
                             
    
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 21, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6108" Then
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
    
    
   'Get Response to get result
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 9) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HC0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    
    lst_Log.AddItem "MCU < 80 C0 00 00 08"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 10
    
    If Send_APDU_SLT(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve Result
                tempStr = ""
    
                For i = 0 To 7
                    tempStr = tempStr & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                
                lst_Log.AddItem "MCU >> " & tempStr
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                tempStr = Replace(tempStr, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    'Verify ACOS Authentication
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H7A
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(tempStr, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(tempStr, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(tempStr, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(tempStr, 15, 2))
    
    
    lst_Log.AddItem "SAM < 80 7A 00 00 08"
    lst_Log.AddItem "    <<" & Mid(tempStr, 1, 2) & " " _
                             & Mid(tempStr, 3, 2) & " " _
                             & Mid(tempStr, 5, 2) & " " _
                             & Mid(tempStr, 7, 2) & " " _
                             & Mid(tempStr, 9, 2) & " " _
                             & Mid(tempStr, 11, 2) & " " _
                             & Mid(tempStr, 13, 2) & " " _
                             & Mid(tempStr, 15, 2)
                             
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
    
    cmd_SubmitPIN.Enabled = True

End Sub

Private Sub cmd_SubmitPIN_Click()
    
Dim SendBuff() As Byte
Dim RecvBuff() As Byte
Dim RecvLen As Long
Dim SN, tempStr As String
Dim i As Integer
    
    
    'Encrypt PIN
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H74
    
    If opt_DES.Value = True Then
        SendBuff(2) = &H1
    ElseIf opt_3DES.Value = True Then
        SendBuff(2) = &H0
    End If
    
    SendBuff(3) = &H1
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(txt_PIN.Text, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(txt_PIN.Text, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(txt_PIN.Text, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(txt_PIN.Text, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(txt_PIN.Text, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(txt_PIN.Text, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(txt_PIN.Text, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(txt_PIN.Text, 15, 2))
    
    
    lst_Log.AddItem "SAM < 80 74 " & Right("00" & Hex(SendBuff(2)), 2) & " 01 08"
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


    'Get Response to get encrypted PIN
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
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(16)), 2) & Right("00" & Hex(RecvBuff(17)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve encrypted PIN
                tempStr = ""
    
                For i = 0 To 7
                    tempStr = tempStr & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                
                lst_Log.AddItem "SAM >> " & tempStr
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                tempStr = Replace(tempStr, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    'Submit Encrypted PIN
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H20
    SendBuff(2) = &H6  'PIN
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(tempStr, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(tempStr, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(tempStr, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(tempStr, 15, 2))
    
    
    lst_Log.AddItem "MCU < 80 20 06 00 08 "
    lst_Log.AddItem "    <<" & Mid(tempStr, 1, 2) & " " _
                             & Mid(tempStr, 3, 2) & " " _
                             & Mid(tempStr, 5, 2) & " " _
                             & Mid(tempStr, 7, 2) & " " _
                             & Mid(tempStr, 9, 2) & " " _
                             & Mid(tempStr, 11, 2) & " " _
                             & Mid(tempStr, 13, 2) & " " _
                             & Mid(tempStr, 15, 2)
    
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

    cmd_Change_PIN.Enabled = True

End Sub

Private Sub cmd_Change_PIN_Click()
Dim SendBuff() As Byte
Dim RecvBuff() As Byte
Dim RecvLen As Long
Dim SN, tempStr As String
Dim i As Integer
        
    'Decrypt PIN
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H76
    
    If opt_DES.Value = True Then
        SendBuff(2) = &H1
    ElseIf opt_3DES.Value = True Then
        SendBuff(2) = &H0
    End If
    
    SendBuff(3) = &H1
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(txt_New_PIN.Text, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(txt_New_PIN.Text, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(txt_New_PIN.Text, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(txt_New_PIN.Text, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(txt_New_PIN.Text, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(txt_New_PIN.Text, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(txt_New_PIN.Text, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(txt_New_PIN.Text, 15, 2))
    
    
    lst_Log.AddItem "SAM < 80 76 " & Right("00" & Hex(SendBuff(2)), 2) & " 01 08"
    lst_Log.AddItem "    <<" & Mid(txt_New_PIN.Text, 1, 2) & " " _
                             & Mid(txt_New_PIN.Text, 3, 2) & " " _
                             & Mid(txt_New_PIN.Text, 5, 2) & " " _
                             & Mid(txt_New_PIN.Text, 7, 2) & " " _
                             & Mid(txt_New_PIN.Text, 9, 2) & " " _
                             & Mid(txt_New_PIN.Text, 11, 2) & " " _
                             & Mid(txt_New_PIN.Text, 13, 2) & " " _
                             & Mid(txt_New_PIN.Text, 15, 2)
    
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


    'Get Response to get decrypted PIN
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
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(16)), 2) & Right("00" & Hex(RecvBuff(17)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve decrypted PIN
                tempStr = ""
    
                For i = 0 To 7
                    tempStr = tempStr & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                
                lst_Log.AddItem "SAM >> " & tempStr
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(8)), 2) & Right("00" & Hex(RecvBuff(9)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                tempStr = Replace(tempStr, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    'Change PIN
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H24
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(tempStr, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(tempStr, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(tempStr, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(tempStr, 15, 2))
    
    
    lst_Log.AddItem "MCU < 80 24 00 00 08 "
    lst_Log.AddItem "    <<" & Mid(tempStr, 1, 2) & " " _
                             & Mid(tempStr, 3, 2) & " " _
                             & Mid(tempStr, 5, 2) & " " _
                             & Mid(tempStr, 7, 2) & " " _
                             & Mid(tempStr, 9, 2) & " " _
                             & Mid(tempStr, 11, 2) & " " _
                             & Mid(tempStr, 13, 2) & " " _
                             & Mid(tempStr, 15, 2)
    
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


End Sub


Private Sub cmdCredit_Click()

Dim SendBuff() As Byte
Dim RecvBuff() As Byte
Dim RecvLen As Long
Dim SN, tempStr As String
Dim i As Integer
    
    
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
                
                SN = Replace(SN, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    'Diversify Kcr
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H72
    SendBuff(2) = &H2
    SendBuff(3) = &H85
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(SN, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(SN, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(SN, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(SN, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(SN, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(SN, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(SN, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(SN, 15, 2))
    
    
    lst_Log.AddItem "SAM < 80 72 02 85 08"
    lst_Log.AddItem "    <<" & Mid(SN, 1, 2) & " " _
                             & Mid(SN, 3, 2) & " " _
                             & Mid(SN, 5, 2) & " " _
                             & Mid(SN, 7, 2) & " " _
                             & Mid(SN, 9, 2) & " " _
                             & Mid(SN, 11, 2) & " " _
                             & Mid(SN, 13, 2) & " " _
                             & Mid(SN, 15, 2)
    
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
    
    
    'Inquire Account
    ReDim SendBuff(0 To 8) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HE4
    SendBuff(2) = &H1
    SendBuff(3) = &H0
    SendBuff(4) = &H4
    '4 bytes reference
    SendBuff(5) = &HAA
    SendBuff(6) = &HBB
    SendBuff(7) = &HCC
    SendBuff(8) = &HDD
    
    lst_Log.AddItem "MCU < 80 E4 01 00 04"
    lst_Log.AddItem "    <<AA BB CC DD"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6119" Then
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
    

    'Get Response to get result
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 26) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HC0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H19
    
    lst_Log.AddItem "MCU < 80 C0 00 00 19"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 27
    
    If Send_APDU_SLT(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(25)), 2) & Right("00" & Hex(RecvBuff(26)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(25)), 2) & Right("00" & Hex(RecvBuff(26)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve Result
                tempStr = ""
    
                For i = 0 To 24
                    tempStr = tempStr & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                
                lst_Log.AddItem "MCU >> " & tempStr
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(25)), 2) & Right("00" & Hex(RecvBuff(26)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                tempStr = Replace(tempStr, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If


    'Verify Inquire Account
    ReDim SendBuff(0 To 33) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H7C
    
    If opt_3DES.Value = True Then
        SendBuff(2) = &H2
    ElseIf opt_DES.Value = True Then
        SendBuff(2) = &H3
    End If
    
    SendBuff(3) = &H0
    SendBuff(4) = &H1D
    'Ref
    SendBuff(5) = &HAA
    SendBuff(6) = &HBB
    SendBuff(7) = &HCC
    SendBuff(8) = &HDD
    'MAC
    SendBuff(9) = CInt("&H" & Mid(tempStr, 1, 2))
    SendBuff(10) = CInt("&H" & Mid(tempStr, 3, 2))
    SendBuff(11) = CInt("&H" & Mid(tempStr, 5, 2))
    SendBuff(12) = CInt("&H" & Mid(tempStr, 7, 2))
    
    'Transaction Type
    SendBuff(13) = CInt("&H" & Mid(tempStr, 9, 2))
    
    'Balance
    SendBuff(14) = CInt("&H" & Mid(tempStr, 11, 2))
    SendBuff(15) = CInt("&H" & Mid(tempStr, 13, 2))
    SendBuff(16) = CInt("&H" & Mid(tempStr, 15, 2))
    
    'ATREF
    SendBuff(17) = CInt("&H" & Mid(tempStr, 17, 2))
    SendBuff(18) = CInt("&H" & Mid(tempStr, 19, 2))
    SendBuff(19) = CInt("&H" & Mid(tempStr, 21, 2))
    SendBuff(20) = CInt("&H" & Mid(tempStr, 23, 2))
    SendBuff(21) = CInt("&H" & Mid(tempStr, 25, 2))
    SendBuff(22) = CInt("&H" & Mid(tempStr, 27, 2))
    
    'Max Balance
    SendBuff(23) = CInt("&H" & Mid(tempStr, 29, 2))
    SendBuff(24) = CInt("&H" & Mid(tempStr, 31, 2))
    SendBuff(25) = CInt("&H" & Mid(tempStr, 33, 2))
    
    'TTREFc
    SendBuff(26) = CInt("&H" & Mid(tempStr, 35, 2))
    SendBuff(27) = CInt("&H" & Mid(tempStr, 37, 2))
    SendBuff(28) = CInt("&H" & Mid(tempStr, 39, 2))
    SendBuff(29) = CInt("&H" & Mid(tempStr, 41, 2))
    
    'TTREFd
    SendBuff(30) = CInt("&H" & Mid(tempStr, 43, 2))
    SendBuff(31) = CInt("&H" & Mid(tempStr, 45, 2))
    SendBuff(32) = CInt("&H" & Mid(tempStr, 47, 2))
    SendBuff(33) = CInt("&H" & Mid(tempStr, 49, 2))
    
    
    lst_Log.AddItem "SAM < 80 7C " & Right("00" & Hex(SendBuff(2)), 2) & " 00 1D"
    lst_Log.AddItem "    <<AA BB CC DD " & Mid(tempStr, 1, 2) & " " _
                             & Mid(tempStr, 3, 2) & " " _
                             & Mid(tempStr, 5, 2) & " " _
                             & Mid(tempStr, 7, 2) & " " _
                             & Mid(tempStr, 9, 2) & " " _
                             & Mid(tempStr, 11, 2) & " " _
                             & Mid(tempStr, 13, 2) & " " _
                             & Mid(tempStr, 15, 2) & " " _
                             & Mid(tempStr, 17, 2) & " " _
                             & Mid(tempStr, 19, 2) & " " _
                             & Mid(tempStr, 21, 2) & " " _
                             & Mid(tempStr, 23, 2)
                             
    
    lst_Log.AddItem "    <<" & Mid(tempStr, 25, 2) & " " _
                             & Mid(tempStr, 27, 2) & " " _
                             & Mid(tempStr, 29, 2) & " " _
                             & Mid(tempStr, 31, 2) & " " _
                             & Mid(tempStr, 33, 2) & " " _
                             & Mid(tempStr, 35, 2) & " " _
                             & Mid(tempStr, 37, 2) & " " _
                             & Mid(tempStr, 39, 2) & " " _
                             & Mid(tempStr, 41, 2) & " " _
                             & Mid(tempStr, 43, 2) & " " _
                             & Mid(tempStr, 45, 2) & " " _
                             & Mid(tempStr, 47, 2) & " " _
                             & Mid(tempStr, 49, 2)
                             
    
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

    txtMaxBalance.Text = Get_Balance(CInt("&H" & Mid(tempStr, 29, 2)), CInt("&H" & Mid(tempStr, 31, 2)), CInt("&H" & Mid(tempStr, 33, 2)))
    
    txtInqAmount.Text = Get_Balance(CInt("&H" & Mid(tempStr, 11, 2)), CInt("&H" & Mid(tempStr, 13, 2)), CInt("&H" & Mid(tempStr, 15, 2)))




    'Prepare ACOS Transaction
    ReDim SendBuff(0 To 17) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H7E
    
    If opt_3DES.Value = True Then
        SendBuff(2) = &H2
    ElseIf opt_DES.Value = True Then
        SendBuff(2) = &H3
    End If
    
    SendBuff(3) = &HE2
    SendBuff(4) = &HD
    
    'Amount to Credit
    SendBuff(5) = CLng(txtCreditAmount.Text) \ 65536
    SendBuff(6) = (CLng(txtCreditAmount.Text) \ 256) Mod 65536 Mod 256
    SendBuff(7) = CLng(txtCreditAmount.Text) Mod 256
    
    'TTREFd
    SendBuff(8) = CInt("&H" & Mid(tempStr, 43, 2))
    SendBuff(9) = CInt("&H" & Mid(tempStr, 45, 2))
    SendBuff(10) = CInt("&H" & Mid(tempStr, 47, 2))
    SendBuff(11) = CInt("&H" & Mid(tempStr, 49, 2))
    
    'ATREF
    SendBuff(12) = CInt("&H" & Mid(tempStr, 17, 2))
    SendBuff(13) = CInt("&H" & Mid(tempStr, 19, 2))
    SendBuff(14) = CInt("&H" & Mid(tempStr, 21, 2))
    SendBuff(15) = CInt("&H" & Mid(tempStr, 23, 2))
    SendBuff(16) = CInt("&H" & Mid(tempStr, 25, 2))
    SendBuff(17) = CInt("&H" & Mid(tempStr, 27, 2))
    
    
    
    lst_Log.AddItem "SAM < 80 7E " & Right("00" & Hex(SendBuff(2)), 2) & " E2 0D"
    lst_Log.AddItem "    <<" & Right("00" & Hex(SendBuff(5)), 2) & " " _
                             & Right("00" & Hex(SendBuff(6)), 2) & " " _
                             & Right("00" & Hex(SendBuff(7)), 2) & " " _
                             & Right("00" & Hex(SendBuff(8)), 2) & " " _
                             & Right("00" & Hex(SendBuff(9)), 2) & " " _
                             & Right("00" & Hex(SendBuff(10)), 2) & " " _
                             & Right("00" & Hex(SendBuff(11)), 2) & " " _
                             & Right("00" & Hex(SendBuff(12)), 2) & " " _
                             & Right("00" & Hex(SendBuff(13)), 2) & " " _
                             & Right("00" & Hex(SendBuff(14)), 2) & " " _
                             & Right("00" & Hex(SendBuff(15)), 2) & " " _
                             & Right("00" & Hex(SendBuff(16)), 2) & " " _
                             & Right("00" & Hex(SendBuff(17)), 2)
                             
    
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SAM(SendBuff, 18, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "610B" Then
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

    
    'Get Response to get result
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 12) As Byte

    SendBuff(0) = &H0
    SendBuff(1) = &HC0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &HB
    
    lst_Log.AddItem "SAM < 00 C0 00 00 0B"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = &HD
    
    If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(11)), 2) & Right("00" & Hex(RecvBuff(12)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(11)), 2) & Right("00" & Hex(RecvBuff(12)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve Result
                tempStr = ""
    
                For i = 0 To 10
                    tempStr = tempStr & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                
                lst_Log.AddItem "SAM >> " & tempStr
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(11)), 2) & Right("00" & Hex(RecvBuff(12)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                tempStr = Replace(tempStr, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    'Credit
    ReDim SendBuff(0 To 15) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HE2
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &HB
    SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(tempStr, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(tempStr, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(tempStr, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(tempStr, 15, 2))
    SendBuff(13) = CInt("&H" & Mid(tempStr, 17, 2))
    SendBuff(14) = CInt("&H" & Mid(tempStr, 19, 2))
    SendBuff(15) = CInt("&H" & Mid(tempStr, 21, 2))
    
    
    lst_Log.AddItem "MCU < 80 E2 00 00 0B "
    lst_Log.AddItem "    <<" & Mid(tempStr, 1, 2) & " " _
                             & Mid(tempStr, 3, 2) & " " _
                             & Mid(tempStr, 5, 2) & " " _
                             & Mid(tempStr, 7, 2) & " " _
                             & Mid(tempStr, 9, 2) & " " _
                             & Mid(tempStr, 11, 2) & " " _
                             & Mid(tempStr, 13, 2) & " " _
                             & Mid(tempStr, 15, 2) & " " _
                             & Mid(tempStr, 17, 2) & " " _
                             & Mid(tempStr, 19, 2) & " " _
                             & Mid(tempStr, 21, 2)
    
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 16, RecvLen, RecvBuff) = True Then
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
    
    
    
    'Perform Verify Inquire Account w/ Credit Key and new ammount
    
    'Inquire Account
    ReDim SendBuff(0 To 8) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HE4
    SendBuff(2) = &H1
    SendBuff(3) = &H0
    SendBuff(4) = &H4
    '4 bytes reference
    SendBuff(5) = &HAA
    SendBuff(6) = &HBB
    SendBuff(7) = &HCC
    SendBuff(8) = &HDD
    
    lst_Log.AddItem "MCU < 80 E4 01 00 04"
    lst_Log.AddItem "    <<AA BB CC DD"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6119" Then
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
    
    
    'Get Response to get result
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 26) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HC0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H19
    
    lst_Log.AddItem "MCU < 80 C0 00 00 19"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 27
    
    If Send_APDU_SLT(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(25)), 2) & Right("00" & Hex(RecvBuff(26)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(25)), 2) & Right("00" & Hex(RecvBuff(26)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve Result
                tempStr = ""
    
                For i = 0 To 24
                    tempStr = tempStr & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                
                lst_Log.AddItem "MCU >> " & tempStr
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(25)), 2) & Right("00" & Hex(RecvBuff(26)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                tempStr = Replace(tempStr, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
        'Verify Inquire Account
    ReDim SendBuff(0 To 33) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H7C
    
    If opt_3DES.Value = True Then
        SendBuff(2) = &H2
    ElseIf opt_DES.Value = True Then
        SendBuff(2) = &H3
    End If
    
    SendBuff(3) = &H0
    SendBuff(4) = &H1D
    'Ref
    SendBuff(5) = &HAA
    SendBuff(6) = &HBB
    SendBuff(7) = &HCC
    SendBuff(8) = &HDD
    'MAC
    SendBuff(9) = CInt("&H" & Mid(tempStr, 1, 2))
    SendBuff(10) = CInt("&H" & Mid(tempStr, 3, 2))
    SendBuff(11) = CInt("&H" & Mid(tempStr, 5, 2))
    SendBuff(12) = CInt("&H" & Mid(tempStr, 7, 2))
    
    'Transaction Type
    SendBuff(13) = CInt("&H" & Mid(tempStr, 9, 2))
    
    'Balance
    SendBuff(14) = CInt("&H" & Mid(tempStr, 11, 2))
    SendBuff(15) = CInt("&H" & Mid(tempStr, 13, 2))
    SendBuff(16) = CInt("&H" & Mid(tempStr, 15, 2))
    
    'ATREF
    SendBuff(17) = CInt("&H" & Mid(tempStr, 17, 2))
    SendBuff(18) = CInt("&H" & Mid(tempStr, 19, 2))
    SendBuff(19) = CInt("&H" & Mid(tempStr, 21, 2))
    SendBuff(20) = CInt("&H" & Mid(tempStr, 23, 2))
    SendBuff(21) = CInt("&H" & Mid(tempStr, 25, 2))
    SendBuff(22) = CInt("&H" & Mid(tempStr, 27, 2))
    
    'Max Balance
    SendBuff(23) = CInt("&H" & Mid(tempStr, 29, 2))
    SendBuff(24) = CInt("&H" & Mid(tempStr, 31, 2))
    SendBuff(25) = CInt("&H" & Mid(tempStr, 33, 2))
    
    'TTREFc
    SendBuff(26) = CInt("&H" & Mid(tempStr, 35, 2))
    SendBuff(27) = CInt("&H" & Mid(tempStr, 37, 2))
    SendBuff(28) = CInt("&H" & Mid(tempStr, 39, 2))
    SendBuff(29) = CInt("&H" & Mid(tempStr, 41, 2))
    
    'TTREFd
    SendBuff(30) = CInt("&H" & Mid(tempStr, 43, 2))
    SendBuff(31) = CInt("&H" & Mid(tempStr, 45, 2))
    SendBuff(32) = CInt("&H" & Mid(tempStr, 47, 2))
    SendBuff(33) = CInt("&H" & Mid(tempStr, 49, 2))
    
    
    lst_Log.AddItem "SAM < 80 7C " & Right("00" & Hex(SendBuff(2)), 2) & " 00 1D"
    lst_Log.AddItem "    <<AA BB CC DD " & Mid(tempStr, 1, 2) & " " _
                             & Mid(tempStr, 3, 2) & " " _
                             & Mid(tempStr, 5, 2) & " " _
                             & Mid(tempStr, 7, 2) & " " _
                             & Mid(tempStr, 9, 2) & " " _
                             & Mid(tempStr, 11, 2) & " " _
                             & Mid(tempStr, 13, 2) & " " _
                             & Mid(tempStr, 15, 2) & " " _
                             & Mid(tempStr, 17, 2) & " " _
                             & Mid(tempStr, 19, 2) & " " _
                             & Mid(tempStr, 21, 2) & " " _
                             & Mid(tempStr, 23, 2)
                             
    
    lst_Log.AddItem "    <<" & Mid(tempStr, 25, 2) & " " _
                             & Mid(tempStr, 27, 2) & " " _
                             & Mid(tempStr, 29, 2) & " " _
                             & Mid(tempStr, 31, 2) & " " _
                             & Mid(tempStr, 33, 2) & " " _
                             & Mid(tempStr, 35, 2) & " " _
                             & Mid(tempStr, 37, 2) & " " _
                             & Mid(tempStr, 39, 2) & " " _
                             & Mid(tempStr, 41, 2) & " " _
                             & Mid(tempStr, 43, 2) & " " _
                             & Mid(tempStr, 45, 2) & " " _
                             & Mid(tempStr, 47, 2) & " " _
                             & Mid(tempStr, 49, 2)
                             
    
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


End Sub

Private Sub cmdDebit_Click()

Dim SendBuff() As Byte
Dim RecvBuff() As Byte
Dim RecvLen As Long
Dim SN, tempStr, tempStr2 As String
Dim i As Integer
Dim Bal, NewBal As Long
    
    
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
                
                SN = Replace(SN, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    'Diversify Kd
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H72
    SendBuff(2) = &H2
    SendBuff(3) = &H84
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(SN, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(SN, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(SN, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(SN, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(SN, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(SN, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(SN, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(SN, 15, 2))
    
    
    lst_Log.AddItem "SAM < 80 72 02 84 08"
    lst_Log.AddItem "    <<" & Mid(SN, 1, 2) & " " _
                             & Mid(SN, 3, 2) & " " _
                             & Mid(SN, 5, 2) & " " _
                             & Mid(SN, 7, 2) & " " _
                             & Mid(SN, 9, 2) & " " _
                             & Mid(SN, 11, 2) & " " _
                             & Mid(SN, 13, 2) & " " _
                             & Mid(SN, 15, 2)
    
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
    
    
    'Inquire Account
    ReDim SendBuff(0 To 8) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HE4
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H4
    '4 bytes reference
    SendBuff(5) = &HAA
    SendBuff(6) = &HBB
    SendBuff(7) = &HCC
    SendBuff(8) = &HDD
    
    lst_Log.AddItem "MCU < 80 E4 00 00 04"
    lst_Log.AddItem "    <<AA BB CC DD"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6119" Then
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
    

    'Get Response to get result
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 26) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HC0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H19
    
    lst_Log.AddItem "MCU < 80 C0 00 00 19"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 27
    
    If Send_APDU_SLT(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(25)), 2) & Right("00" & Hex(RecvBuff(26)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(25)), 2) & Right("00" & Hex(RecvBuff(26)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve Result
                tempStr2 = ""
    
                For i = 0 To 24
                    tempStr2 = tempStr2 & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                
                lst_Log.AddItem "MCU >> " & tempStr2
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(25)), 2) & Right("00" & Hex(RecvBuff(26)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                tempStr2 = Replace(tempStr2, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If


    'Verify Inquire Account
    ReDim SendBuff(0 To 33) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H7C
    
    If opt_3DES.Value = True Then
        SendBuff(2) = &H2
    ElseIf opt_DES.Value = True Then
        SendBuff(2) = &H3
    End If
    
    SendBuff(3) = &H0
    SendBuff(4) = &H1D
    'Ref
    SendBuff(5) = &HAA
    SendBuff(6) = &HBB
    SendBuff(7) = &HCC
    SendBuff(8) = &HDD
    'MAC
    SendBuff(9) = CInt("&H" & Mid(tempStr2, 1, 2))
    SendBuff(10) = CInt("&H" & Mid(tempStr2, 3, 2))
    SendBuff(11) = CInt("&H" & Mid(tempStr2, 5, 2))
    SendBuff(12) = CInt("&H" & Mid(tempStr2, 7, 2))
    
    'Transaction Type
    SendBuff(13) = CInt("&H" & Mid(tempStr2, 9, 2))
    
    'Balance
    SendBuff(14) = CInt("&H" & Mid(tempStr2, 11, 2))
    SendBuff(15) = CInt("&H" & Mid(tempStr2, 13, 2))
    SendBuff(16) = CInt("&H" & Mid(tempStr2, 15, 2))
    
    'ATREF
    SendBuff(17) = CInt("&H" & Mid(tempStr2, 17, 2))
    SendBuff(18) = CInt("&H" & Mid(tempStr2, 19, 2))
    SendBuff(19) = CInt("&H" & Mid(tempStr2, 21, 2))
    SendBuff(20) = CInt("&H" & Mid(tempStr2, 23, 2))
    SendBuff(21) = CInt("&H" & Mid(tempStr2, 25, 2))
    SendBuff(22) = CInt("&H" & Mid(tempStr2, 27, 2))
    
    'Max Balance
    SendBuff(23) = CInt("&H" & Mid(tempStr2, 29, 2))
    SendBuff(24) = CInt("&H" & Mid(tempStr2, 31, 2))
    SendBuff(25) = CInt("&H" & Mid(tempStr2, 33, 2))
    
    'TTREFc
    SendBuff(26) = CInt("&H" & Mid(tempStr2, 35, 2))
    SendBuff(27) = CInt("&H" & Mid(tempStr2, 37, 2))
    SendBuff(28) = CInt("&H" & Mid(tempStr2, 39, 2))
    SendBuff(29) = CInt("&H" & Mid(tempStr2, 41, 2))
    
    'TTREFd
    SendBuff(30) = CInt("&H" & Mid(tempStr2, 43, 2))
    SendBuff(31) = CInt("&H" & Mid(tempStr2, 45, 2))
    SendBuff(32) = CInt("&H" & Mid(tempStr2, 47, 2))
    SendBuff(33) = CInt("&H" & Mid(tempStr2, 49, 2))
    
    
    lst_Log.AddItem "SAM < 80 7C " & Right("00" & Hex(SendBuff(2)), 2) & " 00 1D"
    lst_Log.AddItem "    <<AA BB CC DD " & Mid(tempStr2, 1, 2) & " " _
                             & Mid(tempStr2, 3, 2) & " " _
                             & Mid(tempStr2, 5, 2) & " " _
                             & Mid(tempStr2, 7, 2) & " " _
                             & Mid(tempStr2, 9, 2) & " " _
                             & Mid(tempStr2, 11, 2) & " " _
                             & Mid(tempStr2, 13, 2) & " " _
                             & Mid(tempStr2, 15, 2) & " " _
                             & Mid(tempStr2, 17, 2) & " " _
                             & Mid(tempStr2, 19, 2) & " " _
                             & Mid(tempStr2, 21, 2) & " " _
                             & Mid(tempStr2, 23, 2)
                             
    
    lst_Log.AddItem "    <<" & Mid(tempStr2, 25, 2) & " " _
                             & Mid(tempStr2, 27, 2) & " " _
                             & Mid(tempStr2, 29, 2) & " " _
                             & Mid(tempStr2, 31, 2) & " " _
                             & Mid(tempStr2, 33, 2) & " " _
                             & Mid(tempStr2, 35, 2) & " " _
                             & Mid(tempStr2, 37, 2) & " " _
                             & Mid(tempStr2, 39, 2) & " " _
                             & Mid(tempStr2, 41, 2) & " " _
                             & Mid(tempStr2, 43, 2) & " " _
                             & Mid(tempStr2, 45, 2) & " " _
                             & Mid(tempStr2, 47, 2) & " " _
                             & Mid(tempStr2, 49, 2)
                             
    
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

    Bal = CLng(Get_Balance(CInt("&H" & Mid(tempStr2, 11, 2)), CInt("&H" & Mid(tempStr2, 13, 2)), CInt("&H" & Mid(tempStr2, 15, 2))))
    
    
    'Prepare ACOS Transaction
    ReDim SendBuff(0 To 17) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H7E
    
    If opt_3DES.Value = True Then
        SendBuff(2) = &H2
    ElseIf opt_DES.Value = True Then
        SendBuff(2) = &H3
    End If
    
    SendBuff(3) = &HE6
    SendBuff(4) = &HD
    
    'Amount to Debit
    SendBuff(5) = CLng(txtDebitAmount.Text) \ 65536
    SendBuff(6) = (CLng(txtDebitAmount.Text) \ CLng(256)) Mod 65536 Mod CLng(256)
    SendBuff(7) = CLng(txtDebitAmount.Text) Mod CLng(256)
    
    'TTREFd
    SendBuff(8) = CInt("&H" & Mid(tempStr2, 43, 2))
    SendBuff(9) = CInt("&H" & Mid(tempStr2, 45, 2))
    SendBuff(10) = CInt("&H" & Mid(tempStr2, 47, 2))
    SendBuff(11) = CInt("&H" & Mid(tempStr2, 49, 2))
    
    'ATREF
    SendBuff(12) = CInt("&H" & Mid(tempStr2, 17, 2))
    SendBuff(13) = CInt("&H" & Mid(tempStr2, 19, 2))
    SendBuff(14) = CInt("&H" & Mid(tempStr2, 21, 2))
    SendBuff(15) = CInt("&H" & Mid(tempStr2, 23, 2))
    SendBuff(16) = CInt("&H" & Mid(tempStr2, 25, 2))
    SendBuff(17) = CInt("&H" & Mid(tempStr2, 27, 2))
    
    
    
    lst_Log.AddItem "SAM < 80 7E " & Right("00" & Hex(SendBuff(2)), 2) & " E6 0D"
    lst_Log.AddItem "    <<" & Right("00" & Hex(SendBuff(5)), 2) & " " _
                             & Right("00" & Hex(SendBuff(6)), 2) & " " _
                             & Right("00" & Hex(SendBuff(7)), 2) & " " _
                             & Right("00" & Hex(SendBuff(8)), 2) & " " _
                             & Right("00" & Hex(SendBuff(9)), 2) & " " _
                             & Right("00" & Hex(SendBuff(10)), 2) & " " _
                             & Right("00" & Hex(SendBuff(11)), 2) & " " _
                             & Right("00" & Hex(SendBuff(12)), 2) & " " _
                             & Right("00" & Hex(SendBuff(13)), 2) & " " _
                             & Right("00" & Hex(SendBuff(14)), 2) & " " _
                             & Right("00" & Hex(SendBuff(15)), 2) & " " _
                             & Right("00" & Hex(SendBuff(16)), 2) & " " _
                             & Right("00" & Hex(SendBuff(17)), 2)
                             
    
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SAM(SendBuff, 18, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "610B" Then
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

    
    'Get Response to get result
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 12) As Byte

    SendBuff(0) = &H0
    SendBuff(1) = &HC0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &HB
    
    lst_Log.AddItem "SAM < 00 C0 00 00 0B"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = &HD
    
    If Send_APDU_SAM(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(11)), 2) & Right("00" & Hex(RecvBuff(12)), 2) <> "9000" Then
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(11)), 2) & Right("00" & Hex(RecvBuff(12)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve Result
                tempStr = ""
    
                For i = 0 To 10
                    tempStr = tempStr & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                
                lst_Log.AddItem "SAM >> " & tempStr
                lst_Log.AddItem "SAM > " & Right("00" & Hex(RecvBuff(11)), 2) & Right("00" & Hex(RecvBuff(12)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                tempStr = Replace(tempStr, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    'Debit and return Debit Certificate
    ReDim SendBuff(0 To 15) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HE6
    SendBuff(2) = &H1
    SendBuff(3) = &H0
    SendBuff(4) = &HB
    SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(tempStr, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(tempStr, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(tempStr, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(tempStr, 15, 2))
    SendBuff(13) = CInt("&H" & Mid(tempStr, 17, 2))
    SendBuff(14) = CInt("&H" & Mid(tempStr, 19, 2))
    SendBuff(15) = CInt("&H" & Mid(tempStr, 21, 2))
    
    
    lst_Log.AddItem "MCU < 80 E6 01 00 0B "
    lst_Log.AddItem "    <<" & Mid(tempStr, 1, 2) & " " _
                             & Mid(tempStr, 3, 2) & " " _
                             & Mid(tempStr, 5, 2) & " " _
                             & Mid(tempStr, 7, 2) & " " _
                             & Mid(tempStr, 9, 2) & " " _
                             & Mid(tempStr, 11, 2) & " " _
                             & Mid(tempStr, 13, 2) & " " _
                             & Mid(tempStr, 15, 2) & " " _
                             & Mid(tempStr, 17, 2) & " " _
                             & Mid(tempStr, 19, 2) & " " _
                             & Mid(tempStr, 21, 2)
    
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 16, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6104" Then
                
                If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) = "6A86" Then
                    lst_Log.AddItem "Debit Certificate Not Supported By ACOS2 card or lower"
                    lst_Log.AddItem "Change P1 = 0 to perform Debit without returning debit certificate"
                    lst_Log.ListIndex = lst_Log.NewIndex
                    Debit_ACOS2 tempStr
                Else
                    lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                    lst_Log.ListIndex = lst_Log.NewIndex
                End If
                
                Exit Sub
           Else
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
           End If
    Else
           Exit Sub
    End If
    
    
    'Get Response to get result
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 5) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HC0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H4
    
    lst_Log.AddItem "MCU < 80 C0 00 00 04"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 6
    
    If Send_APDU_SLT(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(4)), 2) & Right("00" & Hex(RecvBuff(5)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(4)), 2) & Right("00" & Hex(RecvBuff(5)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve Result
                tempStr = ""
    
                For i = 0 To 3
                    tempStr = tempStr & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                
                lst_Log.AddItem "MCU >> " & tempStr
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(4)), 2) & Right("00" & Hex(RecvBuff(5)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                tempStr = Replace(tempStr, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If


    'Verify Debit Certificate
    ReDim SendBuff(0 To 24) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H70
    If opt_3DES.Value = True Then
        SendBuff(2) = &H2
    ElseIf opt_DES.Value = True Then
        SendBuff(2) = &H3
    End If
    SendBuff(3) = &H0
    SendBuff(4) = &H14
    SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))
    
    'Amount last Debited from card
    SendBuff(9) = CLng(txtDebitAmount.Text) \ 65536
    SendBuff(10) = (CLng(txtDebitAmount.Text) \ CLng(256)) Mod 65536 Mod CLng(256)
    SendBuff(11) = CLng(txtDebitAmount.Text) Mod CLng(256)
    
    'Expected New Balance after the Debit
    NewBal = Bal - CLng(txtDebitAmount.Text)
    
    SendBuff(12) = CLng(NewBal) \ 65536
    SendBuff(13) = (CLng(NewBal) \ CLng(256)) Mod 65536 Mod CLng(256)
    SendBuff(14) = CLng(NewBal) Mod CLng(256)
        
    'ATREF
    SendBuff(15) = CInt("&H" & Mid(tempStr2, 17, 2))
    SendBuff(16) = CInt("&H" & Mid(tempStr2, 19, 2))
    SendBuff(17) = CInt("&H" & Mid(tempStr2, 21, 2))
    SendBuff(18) = CInt("&H" & Mid(tempStr2, 23, 2))
    SendBuff(19) = CInt("&H" & Mid(tempStr2, 25, 2))
    SendBuff(20) = CInt("&H" & Mid(tempStr2, 27, 2))
    
    'TTREFd
    SendBuff(21) = CInt("&H" & Mid(tempStr2, 43, 2))
    SendBuff(22) = CInt("&H" & Mid(tempStr2, 45, 2))
    SendBuff(23) = CInt("&H" & Mid(tempStr2, 47, 2))
    SendBuff(24) = CInt("&H" & Mid(tempStr2, 49, 2))
    
    
    lst_Log.AddItem "SAM < 80 70 " & Right("00" & Hex(SendBuff(2)), 2) & " 00 14 "
    lst_Log.AddItem "    <<" & Mid(tempStr, 1, 2) & " " _
                             & Mid(tempStr, 3, 2) & " " _
                             & Mid(tempStr, 5, 2) & " " _
                             & Mid(tempStr, 7, 2) & " " _
                             & Right("00" & Hex(SendBuff(9)), 2) & " " _
                             & Right("00" & Hex(SendBuff(10)), 2) & " " _
                             & Right("00" & Hex(SendBuff(11)), 2) & " " _
                             & Right("00" & Hex(SendBuff(12)), 2) & " " _
                             & Right("00" & Hex(SendBuff(13)), 2) & " " _
                             & Right("00" & Hex(SendBuff(14)), 2) & " " _
                             & Right("00" & Hex(SendBuff(15)), 2) & " " _
                             & Right("00" & Hex(SendBuff(16)), 2) & " " _
                             & Right("00" & Hex(SendBuff(17)), 2) & " " _
                             & Right("00" & Hex(SendBuff(18)), 2) & " " _
                             & Right("00" & Hex(SendBuff(19)), 2) & " " _
                             & Right("00" & Hex(SendBuff(20)), 2) & " " _
                             & Right("00" & Hex(SendBuff(21)), 2) & " " _
                             & Right("00" & Hex(SendBuff(22)), 2) & " " _
                             & Right("00" & Hex(SendBuff(23)), 2) & " " _
                             & Right("00" & Hex(SendBuff(24)), 2)
                             
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SAM(SendBuff, 25, RecvLen, RecvBuff) = True Then
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


Private Sub Debit_ACOS2(ByVal tempStr As String)

    'Debit without returning debit certificate for ACOS2 or lower
    Dim RecvLen As Long
    Dim SendBuff(0 To 15) As Byte
    Dim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HE6
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &HB
    SendBuff(5) = CInt("&H" & Mid(tempStr, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(tempStr, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(tempStr, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(tempStr, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(tempStr, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(tempStr, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(tempStr, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(tempStr, 15, 2))
    SendBuff(13) = CInt("&H" & Mid(tempStr, 17, 2))
    SendBuff(14) = CInt("&H" & Mid(tempStr, 19, 2))
    SendBuff(15) = CInt("&H" & Mid(tempStr, 21, 2))
    
    
    lst_Log.AddItem "MCU < 80 E6 00 00 0B "
    lst_Log.AddItem "    <<" & Mid(tempStr, 1, 2) & " " _
                             & Mid(tempStr, 3, 2) & " " _
                             & Mid(tempStr, 5, 2) & " " _
                             & Mid(tempStr, 7, 2) & " " _
                             & Mid(tempStr, 9, 2) & " " _
                             & Mid(tempStr, 11, 2) & " " _
                             & Mid(tempStr, 13, 2) & " " _
                             & Mid(tempStr, 15, 2) & " " _
                             & Mid(tempStr, 17, 2) & " " _
                             & Mid(tempStr, 19, 2) & " " _
                             & Mid(tempStr, 21, 2)
    
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 16, RecvLen, RecvBuff) = True Then
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


Private Sub cmdInqAccount_Click()

Dim SendBuff() As Byte
Dim RecvBuff() As Byte
Dim RecvLen As Long
Dim SN, tempStr As String
Dim i As Integer
    
    
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
                
                SN = Replace(SN, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If
    
    
    'Diversify Kcf
    ReDim SendBuff(0 To 12) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H72
    SendBuff(2) = &H2
    SendBuff(3) = &H86
    SendBuff(4) = &H8
    SendBuff(5) = CInt("&H" & Mid(SN, 1, 2))
    SendBuff(6) = CInt("&H" & Mid(SN, 3, 2))
    SendBuff(7) = CInt("&H" & Mid(SN, 5, 2))
    SendBuff(8) = CInt("&H" & Mid(SN, 7, 2))
    SendBuff(9) = CInt("&H" & Mid(SN, 9, 2))
    SendBuff(10) = CInt("&H" & Mid(SN, 11, 2))
    SendBuff(11) = CInt("&H" & Mid(SN, 13, 2))
    SendBuff(12) = CInt("&H" & Mid(SN, 15, 2))
    
    
    lst_Log.AddItem "SAM < 80 72 02 86 08"
    lst_Log.AddItem "    <<" & Mid(SN, 1, 2) & " " _
                             & Mid(SN, 3, 2) & " " _
                             & Mid(SN, 5, 2) & " " _
                             & Mid(SN, 7, 2) & " " _
                             & Mid(SN, 9, 2) & " " _
                             & Mid(SN, 11, 2) & " " _
                             & Mid(SN, 13, 2) & " " _
                             & Mid(SN, 15, 2)
    
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
    
    
    'Inquire Account
    ReDim SendBuff(0 To 8) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HE4
    SendBuff(2) = &H2
    SendBuff(3) = &H0
    SendBuff(4) = &H4
    '4 bytes reference
    SendBuff(5) = &HAA
    SendBuff(6) = &HBB
    SendBuff(7) = &HCC
    SendBuff(8) = &HDD
    
    lst_Log.AddItem "MCU < 80 E4 02 00 04"
    lst_Log.AddItem "    <<AA BB CC DD"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 2
    
    If Send_APDU_SLT(SendBuff, 9, RecvLen, RecvBuff) = True Then
           If Right("00" & Hex(RecvBuff(0)), 2) & Right("00" & Hex(RecvBuff(1)), 2) <> "6119" Then
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
    

    'Get Response to get result
    ReDim SendBuff(0 To 4) As Byte
    ReDim RecvBuff(0 To 26) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &HC0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H19
    
    lst_Log.AddItem "MCU < 80 C0 00 00 19"
    lst_Log.ListIndex = lst_Log.NewIndex
    
    RecvLen = 27
    
    If Send_APDU_SLT(SendBuff, 5, RecvLen, RecvBuff) = True Then
    
           If Right("00" & Hex(RecvBuff(25)), 2) & Right("00" & Hex(RecvBuff(26)), 2) <> "9000" Then
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(25)), 2) & Right("00" & Hex(RecvBuff(26)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                Exit Sub
           Else
                'Retrieve Result
                tempStr = ""
    
                For i = 0 To 24
                    tempStr = tempStr & Right("00" & Hex(RecvBuff(i)), 2) & " "
                Next i
                
                lst_Log.AddItem "MCU >> " & tempStr
                lst_Log.AddItem "MCU > " & Right("00" & Hex(RecvBuff(25)), 2) & Right("00" & Hex(RecvBuff(26)), 2)
                lst_Log.ListIndex = lst_Log.NewIndex
                
                tempStr = Replace(tempStr, " ", "")
                
           End If
           
    Else
           Exit Sub
    End If



    'Verify Inquire Account
    ReDim SendBuff(0 To 33) As Byte
    ReDim RecvBuff(0 To 1) As Byte

    SendBuff(0) = &H80
    SendBuff(1) = &H7C
    
    If opt_3DES.Value = True Then
        SendBuff(2) = &H2
    ElseIf opt_DES.Value = True Then
        SendBuff(2) = &H3
    End If
    
    SendBuff(3) = &H0
    SendBuff(4) = &H1D
    'Ref
    SendBuff(5) = &HAA
    SendBuff(6) = &HBB
    SendBuff(7) = &HCC
    SendBuff(8) = &HDD
    'MAC
    SendBuff(9) = CInt("&H" & Mid(tempStr, 1, 2))
    SendBuff(10) = CInt("&H" & Mid(tempStr, 3, 2))
    SendBuff(11) = CInt("&H" & Mid(tempStr, 5, 2))
    SendBuff(12) = CInt("&H" & Mid(tempStr, 7, 2))
    
    'Transaction Type
    SendBuff(13) = CInt("&H" & Mid(tempStr, 9, 2))
    
    'Balance
    SendBuff(14) = CInt("&H" & Mid(tempStr, 11, 2))
    SendBuff(15) = CInt("&H" & Mid(tempStr, 13, 2))
    SendBuff(16) = CInt("&H" & Mid(tempStr, 15, 2))
    
    'ATREF
    SendBuff(17) = CInt("&H" & Mid(tempStr, 17, 2))
    SendBuff(18) = CInt("&H" & Mid(tempStr, 19, 2))
    SendBuff(19) = CInt("&H" & Mid(tempStr, 21, 2))
    SendBuff(20) = CInt("&H" & Mid(tempStr, 23, 2))
    SendBuff(21) = CInt("&H" & Mid(tempStr, 25, 2))
    SendBuff(22) = CInt("&H" & Mid(tempStr, 27, 2))
    
    'Max Balance
    SendBuff(23) = CInt("&H" & Mid(tempStr, 29, 2))
    SendBuff(24) = CInt("&H" & Mid(tempStr, 31, 2))
    SendBuff(25) = CInt("&H" & Mid(tempStr, 33, 2))
    
    'TTREFc
    SendBuff(26) = CInt("&H" & Mid(tempStr, 35, 2))
    SendBuff(27) = CInt("&H" & Mid(tempStr, 37, 2))
    SendBuff(28) = CInt("&H" & Mid(tempStr, 39, 2))
    SendBuff(29) = CInt("&H" & Mid(tempStr, 41, 2))
    
    'TTREFd
    SendBuff(30) = CInt("&H" & Mid(tempStr, 43, 2))
    SendBuff(31) = CInt("&H" & Mid(tempStr, 45, 2))
    SendBuff(32) = CInt("&H" & Mid(tempStr, 47, 2))
    SendBuff(33) = CInt("&H" & Mid(tempStr, 49, 2))
    
    
    lst_Log.AddItem "SAM < 80 7C " & Right("00" & Hex(SendBuff(2)), 2) & " 00 1D"
    lst_Log.AddItem "    <<AA BB CC DD " & Mid(tempStr, 1, 2) & " " _
                             & Mid(tempStr, 3, 2) & " " _
                             & Mid(tempStr, 5, 2) & " " _
                             & Mid(tempStr, 7, 2) & " " _
                             & Mid(tempStr, 9, 2) & " " _
                             & Mid(tempStr, 11, 2) & " " _
                             & Mid(tempStr, 13, 2) & " " _
                             & Mid(tempStr, 15, 2) & " " _
                             & Mid(tempStr, 17, 2) & " " _
                             & Mid(tempStr, 19, 2) & " " _
                             & Mid(tempStr, 21, 2) & " " _
                             & Mid(tempStr, 23, 2)
                             
    
    lst_Log.AddItem "    <<" & Mid(tempStr, 25, 2) & " " _
                             & Mid(tempStr, 27, 2) & " " _
                             & Mid(tempStr, 29, 2) & " " _
                             & Mid(tempStr, 31, 2) & " " _
                             & Mid(tempStr, 33, 2) & " " _
                             & Mid(tempStr, 35, 2) & " " _
                             & Mid(tempStr, 37, 2) & " " _
                             & Mid(tempStr, 39, 2) & " " _
                             & Mid(tempStr, 41, 2) & " " _
                             & Mid(tempStr, 43, 2) & " " _
                             & Mid(tempStr, 45, 2) & " " _
                             & Mid(tempStr, 47, 2) & " " _
                             & Mid(tempStr, 49, 2)
                             
    
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

    txtMaxBalance.Text = Get_Balance(CInt("&H" & Mid(tempStr, 29, 2)), CInt("&H" & Mid(tempStr, 31, 2)), CInt("&H" & Mid(tempStr, 33, 2)))
    
    txtInqAmount.Text = Get_Balance(CInt("&H" & Mid(tempStr, 11, 2)), CInt("&H" & Mid(tempStr, 13, 2)), CInt("&H" & Mid(tempStr, 15, 2)))


End Sub

Private Function Get_Balance(ByVal Data1 As Byte, ByVal Data2 As Byte, ByVal Data3 As Byte) As String
    Dim TotalBalance As Long
    
            'Get Total Balance
            TotalBalance = Data1 * 65536
            
            TotalBalance = TotalBalance + (Data2 * CLng(256))
            
            TotalBalance = TotalBalance + Data3
            
            Get_Balance = CStr(TotalBalance)
            
End Function

Private Sub Form_Load()

    'Initialize object
    opt_DES.Value = True
    
End Sub


Private Sub txt_New_PIN_KeyPress(KeyAscii As Integer)
    
    'Verify Key
    KeyAscii = KeyVerify_Hex(KeyAscii)
    
End Sub

Private Sub txt_PIN_KeyPress(KeyAscii As Integer)
    
    'Verify Key
    KeyAscii = KeyVerify_Hex(KeyAscii)
    
End Sub

Private Sub txtCreditAmount_KeyPress(KeyAscii As Integer)
    
    'Verify Key
    If Not IsNumeric(Chr(KeyAscii)) And KeyAscii <> 8 Then
        KeyAscii = 7
    End If
    
End Sub

Private Sub txtDebitAmount_KeyPress(KeyAscii As Integer)
    
    'Verify Key
    If Not IsNumeric(Chr(KeyAscii)) And KeyAscii <> 8 Then
        KeyAscii = 7
    End If
    
End Sub

Private Sub txtSAMGPIN_KeyPress(KeyAscii As Integer)
    
    'Verify Key
    KeyAscii = KeyVerify_Hex(KeyAscii)
    
End Sub
