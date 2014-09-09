VERSION 5.00
Object = "{463B5BD3-AE10-4DB6-81F0-943CEE379A01}#1.0#0"; "HwaUSB.ocx"
Begin VB.Form Form1 
   Caption         =   "VB ¼ÀÇÃ ¿¹Á¦"
   ClientHeight    =   7905
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   11205
   LinkTopic       =   "Form1"
   ScaleHeight     =   7905
   ScaleWidth      =   11205
   StartUpPosition =   2  'CenterScreen
   Begin VB.TextBox Text1 
      Height          =   3615
      Left            =   360
      Locked          =   -1  'True
      MultiLine       =   -1  'True
      TabIndex        =   8
      Text            =   "Form1.frx":0000
      Top             =   600
      Width           =   4935
   End
   Begin VB.Timer Timer1 
      Left            =   7440
      Top             =   5640
   End
   Begin VB.CommandButton Command5 
      Caption         =   "status clear"
      Height          =   615
      Left            =   6960
      TabIndex        =   7
      Top             =   4200
      Width           =   2775
   End
   Begin VB.CommandButton Command4 
      Caption         =   "PRINTER Status"
      Height          =   615
      Left            =   6960
      TabIndex        =   6
      Top             =   3480
      Width           =   2775
   End
   Begin VB.CommandButton Command2 
      Caption         =   "Print with Windows Driver (Landsape,Ticket)"
      Height          =   615
      Left            =   6840
      TabIndex        =   5
      Top             =   1680
      Width           =   2775
   End
   Begin VB.TextBox Text2 
      Height          =   375
      Left            =   6840
      MaxLength       =   3
      TabIndex        =   3
      Text            =   "Text2"
      Top             =   480
      Width           =   1215
   End
   Begin VB.CommandButton Command6 
      Caption         =   "End"
      Height          =   495
      Left            =   8400
      TabIndex        =   2
      Top             =   5640
      Width           =   1575
   End
   Begin VB.CommandButton Command3 
      Caption         =   "Print with OCX Driver"
      Height          =   615
      Left            =   6840
      TabIndex        =   1
      Top             =   2400
      Width           =   2775
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Print with Windows Driver"
      Height          =   615
      Left            =   6840
      TabIndex        =   0
      Top             =   960
      Width           =   2775
   End
   Begin HWAUSBLib.HwaUSB HwaUSB1 
      Left            =   2040
      Top             =   4920
      _Version        =   65536
      _ExtentX        =   1931
      _ExtentY        =   1508
      _StockProps     =   0
   End
   Begin VB.Label Label2 
      Caption         =   "Continuous Printer Status:"
      Height          =   255
      Left            =   360
      TabIndex        =   9
      Top             =   360
      Width           =   2415
   End
   Begin VB.Label Label1 
      Caption         =   "Number of Print"
      Height          =   255
      Left            =   6840
      TabIndex        =   4
      Top             =   240
      Width           =   1455
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Command1_Click()

Dim a
Dim X As Printer
Dim pic As Picture

PrintMode = 1
   
' search for windows target printer priter
SUCCESS = False
For Each X In Printers
    If X.DeviceName = "HWASUNG HMK - 80" Then
        Set Printer = X
        SUCCESS = True
        Exit For
    End If
Next

If Not SUCCESS Then
   MsgBox "There is no Target Printer(HWASUNG HMK - 80)", vbExclamation
   GoTo MyEnd
End If



For i = 1 To Text2.Text

wait:
    DoEvents

' Printer Status Check before Print
    a = HwaUSB1.RealRead
    Call Status_Display(a)

' Print End check!
    If (a And 16) Then
        GoTo wait      ' print not end
    Else
                    ' print end then next print
    End If
' Printer error check!
    If (a And &HE7) Then    ' Near End and printing flag Mask,3rd bit(Can Print)
        GoTo wait
    Else
    End If


    Call PrintFormat1       ' Print with Widows Driver(Normal)

    SerialCnt = SerialCnt + 1


Next i

MyEnd:

End Sub


Private Sub Command2_Click()

Dim mystring As String


PrintMode = 2

Dim a
Dim X As Printer
Dim pic As Picture

PrintMode = 2
   
   
' search for windows target printer
SUCCESS = False
For Each X In Printers
    If X.DeviceName = "HWASUNG HMK - 80" Then
        Set Printer = X
        SUCCESS = True
        Exit For
    End If
Next

If Not SUCCESS Then
   MsgBox "There is no Target Printer(HWASUNG HMK - 80)", vbExclamation
   GoTo MyEnd
End If


myloop:
DoEvents

For i = 1 To Text2.Text

wait:

    DoEvents
' Printer Status Check before Print
    a = HwaUSB1.RealRead
    Call Status_Display(a)

' Print End check!
    If (a And 16) Then
        GoTo wait      ' print not end
    Else
                    ' print end then next print
    End If
' Printer error check!
    If (a And &HE7) Then    ' Near End and Printing flag Mask,3rd bit(Can Print)
        GoTo wait
    Else
    End If


    Call PrintFormat2       ' Print with Windows Driver(Landscape)


    SerialCnt = SerialCnt + 1

Next i


MyEnd:


End Sub

Private Sub Command3_Click()

PrintMode = 3

For i = 1 To Text2.Text

wait:
    DoEvents
' Printer Status Check before Print
    a = HwaUSB1.RealRead
    Call Status_Display(a)

' Print End check!
    If (a And 16) Then
        GoTo wait      ' print not end
    Else

                    ' print end then next print
    End If
' Printer error check!
    If (a And &HE7) Then   ' Near End and printing flag Mask,3rd bit(Can Print)
        GoTo wait
    Else
    End If

    Call PrintFormat3       ' Print with OCX Driver
  
    SerialCnt = SerialCnt + 1


Next i

MyEnd:




End Sub

Private Sub Command4_Click()

a = HwaUSB1.RealRead
Text3.Text = Text3.Text & a & ","


End Sub

Public Sub PrintFormat1()

'--- Print with Windows Driver(Normal) ---------

' pixel( 1pixel = 0.125mm, for our printer)

    Printer.Orientation = 1     ' portrait mode
    Printer.ScaleMode = 3
 

' print picture
    Set pic = LoadPicture("Bone.bmp")
    Printer.PaintPicture pic, 20, 0, 320, 300


    Printer.FontName = Arial
    Printer.FontSize = 10

    Printer.CurrentX = 20       ' left margin 2.5mm( 0.125mm per 1)

    If PrintMode = 0 Then       ' administrator mode
        Printer.Print "PRINT No." & AdminiSerialCnt
    Else
        Printer.Print "PRINT No." & SerialCnt
    End If
    
    Printer.CurrentX = Printer.CurrentX
        
    
    Printer.CurrentX = 20       ' 20 = left margin 2.5mm( 0.125mm per 1)
    Printer.Print "[01] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[02] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[03] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[04] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[05] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[06] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[07] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[08] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[09] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[10] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[11] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[12] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[13] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[14] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[15] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[16] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[17] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[18] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[19] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[20] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[21] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[22] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[23] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[24] This is Windows Font!"
    
    Printer.CurrentX = 20
    Printer.Print "[25] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[26] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[27] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[28] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[29] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[30] This is Windows Font!"
    Printer.CurrentX = 20
    Printer.Print "[31] This is Windows Font!"

'----- form feed to cutting position ---------
    Printer.Print " " & vbLf
    Printer.Print " " & vbLf
    Printer.Print " " & vbLf

    Printer.FontSize = 2
    Printer.Print "."                   ' dummy print for form feed

    Printer.EndDoc

End Sub

Public Sub PrintFormat2()

'--- Print with Windows Driver(Landscape) ---------

    Printer.FontName = Arial
    Printer.FontSize = 10
    Printer.PaperSize = 9
    Printer.Orientation = 2         ' landscape mode

'-----------------------------------------------------------
' Scale mode pixel : 1pixel=0.125mm for our printer
'-----------------------------------------------------------
    Printer.ScaleMode = 3           ' pixel(1dot=0.125mm)
  
    Printer.CurrentX = 0            ' X position(0.125mm per 1)
    Printer.CurrentY = 240          ' Y position(0.125mm per 1)
    
    If PrintMode = 0 Then           ' administrator mode
        Printer.Print "PRINT No." & AdminiSerialCnt & vbCrLf
    Else
        Printer.Print "PRINT No." & SerialCnt & vbCrLf
    End If

    Printer.CurrentY = Printer.CurrentY + 10        ' 10 = gap(1.25mm)
    Printer.Print "Seat No : A-32"
   
    Printer.CurrentY = Printer.CurrentY + 10
    Printer.Print "DATE : " & Date
  
    Printer.CurrentY = Printer.CurrentY + 10
    Printer.Print "Time : " & Time
  
    Printer.FontSize = 16
    Printer.FontBold = True
    Printer.CurrentY = Printer.CurrentY + 10
    Printer.Print "Charge : $2"
 
    Printer.FontSize = 10
    Printer.FontBold = False
    Printer.CurrentY = Printer.CurrentY + 10
    Printer.Print "Discount 30%"
  
    
' print picture
    Printer.CurrentX = 320
    Printer.CurrentY = 300
    Set pic = LoadPicture("Bone.bmp")
    Printer.PaintPicture pic, Printer.CurrentX, Printer.CurrentY, 300, 200     ' x,y coordinate and scale
    
    
    Printer.CurrentX = 728          ' X position
    Printer.CurrentY = 240          ' Y position
    
    If PrintMode = 0 Then           ' administrator mode
        Printer.Print "User No." & AdminiSerialCnt & vbCrLf
    Else
        Printer.Print "User No." & SerialCnt & vbCrLf
    End If
    
    Printer.CurrentX = 728
    Printer.CurrentY = Printer.CurrentY + 10
    Printer.Print "Seat No : A-32"
   
    Printer.CurrentX = 728
    Printer.CurrentY = Printer.CurrentY + 10
    Printer.Print "DATE : " & Date
  
    Printer.CurrentX = 728
    Printer.CurrentY = Printer.CurrentY + 10
    Printer.Print "Time : " & Time
  
    Printer.CurrentX = 728
    Printer.CurrentY = Printer.CurrentY + 10
    Printer.Print "Charge : $2"
 
    Printer.CurrentX = 728
    Printer.CurrentY = Printer.CurrentY + 10
    Printer.Print "Discount 30%"
  
    
    Printer.EndDoc


End Sub
Public Sub PrintFormat3()

'--- Print with OCX Driver ---------

' initial line to line value, printer command
    HwaUSB1.PrintCmd &H1B
    HwaUSB1.PrintCmd &H32


    If PrintMode = 0 Then       ' administrator mode
        HwaUSB1.PrintStr "PRINT No." & AdminiSerialCnt & vbCrLf
    Else
        HwaUSB1.PrintStr "PRINT No." & SerialCnt & vbCrLf
    End If

 
    HwaUSB1.PrintStr "[01]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[02]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[03]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[04]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[05]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[06]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[07]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[08]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[09]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[10]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[11]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[12]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[13]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[14]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[15]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[16]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[17]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[18]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[19]:printer font!" & vbCrLf
    HwaUSB1.PrintStr "[20]:printer font!" & vbCrLf

' printer command feed to cutting position
    HwaUSB1.PrintCmd &H1B
    HwaUSB1.PrintCmd &H4A
    HwaUSB1.PrintCmd &HA0

' cutting command
    HwaUSB1.PrintCmd &H1B
    ''HwaUSB1.PrintCmd &H69        ' full cut
    HwaUSB1.PrintCmd &H6D        ' partial cut


End Sub



Private Sub Status_Display(a)

DoEvents

If a = -1 Then
    Form2.Text1.Text = "[Received Data]:" & a & ", " & "Printer OFFLINE!" & vbCrLf & _
                                                 "Now Reopening..." & vbCrLf & _
                                                 "Check the printer cable and printer power!"
    Command1.Enabled = False
    Command2.Enabled = False
    Administrator = True       ' Administrator mode
    HwaUSB1.Close
    ReOpen_Target              ' reopen target printer
    
ElseIf (a And 1) = 1 Then
  Form2.Text1.Text = "[Received Data]:" & a & ", " & "Printer Paper Out!" & vbCrLf & "Please call in Administrator!!!" & vbCrLf
    Command1.Enabled = False
    Command2.Enabled = False
    Administrator = True       ' Administrator mode
ElseIf (a And 2) = 2 Then
  Form2.Text1.Text = "[Received Data]:" & a & ", " & "Printer Cover Open!" & vbCrLf & "Please call in Administrator!!!" & vbCrLf
  Command1.Enabled = False
  Command2.Enabled = False
  Administrator = True       ' Administrator mode
ElseIf (a And 4) = 4 Then
  Form2.Text1.Text = "[Received Data]:" & a & ", " & "Printer Paper Jam!" & vbCrLf & "Please call in Administrator!!!" & vbCrLf
  Command1.Enabled = False
  Command2.Enabled = False
  Administrator = True       ' Administrator mode
ElseIf (a And 16) = 16 Then
  Form2.Text1.Text = "[Received Data]:" & a & ", " & "Now Printing!" & vbCrLf

ElseIf (a And 8) = 8 Then   ' if near end, not error
  Form2.Text1.Text = "[Received Data]:" & a & ", " & "Printer Near End!(Can Print!) " & vbCrLf
  Command1.Enabled = True
  Command2.Enabled = True
'---------------------------------------------------------------------------
' recovery printer error
'' print stop cause printer error then control the previous form
    If (Administrator = True) Then
        Form3.Show
    End If
'-----------------------------------------------------------------------------

ElseIf (a And 32) = 32 Then
  Form2.Text1.Text = "[Received Data]:" & a & ", " & "Printer Cutter Jam!" & vbCrLf & "Please call in Administrator!!!" & vbCrLf
  Command1.Enabled = False
  Command2.Enabled = False
  Administrator = True       ' Administrator mode
Else                         ' Printer status OK!
  Form2.Text1.Text = "[Received Data]:" & a & ", " & "Printer OK!" & vbCrLf
  Command1.Enabled = True
  Command2.Enabled = True
'---------------------------------------------------------------------------
' recovery printer error
'' print stop cause printer error then control the previous form
    If (Administrator = True) Then
        Form3.Show
    End If
'-----------------------------------------------------------------------------

End If



Text1.Text = Text1.Text & "," & a
If Len(Text1.Text) > 512 Then
    Text1.Text = ""
End If


End Sub

Private Sub Command5_Click()

Text1.Text = ""

End Sub

Private Sub Command6_Click()

End

End Sub

'--------------------------------------------
Private Sub Form_Load()
    
   Do
        DoEvents
        Form2.Show
        
        a = HwaUSB1.Open("HMK-80")   ' open target model HWASUNG HMK - 80

        If a = 0 Then                     ' success to open then 0
            Form2.Text1.Text = "[Returned Data]:" & a & ",Printer Open Success!"
            GoTo MyEnd
        Else                                     ' fail to open then not 0
        Form2.Text1.Text = "[Returned Data]:" & a & ",Printer OFFLINE!" & vbCrLf & _
                                                 "Now Reopening..." & vbCrLf & _
                                                 "Check the printer cable and printer power!"
         End If
    Loop Until (a = 0)
      
MyEnd:
    
    Administrator = False       ' not Administrator mode
    SerialCnt = 1
    SerialAdminiCnt = 1
    Text1.Text = ""
    Text2.Text = 1

    Timer1.Interval = 500       ' do not setting below 100msec
    Timer1.Enabled = True


End Sub

Private Sub Form_Unload(Cancel As Integer)
    
    HwaUSB1.Close
    Timer1.Enabled = False

End Sub
 Private Sub ReOpen_Target()

 a = HwaUSB1.Open("HMK - 80")   ' open target model
 
End Sub


Private Sub Text2_Change()
  a = Len(Text2.Text)
  
    For i = 1 To a
        a = Mid(Text2.Text, i, 1)
        If Asc(a) < 48 Or Asc(a) > 57 Then GoTo Err
    Next i
   
    If Text2.Text = Empty Then GoTo MyEnd
  
    If Text2.Text > 99 Or Text2.Text < 1 Then GoTo Err2
    
    GoTo MyEnd
    
Err:
   MsgBox "The value must be Numeric."
   Text2.Text = ""
    GoTo MyEnd
Err2:
   MsgBox "The value is 1 ~ 99"
   Text2.Text = ""

MyEnd:


End Sub

Private Sub Timer1_Timer()

DoEvents
PrnStatus = HwaUSB1.RealRead
Status_Display (PrnStatus)

End Sub

Private Sub Timer2_Timer()

timer2_f = 1

End Sub
