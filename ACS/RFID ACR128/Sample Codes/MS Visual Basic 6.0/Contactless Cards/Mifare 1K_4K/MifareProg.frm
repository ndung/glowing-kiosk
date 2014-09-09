VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form MainMifareProg 
   Caption         =   "Mifare Card programming"
   ClientHeight    =   9855
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   9990
   Icon            =   "MifareProg.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   9855
   ScaleWidth      =   9990
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   8040
      TabIndex        =   42
      Top             =   9360
      Width           =   1815
   End
   Begin VB.CommandButton bReset 
      Caption         =   "&Reset"
      Height          =   375
      Left            =   6120
      TabIndex        =   41
      Top             =   9360
      Width           =   1815
   End
   Begin VB.CommandButton bClear 
      Caption         =   "C&lear"
      Height          =   375
      Left            =   4200
      TabIndex        =   40
      Top             =   9360
      Width           =   1815
   End
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   6135
      Left            =   4200
      TabIndex        =   63
      Top             =   3120
      Width           =   5655
      _ExtentX        =   9975
      _ExtentY        =   10821
      _Version        =   393217
      ScrollBars      =   2
      TextRTF         =   $"MifareProg.frx":17A2
   End
   Begin VB.Frame frValBlk 
      Caption         =   "Value Block Functions"
      Height          =   2895
      Left            =   4200
      TabIndex        =   58
      Top             =   120
      Width           =   5655
      Begin VB.CommandButton bValRes 
         Caption         =   "Res&tore Value"
         Height          =   375
         Left            =   3360
         TabIndex        =   43
         Top             =   2280
         Width           =   1935
      End
      Begin VB.CommandButton bValRead 
         Caption         =   "R&ead Value"
         Height          =   375
         Left            =   3360
         TabIndex        =   39
         Top             =   1800
         Width           =   1935
      End
      Begin VB.CommandButton bValDec 
         Caption         =   "&Decrement"
         Height          =   375
         Left            =   3360
         TabIndex        =   38
         Top             =   1320
         Width           =   1935
      End
      Begin VB.CommandButton bValInc 
         Caption         =   "I&ncrement"
         Height          =   375
         Left            =   3360
         TabIndex        =   37
         Top             =   840
         Width           =   1935
      End
      Begin VB.CommandButton bValStore 
         Caption         =   "&Store Value"
         Height          =   375
         Left            =   3360
         TabIndex        =   36
         Top             =   360
         Width           =   1935
      End
      Begin VB.TextBox tValTar 
         Height          =   285
         Left            =   1320
         MaxLength       =   3
         TabIndex        =   35
         Top             =   1800
         Width           =   615
      End
      Begin VB.TextBox tValSrc 
         Height          =   285
         Left            =   1320
         MaxLength       =   3
         TabIndex        =   34
         Top             =   1320
         Width           =   615
      End
      Begin VB.TextBox tValBlk 
         Height          =   285
         Left            =   1320
         MaxLength       =   3
         TabIndex        =   33
         Top             =   840
         Width           =   615
      End
      Begin VB.TextBox tValAmt 
         Height          =   285
         Left            =   1320
         MaxLength       =   10
         TabIndex        =   32
         Top             =   360
         Width           =   1935
      End
      Begin VB.Label Label13 
         Caption         =   "Target Block"
         Height          =   255
         Left            =   240
         TabIndex        =   62
         Top             =   1800
         Width           =   2055
      End
      Begin VB.Label Label12 
         Caption         =   "Source Block"
         Height          =   255
         Left            =   240
         TabIndex        =   61
         Top             =   1320
         Width           =   2055
      End
      Begin VB.Label Label11 
         Caption         =   "Block No."
         Height          =   255
         Left            =   240
         TabIndex        =   60
         Top             =   840
         Width           =   1935
      End
      Begin VB.Label Label10 
         Caption         =   "Value Amount"
         Height          =   255
         Left            =   240
         TabIndex        =   59
         Top             =   360
         Width           =   1695
      End
   End
   Begin VB.Frame frBinOps 
      Caption         =   "Binary Block Functions"
      Height          =   2055
      Left            =   120
      TabIndex        =   54
      Top             =   7680
      Width           =   3975
      Begin VB.CommandButton tbinUpd 
         Caption         =   "&Update Block"
         Height          =   375
         Left            =   2040
         TabIndex        =   31
         Top             =   1440
         Width           =   1695
      End
      Begin VB.CommandButton bBinRead 
         Caption         =   "Read &Block"
         Height          =   375
         Left            =   120
         TabIndex        =   30
         Top             =   1440
         Width           =   1695
      End
      Begin VB.TextBox tBinData 
         Height          =   285
         Left            =   120
         TabIndex        =   29
         Top             =   960
         Width           =   3615
      End
      Begin VB.TextBox tBinLen 
         Height          =   285
         Left            =   3000
         MaxLength       =   2
         TabIndex        =   28
         Top             =   360
         Width           =   495
      End
      Begin VB.TextBox tBinBlk 
         Height          =   285
         Left            =   1560
         MaxLength       =   3
         TabIndex        =   27
         Top             =   360
         Width           =   495
      End
      Begin VB.Label Label9 
         Caption         =   "Data (text)"
         Height          =   255
         Left            =   120
         TabIndex        =   57
         Top             =   720
         Width           =   1815
      End
      Begin VB.Label Label8 
         Caption         =   "Length"
         Height          =   255
         Left            =   2400
         TabIndex        =   56
         Top             =   360
         Width           =   615
      End
      Begin VB.Label Label7 
         Caption         =   "Start Block (Dec)"
         Height          =   255
         Left            =   120
         TabIndex        =   55
         Top             =   360
         Width           =   1335
      End
   End
   Begin VB.Frame frAuth 
      Caption         =   "Authentication Function"
      Height          =   3615
      Left            =   120
      TabIndex        =   48
      Top             =   3960
      Width           =   3975
      Begin VB.CommandButton bAuth 
         Caption         =   "&Authenticate"
         Height          =   375
         Left            =   2280
         TabIndex        =   26
         Top             =   3120
         Width           =   1455
      End
      Begin VB.TextBox tKeyIn6 
         Height          =   285
         Left            =   3360
         MaxLength       =   2
         TabIndex        =   25
         Top             =   2640
         Width           =   375
      End
      Begin VB.TextBox tKeyIn5 
         Height          =   285
         Left            =   3000
         MaxLength       =   2
         TabIndex        =   24
         Top             =   2640
         Width           =   375
      End
      Begin VB.TextBox tKeyIn4 
         Height          =   285
         Left            =   2640
         MaxLength       =   2
         TabIndex        =   23
         Top             =   2640
         Width           =   375
      End
      Begin VB.TextBox tKeyIn3 
         Height          =   285
         Left            =   2280
         MaxLength       =   2
         TabIndex        =   22
         Top             =   2640
         Width           =   375
      End
      Begin VB.TextBox tkeyIn2 
         Height          =   285
         Left            =   1920
         MaxLength       =   2
         TabIndex        =   21
         Top             =   2640
         Width           =   375
      End
      Begin VB.TextBox tKeyIn1 
         Height          =   285
         Left            =   1560
         MaxLength       =   2
         TabIndex        =   20
         Top             =   2640
         Width           =   375
      End
      Begin VB.TextBox tKeyAdd 
         Height          =   285
         Left            =   1560
         MaxLength       =   2
         TabIndex        =   19
         Top             =   2280
         Width           =   375
      End
      Begin VB.TextBox tBlkNo 
         Height          =   285
         Left            =   1560
         MaxLength       =   3
         TabIndex        =   18
         Top             =   1920
         Width           =   375
      End
      Begin VB.Frame frKType 
         Caption         =   "Key Type"
         Height          =   1455
         Left            =   2280
         TabIndex        =   50
         Top             =   360
         Width           =   1455
         Begin VB.OptionButton rbKeyB 
            Caption         =   "Key B"
            Height          =   375
            Left            =   120
            TabIndex        =   17
            Top             =   840
            Width           =   975
         End
         Begin VB.OptionButton rbKeyA 
            Caption         =   "Key A"
            Height          =   375
            Left            =   120
            TabIndex        =   16
            Top             =   360
            Width           =   975
         End
      End
      Begin VB.Frame frSource 
         Caption         =   "Key Source"
         Height          =   1455
         Left            =   120
         TabIndex        =   49
         Top             =   360
         Width           =   2055
         Begin VB.OptionButton rbNonVol 
            Caption         =   "Non-Volatile Memory"
            Height          =   255
            Left            =   120
            TabIndex        =   15
            Top             =   960
            Width           =   1815
         End
         Begin VB.OptionButton rbVolatile 
            Caption         =   "Volatile Memeory"
            Height          =   255
            Left            =   120
            TabIndex        =   14
            Top             =   600
            Width           =   1575
         End
         Begin VB.OptionButton rbManual 
            Caption         =   "Manual Input"
            Height          =   255
            Left            =   120
            TabIndex        =   13
            Top             =   240
            Width           =   1455
         End
      End
      Begin VB.Label Label6 
         Caption         =   "Key Value Input"
         Height          =   255
         Left            =   240
         TabIndex        =   53
         Top             =   2760
         Width           =   1335
      End
      Begin VB.Label Label5 
         Caption         =   "Key Store No."
         Height          =   255
         Left            =   240
         TabIndex        =   52
         Top             =   2400
         Width           =   1335
      End
      Begin VB.Label Label4 
         Caption         =   "Block No (Dec)"
         Height          =   255
         Left            =   240
         TabIndex        =   51
         Top             =   2040
         Width           =   1215
      End
   End
   Begin VB.Frame frLoadKeys 
      Caption         =   "Store Authentications Keys to Device"
      Height          =   2055
      Left            =   120
      TabIndex        =   45
      Top             =   1800
      Width           =   3975
      Begin VB.CommandButton bLoadKey 
         Caption         =   "Load &Key"
         Height          =   375
         Left            =   2280
         TabIndex        =   12
         Top             =   1560
         Width           =   1455
      End
      Begin VB.TextBox tKey6 
         Height          =   285
         Left            =   3360
         MaxLength       =   2
         TabIndex        =   11
         Top             =   1080
         Width           =   375
      End
      Begin VB.TextBox tKey5 
         Height          =   285
         Left            =   3000
         MaxLength       =   2
         TabIndex        =   10
         Top             =   1080
         Width           =   375
      End
      Begin VB.TextBox tKey4 
         Height          =   285
         Left            =   2640
         MaxLength       =   2
         TabIndex        =   9
         Top             =   1080
         Width           =   375
      End
      Begin VB.TextBox tkey3 
         Height          =   285
         Left            =   2280
         MaxLength       =   2
         TabIndex        =   8
         Top             =   1080
         Width           =   375
      End
      Begin VB.TextBox tKey2 
         Height          =   285
         Left            =   1920
         MaxLength       =   2
         TabIndex        =   7
         Top             =   1080
         Width           =   375
      End
      Begin VB.TextBox tKey1 
         Height          =   285
         Left            =   1560
         MaxLength       =   2
         TabIndex        =   6
         Top             =   1080
         Width           =   375
      End
      Begin VB.TextBox tMemAdd 
         Height          =   285
         Left            =   1560
         MaxLength       =   2
         TabIndex        =   5
         Top             =   720
         Width           =   375
      End
      Begin VB.OptionButton rbVolMem 
         Caption         =   "Volatile Memory"
         Height          =   255
         Left            =   2280
         TabIndex        =   4
         Top             =   360
         Width           =   1455
      End
      Begin VB.OptionButton rbNonVolMem 
         Caption         =   "Non-Volatile MEmory"
         Height          =   255
         Left            =   240
         TabIndex        =   3
         Top             =   360
         Width           =   1815
      End
      Begin VB.Label Label3 
         Caption         =   "Key Value Input"
         Height          =   255
         Left            =   240
         TabIndex        =   47
         Top             =   1200
         Width           =   1575
      End
      Begin VB.Label Label2 
         Caption         =   "Key Store No."
         Height          =   255
         Left            =   240
         TabIndex        =   46
         Top             =   840
         Width           =   1095
      End
   End
   Begin VB.CommandButton bConnect 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   2400
      TabIndex        =   2
      Top             =   1200
      Width           =   1695
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initialize"
      Height          =   375
      Left            =   2400
      TabIndex        =   1
      Top             =   720
      Width           =   1695
   End
   Begin VB.ComboBox cbReader 
      Height          =   315
      Left            =   1200
      TabIndex        =   0
      Top             =   240
      Width           =   2895
   End
   Begin VB.Label Label1 
      Caption         =   "Select Reader"
      Height          =   255
      Left            =   120
      TabIndex        =   44
      Top             =   360
      Width           =   1455
   End
End
Attribute VB_Name = "MainMifareProg"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              MifareProg.frm
'
'  Description:       This sample program outlines the steps on how to
'                     transact with Mifare 1K/4K cards using ACR128
'
'  Author:            M.J.E.C. Castillo
'
'  Date:              June 17, 2008
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
Dim SendLen, RecvLen As Long
Dim SendBuff(0 To 262) As Byte
Dim RecvBuff(0 To 262) As Byte

Private Sub bAuth_Click()

    'validate input
    If tBlkNo.Text = "" Then
    
        tBlkNo.SetFocus
        Exit Sub
    
    End If
    
    If CInt(tBlkNo.Text) > 319 Then
    
        tBlkNo.Text = 319
        Exit Sub
    
    End If
    
    If rbManual.Value = True Then
    
        If tKeyIn1.Text = "" Then
        
            tKey1.SetFocus
            Exit Sub
        
        End If
        
        If tkeyIn2.Text = "" Then
        
            tKey2.SetFocus
            Exit Sub
        
        End If
        
        If tKeyIn3.Text = "" Then
        
            tkey3.SetFocus
            Exit Sub
        
        End If
        
        If tKeyIn4.Text = "" Then
        
            tKey4.SetFocus
            Exit Sub
        
        End If
        
        If tKeyIn5.Text = "" Then
        
            tKey5.SetFocus
            Exit Sub
        
        End If
        
        If tKeyIn6.Text = "" Then
        
            tKey6.SetFocus
            Exit Sub
        
        End If
    
    Else
    
        If rbNonVol.Value = True Then
        
            If tKeyAdd.Text = "" Then
            
                tKeyAdd.SetFocus
                Exit Sub
            
            End If
            
            If CInt("&H" & tKeyAdd.Text) > &H1F Then
            
                tKeyAdd.Text = "1F"
                Exit Sub
            
            End If
        
        End If
    
    End If
    
    If rbManual.Value = True Then
    
        '1. store value in volatile memory
        Call ClearBuffers
        SendBuff(0) = &HFF                                'CLS
        SendBuff(1) = &H82                                'INS
        SendBuff(2) = &H0                                 'P1: Volatile Memory
        SendBuff(3) = &H20                                'P2: Session Key
        SendBuff(4) = &H6                                 'P3
        SendBuff(5) = CInt("&H" & tKeyIn1.Text)           'Key1 Value
        SendBuff(6) = CInt("&H" & tkeyIn2.Text)           'Key2 Value
        SendBuff(7) = CInt("&H" & tKeyIn3.Text)           'Key3 Value
        SendBuff(8) = CInt("&H" & tKeyIn4.Text)           'Key4 Value
        SendBuff(9) = CInt("&H" & tKeyIn5.Text)           'Key5 Value
        SendBuff(10) = CInt("&H" & tKeyIn6.Text)          'Key6 Value
        SendLen = &HB
        RecvLen = &H2
    
        retCode = SendAPDUandDisplay(0)
    
        If retCode <> SCARD_S_SUCCESS Then
    
            Exit Sub
    
        End If
        
        '2. use volatile memory to authenticate
        Call ClearBuffers
        SendBuff(0) = &HFF                      'CLA
        SendBuff(2) = &H0                       'P1: same for all source types
        SendBuff(1) = &H86                      'INS: for stored key input
        SendBuff(3) = &H0                       'P2: for stored key input
        SendBuff(4) = &H5                       'P3: for stored key input
        SendBuff(5) = &H1                       'Byte 1: version number
        SendBuff(6) = &H0                       'Byte 2
        SendBuff(7) = CInt(tBlkNo.Text)         'Byte 3: sectore no. for stored key input
        
        If rbKeyA.Value = True Then
        
            SendBuff(8) = &H60                  'Byte 4 : Key A for stored key input
        
        Else
        
            SendBuff(8) = &H61                  'Byte 4 : Key B for stored key input
        
        End If
        
        SendBuff(9) = &H20                      'Byte 5 : Session key for volatile memory
        
    Else
        Call ClearBuffers
        SendBuff(0) = &HFF                      'CLA
        SendBuff(2) = &H0                       'P1: same for all source types
        SendBuff(1) = &H86                      'INS: for stored key input
        SendBuff(3) = &H0                       'P2: for stored key input
        SendBuff(4) = &H5                       'P3: for stored key input
        SendBuff(5) = &H1                       'Byte 1: version number
        SendBuff(6) = &H0                       'Byte 2
        SendBuff(7) = CInt(tBlkNo.Text)         'Byte 3: sectore no. for stored key input
        
        If rbKeyA.Value = True Then
        
            SendBuff(8) = &H60                  'Byte 4 : Key A for stored key input
        
        Else
        
            SendBuff(8) = &H61                  'Byte 4 : Key B for stored key input
        
        End If
        
        If rbVolatile.Value = True Then
        
            SendBuff(9) = &H20                  'Byte 5 : Session key for volatile memory
            
        Else
        
            SendBuff(9) = CInt("&H" & tKeyAdd.Text) 'Byte 5 : Session key for non-volatile memory
        
        End If
    
    End If
    
'    If rbManual.Value = True Then
    
'        SendLen = &HB
        
'    Else
    
        SendLen = &HA
    
'    End If
    
    RecvLen = &H2
    
    retCode = SendAPDUandDisplay(0)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If

End Sub

Private Sub bBinRead_Click()

Dim tmpStr As String
Dim indx As Integer

    'validate input
    tBinData.Text = ""
    
    If tBinBlk.Text = "" Then
    
        tBinBlk.SetFocus
        Exit Sub
    
    End If
    
    If tBinLen.Text = "" Then
    
        tBinLen.SetFocus
        Exit Sub
    
    End If
    If CInt(tBinBlk.Text) > 319 Then
    
        tBinBlk.Text = "319"
        Exit Sub
    
    End If
    
    Call ClearBuffers
    SendBuff(0) = &HFF                  'CLA
    SendBuff(1) = &HB0                  'INS
    SendBuff(2) = &H0                   'P1
    SendBuff(3) = CInt(tBinBlk.Text)    'P2: Starting block no.
    SendBuff(4) = CInt(tBinLen.Text)    'P3: Data Length
    
    SendLen = &H5
    RecvLen = SendBuff(4) + 2
    
    retCode = SendAPDUandDisplay(2)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
        
    End If
    
    'Display data in text format
    tmpStr = ""
    For indx = 0 To RecvLen - 1
    
        tmpStr = tmpStr & Chr(RecvBuff(indx))
    
    Next indx
    
    tBinData.Text = tmpStr

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
    frLoadKeys.Enabled = True
    frAuth.Enabled = True
    frBinOps.Enabled = True
    frValBlk.Enabled = True
    rbManual.Value = True
    rbKeyA.Value = True

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

Private Sub bLoadKey_Click()

    'check inputs
    If ((rbNonVolMem.Value = False) And (rbVolMem.Value = False)) Then
    
        rbNonVolMem.SetFocus
        Exit Sub
    
    End If
    
    If rbNonVolMem.Value = True Then
    
        If tMemAdd.Text = "" Then
        
            tMemAdd.SetFocus
            Exit Sub
        
        End If
        
        If CInt("&H" & tMemAdd.Text) > &H1F Then
        
            tMemAdd.Text = "1F"
            Exit Sub
        
        End If
    
    End If
    
    If tKey1.Text = "" Then
    
        tKey1.SetFocus
        Exit Sub
    
    End If
    
        If tKey2.Text = "" Then
    
        tKey2.SetFocus
        Exit Sub
    
    End If
    
        If tkey3.Text = "" Then
    
        tkey3.SetFocus
        Exit Sub
    
    End If
    
        If tKey4.Text = "" Then
    
        tKey4.SetFocus
        Exit Sub
    
    End If
    
        If tKey5.Text = "" Then
    
        tKey5.SetFocus
        Exit Sub
    
    End If
    
        If tKey6.Text = "" Then
    
        tKey6.SetFocus
        Exit Sub
    
    End If
    
    Call ClearBuffers
    SendBuff(0) = &HFF                              'CLS
    SendBuff(1) = &H82                              'INS
    
    If rbNonVolMem.Value = True Then
    
        SendBuff(2) = &H20                          'P1: Non Volatile Memory
        SendBuff(3) = CInt("&H" & tMemAdd.Text)     'P2: Memory Location
        
    Else
    
        SendBuff(2) = &H0                           'P1: Volatile Memory
        SendBuff(3) = &H20                          'P2: Session Key
    
    End If
    
    SendBuff(4) = &H6                               'P3
    SendBuff(5) = CInt("&H" & tKey1.Text)           'Key1 Value
    SendBuff(6) = CInt("&H" & tKey2.Text)           'Key2 Value
    SendBuff(7) = CInt("&H" & tkey3.Text)           'Key3 Value
    SendBuff(8) = CInt("&H" & tKey4.Text)           'Key4 Value
    SendBuff(9) = CInt("&H" & tKey5.Text)           'Key5 Value
    SendBuff(10) = CInt("&H" & tKey6.Text)          'Key6 Value
    SendLen = &HB
    RecvLen = &H2
    
    retCode = SendAPDUandDisplay(0)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If

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
  'MsgBox tmpStr
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
        
    End Select
    
    Call DisplayOut(3, 0, tmpStr)
  End If
  SendAPDUandDisplay = retCode
  
End Function

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

Private Sub bValDec_Click()

Dim amount As Long
Dim tmpVal As Long
Dim indx As Integer

    'validate input
    If tValAmt.Text = "" Then
    
        tValAmt.SetFocus
        Exit Sub
    
    End If
    
    'VB6 only supports up to 2,147,483,647 in Long data type
    If CLng(tValAmt.Text) > 2147483647 Then
    
        tValAmt.Text = "2147483647"
        tValAmt.SetFocus
        Exit Sub
    
    End If
    
    If tValBlk.Text = "" Then
    
        tValBlk.SetFocus
        Exit Sub
    
    End If
    
    If CInt(tValBlk.Text) > 319 Then
    
        tValBlk.Text = "319"
        tValBlk.SetFocus
        Exit Sub
    
    End If
    
    tValSrc.Text = ""
    tValTar.Text = ""
    
    amount = CLng(tValAmt.Text)
    Call ClearBuffers
    
    SendBuff(0) = &HFF                          'CLA
    SendBuff(1) = &HD7                          'INS
    SendBuff(2) = &H0                           'P1
    SendBuff(3) = CInt(tValBlk.Text)            'P2: Block No.
    SendBuff(4) = &H5                           'Lc: Data Length
    SendBuff(5) = &H2                           'VB_OP Value
    
    'shift bit to right
    tmpVal = amount
    For indx = 1 To 24
       
        tmpVal = tmpVal / 2
        
    Next indx
    
    SendBuff(6) = tmpVal And &HFF               'Amount MSByte
    
    tmpVal = amount
    For indx = 1 To 16
       
        tmpVal = tmpVal / 2
        
    Next indx
    
    SendBuff(7) = tmpVal And &HFF               'amount middle byte
    
    tmpVal = amount
    For indx = 1 To 8
       
        tmpVal = tmpVal / 2
        
    Next indx
    
    SendBuff(8) = tmpVal And &HFF               'amount middle byte
    SendBuff(9) = amount And &HFF               'amount LSByte
    
    SendLen = SendBuff(4) + 5
    RecvLen = &H2
    
    retCode = SendAPDUandDisplay(2)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
        
    End If

End Sub

Private Sub bValInc_Click()

Dim amount As Long
Dim tmpVal As Long
Dim indx As Integer

    'validate input
    If tValAmt.Text = "" Then
    
        tValAmt.SetFocus
        Exit Sub
    
    End If
    
    'VB6 only supports up to 2,147,483,647 in Long data type
    If CLng(tValAmt.Text) > 2147483647 Then
    
        tValAmt.Text = "2147483647"
        tValAmt.SetFocus
        Exit Sub
    
    End If
    
    If tValBlk.Text = "" Then
    
        tValBlk.SetFocus
        Exit Sub
    
    End If
    
    If CInt(tValBlk.Text) > 319 Then
    
        tValBlk.Text = "319"
        tValBlk.SetFocus
        Exit Sub
    
    End If
    
    tValSrc.Text = ""
    tValTar.Text = ""
    
    amount = CLng(tValAmt.Text)
    Call ClearBuffers
    
    SendBuff(0) = &HFF                          'CLA
    SendBuff(1) = &HD7                          'INS
    SendBuff(2) = &H0                           'P1
    SendBuff(3) = CInt(tValBlk.Text)            'P2: Block No.
    SendBuff(4) = &H5                           'Lc: Data Length
    SendBuff(5) = &H1                           'VB_OP Value
    
    'shift bit to right
    tmpVal = amount
    For indx = 1 To 24
       
        tmpVal = tmpVal / 2
        
    Next indx
    
    SendBuff(6) = tmpVal And &HFF               'Amount MSByte
    
    tmpVal = amount
    For indx = 1 To 16
       
        tmpVal = tmpVal / 2
        
    Next indx
    
    SendBuff(7) = tmpVal And &HFF               'amount middle byte
    
    tmpVal = amount
    For indx = 1 To 8
       
        tmpVal = tmpVal / 2
        
    Next indx
    
    SendBuff(8) = tmpVal And &HFF               'amount middle byte
    SendBuff(9) = amount And &HFF               'amount LSByte
    
    SendLen = SendBuff(4) + 5
    RecvLen = &H2
    
    retCode = SendAPDUandDisplay(2)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
        
    End If

End Sub

Private Sub bValRead_Click()

Dim amount As Long

    'validate inputs
    If tValBlk.Text = "" Then
    
        tValBlk.SetFocus
        Exit Sub
    
    End If
    
    If CInt(tValBlk.Text) > 319 Then
    
        tValBlk.Text = "319"
        Exit Sub
    
    End If
    
    tValAmt.Text = ""
    tValSrc.Text = ""
    tValSrc.Text = ""
    
    Call ClearBuffers
    SendBuff(0) = &HFF          'CLA
    SendBuff(1) = &HB1          'INS
    SendBuff(2) = &H0           'P1
    SendBuff(3) = CInt(tValBlk.Text) 'P2: Block No.
    SendBuff(4) = &H0           'Le
    
    SendLen = &H5
    RecvLen = &H6
    
    retCode = SendAPDUandDisplay(2)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If

    amount = RecvBuff(3)
    amount = amount + (RecvBuff(2) * 256)
    amount = amount + (RecvBuff(1) * 65536)     '256 * 256
    amount = amount + (RecvBuff(0) * 16777216)  '256 * 256 * 256
    tValAmt.Text = CStr(amount)

End Sub

Private Sub bValRes_Click()

    'validate input
    If tValSrc.Text = "" Then
    
        tValSrc.SetFocus
        Exit Sub
    
    End If
    
    If tValTar.Text = "" Then
    
        tValTar.SetFocus
        Exit Sub
    
    End If
    
    If CInt(tValSrc.Text) > 319 Then
    
        tValSrc.Text = "319"
        Exit Sub
    
    End If
    
    If CInt(tValTar.Text) > 319 Then
    
        tValTar.Text = "319"
        Exit Sub
    
    End If
    
    tValAmt.Text = ""
    tValBlk.Text = ""
    
    Call ClearBuffers
    SendBuff(0) = &HFF                  'CLA
    SendBuff(1) = &HD7                  'INS
    SendBuff(2) = &H0                   'P1
    SendBuff(3) = CInt(tValSrc.Text)    'P2: Source block no
    SendBuff(4) = &H2                   'Lc
    SendBuff(5) = &H3                   'Data in byte 1
    SendBuff(6) = CInt(tValTar.Text)    'P2: target block
    
    SendLen = &H7
    RecvLen = &H2
    
    retCode = SendAPDUandDisplay(2)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    

End Sub

Private Sub bValStore_Click()

Dim amount As Long
Dim tmpVal As Long
Dim excessVal As Long
Dim indx As Integer

    'validate input
    If tValAmt.Text = "" Then
    
        tValAmt.SetFocus
        Exit Sub
    
    End If

    'VB6 only supports up to 2,147,483,647 in Long data type
    If CLng(tValAmt.Text) > 2147483647 Then
    
        tValAmt.Text = "2147483647"
        tValAmt.SetFocus
        Exit Sub
    
    End If
    
    If tValBlk.Text = "" Then
    
        tValBlk.SetFocus
        Exit Sub
    
    End If
    
    If CInt(tValBlk.Text) > 319 Then
    
        tValBlk.Text = "319"
        tValBlk.SetFocus
        Exit Sub
    
    End If
    
    tValSrc.Text = ""
    tValTar.Text = ""
    
    amount = CLng(tValAmt.Text)
    Call ClearBuffers
    
    SendBuff(0) = &HFF                          'CLA
    SendBuff(1) = &HD7                          'INS
    SendBuff(2) = &H0                           'P1
    SendBuff(3) = CInt(tValBlk.Text)            'P2: Block No.
    SendBuff(4) = &H5                           'Lc: Data Length
    SendBuff(5) = &H0                           'VB_OP Value
    
    'shift bit to right
    tmpVal = amount
    For indx = 1 To 24
       
        tmpVal = tmpVal / 2
        
    Next indx
    
    SendBuff(6) = tmpVal And &HFF               'Amount MSByte
    
    tmpVal = amount
    For indx = 1 To 16
       
        tmpVal = tmpVal / 2
        
    Next indx
    
    SendBuff(7) = tmpVal And &HFF               'amount middle byte
    
    tmpVal = amount
    For indx = 1 To 8
       
        tmpVal = tmpVal / 2
        
    Next indx
    
    SendBuff(8) = tmpVal And &HFF               'amount middle byte
    SendBuff(9) = amount And &HFF               'amount LSByte
    
    SendLen = SendBuff(4) + 5
    RecvLen = &H2
    
    retCode = SendAPDUandDisplay(2)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
        
    End If

End Sub

Private Sub Form_Load()

    Call InitMenu

End Sub

Private Sub InitMenu()

    ConnActive = False
    mMsg.Text = ""
    cbReader.Clear
    Call DisplayOut(0, 0, "Program Ready")
    bConnect.Enabled = False
    bInit.Enabled = True
    bReset.Enabled = False
    rbNonVolMem.Value = False
    rbVolMem.Value = False
    tMemAdd.Text = ""
    tKey1.Text = ""
    tKey2.Text = ""
    tkey3.Text = ""
    tKey4.Text = ""
    tKey5.Text = ""
    tKey6.Text = ""
    frLoadKeys.Enabled = False
    tBlkNo.Text = ""
    tKeyAdd.Text = ""
    tKeyIn1.Text = ""
    tkeyIn2.Text = ""
    tKeyIn3.Text = ""
    tKeyIn4.Text = ""
    tKeyIn5.Text = ""
    tKeyIn6.Text = ""
    rbManual.Value = False
    rbNonVol.Value = False
    rbVolatile.Value = False
    rbKeyA.Value = False
    rbKeyB.Value = False
    frAuth.Enabled = False
    tBinBlk.Text = ""
    tBinLen.Text = ""
    tBinData.Text = ""
    frBinOps.Enabled = False
    tValAmt.Text = ""
    tValBlk.Text = ""
    tValSrc.Text = ""
    tValTar.Text = ""
    frValBlk.Enabled = False


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
      
    'for card errors
    Case 4
      mMsg.SelColor = vbRed
      
  End Select
  
  mMsg.SelText = PrintText & vbCrLf
  mMsg.SelStart = Len(mMsg.Text)
  mMsg.SelColor = vbBlack

End Sub

Private Sub rbManual_Click()

    tKeyAdd.Text = ""
    tKeyAdd.Enabled = False
    tBlkNo.Enabled = True
    tKeyIn1.Enabled = True
    tkeyIn2.Enabled = True
    tKeyIn3.Enabled = True
    tKeyIn4.Enabled = True
    tKeyIn5.Enabled = True
    tKeyIn6.Enabled = True

End Sub

Private Sub rbNonVol_Click()

    tKeyIn1.Text = ""
    tkeyIn2.Text = ""
    tKeyIn3.Text = ""
    tKeyIn4.Text = ""
    tKeyIn5.Text = ""
    tKeyIn6.Text = ""
    tKeyIn1.Enabled = False
    tkeyIn2.Enabled = False
    tKeyIn3.Enabled = False
    tKeyIn4.Enabled = False
    tKeyIn5.Enabled = False
    tKeyIn6.Enabled = False
    tKeyAdd.Enabled = True
    tBlkNo.Enabled = True

End Sub

Private Sub rbNonVolMem_Click()
    
    tMemAdd.Enabled = True
    
End Sub

Private Sub rbVolatile_Click()

    tKey1.Text = ""
    tKey2.Text = ""
    tkey3.Text = ""
    tKey4.Text = ""
    tKey5.Text = ""
    tKey6.Text = ""
    tKeyIn1.Enabled = False
    tkeyIn2.Enabled = False
    tKeyIn3.Enabled = False
    tKeyIn4.Enabled = False
    tKeyIn5.Enabled = False
    tKeyIn6.Enabled = False
    tKeyAdd.Enabled = False
    tBlkNo.Enabled = True

End Sub

Private Sub rbVolMem_Click()

    tMemAdd.Text = ""
    tMemAdd.Enabled = False

End Sub

Private Sub tbinUpd_Click()

Dim tmpStr As String
Dim indx As Integer
Dim charArray(0 To 99) As String

    'validate input
    If tBinData.Text = "" Then
    
        tBinData.SetFocus
        Exit Sub
    
    End If
    
    If tBinBlk.Text = "" Then
    
        tBinBlk.SetFocus
        Exit Sub
    
    End If
    
    If CInt(tBinBlk.Text) > 319 Then
    
        tBinBlk.Text = "319"
    
    End If
    
    If tBinLen.Text = "" Then
    
        tBinLen.SetFocus
        Exit Sub
    
    End If
    
    tmpStr = tBinData.Text
    
    Call ClearBuffers
    SendBuff(0) = &HFF                   'CLA
    SendBuff(1) = &HD6                   'INS
    SendBuff(2) = &H0                    'P1
    SendBuff(3) = CInt(tBinBlk.Text)     'P2: Starting block no.
    SendBuff(4) = CInt(tBinLen.Text)      'P3: Data Length

    'place each character in string to an array
    For indx = 1 To Len(tmpStr)
    
        charArray(indx) = Mid$(tmpStr, indx, 1)
    
    Next indx
    
    For indx = 0 To Len(tmpStr) - 1
    
        SendBuff(indx + 5) = Asc(charArray(indx + 1))
    
    Next indx
    
    SendLen = SendBuff(4) + 5
    RecvLen = &H2
    
    retCode = SendAPDUandDisplay(2)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If

End Sub

'for user input control

Private Sub tBinBlk_GotFocus()

    tBinBlk.SelStart = 0
    tBinBlk.SelLength = Len(tBinBlk.Text)

End Sub

Private Sub tBinBlk_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub


Private Sub tBinData_GotFocus()

    tBinData.SelStart = 0
    tBinData.SelLength = Len(tBinData.Text)

End Sub

Private Sub tBinLen_GotFocus()

    tBinLen.SelStart = 0
    tBinLen.SelLength = Len(tBinLen.Text)

End Sub

Private Sub tBinLen_KeyPress(KeyAscii As Integer)
    
    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tBlkNo_GotFocus()

    tBlkNo.SelStart = 0
    tBlkNo.SelLength = Len(tBlkNo.Text)

End Sub

Private Sub tBlkNo_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub



Private Sub tKey1_Change()

    If Len(tKey1.Text) = tKey1.MaxLength Then
    
       tKey2.SetFocus
    
    End If

End Sub

Private Sub tKey1_GotFocus()

    tKey1.SelStart = 0
    tKey1.SelLength = Len(tKey1.Text)

End Sub

Private Sub tKey2_Change()

    If Len(tKey2.Text) = tKey2.MaxLength Then
    
       tkey3.SetFocus
    
    End If

End Sub

Private Sub tKey2_GotFocus()

    tKey2.SelStart = 0
    tKey2.SelLength = Len(tKey2.Text)

End Sub

Private Sub tKey3_Change()

    If Len(tkey3.Text) = tkey3.MaxLength Then
    
       tKey4.SetFocus
    
    End If

End Sub

Private Sub tkey3_GotFocus()

    tkey3.SelStart = 0
    tkey3.SelLength = Len(tkey3.Text)

End Sub

Private Sub tKey4_Change()

    If Len(tKey4.Text) = tKey4.MaxLength Then
    
       tKey5.SetFocus
    
    End If

End Sub

Private Sub tKey4_GotFocus()

    tKey4.SelStart = 0
    tKey4.SelLength = Len(tKey4.Text)

End Sub

Private Sub tKey5_Change()

    If Len(tKey5.Text) = tKey5.MaxLength Then
    
       tKey6.SetFocus
    
    End If

End Sub


Private Sub tKey1_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If


End Sub


Private Sub tKey2_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If


End Sub


Private Sub tkey3_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If


End Sub


Private Sub tKey4_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If


End Sub


Private Sub tKey5_GotFocus()

    tKey5.SelStart = 0
    tKey5.SelLength = Len(tKey5.Text)

End Sub

Private Sub tKey5_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub


Private Sub tKey6_GotFocus()

    tKey6.SelStart = 0
    tKey6.SelLength = Len(tKey6.Text)

End Sub

Private Sub tKey6_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub




Private Sub tKeyAdd_GotFocus()

    tKeyAdd.SelStart = 0
    tKeyAdd.SelLength = Len(tKeyAdd.Text)

End Sub

Private Sub tKeyAdd_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub


Private Sub tKeyIn1_Change()

    If Len(tKeyIn1.Text) = tKeyIn1.MaxLength Then
    
       tkeyIn2.SetFocus
    
    End If
    
End Sub

Private Sub tKeyIn1_GotFocus()

    tKeyIn1.SelStart = 0
    tKeyIn1.SelLength = Len(tKeyIn1.Text)

End Sub

Private Sub tKeyIn2_Change()

    If Len(tkeyIn2.Text) = tkeyIn2.MaxLength Then
    
       tKeyIn3.SetFocus
    
    End If
    
End Sub

Private Sub tkeyIn2_GotFocus()

    tkeyIn2.SelStart = 0
    tkeyIn2.SelLength = Len(tkeyIn2.Text)

End Sub

Private Sub tKeyIn3_Change()

    If Len(tKeyIn3.Text) = tKeyIn3.MaxLength Then
    
       tKeyIn4.SetFocus
    
    End If
    
End Sub

Private Sub tKeyIn3_GotFocus()

    tKeyIn3.SelStart = 0
    tKeyIn3.SelLength = Len(tKeyIn3.Text)

End Sub

Private Sub tKeyIn4_Change()

    If Len(tKeyIn4.Text) = tKeyIn4.MaxLength Then
    
       tKeyIn5.SetFocus
    
    End If
    
End Sub

Private Sub tKeyIn4_GotFocus()

    tKeyIn4.SelStart = 0
    tKeyIn4.SelLength = Len(tKeyIn4.Text)

End Sub

Private Sub tKeyIn5_Change()

    If Len(tKeyIn5.Text) = tKeyIn5.MaxLength Then
    
       tKeyIn6.SetFocus
    
    End If
    
End Sub

Private Sub tKeyIn1_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub


Private Sub tkeyIn2_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tKeyIn3_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub


Private Sub tKeyIn4_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub


Private Sub tKeyIn5_GotFocus()

    tKeyIn5.SelStart = 0
    tKeyIn5.SelLength = Len(tKeyIn5.Text)

End Sub

Private Sub tKeyIn5_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub


Private Sub tKeyIn6_GotFocus()

    tKeyIn6.SelStart = 0
    tKeyIn6.SelLength = Len(tKeyIn6.Text)

End Sub

Private Sub tKeyIn6_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If


End Sub

Private Sub tMemAdd_Change()

    If Len(tMemAdd.Text) = tMemAdd.MaxLength Then
    
       tKey1.SetFocus
    
    End If

End Sub

Private Sub tMemAdd_GotFocus()

    tMemAdd.SelStart = 0
    tMemAdd.SelLength = Len(tMemAdd.Text)

End Sub

Private Sub tMemAdd_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If


End Sub

Private Sub tValAmt_GotFocus()

    tValAmt.SelStart = 0
    tValAmt.SelLength = Len(tValAmt.Text)

End Sub

Private Sub tValAmt_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tValBlk_GotFocus()

    tValBlk.SelStart = 0
    tValBlk.SelLength = Len(tValBlk.Text)

End Sub

Private Sub tValBlk_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub


Private Sub tValSrc_GotFocus()

    tValSrc.SelStart = 0
    tValSrc.SelLength = Len(tValSrc.Text)

End Sub

Private Sub tValSrc_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub


Private Sub tValTar_GotFocus()

    tValTar.SelStart = 0
    tValTar.SelLength = Len(tValTar.Text)

End Sub

Private Sub tValTar_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub
