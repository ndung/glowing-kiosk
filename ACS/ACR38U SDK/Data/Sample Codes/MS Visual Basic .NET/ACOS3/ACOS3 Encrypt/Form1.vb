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
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTerminalKey As System.Windows.Forms.TextBox
    Friend WithEvents cmdFormat As System.Windows.Forms.Button
    Friend WithEvents cmdSetValue As System.Windows.Forms.Button
    Friend WithEvents cmdSubmit As System.Windows.Forms.Button
    Friend WithEvents RBNotEnc As System.Windows.Forms.RadioButton
    Friend WithEvents GBEncrypt As System.Windows.Forms.GroupBox
    Friend WithEvents GBSucureOpt As System.Windows.Forms.GroupBox
    Friend WithEvents RB3DES As System.Windows.Forms.RadioButton
    Friend WithEvents RBDES As System.Windows.Forms.RadioButton
    Friend WithEvents GBKeyTemp As System.Windows.Forms.GroupBox
    Friend WithEvents GBCodeSub As System.Windows.Forms.GroupBox
    Friend WithEvents RBEncrypt As System.Windows.Forms.RadioButton
    Friend WithEvents txtCardKey As System.Windows.Forms.TextBox
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents cmbCode As System.Windows.Forms.ComboBox
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
        Me.GBEncrypt = New System.Windows.Forms.GroupBox
        Me.RBNotEnc = New System.Windows.Forms.RadioButton
        Me.RBEncrypt = New System.Windows.Forms.RadioButton
        Me.GBSucureOpt = New System.Windows.Forms.GroupBox
        Me.RB3DES = New System.Windows.Forms.RadioButton
        Me.RBDES = New System.Windows.Forms.RadioButton
        Me.GBKeyTemp = New System.Windows.Forms.GroupBox
        Me.cmdFormat = New System.Windows.Forms.Button
        Me.txtTerminalKey = New System.Windows.Forms.TextBox
        Me.txtCardKey = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.GBCodeSub = New System.Windows.Forms.GroupBox
        Me.cmbCode = New System.Windows.Forms.ComboBox
        Me.cmdSubmit = New System.Windows.Forms.Button
        Me.cmdSetValue = New System.Windows.Forms.Button
        Me.txtValue = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.GBEncrypt.SuspendLayout()
        Me.GBSucureOpt.SuspendLayout()
        Me.GBKeyTemp.SuspendLayout()
        Me.GBCodeSub.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(504, 376)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(104, 24)
        Me.cmdExit.TabIndex = 23
        Me.cmdExit.Text = "Exit"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(280, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 16)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Result"
        '
        'cmdClear
        '
        Me.cmdClear.Location = New System.Drawing.Point(392, 376)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(104, 24)
        Me.cmdClear.TabIndex = 22
        Me.cmdClear.Text = "Clear"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 16)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Select Reader"
        '
        'cmbReaderPort
        '
        Me.cmbReaderPort.Location = New System.Drawing.Point(8, 24)
        Me.cmbReaderPort.Name = "cmbReaderPort"
        Me.cmbReaderPort.Size = New System.Drawing.Size(128, 21)
        Me.cmbReaderPort.TabIndex = 0
        '
        'cmdConnect
        '
        Me.cmdConnect.Location = New System.Drawing.Point(160, 24)
        Me.cmdConnect.Name = "cmdConnect"
        Me.cmdConnect.Size = New System.Drawing.Size(104, 24)
        Me.cmdConnect.TabIndex = 1
        Me.cmdConnect.Text = "Connect"
        '
        'cmdDisconnect
        '
        Me.cmdDisconnect.Location = New System.Drawing.Point(280, 376)
        Me.cmdDisconnect.Name = "cmdDisconnect"
        Me.cmdDisconnect.Size = New System.Drawing.Size(104, 24)
        Me.cmdDisconnect.TabIndex = 21
        Me.cmdDisconnect.Text = "Disconnect"
        '
        'lstOutput
        '
        Me.lstOutput.HorizontalScrollbar = True
        Me.lstOutput.Location = New System.Drawing.Point(280, 24)
        Me.lstOutput.Name = "lstOutput"
        Me.lstOutput.ScrollAlwaysVisible = True
        Me.lstOutput.Size = New System.Drawing.Size(328, 342)
        Me.lstOutput.TabIndex = 19
        '
        'GBEncrypt
        '
        Me.GBEncrypt.Controls.Add(Me.RBNotEnc)
        Me.GBEncrypt.Controls.Add(Me.RBEncrypt)
        Me.GBEncrypt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBEncrypt.Location = New System.Drawing.Point(8, 64)
        Me.GBEncrypt.Name = "GBEncrypt"
        Me.GBEncrypt.Size = New System.Drawing.Size(120, 88)
        Me.GBEncrypt.TabIndex = 2
        Me.GBEncrypt.TabStop = False
        Me.GBEncrypt.Text = "Encrypt Option"
        '
        'RBNotEnc
        '
        Me.RBNotEnc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBNotEnc.Location = New System.Drawing.Point(8, 24)
        Me.RBNotEnc.Name = "RBNotEnc"
        Me.RBNotEnc.Size = New System.Drawing.Size(104, 24)
        Me.RBNotEnc.TabIndex = 3
        Me.RBNotEnc.Text = "Not Encrypted"
        '
        'RBEncrypt
        '
        Me.RBEncrypt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBEncrypt.Location = New System.Drawing.Point(8, 48)
        Me.RBEncrypt.Name = "RBEncrypt"
        Me.RBEncrypt.Size = New System.Drawing.Size(104, 24)
        Me.RBEncrypt.TabIndex = 4
        Me.RBEncrypt.Text = "Encrypted"
        '
        'GBSucureOpt
        '
        Me.GBSucureOpt.Controls.Add(Me.RB3DES)
        Me.GBSucureOpt.Controls.Add(Me.RBDES)
        Me.GBSucureOpt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBSucureOpt.Location = New System.Drawing.Point(152, 64)
        Me.GBSucureOpt.Name = "GBSucureOpt"
        Me.GBSucureOpt.Size = New System.Drawing.Size(112, 88)
        Me.GBSucureOpt.TabIndex = 5
        Me.GBSucureOpt.TabStop = False
        Me.GBSucureOpt.Text = "Security Option"
        '
        'RB3DES
        '
        Me.RB3DES.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RB3DES.Location = New System.Drawing.Point(16, 48)
        Me.RB3DES.Name = "RB3DES"
        Me.RB3DES.Size = New System.Drawing.Size(80, 24)
        Me.RB3DES.TabIndex = 7
        Me.RB3DES.Text = "3DES"
        '
        'RBDES
        '
        Me.RBDES.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBDES.Location = New System.Drawing.Point(16, 24)
        Me.RBDES.Name = "RBDES"
        Me.RBDES.Size = New System.Drawing.Size(80, 24)
        Me.RBDES.TabIndex = 6
        Me.RBDES.Text = "DES"
        '
        'GBKeyTemp
        '
        Me.GBKeyTemp.Controls.Add(Me.cmdFormat)
        Me.GBKeyTemp.Controls.Add(Me.txtTerminalKey)
        Me.GBKeyTemp.Controls.Add(Me.txtCardKey)
        Me.GBKeyTemp.Controls.Add(Me.Label4)
        Me.GBKeyTemp.Controls.Add(Me.Label3)
        Me.GBKeyTemp.Enabled = False
        Me.GBKeyTemp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBKeyTemp.Location = New System.Drawing.Point(8, 160)
        Me.GBKeyTemp.Name = "GBKeyTemp"
        Me.GBKeyTemp.Size = New System.Drawing.Size(264, 112)
        Me.GBKeyTemp.TabIndex = 8
        Me.GBKeyTemp.TabStop = False
        Me.GBKeyTemp.Text = "Key Template"
        '
        'cmdFormat
        '
        Me.cmdFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFormat.Location = New System.Drawing.Point(152, 80)
        Me.cmdFormat.Name = "cmdFormat"
        Me.cmdFormat.Size = New System.Drawing.Size(104, 23)
        Me.cmdFormat.TabIndex = 13
        Me.cmdFormat.Text = "Format"
        '
        'txtTerminalKey
        '
        Me.txtTerminalKey.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTerminalKey.Location = New System.Drawing.Point(112, 48)
        Me.txtTerminalKey.MaxLength = 8
        Me.txtTerminalKey.Name = "txtTerminalKey"
        Me.txtTerminalKey.Size = New System.Drawing.Size(136, 20)
        Me.txtTerminalKey.TabIndex = 12
        '
        'txtCardKey
        '
        Me.txtCardKey.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCardKey.Location = New System.Drawing.Point(112, 16)
        Me.txtCardKey.MaxLength = 8
        Me.txtCardKey.Name = "txtCardKey"
        Me.txtCardKey.Size = New System.Drawing.Size(136, 20)
        Me.txtCardKey.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 16)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Terminal Key"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 23)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Card Key"
        '
        'GBCodeSub
        '
        Me.GBCodeSub.Controls.Add(Me.cmbCode)
        Me.GBCodeSub.Controls.Add(Me.cmdSubmit)
        Me.GBCodeSub.Controls.Add(Me.cmdSetValue)
        Me.GBCodeSub.Controls.Add(Me.txtValue)
        Me.GBCodeSub.Controls.Add(Me.Label6)
        Me.GBCodeSub.Controls.Add(Me.Label5)
        Me.GBCodeSub.Enabled = False
        Me.GBCodeSub.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBCodeSub.Location = New System.Drawing.Point(8, 280)
        Me.GBCodeSub.Name = "GBCodeSub"
        Me.GBCodeSub.Size = New System.Drawing.Size(264, 120)
        Me.GBCodeSub.TabIndex = 14
        Me.GBCodeSub.TabStop = False
        Me.GBCodeSub.Text = "Code Submission"
        '
        'cmbCode
        '
        Me.cmbCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCode.Items.AddRange(New Object() {"PIN", "App Code 1", "App Code 2", "App Code 3", "App Code 4", "App Code 5"})
        Me.cmbCode.Location = New System.Drawing.Point(112, 16)
        Me.cmbCode.Name = "cmbCode"
        Me.cmbCode.Size = New System.Drawing.Size(136, 21)
        Me.cmbCode.TabIndex = 16
        '
        'cmdSubmit
        '
        Me.cmdSubmit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSubmit.Location = New System.Drawing.Point(152, 88)
        Me.cmdSubmit.Name = "cmdSubmit"
        Me.cmdSubmit.Size = New System.Drawing.Size(104, 23)
        Me.cmdSubmit.TabIndex = 20
        Me.cmdSubmit.Text = "Submit"
        '
        'cmdSetValue
        '
        Me.cmdSetValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSetValue.Location = New System.Drawing.Point(24, 88)
        Me.cmdSetValue.Name = "cmdSetValue"
        Me.cmdSetValue.Size = New System.Drawing.Size(104, 23)
        Me.cmdSetValue.TabIndex = 19
        Me.cmdSetValue.Text = "Set Value"
        '
        'txtValue
        '
        Me.txtValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValue.Location = New System.Drawing.Point(112, 48)
        Me.txtValue.MaxLength = 8
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(136, 20)
        Me.txtValue.TabIndex = 17
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 16)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Set Value"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 23)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Code Value"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(624, 405)
        Me.Controls.Add(Me.GBCodeSub)
        Me.Controls.Add(Me.GBKeyTemp)
        Me.Controls.Add(Me.GBSucureOpt)
        Me.Controls.Add(Me.GBEncrypt)
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
        Me.Text = "ACOS Encrypt"
        Me.GBEncrypt.ResumeLayout(False)
        Me.GBSucureOpt.ResumeLayout(False)
        Me.GBKeyTemp.ResumeLayout(False)
        Me.GBKeyTemp.PerformLayout()
        Me.GBCodeSub.ResumeLayout(False)
        Me.GBCodeSub.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim retCode, Protocol, hContext, hCard, Aprotocol, ReaderCount As Integer
    Dim sReaderList As String
    Dim sReaderGroup As String
    Dim ConnActive As Boolean
    Dim ioRequest As SCARD_IO_REQUEST
    Dim SendLen, RecvLen As Long
    Dim SendBuff(262) As Byte
    Dim RecvBuff(262) As Byte
    Dim SessionKey(15) As Byte
    Dim cKey(16) As Byte
    Dim tKey(16) As Byte
    Dim array(256) As Byte
    Dim apdu As APDURec
    Const INVALID_SW1SW2 = -450
    Dim CRnd(7) As Byte             ' Card random number
    Dim TRnd(7) As Byte             ' Terminal random number
    Dim tmpArray(31) As Byte
    Dim tmpindx As Integer


    ' this routine will encrypt 8-byte data with 8-byte key
    ' the result is stored in data
    Public Sub DES(ByVal Data() As Byte, ByVal key() As Byte)
        Call Chain3Des.Chain_DES(Data(0), key(0), 0, 1, Chain3Des.DATA_ENCRYPT)
    End Sub

    ' this routine will use 3DES algo to encrypt 8-byte data with 16-byte key
    ' the result is stored in data
    Public Sub TripleDES(ByVal Data() As Byte, ByVal key() As Byte)
        Call Chain3Des.Chain_DES(Data(0), key(0), 1, 1, Chain3Des.DATA_ENCRYPT)
    End Sub


    ' MAC as defined in ACOS manual
    ' receives 8-byte Key and 16-byte Data
    ' result is stored in Data
    Public Sub mac(ByVal Data() As Byte, ByVal key() As Byte)
        Dim i As Integer

        DES(Data, key)
        For i = 0 To 7
            Data(i) = Data(i) Xor Data(i + 8)
        Next
        DES(Data, key)
    End Sub

    ' Triple MAC as defined in ACOS manual
    ' receives 16-byte Key and 16-byte Data
    ' result is stored in Data
    Public Sub TripleMAC(ByVal Data() As Byte, ByVal key() As Byte)
        Dim i As Integer

        TripleDES(Data, key)
        For i = 0 To 7
            Data(i) = Data(i) Xor Data(i + 8)
        Next
        TripleDES(Data, key)

    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim sReaderList, sReaderGroup As String
        Dim ctr As Integer
        Dim ReaderCount As Integer

        For ctr = 0 To 255
            sReaderList = sReaderList + vbNullChar
        Next

        ReaderCount = 255

        'Establish context and obtain hContext handle
        retCode = ModWinscard.SCardEstablishContext(ModWinscard.SCARD_SCOPE_USER, 0, 0, hContext)

        If retCode <> ModWinscard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add(GetScardErrMsg(retCode))
        End If

        ' List PCSC card readers installed
        retCode = ModWinscard.SCardListReaders(hContext, sReaderGroup, sReaderList, ReaderCount)

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
        GBKeyTemp.Enabled = True
        GBCodeSub.Enabled = True

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



    Private Sub RBNotEnc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBNotEnc.Click

        txtCardKey.Text = ""
        txtTerminalKey.Text = ""
        txtCardKey.MaxLength = 8
        txtTerminalKey.MaxLength = 8

        RBDES.Checked = True
        RB3DES.Checked = False
        GBSucureOpt.Enabled = False
    End Sub



    Private Sub RBEncrypt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBEncrypt.Click
        txtCardKey.Text = ""
        txtTerminalKey.Text = ""
        RBDES.Checked = True
        GBSucureOpt.Enabled = True

    End Sub

    Private Sub RBDES_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBDES.Click
        txtCardKey.Text = ""
        txtTerminalKey.Text = ""
        txtCardKey.MaxLength = 8
        txtTerminalKey.MaxLength = 8

    End Sub

    Private Sub RB3DES_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB3DES.Click
        txtCardKey.Text = ""
        txtTerminalKey.Text = ""
        txtCardKey.MaxLength = 16
        txtTerminalKey.MaxLength = 16
    End Sub


    Private Sub txtCardKey_KeyUp(ByVal KeyCode As Integer, ByVal Shift As Integer)

        If (Len(txtCardKey.Text) >= txtCardKey.MaxLength) Then
            txtTerminalKey.Focus()
        End If

    End Sub

    Private Sub cmdFormat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFormat.Click

        Dim indx As Integer
        Dim tmpStr As String
        Dim tmpArray(31) As Byte

        ' Validate data template
        If Not ValidTemplate(0) Then
            Exit Sub
        End If

        ' Check if card inserted is an ACOS card
        If Not CheckACOS() Then
            lstOutput.Items.Add("Please insert an ACOS card.")
            Exit Sub
        End If
        lstOutput.Items.Add("ACOS card is detected.")

        ' Submit Issuer Code
        Call SubmitIC()
        'retCode = SubmitIC()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        ' Select FF 02
        Call SelectFile(&HFF, &H2)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

       

        ' Write to FF 02
        '    This step will define the Option registers and Security Option registers.
        '    Personalization bit is not set. Although Secret Codes may be set
        '    individually, this program adopts uniform encryption option for
        '    all codes to simplify coding. Issuer Code (IC) is not encrypted
        '    to remove risk of locking the ACOS card for wrong IC submission.

        If RBDES.Checked = True Then    ' DES option only
            tmpArray(0) = &H0           ' 00h  3-DES is not set
        Else
            tmpArray(0) = &H2           ' 02h  3-DES is enabled
        End If

        If RBNotEnc.Checked = True Then  ' Security Option register
            tmpArray(1) = &H0           ' 00h  Encryption is not set
        Else
            tmpArray(1) = &H7E          ' Encryption on all codes, except IC, enabled
        End If
        tmpArray(2) = &H3             ' 00    No of user files
        tmpArray(3) = &H0             ' 00    Personalization bit

        Call writeRecord(0, &H0, &H4, &H4, tmpArray)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If
        lstOutput.Items.Add("FF 02 is updated")

        'Perform a reset for changes in the ACOS to take effect
        'Reconnect reader to accommodate change of cards
        retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_RESET_CARD)
        retCode = ModWinsCard.SCardConnect(hContext, cmbReaderPort.Text, 1, 0 Or 1, hCard, Aprotocol)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add(GetScardErrMsg(retCode))
            Exit Sub
        End If

        lstOutput.Items.Add("Account files are enabled.")

        ' Submit Issuer Code to write into FF 03
        Call SubmitIC()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        '  Select FF 03
        Call SelectFile(&HFF, &H3)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

      

        ' Write to FF 03
        If RBDES.Checked = True Then    ' DES option uses 8-byte key

            '  Record 02 for Card key
            tmpStr = txtCardKey.Text
            For indx = 0 To 7
                tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx

            Call writeRecord(0, &H2, &H8, &H8, tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

            '  Record 03 for Terminal key
            tmpStr = txtTerminalKey.Text
            For indx = 0 To 7
                tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx

            Call writeRecord(0, &H3, &H8, &H8, tmpArray)
            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If
        Else                          ' 3-DES option uses 16-byte key

            '   Write Record 02 for Left half of Card key
            tmpStr = txtCardKey.Text
            For indx = 0 To 7           ' Left half of Card key
                tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx

            Call writeRecord(0, &H2, &H8, &H8, tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

            ' Record 12 for Right half of Card key
            For indx = 8 To 15          ' Right half of Card key
                tmpArray(indx - 8) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx
            Call writeRecord(0, &HC, &H8, &H8, tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

            ' Write Record 03 for Left half of Terminal key
            tmpStr = txtTerminalKey.Text
            For indx = 0 To 7           ' Left half of Terminal key
                tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx

            Call writeRecord(0, &H3, &H8, &H8, tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

            '  Record 13 for Right half of Terminal key
            For indx = 8 To 15          ' Right half of Terminal key
                tmpArray(indx - 8) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx

            Call writeRecord(0, &HD, &H8, &H8, tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If
        End If

        lstOutput.Items.Add("FF 03 is updated")
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1
    End Sub
    Private Sub ClearTextFields()

        txtCardKey.Text = ""
        txtTerminalKey.Text = ""
        txtValue.Text = ""
        cmbCode.SelectedIndex = -1

    End Sub
    Private Function ValidTemplate(ByVal valType As Integer) As Boolean

        If Len(txtCardKey.Text) < txtCardKey.MaxLength Then
            txtCardKey.Focus()
            ValidTemplate = False
            Exit Function
        End If

        If Len(txtTerminalKey.Text) < txtTerminalKey.MaxLength Then
            txtTerminalKey.Focus()
            ValidTemplate = False
            Exit Function
        End If

        If valType = 1 Then     ' validation requires code input
            If Len(txtValue.Text) < 8 Then
                txtValue.Focus()
                ValidTemplate = False
                Exit Function
            End If
        End If

        ValidTemplate = True
    End Function

    Private Function CheckACOS() As Boolean

        Dim indx As Integer
        Dim tmpStr As String

        'Reconnect reader to accommodate change of cards
        retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_RESET_CARD)
        retCode = ModWinsCard.SCardConnect(hContext, cmbReaderPort.Text, 1, 0 Or 1, hCard, Aprotocol)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add(GetScardErrMsg(retCode))
            Exit Function
        End If

        ' Check for File FF 00
        Call SelectFile(&HFF, &H0)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            CheckACOS = False
            Exit Function
        End If

       

        ' Check for File FF 01
        Call SelectFile(&HFF, &H1)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            CheckACOS = False
            Exit Function
        End If

       

        ' Check for File FF 02
        Call SelectFile(&HFF, &H2)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            CheckACOS = False
            Exit Function
        End If

      
        CheckACOS = True
    End Function

    Private Function SelectFile(ByVal HiAddr As Byte, ByVal LoAddr As Byte) As Long

        Dim indx As Integer
        Dim tmpStr As String

        apdu.Data = array
        Call ClearBuffers()

        apdu.bCLA = &H80       ' CLA
        apdu.bINS = &HA4       ' INS
        apdu.bP1 = &H0         ' P1
        apdu.bP2 = &H0         ' P2
        apdu.bP3 = &H2         ' P3

        apdu.Data(0) = HiAddr     ' Value of High Byte
        apdu.Data(1) = LoAddr     ' Value of Low Byte

        apdu.IsSend = True

        Call PerformTransmitAPDU(apdu)
       

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            SelectFile = retCode
            Exit Function
        End If

        SelectFile = retCode

    End Function

    Private Function SubmitIC() As Long

        Dim indx As Integer
        Dim tmpStr As String

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

    Private Function writeRecord(ByVal caseType As Integer, ByVal RecNo As Byte, ByVal maxLen As Byte, _
                             ByVal dataLen As Byte, ByRef ApduIn() As Byte) As Long

        Dim indx, i As Integer
        Dim tmpStr As String

        If caseType = 1 Then   ' If card data is to be erased before writing new data
            '  Re-initialize card values to $00
            Call ClearBuffers()
            apdu.bCLA = &H80        ' CLA
            apdu.bINS = &HD2         ' INS
            apdu.bP1 = RecNo       ' Record No
            apdu.bP2 = &H0         ' P2
            apdu.bP3 = maxLen     ' Length of Data

            'For indx = 0 To maxLen - 1
            'SendBuff(indx + 5) = &H0
            'Next indx

            apdu.IsSend = True

            For i = 0 To maxLen - 1
                apdu.Data(i) = ApduIn(i)
            Next

            Call PerformTransmitAPDU(apdu)

        End If

        '  Write data to card
        Call ClearBuffers()
        apdu.bCLA = &H80        ' CLA
        apdu.bINS = &HD2        ' INS
        apdu.bP1 = RecNo       ' Record No
        apdu.bP2 = &H0         ' P2
        apdu.bP3 = dataLen    ' Length of Data

        
        apdu.IsSend = True

        For i = 0 To maxLen - 1
            apdu.Data(i) = ApduIn(i)
        Next

        Call PerformTransmitAPDU(apdu)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            writeRecord = retCode
            Exit Function
        End If

        

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
        Dim ctr As Integer

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

    Private Sub cmbCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCode.SelectedIndexChanged

    End Sub

    Private Sub cmdSetValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetValue.Click
        Dim indx As Integer
        Dim tmpStr As String
        Dim tmpArray(31) As Byte
        Dim tmpByte As Byte

        '  Validate data template
        If Not ValidTemplate(1) Then
            Exit Sub
        End If

        ' Check if card inserted is an ACOS card
        If Not CheckACOS() Then
            lstOutput.Items.Add("Please insert an ACOS card")
            Exit Sub
        End If
        lstOutput.Items.Add("ACOS card is detected.")

        '  Submit Issuer Code
        Call SubmitIC()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        '  Select FF 03
        Call SelectFile(&HFF, &H3)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If


        '  Get Code Input
        tmpStr = txtValue.Text
        For indx = 0 To 7
            tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
        Next indx

        Select Case cmbCode.SelectedIndex
            Case 0
                tmpByte = &H1
                tmpStr = "PIN Code"
            Case 1
                tmpByte = &H5
                tmpStr = "Application Code 1"
            Case 2
                tmpByte = &H6
                tmpStr = "Application Code 2"
            Case 3
                tmpByte = &H7
                tmpStr = "Application Code 3"
            Case 4
                tmpByte = &H8
                tmpStr = "Application Code 4"
            Case 5
                tmpByte = &H9
                tmpStr = "Application Code 5"
        End Select

        '  Write to corresponding record in FF 03
        Call writeRecord(0, tmpByte, &H8, &H8, tmpArray)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        lstOutput.Items.Add(tmpStr + " changed successfully.")
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1
    End Sub

    Private Sub cmdSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSubmit.Click

        Dim indx As Integer
        Dim tmpStr As String
        Dim tmpByte As Byte

        ' Validate data template
        If Not ValidTemplate(1) Then
            Exit Sub
        End If

        ' Check if card inserted is an ACOS card
        If Not CheckACOS() Then
            lstOutput.Items.Add("Please insert an ACOS card")
            Exit Sub
        End If
        lstOutput.Items.Add("ACOS card is detected.")

        '  Get Code input
        tmpStr = txtValue.Text
        For indx = 0 To 7
            tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
        Next indx

        If RBEncrypt.Checked = True Then

            Call GetSessionKey()

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

            If RBDES.Checked = True Then
                Call DES(tmpArray, SessionKey)
            Else
                Call TripleDES(tmpArray, SessionKey)
            End If
        End If

        Select Case cmbCode.SelectedIndex
            Case 0
                tmpByte = &H6
                tmpStr = "PIN Code"
            Case 1
                tmpByte = &H1
                tmpStr = "Application Code 1"
            Case 2
                tmpByte = &H2
                tmpStr = "Application Code 2"
            Case 3
                tmpByte = &H3
                tmpStr = "Application Code 3"
            Case 4
                tmpByte = &H4
                tmpStr = "Application Code 4"
            Case 5
                tmpByte = &H5
                tmpStr = "Application Code 5"
        End Select

        '  Submit Issuer Code
        Call SubmitCode(tmpByte, tmpArray)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If
        lstOutput.Items.Add(tmpStr + " submitted successfully.")
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1
    End Sub

    Private Function SubmitCode(ByVal CodeType As Byte, ByRef DataIn() As Byte) As Long

        Dim indx As Integer
        Dim tmpStr As String

        Call ClearBuffers()
        apdu.bCLA = &H80        ' CLA
        apdu.bINS = &H20        ' INS
        apdu.bP1 = CodeType    ' P1
        apdu.bP2 = &H0         ' P2
        apdu.bP3 = &H8         ' P3

        apdu.IsSend = True

        For indx = 0 To 8
            apdu.Data(indx) = DataIn(indx)
        Next indx


        Call PerformTransmitAPDU(apdu)


        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            SubmitCode = retCode
            Exit Function
        End If

       

        If ACOSError(RecvBuff(0), RecvBuff(1)) Then
            SubmitCode = INVALID_SW1SW2
            Exit Function
        End If

       

        SubmitCode = retCode

    End Function

    Private Function GetSessionKey() As Long

        Dim indx As Integer
        Dim tmpStr As String
        Dim RecvBuff(255 + 2) As Byte
        Dim tmpArray(31) As Byte
        Dim ReverseKey(15) As Byte     ' Reverse of Terminal Key

        '  Get Card Key and Terminal Key from Key Template
        tmpStr = txtCardKey.Text

        For indx = 0 To txtCardKey.MaxLength - 1
            cKey(indx) = Asc(Mid(tmpStr, indx + 1, 1))
        Next indx

        tmpStr = txtTerminalKey.Text
        For indx = 0 To txtTerminalKey.MaxLength - 1
            tKey(indx) = Asc(Mid(tmpStr, indx + 1, 1))
        Next indx

        '  Generate random number from card
        Call StartSession()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            GetSessionKey = retCode
            Exit Function
        End If

        '  Store the random number generated by the card to Crnd
        'For indx = 0 To 7
        'CRnd(indx) = RecvBuff(indx)
        'Next indx

        '  Encrypt Random No (CRnd) with Terminal Key (tKey)
        '    tmpArray will hold the 8-byte Enrypted number
        For indx = 0 To 7
            tmpArray(indx) = CRnd(indx)
        Next indx

        If RBDES.Checked = True Then
            Call DES(tmpArray, tKey)
        Else
            Call TripleDES(tmpArray, tKey)
        End If

        '  Issue Authenticate command using 8-byte Encrypted No (tmpArray)
        '    and Random Terminal number (TRnd)
        For indx = 0 To 7
            tmpArray(indx + 8) = TRnd(indx)
        Next indx

        Call Authenticate(tmpArray)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            GetSessionKey = retCode
            Exit Function
        End If

        '  Compute for Session Key
        If RBDES.Checked = True Then

            '  for single DES
            ' prepare SessionKey
            ' SessionKey = DES (DES(RNDc, KC) XOR RNDt, KT)

            ' calculate DES(cRnd,cKey)
            For indx = 0 To 7
                tmpArray(indx) = CRnd(indx)
            Next indx

            Call DES(tmpArray, cKey)

            ' XOR the result with tRnd
            For indx = 0 To 7
                tmpArray(indx) = tmpArray(indx) Xor TRnd(indx)
            Next indx

            ' DES the result with tKey
            Call DES(tmpArray, tKey)

            ' temp now holds the SessionKey
            For indx = 0 To 7
                SessionKey(indx) = tmpArray(indx)
            Next indx
        Else

            '  for triple DES
            ' prepare SessionKey
            ' Left half SessionKey =  3DES (3DES (CRnd, cKey), tKey)
            ' Right half SessionKey = 3DES (TRnd, REV (tKey))

            ' tmpArray = 3DES (CRnd, cKey)
            For indx = 0 To 7
                tmpArray(indx) = CRnd(indx)
            Next indx
            Call TripleDES(tmpArray, cKey)

            ' tmpArray = 3DES (tmpArray, tKey)
            Call TripleDES(tmpArray, tKey)

            ' tmpArray holds the left half of SessionKey
            For indx = 0 To 7
                SessionKey(indx) = tmpArray(indx)
            Next indx

            ' compute ReverseKey of tKey
            ' just swap its left side with right side
            ' ReverseKey = right half of tKey + left half of tKey
            For indx = 0 To 7
                ReverseKey(indx) = tKey(8 + indx)
            Next indx
            For indx = 0 To 7
                ReverseKey(8 + indx) = tKey(indx)
            Next indx

            ' compute tmpArray = 3DES (TRnd, ReverseKey)
            For indx = 0 To 7
                tmpArray(indx) = TRnd(indx)
            Next indx
            Call TripleDES(tmpArray, ReverseKey)

            ' tmpArray holds the right half of SessionKey
            For indx = 0 To 7
                SessionKey(indx + 8) = tmpArray(indx)
            Next indx
        End If

        GetSessionKey = retCode

    End Function

    Private Function StartSession() As Long

        Dim indx As Integer
        Dim tmpStr As String

        Call ClearBuffers()
        apdu.bCLA = &H80        ' CLA
        apdu.bINS = &H84        ' INS
        apdu.bP1 = &H0         ' P1
        apdu.bP2 = &H0         ' P2
        apdu.bP3 = &H8         ' P3

        apdu.IsSend = False

        

        Call PerformTransmitAPDU(apdu)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            StartSession = retCode
            Exit Function
        End If

        
        ' Store the random number generated by the card to Crnd
        For tmpindx = 0 To 7
            CRnd(tmpindx) = apdu.Data(tmpindx)
        Next tmpindx

        StartSession = retCode



    End Function

    Private Function Authenticate(ByRef DataIn() As Byte) As Long

        Dim indx As Integer
        Dim tmpStr As String

        Call ClearBuffers()
        apdu.bCLA = &H80        ' CLA
        apdu.bINS = &H82        ' INS
        apdu.bP1 = &H0         ' P1
        apdu.bP2 = &H0         ' P2
        apdu.bP3 = &H10        ' P3

        apdu.IsSend = True

        For indx = 0 To 15
            apdu.Data(indx) = DataIn(indx)
        Next indx

       

        Call PerformTransmitAPDU(apdu)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Authenticate = retCode
            Exit Function
        End If


        If ACOSError(RecvBuff(0), RecvBuff(1)) Then
            Authenticate = INVALID_SW1SW2
            Exit Function
        End If


        Authenticate = retCode

    End Function

    Private Function ACOSError(ByVal Sw1 As Byte, ByVal Sw2 As Byte) As Boolean

        ' Check for error returned by ACOS card
        ACOSError = True
        If ((Sw1 = &H62) And (Sw2 = &H81)) Then
            lstOutput.Items.Add("Account data may be corrupted.")
            Exit Function
        End If
        If (Sw1 = &H63) Then
            lstOutput.Items.Add("MAC cryptographic checksum is wrong.")
            Exit Function
        End If
        If ((Sw1 = &H69) And (Sw2 = &H66)) Then
            lstOutput.Items.Add("Command not available or option bit not set.")
            Exit Function
        End If
        If ((Sw1 = &H69) And (Sw2 = &H82)) Then
            lstOutput.Items.Add("Security status not satisfied. Secret code, IC or PIN not submitted.")
            Exit Function
        End If
        If ((Sw1 = &H69) And (Sw2 = &H83)) Then
            lstOutput.Items.Add("The specified code is locked.")
            Exit Function
        End If
        If ((Sw1 = &H69) And (Sw2 = &H85)) Then
            lstOutput.Items.Add("Preceding transaction was not DEBIT or mutual authentication has not been completed.")
            Exit Function
        End If
        If ((Sw1 = &H69) And (Sw2 = &HF0)) Then
            lstOutput.Items.Add("Data in account is inconsistent. No access unless in Issuer mode.")
            Exit Function
        End If
        If ((Sw1 = &H6A) And (Sw2 = &H82)) Then
            lstOutput.Items.Add("Account does not exist.")
            Exit Function
        End If
        If ((Sw1 = &H6A) And (Sw2 = &H83)) Then
            lstOutput.Items.Add("Record not found or file too short.")
            Exit Function
        End If
        If ((Sw1 = &H6A) And (Sw2 = &H86)) Then
            lstOutput.Items.Add("P1 or P2 is incorrect.")
            Exit Function
        End If
        If ((Sw1 = &H6B) And (Sw2 = &H20)) Then
            lstOutput.Items.Add("Invalid amount in DEBIT/CREDIT command.")
            Exit Function
        End If
        If (Sw1 = &H6C) Then
            lstOutput.Items.Add("Issue GET RESPONSE with P3 = " & Hex(Sw2) & " to get response data.")
            Exit Function
        End If
        If (Sw1 = &H6D) Then
            lstOutput.Items.Add("Unknown INS.")
            Exit Function
        End If
        If (Sw1 = &H6E) Then
            lstOutput.Items.Add("Unknown CLA.")
            Exit Function
        End If
        If ((Sw1 = &H6F) And (Sw2 = &H10)) Then
            lstOutput.Items.Add("Account Transaction Counter at maximum. No more transaction possible.")
            Exit Function
        End If
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1

        ACOSError = False

    End Function

   
    Private Sub RBDES_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBDES.CheckedChanged
        txtCardKey.MaxLength = 8
        txtTerminalKey.MaxLength = 8
        ClearTextFields()
    End Sub


    Private Sub RB3DES_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB3DES.CheckedChanged
        txtCardKey.MaxLength = 16
        txtTerminalKey.MaxLength = 16
        ClearTextFields()
    End Sub

End Class
'==========================================================
' Description : This sample program demonstrate on how to  
'               Encrypt ACOS card using PCSC platform.
'
'==========================================================