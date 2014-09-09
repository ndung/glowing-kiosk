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
    Friend WithEvents GBSucureOpt As System.Windows.Forms.GroupBox
    Friend WithEvents RB3DES As System.Windows.Forms.RadioButton
    Friend WithEvents RBDES As System.Windows.Forms.RadioButton
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbReaderPort As System.Windows.Forms.ComboBox
    Friend WithEvents cmdConnect As System.Windows.Forms.Button
    Friend WithEvents cmdDisconnect As System.Windows.Forms.Button
    Friend WithEvents lstOutput As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCredit As System.Windows.Forms.TextBox
    Friend WithEvents txtDebit As System.Windows.Forms.TextBox
    Friend WithEvents txtCertify As System.Windows.Forms.TextBox
    Friend WithEvents cmdFormat As System.Windows.Forms.Button
    Friend WithEvents txtRevoke As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmdCredit As System.Windows.Forms.Button
    Friend WithEvents cmdDebit As System.Windows.Forms.Button
    Friend WithEvents cmdRevoke As System.Windows.Forms.Button
    Friend WithEvents cmdInquire As System.Windows.Forms.Button
    Friend WithEvents cb_rdc As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.GBSucureOpt = New System.Windows.Forms.GroupBox
        Me.RB3DES = New System.Windows.Forms.RadioButton
        Me.RBDES = New System.Windows.Forms.RadioButton
        Me.cmdExit = New System.Windows.Forms.Button
        Me.cmdClear = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbReaderPort = New System.Windows.Forms.ComboBox
        Me.cmdConnect = New System.Windows.Forms.Button
        Me.cmdDisconnect = New System.Windows.Forms.Button
        Me.lstOutput = New System.Windows.Forms.ListBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtRevoke = New System.Windows.Forms.TextBox
        Me.txtCertify = New System.Windows.Forms.TextBox
        Me.txtDebit = New System.Windows.Forms.TextBox
        Me.txtCredit = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdFormat = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cb_rdc = New System.Windows.Forms.CheckBox
        Me.cmdRevoke = New System.Windows.Forms.Button
        Me.cmdInquire = New System.Windows.Forms.Button
        Me.cmdDebit = New System.Windows.Forms.Button
        Me.cmdCredit = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtAmount = New System.Windows.Forms.TextBox
        Me.GBSucureOpt.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GBSucureOpt
        '
        Me.GBSucureOpt.Controls.Add(Me.RB3DES)
        Me.GBSucureOpt.Controls.Add(Me.RBDES)
        Me.GBSucureOpt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBSucureOpt.Location = New System.Drawing.Point(152, 24)
        Me.GBSucureOpt.Name = "GBSucureOpt"
        Me.GBSucureOpt.Size = New System.Drawing.Size(104, 80)
        Me.GBSucureOpt.TabIndex = 36
        Me.GBSucureOpt.TabStop = False
        Me.GBSucureOpt.Text = "Security Option"
        '
        'RB3DES
        '
        Me.RB3DES.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RB3DES.Location = New System.Drawing.Point(16, 48)
        Me.RB3DES.Name = "RB3DES"
        Me.RB3DES.Size = New System.Drawing.Size(64, 24)
        Me.RB3DES.TabIndex = 7
        Me.RB3DES.Text = "3DES"
        '
        'RBDES
        '
        Me.RBDES.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBDES.Location = New System.Drawing.Point(16, 24)
        Me.RBDES.Name = "RBDES"
        Me.RBDES.Size = New System.Drawing.Size(64, 24)
        Me.RBDES.TabIndex = 6
        Me.RBDES.Text = "DES"
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(496, 416)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(104, 24)
        Me.cmdExit.TabIndex = 40
        Me.cmdExit.Text = "Exit"
        '
        'cmdClear
        '
        Me.cmdClear.Location = New System.Drawing.Point(384, 416)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(104, 24)
        Me.cmdClear.TabIndex = 39
        Me.cmdClear.Text = "Clear"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 16)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "Select Reader"
        '
        'cmbReaderPort
        '
        Me.cmbReaderPort.Location = New System.Drawing.Point(16, 32)
        Me.cmbReaderPort.Name = "cmbReaderPort"
        Me.cmbReaderPort.Size = New System.Drawing.Size(120, 21)
        Me.cmbReaderPort.TabIndex = 34
        '
        'cmdConnect
        '
        Me.cmdConnect.Location = New System.Drawing.Point(16, 64)
        Me.cmdConnect.Name = "cmdConnect"
        Me.cmdConnect.Size = New System.Drawing.Size(112, 24)
        Me.cmdConnect.TabIndex = 35
        Me.cmdConnect.Text = "Connect"
        '
        'cmdDisconnect
        '
        Me.cmdDisconnect.Location = New System.Drawing.Point(272, 416)
        Me.cmdDisconnect.Name = "cmdDisconnect"
        Me.cmdDisconnect.Size = New System.Drawing.Size(104, 24)
        Me.cmdDisconnect.TabIndex = 38
        Me.cmdDisconnect.Text = "Disconnect"
        '
        'lstOutput
        '
        Me.lstOutput.HorizontalScrollbar = True
        Me.lstOutput.Location = New System.Drawing.Point(272, 8)
        Me.lstOutput.Name = "lstOutput"
        Me.lstOutput.ScrollAlwaysVisible = True
        Me.lstOutput.Size = New System.Drawing.Size(328, 394)
        Me.lstOutput.TabIndex = 37
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtRevoke)
        Me.GroupBox1.Controls.Add(Me.txtCertify)
        Me.GroupBox1.Controls.Add(Me.txtDebit)
        Me.GroupBox1.Controls.Add(Me.txtCredit)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(8, 128)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(248, 128)
        Me.GroupBox1.TabIndex = 42
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Security Keys"
        '
        'txtRevoke
        '
        Me.txtRevoke.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRevoke.Location = New System.Drawing.Point(88, 96)
        Me.txtRevoke.MaxLength = 8
        Me.txtRevoke.Name = "txtRevoke"
        Me.txtRevoke.Size = New System.Drawing.Size(152, 20)
        Me.txtRevoke.TabIndex = 7
        '
        'txtCertify
        '
        Me.txtCertify.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCertify.Location = New System.Drawing.Point(88, 72)
        Me.txtCertify.MaxLength = 8
        Me.txtCertify.Name = "txtCertify"
        Me.txtCertify.Size = New System.Drawing.Size(152, 20)
        Me.txtCertify.TabIndex = 6
        '
        'txtDebit
        '
        Me.txtDebit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDebit.Location = New System.Drawing.Point(88, 48)
        Me.txtDebit.MaxLength = 8
        Me.txtDebit.Name = "txtDebit"
        Me.txtDebit.Size = New System.Drawing.Size(152, 20)
        Me.txtDebit.TabIndex = 5
        '
        'txtCredit
        '
        Me.txtCredit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCredit.Location = New System.Drawing.Point(88, 24)
        Me.txtCredit.MaxLength = 8
        Me.txtCredit.Name = "txtCredit"
        Me.txtCredit.Size = New System.Drawing.Size(152, 20)
        Me.txtCredit.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 96)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 23)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Revoke Debit"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 23)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Certify"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 23)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Debit"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Credit"
        '
        'cmdFormat
        '
        Me.cmdFormat.Enabled = False
        Me.cmdFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFormat.Location = New System.Drawing.Point(16, 96)
        Me.cmdFormat.Name = "cmdFormat"
        Me.cmdFormat.Size = New System.Drawing.Size(112, 23)
        Me.cmdFormat.TabIndex = 43
        Me.cmdFormat.Text = "Format ACOS"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cb_rdc)
        Me.GroupBox2.Controls.Add(Me.cmdRevoke)
        Me.GroupBox2.Controls.Add(Me.cmdInquire)
        Me.GroupBox2.Controls.Add(Me.cmdDebit)
        Me.GroupBox2.Controls.Add(Me.cmdCredit)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtAmount)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(8, 264)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(248, 160)
        Me.GroupBox2.TabIndex = 44
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Account"
        '
        'cb_rdc
        '
        Me.cb_rdc.Location = New System.Drawing.Point(16, 56)
        Me.cb_rdc.Name = "cb_rdc"
        Me.cb_rdc.Size = New System.Drawing.Size(160, 24)
        Me.cb_rdc.TabIndex = 6
        Me.cb_rdc.Text = "Require Debit Certificate"
        '
        'cmdRevoke
        '
        Me.cmdRevoke.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRevoke.Location = New System.Drawing.Point(130, 119)
        Me.cmdRevoke.Name = "cmdRevoke"
        Me.cmdRevoke.Size = New System.Drawing.Size(104, 23)
        Me.cmdRevoke.TabIndex = 5
        Me.cmdRevoke.Text = "Revoke Debit"
        '
        'cmdInquire
        '
        Me.cmdInquire.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdInquire.Location = New System.Drawing.Point(130, 87)
        Me.cmdInquire.Name = "cmdInquire"
        Me.cmdInquire.Size = New System.Drawing.Size(104, 23)
        Me.cmdInquire.TabIndex = 4
        Me.cmdInquire.Text = "Inquire"
        '
        'cmdDebit
        '
        Me.cmdDebit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDebit.Location = New System.Drawing.Point(10, 119)
        Me.cmdDebit.Name = "cmdDebit"
        Me.cmdDebit.Size = New System.Drawing.Size(104, 23)
        Me.cmdDebit.TabIndex = 3
        Me.cmdDebit.Text = "Debit"
        '
        'cmdCredit
        '
        Me.cmdCredit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCredit.Location = New System.Drawing.Point(10, 87)
        Me.cmdCredit.Name = "cmdCredit"
        Me.cmdCredit.Size = New System.Drawing.Size(104, 23)
        Me.cmdCredit.TabIndex = 2
        Me.cmdCredit.Text = "Credit"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(8, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 16)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Amount"
        '
        'txtAmount
        '
        Me.txtAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.Location = New System.Drawing.Point(88, 24)
        Me.txtAmount.MaxLength = 20
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(144, 20)
        Me.txtAmount.TabIndex = 0
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(608, 453)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GBSucureOpt)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbReaderPort)
        Me.Controls.Add(Me.cmdConnect)
        Me.Controls.Add(Me.cmdDisconnect)
        Me.Controls.Add(Me.lstOutput)
        Me.Controls.Add(Me.cmdFormat)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ACOS Account"
        Me.GBSucureOpt.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

  
    Dim retCode, Protocol, hContext, hCard, Aprotocol As Integer
    Dim SendLen As Long
    Dim SendBuff(262) As Byte
    Dim RecvBuff(262) As Byte
    Dim array(256) As Byte
    Dim apdu As APDURec
    Const INVALID_SW1SW2 = -450

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
        cmbReaderPort.SelectedIndex = -1
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

        RBDES.Checked = True

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add("Connection Failed...")
            lstOutput.Items.Add(GetScardErrMsg(retCode))
            Exit Sub
        Else
            lstOutput.Items.Add("Connection Successful...")
        End If
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1

        GBSucureOpt.Enabled = True
        cmdFormat.Enabled = True

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

    Private Sub RBDES_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBDES.Click
        Call ClearFields()
        txtCredit.MaxLength = 8
        txtDebit.MaxLength = 8
        txtCertify.MaxLength = 8
        txtRevoke.MaxLength = 8
    End Sub

    Private Sub RB3DES_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBDES.Click
        Call ClearFields()
        txtCredit.MaxLength = 16
        txtDebit.MaxLength = 16
        txtCertify.MaxLength = 16
        txtRevoke.MaxLength = 16
    End Sub
    Private Sub ClearFields()

        txtCredit.Text = ""
        txtDebit.Text = ""
        txtCertify.Text = ""
        txtRevoke.Text = ""
        txtAmount.Text = ""
        cb_rdc.Checked = False
    End Sub
    Private Sub PerformTransmitAPDU(ByRef apdu As APDURec)

        Dim SendRequest, RecvRequest As SCARD_IO_REQUEST
        Dim SendBuffLen, RecvBuffLen As Integer
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
    Private Sub cmdFormat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFormat.Click
        Dim indx As Integer
        Dim tmpStr As String
        Dim tmpArray(31) As Byte

        'Validate data template
        If Not ValidTemplate() Then
            If txtCredit.MaxLength = 8 Then
                lstOutput.Items.Add("Please enter 8 digit security keys")
            Else
                lstOutput.Items.Add("Please enter 16 digit security keys")
            End If
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
            tmpArray(0) = &H29         ' 00h  3-DES is not set
        Else
            tmpArray(0) = &H2B            ' 02h  3-DES is enabled
        End If
        tmpArray(1) = &H0             ' 00    Security option register
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

        'Submit Issuer Code to write into FF 05 and FF 06
        Call SubmitIC()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        '  Select FF 05
        Call SelectFile(&HFF, &H5)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If
        ' Write to FF 05
        'Record(0)
        tmpArray(0) = &H0        ' TRANSTYP 0
        tmpArray(1) = &H0        ' (3 bytes
        tmpArray(2) = &H0        ' reserved for
        tmpArray(3) = &H0        '  BALANCE 0)

        Call writeRecord(0, &H0, &H4, &H4, tmpArray)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        'Record(1)
        tmpArray(0) = &H0          ' (2 bytes reserved
        tmpArray(1) = &H0          '  for ATC 0)
        tmpArray(2) = &H1          ' Set CHECKSUM 0
        tmpArray(3) = &H0          ' 00h

        Call writeRecord(0, &H1, &H4, &H4, tmpArray)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        ' Record 02
        tmpArray(0) = &H0          ' TRANSTYP 1
        tmpArray(1) = &H0          ' (3 bytes
        tmpArray(2) = &H0          '  reserved for
        tmpArray(3) = &H0          '  BALANCE 1)

        Call writeRecord(0, &H2, &H4, &H4, tmpArray)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        'Record 03
        tmpArray(0) = &H0          ' (2 bytes reserved
        tmpArray(1) = &H0          '  for ATC 1)
        tmpArray(2) = &H1          ' Set CHECKSUM 1
        tmpArray(3) = &H0          ' 00h

        Call writeRecord(0, &H3, &H4, &H4, tmpArray)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        'Record(4)
        tmpArray(0) = &HFF          ' (3 bytes
        tmpArray(1) = &HFF          '  initialized for
        tmpArray(2) = &HFF          '  MAX BALANCE)
        tmpArray(3) = &H0           ' 00h

        Call writeRecord(0, &H4, &H4, &H4, tmpArray)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        ' Record 05
        tmpArray(0) = &H0           ' (4 bytes
        tmpArray(1) = &H0           '  reserved
        tmpArray(2) = &H0           '  for
        tmpArray(3) = &H0           '  AID)

        Call writeRecord(0, &H5, &H4, &H4, tmpArray)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        'Record 06
        tmpArray(0) = &H0           ' (4 bytes
        tmpArray(1) = &H0           '  reserved
        tmpArray(2) = &H0           '  for
        tmpArray(3) = &H0           '  TTREF_C)

        Call writeRecord(0, &H6, &H4, &H4, tmpArray)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        'Record 07
        tmpArray(0) = &H0           ' (4 bytes
        tmpArray(1) = &H0           '  reserved
        tmpArray(2) = &H0           '  for
        tmpArray(3) = &H0           '  TTREF_D)

        Call writeRecord(0, &H7, &H4, &H4, tmpArray)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If
        lstOutput.Items.Add("FF 05 is updated")

        ' Select FF 06
        Call SelectFile(&HFF, &H6)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If


        ' Write to FF 05
        If RBDES.Checked = True Then    ' DES option uses 8-byte key

            ' Record 00 for Debit key
            tmpStr = txtDebit.Text
            For indx = 0 To 7
                tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx

            Call writeRecord(0, &H0, &H8, &H8, tmpArray)
            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

            ' Record 01 for Credit key
            tmpStr = txtCredit.Text
            For indx = 0 To 7
                tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx

            Call writeRecord(0, &H1, &H8, &H8, tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

            ' Record 02 for Certify key
            tmpStr = txtCertify.Text
            For indx = 0 To 7
                tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx

            Call writeRecord(0, &H2, &H8, &H8, tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

            '  Record 03 for Revoke Debit key
            tmpStr = txtRevoke.Text
            For indx = 0 To 7
                tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx

            Call writeRecord(0, &H3, &H8, &H8, tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If
        Else                          ' 3-DES option uses 16-byte key

            '  Record 04 for Left half of Debit key
            tmpStr = txtDebit.Text
            For indx = 0 To 7           ' Left half of Debit key
                tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx

            Call writeRecord(0, &H4, &H8, &H8, tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

            ' Record 00 for Right half of Debit key
            For indx = 8 To 15          ' Right half of Debit key
                tmpArray(indx - 8) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx

            Call writeRecord(0, &H0, &H8, &H8, tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

            ' Record 05 for Left half of Credit key
            tmpStr = txtCredit.Text
            For indx = 0 To 7           ' Left half of Credit key
                tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx

            Call writeRecord(0, &H5, &H8, &H8, tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

            ' Record 01 for Right half of Credit key
            For indx = 8 To 15          ' Right half of Credit key
                tmpArray(indx - 8) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx

            Call writeRecord(0, &H1, &H8, &H8, tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

            ' Record 06 for Left half of Certify key
            tmpStr = txtCertify.Text
            For indx = 0 To 7           ' Left half of Certify key
                tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx

            Call writeRecord(0, &H6, &H8, &H8, tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

            ' Record 02 for Right half of Certify key
            For indx = 8 To 15          ' Right half of Certify key
                tmpArray(indx - 8) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx

            Call writeRecord(0, &H2, &H8, &H8, tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

            ' Record 07 for Left half of Revoke Debit key
            tmpStr = txtRevoke.Text
            For indx = 0 To 7           ' Left half of Revoke Debit key
                tmpArray(indx) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx

            Call writeRecord(0, &H7, &H8, &H8, tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

            ' Record 03 for Right half of Revoke Debit key
            For indx = 8 To 15          ' Right half of Revoke Debit key
                tmpArray(indx - 8) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx

            Call writeRecord(0, &H3, &H8, &H8, tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

        End If

        Call ClearFields()
        lstOutput.Items.Add("FF 06 is updated")
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1

    End Sub





    Private Function ValidTemplate() As Boolean

        If Len(txtCredit.Text) < txtCredit.MaxLength Then
            txtCredit.Focus()
            txtCredit.SelectionStart = Len(txtCredit.Text)
            ValidTemplate = False
            Exit Function
        End If

        If Len(txtDebit.Text) < txtDebit.MaxLength Then
            txtDebit.SelectionStart = Len(txtDebit.Text)
            txtDebit.Focus()
            ValidTemplate = False
            Exit Function
        End If

        If Len(txtCertify.Text) < txtCertify.MaxLength Then
            txtCertify.SelectionStart = Len(txtCertify.Text)
            txtCertify.Focus()
            ValidTemplate = False
            Exit Function
        End If

        If Len(txtRevoke.Text) < txtRevoke.MaxLength Then
            txtRevoke.SelectionStart = Len(txtRevoke.Text)
            txtRevoke.Focus()
            ValidTemplate = False
            Exit Function
        End If

        ValidTemplate = True

    End Function

    Private Function CheckACOS() As Boolean

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

        Dim i As Integer

        If caseType = 1 Then   ' If card data is to be erased before writing new data
            '  Re-initialize card values to $00
            Call ClearBuffers()
            apdu.bCLA = &H80        ' CLA
            apdu.bINS = &HD2         ' INS
            apdu.bP1 = RecNo       ' Record No
            apdu.bP2 = &H0         ' P2
            apdu.bP3 = maxLen     ' Length of Data

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

    Private Sub cmdCredit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCredit.Click
        Dim indx As Integer
        Dim tmpStr As String
        Dim tmpArray(31) As Byte
        Dim Amount, tmpVal As Long
        Dim tmpKey(15) As Byte       ' Credit key to verify MAC
        Dim TTREFc(3) As Byte
        Dim ATREF(5) As Byte

        ' 1. Check if Credit key and valid Transaction value are provided
        If Len(txtCredit.Text) < txtCredit.MaxLength Then
            txtCredit.Focus()
            Exit Sub
        End If
        If txtAmount.Text = "" Then
            txtAmount.Focus()
            Exit Sub
        End If
        If Not IsNumeric(txtAmount.Text) Then
            txtAmount.Focus()
            txtAmount.SelectionStart = 0
            txtAmount.SelectionLength = Len(txtAmount.Text)
            Exit Sub
        End If
        If CLng(txtAmount.Text) > 16777215 Then
            txtAmount.Text = "16777215"
            txtAmount.Focus()
            Exit Sub
        End If

        ' 2. Check if card inserted is an ACOS card
        If Not CheckACOS() Then
            lstOutput.Items.Add("Please insert an ACOS card.")
            Exit Sub
        End If
        lstOutput.Items.Add("ACOS card is detected.")

        ' 3. Issue INQUIRE ACCOUNT command using any arbitrary data and Credit key
        '    Arbitrary data is 1111h
        For indx = 0 To 3
            tmpArray(indx) = &H1
        Next indx

        Call InquireAccount(&H2, tmpArray)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        ' 4. Issue GET RESPONSE command with Le = 19h or 25 bytes
        Call GetResponse(RecvBuff)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        ' 5. Store ACOS card values for TTREFc and ATREF
        For indx = 0 To 3
            TTREFc(indx) = RecvBuff(indx + 17)
        Next indx
        For indx = 0 To 5
            ATREF(indx) = RecvBuff(indx + 8)
        Next indx

        ' 6. Prepare MAC data block: E2 + AMT + TTREFc + ATREF + 00 + 00
        '    use tmpArray as the data block
        Amount = CLng(txtAmount.Text)
        tmpArray(0) = &HE2
        tmpVal = Int(Amount / 256)
        tmpVal = Int(tmpVal / 256)
        tmpArray(1) = tmpVal Mod 256                  ' Amount MSByte
        tmpVal = Int(Amount / 256)
        tmpArray(2) = tmpVal Mod 256                  ' Amount Middle Byte
        tmpArray(3) = Amount Mod 256                  ' Amount LSByte

        For indx = 0 To 3
            tmpArray(indx + 4) = TTREFc(indx)
        Next indx

        For indx = 0 To 5
            tmpArray(indx + 8) = ATREF(indx)
        Next indx

        tmpArray(13) = tmpArray(13) + 1               ' increment last byte of ATREF
        tmpArray(14) = &H0
        tmpArray(15) = &H0

        ' 7. Generate applicable MAC values, MAC result will be stored in tmpArray
        tmpStr = txtCredit.Text
        For indx = 0 To Len(tmpStr) - 1
            tmpKey(indx) = Asc(Mid(tmpStr, indx + 1, 1))
        Next indx
        If RBDES.Checked = True Then
            Call mac(tmpArray, tmpKey)
        Else
            Call TripleMAC(tmpArray, tmpKey)
        End If

        ' 8. Format Credit command data and execute credit command
        '    Using tmpArray, the first four bytes are carried over
        tmpVal = Int(Amount / 256)
        tmpVal = Int(tmpVal / 256)
        tmpArray(4) = tmpVal Mod 256                  ' Amount MSByte
        tmpVal = Int(Amount / 256)
        tmpArray(5) = tmpVal Mod 256                  ' Amount Middle Byte
        tmpArray(6) = Amount Mod 256                  ' Amount LSByte

        For indx = 0 To 3
            tmpArray(indx + 7) = ATREF(indx)
        Next indx
        Call CreditAmount(tmpArray)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        lstOutput.Items.Add("Credit transaction completed")
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1
        Call ClearFields()
    End Sub

    Private Function InquireAccount(ByVal keyNo As Byte, ByRef DataIn() As Byte) As Long

        Dim indx As Integer
        Dim tmpStr As String

        Call ClearBuffers()
        apdu.bCLA = &H80    ' CLA
        apdu.bINS = &HE4    ' INS
        apdu.bP1 = keyNo    ' Key No
        apdu.bP2 = &H0      ' P2
        apdu.bP3 = &H4      ' Length of Data

        apdu.IsSend = True

        For indx = 0 To 3
            apdu.Data(indx) = DataIn(indx)
        Next indx

        Call PerformTransmitAPDU(apdu)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            InquireAccount = retCode
            Exit Function
        End If

        tmpStr = ""

        For indx = 0 To 1
            tmpStr = tmpStr + " " + String.Format("{0:X2}", RecvBuff(indx))
        Next indx

        If ACOSError(RecvBuff(0), RecvBuff(1)) Then
            InquireAccount = INVALID_SW1SW2
            Exit Function
        End If

        If Trim(tmpStr) <> "61 19" Then     ' SW1/SW2 must be equal to 6119h
            InquireAccount = INVALID_SW1SW2
            Exit Function
        End If

        InquireAccount = retCode
    End Function
    Private Function GetResponse(ByVal buff() As Byte) As Long

        Dim ctr As Integer

        Call ClearBuffers()
        apdu.bCLA = &H80        ' CLA
        apdu.bINS = &HC0        ' INS
        apdu.bP1 = &H0          ' P1
        apdu.bP2 = &H0          ' P2
        apdu.bP3 = &H19         ' Length of Data

        apdu.IsSend = False

        Call PerformTransmitAPDU(apdu)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            GetResponse = retCode
            Exit Function
        End If

        If ACOSError(RecvBuff(SendBuff(4)), RecvBuff(SendBuff(4) + 1)) Then
            GetResponse = INVALID_SW1SW2
            Exit Function
        End If

        For ctr = 0 To &H19
            buff(ctr) = apdu.Data(ctr)
        Next

        GetResponse = retCode

    End Function

    Private Function GetResponse_D(ByVal buff() As Byte) As Long

        Dim ctr As Integer

        Call ClearBuffers()
        apdu.bCLA = &H80        ' CLA
        apdu.bINS = &HC0        ' INS
        apdu.bP1 = &H0          ' P1
        apdu.bP2 = &H0          ' P2
        apdu.bP3 = &H4         ' Length of Data

        apdu.IsSend = False

        Call PerformTransmitAPDU(apdu)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            GetResponse_D = retCode
            Exit Function
        End If

        If ACOSError(RecvBuff(SendBuff(4)), RecvBuff(SendBuff(4) + 1)) Then
            GetResponse_D = INVALID_SW1SW2
            Exit Function
        End If

        For ctr = 0 To &H4
            buff(ctr) = apdu.Data(ctr)
        Next

        GetResponse_D = retCode

    End Function


    Private Function CreditAmount(ByRef CreditData() As Byte) As Long

        Dim indx As Integer
        Dim tmpStr As String

        Call ClearBuffers()
        apdu.bCLA = &H80        ' CLA
        apdu.bINS = &HE2        ' INS
        apdu.bP1 = &H0          ' P1
        apdu.bP2 = &H0          ' P2
        apdu.bP3 = &HB          ' P3

        apdu.IsSend = True

        For indx = 0 To 11
            apdu.Data(indx) = CreditData(indx)
        Next indx

        tmpStr = ""
        For indx = 0 To SendLen - 1
            tmpStr = tmpStr + " " + String.Format("{0:X2}", SendBuff(indx))
        Next indx
        lstOutput.Items.Add("Invoke Credit Command.")

        Call PerformTransmitAPDU(apdu)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            CreditAmount = retCode
            Exit Function
        End If

        tmpStr = ""
        For indx = 0 To 1
            tmpStr = tmpStr + " " + String.Format("{0:X2}", RecvBuff(indx))
        Next indx


        If ACOSError(RecvBuff(0), RecvBuff(1)) Then
            CreditAmount = INVALID_SW1SW2
            Exit Function
        End If

        If Trim(tmpStr) <> "90 00" Then
            CreditAmount = INVALID_SW1SW2
            Exit Function
        End If

        CreditAmount = retCode


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

    Private Sub cmdDebit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDebit.Click
        Dim i, indx As Integer
        Dim tmpStr As String
        Dim tmpArray(31) As Byte
        Dim Amount, tmpVal As Long
        Dim tmpKey(15) As Byte        ' Debit key to verify MAC
        Dim TTREFd(3) As Byte
        Dim ATREF(5) As Byte
        Dim tmpBalance(3) As Long
        Dim new_balance As Long

        ' Check if Debit key and valid Transaction value are provided
        If Len(txtDebit.Text) < txtDebit.MaxLength Then
            txtDebit.Focus()
            Exit Sub
        End If
        If txtAmount.Text = "" Then
            txtAmount.Focus()
            Exit Sub
        End If
        If Not IsNumeric(txtAmount.Text) Then
            txtAmount.Focus()
            txtAmount.SelectionStart = 0
            txtAmount.SelectionLength = Len(txtAmount.Text)
            Exit Sub
        End If
        If CLng(txtAmount.Text) > 16777215 Then
            txtAmount.Text = "16777215"
            txtAmount.Focus()
            Exit Sub
        End If

        '  Check if card inserted is an ACOS card
        If Not CheckACOS() Then
            lstOutput.Items.Add("Please insert an ACOS card.")
            Exit Sub
        End If
        lstOutput.Items.Add("ACOS card is detected.")

        ' Issue INQUIRE ACCOUNT command using any arbitrary data and Credit key
        '    Arbitrary data is 1111h
        For indx = 0 To 3
            tmpArray(indx) = &H1
        Next indx
        Call InquireAccount(&H2, tmpArray)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        ' Issue GET RESPONSE command with Le = 19h or 25 bytes
        Call GetResponse(RecvBuff)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        tmpBalance(1) = RecvBuff(7)
        tmpBalance(2) = RecvBuff(6)
        tmpBalance(2) = tmpBalance(2) * 256
        tmpBalance(3) = RecvBuff(5)
        tmpBalance(3) = tmpBalance(3) * 256
        tmpBalance(3) = tmpBalance(3) * 256
        tmpBalance(0) = tmpBalance(1) + tmpBalance(2) + tmpBalance(3)


        '  Store ACOS card values for TTREFd and ATREF
        For indx = 0 To 3
            TTREFd(indx) = RecvBuff(indx + 21)
        Next indx

        For indx = 0 To 5
            ATREF(indx) = RecvBuff(indx + 8)
        Next indx

        '  Prepare MAC data block: E6 + AMT + TTREFd + ATREF + 00 + 00
        '    use tmpArray as the data block
        Amount = CLng(txtAmount.Text)
        tmpArray(0) = &HE6
        tmpVal = Int(Amount / 256)
        tmpVal = Int(tmpVal / 256)
        tmpArray(1) = tmpVal Mod 256                  ' Amount MSByte
        tmpVal = Int(Amount / 256)
        tmpArray(2) = tmpVal Mod 256                  ' Amount Middle Byte
        tmpArray(3) = Amount Mod 256                  ' Amount LSByte

        For indx = 0 To 3
            tmpArray(indx + 4) = TTREFd(indx)
        Next indx

        For indx = 0 To 5
            tmpArray(indx + 8) = ATREF(indx)
        Next indx
        tmpArray(13) = tmpArray(13) + 1               ' increment last byte of ATREF
        tmpArray(14) = &H0
        tmpArray(15) = &H0

        'Generate applicable MAC values, MAC result will be stored in tmpArray
        tmpStr = txtDebit.Text
        For indx = 0 To Len(tmpStr) - 1
            tmpKey(indx) = Asc(Mid(tmpStr, indx + 1, 1))
        Next indx

        If RBDES.Checked = True Then
            Call mac(tmpArray, tmpKey)
        Else
            Call TripleMAC(tmpArray, tmpKey)
        End If

        '  Format Debit command data and execute debit command
        '    Using tmpArray, the first four bytes are carried over
        tmpVal = Int(Amount / 256)
        tmpVal = Int(tmpVal / 256)
        tmpArray(4) = tmpVal Mod 256                  ' Amount MSByte
        tmpVal = Int(Amount / 256)
        tmpArray(5) = tmpVal Mod 256                  ' Amount Middle Byte
        tmpArray(6) = Amount Mod 256                  ' Amount LSByte

        For indx = 0 To 5
            tmpArray(indx + 7) = ATREF(indx)
        Next indx

        'Without Debit Certificate
        If cb_rdc.Checked = False Then

            Call DebitAmount(tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

        Else 'With Debit Certificate

            Call DebitAmountwithDBC(tmpArray)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If


            'Issue GET RESPONSE command with Le = 4h
            Call GetResponse_D(RecvBuff)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Exit Sub
            End If

            'Prepare MAC data block: 01 + New Balance + ATC + TTREFD + 00 + 00 + 00
            '    use tmpArray as the data block

            Amount = CLng(txtAmount.Text)
            new_balance = tmpBalance(0) - Amount
            tmpArray(0) = &H1


            tmpVal = Int(new_balance / 256)
            tmpVal = Int(tmpVal / 256)
            tmpArray(1) = tmpVal Mod 256                  ' Amount MSByte
            tmpVal = Int(new_balance / 256)
            tmpArray(2) = tmpVal Mod 256                  ' Amount Middle Byte
            tmpArray(3) = new_balance Mod 256                  ' Amount LSByte

            tmpVal = Int(Amount / 256)
            tmpVal = Int(tmpVal / 256)
            tmpArray(4) = tmpVal Mod 256                  ' Amount MSByte
            tmpVal = Int(Amount / 256)
            tmpArray(5) = tmpVal Mod 256                  ' Amount Middle Byte
            tmpArray(6) = Amount Mod 256                  ' Amount LSByte
            tmpArray(7) = ATREF(4)
            tmpArray(8) = ATREF(5) + 1                    ' Increment ATC after every transaction

            For indx = 0 To 3
                tmpArray(indx + 9) = TTREFd(indx)
            Next indx

            tmpArray(13) = &H0
            tmpArray(14) = &H0
            tmpArray(15) = &H0


            'Generate applicable MAC values, MAC result will be stored in tmpArray
            tmpStr = txtDebit.Text
            For indx = 0 To Len(tmpStr) - 1
                tmpKey(indx) = Asc(Mid(tmpStr, indx + 1, 1))
            Next indx


            If RBDES.Checked = True Then
                Call mac(tmpArray, tmpKey)
            Else
                Call TripleMAC(tmpArray, tmpKey)
            End If


            For i = 0 To 3

                If RecvBuff(i) <> tmpArray(i) Then

                    MsgBox("Failed")
                    lstOutput.Items.Add("Debit Certificate Failed.")
                    cb_rdc.Checked = False

                    Exit Sub
                End If

            Next i

            lstOutput.Items.Add("Debit Certificate Verified.")

        End If

        lstOutput.Items.Add("Debit transaction completed")
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1
        Call ClearFields()


    End Sub


    Private Function DebitAmount(ByRef DebitData() As Byte) As Long

        Dim indx As Integer
        Dim tmpStr As String

        apdu.bCLA = &H80        ' CLA
        apdu.bINS = &HE6        ' INS
        apdu.bP1 = &H0         ' P1
        apdu.bP2 = &H0         ' P2
        apdu.bP3 = &HB         ' P3

        apdu.IsSend = True

        For indx = 0 To 11
            apdu.Data(indx) = DebitData(indx)
        Next indx

        tmpStr = ""
        For indx = 0 To SendLen - 1
            tmpStr = tmpStr + " " + String.Format("(0:X2)", SendBuff(indx))
        Next indx

        lstOutput.Items.Add("Invoke Debit Command.")

        Call PerformTransmitAPDU(apdu)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            DebitAmount = retCode
            Exit Function
        End If

        tmpStr = ""
        For indx = 0 To 1
            tmpStr = tmpStr + " " + String.Format("{0:X2}", RecvBuff(indx))
        Next indx

        If ACOSError(RecvBuff(0), RecvBuff(1)) Then
            DebitAmount = INVALID_SW1SW2
            Exit Function
        End If

        If tmpStr <> "9000" Then
            DebitAmount = INVALID_SW1SW2
            Exit Function
        End If

        DebitAmount = retCode

    End Function


    Private Function DebitAmountwithDBC(ByRef DebitData() As Byte) As Long

        Dim indx As Integer
        Dim tmpStr As String

        Call ClearBuffers()
        apdu.bCLA = &H80        ' CLA
        apdu.bINS = &HE6        ' INS
        apdu.bP1 = &H1          ' P1
        apdu.bP2 = &H0         ' P2
        apdu.bP3 = &HB         ' P3

        apdu.IsSend = True

        For indx = 0 To 11
            apdu.Data(indx) = DebitData(indx)
        Next indx

        tmpStr = ""
        For indx = 0 To SendLen - 1
            tmpStr = tmpStr + " " + String.Format("(0:X2)", SendBuff(indx))
        Next indx

        Call PerformTransmitAPDU(apdu)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            DebitAmountwithDBC = retCode
            Exit Function
        End If

        tmpStr = ""
        For indx = 0 To 1
            tmpStr = tmpStr + " " + String.Format("{0:X2}", RecvBuff(indx))
        Next indx

        If ACOSError(RecvBuff(0), RecvBuff(1)) Then
            DebitAmountwithDBC = INVALID_SW1SW2
            Exit Function
        End If

        If tmpStr <> "9000" Then
            DebitAmountwithDBC = INVALID_SW1SW2
            Exit Function
        End If

        DebitAmountwithDBC = retCode

    End Function


    Private Sub cmdInquire_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInquire.Click
        Dim indx As Integer
        Dim tmpStr As String
        Dim tmpArray(31) As Byte
        Dim tmpBalance(3) As Long
        Dim tmpKey(15) As Byte        ' certify key to verify MAC
        Dim LastTran As Byte
        Dim TTREFc(3) As Byte
        Dim TTREFd(3) As Byte
        Dim ATREF(5) As Byte

        ' Check if Certify key is provided
        If Len(txtCertify.Text) < txtCertify.MaxLength Then
            txtCertify.Focus()
            Exit Sub
        End If

        '  Check if card inserted is an ACOS card
        If Not CheckACOS() Then
            lstOutput.Items.Add("Please insert an ACOS card.")
            Exit Sub
        End If
        lstOutput.Items.Add("ACOS card is detected.")

        '  Issue INQUIRE ACCOUNT command using any arbitrary data and Certify key
        '    Arbitrary data is 1111h
        For indx = 0 To 3
            tmpArray(indx) = &H1
        Next indx

        Call InquireAccount(&H2, tmpArray)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        '  Issue GET RESPONSE command with Le = 19h or 25 bytes
        Call GetResponse(RecvBuff)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        '  Check integrity of data returned by card
        '  Build MAC input data
        '  Extract the info from ACOS card in Dataout
        LastTran = RecvBuff(4)
        tmpBalance(1) = RecvBuff(7)
        tmpBalance(2) = RecvBuff(6)
        tmpBalance(2) = tmpBalance(2) * 256
        tmpBalance(3) = RecvBuff(5)
        tmpBalance(3) = tmpBalance(3) * 256
        tmpBalance(3) = tmpBalance(3) * 256
        tmpBalance(0) = tmpBalance(1) + tmpBalance(2) + tmpBalance(3)

        For indx = 0 To 3
            TTREFc(indx) = RecvBuff(indx + 17)
        Next indx

        For indx = 0 To 3
            TTREFd(indx) = RecvBuff(indx + 21)
        Next indx

        For indx = 0 To 5
            ATREF(indx) = RecvBuff(indx + 8)
        Next indx

        'Move data from ACOS card as input to MAC calculations
        tmpArray(4) = RecvBuff(4)          ' 4 BYTE MAC + LAST TRANS TYPE
        For indx = 0 To 2                  ' Copy BALANCE
            tmpArray(indx + 5) = RecvBuff(indx + 5)
        Next indx

        For indx = 0 To 5                  ' Copy ATREF
            tmpArray(indx + 8) = RecvBuff(indx + 8)
        Next indx
        tmpArray(14) = &H0
        tmpArray(15) = &H0

        For indx = 0 To 3                  ' Copy TTREFc
            tmpArray(indx + 16) = TTREFc(indx)
        Next indx

        For indx = 0 To 3                  ' Copy TTREFd
            tmpArray(indx + 20) = TTREFd(indx)
        Next indx

        '  Generate applicable MAC values
        tmpStr = txtCertify.Text
        For indx = 0 To Len(tmpStr) - 1
            tmpKey(indx) = Asc(Mid(tmpStr, indx + 1, 1))
        Next indx

        If RBDES.Checked = True Then
            Call mac(tmpArray, tmpKey)
        Else
            Call TripleMAC(tmpArray, tmpKey)
        End If

        '  Compare MAC values
        For indx = 0 To 3
            If tmpArray(indx) <> RecvBuff(indx) Then
                lstOutput.Items.Add("MAC is incorrect, data integrity is jeopardized.")
                Exit For
            End If
        Next indx

        ' Display relevant data from ACOS card
        Select Case LastTran
            Case 1
                tmpStr = "DEBIT"
            Case 2
                tmpStr = "REVOKE DEBIT"
            Case 3
                tmpStr = "CREDIT"
            Case Else
                tmpStr = "NOT DEFINED"
        End Select

        lstOutput.Items.Add("Last transaction is " + tmpStr + ".")
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1
        Call ClearFields()

        txtAmount.Text = Format(tmpBalance(0), "")


    End Sub

    Private Sub cmdRevoke_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRevoke.Click
        Dim indx As Integer
        Dim tmpStr As String
        Dim tmpArray(31) As Byte
        Dim Amount, tmpVal As Long
        Dim tmpKey(15) As Byte        ' Revoke Debit key to verify MAC
        Dim TTREFd(3) As Byte
        Dim ATREF(5) As Byte

        ' Check if Debit key and valid Transaction value are provided
        If Len(txtRevoke.Text) < txtRevoke.MaxLength Then
            txtRevoke.Focus()
            Exit Sub
        End If

        If txtAmount.Text = "" Then
            txtAmount.Focus()
            Exit Sub
        End If

        If Not IsNumeric(txtAmount.Text) Then
            txtAmount.Focus()
            txtAmount.SelectionStart = 0
            txtAmount.SelectionLength = Len(txtAmount.Text)
            Exit Sub
        End If

        If CLng(txtAmount.Text) > 16777215 Then
            txtAmount.Text = "16777215"
            txtAmount.Focus()
            Exit Sub
        End If

        ' Check if card inserted is an ACOS card
        If Not CheckACOS() Then
            lstOutput.Items.Add("Please insert an ACOS card.")
            Exit Sub
        End If
        lstOutput.Items.Add("ACOS card is detected.")

        ' Issue INQUIRE ACCOUNT command using any arbitrary data and Revoke Debit key
        ' Arbitrary data is 1111h
        For indx = 0 To 3
            tmpArray(indx) = &H1
        Next indx

        retCode = InquireAccount(&H2, tmpArray)
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        ' Issue GET RESPONSE command with Le = 19h or 25 bytes
        retCode = GetResponse(RecvBuff)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        ' Store ACOS card values for TTREFd and ATREF
        For indx = 0 To 3
            TTREFd(indx) = RecvBuff(indx + 21)
        Next indx

        For indx = 0 To 5
            ATREF(indx) = RecvBuff(indx + 8)
        Next indx

        ' Store ACOS card values for TTREFd and ATREF
        For indx = 0 To 3
            TTREFd(indx) = RecvBuff(indx + 21)
        Next indx

        For indx = 0 To 5
            ATREF(indx) = RecvBuff(indx + 8)
        Next indx

        'Prepare MAC data block: E8 + AMT + TTREFd + ATREF + 00 + 00
        'use tmpArray as the data block
        Amount = CLng(txtAmount.Text)
        tmpArray(0) = &HE8
        tmpVal = Int(Amount / 256)
        tmpVal = Int(tmpVal / 256)
        tmpArray(1) = tmpVal Mod 256                  ' Amount MSByte
        tmpVal = Int(Amount / 256)
        tmpArray(2) = tmpVal Mod 256                  ' Amount Middle Byte
        tmpArray(3) = Amount Mod 256                  ' Amount LSByte

        For indx = 0 To 3
            tmpArray(indx + 4) = TTREFd(indx)
        Next indx

        For indx = 0 To 5
            tmpArray(indx + 8) = ATREF(indx)
        Next indx
        tmpArray(13) = tmpArray(13) + 1               ' increment last byte of ATREF
        tmpArray(14) = &H0
        tmpArray(15) = &H0

        '  Generate applicable MAC values, MAC result will be stored in tmpArray
        tmpStr = txtRevoke.Text
        For indx = 0 To Len(tmpStr) - 1
            tmpKey(indx) = Asc(Mid(tmpStr, indx + 1, 1))
        Next indx

        If RBDES.Checked = True Then
            Call mac(tmpArray, tmpKey)
        Else
            Call TripleMAC(tmpArray, tmpKey)
        End If

        ' Execute Revoke Debit command data and execute credit command
        ' Using tmpArray, the first four bytes storing the MAC value are carried over
        retCode = RevokeDebit(tmpArray)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        lstOutput.Items.Add("Revoke Debit transaction completed")
        lstOutput.SelectedIndex = lstOutput.Items.Count - 1
        Call ClearFields()
    End Sub


    Private Function RevokeDebit(ByRef RevDebData() As Byte) As Integer

        Dim indx As Integer
        Dim tmpStr As String

        Call ClearBuffers()
        apdu.bCLA = &H80        ' CLA
        apdu.bINS = &HE8        ' INS
        apdu.bP1 = &H0         ' P1
        apdu.bP2 = &H0         ' P2
        apdu.bP3 = &H4         ' P3

        tmpStr = ""

        apdu.IsSend = True

        For indx = 0 To 4
            apdu.Data(indx) = RevDebData(indx)
        Next indx

        For indx = 0 To SendLen - 1
            tmpStr = tmpStr + " " + String.Format("(0:X2)", SendBuff(indx))
        Next indx

        lstOutput.Items.Add("Invoke RevokeDebit Command.")

        Call PerformTransmitAPDU(apdu)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            RevokeDebit = retCode
            Exit Function
        End If

        tmpStr = ""
        For indx = 0 To 1
            tmpStr = tmpStr + " " + String.Format("{0:X2}", RecvBuff(indx))
        Next indx

        If ACOSError(RecvBuff(0), RecvBuff(1)) Then
            RevokeDebit = INVALID_SW1SW2
            Exit Function
        End If

        If Trim(tmpStr) <> "90 00" Then
            RevokeDebit = INVALID_SW1SW2
            Exit Function
        End If

        RevokeDebit = retCode

    End Function


    Private Sub RBDES_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBDES.CheckedChanged
        txtCredit.MaxLength = 8
        txtDebit.MaxLength = 8
        txtCertify.MaxLength = 8
        txtRevoke.MaxLength = 8
        txtAmount.Text = ""
    End Sub


    Private Sub RB3DES_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB3DES.CheckedChanged
        txtCredit.MaxLength = 16
        txtDebit.MaxLength = 16
        txtCertify.MaxLength = 16
        txtRevoke.MaxLength = 16
        txtAmount.Text = ""
    End Sub

End Class

'============================================================================
' Description : This sample program demonstrates the Account functions using 
'               ACOS card.
'
'============================================================================