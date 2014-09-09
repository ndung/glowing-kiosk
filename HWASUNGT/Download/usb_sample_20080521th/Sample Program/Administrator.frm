VERSION 5.00
Begin VB.Form Form3 
   Caption         =   "Administrator"
   ClientHeight    =   3090
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   4680
   LinkTopic       =   "Form3"
   ScaleHeight     =   3090
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows ±âº»°ª
   Begin VB.CommandButton Command2 
      Caption         =   "End"
      Height          =   495
      Left            =   2400
      TabIndex        =   2
      Top             =   1920
      Width           =   1575
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Reprint"
      Height          =   495
      Left            =   2400
      TabIndex        =   1
      Top             =   1320
      Width           =   1455
   End
   Begin VB.TextBox Text1 
      Height          =   375
      Left            =   2520
      MaxLength       =   3
      TabIndex        =   0
      Text            =   "Text1"
      Top             =   480
      Width           =   1095
   End
   Begin VB.Label Label1 
      Caption         =   "Serial No."
      Height          =   255
      Left            =   1680
      TabIndex        =   3
      Top             =   600
      Width           =   855
   End
End
Attribute VB_Name = "Form3"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Command1_Click()


wait:

If Text1.Text < 1 Then
    MsgBox " Print No > 1 "
    GoTo wait
End If

AdminiSerialCnt = Text1.Text

If PrintMode = 1 Then
    PrintMode = 0       ' administrator mode
    Call Form1.PrintFormat1
    PrintMode = 1
ElseIf PrintMode = 2 Then
    PrintMode = 0
    Call Form1.PrintFormat2
    PrintMode = 2
ElseIf PrintMode = 3 Then
    PrintMode = 0
    Call Form1.PrintFormat3
    PrintMode = 3
End If

MyEnd:

End Sub

Private Sub Command2_Click()
    
    Form3.Hide
    Administrator = False

End Sub

Private Sub Form_Load()
    Text1.Text = ""
End Sub

Private Sub Text1_Change()
 
 a = Len(Text1.Text)
  
    For i = 1 To a
        a = Mid(Text1.Text, i, 1)
        If Asc(a) < 48 Or Asc(a) > 57 Then GoTo Err
    Next i
   
    If Text1.Text = Empty Then GoTo MyEnd
  
    If Text1.Text > 99 Or Text1.Text < 1 Then GoTo Err2
    
    GoTo MyEnd
    
Err:
   MsgBox "The value must be Numeric."
   Text1.Text = ""
    GoTo MyEnd
Err2:
   MsgBox "The value is 1 ~ 99"
   Text2.Text = ""

MyEnd:




End Sub
