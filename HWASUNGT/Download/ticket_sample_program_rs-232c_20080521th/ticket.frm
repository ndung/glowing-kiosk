VERSION 5.00
Object = "{648A5603-2C6E-101B-82B6-000000000014}#1.1#0"; "mscomm32.ocx"
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   3825
   ClientLeft      =   1890
   ClientTop       =   1920
   ClientWidth     =   6240
   LinkTopic       =   "Form1"
   ScaleHeight     =   3825
   ScaleWidth      =   6240
   Begin VB.CommandButton Command3 
      Caption         =   "Print ->Toward3"
      Height          =   615
      Left            =   2520
      TabIndex        =   2
      Top             =   1440
      Width           =   2895
   End
   Begin VB.CommandButton Command2 
      Caption         =   "Command2"
      Height          =   615
      Left            =   4800
      TabIndex        =   1
      Top             =   3000
      Width           =   1215
   End
   Begin MSCommLib.MSComm MSComm1 
      Left            =   3960
      Top             =   3120
      _ExtentX        =   1005
      _ExtentY        =   1005
      _Version        =   393216
      DTREnable       =   -1  'True
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Print ->Toward1"
      Height          =   645
      Left            =   2520
      TabIndex        =   0
      Top             =   600
      Width           =   2925
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Private Sub Command1_Click()
    
    ' set page mode
    MSComm1.Output = Chr$(&H1B) & "L"   ' PAGE MODE
    ' print toward
    MSComm1.Output = Chr$(&H1B) & "T" & Chr$(1)
   
    ' x,y coordinate
    MSComm1.Output = Chr$(&H1B) & "W" & "0300" & "0300"
    ' output string
    MSComm1.Output = "ABCDEFGHIJKLMN12345" & Chr$(&HD)
    
    ' -------- PAGE AREA PRINTING ---------------------
    MSComm1.Output = Chr$(&H1B) & Chr$(&HC)
 
   ' ---------- CUTTING POSITION ----------------------
   ' must be adjust to your ticket size
    MSComm1.Output = Chr$(&H1A) & "b" & Chr$(1)     ' FORWARD BLACK MARK SEARCH
    MSComm1.Output = Chr$(&H1B) & "J" & Chr$(80)    ' ADJUST CUTTING POSITION
    MSComm1.Output = Chr$(&H1D) & "V" & Chr$(0)     ' FULL CUTTING
    MSComm1.Output = Chr$(&H1B) & "j" & Chr$(120)   ' ADJUST NEXT PAGE PRINT POSITION
    
    '---------- PAGE AREA CLEAR AND TO STANDARD MODE -----------
    MSComm1.Output = Chr$(&H1B) & "S"
  
End Sub

Private Sub Command2_Click()

    MSComm1.PortOpen = False
    End
End Sub

Private Sub Command3_Click()
    
    ' set page mode
    MSComm1.Output = Chr$(&H1B) & "L"   ' PAGE MODE
    ' print toward
    MSComm1.Output = Chr$(&H1B) & "T" & Chr$(3)
   
    ' x,y coordinate
    MSComm1.Output = Chr$(&H1B) & "W" & "0300" & "0300"
    ' output string
    MSComm1.Output = "ABCDEFGHIJKLMN12345" & Chr$(&HD)
    
    ' -------- PAGE AREA PRINTING ---------------------
    MSComm1.Output = Chr$(&H1B) & Chr$(&HC)
 
   ' ---------- CUTTING POSITION ----------------------
   ' must be adjust to your ticket size
    MSComm1.Output = Chr$(&H1A) & "b" & Chr$(1)     ' FORWARD BLACK MARK SEARCH
    MSComm1.Output = Chr$(&H1B) & "J" & Chr$(80)    ' ADJUST CUTTING POSITION
    MSComm1.Output = Chr$(&H1D) & "V" & Chr$(0)     ' FULL CUTTING
    MSComm1.Output = Chr$(&H1B) & "j" & Chr$(120)   ' ADJUST NEXT PRINT POSITION
    
    '---------- PAGE AREA CLEAR AND TO STANDARD MODE -----------
    MSComm1.Output = Chr$(&H1B) & "S"
  
End Sub

Private Sub Form_Load()
    
    MSComm1.CommPort = 2              ' COM1
    MSComm1.Settings = "19200,n,8,1"  'SET RS-232C"
    MSComm1.Handshaking = comRTS
    MSComm1.RTSEnable = True
       
    MSComm1.InputLen = 1
    MSComm1.InputMode = comInputModeText
    MSComm1.RThreshold = 1
    
    If MSComm1.PortOpen = False Then MSComm1.PortOpen = True
 
End Sub



