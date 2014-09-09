VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   0  'None
   Caption         =   "Form1"
   ClientHeight    =   5985
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   5985
   ScaleWidth      =   4680
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox Text3 
      Height          =   495
      Left            =   3000
      TabIndex        =   13
      Top             =   4680
      Width           =   1215
   End
   Begin VB.CommandButton SSP3 
      Caption         =   "Set Note Route"
      Height          =   495
      Left            =   1680
      TabIndex        =   12
      Top             =   4680
      Width           =   1215
   End
   Begin VB.TextBox Text2 
      Height          =   405
      Left            =   3000
      TabIndex        =   11
      Top             =   3360
      Width           =   1455
   End
   Begin VB.CommandButton SSP2 
      Caption         =   "Float"
      Height          =   495
      Left            =   1680
      TabIndex        =   10
      Top             =   3360
      Width           =   1215
   End
   Begin VB.Timer Timer7 
      Left            =   3240
      Top             =   5400
   End
   Begin VB.Timer Timer6 
      Left            =   2640
      Top             =   5400
   End
   Begin VB.CommandButton ACR 
      Caption         =   "Test Contact Reader"
      Height          =   495
      Left            =   120
      TabIndex        =   9
      Top             =   5280
      Width           =   1335
   End
   Begin VB.CommandButton Printer 
      Caption         =   "Test Print"
      Height          =   375
      Left            =   1680
      TabIndex        =   8
      Top             =   4080
      Width           =   1215
   End
   Begin VB.CommandButton SSP1 
      Caption         =   "Empty SmartPayout"
      Height          =   435
      Left            =   120
      TabIndex        =   7
      Top             =   4680
      Width           =   1335
   End
   Begin VB.Timer Timer5 
      Left            =   2040
      Top             =   5400
   End
   Begin VB.CommandButton Print 
      Caption         =   "Print"
      Height          =   375
      Left            =   120
      TabIndex        =   6
      Top             =   4080
      Width           =   1335
   End
   Begin VB.PictureBox MSComm1 
      Height          =   480
      Left            =   2280
      ScaleHeight     =   420
      ScaleWidth      =   1140
      TabIndex        =   14
      Top             =   2640
      Width           =   1200
   End
   Begin VB.Timer Timer4 
      Left            =   3000
      Top             =   2040
   End
   Begin VB.Timer Timer3 
      Left            =   2280
      Top             =   2040
   End
   Begin VB.Timer Timer2 
      Left            =   3000
      Top             =   480
   End
   Begin VB.Timer Timer1 
      Left            =   2280
      Top             =   480
   End
   Begin VB.PictureBox Winsock1 
      Height          =   480
      Left            =   1680
      ScaleHeight     =   420
      ScaleWidth      =   1140
      TabIndex        =   15
      Top             =   2160
      Width           =   1200
   End
   Begin VB.CommandButton BillAcceptor 
      Caption         =   "Accept Note"
      Height          =   435
      Left            =   120
      TabIndex        =   5
      Top             =   1320
      Width           =   1335
   End
   Begin VB.CommandButton CashDispenser 
      Caption         =   "Dispense Note"
      Height          =   495
      Left            =   120
      TabIndex        =   4
      Top             =   3360
      Width           =   1335
   End
   Begin VB.CommandButton BA 
      Caption         =   "Run System SSP"
      Height          =   495
      Left            =   120
      TabIndex        =   3
      Top             =   2640
      Width           =   1335
   End
   Begin VB.CommandButton SmartPayout 
      Caption         =   "Open Port SSP"
      Height          =   495
      Left            =   120
      TabIndex        =   2
      Top             =   1920
      Width           =   1335
   End
   Begin VB.TextBox Text1 
      Height          =   405
      Left            =   1560
      TabIndex        =   1
      Top             =   1320
      Width           =   1815
   End
   Begin VB.CommandButton CardDispenser 
      Caption         =   "Dispense Card"
      Height          =   495
      Left            =   120
      TabIndex        =   0
      Top             =   720
      Width           =   1335
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Const DISP_ON_COL = &H0&
Const DISP_OFF_COL = &H808080

Const PAY_PROCESS_COL = &HFF0000
Const PAY_DONE_COL = &HC000&
Const PAY_ERROR_COL = &HC0&

Dim sysValue As PAY_SYSTEM_ACCOUNT
Dim send As Boolean
Dim blHalt As Boolean
Dim newPayout As Boolean
Dim haltReq As Boolean
Dim newFloat As Boolean
Dim blExit As Boolean
Dim blTimerEnd As Boolean
Dim blBusy As Boolean
Dim newPayoutEmpty As Boolean
Dim newHopperEmpty As Boolean
Dim newPayoutReset As Boolean
Dim newHopperReset As Boolean
Dim printerInit As Boolean
Dim endPoll As Boolean
Public smartPayoutInit As Boolean
Public smartPayoutHalt As Boolean
Dim isOpenReader As Boolean
Dim lastNotevalue As Long
Dim amount As String
Dim conn As ADODB.Connection
Dim rs As ADODB.Recordset
Dim cmd As ADODB.Command

Dim trxid As String
        
Private Sub ACR_Click()
    InitializeReader
    ConnectReader
    'FormatCard
    InquiryBalance
'    TopUpBalance (10)
    DebitBalance (1.3)
    InquiryBalance
End Sub

Private Sub BA_Click()
    RunSmartPayout
End Sub

Private Sub BillAcceptor_Click()
    SendMsg ("0024RQ0202004950" & RightJustify(Text1.text, 12))
End Sub

Private Sub CardDispenser_Click()
    EnabledCardDispenserPort9600
    TakeOutCard
End Sub

Private Function InitializePrinter() As Boolean

    Dim strPort As String
    
    Set cmd = New ADODB.Command
    cmd.ActiveConnection = conn
    cmd.CommandType = adCmdText
    cmd.CommandText = "SELECT device_port from device WHERE device_code = '02'"
    Set rs = cmd.Execute
    Do While Not rs.EOF
        strPort = rs("device_port")
        rs.MoveNext
    Loop
    Set rs = Nothing
    Set cmd = Nothing
        
    'MSComm1.CommPort = 1              ' COM1
    MSComm1.CommPort = CInt(strPort)
    MSComm1.Settings = "19200,n,8,1"  'SET RS-232C"
    MSComm1.Handshaking = comNone
    MSComm1.RTSEnable = True
       
    MSComm1.InputLen = 1
    MSComm1.InputMode = comInputModeText
    MSComm1.RThreshold = 1
    
    ' If MSComm1.PortOpen = False Then MSComm1.PortOpen = True
    MSComm1.OutBufferSize = 1024          ' 32 byte is the Limit.
    MSComm1.InBufferSize = 512
    
    MSComm1.RThreshold = 1
    MSComm1.SThreshold = 1
    MSComm1.PortOpen = True
    If MSComm1.PortOpen = False Then
        MSComm1.PortOpen = True
        printerInit = True
    End If
    
    Call WriteLog("Printer status : " & MSComm1.PortOpen)
    
    MSComm1.OutBufferCount = 0     ' clear the output buffer
    MSComm1.InBufferCount = 0      'clear the input buffer
    Sleep (1000)
    MSComm1.Output = Chr$(&H1B) & "@"
    Sleep (1000)
End Function

Private Function StopPrinter() As Boolean
    MSComm1.PortOpen = False
End Function

Private Function InitializeSmartPayout() As Boolean
Dim dllVersion(3) As Byte

    ' initialise the global ssp packet paramters
    ' Call SaveSetting("PayInPayOutSystem", "STARTUP", "SSPPAYOUTADDRESS", Val("&H" & Val("00")))
    ' Call SaveSetting("PayInPayOutSystem", "STARTUP", "PAYOUTPORT", 5)
    
    Dim strPort As String
    
    Set cmd = New ADODB.Command
    cmd.ActiveConnection = conn
    cmd.CommandType = adCmdText
    cmd.CommandText = "SELECT device_port from device WHERE device_code = '01'"
    Set rs = cmd.Execute
    Do While Not rs.EOF
        strPort = rs("device_port")
        rs.MoveNext
    Loop
    Set rs = Nothing
    Set cmd = Nothing
    
    strPort = "COM" & strPort

    sspPayout.BaudRate = 9600
    'sspPayout.PortNumber = get_com_port_number_string("COM5")
    sspPayout.PortNumber = get_com_port_number_string(strPort)
    sspPayout.RetryLevel = 3
    sspPayout.sspAddress = Val("&H" & Val("00"))
    sspPayout.TimeOut = 1000
    sspPayout.ignoreerror = 0
    
    If Not OpenCommunicationPorts Then
        smartPayoutInit = False
        Call WriteLog("Can't open smart payout device port, restart machine...")
        RestartMachine
        Exit Function
    End If
    
    Timer3.Interval = 50
    Timer3.Enabled = True

    'Do
    '    DoEvents
    'Loop Until Timer3.Enabled = False
    'hpset.ConnectionStatus = TestDevicePresence(sspHopper, sspHopperInfo)
    
    '    Timer3.Enabled = True
    Do
        DoEvents
    Loop Until Timer3.Enabled = False
    pySet.ConnectionStatus = TestDevicePresence(sspPayout, sspPayoutInfo)

    If Not pySet.ConnectionStatus Then
        sysValue.Payout.StoredValue = 0
        sysValue.Payout.CashBoxValue = 0
    End If

    'If Not hpset.ConnectionStatus Then
    '    sysValue.Hopper.StoredValue = 0
    '    sysValue.Hopper.CashBoxValue = 0
    'End If

    RefreshDeviceData
    'smartPayoutInit = True
End Function

Private Function StopSmartPayout() As Boolean
    haltReq = True
    Call CloseSSPComPort

    End
End Function
Public Function RunSmartPayout() As Boolean
blExit = True
    Timer3.Enabled = False
    
    'If hpset.ConnectionStatus Then
    '    Call SetHopperEnable(sspHopper, hpset, sspHopperInfo)
    'End If
    If pySet.ConnectionStatus Then
        smartPayoutInit = SetPayoutEnable(sspPayout, pySet, sspPayoutInfo)
        If smartPayoutInit = False Then
            Call WriteLog("smart payout is can't run, payout can't be enabled")
            SendMsg ("0014RS010101" & trxid)
            Exit Function
        End If
    Else
        Call WriteLog("smart payout is can't run, connection status is fail")
        SendMsg ("0014RS010101" & trxid)
        smartPayoutInit = False
        Exit Function
    End If
    
    blHalt = False
    Timer1.Interval = 100
    Timer1.Enabled = True
    blTimerEnd = False

    'sysValue.Hopper.RequestedDispenseValue = 0
    sysValue.Payout.RequestedDispenseValue = 0
    'sysValue.Hopper.RequestedFloatValue = 0
    sysValue.Payout.RequestedFloatValue = 0
    'sysValue.Hopper.ValueDispensed = 0
    sysValue.Payout.ValueDispensed = 0
    'sysValue.Hopper.StoredValue = 0
    'sysValue.Payout.StoredValue = 0
    'sysValue.Hopper.CashBoxValue = 0
    'sysValue.Payout.CashBoxValue = 0

    'opPay(0).Value = True
    'txtPayout = "0.00"
    Call WriteLog("smart payout run successfully")
    SendMsg ("0014RS010100" & trxid)
    UpdateAmountDisplay sysValue
    smartPayoutHalt = False
    smartPayoutInit = True
End Function

Private Sub CashDispenser_Click()
    DispenseNote (Text1.text)
End Sub

Private Sub Form_Load()
    
    Call WriteLog("=======================================")
    Call WriteLog("application started...")
    Call WriteLog("Initialize database & socket client connection...")
    'ConnServer
    StartConnect
        
    Call WriteLog("initialize socket client monitoring...")
    Timer5.Interval = 50
    Timer5.Enabled = True
    
    Call WriteLog("initialize printer...")
    InitializePrinter
    
    Call WriteLog("initialize printer status monitoring...")
    Timer6.Interval = 60000
    Timer6.Enabled = True
    
    Call WriteLog("initialize card dispenser status monitoring...")
    Timer7.Interval = 60000
    Timer7.Enabled = True
    
    Call WriteLog("initialize smart payout...")
    'InitializeSmartPayout
    
    'Call WriteLog("run smart payout...")
    'RunSmartPayout
    
    'Call WriteLog("reset and initialize ACS ACR128U ICC...")
    'ResetCard
    'InitializeReader
        
    'ShockwaveFlash1.Movie = App.Path & "\SSK.swf" 'Full Movie Path here
    'ShockwaveFlash1.ScaleMode = 0 'ShowAll
    'Me.WindowState = vbMaximized

End Sub

'Private Sub Form_Resize()

'ShockwaveFlash1.width = Me.ScaleWidth
'ShockwaveFlash1.Height = Me.ScaleHeight

'End Sub

Private Sub Form_Unload(Cancel As Integer)
    Call WriteLog("close existing socket client connection...")
    If Winsock1.state <> sckClosed Then Winsock1.Close
    
    Call WriteLog("stop printer...")
    'StopPrinter
    
    Call WriteLog("stop smart payout...")
    StopSmartPayout
    
    CloseServer
    Call WriteLog("close application...")
    Call WriteLog("=======================================")
End Sub

Private Sub ConnServer()
    Set conn = New ADODB.Connection
    conn.ConnectionString = "DRIVER={MySQL ODBC 5.2w Driver};" _
                        & "SERVER=localhost;" _
                        & " PORT=3306;" _
                        & " DATABASE=sskfrontend;" _
                        & "UID=root;PWD=y35w3kn0w2011!@#; OPTION=3"
    '                    & "UID=root;PWD=password; OPTION=3"
    conn.Open
End Sub

Private Sub CloseServer()
    conn.Close
    Set rs = Nothing
    Set cmd = Nothing
    Set conn = Nothing
End Sub

Public Sub StartConnect()
    On Error Resume Next
    'Dim strPort As String
    
    'Set cmd = New ADODB.Command
    'cmd.ActiveConnection = conn
    'cmd.CommandType = adCmdText
    'cmd.CommandText = "SELECT value from parameter WHERE parameter_name = 'device.host.port'"
    'Set rs = cmd.Execute
    'Do While Not rs.EOF
    '    strPort = rs("value")
    '    rs.MoveNext
    'Loop
    
    'Call WriteLog("first time connect to server..." & strPort)
    If Winsock1.state <> sckClosed Then Winsock1.Close ' close existing connection
    Call Winsock1.Connect("127.0.0.1", 3000)
    With Winsock1
        Do While .state <> sckConnected
            DoEvents
            If .state = sckError Then Exit Sub
        Loop
        If Winsock1.state = sckConnected Then
            Call WriteLog("connected to server...")
        End If
    End With
End Sub

Private Sub Printer_Click()
    Dim a As String
    'a = "^BE CELCOM AXIATA BERHAD ^C^SL Kiosk ID : KIOSK001 ^C^SL Location : Selangor Petaling Jaya                   ^C^SL =========================================^C^SL Date     : 29/05/2011 ^C^SL Time     : 13:46:47 ^C^SL Trace No.: 000273 ^C^SL ^C^SE Purchase ICS Card ^C^SL ^C^SR Product Amount      : 1.0 ^C^SR Admin Charge        : 0.0 ^C^SR Discount Fee        : 0.0 ^C^SR -------------------------------------------------- ^C^SR Transaction Amount  : 1.0 ^C^SR     Payment Amount  : 1 ^C^SR      Change Amount  : 0.0 ^C^SL ^C^SL ^C^SE THANK YOU ^C^SE Hotline Service +62 21 987654321 ^C^SL ^C^SL Here - row start for Advertisement space ^C^SL Go Green by Visit http://www.kibarkabar.com ^C^SL Social & News Portal ^C^SL ^C^SL ^C^X"
    a = "^BE    PT. INDO CIPTA GUNA ^C^SL    TANGGAL       : 19/01/2013 ^C^SL    JAM           : 20:25 ^C^SL    KIOSK ID      : KIOSK001 ^C^SL    LOKASI        : Lippo Cikarang ^C^SL ^C^SE    PEMBELIAN VOUCHER TELKOMSEL ^C^SL    NO HANDPHONE  : 111111111 ^C^SL    NO SERI       : 66717407041000 ^C^SL    TANGGAL BELI  : 19/01/2013 20:24 ^C^SL    JUMLAH        : Rp. 5,000 ^C^SL    TOTAL HARGA   : Rp. 6,000 ^C^SL    CASH          : Rp. 10000.0 ^C^SL    UANG KEMBALI  : Rp. 4000.0 ^C^SL    TOTAL DONASI  : Rp. 10000.0 ^C^SE    PULSA ANDA AKAN BERTAMBAH SECARA OTOMATIS ^C^SE    TERIMA KASIH ^C^SL ^C^SL ^C^X"
    PrintReceipt (a)
End Sub

Private Sub SSP1_Click()
newPayoutEmpty = True
                'sysValue.Hopper.CashBoxValue = 0
                'sysValue.Hopper.RequestedDispenseValue = 0
                sysValue.Payout.RequestedDispenseValue = 0
                'sysValue.Hopper.RequestedFloatValue = 0
                sysValue.Payout.RequestedFloatValue = 0
                'sysValue.Hopper.ValueDispensed = 0
                sysValue.Payout.ValueDispensed = 0
                sysValue.Payout.CashBoxValue = 0
                Call SaveSetting("PayInPayOutSystem", "STARTUP", "NOTECASHBOXVALUE", sysValue.Payout.CashBoxValue)
                   
End Sub

Private Sub SSP2_Click()
    'Call FloatAmount(Text1.text, Text2.text)
If smartPayoutInit = True Then
                    'Call CloseSSPComPort
                    haltReq = True
                    'Sleep (10000)
                    If smartPayoutHalt = True Then
                        'respCode = "00"
                    Else
                        'respCode = "01"
                    End If
                End If
End Sub

Private Sub SSP3_Click()
    pySet.pay.FloatMode(CLng(Text2.text)) = FLOAT_PAYOUT_TO_CASHBOX
    'RefreshDeviceData
    'pySet.pay.FloatMode(CLng(Text2.text)) = GetSetting("PayInPayOutSystem", "STARTUP", "NOTEROUTE" & Text2.text, FLOAT_PAYOUT_TO_CASHBOX)
    Call SetNoteRoute(pySet, CLng(Text2.text), sspPayout, sspPayoutInfo)
End Sub

Private Sub Timer5_Timer()
    If Winsock1.state <> sckConnected Then
        'Call WriteLog("disconnected from server...")
        Winsock1.Close
        'Call WriteLog("connecting to server...")
        Call Winsock1.Connect("127.0.0.1", 5555)
        If Winsock1.state = sckConnected Then
            Call WriteLog("connected...")
        End If
    End If
End Sub

Public Function SendMsg(ByVal strData As String) As Boolean
    Call WriteLog("send message to server : [" & strData & "]")
    If Winsock1.state = sckConnected Then
        Call Winsock1.SendData(strData)
        DoEvents
    Else
        send = False
        Exit Function
    End If
   
    send = True
End Function

Private Sub Timer6_Timer()
Dim lMinCounter As Integer
    'lMinCounter = lMinCounter + 1 '//increment the counter  each time the code is xcuted
    'If (lMinCounter >= 10) Then   '//xx this is the interval that you specifify timer xcute
    '    lMinCounter = 0           '//Reset the timer counter
        CheckPrinterStatus
    'End If
End Sub

Private Sub Timer7_Timer()
Dim statusCode As String
Dim lMinCounter As Integer
    'lMinCounter = lMinCounter + 1
    
    'If (lMinCounter >= 10) Then
    '    lMinCounter = 0
        statusCode = GetCardDispenserStatus
        SendMsg ("0008NQ0301" & LeftJustify(statusCode, 2))
    'End If
End Sub

Private Sub Winsock1_Error(ByVal Number As Integer, Description As String, ByVal Scode As Long, ByVal Source As String, ByVal HelpFile As String, ByVal HelpContext As Long, CancelDisplay As Boolean)
    Call WriteLog("Socket error: " & Description)
End Sub

Private Sub SmartPayout_Click()
    InitializeSmartPayout
End Sub

Private Sub Winsock1_DataArrival(ByVal bytesTotal As Long)
'On Error Resume Next
Dim strData As String
Dim strLength As String
Dim dataLength As String
Dim data As String
Dim msgType As String
Dim deviceCode As String
Dim deviceCommand As String
Dim respCode As String
Dim aa11 As String
Dim bb22 As String
Dim cc33 As String
Dim balance As String
Dim minAmount As String
Dim idx As String
Dim idxRoute As String
Dim acosRes As Long
Dim TimeOut As Date
Dim sql As String
Dim dtStart As Long
Dim dtEnd As Long

    ' get the data from the socket
    Winsock1.GetData dataLength, vbString, 4
    Call WriteLog("receive message length from server : [" & dataLength & "]")
    
    Winsock1.GetData data, vbString, CLng(dataLength)
    Call WriteLog("receive data from server : [" & data & "]")
        
    'dataLength = Mid$(strData, 1, 4)
    'data = Mid$(strData, 5)
    msgType = Mid$(data, 1, 2)
    deviceCode = Mid$(data, 3, 2)
    deviceCommand = Mid$(data, 5, 2)
    trxid = Mid$(data, 9, 6)
    
    If msgType = "RQ" Then
        'SmartPayout
        If deviceCode = "01" Then
            'If blBusy = True Then
            '    Call WriteLog("ssp busy : [" & blBusy & "]")
            '    TimeOut = DateAdd(DateTime.Minute, 1, DateTime.Now)
            '    Do While blBusy <> False
            '        If DateTime.Now > TimeOut Then
            '            Break
            '            SendMsg (dataLength & "RS" & deviceCode & deviceCommand & "50" & trxid)
            '        End If
            '        Sleep (100)
            '        DoEvents
            '    Loop
            'End If
            
            'If blBusy = False Then
                'Init & Start
                If deviceCommand = "01" Then
                    'InitializeSmartPayout
                    If smartPayoutInit = True Then
                        Call WriteLog("smart payout is already run...")
                        SendMsg (dataLength & "RS" & deviceCode & deviceCommand & "00" & trxid)
                    Else
                        RunSmartPayout
                    End If
                
                'Dispense
                ElseIf deviceCommand = "03" Then
                    amount = Mid$(data, 15, 12)
                    DispenseNote (amount)
                    
                'Stop
                ElseIf deviceCommand = "04" Then
                    If smartPayoutHalt = True Then
                        Call WriteLog("smart payout is already halted...")
                        SendMsg (dataLength & "RS" & deviceCode & deviceCommand & "00" & trxid)
                    Else
                        If smartPayoutInit = True Then
                            haltReq = True
                        End If
                    End If
                'Reset
                ElseIf deviceCommand = "05" Then
                    ResetSmartPayout
                
                'Empty
                ElseIf deviceCommand = "06" Then
                    RunSmartPayout
                    newPayoutEmpty = True
                    sysValue.Hopper.CashBoxValue = 0
                    'sysValue.Hopper.RequestedDispenseValue = 0
                    sysValue.Payout.RequestedDispenseValue = 0
                    'sysValue.Hopper.RequestedFloatValue = 0
                    sysValue.Payout.RequestedFloatValue = 0
                    'sysValue.Hopper.ValueDispensed = 0
                    sysValue.Payout.ValueDispensed = 0
                    sysValue.Payout.CashBoxValue = 0
                    
                    'Call SaveSetting("PayInPayOutSystem", "STARTUP", "NOTECASHBOXVALUE", sysValue.Payout.CashBoxValue)
                    Set rs = New Recordset
                    sql = "update parameter set value = '" & sysValue.Payout.CashBoxValue & "' where parameter_name = 'ssp.notecashboxvalue'"
                    rs.Open sql, conn
                    Set rs = Nothing
                    
                    'haltReq = True
                    
                'Float
                ElseIf deviceCommand = "07" Then
                    amount = Mid$(data, 15, 12)
                    minAmount = Mid$(data, 27, 12)
                    Call FloatAmount(amount, minAmount)
                
                'Clear
                ElseIf deviceCommand = "08" Then
                    sysValue.Payout.CashBoxValue = 0
                    
                    'Call SaveSetting("PayInPayOutSystem", "STARTUP", "NOTECASHBOXVALUE", sysValue.Payout.CashBoxValue)
                    Set rs = New Recordset
                    sql = "update parameter set value = '" & sysValue.Payout.CashBoxValue & "' where parameter_name = 'ssp.notecashboxvalue'"
                    rs.Open sql, conn
                    Set rs = Nothing
                    
                'Set note route
                ElseIf deviceCommand = "09" Then
                    idx = Mid$(data, 9, 1)
                    idxRoute = Mid$(data, 10, 1)
                    pySet.pay.FloatMode(CLng(idx)) = CByte(idxRoute)
                    'Call SaveSetting("PayInPayOutSystem", "STARTUP", "NOTEROUTE" & idx, idxRoute)
                    Call SetNoteRoute(pySet, CLng(idx), sspPayout, sspPayoutInfo)
                    
                End If
            'End If
        
        'Printer
        ElseIf deviceCode = "02" Then
            If deviceCommand = "01" Then
                Dim text As String
                text = Mid$(data, 15)
                Call WriteLog("text to print : [" & text & "]")
                If PrintReceipt(text) = True Then
                    respCode = "00"
                Else
                    respCode = "01"
                End If
                SendMsg (dataLength & "RS" & deviceCode & deviceCommand & respCode & trxid & text)
            End If
            
        'Card Dispenser
        ElseIf deviceCode = "03" Then
            If deviceCommand = "01" Then
                If EnabledCardDispenserPort9600 = True Then
                    respCode = "00"
                Else
                    respCode = "01"
                End If
                
                respCode = TakeOutCard
                'SendMsg (dataLength & "RS" & deviceCode & deviceCommand & LeftJustify(respCode, 2))
            End If
        End If
    ElseIf msgType = "RS" Then
        ' display it in the textbox
         MsgBox strData
    End If
End Sub

Private Function RefreshDeviceData() As Boolean
Dim szDataSet As String
Dim payinbit As String
Dim noteroute As String
Dim notecashboxvalue As String
Dim sql As String
Dim i As Integer

    If pySet.ConnectionStatus Then

        If InitialisePayout(sspPayout, pySet, sspPayoutInfo) Then
        ' get saved values for channel inhiibts and routes for the same dataset from the registry
        
        ' szDataSet = GetSetting("PayInPayOutSystem", "STARTUP", "LASTNOTESET", "XXX")
        
        Set cmd = New ADODB.Command
        cmd.ActiveConnection = conn
        cmd.CommandType = adCmdText
        cmd.CommandText = "SELECT value from parameter WHERE parameter_name = 'ssp.lastnoteset'"
        Set rs = cmd.Execute
        Do While Not rs.EOF
            szDataSet = rs("value")
            rs.MoveNext
        Loop
        Set rs = Nothing
        Set cmd = Nothing
        
        If pySet.CountryCode = szDataSet Then
            Set cmd = New ADODB.Command
            cmd.ActiveConnection = conn
            cmd.CommandType = adCmdText
            cmd.CommandText = "SELECT value from parameter WHERE parameter_name = 'ssp.payinbit'"
            Set rs = cmd.Execute
            Do While Not rs.EOF
                payinbit = rs("value")
                rs.MoveNext
            Loop
            Set rs = Nothing
            Set cmd = Nothing
            
            pySet.NoteInhibitRegister = payinbit
            ' pySet.NoteInhibitRegister = GetSetting("PayInPayOutSystem", "STARTUP", "PAYINIBIT", &HFF)
            
            For i = 0 To pySet.pay.NumberOfCoinValue - 1
                Set cmd = New ADODB.Command
                cmd.ActiveConnection = conn
                cmd.CommandType = adCmdText
                cmd.CommandText = "SELECT current_routing from ssp_inventory WHERE idx = " & i
                Set rs = cmd.Execute
                Do While Not rs.EOF
                    noteroute = rs("current_routing")
                    rs.MoveNext
                Loop
                Set rs = Nothing
                Set cmd = Nothing
                
                pySet.pay.FloatMode(i) = noteroute
                'pySet.pay.FloatMode(i) = GetSetting("PayInPayOutSystem", "STARTUP", "NOTEROUTE" & i, FLOAT_PAYOUT_TO_CASHBOX)
            Next i
            
            Set cmd = New ADODB.Command
            cmd.ActiveConnection = conn
            cmd.CommandType = adCmdText
            cmd.CommandText = "SELECT value from parameter WHERE parameter_name = 'ssp.notecashboxvalue'"
            Set rs = cmd.Execute
            Do While Not rs.EOF
                notecashboxvalue = rs("value")
                rs.MoveNext
            Loop
            Set rs = Nothing
            Set cmd = Nothing
            
            sysValue.Payout.CashBoxValue = notecashboxvalue
            'sysValue.Payout.CashBoxValue = GetSetting("PayInPayOutSystem", "STARTUP", "NOTECASHBOXVALUE", 0)
        End If
        'Call SaveSetting("PayInPayOutSystem", "STARTUP", "LASTNOTESET", pySet.CountryCode)
        Set rs = New Recordset
        sql = "update parameter set value = '" & pySet.CountryCode & "' where parameter_name = 'ssp.lastnoteset'"
        rs.Open sql, conn
        Set rs = Nothing
        
        Call UpdatePayoutDisplay(pySet)
        Else
            pySet.ConnectionStatus = False
        End If
    End If

    UpdateAmountDisplay sysValue

End Function

Private Sub Timer1_Timer()
Dim i As Integer

    Timer1.Enabled = False
    blTimerEnd = False

    If Not HandleRequests Then
        'EnableInterface
        'bttnRunSystem.Enabled = True
        blTimerEnd = True
        Exit Sub
    End If
    
    blTimerEnd = True
    If Not blHalt Then Timer1.Enabled = True
    
End Sub

Private Sub Timer2_Timer()
Dim i As Integer

    Timer2.Enabled = False

End Sub

Private Sub Timer3_Timer()
    
    Timer3.Enabled = False

End Sub

Private Function CheckForConnections() As Boolean

    'hpset.ConnectionStatus = TestDevicePresence(sspHopper, sspHopperInfo)
    pySet.ConnectionStatus = TestDevicePresence(sspPayout, sspPayoutInfo)

    'If hpset.ConnectionStatus And frameHopper.Visible = False Then
    '    If Not RefreshDeviceData Then Exit Function
    'End If
    
    'If Not hpset.ConnectionStatus And frameHopper.Visible Then
        'UpdateAmountDisplay sysValue
    'End If
    
    If pySet.ConnectionStatus Then
        If Not RefreshDeviceData Then Exit Function
    End If
    
    If Not pySet.ConnectionStatus Then 'And framePayout.Visible Then
        UpdateAmountDisplay sysValue
    End If

    CheckForConnections = True

End Function

Private Sub Timer4_Timer()

    Timer4.Enabled = False

End Sub
Private Sub WaitDelay(delayInterval As Long)

    Timer4.Interval = delayInterval
    Timer4.Enabled = True
    Do
        DoEvents
    Loop Until Timer4.Enabled = False
End Sub

Private Function HandleRequests() As Boolean
    Dim i As Integer

    If newPayout Then
        newPayout = False
        If sysValue.Payout.RequestedDispenseValue > 0 Then
            sspPayout.CommandDataLength = 5
            sspPayout.EncryptionStatus = 1
            sspPayout.CommandData(0) = cmd_PAYOUT_CMD
            sspPayoutInfo.CommandName = "PAYOUT_CMD"
            For i = 0 To 3
                sspPayout.CommandData(1 + i) = CByte(RShift(sysValue.Payout.RequestedDispenseValue, 8 * i) And 255)
            Next i
            If Not TransmitSSPCommand(sspPayout, sspPayoutInfo) Then
                Call WriteLog("note " & amount & " can't be dispensed because fail to transmit ssp command")
                SendMsg ("0026RS010301" & trxid & amount)
                Exit Function
            End If
        End If
    End If

    If newPayoutReset Then
        Call SendReset(sspPayout, sspPayoutInfo)
        newPayoutReset = False
        If Not CloseCommunicationPorts Then
            Exit Function
        End If
        WaitDelay 3000
        If Not OpenCommunicationPorts Then
            Exit Function
        End If
                
        pySet.ConnectionStatus = TestDevicePresence(sspPayout, sspPayoutInfo)
        If pySet.ConnectionStatus Then
            Call SetPayoutEnable(sspPayout, pySet, sspPayoutInfo)
        End If
    End If
    
    If newPayoutEmpty Then
        newPayoutEmpty = False
        'Call EmptyAllToCashBox(sspPayout, sspPayoutInfo)
        sspPayout.CommandDataLength = 1
        sspPayout.EncryptionStatus = 0
        sspPayout.CommandData(0) = SYNC_CMD
        sspPayoutInfo.CommandName = "SYNC_CMD"
        If Not TransmitSSPCommand(sspPayout, sspPayoutInfo) Then
            Call WriteLog("smart payout can't be emptied because fail to transmit ssp command SYNC")
            SendMsg ("0014RS010601" & trxid)
            Exit Function
        End If
        
        sspPayout.CommandDataLength = 1
        sspPayout.EncryptionStatus = 0
        sspPayout.CommandData(0) = ENABLE_CMD
        sspPayoutInfo.CommandName = "ENABLE_CMD"
        If Not TransmitSSPCommand(sspPayout, sspPayoutInfo) Then
            Call WriteLog("smart payout can't be emptied because fail to transmit ssp command ENABLE")
            SendMsg ("0014RS010601" & trxid)
            Exit Function
        End If

        sspPayout.CommandDataLength = 1
        sspPayout.EncryptionStatus = 1
        sspPayout.CommandData(0) = cmd_EMPTY_ALL
        sspPayoutInfo.CommandName = "EMPTY_ALL"
        If Not TransmitSSPCommand(sspPayout, sspPayoutInfo) Then
            Call WriteLog("smart payout can't be emptied because fail to transmit ssp command EMPTY")
            SendMsg ("0014RS010601" & trxid)
            Exit Function
        End If
    End If
    
    If haltReq Then
        haltReq = False
        blHalt = True
        
        If pySet.ConnectionStatus Then
            sspPayout.CommandDataLength = 1
            sspPayout.EncryptionStatus = 1
            sspPayout.CommandData(0) = DISABLE_CMD
            sspPayoutInfo.CommandName = "DISABLE"
            If Not TransmitSSPCommand(sspPayout, sspPayoutInfo) Then
                smartPayoutHalt = False
                Call WriteLog("smart payout can't be halted because fail to transmit ssp command")
                SendMsg ("0014RS010401" & trxid)
                Exit Function
            End If
            smartPayoutInit = False
            smartPayoutHalt = True
            Call WriteLog("smart payout is halted successfully..")
            SendMsg ("0014RS010400" & trxid)
        End If
    End If
    
    If newFloat Then
        newFloat = False
        If sysValue.Payout.RequestedFloatValue > 0 Then
            If Not SetPayoutFloatAmount(pySet, sysValue, sspPayout, sspPayoutInfo) Then
                Exit Function
            End If
        End If
    End If
    
    If pySet.ConnectionStatus Then
        sspPayout.CommandDataLength = 1
        sspPayout.EncryptionStatus = 1
        sspPayout.CommandData(0) = POLL_CMD
        sspPayoutInfo.CommandName = "POLL_CMD"
        If Not TransmitSSPCommand(sspPayout, sspPayoutInfo) Then Exit Function
            
        For i = 0 To sspPayout.ResponseDataLength - 1
            pySet.PollResponse(i) = sspPayout.ResponseData(i)
        Next i
        pySet.PollLength = sspPayout.ResponseDataLength
    End If
    
    If pySet.ConnectionStatus Then
        If Not DecodePayoutPoll(pySet) Then Exit Function
    End If
    UpdateAmountDisplay sysValue
    
    HandleRequests = True

End Function

Private Function DecodePayoutPoll(py As PAYOUT_SET) As Boolean

Dim i As Integer, j As Integer
Dim crValue As Long
Dim sql As String

    endPoll = False
    'If py.PollLength = 1 Then lblPayoutStatus = ""
    If py.PollLength = 1 Then blBusy = False
    For i = 1 To py.PollLength - 1
        Call WriteLog("py.PollResponse:" & i & "=" & py.PollResponse(i))
        Select Case py.PollResponse(i)
        
        Case DISABLED
            blBusy = True
            
        Case NOTE_READ
            If py.PollResponse(i + 1) = 0 Then
                'Call WriteLog("Note reading...")
            Else
                'Call WriteLog("Note stacking...")
                'lblPayoutStatus = "NOTE STACKING " & Format(py.pay.CoinsInHopper((py.PollResponse(i + 1) - 1)).CoinValue / py.TrueValueMuliplier, "###.00")
            End If
            i = i + 1
            
        Case hp_rsp_DISPENSING
            'Call WriteLog("Smart payout dispensing...")
            sysValue.Payout.ValueDispensed = 0
            For j = 0 To 3
                sysValue.Payout.ValueDispensed = sysValue.Payout.ValueDispensed + (CLng(py.PollResponse(j + i + 1)) * (256 ^ j))
            Next j
            i = i + 4 ' index along events to skip
            
        Case hp_rsp_DISPENSED
            'Call WriteLog("Smart payout dispensed...")
            sysValue.Payout.ValueDispensed = 0
            For j = 0 To 3
                sysValue.Payout.ValueDispensed = sysValue.Payout.ValueDispensed + (CLng(py.PollResponse(j + i + 1)) * (256 ^ j))
            Next j
            i = i + 4 ' index along events to skip
            If Not SetPayoutEnable(sspPayout, py, sspPayoutInfo) Then Exit Function
            
            Set rs = New Recordset
            sql = "update ssp_inventory set current_payout_note = (current_payout_note - 1) where device_code = '01' and denom = '" & lastNotevalue / 100 & "'"
            rs.Open sql, conn
            Set rs = Nothing
            
            Set rs = New Recordset
            sql = "insert into ssp_history(device_code,denom,command,payout,cashbox,stan) values('01','" & lastNotevalue / 100 & "','dispense','-1','0','" & trxid & "')"
            rs.Open sql, conn
            Set rs = Nothing
            
            SendMsg ("0026RS010300" & trxid & amount)
            
            endPoll = True
            
        Case NOTE_STACKED
            Call WriteLog("Note stacked in cashbox last note value : " & lastNotevalue)
            Call WriteLog("Note stacked in cashbox : " & Format(lastNotevalue / 100, "###.00"))
            sysValue.Payout.CashBoxValue = sysValue.Payout.CashBoxValue + lastNotevalue
            
            ' Call SaveSetting("PayInPayOutSystem", "STARTUP", "NOTECASHBOXVALUE", sysValue.Payout.CashBoxValue)
            Set rs = New Recordset
            'sql = "update parameter set value = '" & sysValue.Payout.CashBoxValue & "' where parameter_name = 'ssp.notecashboxvalue'"
            sql = "update ssp_inventory set current_cashbox_note = (current_cashbox_note + 1) where device_code = '01' and denom = '" & lastNotevalue / 100 & "'"
            rs.Open sql, conn
            Set rs = Nothing
            
            Set rs = New Recordset
            sql = "insert into ssp_history(device_code,denom,command,payout,cashbox,stan) values('01','" & lastNotevalue / 100 & "','accept','0','1','" & trxid & "')"
            rs.Open sql, conn
            Set rs = Nothing
            
            SendMsg ("0027RQ010200" & trxid & "1" & RightJustify(lastNotevalue / 100, 12))
            
            endPoll = True
            
        Case STACKING
            'Call WriteLog("smart payout stacking...")
            
        Case spp_ev_NOTE_STORED
            Call WriteLog("Note in payout last note value : " & lastNotevalue)
            Call WriteLog("Note in payout : " & Format(lastNotevalue / 100, "###.00"))
            
            Set rs = New Recordset
            sql = "update ssp_inventory set current_payout_note = (current_payout_note + 1) where device_code = '01' and denom = '" & lastNotevalue / 100 & "'"
            rs.Open sql, conn
            Set rs = Nothing
            
            Set rs = New Recordset
            sql = "insert into ssp_history(device_code,denom,command,payout,cashbox,stan) values('01','" & lastNotevalue / 100 & "','accept','1','0','" & trxid & "')"
            rs.Open sql, conn
            Set rs = Nothing
            
            SendMsg ("0027RQ010200" & trxid & "0" & RightJustify(lastNotevalue / 100, 12))
            
            endPoll = True
            
        Case CREDIT
            lastNotevalue = py.pay.CoinsInHopper((py.PollResponse(i + 1) - 1)).CoinValue
            i = i + 1
            
        Case SAFE_JAM
            'SendMsg ("0027RQ010201" & trxid & RightJustify(lastNotevalue, 12) & "0")
            Call WriteLog("smart payout safe jam...")
            SendMsg ("0008NQ0101CA")
        
        Case UNSAFE_JAM
            'SendMsg ("0027RQ010202" & trxid & RightJustify(lastNotevalue, 12) & "0")
            Call WriteLog("smart payout unsafe jam...")
            SendMsg ("0008NQ0101CB")
            
        Case hp_rsp_FRAUD_ATTEMPT
            Call WriteLog("smart payout fraud detected...")
            SendMsg ("0008NQ0101CC")
            'SendMsg ("0027RQ010203" & trxid & RightJustify(lastNotevalue, 12) & "0")
            i = i + 4 ' index along events to skip
            endPoll = True
            
        Case hp_TIME_OUT
            Call WriteLog("smart payout time out...")
            SendMsg ("0008NQ0101CD")
            'SendMsg ("0027RQ020204" & trxid & RightJustify(lastNotevalue, 12) & "0")
            i = i + 4 ' index along events to skip
            endPoll = True
            
        Case STACKER_FULL
            Call WriteLog("stacker full...")
            SendMsg ("0008NQ0101CE")
        
        Case hp_FLOATING
            'Call WriteLog("smart payout floating...")
            On Local Error Resume Next
            sysValue.Payout.ValueDispensed = 0
            For j = 0 To 3
                sysValue.Payout.ValueDispensed = sysValue.Payout.ValueDispensed + (CLng(py.PollResponse(j + i + 1)) * (256 ^ j))
            Next j
            i = i + 4 ' index along events to skip
            
        Case hp_FLOATED
            'Call WriteLog("smart payout floated...")
            sysValue.Payout.ValueDispensed = 0
            For j = 0 To 3
                sysValue.Payout.ValueDispensed = sysValue.Payout.ValueDispensed + (CLng(py.PollResponse(j + i + 1)) * (256 ^ j))
            Next j
            
            sysValue.Payout.CashBoxValue = sysValue.Payout.CashBoxValue + sysValue.Payout.ValueDispensed
            
            If Not SetPayoutEnable(sspPayout, py, sspPayoutInfo) Then Exit Function
            i = i + 4 ' index along events to skip
            endPoll = True
            
        Case hp_HOPPER_HALTED
            'Call WriteLog("smart payout halted...")
            i = i + 4 ' index along events to skip
            endPoll = True
            
        Case hp_EMPTYING
            'Call WriteLog("smart payout emptying...")
            
        Case hp_EMPTYED
            'Call WriteLog("smart payout emptied...")
            If Not SetPayoutEnable(sspPayout, py, sspPayoutInfo) Then Exit Function
            endPoll = True
            Call WriteLog("smart payout emptied successfully...")
            
            For i = 0 To pySet.pay.NumberOfCoinValues - 1
                Set rs = New Recordset
                sql = "update ssp_inventory set current_payout_note = '0' where device_code = '01' and idx = " & i
                rs.Open sql, conn
                Set rs = Nothing
                
                Set rs = New Recordset
                sql = "insert into ssp_history(device_code,denom,command,payout,cashbox,stan) values('01','" & lastNotevalue / 100 & "','accept','1','0','" & trxid & "')"
                rs.Open sql, conn
                Set rs = Nothing
            Next i
            
            SendMsg ("0014RS010600" & trxid)
            
        Case hp_INPCOMPLETE_PAYOUT
            Call WriteLog("smart payout incomplete payout...")
            endPoll = True
            i = i + 8 ' index along events to skip
            
        Case hp_INPCOMPLETE_FLOAT
            Call WriteLog("smart payout incomplete float...")
            i = i + 8 ' index along events to skip
            endPoll = True

        End Select
    Next i

    If endPoll Then
        Timer3.Interval = 500
        Timer3.Enabled = True
        Do
            DoEvents
        Loop Until Timer3.Enabled = False
        If Not GetPayoutNoteLevels(py, sspPayout, sspPayoutInfo) Then Exit Function
        If Not GetPayoutMinPayout(py, sspPayout, sspPayoutInfo) Then Exit Function
        Call UpdatePayoutDisplay(py)
        UpdateAmountDisplay sysValue
    End If
    
    DecodePayoutPoll = True
    
End Function
Private Sub Print_Click()

    MSComm1.Output = Chr$(&H1B) & "P"   ' PAGE MODE
    'chr(10) / Chr(&HA) =ganti baris

   ' -------- PAGE AREA PRINTING ---------------------
    MSComm1.Output = Chr$(&H1B) & Chr$(&HC)
    MSComm1.Output = Chr$(28) + Chr$(38) 'FS
    ''''''''''''font B
   'MSComm1.Output = Chr$(&H1B) + "!" + Chr$(&H1) & Chr$(&HD) '& Chr$(&HD)
    MSComm1.Output = Chr$(&H1B) + "!" + Chr$(32) 'set font to big
   'MSComm1.Output = Chr$(58) + Chr$(87) + Chr$(12) '''''''FS W n=1
    MSComm1.Output = Chr$(&H1B) + "a" + Chr$(1) 'center
    MSComm1.Output = "ICS" + Chr$(10) '& Chr$(&HD)
    MSComm1.Output = Chr$(&H1B) + "!" + Chr$(4) ' set font to small
    MSComm1.Output = "Lippo Cikarang" + Chr$(10) '& Chr$(&HD)
    MSComm1.Output = "Jl. Mawar I No.10 Lembah Hijau" + Chr$(10) '& Chr$(&HD)
    MSComm1.Output = "Tel/Fax. +62 21 8974636" + Chr$(10) '& Chr$(&HD)
   
    
MSComm1.Output = "=================================================" + Chr$(10) '& Chr$(&HD)
MSComm1.Output = Chr$(&H1B) + "a" + Chr$(0) ' left
MSComm1.Output = "  Trx. Date: " & Now() & Chr$(10) '& Chr$(&HD)
MSComm1.Output = "  Reff No  : Diisi dengan nomor STAN" & Chr$(10) '& Chr$(&HD)
MSComm1.Output = "  KiosK No : Diisi dengan nomor ID KiosK" & Chr$(10) & Chr$(10) '& Chr$(&HD)
MSComm1.Output = Chr$(&H1B) + "a" + Chr$(2) 'right

   MSComm1.Output = "Reload 20K for 0812345678900    22,000   " + Chr$(10) '& Chr$(&HD)
   MSComm1.Output = "Discount                        -1,000   " + Chr$(10) '& Chr$(&HD)
     
   MSComm1.Output = "--------------------------------------   " + Chr$(10) '& Chr$(&HD)
   MSComm1.Output = "Total          21,000   " + Chr$(10) ' & Chr$(&HD)
   MSComm1.Output = "VAT                 0   " + Chr$(10) '& Chr$(&HD)
   MSComm1.Output = "Cash           50,000   " + Chr$(10) ' & Chr$(&HD)
   MSComm1.Output = "Change         29,000   " + Chr$(10) ' & Chr$(&HD)
   MSComm1.Output = Chr$(10) + Chr$(10)
   MSComm1.Output = Chr$(&H1B) + "a" + Chr$(1) 'center
   MSComm1.Output = "THANK YOU" + Chr$(10) '& Chr$(&HD)
   MSComm1.Output = "Hotline Service +62 21 8974636" + Chr$(10) '& Chr$(&HD)
   
   MSComm1.Output = Chr$(10) + Chr$(10)
   
   MSComm1.Output = "Here - row start for Advertisement space" + Chr$(10) '& Chr$(&HD)
   MSComm1.Output = "Go Green by Visit http://www.kibarkabar.com " + Chr$(10) ' & Chr$(&HD)
   MSComm1.Output = "Social & News Portal " + Chr$(10) '& Chr$(&HD)
   
   
   MSComm1.Output = Chr$(10) + Chr$(10) + Chr$(10) + Chr$(10) '& Chr$(&HD)
   
   'Next i
   'Sleep (500)
   ' ---------- CUTTING POSITION ----------------------
   ' must be adjust to your ticket size
    MSComm1.Output = Chr$(&H1A) & "b" & Chr$(1)     ' FORWARD BLACK MARK SEARCH
    MSComm1.Output = Chr$(&H1B) & "J" & Chr$(80)    ' ADJUST CUTTING POSITION
    MSComm1.Output = Chr$(&H1D) & "V" & Chr$(0)     ' FULL CUTTING
    MSComm1.Output = Chr$(&H1B) & "j" & Chr$(120)   ' ADJUST NEXT PAGE PRINT POSITION
    
    '---------- PAGE AREA CLEAR AND TO STANDARD MODE -----------
    MSComm1.Output = Chr$(&H1B) & "S"
Sleep (1000)
MSComm1.Output = Chr$(&H1B) & "@"
    
End Sub
Private Function DispenseNote(ByVal note As String) As Boolean
Dim totalReqested As Long
Dim v1 As Integer
    Call WriteLog("The Smart payout will dispense 0 : " & note)
    'sysValue.Hopper.RequestedDispenseValue = 0
    sysValue.Payout.RequestedDispenseValue = 0
    'sysValue.Hopper.RequestedFloatValue = 0
    sysValue.Payout.RequestedFloatValue = 0
    'sysValue.Hopper.ValueDispensed = 0
    sysValue.Payout.ValueDispensed = 0
    sysValue.TotalFloatedAmount = 0
    sysValue.TotalFloatTarget = 0

    UpdateAmountDisplay sysValue
    If Val(note) = 0 Then
        Call WriteLog("note " & note & "can't be dispensed because Val(note)=0")
        SendMsg ("0026RS010301" & trxid & amount)
        Exit Function
    End If
        If pySet.ConnectionStatus And sysValue.Payout.StoredValue > 0 And pySet.MinPayout > 0 Then
            sysValue.Payout.RequestedDispenseValue = Int((Val(note) * 100)) '/ pySet.MinPayout)
            Call WriteLog("The Smart payout will dispense 1 : " & sysValue.Payout.RequestedDispenseValue)
            'sysValue.Payout.RequestedDispenseValue = sysValue.Payout.RequestedDispenseValue * pySet.MinPayout
            Call WriteLog("The Smart payout will dispense 2 : " & sysValue.Payout.RequestedDispenseValue)
            pySet.pay.LevelMode = LEVEL_CHECK_ON
            pySet.pay.mode = FLOAT_PAYOUT_TO_PAYOUT
            'v1 = TestSplit(pySet.pay, sysValue.Payout.RequestedDispenseValue)
            'If v1 Then
                'Call WriteLog("The Smart payout cannot pay out the requested value of " & Format(sysValue.Payout.RequestedDispenseValue / 100, "####0.00"))
                'Exit Function
            'End If
        End If
        If (Val(note) * 100) - (sysValue.Payout.RequestedDispenseValue) > 0 Then
            Call WriteLog("note " & note & "can't be dispensed because there is not enough amount")
            SendMsg ("0026RS010301" & trxid & amount)
            Exit Function
        End If
        newPayout = True
End Function

Public Sub ResetSmartPayout()
    newPayoutReset = True
End Sub

Private Sub FloatAmount(note As String, minValue As String)
Dim totalReqested As Long
Dim v1 As Integer

    'sysValue.Hopper.RequestedDispenseValue = 0
    sysValue.Payout.RequestedDispenseValue = 0
    'sysValue.Hopper.RequestedFloatValue = 0
    sysValue.Payout.RequestedFloatValue = 0
    'sysValue.Hopper.ValueDispensed = 0
    sysValue.Payout.ValueDispensed = 0
    sysValue.TotalFloatedAmount = 0
    sysValue.TotalFloatTarget = 0

    UpdateAmountDisplay sysValue
    If Val(note) = 0 Then Exit Sub

        sysValue.TotalFloatTarget = sysValue.TotalStoredValue - (Val(note) * 100)
        totalReqested = sysValue.Hopper.StoredValue + sysValue.Payout.StoredValue - (Val(note) * 100)
    
        'sysValue.Hopper.ValueDispensed = 0
        sysValue.Payout.ValueDispensed = 0
        
        If pySet.ConnectionStatus And sysValue.Payout.StoredValue > 0 And pySet.MinPayout > 0 Then
        
            If Val(note) * 100 < pySet.MinPayout Then
                    sysValue.Payout.RequestedFloatValue = 0
            Else
                sysValue.Payout.RequestedDispenseValue = Int((totalReqested) / pySet.MinPayout)
                sysValue.Payout.RequestedDispenseValue = sysValue.Payout.RequestedDispenseValue * pySet.MinPayout
                If sysValue.Payout.RequestedDispenseValue > 0 Then
                    
                    If sysValue.Payout.RequestedDispenseValue > sysValue.Payout.StoredValue Then
                        sysValue.Payout.RequestedDispenseValue = sysValue.Payout.StoredValue - pySet.MinPayout
                    End If
                
                    sysValue.Payout.RequestedFloatValue = sysValue.Payout.StoredValue - sysValue.Payout.RequestedDispenseValue
                    pySet.MinPayout = Val(minValue) * pySet.TrueValueMuliplier
                    pySet.pay.LevelMode = LEVEL_CHECK_ON
                    pySet.pay.mode = FLOAT_PAYOUT_TO_CASHBOX
                    'v1 = TestSplit(pySet.pay, sysValue.Payout.RequestedDispenseValue)
                    'If v1 Then
                    '    Call WriteLog("The Smart hopper cannot float to this value")
                    '    sysValue.Payout.RequestedFloatValue = 0
                    '    Exit Sub
                    'End If
                    
                Else
                    sysValue.Payout.RequestedFloatValue = 0
                End If
                
            End If
        
        Else
            sysValue.Payout.RequestedFloatValue = 0
        End If
        'txtPayout = Format(sysValue.TotalFloatTarget / 100, "####0.00")
        newFloat = True

End Sub

Private Function PrintReceipt(ByVal text As String) As Boolean
Dim X As Integer
Dim str As String

    MSComm1.Output = Chr$(&H1B) & "P"   ' PAGE MODE
    'chr(10) / Chr(&HA) =ganti baris

    ' -------- PAGE AREA PRINTING ---------------------
    MSComm1.Output = Chr$(&H1B) & Chr$(&HC)
    MSComm1.Output = Chr$(28) + Chr$(38) 'FS
    
    While text <> "^X"
        If StringStartsWith(text, "^BE") Then
            MSComm1.Output = Chr$(&H1B) + "!" + Chr$(32) 'set font to big
            MSComm1.Output = Chr$(&H1B) + "a" + Chr$(1)  'center
            X = InStr(text, "^C")
            str = Mid$(text, 4, X - 4)
            text = Mid$(text, X + 2)
            MSComm1.Output = str + Chr$(10)
        ElseIf StringStartsWith(text, "^SL") Then
            MSComm1.Output = Chr$(&H1B) + "!" + Chr$(4) ' set font to small
            MSComm1.Output = Chr$(&H1B) + "a" + Chr$(0) ' left
            X = InStr(text, "^C")
            str = Mid$(text, 4, X - 4)
            text = Mid$(text, X + 2)
            MSComm1.Output = str + Chr$(10)
        ElseIf StringStartsWith(text, "^SE") Then
            MSComm1.Output = Chr$(&H1B) + "!" + Chr$(4) ' set font to small
            MSComm1.Output = Chr$(&H1B) + "a" + Chr$(1)  'center
            X = InStr(text, "^C")
            str = Mid$(text, 4, X - 4)
            text = Mid$(text, X + 2)
            MSComm1.Output = str + Chr$(10)
        ElseIf StringStartsWith(text, "^SR") Then
            MSComm1.Output = Chr$(&H1B) + "!" + Chr$(4) ' set font to small
            MSComm1.Output = Chr$(&H1B) + "a" + Chr$(2) ' right
            X = InStr(text, "^C")
            str = Mid$(text, 4, X - 4)
            text = Mid$(text, X + 2)
            MSComm1.Output = str + Chr$(10)
        End If
    Wend
    
    ''''''''''''font B
    'MSComm1.Output = Chr$(&H1B) + "!" + Chr$(&H1) & Chr$(&HD) '& Chr$(&HD)
    'MSComm1.Output = Chr$(58) + Chr$(87) + Chr$(12) '''''''FS W n=1
       
    'Next i
    'Sleep (500)
    ' ---------- CUTTING POSITION ----------------------
    ' must be adjust to your ticket size
    MSComm1.Output = Chr$(&H1A) & "b" & Chr$(1)     ' FORWARD BLACK MARK SEARCH
    MSComm1.Output = Chr$(&H1B) & "J" & Chr$(80)    ' ADJUST CUTTING POSITION
    MSComm1.Output = Chr$(&H1D) & "V" & Chr$(0)     ' FULL CUTTING
    MSComm1.Output = Chr$(&H1B) & "j" & Chr$(120)   ' ADJUST NEXT PAGE PRINT POSITION
    
    '---------- PAGE AREA CLEAR AND TO STANDARD MODE -----------
    MSComm1.Output = Chr$(&H1B) & "S"
    Sleep (1000)
    MSComm1.Output = Chr$(&H1B) & "@"
    PrintReceipt = True
    
End Function

Private Sub MSComm1_OnComm() '------ rs232 interrupt routine ---------------
   
    Dim TmpStr As String
    Dim mydata As Byte
    Dim StrLen As Long
    Dim i As Long
    Dim LocalStatus As Integer
    
    Select Case MSComm1.CommEvent
    
    Case comEvReceive
    
    ReceiveCnt = ReceiveCnt + 1
    
    TmpStr = MSComm1.Input  ' read from buffer
  
    StrLen = Len(TmpStr)    ' convert to byte
    For i = 1 To StrLen
        mydata = CByte(Asc(Mid(TmpStr, 1, 1)))
    Next i
    
    Call WriteLog("Printer status receiving:" & ReceiveCnt & ", status (Hex):" & Hex(mydata))
        
    Cls
    LocalStatus = mydata And &HF7
    If LocalStatus = 0 Then
        SendMsg ("0008NQ020100") '"Printer is o.k./"
    End If
    LocalStatus = mydata And 1
    If LocalStatus = 1 Then
        SendMsg ("0008NQ0201AA") '"There is no paper./"
    End If
    LocalStatus = mydata And 2
    If LocalStatus = 2 Then
        SendMsg ("0008NQ0201AB") '"Thermal Print Head UP./"
    End If
    LocalStatus = mydata And 4
    If LocalStatus = 4 Then
        SendMsg ("0008NQ0201AC") '"This is a paper jam./"
    End If
    LocalStatus = mydata And 8
    If LocalStatus = 8 Then
        SendMsg ("0008NQ0201AD") '"The paper is ending./"
    End If
    LocalStatus = mydata And 16
    If LocalStatus = 16 Then
        SendMsg ("0008NQ0201AE") '"Wait.It's printing./"
    End If
    LocalStatus = mydata And 32
    If LocalStatus = 32 Then
        SendMsg ("0008NQ0201AF") '"This is a paper jam./"
    End If
    LocalStatus = mydata And 64
    If LocalStatus = 64 Then
        SendMsg ("0008NQ0201AG") '"There is a paper in the dispensor./"
    End If
    
    End Select
      
End Sub

Public Sub CheckPrinterStatus()

    MSComm1.Output = Chr$(&H1D) & "a" & Chr$(&H0) ' set command status
    ' ---- DLE + EOT + 2 --------------
    MSComm1.Output = Chr$(&H10) & Chr$(&H4) & Chr$(&H2)
End Sub

Private Sub UpdatePayoutDisplay(py As PAYOUT_SET)
Dim i As Integer, j As Integer

    sysValue.Payout.StoredValue = 0
    For i = 0 To py.pay.NumberOfCoinValues - 1
        sysValue.Payout.StoredValue = sysValue.Payout.StoredValue + (CLng(py.pay.CoinsInHopper(i).CoinValue) * CLng(py.pay.CoinsInHopper(i).CoinLevel))
        'lv.ListItems.Add , "NT" & py.pay.CoinsInHopper(i).CoinValue, py.CountryCode & " " & py.pay.CoinsInHopper(i).CoinValue / py.TrueValueMuliplier
        'lv.ListItems("NT" & py.pay.CoinsInHopper(i).CoinValue).SubItems(1) = py.pay.CoinsInHopper(i).CoinLevel
        'If py.pay.FloatMode(i) = FLOAT_PAYOUT_TO_PAYOUT Then
        '    lv.ListItems("NT" & py.pay.CoinsInHopper(i).CoinValue).SubItems(2) = "payout"
        'Else
        '    lv.ListItems("NT" & py.pay.CoinsInHopper(i).CoinValue).SubItems(2) = "stacker"
        'End If
        'If py.NoteInhibitRegister And 2 ^ i Then
        '    lv.ListItems("NT" & py.pay.CoinsInHopper(i).CoinValue).ForeColor = DISP_ON_COL
        '    lv.ListItems("NT" & py.pay.CoinsInHopper(i).CoinValue).SubItems(3) = "No"
        '    For j = 1 To 3
        '        lv.ListItems("NT" & py.pay.CoinsInHopper(i).CoinValue).ListSubItems(j).ForeColor = DISP_ON_COL
        '    Next j
        'Else
        '    lv.ListItems("NT" & py.pay.CoinsInHopper(i).CoinValue).ForeColor = DISP_OFF_COL
        '    lv.ListItems("NT" & py.pay.CoinsInHopper(i).CoinValue).SubItems(3) = "Yes"
        '    For j = 1 To 3
        '        lv.ListItems("NT" & py.pay.CoinsInHopper(i).CoinValue).ListSubItems(j).ForeColor = DISP_OFF_COL
        '    Next j
        'End If
    Next i
    
    'lblPayoutCashboxValue = Format(sysValue.Payout.CashBoxValue / py.TrueValueMuliplier, "####,0.00")
    'lblPayoutValue = Format(sysValue.Payout.StoredValue / py.TrueValueMuliplier, "#####0.00")
    'lblPayoutMin = Format(py.MinPayout / py.TrueValueMuliplier, "###0.00")
    'txtMinPayout = lblPayoutMin

End Sub
Private Sub UpdateAmountDisplay(sysAc As PAY_SYSTEM_ACCOUNT)
Dim prgVal As Integer
    
    sysAc.TotalSystemValue = sysAc.Payout.StoredValue + sysAc.Payout.CashBoxValue '+ sysValue.Hopper.StoredValue + sysValue.Hopper.CashBoxValue
    
    sysAc.TotalStoredValue = sysAc.Hopper.StoredValue + sysAc.Payout.StoredValue
    

    'If Val(txtPayout) > 0 Then
    '    txtAmountPaid = Format((sysAc.Hopper.ValueDispensed + sysAc.Payout.ValueDispensed) / 100, "###0.00")
    '    If Val(txtAmountPaid) <> Val(txtPayout) And Val(txtPayout) > 0 Then
    '        prgVal = 100 - ((((Val(txtPayout) * 100) - (Val(txtAmountPaid) * 100)) / (Val(txtPayout) * 100)) * 100)
    '        If prgVal >= 0 And prgVal <= 100 Then
    '            prg1.Value = prgVal
    '        Else
    '            prg1.Value = 0
    '        End If
    '        txtAmountPaid.ForeColor = PAY_PROCESS_COL
    '    Else
    '        txtAmountPaid.ForeColor = PAY_DONE_COL
    '        prg1.Value = 0
    '    End If
    'End If
    
    
    'lblTotal = Format(sysAc.TotalSystemValue / 100, "#####0.00")
    'lblStored = Format(sysAc.TotalStoredValue / 100, "#####0.00")
    
End Sub

Public Sub RestartMachine()
    SendMsg ("0008RQ0000  ")
End Sub
