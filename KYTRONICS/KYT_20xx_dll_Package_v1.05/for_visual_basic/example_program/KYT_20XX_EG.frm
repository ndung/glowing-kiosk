VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "KYT-20XX"
   ClientHeight    =   3615
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4830
   BeginProperty Font 
      Name            =   "±¼¸²"
      Size            =   9
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   LinkTopic       =   "Form1"
   ScaleHeight     =   3615
   ScaleWidth      =   4830
   StartUpPosition =   3  'Windows ±âº»°ª
   Begin VB.CommandButton mo_bt_enable2 
      Caption         =   "EnablePort 19200"
      Height          =   375
      Left            =   2520
      TabIndex        =   6
      Top             =   2160
      Width           =   2175
   End
   Begin VB.CommandButton mo_et_set_baudrate 
      Caption         =   "set_baudrate 19200"
      BeginProperty Font 
         Name            =   "±¼¸²"
         Size            =   9
         Charset         =   129
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   120
      TabIndex        =   5
      Top             =   3120
      Width           =   2175
   End
   Begin VB.CommandButton mo_bt_issue 
      Caption         =   "issue"
      BeginProperty Font 
         Name            =   "±¼¸²"
         Size            =   9
         Charset         =   129
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   2520
      TabIndex        =   4
      Top             =   3120
      Width           =   2175
   End
   Begin VB.CommandButton mo_bt_disable 
      Caption         =   "DisablePort"
      BeginProperty Font 
         Name            =   "±¼¸²"
         Size            =   9
         Charset         =   129
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   120
      TabIndex        =   3
      Top             =   2640
      Width           =   2175
   End
   Begin VB.CommandButton mo_bt_enable 
      Caption         =   "EnablePort 9600"
      BeginProperty Font 
         Name            =   "±¼¸²"
         Size            =   9
         Charset         =   129
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   2520
      TabIndex        =   2
      Top             =   2640
      Width           =   2175
   End
   Begin VB.CommandButton mo_bt_call_src_ver 
      Caption         =   "call_src_ver"
      Height          =   375
      Left            =   120
      TabIndex        =   1
      Top             =   2160
      Width           =   2175
   End
   Begin VB.TextBox mo_et_view 
      BackColor       =   &H00404040&
      BeginProperty Font 
         Name            =   "±¼¸²"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FFFFFF&
      Height          =   1935
      Left            =   120
      MultiLine       =   -1  'True
      TabIndex        =   0
      Top             =   120
      Width           =   4575
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Form_Unload(Cancel As Integer)
 Dim ah_res As Boolean
 ah_res = DisablePort()
 delay (10)
End Sub

Private Sub mo_bt_call_src_ver_Click()
 mo_et_view = _
 "[call_src_ver]"

 call_src_ver
End Sub

Private Sub mo_bt_disable_Click()
 Dim ah_res As Boolean
 ah_res = DisablePort()
 
 If ah_res = False Then
    mo_et_view = "[DisablePort] FALSE"
 Else
    mo_et_view = "[DisablePort] GOOD"
 End If
End Sub

Private Sub mo_bt_enable_Click()
 Dim ah_res As Boolean
 ah_res = EnablePort("COM1", 8, 0, 0, 9600, 0)

 If ah_res = False Then
    mo_et_view = "[EnablePort 9600] FALSE"
 Else
    mo_et_view = "[EnablePort 9600] GOOD"

 End If
End Sub

Private Sub mo_bt_enable2_Click()
 Dim ah_res As Boolean
 ah_res = EnablePort("COM1", 8, 0, 0, 19200, 0)

 If ah_res = False Then
    mo_et_view = "[EnablePort 19200] FALSE"
 Else
    mo_et_view = "[EnablePort 19200] GOOD"

 End If
End Sub

Private Sub mo_bt_issue_Click()
 Dim al_res As Long
 Dim ab_old_prc_no As Byte
 Dim as_stat(10) As Byte
 Dim as_prc_no(10) As Byte
 Dim al_errno As Long

 ab_old_prc_no = issue(3)

 Do
    al_res = chk_res(VarPtr(as_stat(0)), VarPtr(as_prc_no(0)), VarPtr(al_errno))
    delay (10)
 Loop While al_res <> 1

'Log Display
 mo_et_view = _
 "[issue]    " & Chr$(13) & Chr$(10) _
 & "as_stat  :" & Hex(as_stat(0)) & "," & Hex(as_stat(1)) & Chr$(13) & Chr$(10) _
 & "al_errno :" & al_errno & Chr$(13) & Chr$(10) _
 & "as_prc_no:" & as_prc_no(0)
 
 If as_prc_no(0) <> ab_old_prc_no Then
    MsgBox ("Exception handling ---------- ")
 End If
 
 If al_errno <> 0 Then
    MsgBox ("Exception handling ---------- ")
 End If
 
 '
 'Etc processing, "as_stat" variable check ...
 
End Sub

Private Sub mo_et_set_baudrate_Click()
 Dim al_res As Long
 Dim ab_old_prc_no As Byte
 Dim as_stat(10) As Byte
 Dim as_prc_no(10) As Byte
 Dim al_errno As Long
 Dim ah_res As Boolean
 
 ab_old_prc_no = set_baudrate(81)

 Do
    al_res = chk_res(VarPtr(as_stat(0)), VarPtr(as_prc_no(0)), VarPtr(al_errno))
    delay (10)
 Loop While al_res <> 1

 If as_prc_no(0) <> ab_old_prc_no Then
    MsgBox ("Exception handling ---------- ")
 ElseIf al_errno <> 0 Then
    MsgBox ("Exception handling ---------- ")
 Else
    '
    'Etc processing, "as_stat" variable check ...

    delay (100)
    ah_res = DisablePort()
    delay (100)
    ah_res = EnablePort("COM1", 8, 0, 0, 19200, 0)
    
    If ah_res = False Then
        mo_et_view = "[set_baudrate 19200] FALSE"
    Else
       mo_et_view = "[set_baudrate 19200] GOOD"
    End If
 End If
End Sub
