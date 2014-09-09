'============================================================================================
'   Author :  Mary Anne C.A. Arana
'   Module :  ModWinscard.vb
'   Company:  Advanced Card Systems Ltd.
'   Date   :  July 11, 2005
'
'   Revision: (Date /Author/Description)
'             (06/23/2008/ Daryl M. Rojas / Added File Access Flag Bit to FF 04)
'                                                 
'
'=============================================================================================

Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    Friend WithEvents cmdConnect As System.Windows.Forms.Button
    Friend WithEvents cmdDisconnect As System.Windows.Forms.Button
    Friend WithEvents lstOutput As System.Windows.Forms.ListBox
    Friend WithEvents cmdCreate As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbReaderPort As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.cmdExit = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdClear = New System.Windows.Forms.Button
        Me.cmdConnect = New System.Windows.Forms.Button
        Me.cmdDisconnect = New System.Windows.Forms.Button
        Me.lstOutput = New System.Windows.Forms.ListBox
        Me.cmdCreate = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbReaderPort = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(16, 192)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(120, 24)
        Me.cmdExit.TabIndex = 16
        Me.cmdExit.Text = "Exit"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(152, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 16)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Result"
        '
        'cmdClear
        '
        Me.cmdClear.Location = New System.Drawing.Point(16, 160)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(120, 24)
        Me.cmdClear.TabIndex = 12
        Me.cmdClear.Text = "Clear"
        '
        'cmdConnect
        '
        Me.cmdConnect.Location = New System.Drawing.Point(16, 64)
        Me.cmdConnect.Name = "cmdConnect"
        Me.cmdConnect.Size = New System.Drawing.Size(120, 24)
        Me.cmdConnect.TabIndex = 10
        Me.cmdConnect.Text = "Connect"
        '
        'cmdDisconnect
        '
        Me.cmdDisconnect.Location = New System.Drawing.Point(16, 128)
        Me.cmdDisconnect.Name = "cmdDisconnect"
        Me.cmdDisconnect.Size = New System.Drawing.Size(120, 24)
        Me.cmdDisconnect.TabIndex = 11
        Me.cmdDisconnect.Text = "Disconnect"
        '
        'lstOutput
        '
        Me.lstOutput.HorizontalScrollbar = True
        Me.lstOutput.Location = New System.Drawing.Point(152, 24)
        Me.lstOutput.Name = "lstOutput"
        Me.lstOutput.ScrollAlwaysVisible = True
        Me.lstOutput.Size = New System.Drawing.Size(320, 199)
        Me.lstOutput.TabIndex = 9
        '
        'cmdCreate
        '
        Me.cmdCreate.Enabled = False
        Me.cmdCreate.Location = New System.Drawing.Point(16, 96)
        Me.cmdCreate.Name = "cmdCreate"
        Me.cmdCreate.Size = New System.Drawing.Size(120, 23)
        Me.cmdCreate.TabIndex = 18
        Me.cmdCreate.Text = "Create Files"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 23)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Select Reader"
        '
        'cmbReaderPort
        '
        Me.cmbReaderPort.Location = New System.Drawing.Point(16, 32)
        Me.cmbReaderPort.Name = "cmbReaderPort"
        Me.cmbReaderPort.Size = New System.Drawing.Size(121, 21)
        Me.cmbReaderPort.TabIndex = 20
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(488, 229)
        Me.Controls.Add(Me.cmbReaderPort)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdCreate)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.cmdConnect)
        Me.Controls.Add(Me.cmdDisconnect)
        Me.Controls.Add(Me.lstOutput)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ACOSCreateFiles"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim retCode, Protocol, hContext, hCard, ReaderCount As Long
    Dim SendLen, RecvLen As Long
    Dim SendBuff(262) As Byte
    Dim RecvBuff(262) As Byte
    Dim Aprotocol As Integer
    Public array(256) As Byte
    Public apdu As APDURec
    Const INVALID_SW1SW2 = -450
    Dim tmpArray(31) As Byte

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sReaderList, sReaderGroup As String
        Dim ctr As Integer
        Dim ReaderCount As Integer

        sReaderList = ""
        sReaderGroup = ""
        For ctr = 0 To 255
            sReaderList = sReaderList + vbNullChar
        Next

        ReaderCount = 255

        'Establish context and obtain hContext handle
        retCode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, hContext)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add(GetScardErrMsg(retCode))
        End If

        ' List PCSC card readers installed
        retCode = ModWinsCard.SCardListReaders(hContext, sReaderGroup, sReaderList, ReaderCount)

        Call LoadListToControl(cmbReaderPort, sReaderList)

        cmbReaderPort.SelectedIndex = 0
    End Sub

    ' Routines for ErrorCodes
    Public Function GetScardErrMsg(ByVal ReturnCode As Integer) As String

        Select Case ReturnCode
            Case ModWinsCard.SCARD_E_CANCELLED
                GetScardErrMsg = "The action was canceled by an SCardCancel request."
            Case ModWinsCard.SCARD_E_CANT_DISPOSE
                GetScardErrMsg = "The system could not dispose of the media in the requested manner."
            Case ModWinsCard.SCARD_E_CARD_UNSUPPORTED
                GetScardErrMsg = "The smart card does not meet minimal requirements for support."
            Case ModWinsCard.SCARD_E_DUPLICATE_READER
                GetScardErrMsg = "The reader driver didn't produce a unique reader name."
            Case ModWinsCard.SCARD_E_INSUFFICIENT_BUFFER
                GetScardErrMsg = "The data buffer for returned data is too small for the returned data."
            Case ModWinsCard.SCARD_E_INVALID_ATR
                GetScardErrMsg = "An ATR string obtained from the registry is not a valid ATR string."
            Case ModWinsCard.SCARD_E_INVALID_HANDLE
                GetScardErrMsg = "The supplied handle was invalid."
            Case ModWinsCard.SCARD_E_INVALID_PARAMETER
                GetScardErrMsg = "One or more of the supplied parameters could not be properly interpreted."
            Case ModWinsCard.SCARD_E_INVALID_TARGET
                GetScardErrMsg = "Registry startup information is missing or invalid."
            Case ModWinsCard.SCARD_E_INVALID_VALUE
                GetScardErrMsg = "One or more of the supplied parameter values could not be properly interpreted."
            Case ModWinsCard.SCARD_E_NOT_READY
                GetScardErrMsg = "The reader or card is not ready to accept commands."
            Case ModWinsCard.SCARD_E_NOT_TRANSACTED
                GetScardErrMsg = "An attempt was made to end a non-existent transaction."
            Case ModWinsCard.SCARD_E_NO_MEMORY
                GetScardErrMsg = "Not enough memory available to complete this command."
            Case ModWinsCard.SCARD_E_NO_SERVICE
                GetScardErrMsg = "The smart card resource manager is not running."
            Case ModWinsCard.SCARD_E_NO_SMARTCARD
                GetScardErrMsg = "The operation requires a smart card, but no smart card is currently in the device."
            Case ModWinsCard.SCARD_E_PCI_TOO_SMALL
                GetScardErrMsg = "The PCI receive buffer was too small."
            Case ModWinsCard.SCARD_E_PROTO_MISMATCH
                GetScardErrMsg = "The requested protocols are incompatible with the protocol currently in use with the card."
            Case ModWinsCard.SCARD_E_READER_UNAVAILABLE
                GetScardErrMsg = "The specified reader is not currently available for use."
            Case ModWinsCard.SCARD_E_READER_UNSUPPORTED
                GetScardErrMsg = "The reader driver does not meet minimal requirements for support."
            Case ModWinsCard.SCARD_E_SERVICE_STOPPED
                GetScardErrMsg = "The smart card resource manager has shut down."
            Case ModWinsCard.SCARD_E_SHARING_VIOLATION
                GetScardErrMsg = "The smart card cannot be accessed because of other outstanding connections."
            Case ModWinsCard.SCARD_E_SYSTEM_CANCELLED
                GetScardErrMsg = "The action was canceled by the system, presumably to log off or shut down."
            Case ModWinsCard.SCARD_E_TIMEOUT
                GetScardErrMsg = "The user-specified timeout value has expired."
            Case ModWinsCard.SCARD_E_UNKNOWN_CARD
                GetScardErrMsg = "The specified smart card name is not recognized."
            Case ModWinsCard.SCARD_E_UNKNOWN_READER
                GetScardErrMsg = "The specified reader name is not recognized."
            Case ModWinsCard.SCARD_F_COMM_ERROR
                GetScardErrMsg = "An internal communications error has been detected."
            Case ModWinsCard.SCARD_F_INTERNAL_ERROR
                GetScardErrMsg = "An internal consistency check failed."
            Case ModWinsCard.SCARD_F_UNKNOWN_ERROR
                GetScardErrMsg = "An internal error has been detected, but the source is unknown."
            Case ModWinsCard.SCARD_F_WAITED_TOO_Integer
                GetScardErrMsg = "An internal consistency timer has expired."
            Case ModWinsCard.SCARD_S_SUCCESS
                GetScardErrMsg = "No error was encountered."
            Case ModWinsCard.SCARD_W_REMOVED_CARD
                GetScardErrMsg = "The smart card has been removed, so that further communication is not possible."
            Case ModWinsCard.SCARD_W_RESET_CARD
                GetScardErrMsg = "The smart card has been reset, so any shared state information is invalid."
            Case ModWinsCard.SCARD_W_UNPOWERED_CARD
                GetScardErrMsg = "Power has been removed from the smart card, so that further communication is not possible."
            Case ModWinsCard.SCARD_W_UNRESPONSIVE_CARD
                GetScardErrMsg = "The smart card is not responding to a reset."
            Case ModWinsCard.SCARD_W_UNSUPPORTED_CARD
                GetScardErrMsg = "The reader cannot communicate with the card, due to ATR string configuration conflicts."
            Case Else
                GetScardErrMsg = "?"
        End Select
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1
    End Function

    Public Sub LoadListToControl(ByVal Ctrl As ComboBox, ByVal ReaderList As String)

        Dim sTemp As String
        Dim indx As Integer

        indx = 1
        sTemp = ""
        Ctrl.Items.Clear()

        While (Mid(ReaderList, indx, 1) <> vbNullChar)

            While (Mid(ReaderList, indx, 1) <> vbNullChar)
                sTemp = sTemp + Mid(ReaderList, indx, 1)
                indx = indx + 1
            End While
            indx = indx + 1
            Ctrl.Items.Add(sTemp)
            sTemp = ""

        End While
    End Sub

    Private Sub cmdConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConnect.Click
        'Connect to selected reader using hContext handle and obtain hCard handle
        retCode = ModWinsCard.SCardConnect(hContext, cmbReaderPort.Text, 1, 0 Or 1, hCard, Aprotocol)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add("Connection Failed...")
            lstOutput.Items.Add(GetScardErrMsg(retCode))
            Exit Sub
        Else
            lstOutput.Items.Add("Connection Successful...")

        End If
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1

        cmdCreate.Enabled = True
    End Sub

    Private Sub cmdDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDisconnect.Click
        ' Disconnect
        retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_RESET_CARD)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add("Disconnection Error..")
            Exit Sub
        Else
            lstOutput.Items.Add("Disconnection Successful..")
        End If

        lstOutput.SelectedIndex = lstOutput.Items.Count - 1
        cmdCreate.Enabled = False

    End Sub

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        lstOutput.Items.Clear()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        ' terminate the application
        retCode = ModWinsCard.SCardReleaseContext(hContext)
        retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
        End
    End Sub

    Private Sub cmdCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreate.Click

        Call SubmitIC()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        Call SelectFile(&HFF, &H2)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        '  Write to FF 02
        '    This will create 3 User files, no Option registers and
        '    Security Option registers defined, Personalization bit
        '    is not set
        tmpArray(0) = &H0      ' 00    Option registers
        tmpArray(1) = &H0      ' 00    Security option register
        tmpArray(2) = &H3      ' 03    No of user files
        tmpArray(3) = &H0      ' 00    Personalization bit

        Call writeRecord(0, &H0, &H4, &H4, tmpArray)

        lstOutput.Items.Add("FF 02 is updated")

        '  Perform a reset for changes in the ACOS to take effect
        retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
        retCode = ModWinsCard.SCardConnect(hContext, cmbReaderPort.Text, 1, 0 Or 1, hCard, Aprotocol)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        Else
            lstOutput.Items.Add("Card reset is successful.")
        End If

        ' Select FF 04
        lstOutput.Items.Add("Select FF 04")

        Call SelectFile(&HFF, &H4)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        '  Send IC Code
        Call SubmitIC()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        ' Write to FF 04
        '  Write to first record of FF 04
        tmpArray(0) = &H5       ' 5     Record length
        tmpArray(1) = &H3       ' 3     No of records
        tmpArray(2) = &H0       ' 00    Read security attribute
        tmpArray(3) = &H0       ' 00    Write security attribute
        tmpArray(4) = &HAA      ' AA    File identifier
        tmpArray(5) = &H11      ' 11    File identifier
        tmpArray(6) = &H0       ' 00    File Access Flag Bit


        Call writeRecord(0, &H0, &H6, &H6, tmpArray)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        lstOutput.Items.Add("User File AA 11 is defined")

        '  Write to second record of FF 04
        tmpArray(0) = &HA       ' 10    Record length
        tmpArray(1) = &H2       ' 2     No of records
        tmpArray(2) = &H0       ' 00    Read security attribute
        tmpArray(3) = &H0       ' 00    Write security attribute
        tmpArray(4) = &HBB      ' BB    File identifier
        tmpArray(5) = &H22      ' 22    File identifier
        tmpArray(6) = &H0       ' 00    File Access Flag Bit

        Call writeRecord(0, &H1, &H6, &H6, tmpArray)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If
        lstOutput.Items.Add("User File BB 22 is defined")

        '  Write to third record of FF 04
        tmpArray(0) = &H6       ' 6     Record length
        tmpArray(1) = &H4       ' 4     No of records
        tmpArray(2) = &H0       ' 00    Read security attribute
        tmpArray(3) = &H0       ' 00    Write security attribute
        tmpArray(4) = &HCC      ' CC    File identifier
        tmpArray(5) = &H33      ' 33    File identifier
        tmpArray(6) = &H0       ' 00    File Access Flag Bit
        Call writeRecord(0, &H2, &H6, &H6, tmpArray)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        lstOutput.Items.Add("User File CC 33 is defined")

        '  Select 3 User Files created previously for validation
        ' Select User File AA 11
        Call SelectFile(&HAA, &H11)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        lstOutput.Items.Add("User File AA 11 is selected")

        '  Select User File BB 22
        Call SelectFile(&HBB, &H22)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        lstOutput.Items.Add("User File BB 22 is selected")

        '  Select User File CC 33
        Call SelectFile(&HCC, &H33)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        lstOutput.Items.Add("User File CC 33 is selected")
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1

    End Sub

    Private Function SubmitIC() As Long

        ' Send IC Code
        apdu.Data = array

        apdu.bCLA = &H80       ' CLA
        apdu.bINS = &H20       ' INS
        apdu.bP1 = &H7         ' P1
        apdu.bP2 = &H0         ' P2
        apdu.bP3 = &H8         ' P3

        apdu.Data(0) = &H41       ' A
        apdu.Data(1) = &H43       ' C
        apdu.Data(2) = &H4F       ' O
        apdu.Data(3) = &H53       ' S
        apdu.Data(4) = &H54       ' T
        apdu.Data(5) = &H45       ' E
        apdu.Data(6) = &H53       ' S
        apdu.Data(7) = &H54       ' T

        apdu.IsSend = True
        lstOutput.Items.Add("Submit IC")

        Call PerformTransmitAPDU(apdu)

    End Function

    Private Function SelectFile(ByVal HiAddr As Byte, ByVal LoAddr As Byte) As Long

        ' Select FF 02
        apdu.Data = array

        apdu.bCLA = &H80        ' CLA
        apdu.bINS = &HA4        ' INS
        apdu.bP1 = &H0          ' P1
        apdu.bP2 = &H0          ' P2
        apdu.bP3 = &H2          ' P3

        apdu.Data(0) = HiAddr      ' Value of High Byte
        apdu.Data(1) = LoAddr      ' Value of Low Byte

        apdu.IsSend = True
        lstOutput.Items.Add("Select FF 02")

        Call PerformTransmitAPDU(apdu)
    End Function
    Private Function writeRecord(ByVal caseType As Integer, ByVal RecNo As Byte, ByVal maxLen As Byte, _
                             ByVal DataLen As Byte, ByRef ApduIn() As Byte) As Long

        Dim i As Integer

        If caseType = 1 Then   ' If card data is to be erased before writing new data
            'Re-initialize card values to $00

            apdu.bCLA = &H80        ' CLA
            apdu.bINS = &HD2        ' INS
            apdu.bP1 = RecNo        ' Record No
            apdu.bP2 = &H0          ' P2
            apdu.bP3 = maxLen       ' Length of Data

            apdu.IsSend = True


            For i = 0 To maxLen - 1
                apdu.Data(i) = ApduIn(i)
            Next


            Call PerformTransmitAPDU(apdu)

        End If

        'Write data to card
        apdu.bCLA = &H80       ' CLA
        apdu.bINS = &HD2       ' INS
        apdu.bP1 = RecNo       ' Record No
        apdu.bP2 = &H0         ' P2
        apdu.bP3 = DataLen     ' Length of Data

        apdu.IsSend = True


        For i = 0 To maxLen - 1
            apdu.Data(i) = ApduIn(i)
        Next


        Call PerformTransmitAPDU(apdu)

    End Function

    Private Sub PerformTransmitAPDU(ByRef apdu As APDURec)

        Dim SendRequest As SCARD_IO_REQUEST
        Dim RecvRequest As SCARD_IO_REQUEST
        Dim SendBuff(255 + 5) As Byte
        Dim SendBuffLen As Integer
        Dim RecvBuff(255 + 2) As Byte
        Dim RecvBuffLen As Integer
        Dim indx As Integer
        Dim sTemp As String

        SendBuff(0) = apdu.bCLA
        SendBuff(1) = apdu.bINS
        SendBuff(2) = apdu.bP1
        SendBuff(3) = apdu.bP2
        SendBuff(4) = apdu.bP3


        If apdu.IsSend Then

            For indx = 0 To apdu.bP3
                SendBuff(5 + indx) = apdu.Data(indx)
            Next indx

            SendBuffLen = 5 + apdu.bP3
            RecvBuffLen = 2

        Else
            SendBuffLen = 5
            RecvBuffLen = 2 + apdu.bP3
        End If

        SendRequest.dwProtocol = Aprotocol
        SendRequest.cbPciLength = Len(SendRequest)

        RecvRequest.dwProtocol = Aprotocol
        RecvRequest.cbPciLength = Len(RecvRequest)

        retCode = ModWinsCard.SCardTransmit(hCard, SendRequest, SendBuff(0), SendBuffLen, SendRequest, RecvBuff(0), RecvBuffLen)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add("SCardTransmit Error!")
            lstOutput.Items.Add(GetScardErrMsg(retCode))
            Exit Sub
        Else
            lstOutput.Items.Add("SCardTransmit OK...")
        End If

        sTemp = ""
        ' do loop for sendbuffLen
        For indx = 0 To SendBuffLen - 1
            sTemp = sTemp + " " + String.Format("{0:X2}", SendBuff(indx))
        Next

        ' Display Send Buffer Value
        lstOutput.Items.Add("Send Buffer : " + sTemp)
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1

        sTemp = ""

        ' do loop for RecvbuffLen
        For indx = 0 To RecvBuffLen - 1
            sTemp = sTemp + " " + String.Format("{0:X2}", RecvBuff(indx))
        Next

        ' Display Receive Buffer Value
        lstOutput.Items.Add("Receive Buffer:" + sTemp)
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1

        If Not apdu.IsSend Then

            For indx = 0 To apdu.bP3 + 2
                apdu.Data(indx) = RecvBuff(indx)
            Next indx

        End If

        lstOutput.SelectedIndex = lstOutput.Items.Count - 1

    End Sub


End Class
'==============================================================================
' Description : This sample code demonstrate on how to create file using 
'               ACOS card in PCSC platform. 
'==============================================================================                                        
