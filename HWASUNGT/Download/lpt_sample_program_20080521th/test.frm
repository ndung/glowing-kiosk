VERSION 5.00
Begin VB.Form Form1 
   BackColor       =   &H80000000&
   BorderStyle     =   1  '단일 고정
   Caption         =   "HM_UP4MP Update"
   ClientHeight    =   3825
   ClientLeft      =   1875
   ClientTop       =   1905
   ClientWidth     =   6240
   FillColor       =   &H8000000A&
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   ScaleHeight     =   3825
   ScaleWidth      =   6240
   StartUpPosition =   2  '화면 가운데
   Begin VB.CommandButton Command2 
      Caption         =   "Exit"
      Height          =   350
      Left            =   5200
      TabIndex        =   1
      ToolTipText     =   "프로그램을 종료합니다"
      Top             =   3300
      Width           =   735
   End
   Begin VB.CommandButton Command1 
      Caption         =   "TEST"
      Height          =   350
      Left            =   4080
      TabIndex        =   0
      ToolTipText     =   "업데이트를 시작합니다"
      Top             =   3300
      Width           =   855
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False


Private Sub Command1_Click()

    Open "LPT1" For Output As #1

    Print #1, "ABCD1234" & Chr$(&HA)            ' output string
    
    Print #1, Chr$(&HA) & Chr$(&HA) & Chr$(&HA) ' LF
    Print #1, Chr$(&HA) & Chr$(&HA) & Chr$(&HA)
    Print #1, Chr$(&HA) & Chr$(&HA) & Chr$(&HA)
    Print #1, Chr$(&H1B) & "i"                  ' cutting

    Close #1

   
End Sub

Private Sub Command2_Click()

End

End Sub

