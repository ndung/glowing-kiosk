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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbReaderPort As System.Windows.Forms.ComboBox
    Friend WithEvents cmdConnect As System.Windows.Forms.Button
    Friend WithEvents cmdDisconnect As System.Windows.Forms.Button
    Friend WithEvents lstOutput As System.Windows.Forms.ListBox
    Friend WithEvents cmdFormat As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdRead As System.Windows.Forms.Button
    Friend WithEvents cmdWrite As System.Windows.Forms.Button
    Friend WithEvents txtData As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.cmdExit = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdClear = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbReaderPort = New System.Windows.Forms.ComboBox
        Me.cmdConnect = New System.Windows.Forms.Button
        Me.cmdDisconnect = New System.Windows.Forms.Button
        Me.lstOutput = New System.Windows.Forms.ListBox
        Me.cmdFormat = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.RadioButton3 = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmdWrite = New System.Windows.Forms.Button
        Me.cmdRead = New System.Windows.Forms.Button
        Me.txtData = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(288, 360)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(112, 24)
        Me.cmdExit.TabIndex = 16
        Me.cmdExit.Text = "Exit"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 168)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 16)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Result"
        '
        'cmdClear
        '
        Me.cmdClear.Location = New System.Drawing.Point(168, 360)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(112, 24)
        Me.cmdClear.TabIndex = 12
        Me.cmdClear.Text = "Clear"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 16)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Select Reader"
        '
        'cmbReaderPort
        '
        Me.cmbReaderPort.Location = New System.Drawing.Point(24, 32)
        Me.cmbReaderPort.Name = "cmbReaderPort"
        Me.cmbReaderPort.Size = New System.Drawing.Size(120, 21)
        Me.cmbReaderPort.TabIndex = 13
        '
        'cmdConnect
        '
        Me.cmdConnect.Location = New System.Drawing.Point(24, 72)
        Me.cmdConnect.Name = "cmdConnect"
        Me.cmdConnect.Size = New System.Drawing.Size(112, 24)
        Me.cmdConnect.TabIndex = 10
        Me.cmdConnect.Text = "Connect"
        '
        'cmdDisconnect
        '
        Me.cmdDisconnect.Location = New System.Drawing.Point(48, 360)
        Me.cmdDisconnect.Name = "cmdDisconnect"
        Me.cmdDisconnect.Size = New System.Drawing.Size(112, 24)
        Me.cmdDisconnect.TabIndex = 11
        Me.cmdDisconnect.Text = "Disconnect"
        '
        'lstOutput
        '
        Me.lstOutput.HorizontalScrollbar = True
        Me.lstOutput.Location = New System.Drawing.Point(16, 192)
        Me.lstOutput.Name = "lstOutput"
        Me.lstOutput.ScrollAlwaysVisible = True
        Me.lstOutput.Size = New System.Drawing.Size(384, 160)
        Me.lstOutput.TabIndex = 9
        '
        'cmdFormat
        '
        Me.cmdFormat.Enabled = False
        Me.cmdFormat.Location = New System.Drawing.Point(24, 104)
        Me.cmdFormat.Name = "cmdFormat"
        Me.cmdFormat.Size = New System.Drawing.Size(112, 24)
        Me.cmdFormat.TabIndex = 17
        Me.cmdFormat.Text = "Format Card"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButton2)
        Me.GroupBox1.Controls.Add(Me.RadioButton1)
        Me.GroupBox1.Controls.Add(Me.RadioButton3)
        Me.GroupBox1.Enabled = False
        Me.GroupBox1.Location = New System.Drawing.Point(160, 16)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(88, 96)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "User File"
        '
        'RadioButton2
        '
        Me.RadioButton2.Location = New System.Drawing.Point(16, 40)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(64, 24)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.Text = "BB 22"
        '
        'RadioButton1
        '
        Me.RadioButton1.Location = New System.Drawing.Point(16, 16)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(64, 24)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.Text = "AA 11"
        '
        'RadioButton3
        '
        Me.RadioButton3.Location = New System.Drawing.Point(16, 64)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(64, 24)
        Me.RadioButton3.TabIndex = 19
        Me.RadioButton3.Text = "CC 33"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdWrite)
        Me.GroupBox2.Controls.Add(Me.cmdRead)
        Me.GroupBox2.Enabled = False
        Me.GroupBox2.Location = New System.Drawing.Point(256, 16)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(136, 96)
        Me.GroupBox2.TabIndex = 19
        Me.GroupBox2.TabStop = False
        '
        'cmdWrite
        '
        Me.cmdWrite.Location = New System.Drawing.Point(16, 56)
        Me.cmdWrite.Name = "cmdWrite"
        Me.cmdWrite.Size = New System.Drawing.Size(104, 24)
        Me.cmdWrite.TabIndex = 1
        Me.cmdWrite.Text = "Write"
        '
        'cmdRead
        '
        Me.cmdRead.Location = New System.Drawing.Point(16, 24)
        Me.cmdRead.Name = "cmdRead"
        Me.cmdRead.Size = New System.Drawing.Size(104, 24)
        Me.cmdRead.TabIndex = 0
        Me.cmdRead.Text = "Read"
        '
        'txtData
        '
        Me.txtData.Location = New System.Drawing.Point(160, 152)
        Me.txtData.Name = "txtData"
        Me.txtData.Size = New System.Drawing.Size(232, 20)
        Me.txtData.TabIndex = 20
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(160, 128)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(144, 23)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "String Value in Data"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(416, 389)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtData)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdFormat)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbReaderPort)
        Me.Controls.Add(Me.cmdConnect)
        Me.Controls.Add(Me.cmdDisconnect)
        Me.Controls.Add(Me.lstOutput)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ACOSReadWrite"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public retcode, Aprotocol As Integer
    Public hContext, hCard As Integer
    Public SendBuff(262), RecvBuff(262) As Byte
    Public array(256) As Byte
    Public apdu As APDURec

    Private Sub cmdConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConnect.Click

        'Connect to selected reader using hContext handle and obtain hCard handle
        retcode = ModWinsCard.SCardConnect(hContext, cmbReaderPort.Text, 1, 0 Or 1, hCard, Aprotocol)

        If retcode <> ModWinsCard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add("Connection Failed...")
            lstOutput.Items.Add(GetScardErrMsg(retcode))
            Exit Sub
        Else
            lstOutput.Items.Add("Connection Successful...")

        End If
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1

        cmdFormat.Enabled = True

    End Sub


    Private Sub cmdDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDisconnect.Click

        ' Disconnect
        retcode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_RESET_CARD)

        If retcode <> ModWinsCard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add("Disconnection Error..")
            Exit Sub
        Else
            lstOutput.Items.Add("Disconnection Successful..")
            lstOutput.SelectedIndex = lstOutput.Items.Count - 1
        End If

        GroupBox1.Enabled = False
        GroupBox2.Enabled = False
        cmdFormat.Enabled = False
    End Sub


    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        lstOutput.Items.Clear()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        ' terminate the application
        retcode = ModWinsCard.SCardReleaseContext(hContext)
        retcode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
        End
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
        retcode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, hContext)

        If retcode <> ModWinsCard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add(GetScardErrMsg(retcode))
        End If

        ' List PCSC card readers installed
        retcode = ModWinsCard.SCardListReaders(hContext, sReaderGroup, sReaderList, ReaderCount)

        Call LoadListToControl(cmbReaderPort, sReaderList)

        cmbReaderPort.SelectedIndex = 0
    End Sub


    Private Sub ClearBuffers()

        Dim indx As Long

        For indx = 0 To 262
            RecvBuff(indx) = &H0
            SendBuff(indx) = &H0
        Next indx

    End Sub


    Private Sub cmdFormat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFormat.Click

        Dim tmpArray(31) As Byte

        'Send IC Code
        lstOutput.Items.Add("Submit Code")
        Call SubmitIC()

        If retcode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        'Select FF 02
        lstOutput.Items.Add("Select FF 02")
        Call SelectFile(&HFF, &H2)

        'Write to FF 02
        '    This will create 3 User files, no Option registers and
        '    Security Option registers defined, Personalization bit
        '    is not set
        tmpArray(0) = &H0      ' 00    Option registers
        tmpArray(1) = &H0      ' 00    Security option register
        tmpArray(2) = &H3      ' 03    No of user files
        tmpArray(3) = &H0      ' 00    Personalization bit

        Call writeRecord(0, &H0, &H4, &H4, tmpArray)

        'Perform a reset for changes in the ACOS to take effect
        retcode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
        retcode = ModWinsCard.SCardConnect(hContext, cmbReaderPort.Text, 1, 0 Or 1, hCard, Aprotocol)

        'Select FF 04
        lstOutput.Items.Add("Select FF 04")
        Call SelectFile(&HFF, &H4)

        ' Send IC Code
        lstOutput.Items.Add("Submit Code")
        Call SubmitIC()

        If retcode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        ' Write to FF 04
        ' Write to first record of FF 04
        tmpArray(0) = &HA       ' 10    Record length
        tmpArray(1) = &H3       ' 3     No of records
        tmpArray(2) = &H0       ' 00    Read security attribute
        tmpArray(3) = &H0       ' 00    Write security attribute
        tmpArray(4) = &HAA      ' AA    File identifier
        tmpArray(5) = &H11      ' 11    File identifier
        tmpArray(6) = &H0      ' 00    File Access Flag Bit

        Call writeRecord(0, &H0, &H6, &H6, tmpArray)

        If retcode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        'Write to second record of FF 04
        tmpArray(0) = &H10      ' 16    Record length
        tmpArray(1) = &H2       ' 2     No of records
        tmpArray(2) = &H0       ' 00    Read security attribute
        tmpArray(3) = &H0       ' 00    Write security attribute
        tmpArray(4) = &HBB      ' BB    File identifier
        tmpArray(5) = &H22      ' 22    File identifier
        tmpArray(6) = &H0      ' 00    File Access Flag Bit

        Call writeRecord(0, &H1, &H6, &H6, tmpArray)

        If retcode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        lstOutput.Items.Add("User File BB 22 is defined")

        'Write to third record of FF 04
        tmpArray(0) = &H20      ' 32    Record length
        tmpArray(1) = &H4       ' 4     No of records
        tmpArray(2) = &H0       ' 00    Read security attribute
        tmpArray(3) = &H0       ' 00    Write security attribute
        tmpArray(4) = &HCC      ' CC    File identifier
        tmpArray(5) = &H33      ' 33    File identifier
        tmpArray(6) = &H0      ' 00    File Access Flag Bit
        Call writeRecord(0, &H2, &H6, &H6, tmpArray)

        If retcode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        lstOutput.Items.Add("User File CC 33 is defined")
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1

        GroupBox1.Enabled = True
        GroupBox2.Enabled = True

    End Sub


    Private Function SubmitIC() As Long

        apdu.Data = array
        Call ClearBuffers()

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

        apdu.Data = array
        Call ClearBuffers()

        apdu.bCLA = &H80        ' CLA
        apdu.bINS = &HA4        ' INS
        apdu.bP1 = &H0          ' P1
        apdu.bP2 = &H0          ' P2
        apdu.bP3 = &H2          ' P3

        apdu.Data(0) = HiAddr      ' Value of High Byte
        apdu.Data(1) = LoAddr      ' Value of Low Byte

        apdu.IsSend = True

        Call PerformTransmitAPDU(apdu)

    End Function


    Private Function writeRecord(ByVal caseType As Integer, ByVal RecNo As Byte, ByVal maxLen As Byte, _
                                 ByVal DataLen As Byte, ByRef ApduIn() As Byte) As Long

        Dim i As Integer

        If caseType = 1 Then   ' If card data is to be erased before writing new data
            '  Re-initialize card values to $00

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

        apdu.bCLA = &H80         ' CLA
        apdu.bINS = &HD2         ' INS
        apdu.bP1 = RecNo        ' Record No
        apdu.bP2 = &H0         ' P2
        apdu.bP3 = DataLen     ' Length of Data

        apdu.IsSend = True

        For i = 0 To maxLen - 1
            apdu.Data(i) = ApduIn(i)
        Next

        Call PerformTransmitAPDU(apdu)

    End Function


    Private Function readRecord(ByVal RecNo As Byte, ByVal dataLen As Byte) As Long

        apdu.Data = array

        ' Read data from card
        Call ClearBuffers()
        apdu.bCLA = &H80        ' CLA
        apdu.bINS = &HB2       ' INS
        apdu.bP1 = RecNo       ' Record No
        apdu.bP2 = &H0         ' P2
        apdu.bP3 = dataLen     ' Length of Data

        apdu.IsSend = False

        Call PerformTransmitAPDU(apdu)

    End Function


    Private Sub PerformTransmitAPDU(ByRef apdu As APDURec)

        Dim SendRequest As SCARD_IO_REQUEST
        Dim RecvRequest As SCARD_IO_REQUEST
        Dim SendBuffLen As Integer
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

        retcode = ModWinsCard.SCardTransmit(hCard, SendRequest, SendBuff(0), SendBuffLen, SendRequest, RecvBuff(0), RecvBuffLen)

        If retcode <> ModWinsCard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add("SCardTransmit Error!")
            lstOutput.Items.Add(GetScardErrMsg(retcode))
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


    Private Sub RadioButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.Click

        txtData.Text = ""
        txtData.MaxLength = 10
    End Sub


    Private Sub RadioButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.Click

        txtData.Text = ""
        txtData.MaxLength = 16
    End Sub


    Private Sub RadioButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.Click
        txtData.Text = ""
        txtData.MaxLength = 32
    End Sub


    Private Sub cmdRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRead.Click

        Dim indx As Integer
        Dim tmpStr, ChkStr As String
        Dim HiAddr, LoAddr, dataLen As Byte

        ' Check User File selected by user
        If RadioButton1.Checked = True Then
            HiAddr = &HAA
            LoAddr = &H11
            dataLen = &HA
            ChkStr = "91 00"
        End If

        If RadioButton2.Checked = True Then
            HiAddr = &HBB
            LoAddr = &H22
            dataLen = &H10
            ChkStr = "91 01"
        End If

        If RadioButton3.Checked = True Then
            HiAddr = &HCC
            LoAddr = &H33
            dataLen = &H20
            ChkStr = "91 02"
        End If

        ' Select User File
        Call SelectFile(HiAddr, LoAddr)

        If retcode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        ' Read First Record of User File selected
        Call readRecord(&H0, dataLen)
        If retcode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        ' Display data read from card to textbox
        tmpStr = ""
        indx = 0

        While (RecvBuff(indx) <> &H0)
            If indx < txtData.MaxLength Then
                tmpStr = tmpStr & Chr(RecvBuff(indx))
            End If
            indx = indx + 1
        End While

        txtData.Text = tmpStr
        lstOutput.Items.Add("Data read from card is displayed: " + tmpStr)
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1
    End Sub


    Private Sub cmdWrite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWrite.Click
        Dim indx As Integer
        Dim tmpStr, ChkStr As String
        Dim HiAddr, LoAddr, dataLen As Byte
        Dim tmpArray(56) As Byte

        ' Validate input template
        If txtData.Text = "" Then
            txtData.Focus()
            Exit Sub
        End If

        ' Check User File selected by user
        If RadioButton1.Checked = True Then
            HiAddr = &HAA
            LoAddr = &H11
            dataLen = &HA
            ChkStr = "91 00 "
        End If

        If RadioButton2.Checked = True Then
            HiAddr = &HBB
            LoAddr = &H22
            dataLen = &H10
            ChkStr = "91 01 "
        End If

        If RadioButton3.Checked = True Then
            HiAddr = &HCC
            LoAddr = &H33
            dataLen = &H20
            ChkStr = "91 02 "
        End If

        ' Select User File
        Call SelectFile(HiAddr, LoAddr)

        If retcode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If


        ' Write data from text box to card

        tmpStr = txtData.Text
        For indx = 0 To Len(tmpStr) - 1
            tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
        Next indx

        Call writeRecord(1, &H0, dataLen, Len(tmpStr), tmpArray)

        If retcode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If
        lstOutput.Items.Add("Data read from Text Box is written to card.")
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1

    End Sub


End Class
'===============================================================================
'Description:   This sample program demonstrates on how to formatthe ACOS card 
'                and how to  read/write data into it using the PC/SC platform
'
'===============================================================================
