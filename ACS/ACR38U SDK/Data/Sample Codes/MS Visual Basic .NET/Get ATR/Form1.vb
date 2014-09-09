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
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnDisconnect As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents lstOutput As System.Windows.Forms.ListBox
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbReaderPort As System.Windows.Forms.ComboBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnATRValue As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnDisconnect = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.lstOutput = New System.Windows.Forms.ListBox
        Me.btnATRValue = New System.Windows.Forms.Button
        Me.btnConnect = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.cmbReaderPort = New System.Windows.Forms.ComboBox
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.btnDisconnect)
        Me.GroupBox2.Controls.Add(Me.btnClear)
        Me.GroupBox2.Controls.Add(Me.lstOutput)
        Me.GroupBox2.Controls.Add(Me.btnATRValue)
        Me.GroupBox2.Controls.Add(Me.btnConnect)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(464, 248)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(16, 216)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(112, 23)
        Me.btnExit.TabIndex = 6
        Me.btnExit.Text = "Exit"
        '
        'btnDisconnect
        '
        Me.btnDisconnect.Location = New System.Drawing.Point(16, 152)
        Me.btnDisconnect.Name = "btnDisconnect"
        Me.btnDisconnect.Size = New System.Drawing.Size(112, 23)
        Me.btnDisconnect.TabIndex = 5
        Me.btnDisconnect.Text = "Disconnect"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(16, 184)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(112, 23)
        Me.btnClear.TabIndex = 4
        Me.btnClear.Text = "Clear Output"
        '
        'lstOutput
        '
        Me.lstOutput.HorizontalScrollbar = True
        Me.lstOutput.Location = New System.Drawing.Point(144, 24)
        Me.lstOutput.Name = "lstOutput"
        Me.lstOutput.Size = New System.Drawing.Size(312, 212)
        Me.lstOutput.TabIndex = 3
        '
        'btnATRValue
        '
        Me.btnATRValue.Enabled = False
        Me.btnATRValue.Location = New System.Drawing.Point(16, 120)
        Me.btnATRValue.Name = "btnATRValue"
        Me.btnATRValue.Size = New System.Drawing.Size(112, 23)
        Me.btnATRValue.TabIndex = 2
        Me.btnATRValue.Text = "ATR Value"
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(16, 88)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(112, 23)
        Me.btnConnect.TabIndex = 1
        Me.btnConnect.Text = "Connect"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmbReaderPort)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 24)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(128, 48)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Reader Port"
        '
        'cmbReaderPort
        '
        Me.cmbReaderPort.Location = New System.Drawing.Point(8, 16)
        Me.cmbReaderPort.Name = "cmbReaderPort"
        Me.cmbReaderPort.Size = New System.Drawing.Size(112, 21)
        Me.cmbReaderPort.TabIndex = 1
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(480, 267)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Get ATR"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim retcode As Integer
    Dim hContext, hCard As Integer

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click

        Dim Aprotocol As Integer

        'Connect to selected reader using hContext handle and obtain hCard handle
        retcode = ModWinscard.SCardConnect(hContext, cmbReaderPort.Text, 1, 0 Or 1, hCard, Aprotocol)

        If retcode <> ModWinscard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add("Connection Failed!")
            lstOutput.Items.Add(GetScardErrMsg(retcode))
            Exit Sub
        Else
            lstOutput.Items.Add("Connection Successful!")

            ' set button
            btnATRValue.Enabled = True
        End If

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        lstOutput.Items.Clear()
    End Sub

    Private Sub btnDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisconnect.Click
        ' Disconnect card
        retcode = ModWinscard.SCardDisconnect(hCard, ModWinscard.SCARD_UNPOWER_CARD)

        If retcode <> ModWinscard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add("Disconnection Failed!")
            Exit Sub
        Else
            lstOutput.Items.Add("Disconnection Successful..")
        End If

        retcode = ModWinscard.SCardReleaseContext(hContext)

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        ' terminate the application
        retcode = ModWinscard.SCardDisconnect(hCard, 2)
        retcode = ModWinscard.SCardReleaseContext(hContext)
        End
    End Sub

    Private Sub btnATRValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnATRValue.Click

        Dim CardState, ATRLen As Integer
        Dim Protocol, ReaderLen, dwState As Integer
        Dim ATR(33) As Byte
        Dim index As Short, Temp As String
        Dim PRT As Integer
        Dim tmpReader = String.Empty
        ReaderLen = 100

        ATRLen = ModWinscard.SCARD_ATR_LENGTH
        ' perform the Card Status
        retcode = ModWinscard.SCardStatus(hCard, cmbReaderPort.Text, ReaderLen, dwState, Protocol, ATR(0), ATRLen)

        If retcode <> ModWinscard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add("SCardState Failed.")
            Exit Sub
        Else
            lstOutput.Items.Add("SCardStatus OK.")

            ' select for the protocol
            Select Case (Protocol)
                Case 0 : PRT = ModWinscard.SCARD_PROTOCOL_UNDEFINED
                    lstOutput.Items.Add("Active Protocol Undefined")
                Case 1 : PRT = ModWinscard.SCARD_PROTOCOL_T0
                    lstOutput.Items.Add("Active Protocol T0")
                Case 2 : PRT = ModWinscard.SCARD_PROTOCOL_T1
                    lstOutput.Items.Add("Active Protocol T1")
            End Select

            Temp = ""

            ' do loop for ATRlen
            For index = 0 To ATRLen - 1
                Temp = Temp + " " + String.Format("{0:X2}", ATR(index))
            Next index

            'Display ATR value
            lstOutput.Items.Add("ATR:" + Temp)

        End If

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim sReaderList As String
        Dim ctr As Integer
        Dim sReaderGroup As String
        Dim ReaderCount As Long

        For ctr = 0 To 255
            sReaderList = sReaderList + vbNullChar
        Next

        ReaderCount = 255
        'Establish context and obtain hContext handle
        retcode = ModWinscard.SCardEstablishContext(ModWinscard.SCARD_SCOPE_USER, 0, 0, hContext)

        If retcode <> ModWinscard.SCARD_S_SUCCESS Then
            lstOutput.Items.Add("Established Reader Failed.")
        End If
        ' List PCSC card readers installed
        retcode = ModWinscard.SCardListReaders(hContext, sReaderGroup, sReaderList, ReaderCount)

        Call LoadListToControl(cmbReaderPort, sReaderList)

        cmbReaderPort.SelectedIndex = -1

    End Sub


    Public Sub LoadListToControl(ByVal Ctrl As ComboBox, ByVal ReaderList As String)

        Dim sTemp As String
        Dim index As Integer

        index = 1
        sTemp = ""
        Ctrl.Items.Clear()
        While (Mid(ReaderList, index, 1) <> vbNullChar)
            While (Mid(ReaderList, index, 1) <> vbNullChar)
                sTemp = sTemp + Mid(ReaderList, index, 1)
                index = index + 1
            End While
            index = index + 1
            Ctrl.Items.Add(sTemp)
            sTemp = ""
        End While

    End Sub

    ' Routines for Error Codes    
    Public Function GetScardErrMsg(ByVal ReturnCode As Integer) As String

        Select Case ReturnCode
            Case ModWinscard.SCARD_E_CANCELLED
                GetScardErrMsg = "The action was canceled by an SCardCancel request."
            Case ModWinscard.SCARD_E_CANT_DISPOSE
                GetScardErrMsg = "The system could not dispose of the media in the requested manner."
            Case ModWinscard.SCARD_E_CARD_UNSUPPORTED
                GetScardErrMsg = "The smart card does not meet minimal requirements for support."
            Case ModWinscard.SCARD_E_DUPLICATE_READER
                GetScardErrMsg = "The reader driver didn't produce a unique reader name."
            Case ModWinscard.SCARD_E_INSUFFICIENT_BUFFER
                GetScardErrMsg = "The data buffer for returned data is too small for the returned data."
            Case ModWinscard.SCARD_E_INVALID_ATR
                GetScardErrMsg = "An ATR string obtained from the registry is not a valid ATR string."
            Case ModWinscard.SCARD_E_INVALID_HANDLE
                GetScardErrMsg = "The supplied handle was invalid."
            Case ModWinscard.SCARD_E_INVALID_PARAMETER
                GetScardErrMsg = "One or more of the supplied parameters could not be properly interpreted."
            Case ModWinscard.SCARD_E_INVALID_TARGET
                GetScardErrMsg = "Registry startup information is missing or invalid."
            Case ModWinscard.SCARD_E_INVALID_VALUE
                GetScardErrMsg = "One or more of the supplied parameter values could not be properly interpreted."
            Case ModWinscard.SCARD_E_NOT_READY
                GetScardErrMsg = "The reader or card is not ready to accept commands."
            Case ModWinscard.SCARD_E_NOT_TRANSACTED
                GetScardErrMsg = "An attempt was made to end a non-existent transaction."
            Case ModWinscard.SCARD_E_NO_MEMORY
                GetScardErrMsg = "Not enough memory available to complete this command."
            Case ModWinscard.SCARD_E_NO_SERVICE
                GetScardErrMsg = "The smart card resource manager is not running."
            Case ModWinscard.SCARD_E_NO_SMARTCARD
                GetScardErrMsg = "The operation requires a smart card, but no smart card is currently in the device."
            Case ModWinscard.SCARD_E_PCI_TOO_SMALL
                GetScardErrMsg = "The PCI receive buffer was too small."
            Case ModWinscard.SCARD_E_PROTO_MISMATCH
                GetScardErrMsg = "The requested protocols are incompatible with the protocol currently in use with the card."
            Case ModWinscard.SCARD_E_READER_UNAVAILABLE
                GetScardErrMsg = "The specified reader is not currently available for use."
            Case ModWinscard.SCARD_E_READER_UNSUPPORTED
                GetScardErrMsg = "The reader driver does not meet minimal requirements for support."
            Case ModWinscard.SCARD_E_SERVICE_STOPPED
                GetScardErrMsg = "The smart card resource manager has shut down."
            Case ModWinscard.SCARD_E_SHARING_VIOLATION
                GetScardErrMsg = "The smart card cannot be accessed because of other outstanding connections."
            Case ModWinscard.SCARD_E_SYSTEM_CANCELLED
                GetScardErrMsg = "The action was canceled by the system, presumably to log off or shut down."
            Case ModWinscard.SCARD_E_TIMEOUT
                GetScardErrMsg = "The user-specified timeout value has expired."
            Case ModWinscard.SCARD_E_UNKNOWN_CARD
                GetScardErrMsg = "The specified smart card name is not recognized."
            Case ModWinscard.SCARD_E_UNKNOWN_READER
                GetScardErrMsg = "The specified reader name is not recognized."
            Case ModWinscard.SCARD_F_COMM_ERROR
                GetScardErrMsg = "An internal communications error has been detected."
            Case ModWinscard.SCARD_F_INTERNAL_ERROR
                GetScardErrMsg = "An internal consistency check failed."
            Case ModWinscard.SCARD_F_UNKNOWN_ERROR
                GetScardErrMsg = "An internal error has been detected, but the source is unknown."
            Case ModWinscard.SCARD_F_WAITED_TOO_Integer
                GetScardErrMsg = "An internal consistency timer has expired."
            Case ModWinscard.SCARD_S_SUCCESS
                GetScardErrMsg = "No error was encountered."
            Case ModWinscard.SCARD_W_REMOVED_CARD
                GetScardErrMsg = "The smart card has been removed, so that further communication is not possible."
            Case ModWinscard.SCARD_W_RESET_CARD
                GetScardErrMsg = "The smart card has been reset, so any shared state information is invalid."
            Case ModWinscard.SCARD_W_UNPOWERED_CARD
                GetScardErrMsg = "Power has been removed from the smart card, so that further communication is not possible."
            Case ModWinscard.SCARD_W_UNRESPONSIVE_CARD
                GetScardErrMsg = "The smart card is not responding to a reset."
            Case ModWinscard.SCARD_W_UNSUPPORTED_CARD
                GetScardErrMsg = "The reader cannot communicate with the card, due to ATR string configuration conflicts."
            Case Else
                GetScardErrMsg = "?"
        End Select
    End Function

End Class
'**************************************************************************
' 
' DESCRIPTION :  This sample program demonstrate displaying the ATR value 
'                using PC/SC.  
'  
'***************************************************************************